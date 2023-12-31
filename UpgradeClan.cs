using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000206 RID: 518
internal class UpgradeClan : DatabaseEvent
{
	// Token: 0x060010A8 RID: 4264 RVA: 0x000BB3FC File Offset: 0x000B95FC
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ExtendClan, Language.ExtendClanProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Upgrade clan", Color.grey);
		HtmlLayer.Request("clans.php?action=upgrade_clan&clan_id=" + str, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010A9 RID: 4265 RVA: 0x000BB488 File Offset: 0x000B9688
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ExtendClan, Language.ExtendClanComplete, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
	}

	// Token: 0x060010AA RID: 4266 RVA: 0x000BB564 File Offset: 0x000B9764
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
