using System;
using System.IO;
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
			this.tbVolume_ValueChanged(null, null);

			this.internalChanges = false;
		}

		public readonly SoundInfo SoundInfo;
		public event EventHandler DeleteButtonClick;
		public event EventHandler VolumeChanged;
		public event EventHandler PlayChanged;
		private readonly bool internalChanges;

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteButtonClick != null)
			{
				this.DeleteButtonClick.Invoke(this, EventArgs.Empty);
			}
		}

		public static string AskFile()
		{
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				if (dlg.ShowDialog(UIHelper.TopForm) == DialogResult.OK)
				{
					string dir = AppDomain.CurrentDomain.BaseDirectory;
					string filename = dlg.FileName;
					if (filename.StartsWith(dir))
					{
						filename = filename.Substring(dir.Length);
					}
					return filename;
				}
				return null;
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			string file = AskFile();
			if (file != null)
			{
				this.tbFile.Text = file;
			}
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Громкость ({0}%):", this.tbVolume.Value);
			if (!this.internalChanges)
			{
				this.SoundInfo.Volume = this.tbVolume.Value / 100f;

				if (this.VolumeChanged != null)
				{
					this.VolumeChanged.Invoke(this, EventArgs.Empty);
				}

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

			this.cbRelativeIgnore = true;
			if (Path.IsPathRooted(this.tbFile.Text))
			{
				this.cbRelative.Checked = false;
				this.cbRelative.Enabled = !FS.IsAnotherDrive(this.tbFile.Text, AppDomain.CurrentDomain.BaseDirectory);
			}
			else
			{
				this.cbRelative.Checked = true;
				this.cbRelative.Enabled = true;
			}
			this.cbRelativeIgnore = false;
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

		private bool cbRelativeIgnore;
		private void cbRelative_CheckedChanged(object sender, EventArgs e)
		{
			if (this.cbRelativeIgnore) return;

			string path = this.tbFile.Text;
			if (!Path.IsPathRooted(this.tbFile.Text))
			{
				path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.tbFile.Text));
			}

			if (this.cbRelative.Checked)
			{
				this.tbFile.Text = FS.GetRelativePath(path, AppDomain.CurrentDomain.BaseDirectory);
			}
			else
			{
				this.tbFile.Text = path;
			}
		}
	}
}
