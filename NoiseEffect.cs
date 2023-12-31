using System;
using UnityEngine;

// Token: 0x020001CB RID: 459
[AddComponentMenu("Image Effects/NoiseEffect")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
internal class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000F83 RID: 3971 RVA: 0x000B1CC4 File Offset: 0x000AFEC4
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shader == null)
		{
			this.shader = StartData.shaders.Noise;
		}
		if (this.shader == null)
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
		}
		else if (!this.shader.isSupported)
		{
			base.enabled = false;
		}
		if (this.grainTexture == null)
		{
			this.grainTexture = StartData.shaders.noise;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000B1D64 File Offset: 0x000AFF64
	private Material material
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.shader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this._material;
		}
	}

	// Token: 0x06000F85 RID: 3973 RVA: 0x000B1D9C File Offset: 0x000AFF9C
	protected void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x06000F86 RID: 3974 RVA: 0x000B1DBC File Offset: 0x000AFFBC
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = this.material;
		material.SetTexture("_GrainTex", this.grainTexture);
		material.SetVector("_Vals", new Vector4(UnityEngine.Random.value, UnityEngine.Random.value, this.Scale, this.Intensity));
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x04001001 RID: 4097
	public float Scale = 1f;

	// Token: 0x04001002 RID: 4098
	public float Intensity = 1f;

	// Token: 0x04001003 RID: 4099
	public Texture grainTexture;

	// Token: 0x04001004 RID: 4100
	public Shader shader;

	// Token: 0x04001005 RID: 4101
	private Material _material;
}
