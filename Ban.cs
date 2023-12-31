using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EC RID: 492
[AddComponentMenu("Scripts/Game/Events/Ban")]
internal class Ban : DatabaseEvent
{
	// Token: 0x06001045 RID: 4165 RVA: 0x000B8684 File Offset: 0x000B6884
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		int num2 = (int)Crypt.ResolveVariable(args, 0, 1);
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 2);
		if (num2 < 0)
		{
			num2 = -1;
		}
		string text2 = text.Trim();
		Dictionary<string, object> dict = new Dictionary<string, object>();
		JSON.ReadWrite(dict, "reason", ref text2, true);
		string data = ArrayUtility.ToJSON<string, object>(dict);
		HtmlLayer.SendCompressed(string.Concat(new object[]
		{
			"?action=ban&user_id=",
			num,
			"&hours=",
			num2
		}), data, new RequestFinished(this.OnResponse), null);
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x000B873C File Offset: 0x000B693C
	protected override void OnResponse(string text)
	{
		global::Console.print(text);
	}
}
