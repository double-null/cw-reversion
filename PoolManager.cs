using System;
using UnityEngine;

// Token: 0x020000B8 RID: 184
[AddComponentMenu("Scripts/Engine/PoolManager")]
internal class PoolManager : SingletoneForm<PoolManager>
{
	// Token: 0x170000AB RID: 171
	public Pool this[string name]
	{
		get
		{
			for (int i = 0; i < this.pools.Length; i++)
			{
				if (this.pools[i] != null && this.names[i] == name)
				{
					return this.pools[i];
				}
			}
			Debug.LogWarning("No such pool presented (" + name + ")");
			return null;
		}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
	public static GameObject Spawn(string name)
	{
		return SingletoneForm<PoolManager>.Instance[name].Spawn();
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
	public static GameObject Spawn(string name, GameObject prefab, int count = 2)
	{
		for (int i = 0; i < SingletoneForm<PoolManager>.Instance.pools.Length; i++)
		{
			if (SingletoneForm<PoolManager>.Instance.names[i] == name)
			{
				return SingletoneForm<PoolManager>.Instance.pools[i].Spawn();
			}
		}
		Pool pool = new GameObject(name).AddComponent<Pool>();
		pool.transform.parent = Main.Trash;
		for (int j = 0; j < count; j++)
		{
			PoolItem poolItem = (UnityEngine.Object.Instantiate(prefab) as GameObject).AddComponent<PoolItem>();
			poolItem.transform.parent = pool.transform;
			poolItem.gameObject.SetActive(false);
			poolItem.pool = pool;
			poolItem.observed = poolItem.GetComponentsInChildren<PoolableBehaviour>(true);
		}
		pool.objects = pool.GetComponentsInChildren<PoolItem>(true);
		PoolManager.Refresh();
		return SingletoneForm<PoolManager>.Instance[name].Spawn();
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
	public static void Despawn(GameObject obj)
	{
		if (SingletoneForm<PoolManager>.Instance[obj.name] == null)
		{
			return;
		}
		SingletoneForm<PoolManager>.Instance[obj.name].Despawn(obj.GetComponent<PoolItem>());
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
	public static void Despawn(string name, GameObject obj)
	{
		if (SingletoneForm<PoolManager>.Instance[name] == null)
		{
			return;
		}
		SingletoneForm<PoolManager>.Instance[name].Despawn(obj.GetComponent<PoolItem>());
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0001FC30 File Offset: 0x0001DE30
	public static void Refresh()
	{
		SingletoneForm<PoolManager>.Instance.pools = (Pool[])UnityEngine.Object.FindObjectsOfType(typeof(Pool));
		SingletoneForm<PoolManager>.Instance.names = new string[SingletoneForm<PoolManager>.Instance.pools.Length];
		for (int i = 0; i < SingletoneForm<PoolManager>.Instance.names.Length; i++)
		{
			SingletoneForm<PoolManager>.Instance.names[i] = SingletoneForm<PoolManager>.Instance.pools[i].name;
		}
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
	public override void MainInitialize()
	{
		PoolManager.Refresh();
		base.MainInitialize();
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
	public override void OnLevelLoaded()
	{
		PoolManager.Refresh();
		base.OnLevelLoaded();
	}

	// Token: 0x060004C4 RID: 1220 RVA: 0x0001FCD0 File Offset: 0x0001DED0
	public override void OnLevelUnloaded()
	{
		PoolManager.Refresh();
	}

	// Token: 0x04000456 RID: 1110
	private Pool[] pools;

	// Token: 0x04000457 RID: 1111
	private string[] names;
}
