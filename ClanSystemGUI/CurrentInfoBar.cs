using System;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000117 RID: 279
	internal class CurrentInfoBar : IScrollListItem, IComparable
	{
		// Token: 0x0600077B RID: 1915 RVA: 0x00045B70 File Offset: 0x00043D70
		public CurrentInfoBar(ScrollList controller)
		{
			this.controller = controller;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00045B8C File Offset: 0x00043D8C
		public CurrentInfoBar(ScrollList controller, ClanMemberInfo info)
		{
			this.controller = controller;
			this._info = info;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00045BB0 File Offset: 0x00043DB0
		public CurrentInfoBar(ScrollList controller, ClanRequestInfo info)
		{
			this.controller = controller;
			this._info = new ClanMemberInfo(info);
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00045BE8 File Offset: 0x00043DE8
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00045C00 File Offset: 0x00043E00
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00045C18 File Offset: 0x00043E18
		public int CompareTo(object obj)
		{
			if (this._info.earnExp > ((CurrentInfoBar)obj)._info.earnExp)
			{
				return -1;
			}
			if (this._info.earnExp < ((CurrentInfoBar)obj)._info.earnExp)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00045C6C File Offset: 0x00043E6C
		public void DrawBar(float x = 0f, float y = 0f, int index = 0, bool yourStats = false)
		{
			if ((int)this._info.memberClass != 0 && (int)this._info.memberClass < ClanSystemWindow.I.Textures.statsClass.Length)
			{
				GUI.DrawTexture(new Rect(x + 110f, y, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[(int)this._info.memberClass - 1]);
				GUI.Label(new Rect(x + 135f, y + 6f, 200f, 20f), this._info.memberNick, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			}
			else
			{
				GUI.Label(new Rect(x + 118f, y + 6f, 200f, 20f), this._info.memberNick, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			}
			GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.statsBack);
			if (CWGUI.Button(new Rect(x + 513f, y + 14f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
			{
				CarrierGUI.I.LoadInfo(this._info.memberUserID);
			}
			if (!yourStats && GUI.Button(new Rect(x + 535f, y + 14f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this._info.memberUserID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
			{
				object[] args = new object[]
				{
					this._info.memberUserID,
					this._info.Level,
					(int)this._info.memberClass,
					string.Empty,
					this._info.memberNick,
					this._info.memberFName,
					this._info.memberLName
				};
				WatchlistManager.AddRemovePlayer(args);
			}
			GUI.DrawTexture(new Rect(x + 85f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[this._info.Level]);
			GUI.DrawTexture(new Rect(x + 210f, y + 10f, 25f, 12f), ClanSystemWindow.I.Textures.exp);
			GUI.Label(new Rect(x + 240f, y + 6f, 100f, 20f), this._info.earnExp.ToString("F0"), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			if (this._info.memberUserID == Main.UserInfo.userID)
			{
				GUI.Label(new Rect(x + 240f, y + 23f, 50f, 20f), (Main.UserInfo.CurrentProc * 100f).ToString("F0") + "%", ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			}
			else
			{
				GUI.Label(new Rect(x + 240f, y + 23f, 50f, 20f), this._info.earnProc * 100f + "%", ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			}
			GUI.DrawTexture(new Rect(x + 305f, y + 25f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
			GUI.DrawTexture(new Rect(x + 355f, y + 5f, 33f, 28f), ClanSystemWindow.I.Textures.statsWtask);
			GUI.DrawTexture(new Rect(x + 385f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsAchievement);
			GUI.DrawTexture(new Rect(x + 415f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsProKill);
			ClanSystemWindow.I.Styles.styleGrayLabel.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 30f, y + 10f, 50f, 20f), (index + 1).ToString(), ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Styles.styleGrayLabel.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(x + 30f, y + 21f, 50f, 20f), this._info.Role, ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			GUI.Label(new Rect(x + 118f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(this._info.memberFName, this._info.memberLName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 300f, y + 5f, 50f, 20f), this._info.killCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(x + 315f, y + 26f, 50f, 12f), ((this._info.deathCount != 0) ? ((float)this._info.killCount / (float)this._info.deathCount) : ((float)this._info.killCount)).ToString("F2"), ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(x + 355f, y + 28f, (float)ClanSystemWindow.I.Textures.statsWtask.width, 20f), this._info.contracts.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			GUI.Label(new Rect(x + 385f, y + 28f, (float)ClanSystemWindow.I.Textures.statsAchievement.width, 20f), this._info.achievements.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			GUI.Label(new Rect(x + 415f, y + 28f, (float)ClanSystemWindow.I.Textures.statsProKill.width, 20f), this._info.proKills.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			if (this._info.memberUserID != Main.UserInfo.userID && Main.UserInfo.ClanRole > Role.officer)
			{
				string hint = Language.ClansSetLeaderPopupHeader + ". " + Language.ClansSetLeaderPopupHint;
				if (Main.UserInfo.ClanRole == Role.leader)
				{
					Helpers.Hint(new Rect(x + 558f, y + 14f, 26f, 23f), hint, ClanSystemWindow.I.Styles.styleWhiteLabel14MC, Helpers.HintAlignment.Rigth, 0f, 0f);
					if (GUI.Button(new Rect(x + 558f, y + 14f, 28f, 23f), Language.ClansLeaderShort, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
					{
						this.CallSetClanRolePopup(Language.ClansSetLeaderPopupHeader, "leader", 8);
					}
					hint = Language.ClansSetLtPopupHeader + ". " + Language.ClansSetLtPopupHint;
					Helpers.Hint(new Rect(x + 585f, y + 14f, 26f, 23f), hint, ClanSystemWindow.I.Styles.styleWhiteLabel14MC, Helpers.HintAlignment.Rigth, 0f, 0f);
					if (GUI.Button(new Rect(x + 583f, y + 14f, 28f, 23f), Language.ClansLieutenantShort, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
					{
						this.CallSetClanRolePopup((this._info.role != 4) ? Language.ClansSetLtPopupHeader : Language.ClansDismissLtPopupHeader, "lt", 4);
					}
				}
				if (this._info.role != 8)
				{
					hint = Language.ClansSetOfficerPopupHeader + ". " + Language.ClansSetOfficerPopupHint;
					Helpers.Hint(new Rect(x + 610f, y + 14f, 26f, 23f), hint, ClanSystemWindow.I.Styles.styleWhiteLabel14MC, Helpers.HintAlignment.Rigth, 0f, 0f);
					if (GUI.Button(new Rect(x + 608f, y + 14f, 28f, 23f), Language.ClansOfficerShort, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
					{
						this.CallSetClanRolePopup((this._info.role != 2) ? Language.ClansSetOfficerPopupHeader : Language.ClansDismissOfficerPopupHeader, "officer", 2);
					}
					if (CWGUI.Button(new Rect(x + 635f, y + 14f, 30f, 22f), string.Empty, ClanSystemWindow.I.Styles.styleCancelBtn))
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupDismiss, string.Empty, delegate()
						{
							Main.AddDatabaseRequestCallBack<KickFromClan>(delegate
							{
								this.controller.Remove(this);
							}, delegate
							{
							}, new object[]
							{
								Main.UserInfo.clanID.ToString(),
								this._info.memberUserID.ToString()
							});
						}, PopupState.dismissWarrior, false, true, this.AssembleInfo(this._info)));
					}
				}
			}
			MainGUI.Instance.VoteWidget(new Vector2(x + 430f, y + 12f), this._info.memberUserID, this._info.reputation, -1);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000466F4 File Offset: 0x000448F4
		private object[] AssembleInfo(ClanMemberInfo info)
		{
			return new object[]
			{
				info.Level,
				(int)info.memberClass,
				info.memberNick,
				info.memberFName,
				info.memberLName,
				info.role
			};
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00046750 File Offset: 0x00044950
		private void CallSetClanRolePopup(string title, string description, int role)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.SetClanRole, title, description, delegate()
			{
				Main.AddDatabaseRequestCallBack<SetClanRole>(delegate
				{
					this._info.role = ((this._info.role == role) ? 1 : role);
					if (role == 8)
					{
						Main.UserInfo.ClanRole = Role.contractor;
					}
					ClanManagment.UpdateByChangeRole = true;
				}, delegate
				{
				}, new object[]
				{
					Main.UserInfo.clanID,
					this._info.memberUserID,
					role,
					this._info.role == role
				});
			}, PopupState.setClanRole, false, true, this.AssembleInfo(this._info)));
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x000467A0 File Offset: 0x000449A0
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index, this._info.memberUserID == Main.UserInfo.userID);
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000467DC File Offset: 0x000449DC
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000467E4 File Offset: 0x000449E4
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x0400082A RID: 2090
		public static int position;

		// Token: 0x0400082B RID: 2091
		private ScrollList controller;

		// Token: 0x0400082C RID: 2092
		private ClanMemberInfo _info = new ClanMemberInfo();
	}
}
