using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000218 RID: 536
internal class GetAttempts : DatabaseEvent
{
	// Token: 0x060010F0 RID: 4336 RVA: 0x000BD3AC File Offset: 0x000BB5AC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=getAttempts", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010F1 RID: 4337 RVA: 0x000BD3E8 File Offset: 0x000BB5E8
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
		Main.UserInfo.Attempts = (int)dictionary["attempts"];
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x000BD4A4 File Offset: 0x000BB6A4
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}
}
