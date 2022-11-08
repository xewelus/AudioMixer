using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Input;
using CommonWpf.WinForms;
using AudioMixer.Properties;
using CommonWpf.Classes.UI;

namespace AudioMixer
{
	internal class AppNotifyIcon : IDisposable
	{
		private readonly NotifyIcon notifyIcon = new NotifyIcon();
		private readonly NotifyIcon.Item miSysTrayPlay;
		private readonly NotifyIcon.Item miSysTrayStop;
		private readonly NotifyIcon.Item miSysTrayVolume25;
		private readonly NotifyIcon.Item miSysTrayVolume50;
		private readonly NotifyIcon.Item miSysTrayVolume75;
		private readonly NotifyIcon.Item miSysTrayVolume100;

		private readonly WpfMainPanel mainPanel;
		private readonly WpfMixesListPanel pnlMixes;

		public event EventHandler OpenFormClick;
		public event EventHandler CloseClick;

		public AppNotifyIcon(string title, WpfMainPanel mainPanel, WpfMixesListPanel pnlMixes)
		{
			this.mainPanel = mainPanel;
			this.pnlMixes = pnlMixes;

			this.notifyIcon.Icon = Resources.app_play;
			this.notifyIcon.Text = title;
			this.notifyIcon.Click += this.notifyIcon_MouseClick;
			this.notifyIcon.DoubleClick += this.notifyIcon_MouseDoubleClick;
			this.notifyIcon.Opening += this.cmSysTray_Opening;

			this.notifyIcon.AddItem("Открыть", Resources.app_png, this.miOpenForm_Click);

			this.notifyIcon.AddSeparator();

			this.miSysTrayPlay = this.notifyIcon.AddItem("Воспроизвести", Resources.play, this.miSysTrayPlay_Click);
			this.miSysTrayStop = this.notifyIcon.AddItem("Остановить", Resources.pause2, this.miSysTrayStop_Click);

			this.notifyIcon.AddSeparator();

			this.miSysTrayVolume25 = this.notifyIcon.AddItem("Громкость 25%", handler: this.miSysTrayVolume25_Click);
			this.miSysTrayVolume50 = this.notifyIcon.AddItem("Громкость 50%", handler: this.miSysTrayVolume50_Click);
			this.miSysTrayVolume75 = this.notifyIcon.AddItem("Громкость 75%", handler: this.miSysTrayVolume75_Click);
			this.miSysTrayVolume100 = this.notifyIcon.AddItem("Громкость 100%", handler: this.miSysTrayVolume100_Click);

			this.notifyIcon.AddSeparator();

			this.notifyIcon.AddItem("Выход", handler: this.miSysTrayQuit_Click);
		}

		public void SetIcon(Icon icon)
		{
			this.notifyIcon.Icon = icon;
		}

		private bool needPlayChange;
		private async void notifyIcon_MouseClick(object sender, MouseEventArgs e)
		{
			try
			{
				if (e.LeftButton == MouseButtonState.Pressed)
				{
					this.needPlayChange = true;

					await Task.Delay(500);

					if (this.needPlayChange)
					{
						await this.pnlMixes.PlayChange();
					}
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				this.needPlayChange = false;
				this.miOpenForm_Click(null, null);
			}
		}

		private void cmSysTray_Opening(object sender, CancelEventArgs e)
		{
			this.miSysTrayPlay.Enabled = this.pnlMixes.ActivatedMix == null && this.pnlMixes.SelectedMix != null;
			this.miSysTrayStop.Enabled = this.pnlMixes.ActivatedMix != null;

			this.miSysTrayVolume25.Checked = this.mainPanel.Volume == 25;
			this.miSysTrayVolume50.Checked = this.mainPanel.Volume == 50;
			this.miSysTrayVolume75.Checked = this.mainPanel.Volume == 75;
			this.miSysTrayVolume100.Checked = this.mainPanel.Volume == 100;
		}

		private async void miSysTrayPlay_Click(object sender, EventArgs e)
		{
			await this.pnlMixes.PlayChange();
		}

		private async void miSysTrayStop_Click(object sender, EventArgs e)
		{
			await this.pnlMixes.PlayChange();
		}

		private void miSysTrayQuit_Click(object sender, EventArgs e)
		{
			this.CloseClick?.Invoke(this, EventArgs.Empty);
		}

		private void miOpenForm_Click(object sender, EventArgs e)
		{
			this.OpenFormClick?.Invoke(this, EventArgs.Empty);
		}

		private void miSysTrayVolume25_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 25;
		}

		private void miSysTrayVolume50_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 50;
		}

		private void miSysTrayVolume75_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 75;
		}

		private void miSysTrayVolume100_Click(object sender, EventArgs e)
		{
			this.mainPanel.Volume = 100;
		}

		public void Dispose()
		{
			this.notifyIcon.Dispose();
		}
	}
}
