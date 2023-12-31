using System;
using UnityEngine;

// Token: 0x0200002E RID: 46
[AddComponentMenu("Image Effects/TopLight")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class TopLight : MonoBehaviour
{
	// Token: 0x1700000A RID: 10
	// (get) Token: 0x060000B4 RID: 180 RVA: 0x00009968 File Offset: 0x00007B68
	private Material Mat
	{
		get
		{
			if (this._material == null)
			{
				this._material = new Material(this.TheShader);
				this._material.hideFlags = HideFlags.HideAndDontSave;
				this._material.SetColor("_Color", this.Color);
			}
			return this._material;
		}
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x000099C0 File Offset: 0x00007BC0
	private void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.TheShader == null)
		{
			string name = base.GetType().Name;
			Debug.Log(name + " shaders are not set up! Disabling " + name + " effect.");
			base.enabled = false;
		}
		else if (!this.TheShader.isSupported)
		{
			string name2 = base.GetType().Name;
			Debug.Log(name2 + " shaders are not supported! Disabling " + name2 + " effect.");
			base.enabled = false;
		}
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00009A58 File Offset: 0x00007C58
	protected void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00009A78 File Offset: 0x00007C78
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material mat = this.Mat;
		if (this._lastColor != this.Color)
		{
			this._lastColor = this.Color;
			mat.SetColor("_Color", this.Color);
		}
		source.anisoLevel = 0;
		source.filterMode = FilterMode.Point;
		Graphics.Blit(source, destination, mat);
	}

	// Token: 0x04000161 RID: 353
	public Shader TheShader;

	// Token: 0x04000162 RID: 354
	public Color Color;

	// Token: 0x04000163 RID: 355
	private Material _material;

	// Token: 0x04000164 RID: 356
	private Color _lastColor;
}
