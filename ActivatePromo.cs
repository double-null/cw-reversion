using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E8 RID: 488
[AddComponentMenu("Scripts/Game/Events/ActivatePromo")]
internal class ActivatePromo : DatabaseEvent
{
	// Token: 0x06001039 RID: 4153 RVA: 0x000B7B38 File Offset: 0x000B5D38
	public override void Initialize(params object[] args)
	{
		string str = (string)args[0];
		HtmlLayer.Request("?action=promo&code=" + str, new RequestFinished(this.OnResponse), null, string.Empty, string.Empty);
	}

	// Token: 0x0600103A RID: 4154 RVA: 0x000B7B78 File Offset: 0x000B5D78
	protected override void OnResponse(string text)
	{
		base.OnResponse(text);
		MonoBehaviour.print(text);
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
			this.OnFail(new Exception("DataServer LoginError "));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			string desc = string.Empty;
			switch ((int)dictionary["result"])
			{
			case 990:
				desc = Language.PromoCodeAlreadyActivated;
				goto IL_EA;
			case 991:
				desc = Language.PromoObsolete;
				goto IL_EA;
			case 992:
				desc = Language.PromoCodeAlreadyActivatedThisMember;
				goto IL_EA;
			case 994:
				desc = Language.PromoUnknownCode;
				goto IL_EA;
			}
			desc = Language.PromoUnknownError;
			IL_EA:
			global::Console.HideMe();
			EventFactory.Call("ShowPopup", new Popup(WindowsID.PopupGUI, Language.PromoErrorActivation, desc, PopupState.information, true, true, string.Empty, string.Empty));
			return;
		}
		Globals.I.bonuses.Clear();
		if ((int)dictionary["cr_prize"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.cr, (int)dictionary["cr_prize"], -1, -1, 0));
			Main.UserInfo.CR += (int)dictionary["cr_prize"];
		}
		if ((int)dictionary["gp_prize"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.gp, (int)dictionary["gp_prize"], -1, -1, 0));
			Main.UserInfo.GP += (int)dictionary["gp_prize"];
		}
		if ((int)dictionary["sp_prize"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.sp, (int)dictionary["sp_prize"], -1, -1, 0));
			Main.UserInfo.SP += (int)dictionary["sp_prize"];
		}
		if ((int)dictionary["bg_prize"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.bg, (int)dictionary["bg_prize"], -1, -1, 0));
			Main.UserInfo.BG += (int)dictionary["bg_prize"];
		}
		if ((int)dictionary["permanent_weapon_id"] != -1)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.weapon_buy, 0, (int)dictionary["permanent_weapon_id"], -1, 0));
			Main.UserInfo.weaponsStates[(int)dictionary["permanent_weapon_id"]].Unlocked = true;
		}
		if ((int)dictionary["rent_weapon_id"] != -1 && (int)dictionary["rent_weapon_days"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.weapon_rent, 0, (int)dictionary["rent_weapon_id"], -1, (int)dictionary["rent_weapon_days"]));
			Main.UserInfo.weaponsStates[(int)dictionary["rent_weapon_id"]].Unlocked = true;
			Main.UserInfo.weaponsStates[(int)dictionary["rent_weapon_id"]].rentEnd = HtmlLayer.serverUtc + (int)dictionary["rent_weapon_days"] * 60 * 60 * 24;
		}
		if ((int)dictionary["rent_skill"] > 0 && (int)dictionary["rent_skill_days"] > 0)
		{
			Globals.I.bonuses.Add(new PromoBonus(PromoBonusType.skill_rent, 0, -1, (int)dictionary["rent_skill"], (int)dictionary["rent_skill_days"]));
			Main.UserInfo.skillsInfos[(int)dictionary["rent_skill"]].Unlocked = true;
			Main.UserInfo.skillsInfos[(int)dictionary["rent_skill"]].rentEnd = HtmlLayer.serverUtc + (int)dictionary["rent_skill_days"] * 60 * 60 * 24;
		}
		global::Console.print("Total promo options:" + Globals.I.bonuses.Count);
		BannerGUI bannerGUI = Forms.Get(typeof(BannerGUI)) as BannerGUI;
		Audio.Play(bannerGUI.achiv_clip);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.ActivateBonus, Language.PromoActivated, string.Empty, PopupState.ActivatePromo, false, true, string.Empty, string.Empty));
		global::Console.HideMe();
	}
}
