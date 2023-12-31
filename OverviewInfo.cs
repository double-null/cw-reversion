using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002A0 RID: 672
[Serializable]
internal class OverviewInfo
{
	// Token: 0x060012DF RID: 4831 RVA: 0x000CC7AC File Offset: 0x000CA9AC
	public OverviewInfo(bool forceInit = false)
	{
		this.suits = new SuitInfo[5];
		if (!forceInit)
		{
			return;
		}
		for (int i = 0; i < this.suits.Length; i++)
		{
			this.suits[i] = new SuitInfo(true);
		}
		this.weaponsStates = new WeaponInfo[Globals.I.weapons.Length];
		for (int j = 0; j < this.weaponsStates.Length; j++)
		{
			this.weaponsStates[j] = new WeaponInfo();
			this.weaponsStates[j].Init(false, j);
		}
		this.skillsInfos = new SkillInfo[Globals.I.skills.Length];
		for (int k = 0; k < this.skillsInfos.Length; k++)
		{
			this.skillsInfos[k] = new SkillInfo(Globals.I.skills[k], k);
		}
		this.achievementsInfos = new AchievementInfo[Globals.I.achievements.Length];
		for (int l = 0; l < this.achievementsInfos.Length; l++)
		{
			this.achievementsInfos[l] = new AchievementInfo(Globals.I.achievements[l], l);
		}
		this.contractsInfo = new ContractsState(Globals.I.contracts);
		this.packageInfo = ((Globals.I.packages != null) ? new PackagesInfo[PackagesInfo.GetSize(Globals.I.packages)] : new PackagesInfo[7]);
		for (int m = 0; m < this.packageInfo.Length; m++)
		{
			this.packageInfo[m] = new PackagesInfo(Globals.I.packages, m);
		}
	}

	// Token: 0x060012E0 RID: 4832 RVA: 0x000CCA84 File Offset: 0x000CAC84
	public bool skillUnlocked(Skills skill)
	{
		if (Peer.HardcoreMode)
		{
			return skill == Skills.car_block || skill == Skills.att3 || skill == Skills.marks || skill == Skills.uniq_p || skill == Skills.uniq_pp || skill == Skills.spec_mp5 || skill == Skills.uniq_rifle || skill == Skills.uniq_shot || skill == Skills.uniq_mg || skill == Skills.uniq_sni;
		}
		return this.skillsInfos != null && skill >= Skills.analyze1 && skill < (Skills)this.skillsInfos.Length && this.skillsInfos[(int)skill].Unlocked;
	}

	// Token: 0x170002A8 RID: 680
	// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000CCB24 File Offset: 0x000CAD24
	public string sClanRole
	{
		get
		{
			int clanRole = this._clanRole;
			switch (clanRole)
			{
			case 2:
				return Language.ClansOfficer;
			default:
				if (clanRole != 8)
				{
					return Language.ClansContractor;
				}
				return Language.ClansLeader;
			case 4:
				return Language.ClansLieutenant;
			}
		}
	}

