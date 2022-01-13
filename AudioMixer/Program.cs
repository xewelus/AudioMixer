using System;
using System.Windows.Forms;
using System.Xml.Linq;
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
			if (Misc.IsAlreadyStarted("0A2E00D5-8D96-467B-8E0F-2B032EA916EA"))
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UIHelper.SetUnhandledExceptionSafe();

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
	}
}
