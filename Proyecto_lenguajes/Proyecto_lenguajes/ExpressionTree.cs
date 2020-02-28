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
		private Stack<string> items;
		private Stack<ExpressionTree> trees;

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
					items.Push(RE[i].ToString());
				}
				else if (RE[i] == ')')
				{
					while (items.Count > 0 && items.Peek() != "(")
					{
						if (items.Count == 0)
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
						Node node = new Node(items.Pop());
						temp.Root = node;

						ExpressionTree Right = trees.Pop();
						temp.Root.Right = Right.Root;
						ExpressionTree Left = trees.Pop();
						temp.Root.Left = Left.Root;

						trees.Push(temp);
					}

					items.Pop();
				}
				else if (RE[i] == '?' || RE[i] == '*' || RE[i] == '+')
				{
					ExpressionTree temp = new ExpressionTree();
					Node node = new Node(RE[i].ToString());
					temp.Root = node;

					if (trees.Count == 0)
					{
						this.Error = "Faltan operandos";
						return;
					}

					ExpressionTree Left = new ExpressionTree();
					Left = trees.Pop();
					temp.Root.Left = Left.Root;

					trees.Push(temp);
				}
				else if (RE[i] == '|')
				{

				}
				else if (RE[i] == Utilities.CharLimiter)
				{
					try
					{
						if (RE[i + 2] == Utilities.CharLimiter)
						{
							items.Push(RE[i + 1].ToString());
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
						items.Push(set); // se toma ese id y se agrega a los items de la RE
						set = ""; // se reinicia set para un nuevo id
					}
				}
			}
		}


	}
}
