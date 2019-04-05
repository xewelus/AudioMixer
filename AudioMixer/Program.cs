using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;

namespace AudioMixer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Settings.Current.AudioDevice = "Device 1";
			Settings.Current.Mixes = new List<MixInfo>
			{
				new MixInfo
				{
					Name = "Супер микс 1",
					Volume = 1f,
					Sounds = new List<SoundInfo>
					{
						new SoundInfo
						{
							IsActive = true,
							Path = "C:/somepath/super.mp3",
							Volume = 0.5f
						},
						new SoundInfo
						{
							IsActive = true,
							Path = "C:/somepath/mega voice.mp3",
							Volume = 1f
						}
					}
				},
				new MixInfo
				{
					Name = "Для полнейшей релаксации",
					Volume = 0.8f,
					Sounds = new List<SoundInfo>
					{
						new SoundInfo
						{
							IsActive = true,
							Path = "C:/somepath/super.mp3",
							Volume = 0.75f
						},
						new SoundInfo
						{
							IsActive = true,
							Path = "C:/somepath/relax.mp3",
							Volume = 1f
						}
					}
				}
			};

			Settings.Save();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UIHelper.MainForm = new MainForm();
			Application.Run(UIHelper.MainForm);
		}
	}
}
