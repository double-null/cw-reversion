using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x0200018A RID: 394
	internal class TESpawn : AbstractSpawnState
	{
		// Token: 0x06000B55 RID: 2901 RVA: 0x0008BC70 File Offset: 0x00089E70
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
			else
			{
				this.hint.OnGUI();
			}
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0008BD04 File Offset: 0x00089F04
		public override void OnConnected()
		{
			base.OnConnected();
			SpectactorGUI.I.teamChoosing = true;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0008BD18 File Offset: 0x00089F18
		public override void OnUpdate()
		{
			bool flag = Input.GetKeyDown(KeyCode.Mouse0) || Main.UserInfo.settings.graphics.Autorespawn;
			bool flag2 = SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc < 0;
			ClientNetPlayer localPlayer = Peer.ClientGame.LocalPlayer;
			if (localPlayer.IsDeadOrSpectactor && !SpectactorGUI.I.teamChoosing && flag && flag2)
			{
				if (localPlayer.IsTeamChoosed)
				{
					this.Spawn();
				}
				else if (ClientLeagueSystem.IsLeagueGame)
				{
					this.Spawn();
					SpectactorGUI.I.teamChoosing = false;
					SpectactorGUI.I.HideTeamChoose(0.5f);
				}
			}
			if (Peer.ClientGame.MatchState != MatchState.match_result && Peer.ClientGame.MatchState != MatchState.round_pre_ended)
			{
				if (!Peer.ClientGame.IsFull && !MainGUI.Instance.Visible && !Forms.chatLock && SpectactorGUI.I.changeTeamCount > 0 && Input.GetKeyDown(Main.UserInfo.settings.binds.teamChoose))
				{
					SpectactorGUI.I.teamChoosing = !SpectactorGUI.I.teamChoosing;
					if (SpectactorGUI.I.teamChoosing)
					{
						SpectactorGUI.I.ShowTeamChoose(0.5f);
					}
					else
					{
						SpectactorGUI.I.HideTeamChoose(0.5f);
					}
				}
			}
			else
			{
				SpectactorGUI.I.teamChoosing = false;
			}
		}

		// Token: 0x04000D40 RID: 3392
		private SpecHint hint = new TEHint();

		// Token: 0x04000D41 RID: 3393
		private SpecHint waitSpawn = new WaitRessurect();

		// Token: 0x04000D42 RID: 3394
		private SpecHint waitRound = new WaitRound();
	}
}
