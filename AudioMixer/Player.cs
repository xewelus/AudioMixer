using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CommonWinForms;
using CommonWinForms.Extensions;
using CSCore;
using CSCore.Codecs;
using CSCore.CoreAudioAPI;
using CSCore.SoundOut;

namespace AudioMixer
{
	public class Player : IDisposable
	{
		public readonly MixInfo Mix;
		private readonly List<PlayerItem> items = new List<PlayerItem>();
		private Thread checkPauseThread;
		private bool isPaused;

		public Player(MMDevice device, MixInfo mixInfo, float globalVolume)
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
			this.isPaused = false;
			foreach (PlayerItem item in this.items)
			{
				item.Play();
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

		private void CheckPause()
		{
			try
			{
				while (true)
				{
					if (this.isPaused)
					{
						foreach (PlayerItem item in this.items)
						{
							item.Pause();
						}

						Thread.Sleep(50);
					}
				}
			}
			catch (Exception ex)
			{
				UIHelper.MainForm.BeginInvoke(() => ExcHandler.Catch(ex));
			}
		}

		public void UpdateVolume(float globalVolume)
		{
			foreach (PlayerItem readerInfo in this.items)
			{
				readerInfo.UpdateVolume(this.Mix.Volume * globalVolume);
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
			private readonly SoundInfo soundInfo;
			private readonly IWaveSource waveSource;
			private readonly ISoundOut soundOut;

			public PlayerItem(MMDevice device, SoundInfo soundInfo)
			{
				try
				{
					this.soundInfo = soundInfo;

					string file = soundInfo.GetFullPath();
					this.waveSource = CodecFactory.Instance.GetCodec(file);

					if (WasapiOut.IsSupportedOnCurrentPlatform)
					{
						WasapiOut wasapiOut = new WasapiOut();
						wasapiOut.Device = device;
						this.soundOut = wasapiOut;
					}
					else
					{
						DirectSoundOut directSoundOut = new DirectSoundOut();
						directSoundOut.Device = new Guid(device.DeviceID);
						this.soundOut = directSoundOut;
					}

					this.soundOut.Initialize(this.waveSource);
				}
				catch
				{
					this.Dispose();
					throw;
				}
			}

			public void Dispose()
			{
				if (this.soundOut != null)
				{
					this.soundOut.Stop();
					this.soundOut.Dispose();
				}

				if (this.waveSource != null)
				{
					this.waveSource.Dispose();
				}
			}

			public void Play()
			{
				this.soundOut.Play();
			}

			public void Pause()
			{
				this.soundOut.Pause();
			}

			public void UpdateVolume(float globalVolume)
			{
				this.soundOut.Volume = globalVolume * this.soundInfo.Volume;
			}
		}
	}
}
