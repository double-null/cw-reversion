using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000189 RID: 393
	internal class DMSpawn : AbstractSpawnState
	{
		// Token: 0x06000B50 RID: 2896 RVA: 0x0008BA3C File Offset: 0x00089C3C
		public override void Spawn()
		{
			if (Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor)
			{
				if (Peer.ClientGame.IsFull)
				{
					if (Peer.ClientGame.LocalPlayer.IsTeamChoosed)
					{
						base.Spawn();
					}
				}
				else
				{
					if (!Peer.ClientGame.LocalPlayer.IsTeamChoosed)
					{
						if (UnityEngine.Random.value > 0.5f)
						{
							Peer.ClientGame.LocalPlayer.ChooseTeam(PlayerType.bear);
						}
						else
						{
							Peer.ClientGame.LocalPlayer.ChooseTeam(PlayerType.usec);
						}
					}
					base.Spawn();
				}
			}
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0008BADC File Offset: 0x00089CDC
		public override void OnUpdate()
		{
			if (!SpectactorGUI.I.GameplayTutorShowed)
			{
				return;
			}
			bool flag = SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc < 0;
			float timeFirstSpawn = SpectactorGUI.I.timeFirstSpawn;
			if ((Input.GetKeyDown(KeyCode.Mouse0) || (!Peer.ClientGame.LocalPlayer.IsSpectactor && Main.UserInfo.settings.graphics.Autorespawn)) && Main.IsDeadOrSpectactor && flag && SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked && timeFirstSpawn > 0f && timeFirstSpawn < Time.realtimeSinceStartup)
			{
				this.Spawn();
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0008BB90 File Offset: 0x00089D90
		public override void OnGUI()
		{
			if (Peer.ClientGame.MatchState == MatchState.match_result || Peer.ClientGame.MatchState == MatchState.round_ended || Peer.ClientGame.MatchState == MatchState.round_pre_ended)
			{
				this.waitRound.OnGUI();
			}
			else if (SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc > 0)
			{
				if (Peer.ClientGame.LocalPlayer.IsTeamChoosed)
				{
					this.waitSpawn.OnGUI();
				}
			}
			else if (SpectactorGUI.I.GameplayTutorShowed)
			{
				this.hint.OnGUI();
			}
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0008BC34 File Offset: 0x00089E34
		public override void OnConnected()
		{
			SpectactorGUI.I.teamChoosing = false;
		}

		// Token: 0x04000D3D RID: 3389
		private SpecHint hint = new DMHint();

		// Token: 0x04000D3E RID: 3390
		private SpecHint waitSpawn = new WaitRessurect();

		// Token: 0x04000D3F RID: 3391
		private SpecHint waitRound = new WaitRound();
	}
}
