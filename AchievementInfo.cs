using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000240 RID: 576
[Serializable]
internal class AchievementInfo : Convertible
{
	// Token: 0x060011A8 RID: 4520 RVA: 0x000C4108 File Offset: 0x000C2308
	public AchievementInfo()
	{
	}

	// Token: 0x060011A9 RID: 4521 RVA: 0x000C4164 File Offset: 0x000C2364
	public AchievementInfo(Dictionary<string, object> dict, int index)
	{
		this._index = index;
		JSON.ReadWriteEnum<AchievementType>(dict, "type", ref this.type, false);
		JSON.ReadWrite(dict, "prize", ref this.prize, false);
		if (this.type == AchievementType.kill)
		{
			JSON.ReadWriteEnum<AchievementTarget>(dict, "target", ref this.target, false);
			JSON.ReadWriteEnum<AchievementKillType>(dict, "killType", ref this.killType, false);
			JSON.ReadWriteEnum<KillStreakEnum>(dict, "difficult", ref this.difficult, false);
			JSON.ReadWriteEnum<WeaponSpecific>(dict, "weaponType", ref this.weaponSpecific, false);
			JSON.ReadWriteEnum<WeaponNature>(dict, "weaponNature", ref this.weaponNature, false);
			JSON.ReadWrite(dict, "weaponsId", ref this.weaponsId, false);
			JSON.ReadWrite(dict, "hasOptic", ref this.hasOptic, false);
			JSON.ReadWrite(dict, "hasLTP", ref this.hasLTP, false);
			JSON.ReadWrite(dict, "hasKolimator", ref this.hasKolimator, false);
			JSON.ReadWrite(dict, "hasGrip", ref this.hasGrip, false);
			JSON.ReadWrite(dict, "hasSS", ref this.hasSS, false);
			JSON.ReadWrite(dict, "hasCompensator", ref this.hasCompensator, false);
			JSON.ReadWrite(dict, "hasSlug", ref this.hasSlug, false);
			JSON.ReadWriteEnum<WeaponSpecific>(dict, "enemyWeaponType", ref this.enemyWeaponSpecific, false);
			JSON.ReadWrite(dict, "atDeath", ref this.atDeath, false);
			JSON.ReadWrite(dict, "lastClip", ref this.lastClip, false);
			JSON.ReadWrite(dict, "count", ref this.count, false);
		}
		else if (this.type == AchievementType.matchResult)
		{
			JSON.ReadWriteEnum<GameMode>(dict, "GameMode", ref this.mode, false);
			JSON.ReadWriteEnum<MatchResultInfo>(dict, "MatchResultInfo", ref this.matchResult, false);
			JSON.ReadWrite(dict, "count", ref this.count, false);
		}
		else if (this.type == AchievementType.wtaskComplete)
		{
			JSON.ReadWrite(dict, "count", ref this.count, false);
		}
		else if (this.type == AchievementType.armoryUnlock)
		{
			JSON.ReadWriteEnum<WeaponBlockUnlocked>(dict, "WeaponBlockUnlocked", ref this.block, false);
			JSON.ReadWriteEnum<UnlockClassified>(dict, "UnlockClassified", ref this.classifier, false);
			JSON.ReadWrite(dict, "count", ref this.count, false);
		}
		else if (this.type == AchievementType.onlineTime)
		{
			JSON.ReadWrite(dict, "timeNeeded_Seconds", ref this.count, false);
		}
		else if (this.type == AchievementType.sonarDetection)
		{
			JSON.ReadWrite(dict, "count", ref this.count, false);
		}
	}

