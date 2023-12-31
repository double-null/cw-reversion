using System;
using UnityEngine;

namespace UnityGUI.Forms
{
	// Token: 0x020000C4 RID: 196
	internal class Button : Control
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000524 RID: 1316 RVA: 0x00020F08 File Offset: 0x0001F108
		// (remove) Token: 0x06000525 RID: 1317 RVA: 0x00020F24 File Offset: 0x0001F124
		public event EventHandler click;

		// Token: 0x06000526 RID: 1318 RVA: 0x00020F40 File Offset: 0x0001F140
		public override void OnGUI()
		{
			if (GUI.Button(this.Rect, this.content, this.style) && this.click != null)
			{
				this.click(this, new EventArgs());
			}
		}

		// Token: 0x04000494 RID: 1172
		public GUIContent content = new GUIContent();

		// Token: 0x04000495 RID: 1173
		public GUIStyle style = new GUIStyle();
	}
}
