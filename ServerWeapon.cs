using System;
using UnityEngine;

// Token: 0x020002F3 RID: 755
[AddComponentMenu("Scripts/Game/ServerWeapon")]
internal class ServerWeapon : BaseWeapon
{
	// Token: 0x06001586 RID: 5510 RVA: 0x000E23F4 File Offset: 0x000E05F4
	protected override void OnFire()
	{
		(this.player as ServerNetPlayer).UserInfo.weaponsStates[(int)this.type].repair_info = this.state.repair_info;
		base.OnFire();
	}
}
