using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001EF RID: 495
[AddComponentMenu("Scripts/Game/Events/BuyKit")]
internal class BuyKit : DatabaseEvent
{
	// Token: 0x06001050 RID: 4176 RVA: 0x000B8AB8 File Offset: 0x000B6CB8
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.BuyKit, Language.BuyKitProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuyKit", Color.grey);
		BuyKit.KitIndexCached = num;
		HtmlLayer.Request("?action=buykit&kit_index=" + num, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001051 RID: 4177 RVA: 0x000B8B50 File Offset: 0x000B6D50
	protected override void OnResponse(string text)
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
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.BuyKit, Language.KitDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("BuyKit Finished", Color.green);
		Main.UserInfo.suits[BuyKit.KitIndexCached].Unlocked = true;
		Main.UserInfo.GP = (int)dictionary["new_gp"];
	}

	// Token: 0x040010C9 RID: 4297
	public static int KitIndexCached;
}
