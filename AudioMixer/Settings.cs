using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using Common;

namespace AudioMixer
{
	[Serializable]
	public class Settings
	{
		private static Settings previuos;
		private static Settings current = new Settings();
		public static Settings Current
		{
			get
			{
				return current;
			}
		}

		private static readonly string PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");
		private static readonly object locker = new object();
		public static EventHandler OnNeedSave;

		public List<MixInfo> Mixes = new List<MixInfo>();
		public List<Machine> Machines = new List<Machine>();

		public static void SetNeedSave()
		{
			if (OnNeedSave != null)
			{
				OnNeedSave.Invoke(null, EventArgs.Empty);
			}
		}

		public static void SaveAppearance()
		{
			Settings settings = current.DeepCopy();
			if (previuos != null)
			{
				settings.Mixes = previuos.Mixes;
			}
			settings.InternalSave();
		}

		public static void Save()
		{
			Current.InternalSave();
			previuos = Current.DeepCopy();
		}

		private void InternalSave()
		{
			lock (locker)
			{
				XmlSerializer xs = new XmlSerializer(typeof(Settings));
				using (FileStream fs = new FileStream(PATH, FileMode.OpenOrCreate, FileAccess.Write))
				{
					fs.SetLength(0);
					xs.Serialize(fs, this);
				}
			}
		}

		public static void Load()
		{
			lock (locker)
			{
				if (!File.Exists(PATH)) return;

				XmlSerializer xs = new XmlSerializer(typeof(Settings));
				using (FileStream fs = new FileStream(PATH, FileMode.Open, FileAccess.Read))
				{
					current = (Settings)xs.Deserialize(fs);
				}

				bool updated = current.UpdateIds();
				if (updated)
				{
					Save();
				}
				previuos = current.DeepCopy();
			}
		}

		private bool UpdateIds()
		{
			bool updated = false;
			foreach (MixInfo mixInfo in this.Mixes)
			{
				if (mixInfo.ID == 0)
				{
					mixInfo.ID = MixIdsCollection.GetFreeID();
					updated = true;
				}
			}
			return updated;
		}
	}

	[Serializable]
	public class MixInfo
	{
		public int ID;
		public string Name;
		public float Volume = 1f;
		public List<SoundInfo> Sounds = new List<SoundInfo>();

		public static MixInfo Create()
		{
			return new MixInfo {ID = MixIdsCollection.GetFreeID()};
		}

		public override string ToString()
		{
			return string.Format("{0} [{1}]", this.Name ?? "<пусто>", this.ID);
		}
	}

	[Serializable]
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
				return System.IO.Path.GetFullPath(this.Path);
			}
			return System.IO.Path.GetFullPath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.Path));
		}
	}

	[Serializable]
	public class DeviceInfo
	{
		public string Name;
	}

	[Serializable]
	public class DockSettings
	{
		public bool IsVertical = true;
		public int Width = 220;
		public int Height = 100;
	}

	[Serializable]
	public class WindowSettings
	{
		public Point Location = new Point(100, 100);
		public Size Size = new Size(600, 600);
		public bool IsMaximized;
	}

	[Serializable]
	public class Machine
	{
		public string Name;
		public float Volume = 1f;
		public int? LastMixID;
		public WindowSettings Window = new WindowSettings();
		public DockSettings Dock = new DockSettings();
		public DeviceInfo AudioDevice = new DeviceInfo();
	}
}