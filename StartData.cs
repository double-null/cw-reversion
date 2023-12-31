using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000394 RID: 916
internal class StartData : MonoBehaviour
{
	// Token: 0x06001D60 RID: 7520 RVA: 0x0010294C File Offset: 0x00100B4C
	// Note: this type is marked as 'beforefieldinit'.
	static StartData()
	{
		StartData.UpdateForStatic = delegate()
		{
		};
	}

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06001D61 RID: 7521 RVA: 0x0010297C File Offset: 0x00100B7C
	// (remove) Token: 0x06001D62 RID: 7522 RVA: 0x00102994 File Offset: 0x00100B94
	public static event Action UpdateForStatic;

	// Token: 0x17000849 RID: 2121
	// (get) Token: 0x06001D63 RID: 7523 RVA: 0x001029AC File Offset: 0x00100BAC
	public static StartData Instance
	{
		get
		{
			if (StartData._instance == null)
			{
				StartData._instance = (StartData)UnityEngine.Object.FindObjectOfType(typeof(StartData));
			}
			return StartData._instance;
		}
	}

	// Token: 0x06001D64 RID: 7524 RVA: 0x001029E8 File Offset: 0x00100BE8
	private void Awake()
	{
		StartData._instance = this;
		UtilsScreen.Check();
		Shader.SetGlobalFloat("_Snow", 2f);
		Shader.SetGlobalFloat("_SnowFalling", 0f);
	}

	// Token: 0x06001D65 RID: 7525 RVA: 0x00102A14 File Offset: 0x00100C14
	private bool Empty()
	{
		return true;
	}

	// Token: 0x06001D66 RID: 7526 RVA: 0x00102A18 File Offset: 0x00100C18
	private void Update()
	{
		UtilsScreen.Check();
		StartData.UpdateForStatic();
	}

	// Token: 0x1700084A RID: 2122
	// (get) Token: 0x06001D67 RID: 7527 RVA: 0x00102A34 File Offset: 0x00100C34
	public static StartData.ThermalVision thermalVision
	{
		get
		{
			return StartData.Instance._thermalVision;
		}
	}

	// Token: 0x1700084B RID: 2123
	// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00102A40 File Offset: 0x00100C40
	public static StartData.Shaders shaders
	{
		get
		{
			return StartData.Instance._shaders;
		}
	}

	// Token: 0x1700084C RID: 2124
	// (get) Token: 0x06001D69 RID: 7529 RVA: 0x00102A4C File Offset: 0x00100C4C
	public static StartData.Binocular binocular
	{
		get
		{
			return StartData.Instance._binocular;
		}
	}

	// Token: 0x1700084D RID: 2125
	// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00102A58 File Offset: 0x00100C58
	public static StartData.Sun sun
	{
		get
		{
			return StartData.Instance._sun;
		}
	}

	// Token: 0x1700084E RID: 2126
	// (get) Token: 0x06001D6B RID: 7531 RVA: 0x00102A64 File Offset: 0x00100C64
	public static StartData.WeaponShaders weaponShaders
	{
		get
		{
			return StartData.Instance._weaponShaders;
		}
	}

	// Token: 0x04002204 RID: 8708
	private static StartData _instance;

	// Token: 0x04002205 RID: 8709
	private LinkedList<Material> mats;

	// Token: 0x04002206 RID: 8710
	public StartData.ThermalVision _thermalVision;

	// Token: 0x04002207 RID: 8711
	public StartData.Shaders _shaders;

	// Token: 0x04002208 RID: 8712
	public StartData.Binocular _binocular;

	// Token: 0x04002209 RID: 8713
	public StartData.Sun _sun;

	// Token: 0x0400220A RID: 8714
	public StartData.WeaponShaders _weaponShaders;

	// Token: 0x0400220B RID: 8715
	public Shader[] ShitCode;

	// Token: 0x02000395 RID: 917
	[Serializable]
	internal class ThermalVision
	{
		// Token: 0x0400220E RID: 8718
		public AnimationCurve blackFlashGoingToOn;

		// Token: 0x0400220F RID: 8719
		public AnimationCurve blackFlashGoingToOff;

		// Token: 0x04002210 RID: 8720
		public AnimationCurve HighlightImpulse;

