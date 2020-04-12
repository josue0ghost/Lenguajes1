using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_lenguajes
{
	public partial class Tablas : Form
	{
		public Tablas()
		{
			InitializeComponent();
		}

		private void Tablas_Load(object sender, EventArgs e)
		{
			GetFunctions();
		}

		private void GetFunctions()
		{
			Data.Instance.Tree.ClaculateFirst_Last_n_Follow();
			FLN(Data.Instance.Tree.Root);
			Follows();
			bool Correct = Data.Instance.Tree.CalculateTransitionsTable(Data.Instance.Tree.Root);
			if (Correct)
			{
				Transitions();
			}			
		}

		private void FLN(Node root)
		{
			if (root == null)
			{
				return;
			}

			FLN(root.Left);
			FLN(root.Right);

			int n = FLNGrid.Rows.Add();
			FLNGrid.Rows[n].Cells[0].Value = root.Item;
			FLNGrid.Rows[n].Cells[1].Value = ListToString(root.First);
			FLNGrid.Rows[n].Cells[2].Value = ListToString(root.Last);
			FLNGrid.Rows[n].Cells[3].Value = root.Nullable.ToString();
		}

		private void Follows()
		{
			foreach (var item in Data.Instance.Tree.Follows)
			{
				int n = FollowGrid.Rows.Add();

				FollowGrid.Rows[n].Cells[0].Value = (n+1).ToString();
				FollowGrid.Rows[n].Cells[1].Value = ListToString(Data.Instance.Tree.Follows[n + 1]);
			}
		}

		private void Transitions()
		{
			for (int j = 0; j < Data.Instance.Tree.states[0].transitions.Length; j++)
			{
				StatesGrid.Columns.Add("Col" + j.ToString(), Data.Instance.Tree.symbols[j]);
			}

			for (int i = 0; i < Data.Instance.Tree.states.Count; i++)
			{				
				int n = StatesGrid.Rows.Add();
				StatesGrid.Rows[n].Cells[0].Value = ListToString(Data.Instance.Tree.states[i].states);

				for (int j = 0; j < Data.Instance.Tree.states[i].transitions.Length; j++)
				{
					StatesGrid.Rows[n].Cells[j + 1].Value = ListToString(Data.Instance.Tree.states[i].transitions[j]);
				}
				
			}
		}

		private string ListToString(List<int> lista)
		{
			if (lista == null)
			{
				return "-";
			}

			string value = "";
			StringBuilder sb = new StringBuilder();
			foreach (var item in lista)
			{
				sb.Append(item).Append(",");
			}

			if (sb.Length != 0)
			{
				value = sb.Remove(sb.Length - 1, 1).ToString();
			}
			
			return value;
		}
	}
}
