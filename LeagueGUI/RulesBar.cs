using System;
using ClanSystemGUI;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000320 RID: 800
	internal class RulesBar : IScrollListItem, IComparable
	{
		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001B2A RID: 6954 RVA: 0x000F5984 File Offset: 0x000F3B84
		public float BarWidth
		{
			get
			{
				return 370f;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x000F598C File Offset: 0x000F3B8C
		public float BarHeigth
		{
			get
			{
				return 2000f;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x000F5994 File Offset: 0x000F3B94
		public float Width
		{
			get
			{
				return this.BarWidth;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000F599C File Offset: 0x000F3B9C
		public float Height
		{
			get
			{
				return this.BarHeigth;
			}
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000F59A4 File Offset: 0x000F3BA4
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000F59A8 File Offset: 0x000F3BA8
		public void OnGUI(float x, float y, int index)
		{
			this.DrawBar(x, y);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000F59B4 File Offset: 0x000F3BB4
		private void DrawBar(float x, float y)
		{
			GUI.Label(new Rect(5f, 5f, this.BarWidth - 15f, this.BarHeigth), LeagueWindow.I.LeagueInfo.Rules, LeagueWindow.I.Styles.WhiteLabel15);
		}
	}
}
