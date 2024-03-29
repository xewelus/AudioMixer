﻿namespace AudioMixer
{
	partial class SoundPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.tbFile = new System.Windows.Forms.TextBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.tbVolume = new CommonWinForms.Controls.XwTrackBar();
			this.lblVolume = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.cbRelative = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(4, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Файл:";
			// 
			// tbFile
			// 
			this.tbFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbFile.Location = new System.Drawing.Point(107, 1);
			this.tbFile.Name = "tbFile";
			this.tbFile.Size = new System.Drawing.Size(278, 20);
			this.tbFile.TabIndex = 1;
			this.tbFile.TextChanged += new System.EventHandler(this.tbFile_TextChanged);
			// 
			// btnOpen
			// 
			this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOpen.Image = global::AudioMixer.Properties.Resources.open;
			this.btnOpen.Location = new System.Drawing.Point(382, 0);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(22, 22);
			this.btnOpen.TabIndex = 2;
			this.btnOpen.TabStop = false;
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			this.btnOpen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.controls_KeyDown);
			// 
			// tbVolume
			// 
			this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbVolume.AutoSize = false;
			this.tbVolume.LargeChange = 1;
			this.tbVolume.Location = new System.Drawing.Point(103, 24);
			this.tbVolume.Maximum = 100;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new System.Drawing.Size(327, 18);
			this.tbVolume.TabIndex = 5;
			this.tbVolume.Value = 100;
			this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
			this.tbVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.controls_KeyDown);
			// 
			// lblVolume
			// 
			this.lblVolume.AutoSize = true;
			this.lblVolume.Location = new System.Drawing.Point(5, 24);
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Size = new System.Drawing.Size(65, 13);
			this.lblVolume.TabIndex = 4;
			this.lblVolume.Text = "Громкость:";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Location = new System.Drawing.Point(428, -4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(2, 50);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.FlatAppearance.BorderSize = 0;
			this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDelete.Image = global::AudioMixer.Properties.Resources.close;
			this.btnDelete.Location = new System.Drawing.Point(440, 0);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(20, 20);
			this.btnDelete.TabIndex = 7;
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// cbRelative
			// 
			this.cbRelative.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbRelative.Appearance = System.Windows.Forms.Appearance.Button;
			this.cbRelative.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbRelative.Location = new System.Drawing.Point(402, 0);
			this.cbRelative.Name = "cbRelative";
			this.cbRelative.Size = new System.Drawing.Size(22, 22);
			this.cbRelative.TabIndex = 8;
			this.cbRelative.Text = "/";
			this.toolTip.SetToolTip(this.cbRelative, "Относительный путь.");
			this.cbRelative.UseVisualStyleBackColor = true;
			this.cbRelative.CheckedChanged += new System.EventHandler(this.cbRelative_CheckedChanged);
			// 
			// SoundPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.cbRelative);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.tbVolume);
			this.Controls.Add(this.lblVolume);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.tbFile);
			this.Controls.Add(this.label1);
			this.Name = "SoundPanel";
			this.Size = new System.Drawing.Size(463, 46);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.controls_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbFile;
		private System.Windows.Forms.Button btnOpen;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
		private System.Windows.Forms.Label lblVolume;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.CheckBox cbRelative;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
