using System;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000116 RID: 278
	internal class RequestInfoBar : IScrollListItem, IComparable
	{
		// Token: 0x0600076C RID: 1900 RVA: 0x00045384 File Offset: 0x00043584
		public RequestInfoBar(LeadMemberList controller)
		{
			this.controller = controller;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000453A0 File Offset: 0x000435A0
		public RequestInfoBar(LeadMemberList controller, ClanRequestInfo info)
		{
			this.controller = controller;
			this.info = info;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000453C8 File Offset: 0x000435C8
		public ClanRequestInfo RequestInfo
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000453D0 File Offset: 0x000435D0
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000453E8 File Offset: 0x000435E8
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00045400 File Offset: 0x00043600
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00045404 File Offset: 0x00043604
		public void DrawBar(float x, float y, int index)
		{
			if (this.info.currentClass != 0)
			{
				GUI.DrawTexture(new Rect(x + 110f, y, 31f, 32f), ClanSystemWindow.I.Textures.statsClass[this.info.currentClass - 1]);
				MainGUI.Instance.TextField(new Rect(x + 135f, y + 6f, 200f, 20f), this.info.userNick, 16, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
			}
			else
			{
				MainGUI.Instance.TextField(new Rect(x + 118f, y + 6f, 200f, 20f), this.info.userNick, 16, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
			}
			GUI.DrawTexture(new Rect(x + 25f, y + 5f, 643f, 38f), ClanSystemWindow.I.Textures.statsBack);
			if (CWGUI.Button(new Rect(x + 513f, y + 14f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
			{
				CarrierGUI.I.LoadInfo(this.info.userID);
			}
			GUI.DrawTexture(new Rect(x + 85f, y + 5f, 29f, 38f), MainGUI.Instance.rank_icon[this.info.level]);
			GUI.Label(new Rect(x + 240f, y + 15f, 50f, 20f), "---", ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.DrawTexture(new Rect(x + 305f, y + 25f, 20f, 12f), ClanSystemWindow.I.Textures.kd);
			GUI.DrawTexture(new Rect(x + 355f, y + 5f, 33f, 28f), ClanSystemWindow.I.Textures.statsWtask);
			GUI.DrawTexture(new Rect(x + 385f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsAchievement);
			GUI.DrawTexture(new Rect(x + 415f, y + 5f, 37f, 28f), ClanSystemWindow.I.Textures.statsProKill);
			GUI.Label(new Rect(x + 50f, y + 15f, 50f, 20f), "-", ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(x + 118f, y + 23f, 200f, 20f), MainGUI.Instance.FormatedUserName(this.info.userFName, this.info.userLName), ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 300f, y + 5f, 50f, 20f), this.info.killCount.ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
			GUI.Label(new Rect(x + 315f, y + 26f, 50f, 12f), ((this.info.deathCount != 0) ? ((float)this.info.killCount / (float)this.info.deathCount) : ((float)this.info.killCount)).ToString("F2"), ClanSystemWindow.I.Styles.styleGrayLabel14);
			GUI.Label(new Rect(x + 355f, y + 28f, (float)ClanSystemWindow.I.Textures.statsWtask.width, 20f), this.info.contracts.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			GUI.Label(new Rect(x + 385f, y + 28f, (float)ClanSystemWindow.I.Textures.statsAchievement.width, 20f), this.info.achievements.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			GUI.Label(new Rect(x + 415f, y + 28f, (float)ClanSystemWindow.I.Textures.statsProKill.width, 20f), this.info.prokills.ToString(), ClanSystemWindow.I.Styles.DistinctionsLabelStyle);
			MainGUI.Instance.VoteWidget(new Vector2(x + 430f, y + 12f), this.info.userID, (float)this.info.reputation, -1);
			if (GUI.Button(new Rect(x + 535f, y + 14f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.info.userID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
			{
				object[] args = new object[]
				{
					this.info.userID,
					this.info.level,
					this.info.currentClass,
					string.Empty,
					this.info.userNick,
					this.info.userFName,
					this.info.userLName
				};
				WatchlistManager.AddRemovePlayer(args);
			}
			if (CWGUI.Button(new Rect(x + 602f, y + 14f, 30f, 22f), string.Empty, ClanSystemWindow.I.Styles.styleAcceptBtn))
			{
				string[] args2 = new string[]
				{
					Main.UserInfo.clanID.ToString(),
					this.info.userID.ToString()
				};
				Main.AddDatabaseRequestCallBack<AcceptRequest>(delegate
				{
					this.controller.MoveToApproved(this);
				}, delegate
				{
				}, args2);
			}
			if (CWGUI.Button(new Rect(x + 630f, y + 14f, 30f, 22f), string.Empty, ClanSystemWindow.I.Styles.styleCancelBtn))
			{
				string[] args3 = new string[]
				{
					Main.UserInfo.clanID.ToString(),
					this.info.userID.ToString()
				};
				Main.AddDatabaseRequestCallBack<DeleteRequest>(delegate
				{
					this.controller.RemoveRquest(this);
				}, delegate
				{
				}, args3);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00045B2C File Offset: 0x00043D2C
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index);
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x00045B38 File Offset: 0x00043D38
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00045B40 File Offset: 0x00043D40
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x04000825 RID: 2085
		private ClanRequestInfo info = new ClanRequestInfo();

		// Token: 0x04000826 RID: 2086
		public static int position;

		// Token: 0x04000827 RID: 2087
		private LeadMemberList controller;
	}
}
