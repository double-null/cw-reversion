using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Foundation
{
	// Token: 0x020002DD RID: 733
	internal class WatchlistItem : Convertible
	{
		// Token: 0x06001462 RID: 5218 RVA: 0x000D819C File Offset: 0x000D639C
		public void Convert(Dictionary<string, object> dict, bool isWrite)
		{
			JSON.ReadWrite(dict, "user_id", ref this.UserId, isWrite);
			JSON.ReadWrite(dict, "level", ref this.PlayerLevel, isWrite);
			JSON.ReadWrite(dict, "class", ref this.PlayerClass, isWrite);
			JSON.ReadWrite(dict, "exp", ref this.PlayerExp, isWrite);
			JSON.ReadWrite(dict, "kills", ref this.PlayerKills, isWrite);
			JSON.ReadWrite(dict, "deaths", ref this.PlayerDeaths, isWrite);
			JSON.ReadWrite(dict, "kd", ref this.Kd, isWrite);
			JSON.ReadWrite(dict, "repa", ref this.PlayerReputation, isWrite);
			JSON.ReadWrite(dict, "canvote", ref this.Canvote, isWrite);
			JSON.ReadWrite(dict, "online", ref this.IsOnline, isWrite);
			JSON.ReadWrite(dict, "onlineType", ref this.OnlineType, isWrite);
			JSON.ReadWrite(dict, "ip", ref this.Ip, isWrite);
			JSON.ReadWrite(dict, "port", ref this.Port, isWrite);
			JSON.ReadWrite(dict, "clanTag", ref this.ClanTag, isWrite);
			object obj;
			dict.TryGetValue("name", out obj);
			object obj2;
			dict.TryGetValue("first_name", out obj2);
			object obj3;
			dict.TryGetValue("last_name", out obj3);
			if (obj != null)
			{
				this.PlayerNickname = obj.ToString();
			}
			if (obj2 != null)
			{
				this.PlayerFirstname = obj2.ToString();
			}
			if (obj3 != null)
			{
				this.PlayerLastname = obj3.ToString();
			}
		}

		// Token: 0x0400190E RID: 6414
		public int UserId;

		// Token: 0x0400190F RID: 6415
		public int PlayerLevel;

		// Token: 0x04001910 RID: 6416
		public int PlayerClass;

		// Token: 0x04001911 RID: 6417
		public int PlayerExp;

		// Token: 0x04001912 RID: 6418
		public int PlayerKills;

		// Token: 0x04001913 RID: 6419
		public int PlayerDeaths;

		// Token: 0x04001914 RID: 6420
		public int PlayerReputation;

		// Token: 0x04001915 RID: 6421
		public float Kd;

		// Token: 0x04001916 RID: 6422
		public bool Canvote;

		// Token: 0x04001917 RID: 6423
		public bool IsOnline;

		// Token: 0x04001918 RID: 6424
		public string ClanTag;

		// Token: 0x04001919 RID: 6425
		public string PlayerNickname;

		// Token: 0x0400191A RID: 6426
		public string PlayerFirstname;

		// Token: 0x0400191B RID: 6427
		public string PlayerLastname;

		// Token: 0x0400191C RID: 6428
		public int OnlineType;

		// Token: 0x0400191D RID: 6429
		public string Ip;

		// Token: 0x0400191E RID: 6430
		public int Port;
	}
}
