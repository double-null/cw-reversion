using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000101 RID: 257
	internal class ClanTabInfo : AbstractClanTab
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0003D01C File Offset: 0x0003B21C
		public override void OnDrawButton()
		{
			GUI.enabled = ClanTabInfo.showInfo;
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabInfo, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansInfo, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				ClanSystemWindow.I.controller.SetState(this);
			}
			GUI.enabled = true;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0003D088 File Offset: 0x0003B288
		public override void OnGUI()
		{
			this.page.OnGUI();
		}

		// Token: 0x040007A8 RID: 1960
		public static bool showInfo;

		// Token: 0x040007A9 RID: 1961
		private IClanPage page = new ClanDetailInfo();

		// Token: 0x040007AA RID: 1962
		private GUIContent _name = new GUIContent(Language.ClansInfo);
	}
}
