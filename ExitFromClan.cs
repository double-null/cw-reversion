using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000203 RID: 515
internal class ExitFromClan : DatabaseEvent
{
	// Token: 0x0600109C RID: 4252 RVA: 0x000BAEB8 File Offset: 0x000B90B8
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ExitClan, Language.ExitClanProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Exit from clan", Color.grey);
		HtmlLayer.Request("clans.php?action=exit_from_clan&clan_id=" + str, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600109D RID: 4253 RVA: 0x000BAF44 File Offset: 0x000B9144
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ExitClan, Language.ExitClanComplete, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Exit from clan Finished", Color.green);
		Main.UserInfo.clanID = 0;
		this.SuccessAction();
	}

	// Token: 0x0600109E RID: 4254 RVA: 0x000BB034 File Offset: 0x000B9234
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
