using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BC RID: 188
internal class SmartPingManager : MonoBehaviour
{
	// Token: 0x060004F5 RID: 1269 RVA: 0x00020814 File Offset: 0x0001EA14
	private void Awake()
	{
		SmartPingManager.mgr = this;
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0002081C File Offset: 0x0001EA1C
	public PingSample GetPing(string IP)
	{
		PingSample pingSample;
		if (SmartPingManager.pingDict.TryGetValue(IP, out pingSample))
		{
			return pingSample;
		}
		pingSample = new PingSample();
		SmartPingManager.pingDict.Add(IP, pingSample);
		base.StartCoroutine(pingSample.getPing(IP));
		return pingSample;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x00020860 File Offset: 0x0001EA60
	public static void Refresh()
	{
		SmartPingManager.pingDict.Clear();
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x0002086C File Offset: 0x0001EA6C
	public static PingSample GetPing1(string IP)
	{
		return SmartPingManager.mgr.GetPing(IP);
	}

	// Token: 0x04000468 RID: 1128
	public static bool IPObtained = false;

	// Token: 0x04000469 RID: 1129
	public static bool PingObtained = false;

	// Token: 0x0400046A RID: 1130
	private static List<string> uniqueIP = new List<string>();

	// Token: 0x0400046B RID: 1131
	private static Dictionary<string, PingSample> pingDict = new Dictionary<string, PingSample>();

	// Token: 0x0400046C RID: 1132
	private static SmartPingManager mgr = null;
}
