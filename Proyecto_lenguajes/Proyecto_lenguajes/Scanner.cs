using System;
using System.Collections.Generic;
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
			foreach (var item in fr.Tokens)
			{
				for (int i = 0; i < item.Value.Count; i++)
				{
					GeneralER.Add(item.Value[i]);
					GeneralER.Add("|");
				}
			}
			GeneralER.RemoveAt(GeneralER.Count - 1); // Eliminar el último "|"
			
			Data.Instance.Tree.CreateTree(GeneralER, "");
		}
	}
}
