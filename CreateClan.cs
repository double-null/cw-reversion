using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F9 RID: 505
internal class CreateClan : DatabaseEvent
{
	// Token: 0x06001074 RID: 4212 RVA: 0x000B9A2C File Offset: 0x000B7C2C
	public override void Initialize(params object[] args)
	{
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		string text2 = (string)Crypt.ResolveVariable(args, string.Empty, 1);
		string s = (string)Crypt.ResolveVariable(args, string.Empty, 2);
		string s2 = (string)Crypt.ResolveVariable(args, string.Empty, 3);
		string text3 = (string)Crypt.ResolveVariable(args, string.Empty, 4);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.CreateClan, Language.CreateClanProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Create clan", Color.grey);
		HtmlLayer.Request(string.Concat(new string[]
		{
			"clans.php?action=create_clan&clan_tag=",
			text,
			"&clan_color=",
			text2,
			"&clan_name=",
			WWW.EscapeURL(s),
			"&clan_type=",
			text3,
			"&clan_url=",
			WWW.EscapeURL(s2)
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001075 RID: 4213 RVA: 0x000B9B48 File Offset: 0x000B7D48
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
			if (Application.isEditor || Peer.Dedicated)
			{
				throw ex;
			}
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.FailedAction();
		}
		else
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CreateClan, Language.CreateClanComplete, PopupState.information, false, false, string.Empty, string.Empty));
			this.SuccessAction();
			global::Console.print("Create clan Finished", Color.green);
			if (dictionary.ContainsKey("new_cr"))
			{
				Main.UserInfo.CR = (int)dictionary["new_cr"];
			}
			if (dictionary.ContainsKey("new_gp"))
			{
				Main.UserInfo.GP = (int)dictionary["new_gp"];
			}
		}
	}

	// Token: 0x06001076 RID: 4214 RVA: 0x000B9CA0 File Offset: 0x000B7EA0
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.CWMainSkillUnlockError, Language.CWMainSkillUnlockErrorDesc, PopupState.information, true, false, string.Empty, string.Empty));
		this.FailedAction();
	}
}
