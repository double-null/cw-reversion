using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x0200010D RID: 269
	internal class ClanOverview : AbstractClanPage
	{
		// Token: 0x0600071A RID: 1818 RVA: 0x0003F70C File Offset: 0x0003D90C
		private void GenerateList()
		{
			ClanSystemWindow.I.Lists.ClanList.Clear();
			for (int i = 0; i < Main.UserInfo.clanData.clanShortInfoList.Length; i++)
			{
				ClanSystemWindow.I.Lists.ClanList.Add(new ClanInfoBar(Main.UserInfo.clanData.clanShortInfoList[i]));
			}
			ClanSystemWindow.I.Lists.ClanList.Sort();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0003F790 File Offset: 0x0003D990
		public override void OnStart()
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0003F794 File Offset: 0x0003D994
		public override void OnGUI()
		{
			string text = base.Search();
			if (!this.refresh || !string.IsNullOrEmpty(text))
			{
				this.refresh = true;
				Main.AddDatabaseRequestCallBack<ListClan>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					text
				});
			}
			GUI.DrawTexture(new Rect(25f, 81f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			if (this.reupdateTimer < Time.realtimeSinceStartup && GUI.Button(new Rect(653f, 83f, 28f, 23f), string.Empty, ClanSystemWindow.I.Styles.styleJoinRefreshBtn))
			{
				this.reupdateTimer = Time.realtimeSinceStartup + 5f;
				this.refresh = false;
			}
			GUI.DrawTexture(new Rect(658f, 85f, (float)ClanSystemWindow.I.Textures.refreshIcon.width, (float)ClanSystemWindow.I.Textures.refreshIcon.height), ClanSystemWindow.I.Textures.refreshIcon);
			GUI.Label(new Rect(65f, 81f, 100f, 25f), Language.CarrPlace, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(130f, 81f, 100f, 25f), Language.CarrTAG, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(165f, 81f, 100f, 25f), Language.CarrName, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(348f, 81f, 100f, 25f), Language.ClansLead, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(445f, 81f, 100f, 25f), Language.CarrPlayers, ClanSystemWindow.I.Styles.styleGrayLabel);
			GUI.Label(new Rect(510f, 81f, 150f, 25f), Language.CarrEXP, ClanSystemWindow.I.Styles.styleGrayLabel);
			ClanSystemWindow.I.Lists.ClanList.OnGUI();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0003FA18 File Offset: 0x0003DC18
		public override void OnUpdate()
		{
		}

		// Token: 0x040007F0 RID: 2032
		private bool refresh;

		// Token: 0x040007F1 RID: 2033
		private float reupdateTimer;
	}
}
