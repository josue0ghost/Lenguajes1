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
			
		}

		private void GetFunctions()
		{
			FLN(Data.Instance.Tree.Root);
			Follows();
		}

		private void FLN(Node root)
		{
			if (root == null)
			{
				return;
			}

			FLN(root.Left);
			FLN(root.Right);

			StringBuilder sb = new StringBuilder();
			foreach (var item in root.First)
			{
				sb.Append(item).Append(",");
			}
			string First = sb.Remove(sb.Length-1, 1).ToString();
			sb = new StringBuilder();
			foreach (var item in root.Last)
			{
				sb.Append(item).Append(",");
			}
			string Last = sb.Remove(sb.Length - 1, 1).ToString();

			int n = FLNGrid.Rows.Add();
			FLNGrid.Rows[n].Cells[0].Value = root.Item;
			FLNGrid.Rows[n].Cells[1].Value = First;
			FLNGrid.Rows[n].Cells[2].Value = Last;
			FLNGrid.Rows[n].Cells[3].Value = root.Nullable.ToString();
		}

		private void Follows()
		{
			foreach (var item in Data.Instance.Tree.Follows)
			{
				int n = FollowGrid.Rows.Add();

				StringBuilder sb = new StringBuilder();
				foreach (var item2 in Data.Instance.Tree.Follows[n+1])
				{
					sb.Append(item2).Append(",");
				}
				string Flws = sb.Remove(sb.Length - 1, 1).ToString();

				FollowGrid.Rows[n].Cells[0].Value = n.ToString();
				FollowGrid.Rows[n].Cells[1].Value = Flws;
			}
		}
	}
}
