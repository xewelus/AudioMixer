﻿using System;
using System.Drawing;
using System.Windows.Forms;
using AudioMixer.Properties;
using Common;

namespace AudioMixer
{
	public partial class MixesListPanel : UserControl
	{
		public MixesListPanel()
		{
			this.InitializeComponent();

			this.imageList.Images.Add(Resources.play);
			this.imageList.Images.Add(Resources.music);

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
		public MixInfo SelectedMix;
		public MixInfo ActivatedMix;

		public void UpdateName(string name)
		{
			ListViewItem item = this.lvMixes.SelectedItems[0];

			MixInfo mixInfo = (MixInfo)item.Tag;
			mixInfo.Name = name;
			Settings.Save(true);

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
			item.ImageIndex = 1;
			item.Tag = mixInfo;
			return item;
		}

		private ListViewItem lastActivated;
		private void lvMixes_ItemActivate(object sender, System.EventArgs e)
		{
			if (this.lastActivated != null)
			{
				this.lastActivated.ImageIndex = 1;
				this.lastActivated.Font = this.lvMixes.Font;
			}

			ListViewItem item = this.lvMixes.SelectedItems.Count == 0 ? null : this.lvMixes.SelectedItems[0];

			if (this.lastActivated == item)
			{
				this.lastActivated = null;
			}
			else
			{
				if (item != null)
				{
					item.ImageIndex = 0;
					item.Font = new Font(this.lvMixes.Font, FontStyle.Bold);
				}
				this.lastActivated = item;
			}

			this.AdjustList(true);

			this.ActivatedMix = (MixInfo)(this.lastActivated == null ? null : this.lastActivated.Tag);
			if (this.ItemActivated != null)
			{
				this.ItemActivated.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnMixAdd_Click(object sender, EventArgs e)
		{
			MixInfo mixInfo = new MixInfo { Name = "<новый>" };
			Settings.Current.Mixes.Add(mixInfo);
			Settings.Save(true);

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
				if (UIHelper.AskYesNo(string.Format("Вы уверены, что хотите удалить микс '{0}'?", mixInfo.Name)))
				{
					if (this.lastActivated == item)
					{
						this.lastActivated = null;
					}

					this.lvMixes.Items.Remove(item);
					Settings.Current.Mixes.Remove(mixInfo);
					Settings.Save(true);
				}
			}
			this.AdjustList();
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
	}
}
