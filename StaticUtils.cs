using System;
using UnityEngine;

// Token: 0x0200038E RID: 910
public static class StaticUtils
{
	// Token: 0x06001D43 RID: 7491 RVA: 0x00101148 File Offset: 0x000FF348
	public static void RaycastHitSort(RaycastHit[] hits)
	{
		int num = hits.Length;
		for (int i = 1; i < num; i++)
		{
			RaycastHit raycastHit = hits[i];
			float distance = raycastHit.distance;
			int num2 = i;
			int num3 = num2 - 1;
			while (num2 > 0 && distance < hits[num3].distance)
			{
				hits[num2] = hits[num3];
				num2--;
				num3--;
			}
			hits[num2] = raycastHit;
		}
	}
}
