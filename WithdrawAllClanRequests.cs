using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FA RID: 506
internal class WithdrawAllClanRequests : DatabaseEvent
{
	// Token: 0x06001078 RID: 4216 RVA: 0x000B9CE8 File Offset: 0x000B7EE8
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=resetClanRequests", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x000B9D24 File Offset: 0x000B7F24
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
			this.OnFail(new Exception("Data Server Error"));
			if (Application.isEditor || Peer.Dedicated)
			{
				throw;
			}
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error"));
		}
		else
		{
			this.SuccessAction();
		}
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x000B9DEC File Offset: 0x000B7FEC
	protected override void OnFail(Exception e)
	{
		this.FailedAction();
	}
}
