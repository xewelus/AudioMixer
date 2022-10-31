using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common;
using CommonWpf;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace AudioMixer
{
	public partial class WpfMixPanel
	{
		public WpfMixPanel()
		{
			this.InitializeComponent();
		}

		public WpfMixPanel(MixInfo mixInfo) : this()
		{
			this.mixInfo = mixInfo;

			this.internalChanges = true;

			this.tbName.Text = mixInfo.Name;
			this.tbVolume.Value = (int)(mixInfo.Volume * 100);
			this.tbVolume_ValueChanged(null, null);

			using (this.pnlSounds.Dispatcher.DisableProcessing())
			{
				this.pnlSounds.Children.RemoveAt(0);

				foreach (SoundInfo soundInfo in mixInfo.Sounds)
				{
					this.AddSound(soundInfo);
				}
			}

			this.internalChanges = false;
		}

		public event EventHandler NameChanged;
		public event EventHandler VolumeChanged;
		public event EventHandler PlayChanged;
		public event EventHandler ContentChanged;

		private readonly MixInfo mixInfo;
		private readonly bool internalChanges;

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

		public static List<string> AskFiles(bool multiselect = true)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Multiselect = multiselect;
			if (dlg.ShowDialog(UIHelper.TopForm) == true)
			{
				List<string> result = new List<string>();
				string dir = AppDomain.CurrentDomain.BaseDirectory;

				foreach (string file in dlg.FileNames)
				{
					string filename = file;
					if (filename.StartsWith(dir))
					{
						filename = filename.Substring(dir.Length);
					}
					result.Add(filename);
				}
				return result;
			}
			return null;
		}

		private readonly Dictionary<WpfSoundPanel, Control> lines = new Dictionary<WpfSoundPanel, Control>();
		private void btnAdd_Click(object sender, EventArgs e)
		{
			List<string> files = AskFiles();
			if (files == null)
			{
				return;
			}

			foreach (string file in files)
			{
				SoundInfo soundInfo = new SoundInfo();
				soundInfo.Path = file;
				this.mixInfo.Sounds.Add(soundInfo);
				Settings.SetNeedSave();

				this.AddSound(soundInfo);
			}
		}

		private void AddSound(SoundInfo soundInfo)
		{
			Control line = this.AddLine();

			WpfSoundPanel soundPanel = new WpfSoundPanel(soundInfo);
			soundPanel.DeleteButtonClick += this.SoundPanel_DeleteButtonClick;
			soundPanel.VolumeChanged += this.SoundPanel_VolumeChanged;
			soundPanel.PlayChanged += this.SoundPanel_PlayChanged;
			soundPanel.ContentChanged += this.SoundPanelOnContentChanged;
			soundPanel.Padding = new Thickness(20, 0, 0, 0);
			DockPanel.SetDock(soundPanel, Dock.Top);

			this.pnlSounds.Children.Add(soundPanel);

			this.AdjustHeights();

			this.lines.Add(soundPanel, line);

			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SoundPanel_VolumeChanged(object sender, EventArgs e)
		{
			this.VolumeChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SoundPanel_DeleteButtonClick(object sender, EventArgs eventArgs)
		{
			WpfSoundPanel soundPanel = (WpfSoundPanel)sender;
			this.pnlSounds.Children.Remove(soundPanel);

			Control line = this.lines.Get(soundPanel);
			if (line != null)
			{
				this.pnlSounds.Children.Remove(line);
				this.lines.Remove(soundPanel);
			}

			this.AdjustHeights();

			this.mixInfo.Sounds.Remove(soundPanel.SoundInfo);
			Settings.SetNeedSave();

			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SoundPanel_PlayChanged(object sender, EventArgs e)
		{
			this.PlayChanged?.Invoke(this, EventArgs.Empty);
		}

		private void AdjustHeights()
		{
			//this.pnlSounds.SetPreferredHeight();
			//this.SetPreferredHeight();
		}

		private Control AddLine()
		{
			Separator line = new Separator();
			line.Height = 2;
			DockPanel.SetDock(line, Dock.Top);
			this.pnlSounds.Children.Add(line);
			return line;
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Content = string.Format("Громкость ({0:0}%):", this.tbVolume.Value);

			if (!this.internalChanges)
			{
				this.mixInfo.Volume = (float)(this.tbVolume.Value / 100f);

				if (this.VolumeChanged != null)
				{
					this.VolumeChanged.Invoke(this, EventArgs.Empty);
				}

				Settings.SetNeedSave();
			}
		}

		private void SoundPanelOnContentChanged(object sender, EventArgs e)
		{
			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}

		private void controls_KeyDown(object sender, KeyEventArgs e)
		{
			if (MainForm.IsPlayChangeKey(e.Key))
			{
				if (this.PlayChanged != null)
				{
					this.PlayChanged.Invoke(this, EventArgs.Empty);
				}
			}
		}
	}
}
