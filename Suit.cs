using System;
using System.Collections.Generic;

// Token: 0x02000352 RID: 850
internal class Suit
{
	// Token: 0x06001C42 RID: 7234 RVA: 0x000FBE38 File Offset: 0x000FA038
	public Suit(int id, Dictionary<string, object> suitDictionary)
	{
		this.Id = id;
		this.CurrentWeaponsMods = new Dictionary<int, WeaponMods>();
		foreach (KeyValuePair<string, object> keyValuePair in suitDictionary)
		{
			this.CurrentWeaponsMods.Add(int.Parse(keyValuePair.Key), new WeaponMods((Dictionary<string, object>)keyValuePair.Value));
		}
	}

	// Token: 0x04002107 RID: 8455
	public int Id;

	// Token: 0x04002108 RID: 8456
	public Dictionary<int, WeaponMods> CurrentWeaponsMods;
}
