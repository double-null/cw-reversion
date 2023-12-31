using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000319 RID: 793
	internal class ResultBar
	{
		// Token: 0x06001AEB RID: 6891 RVA: 0x000F3650 File Offset: 0x000F1850
		public ResultBar()
		{
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x000F3658 File Offset: 0x000F1858
		public ResultBar(Player p)
		{
			this.player = p;
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x000F3668 File Offset: 0x000F1868
		public float Height
		{
			get
			{
				return 27f;
			}
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000F3670 File Offset: 0x000F1870
		public void OnGUI(float x, float y, int index)
		{
			GUI.DrawTexture(new Rect(x + 2f, y, 348f, 25f), LeagueWindow.I.Textures.DarkGray);
			GUI.DrawTexture(new Rect(x + 352f, y, 420f, 25f), LeagueWindow.I.Textures.DarkGray);
			this.player.DrawFullPlayerInfo(x, y);
		}

		// Token: 0x04001FEE RID: 8174
		private int index;

		// Token: 0x04001FEF RID: 8175
		private Rect r;

		// Token: 0x04001FF0 RID: 8176
		private Player player;
	}
}
