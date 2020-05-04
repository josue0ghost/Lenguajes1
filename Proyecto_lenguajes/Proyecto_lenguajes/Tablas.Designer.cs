namespace Proyecto_lenguajes
{
	partial class Tablas
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.FLNGrid = new System.Windows.Forms.DataGridView();
			this.Symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.First = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Last = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Nullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FollowGrid = new System.Windows.Forms.DataGridView();
			this.SymbolF = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Follow = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.StatesGrid = new System.Windows.Forms.DataGridView();
			this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.generarScanerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.FLNGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FollowGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// FLNGrid
			// 
			this.FLNGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FLNGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Symbol,
            this.First,
            this.Last,
            this.Nullable});
			this.FLNGrid.Location = new System.Drawing.Point(12, 27);
			this.FLNGrid.Name = "FLNGrid";
			this.FLNGrid.Size = new System.Drawing.Size(444, 150);
			this.FLNGrid.TabIndex = 0;
			// 
			// Symbol
			// 
			this.Symbol.HeaderText = "Symbol";
			this.Symbol.Name = "Symbol";
			// 
			// First
			// 
			this.First.HeaderText = "First";
			this.First.Name = "First";
			// 
			// Last
			// 
			this.Last.HeaderText = "Last";
			this.Last.Name = "Last";
			// 
			// Nullable
			// 
			this.Nullable.HeaderText = "Nullable";
			this.Nullable.Name = "Nullable";
			// 
			// FollowGrid
			// 
			this.FollowGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FollowGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SymbolF,
            this.Follow});
			this.FollowGrid.Location = new System.Drawing.Point(462, 27);
			this.FollowGrid.Name = "FollowGrid";
			this.FollowGrid.Size = new System.Drawing.Size(244, 150);
			this.FollowGrid.TabIndex = 1;
			// 
			// SymbolF
			// 
			this.SymbolF.HeaderText = "Symbol";
			this.SymbolF.Name = "SymbolF";
			// 
			// Follow
			// 
			this.Follow.HeaderText = "Follow";
			this.Follow.Name = "Follow";
			// 
			// StatesGrid
			// 
			this.StatesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.StatesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.State});
			this.StatesGrid.Location = new System.Drawing.Point(12, 183);
			this.StatesGrid.Name = "StatesGrid";
			this.StatesGrid.Size = new System.Drawing.Size(694, 270);
			this.StatesGrid.TabIndex = 2;
			// 
			// State
			// 
			this.State.HeaderText = "State";
			this.State.Name = "State";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(719, 24);
			this.menuStrip1.TabIndex = 3;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// archivoToolStripMenuItem
			// 
			this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarScanerToolStripMenuItem});
			this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
			this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.archivoToolStripMenuItem.Text = "Archivo";
			// 
			// generarScanerToolStripMenuItem
			// 
			this.generarScanerToolStripMenuItem.Name = "generarScanerToolStripMenuItem";
			this.generarScanerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.generarScanerToolStripMenuItem.Text = "Generar scaner";
			this.generarScanerToolStripMenuItem.Click += new System.EventHandler(this.generarScanerToolStripMenuItem_Click);
			// 
			// Tablas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(719, 462);
			this.Controls.Add(this.StatesGrid);
			this.Controls.Add(this.FollowGrid);
			this.Controls.Add(this.FLNGrid);
			this.Controls.Add(this.menuStrip1);
			this.Name = "Tablas";
			this.Text = "Tablas";
			this.Load += new System.EventHandler(this.Tablas_Load);
			((System.ComponentModel.ISupportInitialize)(this.FLNGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FollowGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView FLNGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn Symbol;
		private System.Windows.Forms.DataGridViewTextBoxColumn First;
		private System.Windows.Forms.DataGridViewTextBoxColumn Last;
		private System.Windows.Forms.DataGridViewTextBoxColumn Nullable;
		private System.Windows.Forms.DataGridView FollowGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn SymbolF;
		private System.Windows.Forms.DataGridViewTextBoxColumn Follow;
		private System.Windows.Forms.DataGridView StatesGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn State;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem generarScanerToolStripMenuItem;
	}
}