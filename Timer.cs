using System;
using UnityEngine;

// Token: 0x020003A2 RID: 930
internal class Timer
{
	// Token: 0x17000853 RID: 2131
	// (get) Token: 0x06001D9B RID: 7579 RVA: 0x001036A4 File Offset: 0x001018A4
	public float Time
	{
		get
		{
			if (this._time < 0f)
			{
				return 0f;
			}
			return UnityEngine.Time.time - this._time;
		}
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x001036D4 File Offset: 0x001018D4
	public void Start()
	{
		this._time = UnityEngine.Time.time;
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x001036E4 File Offset: 0x001018E4
	public void Stop()
	{
		this._time = -1f;
	}

	// Token: 0x17000854 RID: 2132
	// (get) Token: 0x06001D9E RID: 7582 RVA: 0x001036F4 File Offset: 0x001018F4
	public bool IsStarted
	{
		get
		{
			return this._time >= 0f;
		}
	}

	// Token: 0x0400224E RID: 8782
	private float _time = -1f;
}
