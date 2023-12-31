using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000102 RID: 258
	internal class ClanTabCreate : AbstractClanTab
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0003D0BC File Offset: 0x0003B2BC
		public override void OnDrawButton()
		{
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabCreate, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansCreate, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				if (ClanSystemWindow.I.controller.SelectedTab != this)
				{
					this.page.Clear();
				}
				ClanSystemWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0003D138 File Offset: 0x0003B338
		public override void OnGUI()
		{
			this.page.OnGUI();
		}

		// Token: 0x040007AB RID: 1963
		private IClanPage page = new ClanCreate();

		// Token: 0x040007AC RID: 1964
		private GUIContent _name = new GUIContent(Language.ClansCreate);
	}
}
