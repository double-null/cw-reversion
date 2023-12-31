using System;

// Token: 0x02000029 RID: 41
[Serializable]
public class RundomRange
{
	// Token: 0x060000A1 RID: 161 RVA: 0x00008EC4 File Offset: 0x000070C4
	public void Init()
	{
		this._random = (this.Range > float.Epsilon);
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x060000A2 RID: 162 RVA: 0x00008EDC File Offset: 0x000070DC
	public float Value
	{
		get
		{
			return (!this._random) ? this.Min : (this.Min + FastRndom.Float() * this.Range);
		}
	}

	// Token: 0x04000143 RID: 323
	public float Min;

	// Token: 0x04000144 RID: 324
	public float Range;

	// Token: 0x04000145 RID: 325
	private bool _random;
}
