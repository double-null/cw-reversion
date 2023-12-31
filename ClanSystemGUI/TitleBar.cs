using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000118 RID: 280
	internal class TitleBar : IScrollListItem, IComparable
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00046868 File Offset: 0x00044A68
		public TitleBar(string str)
		{
			this.str = str;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00046878 File Offset: 0x00044A78
		public float Width
		{
			get
			{
				return 660f;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x00046880 File Offset: 0x00044A80
		public float Height
		{
			get
			{
				return 25f;
			}
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00046888 File Offset: 0x00044A88
		public int CompareTo(object obj)
		{
			return 0;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0004688C File Offset: 0x00044A8C
		public void OnGUI(float x, float y, int index)
		{
			GUI.DrawTexture(new Rect(25f, y + 20f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			GUI.Label(new Rect(25f, y + 20f, 660f, 25f), this.str, ClanSystemWindow.I.Styles.styleGrayLabel14);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00046900 File Offset: 0x00044B00
		public void SetStr(string str)
		{
			this.str = str;
		}

		// Token: 0x0400082E RID: 2094
		private string str;
	}
}
