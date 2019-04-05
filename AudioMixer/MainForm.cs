using System;
using System.Windows.Forms;
using Common;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioMixer
{
	public partial class MainForm : Form
	{
		private MixPanel mixPanel;
		private Player player;
		private bool internalChanges;
		public MainForm()
		{
			this.InitializeComponent();

			this.internalChanges = true;

			DirectSoundDeviceInfo toSelect = null;
			foreach (DirectSoundDeviceInfo deviceInfo in DirectSoundOut.Devices)
			{
				this.cbAudioDevice.Items.Add(deviceInfo);

				if (deviceInfo.Description == Settings.Current.AudioDevice)
				{
					toSelect = deviceInfo;
				}
			}

			if (toSelect != null)
			{
				this.cbAudioDevice.SelectedItem = toSelect;
			}

			this.internalChanges = false;
		}

		protected override void OnClosed(EventArgs e)
		{
			Settings.Save();

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

		private void saveTimer_Tick(object sender, EventArgs e)
		{
			Settings.Save(true);
		}

		private void cbAudioDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;

			DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
			Settings.Current.AudioDevice = deviceInfo.Description;
			Settings.Save(true);
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
	}
}
