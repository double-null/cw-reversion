using System;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E5 RID: 229
	[Serializable]
	internal class Text
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0002E97C File Offset: 0x0002CB7C
		public Rect Rect
		{
			get
			{
				return this.rect;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0002E984 File Offset: 0x0002CB84
		private void SetRect(float x, float y)
		{
			if (x != this.rect.x || y != this.rect.y)
			{
				if (this.height == 0f || this.content.text.Length != this.size)
				{
					this.height = this.style.CalcHeight(this.content, this.width);
					this.size = this.content.text.Length;
				}
				this.rect.Set(x, y, this.width, this.height);
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0002EA2C File Offset: 0x0002CC2C
		public void OnGUI(float x, float y)
		{
			this.SetRect(x, y);
			GUI.Label(this.rect, this.content, this.style);
		}

		// Token: 0x04000644 RID: 1604
		private Rect rect = default(Rect);

		// Token: 0x04000645 RID: 1605
		public GUIStyle style = new GUIStyle();

		// Token: 0x04000646 RID: 1606
		public GUIContent content = new GUIContent(string.Empty);

		// Token: 0x04000647 RID: 1607
		private float width = 220f;

		// Token: 0x04000648 RID: 1608
		private float height;

		// Token: 0x04000649 RID: 1609
		private int size;
	}
}
