using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020C RID: 524
internal class LoadClanTransaction : DatabaseEvent
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x000BBE9C File Offset: 0x000BA09C
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, 0, 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.ClanTransactionLoading, PopupState.progress, false, true, string.Empty, string.Empty));
		HtmlLayer.Request("clans.php?action=get_transactions&clan_id=" + str, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x000BBF18 File Offset: 0x000BA118
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.BankBuy, Language.ProcessingRequest, Language.ClanTransactionLoading, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Clan transaction history loaded", Color.green);
		if (dictionary.ContainsKey("data"))
		{
			Main.UserInfo.clanData.ParseClanTransaction((Dictionary<string, object>[])dictionary["data"]);
		}
		this.SuccessAction();
	}

	// Token: 0x060010C2 RID: 4290 RVA: 0x000BC02C File Offset: 0x000BA22C
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.BankBuy, Language.ClanTransactionLoadingFailed, e.ToString(), PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
		this.FailedAction();
	}

	// Token: 0x040010DB RID: 4315
	private ClanTransactionData downloadInfo = new ClanTransactionData();
}
