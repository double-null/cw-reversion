using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000104 RID: 260
	internal class ClanTabWars : AbstractClanTab
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0003D344 File Offset: 0x0003B544
		public override void OnDrawButton()
		{
			if (GUI.Toggle(ClanSystemWindow.I.Tabs.clansTabWars, ClanSystemWindow.I.controller.SelectedTab == this, Language.ClansWars, ClanSystemWindow.I.Styles.styleClansRedTab))
			{
				if (this.current == null)
				{
					this.current = this.pageRace;
				}
				ClanSystemWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0003D3B8 File Offset: 0x0003B5B8
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(615f, 70f, 66f, 46f), ClanSystemWindow.I.Textures.redGlow);
			if (this.current == this.pageRace)
			{
				GUI.DrawTexture(new Rect(28f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (this.current == this.pageRating)
			{
				GUI.DrawTexture(new Rect(98f, 67f, 66f, 46f), ClanSystemWindow.I.Textures.whiteGlow);
			}
			if (GUI.Toggle(this.Race, this.current == this.pageRace, Language.ClansRaceBtn, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageRace;
			}
			GUI.enabled = false;
			GUI.enabled = true;
			if (GUI.Toggle(this.Rating, this.current == this.pageRating, Language.CarrRating, ClanSystemWindow.I.Styles.styleClansWhiteTab))
			{
				this.current = this.pageRating;
			}
			ClanRace clanRace = this.current as ClanRace;
			if (clanRace != null)
			{
				float num = 220f;
				float num2 = 60f;
				for (int i = 0; i < 4; i++)
				{
					float num3 = (float)i * (num2 * 1.2f);
					if (GUI.Toggle(new Rect(num + num3, 87f, num2, 23f), this._currentTopType == i, this._toptypes[i], ClanSystemWindow.I.Styles.styleClansWhiteTab))
					{
						this._currentTopType = i;
					}
				}
				clanRace.OnGUI(this._currentTopType);
			}
			else
			{
				this.current.OnGUI();
			}
		}

		// Token: 0x040007B0 RID: 1968
		private Rect Race = new Rect(25f, 55f, 73f, 25f);

		// Token: 0x040007B1 RID: 1969
		private Rect Rating = new Rect(95f, 55f, 73f, 25f);

		// Token: 0x040007B2 RID: 1970
		private IClanPage pageRace = new ClanRace();

		// Token: 0x040007B3 RID: 1971
		private IClanPage pageRating = new ClanOverview();

		// Token: 0x040007B4 RID: 1972
		private IClanPage current;

		// Token: 0x040007B5 RID: 1973
		private GUIContent _name = new GUIContent(Language.ClansWars);

		// Token: 0x040007B6 RID: 1974
		private int _currentTopType;

		// Token: 0x040007B7 RID: 1975
		private readonly string[] _toptypes = new string[]
		{
			"0 - 50",
			"51 - 150",
			"151 - 300",
			"301+"
		};
	}
}
