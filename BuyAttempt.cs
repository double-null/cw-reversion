using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001ED RID: 493
internal class BuyAttempt : DatabaseEvent
{
	// Token: 0x06001048 RID: 4168 RVA: 0x000B8758 File Offset: 0x000B6958
	public override void Initialize(params object[] args)
	{
		this.attemptsCount = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		global::Console.print("Buy attempts. Request sending...", Color.grey);
		HtmlLayer.Request("?action=buyRoulette&count=" + this.attemptsCount, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001049 RID: 4169 RVA: 0x000B87C4 File Offset: 0x000B69C4
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary;
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
			this.OnFail(new Exception(Language.DataBaseFailure));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception(Language.DataBaseFailure));
			return;
		}
		Main.UserInfo.Attempts += Convert.ToInt32(this.attemptsCount);
		Main.UserInfo.GP -= Globals.I.RouletteInfo.AttemptCost * Convert.ToInt32(this.attemptsCount);
		this.SuccessAction();
		global::Console.print("Attempt added", Color.green);
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x000B88CC File Offset: 0x000B6ACC
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}

	// Token: 0x040010C2 RID: 4290
	private string attemptsCount = string.Empty;
}
