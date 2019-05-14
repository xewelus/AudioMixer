using System;
using System.Collections.Generic;

namespace AudioMixer
{
	public class MixIdsCollection
	{
		private static readonly List<int> reservedIds = new List<int>();
		public static int GetFreeID()
		{
			for (int i = 1; i < int.MaxValue; i++)
			{
				bool exists = false;
				foreach (MixInfo mixInfo in Settings.Current.Mixes)
				{
					if (mixInfo.ID == i)
					{
						exists = true;
						break;
					}
				}

				if (!exists)
				{
					foreach (int id in reservedIds)
					{
						if (id == i)
						{
							exists = true;
							break;
						}
					}
				}

				if (!exists)
				{
					reservedIds.Add(i);
					return i;
				}
			}
			throw new NotSupportedException();
		}
	}
}