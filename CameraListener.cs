using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
[AddComponentMenu("Scripts/Engine/CameraListener")]
internal class CameraListener : SingletoneForm<CameraListener>
{
	// Token: 0x17000024 RID: 36
	// (get) Token: 0x0600019C RID: 412 RVA: 0x0000DB7C File Offset: 0x0000BD7C
	public static Camera Camera
	{
		get
		{
			return SingletoneForm<CameraListener>.Instance.camera;
		}
	}

	// Token: 0x17000025 RID: 37
	// (get) Token: 0x0600019D RID: 413 RVA: 0x0000DB88 File Offset: 0x0000BD88
	public static AudioListener Listener
	{
		get
		{
			return Audio.Listener;
		}
	}

	// Token: 0x0600019E RID: 414 RVA: 0x0000DB90 File Offset: 0x0000BD90
	public static void ChangeTo(GameObject obj)
	{
		if (SingletoneForm<CameraListener>.Instance.camera != null)
		{
			CameraListener.Disable(SingletoneForm<CameraListener>.Instance.camera.gameObject);
		}
		Audio.ChangeListener(obj.GetComponent<AudioListener>());
		SingletoneForm<CameraListener>.Instance.camera = obj.camera;
		CameraListener.OnProfileChanged();
	}

	// Token: 0x0600019F RID: 415 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
	public static void Enable(GameObject obj)
	{
		Camera[] array = new Camera[0];
		BloomAndLensFlares[] array2 = new BloomAndLensFlares[0];
		ColorCorrectionCurves[] array3 = new ColorCorrectionCurves[0];
		DepthOfField34[] array4 = new DepthOfField34[0];
		AntialiasingAsPostEffect[] array5 = new AntialiasingAsPostEffect[0];
		Vignetting[] array6 = new Vignetting[0];
		if (obj.GetComponentInChildren<Camera>())
		{
			array = obj.GetComponentsInChildren<Camera>();
		}
		if (obj.GetComponentInChildren<BloomAndLensFlares>())
		{
			array2 = obj.GetComponentsInChildren<BloomAndLensFlares>();
		}
		if (obj.GetComponentInChildren<ColorCorrectionCurves>())
		{
			array3 = obj.GetComponentsInChildren<ColorCorrectionCurves>();
		}
		if (obj.GetComponentInChildren<DepthOfField34>())
		{
			array4 = obj.GetComponentsInChildren<DepthOfField34>();
		}
		if (obj.GetComponentInChildren<AntialiasingAsPostEffect>())
		{
			array5 = obj.GetComponentsInChildren<AntialiasingAsPostEffect>();
		}
		if (obj.GetComponentInChildren<Vignetting>())
		{
			array6 = obj.GetComponentsInChildren<Vignetting>();
		}
		SunOnGlass[] componentsInChildren = obj.GetComponentsInChildren<SunOnGlass>();
		SunShafts[] componentsInChildren2 = obj.GetComponentsInChildren<SunShafts>();
		for (int i = 0; i < array.Length; i++)
		{
			float[] layerCullDistances = array[i].layerCullDistances;
			layerCullDistances[21] = Main.UserInfo.settings.graphics.SmallDistance;
			layerCullDistances[24] = Main.UserInfo.settings.graphics.SmallDistance;
			array[i].layerCullDistances = layerCullDistances;
			array[i].ResetAspect();
			array[i].enabled = true;
		}
		if (Main.UserInfo.settings.graphics.PostEffects && Main.UserInfo.settings.graphics.Level != QualityLevelUser.VeryLow)
		{
			for (int j = 0; j < componentsInChildren.Length; j++)
			{
				componentsInChildren[j].Settings = 2;
			}
			for (int k = 0; k < componentsInChildren2.Length; k++)
			{
				componentsInChildren2[k].enabled = true;
				if (Main.UserInfo.settings.graphics.LightingQ == Quality.high)
				{
					componentsInChildren2[k].resolution = SunShaftsResolution.Normal;
				}
				else if (Main.UserInfo.settings.graphics.LightingQ == Quality.max)
				{
					componentsInChildren2[k].resolution = SunShaftsResolution.Normal;
				}
				else
				{
					componentsInChildren2[k].resolution = SunShaftsResolution.Low;
				}
			}
			for (int l = 0; l < array2.Length; l++)
			{
				array2[l].enabled = true;
			}
			for (int m = 0; m < array3.Length; m++)
			{
				array3[m].enabled = true;
			}
			for (int n = 0; n < array4.Length; n++)
			{
				if (Main.UserInfo.settings.disableBlur)
				{
					array4[n].enabled = false;
				}
				else
				{
					array4[n].enabled = false;
				}
			}
			for (int num = 0; num < array5.Length; num++)
			{
				array5[num].Mode = AAMode.FXAA2;
				array5[num].enabled = true;
			}
			for (int num2 = 0; num2 < array6.Length; num2++)
			{
				array6[num2].enabled = true;
			}
		}
		else
		{
			for (int num3 = 0; num3 < componentsInChildren.Length; num3++)
			{
				componentsInChildren[num3].Settings = ((Main.UserInfo.settings.graphics.Level == QualityLevelUser.VeryLow) ? 0 : 1);
			}
			for (int num4 = 0; num4 < componentsInChildren2.Length; num4++)
			{
				componentsInChildren2[num4].enabled = false;
			}
			for (int num5 = 0; num5 < array2.Length; num5++)
			{
				array2[num5].enabled = false;
			}
			for (int num6 = 0; num6 < array3.Length; num6++)
			{
				array3[num6].enabled = false;
			}
			for (int num7 = 0; num7 < array4.Length; num7++)
			{
				array4[num7].enabled = false;
			}
			for (int num8 = 0; num8 < array5.Length; num8++)
			{
				array5[num8].Mode = AAMode.FXAA2;
				array5[num8].enabled = true;
			}
			for (int num9 = 0; num9 < array6.Length; num9++)
			{
				array6[num9].enabled = false;
			}
		}
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000E00C File Offset: 0x0000C20C
	public static void Disable(GameObject obj)
	{
		Camera[] array = new Camera[0];
		BloomAndLensFlares[] array2 = new BloomAndLensFlares[0];
		ColorCorrectionCurves[] array3 = new ColorCorrectionCurves[0];
		DepthOfField34[] array4 = new DepthOfField34[0];
		AntialiasingAsPostEffect[] array5 = new AntialiasingAsPostEffect[0];
		Vignetting[] array6 = new Vignetting[0];
		if (obj.GetComponentInChildren<Camera>())
		{
			array = obj.GetComponentsInChildren<Camera>();
		}
		if (obj.GetComponentInChildren<BloomAndLensFlares>())
		{
			array2 = obj.GetComponentsInChildren<BloomAndLensFlares>();
		}
		if (obj.GetComponentInChildren<ColorCorrectionCurves>())
		{
			array3 = obj.GetComponentsInChildren<ColorCorrectionCurves>();
		}
		if (obj.GetComponentInChildren<DepthOfField34>())
		{
			array4 = obj.GetComponentsInChildren<DepthOfField34>();
		}
		if (obj.GetComponentInChildren<AntialiasingAsPostEffect>())
		{
			array5 = obj.GetComponentsInChildren<AntialiasingAsPostEffect>();
		}
		if (obj.GetComponentInChildren<Vignetting>())
		{
			array6 = obj.GetComponentsInChildren<Vignetting>();
		}
		SunOnGlass[] componentsInChildren = obj.GetComponentsInChildren<SunOnGlass>();
		SunShafts[] componentsInChildren2 = obj.GetComponentsInChildren<SunShafts>();
		for (int i = 0; i < array.Length; i++)
		{
			if (BIT.AND(array[i].cullingMask, 1 << LayerMask.NameToLayer("AWH")))
			{
				array[i].cullingMask ^= 1 << LayerMask.NameToLayer("AWH");
			}
			array[i].enabled = false;
		}
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			componentsInChildren[j].Settings = 0;
		}
		for (int k = 0; k < componentsInChildren2.Length; k++)
		{
			componentsInChildren2[k].enabled = false;
		}
		for (int l = 0; l < array2.Length; l++)
		{
			array2[l].enabled = false;
		}
		for (int m = 0; m < array3.Length; m++)
		{
			array3[m].enabled = false;
		}
		for (int n = 0; n < array4.Length; n++)
		{
			array4[n].enabled = false;
		}
		for (int num = 0; num < array5.Length; num++)
		{
			array5[num].enabled = true;
		}
		for (int num2 = 0; num2 < array6.Length; num2++)
		{
			array6[num2].enabled = false;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x0000E240 File Offset: 0x0000C440
	public static void OnProfileChanged()
	{
		CameraListener.Enable(SingletoneForm<CameraListener>.Instance.camera.gameObject);
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x0000E258 File Offset: 0x0000C458
	public override void OnDisconnect()
	{
		if (Main.GUIObject)
		{
			CameraListener.ChangeTo(Main.GUIObject);
		}
	}

	// Token: 0x0400020A RID: 522
	private new Camera camera;
}
