using System;
using System.Collections.Generic;
using Assets.Scripts.Camouflage;

// Token: 0x0200035F RID: 863
internal class ModsStorage
{
	// Token: 0x17000826 RID: 2086
	// (get) Token: 0x06001C4C RID: 7244 RVA: 0x000FC050 File Offset: 0x000FA250
	public Dictionary<int, MasteringMod> Mods
	{
		get
		{
			return this._mods;
		}
	}

	// Token: 0x17000827 RID: 2087
	// (get) Token: 0x06001C4D RID: 7245 RVA: 0x000FC058 File Offset: 0x000FA258
	public Dictionary<int, CamouflageInfo> CharacterCamouflages
	{
		get
		{
			return this._characterCamouflages;
		}
	}

	// Token: 0x06001C4E RID: 7246 RVA: 0x000FC060 File Offset: 0x000FA260
	public static ModsStorage Instance()
	{
		if (ModsStorage._instance == null)
		{
			ModsStorage._instance = new ModsStorage();
		}
		return ModsStorage._instance;
	}

	// Token: 0x06001C4F RID: 7247 RVA: 0x000FC07C File Offset: 0x000FA27C
	public void FillStorage(string json)
	{
		if (this._mods == null)
		{
			this._mods = new Dictionary<int, MasteringMod>();
		}
		if (this._characterCamouflages == null)
		{
			this._characterCamouflages = new Dictionary<int, CamouflageInfo>();
			this._characterCamouflages.Add(0, new CamouflageInfo());
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(json, string.Empty);
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary["data"];
		foreach (KeyValuePair<string, object> keyValuePair in dictionary2)
		{
			Dictionary<string, object> dictionary3 = (Dictionary<string, object>)keyValuePair.Value;
			int key = (int)dictionary3["id"];
			if (!this.Mods.ContainsKey(key))
			{
				this.Mods.Add(key, new MasteringMod(dictionary3));
			}
			if (dictionary3.ContainsKey("for_character") && !this.CharacterCamouflages.ContainsKey(key))
			{
				this.CharacterCamouflages.Add(key, new CamouflageInfo(dictionary3));
			}
		}
		if (!Peer.Dedicated)
		{
			ModIconsDownloader.Instance.LoadModIcons();
		}
	}

	// Token: 0x06001C50 RID: 7248 RVA: 0x000FC1C4 File Offset: 0x000FA3C4
	public MasteringMod GetModById(int id)
	{
		if (this.Mods != null && this.Mods.ContainsKey(id))
		{
			return this.Mods[id];
		}
		return null;
	}

	// Token: 0x06001C51 RID: 7249 RVA: 0x000FC1FC File Offset: 0x000FA3FC
	public MasteringMod GetModByName(string name)
	{
		foreach (KeyValuePair<int, MasteringMod> keyValuePair in this.Mods)
		{
			if (keyValuePair.Value.EngShortName == name)
			{
				return keyValuePair.Value;
			}
		}
		return null;
	}

	// Token: 0x04002131 RID: 8497
	private Dictionary<int, MasteringMod> _mods;

	// Token: 0x04002132 RID: 8498
	private Dictionary<int, CamouflageInfo> _characterCamouflages;

	// Token: 0x04002133 RID: 8499
	private static ModsStorage _instance;
}
