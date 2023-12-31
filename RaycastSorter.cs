using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000AA RID: 170
public class RaycastSorter : IComparer
{
	// Token: 0x060003F6 RID: 1014 RVA: 0x0001B450 File Offset: 0x00019650
	public int Compare(object a, object b)
	{
		RaycastHit raycastHit = (RaycastHit)a;
		RaycastHit raycastHit2 = (RaycastHit)b;
		return raycastHit.distance.CompareTo(raycastHit2.distance);
	}
}
