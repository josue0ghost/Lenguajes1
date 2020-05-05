using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_lenguajes
{
	class Scanner
	{
		List<string> GeneralER = new List<string>();

		public void GenerateExpressionTree(FileReader fr)
		{
			GeneralER.Add("(");
			foreach (var item in fr.Tokens)
			{
				GeneralER.Add("(");
				for (int i = 0; i < item.Value.Count; i++)
				{					
					GeneralER.Add(item.Value[i]);					
				}
				GeneralER.Add(")");
				GeneralER.Add("|");
			}
			GeneralER.RemoveAt(GeneralER.Count - 1); // Eliminar el último "|"
			GeneralER.Add(")");
			GeneralER.Add(".");
			GeneralER.Add("#");

			Data.Instance.Tree = new ExpressionTree();
			Data.Instance.Tree.CreateTree(GeneralER, "");			
		}

		public string GenerateScanner()
		{
			string output = Librerias();
			output += Clase();
			output += AgregarSets();
			output += PrintMain();

			for (int i = 0; i < Data.Instance.Tree.states.Count; i++)
			{
				output += PrintTransFunctions(i);
			}

			output += PrintObtenerToken();

			output += "\t}\n" +
						"}";			

			return output;
		}

		private string Librerias()
		{
			return "using System;\n" +
				"using System.Collections;\n" +
				"using System.Collections.Generic;\n" +
				"using System.Linq;\n";
		}

		private string Clase()
		{
			return	"namespace Scanner\n" +
					"{\n" +
					"\tclass Automata\n" +
					"\t{\n" +
					"\t\tstatic Queue<char> Entrada = new Queue<char>();\n" +
					"\t\tstatic string token =" + " \"\";\n";        		
		}

		private string AgregarSets()
		{			
			string result = "";
			for (int i = 0; i < Data.Instance.fr.Sets.Count; i++)
			{
				KeyValuePair<string, List<char>> kp = Data.Instance.fr.Sets.ElementAt(i);
				result += "\t\tstatic char[] " + kp.Key + "array = new char[] {";
				for (int j = 0; j < kp.Value.Count - 1; j++)
				{
					if (kp.Value[j] == '\'' || kp.Value[j] == '"' || kp.Value[j] == '\\')
					{
						result += "\'" + "\\" + kp.Value[j].ToString() + "\'" + ",";
					}
					else
					{
						result += "\'" + kp.Value[j].ToString() + "\'" + ",";
					}
					
				}
				result += "\'" + kp.Value[kp.Value.Count - 1].ToString() + "\'};\n";
				result += "\t\tstatic List<char> " + kp.Key + " = new List<char>(" + kp.Key + "array);\n";
			}
			
			return result;
		}

		private string PrintMain()
		{
			string result = "";
			result += "\t\tstatic void Main()\n" +
						"\t\t{\n" +
						"\t\t\tConsole.WriteLine(\"Entrada: \");\n" +
						"\t\t\tstring input = Console.ReadLine();\n" +
						"\t\t\tchar[] cinput = input.ToCharArray();\n" +
						"\t\t\tbool aceptacion = false;\n" +
						"\t\t\tEntrada = new Queue<char>(cinput);\n" +
						"\t\t\tint Estado = 0;\n" +
						"\t\t\twhile(Entrada.Count > 0)\n" +
						"\t\t\t{\n" +
						"\t\t\t\tchar ToConsume = Entrada.Peek();\n";

			result += PrintMainSwitch();
			result += AcceptConditions();
			result +=   "\t\t\t}\n";
			result += AcceptConditionsv2();
			result +=	"\t\t}\n";
			
			return result;
		}

		private string PrintMainSwitch()
		{
			string result = "";
			result +=		"\t\t\t\tswitch (Estado)\n" +
							"\t\t\t\t{\n";

			for (int i = 0; i < Data.Instance.Tree.states.Count; i++)
			{
				result +=	"\t\t\t\t\tcase " + i.ToString() + ":\n" +
							"\t\t\t\t\t\tEstado = Trans" + i.ToString() + "(ToConsume);\n";

				int STaccept = Data.Instance.Tree.TerminalSymbolID - 1;
				string acceptStr = Data.Instance.Tree.states[i].states.Contains(STaccept) ? "aceptacion = true;" : "aceptacion = false;";

				result +=	"\t\t\t\t\t\t" + acceptStr + "\n" +
							"\t\t\t\t\t\tbreak;\n";
			}

			result +=		"\t\t\t\t\tdefault:\n" +
							"\t\t\t\t\t\tbreak;\n" +
							"\t\t\t\t}\n";
			return result;
		}

		private string AcceptConditions()
		{
			string result = "";

			result +=	"\t\t\t\tif (Estado == -1 && aceptacion == true)\n" +
						"\t\t\t\t{\n" +
						"\t\t\t\t\tstring tkn = obtenerToken(token);\n" +
						"\t\t\t\t\tConsole.WriteLine(token + \" = \" + tkn);\n" +
						"\t\t\t\t\tEntrada.Dequeue();\n" +
						"\t\t\t\t\ttoken = \"\";\n"  +
						"\t\t\t\t\tEstado = 0;\n" +
						"\t\t\t\t\taceptacion = false;\n" +
						"\t\t\t\t}\n" +
						"\t\t\t\tif (Estado == -1 && aceptacion == false)\n" +
						"\t\t\t\t{\n" +
						"\t\t\t\t\tstring tkn = obtenerToken(token);\n" +
						"\t\t\t\t\tConsole.WriteLine(token + \" = \" + tkn);\n" +
						"\t\t\t\t\tEntrada.Dequeue();\n" +
						"\t\t\t\t\ttoken = \"\";\n" +
						"\t\t\t\t\tEstado = 0;\n" +
						"\t\t\t\t\tEntrada.Dequeue();\n" +
						"\t\t\t\t}\n";

			return result;
		}

		private string AcceptConditionsv2()
		{
			string result = "";

			result +=	"\t\t\tif (";

			int STaccept = Data.Instance.Tree.TerminalSymbolID - 1;
			for (int i = 0; i < Data.Instance.Tree.states.Count; i++)
			{				
				if (Data.Instance.Tree.states[i].states.Contains(STaccept))
				{
					result += "Estado == " + i.ToString() + " || ";
				}
			}
			// eliminar ultimo " || "
			result = result.Remove(result.Length - 1);
			result = result.Remove(result.Length - 1);
			result = result.Remove(result.Length - 1);
			result = result.Remove(result.Length - 1);

			result +=	")\n" +
						"\t\t\t{\n" +
						"\t\t\t\tstring tkn = obtenerToken(token);\n" +
						"\t\t\t\tConsole.WriteLine(token + \" = \" + tkn);\n" +
						"\t\t\t\ttoken = \"\";\n" +
						"\t\t\t}\n";
			 
			return result;
		}

		private string PrintTransFunctions(int Estado)
		{
			string result = "";

			result +=	"\t\tstatic int Trans" + Estado.ToString() + "(char input)\n" +
						"\t\t{\n" +
						"\t\t\tint ret;\n" +
						"\t\t\tswitch (input)\n" +
						"\t\t\t{\n";

			for (int i = 0; i < Data.Instance.Tree.states[Estado].transitions.Length; i++)
			{
				if (Data.Instance.Tree.states[Estado].transitions[i].Count > 0)
				{
					string Symb = Data.Instance.Tree.symbols[i];
					if (Symb[0] == '\'') // '<char>'
					{
						if (Symb == "'''")
						{
							result += "\t\t\t\tcase " + "'" + "\\" + "'':\n";
						}
						else if (Symb == "'\\'")
						{
							result += "\t\t\t\tcase " + "'" + "\\" + "\\" + "':\n";
						}
						else
						{
							result += "\t\t\t\tcase " + Symb + ":\n";
						}
						int NextState = GetStateID(Data.Instance.Tree.states[Estado].transitions[i]);

						result += "\t\t\t\t\tret = " + NextState.ToString() + ";\n" +
									"\t\t\t\t\ttoken += input.ToString();\n" +
									"\t\t\t\t\tEntrada.Dequeue();\n" +
									"\t\t\t\t\tbreak;\n";

					}
				}				
			}

			result +=	"\t\t\t\tdefault:\n" +
						"\t\t\t\t\tret = -1;\n" +
						"\t\t\t\t\tbreak;\n" +
						"\t\t\t}\n";

			for (int i = 0; i < Data.Instance.Tree.states[Estado].transitions.Length; i++)
			{
				if (Data.Instance.Tree.states[Estado].transitions[i].Count > 0)
				{
					string Symb = Data.Instance.Tree.symbols[i];
					if (Symb[0] != '\'') // SET
					{
						int NextState = GetStateID(Data.Instance.Tree.states[Estado].transitions[i]);

						result += "\t\t\tif (" + Symb + ".IndexOf(input) >= 0)\n" +
									"\t\t\t{\n" +
									"\t\t\t\tret = " + NextState.ToString() + ";\n" +
									"\t\t\t\ttoken += input.ToString();\n" +
									"\t\t\t\tEntrada.Dequeue();\n" +
									"\t\t\t}\n";
					}
				}				
			}

			result +=	"\t\t\treturn ret;\n" +
						"\t\t}\n";
			return result;
		}

		private int GetStateID(List<int> ToCompare)
		{
			int result = -1;
			for (int i = 0; i < Data.Instance.Tree.states.Count; i++)
			{
				List<int> Comparing = Data.Instance.Tree.states[i].states;
				if (ToCompare.All(x => Comparing.Contains(x)) && ToCompare.Count == Comparing.Count)
				{
					result = i;
					break;
				}
			}
			return result;
		}

		private string PrintObtenerToken()
		{
			string result = "";

			result += "\t\tstatic string obtenerToken(string cadena)\n" +
						"\t\t{\n" +
						"\t\t\tstring tkn = \"\";\n";
			result += PrintTokens();
			result += "\t\t\tDictionary<int, string> actions = new Dictionary<int, string>(){";

			Dictionary<string, List<string>> actions = Data.Instance.fr.Actions;

			for (int i = 0; i < actions.Count; i++)
			{
				result += PrintActions(actions.Values.ElementAt(i));
			}
			result.Remove(result.Length - 1); // eliminar ultima ,
			result += "};\n";

			result +=	"\t\t\tif (actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Value != null)\n" +
						"\t\t\t{\n" +
						"\t\t\t\ttkn = actions.FirstOrDefault(x => x.Value.Equals(cadena, StringComparison.OrdinalIgnoreCase)).Key.ToString();\n" +
						"\t\t\t}\n" +
						"\t\t\telse if (tokensVal.Contains(cadena))\n" +
						"\t\t\t{\n" +
						"\t\t\t\tint index = tokensVal.IndexOf(cadena);\n" +
						"\t\t\t\ttkn = tokensID[index];\n" +
						"\t\t\t}\n";

			result += "\t\t\treturn tkn;\n";
			result += "\t\t}\n";
			return result;
		}

		internal bool TokenUsesSets(List<string> token)
		{
			for (int i = 0; i < token.Count; i++)
			{
				if (Data.Instance.fr.Sets.ContainsKey(token[i]))
				{
					return true;
				}
			}
			return false;
		}

		internal string Concat(List<string> token)
		{
			string result = "";

			for (int i = 0; i < token.Count; i++)
			{
				if (token[i] != ".")
				{					
					result += token[i][1].ToString();
				}
			}
			return result;
		}

		private string PrintTokens()
		{
			string result = "";

			result += "\t\t\tList<string> tokensVal = new List<string> {";
			Dictionary<string, List<string>> tokens = Data.Instance.fr.Tokens;
			for (int i = 0; i < tokens.Count; i++)
			{
				if (!TokenUsesSets(tokens.Values.ElementAt(i)))
				{
					result += "\"" + Concat(tokens.Values.ElementAt(i)) + "\",";
				}
				else
				{
					result += "\"\",";
				}
			}
			result = result.Remove(result.Length - 1);
			result += "};\n";

			result += "\t\t\tList<string> tokensID = new List<string> {";
			for (int i = 0; i < tokens.Count; i++)
			{
				result += "\"" + tokens.Keys.ElementAt(i).Remove(0,5) + "\",";				
			}
			result = result.Remove(result.Length - 1);
			result += "};\n";

			return result;
		}

		private string PrintActions(List<string> action)
		{
			string result = "";		
			
			for (int i = 0; i < action.Count; i++)
			{
				if (action[i].Replace('\t'.ToString(), "").Replace(" ", "") != "")
				{
					string[] reservada = action[i].Split('=');
					result += "{" + reservada[0] + ", \"" + reservada[1].Replace("'", "").Replace(" ", "") + "\"},";
				}				
			}			

			return result;
		}
	}
}
