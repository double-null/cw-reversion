using System;
using ClanSystemGUI;
using UnityEngine;

namespace ConsoleGUI
{
	// Token: 0x0200006E RID: 110
	internal class ConsoleScrollListItem : IScrollListItem, IComparable
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00010CBC File Offset: 0x0000EEBC
		public ConsoleScrollListItem(LogEntry entry)
		{
			this.entry = entry;
			this.ButtonRect = new Rect(0f, 0f, this.Width, this.Height);
			this.TextFieldRect = new Rect(0f, 0f, this.Width, this.Height);
			this.OpenedHeight = 10f + SingletoneForm<global::Console>.Instance.TextStyle.CalcHeight(new GUIContent(entry.text), this.Width - 100f);
			this.HeaderHeight = 5f + SingletoneForm<global::Console>.Instance.HeadersStyle.CalcHeight(new GUIContent(entry.header), this.Width);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00010DAC File Offset: 0x0000EFAC
		public void OnGUI(float x, float y, int index)
		{
			this.ButtonRect.Set(5f + x, y, this.ButtonRect.width, this.HeaderHeight);
			if (this.entry.opened)
			{
				this.TextFieldRect.Set(20f + x, this.HeaderHeight + y, this.TextFieldRect.width, this.OpenedHeight);
				GUI.TextField(this.TextFieldRect, this.entry.text, SingletoneForm<global::Console>.Instance.TextStyle);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00010E3C File Offset: 0x0000F03C
		public float Width
		{
			get
			{
				return (float)(Screen.width - 50);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00010E48 File Offset: 0x0000F048
		public float Height
		{
			get
			{
				return (!this.entry.opened) ? this.HeaderHeight : (this.HeaderHeight + this.OpenedHeight);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010E80 File Offset: 0x0000F080
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x04000260 RID: 608
		private Rect ButtonRect = default(Rect);

		// Token: 0x04000261 RID: 609
		private Rect TextFieldRect = default(Rect);

		// Token: 0x04000262 RID: 610
		private LogEntry entry = new LogEntry();

		// Token: 0x04000263 RID: 611
		private float OpenedHeight;

		// Token: 0x04000264 RID: 612
		private float HeaderHeight = 30f;
	}
}
