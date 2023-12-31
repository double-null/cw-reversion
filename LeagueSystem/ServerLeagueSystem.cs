using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JsonFx.Json;
using UnityEngine;

namespace LeagueSystem
{
	// Token: 0x0200033A RID: 826
	internal static class ServerLeagueSystem
	{
		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x000F83A4 File Offset: 0x000F65A4
		public static List<PlayerJsonData> Players
		{
			get
			{
				return ServerLeagueSystem._data._jsonData.players;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000F83B8 File Offset: 0x000F65B8
		public static int PlayersCount
		{
			get
			{
				return ServerLeagueSystem._data.PlayersCount;
			}
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x000F83C4 File Offset: 0x000F65C4
		public static void AddPlayerStats(string uid, Stats stats)
		{
			ServerLeagueSystem._data.AddUserStats(uid, stats);
		}

		// Token: 0x06001B96 RID: 7062 RVA: 0x000F83D4 File Offset: 0x000F65D4
		public static void AddWinPoints(int usecPoints, int bearPoints)
		{
			ServerLeagueSystem._data.AddWinPoints(usecPoints, bearPoints);
		}

		// Token: 0x06001B97 RID: 7063 RVA: 0x000F83E4 File Offset: 0x000F65E4
		public static void Connect(int ID)
		{
			ServerLeagueSystem._data.Connect(ID);
			ServerLeagueSystem.PlayerListChangedCallback(-1);
		}

		// Token: 0x06001B98 RID: 7064 RVA: 0x000F83FC File Offset: 0x000F65FC
		public static void OnLeave(int ID)
		{
			ServerLeagueSystem._data.OnLeave(ID);
			ServerLeagueSystem.SendLeaver(ID);
		}

		// Token: 0x06001B99 RID: 7065 RVA: 0x000F8410 File Offset: 0x000F6610
		public static int Team(int ID)
		{
			return ServerLeagueSystem._data.Team(ID);
		}

		// Token: 0x06001B9A RID: 7066 RVA: 0x000F8420 File Offset: 0x000F6620
		public static bool AllPlayersReady()
		{
			return ServerLeagueSystem._data.AllPlayersReady();
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x000F842C File Offset: 0x000F662C
		public static void OnDisconnect(NetworkPlayer player)
		{
			int num;
			if (ServerLeagueSystem._playerToID.TryGetValue(player, out num))
			{
				ServerLeagueSystem._data.OnDisconnect(num);
				ServerLeagueSystem.SendLeaver(num);
			}
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x000F845C File Offset: 0x000F665C
		private static void SendLeaver(int userId)
		{
			if (string.IsNullOrEmpty(ServerLeagueSystem._data.MatchID))
			{
				UnityEngine.Debug.LogError("trying to send leaver on match with no id! " + Environment.StackTrace);
				return;
			}
			Dictionary<string, object> value = new Dictionary<string, object>
			{
				{
					"match_id",
					ServerLeagueSystem._data.MatchID
				},
				{
					"user_id",
					userId
				},
				{
					"disconnect_time",
					Time.time
				}
			};
			string data = JsonWriter.Serialize(value);
			HtmlLayer.SendCompressed("adm.php?q=ladder/server/leave", data, null, null);
			ServerLeagueSystem.PlayerListChangedCallback(userId);
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x000F84F4 File Offset: 0x000F66F4
		public static bool CanConnect(int ID)
		{
			bool flag = ServerLeagueSystem._data.CanConnect(ID);
			ServerLeagueSystem.Debug(string.Concat(new object[]
			{
				ID,
				" Can Connect = ",
				flag,
				" ",
				Peer.ServerGame.MatchState
			}));
			return flag;
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000F8554 File Offset: 0x000F6754
		public static void SaveAtMatchEnd()
		{
			ServerLeagueSystem.Debug("Match end");
			try
			{
				string text = JsonWriter.Serialize(ServerLeagueSystem._data.GetSaveData());
			}
			catch (Exception message)
			{
				UnityEngine.Debug.LogError(message);
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000F85A8 File Offset: 0x000F67A8
		public static void Register(Dictionary<string, object> dict)
		{
			if (!string.IsNullOrEmpty(ServerLeagueSystem._data.MatchID))
			{
				JSON.ReadWrite(dict, "match_id", ref ServerLeagueSystem._data.MatchID, true);
			}
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000F85E0 File Offset: 0x000F67E0
		public static bool IsDataRefreshedFor(int listenerUid)
		{
			if (!ServerLeagueSystem.DataListeners.ContainsKey(listenerUid))
			{
				ServerLeagueSystem.DataListeners.Add(listenerUid, false);
			}
			bool result = ServerLeagueSystem.DataListeners[listenerUid];
			ServerLeagueSystem.DataListeners[listenerUid] = false;
			return result;
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x000F8624 File Offset: 0x000F6824
		public static float TimeTilNewDataRefresh
		{
			get
			{
				return Mathf.Clamp(Peer.RepeatRegisterEverySeconds - (Time.time - ServerLeagueSystem._dataRefreshTime), 0f, Peer.RepeatRegisterEverySeconds);
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x000F8654 File Offset: 0x000F6854
		private static void OnResponceRegister(string text, string url)
		{
			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					int playersCount = ServerLeagueSystem.PlayersCount;
					if (!string.IsNullOrEmpty(text))
					{
						ServerLeagueSystem._data.FromJSON(text);
					}
					ServerLeagueSystem._dataRefreshTime = Time.time;
					foreach (int key in ServerLeagueSystem.DataListeners.Keys.ToList<int>())
					{
						ServerLeagueSystem.DataListeners[key] = true;
					}
					foreach (PlayerJsonData playerJsonData in ServerLeagueSystem._data._jsonData.players)
					{
						ServerLeagueSystem.Debug("OnResponceRegister players: " + playerJsonData.uid);
					}
					if (!string.IsNullOrEmpty(ServerLeagueSystem._data.MatchID) && Peer.ServerGame.MatchState == MatchState.stoped)
					{
						Peer.ServerGame.DelayedMatchStart((float)CVars.LeagueLoadMapTimeout);
					}
					bool flag = ServerLeagueSystem._data._jsonData.CancelledPlayers != null && ServerLeagueSystem._data._jsonData.CancelledPlayers.Length > 0;
					if (flag)
					{
						ServerLeagueSystem.RemovePlayersFromListByUid(ServerLeagueSystem._data._jsonData.CancelledPlayers);
					}
					if (flag || playersCount != ServerLeagueSystem.PlayersCount)
					{
						ServerLeagueSystem.PlayerListChangedCallback(-1);
					}
				}
				catch (Exception ex)
				{
					ServerLeagueSystem.Debug(string.Concat(new object[]
					{
						"url = ",
						url,
						"\n",
						ex
					}));
				}
			}
			if (!string.IsNullOrEmpty(ServerLeagueSystem._data.MatchID))
			{
				ServerLeagueSystem.RestartMatchCheck();
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000F8868 File Offset: 0x000F6A68
		private static void RemovePlayersFromListByUid(string[] uids)
		{
			List<int> list = new List<int>(uids.Count<string>());
			int i;
			for (i = 0; i < ServerLeagueSystem.Players.Count; i++)
			{
				list.AddRange(from uid in uids
				where ServerLeagueSystem.Players[i].uid == uid
				select i);
			}
			foreach (int index in list)
			{
				ServerLeagueSystem.Players.RemoveAt(index);
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000F8938 File Offset: 0x000F6B38
		public static string MatchID
		{
			get
			{
				return ServerLeagueSystem._data.MatchID;
			}
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x000F8944 File Offset: 0x000F6B44
		private static void OnResponceMatchEnd(string text, string url)
		{
			try
			{
				RemoteMatchenddData data = JsonReader.Deserialize<RemoteMatchenddData>(text);
				ServerLeagueSystem._data.AddRemoteData(data);
			}
			catch (Exception ob)
			{
				ServerLeagueSystem.Debug(ob);
			}
			Peer.ServerGame.MatchEndLeague(ServerLeagueSystem.GetMatchData());
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x000F89A0 File Offset: 0x000F6BA0
		private static void OnFailedMachEnd(Exception e, string url)
		{
			Peer.ServerGame.MatchEndLeague(ServerLeagueSystem.GetMatchData());
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x000F89B4 File Offset: 0x000F6BB4
		private static void Debug(object ob)
		{
			UnityEngine.Debug.Log("LeagueSystem --> " + ob);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000F89C8 File Offset: 0x000F6BC8
		private static void RestartMatchCheck()
		{
			if (ServerLeagueSystem._data.NeedRestart())
			{
				if (!ServerLeagueSystem._restartTimer.IsStarted)
				{
					ServerLeagueSystem._restartTimer.Start();
				}
			}
			else
			{
				ServerLeagueSystem._restartTimer.Stop();
			}
			if (ServerLeagueSystem._restartTimer.Time > (float)ServerLeagueSystem.RestartTime && ServerLeagueSystem._data != null && ServerLeagueSystem._data.NeedRestart())
			{
				ServerLeagueSystem._restartTimer.Stop();
				ServerLeagueSystem.Debug("Need to restart = true");
				Peer.ServerGame.RoundEnd();
				Peer.ServerGame.MatchEnd();
			}
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000F8A64 File Offset: 0x000F6C64
		public static string GetMatchData()
		{
			return ServerLeagueSystem._data.ToJSON();
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x000F8A70 File Offset: 0x000F6C70
		public static void Clear()
		{
			ServerLeagueSystem._playerToID.Clear();
			ServerLeagueSystem._restartTimer.Stop();
			ServerLeagueSystem.Debug("Clear");
			ServerLeagueSystem._data = new LeagueData();
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x000F8AA8 File Offset: 0x000F6CA8
		public static IEnumerator ServerLogic()
		{
			while (string.IsNullOrEmpty(ServerLeagueSystem._data.MatchID))
			{
				yield return new WaitForSeconds(1f);
			}
			while (Peer.ServerGame.ServerNetPlayers.Count < ServerLeagueSystem._data.PlayersCount)
			{
				yield return new WaitForSeconds(1f);
			}
			yield break;
		}

		// Token: 0x0400209B RID: 8347
		public static Action<int> PlayerListChangedCallback;

		// Token: 0x0400209C RID: 8348
		public static bool Enabled = false;

		// Token: 0x0400209D RID: 8349
		private static int RestartTime = 120;

		// Token: 0x0400209E RID: 8350
		private static LeagueData _data = new LeagueData();

		// Token: 0x0400209F RID: 8351
		private static Timer _restartTimer = new Timer();

		// Token: 0x040020A0 RID: 8352
		private static Dictionary<NetworkPlayer, int> _playerToID = new Dictionary<NetworkPlayer, int>();

		// Token: 0x040020A1 RID: 8353
		private static readonly Dictionary<int, bool> DataListeners = new Dictionary<int, bool>();

		// Token: 0x040020A2 RID: 8354
		private static float _dataRefreshTime;
	}
}