	// Token: 0x170002A9 RID: 681
	// (get) Token: 0x060012E2 RID: 4834 RVA: 0x000CCB70 File Offset: 0x000CAD70
	public int PlayerLevel
	{
		get
		{
			return this._playerLevel;
		}
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x000CCB78 File Offset: 0x000CAD78
	private void RefreshPlayerLevel()
	{
		this._playerLevel = OverviewInfo.GetLevel((float)this.currentXP);
	}

	// Token: 0x060012E4 RID: 4836 RVA: 0x000CCB90 File Offset: 0x000CAD90
	private void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "social_id", ref this.socialID, isWrite);
		JSON.ReadWrite(dict, "username", ref this.nick, isWrite);
		this.nick = this.nick.Replace("\n", string.Empty);
		JSON.ReadWrite(dict, "perk_states", ref this.nickColor, isWrite);
		if (!this.nickColor.StartsWith("#"))
		{
			this.nickColor = "#" + this.nickColor;
		}
		JSON.ReadWrite(dict, "currentXP", ref this.currentXP, isWrite);
		if (!isWrite)
		{
			this.RefreshPlayerLevel();
		}
		JSON.ReadWrite(dict, "killCount", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "deathCount", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "winCount", ref this.winCount, isWrite);
		JSON.ReadWrite(dict, "lossCount", ref this.lossCount, isWrite);
		JSON.ReadWrite(dict, "hcKillCount", ref this.hcKillCount, isWrite);
		JSON.ReadWrite(dict, "hcDeathCount", ref this.hcDeathCount, isWrite);
		JSON.ReadWrite(dict, "cr", ref this.CR, isWrite);
		JSON.ReadWrite(dict, "gp", ref this.GP, isWrite);
		JSON.ReadWrite(dict, "sp", ref this.SP, isWrite);
		JSON.ReadWrite(dict, "repa", ref this.repa, isWrite);
		JSON.ReadWrite(dict, "clanTag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "clanID", ref this.clanID, isWrite);
		JSON.ReadWrite(dict, "clan_earn", ref this.ClanEarn, isWrite);
		JSON.ReadWrite(dict, "clan_earn_proc", ref this.ClanEarnProc, isWrite);
		JSON.ReadWrite(dict, "clan_role", ref this._clanRole, isWrite);
		JSON.ReadWrite(dict, "clan_place", ref this.ClanPlace, isWrite);
		JSON.ReadWrite(dict, "clan_name", ref this.ClanName, isWrite);
		this.ConvertAwards(dict, isWrite);
		if (this.banned == null)
		{
			this.banned = new Int();
		}
		if (this.bannedUntil == null)
		{
			this.bannedUntil = new Int();
		}
		JSON.ReadWrite(dict, "banned", ref this.banned, isWrite);
		JSON.ReadWrite(dict, "banned_until", ref this.bannedUntil, isWrite);
		JSON.ReadWrite(dict, "reason", ref this.bannedReason, isWrite);
		ArrayUtility.AdjustArraySize<SuitInfo>(ref this.suits, 5);
		for (int i = 0; i < this.suits.Length; i++)
		{
			int secondaryIndex = this.suits[i].secondaryIndex;
			int primaryIndex = this.suits[i].primaryIndex;
			bool secondaryMod = this.suits[i].secondaryMod;
			bool primaryMod = this.suits[i].primaryMod;
			JSON.ReadWrite<SuitInfo>(dict, "info" + i, ref this.suits[i], isWrite);
			if (Main.IsGameLoaded && this == Main.UserInfo && !isWrite)
			{
				this.suits[i].secondaryIndex = secondaryIndex;
				this.suits[i].primaryIndex = primaryIndex;
				this.suits[i].secondaryMod = secondaryMod;
				this.suits[i].primaryMod = primaryMod;
			}
			if (this.suits[i].secondaryIndex != 127 && !this.weaponsStates[this.suits[i].secondaryIndex].Unlocked)
			{
				this.suits[i].secondaryIndex = 0;
			}
			if (this.suits[i].primaryIndex != 127 && !this.weaponsStates[this.suits[i].primaryIndex].Unlocked)
			{
				this.suits[i].primaryIndex = 127;
			}
			if (this.suits[i].secondaryIndex != 127 && this.weaponsStates[this.suits[i].secondaryIndex].CurrentWeapon.weaponUseType == WeaponUseType.Primary)
			{
				this.suits[i].secondaryIndex = 0;
			}
			if (this.suits[i].primaryIndex != 127 && this.weaponsStates[this.suits[i].primaryIndex].CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
			{
				this.suits[i].primaryIndex = 127;
			}
			for (int j = 0; j < Math.Min(this.weaponsStates.Length, Globals.I.weapons.Length); j++)
			{
				if (Utility.IsModableWeapon((int)this.weaponsStates[j].CurrentWeapon.type))
				{
					this.suits[i].SetWtask(j, false);
				}
				else if (!this.weaponsStates[j].wtaskUnlocked && this.suits[i].GetWtask(j))
				{
					this.suits[i].SetWtask(j, false);
				}
			}
		}
		this.iWtaskCount = 0;
		for (int k = 0; k < Math.Min(this.weaponsStates.Length, Globals.I.weapons.Length); k++)
		{
			if (!this.weaponsStates[k].CurrentWeapon.isPremium)
			{
				this.iWtaskCount++;
			}
		}
		if (!isWrite)
		{
			if (!this.suits[this.suitNameIndex].Unlocked)
			{
				this.suitNameIndex = 0;
			}
			this.suits[0].Unlocked = true;
		}
	}

	// Token: 0x060012E5 RID: 4837 RVA: 0x000CD0E4 File Offset: 0x000CB2E4
	protected void ConvertAwards(Dictionary<string, object> dict, bool isWrite)
	{
		if (isWrite)
		{
			if (dict.ContainsKey("awards"))
			{
				dict["awards"] = this.Awards;
			}
			else
			{
				dict.Add("awards", this.Awards);
			}
		}
		else if (this.userID != IDUtil.BotID)
		{
			JSON.ConvertDict<int[]>(dict["awards"], ref this.Awards, new Func<object, int[]>(this.ParseAward));
		}
	}

	// Token: 0x060012E6 RID: 4838 RVA: 0x000CD16C File Offset: 0x000CB36C
	private int[] ParseAward(object data)
	{
		string[] array = data as string[];
		if (array == null)
		{
			Debug.LogError("error parsing award");
			return null;
		}
		int[] array2 = new int[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array2[i] = int.Parse(array[i]);
		}
		return array2;
	}

	// Token: 0x060012E7 RID: 4839 RVA: 0x000CD1BC File Offset: 0x000CB3BC
	private void Convert(Dictionary<string, object> dict, bool isWrite, bool WPAK_Needed)
	{
		if (WPAK_Needed)
		{
			ArrayUtility.AdjustArraySize<WeaponInfo>(ref this.weaponsStates, Globals.I.weapons.Length);
			ArrayUtility.AdjustArraySize<SkillInfo>(ref this.skillsInfos, Globals.I.skills.Length);
			ArrayUtility.AdjustArraySize<AchievementInfo>(ref this.achievementsInfos, Globals.I.achievements.Length);
			JSON.ReadWrite<SkillInfo>(dict, "skills", ref this.skillsInfos, isWrite);
			this.playerClass = this.GetPlayerClass;
			JSON.ReadWrite<ContractsState>(dict, "contracts", ref this.contractsInfo, isWrite);
			JSON.ReadWrite<WeaponInfo>(dict, "weapons", ref this.weaponsStates, isWrite);
			int[] array = new int[0];
			ArrayUtility.AdjustArraySize<int>(ref array, this.achievementsInfos.Length);
			JSON.ReadWrite(dict, "achievements", ref array, isWrite);
			for (int i = 0; i < array.Length; i++)
			{
				this.achievementsInfos[i].current = array[i];
			}
		}
		this.Convert(dict, isWrite);
	}

	// Token: 0x060012E8 RID: 4840 RVA: 0x000CD2A8 File Offset: 0x000CB4A8
	public void Read(Dictionary<string, object> dict, bool WPAK)
	{
		this.userStats.Read((Dictionary<string, object>)dict["stats"]);
		this.socialInfo.Read((Dictionary<string, object>)dict["social"]);
		this.Convert((Dictionary<string, object>)dict["data"], false, WPAK);
	}

	// Token: 0x170002AA RID: 682
	// (get) Token: 0x060012E9 RID: 4841 RVA: 0x000CD304 File Offset: 0x000CB504
	public int CurrentLevel
	{
		get
		{
			return OverviewInfo.GetLevel((float)this.currentXP);
		}
	}

	// Token: 0x060012EA RID: 4842 RVA: 0x000CD318 File Offset: 0x000CB518
	public static float MaxXp(float exp)
	{
		return (float)Globals.I.expTable[OverviewInfo.GetLevel(exp) + 1];
	}

	// Token: 0x060012EB RID: 4843 RVA: 0x000CD330 File Offset: 0x000CB530
	public static float MinXp(float exp)
	{
		return (float)Globals.I.expTable[OverviewInfo.GetLevel(exp)];
	}

	// Token: 0x170002AB RID: 683
	// (get) Token: 0x060012EC RID: 4844 RVA: 0x000CD344 File Offset: 0x000CB544
	private PlayerClass GetPlayerClass
	{
		get
		{
			int[] array = new int[7];
			int num = 0;
			for (int i = 0; i < this.skillsInfos.Length; i++)
			{
				if (this.skillsInfos[i].Unlocked)
				{
					if (this.skillsInfos[i].type == Skills.car_expbonus3)
					{
						array[5] += this.skillsInfos[i].SP.Value;
					}
					else
					{
						array[(int)Utility.convertFromWeaponSpecific(this.skillsInfos[i].classType)] += this.skillsInfos[i].SP.Value;
					}
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] > num)
				{
					num = array[j];
					this.pClass = (PlayerClass)j;
				}
			}
			return this.pClass;
		}
	}

