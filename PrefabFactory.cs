using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B9 RID: 185
[AddComponentMenu("Scripts/Engine/PrefabFactory")]
internal class PrefabFactory : SingletoneForm<PrefabFactory>
{
	// Token: 0x170000AC RID: 172
	// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001FCF4 File Offset: 0x0001DEF4
	// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001FD00 File Offset: 0x0001DF00
	public static Light Sun
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.sun;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.sun = value;
		}
	}

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0001FD10 File Offset: 0x0001DF10
	private static bool ClientAddLoaded
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.clientAdd != null && SingletoneForm<PrefabFactory>.instance.clientAdd.isDone;
		}
	}

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001FD44 File Offset: 0x0001DF44
	private static bool ServerAddLoaded
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.serverAdd != null && SingletoneForm<PrefabFactory>.instance.serverAdd.isDone;
		}
	}

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001FD78 File Offset: 0x0001DF78
	public static bool AddLoaded
	{
		get
		{
			return PrefabFactory.ServerAddLoaded || PrefabFactory.ClientAddLoaded;
		}
	}

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001FD8C File Offset: 0x0001DF8C
	// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001FD98 File Offset: 0x0001DF98
	public static GameObject CurrentLevel
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.currentLevel;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.currentLevel = value;
		}
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001FDA8 File Offset: 0x0001DFA8
	// (set) Token: 0x060004CE RID: 1230 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
	public static Renderer[] Renderers
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.renderers;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.renderers = value;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
	// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0001FDD0 File Offset: 0x0001DFD0
	public static int[] RenderersIndex
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.renderersIndex;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.renderersIndex = value;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
	// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001FDEC File Offset: 0x0001DFEC
	public static Terrain Terrain
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.terrain;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.terrain = value;
		}
	}

	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001FDFC File Offset: 0x0001DFFC
	// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0001FE08 File Offset: 0x0001E008
	public static int TerrainLightmapIndex
	{
		get
		{
			return SingletoneForm<PrefabFactory>.Instance.terrainLightmapIndex;
		}
		set
		{
			SingletoneForm<PrefabFactory>.Instance.terrainLightmapIndex = value;
		}
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0001FE18 File Offset: 0x0001E018
	public static GameObject GenerateWeaponWithoutCreating(int weapon)
	{
		return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Weapon)).Generate();
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0001FE2C File Offset: 0x0001E02C
	public static GameObject GenerateModWithoutCreating(MasteringMod mod)
	{
		GameObject result = null;
		PrefabHolder holderByName = PrefabFactory.GetHolderByName(Utility.GetModPrefabName(mod, WeaponPrefabType.Weapon));
		if (holderByName != null)
		{
			result = holderByName.Generate();
		}
		return result;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0001FE58 File Offset: 0x0001E058
	public static GameObject GenerateLodModWithoutCreating(MasteringMod mod)
	{
		return PrefabFactory.GetHolderByName(Utility.GetModPrefabName(mod, WeaponPrefabType.Lod)).Generate();
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0001FE6C File Offset: 0x0001E06C
	public static GameObject GenerateLodWeaponWithoutCreating(int weapon)
	{
		return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Lod)).Generate();
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0001FE80 File Offset: 0x0001E080
	public static GameObject GenerateLodSecondaryWeaponWithoutCreating(int weapon)
	{
		if (PrefabFactory.IsHolderExist(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Lod)))
		{
			return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Lod)).Generate();
		}
		return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(0, WeaponPrefabType.Lod)).Generate();
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0001FEC4 File Offset: 0x0001E0C4
	public static GameObject GenerateLodPrimaryWeaponWithoutCreating(int weapon)
	{
		if (PrefabFactory.IsHolderExist(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Lod)))
		{
			return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Lod)).Generate();
		}
		return PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(5, WeaponPrefabType.Lod)).Generate();
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0001FF08 File Offset: 0x0001E108
	public static void GenerateHandsAnimationWithoutCreating(int weapon, Animation a)
	{
		GameObject gameObject = PrefabFactory.GetHolderByName(Utility.GetWeaponPrefabName(weapon, WeaponPrefabType.Hands)).Generate();
		foreach (object obj in gameObject.animation)
		{
			AnimationState animationState = (AnimationState)obj;
			if (a.GetClip(animationState.name) == null)
			{
				a.AddClip(animationState.clip, animationState.clip.name);
			}
		}
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
	public IEnumerator LoadScene(string sceneName)
	{
		if (!Peer.Dedicated)
		{
			yield return base.StartCoroutine(PrefabFactory.GetHolderByName(sceneName).LoadAllAsync());
			AsyncOperation op = Application.LoadLevelAdditiveAsync("add");
			op.priority = 0;
			this.clientAdd = op;
			yield return op;
			CamouflageGUI.AddDownloaded = true;
		}
		yield break;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0001FFDC File Offset: 0x0001E1DC
	public IEnumerator LoadLevel(string mapName)
	{
		Network.SetLevelPrefix(1);
		yield return base.StartCoroutine(PrefabFactory.GetHolderByName(mapName).LoadAllAsync());
		if (!Peer.Dedicated)
		{
			yield return base.StartCoroutine(PrefabFactory.GetHolderByName("preweapon").LoadAllAsync());
			if (CVars.EncryptContent)
			{
				if (CVars.UseUnityCache)
				{
					yield return base.StartCoroutine(PrefabFactory.GetHolderByName("_cache_add").LoadAllAsync());
				}
				else
				{
					yield return base.StartCoroutine(PrefabFactory.GetHolderByName("_newWWW_add").LoadAllAsync());
				}
			}
			else
			{
				yield return base.StartCoroutine(PrefabFactory.GetHolderByName("add").LoadAllAsync());
			}
		}
		if (Peer.IsHost)
		{
			yield return base.StartCoroutine(PrefabFactory.GetHolderByName("server_add").LoadAllAsync());
		}
		yield return base.StartCoroutine(PrefabFactory.GetHolderByName(Main.HostInfo.MapName + "_add").LoadAllAsync());
		AsyncOperation op = Application.LoadLevelAdditiveAsync(Main.HostInfo.MapName);
		op.priority = 0;
		yield return op;
		PrefabFactory.Sun = GameObject.Find("sun").light;
		PrefabFactory.CurrentLevel = GameObject.Find(Main.HostInfo.MapName);
		PrefabFactory.Renderers = PrefabFactory.CurrentLevel.GetComponentsInChildren<Renderer>();
		PrefabFactory.RenderersIndex = new int[PrefabFactory.Renderers.Length];
		for (int i = 0; i < PrefabFactory.RenderersIndex.Length; i++)
		{
			PrefabFactory.RenderersIndex[i] = PrefabFactory.Renderers[i].lightmapIndex;
		}
		PrefabFactory.Terrain = (Terrain)UnityEngine.Object.FindObjectOfType(typeof(Terrain));
		if (PrefabFactory.Terrain)
		{
			PrefabFactory.TerrainLightmapIndex = PrefabFactory.Terrain.lightmapIndex;
		}
		if (!Peer.Dedicated)
		{
			op = Application.LoadLevelAdditiveAsync("add");
			op.priority = 0;
			this.clientAdd = op;
			yield return op;
			Main.UserInfo.settings.graphics.OnLevelLoaded();
		}
		if (Peer.IsHost)
		{
			op = Application.LoadLevelAdditiveAsync("server_add");
			op.priority = 0;
			this.serverAdd = op;
			yield return op;
			if (op != null)
			{
				yield return op;
			}
		}
		op = this.GetAsyncOperationLoadLevel(Main.HostInfo.MapName + "_add");
		Forms.OnLevelLoaded();
		yield break;
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x00020008 File Offset: 0x0001E208
	private AsyncOperation GetAsyncOperationLoadLevel(string level)
	{
		AsyncOperation asyncOperation = null;
		try
		{
			asyncOperation = Application.LoadLevelAdditiveAsync(level);
			asyncOperation.priority = 0;
		}
		catch (Exception)
		{
		}
		return asyncOperation;
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x00020050 File Offset: 0x0001E250
	public static void DestroyLevel(bool unloadAll = false)
	{
		List<PoolItem> list = new List<PoolItem>();
		for (int i = 0; i < Main.Trash.GetChildCount(); i++)
		{
			PoolItem component = Main.Trash.GetChild(i).GetComponent<PoolItem>();
			if (component)
			{
				list.Add(component);
			}
		}
		for (int j = 0; j < list.Count; j++)
		{
			SingletoneForm<PoolManager>.Instance[list[j].name].Despawn(list[j]);
		}
		list.Clear();
		List<Pool> list2 = new List<Pool>();
		for (int k = 0; k < Main.Trash.GetChildCount(); k++)
		{
			Pool component2 = Main.Trash.GetChild(k).GetComponent<Pool>();
			if (component2)
			{
				list2.Add(component2);
			}
		}
		for (int l = 0; l < list2.Count; l++)
		{
			UnityEngine.Object.Destroy(list2[l].gameObject);
		}
		list2.Clear();
		GameObject objectName = GameObject.Find("add");
		PrefabFactory.DestroyLevelItem(objectName);
		GameObject objectName2 = GameObject.Find("server_add");
		PrefabFactory.DestroyLevelItem(objectName2);
		if (SingletoneForm<PrefabFactory>.Instance.currentLevel)
		{
			GameObject objectName3 = GameObject.Find(SingletoneForm<PrefabFactory>.Instance.currentLevel.name + "_add");
			PrefabFactory.DestroyLevelItem(objectName3);
		}
		PrefabFactory.DestroyLevelItem(SingletoneForm<PrefabFactory>.Instance.currentLevel);
		SingletoneForm<PrefabFactory>.Instance.sun = null;
		SingletoneForm<PrefabFactory>.Instance.serverAdd = null;
		SingletoneForm<PrefabFactory>.Instance.clientAdd = null;
		SingletoneForm<PrefabFactory>.Instance.renderers = null;
		SingletoneForm<PrefabFactory>.Instance.terrain = null;
		SingletoneForm<PrefabFactory>.Instance.terrainLightmapIndex = -1;
		Forms.UpdateForms();
		Audio.EnableMusic();
		if (unloadAll)
		{
			SingletoneForm<PrefabFactory>.Instance.StartCoroutine(SingletoneForm<PrefabFactory>.Instance.UnloadAll());
		}
		Forms.OnLevelUnloaded();
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x00020240 File Offset: 0x0001E440
	private static void DestroyLevelItem(GameObject objectName)
	{
		if (objectName)
		{
			UnityEngine.Object.Destroy(objectName);
			objectName = null;
		}
	}

	// Token: 0x060004E1 RID: 1249 RVA: 0x00020258 File Offset: 0x0001E458
	private IEnumerator UnloadAll()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0.1f);
		PoolManager.Refresh();
		PrefabFactory.ForcedUnload();
		yield return new WaitForEndOfFrame();
		yield return new WaitForSeconds(0.1f);
		AsyncOperation op = Resources.UnloadUnusedAssets();
		op.priority = 0;
		yield return op;
		global::Console.print("Resources unloaded");
		yield break;
	}

	// Token: 0x060004E2 RID: 1250 RVA: 0x0002026C File Offset: 0x0001E46C
	public static void ForcedUnload()
	{
		for (int i = 0; i < SingletoneForm<PrefabFactory>.Instance.prefabs.Count; i++)
		{
			SingletoneForm<PrefabFactory>.Instance.prefabs[i].Unload();
		}
		if (CVars.n_unloadAssetBundle)
		{
			SingletoneForm<PrefabFactory>.Instance.prefabs.Clear();
			Loader.Unload(true);
		}
	}

	// Token: 0x060004E3 RID: 1251 RVA: 0x000202D0 File Offset: 0x0001E4D0
	public static void AddHolder(Downloader downloader)
	{
		for (int i = 0; i < SingletoneForm<PrefabFactory>.Instance.prefabs.Count; i++)
		{
			if (SingletoneForm<PrefabFactory>.Instance.prefabs[i].FileNameWithoutExtension == downloader.FileNameWithoutExtension)
			{
				return;
			}
		}
		PrefabHolder prefabHolder = new PrefabHolder();
		prefabHolder.Init(downloader);
		SingletoneForm<PrefabFactory>.Instance.prefabs.Add(prefabHolder);
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x00020340 File Offset: 0x0001E540
	public static PrefabHolder GetHolderByName(string Name)
	{
		for (int i = 0; i < SingletoneForm<PrefabFactory>.Instance.prefabs.Count; i++)
		{
			if (SingletoneForm<PrefabFactory>.Instance.prefabs[i].FileNameWithoutExtension == Name)
			{
				return SingletoneForm<PrefabFactory>.Instance.prefabs[i];
			}
		}
		Debug.LogWarning("Object not found " + Name);
		return null;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x000203B0 File Offset: 0x0001E5B0
	public static bool IsHolderExist(string Name)
	{
		for (int i = 0; i < SingletoneForm<PrefabFactory>.Instance.prefabs.Count; i++)
		{
			if (SingletoneForm<PrefabFactory>.Instance.prefabs[i].FileNameWithoutExtension == Name)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004E6 RID: 1254 RVA: 0x00020400 File Offset: 0x0001E600
	public override void OnDisconnect()
	{
		LightmapSettings.lightmaps = new LightmapData[0];
		base.StopAllCoroutines();
		base.OnDisconnect();
	}

	// Token: 0x04000458 RID: 1112
	private List<PrefabHolder> prefabs = new List<PrefabHolder>();

	// Token: 0x04000459 RID: 1113
	private Light sun;

	// Token: 0x0400045A RID: 1114
	private GameObject currentLevel;

	// Token: 0x0400045B RID: 1115
	private Renderer[] renderers;

	// Token: 0x0400045C RID: 1116
	private int[] renderersIndex;

	// Token: 0x0400045D RID: 1117
	private Terrain terrain;

	// Token: 0x0400045E RID: 1118
	private int terrainLightmapIndex = -1;

	// Token: 0x0400045F RID: 1119
	private AsyncOperation clientAdd;

	// Token: 0x04000460 RID: 1120
	private AsyncOperation serverAdd;
}
