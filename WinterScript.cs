using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000006 RID: 6
public class WinterScript : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000010 RID: 16 RVA: 0x00002B4C File Offset: 0x00000D4C
	private float TdsStartedAgo
	{
		get
		{
			if (Main.GameMode != GameMode.TargetDesignation)
			{
				return 0f;
			}
			float matchRoundTime = Globals.I.maps[Main.HostInfo.MapIndex].GameModes[GameMode.TargetDesignation].matchRoundTime;
			BaseClientGame clientGame = Peer.ClientGame;
			int num = clientGame.BearWinCount + clientGame.UsecWinCount;
			return matchRoundTime * (float)num;
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002BA8 File Offset: 0x00000DA8
	private void Start()
	{
		this.StartLevel();
		this.RenderSnowDepth();
		Terrain activeTerrain = Terrain.activeTerrain;
		if (activeTerrain == null)
		{
			Debug.Log("terrain is null");
		}
		this._terrainDetailsRepaint = null;
		if (activeTerrain != null)
		{
			if (this.TerrainDetails == null)
			{
				Debug.Log("TerrainDetails is null");
			}
			if (this.TerrainDetails.Length == 0)
			{
				Debug.Log("TerrainDetails.Length equal 0");
			}
			Texture2D detailTex = this.TerrainDetails[0];
			if (activeTerrain.terrainData == null)
			{
				Debug.Log("terrain.terrainData is null");
			}
			if (activeTerrain.terrainData.detailPrototypes == null)
			{
				Debug.Log("terrain.terrainData.detailPrototypes is null");
			}
			DetailPrototype[] detailPrototypes = activeTerrain.terrainData.detailPrototypes;
			if (detailPrototypes == null)
			{
				Debug.Log("prototypes is null");
			}
			if (detailPrototypes.Length == 0)
			{
				Debug.Log("prototypes.Length equal 0");
			}
			if (detailPrototypes != null && detailPrototypes.Length > 0)
			{
				string text = null;
				foreach (DetailPrototype detailPrototype in detailPrototypes)
				{
					if (detailPrototype.prototypeTexture != null)
					{
						text = detailPrototype.prototypeTexture.name;
						break;
					}
				}
				if (text == null)
				{
					Debug.Log("detailName is null");
				}
				if (text != null)
				{
					foreach (Texture2D texture2D in this.TerrainDetails)
					{
						if (texture2D.name == text)
						{
							detailTex = texture2D;
						}
					}
					this._terrainDetailsRepaint = new WinterScript.TerrainDetailsRepaint(activeTerrain, detailTex);
				}
			}
		}
		UtilsScreen.OnScreenChange += this.RenderSnowDepth;
		UserGraphics.ProfileChanged += this.RenderSnowDepth;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002D64 File Offset: 0x00000F64
	private void Update()
	{
		float num = this.TdsStartedAgo - this.StartTime;
		if (Peer.ClientGame != null && Peer.ClientGame.MatchState == MatchState.round_going)
		{
			num = Peer.ClientGame.MatchPassedTime + this.TdsStartedAgo - this.StartTime;
		}
		Shader.SetGlobalFloat("_Snow", this.SnowLevelCurve.Evaluate(num));
		float num2 = this.SnowFallingCurve.Evaluate(num);
		Shader.SetGlobalFloat("_SnowFalling", num2);
		if (this._sun != null)
		{
			this._sun.color = Color.Lerp(this._sourceSunColor, this._desaturatedSunColor, this.DesaturateSunCurve.Evaluate(num));
			float num3 = 1f - num2 * this.FadeShadow;
			this._sun.intensity = this._sunIntensity * num3 * this.SunIntensityCurve.Evaluate(num);
			this._sun.shadowStrength = this._shadowStrength * num3;
			if (SunOnGlass.SunOnGlassInstance != null)
			{
				Color sunColor = this._sun.color * (1f - num2 * this.FadeScratches);
				SunOnGlass.SunOnGlassInstance.SunColor = sunColor;
				SunOnGlass.SunShaftsInstance.sunColor = sunColor;
			}
		}
		if (Thermal.Exist && !Thermal.On)
		{
			RenderSettings.fogDensity = this._fogDensity * (1f + num2 * this.FadeFog);
			RenderSettings.fogColor = Color.Lerp(this._sourceFogColor, this._desaturatedFogColor, this.DesaturateSunCurve.Evaluate(num));
		}
		float num4 = this.SoundsLerpCurve.Evaluate(num);
		if (Mathf.Abs(this._lastStepLerpVal - num4) > 0.04f)
		{
			this._lastStepLerpVal = num4;
			if (this._lerpers == null)
			{
				Debug.Log("_lerpers is null");
			}
			using (LinkedList<WinterScript.AudioLerper>.Enumerator enumerator = this._lerpers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == null)
					{
						Debug.Log("lerper is null");
					}
				}
			}
			foreach (WinterScript.AudioLerper audioLerper in this._lerpers)
			{
				audioLerper.Add(num4);
			}
		}
		if (this._oldMusic != null)
		{
			this._oldMusic.volume = this.MusicFadeOut.Evaluate(num);
			if (this.MusicFadeOut[this.MusicFadeOut.length - 1].time < num)
			{
				UnityEngine.Object.Destroy(this._oldMusic);
				this._oldMusic = null;
			}
		}
		if (this._newMusic != null)
		{
			this._newMusic.volume = this.MusicFadeIn.Evaluate(num);
			if (this.MusicFadeIn[this.MusicFadeIn.length - 1].time < num)
			{
				this._newMusic = null;
			}
		}
		if (this._breathPs != null)
		{
			this._breathPs.startColor = new Color(1f, 1f, 1f, this.BreathCurve.Evaluate(num));
			if (CameraListener.Camera.transform.parent != this._cameraParent)
			{
				this._cameraParent = CameraListener.Camera.transform.parent;
				if (this._cameraParent.name == "Head_Camera")
				{
					this._breathTr.parent = CameraListener.Camera.transform;
					this._breathTr.localPosition = new Vector3(0f, -0.015f, 0.0301f);
					this._breathTr.localRotation = Quaternion.identity;
					this._breathPs.gameObject.SetActive(true);
					this._breathPs.enableEmission = true;
					this._breathPs.Play(false);
				}
				else
				{
					this._breathPs.gameObject.SetActive(false);
					this._breathPs.enableEmission = false;
					this._breathPs.Pause(false);
				}
			}
		}
		if (this._terrainDetailsRepaint != null)
		{
			this._terrainDetailsRepaint.Update(this.TerrainDetailCurve.Evaluate(num));
		}
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000031F8 File Offset: 0x000013F8
	private void StartLevel()
	{
		this._timeShift = Time.time + this.StartTime;
		Shader.SetGlobalTexture("_SnowTexture", this.SnowTex);
		this._sun = GameObject.Find("sun").light;
		this._sourceSunColor = this._sun.color;
		this._sunIntensity = this._sun.intensity;
		this._shadowStrength = this._sun.shadowStrength;
		LevelSettings levelSettings = (LevelSettings)UnityEngine.Object.FindObjectOfType(typeof(LevelSettings));
		this._fogDensity = levelSettings.fogDensity;
		this._sourceFogColor = levelSettings.fogColor;
		this._desaturatedFogColor = WinterScript.Desaturate(this._sourceFogColor);
		this._desaturatedSunColor = WinterScript.Desaturate(this._sourceSunColor);
		this.SoundInit();
		AudioSource[] componentsInChildren = levelSettings.gameObject.GetComponentsInChildren<AudioSource>();
		foreach (AudioSource audioSource in componentsInChildren)
		{
			if (audioSource.playOnAwake && audioSource.loop)
			{
				this._oldMusic = audioSource;
				this._newMusic = audioSource.gameObject.AddComponent<AudioSource>();
				this._newMusic.clip = this.SnowyWind;
				this._newMusic.loop = true;
				this._newMusic.Play();
				break;
			}
		}
		this._breathTr = (Transform)UnityEngine.Object.Instantiate(this.BreathSystem);
		this._breathTr.gameObject.name = "Breath";
		this._breathPs = this._breathTr.GetComponent<ParticleSystem>();
		this.FixTerrainSpecular();
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000338C File Offset: 0x0000158C
	private void FixTerrainSpecular()
	{
		string mapName = Peer.Info.MapName;
		if (mapName != null)
		{
			if (WinterScript.<>f__switch$map0 == null)
			{
				WinterScript.<>f__switch$map0 = new Dictionary<string, int>(6)
				{
					{
						"lake",
						0
					},
					{
						"evac",
						0
					},
					{
						"old_sawmill_dm",
						0
					},
					{
						"old_sawmill",
						0
					},
					{
						"parkside",
						0
					},
					{
						"interchange_dm",
						0
					}
				};
			}
			int num;
			if (WinterScript.<>f__switch$map0.TryGetValue(mapName, out num))
			{
				if (num == 0)
				{
					this.TerrainMaterial.SetColor("_SpecColor", Color.black);
				}
			}
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00003440 File Offset: 0x00001640
	private void SoundInit()
	{
		DecalFactory instance = SingletoneForm<DecalFactory>.Instance;
		List<SurfaceDescription> surfaces = instance.surfaces;
		float[][] array = new float[this.SnowStepClip.Length][];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = new float[this.SnowStepClip[i].samples];
			this.SnowStepClip[i].GetData(array[i], 0);
		}
		foreach (SurfaceDescription surfaceDescription in surfaces)
		{
			for (int j = 0; j < surfaceDescription.walkSounds.Count; j++)
			{
				AudioClip sourceClip = surfaceDescription.walkSounds[j];
				this._lerpers.AddLast(new WinterScript.AudioLerper(sourceClip, array[(j >= array.Length) ? (j - array.Length) : j]));
			}
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00003550 File Offset: 0x00001750
	private static Color Desaturate(Color color)
	{
		float num = (color.r + color.g + color.b) / 3f;
		return new Color(num, num, num, color.a);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000358C File Offset: 0x0000178C
	private void RenderSnowDepth()
	{
		if (this._renderDepthLocked)
		{
			return;
		}
		this._renderDepthLocked = true;
		GameObject gameObject = ((LevelSettings)UnityEngine.Object.FindObjectOfType(typeof(LevelSettings))).gameObject;
		Vector3 position = gameObject.GetComponentInChildren<UpperLeftPoint>().transform.position;
		Vector3 position2 = gameObject.GetComponentInChildren<LowerRightPoint>().transform.position;
		Vector2 a = new Vector2(Mathf.Max(position.x, position2.x), Mathf.Max(position.z, position2.z));
		Vector2 b = new Vector2(Mathf.Min(position.x, position2.x), Mathf.Min(position.z, position2.z));
		Vector2 vector = (a + b) * 0.5f;
		Vector2 vector2 = a - b;
		Vector2 minMaxY = WinterScript.GetMinMaxY();
		float num = Mathf.Max(vector2.x, vector2.y);
		float num2 = minMaxY.y - minMaxY.x;
		GameObject gameObject2 = new GameObject("SnowCam", new Type[]
		{
			typeof(Camera)
		});
		gameObject2.transform.position = new Vector3(vector.x, minMaxY.y, vector.y);
		gameObject2.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
		Camera component = gameObject2.GetComponent<Camera>();
		component.orthographic = true;
		component.orthographicSize = num;
		component.aspect = 1f;
		component.farClipPlane = num2;
		component.depthTextureMode = DepthTextureMode.Depth;
		LinkedList<Renderer> linkedList = WinterScript.FindTrees();
		foreach (Renderer renderer in linkedList)
		{
			renderer.enabled = false;
		}
		RenderTexture renderTexture = new RenderTexture(2048, 2048, 1, RenderTextureFormat.Default);
		component.cullingMask = this.DepthRendererMask.value;
		component.targetTexture = RenderTexture.GetTemporary(2048, 2048, 1, RenderTextureFormat.Default);
		component.Render();
		Graphics.Blit(component.targetTexture, renderTexture, this.DepthMaterial);
		RenderTexture.ReleaseTemporary(component.targetTexture);
		foreach (Renderer renderer2 in linkedList)
		{
			renderer2.enabled = true;
		}
		Vector3 v = new Vector3(vector.x - num, minMaxY.x, vector.y - num);
		Vector3 v2 = new Vector3(0.5f / num, 1f / num2, 0.5f / num);
		Shader.SetGlobalVector("_SnowMapStart", v);
		Shader.SetGlobalVector("_SnowMapScale", v2);
		Shader.SetGlobalTexture("_SnowMap", renderTexture);
		UnityEngine.Object.Destroy(gameObject2);
		base.StartCoroutine(this.UnlockDepth());
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000038C8 File Offset: 0x00001AC8
	private IEnumerator UnlockDepth()
	{
		yield return new WaitForEndOfFrame();
		this._renderDepthLocked = false;
		yield break;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x000038E4 File Offset: 0x00001AE4
	private static LinkedList<Renderer> FindTrees()
	{
		LevelSettings levelSettings = (LevelSettings)UnityEngine.Object.FindObjectOfType(typeof(LevelSettings));
		GameObject gameObject = levelSettings.gameObject;
		HashSet<string> hashSet = new HashSet<string>(new string[]
		{
			"Hidden/Nature/Tree Creator Bark Optimized Snow",
			"Hidden/Nature/Tree Creator Bark Optimized",
			"Hidden/Nature/Tree Creator Leaves Optimized Snow 2",
			"Hidden/Nature/Tree Creator Leaves Optimized",
			"Hidden/TerrainEngine/Details/WavingDoublePass",
			"Hidden/TerrainEngine/Details/BillboardWavingDoublePass",
			"Transparent/Cutout/Diffuse TreesSnow 1"
		});
		LinkedList<Renderer> linkedList = new LinkedList<Renderer>();
		Renderer[] componentsInChildren = gameObject.GetComponentsInChildren<Renderer>();
		foreach (Renderer renderer in componentsInChildren)
		{
			Material[] materials = renderer.materials;
			foreach (Material material in materials)
			{
				if (hashSet.Contains(material.shader.name))
				{
					linkedList.AddLast(renderer);
					break;
				}
			}
		}
		return linkedList;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000039DC File Offset: 0x00001BDC
	private static Vector2 GetMinMaxY()
	{
		string mapName = Peer.Info.MapName;
		switch (mapName)
		{
		case "interchange":
			return new Vector2(-12f, 20f);
		case "bay5":
			return new Vector2(-12f, 12f);
		case "terminal":
			return new Vector2(-12f, 15f);
		}
		return new Vector2(-50f, 60f);
	}

	// Token: 0x04000016 RID: 22
	public float debugValue;

	// Token: 0x04000017 RID: 23
	public bool debugWrite;

	// Token: 0x04000018 RID: 24
	public Texture SnowTex;

	// Token: 0x04000019 RID: 25
	public float StartTime;

	// Token: 0x0400001A RID: 26
	public AnimationCurve SnowLevelCurve;

	// Token: 0x0400001B RID: 27
	public AnimationCurve SnowFallingCurve;

	// Token: 0x0400001C RID: 28
	public AnimationCurve DesaturateSunCurve;

	// Token: 0x0400001D RID: 29
	public AnimationCurve SunIntensityCurve = AnimationCurve.Linear(0f, 1f, 100f, 1f);

	// Token: 0x0400001E RID: 30
	public AnimationCurve SoundsLerpCurve;

	// Token: 0x0400001F RID: 31
	public AudioClip[] SnowStepClip;

	// Token: 0x04000020 RID: 32
	public AudioClip SnowyWind;

	// Token: 0x04000021 RID: 33
	public AnimationCurve MusicFadeOut;

	// Token: 0x04000022 RID: 34
	public AnimationCurve MusicFadeIn;

	// Token: 0x04000023 RID: 35
	public AnimationCurve BreathCurve;

	// Token: 0x04000024 RID: 36
	public float FadeShadow = 0.4f;

	// Token: 0x04000025 RID: 37
	public float FadeScratches = 0.7f;

	// Token: 0x04000026 RID: 38
	public float FadeFog = 1.2f;

	// Token: 0x04000027 RID: 39
	private float _snowFallingSpeed;

	// Token: 0x04000028 RID: 40
	private float _timeShift;

	// Token: 0x04000029 RID: 41
	private Light _sun;

	// Token: 0x0400002A RID: 42
	private Color _sourceSunColor;

	// Token: 0x0400002B RID: 43
	private Color _sourceFogColor;

	// Token: 0x0400002C RID: 44
	private Color _desaturatedSunColor;

	// Token: 0x0400002D RID: 45
	private Color _desaturatedFogColor;

	// Token: 0x0400002E RID: 46
	private float _sunIntensity;

	// Token: 0x0400002F RID: 47
	private float _shadowStrength;

	// Token: 0x04000030 RID: 48
	private float _fogDensity;

	// Token: 0x04000031 RID: 49
	private float _lastStepLerpVal;

	// Token: 0x04000032 RID: 50
	private AudioSource _oldMusic;

	// Token: 0x04000033 RID: 51
	private AudioSource _newMusic;

	// Token: 0x04000034 RID: 52
	private LinkedList<WinterScript.AudioLerper> _lerpers = new LinkedList<WinterScript.AudioLerper>();

	// Token: 0x04000035 RID: 53
	public Transform BreathSystem;

	// Token: 0x04000036 RID: 54
	private Transform _breathTr;

	// Token: 0x04000037 RID: 55
	private ParticleSystem _breathPs;

	// Token: 0x04000038 RID: 56
	private Transform _cameraParent;

	// Token: 0x04000039 RID: 57
	public LayerMask DepthRendererMask;

	// Token: 0x0400003A RID: 58
	public Material DepthMaterial;

	// Token: 0x0400003B RID: 59
	public Material TerrainMaterial;

	// Token: 0x0400003C RID: 60
	private WinterScript.TerrainDetailsRepaint _terrainDetailsRepaint;

	// Token: 0x0400003D RID: 61
	public AnimationCurve TerrainDetailCurve;

	// Token: 0x0400003E RID: 62
	public Texture2D[] TerrainDetails;

	// Token: 0x0400003F RID: 63
	private bool _renderDepthLocked;

	// Token: 0x02000007 RID: 7
	public class AudioLerper
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public AudioLerper(AudioClip sourceClip, float[] addData)
		{
			if (sourceClip == null)
			{
				Debug.Log("sourceClip is null");
			}
			if (addData == null)
			{
				Debug.Log("addData is null");
			}
			this._sourceClip = sourceClip;
			this._sourceData = new float[sourceClip.samples];
			sourceClip.GetData(this._sourceData, 0);
			this._data = new float[this._sourceData.Length];
			for (int i = 0; i < this._data.Length; i++)
			{
				this._data[i] = this._sourceData[i];
			}
			this._addData = addData;
			this._length = ((sourceClip.samples >= addData.Length) ? addData.Length : sourceClip.samples);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003B64 File Offset: 0x00001D64
		public void Add(float f)
		{
			for (int i = 0; i < this._length; i++)
			{
				this._data[i] = this._sourceData[i] + (this._addData[i] - this._sourceData[i]) * f;
			}
			this._sourceClip.SetData(this._data, 0);
		}

		// Token: 0x04000042 RID: 66
		private AudioClip _sourceClip;

		// Token: 0x04000043 RID: 67
		private float[] _data;

		// Token: 0x04000044 RID: 68
		private float[] _sourceData;

		// Token: 0x04000045 RID: 69
		private float[] _addData;

		// Token: 0x04000046 RID: 70
		private int _length;
	}

	// Token: 0x02000008 RID: 8
	public class TerrainDetailsRepaint
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00003BC0 File Offset: 0x00001DC0
		public TerrainDetailsRepaint(Terrain terrain, Texture2D detailTex)
		{
			Debug.Log("TerrainDetailsRepaint Ctr");
			if (terrain == null)
			{
				Debug.Log("terrain is null");
			}
			if (detailTex == null)
			{
				Debug.Log("detailTex is null");
			}
			this._terrainData = terrain.terrainData;
			this._prototypes = this._terrainData.detailPrototypes;
			if (this._terrainData == null)
			{
				Debug.Log("_terrainData is null");
			}
			if (this._prototypes == null)
			{
				Debug.Log("_prototypes is null");
			}
			if (this._prototypes.Length == 0)
			{
				Debug.Log("_prototypes.Length equal 0");
			}
			if (this._prototypes.Length > 0)
			{
				this._source = detailTex.GetPixels32();
				this._current = new Color32[this._source.Length];
				for (int i = 0; i < this._current.Length; i++)
				{
					this._current[i] = this._source[i];
				}
				this._texture = new Texture2D(detailTex.width, detailTex.height, TextureFormat.RGBA32, true);
				this._lerpers = new WinterScript.TerrainDetailsRepaint.DetailColorLerper[this._prototypes.Length];
				for (int j = 0; j < this._prototypes.Length; j++)
				{
					this._prototypes[j].prototypeTexture = this._texture;
					this._lerpers[j] = new WinterScript.TerrainDetailsRepaint.DetailColorLerper(this._prototypes[j]);
				}
			}
			this._green = new int[256];
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003D54 File Offset: 0x00001F54
		public void Update(float t)
		{
			if (Mathf.Abs(this._lastT - t) < 0.02f)
			{
				return;
			}
			this._lastT = t;
			int num = (int)(t * 140f) - 50;
			t *= 2f;
			if (this._lerpers == null)
			{
				Debug.Log("Terrain._lerpers is null");
			}
			WinterScript.TerrainDetailsRepaint.DetailColorLerper[] lerpers = this._lerpers;
			for (int i = 0; i < lerpers.Length; i++)
			{
				if (lerpers[i] == null)
				{
					Debug.Log("Terrain.lerper is null");
				}
			}
			foreach (WinterScript.TerrainDetailsRepaint.DetailColorLerper detailColorLerper in this._lerpers)
			{
				detailColorLerper.Lerp(t);
			}
			for (int k = 0; k < 256; k++)
			{
				int num2 = (k >> 1) + num;
				if (num2 < 0)
				{
					num2 = 0;
				}
				if (num2 > 255)
				{
					num2 = 255;
				}
				this._green[k] = num2;
			}
			WinterScript.TerrainDetailsRepaint.CalcWithoutShader(this._source, this._current, this._green);
			if (this._texture == null)
			{
				Debug.Log("_texture is null");
			}
			if (this._terrainData == null)
			{
				Debug.Log("_terrainData2 is null");
			}
			if (this._prototypes == null)
			{
				Debug.Log("_prototypes2 is null");
			}
			this._texture.SetPixels32(this._current);
			this._texture.Apply(true);
			this._terrainData.detailPrototypes = this._prototypes;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003EE4 File Offset: 0x000020E4
		private static void CalcWithoutShader(Color32[] source, Color32[] current, int[] green)
		{
			for (int i = 0; i < source.Length; i++)
			{
				int b = green[(int)source[i].g];
				current[i].r = WinterScript.TerrainDetailsRepaint.Add(source[i].r, b);
				current[i].g = WinterScript.TerrainDetailsRepaint.Add(source[i].g, b);
				current[i].b = WinterScript.TerrainDetailsRepaint.Add(source[i].b, b);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003F70 File Offset: 0x00002170
		private static byte Add(byte a, int b)
		{
			b += (int)a;
			if (b > 255)
			{
				b = 255;
			}
			return (byte)b;
		}

		// Token: 0x04000047 RID: 71
		private TerrainData _terrainData;

		// Token: 0x04000048 RID: 72
		private DetailPrototype[] _prototypes;

		// Token: 0x04000049 RID: 73
		private WinterScript.TerrainDetailsRepaint.DetailColorLerper[] _lerpers;

		// Token: 0x0400004A RID: 74
		private int[] _green;

		// Token: 0x0400004B RID: 75
		private Color32[] _source;

		// Token: 0x0400004C RID: 76
		private Color32[] _current;

		// Token: 0x0400004D RID: 77
		private Texture2D _texture;

		// Token: 0x0400004E RID: 78
		private float _lastT;

		// Token: 0x02000009 RID: 9
		private class DetailColorLerper
		{
			// Token: 0x06000021 RID: 33 RVA: 0x00003F8C File Offset: 0x0000218C
			public DetailColorLerper(DetailPrototype prototype)
			{
				this._prototype = prototype;
				this._dryColor = prototype.dryColor;
				this._healthyColor = prototype.healthyColor;
			}

			// Token: 0x06000023 RID: 35 RVA: 0x00003FC0 File Offset: 0x000021C0
			public void Lerp(float t)
			{
				if (t >= 1f)
				{
					this._prototype.dryColor = WinterScript.TerrainDetailsRepaint.DetailColorLerper.White;
					this._prototype.healthyColor = WinterScript.TerrainDetailsRepaint.DetailColorLerper.White;
				}
				else
				{
					this._prototype.dryColor = WinterScript.TerrainDetailsRepaint.DetailColorLerper.Lerp(this._dryColor, WinterScript.TerrainDetailsRepaint.DetailColorLerper.White, t);
					this._prototype.healthyColor = WinterScript.TerrainDetailsRepaint.DetailColorLerper.Lerp(this._healthyColor, WinterScript.TerrainDetailsRepaint.DetailColorLerper.White, t);
				}
			}

			// Token: 0x06000024 RID: 36 RVA: 0x00004038 File Offset: 0x00002238
			private static Color Lerp(Color a, Color b, float t)
			{
				return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
			}

			// Token: 0x0400004F RID: 79
			private static readonly Color White = Color.white;

			// Token: 0x04000050 RID: 80
			private DetailPrototype _prototype;

			// Token: 0x04000051 RID: 81
			private Color _dryColor;

			// Token: 0x04000052 RID: 82
			private Color _healthyColor;
		}
	}
}
