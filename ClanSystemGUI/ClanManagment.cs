using System;
using System.Linq;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010E RID: 270
	internal class ClanManagment : AbstractClanPage
	{
		// Token: 0x06000721 RID: 1825 RVA: 0x0003FA50 File Offset: 0x0003DC50
		private void GenerateListForLead()
		{
			ClanSystemWindow.I.Lists.LeadMemberList.Clear();
			for (int i = 0; i < this.yourClan.RequestList.Length; i++)
			{
				((LeadMemberList)ClanSystemWindow.I.Lists.LeadMemberList).AddRequests(new RequestInfoBar((LeadMemberList)ClanSystemWindow.I.Lists.LeadMemberList, this.yourClan.RequestList[i]));
			}
			for (int j = 0; j < this.yourClan.MemberList.Length; j++)
			{
				ClanSystemWindow.I.Lists.LeadMemberList.Add(new CurrentInfoBar(ClanSystemWindow.I.Lists.LeadMemberList, this.yourClan.MemberList[j]));
			}
			((LeadMemberList)ClanSystemWindow.I.Lists.LeadMemberList).RefreshTitles();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0003FB3C File Offset: 0x0003DD3C
		private void GenereteListForMember()
		{
			ClanSystemWindow.I.Lists.LeadMemberList.Clear();
			for (int i = 0; i < this.yourClan.MemberList.Length; i++)
			{
				ClanSystemWindow.I.Lists.LeadMemberList.Add(new PlayerInfoBar(this.yourClan.MemberList[i]));
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		public void DrawLabelOverButton(float x, float y1, float y2, float width, float heigth, string upperStr, string lowerStr)
		{
			GUI.Label(new Rect(x, y1 + 0.5f, width, heigth), upperStr, ClanSystemWindow.I.Styles.styleBtnLabel);
			GUI.Label(new Rect(x - 2f, y2 + 0.5f, width, heigth), lowerStr, ClanSystemWindow.I.Styles.styleBtnLabel);
			GUI.Label(new Rect(x, y1, 80f, 20f), upperStr, ClanSystemWindow.I.Styles.styleBlackLabel);
			GUI.Label(new Rect(x - 2f, y2, 80f, 20f), lowerStr, ClanSystemWindow.I.Styles.styleBlackLabel);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0003FC5C File Offset: 0x0003DE5C
		public void DrawInformation()
		{
			MainGUI.Instance.TextLabel(new Rect(35f, 55f, 100f, 25f), this.yourClan.clanTag, 12, "#" + ((this.yourClan.clanTagColor.Length >= 6) ? (this.yourClan.clanTagColor + "_Micra") : "ffffff_Micra"), TextAnchor.MiddleLeft, true);
			GUI.Label(new Rect(35f + MainGUI.Instance.CalcWidth(this.yourClan.clanTag, MainGUI.Instance.fontMicra, 12), 56f, 300f, 25f), this.yourClan.clanName, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			if (Main.UserInfo.IsClanLeader && GUI.Button(new Rect(255f, 56f, 34f, 24f), string.Empty, ClanSystemWindow.I.Styles.ClanEditBtn))
			{
				object[] args = new object[]
				{
					this.yourClan.clanURL,
					this.yourClan.clanTagColor,
					this.yourClan.clanGP
				};
				EventFactory.Call("ShowPopup", new Popup(WindowsID.EditClanInfo, Language.ClansEditPopupHeader, string.Empty, delegate()
				{
				}, PopupState.editClanInfo, false, true, args));
			}
			GUI.DrawTexture(new Rect(25f, 110f, 230f, 73f), ClanSystemWindow.I.Textures.clanLeadBG);
			GUI.DrawTexture(new Rect(265f, 115f, 25f, 12f), ClanSystemWindow.I.Textures.exp);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 16;
			ClanSystemWindow.I.Styles.styleGrayLabel14Left.fontSize = 16;
			GUI.Label(new Rect(292f, 113f, 150f, 18f), Helpers.SeparateNumericString(this.yourClan.clanExp.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel14);
			GUI.Label(new Rect(287f + MainGUI.Instance.CalcWidth(Helpers.SeparateNumericString(this.yourClan.clanExp.ToString("0")), MainGUI.Instance.fontDNC57, 16), 113f, 150f, 18f), "/" + Helpers.SeparateNumericString(this.yourClan.nextLevelExp.ToString("F0")), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.fontSize = 14;
			ClanSystemWindow.I.Styles.styleGrayLabel14Left.fontSize = 14;
			this._creepingLine.OnGUI(new Rect(25f, 191f, 658f, 30f), Main.UserInfo.ClanRole > Role.officer);
			GUI.DrawTexture(new Rect(25f, 225f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			if (ClanSystemWindow.I.controller.SelectedTab == ClanSystemWindow.I.controller.TabManagment)
			{
				if (Main.UserInfo.ClanRole > Role.contractor)
				{
					GUI.Label(new Rect(5f, 225f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(50f, 225f, 100f, 25f), Language.CarrLVL, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(80f, 225f, 100f, 25f), Language.CarrItemName, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(200f, 225f, 100f, 25f), Language.ClansTableContribution, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(275f, 225f, 100f, 25f), Language.CarrKills, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(348f, 225f, 100f, 25f), Language.ClansTableDiff, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(440f, 225f, 100f, 25f), Language.CarrReputation, ClanSystemWindow.I.Styles.ListHeaderLabel);
				}
				else
				{
					GUI.Label(new Rect(5f, 225f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(50f, 225f, 100f, 25f), Language.CarrLVL, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(100f, 225f, 100f, 25f), Language.CarrItemName, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(240f, 225f, 100f, 25f), Language.ClansTableContribution, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(315f, 225f, 100f, 25f), Language.CarrKills, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(425f, 225f, 100f, 25f), Language.ClansTableDiff, ClanSystemWindow.I.Styles.ListHeaderLabel);
					GUI.Label(new Rect(560f, 225f, 100f, 25f), Language.CarrReputation, ClanSystemWindow.I.Styles.ListHeaderLabel);
				}
			}
			if (ClanSystemWindow.I.controller.SelectedTab != ClanSystemWindow.I.controller.TabInfo)
			{
				if (this._reupdateTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 227f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
				{
					this._reupdateTimer = Time.realtimeSinceStartup + 5f;
					this.ReloadInfo(true);
				}
				if (ClanManagment.UpdateByChangeRole)
				{
					ClanManagment.UpdateByChangeRole = false;
					this.ReloadInfo(true);
				}
				GUI.DrawTexture(new Rect(658f, 229f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			}
			int currentMemberCount = this.yourClan.currentMemberCount;
			int maxMemberCount = this.yourClan.maxMemberCount;
			GUI.Label(new Rect(368f, 128f, 50f, 25f), currentMemberCount.ToString("F0"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 16;
			GUI.Label(new Rect(368f + MainGUI.Instance.CalcWidth(currentMemberCount.ToString("F0"), MainGUI.Instance.fontDNC57, 12), 128f, 50f, 25f), "/" + maxMemberCount.ToString("F0"), ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Styles.styleGrayLabel.fontSize = 18;
			GUI.Label(new Rect(368f, 164f, 50f, 25f), this.yourClan.raceWins.ToString("0"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			float num = Math.Max(2f, Mathf.Floor((float)(currentMemberCount / 10)));
			string text = this.yourClan.MemberList.Count((ClanMemberInfo info) => info.role == 2).ToString("0") + " /" + num.ToString("0");
			GUI.Label(new Rect(368f, 146f, 50f, 25f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(27f, 112f, 50f, 50f), ClanSystemWindow.I.Textures.avatar);
			GUI.DrawTexture(new Rect(85f, 115f, 29f, 38f), MainGUI.Instance.rank_icon[this.yourClan.leaderLevel]);
			if (this.yourClan.leaderClass != 0)
			{
				GUI.DrawTexture(new Rect(110f, 110f, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[this.yourClan.leaderClass - 1]);
				MainGUI.Instance.TextField(new Rect(135f, 115f, 200f, 20f), this.yourClan.leaderNick, 16, this.yourClan.leaderNickColor, TextAnchor.MiddleLeft, false, false);
			}
			else
			{
				MainGUI.Instance.TextField(new Rect(118f, 115f, 200f, 20f), this.yourClan.leaderNick, 16, this.yourClan.leaderNickColor, TextAnchor.MiddleLeft, false, false);
			}
			GUI.Label(new Rect(118f, 133f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.yourClan.leaderFName, this.yourClan.leaderLName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			if (this.yourClan.leaderID != Main.UserInfo.userID)
			{
				MainGUI.Instance.VoteWidget(new Vector2(43f, 162f), this.yourClan.leaderID, this.yourClan.leaderReputation, -1);
				if (GUI.Button(new Rect(124f, 164f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
				{
					CarrierGUI.I.LoadInfo(this.yourClan.leaderID);
				}
				if (GUI.Button(new Rect(144f, 164f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.yourClan.leaderID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
				{
					object[] args2 = new object[]
					{
						this.yourClan.leaderID,
						this.yourClan.leaderLevel,
						this.yourClan.leaderClass,
						this.yourClan.clanTag,
						this.yourClan.leaderNick,
						this.yourClan.leaderFName,
						this.yourClan.leaderLName
					};
					WatchlistManager.AddRemovePlayer(args2);
				}
			}
			GUI.DrawTexture(new Rect(175f, 168f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
			GUI.Label(new Rect(200f, 162f, 50f, 25f), ((this.yourClan.leaderDeath != 0) ? ((float)this.yourClan.leaderKills / (float)this.yourClan.leaderDeath) : ((float)this.yourClan.leaderKills)).ToString("F2"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.DrawTexture(new Rect(290f, 55f, 47f, 25f), ClanSystemWindow.I.Textures.yellowBack);
			GUI.Label(new Rect(290f, 55f, 47f, 25f), this.yourClan.clanLevel.ToString("F0"), ClanSystemWindow.I.Styles.styleLevelLabel);
			GUI.Label(new Rect(290f, 65f, 47f, 10f), Language.Level.ToLower(), ClanSystemWindow.I.Styles.styleLevel);
			GUI.DrawTexture(new Rect(340f, 55f, 47f, 25f), ClanSystemWindow.I.Textures.blueBack);
			string text2 = (this.yourClan.place + 1).ToString("F0");
			int fontSize = ClanSystemWindow.I.Styles.stylePositionLabel.fontSize;
			if (text2.Length == 5)
			{
				ClanSystemWindow.I.Styles.stylePositionLabel.fontSize = (int)((float)fontSize / 1.5f);
			}
			else if (text2.Length > 5)
			{
				ClanSystemWindow.I.Styles.stylePositionLabel.fontSize = fontSize / 2;
			}
			GUI.Label(new Rect(340f, 55f, 47f, 25f), text2, ClanSystemWindow.I.Styles.stylePositionLabel);
			ClanSystemWindow.I.Styles.stylePositionLabel.fontSize = fontSize;
			GUI.Label(new Rect(340f, 65f, 47f, 10f), Language.CarrPlace.ToLower(), ClanSystemWindow.I.Styles.stylePosition);
			GUI.Label(new Rect(25f, 85f, 100f, 25f), Language.ClansLead, ClanSystemWindow.I.Styles.styleGrayLabel);
			if (this.yourClan.leaderID == Main.UserInfo.userID)
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
				if (Main.IsGameLoaded || Main.UserInfo.currentLevel < 10)
				{
					bool enabled = GUI.enabled;
					GUI.enabled = false;
					Main.UserInfo.ClanEarnProc = MainGUI.Instance.FloatSlider0dot00(new Vector2(488f, 115f), Main.UserInfo.ClanEarnProc, 0.1f, 0.75f, false);
					GUI.enabled = enabled;
					Rect rect = new Rect(483f, 114f, 195f, 13f);
					if (rect.Contains(Event.current.mousePosition))
					{
						this.ShowExpSliderHint();
					}
				}
				else
				{
					Main.UserInfo.ClanEarnProc = MainGUI.Instance.FloatSlider0dot00(new Vector2(488f, 115f), Main.UserInfo.ClanEarnProc, 0.1f, 0.75f, false);
				}
				GUI.Label(new Rect(430f, 125f, 300f, 10f), Language.ClansYourContributionHint, ClanSystemWindow.I.Styles.stylePosition);
				if (Main.UserInfo.ClanEarnProc != this.expPreviousState)
				{
					this.expTimer.Start();
				}
				if (this.expTimer.Elapsed > 2f)
				{
					this.expTimer.Stop();
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
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00040F5C File Offset: 0x0003F15C
		private void ShowExpSliderHint()
		{
			Rect position = new Rect(500f, 95f, 170f, 22f);
			GUI.DrawTexture(position, MainGUI.Instance.black);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.alignment = TextAnchor.MiddleCenter;
			GUI.Label(position, (!Main.IsGameLoaded) ? Language.ClansExpSliderHint : Language.ClansExpSliderInGameHint, ClanSystemWindow.I.Styles.styleWhiteLabel14);
			ClanSystemWindow.I.Styles.styleWhiteLabel14.alignment = TextAnchor.MiddleLeft;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00040FEC File Offset: 0x0003F1EC
		public override void OnStart()
		{
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00040FF0 File Offset: 0x0003F1F0
		public override void OnGUI()
		{
			if (this._shortClanInfo == null)
			{
				this._shortClanInfo = new ShortClanInfo();
				this._shortClanInfo.clanID = Main.UserInfo.clanID;
				this.ReloadInfo(false);
			}
			if (this.yourClan.clanTagColor.Length < 6)
			{
				return;
			}
			this.DrawInformation();
			GUI.DrawTexture(new Rect(473f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			if (Main.UserInfo.ClanRole > Role.contractor)
			{
				if (Main.UserInfo.ClanRole != Role.lt)
				{
					if (CWGUI.Button(new Rect(440f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
					{
						if (CVars.IsStandaloneRealm)
						{
							Application.OpenURL(this.yourClan.clanURL);
						}
						else
						{
							Application.ExternalCall("window.open", new object[]
							{
								this.yourClan.clanURL
							});
						}
					}
					this.DrawLabelOverButton(440f, 152f, 164f, 80f, 20f, Language.ClansHeadquarters1, Language.ClansHeadquarters2);
				}
				if (CWGUI.Button(new Rect(520f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupDiscard, Main.UserInfo.clanID.ToString(), delegate()
					{
						((LeadMemberList)ClanSystemWindow.I.Lists.LeadMemberList).ClearRequests();
					}, PopupState.discardAllRequest, false, true, new object[0]));
				}
				this.DrawLabelOverButton(520f, 152f, 164f, 80f, 20f, Language.ClansManagmentDiscard1, Language.ClansManagmentDiscard2);
				if (Main.UserInfo.ClanRole > Role.officer)
				{
					if (CWGUI.Button(new Rect(600f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupExtend, Main.UserInfo.clanID.ToString(), delegate()
						{
							this.ReloadInfo(true);
							ClanSkills.requestSended = false;
						}, PopupState.extendClan, false, true, new object[]
						{
							this.yourClan.clanGP
						}));
					}
					this.DrawLabelOverButton(600f, 152f, 164f, 80f, 20f, Language.ClansManagmentExtend, Language.ClansName);
				}
				ClanSystemWindow.I.Lists.LeadMemberList.OnGUI();
			}
			if (Main.UserInfo.ClanRole < Role.leader)
			{
				if (CWGUI.Button(new Rect((float)((Main.UserInfo.ClanRole != Role.lt) ? 600 : 440), 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupLeave, Main.UserInfo.clanID.ToString(), delegate()
					{
						CarrierGUI.I.Hide(0.35f);
						CarrierGUI.I.SetCarrerState(CarrierState.OVERVIEW);
					}, PopupState.leaveClan, false, true, new object[0]));
				}
				this.DrawLabelOverButton((float)((Main.UserInfo.ClanRole != Role.lt) ? 600 : 440), 152f, 164f, 80f, 20f, Language.ClansManagmentLeave, Language.ClansName);
				ClanSystemWindow.I.Lists.LeadMemberList.OnGUI();
			}
			else if (Main.UserInfo.ClanRole < Role.officer)
			{
				if (CWGUI.Button(new Rect(520f, 150f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
				{
					if (CVars.IsStandaloneRealm)
					{
						Application.OpenURL(this.yourClan.clanURL);
					}
					else
					{
						Application.ExternalCall("window.open", new object[]
						{
							this.yourClan.clanURL
						});
					}
				}
				this.DrawLabelOverButton(520f, 152f, 164f, 80f, 20f, Language.ClansHeadquarters1, Language.ClansHeadquarters2);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0004146C File Offset: 0x0003F66C
		private void ReloadInfo(bool force = false)
		{
			this._shortClanInfo.ReloadAdditionInfo(delegate
			{
				this.yourClan = this._shortClanInfo.DetailInfo;
				if (this.yourClan.leaderID == Main.UserInfo.userID || Main.UserInfo.ClanRole == Role.lt || Main.UserInfo.ClanRole == Role.officer)
				{
					this.GenerateListForLead();
				}
				else
				{
					this.GenereteListForMember();
				}
			}, delegate
			{
			}, force);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x000414A4 File Offset: 0x0003F6A4
		public override void OnUpdate()
		{
		}

		// Token: 0x040007F3 RID: 2035
		private ShortClanInfo _shortClanInfo;

		// Token: 0x040007F4 RID: 2036
		private DetailClanInfo yourClan = new DetailClanInfo();

		// Token: 0x040007F5 RID: 2037
		private eTimer expTimer = new eTimer();

		// Token: 0x040007F6 RID: 2038
		private float expPreviousState;

		// Token: 0x040007F7 RID: 2039
		private float _reupdateTimer;

		// Token: 0x040007F8 RID: 2040
		public static bool UpdateByChangeRole;

		// Token: 0x040007F9 RID: 2041
		private readonly CreepingLine _creepingLine = new CreepingLine();
	}
}
