using System;
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
	public partial class Analizador : Form
	{
		FileReader fr = new FileReader();

		public Analizador()
		{
			InitializeComponent();
		}

		private void archivoDeEntradaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "txt files (*.txt)|*.txt";

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string path = openFileDialog.FileName;
				File.Text = fr.Read(path);
				txtFileName.Text = openFileDialog.FileName;
			}
		}

		private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			fr = new FileReader();
			fr.Analize(File.Text);

			if (fr.Warning != "")
			{
				Warning.Text = fr.Warning;
			}
			else
			{
				Warning.Text = "Advertencia:";
			}

			if (fr.Error != "")
			{				
				Error.Text = fr.Error;
				if (fr.LineIndexError != 0)
				{
					Error.Text += ". Línea: " + fr.LineIndexError;
				}				 
			}
			else
			{
				Error.Text = "Error:";
				MessageBox.Show("Formato correcto");

				Scanner sc = new Scanner();
				sc.GenerateExpressionTree(fr);
			}
		}

		private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "txt files (*.txt)|*.txt";			

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile());

				writer.WriteLine(File.Text);
				writer.Dispose();
				writer.Close();
			}
		}

		private void verTablasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Tablas tablas = new Tablas();
			tablas.Show();
		}
	}
}
