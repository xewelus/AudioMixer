﻿using System;
using System.Linq;
using System.Windows.Forms;
using Common;

namespace AudioMixer
{
	public partial class SoundPanel : UserControl
	{
		public SoundPanel()
		{
			this.InitializeComponent();
		}

		public SoundPanel(SoundInfo soundInfo) : this()
		{
			this.SoundInfo = soundInfo;

			this.internalChanges = true;

			this.tbFile.Text = soundInfo.Path;
			this.tbVolume.Value = (int)(soundInfo.Volume * 100);
			this.tbVolume_Scroll(null, null);

			this.internalChanges = false;
		}

		public readonly SoundInfo SoundInfo;
		public event EventHandler DeleteButtonClick;
		private bool internalChanges;

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteButtonClick != null)
			{
				this.DeleteButtonClick.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				if (dlg.ShowDialog(UIHelper.TopForm) == DialogResult.OK)
				{
					this.tbFile.Text = dlg.FileName;
					Settings.Save(true);
				}
			}
		}

		private void tbVolume_Scroll(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Громкость ({0}%):", this.tbVolume.Value);
			if (!this.internalChanges)
			{
				this.SoundInfo.Volume = this.tbVolume.Value / 100f;
				Settings.Save(true);
			}
		}

		private void tbFile_TextChanged(object sender, EventArgs e)
		{
			if (!this.internalChanges)
			{
				this.SoundInfo.Path = this.tbFile.Text;
				Settings.Save(true);
			}
		}
	}
}
