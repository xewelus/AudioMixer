using System;
using System.Windows;
using System.Windows.Input;
using Common;
using CommonWpf;
using MouseKeyboardLibrary;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using CommonWpf.Classes.UI;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using KeyEventHandler = System.Windows.Forms.KeyEventHandler;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;

namespace AudioMixer
{
	public partial class WpfMainForm
	{
		private readonly Machine currentMachine;
		private readonly WindowController windowController;
		private readonly KeyboardHook keyboardHook = new KeyboardHook();

		private readonly WpfMixesListPanel pnlMixes;

		public WpfMainForm()
		{
			this.InitializeComponent();
			this.InitializeComponentWin();

			this.pnlMixes = this.mainPanel.MixesListPanel;

			this.Title = string.Format("{0} ({1})", this.Title, AssemblyInfo.VERSION);

			this.notifyIcon.Icon = Properties.Resources.app_play;
			this.notifyIcon.Text = this.Title;

			this.currentMachine = InitMachine();

			this.windowController = new WindowController(this, this.mainPanel.SplitContainer, this.currentMachine.Window, this.currentMachine.Dock);
			this.windowController.Init();

			this.mainPanel.Init(this.currentMachine, this.windowController);

			this.keyboardHook.KeyDown += new KeyEventHandler(this.keyboardHook_KeyDown);
			this.keyboardHook.Start();
		}

		private static Machine InitMachine()
		{
			string machineName = Environment.MachineName;
			foreach (Machine machine in Settings.Current.Machines)
			{
				if (machine.Name == machineName)
				{
					return machine;
				}
			}

			Machine newMachine = new Machine();
			newMachine.Name = machineName;
			Settings.Current.Machines.Add(newMachine);
			Settings.SaveAppearance();
			return newMachine;
		}

		private void WpfMainForm_OnClosing(object sender, CancelEventArgs e)
		{
			if (this.mainPanel.NeedSave)
			{
				MessageBoxResult result = UIHelper.ShowMessageBox("Сохранить настройки перед выходом?", buttons: MessageBoxButton.YesNoCancel);
				if (result == MessageBoxResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (result == MessageBoxResult.Yes)
				{
					this.mainPanel.Save();
				}
			}
		}

		private void WpfMainForm_OnClosed(object sender, EventArgs e)
		{
			this.keyboardHook.Stop();
			this.mainPanel.OnClosed();
		}

		public static bool IsPlayChangeKey(KeyEventArgs e)
		{
			return e.KeyData.In(Keys.Enter, Keys.Space);
		}

		public static bool IsPlayChangeKey(Key key)
		{
			return key.In(Key.Enter, Key.Space);
		}

		private void WpfMainForm_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.currentMachine == null)
			{
				return;
			}

			this.windowController.IgnoreSplitterMoved = true;
			//base.OnResize(e);
			this.windowController.IgnoreSplitterMoved = false;

			this.windowController.OnResize();
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			if (this.windowController != null)
			{
				this.windowController.LocationChanged();
			}
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.clickDelayTimer.Start();
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.clickDelayTimer.Stop();
				this.miOpenForm_Click(null, null);
			}
		}

		private void cmSysTray_Opening(object sender, CancelEventArgs e)
		{
			this.miSysTrayPlay.Enabled = this.pnlMixes.ActivatedMix == null && this.pnlMixes.SelectedMix != null;
			this.miSysTrayStop.Enabled = this.pnlMixes.ActivatedMix != null;

			this.miSysTrayVolume25.Checked = this.mainPanel.Volume == 25;
			this.miSysTrayVolume50.Checked = this.mainPanel.Volume == 50;
			this.miSysTrayVolume75.Checked = this.mainPanel.Volume == 75;
			this.miSysTrayVolume100.Checked = this.mainPanel.Volume == 100;
		}

		private void miSysTrayPlay_Click(object sender, EventArgs e)
		{
			this.pnlMixes.PlayChange();
		}

		private void miSysTrayStop_Click(object sender, EventArgs e)
		{
			this.pnlMixes.PlayChange();
		}

		private void miSysTrayQuit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mainPanel_PlayStateChanged(object sender, EventArgs e)
		{
			this.notifyIcon.Icon = this.pnlMixes.ActivatedMix == null ? Properties.Resources.app_stop : Properties.Resources.app_play;
		}

		private void miOpenForm_Click(object sender, EventArgs e)
		{
			this.WindowState = WindowState.Normal;
			this.Activate();
		}

		private void clickDelayTimer_Tick(object sender, EventArgs e)
		{
			this.clickDelayTimer.Stop();
			this.pnlMixes.PlayChange();
		}

		private void miSysTrayVolume25_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 25;
		}

		private void miSysTrayVolume50_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 50;
		}

		private void miSysTrayVolume75_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 75;
		}

		private void miSysTrayVolume100_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 100;
		}

		private Stopwatch keySw;
		private void keyboardHook_KeyDown(object sender, KeyEventArgs e)
		{
			try
			{
				if (e.KeyCode != Keys.F2) return;

				if (this.keySw == null)
				{
					this.keySw = Stopwatch.StartNew();
					return;
				}

				if (this.keySw.ElapsedMilliseconds < 500)
				{
					this.pnlMixes.PlayChange();
				}
				this.keySw = null;
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}


		#region MyRegion

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

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components;
		private void InitializeComponentWin()
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
			this.cmSysTray.ResumeLayout(false);
		}



		#endregion
	}
}
