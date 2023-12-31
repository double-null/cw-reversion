using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Foundation;

// Token: 0x020002D9 RID: 729
[Serializable]
internal class UserInfo : OverviewInfo, Convertible, ICloneable
{
	// Token: 0x0600142A RID: 5162 RVA: 0x000D65E8 File Offset: 0x000D47E8
	public UserInfo(bool forceInit = false)
	{
		int[] array = new int[7];
		array[0] = 1;
		this.unlockedSets = array;
		this.settings = new UserSettings();
		this.sp_available = new Int();
		this.BG = new Int();
		this.clanData = new ClanSystemData();
		this.Violation = new Int(1);
		this.Mastering = new MasteringInfo();
		this.HashID = string.Empty;
		base..ctor(false);
		this._intPermission = new Int();
		this.banned = new Int();
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
		if (Globals.I.packages == null)
		{
			this.packageInfo = new PackagesInfo[7];
		}
		else
		{
			this.packageInfo = new PackagesInfo[PackagesInfo.GetSize(Globals.I.packages)];
		}
		for (int m = 0; m < this.packageInfo.Length; m++)
		{
			this.packageInfo[m] = new PackagesInfo(Globals.I.packages, m);
		}
		if (Globals.I.ClanSkills.Length > 0)
		{
			this.ClanSkillsInfos = new ClanSkillInfo[Globals.I.ClanSkills.Length];
			for (int n = 0; n < this.ClanSkillsInfos.Length; n++)
			{
				this.ClanSkillsInfos[n] = new ClanSkillInfo(Globals.I.ClanSkills[n], n);
			}
		}
		this.CurrencyInfo = new CurrencyInfo();
		this.CurrentMpExp = Main.UserInfo.Mastering.CurrentExp;
	}

	// Token: 0x170002F8 RID: 760
	// (get) Token: 0x0600142B RID: 5163 RVA: 0x000D68F8 File Offset: 0x000D4AF8
	public EPermission Permission
	{
		get
		{
			if (this._cachedIntPermission == this._intPermission.Value)
			{
				return this._cachedPermission;
			}
			this._cachedIntPermission = this._intPermission.Value;
			this._cachedPermission = (EPermission)this._intPermission.Value;
			return this._cachedPermission;
		}
	}

	// Token: 0x170002F9 RID: 761
	// (get) Token: 0x0600142C RID: 5164 RVA: 0x000D694C File Offset: 0x000D4B4C
	public bool ProfileLoaded
	{
		get
		{
			return this._profileLoaded;
		}
	}

