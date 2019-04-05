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
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			UIHelper.MainForm = new MainForm();
			Application.Run(UIHelper.MainForm);
		}
	}
}
