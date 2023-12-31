using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x020000C3 RID: 195
[AddComponentMenu("Scripts/Engine/eNetwork")]
internal class eNetwork : SingletoneForm<eNetwork>
{
	// Token: 0x06000515 RID: 1301 RVA: 0x00020B40 File Offset: 0x0001ED40
	public static void RPC(string name, eNetworkPlayer player, params object[] args)
	{
		if (player.IsVirtual)
		{
			SingletoneForm<eNetwork>.Instance.StartCoroutine(SingletoneForm<eNetwork>.Instance.RPC_localcall(name, SingletoneForm<Peer>.Instance.networkView.viewID, args));
		}
		else
		{
			try
			{
				for (int i = 0; i < Network.connections.Length; i++)
				{
					if (Network.connections[i] == player.owner)
					{
						SingletoneForm<Peer>.Instance.networkView.RPC(name, player.owner, args);
					}
				}
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00020C00 File Offset: 0x0001EE00
	private IEnumerator RPC_localcall(string name, NetworkViewID reciever, params object[] args)
	{
		PoolableBehaviour external_reciever = SingletoneForm<Peer>.Instance;
		MethodInfo method = external_reciever.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		try
		{
			method.Invoke(external_reciever, args);
		}
		catch (Exception ex)
		{
			Exception e = ex;
			global::Console.exception(e);
		}
		yield return 0;
		yield break;
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x00020C30 File Offset: 0x0001EE30
	public static void DisableAllGroups(NetworkPlayer player)
	{
		eNetwork.SetReceivingEnabled(player, 0, true);
		eNetwork.SetSendingEnabled(player, 0, true);
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00020C44 File Offset: 0x0001EE44
	public static void SetSendingEnabled(NetworkPlayer player, int group, bool enabled)
	{
		for (int i = 0; i < Network.connections.Length; i++)
		{
			if (player == Network.connections[i])
			{
				Network.SetSendingEnabled(player, group, enabled);
			}
		}
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00020C8C File Offset: 0x0001EE8C
	public static void SetReceivingEnabled(NetworkPlayer player, int group, bool enabled)
	{
		for (int i = 0; i < Network.connections.Length; i++)
		{
			if (player == Network.connections[i])
			{
				Network.SetReceivingEnabled(player, group, enabled);
			}
		}
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00020CD4 File Offset: 0x0001EED4
	public static int Ping(eNetworkPlayer player)
	{
		if (player.IsVirtual)
		{
			return (int)CVars.n_ping;
		}
		return Network.GetAveragePing(player.owner);
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00020CF4 File Offset: 0x0001EEF4
	public static bool PlayerConnected(eNetworkPlayer player)
	{
		if (player.IsVirtual)
		{
			return true;
		}
		for (int i = 0; i < Network.connections.Length; i++)
		{
			if (Network.connections[i] == player.owner)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00020D4C File Offset: 0x0001EF4C
	public static NetworkConnectionError Connect(HostInfo hostInfo)
	{
		NetworkConnectionError result;
		if (hostInfo.ForceNAT)
		{
			result = Network.Connect(hostInfo.GUID);
		}
		else
		{
			result = Network.Connect(hostInfo.Ip, hostInfo.Port);
		}
		return result;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00020D8C File Offset: 0x0001EF8C
	public static NetworkConnectionError Connect(string Ip, int port)
	{
		return Network.Connect(Ip, port);
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00020D98 File Offset: 0x0001EF98
	public static void CloseConnection(eNetworkPlayer player, string title = "", string reason = "")
	{
		if (Peer.PeerType == NetworkPeerType.Server)
		{
			if (player.IsVirtual)
			{
				Peer.Disconnect(true);
				EventFactory.Call("ShowInterface", null);
				if (string.IsNullOrEmpty(title))
				{
					title = Language.Connection;
				}
				if (string.IsNullOrEmpty(reason))
				{
					reason = Language.ServerDisconnetProfileLoadError;
				}
				Utility.ShowDisconnectReason(title, reason);
			}
			else
			{
				try
				{
					for (int i = 0; i < Network.connections.Length; i++)
					{
						if (Network.connections[i] == player.owner)
						{
							Network.CloseConnection(player.owner, true);
							break;
						}
					}
				}
				catch (Exception e)
				{
					global::Console.exception(e);
				}
			}
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00020E74 File Offset: 0x0001F074
	public static void CloseAllConnections()
	{
		for (int i = 0; i < Network.connections.Length; i++)
		{
			Network.CloseConnection(Network.connections[i], true);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00020EB0 File Offset: 0x0001F0B0
	public static void Disconnect()
	{
		eNetwork.password = string.Empty;
		Network.Disconnect(200);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00020EC8 File Offset: 0x0001F0C8
	public override void MainInitialize()
	{
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00020ED8 File Offset: 0x0001F0D8
	public override void OnDisconnect()
	{
		base.StopAllCoroutines();
		base.OnDisconnect();
	}

	// Token: 0x04000493 RID: 1171
	public static string password = string.Empty;
}
