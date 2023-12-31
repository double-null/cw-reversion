using System;
using System.Collections;
using System.Collections.Generic;
using PrefabDownloadNamespace;
using UnityEngine;

// Token: 0x020000B1 RID: 177
[AddComponentMenu("Scripts/Engine/Loader")]
internal class Loader : SingletoneForm<Loader>
{
	// Token: 0x0600044F RID: 1103 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
	private Downloader FindDownloader(string url)
	{
		for (int i = 0; i < this._downloaders.Count; i++)
		{
			if (this._downloaders[i].Url == url)
			{
				return this._downloaders[i];
			}
		}
		return null;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001CF44 File Offset: 0x0001B144
	private StateDownloader Find(string stateName)
	{
		for (int i = 0; i < this._states.Count; i++)
		{
			if (this._states[i].Name == stateName)
			{
				return this._states[i];
			}
		}
		return null;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001CF98 File Offset: 0x0001B198
	private void Init(string stateName, StateDownloaderFinishedCallback finish, StateDownloaderFinishedCallback callback = null)
	{
		StateDownloader stateDownloader = this.Find(stateName);
		if (stateDownloader != null)
		{
			return;
		}
		StateDownloader stateDownloader2 = new StateDownloader();
		stateDownloader2.Name = stateName;
		stateDownloader2.Init(finish, callback);
		SingletoneForm<Loader>.Instance._states.Add(stateDownloader2);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001CFDC File Offset: 0x0001B1DC
	private void Download(string stateName, string Url, bool repeatAtFail = true, int maxTryCount = 100)
	{
		this.Find(stateName).Download(Url);
		for (int i = 0; i < SingletoneForm<Loader>.Instance._downloaders.Count; i++)
		{
			if (SingletoneForm<Loader>.Instance._downloaders[i].Url == Url)
			{
				return;
			}
		}
		Downloader downloader = SingletoneForm<Loader>.Instance.gameObject.AddComponent<Downloader>();
		downloader.Download(Url, new DownloaderFinishedDownload(this.OnLoadingFinished), repeatAtFail, maxTryCount);
		this._downloaders.Add(downloader);
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001D06C File Offset: 0x0001B26C
	private void OnLoadingFinished(object downloader)
	{
		PrefabFactory.AddHolder((Downloader)downloader);
		this.CheckIfAllLoaded();
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001D080 File Offset: 0x0001B280
	private void CheckIfAllLoaded()
	{
		for (int i = 0; i < this._states.Count; i++)
		{
			if (this._states[i].IsFinished(this._downloaders))
			{
				this._states[i].Finish(this._downloaders);
				this._states.RemoveAt(i);
				return;
			}
		}
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
	public static List<Downloader> GetStateDownloaders(string stateName)
	{
		return SingletoneForm<Loader>.instance.Find(stateName).GetDownloaders(SingletoneForm<Loader>.instance._downloaders);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001D10C File Offset: 0x0001B30C
	public static float Progress(string stateName)
	{
		StateDownloader stateDownloader = SingletoneForm<Loader>.Instance.Find(stateName);
		return (stateDownloader != null) ? stateDownloader.Progress(SingletoneForm<Loader>.Instance._downloaders) : 1f;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001D148 File Offset: 0x0001B348
	public static void Cancel(string stateName)
	{
		StateDownloader stateDownloader = SingletoneForm<Loader>.Instance.Find(stateName);
		if (stateDownloader == null)
		{
			return;
		}
		Debug.Log("Cancel " + stateName);
		SingletoneForm<Loader>.Instance._states.Remove(stateDownloader);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001D18C File Offset: 0x0001B38C
	public static void Unload(bool unload = true)
	{
		for (int i = 0; i < SingletoneForm<Loader>.Instance._downloaders.Count; i++)
		{
			if (unload)
			{
				SingletoneForm<Loader>.Instance._downloaders[i].Unload();
			}
			UnityEngine.Object.DestroyObject(SingletoneForm<Loader>.Instance._downloaders[i]);
		}
		SingletoneForm<Loader>.Instance._downloaders.Clear();
		SingletoneForm<Loader>.Instance._states.Clear();
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0001D208 File Offset: 0x0001B408
	public void DownloadAllWeapons()
	{
		string stateName = "DownLoadAllWeaponsState";
		SingletoneForm<Loader>.Instance.Init(stateName, delegate
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadWeapons, PopupState.precentageWeaponsProgress, false, true, string.Empty, string.Empty));
		}, null);
		for (int i = 0; i < 101; i++)
		{
			SingletoneForm<Loader>.Instance.Download(stateName, WWWUtil.lodsUrl(EnumNames.Weapons(i)), true, 100);
			SingletoneForm<Loader>.Instance.Download(stateName, WWWUtil.weaponsUrl(EnumNames.Weapons(i)), true, 100);
			SingletoneForm<Loader>.Instance.Download(stateName, WWWUtil.handsUrl(EnumNames.Weapons(i)), true, 100);
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001D2AC File Offset: 0x0001B4AC
	public void DownloadAllMaps()
	{
		string stateName = "DownLoadAllMapsState";
		SingletoneForm<Loader>.Instance.Init(stateName, delegate
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadMaps, PopupState.precentageMapsProgress, false, true, string.Empty, string.Empty));
		}, null);
		for (int i = 0; i < Globals.I.maps.Length; i++)
		{
			SingletoneForm<Loader>.Instance.Download(stateName, WWWUtil.levelsUrl(Globals.I.maps[i].Name), true, 100);
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
	}

	// Token: 0x1700009C RID: 156
	// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001D334 File Offset: 0x0001B534
	public List<Downloader> WeaponsLoaders
	{
		get
		{
			List<Downloader> result;
			try
			{
				result = Loader.GetStateDownloaders("DownLoadAllWeaponsState");
			}
			catch (Exception)
			{
				result = new List<Downloader>();
			}
			return result;
		}
	}

	// Token: 0x1700009D RID: 157
	// (get) Token: 0x0600045C RID: 1116 RVA: 0x0001D384 File Offset: 0x0001B584
	public List<Downloader> MapsLoaders
	{
		get
		{
			List<Downloader> result;
			try
			{
				result = Loader.GetStateDownloaders("DownLoadAllMapsState");
			}
			catch (Exception)
			{
				result = new List<Downloader>();
			}
			return result;
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
	public static void DownloadGameData(LoaderSettings loaderSettings, string mapName, SuitInfo suit)
	{
		string text = EnumNames.Weapons((Weapons)((suit == null) ? 127 : suit.secondaryIndex));
		string text2 = EnumNames.Weapons((Weapons)((suit == null) ? 127 : suit.primaryIndex));
		SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked = false;
		SingletoneForm<Loader>.Instance.IsGameLoaded = false;
		SingletoneForm<Loader>.Instance.CanBeCanceled = true;
		SingletoneForm<Loader>.Instance._states.Clear();
		global::Console.print("Download GameData", Color.grey);
		SingletoneForm<Loader>.Instance.Init("GameData", new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.GameDataDownloaded), null);
		if (!Peer.Dedicated)
		{
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.lodsUrl(Weapons.kac_pdw.ToString()), true, 100);
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.lodsUrl(Weapons.pm.ToString()), true, 100);
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.handsUrl(EnumNames.Weapons(Weapons.pm)), true, 100);
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.weaponsUrl(EnumNames.Weapons(Weapons.pm)), true, 100);
			Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[MainGUI.Instance.CurrentSuitIndex].CurrentWeaponsMods;
			if (text != EnumNames.Weapons(Weapons.none))
			{
				SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.weaponsUrl(text), true, 100);
				SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.handsUrl(text), true, 100);
				if (Utility.IsModableWeapon(suit.secondaryIndex))
				{
					Dictionary<ModType, int> mods = currentWeaponsMods[suit.secondaryIndex].Mods;
					Loader.DownloadMods(mods, "GameData", false);
				}
			}
			if (text2 != EnumNames.Weapons(127))
			{
				SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.weaponsUrl(text2), true, 100);
				SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.handsUrl(text2), true, 100);
				if (Utility.IsModableWeapon(suit.primaryIndex))
				{
					Dictionary<ModType, int> mods2 = currentWeaponsMods[suit.primaryIndex].Mods;
					Loader.DownloadMods(mods2, "GameData", false);
				}
			}
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl("preweapon"), true, 100);
			if (CVars.EncryptContent)
			{
				if (CVars.UseUnityCache)
				{
					SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl("_cache_add"), true, 100);
				}
				else
				{
					SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl("_newWWW_add"), true, 100);
				}
			}
			else
			{
				SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl("add"), true, 100);
			}
		}
		if (Peer.IsHost)
		{
			SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl("server_add"), true, 100);
		}
		SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl(Main.HostInfo.MapName + "_add"), false, 100);
		SingletoneForm<Loader>.Instance.Download("GameData", WWWUtil.levelsUrl(mapName), true, 100);
		if (!Peer.Dedicated)
		{
			SingletoneForm<Loader>.Instance.DownloadersForLoadingGui = Loader.GetStateDownloaders("GameData");
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001D71C File Offset: 0x0001B91C
	public static void DownloadAdditionalGameData(SuitInfo suit, bool whileGameLoading = false)
	{
		string text = EnumNames.Weapons(suit.secondaryIndex);
		string text2 = EnumNames.Weapons(suit.primaryIndex);
		Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[MainGUI.Instance.CurrentSuitIndex].CurrentWeaponsMods;
		SingletoneForm<Loader>.Instance._states.Clear();
		global::Console.print("Download AdditionalWeapon", Color.grey);
		if (whileGameLoading)
		{
			SingletoneForm<Loader>.Instance.Init("AdditionalWeapon", new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.AdditionalWeaponDownloadedWhileGame), null);
		}
		else
		{
			SingletoneForm<Loader>.Instance.Init("AdditionalWeapon", new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.AdditionalWeaponDownloaded), null);
		}
		if (text != EnumNames.Weapons(127))
		{
			SingletoneForm<Loader>.Instance.Download("AdditionalWeapon", WWWUtil.handsUrl(text), true, 100);
			SingletoneForm<Loader>.Instance.Download("AdditionalWeapon", WWWUtil.weaponsUrl(text), true, 100);
			if (Utility.IsModableWeapon(suit.secondaryIndex))
			{
				Dictionary<ModType, int> mods = currentWeaponsMods[suit.secondaryIndex].Mods;
				Loader.DownloadMods(mods, "AdditionalWeapon", false);
			}
		}
		if (text2 != EnumNames.Weapons(127))
		{
			SingletoneForm<Loader>.Instance.Download("AdditionalWeapon", WWWUtil.weaponsUrl(text2), true, 100);
			SingletoneForm<Loader>.Instance.Download("AdditionalWeapon", WWWUtil.handsUrl(text2), true, 100);
			if (Utility.IsModableWeapon(suit.primaryIndex))
			{
				Dictionary<ModType, int> mods2 = currentWeaponsMods[suit.primaryIndex].Mods;
				Loader.DownloadMods(mods2, "AdditionalWeapon", false);
			}
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001D8B8 File Offset: 0x0001BAB8
	public static void DownloadMods(Dictionary<ModType, int> mods, string stateName, bool loadLod = false)
	{
		foreach (KeyValuePair<ModType, int> keyValuePair in mods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (modById != null && !modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
			{
				SingletoneForm<Loader>.Instance.Download(stateName, (!loadLod) ? WWWUtil.ModsUrl(modById) : WWWUtil.ModsLodUrl(modById), true, 100);
			}
		}
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0001D978 File Offset: 0x0001BB78
	public static void DownloadModLod(MasteringMod mod, StateDownloaderFinishedCallback callback = null)
	{
		if (mod.IsBasic)
		{
			return;
		}
		string stateName = "Download Mod for WV" + SingletoneForm<Loader>.Instance._uniqueId;
		SingletoneForm<Loader>.Instance.Init(stateName, callback, null);
		SingletoneForm<Loader>.Instance.Download(stateName, WWWUtil.ModsLodUrl(mod), true, 5);
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x0001D9D8 File Offset: 0x0001BBD8
	public static string DownloadLodWV(int weaponIndex, StateDownloaderFinishedCallback callback)
	{
		global::Console.print("Download Prefab", Color.grey);
		string name = ((Weapons)weaponIndex).ToString();
		string url = WWWUtil.lodsUrl(name);
		string text = "Download Weapon for WV" + SingletoneForm<Loader>.Instance._uniqueId;
		SingletoneForm<Loader>.Instance.Init(text, new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.WeaponDownloaded), callback);
		SingletoneForm<Loader>.Instance.Download(text, WWWUtil.levelsUrl("preweapon"), true, 100);
		SingletoneForm<Loader>.Instance.Download(text, url, true, 100);
		if (Utility.IsModableWeapon(weaponIndex))
		{
			Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods;
			Dictionary<ModType, int> mods = currentWeaponsMods[weaponIndex].Mods;
			Loader.DownloadMods(mods, text, true);
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
		return text;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0001DAB8 File Offset: 0x0001BCB8
	public static string DownloadLod(BaseWeapon weapon, StateDownloaderFinishedCallback callback)
	{
		global::Console.print("Download Weapon " + weapon.weaponUseType, Color.grey);
		string text = weapon.weaponUseType + SingletoneForm<Loader>.Instance._uniqueId.ToString();
		SingletoneForm<Loader>.Instance.Init(text, new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.WeaponDownloaded), callback);
		SingletoneForm<Loader>.Instance.Download(text, WWWUtil.lodsUrl(weapon.type.ToString()), true, 100);
		if (Utility.IsModableWeapon(weapon.type))
		{
			Loader.DownloadMods(Utility.StringToMods(weapon.state.Mods), text, true);
		}
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
		return text;
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0001DB78 File Offset: 0x0001BD78
	public static string DownloadCharacterView()
	{
		global::Console.print("Download characterView", Color.grey);
		SingletoneForm<Loader>.Instance.Init("characterView", new StateDownloaderFinishedCallback(SingletoneForm<Loader>.instance.CharacterViewLoaded), null);
		SingletoneForm<Loader>.Instance.Download("characterView", WWWUtil.levelsUrl("add"), true, 100);
		SingletoneForm<Loader>.Instance.CheckIfAllLoaded();
		return "characterView";
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001DBE0 File Offset: 0x0001BDE0
	private void GameDataDownloaded()
	{
		this.CanBeCanceled = false;
		global::Console.print("GameData Downloaded", Color.green);
		base.StartCoroutine(this.InitializeGame());
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001DC08 File Offset: 0x0001BE08
	private void AdditionalWeaponDownloaded()
	{
		global::Console.print("AdditionalWeapon Downloaded", Color.green);
		Peer.ClientGame.LocalPlayer.ChooseAmmunition();
		EventFactory.Call("HideInterface", null);
		EventFactory.Call("HidePopup", new Popup(WindowsID.DownloadAdditionalGameData, Language.CWMainLoading, Language.DownloadAdditionalGameDataDesc, PopupState.progress, false, false, string.Empty, "AdditionalWeapon"));
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001DC68 File Offset: 0x0001BE68
	private void AdditionalWeaponDownloadedWhileGame()
	{
		global::Console.print("AdditionalWeapon Downloaded", Color.green);
		Peer.ClientGame.LocalPlayer.ChooseAmmunition();
		EventFactory.Call("HidePopup", new Popup(WindowsID.DownloadAdditionalGameData, Language.CWMainLoading, Language.DownloadAdditionalGameDataDesc, PopupState.progress, false, false, string.Empty, "AdditionalWeapon"));
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001DCBC File Offset: 0x0001BEBC
	private void WeaponDownloaded()
	{
		global::Console.print("Weapon Downloaded", Color.green);
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001DCD0 File Offset: 0x0001BED0
	private void CharacterViewLoaded()
	{
		base.StartCoroutine(SingletoneForm<PrefabFactory>.Instance.LoadScene("add"));
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001DCE8 File Offset: 0x0001BEE8
	private IEnumerator InitializeGame()
	{
		yield return new WaitForSeconds(0.5f);
		Audio.DisableMusic();
		global::Console.print("LoadLevel Finished");
		for (int i = 0; i < Globals.I.mapNames.Length; i++)
		{
			if (GameObject.Find(Globals.I.mapNames[i]))
			{
				UnityEngine.Object.Destroy(GameObject.Find(Globals.I.mapNames[i]).gameObject);
			}
		}
		yield return base.StartCoroutine(SingletoneForm<PrefabFactory>.Instance.LoadLevel(Main.HostInfo.MapName));
		global::Console.print("LoadLevel loaded all content");
		Forms.UpdateForms();
		Audio.RefreshLoudness();
		if (Peer.Dedicated)
		{
			Peer.InitServer();
		}
		else
		{
			if (Peer.IsHost)
			{
				Peer.InitServer();
			}
			while (Main.IsGameLoading || !PrefabFactory.AddLoaded)
			{
				yield return new WaitForSeconds(1f);
			}
			yield return new WaitForSeconds(2f);
			Peer.ClientGame.LocalPlayer.FinishedLoading();
		}
		Forms.UpdateForms();
		Forms.OnConnected();
		this.IsGameLoaded = true;
		if (!Peer.Dedicated && CVars.Snow)
		{
			this.SetSnow();
		}
		if (!Peer.Dedicated && ClientLeagueSystem.IsLeagueGame)
		{
			ClientLeagueSystem.WaitPlayers();
			yield return new WaitForSeconds(5f);
			if (Peer.ClientGame.MatchState != MatchState.stoped)
			{
				ClientLeagueSystem.MatchStarting();
			}
		}
		yield break;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001DD04 File Offset: 0x0001BF04
	private void SetSnow()
	{
		try
		{
			Shader.SetGlobalFloat("_Snow", 2f);
			Shader.SetGlobalFloat("_SnowFalling", 0f);
		}
		catch (Exception)
		{
		}
		AdditivePrefabDownloader.Download("Snow", delegate(Prefab prefab)
		{
			if (prefab.obj)
			{
				this.destroyObj.Add((GameObject)UnityEngine.Object.Instantiate(prefab.obj));
			}
			else
			{
				GameObject gameObject = (GameObject)prefab.bundle.Load("Snow");
				if (gameObject)
				{
					this.destroyObj.Add((GameObject)UnityEngine.Object.Instantiate(gameObject));
				}
			}
		});
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0001DD6C File Offset: 0x0001BF6C
	public override void MainInitialize()
	{
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0001DD7C File Offset: 0x0001BF7C
	public override void OnDisconnect()
	{
		foreach (GameObject gameObject in this.destroyObj)
		{
			gameObject.SetActive(false);
			UnityEngine.Object.Destroy(gameObject);
		}
		this.destroyObj.Clear();
		this.IsGameLoaded = false;
		this.IsGameLoadedAndClicked = false;
		this.DownloadersForLoadingGui.Clear();
		base.StopAllCoroutines();
		base.OnDisconnect();
	}

	// Token: 0x0400041F RID: 1055
	public bool IsGameLoadedAndClicked;

	// Token: 0x04000420 RID: 1056
	public bool IsGameLoaded;

	// Token: 0x04000421 RID: 1057
	public bool CanBeCanceled;

	// Token: 0x04000422 RID: 1058
	public List<Downloader> DownloadersForLoadingGui = new List<Downloader>();

	// Token: 0x04000423 RID: 1059
	private List<Downloader> _downloaders = new List<Downloader>();

	// Token: 0x04000424 RID: 1060
	private List<StateDownloader> _states = new List<StateDownloader>();

	// Token: 0x04000425 RID: 1061
	private int _uniqueId = IDUtil.FirstID;

	// Token: 0x04000426 RID: 1062
	private List<GameObject> destroyObj = new List<GameObject>(10);
}
