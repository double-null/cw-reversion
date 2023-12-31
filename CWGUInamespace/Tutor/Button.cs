using System;
using UnityEngine;

namespace CWGUInamespace.Tutor
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	internal class Button
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x0002EC7C File Offset: 0x0002CE7C
		private void SetRect(float x, float y)
		{
			if ((this.rect.x != x || this.rect.y != y) && this.style.normal.background != null)
			{
				this.rect.Set(x - (float)(this.style.normal.background.width / 2), y - (float)(this.style.normal.background.height / 2), (float)this.style.normal.background.width, (float)this.style.normal.background.height);
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0002ED34 File Offset: 0x0002CF34
		public void OnGUI(float x, float y)
		{
			this.SetRect(x, y);
			if (GUI.Button(this.rect, this.content, this.style))
			{
				this.action();
			}
		}

		// Token: 0x0400064F RID: 1615
		public Button.execute action = delegate()
		{
		};

		// Token: 0x04000650 RID: 1616
		private Rect rect = default(Rect);

		// Token: 0x04000651 RID: 1617
		public GUIStyle style = new GUIStyle();

		// Token: 0x04000652 RID: 1618
		public GUIContent content = new GUIContent();

		// Token: 0x020003AC RID: 940
		// (Invoke) Token: 0x06001E18 RID: 7704
		public delegate void execute();
	}
}
