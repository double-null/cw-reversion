using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000230 RID: 560
[AddComponentMenu("Scripts/Game/Events/SBan")]
internal class SBan : Ban
{
	// Token: 0x0600116C RID: 4460 RVA: 0x000C241C File Offset: 0x000C061C
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		int num2 = (int)Crypt.ResolveVariable(args, 0, 1);
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 2);
		Dictionary<string, object> dict = new Dictionary<string, object>();
		string text2 = text.Trim();
		JSON.ReadWrite(dict, "reason", ref text2, true);
		string data = ArrayUtility.ToJSON<string, object>(dict);
		HtmlLayer.SendCompressed("?action=ban&social_id=" + num.ToString() + "&hours=" + num2.ToString(), data, null, null);
	}
}
