using System;
using ClanSystemGUI;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200031D RID: 797
	internal class RatingBar : IScrollListItem, IComparable
	{
		// Token: 0x06001B0D RID: 6925 RVA: 0x000F4AF4 File Offset: 0x000F2CF4
		protected RatingBar()
		{
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000F4B24 File Offset: 0x000F2D24
		public RatingBar(LeagueRatingInfo info)
		{
			this._info = info;
			this.InitRect();
			if (this._info.player_class > 0)
			{
				this.ClassIcon = ClanSystemWindow.I.Textures.statsClass[this._info.player_class - 1];
			}
			this.LevelIcon = MainGUI.Instance.rank_icon[this._info.level];
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000F4BB0 File Offset: 0x000F2DB0
		public float BarWidth
		{
			get
			{
				return 360f;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000F4BB8 File Offset: 0x000F2DB8
		public float BarHeigth
		{
			get
			{
				return 47f;
			}
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000F4BC0 File Offset: 0x000F2DC0
		public int CompareTo(object obj)
		{
			if (this._info.lp > ((RatingBar)obj)._info.lp)
			{
				return -1;
			}
			if (this._info.lp < ((RatingBar)obj)._info.lp)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000F4C14 File Offset: 0x000F2E14
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index);
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000F4C20 File Offset: 0x000F2E20
		public void DrawBar(float x, float y, int index)
		{
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleCenter;
			GUI.DrawTexture(new Rect(x, y, this.BarWidth, this.BarHeigth), LeagueWindow.I.Textures.DarkGray);
			if (!LeagueWindow.I.LeagueInfo.Offseason && this._info.place - 1 < 3)
			{
				GUI.DrawTexture(new Rect(x, y, (float)this.back[this._info.place - 1].width, (float)this.back[this._info.place - 1].height), this.back[this._info.place - 1]);
			}
			GUI.Label(new Rect(x, 24f + y, 25f, 0f), this._info.place.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.DrawTexture(new Rect(30f, 5f + y, (float)LeagueWindow.I.Textures.OnlineSpot[0].width, (float)LeagueWindow.I.Textures.OnlineSpot[0].height), LeagueWindow.I.Textures.OnlineSpot[(!this._info.online) ? 1 : 0]);
			GUI.Label(new Rect(242f, 24f + y, 0f, 0f), this._info.lp.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(285f, 24f + y, 0f, 0f), this._info.wins.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(315f, 24f + y, 0f, 0f), this._info.loss.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(345f, 24f + y, 0f, 0f), this._info.leav.ToString(), LeagueWindow.I.Styles.WhiteLabel16);
			LeagueWindow.I.Styles.WhiteLabel16.alignment = TextAnchor.MiddleLeft;
			LeagueHelpers.DrawRank(x + 31f, y + 26f, this._info.lp, 1f, false);
			this.DrawPlayerInfo(x, y);
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000F4EC8 File Offset: 0x000F30C8
		private void InitRect()
		{
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.RatingBarRects[1], LeagueWindow.I.Textures.OnlineSpot[0]);
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.RatingBarRects[2], LeagueWindow.I.Textures.Rank);
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.RatingBarRects[3], MainGUI.Instance.rank_icon[this._info.level]);
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.RatingBarRects[9], LeagueWindow.I.Styles.InfoBtnStyle);
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000F4F98 File Offset: 0x000F3198
		public float Width
		{
			get
			{
				return this.BarWidth;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000F4FA0 File Offset: 0x000F31A0
		public float Height
		{
			get
			{
				return this.BarHeigth;
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000F4FA8 File Offset: 0x000F31A8
		public void DrawPlayerInfo(float x, float y)
		{
			float num = 0f;
			GUI.DrawTexture(new Rect(x + 55f, y + 5f, (float)this.LevelIcon.width, (float)this.LevelIcon.height), this.LevelIcon);
			if (this.ClassIcon)
			{
				GUI.DrawTexture(new Rect(x + 80f, y - 1f, (float)this.ClassIcon.width, (float)this.ClassIcon.height), this.ClassIcon);
				num += (float)(50 + this.LevelIcon.width + this.ClassIcon.width);
			}
			else
			{
				num += (float)(50 + this.LevelIcon.width + 10);
			}
			LeagueWindow.I.Styles.WhiteLabel16.clipping = TextClipping.Clip;
			this.DrawNick(x, y, num);
			LeagueWindow.I.Styles.WhiteLabel16.clipping = TextClipping.Overflow;
			num = (float)(50 + ((!this.ClassIcon) ? 31 : this.ClassIcon.width) + 8);
			LeagueWindow.I.Styles.GrayLabel.clipping = TextClipping.Clip;
			GUI.Label(new Rect(x + num, y + 26f, 120f, 14f), this._info.first_name + " " + this._info.last_name, LeagueWindow.I.Styles.GrayLabel);
			LeagueWindow.I.Styles.GrayLabel.clipping = TextClipping.Overflow;
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000F5144 File Offset: 0x000F3344
		private void DrawNick(float x, float y, float dx)
		{
			int num = this._info.tag.Length + this._info.user_name.Trim().Length - 9 + 1;
			if ((float)num > 15f)
			{
				this.DrawMovingNick(x, y, dx, num);
			}
			else
			{
				GUI.Label(new Rect(x + dx, y + 8f, 100f, 16f), Helpers.ColoredTag(this._info.tag) + " " + this._info.user_name, LeagueWindow.I.Styles.WhiteLabel16);
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000F51EC File Offset: 0x000F33EC
		private void DrawMovingNick(float x, float y, float dx, int realTagAndNickLength)
		{
			string str = this._info.tag.Substring(0, 9);
			string text = this._info.tag.Substring(9);
			string user_name = this._info.user_name;
			this._deltaTime += Time.deltaTime;
			string text3;
			char checkChar;
			if (this._nickStringStartPosition < text.Length)
			{
				string text2 = text.Substring(this._nickStringStartPosition);
				text3 = Helpers.ColoredTag(str + text2) + " " + user_name;
				checkChar = text2[0];
			}
			else
			{
				string text4 = " " + user_name;
				text3 = text4.Substring(this._nickStringStartPosition - text.Length + 1);
				checkChar = text3[0];
			}
			float divisorForChar = RatingBar.GetDivisorForChar(checkChar);
			float num = 7f / divisorForChar;
			float num2 = 0.55f / divisorForChar;
			float num3 = num * this._deltaTime / num2;
			GUI.Label(new Rect(x + dx - num3 * (float)this._step, y + 8f, 100f + num3 * (float)this._step, 16f), text3, LeagueWindow.I.Styles.WhiteLabel16);
			if (this._deltaTime >= num2)
			{
				this._deltaTime = 0f;
				this._nickStringStartPosition += this._step;
				if ((float)(realTagAndNickLength - this._nickStringStartPosition) <= 15f || this._nickStringStartPosition < 1)
				{
					this._step = -this._step;
				}
			}
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000F537C File Offset: 0x000F357C
		private static float GetDivisorForChar(char checkChar)
		{
			float result;
			if (char.IsUpper(checkChar) || checkChar == '*')
			{
				result = 1f;
			}
			else
			{
				result = 1.75f;
			}
			return result;
		}

		// Token: 0x0400200C RID: 8204
		private const float MaxStaticNickLength = 15f;

		// Token: 0x0400200D RID: 8205
		private const float CharScrollTime = 0.55f;

		// Token: 0x0400200E RID: 8206
		private const float DxMaxDelta = 7f;

		// Token: 0x0400200F RID: 8207
		private const int TagColorCodeLength = 9;

		// Token: 0x04002010 RID: 8208
		private LeagueRatingInfo _info;

		// Token: 0x04002011 RID: 8209
		private Texture2D ClassIcon;

		// Token: 0x04002012 RID: 8210
		private Texture2D LevelIcon;

		// Token: 0x04002013 RID: 8211
		protected Texture2D[] back = LeagueWindow.I.Textures.PrizePlaceStripes;

		// Token: 0x04002014 RID: 8212
		private int _nickStringStartPosition;

		// Token: 0x04002015 RID: 8213
		private float _deltaTime;

		// Token: 0x04002016 RID: 8214
		private int _step = 1;
	}
}
