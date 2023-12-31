using System;
using UnityEngine;

// Token: 0x02000391 RID: 913
public struct eRaycastHit
{
	// Token: 0x06001D55 RID: 7509 RVA: 0x0010167C File Offset: 0x000FF87C
	public eRaycastHit(RaycastHit hit)
	{
		this.hit = hit;
		this.transform = hit.transform;
		this.MaterialName = hit.collider.material.name;
		this.distance = hit.distance;
		this.point = hit.point;
	}

	// Token: 0x040021FD RID: 8701
	public RaycastHit hit;

	// Token: 0x040021FE RID: 8702
	public Transform transform;

	// Token: 0x040021FF RID: 8703
	public string MaterialName;

	// Token: 0x04002200 RID: 8704
	public float distance;

	// Token: 0x04002201 RID: 8705
	public Vector3 point;
}
