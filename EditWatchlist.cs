using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200023F RID: 575
internal class EditWatchlist : DatabaseEvent
{
	// Token: 0x060011A3 RID: 4515 RVA: 0x000C3F38 File Offset: 0x000C2138
	public override void Initialize(params object[] args)
	{
		string str = Crypt.ResolveVariable(args, "0", 0).ToString();
		bool flag = Convert.ToBoolean(Crypt.ResolveVariable(args, "0", 1));
		string text = ((!flag) ? "?action=wl_add" : "?action=wl_remove") + "&user_id=" + str;
		HtmlLayer.Request(text, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
		Debug.Log(text);
	}

	// Token: 0x060011A4 RID: 4516 RVA: 0x000C3FBC File Offset: 0x000C21BC
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text) || text == "NULL")
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		object obj;
		dictionary.TryGetValue("result", out obj);
		if (Convert.ToInt32(obj) == 0)
		{
			this.SuccessAction();
		}
		else if (Convert.ToInt32(obj) == 1002)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Favorite, Language.AddToFavorites, "1002", delegate()
			{
			}, PopupState.favorite, false, true, new object[0]));
		}
		else if (Convert.ToInt32(obj) == 1001)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Favorite, Language.AddToFavorites, "1001", delegate()
			{
			}, PopupState.favorite, false, true, new object[0]));
		}
		else
		{
			this.FailedAction();
			if (obj != null)
			{
				throw new Exception(obj.ToString());
			}
		}
	}

	// Token: 0x060011A5 RID: 4517 RVA: 0x000C40F0 File Offset: 0x000C22F0
	protected override void OnFail(Exception e)
	{
		this.FailedAction();
	}
}
