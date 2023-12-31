using System;
using UnityEngine;

// Token: 0x020001DE RID: 478
internal class Tonemapping2 : MonoBehaviour
{
	// Token: 0x06000FEC RID: 4076 RVA: 0x000B4B3C File Offset: 0x000B2D3C
	private void Start()
	{
		if (this.tonemapMaterial == null)
		{
			this.tonemapMaterial = new Material(StartData.shaders.Tonemapper2);
		}
		this.UpdateCurve();
	}

	// Token: 0x06000FED RID: 4077 RVA: 0x000B4B78 File Offset: 0x000B2D78
	public void UpdateCurve()
	{
		if (this.remapCurve == null)
		{
			this.remapCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe(0f, 0f),
				new Keyframe(0.1f, 0.2f),
				new Keyframe(0.2f, 0.1f),
				new Keyframe(0.8f, 1.8f)
			});
		}
		this.curveTex = this.CurveToTexture(this.remapCurve, 255, this.curveTex);
	}

	// Token: 0x06000FEE RID: 4078 RVA: 0x000B4C2C File Offset: 0x000B2E2C
	public Texture2D CurveToTexture(AnimationCurve curve, int length, Texture2D texture = null)
	{
		if (curve == null)
		{
			return null;
		}
		if (texture == null)
		{
			texture = new Texture2D(length, 1, TextureFormat.ARGB32, false, true);
			texture.filterMode = FilterMode.Bilinear;
			texture.wrapMode = TextureWrapMode.Clamp;
			texture.hideFlags = HideFlags.DontSave;
		}
		float num = 1f / (float)length;
		for (int i = 0; i <= length; i++)
		{
			float num2 = this.remapCurve.Evaluate((float)i * num);
			texture.SetPixel(i, 0, new Color(num2, num2, num2));
		}
		texture.Apply();
		return texture;
	}

	// Token: 0x06000FEF RID: 4079 RVA: 0x000B4CB0 File Offset: 0x000B2EB0
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.tonemapMaterial.SetTexture("_Curve", this.curveTex);
		Graphics.Blit(source, destination, this.tonemapMaterial);
		if (this.Thermal != null)
		{
			this.Thermal.DrawMask();
		}
	}

	// Token: 0x0400106C RID: 4204
	private Material tonemapMaterial;

	// Token: 0x0400106D RID: 4205
	private Texture2D curveTex;

	// Token: 0x0400106E RID: 4206
	public AnimationCurve remapCurve;

	// Token: 0x0400106F RID: 4207
	public Thermal Thermal;
}
