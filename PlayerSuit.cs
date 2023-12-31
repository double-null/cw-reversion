using System;
using System.Collections.Generic;

// Token: 0x02000356 RID: 854
internal class PlayerSuit
{
	// Token: 0x06001C48 RID: 7240 RVA: 0x000FC004 File Offset: 0x000FA204
	public PlayerSuit(Dictionary<int, PlayerWeapon> weapons)
	{
		this.PlayerWeapons = weapons;
	}

	// Token: 0x0400210E RID: 8462
	public Dictionary<int, PlayerWeapon> PlayerWeapons;
}
