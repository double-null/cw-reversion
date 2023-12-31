using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020A RID: 522
internal class FillUpClanBalance : DatabaseEvent
{
	// Token: 0x060010B8 RID: 4280 RVA: 0x000BB9BC File Offset: 0x000B9BBC
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, 0, 0);
		string str2 = (string)Crypt.ResolveVariable(args, 0, 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.progress, false, false, string.Empty, string.Empty));
		HtmlLayer.Request("clans.php?action=transfer&credits=" + str + "&gp=" + str2, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010B9 RID: 4281 RVA: 0x000BBA54 File Offset: 0x000B9C54
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Clan balance replenished    ", Color.green);
		Main.UserInfo.CR = (int)dictionary["new_cr"];
		Main.UserInfo.GP = (int)dictionary["new_gp"];
		this.SuccessAction();
	}

	// Token: 0x060010BA RID: 4282 RVA: 0x000BBB78 File Offset: 0x000B9D78
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.FundsDeliveryFailed, e.ToString(), PopupState.information, true, true, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}
}