		// Token: 0x04002211 RID: 8721
		public AudioClip SwitchOn;

		// Token: 0x04002212 RID: 8722
		public AudioClip SwitchOff;

		// Token: 0x04002213 RID: 8723
		public Texture mask;

		// Token: 0x04002214 RID: 8724
		public Shader ThermalShaderW;
	}

	// Token: 0x02000396 RID: 918
	[Serializable]
	internal class Shaders
	{
		// Token: 0x04002215 RID: 8725
		public Texture noise;

		// Token: 0x04002216 RID: 8726
		public Shader Tonemapper2;

		// Token: 0x04002217 RID: 8727
		public Shader Noise;
	}

	// Token: 0x02000397 RID: 919
	[Serializable]
	internal class Binocular
	{
		// Token: 0x04002218 RID: 8728
		public Texture2D Reticle;

		// Token: 0x04002219 RID: 8729
		public AnimationCurve blackFlashGoingToOn;

		// Token: 0x0400221A RID: 8730
		public AnimationCurve blackFlashGoingToOff;

		// Token: 0x0400221B RID: 8731
		public float GoingToOnTimeMax;

		// Token: 0x0400221C RID: 8732
		public float GoingToOffTimeMax;

		// Token: 0x0400221D RID: 8733
		public AudioClip SwitchOn;

		// Token: 0x0400221E RID: 8734
		public AudioClip SwitchOff;

		// Token: 0x0400221F RID: 8735
		public LayerMask LayerMask;
	}

	// Token: 0x02000398 RID: 920
	[Serializable]
	internal class Sun
	{
		// Token: 0x04002220 RID: 8736
		public Color SunColor;

		// Token: 0x04002221 RID: 8737
		public Texture ScreenTexture;

		// Token: 0x04002222 RID: 8738
		public Shader ScreenShader;

		// Token: 0x04002223 RID: 8739
		public Shader VisibilityCheckerShader;

		// Token: 0x04002224 RID: 8740
		public CustomLensFlare LensFlares;

		// Token: 0x04002225 RID: 8741
		public Texture SunBackTexture;

		// Token: 0x04002226 RID: 8742
		public AnimationCurve SunCurve;

		// Token: 0x04002227 RID: 8743
		public Shader SunShaftsShader;

		// Token: 0x04002228 RID: 8744
		public Shader SunShaftsClearShader;
	}

