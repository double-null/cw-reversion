using System;
using ClanSystemGUI;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000306 RID: 774
	[Serializable]
	internal class LeagueWindow
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x000F031C File Offset: 0x000EE51C
		public LeagueWindow()
		{
			LeagueWindow.I = this;
			this.controller = new LeagueWindowController();
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000F0398 File Offset: 0x000EE598
		public void InitRect(ref Rect rect, GUIStyle style)
		{
			rect.Set(rect.x, rect.y, (float)style.normal.background.width, (float)style.normal.background.height);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000F03DC File Offset: 0x000EE5DC
		public void InitRect(ref Rect rect, Texture2D texture)
		{
			rect.Set(rect.x, rect.y, (float)texture.width, (float)texture.height);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000F040C File Offset: 0x000EE60C
		public void Start()
		{
			this.Enabled = true;
			this.InitRect(ref this.Rects.CloseBtnRect, this.Styles.CloseBtnStyle);
			this.InitRect(ref this.Rects.SearchGameBtnRect, this.Styles.SearchGameBtnStyle);
			this.InitRect(ref this.Rects.BoosterTextureRect, this.Textures.Booster);
			this.InitRect(ref this.Rects.RightStripedBackTextureRect, this.Textures.StripedBack);
			this.InitRect(ref this.Rects.TimerBackTextureRect, this.Textures.TimerBack);
			this.InitRect(ref this.Rects.TimerBackTextureRect, this.Styles.Timer);
			this.InitRect(ref this.Rects.TwisterTextureRect, this.Textures.Twister);
			this.InitRect(ref this.Rects.SearchingRects[6], this.Styles.AcceptBtn);
			this.InitRect(ref this.Rects.TeamIconTextureRects[0], this.Textures.TeamIcon[0]);
			this.InitRect(ref this.Rects.TeamIconTextureRects[1], this.Textures.TeamIcon[1]);
			this.InitRect(ref this.Rects.ResultHeaderRects[0], this.Textures.BlueGlow);
			this.topFrame.OnStart();
			this.controller.OnStart();
			this.controller.SetState(this.controller.MainWindow);
			this.LeagueAlpha = new Alpha();
			ClientLeagueSystem.OnCanelGame = new ClientLeagueSystem.LegueAction(this.OnCancelGame);
			ClientLeagueSystem.OnJoinGame = new ClientLeagueSystem.LegueAction(this.OnJoinGame);
			ClientLeagueSystem.OnMatchEnd = new ClientLeagueSystem.LegueAction(this.OnMatchEnd);
			ClientLeagueSystem.OnReadyGame = new ClientLeagueSystem.LegueAction(this.OnReadyGame);
			ClientLeagueSystem.OnSendRequest = new ClientLeagueSystem.LegueAction(this.OnAcceptRequest);
			ClientLeagueSystem.OnCancelRequest = new ClientLeagueSystem.LegueAction(this.OnCancelRequest);
			ClientLeagueSystem.OnWaitingPlayers = new ClientLeagueSystem.LegueAction(this.OnWaitingPlayers);
			ClientLeagueSystem.OnMapLoding = new ClientLeagueSystem.LegueAction(this.OnMapLoading);
			ClientLeagueSystem.OnMatchStating = new ClientLeagueSystem.LegueAction(this.OnMatchStarting);
			this.controller.DownloadRankPoints();
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000F064C File Offset: 0x000EE84C
		public void OnGUI()
		{
			if (!this.LeagueAlpha.Visible && this.Enabled)
			{
				this.LeagueAlpha.Show(0.5f, 0f);
			}
			Color color = GUI.color;
			GUI.color = Colors.alpha(GUI.color, this.LeagueAlpha.visibility);
			this.topFrame.OnGUI();
			GUISkin guiskin = GUI.skin;
			GUI.skin = LeagueWindow.I.skin;
			this.controller.OnGUI();
			GUI.skin = guiskin;
			GUI.color = color;
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000F06E0 File Offset: 0x000EE8E0
		public void OnUpdate()
		{
			this.topFrame.OnUpdate();
			this.controller.OnUpdate();
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000F06F8 File Offset: 0x000EE8F8
		private void OnReadyGame()
		{
			this.controller.CurrentWindow.OnReadyGame();
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000F070C File Offset: 0x000EE90C
		public void Reset()
		{
			this.topFrame.Searching = false;
			this.topFrame.OnStart();
			this.controller.OnStart();
			this.controller.SetState(this.controller.MainWindow);
			this.LeagueAlpha = new Alpha();
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000F075C File Offset: 0x000EE95C
		private void OnCancelGame()
		{
			this.topFrame.Searching = false;
			this.controller.CurrentWindow.OnCancelGame();
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000F077C File Offset: 0x000EE97C
		private void OnJoinGame()
		{
			LeagueWindow.I.Enabled = false;
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000F078C File Offset: 0x000EE98C
		private void OnMatchEnd()
		{
			this.controller.SetState(this.controller.ResultWindow);
			Main.AddDatabaseRequestCallBack<LoadProfileInfo>(delegate
			{
				TopFrame.RefreshPlayerData = true;
			}, delegate
			{
			}, new object[0]);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000F07F4 File Offset: 0x000EE9F4
		private void OnAcceptRequest()
		{
			this.topFrame.Searching = true;
			this.controller.SetState(LeagueWindow.I.controller.MatchmakeWindow);
			this.controller.MatchmakeWindow.OnStart();
			LeagueWindow.Timer.Start();
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000F0844 File Offset: 0x000EEA44
		private void OnCancelRequest()
		{
			if (SearchFrame.Accepted)
			{
				this.topFrame.Searching = true;
				ClientLeagueSystem.SendRequest();
			}
			else
			{
				this.topFrame.Searching = false;
				this.controller.SetState(this.controller.MainWindow);
			}
			SearchFrame.Accepted = false;
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000F089C File Offset: 0x000EEA9C
		private void OnWaitingPlayers()
		{
			this.controller.CurrentWindow.OnWaitingPlayers();
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000F08B0 File Offset: 0x000EEAB0
		private void OnMapLoading()
		{
			this.controller.CurrentWindow.OnMapLoading();
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000F08C4 File Offset: 0x000EEAC4
		private void OnMatchStarting()
		{
			this.controller.CurrentWindow.OnMatchStarting();
		}

		// Token: 0x04001F45 RID: 8005
		[HideInInspector]
		public LeagueRatingInfo[] LeagueRatingInfo;

		// Token: 0x04001F46 RID: 8006
		public static LeagueWindow I = null;

		// Token: 0x04001F47 RID: 8007
		public static Timer Timer = new Timer();

		// Token: 0x04001F48 RID: 8008
		public GUISkin skin;

		// Token: 0x04001F49 RID: 8009
		public LeagueInfo LeagueInfo;

		// Token: 0x04001F4A RID: 8010
		public LeagueWindowController controller;

		// Token: 0x04001F4B RID: 8011
		public TopFrame topFrame = new TopFrame();

		// Token: 0x04001F4C RID: 8012
		public bool Enabled;

		// Token: 0x04001F4D RID: 8013
		public LeagueWindow.GroupTexture Textures = new LeagueWindow.GroupTexture();

		// Token: 0x04001F4E RID: 8014
		public LeagueWindow.GroupStyle Styles = new LeagueWindow.GroupStyle();

		// Token: 0x04001F4F RID: 8015
		public LeagueWindow.GroupRect Rects = new LeagueWindow.GroupRect();

		// Token: 0x04001F50 RID: 8016
		public LeagueWindow.GroupList Lists = new LeagueWindow.GroupList();

		// Token: 0x04001F51 RID: 8017
		public Color[] RankColor;

		// Token: 0x04001F52 RID: 8018
		public AudioClip MatchFound;

		// Token: 0x04001F53 RID: 8019
		public AudioClip TimerClip;

		// Token: 0x04001F54 RID: 8020
		public AnimationCurve AlphaCurve;

		// Token: 0x04001F55 RID: 8021
		[HideInInspector]
		public Alpha LeagueAlpha = new Alpha();

		// Token: 0x04001F56 RID: 8022
		public SeasonState SeasonState;

		// Token: 0x02000307 RID: 775
		[Serializable]
		internal class GroupTexture
		{
			// Token: 0x04001F59 RID: 8025
			public Texture2D Black;

			// Token: 0x04001F5A RID: 8026
			public Texture2D LightGray;

			// Token: 0x04001F5B RID: 8027
			public Texture2D Gray;

			// Token: 0x04001F5C RID: 8028
			public Texture2D DarkGray;

			// Token: 0x04001F5D RID: 8029
			public Texture2D Green;

			// Token: 0x04001F5E RID: 8030
			public Texture2D Logo;

			// Token: 0x04001F5F RID: 8031
			public Texture2D Booster;

			// Token: 0x04001F60 RID: 8032
			public Texture2D PrizePlaceBack;

			// Token: 0x04001F61 RID: 8033
			public Texture2D StripedBack;

			// Token: 0x04001F62 RID: 8034
			public Texture2D TimerBack;

			// Token: 0x04001F63 RID: 8035
			public Texture2D Champion;

			// Token: 0x04001F64 RID: 8036
			public Texture2D Rank;

			// Token: 0x04001F65 RID: 8037
			public Texture2D Twister;

			// Token: 0x04001F66 RID: 8038
			public Texture2D BlueGlow;

			// Token: 0x04001F67 RID: 8039
			public Texture2D RedGlow;

			// Token: 0x04001F68 RID: 8040
			public Texture2D MyPositionIcon;

			// Token: 0x04001F69 RID: 8041
			public Texture2D[] OnlineSpot;

			// Token: 0x04001F6A RID: 8042
			public Texture2D[] PrizePlaceStripes;

			// Token: 0x04001F6B RID: 8043
			public Texture2D[] TeamIcon;

			// Token: 0x04001F6C RID: 8044
			public Texture2D[] Arrows;

			// Token: 0x04001F6D RID: 8045
			public Texture2D[] ResultTextures;
		}

		// Token: 0x02000308 RID: 776
		[Serializable]
		internal class GroupStyle
		{
			// Token: 0x04001F6E RID: 8046
			public GUIStyle ScrollBar;

			// Token: 0x04001F6F RID: 8047
			public GUIStyle SearchGameBtnStyle;

			// Token: 0x04001F70 RID: 8048
			public GUIStyle CloseBtnStyle;

			// Token: 0x04001F71 RID: 8049
			public GUIStyle InfoBtnStyle;

			// Token: 0x04001F72 RID: 8050
			public GUIStyle AcceptBtn;

			// Token: 0x04001F73 RID: 8051
			public GUIStyle ActiveAdBtnStyle;

			// Token: 0x04001F74 RID: 8052
			public GUIStyle InactiveAdBtnStyle;

			// Token: 0x04001F75 RID: 8053
			public GUIStyle LongBtnStyle;

			// Token: 0x04001F76 RID: 8054
			public GUIStyle SmallBtnStyle;

			// Token: 0x04001F77 RID: 8055
			public GUIStyle RefreshBtnStyle;

			// Token: 0x04001F78 RID: 8056
			public GUIStyle Timer;

			// Token: 0x04001F79 RID: 8057
			public GUIStyle TimerBreak;

			// Token: 0x04001F7A RID: 8058
			public GUIStyle WhiteLabel14;

			// Token: 0x04001F7B RID: 8059
			public GUIStyle WhiteLabel14L;

			// Token: 0x04001F7C RID: 8060
			public GUIStyle WhiteLabel15;

			// Token: 0x04001F7D RID: 8061
			public GUIStyle WhiteLabel16;

			// Token: 0x04001F7E RID: 8062
			public GUIStyle WhiteLabel20;

			// Token: 0x04001F7F RID: 8063
			public GUIStyle GrayLabel;

			// Token: 0x04001F80 RID: 8064
			public GUIStyle DarkGrayLabel;

			// Token: 0x04001F81 RID: 8065
			public GUIStyle BrownLabel;

			// Token: 0x04001F82 RID: 8066
			public GUIStyle BrownLabelR;

			// Token: 0x04001F83 RID: 8067
			public GUIStyle BrownLabel28;

			// Token: 0x04001F84 RID: 8068
			public GUIStyle BlackLabel;

			// Token: 0x04001F85 RID: 8069
			public GUIStyle GrayWhiteLabel;

			// Token: 0x04001F86 RID: 8070
			public GUIStyle BlueLabel28;

			// Token: 0x04001F87 RID: 8071
			public GUIStyle RedLabel28;

			// Token: 0x04001F88 RID: 8072
			public GUIStyle WhiteKorataki34;

			// Token: 0x04001F89 RID: 8073
			public GUIStyle GPLabel;

			// Token: 0x04001F8A RID: 8074
			public GUIStyle CRLabel;

			// Token: 0x04001F8B RID: 8075
			public GUIStyle LpGainBtn;

			// Token: 0x04001F8C RID: 8076
			public GUIStyle LpProtectBtn;

			// Token: 0x04001F8D RID: 8077
			public GUIStyle tmpStyle;
		}

		// Token: 0x02000309 RID: 777
		[Serializable]
		internal class GroupRect
		{
			// Token: 0x04001F8E RID: 8078
			public Rect CloseBtnRect = default(Rect);

			// Token: 0x04001F8F RID: 8079
			public Rect SearchGameBtnRect = default(Rect);

			// Token: 0x04001F90 RID: 8080
			public Rect BoosterTextureRect = default(Rect);

			// Token: 0x04001F91 RID: 8081
			public Rect BoosterLabelRect = default(Rect);

			// Token: 0x04001F92 RID: 8082
			public Rect SeasonEndLabelRect = default(Rect);

			// Token: 0x04001F93 RID: 8083
			public Rect LeftStripedBackTextureRect = default(Rect);

			// Token: 0x04001F94 RID: 8084
			public Rect RightStripedBackTextureRect = default(Rect);

			// Token: 0x04001F95 RID: 8085
			public Rect TimerBackTextureRect = default(Rect);

			// Token: 0x04001F96 RID: 8086
			public Rect TwisterTextureRect = default(Rect);

			// Token: 0x04001F97 RID: 8087
			public Rect AdvertisingHeaderRect = default(Rect);

			// Token: 0x04001F98 RID: 8088
			public Rect AdvertisingBodyRect = default(Rect);

			// Token: 0x04001F99 RID: 8089
			public Rect PlayerCountRect = default(Rect);

			// Token: 0x04001F9A RID: 8090
			public Rect StartTimerRect = default(Rect);

			// Token: 0x04001F9B RID: 8091
			public Rect[] ResultHeaderRects = new Rect[10];

			// Token: 0x04001F9C RID: 8092
			public Rect[] RatingHeaderRects = new Rect[8];

			// Token: 0x04001F9D RID: 8093
			public Rect[] RatingBarRects = new Rect[10];

			// Token: 0x04001F9E RID: 8094
			public Rect[] SearchingRects = new Rect[11];

			// Token: 0x04001F9F RID: 8095
			public Rect[] GatheringRects = new Rect[5];

			// Token: 0x04001FA0 RID: 8096
			public Rect[] GatheringHeaderRects = new Rect[7];

			// Token: 0x04001FA1 RID: 8097
			public Rect[] TeamIconTextureRects = new Rect[2];

			// Token: 0x04001FA2 RID: 8098
			public Rect[] TeamLabelRects = new Rect[2];
		}

		// Token: 0x0200030A RID: 778
		[Serializable]
		internal class GroupList
		{
			// Token: 0x04001FA3 RID: 8099
			public ScrollList RatingList = new ScrollList();

			// Token: 0x04001FA4 RID: 8100
			public ScrollList RatingPastList = new ScrollList();

			// Token: 0x04001FA5 RID: 8101
			public ScrollList RatingFutureList = new ScrollList();

			// Token: 0x04001FA6 RID: 8102
			public ScrollList RulesList = new ScrollList();

			// Token: 0x04001FA7 RID: 8103
			public ScrollList ResultList = new ScrollList();

			// Token: 0x04001FA8 RID: 8104
			public ScrollList GatheringUsecList = new ScrollList();

			// Token: 0x04001FA9 RID: 8105
			public ScrollList GatheringBearList = new ScrollList();
		}
	}
}
