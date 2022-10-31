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
			this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.cmSysTray = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miOpenForm = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miSysTrayPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miSysTrayQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.clickDelayTimer = new System.Windows.Forms.Timer(this.components);
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.miSysTrayVolume100 = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayVolume25 = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayVolume50 = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayVolume75 = new System.Windows.Forms.ToolStripMenuItem();
			this.cmSysTray.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIcon
			// 
			this.notifyIcon.ContextMenuStrip = this.cmSysTray;
			this.notifyIcon.Text = "AudioMixer";
			this.notifyIcon.Visible = true;
			this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
			this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
			// 
			// cmSysTray
			// 
			this.cmSysTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenForm,
            this.toolStripSeparator2,
            this.miSysTrayPlay,
            this.miSysTrayStop,
            this.toolStripSeparator3,
            this.miSysTrayVolume25,
            this.miSysTrayVolume50,
            this.miSysTrayVolume75,
            this.miSysTrayVolume100,
            this.toolStripSeparator1,
            this.miSysTrayQuit});
			this.cmSysTray.Name = "cmSysTray";
			this.cmSysTray.Size = new System.Drawing.Size(181, 220);
			this.cmSysTray.Opening += new System.ComponentModel.CancelEventHandler(this.cmSysTray_Opening);
			// 
			// miOpenForm
			// 
			this.miOpenForm.Image = global::AudioMixer.Properties.Resources.app_png;
			this.miOpenForm.Name = "miOpenForm";
			this.miOpenForm.Size = new System.Drawing.Size(180, 22);
			this.miOpenForm.Text = "Открыть";
			this.miOpenForm.Click += new System.EventHandler(this.miOpenForm_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// miSysTrayPlay
			// 
			this.miSysTrayPlay.Image = global::AudioMixer.Properties.Resources.play;
			this.miSysTrayPlay.Name = "miSysTrayPlay";
			this.miSysTrayPlay.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayPlay.Text = "Воспроизвести";
			this.miSysTrayPlay.Click += new System.EventHandler(this.miSysTrayPlay_Click);
			// 
			// miSysTrayStop
			// 
			this.miSysTrayStop.Image = global::AudioMixer.Properties.Resources.pause2;
			this.miSysTrayStop.Name = "miSysTrayStop";
			this.miSysTrayStop.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayStop.Text = "Остановить";
			this.miSysTrayStop.Click += new System.EventHandler(this.miSysTrayStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// miSysTrayQuit
			// 
			this.miSysTrayQuit.Name = "miSysTrayQuit";
			this.miSysTrayQuit.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayQuit.Text = "Выход";
			this.miSysTrayQuit.Click += new System.EventHandler(this.miSysTrayQuit_Click);
			// 
			// clickDelayTimer
			// 
			this.clickDelayTimer.Tick += new System.EventHandler(this.clickDelayTimer_Tick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
			// 
			// miSysTrayVolume100
			// 
			this.miSysTrayVolume100.Name = "miSysTrayVolume100";
			this.miSysTrayVolume100.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayVolume100.Text = "Громкость 100%";
			this.miSysTrayVolume100.Click += new System.EventHandler(this.miSysTrayVolume100_Click);
			// 
			// miSysTrayVolume25
			// 
			this.miSysTrayVolume25.Name = "miSysTrayVolume25";
			this.miSysTrayVolume25.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayVolume25.Text = "Громкость 25%";
			this.miSysTrayVolume25.Click += new System.EventHandler(this.miSysTrayVolume25_Click);
			// 
			// miSysTrayVolume50
			// 
			this.miSysTrayVolume50.Name = "miSysTrayVolume50";
			this.miSysTrayVolume50.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayVolume50.Text = "Громкость 50%";
			this.miSysTrayVolume50.Click += new System.EventHandler(this.miSysTrayVolume50_Click);
			// 
			// miSysTrayVolume75
			// 
			this.miSysTrayVolume75.Name = "miSysTrayVolume75";
			this.miSysTrayVolume75.Size = new System.Drawing.Size(180, 22);
			this.miSysTrayVolume75.Text = "Громкость 75%";
			this.miSysTrayVolume75.Click += new System.EventHandler(this.miSysTrayVolume75_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 483);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "AudioMixer";
			this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
			this.cmSysTray.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.NotifyIcon notifyIcon;
		private System.Windows.Forms.ContextMenuStrip cmSysTray;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayPlay;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayStop;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayQuit;
		private System.Windows.Forms.ToolStripMenuItem miOpenForm;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Timer clickDelayTimer;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayVolume25;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayVolume50;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayVolume75;
		private System.Windows.Forms.ToolStripMenuItem miSysTrayVolume100;
	}
}

