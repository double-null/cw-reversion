using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000048 RID: 72
[Obfuscation(Exclude = true)]
[AddComponentMenu("Image Effects/Lighten")]
[ExecuteInEditMode]
public class LightenEffect : ImageEffectBase
{
	// Token: 0x06000101 RID: 257 RVA: 0x0000B9FC File Offset: 0x00009BFC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001B5 RID: 437
	public float desaturateAmount;

	// Token: 0x040001B6 RID: 438
	public Texture textureRamp;

	// Token: 0x040001B7 RID: 439
	public float rampOffsetR;

	// Token: 0x040001B8 RID: 440
	public float rampOffsetG;

	// Token: 0x040001B9 RID: 441
	public float rampOffsetB;
}
