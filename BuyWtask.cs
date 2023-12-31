using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F5 RID: 501
[AddComponentMenu("Scripts/Game/Events/BuyWtask")]
internal class BuyWtask : DatabaseEvent
{
	// Token: 0x06001065 RID: 4197 RVA: 0x000B94C0 File Offset: 0x000B76C0
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ThePurchaseOfModification, Language.BuyKitProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuyWtask", Color.grey);
		HtmlLayer.Request("?action=buy_wtask&weapon_index=" + BuyWtask.unlockWtaskCached.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001066 RID: 4198 RVA: 0x000B9544 File Offset: 0x000B7744
	protected override void OnResponse(string text)
	{
		base.OnResponse(text);
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
		Main.UserInfo.weaponsStates[BuyWtask.unlockWtaskCached].wtaskCurrent = -77f;
		Main.UserInfo.GP = (int)dictionary["new_gp"];
		if (Utility.IsModableWeapon(BuyWtask.unlockWtaskCached))
		{
			Main.AddDatabaseRequestCallBack<BuyMasteringWtask>(delegate
			{
				global::Console.print("BuyWtask Finished", Color.green);
				EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ThePurchaseOfModification, Language.KitDelivered, PopupState.information, false, false, string.Empty, string.Empty));
			}, delegate
			{
			}, new object[]
			{
				BuyWtask.unlockWtaskCached
			});
		}
		else
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ThePurchaseOfModification, Language.KitDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		}
	}

	// Token: 0x040010CC RID: 4300
	public static int unlockWtaskCached;
}
