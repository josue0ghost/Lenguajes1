using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_lenguajes
{
	class FileLecture
	{
		List<string> FileReaded = new List<string>();
		public void Read(string path)
		{
			using (StreamReader sr = new StreamReader(path))
			{
				string sline = "";
				while (sline != null)
				{
					sline = sr.ReadLine();
					if (sline != null)
						FileReaded.Add(sline);
				}
			}

			ContainsAllSections();
		}

		private bool ContainsAllSections()
		{
			if (FileReaded.Contains("SETS"))
				if (FileReaded.Contains("TOKENS"))
					if (FileReaded.Contains("ACTIONS"))
						if (FileReaded.Contains("ERROR"))
							return true;
						else
							return false;
					else
						return false;
				else
					return false;
			return false;
		}

		private void FileSets()
		{
			using (StreamWriter sw = new StreamWriter("SETS_"));
		}
	}
}
