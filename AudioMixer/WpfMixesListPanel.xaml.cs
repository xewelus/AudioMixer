using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CommonWinForms.Extensions;
using CommonWpf;
using Application = System.Windows.Forms.Application;
using ListView = System.Windows.Controls.ListView;
using ListViewItem = System.Windows.Controls.ListViewItem;
using Orientation = System.Windows.Forms.Orientation;

namespace AudioMixer
{
	public partial class WpfMixesListPanel
	{
		public WpfMixesListPanel()
		{
			this.InitializeComponent();

			if (this.InRuntime())
			{
				this.DataContext = this;
				this.Items = Settings.Current.Mixes.Select(m => new MixInfoItem(m)).ToList();
				//this.lvMixes.GetBindingExpression(ListView.ItemsSourceProperty)?.UpdateTarget();
			}
		}

		public List<MixInfoItem> Items { get; set; }

		public event EventHandler ItemSelected;
		public event EventHandler ItemActivated;
		public event EventHandler DockButtonClick;

		private MixInfoItem selectedItem;
		public MixInfo SelectedMix
		{
			get
			{
				return this.selectedItem?.MixInfo;
			}
		}

		private MixInfoItem activatedItem;
		public MixInfo ActivatedMix
		{
			get
			{
				return this.activatedItem?.MixInfo;
			}
		}

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

				//string res = value == Orientation.Vertical ? "Resources/dock-top.png" : "Resources/dock-dock_left.png";
				//this.imgDock.Source = new BitmapImage(new Uri(res));
			}
		}

		public void UpdateName(string name)
		{
			ListViewItem item = (ListViewItem)this.lvMixes.SelectedItems[0];

			MixInfo mixInfo = (MixInfo)item.Tag;
			mixInfo.Name = name;
			Settings.SetNeedSave();

			//item.Text = name;

			this.AdjustList(true);
		}

		public void PlayChange()
		{
			this.lvMixes_ItemActivate(null, null);
		}

		private ListViewItem lastActivated;
		private void lvMixes_ItemActivate(object sender, System.EventArgs e)
		{
			object item = this.lvMixes.SelectedItems.Count == 0 ? null : this.lvMixes.SelectedItems[0];
			this.ActivateItem((ListViewItem)item);
		}

		public void SelectItemByID(int mixID)
		{
			foreach (ListViewItem item in this.lvMixes.Items)
			{
				MixInfo mixInfo = (MixInfo)item.Tag;
				if (mixInfo.ID == mixID)
				{
					item.IsSelected = true;
					break;
				}
			}
		}

		private void ActivateItem(ListViewItem item)
		{
			if (this.lastActivated != null)
			{
				//this.lastActivated.ImageIndex = IMAGE_DEFAULT;
				this.lastActivated.FontWeight = FontWeights.Normal;
			}

			if (this.lastActivated == item)
			{
				this.lastActivated = null;
			}
			else
			{
				if (item != null)
				{
					//item.ImageIndex = IMAGE_PLAY;
					item.FontWeight = FontWeights.Bold;
				}
				this.lastActivated = item;
			}

			this.AdjustList(true);

			Application.DoEvents();

			this.activatedItem = (MixInfoItem)(this.lastActivated == null ? null : this.lastActivated.Tag);
			if (this.ItemActivated != null)
			{
				this.ItemActivated.Invoke(this, EventArgs.Empty);
			}
		}

		private void btnMixAdd_Click(object sender, EventArgs e)
		{
			List<string> files = WpfMixPanel.AskFiles();
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

			//ListViewItem item = this.AddListItem(mixInfo);
			//item.Selected = true;

			this.AdjustList(true);
		}

		private void lvMixes_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.lvMixes.SelectedItems.Count == 0)
			{
				this.btnMixDelete.IsEnabled = false;
				this.selectedItem = null;
			}
			else
			{
				this.btnMixDelete.IsEnabled = true;
				this.selectedItem = (MixInfoItem)this.lvMixes.SelectedItems[0];
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
			//this.lvMixes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

			//if (needSort)
			//{
			//	this.lvMixes.Sort();
			//}
		}

		private void lvMixes_KeyDown(object sender, KeyEventArgs e)
		{
			if (WpfMainForm.IsPlayChangeKey(e))
			{
				this.PlayChange();
			}
		}

		private ListViewItem mouseItem;
		private void lvMixes_MouseMove(object sender, MouseEventArgs e)
		{
			//this.mouseItem = e.X >= 16 ? null : this.lvMixes.GetItemAt(e.X, e.Y);
			//foreach (ListViewItem item in this.lvMixes.Items)
			//{
			//	if (item == null) throw new NullReferenceException(); // bad VS suggestion

			//	if (this.mouseItem == item)
			//	{
			//		if (item == this.lastActivated)
			//		{
			//			item.ImageIndex = IMAGE_PAUSE;
			//		}
			//		else
			//		{
			//			item.ImageIndex = IMAGE_PLAY;
			//		}
			//	}
			//	else
			//	{
			//		if (item == this.lastActivated)
			//		{
			//			item.ImageIndex = IMAGE_PLAY;
			//		}
			//		else
			//		{
			//			item.ImageIndex = IMAGE_DEFAULT;
			//		}
			//	}
			//}
		}

		private void lvMixes_MouseClick(object sender, MouseEventArgs e)
		{
			//if (this.mouseItem != null)
			//{
			//	this.ActivateItem(this.mouseItem == this.lastActivated ? null : this.mouseItem);
			//}
		}

		private void btnDock_Click(object sender, EventArgs e)
		{
			if (this.DockButtonClick != null)
			{
				this.DockButtonClick(this, EventArgs.Empty);
			}
		}

		private void cmList_Opening(object sender, ContextMenuEventArgs e)
		{
			bool selected = this.SelectedMix != null;
			this.miCopy.IsEnabled = selected;
			this.miDelete.IsEnabled = selected;
			this.miPlay.IsEnabled = selected && this.SelectedMix != this.ActivatedMix;
			this.miPause.IsEnabled = selected && this.SelectedMix == this.ActivatedMix;
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

		public class MixInfoItem
		{
			public readonly MixInfo MixInfo;

			public string Name
			{
				get
				{
					return this.MixInfo.Name;
				}
			}

			public MixInfoItem(MixInfo mixInfo)
			{
				this.MixInfo = mixInfo;
			}
		}
	}
}
