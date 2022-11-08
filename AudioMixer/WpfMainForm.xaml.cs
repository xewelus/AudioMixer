using System;
using System.Windows;
using System.Windows.Input;
using Common;
using CommonWpf;
using Common.InputHooks;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using CommonWpf.Classes.UI;

namespace AudioMixer
{
	public partial class WpfMainForm
	{
		private readonly Machine currentMachine;
		private readonly WindowController windowController;

		private readonly WpfMixesListPanel pnlMixes;
		private readonly AppNotifyIcon notifyIcon;
		
		public WpfMainForm()
		{
			this.InitializeComponent();

			this.pnlMixes = this.mainPanel.MixesListPanel;

			this.Title = string.Format("{0} ({1})", this.Title, AssemblyInfo.VERSION);

			this.notifyIcon = new AppNotifyIcon(this.Title, this.mainPanel, this.pnlMixes);
			this.notifyIcon.OpenFormClick += (_, _) =>
			                                    {
				                                    this.WindowState = WindowState.Normal;
				                                    this.Activate();
												};
			this.notifyIcon.CloseClick += (_, _) => this.Close();

			this.currentMachine = InitMachine();

			this.windowController = new WindowController(this, this.mainPanel.SplitContainer, this.currentMachine.Window, this.currentMachine.Dock);
			this.windowController.Init();

			this.mainPanel.Init(this.currentMachine, this.windowController);
			this.mainPanel.PlayStateChanged += this.mainPanel_PlayStateChanged;

			KeyboardHook.Current.KeyDown += this.keyboardHook_KeyDown;
		}

		private static Machine InitMachine()
		{
			string machineName = Environment.MachineName;
			foreach (Machine machine in Settings.Current.Machines)
			{
				if (machine.Name == machineName)
				{
					return machine;
				}
			}

			Machine newMachine = new Machine();
			newMachine.Name = machineName;
			Settings.Current.Machines.Add(newMachine);
			Settings.SaveAppearance();
			return newMachine;
		}

		private void WpfMainForm_OnClosing(object sender, CancelEventArgs e)
		{
			if (this.mainPanel.NeedSave)
			{
				MessageBoxResult result = UIHelper.ShowMessageBox("Сохранить настройки перед выходом?", buttons: MessageBoxButton.YesNoCancel);
				if (result == MessageBoxResult.Cancel)
				{
					e.Cancel = true;
				}
				else if (result == MessageBoxResult.Yes)
				{
					this.mainPanel.Save();
				}
			}
		}

		private void WpfMainForm_OnClosed(object sender, EventArgs e)
		{
			this.mainPanel.OnClosed();
			this.notifyIcon.Dispose();
		}

		public static bool IsPlayChangeKey(KeyEventArgs e)
		{
			return e.Key.In(Key.Enter, Key.Space);
		}

		public static bool IsPlayChangeKey(Key key)
		{
			return key.In(Key.Enter, Key.Space);
		}

		private void WpfMainForm_OnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			if (this.currentMachine == null)
			{
				return;
			}

			this.windowController.IgnoreSplitterMoved = true;
			//base.OnResize(e);
			this.windowController.IgnoreSplitterMoved = false;

			this.windowController.OnResize();
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			if (this.windowController != null)
			{
				this.windowController.LocationChanged();
			}
		}

		private void mainPanel_PlayStateChanged(object sender, EventArgs e)
		{
			Icon icon = this.pnlMixes.ActivatedMix == null ? Properties.Resources.app_stop : Properties.Resources.app_play;
			this.notifyIcon.SetIcon(icon);
		}

		private Stopwatch keySw;
		private void keyboardHook_KeyDown(KeyboardHook sender, KeyHandlerEventArgs e)
		{
			try
			{
				if (e.KeyCode != (int)Key.F2) return;

				if (this.keySw == null)
				{
					this.keySw = Stopwatch.StartNew();
					return;
				}

				if (this.keySw.ElapsedMilliseconds < 500)
				{
					this.pnlMixes.PlayChange();
				}
				this.keySw = null;
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}


	}
}
