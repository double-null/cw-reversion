using System;

// Token: 0x02000348 RID: 840
internal class MasteringLoadSuits : DatabaseEvent
{
	// Token: 0x06001C0E RID: 7182 RVA: 0x000FAB40 File Offset: 0x000F8D40
	public override void Initialize(params object[] args)
	{
		string actions = "adm.php?q=customization/player/load_suits";
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x000FAB80 File Offset: 0x000F8D80
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		MasteringSuitsInfo.Instance.Initialize(text);
		this.SuccessAction();
	}
}
