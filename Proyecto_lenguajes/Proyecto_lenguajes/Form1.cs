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
	public partial class Form1 : Form
	{
		FileLecture lae = new FileLecture();

		public Form1()
		{
			InitializeComponent();
		}

		private void archivoDeEntradaToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.ShowDialog();
			string path = openFileDialog.FileName;
			lae.Read(path);
		}
	}
}
