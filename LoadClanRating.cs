using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000220 RID: 544
[AddComponentMenu("Scripts/Game/Events/LoadClanRating")]
internal class LoadClanRating : DatabaseEvent
{
	// Token: 0x06001112 RID: 4370 RVA: 0x000BE664 File Offset: 0x000BC864
	public override void Initialize(params object[] args)
	{
		if (this.updateRatingTime > Time.realtimeSinceStartup)
		{
			return;
		}
		this.updateRatingTime = Time.realtimeSinceStartup + 60f;
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRating, Language.CWMainLoadRatingDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("LoadRating", Color.grey);
		HtmlLayer.Request("?action=getClanRating", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001113 RID: 4371 RVA: 0x000BE6F8 File Offset: 0x000BC8F8
	protected override void OnResponse(string text)
	{
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
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRating, Language.CWMainLoadRatingFinishedDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("LoadRating Finished", Color.green);
		if (!dictionary.ContainsKey("data"))
		{
			Main.ClanRatingTable = new ClanRating[0];
			return;
		}
		Main.ClanRatingTable = new ClanRating[((Dictionary<string, object>[])dictionary["data"]).Length];
		int num = 0;
		foreach (Dictionary<string, object> dict in (Dictionary<string, object>[])dictionary["data"])
		{
			Main.ClanRatingTable[num] = new ClanRating();
			Main.ClanRatingTable[num].Read(dict);
			num++;
		}
	}

	// Token: 0x06001114 RID: 4372 RVA: 0x000BE858 File Offset: 0x000BCA58
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRatingError, Language.CWMainLoadRatingErrorDesc, PopupState.information, true, true, string.Empty, string.Empty));
		global::Console.print(base.GetType().ToString() + " Error, Reload", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x040010E9 RID: 4329
	private float updateRatingTime;
}
