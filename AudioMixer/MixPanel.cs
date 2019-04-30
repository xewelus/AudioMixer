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

			this.internalChanges = true;

			this.tbName.Text = mixInfo.Name;
			this.tbVolume.Value = (int)(mixInfo.Volume * 100);
			this.tbVolume_ValueChanged(null, null);

			this.pnlSounds.SuspendLayout();
			foreach (SoundInfo soundInfo in mixInfo.Sounds)
			{
				this.AddSound(soundInfo);
			}
			this.pnlSounds.ResumeLayout();

			this.internalChanges = false;
		}

		public event EventHandler NameChanged;
		public event EventHandler VolumeChanged;
		public event EventHandler PlayChanged;
		private readonly MixInfo mixInfo;
		private bool internalChanges;

		public string MixName
		{
			get
			{
				return this.tbName.Text;
			}
		}

		private void tbName_TextChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;

			if (this.NameChanged != null)
			{
				this.NameChanged.Invoke(this, EventArgs.Empty);
			}
		}

		private readonly Dictionary<SoundPanel, Control> lines = new Dictionary<SoundPanel, Control>();
		private void btnAdd_Click(object sender, EventArgs e)
		{
			string file = SoundPanel.AskFile();
			if (file == null)
			{
				return;
			}

			SoundInfo soundInfo = new SoundInfo();
			soundInfo.Path = file;
			this.mixInfo.Sounds.Add(soundInfo);
			Settings.SetNeedSave();

			this.AddSound(soundInfo);
		}

		private void AddSound(SoundInfo soundInfo)
		{
			Control line = this.AddLine();

			SoundPanel soundPanel = new SoundPanel(soundInfo);
			soundPanel.Dock = DockStyle.Top;
			soundPanel.DeleteButtonClick += this.SoundPanel_DeleteButtonClick;
			soundPanel.VolumeChanged += this.SoundPanel_VolumeChanged;
			soundPanel.PlayChanged += this.SoundPanel_PlayChanged;

			this.pnlSounds.Controls.Add(soundPanel);
			this.pnlSounds.Controls.SetChildIndex(soundPanel, 0);

			this.AdjustHeights();

			this.lines.Add(soundPanel, line);
		}

		private void SoundPanel_VolumeChanged(object sender, EventArgs e)
		{
			if (this.VolumeChanged != null)
			{
				this.VolumeChanged.Invoke(this, EventArgs.Empty);
			}
		}

		private void SoundPanel_DeleteButtonClick(object sender, EventArgs eventArgs)
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

			this.mixInfo.Sounds.Remove(soundPanel.SoundInfo);
			Settings.SetNeedSave();
		}

		private void SoundPanel_PlayChanged(object sender, EventArgs e)
		{
			if (this.PlayChanged != null)
			{
				this.PlayChanged.Invoke(this, EventArgs.Empty);
			}
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

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Громкость ({0}%):", this.tbVolume.Value);

			if (!this.internalChanges)
			{
				this.mixInfo.Volume = this.tbVolume.Value / 100f;

				if (this.VolumeChanged != null)
				{
					this.VolumeChanged.Invoke(this, EventArgs.Empty);
				}

				Settings.SetNeedSave();
			}
		}

		private void controls_KeyDown(object sender, KeyEventArgs e)
		{
			if (MainForm.IsPlayChangeKey(e))
			{
				if (this.PlayChanged != null)
				{
					this.PlayChanged.Invoke(this, EventArgs.Empty);
				}
			}
		}
	}
}
