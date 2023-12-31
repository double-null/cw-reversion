using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000232 RID: 562
[AddComponentMenu("Scripts/Game/Events/SaveProfile")]
internal class SaveProfile : DatabaseEvent
{
	// Token: 0x06001170 RID: 4464 RVA: 0x000C2524 File Offset: 0x000C0724
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Save, Language.CWMainSave, Language.CWMainSaveDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Save", Color.grey);
		string data = Main.UserInfo.ToJSON(false);
		HtmlLayer.SendCompressed("?action=save", data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x000C259C File Offset: 0x000C079C
	protected override void OnResponse(string text)
	{
		base.OnResponse(text);
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
		if (!dictionary.ContainsKey("result") || (int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error " + dictionary["message"].ToString() + " Responce: " + text));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Save, Language.CWMainSave, Language.CWMainSaveFinishedDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Save Finished", Color.green);
		Main.UserInfo.settings.graphics.OnProfileChanged(true);
	}
}
