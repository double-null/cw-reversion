using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000046 RID: 70
[AddComponentMenu("Image Effects/Desaturate")]
[Obfuscation(Exclude = true)]
[ExecuteInEditMode]
public class DesaturateEffect : ImageEffectBase
{
	// Token: 0x060000FD RID: 253 RVA: 0x0000B904 File Offset: 0x00009B04
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_Desat", this.desaturateAmount);
		base.material.SetVector("_RampOffset", new Vector4(this.rampOffsetR, this.rampOffsetG, this.rampOffsetB, 0f));
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x040001AB RID: 427
	public float desaturateAmount;

	// Token: 0x040001AC RID: 428
	public Texture textureRamp;

	// Token: 0x040001AD RID: 429
	public float rampOffsetR;

	// Token: 0x040001AE RID: 430
	public float rampOffsetG;

	// Token: 0x040001AF RID: 431
	public float rampOffsetB;
}