	// Token: 0x17000260 RID: 608
	// (get) Token: 0x060011AA RID: 4522 RVA: 0x000C4420 File Offset: 0x000C2620
	public string name
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedName;
		}
	}

	// Token: 0x17000261 RID: 609
	// (get) Token: 0x060011AB RID: 4523 RVA: 0x000C4430 File Offset: 0x000C2630
	public string description
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedDescription;
		}
	}

	// Token: 0x060011AC RID: 4524 RVA: 0x000C4440 File Offset: 0x000C2640
	private void RecacheStringsIfNeeded()
	{
		if (!string.IsNullOrEmpty(this._cachedName) && !string.IsNullOrEmpty(this._cachedDescription) && this._cachedlanguage == Language.CurrentLanguage)
		{
			return;
		}
		this._cachedName = (Globals.I.achievements[this._index]["name"] as string);
		this._cachedDescription = (Globals.I.achievements[this._index]["description"] as string);
		this._cachedlanguage = Language.CurrentLanguage;
	}

	// Token: 0x17000262 RID: 610
	// (get) Token: 0x060011AD RID: 4525 RVA: 0x000C44D8 File Offset: 0x000C26D8
	public bool Complete
	{
		get
		{
			return this.current >= this.count;
		}
	}

	// Token: 0x060011AE RID: 4526 RVA: 0x000C44EC File Offset: 0x000C26EC
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "current", ref this.current, isWrite);
	}

	// Token: 0x060011AF RID: 4527 RVA: 0x000C4500 File Offset: 0x000C2700
	public bool Kill(WeaponNature weaponNature, BaseWeapon killerWeapon, BaseWeapon iEnemyWeapon, AchievementTarget iBeef, AchievementKillType iKillType, KillStreakEnum iDifficult, bool iAtDeath, bool iLastClip)
	{
		bool flag = this.weaponNature == WeaponNature.none || BIT.AND((int)this.weaponNature, (int)weaponNature);
		if (weaponNature == WeaponNature.knife && !flag)
		{
			return false;
		}
		if (weaponNature == WeaponNature.grenade && !flag)
		{
			return false;
		}
		if (this.weaponNature != WeaponNature.none && !flag)
		{
			return false;
		}
		if (killerWeapon != null)
		{
			if (this.weaponSpecific != WeaponSpecific.none && !BIT.AND((int)this.weaponSpecific, (int)killerWeapon.weaponSpecific))
			{
				return false;
			}
			if (this.weaponsId != null && !(from id in this.weaponsId
			where id == (int)killerWeapon.type
			select id).Any<int>())
			{
				return false;
			}
			if (this.hasOptic && !killerWeapon.Optic)
			{
				return false;
			}
			if (this.hasLTP && !killerWeapon.LTP)
			{
				return false;
			}
			if (this.hasKolimator && !killerWeapon.Kolimator)
			{
				return false;
			}
			if (this.hasGrip && !killerWeapon.Grip)
			{
				return false;
			}
			if (this.hasSS && !killerWeapon.SS)
			{
				return false;
			}
			if (this.hasCompensator && !killerWeapon.Compensator)
			{
				return false;
			}
			if (this.hasSlug && !killerWeapon.Slug)
			{
				return false;
			}
		}
		return (!(iEnemyWeapon != null) || this.enemyWeaponSpecific == WeaponSpecific.none || BIT.AND((int)this.enemyWeaponSpecific, (int)iEnemyWeapon.weaponSpecific)) && (this.target == AchievementTarget.any || BIT.AND((int)this.target, (int)iBeef)) && (this.killType == AchievementKillType.kill || BIT.AND((int)this.killType, (int)iKillType)) && (this.difficult == KillStreakEnum.none || BIT.AND((int)this.difficult, (int)iDifficult)) && (!this.atDeath || iAtDeath) && (!this.lastClip || iLastClip);
	}

	// Token: 0x060011B0 RID: 4528 RVA: 0x000C4760 File Offset: 0x000C2960
	public bool MatchResult(GameMode iMode, MatchResultInfo iMatchResult, bool iNoDeaths)
	{
		return (this.mode == GameMode.none || this.mode == iMode) && (this.matchResult == MatchResultInfo.none || BIT.AND((int)this.matchResult, (int)iMatchResult)) && (!this.noDeaths || iNoDeaths);
	}

	// Token: 0x060011B1 RID: 4529 RVA: 0x000C47B8 File Offset: 0x000C29B8
	public bool WtaskComplete(int iWtaskOpenedCount)
	{
		return this.count == 0 || iWtaskOpenedCount >= this.count;
	}

	// Token: 0x060011B2 RID: 4530 RVA: 0x000C47D4 File Offset: 0x000C29D4
	public bool ArmoryUnlock(WeaponBlockUnlocked iBlock, UserInfo info)
	{
		if (this.block != WeaponBlockUnlocked.none && iBlock < this.block)
		{
			return false;
		}
		if (this.classifier != UnlockClassified.none)
		{
			int num = (int)this.block;
			if (this.classifier == UnlockClassified.pistols)
			{
				bool flag = true;
				int num2 = 0;
				for (int i = 0; i < info.weaponsStates.Length; i++)
				{
					WeaponInfo weaponInfo = info.weaponsStates[i];
					if (weaponInfo.GetWeapon.armoryBlock == num && weaponInfo.GetWeapon.weaponUseType == WeaponUseType.Secondary && !info.weaponsStates[i].Unlocked)
					{
						flag = false;
						break;
					}
					num2++;
				}
				if (this.count != 0 && this.count != num2)
				{
					return false;
				}
				if (!flag)
				{
					return false;
				}
			}
			if (this.classifier == UnlockClassified.automatic)
			{
				bool flag2 = true;
				int num3 = 0;
				for (int j = 0; j < info.weaponsStates.Length; j++)
				{
					WeaponInfo weaponInfo2 = info.weaponsStates[j];
					if (weaponInfo2.GetWeapon.armoryBlock == num && weaponInfo2.GetWeapon.weaponUseType == WeaponUseType.Primary && !info.weaponsStates[j].Unlocked)
					{
						flag2 = false;
						break;
					}
					num3++;
				}
				if (this.count != 0 && this.count != num3)
				{
					return false;
				}
				if (!flag2)
				{
					return false;
				}
			}
		}
		else
		{
			int num4 = 0;
			if (this.count != 0 && num4 < this.count)
			{
				return false;
			}
			int num5 = (int)this.block;
			num4 = 0;
			bool flag3 = true;
			for (int k = 0; k < info.weaponsStates.Length; k++)
			{
				WeaponInfo weaponInfo3 = info.weaponsStates[k];
				if (weaponInfo3.GetWeapon.armoryBlock == num5 && weaponInfo3.GetWeapon.weaponUseType == WeaponUseType.Secondary && !info.weaponsStates[k].Unlocked)
				{
					flag3 = false;
					break;
				}
				num4++;
			}
			if (this.count != 0 && this.count != num4)
			{
				return false;
			}
			if (!flag3)
			{
				return false;
			}
			flag3 = true;
			num4 = 0;
			for (int l = 0; l < info.weaponsStates.Length; l++)
			{
				WeaponInfo weaponInfo4 = info.weaponsStates[l];
				if (weaponInfo4.GetWeapon.armoryBlock == num5 && weaponInfo4.GetWeapon.weaponUseType == WeaponUseType.Primary && !info.weaponsStates[l].Unlocked)
				{
					flag3 = false;
					break;
				}
				num4++;
			}
			if (this.count != 0 && this.count != num4)
			{
				return false;
			}
			if (!flag3)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060011B3 RID: 4531 RVA: 0x000C4A9C File Offset: 0x000C2C9C
	public bool OnlineTime(int ITimeSeconds)
	{
		return this.count == 0 || ITimeSeconds >= this.count;
	}

	// Token: 0x0400112F RID: 4399
	public AchievementType type;

	// Token: 0x04001130 RID: 4400
	public int count = 1;

	// Token: 0x04001131 RID: 4401
	public int prize;

	// Token: 0x04001132 RID: 4402
	private int _index;

	// Token: 0x04001133 RID: 4403
	private ELanguage _cachedlanguage;

	// Token: 0x04001134 RID: 4404
	private string _cachedName;

	// Token: 0x04001135 RID: 4405
	private string _cachedDescription;

	// Token: 0x04001136 RID: 4406
	public int current;

	// Token: 0x04001137 RID: 4407
	private AchievementTarget target = AchievementTarget.any;

	// Token: 0x04001138 RID: 4408
	private AchievementKillType killType = AchievementKillType.kill;

	// Token: 0x04001139 RID: 4409
	public KillStreakEnum difficult;

	// Token: 0x0400113A RID: 4410
	private WeaponSpecific weaponSpecific = WeaponSpecific.none;

	// Token: 0x0400113B RID: 4411
	public WeaponNature weaponNature = WeaponNature.none;

	// Token: 0x0400113C RID: 4412
	private int[] weaponsId;

	// Token: 0x0400113D RID: 4413
	private bool hasOptic;

	// Token: 0x0400113E RID: 4414
	private bool hasLTP;

	// Token: 0x0400113F RID: 4415
	private bool hasKolimator;

	// Token: 0x04001140 RID: 4416
	private bool hasGrip;

	// Token: 0x04001141 RID: 4417
	private bool hasSS;

	// Token: 0x04001142 RID: 4418
	private bool hasCompensator;

	// Token: 0x04001143 RID: 4419
	private bool hasSlug;

	// Token: 0x04001144 RID: 4420
	private WeaponSpecific enemyWeaponSpecific = WeaponSpecific.none;

	// Token: 0x04001145 RID: 4421
	private bool atDeath;

	// Token: 0x04001146 RID: 4422
	private bool lastClip;

	// Token: 0x04001147 RID: 4423
	private GameMode mode = GameMode.none;

	// Token: 0x04001148 RID: 4424
	private MatchResultInfo matchResult = MatchResultInfo.none;

	// Token: 0x04001149 RID: 4425
	private bool noDeaths;

	// Token: 0x0400114A RID: 4426
	private WeaponBlockUnlocked block = WeaponBlockUnlocked.none;

	// Token: 0x0400114B RID: 4427
	private UnlockClassified classifier;
}
