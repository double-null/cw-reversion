using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000154 RID: 340
[AddComponentMenu("Scripts/GUI/HelpersGUI")]
internal class HelpersGUI : Form
{
	// Token: 0x06000864 RID: 2148 RVA: 0x0004AE00 File Offset: 0x00049000
	[Obfuscation(Exclude = true)]
	private void ShowBuyPopup(object obj)
	{
		if (Main.IsGameLoaded)
		{
			return;
		}
		this.Clear();
		this.alreadyClicked = false;
		object[] array = (object[])obj;
		BuyTypes buyTypes = (BuyTypes)((int)array[0]);
		object obj2 = array[1];
		this.repair = (bool)array[2];
		if (array.Length > 3)
		{
			this.isTutorial = (bool)array[3];
		}
		this.buyType = buyTypes;
		BuyTypes buyTypes2 = this.buyType;
		if (buyTypes2 == BuyTypes.PISTOL || buyTypes2 == BuyTypes.GUN)
		{
			this.buyWeapon = (obj2 as WeaponInfo);
		}
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0004AEA4 File Offset: 0x000490A4
	public override void MainInitialize()
	{
		this.isRendering = true;
		HelpersGUI.I = this;
		base.MainInitialize();
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0004AEBC File Offset: 0x000490BC
	public override void Clear()
	{
		this.makeUndestructible = false;
		this.selectIndex = 0;
		this.buyWeapon = null;
		this.repair = false;
		this.statedelta = 0;
		this.alreadyClicked = false;
		if (CVars.realm == "fb" || CVars.realm == "kg")
		{
			this.selectIndex = 4;
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0004AF24 File Offset: 0x00049124
	public override void Register()
	{
		EventFactory.Register("ShowBuyPopup", this);
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0004AF34 File Offset: 0x00049134
	public override void InterfaceGUI()
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width - this.background.width) * 0.5f, (float)(Screen.height - this.background.height) * 0.5f, (float)this.background.width, (float)this.background.height);
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow);
		this.gui.Picture(new Vector2(0f, 0f), this.background);
		string str = string.Empty;
		string str2 = Language.HGUnlock + " ";
		string text = Language.HGUnlockQuestion;
		bool flag = false;
		int num7;
		if (this.repair)
		{
			int num = this.buyWeapon.CurrentWeapon.durability - (int)this.buyWeapon.repair_info;
			int durability = this.buyWeapon.CurrentWeapon.durability;
			int num2 = num + this.statedelta;
			float num3 = (float)num2 / (float)durability;
			this.gui.Picture(new Vector2(95f, 88f), (num3 <= 0.2f || this.buyWeapon.repair_info >= (float)this.buyWeapon.CurrentWeapon.durability) ? this.repairs[2] : this.repairs[1]);
			str2 = Language.Repair + " - ";
			str = this.buyWeapon.CurrentWeapon.Name;
			text = Language.RepairQuestion;
			this.gui.Picture(new Vector2(90f, 56f), this.repairs[0]);
			this.gui.TextField(new Rect(25f, 81f, 100f, 20f), Language.HGState + ":", 16, "#ffffff", TextAnchor.MiddleLeft, false, false);
			float num4 = 1f;
			float num5;
			if (this.buyWeapon.CurrentWeapon.UnitRepairCost > 0f)
			{
				num5 = this.buyWeapon.CurrentWeapon.UnitRepairCost / CVars.g_repairCoef;
			}
			else
			{
				num5 = (float)(this.buyWeapon.CurrentWeapon.price / this.buyWeapon.CurrentWeapon.durability);
			}
			if (Main.UserInfo.skillUnlocked(Skills.rep1))
			{
				num4 = 0.9f;
			}
			if (Main.UserInfo.skillUnlocked(Skills.rep2))
			{
				num4 = 0.75f;
			}
			if (Main.UserInfo.skillUnlocked(Skills.rep3))
			{
				num4 = 0.5f;
			}
			if (this.buyWeapon.repair_info < (float)this.buyWeapon.CurrentWeapon.durability)
			{
				this.gui.TextField(new Rect(150f, 81f, 100f, 20f), num2 + "/" + durability, 16, "#ffffff", TextAnchor.MiddleLeft, false, false);
				this.gui.ProgressBar(new Vector2(97f, 90f), (float)(this.repairs[1].width - 4), num3, (num3 <= 0.2f) ? this.repairs[4] : this.repairs[3], 0f, false, true);
				this.statedelta = (int)(this.gui.FloatSlider0dot00(new Vector2(215f, 86f), (float)this.statedelta, 0f, (float)(durability - num), false) + 0.5f);
				float num6 = 188f / (float)(durability - num);
				this.gui.TextField(new Rect((float)(164 + (int)((float)this.statedelta * num6)), 67f, 100f, 20f), this.statedelta, 16, "#ffffff", TextAnchor.MiddleCenter, false, false);
				num7 = (int)Math.Round((double)(num5 * (float)this.statedelta * CVars.g_repairCoef * num4));
			}
			else
			{
				this.gui.TextField(new Rect(150f, 81f, 100f, 20f), "0/" + durability, 16, "#ffffff", TextAnchor.MiddleLeft, false, false);
				num7 = (int)Math.Round((double)(num5 * (float)this.buyWeapon.CurrentWeapon.durability * CVars.g_repairCoef * num4));
				this.gui.TextField(new Rect(220f, -9f, 200f, 200f), Language.HGWeaponBrokenNeedRepair, 16, "#ffffff", TextAnchor.MiddleCenter, false, false);
				this.statedelta = this.buyWeapon.CurrentWeapon.durability;
			}
			this.gui.TextField(new Rect(218f, 101f, 200f, 20f), Language.HGIndestructible, 16, "#ffffff", TextAnchor.MiddleCenter, false, false);
			Texture2D texture2D = (!this.makeUndestructible) ? this.checkbox[0] : this.checkbox[1];
			if (this.gui.Button(new Vector2(209f, 102f), texture2D, texture2D, texture2D, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.makeUndestructible = !this.makeUndestructible;
			}
			if (this.makeUndestructible)
			{
				if (this.buyWeapon.CurrentWeapon.IndestructiblePrice > 0)
				{
					num7 = this.buyWeapon.CurrentWeapon.IndestructiblePrice;
				}
				else
				{
					num7 = (int)((float)this.buyWeapon.CurrentWeapon.price * Globals.I.undestructibleModCoef + 0.5f);
				}
				flag = true;
				text = Language.HGPayQuestion;
			}
		}
		else
		{
			num7 = this.buyWeapon.CurrentWeapon.price;
			if (this.buyWeapon.CurrentWeapon.isPremium)
			{
				str2 = Language.HGRent + " ";
				text = Language.HGRentQuestion;
				flag = true;
			}
			else if (Main.UserInfo.skillUnlocked(Skills.car_weap))
			{
				num7 = (int)Mathf.Round((float)num7 * 0.8f);
			}
			str = Language.HGWeapon;
			if (this.buyWeapon.CurrentWeapon.isPremium)
			{
				if (this.buyType == BuyTypes.PISTOL)
				{
					this.gui.Picture(new Vector2(76f, 58f), this.pistolFrame);
					this.gui.TextField(new Rect(82f, 98f, 100f, 20f), this.buyWeapon.CurrentWeapon.ShortName, 14, "#FFFFFF", TextAnchor.UpperLeft, false, false);
					this.gui.Picture(new Vector2(108f, 64f), this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type]);
					for (int i = 0; i < this.buyWeapon.CurrentWeapon.rentPrice.Length; i++)
					{
						Texture2D texture2D2 = (i != this.selectIndex) ? this.checkbox[0] : this.checkbox[1];
						if (this.gui.Button(new Vector2(263f, (float)(57 + i * 21)), texture2D2, texture2D2, texture2D2, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
						{
							this.selectIndex = i;
						}
						this.gui.TextField(new Rect(293f, (float)(57 + i * 21), 100f, 22f), this.buyWeapon.CurrentWeapon.rentTime[i] + " " + ((this.buyWeapon.CurrentWeapon.rentTime[i] != 1) ? Language.Days_dney : Language.Day), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
						this.gui.TextField(new Rect(288f, (float)(57 + i * 21), 100f, 22f), this.buyWeapon.CurrentWeapon.GetRentPrice(i), 16, "#fbc421", TextAnchor.UpperRight, false, false);
						this.gui.Picture(new Vector2(389f, (float)(57 + i * 21)), this.gui.gldIcon);
						if (this.selectIndex < 3)
						{
							num7 = this.buyWeapon.CurrentWeapon.GetRentPrice(this.selectIndex);
						}
					}
				}
				else
				{
					this.gui.Picture(new Vector2(rect.width / 2f - (float)(this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type].width / 2) - 79f, rect.height / 2f - (float)(this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type].height / 2) - 16f), this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type]);
					this.gui.Picture(new Vector2(25f, 56f), this.weaponFrame);
					this.gui.TextField(new Rect(32f, 95f, 100f, 20f), this.buyWeapon.CurrentWeapon.ShortName, 14, "#FFFFFF", TextAnchor.UpperLeft, false, false);
					for (int j = 0; j < this.buyWeapon.CurrentWeapon.rentPrice.Length; j++)
					{
						Texture2D texture2D3 = (j != this.selectIndex) ? this.checkbox[0] : this.checkbox[1];
						if (this.gui.Button(new Vector2(263f, (float)(57 + j * 21)), texture2D3, texture2D3, texture2D3, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
						{
							this.selectIndex = j;
						}
						this.gui.TextField(new Rect(293f, (float)(57 + j * 21), 100f, 22f), this.buyWeapon.CurrentWeapon.rentTime[j] + " " + ((this.buyWeapon.CurrentWeapon.rentTime[j] != 1) ? Language.Days_dney : Language.Day), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
						this.gui.TextField(new Rect(288f, (float)(57 + j * 21), 100f, 22f), this.buyWeapon.CurrentWeapon.GetRentPrice(j), 16, "#fbc421", TextAnchor.UpperRight, false, false);
						this.gui.Picture(new Vector2(389f, (float)(57 + j * 21)), this.gui.gldIcon);
						if (this.selectIndex < 3)
						{
							num7 = this.buyWeapon.CurrentWeapon.GetRentPrice(this.selectIndex);
						}
					}
				}
				this.gui.Picture(new Vector2(26f, 123f), this.yellowStripe);
				int market_amount = Main.UserInfo.weaponsStates[(int)this.buyWeapon.CurrentWeapon.type].market_amount;
				this.gui.TextField(new Rect(32f, 123f, 200f, 22f), (market_amount <= 0) ? Language.HGNotAvaliable : (Language.HGAvaliable + ": " + market_amount), 16, (market_amount <= 0) ? "#FF3333" : "#FFFFFF", TextAnchor.UpperLeft, false, false);
				if (market_amount > 0)
				{
					Texture2D texture2D4 = (this.selectIndex != 4) ? this.checkbox[0] : this.checkbox[1];
					if (this.gui.Button(new Vector2(263f, 125f), texture2D4, texture2D4, texture2D4, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						this.selectIndex = 4;
					}
				}
				else
				{
					this.gui.Button(new Vector2(263f, 125f), this.checkbox[2], this.checkbox[2], this.checkbox[2], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					if (this.selectIndex == 4)
					{
						this.selectIndex = 0;
						num7 = this.buyWeapon.CurrentWeapon.GetRentPrice(this.selectIndex);
					}
				}
				this.gui.TextField(new Rect(293f, 123f, 100f, 22f), Language.ForeverNormal, 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(288f, 123f, 100f, 22f), this.buyWeapon.CurrentWeapon.getPermanentPrice, 16, "#fbc421", TextAnchor.UpperRight, false, false);
				this.gui.Picture(new Vector2(389f, 123f), this.gui.gldIcon);
				if (this.selectIndex == 4)
				{
					num7 = this.buyWeapon.CurrentWeapon.getPermanentPrice;
					str2 = Language.HGPremiumBuy + " ";
					text = ((market_amount <= 0) ? string.Empty : Language.HGBuyQuestion);
				}
			}
			else
			{
				this.gui.Picture(new Vector2(rect.width / 2f - (float)(this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type].width / 2), rect.height / 2f - (float)(this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type].height / 2) - 10f), this.gui.weapon_unlocked[(int)this.buyWeapon.CurrentWeapon.type]);
				if (this.buyType == BuyTypes.PISTOL)
				{
					this.gui.Picture(new Vector2(rect.width / 2f - (float)(this.pistolFrame.width / 2) + 2f, rect.height / 2f - (float)(this.pistolFrame.height / 2) - 10f), this.pistolFrame);
					this.gui.TextField(new Rect(rect.width / 2f - (float)(this.pistolFrame.width / 2) + 7f, rect.height / 2f, 100f, 20f), this.buyWeapon.CurrentWeapon.ShortName, 14, "#FFFFFF", TextAnchor.UpperLeft, false, false);
				}
				else
				{
					this.gui.Picture(new Vector2(rect.width / 2f - (float)(this.weaponFrame.width / 2) + 2f, rect.height / 2f - (float)(this.weaponFrame.height / 2) - 10f), this.weaponFrame);
					this.gui.TextField(new Rect(rect.width / 2f - (float)(this.weaponFrame.width / 2) + 7f, rect.height / 2f, 100f, 20f), this.buyWeapon.CurrentWeapon.ShortName, 14, "#FFFFFF", TextAnchor.UpperLeft, false, false);
				}
			}
		}
		bool flag2 = false;
		if (flag)
		{
			if (Main.UserInfo.GP >= num7)
			{
				flag2 = true;
			}
		}
		else if (Main.UserInfo.CR >= num7)
		{
			flag2 = true;
		}
		this.gui.TextField(new Rect(30f, 22f, 300f, 40f), str2 + str, 18, "#FFFFFF", TextAnchor.UpperLeft, false, false);
		if (flag)
		{
			if (flag2)
			{
				this.gui.TextField(new Rect(120f, 140f, 100f, 40f), text, 15, "#999999", TextAnchor.LowerRight, false, false);
			}
		}
		else if (flag2)
		{
			this.gui.TextField(new Rect(270f, 112f, 100f, 40f), text, 15, "#999999", TextAnchor.LowerRight, false, false);
		}
		if (flag2 && !flag)
		{
			this.gui.Picture(new Vector2(24f, 130f), this.lowBar);
		}
		this.gui.TextField(new Rect(30f, 150f, 100f, 40f), Language.Price, 25, (!flag2) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(85f, 150f, 100f, 40f), Helpers.SeparateNumericString(num7.ToString()), 25, (!flag2) ? "#FF0000" : ((!flag) ? "#FFFFFF" : "#fbc421"), TextAnchor.MiddleLeft, false, false);
		this.gui.Picture(new Vector2(82f + this.gui.CalcWidth(Helpers.SeparateNumericString(num7.ToString()), this.gui.fontDNC57, 25), (float)((!flag) ? 157 : 158)), (!flag) ? this.crIcon : this.gui.gldIcon);
		if (flag2)
		{
			if (num7 > 0 && this.gui.Button(new Vector2(227f, 151f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.Yes, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, (!this.repair) ? this.gui.buy_sound : this.gui.repair_sound).Clicked && !this.alreadyClicked)
			{
				this.alreadyClicked = true;
				if (this.repair)
				{
					if (this.makeUndestructible)
					{
						Main.AddDatabaseRequest<MakeUndestructable>(new object[]
						{
							(int)this.buyWeapon.CurrentWeapon.type
						});
					}
					else
					{
						Main.AddDatabaseRequest<RepairWeapon>(new object[]
						{
							(int)this.buyWeapon.CurrentWeapon.type,
							this.statedelta
						});
						this.statedelta = 0;
					}
				}
				else
				{
					Main.AddDatabaseRequest<UnlockWeapon>(new object[]
					{
						(int)this.buyWeapon.CurrentWeapon.type,
						(!flag) ? -1 : this.selectIndex
					});
				}
				this.Hide(0.35f);
			}
			if (this.isTutorial)
			{
				GUI.enabled = false;
			}
			if (this.gui.Button(new Vector2(321f, 151f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.No, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.Hide(0.35f);
				this.statedelta = 0;
			}
			GUI.enabled = true;
		}
		else
		{
			if (this.gui.Button(new Vector2(200f, 157f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			this.gui.Picture(new Vector2(24f, 130f), this.lowBarRed);
		}
		if (this.isTutorial)
		{
			GUI.enabled = false;
		}
		if (this.gui.Button(new Vector2(333f, 21f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.Hide(0.35f);
			this.statedelta = 0;
		}
		GUI.enabled = true;
		this.gui.EndGroup();
	}

	// Token: 0x04000976 RID: 2422
	public Texture2D background;

	// Token: 0x04000977 RID: 2423
	public Texture2D crIcon;

	// Token: 0x04000978 RID: 2424
	public Texture2D weaponFrame;

	// Token: 0x04000979 RID: 2425
	public Texture2D pistolFrame;

	// Token: 0x0400097A RID: 2426
	public Texture2D lowBar;

	// Token: 0x0400097B RID: 2427
	public Texture2D lowBarRed;

	// Token: 0x0400097C RID: 2428
	public Texture2D yellowStripe;

	// Token: 0x0400097D RID: 2429
	public Texture2D[] checkbox;

	// Token: 0x0400097E RID: 2430
	public Texture2D[] repairs;

	// Token: 0x0400097F RID: 2431
	private int selectIndex;

	// Token: 0x04000980 RID: 2432
	private BuyTypes buyType;

	// Token: 0x04000981 RID: 2433
	private WeaponInfo buyWeapon;

	// Token: 0x04000982 RID: 2434
	private bool repair;

	// Token: 0x04000983 RID: 2435
	private bool isTutorial;

	// Token: 0x04000984 RID: 2436
	private int statedelta;

	// Token: 0x04000985 RID: 2437
	public bool makeUndestructible;

	// Token: 0x04000986 RID: 2438
	public bool alreadyClicked;

	// Token: 0x04000987 RID: 2439
	public static HelpersGUI I;
}
