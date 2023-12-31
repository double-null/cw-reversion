using System;
using UnityEngine;

// Token: 0x0200004A RID: 74
[AddComponentMenu("Image Effects/Screen")]
[ExecuteInEditMode]
public class ScreenEffect : ImageEffectBase
{
	// Token: 0x06000105 RID: 261 RVA: 0x0000BAF4 File Offset: 0x00009CF4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001BF RID: 447
	public float desaturateAmount;

	// Token: 0x040001C0 RID: 448
	public Texture textureRamp;

	// Token: 0x040001C1 RID: 449
	public float rampOffsetR;

	// Token: 0x040001C2 RID: 450
	public float rampOffsetG;

	// Token: 0x040001C3 RID: 451
	public float rampOffsetB;
}
