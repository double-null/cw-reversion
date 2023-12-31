using System;
using System.Collections.Generic;

// Token: 0x02000360 RID: 864
internal class WeaponModsStorage
{
	// Token: 0x17000828 RID: 2088
	// (get) Token: 0x06001C53 RID: 7251 RVA: 0x000FC28C File Offset: 0x000FA48C
	public Dictionary<int, WeaponModsInfo> Weapons
	{
		get
		{
			return this._weapons;
		}
	}

	// Token: 0x06001C54 RID: 7252 RVA: 0x000FC294 File Offset: 0x000FA494
	public static WeaponModsStorage Instance()
	{
		WeaponModsStorage result;
		if ((result = WeaponModsStorage._instance) == null)
		{
			result = (WeaponModsStorage._instance = new WeaponModsStorage());
		}
		return result;
	}

	// Token: 0x06001C55 RID: 7253 RVA: 0x000FC2B0 File Offset: 0x000FA4B0
	public void FillStorage(string json)
	{
		this._weapons = new Dictionary<int, WeaponModsInfo>();
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(json, string.Empty);
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary["data"];
		foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
		{
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)keyValuePair.Value;
			int key = (int)dictionary3["id"];
			this.Weapons.Add(key, new WeaponModsInfo(dictionary3));
		}
	}

	// Token: 0x06001C56 RID: 7254 RVA: 0x000FC368 File Offset: 0x000FA568
	public WeaponModsInfo GetModsByWeaponId(int id)
	{
		if (this.Weapons.ContainsKey(id))
		{
			return this.Weapons[id];
		}
		return null;
	}

	// Token: 0x04002134 RID: 8500
	private Dictionary<int, WeaponModsInfo> _weapons;

	// Token: 0x04002135 RID: 8501
	private static WeaponModsStorage _instance;
}
