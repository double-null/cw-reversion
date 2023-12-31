using System;
using System.Collections.Generic;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F3 RID: 243
	internal class ScrollableLine
	{
		// Token: 0x06000690 RID: 1680 RVA: 0x0003A560 File Offset: 0x00038760
		public ScrollableLine(Rect rect)
		{
			this.lineRect = rect;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0003A588 File Offset: 0x00038788
		public void ClearContainer()
		{
			this.container.Clear();
			this.maxContainerWidth = 0f;
			this.startIndex = 0;
			this.RecalcDeltaPos();
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0003A5B0 File Offset: 0x000387B0
		public void SetImage(IconContainer item)
		{
			this.maxContainerWidth += (float)item.Width + this.deltaPos;
			this.container.Add(item);
			this.RecalcDeltaPos();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0003A5E0 File Offset: 0x000387E0
		private void RecalcDeltaPos()
		{
			if (this.lineRect.width < this.maxContainerWidth)
			{
				this.deltaPos = 70f;
			}
			else
			{
				this.deltaPos = this.lineRect.width / 2f - this.maxContainerWidth / 2f;
			}
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0003A638 File Offset: 0x00038838
		public void OnGUI()
		{
			if (this.container.Count > 0 && this.contBackgr != null)
			{
				GUI.DrawTexture(new Rect(this.lineRect.x, this.lineRect.y, (float)this.contBackgr.width, (float)this.contBackgr.height), this.contBackgr);
			}
			int i = this.startIndex;
			int num = 0;
			while (i < this.container.Count)
			{
				if ((float)num + this.lineRect.x + this.deltaPos > this.lineRect.x)
				{
					this.container[i].OnGUI((float)num + this.lineRect.x + this.deltaPos, this.lineRect.y);
				}
				num += this.container[i].Width;
				if (this.deltaPos + (float)num > this.lineRect.width)
				{
					break;
				}
				i++;
			}
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0003A750 File Offset: 0x00038950
		public void Left()
		{
			if (this.maxContainerWidth <= this.lineRect.width)
			{
				return;
			}
			this.deltaPos -= this.deltaStep;
			if (this.deltaPos < -this.maxContainerWidth + this.lineRect.width)
			{
				this.deltaPos = -this.maxContainerWidth + this.lineRect.width;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0003A7C0 File Offset: 0x000389C0
		public void Right()
		{
			if (this.maxContainerWidth <= this.lineRect.width)
			{
				return;
			}
			this.deltaPos += this.deltaStep;
			if (this.deltaPos > this.lineRect.width / 2f)
			{
				this.deltaPos = this.lineRect.width / 2f;
			}
		}

		// Token: 0x04000727 RID: 1831
		protected List<IconContainer> container = new List<IconContainer>();

		// Token: 0x04000728 RID: 1832
		public Texture2D contBackgr;

		// Token: 0x04000729 RID: 1833
		public float deltaStep = 1f;

		// Token: 0x0400072A RID: 1834
		protected float deltaPos;

		// Token: 0x0400072B RID: 1835
		protected int startIndex;

		// Token: 0x0400072C RID: 1836
		protected Rect lineRect;

		// Token: 0x0400072D RID: 1837
		protected float maxContainerWidth;
	}
}
