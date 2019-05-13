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

		public List<DeviceInfo> AudioDevices = new List<DeviceInfo>();
		public List<MixInfo> Mixes = new List<MixInfo>();
		public DockSettings DockSettings = new DockSettings();

		private static readonly string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

		private static readonly object locker = new object();

		public static EventHandler OnNeedSave;

		public static void SetNeedSave()
		{
			if (OnNeedSave != null)
			{
				OnNeedSave.Invoke(null, EventArgs.Empty);
			}
		}

		public static void Save()
		{
			lock (locker)
			{
				XmlSerializer xs = new XmlSerializer(typeof(Settings));
				using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.Write))
				{
					fs.SetLength(0);
					xs.Serialize(fs, Current);
				}
			}
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

	public class DeviceInfo
	{
		public string Hash;
		public string Name;
	}

	public class DockSettings
	{
		public bool IsVertical = true;
		public int Width = 220;
		public int Height = 100;
	}
}