using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class SceneCameraFollow : MonoBehaviour
{
	// Token: 0x060000B1 RID: 177 RVA: 0x00009928 File Offset: 0x00007B28
	private void Update()
	{
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x0000992C File Offset: 0x00007B2C
	private static void Follow(Camera follower, Camera target)
	{
		Transform transform = target.transform;
		Transform transform2 = follower.transform;
		transform2.position = transform.position;
		transform2.rotation = transform.rotation;
	}
}
