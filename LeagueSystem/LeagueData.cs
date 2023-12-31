using System;
using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine;

namespace LeagueSystem
{
	// Token: 0x020002FF RID: 767
	internal class LeagueData
	{
		// Token: 0x06001A55 RID: 6741 RVA: 0x000EF864 File Offset: 0x000EDA64
		public void AddUserStats(string uid, Stats stats)
		{
			this._jsonData.AddUserStats(uid, stats);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000EF874 File Offset: 0x000EDA74
		public void AddWinPoints(int usecPoints, int bearPoints)
		{
			this._jsonData.usec_points = usecPoints;
			this._jsonData.bear_points = bearPoints;
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000EF890 File Offset: 0x000EDA90
		public int PlayersCount
		{
			get
			{
				return this._jsonData.players.Count;
			}
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000EF8A4 File Offset: 0x000EDAA4
		public bool NeedRestart()
		{
			int num = 0;
			int num2 = 0;
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.status == PlayerStatus.connected)
				{
					if (playerJsonData.team == 1)
					{
						num2++;
					}
					if (playerJsonData.team == 2)
					{
						num++;
					}
				}
			}
			return num2 == 0 || num == 0;
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000EF948 File Offset: 0x000EDB48
		public bool AllPlayersReady()
		{
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.status != PlayerStatus.connected)
				{
					return false;
				}
			}
			return this._jsonData.players.Count > 0;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000EF9D4 File Offset: 0x000EDBD4
		public void OnDisconnect(int ID)
		{
			this.SetStatus(ID, PlayerStatus.disconnected);
			this.SetDisconnectTime(ID, Time.time);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000EF9EC File Offset: 0x000EDBEC
		public void OnLeave(int ID)
		{
			this.SetDisconnectTime(ID, Time.time);
			this.SetStatus(ID, PlayerStatus.leaver);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000EFA04 File Offset: 0x000EDC04
		public void AddRemoteData(RemoteMatchenddData data)
		{
			this._jsonData.SetRemoteData(data);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000EFA14 File Offset: 0x000EDC14
		public bool CanConnect(int ID)
		{
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.uid == ID.ToString())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000EFA9C File Offset: 0x000EDC9C
		public void Connect(int ID)
		{
			this.SetStatus(ID, PlayerStatus.connected);
			this.SetDisconnectTime(ID, 0f);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000EFAB4 File Offset: 0x000EDCB4
		public int Team(int ID)
		{
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.uid == ID.ToString())
				{
					return playerJsonData.team;
				}
			}
			return 0;
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000EFB40 File Offset: 0x000EDD40
		private void SetStatus(int ID, PlayerStatus status)
		{
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.uid == ID.ToString())
				{
					playerJsonData.status = status;
				}
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000EFBC4 File Offset: 0x000EDDC4
		public PlayerJsonData GetPlayerData(string uid)
		{
			for (int i = 0; i < this._jsonData.players.Count; i++)
			{
				if (this._jsonData.players[i].uid == uid)
				{
					return this._jsonData.players[i];
				}
			}
			return null;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000EFC28 File Offset: 0x000EDE28
		private void SetDisconnectTime(int ID, float disconnectTime)
		{
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				if (playerJsonData.uid == ID.ToString())
				{
					playerJsonData.disconnect_time = disconnectTime;
				}
			}
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x000EFCAC File Offset: 0x000EDEAC
		public string ToJSON()
		{
			this._jsonData.port = Peer.Info.Port;
			this._jsonData.ip = Peer.Info.Ip;
			return JsonWriter.Serialize(this._jsonData);
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x000EFCE4 File Offset: 0x000EDEE4
		public void FromJSON(string json)
		{
			this._jsonData = JsonReader.Deserialize<MatchJsonData>(json);
			if (string.IsNullOrEmpty(this.MatchID))
			{
				this.MatchID = this._jsonData.match_id;
			}
			else if (this.MatchID != this._jsonData.match_id)
			{
				Debug.LogError("trying to set wrong match id to match with already existing id!");
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x000EFD48 File Offset: 0x000EDF48
		public Dictionary<string, object> GetSaveData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("match_id", this.MatchID);
			dictionary.Add("ip", Peer.Info.Ip);
			dictionary.Add("port", Peer.Info.Port);
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			foreach (PlayerJsonData playerJsonData in this._jsonData.players)
			{
				Dictionary<string, object> dictionary3 = new Dictionary<string, object>();
				dictionary3.Add("disconnect_time", playerJsonData.disconnect_time);
				dictionary3.Add("isWinner", playerJsonData.isWinner);
				try
				{
					foreach (ServerNetPlayer serverNetPlayer in Peer.ServerGame.ServerNetPlayers)
					{
						if (serverNetPlayer.UserID.ToString() == playerJsonData.uid)
						{
							dictionary3.Add("profile", serverNetPlayer.UserInfo.ToDict(true));
							break;
						}
					}
				}
				catch (Exception)
				{
				}
				dictionary2.Add(playerJsonData.uid, dictionary3);
			}
			dictionary.Add("players", dictionary2);
			return dictionary;
		}

		// Token: 0x04001EFD RID: 7933
		public string MatchID = string.Empty;

		// Token: 0x04001EFE RID: 7934
		public MatchJsonData _jsonData = new MatchJsonData();

		// Token: 0x04001EFF RID: 7935
		private Timer _updateReadyTimer = new Timer();
	}
}
