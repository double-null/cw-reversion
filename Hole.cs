using System;
using UnityEngine;

// Token: 0x020001C8 RID: 456
[AddComponentMenu("Scripts/Game/Components/Hole")]
internal class Hole : PoolableBehaviour
{
	// Token: 0x06000F78 RID: 3960 RVA: 0x000B187C File Offset: 0x000AFA7C
	public override void OnPoolDespawn()
	{
		PoolItem[] componentsInChildren = base.gameObject.GetComponentsInChildren<PoolItem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			SingletoneForm<PoolManager>.Instance[componentsInChildren[i].name].Despawn(componentsInChildren[i]);
		}
		base.OnPoolDespawn();
	}
}
