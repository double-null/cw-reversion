using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000227 RID: 551
[AddComponentMenu("Scripts/Game/Events/PassFriendList")]
internal class PassFriendList : DatabaseEvent
{
	// Token: 0x0600114A RID: 4426 RVA: 0x000C1334 File Offset: 0x000BF534
	public override void Initialize(params object[] args)
	{
		PassFriendList.CallCount++;
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		Dictionary<string, object> dict = new Dictionary<string, object>();
		JSON.ReadWrite(dict, "uids", ref text, true);
		string data = ArrayUtility.ToJSON<string, object>(dict);
		HtmlLayer.SendCompressed("?action=recordfriends&user_id=" + Main.UserInfo.userID.ToString() + "&firsttime=" + ((!LoadProfile.firstRun) ? "0" : "1"), data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
	}

	// Token: 0x0600114B RID: 4427 RVA: 0x000C13D4 File Offset: 0x000BF5D4
	protected override void OnResponse(string text, string url)
	{
		this.SuccessAction();
	}

	// Token: 0x0600114C RID: 4428 RVA: 0x000C13E4 File Offset: 0x000BF5E4
	protected override void OnFail(Exception e, string url)
	{
		this.FailedAction();
	}

	// Token: 0x040010FC RID: 4348
	public static int CallCount;
}
