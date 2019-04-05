namespace AudioMixer
{
	partial class MainForm
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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.tsMixes = new System.Windows.Forms.ToolStrip();
			this.lvMixes = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.btnMixDelete = new System.Windows.Forms.ToolStripButton();
			this.btnMixAdd = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbAudioDevice = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.tsMixes.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 32);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.lvMixes);
			this.splitContainer.Panel1.Controls.Add(this.tsMixes);
			this.splitContainer.Size = new System.Drawing.Size(1291, 731);
			this.splitContainer.SplitterDistance = 261;
			this.splitContainer.TabIndex = 0;
			// 
			// tsMixes
			// 
			this.tsMixes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMixes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMixDelete,
            this.btnMixAdd});
			this.tsMixes.Location = new System.Drawing.Point(0, 0);
			this.tsMixes.Name = "tsMixes";
			this.tsMixes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.tsMixes.ShowItemToolTips = false;
			this.tsMixes.Size = new System.Drawing.Size(261, 25);
			this.tsMixes.TabIndex = 0;
			// 
			// lvMixes
			// 
			this.lvMixes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lvMixes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMixes.FullRowSelect = true;
			this.lvMixes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvMixes.Location = new System.Drawing.Point(0, 25);
			this.lvMixes.Name = "lvMixes";
			this.lvMixes.Size = new System.Drawing.Size(261, 706);
			this.lvMixes.TabIndex = 1;
			this.lvMixes.UseCompatibleStateImageBehavior = false;
			this.lvMixes.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 26;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 224;
			// 
			// btnMixDelete
			// 
			this.btnMixDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnMixDelete.Enabled = false;
			this.btnMixDelete.Image = global::AudioMixer.Properties.Resources.minus;
			this.btnMixDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMixDelete.Name = "btnMixDelete";
			this.btnMixDelete.Size = new System.Drawing.Size(23, 22);
			this.btnMixDelete.Text = "toolStripButton2";
			// 
			// btnMixAdd
			// 
			this.btnMixAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnMixAdd.Image = global::AudioMixer.Properties.Resources.plus;
			this.btnMixAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMixAdd.Name = "btnMixAdd";
			this.btnMixAdd.Size = new System.Drawing.Size(23, 22);
			this.btnMixAdd.Text = "toolStripButton1";
			this.btnMixAdd.Click += new System.EventHandler(this.btnMixAdd_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbAudioDevice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1291, 32);
			this.panel1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Аудио-устройство:";
			// 
			// cbAudioDevice
			// 
			this.cbAudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAudioDevice.FormattingEnabled = true;
			this.cbAudioDevice.Location = new System.Drawing.Point(117, 6);
			this.cbAudioDevice.Name = "cbAudioDevice";
			this.cbAudioDevice.Size = new System.Drawing.Size(379, 21);
			this.cbAudioDevice.TabIndex = 1;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1291, 763);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.panel1);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.Text = "AudioMixer";
			this.splitContainer.Panel1.ResumeLayout(false);
			this.splitContainer.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.tsMixes.ResumeLayout(false);
			this.tsMixes.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.ToolStrip tsMixes;
		private System.Windows.Forms.ToolStripButton btnMixAdd;
		private System.Windows.Forms.ToolStripButton btnMixDelete;
		private System.Windows.Forms.ListView lvMixes;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbAudioDevice;
	}
}

