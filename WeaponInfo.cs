using System;
using System.Collections.Generic;

// Token: 0x020002DE RID: 734
[Serializable]
internal class WeaponInfo : Convertible
{
	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06001464 RID: 5220 RVA: 0x000D8334 File Offset: 0x000D6534
	// (set) Token: 0x06001465 RID: 5221 RVA: 0x000D8350 File Offset: 0x000D6550
	public bool Unlocked
	{
		get
		{
			return this.unlocked || this.CurrentWeapon.unlockBySkill;
		}
		set
		{
			this.unlocked = value;
		}
	}

	// Token: 0x17000312 RID: 786
	// (get) Token: 0x06001466 RID: 5222 RVA: 0x000D835C File Offset: 0x000D655C
	public bool isUndestructable
	{
		get
		{
			return this.currentWeapon.isPremium || this.currentWeapon.isSocial || this.currentWeapon.isDonate || this.currentWeapon.type == Weapons.pm || this.repair_info == -77f;
		}
	}

	// Token: 0x17000313 RID: 787
	// (get) Token: 0x06001467 RID: 5223 RVA: 0x000D83BC File Offset: 0x000D65BC
	public bool NeedRepair
	{
		get
		{
			return this.repair_info > 0f;
		}
	}

	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06001468 RID: 5224 RVA: 0x000D83CC File Offset: 0x000D65CC
	public bool wtaskUnlocked
	{
		get
		{
			return this.wtaskCurrent >= (float)this.CurrentWeapon.wtask.count || this.wtaskCurrent == -77f;
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06001469 RID: 5225 RVA: 0x000D8408 File Offset: 0x000D6608
	public float wtaskProgress
	{
		get
		{
			if (this.wtaskCurrent == -77f)
			{
				return 1f;
			}
			return this.wtaskCurrent / (float)this.CurrentWeapon.wtask.count;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x0600146A RID: 5226 RVA: 0x000D8444 File Offset: 0x000D6644
	public int wtaskProgress100
	{
		get
		{
			if (this.wtaskCurrent == -77f)
			{
				return 100;
			}
			if (Utility.IsModableWeapon(this.CurrentWeapon.type) && Main.UserInfo.Mastering.WeaponsStats.ContainsKey((int)this.CurrentWeapon.type) && Main.UserInfo.Mastering.WeaponsStats[(int)this.CurrentWeapon.type].WtaskMetaUnlocked)
			{
				return 100;
			}
			return (int)(this.wtaskCurrent / (float)this.CurrentWeapon.wtask.count * 100f);
		}
	}

	// Token: 0x0600146B RID: 5227 RVA: 0x000D84E8 File Offset: 0x000D66E8
	public void Init(bool initWithMain, int index)
	{
		this.cachedIndex = index;
		if (initWithMain)
		{
			this.currentWeapon = Main.UserInfo.weaponsStates[index].currentWeapon;
		}
	}

	// Token: 0x0600146C RID: 5228 RVA: 0x000D851C File Offset: 0x000D671C
	public void Convert(Dictionary<string, object> dict, bool set)
	{
		JSON.ReadWrite(dict, "unlocked", ref this.unlocked, set);
		JSON.ReadWrite(dict, "wtaskCurrent", ref this.wtaskCurrent, set);
		JSON.ReadWrite(dict, "repair_info", ref this.repair_info, set);
		JSON.ReadWrite(dict, "rentEnd", ref this.rentEnd, set);
		if (!set)
		{
			JSON.ReadWrite(dict, "market_amount", ref this.market_amount, set);
		}
	}

	// Token: 0x17000317 RID: 791
	// (get) Token: 0x0600146D RID: 5229 RVA: 0x000D858C File Offset: 0x000D678C
	public BaseWeapon GetWeapon
	{
		get
		{
			return this.currentWeapon;
		}
	}

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x0600146E RID: 5230 RVA: 0x000D8594 File Offset: 0x000D6794
	public BaseWeapon CurrentWeapon
	{
		get
		{
			if (this != Main.UserInfo.weaponsStates[this.cachedIndex])
			{
				this.currentWeapon = Main.UserInfo.weaponsStates[this.cachedIndex].CurrentWeapon;
			}
			if (this.currentWeapon == null || Main.CurrentSuit.GetWtask(this.cachedIndex) != this.currentWeapon.IsMod)
			{
				this.UpdateWeapon();
			}
			return this.currentWeapon;
		}
	}

	// Token: 0x0600146F RID: 5231 RVA: 0x000D8614 File Offset: 0x000D6814
	public void UpdateWeapon()
	{
		if (this.currentWeapon)
		{
			PoolManager.Despawn(this.currentWeapon.gameObject);
			this.currentWeapon = null;
		}
		this.currentWeapon = SingletoneForm<PoolManager>.Instance["gui_weapon"].Spawn().GetComponent<BaseWeapon>();
		this.currentWeapon.state.isMod = Main.CurrentSuit.GetWtask(this.cachedIndex);
		this.currentWeapon.LoadTable(Globals.I.weapons[this.cachedIndex]);
	}

	// Token: 0x0400191F RID: 6431
	private bool unlocked;

	// Token: 0x04001920 RID: 6432
	public float wtaskCurrent;

	// Token: 0x04001921 RID: 6433
	public int rentEnd = -1;

	// Token: 0x04001922 RID: 6434
	public float repair_info;

	// Token: 0x04001923 RID: 6435
	public int market_amount;

	// Token: 0x04001924 RID: 6436
	public ButtonState buttonState = default(ButtonState);

	// Token: 0x04001925 RID: 6437
	public int cachedIndex;

	// Token: 0x04001926 RID: 6438
	private BaseWeapon currentWeapon;
}
