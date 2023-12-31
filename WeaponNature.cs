using System;
using System.Reflection;

// Token: 0x0200028A RID: 650
[Obfuscation(Exclude = true)]
public enum WeaponNature
{
	// Token: 0x040014C3 RID: 5315
	any,
	// Token: 0x040014C4 RID: 5316
	none,
	// Token: 0x040014C5 RID: 5317
	knife,
	// Token: 0x040014C6 RID: 5318
	grenade = 4,
	// Token: 0x040014C7 RID: 5319
	pistol = 8,
	// Token: 0x040014C8 RID: 5320
	automatic_pistol = 16,
	// Token: 0x040014C9 RID: 5321
	automatic = 32,
	// Token: 0x040014CA RID: 5322
	assault_rifle = 64,
	// Token: 0x040014CB RID: 5323
	sniper_rifle = 128,
	// Token: 0x040014CC RID: 5324
	shotgun = 256,
	// Token: 0x040014CD RID: 5325
	machinegun = 512,
	// Token: 0x040014CE RID: 5326
	mortar = 1024
}
