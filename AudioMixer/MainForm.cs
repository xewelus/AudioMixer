using System;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AudioMixer.Properties;
using Common;
using NAudio.Wave;

namespace AudioMixer
{
	public sealed partial class MainForm : Form
	{
		private MixPanel mixPanel;
		private Player player;
		private readonly bool internalChanges;
		private DeviceInfo currentDevice;
		private Machine currentMachine;
		private FormWindowState prevFormState;

		public MainForm()
		{
			this.InitializeComponent();

			try
			{
				this.internalChanges = true;

				this.Icon = Resources.app;
				this.ShowIcon = true;

				this.Text = string.Format("{0} ({1})", this.Text, AssemblyInfo.VERSION);

				this.notifyIcon.Icon = Resources.app_play;
				this.notifyIcon.Text = this.Text;

				Settings.OnNeedSave += this.OnNeedSave;

				this.InitMachine();

				this.prevFormState = this.WindowState;
			}
			finally
			{
				this.internalChanges = false;
			}
		}

		private void SetupOrientation()
		{
			this.ignoreSplitterMoved = true;
			this.splitContainer.Orientation = this.currentMachine.Dock.IsVertical ? Orientation.Vertical : Orientation.Horizontal;
			if (this.currentMachine.Dock.IsVertical)
			{
				this.splitContainer.SplitterDistance = this.currentMachine.Dock.Width;
			}
			else
			{
				this.splitContainer.SplitterDistance = this.currentMachine.Dock.Height;
			}
			this.ignoreSplitterMoved = false;
		}

		private void InitDevice()
		{
			StringBuilder sb = new StringBuilder();
			foreach (DirectSoundDeviceInfo di in DirectSoundOut.Devices)
			{
				this.cbAudioDevice.Items.Add(di);

				if (sb.Length > 0)
				{
					sb.AppendLine();
				}
				sb.Append(di.Description);
			}

			byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
			SHA256 sha = SHA256.Create();
			byte[] hashBytes = sha.ComputeHash(bytes);
			string hash = Convert.ToBase64String(hashBytes);

			foreach (DeviceInfo deviceInfo in this.currentMachine.AudioDevices)
			{
				if (deviceInfo.Hash == hash)
				{
					this.currentDevice = deviceInfo;
					break;
				}
			}

			if (this.currentDevice == null)
			{
				this.currentDevice = new DeviceInfo();
				this.currentDevice.Hash = hash;
				this.currentMachine.AudioDevices.Add(this.currentDevice);
				Settings.SetNeedSave();
			}

			if (this.currentDevice != null)
			{
				foreach (DirectSoundDeviceInfo deviceInfo in this.cbAudioDevice.Items)
				{
					if (deviceInfo.Description == this.currentDevice.Name)
					{
						this.cbAudioDevice.SelectedItem = deviceInfo;
						break;
					}
				}
			}
		}

		private void InitMachine()
		{
			string machineName = Environment.MachineName;
			foreach (Machine machine in Settings.Current.Machines)
			{
				if (machine.Name == machineName)
				{
					this.currentMachine = machine;
					break;
				}
			}

			if (this.currentMachine == null)
			{
				this.currentMachine = new Machine();
				this.currentMachine.Name = machineName;
				Settings.Current.Machines.Add(this.currentMachine);
				Settings.SetNeedSave();
			}

			WindowSettings window = this.currentMachine.Window;

			this.Location = window.Location;
			this.Size = window.Size;

			if (window.IsMaximized)
			{
				this.WindowState = FormWindowState.Maximized;
			}

			this.InitDevice();
			this.SetupOrientation();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (this.needSave)
			{
				DialogResult result = UIHelper.ShowMessageBox("Сохранить настройки перед выходом?", buttons: MessageBoxButtons.YesNoCancel);
				if (result == DialogResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (result == DialogResult.Yes)
				{
					this.btnSave_Click(this, EventArgs.Empty);
				}
			}

			base.OnClosing(e);
		}

		protected override void OnClosed(EventArgs e)
		{
			if (this.player != null)
			{
				this.player.Dispose();
				this.player = null;
			}

			base.OnClosed(e);
		}

		private void pnlMixes_ItemSelected(object sender, EventArgs e)
		{
			if (this.mixPanel != null)
			{
				this.mixPanel.Remove(true);
				this.mixPanel = null;
			}

			if (this.pnlMixes.SelectedMix != null)
			{
				this.mixPanel = new MixPanel(this.pnlMixes.SelectedMix);
				this.mixPanel.Dock = DockStyle.Fill;
				this.mixPanel.NameChanged += this.MixPanelOnNameChanged;
				this.mixPanel.VolumeChanged += this.MixPanelOnVolumeChanged;
				this.mixPanel.PlayChanged += this.MixPanelOnPlayChanged;
				this.splitContainer.Panel2.Controls.Add(this.mixPanel);
			}
		}

		private void MixPanelOnNameChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.UpdateName(this.mixPanel.MixName);
		}

