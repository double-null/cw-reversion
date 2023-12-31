using System;
using System.Collections.Generic;

// Token: 0x0200029D RID: 669
[Serializable]
internal class MatchResultPlayerData : Convertible
{
	// Token: 0x060012D3 RID: 4819 RVA: 0x000CBC00 File Offset: 0x000C9E00
	public MatchResultPlayerData()
	{
	}

	// Token: 0x060012D4 RID: 4820 RVA: 0x000CBC14 File Offset: 0x000C9E14
	public MatchResultPlayerData(ServerNetPlayer player)
	{
		this.userID = player.UserInfo.userID;
		this.socialID = player.UserInfo.socialID;
		this.reputation = new Float((float)player.UserInfo.repa);
		this.nick = player.UserInfo.nick;
		this.nickColor = player.UserInfo.nickColor;
		this.clanTag = player.UserInfo.clanTag;
		this.spectactor = (player.playerInfo.playerType == PlayerType.spectactor);
		this.isBear = (player.playerInfo.playerType == PlayerType.bear);
		this.level = player.playerInfo.level;
		this.killCount = player.Stats.kills;
		this.deathCount = player.Stats.deaths;
		this.expa = player.playerInfo.points;
		this.kd_string = string.Format("{0:0.00}", (this.deathCount == 0) ? ((float)this.killCount) : ((float)this.killCount / (float)this.deathCount));
		this.socialInfo = player.UserInfo.socialInfo;
	}

	// Token: 0x060012D5 RID: 4821 RVA: 0x000CBD60 File Offset: 0x000C9F60
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "userID", ref this.userID, isWrite);
		JSON.ReadWrite(dict, "socialID", ref this.socialID, isWrite);
		JSON.ReadWrite(dict, "reputation", ref this.reputation, isWrite);
		JSON.ReadWrite(dict, "nick", ref this.nick, isWrite);
		JSON.ReadWrite(dict, "nickColor", ref this.nickColor, isWrite);
		JSON.ReadWrite(dict, "clanTag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "spectactor", ref this.spectactor, isWrite);
		JSON.ReadWrite(dict, "isBear", ref this.isBear, isWrite);
		JSON.ReadWrite(dict, "level", ref this.level, isWrite);
		JSON.ReadWrite(dict, "killCount", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "deathCount", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "expa", ref this.expa, isWrite);
		JSON.ReadWrite(dict, "kd_string", ref this.kd_string, isWrite);
		JSON.ReadWrite<Social>(dict, "socialInfo", ref this.socialInfo, isWrite);
	}

	// Token: 0x04001587 RID: 5511
	public int userID;

	// Token: 0x04001588 RID: 5512
	public string socialID;

	// Token: 0x04001589 RID: 5513
	public Float reputation;

	// Token: 0x0400158A RID: 5514
	public string nick;

	// Token: 0x0400158B RID: 5515
	public string nickColor = "#ffffff";

	// Token: 0x0400158C RID: 5516
	public string clanTag;

	// Token: 0x0400158D RID: 5517
	public bool isBear;

	// Token: 0x0400158E RID: 5518
	public bool spectactor;

	// Token: 0x0400158F RID: 5519
	public int level;

	// Token: 0x04001590 RID: 5520
	public int killCount;

	// Token: 0x04001591 RID: 5521
	public int deathCount;

	// Token: 0x04001592 RID: 5522
	public int expa;

	// Token: 0x04001593 RID: 5523
	public string kd_string;

	// Token: 0x04001594 RID: 5524
	public Social socialInfo;
}
