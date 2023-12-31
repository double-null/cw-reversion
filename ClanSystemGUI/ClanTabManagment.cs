using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000105 RID: 261
	internal class ClanTabManagment : AbstractClanTab
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0003D5B8 File Offset: 0x0003B7B8
		public override void OnDrawButton()
		{
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabManagment, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansManagment, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				ClanSystemWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0003D614 File Offset: 0x0003B814
		public override void OnGUI()
		{
			this.page.OnGUI();
		}

		// Token: 0x040007B8 RID: 1976
		private IClanPage page = new ClanManagment();

		// Token: 0x040007B9 RID: 1977
		private GUIContent _name = new GUIContent(Language.ClansManagment);
	}
}
