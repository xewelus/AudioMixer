using System;
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
			if (Misc.IsSameProcessExists())
			{
				while (true)
				{
					Application.DoEvents();
				}
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
