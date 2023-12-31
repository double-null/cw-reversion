using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;
using VBuffer;

// Token: 0x02000222 RID: 546
[AddComponentMenu("Scripts/Game/Events/LoadProfile")]
internal class LoadProfile : DatabaseEvent
{
	// Token: 0x0600111C RID: 4380 RVA: 0x000BEAB8 File Offset: 0x000BCCB8
	public override void Initialize(params object[] args)
	{
		global::Console.print("Load", Color.grey);
		if (!LoadProfile.splashScreenOn)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.CWMainLoad, Language.CWMainLoadDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		}
		byte[] bytes = Encoding.UTF8.GetBytes(CVars.Version);
		string data = Convert.ToBase64String(bytes);
		HtmlLayer.SendCompressed("?action=load", data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
	}

	// Token: 0x0600111D RID: 4381 RVA: 0x000BEB44 File Offset: 0x000BCD44
	protected override void OnResponse(string text, string url)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.MainBlocker, Language.CWMainLoading, Language.CWMainGlobalInfoLoadingFinished, PopupState.progress, false, false, string.Empty, string.Empty));
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error\n" + text));
			return;
		}
		try
		{
			if ((int)dictionary["result"] == 1)
			{
				this.OnFail(new Exception("Data Server Error " + dictionary["error"]));
				return;
			}
			if ((int)dictionary["result"] == 2)
			{
				EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.CWMainLoad, Language.CWMainLoadFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
				global::Console.print("LoadProfile Finished", Color.green);
				EventFactory.Call("ShowPopup", new Popup(WindowsID.BANNED, string.Empty, string.Empty, PopupState.banned, false, false, string.Empty, string.Empty));
				if (LoadProfile.splashScreenOn)
				{
					EventFactory.Call("ShowMainGUI", null);
				}
				LoadProfile.profileLoaded = true;
				Forms.OnInitialized();
				return;
			}
		}
		catch (Exception innerException)
		{
			this.OnFail(new Exception("Bad Json: NONE result", innerException));
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.CWMainLoad, Language.CWMainLoadFinishedDesc, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("LoadProfile Finished", Color.green);
		if (dictionary.ContainsKey("showXPBonus") && (bool)dictionary["showXPBonus"])
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.XPBonus, Language.DailyBonus, string.Empty, PopupState.XPBonus, false, true, string.Empty, string.Empty));
		}
		if (dictionary.ContainsKey("showPersonalDiscount") && (bool)dictionary["showPersonalDiscount"])
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.PersonalDiscount, Language.DailyBonus, string.Empty, PopupState.PersonalDiscount, false, false, string.Empty, string.Empty));
		}
		if (dictionary.ContainsKey("show_invite") && (bool)dictionary["show_invite"] && LoadProfile.newLevel != 0)
		{
			if (CVars.realm == "fb" || CVars.realm == "kg" || CVars.realm == "ag" || CVars.realm == "mc")
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.BuyGp, Language.GetGoldPoints, string.Empty, PopupState.buyGp, false, true, string.Empty, string.Empty));
			}
			else
			{
				global::Console.print("Invite friends, please!");
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.GatherTheTeam, string.Empty, PopupState.invite, false, true, string.Empty, string.Empty));
			}
		}
		if (dictionary.ContainsKey("newUser"))
		{
			LoadProfile.firstRun = (bool)dictionary["newUser"];
		}
		if (dictionary.ContainsKey("xsolla_token"))
		{
			LoadProfile.XsollaToken = (string)dictionary["xsolla_token"];
		}
		if (dictionary.ContainsKey("hops_secret_key"))
		{
			this._hopsSecretKey = (string)dictionary["hops_secret_key"];
		}
		if (dictionary.ContainsKey("hops_roulette_prize_key"))
		{
			this._hopsRoulettePrizeKey = (string)dictionary["hops_roulette_prize_key"];
		}
		object obj;
		if (dictionary.TryGetValue("seasonAward", out obj))
		{
			Popup values = new Popup(WindowsID.ProgressBonus, Language.SeasonAward, string.Empty, PopupState.SeasonAward, false, false, string.Empty, string.Empty)
			{
				args = new object[]
				{
					obj,
					0
				}
			};
			EventFactory.Call("ShowPopup", values);
		}
		if (dictionary.ContainsKey("newLevel"))
		{
			LoadProfile.newLevel = (int)dictionary["newLevel"];
		}
		if ((LoadProfile.newLevel >= 0 && LoadProfile.newLevel <= 10) || LoadProfile.newLevel == 20 || (LoadProfile.newLevel >= 60 && LoadProfile.newLevel <= 70))
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.ProgressBonus, Language.CrrierBonus, string.Empty, PopupState.progressBonus, false, false, string.Empty, string.Empty));
		}
		if (LoadProfile.newLevel != -1 && LoadProfile.newLevel != 0 && !CVars.IsStandaloneRealm)
		{
			Application.ExternalCall("WallPost", new object[]
			{
				string.Concat(new object[]
				{
					Language.GetLevel0,
					" ",
					LoadProfile.newLevel,
					" ",
					Language.GetLevel1
				}),
				"http://contractwarsgame.com",
				"photo-21974398_199006015"
			});
		}
		object obj2;
		if (dictionary.TryGetValue("data", out obj2))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj2;
			object obj3;
			if (dictionary2.TryGetValue("ladder_prize", out obj3) && obj3 != null)
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)obj3;
				object obj4;
				if (dictionary3.TryGetValue("place", out obj4) && obj4 != null && (int)obj4 != 0)
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.LeaguePopupCongrats, string.Empty, delegate()
					{
					}, PopupState.leagueWinner, false, false, new object[]
					{
						dictionary3
					}));
				}
			}
		}
		Main.UserInfo.Read(dictionary, true);
		Main.UserInfo.HopsSecretKey = this._hopsSecretKey;
		Main.UserInfo.HopsRoulettePrizeKey = this._hopsRoulettePrizeKey;
		if (Main.UserInfo.settings.resolution.width == 0)
		{
			Main.UserInfo.settings.resolution.width = 800;
			Main.UserInfo.settings.resolution.height = 600;
		}
		if (Main.UserInfo.banned != 0)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.BANNED, string.Empty, string.Empty, PopupState.banned, false, false, string.Empty, string.Empty));
		}
		if (!Peer.Dedicated)
		{
			if (!LoadProfile.profileLoaded)
			{
				Audio.Init();
				CameraListener.ChangeTo(Main.GUIObject);
			}
			base.Invoke("UpdateUserSettings", 1f);
		}
		else if (Application.isEditor)
		{
			SingletoneComponent<Main>.Instance.gameObject.AddComponent<AudioListener>();
		}
		if (Peer.Dedicated)
		{
			LoadProfile.ServerInit();
		}
		else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--connect"))
		{
			string ip = SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--ip");
			int port = 27015;
			if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--port"))
			{
				port = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--port"));
			}
			Peer.JoinGame(ip, port, false);
		}
		if (LoadProfile.splashScreenOn)
		{
			EventFactory.Call("ShowMainGUI", null);
		}
		if (LoadProfile.newLevel == 0 && !Globals.I.TutorialDisabled)
		{
			MainGUI.Instance.tutor.InitHintList();
			EventFactory.Call("MainGUIShowTutor", null);
		}
		if (!LoadProfile.profileLoaded)
		{
			VBuffer vbuffer = new VBuffer();
			vbuffer.Init(Main.UserInfo.userID.ToString());
			if (Screen.fullScreen)
			{
				Utility.SetResolution(Main.UserInfo.settings.resolution);
			}
		}
		LoadProfile.profileLoaded = true;
		Forms.OnInitialized();
		if (CVars.realm == "kg" && !Peer.Dedicated)
		{
			this.SendKGStats();
		}
		if (!Peer.Dedicated)
		{
			if (!this._masteringMainLoad)
			{
				Main.AddDatabaseRequestCallBack<MasteringMainLoad>(delegate
				{
					this._masteringMainLoad = true;
					ModIconsDownloader.Instance.LoadInstalledModsIcons();
					Dictionary<int, WeaponMods> currentWeaponsMods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods;
					foreach (KeyValuePair<int, WeaponMods> keyValuePair in currentWeaponsMods)
					{
						Main.UserInfo.weaponsStates[keyValuePair.Key].CurrentWeapon.LoadTable(Globals.I.weapons[keyValuePair.Key]);
						Main.UserInfo.weaponsStates[keyValuePair.Key].CurrentWeapon.ApplyModsEffect(keyValuePair.Value.Mods);
					}
				}, delegate
				{
				}, new object[0]);
			}
			else
			{
				Main.AddDatabaseRequest<LoadMasteringInfo>(new object[0]);
			}
			Main.AddDatabaseRequest<GetAttempts>(new object[0]);
		}
		if (Globals.I.AdEnabled)
		{
			AdDownloader.I.Load();
		}
		if (SplashController.IsFirstStart)
		{
			Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, true);
		}
	}

	// Token: 0x0600111E RID: 4382 RVA: 0x000BF448 File Offset: 0x000BD648
	public static void ServerInit()
	{
		Main.AuthData = new AuthData();
		if (SingletoneComponent<Main>.Instance.GetComponent<AudioListener>() == null)
		{
			SingletoneComponent<Main>.Instance.gameObject.AddComponent<AudioListener>();
		}
		AudioListener.pause = true;
		Peer.Disconnect(false);
		Peer.Dedicated = true;
		Peer.Info = new HostInfo();
		string text = SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--map");
		for (int i = 0; i < Globals.I.maps.Length; i++)
		{
			if (text.Equals(Globals.I.maps[i].Name, StringComparison.OrdinalIgnoreCase))
			{
				Main.HostInfo.MapName = Globals.I.maps[i].Name;
				Main.HostInfo.MapIndex = i;
				Main.HostInfo.GameMode = Globals.I.maps[i].Modes[0];
			}
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--pass"))
		{
			Main.HostInfo.PasswordProtected = true;
			eNetwork.password = SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--pass");
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--maxPlayers"))
		{
			Main.HostInfo.MaxPlayers = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--maxPlayers"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--gameName"))
		{
			Main.HostInfo.Name = SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--gameName");
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--port"))
		{
			Main.HostInfo.Port = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--port"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--NAT"))
		{
			Main.HostInfo.ForceNAT = Convert.ToBoolean(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--NAT"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mode"))
		{
			Main.HostInfo.GameMode = (GameMode)((int)Enum.Parse(typeof(GameMode), SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--mode")));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--hidden"))
		{
			Main.HostInfo.IsHidden = Convert.ToBoolean(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--hidden"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--minLevel"))
		{
			Main.HostInfo.MinLevel = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--minLevel"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--maxLevel"))
		{
			Main.HostInfo.MaxLevel = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--maxLevel"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--hc"))
		{
			Main.HostInfo.Hardcore = Convert.ToBoolean(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--hc"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--skip"))
		{
			Main.HostInfo.SkipInQuickGame = Convert.ToBoolean(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--skip"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--testVip"))
		{
			Main.HostInfo.TestVip = Convert.ToBoolean(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--testVip"));
		}
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ip"))
		{
			Main.HostInfo.ExternalIp = SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--ip");
		}
		Peer.IsHost = true;
		Peer.CreateServer();
	}

	// Token: 0x0600111F RID: 4383 RVA: 0x000BF854 File Offset: 0x000BDA54
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.CWMainLoadError, Language.CWMainLoadErrorDesc, PopupState.information, true, true, string.Empty, string.Empty));
		base.OnFail(e);
	}

	// Token: 0x06001120 RID: 4384 RVA: 0x000BF890 File Offset: 0x000BDA90
	[Obfuscation(Exclude = true)]
	private void UpdateUserSettings()
	{
		Main.UserInfo.settings.graphics.OnProfileChanged(false);
	}

	// Token: 0x06001121 RID: 4385 RVA: 0x000BF8A8 File Offset: 0x000BDAA8
	private void SendKGStats()
	{
		string text = string.Empty;
		text = "{";
		if (Main.UserInfo.currentXP > 0f)
		{
			text += this.StatsString("initialized", 1, false);
		}
		text += this.StatsString("onlineTime", Main.UserInfo.userStats.timeOnline, false);
		text += this.StatsString("wins", Main.UserInfo.winCount, false);
		text += this.StatsString("losses", Main.UserInfo.lossCount, false);
		text += this.StatsString("damageApplied", Main.UserInfo.userStats.totalDamage, false);
		text += this.StatsString("ammoUsed", Main.UserInfo.userStats.totalAmmo, false);
		text += this.StatsString("headshots", Main.UserInfo.userStats.headShots, false);
		text += this.StatsString("doubleHS", Main.UserInfo.userStats.doubleHeadShots, false);
		text += this.StatsString("longShots", Main.UserInfo.userStats.longShots, false);
		text += this.StatsString("doubleKills", Main.UserInfo.userStats.doubleKills, false);
		text += this.StatsString("tripleKills", Main.UserInfo.userStats.tripleKills, false);
		text += this.StatsString("quadKills", Main.UserInfo.userStats.quadKills, false);
		text += this.StatsString("proKills", Main.UserInfo.userStats.proKills, false);
		if (Globals.I.LegendaryKill)
		{
			text += this.StatsString("legendaryKills", Main.UserInfo.userStats.legendaryKills, false);
		}
		text += this.StatsString("creditsSpent", Main.UserInfo.userStats.creditsSpent, false);
		text += this.StatsString("wtaskUnlocked", Main.UserInfo.wtaskOpenedCount, false);
		text += this.StatsString("achUnlocked", AchievementsEngine.OpenedCount(Main.UserInfo), false);
		text += this.StatsString("supportUsed", Main.UserInfo.userStats.armstreaksUsed, false);
		text += this.StatsString("knifeKills", Main.UserInfo.userStats.knifeKills, false);
		text += this.StatsString("grenadeKills", Main.UserInfo.userStats.grenadeKills, false);
		text += this.StatsString("expCount", Mathf.RoundToInt((float)Main.UserInfo.currentXP), false);
		text += this.StatsString("totalKills", Main.UserInfo.killCount, false);
		text += this.StatsString("totalDeaths", Main.UserInfo.deathCount, false);
		text += this.StatsString("premweapUnlocked", Main.UserInfo.premiumWeapUnlocked, false);
		text += this.StatsString("easyContrCompleted", Main.UserInfo.contractsInfo.CurrentEasyIndex, false);
		text += this.StatsString("mediumContrCompleted", Main.UserInfo.contractsInfo.CurrentNormalIndex, false);
		text += this.StatsString("hardContrCompleted", Main.UserInfo.contractsInfo.CurrentHardIndex, false);
		text += this.StatsString("AllContrCompleted", Main.UserInfo.contractsInfo.CurrentEasyIndex + Main.UserInfo.contractsInfo.CurrentNormalIndex + Main.UserInfo.contractsInfo.CurrentHardIndex, false);
		text += this.StatsString("level10Reached", (Main.UserInfo.currentLevel < 10) ? 0 : 1, false);
		text += this.StatsString("level20Reached", (Main.UserInfo.currentLevel < 20) ? 0 : 1, false);
		text += this.StatsString("level30Reached", (Main.UserInfo.currentLevel < 30) ? 0 : 1, false);
		text += this.StatsString("level40Reached", (Main.UserInfo.currentLevel < 40) ? 0 : 1, false);
		text += this.StatsString("level50Reached", (Main.UserInfo.currentLevel < 50) ? 0 : 1, false);
		text += this.StatsString("level60Reached", (Main.UserInfo.currentLevel < 60) ? 0 : 1, true);
		text += "}";
		Application.ExternalCall("KGPushStats", new object[]
		{
			text
		});
	}

	// Token: 0x06001122 RID: 4386 RVA: 0x000BFDCC File Offset: 0x000BDFCC
	private string StatsString(string name, int value, bool last = false)
	{
		return string.Concat(new string[]
		{
			"\"",
			name,
			"\":",
			value.ToString(),
			(!last) ? "," : string.Empty
		});
	}

	// Token: 0x040010EB RID: 4331
	public static bool firstRun;

	// Token: 0x040010EC RID: 4332
	public static int newLevel = -1;

	// Token: 0x040010ED RID: 4333
	public static string XsollaToken;

	// Token: 0x040010EE RID: 4334
	public static bool profileLoaded;

	// Token: 0x040010EF RID: 4335
	public static bool splashScreenOn = true;

	// Token: 0x040010F0 RID: 4336
	private bool _masteringMainLoad;

	// Token: 0x040010F1 RID: 4337
	private string _hopsSecretKey = string.Empty;

	// Token: 0x040010F2 RID: 4338
	private string _hopsRoulettePrizeKey = string.Empty;
}
