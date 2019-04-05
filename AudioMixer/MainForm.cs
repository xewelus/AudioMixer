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
				this.splitContainer.Panel2.Controls.Add(this.mixPanel);
			}
		}

		private void MixPanelOnNameChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.UpdateName(this.mixPanel.MixName);
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

        private IWavePlayer waveOut;
        private AudioFileReader audioFileReader;
		private Action<float> setVolumeDelegate;

		private void pnlMixes_ItemActivated(object sender, EventArgs e)
		{
			if (this.pnlMixes.ActivatedMix == null)
			{
				if (waveOut != null)
				{
					//waveOut.Stop();
					CloseWaveOut();
				}
				return;
			}

			try
			{
				this.CreateWaveOut();
			}
			catch (Exception driverCreateException)
			{
				MessageBox.Show(String.Format("{0}", driverCreateException.Message));
				return;
			}

			ISampleProvider sampleProvider;
			try
			{
				sampleProvider = this.CreateInputStream(this.pnlMixes.ActivatedMix.Sounds[0].Path);
			}
			catch (Exception createException)
			{
				MessageBox.Show(String.Format("{0}", createException.Message), "Error Loading File");
				return;
			}

			try
			{
				this.waveOut.Init(sampleProvider);
			}
			catch (Exception initException)
			{
				MessageBox.Show(String.Format("{0}", initException.Message), "Error Initializing Output");
				return;
			}

			this.setVolumeDelegate(this.pnlMixes.ActivatedMix.Volume);
			this.waveOut.Play();
		}

		private void CreateWaveOut()
		{
			this.CloseWaveOut();

			DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
			if (deviceInfo == null)
			{
				UIHelper.ShowError("Необходимо выбрать аудио-устройство.");
				return;
			}

			this.waveOut = new DirectSoundOut(deviceInfo.Guid, 500);
			this.waveOut.PlaybackStopped += this.OnPlaybackStopped;
		}

		private void CloseWaveOut()
		{
			if (this.waveOut != null)
			{
				this.waveOut.Stop();
			}
			if (this.audioFileReader != null)
			{
				// this one really closes the file and ACM conversion
				this.audioFileReader.Dispose();
				this.setVolumeDelegate = null;
				this.audioFileReader = null;
			}
			if (this.waveOut != null)
			{
				this.waveOut.Dispose();
				this.waveOut = null;
			}
		}

		private ISampleProvider CreateInputStream(string fileName)
		{
			this.audioFileReader = new AudioFileReader(fileName);

			SampleChannel sampleChannel = new SampleChannel(this.audioFileReader, true);
			this.setVolumeDelegate = vol => sampleChannel.Volume = vol;
			MeteringSampleProvider postVolumeMeter = new MeteringSampleProvider(sampleChannel);

			return postVolumeMeter;
		}

		private void OnPlaybackStopped(object sender, StoppedEventArgs e)
		{
			if (this.audioFileReader != null)
			{
				this.audioFileReader.Position = 0;
			}
			if (e.Exception != null)
			{
				//throw new Exception("Playback Device Error", e.Exception);
			}
		}
	}
}
