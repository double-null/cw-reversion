using System;
using UnityEngine;

// Token: 0x02000212 RID: 530
[AddComponentMenu("Scripts/Game/Events/ClearProfile")]
internal class ClearProfile : DatabaseEvent
{
	// Token: 0x060010D6 RID: 4310 RVA: 0x000BCBCC File Offset: 0x000BADCC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=clearProfile&user_id=" + ((int)Crypt.ResolveVariable(args, 0, 0)).ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010D7 RID: 4311 RVA: 0x000BCC28 File Offset: 0x000BAE28
	protected override void OnResponse(string text)
	{
		global::Console.print(text);
	}
}
