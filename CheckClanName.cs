using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000208 RID: 520
internal class CheckClanName : DatabaseEvent
{
	// Token: 0x060010B0 RID: 4272 RVA: 0x000BB704 File Offset: 0x000B9904
	public override void Initialize(params object[] args)
	{
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		int num = 2;
		HtmlLayer.Request(string.Concat(new object[]
		{
			"clans.php?action=check&needle=",
			text,
			"&haystack_type=",
			num
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010B1 RID: 4273 RVA: 0x000BB778 File Offset: 0x000B9978
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

	// Token: 0x060010B2 RID: 4274 RVA: 0x000BB844 File Offset: 0x000B9A44
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}
}
