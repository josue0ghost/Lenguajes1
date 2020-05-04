﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_lenguajes
{
	public partial class Tablas : Form
	{
		Scanner sc = new Scanner();

		public Tablas()
		{
			InitializeComponent();
		}

		private void Tablas_Load(object sender, EventArgs e)
		{
			if (Data.Instance.fr.Tokens.Count() > 0)
			{
				GetFunctions();
				for (int i = 0; i < Data.Instance.fr.Trees.Count; i++)
				{
					GetFunctions(Data.Instance.fr.Trees.Values.ElementAt(i));
				}
			}
			else
			{
				MessageBox.Show("No se pueden generar tablas de un archivo con formato incorrecto");
			}
		}

		private void GetFunctions()
		{			
			sc.GenerateExpressionTree(Data.Instance.fr);

			Data.Instance.Tree.ClaculateFirst_Last_n_Follow();
			FLN(Data.Instance.Tree.Root);
			Follows(Data.Instance.Tree);
			bool Correct = Data.Instance.Tree.CalculateTransitionsTable(Data.Instance.Tree.Root);
			if (Correct)
			{
				Transitions(Data.Instance.Tree);
			}			
		}

		private void GetFunctions(ExpressionTree Tree)
		{
			Tree.ClaculateFirst_Last_n_Follow();
			Tree.CalculateTransitionsTable(Tree.Root);
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

		private void Follows(ExpressionTree Tree)
		{
			foreach (var item in Tree.Follows)
			{
				int n = FollowGrid.Rows.Add();

				FollowGrid.Rows[n].Cells[0].Value = (n+1).ToString();
				FollowGrid.Rows[n].Cells[1].Value = ListToString(Tree.Follows[n + 1]);
			}
		}

		private void Transitions(ExpressionTree Tree)
		{
			for (int j = 0; j < Tree.symbols.Count; j++)
			{
				StatesGrid.Columns.Add("Col" + j.ToString(), Tree.symbols[j]);
			}


			for (int i = 0; i < Tree.states.Count; i++)
			{
				if (Tree.states[i].states.Count != 0)
				{
					int n = StatesGrid.Rows.Add();
					StatesGrid.Rows[n].Cells[0].Value = ListToString(Tree.states[i].states);

					for (int j = 0; j < Tree.states[i].transitions.Length; j++)
					{
						StatesGrid.Rows[n].Cells[j + 1].Value = ListToString(Tree.states[i].transitions[j]);
					}
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

		private void generarScanerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "C# files (*.cs)|*.cs";

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{				
				string output = sc.GenerateScanner();

				using (FileStream fs = (FileStream)saveFileDialog.OpenFile())
				{
					byte[] buffer = Encoding.UTF8.GetBytes(output);
					fs.Write(buffer, 0, buffer.Length);
				}
			}
		}
	}
}
