using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023B RID: 571
[AddComponentMenu("Scripts/Game/Events/UnlockWeapon")]
internal class UnlockWeapon : DatabaseEvent
{
	// Token: 0x06001196 RID: 4502 RVA: 0x000C3854 File Offset: 0x000C1A54
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		int num2 = (int)Crypt.ResolveVariable(args, -1, 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Unlock, Language.CWMainWeaponUnlock, Language.CWMainWeaponUnlockDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("UnlockWeapon", Color.grey);
		this.weaponIndexCached = num;
		if (num2 == -1)
		{
			HtmlLayer.Request("?action=weaponUnlock&weapon_index=" + num.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
		else
		{
			HtmlLayer.Request("?action=premiumWeaponUnlock&weapon_index=" + num.ToString() + "&rentOption=" + num2.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		}
	}

	// Token: 0x06001197 RID: 4503 RVA: 0x000C3954 File Offset: 0x000C1B54
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Unlock, Language.CWMainWeaponUnlock, Language.CWMainWeaponUnlockFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Weapon Buy Finished", Color.green);
		if (dictionary.ContainsKey("premiumBuy") && (bool)dictionary["premiumBuy"])
		{
			Main.UserInfo.weaponsStates[this.weaponIndexCached].Unlocked = true;
			Main.UserInfo.weaponsStates[this.weaponIndexCached].rentEnd = (int)dictionary["rentEnd"];
			Main.UserInfo.GP = (int)dictionary["newGP"];
		}
		else
		{
			Main.UserInfo.weaponsStates[this.weaponIndexCached].Unlocked = true;
			int num = Main.UserInfo.weaponsStates[this.weaponIndexCached].CurrentWeapon.price;
			if (Main.UserInfo.skillUnlocked(Skills.car_weap))
			{
				num = Mathf.RoundToInt((float)num * 0.8f);
			}
			Main.UserInfo.CR -= num;
		}
	}

	// Token: 0x06001198 RID: 4504 RVA: 0x000C3B20 File Offset: 0x000C1D20
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Unlock, Language.CWMainWeaponUnlockError, Language.CWMainWeaponUnlockErrorDesc, PopupState.information, true, true, string.Empty, string.Empty));
		base.OnFail(e);
	}

	// Token: 0x0400112A RID: 4394
	private int weaponIndexCached;
}
