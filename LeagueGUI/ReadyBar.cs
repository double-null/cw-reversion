using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000322 RID: 802
	internal class ReadyBar : IFrame
	{
		// Token: 0x06001B37 RID: 6967 RVA: 0x000F5B70 File Offset: 0x000F3D70
		public void OnStart()
		{
			Audio.Play(LeagueWindow.I.MatchFound);
			this._green = LeagueWindow.I.Textures.OnlineSpot[0];
			this._red = LeagueWindow.I.Textures.OnlineSpot[1];
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000F5BBC File Offset: 0x000F3DBC
		public void OnGUI()
		{
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[5], Language.LeagueGameReady, LeagueWindow.I.Styles.BrownLabel28);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[1], Language.LeagueInQueue, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[2], Language.LeaguePlaying, LeagueWindow.I.Styles.BrownLabelR);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[3], ClientLeagueSystem.PlayersInQueue.ToString(), LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.SearchingRects[4], ClientLeagueSystem.PlayersInMatches.ToString(), LeagueWindow.I.Styles.WhiteLabel14L);
			if (SearchFrame.Accepted)
			{
				GUI.BeginGroup(new Rect((float)((800 - 20 * ClientLeagueSystem.PlayersTotal) / 2), 20f, (float)(20 * ClientLeagueSystem.PlayersTotal), (float)this._red.height));
				for (int i = 0; i < ClientLeagueSystem.PlayersTotal; i++)
				{
					if (i >= ClientLeagueSystem.PlayersTotal - ClientLeagueSystem.PlayersExpected)
					{
						GUI.DrawTexture(new Rect((float)(0 + 20 * i), 0f, (float)this._red.width, (float)this._red.height), this._red);
					}
					else
					{
						GUI.DrawTexture(new Rect((float)(0 + 20 * i), 0f, (float)this._green.width, (float)this._green.height), this._green);
					}
				}
				GUI.EndGroup();
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000F5DAC File Offset: 0x000F3FAC
		public void OnUpdate()
		{
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000F5DB0 File Offset: 0x000F3FB0
		public void Clear()
		{
		}

		// Token: 0x0400201A RID: 8218
		private float _prevTime;

		// Token: 0x0400201B RID: 8219
		private float _waitTime = 16f;

		// Token: 0x0400201C RID: 8220
		private Texture2D _green;

		// Token: 0x0400201D RID: 8221
		private Texture2D _red;
	}
}
