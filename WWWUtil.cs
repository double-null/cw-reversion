using System;
using UnityEngine;

// Token: 0x020003A5 RID: 933
internal class WWWUtil
{
	// Token: 0x17000855 RID: 2133
	// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x00104C28 File Offset: 0x00102E28
	public static string protocol
	{
		get
		{
			return CVars.n_protocol;
		}
	}

	// Token: 0x17000856 RID: 2134
	// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00104C30 File Offset: 0x00102E30
	public static string contentProtocol
	{
		get
		{
			return CVars.n_contentProtocol;
		}
	}

	// Token: 0x17000857 RID: 2135
	// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x00104C38 File Offset: 0x00102E38
	public static string extension
	{
		get
		{
			return (!Main.ExternalContent) ? ".prefab" : ".unity3d";
		}
	}

	// Token: 0x17000858 RID: 2136
	// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x00104C54 File Offset: 0x00102E54
	public static string ContentHost
	{
		get
		{
			return WWWUtil.contentProtocol + WWWUtil.contentWWW;
		}
	}

	// Token: 0x17000859 RID: 2137
	// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x00104C68 File Offset: 0x00102E68
	public static string databaseWWW
	{
		get
		{
			return Globals.I.databaseIP + Globals.I.databaseRoot;
		}
	}

