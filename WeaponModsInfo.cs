using System;
using System.Collections.Generic;

// Token: 0x0200034A RID: 842
internal class WeaponModsInfo : Convertible
{
	// Token: 0x06001C13 RID: 7187 RVA: 0x000FAC24 File Offset: 0x000F8E24
	public WeaponModsInfo(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x06001C14 RID: 7188 RVA: 0x000FAC34 File Offset: 0x000F8E34
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "id", ref this.WeaponId, isWrite);
		JSON.ReadWrite(dict["isAvailable"] as Dictionary<string, object>, "optic", ref this.OpticAvailable, isWrite);
		JSON.ReadWrite(dict["isAvailable"] as Dictionary<string, object>, "tactical", ref this.TacticalAvailable, isWrite);
		JSON.ReadWrite(dict["isAvailable"] as Dictionary<string, object>, "silencer", ref this.SilencerAvailable, isWrite);
		JSON.ReadWrite(dict["isAvailable"] as Dictionary<string, object>, "ammo", ref this.AmmoTypeAvailable, isWrite);
		this.ParseMetaLevels(dict["meta"] as Dictionary<string, object>, this.WeaponId);
	}

	// Token: 0x06001C15 RID: 7189 RVA: 0x000FACF4 File Offset: 0x000F8EF4
	private void ParseMetaLevels(Dictionary<string, object> dict, int weaponId)
	{
		if (this.MetaLevels == null)
		{
			this.MetaLevels = new List<MasteringMetaLevel>();
		}
		else
		{
			this.MetaLevels.Clear();
		}
		int count = dict.Count;
		bool flag = false;
		for (int i = -1; i < count - 1; i++)
		{
			string key = i.ToString();
			if (!dict.ContainsKey(key) && i == 0)
			{
				this.MetaLevels.Add(new MasteringMetaLevel(new Dictionary<string, object>(), i, weaponId, true));
				flag = true;
			}
			else
			{
				this.MetaLevels.Add(new MasteringMetaLevel(dict[key] as Dictionary<string, object>, i, weaponId, false));
			}
		}
		if (flag)
		{
			int metaId = count - 1;
			string key2 = metaId.ToString();
			this.MetaLevels.Add(new MasteringMetaLevel(dict[key2] as Dictionary<string, object>, metaId, weaponId, false));
		}
	}

	// Token: 0x040020C4 RID: 8388
	public int WeaponId;

	// Token: 0x040020C5 RID: 8389
	public bool OpticAvailable;

	// Token: 0x040020C6 RID: 8390
	public bool TacticalAvailable;

	// Token: 0x040020C7 RID: 8391
	public bool SilencerAvailable;

	// Token: 0x040020C8 RID: 8392
	public bool AmmoTypeAvailable;

	// Token: 0x040020C9 RID: 8393
	public List<MasteringMetaLevel> MetaLevels;
}
