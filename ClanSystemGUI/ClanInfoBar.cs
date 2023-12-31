using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200011A RID: 282
	internal class ClanInfoBar : IScrollListItem, IComparable
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x00046F34 File Offset: 0x00045134
		public ClanInfoBar()
		{
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00046F54 File Offset: 0x00045154
		public ClanInfoBar(ShortClanInfo info)
		{
			this.shortClanInfo = info;
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00046F80 File Offset: 0x00045180
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00046F98 File Offset: 0x00045198
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00046FB0 File Offset: 0x000451B0
		public int CompareTo(object obj)
		{
			if (this.shortClanInfo.clanExp.Value > ((ClanInfoBar)obj).shortClanInfo.clanExp.Value)
			{
				return -1;
			}
			if (this.shortClanInfo.clanExp.Value < ((ClanInfoBar)obj).shortClanInfo.clanExp.Value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00047018 File Offset: 0x00045218
		public void DrawBar(float x, float y, int index, bool yourStats = false)
		{
			if (this.shortClanInfo.SocialNet > 0)
			{
				int num = (this.shortClanInfo.SocialNet != 2) ? 16 : 8;
				int num2 = this.shortClanInfo.SocialNet - 1;
				if (num2 > MainGUI.Instance.SocialNetIcons.Length - 1)
				{
					num2 = 2;
				}
				Texture2D texture2D = MainGUI.Instance.SocialNetIcons[num2];
				GUI.DrawTexture(new Rect(x + 32f, y + (float)num, (float)(texture2D.width / 2), (float)(texture2D.height / 2)), texture2D);
			}
			x += 30f;
			GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.statsBack);
			string text = (index + 1).ToString();
			int num3 = (text.Length <= 1) ? 25 : 32;
			GUI.Label(new Rect(x + (float)num3, y + 10f, 50f, 25f), this.shortClanInfo.clanLevel.ToString(), ClanSystemWindow.I.Styles.styleLevelLabel);
			GUI.Label(new Rect(x + 27f, y + 22f, 50f, 10f), Language.Level.ToLower(), ClanSystemWindow.I.Styles.styleLevel);
			MainGUI.Instance.TextField(new Rect(x + 80f, y + 15f, 50f, 20f), this.shortClanInfo.tag, 16, "#" + this.shortClanInfo.tagColor, TextAnchor.MiddleCenter, false, false);
			GUI.Label(new Rect(x + 135f, y + 15f, 300f, 20f), this.shortClanInfo.name, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			x -= 30f;
			if (!this.shortClanInfo.banned)
			{
				GUI.DrawTexture(new Rect(x + 315f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[this.shortClanInfo.clanLeaderLevel]);
				GUI.Label(new Rect(x + 62f, y + 10f, 50f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
				GUI.Label(new Rect(x + 348f, y + 6f, 200f, 20f), this.shortClanInfo.leaderNick, ClanSystemWindow.I.Styles.styleWhiteLabel16);
				GUI.Label(new Rect(x + 348f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.shortClanInfo.leaderFname, this.shortClanInfo.leaderLname), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
				GUI.Label(new Rect(x + 440f, y + 12f, 50f, 27f), this.shortClanInfo.membersCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				GUI.Label(new Rect(x + 480f, y + 12f, 86f, 27f), Helpers.SeparateNumericString(this.shortClanInfo.clanExp.Value.ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
				ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
				if (GUI.Button(new Rect(x + 638f, y + 14f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
				{
					ClanSystemWindow.I.SelectedClan = this.shortClanInfo;
					this.shortClanInfo.ReloadAdditionInfo(delegate
					{
						ClanSystemWindow.I.controller.SetState(ClanSystemWindow.I.controller.TabInfo);
						ClanTabInfo.showInfo = true;
					}, delegate
					{
					}, false);
				}
				if (Main.UserInfo.clanID == 0 && !this.shortClanInfo.didRequest)
				{
					if (GUI.Button(new Rect(x + 560f, y + 7f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
					{
						if (Main.UserInfo.ClanRequestsLeft != 0)
						{
							Main.AddDatabaseRequestCallBack<SendRequest>(delegate
							{
								this.shortClanInfo.didRequest = true;
								Main.UserInfo.ClanRequestsLeft--;
							}, delegate
							{
							}, new object[]
							{
								this.shortClanInfo.clanID
							});
						}
						else
						{
							EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.ClansPopupError, "byOrderFailed", PopupState.clanError, false, true, string.Empty, string.Empty));
						}
					}
					this.currentClanInfo.DrawLabelOverButton(x + 560f, y + 9f, y + 21f, 80f, 20f, Language.ClansRequest1, Language.ClansRequest2);
				}
				else if (Main.UserInfo.clanID == 0)
				{
					if (GUI.Button(new Rect(x + 560f, y + 7f, 80f, 38f), string.Empty, ClanSystemWindow.I.Styles.styleRequestBtn))
					{
						Main.AddDatabaseRequestCallBack<RevokeRequest>(delegate
						{
							this.shortClanInfo.didRequest = false;
							Main.UserInfo.ClanRequestsLeft++;
						}, delegate
						{
						}, new object[]
						{
							this.shortClanInfo.clanID
						});
					}
					this.currentClanInfo.DrawLabelOverButton(x + 560f, y + 9f, y + 21f, 80f, 20f, Language.ClansWithdraw, Language.ClansRequest2);
				}
			}
			else
			{
				GUI.DrawTexture(new Rect(x + 30f, y - 2f, 51f, 53f), ClanSystemWindow.I.Textures.disbanded);
				ClanSystemWindow.I.Styles.styleRedLabel.fontSize = 16;
				GUI.Label(new Rect(x + 420f, y + 12f, 100f, 27f), Language.ClansDisbanded, ClanSystemWindow.I.Styles.styleRedLabel);
				ClanSystemWindow.I.Styles.styleRedLabel.fontSize = 14;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000476E0 File Offset: 0x000458E0
		public void WithdrawRequest()
		{
			if (!this.shortClanInfo.didRequest)
			{
				return;
			}
			this.shortClanInfo.didRequest = false;
			Main.UserInfo.ClanRequestsLeft++;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00047714 File Offset: 0x00045914
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index, false);
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00047720 File Offset: 0x00045920
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00047728 File Offset: 0x00045928
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x04000833 RID: 2099
		public static int position;

		// Token: 0x04000834 RID: 2100
		private CurrentClanInfo currentClanInfo = new CurrentClanInfo();

		// Token: 0x04000835 RID: 2101
		private ShortClanInfo shortClanInfo = new ShortClanInfo();
	}
}
