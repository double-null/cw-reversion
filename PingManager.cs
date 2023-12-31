using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B7 RID: 183
[AddComponentMenu("Scripts/Engine/PingManager")]
internal class PingManager : SingletoneForm<PingManager>
{
	// Token: 0x060004B9 RID: 1209 RVA: 0x0001F908 File Offset: 0x0001DB08
	public static void Clear()
	{
		SingletoneForm<PingManager>.Instance.pings.Clear();
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0001F91C File Offset: 0x0001DB1C
	public static int Ping(HostInfo hostInfo)
	{
		if (hostInfo == null)
		{
			return 99999;
		}
		for (int i = 0; i < SingletoneForm<PingManager>.Instance.pings.Count; i++)
		{
			if (SingletoneForm<PingManager>.Instance.pings[i].hostInfo.ForceNAT != hostInfo.ForceNAT)
			{
				break;
			}
			PingInstance pingInstance = SingletoneForm<PingManager>.Instance.pings[i];
			for (int j = 0; j < hostInfo.Ip.Length; j++)
			{
				if (SingletoneForm<PingManager>.Instance.pings[i].hostInfo.Ip.Length != hostInfo.Ip.Length)
				{
					pingInstance = null;
				}
				else if (SingletoneForm<PingManager>.Instance.pings[i].hostInfo.Ip[j] != hostInfo.Ip[j])
				{
					pingInstance = null;
				}
			}
			if (pingInstance != null)
			{
				return pingInstance.Ping;
			}
		}
		PingInstance pingInstance2 = new PingInstance(hostInfo);
		SingletoneForm<PingManager>.Instance.pings.Add(pingInstance2);
		return pingInstance2.Ping;
	}

	// Token: 0x04000455 RID: 1109
	private List<PingInstance> pings = new List<PingInstance>();
}
