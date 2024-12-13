namespace AudioMixer
{
	public partial class SubmixPanel : UserControl
	{
		public SubmixPanel()
		{
			this.InitializeComponent();
		}

		public SoundInfo SoundInfo { get; private set; }
		public event EventHandler DeleteButtonClick;
		public event EventHandler VolumeChanged;
		public event EventHandler PlayChanged;
		public event EventHandler ContentChanged;

		private bool internalChanges;

		public void SetSoundInfo(SoundInfo soundInfo, MixInfo toExclude)
		{
			this.SoundInfo = soundInfo;

			this.internalChanges = true;

			this.FillMixList(toExclude);

			this.tbVolume.Value = (int)(soundInfo.Volume * 100);
			this.tbVolume_ValueChanged(null, null);

			this.internalChanges = false;
		}

		void FillMixList(MixInfo toExclude)
		{
			MixItem toSelect = null;
			this.cbMix.Items.Clear();
			foreach (MixInfo mix in Settings.Current.Mixes)
			{
				if (mix.ID == toExclude.ID) continue;

				MixItem mixItem = new MixItem { ID = mix.ID, Name = mix.Name };
				this.cbMix.Items.Add(mixItem);
				
				if (mix.ID == this.SoundInfo.MixID)
				{
					toSelect = mixItem;
				}
			}
			this.cbMix.Sorted = true;

			if (toSelect != null)
			{
				this.cbMix.SelectedItem = toSelect;
			}
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			this.DeleteButtonClick?.Invoke(this, EventArgs.Empty);
		}

		private void tbVolume_ValueChanged(object sender, EventArgs e)
		{
			this.lblVolume.Text = string.Format("Volume ({0}%):", this.tbVolume.Value);
			if (this.internalChanges) return;
			this.SoundInfo.Volume = this.tbVolume.Value / 100f;

			this.VolumeChanged?.Invoke(this, EventArgs.Empty);

			Settings.SetNeedSave();
		}

		private void controls_KeyDown(object sender, KeyEventArgs e)
		{
			if (MainForm.IsPlayChangeKey(e))
			{
				this.PlayChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		private void cbMix_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.internalChanges)
			{
				return;
			}

			if (this.cbMix.SelectedItem is MixItem selectedMixItem && selectedMixItem.ID != this.SoundInfo.MixID)
			{
				this.SoundInfo.MixID = selectedMixItem.ID;
				Settings.SetNeedSave();
			}

			this.ContentChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public class MixItem
	{
		public int ID;
		public string Name;

		public override string ToString()
		{
			return this.Name;
		}
	}
}
