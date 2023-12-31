using System;
using System.Collections.Generic;

// Token: 0x02000346 RID: 838
internal class MasteringSetWtaskInfo : DatabaseEvent
{
	// Token: 0x06001C08 RID: 7176 RVA: 0x000FA910 File Offset: 0x000F8B10
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		string actions = "adm.php?q=customization/player/set_wtask_info/" + this._weaponId;
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x000FA978 File Offset: 0x000F8B78
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		if (dictionary.ContainsKey("result") && (int)dictionary["result"] == 0)
		{
			Main.UserInfo.Mastering.WeaponsStats[this._weaponId].UnlockWtaskMeta();
			this.SuccessAction();
			return;
		}
		throw new Exception("Bad result");
	}

	// Token: 0x040020C3 RID: 8387
	private int _weaponId;
}