	// Token: 0x060012ED RID: 4845 RVA: 0x000CD420 File Offset: 0x000CB620
	public static int GetLevel(float exp)
	{
		for (int i = 0; i < Globals.I.expTable.Length; i++)
		{
			if (exp < (float)Globals.I.expTable[i])
			{
				return i - 1;
			}
		}
		return Globals.I.expTable.Length - 1;
	}

	// Token: 0x170002AC RID: 684
	// (get) Token: 0x060012EE RID: 4846 RVA: 0x000CD470 File Offset: 0x000CB670
	public int WtaskOpenedCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.weaponsStates.Length; i++)
			{
				if (this.weaponsStates[i].wtaskUnlocked)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x170002AD RID: 685
	// (get) Token: 0x060012EF RID: 4847 RVA: 0x000CD4B0 File Offset: 0x000CB6B0
	public int premiumWeapUnlocked
	{
		get
		{
			int num = 0;
			for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
			{
				if (Main.UserInfo.weaponsStates[i].Unlocked && Main.UserInfo.weaponsStates[i].rentEnd == -1 && Main.UserInfo.weaponsStates[i].CurrentWeapon.isPremium)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x040015BC RID: 5564
	public Int winCount = new Int();

	// Token: 0x040015BD RID: 5565
	public Int lossCount = new Int();

	// Token: 0x040015BE RID: 5566
	public Float currentXP = new Float(0f);

	// Token: 0x040015BF RID: 5567
	public string nick = Language.ProfileNotLoaded;

	// Token: 0x040015C0 RID: 5568
	public string nickColor = "#FFFFFF";

	// Token: 0x040015C1 RID: 5569
	public Int killCount = new Int();

	// Token: 0x040015C2 RID: 5570
	public Int deathCount = new Int();

	// Token: 0x040015C3 RID: 5571
	public Int hcKillCount = new Int();

	// Token: 0x040015C4 RID: 5572
	public Int hcDeathCount = new Int();

	// Token: 0x040015C5 RID: 5573
	public Int CR = new Int();

	// Token: 0x040015C6 RID: 5574
	public Int GP = new Int();

	// Token: 0x040015C7 RID: 5575
	public Int SP = new Int();

	// Token: 0x040015C8 RID: 5576
	public Int suitNameIndex = new Int();

	// Token: 0x040015C9 RID: 5577
	public Int userID = new Int(IDUtil.NoID);

	// Token: 0x040015CA RID: 5578
	public string socialID = string.Empty;

	// Token: 0x040015CB RID: 5579
	public string socialUsername = string.Empty;

	// Token: 0x040015CC RID: 5580
	public string clanTag = string.Empty;

	// Token: 0x040015CD RID: 5581
	public int clanID;

	// Token: 0x040015CE RID: 5582
	public Float repa = new Float();

	// Token: 0x040015CF RID: 5583
	public Int ratingPlace = new Int();

	// Token: 0x040015D0 RID: 5584
	public Int banned;

	// Token: 0x040015D1 RID: 5585
	public Int bannedUntil = new Int();

	// Token: 0x040015D2 RID: 5586
	public string bannedReason = string.Empty;

	// Token: 0x040015D3 RID: 5587
	public Stats userStats = new Stats();

	// Token: 0x040015D4 RID: 5588
	public global::Social socialInfo = new global::Social();

	// Token: 0x040015D5 RID: 5589
	public ContractsState contractsInfo = new ContractsState();

	// Token: 0x040015D6 RID: 5590
	public WeaponInfo[] weaponsStates;

	// Token: 0x040015D7 RID: 5591
	public CardInfo[] cardsInfos;

	// Token: 0x040015D8 RID: 5592
	public Dictionary<string, int[]> Awards;

	// Token: 0x040015D9 RID: 5593
	public PackagesInfo[] packageInfo;

	// Token: 0x040015DA RID: 5594
	public float ClanEarn;

	// Token: 0x040015DB RID: 5595
	public float ClanEarnProc;

	// Token: 0x040015DC RID: 5596
	protected int _clanRole;

	// Token: 0x040015DD RID: 5597
	public int ClanPlace;

	// Token: 0x040015DE RID: 5598
	public string ClanName = string.Empty;

	// Token: 0x040015DF RID: 5599
	public PlayerClass playerClass;

	// Token: 0x040015E0 RID: 5600
	public PlayerClass pClass;

	// Token: 0x040015E1 RID: 5601
	public int _playerLevel;

	// Token: 0x040015E2 RID: 5602
	public int Attempts;

	// Token: 0x040015E3 RID: 5603
	public bool WonAttempt;

	// Token: 0x040015E4 RID: 5604
	public SuitInfo[] suits;

	// Token: 0x040015E5 RID: 5605
	public List<int> WatchlistUsersId = new List<int>();

	// Token: 0x040015E6 RID: 5606
	public AchievementInfo[] achievementsInfos;

	// Token: 0x040015E7 RID: 5607
	public SkillInfo[] skillsInfos;

	// Token: 0x040015E8 RID: 5608
	protected int iWtaskCount;
}
