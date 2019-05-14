using System.Windows.Forms;

namespace AudioMixer
{
	public class WindowController
	{
		private readonly MainForm form;
		private readonly WindowSettings window;
		private readonly DockSettings dock;
		private readonly SplitContainer splitContainer;

		private bool internalChanges;
		private FormWindowState prevFormState;

		public bool IgnoreSplitterMoved;

		public WindowController(MainForm form, SplitContainer splitContainer, WindowSettings window, DockSettings dock)
		{
			this.form = form;
			this.splitContainer = splitContainer;
			this.window = window;
			this.dock = dock;
			this.prevFormState = this.form.WindowState;
		}

		public void Init()
		{
			try
			{
				this.internalChanges = true;

				this.form.Location = this.window.Location;
				this.form.Size = this.window.Size;

				if (this.window.IsMaximized)
				{
					this.form.WindowState = FormWindowState.Maximized;
				}

				this.SetupOrientation();
			}
			finally
			{
				this.internalChanges = false;
			}
		}

		private void SetupOrientation()
		{
			this.IgnoreSplitterMoved = true;
			this.splitContainer.Orientation = this.dock.IsVertical ? Orientation.Vertical : Orientation.Horizontal;
			if (this.dock.IsVertical)
			{
				this.splitContainer.SplitterDistance = this.dock.Width;
			}
			else
			{
				this.splitContainer.SplitterDistance = this.dock.Height;
			}
			this.IgnoreSplitterMoved = false;
		}

		public void OnResize()
		{
			FormWindowState windowState = this.form.WindowState;
			this.form.ShowInTaskbar = windowState != FormWindowState.Minimized;

			bool save = false;
			if (windowState != FormWindowState.Minimized)
			{
				if (this.prevFormState != windowState)
				{
					this.prevFormState = windowState;
					this.window.IsMaximized = windowState == FormWindowState.Maximized;
					save = true;

					if (windowState != FormWindowState.Maximized)
					{
						this.form.Location = this.window.Location;
					}
				}

				if (this.form.WindowState == FormWindowState.Normal && this.window.Size != this.form.Size)
				{
					this.window.Size = this.form.Size;
					save = true;
				}
			}

			if (save)
			{
				Settings.SaveAppearance();
			}
		}

		public void SwitchOrientation()
		{
			this.dock.IsVertical = !this.dock.IsVertical;
			Settings.SaveAppearance();

			this.SetupOrientation();
		}

		public void SplitterMoved()
		{
			if (this.IgnoreSplitterMoved) return;

			if (this.dock.IsVertical)
			{
				if (this.dock.Width != this.splitContainer.SplitterDistance)
				{
					this.dock.Width = this.splitContainer.SplitterDistance;
					Settings.SaveAppearance();
				}
			}
			else
			{
				if (this.dock.Height != this.splitContainer.SplitterDistance)
				{
					this.dock.Height = this.splitContainer.SplitterDistance;
					Settings.SaveAppearance();
				}
			}
		}

		public void LocationChanged()
		{
			if (this.internalChanges) return;
			if (this.form.WindowState != FormWindowState.Normal) return;

			if (this.window.Location != this.form.Location)
			{
				this.window.Location = this.form.Location;
				Settings.SaveAppearance();
			}
		}
	}
}
