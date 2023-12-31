using System;

// Token: 0x02000349 RID: 841
internal class GetRarityColors : DatabaseEvent
{
	// Token: 0x06001C11 RID: 7185 RVA: 0x000FABC4 File Offset: 0x000F8DC4
	public override void Initialize(params object[] args)
	{
		string actions = "adm.php?q=customization/client/rarity_colors";
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001C12 RID: 7186 RVA: 0x000FAC04 File Offset: 0x000F8E04
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		RarityColors.Initialize(text);
	}
}
