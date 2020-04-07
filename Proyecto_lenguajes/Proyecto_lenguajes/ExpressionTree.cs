using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_lenguajes
{
	class Node
	{
		public Node Left { get; set; }
		public Node Right { get; set; }
		public string Item { get; set; }
		public int id { get; set; }
		// funcionalidades
		public List<int> First = new List<int>();
		public List<int> Last = new List<int>();		
		public bool Nullable { get; set; }


		public bool Leaf => (this.Left == null && this.Right == null);
		public bool Full => (this.Left != null && this.Right != null);

		public Node() { }

		public Node(string value) : this(value, null, null) { }
		
		public Node(string value, Node Left, Node Right)
		{
			this.Item = value;
			this.Left = Left;
			this.Right = Right;
		}

	}
	class ExpressionTree
	{
		public Node Root { get; set; }
		public string Error = "";
		private Stack<string> tokens = new Stack<string>();
		private Stack<ExpressionTree> trees = new Stack<ExpressionTree>();
		private int lastPrecedence = int.MaxValue;
		private int STid = 1;
		public Dictionary<int, List<int>> Follows = new Dictionary<int, List<int>>();		

		private void GetLastPrecedence(string token)
		{
			if (token == "(" || token == ")")
			{
				lastPrecedence = 4;
			}
			else if (token == "*" || token == "+" || token == "?")
			{
				lastPrecedence = 3;
			}
			else if (token == ".")
			{
				lastPrecedence = 2;
			}
			else if (token == "|")
			{
				lastPrecedence = 1;
			}
		}		

		public ExpressionTree()
		{

		}

		/// <summary>
		/// Algoritmo creado por Moises Alonso
		/// Algoritmo de creacion de arbol de expresion a traves de expresion regular
		/// </summary>
		/// <param name="RE"></param>
		public void CreateTree(List<string> RE, string id)
		{
			int lstPrcdnc = 0;
			for (int i = 0; i < RE.Count; i++)
			{								
				if (RE[i] == "(")
				{
					tokens.Push(RE[i].ToString());
					GetLastPrecedence(RE[i]);
				}
				else if (RE[i] == ")")
				{
					while (tokens.Count > 0 && tokens.Peek() != "(")
					{
						if (tokens.Count == 0)
						{
							this.Error = "Faltan operandos en la expresion regular asignada a " + id
								+ ". No se puede cerrar una agrupacion sin antes abrirla";
							return;
						}
						if (trees.Count < 2)
						{
							this.Error = "Faltan operandos en la expresion regular asignada a " + id;
							return;
						}

						ExpressionTree temp = new ExpressionTree();						
						temp.Root = new Node(tokens.Pop());

						ExpressionTree Right = trees.Pop();
						temp.Root.Right = Right.Root;
						ExpressionTree Left = trees.Pop();
						temp.Root.Left = Left.Root;

						trees.Push(temp);
					}
					if (tokens.Count == 0)
					{
						this.Error = "Faltan operandos en la expresion regular asignada a " + id
							+ ". No se puede cerrar una agrupacion sin antes abrirla";
						return;
					}
					tokens.Pop();
					GetLastPrecedence(")");
				}
				else if (Utilities.Op.Contains(RE[i]))
				{
					if (RE[i] == "?" || RE[i] == "*" || RE[i] == "+")
					{
						ExpressionTree temp = new ExpressionTree();
						temp.Root = new Node(RE[i].ToString());

						if (trees.Count == 0)
						{
							this.Error = "Faltan operandos en la expresion regular asignada a " + id;
							return;
						}

						ExpressionTree Left = new ExpressionTree();
						Left = trees.Pop();
						temp.Root.Left = Left.Root;

						trees.Push(temp);
						GetLastPrecedence(RE[i].ToString());
					}
					else if (tokens.Count > 0 && tokens.Peek() != "(")
					{
						lstPrcdnc = this.lastPrecedence;
						GetLastPrecedence(RE[i]);

						if (this.lastPrecedence < lstPrcdnc)
						{
							ExpressionTree temp = new ExpressionTree();
							temp.Root = new Node(tokens.Pop().ToString());

							if (trees.Count() < 2)
							{
								this.Error = "Faltan operandos en la expresion regular asignada a " + id;
								return;
							}

							ExpressionTree ctree = trees.Pop();
							temp.Root.Right = ctree.Root;

							ctree = trees.Pop();
							temp.Root.Left = ctree.Root;

							trees.Push(temp);
						}
					}
					
					if (RE[i] == "|" || RE[i] == ".")
					{
						tokens.Push(RE[i]);
						GetLastPrecedence(RE[i]);
					}					
				}				
				else
				{
					ExpressionTree temp = new ExpressionTree();
					temp.Root = new Node(RE[i]);

					trees.Push(temp);
				}									
			}

			while (tokens.Count > 0)
			{
				string op = tokens.Pop();
				if (op == "(" || trees.Count < 2)
				{
					this.Error = "Faltan operandos en la expresion regular asignada a " + id;
					return;
				}

				ExpressionTree temp = new ExpressionTree();
				temp.Root = new Node(op);

				ExpressionTree ctree = new ExpressionTree();
				ctree = trees.Pop();
				temp.Root.Right = ctree.Root;

				ctree = trees.Pop();
				temp.Root.Left = ctree.Root;

				trees.Push(temp);
			}

			if (trees.Count != 1)
			{
				this.Error = "Faltan operandos en la expresion regular asignada a " + id;
				return;
			}

			ExpressionTree result = trees.Pop();

			this.Root = result.Root;
		}

		/// <summary>
		/// Calculos realizando un recorrido post order
		/// </summary>
		public void ClaculateFirst_Last_n_Follow()
		{
			CalculateFirst_n_Last(this.Root);
			CalculateFollow(this.Root);
		}
		
		/// <summary>
		/// Calculo de First y Last en un recorrido post order
		/// </summary>
		/// <param name="root"></param>
		private void CalculateFirst_n_Last(Node root)
		{
			if (root == null)
			{
				return;
			}
			CalculateFirst_n_Last(root.Left);
			CalculateFirst_n_Last(root.Right);

			if (root.Leaf) // nodo hoja es ST
			{
				Follows.Add(STid, new List<int>());
				root.id = STid;
				STid++;

				root.First.Add(root.id);
				root.Last.Add(root.id);
				root.Nullable = false;
			}
			else // nodo es {"*", "?", "+", ".", "|" }
			{
				string[] Unary = new string[] {"*", "?", "+" };
				if (Unary.Contains(root.Item))
				{
					root.First = root.Left.First;
					root.Last = root.Left.Last;
					root.Nullable = (root.Item == "+") ? false : true;
				}
				else if (root.Item == "|")
				{
					root.First = root.Left.First;
					root.First.AddRange(root.Right.First);
					root.Last = root.Left.Last;
					root.Last.AddRange(root.Right.Last);
					root.Nullable = (root.Left.Nullable == true || root.Right.Nullable == true);
				}
				else // root.Item == "."
				{
					root.First = root.Left.First;
					if (root.Left.Nullable)
					{
						root.First.AddRange(root.Right.First);
					}

					root.Last = root.Right.Last;
					if (root.Right.Nullable)
					{
						root.First.AddRange(root.Left.Last);
					}

					root.Nullable = (root.Left.Nullable == true && root.Right.Nullable == true);
				}
			}
		}

		/// <summary>
		/// Calculo de Follows en un recorrido post order
		/// </summary>
		/// <param name="root"></param>
		private void CalculateFollow(Node root)
		{
			if (root == null)
			{
				return;
			}
			CalculateFollow(root.Left);
			CalculateFollow(root.Right);

			if (!root.Leaf) // nodo es {"*", "?", "+", ".", "|" }
			{
				string[] Unary = new string[] { "*", "?", "+" };
				if (Unary.Contains(root.Item))
				{
					// A L(c1) => F(c1)
					for (int i = 0; i < root.Left.Last.Count; i++)
					{
						Follows[root.Left.Last[i]].AddRange(root.Left.First);
					}
				}
				else if (root.Item == ".")
				{
					// A L(c1) => F(c2)
					for (int i = 0; i < root.Left.Last.Count; i++)
					{
						Follows[root.Left.Last[i]].AddRange(root.Right.First);
					}
				}				
			}
		}
	}
}
