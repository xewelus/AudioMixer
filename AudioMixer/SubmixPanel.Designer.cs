namespace AudioMixer
{
	partial class SubmixPanel
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
			cbMix = new ComboBox();
			tbVolume = new CommonWinForms.Controls.XwTrackBar();
			lblVolume = new Label();
			groupBox1 = new GroupBox();
			btnDelete = new Button();
			toolTip = new ToolTip(components);
			((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(5, 5);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(30, 15);
			label1.TabIndex = 0;
			label1.Text = "Mix:";
			// 
			// cbMix
			// 
			cbMix.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			cbMix.DropDownStyle = ComboBoxStyle.DropDownList;
			cbMix.Location = new Point(125, 1);
			cbMix.Margin = new Padding(4, 3, 4, 3);
			cbMix.Name = "cbMix";
			cbMix.Size = new Size(366, 23);
			cbMix.TabIndex = 1;
			cbMix.SelectedIndexChanged += cbMix_SelectedIndexChanged;
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
			// SubmixPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btnDelete);
			Controls.Add(groupBox1);
			Controls.Add(tbVolume);
			Controls.Add(lblVolume);
			Controls.Add(cbMix);
			Controls.Add(label1);
			Margin = new Padding(4, 3, 4, 3);
			Name = "SubmixPanel";
			Size = new Size(540, 53);
			KeyDown += controls_KeyDown;
			((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbMix;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
		private System.Windows.Forms.Label lblVolume;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.ToolTip toolTip;
	}
}
