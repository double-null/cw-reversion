using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F4 RID: 500
[AddComponentMenu("Scripts/Game/Events/BuySet")]
internal class BuySet : DatabaseEvent
{
	// Token: 0x06001062 RID: 4194 RVA: 0x000B9314 File Offset: 0x000B7514
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.BuySet, Language.BuyKitProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuySet", Color.grey);
		BuySet.setIndexCached = num;
		HtmlLayer.Request("?action=buyset&set_index=" + num.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001063 RID: 4195 RVA: 0x000B93AC File Offset: 0x000B75AC
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.BuySet, Language.KitDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("BuySet Finished", Color.green);
		Main.UserInfo.unlockedSets[BuySet.setIndexCached] = 1;
		Main.UserInfo.GP = (int)dictionary["new_gp"];
	}

	// Token: 0x040010CB RID: 4299
	public static int setIndexCached;
}
