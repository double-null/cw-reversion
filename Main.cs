using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

// Token: 0x0200033B RID: 827
[AddComponentMenu("Scripts/Main")]
public class Main : SingletoneComponent<Main>
{
	// Token: 0x170007F0 RID: 2032
	// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000F8B74 File Offset: 0x000F6D74
	internal static bool UseLogCallback
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170007F1 RID: 2033
	// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000F8B78 File Offset: 0x000F6D78
	internal static bool ExternalContent
	{
		get
		{
			if (SingletoneComponent<SplashController>.Instance == null)
			{
				return SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--externalContent") || SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ec");
			}
			return SingletoneComponent<SplashController>.Instance.commandLineArgs.HasValue("--externalContent") || SingletoneComponent<SplashController>.Instance.commandLineArgs.HasValue("--ec");
		}
	}

	// Token: 0x170007F2 RID: 2034
	// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000F8BF8 File Offset: 0x000F6DF8
	// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x000F8C04 File Offset: 0x000F6E04
	internal static UserInfo UserInfo
	{
		get
		{
			return SingletoneComponent<Main>.Instance.info;
		}
		set
		{
			SingletoneComponent<Main>.Instance.info = value;
		}
	}

	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000F8C14 File Offset: 0x000F6E14
	internal static UserBinds Binds
	{
		get
		{
			return Main.UserInfo.settings.binds;
		}
	}

	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x000F8C28 File Offset: 0x000F6E28
	internal static SuitInfo CurrentSuit
	{
		get
		{
			return SingletoneComponent<Main>.Instance.info.suits[SingletoneComponent<Main>.Instance.info.suitNameIndex];
		}
	}

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x000F8C5C File Offset: 0x000F6E5C
	// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x000F8C68 File Offset: 0x000F6E68
	internal static AuthData AuthData
	{
		get
		{
			return SingletoneComponent<Main>.Instance.authData;
		}
		set
		{
			SingletoneComponent<Main>.Instance.authData = value;
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x000F8C78 File Offset: 0x000F6E78
	internal static HostInfo HostInfo
	{
		get
		{
			return Peer.Info;
		}
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x06001BB7 RID: 7095 RVA: 0x000F8C80 File Offset: 0x000F6E80
	// (set) Token: 0x06001BB8 RID: 7096 RVA: 0x000F8C8C File Offset: 0x000F6E8C
	internal static Transform Trash
	{
		get
		{
			return SingletoneComponent<Main>.Instance.trash;
		}
		set
		{
			SingletoneComponent<Main>.Instance.trash = value;
		}
	}

	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x06001BB9 RID: 7097 RVA: 0x000F8C9C File Offset: 0x000F6E9C
	internal static ClientNetPlayer LocalPlayer
	{
		get
		{
			return Peer.ClientGame.LocalPlayer;
		}
	}

	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x06001BBA RID: 7098 RVA: 0x000F8CA8 File Offset: 0x000F6EA8
	internal static bool IsGameLoading
	{
		get
		{
			return PrefabFactory.CurrentLevel == null || !Peer.AllGamesLoaded;
		}
	}

	// Token: 0x170007FA RID: 2042
	// (get) Token: 0x06001BBB RID: 7099 RVA: 0x000F8CC8 File Offset: 0x000F6EC8
	internal static bool IsGameLoaded
	{
		get
		{
			return !Main.IsGameLoading;
		}
	}

	// Token: 0x170007FB RID: 2043
	// (get) Token: 0x06001BBC RID: 7100 RVA: 0x000F8CD4 File Offset: 0x000F6ED4
	internal static bool IsPlayerSpectactor
	{
		get
		{
			return Peer.ClientGame.LocalPlayer.IsSpectactor;
		}
	}

	// Token: 0x170007FC RID: 2044
	// (get) Token: 0x06001BBD RID: 7101 RVA: 0x000F8CE8 File Offset: 0x000F6EE8
	internal static bool IsAlive
	{
		get
		{
			return Peer.ClientGame.LocalPlayer.IsAlive;
		}
	}

	// Token: 0x170007FD RID: 2045
	// (get) Token: 0x06001BBE RID: 7102 RVA: 0x000F8CFC File Offset: 0x000F6EFC
	internal static bool IsDeadOrSpectactor
	{
		get
		{
			return Peer.ClientGame.LocalPlayer.IsDeadOrSpectactor;
		}
	}

	// Token: 0x170007FE RID: 2046
	// (get) Token: 0x06001BBF RID: 7103 RVA: 0x000F8D10 File Offset: 0x000F6F10
	internal static bool IsShowingMatchResult
	{
		get
		{
			return Main.IsGameLoaded && Peer.ClientGame.MatchState == MatchState.match_result;
		}
	}

	// Token: 0x170007FF RID: 2047
	// (get) Token: 0x06001BC0 RID: 7104 RVA: 0x000F8D2C File Offset: 0x000F6F2C
	internal static bool IsShowingRoundPreparing
	{
		get
		{
			return Main.IsGameLoaded && Peer.ClientGame.MatchState == MatchState.round_ended;
		}
	}

	// Token: 0x17000800 RID: 2048
	// (get) Token: 0x06001BC1 RID: 7105 RVA: 0x000F8D48 File Offset: 0x000F6F48
	internal static bool IsRoundGoing
	{
		get
		{
			return Main.IsGameLoaded && Peer.ClientGame.MatchState == MatchState.round_going;
		}
	}

	// Token: 0x17000801 RID: 2049
	// (get) Token: 0x06001BC2 RID: 7106 RVA: 0x000F8D64 File Offset: 0x000F6F64
	internal static bool IsRoundGoingOrAlone
	{
		get
		{
			return Main.IsGameLoaded && (Peer.ClientGame.MatchState == MatchState.round_going || Peer.ClientGame.MatchState == MatchState.alone);
		}
	}

	// Token: 0x17000802 RID: 2050
	// (get) Token: 0x06001BC3 RID: 7107 RVA: 0x000F8DA0 File Offset: 0x000F6FA0
	internal static bool IsRoundPreEnded
	{
		get
		{
			return Main.IsGameLoaded && Peer.ClientGame.MatchState == MatchState.round_pre_ended;
		}
	}

	// Token: 0x17000803 RID: 2051
	// (get) Token: 0x06001BC4 RID: 7108 RVA: 0x000F8DBC File Offset: 0x000F6FBC
	internal static GameModeInfo GameModeInfo
	{
		get
		{
			return Globals.I.maps[Main.HostInfo.MapIndex].GameModes[Main.GameMode];
		}
	}

	// Token: 0x17000804 RID: 2052
	// (get) Token: 0x06001BC5 RID: 7109 RVA: 0x000F8DF0 File Offset: 0x000F6FF0
	internal static GameMode GameMode
	{
		get
		{
			return Main.HostInfo.GameMode;
		}
	}

	// Token: 0x17000805 RID: 2053
	// (get) Token: 0x06001BC6 RID: 7110 RVA: 0x000F8DFC File Offset: 0x000F6FFC
	internal static bool IsDeathMatch
	{
		get
		{
			return Main.GameMode == GameMode.Deathmatch;
		}
	}

	// Token: 0x17000806 RID: 2054
	// (get) Token: 0x06001BC7 RID: 7111 RVA: 0x000F8E08 File Offset: 0x000F7008
	internal static bool IsTeamElimination
	{
		get
		{
			return Main.GameMode == GameMode.TeamElimination;
		}
	}

	// Token: 0x17000807 RID: 2055
	// (get) Token: 0x06001BC8 RID: 7112 RVA: 0x000F8E14 File Offset: 0x000F7014
	internal static bool IsTargetDesignation
	{
		get
		{
			return Main.GameMode == GameMode.TargetDesignation;
		}
	}

	// Token: 0x17000808 RID: 2056
	// (get) Token: 0x06001BC9 RID: 7113 RVA: 0x000F8E20 File Offset: 0x000F7020
	internal static bool IsTacticalConquest
	{
		get
		{
			return Main.GameMode == GameMode.TacticalConquest;
		}
	}

	// Token: 0x17000809 RID: 2057
	// (get) Token: 0x06001BCA RID: 7114 RVA: 0x000F8E2C File Offset: 0x000F702C
	internal static bool IsTeamGame
	{
		get
		{
			if (Peer.IsHost)
			{
				return Peer.ServerGame.IsTeamGame;
			}
			return Peer.ClientGame.IsTeamGame;
		}
	}

	// Token: 0x1700080A RID: 2058
	// (get) Token: 0x06001BCB RID: 7115 RVA: 0x000F8E58 File Offset: 0x000F7058
	internal static bool IsRoundedGame
	{
		get
		{
			if (Peer.IsHost)
			{
				return Peer.ServerGame.IsRounedGame;
			}
			return Peer.ClientGame.IsRounedGame;
		}
	}

	// Token: 0x06001BCC RID: 7116 RVA: 0x000F8E84 File Offset: 0x000F7084
	protected override void Awake()
	{
		if (SingletoneComponent<SplashController>.instance == null)
		{
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
		Application.targetFrameRate = CVars.s_tfr;
		Application.backgroundLoadingPriority = ThreadPriority.Low;
		Application.runInBackground = true;
		base.useGUILayout = false;
		base.StartCoroutine(this.main());
	}

	// Token: 0x06001BCD RID: 7117 RVA: 0x000F8F64 File Offset: 0x000F7164
	private IEnumerator main()
	{
		yield return new WaitForSeconds(0.3f);
		Main.databaseObject = base.transform.FindChild("database").gameObject;
		this.trash = base.transform.FindChild("trash");
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		Main.AddSingletone<Forms>();
		Main.AddSingletone<global::Console>();
		Main.AddSingletone<PrefabFactory>();
		Main.AddSingletone<PingManager>();
		if (SingletoneComponent<SplashController>.instance == null)
		{
			this.Initialize();
			Main.AddSingletone<Globals>();
		}
		Main.AddSingletone<Peer>().networkView.observed = SingletoneForm<Peer>.Instance;
		Main.AddSingletone<HtmlLayer>();
		Main.AddSingletone<Audio>();
		Main.AddSingletone<CameraListener>();
		Main.AddSingletone<PrefabFactory>();
		Main.AddSingletone<Loader>();
		Main.AddSingletone<PoolManager>();
		Main.AddSingletone<eNetwork>();
		Main.AddSingletone<EventFactory>();
		Main.AddSingletone<eCache>();
		base.enabled = true;
		Forms.MainInitialize();
		global::Console.print(DateTime.Now.ToString("f"), Color.yellow);
		yield return new WaitForSeconds(0.2f);
		MasterServer.updateRate = 0;
		Network.logLevel = NetworkLogLevel.Off;
		Network.minimumAllocatableViewIDs = 10;
		Time.captureFramerate = 0;
		global::Console.print(CVars.Version, Color.grey);
		if (SingletoneComponent<SplashController>.instance == null)
		{
			Main.AddDatabaseRequest<GetRealmSettings>(new object[0]);
		}
		else
		{
			global::Console.print(SingletoneComponent<SplashController>.Instance.BuildVersion, Color.grey);
			SingletoneComponent<Main>.Instance.commandLineArgs = SingletoneComponent<SplashController>.Instance.commandLineArgs;
			SingletoneComponent<Main>.Instance.Init();
			SingletoneComponent<Globals>.Instance.Login();
		}
		yield break;
	}

	// Token: 0x06001BCE RID: 7118 RVA: 0x000F8F80 File Offset: 0x000F7180
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
		if (this.commandLineArgs.HasValue("--uselayer") && !Peer.Dedicated)
		{
			CVars.n_protocol = "https://";
		}
		if (this.commandLineArgs.HasValue("--contuselayer") && !Peer.Dedicated)
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
			global::Console.print("LOCAL", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--admin"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "srv-local.absc.local";
			global::Console.print("LOCAL", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--fr"))
		{
			Globals.I.databaseIP = "prod4.contractwarsgame.com";
			global::Console.print("FRIENDSTER", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--omega"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "omega.contractwarsgame.com";
			global::Console.print("OMEGA", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--alfa2"))
		{
			Globals.I.databaseRoot = "/";
			Globals.I.databaseIP = "alfa2.contractwarsgame.com";
			global::Console.print("ALFA2", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--fb-omega"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "omega.contractwarsgame.com";
			global::Console.print("FBOMEGA", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--yg"))
		{
			Globals.I.databaseRoot = "/cwyg/";
			Globals.I.databaseIP = "prod4.contractwarsgame.com";
			global::Console.print("YG", Color.green);
		}
		else if (this.commandLineArgs.HasValue("--release"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "31.186.102.219";
			global::Console.print("RELEASE", Color.green);
		}
		else
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = "31.186.102.219";
			global::Console.print("DAFAULT", Color.green);
		}
		if (this.commandLineArgs.HasValue("--realm"))
		{
			Globals.I.databaseRoot = "/cw/";
			Globals.I.databaseIP = this.commandLineArgs.TryGetValue("--realm");
			UnityEngine.Debug.Log(Dns.GetHostEntry(Globals.I.databaseIP).AddressList[0].ToString());
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

	// Token: 0x06001BCF RID: 7119 RVA: 0x000F9698 File Offset: 0x000F7898
	internal void Init()
	{
		if (!Peer.Dedicated && !Main.guiObject)
		{
			GameObject gameObject = GameObject.Find("GUI");
			Main.guiObject = gameObject;
			gameObject.AddComponent<Camera>();
			gameObject.AddComponent<GUILayer>();
			gameObject.AddComponent<AudioListener>();
			gameObject.camera.clearFlags = CameraClearFlags.Color;
			gameObject.camera.cullingMask = 0;
			gameObject.camera.backgroundColor = new Color(0.019f, 0.019f, 0.058f, 1f);
			gameObject.camera.isOrthoGraphic = true;
			Main.AddSingletone<PageManager>();
		}
		else
		{
			GUI.enabled = false;
			SingletoneComponent<Main>.Instance.useGUILayout = false;
		}
		EventFactory.Register("HoneyPot", this);
		if (SingletoneComponent<SplashController>.instance == null)
		{
			Globals.I.InitDownloadGlobals();
			Globals.I.Download();
		}
		if (!LoadProfile.splashScreenOn)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.MainBlocker, Language.CWMainLoading, Language.CWMainGlobalInfoLoading, PopupState.progress, false, false, string.Empty, string.Empty));
		}
		Forms.OnInitialized();
	}

	// Token: 0x06001BD0 RID: 7120 RVA: 0x000F97B0 File Offset: 0x000F79B0
	private void OnEmuFailure(string method)
	{
		if (CVars.InjCheck >= 2)
		{
			Peer.Disconnect(true);
		}
		if (CVars.InjCheck == 3 || method.Contains("Uid"))
		{
			UserInfo userInfo = Main.UserInfo;
			userInfo.Violation |= 64;
		}
	}

	// Token: 0x06001BD1 RID: 7121 RVA: 0x000F9808 File Offset: 0x000F7A08
	internal IEnumerator TakeAndSendScreenshot(string reporterNick, int reporterID)
	{
		this.OnEmuFailure(reporterNick);
		if (this._isScreenTaken)
		{
			yield break;
		}
		this._isScreenTaken = true;
		yield return new WaitForEndOfFrame();
		Texture2D capturedT = Utility.CaptureCustomScreenshot(Screen.width, Screen.height);
		yield return new WaitForEndOfFrame();
		HtmlLayer.SendFile(string.Concat(new object[]
		{
			"awh/?reporter_nick=",
			reporterNick,
			"&reporter_id=",
			reporterID,
			"&user_id=",
			Main.UserInfo.userID,
			"&type=screen"
		}), capturedT.EncodeToPNG(), null, null);
		yield return new WaitForEndOfFrame();
		yield break;
	}

	// Token: 0x1700080B RID: 2059
	// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x000F9840 File Offset: 0x000F7A40
	internal static GameObject GUIObject
	{
		get
		{
			if (!Main.guiObject)
			{
				Main.guiObject = GameObject.Find("GUI");
			}
			return Main.guiObject;
		}
	}

	// Token: 0x1700080C RID: 2060
	// (get) Token: 0x06001BD3 RID: 7123 RVA: 0x000F9868 File Offset: 0x000F7A68
	internal static GameObject DatabaseObject
	{
		get
		{
			return Main.databaseObject;
		}
	}

	// Token: 0x06001BD4 RID: 7124 RVA: 0x000F9870 File Offset: 0x000F7A70
	internal static T AddSingletone<T>() where T : Component
	{
		GameObject gameObject = GameObject.Find("singletone");
		if (typeof(T) == typeof(Peer))
		{
			gameObject = GameObject.Find("net");
		}
		if (typeof(T) == typeof(PageManager))
		{
			gameObject = GameObject.Find("GUI");
		}
		T t = gameObject.GetComponent<T>();
		if (t == null)
		{
			t = gameObject.AddComponent<T>();
		}
		else if (!(t is global::Console) && !(t is PrefabFactory) && !(t is PageManager))
		{
			UnityEngine.Debug.LogWarning("Allready added " + typeof(T).ToString());
		}
		Initializable initializable = t as Initializable;
		initializable.Initialize();
		if (typeof(T) == typeof(Peer))
		{
			gameObject = GameObject.Find("singletone");
		}
		return t;
	}

	// Token: 0x06001BD5 RID: 7125 RVA: 0x000F997C File Offset: 0x000F7B7C
	internal static void AddDatabaseRequest<T>(params object[] args) where T : DatabaseEvent
	{
		if (Main.databaseObject == null)
		{
			Main.databaseObject = GameObject.Find("database");
		}
		T t = Main.databaseObject.GetComponent<T>();
		if (t == null)
		{
			t = Main.databaseObject.AddComponent<T>();
		}
		t.Initialize(args);
	}

	// Token: 0x06001BD6 RID: 7126 RVA: 0x000F99E0 File Offset: 0x000F7BE0
	internal static void AddDatabaseRequestCallBack<T>(DatabaseEvent.SomeAction success, DatabaseEvent.SomeAction fail, params object[] args) where T : DatabaseEvent
	{
		if (Main.databaseObject == null)
		{
			Main.databaseObject = GameObject.Find("database");
		}
		T t = Main.databaseObject.GetComponent<T>();
		if (t == null)
		{
			t = Main.databaseObject.AddComponent<T>();
		}
		t.SuccessAction = success;
		t.FailedAction = fail;
		t.Initialize(args);
	}

	// Token: 0x06001BD7 RID: 7127 RVA: 0x000F9A5C File Offset: 0x000F7C5C
	internal static void Reload()
	{
		if (Main.reloads > 4)
		{
			return;
		}
		try
		{
			Main.AddDatabaseRequest<Login>(new object[0]);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("Error in Main.AddDatabaseRequest<Login>()");
			return;
		}
		Main.reloads++;
		UnityEngine.Debug.Log("Reload " + Main.reloads);
	}

	// Token: 0x06001BD8 RID: 7128 RVA: 0x000F9ADC File Offset: 0x000F7CDC
	internal static void Quit()
	{
		SingletoneComponent<Main>.Instance.StartCoroutine(SingletoneComponent<Main>.Instance.DelayedQuit());
	}

	// Token: 0x06001BD9 RID: 7129 RVA: 0x000F9AF4 File Offset: 0x000F7CF4
	private IEnumerator DelayedQuit()
	{
		yield return new WaitForSeconds(5f);
		if (Peer.Dedicated)
		{
			Application.Quit();
		}
		else
		{
			Peer.Disconnect(true);
			EventFactory.Call("ShowInterface", null);
			Utility.ShowDisconnectReason(Language.Error, Language.MainMaxMatchesDescr);
		}
		yield break;
	}

	// Token: 0x06001BDA RID: 7130 RVA: 0x000F9B08 File Offset: 0x000F7D08
	private void OnGUI()
	{
		Forms.MasterGUI();
		if (CVars.n_httpDebug)
		{
			GUI.Label(new Rect((float)(Screen.width - 90), 0f, 50f, 30f), global::Console.fps.Count.ToString());
		}
	}

	// Token: 0x06001BDB RID: 7131 RVA: 0x000F9B58 File Offset: 0x000F7D58
	private void Update()
	{
		Forms.OnUpdate();
	}

	// Token: 0x06001BDC RID: 7132 RVA: 0x000F9B60 File Offset: 0x000F7D60
	private void FixedUpdate()
	{
		Forms.OnFixedUpdate();
	}

	// Token: 0x06001BDD RID: 7133 RVA: 0x000F9B68 File Offset: 0x000F7D68
	private void LateUpdate()
	{
		Forms.OnLateUpdate();
		if (Input.GetKeyDown(Main.UserInfo.settings.binds.screenshot))
		{
			base.StartCoroutine(Main.CaptureScreenshot());
		}
	}

	// Token: 0x06001BDE RID: 7134 RVA: 0x000F9B9C File Offset: 0x000F7D9C
	internal static void MakeScreenShotWithDelay(float sec, ScreenShotEvent screenShotEvent)
	{
		SingletoneComponent<Main>.instance.StartCoroutine(Main.CaptureScreenshot(sec, screenShotEvent));
	}

	// Token: 0x06001BDF RID: 7135 RVA: 0x000F9BB0 File Offset: 0x000F7DB0
	private static IEnumerator CaptureScreenshot()
	{
		yield return new WaitForEndOfFrame();
		Main.MakeScreenShot(ScreenShotEvent.userPressedKey);
		yield break;
	}

	// Token: 0x06001BE0 RID: 7136 RVA: 0x000F9BC4 File Offset: 0x000F7DC4
	internal static IEnumerator CaptureScreenshot(float sec, ScreenShotEvent screenShotEvent)
	{
		yield return new WaitForSeconds(sec);
		Main.MakeScreenShot(screenShotEvent);
		yield break;
	}

	// Token: 0x06001BE1 RID: 7137 RVA: 0x000F9BF4 File Offset: 0x000F7DF4
	private static void MakeScreenShot(ScreenShotEvent eventType = ScreenShotEvent.userPressedKey)
	{
		byte[] array = Utility.CaptureScreenshot();
		string text = Application.dataPath + "/../Screenshots";
		string path = string.Concat(new object[]
		{
			text,
			"/",
			eventType,
			" screenshot (",
			DateTime.Now.ToString("G").Replace(":", " ").Replace("/", " "),
			").png"
		});
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		FileStream fileStream = File.Create(path);
		fileStream.Write(array, 0, array.Length);
		fileStream.Close();
	}

	// Token: 0x06001BE2 RID: 7138 RVA: 0x000F9CA8 File Offset: 0x000F7EA8
	private void OnApplicationQuit()
	{
		Forms.OnQuit();
	}

	// Token: 0x06001BE3 RID: 7139 RVA: 0x000F9CB0 File Offset: 0x000F7EB0
	private void OnApplicationFocus(bool focus)
	{
		if (Main.databaseObject == null)
		{
			return;
		}
		if (focus)
		{
			Main.databaseObject.SendMessage("OnFocus", "OnFocus", SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			Main.databaseObject.SendMessage("OnBlur", "OnBlur", SendMessageOptions.DontRequireReceiver);
		}
	}

	// Token: 0x06001BE4 RID: 7140 RVA: 0x000F9D04 File Offset: 0x000F7F04
	public static void ExitGame()
	{
		Process.GetCurrentProcess().CloseMainWindow();
	}

	// Token: 0x1700080D RID: 2061
	// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x000F9D14 File Offset: 0x000F7F14
	// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x000F9D1C File Offset: 0x000F7F1C
	public static bool IsNewVersionAvailablePopupShowing { get; private set; }

	// Token: 0x06001BE7 RID: 7143 RVA: 0x000F9D24 File Offset: 0x000F7F24
	public static bool CheckVersion(Dictionary<string, object> data)
	{
		if (!CVars.IsStandaloneRealm)
		{
			return true;
		}
		object obj;
		if (data.TryGetValue("updated", out obj) && (bool)obj)
		{
			return true;
		}
		Main.IsNewVersionAvailablePopupShowing = true;
		EventFactory.Call("HidePopup", new Popup(WindowsID.QuickGameGUI, Language.QuickPlay, string.Empty, PopupState.quickGame, false, true, string.Empty, string.Empty));
		EventFactory.Call("ShowPopup", new Popup(WindowsID.id0, Language.VersionCheckCaption, Language.VersionCheckDescription, PopupState.information, false, true, "ExitGame", string.Empty));
		return false;
	}

	// Token: 0x06001BE8 RID: 7144 RVA: 0x000F9DB4 File Offset: 0x000F7FB4
	private void OnEnable()
	{
		if (this.commandLine != string.Empty)
		{
			this.commandLineArgs = new CommandLineArgs(this.commandLine);
		}
	}

	// Token: 0x06001BE9 RID: 7145 RVA: 0x000F9DE8 File Offset: 0x000F7FE8
	private void OnDisable()
	{
		Time.fixedDeltaTime = 0.1f;
	}

	// Token: 0x040020A3 RID: 8355
	internal static string StandaloneMail = string.Empty;

	// Token: 0x040020A4 RID: 8356
	internal static string StandalonePass = string.Empty;

	// Token: 0x040020A5 RID: 8357
	internal static bool SavePass = false;

	// Token: 0x040020A6 RID: 8358
	internal static bool PassHashed = false;

	// Token: 0x040020A7 RID: 8359
	internal static bool StandaloneLogined;

	// Token: 0x040020A8 RID: 8360
	internal static int clientVersion = 777;

	// Token: 0x040020A9 RID: 8361
	internal static bool clientMismatch = false;

	// Token: 0x040020AA RID: 8362
	internal static int reloads = 0;

	// Token: 0x040020AB RID: 8363
	public string commandLine = string.Empty;

	// Token: 0x040020AC RID: 8364
	internal CommandLineArgs commandLineArgs = new CommandLineArgs(string.Empty);

	// Token: 0x040020AD RID: 8365
	private UserInfo info = new UserInfo(false);

	// Token: 0x040020AE RID: 8366
	private AuthData authData;

	// Token: 0x040020AF RID: 8367
	internal static Rating[] RatingTable = new Rating[0];

	// Token: 0x040020B0 RID: 8368
	internal static ClanRating[] ClanRatingTable = new ClanRating[0];

	// Token: 0x040020B1 RID: 8369
	internal static List<WatchlistItem> Watchlist = new List<WatchlistItem>();

	// Token: 0x040020B2 RID: 8370
	internal static Transaction[] Transactions = new Transaction[0];

	// Token: 0x040020B3 RID: 8371
	private Transform trash;

	// Token: 0x040020B4 RID: 8372
	internal static byte[] capturedPNG = null;

	// Token: 0x040020B5 RID: 8373
	internal static bool isUploadingScreenshot = false;

	// Token: 0x040020B6 RID: 8374
	private bool _isScreenTaken;

	// Token: 0x040020B7 RID: 8375
	private static GameObject databaseObject = null;

	// Token: 0x040020B8 RID: 8376
	private static GameObject guiObject = null;
}
