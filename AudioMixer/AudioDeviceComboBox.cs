using System;
using System.Windows.Forms;
using CommonWinForms;
using CSCore.CoreAudioAPI;

namespace AudioMixer
{
	public class AudioDeviceComboBox : ComboBox
	{
		private Machine currentMachine;
		private bool internalChanges = true;
		private ItemInfo prev;

		public MMDevice SelectedDevice
		{
			get
			{
				ItemInfo info = (ItemInfo)this.SelectedItem;
				if (info == null) return null;

				return info.Device;
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
			foreach (MMDevice device in MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active))
			{
				this.Items.Add(new ItemInfo(device));
			}

			if (this.currentMachine.AudioDevice?.Name != null)
			{
				foreach (ItemInfo info in this.Items)
				{
					if (info.Device.FriendlyName == this.currentMachine.AudioDevice.Name)
					{
						this.prev = info;
						this.SelectedItem = info;
						break;
					}
				}
			}

			this.internalChanges = false;
		}

		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown(e);

			this.RefreshDevices();
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			ItemInfo info = (ItemInfo)this.SelectedItem;
			if (this.prev != info)
			{
				this.prev = info;

				base.OnSelectedIndexChanged(e);

				if (this.internalChanges) return;

				this.currentMachine.AudioDevice.Name = info.Device.FriendlyName;
				Settings.SaveAppearance();
			}
		}

		private class ItemInfo
		{
			public readonly MMDevice Device;
			private readonly string name;

			public ItemInfo(MMDevice device)
			{
				this.Device = device;

				try
				{
					this.name = device.ToString();
				}
				catch (Exception ex)
				{
					ExcHandler.Catch(ex);
					this.name = $"<{ex.Message}>";
				}
			}

			public override string ToString()
			{
				return this.name;
			}
		}
	}
}
