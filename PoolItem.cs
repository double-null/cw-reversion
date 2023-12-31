using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000091 RID: 145
[AddComponentMenu("Pool System/PoolItem")]
public class PoolItem : MonoBehaviour
{
	// Token: 0x0600033F RID: 831 RVA: 0x00017C30 File Offset: 0x00015E30
	public void OnPoolSpawn()
	{
		for (int i = 0; i < this.observed.Length; i++)
		{
			this.observed[i].OnPoolSpawn();
		}
		if (this.autoDespawn)
		{
			this.AutoDespawn(this.autoDespawnTime);
		}
	}

	// Token: 0x06000340 RID: 832 RVA: 0x00017C7C File Offset: 0x00015E7C
	public void OnPoolDespawn()
	{
		for (int i = 0; i < this.observed.Length; i++)
		{
			this.observed[i].OnPoolDespawn();
		}
		for (int j = 0; j < this.childs.Count; j++)
		{
			this.childs[j].Despawn();
		}
		this.childs.Clear();
		base.CancelInvoke();
		base.StopAllCoroutines();
	}

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x06000341 RID: 833 RVA: 0x00017CF4 File Offset: 0x00015EF4
	public List<PoolItem> Childs
	{
		get
		{
			return this.childs;
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x00017CFC File Offset: 0x00015EFC
	public void AutoDespawn(float time)
	{
		base.CancelInvoke("Despawn");
		base.Invoke("Despawn", time);
	}

	// Token: 0x06000343 RID: 835 RVA: 0x00017D18 File Offset: 0x00015F18
	[Obfuscation(Exclude = true)]
	public void Despawn()
	{
		this.pool.Despawn(this);
	}

	// Token: 0x04000373 RID: 883
	public bool autoDespawn;

	// Token: 0x04000374 RID: 884
	public float autoDespawnTime;

	// Token: 0x04000375 RID: 885
	public PoolableBehaviour[] observed;

	// Token: 0x04000376 RID: 886
	public Pool pool;

	// Token: 0x04000377 RID: 887
	public bool itemActive;

	// Token: 0x04000378 RID: 888
	private List<PoolItem> childs = new List<PoolItem>();
}