		private void MixPanelOnVolumeChanged(object sender, EventArgs eventArgs)
		{
			if (this.player != null)
			{
				this.player.UpdateVolume();
			}
		}

		private void MixPanelOnPlayChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.PlayChange();
		}

		public static bool IsPlayChangeKey(KeyEventArgs e)
		{
			return e.KeyData.In(Keys.Enter, Keys.Space);
		}

		private void cbAudioDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;

			DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
			this.currentDevice.Name = deviceInfo.Description;
			Settings.SetNeedSave();
		}

		private void pnlMixes_ItemActivated(object sender, EventArgs e)
		{
			if (this.player != null)
			{
				this.player.Dispose();
				this.player = null;
			}

			if (this.pnlMixes.ActivatedMix != null)
			{
				DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
				if (deviceInfo == null)
				{
					UIHelper.ShowError("Необходимо выбрать аудио-устройство.");
					return;
				}

				this.player = new Player(deviceInfo, this.pnlMixes.ActivatedMix);
				this.player.Play();
			}

			this.UpdateNotifyIcon();
		}

		private void UpdateNotifyIcon()
		{
			this.notifyIcon.Icon = this.pnlMixes.ActivatedMix == null ? Resources.app_stop : Resources.app_play;
		}

		private bool needSave;
		private void OnNeedSave(object sender, EventArgs eventArgs)
		{
			this.btnSave.Enabled = true;
			this.needSave = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				this.btnSave.Enabled = false;
				this.needSave = false;

				Application.DoEvents();
				Settings.Save();
			}
			catch
			{
				this.btnSave.Enabled = true;
				this.needSave = true;
				throw;
			}
		}

		protected override void OnResize(EventArgs e)
		{
			this.ignoreSplitterMoved = true;
			base.OnResize(e);
			this.ignoreSplitterMoved = false;

			if (this.currentMachine == null) return;

			bool minimized = this.WindowState == FormWindowState.Minimized;
			this.notifyIcon.Visible = minimized;
			this.ShowInTaskbar = !minimized;

			bool save = false;
			if (!minimized)
			{
				if (this.prevFormState != this.WindowState)
				{
					this.prevFormState = this.WindowState;
					this.currentMachine.Window.IsMaximized = this.WindowState == FormWindowState.Maximized;
					save = true;

					if (!this.currentMachine.Window.IsMaximized)
					{
						this.Location = this.currentMachine.Window.Location;
					}
				}
			}

			if (this.WindowState == FormWindowState.Normal && this.currentMachine.Window.Size != this.Size)
			{
				this.currentMachine.Window.Size = this.Size;
				save = true;
			}

			if (save)
			{
				Settings.SetNeedSave();
			}
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.pnlMixes.PlayChange();
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.WindowState = FormWindowState.Normal;
			}
		}

		private void cmSysTray_Opening(object sender, CancelEventArgs e)
		{
			this.miSysTrayPlay.Enabled = this.pnlMixes.ActivatedMix == null && this.pnlMixes.SelectedMix != null;
			this.miSysTrayStop.Enabled = this.pnlMixes.ActivatedMix != null;
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

		private void pnlMixes_DockButtonClick(object sender, EventArgs e)
		{
			this.currentMachine.Dock.IsVertical = !this.currentMachine.Dock.IsVertical;
			Settings.SetNeedSave();

			this.SetupOrientation();

			this.pnlMixes.PanelOrientation = this.splitContainer.Orientation;
		}

		private bool ignoreSplitterMoved;
		private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.ignoreSplitterMoved) return;

			if (this.currentMachine.Dock.IsVertical)
			{
				if (this.currentMachine.Dock.Width != this.splitContainer.SplitterDistance)
				{
					this.currentMachine.Dock.Width = this.splitContainer.SplitterDistance;
					Settings.SetNeedSave();
				}
			}
			else
			{
				if (this.currentMachine.Dock.Height != this.splitContainer.SplitterDistance)
				{
					this.currentMachine.Dock.Height = this.splitContainer.SplitterDistance;
					Settings.SetNeedSave();
				}
			}
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;
			if (this.WindowState != FormWindowState.Normal) return;

			if (this.currentMachine.Window.Location != this.Location)
			{
				this.currentMachine.Window.Location = this.Location;
				Settings.SetNeedSave();
			}
		}
	}
}
