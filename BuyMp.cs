using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F0 RID: 496
internal class BuyMp : DatabaseEvent
{
	// Token: 0x06001053 RID: 4179 RVA: 0x000B8C64 File Offset: 0x000B6E64
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		string actions = "adm.php?q=customization/player/mp/buy/" + num;
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001054 RID: 4180 RVA: 0x000B8CC0 File Offset: 0x000B6EC0
	protected override void OnResponse(string text)
	{
		Debug.Log(text);
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		if (dictionary.ContainsKey("result"))
		{
			if ((int)dictionary["result"] != 0)
			{
				this.FailedAction();
			}
			else
			{
				this.SuccessAction();
			}
		}
		else
		{
			this.FailedAction();
		}
	}
}
