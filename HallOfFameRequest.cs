using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021D RID: 541
internal class HallOfFameRequest : DatabaseEvent
{
	// Token: 0x06001105 RID: 4357 RVA: 0x000BD948 File Offset: 0x000BBB48
	public override void Initialize(params object[] args)
	{
		this.hallOfFame = (HallOfFameGUI)Crypt.ResolveVariable(args, 1, 0);
		this.move = (int)Crypt.ResolveVariable(args, 0, 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRating, Language.CWMainLoadRatingDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("LoadRating", Color.grey);
		string empty = string.Empty;
		if (this.move == 1)
		{
			empty = this.aForvard;
		}
		if (this.move == 2)
		{
			empty = this.aBack;
		}
		HtmlLayer.Request("?action=hall_of_fame_unit" + empty, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001106 RID: 4358 RVA: 0x000BDA1C File Offset: 0x000BBC1C
	protected override void OnResponse(string text)
	{
		Debug.Log(text);
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Main.UserInfo.Permission >= EPermission.Admin)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if (!dictionary.ContainsKey("year") || !dictionary.ContainsKey("unit") || !dictionary.ContainsKey("month"))
		{
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRating, Language.CWMainLoadRatingFinishedDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("LoadRating Finished", Color.green);
		int y = (int)dictionary["year"];
		int m = (int)dictionary["month"];
		bool lA = (bool)dictionary["leftArrow"];
		bool rA = (bool)dictionary["rightArrow"];
		if (this.hallOfFame != null)
		{
			this.hallOfFame.Parse((Dictionary<string, object>[])dictionary["unit"], y, m, lA, rA);
		}
	}

	// Token: 0x06001107 RID: 4359 RVA: 0x000BDB8C File Offset: 0x000BBD8C
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRatingError, Language.CWMainLoadRatingErrorDesc, PopupState.information, true, true, string.Empty, string.Empty));
		global::Console.print(base.GetType().ToString() + " Error, Reload", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x040010E1 RID: 4321
	private HallOfFameGUI hallOfFame;

	// Token: 0x040010E2 RID: 4322
	private int move;

	// Token: 0x040010E3 RID: 4323
	private string aForvard = "&show=left";

	// Token: 0x040010E4 RID: 4324
	private string aBack = "&show=right";
}
