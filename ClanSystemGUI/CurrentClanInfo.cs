using System;
using System.Linq;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000114 RID: 276
	internal class CurrentClanInfo
	{
		// Token: 0x06000756 RID: 1878 RVA: 0x00042FA4 File Offset: 0x000411A4
		private void GenerateList()
		{
			if (this.selectedClanInfo == null)
			{
				return;
			}
			ClanSystemWindow.I.Lists.MemberList.Clear();
			for (int i = 0; i < this.selectedClanInfo.MemberList.Length; i++)
			{
				ClanSystemWindow.I.Lists.MemberList.Add(new PlayerInfoBar(this.selectedClanInfo.MemberList[i]));
			}
			ClanSystemWindow.I.Lists.MemberList.Sort();
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0004302C File Offset: 0x0004122C
		public void DrawLabelOverButton(float x, float y1, float y2, float width, float heigth, string upperStr, string lowerStr)
		{
			GUI.Label(new Rect(x, y1 + 0.5f, width, heigth), upperStr, ClanSystemWindow.I.Styles.styleBtnLabel);
			GUI.Label(new Rect(x - 2f, y2 + 0.5f, width, heigth), lowerStr, ClanSystemWindow.I.Styles.styleBtnLabel);
			GUI.Label(new Rect(x, y1, 80f, 20f), upperStr, ClanSystemWindow.I.Styles.styleBlackLabel);
			GUI.Label(new Rect(x - 2f, y2, 80f, 20f), lowerStr, ClanSystemWindow.I.Styles.styleBlackLabel);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000430E4 File Offset: 0x000412E4
		public void RefreshButton(float x, float y)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000430E8 File Offset: 0x000412E8
		public void OnGUI()
		{
			if (this.shortClanInfo != ClanSystemWindow.I.SelectedClan)
			{
				this.shortClanInfo = ClanSystemWindow.I.SelectedClan;
				this.selectedClanInfo = this.shortClanInfo.DetailInfo;
				this.GenerateList();
			}
			ClanSystemWindow.I.Lists.MemberList.OnGUI();
			if (this.selectedClanInfo.clanTagColor.Length < 6)
			{
				return;
			}
			MainGUI.Instance.TextField(new Rect(35f, 55f, 100f, 25f), this.selectedClanInfo.clanTag, 12, "#" + ((this.selectedClanInfo.clanTagColor.Length >= 6) ? (this.selectedClanInfo.clanTagColor + "_Micra") : "ffffff_Micra"), TextAnchor.MiddleLeft, false, false);
			GUI.Label(new Rect(35f + MainGUI.Instance.CalcWidth(this.selectedClanInfo.clanTag, MainGUI.Instance.fontMicra, 12), 56f, 300f, 25f), this.selectedClanInfo.clanName, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.DrawTexture(new Rect(25f, 110f, 230f, 73f), ClanSystemWindow.I.Textures.clanLeadBG);
			GUI.DrawTexture(new Rect(265f, 115f, 25f, 12f), ClanSystemWindow.I.Textures.exp);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 16;
			ClanSystemWindow.I.Styles.styleGrayLabel14Left.fontSize = 16;
			GUI.Label(new Rect(292f, 113f, 150f, 18f), Helpers.SeparateNumericString(this.selectedClanInfo.clanExp.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel14);
			GUI.Label(new Rect(287f + MainGUI.Instance.CalcWidth(Helpers.SeparateNumericString(this.selectedClanInfo.clanExp.ToString("F0")), MainGUI.Instance.fontDNC57, 16), 113f, 150f, 18f), "/" + Helpers.SeparateNumericString(this.selectedClanInfo.nextLevelExp.ToString("F0")), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 14;
			ClanSystemWindow.I.Styles.styleGrayLabel14Left.fontSize = 14;
			GUI.DrawTexture(new Rect(25f, 195f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			if (this.refreshTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 197f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.refreshTimer = Time.realtimeSinceStartup + 5f;
				Main.AddDatabaseRequestCallBack<ListClan>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[0]);
			}
			GUI.DrawTexture(new Rect(658f, 199f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			if (this.selectedClanInfo.leaderID == Main.UserInfo.userID && ClanSystemWindow.I.controller.SelectedTab == ClanSystemWindow.I.controller.TabManagment)
			{
				GUI.Label(new Rect(5f, 195f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(50f, 195f, 100f, 25f), Language.CarrLVL, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(100f, 195f, 100f, 25f), Language.CarrItemName, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(200f, 195f, 100f, 25f), Language.ClansTableContribution, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(275f, 195f, 100f, 25f), Language.CarrKills, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(368f, 195f, 100f, 25f), Language.ClansTableDiff, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(460f, 195f, 100f, 25f), Language.CarrReputation, ClanSystemWindow.I.Styles.ListHeaderLabel);
			}
			else
			{
				GUI.Label(new Rect(5f, 195f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(50f, 195f, 100f, 25f), Language.CarrLVL, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(100f, 195f, 100f, 25f), Language.CarrItemName, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(240f, 195f, 100f, 25f), Language.ClansTableContribution, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(315f, 195f, 100f, 25f), Language.CarrKills, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(425f, 195f, 100f, 25f), Language.ClansTableDiff, ClanSystemWindow.I.Styles.ListHeaderLabel);
				GUI.Label(new Rect(560f, 195f, 100f, 25f), Language.CarrReputation, ClanSystemWindow.I.Styles.ListHeaderLabel);
			}
			int currentMemberCount = this.selectedClanInfo.currentMemberCount;
			GUI.Label(new Rect(368f, 128f, 50f, 25f), currentMemberCount.ToString("F0"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 16;
			GUI.Label(new Rect(368f + MainGUI.Instance.CalcWidth(currentMemberCount.ToString("F0"), MainGUI.Instance.fontDNC57, 12), 128f, 50f, 25f), "/" + this.selectedClanInfo.maxMemberCount.ToString("F0"), ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 18;
			GUI.Label(new Rect(368f, 164f, 50f, 25f), this.selectedClanInfo.raceWins.ToString("0"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			float num = Math.Max(2f, Mathf.Floor((float)(currentMemberCount / 10)));
			string text = this.selectedClanInfo.MemberList.Count((ClanMemberInfo info) => info.role == 2).ToString("0") + " /" + num.ToString("0");
			GUI.Label(new Rect(368f, 146f, 50f, 25f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(27f, 112f, 50f, 50f), ClanSystemWindow.I.Textures.avatar);
			GUI.DrawTexture(new Rect(85f, 115f, 29f, 38f), MainGUI.Instance.rank_icon[this.selectedClanInfo.leaderLevel]);
			if (this.selectedClanInfo.leaderClass != 0)
			{
				GUI.DrawTexture(new Rect(110f, 110f, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[this.selectedClanInfo.leaderClass - 1]);
				MainGUI.Instance.TextField(new Rect(135f, 115f, 200f, 20f), this.selectedClanInfo.leaderNick, 16, this.selectedClanInfo.leaderNickColor, TextAnchor.MiddleLeft, false, false);
			}
			else
			{
				MainGUI.Instance.TextField(new Rect(118f, 115f, 200f, 20f), this.shortClanInfo.leaderNick, 16, this.selectedClanInfo.leaderNickColor, TextAnchor.MiddleLeft, false, false);
			}
			GUI.Label(new Rect(118f, 133f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.selectedClanInfo.leaderFName, this.selectedClanInfo.leaderLName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			if (GUI.Button(ClanSystemWindow.I.Tabs.infoBtn, string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
			{
				CarrierGUI.I.LoadInfo(this.selectedClanInfo.leaderID);
			}
			GUI.DrawTexture(new Rect(175f, 168f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
			GUI.Label(new Rect(200f, 162f, 50f, 25f), ((this.selectedClanInfo.leaderDeath != 0) ? ((float)this.selectedClanInfo.leaderKills / (float)this.selectedClanInfo.leaderDeath) : ((float)this.selectedClanInfo.leaderKills)).ToString("F2"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(290f, 55f, 47f, 25f), ClanSystemWindow.I.Textures.yellowBack);
			GUI.Label(new Rect(290f, 55f, 47f, 25f), this.selectedClanInfo.clanLevel.ToString("F0"), ClanSystemWindow.I.Styles.styleLevelLabel);
			GUI.Label(new Rect(290f, 65f, 47f, 10f), Language.Level.ToLower(), ClanSystemWindow.I.Styles.styleLevel);
			GUI.DrawTexture(new Rect(340f, 55f, 47f, 25f), ClanSystemWindow.I.Textures.blueBack);
			GUI.Label(new Rect(340f, 55f, 47f, 25f), (this.selectedClanInfo.place + 1).ToString("F0"), ClanSystemWindow.I.Styles.stylePositionLabel);
			GUI.Label(new Rect(340f, 65f, 47f, 10f), Language.CarrPlace.ToLower(), ClanSystemWindow.I.Styles.stylePosition);
			GUI.Label(new Rect(25f, 85f, 100f, 25f), Language.ClansLead, ClanSystemWindow.I.Styles.styleGrayLabel);
			if (this.selectedClanInfo.leaderID == Main.UserInfo.userID)
			{
				GUI.Label(new Rect(95f, 85f, 100f, 25f), Language.ClansLeadYou, ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			if (ClanSystemWindow.I.controller.SelectedTab == ClanSystemWindow.I.controller.TabInfo)
			{
				GUI.Label(new Rect(265f, 85f, 100f, 25f), Language.ClansStats, ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			else
			{
				GUI.Label(new Rect(265f, 85f, 200f, 25f), Language.ClansYourStats, ClanSystemWindow.I.Styles.styleGrayLabel);
			}
			if (ClanSystemWindow.I.controller.SelectedTab != ClanSystemWindow.I.controller.TabInfo)
			{
				GUI.Label(new Rect(485f, 85f, 200f, 25f), Language.ClansYourContribution, ClanSystemWindow.I.Styles.styleGrayLabel);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
				GUI.Label(new Rect(597f, 86f, 50f, 25f), (Main.UserInfo.ClanEarnProc * 100f).ToString("F0") + "%", ClanSystemWindow.I.Styles.styleWhiteLabel16);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
				GUI.DrawTexture(new Rect(653f, 92f, 25f, 12f), ClanSystemWindow.I.Textures.exp);
				this.expPreviousState = Main.UserInfo.ClanEarnProc;
				if (Main.IsGameLoaded)
				{
					bool enabled = GUI.enabled;
					GUI.enabled = false;
					Main.UserInfo.ClanEarnProc = MainGUI.Instance.FloatSlider0dot00(new Vector2(488f, 115f), Main.UserInfo.ClanEarnProc, 0f, 1f, false);
					GUI.enabled = enabled;
				}
				else
				{
					Main.UserInfo.ClanEarnProc = MainGUI.Instance.FloatSlider0dot00(new Vector2(488f, 115f), Main.UserInfo.ClanEarnProc, 0f, 1f, false);
				}
				if (Main.UserInfo.ClanEarnProc != this.expPreviousState)
				{
					this.timer.Start();
				}
				if (this.timer.Elapsed > 2f)
				{
					this.timer.Stop();
					Main.AddDatabaseRequestCallBack<SaveRatio>(delegate
					{
					}, delegate
					{
					}, new object[]
					{
						Main.UserInfo.ClanEarnProc.ToString("F2")
					});
					Main.UserInfo.CurrentProc = Main.UserInfo.ClanEarnProc;
				}
			}
			GUI.Label(new Rect(265f, 128f, 100f, 25f), Language.ClansSize, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(265f, 164f, 100f, 25f), Language.ClansVictory, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(265f, 146f, 100f, 25f), Language.Officers, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			if (Main.UserInfo.clanID == 0 && !this.shortClanInfo.didRequest)
			{
				if (CWGUI.Button(new Rect(520f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					if (CVars.IsStandaloneRealm)
					{
						Application.OpenURL(this.selectedClanInfo.clanURL);
					}
					else
					{
						Application.ExternalCall("window.open", new object[]
						{
							this.selectedClanInfo.clanURL
						});
					}
				}
				this.DrawLabelOverButton(520f, 152f, 164f, 80f, 20f, Language.ClansHeadquarters1, Language.ClansHeadquarters2);
				if (GUI.Button(new Rect(600f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					Main.AddDatabaseRequestCallBack<SendRequest>(delegate
					{
						this.shortClanInfo.didRequest = true;
					}, delegate
					{
					}, new object[]
					{
						this.shortClanInfo.clanID
					});
				}
				this.DrawLabelOverButton(600f, 152f, 164f, 80f, 20f, Language.ClansRequest1, Language.ClansRequest2);
			}
			else if (Main.UserInfo.clanID == 0)
			{
				if (GUI.Button(new Rect(600f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					Main.AddDatabaseRequestCallBack<RevokeRequest>(delegate
					{
						this.shortClanInfo.didRequest = false;
					}, delegate
					{
					}, new object[]
					{
						this.shortClanInfo.clanID
					});
				}
				this.DrawLabelOverButton(600f, 152f, 164f, 80f, 20f, Language.ClansWithdraw, Language.ClansRequest2);
			}
			else if (Main.UserInfo.clanID != 0)
			{
				if (CWGUI.Button(new Rect(600f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					if (CVars.IsStandaloneRealm)
					{
						Application.OpenURL(this.selectedClanInfo.clanURL);
					}
					else
					{
						Application.ExternalCall("window.open", new object[]
						{
							this.selectedClanInfo.clanURL
						});
					}
				}
				this.DrawLabelOverButton(600f, 152f, 164f, 80f, 20f, Language.ClansHeadquarters1, Language.ClansHeadquarters2);
			}
			MainGUI.Instance.VoteWidget(new Vector2(3f, 162f), this.selectedClanInfo.leaderID, this.selectedClanInfo.leaderReputation, -1);
			if (GUI.Button(new Rect(104f, 164f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.selectedClanInfo.leaderID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
			{
				object[] args = new object[]
				{
					this.selectedClanInfo.leaderID,
					this.selectedClanInfo.leaderLevel,
					this.selectedClanInfo.leaderClass,
					this.selectedClanInfo.clanTag,
					this.selectedClanInfo.leaderNick,
					this.selectedClanInfo.leaderFName,
					this.selectedClanInfo.leaderLName
				};
				WatchlistManager.AddRemovePlayer(args);
			}
			if (Main.UserInfo.clanID == 0)
			{
				GUI.DrawTexture(new Rect(25f, 460f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
				GUI.Label(new Rect(300f, 460f, 100f, 25f), Language.ClansYourWarrior, ClanSystemWindow.I.Styles.styleGrayLabel14);
				this.infoBar.DrawBar(0f, 480f, 0, true);
			}
			else
			{
				ClanSystemWindow.I.Lists.MemberList.OverRect.height = 300f;
			}
		}

		// Token: 0x04000814 RID: 2068
		private ShortClanInfo shortClanInfo = new ShortClanInfo();

		// Token: 0x04000815 RID: 2069
		private DetailClanInfo selectedClanInfo = new DetailClanInfo();

		// Token: 0x04000816 RID: 2070
		private eTimer timer = new eTimer();

		// Token: 0x04000817 RID: 2071
		private bool _listRequest;

		// Token: 0x04000818 RID: 2072
		private float expPreviousState;

		// Token: 0x04000819 RID: 2073
		private float minWidth;

		// Token: 0x0400081A RID: 2074
		private float maxWidth;

		// Token: 0x0400081B RID: 2075
		private float refreshTimer;

		// Token: 0x0400081C RID: 2076
		private PlayerInfoBar infoBar = new PlayerInfoBar();
	}
}
