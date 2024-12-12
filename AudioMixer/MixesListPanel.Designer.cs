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
			this.components = new System.ComponentModel.Container();
			this.lvMixes = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.tsMixes = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.cmList = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.miNew = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopy = new System.Windows.Forms.ToolStripMenuItem();
			this.miDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.miPlay = new System.Windows.Forms.ToolStripMenuItem();
			this.miPause = new System.Windows.Forms.ToolStripMenuItem();
			this.btnMixDelete = new System.Windows.Forms.ToolStripButton();
			this.btnMixAdd = new System.Windows.Forms.ToolStripButton();
			this.btnDock = new System.Windows.Forms.ToolStripButton();
			this.tsMixes.SuspendLayout();
			this.cmList.SuspendLayout();
			this.SuspendLayout();
			// 
			// lvMixes
			// 
			this.lvMixes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lvMixes.ContextMenuStrip = this.cmList;
			this.lvMixes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvMixes.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lvMixes.FullRowSelect = true;
			this.lvMixes.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvMixes.HideSelection = false;
			this.lvMixes.Location = new System.Drawing.Point(0, 25);
			this.lvMixes.MultiSelect = false;
			this.lvMixes.Name = "lvMixes";
			this.lvMixes.Size = new System.Drawing.Size(259, 313);
			this.lvMixes.SmallImageList = this.imageList;
			this.lvMixes.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvMixes.TabIndex = 3;
			this.lvMixes.UseCompatibleStateImageBehavior = false;
			this.lvMixes.View = System.Windows.Forms.View.Details;
			this.lvMixes.ItemActivate += new System.EventHandler(this.lvMixes_ItemActivate);
			this.lvMixes.SelectedIndexChanged += new System.EventHandler(this.lvMixes_SelectedIndexChanged);
			this.lvMixes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvMixes_KeyDown);
			this.lvMixes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvMixes_MouseClick);
			this.lvMixes.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lvMixes_MouseMove);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 100;
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tsMixes
			// 
			this.tsMixes.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMixes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnMixDelete,
            this.btnMixAdd,
            this.toolStripSeparator1,
            this.btnDock});
			this.tsMixes.Location = new System.Drawing.Point(0, 0);
			this.tsMixes.Name = "tsMixes";
			this.tsMixes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.tsMixes.Size = new System.Drawing.Size(259, 25);
			this.tsMixes.TabIndex = 2;
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// cmList
			// 
			this.cmList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNew,
            this.miCopy,
            this.miDelete,
            this.toolStripSeparator2,
            this.miPlay,
            this.miPause});
			this.cmList.Name = "cmList";
			this.cmList.Size = new System.Drawing.Size(158, 120);
			this.cmList.Opening += new System.ComponentModel.CancelEventHandler(this.cmList_Opening);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
			// 
			// miNew
			// 
			this.miNew.Image = global::AudioMixer.Properties.Resources.plus;
			this.miNew.Name = "miNew";
			this.miNew.Size = new System.Drawing.Size(157, 22);
			this.miNew.Text = "New";
			this.miNew.Click += new System.EventHandler(this.miNew_Click);
			// 
			// miCopy
			// 
			this.miCopy.Image = global::AudioMixer.Properties.Resources.copy;
			this.miCopy.Name = "miCopy";
			this.miCopy.Size = new System.Drawing.Size(157, 22);
			this.miCopy.Text = "Duplicate";
			this.miCopy.Click += new System.EventHandler(this.miCopy_Click);
			// 
			// miDelete
			// 
			this.miDelete.Image = global::AudioMixer.Properties.Resources.close;
			this.miDelete.Name = "miDelete";
			this.miDelete.Size = new System.Drawing.Size(157, 22);
			this.miDelete.Text = "Delete";
			this.miDelete.Click += new System.EventHandler(this.miDelete_Click);
			// 
			// miPlay
			// 
			this.miPlay.Image = global::AudioMixer.Properties.Resources.play;
			this.miPlay.Name = "miPlay";
			this.miPlay.Size = new System.Drawing.Size(157, 22);
			this.miPlay.Text = "Play";
			this.miPlay.Click += new System.EventHandler(this.miPlay_Click);
			// 
			// miPause
			// 
			this.miPause.Image = global::AudioMixer.Properties.Resources.pause2;
			this.miPause.Name = "miPause";
			this.miPause.Size = new System.Drawing.Size(157, 22);
			this.miPause.Text = "Stop";
			this.miPause.Click += new System.EventHandler(this.miPause_Click);
			// 
			// btnMixDelete
			// 
			this.btnMixDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnMixDelete.Enabled = false;
			this.btnMixDelete.Image = global::AudioMixer.Properties.Resources.minus;
			this.btnMixDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMixDelete.Name = "btnMixDelete";
			this.btnMixDelete.Size = new System.Drawing.Size(23, 22);
			this.btnMixDelete.Text = "Delete";
			this.btnMixDelete.Click += new System.EventHandler(this.btnMixDelete_Click);
			// 
			// btnMixAdd
			// 
			this.btnMixAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnMixAdd.Image = global::AudioMixer.Properties.Resources.plus;
			this.btnMixAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnMixAdd.Name = "btnMixAdd";
			this.btnMixAdd.Size = new System.Drawing.Size(23, 22);
			this.btnMixAdd.Text = "New";
			this.btnMixAdd.Click += new System.EventHandler(this.btnMixAdd_Click);
			// 
			// btnDock
			// 
			this.btnDock.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnDock.Image = global::AudioMixer.Properties.Resources.dock_top;
			this.btnDock.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnDock.Name = "btnDock";
			this.btnDock.Size = new System.Drawing.Size(23, 22);
			this.btnDock.Text = "Change window position";
			this.btnDock.Click += new System.EventHandler(this.btnDock_Click);
			// 
			// MixesListPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lvMixes);
			this.Controls.Add(this.tsMixes);
			this.Name = "MixesListPanel";
			this.Size = new System.Drawing.Size(259, 338);
			this.tsMixes.ResumeLayout(false);
			this.tsMixes.PerformLayout();
			this.cmList.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

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
	}
}
