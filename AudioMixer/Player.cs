using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using CommonWinForms;
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
		private SoundHierarchy soundHierarchy = new SoundHierarchy();

		public Player(Device device, MixInfo mixInfo, float globalVolume)
		{
			this.Mix = mixInfo;
			var processedSounds = new HashSet<SoundInfo>();

			foreach (SoundInfo soundInfo in mixInfo.Sounds)
			{
				this.AddPlayerItemsRecursively(device, soundInfo, null, processedSounds);
			}

			this.UpdateVolume(globalVolume);
		}

		private void AddPlayerItemsRecursively(Device device, SoundInfo soundInfo, SoundInfo parentSoundInfo, HashSet<SoundInfo> processedSounds)
		{
			if (processedSounds.Contains(soundInfo))
			{
				return;
			}
			
			processedSounds.Add(soundInfo);

			if (soundInfo.Type == SoundInfo.Types.Mix)
			{
				if (soundInfo.MixID == null)
				{
					UIHelper.ShowError($"Mix not set.");
					return;
				}

				MixInfo mix = Settings.Current.Mixes.FirstOrDefault(m => m.ID == soundInfo.MixID);
				if (mix == null)
				{
					UIHelper.ShowError($"Mix '{soundInfo.MixID}' not found.");
					return;
				}
				foreach (SoundInfo submixSound in mix.Sounds)
				{
					this.AddPlayerItemsRecursively(device, submixSound, soundInfo, processedSounds);
				}
			}
			else
			{
				this.AddPlayerItem(device, soundInfo, parentSoundInfo);
			}
		}

		private void AddPlayerItem(Device device, SoundInfo soundInfo, SoundInfo parentSoundInfo)
		{
			if (parentSoundInfo != null)
			{
				soundHierarchy.Add(soundInfo, parentSoundInfo);
			}

			string path = soundInfo.GetFullPath();
			if (!File.Exists(path))
			{
				UIHelper.ShowError($"File '{path}' not found.");
				return;
			}
			PlayerItem readerInfo = new PlayerItem(device, soundInfo, parentSoundInfo, this.soundHierarchy);
			this.items.Add(readerInfo);
		}

		public void Play()
		{
			lock (this.items)
			{
				this.isPaused = false;
				foreach (PlayerItem item in this.items)
				{
					Task.Run(() => item.Play());
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
				UIHelper.MainForm.BeginInvoke(() => ExcHandler.Catch(ex));
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
			private readonly SoundInfo parentSoundInfo;
			private readonly SoundHierarchy soundHierarchy;
			private readonly IWaveSource waveSource;
			private readonly ISoundOut soundOut;
			private bool disposed;

			public PlayerItem(Device device, SoundInfo soundInfo, SoundInfo parentSoundInfo, SoundHierarchy soundHierarchy)
			{
				this.soundInfo = soundInfo;
				this.parentSoundInfo = parentSoundInfo;
				this.soundHierarchy = soundHierarchy;

				try
				{
					string path = this.soundInfo.GetFullPath();
					this.waveSource = CodecFactory.Instance.GetCodec(path);

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
					try
					{
						this.soundOut.Pause();
					}
					catch
					{
						// do nothing
					}
				}
			}

			public void UpdateVolume(float globalVolume)
			{
				float effectiveVolume = globalVolume * this.soundInfo.Volume;
				effectiveVolume *= GetParentVolume(this.parentSoundInfo);

				lock (this.soundOut)
				{
					this.soundOut.Volume = effectiveVolume;
				}
			}

			private float GetParentVolume(SoundInfo parent)
			{
				if (parent == null) return 1.0f;
				SoundInfo parentSound = this.soundHierarchy.GetParent(parent);
				return parent.Volume * GetParentVolume(parentSound);
			}
		}
	}
	
	public class SoundHierarchy
	{
		private readonly Dictionary<SoundInfo, SoundInfo> hierarchy = new Dictionary<SoundInfo, SoundInfo>();

        public void Add(SoundInfo child, SoundInfo parent)
        {
            if (child != null && parent != null)
            {
                hierarchy[child] = parent;
            }
        }

        public SoundInfo GetParent(SoundInfo soundInfo)
        {
            if (hierarchy.TryGetValue(soundInfo, out SoundInfo parent))
            {
                return parent;
            }
            return null; // No parent found
        }
    }
}