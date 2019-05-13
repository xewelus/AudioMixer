using System;
using System.ComponentModel;
using System.Diagnostics;
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
			this.imageList.Images.Add(Resources.pause);

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
		public MixInfo SelectedMix;
		public MixInfo ActivatedMix;

		private const int IMAGE_PLAY = 0;
		private const int IMAGE_DEFAULT = 1;
		private const int IMAGE_PAUSE = 2;

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
			item.ImageIndex = 1;
			item.Tag = mixInfo;
			return item;
		}

		private ListViewItem lastActivated;
		private void lvMixes_ItemActivate(object sender, System.EventArgs e)
		{
			this.ActivateItem(this.lvMixes.SelectedItems.Count == 0 ? null : this.lvMixes.SelectedItems[0]);
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
			this.AddNewMix(new MixInfo { Name = "<новый>" });
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
				if (UIHelper.AskYesNo(string.Format("Вы уверены, что хотите удалить микс '{0}'?", mixInfo.Name)))
				{
					if (this.lastActivated == item)
					{
						this.lastActivated = null;
					}

					this.lvMixes.Items.Remove(item);
					Settings.Current.Mixes.Remove(mixInfo);
					Settings.SetNeedSave();
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

		private ListViewItem mouseItem;
		private void lvMixes_MouseMove(object sender, MouseEventArgs e)
		{
			this.mouseItem = e.X >= 16 ? null : this.lvMixes.GetItemAt(e.X, e.Y);
			foreach (ListViewItem item in this.lvMixes.Items)
			{
				if (item == null) throw new NullReferenceException(); // bad VS suggestion

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
						item.ImageIndex = IMAGE_DEFAULT;
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
			MixInfo newMix = new MixInfo();
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
	}
}
