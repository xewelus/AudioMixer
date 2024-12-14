namespace AudioMixer
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
			components = new System.ComponentModel.Container();
			label1 = new Label();
			tbFile = new TextBox();
			btnOpen = new Button();
			tbVolume = new CommonWinForms.Controls.XwTrackBar();
			lblVolume = new Label();
			groupBox1 = new GroupBox();
			btnDelete = new Button();
			cbRelative = new CheckBox();
			toolTip = new ToolTip(components);
			cbBoost = new CheckBox();
			((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(5, 5);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(28, 15);
			label1.TabIndex = 0;
			label1.Text = "File:";
			// 
			// tbFile
			// 
			tbFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tbFile.Location = new Point(125, 1);
			tbFile.Margin = new Padding(4, 3, 4, 3);
			tbFile.Name = "tbFile";
			tbFile.Size = new Size(303, 23);
			tbFile.TabIndex = 1;
			tbFile.TextChanged += tbFile_TextChanged;
			// 
			// btnOpen
			// 
			btnOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnOpen.Image = Properties.Resources.open;
			btnOpen.Location = new Point(428, 0);
			btnOpen.Margin = new Padding(4, 3, 4, 3);
			btnOpen.Name = "btnOpen";
			btnOpen.Size = new Size(26, 25);
			btnOpen.TabIndex = 2;
			btnOpen.TabStop = false;
			btnOpen.UseVisualStyleBackColor = true;
			btnOpen.Click += btnOpen_Click;
			btnOpen.KeyDown += controls_KeyDown;
			// 
			// tbVolume
			// 
			tbVolume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tbVolume.AutoSize = false;
			tbVolume.LargeChange = 1;
			tbVolume.Location = new Point(120, 28);
			tbVolume.Margin = new Padding(4, 3, 4, 3);
			tbVolume.Maximum = 100;
			tbVolume.Name = "tbVolume";
			tbVolume.Size = new Size(382, 21);
			tbVolume.TabIndex = 5;
			tbVolume.Value = 100;
			tbVolume.ValueChanged += tbVolume_ValueChanged;
			tbVolume.KeyDown += controls_KeyDown;
			// 
			// lblVolume
			// 
			lblVolume.AutoSize = true;
			lblVolume.Location = new Point(6, 28);
			lblVolume.Margin = new Padding(4, 0, 4, 0);
			lblVolume.Name = "lblVolume";
			lblVolume.Size = new Size(50, 15);
			lblVolume.TabIndex = 4;
			lblVolume.Text = "Volume:";
			// 
			// groupBox1
			// 
			groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
			groupBox1.Location = new Point(499, -5);
			groupBox1.Margin = new Padding(4, 3, 4, 3);
			groupBox1.Name = "groupBox1";
			groupBox1.Padding = new Padding(4, 3, 4, 3);
			groupBox1.Size = new Size(2, 58);
			groupBox1.TabIndex = 6;
			groupBox1.TabStop = false;
			groupBox1.Text = "groupBox1";
			// 
			// btnDelete
			// 
			btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Image = Properties.Resources.close;
			btnDelete.Location = new Point(513, 0);
			btnDelete.Margin = new Padding(4, 3, 4, 3);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(23, 23);
			btnDelete.TabIndex = 7;
			btnDelete.UseVisualStyleBackColor = true;
			btnDelete.Click += btnDelete_Click;
			// 
			// cbRelative
			// 
			cbRelative.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			cbRelative.Appearance = Appearance.Button;
			cbRelative.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			cbRelative.Location = new Point(451, 0);
			cbRelative.Margin = new Padding(4, 3, 4, 3);
			cbRelative.Name = "cbRelative";
			cbRelative.Size = new Size(26, 25);
			cbRelative.TabIndex = 8;
			cbRelative.Text = "/";
			toolTip.SetToolTip(cbRelative, "Relative path.");
			cbRelative.UseVisualStyleBackColor = true;
			cbRelative.CheckedChanged += cbRelative_CheckedChanged;
			// 
			// cbBoost
			// 
			cbBoost.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			cbBoost.Appearance = Appearance.Button;
			cbBoost.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			cbBoost.Location = new Point(475, 0);
			cbBoost.Margin = new Padding(4, 3, 4, 3);
			cbBoost.Name = "cbBoost";
			cbBoost.Size = new Size(26, 25);
			cbBoost.TabIndex = 9;
			cbBoost.Text = "⏫";
			toolTip.SetToolTip(cbBoost, "Boost volume.");
			cbBoost.UseVisualStyleBackColor = true;
			cbBoost.CheckedChanged += cbBoost_CheckedChanged;
			// 
			// SoundPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(cbBoost);
			Controls.Add(cbRelative);
			Controls.Add(btnDelete);
			Controls.Add(groupBox1);
			Controls.Add(tbVolume);
			Controls.Add(lblVolume);
			Controls.Add(btnOpen);
			Controls.Add(tbFile);
			Controls.Add(label1);
			Margin = new Padding(4, 3, 4, 3);
			Name = "SoundPanel";
			Size = new Size(540, 53);
			KeyDown += controls_KeyDown;
			((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
			ResumeLayout(false);
			PerformLayout();
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
		private CheckBox cbBoost;
	}
}
