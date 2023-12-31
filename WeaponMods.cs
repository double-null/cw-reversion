using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000353 RID: 851
internal class WeaponMods
{
	// Token: 0x06001C43 RID: 7235 RVA: 0x000FBED4 File Offset: 0x000FA0D4
	public WeaponMods(Dictionary<string, object> mods)
	{
		this.Mods = new Dictionary<ModType, int>();
		foreach (KeyValuePair<string, object> keyValuePair in mods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(int.Parse(keyValuePair.Value.ToString()));
			if (modById != null)
			{
				if (this.Mods.ContainsKey(modById.Type))
				{
					Debug.LogError(modById.EngShortName);
				}
				else
				{
					this.Mods.Add(modById.Type, modById.Id);
				}
			}
		}
		if (!this.Mods.ContainsKey(ModType.camo))
		{
			this.Mods.Add(ModType.camo, MasteringInfo.BaseCamoIndex);
		}
	}

	// Token: 0x04002109 RID: 8457
	public Dictionary<ModType, int> Mods;

	// Token: 0x0400210A RID: 8458
	public Dictionary<ModType, int> TemporaryMods;
}
