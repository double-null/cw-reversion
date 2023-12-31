using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000341 RID: 833
internal class BuyMasteringWtask : DatabaseEvent
{
	// Token: 0x06001BFA RID: 7162 RVA: 0x000FA3C8 File Offset: 0x000F85C8
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		string actions = "adm.php?q=customization/player/wtask/unlock/" + this._weaponId;
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BFB RID: 7163 RVA: 0x000FA430 File Offset: 0x000F8630
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Debug.Log(text);
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		if (dictionary.ContainsKey("result") && (int)dictionary["result"] != 0)
		{
			throw new Exception("Bad result: " + dictionary["result"]);
		}
		if (dictionary.ContainsKey(this._weaponId.ToString()))
		{
			Dictionary<string, object> dict = (Dictionary<string, object>)dictionary[this._weaponId.ToString()];
			Main.UserInfo.Mastering.WeaponsStats[this._weaponId].Initialize(dict);
			this.SuccessAction();
		}
	}

	// Token: 0x040020BC RID: 8380
	private int _weaponId;
}
