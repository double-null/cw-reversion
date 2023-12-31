using System;
using UnityEngine;

// Token: 0x02000127 RID: 295
internal class CurveEval
{
	// Token: 0x060007E0 RID: 2016 RVA: 0x00048470 File Offset: 0x00046670
	public CurveEval(AnimationCurve c)
	{
		this._moveCurve = c;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x0004848C File Offset: 0x0004668C
	public void Start(bool invert = false)
	{
		this._time = Time.time;
		this._invert = invert;
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000484A0 File Offset: 0x000466A0
	public float Value
	{
		get
		{
			return (!this._invert) ? this._moveCurve.Evaluate((Time.time - this._time) * this.Speed) : (1f - this._moveCurve.Evaluate((Time.time - this._time) * this.Speed));
		}
	}

	// Token: 0x0400084D RID: 2125
	private readonly AnimationCurve _moveCurve;

	// Token: 0x0400084E RID: 2126
	private bool _invert;

	// Token: 0x0400084F RID: 2127
	private float _time;

	// Token: 0x04000850 RID: 2128
	public float Speed = 1f;
}
