using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EB RID: 491
[AddComponentMenu("Scripts/Game/Events/BalanceChangedRequest")]
internal class BalanceChangedRequest : DatabaseEvent
{
	// Token: 0x06001041 RID: 4161 RVA: 0x000B83C8 File Offset: 0x000B65C8
	public override void Initialize(params object[] args)
	{
		int num = 0;
		try
		{
			num = int.Parse(args[0].ToString());
		}
		catch (Exception innerException)
		{
			global::Console.print(new Exception("External params error, value = " + ((args.Length <= 0) ? "NULL" : args[0]) + "\n", innerException));
			return;
		}
		global::Console.print("Balance changed: " + num.ToString());
		BankGUI bankGUI = Forms.Get(typeof(BankGUI)) as BankGUI;
		bankGUI.Hide(0.35f);
		HtmlLayer.Request("?action=buy&cur=" + ((!bankGUI.lastBuyGold) ? "1" : "2") + "&amount=" + bankGUI.lastBuy.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.progress, false, false, string.Empty, string.Empty));
	}

	// Token: 0x06001042 RID: 4162 RVA: 0x000B84FC File Offset: 0x000B66FC
	protected override void OnResponse(string text)
	{
		base.OnResponse(text);
		EventFactory.Call("HidePopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.information, false, false, string.Empty, string.Empty));
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
			this.OnFail(new Exception("Data Server Error: " + (string)dictionary["message"]));
			return;
		}
		if ((int)dictionary["currency"] == 2)
		{
			Main.UserInfo.GP = (int)dictionary["new_amount"];
		}
		else
		{
			Main.UserInfo.CR = (int)dictionary["new_amount"];
		}
	}

	// Token: 0x06001043 RID: 4163 RVA: 0x000B863C File Offset: 0x000B683C
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.FundsDeliveryFailed, e.ToString(), PopupState.information, true, true, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