	// Token: 0x02000399 RID: 921
	[Serializable]
	internal class WeaponShaders
	{
		// Token: 0x06001D72 RID: 7538 RVA: 0x00102A9C File Offset: 0x00100C9C
		private void shadersCheck()
		{
			if (StartData.WeaponShaders._shaders == null)
			{
				StartData.WeaponShaders._shaders = new Dictionary<string, Shader>();
				foreach (StartData.WeaponShaders.ReplaceShader replaceShader in this.Shaders)
				{
					StartData.WeaponShaders._shaders.Add(replaceShader.Name, replaceShader.Shader);
				}
				StartData.WeaponShaders._invertShaders = new Dictionary<string, Shader>();
				foreach (StartData.WeaponShaders.ReplaceShader replaceShader2 in this.ShadersInvert)
				{
					StartData.WeaponShaders._invertShaders.Add(replaceShader2.Name, replaceShader2.Shader);
				}
			}
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00102B3C File Offset: 0x00100D3C
		private void ReplaceInWeapons(GameObject obj)
		{
			if (obj == null)
			{
				Debug.LogError("ReplaceInWeapons: obj = null");
				return;
			}
			this.shadersCheck();
			Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>(true);
			foreach (Renderer renderer in componentsInChildren)
			{
				renderer.castShadows = false;
				Material[] materials = renderer.materials;
				foreach (Material material in materials)
				{
					Shader shader;
					if (StartData.WeaponShaders._shaders.TryGetValue(material.shader.name, out shader))
					{
						material.shader = shader;
					}
					else if (!material.shader.name.EndsWith("W"))
					{
						Debug.Log("ReplaceInWeapons: shader " + material.shader.name + " not replaced");
					}
				}
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00102C24 File Offset: 0x00100E24
		private void ReplaceInArms(GameObject obj)
		{
			if (obj == null)
			{
				return;
			}
			this.shadersCheck();
			Renderer renderer = obj.renderer;
			renderer.castShadows = false;
			Material[] materials = renderer.materials;
			foreach (Material material in materials)
			{
				Shader shader;
				if (StartData.WeaponShaders._shaders.TryGetValue(material.shader.name, out shader))
				{
					material.shader = shader;
				}
				else if (material.shader.name[material.shader.name.Length - 1] != 'W')
				{
					Debug.Log("give oleg next line\n" + material.shader.name);
				}
			}
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00102CE4 File Offset: 0x00100EE4
		public void ReplaceEntity(GameObject obj)
		{
			this.ReplaceInWeapons(obj);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00102CF0 File Offset: 0x00100EF0
		public void ReplaceEntityInvert(GameObject obj)
		{
			if (obj == null)
			{
				return;
			}
			this.shadersCheck();
			Renderer[] componentsInChildren = obj.GetComponentsInChildren<Renderer>(true);
			foreach (Renderer renderer in componentsInChildren)
			{
				renderer.castShadows = false;
				Material[] materials = renderer.materials;
				foreach (Material material in materials)
				{
					Shader shader;
					if (StartData.WeaponShaders._invertShaders.TryGetValue(material.shader.name, out shader))
					{
						material.shader = shader;
					}
				}
			}
		}

		// Token: 0x04002229 RID: 8745
		public StartData.WeaponShaders.ReplaceShader[] Shaders;

		// Token: 0x0400222A RID: 8746
		public StartData.WeaponShaders.ReplaceShader[] ShadersInvert;

		// Token: 0x0400222B RID: 8747
		private static Dictionary<string, Shader> _shaders;

		// Token: 0x0400222C RID: 8748
		private static Dictionary<string, Shader> _invertShaders;

		// Token: 0x0200039A RID: 922
		[Serializable]
		internal class ReplaceShader
		{
			// Token: 0x0400222D RID: 8749
			public Shader Shader;

			// Token: 0x0400222E RID: 8750
			public string Name;
		}

		// Token: 0x0200039B RID: 923
		internal class Replace
		{
			// Token: 0x06001D7A RID: 7546 RVA: 0x00102DB4 File Offset: 0x00100FB4
			public void Init()
			{
				StartData.WeaponShaders.Replace._camera = SunOnGlass.SunOnGlassInstance.transform;
				Transform transform = GameObject.Find("Arms_root").transform;
				StartData.weaponShaders.ReplaceInWeapons(transform.gameObject);
				Camera[] componentsInChildren = transform.gameObject.GetComponentsInChildren<Camera>();
				if (componentsInChildren != null && componentsInChildren.Length != 0)
				{
					foreach (Camera camera in componentsInChildren)
					{
						if (camera.name == "optic_camera" && SingletoneForm<LevelSettings>.Instance.ModOpticCameraNearClipPlane != 0f)
						{
							camera.near = SingletoneForm<LevelSettings>.Instance.ModOpticCameraNearClipPlane;
						}
						else
						{
							camera.near = 0.02f;
						}
					}
				}
			}

			// Token: 0x06001D7B RID: 7547 RVA: 0x00102E74 File Offset: 0x00101074
			public static void SetWeaponAspect(float weaponAspect)
			{
				if (StartData.WeaponShaders.Replace._lastWeaponAspect == weaponAspect)
				{
					return;
				}
				Shader.SetGlobalVector("_WeaponScale", new Vector4(1f, 1f, weaponAspect, 1f));
				StartData.WeaponShaders.Replace._lastWeaponAspect = weaponAspect;
			}

			// Token: 0x06001D7C RID: 7548 RVA: 0x00102EA8 File Offset: 0x001010A8
			public static Vector3 ModifyVec(Vector3 vec)
			{
				vec = StartData.WeaponShaders.Replace._camera.InverseTransformPoint(vec);
				vec.z *= StartData.WeaponShaders.Replace._lastWeaponAspect;
				vec = StartData.WeaponShaders.Replace._camera.TransformPoint(vec);
				return vec;
			}

			// Token: 0x0400222F RID: 8751
			private static Transform _camera;

			// Token: 0x04002230 RID: 8752
			private static float _startZScale = 0.664f;

			// Token: 0x04002231 RID: 8753
			private static float _lastWeaponAspect = -1f;
		}
	}
}
