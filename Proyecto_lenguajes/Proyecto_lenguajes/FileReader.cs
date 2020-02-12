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

		public void Analize(string FileText)
		{
			string Error = "";
			List<string> FileReaded = new List<string>(FileText.Split('\n'));
			ContainsAllSections(FileReaded, ref Error);
		}

		/// <summary>
		/// Returns <see langword="false"/> if there's no TOKENS, ACTIONS or at least one ERROR in <paramref name="FileContent"/> elements
		/// , otherwise returns <see langword="true"/>
		/// </summary>
		/// <param name="FileContent">File lines stored in a List of Strings</param>
		/// <param name="Error">Referenced value gets a string if exist a error in <paramref name="FileContent"/></param>
		/// <returns></returns>
		private bool ContainsAllSections(List<string> FileContent, ref string Error)
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
		internal bool FindSection(string KeyWord, List<string> FileContent, ref string Error)
		{
			string ErrorType = FileContent.Find(str => str.Contains(KeyWord));
			if (ErrorType == null)
			{
				Error = "Se esperaba '" + KeyWord + "'";
				return false;
			}
			return true;
		}

		internal bool FindSection(string KeyWord, List<string> FileContent)
		{
			string ErrorType = FileContent.Find(str => str.Contains(KeyWord));
			if (ErrorType == null)
			{
				return false;
			}
			return true;
		}
	}
}
