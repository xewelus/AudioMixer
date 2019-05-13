namespace AudioMixer
{
	sealed partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.pnlMixes = new AudioMixer.MixesListPanel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.cbAudioDevice = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmSysTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miSysTrayPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miSysTrayQuit = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.cmSysTray.SuspendLayout();
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
			this.splitContainer.Panel1.Controls.Add(this.pnlMixes);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.AutoScroll = true;
			this.splitContainer.Size = new System.Drawing.Size(655, 451);
			this.splitContainer.SplitterDistance = 217;
			this.splitContainer.TabIndex = 0;
			this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
			// 
			// pnlMixes
			// 
			this.pnlMixes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMixes.Location = new System.Drawing.Point(0, 0);
			this.pnlMixes.Name = "pnlMixes";
			this.pnlMixes.Size = new System.Drawing.Size(217, 451);
			this.pnlMixes.TabIndex = 0;
			this.pnlMixes.ItemSelected += new System.EventHandler(this.pnlMixes_ItemSelected);
			this.pnlMixes.ItemActivated += new System.EventHandler(this.pnlMixes_ItemActivated);
			this.pnlMixes.DockButtonClick += new System.EventHandler(this.pnlMixes_DockButtonClick);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.cbAudioDevice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(655, 32);
			this.panel1.TabIndex = 1;
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
			this.label1.Text = "Аудио-устройство:";
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.cmSysTray;
			this.notifyIcon.Text = "AudioMixer";
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// cmSysTray
			// 
			this.cmSysTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSysTrayPlay,
            this.miSysTrayStop,
            this.toolStripSeparator1,
            this.miSysTrayQuit});
			this.cmSysTray.Name = "cmSysTray";
			this.cmSysTray.Size = new System.Drawing.Size(172, 76);
			this.cmSysTray.Opening += new System.ComponentModel.CancelEventHandler(this.cmSysTray_Opening);
			// 
			// miSysTrayPlay
			// 
			this.miSysTrayPlay.Image = global::AudioMixer.Properties.Resources.play;
			this.miSysTrayPlay.Name = "miSysTrayPlay";
			this.miSysTrayPlay.Size = new System.Drawing.Size(171, 22);
			this.miSysTrayPlay.Text = "Воспроизведение";
			this.miSysTrayPlay.Click += new System.EventHandler(this.miSysTrayPlay_Click);
			// 
			// miSysTrayStop
			// 
			this.miSysTrayStop.Image = global::AudioMixer.Properties.Resources.stop;
			this.miSysTrayStop.Name = "miSysTrayStop";
			this.miSysTrayStop.Size = new System.Drawing.Size(171, 22);
			this.miSysTrayStop.Text = "Отключить";
			this.miSysTrayStop.Click += new System.EventHandler(this.miSysTrayStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
			// 
			// miSysTrayQuit
			// 
			this.miSysTrayQuit.Name = "miSysTrayQuit";
			this.miSysTrayQuit.Size = new System.Drawing.Size(171, 22);
			this.miSysTrayQuit.Text = "Выход";
			this.miSysTrayQuit.Click += new System.EventHandler(this.miSysTrayQuit_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 483);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.panel1);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AudioMixer";
			this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
			this.splitContainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.cmSysTray.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbAudioDevice;
		private MixesListPanel pnlMixes;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip cmSysTray;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayPlay;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayQuit;
	}
}

