﻿namespace AircraftPlantView
{
	partial class FormClients
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
			this.dataGridViewClients = new System.Windows.Forms.DataGridView();
			this.buttonDel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewClients
			// 
			this.dataGridViewClients.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
			this.dataGridViewClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewClients.Location = new System.Drawing.Point(6, 8);
			this.dataGridViewClients.Name = "dataGridViewClients";
			this.dataGridViewClients.RowHeadersWidth = 51;
			this.dataGridViewClients.RowTemplate.Height = 29;
			this.dataGridViewClients.Size = new System.Drawing.Size(544, 316);
			this.dataGridViewClients.TabIndex = 0;
			// 
			// buttonDel
			// 
			this.buttonDel.Location = new System.Drawing.Point(597, 30);
			this.buttonDel.Name = "buttonDel";
			this.buttonDel.Size = new System.Drawing.Size(94, 29);
			this.buttonDel.TabIndex = 1;
			this.buttonDel.Text = "Удалить";
			this.buttonDel.UseVisualStyleBackColor = true;
			this.buttonDel.Click += new System.EventHandler(this.buttonDel_Click);
			// 
			// FormClients
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(766, 345);
			this.Controls.Add(this.buttonDel);
			this.Controls.Add(this.dataGridViewClients);
			this.Name = "FormClients";
			this.Text = "Клиенты";
			this.Load += new System.EventHandler(this.FormClients_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewClients)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewClients;
		private System.Windows.Forms.Button buttonDel;
	}
}