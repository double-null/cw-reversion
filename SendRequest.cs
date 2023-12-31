using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FE RID: 510
internal class SendRequest : DatabaseEvent
{
	// Token: 0x06001088 RID: 4232 RVA: 0x000BA584 File Offset: 0x000B8784
	public override void Initialize(params object[] args)
	{
		int num = (int)args[0];
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.SendRequest, Language.SendRequestProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Send request", Color.grey);
		HtmlLayer.Request("clans.php?action=send_request&clan_id=" + num, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001089 RID: 4233 RVA: 0x000BA60C File Offset: 0x000B880C
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
		if (dictionary.ContainsKey("error_code") && (int)dictionary["error_code"] == 102)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClansPopupError, "byVacancyFailed", PopupState.clanError, false, true, string.Empty, string.Empty));
			this.FailedAction();
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.FailedAction();
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.SendRequest, Language.SendRequestComplete, PopupState.information, false, true, string.Empty, string.Empty));
		global::Console.print("Request send success", Color.green);
		this.SuccessAction();
	}

	// Token: 0x0600108A RID: 4234 RVA: 0x000BA758 File Offset: 0x000B8958
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClansPopupError, e.ToString(), PopupState.clanError, false, true, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
