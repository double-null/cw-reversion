using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D5 RID: 469
internal class Thermal : MonoBehaviour
{
	// Token: 0x17000250 RID: 592
	// (get) Token: 0x06000FBB RID: 4027 RVA: 0x000B35A8 File Offset: 0x000B17A8
	public static bool Exist
	{
		get
		{
			return Thermal.Instance != null;
		}
	}

	// Token: 0x17000251 RID: 593
	// (set) Token: 0x06000FBC RID: 4028 RVA: 0x000B35B8 File Offset: 0x000B17B8
	public static bool HighLight
	{
		set
		{
			Thermal.Instance.highLighting.Enable = value;
		}
	}

	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000B35CC File Offset: 0x000B17CC
	public static bool On
	{
		get
		{
			return Thermal.Instance._on;
		}
	}

	// Token: 0x06000FBE RID: 4030 RVA: 0x000B35D8 File Offset: 0x000B17D8
	private void Awake()
	{
		Thermal.Instance = this;
		this.switcher = new StateSwitcher();
		this.tonemapping = base.gameObject.AddComponent<Tonemapping2>();
		this.tonemapping.enabled = false;
		this.tonemapping.Thermal = this;
		this.noise = base.gameObject.AddComponent<NoiseEffect>();
		this.noise.enabled = false;
		this.noise.Scale = 2f;
		this.noise.Intensity = 0.3f;
		this.lightenEffect = base.gameObject.GetComponent<LightenEffect>();
		this.colorCorrection = base.gameObject.GetComponent<ColorCorrectionCurves>();
		this.bloom = new Thermal.Bloom(base.gameObject);
		this.materials = new Thermal.Materials();
		this.lightMap = new Thermal.LightQuality();
		this.highLighting = new Thermal.HighLighting();
		this.mask = new GUIMaskTex(StartData.thermalVision.mask, true);
		this.gridd = new GUITiledTex(TexUtils.GetTexture(1, 2, new Color[]
		{
			new Color(0f, 0f, 0f, 0f),
			new Color(0f, 0f, 0f, 0.1f)
		}), 2f, false, true);
		this.flash = new GUIScreenTex(TexUtils.GetTexture(1, 1, new Color[]
		{
			Color.black
		}));
		this.switcher.SwitchingOn = StartData.thermalVision.blackFlashGoingToOn;
		this.switcher.SwitchingOff = StartData.thermalVision.blackFlashGoingToOff;
		this.switcher.AddCase(Thermal.getMaxValueTime(this.switcher.SwitchingOn), delegate
		{
			this.Switch(true);
		}, true);
		this.switcher.AddCase(Thermal.getMaxValueTime(this.switcher.SwitchingOff), delegate
		{
			this.Switch(false);
		}, false);
	}

	// Token: 0x06000FBF RID: 4031 RVA: 0x000B37D4 File Offset: 0x000B19D4
	private void OnGUI()
	{
		if (this._on)
		{
			this.highLighting.Update();
			this.gridd.Draw();
		}
		if (this.switcher.InProcess)
		{
			this.flash.Draw(this.switcher.Value);
		}
	}

	// Token: 0x06000FC0 RID: 4032 RVA: 0x000B3828 File Offset: 0x000B1A28
	public void DrawMask()
	{
		
	}

	// Token: 0x06000FC1 RID: 4033 RVA: 0x000B3840 File Offset: 0x000B1A40
	public static void StartSwitch(bool on)
	{
		if (Thermal.Instance == null)
		{
			return;
		}
		if (Thermal.Instance.switcher.InProcess)
		{
			return;
		}
		if (!Thermal.Instance.switcher.Switch(on))
		{
			return;
		}
		Audio2DPlayer.Play((!on) ? StartData.thermalVision.SwitchOff : StartData.thermalVision.SwitchOn);
	}

	// Token: 0x06000FC2 RID: 4034 RVA: 0x000B38B0 File Offset: 0x000B1AB0
	private bool Active()
	{
		if (this.switcher.InProcess)
		{
			return false;
		}
		if (!this.switcher.Switch(true))
		{
			return false;
		}
		Audio2DPlayer.Play(StartData.thermalVision.SwitchOn);
		return true;
	}

