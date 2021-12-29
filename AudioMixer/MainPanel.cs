using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CommonWinForms;
using CommonWinForms.Extensions;
using NAudio.Wave;

namespace AudioMixer
{
	public sealed partial class MainPanel : UserControl
	{
		private MixPanel mixPanel;
		private Player player;
		private bool internalChanges;
		private DeviceInfo currentDevice;
		private Machine currentMachine;
		private WindowController windowController;

		public bool NeedSave { get; private set; }

		public SplitContainer SplitContainer
		{
			get
			{
				return this.splitContainer;
			}
		}

		public MixesListPanel MixesListPanel
		{
			get
			{
				return this.pnlMixes;
			}
		}

		public event EventHandler PlayStateChanged;

		public MainPanel()
		{
			this.InitializeComponent();
			Settings.OnNeedSave += this.OnNeedSave;
		}

		public void Init(Machine machine, WindowController wc)
		{
			this.currentMachine = machine;
			this.windowController = wc;

			try
			{
				this.internalChanges = true;

				this.InitDevice();

				this.tbVolume.Value = (int)(this.currentMachine.Volume * 100);
				this.tbVolume_ValueChanged(null, null);

				this.pnlMixes.PanelOrientation = this.splitContainer.Orientation;

				if (machine.LastMixID != null)
				{
					this.pnlMixes.SelectItemByID(machine.LastMixID.Value);
				}
			}
			finally
			{
				this.internalChanges = false;
			}
		}

		private void InitDevice()
		{
			StringBuilder sb = new StringBuilder();
			foreach (DirectSoundDeviceInfo di in DirectSoundOut.Devices)
			{
				this.cbAudioDevice.Items.Add(di);

				if (sb.Length > 0)
				{
					sb.AppendLine();
				}
				sb.Append(di.Description);
			}

			byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
			SHA256 sha = SHA256.Create();
			byte[] hashBytes = sha.ComputeHash(bytes);
			string hash = Convert.ToBase64String(hashBytes);

			foreach (DeviceInfo deviceInfo in this.currentMachine.AudioDevices)
			{
				if (deviceInfo.Hash == hash)
				{
					this.currentDevice = deviceInfo;
					break;
				}
			}

			if (this.currentDevice == null)
			{
				this.currentDevice = new DeviceInfo();
				this.currentDevice.Hash = hash;
				this.currentMachine.AudioDevices.Add(this.currentDevice);
				Settings.SaveAppearance();
			}

			if (this.currentDevice != null)
			{
				foreach (DirectSoundDeviceInfo deviceInfo in this.cbAudioDevice.Items)
				{
					if (deviceInfo.Description == this.currentDevice.Name)
					{
						this.cbAudioDevice.SelectedItem = deviceInfo;
						break;
					}
				}
			}
		}

		private void pnlMixes_ItemSelected(object sender, EventArgs e)
		{
			if (this.mixPanel != null)
			{
				this.mixPanel.Remove(true);
				this.mixPanel = null;
			}

			if (this.pnlMixes.SelectedMix != null)
			{
				this.mixPanel = new MixPanel(this.pnlMixes.SelectedMix);
				this.mixPanel.Dock = DockStyle.Fill;
				this.mixPanel.NameChanged += this.MixPanelOnNameChanged;
				this.mixPanel.VolumeChanged += this.MixPanelOnVolumeChanged;
				this.mixPanel.PlayChanged += this.MixPanelOnPlayChanged;
				this.splitContainer.Panel2.Controls.Add(this.mixPanel);
			}
		}

		private void MixPanelOnNameChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.UpdateName(this.mixPanel.MixName);
		}

		private void UpdateVolume()
		{
			if (this.player != null)
			{
				this.player.UpdateVolume(this.currentMachine.Volume);
			}
		}

		private void MixPanelOnVolumeChanged(object sender, EventArgs eventArgs)
		{
			this.UpdateVolume();
		}

		private void MixPanelOnPlayChanged(object sender, EventArgs eventArgs)
		{
			this.pnlMixes.PlayChange();
		}

		private void cbAudioDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;

			DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
			this.currentDevice.Name = deviceInfo.Description;
			Settings.SaveAppearance();
		}

		private void pnlMixes_ItemActivated(object sender, EventArgs e)
		{
			if (this.player != null)
			{
				if (this.pnlMixes.ActivatedMix == null)
				{
					this.player.Pause();
				}
				else if (this.pnlMixes.ActivatedMix == this.player.Mix)
				{
					this.player.Play();
				}
				else
				{
					this.player.Dispose();
					this.player = null;
				}
			}

			if (this.player == null && this.pnlMixes.ActivatedMix != null)
			{
				DirectSoundDeviceInfo deviceInfo = (DirectSoundDeviceInfo)this.cbAudioDevice.SelectedItem;
				if (deviceInfo == null)
				{
					UIHelper.ShowError("Необходимо выбрать аудио-устройство.");
					return;
				}

				this.currentMachine.LastMixID = this.pnlMixes.ActivatedMix.ID;
				Settings.SaveAppearance();

				this.player = new Player(deviceInfo, this.pnlMixes.ActivatedMix, this.currentMachine.Volume);
				this.player.Play();
			}

			if (this.PlayStateChanged != null)
			{
				this.PlayStateChanged(this, EventArgs.Empty);
			}
		}

		private void OnNeedSave(object sender, EventArgs eventArgs)
		{
			this.btnSave.Enabled = true;
			this.NeedSave = true;
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			this.Save();
		}

		public void Save()
		{
			try
			{
				this.btnSave.Enabled = false;
				this.NeedSave = false;

				Application.DoEvents();
				Settings.Save();
			}
			catch
			{
				this.btnSave.Enabled = true;
				this.NeedSave = true;
				throw;
			}
		}

		private void pnlMixes_DockButtonClick(object sender, EventArgs e)
		{
			this.windowController.SwitchOrientation();
			this.pnlMixes.PanelOrientation = this.splitContainer.Orientation;
		}

		private void splitContainer_SplitterMoved(object sender, SplitterEventArgs e)
		{
			if (this.windowController != null)
			{
				this.windowController.SplitterMoved();
			}
		}

		private void tbVolume_KeyDown(object sender, KeyEventArgs e)
		{
			if (MainForm.IsPlayChangeKey(e))
			{
				this.UpdateVolume();
			}
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Громкость ({0}%):", this.tbVolume.Value);

			if (!this.internalChanges)
			{
				this.currentMachine.Volume = this.tbVolume.Value / 100f;
				Settings.SaveAppearance();

				this.UpdateVolume();
			}
		}

		public void OnClosed()
		{
			if (this.player != null)
			{
				this.player.Dispose();
				this.player = null;
			}
		}
	}
}
