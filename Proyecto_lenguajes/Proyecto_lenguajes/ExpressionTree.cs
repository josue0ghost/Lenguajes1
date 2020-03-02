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
		string item { get; set; }
		public bool Leaf => (this.Left == null && this.Right == null);
		public bool Full => (this.Left != null && this.Right != null);

		public Node() { }

		public Node(string value) : this(value, null, null) { }
		
		public Node(string value, Node Left, Node Right)
		{
			this.item = value;
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
			string set = "";
			int lstPrcdnc = 0;
			for (int i = 0; i < RE.Count; i++)
			{				
				if (tokens.Count > 0)
				{
					lstPrcdnc = this.lastPrecedence;
					GetLastPrecedence(tokens.Peek());
				}
				
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
					else if (tokens.Count > 0 && tokens.Peek() != "(" && this.lastPrecedence < lstPrcdnc)
					{
						ExpressionTree temp = new ExpressionTree();
						temp.Root = new Node(RE[i].ToString());

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
					else if (RE[i] == "|" || RE[i] == ".")
					{
						tokens.Push(RE[i]);
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

			this.Root = new Node(".")
			{
				Right = new Node("#"),
				Left = result.Root
			};
		}


	}
}
