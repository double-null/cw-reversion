using System;
using LeagueSystem;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x02000329 RID: 809
	internal class PlayerGatheringFrame : AbstractFrame
	{
		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x000F68A4 File Offset: 0x000F4AA4
		private Rect playerGatheringFrameRect
		{
			get
			{
				return new Rect((float)(Screen.width - 800) * 0.5f, (float)(Screen.height - 600) * 0.5f + 180f, 800f, 600f);
			}
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000F68E0 File Offset: 0x000F4AE0
		public override void OnStart()
		{
			ClientLeagueSystem.SetMatchDataEvent += this.GenerateTable;
			this.GenerateTable();
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000F68FC File Offset: 0x000F4AFC
		public override void OnGUI()
		{
			if (Peer.ClientGame != null)
			{
				this._playersTotal = ClientLeagueSystem.ListWaitingPlayers.Count;
				if (this._timer.Time > 1f)
				{
					this._playersLoaded = 0;
					foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
					{
						if (entityNetPlayer.playerInfo.loading >= 100 || !entityNetPlayer.IsSpectactor)
						{
							this._playersLoaded++;
						}
					}
					this._timer.Start();
				}
			}
			GUI.BeginGroup(this.playerGatheringFrameRect);
			GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[0], LeagueWindow.I.Textures.LightGray);
			if (this._playersTotal == this._playersLoaded && this._playersTotal != 0)
			{
				GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[1], LeagueWindow.I.Textures.Green);
			}
			else
			{
				GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[1], LeagueWindow.I.Textures.Gray);
			}
			GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[2], LeagueWindow.I.Textures.LightGray);
			GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[3], LeagueWindow.I.Textures.Gray);
			GUI.DrawTexture(LeagueWindow.I.Rects.GatheringRects[4], LeagueWindow.I.Textures.Gray);
			GUI.DrawTexture(LeagueWindow.I.Rects.TeamIconTextureRects[0], LeagueWindow.I.Textures.TeamIcon[0]);
			GUI.DrawTexture(LeagueWindow.I.Rects.TeamIconTextureRects[1], LeagueWindow.I.Textures.TeamIcon[1]);
			GUI.Label(LeagueWindow.I.Rects.TeamLabelRects[0], "BEAR", LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(LeagueWindow.I.Rects.TeamLabelRects[1], "USEC", LeagueWindow.I.Styles.WhiteLabel16);
			GUI.Label(LeagueWindow.I.Rects.PlayerCountRect, this._playersLoaded + " / " + this._playersTotal, LeagueWindow.I.Styles.WhiteLabel16);
			this.usecTable.OnGUI();
			LeagueWindow.I.Lists.GatheringUsecList.OnGUI();
			GUI.BeginGroup(new Rect(400f, 0f, 400f, 420f));
			this.bearTable.OnGUI();
			LeagueWindow.I.Lists.GatheringBearList.OnGUI();
			GUI.EndGroup();
			GUI.EndGroup();
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x000F6C80 File Offset: 0x000F4E80
		public void GenerateTable()
		{
			LeagueWindow.I.Lists.GatheringUsecList.Clear();
			LeagueWindow.I.Lists.GatheringBearList.Clear();
			foreach (PlayerJsonData playerJsonData in ClientLeagueSystem.ListWaitingPlayers)
			{
				if (playerJsonData.status == PlayerStatus.connected)
				{
					if (playerJsonData.team == 1)
					{
						LeagueWindow.I.Lists.GatheringBearList.Add(new GatheringBar(playerJsonData, null));
					}
					if (playerJsonData.team == 2)
					{
						LeagueWindow.I.Lists.GatheringUsecList.Add(new GatheringBar(playerJsonData, null));
					}
				}
			}
			this._timer = new Timer();
			this._timer.Start();
		}

		// Token: 0x04002037 RID: 8247
		private GatheringTable usecTable = new GatheringTable();

		// Token: 0x04002038 RID: 8248
		private GatheringTable bearTable = new GatheringTable();

		// Token: 0x04002039 RID: 8249
		private int _playersLoaded;

		// Token: 0x0400203A RID: 8250
		private int _playersTotal;

		// Token: 0x0400203B RID: 8251
		private Timer _timer;
	}
}
