﻿namespace TextualUserControls
{
	partial class Form3
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
			this.control11 = new TextualUserControls.Control1();
			this.SuspendLayout();
			// 
			// control11
			// 
			this.control11.BackColor = System.Drawing.Color.White;
			this.control11.Location = new System.Drawing.Point(12, 12);
			this.control11.Name = "control11";
			this.control11.Size = new System.Drawing.Size(321, 259);
			this.control11.TabIndex = 0;
			// 
			// Form3
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(424, 283);
			this.Controls.Add(this.control11);
			this.Name = "Form3";
			this.Text = "Form3";
			this.ResumeLayout(false);

		}

		#endregion

		private Control1 control11;

	}
}