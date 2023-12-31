using System;
using System.Collections.Generic;

// Token: 0x02000258 RID: 600
[Serializable]
internal class DetailClanInfo : Convertible
{
	// Token: 0x06001269 RID: 4713 RVA: 0x000C9A54 File Offset: 0x000C7C54
	public DetailClanInfo()
	{
	}

	// Token: 0x0600126A RID: 4714 RVA: 0x000C9AE8 File Offset: 0x000C7CE8
	public DetailClanInfo(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x0600126B RID: 4715 RVA: 0x000C9B84 File Offset: 0x000C7D84
	public void ParseClanMemberList(Dictionary<string, object>[] dict)
	{
		this.MemberList = new ClanMemberInfo[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.MemberList[i] = new ClanMemberInfo(dict[i]);
		}
	}

	// Token: 0x0600126C RID: 4716 RVA: 0x000C9BC4 File Offset: 0x000C7DC4
	public void ParseRequestList(Dictionary<string, object>[] dict)
	{
		if (dict.Length == 0)
		{
			return;
		}
		this.RequestList = new ClanRequestInfo[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.RequestList[i] = new ClanRequestInfo(dict[i]);
		}
	}

	// Token: 0x0600126D RID: 4717 RVA: 0x000C9C0C File Offset: 0x000C7E0C
	private int GetClanLevel(float exp)
	{
		if (exp < 0f)
		{
			exp = 0f;
		}
		for (int i = 0; i < Globals.I.clanExpTable.Length; i++)
		{
			if (exp < (float)Globals.I.clanExpTable[i])
			{
				return i - 1;
			}
		}
		return 0;
	}

	// Token: 0x0600126E RID: 4718 RVA: 0x000C9C60 File Offset: 0x000C7E60
	private float GetNextLevelExp(float exp)
	{
		int num = Globals.I.clanExpTable.Length;
		if (exp <= 0f)
		{
			return (float)Globals.I.clanExpTable[1];
		}
		for (int i = 0; i < num; i++)
		{
			if (exp < (float)Globals.I.clanExpTable[i])
			{
				return (float)Globals.I.clanExpTable[i];
			}
		}
		return (float)((num <= 0) ? 0 : Globals.I.clanExpTable[num - 1]);
	}

	// Token: 0x0600126F RID: 4719 RVA: 0x000C9CE4 File Offset: 0x000C7EE4
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

	// Token: 0x06001270 RID: 4720 RVA: 0x000C9D34 File Offset: 0x000C7F34
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "order", ref this.order, isWrite);
		JSON.ReadWrite(dict, "clan_id", ref this.clanID, isWrite);
		JSON.ReadWrite(dict, "tag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "tag_color", ref this.clanTagColor, isWrite);
		JSON.ReadWrite(dict, "name", ref this.clanName, isWrite);
		JSON.ReadWrite(dict, "clan_exp", ref this.clanExp, isWrite);
		if (!isWrite && this.clanExp < 0f)
		{
			this.clanExp = 0f;
		}
		if (!isWrite)
		{
			this.clanLevel = this.GetClanLevel(this.clanExp);
		}
		if (!isWrite)
		{
			this.nextLevelExp = this.GetNextLevelExp(this.clanExp);
		}
		JSON.ReadWrite(dict, "race_exp", ref this.raceExp, isWrite);
		JSON.ReadWrite(dict, "leader", ref this.leaderID, isWrite);
		JSON.ReadWrite(dict, "leader_nick", ref this.leaderNick, isWrite);
		JSON.ReadWrite(dict, "perk_states", ref this.leaderNickColor, isWrite);
		if (!this.leaderNickColor.StartsWith("#"))
		{
			this.leaderNickColor = "#" + this.leaderNickColor;
		}
		JSON.ReadWrite(dict, "leader_fname", ref this.leaderFName, isWrite);
		JSON.ReadWrite(dict, "leader_lname", ref this.leaderLName, isWrite);
		JSON.ReadWrite(dict, "leader_exp", ref this.leaderExp, isWrite);
		if (!isWrite)
		{
			this.leaderLevel = this.GetPlayerLevel(this.leaderExp.Value);
		}
		JSON.ReadWrite(dict, "leader_reputation", ref this.leaderReputation, isWrite);
		JSON.ReadWrite(dict, "leader_class", ref this.leaderClass, isWrite);
		JSON.ReadWrite(dict, "leader_kills", ref this.leaderKills, isWrite);
		JSON.ReadWrite(dict, "leader_death", ref this.leaderDeath, isWrite);
		JSON.ReadWrite(dict, "members_count", ref this.currentMemberCount, isWrite);
		JSON.ReadWrite(dict, "max_users", ref this.maxMemberCount, isWrite);
		JSON.ReadWrite(dict, "race_wins", ref this.raceWins, isWrite);
		JSON.ReadWrite(dict, "place", ref this.place, isWrite);
		JSON.ReadWrite(dict, "did_request", ref this.didRequest, isWrite);
		JSON.ReadWrite(dict, "clan_url", ref this.clanURL, isWrite);
		JSON.ReadWrite(dict, "clan_cr", ref this.clanCR, isWrite);
		JSON.ReadWrite(dict, "clan_gp", ref this.clanGP, isWrite);
		JSON.ReadWrite(dict, "clan_bg", ref this.clanBG, isWrite);
	}

