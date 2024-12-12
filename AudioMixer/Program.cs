using System;
using System.IO;
using System.Windows.Forms;
using Common;
using CommonWinForms;

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
			//todo: [high] restore mutex behaviour

			//if (Misc.IsAlreadyStarted("0A2E00D5-8D96-467B-8E0F-2B032EA916EA"))
			//{
			//	return;
			//}

			UIHelper.IsDarkMode = true;

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UIHelper.SetUnhandledExceptionSafe();

			ExcHandler.OnError = OnError;

			try
			{
				Settings.Load();
				UIHelper.MainForm = new MainForm();
			}
			catch (Exception ex)
			{
				ExcHandler.Catch(ex);
			}

			Application.Run(UIHelper.MainForm);
		}

		private static void OnError(Exception exception)
		{
			#if DEBUG
			string exePath = Application.ExecutablePath;
			string projPath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(exePath), "../../../../"));
			string logPath = Path.Combine(projPath, "last_error.log");
			File.WriteAllText(logPath, $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff}]\r\n{exception}\r\n\r\n");
			#endif

			string path = FS.GetAppPath("errors.log");
			File.AppendAllText(path, $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss.fff}] {exception}\r\n\r\n");
		}
	}
}
