using System;
using System.Collections.Generic;

// Token: 0x02000344 RID: 836
internal class MasteringSetWeaponInfo : DatabaseEvent
{
	// Token: 0x06001C03 RID: 7171 RVA: 0x000FA738 File Offset: 0x000F8938
	public override void Initialize(params object[] args)
	{
		this._weaponId = (int)Crypt.ResolveVariable(args, -1, 0);
		string actions = "adm.php?q=customization/player/set_weapon_info/" + this._weaponId;
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000FA7A0 File Offset: 0x000F89A0
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		if (!dictionary.ContainsKey(this._weaponId.ToString()))
		{
			throw new Exception("Data not contain weapon id key");
		}
		Main.UserInfo.Mastering.WeaponsStats.Add(this._weaponId, new MasteringInfo.MasteringWeaponStats((Dictionary<string, object>)dictionary[this._weaponId.ToString()]));
		this.SuccessAction();
	}

	// Token: 0x040020BF RID: 8383
	private int _weaponId;
}
