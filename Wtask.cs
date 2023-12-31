using System;
using System.Collections.Generic;

// Token: 0x020002E0 RID: 736
[Serializable]
internal class Wtask : Convertible
{
	// Token: 0x06001479 RID: 5241 RVA: 0x000D8AD8 File Offset: 0x000D6CD8
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWriteEnum<AchievementType>(dict, "type", ref this.type, isWrite);
		if (this.type == AchievementType.kill)
		{
			JSON.ReadWrite(dict, "count", ref this.count, isWrite);
			JSON.ReadWriteEnum<AchievementTarget>(dict, "beef", ref this.beef, isWrite);
			JSON.ReadWriteEnum<AchievementKillType>(dict, "killType", ref this.killType, isWrite);
			JSON.ReadWriteEnum<KillStreakEnum>(dict, "difficult", ref this.difficult, isWrite);
			JSON.ReadWriteEnum<WeaponSpecific>(dict, "weaponType", ref this.weaponType, isWrite);
			JSON.ReadWrite(dict, "atDeath", ref this.atDeath, isWrite);
			JSON.ReadWrite(dict, "lastClip", ref this.lastClip, isWrite);
		}
	}

	// Token: 0x0600147A RID: 5242 RVA: 0x000D8B80 File Offset: 0x000D6D80
	public bool Kill(AchievementTarget iBeef, AchievementKillType iKillType, KillStreakEnum iDifficult, bool iAtDeath, bool iLastClip, WeaponSpecific iWeaponType)
	{
		return (this.beef == AchievementTarget.any || BIT.AND((int)this.beef, (int)iBeef)) && (this.killType == AchievementKillType.kill || BIT.AND((int)this.killType, (int)iKillType)) && (this.difficult == KillStreakEnum.none || BIT.AND((int)this.difficult, (int)iDifficult)) && (!this.atDeath || iAtDeath) && (!this.lastClip || iLastClip) && (this.weaponType == WeaponSpecific.none || BIT.AND((int)this.weaponType, (int)iWeaponType));
	}

	// Token: 0x04001931 RID: 6449
	public int count;

	// Token: 0x04001932 RID: 6450
	private AchievementType type;

	// Token: 0x04001933 RID: 6451
	private AchievementTarget beef = AchievementTarget.any;

	// Token: 0x04001934 RID: 6452
	private AchievementKillType killType = AchievementKillType.kill;

	// Token: 0x04001935 RID: 6453
	private KillStreakEnum difficult;

	// Token: 0x04001936 RID: 6454
	private WeaponSpecific weaponType = WeaponSpecific.none;

	// Token: 0x04001937 RID: 6455
	private bool atDeath;

	// Token: 0x04001938 RID: 6456
	private bool lastClip;
}
