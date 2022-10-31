using System.Windows.Controls;

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
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblVolume = new System.Windows.Forms.Label();
			this.tbVolume = new CommonWinForms.Controls.XwTrackBar();
			this.btnSave = new System.Windows.Forms.Button();
			this.cbAudioDevice = new AudioMixer.AudioDeviceComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.hostMixesList = new System.Windows.Forms.Integration.ElementHost();
			this.pnlMixes = new AudioMixer.WpfMixesListPanel();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).BeginInit();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 75);
			this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.hostMixesList);
			// 
			// splitContainer.Panel2
			// 
			this.splitContainer.Panel2.AutoScroll = true;
			this.splitContainer.Size = new System.Drawing.Size(873, 519);
			this.splitContainer.SplitterDistance = 289;
			this.splitContainer.SplitterWidth = 5;
			this.splitContainer.TabIndex = 0;
			this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer_SplitterMoved);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblVolume);
			this.panel1.Controls.Add(this.tbVolume);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.cbAudioDevice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(873, 75);
			this.panel1.TabIndex = 1;
			// 
			// lblVolume
			// 
			this.lblVolume.AutoSize = true;
			this.lblVolume.Location = new System.Drawing.Point(12, 39);
			this.lblVolume.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Size = new System.Drawing.Size(78, 16);
			this.lblVolume.TabIndex = 5;
			this.lblVolume.Text = "Громкость:";
			// 
			// tbVolume
			// 
			this.tbVolume.AutoSize = false;
			this.tbVolume.LargeChange = 1;
			this.tbVolume.Location = new System.Drawing.Point(148, 34);
			this.tbVolume.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tbVolume.Maximum = 100;
			this.tbVolume.Name = "tbVolume";
			this.tbVolume.Size = new System.Drawing.Size(488, 30);
			this.tbVolume.TabIndex = 4;
			this.tbVolume.Value = 100;
			this.tbVolume.ValueChanged += new System.EventHandler(this.tbVolume_ValueChanged);
			this.tbVolume.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbVolume_KeyDown);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Enabled = false;
			this.btnSave.Image = global::AudioMixer.Properties.Resources.save;
			this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSave.Location = new System.Drawing.Point(839, 5);
			this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(31, 28);
			this.btnSave.TabIndex = 2;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// cbAudioDevice
			// 
			this.cbAudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAudioDevice.FormattingEnabled = true;
			this.cbAudioDevice.Location = new System.Drawing.Point(156, 7);
			this.cbAudioDevice.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cbAudioDevice.Name = "cbAudioDevice";
			this.cbAudioDevice.Size = new System.Drawing.Size(471, 24);
			this.cbAudioDevice.TabIndex = 1;
			this.cbAudioDevice.SelectedIndexChanged += new System.EventHandler(this.cbAudioDevice_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 11);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(131, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Аудио-устройство:";
			// 
			// hostMixesList
			// 
			this.hostMixesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.hostMixesList.Location = new System.Drawing.Point(0, 0);
			this.hostMixesList.Name = "hostMixesList";
			this.hostMixesList.Size = new System.Drawing.Size(289, 519);
			this.hostMixesList.TabIndex = 0;
			this.hostMixesList.Text = "hostMixesList";
			this.hostMixesList.Child = this.pnlMixes;
			// 
			// MainPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainPanel";
			this.Size = new System.Drawing.Size(873, 594);
			this.splitContainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.tbVolume)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private AudioDeviceComboBox cbAudioDevice;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Label lblVolume;
		private CommonWinForms.Controls.XwTrackBar tbVolume;
		private System.Windows.Forms.Integration.ElementHost hostMixesList;
		private WpfMixesListPanel pnlMixes;
	}
}

