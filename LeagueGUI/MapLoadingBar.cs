using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000323 RID: 803
	internal class MapLoadingBar : IFrame
	{
		// Token: 0x06001B3C RID: 6972 RVA: 0x000F5DC8 File Offset: 0x000F3FC8
		public void OnStart()
		{
			Audio.Play(LeagueWindow.I.MatchFound);
			SearchFrame.Accepted = true;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000F5DE0 File Offset: 0x000F3FE0
		public void OnGUI()
		{
			LeagueHelpers.Twister();
			int num = (int)(Loader.Progress("GameData") * 100f);
			int num2 = CVars.LeagueLoadMapTimeout - (int)SearchFrame.Countdown.Time;
			string text = string.Concat(new object[]
			{
				Language.LeagueMapLoading,
				" ... [",
				num,
				"%] [",
				MainGUI.Instance.SecondsToStringMS(num2),
				"]"
			});
			if (num2 <= 0)
			{
				text = string.Concat(new object[]
				{
					Language.LeagueMapLoading,
					" ... [",
					num,
					"%] ",
					Language.LeagueGameStarted
				});
			}
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[0], text, LeagueWindow.I.Styles.BrownLabel28);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[7], Language.LeagueMap, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[8], Language.LeagueMode, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[9], ClientLeagueSystem.Map, LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[10], ClientLeagueSystem.Mode, LeagueWindow.I.Styles.WhiteLabel14L);
			this._countdownSound.Play(num2);
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000F5FA0 File Offset: 0x000F41A0
		public void OnUpdate()
		{
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000F5FA4 File Offset: 0x000F41A4
		public void Clear()
		{
		}

		// Token: 0x0400201E RID: 8222
		private CountdownSound _countdownSound = new CountdownSound();
	}
}
