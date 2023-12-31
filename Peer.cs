using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LeagueSystem;
using PeerNamespace;
using UnityEngine;

// Token: 0x020000B4 RID: 180
[AddComponentMenu("Scripts/Engine/Peer")]
internal class Peer : SingletoneForm<Peer>
{
	// Token: 0x1700009E RID: 158
	// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
	public static float RepeatRegisterEverySeconds
	{
		get
		{
			return (ServerLeagueSystem.Enabled && Peer.ServerGame.MatchState == MatchState.stoped) ? 10f : 30f;
		}
	}

	// Token: 0x1700009F RID: 159
	// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001E1E8 File Offset: 0x0001C3E8
	// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001E1F4 File Offset: 0x0001C3F4
	public static NetworkPeerType PeerType
	{
		get
		{
			return SingletoneForm<Peer>.Instance._peerType;
		}
		set
		{
			SingletoneForm<Peer>.Instance._peerType = value;
		}
	}

	// Token: 0x170000A0 RID: 160
	// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001E204 File Offset: 0x0001C404
	// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001E210 File Offset: 0x0001C410
	public static HostInfo Info
	{
		get
		{
			return SingletoneForm<Peer>.Instance._hostInfo;
		}
		set
		{
			SingletoneForm<Peer>.Instance._hostInfo = value;
		}
	}

	// Token: 0x170000A1 RID: 161
	// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001E220 File Offset: 0x0001C420
	public static BaseServerGame ServerGame
	{
		get
		{
			return SingletoneForm<Peer>.Instance._serverGame;
		}
	}

	// Token: 0x170000A2 RID: 162
	// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001E22C File Offset: 0x0001C42C
	public static BaseClientGame ClientGame
	{
		get
		{
			return SingletoneForm<Peer>.Instance._clientGame;
		}
	}

	// Token: 0x170000A3 RID: 163
	// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001E238 File Offset: 0x0001C438
	// (set) Token: 0x06000483 RID: 1155 RVA: 0x0001E244 File Offset: 0x0001C444
	public static bool Dedicated
	{
		get
		{
			return SingletoneForm<Peer>.Instance._dedicated;
		}
		set
		{
			SingletoneForm<Peer>.Instance._dedicated = value;
		}
	}

	// Token: 0x170000A4 RID: 164
	// (get) Token: 0x06000484 RID: 1156 RVA: 0x0001E254 File Offset: 0x0001C454
	// (set) Token: 0x06000485 RID: 1157 RVA: 0x0001E260 File Offset: 0x0001C460
	public static bool IsHost
	{
		get
		{
			return SingletoneForm<Peer>.Instance._isHost;
		}
		set
		{
			SingletoneForm<Peer>.Instance._isHost = value;
		}
	}

