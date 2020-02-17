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
				Split(FileContent, ref Sets, ref Tokens, ref Actions, ref Error);
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
				Error = "Se esperaba '" + KeyWord + "'";
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
			Actions = FileContent.Substring(FileContent.IndexOf("ACTIONS"), (FileContent.IndexOf("ERROR") - FileContent.IndexOf("ACTIONS")));
			Tokens = FileContent.Substring(FileContent.IndexOf("TOKENS"), (FileContent.IndexOf("ACTIONS") - FileContent.IndexOf("TOKENS")));
			if (this.ContainsSets)
			{
				Sets = FileContent.Substring(FileContent.IndexOf("SETS"), (FileContent.IndexOf("TOKENS") - FileContent.IndexOf("SETS")));
			}
		}
	}
}
