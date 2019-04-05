using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace AudioMixer
{
	public class Settings
	{
		public static Settings Current = new Settings();

		public string AudioDevice;
		public List<MixInfo> Mixes = new List<MixInfo>();

		private static readonly string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

		public static void Save()
		{
			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.Write))
			{
				fs.SetLength(0);
				xs.Serialize(fs, Current);
			}
		}

		public static void Load()
		{
			if (!File.Exists(PATH)) return;

			XmlSerializer xs = new XmlSerializer(typeof(Settings));
			using (FileStream fs = new FileStream(PATH, FileMode.Open, FileAccess.Read))
			{
				Current = (Settings)xs.Deserialize(fs);
			}
		}
	}

	public class MixInfo
	{
		public string Name;
		public float Volume;
		public List<SoundInfo> Sounds = new List<SoundInfo>();
	}

	public class SoundInfo
	{
		public string Path;
		public bool IsActive;
		public float Volume;
	}
}