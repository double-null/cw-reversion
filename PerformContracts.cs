using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000228 RID: 552
internal class PerformContracts : DatabaseEvent
{
	// Token: 0x0600114E RID: 4430 RVA: 0x000C13FC File Offset: 0x000BF5FC
	public override void Initialize(params object[] args)
	{
		global::Console.print("Perform contracts. Request sending...", Color.grey);
		HtmlLayer.Request("?action=performContracts&uid=" + Main.UserInfo.userID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600114F RID: 4431 RVA: 0x000C1458 File Offset: 0x000BF658
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			this.OnFail(ex);
			return;
		}
		object value;
		dictionary.TryGetValue("result", out value);
		if (Convert.ToInt32(value) == 0)
		{
			this.SuccessAction();
		}
		else
		{
			object arg;
			dictionary.TryGetValue("message", out arg);
			this.OnFail(new Exception("Returns bad result in PerformContracts: " + arg));
		}
	}

	// Token: 0x06001150 RID: 4432 RVA: 0x000C1500 File Offset: 0x000BF700
	protected override void OnFail(Exception e)
	{
		global::Console.print(e.ToString(), Color.red);
		this.FailedAction();
		base.OnFail(e);
	}
}
