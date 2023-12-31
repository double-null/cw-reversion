using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200011B RID: 283
	internal class ClanTransactionBar : IScrollListItem, IComparable
	{
		// Token: 0x060007AE RID: 1966 RVA: 0x000477B0 File Offset: 0x000459B0
		public ClanTransactionBar()
		{
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000477C4 File Offset: 0x000459C4
		public ClanTransactionBar(ClanTransactionData data)
		{
			this.transactionData = data;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x000477E0 File Offset: 0x000459E0
		public float barHeight
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.height;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000477F8 File Offset: 0x000459F8
		public float barWidth
		{
			get
			{
				return (float)ClanSystemWindow.I.Textures.statsBack.width;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x00047810 File Offset: 0x00045A10
		public float Width
		{
			get
			{
				return this.barWidth;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00047818 File Offset: 0x00045A18
		public float Height
		{
			get
			{
				return this.barHeight;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00047820 File Offset: 0x00045A20
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00047824 File Offset: 0x00045A24
		private void PayerInfo(float x, float y)
		{
			GUI.Label(new Rect(x, y, 200f, 20f), this.transactionData.Nickname, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			GUI.Label(new Rect(x, y + 18f, 200f, 20f), this.transactionData.Firstname + " " + this.transactionData.Lastname, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x000478AC File Offset: 0x00045AAC
		public void DrawBar(float x = 0f, float y = 0f, int index = 0, bool isGP = true)
		{
			GUI.DrawTexture(new Rect(x, y, (float)ClanSystemWindow.I.Textures.statsBack.width, (float)ClanSystemWindow.I.Textures.statsBack.height), ClanSystemWindow.I.Textures.statsBack);
			GUI.Label(new Rect(x + 20f, y + 10f, 30f, 20f), (index + 1).ToString(), ClanSystemWindow.I.Styles.styleWhiteLabel16);
			if (this.transactionData.Currency == 2)
			{
				GUI.DrawTexture(new Rect(x + 70f, y + 3f, (float)ClanSystemWindow.I.Textures.historyGlow[0].width, (float)ClanSystemWindow.I.Textures.historyGlow[0].height), ClanSystemWindow.I.Textures.historyGlow[0]);
				GUI.DrawTexture(new Rect(x + 230f, y + 9f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
			}
			else
			{
				GUI.DrawTexture(new Rect(x + 70f, y + 3f, (float)ClanSystemWindow.I.Textures.historyGlow[1].width, (float)ClanSystemWindow.I.Textures.historyGlow[1].height), ClanSystemWindow.I.Textures.historyGlow[1]);
				GUI.DrawTexture(new Rect(x + 230f, y + 8f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
			}
			GUI.Label(new Rect(x + 25f, y + 10f, 200f, 20f), Helpers.SeparateNumericString(this.transactionData.Amount.ToString()), ClanSystemWindow.I.Styles.styleWhiteKoratakiLable);
			this.PayerInfo(x + 350f, y);
			GUI.Label(new Rect(x + 515f, y + 10f, 200f, 20f), this.transactionData.Date, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00047B14 File Offset: 0x00045D14
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y, index, true);
		}

		// Token: 0x0400083A RID: 2106
		private ClanTransactionData transactionData = new ClanTransactionData();
	}
}
