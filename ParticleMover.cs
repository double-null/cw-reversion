using System;
using UnityEngine;

// Token: 0x02000371 RID: 881
[AddComponentMenu("Scripts/Engine/Components/ParticleMover")]
internal class ParticleMover : PoolableBehaviour
{
	// Token: 0x06001CA3 RID: 7331 RVA: 0x000FDBE4 File Offset: 0x000FBDE4
	public override void OnPoolDespawn()
	{
		base.transform.localPosition = this.cachedlocalPosition;
		base.OnPoolDespawn();
	}

	// Token: 0x06001CA4 RID: 7332 RVA: 0x000FDC00 File Offset: 0x000FBE00
	public override void OnPoolSpawn()
	{
		if (this.randomPositionInSphere)
		{
			base.transform.localPosition = this.cachedlocalPosition + UnityEngine.Random.insideUnitSphere * this.randomPositionInSphereRadius;
		}
		base.OnPoolSpawn();
	}

	// Token: 0x06001CA5 RID: 7333 RVA: 0x000FDC44 File Offset: 0x000FBE44
	protected override void Awake()
	{
		this.cachedlocalPosition = base.transform.localPosition;
	}

	// Token: 0x06001CA6 RID: 7334 RVA: 0x000FDC58 File Offset: 0x000FBE58
	private void Update()
	{
		base.transform.localPosition += this.moveDirection * Time.deltaTime * this.speed;
	}

	// Token: 0x0400217F RID: 8575
	public bool randomPositionInSphere;

	// Token: 0x04002180 RID: 8576
	public float randomPositionInSphereRadius = 1f;

	// Token: 0x04002181 RID: 8577
	public Vector3 moveDirection = Vector3.up;

	// Token: 0x04002182 RID: 8578
	public float speed = 1f;

	// Token: 0x04002183 RID: 8579
	private Vector3 cachedlocalPosition;
}
