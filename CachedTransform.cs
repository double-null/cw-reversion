using System;
using UnityEngine;

// Token: 0x02000076 RID: 118
[Serializable]
internal class CachedTransform
{
	// Token: 0x06000263 RID: 611 RVA: 0x00013E48 File Offset: 0x00012048
	public CachedTransform(Transform transform)
	{
		this.parent = transform.parent;
		this.localPosition = transform.localPosition;
		this.localEulerAngles = transform.localEulerAngles;
		this.localScale = transform.localScale;
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00013E8C File Offset: 0x0001208C
	public void Reset(Transform transform)
	{
		transform.parent = this.parent;
		transform.localPosition = this.localPosition;
		transform.localEulerAngles = this.localEulerAngles;
		transform.localScale = this.localScale;
	}

	// Token: 0x0400031B RID: 795
	private Transform parent;

	// Token: 0x0400031C RID: 796
	private Vector3 localPosition;

	// Token: 0x0400031D RID: 797
	private Vector3 localEulerAngles;

	// Token: 0x0400031E RID: 798
	private Vector3 localScale;
}
