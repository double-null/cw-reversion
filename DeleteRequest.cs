using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000201 RID: 513
internal class DeleteRequest : DatabaseEvent
{
	// Token: 0x06001094 RID: 4244 RVA: 0x000BAB34 File Offset: 0x000B8D34
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		string str2 = (string)Crypt.ResolveVariable(args, "0", 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.DeleteRequest, Language.DeleteRequestProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Delete request", Color.grey);
		HtmlLayer.Request("clans.php?action=delete_request&clan_id=" + str + "&user_id=" + str2, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001095 RID: 4245 RVA: 0x000BABD8 File Offset: 0x000B8DD8
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.DeleteRequest, Language.DeleteRequestComplete, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
		this.SuccessAction();
	}

	// Token: 0x06001096 RID: 4246 RVA: 0x000BACBC File Offset: 0x000B8EBC
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.DeleteRequest, Language.CWMainSkillUnlockErrorDesc, PopupState.progress, true, false, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}
}
