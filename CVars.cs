using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x02000075 RID: 117
[Obfuscation(Exclude = true, ApplyToMembers = true)]
public class CVars
{
	// Token: 0x17000031 RID: 49
	// (get) Token: 0x0600021E RID: 542 RVA: 0x00012710 File Offset: 0x00010910
	[CVar("1/Tickrate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_one_div_tickrate
	{
		get
		{
			return 1f / (float)CVars.g_tickrate;
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00012720 File Offset: 0x00010920
	[CVarFunction("Start map with map", CVarType.t_unknown, EPermission.Moder)]
	internal static void map(object[] v = null)
	{
		Debug.LogError("map is obsolete!!!");
	}

	// Token: 0x06000220 RID: 544 RVA: 0x0001272C File Offset: 0x0001092C
	[CVarFunction("Stop bot", CVarType.t_unknown, EPermission.Moder)]
	internal static void botstop(object[] values = null)
	{
		CVars.g_botmove = false;
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00012734 File Offset: 0x00010934
	[CVarFunction("Start bot", CVarType.t_unknown, EPermission.Moder)]
	internal static void botstart(object[] values = null)
	{
		CVars.g_botmove = true;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0001273C File Offset: 0x0001093C
	[CVarFunction("Bot Fire", CVarType.t_unknown, EPermission.Moder)]
	internal static void botfire(object[] values = null)
	{
		CVars.botfireTimer.Start();
	}

	// Token: 0x06000223 RID: 547 RVA: 0x00012748 File Offset: 0x00010948
	[CVarFunction("Quality test", CVarType.t_unknown, EPermission.Moder)]
	internal static void qtest(object[] values = null)
	{
		global::Console.print(string.Concat(new object[]
		{
			"TX Q: ",
			Main.UserInfo.settings.graphics.textureQuality.ToString(),
			"(",
			(int)Main.UserInfo.settings.graphics.TextureQ,
			")"
		}));
	}

	// Token: 0x06000224 RID: 548 RVA: 0x000127B8 File Offset: 0x000109B8
	[CVarFunction("Debug Weapon", CVarType.t_unknown, EPermission.Player)]
	internal static void weapinfo(object[] values = null)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (!Main.IsAlive)
		{
			return;
		}
		if (!Main.LocalPlayer.Ammo.weaponEquiped)
		{
			return;
		}
		BaseWeapon currentWeapon = Main.LocalPlayer.Ammo.CurrentWeapon;
		global::Console.print("Primary weapon " + currentWeapon.type.ToString() + " info:");
		global::Console.print("                         Base%     +Mod%   +Skill%   Result%        Result ");
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Accuracy      ",
			currentWeapon.BaseAccuracyProc,
			currentWeapon.ModAccuracyProcBonus,
			currentWeapon.SkillAccuracyProcBonus,
			currentWeapon.AccuracyProcWithoutDurab.ToString() + " - " + ((int)(currentWeapon.AccuracyProcWithoutDurab - currentWeapon.AccuracyProc)).ToString(),
			currentWeapon.accuracy
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Recoil           ",
			currentWeapon.BaseRecoilProc,
			currentWeapon.ModRecoilProcBonus,
			currentWeapon.SkillRecoilProcBonus,
			currentWeapon.RecoilProc,
			currentWeapon.recoil
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Damage       ",
			currentWeapon.BaseDamageProc,
			currentWeapon.ModDamageProcBonus,
			currentWeapon.SkillDamageProcBonus,
			currentWeapon.DamageProc,
			currentWeapon.damage
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Firerate        ",
			currentWeapon.BaseFirerateProc,
			currentWeapon.ModFirerateProcBonus,
			currentWeapon.SkillFirerateProcBonus,
			currentWeapon.FirerateProc,
			currentWeapon.firerate
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Mobility        ",
			currentWeapon.BaseMobilityProc,
			currentWeapon.ModMobilityProcBonus,
			currentWeapon.SkillMobilityProcBonus,
			currentWeapon.MobilityProc,
			currentWeapon.mobility
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"ReloadSpeed ",
			currentWeapon.BaseReloadSpeedProc,
			currentWeapon.ModReloadSpeedProcBonus,
			currentWeapon.SkillReloadSpeedProcBonus,
			currentWeapon.ReloadSpeedProc,
			currentWeapon.reloadSpeed
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}{3,16}{4,16}{5,16:#####.##}", new object[]
		{
			"Pierce           ",
			currentWeapon.BasePierceProc,
			currentWeapon.ModPierceProcBonus,
			currentWeapon.SkillPierceProcBonus,
			currentWeapon.PierceProc,
			currentWeapon.PierceReducer
		}));
		global::Console.print(string.Format("{0}{1,12}{2,12}", "DistanceMin   ", currentWeapon.damageReduceDistanceMin, currentWeapon.damageReduceDistanceMinSkillBonus));
		global::Console.print(string.Format("{0}{1,12}{2,12}", "DistanceMax  ", currentWeapon.damageReduceDistanceMax, currentWeapon.damageReduceDistanceMaxSkillBonus));
	}

	// Token: 0x06000225 RID: 549 RVA: 0x00012B5C File Offset: 0x00010D5C
	[CVarFunction("wallpost test", CVarType.t_unknown, EPermission.Moder)]
	internal static void wptest(object[] values = null)
	{
		Application.ExternalCall("WallPost", new object[]
		{
			"Got level test",
			"http://contractwarsgame.com",
			"http://cs9732.vk.me/u11107/123714478/z_d1c5e159.jpg"
		});
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00012B94 File Offset: 0x00010D94
	private static void ddd(string test, string url)
	{
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00012B98 File Offset: 0x00010D98
	[CVarFunction("memusage", CVarType.t_unknown, EPermission.Player)]
	internal static void memusage(object[] values = null)
	{
	}

	// Token: 0x06000228 RID: 552 RVA: 0x00012B9C File Offset: 0x00010D9C
	private static long CalcMemUsage<T>() where T : UnityEngine.Object
	{
		return 0L;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00012BB0 File Offset: 0x00010DB0
	[CVarFunction("Start tutorial", CVarType.t_unknown, EPermission.Moder)]
	internal static void tutorial(object[] values = null)
	{
		MainGUI mainGUI = Forms.Get(typeof(MainGUI)) as MainGUI;
		mainGUI.currentSuit.secondaryIndex = 0;
		mainGUI.currentSuit.primaryIndex = 4;
		Peer.Info = new HostInfo();
		Main.HostInfo.MapIndex = 9;
		Main.HostInfo.MapName = Globals.I.maps[Main.HostInfo.MapIndex].Name;
		Main.HostInfo.Name = "ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½";
		Peer.Info.GameMode = Globals.I.maps[Main.HostInfo.MapIndex].Modes[0];
		Peer.IsHost = true;
		Peer.IsHidden = true;
		Peer.CreateServer();
	}

	// Token: 0x0600022A RID: 554 RVA: 0x00012C70 File Offset: 0x00010E70
	[CVarFunction("leakstart", CVarType.t_unknown, EPermission.Player)]
	internal static void leakstart(object[] v = null)
	{
		Memory.Snapshot();
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00012C78 File Offset: 0x00010E78
	[CVarFunction("leak", CVarType.t_unknown, EPermission.Player)]
	internal static void leak(object[] v = null)
	{
		Memory.Delta();
		global::Console.print(Memory.delta.Object);
		global::Console.print(Memory.delta.Texture);
		global::Console.print(Memory.delta.Mesh);
		global::Console.print(Memory.delta.Material);
		global::Console.print(Memory.delta.AnimationClip);
		global::Console.print(Memory.delta.AudioClip);
		global::Console.print(Memory.delta.GameObject);
		global::Console.print(Memory.delta.MonoBehaviour);
		global::Console.print(Memory.delta.Shader);
		global::Console.print(Memory.delta.Transform);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x00012D20 File Offset: 0x00010F20
	[CVarFunction("clearleak", CVarType.t_unknown, EPermission.Moder)]
	internal static void clearleak(object[] v = null)
	{
		Memory.delta.ClearLeak();
	}

	// Token: 0x0600022D RID: 557 RVA: 0x00012D2C File Offset: 0x00010F2C
	[CVarFunction("loadbug", CVarType.t_unknown, EPermission.Player)]
	internal static void loadbug(object[] v = null)
	{
		for (int i = 0; i < Peer.games.Count; i++)
		{
			if (Peer.games[i].LoadPlayerCount > 0)
			{
				global::Console.print(string.Concat(new object[]
				{
					Peer.games[i].Name,
					" loadbug (",
					Peer.games[i].LoadPlayerCount,
					")"
				}));
			}
		}
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00012DB8 File Offset: 0x00010FB8
	[CVarFunction("online", CVarType.t_unknown, EPermission.Player)]
	internal static void online(object[] v = null)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int i = 0; i < Peer.games.Count; i++)
		{
			num += Peer.games[i].PlayerCount;
			num3 += Peer.games[i].SpectactorCount;
			num4 += Peer.games[i].LoadPlayerCount;
			num2 += Peer.games[i].MaxPlayers;
		}
		int num5 = num + num3 + num4;
		global::Console.print(string.Concat(new object[]
		{
			"online at ",
			DateTime.Now.ToString("f"),
			" is ",
			num5,
			" (Playing:",
			num,
			"/",
			num2,
			" Spectactors:",
			num3,
			" Loading Profile:",
			num4,
			")"
		}), Color.yellow);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00012EDC File Offset: 0x000110DC
	[CVarFunction("s_debug", CVarType.t_unknown, EPermission.Moder)]
	internal static void s_debug(object[] v = null)
	{
		global::Console.print("Server debug info:");
		for (int i = 0; i < Peer.games.Count; i++)
		{
			if (CVars.val != string.Empty && Peer.games[i].Name.StartsWith(CVars.val))
			{
				global::Console.print(Peer.games[i].Name + " " + Peer.games[i].Debug);
			}
			if (CVars.val == string.Empty)
			{
				global::Console.print(Peer.games[i].Name + " " + Peer.games[i].Debug);
			}
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x00012FB4 File Offset: 0x000111B4
	[CVarFunction("hierarchy", CVarType.t_unknown, EPermission.Player)]
	internal static void hierarchy(object[] v = null)
	{
		global::Console.print(Utility.PrintHierarchy(SingletoneComponent<Main>.Instance.transform, 1));
	}

	// Token: 0x06000231 RID: 561 RVA: 0x00012FCC File Offset: 0x000111CC
	[CVarFunction("userID", CVarType.t_unknown, EPermission.Player)]
	internal static void userID(object[] v = null)
	{
		global::Console.print(Main.UserInfo.userID);
	}

	// Token: 0x06000232 RID: 562 RVA: 0x00012FE4 File Offset: 0x000111E4
	[CVarFunction("socialID", CVarType.t_unknown, EPermission.Player)]
	internal static void socialID(object[] v = null)
	{
		global::Console.print(Main.UserInfo.socialID);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x00012FF8 File Offset: 0x000111F8
	[CVarFunction("Create hidden server", CVarType.t_unknown, EPermission.Moder)]
	internal static void hidden(object[] v = null)
	{
		Peer.Info = new HostInfo();
		Peer.Info.MinLevel = 0;
		Peer.Info.MaxLevel = 70;
		Main.HostInfo.MapIndex = (int)CVars.fastmap;
		Main.HostInfo.MapName = Globals.I.maps[Main.HostInfo.MapIndex].Name;
		Peer.Info.GameMode = Globals.I.maps[Main.HostInfo.MapIndex].Modes[0];
		if (Input.GetKey(KeyCode.LeftControl) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TeamElimination))
		{
			Peer.Info.GameMode = GameMode.TeamElimination;
		}
		if (Input.GetKey(KeyCode.LeftShift) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TargetDesignation))
		{
			Peer.Info.GameMode = GameMode.TargetDesignation;
		}
		if (Input.GetKey(KeyCode.RightControl) && Globals.I.maps[Main.HostInfo.MapIndex].Modes.Contains(GameMode.TacticalConquest))
		{
			Peer.Info.GameMode = GameMode.TacticalConquest;
		}
		Main.HostInfo.Name = "Developer Test Server " + (int)(UnityEngine.Random.value * 13f);
		Peer.IsHidden = true;
		Peer.Info.ForceNAT = true;
		Peer.IsHost = true;
		Peer.CreateServer();
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00013184 File Offset: 0x00011384
	[CVarFunction("Connect function, OBSOLETED!!!", CVarType.t_unknown, EPermission.Player)]
	internal static void connect(object[] v = null)
	{
		string[] array = CVars.line.Split(new char[]
		{
			' ',
			'='
		});
		string[] array2 = new string[array.Length - 1];
		for (int i = 1; i < array.Length; i++)
		{
			array2[i - 1] = array[i];
		}
		if (array.Length > 1)
		{
			CVars.val = array[1];
		}
		if (array2.Length > 2)
		{
			eNetwork.password = array2[2];
		}
		Peer.JoinGame(array2[0], (array2.Length <= 1) ? 27015 : System.Convert.ToInt32(array2[1]), false);
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0001321C File Offset: 0x0001141C
	[CVarFunction("cleancache", CVarType.t_unknown, EPermission.Moder)]
	internal static void cleancache(object[] values = null)
	{
		Caching.CleanCache();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x00013224 File Offset: 0x00011424
	[CVarFunction("dblogload", CVarType.t_unknown, EPermission.Moder)]
	internal static void dblogload(object[] values = null)
	{
		Main.AddDatabaseRequest<DBLog>(new object[]
		{
			"load",
			string.Empty,
			Main.UserInfo.userID
		});
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0001325C File Offset: 0x0001145C
	[CVarFunction("dblogsave", CVarType.t_unknown, EPermission.Moder)]
	internal static void dblogsave(object[] values = null)
	{
		Main.AddDatabaseRequest<DBLog>(new object[]
		{
			"save",
			"savetest",
			Main.UserInfo.userID
		});
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00013294 File Offset: 0x00011494
	[CVarFunction("dblogclear", CVarType.t_unknown, EPermission.Moder)]
	internal static void dblogclear(object[] values = null)
	{
		Main.AddDatabaseRequest<DBLog>(new object[]
		{
			"clear",
			string.Empty,
			Main.UserInfo.userID
		});
	}

	// Token: 0x06000239 RID: 569 RVA: 0x000132CC File Offset: 0x000114CC
	[CVarFunction("Emulate Error", CVarType.t_unknown, EPermission.Moder)]
	internal static void error(object[] values = null)
	{
		Debug.LogError(GameObject.Find("error").rigidbody);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x000132E4 File Offset: 0x000114E4
	[CVarFunction("Kill player", CVarType.t_unknown, EPermission.Moder)]
	internal static void kill(object[] values = null)
	{
		if (Peer.ClientGame && Main.UserInfo.Permission >= EPermission.Admin)
		{
			if (Peer.PeerType == NetworkPeerType.Server)
			{
				Peer.ClientGame.LocalPlayer.AdminCommand(CVars.line);
			}
			if (Peer.PeerType == NetworkPeerType.Client)
			{
				Peer.ClientGame.LocalPlayer.AdminCommand(CVars.line);
			}
		}
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00013350 File Offset: 0x00011550
	[CVarFunction("Add Bot", CVarType.t_unknown, EPermission.Moder)]
	internal static void addbot(object[] values = null)
	{
		if (Main.UserInfo.Permission >= EPermission.Admin)
		{
			int num = 1;
			if (values.Length > 0 && !int.TryParse((string)values[0], out num))
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				eNetwork.RPC("AddBot", new eNetworkPlayer(), new object[]
				{
					Globals.I.version,
					eNetwork.password,
					IDUtil.BotID,
					Network.AllocateViewID(),
					default(NetworkMessageInfo)
				});
			}
		}
	}

	// Token: 0x0600023C RID: 572 RVA: 0x000133F8 File Offset: 0x000115F8
	[CVarFunction("Add Fake", CVarType.t_unknown, EPermission.Moder)]
	internal static void addfake(object[] values = null)
	{
		if (Main.UserInfo.Permission >= EPermission.Admin)
		{
			int num = 1;
			if (values.Length > 0 && !int.TryParse((string)values[0], out num))
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				Peer.ClientGame.BotMainInitialize(NetworkViewID.unassigned, NetworkViewID.unassigned, -1, -1, 0f);
			}
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00013464 File Offset: 0x00011664
	[CVarFunction("Available screen resolutions", CVarType.t_unknown, EPermission.Moder)]
	internal static void resolutions(object[] values = null)
	{
		global::Console.print("Available screen resolutions:");
		for (int i = 0; i < Screen.resolutions.Length; i++)
		{
			global::Console.print(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height);
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x000134CC File Offset: 0x000116CC
	[CVarFunction("crosshair", CVarType.t_unknown, EPermission.Moder)]
	internal static void crosshair(object[] values = null)
	{
		if (Main.IsGameLoaded && Main.UserInfo.Permission >= EPermission.Admin)
		{
			Peer.ClientGame.gameObject.AddComponent<CrossHair>();
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00013504 File Offset: 0x00011704
	[CVarFunction("showcross", CVarType.t_unknown, EPermission.Moder)]
	internal static void showcross(object[] values = null)
	{
		if (Main.IsGameLoaded && Main.UserInfo.Permission >= EPermission.Admin)
		{
			Peer.ClientGame.gameObject.AddComponent<ShowCross>();
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0001353C File Offset: 0x0001173C
	[CVarFunction("http_debug", CVarType.t_unknown, EPermission.Moder)]
	internal static void http_debug(object[] values = null)
	{
		CVars.n_httpDebug = !CVars.n_httpDebug;
		global::Console.print("HTTP_DEBUG: " + CVars.n_httpDebug.ToString(), Color.green);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0001356C File Offset: 0x0001176C
	[CVarFunction("awhlog", CVarType.t_unknown, EPermission.Moder)]
	internal static void awhlog(object[] values = null)
	{
		CVars.WHLOG = !CVars.WHLOG;
		global::Console.print("WHLOG: " + CVars.WHLOG.ToString(), Color.green);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0001359C File Offset: 0x0001179C
	[CVarFunction("bigrigs", CVarType.t_unknown, EPermission.Player)]
	internal static void bigrigs(object[] values = null)
	{
		CVars.f_bigrigs = true;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x000135A4 File Offset: 0x000117A4
	[CVarFunction("nat", CVarType.t_unknown, EPermission.Moder)]
	internal static void nat(object[] values = null)
	{
		global::Console.print("Unity useNat: " + (!Network.HavePublicAddress()).ToString());
	}

	// Token: 0x06000244 RID: 580 RVA: 0x000135D0 File Offset: 0x000117D0
	[CVarFunction("voteinfo", CVarType.t_unknown, EPermission.Moder)]
	internal static void voteinfo(object[] values = null)
	{
		string text = string.Empty;
		for (int i = 0; i < Main.UserInfo.voteInfo.Length; i++)
		{
			text = text + Main.UserInfo.voteInfo[i].ToString() + " ";
		}
		global::Console.print("Voteinfo:(" + Main.UserInfo.voteInfo.Length.ToString() + ") " + text);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0001364C File Offset: 0x0001184C
	[CVarFunction("list", CVarType.t_unknown, EPermission.Player)]
	internal static void list(object[] values = null)
	{
		if (Peer.ClientGame)
		{
			for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
			{
				EntityNetPlayer entityNetPlayer = Peer.ClientGame.AllPlayers[i];
				if (!entityNetPlayer.playerInfo.isModerator || Main.UserInfo.Permission >= EPermission.Admin)
				{
					if (!EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID))
					{
						global::Console.print(string.Concat(new object[]
						{
							"nick: ",
							entityNetPlayer.playerInfo.Nick,
							" playerID: ",
							entityNetPlayer.ID,
							" userID: ",
							entityNetPlayer.playerInfo.userID
						}), Colors.consoleServerCmds);
					}
				}
			}
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x00013734 File Offset: 0x00011934
	[CVarFunction("targetframerate", CVarType.t_unknown, EPermission.Player)]
	internal static void targetframerate(object[] values = null)
	{
		if (CVars.val.Length == 0)
		{
			global::Console.print(Application.targetFrameRate);
		}
		else
		{
			Application.targetFrameRate = System.Convert.ToInt32(CVars.val);
		}
	}

	// Token: 0x06000247 RID: 583 RVA: 0x00013764 File Offset: 0x00011964
	[CVarFunction("fixedDeltaTime", CVarType.t_unknown, EPermission.Player)]
	internal static void fixedDeltaTime(object[] values = null)
	{
		if (CVars.val.Length == 0)
		{
			global::Console.print(Time.fixedDeltaTime);
		}
		else
		{
			Time.fixedDeltaTime = System.Convert.ToSingle(CVars.val);
		}
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00013794 File Offset: 0x00011994
	[CVarFunction("own", CVarType.t_unknown, EPermission.Moder)]
	internal static void own(object[] values = null)
	{
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00013798 File Offset: 0x00011998
	[CVarFunction("clearprofile", CVarType.t_unknown, EPermission.Moder)]
	internal static void clearprofile(object[] values = null)
	{
		Main.AddDatabaseRequest<ClearProfile>(new object[]
		{
			Main.UserInfo.userID
		});
	}

	// Token: 0x0600024A RID: 586 RVA: 0x000137B4 File Offset: 0x000119B4
	[CVarFunction("lang", CVarType.t_unknown, EPermission.Moder)]
	internal static void lang(object[] values = null)
	{
		Language.CurrentLanguage = ELanguage.EN;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x000137BC File Offset: 0x000119BC
	[CVarFunction("DEBUGA", CVarType.t_unknown, EPermission.Moder)]
	internal static void debuga(object[] values = null)
	{
		Peer.ClientGame.LocalPlayer.Controller.gameObject.AddComponent<DebugAnimations>();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x000137E4 File Offset: 0x000119E4
	[CVarFunction("promo", CVarType.t_hidden, EPermission.Player)]
	internal static void promo(object[] values = null)
	{
		Main.AddDatabaseRequest<ActivatePromo>(new object[]
		{
			CVars.val
		});
	}

	// Token: 0x0600024D RID: 589 RVA: 0x000137FC File Offset: 0x000119FC
	[CVarFunction("mailru_balance", CVarType.t_unknown, EPermission.Moder)]
	internal static void mailru_balance(object[] values = null)
	{
		global::Console.print("SN balance change request");
		Main.AddDatabaseRequest<BalanceChangedMailruRequest>(new object[0]);
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00013814 File Offset: 0x00011A14
	[CVarFunction("friendlist", CVarType.t_unknown, EPermission.Moder)]
	internal static void friendlist(object[] values = null)
	{
		string text = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10";
		Debug.Log(values.Length);
		if (values.Length > 0)
		{
			int num = int.Parse((string)values[0]);
			Debug.Log(num);
			text = string.Empty;
			for (int i = 0; i < num; i++)
			{
				text += "i";
				if (i < num - 1)
				{
					text += ",";
				}
			}
		}
		Main.AddDatabaseRequest<PassFriendList>(new object[]
		{
			text
		});
	}

	// Token: 0x0600024F RID: 591 RVA: 0x000138A0 File Offset: 0x00011AA0
	[CVarFunction("Set GP", CVarType.t_unknown, EPermission.Moder)]
	internal static void GP(object[] values = null)
	{
		Main.UserInfo.GP = System.Convert.ToInt32(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000138D4 File Offset: 0x00011AD4
	[CVarFunction("Respect is everything!", CVarType.t_unknown, EPermission.Moder)]
	internal static void repa(object[] values = null)
	{
		Main.UserInfo.repa = System.Convert.ToSingle(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000251 RID: 593 RVA: 0x00013908 File Offset: 0x00011B08
	[CVarFunction("Set CR", CVarType.t_unknown, EPermission.Moder)]
	internal static void CR(object[] values = null)
	{
		Main.UserInfo.CR = System.Convert.ToInt32(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0001393C File Offset: 0x00011B3C
	[CVarFunction("Set SP", CVarType.t_unknown, EPermission.Moder)]
	internal static void SP(object[] values = null)
	{
		Main.UserInfo.SP = System.Convert.ToInt32(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00013970 File Offset: 0x00011B70
	[CVarFunction("Set BG", CVarType.t_unknown, EPermission.Moder)]
	internal static void BG(object[] values = null)
	{
		Main.UserInfo.BG = System.Convert.ToInt32(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000254 RID: 596 RVA: 0x000139A4 File Offset: 0x00011BA4
	[CVarFunction("Set Level", CVarType.t_unknown, EPermission.Moder)]
	internal static void level(object[] values = null)
	{
		Main.UserInfo.currentXP = (float)Globals.I.expTable[System.Convert.ToInt32(CVars.val)];
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000255 RID: 597 RVA: 0x000139E4 File Offset: 0x00011BE4
	[CVarFunction("Set Exp", CVarType.t_unknown, EPermission.Moder)]
	internal static void exp(object[] values = null)
	{
		Main.UserInfo.currentXP = (float)System.Convert.ToInt32(CVars.val);
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
		Main.UserInfo.RefreshPlayerLevel();
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00013A18 File Offset: 0x00011C18
	[CVarFunction("Wipe all wtasks", CVarType.t_unknown, EPermission.Moder)]
	internal static void wipewtasks(object[] values = null)
	{
		for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
		{
			Main.UserInfo.weaponsStates[i].wtaskCurrent = 0f;
		}
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000257 RID: 599 RVA: 0x00013A64 File Offset: 0x00011C64
	[CVarFunction("Set to max all wtasks", CVarType.t_unknown, EPermission.Moder)]
	internal static void masterofarms(object[] values = null)
	{
		for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
		{
			Main.UserInfo.weaponsStates[i].wtaskCurrent = (float)Main.UserInfo.weaponsStates[i].CurrentWeapon.wtask.count;
		}
		Main.AddDatabaseRequest<ServerSaveProfile>(new object[0]);
	}

	// Token: 0x06000258 RID: 600 RVA: 0x00013AC8 File Offset: 0x00011CC8
	[CVarFunction("silence", CVarType.t_unknown, EPermission.Moder)]
	internal static void silence(object[] values = null)
	{
	}

	// Token: 0x06000259 RID: 601 RVA: 0x00013ACC File Offset: 0x00011CCC
	[CVarFunction("ban", CVarType.t_unknown, EPermission.Moder)]
	internal static void ban(object[] values = null)
	{
	}

	// Token: 0x0600025A RID: 602 RVA: 0x00013AD0 File Offset: 0x00011CD0
	[CVarFunction("News", CVarType.t_unknown, EPermission.Moder)]
	internal static void News(object[] values = null)
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.News, Language.ProjectNews, "trekltjerkltjerkltj http://ya.ru", PopupState.news, false, true, "MarkNewsRead", string.Empty));
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00013B08 File Offset: 0x00011D08
	[CVarFunction("AddBeef", CVarType.t_unknown, EPermission.Moder)]
	internal static void AddBeef(object[] values = null)
	{
		EventFactory.Call("AddBeef", new object[]
		{
			"killer",
			"#ffffff",
			5,
			true,
			"beef",
			"#ffffff",
			true,
			true
		});
	}

	// Token: 0x0600025C RID: 604 RVA: 0x00013B6C File Offset: 0x00011D6C
	[CVarFunction("KillBanner", CVarType.t_unknown, EPermission.Moder)]
	internal static void KillBanner(object[] values = null)
	{
		EventFactory.Call("KillBanner", new object[]
		{
			"nick",
			"#ffffff",
			10
		});
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00013BA4 File Offset: 0x00011DA4
	[CVarFunction("DeadBanner", CVarType.t_unknown, EPermission.Moder)]
	internal static void DeadBanner(object[] values = null)
	{
		EventFactory.Call("DeadBanner", new object[]
		{
			5,
			"nick",
			"#ffffff",
			10,
			true
		});
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00013BF0 File Offset: 0x00011DF0
	[CVarFunction("Achievement", CVarType.t_unknown, EPermission.Moder)]
	internal static void Achievement(object[] values = null)
	{
		EventFactory.Call("Achievement", 13);
	}

	// Token: 0x0600025F RID: 607 RVA: 0x00013C04 File Offset: 0x00011E04
	[CVarFunction("Wtask", CVarType.t_unknown, EPermission.Moder)]
	internal static void Wtask(object[] values = null)
	{
		EventFactory.Call("Wtask", new object[]
		{
			"Wtask BlaBlaBla",
			1,
			10
		});
	}

	// Token: 0x06000260 RID: 608 RVA: 0x00013C34 File Offset: 0x00011E34
	internal static void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		if (!isWrite)
		{
			FieldInfo[] fields = typeof(CVars).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			for (int i = 0; i < fields.Length; i++)
			{
				if (dict.ContainsKey(fields[i].Name))
				{
					object[] customAttributes = fields[i].GetCustomAttributes(typeof(CVar), true);
					if (customAttributes.Length != 0 && customAttributes[0] is CVar)
					{
						if (fields[i].GetValue(null) is int)
						{
							fields[i].SetValue(null, System.Convert.ToInt32(dict[fields[i].Name]));
						}
						if (fields[i].GetValue(null) is ObscuredInt)
						{
							ObscuredInt obscuredInt = default(ObscuredInt);
							obscuredInt = System.Convert.ToInt32(dict[fields[i].Name]);
							fields[i].SetValue(null, obscuredInt);
						}
						if (fields[i].GetValue(null) is ObscuredInt[])
						{
							JSON.ReadWrite(dict, "ClientPlayerUserIds", ref CVars.ClientPlayerUserIds, isWrite);
						}
						if (fields[i].GetValue(null) is float)
						{
							fields[i].SetValue(null, System.Convert.ToSingle(dict[fields[i].Name]));
						}
						if (fields[i].GetValue(null) is bool)
						{
							fields[i].SetValue(null, System.Convert.ToBoolean(dict[fields[i].Name]));
						}
						if (fields[i].GetValue(null) is string)
						{
							fields[i].SetValue(null, dict[fields[i].Name]);
						}
						if (fields[i].GetValue(null).GetType() == typeof(Enum))
						{
							fields[i].SetValue(null, System.Convert.ToInt32(dict[fields[i].Name]));
						}
					}
				}
			}
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00013E20 File Offset: 0x00012020
	internal static bool IsStandaloneBackend()
	{
		return CVars.RealmsMovedToStandaloneBackend.Contains(CVars.realm);
	}

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x06000262 RID: 610 RVA: 0x00013E34 File Offset: 0x00012034
	internal static bool IsStandaloneRealm
	{
		get
		{
			return CVars.realm == "standalone";
		}
	}

	// Token: 0x0400027F RID: 639
	internal const string StandaloneRealm = "standalone";

	// Token: 0x04000280 RID: 640
	internal static string line = string.Empty;

	// Token: 0x04000281 RID: 641
	internal static string val = string.Empty;

	// Token: 0x04000282 RID: 642
	[CVar("LeagueButton", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int LeagueLevel = -1;

	// Token: 0x04000283 RID: 643
	[CVar("LeagueLoadMapTimeout", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int LeagueLoadMapTimeout = 180;

	// Token: 0x04000284 RID: 644
	[CVar("LeagueAllReadyStartTimeout", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int LeagueAllReadyStartTimeout = 10;

	// Token: 0x04000285 RID: 645
	[CVar("earlyExitSave", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool earlyExitSave = true;

	// Token: 0x04000286 RID: 646
	[CVar("MinReportReputation", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int MinReportReputation = 100;

	// Token: 0x04000287 RID: 647
	[CVar("StartSuspectCooldownTime", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int StartSuspectCooldownTime = 30;

	// Token: 0x04000288 RID: 648
	[CVar("SuspectCooldownTime", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int SuspectCooldownTime = 15;

	// Token: 0x04000289 RID: 649
	[CVar("AimbotButtonRatio", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float AimbotButtonRatio = 0.5f;

	// Token: 0x0400028A RID: 650
	[CVar("ClientPlayersCount", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static ObscuredInt ClientPlayersCount = 0;

	// Token: 0x0400028B RID: 651
	[CVar("ClientPlayerId", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static ObscuredInt ClientPlayerId = 0;

	// Token: 0x0400028C RID: 652
	[CVar("ClientPlayerUserIds", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static ObscuredInt[] ClientPlayerUserIds = new ObscuredInt[]
	{
		-500,
		-501,
		-502,
		-503,
		-504,
		-505
	};

	// Token: 0x0400028D RID: 653
	[CVar("a", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float a = 1f;

	// Token: 0x0400028E RID: 654
	[CVar("g", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g = 1f;

	// Token: 0x0400028F RID: 655
	[CVar("s", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float s = 1f;

	// Token: 0x04000290 RID: 656
	[CVar("n", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float n = 1f;

	// Token: 0x04000291 RID: 657
	[CVar("Version", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string Version = "1.6797";

	// Token: 0x04000292 RID: 658
	[CVar("IsAimTest", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool IsAimTest = false;

	// Token: 0x04000293 RID: 659
	[CVar("HoldAim", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool HoldAim = false;

	// Token: 0x04000294 RID: 660
	[CVar("UseUnityCache", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool UseUnityCache = false;

	// Token: 0x04000295 RID: 661
	[CVar("EncryptContent", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool EncryptContent = false;

	// Token: 0x04000296 RID: 662
	[CVar("TEAutoBalance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool TEAutoBalance = false;

	// Token: 0x04000297 RID: 663
	[CVar("TCAutoBalance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool TCAutoBalance = false;

	// Token: 0x04000298 RID: 664
	[CVar("BanTime artm;cheng", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int BanTime = 0;

	// Token: 0x04000299 RID: 665
	[CVar("BanTime WAllHack", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int WhBanTime = 0;

	// Token: 0x0400029A RID: 666
	[CVar("BanTime WPE", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int WpeBanTime = 0;

	// Token: 0x0400029B RID: 667
	[CVar("BanTime Injection", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int InjectionBanTime = 0;

	// Token: 0x0400029C RID: 668
	[CVar("Mip Map shot", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool CaptureMipMapScreen = false;

	// Token: 0x0400029D RID: 669
	internal static bool debugMode = false;

	// Token: 0x0400029E RID: 670
	[CVar("InGameChangeTeam", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool InGameChangeTeam = false;

	// Token: 0x0400029F RID: 671
	[CVar("KickAtDBFail", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool KickAtDBFail = false;

	// Token: 0x040002A0 RID: 672
	[CVar("Snow", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool Snow = false;

	// Token: 0x040002A1 RID: 673
	[CVar("Fly Control", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool FlyControl = true;

	// Token: 0x040002A2 RID: 674
	[CVar("test spawn VIP", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool VIP_test = false;

	// Token: 0x040002A3 RID: 675
	[CVar("targetFrameRate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int targetFrameRate = -1;

	// Token: 0x040002A4 RID: 676
	[CVar("Enable radio chat", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_radio = false;

	// Token: 0x040002A5 RID: 677
	[CVar("flud kick timer", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_floodTimer = 10f;

	// Token: 0x040002A6 RID: 678
	[CVar("flud kick counter, message in 10 second", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_floodCounter = 3;

	// Token: 0x040002A7 RID: 679
	[CVar("VIP health amount", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int VIP_health = 1000;

	// Token: 0x040002A8 RID: 680
	[CVar("Allow DM spectate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_allowDMSpectate = true;

	// Token: 0x040002A9 RID: 681
	[CVar("maxQueuedFrames", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int maxQueuedFrames = 0;

	// Token: 0x040002AA RID: 682
	[CVar("g_nextmap", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static Maps fastmap = Maps.develop;

	// Token: 0x040002AB RID: 683
	[CVar("g_assistexp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_assistexp = 5f;

	// Token: 0x040002AC RID: 684
	[CVar("g_vipassistexp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_vipassistexp = 200f;

	// Token: 0x040002AD RID: 685
	[CVar("g_capturexp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_capturexp = 50f;

	// Token: 0x040002AE RID: 686
	[CVar("g_neutralizexp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_neutralizexp = 25f;

	// Token: 0x040002AF RID: 687
	[CVar("g_hardcorexpCoef", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hardcorexpCoef = 2f;

	// Token: 0x040002B0 RID: 688
	[CVar("g_hardcoreDamageCoef", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hardcoreDamageCoef = 1.5f;

	// Token: 0x040002B1 RID: 689
	[CVar("g_hardcoreAccuracyIncrease", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hardcoreAccuracyIncrease = 0f;

	// Token: 0x040002B2 RID: 690
	[CVar("g_hardcoreMobilityIncrease", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hardcoreMobilityIncrease = 0f;

	// Token: 0x040002B3 RID: 691
	[CVar("g_hardcoreRecoilIncrease", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hardcoreRecoilIncrease = 0f;

	// Token: 0x040002B4 RID: 692
	[CVar("Wind", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float px_windpower = 8f;

	// Token: 0x040002B5 RID: 693
	[CVar("Match maximum idle time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_matchMaxTime = 3600f;

	// Token: 0x040002B6 RID: 694
	[CVar("Friendly fire", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_friendlyfire = false;

	// Token: 0x040002B7 RID: 695
	[CVar("Weapon repair coefficient", CVarType.t_database)]
	internal static float g_repairCoef = 0.1f;

	// Token: 0x040002B8 RID: 696
	[CVar("Global exp multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_globalExpMult = 1f;

	// Token: 0x040002B9 RID: 697
	[CVar("IsDefender exp multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_isDefenderExpMult = 1.5f;

	// Token: 0x040002BA RID: 698
	[CVar("Clan leader exp buff multiplier ", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_ClanLeaderExpBuffMult = 1.2f;

	// Token: 0x040002BB RID: 699
	[CVar("Player first place bonus exp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_firstPlaceBonus = 100f;

	// Token: 0x040002BC RID: 700
	[CVar("Player never dead bonus exp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_neverDeadBonus = 200f;

	// Token: 0x040002BD RID: 701
	[CVar("Player team win bonus exp", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_teamWinBonus = 50f;

	// Token: 0x040002BE RID: 702
	[CVar("Player run speed", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_runSpeed = 3.9f;

	// Token: 0x040002BF RID: 703
	[CVar("Player backward speed multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_backwardMult = 0.9f;

	// Token: 0x040002C0 RID: 704
	[CVar("Player side speed multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_sideMult = 1f;

	// Token: 0x040002C1 RID: 705
	[CVar("Player sprint speed multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_sprintMult = 1.5f;

	// Token: 0x040002C2 RID: 706
	[CVar("Player walk speed multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_walkMult = 0.6f;

	// Token: 0x040002C3 RID: 707
	[CVar("Player seat speed multiplier", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_seatMult = 0.4f;

	// Token: 0x040002C4 RID: 708
	[CVar("Reload", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_reload = false;

	// Token: 0x040002C5 RID: 709
	[CVar("Player jump height", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_jumpHeight = 0.8f;

	// Token: 0x040002C6 RID: 710
	[CVar("Tracer speed", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_tracerSpeed = 230f;

	// Token: 0x040002C7 RID: 711
	[CVar("LOD1 min distance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_lod1Distance = 10f;

	// Token: 0x040002C8 RID: 712
	[CVar("LOD2 min distance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_lod2Distance = 15f;

	// Token: 0x040002C9 RID: 713
	[CVar("Grenade Throw Timeout", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_grenadeTimeout = 1.5f;

	// Token: 0x040002CA RID: 714
	[CVar("CollectMultiplePerTick", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_MultipleTick = 4;

	// Token: 0x040002CB RID: 715
	[CVar("Tickrate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_tickrate = 55;

	// Token: 0x040002CC RID: 716
	[CVar("Is bot moving", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_botmove = true;

	// Token: 0x040002CD RID: 717
	[CVar("Debug hits show time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_hitsShowTime = 0f;

	// Token: 0x040002CE RID: 718
	[CVar("Decal effect maximum generate-skip delay", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_decalDelay = 0.05f;

	// Token: 0x040002CF RID: 719
	[CVar("Initial amount of HP", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_baseHealthAmount = 100f;

	// Token: 0x040002D0 RID: 720
	[CVar("Damage debug info", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_damageDebug = false;

	// Token: 0x040002D1 RID: 721
	[CVar("Players required to spawn VIP", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_VIPRequiredPlayers = 2;

	// Token: 0x040002D2 RID: 722
	[CVar("Hardcore", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool hardcore = false;

	// Token: 0x040002D3 RID: 723
	[CVar("Max Clan Message Length", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int ClanMessageLength = 100;

	// Token: 0x040002D4 RID: 724
	[CVar("RandomSpawn", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool RandomSpawn = false;

	// Token: 0x040002D5 RID: 725
	[CVar("Profile reset cost", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int ProfileResetCost = 300;

	// Token: 0x040002D6 RID: 726
	[CVar("Real Currency Multiple", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float RealCurrencyMultiple = 0f;

	// Token: 0x040002D7 RID: 727
	[CVar("race_points", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int race_points = 0;

	// Token: 0x040002D8 RID: 728
	[CVar("Standalone registartion url", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string StandaloneRegistartionUrl = "http://www.contractwarsgame.com/registration";

	// Token: 0x040002D9 RID: 729
	[CVar("Bot type", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_botType = 0;

	// Token: 0x040002DA RID: 730
	internal static eTimer botfireTimer = new eTimer();

	// Token: 0x040002DB RID: 731
	[CVar("Player ClientCmd tune variable", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int s_cmdTune = -2;

	// Token: 0x040002DC RID: 732
	[CVar("Number of shots to damage 1 point of weapon durability", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_shotsToDamageWeapon = 20;

	// Token: 0x040002DD RID: 733
	internal static float g_DamageWeaponReducer = 1f;

	// Token: 0x040002DE RID: 734
	[CVar("Default master-server game name", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string ms_gameName = string.Empty;

	// Token: 0x040002DF RID: 735
	[CVar("Say hello to big rigs world", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool f_bigrigs = false;

	// Token: 0x040002E0 RID: 736
	[CVar("Realm string", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string realm = "release";

	// Token: 0x040002E1 RID: 737
	[CVar("test param", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float test = 1f;

	// Token: 0x040002E2 RID: 738
	internal static string loadTest = string.Empty;

	// Token: 0x040002E3 RID: 739
	[CVar("Player minimum hear distance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_minStepDistance = 3f;

	// Token: 0x040002E4 RID: 740
	[CVar("Player maximum hear distance", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_maxStepDistance = 15f;

	// Token: 0x040002E5 RID: 741
	[CVar("Entity move coeff", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float e_move = 1f;

	// Token: 0x040002E6 RID: 742
	[CVar("Entity lerp coeff ", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float e_movelerk = 0.25f;

	// Token: 0x040002E7 RID: 743
	[CVar("Show WeaponViewer", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool i_showWV = false;

	// Token: 0x040002E8 RID: 744
	[CVar("Use Additive Shader", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool i_useAdditiveShader = true;

	// Token: 0x040002E9 RID: 745
	[CVar("Tournament version", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool i_tour = false;

	// Token: 0x040002EA RID: 746
	[CVar("Pool max size", "", "", CVarType.t_unknown, EPermission.Moder)]
	public static int pl_pool_maxsize = 32;

	// Token: 0x040002EB RID: 747
	[CVar("Player Ammo marker idle_to_out time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_marker_idle_to_out = 0.8f;

	// Token: 0x040002EC RID: 748
	[CVar("Player Ammo marker out_to_idle time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_marker_out_to_idle = 0.8f;

	// Token: 0x040002ED RID: 749
	[CVar("Player Ammo marker idle_to_charge time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_marker_idle_to_charge = 1.5f;

	// Token: 0x040002EE RID: 750
	[CVar("Player Ammo marker charge_to_out time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_marker_charge_to_out = 0.8f;

	// Token: 0x040002EF RID: 751
	[CVar("Player Stand Height", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_stand_height = 0.65f;

	// Token: 0x040002F0 RID: 752
	[CVar("Player Seat Height", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float pl_seat_height = 0.2f;

	// Token: 0x040002F1 RID: 753
	[CVar("Prediction threshold minimum", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float p_thresholdMin = 0.02f;

	// Token: 0x040002F2 RID: 754
	[CVar("Prediction threshold maximum", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float p_thresholdMax = 1.5f;

	// Token: 0x040002F3 RID: 755
	[CVar("Prediction correct speed", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float p_correctSpeed = 5f;

	// Token: 0x040002F4 RID: 756
	[CVar("Interpolation time ammount", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float p_interpolateTime = 0.2f;

	// Token: 0x040002F5 RID: 757
	[CVar("Capsule skin width (decreased)", "0.0f", "10.0f", CVarType.t_unknown, EPermission.Moder)]
	public static float px_capsuleskinwidth = 1f;

	// Token: 0x040002F6 RID: 758
	[CVar("Capsule height", "", "", CVarType.t_unknown, EPermission.Moder)]
	public static float px_capsuleheight = 1.74f;

	// Token: 0x040002F7 RID: 759
	[CVar("Capsule radius", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float px_capsuleradius = 0.3f;

	// Token: 0x040002F8 RID: 760
	[CVar("Vanilla client", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool IsVanilla = false;

	// Token: 0x040002F9 RID: 761
	[CVar("Client version", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string HopsActivationInstructionUrl = "https://www.hiredops.com/news/id/61";

	// Token: 0x040002FA RID: 762
	[CVar("Client version", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int c_clientVersion = 0;

	// Token: 0x040002FB RID: 763
	[CVar("Request protocol", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string n_protocol = "http://";

	// Token: 0x040002FC RID: 764
	[CVar("Content protocol", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static string n_contentProtocol = "http://";

	// Token: 0x040002FD RID: 765
	[CVar("Admin Grenade", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool a_grenade = false;

	// Token: 0x040002FE RID: 766
	[CVar("Debug framerate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool d_rate = false;

	// Token: 0x040002FF RID: 767
	[CVar("Server maximum connections", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int n_serverMaxConnections = 30;

	// Token: 0x04000300 RID: 768
	[CVar("Contracts enabled", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool c_contractsEnabled = true;

	// Token: 0x04000301 RID: 769
	[CVar("Contracts reset time", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int contractsTimer = 24;

	// Token: 0x04000302 RID: 770
	[CVar("Maximum matches", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int n_maxMatches = 10;

	// Token: 0x04000303 RID: 771
	[CVar("MasterServer port", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int n_masterServerPort = 23466;

	// Token: 0x04000304 RID: 772
	internal static Vector3 h_v3infinity = new Vector3(9999999f, 9999999f, 9999999f);

	// Token: 0x04000305 RID: 773
	internal static Vector2 h_v2infinity = new Vector2(9999999f, 9999999f);

	// Token: 0x04000306 RID: 774
	internal static int h_networkBufferMaxSize = 128;

	// Token: 0x04000307 RID: 775
	[CVar("Stats double kill delay", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float g_killDelay = 2f;

	// Token: 0x04000308 RID: 776
	[CVar("Server target framerate", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int s_tfr = 20;

	// Token: 0x04000309 RID: 777
	[CVar("Show Debug Errors", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool d_showerror = true;

	// Token: 0x0400030A RID: 778
	[CVar("Enable in-game chat", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool g_chatEnabled = false;

	// Token: 0x0400030B RID: 779
	[CVar("Repa required to chat", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int g_chatRepaRequired = 100;

	// Token: 0x0400030C RID: 780
	[CVar("Use unity http request", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool n_unityhttp = false;

	// Token: 0x0400030D RID: 781
	[CVar("Use http debug mode", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool n_httpDebug = false;

	// Token: 0x0400030E RID: 782
	[CVar("Use http debug mode for loading assets", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool n_httpAssetsDebug = false;

	// Token: 0x0400030F RID: 783
	[CVar("Accept GZIP", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool n_acceptGzip = false;

	// Token: 0x04000310 RID: 784
	[CVar("Server send frequency", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int n_updaterate = 10;

	// Token: 0x04000311 RID: 785
	[CVar("Client send frequency", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int n_cmdrate = 20;

	// Token: 0x04000312 RID: 786
	[CVar("Unload and destroy asset bundle", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static bool n_unloadAssetBundle = true;

	// Token: 0x04000313 RID: 787
	[CVar("Virtual ping", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float n_ping = 0f;

	// Token: 0x04000314 RID: 788
	[CVar("Virtual packet lossage", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static float n_lossage = 0f;

	// Token: 0x04000315 RID: 789
	[CVar("PageManager editing", "", "", CVarType.t_unknown, EPermission.Moder)]
	public static int i_pagemanager = 0;

	// Token: 0x04000316 RID: 790
	[CVar("AWHType", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int AWHType = 0;

	// Token: 0x04000317 RID: 791
	[CVar("InjCheck", "", "", CVarType.t_unknown, EPermission.Moder)]
	internal static int InjCheck = 1;

	// Token: 0x04000318 RID: 792
	internal static int InjCheckCount = 3;

	// Token: 0x04000319 RID: 793
	internal static bool WHLOG = false;

	// Token: 0x0400031A RID: 794
	private static readonly string[] RealmsMovedToStandaloneBackend = new string[]
	{
		"standalone",
		"mailru",
		"ok",
		"vk",
		"fb",
		"kg",
		"mc",
		"ag"
	};
}
