using System;
using UnityEngine;

// Token: 0x020001E1 RID: 481
[AddComponentMenu("Scripts/Game/Components/UpperLeftPoint")]
internal class UpperLeftPoint : MonoBehaviour
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x000B4DFC File Offset: 0x000B2FFC
	private void Awake()
	{
		base.renderer.enabled = false;
	}
}
