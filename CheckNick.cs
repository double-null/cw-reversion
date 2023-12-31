using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F6 RID: 502
[AddComponentMenu("Scripts/Game/Events/CheckNick")]
internal class CheckNick : DatabaseEvent
{
	// Token: 0x0600106A RID: 4202 RVA: 0x000B9704 File Offset: 0x000B7904
	public override void Initialize(params object[] args)
	{
		string text = (string)Crypt.ResolveVariable(args, "Player", 0);
		text = text.Replace("\n", string.Empty);
		HtmlLayer.Request("checknick.php?nick=" + WWW.EscapeURL(text), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600106B RID: 4203 RVA: 0x000B9770 File Offset: 0x000B7970
	protected override void OnResponse(string text)
	{
		base.OnResponse(text);
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
		if ((int)dictionary["result"] == 0)
		{
			EventFactory.Call("NickAvailable", false);
		}
		else if ((int)dictionary["result"] == 1)
		{
			EventFactory.Call("NickAvailable", true);
		}
	}
}
