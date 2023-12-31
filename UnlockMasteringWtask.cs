using System;

// Token: 0x02000340 RID: 832
internal class UnlockMasteringWtask : DatabaseEvent
{
	// Token: 0x06001BF7 RID: 7159 RVA: 0x000FA33C File Offset: 0x000F853C
	public override void Initialize(params object[] args)
	{
		object obj = Crypt.ResolveVariable(args, string.Empty, 0);
		int num = (int)Crypt.ResolveVariable(args, -1, 1);
		string actions = string.Concat(new object[]
		{
			"adm.php?q=customization/player/wtask/server_unlock/",
			obj,
			"/",
			num
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BF8 RID: 7160 RVA: 0x000FA3BC File Offset: 0x000F85BC
	protected override void OnResponse(string text)
	{
	}
}
