using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
[Serializable]
internal class CWInput : ReusableClass<CWInput>
{
	// Token: 0x17000321 RID: 801
	// (get) Token: 0x0600148D RID: 5261 RVA: 0x000D8FA4 File Offset: 0x000D71A4
	public int Buttons
	{
		get
		{
			return this.buttons;
		}
	}

	// Token: 0x0600148E RID: 5262 RVA: 0x000D8FAC File Offset: 0x000D71AC
	public void Update()
	{
		this.buttons = 0;
		this.UpdateKey(Main.UserInfo.settings.binds.up, UKeyCode.up);
		this.UpdateKey(Main.UserInfo.settings.binds.down, UKeyCode.down);
		this.UpdateKey(Main.UserInfo.settings.binds.left, UKeyCode.left);
		this.UpdateKey(Main.UserInfo.settings.binds.right, UKeyCode.right);
		this.UpdateKey(Main.UserInfo.settings.binds.sit, UKeyCode.sit);
		this.UpdateKey(Main.UserInfo.settings.binds.walk, UKeyCode.walk);
		this.UpdateKey(Main.UserInfo.settings.binds.interaction, UKeyCode.interaction);
		this.UpdateKey(Main.UserInfo.settings.binds.knife, UKeyCode.knife);
		this.UpdateKey(Main.UserInfo.settings.binds.pistol, UKeyCode.SecondaryWeapon);
		this.UpdateKey(Main.UserInfo.settings.binds.weapon, UKeyCode.PrimaryWeapon);
		this.UpdateKey(Main.UserInfo.settings.binds.support, UKeyCode.sonar);
		this.UpdateKey(Main.UserInfo.settings.binds.mortar, UKeyCode.mortar);
		this.UpdateOncedKey(Main.UserInfo.settings.binds.flashlight, UKeyCode.flashlight);
		this.UpdateOncedKey(Main.UserInfo.settings.binds.teamChoose, UKeyCode.teamChoose);
		if (!Input.GetKey(Main.UserInfo.settings.binds.statistics) && (Input.GetAxis("Mouse ScrollWheel") > 0.01f || Input.GetAxis("Mouse ScrollWheel") < -0.01f || Input.GetKey(Main.UserInfo.settings.binds.toggle)))
		{
			this.updateButtons += 131072;
		}
		this.UpdateKey(Main.UserInfo.settings.binds.reload, UKeyCode.reload);
		this.UpdateKey(Main.UserInfo.settings.binds.burst, UKeyCode.auto);
		this.UpdateKey(Main.UserInfo.settings.binds.fire, UKeyCode.fire);
		this.UpdateKey(Main.UserInfo.settings.binds.aim, UKeyCode.aim);
		this.UpdateKey(Main.UserInfo.settings.binds.jump, UKeyCode.jump);
		this.UpdateKey(Main.UserInfo.settings.binds.grenade, UKeyCode.grenade);
		this.UpdateKey(Main.UserInfo.settings.binds.radio, UKeyCode.radio);
		this.UpdateKey(Main.UserInfo.settings.binds.clanSpecialAbility, UKeyCode.clanSpecialAbility);
		this.UpdateKey(Main.UserInfo.settings.binds.hideInterface, UKeyCode.hideInterface);
		this.lastbuttons = this.buttons;
		this.buttons = this.updateButtons;
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x000D9300 File Offset: 0x000D7500
	public void SetKey(UKeyCode ukey)
	{
		this.updateButtons |= (int)ukey;
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x000D9310 File Offset: 0x000D7510
	private void UpdateKey(KeyCode key, UKeyCode ukey)
	{
		if (Input.GetKey(key))
		{
			this.updateButtons |= (int)ukey;
		}
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x000D932C File Offset: 0x000D752C
	private void UpdateOncedKey(KeyCode key, UKeyCode ukey)
	{
		if (Input.GetKeyDown(key))
		{
			this.updateButtons |= (int)ukey;
		}
	}

	// Token: 0x06001492 RID: 5266 RVA: 0x000D9348 File Offset: 0x000D7548
	public void FixedUpdate()
	{
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x000D934C File Offset: 0x000D754C
	public int Save()
	{
		return this.buttons;
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x000D9354 File Offset: 0x000D7554
	public void Load(int buttons)
	{
		this.lastbuttons = this.buttons;
		this.buttons = buttons;
	}

	// Token: 0x06001495 RID: 5269 RVA: 0x000D936C File Offset: 0x000D756C
	public void SetKey(UKeyCode ukey, KeyState state)
	{
		if (state == KeyState.released)
		{
			if (BIT.AND(this.buttons, (int)ukey))
			{
				this.buttons -= (int)ukey;
			}
		}
		else if (!BIT.AND(this.buttons, (int)ukey))
		{
			this.buttons |= (int)ukey;
		}
	}

	// Token: 0x06001496 RID: 5270 RVA: 0x000D93C4 File Offset: 0x000D75C4
	public bool GetKeyDown(UKeyCode ukey, bool changeState = true)
	{
		return BIT.AND(this.buttons, (int)ukey) && !BIT.AND(this.lastbuttons, (int)ukey);
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x000D93EC File Offset: 0x000D75EC
	public bool GetKey(UKeyCode ukey, bool changeState = true)
	{
		return BIT.AND(this.buttons, (int)ukey);
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x000D93FC File Offset: 0x000D75FC
	public bool GetKeyUp(UKeyCode ukey, bool changeState = true)
	{
		return !BIT.AND(this.buttons, (int)ukey) && BIT.AND(this.lastbuttons, (int)ukey);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x000D942C File Offset: 0x000D762C
	public void Clone(CWInput obj)
	{
		this.lastbuttons = this.buttons;
		this.buttons = obj.Buttons;
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x000D9448 File Offset: 0x000D7648
	public void Clear()
	{
		this.buttons = 0;
		this.updateButtons = 0;
	}

	// Token: 0x0400194C RID: 6476
	private int buttons;

	// Token: 0x0400194D RID: 6477
	private int updateButtons;

	// Token: 0x0400194E RID: 6478
	private int lastbuttons;
}
