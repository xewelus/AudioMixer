namespace AudioMixer
{
	partial class MixesListPanel
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			lvMixes = new ListView();
			columnHeader1 = new ColumnHeader();
			cmList = new ContextMenuStrip(components);
			miNew = new ToolStripMenuItem();
			miCopy = new ToolStripMenuItem();
			miDelete = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			miPlay = new ToolStripMenuItem();
			miPause = new ToolStripMenuItem();
			imageList = new ImageList(components);
			tsMixes = new ToolStrip();
			btnMixDelete = new ToolStripButton();
			btnMixAdd = new ToolStripButton();
			toolStripSeparator1 = new ToolStripSeparator();
			btnDock = new ToolStripButton();
			btnDarkMode = new ToolStripButton();
			cmList.SuspendLayout();
			tsMixes.SuspendLayout();
			SuspendLayout();
			// 
			// lvMixes
			// 
			lvMixes.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
			lvMixes.ContextMenuStrip = cmList;
			lvMixes.Dock = DockStyle.Fill;
			lvMixes.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
			lvMixes.FullRowSelect = true;
			lvMixes.HeaderStyle = ColumnHeaderStyle.None;
			lvMixes.Location = new Point(0, 25);
			lvMixes.Margin = new Padding(4, 3, 4, 3);
			lvMixes.MultiSelect = false;
			lvMixes.Name = "lvMixes";
			lvMixes.Size = new Size(302, 365);
			lvMixes.SmallImageList = imageList;
			lvMixes.Sorting = SortOrder.Ascending;
			lvMixes.TabIndex = 3;
			lvMixes.UseCompatibleStateImageBehavior = false;
			lvMixes.View = View.Details;
			lvMixes.ItemActivate += lvMixes_ItemActivate;
			lvMixes.SelectedIndexChanged += lvMixes_SelectedIndexChanged;
			lvMixes.KeyDown += lvMixes_KeyDown;
			lvMixes.MouseClick += lvMixes_MouseClick;
			lvMixes.MouseMove += lvMixes_MouseMove;
			// 
			// columnHeader1
			// 
			columnHeader1.Width = 100;
			// 
			// cmList
			// 
			cmList.Items.AddRange(new ToolStripItem[] { miNew, miCopy, miDelete, toolStripSeparator2, miPlay, miPause });
			cmList.Name = "cmList";
			cmList.Size = new Size(125, 120);
			cmList.Opening += cmList_Opening;
			// 
			// miNew
			// 
			miNew.Image = Properties.Resources.plus;
			miNew.Name = "miNew";
			miNew.Size = new Size(124, 22);
			miNew.Text = "New";
			miNew.Click += miNew_Click;
			// 
			// miCopy
			// 
			miCopy.Image = Properties.Resources.copy;
			miCopy.Name = "miCopy";
			miCopy.Size = new Size(124, 22);
			miCopy.Text = "Duplicate";
			miCopy.Click += miCopy_Click;
			// 
			// miDelete
			// 
			miDelete.Image = Properties.Resources.close;
			miDelete.Name = "miDelete";
			miDelete.Size = new Size(124, 22);
			miDelete.Text = "Delete";
			miDelete.Click += miDelete_Click;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(121, 6);
			// 
			// miPlay
			// 
			miPlay.Image = Properties.Resources.play;
			miPlay.Name = "miPlay";
			miPlay.Size = new Size(124, 22);
			miPlay.Text = "Play";
			miPlay.Click += miPlay_Click;
			// 
			// miPause
			// 
			miPause.Image = Properties.Resources.pause2;
			miPause.Name = "miPause";
			miPause.Size = new Size(124, 22);
			miPause.Text = "Stop";
			miPause.Click += miPause_Click;
			// 
			// imageList
			// 
			imageList.ColorDepth = ColorDepth.Depth8Bit;
			imageList.ImageSize = new Size(16, 16);
			imageList.TransparentColor = Color.Transparent;
			// 
			// tsMixes
			// 
			tsMixes.GripStyle = ToolStripGripStyle.Hidden;
			tsMixes.Items.AddRange(new ToolStripItem[] { btnMixDelete, btnMixAdd, toolStripSeparator1, btnDock, btnDarkMode });
			tsMixes.Location = new Point(0, 0);
			tsMixes.Name = "tsMixes";
			tsMixes.RightToLeft = RightToLeft.Yes;
			tsMixes.Size = new Size(302, 25);
			tsMixes.TabIndex = 2;
			// 
			// btnMixDelete
			// 
			btnMixDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
			btnMixDelete.Enabled = false;
			btnMixDelete.Image = Properties.Resources.minus;
			btnMixDelete.ImageTransparentColor = Color.Magenta;
			btnMixDelete.Name = "btnMixDelete";
			btnMixDelete.Size = new Size(23, 22);
			btnMixDelete.Text = "Delete";
			btnMixDelete.Click += btnMixDelete_Click;
			// 
			// btnMixAdd
			// 
			btnMixAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
			btnMixAdd.Image = Properties.Resources.plus;
			btnMixAdd.ImageTransparentColor = Color.Magenta;
			btnMixAdd.Name = "btnMixAdd";
			btnMixAdd.Size = new Size(23, 22);
			btnMixAdd.Text = "New";
			btnMixAdd.Click += btnMixAdd_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(6, 25);
			// 
			// btnDock
			// 
			btnDock.DisplayStyle = ToolStripItemDisplayStyle.Image;
			btnDock.Image = Properties.Resources.dock_top;
			btnDock.ImageTransparentColor = Color.Magenta;
			btnDock.Name = "btnDock";
			btnDock.Size = new Size(23, 22);
			btnDock.Text = "Change window position";
			btnDock.Click += btnDock_Click;
			// 
			// btnDarkMode
			// 
			btnDarkMode.DisplayStyle = ToolStripItemDisplayStyle.Image;
			btnDarkMode.Image = Properties.Resources.icons8_moon_symbol_16;
			btnDarkMode.ImageTransparentColor = Color.Magenta;
			btnDarkMode.Name = "btnDarkMode";
			btnDarkMode.Size = new Size(23, 22);
			btnDarkMode.Text = "Switch theme";
			btnDarkMode.Click += btnDarkMode_Click;
			// 
			// MixesListPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(lvMixes);
			Controls.Add(tsMixes);
			Margin = new Padding(4, 3, 4, 3);
			Name = "MixesListPanel";
			Size = new Size(302, 390);
			cmList.ResumeLayout(false);
			tsMixes.ResumeLayout(false);
			tsMixes.PerformLayout();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ListView lvMixes;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolStrip tsMixes;
		private System.Windows.Forms.ToolStripButton btnMixDelete;
		private System.Windows.Forms.ToolStripButton btnMixAdd;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton btnDock;
		private System.Windows.Forms.ContextMenuStrip cmList;
		private System.Windows.Forms.ToolStripMenuItem miNew;
		private System.Windows.Forms.ToolStripMenuItem miCopy;
		private System.Windows.Forms.ToolStripMenuItem miDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem miPlay;
		private System.Windows.Forms.ToolStripMenuItem miPause;
		private ToolStripButton btnDarkMode;
	}
}
