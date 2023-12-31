using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F3 RID: 499
[AddComponentMenu("Scripts/Game/Events/BuySP")]
internal class BuySP : DatabaseEvent
{
	// Token: 0x0600105E RID: 4190 RVA: 0x000B90E0 File Offset: 0x000B72E0
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuySP", Color.grey);
		HtmlLayer.Request("?action=buysp", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600105F RID: 4191 RVA: 0x000B9154 File Offset: 0x000B7354
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
			EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
			global::Console.print("Buy SP Finished", Color.green);
			if (dictionary.ContainsKey("new_gp") && dictionary.ContainsKey("new_sp") && dictionary.ContainsKey("new_sp_available"))
			{
				Main.UserInfo.GP = (int)dictionary["new_gp"];
				Main.UserInfo.SP = (int)dictionary["new_sp"];
				Main.UserInfo.sp_available = (int)dictionary["new_sp_available"];
			}
		}
	}

	// Token: 0x06001060 RID: 4192 RVA: 0x000B92D0 File Offset: 0x000B74D0
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