	// Token: 0x06000FC3 RID: 4035 RVA: 0x000B38F4 File Offset: 0x000B1AF4
	private bool Deactive()
	{
		if (this.switcher.InProcess)
		{
			return false;
		}
		if (!this.switcher.Switch(false))
		{
			return false;
		}
		Audio2DPlayer.Play(StartData.thermalVision.SwitchOff);
		return true;
	}

	// Token: 0x06000FC4 RID: 4036 RVA: 0x000B3938 File Offset: 0x000B1B38
	private void Update()
	{
		this.switcher.Process(Time.deltaTime);
		this.lightMap.CheckChanges();
	}

	// Token: 0x06000FC5 RID: 4037 RVA: 0x000B3958 File Offset: 0x000B1B58
	public static void ForceSwitchOff()
	{
		if (Thermal.Instance == null)
		{
			return;
		}
		Thermal.Instance.switcher.ForceSwitch(false);
		Thermal.Instance.Switch(false);
	}

	// Token: 0x06000FC6 RID: 4038 RVA: 0x000B3994 File Offset: 0x000B1B94
	private void Switch(bool on)
	{
		this.FindReferences();
		this.lightMap.Switch(!on);
		this.noise.enabled = on;
		this.bloom.Switch(on);
		this.materials.Switch(on);
		if (this.lightenEffect != null)
		{
			this.lightenEffect.enabled = !on;
		}
		if (Main.UserInfo.settings.graphics.PostEffects)
		{
			this.colorCorrection.enabled = !on;
		}
		this.tonemapping.enabled = on;
		this._on = on;
		this.highLighting.RefrashTransforms();
	}

	// Token: 0x06000FC7 RID: 4039 RVA: 0x000B3A40 File Offset: 0x000B1C40
	private void FindReferences()
	{
		this.bloom.FindReferences();
		this.materials.FindReferences();
		this.lightMap.FindReferences();
	}

	// Token: 0x06000FC8 RID: 4040 RVA: 0x000B3A64 File Offset: 0x000B1C64
	private static float getMaxValueTime(AnimationCurve curve)
	{
		float result = 0f;
		float num = float.MinValue;
		foreach (Keyframe keyframe in curve.keys)
		{
			if (keyframe.value > num)
			{
				result = keyframe.time;
				num = keyframe.value;
			}
		}
		return result;
	}

	// Token: 0x06000FC9 RID: 4041 RVA: 0x000B3AC8 File Offset: 0x000B1CC8
	public static void RefrashMaterials(GameObject obj)
	{
		if (Thermal.Instance == null)
		{
			return;
		}
		Thermal.Instance.materials.Refrash(obj);
	}

	// Token: 0x06000FCA RID: 4042 RVA: 0x000B3AEC File Offset: 0x000B1CEC
	public static void HighLightEntity(EntityNetPlayer player)
	{
		if (Thermal.Instance == null)
		{
			return;
		}
		Thermal.Instance.highLighting.AddPlayer(player);
	}

	// Token: 0x06000FCB RID: 4043 RVA: 0x000B3B10 File Offset: 0x000B1D10
	public static Thermal AddTo(GameObject gameObject)
	{
		return gameObject.GetComponent<Thermal>() ?? gameObject.AddComponent<Thermal>();
	}

	// Token: 0x04001032 RID: 4146
	private static Thermal Instance;

	// Token: 0x04001033 RID: 4147
	public float Offset = 0.04f;

	// Token: 0x04001034 RID: 4148
	public float Treshold = 1.5f;

	// Token: 0x04001035 RID: 4149
	private Shader ScreenShader;

	// Token: 0x04001036 RID: 4150
	private Material screenMaterial;

	// Token: 0x04001037 RID: 4151
	private Tonemapping2 tonemapping;

	// Token: 0x04001038 RID: 4152
	private NoiseEffect noise;

	// Token: 0x04001039 RID: 4153
	private Thermal.Bloom bloom;

	// Token: 0x0400103A RID: 4154
	private Thermal.Materials materials;

	// Token: 0x0400103B RID: 4155
	private Thermal.LightQuality lightMap;

	// Token: 0x0400103C RID: 4156
	private Thermal.HighLighting highLighting;

	// Token: 0x0400103D RID: 4157
	private LightenEffect lightenEffect;

