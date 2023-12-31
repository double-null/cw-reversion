using System;
using UnityEngine;

// Token: 0x02000080 RID: 128
[Serializable]
internal class Hierarchy
{
	// Token: 0x060002DA RID: 730 RVA: 0x000153D4 File Offset: 0x000135D4
	public void Init(Transform transform)
	{
		this.transform = transform;
		this.pos = transform.localPosition;
		this.euler = transform.localEulerAngles;
		this.childs = new Hierarchy[transform.GetChildCount()];
		for (int i = 0; i < this.childs.Length; i++)
		{
			this.childs[i] = new Hierarchy();
			this.childs[i].Init(transform.GetChild(i));
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x0001544C File Offset: 0x0001364C
	public void Reset()
	{
		this.transform.localPosition = this.pos;
		this.transform.localEulerAngles = this.euler;
		for (int i = 0; i < this.childs.Length; i++)
		{
			this.childs[i].Reset();
		}
	}

	// Token: 0x04000346 RID: 838
	public Vector3 pos;

	// Token: 0x04000347 RID: 839
	public Vector3 euler;

	// Token: 0x04000348 RID: 840
	public Transform transform;

	// Token: 0x04000349 RID: 841
	private Hierarchy[] childs;
}
