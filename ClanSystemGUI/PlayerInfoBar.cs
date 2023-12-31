using System;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000115 RID: 277
	internal class PlayerInfoBar : IScrollListItem, IComparable
	{
		// Token: 0x06000762 RID: 1890 RVA: 0x000445F0 File Offset: 0x000427F0
		public PlayerInfoBar()
		{
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00044604 File Offset: 0x00042804
		public PlayerInfoBar(ClanMemberInfo info)
		{
			this.info = info;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00044624 File Offset: 0x00042824
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0004463C File Offset: 0x0004283C
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00044654 File Offset: 0x00042854
		public int CompareTo(object obj)
		{
			if (this.info.earnExp > ((PlayerInfoBar)obj).info.earnExp)
			{
				return -1;
			}
			if (this.info.earnExp < ((PlayerInfoBar)obj).info.earnExp)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000446A8 File Offset: 0x000428A8
		public void DrawBar(float x = 0f, float y = 0f, int index = 0, bool yourStats = false)
		{
			if (!yourStats)
			{
				if (this.info.memberClass != 0f)
				{
					GUI.DrawTexture(new Rect(x + 110f, y, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[(int)this.info.memberClass - 1]);
					MainGUI.Instance.TextField(new Rect(x + 135f, y + 6f, 200f, 20f), this.info.memberNick, 16, this.info.memberNickColor, TextAnchor.MiddleLeft, false, false);
				}
				else
				{
					MainGUI.Instance.TextField(new Rect(x + 118f, y + 6f, 200f, 20f), this.info.memberNick, 16, this.info.memberNickColor, TextAnchor.MiddleLeft, false, false);
				}
				GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.statsBack);
				ClanSystemWindow.I.Styles.styleGrayLabel.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(x + 30f, y + 10f, 50f, 20f), (index + 1).ToString(), ClanSystemWindow.I.Styles.styleGrayLabel);
				ClanSystemWindow.I.Styles.styleGrayLabel.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(x + 30f, y + 21f, 50f, 20f), this.info.Role, ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				if (CWGUI.Button(new Rect(x + 618f, y + 14f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
				{
					CarrierGUI.I.LoadInfo(this.info.memberUserID);
				}
				GUI.DrawTexture(new Rect(x + 85f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[this.info.Level]);
				GUI.DrawTexture(new Rect(x + 245f, y + 10f, 25f, 12f), ClanSystemWindow.I.Textures.exp);
				GUI.Label(new Rect(x + 275f, y + 6f, 100f, 20f), Helpers.SeparateNumericString(this.info.earnExp.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				if (this.info.memberUserID == Main.UserInfo.userID)
				{
					GUI.Label(new Rect(x + 275f, y + 23f, 50f, 20f), (Main.UserInfo.CurrentProc * 100f).ToString("F0") + "%", ClanSystemWindow.I.Styles.styleGrayLabel14Left);
				}
				else
				{
					GUI.Label(new Rect(x + 275f, y + 23f, 50f, 20f), this.info.earnProc * 100f + "%", ClanSystemWindow.I.Styles.styleGrayLabel14Left);
				}
				GUI.DrawTexture(new Rect(x + 345f, y + 25f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
				GUI.DrawTexture(new Rect(x + 430f, y + 5f, 33f, 28f), ClanSystemWindow.I.Textures.statsWtask);
				GUI.DrawTexture(new Rect(x + 460f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsAchievement);
				GUI.DrawTexture(new Rect(x + 490f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsProKill);
				GUI.Label(new Rect(x + 118f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.info.memberFName, this.info.memberLName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(x + 340f, y + 5f, 50f, 20f), this.info.killCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(x + 355f, y + 25f, 50f, 12f), ((this.info.deathCount != 0) ? ((float)this.info.killCount / (float)this.info.deathCount) : ((float)this.info.killCount)).ToString("F2"), ClanSystemWindow.I.Styles.styleGrayLabel14);
				GUI.Label(new Rect(x + 430f, y + 28f, (float)ClanSystemWindow.I.Textures.statsWtask.width, 20f), this.info.contracts.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				GUI.Label(new Rect(x + 460f, y + 28f, (float)ClanSystemWindow.I.Textures.statsAchievement.width, 20f), this.info.achievements.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				GUI.Label(new Rect(x + 490f, y + 28f, (float)ClanSystemWindow.I.Textures.statsProKill.width, 20f), this.info.proKills.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				MainGUI.Instance.VoteWidget(new Vector2(x + 538f, y + 12f), this.info.memberUserID, this.info.reputation, -1);
				if (GUI.Button(new Rect(x + 638f, y + 14f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.info.memberUserID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
				{
					object[] args = new object[]
					{
						this.info.memberUserID,
						this.info.Level,
						(int)this.info.memberClass,
						string.Empty,
						this.info.memberNick,
						this.info.memberFName,
						this.info.memberLName
					};
					WatchlistManager.AddRemovePlayer(args);
				}
			}
			else
			{
				if (this.info.memberClass != 0f)
				{
					GUI.DrawTexture(new Rect(x + 110f, y, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[Main.UserInfo.playerClass - PlayerClass.storm_trooper]);
					MainGUI.Instance.TextField(new Rect(x + 135f, y + 6f, 200f, 20f), Main.UserInfo.nick, 16, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
				}
				else
				{
					MainGUI.Instance.TextField(new Rect(x + 118f, y + 6f, 200f, 20f), Main.UserInfo.nick, 16, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
				}
				GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.yourStatsBack);
				GUI.Label(new Rect(x + 250f, y + 15f, 50f, 20f), "---", ClanSystemWindow.I.Styles.styleGrayLabel);
				GUI.DrawTexture(new Rect(x + 85f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[Main.UserInfo.currentLevel]);
				GUI.DrawTexture(new Rect(x + 335f, y + 25f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
				GUI.DrawTexture(new Rect(x + 430f, y + 5f, 33f, 28f), ClanSystemWindow.I.Textures.statsWtask);
				GUI.DrawTexture(new Rect(x + 460f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsAchievement);
				GUI.DrawTexture(new Rect(x + 490f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsProKill);
				GUI.Label(new Rect(x + 50f, y + 15f, 50f, 20f), (index + 1).ToString(), ClanSystemWindow.I.Styles.styleGrayLabel);
				GUI.Label(new Rect(x + 118f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(Main.UserInfo.socialInfo.firstName, Main.UserInfo.socialInfo.lastName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(x + 330f, y + 5f, 50f, 20f), Main.UserInfo.killCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
				GUI.Label(new Rect(x + 345f, y + 25f, 50f, 12f), ((Main.UserInfo.deathCount.Value != 0) ? ((float)Main.UserInfo.killCount.Value / (float)Main.UserInfo.deathCount.Value) : ((float)Main.UserInfo.killCount.Value)).ToString("F2"), ClanSystemWindow.I.Styles.styleGrayLabel14);
				GUI.Label(new Rect(x + 430f, y + 28f, (float)ClanSystemWindow.I.Textures.statsWtask.width, 20f), Main.UserInfo.contractsCount.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				GUI.Label(new Rect(x + 460f, y + 28f, (float)ClanSystemWindow.I.Textures.statsAchievement.width, 20f), AchievementsEngine.OpenedCount(Main.UserInfo).ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				GUI.Label(new Rect(x + 490f, y + 28f, (float)ClanSystemWindow.I.Textures.statsProKill.width, 20f), Main.UserInfo.userStats.proKills.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
				GUI.Label(new Rect(x + 567f, y + 15f, 50f, 20f), Main.UserInfo.repa.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
			}
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00045368 File Offset: 0x00043568
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index, false);
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00045374 File Offset: 0x00043574
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0004537C File Offset: 0x0004357C
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x04000823 RID: 2083
		public static int position;

		// Token: 0x04000824 RID: 2084
		private ClanMemberInfo info = new ClanMemberInfo();
	}
}
