using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B0 RID: 176
internal class HttpMasterServer
{
	// Token: 0x1700009B RID: 155
	// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
	public static HttpMasterServer I
	{
		get
		{
			if (HttpMasterServer.instance == null)
			{
				HttpMasterServer.instance = new HttpMasterServer();
			}
			return HttpMasterServer.instance;
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001CCDC File Offset: 0x0001AEDC
	public static HostInfo[] PollHostList()
	{
		return HttpMasterServer.collectedHosts;
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
	public static void ClearHostList()
	{
		HttpMasterServer.collectedHosts = null;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0001CCEC File Offset: 0x0001AEEC
	public static void RequestHostList(string realm)
	{
		if (!HttpMasterServer.queued)
		{
			HttpMasterServer.queued = true;
			if (CVars.realm == "local")
			{
				HtmlLayer.RequestCompressed("ms/?action=list&realm=" + CVars.realm, new RequestFinished(HttpMasterServer.RequestHostListFinished), new RequestFailed(HttpMasterServer.RequestHostListFailed), string.Empty, string.Empty);
			}
			else
			{
				HtmlLayer.RequestCompressed("?action=gethosts&type=with_friends&version=" + CVars.Version, new RequestFinished(HttpMasterServer.RequestHostListFinished), new RequestFailed(HttpMasterServer.RequestHostListFailed), string.Empty, string.Empty);
			}
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0001CD90 File Offset: 0x0001AF90
	private static void RequestHostListFinished(string text, string url)
	{
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		HttpMasterServer.queued = false;
		try
		{
			Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
			Main.CheckVersion(dictionary);
			Dictionary<string, object>[] array = dictionary["hosts"] as Dictionary<string, object>[];
			if (array != null)
			{
				HttpMasterServer.collectedHosts = new HostInfo[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					HttpMasterServer.collectedHosts[i] = new HostInfo();
					HttpMasterServer.collectedHosts[i].Read(array[i]);
				}
			}
			Peer.GamesListLoaded = true;
		}
		catch (Exception innerException)
		{
			Debug.Log(new Exception("URL:" + url, innerException));
			throw;
		}
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0001CE5C File Offset: 0x0001B05C
	private static void RequestHostListFailed(Exception e, string url)
	{
		HttpMasterServer.queued = false;
		Debug.Log(e);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001CE6C File Offset: 0x0001B06C
	public static void RegisterHost(string realm, string ip, string port, string data)
	{
		HtmlLayer.SendCompressed("ms/?action=register", data, new RequestFinished(HttpMasterServer.RegisterFinished), new RequestFailed(HttpMasterServer.RegisterFailed));
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0001CE94 File Offset: 0x0001B094
	private static void RegisterFinished(string text, string url)
	{
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x0001CE98 File Offset: 0x0001B098
	private static void RegisterFailed(Exception e, string url)
	{
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0001CE9C File Offset: 0x0001B09C
	public static void UnregisterHost(string realm, string ip, string port)
	{
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001CEA0 File Offset: 0x0001B0A0
	public static void UnregisterHost()
	{
	}

	// Token: 0x0400041A RID: 1050
	public string masterserverIP = string.Empty;

	// Token: 0x0400041B RID: 1051
	public string masterserverURI = string.Empty;

	// Token: 0x0400041C RID: 1052
	public static HttpMasterServer instance;

	// Token: 0x0400041D RID: 1053
	public static HostInfo[] collectedHosts;

	// Token: 0x0400041E RID: 1054
	public static bool queued;
}
