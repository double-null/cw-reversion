using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000103 RID: 259
	internal class ClanTabJoin : AbstractClanTab
	{
		// Token: 0x060006DC RID: 1756 RVA: 0x0003D160 File Offset: 0x0003B360
		private void GenerateList()
		{
			ClanSystemWindow.I.Lists.ClanList.Clear();
			for (int i = 0; i < Main.UserInfo.clanData.clanShortInfoList.Length; i++)
			{
				ClanSystemWindow.I.Lists.ClanList.Add(new ClanInfoBar(Main.UserInfo.clanData.clanShortInfoList[i]));
			}
			ClanSystemWindow.I.Lists.ClanList.Sort();
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0003D1E4 File Offset: 0x0003B3E4
		public override void OnDrawButton()
		{
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabJoin, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansJoin, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				ClanSystemWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0003D240 File Offset: 0x0003B440
		public override void OnGUI()
		{
			if (!ClanTabJoin._dataRequest)
			{
				ClanTabJoin._dataRequest = true;
				Main.AddDatabaseRequestCallBack<ListClan>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[0]);
			}
			this.page.OnGUI();
		}

		// Token: 0x040007AD RID: 1965
		public static bool _dataRequest;

		// Token: 0x040007AE RID: 1966
		private IClanPage page = new ClanJoin();
	}
}
