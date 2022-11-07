using System;
using CommonWpf.Classes;
using CommonWpf.Classes.UI;

namespace AudioMixer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App
	{
		public App()
		{
			bool ok = AppInitializer.Initialize(
				needFileLog: true,
				singleInstance: true,
				needKeyboardHook: true);

			if (!ok) return;

			try
			{
				Settings.Load();
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}
	}
}