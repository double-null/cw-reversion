using System;
using UnityEngine;

// Token: 0x02000047 RID: 71
[AddComponentMenu("Image Effects/Grayscale")]
[ExecuteInEditMode]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x060000FF RID: 255 RVA: 0x0000B980 File Offset: 0x00009B80
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.intensity);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001B0 RID: 432
	public float intensity;

	// Token: 0x040001B1 RID: 433
	public Texture textureRamp;

	// Token: 0x040001B2 RID: 434
	public float rampOffsetR;

	// Token: 0x040001B3 RID: 435
	public float rampOffsetG;

	// Token: 0x040001B4 RID: 436
	public float rampOffsetB;
}
