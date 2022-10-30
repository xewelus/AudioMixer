﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Common;
using CommonWpf.Classes.UI;

namespace AudioMixer
{
	/// <summary>
	/// Interaction logic for WpfSoundPanel.xaml
	/// </summary>
	public partial class WpfSoundPanel
	{
		public WpfSoundPanel()
		{
			this.InitializeComponent();
		}

		public WpfSoundPanel(SoundInfo soundInfo) : this()
		{
			this.SoundInfo = soundInfo;

			this.internalChanges = true;

			this.tbFile.Text = soundInfo.Path;
			this.RefreshFileState();

			this.tbVolume.Value = (int)(soundInfo.Volume * 100);
			this.tbVolume_ValueChanged(null, null);

			this.internalChanges = false;
		}

		public readonly SoundInfo SoundInfo;
		public event EventHandler DeleteButtonClick;
		public event EventHandler VolumeChanged;
		public event EventHandler PlayChanged;
		public event EventHandler ContentChanged;

		private readonly bool internalChanges = true;

		private void btnDelete_Click(object sender, EventArgs e)
		{
			if (this.DeleteButtonClick != null)
			{
				this.DeleteButtonClick.Invoke(this, EventArgs.Empty);
			}
		}

		private static string AskFile()
		{
			List<string> files = MixPanel.AskFiles(false);
			if (files == null || files.Count == 0)
			{
				return null;
			}
			return files[0];
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			string file = AskFile();
			if (file != null)
			{
				this.tbFile.Text = file;
				this.RefreshFileState();
			}
		}

		private void tbVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				this.lblVolume.Content = string.Format("Громкость ({0:0}%):", this.tbVolume.Value);
				if (!this.internalChanges)
				{
					this.SoundInfo.Volume = (float)this.tbVolume.Value / 100f;

					if (this.VolumeChanged != null)
					{
						this.VolumeChanged.Invoke(this, EventArgs.Empty);
					}

					Settings.SetNeedSave();
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.Catch(ex);
			}
		}

		private void tbFile_TextChanged(object sender, EventArgs e)
		{
			if (!this.internalChanges)
			{
				this.SoundInfo.Path = this.tbFile.Text;
				Settings.SetNeedSave();
			}

			this.cbRelativeIgnore = true;
			if (Path.IsPathRooted(this.tbFile.Text))
			{
				this.cbRelative.IsChecked = false;
				this.cbRelative.IsEnabled = !FS.IsAnotherDrive(this.tbFile.Text, AppDomain.CurrentDomain.BaseDirectory);
			}
			else
			{
				this.cbRelative.IsChecked = true;
				this.cbRelative.IsEnabled = true;
			}
			this.cbRelativeIgnore = false;
			this.RefreshFileState();
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

		private bool cbRelativeIgnore;
		private void cbRelative_CheckedChanged(object sender, EventArgs e)
		{
			if (this.cbRelativeIgnore) return;

			string path = this.GetFilePath();

			if (this.cbRelative.IsChecked == true)
			{
				this.tbFile.Text = FS.GetRelativePath(path, AppDomain.CurrentDomain.BaseDirectory);
			}
			else
			{
				this.tbFile.Text = path;
			}
			this.RefreshFileState();
		}

		private string GetFilePath()
		{
			string path = this.tbFile.Text;
			if (!Path.IsPathRooted(this.tbFile.Text))
			{
				path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, this.tbFile.Text));
			}
			return path;
		}

		private void RefreshFileState()
		{
			string path = this.GetFilePath();
			bool exists = File.Exists(path);
			this.tbFile.Foreground = new SolidColorBrush(exists ? Colors.Black : Colors.Red);

			if (exists)
			{
				this.ContentChanged?.Invoke(this, EventArgs.Empty);
			}
		}
	}
}
