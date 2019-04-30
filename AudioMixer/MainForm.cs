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
		private bool internalChanges;
		private DeviceInfo currentDevice;
		public MainForm()
		{
			this.InitializeComponent();

			this.Icon = Resources.app;
			this.ShowIcon = true;

			this.Text = string.Format("{0} ({1})", this.Text, AssemblyInfo.VERSION);

			this.notifyIcon.Icon = this.Icon;
			this.notifyIcon.Text = this.Text;

			Settings.OnNeedSave += this.OnNeedSave;

			this.InitDevice();
		}

		private void InitDevice()
		{
			this.internalChanges = true;
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

			foreach (DeviceInfo deviceInfo in Settings.Current.AudioDevices)
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
				Settings.Current.AudioDevices.Add(this.currentDevice);
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
			this.internalChanges = false;
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (this.needSave && !UIHelper.AskYesNo("Вы уверены, что хотите закрыть форму без сохранения настроек?"))
			{
				e.Cancel = true;
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
			base.OnResize(e);

			bool minimized = this.WindowState == FormWindowState.Minimized;
			this.notifyIcon.Visible = minimized;
			this.ShowInTaskbar = !minimized;
		}

		private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.WindowState = FormWindowState.Normal;
			}
		}
	}
}
