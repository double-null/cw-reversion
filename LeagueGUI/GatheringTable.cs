using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200032A RID: 810
	internal class GatheringTable : AbstractFrame
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x000F6D84 File Offset: 0x000F4F84
		public override void OnGUI()
		{
			this.DrawHeaders();
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x000F6D8C File Offset: 0x000F4F8C
		private void DrawHeaders()
		{
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[0], Language.LeagueRank, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[1], Language.LeagueRatingHeaderLvl, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[2], Language.LeagueRatingHeaderNameNick, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[3], Language.LeagueRatingHeaderLP, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[4], Language.LeagueRatingHeaderWins, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[5], Language.LeagueRatingHeaderDefeats, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(LeagueWindow.I.Rects.GatheringHeaderRects[6], Language.LeagueRatingHeaderLeaves, LeagueWindow.I.Styles.DarkGrayLabel);
		}
	}
}
