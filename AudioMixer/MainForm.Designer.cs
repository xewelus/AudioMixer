namespace AudioMixer
{
	partial class MainForm
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
			this.components = new System.ComponentModel.Container();
			this.splitContainer = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.cbAudioDevice = new System.Windows.Forms.ComboBox();
			this.pnlMixes = new AudioMixer.MixesListPanel();
			this.saveTimer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
			this.splitContainer.Panel1.SuspendLayout();
			this.splitContainer.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer
			// 
			this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer.Location = new System.Drawing.Point(0, 32);
			this.splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			this.splitContainer.Panel1.Controls.Add(this.pnlMixes);
			this.splitContainer.Size = new System.Drawing.Size(1291, 731);
			this.splitContainer.SplitterDistance = 261;
			this.splitContainer.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbAudioDevice);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1291, 32);
			this.panel1.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Аудио-устройство:";
			// 
			// cbAudioDevice
			// 
			this.cbAudioDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbAudioDevice.FormattingEnabled = true;
			this.cbAudioDevice.Location = new System.Drawing.Point(117, 6);
			this.cbAudioDevice.Name = "cbAudioDevice";
			this.cbAudioDevice.Size = new System.Drawing.Size(379, 21);
			this.cbAudioDevice.TabIndex = 1;
			// 
			// pnlMixes
			// 
			this.pnlMixes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMixes.Location = new System.Drawing.Point(0, 0);
			this.pnlMixes.Name = "pnlMixes";
			this.pnlMixes.Size = new System.Drawing.Size(261, 731);
			this.pnlMixes.TabIndex = 0;
			this.pnlMixes.ItemSelected += new System.EventHandler(this.pnlMixes_ItemSelected);
			// 
			// saveTimer
			// 
			this.saveTimer.Enabled = true;
			this.saveTimer.Interval = 60000;
			this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1291, 763);
			this.Controls.Add(this.splitContainer);
			this.Controls.Add(this.panel1);
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.Text = "AudioMixer";
			this.splitContainer.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
			this.splitContainer.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbAudioDevice;
		private MixesListPanel pnlMixes;
		private System.Windows.Forms.Timer saveTimer;
	}
}

