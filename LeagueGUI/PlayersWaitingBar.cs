using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000324 RID: 804
	internal class PlayersWaitingBar : IFrame
	{
		// Token: 0x06001B41 RID: 6977 RVA: 0x000F5FBC File Offset: 0x000F41BC
		public void OnStart()
		{
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000F5FC0 File Offset: 0x000F41C0
		public void OnGUI()
		{
			LeagueHelpers.Twister();
			int num = CVars.LeagueLoadMapTimeout - (int)SearchFrame.Countdown.Time;
			string text = Language.LeagueWaitingPlayers + " ... [" + MainGUI.Instance.SecondsToStringMS(num) + "]";
			if (num <= 0)
			{
				text = Language.LeagueWaitingPlayers + " ... ";
			}
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[0], text, LeagueWindow.I.Styles.BrownLabel28);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[7], Language.LeagueMap, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[8], Language.LeagueMode, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[9], ClientLeagueSystem.Map, LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[10], ClientLeagueSystem.Mode, LeagueWindow.I.Styles.WhiteLabel14L);
			this._countdownSound.Play(num);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000F6124 File Offset: 0x000F4324
		public void OnUpdate()
		{
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000F6128 File Offset: 0x000F4328
		public void Clear()
		{
		}

		// Token: 0x0400201F RID: 8223
		private CountdownSound _countdownSound = new CountdownSound();
	}
}
