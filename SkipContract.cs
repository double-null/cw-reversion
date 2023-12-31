using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000236 RID: 566
internal class SkipContract : DatabaseEvent
{
	// Token: 0x06001187 RID: 4487 RVA: 0x000C3098 File Offset: 0x000C1298
	public override void Initialize(params object[] args)
	{
		object obj = Crypt.ResolveVariable(args, string.Empty, 0);
		object obj2 = Crypt.ResolveVariable(args, string.Empty, 1);
		global::Console.print("Skip contract. Request sending...", Color.grey);
		HtmlLayer.Request(string.Concat(new object[]
		{
			"?action=skipContract&uid=",
			Main.UserInfo.userID,
			"&type=",
			obj,
			"&id=",
			obj2
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001188 RID: 4488 RVA: 0x000C3130 File Offset: 0x000C1330
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			this.OnFail(ex);
			return;
		}
		object value;
		dictionary.TryGetValue("result", out value);
		if (Convert.ToInt32(value) == 0)
		{
			this.SuccessAction();
		}
		else
		{
			object arg;
			dictionary.TryGetValue("message", out arg);
			this.OnFail(new Exception("Returns bad result in SkipContract: " + arg));
		}
	}

	// Token: 0x06001189 RID: 4489 RVA: 0x000C31D8 File Offset: 0x000C13D8
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
