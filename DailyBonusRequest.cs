using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000215 RID: 533
[AddComponentMenu("Scripts/Game/Events/DailyBonusRequest")]
internal class DailyBonusRequest : DatabaseEvent
{
	// Token: 0x060010E0 RID: 4320 RVA: 0x000BCE30 File Offset: 0x000BB030
	public override void Initialize(params object[] args)
	{
		Main.UserInfo.dailyBonus.Clear();
		global::Console.print("GetDailyBonus", Color.grey);
		HtmlLayer.Request("?action=GetDailyBonus", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010E1 RID: 4321 RVA: 0x000BCE8C File Offset: 0x000BB08C
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
			global::Console.print("GetDailyBonus Finished", Color.green);
			Main.UserInfo.dailyBonus.CR = 0;
			Main.UserInfo.dailyBonus.GP = 0;
			Main.UserInfo.dailyBonus.SP = 0;
			Main.UserInfo.dailyBonus.BG = 0;
			Main.UserInfo.dailyBonus.iIDWeapon = -1;
			Main.UserInfo.dailyBonus.iRentTime = -1;
			if ((int)dictionary["weapon_id"] >= 0)
			{
				Main.UserInfo.dailyBonus.iIDWeapon = (int)dictionary["weapon_id"];
				if ((int)dictionary["rent_time"] > 0)
				{
					Main.UserInfo.dailyBonus.iRentTime = (int)dictionary["rent_time"];
				}
				if (Main.UserInfo.weaponsStates.Length > Main.UserInfo.dailyBonus.WeaponID && Main.UserInfo.dailyBonus.iRentTime > 0 && Main.UserInfo.weaponsStates[Main.UserInfo.dailyBonus.WeaponID].CurrentWeapon.isPremium)
				{
					Main.UserInfo.weaponsStates[Main.UserInfo.dailyBonus.WeaponID].Unlocked = true;
					Main.UserInfo.weaponsStates[Main.UserInfo.dailyBonus.WeaponID].rentEnd = Main.UserInfo.dailyBonus.iRentTime;
					Main.UserInfo.weaponsStates[Main.UserInfo.dailyBonus.WeaponID].UpdateWeapon();
				}
			}
			else
			{
				if ((int)dictionary["new_cr"] > 0)
				{
					Main.UserInfo.dailyBonus.CR = (int)dictionary["new_cr"] - Main.UserInfo.CR;
					Main.UserInfo.CR = (int)dictionary["new_cr"];
				}
				if ((int)dictionary["new_sp"] > 0)
				{
					Main.UserInfo.dailyBonus.SP = (int)dictionary["new_sp"] - Main.UserInfo.SP;
					Main.UserInfo.SP = (int)dictionary["new_sp"];
				}
				if ((int)dictionary["new_gp"] > 0)
				{
					Main.UserInfo.dailyBonus.GP = (int)dictionary["new_gp"] - Main.UserInfo.GP;
					Main.UserInfo.GP = (int)dictionary["new_gp"];
				}
				if ((int)dictionary["new_bg"] > 0)
				{
					Main.UserInfo.dailyBonus.BG = (int)dictionary["new_bg"] - Main.UserInfo.BG;
					Main.UserInfo.BG = (int)dictionary["new_bg"];
				}
			}
		}
	}

	// Token: 0x060010E2 RID: 4322 RVA: 0x000BD2A4 File Offset: 0x000BB4A4
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
