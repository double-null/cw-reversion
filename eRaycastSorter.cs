using System;
using System.Collections.Generic;

// Token: 0x020000AB RID: 171
internal class eRaycastSorter : IComparer<eRaycastHit>
{
	// Token: 0x060003F8 RID: 1016 RVA: 0x0001B48C File Offset: 0x0001968C
	public int Compare(eRaycastHit a, eRaycastHit b)
	{
		return a.distance.CompareTo(b.distance);
	}
}
