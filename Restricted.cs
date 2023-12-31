using System;
using UnityEngine;

// Token: 0x020001CF RID: 463
[AddComponentMenu("Scripts/Game/Components/Restricted")]
internal class Restricted : MonoBehaviour
{
	// Token: 0x06000F92 RID: 3986 RVA: 0x000B23F8 File Offset: 0x000B05F8
	private void Awake()
	{
		if (base.renderer != null)
		{
			base.renderer.enabled = false;
		}
	}

	// Token: 0x06000F93 RID: 3987 RVA: 0x000B2418 File Offset: 0x000B0618
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "restricted13.png");
	}
}
