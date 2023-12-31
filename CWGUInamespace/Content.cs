using System;
using UnityEngine;

namespace CWGUInamespace
{
	// Token: 0x020000EA RID: 234
	[Serializable]
	internal class Content
	{
		// Token: 0x0600060E RID: 1550 RVA: 0x0002EE14 File Offset: 0x0002D014
		public void OnGUI()
		{
			if (this.ContentType == Content.types.Label)
			{
				GUI.Label(this.rect, this.guiContent, this.style);
			}
			else if (this.ContentType == Content.types.Button)
			{
				if (GUI.Button(this.rect, this.guiContent, this.style))
				{
					this.action();
				}
			}
			else if (this.ContentType == Content.types.Image)
			{
				GUI.DrawTexture(this.rect, this.guiContent.image, this.scaleMode);
			}
			this.RecalcParams();
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
		public void RecalcParams()
		{
			if (this.SetRealContentSize || this.SetRealBackgroundSize)
			{
				if (this.SetRealContentSize)
				{
					this.rect.Set(this.rect.x, this.rect.y, (float)this.guiContent.image.width, (float)this.guiContent.image.height);
				}
				if (this.SetRealBackgroundSize)
				{
					this.rect.Set(this.rect.x, this.rect.y, (float)this.style.normal.background.width, (float)this.style.normal.background.height);
				}
				this.SetRealContentSize = (this.SetRealBackgroundSize = false);
			}
		}

		// Token: 0x04000659 RID: 1625
		public string Description = "test";

		// Token: 0x0400065A RID: 1626
		public Content.types ContentType;

		// Token: 0x0400065B RID: 1627
		public Rect rect = default(Rect);

		// Token: 0x0400065C RID: 1628
		public GUIStyle style = new GUIStyle();

		// Token: 0x0400065D RID: 1629
		public GUIContent guiContent = new GUIContent();

		// Token: 0x0400065E RID: 1630
		public ScaleMode scaleMode;

		// Token: 0x0400065F RID: 1631
		public Content.SomeAction action = delegate()
		{
		};

		// Token: 0x04000660 RID: 1632
		public bool SetRealContentSize;

		// Token: 0x04000661 RID: 1633
		public bool SetRealBackgroundSize;

		// Token: 0x020000EB RID: 235
		public enum types
		{
			// Token: 0x04000664 RID: 1636
			unknown,
			// Token: 0x04000665 RID: 1637
			Button,
			// Token: 0x04000666 RID: 1638
			Label,
			// Token: 0x04000667 RID: 1639
			Image
		}

		// Token: 0x020003AD RID: 941
		// (Invoke) Token: 0x06001E1C RID: 7708
		public delegate void SomeAction();
	}
}
