using System;
using UnityEngine;

namespace Samples
{
	// Token: 0x02000176 RID: 374
	internal class SpawnButton : item
	{
		// Token: 0x06000A98 RID: 2712 RVA: 0x00080798 File Offset: 0x0007E998
		public SpawnButton(float x, float y, GUIContent cont, int ID, bool drawID = false)
		{
			this.content = cont;
			this.x = x;
			this.y = y;
			this.ID = ID;
			this.drawID = drawID;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000807EC File Offset: 0x0007E9EC
		public int GetID()
		{
			return this.ID;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x000807F4 File Offset: 0x0007E9F4
		public void SetContent(GUIContent content)
		{
			this.content = content;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00080800 File Offset: 0x0007EA00
		public void Set(SpawnEngine en)
		{
			this.engine = en;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0008080C File Offset: 0x0007EA0C
		public void SetPos(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0008081C File Offset: 0x0007EA1C
		public void Reset()
		{
			this.b = false;
			this.alpha.Hide(0.15f, 0f);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0008083C File Offset: 0x0007EA3C
		public void OnGUI()
		{
			if (!this.Enabled || this.content == null || this.content.image == null)
			{
				return;
			}
			GUI.DrawTexture(new Rect(this.x - (float)(this.content.image.width / 2), this.y - (float)(this.content.image.height / 2), (float)this.content.image.width, (float)this.content.image.height), this.content.image);
			if (this.b)
			{
				if (this.alpha.Showing || this.alpha.visibility > 0f)
				{
					this.visibility = 1f;
					if (this.visibility < 0f && this.alpha.Showing)
					{
						this.visibility = 0.5f;
					}
					this.lastColor = GUI.color;
					GUI.color = Colors.alpha(GUI.color, this.alpha.visibility * this.lastColor.a);
					this.rotated = true;
				}
				GUI.DrawTexture(new Rect(this.x - (float)CWGUI.p.spawnChooseButtonPushed.normal.background.width * this.visibility / 2f, this.y - (float)CWGUI.p.spawnChooseButtonPushed.normal.background.height * this.visibility / 2f, (float)CWGUI.p.spawnChooseButtonPushed.normal.background.width * this.visibility, (float)CWGUI.p.spawnChooseButtonPushed.normal.background.height * this.visibility), CWGUI.p.spawnChooseButtonPushed.normal.background, ScaleMode.StretchToFill);
				if (this.rotated)
				{
					GUI.color = Colors.alpha(GUI.color, this.lastColor.a);
					this.rotated = false;
				}
			}
			if (!GUI.Button(new Rect(this.x - (float)this.content.image.width, this.y - (float)this.content.image.height, (float)(this.content.image.width * 2), (float)(this.content.image.height * 2)), (!this.drawID) ? string.Empty : this.ID.ToString(), CWGUI.p.spawnChooseButton))
			{
				return;
			}
			if (this.isClickable)
			{
				this.OnClick();
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00080B0C File Offset: 0x0007ED0C
		public void OnClick()
		{
			if (!this.alpha.Showing)
			{
				this.alpha.Show(0.15f, 0f);
			}
			this.b = true;
			this.engine.OnClick(this);
		}

		// Token: 0x04000C6D RID: 3181
		private bool rotated;

		// Token: 0x04000C6E RID: 3182
		private bool b;

		// Token: 0x04000C6F RID: 3183
		public bool drawID;

		// Token: 0x04000C70 RID: 3184
		private int ID = -1;

		// Token: 0x04000C71 RID: 3185
		private float x;

		// Token: 0x04000C72 RID: 3186
		private float y;

		// Token: 0x04000C73 RID: 3187
		private float visibility;

		// Token: 0x04000C74 RID: 3188
		private Alpha alpha = new Alpha();

		// Token: 0x04000C75 RID: 3189
		private Color lastColor;

		// Token: 0x04000C76 RID: 3190
		public bool Enabled;

		// Token: 0x04000C77 RID: 3191
		public bool isClickable = true;

		// Token: 0x04000C78 RID: 3192
		public GUIContent content;

		// Token: 0x04000C79 RID: 3193
		public SpawnEngine engine;
	}
}
