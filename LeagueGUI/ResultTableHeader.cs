using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200031A RID: 794
	internal class ResultTableHeader
	{
		// Token: 0x06001AEF RID: 6895 RVA: 0x000F36E0 File Offset: 0x000F18E0
		public ResultTableHeader()
		{
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000F3700 File Offset: 0x000F1900
		public ResultTableHeader(bool IsBear, ref int winPoints, bool winner)
		{
			this.isBear = IsBear;
			this._winPoints = winPoints;
			this._winner = winner;
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x000F3734 File Offset: 0x000F1934
		public float Height
		{
			get
			{
				return 60f;
			}
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000F373C File Offset: 0x000F193C
		public void OnGUI(float x, float y, int index)
		{
			GUI.DrawTexture(new Rect(x, y, 780f, 35f), LeagueWindow.I.Textures.LightGray);
			GUI.DrawTexture(new Rect(x + 5f, y + 4f, 37f, 28f), (!this.isBear) ? LeagueWindow.I.Textures.TeamIcon[1] : LeagueWindow.I.Textures.TeamIcon[0]);
			GUI.Label(new Rect(x + 25f, y + 12f, 50f, 14f), (!this.isBear) ? "USEC" : "BEAR", LeagueWindow.I.Styles.WhiteLabel14);
			LeagueWindow.I.Styles.WhiteKorataki34.fontSize = 20;
			GUI.Label(new Rect(x + 750f, y + 17f, 0f, 0f), this._winPoints.ToString(), LeagueWindow.I.Styles.WhiteKorataki34);
			LeagueWindow.I.Styles.WhiteKorataki34.fontSize = 34;
			if (this._winner)
			{
				GUI.DrawTexture(new Rect(x + 705f, y + 2f, (float)this._tex[5].width, (float)this._tex[5].height), this._tex[5]);
			}
			GUI.Label(new Rect(x + 10f, y + 40f, 100f, 14f), Language.LeagueRank, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 47f, y + 40f, 100f, 14f), Language.LeagueRatingHeaderLvl, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 75f, y + 40f, 100f, 14f), Language.LeagueNickname, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 220f, y + 40f, 100f, 14f), Language.LeagueRatingHeaderLP, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 267f, y + 40f, 100f, 14f), Language.LeagueRatingHeaderWins, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 295f, y + 40f, 100f, 14f), Language.LeagueRatingHeaderDefeats, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 320f, y + 40f, 100f, 14f), Language.LeagueRatingHeaderLeaves, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 420f, y + 40f, 100f, 14f), Language.LeagueKills, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 475f, y + 40f, 100f, 14f), Language.LeagueDeaths, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 530f, y + 40f, 100f, 14f), Language.LeagueAssists, LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.Label(new Rect(x + 590f, y + 40f, 100f, 14f), "K/D", LeagueWindow.I.Styles.DarkGrayLabel);
			GUI.DrawTexture(new Rect(x + 375f, y + 42f, (float)this._tex[0].width, (float)this._tex[0].height), this._tex[0]);
			GUI.DrawTexture(new Rect(x + 630f, y + 32f, (float)this._tex[1].width, (float)this._tex[1].height), this._tex[1]);
			GUI.DrawTexture(new Rect(x + 660f, y + 33f, (float)this._tex[2].width, (float)this._tex[2].height), this._tex[2]);
			GUI.DrawTexture(new Rect(x + 690f, y + 33f, (float)this._tex[3].width, (float)this._tex[3].height), this._tex[3]);
			GUI.DrawTexture(new Rect(x + 720f, y + 34f, (float)this._tex[4].width, (float)this._tex[4].height), this._tex[4]);
		}

		// Token: 0x04001FF1 RID: 8177
		private Texture2D[] _tex = LeagueWindow.I.Textures.ResultTextures;

		// Token: 0x04001FF2 RID: 8178
		private bool isBear;

		// Token: 0x04001FF3 RID: 8179
		private int _winPoints;

		// Token: 0x04001FF4 RID: 8180
		private bool _winner;
	}
}
