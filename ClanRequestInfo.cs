using System;
using System.Collections.Generic;

// Token: 0x0200024E RID: 590
[Serializable]
internal class ClanRequestInfo : Convertible
{
	// Token: 0x0600120A RID: 4618 RVA: 0x000C7414 File Offset: 0x000C5614
	public ClanRequestInfo()
	{
	}

	// Token: 0x0600120B RID: 4619 RVA: 0x000C7470 File Offset: 0x000C5670
	public ClanRequestInfo(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x0600120C RID: 4620 RVA: 0x000C74D4 File Offset: 0x000C56D4
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

	// Token: 0x0600120D RID: 4621 RVA: 0x000C7524 File Offset: 0x000C5724
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "place", ref this.place, isWrite);
		JSON.ReadWrite(dict, "user_id", ref this.userID, isWrite);
		JSON.ReadWrite(dict, "social_id", ref this.socialID, isWrite);
		JSON.ReadWrite(dict, "curr_xp", ref this.currentExp, isWrite);
		this.level = this.GetPlayerLevel(this.currentExp.Value);
		JSON.ReadWrite(dict, "class", ref this.currentClass, isWrite);
		JSON.ReadWrite(dict, "user_name", ref this.userNick, isWrite);
		JSON.ReadWrite(dict, "perk_states", ref this.userNickColor, isWrite);
		if (!this.userNickColor.StartsWith("#"))
		{
			this.userNickColor = "#" + this.userNickColor;
		}
		JSON.ReadWrite(dict, "first_name", ref this.userFName, isWrite);
		JSON.ReadWrite(dict, "last_name", ref this.userLName, isWrite);
		JSON.ReadWrite(dict, "kill_count", ref this.killCount, isWrite);
		JSON.ReadWrite(dict, "death_count", ref this.deathCount, isWrite);
		JSON.ReadWrite(dict, "repa", ref this.reputation, isWrite);
		JSON.ReadWrite(dict, "prokills", ref this.prokills, isWrite);
		JSON.ReadWrite(dict, "achievements", ref this.achievements, isWrite);
		JSON.ReadWrite(dict, "contracts", ref this.contracts, isWrite);
	}

	// Token: 0x040011A0 RID: 4512
	public int place;

	// Token: 0x040011A1 RID: 4513
	public int userID;

	// Token: 0x040011A2 RID: 4514
	public string socialID = string.Empty;

	// Token: 0x040011A3 RID: 4515
	public Float currentExp = new Float(0f);

	// Token: 0x040011A4 RID: 4516
	public int currentClass;

	// Token: 0x040011A5 RID: 4517
	public string userNick = string.Empty;

	// Token: 0x040011A6 RID: 4518
	public string userNickColor = "#FFFFFF";

	// Token: 0x040011A7 RID: 4519
	public string userFName = string.Empty;

	// Token: 0x040011A8 RID: 4520
	public string userLName = string.Empty;

	// Token: 0x040011A9 RID: 4521
	public int killCount;

	// Token: 0x040011AA RID: 4522
	public int deathCount;

	// Token: 0x040011AB RID: 4523
	public int reputation;

	// Token: 0x040011AC RID: 4524
	public int prokills;

	// Token: 0x040011AD RID: 4525
	public int achievements;

	// Token: 0x040011AE RID: 4526
	public int contracts;

	// Token: 0x040011AF RID: 4527
	public int level;
}
