using System;
using UnityEngine;

namespace namespaceMainGUI
{
	// Token: 0x0200015E RID: 350
	internal class TextHint
	{
		// Token: 0x06000922 RID: 2338 RVA: 0x0005B008 File Offset: 0x00059208
		public void ReCalcWidth()
		{
			float num = 0f;
			float num2 = 0f;
			if (this.obj != null)
			{
				CWGUI.p.awardsStyle.CalcMinMaxWidth(new GUIContent(this.obj.GetText()), out num, out num2);
			}
			else
			{
				CWGUI.p.awardsStyle.CalcMinMaxWidth(new GUIContent(this.Hint), out num, out num2);
			}
			this.width = (int)num2 + 10;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0005B080 File Offset: 0x00059280
		public void ReCalcWidth(string str)
		{
			float num = 0f;
			float num2 = 0f;
			CWGUI.p.awardsStyle.CalcMinMaxWidth(new GUIContent(str), out num, out num2);
			this.width = (int)num2 + 10;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0005B0C0 File Offset: 0x000592C0
		public void OnGUI(float x, float y)
		{
			if (this.rect.x != x || this.rect.y != y)
			{
				this.rect.Set(x, y, this.rect.width, this.rect.height);
				this.OnChangedRect();
			}
			if (this.rect.Contains(Event.current.mousePosition))
			{
				if (this.obj == null)
				{
					if (this.Hint != string.Empty)
					{
						GUI.Label(this.hintRect, this.Hint, CWGUI.p.awardsStyle);
					}
				}
				else
				{
					GUI.Label(this.hintRect, this.obj.GetText(), CWGUI.p.awardsStyle);
				}
				if (this.hoverEvent != null)
				{
					this.hoverEvent.OnHover();
					this.onceOnHover = true;
				}
			}
			else if (this.onceOnHover)
			{
				this.hoverEvent.OnNormal();
				this.onceOnHover = false;
			}
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x0005B1D4 File Offset: 0x000593D4
		public void OnChangedRect()
		{
			float num = CWGUI.p.awardsStyle.CalcHeight(new GUIContent(this.Hint), (float)this.width);
			this.hintRect.Set(this.rect.x + this.xoffset, this.rect.y - num + this.yoffset, (float)this.width, num);
		}

		// Token: 0x04000A4B RID: 2635
		public Rect rect = default(Rect);

		// Token: 0x04000A4C RID: 2636
		public string Hint = string.Empty;

		// Token: 0x04000A4D RID: 2637
		public GetText obj;

		// Token: 0x04000A4E RID: 2638
		public HoverEvent hoverEvent;

		// Token: 0x04000A4F RID: 2639
		protected Rect hintRect = default(Rect);

		// Token: 0x04000A50 RID: 2640
		protected float TextHeight;

		// Token: 0x04000A51 RID: 2641
		public int width = 78;

		// Token: 0x04000A52 RID: 2642
		private bool onceOnHover;

		// Token: 0x04000A53 RID: 2643
		public float xoffset;

		// Token: 0x04000A54 RID: 2644
		public float yoffset;
	}
}
