using System;
using System.Collections.Generic;
using System.Linq;
using CommonWpf.Classes.UI;
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
			ExceptionHandler.Catch(ex);
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