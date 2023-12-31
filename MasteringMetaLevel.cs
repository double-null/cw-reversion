using System;
using System.Collections.Generic;

// Token: 0x0200034B RID: 843
internal class MasteringMetaLevel
{
	// Token: 0x06001C16 RID: 7190 RVA: 0x000FADD4 File Offset: 0x000F8FD4
	public MasteringMetaLevel(Dictionary<string, object> dict, int metaId, int weaponId, bool hidden = false)
	{
		this.Id = metaId;
		this.Hidden = hidden;
		this.Convert(dict, false, this.Id, weaponId);
	}

	// Token: 0x06001C17 RID: 7191 RVA: 0x000FAE08 File Offset: 0x000F9008
	public void Convert(Dictionary<string, object> dict, bool isWrite, int metaId, int weaponId)
	{
		JSON.ReadWrite(dict, "exp", ref this.ExpToUnlock, isWrite);
		JSON.ReadWrite(dict, "gp", ref this.GpToUnlock, isWrite);
		if (dict.ContainsKey("mods"))
		{
			this.ParseMods(dict["mods"] as Dictionary<string, object>, metaId, weaponId);
		}
	}

	// Token: 0x06001C18 RID: 7192 RVA: 0x000FAE64 File Offset: 0x000F9064
	private void ParseMods(Dictionary<string, object> dict, int metaId, int weaponId)
	{
		if (this.Mods == null)
		{
			this.Mods = new List<MasteringMod>();
		}
		else
		{
			this.Mods.Clear();
		}
		for (int i = 1; i < 5; i++)
		{
			string key = i.ToString();
			if (dict.ContainsKey(key))
			{
				Dictionary<string, object> dictionary = dict[key] as Dictionary<string, object>;
				if (dictionary != null)
				{
					int id = int.Parse(dictionary["mod"].ToString());
					MasteringMod modById = ModsStorage.Instance().GetModById(id);
					if (!modById.WeaponSpecificInfo.ContainsKey(weaponId))
					{
						modById.WeaponSpecificInfo.Add(weaponId, new WeaponSpecificModInfo(dictionary, metaId, i));
					}
					this.Mods.Add(modById);
				}
			}
			else
			{
				this.Mods.Add(null);
			}
		}
	}

	// Token: 0x040020CA RID: 8394
	public int Id;

	// Token: 0x040020CB RID: 8395
	public int ExpToUnlock;

	// Token: 0x040020CC RID: 8396
	public int GpToUnlock;

	// Token: 0x040020CD RID: 8397
	public bool Hidden;

	// Token: 0x040020CE RID: 8398
	public List<MasteringMod> Mods;
}
