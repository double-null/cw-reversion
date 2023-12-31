using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using DownloadGlobals;
using Ionic.Zip;
using SocketHttp;
using UnityEngine;

// Token: 0x020000AE RID: 174
[AddComponentMenu("Scripts/Engine/Forms")]
internal class Globals : SingletoneComponent<Globals>, Convertible
{
	// Token: 0x17000088 RID: 136
	// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001B6D8 File Offset: 0x000198D8
	public static Globals I
	{
		get
		{
			return SingletoneComponent<Globals>.Instance;
		}
	}

	// Token: 0x17000089 RID: 137
	// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001B6E0 File Offset: 0x000198E0
	// (set) Token: 0x0600040A RID: 1034 RVA: 0x0001B6E8 File Offset: 0x000198E8
	public string databaseIP
	{
		get
		{
			return this._dpip;
		}
		set
		{
			if (!this._lockDBIP)
			{
				this._dpip = value;
			}
		}
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001B6FC File Offset: 0x000198FC
	public void lockDBIP()
	{
		this._lockDBIP = true;
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001B708 File Offset: 0x00019908
	public void unlockDBIP()
	{
		this._lockDBIP = false;
	}

	// Token: 0x1700008A RID: 138
	// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001B714 File Offset: 0x00019914
	public bool IsSeasonGoing
	{
		get
		{
			return this.OngoingSeason != 0;
		}
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001B724 File Offset: 0x00019924
	[Obfuscation(Exclude = true)]
	public void WrongURL()
	{
		global::Console.print("WrongURL");
		base.StopAllCoroutines();
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x0001B744 File Offset: 0x00019944
	public void SetActualBackEnd()
	{
		this.unlockDBIP();
		this.databaseIP = "gw-01.contractwarsgame.com";
		this.lockDBIP();
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001B760 File Offset: 0x00019960
	public void SetVanillaBackEnd()
	{
		this.unlockDBIP();
		this.databaseIP = "cw-revival.contractwarsgame.com";
		this.lockDBIP();
	}

	// Token: 0x06000411 RID: 1041 RVA: 0x0001B77C File Offset: 0x0001997C
	public void InitDownloadGlobals()
	{
		if (CVars.IsStandaloneBackend())
		{
			this.NewInitGlobalDatas();
		}
		else
		{
			this.OldInitGlobalDatas();
		}
		this.QueueDownloads.Add(this.dataglobals);
		this.QueueDownloads.Add(this.dataWeapons);
		this.QueueDownloads.Add(this.dataMaps);
		this.QueueDownloads.Add(this.dataAchievements);
		this.QueueDownloads.Add(this.dataSkills);
		this.QueueDownloads.Add(this.dataTasks);
		this.QueueDownloads.Add(this.dataBoxes);
		this.QueueDownloads.Add(this.dataClanskills);
		this.QueueDownloads.Add(this.dataHints);
		this.QueueDownloads.Add(this.dataBank);
		this.QueueDownloads.Add(this.dataRoulette);
		if (CVars.IsStandaloneRealm)
		{
			this.QueueDownloads.Add(this.dataWeapons2ndLang);
			this.QueueDownloads.Add(this.dataAchievements2ndLang);
			this.QueueDownloads.Add(this.dataSkills2ndLang);
			this.QueueDownloads.Add(this.dataClanskills2ndLang);
			this.QueueDownloads.Add(this.dataHints2ndLang);
			this.QueueDownloads.Add(this.dataTasks2ndLang);
			this.QueueDownloads.Add(this.dataBank2ndLang);
			this.QueueDownloads.Add(this.dataBoxes2ndLang);
		}
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001B8F4 File Offset: 0x00019AF4
	private void OldInitGlobalDatas()
	{
		bool flag = true;
		string text = ".php";
		string text2 = ".json";
		string text3 = (!flag) ? text : text2;
		string str = this.LangPrefix.ToLower();
		this.dataglobals = new GlobalData<Dictionary<string, object>>(WWWUtil.infoWWW("globals" + text3), string.Empty);
		this.dataWeapons = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("weapons_" + str + text3), "weapons");
		this.dataMaps = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("maps" + text3), "maps");
		this.dataAchievements = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("achievements_" + str + text3), "achievements");
		this.dataSkills = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("skills_" + str + text3), "skills");
		this.dataTasks = new GlobalData<Dictionary<string, object>>(WWWUtil.infoWWW("tasks_" + str + text3), "tasks");
		this.dataBoxes = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("boxes" + str + text3), "packages");
		this.dataClanskills = new GlobalData<Dictionary<string, object>[]>(WWWUtil.infoWWW("clanskills_" + str + text3), "clan_skills");
		this.dataHints = new GlobalData<string[]>(WWWUtil.infoWWW("hints_" + str + text3), "hints");
		this.dataBank = new GlobalData<Dictionary<string, object>>(WWWUtil.infoWWW("bankPackages" + text3), string.Empty);
		this.dataRoulette = new GlobalData<Dictionary<string, object>>(WWWUtil.infoWWW("roulette" + text3), string.Empty);
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001BA98 File Offset: 0x00019C98
	private void NewInitGlobalDatas()
	{
		this.dataglobals = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("Globals"), string.Empty);
		this.dataMaps = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Maps"), "maps");
		string str = (!CVars.IsStandaloneRealm) ? this.LangPrefix : "Ru";
		this.dataWeapons = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Weapons" + str), "weapons");
		this.dataAchievements = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Achievements" + str), "achievements");
		this.dataSkills = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Skills" + str), "skills");
		this.dataTasks = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("Task" + str), "tasks");
		this.dataClanskills = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("ClanSkills" + str), "clan_skills");
		this.dataHints = new GlobalData<string[]>(this.GlobalDataUri("Hints" + str), "hints");
		this.dataBank = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("BankPackages&lang=" + str), string.Empty);
		this.dataBoxes = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Boxes" + str), "packages");
		this.dataRoulette = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("Roulette"), string.Empty);
		if (CVars.IsStandaloneRealm)
		{
			str = "En";
			this.dataWeapons2ndLang = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Weapons" + str), "weapons");
			this.dataAchievements2ndLang = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Achievements" + str), "achievements");
			this.dataSkills2ndLang = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Skills" + str), "skills");
			this.dataTasks2ndLang = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("Task" + str), "tasks");
			this.dataClanskills2ndLang = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("ClanSkills" + str), "clan_skills");
			this.dataHints2ndLang = new GlobalData<string[]>(this.GlobalDataUri("Hints" + str), "hints");
			this.dataBank2ndLang = new GlobalData<Dictionary<string, object>>(this.GlobalDataUri("BankPackages&lang=" + str), string.Empty);
			this.dataBoxes2ndLang = new GlobalData<Dictionary<string, object>[]>(this.GlobalDataUri("Boxes" + str), "packages");
		}
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0001BD34 File Offset: 0x00019F34
	private string GlobalDataUri(string data)
	{
		return string.Concat(new string[]
		{
			CVars.n_protocol,
			WWWUtil.databaseWWW,
			"adm.php?q=setting/get",
			data,
			"&platform=",
			CVars.realm
		});
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0001BD70 File Offset: 0x00019F70
	internal void Login()
	{
		if (Peer.Dedicated)
		{
			CVars.n_unityhttp = true;
		}
		MasterServer.ipAddress = Globals.I.masterserverIP;
		MasterServer.port = CVars.n_masterServerPort;
		Network.natFacilitatorIP = Globals.I.faciliatorIP;
		Main.UserInfo = new UserInfo(true);
		Time.fixedDeltaTime = 1f / (float)CVars.g_tickrate;
		if (Peer.Dedicated)
		{
			Application.targetFrameRate = CVars.s_tfr;
		}
		else
		{
			base.StartCoroutine(SplashGUI.downloadSplash());
			Application.targetFrameRate = -1;
			if (CVars.targetFrameRate > 40)
			{
				Application.targetFrameRate = CVars.targetFrameRate;
			}
		}
		Main.AddDatabaseRequestCallBack<Login>(delegate
		{
			NetEmulation.Enabled = true;
			StandaloneKeepalive.Instance.Init(new StandaloneKeepalive.StartDelegate(SingletoneComponent<Main>.Instance.StartCoroutine));
		}, delegate
		{
		}, new object[0]);
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x0001BE58 File Offset: 0x0001A058
	public void Download()
	{
		base.StartCoroutine(this.LoaderCoroutine());
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x0001BE68 File Offset: 0x0001A068
	private void OnDownloadComplete()
	{
		this.ParseMaps(this.dataMaps.Data);
		this.ParseRoulette(this.dataRoulette.Data);
		this.Convert(this.dataglobals.Data, false);
		if (SingletoneComponent<SplashController>.Instance != null)
		{
			SingletoneComponent<SplashController>.Instance.OnGlobalsDownloaded();
		}
		if (SingletoneComponent<SplashController>.Instance == null)
		{
			SingletoneComponent<Globals>.Instance.Login();
		}
		if (CVars.c_clientVersion != Main.clientVersion)
		{
			Main.clientMismatch = true;
		}
		if (!this.CheckLauncherVersion())
		{
			base.StartCoroutine(this.DownloadLauncher());
		}
	}

	// Token: 0x06000418 RID: 1048 RVA: 0x0001BF0C File Offset: 0x0001A10C
	private bool CheckLauncherVersion()
	{
		object obj;
		if (!this.dataglobals.Data.TryGetValue("LauncherVersion", out obj))
		{
			return true;
		}
		string text = null;
		string path = Directory.GetCurrentDirectory() + "\\LauncherVersion";
		if (File.Exists(path))
		{
			text = File.ReadAllText(path);
		}
		if (string.IsNullOrEmpty(text))
		{
			return false;
		}
		int num = int.Parse(text, CultureInfo.InvariantCulture);
		return num >= (int)obj;
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x0001BF80 File Offset: 0x0001A180
	private IEnumerator DownloadLauncher()
	{
		WWW launcherWWW = new WWW(WWWUtil.contentWWW + "Launcher.zip");
		yield return launcherWWW;
		string launcherZipFileFullPath = Directory.GetCurrentDirectory() + "\\Launcher.zip";
		try
		{
			using (FileStream fs = new FileStream(launcherZipFileFullPath, FileMode.Create, FileAccess.Write))
			{
				fs.Write(launcherWWW.bytes, 0, launcherWWW.bytes.Length);
			}
			using (ZipFile zipRead = ZipFile.Read(launcherZipFileFullPath))
			{
				foreach (ZipEntry entry in zipRead)
				{
					entry.Extract(Directory.GetCurrentDirectory(), ExtractExistingFileAction.OverwriteSilently);
				}
			}
		}
		catch (Exception ex2)
		{
			Exception ex = ex2;
			Debug.LogException(ex);
		}
		File.Delete(launcherZipFileFullPath);
		yield break;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0001BF94 File Offset: 0x0001A194
	private static IEnumerator RestartServer(float time)
	{
		if (Peer.Dedicated)
		{
			Debug.Log(string.Concat(new object[]
			{
				"Restart Server at ",
				time,
				" seconds\n ",
				SingletoneComponent<Main>.Instance.commandLineArgs.CmdLine
			}));
			yield return new WaitForSeconds(time);
			global::Console.print("Error Download Profile, please refresh web page...");
		}
		yield return 0;
		yield break;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
	private string GetIpByHostName(string IP)
	{
		string result = string.Empty;
		try
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(IP);
			result = hostAddresses[0].ToString();
		}
		catch (Exception ex)
		{
			if (Peer.Dedicated)
			{
				Debug.Log("Restart server");
			}
			base.StopAllCoroutines();
			base.StartCoroutine(Globals.RestartServer(600f));
		}
		return result;
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0001C030 File Offset: 0x0001A230
	private IEnumerator LoaderCoroutine()
	{
		yield return new WaitForSeconds(1f);
		if (Input.GetKey(KeyCode.F9))
		{
			SocketHttp.Request.DebugLog = true;
		}
		bool done = false;
		bool error = false;
		foreach (IDataDownloader dataDownloader in this.QueueDownloads)
		{
			dataDownloader.Download();
		}
		yield return 0;
		int count = 0;
		while (!done)
		{
			done = true;
			int tmp = 0;
			foreach (IDataDownloader dataDownloader2 in this.QueueDownloads)
			{
				yield return new WaitForSeconds(0.1f);
				done &= dataDownloader2.IsDone;
				if (dataDownloader2.IsDone)
				{
					tmp++;
				}
				if (dataDownloader2.IsError)
				{
					error = true;
				}
			}
			if (tmp != count)
			{
				count = tmp;
			}
			yield return new WaitForSeconds(0.5f);
		}
		yield return 0;
		if (error)
		{
			Main.reloads = 5;
			base.StartCoroutine(Globals.RestartServer(600f));
		}
		else
		{
			this.OnDownloadComplete();
		}
		yield break;
	}

	// Token: 0x1700008B RID: 139
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001C04C File Offset: 0x0001A24C
	public Dictionary<string, object>[] weapons
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataWeapons.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataWeapons2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataWeapons2ndLang.Data;
			}
			return this.dataWeapons.Data;
		}
	}

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001C0AC File Offset: 0x0001A2AC
	public Dictionary<string, object>[] achievements
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataAchievements.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataAchievements2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataAchievements2ndLang.Data;
			}
			return this.dataAchievements.Data;
		}
	}

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001C10C File Offset: 0x0001A30C
	public Dictionary<string, object>[] skills
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataSkills.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataSkills2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataSkills2ndLang.Data;
			}
			return this.dataSkills.Data;
		}
	}

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001C16C File Offset: 0x0001A36C
	public Dictionary<string, object>[] ClanSkills
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataClanskills.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataClanskills2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataClanskills2ndLang.Data;
			}
			return this.dataClanskills.Data;
		}
	}

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001C1CC File Offset: 0x0001A3CC
	public string[] hints
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataHints.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataHints2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataHints2ndLang.Data;
			}
			return this.dataHints.Data;
		}
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001C22C File Offset: 0x0001A42C
	public Dictionary<string, object> contracts
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataTasks.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataTasks2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataTasks2ndLang.Data;
			}
			return this.dataTasks.Data;
		}
	}

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001C28C File Offset: 0x0001A48C
	public Dictionary<string, object>[] packages
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.dataBoxes.Data;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.dataBoxes2ndLang.Data;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.dataBoxes2ndLang.Data;
			}
			return this.dataBoxes.Data;
		}
	}

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001C2EC File Offset: 0x0001A4EC
	public Dictionary<string, object> CurrentBank
	{
		get
		{
			if (!CVars.IsStandaloneRealm)
			{
				return this.Bank;
			}
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage == ELanguage.EN)
			{
				return this.Bank2ndLang;
			}
			if (currentLanguage != ELanguage.RU)
			{
				return this.Bank2ndLang;
			}
			return this.Bank;
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001C338 File Offset: 0x0001A538
	public Dictionary<string, object> Bank
	{
		get
		{
			return this.dataBank.Data;
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001C348 File Offset: 0x0001A548
	public Dictionary<string, object> Bank2ndLang
	{
		get
		{
			return this.dataBank2ndLang.Data;
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001C358 File Offset: 0x0001A558
	private string LangPrefix
	{
		get
		{
			ELanguage currentLanguage = Language.CurrentLanguage;
			if (currentLanguage != ELanguage.RU)
			{
				return "En";
			}
			return "Ru";
		}
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001C384 File Offset: 0x0001A584
	private void ParseMaps(Dictionary<string, object>[] dict)
	{
		this.maps = new Map[dict.Length];
		this.mapNames = new string[dict.Length];
		for (int i = 0; i < dict.Length; i++)
		{
			this.maps[i] = new Map();
			this.maps[i].Read(dict[i]);
			this.mapNames[i] = this.maps[i].Name;
		}
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
	public void ParseRoulette(Dictionary<string, object> dict)
	{
		this.RouletteInfo = new RouletteInfo();
		this.RouletteInfo.Convert(dict, false);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001C410 File Offset: 0x0001A610
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		isWrite = false;
		JSON.ReadWrite(dict, "contentIP", ref this.contentIP, isWrite);
		JSON.ReadWrite(dict, "masterserverIP", ref this.masterserverIP, isWrite);
		JSON.ReadWrite(dict, "faciliatorIP", ref this.faciliatorIP, isWrite);
		JSON.ReadWrite(dict, "exceptionWWW", ref this.exceptionWWW, isWrite);
		JSON.ReadWrite(dict, "contentRoot", ref this.contentRoot, isWrite);
		JSON.ReadWrite(dict, "mailruAuth", ref this.mailruAuth, isWrite);
		JSON.ReadWrite(dict, "version", ref this.version, isWrite);
		JSON.ReadWrite(dict, "content_version", ref this.content_version, isWrite);
		JSON.ReadWrite(dict, "tracks", ref this.tracks, isWrite);
		JSON.ReadWrite(dict, "GameName", ref this.GameName, isWrite);
		JSON.ReadWrite(dict, "skillResetPrice", ref this.skillResetPrice, isWrite);
		JSON.ReadWrite(dict, "skillResetPriceCR", ref this.skillResetPriceCR, isWrite);
		JSON.ReadWrite(dict, "undestructibleModCoef", ref this.undestructibleModCoef, isWrite);
		JSON.ReadWrite(dict, "buySPPrice", ref this.buySPPrice, isWrite);
		JSON.ReadWrite(dict, "buyNicknameChangePrice", ref this.buyNicknameChangePrice, isWrite);
		JSON.ReadWrite(dict, "buyNickColorChangePrice", ref this.buyNickColorChangePrice, isWrite);
		JSON.ReadWrite(dict, "clanBasePrice", ref this.clanBasePrice, isWrite);
		JSON.ReadWrite(dict, "clanExtendedPrice", ref this.clanExtendedPrice, isWrite);
		JSON.ReadWrite(dict, "clanPremiumPrice", ref this.clanPremiumPrice, isWrite);
		JSON.ReadWrite(dict, "clanCRPrice", ref this.clanCRPrice, isWrite);
		JSON.ReadWrite(dict, "clanExtendPrice", ref this.clanExtendPrice, isWrite);
		JSON.ReadWrite(dict, "clanAssignmentCosts", ref this.ClanAssignmentCost, isWrite);
		JSON.ReadWrite(dict, "clanChangeUrlCost", ref this.ClanChangeUrlCost, isWrite);
		JSON.ReadWrite(dict, "clanChangeTagColorCost", ref this.ClanChangeTagColorCost, isWrite);
		JSON.ReadWrite(dict, "expTable", ref this.expTable, isWrite);
		JSON.ReadWrite(dict, "streakExp", ref this.streakExp, isWrite);
		JSON.ReadWrite(dict, "masteringTable", ref this.masteringTable, isWrite);
		JSON.ReadWrite(dict, "clanExpTable", ref this.clanExpTable, isWrite);
		JSON.ReadWrite(dict, "weaponSetPrices", ref this.weaponSetPrices, isWrite);
		JSON.ReadWrite(dict, "unlockSetPrices", ref this.unlockSetPrices, isWrite);
		JSON.ReadWrite(dict, "ongoing_race", ref this.ongoingRace, isWrite);
		JSON.ReadWrite(dict, "race_ends", ref this.raceEnds, isWrite);
		JSON.ReadWrite(dict, "showPremiumBox", ref this.showPremiumBox, isWrite);
		JSON.ReadWrite(dict, "badPing", ref this.BadPing, isWrite);
		JSON.ReadWrite(dict, "teamPlayersOdds", ref this.TeamPlayersOdds, isWrite);
		JSON.ReadWrite(dict, "teamWeightOdds", ref this.TeamWeightOdds, isWrite);
		JSON.ReadWrite(dict, "newTeamBalancing", ref this.NewTeamBalancing, isWrite);
		JSON.ReadWrite(dict, "balanceTeamAtStart", ref this.BalanceTeamAtStart, isWrite);
		JSON.ReadWrite(dict, "beaconExp", ref this.BeaconExp, isWrite);
		JSON.ReadWrite(dict, "tutorialDisabled", ref this.TutorialDisabled, isWrite);
		JSON.ReadWrite(dict, "adEnabled", ref this.AdEnabled, isWrite);
		JSON.ReadWrite(dict, "legendaryKill", ref this.LegendaryKill, isWrite);
		JSON.ReadWrite(dict, "expForMp", ref this.MasteringExpToNextPoint, isWrite);
		JSON.ReadWrite(dict, "gpForMp", ref this.MpCost, isWrite);
		JSON.ReadWrite(dict, "DisableHardcoreFriendlyFire", ref this.DisableHardcoreFriendlyFire, isWrite);
		JSON.ReadWrite(dict, "gp_discount", ref this.GpDiscount, isWrite);
		JSON.ReadWrite(dict, "gp_discount_time", ref this.GpDiscountTime, isWrite);
		if (this.GpDiscountTime <= 0)
		{
			this.GpDiscountTime = 48;
		}
		JSON.ReadWrite(dict, "ongoing_season", ref this.OngoingSeason, isWrite);
		int reloadScheme = (int)this.ReloadScheme;
		JSON.ReadWrite(dict, "ReloadScheme", ref reloadScheme, isWrite);
		this.ReloadScheme = (ReloadScheme)reloadScheme;
		CVars.Convert(dict, isWrite);
		if (CVars.debugMode)
		{
			CVars.n_httpDebug = true;
		}
	}

	// Token: 0x040003BE RID: 958
	private const string VersionFileName = "LauncherVersion";

	// Token: 0x040003BF RID: 959
	private const string LauncherZipFileName = "Launcher.zip";

	// Token: 0x040003C0 RID: 960
	private string _dpip;

	// Token: 0x040003C1 RID: 961
	private bool _lockDBIP;

	// Token: 0x040003C2 RID: 962
	public bool TESTERROR;

	// Token: 0x040003C3 RID: 963
	public string[] contentIP;

	// Token: 0x040003C4 RID: 964
	public string faciliatorIP = string.Empty;

	// Token: 0x040003C5 RID: 965
	public string masterserverIP = string.Empty;

	// Token: 0x040003C6 RID: 966
	public string exceptionWWW = "95.161.1.14/cw-omega/";

	// Token: 0x040003C7 RID: 967
	public string databaseRoot = string.Empty;

	// Token: 0x040003C8 RID: 968
	public string contentRoot = string.Empty;

	// Token: 0x040003C9 RID: 969
	public bool mailruAuth;

	// Token: 0x040003CA RID: 970
	public string GameName = string.Empty;

	// Token: 0x040003CB RID: 971
	public int skillResetPrice = 15;

	// Token: 0x040003CC RID: 972
	public int skillResetPriceCR = 8000;

	// Token: 0x040003CD RID: 973
	public float undestructibleModCoef = 0.01f;

	// Token: 0x040003CE RID: 974
	public int buySPPrice = 100;

	// Token: 0x040003CF RID: 975
	public int MpCost = 10;

	// Token: 0x040003D0 RID: 976
	public int buyNicknameChangePrice = 100;

	// Token: 0x040003D1 RID: 977
	public int buyNickColorChangePrice = 1000;

	// Token: 0x040003D2 RID: 978
	public int clanBasePrice = 750;

	// Token: 0x040003D3 RID: 979
	public int clanExtendedPrice = 1250;

	// Token: 0x040003D4 RID: 980
	public int clanPremiumPrice = 2500;

	// Token: 0x040003D5 RID: 981
	public int clanCRPrice;

	// Token: 0x040003D6 RID: 982
	public int clanExtendPrice = 500;

	// Token: 0x040003D7 RID: 983
	public int ongoingRace;

	// Token: 0x040003D8 RID: 984
	public string raceEnds = string.Empty;

	// Token: 0x040003D9 RID: 985
	public int ClanAssignmentCost;

	// Token: 0x040003DA RID: 986
	public int ClanChangeUrlCost;

	// Token: 0x040003DB RID: 987
	public int ClanChangeTagColorCost;

	// Token: 0x040003DC RID: 988
	public ReloadScheme ReloadScheme;

	// Token: 0x040003DD RID: 989
	public string version;

	// Token: 0x040003DE RID: 990
	public int content_version;

	// Token: 0x040003DF RID: 991
	public int tracks;

	// Token: 0x040003E0 RID: 992
	public int[] weaponSetPrices;

	// Token: 0x040003E1 RID: 993
	public int[] unlockSetPrices;

	// Token: 0x040003E2 RID: 994
	public int[] expTable;

	// Token: 0x040003E3 RID: 995
	public int[] streakExp;

	// Token: 0x040003E4 RID: 996
	public int[] masteringTable = new int[0];

	// Token: 0x040003E5 RID: 997
	public int[] clanExpTable = new int[0];

	// Token: 0x040003E6 RID: 998
	public RouletteInfo RouletteInfo;

	// Token: 0x040003E7 RID: 999
	public List<PromoBonus> bonuses = new List<PromoBonus>();

	// Token: 0x040003E8 RID: 1000
	public string[] mapNames;

	// Token: 0x040003E9 RID: 1001
	public Map[] maps;

	// Token: 0x040003EA RID: 1002
	public string roulette = string.Empty;

	// Token: 0x040003EB RID: 1003
	public int ContractsUpdateCost = 50;

	// Token: 0x040003EC RID: 1004
	public bool showPremiumBox;

	// Token: 0x040003ED RID: 1005
	public int BadPing = 300;

	// Token: 0x040003EE RID: 1006
	public int TeamPlayersOdds = 4;

	// Token: 0x040003EF RID: 1007
	public int TeamWeightOdds = 20;

	// Token: 0x040003F0 RID: 1008
	public int NewTeamBalancing;

	// Token: 0x040003F1 RID: 1009
	public bool BalanceTeamAtStart;

	// Token: 0x040003F2 RID: 1010
	public float BeaconExp = 30f;

	// Token: 0x040003F3 RID: 1011
	public bool TutorialDisabled;

	// Token: 0x040003F4 RID: 1012
	public bool AdEnabled;

	// Token: 0x040003F5 RID: 1013
	public bool LegendaryKill;

	// Token: 0x040003F6 RID: 1014
	public int MasteringExpToNextPoint = 10000;

	// Token: 0x040003F7 RID: 1015
	public bool DisableHardcoreFriendlyFire;

	// Token: 0x040003F8 RID: 1016
	public int GpDiscount;

	// Token: 0x040003F9 RID: 1017
	public int GpDiscountTime;

	// Token: 0x040003FA RID: 1018
	public int OngoingSeason;

	// Token: 0x040003FB RID: 1019
	private GlobalData<Dictionary<string, object>[]> dataWeapons;

	// Token: 0x040003FC RID: 1020
	private GlobalData<Dictionary<string, object>[]> dataAchievements;

	// Token: 0x040003FD RID: 1021
	private GlobalData<Dictionary<string, object>[]> dataSkills;

	// Token: 0x040003FE RID: 1022
	private GlobalData<Dictionary<string, object>[]> dataClanskills;

	// Token: 0x040003FF RID: 1023
	private GlobalData<string[]> dataHints;

	// Token: 0x04000400 RID: 1024
	private GlobalData<Dictionary<string, object>> dataTasks;

	// Token: 0x04000401 RID: 1025
	private GlobalData<Dictionary<string, object>> dataBank;

	// Token: 0x04000402 RID: 1026
	private GlobalData<Dictionary<string, object>[]> dataBoxes;

	// Token: 0x04000403 RID: 1027
	private GlobalData<Dictionary<string, object>[]> dataWeapons2ndLang;

	// Token: 0x04000404 RID: 1028
	private GlobalData<Dictionary<string, object>[]> dataAchievements2ndLang;

	// Token: 0x04000405 RID: 1029
	private GlobalData<Dictionary<string, object>[]> dataSkills2ndLang;

	// Token: 0x04000406 RID: 1030
	private GlobalData<Dictionary<string, object>[]> dataClanskills2ndLang;

	// Token: 0x04000407 RID: 1031
	private GlobalData<string[]> dataHints2ndLang;

	// Token: 0x04000408 RID: 1032
	private GlobalData<Dictionary<string, object>> dataTasks2ndLang;

	// Token: 0x04000409 RID: 1033
	private GlobalData<Dictionary<string, object>> dataBank2ndLang;

	// Token: 0x0400040A RID: 1034
	private GlobalData<Dictionary<string, object>[]> dataBoxes2ndLang;

	// Token: 0x0400040B RID: 1035
	private GlobalData<Dictionary<string, object>[]> dataMaps;

	// Token: 0x0400040C RID: 1036
	private GlobalData<Dictionary<string, object>> dataglobals;

	// Token: 0x0400040D RID: 1037
	private GlobalData<Dictionary<string, object>> dataRoulette;

	// Token: 0x0400040E RID: 1038
	private List<IDataDownloader> QueueDownloads = new List<IDataDownloader>();

	// Token: 0x0400040F RID: 1039
	public static bool GlobalDebug;
}
