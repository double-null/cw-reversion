using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200029E RID: 670
[Serializable]
internal class MatchResultData : Convertible
{
	// Token: 0x060012D6 RID: 4822 RVA: 0x000CBE6C File Offset: 0x000CA06C
	public MatchResultData()
	{
	}

	// Token: 0x060012D7 RID: 4823 RVA: 0x000CBE74 File Offset: 0x000CA074
	public MatchResultData(ServerNetPlayer player)
	{
		this.oldXP = (float)player.UserInfo.currentXP;
		this.newXP = this.oldXP + player.ObtainedExp;
		this.AchievementCR = player.Stats.achivCR;
		this.gainedCR = Math.Max(Mathf.RoundToInt(player.Stats.obtainedXP * Main.GameModeInfo.incomeCoef), 0);
		if (this.IsNight && player.UserInfo.skillUnlocked(Skills.car_night))
		{
			this.gainedCR = Mathf.RoundToInt((float)this.gainedCR * 0.8f);
		}
		this.XP_player_mult = (float)player.ExpMult;
		this.XP_map_mult = Main.GameModeInfo.expCoef;
		this.CR_map_mult = Main.GameModeInfo.incomeCoef;
		this.playerInfo = new MatchResultPlayerData(player);
		this.matchEndBonus = player.matchExpBonus;
		this.ClanEarnProc = player.UserInfo.ClanEarnProc;
		if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus3))
		{
			this.ClanEarnProc *= 1.2f;
		}
		else if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus2))
		{
			this.ClanEarnProc *= 1.1f;
		}
		else if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus1))
		{
			this.ClanEarnProc *= 1.05f;
		}
	}

	// Token: 0x060012D8 RID: 4824 RVA: 0x000CBFEC File Offset: 0x000CA1EC
	public MatchResultData(MatchResultData data, ServerNetPlayer player)
	{
		this.oldXP = (float)player.UserInfo.currentXP;
		this.newXP = this.oldXP + player.ObtainedExp;
		this.AchievementCR = player.Stats.achivCR;
		this.gainedCR = Math.Max(Mathf.CeilToInt(player.Stats.obtainedXP * Main.GameModeInfo.incomeCoef), 0);
		if (Peer.ServerGame.is_night && player.UserInfo.skillUnlocked(Skills.car_night))
		{
			this.gainedCR = Mathf.CeilToInt((float)this.gainedCR * 1.3f);
		}
		this.sp_gained = player.Stats.obtainedSP;
		this.IsNight = (Peer.ServerGame.is_night && (player.UserInfo.skillUnlocked(Skills.car_night) || player.UserInfo.skillUnlocked(Skills.car_night2)));
		this.XP_player_mult = (float)player.ExpMult;
		this.XP_map_mult = Main.GameModeInfo.expCoef;
		this.CR_map_mult = Main.GameModeInfo.incomeCoef;
		this.playerInfo = new MatchResultPlayerData(player);
		this.teamWin = player.matchResultData.teamWin;
		this.record = player.matchResultData.record;
		this.recordValues = player.matchResultData.recordValues;
		this.bestPlayer = data.bestPlayer;
		this.worstPlayer = data.worstPlayer;
		this.players = data.players;
		this.tax = data.tax;
		this.matchEndBonus = player.matchExpBonus;
		this.ClanEarnProc = player.UserInfo.ClanEarnProc;
		this.MpGained = player.Stats.obtainedMP;
		if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus3))
		{
			this.ClanEarnProc *= 1.2f;
		}
		else if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus2))
		{
			this.ClanEarnProc *= 1.1f;
		}
		else if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_bonus1))
		{
			this.ClanEarnProc *= 1.05f;
		}
		this.gameTotalTime = data.gameTotalTime;
		this.gameTotalDeaths = data.gameTotalDeaths;
		this.gameTotalKills = data.gameTotalKills;
	}

	// Token: 0x060012D9 RID: 4825 RVA: 0x000CC250 File Offset: 0x000CA450
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "oldXP", ref this.oldXP, isWrite);
		JSON.ReadWrite(dict, "newXP", ref this.newXP, isWrite);
		JSON.ReadWrite(dict, "AchievementCR", ref this.AchievementCR, isWrite);
		JSON.ReadWrite(dict, "gainedCR", ref this.gainedCR, isWrite);
		JSON.ReadWrite(dict, "XP_player_mult", ref this.XP_player_mult, isWrite);
		JSON.ReadWrite(dict, "XP_map_mult", ref this.XP_map_mult, isWrite);
		JSON.ReadWrite(dict, "CR_map_mult", ref this.CR_map_mult, isWrite);
		JSON.ReadWrite<MatchResultPlayerData>(dict, "playerInfo", ref this.playerInfo, isWrite);
		JSON.ReadWrite(dict, "teamWin", ref this.teamWin, isWrite);
		JSON.ReadWrite(dict, "record", ref this.record, isWrite);
		JSON.ReadWrite(dict, "recordValues", ref this.recordValues, isWrite);
		JSON.ReadWrite<MatchResultPlayerData>(dict, "players", ref this.players, isWrite);
		JSON.ReadWrite<MatchResultData>(dict, "bestPlayer", ref this.bestPlayer, isWrite);
		JSON.ReadWrite<MatchResultData>(dict, "worstPlayer", ref this.worstPlayer, isWrite);
		JSON.ReadWrite(dict, "gameTotalTime", ref this.gameTotalTime, isWrite);
		JSON.ReadWrite(dict, "gameTotalDeaths", ref this.gameTotalDeaths, isWrite);
		JSON.ReadWrite(dict, "gameTotalKills", ref this.gameTotalKills, isWrite);
		JSON.ReadWrite(dict, "isNight", ref this.IsNight, isWrite);
		JSON.ReadWrite(dict, "tax", ref this.tax, isWrite);
		JSON.ReadWrite(dict, "sp_gained", ref this.sp_gained, isWrite);
		JSON.ReadWrite(dict, "matchEndBonus", ref this.matchEndBonus, isWrite);
		JSON.ReadWrite(dict, "ClanEarnProc", ref this.ClanEarnProc, isWrite);
		JSON.ReadWrite(dict, "MpGained", ref this.MpGained, isWrite);
	}

	// Token: 0x04001595 RID: 5525
	public float oldXP;

	// Token: 0x04001596 RID: 5526
	public float newXP;

	// Token: 0x04001597 RID: 5527
	public int gainedCR;

	// Token: 0x04001598 RID: 5528
	public int AchievementCR;

	// Token: 0x04001599 RID: 5529
	public float XP_player_mult;

	// Token: 0x0400159A RID: 5530
	public float XP_map_mult;

	// Token: 0x0400159B RID: 5531
	public float CR_map_mult;

	// Token: 0x0400159C RID: 5532
	public MatchResultPlayerData playerInfo;

	// Token: 0x0400159D RID: 5533
	public bool teamWin;

	// Token: 0x0400159E RID: 5534
	public int record;

	// Token: 0x0400159F RID: 5535
	public int[] recordValues;

	// Token: 0x040015A0 RID: 5536
	public MatchResultPlayerData[] players;

	// Token: 0x040015A1 RID: 5537
	public MatchResultData bestPlayer;

	// Token: 0x040015A2 RID: 5538
	public MatchResultData worstPlayer;

	// Token: 0x040015A3 RID: 5539
	public int gameTotalTime;

	// Token: 0x040015A4 RID: 5540
	public int gameTotalDeaths;

	// Token: 0x040015A5 RID: 5541
	public int gameTotalKills;

	// Token: 0x040015A6 RID: 5542
	public int tax;

	// Token: 0x040015A7 RID: 5543
	public int sp_gained;

	// Token: 0x040015A8 RID: 5544
	public bool IsNight;

	// Token: 0x040015A9 RID: 5545
	public int matchEndBonus;

	// Token: 0x040015AA RID: 5546
	public float ClanEarnProc;

	// Token: 0x040015AB RID: 5547
	public int MpGained;
}
