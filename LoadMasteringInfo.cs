using System;

// Token: 0x02000343 RID: 835
internal class LoadMasteringInfo : DatabaseEvent
{
	// Token: 0x06001C00 RID: 7168 RVA: 0x000FA6C0 File Offset: 0x000F88C0
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=customization/player/load", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001C01 RID: 7169 RVA: 0x000FA6FC File Offset: 0x000F88FC
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Main.UserInfo.Mastering.Initialize(text);
	}
}
