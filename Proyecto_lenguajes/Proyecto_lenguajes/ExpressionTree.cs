using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_lenguajes
{
	class Node
	{

	}
	class ExpressionTree
	{
		public Node Father;
		public Node Left;
		public Node Right;
		public string Error;
		private Stack<string> items;
		private Stack<ExpressionTree> trees;
		

		public void CreateTree(char[] RE)
		{
			string set = "";
			for (int i = 0; i < RE.Length; i++)
			{
				if (RE[i] == '(')
				{

				}
				else if (RE[i] == '|' || RE[i] == '?' || RE[i] == '*' || RE[i] == '+')
				{
					items.Push(RE[i].ToString());
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
