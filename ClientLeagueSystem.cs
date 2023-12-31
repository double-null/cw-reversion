using System;
using System.Collections.Generic;
using JsonFx.Json;
using LeagueGUI;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002FC RID: 764
internal class ClientLeagueSystem : MonoBehaviour
{
	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06001A2F RID: 6703 RVA: 0x000EEE78 File Offset: 0x000ED078
	// (remove) Token: 0x06001A30 RID: 6704 RVA: 0x000EEE90 File Offset: 0x000ED090
	public static event Action SetMatchDataEvent;

	// Token: 0x170007C6 RID: 1990
	// (get) Token: 0x06001A31 RID: 6705 RVA: 0x000EEEA8 File Offset: 0x000ED0A8
	public static List<PlayerJsonData> ListWaitingPlayers
	{
		get
		{
			return ClientLeagueSystem._jsonMatchData.players;
		}
	}

	// Token: 0x170007C7 RID: 1991
	// (get) Token: 0x06001A32 RID: 6706 RVA: 0x000EEEB4 File Offset: 0x000ED0B4
	public static MatchJsonData MatchEndData
	{
		get
		{
			return ClientLeagueSystem._jsonMatchData;
		}
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x000EEEBC File Offset: 0x000ED0BC
	private static void OnRequestSuccess(string text, string url)
	{
		if (ClientLeagueSystem.OnSendRequest != null)
		{
			ClientLeagueSystem.OnSendRequest();
		}
		ClientLeagueSystem._time = Time.time + ClientLeagueSystem._timeout;
		ClientLeagueSystem._inQueue = true;
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x000EEEF4 File Offset: 0x000ED0F4
	private static void OnRequestFailed(Exception e, string url)
	{
		ClientLeagueSystem._inQueue = false;
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x000EEEFC File Offset: 0x000ED0FC
	private static void OnCancelRequestSuccess(string text, string url)
	{
		if (ClientLeagueSystem.OnCancelRequest != null)
		{
			ClientLeagueSystem.OnCancelRequest();
		}
		ClientLeagueSystem._time = 0f;
		ClientLeagueSystem._inQueue = false;
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x000EEF30 File Offset: 0x000ED130
	public static void SendRequest()
	{
		for (int i = 0; i < ClientLeagueSystem.debugEvents.Length; i++)
		{
			ClientLeagueSystem.debugEvents[i] = new DoOnce();
		}
		ClientLeagueSystem._readyGameDo.Clear();
		ClientLeagueSystem._testTimer.Start();
		HtmlLayer.RequestCompressed("adm.php?q=ladder/join", new RequestFinished(ClientLeagueSystem.OnRequestSuccess), new RequestFailed(ClientLeagueSystem.OnRequestFailed), string.Empty, string.Empty);
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x000EEFA4 File Offset: 0x000ED1A4
	public static void CancelRequest()
	{
		for (int i = 0; i < ClientLeagueSystem.debugEvents.Length; i++)
		{
			ClientLeagueSystem.debugEvents[i] = new DoOnce();
		}
		ClientLeagueSystem._testTimer.Stop();
		ClientLeagueSystem.SendCancelRequest();
		ClientLeagueSystem._connectTo = null;
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x000EEFEC File Offset: 0x000ED1EC
	public static void SendCancelRequest()
	{
		HtmlLayer.RequestCompressed("adm.php?q=ladder/exit", new RequestFinished(ClientLeagueSystem.OnCancelRequestSuccess), null, string.Empty, string.Empty);
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x000EF010 File Offset: 0x000ED210
	private static void Stop()
	{
		ClientLeagueSystem._time = 0f;
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x000EF01C File Offset: 0x000ED21C
	public static void SetMatchData(string json)
	{
		ClientLeagueSystem._jsonMatchData = JsonReader.Deserialize<MatchJsonData>(json);
		if (ClientLeagueSystem.SetMatchDataEvent != null)
		{
			ClientLeagueSystem.SetMatchDataEvent();
		}
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x000EF040 File Offset: 0x000ED240
	public static void CancelGame()
	{
		if (ClientLeagueSystem.OnCanelGame != null)
		{
			ClientLeagueSystem.OnCanelGame();
		}
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x000EF058 File Offset: 0x000ED258
	public static void MatchStarting()
	{
		if (ClientLeagueSystem.OnMatchStating != null)
		{
			ClientLeagueSystem.OnMatchStating();
		}
		SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked = true;
		SpectactorGUI.I.timeFirstSpawn = Time.realtimeSinceStartup + 0.3f;
		EventFactory.Call("HideInterface", null);
		EventFactory.Call("HideTeamChoose", 0.5f);
		SpectactorGUI.I.teamChoosing = false;
		LeagueWindow.I.Enabled = false;
		if (!Main.IsTacticalConquest)
		{
			return;
		}
		SpectactorGUI.I.spawnWindow.SelectDefaultPoint(Main.LocalPlayer.IsBear);
		SpectactorGUI.I.spawnWindow.Enabled = true;
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x000EF104 File Offset: 0x000ED304
	public static void MatchEnd(string json)
	{
		ClientLeagueSystem._jsonMatchData = JsonReader.Deserialize<MatchJsonData>(json);
		LeagueWindow.I.Enabled = true;
		EventFactory.Call("ShowInterface", null);
		if (ClientLeagueSystem.OnMatchEnd != null)
		{
			ClientLeagueSystem.OnMatchEnd();
		}
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x000EF13C File Offset: 0x000ED33C
	private void SendHeartBeat()
	{
		HtmlLayer.RequestCompressed("adm.php?q=ladder/heartbeat", new RequestFinished(this.OnHeartBeatSuccess), new RequestFailed(this.OnHeartBeatFailed), string.Empty, string.Empty);
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x000EF178 File Offset: 0x000ED378
	private void OnHeartBeatSuccess(string text, string url)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		ClientLeagueSystem._inAction = false;
		ClientLeagueSystem._time = Time.time + ClientLeagueSystem._timeout;
		try
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)JsonReader.Deserialize(text);
			if (dictionary.ContainsKey("result"))
			{
				int num = (int)dictionary["result"];
				int num2 = num;
				if (num2 != 0)
				{
					if (num2 == 1)
					{
						ClientLeagueSystem.Stop();
						ClientLeagueSystem.OnCancelRequest();
						EventFactory.Call("ShowPopup", new Popup(WindowsID.SearchGamesGUI, string.Empty, Language.NoSeats, PopupState.information, false, true, string.Empty, string.Empty));
					}
				}
				else
				{
					this.ParseHeartbeatResponse(dictionary);
				}
			}
		}
		catch (Exception innerException)
		{
			ClientLeagueSystem.Stop();
			Debug.LogError(new Exception(url + "\n" + text, innerException));
		}
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x000EF274 File Offset: 0x000ED474
	private void ParseHeartbeatResponse(Dictionary<string, object> data)
	{
		object obj;
		if (data.TryGetValue("stats", out obj))
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)obj;
			object obj2;
			if (dictionary.TryGetValue("queued", out obj2))
			{
				ClientLeagueSystem.PlayersInQueue = (int)obj2;
			}
			object obj3;
			if (dictionary.TryGetValue("matched", out obj3))
			{
				ClientLeagueSystem.PlayersInMatches = (int)obj3;
			}
		}
		object obj4;
		if (data.TryGetValue("status", out obj4))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj4;
			object obj5;
			if (dictionary2.TryGetValue("players_total", out obj5))
			{
				ClientLeagueSystem.PlayersTotal = (int)obj5;
			}
			object obj6;
			if (dictionary2.TryGetValue("players_expected", out obj6))
			{
				ClientLeagueSystem.PlayersExpected = (int)obj6;
			}
			if (ClientLeagueSystem._readyGameDo.Do() && ClientLeagueSystem.OnReadyGame != null)
			{
				ClientLeagueSystem.OnReadyGame();
			}
		}
		object obj7;
		if (data.TryGetValue("map_index", out obj7))
		{
			ClientLeagueSystem.Map = ((Maps)((int)obj7)).ToString().ToUpper();
		}
		object obj8;
		if (data.TryGetValue("mode", out obj8))
		{
			switch ((int)obj8)
			{
			case 1:
				ClientLeagueSystem.Mode = "TEAM ELIMINATION";
				break;
			case 2:
				ClientLeagueSystem.Mode = "TARGET DESIGNATION";
				break;
			case 3:
				ClientLeagueSystem.Mode = "TACTICAL CONQUEST";
				break;
			}
		}
		int num = 0;
		object obj9;
		if (data.TryGetValue("port", out obj9))
		{
			num = (int)obj9;
		}
		string text = string.Empty;
		if (data.TryGetValue("ip", out obj9))
		{
			text = (string)obj9;
		}
		if (!string.IsNullOrEmpty(text) && num > 0)
		{
			ClientLeagueSystem.Stop();
			ClientLeagueSystem._connectTo = new ClientLeagueSystem.ConnectionInfo(text, num);
			ClientLeagueSystem.JoinGame();
		}
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x000EF44C File Offset: 0x000ED64C
	private void OnHeartBeatFailed(Exception e, string url)
	{
		ClientLeagueSystem._inAction = false;
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x000EF454 File Offset: 0x000ED654
	public static void MapLoading()
	{
		if (ClientLeagueSystem.OnMapLoding != null)
		{
			ClientLeagueSystem.OnMapLoding();
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x000EF46C File Offset: 0x000ED66C
	public static void WaitPlayers()
	{
		if (ClientLeagueSystem.OnWaitingPlayers != null)
		{
			ClientLeagueSystem.OnWaitingPlayers();
		}
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x000EF484 File Offset: 0x000ED684
	public static void JoinGame()
	{
		if (ClientLeagueSystem._connectTo == null)
		{
			return;
		}
		Peer.JoinGame(ClientLeagueSystem._connectTo.IP, ClientLeagueSystem._connectTo.Port, true);
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x000EF4AC File Offset: 0x000ED6AC
	private void Update()
	{
		if (ClientLeagueSystem._time > Time.time && this._heartBeatTime < Time.time && !ClientLeagueSystem._inAction)
		{
			this._heartBeatTime = Time.time + this._heartBeatTimeot;
			ClientLeagueSystem._inAction = true;
			this.SendHeartBeat();
		}
		if (this._emulate)
		{
			if (ClientLeagueSystem._testTimer.Time > 3f && ClientLeagueSystem.debugEvents[0].Do())
			{
				Debug.Log("OnSendRequest");
				ClientLeagueSystem.OnSendRequest();
			}
			if (ClientLeagueSystem._testTimer.Time > 5f && ClientLeagueSystem.debugEvents[1].Do())
			{
				Debug.Log("OnCancelRequest");
				ClientLeagueSystem.OnCancelRequest();
			}
			if (ClientLeagueSystem._testTimer.Time > 8f && ClientLeagueSystem.debugEvents[2].Do())
			{
				Debug.Log("OnSendRequest");
				ClientLeagueSystem.OnSendRequest();
			}
			if (ClientLeagueSystem._testTimer.Time > 12f && ClientLeagueSystem.debugEvents[4].Do())
			{
				Debug.Log("OnReadyGame");
				ClientLeagueSystem.OnReadyGame();
			}
			if (ClientLeagueSystem._testTimer.Time > 15f && ClientLeagueSystem.debugEvents[5].Do())
			{
				Debug.Log("OnMapLoding");
				ClientLeagueSystem.OnMapLoding();
			}
			if (ClientLeagueSystem._testTimer.Time > 18f && ClientLeagueSystem.debugEvents[6].Do())
			{
				Debug.Log("OnWaitingPlayers");
				ClientLeagueSystem.OnWaitingPlayers();
			}
			if (ClientLeagueSystem._testTimer.Time > 21f && ClientLeagueSystem.debugEvents[7].Do())
			{
				Debug.Log("OnMatchStating");
				ClientLeagueSystem.OnMatchStating();
			}
			if (ClientLeagueSystem._testTimer.Time > 24f && ClientLeagueSystem.debugEvents[8].Do())
			{
				Debug.Log("OnMatchEnd");
				ClientLeagueSystem.OnMatchEnd();
			}
			if (ClientLeagueSystem._testTimer.Time > 27f && ClientLeagueSystem.debugEvents[9].Do())
			{
				Debug.Log("OnCanelGame");
				ClientLeagueSystem.OnCanelGame();
			}
		}
	}

	// Token: 0x04001ED0 RID: 7888
	public static int PlayersInQueue = 0;

	// Token: 0x04001ED1 RID: 7889
	public static int PlayersInMatches = 0;

	// Token: 0x04001ED2 RID: 7890
	public static int PlayersTotal = 0;

	// Token: 0x04001ED3 RID: 7891
	public static int PlayersExpected = 0;

	// Token: 0x04001ED4 RID: 7892
	public static bool IsLeagueGame = false;

	// Token: 0x04001ED5 RID: 7893
	public static string Map = string.Empty;

	// Token: 0x04001ED6 RID: 7894
	public static string Mode = string.Empty;

	// Token: 0x04001ED7 RID: 7895
	private static float _timeout = 30f;

	// Token: 0x04001ED8 RID: 7896
	private static float _time = 0f;

	// Token: 0x04001ED9 RID: 7897
	private static bool _inQueue = false;

	// Token: 0x04001EDA RID: 7898
	private float _heartBeatTimeot = 10f;

	// Token: 0x04001EDB RID: 7899
	private float _heartBeatTime;

	// Token: 0x04001EDC RID: 7900
	private static bool _inAction = false;

	// Token: 0x04001EDD RID: 7901
	private static Timer _testTimer = new Timer();

	// Token: 0x04001EDE RID: 7902
	private bool _emulate;

	// Token: 0x04001EDF RID: 7903
	private DoOnce _loadMapDo = new DoOnce();

	// Token: 0x04001EE0 RID: 7904
	private static DoOnce _readyGameDo = new DoOnce();

	// Token: 0x04001EE1 RID: 7905
	private static MatchJsonData _jsonMatchData = new MatchJsonData();

	// Token: 0x04001EE2 RID: 7906
	public static ClientLeagueSystem.LegueAction OnSendRequest = delegate()
	{
	};

	// Token: 0x04001EE3 RID: 7907
	public static ClientLeagueSystem.LegueAction OnCancelRequest = delegate()
	{
	};

	// Token: 0x04001EE4 RID: 7908
	public static ClientLeagueSystem.LegueAction OnReadyGame = delegate()
	{
	};

	// Token: 0x04001EE5 RID: 7909
	public static ClientLeagueSystem.LegueAction OnMapLoding = delegate()
	{
	};

	// Token: 0x04001EE6 RID: 7910
	public static ClientLeagueSystem.LegueAction OnWaitingPlayers = delegate()
	{
	};

	// Token: 0x04001EE7 RID: 7911
	public static ClientLeagueSystem.LegueAction OnMatchStating = delegate()
	{
	};

	// Token: 0x04001EE8 RID: 7912
	public static ClientLeagueSystem.LegueAction OnJoinGame = delegate()
	{
	};

	// Token: 0x04001EE9 RID: 7913
	public static ClientLeagueSystem.LegueAction OnMatchEnd = delegate()
	{
	};

	// Token: 0x04001EEA RID: 7914
	public static ClientLeagueSystem.LegueAction OnCanelGame = delegate()
	{
	};

	// Token: 0x04001EEB RID: 7915
	private static ClientLeagueSystem.ConnectionInfo _connectTo = null;

	// Token: 0x04001EEC RID: 7916
	private static DoOnce[] debugEvents = new DoOnce[15];

	// Token: 0x020002FD RID: 765
	private class ConnectionInfo
	{
		// Token: 0x06001A4F RID: 6735 RVA: 0x000EF734 File Offset: 0x000ED934
		public ConnectionInfo(string ip, int port)
		{
			this.IP = ip;
			this.Port = port;
		}

		// Token: 0x04001EF7 RID: 7927
		public string IP = string.Empty;

		// Token: 0x04001EF8 RID: 7928
		public int Port = 27000;
	}

	// Token: 0x020003B2 RID: 946
	// (Invoke) Token: 0x06001E30 RID: 7728
	public delegate void LegueAction();
}
