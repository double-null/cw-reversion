using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class CameraShake : MonoBehaviour
{
	// Token: 0x060000F0 RID: 240 RVA: 0x0000B7A4 File Offset: 0x000099A4
	private void Start()
	{
		this.myTransform = base.transform;
		this.initPos = this.myTransform.position;
		this.noiseBigX = new Noise(this.bigSpeed);
		this.noiseBigY = new Noise(this.bigSpeed);
		this.noiseSmallX = new Noise(this.smallSpeed);
		this.noiseSmallY = new Noise(this.smallSpeed);
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000B814 File Offset: 0x00009A14
	private void Update()
	{
		this.myTransform.position = this.initPos + new Vector3(this.noiseBigX.Update() * this.bigRadius + this.noiseSmallX.Update() * this.smallRadius, this.noiseBigY.Update() * this.bigRadius + this.noiseSmallY.Update() * this.smallRadius, 0f);
	}

	// Token: 0x040001A1 RID: 417
	private Transform myTransform;

	// Token: 0x040001A2 RID: 418
	private Vector3 initPos;

	// Token: 0x040001A3 RID: 419
	public float bigRadius = 3f;

	// Token: 0x040001A4 RID: 420
	public float bigSpeed = 0.5f;

	// Token: 0x040001A5 RID: 421
	public float smallRadius = 0.5f;

	// Token: 0x040001A6 RID: 422
	public float smallSpeed = 2f;

	// Token: 0x040001A7 RID: 423
	private Noise noiseBigX;

	// Token: 0x040001A8 RID: 424
	private Noise noiseBigY;

	// Token: 0x040001A9 RID: 425
	private Noise noiseSmallX;

	// Token: 0x040001AA RID: 426
	private Noise noiseSmallY;
}
