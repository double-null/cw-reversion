using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000257 RID: 599
[AddComponentMenu("Scripts/Game/Foundation/DailyBonus")]
internal class DailyBonus : Convertible
{
	// Token: 0x0600125E RID: 4702 RVA: 0x000C95B8 File Offset: 0x000C77B8
	public DailyBonus()
	{
	}

	// Token: 0x0600125F RID: 4703 RVA: 0x000C95C8 File Offset: 0x000C77C8
	public DailyBonus(Dictionary<string, object> dict)
	{
	}

	// Token: 0x17000284 RID: 644
	// (get) Token: 0x06001260 RID: 4704 RVA: 0x000C95D8 File Offset: 0x000C77D8
	public int WeaponID
	{
		get
		{
			return this.iIDWeapon;
		}
	}

	// Token: 0x06001261 RID: 4705 RVA: 0x000C95E0 File Offset: 0x000C77E0
	public bool BonusIsWeapon()
	{
		return this.iIDWeapon >= 0;
	}

	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06001262 RID: 4706 RVA: 0x000C95F0 File Offset: 0x000C77F0
	public bool IsPermanentWeapon
	{
		get
		{
			return this.isPermanent;
		}
	}

	// Token: 0x06001263 RID: 4707 RVA: 0x000C95F8 File Offset: 0x000C77F8
	public int GetValue()
	{
		int result = 0;
		if (this.CR > 0)
		{
			result = this.CR;
		}
		if (this.GP > 0)
		{
			result = this.GP;
		}
		if (this.SP > 0)
		{
			result = this.SP;
		}
		if (this.BG > 0)
		{
			result = this.BG;
		}
		return result;
	}

	// Token: 0x17000286 RID: 646
	// (get) Token: 0x06001264 RID: 4708 RVA: 0x000C9654 File Offset: 0x000C7854
	public int RentTime
	{
		get
		{
			if (this.iRentTime > 0)
			{
				return this.iRentTime;
			}
			return 0;
		}
	}

	// Token: 0x06001265 RID: 4709 RVA: 0x000C966C File Offset: 0x000C786C
	public string GetValueName()
	{
		if (this.CR > 0)
		{
			return "CR";
		}
		if (this.GP > 0)
		{
			return "GP";
		}
		if (this.SP > 0)
		{
			return "SP";
		}
		if (this.BG > 0)
		{
			return "BG";
		}
		return string.Empty;
	}

	// Token: 0x06001266 RID: 4710 RVA: 0x000C96C8 File Offset: 0x000C78C8
	public void Clear()
	{
		this.CR = 0;
		this.GP = 0;
		this.SP = 0;
		this.BG = 0;
		this.iRentDays = 0;
		this.iIDWeapon = -1;
		this.iRentTime = 0;
	}

	// Token: 0x06001267 RID: 4711 RVA: 0x000C96FC File Offset: 0x000C78FC
	public bool IsWin()
	{
		return this.CR > 0 || this.GP > 0 || this.SP > 0 || this.WeaponID >= 0;
	}

	// Token: 0x06001268 RID: 4712 RVA: 0x000C9734 File Offset: 0x000C7934
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		this.CR = 0;
		this.GP = 0;
		this.SP = 0;
		this.BG = 0;
		this.iRentDays = 0;
		this.iIDWeapon = -1;
		this.iRentTime = -1;
		if (isWrite)
		{
			return;
		}
		if ((int)dict["result"] == 0)
		{
			if ((int)dict["weapon_id"] >= 0)
			{
				this.iIDWeapon = (int)dict["weapon_id"];
				if ((int)dict["rent_time"] > 0)
				{
					this.iRentTime = (int)dict["rent_time"];
					if (Main.UserInfo.weaponsStates.Length > this.WeaponID && this.iRentTime > 0 && Main.UserInfo.weaponsStates[this.WeaponID].CurrentWeapon.isPremium)
					{
						this.iRentDays = (((this.iRentTime - HtmlLayer.serverUtc) / 86400 <= 0) ? 1 : ((this.iRentTime - HtmlLayer.serverUtc) / 86400));
						Main.UserInfo.weaponsStates[this.WeaponID].Unlocked = true;
						Main.UserInfo.weaponsStates[this.WeaponID].rentEnd = this.iRentTime;
						Main.UserInfo.weaponsStates[this.WeaponID].UpdateWeapon();
					}
				}
				else if ((int)dict["rent_time"] == -1)
				{
					Main.UserInfo.weaponsStates[this.WeaponID].Unlocked = true;
					Main.UserInfo.weaponsStates[this.WeaponID].UpdateWeapon();
					this.isPermanent = true;
				}
			}
			else
			{
				if ((int)dict["new_cr"] >= 0 && (int)dict["win_cr"] > 0)
				{
					this.CR = (int)dict["win_cr"];
					Main.UserInfo.CR = (int)dict["new_cr"];
				}
				if ((int)dict["new_sp"] >= 0 && (int)dict["win_sp"] > 0)
				{
					this.SP = (int)dict["win_sp"];
					Main.UserInfo.SP = (int)dict["new_sp"];
				}
				if ((int)dict["new_gp"] >= 0 && (int)dict["win_gp"] > 0)
				{
					this.GP = (int)dict["win_gp"];
					Main.UserInfo.GP = (int)dict["new_gp"];
				}
			}
			if (this.IsWin())
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Podgon, Language.DailyBonus, string.Empty, PopupState.dailyBonus, false, true, string.Empty, string.Empty));
			}
		}
	}

	// Token: 0x04001202 RID: 4610
	public int CR;

	// Token: 0x04001203 RID: 4611
	public int GP;

	// Token: 0x04001204 RID: 4612
	public int SP;

	// Token: 0x04001205 RID: 4613
	public int BG;

	// Token: 0x04001206 RID: 4614
	public int iIDWeapon = -1;

	// Token: 0x04001207 RID: 4615
	public int iRentTime;

	// Token: 0x04001208 RID: 4616
	public int iRentDays;

	// Token: 0x04001209 RID: 4617
	private bool isPermanent;
}
