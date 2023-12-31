using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000325 RID: 805
	internal class MatchStartBar : IFrame
	{
		// Token: 0x06001B46 RID: 6982 RVA: 0x000F614C File Offset: 0x000F434C
		public void OnStart()
		{
			this._countdown.Start();
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000F615C File Offset: 0x000F435C
		public void OnGUI()
		{
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[5], Language.LeagueMatchStart, LeagueWindow.I.Styles.BrownLabel28);
			LeagueWindow.I.Styles.WhiteLabel20.fontSize = 36;
			int num = CVars.LeagueAllReadyStartTimeout - (int)this._countdown.Time;
			if (num <= 0)
			{
				num = 0;
			}
			GUI.Label(LeagueWindow.I.Rects.StartTimerRect, MainGUI.Instance.SecondsToStringMS(num), LeagueWindow.I.Styles.WhiteLabel20);
			LeagueWindow.I.Styles.WhiteLabel20.fontSize = 20;
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[7], Language.LeagueMap, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[8], Language.LeagueMode, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[9], ClientLeagueSystem.Map, LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[10], ClientLeagueSystem.Mode, LeagueWindow.I.Styles.WhiteLabel14L);
			this._countdownSound.Play(num);
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000F62EC File Offset: 0x000F44EC
		public void OnUpdate()
		{
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000F62F0 File Offset: 0x000F44F0
		public void Clear()
		{
		}

		// Token: 0x04002020 RID: 8224
		private Timer _countdown = new Timer();

		// Token: 0x04002021 RID: 8225
		private CountdownSound _countdownSound = new CountdownSound();
	}
}
