using System;
using UnityEngine;

// Token: 0x0200003B RID: 59
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/ScreenWater")]
[RequireComponent(typeof(Camera))]
public class ScreenWater : MonoBehaviour
{
	// Token: 0x060000E9 RID: 233 RVA: 0x0000B5A8 File Offset: 0x000097A8
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shaderRGB == null)
		{
			Debug.Log("Sat shaders are not set up! Disabling saturation effect.");
			base.enabled = false;
		}
		else if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x060000EA RID: 234 RVA: 0x0000B608 File Offset: 0x00009808
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_MaterialRGB;
		}
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000B640 File Offset: 0x00009840
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			UnityEngine.Object.DestroyImmediate(this.m_MaterialRGB);
		}
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000B660 File Offset: 0x00009860
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = this.material;
		material.SetTexture("_Splat", this.WaterFlows);
		material.SetTexture("_Flow", this.WaterMask);
		material.SetTexture("_Water", this.WetScreen);
		material.SetFloat("_Speed", this.Speed);
		material.SetFloat("_Intens", this.Intens);
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x04000198 RID: 408
	public Shader shaderRGB;

	// Token: 0x04000199 RID: 409
	private Material m_MaterialRGB;

	// Token: 0x0400019A RID: 410
	public Texture2D WaterFlows;

	// Token: 0x0400019B RID: 411
	public Texture2D WaterMask;

	// Token: 0x0400019C RID: 412
	public Texture2D WetScreen;

	// Token: 0x0400019D RID: 413
	public float Speed = 0.5f;

	// Token: 0x0400019E RID: 414
	public float Intens = 0.5f;
}
