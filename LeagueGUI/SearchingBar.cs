using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000321 RID: 801
	internal class SearchingBar : IFrame
	{
		// Token: 0x06001B32 RID: 6962 RVA: 0x000F5A10 File Offset: 0x000F3C10
		public void OnStart()
		{
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000F5A14 File Offset: 0x000F3C14
		public void OnGUI()
		{
			LeagueHelpers.Twister();
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[0], Language.LeagueSearching + " ... [" + MainGUI.Instance.SecondsToStringMSS((int)LeagueWindow.Timer.Time) + "]", LeagueWindow.I.Styles.BrownLabel28);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[1], Language.LeagueInQueue, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[2], Language.LeaguePlaying, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[3], ClientLeagueSystem.PlayersInQueue.ToString(), LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[4], ClientLeagueSystem.PlayersInMatches.ToString(), LeagueWindow.I.Styles.WhiteLabel14L);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000F5B54 File Offset: 0x000F3D54
		public void OnUpdate()
		{
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000F5B58 File Offset: 0x000F3D58
		public void Clear()
		{
		}
	}
}
