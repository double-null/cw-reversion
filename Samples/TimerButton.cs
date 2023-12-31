using System;
using UnityEngine;

namespace Samples
{
	// Token: 0x02000178 RID: 376
	internal class TimerButton
	{
		// Token: 0x06000AA8 RID: 2728 RVA: 0x00080CF0 File Offset: 0x0007EEF0
		public TimerButton(float x, float y, GUIContent button)
		{
			this.button = button;
			this.tmpButton.image = button.image;
			this.tmpButton.text = button.text;
			this.tmpButton.tooltip = button.tooltip;
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00080D64 File Offset: 0x0007EF64
		public void Set(float t)
		{
			this.time = Time.time + t;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00080D74 File Offset: 0x0007EF74
		public void SetPos(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00080D84 File Offset: 0x0007EF84
		public void OnGUI()
		{
			if (Time.time < this.time)
			{
				this.lastcolor = GUI.color;
				GUI.color = Colors.alpha(GUI.color, this.alpha);
				this.tmpButton.text = this.button.text + " (" + (this.time - Time.time).ToString("F0") + ")";
			}
			if ((GUI.Button(new Rect(this.x, this.y, (float)CWGUI.p.menuButton.normal.background.width, (float)CWGUI.p.menuButton.normal.background.height), (Time.time >= this.time) ? this.button : this.tmpButton, CWGUI.p.menuButton) || (!Peer.ClientGame.LocalPlayer.IsSpectactor && Main.UserInfo.settings.graphics.Autorespawn)) && Time.time > this.time && this.ex != null)
			{
				this.ex.execute();
			}
			if (Time.time < this.time)
			{
				GUI.color = this.lastcolor;
			}
		}

		// Token: 0x04000C7C RID: 3196
		public Execute ex;

		// Token: 0x04000C7D RID: 3197
		private GUIContent button;

		// Token: 0x04000C7E RID: 3198
		private GUIContent tmpButton = new GUIContent();

		// Token: 0x04000C7F RID: 3199
		public float alpha = 0.5f;

		// Token: 0x04000C80 RID: 3200
		private float x;

		// Token: 0x04000C81 RID: 3201
		private float y;

		// Token: 0x04000C82 RID: 3202
		private float time;

		// Token: 0x04000C83 RID: 3203
		private Color lastcolor;
	}
}
