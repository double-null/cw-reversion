using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000109 RID: 265
	internal class ClanDetailInfo : AbstractClanPage
	{
		// Token: 0x060006F4 RID: 1780 RVA: 0x0003DB58 File Offset: 0x0003BD58
		public override void OnStart()
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0003DB5C File Offset: 0x0003BD5C
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(403f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			this.info.OnGUI();
			ClanSystemWindow.I.Lists.InfoList.OnGUI();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0003DBB8 File Offset: 0x0003BDB8
		public override void OnUpdate()
		{
		}

		// Token: 0x040007C6 RID: 1990
		private CurrentClanInfo info = new CurrentClanInfo();
	}
}
