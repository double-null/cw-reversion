using System;
using UnityEngine;

// Token: 0x0200009F RID: 159
public class SingletoneForm<T> : Form, Initializable, Singletone<T> where T : Component
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000389 RID: 905 RVA: 0x000198BC File Offset: 0x00017ABC
	public static T Instance
	{
		get
		{
			if (SingletoneForm<T>.instance == null)
			{
				SingletoneForm<T>.instance = (T)((object)UnityEngine.Object.FindObjectOfType(typeof(T)));
				if (SingletoneForm<T>.instance == null)
				{
					Debug.Log(typeof(T).Name + " instance not found.");
				}
			}
			return SingletoneForm<T>.instance;
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00019930 File Offset: 0x00017B30
	public void Initialize()
	{
		SingletoneForm<T>.instance = (this as T);
	}

	// Token: 0x0400039E RID: 926
	protected static T instance;
}
