using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/CustomGlobalFog")]
[RequireComponent(typeof(Camera))]
public class CustomGlobalFog : MonoBehaviour
{
	// Token: 0x0600002B RID: 43 RVA: 0x00004228 File Offset: 0x00002428
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
		this._fovDepthFix = CustomGlobalFog.GetCurve(10f, new float[]
		{
			1f,
			1.007f,
			1.019f,
			1.038f,
			1.068f,
			1.107f,
			1.159f,
			1.222f,
			1.309f,
			1.413f,
			1.557f
		});
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600002C RID: 44 RVA: 0x000042A8 File Offset: 0x000024A8
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

	// Token: 0x0600002D RID: 45 RVA: 0x000042E0 File Offset: 0x000024E0
	private void OnEnable()
	{
		base.camera.depthTextureMode |= DepthTextureMode.DepthNormals;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000042F8 File Offset: 0x000024F8
	protected void OnDisable()
	{
		if (this._material)
		{
			UnityEngine.Object.DestroyImmediate(this._material);
		}
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00004318 File Offset: 0x00002518
	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material material = this.material;
		Shader.SetGlobalFloat("_Debug", this.DebugVal);
		Vector3 direction = base.camera.ViewportPointToRay(new Vector2(0.5f, 0f)).direction;
		Vector3 direction2 = base.camera.ViewportPointToRay(new Vector2(0.5f, 1f)).direction;
		material.SetVector("_YVec0", direction);
		material.SetVector("_YVec1", direction2);
		material.SetFloat("_GFogDepthMult", this._fovDepthFix.Evaluate(base.camera.fov));
		Shader.SetGlobalMatrix("_GFogCamToWorld", base.camera.cameraToWorldMatrix);
		Shader.SetGlobalFloat("_GFogMax", this.FogMaxDistance);
		Shader.SetGlobalColor("_GFogColor", this.FogColor);
		Shader.SetGlobalFloat("_GFogStrength", this.FogStrength);
		Shader.SetGlobalFloat("_GFogY", this.FogY);
		Shader.SetGlobalVector("_GFogFuncVals", new Vector4(this.FuncStart, this.FuncSoftness, this.FuncSoftness / this.FuncStart, this.DebugVal));
		Shader.SetGlobalVector("_GFogTopFuncVals", new Vector4(-this.FogToplength, 0.5f / this.FogToplength, this.FogTopIntensity, this.FuncSoftness * Mathf.Log(this.FuncSoftness / this.FuncStart)));
		source.filterMode = FilterMode.Bilinear;
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000044A4 File Offset: 0x000026A4
	public static AnimationCurve GetCurve(float step, params float[] vals)
	{
		Keyframe[] array = new Keyframe[vals.Length];
		int num = vals.Length - 1;
		for (int i = 1; i < num; i++)
		{
			float num2 = (vals[i + 1] - vals[i - 1]) * 0.05f;
			array[i] = new Keyframe((float)i * step, vals[i], num2, num2);
		}
		float num3 = (vals[1] - vals[0]) * 0.05f * 2f;
		array[0] = new Keyframe(0f, vals[0], num3, num3);
		float num4 = (vals[num] - vals[num - 1]) * 0.05f * 2f;
		array[num] = new Keyframe((float)num * step, vals[num], num4, num4);
		return new AnimationCurve(array);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000456C File Offset: 0x0000276C
	private void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr = 0)
	{
		RenderTexture.active = dest;
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		GL.TexCoord(base.camera.ViewportPointToRay(new Vector2(0f, 0f)).direction);
		GL.Vertex3(0f, 0f, 0f);
		GL.TexCoord(base.camera.ViewportPointToRay(new Vector2(0f, 1f)).direction);
		GL.Vertex3(0f, 1f, 0f);
		GL.TexCoord(base.camera.ViewportPointToRay(new Vector2(1f, 1f)).direction);
		GL.Vertex3(1f, 1f, 0f);
		GL.TexCoord(base.camera.ViewportPointToRay(new Vector2(1f, 0f)).direction);
		GL.Vertex3(1f, 0f, 0f);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x04000058 RID: 88
	private const float ChangeDirectionDelta = 0.001f;

	// Token: 0x04000059 RID: 89
	public Shader shader;

	// Token: 0x0400005A RID: 90
	public float DebugVal;

	// Token: 0x0400005B RID: 91
	public Color FogColor;

	// Token: 0x0400005C RID: 92
	public float FogStrength;

	// Token: 0x0400005D RID: 93
	public float FogY;

	// Token: 0x0400005E RID: 94
	public float FogToplength;

	// Token: 0x0400005F RID: 95
	public float FogTopIntensity;

	// Token: 0x04000060 RID: 96
	public float FogMaxDistance;

	// Token: 0x04000061 RID: 97
	public float DirectionDifferenceThreshold = 0.047f;

	// Token: 0x04000062 RID: 98
	public float FuncSoftness;

	// Token: 0x04000063 RID: 99
	public float FuncStart;

	// Token: 0x04000064 RID: 100
	private AnimationCurve _fovDepthFix;

	// Token: 0x04000065 RID: 101
	private Material _material;
}
