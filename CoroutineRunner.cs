using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000378 RID: 888
internal class CoroutineRunner : MonoBehaviour
{
	// Token: 0x06001CD5 RID: 7381 RVA: 0x000FF268 File Offset: 0x000FD468
	internal static void RunCoroutine(IEnumerator coroutine)
	{
		if (CoroutineRunner._instance == null)
		{
			CoroutineRunner._instance = new GameObject("CoroutineRunner").AddComponent<CoroutineRunner>();
		}
		CoroutineRunner._instance.StartCoroutine(coroutine);
	}

	// Token: 0x04002196 RID: 8598
	private static CoroutineRunner _instance;
}
