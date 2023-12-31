using System;
using System.Collections.Generic;

// Token: 0x0200033F RID: 831
internal class BuyMasteringMetaLevel : DatabaseEvent
{
	// Token: 0x06001BF4 RID: 7156 RVA: 0x000FA1CC File Offset: 0x000F83CC
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		this._metaLevel = (MasteringMetaLevel)Crypt.ResolveVariable(args, null, 1);
		string actions = string.Concat(new object[]
		{
			"adm.php?q=customization/player/meta/buy/",
			this._weaponId,
			"/",
			this._metaLevel.Id
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BF5 RID: 7157 RVA: 0x000FA26C File Offset: 0x000F846C
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
			Main.UserInfo.GP -= this._metaLevel.GpToUnlock;
			object obj;
			if (dictionary.TryGetValue(this._weaponId.ToString(), out obj))
			{
				Main.UserInfo.Mastering.WeaponsStats[this._weaponId].Initialize((Dictionary<string, object>)obj);
			}
			this.SuccessAction();
		}
	}

	// Token: 0x040020BA RID: 8378
	private int _weaponId = -1;

	// Token: 0x040020BB RID: 8379
	private MasteringMetaLevel _metaLevel;
}
