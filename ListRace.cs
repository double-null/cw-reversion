using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FC RID: 508
internal class ListRace : DatabaseEvent
{
	// Token: 0x06001080 RID: 4224 RVA: 0x000BA004 File Offset: 0x000B8204
	public override void Initialize(params object[] args)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Create race list", Color.grey);
		string text = "clans.php?action=list_clans";
		if (args.Length > 0 && args[0] is string)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				"&searchTerm=",
				args[0],
				"&topType=",
				args[1]
			});
		}
		HtmlLayer.Request(text, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001081 RID: 4225 RVA: 0x000BA0BC File Offset: 0x000B82BC
	protected override void OnResponse(string text, string url)
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
		try
		{
			Dictionary<string, object>[] array = dictionary["list"] as Dictionary<string, object>[];
			if (array == null)
			{
				array = new Dictionary<string, object>[0];
			}
			Main.UserInfo.clanData.ParseClanInfo(array);
			Main.UserInfo.clanData.ongoing_race = (int)((Dictionary<string, object>)dictionary["race_info"])["ongoing_race"];
			Main.UserInfo.clanData.race_end = (string)((Dictionary<string, object>)dictionary["race_info"])["race_ends"];
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingFin, PopupState.information, false, false, string.Empty, string.Empty));
		}
		catch (Exception ex2)
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingFin, PopupState.information, false, false, string.Empty, string.Empty));
			throw ex2;
		}
		this.SuccessAction();
	}

	// Token: 0x06001082 RID: 4226 RVA: 0x000BA280 File Offset: 0x000B8480
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanListLoading, Language.ClanListLoadingErrDesc, PopupState.information, false, true, string.Empty, string.Empty));
		this.FailedAction();
	}
}
