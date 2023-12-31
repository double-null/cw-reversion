using System;
using UnityEngine;

// Token: 0x020001C3 RID: 451
[AddComponentMenu("Scripts/Game/Components/DamageX")]
internal class DamageX : MonoBehaviour
{
	// Token: 0x06000F6B RID: 3947 RVA: 0x000B1174 File Offset: 0x000AF374
	public void Restore()
	{
	}

	// Token: 0x06000F6C RID: 3948 RVA: 0x000B1178 File Offset: 0x000AF378
	public void ShowDebug()
	{
		Debug.Log("Hitted" + this.player.playerInfo.Nick + " to  " + base.gameObject.name);
	}

	// Token: 0x04000FC0 RID: 4032
	public float X = 1f;

	// Token: 0x04000FC1 RID: 4033
	[HideInInspector]
	public BaseNetPlayer player;

	// Token: 0x04000FC2 RID: 4034
	[HideInInspector]
	public bool Armor;
}
