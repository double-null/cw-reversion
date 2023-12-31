using System;
using System.Collections.Generic;
using namespaceMainGUI;
using UnityEngine;

// Token: 0x020001EE RID: 494
[AddComponentMenu("Scripts/Game/Events/BuyBox")]
internal class BuyBox : DatabaseEvent
{
	// Token: 0x0600104D RID: 4173 RVA: 0x000B88F4 File Offset: 0x000B6AF4
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.BuyBox, Language.BuyBoxProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuyBox", Color.grey);
		HtmlLayer.Request("?action=buybox&box_id=" + BuyBox.boxIndexCached.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600104E RID: 4174 RVA: 0x000B8978 File Offset: 0x000B6B78
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.BuyBox, Language.BoxDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("BuyBox Finished", Color.green);
		Main.UserInfo.GP = (int)dictionary["new_gp"];
		Main.UserInfo.CR = (int)dictionary["new_cr"];
		BuyBox.pack.Unlock();
		if (BuyBox.toRefresh != null)
		{
			BuyBox.toRefresh.Refresh();
		}
	}

	// Token: 0x040010C3 RID: 4291
	public static int boxIndexCached;

	// Token: 0x040010C4 RID: 4292
	public static int price;

	// Token: 0x040010C5 RID: 4293
	public static bool isGP;

	// Token: 0x040010C6 RID: 4294
	public static string BoxName = string.Empty;

	// Token: 0x040010C7 RID: 4295
	public static Packages pack;

	// Token: 0x040010C8 RID: 4296
	public static PackageSet toRefresh;
}
