using System;
using System.Collections.Generic;
using System.Threading;
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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UIHelper.SetUnhandledExceptionSafe();

			try
			{
				// TestSaveSettings();
				Settings.Load();
				UIHelper.MainForm = new MainForm();
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}

			Application.Run(UIHelper.MainForm);
		}

		private static void TestSaveSettings()
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
							Path = "C:/somepath/super.mp3",
							Volume = 0.5f
						},
						new SoundInfo
						{
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
							Path = "C:/somepath/super.mp3",
							Volume = 0.75f
						},
						new SoundInfo
						{
							Path = "C:/somepath/relax.mp3",
							Volume = 1f
						}
					}
				}
			};

			Settings.Save();
		}
	}
}
