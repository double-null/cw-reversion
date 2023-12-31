using System;

namespace LeagueSystem
{
	// Token: 0x02000300 RID: 768
	[Serializable]
	internal class PlayerJsonData
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x000EFF80 File Offset: 0x000EE180
		public void SetRemoteData(RemoteMatchendPlayerData data)
		{
			this.wins = data.wins;
			this.lp = data.lp;
			this.lp_delta = data.lp_delta;
			this.loss = data.loss;
			this.leav = data.leav;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000EFFCC File Offset: 0x000EE1CC
		public void SetPlayerStats(Stats playerStats)
		{
			this.delta_exp = playerStats.obtainedXP;
			this.isWinner = playerStats.IsWinner;
			this.kills = playerStats.kills;
			this.deaths = playerStats.deaths;
			this.timeOnline = playerStats.timeOnline;
			this.timeHardcore = playerStats.timeHardcore;
			this.totalAmmo = playerStats.totalAmmo;
			this.totalDamage = playerStats.totalDamage;
			this.headShots = playerStats.headShots;
			this.doubleKills = playerStats.doubleKills;
			this.doubleHeadShots = playerStats.doubleHeadShots;
			this.tripleKills = playerStats.tripleKills;
			this.longShots = playerStats.longShots;
			this.grenadeKills = playerStats.grenadeKills;
			this.suicides = playerStats.suicides;
			this.rageKills = playerStats.rageKills;
			this.stormKills = playerStats.stormKills;
			this.proKills = playerStats.proKills;
			if (Globals.I.LegendaryKill)
			{
				this.legendaryKills = playerStats.legendaryKills;
			}
			this.assists = playerStats.assists;
			this.teamKills = playerStats.teamKills;
			this.bearKills = playerStats.bearKills;
			this.usecKills = playerStats.usecKills;
			this.knifeKills = playerStats.knifeKills;
			this.favGun = playerStats.favGun;
			this.armstreaksEarned = playerStats.armstreaksEarned;
			this.armstreaksUsed = playerStats.armstreaksUsed;
			this.totalHits = playerStats.totalHits;
		}

		// Token: 0x04001F00 RID: 7936
		public string uid = string.Empty;

		// Token: 0x04001F01 RID: 7937
		public int wins;

		// Token: 0x04001F02 RID: 7938
		public int loss;

		// Token: 0x04001F03 RID: 7939
		public int leav;

		// Token: 0x04001F04 RID: 7940
		public int lp;

		// Token: 0x04001F05 RID: 7941
		public int lp_delta;

		// Token: 0x04001F06 RID: 7942
		public int team;

		// Token: 0x04001F07 RID: 7943
		public bool accept;

		// Token: 0x04001F08 RID: 7944
		public string player_class = string.Empty;

		// Token: 0x04001F09 RID: 7945
		public string curr_xp = string.Empty;

		// Token: 0x04001F0A RID: 7946
		public string social_id = string.Empty;

		// Token: 0x04001F0B RID: 7947
		public int clan_id = 4;

		// Token: 0x04001F0C RID: 7948
		public string user_name = string.Empty;

		// Token: 0x04001F0D RID: 7949
		public string tag = string.Empty;

		// Token: 0x04001F0E RID: 7950
		public string first_name = string.Empty;

		// Token: 0x04001F0F RID: 7951
		public string last_name = string.Empty;

		// Token: 0x04001F10 RID: 7952
		public int level;

		// Token: 0x04001F11 RID: 7953
		public float disconnect_time = -1f;

		// Token: 0x04001F12 RID: 7954
		public PlayerStatus status;

		// Token: 0x04001F13 RID: 7955
		public int isWinner;

		// Token: 0x04001F14 RID: 7956
		public int kills;

		// Token: 0x04001F15 RID: 7957
		public int deaths;

		// Token: 0x04001F16 RID: 7958
		public int timeOnline;

		// Token: 0x04001F17 RID: 7959
		public int timeHardcore;

		// Token: 0x04001F18 RID: 7960
		public int totalAmmo;

		// Token: 0x04001F19 RID: 7961
		public int totalDamage;

		// Token: 0x04001F1A RID: 7962
		public int headShots;

		// Token: 0x04001F1B RID: 7963
		public int doubleKills;

		// Token: 0x04001F1C RID: 7964
		public int doubleHeadShots;

		// Token: 0x04001F1D RID: 7965
		public int tripleKills;

		// Token: 0x04001F1E RID: 7966
		public int longShots;

		// Token: 0x04001F1F RID: 7967
		public int grenadeKills;

		// Token: 0x04001F20 RID: 7968
		public int suicides;

		// Token: 0x04001F21 RID: 7969
		public int rageKills;

		// Token: 0x04001F22 RID: 7970
		public int stormKills;

		// Token: 0x04001F23 RID: 7971
		public int proKills;

		// Token: 0x04001F24 RID: 7972
		public int legendaryKills;

		// Token: 0x04001F25 RID: 7973
		public int assists;

		// Token: 0x04001F26 RID: 7974
		public int teamKills;

		// Token: 0x04001F27 RID: 7975
		public int bearKills;

		// Token: 0x04001F28 RID: 7976
		public int usecKills;

		// Token: 0x04001F29 RID: 7977
		public int knifeKills;

		// Token: 0x04001F2A RID: 7978
		public int favGun;

		// Token: 0x04001F2B RID: 7979
		public int armstreaksEarned;

		// Token: 0x04001F2C RID: 7980
		public int armstreaksUsed;

		// Token: 0x04001F2D RID: 7981
		public int totalHits;

		// Token: 0x04001F2E RID: 7982
		public float delta_exp;
	}
}
