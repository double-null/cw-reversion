using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000223 RID: 547
[AddComponentMenu("Scripts/Game/Events/LoadRating")]
internal class LoadRating : DatabaseEvent
{
	// Token: 0x06001127 RID: 4391 RVA: 0x000BFF14 File Offset: 0x000BE114
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 1, 0);
		string text = (string)Crypt.ResolveVariable(args, string.Empty, 1);
		bool flag = (bool)Crypt.ResolveVariable(args, false, 2);
		bool flag2 = (bool)Crypt.ResolveVariable(args, false, 3);
		bool flag3 = (bool)Crypt.ResolveVariable(args, false, 4);
		bool flag4 = (bool)Crypt.ResolveVariable(args, false, 5);
		CarrierGUI carrierGUI = Forms.Get(typeof(CarrierGUI)) as CarrierGUI;
		carrierGUI.ratingLoading = true;
		carrierGUI.ratingLoaded = false;
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRating, Language.CWMainLoadRatingDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("LoadRating", Color.grey);
		string text2 = (!flag4) ? "getrating" : "getSeasonRating";
		HtmlLayer.Request(string.Concat(new object[]
		{
			"?action=",
			text2,
			"&type=",
			num,
			(!(text != string.Empty)) ? string.Empty : ("&search=" + WWW.EscapeURL(text)),
			(!flag) ? string.Empty : "&friends=1",
			(!flag2) ? string.Empty : "&online=1",
			(!flag3) ? string.Empty : "&hc=1"
		}), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001128 RID: 4392 RVA: 0x000C00D4 File Offset: 0x000BE2D4
	protected override void OnResponse(string text)
	{
		CarrierGUI carrierGUI = Forms.Get(typeof(CarrierGUI)) as CarrierGUI;
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
		if (!dictionary.ContainsKey("data") || dictionary["data"] == null)
		{
			Main.RatingTable = new Rating[0];
			return;
		}
		if (((object[])dictionary["data"]).Length == 0)
		{
			Main.RatingTable = new Rating[0];
		}
		else
		{
			Main.RatingTable = new Rating[((Dictionary<string, object>[])dictionary["data"]).Length];
			int num = 0;
			foreach (Dictionary<string, object> dict in (Dictionary<string, object>[])dictionary["data"])
			{
				Main.RatingTable[num] = new Rating();
				Main.RatingTable[num].Read(dict);
				if (Main.RatingTable[num].userID == Main.UserInfo.userID)
				{
					Main.UserInfo.ratingPlace = Main.RatingTable[num].place;
					carrierGUI.ratingPlaceCached = Main.RatingTable[num].place;
				}
				num++;
			}
		}
		carrierGUI.ratingLoading = false;
		carrierGUI.ratingLoaded = true;
		this.SuccessAction();
	}

	// Token: 0x06001129 RID: 4393 RVA: 0x000C02F0 File Offset: 0x000BE4F0
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.CWMainLoadRatingError, Language.CWMainLoadRatingErrorDesc, PopupState.information, true, true, string.Empty, string.Empty));
		global::Console.print(base.GetType().ToString() + " Error, Reload", Color.red);
		global::Console.exception(e);
		this.FailedAction();
	}
}