	// Token: 0x1700085A RID: 2138
	// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x00104C84 File Offset: 0x00102E84
	public static string contentWWW
	{
		get
		{
			if (WWWUtil.mirror == string.Empty)
			{
				WWWUtil.mirror = Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)];
			}
			return WWWUtil.mirror + Globals.I.contentRoot;
		}
	}

	// Token: 0x06001DD9 RID: 7641 RVA: 0x00104CE0 File Offset: 0x00102EE0
	public static string HttpMSWWW()
	{
		return CVars.n_protocol + WWWUtil.databaseWWW + "ms/";
	}

	// Token: 0x06001DDA RID: 7642 RVA: 0x00104CF8 File Offset: 0x00102EF8
	public static string infoWWW(string name)
	{
		return CVars.n_protocol + WWWUtil.databaseWWW + "info/" + name;
	}

	// Token: 0x06001DDB RID: 7643 RVA: 0x00104D10 File Offset: 0x00102F10
	public static string musicWWW(string name)
	{
		return WWWUtil.contentProtocol + WWWUtil.contentWWW + "Music/" + name;
	}

	// Token: 0x06001DDC RID: 7644 RVA: 0x00104D28 File Offset: 0x00102F28
	public static string levelsWWW(string name)
	{
		string text = (!Main.ExternalContent) ? ".unity" : ".unity3d";
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"Levels/",
			name,
			text
		});
	}

	// Token: 0x06001DDD RID: 7645 RVA: 0x00104D7C File Offset: 0x00102F7C
	public static string weaponsWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"Entities/weapons/",
			name,
			WWWUtil.extension
		});
	}

	// Token: 0x06001DDE RID: 7646 RVA: 0x00104DB0 File Offset: 0x00102FB0
	public static string handsWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"Entities/weapons-hands/",
			name,
			"-hands",
			WWWUtil.extension
		});
	}

	// Token: 0x06001DDF RID: 7647 RVA: 0x00104DEC File Offset: 0x00102FEC
	public static string lodsWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"Entities/weapons-lods/",
			name,
			"-lod",
			WWWUtil.extension
		});
	}

	// Token: 0x06001DE0 RID: 7648 RVA: 0x00104E28 File Offset: 0x00103028
	public static string rootWWW(string name)
	{
		return WWWUtil.contentProtocol + WWWUtil.contentWWW + name + WWWUtil.extension;
	}

	// Token: 0x06001DE1 RID: 7649 RVA: 0x00104E40 File Offset: 0x00103040
	public static string rootWWWNoExtension(string name)
	{
		return WWWUtil.contentProtocol + WWWUtil.contentWWW + name;
	}

	// Token: 0x06001DE2 RID: 7650 RVA: 0x00104E54 File Offset: 0x00103054
	public static string levelsplashWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"img/levelsplash/",
			name,
			".jpg"
		});
	}

	// Token: 0x06001DE3 RID: 7651 RVA: 0x00104E88 File Offset: 0x00103088
	public static string leagueAdWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"League/img/ad/",
			name,
			".jpg"
		});
	}

	// Token: 0x06001DE4 RID: 7652 RVA: 0x00104EBC File Offset: 0x001030BC
	public static string leaguePrizeWWW(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"League/img/prize/",
			name,
			".png"
		});
	}

	// Token: 0x06001DE5 RID: 7653 RVA: 0x00104EF0 File Offset: 0x001030F0
	public static string leagueRulesWWW()
	{
		return WWWUtil.contentProtocol + WWWUtil.contentWWW + "League/rules.txt";
	}

	// Token: 0x06001DE6 RID: 7654 RVA: 0x00104F08 File Offset: 0x00103108
	public static string AdditiveAsset(string name)
	{
		return string.Concat(new string[]
		{
			WWWUtil.contentProtocol,
			WWWUtil.contentWWW,
			"Events/Winter/",
			name,
			WWWUtil.extension
		});
	}

	// Token: 0x06001DE7 RID: 7655 RVA: 0x00104F3C File Offset: 0x0010313C
	public static string modIconWWW(string name)
	{
		if (WWWUtil.mirror == string.Empty)
		{
			WWWUtil.mirror = Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)];
		}
		return string.Concat(new string[]
		{
			CVars.n_contentProtocol,
			WWWUtil.mirror,
			Globals.I.contentRoot,
			"files/customization/mod/icons/",
			name,
			".png"
		});
	}

	// Token: 0x06001DE8 RID: 7656 RVA: 0x00104FC0 File Offset: 0x001031C0
	public static string camoIconWWW(string name)
	{
		if (WWWUtil.mirror == string.Empty)
		{
			WWWUtil.mirror = Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)];
		}
		return string.Concat(new string[]
		{
			CVars.n_contentProtocol,
			WWWUtil.mirror,
			Globals.I.contentRoot,
			"files/customization/camo/icons/",
			name,
			".jpg"
		});
	}

	// Token: 0x06001DE9 RID: 7657 RVA: 0x00105044 File Offset: 0x00103244
	public static string camoTextureWWW(string name)
	{
		if (WWWUtil.mirror == string.Empty)
		{
			WWWUtil.mirror = Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)];
		}
		return string.Concat(new string[]
		{
			CVars.n_contentProtocol,
			WWWUtil.mirror,
			Globals.I.contentRoot,
			"files/customization/camo/textures/",
			name,
			".unity3d"
		});
	}

	// Token: 0x06001DEA RID: 7658 RVA: 0x001050C8 File Offset: 0x001032C8
	public static string levelsUrl(string name)
	{
		if (Main.ExternalContent)
		{
			return "Levels/" + name + ".unity3d";
		}
		return "Levels/" + name + ".unity";
	}

	// Token: 0x06001DEB RID: 7659 RVA: 0x001050F8 File Offset: 0x001032F8
	public static string weaponsUrl(string name)
	{
		return Utility.GetWeaponPrefabPath(name, WeaponPrefabType.Weapon) + Utility.GetWeaponPrefabName(name, WeaponPrefabType.Weapon) + WWWUtil.extension;
	}

	// Token: 0x06001DEC RID: 7660 RVA: 0x00105114 File Offset: 0x00103314
	public static string handsUrl(string name)
	{
		return Utility.GetWeaponPrefabPath(name, WeaponPrefabType.Hands) + Utility.GetWeaponPrefabName(name, WeaponPrefabType.Hands) + WWWUtil.extension;
	}

	// Token: 0x06001DED RID: 7661 RVA: 0x00105130 File Offset: 0x00103330
	public static string lodsUrl(string name)
	{
		return Utility.GetWeaponPrefabPath(name, WeaponPrefabType.Lod) + Utility.GetWeaponPrefabName(name, WeaponPrefabType.Lod) + WWWUtil.extension;
	}

	// Token: 0x06001DEE RID: 7662 RVA: 0x0010514C File Offset: 0x0010334C
	public static string ModsUrl(MasteringMod mod)
	{
		return Utility.GetModPrefabPath(WeaponPrefabType.Weapon) + Utility.GetModPrefabName(mod, WeaponPrefabType.Weapon) + WWWUtil.extension;
	}

	// Token: 0x06001DEF RID: 7663 RVA: 0x00105168 File Offset: 0x00103368
	public static string ModsLodUrl(MasteringMod mod)
	{
		return Utility.GetModPrefabPath(WeaponPrefabType.Lod) + Utility.GetModPrefabName(mod, WeaponPrefabType.Lod) + WWWUtil.extension;
	}

	// Token: 0x06001DF0 RID: 7664 RVA: 0x00105184 File Offset: 0x00103384
	public static string rootUrl(string name)
	{
		return name + WWWUtil.extension;
	}

	// Token: 0x06001DF1 RID: 7665 RVA: 0x00105194 File Offset: 0x00103394
	public static string rootUrlNoExtension(string name)
	{
		return name;
	}

	// Token: 0x06001DF2 RID: 7666 RVA: 0x00105198 File Offset: 0x00103398
	public static string levelsplashUrl(string name)
	{
		return "img/levelsplash/" + name + ".jpg";
	}

	// Token: 0x06001DF3 RID: 7667 RVA: 0x001051AC File Offset: 0x001033AC
	public static string leagueAdUrl(string name)
	{
		return "League/img/ad/" + name + ".jpg";
	}

	// Token: 0x06001DF4 RID: 7668 RVA: 0x001051C0 File Offset: 0x001033C0
	public static string leaguePrizeUrl(string name)
	{
		return "League/img/prize/" + name + ".png";
	}

	// Token: 0x06001DF5 RID: 7669 RVA: 0x001051D4 File Offset: 0x001033D4
	public static string leagueRulesUrl()
	{
		return "League/rules.txt";
	}

	// Token: 0x06001DF6 RID: 7670 RVA: 0x001051DC File Offset: 0x001033DC
	public static string AdditiveAssetUrl(string name)
	{
		return "Events/Winter/" + name + WWWUtil.extension;
	}

	// Token: 0x06001DF7 RID: 7671 RVA: 0x001051F0 File Offset: 0x001033F0
	public static void ChangeContentServer()
	{
		WWWUtil.mirror = Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)];
	}

	// Token: 0x06001DF8 RID: 7672 RVA: 0x00105224 File Offset: 0x00103424
	public static string GetRandomContentServer()
	{
		return WWWUtil.contentProtocol + Globals.I.contentIP[(int)(UnityEngine.Random.value * (float)Globals.I.contentIP.Length)] + Globals.I.contentRoot;
	}

	// Token: 0x04002254 RID: 8788
	internal const string StandaloneDBRoot = "/";

	// Token: 0x04002255 RID: 8789
	internal const string StandaloneDBIp = "gw-01.contractwarsgame.com";

	// Token: 0x04002256 RID: 8790
	internal const string StandaloneVanillaDBIp = "cw-revival.contractwarsgame.com";

	// Token: 0x04002257 RID: 8791
	internal const string StandaloneTestDBIp = "dev.contractwarsgame.com";

	// Token: 0x04002258 RID: 8792
	internal const string StandaloneTestDBIp2 = "dev2.contractwarsgame.com";

	// Token: 0x04002259 RID: 8793
	public static string mirror = string.Empty;

	// Token: 0x0400225A RID: 8794
	public static bool contentIpLoaded;
}
