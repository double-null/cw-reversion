using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000316 RID: 790
	internal class ResultHeaderFrame : AbstractFrame
	{
		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001ADF RID: 6879 RVA: 0x000F2D24 File Offset: 0x000F0F24
		private Rect HeaderRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f + 4f, (float)(Screen.height - 600) * 0.5f + 112f, 792f, 78f);
			}
		}

		// Token: 0x06001AE0 RID: 6880 RVA: 0x000F2D70 File Offset: 0x000F0F70
		public override void OnStart()
		{
			this.InitRect();
			if (Peer.ClientGame)
			{
				if (Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.win = (Peer.ClientGame.BearWinCount > Peer.ClientGame.UsecWinCount);
				}
				else
				{
					this.win = (Peer.ClientGame.BearWinCount < Peer.ClientGame.UsecWinCount);
				}
				Debug.Log(Peer.ClientGame.BearWinCount + " " + Peer.ClientGame.UsecWinCount);
				this.tie = (Peer.ClientGame.BearWinCount == Peer.ClientGame.UsecWinCount);
			}
			this._points.Awake();
			this._glow = LeagueWindow.I.Textures.BlueGlow;
			if (!this.win || this.tie)
			{
				this._glow = LeagueWindow.I.Textures.RedGlow;
			}
			if (this.win)
			{
				this._points.Points = LeagueWindow.I.LeagueInfo.WinPoint.ToString();
				if (Main.UserInfo.skillUnlocked(Skills.lp_gain))
				{
					this._points.Points = "+" + (LeagueWindow.I.LeagueInfo.WinPoint + 5);
				}
			}
			else
			{
				this._points.Points = LeagueWindow.I.LeagueInfo.LosePoint.ToString();
				if (Main.UserInfo.skillUnlocked(Skills.lp_protect))
				{
					this._points.Points = "-" + (LeagueWindow.I.LeagueInfo.LosePoint + 5);
				}
			}
			if (this.tie)
			{
				this._points.Points = "0";
			}
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000F2F60 File Offset: 0x000F1160
		public override void OnGUI()
		{
			string text = (!this.win) ? Language.LeagueYourTeamLose : Language.LeagueYourTeamWon;
			if (this.tie)
			{
				text = Language.LeagueTie;
			}
			GUIStyle style = (!this.win) ? LeagueWindow.I.Styles.RedLabel28 : LeagueWindow.I.Styles.BlueLabel28;
			if (this.tie)
			{
				style = LeagueWindow.I.Styles.RedLabel28;
			}
			GUI.BeginGroup(this.HeaderRect);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleLeft;
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[1], Language.LeagueGoneGameResult, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[2], text, style);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleRight;
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[5], Language.LeagueMap, LeagueWindow.I.Styles.BrownLabel);
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[6], Language.LeagueMode, LeagueWindow.I.Styles.BrownLabel);
			LeagueWindow.I.Styles.BrownLabel.alignment = TextAnchor.MiddleCenter;
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[3], ClientLeagueSystem.Map, LeagueWindow.I.Styles.WhiteLabel14L);
			GUI.Label(LeagueWindow.I.Rects.ResultHeaderRects[4], ClientLeagueSystem.Mode, LeagueWindow.I.Styles.WhiteLabel14L);
			if (GUI.Button(LeagueWindow.I.Rects.ResultHeaderRects[8], Language.LeagueNext, LeagueWindow.I.Styles.LongBtnStyle))
			{
				LeagueWindow.I.controller.SetState(LeagueWindow.I.controller.MainWindow);
			}
			GUI.DrawTexture(new Rect(775f, 48f, (float)LeagueWindow.I.Textures.Arrows[1].width, (float)LeagueWindow.I.Textures.Arrows[1].height), LeagueWindow.I.Textures.Arrows[1]);
			GUI.DrawTexture(new Rect((this.HeaderRect.width - (float)this._glow.width) / 2f, 0f, (float)this._glow.width, (float)this._glow.height), this._glow);
			this._points.OnGUI();
			GUI.EndGroup();
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000F3254 File Offset: 0x000F1454
		private void InitRect()
		{
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.ResultHeaderRects[7], LeagueWindow.I.Styles.LongBtnStyle);
			LeagueWindow.I.InitRect(ref LeagueWindow.I.Rects.ResultHeaderRects[8], LeagueWindow.I.Styles.LongBtnStyle);
		}

		// Token: 0x04001FE6 RID: 8166
		private bool win;

		// Token: 0x04001FE7 RID: 8167
		private bool tie;

		// Token: 0x04001FE8 RID: 8168
		private ResultHeaderFrame.ShowPoints _points = new ResultHeaderFrame.ShowPoints();

		// Token: 0x04001FE9 RID: 8169
		private Texture2D _glow;

		// Token: 0x02000317 RID: 791
		private class ShowPoints
		{
			// Token: 0x06001AE4 RID: 6884 RVA: 0x000F32D4 File Offset: 0x000F14D4
			public void Awake()
			{
				this._alpha = new CurveEval(LeagueWindow.I.AlphaCurve);
				this._timer = new Timer();
				this._tempColor = GUI.color;
			}

			// Token: 0x06001AE5 RID: 6885 RVA: 0x000F3304 File Offset: 0x000F1504
			public void OnGUI()
			{
				if (!this._timer.IsStarted)
				{
					this._timer.Start();
				}
				if (this._timer.Time > 5f)
				{
					if (this._alpha.Value >= 1f)
					{
						this._alpha.Start(true);
					}
					else
					{
						this._alpha.Start(false);
					}
					this._timer.Start();
				}
				GUI.color = new Color(1f, 1f, 1f, this._alpha.Value);
				GUI.Label(new Rect(0f, -10f, 792f, 78f), this.Points, LeagueWindow.I.Styles.WhiteKorataki34);
				GUI.Label(new Rect(0f + MainGUI.Instance.CalcWidth("1", MainGUI.Instance.fontMicra, 34) / 2f, 15f, 792f, 78f), "LEAGUE POINTS", LeagueWindow.I.Styles.WhiteLabel14);
				GUI.color = new Color(1f, 1f, 1f, 1f - this._alpha.Value);
				string text = (LeagueHelpers.GetNextRank(LeagueWindow.I.LeagueInfo.UserLp) - LeagueWindow.I.LeagueInfo.UserLp).ToString();
				GUI.Label(new Rect(0f, -10f, 792f, 78f), text, LeagueWindow.I.Styles.WhiteKorataki34);
				GUI.Label(new Rect(-11f, 15f, 792f, 78f), Language.LeaguePointLeft, LeagueWindow.I.Styles.WhiteLabel14);
				LeagueHelpers.DrawRank(430f, 43f, LeagueHelpers.GetNextRank(LeagueWindow.I.LeagueInfo.UserLp), 1f - this._alpha.Value, false);
				GUI.color = this._tempColor;
			}

			// Token: 0x04001FEA RID: 8170
			private CurveEval _alpha;

			// Token: 0x04001FEB RID: 8171
			private Timer _timer;

			// Token: 0x04001FEC RID: 8172
			private Color _tempColor;

			// Token: 0x04001FED RID: 8173
			public string Points = string.Empty;
		}
	}
}
