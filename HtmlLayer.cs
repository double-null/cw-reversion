using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ComponentAce.Compression.Libs.zlib;
using UnityEngine;

// Token: 0x020000AF RID: 175
[AddComponentMenu("Scripts/Engine/HtmlLayer")]
internal class HtmlLayer : SingletoneComponent<HtmlLayer>
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600042F RID: 1071 RVA: 0x0001C830 File Offset: 0x0001AA30
	private static DateTime UtcStart
	{
		get
		{
			DateTime? dateTime = HtmlLayer.utcStart;
			if (dateTime == null)
			{
				HtmlLayer.utcStart = new DateTime?(new DateTime(1970, 1, 1));
			}
			return HtmlLayer.utcStart.Value;
		}
	}

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000430 RID: 1072 RVA: 0x0001C874 File Offset: 0x0001AA74
	private static DateTime UtcNow
	{
		get
		{
			if (Time.realtimeSinceStartup - HtmlLayer.lastUpdateTime <= 0.1f)
			{
				DateTime? dateTime = HtmlLayer.utcNow;
				if (dateTime != null)
				{
					goto IL_43;
				}
			}
			HtmlLayer.utcNow = new DateTime?(DateTime.UtcNow);
			HtmlLayer.lastUpdateTime = Time.realtimeSinceStartup;
			IL_43:
			return HtmlLayer.utcNow.Value;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000431 RID: 1073 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
	private int deltaUtc
	{
		get
		{
			return (int)(HtmlLayer.UtcNow - HtmlLayer.UtcStart).TotalSeconds - this.utcLocalTime;
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000432 RID: 1074 RVA: 0x0001C8FC File Offset: 0x0001AAFC
	public static int serverUtc
	{
		get
		{
			return SingletoneComponent<HtmlLayer>.Instance.utcServerTime + SingletoneComponent<HtmlLayer>.Instance.deltaUtc;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001C914 File Offset: 0x0001AB14
	// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001C920 File Offset: 0x0001AB20
	public static List<string> Cookies
	{
		get
		{
			return SingletoneComponent<HtmlLayer>.instance.cookies;
		}
		set
		{
			SingletoneComponent<HtmlLayer>.instance.cookies = value;
		}
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0001C930 File Offset: 0x0001AB30
	private IEnumerator DirectRequestCorotine(string url, RequestFinished finished = null, RequestFailed failed = null)
	{
		string responce = string.Empty;
		Request request = new Request("GET", url, true, false);
		byte[] bytes = new byte[0];
		request.bytes = bytes;
		request.Send();
		while (!request.isDone)
		{
			yield return new WaitForSeconds(0.5f);
		}
		if (request.exception != null && failed != null)
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log(url);
			}
			failed(request.exception, url);
			yield break;
		}
		yield return new WaitForSeconds(0.5f);
		responce = SimpleZlib.Decompress(request.response.bytes, null);
		if (string.IsNullOrEmpty(responce))
		{
			responce = Encoding.UTF8.GetString(request.response.bytes);
		}
		if (string.IsNullOrEmpty(responce))
		{
			responce = "NULL";
		}
		if (CVars.n_httpDebug)
		{
			Debug.Log("RESPONCE: " + responce);
		}
		if (finished != null)
		{
			finished(responce, url);
		}
		yield break;
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001C970 File Offset: 0x0001AB70
	public static void DirectRequest(string url, RequestFinished finished = null, RequestFailed failed = null)
	{
		SingletoneComponent<HtmlLayer>.instance.StartCoroutine(SingletoneComponent<HtmlLayer>.instance.DirectRequestCorotine(url, finished, failed));
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0001C98C File Offset: 0x0001AB8C
	public IEnumerator AsyncRequest(string actions, RequestFinished finished = null, RequestFailed failed = null, string postUnencoded = "", string postEncoded = "", bool decompress = false)
	{
		string request_uri = actions + "&expire=" + ((double)(HtmlLayer.serverUtc + 3600)).ToString();
		string url = string.Concat(new string[]
		{
			CVars.n_protocol,
			WWWUtil.databaseWWW,
			request_uri,
			"&sig=",
			Crypt.getMD5Hash(Main.AuthData.sharedSecret + request_uri + postUnencoded)
		});
		if (CVars.n_httpDebug)
		{
			global::Console.print(url, Color.yellow);
		}
		string responce = string.Empty;
		Request request = new Request("POST", url, true, false);
		byte[] bytes = new byte[1];
		request.bytes = bytes;
		request.Send();
		while (!request.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		if (request.exception != null && failed != null)
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log(url);
			}
			failed(request.exception, url);
			yield break;
		}
		yield return new WaitForEndOfFrame();
		responce = SimpleZlib.Decompress(request.response.bytes, null);
		if (string.IsNullOrEmpty(responce))
		{
			responce = Encoding.UTF8.GetString(request.response.bytes);
		}
		if (string.IsNullOrEmpty(responce))
		{
			responce = "NULL";
		}
		if (CVars.n_httpDebug)
		{
			Debug.Log("url: " + url + " \nRESPONSE: " + responce);
		}
		if (finished != null)
		{
			finished(responce, url);
		}
		yield break;
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0001C9DC File Offset: 0x0001ABDC
	public static void Request(string actions, RequestFinished finished = null, RequestFailed failed = null, string postUnencoded = "", string postEncoded = "")
	{
		SingletoneComponent<HtmlLayer>.instance.StartCoroutine(SingletoneComponent<HtmlLayer>.instance.AsyncRequest(actions, finished, failed, postUnencoded, postEncoded, false));
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0001CA08 File Offset: 0x0001AC08
	public static void RequestCompressed(string actions, RequestFinished finished = null, RequestFailed failed = null, string postUnencoded = "", string postEncoded = "")
	{
		SingletoneComponent<HtmlLayer>.instance.StartCoroutine(SingletoneComponent<HtmlLayer>.instance.AsyncRequest(actions, finished, failed, postUnencoded, postEncoded, true));
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0001CA34 File Offset: 0x0001AC34
	public IEnumerator AsyncSendCompressed(string actions, string data, RequestFinished finished = null, RequestFailed failed = null)
	{
		string request_uri = actions + "&expire=" + ((double)(HtmlLayer.serverUtc + 3600)).ToString();
		string url = string.Concat(new string[]
		{
			CVars.n_protocol,
			WWWUtil.databaseWWW,
			request_uri,
			"&sig=",
			Crypt.getMD5Hash(Main.AuthData.sharedSecret + request_uri + "json=" + data)
		});
		string responce = string.Empty;
		Request request = new Request("POST", url, true, false);
		byte[] bytes = new byte[0];
		bytes = SimpleZlib.CompressToBytes(data, 9, null);
		request.bytes = bytes;
		request.Send();
		while (!request.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		if (request.exception != null && failed != null)
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log(url);
			}
			failed(request.exception, url);
			yield break;
		}
		yield return new WaitForEndOfFrame();
		responce = SimpleZlib.Decompress(request.response.bytes, null);
		if (string.IsNullOrEmpty(responce))
		{
			responce = Encoding.UTF8.GetString(request.response.bytes);
		}
		if (string.IsNullOrEmpty(responce))
		{
			responce = "NULL";
		}
		if (CVars.n_httpDebug)
		{
			Debug.Log("url : " + url + " \nRESPONCE: " + responce);
		}
		if (finished != null)
		{
			finished(responce, url);
		}
		yield break;
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0001CA84 File Offset: 0x0001AC84
	public IEnumerator AsyncSendFile(string actions, byte[] data, RequestFinished finished = null, RequestFailed failed = null)
	{
		double expire_time = (double)(HtmlLayer.serverUtc + 3600);
		string url = CVars.n_protocol + WWWUtil.databaseWWW + actions;
		string responce = string.Empty;
		Request request = new Request("POST", url, false, true);
		byte[] bytes = new byte[0];
		bytes = data;
		request.bytes = bytes;
		request.Send();
		while (!request.isDone)
		{
			yield return new WaitForEndOfFrame();
		}
		if (request.exception != null && failed != null)
		{
			if (CVars.n_httpDebug)
			{
				Debug.Log(url);
			}
			failed(request.exception, url);
			yield break;
		}
		yield return new WaitForEndOfFrame();
		responce = SimpleZlib.Decompress(request.response.bytes, null);
		if (string.IsNullOrEmpty(responce))
		{
			responce = Encoding.UTF8.GetString(request.response.bytes);
		}
		if (string.IsNullOrEmpty(responce))
		{
			responce = "NULL";
		}
		if (CVars.n_httpDebug)
		{
			Debug.Log("url : " + url + " \nRESPONCE: " + responce);
		}
		if (finished != null)
		{
			finished(responce, url);
		}
		yield break;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001CAD4 File Offset: 0x0001ACD4
	private void CheckCookie(WWW client)
	{
		if (HtmlLayer.headers.Count < 1 && client.responseHeaders.ContainsKey("SET-COOKIE"))
		{
			HtmlLayer.headers.Clear();
			HtmlLayer.headers.Add("COOKIE", client.responseHeaders["SET-COOKIE"]);
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0001CB30 File Offset: 0x0001AD30
	public static void SendCompressed(string actions, string data, RequestFinished finished = null, RequestFailed failed = null)
	{
		SingletoneComponent<HtmlLayer>.instance.StartCoroutine(SingletoneComponent<HtmlLayer>.instance.AsyncSendCompressed(actions, data, finished, failed));
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001CB4C File Offset: 0x0001AD4C
	public static void SendFile(string actions, byte[] data, RequestFinished finished = null, RequestFailed failed = null)
	{
		SingletoneComponent<HtmlLayer>.instance.StartCoroutine(SingletoneComponent<HtmlLayer>.instance.AsyncSendFile(actions, data, finished, failed));
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0001CB68 File Offset: 0x0001AD68
	public static void ExceptionRequest(string actions, RequestFinished finished = null, RequestFailed failed = null, string postUnencoded = "", string postEncoded = "")
	{
		string str = actions + "&expire=" + ((double)(HtmlLayer.serverUtc + 3600)).ToString();
		string url = CVars.n_protocol + Globals.I.exceptionWWW + str + "&sig=";
		BaseHttpRequest baseHttpRequest;
		if (!CVars.n_unityhttp && !Peer.Dedicated)
		{
			baseHttpRequest = GameObject.Find("net").AddComponent<BaseHttpRequest>();
		}
		else
		{
			baseHttpRequest = GameObject.Find("net").AddComponent<UnityHttpRequest>();
		}
		baseHttpRequest.Init(finished, failed, string.Empty);
		baseHttpRequest.Download(url, postEncoded);
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0001CC00 File Offset: 0x0001AE00
	public static void InitServerTime(Dictionary<string, object> dict)
	{
		SingletoneComponent<HtmlLayer>.Instance.utcLocalTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
		JSON.ReadWrite(dict, "time", ref SingletoneComponent<HtmlLayer>.Instance.utcServerTime, false);
		if (CVars.n_httpDebug)
		{
			global::Console.print("Local time is " + SingletoneComponent<HtmlLayer>.Instance.utcLocalTime, Color.yellow);
			global::Console.print("Server time is " + SingletoneComponent<HtmlLayer>.Instance.utcServerTime, Color.yellow);
		}
	}

	// Token: 0x04000412 RID: 1042
	public static Hashtable headers = new Hashtable();

	// Token: 0x04000413 RID: 1043
	public static float failDelay = 5f;

	// Token: 0x04000414 RID: 1044
	private static DateTime? utcStart;

	// Token: 0x04000415 RID: 1045
	private static DateTime? utcNow;

	// Token: 0x04000416 RID: 1046
	private static float lastUpdateTime = -1f;

	// Token: 0x04000417 RID: 1047
	private int utcServerTime;

	// Token: 0x04000418 RID: 1048
	private int utcLocalTime;

	// Token: 0x04000419 RID: 1049
	private List<string> cookies = new List<string>();
}
