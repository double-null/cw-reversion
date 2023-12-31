using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000106 RID: 262
	internal class ClanTabLeveling : AbstractClanTab
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x0003D6F0 File Offset: 0x0003B8F0
		public override void OnDrawButton()
		{
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabLeveling, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansLeveling, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				if (this.current == null)
				{
					this.current = this.pageSkills;
				}
				ClanSystemWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0003D764 File Offset: 0x0003B964
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(543f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			if (this.current == this.pageSkills)
			{
				GUI.DrawTexture(new Rect(28f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (this.current == this.pageArmory)
			{
				GUI.DrawTexture(new Rect(98f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (this.current == this.pageCamouflage)
			{
				GUI.DrawTexture(new Rect(168f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (this.current == this.pageHistory)
			{
				GUI.DrawTexture(new Rect(238f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (GUI.Toggle(this.skillsRect, this.current == this.pageSkills, Language.CarrSkills, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageSkills;
			}
			GUI.enabled = false;
			if (GUI.Toggle(this.armoryRect, this.current == this.pageArmory, Language.ClansArmoryBtn, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageArmory;
			}
			if (GUI.Toggle(this.camouflageRect, this.current == this.pageCamouflage, Language.ClansCamouflageBtn, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageCamouflage;
			}
			GUI.enabled = true;
			if (GUI.Toggle(this.historyRect, this.current == this.pageHistory, Language.ClansHistory, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageHistory;
			}
			this.current.OnGUI();
		}

		// Token: 0x040007BA RID: 1978
		private Rect skillsRect = new Rect(25f, 55f, 73f, 25f);

		// Token: 0x040007BB RID: 1979
		private Rect armoryRect = new Rect(95f, 55f, 73f, 25f);

		// Token: 0x040007BC RID: 1980
		private Rect camouflageRect = new Rect(165f, 55f, 73f, 25f);

		// Token: 0x040007BD RID: 1981
		private Rect historyRect = new Rect(235f, 55f, 73f, 25f);

		// Token: 0x040007BE RID: 1982
		private IClanPage pageSkills = new ClanSkills();

		// Token: 0x040007BF RID: 1983
		private IClanPage pageArmory = new ClanArmory();

		// Token: 0x040007C0 RID: 1984
		private IClanPage pageCamouflage = new ClanCamouflage();

		// Token: 0x040007C1 RID: 1985
		private IClanPage pageHistory = new ClanHistory();

		// Token: 0x040007C2 RID: 1986
		private IClanPage current;

		// Token: 0x040007C3 RID: 1987
		private bool reqestSended;

		// Token: 0x040007C4 RID: 1988
		private GUIContent _name = new GUIContent(Language.ClansLeveling);
	}
}
