using System;
using System.ComponentModel;
using System.Windows.Forms;
using AudioMixer.Properties;
using Common;
using CommonWinForms;

namespace AudioMixer
{
	public sealed partial class MainForm : Form
	{
		private readonly Machine currentMachine;
		private readonly WindowController windowController;

		private readonly MixesListPanel pnlMixes;

		public MainForm()
		{
			this.InitializeComponent();

			this.pnlMixes = this.mainPanel.MixesListPanel;

			this.Icon = Resources.app;
			this.ShowIcon = true;

			this.Text = string.Format("{0} ({1})", this.Text, AssemblyInfo.VERSION);

			this.notifyIcon.Icon = Resources.app_play;
			this.notifyIcon.Text = this.Text;

			this.currentMachine = InitMachine();

			this.windowController = new WindowController(this, this.mainPanel.SplitContainer, this.currentMachine.Window, this.currentMachine.Dock);
			this.windowController.Init();

			this.mainPanel.Init(this.currentMachine, this.windowController);
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

		protected override void OnClosing(CancelEventArgs e)
		{
			if (this.mainPanel.NeedSave)
			{
				DialogResult result = UIHelper.ShowMessageBox("Сохранить настройки перед выходом?", buttons: MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (result == DialogResult.Yes)
				{
					this.mainPanel.Save();
				}
			}

			base.OnClosing(e);
		}

		protected override void OnClosed(EventArgs e)
		{
			this.mainPanel.OnClosed();
			base.OnClosed(e);
		}

		public static bool IsPlayChangeKey(KeyEventArgs e)
		{
			return e.KeyData.In(Keys.Enter, Keys.Space);
		}

		protected override void OnResize(EventArgs e)
		{
			if (this.currentMachine == null)
			{
				base.OnResize(e);
				return;
			}

			this.windowController.IgnoreSplitterMoved = true;
			base.OnResize(e);
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
			this.notifyIcon.Icon = this.pnlMixes.ActivatedMix == null ? Resources.app_stop : Resources.app_play;
		}

		private void miOpenForm_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Normal;
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
	}
}
