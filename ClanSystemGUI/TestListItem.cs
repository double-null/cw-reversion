using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200011E RID: 286
	internal class TestListItem : IScrollListItem, IComparable
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x00047B28 File Offset: 0x00045D28
		public float Width
		{
			get
			{
				return 450f;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00047B30 File Offset: 0x00045D30
		public float Height
		{
			get
			{
				return 20f;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00047B38 File Offset: 0x00045D38
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00047B3C File Offset: 0x00045D3C
		public void OnGUI(float x, float y, int index)
		{
			Graphics.DrawTexture(new Rect(x, y, this.Width, 10f), MainGUI.Instance.red);
			Graphics.DrawTexture(new Rect(x, y + 10f, this.Width, 10f), MainGUI.Instance.blue);
		}
	}
}
