using System;
using UnityEngine;

// Token: 0x020002B2 RID: 690
[Serializable]
internal class Spawn
{
	// Token: 0x0600136A RID: 4970 RVA: 0x000D0B04 File Offset: 0x000CED04
	public Spawn()
	{
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x000D0B0C File Offset: 0x000CED0C
	public Spawn(SpawnPoint point)
	{
		this.pos = point.transform.position;
		this.euler = point.transform.eulerAngles;
	}

	// Token: 0x04001689 RID: 5769
	public Vector3 pos;

	// Token: 0x0400168A RID: 5770
	public Vector3 euler;
}
