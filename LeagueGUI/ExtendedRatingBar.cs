using System;
using ClanSystemGUI;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200031E RID: 798
	internal class ExtendedRatingBar : RatingBar, IScrollListItem, IComparable
	{
		// Token: 0x06001B1B RID: 6939 RVA: 0x000F53B0 File Offset: 0x000F35B0
		public ExtendedRatingBar()
		{
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000F53C4 File Offset: 0x000F35C4
		public ExtendedRatingBar(LeagueRatingInfo info) : base(info)
		{
			for (int i = 0; i < this.t.Length; i++)
			{
				this.t[i] = LeagueWindow.I.LeagueInfo.SeasonPrizes[i].AwardIcon;
			}
			this._gp = MainGUI.Instance.gldIcon;
			this._cr = MainGUI.Instance.crIcon;
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001B1D RID: 6941 RVA: 0x000F5440 File Offset: 0x000F3640
		public new float BarWidth
		{
			get
			{
				return 360f;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x000F5448 File Offset: 0x000F3648
		public new float BarHeigth
		{
			get
			{
				return 94f;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001B1F RID: 6943 RVA: 0x000F5450 File Offset: 0x000F3650
		public new float Width
		{
			get
			{
				return this.BarWidth;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000F5458 File Offset: 0x000F3658
		public float Heigth
		{
			get
			{
				return this.BarHeigth;
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000F5460 File Offset: 0x000F3660
		public new int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000F5464 File Offset: 0x000F3664
		public new void OnGUI(float x, float y, int index)
		{
			base.OnGUI(x, y, index);
			this.DrawExtendedBar(x, y + 48f, index);
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000F5480 File Offset: 0x000F3680
		private void DrawExtendedBar(float x, float y, int index)
		{
			GUI.DrawTexture(new Rect(x, y, base.BarWidth, base.BarHeigth), LeagueWindow.I.Textures.DarkGray);
			if (index < 3)
			{
				GUI.DrawTexture(new Rect(x, y, (float)this.back[index].width, (float)this.back[index].height), this.back[index]);
			}
			GUI.Label(new Rect(x + 50f, y + 25f, 0f, 0f), Language.LeaguePrizes, LeagueWindow.I.Styles.WhiteLabel14);
			if (index < 3)
			{
				GUI.DrawTexture(new Rect(x + 150f, y + 1f, (float)this.t[index].width, (float)this.t[index].height), this.t[index]);
				GUI.Label(new Rect(x + 230f, y + 18f, 20f, 14f), "+", LeagueWindow.I.Styles.WhiteLabel14);
				GUI.Label(new Rect(x + 230f, y + 19f, 50f, 14f), LeagueWindow.I.LeagueInfo.SeasonPrizes[index].Amount.ToString(), LeagueWindow.I.Styles.GPLabel);
				GUI.DrawTexture(new Rect(x + 280f, y + 15f, (float)this._gp.width, (float)this._gp.height), this._gp);
			}
			else if (index < 10)
			{
				string text = Helpers.SeparateNumericString(LeagueWindow.I.LeagueInfo.SeasonPrizes[3].Amount.ToString());
				GUI.Label(new Rect(x + 125f, y + 18f, 100f, 14f), text, LeagueWindow.I.Styles.CRLabel);
				GUI.DrawTexture(new Rect(x + 230f, y + 14f, (float)this._gp.width, (float)this._gp.height), this._gp);
			}
			else if (index < 100)
			{
				string text2 = Helpers.SeparateNumericString(LeagueWindow.I.LeagueInfo.SeasonPrizes[4].Amount.ToString());
				GUI.Label(new Rect(x + 125f, y + 18f, 100f, 14f), text2, LeagueWindow.I.Styles.CRLabel);
				GUI.DrawTexture(new Rect(x + 230f, y + 14f, (float)this._gp.width, (float)this._gp.height), this._gp);
			}
			else
			{
				string text3 = Helpers.SeparateNumericString(LeagueWindow.I.LeagueInfo.SeasonPrizes[5].Amount.ToString());
				GUI.Label(new Rect(x + 125f, y + 18f, 100f, 14f), text3, LeagueWindow.I.Styles.CRLabel);
				GUI.DrawTexture(new Rect(x + 230f, y + 14f, (float)this._gp.width, (float)this._gp.height), this._gp);
			}
		}

		// Token: 0x04002017 RID: 8215
		private Texture2D[] t = new Texture2D[3];

		// Token: 0x04002018 RID: 8216
		private Texture2D _gp;

		// Token: 0x04002019 RID: 8217
		private Texture2D _cr;
	}
}
