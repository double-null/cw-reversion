using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000205 RID: 517
internal class SaveRatio : DatabaseEvent
{
	// Token: 0x060010A4 RID: 4260 RVA: 0x000BB250 File Offset: 0x000B9450
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, "0", 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.CWMainSave, Language.CWMainSaveDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Save ratio", Color.grey);
		HtmlLayer.Request("clans.php?action=save_ratio&ratio=" + str, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x000BB2DC File Offset: 0x000B94DC
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
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CWMainSave, Language.CWMainSaveFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Create clan Finished", Color.green);
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x000BB3B8 File Offset: 0x000B95B8
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.UnlockSkill, Language.CWMainSaveError, Language.CWMainSaveErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		base.OnFail(e);
	}
}
