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
			this.miSysTrayPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.miSysTrayStop = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miSysTrayQuit = new System.Windows.Forms.ToolStripMenuItem();
			this.mainPanel = new AudioMixer.MainPanel();
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
            this.miSysTrayPlay,
            this.miSysTrayStop,
            this.toolStripSeparator1,
            this.miSysTrayQuit});
			this.cmSysTray.Name = "cmSysTray";
			this.cmSysTray.Size = new System.Drawing.Size(158, 76);
			this.cmSysTray.Opening += new System.ComponentModel.CancelEventHandler(this.cmSysTray_Opening);
			// 
			// miSysTrayPlay
			// 
			this.miSysTrayPlay.Image = global::AudioMixer.Properties.Resources.play;
			this.miSysTrayPlay.Name = "miSysTrayPlay";
			this.miSysTrayPlay.Size = new System.Drawing.Size(157, 22);
			this.miSysTrayPlay.Text = "Воспроизвести";
			this.miSysTrayPlay.Click += new System.EventHandler(this.miSysTrayPlay_Click);
			// 
			// miSysTrayStop
			// 
			this.miSysTrayStop.Image = global::AudioMixer.Properties.Resources.pause2;
			this.miSysTrayStop.Name = "miSysTrayStop";
			this.miSysTrayStop.Size = new System.Drawing.Size(157, 22);
			this.miSysTrayStop.Text = "Остановить";
			this.miSysTrayStop.Click += new System.EventHandler(this.miSysTrayStop_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
			// 
			// miSysTrayQuit
			// 
			this.miSysTrayQuit.Name = "miSysTrayQuit";
			this.miSysTrayQuit.Size = new System.Drawing.Size(157, 22);
			this.miSysTrayQuit.Text = "Выход";
			this.miSysTrayQuit.Click += new System.EventHandler(this.miSysTrayQuit_Click);
			// 
			// mainPanel
			// 
			this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.mainPanel.Location = new System.Drawing.Point(0, 0);
			this.mainPanel.Margin = new System.Windows.Forms.Padding(0);
			this.mainPanel.Name = "mainPanel";
			this.mainPanel.Size = new System.Drawing.Size(655, 483);
			this.mainPanel.TabIndex = 1;
			this.mainPanel.PlayStateChanged += new System.EventHandler(this.mainPanel_PlayStateChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(655, 483);
			this.Controls.Add(this.mainPanel);
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
		private MainPanel mainPanel;
	}
}

