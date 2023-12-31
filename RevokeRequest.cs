using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FF RID: 511
internal class RevokeRequest : DatabaseEvent
{
	// Token: 0x0600108C RID: 4236 RVA: 0x000BA7A0 File Offset: 0x000B89A0
	public override void Initialize(params object[] args)
	{
		int num = (int)args[0];
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.RevokeRequest, Language.RevokeRequestProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Revoke request", Color.grey);
		HtmlLayer.Request("clans.php?action=revoke_request&clan_id=" + num, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600108D RID: 4237 RVA: 0x000BA828 File Offset: 0x000B8A28
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
			this.FailedAction();
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.RevokeRequest, Language.RevokeRequestComplete, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
		this.SuccessAction();
	}

	// Token: 0x0600108E RID: 4238 RVA: 0x000BA918 File Offset: 0x000B8B18
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
