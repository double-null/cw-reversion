using System;
using System.Collections.Generic;

// Token: 0x0200022A RID: 554
[Serializable]
internal class Rating : Convertible
{
	// Token: 0x06001154 RID: 4436 RVA: 0x000C1598 File Offset: 0x000BF798
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "place", ref this.place, isWrite);
		JSON.ReadWrite(dict, "level", ref this.level, isWrite);
		JSON.ReadWrite(dict, "user_id", ref this.userID, isWrite);
		JSON.ReadWrite(dict, "clanTag", ref this.clanTag, isWrite);
		JSON.ReadWrite(dict, "exp", ref this.exp, isWrite);
		JSON.ReadWrite(dict, "kills", ref this.kills, isWrite);
		JSON.ReadWrite(dict, "deaths", ref this.deaths, isWrite);
		JSON.ReadWrite(dict, "kd", ref this.kd, isWrite);
		JSON.ReadWrite(dict, "repa", ref this.repa, isWrite);
		this.kd_string = string.Format("{0:0.00}", this.kd);
		JSON.ReadWrite(dict, "online", ref this.online, isWrite);
		JSON.ReadWrite(dict, "lastOnline", ref this.lastOnline, isWrite);
		JSON.ReadWrite(dict, "onlineData", ref this.onlineData, isWrite);
		JSON.ReadWrite(dict, "onlineType", ref this.onlineType, isWrite);
		JSON.ReadWrite(dict, "ip", ref this.ip, isWrite);
		JSON.ReadWrite(dict, "port", ref this.port, isWrite);
		JSON.ReadWrite(dict, "class", ref this.class_int, isWrite);
		if (this.class_int >= 7)
		{
			this.class_int = 0;
		}
		object obj;
		dict.TryGetValue("name", out obj);
		object obj2;
		dict.TryGetValue("perk_states", out obj2);
		object obj3;
		dict.TryGetValue("first_name", out obj3);
		object obj4;
		dict.TryGetValue("last_name", out obj4);
		if (obj != null)
		{
			this.name = obj.ToString();
		}
		if (obj2 != null)
		{
			this.nameColor = obj2.ToString();
			if (!this.nameColor.StartsWith("#"))
			{
				this.nameColor = "#" + this.nameColor;
			}
		}
		if (obj3 != null)
		{
			this.first_name = obj3.ToString();
		}
		if (obj4 != null)
		{
			this.last_name = obj4.ToString();
		}
		JSON.ReadWrite(dict, "platform", ref this.SocialNet, isWrite);
		JSON.ReadWrite(dict, "season_reward", ref this.SeasonReward, isWrite);
	}

	// Token: 0x04001102 RID: 4354
	public int place;

	// Token: 0x04001103 RID: 4355
	public int level;

	// Token: 0x04001104 RID: 4356
	public int userID;

	// Token: 0x04001105 RID: 4357
	public string name;

	// Token: 0x04001106 RID: 4358
	public string clanTag;

	// Token: 0x04001107 RID: 4359
	public string first_name;

	// Token: 0x04001108 RID: 4360
	public string last_name;

	// Token: 0x04001109 RID: 4361
	public string nameColor = "#FFFFFF";

	// Token: 0x0400110A RID: 4362
	public float exp;

	// Token: 0x0400110B RID: 4363
	public int kills;

	// Token: 0x0400110C RID: 4364
	public int deaths;

	// Token: 0x0400110D RID: 4365
	public float kd;

	// Token: 0x0400110E RID: 4366
	public Float repa;

	// Token: 0x0400110F RID: 4367
	public string kd_string;

	// Token: 0x04001110 RID: 4368
	public bool online;

	// Token: 0x04001111 RID: 4369
	public string lastOnline;

	// Token: 0x04001112 RID: 4370
	public string onlineData;

	// Token: 0x04001113 RID: 4371
	public int onlineType;

	// Token: 0x04001114 RID: 4372
	public string ip = string.Empty;

	// Token: 0x04001115 RID: 4373
	public int port = -1;

	// Token: 0x04001116 RID: 4374
	public int class_int;

	// Token: 0x04001117 RID: 4375
	public int SeasonReward;

	// Token: 0x04001118 RID: 4376
	public int SocialNet;
}
