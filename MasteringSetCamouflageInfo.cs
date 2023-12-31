using System;

// Token: 0x02000345 RID: 837
internal class MasteringSetCamouflageInfo : DatabaseEvent
{
	// Token: 0x06001C06 RID: 7174 RVA: 0x000FA838 File Offset: 0x000F8A38
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		this._meta = (int)Crypt.ResolveVariable(args, -1, 1);
		this._index = (int)Crypt.ResolveVariable(args, -1, 2);
		string actions = string.Concat(new object[]
		{
			"adm.php?q=customization/player/set_camo_info/",
			this._weaponId,
			"/",
			this._meta,
			"/",
			this._index
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x040020C0 RID: 8384
	private int _weaponId;

	// Token: 0x040020C1 RID: 8385
	private int _meta;

	// Token: 0x040020C2 RID: 8386
	private int _index;
}
