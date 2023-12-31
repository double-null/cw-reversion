using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000213 RID: 531
[AddComponentMenu("Scripts/Game/Events/Contracts")]
internal class Contracts : DatabaseEvent
{
	// Token: 0x060010D9 RID: 4313 RVA: 0x000BCC38 File Offset: 0x000BAE38
	public override void Initialize(params object[] args)
	{
		global::Console.print("Contracts", Color.grey);
		HtmlLayer.Request("?action=get_contracts", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010DA RID: 4314 RVA: 0x000BCC78 File Offset: 0x000BAE78
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error:" + dictionary["error"]));
		}
		else
		{
			global::Console.print("Contracts get Finished", Color.green);
			if (!dictionary.ContainsKey("user_id") || !dictionary.ContainsKey("contracts") || dictionary.ContainsKey("timer"))
			{
			}
		}
	}

	// Token: 0x060010DB RID: 4315 RVA: 0x000BCD70 File Offset: 0x000BAF70
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
