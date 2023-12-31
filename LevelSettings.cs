using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001C9 RID: 457
[AddComponentMenu("Scripts/Game/Components/LevelSettings")]
public class LevelSettings : SingletoneForm<LevelSettings>
{
	// Token: 0x06000F7A RID: 3962 RVA: 0x000B197C File Offset: 0x000AFB7C
	protected override void Awake()
	{
		base.Awake();
		this.ambientLight_temp = RenderSettings.ambientLight;
		this.flareStrength_temp = RenderSettings.flareStrength;
		this.fog_temp = RenderSettings.fog;
		this.fogColor_temp = RenderSettings.fogColor;
		this.fogDensity_temp = RenderSettings.fogDensity;
		this.fogEndDistance_temp = RenderSettings.fogEndDistance;
		this.fogMode_temp = RenderSettings.fogMode;
		this.fogStartDistance_temp = RenderSettings.fogStartDistance;
		this.haloStrength_temp = RenderSettings.haloStrength;
		this.skybox_temp = RenderSettings.skybox;
		this.lightmapsMode_temp = LightmapSettings.lightmapsMode;
		Shader.SetGlobalFloat("_FogMultiplier", this.opticFogMultiplier);
	}

	// Token: 0x06000F7B RID: 3963 RVA: 0x000B1A18 File Offset: 0x000AFC18
	private void Start()
	{
		TerrainCollider terrainCollider = (TerrainCollider)UnityEngine.Object.FindObjectOfType(typeof(TerrainCollider));
		if (terrainCollider != null)
		{
			string mapName = Peer.Info.MapName;
			if (mapName != null)
			{
				if (LevelSettings.<>f__switch$map8 == null)
				{
					LevelSettings.<>f__switch$map8 = new Dictionary<string, int>(8)
					{
						{
							"day7",
							0
						},
						{
							"bay5",
							0
						},
						{
							"bay7",
							0
						},
						{
							"terminal",
							0
						},
						{
							"terminal2",
							0
						},
						{
							"terminal_dm",
							0
						},
						{
							"station",
							0
						},
						{
							"develop",
							0
						}
					};
				}
				int num;
				if (LevelSettings.<>f__switch$map8.TryGetValue(mapName, out num))
				{
					if (num == 0)
					{
						terrainCollider.material = new PhysicMaterial("concrete");
						return;
					}
				}
			}
			terrainCollider.material = new PhysicMaterial("soil");
		}
	}

	// Token: 0x06000F7C RID: 3964 RVA: 0x000B1B18 File Offset: 0x000AFD18
	private void Update()
	{
		if (this.configureOpticFogMultiplierAtRuntime)
		{
			Shader.SetGlobalFloat("_FogMultiplier", this.opticFogMultiplier);
		}
	}

	// Token: 0x06000F7D RID: 3965 RVA: 0x000B1B38 File Offset: 0x000AFD38
	public static void OnProfileChanged()
	{
		LevelSettings.OnProfileChanged(false);
	}

	// Token: 0x06000F7E RID: 3966 RVA: 0x000B1B40 File Offset: 0x000AFD40
	public static void OnProfileChanged(bool fast)
	{
		RenderSettings.ambientLight = SingletoneForm<LevelSettings>.Instance.ambientLight;
		if (!fast)
		{
			RenderSettings.flareStrength = SingletoneForm<LevelSettings>.instance.flareStrength;
		}
		RenderSettings.fog = SingletoneForm<LevelSettings>.instance.fog;
		RenderSettings.fogColor = SingletoneForm<LevelSettings>.instance.fogColor;
		RenderSettings.fogDensity = SingletoneForm<LevelSettings>.instance.fogDensity;
		RenderSettings.fogEndDistance = SingletoneForm<LevelSettings>.instance.fogEndDistance;
		RenderSettings.fogMode = SingletoneForm<LevelSettings>.instance.fogMode;
		RenderSettings.fogStartDistance = SingletoneForm<LevelSettings>.instance.fogStartDistance;
		if (!fast)
		{
			RenderSettings.haloStrength = SingletoneForm<LevelSettings>.instance.haloStrength;
		}
		RenderSettings.skybox = SingletoneForm<LevelSettings>.instance.skybox;
		LightmapSettings.lightmapsMode = SingletoneForm<LevelSettings>.instance.lightmapsMode_temp;
	}