	// Token: 0x170002FA RID: 762
	// (get) Token: 0x0600142D RID: 5165 RVA: 0x000D6954 File Offset: 0x000D4B54
	// (set) Token: 0x0600142E RID: 5166 RVA: 0x000D6990 File Offset: 0x000D4B90
	public Role ClanRole
	{
		get
		{
			int clanRole = this._clanRole;
			switch (clanRole)
			{
			case 2:
				return Role.officer;
			default:
				if (clanRole != 8)
				{
					return Role.contractor;
				}
				return Role.leader;
			case 4:
				return Role.lt;
			}
		}
		set
		{
			this._clanRole = (int)value;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x0600142F RID: 5167 RVA: 0x000D699C File Offset: 0x000D4B9C
	public new string sClanRole
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

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x06001430 RID: 5168 RVA: 0x000D69E8 File Offset: 0x000D4BE8
	public new int PlayerLevel
	{
		get
		{
			return this._playerLevel;
		}
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x000D69F0 File Offset: 0x000D4BF0
	public void RefreshPlayerLevel()
	{
		this._playerLevel = this.getLevel((float)this.currentXP);
	}

	// Token: 0x170002FD RID: 765
	// (get) Token: 0x06001432 RID: 5170 RVA: 0x000D6A0C File Offset: 0x000D4C0C
	public float PercentForNextLevel
	{
		get
		{
			float num;
			float num2;
			if (this.PlayerLevel == 0)
			{
				num = (float)Globals.I.expTable[this.PlayerLevel + 1];
				num2 = this.currentXP.Value;
			}
			else
			{
				num = (float)(Globals.I.expTable[this.PlayerLevel + 1] - Globals.I.expTable[this.PlayerLevel]);
				num2 = this.currentXP.Value - (float)Globals.I.expTable[this.PlayerLevel];
			}
			if (num == 0f)
			{
				num = 1f;
			}
			return num2 / num;
		}
	}

	// Token: 0x170002FE RID: 766
	// (get) Token: 0x06001433 RID: 5171 RVA: 0x000D6AB0 File Offset: 0x000D4CB0
	public float PercentForNextMpLevel
	{
		get
		{
			return (float)this.CurrentMpExp / (float)((Globals.I.MasteringExpToNextPoint != 0) ? Globals.I.MasteringExpToNextPoint : 1);
		}
	}

	// Token: 0x170002FF RID: 767
	// (get) Token: 0x06001434 RID: 5172 RVA: 0x000D6AE8 File Offset: 0x000D4CE8
	// (set) Token: 0x06001435 RID: 5173 RVA: 0x000D6AF0 File Offset: 0x000D4CF0
	public string HopsSecretKey { get; set; }

	// Token: 0x17000300 RID: 768
	// (get) Token: 0x06001436 RID: 5174 RVA: 0x000D6AFC File Offset: 0x000D4CFC
	// (set) Token: 0x06001437 RID: 5175 RVA: 0x000D6B04 File Offset: 0x000D4D04
	public string HopsRoulettePrizeKey { get; set; }

	// Token: 0x06001438 RID: 5176 RVA: 0x000D6B10 File Offset: 0x000D4D10
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "social_id", ref this.socialID, isWrite);
		JSON.ReadWrite(dict, "username", ref this.nick, isWrite);
		JSON.ReadWrite(dict, "nickname_color", ref this.nickColor, isWrite);
		if (!this.nickColor.StartsWith("#"))
		{
			this.nickColor = "#" + this.nickColor;
		}
		JSON.ReadWrite(dict, "currentXP", ref this.currentXP, isWrite);
		JSON.ReadWrite(dict, "delta_xp", ref this.DeltaExp, isWrite);
		JSON.ReadWrite(dict, "delta_cr", ref this.DeltaCr, isWrite);
		if (!isWrite)
		{
			this.RefreshPlayerLevel();
		}
		JSON.ReadWrite(dict, "killCount", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "deathCount", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "winCount", ref this.winCount, isWrite);
		JSON.ReadWrite(dict, "lossCount", ref this.lossCount, isWrite);
		JSON.ReadWrite(dict, "hcCurrentXP", ref this.hcCurrentXP, isWrite);
		JSON.ReadWrite(dict, "hcKillCount", ref this.hcKillCount, isWrite);
		JSON.ReadWrite(dict, "hcDeathCount", ref this.hcDeathCount, isWrite);
		JSON.ReadWrite(dict, "hcWinCount", ref this.hcWinCount, isWrite);
		JSON.ReadWrite(dict, "hcLossCount", ref this.hcLossCount, isWrite);
		JSON.ReadWrite(dict, "cr", ref this.CR, isWrite);
		JSON.ReadWrite(dict, "gp", ref this.GP, isWrite);
		JSON.ReadWrite(dict, "sp", ref this.SP, isWrite);
		JSON.ReadWrite(dict, "bg", ref this.BG, isWrite);
		JSON.ReadWrite(dict, "sp_available", ref this.sp_available, isWrite);
		JSON.ReadWrite(dict, "discount_id", ref this.discount_id, isWrite);
		JSON.ReadWrite(dict, "discount", ref this.discount, isWrite);
		if (!Main.IsGameLoaded || this != Main.UserInfo || isWrite)
		{
			JSON.ReadWrite(dict, "suitNameIndex", ref this.suitNameIndex, isWrite);
		}
		JSON.ReadWrite(dict, "votes", ref this.votes, isWrite);
		JSON.ReadWrite(dict, "permission", ref this._intPermission, isWrite);
		this._cachedIntPermission = this._intPermission.Value;
		this._cachedPermission = (EPermission)this._cachedIntPermission;
		if (this.Permission == EPermission.Moder)
		{
			CVars.g_allowDMSpectate = true;
		}
		JSON.ReadWrite(dict, "repa", ref this.repa, isWrite);
		this.banned = new Int();
		if (!isWrite)
		{
			JSON.ReadWrite(dict, "banned", ref this.banned, isWrite);
		}
		if (!isWrite)
		{
			JSON.ReadWrite(dict, "is_suspect", ref this.isSuspected, isWrite);
		}
		try
		{
			JSON.ReadWrite(dict, "friendsAdded", ref this.friendsAdded, isWrite);
		}
		catch (Exception)
		{
		}
		JSON.ReadWrite(dict, "clanTag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "clanID", ref this.clanID, isWrite);
		JSON.ReadWrite(dict, "renameCount", ref this.nickChange, isWrite);
		JSON.ReadWrite(dict, "clan_earn", ref this.ClanEarn, isWrite);
		JSON.ReadWrite(dict, "clan_earn_proc", ref this.ClanEarnProc, isWrite);
		JSON.ReadWrite(dict, "clan_role", ref this._clanRole, isWrite);
		JSON.ReadWrite(dict, "clan_place", ref this.ClanPlace, isWrite);
		JSON.ReadWrite(dict, "clan_name", ref this.ClanName, isWrite);
		JSON.ReadWrite(dict, "clan_message", ref this.ClanMessage, isWrite);
		if (string.IsNullOrEmpty(this.ClanMessage) && this._clanRole > 2)
		{
			this.ClanMessage = Language.ClansDefaultMessage;
		}
		if (!isWrite)
		{
			this.CurrentProc = this.ClanEarnProc;
		}
		JSON.ReadWrite(dict, "clan_requests", ref this.ClanRequestsSended, isWrite);
		JSON.ReadWrite(dict, "isClanLeader", ref this.IsClanLeader, isWrite);
		JSON.ReadWrite(dict, "isWinner", ref this.IsWinner, isWrite);
		JSON.ReadWrite(dict, "wl_perm", ref this.wl_perm, isWrite);
		JSON.ReadWrite(dict, "wl_list", ref this._watchlistUsersId, isWrite);
		if (!isWrite && this._watchlistUsersId != null)
		{
			this.WatchlistUsersId.Clear();
			for (int i = 0; i < this._watchlistUsersId.Length; i++)
			{
				this.WatchlistUsersId.Add(this._watchlistUsersId[i]);
			}
		}
		if (Main.UserInfo.socialID == this.socialID)
		{
			JSON.ReadWrite<DailyBonus>(dict, "podgon", ref this.dailyBonus, isWrite);
		}
		try
		{
			JSON.ReadWrite(dict, "voteInfo", ref this.voteInfo, isWrite);
		}
		catch (Exception e)
		{
			global::Console.exception(e);
			this.voteInfo = new int[0];
		}
		if (this.banned > 0)
		{
			JSON.ReadWrite(dict, "banned_until", ref this.bannedUntil, isWrite);
			JSON.ReadWrite(dict, "reason", ref this.bannedReason, isWrite);
		}
		ArrayUtility.AdjustArraySize<SuitInfo>(ref this.suits, 5);
		for (int j = 0; j < this.suits.Length; j++)
		{
			int secondaryIndex = this.suits[j].secondaryIndex;
			int primaryIndex = this.suits[j].primaryIndex;
			bool secondaryMod = this.suits[j].secondaryMod;
			bool primaryMod = this.suits[j].primaryMod;
			JSON.ReadWrite<SuitInfo>(dict, "info" + j, ref this.suits[j], isWrite);
			if (Main.IsGameLoaded && this == Main.UserInfo && !isWrite)
			{
				this.suits[j].secondaryIndex = secondaryIndex;
				this.suits[j].primaryIndex = primaryIndex;
				this.suits[j].secondaryMod = secondaryMod;
				this.suits[j].primaryMod = primaryMod;
			}
			if (this.suits[j].secondaryIndex != 127 && !this.weaponsStates[this.suits[j].secondaryIndex].Unlocked)
			{
				this.suits[j].secondaryIndex = 0;
			}
			if (this.suits[j].primaryIndex != 127 && !this.weaponsStates[this.suits[j].primaryIndex].Unlocked)
			{
				this.suits[j].primaryIndex = 127;
			}
			if (this.suits[j].secondaryIndex != 127 && this.weaponsStates[this.suits[j].secondaryIndex].CurrentWeapon.weaponUseType == WeaponUseType.Primary)
			{
				this.suits[j].secondaryIndex = 0;
			}
			if (this.suits[j].primaryIndex != 127 && this.weaponsStates[this.suits[j].primaryIndex].CurrentWeapon.weaponUseType == WeaponUseType.Secondary)
			{
				this.suits[j].primaryIndex = 127;
			}
			for (int k = 0; k < Math.Min(this.weaponsStates.Length, Globals.I.weapons.Length); k++)
			{
				if (Utility.IsModableWeapon((int)this.weaponsStates[k].CurrentWeapon.type))
				{
					this.suits[j].SetWtask(k, false);
				}
				else if (!this.weaponsStates[k].wtaskUnlocked && this.suits[j].GetWtask(k))
				{
					this.suits[j].SetWtask(k, false);
				}
			}
		}
		this.iWtaskCount = 0;
		for (int l = 0; l < Math.Min(this.weaponsStates.Length, Globals.I.weapons.Length); l++)
		{
			if (!this.weaponsStates[l].CurrentWeapon.isPremium)
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
		JSON.ReadWrite(dict, "unlockedSets", ref this.unlockedSets, isWrite);
		JSON.ReadWrite<UserSettings>(dict, "settings", ref this.settings, isWrite);
		base.ConvertAwards(dict, isWrite);
		for (int m = 0; m < this.unlockedSets.Length; m++)
		{
			if (this.getLevel((float)this.currentXP) >= 10 * m)
			{
				this.unlockedSets[m] = 1;
			}
		}
		this.ClanRequestLeft();
		this.SetTotalClanRequest();
		object obj;
		if (dict.TryGetValue("gp_discount", out obj))
		{
			this.PersonaBankDiscount = new PersonalBankDiscount();
			this.PersonaBankDiscount.Read((Dictionary<string, object>)obj);
		}
		else if (this.PersonaBankDiscount != null && this.PersonaBankDiscount.DiscountExists)
		{
			this.PersonaBankDiscount.CancelDiscount();
		}
		JSON.ReadWrite(dict, "is_imported", ref this.IsProfileTransfered, isWrite);
		JSON.ReadWrite(dict, "platform", ref this.SocialNet, isWrite);
	}

	// Token: 0x06001439 RID: 5177 RVA: 0x000D7418 File Offset: 0x000D5618
	protected void Convert(Dictionary<string, object> dict, bool isWrite, bool WPAK_Needed)
	{
		if (WPAK_Needed)
		{
			ArrayUtility.AdjustArraySize<WeaponInfo>(ref this.weaponsStates, Globals.I.weapons.Length);
			ArrayUtility.AdjustArraySize<SkillInfo>(ref this.skillsInfos, Globals.I.skills.Length);
			ArrayUtility.AdjustArraySize<AchievementInfo>(ref this.achievementsInfos, Globals.I.achievements.Length);
			ArrayUtility.AdjustArraySize<ClanSkillInfo>(ref this.ClanSkillsInfos, Globals.I.ClanSkills.Length);
			JSON.ReadWrite<SkillInfo>(dict, "skills", ref this.skillsInfos, isWrite);
			this.playerClass = this.GetPlayerClass;
			JSON.ReadWrite<ContractsState>(dict, "contracts", ref this.contractsInfo, isWrite);
			JSON.ReadWrite<WeaponInfo>(dict, "weapons", ref this.weaponsStates, isWrite);
			JSON.ReadWrite<ClanSkillInfo>(dict, "clan_skills", ref this.ClanSkillsInfos, isWrite);
			if (isWrite)
			{
				this.old_achievements = new int[this.achievementsInfos.Length];
				for (int i = 0; i < this.achievementsInfos.Length; i++)
				{
					this.old_achievements[i] = this.achievementsInfos[i].current;
				}
				JSON.ReadWrite(dict, "achievements", ref this.old_achievements, isWrite);
			}
			else if (this.userID != IDUtil.BotID)
			{
				JSON.ReadWrite(dict, "achievements", ref this.old_achievements, isWrite);
				for (int j = 0; j < this.old_achievements.Length; j++)
				{
					this.achievementsInfos[j].current = this.old_achievements[j];
				}
			}
		}
		this.Convert(dict, isWrite);
	}

	// Token: 0x0600143A RID: 5178 RVA: 0x000D7598 File Offset: 0x000D5798
	public new void Read(Dictionary<string, object> dict, bool WPAK)
	{
		this.userStats.Read((Dictionary<string, object>)dict["stats"]);
		this.socialInfo.Read((Dictionary<string, object>)dict["social"]);
		this.Convert((Dictionary<string, object>)dict["data"], false, WPAK);
		this._profileLoaded = true;
	}

	// Token: 0x0600143B RID: 5179 RVA: 0x000D75FC File Offset: 0x000D57FC
	public void Write(Dictionary<string, object> dict, bool WPAK)
	{
		this.Convert(dict, true, WPAK);
	}

	// Token: 0x0600143C RID: 5180 RVA: 0x000D7608 File Offset: 0x000D5808
	public string ToJSON(bool WPAK)
	{
		return ArrayUtility.ToJSON<string, object>(this.ToDict(WPAK));
	}

	// Token: 0x0600143D RID: 5181 RVA: 0x000D7618 File Offset: 0x000D5818
	public Dictionary<string, object> ToDict(bool WPAK)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
		this.Write(dictionary2, WPAK);
		Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
		this.userStats.Write(dictionary3);
		if (WPAK)
		{
			dictionary.Add("stats", dictionary3);
		}
		dictionary.Add("data", dictionary2);
		dictionary.Add("class", (int)this.playerClass);
		return dictionary;
	}

	// Token: 0x0600143E RID: 5182 RVA: 0x000D7680 File Offset: 0x000D5880
	public Dictionary<string, object> ToServerDict()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
		this.old_achievements = new int[this.achievementsInfos.Length];
		for (int i = 0; i < this.achievementsInfos.Length; i++)
		{
			this.old_achievements[i] = this.achievementsInfos[i].current;
		}
		JSON.ReadWrite(dictionary2, "achievements", ref this.old_achievements, true);
		JSON.ReadWrite<WeaponInfo>(dictionary2, "weapons", ref this.weaponsStates, true);
		JSON.ReadWrite<ContractsState>(dictionary2, "contracts", ref this.contractsInfo, true);
		JSON.ReadWrite(dictionary2, "currentXP", ref this.currentXP, true);
		JSON.ReadWrite(dictionary2, "delta_xp", ref this.DeltaExp, true);
		JSON.ReadWrite(dictionary2, "delta_cr", ref this.DeltaCr, true);
		JSON.ReadWrite(dictionary2, "killCount", ref this.killCount, true);
		JSON.ReadWrite(dictionary2, "deathCount", ref this.deathCount, true);
		JSON.ReadWrite(dictionary2, "winCount", ref this.winCount, true);
		JSON.ReadWrite(dictionary2, "lossCount", ref this.lossCount, true);
		JSON.ReadWrite(dictionary2, "hcCurrentXP", ref this.hcCurrentXP, true);
		JSON.ReadWrite(dictionary2, "hcKillCount", ref this.hcKillCount, true);
		JSON.ReadWrite(dictionary2, "hcDeathCount", ref this.hcDeathCount, true);
		JSON.ReadWrite(dictionary2, "hcWinCount", ref this.hcWinCount, true);
		JSON.ReadWrite(dictionary2, "hcLossCount", ref this.hcLossCount, true);
		JSON.ReadWrite(dictionary2, "votes", ref this.votes, true);
		base.ConvertAwards(dictionary2, true);
		dictionary.Add("stats", this.userStats.LegueSystemConvert());
		dictionary.Add("data", dictionary2);
		dictionary.Add("class", (int)this.playerClass);
		return dictionary;
	}

	// Token: 0x0600143F RID: 5183 RVA: 0x000D7840 File Offset: 0x000D5A40
	public void FromJSON(string json, bool WPAK)
	{
		this.Read(ArrayUtility.FromJSON(json, string.Empty), WPAK);
	}

	// Token: 0x17000301 RID: 769
	// (get) Token: 0x06001440 RID: 5184 RVA: 0x000D7854 File Offset: 0x000D5A54
	public int currentLevel
	{
		get
		{
			return this.getLevel((float)this.currentXP);
		}
	}

	// Token: 0x06001441 RID: 5185 RVA: 0x000D7868 File Offset: 0x000D5A68
	public float maxXP(float exp)
	{
		return (float)Globals.I.expTable[this.getLevel(exp) + 1];
	}

	// Token: 0x06001442 RID: 5186 RVA: 0x000D7880 File Offset: 0x000D5A80
	public float minXP(float exp)
	{
		return (float)Globals.I.expTable[this.getLevel(exp)];
	}

	// Token: 0x17000302 RID: 770
	// (get) Token: 0x06001443 RID: 5187 RVA: 0x000D7898 File Offset: 0x000D5A98
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

	// Token: 0x17000303 RID: 771
	// (get) Token: 0x06001444 RID: 5188 RVA: 0x000D7970 File Offset: 0x000D5B70
	public int totalSPspent
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.skillsInfos.Length; i++)
			{
				if (this.skillsInfos[i].Unlocked)
				{
					num += this.skillsInfos[i].SP.Value;
				}
			}
			return num;
		}
	}

	// Token: 0x17000304 RID: 772
	// (get) Token: 0x06001445 RID: 5189 RVA: 0x000D79C0 File Offset: 0x000D5BC0
	public int skillResetGPRefund
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.skillsInfos.Length; i++)
			{
				if (this.skillsInfos[i].type != Skills.efd2)
				{
					if (this.skillsInfos[i].type != Skills.efd_radius)
					{
						if (this.skillsInfos[i].type != Skills.dam3)
						{
							if (this.skillsInfos[i].type != Skills.ammo_p)
							{
								if (this.skillsInfos[i].type != Skills.arm_limbs)
								{
									if (this.skillsInfos[i].Unlocked && !this.skillsInfos[i].rentable)
									{
										num += this.skillsInfos[i].GP.Value;
									}
								}
							}
						}
					}
				}
			}
			return num / 2;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x06001446 RID: 5190 RVA: 0x000D7AA4 File Offset: 0x000D5CA4
	public bool Set1Unlocked
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000306 RID: 774
	// (get) Token: 0x06001447 RID: 5191 RVA: 0x000D7AA8 File Offset: 0x000D5CA8
	public bool Set2Unlocked
	{
		get
		{
			return this.currentLevel >= 10 || this.unlockedSets[1] == 1;
		}
	}

	// Token: 0x17000307 RID: 775
	// (get) Token: 0x06001448 RID: 5192 RVA: 0x000D7AC8 File Offset: 0x000D5CC8
	public bool Set3Unlocked
	{
		get
		{
			return this.currentLevel >= 20 || this.unlockedSets[2] == 1;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x06001449 RID: 5193 RVA: 0x000D7AE8 File Offset: 0x000D5CE8
	public bool Set4Unlocked
	{
		get
		{
			return this.currentLevel >= 30 || this.unlockedSets[3] == 1;
		}
	}

	// Token: 0x17000309 RID: 777
	// (get) Token: 0x0600144A RID: 5194 RVA: 0x000D7B08 File Offset: 0x000D5D08
	public bool Set5Unlocked
	{
		get
		{
			return this.currentLevel >= 40 || this.unlockedSets[4] == 1;
		}
	}

	// Token: 0x1700030A RID: 778
	// (get) Token: 0x0600144B RID: 5195 RVA: 0x000D7B28 File Offset: 0x000D5D28
	public bool Set6Unlocked
	{
		get
		{
			return this.currentLevel >= 50 || this.unlockedSets[5] == 1;
		}
	}

	// Token: 0x0600144C RID: 5196 RVA: 0x000D7B48 File Offset: 0x000D5D48
	public int getLevel(float exp)
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

	// Token: 0x0600144D RID: 5197 RVA: 0x000D7B98 File Offset: 0x000D5D98
	public int getWeaponLevel(int currentWeaponKill)
	{
		for (int i = 0; i < Globals.I.masteringTable.Length; i++)
		{
			if (currentWeaponKill < Globals.I.masteringTable[i])
			{
				return i - 1;
			}
		}
		return Globals.I.masteringTable.Length - 1;
	}

	// Token: 0x1700030B RID: 779
	// (get) Token: 0x0600144E RID: 5198 RVA: 0x000D7BE8 File Offset: 0x000D5DE8
	public int wtaskOpenedCount
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

	// Token: 0x0600144F RID: 5199 RVA: 0x000D7C28 File Offset: 0x000D5E28
	public BaseWeapon getWeapon(int type)
	{
		return this.weaponsStates[type].CurrentWeapon;
	}

	// Token: 0x06001450 RID: 5200 RVA: 0x000D7C38 File Offset: 0x000D5E38
	public bool clanSkillUnlocked(Cl_Skills clanSkill)
	{
		return !Peer.HardcoreMode && (this.ClanSkillsInfos != null && clanSkill >= Cl_Skills.cl_efd && clanSkill < (Cl_Skills)this.ClanSkillsInfos.Length) && this.ClanSkillsInfos[(int)clanSkill].IsUnlocked;
	}

	// Token: 0x1700030C RID: 780
	// (get) Token: 0x06001451 RID: 5201 RVA: 0x000D7C78 File Offset: 0x000D5E78
	public bool[] skillArray
	{
		get
		{
			bool[] array = new bool[this.skillsInfos.Length];
			for (int i = 0; i < this.skillsInfos.Length; i++)
			{
				array[i] = this.skillsInfos[i].Unlocked;
			}
			return array;
		}
	}

	// Token: 0x1700030D RID: 781
	// (get) Token: 0x06001452 RID: 5202 RVA: 0x000D7CC0 File Offset: 0x000D5EC0
	public bool[] clanSkillArray
	{
		get
		{
			bool[] array = new bool[this.ClanSkillsInfos.Length];
			for (int i = 0; i < this.ClanSkillsInfos.Length; i++)
			{
				array[i] = this.ClanSkillsInfos[i].IsUnlocked;
			}
			return array;
		}
	}

	// Token: 0x1700030E RID: 782
	// (get) Token: 0x06001453 RID: 5203 RVA: 0x000D7D08 File Offset: 0x000D5F08
	public int contractsCount
	{
		get
		{
			int num = 0;
			if (this.contractsInfo.CurrentEasyCount >= this.contractsInfo.CurrentEasy.task_counter)
			{
				num += this.contractsInfo.CurrentEasyIndex + 1;
			}
			else
			{
				num += this.contractsInfo.CurrentEasyIndex;
			}
			if (this.contractsInfo.CurrentNormalCount >= this.contractsInfo.CurrentNormal.task_counter)
			{
				num += this.contractsInfo.CurrentNormalIndex + 1;
			}
			else
			{
				num += this.contractsInfo.CurrentNormalIndex;
			}
			if (this.contractsInfo.CurrentHardCount >= this.contractsInfo.CurrentHard.task_counter)
			{
				num += this.contractsInfo.CurrentHardIndex + 1;
			}
			else
			{
				num += this.contractsInfo.CurrentHardIndex;
			}
			return num;
		}
	}

	// Token: 0x1700030F RID: 783
	// (get) Token: 0x06001454 RID: 5204 RVA: 0x000D7DE4 File Offset: 0x000D5FE4
	public float KD
	{
		get
		{
			return (!(this.deathCount > 0)) ? ((float)this.killCount) : ((float)this.killCount / (float)this.deathCount);
		}
	}

	// Token: 0x17000310 RID: 784
	// (get) Token: 0x06001455 RID: 5205 RVA: 0x000D7E24 File Offset: 0x000D6024
	public float HardcoreKD
	{
		get
		{
			return (!(this.hcDeathCount > 0)) ? ((float)this.hcKillCount) : ((float)this.hcKillCount / (float)this.hcDeathCount);
		}
	}

	// Token: 0x06001456 RID: 5206 RVA: 0x000D7E64 File Offset: 0x000D6064
	public void ClanRequestLeft()
	{
		if (this.currentLevel <= 10)
		{
			this.ClanRequestsLeft = 1 - this.ClanRequestsSended;
		}
		else
		{
			this.ClanRequestsLeft = this.currentLevel / 10 - this.ClanRequestsSended;
		}
	}

	// Token: 0x06001457 RID: 5207 RVA: 0x000D7EA8 File Offset: 0x000D60A8
	public void SetTotalClanRequest()
	{
		if (this.currentLevel <= 10)
		{
			this.TotalClanRequests = 1;
		}
		else
		{
			this.TotalClanRequests = this.currentLevel / 10;
		}
	}

	// Token: 0x06001458 RID: 5208 RVA: 0x000D7EE0 File Offset: 0x000D60E0
	public object Clone()
	{
		return base.MemberwiseClone();
	}

	// Token: 0x040018D3 RID: 6355
	public Int votes = new Int();

	// Token: 0x040018D4 RID: 6356
	public Int nickChange = new Int();

	// Token: 0x040018D5 RID: 6357
	public bool nickColorChange;

	// Token: 0x040018D6 RID: 6358
	public float DeltaExp;

	// Token: 0x040018D7 RID: 6359
	public float DeltaCr;

	// Token: 0x040018D8 RID: 6360
	public Float hcCurrentXP = new Float(0f);

	// Token: 0x040018D9 RID: 6361
	public Int hcWinCount = new Int();

	// Token: 0x040018DA RID: 6362
	public Int hcLossCount = new Int();

	// Token: 0x040018DB RID: 6363
	public int discount_id = -1;

	// Token: 0x040018DC RID: 6364
	public int discount;

	// Token: 0x040018DD RID: 6365
	public int friendsAdded;

	// Token: 0x040018DE RID: 6366
	public string ClanMessage = string.Empty;

	// Token: 0x040018DF RID: 6367
	public int ClanRequestsSended;

	// Token: 0x040018E0 RID: 6368
	public bool IsClanLeader;

	// Token: 0x040018E1 RID: 6369
	public bool IsWinner;

	// Token: 0x040018E2 RID: 6370
	public bool wl_perm;

	// Token: 0x040018E3 RID: 6371
	private int[] _watchlistUsersId;

	// Token: 0x040018E4 RID: 6372
	public DailyBonus dailyBonus = new DailyBonus();

	// Token: 0x040018E5 RID: 6373
	public int[] voteInfo = new int[0];

	// Token: 0x040018E6 RID: 6374
	public int[] unlockedSets;

	// Token: 0x040018E7 RID: 6375
	public UserSettings settings;

	// Token: 0x040018E8 RID: 6376
	public ClanSkillInfo[] ClanSkillsInfos;

	// Token: 0x040018E9 RID: 6377
	public int[] old_achievements;

	// Token: 0x040018EA RID: 6378
	public Int sp_available;

	// Token: 0x040018EB RID: 6379
	public Int BG;

	// Token: 0x040018EC RID: 6380
	private bool _profileLoaded;

	// Token: 0x040018ED RID: 6381
	public ClanSystemData clanData;

	// Token: 0x040018EE RID: 6382
	public CurrencyInfo CurrencyInfo;

	// Token: 0x040018EF RID: 6383
	public float CurrentProc;

	// Token: 0x040018F0 RID: 6384
	public int ClanRequestsLeft;

	// Token: 0x040018F1 RID: 6385
	public int TotalClanRequests;

	// Token: 0x040018F2 RID: 6386
	public Int Violation;

	// Token: 0x040018F3 RID: 6387
	public int CurrentMpExp;

	// Token: 0x040018F4 RID: 6388
	public MasteringInfo Mastering;

	// Token: 0x040018F5 RID: 6389
	public bool isSuspected;

	// Token: 0x040018F6 RID: 6390
	public PersonalBankDiscount PersonaBankDiscount;

	// Token: 0x040018F7 RID: 6391
	public bool IsProfileTransfered;

	// Token: 0x040018F8 RID: 6392
	public int SocialNet;

	// Token: 0x040018F9 RID: 6393
	private Int _intPermission;

	// Token: 0x040018FA RID: 6394
	private int _cachedIntPermission;

	// Token: 0x040018FB RID: 6395
	private EPermission _cachedPermission;

	// Token: 0x040018FC RID: 6396
	public string HashID;
}
