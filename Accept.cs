using System;

// Token: 0x02000333 RID: 819
internal class Accept : DatabaseEvent
{
	// Token: 0x06001B85 RID: 7045 RVA: 0x000F7E8C File Offset: 0x000F608C
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=ladder/accept", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}
}
