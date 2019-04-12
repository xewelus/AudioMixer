using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

namespace AudioMixer
{
	public class Settings
	{
		private static Settings current = new Settings();
		public static Settings Current
		{
			get
			{
				return current;
			}
		}

		public string AudioDevice;
		public List<MixInfo> Mixes = new List<MixInfo>();

		private static readonly string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

		private static readonly object locker = new object();
		private static bool isSaving;

		public static void Save(bool anotherThread = false)
		{
			if (anotherThread)
			{
				Thread thread = new Thread(SaveThread);
				thread.Start();
			}
			else
			{
				lock (locker)
				{
					try
					{
						isSaving = true;

						XmlSerializer xs = new XmlSerializer(typeof(Settings));
						using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.Write))
						{
							fs.SetLength(0);
							xs.Serialize(fs, Current);
						}
					}
					finally
					{
						isSaving = false;
					}
				}
			}
		}

		private static void SaveThread()
		{
			Save();
		}

		public static void Load()
		{
			if (!File.Exists(PATH)) return;

			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			using (FileStream fs = new FileStream(PATH, FileMode.Open, FileAccess.Read))
			{
				current = (Settings)xs.Deserialize(fs);
			}
		}
	}

	public class MixInfo
	{
		public string Name;
		public float Volume = 1f;
		public List<SoundInfo> Sounds = new List<SoundInfo>();

		public override string ToString()
		{
			return this.Name ?? "<пусто>";
		}
	}

	public class SoundInfo
	{
		public string Path;
		public float Volume = 1f;

		public override string ToString()
		{
			return this.Path ?? "<пусто>";
		}

		public string GetFullPath()
		{
			if (System.IO.Path.IsPathRooted(this.Path))
			{
				return this.Path;
			}
			return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.Path);
		}
	}
}