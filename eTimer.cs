using System;
using UnityEngine;

// Token: 0x020000AC RID: 172
[Serializable]
internal class eTimer
{
	// Token: 0x060003FA RID: 1018 RVA: 0x0001B4AC File Offset: 0x000196AC
	public void Start()
	{
		this.startTime = Time.realtimeSinceStartup;
		this.enabled = true;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0001B4C0 File Offset: 0x000196C0
	public void Stop()
	{
		this.enabled = false;
	}

	// Token: 0x17000084 RID: 132
	// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001B4CC File Offset: 0x000196CC
	public float Elapsed
	{
		get
		{
			return (!this.enabled) ? 0f : (Time.realtimeSinceStartup - this.startTime);
		}
	}

	// Token: 0x17000085 RID: 133
	// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001B4F0 File Offset: 0x000196F0
	public bool Enabled
	{
		get
		{
			return this.enabled;
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001B4F8 File Offset: 0x000196F8
	public bool Disabled
	{
		get
		{
			return !this.Enabled;
		}
	}

	// Token: 0x040003BB RID: 955
	private bool enabled;

	// Token: 0x040003BC RID: 956
	private float startTime;
}
