using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000BB RID: 187
internal class PingSample
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x000207AC File Offset: 0x0001E9AC
	public IEnumerator getPing(string IP)
	{
		while (!PingSample.done)
		{
			yield return new WaitForSeconds(0.01f);
		}
		if (this.ping == null)
		{
			this.ping = new Ping(IP);
		}
		while (!this.ping.isDone)
		{
			yield return new WaitForSeconds(0.01f);
		}
		yield return new WaitForSeconds(0.01f);
		PingSample.done = this.ping.isDone;
		this.Ping = this.ping.time;
		this.ping.DestroyPing();
		yield break;
	}

	// Token: 0x04000465 RID: 1125
	private Ping ping;

	// Token: 0x04000466 RID: 1126
	public int Ping = 999;

	// Token: 0x04000467 RID: 1127
	private static bool done = true;
}
