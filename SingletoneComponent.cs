using System;
using UnityEngine;

// Token: 0x020000A0 RID: 160
public class SingletoneComponent<T> : PoolableBehaviour, Initializable, Singletone<T> where T : Component
{
	// Token: 0x0600038D RID: 909 RVA: 0x00019954 File Offset: 0x00017B54
	public void Initialize()
	{
		SingletoneComponent<T>.instance = (this as T);
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x0600038E RID: 910 RVA: 0x0001996C File Offset: 0x00017B6C
	public static T Instance
	{
		get
		{
			if (SingletoneComponent<T>.instance != null)
			{
				return SingletoneComponent<T>.instance;
			}
			SingletoneComponent<T>.instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
			if (SingletoneComponent<T>.instance == null)
			{
				Debug.Log(typeof(T).Name + " instance not found.");
			}
			return SingletoneComponent<T>.instance;
		}
	}

	// Token: 0x0400039F RID: 927
	protected static T instance;
}