	// Token: 0x0400120A RID: 4618
	public ClanMemberInfo[] MemberList = new ClanMemberInfo[0];

	// Token: 0x0400120B RID: 4619
	public ClanRequestInfo[] RequestList = new ClanRequestInfo[0];

	// Token: 0x0400120C RID: 4620
	public int order;

	// Token: 0x0400120D RID: 4621
	public int clanID;

	// Token: 0x0400120E RID: 4622
	public string clanTag = string.Empty;

	// Token: 0x0400120F RID: 4623
	public string clanTagColor = string.Empty;

	// Token: 0x04001210 RID: 4624
	public string clanName = string.Empty;

	// Token: 0x04001211 RID: 4625
	public float clanExp;

	// Token: 0x04001212 RID: 4626
	public float raceExp;

	// Token: 0x04001213 RID: 4627
	public int leaderID;

	// Token: 0x04001214 RID: 4628
	public string leaderNick = string.Empty;

	// Token: 0x04001215 RID: 4629
	public string leaderNickColor = "#FFFFFF";

	// Token: 0x04001216 RID: 4630
	public string leaderFName = string.Empty;

	// Token: 0x04001217 RID: 4631
	public string leaderLName = string.Empty;

	// Token: 0x04001218 RID: 4632
	public Float leaderExp = 0f;

	// Token: 0x04001219 RID: 4633
	public float leaderReputation;

	// Token: 0x0400121A RID: 4634
	public int leaderClass;

	// Token: 0x0400121B RID: 4635
	public int leaderKills;

	// Token: 0x0400121C RID: 4636
	public int leaderDeath;

	// Token: 0x0400121D RID: 4637
	public int currentMemberCount;

	// Token: 0x0400121E RID: 4638
	public int maxMemberCount;

	// Token: 0x0400121F RID: 4639
	public int raceWins;

	// Token: 0x04001220 RID: 4640
	public int place;

	// Token: 0x04001221 RID: 4641
	public bool didRequest;

	// Token: 0x04001222 RID: 4642
	public string clanURL = string.Empty;

	// Token: 0x04001223 RID: 4643
	public long clanCR;

	// Token: 0x04001224 RID: 4644
	public int clanGP;

	// Token: 0x04001225 RID: 4645
	public int clanBG;

	// Token: 0x04001226 RID: 4646
	public int clanLevel;

	// Token: 0x04001227 RID: 4647
	public int leaderLevel;

	// Token: 0x04001228 RID: 4648
	public float nextLevelExp;
}
