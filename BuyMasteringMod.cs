using System;
using System.Collections.Generic;

// Token: 0x02000342 RID: 834
internal class BuyMasteringMod : DatabaseEvent
{
	// Token: 0x06001BFD RID: 7165 RVA: 0x000FA510 File Offset: 0x000F8710
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		this._mod = (MasteringMod)Crypt.ResolveVariable(args, null, 1);
		int num = (int)Crypt.ResolveVariable(args, 0, 2);
		int num2 = (int)Crypt.ResolveVariable(args, 0, 3);
		string actions = string.Concat(new object[]
		{
			"adm.php?q=customization/player/mod/buy/",
			this._weaponId,
			"/",
			num,
			"/",
			num2
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BFE RID: 7166 RVA: 0x000FA5DC File Offset: 0x000F87DC
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		if (dictionary.ContainsKey("result"))
		{
			throw new Exception("Bad result");
		}
		if (dictionary.ContainsKey(this._weaponId.ToString()))
		{
			Main.UserInfo.Mastering.MasteringPoints -= this._mod.WeaponSpecificInfo[this._weaponId].Mp;
			object obj;
			if (dictionary.TryGetValue(this._weaponId.ToString(), out obj))
			{
				Main.UserInfo.Mastering.WeaponsStats[this._weaponId].Initialize((Dictionary<string, object>)obj);
			}
			this.SuccessAction();
		}
	}

	// Token: 0x040020BD RID: 8381
	private int _weaponId = -1;

	// Token: 0x040020BE RID: 8382
	private MasteringMod _mod;
}
