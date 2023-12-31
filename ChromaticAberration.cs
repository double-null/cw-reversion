using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[AddComponentMenu("Image Effects/ChromaticAberration")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class ChromaticAberration : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x000040CC File Offset: 0x000022CC
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
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
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000027 RID: 39 RVA: 0x0000412C File Offset: 0x0000232C
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

	// Token: 0x06000028 RID: 40 RVA: 0x00004164 File Offset: 0x00002364
	protected void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00004184 File Offset: 0x00002384
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = this.material;
		material.SetVector("_Shift", new Vector4(1f - this.Shift, 1f + this.Shift, 1f - this.Shift * 0.5f, 1f + this.Shift * 0.5f));
		source.anisoLevel = this.Aniso;
		source.filterMode = FilterMode.Bilinear;
		Graphics.Blit(source, destination, material, (!this.Simple) ? 1 : 0);
	}

	// Token: 0x04000053 RID: 83
	public float Shift = 0.01f;

	// Token: 0x04000054 RID: 84
	public int Aniso = 3;

	// Token: 0x04000055 RID: 85
	public bool Simple;

	// Token: 0x04000056 RID: 86
	public Shader shader;

	// Token: 0x04000057 RID: 87
	private Material _material;
}
