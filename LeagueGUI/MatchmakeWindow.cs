using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000311 RID: 785
	internal class MatchmakeWindow : AbstractWindow
	{
		// Token: 0x06001AC0 RID: 6848 RVA: 0x000F1D6C File Offset: 0x000EFF6C
		private void SetFrame(IFrame frame)
		{
			this._currentFrame = frame;
			this._currentFrame.OnStart();
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x000F1D80 File Offset: 0x000EFF80
		public override void OnStart()
		{
			this._searchFrame.OnStart();
			this._adFrame.OnStart();
			this._gatheringFrame.OnStart();
			this.Awake();
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000F1DAC File Offset: 0x000EFFAC
		public void Awake()
		{
			this.SetFrame(this._adFrame);
			this._searchFrame.SetFrame(this._searchFrame.SearchingFrame);
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x000F1DDC File Offset: 0x000EFFDC
		public override void OnGUI()
		{
			this._currentFrame.OnGUI();
			this._searchFrame.OnGUI();
			if (this._searchFrame.GetFrame() == this._searchFrame.ReadyFrame && !SearchFrame.Accepted)
			{
				SearchFrame.Accepted = true;
			}
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x000F1E2C File Offset: 0x000F002C
		public override void OnUpdate()
		{
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x000F1E30 File Offset: 0x000F0030
		public override void OnReadyGame()
		{
			Debug.Log(" - >>> OnReadyGame ");
			this._searchFrame.SetFrame(this._searchFrame.ReadyFrame);
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000F1E60 File Offset: 0x000F0060
		public override void OnMapLoading()
		{
			Debug.Log(" - >>> MapLoading ");
			this._searchFrame.SetFrame(this._searchFrame.MapLoadingFrame);
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x000F1E90 File Offset: 0x000F0090
		public override void OnWaitingPlayers()
		{
			Debug.Log(" - >>> _gatheringFrame ");
			this.SetFrame(this._gatheringFrame);
			this._searchFrame.SetFrame(this._searchFrame.PlayersWaitingFrame);
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x000F1ECC File Offset: 0x000F00CC
		public override void OnMatchStarting()
		{
			Debug.Log(" - >>> _matchStartFrame ");
			this._searchFrame.SetFrame(this._searchFrame.MatchStartFrame);
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x000F1EFC File Offset: 0x000F00FC
		public override void OnCancelGame()
		{
			base.OnCancelGame();
			LeagueWindow.I.Reset();
		}

		// Token: 0x04001FD2 RID: 8146
		private int _waitTime = 16;

		// Token: 0x04001FD3 RID: 8147
		private CountdownSound _countdownSound = new CountdownSound();

		// Token: 0x04001FD4 RID: 8148
		private readonly SearchFrame _searchFrame = new SearchFrame();

		// Token: 0x04001FD5 RID: 8149
		private readonly IFrame _adFrame = new AdvertisingFrame();

		// Token: 0x04001FD6 RID: 8150
		private readonly IFrame _gatheringFrame = new PlayerGatheringFrame();

		// Token: 0x04001FD7 RID: 8151
		private IFrame _currentFrame;
	}
}
