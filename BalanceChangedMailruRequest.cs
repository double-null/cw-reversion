using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001EA RID: 490
[AddComponentMenu("Scripts/Game/Events/BalanceChangedMailruRequest")]
internal class BalanceChangedMailruRequest : BalanceChangedRequest
{
	// Token: 0x0600103D RID: 4157 RVA: 0x000B81B8 File Offset: 0x000B63B8
	public override void Initialize(params object[] args)
	{
		BankGUI bankGUI = Forms.Get(typeof(BankGUI)) as BankGUI;
		bankGUI.Hide(0.35f);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.progress, false, true, string.Empty, string.Empty));
		this.Request();
	}

	// Token: 0x0600103E RID: 4158 RVA: 0x000B8214 File Offset: 0x000B6414
	[Obfuscation(Exclude = true)]
	private void Request()
	{
		global::Console.print("mailru balance checker");
		HtmlLayer.Request("?action=payments_pending", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600103F RID: 4159 RVA: 0x000B825C File Offset: 0x000B645C
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
			this.OnFail(new Exception("Data Server Error: " + (string)dictionary["message"]));
			return;
		}
		if ((int)dictionary["payments_pending"] > 0)
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.FundsDelivery, PopupState.information, false, true, string.Empty, string.Empty));
			global::Console.print("Refreshing balance SUCCESS", Color.green);
			Main.AddDatabaseRequest<LoadProfile>(new object[0]);
		}
		else if (this.inv_iterations < 10)
		{
			global::Console.print("Refreshing balance", Color.cyan);
			base.Invoke("Request", 10f);
			this.inv_iterations++;
		}
		else
		{
			global::Console.print("Refreshing balance FAILED, please reload page.", Color.red);
		}
	}

	// Token: 0x040010C1 RID: 4289
	private int inv_iterations;
}
