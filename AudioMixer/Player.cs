using System;
using System.Collections.Generic;
using System.IO;
using Common;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace AudioMixer
{
	public class Player : IDisposable
	{
		private readonly MixInfo mixInfo;
		private readonly List<PlayerItem> items = new List<PlayerItem>();

		public Player(DirectSoundDeviceInfo deviceInfo, MixInfo mixInfo)
		{
			this.mixInfo = mixInfo;

			foreach (SoundInfo soundInfo in mixInfo.Sounds)
			{
				string path = soundInfo.GetFullPath();
				if (!File.Exists(path))
				{
					UIHelper.ShowError(string.Format("Не найден файл '{0}'.", path));
					return;
				}
				PlayerItem readerInfo = new PlayerItem(deviceInfo, soundInfo);
				this.items.Add(readerInfo);
			}

			this.UpdateVolume();
		}

		public void Play()
		{
			foreach (PlayerItem item in this.items)
			{
				item.Play();
			}
		}

		public void UpdateVolume()
		{
			foreach (PlayerItem readerInfo in this.items)
			{
				readerInfo.UpdateVolume(this.mixInfo.Volume);
			}
		}

		public void Dispose()
		{
			foreach (PlayerItem item in this.items)
			{
				item.Dispose();
			}
			this.items.Clear();
		}

		private class PlayerItem : IDisposable
		{
			private IWavePlayer waveOut;
			private readonly SoundInfo soundInfo;
			public AudioFileReader AudioFileReader;
			private readonly SampleChannel sampleChannel;

			public PlayerItem(DirectSoundDeviceInfo deviceInfo, SoundInfo soundInfo)
			{
				this.soundInfo = soundInfo;

				this.waveOut = new DirectSoundOut(deviceInfo.Guid, 100);
				this.waveOut.PlaybackStopped += this.OnPlaybackStopped;

				this.AudioFileReader = new AudioFileReader(soundInfo.GetFullPath());
				this.sampleChannel = new SampleChannel(this.AudioFileReader, true);
				MeteringSampleProvider postVolumeMeter = new MeteringSampleProvider(this.sampleChannel);
				this.waveOut.Init(postVolumeMeter);
			}

			public void Dispose()
			{
				if (this.waveOut != null)
				{
					this.waveOut.Stop();
				}

				if (this.AudioFileReader != null)
				{
					this.AudioFileReader.Dispose();
				}
				this.AudioFileReader = null;

				if (this.waveOut != null)
				{
					this.waveOut.Dispose();
					this.waveOut = null;
				}
			}

			public void Play()
			{
				this.waveOut.Play();
			}

			public void UpdateVolume(float globalVolume)
			{
				this.sampleChannel.Volume = globalVolume * this.soundInfo.Volume;
			}

			private void OnPlaybackStopped(object sender, StoppedEventArgs e)
			{
				if (this.AudioFileReader != null)
				{
					this.AudioFileReader.Position = 0;

					if (e.Exception == null)
					{
						this.waveOut.Play();
					}
				}
				if (e.Exception != null)
				{
					//throw new Exception("Playback Device Error", e.Exception);
				}
			}
		}
	}
}
