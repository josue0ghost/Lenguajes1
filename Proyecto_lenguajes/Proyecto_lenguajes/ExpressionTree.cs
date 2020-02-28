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
		public string Error;
		private Stack<string> tokens;
		private Stack<ExpressionTree> trees;
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
			else if (token == " ")
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
		public void CreateTree(char[] RE, string id)
		{
			string set = "";
			for (int i = 0; i < RE.Length; i++)
			{
				if (RE[i] == '(')
				{
					tokens.Push(RE[i].ToString());
					GetLastPrecedence("(");
				}
				else if (RE[i] == ')')
				{
					while (tokens.Count > 0 && tokens.Peek() != "(")
					{
						if (tokens.Count == 0)
						{
							this.Error = "Faltan operandos en la expresion regular asignada a ".Concat(id)
								.Concat(". No se puede cerrar una agrupacion sin antes abrirla").ToString();
							return;
						}
						if (trees.Count < 2)
						{
							this.Error = "Faltan operandos en la expresion regular asignada a ".Concat(id).ToString();
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
				else if (RE[i] == '?' || RE[i] == '*' || RE[i] == '+')
				{
					ExpressionTree temp = new ExpressionTree();					
					temp.Root = new Node(RE[i].ToString());

					if (trees.Count == 0)
					{
						this.Error = "Faltan operandos";
						return;
					}

					ExpressionTree Left = new ExpressionTree();
					Left = trees.Pop();
					temp.Root.Left = Left.Root;

					trees.Push(temp);
					GetLastPrecedence(RE[i].ToString());
				}
				else if (RE[i] == '|' && tokens.Count > 0)
				{
					string top = tokens.Peek();
					int lstPrcdnc = this.lastPrecedence;
					if (top != "(")
					{
						GetLastPrecedence(tokens.Peek());
						if (lstPrcdnc < this.lastPrecedence)
						{

						}
					}
				}
				else if (RE[i] == Utilities.CharLimiter)
				{
					try
					{
						if (RE[i + 2] == Utilities.CharLimiter)
						{
							tokens.Push(RE[i + 1].ToString());
						}
						else
						{
							this.Error = "Formato de token incorrecto. Se esperaba expresion de tipo '<char>'";
							return;
						}
						i += 2;
					}
					catch (Exception)
					{
						this.Error = "Formato de token incorrecto. Se esperaba expresion de tipo '<char>'";
						return;
					}
				}
				else if (RE[i] != ' ' || RE[i] != '\t')
				{
					set.Concat(RE[i].ToString());
				}
				else if (RE[i] == ' ' || RE[i] == '\t')
				{
					if (set != "")
					{
						tokens.Push(set); // se toma ese id y se agrega a los items de la RE
						set = ""; // se reinicia set para un nuevo id
					}
				}
			}
		}


	}
}
