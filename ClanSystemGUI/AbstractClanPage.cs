using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000108 RID: 264
	internal abstract class AbstractClanPage : IClanPage
	{
		// Token: 0x060006EE RID: 1774 RVA: 0x0003D9BC File Offset: 0x0003BBBC
		public virtual void OnStart()
		{
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		public virtual void OnGUI()
		{
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0003D9C4 File Offset: 0x0003BBC4
		public virtual void OnUpdate()
		{
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
		public virtual void Clear()
		{
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0003D9CC File Offset: 0x0003BBCC
		protected string Search()
		{
			int num = 173;
			int num2 = 56;
			GUI.DrawTexture(new Rect((float)num, (float)(num2 + 1), (float)CarrierGUI.I.ratingRecord[15].width, (float)CarrierGUI.I.ratingRecord[15].height), CarrierGUI.I.ratingRecord[15]);
			int textMaxSize = MainGUI.Instance.textMaxSize;
			MainGUI.Instance.textMaxSize = 40;
			int textSize = (this.SearchTerm.Length <= 25) ? 15 : 10;
			this.SearchTerm = MainGUI.Instance.TextField(new Rect((float)(num + 8), (float)num2, 180f, 25f), this.SearchTerm, textSize, "#FFFFFF", TextAnchor.MiddleCenter, true, true);
			MainGUI.Instance.textMaxSize = textMaxSize;
			if (this.SearchTerm == string.Empty)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(new Rect((float)(num + 191), (float)num2, 32f, 24f), string.Empty, CarrierGUI.I.RatingStyles.RatingOnlineBtn))
			{
				return this.SearchTerm;
			}
			GUI.enabled = true;
			GUI.DrawTexture(new Rect((float)(num + 198), (float)(num2 + 4), (float)CarrierGUI.I.ratingRecord[17].width, (float)CarrierGUI.I.ratingRecord[17].height), CarrierGUI.I.ratingRecord[17]);
			return string.Empty;
		}

		// Token: 0x040007C5 RID: 1989
		protected string SearchTerm = string.Empty;
	}
}
