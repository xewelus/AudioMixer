using System;
using System.Windows.Controls;
using CSCore.CoreAudioAPI;

namespace AudioMixer
{
	public class AudioDeviceComboBox : ComboBox
	{
		private Machine currentMachine;
		private bool internalChanges = true;
		private Device prev;

		public Device SelectedDevice
		{
			get
			{
				return (Device)this.SelectedItem;
			}
		}

		public void Init(Machine machine)
		{
			this.currentMachine = machine;
			this.RefreshDevices();
		}

		private void RefreshDevices()
		{
			this.internalChanges = true;

			this.Items.Clear();
			foreach (MMDevice device in Device.GetDevices())
			{
				this.Items.Add(new Device(device));
			}

			if (this.currentMachine.AudioDevice?.Name != null)
			{
				foreach (Device info in this.Items)
				{
					if (info.Name == this.currentMachine.AudioDevice.Name)
					{
						this.prev = info;
						this.SelectedItem = info;
						break;
					}
				}
			}

			this.internalChanges = false;
		}

		protected override void OnDropDownOpened(EventArgs e)
		{
			base.OnDropDownOpened(e);
			this.RefreshDevices();
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			base.OnSelectionChanged(e);

			Device info = (Device)this.SelectedItem;
			if (this.prev != info)
			{
				this.prev = info;

				base.OnSelectionChanged(e);

				if (this.internalChanges) return;

				this.currentMachine.AudioDevice.Name = info.Name;
				Settings.SaveAppearance();
			}
		}

	
	}
}
