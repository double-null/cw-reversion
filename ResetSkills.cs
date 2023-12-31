using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022E RID: 558
[AddComponentMenu("Scripts/Game/Events/ResetSkills")]
internal class ResetSkills : DatabaseEvent
{
	// Token: 0x06001162 RID: 4450 RVA: 0x000C1C34 File Offset: 0x000BFE34
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 1, 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("ResetSkills", Color.grey);
		if (num == 1)
		{
			HtmlLayer.Request("?action=reset_skills", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		else
		{
			HtmlLayer.Request("?action=reset_skillscr", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
	}

	// Token: 0x06001163 RID: 4451 RVA: 0x000C1CF4 File Offset: 0x000BFEF4
	protected override void OnResponse(string text)
	{
		if (CVars.n_httpDebug)
		{
			MonoBehaviour.print(text);
		}
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
			global::Console.print("Skill Buy Finished", Color.green);
			if (dictionary.ContainsKey("user_id"))
			{
				if (dictionary.ContainsKey("new_sp"))
				{
					Main.UserInfo.SP = (int)dictionary["new_sp"];
				}
				if (dictionary.ContainsKey("new_gp"))
				{
					Main.UserInfo.GP = (int)dictionary["new_gp"];
				}
				if (dictionary.ContainsKey("new_cr"))
				{
					Main.UserInfo.CR = (int)dictionary["new_cr"];
				}
			}
			for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
			{
				Main.UserInfo.weaponsStates[i].UpdateWeapon();
			}
		}
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x000C1EBC File Offset: 0x000C00BC
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
