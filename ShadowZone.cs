using System;
using UnityEngine;

// Token: 0x020001D0 RID: 464
[AddComponentMenu("Scripts/Game/Components/ShadowZone")]
public class ShadowZone : MonoBehaviour
{
	// Token: 0x06000F95 RID: 3989 RVA: 0x000B2438 File Offset: 0x000B0638
	private void Awake()
	{
		base.gameObject.layer = LayerMask.NameToLayer("shadow_zones");
	}

	// Token: 0x06000F96 RID: 3990 RVA: 0x000B2450 File Offset: 0x000B0650
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "shadowZone.png");
	}
}
