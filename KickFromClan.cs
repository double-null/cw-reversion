using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000204 RID: 516
internal class KickFromClan : DatabaseEvent
{
	// Token: 0x060010A0 RID: 4256 RVA: 0x000BB078 File Offset: 0x000B9278
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		string str2 = (string)Crypt.ResolveVariable(args, "0", 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.KickFromClan, Language.KickFromClanProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Kick from clan", Color.grey);
		HtmlLayer.Request("clans.php?action=kick_from_clan&clan_id=" + str + "&user_id=" + str2, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010A1 RID: 4257 RVA: 0x000BB11C File Offset: 0x000B931C
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.KickFromClan, Language.KickFromClanComplete, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Kick from clan Finished", Color.green);
		this.SuccessAction();
	}

	// Token: 0x060010A2 RID: 4258 RVA: 0x000BB200 File Offset: 0x000B9400
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.progress, true, false, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}
}
