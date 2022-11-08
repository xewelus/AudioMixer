using System.Windows;
using System.Windows.Controls;

namespace AudioMixer
{
	public class WindowController
	{
		private readonly Window form;
		private readonly WindowSettings window;
		private readonly DockSettings dock;
		private readonly SplitContainer splitContainer;

		private bool internalChanges;
		private WindowState prevFormState;

		public bool IgnoreSplitterMoved;

		public WindowController(Window form, SplitContainer splitContainer, WindowSettings window, DockSettings dock)
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

				this.form.Left = this.window.Location.X;
				this.form.Top = this.window.Location.Y;
				this.form.Width = this.window.Size.Width;
				this.form.Height = this.window.Size.Height;

				if (this.window.IsMaximized)
				{
					this.form.WindowState = WindowState.Maximized;
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
			WindowState windowState = this.form.WindowState;
			this.form.ShowInTaskbar = windowState != WindowState.Minimized;

			bool save = false;
			if (windowState != WindowState.Minimized)
			{
				if (this.prevFormState != windowState)
				{
					this.prevFormState = windowState;
					this.window.IsMaximized = windowState == WindowState.Maximized;
					save = true;

					if (windowState != WindowState.Maximized)
					{
						this.form.Left = this.window.Location.X;
						this.form.Top = this.window.Location.Y;
					}
				}

				System.Drawing.Size formSize = new System.Drawing.Size((int)this.form.Width, (int)this.form.Height);
				if (this.form.WindowState == WindowState.Normal && this.window.Size != formSize)
				{
					this.window.Size = formSize;
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
			if (this.form.WindowState != WindowState.Normal) return;

			System.Drawing.Point location = new System.Drawing.Point((int)this.form.Left, (int)this.form.Top);
			if (this.window.Location != location)
			{
				this.window.Location = location;
				Settings.SaveAppearance();
			}
		}
	}
}
