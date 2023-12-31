using System;
using System.Collections.Generic;

// Token: 0x02000253 RID: 595
[Serializable]
internal class ContractInfo : Convertible
{
	// Token: 0x06001230 RID: 4656 RVA: 0x000C7D78 File Offset: 0x000C5F78
	public ContractInfo()
	{
	}

	// Token: 0x06001231 RID: 4657 RVA: 0x000C7DB0 File Offset: 0x000C5FB0
	public ContractInfo(Dictionary<string, object> dict, string name, int index)
	{
		this._index = index;
		this._name = name;
		JSON.ReadWrite(dict, "id", ref this.id, false);
		JSON.ReadWriteEnum<ContractTaskType>(dict, "difficulty", ref this.DIFFCLTY, false);
		JSON.ReadWrite(dict, "task_order", ref this.task_order, false);
		JSON.ReadWrite(dict, "task_counter", ref this.task_counter, false);
		JSON.ReadWriteEnum<ContractType>(dict, "task_type", ref this.task_type, false);
		JSON.ReadWriteEnum<KillStreakEnum>(dict, "kill_type", ref this.kill_type, false);
		JSON.ReadWrite(dict, "victim_lvl", ref this.victimLVL, false);
		JSON.ReadWriteEnum<WeaponSpecific>(dict, "victim_class", ref this.victim_class, false);
		JSON.ReadWriteEnum<WeaponNature>(dict, "weapon_type", ref this.weapon_type, false);
		JSON.ReadWriteEnum<GameMode>(dict, "game_mode", ref this.game_mode, false);
		JSON.ReadWrite(dict, "prize_cr", ref this.prizeCR, false);
		JSON.ReadWrite(dict, "prize_gp", ref this.prizeGP, false);
		JSON.ReadWrite(dict, "prize_sp", ref this.prizeSP, false);
		JSON.ReadWrite(dict, "imgidx", ref this.imgidx, false);
		JSON.ReadWrite(dict, "weapon_id", ref this.WEAPON_ID, false);
		JSON.ReadWriteEnum<Maps>(dict, "task_map", ref this.MAP, false);
	}

	// Token: 0x17000276 RID: 630
	// (get) Token: 0x06001232 RID: 4658 RVA: 0x000C7F18 File Offset: 0x000C6118
	public string description
	{
		get
		{
			if (!string.IsNullOrEmpty(this._cachedDescription) && this._cachedLanguage == Language.CurrentLanguage)
			{
				return this._cachedDescription;
			}
			this._cachedDescription = (((Dictionary<string, object>[])Globals.I.contracts[this._name])[this._index]["description"] as string);
			this._cachedLanguage = Language.CurrentLanguage;
			return this._cachedDescription;
		}
	}

