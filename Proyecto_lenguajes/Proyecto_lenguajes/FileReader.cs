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
		public Dictionary<string, string> Sets = new Dictionary<string, string>();
		public Dictionary<string, ExpressionTree> Tokens = new Dictionary<string, ExpressionTree>();
		public Dictionary<string, List<string>> Actions = new Dictionary<string, List<string>>();
		public Dictionary<int, string> Errors = new Dictionary<int, string>();

		public string Error { get; set; }
		public string Warning { get; set; }
		private bool ContainsSets { get; set; }

		public string LineError = "";
		public int LineIndexError = 0;

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
			List<string> Lines = new List<string>(FileContent.Split('\n'));
			string Error = "";
			string Sets = "", Tokens = "", Actions = "", Errors = "";
			if (ContainsAllSections(FileContent, ref Error))
			{
				Split(FileContent, ref Sets, ref Tokens, ref Actions, ref Errors);
			}
			else
			{
				this.Error = Error;
				return;
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

			if (this.Error != "")
			{
				if (this.LineError != "")
				{
					string lineE = Lines.Find(x => x.Contains(this.LineError));
					this.LineIndexError = Lines.IndexOf(lineE) + 1;
				}				
			}
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
			//FileContent = FileContent.Replace(" ", "");
			//FileContent = FileContent.Replace('\t'.ToString(), "");
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
				Error = "Se esperaba sección '" + KeyWord + "'. ¿Quizá no está definida correctamente?";
				return false;
			}
			return true;
		}

		internal bool FindSection(string KeyWord, string FileContent)
		{			
			if (!FileContent.Contains(KeyWord))
			{
				this.Warning = "Advertencia. El archivo no contiene la seccion SETS o no está definida correctamente.";
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

		#region SETS
		/// <summary>
		/// Splits sets by '\n' and analize each one in order to find errors
		/// </summary>
		/// <param name="Sets"></param>
		/// <returns></returns>
		internal string SETS(string Sets)
		{
			string[] SetsArray = Sets.Split('\n');

			for (int i = 0; i < SetsArray.Length; i++)
			{
				string line = SetsArray[i];
				// si la cadena no contiene elementos para analizar
				if (line.Trim().Replace('\t'.ToString(), "") != "")
				{
					if (!AnalizeSet(line))
					{
						this.LineError = SetsArray[i];
						return this.Error;
					}
				}				
			}

			return "";
		}

		/// <summary>
		/// Analize a set in order to find errors. Returns <see langword="false"/> if syntax error was found
		/// </summary>
		/// <param name="Set"></param>
		/// <returns></returns>
		internal bool AnalizeSet(string Set)
		{
			bool ValidSet = true;
			Set.Trim();

			if (!Set.Contains(Utilities.EqualsSign.ToString()))
			{
				this.Error = "Formato de set incorrecto. Se esperaba asignación";
				return !ValidSet;
			}

			string id = Set.Substring(0, Set.IndexOf(Utilities.EqualsSign)).Replace(" ", "").Replace('\t'.ToString(), "");
			string value = Set.Substring(Set.IndexOf(Utilities.EqualsSign) + 1).Trim().Replace('\t'.ToString(), "");
			char[] expression = value.ToCharArray();

			bool lastWasRange = false;
			bool lastWasST = false;
			bool lastWasConcat = false;
			for (int i = 0; i < expression.Length; i++)
			{
				// expresiones '<char>'
				if (expression[i] == Utilities.CharLimiter)
				{
					if (lastWasST)
					{
						this.Error = "Debe existir una sentencia de concatenación o rango entre símbolos";
						return !ValidSet;
					}
					try
					{
						if (expression[i + 2] != Utilities.CharLimiter)
						{
							this.Error = "No se puede convertir explícitamente <string> en <char>";
							return !ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Se esperaba caracter de cierre de expresion de tipo <char>: " + Utilities.CharLimiter.ToString();
						return !ValidSet;
					}
					lastWasST = true;
					lastWasRange = false;
					lastWasConcat = false;
					i += 2;
				}
				// para funciones CHR
				else if (expression[i] == 'C')
				{
					if (lastWasST)
					{
						this.Error = "Debe existir una sentencia de concatenación o rango entre símbolos";
						return !ValidSet;
					}
					try
					{
						if (expression[i+1] != 'H' || expression[i+2] != 'R')
						{
							this.Error = "No se reconoce la funcion escrita. ¿Quizá quisiste escribir CHR(<int>)?";
							return !ValidSet;
						}

						if (expression[i + 3] != Utilities.OpeningBracket)
						{
							this.Error = "Se esperaba caracter de apertura en funcion CHR: " + Utilities.OpeningBracket.ToString();
							return !ValidSet;
						}

						i += 4;

						// verificar el formato para enteros "(<int>)"
						while (expression[i] != Utilities.ClosingBracket)
						{							
							if (!int.TryParse(expression[i].ToString(), out int x))
							{
								this.Error = "Se esperaba <int> como parámetro de función CHR";
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
					lastWasST = true;
					lastWasRange = false;
					lastWasConcat = false;
				}
				// Maneja los rangos ("..")
				else if (expression[i] == Utilities.Range)
				{
					if (lastWasRange || lastWasConcat)
					{
						this.Error = "No puede existir una sentencia de rango inmediata a una sentencia de rango o concatenación";
						return !ValidSet;
					}
					try
					{
						if (expression[i + 1] != Utilities.Range)
						{
							this.Error = "Se esperaba: " + Utilities.Range.ToString() + " en operación de tipo rango";
							return !ValidSet;
						}	
					}
					catch (Exception)
					{
						this.Error = "Formato de set incorrecto";
						return !ValidSet;
					}					
					i += 1;
					lastWasST = false;
					lastWasRange = true;
					lastWasConcat = false;
				}
				// Maneja las concatenaciones ('+')
				else if (expression[i] == Utilities.ConcatSign)
				{
					if (lastWasRange)
					{
						this.Error = "No puede existir una sentencia de concatenación inmediata a una sentencia de rango";
						return !ValidSet;
					}
					try
					{
						if (!(expression[i + 1] == '\'' || expression[i + 1] == 'C'))
						{
							this.Error = "Concatenación incorrecta. Se esperaba expresión de tipo '<char>' o CHR(<int>)";
							return !ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Concatenación incorrecta. Se esperaba expresión de tipo '<char>' o CHR(<int>)";
						return !ValidSet;
					}
					lastWasST = false;
					lastWasRange = false;
					lastWasConcat = true;
				}
				else // precede un caracter no esperado
				{
					this.Error = "Caracter no esperado en definición de set";
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

		#endregion

		#region TOKENS
		/// <summary>
		/// Splits tokens by '\n' and analize each one in order to find errors
		/// </summary>
		/// <param name="Tokens"></param>
		/// <returns></returns>
		internal string TOKENS(string Tokens)
		{
			string[] TokensArray = Tokens.Split('\n');

			for (int i = 0; i < TokensArray.Length; i++)
			{
				string line = TokensArray[i];
				if (line.Trim() != "")
				{
					if (!AnalizeToken(line))
					{
						this.LineError = TokensArray[i];
						return this.Error;
					}
				}
			}
			return "";
		}

		/// <summary>
		/// Analize token in order to find errors. Returns <see langword="false"/> if syntax error was found
		/// </summary>
		/// <param name="Token"></param>
		/// <returns></returns>
		internal bool AnalizeToken(string Token)
		{
			bool ValidToken = true;

			// si el token no tiene una asignación
			if (!Token.Contains(Utilities.EqualsSign))
			{
				this.Error = "Formato de token incorrecto. Se esperaba asignacion";
				return !ValidToken;
			}			

			// id = Token[0]..Token[IndexOf('EqualsSign')]
			string id = Token.Substring(0, Token.IndexOf(Utilities.EqualsSign)).Replace('\t'.ToString(), "").Replace(" ", "");			

			// si existe la cadena
			if (id.IndexOf("TOKEN", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				string num = "";
				char[] idChar = id.ToCharArray();
				// los numeros deberian empezar en la posicion 5 del arreglo de caracteres
				bool NumWasEvaluated = false;
				for (int i = 5; i < idChar.Length; i++)
				{
					if (int.TryParse(idChar[i].ToString(), out _))
					{
						num.Concat(idChar[i].ToString());
					}
					else
					{
						this.Error = "Se esperaba valor de tipo <int> en la definición de token";
						return !ValidToken;
					}
					NumWasEvaluated = true;
				}
				if (!NumWasEvaluated)
				{
					this.Error = "Se esperaba valor de tipo <int> en la definición de token";
					return !ValidToken;
				}
			}
			else
			{
				this.Error = "Falta la palabra 'TOKEN' en la definición del identificador del token";
				return !ValidToken;
			}

			if (Token != null && Tokens.ContainsKey(id))
			{
				this.Error = id + " ya fue declarado previamente.";
				return !ValidToken;				
			}			
			
			char[] RE = Token.Substring(Token.IndexOf(Utilities.EqualsSign) + 1).Trim().ToCharArray();

			bool lastWasST = false;
			bool lastWasOP = false;
			bool lastWasClsngB = false;
			bool BraceOpened = false;

			List<string> tlist = new List<string>();
			string set = "";							// la variable set se utiliza para los st que deberían estar definidos en los Sets
			tlist.Add("(");								// Para (<ER>).#
			for (int i = 0; i < RE.Length; i++)
			{
				// maneja tokens de tipo '<char>'
				if (RE[i] == Utilities.CharLimiter)
				{
					try
					{
						if (RE[i + 2] == Utilities.CharLimiter)
						{
							if (set != "")
							{
								tlist.Add(".");
								tlist.Add(set);
								set = "";
								lastWasST = true;
							}
							if (lastWasOP || lastWasST || lastWasClsngB)
							{
								tlist.Add(".");
							}
								
							tlist.Add("'" + RE[i + 1].ToString() + "'");
							lastWasST = true;
							lastWasOP = false;
							lastWasClsngB = false;
							i += 2;
						}
						else
						{
							this.Error = "Se esperaba expresión de tipo '<char>' en " + id;
							return !ValidToken;
						}
						
					}
					catch (Exception)
					{
						this.Error = "Se esperaba expresión de tipo '<char>' en " + id;
						return !ValidToken;
					}
				}
				else if (RE[i] == '(' || RE[i] == ')' || RE[i] == '|')
				{

					if (set != "")
					{
						tlist.Add(set);
						set = "";
					}

					if (RE[i] == '(')
					{
						tlist.Add(".");
						lastWasClsngB = false;
						//lastWasOpngB = true;
					}
					if (RE[i] == ')')
					{
						lastWasClsngB = true;
						//lastWasOpngB = false;
					}
					if (RE[i] == '|')
					{
						lastWasClsngB = false;
						//lastWasOpngB = false;
					}

					tlist.Add(RE[i].ToString());
					lastWasST = false;
					lastWasOP = false;
				}
				else if (RE[i] == '+' || RE[i] == '?' || RE[i] == '*')
				{
					if (set != "")
					{
						tlist.Add(set);
						set = "";
					}											
					tlist.Add(RE[i].ToString());
					lastWasOP = true;
					lastWasST = false;
					lastWasClsngB = false;
					//lastWasOpngB = false;
				}
				else if (RE[i] == ' ' || RE[i] == '\t')
				{
					if (set != "")
					{
						if (lastWasST || lastWasOP || lastWasClsngB)
						{
							tlist.Add(".");
						}							
						tlist.Add(set);
						lastWasST = true;
						lastWasOP = false;
						lastWasClsngB = false;
					}
					//if (lastWasOpngB)
					//{
					//	lastWasST = false;						
					//}				
					set = "";
				}
				// RE[i] == '{'
				else if (RE[i] == Utilities.OpeningBrace)
				{
					BraceOpened = true; // se inicio un '{'
					if (lastWasClsngB || lastWasST || lastWasOP)
					{
						tlist.Add(".");
					}
					i++;
					while (RE[i] != Utilities.ClosingBrace && RE.Length > i)
					{												
						if (RE[i] != ' ' && RE[i] != '\t')
						{
							set += RE[i];
						}
						i++;
					}

					if (RE.Length <= i)
					{
						this.Error = "Definición de función en " + id + " errónea. Se esperaba '}'";
						return !ValidToken;
					}
					tlist.Add(set);
					set = "";

					lastWasST = true;
					lastWasOP = false;
					lastWasClsngB = false;
				}
				// RE[i] == '}'
				else if (RE[i] == Utilities.ClosingBrace)
				{
					if (!BraceOpened)
					{
						this.Error = "Cierre de definición de función inesperado";
						return !ValidToken;
					}
					else
					{
						BraceOpened = false;
					}
				}
				else
				{
					set += RE[i].ToString();
				}
			}

			// en el caso de que haya quedado un Set sin agregar
			if (set != "")
			{
				tlist.Add(".");
				tlist.Add(set);
			}

			tlist.Add(")");					// Para (<ER>).#
			tlist.Add(".");					// Para (<ER>).#
			tlist.Add("#");					// Para (<ER>).#


			ExpressionTree ET = new ExpressionTree();
			ET.CreateTree(tlist, id);

			if (ET.Error != "")
			{
				this.Error = ET.Error;
				return !ValidToken;
			}

			Tokens.Add(id, ET);

			return ValidToken;
		}
		#endregion

		#region ACTIONS
		/// <summary>
		/// Analize actions. Search for RESERVADAS() function and sends all functions to analize in order to find errors.
		/// </summary>
		/// <param name="Actions"></param>
		/// <returns></returns>
		internal string ACTIONS(string Actions)
		{
			string sActions = Actions.Replace('\t'.ToString(), "");
			
			if (sActions.IndexOf("RESERVADAS()", StringComparison.OrdinalIgnoreCase) < 0)
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

		/// <summary>
		/// Splits by '{' and '}' and analize in order to find errors in function's type signature errors.
		/// Returns <see langword="false"/> if error was found
		/// </summary>
		/// <param name="Action"></param>
		/// <returns></returns>
		internal bool AnalizeAction(string Action)
		{
			bool ValidAction = true;

			char[] cAct = Action.ToCharArray();
			int countOpB = 0;	// {
			int countClB = 0;	// }
			for (int i = 0; i < cAct.Length; i++)
			{
				if (cAct[i] == '{')
				{
					countOpB++;
				}
				else if (cAct[i] == '}')
				{
					countClB++;
				}
			}
			// hay una misma cantidad de '{' que de '}'
			if (countOpB != countClB)
			{
				this.Error = "Formato de función incorrecta en Actions";
				return !ValidAction;
			}

			char[] delimiterChars = { '{', '}' };			
			string[] temp = Action.Split(delimiterChars);

			string id = "";
			string actions = "";
			for (int i = 0; i < temp.Length; i++)
			{
				string sTemp = temp[i];
				if (temp[i].Replace('\n'.ToString(), "").Trim() != "")
				{
					try
					{
						id = temp[i].Replace('\n'.ToString(), "").Trim();
						actions = temp[i + 1].Replace('\n'.ToString(), "").Trim();
						if (id != "" && actions != "")
						{
							if (SendActionToValidate(temp[i], temp[i + 1]))
							{
								Actions.Add(temp[i], new List<string>(temp[i + 1].Split('\n')));
							}
							else
							{
								if (this.LineError == "") // se cumple cuando el error fue que la definición ya había sido declarada con anterioridad
								{									
									this.LineError = sTemp;
								}
								return !ValidAction;
							}						
						}
						else
						{
							if (id == "")
							{
								this.LineError = temp[i]; // si no existe un id
							}
							else
							{
								this.LineError = temp[i + 1]; // si no existe una asignación
							}
							this.Error = "Formato de función incorrecto en sección ACTIONS";
							return !ValidAction;
						}
					}
					catch (Exception)
					{
						this.LineError = temp[i];
						this.Error = "Formato de función incorrecto en sección ACTIONS";
						return !ValidAction;
					}
					i++;
				}												
			}

			return ValidAction;
		}

		/// <summary>
		/// Recives a function's type signature for it's validation in order to find errors.
		/// Returns <see langword="false"/> if error was found
		/// </summary>
		/// <param name="id"></param>
		/// <param name="actions"></param>
		/// <returns></returns>
		private bool SendActionToValidate(string id, string actions)
		{
			// identificadores de funciones
			
			if (!ActionsID(id))
			{
				return false;
			}			
			// asignaciones		
			if (!ActionsAsigns(actions))
			{
				return false;
			}			

			if (Actions != null && Actions.ContainsKey(id))
			{
				this.Error = "La función ya fue declarada anteriormente";
				return false;
			}			
			return true;
		}

		/// <summary>
		/// Analize a function's id. Returns <see langword="false"/> if error was found
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		private bool ActionsID(string ID)
		{
			bool ValidID = true;

			char[] charID = ID.Trim().Replace('\n'.ToString(), "").ToCharArray();

			// no puede iniciar con un numero
			if (int.TryParse(charID[0].ToString(), out _))
			{
				this.LineError = ID.Replace('\n'.ToString(), "");
				this.Error = "Se esperaba un identificador. No puede iniciarse un identificador con un caracter numérico";
				return !ValidID;
			}
			// terminacion en "()"
			if (charID[charID.Length - 1] != Utilities.ClosingBracket || charID[charID.Length - 2] != Utilities.OpeningBracket)
			{
				this.LineError = ID.Replace('\n'.ToString(), "");
				this.Error = "Sintaxis de funcion incorrecta. Se esperaba '()'";
				return !ValidID;
			}
			return ValidID;
		}

		/// <summary>
		/// Analize a function's instructions in order to find errors.
		/// Returns <see langword="false"/> if error was found
		/// </summary>
		/// <param name="Asign"></param>
		/// <returns></returns>
		private bool ActionsAsigns(string Asign)
		{
			bool ValidAsign = true;

			string[] AsignsArray = Asign.Trim().Split('\n');

			foreach (var item in AsignsArray)
			{
				string[] temp = item.Split(Utilities.EqualsSign.ToCharArray()); // separa {id, asignación}

				if (temp.Length != 2)
				{
					if (temp[0].Trim() != "")
					{
						this.LineError = item;
						this.Error = "Formato de asignación incorrecto";
						return !ValidAsign;
					}					
				}
				else if (!int.TryParse(temp[0], out _))
				{
					this.LineError = item;
					this.Error = "Se esperaba identificador de tipo <int>";
					return !ValidAsign;
				}
				else if (!temp[1].Trim().StartsWith("'") || !temp[1].Trim().EndsWith("'"))
				{
					this.LineError = item;
					this.Error = "Se esperaba un valor con sintaxis: '<string>'";
					return !ValidAsign;
				}
				if (temp[1].Trim().Split(' ').Length > 1) // se definieron varias palabras
				{
					this.LineError = item;
					this.Error = "No se puede definir más de una palabra en una asignación de tipo '<string>'";
					return !ValidAsign;
				}
			}
			return ValidAsign;
		}
		#endregion
		
		/// <summary>
		/// Splits by '\n' ERRORs section and analize each one in order to find errors.
		/// Returns string with a description of the error
		/// </summary>
		/// <param name="Error"></param>
		/// <returns></returns>
		internal string ERRORS(string Error)
		{
			string[] ErrorArrays = Error.Split('\n');
			int num = 0;
			foreach (var item in ErrorArrays)
			{
				if (item.Replace(" ", "").Replace('\t'.ToString(), "") != "")
				{
					string[] temp = item.Split(Utilities.EqualsSign.ToCharArray());

					if (temp.Length != 2)
					{
						this.LineError = item;
						return "Sintaxis de asignacion de errores erronea";
					}
					else if (temp[0].IndexOf("ERROR") < 0)
					{
						this.LineError = item;
						return "El error no contiene el sufijo 'ERROR'";
					}
					else if (!int.TryParse(temp[1].Trim(), out num))
					{
						this.LineError = item;
						return "Sintaxis de asignacion de ERROR erronea. Se esperaba valor de tipo <int>";
					}

					char[] id = temp[0].Trim().ToCharArray();
					if (id[id.Length - 1] != 'R' || id[id.Length - 2] != 'O' || id[id.Length - 3] != 'R' || id[id.Length - 4] != 'R' || id[id.Length - 5] != 'E')
					{
						this.LineError = item;
						return "El error no contiene el sufijo 'ERROR'";
					}

					if (Errors != null && Errors.ContainsKey(num))
					{
						this.LineError = item;
						return "El numero de error ya fue asignado anteriormente";
					}
					Errors.Add(num, temp[0].Trim());
				}				
			}

			return "";
		}
	}
}