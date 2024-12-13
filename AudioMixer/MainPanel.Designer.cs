namespace AudioMixer
{
	sealed partial class MainPanel
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			splitContainer = new SplitContainer();
			pnlMixes = new MixesListPanel();
			panel1 = new Panel();
			lblVolume = new Label();
			tbVolume = new CommonWinForms.Controls.XwTrackBar();
			btnSave = new Button();
			cbAudioDevice = new AudioDeviceComboBox();
			label1 = new Label();
			((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)tbVolume).BeginInit();
			SuspendLayout();
			// 
			// splitContainer
			// 
			splitContainer.Dock = DockStyle.Fill;
			splitContainer.Location = new Point(0, 70);
			splitContainer.Margin = new Padding(4, 3, 4, 3);
			splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			splitContainer.Panel1.Controls.Add(pnlMixes);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.AutoScroll = true;
			splitContainer.Size = new Size(764, 487);
			splitContainer.SplitterDistance = 253;
			splitContainer.SplitterWidth = 5;
			splitContainer.TabIndex = 0;
			splitContainer.SplitterMoved += splitContainer_SplitterMoved;
			// 
			// pnlMixes
			// 
			pnlMixes.Dock = DockStyle.Fill;
			pnlMixes.Location = new Point(0, 0);
			pnlMixes.Margin = new Padding(5, 3, 5, 3);
			pnlMixes.Name = "pnlMixes";
			pnlMixes.Size = new Size(253, 487);
			pnlMixes.TabIndex = 0;
			pnlMixes.ItemSelected += pnlMixes_ItemSelected;
			pnlMixes.ItemActivated += pnlMixes_ItemActivated;
			pnlMixes.DockButtonClick += pnlMixes_DockButtonClick;
			pnlMixes.ThemeButtonClick += pnlMixes_ThemeButtonClick;
			pnlMixes.SelectedMixChanged += pnlMixes_SelectedMixChanged;
			// 
			// panel1
			// 
			panel1.Controls.Add(lblVolume);
			panel1.Controls.Add(tbVolume);
			panel1.Controls.Add(btnSave);
			panel1.Controls.Add(cbAudioDevice);
			panel1.Controls.Add(label1);
			panel1.Dock = DockStyle.Top;
			panel1.Location = new Point(0, 0);
			panel1.Margin = new Padding(4, 3, 4, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(764, 70);
			panel1.TabIndex = 1;
			// 
			// lblVolume
			// 
			lblVolume.AutoSize = true;
			lblVolume.Location = new Point(10, 37);
			lblVolume.Margin = new Padding(4, 0, 4, 0);
			lblVolume.Name = "lblVolume";
			lblVolume.Size = new Size(50, 15);
			lblVolume.TabIndex = 5;
			lblVolume.Text = "Volume:";
			// 
			// tbVolume
			// 
			tbVolume.AutoSize = false;
			tbVolume.LargeChange = 1;
			tbVolume.Location = new Point(130, 32);
			tbVolume.Margin = new Padding(4, 3, 4, 3);
			tbVolume.Maximum = 100;
			tbVolume.Name = "tbVolume";
			tbVolume.Size = new Size(427, 28);
			tbVolume.TabIndex = 4;
			tbVolume.Value = 100;
			tbVolume.ValueChanged += tbVolume_ValueChanged;
			tbVolume.KeyDown += tbVolume_KeyDown;
			// 
			// btnSave
			// 
			btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnSave.Enabled = false;
			btnSave.Image = Properties.Resources.save;
			btnSave.ImageAlign = ContentAlignment.MiddleLeft;
			btnSave.Location = new Point(734, 5);
			btnSave.Margin = new Padding(4, 3, 4, 3);
			btnSave.Name = "btnSave";
			btnSave.Size = new Size(27, 27);
			btnSave.TabIndex = 2;
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// cbAudioDevice
			// 
			cbAudioDevice.DropDownStyle = ComboBoxStyle.DropDownList;
			cbAudioDevice.FormattingEnabled = true;
			cbAudioDevice.Location = new Point(136, 7);
			cbAudioDevice.Margin = new Padding(4, 3, 4, 3);
			cbAudioDevice.Name = "cbAudioDevice";
			cbAudioDevice.Size = new Size(412, 23);
			cbAudioDevice.TabIndex = 1;
			cbAudioDevice.SelectedIndexChanged += cbAudioDevice_SelectedIndexChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(10, 10);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(79, 15);
			label1.TabIndex = 0;
			label1.Text = "Audio device:";
			// 
			// MainPanel
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(splitContainer);
			Controls.Add(panel1);
			Margin = new Padding(4, 3, 4, 3);
			Name = "MainPanel";
			Size = new Size(764, 557);
			splitContainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
			splitContainer.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)tbVolume).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private AudioDeviceComboBox cbAudioDevice;
		private MixesListPanel pnlMixes;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblVolume;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
	}
}

