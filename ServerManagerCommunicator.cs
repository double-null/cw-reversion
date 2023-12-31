using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using UnityEngine;

// Token: 0x02000098 RID: 152
public class ServerManagerCommunicator : SingletoneForm<ServerManagerCommunicator>
{
	// Token: 0x0600036E RID: 878 RVA: 0x000190C8 File Offset: 0x000172C8
	private IEnumerator Start()
	{
		for (;;)
		{
			yield return new WaitForSeconds(30f);
			Dictionary<string, object> param = new Dictionary<string, object>
			{
				{
					"command",
					"room_keepalive"
				}
			};
			Dictionary<string, object> args = new Dictionary<string, object>
			{
				{
					"process_id",
					Process.GetCurrentProcess().Id
				},
				{
					"players_count",
					this._lastPlayersCount
				},
				{
					"matches_count",
					this._lastMatchesCount
				},
				{
					"match_state",
					Peer.ServerGame.MatchState
				},
				{
					"next_event_time",
					Peer.ServerGame.ElapsedNextEventTime
				}
			};
			param.Add("params", args);
			this.Post(param);
		}
		yield break;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x000190E4 File Offset: 0x000172E4
	public void SendCurrentPlayersCount(int playersCount)
	{
		this._lastPlayersCount = playersCount;
	}

	// Token: 0x06000370 RID: 880 RVA: 0x000190F0 File Offset: 0x000172F0
	public void SendMatchesLeftCount(int matchesCount)
	{
		this._lastMatchesCount = matchesCount;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x000190FC File Offset: 0x000172FC
	private void Post(Dictionary<string, object> postData)
	{
		try
		{
			string requestUriString = string.Concat(new object[]
			{
				"http://",
				Peer.Info.Ip,
				":",
				13000
			});
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
			httpWebRequest.ContentType = "application/json";
			httpWebRequest.Method = "POST";
			httpWebRequest.BeginGetRequestStream(delegate(IAsyncResult result)
			{
				this.GetRequestStreamCallback(result, postData);
			}, httpWebRequest);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("Can't send data to server manager: " + ex.Message);
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000191C8 File Offset: 0x000173C8
	private void GetRequestStreamCallback(IAsyncResult result, Dictionary<string, object> postData)
	{
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
			Stream stream = httpWebRequest.EndGetRequestStream(result);
			using (StreamWriter streamWriter = new StreamWriter(stream))
			{
				string value = ArrayUtility.ToJSON<string, object>(postData);
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.LogError("Can't send data to server manager on get request stream callback: " + ex.Message);
		}
	}

	// Token: 0x04000393 RID: 915
	private const int ServerManagerPort = 13000;

	// Token: 0x04000394 RID: 916
	private const float KeepAliveSendInterval = 30f;

	// Token: 0x04000395 RID: 917
	private int _lastPlayersCount;

	// Token: 0x04000396 RID: 918
	private int _lastMatchesCount;
}
