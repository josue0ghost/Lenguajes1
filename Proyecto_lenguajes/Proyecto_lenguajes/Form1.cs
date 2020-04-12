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
		bool Analized = false;

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
				File.Text = Data.Instance.fr.Read(path);
				txtFileName.Text = openFileDialog.FileName;
			}
		}

		private void analizarToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Data.Instance.fr = new FileReader();
			Data.Instance.fr.Analize(File.Text);

			if (Data.Instance.fr.Warning != "")
			{
				Warning.Text = Data.Instance.fr.Warning;
			}
			else
			{
				Warning.Text = "Advertencia:";
			}

			if (Data.Instance.fr.Error != "")
			{				
				Error.Text = Data.Instance.fr.Error;
				if (Data.Instance.fr.LineIndexError != 0)
				{
					Error.Text += ". Línea: " + Data.Instance.fr.LineIndexError;
				}				 
			}
			else
			{
				Error.Text = "Error:";
				MessageBox.Show("Formato correcto");
				Analized = true;
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
			if (!Analized)
			{
				MessageBox.Show("No se podrán generar tablas hasta el análisis correcto del archivo");
			}
			else
			{
				Tablas tablas = new Tablas();
				tablas.Show();
			}
		}
	}
}
