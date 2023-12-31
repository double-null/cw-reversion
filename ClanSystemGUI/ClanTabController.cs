using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x020000FE RID: 254
	internal class ClanTabController
	{
		// Token: 0x060006C1 RID: 1729 RVA: 0x0003CE8C File Offset: 0x0003B08C
		public ClanTabController()
		{
			this.TabInfo = new ClanTabInfo();
			this.TabCreate = new ClanTabCreate();
			this.TabJoin = new ClanTabJoin();
			this.TabWars = new ClanTabWars();
			this.TabManagment = new ClanTabManagment();
			this.TabLeveling = new ClanTabLeveling();
			this._currentTab = this.TabInfo;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		public IClanTab SelectedTab
		{
			get
			{
				return this._currentTab;
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0003CEF8 File Offset: 0x0003B0F8
		public void OnStart()
		{
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0003CEFC File Offset: 0x0003B0FC
		public void SetState(IClanTab state)
		{
			this._currentTab = state;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0003CF08 File Offset: 0x0003B108
		public IClanTab GetState()
		{
			return this._currentTab;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0003CF10 File Offset: 0x0003B110
		public void OnUpdate()
		{
			if (this._currentTab != null)
			{
				this._currentTab.OnUpdate();
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003CF28 File Offset: 0x0003B128
		public void OnGUI()
		{
			GUI.DrawTexture(new Rect(25f, 55f, 660f, 25f), ClanSystemWindow.I.Textures.narrowStripe);
			if (this._currentTab != null)
			{
				this._currentTab.OnGUI();
			}
			this.TabInfo.OnDrawButton();
			if (Main.UserInfo.clanID == 0)
			{
				this.TabJoin.OnDrawButton();
				this.TabCreate.OnDrawButton();
			}
			else
			{
				this.TabManagment.OnDrawButton();
				this.TabLeveling.OnDrawButton();
			}
			this.TabWars.OnDrawButton();
		}

		// Token: 0x0400079E RID: 1950
		private IClanTab _currentTab;

		// Token: 0x0400079F RID: 1951
		public IClanTab TabInfo;

		// Token: 0x040007A0 RID: 1952
		public IClanTab TabCreate;

		// Token: 0x040007A1 RID: 1953
		public IClanTab TabJoin;

		// Token: 0x040007A2 RID: 1954
		public IClanTab TabWars;

		// Token: 0x040007A3 RID: 1955
		public IClanTab TabManagment;

		// Token: 0x040007A4 RID: 1956
		public IClanTab TabLeveling;

		// Token: 0x040007A5 RID: 1957
		public IClanTab TabWars_Race;

		// Token: 0x040007A6 RID: 1958
		public IClanTab TabWars_Wars;
	}
}
