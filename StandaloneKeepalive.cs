using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C0 RID: 192
internal class StandaloneKeepalive
{
	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x060004FE RID: 1278 RVA: 0x000208A4 File Offset: 0x0001EAA4
	public static StandaloneKeepalive Instance
	{
		get
		{
			if (StandaloneKeepalive._instance == null)
			{
				StandaloneKeepalive._instance = new StandaloneKeepalive();
			}
			return StandaloneKeepalive._instance;
		}
	}

	// Token: 0x060004FF RID: 1279 RVA: 0x000208C0 File Offset: 0x0001EAC0
	public void Init(StandaloneKeepalive.StartDelegate startCoroutine)
	{
		this._startCoroutine = startCoroutine;
		this._startCoroutine(StandaloneKeepalive.Instance.KeepAlive());
	}

	// Token: 0x06000500 RID: 1280 RVA: 0x000208E0 File Offset: 0x0001EAE0
	private IEnumerator KeepAlive()
	{
		yield return new WaitForSeconds(45f);
		for (;;)
		{
			Main.AddDatabaseRequest<StandaloneKeepaliveRequest>(new object[0]);
			yield return new WaitForSeconds(300f);
		}
		yield break;
	}

	// Token: 0x04000489 RID: 1161
	private const float KEEPALIVE_INTERVAL = 300f;

	// Token: 0x0400048A RID: 1162
	private StandaloneKeepalive.StartDelegate _startCoroutine;

	// Token: 0x0400048B RID: 1163
	private static StandaloneKeepalive _instance;

	// Token: 0x020003AB RID: 939
	// (Invoke) Token: 0x06001E14 RID: 7700
	public delegate Coroutine StartDelegate(IEnumerator routine);
}
