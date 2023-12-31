using System;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	internal class Arrow : item
	{
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0002E880 File Offset: 0x0002CA80
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0002E888 File Offset: 0x0002CA88
		public void SetRect(Rect rect)
		{
			if ((this.rect.x != rect.x || this.rect.y != rect.y) && this.arrow != null)
			{
				this.rect.Set(rect.x, rect.y, (float)this.arrow.width, (float)this.arrow.height);
			}
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0002E908 File Offset: 0x0002CB08
		public void OnGUI(Rect rect)
		{
			this.SetRect(rect);
			if (this.arrow != null)
			{
				GUI.DrawTexture(this.rect, this.arrow);
			}
		}

		// Token: 0x04000642 RID: 1602
		public Texture2D arrow;

		// Token: 0x04000643 RID: 1603
		private Rect rect = default(Rect);
	}
}
