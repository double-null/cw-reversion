using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x02000198 RID: 408
[AddComponentMenu("Scripts/GUI/SplashGUI")]
internal class SplashGUI : Form
{
	// Token: 0x06000B7B RID: 2939 RVA: 0x0008E494 File Offset: 0x0008C694
	[Obfuscation(Exclude = true)]
	public void StartSplash(object obj)
	{
		this.Show(0.5f, 0f);
		this.buttonA.Show(0.5f, 0f);
		this.texA.Show(0.5f, 0f);
	}

	// Token: 0x06000B7C RID: 2940 RVA: 0x0008E4DC File Offset: 0x0008C6DC
	public void DownloadSplash()
	{
		base.StartCoroutine(SplashGUI.downloadSplash());
	}

	// Token: 0x06000B7D RID: 2941 RVA: 0x0008E4EC File Offset: 0x0008C6EC
	public static IEnumerator downloadSplash()
	{
		string splashPicName = (!CVars.IsStandaloneRealm) ? "splash.jpg" : "splash_client.jpg";
		WWW www = new WWW(WWWUtil.rootWWWNoExtension(splashPicName));
		yield return www;
		if (www != null && www.error == null)
		{
			yield return new WaitForSeconds(1f);
			SplashGUI.downloadedSplash = www.texture;
			yield return new WaitForSeconds(2f);
			www.Dispose();
			www = null;
		}
		yield break;
	}

	// Token: 0x06000B7E RID: 2942 RVA: 0x0008E500 File Offset: 0x0008C700
	protected override void Awake()
	{
		base.Awake();
		this.splashAlpha.Hide(0f, 0f);
		SplashGUI.I = this;
	}

	// Token: 0x06000B7F RID: 2943 RVA: 0x0008E524 File Offset: 0x0008C724
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000B80 RID: 2944 RVA: 0x0008E534 File Offset: 0x0008C734
	public override void Register()
	{
		EventFactory.Register("StartSplash", this);
	}

	// Token: 0x17000157 RID: 343
	// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0008E544 File Offset: 0x0008C744
	public override int Width
	{
		get
		{
			return 800;
		}
	}