	// Token: 0x0400103E RID: 4158
	private ColorCorrectionCurves colorCorrection;

	// Token: 0x0400103F RID: 4159
	private GUIMaskTex mask;

	// Token: 0x04001040 RID: 4160
	private GUIScreenTex flash;

	// Token: 0x04001041 RID: 4161
	private GUITiledTex gridd;

	// Token: 0x04001042 RID: 4162
	private StateSwitcher switcher;

	// Token: 0x04001043 RID: 4163
	private bool _on;

	// Token: 0x020001D6 RID: 470
	private class Bloom
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x000B3B40 File Offset: 0x000B1D40
		public Bloom(GameObject gameObject)
		{
			this.gameObject = gameObject;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000B3B50 File Offset: 0x000B1D50
		public void FindReferences()
		{
			if (this.bloom != null)
			{
				return;
			}
			this.bloom = this.gameObject.GetComponent<BloomAndLensFlares>();
			this.savedIntensity = this.bloom.bloomIntensity;
			this.savedThreshhold = this.bloom.bloomThreshhold;
			this.savedSpread = this.bloom.sepBlurSpread;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000B3BB8 File Offset: 0x000B1DB8
		public void Switch(bool on)
		{
			if (Main.UserInfo.settings.graphics.PostEffects)
			{
				this.bloom.enabled = !on;
			}
			if (on)
			{
				this.bloom.bloomIntensity = 0.36f;
				this.bloom.bloomThreshhold = -0.05f;
				this.bloom.sepBlurSpread = 5.53f;
			}
			else
			{
				this.bloom.bloomIntensity = this.savedIntensity;
				this.bloom.bloomThreshhold = this.savedThreshhold;
				this.bloom.sepBlurSpread = this.savedSpread;
			}
		}

		// Token: 0x04001044 RID: 4164
		private BloomAndLensFlares bloom;

		// Token: 0x04001045 RID: 4165
		private GameObject gameObject;

		// Token: 0x04001046 RID: 4166
		private float savedIntensity;

		// Token: 0x04001047 RID: 4167
		private float savedThreshhold;

		// Token: 0x04001048 RID: 4168
		private float savedSpread;
	}

	// Token: 0x020001D7 RID: 471
	private class Materials
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x000B3C5C File Offset: 0x000B1E5C
		public Materials()
		{
			Thermal.Materials.RendererSwitcher._bearMaterials = null;
			Thermal.Materials.RendererSwitcher._bearMaterials1 = null;
			Thermal.Materials.RendererSwitcher._usecMaterials = null;
			Thermal.Materials.RendererSwitcher._usecMaterials1 = null;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000B3C7C File Offset: 0x000B1E7C
		private void FindAndOff()
		{
			string[] array = new string[]
			{
				"p0/Detailed Bumped Specular",
				"p0/Reflective/Bumped Specular",
				"p0/Reflective/Specular",
				"p0/Bumped Specular",
				"p0/Specular",
				"p0/Transparent/Reflective/Specular",
				"Transparent/Cutout/Bumped Specular",
				"Transparent/Cutout/Specular",
				"Reflective/Bumped Specular"
			};
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000B3CDC File Offset: 0x000B1EDC
		public void FindReferences()
		{
			if (this.ThermalMat == null)
			{
				this.ThermalMat = new Material("Shader \"Unlit a7efd2 \" { SubShader { Pass { Lighting Off Color (1,1,1,0) }}}");
				this.ThermalMatW = new Material(StartData.thermalVision.ThermalShaderW);
			}
			if (this.renderers == null)
			{
				this.renderers = new Dictionary<Renderer, Thermal.Materials.RendererSwitcher>();
				if (!this.FindBodies())
				{
					this.renderers = null;
					return;
				}
			}
			if (this.renderers2 == null)
			{
				this.renderers2 = new Dictionary<Renderer, Thermal.Materials.RendererSwitcher2>();
			}
			this.FindWeapons();
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000B3D68 File Offset: 0x000B1F68
		private bool FindBodies()
		{
			GameObject gameObject = GameObject.Find("Arms");
			if (gameObject == null)
			{
				return false;
			}
			Renderer renderer = gameObject.renderer;
			if (renderer == null)
			{
				return false;
			}
			this.renderers.Add(renderer, new Thermal.Materials.RendererSwitcher(renderer, this.ThermalMatW));
			SkinnedMeshRenderer[] array = (SkinnedMeshRenderer[])UnityEngine.Object.FindObjectsOfType(typeof(SkinnedMeshRenderer));
			if (array.Length == 0)
			{
				return false;
			}
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in array)
			{
				if (!this.renderers.ContainsKey(skinnedMeshRenderer))
				{
					this.renderers.Add(skinnedMeshRenderer, new Thermal.Materials.RendererSwitcher(skinnedMeshRenderer, (!(skinnedMeshRenderer.name == "Arms")) ? this.ThermalMat : this.ThermalMatW));
				}
			}
			return true;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000B3E4C File Offset: 0x000B204C
		private bool FindWeapons()
		{
			if (this._bamm == null)
			{
				this._bamm = Peer.ClientGame.LocalPlayer.Ammo;
			}
			if (this._bamm == null)
			{
				return false;
			}
			UnityEngine.Object[] array = new UnityEngine.Object[]
			{
				this._bamm.cPrimary,
				this._bamm.cSecondary
			};
			if (array.Length == 0)
			{
				return false;
			}
			foreach (UnityEngine.Object @object in array)
			{
				if (!(@object == null))
				{
					GameObject gameObject = ((ClientWeapon)@object).gameObject;
					if (gameObject.active)
					{
						Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>(false);
						for (int j = 0; j < componentsInChildren.Length; j++)
						{
							if (!this.renderers2.ContainsKey(componentsInChildren[j]))
							{
								this.renderers2.Add(componentsInChildren[j], new Thermal.Materials.RendererSwitcher2(componentsInChildren[j], new string[]
								{
									"_SpecColor",
									"_ReflectColor"
								}, new Color[]
								{
									Color.black,
									Color.black
								}));
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000B3FA0 File Offset: 0x000B21A0
		public void Refrash(GameObject obj)
		{
			SkinnedMeshRenderer[] componentsInChildren = obj.GetComponentsInChildren<SkinnedMeshRenderer>();
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in componentsInChildren)
			{
				if (!this.renderers.ContainsKey(skinnedMeshRenderer))
				{
					if (skinnedMeshRenderer.material == null)
					{
						return;
					}
					Thermal.Materials.RendererSwitcher rendererSwitcher = new Thermal.Materials.RendererSwitcher(skinnedMeshRenderer, this.ThermalMat);
					rendererSwitcher.Switch(Thermal.Instance._on);
					this.renderers.Add(skinnedMeshRenderer, rendererSwitcher);
				}
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000B4024 File Offset: 0x000B2224
		public void Switch(bool on)
		{
			if (this.renderers == null || this.renderers2 == null)
			{
				return;
			}
			foreach (KeyValuePair<Renderer, Thermal.Materials.RendererSwitcher> keyValuePair in this.renderers)
			{
				keyValuePair.Value.Switch(on);
			}
			foreach (KeyValuePair<Renderer, Thermal.Materials.RendererSwitcher2> keyValuePair2 in this.renderers2)
			{
				keyValuePair2.Value.Switch(on);
			}
		}

		// Token: 0x04001049 RID: 4169
		private Material ThermalMat;

		// Token: 0x0400104A RID: 4170
		private Material ThermalMatW;

		// Token: 0x0400104B RID: 4171
		private Dictionary<Renderer, Thermal.Materials.RendererSwitcher> renderers;

		// Token: 0x0400104C RID: 4172
		private Dictionary<Renderer, Thermal.Materials.RendererSwitcher2> renderers2;

		// Token: 0x0400104D RID: 4173
		private BaseAmmunitions _bamm;

		// Token: 0x020001D8 RID: 472
		private class RendererSwitcher
		{
			// Token: 0x06000FD8 RID: 4056 RVA: 0x000B4104 File Offset: 0x000B2304
			public RendererSwitcher(Renderer r, Material material)
			{
				this._renderer = r;
				this._savedMaterials = this._renderer.materials;
				string name = r.transform.parent.name;
				bool flag = r.name == "LOD0";
				if (name == "client_bear")
				{
					if (flag)
					{
						if (Thermal.Materials.RendererSwitcher._bearMaterials == null)
						{
							Thermal.Materials.RendererSwitcher._bearMaterials = this._savedMaterials;
						}
						else
						{
							this._savedMaterials = Thermal.Materials.RendererSwitcher._bearMaterials;
						}
					}
					else if (Thermal.Materials.RendererSwitcher._bearMaterials1 == null)
					{
						Thermal.Materials.RendererSwitcher._bearMaterials1 = this._savedMaterials;
					}
					else
					{
						this._savedMaterials = Thermal.Materials.RendererSwitcher._bearMaterials1;
					}
				}
				else if (name == "client_usec")
				{
					if (flag)
					{
						if (Thermal.Materials.RendererSwitcher._usecMaterials == null)
						{
							Thermal.Materials.RendererSwitcher._usecMaterials = this._savedMaterials;
						}
						else
						{
							this._savedMaterials = Thermal.Materials.RendererSwitcher._usecMaterials;
						}
					}
					else if (Thermal.Materials.RendererSwitcher._usecMaterials1 == null)
					{
						Thermal.Materials.RendererSwitcher._usecMaterials1 = this._savedMaterials;
					}
					else
					{
						this._savedMaterials = Thermal.Materials.RendererSwitcher._usecMaterials1;
					}
				}
				this._newMaterials = new Material[this._savedMaterials.Length];
				for (int i = 0; i < this._newMaterials.Length; i++)
				{
					this._newMaterials[i] = material;
				}
			}

			// Token: 0x06000FD9 RID: 4057 RVA: 0x000B4258 File Offset: 0x000B2458
			public void Switch(bool on)
			{
				foreach (Material material in this._renderer.sharedMaterials)
				{
					if (material.name.Contains("client_fake"))
					{
						return;
					}
				}
				this._renderer.materials = ((!on) ? this._savedMaterials : this._newMaterials);
			}

			// Token: 0x0400104E RID: 4174
			public static Material[] _bearMaterials;

			// Token: 0x0400104F RID: 4175
			public static Material[] _usecMaterials;

			// Token: 0x04001050 RID: 4176
			public static Material[] _bearMaterials1;

			// Token: 0x04001051 RID: 4177
			public static Material[] _usecMaterials1;

			// Token: 0x04001052 RID: 4178
			private Renderer _renderer;

			// Token: 0x04001053 RID: 4179
			private Material[] _savedMaterials;

			// Token: 0x04001054 RID: 4180
			private Material[] _newMaterials;
		}

		// Token: 0x020001D9 RID: 473
		private class RendererSwitcher2
		{
			// Token: 0x06000FDA RID: 4058 RVA: 0x000B42C4 File Offset: 0x000B24C4
			public RendererSwitcher2(Renderer r, string[] propertys, Color[] colors)
			{
				this.renderer = r;
				Material[] array = this.renderer.materials;
				this.materials = new Thermal.Materials.RendererSwitcher2.MaterialSwitcher[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					this.materials[i] = new Thermal.Materials.RendererSwitcher2.MaterialSwitcher(array[i], propertys, colors);
				}
			}

			// Token: 0x06000FDB RID: 4059 RVA: 0x000B4320 File Offset: 0x000B2520
			public void Switch(bool on)
			{
				for (int i = 0; i < this.materials.Length; i++)
				{
					this.materials[i].Switch(on);
				}
			}

			// Token: 0x04001055 RID: 4181
			private Renderer renderer;

			// Token: 0x04001056 RID: 4182
			private Thermal.Materials.RendererSwitcher2.MaterialSwitcher[] materials;

			// Token: 0x020001DA RID: 474
			private class MaterialSwitcher
			{
				// Token: 0x06000FDC RID: 4060 RVA: 0x000B4354 File Offset: 0x000B2554
				public MaterialSwitcher(Material material, string[] propertys, Color[] colors)
				{
					this._material = material;
					int num = 0;
					for (int i = 0; i < propertys.Length; i++)
					{
						if (material.HasProperty(propertys[i]))
						{
							num++;
						}
					}
					this._names = new string[num];
					this._savedColors = new Color[num];
					this._newColors = new Color[num];
					num = 0;
					for (int j = 0; j < propertys.Length; j++)
					{
						if (material.HasProperty(propertys[j]))
						{
							this._names[num] = propertys[j];
							this._savedColors[num] = material.GetColor(propertys[j]);
							this._newColors[num] = colors[j];
							num++;
						}
					}
				}

				// Token: 0x06000FDD RID: 4061 RVA: 0x000B4424 File Offset: 0x000B2624
				public void Switch(bool on)
				{
					for (int i = 0; i < this._names.Length; i++)
					{
						this._material.SetColor(this._names[i], (!on) ? this._savedColors[i] : this._newColors[i]);
					}
				}

				// Token: 0x04001057 RID: 4183
				private Material _material;

				// Token: 0x04001058 RID: 4184
				private string[] _names;

				// Token: 0x04001059 RID: 4185
				private Color[] _savedColors;

				// Token: 0x0400105A RID: 4186
				private Color[] _newColors;
			}
		}
	}

	// Token: 0x020001DB RID: 475
	private class LightQuality
	{
		// Token: 0x06000FDF RID: 4063 RVA: 0x000B449C File Offset: 0x000B269C
		public void FindReferences()
		{
			if (this.sun == null)
			{
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Light));
				for (int i = 0; i < array.Length; i++)
				{
					if (this.sun != null)
					{
						break;
					}
					if (array[i].name == "sun")
					{
						this.sun = (Light)array[i];
					}
					else if (array[i].name == "sunSecond")
					{
						this.sunSecond = (Light)array[i];
					}
				}
			}
			if (this.BlackSkybox == null)
			{
				this.BlackSkybox = new Material(Shader.Find("RenderFX/Skybox Cubed"));
				this.BlackSkybox.SetColor("_Tint", Color.black);
			}
			if (this.savedSkybox == null)
			{
				this.savedSkybox = RenderSettings.skybox;
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000B459C File Offset: 0x000B279C
		public void CheckChanges()
		{
			if (this._on)
			{
				return;
			}
			if (this.sun == null)
			{
				return;
			}
			if (RenderSettings.skybox != this.BlackSkybox)
			{
				this.Off();
			}
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000B45D8 File Offset: 0x000B27D8
		public void Switch(bool on)
		{
			this._on = on;
			if (on)
			{
				this.On();
			}
			else
			{
				this.Off();
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000B45F8 File Offset: 0x000B27F8
		private void On()
		{
			LevelSettings.OnProfileChanged(true);
			Main.UserInfo.settings.graphics.SetLightingQuality();
			SunOnGlass.UpdateSettings();
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000B461C File Offset: 0x000B281C
		private void Off()
		{
			if (PrefabFactory.Terrain)
			{
				PrefabFactory.Terrain.lightmapIndex = 255;
			}
			for (int i = 0; i < PrefabFactory.Renderers.Length; i++)
			{
				if (PrefabFactory.RenderersIndex[i] != -1)
				{
					PrefabFactory.Renderers[i].lightmapIndex = 255;
				}
			}
			if (this.sun != null)
			{
				this.sun.enabled = false;
			}
			if (this.sunSecond != null)
			{
				this.sunSecond.enabled = false;
			}
			RenderSettings.skybox = this.BlackSkybox;
			RenderSettings.fogColor = Color.black;
			RenderSettings.fogDensity = 0f;
			RenderSettings.ambientLight = new Color32(125, 147, 160, byte.MaxValue);
			SunOnGlass.SunOnGlassInstance.enabled = false;
			SunOnGlass.SunShaftsInstance.enabled = false;
		}

		// Token: 0x0400105B RID: 4187
		private Material BlackSkybox;

		// Token: 0x0400105C RID: 4188
		private Material savedSkybox;

		// Token: 0x0400105D RID: 4189
		private Light sun;

		// Token: 0x0400105E RID: 4190
		private Light sunSecond;

		// Token: 0x0400105F RID: 4191
		private Quality lastRenderQuality;

		// Token: 0x04001060 RID: 4192
		private bool _on;

		// Token: 0x04001061 RID: 4193
		private int ferstLightmapedRenderer = -1;
	}

	// Token: 0x020001DC RID: 476
	private class HighLighting
	{
		// Token: 0x06000FE4 RID: 4068 RVA: 0x000B4744 File Offset: 0x000B2944
		public HighLighting()
		{
			RadarGUI radarGUI = (RadarGUI)UnityEngine.Object.FindObjectOfType(typeof(RadarGUI));
			this._hotspot = radarGUI.hotspot;
			this._impulse = StartData.thermalVision.HighlightImpulse;
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x000B47BC File Offset: 0x000B29BC
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x000B47B0 File Offset: 0x000B29B0
		public bool Enable { get; set; }

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000B47C4 File Offset: 0x000B29C4
		public void AddPlayer(EntityNetPlayer player)
		{
			if (!this.Enable || player == null || player.Controller == null)
			{
				return;
			}
			if (this._players.ContainsKey(player))
			{
				if (player.IsBear == this._bear && !player.isPlayer)
				{
					this._players[player] = new Thermal.HighLighting.Player(player.Controller.spine);
				}
				else
				{
					this._players.Remove(player);
				}
			}
			else if (player.IsBear == this._bear && !player.isPlayer)
			{
				this._players.Add(player, new Thermal.HighLighting.Player(player.Controller.spine));
			}
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000B4894 File Offset: 0x000B2A94
		public void RefrashTransforms()
		{
			List<EntityNetPlayer> allPlayers = Peer.ClientGame.AllPlayers;
			foreach (EntityNetPlayer player in allPlayers)
			{
				this.AddPlayer(player);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000B4900 File Offset: 0x000B2B00
		public void Update()
		{
			if (!this.Enable || !Main.IsTeamGame)
			{
				return;
			}
			if (this._bear != Main.LocalPlayer.IsBear)
			{
				this._bear = Main.LocalPlayer.IsBear;
				this.RefrashTransforms();
			}
			if (this._camera == null)
			{
				this._camera = (Camera.main ?? Camera.mainCamera);
			}
			Vector3 position = this._camera.transform.position;
			foreach (KeyValuePair<EntityNetPlayer, Thermal.HighLighting.Player> keyValuePair in this._players)
			{
				if (!keyValuePair.Key.IsSpectactor)
				{
					Thermal.HighLighting.Player value = keyValuePair.Value;
					Vector3 vector = value.Tr.TransformPoint(this._vec);
					Vector3 vector2 = this._camera.WorldToScreenPoint(vector);
					if (vector2.z >= 0f)
					{
						if (!Physics.Linecast(vector, position, 4458496))
						{
							GUI.color = new Color(1f, 1f, 1f, this._impulse.Evaluate(Time.time + value.RndVal));
							int num = (int)(1000f / (vector - position).magnitude);
							int num2 = num >> 1;
							GUI.DrawTexture(new Rect(vector2.x - (float)num2, (float)Screen.height - vector2.y - (float)num2, (float)num, (float)num), this._hotspot);
						}
					}
				}
			}
			GUI.color = Color.white;
		}

		// Token: 0x04001062 RID: 4194
		private Dictionary<EntityNetPlayer, Thermal.HighLighting.Player> _players = new Dictionary<EntityNetPlayer, Thermal.HighLighting.Player>();

		// Token: 0x04001063 RID: 4195
		private Vector3 _vec = new Vector3(-0.34f, 0f, -0.15f);

		// Token: 0x04001064 RID: 4196
		private bool _bear;

		// Token: 0x04001065 RID: 4197
		private int _lastCount;

		// Token: 0x04001066 RID: 4198
		private Camera _camera;

		// Token: 0x04001067 RID: 4199
		private Texture _hotspot;

		// Token: 0x04001068 RID: 4200
		private AnimationCurve _impulse;

		// Token: 0x020001DD RID: 477
		private class Player
		{
			// Token: 0x06000FEA RID: 4074 RVA: 0x000B4AD8 File Offset: 0x000B2CD8
			public Player(Transform tr)
			{
				this.Tr = tr;
				int num = this.Tr.GetHashCode();
				num = ((num <= 0) ? (-num) : num);
				this.RndVal = (float)(num % 8483) * 0.02351f;
				this.RndVal %= 1f;
			}

			// Token: 0x0400106A RID: 4202
			public Transform Tr;

			// Token: 0x0400106B RID: 4203
			public float RndVal;
		}
	}
}
