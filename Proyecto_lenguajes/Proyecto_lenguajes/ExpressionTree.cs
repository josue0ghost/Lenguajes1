using System;
using System.Collections;
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
		public int Id { get; set; }
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

	class State
	{
		// estados [Key]		
		public List<int> states = new List<int>();
		// transiciones [Value]
		public List<int>[] transitions;
		// para evitar StackOverflow
		public bool Setted;

		public State(int NoSymbols, List<int> state)
		{
			states = state;
			transitions = new List<int>[NoSymbols];
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
		public Dictionary<List<int>, List<int>[]> tabla = new Dictionary<List<int>, List<int>[]>();
		public List<string> symbols = new List<string>();
		private List<Node> Leafs = new List<Node>();
		public List<State> states = new List<State>();
		private List<List<int>> AuxS = new List<List<int>>();

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
					//if (tokens.Count == 0)
					//{
					//	this.Error = "Faltan operandos en la expresion regular asignada a " + id
					//		+ ". No se puede cerrar una agrupacion sin antes abrirla";
					//	return;
					//}
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
						GetLastPrecedence(RE[i]); // modifies this.lastPrecedence

						if (this.lastPrecedence <= lstPrcdnc)
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
				root.Id = STid;
				STid++;

				root.First.Add(root.Id);
				root.Last.Add(root.Id);
				root.Nullable = false;
			}
			else // nodo es {"*", "?", "+", ".", "|" }
			{
				string[] Unary = new string[] {"*", "?", "+" };
				if (Unary.Contains(root.Item))
				{
					root.First = root.Left.First;
					root.Last = root.Left.Last;
					root.Nullable = (root.Item == "+") ? (root.Left.Nullable) : true;
				}
				else if (root.Item == "|")
				{
					List<int> aux = new List<int>(root.Left.First);

					root.First = aux;
					root.First.AddRange(root.Right.First);

					aux = new List<int>(root.Left.Last);

					root.Last = aux;
					root.Last.AddRange(root.Right.Last);

					root.Nullable = (root.Left.Nullable == true || root.Right.Nullable == true);
				}
				else // root.Item == "."
				{
					List<int> aux = new List<int>(root.Left.First);

					root.First = aux;
					if (root.Left.Nullable)
					{
						root.First.AddRange(root.Right.First);
					}

					aux = new List<int>(root.Right.Last);
					root.Last = aux;
					if (root.Right.Nullable)
					{
						root.Last.AddRange(root.Left.Last);
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
				string[] Unary = new string[] { "*", "+" }; // "?" no tiene follow
				if (Unary.Contains(root.Item))
				{
					// A L(c1) => F(c1)
					for (int i = 0; i < root.Left.Last.Count; i++)
					{
						Follows[root.Left.Last[i]].AddRange(root.Left.First.Except(Follows[root.Left.Last[i]]));
						Follows[root.Left.Last[i]].Sort();
					}
				}
				else if (root.Item == ".")
				{
					// A L(c1) => F(c2)
					for (int i = 0; i < root.Left.Last.Count; i++)
					{
						Follows[root.Left.Last[i]].AddRange(root.Right.First.Except(Follows[root.Left.Last[i]]));
						Follows[root.Left.Last[i]].Sort();
					}
				}				
			}
		}

		private void SortAll(Node root)
		{
			if (root == null)
			{
				return;
			}

			SortAll(root.Left);
			SortAll(root.Right);

			root.First.Sort();
			root.Last.Sort();
		}

		private void GetSymbols(Node root)
		{
			if (root == null)
			{
				return;
			}			

			GetSymbols(root.Left);
			GetSymbols(root.Right);

			if (root.Leaf)
			{
				Leafs.Add(root);
				if (!symbols.Contains(root.Item))
				{
					symbols.Add(root.Item);
				}
			}
		}

		public bool CalculateTransitionsTable(Node root)
		{
			if (root == null)
			{
				return false;
			}
			else
			{
				SortAll(root);
				GetSymbols(root);
				// El ultimo elemento es "#" ya que no sirve para el análisis de transiciones, se elimina
				symbols.RemoveAt(symbols.Count - 1);

				// El estado inicial es el First de la raíz
				states.Add(new State(symbols.Count, root.First));
				AuxS.Add(new List<int>(root.First));
				GetTransitions(states[0]);

				while (!NoMoreStates)
				{
					for (int i = 0; i < AuxS.Count; i++)
					{
						if (!states[i].Setted)
						{
							GetTransitions(states[i]);
						}
					}
				}

				return true;
			}			
		}

		bool NoMoreStates = true;
		private void GetTransitions(State st)
		{
			NoMoreStates = true;
			// Para cada simbolo
			for (int i = 0; i < symbols.Count; i++)
			{
				st.transitions[i] = new List<int>();
				// para cada id de hojas en el estado
				for (int j = 0; j < st.states.Count; j++)
				{
					// busca las coincidencias de la transicion con los symbols
					// según los estados

					// st.states[j] es el id de la hoja, se resta 1 para obtener el index en la lista de hojas
					if (Leafs[st.states[j] - 1].Item == symbols[i])
					{
						// si hay coincidencias se agregan los follows a esa transicion
						st.transitions[i].AddRange(Follows[st.states[j]].Except(st.transitions[i]));
					}
				}
			}
			// Se terminaron de calcular las transiciones del estado
			st.Setted = true;

			// Busca las transiciones que no estén incluidas en la lista de estados y las agrega
			for (int i = 0; i < st.transitions.Length; i++)
			{
				// si la transición no está en el conjunto de estados		
				if (Contains(AuxS, st.transitions[i]) == false)
				{
					states.Add(new State(symbols.Count, st.transitions[i]));
					AuxS.Add(st.transitions[i]);
					NoMoreStates = false;
				}
			}
		}

		private bool Contains(List<List<int>> a, List<int> b)
		{
			for (int i = 0; i < a.Count; i++)
			{
				if (b.All(x => a[i].Contains(x)) && b.Count == a[i].Count) // son totalmente iguales
				{
					return true;
				}				
			}
			return false;
		}
	}
}
