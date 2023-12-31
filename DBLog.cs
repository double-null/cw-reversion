using System;
using UnityEngine;

// Token: 0x02000214 RID: 532
[AddComponentMenu("Scripts/Game/Events/DBLog")]
internal class DBLog : DatabaseEvent
{
	// Token: 0x060010DD RID: 4317 RVA: 0x000BCD84 File Offset: 0x000BAF84
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 1);
		HtmlLayer.ExceptionRequest("error_log.php?action=" + str + "&user_id=" + ((int)Crypt.ResolveVariable(args, IDUtil.NoID, 2)).ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), "error_msg=" + text, "error_msg=" + WWW.EscapeURL(text));
	}

	// Token: 0x060010DE RID: 4318 RVA: 0x000BCE20 File Offset: 0x000BB020
	protected override void OnResponse(string text)
	{
		global::Console.print(text);
	}
}
