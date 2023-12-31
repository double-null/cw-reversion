using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C2 RID: 450
internal class CustomStaticBatching : MonoBehaviour
{
	// Token: 0x06000F69 RID: 3945 RVA: 0x000B10D8 File Offset: 0x000AF2D8
	private void Start()
	{
		Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
		LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
		foreach (Renderer renderer in componentsInChildren)
		{
			if (renderer.enabled && renderer.sharedMaterial != null)
			{
				GameObject gameObject = renderer.gameObject;
				linkedList.AddLast(gameObject);
			}
		}
		GameObject[] array = new GameObject[linkedList.Count];
		linkedList.CopyTo(array, 0);
		StaticBatchingUtility.Combine(array, base.gameObject);
	}
}
