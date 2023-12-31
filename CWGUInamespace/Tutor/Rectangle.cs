using System;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	internal class Rectangle
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0002E710 File Offset: 0x0002C910
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0002E718 File Offset: 0x0002C918
		public void SetRect(Rect rect)
		{
			if (this.rect != rect)
			{
				this.rect = rect;
				this.rectUp.Set(rect.x, rect.y, rect.width, this.borderSize);
				this.rectDown.Set(rect.x, rect.yMax - this.borderSize, rect.width, this.borderSize);
				this.rectLeft.Set(rect.x, rect.y, this.borderSize, rect.height);
				this.rectRight.Set(rect.xMax - this.borderSize, rect.y, this.borderSize, rect.height);
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		public void OnGUI(Rect rect)
		{
			this.SetRect(rect);
			if (this.pix != null)
			{
				this.DrawRect();
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0002E804 File Offset: 0x0002CA04
		private void DrawRect()
		{
			GUI.DrawTexture(this.rectUp, this.pix, ScaleMode.StretchToFill);
			GUI.DrawTexture(this.rectDown, this.pix, ScaleMode.StretchToFill);
			GUI.DrawTexture(this.rectLeft, this.pix, ScaleMode.StretchToFill);
			GUI.DrawTexture(this.rectRight, this.pix, ScaleMode.StretchToFill);
		}

		// Token: 0x0400063B RID: 1595
		private Rect rect = default(Rect);

		// Token: 0x0400063C RID: 1596
		private Rect rectUp = default(Rect);

		// Token: 0x0400063D RID: 1597
		private Rect rectDown = default(Rect);

		// Token: 0x0400063E RID: 1598
		private Rect rectLeft = default(Rect);

		// Token: 0x0400063F RID: 1599
		private Rect rectRight = default(Rect);

		// Token: 0x04000640 RID: 1600
		public float borderSize = 1f;

		// Token: 0x04000641 RID: 1601
		public Texture2D pix;
	}
}
