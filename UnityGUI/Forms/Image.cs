using System;
using UnityEngine;

namespace UnityGUI.Forms
{
	// Token: 0x020000C7 RID: 199
	internal class Image : Control
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00021064 File Offset: 0x0001F264
		public override void OnGUI()
		{
			if (this.texture != null)
			{
				GUI.DrawTexture(this.Rect, this.texture, ScaleMode.ScaleToFit, this.alpha);
			}
		}

		// Token: 0x04000499 RID: 1177
		public Texture texture;

		// Token: 0x0400049A RID: 1178
		protected ScaleMode scaleMode = ScaleMode.ScaleToFit;

		// Token: 0x0400049B RID: 1179
		protected bool alpha = true;
	}
}
