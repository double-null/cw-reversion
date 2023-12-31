using System;
using System.Collections.Generic;

// Token: 0x02000357 RID: 855
internal class PlayerWeapon
{
	// Token: 0x06001C49 RID: 7241 RVA: 0x000FC014 File Offset: 0x000FA214
	public PlayerWeapon(Dictionary<ModType, int> mods)
	{
		this.WeaponMods = mods;
	}

	// Token: 0x0400210F RID: 8463
	public Dictionary<ModType, int> WeaponMods;
}
