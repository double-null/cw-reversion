using System;
using UnityEngine;

// Token: 0x020000DF RID: 223
internal class KillsCounter
{
	// Token: 0x060005E3 RID: 1507 RVA: 0x0002D6C4 File Offset: 0x0002B8C4
	public KillsCounter(PlayerInfo info)
	{
		this.info = info;
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0002D6D4 File Offset: 0x0002B8D4
	public void DrawKills(float x, float y)
	{
		MainGUI.Instance.TextLabel(new Rect(x, y, 50f, 22f), this.info.killCount, 20, "#FFFFFF_T", TextAnchor.MiddleCenter, true);
	}

	// Token: 0x040005CE RID: 1486
	private PlayerInfo info;
}
