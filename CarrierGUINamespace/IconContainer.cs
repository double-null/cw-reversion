using System;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F1 RID: 241
	internal class IconContainer
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0003A300 File Offset: 0x00038500
		public IconContainer(bool isSeasonTopAward = false)
		{
			this._isSeasonTopAward = isSeasonTopAward;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0003A32C File Offset: 0x0003852C
		public virtual void OnGUI(float x, float y)
		{
			if (this._isSeasonTopAward)
			{
				y += 15f;
			}
			if (this.image != null)
			{
				if (this.rect.x != x || this.rect.y != y)
				{
					this.rect.Set(x, y, (float)this.image.width, (float)this.image.height);
					this.OnChangedRect();
				}
				GUI.DrawTexture(new Rect(this.rect), this.image);
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0003A3C4 File Offset: 0x000385C4
		public virtual void OnChangedRect()
		{
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0003A3C8 File Offset: 0x000385C8
		public int Width
		{
			get
			{
				return (!(this.image != null)) ? 0 : this.image.width;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0003A3F8 File Offset: 0x000385F8
		public int Height
		{
			get
			{
				return (!(this.image != null)) ? 0 : this.image.height;
			}
		}

		// Token: 0x04000721 RID: 1825
		public Texture2D image;

		// Token: 0x04000722 RID: 1826
		protected Rect rect = default(Rect);

		// Token: 0x04000723 RID: 1827
		protected bool _isSeasonTopAward;
	}
}
