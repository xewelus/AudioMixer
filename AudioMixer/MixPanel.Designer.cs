namespace AudioMixer
{
	partial class MixPanel
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
			label1 = new Label();
			tbName = new TextBox();
			panel1 = new Panel();
			tbVolume = new CommonWinForms.Controls.XwTrackBar();
			lblVolume = new Label();
			pnlSounds = new Panel();
			tsMixes = new ToolStrip();
			btnAdd = new ToolStripButton();
			btnAddMix = new ToolStripButton();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
			tsMixes.SuspendLayout();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			label1.Location = new Point(6, 7);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(43, 13);
			label1.TabIndex = 0;
			label1.Text = "Name:";
			// 
			// tbName
			// 
			tbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tbName.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			tbName.Location = new Point(142, 3);
			tbName.Margin = new Padding(4, 3, 4, 3);
			tbName.Name = "tbName";
			tbName.Size = new Size(414, 20);
			tbName.TabIndex = 1;
			tbName.TextChanged += tbName_TextChanged;
			// 
			// panel1
			// 
			panel1.BackColor = Color.Silver;
			panel1.Controls.Add(tbVolume);
			panel1.Controls.Add(lblVolume);
			panel1.Controls.Add(tbName);
			panel1.Controls.Add(label1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(4, 3, 4, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(568, 63);
			panel1.TabIndex = 2;
			// 
			// tbVolume
			// 
			tbVolume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			tbVolume.AutoSize = false;
			tbVolume.LargeChange = 1;
			tbVolume.Location = new Point(138, 29);
			tbVolume.Margin = new Padding(4, 3, 4, 3);
			tbVolume.Maximum = 100;
			tbVolume.Name = "tbVolume";
			tbVolume.Size = new Size(427, 28);
			tbVolume.TabIndex = 3;
			tbVolume.Value = 100;
			tbVolume.ValueChanged += tbVolume_ValueChanged;
			tbVolume.KeyDown += controls_KeyDown;
			// 
			// lblVolume
			// 
			lblVolume.AutoSize = true;
			lblVolume.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
			lblVolume.Location = new Point(6, 31);
			lblVolume.Margin = new Padding(4, 0, 4, 0);
			lblVolume.Name = "lblVolume";
			lblVolume.Size = new Size(52, 13);
			lblVolume.TabIndex = 2;
			lblVolume.Text = "Volume:";
			// 
			// pnlSounds
			// 
			pnlSounds.Dock = DockStyle.Top;
			pnlSounds.Location = new Point(0, 63);
			pnlSounds.Margin = new Padding(4, 3, 4, 3);
			pnlSounds.Name = "pnlSounds";
			pnlSounds.Size = new Size(568, 0);
			pnlSounds.TabIndex = 3;
			// 
			// tsMixes
			// 
			tsMixes.GripStyle = ToolStripGripStyle.Hidden;
			tsMixes.Items.AddRange(new ToolStripItem[] { btnAdd, btnAddMix });
			tsMixes.Location = new Point(0, 63);
			tsMixes.Name = "tsMixes";
			tsMixes.RightToLeft = RightToLeft.Yes;
			tsMixes.ShowItemToolTips = false;
			tsMixes.Size = new Size(568, 25);
			tsMixes.TabIndex = 4;
			tsMixes.KeyDown += controls_KeyDown;
			// 
			// btnAdd
			// 
			btnAdd.Image = Properties.Resources.plus;
			btnAdd.ImageTransparentColor = Color.Magenta;
			btnAdd.Name = "btnAdd";
			btnAdd.Size = new Size(45, 22);
			btnAdd.Text = "File";
			btnAdd.TextImageRelation = TextImageRelation.TextBeforeImage;
			btnAdd.Click += btnAdd_Click;
			// 
			// btnAddMix
			// 
			btnAddMix.Image = Properties.Resources.plus;
			btnAddMix.ImageTransparentColor = Color.Magenta;
			btnAddMix.Name = "btnAddMix";
			btnAddMix.Size = new Size(47, 22);
			btnAddMix.Text = "Mix";
			btnAddMix.TextImageRelation = TextImageRelation.TextBeforeImage;
			btnAddMix.Click += btnAddMix_Click;
			// 
			// MixPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			AutoScroll = true;
			Controls.Add(tsMixes);
			Controls.Add(pnlSounds);
			Controls.Add(panel1);
			Margin = new Padding(4, 3, 4, 3);
			Name = "MixPanel";
			Size = new Size(568, 110);
			KeyDown += controls_KeyDown;
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
			tsMixes.ResumeLayout(false);
			tsMixes.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlSounds;
		private System.Windows.Forms.ToolStrip tsMixes;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.Label lblVolume;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
		private ToolStripButton btnAddMix;
	}
}
