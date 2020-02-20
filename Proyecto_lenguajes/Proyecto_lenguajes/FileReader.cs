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
				if (SetsArray[i] != "")
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
			bool ValidSet = false;
			Set.Trim();

			string id = Set.Substring(0, Set.IndexOf(Utilities.EqualsSign));
			char[] expression = Set.Substring(Set.IndexOf(Utilities.EqualsSign) + 1).Trim().ToCharArray();			

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
							return ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Se esperaba caracter de cierre: ".Concat(Utilities.CharLimiter.ToString()).ToString();
						return ValidSet;
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
							return ValidSet;
						}

						if (expression[i + 3] != Utilities.OpenAgrupation)
						{
							this.Error = "Se esperaba caracter de apertura: ".Concat(Utilities.OpenAgrupation.ToString()).ToString();
							return ValidSet;
						}

						i += 4;

						// verificar el formato para enteros "(<int>)"
						while (expression[i] != Utilities.CloseAgrupation)
						{							
							if (!int.TryParse(expression[i].ToString(), out int x))
							{
								this.Error = "Se esperaba numero entero";
								return ValidSet;
							}
							i++;
						}
					}
					catch (Exception)
					{
						this.Error = "Formato de función CHR(<int>) incorrecto";
						return ValidSet;
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
							return ValidSet;
						}	
					}
					catch (Exception)
					{
						this.Error = "Formato de set incorrecto";
						return ValidSet;
					}					
					i += 1;
				}
				else if (expression[i] == Utilities.ConcatSign)
				{
					try
					{
						if (!(expression[i + 1] == '\'' || expression[i + 1] == 'C'))
						{
							this.Error = "Concatenación incorrecta";
							return ValidSet;
						}
					}
					catch (Exception)
					{
						this.Error = "Se esperaba expresion de tipo '<char>' o CHR(<int>)";
						return ValidSet;
					}										
				}
				else // precede un caracter no esperado
				{
					this.Error = "Caracter no esperado";
					return ValidSet;
				}
			}
			
			// TO DO
			// hay error si lo que está entre comillas tiene tamño mayor a 1 
			// Si hay un punto debe estar después de una comilla o paréntesis
			// Si hay un punto debe estar seguido de otro punto (si este no está entre comillas)
			// Si hay un signo + debe estar seguido de una comilla o C


			return true;
		}
	}
}
