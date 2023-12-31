using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000202 RID: 514
internal class DeleteAllRequest : DatabaseEvent
{
	// Token: 0x06001098 RID: 4248 RVA: 0x000BAD0C File Offset: 0x000B8F0C
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.DeleteAllRequest, Language.DeleteAllRequestProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Delete all request", Color.grey);
		HtmlLayer.Request("clans.php?action=delete_all_requests&clan_id=" + str, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001099 RID: 4249 RVA: 0x000BAD98 File Offset: 0x000B8F98
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.DeleteAllRequest, Language.DeleteAllRequestComplete, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
	}

	// Token: 0x0600109A RID: 4250 RVA: 0x000BAE74 File Offset: 0x000B9074
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
