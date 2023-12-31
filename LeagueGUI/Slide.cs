using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000327 RID: 807
	internal class Slide
	{
		// Token: 0x06001B55 RID: 6997 RVA: 0x000F648C File Offset: 0x000F468C
		public void Awake()
		{
			this._alpha.Start(false);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000F649C File Offset: 0x000F469C
		public void DrawSlide(float x = 0f, float y = 0f)
		{
			Color color = GUI.color;
			GUI.color = new Color(1f, 1f, 1f, this._alpha.Value);
			GUI.DrawTexture(new Rect(x, y, (float)this.Banner.width, (float)this.Banner.height), this.Banner);
			GUI.Label(new Rect(x, y + 330f, 0f, 0f), this.Title, LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(new Rect(x, y + 350f, 0f, 0f), this.Description, LeagueWindow.I.Styles.GrayWhiteLabel);
			GUI.color = color;
		}

		// Token: 0x0400202B RID: 8235
		private readonly CurveEval _alpha = new CurveEval(LeagueWindow.I.AlphaCurve);

		// Token: 0x0400202C RID: 8236
		public Texture2D Banner;

		// Token: 0x0400202D RID: 8237
		public string Title;

		// Token: 0x0400202E RID: 8238
		public string Description;
	}
}
