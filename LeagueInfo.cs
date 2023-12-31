using System;
using System.Collections.Generic;
using LeagueGUI;
using UnityEngine;

// Token: 0x0200032F RID: 815
internal class LeagueInfo
{
	// Token: 0x06001B7C RID: 7036 RVA: 0x000F7A98 File Offset: 0x000F5C98
	public void ParseSeason(Dictionary<string, object> dictionary)
	{
		object value;
		dictionary.TryGetValue("offseason", out value);
		object value2;
		dictionary.TryGetValue("next_season_start", out value2);
		if (Convert.ToBoolean(value))
		{
			this.Offseason = Convert.ToBoolean(value);
			this.NextSeasonStart = Convert.ToInt32(value2);
		}
		this.SeasonType = (int)dictionary["type"];
		this.SeasonName = (string)dictionary["name"];
		this.SeasonDescription = (string)dictionary["description"];
		this.SeasonOpenDate = (int)dictionary["date_open"];
		this.SeasonCloseDate = (int)dictionary["date_close"];
		this.SeasonUsersCount = (int)dictionary["users_count"];
		this.SeasonMatchesCount = (int)dictionary["matches_count"];
		this.SeasonStatus = (int)dictionary["status"];
		this.SeasonPrizes.Clear();
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)dictionary["prizes"]))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)keyValuePair.Value;
			this.SeasonPrizes.Add(new LeagueInfo.SeasonPrize
			{
				Currency = (int)dictionary2["cur"],
				Amount = (int)dictionary2["sum"],
				AwardLink = (string)dictionary2["award"]
			});
		}
	}

	// Token: 0x06001B7D RID: 7037 RVA: 0x000F7C68 File Offset: 0x000F5E68
	public void ParseAds(Dictionary<string, object>[] dictionary)
	{
		LeagueWindow.I.LeagueInfo.Ads = new LeagueInfo.AdInfo[dictionary.Length];
		int num = 0;
		foreach (Dictionary<string, object> dictionary2 in dictionary)
		{
			LeagueWindow.I.LeagueInfo.Ads[num] = new LeagueInfo.AdInfo
			{
				Title = (string)dictionary2["title"],
				Description = (string)dictionary2["description"],
				AdLink = (string)dictionary2["picture"],
				Active = (bool)dictionary2["status"]
			};
			num++;
		}
	}

	// Token: 0x06001B7E RID: 7038 RVA: 0x000F7D24 File Offset: 0x000F5F24
	public void ParseUser(Dictionary<string, object> dictionary)
	{
		this.UserId = (int)dictionary["uid"];
		this.UserLp = (int)dictionary["lp"];
		this.UserRank = (string)dictionary["rank"];
		this.UserLastReset = (int)dictionary["last_reset"];
		this.UserPLace = (int)dictionary["place"];
		this.UserWins = (int)dictionary["wins"];
		this.UserLoss = (int)dictionary["loss"];
		this.UserLeave = (int)dictionary["leav"];
	}

	// Token: 0x06001B7F RID: 7039 RVA: 0x000F7DE4 File Offset: 0x000F5FE4
	public void ParseRules(Dictionary<string, object> dictionary)
	{
		this.Rules = (string)dictionary["rules"];
	}

	// Token: 0x0400204B RID: 8267
	public int SeasonType;

	// Token: 0x0400204C RID: 8268
	public string SeasonName;

	// Token: 0x0400204D RID: 8269
	public string SeasonDescription;

	// Token: 0x0400204E RID: 8270
	public int SeasonOpenDate;

	// Token: 0x0400204F RID: 8271
	public int SeasonCloseDate;

	// Token: 0x04002050 RID: 8272
	public int SeasonUsersCount;

	// Token: 0x04002051 RID: 8273
	public int SeasonMatchesCount;

	// Token: 0x04002052 RID: 8274
	public int SeasonStatus;

	// Token: 0x04002053 RID: 8275
	public List<LeagueInfo.SeasonPrize> SeasonPrizes = new List<LeagueInfo.SeasonPrize>();

	// Token: 0x04002054 RID: 8276
	public LeagueInfo.AdInfo[] Ads;

	// Token: 0x04002055 RID: 8277
	public bool Offseason;

	// Token: 0x04002056 RID: 8278
	public int NextSeasonStart;

	// Token: 0x04002057 RID: 8279
	public int UserId;

	// Token: 0x04002058 RID: 8280
	public int UserLp;

	// Token: 0x04002059 RID: 8281
	public string UserRank;

	// Token: 0x0400205A RID: 8282
	public int UserLastReset;

	// Token: 0x0400205B RID: 8283
	public int UserPLace;

	// Token: 0x0400205C RID: 8284
	public int UserWins;

	// Token: 0x0400205D RID: 8285
	public int UserLoss;

	// Token: 0x0400205E RID: 8286
	public int UserLeave;

	// Token: 0x0400205F RID: 8287
	public string Rules;

	// Token: 0x04002060 RID: 8288
	public float AdShowTime;

	// Token: 0x04002061 RID: 8289
	public int WinPoint;

	// Token: 0x04002062 RID: 8290
	public int LosePoint;

	// Token: 0x04002063 RID: 8291
	public int LeavePoint;

	// Token: 0x02000330 RID: 816
	internal class SeasonPrize
	{
		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x000F7E04 File Offset: 0x000F6004
		public bool IsGp
		{
			get
			{
				return this.Currency > 1;
			}
		}

		// Token: 0x04002064 RID: 8292
		public int Currency;

		// Token: 0x04002065 RID: 8293
		public int Amount;

		// Token: 0x04002066 RID: 8294
		public string AwardLink;

		// Token: 0x04002067 RID: 8295
		public Texture2D AwardIcon;
	}

	// Token: 0x02000331 RID: 817
	internal class AdInfo
	{
		// Token: 0x04002068 RID: 8296
		public string Title;

		// Token: 0x04002069 RID: 8297
		public string Description;

		// Token: 0x0400206A RID: 8298
		public string AdLink;

		// Token: 0x0400206B RID: 8299
		public bool Active;

		// Token: 0x0400206C RID: 8300
		public Texture2D AdBanner;
	}
}
