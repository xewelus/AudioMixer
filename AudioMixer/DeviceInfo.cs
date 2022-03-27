using System;
using System.Collections.Generic;
using System.Linq;
using CommonWinForms;
using CSCore.CoreAudioAPI;

namespace AudioMixer;

public class Device
{
	public readonly string Name;

	public Device(MMDevice device)
	{
		try
		{
			this.Name = device.FriendlyName;
		}
		catch (Exception ex)
		{
			ExcHandler.Catch(ex);
			this.Name = $"<{ex.Message}>";
		}
	}

	public static IEnumerable<MMDevice> GetDevices()
	{
		return MMDeviceEnumerator.EnumerateDevices(DataFlow.Render, DeviceState.Active);
	}

	public MMDevice GetDevice()
	{
		return GetDevices().FirstOrDefault(d => d.FriendlyName == this.Name);
	}

	public override string ToString()
	{
		return this.Name;
	}
}