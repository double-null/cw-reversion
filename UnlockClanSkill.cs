using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020B RID: 523
internal class UnlockClanSkill : DatabaseEvent
{
	// Token: 0x060010BC RID: 4284 RVA: 0x000BBBC8 File Offset: 0x000B9DC8
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, "0", 0);
		int num2 = (int)Crypt.ResolveVariable(args, "0", 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		if (num2 == -1)
		{
			HtmlLayer.Request("clans.php?action=buySkill&skill_id=" + num.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		else
		{
			HtmlLayer.Request(string.Concat(new object[]
			{
				"clans.php?action=rentSkill&skill_id=",
				num,
				"&rent_option=",
				num2.ToString()
			}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		this.skillID = num;
	}

	// Token: 0x060010BD RID: 4285 RVA: 0x000BBCC8 File Offset: 0x000B9EC8
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Clan skill unlocked", Color.green);
		if (dictionary.ContainsKey("rentEnd") && (int)dictionary["rentEnd"] > 0)
		{
			Main.UserInfo.ClanSkillsInfos[this.skillID].RentEnd = (int)dictionary["rentEnd"];
		}
		Main.UserInfo.ClanSkillsInfos[this.skillID].IsUnlocked = true;
		for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
		{
			Main.UserInfo.weaponsStates[i].UpdateWeapon();
		}
		this.SuccessAction();
	}

	// Token: 0x060010BE RID: 4286 RVA: 0x000BBE40 File Offset: 0x000BA040
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}

	// Token: 0x040010DA RID: 4314
	private int skillID;
}
