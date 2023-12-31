using System;
using System.Reflection;

// Token: 0x020002C1 RID: 705
[Obfuscation(Exclude = true)]
[Flags]
public enum KillStreakEnum
{
	// Token: 0x04001709 RID: 5897
	none = 0,
	// Token: 0x0400170A RID: 5898
	kill = 1,
	// Token: 0x0400170B RID: 5899
	headshot = 2,
	// Token: 0x0400170C RID: 5900
	doubleKill = 4,
	// Token: 0x0400170D RID: 5901
	trippleKill = 8,
	// Token: 0x0400170E RID: 5902
	longShot = 16,
	// Token: 0x0400170F RID: 5903
	grenade = 32,
	// Token: 0x04001710 RID: 5904
	rage = 64,
	// Token: 0x04001711 RID: 5905
	storm = 128,
	// Token: 0x04001712 RID: 5906
	suicide = 256,
	// Token: 0x04001713 RID: 5907
	assist = 512,
	// Token: 0x04001714 RID: 5908
	prokill = 1024,
	// Token: 0x04001715 RID: 5909
	sitkill = 2048,
	// Token: 0x04001716 RID: 5910
	peepkill = 4096,
	// Token: 0x04001717 RID: 5911
	doubleHeadshot = 8192,
	// Token: 0x04001718 RID: 5912
	vipkill = 16384,
	// Token: 0x04001719 RID: 5913
	quadkill = 32768,
	// Token: 0x0400171A RID: 5914
	legendarykill = 65536
}
