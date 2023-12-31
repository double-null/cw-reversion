using System;
using UnityEngine;

namespace UnityGUI.Forms
{
	// Token: 0x020000C8 RID: 200
	internal class Text : Control
	{
		// Token: 0x06000530 RID: 1328 RVA: 0x000210B0 File Offset: 0x0001F2B0
		public override void OnGUI()
		{
			GUI.Label(this.Rect, this.Content, this.Style);
		}

		// Token: 0x0400049C RID: 1180
		public GUIContent Content = new GUIContent();

		// Token: 0x0400049D RID: 1181
		public GUIStyle Style = new GUIStyle();
	}
}
