using System;
using UnityEngine;

// Token: 0x02000299 RID: 665
[Serializable]
internal class HitInfoUnit : ReusableClass<HitInfoUnit>
{
	// Token: 0x06001293 RID: 4755 RVA: 0x000CB2AC File Offset: 0x000C94AC
	public void Clear()
	{
		this.pos = Vector3.zero;
		this.euler = Vector3.zero;
	}

	// Token: 0x06001294 RID: 4756 RVA: 0x000CB2C4 File Offset: 0x000C94C4
	public void Clone(HitInfoUnit i)
	{
		this.pos = i.pos;
		this.euler = i.euler;
	}

	// Token: 0x0400155B RID: 5467
	public Vector3 pos;

	// Token: 0x0400155C RID: 5468
	public Vector3 euler;
}
