using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021B RID: 539
[AddComponentMenu("Scripts/Game/Events/GetRandomHint")]
internal class GetRandomHint : DatabaseEvent
{
	// Token: 0x060010FF RID: 4351 RVA: 0x000BD784 File Offset: 0x000BB984
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("getrandomhint.php?", new RequestFinished(this.GetRandomHintFinished), null, string.Empty, string.Empty);
	}

	// Token: 0x06001100 RID: 4352 RVA: 0x000BD7A8 File Offset: 0x000BB9A8
	public void GetRandomHintFinished(string text, string url)
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
		if ((int)dictionary["result"] == 0)
		{
			GetRandomHint.Hint = (string)dictionary["hint_text"];
		}
	}

	// Token: 0x040010E0 RID: 4320
	public static string Hint = string.Empty;
}
