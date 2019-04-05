using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common;

namespace AudioMixer
{
	public partial class MixPanel : UserControl
	{
		public MixPanel()
		{
			this.InitializeComponent();
		}

		public MixPanel(MixInfo mixInfo) : this()
		{
			this.mixInfo = mixInfo;

			this.tbName.Text = mixInfo.Name;
			this.tbVolume.Value = (int)(mixInfo.Volume * 100);
			this.tbVolume_Scroll(null, null);
		}

		public event EventHandler NameChanged;
		private readonly MixInfo mixInfo;

		public string MixName
		{
			get
			{
				return this.tbName.Text;
			}
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
			if (this.NameChanged != null)
			{
				this.NameChanged.Invoke(this, EventArgs.Empty);
			}
		}

		private readonly Dictionary<SoundPanel, Control> lines = new Dictionary<SoundPanel, Control>();
		private void btnAdd_Click(object sender, EventArgs e)
		{
			Control line = this.AddLine();

			SoundPanel soundPanel = new SoundPanel();
			soundPanel.Dock = DockStyle.Top;
			soundPanel.DeleteButtonClick += this.SoundPanelOnDeleteButtonClick;

			this.pnlSounds.Controls.Add(soundPanel);
			this.pnlSounds.Controls.SetChildIndex(soundPanel, 0);

			this.AdjustHeights();

			this.lines.Add(soundPanel, line);
		}

		private void SoundPanelOnDeleteButtonClick(object sender, EventArgs eventArgs)
		{
			SoundPanel soundPanel = (SoundPanel)sender;
			this.pnlSounds.Controls.Remove(soundPanel);

			Control line = this.lines.Get(soundPanel);
			if (line != null)
			{
				this.pnlSounds.Controls.Remove(line);
				this.lines.Remove(soundPanel);
			}

			this.AdjustHeights();
		}

		private void AdjustHeights()
		{
			this.pnlSounds.SetPreferredHeight();
			this.SetPreferredHeight();
		}

		private Control AddLine()
		{
			GroupBox line = new GroupBox();
			line.Height = 2;
			line.Dock = DockStyle.Top;
			this.pnlSounds.Controls.Add(line);
			this.pnlSounds.Controls.SetChildIndex(line, 0);
			return line;
		}

		private void tbVolume_Scroll(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Громкость ({0}%):", this.tbVolume.Value);
			this.mixInfo.Volume = this.tbVolume.Value / 100f;
			Settings.Save(true);
		}
	}
}
