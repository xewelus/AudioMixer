using System;
using System.Windows.Controls;
using CommonWpf;
using CommonWinForms.Extensions;
using System.Windows.Forms;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace AudioMixer
{
	public partial class WpfMainPanel
	{
		private WpfMixPanel mixPanel;
		private Player player;
		private bool internalChanges;
		private Machine currentMachine;
		private WindowController windowController;

		public bool NeedSave { get; private set; }

		private SplitContainer sc = new SplitContainer();
		public SplitContainer SplitContainer
		{
			get
			{
				return this.sc;
			}
		}

		public WpfMixesListPanel MixesListPanel
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
				return (int)this.tbVolume.Value;
			}
			set
			{
				this.tbVolume.Value = value;
			}
		}

		public event EventHandler PlayStateChanged;

		public WpfMainPanel()
		{
			this.InitializeComponent();

			if (this.InRuntime())
			{
				Settings.OnNeedSave += this.OnNeedSave;

				this.mixPanelContainer.Children.Clear();
			}
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

				this.pnlMixes.ItemSelected += new System.EventHandler(this.pnlMixes_ItemSelected);
				this.pnlMixes.ItemActivated += new System.EventHandler(this.pnlMixes_ItemActivated);
				this.pnlMixes.DockButtonClick += new System.EventHandler(this.pnlMixes_DockButtonClick);

				this.pnlMixes.PanelOrientation = this.SplitContainer.Orientation;

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
			if (this.mixPanel != null)
			{
				this.mixPanel.Remove();
				this.mixPanel = null;
			}

			if (this.pnlMixes.SelectedMix != null)
			{
				this.mixPanel = new WpfMixPanel(this.pnlMixes.SelectedMix);
				this.mixPanel.NameChanged += this.MixPanelOnNameChanged;
				this.mixPanel.VolumeChanged += this.MixPanelOnVolumeChanged;
				this.mixPanel.PlayChanged += this.MixPanelOnPlayChanged;
				this.mixPanel.ContentChanged += this.MixPanelOnContentChanged;

				this.mixPanelContainer.Children.Add(this.mixPanel);
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

		private void MixPanelOnContentChanged(object sender, EventArgs e)
		{
			this.Play(false);
		}

		private void cbAudioDevice_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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
					UIHelper.ShowError("Необходимо выбрать аудио-устройство.");
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
			this.btnSave.IsEnabled = true;
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
				this.btnSave.IsEnabled = false;
				this.NeedSave = false;

				//Application.DoEvents();
				Settings.Save();
			}
			catch
			{
				this.btnSave.IsEnabled = true;
				this.NeedSave = true;
				throw;
			}
		}

		private void pnlMixes_DockButtonClick(object sender, EventArgs e)
		{
			this.windowController.SwitchOrientation();
			this.pnlMixes.PanelOrientation = this.SplitContainer.Orientation;
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
			if (WpfMainForm.IsPlayChangeKey(e.Key))
			{
				this.UpdateVolume();
			}
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Content = string.Format("Громкость ({0:0}%):", this.tbVolume.Value);

			if (!this.internalChanges)
			{
				this.currentMachine.Volume = (float)(this.tbVolume.Value / 100f);
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
