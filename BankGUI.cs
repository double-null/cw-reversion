using System;
using System.Collections.Generic;
using System.Reflection;
using CWSARequests;
using JsonFx.Json;
using UnityEngine;

// Token: 0x020000D0 RID: 208
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/GUI/BankGUI")]
internal class BankGUI : Form
{
	// Token: 0x06000553 RID: 1363 RVA: 0x00021DC0 File Offset: 0x0001FFC0
	[Obfuscation(Exclude = true)]
	private void LoadTransactionsFinished(string text, string url)
	{
		Dictionary<string, object> dictionary;
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
			global::Console.exception(new Exception("Data Server Error"));
			return;
		}
		Dictionary<string, object>[] array = (Dictionary<string, object>[])dictionary["transactions"];
		if ((int)dictionary["result"] == 0)
		{
			Main.Transactions = new Transaction[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Main.Transactions[i] = new Transaction();
				Main.Transactions[i].Read(array[i]);
			}
			this._bankState = PremiumState.HISTORY;
			EventFactory.Call("HidePopup", new Popup(WindowsID.Invite, Language.BankTransactions, Language.BankTransactionsLoaded, PopupState.information, false, true, string.Empty, string.Empty));
		}
		global::Console.print("TransactionLoad Finished", Color.magenta);
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x00021EDC File Offset: 0x000200DC
	private void BuyButton(int x, int y, float price, int buy, string priceStr, string discount = "", bool gold = true, int packageNumber = 0)
	{
		this.gui.Picture(new Vector2((float)x, (float)y), this.Bank[5]);
		this.gui.Picture(new Vector2((float)(x + 5), (float)(y + 9)), (!gold) ? this.Bank[8] : this.Bank[3]);
		this.gui.Picture(new Vector2((float)(x + 143), (float)(y + 8)), (!gold) ? this.Bank[1] : this.Bank[2]);
		this.gui.TextLabel(new Rect((float)(x + 25), (float)(y + 43), (float)this.Bank[3].width, (float)this.Bank[3].height), priceStr, 17, "#FFFFFF", TextAnchor.MiddleLeft, true);
		if (gold)
		{
			this.gui.TextLabel(new Rect((float)(x + -69), (float)(y + 9), (float)this.Bank[3].width, (float)this.Bank[3].height), "+" + buy, 24, "#000000_Micra", TextAnchor.MiddleRight, true);
			this.gui.TextLabel(new Rect((float)(x + -70), (float)(y + 9), (float)this.Bank[3].width, (float)this.Bank[3].height), "+" + buy, 24, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
			this.gui.TextLabel(new Rect((float)(x + 95), (float)(y + 49), 200f, 22f), discount, 16, "#62b0ff", TextAnchor.MiddleRight, true);
			if (Main.UserInfo.PersonaBankDiscount != null && Main.UserInfo.PersonaBankDiscount.DiscountExists)
			{
				int discountValue = Main.UserInfo.PersonaBankDiscount.DiscountValue;
				int seconds = Main.UserInfo.PersonaBankDiscount.DiscountEnds - HtmlLayer.serverUtc;
				int num = Mathf.CeilToInt((float)buy * ((float)discountValue / 100f));
				this.gui.Picture(new Vector2((float)(x + 28), (float)(y - 10)), this.gui.discountbar);
				this.gui.TextLabel(new Rect((float)(x + 28), (float)(y - 10), (float)this.gui.discountbar.width, (float)this.gui.discountbar.height), string.Concat(new object[]
				{
					Language.Bonus,
					" +",
					num,
					" GP"
				}), 14, "#c3751a_D", TextAnchor.MiddleCenter, true);
				this.gui.Picture(new Vector2((float)(x + 22 + this.gui.discountbar.width), (float)y), this.gui.masteringTextures[1]);
				this.gui.TextLabel(new Rect((float)(x + 22 + this.gui.discountbar.width), (float)(y - 1), (float)this.gui.gui.masteringTextures[1].width, (float)this.gui.gui.masteringTextures[1].height), MainGUI.Instance.SecondsToStringHHHMMSS(seconds), 9, "#FFFFFF_T", TextAnchor.MiddleCenter, true);
			}
		}
		else
		{
			this.gui.TextLabel(new Rect((float)(x - 69), (float)(y + 9), (float)this.Bank[3].width, (float)this.Bank[3].height), "+" + buy, 16, "#000000_Micra", TextAnchor.MiddleRight, true);
			this.gui.TextLabel(new Rect((float)(x - 70), (float)(y + 9), (float)this.Bank[3].width, (float)this.Bank[3].height), "+" + buy, 16, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
		}
		if (this.gui.Button(new Vector2((float)(x + 188), (float)(y + 8)), this.Bank[6], this.Bank[7], null, (!gold) ? (Language.BankGet + " CR") : (Language.BankGet + " GP"), 15, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.lastBuy = buy;
			this.lastBuyGold = gold;
			if (CVars.IsStandaloneRealm)
			{
				Main.AddDatabaseRequest<CWSABuyRequest>(new object[]
				{
					gold,
					packageNumber
				});
			}
			else if (CVars.realm == "vk" || CVars.realm == "omega" || CVars.realm == "local" || CVars.realm == "release" || CVars.realm == "kg" || CVars.realm == "fr")
			{
				Application.ExternalCall("Payment", new object[]
				{
					((!gold) ? "cr" : "gp") + "-" + buy
				});
			}
			else if (CVars.realm == "fb")
			{
				Application.ExternalCall("Payment", new object[]
				{
					(!gold) ? "cr" : "gp",
					(!gold) ? (buy / Main.UserInfo.CurrencyInfo.MultiplierCr) : (buy / Main.UserInfo.CurrencyInfo.MultiplierGp)
				});
			}
			else if (CVars.realm == "ag" || CVars.realm == "mc")
			{
				Application.ExternalCall("Payment", new object[]
				{
					price,
					LoadProfile.XsollaToken
				});
			}
			else if (CVars.realm == "ok")
			{
				Application.ExternalCall("Payment", new object[]
				{
					price,
					JsonWriter.Serialize(Globals.I.Bank)
				});
			}
			else
			{
				Application.ExternalCall("Payment", new object[]
				{
					price
				});
			}
			global::Console.print(string.Concat(new object[]
			{
				"Buy ",
				buy,
				(!gold) ? "CR" : "GP",
				" for ",
				price,
				"votes"
			}));
		}
	}

	// Token: 0x06000555 RID: 1365 RVA: 0x000225B4 File Offset: 0x000207B4
	private static byte ToByte(float f)
	{
		f = Mathf.Clamp01(f);
		return (byte)(f * 255f);
	}

	// Token: 0x06000556 RID: 1366 RVA: 0x000225C8 File Offset: 0x000207C8
	private void UpdateStandaloneBalanceButton(int x, int y)
	{
		this.gui.Picture(new Vector2((float)(x + 50), (float)(y + 3)), this.Bank[3]);
		float num = (!this._updateBalanceTimer.IsStarted) ? 1f : (this._updateBalanceTimer.Time / 5f);
		Color color = new Color(1f, num, num);
		string textColor = string.Format("#{0:X2}{1:X2}{2:X2}", BankGUI.ToByte(color.r), BankGUI.ToByte(color.g), BankGUI.ToByte(color.b));
		if (this.gui.Button(new Vector2((float)(x + 94), (float)y), this.Bank[6], this.Bank[7], null, Language.CWSAUpdateBalance, 15, textColor, TextAnchor.MiddleCenter, null, null, null, null).Clicked && (!this._updateBalanceTimer.IsStarted || this._updateBalanceTimer.Time > 5f))
		{
			this._updateBalanceTimer.Start();
			Main.AddDatabaseRequest<CWSAUpdateBalanceRequest>(new object[0]);
		}
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x00022700 File Offset: 0x00020900
	private void BuySpButton(int x, int y)
	{
		int buySPPrice = Globals.I.buySPPrice;
		Int sp_available = Main.UserInfo.sp_available;
		this.gui.Picture(new Vector2((float)x, (float)y), this.Bank[5]);
		this.gui.Picture(new Vector2((float)(x + 143), (float)(y + 24)), this.Bank[2]);
		this.gui.Picture(new Vector2((float)(x + 50), (float)(y + 24)), this.Bank[9]);
		this.gui.TextLabel(new Rect(372f, 60f, 294f, 41f), Language.BankSPTitle, 15, "#ffffff", TextAnchor.UpperCenter, true);
		this.gui.TextLabel(new Rect((float)(x + 30), (float)(y - 2), 60f, (float)this.Bank[3].height), Language.BankAvaliable + ":", 14, "#FFFFFF", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect((float)(x + 100), (float)(y - 2), 90f, (float)this.Bank[3].height), Language.BankCostCAPS + " 1 SP", 14, "#FFFFFF", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect((float)(x + 70), (float)(y + 25), 71f, (float)this.Bank[3].height), buySPPrice.ToString(), 30, (!(Main.UserInfo.GP >= buySPPrice)) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleRight, true);
		this.gui.TextLabel(new Rect((float)x, (float)(y + 25), 50f, (float)this.Bank[3].height), sp_available.ToString(), 30, (!(sp_available > 0)) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleRight, true);
		if (Main.UserInfo.GP >= buySPPrice && sp_available > 0 && this.gui.Button(new Vector2((float)(x + 188), (float)(y + 24)), this.Bank[6], this.Bank[7], null, Language.BankGet + " SP", 15, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BankBuySP, string.Empty, PopupState.buySP, false, true, string.Empty, string.Empty));
		}
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x000229A0 File Offset: 0x00020BA0
	private void BuyMpButton(int x, int y)
	{
		Texture2D texture2D = this.Bank[5];
		Texture2D picture = MainGUI.Instance.masteringTextures[0];
		Texture2D picture2 = this.Bank[2];
		this.gui.TextLabel(new Rect((float)x, (float)(y - ((!CVars.IsVanilla) ? 28 : 46)), (float)texture2D.width, 30f), Language.BankMPTitle, 15, "#ffffff", TextAnchor.LowerCenter, true);
		this.gui.Picture(new Vector2((float)x, (float)y), texture2D);
		this.gui.Picture(new Vector2((float)(x + 20), (float)(y + 5)), picture);
		this.gui.Picture(new Vector2((float)(x + 143), (float)(y + 24)), picture2);
		this.gui.TextLabel(new Rect((float)(x + 100), (float)(y - 2), 90f, (float)this.Bank[3].height), Language.BankCostCAPS + " 1 MP", 14, "#FFFFFF", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect((float)(x + 70), (float)(y + 25), 71f, (float)this.Bank[3].height), Globals.I.MpCost.ToString(), 30, (!(Main.UserInfo.GP >= Globals.I.MpCost)) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleRight, true);
		GUI.enabled = (Main.UserInfo.GP >= Globals.I.MpCost);
		if (this.gui.Button(new Vector2((float)(x + 188), (float)(y + 24)), this.Bank[6], this.Bank[7], null, Language.BankGet + " MP", 15, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BankMPTitle, string.Empty, PopupState.buyMp, false, true, string.Empty, string.Empty));
		}
		GUI.enabled = true;
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x00022BC4 File Offset: 0x00020DC4
	private void DrawHints()
	{
		this.gui.color = new Color(1f, 1f, 1f, (base.visibility >= 0.3f) ? 0.3f : base.visibility);
		GUI.DrawTexture(new Rect(35f, 70f, 311f, 40f), MainGUI.Instance.black);
		if (CVars.IsVanilla)
		{
			GUI.DrawTexture(new Rect(365f, 70f, 311f, 40f), MainGUI.Instance.black);
		}
		else
		{
			GUI.DrawTexture(new Rect(365f, 60f, 311f, 20f), MainGUI.Instance.black);
			GUI.DrawTexture(new Rect(365f, 161f, 311f, 20f), MainGUI.Instance.black);
			GUI.DrawTexture(new Rect(365f, 259f, 311f, 20f), MainGUI.Instance.black);
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if (CVars.IsVanilla)
		{
			this.BuyMpButton(365, 120);
		}
		else
		{
			this.BuySpButton(365, 85);
			this.BuyMpButton(365, 183);
		}
	}

	// Token: 0x0600055A RID: 1370 RVA: 0x00022D48 File Offset: 0x00020F48
	private void FbBuy()
	{
		int multiplierCr = Main.UserInfo.CurrencyInfo.MultiplierCr;
		int multiplierGp = Main.UserInfo.CurrencyInfo.MultiplierGp;
		CurrencyInfo currencyInfo = Main.UserInfo.CurrencyInfo;
		this.gui.TextLabel(new Rect(35f, 70f, 310f, 41f), Language.BankFBTitle0, 15, "#ffffff", TextAnchor.UpperCenter, true);
		this.gui.TextLabel(new Rect(372f, 258f, 300f, 41f), Language.BankCRTitle, 15, "#ffffff", TextAnchor.UpperCenter, true);
		this.BuyButton(35, 440, (float)((int)currencyInfo.PricesGp[0]), (int)(currencyInfo.AmounGp[0] * (float)multiplierGp), Language.BankFor + this.FBPrice(currencyInfo.PricesGp[0]), string.Empty, true, 0);
		this.BuyButton(35, 360, (float)((int)currencyInfo.PricesGp[1]), (int)(currencyInfo.AmounGp[1] * (float)multiplierGp), Language.BankFor + this.FBPrice(currencyInfo.PricesGp[1]), string.Empty, true, 0);
		this.BuyButton(35, 280, (float)((int)currencyInfo.PricesGp[2]), (int)(currencyInfo.AmounGp[2] * (float)multiplierGp), Language.BankFor + this.FBPrice(currencyInfo.PricesGp[2]), string.Empty, true, 0);
		this.BuyButton(35, 200, (float)((int)currencyInfo.PricesGp[3]), (int)(currencyInfo.AmounGp[3] * (float)multiplierGp), Language.BankFor + this.FBPrice(currencyInfo.PricesGp[3]), string.Empty, true, 0);
		this.BuyButton(35, 120, (float)((int)currencyInfo.PricesGp[4]), (int)(currencyInfo.AmounGp[4] * (float)multiplierGp), Language.BankFor + this.FBPrice(currencyInfo.PricesGp[4]), string.Empty, true, 0);
		this.BuyButton(365, 440, (float)((int)currencyInfo.PricesCr[0]), (int)(currencyInfo.AmounCr[0] * (float)multiplierCr), Language.BankFor + this.FBPrice(currencyInfo.PricesCr[0]), string.Empty, false, 0);
		this.BuyButton(365, 360, (float)((int)currencyInfo.PricesCr[2]), (int)(currencyInfo.AmounCr[2] * (float)multiplierCr), Language.BankFor + this.FBPrice(currencyInfo.PricesCr[2]), string.Empty, false, 0);
		this.BuyButton(365, 280, (float)((int)currencyInfo.PricesCr[3]), (int)(currencyInfo.AmounCr[3] * (float)multiplierCr), Language.BankFor + this.FBPrice(currencyInfo.PricesCr[3]), string.Empty, false, 0);
	}

	// Token: 0x0600055B RID: 1371 RVA: 0x00022FFC File Offset: 0x000211FC
	private void Buy(string realm)
	{
		string text = Language.BankFor + " ";
		string bankCurrency = Language.BankCurrency;
		string text2;
		string content;
		string content2;
		switch (realm)
		{
		case "ok":
			text2 = Language.BankCurOK;
			content = Language.BankOKTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "mailru":
			text2 = Language.BankCurMailru;
			content = Language.BankMailruTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "kg":
			text2 = "KREDS";
			content = Language.BankVKTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "fr":
			text2 = "FRIENDSTER COINS";
			content = Language.BankFRTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "ag":
			text2 = "$";
			content = Language.BankFBTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "mc":
			text2 = "$";
			content = Language.BankFBTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "yg":
			text2 = "$";
			content = Language.BankFBTitle0;
			content2 = Language.BankCRTitle;
			goto IL_1FE;
		case "standalone":
		{
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage != ELanguage.EN)
			{
				if (currentLanguage != ELanguage.RU)
				{
					text2 = "$";
				}
				else
				{
					text2 = "РУБ.";
				}
			}
			else
			{
				text2 = "$";
			}
			content2 = (content = string.Empty);
			goto IL_1FE;
		}
		}
		text2 = Language.BankVote_golosov;
		content = Language.BankVKTitle0;
		content2 = Language.BankCRTitle;
		IL_1FE:
		if (realm == "standalone")
		{
			this.UpdateStandaloneBalanceButton(34, 70);
		}
		else
		{
			this.gui.TextLabel(new Rect(34f, 70f, 310f, 41f), content, 15, "#ffffff", TextAnchor.UpperCenter, true);
			this.gui.TextLabel(new Rect(372f, 258f, 294f, 41f), content2, 15, "#ffffff", TextAnchor.UpperCenter, true);
		}
		if (CVars.IsStandaloneRealm)
		{
			CVars.RealCurrencyMultiple = 0f;
		}
		int num = 440;
		Dictionary<int, float> dictionary;
		if (CVars.IsStandaloneRealm)
		{
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage != ELanguage.EN)
			{
				if (currentLanguage != ELanguage.RU)
				{
					dictionary = this._gpDict2ndLang;
				}
				else
				{
					dictionary = this._gpDict;
				}
			}
			else
			{
				dictionary = this._gpDict2ndLang;
			}
		}
		else
		{
			dictionary = this._gpDict;
		}
		int num2 = 0;
		foreach (KeyValuePair<int, float> keyValuePair in dictionary)
		{
			if (realm == "release")
			{
				text2 = ((keyValuePair.Value >= 5f) ? Language.BankVote_golosov : Language.BankVote_golosa);
			}
			string priceStr = (CVars.RealCurrencyMultiple <= 0f) ? string.Concat(new object[]
			{
				text,
				keyValuePair.Value,
				" ",
				text2
			}) : string.Concat(new object[]
			{
				text,
				keyValuePair.Value,
				" ",
				text2,
				" (~",
				(keyValuePair.Value * CVars.RealCurrencyMultiple).ToString("F0"),
				" ",
				bankCurrency,
				")"
			});
			int packageNumber = num2 + 1;
			this.BuyButton(35, num, keyValuePair.Value, keyValuePair.Key, priceStr, string.Empty, true, packageNumber);
			num2++;
			num -= 80;
		}
		num = 440;
		num2 = 0;
		Dictionary<int, float> dictionary2;
		if (CVars.IsStandaloneRealm)
		{
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage != ELanguage.EN)
			{
				if (currentLanguage != ELanguage.RU)
				{
					dictionary2 = this._crDict2ndLang;
				}
				else
				{
					dictionary2 = this._crDict;
				}
			}
			else
			{
				dictionary2 = this._crDict2ndLang;
			}
		}
		else
		{
			dictionary2 = this._crDict;
		}
		foreach (KeyValuePair<int, float> keyValuePair2 in dictionary2)
		{
			if (realm == "release")
			{
				text2 = ((keyValuePair2.Value >= 5f) ? Language.BankVote_golosov : Language.BankVote_golosa);
			}
			string priceStr = (CVars.RealCurrencyMultiple <= 0f) ? string.Concat(new object[]
			{
				text,
				keyValuePair2.Value,
				" ",
				text2
			}) : string.Concat(new object[]
			{
				text,
				keyValuePair2.Value,
				" ",
				text2,
				" (~",
				(keyValuePair2.Value * CVars.RealCurrencyMultiple).ToString("F0"),
				" ",
				bankCurrency,
				")"
			});
			if (num2 != 2 || CVars.IsStandaloneRealm)
			{
				this.BuyButton(365, num, keyValuePair2.Value, keyValuePair2.Key, priceStr, string.Empty, false, num2 + 1);
				num -= 80;
			}
			num2++;
		}
	}

	// Token: 0x0600055C RID: 1372 RVA: 0x00023620 File Offset: 0x00021820
	private string FBPrice(float price)
	{
		string text = Main.UserInfo.CurrencyInfo.CurrencySymbol;
		bool flag = Main.UserInfo.CurrencyInfo.PrefixSymbol;
		if (text == string.Empty)
		{
			text = Main.UserInfo.CurrencyInfo.CurrencyName;
			flag = false;
		}
		return string.Concat(new string[]
		{
			" ",
			(!flag) ? string.Empty : text,
			price.ToString("F0"),
			" ",
			flag ? string.Empty : (" " + text)
		});
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x000236CC File Offset: 0x000218CC
	[Obfuscation(Exclude = true)]
	private void ShowBankGUI(object obj)
	{
		this.Show(0.5f, 0f);
		this._bankState = PremiumState.BUY;
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x000236E8 File Offset: 0x000218E8
	[Obfuscation(Exclude = true)]
	private void HideBankGUI(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x170000BD RID: 189
	// (get) Token: 0x0600055F RID: 1375 RVA: 0x000236F8 File Offset: 0x000218F8
	public override Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width - this.gui.Width) * 0.5f, (float)(Screen.height - this.gui.Height) * 0.5f, (float)this.gui.Width, (float)this.gui.Height);
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00023754 File Offset: 0x00021954
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
		this._carrierGui = this.gui.GetComponent<CarrierGUI>();
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00023774 File Offset: 0x00021974
	public override void Register()
	{
		EventFactory.Register("ShowBankGUI", this);
		EventFactory.Register("HideBankGUI", this);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0002378C File Offset: 0x0002198C
	public override void InterfaceGUI()
	{
		if (!this._initPrices)
		{
			this.InitPrices(Globals.I.Bank, false);
			if (CVars.IsStandaloneRealm)
			{
				this.InitPrices(Globals.I.Bank2ndLang, true);
			}
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		this.gui.Picture(new Vector2(50f, 40f), this.gui.games_bg);
		this.gui.BeginGroup(new Rect(50f, 40f, (float)this.gui.games_bg.width, (float)this.gui.games_bg.height));
		if (this.gui.Button(new Vector2(608f, 23f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.Hide(0.35f);
		}
		this.gui.Picture(new Vector2(293f, 27f), this.Bank[4]);
		Texture2D idle = this.gui.server_window[14];
		Texture2D over = this.gui.server_window[15];
		Texture2D selected = this.gui.server_window[16];
		if (this._bankState == PremiumState.BUY)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(24f, 23f), idle, over, selected, Language.BankTransaction, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(24f, 23f), idle, over, selected, Language.BankTransaction, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._bankState = PremiumState.BUY;
		}
		if (this._bankState == PremiumState.HISTORY)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(113f, 23f), idle, over, selected, Language.BankHistory, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(113f, 23f), idle, over, selected, Language.BankHistory, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BankTransactions, Language.BankTransactionsLoading, PopupState.progress, false, true, string.Empty, string.Empty));
			HtmlLayer.Request("?action=get_transactions&user_id=" + Main.UserInfo.userID, new RequestFinished(this.LoadTransactionsFinished), null, string.Empty, string.Empty);
			global::Console.print("TransactionLoad", Color.magenta);
		}
		if (this._bankState == PremiumState.SERVICES)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(201f, 23f), idle, over, selected, Language.BankServices, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(201f, 23f), this.gui.server_window[20], this.gui.server_window[20], this.gui.server_window[20], Language.BankServices, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
		}
		if (this._bankState == PremiumState.BUY)
		{
			this.DrawHints();
			string realm = CVars.realm;
			if (realm != null)
			{
				if (BankGUI.<>f__switch$map5 == null)
				{
					BankGUI.<>f__switch$map5 = new Dictionary<string, int>(1)
					{
						{
							"fb",
							0
						}
					};
				}
				int num;
				if (BankGUI.<>f__switch$map5.TryGetValue(realm, out num))
				{
					if (num == 0)
					{
						this.FbBuy();
						goto IL_46D;
					}
				}
			}
			this.Buy(CVars.realm);
		}
		IL_46D:
		if (this._bankState == PremiumState.HISTORY)
		{
			this.gui.Picture(new Vector2(22f, 56f), this._carrierGui.top_bg);
			this.gui.TextLabel(new Rect(243f, 57f, 200f, 28f), Language.BankOperationHistory, 16, "#FFFFFF", TextAnchor.UpperCenter, true);
			this.gui.Picture(new Vector2(663f, 101f), this.gui.server_window[10]);
			this.gui.TextButton(new Rect(17f, 80f, 70f, 25f), "№", 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.TextButton(new Rect(140f, 80f, 90f, 25f), Language.BankQuantity, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.TextButton(new Rect(204f, 80f, 90f, 25f), Language.BankValuta, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.TextButton(new Rect(429f, 80f, 100f, 25f), Language.BankComment, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleRight, null, null, null, null);
			this.gui.TextButton(new Rect(518f, 80f, 70f, 25f), Language.BankDate, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			int num2 = Main.Transactions.Length * 40;
			if (this._scrollPos.y > (float)num2)
			{
				this._scrollPos.y = (float)num2;
			}
			this._scrollPos = this.gui.BeginScrollView(new Rect(23f, 105f, 657f, 405f), this._scrollPos, new Rect(0f, 0f, 200f, (float)num2), float.MaxValue);
			for (int i = 0; i < Main.Transactions.Length; i++)
			{
				this.gui.HoverPicture(new Vector2(0f, (float)(40 * i)), this._carrierGui.ratingRecord[0], this._carrierGui.ratingRecord[1]);
				this.gui.TextLabel(new Rect(-13f, (float)(40 * i + 6), 80f, 28f), i + 1, 20, "#FFFFFF", TextAnchor.MiddleCenter, true);
				Texture2D picture = this.gui.crIcon;
				if (Main.Transactions[i].currency == 2 || Main.Transactions[i].currency == 0)
				{
					picture = this.gui.gldIcon;
				}
				if (Main.Transactions[i].currency == 3)
				{
					picture = this.gui.masteringTextures[6];
				}
				this.gui.Picture(new Vector2(213f, (float)(40 * i + ((Main.Transactions[i].currency != 3) ? 7 : 13))), picture);
				this.gui.Picture(new Vector2(60f, (float)(40 * i + 2)), (Main.Transactions[i].currency != 2) ? this.Bank[8] : this.Bank[3]);
				this.gui.TextLabel(new Rect(8f, (float)(40 * i + 6), 200f, 28f), Main.Transactions[i].amount, 17, "#000000_Micra", TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(7f, (float)(40 * i + 6), 200f, 28f), Main.Transactions[i].amount, 17, "#FFFFFF_Micra", TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(246f, (float)(40 * i + 5), 256f, 28f), Main.Transactions[i].comment, 19, "#FFFFFF", TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(433f, (float)(40 * i + 5), 200f, 28f), Main.Transactions[i].date, 19, "#FFFFFF", TextAnchor.UpperRight, true);
			}
			this.gui.EndScrollView();
		}
		if (this._bankState == PremiumState.SERVICES)
		{
			this.gui.Picture(new Vector2(22f, 56f), this._carrierGui.top_bg);
			this.gui.TextLabel(new Rect(243f, 57f, 200f, 28f), "УСЛУГИ", 16, "#FFFFFF", TextAnchor.UpperCenter, true);
			this.gui.Picture(new Vector2(663f, 101f), this.gui.server_window[10]);
			this.gui.TextButton(new Rect(17f, 80f, 70f, 25f), "Кол-во", 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			this.gui.TextButton(new Rect(140f, 80f, 90f, 25f), "Услуга", 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null);
			int num3 = Main.Transactions.Length * 40;
			if (this._scrollPos.y > (float)num3)
			{
				this._scrollPos.y = (float)num3;
			}
			this._scrollPos = this.gui.BeginScrollView(new Rect(23f, 105f, 657f, 405f), this._scrollPos, new Rect(0f, 0f, 200f, (float)num3), float.MaxValue);
			this.gui.HoverPicture(new Vector2(0f, 0f), this._carrierGui.ratingRecord[0], this._carrierGui.ratingRecord[1]);
			this.gui.TextLabel(new Rect(-13f, 6f, 80f, 28f), "∞", 20, "#FFFFFF", TextAnchor.MiddleCenter, true);
			this.gui.TextLabel(new Rect(302f, 5f, 200f, 28f), "Сброс аккаунта", 19, "#FFFFFF", TextAnchor.UpperLeft, true);
			this.gui.EndScrollView();
		}
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00024354 File Offset: 0x00022554
	private void InitPrices(Dictionary<string, object> bank, bool secondLang = false)
	{
		object obj;
		if (bank.TryGetValue("1", out obj))
		{
			Dictionary<int, float> dictionary;
			if (secondLang)
			{
				dictionary = (this._crDict2ndLang = new Dictionary<int, float>());
			}
			else
			{
				dictionary = (this._crDict = new Dictionary<int, float>());
			}
			foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)obj))
			{
				dictionary.Add(int.Parse(keyValuePair.Key), float.Parse(keyValuePair.Value.ToString()));
			}
		}
		object obj2;
		if (bank.TryGetValue("2", out obj2))
		{
			Dictionary<int, float> dictionary2;
			if (secondLang)
			{
				dictionary2 = (this._gpDict2ndLang = new Dictionary<int, float>());
			}
			else
			{
				dictionary2 = (this._gpDict = new Dictionary<int, float>());
			}
			foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)obj2))
			{
				dictionary2.Add(int.Parse(keyValuePair2.Key), float.Parse(keyValuePair2.Value.ToString()));
			}
		}
	}

	// Token: 0x040004C1 RID: 1217
	private const float UpdateBalanceCoolDown = 5f;

	// Token: 0x040004C2 RID: 1218
	[HideInInspector]
	public int lastBuy;

	// Token: 0x040004C3 RID: 1219
	[HideInInspector]
	public bool lastBuyGold;

	// Token: 0x040004C4 RID: 1220
	public Texture2D[] Bank;

	// Token: 0x040004C5 RID: 1221
	private Vector2 _scrollPos = Vector2.zero;

	// Token: 0x040004C6 RID: 1222
	private PremiumState _bankState;

	// Token: 0x040004C7 RID: 1223
	private CarrierGUI _carrierGui;

	// Token: 0x040004C8 RID: 1224
	private Dictionary<int, float> _gpDict;

	// Token: 0x040004C9 RID: 1225
	private Dictionary<int, float> _crDict;

	// Token: 0x040004CA RID: 1226
	private Dictionary<int, float> _gpDict2ndLang;

	// Token: 0x040004CB RID: 1227
	private Dictionary<int, float> _crDict2ndLang;

	// Token: 0x040004CC RID: 1228
	private bool _initPrices;

	// Token: 0x040004CD RID: 1229
	private readonly Timer _updateBalanceTimer = new Timer();
}
