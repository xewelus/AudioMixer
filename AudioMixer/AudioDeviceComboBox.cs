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

			foreach (MMDevice device in MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active))
			{
				this.Items.Add(new ItemInfo(device));
			}

			if (machine.AudioDevice?.Name != null)
			{
				foreach (ItemInfo info in this.Items)
				{
					if (info.Device.FriendlyName == machine.AudioDevice.Name)
					{
						this.SelectedItem = info;
						break;
					}
				}
			}

			this.internalChanges = false;
		}

		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);

			if (this.internalChanges) return;

			ItemInfo info = (ItemInfo)this.SelectedItem;
			this.currentMachine.AudioDevice.Name = info.Device.FriendlyName;
			Settings.SaveAppearance();
		}

		private class ItemInfo
		{
			public readonly MMDevice Device;

			public ItemInfo(MMDevice device)
			{
				this.Device = device;
			}

			public override string ToString()
			{
				try
				{
					return this.Device.ToString();
				}
				catch (Exception ex)
				{
					ExcHandler.Catch(ex);
					return $"<{ex.Message}>";
				}
			}
		}
	}
}
