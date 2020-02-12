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
			openFileDialog.ShowDialog();
			string path = openFileDialog.FileName;
			File.Text = fr.Read(path);			
		}
	}
}
