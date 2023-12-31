using System;
using UnityEngine;

// Token: 0x0200003C RID: 60
internal class Noise
{
	// Token: 0x060000ED RID: 237 RVA: 0x0000B6D4 File Offset: 0x000098D4
	public Noise(float speed)
	{
		this.seed = new Vector2(UnityEngine.Random.value, UnityEngine.Random.value);
		this.speed = speed;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000B704 File Offset: 0x00009904
	public float Update()
	{
		this.seed += Vector2.one * this.speed * Time.deltaTime;
		return Mathf.PerlinNoise(this.seed.x, this.seed.y) * 2f - 1f;
	}

	// Token: 0x0400019F RID: 415
	private Vector2 seed;

	// Token: 0x040001A0 RID: 416
	private float speed;
}
