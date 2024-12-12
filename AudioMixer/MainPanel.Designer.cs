namespace AudioMixer
{
	sealed partial class MainPanel
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
			this.pnlMixes = new AudioMixer.MixesListPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblVolume = new System.Windows.Forms.Label();
			this.tbVolume = new CommonWinForms.Controls.XwTrackBar();
			this.btnSave = new System.Windows.Forms.Button();
			this.cbAudioDevice = new AudioDeviceComboBox();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 61);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.pnlMixes);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.AutoScroll = true;
			this.splitContainer.Size = new System.Drawing.Size(655, 422);
			this.splitContainer.SplitterDistance = 217;
			this.splitContainer.TabIndex = 0;
			this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
			// 
			// pnlMixes
			// 
			this.pnlMixes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMixes.Location = new System.Drawing.Point(0, 0);
			this.pnlMixes.Name = "pnlMixes";
			this.pnlMixes.Size = new System.Drawing.Size(217, 422);
			this.pnlMixes.TabIndex = 0;
			this.pnlMixes.ItemSelected += new System.EventHandler(this.pnlMixes_ItemSelected);
			this.pnlMixes.ItemActivated += new System.EventHandler(this.pnlMixes_ItemActivated);
			this.pnlMixes.DockButtonClick += new System.EventHandler(this.pnlMixes_DockButtonClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblVolume);
			this.panel1.Controls.Add(this.tbVolume);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.cbAudioDevice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(655, 61);
			this.panel1.TabIndex = 1;
			// 
			// lblVolume
			// 
			this.lblVolume.AutoSize = true;
			this.lblVolume.Location = new System.Drawing.Point(9, 32);
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Size = new System.Drawing.Size(65, 13);
			this.lblVolume.TabIndex = 5;
			this.lblVolume.Text = "Volume:";
			// 
			// tbVolume
			// 
			this.tbVolume.AutoSize = false;
			this.tbVolume.LargeChange = 1;
			this.tbVolume.Location = new System.Drawing.Point(111, 28);
			this.tbVolume.Maximum = 100;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new System.Drawing.Size(366, 24);
			this.tbVolume.TabIndex = 4;
			this.tbVolume.Value = 100;
			this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
			this.tbVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbVolume_KeyDown);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Enabled = false;
			this.btnSave.Image = global::AudioMixer.Properties.Resources.save;
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(629, 4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(23, 23);
			this.btnSave.TabIndex = 2;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// cbAudioDevice
			// 
			this.cbAudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAudioDevice.FormattingEnabled = true;
			this.cbAudioDevice.Location = new System.Drawing.Point(117, 6);
			this.cbAudioDevice.Name = "cbAudioDevice";
			this.cbAudioDevice.Size = new System.Drawing.Size(354, 21);
			this.cbAudioDevice.TabIndex = 1;
			this.cbAudioDevice.SelectedIndexChanged += new System.EventHandler(this.cbAudioDevice_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Audio device:";
			// 
			// MainPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.panel1);
			this.Name = "MainPanel";
			this.Size = new System.Drawing.Size(655, 483);
			this.splitContainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private AudioDeviceComboBox cbAudioDevice;
		private MixesListPanel pnlMixes;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblVolume;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
	}
}

