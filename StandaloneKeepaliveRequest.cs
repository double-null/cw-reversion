using System;
using System.Collections.Generic;
using JsonFx.Json;
using UnityEngine;

// Token: 0x020000C1 RID: 193
internal class StandaloneKeepaliveRequest : DatabaseEvent
{
	// Token: 0x06000502 RID: 1282 RVA: 0x000208FC File Offset: 0x0001EAFC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=keepalive", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00020938 File Offset: 0x0001EB38
	protected override void OnResponse(string text, string url)
	{
		Dictionary<string, object> dictionary = StandaloneKeepaliveRequest.FromJSON(text, string.Empty);
		if (!(bool)dictionary["data"])
		{
			this.OnJsonDataFalse();
		}
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002096C File Offset: 0x0001EB6C
	private static Dictionary<string, object> FromJSON(string text, string name = "")
	{
		Dictionary<string, object> result;
		try
		{
			if (name == string.Empty)
			{
				result = (Dictionary<string, object>)JsonReader.Deserialize(text);
			}
			else
			{
				result = (Dictionary<string, object>)((Dictionary<string, object>)JsonReader.Deserialize(text))[name];
			}
		}
		catch (Exception)
		{
			Debug.LogError("StandaloneKeepaliveRequest. parsing json error. NAME = " + ((name != null) ? name : "NULL"));
			result = new Dictionary<string, object>();
		}
		return result;
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x00020A0C File Offset: 0x0001EC0C
	private void OnJsonDataFalse()
	{
		HtmlLayer.Cookies.Clear();
		Main.AddDatabaseRequest<InitUser>(new object[]
		{
			true
		});
	}

	// Token: 0x0400048C RID: 1164
	private const string KeepAliveAction = "?action=keepalive";
}
