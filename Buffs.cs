using System;

// Token: 0x02000264 RID: 612
[Flags]
public enum Buffs
{
	// Token: 0x04001268 RID: 4712
	none = 1,
	// Token: 0x04001269 RID: 4713
	fire = 2,
	// Token: 0x0400126A RID: 4714
	immortal = 4,
	// Token: 0x0400126B RID: 4715
	health_boost = 8,
	// Token: 0x0400126C RID: 4716
	placement = 16,
	// Token: 0x0400126D RID: 4717
	beacon_user = 32,
	// Token: 0x0400126E RID: 4718
	is_night = 64,
	// Token: 0x0400126F RID: 4719
	VIP = 128,
	// Token: 0x04001270 RID: 4720
	db_load = 256,
	// Token: 0x04001271 RID: 4721
	db_save = 512,
	// Token: 0x04001272 RID: 4722
	unblicable = 1024,
	// Token: 0x04001273 RID: 4723
	binocular = 2048,
	// Token: 0x04001274 RID: 4724
	hilighted = 4096,
	// Token: 0x04001275 RID: 4725
	clanAllProfs = 8192,
	// Token: 0x04001276 RID: 4726
	tactical_point_4 = 16384,
	// Token: 0x04001277 RID: 4727
	bruno_helix = 32768,
	// Token: 0x04001278 RID: 4728
	defender = 65536,
	// Token: 0x04001279 RID: 4729
	bearBaseHurt = 131072,
	// Token: 0x0400127A RID: 4730
	usecBaseHurt = 262144,
	// Token: 0x0400127B RID: 4731
	brokenLeg = 524288,
	// Token: 0x0400127C RID: 4732
	brokenHand = 1048576,
	// Token: 0x0400127D RID: 4733
	bleed = 2097152,
	// Token: 0x0400127E RID: 4734
	exp_disabled = 4194304,
	// Token: 0x0400127F RID: 4735
	tasks_disabled = 8388608,
	// Token: 0x04001280 RID: 4736
	leader_exp = 16777216,
	// Token: 0x04001281 RID: 4737
	leader_health = 33554432,
	// Token: 0x04001282 RID: 4738
	leader_regen = 67108864,
	// Token: 0x04001283 RID: 4739
	destroyer_add_mag = 134217728,
	// Token: 0x04001284 RID: 4740
	gunsmith_repair = 268435456,
	// Token: 0x04001285 RID: 4741
	clan_squad = 536870912
}
