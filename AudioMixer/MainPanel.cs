using System;
using System.Windows.Forms;
using CommonWinForms;
using CommonWinForms.Extensions;
using CSCore.CoreAudioAPI;

namespace AudioMixer
{
	public sealed partial class MainPanel : UserControl
	{
		private MixPanel mixPanel;
		private Player player;
		private bool internalChanges;
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

		public int Volume
		{
			get
			{
				return this.tbVolume.Value;
			}
			set
			{
				this.tbVolume.Value = value;
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

				this.cbAudioDevice.Init(machine);

				this.tbVolume.Value = (int)(machine.Volume * 100);
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

		private void pnlMixes_ItemSelected(object sender, EventArgs e)
		{
			var container = this.splitContainer.Panel2;
			container.SuspendLayout();

			if (this.mixPanel == null)
			{
				this.mixPanel = new MixPanel();
				this.mixPanel.Dock = DockStyle.Fill;
				this.mixPanel.NameChanged += this.MixPanelOnNameChanged;
				this.mixPanel.VolumeChanged += this.MixPanelOnVolumeChanged;
				this.mixPanel.PlayChanged += this.MixPanelOnPlayChanged;
				this.mixPanel.ContentChanged += MixPanelOnContentChanged;
			}
			else
			{
				this.mixPanel.ClearSoundPanels();
			}

			if (this.pnlMixes.SelectedMix == null)
			{
				if (this.mixPanel.Parent != null)
				{
					container.Controls.Remove(this.mixPanel);
				}
			}
			else
			{
				this.mixPanel.SetMixInfo(this.pnlMixes.SelectedMix);

				if (this.mixPanel.Parent == null)
				{
					container.Controls.Add(this.mixPanel);
				}

				this.mixPanel.Visible = true;
			}

			container.ResumeLayout();
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

		private void MixPanelOnContentChanged(object sender, EventArgs e)
		{
			this.Play(false);
		}

		private void cbAudioDevice_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.internalChanges) return;

			if (this.player != null)
			{
				bool isPaused = this.player.IsPaused();

				this.player.Dispose();
				this.player = null;

				if (!isPaused)
				{
					this.pnlMixes_ItemActivated(null, null);
				}
			}
		}

		private void pnlMixes_ItemActivated(object sender, EventArgs e)
		{
			this.Play(true);
		}

		private void Play(bool toggleMode)
		{
			if (this.player != null)
			{
				bool needDispose = false;

				if (toggleMode)
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
						needDispose = true;
					}
				}
				else
				{
					needDispose = true;
				}

				if (needDispose)
				{
					this.player.Dispose();
					this.player = null;
				}
			}

			if (this.player == null && this.pnlMixes.ActivatedMix != null)
			{
				Device device = this.cbAudioDevice.SelectedDevice;
				if (device == null)
				{
					UIHelper.ShowError("You must select an audio device.");
					return;
				}

				this.currentMachine.LastMixID = this.pnlMixes.ActivatedMix.ID;
				Settings.SaveAppearance();

				this.player = new Player(device, this.pnlMixes.ActivatedMix, this.currentMachine.Volume);
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
			this.lblVolume.Text = string.Format("Volume ({0}%):", this.tbVolume.Value);

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
