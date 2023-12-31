using System;
using UnityEngine;

// Token: 0x02000370 RID: 880
[AddComponentMenu("Scripts/Engine/Components/ParticleLight")]
internal class ParticleLight : PoolableBehaviour
{
	// Token: 0x06001C9E RID: 7326 RVA: 0x000FDAD4 File Offset: 0x000FBCD4
	public override void OnPoolDespawn()
	{
		this.time = 0f;
		base.light.range = this.lightRange;
		base.light.intensity = this.lightIntensity;
		base.OnPoolDespawn();
	}

	// Token: 0x06001C9F RID: 7327 RVA: 0x000FDB14 File Offset: 0x000FBD14
	public override void OnPoolSpawn()
	{
		this.time = Time.realtimeSinceStartup;
		base.OnPoolSpawn();
	}

	// Token: 0x06001CA0 RID: 7328 RVA: 0x000FDB28 File Offset: 0x000FBD28
	protected override void Awake()
	{
		this.lightRange = base.light.range;
		this.lightIntensity = base.light.intensity;
	}

	// Token: 0x06001CA1 RID: 7329 RVA: 0x000FDB58 File Offset: 0x000FBD58
	private void Update()
	{
		base.light.range = this.range.Evaluate(Time.realtimeSinceStartup - this.time) * this.lightRange;
		base.light.intensity = this.alpha.Evaluate(Time.realtimeSinceStartup - this.time) * this.lightIntensity;
	}

	// Token: 0x0400217A RID: 8570
	public AnimationCurve range;

	// Token: 0x0400217B RID: 8571
	public AnimationCurve alpha;

	// Token: 0x0400217C RID: 8572
	private float time;

	// Token: 0x0400217D RID: 8573
	private float lightRange = 1f;

	// Token: 0x0400217E RID: 8574
	private float lightIntensity = 1f;
}
