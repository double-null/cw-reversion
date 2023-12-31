using System;
using Assets.Scripts.Game.Foundation;
using ClanSystemGUI;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F6 RID: 246
	internal class WatchlistBar : IScrollListItem, IComparable
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0003B484 File Offset: 0x00039684
		public WatchlistBar()
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003B4A4 File Offset: 0x000396A4
		public WatchlistBar(WatchlistItem item)
		{
			this.watchlistItem = item;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x0003B4CC File Offset: 0x000396CC
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0003B4E4 File Offset: 0x000396E4
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0003B4FC File Offset: 0x000396FC
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0003B504 File Offset: 0x00039704
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003B50C File Offset: 0x0003970C
		public int CompareTo(object obj)
		{
			if (this.watchlistItem.PlayerExp > ((WatchlistBar)obj).watchlistItem.PlayerExp)
			{
				return -1;
			}
			if (this.watchlistItem.PlayerExp < ((WatchlistBar)obj).watchlistItem.PlayerExp)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0003B560 File Offset: 0x00039760
		public void DrawBar(float x, float y, int index)
		{
			if (this.watchlistItem.PlayerClass > 0)
			{
				this.classIconWidth = 31f;
			}
			if (!string.IsNullOrEmpty(this.watchlistItem.ClanTag))
			{
				this.clanTagWidth = MainGUI.Instance.CalcWidth(this.watchlistItem.ClanTag, MainGUI.Instance.fontDNC57, 16);
			}
			if (this.watchlistItem.PlayerFirstname != string.Empty)
			{
				this.firstNameWidth = MainGUI.Instance.CalcWidth(this.watchlistItem.PlayerFirstname, MainGUI.Instance.fontDNC57, 14);
			}
			GUI.DrawTexture(new Rect(x, y, (float)CarrierGUI.I.ratingRecord[0].width, (float)CarrierGUI.I.ratingRecord[0].height), CarrierGUI.I.ratingRecord[0]);
			GUI.DrawTexture(new Rect(x + 57f, y, (float)MainGUI.Instance.rank_icon[this.watchlistItem.PlayerLevel].width, (float)MainGUI.Instance.rank_icon[this.watchlistItem.PlayerLevel].height), MainGUI.Instance.rank_icon[this.watchlistItem.PlayerLevel]);
			if (this.watchlistItem.PlayerClass > 0)
			{
				GUI.DrawTexture(new Rect(x + 85f, y - 5f, 31f, 32f), CarrierGUI.I.Class_small_ICON[this.watchlistItem.PlayerClass - 1]);
			}
			GUI.Label(new Rect(x, y + 10f, 60f, 20f), (index + 1).ToString(), CarrierGUI.I.RatingStyles.RatingLabel);
			if (!string.IsNullOrEmpty(this.watchlistItem.ClanTag))
			{
				MainGUI.Instance.TextField(new Rect(x + 85f + this.classIconWidth, y + 1f, 100f, 20f), this.watchlistItem.ClanTag, 16, "#D40000", TextAnchor.MiddleLeft, false, false);
			}
			CarrierGUI.I.RatingStyles.RatingLabel.alignment = TextAnchor.MiddleLeft;
			CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 16;
			GUI.Label(new Rect(x + 85f + this.classIconWidth + this.clanTagWidth, y + 1f, 200f, 20f), this.watchlistItem.PlayerNickname, CarrierGUI.I.RatingStyles.RatingLabel);
			CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 20;
			CarrierGUI.I.RatingStyles.RatingLabel.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 95f, y + 18f, 200f, 20f), this.watchlistItem.PlayerFirstname, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(x + 95f + this.firstNameWidth, y + 18f, 200f, 20f), this.watchlistItem.PlayerLastname, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(x + 220f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.watchlistItem.PlayerExp.ToString("F0")), CarrierGUI.I.RatingStyles.RatingLabel);
			GUI.Label(new Rect(x + 305f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.watchlistItem.PlayerKills.ToString()), CarrierGUI.I.RatingStyles.RatingLabel);
			GUI.Label(new Rect(x + 370f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.watchlistItem.PlayerDeaths.ToString()), CarrierGUI.I.RatingStyles.RatingLabel);
			GUI.Label(new Rect(x + 430f, y + 10f, 100f, 20f), this.watchlistItem.Kd.ToString("F2"), CarrierGUI.I.RatingStyles.RatingLabel);
			MainGUI.Instance.VoteWidget(new Vector2(x + 485f, y + 7f), this.watchlistItem.UserId, (float)this.watchlistItem.PlayerReputation, index);
			if (GUI.Button(new Rect(x + 585f, y + 9f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.watchlistItem.UserId)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
			{
				WatchlistItem watchlistItem = this.watchlistItem;
				object[] args = new object[]
				{
					watchlistItem.UserId,
					watchlistItem.PlayerLevel,
					watchlistItem.PlayerClass,
					watchlistItem.ClanTag,
					watchlistItem.PlayerNickname,
					watchlistItem.PlayerFirstname,
					watchlistItem.PlayerLastname
				};
				WatchlistManager.AddRemovePlayer(args);
			}
			if (this.watchlistItem.UserId != Main.UserInfo.userID && GUI.Button(new Rect(x + 565f, y + 9f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
			{
				CarrierGUI.I.voteIndex = index;
				CarrierGUI.I.idCached = this.watchlistItem.UserId;
				CarrierGUI.I.LoadInfo(this.watchlistItem.UserId);
			}
			if (!this.watchlistItem.IsOnline)
			{
				return;
			}
			if (this.watchlistItem.OnlineType == 2 && !Main.IsGameLoaded && GUI.Button(new Rect(x + 607f, y + 7f, 32f, 24f), string.Empty, CarrierGUI.I.RatingStyles.RatingOnlineBtn))
			{
				Peer.JoinGame(this.watchlistItem.Ip, this.watchlistItem.Port, false);
			}
			GUI.DrawTexture(new Rect(x + 612f, y + 10f, 22f, 22f), CarrierGUI.I.ratingRecord[14]);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0003BC04 File Offset: 0x00039E04
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index);
		}

		// Token: 0x04000737 RID: 1847
		private WatchlistItem watchlistItem = new WatchlistItem();

		// Token: 0x04000738 RID: 1848
		private float classIconWidth = 10f;

		// Token: 0x04000739 RID: 1849
		private float clanTagWidth;

		// Token: 0x0400073A RID: 1850
		private float firstNameWidth;
	}
}
