using System;
using System.Collections.Generic;

// Token: 0x020001F7 RID: 503
[Serializable]
internal class ClanRating : Convertible
{
	// Token: 0x1700025C RID: 604
	// (get) Token: 0x0600106E RID: 4206 RVA: 0x000B986C File Offset: 0x000B7A6C
	public static float Tag_Width
	{
		get
		{
			return (ClanRating.tag_width >= 200f) ? 200f : ClanRating.tag_width;
		}
	}

	// Token: 0x1700025D RID: 605
	// (get) Token: 0x0600106F RID: 4207 RVA: 0x000B988C File Offset: 0x000B7A8C
	public static float Name_Width
	{
		get
		{
			return (ClanRating.name_width >= 300f) ? 300f : ClanRating.name_width;
		}
	}

	// Token: 0x1700025E RID: 606
	// (get) Token: 0x06001070 RID: 4208 RVA: 0x000B98AC File Offset: 0x000B7AAC
	public static float Gamers_Count_Width
	{
		get
		{
			return (ClanRating.gamers_count_width <= 50f) ? 50f : ((ClanRating.gamers_count_width >= 200f) ? 200f : ClanRating.gamers_count_width);
		}
	}

	// Token: 0x06001071 RID: 4209 RVA: 0x000B98E8 File Offset: 0x000B7AE8
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "tag", ref this.tag, isWrite);
		JSON.ReadWrite(dict, "name", ref this.name, isWrite);
		JSON.ReadWrite(dict, "clan_exp", ref this.exp, isWrite);
		JSON.ReadWrite(dict, "gamers_count", ref this.gamers_count, isWrite);
		JSON.ReadWrite(dict, "id", ref this.ID, isWrite);
		JSON.ReadWrite(dict, "create_date", ref this.create_date, isWrite);
		if (ClanRating.tag_width < (float)this.tag.Length * 6.2f)
		{
			ClanRating.tag_width = (float)this.tag.Length * 6.2f;
		}
		if (ClanRating.name_width < (float)this.name.Length * 6.2f + 50f)
		{
			ClanRating.name_width = (float)this.name.Length * 6.2f + 50f;
		}
		if (ClanRating.gamers_count_width < (float)this.gamers_count.ToString().Length * 8.2f + 50f)
		{
			ClanRating.gamers_count_width = (float)this.gamers_count.ToString().Length * 8.2f + 50f;
		}
	}

	// Token: 0x040010CF RID: 4303
	public string tag;

	// Token: 0x040010D0 RID: 4304
	public string name;

	// Token: 0x040010D1 RID: 4305
	public float gamers_count;

	// Token: 0x040010D2 RID: 4306
	public float exp;

	// Token: 0x040010D3 RID: 4307
	public int ID;

	// Token: 0x040010D4 RID: 4308
	public int create_date;

	// Token: 0x040010D5 RID: 4309
	private static float tag_width = 86.8f;

	// Token: 0x040010D6 RID: 4310
	private static float name_width = 174f;

	// Token: 0x040010D7 RID: 4311
	private static float gamers_count_width = 58.2f;
}
