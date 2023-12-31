using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x0200018B RID: 395
	internal class TCSpawn : AbstractSpawnState
	{
		// Token: 0x06000B59 RID: 2905 RVA: 0x0008BED0 File Offset: 0x0008A0D0
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

		// Token: 0x06000B5A RID: 2906 RVA: 0x0008BF44 File Offset: 0x0008A144
		public override void OnConnected()
		{
			SpectactorGUI.I.teamChoosing = true;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0008BF54 File Offset: 0x0008A154
		public override void OnUpdate()
		{
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

		// Token: 0x04000D43 RID: 3395
		private SpecHint hint = new TCHint();

		// Token: 0x04000D44 RID: 3396
		private SpecHint waitRound = new WaitRound();
	}
}
