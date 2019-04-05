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
			this.label1 = new System.Windows.Forms.Label();
			this.tbName = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pnlSounds = new System.Windows.Forms.Panel();
			this.tsMixes = new System.Windows.Forms.ToolStrip();
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.label2 = new System.Windows.Forms.Label();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.panel1.SuspendLayout();
			this.tsMixes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(5, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Наименование:";
			// 
			// tbName
			// 
			this.tbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tbName.Location = new System.Drawing.Point(110, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(367, 20);
			this.tbName.TabIndex = 1;
			this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.trackBar1);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.tbName);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(487, 48);
			this.panel1.TabIndex = 2;
			// 
			// pnlSounds
			// 
			this.pnlSounds.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSounds.Location = new System.Drawing.Point(0, 48);
			this.pnlSounds.Name = "pnlSounds";
			this.pnlSounds.Size = new System.Drawing.Size(487, 10);
			this.pnlSounds.TabIndex = 3;
			// 
			// tsMixes
			// 
			this.tsMixes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMixes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd});
			this.tsMixes.Location = new System.Drawing.Point(0, 58);
			this.tsMixes.Name = "tsMixes";
			this.tsMixes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.tsMixes.ShowItemToolTips = false;
			this.tsMixes.Size = new System.Drawing.Size(487, 25);
			this.tsMixes.TabIndex = 4;
			// 
			// btnAdd
			// 
			this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAdd.Image = global::AudioMixer.Properties.Resources.plus;
			this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(23, 22);
			this.btnAdd.Text = "toolStripButton1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 27);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Громкость:";
			// 
			// trackBar1
			// 
			this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackBar1.AutoSize = false;
			this.trackBar1.Location = new System.Drawing.Point(106, 25);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(378, 18);
			this.trackBar1.TabIndex = 3;
			this.trackBar1.TickFrequency = 2;
			this.trackBar1.Value = 100;
			// 
			// MixPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tsMixes);
			this.Controls.Add(this.pnlSounds);
			this.Controls.Add(this.panel1);
			this.Name = "MixPanel";
			this.Size = new System.Drawing.Size(487, 155);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tsMixes.ResumeLayout(false);
			this.tsMixes.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbName;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pnlSounds;
		private System.Windows.Forms.ToolStrip tsMixes;
		private System.Windows.Forms.ToolStripButton btnAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar trackBar1;
	}
}
