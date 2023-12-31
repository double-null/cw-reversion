using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public class ScrollList
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x00047B94 File Offset: 0x00045D94
		public ScrollList()
		{
			this.ItemList = new List<IScrollListItem>();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00047BEC File Offset: 0x00045DEC
		public virtual void OnGUI()
		{
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
			int num = (int)(this.ScrollPosition.y / this.FillDensity);
			while ((float)num < (this.ScrollPosition.y + this.OverRect.height) / this.FillDensity)
			{
				if (num < this.ItemList.Count)
				{
					this.ItemList[num].OnGUI(this.InnerRect.x, this.FillDensity * (float)num, num + this.DeltaIndex);
				}
				num++;
			}
			GUI.EndScrollView();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00047D58 File Offset: 0x00045F58
		public virtual void Sort()
		{
			this.ItemList.Sort();
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00047D68 File Offset: 0x00045F68
		public virtual void Remove(IScrollListItem item)
		{
			this.ItemList.Remove(item);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00047D78 File Offset: 0x00045F78
		public virtual void Add(IScrollListItem item)
		{
			this.ItemList.Add(item);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00047D88 File Offset: 0x00045F88
		public virtual void Clear()
		{
			this.ItemList.Clear();
		}

		// Token: 0x0400083B RID: 2107
		public Vector2 ScrollPosition = default(Vector2);

		// Token: 0x0400083C RID: 2108
		protected List<IScrollListItem> ItemList;

		// Token: 0x0400083D RID: 2109
		public float FillDensity = 10f;

		// Token: 0x0400083E RID: 2110
		public Rect OverRect = default(Rect);

		// Token: 0x0400083F RID: 2111
		public Rect InnerRect = default(Rect);

		// Token: 0x04000840 RID: 2112
		public int DeltaIndex;

		// Token: 0x04000841 RID: 2113
		protected float scrollPosMin;
	}
}
