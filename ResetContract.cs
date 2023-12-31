using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022D RID: 557
[AddComponentMenu("Scripts/Game/Events/ResetContract")]
internal class ResetContract : DatabaseEvent
{
	// Token: 0x0600115E RID: 4446 RVA: 0x000C1A54 File Offset: 0x000BFC54
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=initContracts", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x000C1A90 File Offset: 0x000BFC90
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error:" + dictionary["error"]));
		}
		else
		{
			Main.UserInfo.contractsInfo.DeltaTime = (int)dictionary["timer_end"];
			Main.UserInfo.contractsInfo.CurrentEasyIndex = (int)dictionary["current_easy"];
			Main.UserInfo.contractsInfo.CurrentNormalIndex = (int)dictionary["current_normal"];
			Main.UserInfo.contractsInfo.CurrentHardIndex = (int)dictionary["current_hard"];
			Main.UserInfo.contractsInfo.CurrentEasyCount = (int)dictionary["easy_counter"];
			Main.UserInfo.contractsInfo.CurrentNormalCount = (int)dictionary["normal_counter"];
			Main.UserInfo.contractsInfo.CurrentHardCount = (int)dictionary["hard_counter"];
		}
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x000C1C20 File Offset: 0x000BFE20
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
