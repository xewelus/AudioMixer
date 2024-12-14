using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AudioMixer.Properties;
using Common;
using CommonWinForms;

namespace AudioMixer
{
	public partial class MixesListPanel : UserControl
	{
		public MixesListPanel()
		{
			this.InitializeComponent();

			this.imageList.Images.Add(Resources.play);
			this.imageList.Images.Add(Resources.music);
			this.imageList.Images.Add(Resources.pause);
			this.imageList.Images.Add(Resources.fav_star);

			this.lvMixes.BeginUpdate();
			this.lvMixes.Items.Clear();
			foreach (MixInfo mixInfo in Settings.Current.Mixes)
			{
				this.AddListItem(mixInfo);
			}
			this.lvMixes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			this.lvMixes.Sort();
			this.lvMixes.EndUpdate();
		}

		public event EventHandler ItemSelected;
		public event EventHandler ItemActivated;
		public event EventHandler DockButtonClick;
		public event EventHandler ThemeButtonClick;
		public event EventHandler SelectedMixChanged;
		public MixInfo SelectedMix;
		public MixInfo ActivatedMix;

		private const int IMAGE_PLAY = 0;
		private const int IMAGE_DEFAULT = 1;
		private const int IMAGE_PAUSE = 2;
		private const int IMAGE_FAVORITE = 3;

		private Orientation panelOrientation = Orientation.Vertical;
		[DefaultValue(Orientation.Vertical)]
		public Orientation PanelOrientation
		{
			get
			{
				return this.panelOrientation;
			}
			set
			{
				this.panelOrientation = value;
				this.btnDock.Image = value == Orientation.Vertical ? Resources.dock_top : Resources.dock_left;
			}
		}

		public void UpdateName(string name)
		{
			ListViewItem item = this.lvMixes.SelectedItems[0];

			MixInfo mixInfo = (MixInfo)item.Tag;
			mixInfo.Name = name;
			Settings.SetNeedSave();

			item.Text = name;

			this.AdjustList(true);
		}

		public void PlayChange()
		{
			this.lvMixes_ItemActivate(null, null);
		}

		private ListViewItem AddListItem(MixInfo mixInfo)
		{
			ListViewItem item = this.lvMixes.Items.Add(mixInfo.Name);
			item.ImageIndex = mixInfo.IsFavorite ? IMAGE_FAVORITE : IMAGE_DEFAULT;
			item.Tag = mixInfo;
			return item;
		}

		private ListViewItem lastActivated;
		private void lvMixes_ItemActivate(object sender, System.EventArgs e)
		{
			this.ActivateItem(this.lvMixes.SelectedItems.Count == 0 ? null : this.lvMixes.SelectedItems[0]);
		}

		public void SelectItemByID(int mixID)
		{
			foreach (ListViewItem item in this.lvMixes.Items)
			{
				MixInfo mixInfo = (MixInfo)item.Tag;
				if (mixInfo.ID == mixID)
				{
					item.Selected = true;
					break;
				}
			}
		}

		private void ActivateItem(ListViewItem item)
		{
			if (this.lastActivated != null)
			{
				this.lastActivated.ImageIndex = IMAGE_DEFAULT;
				this.lastActivated.Font = this.lvMixes.Font;
			}

			if (this.lastActivated == item)
			{
				this.lastActivated = null;
			}
			else
			{
				if (item != null)
				{
					item.ImageIndex = IMAGE_PLAY;
					item.Font = new Font(this.lvMixes.Font, FontStyle.Bold);
				}
				this.lastActivated = item;
			}

			this.AdjustList(true);

			Application.DoEvents();

			this.ActivatedMix = (MixInfo)(this.lastActivated == null ? null : this.lastActivated.Tag);
			if (this.ItemActivated != null)
			{
				this.ItemActivated.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnMixAdd_Click(object sender, EventArgs e)
		{
			List<string> files = MixPanel.AskFiles();
			if (files == null)
			{
				return;
			}

			MixInfo mixInfo = MixInfo.Create();
			mixInfo.Name = Path.GetFileNameWithoutExtension(files[0]);
			foreach (string file in files)
			{
				SoundInfo soundInfo = new SoundInfo();
				soundInfo.Path = file;
				mixInfo.Sounds.Add(soundInfo);
			}
			this.AddNewMix(mixInfo);
		}

		private void AddNewMix(MixInfo mixInfo)
		{
			Settings.Current.Mixes.Add(mixInfo);
			Settings.SetNeedSave();

			ListViewItem item = this.AddListItem(mixInfo);
			item.Selected = true;

			this.AdjustList(true);
		}

		private void lvMixes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lvMixes.SelectedItems.Count == 0)
			{
				this.btnMixDelete.Enabled = false;
				this.SelectedMix = null;
			}
			else
			{
				this.btnMixDelete.Enabled = true;
				this.SelectedMix = (MixInfo)this.lvMixes.SelectedItems[0].Tag;
			}

			if (this.ItemSelected != null)
			{
				this.ItemSelected.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnMixDelete_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem item in this.lvMixes.SelectedItems)
			{
				MixInfo mixInfo = (MixInfo)item.Tag;
				if (UIHelper.AskYesNo(string.Format("Are you sure you want to delete mix '{0}'?", mixInfo.Name)))
				{
					if (this.lastActivated == item)
					{
						this.lastActivated = null;
					}

					this.lvMixes.Items.Remove(item);
					Settings.Current.Mixes.Remove(mixInfo);

					bool hasDeletingMix = this.RemoveMixFromSubmixes(mixInfo);
					if (hasDeletingMix)
					{
						this.SelectedMixChanged?.Invoke(this, EventArgs.Empty);
					}

					Settings.SetNeedSave();
				}
			}
			this.AdjustList();
		}

