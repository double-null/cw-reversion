using System;
using UnityEngine;

// Token: 0x020001D3 RID: 467
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/SunOnGlass")]
[ExecuteInEditMode]
internal class SunOnGlass : MonoBehaviour
{
	// Token: 0x1700024D RID: 589
	// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x000B26D0 File Offset: 0x000B08D0
	public static SunOnGlass SunOnGlassInstance
	{
		get
		{
			if (SunOnGlass._sunOnGlassInstance == null)
			{
				SunOnGlass._sunOnGlassInstance = (SunOnGlass)UnityEngine.Object.FindObjectOfType(typeof(SunOnGlass));
			}
			return SunOnGlass._sunOnGlassInstance;
		}
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x000B270C File Offset: 0x000B090C
	public static SunShafts SunShaftsInstance
	{
		get
		{
			if (SunOnGlass._sunShaftsInstance == null)
			{
				SunOnGlass._sunShaftsInstance = (SunShafts)UnityEngine.Object.FindObjectOfType(typeof(SunShafts));
			}
			return SunOnGlass._sunShaftsInstance;
		}
	}

	// Token: 0x1700024F RID: 591
	// (set) Token: 0x06000FA4 RID: 4004 RVA: 0x000B2748 File Offset: 0x000B0948
	public int Settings
	{
		set
		{
			switch (value)
			{
			case 0:
				this._sunScratchesEnabled = false;
				this._lensFlaresEnabled = false;
				this._sunBackEnabled = true;
				break;
			case 1:
				this._sunScratchesEnabled = false;
				this._lensFlaresEnabled = true;
				this._sunBackEnabled = true;
				break;
			case 2:
				this._sunScratchesEnabled = true;
				this._lensFlaresEnabled = true;
				this._sunBackEnabled = true;
				break;
			default:
				this._sunScratchesEnabled = true;
				this._lensFlaresEnabled = true;
				this._sunBackEnabled = true;
				break;
			}
		}
	}

	// Token: 0x06000FA5 RID: 4005 RVA: 0x000B27D8 File Offset: 0x000B09D8
	public void Start()
	{
		SunOnGlass._sunOnGlassInstance = this;
		if (!SystemInfo.supportsImageEffects || SystemInfo.graphicsShaderLevel < 30)
		{
			base.enabled = false;
			return;
		}
		if (this.ScreenShader == null)
		{
			Debug.Log("Shader are not set up! Disabling effect.");
			base.enabled = false;
		}
		this._camera = base.camera;
		this._cameraTransform = this._camera.transform;
		base.camera.depthTextureMode = DepthTextureMode.Depth;
		this.LensFlares.Initialize();
		this._fallofTex = SunOnGlass.GetTexFromCurve(this.SunCurve, 255);
	}

	// Token: 0x06000FA6 RID: 4006 RVA: 0x000B2878 File Offset: 0x000B0A78
	private void Update()
	{
		if (this._sunBackEnabled)
		{
			this.SunBackGround.UpdatePos(base.transform.position);
		}
	}

	// Token: 0x06000FA7 RID: 4007 RVA: 0x000B289C File Offset: 0x000B0A9C
	private Material GetVisibilityCheckerMaterial()
	{
		if (this._materialvc == null)
		{
			this._materialvc = new Material(this.VisibilityCheckerShader);
			this._materialvc.hideFlags = HideFlags.HideAndDontSave;
		}
		return this._materialvc;
	}

	// Token: 0x06000FA8 RID: 4008 RVA: 0x000B28D4 File Offset: 0x000B0AD4
	private Material GetMaterial()
	{
		if (this._material == null)
		{
			this._material = new Material(this.ScreenShader);
			this._material.hideFlags = HideFlags.HideAndDontSave;
			this._material.SetTexture("_ScreenTex", this.ScreenTexture);
			this._material.SetTexture("_FallofTex", this._fallofTex);
		}
		return this._material;
	}

	// Token: 0x06000FA9 RID: 4009 RVA: 0x000B2944 File Offset: 0x000B0B44
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Vector2 positionOnScreen;
		if ((!this._sunScratchesEnabled && !this._lensFlaresEnabled) || !this.LightVisible(-this.Sun.forward, out positionOnScreen))
		{
			Graphics.Blit(source, destination);
			return;
		}
		Vector4 vector = new Vector4(positionOnScreen.x, positionOnScreen.y, (float)Screen.width / (float)Screen.height, this.Size);
		this.VisibilityChecker(source, destination, vector);
		if (this._sunScratchesEnabled)
		{
			Material material = this.GetMaterial();
			Shader.SetGlobalColor("_SunColor", SunOnGlass.ColorCorrector(this.SunColor));
			material.SetVector("_SunPos", vector);
			if (Application.isEditor)
			{
				this._material.SetTexture("_ScreenTex", this.ScreenTexture);
				this._material.SetTexture("_FallofTex", this._fallofTex);
			}
			Graphics.Blit(source, destination, material);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
		if (this._lensFlaresEnabled)
		{
			this.LensFlares.Draw(positionOnScreen);
		}
	}

	// Token: 0x06000FAA RID: 4010 RVA: 0x000B2A54 File Offset: 0x000B0C54
	private void OnDestroy()
	{
		if (this.SunBackGround != null && this.SunBackGround.GetSun())
		{
			UnityEngine.Object.DestroyImmediate(this.SunBackGround.GetSun().gameObject);
		}
	}

	// Token: 0x06000FAB RID: 4011 RVA: 0x000B2A98 File Offset: 0x000B0C98
	private void OnApplicationQuit()
	{
		UnityEngine.Object.DestroyImmediate(this.SunBackGround.GetSun().gameObject);
	}

	// Token: 0x06000FAC RID: 4012 RVA: 0x000B2AB0 File Offset: 0x000B0CB0
	private void VisibilityChecker(RenderTexture source, RenderTexture destination, Vector4 sunPos)
	{
		Material visibilityCheckerMaterial = this.GetVisibilityCheckerMaterial();
		visibilityCheckerMaterial.SetVector("_SunPos", sunPos);
		if (this._vc1 == null)
		{
			this._vc1 = new RenderTexture(4, 4, 0);
		}
		if (this._vc2 == null)
		{
			this._vc2 = new RenderTexture(1, 1, 0);
			Shader.SetGlobalTexture("_SunVisible", this._vc2);
		}
		Graphics.Blit(source, destination, visibilityCheckerMaterial, 2);
		Graphics.Blit(destination, this._vc1, visibilityCheckerMaterial, 0);
		Graphics.Blit(this._vc1, this._vc2, visibilityCheckerMaterial, 1);
	}

	// Token: 0x06000FAD RID: 4013 RVA: 0x000B2B48 File Offset: 0x000B0D48
	private static Color ColorCorrector(Color color)
	{
		float num = 1f / ((color.r + color.g + color.b) / 3f);
		color.a = num - 1f;
		return color;
	}

	// Token: 0x06000FAE RID: 4014 RVA: 0x000B2B88 File Offset: 0x000B0D88
	private bool LightVisible(Vector3 lightDirection, out Vector2 onScreen)
	{
		Vector3 position = this._cameraTransform.position;
		Vector3 v = this._camera.WorldToViewportPoint(position + lightDirection);
		onScreen = v;
		return v.z >= 0f && this.LightVisibleOnScreen(v);
	}

	// Token: 0x06000FAF RID: 4015 RVA: 0x000B2BE8 File Offset: 0x000B0DE8
	private bool LightVisibleOnScreen(Vector2 lightPos)
	{
		return lightPos.x >= -0.2f && lightPos.x <= 1.2f && lightPos.y >= -0.2f && lightPos.y <= 1.2f;
	}

	// Token: 0x06000FB0 RID: 4016 RVA: 0x000B2C44 File Offset: 0x000B0E44
	public static Texture2D GetTexFromCurve(AnimationCurve curve, int width = 255)
	{
		Texture2D texture2D = new Texture2D(width, 1, TextureFormat.Alpha8, false);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		float num = 1f / (float)width;
		float num2 = 0f;
		for (int i = 0; i < width; i++)
		{
			float num3 = Mathf.Clamp(curve.Evaluate(num2), 0f, 1f);
			texture2D.SetPixel(i, 0, new Color(num3, num3, num3, num3));
			num2 += num;
		}
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000FB1 RID: 4017 RVA: 0x000B2CBC File Offset: 0x000B0EBC
	public static void AddTo(GameObject obj)
	{
		Transform transform = null;
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Light));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].name == "sun")
			{
				transform = ((Light)array[i]).transform;
				break;
			}
		}
		SunOnGlass.UnicMapSunlightCorrection(transform);
		LevelSettings instance = SingletoneForm<LevelSettings>.Instance;
		obj.camera.depthTextureMode = DepthTextureMode.Depth;
		SunOnGlass sunOnGlass = obj.AddComponent<SunOnGlass>();
		sunOnGlass.SunColor = instance.SunColor;
		sunOnGlass.Size = instance.SunScratchesIntensity;
		sunOnGlass.Sun = transform;
		sunOnGlass.ScreenTexture = StartData.sun.ScreenTexture;
		sunOnGlass.ScreenShader = StartData.sun.ScreenShader;
		sunOnGlass.VisibilityCheckerShader = StartData.sun.VisibilityCheckerShader;
		sunOnGlass.SunCurve = StartData.sun.SunCurve;
		sunOnGlass.LensFlares = StartData.sun.LensFlares;
		sunOnGlass.SunBackGround = new SunOnGlass.SunBack();
		sunOnGlass.SunBackGround.Light = transform;
		sunOnGlass.SunBackGround.SunTexture = StartData.sun.SunBackTexture;
		sunOnGlass.SunBackGround.SunColor = sunOnGlass.SunColor;
		sunOnGlass.SunBackGround.size = 200f;
		sunOnGlass.SunBackGround.Initialize(false);
		SunShafts sunShafts = obj.AddComponent<SunShafts>();
		sunShafts.resolution = SunShaftsResolution.Low;
		sunShafts.screenBlendMode = ShaftsScreenBlendMode.Screen;
		sunShafts.sunTransform = sunOnGlass.SunBackGround.GetSun();
		sunShafts.sunColor = StartData.sun.SunColor;
		sunShafts.maxRadius = instance.SunShaftsDensity;
		sunShafts.sunShaftBlurRadius = 3.5f;
		sunShafts.radialBlurIterations = 2;
		sunShafts.sunShaftIntensity = instance.SunShaftsIntensity;
		sunShafts.useSkyBoxAlpha = -30f;
		sunShafts.sunShaftsShader = StartData.sun.SunShaftsShader;
		sunShafts.simpleClearShader = StartData.sun.SunShaftsClearShader;
		sunShafts.CheckResources();
		if (!instance.SunLensFlares)
		{
			sunOnGlass.LensFlares.FlareMaterial = null;
		}
		sunShafts.sunTransform.renderer.enabled = instance.SunBackGroung;
		SunOnGlass.UnicMapSunSettings(sunOnGlass, sunShafts);
	}

	// Token: 0x06000FB2 RID: 4018 RVA: 0x000B2EE8 File Offset: 0x000B10E8
	private static void UnicMapSunSettings(SunOnGlass sunOnGlass, SunShafts sunShafts)
	{
		string mapName = Peer.Info.MapName;
		switch (mapName)
		{
		case "station":
			sunOnGlass.SunColor = Color.white;
			sunOnGlass.Size = 2f;
			sunOnGlass.LensFlares.FlareMaterial = null;
			sunShafts.sunColor = Color.white;
			sunShafts.sunTransform.renderer.enabled = false;
			sunShafts.sunShaftIntensity = 0.09f;
			sunShafts.sunShaftBlurRadius = 4.05f;
			sunShafts.useSkyBoxAlpha = -30f;
			sunShafts.maxRadius = 0.3f;
			break;
		case "evac":
			sunOnGlass.SunColor = new Color32(byte.MaxValue, 180, 145, 244);
			sunOnGlass.Size = 8f;
			sunShafts.sunColor = sunOnGlass.SunColor;
			sunShafts.sunShaftIntensity = 0.25f;
			sunShafts.sunShaftBlurRadius = 4f;
			sunShafts.useSkyBoxAlpha = -20f;
			sunShafts.maxRadius = 0.25f;
			break;
		case "bay7":
			sunOnGlass.SunColor = Color.white;
			sunOnGlass.Size = 1f;
			sunOnGlass.LensFlares.FlareMaterial = null;
			sunShafts.sunColor = Color.white;
			sunShafts.sunTransform.renderer.enabled = false;
			sunShafts.sunShaftIntensity = 0.04f;
			sunShafts.sunShaftBlurRadius = 4.05f;
			sunShafts.useSkyBoxAlpha = -30f;
			sunShafts.maxRadius = 0.3f;
			break;
		}
	}

	// Token: 0x06000FB3 RID: 4019 RVA: 0x000B30B4 File Offset: 0x000B12B4
	private static void UnicMapSunlightCorrection(Transform sun)
	{
		string mapName = Peer.Info.MapName;
		switch (mapName)
		{
		case "station":
			sun.eulerAngles = new Vector3(37.5f, 272.3f);
			break;
		case "bay7":
			sun.eulerAngles = new Vector3(36.8f, 272f);
			break;
		}
	}

	// Token: 0x06000FB4 RID: 4020 RVA: 0x000B3170 File Offset: 0x000B1370
	public static void UpdateSettings()
	{
		SunOnGlass sunOnGlassInstance = SunOnGlass.SunOnGlassInstance;
		SunShafts sunShaftsInstance = SunOnGlass.SunShaftsInstance;
		if (Main.UserInfo.settings.graphics.PostEffects && Main.UserInfo.settings.graphics.Level != QualityLevelUser.VeryLow)
		{
			sunOnGlassInstance.enabled = true;
			sunShaftsInstance.enabled = true;
			sunOnGlassInstance.Settings = 2;
			if (Main.UserInfo.settings.graphics.LightingQ == Quality.high)
			{
				sunShaftsInstance.resolution = SunShaftsResolution.Normal;
			}
			else if (Main.UserInfo.settings.graphics.LightingQ == Quality.max)
			{
				sunShaftsInstance.resolution = SunShaftsResolution.Normal;
			}
			else
			{
				sunShaftsInstance.resolution = SunShaftsResolution.Low;
			}
		}
		else
		{
			sunOnGlassInstance.enabled = true;
			sunOnGlassInstance.Settings = ((Main.UserInfo.settings.graphics.Level == QualityLevelUser.VeryLow) ? 0 : 1);
			sunShaftsInstance.enabled = false;
		}
	}

	// Token: 0x04001013 RID: 4115
	private static SunOnGlass _sunOnGlassInstance;

	// Token: 0x04001014 RID: 4116
	private static SunShafts _sunShaftsInstance;

	// Token: 0x04001015 RID: 4117
	public float Size = 1f;

	// Token: 0x04001016 RID: 4118
	public Transform Sun;

	// Token: 0x04001017 RID: 4119
	public Texture ScreenTexture;

	// Token: 0x04001018 RID: 4120
	public Shader ScreenShader;

	// Token: 0x04001019 RID: 4121
	public Shader VisibilityCheckerShader;

	// Token: 0x0400101A RID: 4122
	public CustomLensFlare LensFlares;

	// Token: 0x0400101B RID: 4123
	public SunOnGlass.SunBack SunBackGround;

	// Token: 0x0400101C RID: 4124
	public Color SunColor;

	// Token: 0x0400101D RID: 4125
	public AnimationCurve SunCurve;

	// Token: 0x0400101E RID: 4126
	private bool _sunScratchesEnabled = true;

	// Token: 0x0400101F RID: 4127
	private bool _lensFlaresEnabled = true;

	// Token: 0x04001020 RID: 4128
	private bool _sunBackEnabled = true;

	// Token: 0x04001021 RID: 4129
	private Texture _fallofTex;

	// Token: 0x04001022 RID: 4130
	private Material _material;

	// Token: 0x04001023 RID: 4131
	private Material _materialvc;

	// Token: 0x04001024 RID: 4132
	private Camera _camera;

	// Token: 0x04001025 RID: 4133
	private Transform _cameraTransform;

	// Token: 0x04001026 RID: 4134
	private Texture2D _visibilityTexture;

	// Token: 0x04001027 RID: 4135
	private RenderTexture _vc1;

	// Token: 0x04001028 RID: 4136
	private RenderTexture _vc2;

	// Token: 0x04001029 RID: 4137
	private Rect _oneRect;

	// Token: 0x020001D4 RID: 468
	[Serializable]
	internal class SunBack
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x000B3270 File Offset: 0x000B1470
		public void Initialize(bool formEditor = false)
		{
			if (!Application.isPlaying && !formEditor)
			{
				return;
			}
			this._trans = this.CreatePolygon();
			this._trans.gameObject.hideFlags = HideFlags.DontSave;
			this._trans.name = "SunSprite";
			this._trans.rotation = this.Light.rotation;
			this._position = this.Light.rotation * Vector3.back * 800f;
			this._trans.position = this._position;
			this._trans.localScale = new Vector3(this.size, this.size, 0.1f);
			string contents = string.Concat(new object[]
			{
				"Shader \"f2e8\" {Properties {_MainTex (\"Base\", 2D) = \"white\" {}}SubShader {Tags {\"Queue\" = \"Transparent\" } Fog { Mode Off } ZWrite Off  Blend One One Pass { SetTexture [_MainTex] {constantColor (",
				this.SunColor.r * 1.3f,
				",",
				this.SunColor.g * 1.3f,
				",",
				this.SunColor.b * 1.3f,
				",0)  combine constant * texture}}}} "
			});
			Material material = new Material(contents);
			material.hideFlags = HideFlags.HideAndDontSave;
			this._trans.renderer.material = material;
			material.mainTexture = this.SunTexture;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000B33CC File Offset: 0x000B15CC
		public void UpdatePos(Vector3 camPos)
		{
			if (this._trans == null)
			{
				return;
			}
			this._position = this.Light.rotation * Vector3.back * 800f;
			this._trans.position = this._position + camPos;
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000B3428 File Offset: 0x000B1628
		public Transform GetSun()
		{
			return this._trans;
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000B3430 File Offset: 0x000B1630
		public Transform CreatePolygon()
		{
			GameObject gameObject = new GameObject("SunBack", new Type[]
			{
				typeof(MeshFilter),
				typeof(MeshRenderer)
			});
			gameObject.GetComponent<MeshFilter>().mesh = new Mesh
			{
				vertices = new Vector3[]
				{
					new Vector3(-0.5f, -0.5f),
					new Vector3(-0.5f, 0.5f),
					new Vector3(0.5f, -0.5f),
					new Vector3(0.5f, 0.5f)
				},
				uv = new Vector2[]
				{
					new Vector2(0f, 0f),
					new Vector2(0f, 1f),
					new Vector2(1f, 0f),
					new Vector2(1f, 1f)
				},
				triangles = new int[]
				{
					0,
					2,
					3,
					3,
					1,
					0
				}
			};
			return gameObject.transform;
		}

		// Token: 0x0400102C RID: 4140
		public Transform Light;

		// Token: 0x0400102D RID: 4141
		public Texture SunTexture;

		// Token: 0x0400102E RID: 4142
		public Color SunColor;

		// Token: 0x0400102F RID: 4143
		public float size = 100f;

		// Token: 0x04001030 RID: 4144
		public Transform _trans;

		// Token: 0x04001031 RID: 4145
		private Vector3 _position;
	}
}
