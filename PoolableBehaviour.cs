using System;
using UnityEngine;

// Token: 0x02000093 RID: 147
[AddComponentMenu("Scripts/Engine/Foundation/eMono")]
public class PoolableBehaviour : MonoBehaviour
{
	// Token: 0x06000347 RID: 839 RVA: 0x00017DA8 File Offset: 0x00015FA8
	public virtual void OnPoolSpawn()
	{
	}

	// Token: 0x06000348 RID: 840 RVA: 0x00017DAC File Offset: 0x00015FAC
	public virtual void OnPoolDespawn()
	{
		base.CancelInvoke();
		base.StopAllCoroutines();
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00017DBC File Offset: 0x00015FBC
	protected virtual void Awake()
	{
	}
}