	// Token: 0x06000F7F RID: 3967 RVA: 0x000B1C00 File Offset: 0x000AFE00
	public override void OnDisconnect()
	{
		RenderSettings.ambientLight = this.ambientLight_temp;
		RenderSettings.flareStrength = this.flareStrength_temp;
		RenderSettings.fog = this.fog_temp;
		RenderSettings.fogColor = this.fogColor_temp;
		RenderSettings.fogDensity = this.fogDensity_temp;
		RenderSettings.fogEndDistance = this.fogEndDistance_temp;
		RenderSettings.fogMode = this.fogMode_temp;
		RenderSettings.fogStartDistance = this.fogStartDistance_temp;
		RenderSettings.haloStrength = this.haloStrength_temp;
		RenderSettings.skybox = this.skybox_temp;
		LightmapSettings.lightmapsMode = this.lightmapsMode_temp;
		SingletoneForm<LevelSettings>.instance = null;
	}

	// Token: 0x04000FDA RID: 4058
	public Color ambientLight;

	// Token: 0x04000FDB RID: 4059
	public Color ambientLightLQ = new Color(0.6039216f, 0.63529414f, 0.6509804f);

	// Token: 0x04000FDC RID: 4060
	public Color ambientLightLQ2 = new Color(0.6039216f, 0.63529414f, 0.6509804f);

	// Token: 0x04000FDD RID: 4061
	public float flareStrength;

	// Token: 0x04000FDE RID: 4062
	public bool fog;

	// Token: 0x04000FDF RID: 4063
	public Color fogColor;

	// Token: 0x04000FE0 RID: 4064
	public float fogDensity;

	// Token: 0x04000FE1 RID: 4065
	public float fogEndDistance;

	// Token: 0x04000FE2 RID: 4066
	public FogMode fogMode;

	// Token: 0x04000FE3 RID: 4067
	public bool configureOpticFogMultiplierAtRuntime;

	// Token: 0x04000FE4 RID: 4068
	[Range(0f, 1f)]
	public float opticFogMultiplier;

	// Token: 0x04000FE5 RID: 4069
	public float fogStartDistance;

	// Token: 0x04000FE6 RID: 4070
	public float haloStrength;

	// Token: 0x04000FE7 RID: 4071
	public Material skybox;

	// Token: 0x04000FE8 RID: 4072
	public LightmapsMode lightmapsMode;

	// Token: 0x04000FE9 RID: 4073
	public float PlayerCameraFarClipPlane;

	// Token: 0x04000FEA RID: 4074
	public float PlayerCameraNearClipPlane;

	// Token: 0x04000FEB RID: 4075
	public float ModOpticCameraNearClipPlane;

	// Token: 0x04000FEC RID: 4076
	private Color ambientLight_temp;

	// Token: 0x04000FED RID: 4077
	private float flareStrength_temp;

	// Token: 0x04000FEE RID: 4078
	private bool fog_temp;

	// Token: 0x04000FEF RID: 4079
	private Color fogColor_temp;

	// Token: 0x04000FF0 RID: 4080
	private float fogDensity_temp;

	// Token: 0x04000FF1 RID: 4081
	private float fogEndDistance_temp;

	// Token: 0x04000FF2 RID: 4082
	private FogMode fogMode_temp;

	// Token: 0x04000FF3 RID: 4083
	private float fogStartDistance_temp;

	// Token: 0x04000FF4 RID: 4084
	private float haloStrength_temp;

	// Token: 0x04000FF5 RID: 4085
	private Material skybox_temp;

	// Token: 0x04000FF6 RID: 4086
	private LightmapsMode lightmapsMode_temp;

	// Token: 0x04000FF7 RID: 4087
	public Color SunColor = new Color32(byte.MaxValue, 209, 145, byte.MaxValue);

	// Token: 0x04000FF8 RID: 4088
	public float SunScratchesIntensity = 6f;

	// Token: 0x04000FF9 RID: 4089
	public float SunShaftsIntensity = 0.2f;

	// Token: 0x04000FFA RID: 4090
	public float SunShaftsDensity = 0.15f;

	// Token: 0x04000FFB RID: 4091
	public bool SunLensFlares = true;

	// Token: 0x04000FFC RID: 4092
	public bool SunBackGroung = true;

	// Token: 0x04000FFD RID: 4093
	public Texture2D radar;

	// Token: 0x04000FFE RID: 4094
	public float scale = 1f;

	// Token: 0x04000FFF RID: 4095
	public float LODMult = 1f;
}
