using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_lenguajes
{
	class FileReader
	{
		public Dictionary<string, string> Sets { get; set; }
		public Dictionary<string, ExpressionTree> Tokens { get; set; }
		public Dictionary<string, List<string>> Actions { get; set; }
		public string Error { get; set; }
		private bool ContainsSets { get; set; }

		public string Read(string path)
		{
			string freaded = "";
			using (StreamReader sr = new StreamReader(path))
			{
				freaded = sr.ReadToEnd();
			}
			return freaded;
		}

		public void Analize(string FileContent)
		{
			string Error = "";
			string Sets = "", Tokens = "", Actions = "", Errors = "";
			if (ContainsAllSections(FileContent, ref Error))
			{
				Split(FileContent, ref Sets, ref Tokens, ref Actions, ref Errors);
			}

			if (this.ContainsSets)
			{
				Error = SETS(Sets);
			}
			if (Error == "")
			{
				Error = TOKENS(Tokens);
			}
			if (Error == "")
			{
				Error = ACTIONS(Actions);
			}
			if (Error == "")
			{
				Error = ERRORS(Errors);
			}
			this.Error = Error;
		}

		/// <summary>
		/// Returns <see langword="false"/> if there's no TOKENS, ACTIONS or at least one ERROR in <paramref name="FileContent"/> elements
		/// , otherwise returns <see langword="true"/>
		/// </summary>
		/// <param name="FileContent">File lines stored in a List of Strings</param>
		/// <param name="Error">Referenced value gets a string if exist a error in <paramref name="FileContent"/></param>
		/// <returns></returns>
		private bool ContainsAllSections(string FileContent, ref string Error)
		{
			FileContent = FileContent.Replace(" ", "");
			FileContent = FileContent.Replace('\t'.ToString(), "");
			this.ContainsSets = FindSection("SETS", FileContent);
			if (!FindSection("TOKENS", FileContent, ref Error)) return false;
			if (!FindSection("ACTIONS", FileContent, ref Error)) return false;
			if (!FindSection("ERROR", FileContent, ref Error)) return false;

			return true;
		}

		/// <summary>
		/// Search for <paramref name="KeyWord"/> in <paramref name="FileContent"/>
		/// , sets <paramref name="Error"/> to a error message if <paramref name="KeyWord"/> not found
		/// </summary>
		/// <param name="KeyWord"></param>
		/// <param name="FileContent"></param>
		/// <param name="Error"></param>
		/// <returns></returns>
		internal bool FindSection(string KeyWord, string FileContent, ref string Error)
		{			
			if (!FileContent.Contains(KeyWord))
			{
				Error = "Se esperaba '".Concat(KeyWord).Concat("'").ToString();
				return false;
			}
			return true;
		}

		internal bool FindSection(string KeyWord, string FileContent)
		{			
			if (!FileContent.Contains(KeyWord))
			{
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// Splits <paramref name="FileContent"/> to get all file sections in other strings
		/// </summary>
		/// <param name="FileContent"></param>
		/// <param name="Sets"></param>
		/// <param name="Tokens"></param>
		/// <param name="Actions"></param>
		/// <param name="Errors"></param>
		internal void Split(string FileContent, ref string Sets, ref string Tokens, ref string Actions, ref string Errors)
		{
			Errors = FileContent.Substring(FileContent.IndexOf("ERROR"));
			Actions = FileContent.Substring(FileContent.IndexOf("ACTIONS") + 7, (FileContent.IndexOf("ERROR") - FileContent.IndexOf("ACTIONS")) - 7);
			Tokens = FileContent.Substring(FileContent.IndexOf("TOKENS") + 6, (FileContent.IndexOf("ACTIONS") - FileContent.IndexOf("TOKENS")) - 6);
			if (this.ContainsSets)
			{
				Sets = FileContent.Substring(FileContent.IndexOf("SETS") + 4, (FileContent.IndexOf("TOKENS") - FileContent.IndexOf("SETS")) - 4);
			}
		}

		internal string SETS(string Sets)
		{
			string[] SetsArray = Sets.Split('\n');

			for (int i = 0; i < SetsArray.Length; i++)
			{
				if (SetsArray[i].Trim().Replace('\t'.ToString(), "") != "")
				{
					if (!AnalizeSet(SetsArray[i]))
					{
						return this.Error;
					}
				}				
			}

			return "";
		}

		internal bool AnalizeSet(string Set)
		{
			bool ValidSet = true;
			Set.Trim();

			if (!Set.Contains(Utilities.EqualsSign.ToString()))
			{
				this.Error = "Formato de set incorrecto. Se esperaba asignación.";
				return !ValidSet;
			}

			string id = Set.Substring(0, Set.IndexOf(Utilities.EqualsSign)).Replace(" ", "").Replace('\t'.ToString(), "");
			string value = Set.Substring(Set.IndexOf(Utilities.EqualsSign) + 1).Trim().Replace('\t'.ToString(), "");
			char[] expression = value.ToCharArray();

			for (int i = 0; i < expression.Length; i++)
			{
				// expresiones '<char>'
				if (expression[i] == Utilities.CharLimiter)
				{
					try
					{
						if (expression[i + 2] != Utilities.CharLimiter)
						{
							this.Error = "No se puede convertir explicitamente <string> en <char>";
							return !ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Se esperaba caracter de cierre: ".Concat(Utilities.CharLimiter.ToString()).ToString();
						return !ValidSet;
					}
					
					i += 2;
				}
				// para funciones CHR
				else if (expression[i] == 'C')
				{
					try
					{
						if (expression[i+1] != 'H' || expression[i+2] != 'R')
						{
							this.Error = "No se reconoce la funcion escrita";
							return !ValidSet;
						}

						if (expression[i + 3] != Utilities.OpeningBracket)
						{
							this.Error = "Se esperaba caracter de apertura: ".Concat(Utilities.OpeningBracket.ToString()).ToString();
							return !ValidSet;
						}

						i += 4;

						// verificar el formato para enteros "(<int>)"
						while (expression[i] != Utilities.ClosingBracket)
						{							
							if (!int.TryParse(expression[i].ToString(), out int x))
							{
								this.Error = "Se esperaba numero entero";
								return !ValidSet;
							}
							i++;
						}
					}
					catch (Exception)
					{
						this.Error = "Formato de función CHR(<int>) incorrecto";
						return !ValidSet;
					}
				}
				// Maneja los rangos ("..")
				else if (expression[i] == Utilities.Range)
				{
					try
					{
						if (expression[i + 1] != Utilities.Range)
						{
							this.Error = "Se esperaba: ".Concat(Utilities.Range.ToString()).ToString();
							return !ValidSet;
						}	
					}
					catch (Exception)
					{
						this.Error = "Formato de set incorrecto";
						return !ValidSet;
					}					
					i += 1;
				}
				// Maneja las concatenaciones ('+')
				else if (expression[i] == Utilities.ConcatSign)
				{
					try
					{
						if (!(expression[i + 1] == '\'' || expression[i + 1] == 'C'))
						{
							this.Error = "Concatenación incorrecta. Se esperaba expresion de tipo '<char>' o CHR(<int>)";
							return !ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Concatenación incorrecta. Se esperaba expresion de tipo '<char>' o CHR(<int>)";
						return !ValidSet;
					}										
				}
				else // precede un caracter no esperado
				{
					this.Error = "Caracter no esperado";
					return !ValidSet;
				}
			}

			if (!Sets.ContainsKey(id))
			{
				Sets.Add(id, value);
			}
			else
			{
				this.Error = "El set ya fue definido anteriormente";
				return !ValidSet;
			}

			return ValidSet;
		}

		internal string TOKENS(string Tokens)
		{
			string[] TokensArray = Tokens.Split('\n');

			for (int i = 0; i < TokensArray.Length; i++)
			{
				if (TokensArray[i] != "")
				{
					if (!AnalizeToken(TokensArray[i]))
					{
						return this.Error;
					}
				}
			}
			return "";
		}

		internal bool AnalizeToken(string Token)
		{
			bool ValidToken = true;

			if (!Token.Contains(Utilities.EqualsSign))
			{
				this.Error = "Formato de token incorrecto. Se esperaba asignacion.";
				return !ValidToken;
			}			

			string id = Token.Substring(0, Token.IndexOf(Utilities.EqualsSign)).Replace('\t'.ToString(), "").Replace(" ", "");			

			// si existe la cadena
			if (id.IndexOf("TOKEN", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				string num = "";
				char[] idChar = id.ToCharArray();
				// los numeros deberian empezar en la posicion 5 del arreglo de caracteres
				for (int i = 5; i < idChar.Length; i++)
				{
					if (int.TryParse(idChar[i].ToString(), out _))
					{
						num.Concat(idChar[i].ToString());
					}
					else
					{
						this.Error = "Se esperaba valor de tipo <int>";
						return !ValidToken;
					}
				}
			}
			else
			{
				this.Error = "Falta la palabra 'TOKEN' en la definicion del identificador token";
				return !ValidToken;
			}

			if (Tokens.ContainsKey(id))
			{
				this.Error = id.Concat(" ya fue declarado previamente").ToString();
				return !ValidToken;
			}
			
			char[] RE = Token.Substring(Token.IndexOf(Utilities.EqualsSign) + 1).Trim().ToCharArray();

			try
			{
				if (RE[0] == '|' || RE[0] == '?' || RE[0] == '*' || RE[0] == '+')
				{
					this.Error = "Una expresion regular no puede iniciar con operadores";
					return !ValidToken;
				}
			}
			catch (Exception)
			{
				this.Error = "No existe la definicion de una expresion regular en ".Concat(id).ToString();
			}

			ExpressionTree ET = new ExpressionTree();
			ET.CreateTree(RE);

			Tokens.Add(id, ET);

			return ValidToken;
		}

		internal string ACTIONS(string Actions)
		{
			string sActions = Actions.Replace(" ", "").Replace('\t'.ToString(), "");

			if (sActions.IndexOf("RESERVADAS", StringComparison.OrdinalIgnoreCase) < 0)
			{
				this.Error = "Se esperaba funcion RESERVADAS() en ACTIONS";
				return this.Error;
			}

			if (!AnalizeAction(sActions))
			{
				return this.Error;
			}

			return "";
		}

		internal bool AnalizeAction(string Action)
		{
			bool ValidAction = true;
			char[] delimiterChars = { '{', '}' };
			string[] temp = Action.Split(delimiterChars);

			for (int i = 0; i < temp.Length; i++)
			{
				if (i % 2 == 0) // identificadores de funciones
				{
					if (!ActionsID(temp[i]))
					{
						return !ValidAction;
					}
				}
				else // firmas
				{
					if (!ActionsAsigns(temp[i]))
					{
						return !ValidAction;
					}
				}
			}

			return ValidAction;
		}

		private bool ActionsID(string ID)
		{
			bool ValidID = true;

			char[] charID = ID.Replace('\n'.ToString(), "").ToCharArray();

			// no puede iniciar con un numero
			if (int.TryParse(charID[0].ToString(), out _))
			{
				this.Error = "Se esperaba un identificador. No puede iniciarse un identificador con un caracter numerico";
				return !ValidID;
			}
			// terminacion en "()"
			if (charID[charID.Length - 1] != Utilities.ClosingBracket || charID[charID.Length - 2] != Utilities.OpeningBracket)
			{
				this.Error = "Sintaxis de funcion incorrecta. Se esperaba '()'";
				return !ValidID;
			}
			return ValidID;
		}

		private bool ActionsAsigns(string Asign)
		{
			bool ValidAsign = true;

			string[] AsignsArray = Asign.Split('\n');

			foreach (var item in AsignsArray)
			{
				string[] temp = item.Split(Utilities.EqualsSign.ToCharArray());

				if (temp.Length != 2)
				{
					this.Error = "Se esperaba asignacion";
					return !ValidAsign;
				}
				else if (!int.TryParse(temp[0], out _))
				{
					this.Error = "Se esperaba identificador de tipo <int>";
					return !ValidAsign;
				}
				else if (!temp[1].StartsWith("'") || !temp[1].EndsWith("'"))
				{
					this.Error = "Se esperaba un valor con sintaxis: '<string>'";
					return !ValidAsign;
				}
			}

			return ValidAsign;
		}

		internal string ERRORS(string Error)
		{
			string[] ErrorArrays = Error.Replace(" ", "").Replace('\t'.ToString(), "").Split('\n');

			foreach (var item in ErrorArrays)
			{
				string[] temp = item.Split(Utilities.EqualsSign.ToCharArray());

				if (temp.Length != 2)
				{
					return "Sintaxis de asignacion de errores erronea";
				}
				else if (temp[0].IndexOf("ERROR") < 0)
				{
					return "El error no contiene el sufijo 'ERROR'";
				}
				else if (!int.TryParse(temp[1], out _))
				{
					return "Sintaxis de asignacion de ERROR erronea. Se esperaba valor de tipo <int>";
				}
			}

			return "";
		}
	}
}
