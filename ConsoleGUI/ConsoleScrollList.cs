using System;
using ClanSystemGUI;
using UnityEngine;

namespace ConsoleGUI
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class ConsoleScrollList : ScrollList
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x00010A6C File Offset: 0x0000EC6C
		public ConsoleScrollList()
		{
			this.OverRect.Set(0f, 0f, (float)(Screen.width - 30), 350f);
			this.FillDensity = 35f;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public override void OnGUI()
		{
			this.OverRect.Set(0f, 0f, (float)(Screen.width - 30), 350f);
			this.InnerRect.Set(this.InnerRect.x, this.InnerRect.y, this.InnerRect.width, this.FillDensity * (float)this.ItemList.Count);
			if (this.InnerRect.height < this.OverRect.height)
			{
				this.InnerRect.Set(this.InnerRect.x, this.InnerRect.y, this.InnerRect.width, this.OverRect.height);
			}
			this.ScrollPosition = GUI.BeginScrollView(this.OverRect, this.ScrollPosition, this.InnerRect, false, false);
			if (this.ScrollPosition.y < this.scrollPosMin)
			{
				this.ScrollPosition.y = this.scrollPosMin;
			}
			if (this.refresh)
			{
				this.CurentPos = 0f;
			}
			else
			{
				this.CurentPos = this.lastPos;
			}
			for (int i = this.beginDrawIndex; i < this.ItemList.Count; i++)
			{
				if (this.CurentPos > this.ScrollPosition.y)
				{
					this.ItemList[i].OnGUI(this.InnerRect.x, this.CurentPos, i + this.DeltaIndex);
					Debug.Log(i);
					if (this.refresh)
					{
						this.refresh = false;
						this.beginDrawIndex = i;
						this.lastPos = this.CurentPos;
					}
				}
				if (this.CurentPos > this.ScrollPosition.y + this.OverRect.height)
				{
					break;
				}
				this.CurentPos += this.ItemList[i].Height;
			}
			GUI.EndScrollView();
		}

		// Token: 0x0400025B RID: 603
		private float CurentPos;

		// Token: 0x0400025C RID: 604
		private float lastScrollPos;

		// Token: 0x0400025D RID: 605
		private float lastPos;

		// Token: 0x0400025E RID: 606
		private int beginDrawIndex;

		// Token: 0x0400025F RID: 607
		private bool refresh;
	}
}
