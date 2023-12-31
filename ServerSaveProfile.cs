using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000235 RID: 565
[AddComponentMenu("Scripts/Game/Events/ServerSaveProfile")]
internal class ServerSaveProfile : DatabaseEvent
{
	// Token: 0x06001184 RID: 4484 RVA: 0x000C2ED4 File Offset: 0x000C10D4
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Save, Language.CWMainSave, Language.CWMainSaveDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Save", Color.grey);
		Debug.Log("SERVER SAVE");
		string data = Main.UserInfo.ToJSON(true);
		string text = "?action=idsave&user_id=" + Main.UserInfo.userID;
		if (CVars.IsStandaloneRealm)
		{
			text += "&platform=standalone";
		}
		HtmlLayer.SendCompressed(text, data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
	}

	// Token: 0x06001185 RID: 4485 RVA: 0x000C2F7C File Offset: 0x000C117C
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
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error " + dictionary["message"].ToString()));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Save, Language.CWMainSave, Language.CWMainSaveFinishedDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		if (CVars.n_httpDebug)
		{
			global::Console.print("Save Finished", Color.green);
		}
		Main.UserInfo.settings.graphics.OnProfileChanged(false);
	}
}
