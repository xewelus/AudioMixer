using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Threading;
using CommonWinForms;
using CommonWpf.Classes.UI;
using CSCore;
using CSCore.Codecs;
using CSCore.SoundOut;

namespace AudioMixer
{
	public class Player : IDisposable
	{
		public readonly MixInfo Mix;
		private readonly List<PlayerItem> items = new List<PlayerItem>();
		private Thread checkPauseThread;
		private bool isPaused;

		public Player(Device device, MixInfo mixInfo, float globalVolume)
		{
			this.Mix = mixInfo;

			foreach (SoundInfo soundInfo in mixInfo.Sounds)
			{
				string path = soundInfo.GetFullPath();
				if (!File.Exists(path))
				{
					UIHelper.ShowError(string.Format("Не найден файл '{0}'.", path));
					return;
				}
				PlayerItem readerInfo = new PlayerItem(device, soundInfo);
				this.items.Add(readerInfo);
			}

			this.UpdateVolume(globalVolume);
		}

		public void Play()
		{
			lock (this.items)
			{
				this.isPaused = false;
				foreach (PlayerItem item in this.items)
				{
					item.Play();
				}
			}
		}

		public void Pause()
		{
			this.isPaused = true;
			if (this.checkPauseThread == null)
			{
				this.checkPauseThread = new Thread(this.CheckPause);
				this.checkPauseThread.Start();
			}
		}

		public bool IsPaused()
		{
			return this.isPaused;
		}

		private void CheckPause()
		{
			try
			{
				while (true)
				{
					lock (this.items)
					{
						if (this.isPaused)
						{
							foreach (PlayerItem item in this.items)
							{
								item.Pause();
							}
						}
					}

					Thread.Sleep(50);
				}
			}
			catch (Exception ex)
			{
				App.Current.Dispatcher.BeginInvoke(() => ExceptionHandler.Catch(ex));
			}
		}

		public void UpdateVolume(float globalVolume)
		{
			lock (this.items)
			{
				foreach (PlayerItem readerInfo in this.items)
				{
					readerInfo.UpdateVolume(this.Mix.Volume * globalVolume);
				}
			}
		}

		public void Dispose()
		{
			lock (this.items)
			{
				foreach (PlayerItem item in this.items)
				{
					item.Dispose();
				}
				this.items.Clear();
			}
		}

		private class PlayerItem : IDisposable
		{
			private readonly SoundInfo soundInfo;
			private readonly IWaveSource waveSource;
			private readonly ISoundOut soundOut;
			private bool disposed;

			public PlayerItem(Device device, SoundInfo soundInfo)
			{
				try
				{
					this.soundInfo = soundInfo;

					string file = soundInfo.GetFullPath();
					this.waveSource = CodecFactory.Instance.GetCodec(file);

					if (WasapiOut.IsSupportedOnCurrentPlatform)
					{
						WasapiOut wasapiOut = new WasapiOut();
						wasapiOut.StreamRoutingOptions = StreamRoutingOptions.All;
						wasapiOut.Device = device.GetDevice();
						this.soundOut = wasapiOut;
					}
					else
					{
						throw new NotSupportedException(nameof(DirectSoundOut));
					}

					this.soundOut.Initialize(this.waveSource);
					this.soundOut.Stopped += this.SoundOut_Stopped;
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			private void SoundOut_Stopped(object sender, PlaybackStoppedEventArgs e)
			{
				if (this.disposed) return;

				this.waveSource.Position = 0;

				lock (this.soundOut)
				{
					this.soundOut.Play();
				}
			}

			public void Dispose()
			{
				this.disposed = true;
				if (this.soundOut != null)
				{
					this.soundOut.Stop();
					this.soundOut.WaitForStopped();
					this.soundOut.Dispose();
				}

				if (this.waveSource != null)
				{
					this.waveSource.Dispose();
				}
			}

			public void Play()
			{
				lock (this.soundOut)
				{
					this.soundOut.Play();
				}
			}

			public void Pause()
			{
				lock (this.soundOut)
				{
					this.soundOut.Pause();
				}
			}

			public void UpdateVolume(float globalVolume)
			{
				lock (this.soundOut)
				{
					this.soundOut.Volume = globalVolume * this.soundInfo.Volume;
				}
			}
		}
	}
}
