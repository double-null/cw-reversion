using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000200 RID: 512
internal class AcceptRequest : DatabaseEvent
{
	// Token: 0x06001090 RID: 4240 RVA: 0x000BA95C File Offset: 0x000B8B5C
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		string str2 = (string)Crypt.ResolveVariable(args, "0", 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.AcceptRequest, Language.AcceptRequestProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Accept request", Color.grey);
		HtmlLayer.Request("clans.php?action=accept_request&clan_id=" + str + "&user_id=" + str2, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001091 RID: 4241 RVA: 0x000BAA00 File Offset: 0x000B8C00
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
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.AcceptRequest, Language.AcceptRequestComplete, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
		this.SuccessAction();
	}

	// Token: 0x06001092 RID: 4242 RVA: 0x000BAAE4 File Offset: 0x000B8CE4
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.AcceptRequest, Language.CWMainSkillUnlockErrorDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}
}