	// Token: 0x06001233 RID: 4659 RVA: 0x000C7F94 File Offset: 0x000C6194
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "current", ref this.current, isWrite);
		JSON.ReadWrite(dict, "task_order", ref this.task_order, isWrite);
		JSON.ReadWrite(dict, "contractEndTime", ref this.contractEndTime, isWrite);
	}

	// Token: 0x06001234 RID: 4660 RVA: 0x000C7FD8 File Offset: 0x000C61D8
	private bool IsSameMap(Maps map)
	{
		if (this.MAP == Maps.old_sawmill)
		{
			return map == Maps.old_sawmill || map == Maps.old_sawmill2;
		}
		return this.MAP == Maps.evac && (map == Maps.evac || map == Maps.evac2);
	}

	// Token: 0x06001235 RID: 4661 RVA: 0x000C8018 File Offset: 0x000C6218
	public bool Kill(WeaponNature weaponNature, ServerNetPlayer player, ServerNetPlayer target, AchievementTarget iBeef, AchievementKillType iKillType, KillStreakEnum iDifficult, Maps map, GameMode mode, int victimLvl, PlayerClass victimClass)
	{
		BaseWeapon baseWeapon = null;
		if (player.IsAlive)
		{
			baseWeapon = player.Ammo.CurrentWeapon;
		}
		KillStreakEnum killStreakEnum = KillStreakEnum.none;
		int num = this.kill_type - KillStreakEnum.kill;
		if (this.kill_type != KillStreakEnum.none)
		{
			killStreakEnum = (KillStreakEnum)(1 << num);
		}
		return (this.MAP == Maps.none || this.MAP == map || this.IsSameMap(map)) && (this.game_mode == GameMode.any || this.game_mode == mode) && this.victimLVL <= victimLvl && (this.victim_class == WeaponSpecific.any || Utility.TranslatePlayerClass(victimClass) == (int)this.victim_class) && (this.weapon_type == WeaponNature.any || this.weapon_type == weaponNature) && (this.WEAPON_ID == -1 || !(baseWeapon != null) || this.WEAPON_ID == (int)baseWeapon.type) && (this.WEAPON_ID == -1 || (weaponNature != WeaponNature.knife && weaponNature != WeaponNature.grenade)) && (killStreakEnum == KillStreakEnum.kill || (killStreakEnum == KillStreakEnum.headshot && iKillType == AchievementKillType.headshot) || (killStreakEnum == KillStreakEnum.headshot && iDifficult == KillStreakEnum.doubleHeadshot) || killStreakEnum == KillStreakEnum.kill || BIT.AND((int)killStreakEnum, (int)iDifficult));
	}

	// Token: 0x06001236 RID: 4662 RVA: 0x000C8174 File Offset: 0x000C6374
	public bool DisarmExplode(Maps map)
	{
		return this.MAP == Maps.none || this.MAP == map || this.IsSameMap(map);
	}

	// Token: 0x06001237 RID: 4663 RVA: 0x000C81A0 File Offset: 0x000C63A0
	public bool MatchResult(ServerNetPlayer player, GameMode mode, MatchResultInfo matchResult, Maps map)
	{
		if (this.MAP != Maps.none && this.MAP != map && !this.IsSameMap(map))
		{
			return false;
		}
		if (this.game_mode != GameMode.any && this.game_mode != mode)
		{
			return false;
		}
		if (this.task_type == ContractType.WinMatch)
		{
			if (Main.IsTeamGame)
			{
				if (!BIT.AND((int)matchResult, 16))
				{
					return false;
				}
			}
			else if (!BIT.AND((int)matchResult, 4))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x040011D7 RID: 4567
	public ContractTaskType DIFFCLTY;

	// Token: 0x040011D8 RID: 4568
	public ContractType task_type;

	// Token: 0x040011D9 RID: 4569
	public Maps MAP = Maps.none;

	// Token: 0x040011DA RID: 4570
	public GameMode game_mode = GameMode.none;

	// Token: 0x040011DB RID: 4571
	public WeaponNature weapon_type;

	// Token: 0x040011DC RID: 4572
	public WeaponSpecific victim_class;

	// Token: 0x040011DD RID: 4573
	public int victimLVL;

	// Token: 0x040011DE RID: 4574
	public KillStreakEnum kill_type;

	// Token: 0x040011DF RID: 4575
	public int task_counter = 1;

	// Token: 0x040011E0 RID: 4576
	public int prizeCR;

	// Token: 0x040011E1 RID: 4577
	public int prizeSP;

	// Token: 0x040011E2 RID: 4578
	public int prizeGP;

	// Token: 0x040011E3 RID: 4579
	private int _index;

	// Token: 0x040011E4 RID: 4580
	private string _name;

	// Token: 0x040011E5 RID: 4581
	private ELanguage _cachedLanguage;

	// Token: 0x040011E6 RID: 4582
	private string _cachedDescription;

	// Token: 0x040011E7 RID: 4583
	public ContractKillType killType = ContractKillType.kill;

	// Token: 0x040011E8 RID: 4584
	private int WEAPON_ID = -1;

	// Token: 0x040011E9 RID: 4585
	public int id;

	// Token: 0x040011EA RID: 4586
	public int imgidx;

	// Token: 0x040011EB RID: 4587
	public int current;

	// Token: 0x040011EC RID: 4588
	public int task_order;

	// Token: 0x040011ED RID: 4589
	public int contractEndTime;
}
