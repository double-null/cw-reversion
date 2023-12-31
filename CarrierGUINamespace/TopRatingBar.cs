using System;
using System.Text.RegularExpressions;
using Assets.Scripts.Game.Foundation;
using ClanSystemGUI;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F4 RID: 244
	internal class TopRatingBar : IScrollListItem, IComparable
	{
		// Token: 0x06000697 RID: 1687 RVA: 0x0003A82C File Offset: 0x00038A2C
		public TopRatingBar()
		{
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0003A880 File Offset: 0x00038A80
		public TopRatingBar(Rating info, bool isSeason, bool isHardcore)
		{
			this.ratingInfo = info;
			this._isSeasonRating = isSeason;
			this._isHardcore = isHardcore;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0003A8E8 File Offset: 0x00038AE8
		public TopRatingBar(WatchlistItem item)
		{
			this.watchlistItem = item;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0003A944 File Offset: 0x00038B44
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0003A95C File Offset: 0x00038B5C
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0003A974 File Offset: 0x00038B74
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0003A97C File Offset: 0x00038B7C
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003A984 File Offset: 0x00038B84
		public int CompareTo(object obj)
		{
			if (this.ratingInfo.exp > ((TopRatingBar)obj).ratingInfo.exp)
			{
				return -1;
			}
			if (this.ratingInfo.exp < ((TopRatingBar)obj).ratingInfo.exp)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		public void DrawBar(float x, float y, int index)
		{
			if (this.ratingInfo.SocialNet > 0)
			{
				int num = (this.ratingInfo.SocialNet != 2) ? 11 : 3;
				int num2 = this.ratingInfo.SocialNet - 1;
				if (num2 > MainGUI.Instance.SocialNetIcons.Length - 1)
				{
					num2 = 2;
				}
				Texture2D texture2D = MainGUI.Instance.SocialNetIcons[num2];
				GUI.DrawTexture(new Rect(x + 8f, y + (float)num, (float)(texture2D.width / 2), (float)(texture2D.height / 2)), texture2D);
			}
			x += 30f;
			if (this.ratingInfo.class_int > 0)
			{
				this.classIconWidth = 31f;
			}
			if (!string.IsNullOrEmpty(this.ratingInfo.clanTag))
			{
				this.clanTagWidth = MainGUI.Instance.CalcWidth(this.ratingInfo.clanTag, MainGUI.Instance.fontDNC57, 16);
			}
			if (this.ratingInfo.first_name != string.Empty)
			{
				this.firstNameWidth = MainGUI.Instance.CalcWidth(this.ratingInfo.first_name, MainGUI.Instance.fontDNC57, 14);
			}
			GUI.DrawTexture(new Rect(x, y, (float)CarrierGUI.I.ratingRecord[0].width, (float)CarrierGUI.I.ratingRecord[0].height), CarrierGUI.I.ratingRecord[0]);
			GUI.DrawTexture(new Rect(x + 57f, y, (float)MainGUI.Instance.rank_icon[this.ratingInfo.level].width, (float)MainGUI.Instance.rank_icon[this.ratingInfo.level].height), MainGUI.Instance.rank_icon[this.ratingInfo.level]);
			if (this.ratingInfo.class_int > 0)
			{
				GUI.DrawTexture(new Rect(x + 85f, y - 5f, 31f, 32f), CarrierGUI.I.Class_small_ICON[this.ratingInfo.class_int - 1]);
			}
			GUI.Label(new Rect(x, y + 10f, 60f, 20f), (index + 1).ToString(), CarrierGUI.I.RatingStyles.RatingLabel);
			if (!string.IsNullOrEmpty(this.ratingInfo.clanTag))
			{
				MainGUI.Instance.TextField(new Rect(x + 85f + this.classIconWidth, y + 1f, 100f, 20f), this.ratingInfo.clanTag, 16, "#D40000", TextAnchor.MiddleLeft, false, false);
			}
			CarrierGUI.I.RatingStyles.RatingLabel.alignment = TextAnchor.MiddleLeft;
			CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 16;
			GUIStyle guistyle = new GUIStyle(CarrierGUI.I.RatingStyles.RatingLabel);
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			if (Regex.IsMatch(this.ratingInfo.nameColor.Substring(1, 6), "^[0-9,A-F,a-f]+$"))
			{
				num3 = Convert.ToInt32(this.ratingInfo.nameColor.Substring(1, 2), 16);
				num4 = Convert.ToInt32(this.ratingInfo.nameColor.Substring(3, 2), 16);
				num5 = Convert.ToInt32(this.ratingInfo.nameColor.Substring(5, 2), 16);
			}
			guistyle.normal.textColor = new Color((float)num3 / 256f, (float)num4 / 256f, (float)num5 / 256f);
			GUI.Label(new Rect(x + 85f + this.classIconWidth + this.clanTagWidth, y + 1f, 200f, 20f), this.ratingInfo.name, guistyle);
			CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 20;
			CarrierGUI.I.RatingStyles.RatingLabel.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(x + 95f, y + 18f, 200f, 20f), this.ratingInfo.first_name, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(x + 95f + this.firstNameWidth, y + 18f, 200f, 20f), this.ratingInfo.last_name, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			GUI.Label(new Rect(x + 220f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.ratingInfo.exp.ToString("F0")), CarrierGUI.I.RatingStyles.RatingLabel);
			GUI.Label(new Rect(x + 305f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.ratingInfo.kills.ToString()), CarrierGUI.I.RatingStyles.RatingLabel);
			if (!this._isSeasonRating)
			{
				GUI.Label(new Rect(x + 370f, y + 10f, 100f, 20f), Helpers.SeparateNumericString(this.ratingInfo.deaths.ToString()), CarrierGUI.I.RatingStyles.RatingLabel);
			}
			float left = (!this._isSeasonRating) ? (x + 430f) : (x + 370f);
			GUI.Label(new Rect(left, y + 10f, 100f, 20f), this.ratingInfo.kd.ToString("F2"), CarrierGUI.I.RatingStyles.RatingLabel);
			if (this._isSeasonRating)
			{
				this.Award(x, y);
			}
			else
			{
				this.Reputation(x, y, index);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003AFB4 File Offset: 0x000391B4
		private void Award(float x, float y)
		{
			float num = x + 440f;
			GUI.Label(new Rect(num, y + 10f, 100f, 20f), this.ratingInfo.SeasonReward.ToString(), CarrierGUI.I.RatingStyles.RatingLabel);
			Texture2D gldIcon = MainGUI.Instance.gldIcon;
			float num2 = num + (float)gldIcon.width + 48f;
			GUI.DrawTexture(new Rect(num2, y + 9f, (float)gldIcon.width, (float)gldIcon.height), gldIcon, ScaleMode.ScaleToFit);
			Texture2D awardIconByPlace = CarrierGUI.I.AwardsManager.GetAwardIconByPlace(this.ratingInfo.place, this._isHardcore);
			float num3 = num2 + 35f;
			float top = y - 6f;
			if (awardIconByPlace == null)
			{
				return;
			}
			this._awardRectangle.Set(num3, top, (float)awardIconByPlace.width * 1f, (float)awardIconByPlace.height * 1f);
			GUI.DrawTexture(this._awardRectangle, awardIconByPlace, ScaleMode.ScaleToFit);
			this._hintRectangle.Set(num3 + 10f, top, (float)awardIconByPlace.width * 0.6f, (float)awardIconByPlace.height * 0.65f);
			Helpers.Hint(this._hintRectangle, Language.CarrSpecialBadge, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Rigth, 0f, 15f);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003B10C File Offset: 0x0003930C
		private void Reputation(float x, float y, int index)
		{
			MainGUI.Instance.VoteWidget(new Vector2(x + 485f, y + 7f), this.ratingInfo.userID, this.ratingInfo.repa, index);
			if (this.ratingInfo.userID != Main.UserInfo.userID)
			{
				if (GUI.Button(new Rect(x + 565f, y + 9f, 23f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleInfoBtn))
				{
					CarrierGUI.I.voteIndex = index;
					CarrierGUI.I.idCached = this.ratingInfo.userID;
					CarrierGUI.I.ratingPlaceCached = this.ratingInfo.place;
					CarrierGUI.I.LoadInfo(this.ratingInfo.userID);
				}
				Rect rect = new Rect(x + 565f, y + 9f, 23f, 23f);
				if (rect.Contains(Event.current.mousePosition))
				{
					float yOffset = (float)((index != 0) ? 0 : 37);
					Helpers.Hint(new Rect(x + 565f, y + 9f, 23f, 23f), Language.HintRatingBtnInfo, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, yOffset);
				}
				if (GUI.Button(new Rect(x + 585f, y + 9f, 23f, 23f), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this.ratingInfo.userID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
				{
					Rating rating = this.ratingInfo;
					object[] args = new object[]
					{
						rating.userID,
						rating.level,
						rating.class_int,
						rating.clanTag,
						rating.name,
						rating.first_name,
						rating.last_name
					};
					WatchlistManager.AddRemovePlayer(args);
				}
				Rect rect2 = new Rect(x + 585f, y + 9f, 23f, 23f);
				if (rect2.Contains(Event.current.mousePosition))
				{
					float yOffset = (float)((index != 0) ? 0 : 37);
					Helpers.Hint(new Rect(x + 585f, y + 9f, 23f, 23f), Language.HintRatingBtnAddToFavorites, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Rigth, 0f, yOffset);
				}
			}
			if (!this.ratingInfo.online)
			{
				return;
			}
			if (this.ratingInfo.onlineType == 2 && !Main.IsGameLoaded && GUI.Button(new Rect(x + 607f, y + 7f, 32f, 24f), string.Empty, CarrierGUI.I.RatingStyles.RatingOnlineBtn))
			{
				Peer.JoinGame(this.ratingInfo.ip, this.ratingInfo.port, false);
			}
			GUI.DrawTexture(new Rect(x + 612f, y + 10f, 22f, 22f), CarrierGUI.I.ratingRecord[14]);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0003B470 File Offset: 0x00039670
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index);
		}

		// Token: 0x0400072E RID: 1838
		private Rating ratingInfo = new Rating();

		// Token: 0x0400072F RID: 1839
		private WatchlistItem watchlistItem = new WatchlistItem();

		// Token: 0x04000730 RID: 1840
		private float classIconWidth = 10f;

		// Token: 0x04000731 RID: 1841
		private float clanTagWidth;

		// Token: 0x04000732 RID: 1842
		private float firstNameWidth;

		// Token: 0x04000733 RID: 1843
		private bool _isSeasonRating;

		// Token: 0x04000734 RID: 1844
		private bool _isHardcore;

		// Token: 0x04000735 RID: 1845
		private Rect _awardRectangle = default(Rect);

		// Token: 0x04000736 RID: 1846
		private Rect _hintRectangle = default(Rect);
	}
}
