using System;
using UnityEngine;

// Token: 0x02000373 RID: 883
[ExecuteInEditMode]
internal class AddSunToCamera : MonoBehaviour
{
	// Token: 0x06001CAB RID: 7339 RVA: 0x000FDD44 File Offset: 0x000FBF44
	private void OnEnable()
	{
		if (!Application.isEditor)
		{
			Debug.LogError("WARNING!!! AddSunToCamera in build!!!");
		}
		if (base.camera == null)
		{
			return;
		}
		this._cameraObject = base.gameObject;
		GameObject gameObject = GameObject.Find("SunSprite");
		while (gameObject != null)
		{
			UnityEngine.Object.DestroyImmediate(gameObject);
			gameObject = GameObject.Find("SunSprite");
		}
		UnityEngine.Object.DestroyImmediate(this._cameraObject.GetComponent<SunOnGlass>());
		UnityEngine.Object.DestroyImmediate(this._cameraObject.GetComponent<SunShafts>());
		this._camera = this._cameraObject.camera;
		this._camera.cullingMask |= 1280;
		this._camera.nearClipPlane = 0.03f;
		this._camera.depthTextureMode = DepthTextureMode.Depth;
		if (this.Sun == null)
		{
			this.Sun = GameObject.Find("sun").transform;
		}
		this.LevelSettings = SingletoneForm<LevelSettings>.Instance;
		this.SaveChanges(this.LevelSettings);
		this.UpdateSunOnGlass(this.LevelSettings);
		this.UpdateSunShafts(this.LevelSettings);
	}

	// Token: 0x06001CAC RID: 7340 RVA: 0x000FDE6C File Offset: 0x000FC06C
	private void UpdateSunOnGlass(LevelSettings levelSettings)
	{
		if (this._sunOnGlass == null || this._sunOnGlass.gameObject == null)
		{
			this._sunOnGlass = (this._cameraObject.GetComponent<SunOnGlass>() ?? this._cameraObject.AddComponent<SunOnGlass>());
		}
		this._sunOnGlass.SunColor = levelSettings.SunColor;
		this._sunOnGlass.Size = levelSettings.SunScratchesIntensity;
		this._sunOnGlass.Sun = this.Sun;
		this._sunOnGlass.ScreenTexture = StartData.sun.ScreenTexture;
		this._sunOnGlass.ScreenShader = StartData.sun.ScreenShader;
		this._sunOnGlass.VisibilityCheckerShader = StartData.sun.VisibilityCheckerShader;
		this._sunOnGlass.SunCurve = StartData.sun.SunCurve;
		this._sunOnGlass.LensFlares = StartData.sun.LensFlares;
		if (this._sunSprite == null)
		{
			GameObject gameObject = GameObject.Find("SunSprite");
			if (gameObject != null)
			{
				this._sunSprite = gameObject.transform;
			}
		}
		if (this._sunSprite != null)
		{
			UnityEngine.Object.DestroyImmediate(this._sunSprite.gameObject);
		}
		this._sunOnGlass.SunBackGround = new SunOnGlass.SunBack();
		this._sunOnGlass.SunBackGround.Light = this.Sun;
		this._sunOnGlass.SunBackGround.SunTexture = StartData.sun.SunBackTexture;
		this._sunOnGlass.SunBackGround.SunColor = this._sunOnGlass.SunColor;
		this._sunOnGlass.SunBackGround.size = 200f;
		this._sunOnGlass.SunBackGround.Initialize(true);
		this._sunOnGlass.Start();
		this._sunSprite = this._sunOnGlass.SunBackGround.GetSun();
	}

