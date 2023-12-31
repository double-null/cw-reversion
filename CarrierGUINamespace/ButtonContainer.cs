using System;
using UnityEngine;

namespace CarrierGUINamespace
{
	// Token: 0x020000F2 RID: 242
	internal class ButtonContainer : IconContainer
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x0003A428 File Offset: 0x00038628
		public ButtonContainer(bool isSeasonTopAward = false) : base(isSeasonTopAward)
		{
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0003A44C File Offset: 0x0003864C
		public ButtonContainer(Texture2D texture, string hint, bool isSeasonTopAward = false) : base(isSeasonTopAward)
		{
			this.image = texture;
			this.Hint = hint;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0003A480 File Offset: 0x00038680
		// (set) Token: 0x0600068D RID: 1677 RVA: 0x0003A488 File Offset: 0x00038688
		public string Hint { get; private set; }

		// Token: 0x0600068E RID: 1678 RVA: 0x0003A494 File Offset: 0x00038694
		public override void OnGUI(float x, float y)
		{
			base.OnGUI(x, y);
			if (!string.IsNullOrEmpty(this.Hint) && this.rect.Contains(Event.current.mousePosition))
			{
				GUI.Label(this.hintRect, this.Hint, CWGUI.p.awardsStyle);
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003A4F0 File Offset: 0x000386F0
		public override void OnChangedRect()
		{
			float num = CWGUI.p.awardsStyle.CalcHeight(new GUIContent(this.Hint), 100f);
			this.hintRect.Set(this.rect.x + this.rect.width / 2f - 50f, this.rect.y - num, 100f, num);
		}

		// Token: 0x04000724 RID: 1828
		protected Rect hintRect = default(Rect);

		// Token: 0x04000725 RID: 1829
		protected float TextHeight;
	}
}
