using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x0200018C RID: 396
	internal class TDSSpawn : AbstractSpawnState
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x0008C05C File Offset: 0x0008A25C
		public override void OnGUI()
		{
			if (Peer.ClientGame.MatchState == MatchState.match_result || Peer.ClientGame.MatchState == MatchState.round_ended || Peer.ClientGame.MatchState == MatchState.round_pre_ended)
			{
				this.waitRound.OnGUI();
			}
			else if (SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc <= 0)
			{
				this.hint.OnGUI();
			}
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0008C0D0 File Offset: 0x0008A2D0
		public override void OnConnected()
		{
			SpectactorGUI.I.teamChoosing = true;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0008C0E0 File Offset: 0x0008A2E0
		public override void OnUpdate()
		{
			if (Peer.ClientGame.MatchState != MatchState.match_result && Peer.ClientGame.MatchState != MatchState.round_pre_ended)
			{
				if (!MainGUI.Instance.Visible && !Forms.chatLock && (SpectactorGUI.I.changeTeamCount > 0 || Peer.ClientGame.MatchState == MatchState.alone) && Input.GetKeyDown(Main.UserInfo.settings.binds.teamChoose))
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

		// Token: 0x04000D45 RID: 3397
		private SpecHint hint = new TDSHint();

		// Token: 0x04000D46 RID: 3398
		private SpecHint waitRound = new WaitRound();
	}
}
