using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F2 RID: 498
[AddComponentMenu("Scripts/Game/Events/BuyNick")]
internal class BuyNickColor : DatabaseEvent
{
	// Token: 0x0600105A RID: 4186 RVA: 0x000B8F00 File Offset: 0x000B7100
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.BuyNickColor, Language.BuyNickColorProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("BuyNickColor", Color.grey);
		string text = (string)args[0];
		this._nickColor = text;
		HtmlLayer.Request("adm.php?q=customization/player/nicknamecolor/" + text, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600105B RID: 4187 RVA: 0x000B8F88 File Offset: 0x000B7188
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.BuyNickColor, Language.NickDelivered, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("BuyNickColor Finished", Color.green);
		Main.UserInfo.GP -= Globals.I.buyNickColorChangePrice;
		Main.UserInfo.nickColor = "#" + this._nickColor;
	}

	// Token: 0x0600105C RID: 4188 RVA: 0x000B909C File Offset: 0x000B729C
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}

	// Token: 0x040010CA RID: 4298
	private string _nickColor;
}
