using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001FD RID: 509
internal class LoadDetailInfo : DatabaseEvent
{
	// Token: 0x06001084 RID: 4228 RVA: 0x000BA2C8 File Offset: 0x000B84C8
	public override void Initialize(params object[] args)
	{
		this.shotrclaninfo = (ShortClanInfo)args[0];
		if (this.shotrclaninfo == null)
		{
			return;
		}
		this.downloadingInfo = this.shotrclaninfo.DetailInfo;
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanDetailLoading, Language.ClanDetailLoadingDesc, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("Load Detail Clan Info", Color.grey);
		HtmlLayer.Request("clans.php?action=clan_info&clan_id=" + this.shotrclaninfo.clanID, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001085 RID: 4229 RVA: 0x000BA37C File Offset: 0x000B857C
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
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ClanDetailLoading, Language.ClanDetailLoadingFin, PopupState.progress, false, false, string.Empty, string.Empty));
			this.FailedAction();
		}
		else
		{
			this.downloadingInfo.Convert((Dictionary<string, object>)dictionary["claninfo"], false);
			try
			{
				this.downloadingInfo.ParseClanMemberList((Dictionary<string, object>[])dictionary["clan_userlist"]);
			}
			catch (Exception ex2)
			{
				this.downloadingInfo.MemberList = new ClanMemberInfo[0];
			}
			try
			{
				this.downloadingInfo.ParseRequestList((Dictionary<string, object>[])dictionary["clan_requests"]);
			}
			catch (Exception ex3)
			{
				this.downloadingInfo.RequestList = new ClanRequestInfo[0];
			}
			EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.ClanDetailLoading, Language.ClanDetailLoadingFin, PopupState.progress, false, false, string.Empty, string.Empty));
			this.SuccessAction();
		}
	}

	// Token: 0x06001086 RID: 4230 RVA: 0x000BA53C File Offset: 0x000B873C
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.ClanDetailLoading, Language.ClanDetailLoadingErrDesc, PopupState.information, false, true, string.Empty, string.Empty));
		this.FailedAction();
	}

	// Token: 0x040010D8 RID: 4312
	private DetailClanInfo downloadingInfo;

	// Token: 0x040010D9 RID: 4313
	private ShortClanInfo shotrclaninfo;
}
