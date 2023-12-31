using System;
using UnityEngine;

// Token: 0x02000049 RID: 73
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Luminosity")]
public class LuminosityEffect : ImageEffectBase
{
	// Token: 0x06000103 RID: 259 RVA: 0x0000BA78 File Offset: 0x00009C78
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001BA RID: 442
	public float desaturateAmount;

	// Token: 0x040001BB RID: 443
	public Texture textureRamp;

	// Token: 0x040001BC RID: 444
	public float rampOffsetR;

	// Token: 0x040001BD RID: 445
	public float rampOffsetG;

	// Token: 0x040001BE RID: 446
	public float rampOffsetB;
}
