using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000209 RID: 521
internal class CheckClanTagColor : DatabaseEvent
{
	// Token: 0x060010B4 RID: 4276 RVA: 0x000BB860 File Offset: 0x000B9A60
	public override void Initialize(params object[] args)
	{
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		int num = 3;
		HtmlLayer.Request(string.Concat(new object[]
		{
			"clans.php?action=check&needle=",
			text,
			"&haystack_type=",
			num
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010B5 RID: 4277 RVA: 0x000BB8D4 File Offset: 0x000B9AD4
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
			this.SuccessAction();
		}
		else if ((int)dictionary["result"] == 1)
		{
			this.FailedAction();
		}
	}

	// Token: 0x060010B6 RID: 4278 RVA: 0x000BB9A0 File Offset: 0x000B9BA0
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}
}
