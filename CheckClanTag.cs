using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000207 RID: 519
internal class CheckClanTag : DatabaseEvent
{
	// Token: 0x060010AC RID: 4268 RVA: 0x000BB5A8 File Offset: 0x000B97A8
	public override void Initialize(params object[] args)
	{
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		int num = 1;
		HtmlLayer.Request(string.Concat(new object[]
		{
			"clans.php?action=check&needle=",
			text,
			"&haystack_type=",
			num
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010AD RID: 4269 RVA: 0x000BB61C File Offset: 0x000B981C
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

	// Token: 0x060010AE RID: 4270 RVA: 0x000BB6E8 File Offset: 0x000B98E8
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
		this.FailedAction();
	}
}
