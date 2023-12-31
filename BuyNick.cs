using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F1 RID: 497
[AddComponentMenu("Scripts/Game/Events/BuyNick")]
internal class BuyNick : DatabaseEvent
{
	// Token: 0x06001056 RID: 4182 RVA: 0x000B8D38 File Offset: 0x000B6F38
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.BuyNick, Language.BuyNickProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuyNick", Color.grey);
		HtmlLayer.Request("?action=buyNickChanges", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001057 RID: 4183 RVA: 0x000B8DAC File Offset: 0x000B6FAC
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.BuyNick, Language.NickDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("BuyNick Finished", Color.green);
		Main.UserInfo.GP = (int)dictionary["new_gp"];
		UserInfo userInfo = Main.UserInfo;
		userInfo.nickChange = ++userInfo.nickChange;
	}

	// Token: 0x06001058 RID: 4184 RVA: 0x000B8EBC File Offset: 0x000B70BC
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
