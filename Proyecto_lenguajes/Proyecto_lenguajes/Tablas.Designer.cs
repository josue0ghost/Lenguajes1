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
			((System.ComponentModel.ISupportInitialize)(this.FLNGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.FollowGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).BeginInit();
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
			this.FLNGrid.Location = new System.Drawing.Point(12, 12);
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
			this.FollowGrid.Location = new System.Drawing.Point(462, 12);
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
			this.StatesGrid.Location = new System.Drawing.Point(12, 168);
			this.StatesGrid.Name = "StatesGrid";
			this.StatesGrid.Size = new System.Drawing.Size(694, 270);
			this.StatesGrid.TabIndex = 2;
			// 
			// State
			// 
			this.State.HeaderText = "State";
			this.State.Name = "State";
			// 
			// Tablas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(719, 450);
			this.Controls.Add(this.StatesGrid);
			this.Controls.Add(this.FollowGrid);
			this.Controls.Add(this.FLNGrid);
			this.Name = "Tablas";
			this.Text = "Tablas";
			this.Load += new System.EventHandler(this.Tablas_Load);
			((System.ComponentModel.ISupportInitialize)(this.FLNGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.FollowGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.StatesGrid)).EndInit();
			this.ResumeLayout(false);

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
	}
}