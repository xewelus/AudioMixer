using System;
using System.Windows.Forms;
using Common;

namespace AudioMixer
{
	public partial class MainForm : Form
	{
		private MixPanel mixPanel;
		public MainForm()
		{
			this.InitializeComponent();
		}

		protected override void OnClosed(EventArgs e)
		{
			Settings.Save();
			base.OnClosed(e);
		}

		private void pnlMixes_ItemSelected(object sender, EventArgs e)
		{
			if (this.mixPanel != null)
			{
				this.mixPanel.Remove(true);
				this.mixPanel = null;
			}

			if (this.pnlMixes.SelectedMix != null)
			{
				this.mixPanel = new MixPanel(this.pnlMixes.SelectedMix);
				this.mixPanel.Dock = DockStyle.Fill;
				this.mixPanel.NameChanged += this.MixPanelOnNameChanged;
				this.splitContainer.Panel2.Controls.Add(this.mixPanel);
			}
		}

		private void MixPanelOnNameChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.UpdateName(this.mixPanel.MixName);
		}

		private void saveTimer_Tick(object sender, EventArgs e)
		{
			Settings.Save(true);
		}
	}
}
