using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023A RID: 570
[AddComponentMenu("Scripts/Game/Events/UnlockSkill")]
internal class UnlockSkill : DatabaseEvent
{
	// Token: 0x06001192 RID: 4498 RVA: 0x000C33F8 File Offset: 0x000C15F8
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		int num2 = (int)Crypt.ResolveVariable(args, -1, 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("UnlockSkill", Color.grey);
		this.skillIndexCached = num;
		if (num2 == -1)
		{
			HtmlLayer.Request("?action=skillUnlock&skill_index=" + num.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		else
		{
			HtmlLayer.Request("?action=skillRent&skill_index=" + num.ToString() + "&rentOption=" + num2.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
	}

	// Token: 0x06001193 RID: 4499 RVA: 0x000C34F8 File Offset: 0x000C16F8
	protected override void OnResponse(string text, string url)
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
			this.OnFail(new Exception("Data Server Error\n", ex));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error:" + dictionary["error"] + " badResult"));
		}
		else
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlock, Language.CWMainSkillUnlockFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
			global::Console.print("Skill Buy Finished", Color.green);
			if (dictionary.ContainsKey("premiumBuy") && (bool)dictionary["premiumBuy"])
			{
				Main.UserInfo.skillsInfos[this.skillIndexCached].Unlocked = true;
				Main.UserInfo.skillsInfos[this.skillIndexCached].rentEnd = (int)dictionary["rentEnd"];
				Main.UserInfo.GP = (int)dictionary["new_gp"];
			}
			else
			{
				Main.UserInfo.skillsInfos[this.skillIndexCached].Unlocked = true;
				Main.UserInfo.CR = (int)dictionary["new_cr"];
				Main.UserInfo.SP = (int)dictionary["new_sp"];
				Main.UserInfo.GP = (int)dictionary["new_gp"];
				if (dictionary.ContainsKey("new_bg"))
				{
					Main.UserInfo.BG = (int)dictionary["new_bg"];
				}
			}
			if (this.skillIndexCached == 153)
			{
				HtmlLayer.Request("adm.php?q=ladder/skill/win/1", null, null, string.Empty, string.Empty);
			}
			if (this.skillIndexCached == 154)
			{
				HtmlLayer.Request("adm.php?q=ladder/skill/los/1", null, null, string.Empty, string.Empty);
			}
			for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
			{
				Main.UserInfo.weaponsStates[i].UpdateWeapon();
				if (Utility.IsModableWeapon((int)Main.UserInfo.weaponsStates[i].CurrentWeapon.type))
				{
					int type = (int)Main.UserInfo.weaponsStates[i].CurrentWeapon.type;
					Dictionary<ModType, int> mods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods[type].Mods;
					Main.UserInfo.weaponsStates[type].CurrentWeapon.ApplyModsEffect(mods);
				}
			}
		}
	}

	// Token: 0x06001194 RID: 4500 RVA: 0x000C3810 File Offset: 0x000C1A10
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}

	// Token: 0x04001129 RID: 4393
	private int skillIndexCached;
}
