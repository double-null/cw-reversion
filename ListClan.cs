using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FB RID: 507
internal class ListClan : DatabaseEvent
{
	// Token: 0x0600107C RID: 4220 RVA: 0x000B9E04 File Offset: 0x000B8004
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Clan list loading", Color.grey);
		string text = "clans.php?action=list_clans";
		if (args.Length > 0 && args[0] is string)
		{
			text = text + "&searchTerm=" + args[0];
		}
		HtmlLayer.Request(text, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600107D RID: 4221 RVA: 0x000B9EA0 File Offset: 0x000B80A0
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
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		Dictionary<string, object>[] array = dictionary["list"] as Dictionary<string, object>[];
		if (array == null)
		{
			array = new Dictionary<string, object>[0];
		}
		Main.UserInfo.clanData.ParseClanInfo(array);
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingFin, PopupState.information, false, false, string.Empty, string.Empty));
		this.SuccessAction();
	}

	// Token: 0x0600107E RID: 4222 RVA: 0x000B9FBC File Offset: 0x000B81BC
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingErrDesc, PopupState.information, false, true, string.Empty, string.Empty));
		this.FailedAction();
	}
}
