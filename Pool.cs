using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
[AddComponentMenu("Pool System/Pool")]
public class Pool : MonoBehaviour
{
	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000339 RID: 825 RVA: 0x00017904 File Offset: 0x00015B04
	private int Balance
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.objects.Length; i++)
			{
				if (this.objects[i].itemActive)
				{
					num++;
				}
			}
			return this.objects.Length - num;
		}
	}

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x0600033A RID: 826 RVA: 0x0001794C File Offset: 0x00015B4C
	private GameObject Next
	{
		get
		{
			if (this.Balance > 1)
			{
				for (int i = 0; i < this.objects.Length; i++)
				{
					if (!this.objects[i].itemActive)
					{
						if (this.switchActive)
						{
							this.objects[i].gameObject.SetActiveRecursively(true);
							this.objects[i].gameObject.active = true;
						}
						this.objects[i].OnPoolSpawn();
						this.objects[i].itemActive = true;
						return this.objects[i].gameObject;
					}
				}
			}
			return null;
		}
	}

	// Token: 0x0600033B RID: 827 RVA: 0x000179EC File Offset: 0x00015BEC
	public void Expand()
	{
		if (this.autodespawn)
		{
			this.Despawn(this.objects[0]);
		}
		else
		{
			if (this.Balance >= 1)
			{
				if (CVars.n_httpDebug)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Pool ",
						base.name,
						" growing. (Expensive cost, next size is ",
						(this.objects.Length + 2).ToString(),
						")"
					}));
				}
			}
			else
			{
				Debug.LogError("Pool " + base.name + " can't grow. (Balance is 0)");
				Debug.Break();
			}
			PoolItem[] array = new PoolItem[this.objects.Length + 2];
			PoolItem poolItem = null;
			for (int i = 0; i < array.Length; i++)
			{
				if (i < this.objects.Length)
				{
					array[i] = this.objects[i];
					if (!this.objects[i].itemActive)
					{
						poolItem = this.objects[i];
					}
				}
				else
				{
					if (poolItem == null)
					{
						Debug.LogError("PoolItem free is null");
						Debug.Break();
					}
					array[i] = ((GameObject)UnityEngine.Object.Instantiate(poolItem.gameObject)).GetComponent<PoolItem>();
					array[i].name = base.name;
					this.Despawn(array[i]);
				}
			}
			this.objects = array;
		}
	}

	// Token: 0x0600033C RID: 828 RVA: 0x00017B48 File Offset: 0x00015D48
	public GameObject Spawn()
	{
		if (this.cachedTransform == null)
		{
			this.cachedTransform = new CachedTransform(this.objects[0].transform);
		}
		GameObject next = this.Next;
		if (next)
		{
			return next;
		}
		this.Expand();
		return this.Next;
	}

	// Token: 0x0600033D RID: 829 RVA: 0x00017B98 File Offset: 0x00015D98
	public void Despawn(PoolItem obj)
	{
		Utility.ChangeParent(obj.transform, base.transform);
		if (this.cachedTransform == null)
		{
			this.cachedTransform = new CachedTransform(this.objects[0].transform);
		}
		this.cachedTransform.Reset(obj.transform);
		obj.OnPoolDespawn();
		obj.itemActive = false;
		if (this.switchActive)
		{
			obj.gameObject.SetActiveRecursively(false);
			obj.gameObject.active = false;
		}
	}

	// Token: 0x0400036F RID: 879
	public bool autodespawn;

	// Token: 0x04000370 RID: 880
	public bool switchActive = true;

	// Token: 0x04000371 RID: 881
	public PoolItem[] objects;

	// Token: 0x04000372 RID: 882
	private CachedTransform cachedTransform;
}
