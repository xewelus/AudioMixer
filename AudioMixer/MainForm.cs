using System;
using System.ComponentModel;
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
		private readonly bool internalChanges;
		public MainForm()
		{
			this.InitializeComponent();

			Settings.OnNeedSave += this.OnNeedSave;

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
			Settings.Current.AudioDevice = deviceInfo.Description;
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
	}
}