	// Token: 0x06001CAD RID: 7341 RVA: 0x000FE058 File Offset: 0x000FC258
	private void UpdateSunShafts(LevelSettings levelSettings)
	{
		if (this._sunShafts == null || this._sunShafts.gameObject == null)
		{
			this._sunShafts = (this._cameraObject.GetComponent<SunShafts>() ?? this._cameraObject.AddComponent<SunShafts>());
		}
		this._sunShafts.resolution = SunShaftsResolution.Low;
		this._sunShafts.screenBlendMode = ShaftsScreenBlendMode.Screen;
		this._sunShafts.sunTransform = this._sunSprite;
		this._sunShafts.sunColor = StartData.sun.SunColor;
		this._sunShafts.maxRadius = levelSettings.SunShaftsDensity;
		this._sunShafts.sunShaftBlurRadius = 3.5f;
		this._sunShafts.radialBlurIterations = 2;
		this._sunShafts.sunShaftIntensity = levelSettings.SunShaftsIntensity;
		this._sunShafts.useSkyBoxAlpha = -30f;
		this._sunShafts.sunShaftsShader = StartData.sun.SunShaftsShader;
		this._sunShafts.simpleClearShader = StartData.sun.SunShaftsClearShader;
		this._sunShafts.CheckResources();
		if (!levelSettings.SunLensFlares)
		{
			this._sunOnGlass.LensFlares.FlareMaterial = null;
		}
	}

	// Token: 0x06001CAE RID: 7342 RVA: 0x000FE190 File Offset: 0x000FC390
	private bool SettingsChanged(LevelSettings levelSettings)
	{
		return levelSettings.SunColor != this._savedSunColor || levelSettings.SunScratchesIntensity != this._savedSunScratchesIntensity || levelSettings.SunShaftsIntensity != this._savedSunShaftsIntensity || levelSettings.SunShaftsDensity != this._savedSunShaftsDensity || levelSettings.SunLensFlares != this._savedSunLensFlares || levelSettings.SunBackGroung != this._savedSunBackGroung;
	}

	// Token: 0x06001CAF RID: 7343 RVA: 0x000FE218 File Offset: 0x000FC418
	private void SaveChanges(LevelSettings levelSettings)
	{
		this._savedSunColor = levelSettings.SunColor;
		this._savedSunScratchesIntensity = levelSettings.SunScratchesIntensity;
		this._savedSunShaftsIntensity = levelSettings.SunShaftsIntensity;
		this._savedSunShaftsDensity = levelSettings.SunShaftsDensity;
		this._savedSunLensFlares = levelSettings.SunLensFlares;
		this._savedSunBackGroung = levelSettings.SunBackGroung;
	}

	// Token: 0x06001CB0 RID: 7344 RVA: 0x000FE270 File Offset: 0x000FC470
	private void Update()
	{
		if (!Application.isEditor)
		{
			return;
		}
		if (this.LevelSettings == null)
		{
			return;
		}
		if (this.SettingsChanged(this.LevelSettings))
		{
			this.SaveChanges(this.LevelSettings);
			this.UpdateSunOnGlass(this.LevelSettings);
			this.UpdateSunShafts(this.LevelSettings);
		}
	}

	// Token: 0x04002186 RID: 8582
	public Transform Sun;

	// Token: 0x04002187 RID: 8583
	private GameObject _cameraObject;

	// Token: 0x04002188 RID: 8584
	private Camera _camera;

	// Token: 0x04002189 RID: 8585
	private SunOnGlass _sunOnGlass;

	// Token: 0x0400218A RID: 8586
	private SunShafts _sunShafts;

	// Token: 0x0400218B RID: 8587
	public LevelSettings LevelSettings;

	// Token: 0x0400218C RID: 8588
	private Transform _sunSprite;

	// Token: 0x0400218D RID: 8589
	private Color _savedSunColor;

	// Token: 0x0400218E RID: 8590
	private float _savedSunScratchesIntensity;

	// Token: 0x0400218F RID: 8591
	private float _savedSunShaftsIntensity;

	// Token: 0x04002190 RID: 8592
	private float _savedSunShaftsDensity;

	// Token: 0x04002191 RID: 8593
	private bool _savedSunLensFlares;

	// Token: 0x04002192 RID: 8594
	private bool _savedSunBackGroung;
}
