using System;
using UnityEngine;

// Token: 0x020001E3 RID: 483
[AddComponentMenu("Scripts/Game/Components/WeaponLight")]
internal class WeaponLight : MonoBehaviour
{
	// Token: 0x06000FFD RID: 4093 RVA: 0x000B4E1C File Offset: 0x000B301C
	public void Init()
	{
		base.light.range = 2f + UnityEngine.Random.value * 2f;
		base.light.intensity = 1f + UnityEngine.Random.value * 1.5f;
	}
}
