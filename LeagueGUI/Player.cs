using System;
using System.Collections;
using ClanSystemGUI;
using LeagueSystem;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200030C RID: 780
	internal class Player
	{
		// Token: 0x06001A95 RID: 6805 RVA: 0x000F0BB4 File Offset: 0x000EEDB4
		public Player(PlayerJsonData playerData) : this()
		{
			this.Level = playerData.level;
			int.TryParse(playerData.player_class, out this.Class);
			this.Tag = playerData.tag;
			this.Nick = playerData.user_name;
			this.FirstName = playerData.first_name;
			this.LastName = playerData.last_name;
			this.LevelIcon = MainGUI.Instance.rank_icon[this.Level];
			if (this.Class != 0)
			{
				this.ClassIcon = ClanSystemWindow.I.Textures.statsClass[this.Class - 1];
			}
			this.Points = playerData.lp;
			this.Wins = playerData.wins;
			this.Defeats = playerData.loss;
			this.Leaves = playerData.leav;
			this.Exp = playerData.delta_exp;
			this.Kills = playerData.kills;
			this.Deaths = playerData.deaths;
			this.Assists = playerData.assists;
			this.Headshots = playerData.headShots;
			this.Longshots = playerData.longShots;
			this.GrenadeKills = playerData.grenadeKills;
			this.KnifeKills = playerData.knifeKills;
			int num = playerData.wins + playerData.loss + playerData.leav;
			this.Ratio = ((num <= 0) ? 100f : ((float)this.Wins / (float)num * 100f));
			float num2 = 1f;
			if (this.Deaths > 0)
			{
				num2 = (float)this.Deaths;
			}
			this.KD = (float)this.Kills / num2;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000F0D4C File Offset: 0x000EEF4C
		public Player()
		{
			if (!Player._avatarDownloaded)
			{
				CoroutineRunner.RunCoroutine(this.DownloadAvatar());
			}
			this.Level = Main.UserInfo.currentLevel;
			this.Class = (int)Main.UserInfo.playerClass;
			this.Tag = Main.UserInfo.clanTag;
			this.Nick = Main.UserInfo.nick;
			this.FirstName = Main.UserInfo.socialInfo.firstName;
			this.LastName = Main.UserInfo.socialInfo.lastName;
			this.LevelIcon = MainGUI.Instance.rank_icon[this.Level];
			if (this.Class != 0)
			{
				this.ClassIcon = ClanSystemWindow.I.Textures.statsClass[this.Class - 1];
			}
			this.RankIcon = LeagueWindow.I.Textures.Champion;
			this.Place = LeagueWindow.I.LeagueInfo.UserPLace;
			this.Rank = LeagueWindow.I.LeagueInfo.UserRank;
			this.Points = LeagueWindow.I.LeagueInfo.UserLp;
			this.Wins = LeagueWindow.I.LeagueInfo.UserWins;
			this.Defeats = LeagueWindow.I.LeagueInfo.UserLoss;
			this.Leaves = LeagueWindow.I.LeagueInfo.UserLeave;
			int num = LeagueWindow.I.LeagueInfo.UserWins + LeagueWindow.I.LeagueInfo.UserLoss + LeagueWindow.I.LeagueInfo.UserLeave;
			this.Ratio = ((num <= 0) ? 100f : ((float)this.Wins / (float)num * 100f));
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000F0F34 File Offset: 0x000EF134
		private IEnumerator DownloadAvatar()
		{
			if (string.IsNullOrEmpty(Main.UserInfo.socialInfo.photo))
			{
				yield break;
			}
			WWW www = new WWW(Main.UserInfo.socialInfo.photo);
			yield return www;
			if (www.error != null)
			{
				yield break;
			}
			Player._avatar = www.texture;
			Player._avatarDownloaded = true;
			www.Dispose();
			yield break;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000F0F48 File Offset: 0x000EF148
		public void RefreshData()
		{
			this.Place = LeagueWindow.I.LeagueInfo.UserPLace;
			this.Rank = LeagueWindow.I.LeagueInfo.UserRank;
			this.Points = LeagueWindow.I.LeagueInfo.UserLp;
			this.Wins = LeagueWindow.I.LeagueInfo.UserWins;
			this.Defeats = LeagueWindow.I.LeagueInfo.UserLoss;
			this.Leaves = LeagueWindow.I.LeagueInfo.UserLeave;
			int num = LeagueWindow.I.LeagueInfo.UserWins + LeagueWindow.I.LeagueInfo.UserLoss + LeagueWindow.I.LeagueInfo.UserLeave;
			this.Ratio = ((num <= 0) ? 100f : ((float)this.Wins / (float)num * 100f));
			Debug.Log("DATA R");
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000F1034 File Offset: 0x000EF234
		public void DrawAvatar(float x, float y)
		{
			GUI.DrawTexture(new Rect(x, y, 50f, 50f), Player._avatar);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000F1054 File Offset: 0x000EF254
		public void DrawPlayerInfo(float x, float y)
		{
			float num = 0f;
			string tag = Helpers.GetTag(this.Tag);
			GUI.DrawTexture(new Rect(x + 55f, y + 5f, (float)this.LevelIcon.width, (float)this.LevelIcon.height), this.LevelIcon);
			if (this.ClassIcon)
			{
				GUI.DrawTexture(new Rect(x + 80f, y - 1f, (float)this.ClassIcon.width, (float)this.ClassIcon.height), this.ClassIcon);
				num += 50f + (float)this.LevelIcon.width + (float)this.ClassIcon.width;
			}
			else
			{
				num += 50f + (float)this.LevelIcon.width + 10f;
			}
			GUI.Label(new Rect(x + num, y + 8f, 100f, 16f), Helpers.ColoredTag(this.Tag), LeagueWindow.I.Styles.WhiteLabel16);
			if (tag != string.Empty)
			{
				num += MainGUI.Instance.CalcWidth(tag, MainGUI.Instance.fontDNC57, 16);
			}
			GUI.Label(new Rect(x + num, y + 8f, 100f, 16f), this.Nick, LeagueWindow.I.Styles.WhiteLabel16);
			num = 50f + (float)((!this.ClassIcon) ? 31 : this.ClassIcon.width) + 8f;
			GUI.Label(new Rect(x + num, y + 26f, 100f, 14f), this.FirstName, LeagueWindow.I.Styles.GrayLabel);
			num += MainGUI.Instance.CalcWidth(this.FirstName, MainGUI.Instance.fontDNC57, 14);
			GUI.Label(new Rect(x + num, y + 26f, 100f, 14f), this.LastName, LeagueWindow.I.Styles.GrayLabel);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000F127C File Offset: 0x000EF47C
		public void DrawShortPlayerInfo(float x, float y)
		{
			LeagueHelpers.DrawRank(x + 10f, y + 5f, this.Points, 1f, false);
			if (this.ClassIcon)
			{
				GUI.DrawTexture(new Rect(x + 65f, y - 5f, (float)this.ClassIcon.width, (float)this.ClassIcon.height), this.ClassIcon);
			}
			GUI.Label(new Rect(x + (float)((!this.ClassIcon) ? 75 : 92), y, 50f, 25f), (!(this.Tag != string.Empty)) ? this.Nick : (Helpers.ColoredTag(this.Tag) + " " + this.Nick), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 28f, y, 50f, 25f), this.Level.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 213f, y, 50f, 25f), this.Points.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 250f, y, 50f, 25f), this.Wins.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 278f, y, 50f, 25f), this.Defeats.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 302f, y, 50f, 25f), this.Leaves.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleLeft;
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000F14A4 File Offset: 0x000EF6A4
		public void DrawFullPlayerInfo(float x, float y)
		{
			LeagueHelpers.DrawRank(x + 10f, y + 5f, this.Points, 1f, false);
			if (this.ClassIcon)
			{
				GUI.DrawTexture(new Rect(x + 65f, y - 5f, (float)this.ClassIcon.width, (float)this.ClassIcon.height), this.ClassIcon);
			}
			GUI.Label(new Rect(x + (float)((!this.ClassIcon) ? 75 : 92), y, 50f, 25f), (!(this.Tag != string.Empty)) ? this.Nick : (Helpers.ColoredTag(this.Tag) + " " + this.Nick), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 28f, y, 50f, 25f), this.Level.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 213f, y, 50f, 25f), this.Points.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 250f, y, 50f, 25f), this.Wins.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 278f, y, 50f, 25f), this.Defeats.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 302f, y, 50f, 25f), this.Leaves.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 362f, y, 50f, 25f), this.Exp.ToString("F0"), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 412f, y, 50f, 25f), this.Kills.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 465f, y, 50f, 25f), this.Deaths.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 522f, y, 50f, 25f), this.Assists.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 572f, y, 50f, 25f), this.KD.ToString("F2"), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 618f, y, 50f, 25f), this.Headshots.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 648f, y, 50f, 25f), this.Longshots.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 675f, y, 50f, 25f), this.GrenadeKills.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x + 702f, y, 50f, 25f), this.KnifeKills.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleLeft;
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000F18BC File Offset: 0x000EFABC
		public void DrawPlayerStats(float x, float y)
		{
			LeagueHelpers.DrawRank(x + 15f, y + 66f, this.Points, 1f, false);
			GUI.Label(new Rect(x + 45f, y + 65f, 40f, 20f), this.Place.ToString(), LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 90f, y + 65f, 40f, 20f), this.Points.ToString(), LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 135f, y + 65f, 40f, 20f), this.Wins.ToString(), LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 180f, y + 65f, 40f, 20f), this.Defeats.ToString(), LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 225f, y + 65f, 40f, 20f), this.Leaves.ToString(), LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 270f, y + 65f, 40f, 20f), this.Ratio.ToString("F0") + "%", LeagueWindow.I.Styles.WhiteLabel20);
			GUI.Label(new Rect(x + 5f, y + 87f, 40f, 14f), Language.LeagueRank.ToLower(), LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 45f, y + 87f, 40f, 14f), Language.LeaguePlace, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 90f, y + 87f, 40f, 14f), Language.LeagueLP, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 135f, y + 87f, 40f, 14f), Language.LeagueWins, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 180f, y + 87f, 40f, 14f), Language.LeagueDefeats, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 225f, y + 87f, 40f, 14f), Language.LeagueLeaves, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(new Rect(x + 270f, y + 87f, 40f, 14f), Language.LeagueRatio, LeagueWindow.I.Styles.BrownLabel);
		}

		// Token: 0x04001FAF RID: 8111
		private const float AvatarDimension = 50f;

		// Token: 0x04001FB0 RID: 8112
		public Texture2D LevelIcon;

		// Token: 0x04001FB1 RID: 8113
		public Texture2D ClassIcon;

		// Token: 0x04001FB2 RID: 8114
		public Texture2D RankIcon;

		// Token: 0x04001FB3 RID: 8115
		public int Level;

		// Token: 0x04001FB4 RID: 8116
		public int Class;

		// Token: 0x04001FB5 RID: 8117
		public int Place;

		// Token: 0x04001FB6 RID: 8118
		public int Points;

		// Token: 0x04001FB7 RID: 8119
		public int Wins;

		// Token: 0x04001FB8 RID: 8120
		public int Defeats;

		// Token: 0x04001FB9 RID: 8121
		public int Leaves;

		// Token: 0x04001FBA RID: 8122
		public float Ratio;

		// Token: 0x04001FBB RID: 8123
		public string Rank;

		// Token: 0x04001FBC RID: 8124
		public string Tag;

		// Token: 0x04001FBD RID: 8125
		public string Nick;

		// Token: 0x04001FBE RID: 8126
		public string FirstName;

		// Token: 0x04001FBF RID: 8127
		public string LastName;

		// Token: 0x04001FC0 RID: 8128
		public float Exp;

		// Token: 0x04001FC1 RID: 8129
		public int Kills;

		// Token: 0x04001FC2 RID: 8130
		public int Deaths;

		// Token: 0x04001FC3 RID: 8131
		public int Assists;

		// Token: 0x04001FC4 RID: 8132
		public float KD;

		// Token: 0x04001FC5 RID: 8133
		public int Headshots;

		// Token: 0x04001FC6 RID: 8134
		public int Longshots;

		// Token: 0x04001FC7 RID: 8135
		public int GrenadeKills;

		// Token: 0x04001FC8 RID: 8136
		public int KnifeKills;

		// Token: 0x04001FC9 RID: 8137
		private int[] ranks = new int[]
		{
			399,
			899,
			1999,
			2999,
			3999,
			4999,
			5999,
			6999,
			7999,
			8999,
			10499,
			11999,
			14999
		};

		// Token: 0x04001FCA RID: 8138
		private static Texture2D _avatar = CarrierGUI.I.avatar;

		// Token: 0x04001FCB RID: 8139
		private static bool _avatarDownloaded;
	}
}