	// Token: 0x17000158 RID: 344
	// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0008E54C File Offset: 0x0008C74C
	public override int Height
	{
		get
		{
			return 600;
		}
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0008E554 File Offset: 0x0008C754
	public override void LateGUI()
	{
		if (!base.Visible)
		{
			return;
		}
		if (SplashGUI.downloadedSplash != null && this.splash != SplashGUI.downloadedSplash)
		{
			this.splash = SplashGUI.downloadedSplash;
			this.splashAlpha.Show(0.3f, 0f);
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		GUI.DrawTexture(new Rect(-10f, -10f, (float)(Screen.width + 20), (float)(Screen.height + 20)), this.splash_black);
		this.gui.color = new Color(1f, 1f, 1f, this.texA.visibility * base.visibility);
		this.gui.BeginGroup(new Rect((float)(Screen.width / 2 - this.splash.width / 2) + this.rect.x, (float)(Screen.height / 2 - this.splash.height / 2) + this.rect.y, (float)this.splash.width, (float)this.splash.height));
		float a = this.gui.color.a;
		this.gui.color = new Color(1f, 1f, 1f, this.splashAlpha.visibility * base.visibility);
		this.gui.Picture(new Vector2(0f, 0f), this.splash);
		this.gui.color = new Color(1f, 1f, 1f, a);
		this.gui.EndGroup();
		this.gui.BeginGroup(new Rect((float)(Screen.width / 2 - 100), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height), false);
		int num = 400;
		if (LoadProfile.profileLoaded && this.buttonA.Visible && Main.reloads < 4)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.buttonA.visibility * base.visibility);
			if ((this.gui.Button(new Vector2(8f, (float)(num - 6)), this.button[0], this.button[1], this.button[1], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.buttonA.MaxVisible) || CVars.realm == "ag")
			{
				Memory.Snapshot();
				LoadProfile.splashScreenOn = false;
				this.Hide(0.35f);
				this.buttonA.Hide(0.5f, 0f);
			}
			this.gui.TextField(new Rect(8f, (float)(num - 4), (float)this.button[1].width, (float)this.button[1].height), Language.StartGame, 25, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(8f, (float)(num - 5), (float)this.button[1].width, (float)this.button[1].height), Language.StartGame, 25, "#000000", TextAnchor.MiddleCenter, false, false);
		}
		else if (Main.reloads < 4 && Time.realtimeSinceStartup < 120f)
		{
			float angle = 180f * Time.realtimeSinceStartup * 1.5f;
			Vector2 vector = new Vector2(21f, (float)(num + 8));
			this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.krutilka_small.width / 2), vector.y + (float)(this.krutilka_small.height / 2)));
			this.gui.Picture(new Vector2(vector.x, vector.y), this.krutilka_small);
			this.gui.RotateGUI(0f, Vector2.zero);
			this.gui.TextField(new Rect(19f, (float)(num - 2), (float)this.button[1].width, (float)this.button[1].height), Language.LoadingProfile, 18, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		}
		else
		{
			this.gui.TextField(new Rect(5f, (float)(num - 2), (float)this.button[1].width, (float)this.button[1].height), Language.LoadingProfileFailed, 18, "#FF0000", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(5f, (float)(num + 40), (float)this.button[1].width, (float)this.button[1].height), Language.LoadingProfileCheckConnection, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(5f, (float)(num + 55), (float)this.button[1].width, (float)this.button[1].height), Language.LoadingProfileCheckSoftware, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(5f, (float)(num + 70), (float)this.button[1].width, (float)this.button[1].height), Language.LoadingProfileReloadApplication, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		}
		this.gui.EndGroup();
		if (CVars.IsStandaloneRealm || CVars.realm == "fb" || CVars.realm == "kg" || CVars.realm == "omega")
		{
			float width = MainGUI.Instance.CalcWidth("Privacy policy", MainGUI.Instance.fontDNC57, 14);
			Rect rect = new Rect(5f, (float)(Screen.height - 20), width, 30f);
			MainGUI.Instance.TextLabel(rect, "Privacy policy", 14, (!rect.Contains(Event.current.mousePosition)) ? "#999999" : "#ffffff", TextAnchor.UpperLeft, true);
			if (GUI.Button(rect, string.Empty, CWGUI.p.EmptyButton))
			{
				if (CVars.IsStandaloneRealm)
				{
					Application.OpenURL("http://contractwarsgame.com/privacy.html");
				}
				else
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://contractwarsgame.com/privacy.html"
					});
				}
			}
		}
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0008EC3C File Offset: 0x0008CE3C
	public override void OnDestroy()
	{
		if (this.wwwTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.wwwTexture, true);
			this.wwwTexture = null;
		}
	}

	// Token: 0x04000D55 RID: 3413
	public static SplashGUI I;

	// Token: 0x04000D56 RID: 3414
	public Texture2D[] button;

	// Token: 0x04000D57 RID: 3415
	public Texture2D splash;

	// Token: 0x04000D58 RID: 3416
	public Texture2D splash_black;

	// Token: 0x04000D59 RID: 3417
	public Texture2D krutilka_small;

	// Token: 0x04000D5A RID: 3418
	public static Texture2D downloadedSplash;

	// Token: 0x04000D5B RID: 3419
	public GUIStyle FbBtn = new GUIStyle();

	// Token: 0x04000D5C RID: 3420
	public GUIStyle VkBtn = new GUIStyle();

	// Token: 0x04000D5D RID: 3421
	public GUIStyle MrBtn = new GUIStyle();

	// Token: 0x04000D5E RID: 3422
	private Texture2D wwwTexture;

	// Token: 0x04000D5F RID: 3423
	private Alpha buttonA = new Alpha();

	// Token: 0x04000D60 RID: 3424
	private Alpha texA = new Alpha();

	// Token: 0x04000D61 RID: 3425
	private Alpha splashAlpha = new Alpha();
}
