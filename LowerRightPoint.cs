using System;
using UnityEngine;

// Token: 0x020001CA RID: 458
[AddComponentMenu("Scripts/Game/Components/LowerRightPoint")]
internal class LowerRightPoint : MonoBehaviour
{
	// Token: 0x06000F81 RID: 3969 RVA: 0x000B1C94 File Offset: 0x000AFE94
	private void Awake()
	{
		base.renderer.enabled = false;
	}
}
