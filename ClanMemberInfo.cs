using System;
using System.Collections.Generic;

// Token: 0x0200024D RID: 589
internal class ClanMemberInfo : Convertible
{
	// Token: 0x06001204 RID: 4612 RVA: 0x000C6FF8 File Offset: 0x000C51F8
	public ClanMemberInfo()
	{
	}

	// Token: 0x06001205 RID: 4613 RVA: 0x000C7038 File Offset: 0x000C5238
	public ClanMemberInfo(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x06001206 RID: 4614 RVA: 0x000C708C File Offset: 0x000C528C
	public ClanMemberInfo(ClanRequestInfo request)
	{
		this.memberUserID = request.userID;
		this.memberSocialID = request.socialID;
		this.currentExp = request.currentExp.Value;
		this.Level = request.level;
		this.memberClass = (float)request.currentClass;
		this.memberNick = request.userNick;
		this.memberNickColor = request.userNickColor;
		this.memberFName = request.userFName;
		this.memberLName = request.userLName;
		this.earnExp = 0f;
		this.earnProc = 0f;
		this.killCount = request.killCount;
		this.deathCount = request.deathCount;
		this.reputation = (float)request.reputation;
		this.proKills = request.prokills;
		this.achievements = request.achievements;
		this.contracts = request.contracts;
	}

	// Token: 0x17000267 RID: 615
	// (get) Token: 0x06001207 RID: 4615 RVA: 0x000C71A8 File Offset: 0x000C53A8
	public string Role
	{
		get
		{
			int num = this.role;
			switch (num)
			{
			case 2:
				return "<color=#ffffff><size=13>" + Language.ClansOfficer + "</size></color>";
			default:
				if (num != 8)
				{
					return "<color=#808080><size=13>" + Language.ClansContractor + "</size></color>";
				}
				return "<color=#ffc600><size=13>" + Language.ClansLeader + "</size></color>";
			case 4:
				return "<color=#ffc600><size=13>" + Language.ClansLieutenant + "</size></color>";
			}
		}
	}

	// Token: 0x06001208 RID: 4616 RVA: 0x000C7230 File Offset: 0x000C5430
	private int GetPlayerLevel(float exp)
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

	// Token: 0x06001209 RID: 4617 RVA: 0x000C7280 File Offset: 0x000C5480
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "order", ref this.order, isWrite);
		JSON.ReadWrite(dict, "user_id", ref this.memberUserID, isWrite);
		JSON.ReadWrite(dict, "social_id", ref this.memberSocialID, isWrite);
		JSON.ReadWrite(dict, "curr_xp", ref this.currentExp, isWrite);
		if (!isWrite)
		{
			this.Level = this.GetPlayerLevel(this.currentExp);
		}
		JSON.ReadWrite(dict, "class", ref this.memberClass, isWrite);
		JSON.ReadWrite(dict, "user_name", ref this.memberNick, isWrite);
		JSON.ReadWrite(dict, "perk_states", ref this.memberNickColor, isWrite);
		if (!this.memberNickColor.StartsWith("#"))
		{
			this.memberNickColor = "#" + this.memberNickColor;
		}
		JSON.ReadWrite(dict, "first_name", ref this.memberFName, isWrite);
		JSON.ReadWrite(dict, "last_name", ref this.memberLName, isWrite);
		JSON.ReadWrite(dict, "earn", ref this.earnExp, isWrite);
		JSON.ReadWrite(dict, "earn_proc", ref this.earnProc, isWrite);
		JSON.ReadWrite(dict, "kill_count", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "death_count", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "repa", ref this.reputation, isWrite);
		JSON.ReadWrite(dict, "prokills", ref this.proKills, isWrite);
		JSON.ReadWrite(dict, "achievements", ref this.achievements, isWrite);
		JSON.ReadWrite(dict, "contracts", ref this.contracts, isWrite);
		JSON.ReadWrite(dict, "clan_role", ref this.role, isWrite);
	}

	// Token: 0x0400118D RID: 4493
	public int order;

	// Token: 0x0400118E RID: 4494
	public int memberUserID;

	// Token: 0x0400118F RID: 4495
	public string memberSocialID = string.Empty;

	// Token: 0x04001190 RID: 4496
	public float currentExp;

	// Token: 0x04001191 RID: 4497
	public int Level;

	// Token: 0x04001192 RID: 4498
	public float memberClass;

	// Token: 0x04001193 RID: 4499
	public string memberNick = string.Empty;

	// Token: 0x04001194 RID: 4500
	public string memberNickColor = "#FFFFFF";

	// Token: 0x04001195 RID: 4501
	public string memberFName = string.Empty;

	// Token: 0x04001196 RID: 4502
	public string memberLName = string.Empty;

	// Token: 0x04001197 RID: 4503
	public float earnExp;

	// Token: 0x04001198 RID: 4504
	public float earnProc;

	// Token: 0x04001199 RID: 4505
	public int killCount;

	// Token: 0x0400119A RID: 4506
	public int deathCount;

	// Token: 0x0400119B RID: 4507
	public float reputation;

	// Token: 0x0400119C RID: 4508
	public int proKills;

	// Token: 0x0400119D RID: 4509
	public int achievements;

	// Token: 0x0400119E RID: 4510
	public int contracts;

	// Token: 0x0400119F RID: 4511
	public int role;
}
