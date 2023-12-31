using System;
using System.Collections.Generic;

// Token: 0x020002AF RID: 687
[Serializable]
internal class ShortClanInfo : Convertible
{
	// Token: 0x06001355 RID: 4949 RVA: 0x000D01A4 File Offset: 0x000CE3A4
	public ShortClanInfo()
	{
	}

	// Token: 0x06001356 RID: 4950 RVA: 0x000D0224 File Offset: 0x000CE424
	public ShortClanInfo(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x06001357 RID: 4951 RVA: 0x000D02AC File Offset: 0x000CE4AC
	public void ReloadAdditionInfo(DatabaseEvent.SomeAction success, DatabaseEvent.SomeAction failed, bool forceReload = false)
	{
		if (this.DetailInfo == null || forceReload)
		{
			this.DetailInfo = new DetailClanInfo();
			Main.AddDatabaseRequestCallBack<LoadDetailInfo>(success, failed, new object[]
			{
				this
			});
		}
		else
		{
			success();
		}
	}

	// Token: 0x06001358 RID: 4952 RVA: 0x000D02F4 File Offset: 0x000CE4F4
	public void ParseDetailClanInfo(Dictionary<string, object> dict)
	{
		this.DetailInfo = new DetailClanInfo(dict);
	}

	// Token: 0x06001359 RID: 4953 RVA: 0x000D0304 File Offset: 0x000CE504
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
		return Globals.I.clanExpTable.Length - 1;
	}

	// Token: 0x0600135A RID: 4954 RVA: 0x000D0368 File Offset: 0x000CE568
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

	// Token: 0x0600135B RID: 4955 RVA: 0x000D03B8 File Offset: 0x000CE5B8
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "order", ref this.order, isWrite);
		JSON.ReadWrite(dict, "clan_id", ref this.clanID, isWrite);
		JSON.ReadWrite(dict, "tag", ref this.tag, isWrite);
		JSON.ReadWrite(dict, "tag_color", ref this.tagColor, isWrite);
		JSON.ReadWrite(dict, "name", ref this.name, isWrite);
		JSON.ReadWrite(dict, "clan_exp", ref this.clanExp, isWrite);
		if (!isWrite && this.clanExp < 0f)
		{
			this.clanExp = 0f;
		}
		if (!isWrite)
		{
			this.clanLevel = this.GetClanLevel(this.clanExp.Value);
		}
		JSON.ReadWrite(dict, "race_exp", ref this.raceExp, isWrite);
		JSON.ReadWrite(dict, "leader_nick", ref this.leaderNick, isWrite);
		JSON.ReadWrite(dict, "perk_states", ref this.leaderNickColor, isWrite);
		if (!this.leaderNickColor.StartsWith("#"))
		{
			this.leaderNickColor = "#" + this.leaderNickColor;
		}
		JSON.ReadWrite(dict, "leader_exp", ref this.leaderExp, isWrite);
		if (!isWrite)
		{
			this.clanLeaderLevel = this.GetPlayerLevel(this.leaderExp);
		}
		JSON.ReadWrite(dict, "leader_fname", ref this.leaderFname, isWrite);
		JSON.ReadWrite(dict, "leader_lname", ref this.leaderLname, isWrite);
		JSON.ReadWrite(dict, "members_count", ref this.membersCount, isWrite);
		JSON.ReadWrite(dict, "banned", ref this.banned, isWrite);
		JSON.ReadWrite(dict, "did_request", ref this.didRequest, isWrite);
		JSON.ReadWrite(dict, "platform", ref this.SocialNet, isWrite);
	}

	// Token: 0x0400165F RID: 5727
	public DetailClanInfo DetailInfo;

	// Token: 0x04001660 RID: 5728
	public int order;

	// Token: 0x04001661 RID: 5729
	public int clanID;

	// Token: 0x04001662 RID: 5730
	public int membersCount;

	// Token: 0x04001663 RID: 5731
	public string tag = string.Empty;

	// Token: 0x04001664 RID: 5732
	public string tagColor = "#ffffff";

	// Token: 0x04001665 RID: 5733
	public string name = string.Empty;

	// Token: 0x04001666 RID: 5734
	public string leaderNick = string.Empty;

	// Token: 0x04001667 RID: 5735
	public string leaderNickColor = "#FFFFFF";

	// Token: 0x04001668 RID: 5736
	public string leaderFname = string.Empty;

	// Token: 0x04001669 RID: 5737
	public string leaderLname = string.Empty;

	// Token: 0x0400166A RID: 5738
	public Float clanExp = new Float(0f);

	// Token: 0x0400166B RID: 5739
	public Float raceExp = new Float(0f);

	// Token: 0x0400166C RID: 5740
	public float leaderExp;

	// Token: 0x0400166D RID: 5741
	public int clanLevel;

	// Token: 0x0400166E RID: 5742
	public int clanLeaderLevel;

	// Token: 0x0400166F RID: 5743
	public bool didRequest;

	// Token: 0x04001670 RID: 5744
	public bool banned;

	// Token: 0x04001671 RID: 5745
	public int racePlace;

	// Token: 0x04001672 RID: 5746
	public int SocialNet;
}
