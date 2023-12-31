using System;
using UnityEngine;

// Token: 0x0200022B RID: 555
internal class RepairAllWeapon : DatabaseEvent
{
	// Token: 0x06001156 RID: 4438 RVA: 0x000C17CC File Offset: 0x000BF9CC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=repair_all", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001157 RID: 4439 RVA: 0x000C1808 File Offset: 0x000BFA08
	protected override void OnResponse(string text)
	{
		Debug.Log(Helpers.ColoredLog(text, "#00FF00"));
	}

	// Token: 0x06001158 RID: 4440 RVA: 0x000C181C File Offset: 0x000BFA1C
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