	// Token: 0x170000A5 RID: 165
	// (get) Token: 0x06000486 RID: 1158 RVA: 0x0001E270 File Offset: 0x0001C470
	public static bool IsConnected
	{
		get
		{
			return Network.connections.Length > 0;
		}
	}

	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x06000487 RID: 1159 RVA: 0x0001E27C File Offset: 0x0001C47C
	public static bool HardcoreMode
	{
		get
		{
			return SingletoneForm<Peer>.Instance._hostInfo != null && SingletoneForm<Peer>.Instance._hostInfo.Hardcore;
		}
	}

	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001E2A0 File Offset: 0x0001C4A0
	public static bool AllGamesLoaded
	{
		get
		{
			return SingletoneForm<Peer>.Instance._allGamesLoaded;
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000489 RID: 1161 RVA: 0x0001E2AC File Offset: 0x0001C4AC
	// (set) Token: 0x0600048A RID: 1162 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
	public static bool IsHidden
	{
		get
		{
			return SingletoneForm<Peer>.Instance._isHidden;
		}
		set
		{
			SingletoneForm<Peer>.Instance._isHidden = value;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x0600048B RID: 1163 RVA: 0x0001E2C8 File Offset: 0x0001C4C8
	// (set) Token: 0x0600048C RID: 1164 RVA: 0x0001E2D4 File Offset: 0x0001C4D4
	public static bool IsClosing
	{
		get
		{
			return SingletoneForm<Peer>.Instance._isClosing;
		}
		set
		{
			if (value)
			{
				HttpMasterServer.UnregisterHost(CVars.realm, Peer.Info.Ip, Peer.Info.Port.ToString());
			}
			SingletoneForm<Peer>.Instance._isClosing = value;
		}
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0001E318 File Offset: 0x0001C518
	public static void Disconnect(bool unloadAll)
	{
		Forms.OnDisconnect();
		PrefabFactory.DestroyLevel(unloadAll);
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0001E328 File Offset: 0x0001C528
	public static void CreateServer()
	{
		if (Main.UserInfo.Permission <= EPermission.Tester)
		{
			Peer.Info.Ranked = false;
		}
		if (Peer.Dedicated)
		{
			Loader.DownloadGameData(Main.UserInfo.settings.loaderSettings, Peer.Info.MapName, null);
		}
		else
		{
			EventFactory.Call("HideInterface", null);
			EventFactory.Call("ShowLoading", null);
			MainGUI mainGUI = Forms.Get(typeof(MainGUI)) as MainGUI;
			Loader.DownloadGameData(Main.UserInfo.settings.loaderSettings, Peer.Info.MapName, mainGUI.currentSuit);
		}
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
	public static void InitServer()
	{
		global::Console.print("Starting server at " + Peer.Info.Port + ((!Peer.Info.ForceNAT) ? string.Empty : " (NAT)"), Color.green);
		Network.sendRate = (float)CVars.n_updaterate;
		NetworkConnectionError networkConnectionError = Network.InitializeServer(CVars.n_serverMaxConnections, Peer.Info.Port, Peer.Info.ForceNAT);
		if (networkConnectionError == NetworkConnectionError.CreateSocketOrThreadFailure)
		{
			Debug.LogWarning("Port (" + Peer.Info.Port + ") or socket is already in use or failed on creating.");
			networkConnectionError = Network.InitializeServer(CVars.n_serverMaxConnections, Peer.Info.Port + 100, Peer.Info.ForceNAT);
		}
		if (networkConnectionError != NetworkConnectionError.NoError)
		{
			if (Peer.Dedicated)
			{
				Application.Quit();
			}
			else
			{
				Peer.Disconnect(false);
				EventFactory.Call("ShowInterface", null);
				Utility.ShowDisconnectReason(Language.ServerCreation, Language.ServerCreationFailed + " :" + networkConnectionError);
			}
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0001E4E0 File Offset: 0x0001C6E0
	private void OnServerInitialized()
	{
		global::Console.print("InitServerFinished", Color.green);
		Peer.PeerType = NetworkPeerType.Server;
		if (Peer.Info.GameMode == GameMode.TargetDesignation)
		{
			this._serverGame = SingletoneForm<PoolManager>.Instance["server_td_game"].Spawn().GetComponent<ServerTargetDesignationGame>();
		}
		else if (Peer.Info.GameMode == GameMode.Deathmatch)
		{
			this._serverGame = SingletoneForm<PoolManager>.Instance["server_dm_game"].Spawn().GetComponent<ServerDeathMatchGame>();
		}
		else if (Peer.Info.GameMode == GameMode.TeamElimination)
		{
			this._serverGame = SingletoneForm<PoolManager>.Instance["server_te_game"].Spawn().GetComponent<ServerTeamEliminationGame>();
		}
		else if (Peer.Info.GameMode == GameMode.TacticalConquest)
		{
			this._serverGame = SingletoneForm<PoolManager>.Instance["server_tc_game"].Spawn().GetComponent<ServerTacticalConquestGame>();
		}
		this._serverGame.MainInitialize();
		this._serverGame.transform.parent = null;
		if (Peer.Dedicated)
		{
			this._allGamesLoaded = true;
		}
		else
		{
			global::Console.print("Join", Color.grey);
			eNetwork.RPC("OpenConnectionSecure", new eNetworkPlayer(), new object[]
			{
				2,
				Globals.I.version,
				eNetwork.password,
				Main.UserInfo.userID,
				Main.UserInfo.HashID,
				Network.AllocateViewID(),
				default(NetworkMessageInfo)
			});
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x0001E684 File Offset: 0x0001C884
	private void OnPlayerConnected(NetworkPlayer player)
	{
		int num = 0;
		for (int i = 0; i < Network.connections.Length; i++)
		{
			if (Network.connections[i].guid == player.guid)
			{
				num++;
			}
		}
		if (num > 1)
		{
			Network.CloseConnection(player, false);
			return;
		}
		eNetwork.DisableAllGroups(player);
		Peer.ServerGame.PlayerConnectedOverheaded();
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
	private void OnPlayerDisconnected(NetworkPlayer player)
	{
		if (this._filterDoubleConnection.Contains(player))
		{
			this._filterDoubleConnection.Remove(player);
		}
		else
		{
			Debug.LogException(new Exception("Warning at OnPlayerDisconnected"));
		}
		ServerLeagueSystem.OnDisconnect(player);
		Peer.ServerGame.PlayerDisconnected(player);
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0001E740 File Offset: 0x0001C940
	public static bool JoinGame(HostInfo hostGame, bool league = false)
	{
		ClientLeagueSystem.IsLeagueGame = league;
		if (Main.UserInfo.Violation != 1)
		{
			return true;
		}
		if (Network.connections.Length > 0)
		{
			return false;
		}
		if (hostGame == null)
		{
			return false;
		}
		if (hostGame.PasswordProtected && eNetwork.password == string.Empty)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, "Auth", string.Empty, PopupState.auth, false, true, string.Empty, string.Empty));
			return false;
		}
		string password = eNetwork.password;
		Peer.Disconnect(false);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Connection, Language.Connection, Language.TryingConnection, PopupState.progress, false, true, "StopConnection", string.Empty));
		eNetwork.password = password;
		Peer.Info = hostGame;
		Peer.PeerType = NetworkPeerType.Connecting;
		Network.sendRate = (float)CVars.n_cmdrate;
		SingletoneForm<Peer>.Instance.Invoke("OnDelayedDisconnect", 10f);
		if (eNetwork.Connect(hostGame.Ip, hostGame.Port) == NetworkConnectionError.NoError)
		{
			return true;
		}
		Peer.Disconnect(false);
		EventFactory.Call("ShowInterface", null);
		Utility.ShowDisconnectReason(Language.Error, Language.ConnectionFailed);
		return false;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001E86C File Offset: 0x0001CA6C
	public static void JoinGame(string Ip, int port, bool league = false)
	{
		ClientLeagueSystem.IsLeagueGame = league;
		Debug.Log("JoinGame");
		if (Main.UserInfo.Violation != 1)
		{
			return;
		}
		string password = eNetwork.password;
		Peer.Disconnect(false);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Connection, Language.Connection, Language.TryingConnection, PopupState.progress, false, true, "StopConnection", string.Empty));
		eNetwork.password = password;
		Peer.PeerType = NetworkPeerType.Connecting;
		Network.sendRate = (float)CVars.n_cmdrate;
		SingletoneForm<Peer>.Instance.Invoke("OnDelayedDisconnect", 10f);
		if (eNetwork.Connect(Ip, port) == NetworkConnectionError.NoError)
		{
			return;
		}
		Peer.Disconnect(false);
		EventFactory.Call("ShowInterface", null);
		Utility.ShowDisconnectReason(Language.Error, Language.ConnectionFailed);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x0001E930 File Offset: 0x0001CB30
	private void OnConnectedToServer()
	{
		eNetwork.DisableAllGroups(Network.connections[0]);
		base.CancelInvoke("OnDelayedDisconnect");
		Peer.PeerType = NetworkPeerType.Client;
		if (Peer.Info == null)
		{
			global::Console.print("GetHostData", Color.grey);
			eNetwork.RPC("GetHostDataFromClient", new eNetworkPlayer
			{
				owner = Network.connections[0]
			}, new object[0]);
		}
		else
		{
			global::Console.print("Join", Color.grey);
			eNetwork.RPC("OpenConnectionSecure", new eNetworkPlayer
			{
				owner = Network.connections[0]
			}, new object[]
			{
				(int)Main.UserInfo.Permission,
				Globals.I.version,
				eNetwork.password,
				Main.UserInfo.userID,
				Main.UserInfo.HashID,
				Network.AllocateViewID()
			});
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x0001EA44 File Offset: 0x0001CC44
	private void OnFailedToConnect(NetworkConnectionError error)
	{
		if (Peer.PeerType == NetworkPeerType.Connecting || Peer.PeerType == NetworkPeerType.Client)
		{
			Peer.Disconnect(true);
			EventFactory.Call("ShowInterface", null);
			Utility.ShowDisconnectReason(Language.Error, Language.ConnectionFailed);
		}
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x0001EA88 File Offset: 0x0001CC88
	private void OnDisconnectedFromServer(NetworkDisconnection info)
	{
		if (Peer.PeerType == NetworkPeerType.Connecting || Peer.PeerType == NetworkPeerType.Client)
		{
			Peer.Disconnect(true);
			EventFactory.Call("ShowInterface", null);
			Utility.ShowDisconnectReason(Language.Error, Language.ServerDisconnectYou);
		}
		if (ClientLeagueSystem.IsLeagueGame)
		{
			ClientLeagueSystem.CancelGame();
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x0001EADC File Offset: 0x0001CCDC
	[Obfuscation(Exclude = true)]
	private void OnDelayedDisconnect()
	{
		Peer.Disconnect(true);
		Utility.ShowDisconnectReason(Language.Error, Language.ConnectionFailed);
		EventFactory.Call("HidePopup", new Popup(WindowsID.Quit, Language.Connection, Language.ExittingFromServer, PopupState.information, false, true, string.Empty, string.Empty));
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x0001EB28 File Offset: 0x0001CD28
	private void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent != MasterServerEvent.HostListReceived)
		{
			global::Console.print("MasterServerEvent: " + msEvent.ToString(), Color.yellow);
		}
		else
		{
			Peer.GamesListLoaded = true;
		}
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0001EB5C File Offset: 0x0001CD5C
	public void HttpMasterServerListLoaded()
	{
		Peer.GamesListLoaded = true;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x0001EB64 File Offset: 0x0001CD64
	private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
	{
		global::Console.print("Master Server " + error.ToString(), Color.yellow);
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x0001EB88 File Offset: 0x0001CD88
	public static void ForceUpdateServers()
	{
		Peer.games.Clear();
		SingletoneForm<Peer>.Instance.UpdateServers();
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x0001EBA0 File Offset: 0x0001CDA0
	private void UpdateServers()
	{
		if ((Peer.PeerType == NetworkPeerType.Disconnected || Peer.PeerType == NetworkPeerType.Connecting) && Globals.I.GameName != string.Empty)
		{
			HttpMasterServer.RequestHostList(CVars.realm);
			for (int i = 0; i < this.events.Count; i++)
			{
				this.events[i].OnUpdateServersList();
			}
		}
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x0001EC14 File Offset: 0x0001CE14
	private IEnumerator CloseConnection(eNetworkPlayer player, float delay)
	{
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(delay));
		eNetwork.CloseConnection(player, string.Empty, string.Empty);
		yield break;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x0001EC4C File Offset: 0x0001CE4C
	public void Quit()
	{
		SingletoneForm<Peer>.Instance.Invoke("OnDelayedDisconnect", 10f);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0001EC64 File Offset: 0x0001CE64
	[Obfuscation(Exclude = true)]
	[RPC]
	public void GetHostDataFromClient(NetworkMessageInfo nmi)
	{
		eNetwork.RPC("GetHostDataFromServer", new eNetworkPlayer(nmi.sender), new object[]
		{
			Peer.Info.ToJSON()
		});
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0001EC9C File Offset: 0x0001CE9C
	[RPC]
	[Obfuscation(Exclude = true)]
	public void GetHostDataFromServer(string info)
	{
		Peer.Info = new HostInfo();
		Peer.Info.FromJSON(info);
		global::Console.print("Join", Color.grey);
		eNetworkPlayer eNetworkPlayer = new eNetworkPlayer();
		eNetworkPlayer.owner = Network.connections[0];
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--bot"))
		{
			eNetwork.RPC("OpenConnection", eNetworkPlayer, new object[]
			{
				2,
				Globals.I.version,
				eNetwork.password,
				IDUtil.BotID,
				Network.AllocateViewID()
			});
		}
		else
		{
			eNetwork.RPC("OpenConnectionSecure", eNetworkPlayer, new object[]
			{
				(int)Main.UserInfo.Permission,
				Globals.I.version,
				eNetwork.password,
				Main.UserInfo.userID,
				Main.UserInfo.HashID,
				Network.AllocateViewID()
			});
		}
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x0001EDBC File Offset: 0x0001CFBC
	[RPC]
	[Obfuscation(Exclude = true)]
	public void AddBot(string version, string password, int userID, NetworkViewID targetID, NetworkMessageInfo msg)
	{
		if (userID == IDUtil.BotID)
		{
			string text = "no reason";
			if (Peer.Info.PasswordProtected && password != eNetwork.password)
			{
				text = Language.WrongPassword;
			}
			else if (Peer.ServerGame.IsUserInGame(userID) && userID != IDUtil.AdminID && userID != IDUtil.GuestID && userID != IDUtil.BotID)
			{
				text = Language.YouAreAlreadyAtServer;
			}
			else if (Peer.IsClosing)
			{
				text = Language.ServerRestarted;
			}
			else if (version != Globals.I.version)
			{
				text = Language.ClientNotMatchTheServerVersion;
			}
			else if (!Peer.ServerGame.HaveSlot && userID != IDUtil.AdminID)
			{
				text = Peer.ServerGame.HaveSlotText;
			}
			if (text != "no reason")
			{
				try
				{
					if (msg.sender != default(NetworkPlayer))
					{
						eNetwork.RPC("OpenConnectionFailed", new eNetworkPlayer(msg.sender), new object[]
						{
							text
						});
						base.StartCoroutine(this.CloseConnection(new eNetworkPlayer(msg), 0.5f));
					}
					else
					{
						this.OpenConnectionFailed(text);
					}
				}
				catch (Exception e)
				{
					global::Console.exception(e);
				}
			}
			else
			{
				Peer.ServerGame.AddPlayer(userID, targetID, msg.sender.guid, string.Empty);
			}
		}
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x0001EF64 File Offset: 0x0001D164
	[RPC]
	[Obfuscation(Exclude = true)]
	public void OpenConnection(int privilege, string version, string password, int userID, NetworkViewID targetID, NetworkMessageInfo msg)
	{
		base.StartCoroutine(this.CheckConnectionAndSendReason(privilege, version, password, userID, targetID, msg, string.Empty));
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x0001EF8C File Offset: 0x0001D18C
	[RPC]
	[Obfuscation(Exclude = true)]
	public void OpenConnectionSecure(int privilege, string version, string password, int userID, string hash, NetworkViewID targetID, NetworkMessageInfo msg)
	{
		base.StartCoroutine(this.CheckConnectionAndSendReason(privilege, version, password, userID, targetID, msg, hash));
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
	private IEnumerator CheckConnectionAndSendReason(int privilege, string version, string password, int userID, NetworkViewID targetID, NetworkMessageInfo msg, string hash = "")
	{
		if (this._filterDoubleConnection.Contains(msg.sender))
		{
			yield break;
		}
		this._filterDoubleConnection.Add(msg.sender);
		string reason = "no reason";
		if (ServerLeagueSystem.Enabled)
		{
			int leagueConnectAttempts = 0;
			while (!ServerLeagueSystem.CanConnect(userID))
			{
				leagueConnectAttempts++;
				yield return new WaitForSeconds(ServerLeagueSystem.TimeTilNewDataRefresh);
				while (!ServerLeagueSystem.IsDataRefreshedFor(userID))
				{
					yield return new WaitForSeconds(1f);
				}
				if (leagueConnectAttempts >= 5)
				{
					reason = Language.WrongPassword;
					goto IL_142;
				}
			}
			ServerLeagueSystem.Connect(userID);
		}
		else if (Peer.IsTemporaryBanned(userID))
		{
			reason = Language.TeamKillKick;
		}
		IL_142:
		if (Peer.Info.PasswordProtected && password != eNetwork.password && privilege < 1)
		{
			reason = Language.WrongPassword;
		}
		else if (Peer.ServerGame.IsUserInGame(userID) && userID != IDUtil.AdminID && userID != IDUtil.GuestID && userID != IDUtil.BotID)
		{
			reason = Language.YouAreAlreadyAtServer;
		}
		else if (Peer.IsClosing)
		{
			reason = Language.ServerRestarted;
		}
		else if (version != Globals.I.version)
		{
			reason = Language.ClientNotMatchTheServerVersion;
		}
		else if (!Peer.ServerGame.HaveSlot && userID != IDUtil.AdminID && privilege < 1)
		{
			reason = Peer.ServerGame.HaveSlotText;
		}
		if (reason == "no reason")
		{
			if (this.CanAddPlayer(msg.sender.guid))
			{
				Peer.ServerGame.AddPlayer(userID, targetID, msg.sender.guid, hash);
			}
		}
		else
		{
			this.SendReason(msg, reason);
		}
		yield break;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x0001F03C File Offset: 0x0001D23C
	private void SendReason(NetworkMessageInfo msg, string reason)
	{
		try
		{
			if (msg.sender != default(NetworkPlayer))
			{
				eNetwork.RPC("OpenConnectionFailed", new eNetworkPlayer(msg.sender), new object[]
				{
					reason
				});
				base.StartCoroutine(this.CloseConnection(new eNetworkPlayer(msg), 0.5f));
			}
			else
			{
				this.OpenConnectionFailed(reason);
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
	private bool CanAddPlayer(string guid)
	{
		return Peer.ServerGame.LoadingNetPlayers.All((ServerNetPlayer loadPlayer) => loadPlayer.guid != guid) && Peer.ServerGame.ServerNetPlayers.All((ServerNetPlayer netPlayer) => netPlayer.guid != guid);
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0001F12C File Offset: 0x0001D32C
	[RPC]
	[Obfuscation(Exclude = true)]
	public void OpenConnectionFailed(string reason)
	{
		Peer.Disconnect(true);
		foreach (OnPeerEvent onPeerEvent in this.events)
		{
			onPeerEvent.OnConnectionFailed();
		}
		Utility.ShowDisconnectReason(Language.Connection, reason);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0001F1A4 File Offset: 0x0001D3A4
	[Obfuscation(Exclude = true)]
	[RPC]
	public void OpenConnectionFinished(int userID, NetworkViewID myID, NetworkViewID targetID, int playerID, int group, float serverTime, bool isSnow)
	{
		CVars.Snow = isSnow;
		EventFactory.Call("HidePopup", new Popup(WindowsID.Connection, Language.Connection, Language.ConnectionCompleted, PopupState.information, false, true, string.Empty, string.Empty));
		global::Console.print("Join Finished", Color.green);
		for (int i = 0; i < this.events.Count; i++)
		{
			this.events[i].OnConnectionSuccessful();
		}
		if (this._clientGame == null)
		{
			this._clientGame = this.CreateGame();
		}
		if (userID == IDUtil.BotID)
		{
			this._clientGame.BotMainInitialize(myID, targetID, playerID, group, serverTime);
		}
		else
		{
			this._clientGame.MainInitialize(myID, targetID, playerID, group, serverTime);
			this._allGamesLoaded = true;
			if (!Peer.Dedicated && !Peer.IsHost)
			{
				if (ClientLeagueSystem.IsLeagueGame)
				{
					ClientLeagueSystem.MapLoading();
				}
				else
				{
					EventFactory.Call("HideInterface", null);
					EventFactory.Call("ShowLoading", null);
				}
				MainGUI mainGUI = Forms.Get(typeof(MainGUI)) as MainGUI;
				Loader.DownloadGameData(Main.UserInfo.settings.loaderSettings, Peer.Info.MapName, mainGUI.currentSuit);
			}
		}
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0001F2F0 File Offset: 0x0001D4F0
	private BaseClientGame CreateGame()
	{
		BaseClientGame baseClientGame = null;
		switch (Peer.Info.GameMode)
		{
		case GameMode.Deathmatch:
			baseClientGame = SingletoneForm<PoolManager>.Instance["client_dm_game"].Spawn().GetComponent<ClientDeathMatchGame>();
			break;
		case GameMode.TeamElimination:
			baseClientGame = SingletoneForm<PoolManager>.Instance["client_te_game"].Spawn().GetComponent<ClientTeamEliminationGame>();
			break;
		case GameMode.TargetDesignation:
			baseClientGame = SingletoneForm<PoolManager>.Instance["client_td_game"].Spawn().GetComponent<ClientTargetDesignationGame>();
			break;
		case GameMode.TacticalConquest:
			baseClientGame = SingletoneForm<PoolManager>.Instance["client_tc_game"].Spawn().GetComponent<ClientTacticalConquestGame>();
			break;
		}
		if (baseClientGame != null)
		{
			baseClientGame.transform.parent = null;
			return baseClientGame;
		}
		throw new Exception("Cant create client game");
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0001F3CC File Offset: 0x0001D5CC
	public override void MainInitialize()
	{
		base.networkView.group = 0;
		this.isUpdating = true;
		base.MainInitialize();
		if (this.gui)
		{
			this._searchGamesGui = this.gui.GetComponent<SearchGamesGUI>();
		}
		if (this.gui)
		{
			this._searchPopupGui = this.gui.GetComponent<PopupGUI>();
		}
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0001F434 File Offset: 0x0001D634
	public override void OnDisconnect()
	{
		base.StopAllCoroutines();
		base.CancelInvoke("OnDelayedDisconnect");
		if (this._serverGame)
		{
			PoolManager.Despawn(this._serverGame.gameObject);
			this._serverGame = null;
		}
		if (this._clientGame)
		{
			CVars.Snow = false;
			PoolManager.Despawn(this._clientGame.gameObject);
			this._clientGame = null;
		}
		this._dedicated = false;
		this._isHost = false;
		Peer.PeerType = NetworkPeerType.Disconnected;
		if (Peer.PeerType == NetworkPeerType.Server)
		{
			HttpMasterServer.UnregisterHost();
		}
		this._hostInfo = null;
		Peer.GamesListLoaded = false;
		this._allGamesLoaded = false;
		this._isHidden = false;
		this._isClosing = false;
		eNetwork.Disconnect();
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
	public override void OnUpdate()
	{
		if (this._registerTimer < Time.time && Peer.PeerType == NetworkPeerType.Server)
		{
			this._registerTimer = Time.time + Peer.RepeatRegisterEverySeconds;
			this.RegisterServer();
		}
		if (Peer.GamesListLoaded && ((this._searchGamesGui && this._searchGamesGui.Visible) || (this._searchPopupGui && this._searchPopupGui.IsShowingQuickGame) || (this._searchPopupGui && this._searchPopupGui.IsFirstTimeInGame)))
		{
			Peer.GamesListLoaded = false;
			if (Peer.PeerType == NetworkPeerType.Disconnected || Peer.PeerType == NetworkPeerType.Connecting)
			{
				PingManager.Clear();
				Peer.games.Clear();
				HostInfo[] array = HttpMasterServer.PollHostList();
				if (array != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						Peer.games.Add(array[i]);
					}
					HttpMasterServer.ClearHostList();
				}
			}
		}
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0001F5FC File Offset: 0x0001D7FC
	private void RegisterServer()
	{
		if (Peer.PeerType != NetworkPeerType.Server)
		{
			return;
		}
		if (Main.UserInfo.Permission <= EPermission.Tester)
		{
			Peer.Info.Ranked = false;
		}
		if (this._isHidden || this._isClosing)
		{
			return;
		}
		Peer.Info.ip = ((!string.IsNullOrEmpty(Main.HostInfo.ExternalIp)) ? Main.HostInfo.ExternalIp : Network.player.ipAddress);
		if (ServerLeagueSystem.Enabled)
		{
			Dictionary<string, object> dict = new Dictionary<string, object>();
			this._hostInfo.Convert(dict, true);
			ServerLeagueSystem.Register(dict);
		}
		else
		{
			string data = Peer.Info.ToJSON();
			HttpMasterServer.RegisterHost(CVars.realm, Peer.Info.ip, Peer.Info.Port.ToString(), data);
		}
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0001F6DC File Offset: 0x0001D8DC
	public void AddPeerObserver(OnPeerEvent peerEvent)
	{
		if (!this.events.Contains(peerEvent))
		{
			this.events.Add(peerEvent);
		}
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0001F6FC File Offset: 0x0001D8FC
	public void RemovePeerObserver(OnPeerEvent peerEvent)
	{
		if (this.events.Contains(peerEvent))
		{
			this.events.Remove(peerEvent);
		}
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0001F71C File Offset: 0x0001D91C
	public static void TemporaryBan(int id, float time)
	{
		if (!Peer.TemporaryBanned.ContainsKey(id))
		{
			Peer.TemporaryBanned.Add(id, Time.time + time);
		}
		else
		{
			Peer.TemporaryBanned[id] = Time.time + time;
		}
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0001F764 File Offset: 0x0001D964
	private static bool IsTemporaryBanned(int id)
	{
		return Peer.TemporaryBanned.ContainsKey(id) && Peer.TemporaryBanned[id] > Time.time;
	}

	// Token: 0x04000440 RID: 1088
	private SearchGamesGUI _searchGamesGui;

	// Token: 0x04000441 RID: 1089
	private PopupGUI _searchPopupGui;

	// Token: 0x04000442 RID: 1090
	private HostInfo _hostInfo;

	// Token: 0x04000443 RID: 1091
	private BaseServerGame _serverGame;

	// Token: 0x04000444 RID: 1092
	private BaseClientGame _clientGame;

	// Token: 0x04000445 RID: 1093
	private float _registerTimer;

	// Token: 0x04000446 RID: 1094
	private bool _dedicated;

	// Token: 0x04000447 RID: 1095
	private bool _isHost;

	// Token: 0x04000448 RID: 1096
	private bool _isHidden;

	// Token: 0x04000449 RID: 1097
	private bool _isClosing;

	// Token: 0x0400044A RID: 1098
	private bool _allGamesLoaded;

	// Token: 0x0400044B RID: 1099
	private NetworkPeerType _peerType;

	// Token: 0x0400044C RID: 1100
	private List<NetworkPlayer> _filterDoubleConnection = new List<NetworkPlayer>(32);

	// Token: 0x0400044D RID: 1101
	private static readonly Dictionary<int, float> TemporaryBanned = new Dictionary<int, float>();

	// Token: 0x0400044E RID: 1102
	public static List<HostInfo> games = new List<HostInfo>();

	// Token: 0x0400044F RID: 1103
	public static bool GamesListLoaded;

	// Token: 0x04000450 RID: 1104
	[HideInInspector]
	public List<OnPeerEvent> events = new List<OnPeerEvent>();
}
