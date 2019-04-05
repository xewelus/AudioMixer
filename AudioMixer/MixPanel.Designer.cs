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
			this.tbVolume = new System.Windows.Forms.TrackBar();
			this.lblVolume = new System.Windows.Forms.Label();
			this.pnlSounds = new System.Windows.Forms.Panel();
			this.tsMixes = new System.Windows.Forms.ToolStrip();
			this.btnAdd = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
			this.tsMixes.SuspendLayout();
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
			this.tbName.Location = new System.Drawing.Point(122, 3);
			this.tbName.Name = "tbName";
			this.tbName.Size = new System.Drawing.Size(355, 20);
			this.tbName.TabIndex = 1;
			this.tbName.TextChanged += new System.EventHandler(this.tbName_TextChanged);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.panel1.Controls.Add(this.tbVolume);
			this.panel1.Controls.Add(this.lblVolume);
			this.panel1.Controls.Add(this.tbName);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(487, 55);
			this.panel1.TabIndex = 2;
			// 
			// tbVolume
			// 
			this.tbVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbVolume.AutoSize = false;
			this.tbVolume.LargeChange = 1;
			this.tbVolume.Location = new System.Drawing.Point(118, 25);
			this.tbVolume.Maximum = 100;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new System.Drawing.Size(366, 24);
			this.tbVolume.TabIndex = 3;
			this.tbVolume.Value = 100;
			this.tbVolume.Scroll += new System.EventHandler(this.tbVolume_Scroll);
			// 
			// lblVolume
			// 
			this.lblVolume.AutoSize = true;
			this.lblVolume.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblVolume.Location = new System.Drawing.Point(5, 27);
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Size = new System.Drawing.Size(75, 13);
			this.lblVolume.TabIndex = 2;
			this.lblVolume.Text = "Громкость:";
			// 
			// pnlSounds
			// 
			this.pnlSounds.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnlSounds.Location = new System.Drawing.Point(0, 55);
			this.pnlSounds.Name = "pnlSounds";
			this.pnlSounds.Size = new System.Drawing.Size(487, 0);
			this.pnlSounds.TabIndex = 3;
			// 
			// tsMixes
			// 
			this.tsMixes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMixes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd});
			this.tsMixes.Location = new System.Drawing.Point(0, 55);
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
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// MixPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.tsMixes);
			this.Controls.Add(this.pnlSounds);
			this.Controls.Add(this.panel1);
			this.Name = "MixPanel";
			this.Size = new System.Drawing.Size(487, 95);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
			this.tsMixes.ResumeLayout(false);
			this.tsMixes.PerformLayout();
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
		private System.Windows.Forms.Label lblVolume;
		private System.Windows.Forms.TrackBar tbVolume;
	}
}