		private bool RemoveMixFromSubmixes(MixInfo mixInfo)
		{
			bool hasDeletingMix = false;
			foreach (MixInfo mix in Settings.Current.Mixes)
			{
				foreach (SoundInfo sound in mix.Sounds)
				{
					if (sound.MixID == mixInfo.ID)
					{
						mix.Sounds.Remove(sound);
						hasDeletingMix = true;
					}
				}
			}
			return hasDeletingMix;
		}

		private void AdjustList(bool needSort = false)
		{
			this.lvMixes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

			if (needSort)
			{
				this.lvMixes.Sort();
			}
		}

		private void lvMixes_KeyDown(object sender, KeyEventArgs e)
		{
			if (MainForm.IsPlayChangeKey(e))
			{
				this.PlayChange();
			}
		}

		private ListViewItem mouseItem;
		private void lvMixes_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseItem = e.X >= 16 ? null : this.lvMixes.GetItemAt(e.X, e.Y);
			foreach (ListViewItem item in this.lvMixes.Items)
			{
				if (item == null) throw new NullReferenceException(); // bad VS suggestion

				MixInfo mixInfo = (MixInfo)item.Tag;
				if (this.mouseItem == item)
				{
					if (item == this.lastActivated)
					{
						item.ImageIndex = IMAGE_PAUSE;
					}
					else
					{
						item.ImageIndex = IMAGE_PLAY;
					}
				}
				else
				{
					if (item == this.lastActivated)
					{
						item.ImageIndex = IMAGE_PLAY;
					}
					else
					{
						item.ImageIndex = mixInfo.IsFavorite ? IMAGE_FAVORITE : IMAGE_DEFAULT;
					}
				}
			}
		}

		private void lvMixes_MouseClick(object sender, MouseEventArgs e)
		{
			if (this.mouseItem != null)
			{
				this.ActivateItem(this.mouseItem == this.lastActivated ? null : this.mouseItem);
			}
		}

		private void btnDock_Click(object sender, EventArgs e)
		{
			if (this.DockButtonClick != null)
			{
				this.DockButtonClick(this, EventArgs.Empty);
			}
		}

		private void cmList_Opening(object sender, CancelEventArgs e)
		{
			bool selected = this.SelectedMix != null;
			this.miCopy.Enabled = selected;
			this.miDelete.Enabled = selected;
			this.miPlay.Enabled = selected && this.SelectedMix != this.ActivatedMix;
			this.miPause.Enabled = selected && this.SelectedMix == this.ActivatedMix;
			this.miAddToFavorites.Enabled = selected && !this.SelectedMix.IsFavorite;
			this.miRemoveFromFavorites.Enabled = selected && this.SelectedMix.IsFavorite;
		}

		private void miNew_Click(object sender, EventArgs e)
		{
			this.btnMixAdd_Click(this, EventArgs.Empty);
		}

		private void miDelete_Click(object sender, EventArgs e)
		{
			this.btnMixDelete_Click(this, EventArgs.Empty);
		}

		private void miCopy_Click(object sender, EventArgs e)
		{
			MixInfo mix = this.SelectedMix;
			MixInfo newMix = MixInfo.Create();
			newMix.Name = mix.Name + " - копия";
			newMix.Volume = mix.Volume;
			foreach (SoundInfo sound in mix.Sounds)
			{
				SoundInfo newSound = new SoundInfo();
				newSound.Path = sound.Path;
				newSound.Volume = sound.Volume;
				newMix.Sounds.Add(newSound);
			}
			this.AddNewMix(newMix);
		}

		private void miPlay_Click(object sender, EventArgs e)
		{
			this.lvMixes_ItemActivate(this, EventArgs.Empty);
		}

		private void miPause_Click(object sender, EventArgs e)
		{
			this.lvMixes_ItemActivate(this, EventArgs.Empty);
		}

		private void btnDarkMode_Click(object sender, EventArgs e)
		{
			if (this.ThemeButtonClick != null)
			{
				this.ThemeButtonClick(this, EventArgs.Empty);
			}
		}

		private void miAddToFavorites_Click(object sender, EventArgs e)
		{
			if (this.SelectedMix != null)
			{
				this.SelectedMix.IsFavorite = true;
				Settings.SetNeedSave();
				this.RefreshMixIcon(this.lvMixes.SelectedItems[0]);
			}
		}

		private void miRemoveFromFavorites_Click(object sender, EventArgs e)
		{
			if (this.SelectedMix != null)
			{
				this.SelectedMix.IsFavorite = false;
				Settings.SetNeedSave();
				this.RefreshMixIcon(this.lvMixes.SelectedItems[0]);
			}
		}

		private void RefreshMixIcon(ListViewItem item)
		{
			MixInfo mixInfo = (MixInfo)item.Tag;
			if (item == this.lastActivated)
			{
				item.ImageIndex = IMAGE_PLAY;
			}
			else
			{
				item.ImageIndex = mixInfo.IsFavorite ? IMAGE_FAVORITE : IMAGE_DEFAULT;
			}
		}

		public void SwitchToNextFavorite()
		{
			if (this.lvMixes.Items.Count == 0) return;

			int startIndex = this.lvMixes.SelectedIndices.Count > 0 ? this.lvMixes.SelectedIndices[0] : -1;
			int currentIndex = startIndex;

			do
			{
				currentIndex = (currentIndex + 1) % this.lvMixes.Items.Count;
				ListViewItem item = this.lvMixes.Items[currentIndex];
				MixInfo mixInfo = (MixInfo)item.Tag;

				if (mixInfo.IsFavorite)
				{
					item.Selected = true;
					item.EnsureVisible();
					
					this.ActivateItem(item);
					return;
				}
			} while (currentIndex != startIndex);
		}
	}
}
