using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000314 RID: 788
	internal class TopFrame : AbstractFrame
	{
		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000F1F30 File Offset: 0x000F0130
		private Rect TopFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f, (float)(Screen.height - 600) * 0.5f, 800f, 120f);
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000F1F68 File Offset: 0x000F0168
		private bool SearchGameEnabled
		{
			get
			{
				return !LeagueWindow.I.LeagueInfo.Offseason && (!this._searchGameCooldown.IsStarted || this._searchGameCooldown.Time >= 10f || this.Searching);
			}
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x000F1FC0 File Offset: 0x000F01C0
		public override void OnStart()
		{
			this._player = new Player();
			this._logo = LeagueWindow.I.Textures.Logo;
			this._rArrow = LeagueWindow.I.Textures.Arrows[2];
			this._lArrow = LeagueWindow.I.Textures.Arrows[3];
			this._searchGameCooldown = new Timer();
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000F2028 File Offset: 0x000F0228
		public override void OnGUI()
		{
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), LeagueWindow.I.Textures.Black);
			GUI.BeginGroup(this.TopFrameRect);
			GUI.DrawTexture(new Rect((this.TopFrameRect.width - (float)this._logo.width) * 0.5f, -15f, (float)this._logo.width, (float)this._logo.height), this._logo);
			GUI.DrawTexture(LeagueWindow.I.Rects.LeftStripedBackTextureRect, LeagueWindow.I.Textures.StripedBack);
			GUI.DrawTexture(LeagueWindow.I.Rects.RightStripedBackTextureRect, LeagueWindow.I.Textures.StripedBack);
			GUI.enabled = this.SearchGameEnabled;
			if (SearchFrame.Accepted)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(LeagueWindow.I.Rects.SearchGameBtnRect, (!this.Searching) ? Language.LeagueSearchGame : Language.LeagueCancel, LeagueWindow.I.Styles.SearchGameBtnStyle))
			{
				if (this.Searching)
				{
					this.Searching = false;
					LeagueWindow.I.controller.SetState(LeagueWindow.I.controller.MainWindow);
					LeagueWindow.Timer.Stop();
					ClientLeagueSystem.CancelRequest();
				}
				else
				{
					this._searchGameCooldown.Start();
					this.Searching = true;
					ClientLeagueSystem.SendRequest();
				}
			}
			if (this.SearchGameEnabled && GUI.enabled)
			{
				if (this.Searching)
				{
					GUI.DrawTexture(new Rect(25f, 21f, (float)this._lArrow.width, (float)this._lArrow.height), this._lArrow);
				}
				else
				{
					GUI.DrawTexture(new Rect(285f, 21f, (float)this._rArrow.width, (float)this._rArrow.height), this._rArrow);
				}
			}
			GUI.enabled = true;
			if (LeagueWindow.I.LeagueInfo.Offseason)
			{
				GUI.Label(LeagueWindow.I.Rects.SeasonEndLabelRect, Language.LeagueSeasonBreak, LeagueWindow.I.Styles.BrownLabel);
				string text;
				if (LeagueWindow.I.LeagueInfo.Offseason)
				{
					text = MainGUI.Instance.SecondsTostringDDHHMMSS(LeagueWindow.I.LeagueInfo.NextSeasonStart - HtmlLayer.serverUtc);
				}
				else
				{
					text = MainGUI.Instance.SecondsTostringDDHHMMSS(LeagueWindow.I.LeagueInfo.SeasonCloseDate - HtmlLayer.serverUtc);
				}
				GUI.Button(LeagueWindow.I.Rects.TimerBackTextureRect, text, LeagueWindow.I.Styles.TimerBreak);
			}
			else
			{
				GUI.Label(LeagueWindow.I.Rects.SeasonEndLabelRect, Language.LeagueSeasonEnd, LeagueWindow.I.Styles.BrownLabel);
				string text2 = MainGUI.Instance.SecondsTostringDDHHMMSS(LeagueWindow.I.LeagueInfo.SeasonCloseDate - HtmlLayer.serverUtc);
				GUI.Button(LeagueWindow.I.Rects.TimerBackTextureRect, text2, LeagueWindow.I.Styles.Timer);
			}
			GUI.DrawTexture(LeagueWindow.I.Rects.BoosterTextureRect, LeagueWindow.I.Textures.Booster);
			GUI.Label(LeagueWindow.I.Rects.BoosterLabelRect, Language.LeagueBoosters.ToUpper(), LeagueWindow.I.Styles.BlackLabel);
			float width = (float)CarrierGUI.I.Class_skills[0].width;
			float height = (float)CarrierGUI.I.Class_skills[0].height;
			for (byte b = 0; b < 2; b += 1)
			{
				GUI.DrawTexture(new Rect((float)(130 + b * 80), 57f, width, height), CarrierGUI.I.Class_skills[(int)(153 + b)]);
				if (Main.UserInfo.skillUnlocked(Skills.lp_gain))
				{
					GUI.DrawTexture(new Rect(130f, 57f, width, height), CarrierGUI.I.Class_skill_button[0]);
				}
				if (Main.UserInfo.skillUnlocked(Skills.lp_protect))
				{
					GUI.DrawTexture(new Rect(210f, 57f, width, height), CarrierGUI.I.Class_skill_button[0]);
				}
			}
			this._player.DrawAvatar(470f, 5f);
			this._player.DrawPlayerInfo(470f, 5f);
			this._player.DrawPlayerStats(470f, 5f);
			if (SearchFrame.Accepted)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(LeagueWindow.I.Rects.CloseBtnRect, string.Empty, LeagueWindow.I.Styles.CloseBtnStyle))
			{
				LeagueWindow.I.Enabled = false;
				if (this.Searching)
				{
					this.Searching = false;
					LeagueWindow.Timer.Stop();
					ClientLeagueSystem.CancelRequest();
				}
				LeagueWindow.I.controller.SetState(LeagueWindow.I.controller.MainWindow);
			}
			GUI.enabled = true;
			GUI.EndGroup();
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x000F2574 File Offset: 0x000F0774
		public override void OnUpdate()
		{
			if (LeagueWindow.I.controller.CurrentWindow != LeagueWindow.I.controller.MatchmakeWindow)
			{
				this.Searching = false;
			}
			if (TopFrame.RefreshPlayerData)
			{
				this._player.RefreshData();
				TopFrame.RefreshPlayerData = false;
			}
		}

		// Token: 0x04001FD8 RID: 8152
		public bool Searching;

		// Token: 0x04001FD9 RID: 8153
		public static bool RefreshPlayerData;

		// Token: 0x04001FDA RID: 8154
		private bool _isResult;

		// Token: 0x04001FDB RID: 8155
		private Texture2D _logo;

		// Token: 0x04001FDC RID: 8156
		private Texture2D _rArrow;

		// Token: 0x04001FDD RID: 8157
		private Texture2D _lArrow;

		// Token: 0x04001FDE RID: 8158
		private Player _player;

		// Token: 0x04001FDF RID: 8159
		private Timer _searchGameCooldown;
	}
}
