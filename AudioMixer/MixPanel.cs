﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common;
using CommonWinForms;
using CommonWinForms.Extensions;

namespace AudioMixer
{
	public partial class MixPanel : UserControl
	{
		public MixPanel()
		{
			this.InitializeComponent();
		}

		public void SetMixInfo(MixInfo mixInfo)
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
		public event EventHandler ContentChanged;

		private MixInfo mixInfo;
		private bool internalChanges;

		public string MixName
		{
			get
			{
				return this.tbName.Text;
			}
		}

		public void RefreshContent()
		{
			this.ClearSoundPanels();
			this.SetMixInfo(this.mixInfo);
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
			using (OpenFileDialog dlg = new OpenFileDialog())
			{
				dlg.Multiselect = multiselect;
				if (dlg.ShowDialog(UIHelper.TopForm) == DialogResult.OK)
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
			}
			return null;
		}

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

			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}

		private readonly Stack<PooledSoundPanel> soundPanelsPool = new Stack<PooledSoundPanel>();
		private readonly Stack<PooledSoundPanel> submixPanelsPool = new Stack<PooledSoundPanel>();
		private readonly List<PooledSoundPanel> soundPanels = new List<PooledSoundPanel>();
		

		private class PooledSoundPanel
		{
			public SoundPanel SoundPanel;
			public SubmixPanel SubmixPanel;
			public Panel Container;
			public GroupBox Line;
		}

		private void AddSound(SoundInfo soundInfo)
		{
			GroupBox line;

			Stack<PooledSoundPanel> pool;
			if (soundInfo.Type == SoundInfo.Types.Mix)
			{
				pool = this.submixPanelsPool;
			}
			else
			{
				pool = this.soundPanelsPool;
			}

			PooledSoundPanel pooledSoundPanel;
			Panel panel;
			if (pool.Count == 0)
			{
				pooledSoundPanel = new PooledSoundPanel();

				line = new GroupBox();
				line.Height = 2;
				line.Dock = DockStyle.Top;

				pooledSoundPanel.Line = line;

				panel = new Panel();
				panel.Dock = DockStyle.Top;
				panel.Padding = new Padding(20, 0, 0, 0);

				if (soundInfo.Type == SoundInfo.Types.Mix)
				{
					SubmixPanel submixPanel = new SubmixPanel();
					submixPanel.Dock = DockStyle.Top;
					panel.Height = submixPanel.Height;
					panel.Controls.Add(submixPanel);

					submixPanel.DeleteButtonClick += this.SoundPanel_DeleteButtonClick;
					submixPanel.VolumeChanged += this.SoundPanel_VolumeChanged;
					submixPanel.PlayChanged += this.SoundPanel_PlayChanged;
					submixPanel.ContentChanged += OnContentChanged;

					pooledSoundPanel.SubmixPanel = submixPanel;
				}
				else
				{
					SoundPanel soundPanel = new SoundPanel();
					soundPanel.Dock = DockStyle.Top;
					panel.Height = soundPanel.Height;
					panel.Controls.Add(soundPanel);

					soundPanel.DeleteButtonClick += this.SoundPanel_DeleteButtonClick;
					soundPanel.VolumeChanged += this.SoundPanel_VolumeChanged;
					soundPanel.PlayChanged += this.SoundPanel_PlayChanged;
					soundPanel.ContentChanged += OnContentChanged;

					pooledSoundPanel.SoundPanel = soundPanel;
				}

				pooledSoundPanel.Container = panel;
			}
			else
			{
				pooledSoundPanel = pool.Pop();
				panel = pooledSoundPanel.Container;
				line = pooledSoundPanel.Line;
			}

			this.soundPanels.Add(pooledSoundPanel);

			// delimeter line
			this.pnlSounds.Controls.Add(line);
			this.pnlSounds.Controls.SetChildIndex(line, 0);

			// sound panel
			if (soundInfo.Type == SoundInfo.Types.Mix)
			{
				pooledSoundPanel.SubmixPanel.SetSoundInfo(soundInfo, this.mixInfo);
			}
			else
			{
				pooledSoundPanel.SoundPanel.SetSoundInfo(soundInfo);
			}
			this.pnlSounds.Controls.Add(panel);
			this.pnlSounds.Controls.SetChildIndex(panel, 0);

			this.AdjustHeights();
		}

		public void ClearSoundPanels()
		{
			foreach (PooledSoundPanel pooledSoundPanel in this.soundPanels)
			{
				this.pnlSounds.Controls.Remove(pooledSoundPanel.Container);
				this.pnlSounds.Controls.Remove(pooledSoundPanel.Line);

				if (pooledSoundPanel.SubmixPanel != null)
				{
					this.submixPanelsPool.Push(pooledSoundPanel);
				}
				else
				{
					this.soundPanelsPool.Push(pooledSoundPanel);
				}
			}
			this.soundPanels.Clear();
		}

		private void SoundPanel_VolumeChanged(object sender, EventArgs e)
		{
			this.VolumeChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SoundPanel_DeleteButtonClick(object sender, EventArgs eventArgs)
		{
			SoundPanel soundPanel = (SoundPanel)sender;
			PooledSoundPanel pooled = this.soundPanels.Find(p => p.SoundPanel == soundPanel);
			if (pooled == null) throw new Exception(nameof(pooled));

			this.pnlSounds.Controls.Remove(pooled.Container);

			Control line = pooled.Line;
			if (line != null)
			{
				this.pnlSounds.Controls.Remove(line);
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
			this.pnlSounds.SetPreferredHeight();
			this.SetPreferredHeight();
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Volume ({0}%):", this.tbVolume.Value);

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

		private void OnContentChanged(object sender, EventArgs e)
		{
			this.ContentChanged?.Invoke(this, EventArgs.Empty);
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

		private void btnAddMix_Click(object sender, EventArgs e)
		{
			SoundInfo soundInfo = new SoundInfo();
			soundInfo.Type = SoundInfo.Types.Mix;
			this.mixInfo.Sounds.Add(soundInfo);
			Settings.SetNeedSave();

			this.AddSound(soundInfo);

			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
