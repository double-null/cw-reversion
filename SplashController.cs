using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class SplashController : SingletoneComponent<SplashController>
{
	// Token: 0x1700085B RID: 2139
	// (get) Token: 0x06001DFA RID: 7674 RVA: 0x001052A0 File Offset: 0x001034A0
	internal static bool ExternalContent
	{
		get
		{
			return SingletoneComponent<SplashController>.Instance.commandLineArgs.HasValue("--externalContent") || SingletoneComponent<SplashController>.Instance.commandLineArgs.HasValue("--ec");
		}
	}

	// Token: 0x1700085C RID: 2140
	// (get) Token: 0x06001DFB RID: 7675 RVA: 0x001052E0 File Offset: 0x001034E0
	// (set) Token: 0x06001DFC RID: 7676 RVA: 0x001052E8 File Offset: 0x001034E8
	public static bool IsFirstStart { get; private set; }

	// Token: 0x06001DFD RID: 7677 RVA: 0x001052F0 File Offset: 0x001034F0
	private new void Awake()
	{
		Utility.FixResolution();
		SplashController.IsFirstStart = (PlayerPrefs.GetInt("FirstStart") == 0);
		if (SplashController.IsFirstStart)
		{
			PlayerPrefs.DeleteAll();
			PlayerPrefs.SetInt("FirstStart", 1);
		}
		Main.StandaloneMail = PlayerPrefs.GetString("Login");
		Main.StandalonePass = PlayerPrefs.GetString("Password");
		Main.SavePass = (PlayerPrefs.GetInt("SavePass") != 0);
		Main.PassHashed = (PlayerPrefs.HasKey("Password") && !string.IsNullOrEmpty(Main.StandalonePass));
		if (Main.SavePass && Main.PassHashed)
		{
			Main.StandalonePass = PlayerPrefs.GetString("Password");
		}
		else
		{
			Main.StandalonePass = string.Empty;
		}
		Language.CurrentLanguage = (ELanguage)PlayerPrefs.GetInt("Language");
	}

	// Token: 0x06001DFE RID: 7678 RVA: 0x001053C8 File Offset: 0x001035C8
	private void Start()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
		this.Initialize();
		Globals globals = base.gameObject.AddComponent<Globals>();
		globals.Initialize();
		HtmlLayer htmlLayer = base.gameObject.AddComponent<HtmlLayer>();
		htmlLayer.Initialize();
		GetRealmSettings getRealmSettings = base.gameObject.AddComponent<GetRealmSettings>();
		getRealmSettings.Initialize(new object[0]);
	}

	// Token: 0x06001DFF RID: 7679 RVA: 0x00105420 File Offset: 0x00103620
	internal IEnumerator OnGetRealmSettingsFinished()
	{
		if (CVars.realm == "ok")
		{
			yield return new WaitForSeconds(2f);
		}
		Globals.I.InitDownloadGlobals();
		Globals.I.Download();
		yield break;
	}

	// Token: 0x06001E00 RID: 7680 RVA: 0x00105434 File Offset: 0x00103634
	public void OnGlobalsDownloaded()
	{
		base.StartCoroutine(this.DownloadBuild());
	}

	// Token: 0x06001E01 RID: 7681 RVA: 0x00105444 File Offset: 0x00103644
	private IEnumerator DownloadBuild()
	{
		string sceneName = "build";
		if (Main.ExternalContent)
		{
			int version = 0;
			WWW meta = new WWW(WWWUtil.levelsWWW(sceneName).Replace(".unity3d", ".meta") + "?v=" + (int)DateTime.Now.TimeOfDay.TotalSeconds);
			yield return meta;
			if (meta.error != null)
			{
				Debug.LogError(meta.url + "\n" + meta.error);
				yield break;
			}
			if (meta.responseHeaders.TryGetValue("LAST-MODIFIED", out this.BuildVersion))
			{
				DateTime datetime = default(DateTime);
				if (DateTime.TryParse(this.BuildVersion, out datetime))
				{
					version = Mathf.Abs((int)datetime.TimeOfDay.TotalSeconds);
				}
			}
			Dictionary<string, object> dict = ArrayUtility.FromJSON(meta.text, string.Empty);
			JSON.ReadWrite(dict, "size", ref this._buildSize, false);
			this._buildSize /= 1024;
			string url = WWWUtil.levelsWWW(sceneName) + "?v=" + version;
			AssetBundleCreateRequest createRequest = null;
			if (CVars.IsStandaloneRealm)
			{
				createRequest = StandaloneCacher.GetCreateRequest(WWWUtil.levelsWWW(sceneName), version);
			}
			bool isCached;
			if (createRequest != null)
			{
				isCached = true;
				yield return createRequest;
				this._request = createRequest.assetBundle.LoadAsync(string.Empty, typeof(UnityEngine.Object));
			}
			else
			{
				this._downloader = ((!CVars.IsStandaloneRealm) ? WWW.LoadFromCacheOrDownload(url, version) : new WWW(url));
				isCached = Caching.IsVersionCached(this._downloader.url, version);
				yield return this._downloader;
				if (this._downloader.error != null)
				{
					Debug.LogError(this._downloader.url + "\n" + this._downloader.error);
					yield break;
				}
				if (CVars.IsStandaloneRealm)
				{
					StandaloneCacher.Cache(this._downloader.bytes, WWWUtil.levelsWWW(sceneName), version);
				}
				this._request = this._downloader.assetBundle.LoadAsync(string.Empty, typeof(UnityEngine.Object));
			}
			Debug.Log(string.Concat(new object[]
			{
				"LoadFromCacheOrDownload ",
				version,
				" cached = ",
				isCached
			}));
			if (this._downloader != null)
			{
				Debug.Log(this._downloader.url);
			}
			string buildVersion = this.BuildVersion;
			this.BuildVersion = string.Concat(new object[]
			{
				buildVersion,
				" v = ",
				version,
				"; cached = ",
				isCached
			});
			yield return this._request;
		}
		SingletoneComponent<SplashController>.Instance.buildLoaded = true;
		this._asyncOperation = Application.LoadLevelAsync(sceneName);
		yield return this._asyncOperation;
		Main main = UnityEngine.Object.FindObjectOfType(typeof(Main)) as Main;
		main.Initialize();
		this._downloader = null;
		yield break;
	}

	// Token: 0x06001E02 RID: 7682 RVA: 0x00105460 File Offset: 0x00103660
	private void OnGUI()
	{
		if (this.buildLoaded)
		{
			return;
		}
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		this.RotateGUI(angle, new Vector2((float)(Screen.width - 130 + this.KrutilkaSmall.width / 2), (float)(Screen.height - 95 + this.KrutilkaSmall.height / 2)));
		GUI.DrawTexture(new Rect((float)(Screen.width - 130), (float)(Screen.height - 95), (float)this.KrutilkaSmall.width, (float)this.KrutilkaSmall.height), this.KrutilkaSmall);
		this.RotateGUI(0f, Vector2.zero);
		if (this._downloader != null)
		{
			int num = (int)(this._downloader.progress * 100f);
			GUI.Label(new Rect((float)(Screen.width - 238), (float)(Screen.height - 78), 100f, 50f), num + "%", this.PercentStyle);
			GUI.Label(new Rect((float)(Screen.width - 238), (float)(Screen.height - 80), 100f, 50f), string.Concat(new object[]
			{
				this._buildSize * num / 100,
				" / ",
				this._buildSize,
				" KB"
			}), this.BytesStyle);
		}
		else
		{
			GUI.Label(new Rect((float)(Screen.width - 238), (float)(Screen.height - 78), 100f, 50f), Language.LoadingBuild, this.LoadingBuildStyle);
		}
	}

	// Token: 0x06001E03 RID: 7683 RVA: 0x00105618 File Offset: 0x00103818
	public void RotateGUI(float angle, Vector2 center)
	{
		if (angle == 0f)
		{
			GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one);
		}
		else
		{
			GUIUtility.RotateAroundPivot(angle, center);
		}
	}

	// Token: 0x06001E04 RID: 7684 RVA: 0x00105658 File Offset: 0x00103858
	internal void ParseCommandLineArgs(string commandLineIn)
	{
		if (!Application.isEditor && !commandLineIn.Contains(" --externalContent "))
		{
			commandLineIn += " --externalContent ";
		}
		if (commandLineIn.Contains("--standalone"))
		{
			CVars.realm = "standalone";
		}
		else if (commandLineIn.Contains("--mailru"))
		{
			CVars.realm = "mailru";
		}
		else if (commandLineIn.Contains("--omega"))
		{
			CVars.realm = "local";
		}
		else if (commandLineIn.Contains("--alfa2"))
		{
			CVars.realm = "local";
		}
		else if (commandLineIn.Contains("--ok"))
		{
			CVars.realm = "ok";
		}
		else if (commandLineIn.Contains("--kg"))
		{
			CVars.realm = "kg";
		}
		else if (commandLineIn.Contains("--ag"))
		{
			CVars.realm = "ag";
		}
		else if (commandLineIn.Contains("--mc"))
		{
			CVars.realm = "mc";
		}
		else if (commandLineIn.Contains("--yg"))
		{
			CVars.realm = "yg";
		}
		else if (commandLineIn.Contains("--fb"))
		{
			CVars.realm = "fb";
		}
		else if (commandLineIn.Contains("--fb-omega"))
		{
			CVars.realm = "fb-omega";
		}
		else if (commandLineIn.Contains("--local"))
		{
			CVars.realm = "local";
		}
		else if (commandLineIn.Contains("--admin"))
		{
			CVars.realm = "local";
		}
		else if (commandLineIn.Contains("--fr"))
		{
			CVars.realm = "fr";
		}
		else if (commandLineIn.Contains("--vk"))
		{
			CVars.realm = "vk";
		}
		else
		{
			CVars.realm = "release";
		}
		if (!CVars.IsStandaloneRealm)
		{
			Language.CurrentLanguage = ((!commandLineIn.Contains("--ENG")) ? ELanguage.RU : ELanguage.EN);
		}
		if (commandLineIn.Contains("--hc"))
		{
			CVars.hardcore = true;
		}
		this.commandLine = commandLineIn;
		this.commandLineArgs = new CommandLineArgs(commandLineIn);
		if (this.commandLineArgs.HasValue("--vanilla"))
		{
			CVars.IsVanilla = true;
		}
		if (this.commandLineArgs.HasValue("--uselayer"))
		{
			CVars.n_protocol = "https://";
		}
		if (this.commandLineArgs.HasValue("--contuselayer"))
		{
			CVars.n_contentProtocol = "https://";
		}
		if (this.commandLineArgs.HasValue("--standalone") || this.commandLineArgs.HasValue("--mailru") || this.commandLineArgs.HasValue("--ok") || this.commandLineArgs.HasValue("--vk") || this.commandLineArgs.HasValue("--fb") || this.commandLineArgs.HasValue("--mc") || this.commandLineArgs.HasValue("--ag") || this.commandLineArgs.HasValue("--kg"))
		{
			Globals.I.databaseRoot = "/";
			if (this.commandLineArgs.HasValue("--dev"))
			{
				Globals.I.databaseIP = "dev.contractwarsgame.com";
			}
			else if (this.commandLineArgs.HasValue("--dev2"))
			{
				Globals.I.databaseIP = "dev2.contractwarsgame.com";
			}
			else if (this.commandLineArgs.HasValue("--vanilla"))
			{
				Globals.I.databaseIP = "cw-revival.contractwarsgame.com";
			}
			else
			{
				Globals.I.databaseIP = "gw-01.contractwarsgame.com";
			}
		}
		else if (this.commandLineArgs.HasValue("--local"))
		{
			Globals.I.databaseRoot = "/";
			Globals.I.databaseIP = "192.168.1.118";
		}
		else if (this.commandLineArgs.HasValue("--admin"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "srv-local.absc.local";
		}
		else if (this.commandLineArgs.HasValue("--omega"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "omega.contractwarsgame.com";
		}
		else if (this.commandLineArgs.HasValue("--alfa2"))
		{
			Globals.I.databaseRoot = "/";
			Globals.I.databaseIP = "alfa2.contractwarsgame.com";
		}
		else if (this.commandLineArgs.HasValue("--fb-omega"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "omega.contractwarsgame.com";
		}
		else if (this.commandLineArgs.HasValue("--yg"))
		{
			Globals.I.databaseRoot = "/cwyg/";
			Globals.I.databaseIP = "prod4.contractwarsgame.com";
		}
		else if (this.commandLineArgs.HasValue("--release"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "31.186.102.219";
		}
		else
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "31.186.102.219";
		}
		if (this.commandLineArgs.HasValue("--realm"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = this.commandLineArgs.TryGetValue("--realm");
			Debug.Log(Dns.GetHostEntry(Globals.I.databaseIP).AddressList[0].ToString());
		}
		if (this.commandLineArgs.HasValue("--root"))
		{
			Globals.I.databaseRoot = this.commandLineArgs.TryGetValue("--root");
			if (string.IsNullOrEmpty(Globals.I.databaseRoot))
			{
				Globals.I.databaseRoot = "/cw/";
			}
		}
	}

	// Token: 0x0400225B RID: 8795
	private AsyncOperation _asyncOperation;

	// Token: 0x0400225C RID: 8796
	private WWW _downloader;

	// Token: 0x0400225D RID: 8797
	private AssetBundleRequest _request;

	// Token: 0x0400225E RID: 8798
	public string commandLine = string.Empty;

	// Token: 0x0400225F RID: 8799
	internal CommandLineArgs commandLineArgs = new CommandLineArgs(string.Empty);

	// Token: 0x04002260 RID: 8800
	private int _buildSize;

	// Token: 0x04002261 RID: 8801
	public Texture2D KrutilkaSmall;

	// Token: 0x04002262 RID: 8802
	public GUIStyle PercentStyle;

	// Token: 0x04002263 RID: 8803
	public GUIStyle LoadingBuildStyle;

	// Token: 0x04002264 RID: 8804
	public GUIStyle BytesStyle;

	// Token: 0x04002265 RID: 8805
	public string BuildVersion = "buildVersion";

	// Token: 0x04002266 RID: 8806
	public bool buildLoaded;
}
