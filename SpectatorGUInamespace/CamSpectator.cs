using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000185 RID: 389
	internal class CamSpectator
	{
		// Token: 0x06000B3C RID: 2876 RVA: 0x0008B864 File Offset: 0x00089A64
		public CamSpectator()
		{
			this.spectatePlans = new SpectatePlans(this);
			this.spectatePlayer = new SpectatePlayer(this);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0008B884 File Offset: 0x00089A84
		public bool Enabled
		{
			get
			{
				return this._enabled && this.state != null;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0008B8A0 File Offset: 0x00089AA0
		public Spectate CurrentState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0008B8A8 File Offset: 0x00089AA8
		public void SetState(Spectate state)
		{
			if (this.state != null)
			{
				this.state.Disable();
			}
			this.state = state;
			this.state.Enable();
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0008B8E0 File Offset: 0x00089AE0
		public void Enable()
		{
			if (this.state != null)
			{
				this.state.Enable();
			}
			this._enabled = true;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0008B900 File Offset: 0x00089B00
		public void Disable()
		{
			if (this.state != null)
			{
				this.state.Disable();
			}
			this._enabled = false;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0008B920 File Offset: 0x00089B20
		public void next()
		{
			if (this.state != null)
			{
				this.state.next();
			}
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0008B938 File Offset: 0x00089B38
		public void OnConnected()
		{
			this.spectatePlans.OnConnected();
			this.spectatePlayer.OnConnected();
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0008B950 File Offset: 0x00089B50
		public void OnGUI()
		{
			if (this.state != null && !MainGUI.Instance.Visible && Peer.ClientGame.MatchState != MatchState.match_result && SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc <= 0)
			{
				this.state.OnGUI();
			}
		}

		// Token: 0x04000D39 RID: 3385
		private Spectate state;

		// Token: 0x04000D3A RID: 3386
		public Spectate spectatePlans;

		// Token: 0x04000D3B RID: 3387
		public Spectate spectatePlayer;

		// Token: 0x04000D3C RID: 3388
		private bool _enabled;
	}
}
