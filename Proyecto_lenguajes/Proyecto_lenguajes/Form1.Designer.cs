namespace Proyecto_lenguajes
{
	partial class Analizador
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.archivoDeEntradaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.analizarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.File = new System.Windows.Forms.RichTextBox();
			this.Error = new System.Windows.Forms.Label();
			this.Warning = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.verTablasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.analizarToolStripMenuItem,
            this.verTablasToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(588, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archivoToolStripMenuItem
			// 
			this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem});
			this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
			this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.archivoToolStripMenuItem.Text = "Archivo";
			// 
			// abrirToolStripMenuItem
			// 
			this.abrirToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoDeEntradaToolStripMenuItem});
			this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
			this.abrirToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.abrirToolStripMenuItem.Text = "Abrir";
			// 
			// archivoDeEntradaToolStripMenuItem
			// 
			this.archivoDeEntradaToolStripMenuItem.Name = "archivoDeEntradaToolStripMenuItem";
			this.archivoDeEntradaToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.archivoDeEntradaToolStripMenuItem.Text = "Archivo de entrada";
			this.archivoDeEntradaToolStripMenuItem.Click += new System.EventHandler(this.archivoDeEntradaToolStripMenuItem_Click);
			// 
			// guardarToolStripMenuItem
			// 
			this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
			this.guardarToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
			this.guardarToolStripMenuItem.Text = "Guardar como";
			this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
			// 
			// analizarToolStripMenuItem
			// 
			this.analizarToolStripMenuItem.Name = "analizarToolStripMenuItem";
			this.analizarToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.analizarToolStripMenuItem.Text = "Analizar";
			this.analizarToolStripMenuItem.Click += new System.EventHandler(this.analizarToolStripMenuItem_Click);
			// 
			// File
			// 
			this.File.Location = new System.Drawing.Point(23, 51);
			this.File.Name = "File";
			this.File.Size = new System.Drawing.Size(539, 354);
			this.File.TabIndex = 1;
			this.File.Text = "";
			// 
			// Error
			// 
			this.Error.AutoSize = true;
			this.Error.ForeColor = System.Drawing.Color.Red;
			this.Error.Location = new System.Drawing.Point(20, 408);
			this.Error.Name = "Error";
			this.Error.Size = new System.Drawing.Size(32, 13);
			this.Error.TabIndex = 2;
			this.Error.Text = "Error:";
			// 
			// Warning
			// 
			this.Warning.AutoSize = true;
			this.Warning.ForeColor = System.Drawing.Color.Orange;
			this.Warning.Location = new System.Drawing.Point(20, 429);
			this.Warning.Name = "Warning";
			this.Warning.Size = new System.Drawing.Size(67, 13);
			this.Warning.TabIndex = 3;
			this.Warning.Text = "Advertencia:";
			// 
			// txtFileName
			// 
			this.txtFileName.Location = new System.Drawing.Point(23, 25);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.ReadOnly = true;
			this.txtFileName.Size = new System.Drawing.Size(539, 20);
			this.txtFileName.TabIndex = 4;
			// 
			// verTablasToolStripMenuItem
			// 
			this.verTablasToolStripMenuItem.Name = "verTablasToolStripMenuItem";
			this.verTablasToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			this.verTablasToolStripMenuItem.Text = "Ver tablas";
			// 
			// Analizador
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(588, 450);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.Warning);
			this.Controls.Add(this.Error);
			this.Controls.Add(this.File);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Analizador";
			this.Text = "Scanner";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem archivoDeEntradaToolStripMenuItem;
		private System.Windows.Forms.RichTextBox File;
		private System.Windows.Forms.ToolStripMenuItem analizarToolStripMenuItem;
		private System.Windows.Forms.Label Error;
		private System.Windows.Forms.Label Warning;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem verTablasToolStripMenuItem;
	}
}

