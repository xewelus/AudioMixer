using System;
using System.IO;
using System.Windows;
using Common;
using CommonWinForms;
using CommonWpf.Classes;
using CommonWpf.Classes.UI;

namespace AudioMixer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			if (Misc.IsAlreadyStarted("0A2E00D5-8D96-467B-8E0F-2B032EA916EA"))
			{
				return;
			}

			AppInitializer.Initialize();
			CommonWpf.UIHelper.InRuntime = true;

			ExcHandler.OnError = OnError;
			ExceptionHandler.OnError = OnError;

			try
			{
				Settings.Load();
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}
		}

		private static void OnError(Exception exception)
		{
			string path = FS.GetAppPath("errors.log");
			File.AppendAllText(path, $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff}] {exception}\r\n\r\n");
		}
	}
}
