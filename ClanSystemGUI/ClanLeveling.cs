using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010F RID: 271
	internal class ClanLeveling : AbstractClanPage
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x0004158C File Offset: 0x0003F78C
		public override void OnStart()
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00041590 File Offset: 0x0003F790
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(543f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x000415C0 File Offset: 0x0003F7C0
		public override void OnUpdate()
		{
		}
	}
}
