using System;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E7 RID: 231
	internal class Animate
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0002EA50 File Offset: 0x0002CC50
		public Animate(item animItem)
		{
			this.alpha.Show(0f, 0f);
			this.itemToAnim = animItem;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0002EAB4 File Offset: 0x0002CCB4
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0002EABC File Offset: 0x0002CCBC
		public void OnGUI(float x, float y)
		{
			this.SetRect(x, y);
			if (this.itemToAnim != null)
			{
				this.itemToAnim.OnGUI(this.animatedRect);
				if (this.alpha.visibility <= 0f && !this.alpha.Showing)
				{
					this.alpha.Show(this.fadePeriod, 0f);
				}
				if (this.alpha.visibility >= 1f && !this.alpha.Hiding)
				{
					this.alpha.Hide(this.fadePeriod, 0f);
				}
			}
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0002EB64 File Offset: 0x0002CD64
		private void SetRect(float x, float y)
		{
			if (this.rect.x != x || this.rect.y != y)
			{
				this.rect.Set(x, y, this.rect.width, this.rect.height);
			}
			this.animatedRect.Set(this.rect.x + 10f * ((this.alpha.visibility <= 0f) ? 0f : this.alpha.visibility), this.rect.y, this.rect.width, this.rect.height);
		}

		// Token: 0x0400064A RID: 1610
		public float fadePeriod = 0.7f;

		// Token: 0x0400064B RID: 1611
		private item itemToAnim;

		// Token: 0x0400064C RID: 1612
		private Rect rect = default(Rect);

		// Token: 0x0400064D RID: 1613
		private Rect animatedRect = default(Rect);

		// Token: 0x0400064E RID: 1614
		private Alpha alpha = new Alpha();
	}
}
