using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002FA RID: 762
internal class Language
{
	// Token: 0x17000343 RID: 835
	// (get) Token: 0x060015A0 RID: 5536 RVA: 0x000EA424 File Offset: 0x000E8624
	internal static string[] Languages
	{
		get
		{
			if (Language._languages == null)
			{
				Language._languages = Enum.GetNames(typeof(ELanguage));
			}
			return Language._languages;
		}
	}

	// Token: 0x17000344 RID: 836
	// (get) Token: 0x060015A1 RID: 5537 RVA: 0x000EA44C File Offset: 0x000E864C
	public static string CurrentLanguageStr
	{
		get
		{
			ELanguage lang = Language.sL.Lang;
			if (lang == ELanguage.EN)
			{
				return Language.sL.EngLanguage;
			}
			if (lang != ELanguage.RU)
			{
				return Language.sL.EngLanguage;
			}
			return Language.sL.RusLanguage;
		}
	}

	// Token: 0x17000345 RID: 837
	// (get) Token: 0x060015A2 RID: 5538 RVA: 0x000EA498 File Offset: 0x000E8698
	// (set) Token: 0x060015A3 RID: 5539 RVA: 0x000EA4A4 File Offset: 0x000E86A4
	public static ELanguage CurrentLanguage
	{
		get
		{
			return Language.sL.Lang;
		}
		set
		{
			if (value == Language.sL.Lang)
			{
				return;
			}
			if (value != ELanguage.EN)
			{
				if (value != ELanguage.RU)
				{
					Debug.LogError("Unknown Language");
				}
				else
				{
					if (Language.rus == null)
					{
						Language.rus = new RUSLang();
					}
					Language.SetLanguage(Language.rus);
				}
			}
			else
			{
				if (Language.eng == null)
				{
					Language.eng = new ENGLang();
				}
				Language.SetLanguage(Language.eng);
			}
			if (CVars.IsStandaloneRealm && SingletoneComponent<Main>.Instance != null && Main.UserInfo.weaponsStates != null)
			{
				for (int i = 0; i < Main.UserInfo.weaponsStates.Length; i++)
				{
					WeaponInfo weaponInfo = Main.UserInfo.weaponsStates[i];
					weaponInfo.CurrentWeapon.LoadTable(Globals.I.weapons[i]);
				}
			}
			if (CVars.IsStandaloneRealm)
			{
				PlayerPrefs.SetInt("Language", (int)value);
				PlayerPrefs.Save();
			}
		}
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x000EA5B0 File Offset: 0x000E87B0
	private static void SetLanguage(BaseLanguage l)
	{
		if (Language.sL == l)
		{
			return;
		}
		Language.sL = l;
		Language.sL.SetLanguage();
		Language.sL.ReAssembleMass();
	}

	// Token: 0x17000346 RID: 838
	// (get) Token: 0x060015A5 RID: 5541 RVA: 0x000EA5E4 File Offset: 0x000E87E4
	public static string CWMainLoading
	{
		get
		{
			return Language.sL.CWMainLoading;
		}
	}

	// Token: 0x17000347 RID: 839
	// (get) Token: 0x060015A6 RID: 5542 RVA: 0x000EA5F0 File Offset: 0x000E87F0
	public static string CWMainGlobalInfoLoading
	{
		get
		{
			return Language.sL.CWMainGlobalInfoLoading;
		}
	}

	// Token: 0x17000348 RID: 840
	// (get) Token: 0x060015A7 RID: 5543 RVA: 0x000EA5FC File Offset: 0x000E87FC
	public static string CWMainGlobalInfoLoadingFinished
	{
		get
		{
			return Language.sL.CWMainGlobalInfoLoadingFinished;
		}
	}

	// Token: 0x17000349 RID: 841
	// (get) Token: 0x060015A8 RID: 5544 RVA: 0x000EA608 File Offset: 0x000E8808
	public static string CWMainLoginDesc
	{
		get
		{
			return Language.sL.CWMainLoginDesc;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x060015A9 RID: 5545 RVA: 0x000EA614 File Offset: 0x000E8814
	public static string CWMainLoginFinishedDesc
	{
		get
		{
			return Language.sL.CWMainLoginFinishedDesc;
		}
	}

	// Token: 0x1700034B RID: 843
	// (get) Token: 0x060015AA RID: 5546 RVA: 0x000EA620 File Offset: 0x000E8820
	public static string CWMainInitUserDesc
	{
		get
		{
			return Language.sL.CWMainInitUserDesc;
		}
	}

	// Token: 0x1700034C RID: 844
	// (get) Token: 0x060015AB RID: 5547 RVA: 0x000EA62C File Offset: 0x000E882C
	public static string CWMainInitUserFinishedDesc
	{
		get
		{
			return Language.sL.CWMainInitUserFinishedDesc;
		}
	}

	// Token: 0x1700034D RID: 845
	// (get) Token: 0x060015AC RID: 5548 RVA: 0x000EA638 File Offset: 0x000E8838
	public static string CWMainLoad
	{
		get
		{
			return Language.sL.CWMainLoad;
		}
	}

	// Token: 0x1700034E RID: 846
	// (get) Token: 0x060015AD RID: 5549 RVA: 0x000EA644 File Offset: 0x000E8844
	public static string CWMainLoadDesc
	{
		get
		{
			return Language.sL.CWMainLoadDesc;
		}
	}

	// Token: 0x1700034F RID: 847
	// (get) Token: 0x060015AE RID: 5550 RVA: 0x000EA650 File Offset: 0x000E8850
	public static string CWMainLoadFinishedDesc
	{
		get
		{
			return Language.sL.CWMainLoadFinishedDesc;
		}
	}

	// Token: 0x17000350 RID: 848
	// (get) Token: 0x060015AF RID: 5551 RVA: 0x000EA65C File Offset: 0x000E885C
	public static string CWMainLoadError
	{
		get
		{
			return Language.sL.CWMainLoadError;
		}
	}

	// Token: 0x17000351 RID: 849
	// (get) Token: 0x060015B0 RID: 5552 RVA: 0x000EA668 File Offset: 0x000E8868
	public static string CWMainLoadErrorDesc
	{
		get
		{
			return Language.sL.CWMainLoadErrorDesc;
		}
	}

	// Token: 0x17000352 RID: 850
	// (get) Token: 0x060015B1 RID: 5553 RVA: 0x000EA674 File Offset: 0x000E8874
	public static string CWMainSave
	{
		get
		{
			return Language.sL.CWMainSave;
		}
	}

	// Token: 0x17000353 RID: 851
	// (get) Token: 0x060015B2 RID: 5554 RVA: 0x000EA680 File Offset: 0x000E8880
	public static string CWMainSaveDesc
	{
		get
		{
			return Language.sL.CWMainSaveDesc;
		}
	}

	// Token: 0x17000354 RID: 852
	// (get) Token: 0x060015B3 RID: 5555 RVA: 0x000EA68C File Offset: 0x000E888C
	public static string CWMainSaveFinishedDesc
	{
		get
		{
			return Language.sL.CWMainSaveFinishedDesc;
		}
	}

	// Token: 0x17000355 RID: 853
	// (get) Token: 0x060015B4 RID: 5556 RVA: 0x000EA698 File Offset: 0x000E8898
	public static string CWMainSaveError
	{
		get
		{
			return Language.sL.CWMainSaveError;
		}
	}

	// Token: 0x17000356 RID: 854
	// (get) Token: 0x060015B5 RID: 5557 RVA: 0x000EA6A4 File Offset: 0x000E88A4
	public static string CWMainSaveErrorDesc
	{
		get
		{
			return Language.sL.CWMainSaveErrorDesc;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x060015B6 RID: 5558 RVA: 0x000EA6B0 File Offset: 0x000E88B0
	public static string CWMainWeaponUnlock
	{
		get
		{
			return Language.sL.CWMainWeaponUnlock;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x060015B7 RID: 5559 RVA: 0x000EA6BC File Offset: 0x000E88BC
	public static string CWMainWeaponUnlockDesc
	{
		get
		{
			return Language.sL.CWMainWeaponUnlockDesc;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x060015B8 RID: 5560 RVA: 0x000EA6C8 File Offset: 0x000E88C8
	public static string CWMainWeaponUnlockFinishedDesc
	{
		get
		{
			return Language.sL.CWMainWeaponUnlockFinishedDesc;
		}
	}

	// Token: 0x1700035A RID: 858
	// (get) Token: 0x060015B9 RID: 5561 RVA: 0x000EA6D4 File Offset: 0x000E88D4
	public static string CWMainWeaponUnlockError
	{
		get
		{
			return Language.sL.CWMainWeaponUnlockError;
		}
	}

	// Token: 0x1700035B RID: 859
	// (get) Token: 0x060015BA RID: 5562 RVA: 0x000EA6E0 File Offset: 0x000E88E0
	public static string CWMainWeaponUnlockErrorDesc
	{
		get
		{
			return Language.sL.CWMainWeaponUnlockErrorDesc;
		}
	}

	// Token: 0x1700035C RID: 860
	// (get) Token: 0x060015BB RID: 5563 RVA: 0x000EA6EC File Offset: 0x000E88EC
	public static string CWMainSkillUnlock
	{
		get
		{
			return Language.sL.CWMainSkillUnlock;
		}
	}

	// Token: 0x1700035D RID: 861
	// (get) Token: 0x060015BC RID: 5564 RVA: 0x000EA6F8 File Offset: 0x000E88F8
	public static string CWMainSkillUnlockDesc
	{
		get
		{
			return Language.sL.CWMainSkillUnlockDesc;
		}
	}

	// Token: 0x1700035E RID: 862
	// (get) Token: 0x060015BD RID: 5565 RVA: 0x000EA704 File Offset: 0x000E8904
	public static string CWMainSkillUnlockFinishedDesc
	{
		get
		{
			return Language.sL.CWMainSkillUnlockFinishedDesc;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x060015BE RID: 5566 RVA: 0x000EA710 File Offset: 0x000E8910
	public static string CWMainSkillUnlockError
	{
		get
		{
			return Language.sL.CWMainSkillUnlockError;
		}
	}

	// Token: 0x17000360 RID: 864
	// (get) Token: 0x060015BF RID: 5567 RVA: 0x000EA71C File Offset: 0x000E891C
	public static string CWMainSkillUnlockErrorDesc
	{
		get
		{
			return Language.sL.CWMainSkillUnlockErrorDesc;
		}
	}

	// Token: 0x17000361 RID: 865
	// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000EA728 File Offset: 0x000E8928
	public static string CWMainLoadRating
	{
		get
		{
			return Language.sL.CWMainLoadRating;
		}
	}

	// Token: 0x17000362 RID: 866
	// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000EA734 File Offset: 0x000E8934
	public static string CWMainLoadRatingDesc
	{
		get
		{
			return Language.sL.CWMainLoadRatingDesc;
		}
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000EA740 File Offset: 0x000E8940
	public static string CWMainLoadRatingFinishedDesc
	{
		get
		{
			return Language.sL.CWMainLoadRatingFinishedDesc;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000EA74C File Offset: 0x000E894C
	public static string CWMainLoadRatingError
	{
		get
		{
			return Language.sL.CWMainLoadRatingError;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x060015C4 RID: 5572 RVA: 0x000EA758 File Offset: 0x000E8958
	public static string CWMainLoadRatingErrorDesc
	{
		get
		{
			return Language.sL.CWMainLoadRatingErrorDesc;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x060015C5 RID: 5573 RVA: 0x000EA764 File Offset: 0x000E8964
	public static string DownloadAdditionalGameDataDesc
	{
		get
		{
			return Language.sL.DownloadAdditionalGameDataDesc;
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000EA770 File Offset: 0x000E8970
	public static string Error
	{
		get
		{
			return Language.sL.Error;
		}
	}

	// Token: 0x17000368 RID: 872
	// (get) Token: 0x060015C7 RID: 5575 RVA: 0x000EA77C File Offset: 0x000E897C
	public static string Connection
	{
		get
		{
			return Language.sL.Connection;
		}
	}

	// Token: 0x17000369 RID: 873
	// (get) Token: 0x060015C8 RID: 5576 RVA: 0x000EA788 File Offset: 0x000E8988
	public static string TryingConnection
	{
		get
		{
			return Language.sL.TryingConnection;
		}
	}

	// Token: 0x1700036A RID: 874
	// (get) Token: 0x060015C9 RID: 5577 RVA: 0x000EA794 File Offset: 0x000E8994
	public static string ConnectionFailed
	{
		get
		{
			return Language.sL.ConnectionFailed;
		}
	}

	// Token: 0x1700036B RID: 875
	// (get) Token: 0x060015CA RID: 5578 RVA: 0x000EA7A0 File Offset: 0x000E89A0
	public static string ConnectionFailedOnDelay
	{
		get
		{
			return Language.sL.ConnectionFailedOnDelay;
		}
	}

	// Token: 0x1700036C RID: 876
	// (get) Token: 0x060015CB RID: 5579 RVA: 0x000EA7AC File Offset: 0x000E89AC
	public static string ConnectionCompleted
	{
		get
		{
			return Language.sL.ConnectionCompleted;
		}
	}

	// Token: 0x1700036D RID: 877
	// (get) Token: 0x060015CC RID: 5580 RVA: 0x000EA7B8 File Offset: 0x000E89B8
	public static string ServerDisconnectYou
	{
		get
		{
			return Language.sL.ServerDisconnectYou;
		}
	}

	// Token: 0x1700036E RID: 878
	// (get) Token: 0x060015CD RID: 5581 RVA: 0x000EA7C4 File Offset: 0x000E89C4
	public static string FailedToConnectToMasterServer
	{
		get
		{
			return Language.sL.FailedToConnectToMasterServer;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x060015CE RID: 5582 RVA: 0x000EA7D0 File Offset: 0x000E89D0
	public static string ServerCreation
	{
		get
		{
			return Language.sL.ServerCreation;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x060015CF RID: 5583 RVA: 0x000EA7DC File Offset: 0x000E89DC
	public static string ServerCreationFailed
	{
		get
		{
			return Language.sL.ServerCreationFailed;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000EA7E8 File Offset: 0x000E89E8
	public static string BannerGUIKnife
	{
		get
		{
			return Language.sL.BannerGUIKnife;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x060015D1 RID: 5585 RVA: 0x000EA7F4 File Offset: 0x000E89F4
	public static string BannerGUIGrenade
	{
		get
		{
			return Language.sL.BannerGUIGrenade;
		}
	}

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x060015D2 RID: 5586 RVA: 0x000EA800 File Offset: 0x000E8A00
	public static string BannerGUIMortarStrike
	{
		get
		{
			return Language.sL.BannerGUIMortarStrike;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x060015D3 RID: 5587 RVA: 0x000EA80C File Offset: 0x000E8A0C
	public static string Push
	{
		get
		{
			return Language.sL.Push;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x060015D4 RID: 5588 RVA: 0x000EA818 File Offset: 0x000E8A18
	public static string NeedMoreWarrior
	{
		get
		{
			return Language.sL.NeedMoreWarrior;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x060015D5 RID: 5589 RVA: 0x000EA824 File Offset: 0x000E8A24
	public static string CapturingPoint
	{
		get
		{
			return Language.sL.CapturingPoint;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x060015D6 RID: 5590 RVA: 0x000EA830 File Offset: 0x000E8A30
	public static string EnemyCapturingYourPoint
	{
		get
		{
			return Language.sL.EnemyCapturingYourPoint;
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000EA83C File Offset: 0x000E8A3C
	public static string NeutralizePoint
	{
		get
		{
			return Language.sL.NeutralizePoint;
		}
	}

	// Token: 0x17000379 RID: 889
	// (get) Token: 0x060015D8 RID: 5592 RVA: 0x000EA848 File Offset: 0x000E8A48
	public static string FriendCaptured
	{
		get
		{
			return Language.sL.FriendCaptured;
		}
	}

	// Token: 0x1700037A RID: 890
	// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000EA854 File Offset: 0x000E8A54
	public static string EnemyCaptured
	{
		get
		{
			return Language.sL.EnemyCaptured;
		}
	}

	// Token: 0x1700037B RID: 891
	// (get) Token: 0x060015DA RID: 5594 RVA: 0x000EA860 File Offset: 0x000E8A60
	public static string PointBearCaptured
	{
		get
		{
			return Language.sL.PointBearCaptured;
		}
	}

	// Token: 0x1700037C RID: 892
	// (get) Token: 0x060015DB RID: 5595 RVA: 0x000EA86C File Offset: 0x000E8A6C
	public static string PointUsecCaptured
	{
		get
		{
			return Language.sL.PointUsecCaptured;
		}
	}

	// Token: 0x1700037D RID: 893
	// (get) Token: 0x060015DC RID: 5596 RVA: 0x000EA878 File Offset: 0x000E8A78
	public static string PointPurification
	{
		get
		{
			return Language.sL.PointPurification;
		}
	}

	// Token: 0x1700037E RID: 894
	// (get) Token: 0x060015DD RID: 5597 RVA: 0x000EA884 File Offset: 0x000E8A84
	public static string NeedMoreBear
	{
		get
		{
			return Language.sL.NeedMoreBear;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x060015DE RID: 5598 RVA: 0x000EA890 File Offset: 0x000E8A90
	public static string NeedMoreUsec
	{
		get
		{
			return Language.sL.NeedMoreUsec;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x060015DF RID: 5599 RVA: 0x000EA89C File Offset: 0x000E8A9C
	public static string[] PointName
	{
		get
		{
			return Language.sL.PointName;
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x060015E0 RID: 5600 RVA: 0x000EA8A8 File Offset: 0x000E8AA8
	public static string Point
	{
		get
		{
			return Language.sL.Point;
		}
	}

	// Token: 0x17000382 RID: 898
	// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000EA8B4 File Offset: 0x000E8AB4
	public static string DownloadWeapons
	{
		get
		{
			return Language.sL.DownloadWeapons;
		}
	}

	// Token: 0x17000383 RID: 899
	// (get) Token: 0x060015E2 RID: 5602 RVA: 0x000EA8C0 File Offset: 0x000E8AC0
	public static string WeaponsLoaded
	{
		get
		{
			return Language.sL.WeaponsLoaded;
		}
	}

	// Token: 0x17000384 RID: 900
	// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000EA8CC File Offset: 0x000E8ACC
	public static string DownloadMaps
	{
		get
		{
			return Language.sL.DownloadMaps;
		}
	}

	// Token: 0x17000385 RID: 901
	// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000EA8D8 File Offset: 0x000E8AD8
	public static string MapsLoaded
	{
		get
		{
			return Language.sL.MapsLoaded;
		}
	}

	// Token: 0x17000386 RID: 902
	// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000EA8E4 File Offset: 0x000E8AE4
	public static string ReceivingInformation
	{
		get
		{
			return Language.sL.ReceivingInformation;
		}
	}

	// Token: 0x17000387 RID: 903
	// (get) Token: 0x060015E6 RID: 5606 RVA: 0x000EA8F0 File Offset: 0x000E8AF0
	public static string ErrorDownloadingContent
	{
		get
		{
			return Language.sL.ErrorDownloadingContent;
		}
	}

	// Token: 0x17000388 RID: 904
	// (get) Token: 0x060015E7 RID: 5607 RVA: 0x000EA8FC File Offset: 0x000E8AFC
	public static string WeaponSizeLoaded
	{
		get
		{
			return Language.sL.WeaponSizeLoaded;
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x060015E8 RID: 5608 RVA: 0x000EA908 File Offset: 0x000E8B08
	public static string SizeTotal
	{
		get
		{
			return Language.sL.SizeTotal;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000EA914 File Offset: 0x000E8B14
	public static string DownloadAllWeapons
	{
		get
		{
			return Language.sL.DownloadAllWeapons;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x060015EA RID: 5610 RVA: 0x000EA920 File Offset: 0x000E8B20
	public static string MapsSizeLoaded
	{
		get
		{
			return Language.sL.MapsSizeLoaded;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x060015EB RID: 5611 RVA: 0x000EA92C File Offset: 0x000E8B2C
	public static string DownloadAllMaps
	{
		get
		{
			return Language.sL.DownloadAllMaps;
		}
	}

	// Token: 0x060015EC RID: 5612 RVA: 0x000EA938 File Offset: 0x000E8B38
	public static string PressM(KeyCode button)
	{
		return Language.sL.PressM(button);
	}

	// Token: 0x060015ED RID: 5613 RVA: 0x000EA948 File Offset: 0x000E8B48
	public static string Press3Mortar(KeyCode button)
	{
		return Language.sL.Press3Mortar(button);
	}

	// Token: 0x060015EE RID: 5614 RVA: 0x000EA958 File Offset: 0x000E8B58
	public static string PressMouse2(KeyCode button)
	{
		return Language.sL.PressMouse2(button);
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x060015EF RID: 5615 RVA: 0x000EA968 File Offset: 0x000E8B68
	public static string PlayerRecieveSonar
	{
		get
		{
			return Language.sL.PlayerRecieveSonar;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000EA974 File Offset: 0x000E8B74
	public static string PlayerRecieveMortar
	{
		get
		{
			return Language.sL.PlayerRecieveMortar;
		}
	}

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000EA980 File Offset: 0x000E8B80
	public static string PlayerPlacedMarker
	{
		get
		{
			return Language.sL.PlayerPlacedMarker;
		}
	}

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000EA98C File Offset: 0x000E8B8C
	public static string PlayerDifuseMarker
	{
		get
		{
			return Language.sL.PlayerDifuseMarker;
		}
	}

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x060015F3 RID: 5619 RVA: 0x000EA998 File Offset: 0x000E8B98
	public static string PlayerMakeStormKill
	{
		get
		{
			return Language.sL.PlayerMakeStormKill;
		}
	}

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000EA9A4 File Offset: 0x000E8BA4
	public static string PlayerMakeProKill
	{
		get
		{
			return Language.sL.PlayerMakeProKill;
		}
	}

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000EA9B0 File Offset: 0x000E8BB0
	public static string PlayerMakeLegendaryKill
	{
		get
		{
			return Language.sL.PlayerMakeLegendaryKill;
		}
	}

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x060015F6 RID: 5622 RVA: 0x000EA9BC File Offset: 0x000E8BBC
	public static string PlayerGainedAchivment
	{
		get
		{
			return Language.sL.PlayerGainedAchivment;
		}
	}

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x060015F7 RID: 5623 RVA: 0x000EA9C8 File Offset: 0x000E8BC8
	public static string PlayerCapturedPoint
	{
		get
		{
			return Language.sL.PlayerCapturedPoint;
		}
	}

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000EA9D4 File Offset: 0x000E8BD4
	public static string AutoTeamBalance
	{
		get
		{
			return Language.sL.AutoTeamBalance;
		}
	}

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x060015F9 RID: 5625 RVA: 0x000EA9E0 File Offset: 0x000E8BE0
	public static string ServerRestarting
	{
		get
		{
			return Language.sL.ServerRestarting;
		}
	}

	// Token: 0x17000398 RID: 920
	// (get) Token: 0x060015FA RID: 5626 RVA: 0x000EA9EC File Offset: 0x000E8BEC
	public static string PlayerConnected
	{
		get
		{
			return Language.sL.PlayerConnected;
		}
	}

	// Token: 0x17000399 RID: 921
	// (get) Token: 0x060015FB RID: 5627 RVA: 0x000EA9F8 File Offset: 0x000E8BF8
	public static string PlayerDisconnected
	{
		get
		{
			return Language.sL.PlayerDisconnected;
		}
	}

	// Token: 0x1700039A RID: 922
	// (get) Token: 0x060015FC RID: 5628 RVA: 0x000EAA04 File Offset: 0x000E8C04
	public static string SpecChangeCam_Spacebar
	{
		get
		{
			return Language.sL.SpecChangeCam_Spacebar;
		}
	}

	// Token: 0x1700039B RID: 923
	// (get) Token: 0x060015FD RID: 5629 RVA: 0x000EAA10 File Offset: 0x000E8C10
	public static string SpecChangeCam_MidleButton
	{
		get
		{
			return Language.sL.SpecChangeCam_MidleButton;
		}
	}

	// Token: 0x1700039C RID: 924
	// (get) Token: 0x060015FE RID: 5630 RVA: 0x000EAA1C File Offset: 0x000E8C1C
	public static string SpecPressMToSelectTeam
	{
		get
		{
			return Language.sL.SpecPressMToSelectTeam;
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x060015FF RID: 5631 RVA: 0x000EAA28 File Offset: 0x000E8C28
	public static string SpecViewaAt
	{
		get
		{
			return Language.sL.SpecViewaAt;
		}
	}

	// Token: 0x1700039E RID: 926
	// (get) Token: 0x06001600 RID: 5632 RVA: 0x000EAA34 File Offset: 0x000E8C34
	public static string SpecChooseTeam
	{
		get
		{
			return Language.sL.SpecChooseTeam;
		}
	}

	// Token: 0x1700039F RID: 927
	// (get) Token: 0x06001601 RID: 5633 RVA: 0x000EAA40 File Offset: 0x000E8C40
	public static string SpecPremium
	{
		get
		{
			return Language.sL.SpecPremium;
		}
	}

	// Token: 0x170003A0 RID: 928
	// (get) Token: 0x06001602 RID: 5634 RVA: 0x000EAA4C File Offset: 0x000E8C4C
	public static string SpecGamers
	{
		get
		{
			return Language.sL.SpecGamers;
		}
	}

	// Token: 0x170003A1 RID: 929
	// (get) Token: 0x06001603 RID: 5635 RVA: 0x000EAA58 File Offset: 0x000E8C58
	public static string SpecSpectator
	{
		get
		{
			return Language.sL.SpecSpectator;
		}
	}

	// Token: 0x170003A2 RID: 930
	// (get) Token: 0x06001604 RID: 5636 RVA: 0x000EAA64 File Offset: 0x000E8C64
	public static string SpecAutobalance
	{
		get
		{
			return Language.sL.SpecAutobalance;
		}
	}

	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06001605 RID: 5637 RVA: 0x000EAA70 File Offset: 0x000E8C70
	public static string SpecBEARWins
	{
		get
		{
			return Language.sL.SpecBEARWins;
		}
	}

	// Token: 0x170003A4 RID: 932
	// (get) Token: 0x06001606 RID: 5638 RVA: 0x000EAA7C File Offset: 0x000E8C7C
	public static string SpecUSECWins
	{
		get
		{
			return Language.sL.SpecUSECWins;
		}
	}

	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x06001607 RID: 5639 RVA: 0x000EAA88 File Offset: 0x000E8C88
	public static string SpecWin
	{
		get
		{
			return Language.sL.SpecWin;
		}
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001608 RID: 5640 RVA: 0x000EAA94 File Offset: 0x000E8C94
	public static string SpecLoose
	{
		get
		{
			return Language.sL.SpecLoose;
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001609 RID: 5641 RVA: 0x000EAAA0 File Offset: 0x000E8CA0
	public static string No
	{
		get
		{
			return Language.sL.No;
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x0600160A RID: 5642 RVA: 0x000EAAAC File Offset: 0x000E8CAC
	public static string LoadingProfile
	{
		get
		{
			return Language.sL.LoadingProfile;
		}
	}

	// Token: 0x170003A9 RID: 937
	// (get) Token: 0x0600160B RID: 5643 RVA: 0x000EAAB8 File Offset: 0x000E8CB8
	public static string LoadingBuild
	{
		get
		{
			return Language.sL.LoadingBuild;
		}
	}

	// Token: 0x170003AA RID: 938
	// (get) Token: 0x0600160C RID: 5644 RVA: 0x000EAAC4 File Offset: 0x000E8CC4
	public static string LoadingProfileFailed
	{
		get
		{
			return Language.sL.LoadingProfileFailed;
		}
	}

	// Token: 0x170003AB RID: 939
	// (get) Token: 0x0600160D RID: 5645 RVA: 0x000EAAD0 File Offset: 0x000E8CD0
	public static string LoadingProfileCheckConnection
	{
		get
		{
			return Language.sL.LoadingProfileCheckConnection;
		}
	}

	// Token: 0x170003AC RID: 940
	// (get) Token: 0x0600160E RID: 5646 RVA: 0x000EAADC File Offset: 0x000E8CDC
	public static string LoadingProfileCheckSoftware
	{
		get
		{
			return Language.sL.LoadingProfileCheckSoftware;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x0600160F RID: 5647 RVA: 0x000EAAE8 File Offset: 0x000E8CE8
	public static string LoadingProfileReloadApplication
	{
		get
		{
			return Language.sL.LoadingProfileReloadApplication;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x06001610 RID: 5648 RVA: 0x000EAAF4 File Offset: 0x000E8CF4
	public static string PlayerAutoBallanced
	{
		get
		{
			return Language.sL.PlayerAutoBallanced;
		}
	}

	// Token: 0x170003AF RID: 943
	// (get) Token: 0x06001611 RID: 5649 RVA: 0x000EAB00 File Offset: 0x000E8D00
	public static string killedVIP
	{
		get
		{
			return Language.sL.killedVIP;
		}
	}

	// Token: 0x170003B0 RID: 944
	// (get) Token: 0x06001612 RID: 5650 RVA: 0x000EAB0C File Offset: 0x000E8D0C
	public static string VIPInYourTeam
	{
		get
		{
			return Language.sL.VIPInYourTeam;
		}
	}

	// Token: 0x170003B1 RID: 945
	// (get) Token: 0x06001613 RID: 5651 RVA: 0x000EAB18 File Offset: 0x000E8D18
	public static string VIPInEnemyTeam
	{
		get
		{
			return Language.sL.VIPInEnemyTeam;
		}
	}

	// Token: 0x170003B2 RID: 946
	// (get) Token: 0x06001614 RID: 5652 RVA: 0x000EAB24 File Offset: 0x000E8D24
	public static string[,] GameModeDescrCutted
	{
		get
		{
			return Language.sL.GameModeDescrCutted;
		}
	}

	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x06001615 RID: 5653 RVA: 0x000EAB30 File Offset: 0x000E8D30
	public static string[] QuickGameDescrCutted
	{
		get
		{
			return Language.sL.QuickGameDescrCutted;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x06001616 RID: 5654 RVA: 0x000EAB3C File Offset: 0x000E8D3C
	public static string PlayerQuit
	{
		get
		{
			if (Language.sL != null)
			{
				return Language.sL.PlayerQuit;
			}
			return string.Empty;
		}
	}

	// Token: 0x170003B5 RID: 949
	// (get) Token: 0x06001617 RID: 5655 RVA: 0x000EAB58 File Offset: 0x000E8D58
	public static string[] HallOfFameHeader
	{
		get
		{
			return Language.sL.HallOfFameHeader;
		}
	}

	// Token: 0x170003B6 RID: 950
	// (get) Token: 0x06001618 RID: 5656 RVA: 0x000EAB64 File Offset: 0x000E8D64
	public static string StartGame
	{
		get
		{
			return Language.sL.StartGame;
		}
	}

	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x06001619 RID: 5657 RVA: 0x000EAB70 File Offset: 0x000E8D70
	public static string anyMale
	{
		get
		{
			return Language.sL.anyMale;
		}
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x0600161A RID: 5658 RVA: 0x000EAB7C File Offset: 0x000E8D7C
	public static string anyFemale
	{
		get
		{
			return Language.sL.anyFemale;
		}
	}

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x0600161B RID: 5659 RVA: 0x000EAB88 File Offset: 0x000E8D88
	public static string Completed
	{
		get
		{
			return Language.sL.Completed;
		}
	}

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x0600161C RID: 5660 RVA: 0x000EAB94 File Offset: 0x000E8D94
	public static string magazPo
	{
		get
		{
			return Language.sL.magazPo;
		}
	}

	// Token: 0x170003BB RID: 955
	// (get) Token: 0x0600161D RID: 5661 RVA: 0x000EABA0 File Offset: 0x000E8DA0
	public static string patr
	{
		get
		{
			return Language.sL.patr;
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x0600161E RID: 5662 RVA: 0x000EABAC File Offset: 0x000E8DAC
	public static string ReturnToTheGame
	{
		get
		{
			return Language.sL.ReturnToTheGame;
		}
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x0600161F RID: 5663 RVA: 0x000EABB8 File Offset: 0x000E8DB8
	public static string ExitFromTheServer
	{
		get
		{
			return Language.sL.ExitFromTheServer;
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x06001620 RID: 5664 RVA: 0x000EABC4 File Offset: 0x000E8DC4
	public static string QuickPlay
	{
		get
		{
			return Language.sL.QuickPlay;
		}
	}

	// Token: 0x170003BF RID: 959
	// (get) Token: 0x06001621 RID: 5665 RVA: 0x000EABD0 File Offset: 0x000E8DD0
	public static string SearchGames
	{
		get
		{
			return Language.sL.SearchGames;
		}
	}

	// Token: 0x170003C0 RID: 960
	// (get) Token: 0x06001622 RID: 5666 RVA: 0x000EABDC File Offset: 0x000E8DDC
	public static string Settings
	{
		get
		{
			return Language.sL.Settings;
		}
	}

	// Token: 0x170003C1 RID: 961
	// (get) Token: 0x06001623 RID: 5667 RVA: 0x000EABE8 File Offset: 0x000E8DE8
	public static string Career
	{
		get
		{
			return Language.sL.Career;
		}
	}

	// Token: 0x170003C2 RID: 962
	// (get) Token: 0x06001624 RID: 5668 RVA: 0x000EABF4 File Offset: 0x000E8DF4
	public static string Help
	{
		get
		{
			return Language.sL.Help;
		}
	}

	// Token: 0x170003C3 RID: 963
	// (get) Token: 0x06001625 RID: 5669 RVA: 0x000EAC00 File Offset: 0x000E8E00
	public static string GatherTheTeam
	{
		get
		{
			return Language.sL.GatherTheTeam;
		}
	}

	// Token: 0x170003C4 RID: 964
	// (get) Token: 0x06001626 RID: 5670 RVA: 0x000EAC0C File Offset: 0x000E8E0C
	public static string GetGoldPoints
	{
		get
		{
			return Language.sL.GetGoldPoints;
		}
	}

	// Token: 0x170003C5 RID: 965
	// (get) Token: 0x06001627 RID: 5671 RVA: 0x000EAC18 File Offset: 0x000E8E18
	public static string GetGpNow
	{
		get
		{
			return Language.sL.GetGpNow;
		}
	}

	// Token: 0x170003C6 RID: 966
	// (get) Token: 0x06001628 RID: 5672 RVA: 0x000EAC24 File Offset: 0x000E8E24
	public static string FriendsInvited
	{
		get
		{
			return Language.sL.FriendsInvited;
		}
	}

	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x06001629 RID: 5673 RVA: 0x000EAC30 File Offset: 0x000E8E30
	public static string ThePurchaseOfModification
	{
		get
		{
			return Language.sL.ThePurchaseOfModification;
		}
	}

	// Token: 0x170003C8 RID: 968
	// (get) Token: 0x0600162A RID: 5674 RVA: 0x000EAC3C File Offset: 0x000E8E3C
	public static string ProgressDailyContracts
	{
		get
		{
			return Language.sL.ProgressDailyContracts;
		}
	}

	// Token: 0x170003C9 RID: 969
	// (get) Token: 0x0600162B RID: 5675 RVA: 0x000EAC48 File Offset: 0x000E8E48
	public static string SetWeapons
	{
		get
		{
			return Language.sL.SetWeapons;
		}
	}

	// Token: 0x170003CA RID: 970
	// (get) Token: 0x0600162C RID: 5676 RVA: 0x000EAC54 File Offset: 0x000E8E54
	public static string LockedRequired
	{
		get
		{
			return Language.sL.LockedRequired;
		}
	}

	// Token: 0x170003CB RID: 971
	// (get) Token: 0x0600162D RID: 5677 RVA: 0x000EAC60 File Offset: 0x000E8E60
	public static string Level
	{
		get
		{
			return Language.sL.Level;
		}
	}

	// Token: 0x170003CC RID: 972
	// (get) Token: 0x0600162E RID: 5678 RVA: 0x000EAC6C File Offset: 0x000E8E6C
	public static string EarlyAccessCaps
	{
		get
		{
			return Language.sL.EarlyAccessCaps;
		}
	}

	// Token: 0x170003CD RID: 973
	// (get) Token: 0x0600162F RID: 5679 RVA: 0x000EAC78 File Offset: 0x000E8E78
	public static string EarlyAccess
	{
		get
		{
			return Language.sL.EarlyAccess;
		}
	}

	// Token: 0x170003CE RID: 974
	// (get) Token: 0x06001630 RID: 5680 RVA: 0x000EAC84 File Offset: 0x000E8E84
	public static string TextToOpenLastSet
	{
		get
		{
			return Language.sL.TextToOpenLastSet;
		}
	}

	// Token: 0x170003CF RID: 975
	// (get) Token: 0x06001631 RID: 5681 RVA: 0x000EAC90 File Offset: 0x000E8E90
	public static string UnlockingTheSet
	{
		get
		{
			return Language.sL.UnlockingTheSet;
		}
	}

	// Token: 0x170003D0 RID: 976
	// (get) Token: 0x06001632 RID: 5682 RVA: 0x000EAC9C File Offset: 0x000E8E9C
	public static string ProfileNotLoaded
	{
		get
		{
			return Language.sL.ProfileNotLoaded;
		}
	}

	// Token: 0x170003D1 RID: 977
	// (get) Token: 0x06001633 RID: 5683 RVA: 0x000EACA8 File Offset: 0x000E8EA8
	public static string FillUpBalance
	{
		get
		{
			return Language.sL.FillUpBalance;
		}
	}

	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06001634 RID: 5684 RVA: 0x000EACB4 File Offset: 0x000E8EB4
	public static string Set
	{
		get
		{
			return Language.sL.Set;
		}
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06001635 RID: 5685 RVA: 0x000EACC0 File Offset: 0x000E8EC0
	public static string Votes
	{
		get
		{
			return Language.sL.Votes;
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06001636 RID: 5686 RVA: 0x000EACCC File Offset: 0x000E8ECC
	public static string SetOfEquipment
	{
		get
		{
			return Language.sL.SetOfEquipment;
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x06001637 RID: 5687 RVA: 0x000EACD8 File Offset: 0x000E8ED8
	public static string SelectedSet
	{
		get
		{
			return Language.sL.SelectedSet;
		}
	}

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x06001638 RID: 5688 RVA: 0x000EACE4 File Offset: 0x000E8EE4
	public static string CharacterCamouflage
	{
		get
		{
			return Language.sL.CharacterCamouflage;
		}
	}

	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06001639 RID: 5689 RVA: 0x000EACF0 File Offset: 0x000E8EF0
	public static string Select
	{
		get
		{
			return Language.sL.Select;
		}
	}

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x0600163A RID: 5690 RVA: 0x000EACFC File Offset: 0x000E8EFC
	public static string CamouflageSelection
	{
		get
		{
			return Language.sL.CamouflageSelection;
		}
	}

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x0600163B RID: 5691 RVA: 0x000EAD08 File Offset: 0x000E8F08
	public static string BonusString0
	{
		get
		{
			return Language.sL.BonusString0;
		}
	}

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x0600163C RID: 5692 RVA: 0x000EAD14 File Offset: 0x000E8F14
	public static string BonusString3
	{
		get
		{
			return Language.sL.BonusString3;
		}
	}

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x0600163D RID: 5693 RVA: 0x000EAD20 File Offset: 0x000E8F20
	public static string BonusString10
	{
		get
		{
			return Language.sL.BonusString10;
		}
	}

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x0600163E RID: 5694 RVA: 0x000EAD2C File Offset: 0x000E8F2C
	public static string BonusString20
	{
		get
		{
			return Language.sL.BonusString20;
		}
	}

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x0600163F RID: 5695 RVA: 0x000EAD38 File Offset: 0x000E8F38
	public static string BonusString60
	{
		get
		{
			return Language.sL.BonusString60;
		}
	}

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06001640 RID: 5696 RVA: 0x000EAD44 File Offset: 0x000E8F44
	public static string BonusString61
	{
		get
		{
			return Language.sL.BonusString61;
		}
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06001641 RID: 5697 RVA: 0x000EAD50 File Offset: 0x000E8F50
	public static string BonusString62
	{
		get
		{
			return Language.sL.BonusString62;
		}
	}

	// Token: 0x170003E0 RID: 992
	// (get) Token: 0x06001642 RID: 5698 RVA: 0x000EAD5C File Offset: 0x000E8F5C
	public static string BonusString63
	{
		get
		{
			return Language.sL.BonusString63;
		}
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x06001643 RID: 5699 RVA: 0x000EAD68 File Offset: 0x000E8F68
	public static string BonusString64
	{
		get
		{
			return Language.sL.BonusString64;
		}
	}

	// Token: 0x170003E2 RID: 994
	// (get) Token: 0x06001644 RID: 5700 RVA: 0x000EAD74 File Offset: 0x000E8F74
	public static string BonusString65
	{
		get
		{
			return Language.sL.BonusString65;
		}
	}

	// Token: 0x170003E3 RID: 995
	// (get) Token: 0x06001645 RID: 5701 RVA: 0x000EAD80 File Offset: 0x000E8F80
	public static string BonusString66
	{
		get
		{
			return Language.sL.BonusString66;
		}
	}

	// Token: 0x170003E4 RID: 996
	// (get) Token: 0x06001646 RID: 5702 RVA: 0x000EAD8C File Offset: 0x000E8F8C
	public static string BonusString67
	{
		get
		{
			return Language.sL.BonusString67;
		}
	}

	// Token: 0x170003E5 RID: 997
	// (get) Token: 0x06001647 RID: 5703 RVA: 0x000EAD98 File Offset: 0x000E8F98
	public static string BonusString68
	{
		get
		{
			return Language.sL.BonusString68;
		}
	}

	// Token: 0x170003E6 RID: 998
	// (get) Token: 0x06001648 RID: 5704 RVA: 0x000EADA4 File Offset: 0x000E8FA4
	public static string BonusString69
	{
		get
		{
			return Language.sL.BonusString69;
		}
	}

	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001649 RID: 5705 RVA: 0x000EADB0 File Offset: 0x000E8FB0
	public static string BonusString70
	{
		get
		{
			return Language.sL.BonusString70;
		}
	}

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x0600164A RID: 5706 RVA: 0x000EADBC File Offset: 0x000E8FBC
	public static string TheTerm
	{
		get
		{
			return Language.sL.TheTerm;
		}
	}

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x0600164B RID: 5707 RVA: 0x000EADC8 File Offset: 0x000E8FC8
	public static string Day
	{
		get
		{
			return Language.sL.Day;
		}
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x0600164C RID: 5708 RVA: 0x000EADD4 File Offset: 0x000E8FD4
	public static string Days_dnya
	{
		get
		{
			return Language.sL.Days_dnya;
		}
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x0600164D RID: 5709 RVA: 0x000EADE0 File Offset: 0x000E8FE0
	public static string Days_dney
	{
		get
		{
			return Language.sL.Days_dney;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x0600164E RID: 5710 RVA: 0x000EADEC File Offset: 0x000E8FEC
	public static string D
	{
		get
		{
			return Language.sL.D;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x0600164F RID: 5711 RVA: 0x000EADF8 File Offset: 0x000E8FF8
	public static string H
	{
		get
		{
			return Language.sL.H;
		}
	}

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x06001650 RID: 5712 RVA: 0x000EAE04 File Offset: 0x000E9004
	public static string M
	{
		get
		{
			return Language.sL.M;
		}
	}

	// Token: 0x170003EF RID: 1007
	// (get) Token: 0x06001651 RID: 5713 RVA: 0x000EAE10 File Offset: 0x000E9010
	public static string S
	{
		get
		{
			return Language.sL.S;
		}
	}

	// Token: 0x170003F0 RID: 1008
	// (get) Token: 0x06001652 RID: 5714 RVA: 0x000EAE1C File Offset: 0x000E901C
	public static string Reason
	{
		get
		{
			return Language.sL.Reason;
		}
	}

	// Token: 0x170003F1 RID: 1009
	// (get) Token: 0x06001653 RID: 5715 RVA: 0x000EAE28 File Offset: 0x000E9028
	public static string FriendText
	{
		get
		{
			return Language.sL.FriendText;
		}
	}

	// Token: 0x170003F2 RID: 1010
	// (get) Token: 0x06001654 RID: 5716 RVA: 0x000EAE34 File Offset: 0x000E9034
	public static string FollowByLink
	{
		get
		{
			return Language.sL.FollowByLink;
		}
	}

	// Token: 0x170003F3 RID: 1011
	// (get) Token: 0x06001655 RID: 5717 RVA: 0x000EAE40 File Offset: 0x000E9040
	public static string InOtherNews
	{
		get
		{
			return Language.sL.InOtherNews;
		}
	}

	// Token: 0x170003F4 RID: 1012
	// (get) Token: 0x06001656 RID: 5718 RVA: 0x000EAE4C File Offset: 0x000E904C
	public static string TextUnlockKit
	{
		get
		{
			return Language.sL.TextUnlockKit;
		}
	}

	// Token: 0x170003F5 RID: 1013
	// (get) Token: 0x06001657 RID: 5719 RVA: 0x000EAE58 File Offset: 0x000E9058
	public static string UnlockFor
	{
		get
		{
			return Language.sL.UnlockFor;
		}
	}

	// Token: 0x170003F6 RID: 1014
	// (get) Token: 0x06001658 RID: 5720 RVA: 0x000EAE64 File Offset: 0x000E9064
	public static string InsufficientFunds
	{
		get
		{
			return Language.sL.InsufficientFunds;
		}
	}

	// Token: 0x170003F7 RID: 1015
	// (get) Token: 0x06001659 RID: 5721 RVA: 0x000EAE70 File Offset: 0x000E9070
	public static string TextUnlockSet
	{
		get
		{
			return Language.sL.TextUnlockSet;
		}
	}

	// Token: 0x170003F8 RID: 1016
	// (get) Token: 0x0600165A RID: 5722 RVA: 0x000EAE7C File Offset: 0x000E907C
	public static string InsufficientFundsNeed
	{
		get
		{
			return Language.sL.InsufficientFundsNeed;
		}
	}

	// Token: 0x170003F9 RID: 1017
	// (get) Token: 0x0600165B RID: 5723 RVA: 0x000EAE88 File Offset: 0x000E9088
	public static string TextUnlockWTask
	{
		get
		{
			return Language.sL.TextUnlockWTask;
		}
	}

	// Token: 0x170003FA RID: 1018
	// (get) Token: 0x0600165C RID: 5724 RVA: 0x000EAE94 File Offset: 0x000E9094
	public static string BuyModFor
	{
		get
		{
			return Language.sL.BuyModFor;
		}
	}

	// Token: 0x170003FB RID: 1019
	// (get) Token: 0x0600165D RID: 5725 RVA: 0x000EAEA0 File Offset: 0x000E90A0
	public static string RentSkill
	{
		get
		{
			return Language.sL.RentSkill;
		}
	}

	// Token: 0x170003FC RID: 1020
	// (get) Token: 0x0600165E RID: 5726 RVA: 0x000EAEAC File Offset: 0x000E90AC
	public static string Price
	{
		get
		{
			return Language.sL.Price;
		}
	}

	// Token: 0x170003FD RID: 1021
	// (get) Token: 0x0600165F RID: 5727 RVA: 0x000EAEB8 File Offset: 0x000E90B8
	public static string Cost
	{
		get
		{
			return Language.sL.Cost;
		}
	}

	// Token: 0x170003FE RID: 1022
	// (get) Token: 0x06001660 RID: 5728 RVA: 0x000EAEC4 File Offset: 0x000E90C4
	public static string UnlockQuestion
	{
		get
		{
			return Language.sL.UnlockQuestion;
		}
	}

	// Token: 0x170003FF RID: 1023
	// (get) Token: 0x06001661 RID: 5729 RVA: 0x000EAED0 File Offset: 0x000E90D0
	public static string Yes
	{
		get
		{
			return Language.sL.Yes;
		}
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x06001662 RID: 5730 RVA: 0x000EAEDC File Offset: 0x000E90DC
	public static string TextResetSkills
	{
		get
		{
			return Language.sL.TextResetSkills;
		}
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x06001663 RID: 5731 RVA: 0x000EAEE8 File Offset: 0x000E90E8
	public static string ResetSkillYouGet
	{
		get
		{
			return Language.sL.ResetSkillYouGet;
		}
	}

	// Token: 0x17000402 RID: 1026
	// (get) Token: 0x06001664 RID: 5732 RVA: 0x000EAEF4 File Offset: 0x000E90F4
	public static string ResetSkillsFor
	{
		get
		{
			return Language.sL.ResetSkillsFor;
		}
	}

	// Token: 0x17000403 RID: 1027
	// (get) Token: 0x06001665 RID: 5733 RVA: 0x000EAF00 File Offset: 0x000E9100
	public static string Or
	{
		get
		{
			return Language.sL.Or;
		}
	}

	// Token: 0x17000404 RID: 1028
	// (get) Token: 0x06001666 RID: 5734 RVA: 0x000EAF0C File Offset: 0x000E910C
	public static string ResetSkillAttention
	{
		get
		{
			return Language.sL.ResetSkillAttention;
		}
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x06001667 RID: 5735 RVA: 0x000EAF18 File Offset: 0x000E9118
	public static string EnterPassword
	{
		get
		{
			return Language.sL.EnterPassword;
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x06001668 RID: 5736 RVA: 0x000EAF24 File Offset: 0x000E9124
	public static string OK
	{
		get
		{
			return Language.sL.OK;
		}
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x06001669 RID: 5737 RVA: 0x000EAF30 File Offset: 0x000E9130
	public static string PromoHello
	{
		get
		{
			return Language.sL.PromoHello;
		}
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x0600166A RID: 5738 RVA: 0x000EAF3C File Offset: 0x000E913C
	public static string PromoHelloForResourse0
	{
		get
		{
			return Language.sL.PromoHelloForResourse0;
		}
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x0600166B RID: 5739 RVA: 0x000EAF48 File Offset: 0x000E9148
	public static string PromoHelloForResourse1
	{
		get
		{
			return Language.sL.PromoHelloForResourse1;
		}
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x0600166C RID: 5740 RVA: 0x000EAF54 File Offset: 0x000E9154
	public static string PromoHelloForResourse2
	{
		get
		{
			return Language.sL.PromoHelloForResourse2;
		}
	}

	// Token: 0x1700040B RID: 1035
	// (get) Token: 0x0600166D RID: 5741 RVA: 0x000EAF60 File Offset: 0x000E9160
	public static string HundredsOfOil
	{
		get
		{
			return Language.sL.HundredsOfOil;
		}
	}

	// Token: 0x1700040C RID: 1036
	// (get) Token: 0x0600166E RID: 5742 RVA: 0x000EAF6C File Offset: 0x000E916C
	public static string Forever
	{
		get
		{
			return Language.sL.Forever;
		}
	}

	// Token: 0x1700040D RID: 1037
	// (get) Token: 0x0600166F RID: 5743 RVA: 0x000EAF78 File Offset: 0x000E9178
	public static string WellcomeToCWandGetBonus
	{
		get
		{
			return Language.sL.WellcomeToCWandGetBonus;
		}
	}

	// Token: 0x1700040E RID: 1038
	// (get) Token: 0x06001670 RID: 5744 RVA: 0x000EAF84 File Offset: 0x000E9184
	public static string CRGPSPEveryDay
	{
		get
		{
			return Language.sL.CRGPSPEveryDay;
		}
	}

	// Token: 0x1700040F RID: 1039
	// (get) Token: 0x06001671 RID: 5745 RVA: 0x000EAF90 File Offset: 0x000E9190
	public static string Map
	{
		get
		{
			return Language.sL.Map;
		}
	}

	// Token: 0x17000410 RID: 1040
	// (get) Token: 0x06001672 RID: 5746 RVA: 0x000EAF9C File Offset: 0x000E919C
	public static string BuySPAttention
	{
		get
		{
			return Language.sL.BuySPAttention;
		}
	}

	// Token: 0x17000411 RID: 1041
	// (get) Token: 0x06001673 RID: 5747 RVA: 0x000EAFA8 File Offset: 0x000E91A8
	public static string BuySPFor
	{
		get
		{
			return Language.sL.BuySPFor;
		}
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06001674 RID: 5748 RVA: 0x000EAFB4 File Offset: 0x000E91B4
	public static string Mode
	{
		get
		{
			return Language.sL.Mode;
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x06001675 RID: 5749 RVA: 0x000EAFC0 File Offset: 0x000E91C0
	public static string GoToTheBattle
	{
		get
		{
			return Language.sL.GoToTheBattle;
		}
	}

	// Token: 0x17000414 RID: 1044
	// (get) Token: 0x06001676 RID: 5750 RVA: 0x000EAFCC File Offset: 0x000E91CC
	public static string GamesNotFound
	{
		get
		{
			return Language.sL.GamesNotFound;
		}
	}

	// Token: 0x17000415 RID: 1045
	// (get) Token: 0x06001677 RID: 5751 RVA: 0x000EAFD8 File Offset: 0x000E91D8
	public static string tventyMinutes
	{
		get
		{
			return Language.sL.tventyMinutes;
		}
	}

	// Token: 0x17000416 RID: 1046
	// (get) Token: 0x06001678 RID: 5752 RVA: 0x000EAFE4 File Offset: 0x000E91E4
	public static string discount
	{
		get
		{
			return Language.sL.discount;
		}
	}

	// Token: 0x17000417 RID: 1047
	// (get) Token: 0x06001679 RID: 5753 RVA: 0x000EAFF0 File Offset: 0x000E91F0
	public static string discountGreeting2
	{
		get
		{
			return Language.sL.discountGreeteng2;
		}
	}

	// Token: 0x17000418 RID: 1048
	// (get) Token: 0x0600167A RID: 5754 RVA: 0x000EAFFC File Offset: 0x000E91FC
	public static string NoSeats
	{
		get
		{
			return Language.sL.NoSeats;
		}
	}

	// Token: 0x17000419 RID: 1049
	// (get) Token: 0x0600167B RID: 5755 RVA: 0x000EB008 File Offset: 0x000E9208
	public static string Later
	{
		get
		{
			return Language.sL.Later;
		}
	}

	// Token: 0x1700041A RID: 1050
	// (get) Token: 0x0600167C RID: 5756 RVA: 0x000EB014 File Offset: 0x000E9214
	public static string JoinFight
	{
		get
		{
			return Language.sL.JoinFight;
		}
	}

	// Token: 0x1700041B RID: 1051
	// (get) Token: 0x0600167D RID: 5757 RVA: 0x000EB020 File Offset: 0x000E9220
	public static string AchievementWillNotCount
	{
		get
		{
			return Language.sL.AchievementWillNotCount;
		}
	}

	// Token: 0x1700041C RID: 1052
	// (get) Token: 0x0600167E RID: 5758 RVA: 0x000EB02C File Offset: 0x000E922C
	public static string DMHeader
	{
		get
		{
			return Language.sL.DMHeader;
		}
	}

	// Token: 0x1700041D RID: 1053
	// (get) Token: 0x0600167F RID: 5759 RVA: 0x000EB038 File Offset: 0x000E9238
	public static string DMDescription
	{
		get
		{
			return Language.sL.DMDescription;
		}
	}

	// Token: 0x1700041E RID: 1054
	// (get) Token: 0x06001680 RID: 5760 RVA: 0x000EB044 File Offset: 0x000E9244
	public static string VIPHeader
	{
		get
		{
			return Language.sL.VIPHeader;
		}
	}

	// Token: 0x1700041F RID: 1055
	// (get) Token: 0x06001681 RID: 5761 RVA: 0x000EB050 File Offset: 0x000E9250
	public static string VIPDescription
	{
		get
		{
			return Language.sL.VIPDescription;
		}
	}

	// Token: 0x17000420 RID: 1056
	// (get) Token: 0x06001682 RID: 5762 RVA: 0x000EB05C File Offset: 0x000E925C
	public static string TDSHeader
	{
		get
		{
			return Language.sL.TDSHeader;
		}
	}

	// Token: 0x17000421 RID: 1057
	// (get) Token: 0x06001683 RID: 5763 RVA: 0x000EB068 File Offset: 0x000E9268
	public static string TDSDescriptionB
	{
		get
		{
			return Language.sL.TDSDescriptionB;
		}
	}

	// Token: 0x17000422 RID: 1058
	// (get) Token: 0x06001684 RID: 5764 RVA: 0x000EB074 File Offset: 0x000E9274
	public static string TDSDescriptionU
	{
		get
		{
			return Language.sL.TDSDescriptionU;
		}
	}

	// Token: 0x17000423 RID: 1059
	// (get) Token: 0x06001685 RID: 5765 RVA: 0x000EB080 File Offset: 0x000E9280
	public static string TEHeader
	{
		get
		{
			return Language.sL.TEHeader;
		}
	}

	// Token: 0x17000424 RID: 1060
	// (get) Token: 0x06001686 RID: 5766 RVA: 0x000EB08C File Offset: 0x000E928C
	public static string TEDescription
	{
		get
		{
			return Language.sL.TEDescription;
		}
	}

	// Token: 0x17000425 RID: 1061
	// (get) Token: 0x06001687 RID: 5767 RVA: 0x000EB098 File Offset: 0x000E9298
	public static string TCHeader
	{
		get
		{
			return Language.sL.TCHeader;
		}
	}

	// Token: 0x17000426 RID: 1062
	// (get) Token: 0x06001688 RID: 5768 RVA: 0x000EB0A4 File Offset: 0x000E92A4
	public static string TCDescription
	{
		get
		{
			return Language.sL.TCDescription;
		}
	}

	// Token: 0x17000427 RID: 1063
	// (get) Token: 0x06001689 RID: 5769 RVA: 0x000EB0B0 File Offset: 0x000E92B0
	public static string YouKill
	{
		get
		{
			return Language.sL.YouKill;
		}
	}

	// Token: 0x17000428 RID: 1064
	// (get) Token: 0x0600168A RID: 5770 RVA: 0x000EB0BC File Offset: 0x000E92BC
	public static string YouKilled
	{
		get
		{
			return Language.sL.YouKilled;
		}
	}

	// Token: 0x17000429 RID: 1065
	// (get) Token: 0x0600168B RID: 5771 RVA: 0x000EB0C8 File Offset: 0x000E92C8
	public static string WTaskProgress
	{
		get
		{
			return Language.sL.WTaskProgress;
		}
	}

	// Token: 0x1700042A RID: 1066
	// (get) Token: 0x0600168C RID: 5772 RVA: 0x000EB0D4 File Offset: 0x000E92D4
	public static string ProgressTowards
	{
		get
		{
			return Language.sL.ProgressTowards;
		}
	}

	// Token: 0x1700042B RID: 1067
	// (get) Token: 0x0600168D RID: 5773 RVA: 0x000EB0E0 File Offset: 0x000E92E0
	public static string CallForSupport
	{
		get
		{
			return Language.sL.CallForSupport;
		}
	}

	// Token: 0x1700042C RID: 1068
	// (get) Token: 0x0600168E RID: 5774 RVA: 0x000EB0EC File Offset: 0x000E92EC
	public static string BeginInstallingTheBeacon
	{
		get
		{
			return Language.sL.BeginInstallingTheBeacon;
		}
	}

	// Token: 0x1700042D RID: 1069
	// (get) Token: 0x0600168F RID: 5775 RVA: 0x000EB0F8 File Offset: 0x000E92F8
	public static string ClearanceToStartBeacon
	{
		get
		{
			return Language.sL.ClearanceToStartBeacon;
		}
	}

	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06001690 RID: 5776 RVA: 0x000EB104 File Offset: 0x000E9304
	public static string CompletedSmall
	{
		get
		{
			return Language.sL.CompletedSmall;
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06001691 RID: 5777 RVA: 0x000EB110 File Offset: 0x000E9310
	public static string SpecServerIsFull
	{
		get
		{
			return Language.sL.SpecServerIsFull;
		}
	}

	// Token: 0x17000430 RID: 1072
	// (get) Token: 0x06001692 RID: 5778 RVA: 0x000EB11C File Offset: 0x000E931C
	public static string SpecRessurectAfter
	{
		get
		{
			return Language.sL.SpecRessurectAfter;
		}
	}

	// Token: 0x17000431 RID: 1073
	// (get) Token: 0x06001693 RID: 5779 RVA: 0x000EB128 File Offset: 0x000E9328
	public static string Space
	{
		get
		{
			return Language.sL.Space;
		}
	}

	// Token: 0x17000432 RID: 1074
	// (get) Token: 0x06001694 RID: 5780 RVA: 0x000EB134 File Offset: 0x000E9334
	public static string SpecForCycleCamChanged
	{
		get
		{
			return Language.sL.SpecForCycleCamChanged;
		}
	}

	// Token: 0x17000433 RID: 1075
	// (get) Token: 0x06001695 RID: 5781 RVA: 0x000EB140 File Offset: 0x000E9340
	public static string MMB
	{
		get
		{
			return Language.sL.MMB;
		}
	}

	// Token: 0x17000434 RID: 1076
	// (get) Token: 0x06001696 RID: 5782 RVA: 0x000EB14C File Offset: 0x000E934C
	public static string ForCamChanged
	{
		get
		{
			return Language.sL.ForCamChanged;
		}
	}

	// Token: 0x17000435 RID: 1077
	// (get) Token: 0x06001697 RID: 5783 RVA: 0x000EB158 File Offset: 0x000E9358
	public static string LMB
	{
		get
		{
			return Language.sL.LMB;
		}
	}

	// Token: 0x17000436 RID: 1078
	// (get) Token: 0x06001698 RID: 5784 RVA: 0x000EB164 File Offset: 0x000E9364
	public static string SpecForBeginGame
	{
		get
		{
			return Language.sL.SpecForBeginGame;
		}
	}

	// Token: 0x17000437 RID: 1079
	// (get) Token: 0x06001699 RID: 5785 RVA: 0x000EB170 File Offset: 0x000E9370
	public static string SpecForChooseTeam
	{
		get
		{
			return Language.sL.SpecForChooseTeam;
		}
	}

	// Token: 0x17000438 RID: 1080
	// (get) Token: 0x0600169A RID: 5786 RVA: 0x000EB17C File Offset: 0x000E937C
	public static string SpecCantChangeTeam
	{
		get
		{
			return Language.sL.SpecCantChangeTeam;
		}
	}

	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x0600169B RID: 5787 RVA: 0x000EB188 File Offset: 0x000E9388
	public static string SpecTeamIsOverpowered
	{
		get
		{
			return Language.sL.SpecTeamIsOverpowered;
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x0600169C RID: 5788 RVA: 0x000EB194 File Offset: 0x000E9394
	public static string SpecWaitForBeginRound
	{
		get
		{
			return Language.sL.SpecWaitForBeginRound;
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x0600169D RID: 5789 RVA: 0x000EB1A0 File Offset: 0x000E93A0
	public static string[] CarrClassName
	{
		get
		{
			return Language.sL.CarrClassName;
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x0600169E RID: 5790 RVA: 0x000EB1AC File Offset: 0x000E93AC
	public static string CarrClassified
	{
		get
		{
			return Language.sL.CarrClassified;
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x0600169F RID: 5791 RVA: 0x000EB1B8 File Offset: 0x000E93B8
	public static string CarrUserForbidYourself
	{
		get
		{
			return Language.sL.CarrUserForbidYourself;
		}
	}

	// Token: 0x1700043E RID: 1086
	// (get) Token: 0x060016A0 RID: 5792 RVA: 0x000EB1C4 File Offset: 0x000E93C4
	public static string CarrLVL
	{
		get
		{
			return Language.sL.CarrLVL;
		}
	}

	// Token: 0x1700043F RID: 1087
	// (get) Token: 0x060016A1 RID: 5793 RVA: 0x000EB1D0 File Offset: 0x000E93D0
	public static string CarrNameSet
	{
		get
		{
			return Language.sL.CarrNameSet;
		}
	}

	// Token: 0x17000440 RID: 1088
	// (get) Token: 0x060016A2 RID: 5794 RVA: 0x000EB1DC File Offset: 0x000E93DC
	public static string CarrToTheNextRank
	{
		get
		{
			return Language.sL.CarrToTheNextRank;
		}
	}

	// Token: 0x17000441 RID: 1089
	// (get) Token: 0x060016A3 RID: 5795 RVA: 0x000EB1E8 File Offset: 0x000E93E8
	public static string CarrRank
	{
		get
		{
			return Language.sL.CarrRank;
		}
	}

	// Token: 0x17000442 RID: 1090
	// (get) Token: 0x060016A4 RID: 5796 RVA: 0x000EB1F4 File Offset: 0x000E93F4
	public static string CarrNextRank
	{
		get
		{
			return Language.sL.CarrNextRank;
		}
	}

	// Token: 0x17000443 RID: 1091
	// (get) Token: 0x060016A5 RID: 5797 RVA: 0x000EB200 File Offset: 0x000E9400
	public static string CarrPlaceRanking
	{
		get
		{
			return Language.sL.CarrPlaceRanking;
		}
	}

	// Token: 0x17000444 RID: 1092
	// (get) Token: 0x060016A6 RID: 5798 RVA: 0x000EB20C File Offset: 0x000E940C
	public static string CarrAchievementComplete
	{
		get
		{
			return Language.sL.CarrAchievementComplete;
		}
	}

	// Token: 0x17000445 RID: 1093
	// (get) Token: 0x060016A7 RID: 5799 RVA: 0x000EB218 File Offset: 0x000E9418
	public static string CarrAchievementNext
	{
		get
		{
			return Language.sL.CarrAchievementNext;
		}
	}

	// Token: 0x17000446 RID: 1094
	// (get) Token: 0x060016A8 RID: 5800 RVA: 0x000EB224 File Offset: 0x000E9424
	public static string NoSmall
	{
		get
		{
			return Language.sL.NoSmall;
		}
	}

	// Token: 0x17000447 RID: 1095
	// (get) Token: 0x060016A9 RID: 5801 RVA: 0x000EB230 File Offset: 0x000E9430
	public static string CarrCollectCards
	{
		get
		{
			return Language.sL.CarrCollectCards;
		}
	}

	// Token: 0x17000448 RID: 1096
	// (get) Token: 0x060016AA RID: 5802 RVA: 0x000EB23C File Offset: 0x000E943C
	public static string CarrLastCollectedCard
	{
		get
		{
			return Language.sL.CarrLastCollectedCard;
		}
	}

	// Token: 0x17000449 RID: 1097
	// (get) Token: 0x060016AB RID: 5803 RVA: 0x000EB248 File Offset: 0x000E9448
	public static string CarrWTaskComplete
	{
		get
		{
			return Language.sL.CarrWTaskComplete;
		}
	}

	// Token: 0x1700044A RID: 1098
	// (get) Token: 0x060016AC RID: 5804 RVA: 0x000EB254 File Offset: 0x000E9454
	public static string CarrContractsComplete
	{
		get
		{
			return Language.sL.CarrContractsComplete;
		}
	}

	// Token: 0x1700044B RID: 1099
	// (get) Token: 0x060016AD RID: 5805 RVA: 0x000EB260 File Offset: 0x000E9460
	public static string CarrCurrentContract
	{
		get
		{
			return Language.sL.CarrCurrentContract;
		}
	}

	// Token: 0x1700044C RID: 1100
	// (get) Token: 0x060016AE RID: 5806 RVA: 0x000EB26C File Offset: 0x000E946C
	public static string CarrSpecialBadges
	{
		get
		{
			return Language.sL.CarrSpecialBadges;
		}
	}

	// Token: 0x1700044D RID: 1101
	// (get) Token: 0x060016AF RID: 5807 RVA: 0x000EB278 File Offset: 0x000E9478
	public static string CarrSpecialBadge
	{
		get
		{
			return Language.sL.CarrSpecialBadge;
		}
	}

	// Token: 0x1700044E RID: 1102
	// (get) Token: 0x060016B0 RID: 5808 RVA: 0x000EB284 File Offset: 0x000E9484
	public static string CarrProfile
	{
		get
		{
			return Language.sL.CarrProfile;
		}
	}

	// Token: 0x1700044F RID: 1103
	// (get) Token: 0x060016B1 RID: 5809 RVA: 0x000EB290 File Offset: 0x000E9490
	public static string CarrTopFriends
	{
		get
		{
			return Language.sL.CarrTopFriends;
		}
	}

	// Token: 0x17000450 RID: 1104
	// (get) Token: 0x060016B2 RID: 5810 RVA: 0x000EB29C File Offset: 0x000E949C
	public static string CarrTopClans
	{
		get
		{
			return Language.sL.CarrTopClans;
		}
	}

	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x060016B3 RID: 5811 RVA: 0x000EB2A8 File Offset: 0x000E94A8
	public static string CarrClans
	{
		get
		{
			return Language.sL.CarrClans;
		}
	}

	// Token: 0x17000452 RID: 1106
	// (get) Token: 0x060016B4 RID: 5812 RVA: 0x000EB2B4 File Offset: 0x000E94B4
	public static string CarrPlace
	{
		get
		{
			return Language.sL.CarrPlace;
		}
	}

	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x060016B5 RID: 5813 RVA: 0x000EB2C0 File Offset: 0x000E94C0
	public static string CarrTOP
	{
		get
		{
			return Language.sL.CarrTOP;
		}
	}

	// Token: 0x17000454 RID: 1108
	// (get) Token: 0x060016B6 RID: 5814 RVA: 0x000EB2CC File Offset: 0x000E94CC
	public static string CarrTAG
	{
		get
		{
			return Language.sL.CarrTAG;
		}
	}

	// Token: 0x17000455 RID: 1109
	// (get) Token: 0x060016B7 RID: 5815 RVA: 0x000EB2D8 File Offset: 0x000E94D8
	public static string CarrPlayers
	{
		get
		{
			return Language.sL.CarrPlayers;
		}
	}

	// Token: 0x17000456 RID: 1110
	// (get) Token: 0x060016B8 RID: 5816 RVA: 0x000EB2E4 File Offset: 0x000E94E4
	public static string CarrName
	{
		get
		{
			return Language.sL.CarrName;
		}
	}

	// Token: 0x17000457 RID: 1111
	// (get) Token: 0x060016B9 RID: 5817 RVA: 0x000EB2F0 File Offset: 0x000E94F0
	public static string CarrEXP
	{
		get
		{
			return Language.sL.CarrEXP;
		}
	}

	// Token: 0x17000458 RID: 1112
	// (get) Token: 0x060016BA RID: 5818 RVA: 0x000EB2FC File Offset: 0x000E94FC
	public static string CarrTop100lvl
	{
		get
		{
			return Language.sL.CarrTop100lvl;
		}
	}

	// Token: 0x17000459 RID: 1113
	// (get) Token: 0x060016BB RID: 5819 RVA: 0x000EB308 File Offset: 0x000E9508
	public static string CarrItemName
	{
		get
		{
			return Language.sL.CarrItemName;
		}
	}

	// Token: 0x1700045A RID: 1114
	// (get) Token: 0x060016BC RID: 5820 RVA: 0x000EB314 File Offset: 0x000E9514
	public static string CarrPoints
	{
		get
		{
			return Language.sL.CarrPoints;
		}
	}

	// Token: 0x1700045B RID: 1115
	// (get) Token: 0x060016BD RID: 5821 RVA: 0x000EB320 File Offset: 0x000E9520
	public static string CarrTop100EXP
	{
		get
		{
			return Language.sL.CarrTop100EXP;
		}
	}

	// Token: 0x1700045C RID: 1116
	// (get) Token: 0x060016BE RID: 5822 RVA: 0x000EB32C File Offset: 0x000E952C
	public static string CarrKills
	{
		get
		{
			return Language.sL.CarrKills;
		}
	}

	// Token: 0x1700045D RID: 1117
	// (get) Token: 0x060016BF RID: 5823 RVA: 0x000EB338 File Offset: 0x000E9538
	public static string CarrTop100Kills
	{
		get
		{
			return Language.sL.CarrTop100Kills;
		}
	}

	// Token: 0x1700045E RID: 1118
	// (get) Token: 0x060016C0 RID: 5824 RVA: 0x000EB344 File Offset: 0x000E9544
	public static string CarrDeath
	{
		get
		{
			return Language.sL.CarrDeath;
		}
	}

	// Token: 0x1700045F RID: 1119
	// (get) Token: 0x060016C1 RID: 5825 RVA: 0x000EB350 File Offset: 0x000E9550
	public static string CarrTop100Death
	{
		get
		{
			return Language.sL.CarrTop100Death;
		}
	}

	// Token: 0x17000460 RID: 1120
	// (get) Token: 0x060016C2 RID: 5826 RVA: 0x000EB35C File Offset: 0x000E955C
	public static string CarrTop100KD
	{
		get
		{
			return Language.sL.CarrTop100KD;
		}
	}

	// Token: 0x17000461 RID: 1121
	// (get) Token: 0x060016C3 RID: 5827 RVA: 0x000EB368 File Offset: 0x000E9568
	public static string CarrReputation
	{
		get
		{
			return Language.sL.CarrReputation;
		}
	}

	// Token: 0x17000462 RID: 1122
	// (get) Token: 0x060016C4 RID: 5828 RVA: 0x000EB374 File Offset: 0x000E9574
	public static string SeasonReward
	{
		get
		{
			return Language.sL.SeasonReward;
		}
	}

	// Token: 0x17000463 RID: 1123
	// (get) Token: 0x060016C5 RID: 5829 RVA: 0x000EB380 File Offset: 0x000E9580
	public static string CarrTop100Rep
	{
		get
		{
			return Language.sL.CarrTop100Rep;
		}
	}

	// Token: 0x17000464 RID: 1124
	// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000EB38C File Offset: 0x000E958C
	public static string CarrCardDescr
	{
		get
		{
			return Language.sL.CarrCardDescr;
		}
	}

	// Token: 0x17000465 RID: 1125
	// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000EB398 File Offset: 0x000E9598
	public static string CarrAvaliableAt
	{
		get
		{
			return Language.sL.CarrAvaliableAt;
		}
	}

	// Token: 0x17000466 RID: 1126
	// (get) Token: 0x060016C8 RID: 5832 RVA: 0x000EB3A4 File Offset: 0x000E95A4
	public static string CarrMapName
	{
		get
		{
			return Language.sL.CarrMapName;
		}
	}

	// Token: 0x17000467 RID: 1127
	// (get) Token: 0x060016C9 RID: 5833 RVA: 0x000EB3B0 File Offset: 0x000E95B0
	public static string CarrDaily
	{
		get
		{
			return Language.sL.CarrDaily;
		}
	}

	// Token: 0x17000468 RID: 1128
	// (get) Token: 0x060016CA RID: 5834 RVA: 0x000EB3BC File Offset: 0x000E95BC
	public static string CarrCurrentContractsCAPS
	{
		get
		{
			return Language.sL.CarrCurrentContractsCAPS;
		}
	}

	// Token: 0x17000469 RID: 1129
	// (get) Token: 0x060016CB RID: 5835 RVA: 0x000EB3C8 File Offset: 0x000E95C8
	public static string CarrNextContractsCAPS
	{
		get
		{
			return Language.sL.CarrNextContractsCAPS;
		}
	}

	// Token: 0x1700046A RID: 1130
	// (get) Token: 0x060016CC RID: 5836 RVA: 0x000EB3D4 File Offset: 0x000E95D4
	public static string CarrSkipContract
	{
		get
		{
			return Language.sL.CarrSkipContract;
		}
	}

	// Token: 0x1700046B RID: 1131
	// (get) Token: 0x060016CD RID: 5837 RVA: 0x000EB3E0 File Offset: 0x000E95E0
	public static string CarrUpdateContractsPopupHeader
	{
		get
		{
			return Language.sL.CarrUpdateContractsPopupHeader;
		}
	}

	// Token: 0x1700046C RID: 1132
	// (get) Token: 0x060016CE RID: 5838 RVA: 0x000EB3EC File Offset: 0x000E95EC
	public static string CarrUpdateContractsPopupBody0
	{
		get
		{
			return Language.sL.CarrUpdateContractsPopupBody0;
		}
	}

	// Token: 0x1700046D RID: 1133
	// (get) Token: 0x060016CF RID: 5839 RVA: 0x000EB3F8 File Offset: 0x000E95F8
	public static string CarrUpdateContractsPopupBody1
	{
		get
		{
			return Language.sL.CarrUpdateContractsPopupBody1;
		}
	}

	// Token: 0x1700046E RID: 1134
	// (get) Token: 0x060016D0 RID: 5840 RVA: 0x000EB404 File Offset: 0x000E9604
	public static string CarrSkipContractPopupHeader
	{
		get
		{
			return Language.sL.CarrSkipContractPopupHeader;
		}
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x060016D1 RID: 5841 RVA: 0x000EB410 File Offset: 0x000E9610
	public static string CarrSkipContractPopupBody0
	{
		get
		{
			return Language.sL.CarrSkipContractPopupBody0;
		}
	}

	// Token: 0x17000470 RID: 1136
	// (get) Token: 0x060016D2 RID: 5842 RVA: 0x000EB41C File Offset: 0x000E961C
	public static string CarrSkipContractPopupBody1
	{
		get
		{
			return Language.sL.CarrSkipContractPopupBody1;
		}
	}

	// Token: 0x17000471 RID: 1137
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x000EB428 File Offset: 0x000E9628
	public static string CarrSkipContractPopupBody2
	{
		get
		{
			return Language.sL.CarrSkipContractPopupBody2;
		}
	}

	// Token: 0x17000472 RID: 1138
	// (get) Token: 0x060016D4 RID: 5844 RVA: 0x000EB434 File Offset: 0x000E9634
	public static string CarrContractRefreshDescr0
	{
		get
		{
			return Language.sL.CarrContractRefreshDescr0;
		}
	}

	// Token: 0x17000473 RID: 1139
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x000EB440 File Offset: 0x000E9640
	public static string CarrContractRefreshDescr1
	{
		get
		{
			return Language.sL.CarrContractRefreshDescr1;
		}
	}

	// Token: 0x17000474 RID: 1140
	// (get) Token: 0x060016D6 RID: 5846 RVA: 0x000EB44C File Offset: 0x000E964C
	public static string CarrContractRefreshDescr2
	{
		get
		{
			return Language.sL.CarrContractRefreshDescr2;
		}
	}

	// Token: 0x17000475 RID: 1141
	// (get) Token: 0x060016D7 RID: 5847 RVA: 0x000EB458 File Offset: 0x000E9658
	public static string CarrContractRefreshDescr3
	{
		get
		{
			return Language.sL.CarrContractRefreshDescr3;
		}
	}

	// Token: 0x17000476 RID: 1142
	// (get) Token: 0x060016D8 RID: 5848 RVA: 0x000EB464 File Offset: 0x000E9664
	public static string CarrOnlineTime
	{
		get
		{
			return Language.sL.CarrOnlineTime;
		}
	}

	// Token: 0x17000477 RID: 1143
	// (get) Token: 0x060016D9 RID: 5849 RVA: 0x000EB470 File Offset: 0x000E9670
	public static string CarrHardcoreTime
	{
		get
		{
			return Language.sL.CarrHardcoreTime;
		}
	}

	// Token: 0x17000478 RID: 1144
	// (get) Token: 0x060016DA RID: 5850 RVA: 0x000EB47C File Offset: 0x000E967C
	public static string CarrWins
	{
		get
		{
			return Language.sL.CarrWins;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x060016DB RID: 5851 RVA: 0x000EB488 File Offset: 0x000E9688
	public static string CarrLoose
	{
		get
		{
			return Language.sL.CarrLoose;
		}
	}

	// Token: 0x1700047A RID: 1146
	// (get) Token: 0x060016DC RID: 5852 RVA: 0x000EB494 File Offset: 0x000E9694
	public static string CarrDamage
	{
		get
		{
			return Language.sL.CarrDamage;
		}
	}

	// Token: 0x1700047B RID: 1147
	// (get) Token: 0x060016DD RID: 5853 RVA: 0x000EB4A0 File Offset: 0x000E96A0
	public static string CarrUsedBullets
	{
		get
		{
			return Language.sL.CarrUsedBullets;
		}
	}

	// Token: 0x1700047C RID: 1148
	// (get) Token: 0x060016DE RID: 5854 RVA: 0x000EB4AC File Offset: 0x000E96AC
	public static string CarrHeadShots
	{
		get
		{
			return Language.sL.CarrHeadShots;
		}
	}

	// Token: 0x1700047D RID: 1149
	// (get) Token: 0x060016DF RID: 5855 RVA: 0x000EB4B8 File Offset: 0x000E96B8
	public static string CarrDoubleHeadShots
	{
		get
		{
			return Language.sL.CarrDoubleHeadShots;
		}
	}

	// Token: 0x1700047E RID: 1150
	// (get) Token: 0x060016E0 RID: 5856 RVA: 0x000EB4C4 File Offset: 0x000E96C4
	public static string CarrLongHeadShots
	{
		get
		{
			return Language.sL.CarrLongHeadShots;
		}
	}

	// Token: 0x1700047F RID: 1151
	// (get) Token: 0x060016E1 RID: 5857 RVA: 0x000EB4D0 File Offset: 0x000E96D0
	public static string CarrDoubleKills
	{
		get
		{
			return Language.sL.CarrDoubleKills;
		}
	}

	// Token: 0x17000480 RID: 1152
	// (get) Token: 0x060016E2 RID: 5858 RVA: 0x000EB4DC File Offset: 0x000E96DC
	public static string CarrTripleKills
	{
		get
		{
			return Language.sL.CarrTripleKills;
		}
	}

	// Token: 0x17000481 RID: 1153
	// (get) Token: 0x060016E3 RID: 5859 RVA: 0x000EB4E8 File Offset: 0x000E96E8
	public static string CarrAssists
	{
		get
		{
			return Language.sL.CarrAssists;
		}
	}

	// Token: 0x17000482 RID: 1154
	// (get) Token: 0x060016E4 RID: 5860 RVA: 0x000EB4F4 File Offset: 0x000E96F4
	public static string CarrCreditSpend
	{
		get
		{
			return Language.sL.CarrCreditSpend;
		}
	}

	// Token: 0x17000483 RID: 1155
	// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000EB500 File Offset: 0x000E9700
	public static string CarrWTaskOpened
	{
		get
		{
			return Language.sL.CarrWTaskOpened;
		}
	}

	// Token: 0x17000484 RID: 1156
	// (get) Token: 0x060016E6 RID: 5862 RVA: 0x000EB50C File Offset: 0x000E970C
	public static string CarrAchievingGetted
	{
		get
		{
			return Language.sL.CarrAchievingGetted;
		}
	}

	// Token: 0x17000485 RID: 1157
	// (get) Token: 0x060016E7 RID: 5863 RVA: 0x000EB518 File Offset: 0x000E9718
	public static string CarrArmstreakGetted
	{
		get
		{
			return Language.sL.CarrArmstreakGetted;
		}
	}

	// Token: 0x17000486 RID: 1158
	// (get) Token: 0x060016E8 RID: 5864 RVA: 0x000EB524 File Offset: 0x000E9724
	public static string CarrSupportCaused
	{
		get
		{
			return Language.sL.CarrSupportCaused;
		}
	}

	// Token: 0x17000487 RID: 1159
	// (get) Token: 0x060016E9 RID: 5865 RVA: 0x000EB530 File Offset: 0x000E9730
	public static string CarrKnifeKill
	{
		get
		{
			return Language.sL.CarrKnifeKill;
		}
	}

	// Token: 0x17000488 RID: 1160
	// (get) Token: 0x060016EA RID: 5866 RVA: 0x000EB53C File Offset: 0x000E973C
	public static string CarrGrenadeKill
	{
		get
		{
			return Language.sL.CarrGrenadeKill;
		}
	}

	// Token: 0x17000489 RID: 1161
	// (get) Token: 0x060016EB RID: 5867 RVA: 0x000EB548 File Offset: 0x000E9748
	public static string CarrTeammateKill
	{
		get
		{
			return Language.sL.CarrTeammateKill;
		}
	}

	// Token: 0x1700048A RID: 1162
	// (get) Token: 0x060016EC RID: 5868 RVA: 0x000EB554 File Offset: 0x000E9754
	public static string CarrSuicides
	{
		get
		{
			return Language.sL.CarrSuicides;
		}
	}

	// Token: 0x1700048B RID: 1163
	// (get) Token: 0x060016ED RID: 5869 RVA: 0x000EB560 File Offset: 0x000E9760
	public static string CarrBEARKills
	{
		get
		{
			return Language.sL.CarrBEARKills;
		}
	}

	// Token: 0x1700048C RID: 1164
	// (get) Token: 0x060016EE RID: 5870 RVA: 0x000EB56C File Offset: 0x000E976C
	public static string CarrUsecKills
	{
		get
		{
			return Language.sL.CarrUsecKills;
		}
	}

	// Token: 0x1700048D RID: 1165
	// (get) Token: 0x060016EF RID: 5871 RVA: 0x000EB578 File Offset: 0x000E9778
	public static string CarrFavoriteWeapon
	{
		get
		{
			return Language.sL.CarrFavoriteWeapon;
		}
	}

	// Token: 0x1700048E RID: 1166
	// (get) Token: 0x060016F0 RID: 5872 RVA: 0x000EB584 File Offset: 0x000E9784
	public static string CarrTotalAccuracy
	{
		get
		{
			return Language.sL.CarrTotalAccuracy;
		}
	}

	// Token: 0x1700048F RID: 1167
	// (get) Token: 0x060016F1 RID: 5873 RVA: 0x000EB590 File Offset: 0x000E9790
	public static string CarrMatchesCompleted
	{
		get
		{
			return Language.sL.CarrMatchesCompleted;
		}
	}

	// Token: 0x17000490 RID: 1168
	// (get) Token: 0x060016F2 RID: 5874 RVA: 0x000EB59C File Offset: 0x000E979C
	public static string CarrStormtrooper
	{
		get
		{
			return Language.sL.CarrStormtrooper;
		}
	}

	// Token: 0x17000491 RID: 1169
	// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000EB5A8 File Offset: 0x000E97A8
	public static string CarrDestroyer
	{
		get
		{
			return Language.sL.CarrDestroyer;
		}
	}

	// Token: 0x17000492 RID: 1170
	// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000EB5B4 File Offset: 0x000E97B4
	public static string CarrSniper
	{
		get
		{
			return Language.sL.CarrSniper;
		}
	}

	// Token: 0x17000493 RID: 1171
	// (get) Token: 0x060016F5 RID: 5877 RVA: 0x000EB5C0 File Offset: 0x000E97C0
	public static string CarrArmorer
	{
		get
		{
			return Language.sL.CarrArmorer;
		}
	}

	// Token: 0x17000494 RID: 1172
	// (get) Token: 0x060016F6 RID: 5878 RVA: 0x000EB5CC File Offset: 0x000E97CC
	public static string CarrCareerist
	{
		get
		{
			return Language.sL.CarrCareerist;
		}
	}

	// Token: 0x17000495 RID: 1173
	// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000EB5D8 File Offset: 0x000E97D8
	public static string CarrCurrentBalance
	{
		get
		{
			return Language.sL.CarrCurrentBalance;
		}
	}

	// Token: 0x17000496 RID: 1174
	// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000EB5E4 File Offset: 0x000E97E4
	public static string CarrResetSkills
	{
		get
		{
			return Language.sL.CarrResetSkills;
		}
	}

	// Token: 0x17000497 RID: 1175
	// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000EB5F0 File Offset: 0x000E97F0
	public static string CarrBootSkills
	{
		get
		{
			return Language.sL.CarrBootSkills;
		}
	}

	// Token: 0x17000498 RID: 1176
	// (get) Token: 0x060016FA RID: 5882 RVA: 0x000EB5FC File Offset: 0x000E97FC
	public static string CarrBonus
	{
		get
		{
			return Language.sL.CarrBonus;
		}
	}

	// Token: 0x17000499 RID: 1177
	// (get) Token: 0x060016FB RID: 5883 RVA: 0x000EB608 File Offset: 0x000E9808
	public static string CarrRentTime
	{
		get
		{
			return Language.sL.CarrRentTime;
		}
	}

	// Token: 0x1700049A RID: 1178
	// (get) Token: 0x060016FC RID: 5884 RVA: 0x000EB614 File Offset: 0x000E9814
	public static string CarrUnlocked
	{
		get
		{
			return Language.sL.CarrUnlocked;
		}
	}

	// Token: 0x1700049B RID: 1179
	// (get) Token: 0x060016FD RID: 5885 RVA: 0x000EB620 File Offset: 0x000E9820
	public static string CarrYouInTheBattle
	{
		get
		{
			return Language.sL.CarrYouInTheBattle;
		}
	}

	// Token: 0x1700049C RID: 1180
	// (get) Token: 0x060016FE RID: 5886 RVA: 0x000EB62C File Offset: 0x000E982C
	public static string CarrTemporarilyUnavailable
	{
		get
		{
			return Language.sL.CarrTemporarilyUnavailable;
		}
	}

	// Token: 0x1700049D RID: 1181
	// (get) Token: 0x060016FF RID: 5887 RVA: 0x000EB638 File Offset: 0x000E9838
	public static string CarrSkills
	{
		get
		{
			return Language.sL.CarrSkills;
		}
	}

	// Token: 0x1700049E RID: 1182
	// (get) Token: 0x06001700 RID: 5888 RVA: 0x000EB644 File Offset: 0x000E9844
	public static string CarrRating
	{
		get
		{
			return Language.sL.CarrRating;
		}
	}

	// Token: 0x1700049F RID: 1183
	// (get) Token: 0x06001701 RID: 5889 RVA: 0x000EB650 File Offset: 0x000E9850
	public static string CarrSummary
	{
		get
		{
			return Language.sL.CarrSummary;
		}
	}

	// Token: 0x170004A0 RID: 1184
	// (get) Token: 0x06001702 RID: 5890 RVA: 0x000EB65C File Offset: 0x000E985C
	public static string CarrAchievements
	{
		get
		{
			return Language.sL.CarrAchievements;
		}
	}

	// Token: 0x170004A1 RID: 1185
	// (get) Token: 0x06001703 RID: 5891 RVA: 0x000EB668 File Offset: 0x000E9868
	public static string CarrContracts
	{
		get
		{
			return Language.sL.CarrContracts;
		}
	}

	// Token: 0x170004A2 RID: 1186
	// (get) Token: 0x06001704 RID: 5892 RVA: 0x000EB674 File Offset: 0x000E9874
	public static string CarrStatistics
	{
		get
		{
			return Language.sL.CarrStatistics;
		}
	}

	// Token: 0x170004A3 RID: 1187
	// (get) Token: 0x06001705 RID: 5893 RVA: 0x000EB680 File Offset: 0x000E9880
	public static string BankGet
	{
		get
		{
			return Language.sL.BankGet;
		}
	}

	// Token: 0x170004A4 RID: 1188
	// (get) Token: 0x06001706 RID: 5894 RVA: 0x000EB68C File Offset: 0x000E988C
	public static string BankAvaliable
	{
		get
		{
			return Language.sL.BankAvaliable;
		}
	}

	// Token: 0x170004A5 RID: 1189
	// (get) Token: 0x06001707 RID: 5895 RVA: 0x000EB698 File Offset: 0x000E9898
	public static string BankCostCAPS
	{
		get
		{
			return Language.sL.BankCostCAPS;
		}
	}

	// Token: 0x170004A6 RID: 1190
	// (get) Token: 0x06001708 RID: 5896 RVA: 0x000EB6A4 File Offset: 0x000E98A4
	public static string BankBuySP
	{
		get
		{
			return Language.sL.BankBuySP;
		}
	}

	// Token: 0x170004A7 RID: 1191
	// (get) Token: 0x06001709 RID: 5897 RVA: 0x000EB6B0 File Offset: 0x000E98B0
	public static string CWSAUpdateBalance
	{
		get
		{
			return Language.sL.CWSAUpdateBalance;
		}
	}

	// Token: 0x170004A8 RID: 1192
	// (get) Token: 0x0600170A RID: 5898 RVA: 0x000EB6BC File Offset: 0x000E98BC
	public static string BankFor
	{
		get
		{
			return Language.sL.BankFor;
		}
	}

	// Token: 0x170004A9 RID: 1193
	// (get) Token: 0x0600170B RID: 5899 RVA: 0x000EB6C8 File Offset: 0x000E98C8
	public static string BankVote
	{
		get
		{
			return Language.sL.BankVote;
		}
	}

	// Token: 0x170004AA RID: 1194
	// (get) Token: 0x0600170C RID: 5900 RVA: 0x000EB6D4 File Offset: 0x000E98D4
	public static string BankVote_golosov
	{
		get
		{
			return Language.sL.BankVote_golosov;
		}
	}

	// Token: 0x170004AB RID: 1195
	// (get) Token: 0x0600170D RID: 5901 RVA: 0x000EB6E0 File Offset: 0x000E98E0
	public static string BankVote_golosa
	{
		get
		{
			return Language.sL.BankVote_golosa;
		}
	}

	// Token: 0x170004AC RID: 1196
	// (get) Token: 0x0600170E RID: 5902 RVA: 0x000EB6EC File Offset: 0x000E98EC
	public static string BankDiscount
	{
		get
		{
			return Language.sL.BankDiscount;
		}
	}

	// Token: 0x170004AD RID: 1197
	// (get) Token: 0x0600170F RID: 5903 RVA: 0x000EB6F8 File Offset: 0x000E98F8
	public static string BankCurrency
	{
		get
		{
			return Language.sL.BankCurrency;
		}
	}

	// Token: 0x170004AE RID: 1198
	// (get) Token: 0x06001710 RID: 5904 RVA: 0x000EB704 File Offset: 0x000E9904
	public static string BankCurFS
	{
		get
		{
			return Language.sL.BankCurFS;
		}
	}

	// Token: 0x170004AF RID: 1199
	// (get) Token: 0x06001711 RID: 5905 RVA: 0x000EB710 File Offset: 0x000E9910
	public static string BankCurMailru
	{
		get
		{
			return Language.sL.BankCurMailru;
		}
	}

	// Token: 0x170004B0 RID: 1200
	// (get) Token: 0x06001712 RID: 5906 RVA: 0x000EB71C File Offset: 0x000E991C
	public static string BankCurOK
	{
		get
		{
			return Language.sL.BankCurOK;
		}
	}

	// Token: 0x170004B1 RID: 1201
	// (get) Token: 0x06001713 RID: 5907 RVA: 0x000EB728 File Offset: 0x000E9928
	public static string BankSPTitle
	{
		get
		{
			return Language.sL.BankSPTitle;
		}
	}

	// Token: 0x170004B2 RID: 1202
	// (get) Token: 0x06001714 RID: 5908 RVA: 0x000EB734 File Offset: 0x000E9934
	public static string BankMPTitle
	{
		get
		{
			return Language.sL.BankMPTitle;
		}
	}

	// Token: 0x170004B3 RID: 1203
	// (get) Token: 0x06001715 RID: 5909 RVA: 0x000EB740 File Offset: 0x000E9940
	public static string BankCRTitle
	{
		get
		{
			return Language.sL.BankCRTitle;
		}
	}

	// Token: 0x170004B4 RID: 1204
	// (get) Token: 0x06001716 RID: 5910 RVA: 0x000EB74C File Offset: 0x000E994C
	public static string BankVKTitle0
	{
		get
		{
			return Language.sL.BankVKTitle0;
		}
	}

	// Token: 0x170004B5 RID: 1205
	// (get) Token: 0x06001717 RID: 5911 RVA: 0x000EB758 File Offset: 0x000E9958
	public static string BankVKTitle1
	{
		get
		{
			return Language.sL.BankVKTitle1;
		}
	}

	// Token: 0x170004B6 RID: 1206
	// (get) Token: 0x06001718 RID: 5912 RVA: 0x000EB764 File Offset: 0x000E9964
	public static string BankFRTitle0
	{
		get
		{
			return Language.sL.BankFRTitle0;
		}
	}

	// Token: 0x170004B7 RID: 1207
	// (get) Token: 0x06001719 RID: 5913 RVA: 0x000EB770 File Offset: 0x000E9970
	public static string BankFRTitle1
	{
		get
		{
			return Language.sL.BankFRTitle1;
		}
	}

	// Token: 0x170004B8 RID: 1208
	// (get) Token: 0x0600171A RID: 5914 RVA: 0x000EB77C File Offset: 0x000E997C
	public static string BankFBTitle0
	{
		get
		{
			return Language.sL.BankFBTitle0;
		}
	}

	// Token: 0x170004B9 RID: 1209
	// (get) Token: 0x0600171B RID: 5915 RVA: 0x000EB788 File Offset: 0x000E9988
	public static string BankFBTitle1
	{
		get
		{
			return Language.sL.BankFBTitle1;
		}
	}

	// Token: 0x170004BA RID: 1210
	// (get) Token: 0x0600171C RID: 5916 RVA: 0x000EB794 File Offset: 0x000E9994
	public static string BankFSTitle0
	{
		get
		{
			return Language.sL.BankFSTitle0;
		}
	}

	// Token: 0x170004BB RID: 1211
	// (get) Token: 0x0600171D RID: 5917 RVA: 0x000EB7A0 File Offset: 0x000E99A0
	public static string BankFSTitle1
	{
		get
		{
			return Language.sL.BankFSTitle1;
		}
	}

	// Token: 0x170004BC RID: 1212
	// (get) Token: 0x0600171E RID: 5918 RVA: 0x000EB7AC File Offset: 0x000E99AC
	public static string BankMailruTitle0
	{
		get
		{
			return Language.sL.BankMailruTitle0;
		}
	}

	// Token: 0x170004BD RID: 1213
	// (get) Token: 0x0600171F RID: 5919 RVA: 0x000EB7B8 File Offset: 0x000E99B8
	public static string BankMailruTitle1
	{
		get
		{
			return Language.sL.BankMailruTitle1;
		}
	}

	// Token: 0x170004BE RID: 1214
	// (get) Token: 0x06001720 RID: 5920 RVA: 0x000EB7C4 File Offset: 0x000E99C4
	public static string BankOKTitle0
	{
		get
		{
			return Language.sL.BankOKTitle0;
		}
	}

	// Token: 0x170004BF RID: 1215
	// (get) Token: 0x06001721 RID: 5921 RVA: 0x000EB7D0 File Offset: 0x000E99D0
	public static string BankOKTitle1
	{
		get
		{
			return Language.sL.BankOKTitle1;
		}
	}

	// Token: 0x170004C0 RID: 1216
	// (get) Token: 0x06001722 RID: 5922 RVA: 0x000EB7DC File Offset: 0x000E99DC
	public static string BankTransaction
	{
		get
		{
			return Language.sL.BankTransaction;
		}
	}

	// Token: 0x170004C1 RID: 1217
	// (get) Token: 0x06001723 RID: 5923 RVA: 0x000EB7E8 File Offset: 0x000E99E8
	public static string BankHistory
	{
		get
		{
			return Language.sL.BankHistory;
		}
	}

	// Token: 0x170004C2 RID: 1218
	// (get) Token: 0x06001724 RID: 5924 RVA: 0x000EB7F4 File Offset: 0x000E99F4
	public static string BankServices
	{
		get
		{
			return Language.sL.BankServices;
		}
	}

	// Token: 0x170004C3 RID: 1219
	// (get) Token: 0x06001725 RID: 5925 RVA: 0x000EB800 File Offset: 0x000E9A00
	public static string BankOperationHistory
	{
		get
		{
			return Language.sL.BankOperationHistory;
		}
	}

	// Token: 0x170004C4 RID: 1220
	// (get) Token: 0x06001726 RID: 5926 RVA: 0x000EB80C File Offset: 0x000E9A0C
	public static string BankQuantity
	{
		get
		{
			return Language.sL.BankQuantity;
		}
	}

	// Token: 0x170004C5 RID: 1221
	// (get) Token: 0x06001727 RID: 5927 RVA: 0x000EB818 File Offset: 0x000E9A18
	public static string BankValuta
	{
		get
		{
			return Language.sL.BankValuta;
		}
	}

	// Token: 0x170004C6 RID: 1222
	// (get) Token: 0x06001728 RID: 5928 RVA: 0x000EB824 File Offset: 0x000E9A24
	public static string BankComment
	{
		get
		{
			return Language.sL.BankComment;
		}
	}

	// Token: 0x170004C7 RID: 1223
	// (get) Token: 0x06001729 RID: 5929 RVA: 0x000EB830 File Offset: 0x000E9A30
	public static string BankDate
	{
		get
		{
			return Language.sL.BankDate;
		}
	}

	// Token: 0x170004C8 RID: 1224
	// (get) Token: 0x0600172A RID: 5930 RVA: 0x000EB83C File Offset: 0x000E9A3C
	public static string TabLoading
	{
		get
		{
			return Language.sL.TabLoading;
		}
	}

	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x0600172B RID: 5931 RVA: 0x000EB848 File Offset: 0x000E9A48
	public static string TabLeadingOnPoints
	{
		get
		{
			return Language.sL.TabLeadingOnPoints;
		}
	}

	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x0600172C RID: 5932 RVA: 0x000EB854 File Offset: 0x000E9A54
	public static string TabPing
	{
		get
		{
			return Language.sL.TabPing;
		}
	}

	// Token: 0x170004CB RID: 1227
	// (get) Token: 0x0600172D RID: 5933 RVA: 0x000EB860 File Offset: 0x000E9A60
	public static string TabSuspectPlayer
	{
		get
		{
			return Language.sL.TabSuspectPlayer;
		}
	}

	// Token: 0x170004CC RID: 1228
	// (get) Token: 0x0600172E RID: 5934 RVA: 0x000EB86C File Offset: 0x000E9A6C
	public static string TabAlreadySuspectedPlayer
	{
		get
		{
			return Language.sL.TabAlreadySuspectedPlayer;
		}
	}

	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x0600172F RID: 5935 RVA: 0x000EB878 File Offset: 0x000E9A78
	public static string TabSuspected
	{
		get
		{
			return Language.sL.TabSuspected;
		}
	}

	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x06001730 RID: 5936 RVA: 0x000EB884 File Offset: 0x000E9A84
	public static string TabSuspectCheat
	{
		get
		{
			return Language.sL.TabSuspectCheat;
		}
	}

	// Token: 0x170004CF RID: 1231
	// (get) Token: 0x06001731 RID: 5937 RVA: 0x000EB890 File Offset: 0x000E9A90
	public static string TabSuspectBugUse
	{
		get
		{
			return Language.sL.TabSuspectBugUse;
		}
	}

	// Token: 0x170004D0 RID: 1232
	// (get) Token: 0x06001732 RID: 5938 RVA: 0x000EB89C File Offset: 0x000E9A9C
	public static string TabSuspectAbuse
	{
		get
		{
			return Language.sL.TabSuspectAbuse;
		}
	}

	// Token: 0x170004D1 RID: 1233
	// (get) Token: 0x06001733 RID: 5939 RVA: 0x000EB8A8 File Offset: 0x000E9AA8
	public static string TabNeedReputation
	{
		get
		{
			return Language.sL.TabNeedReputation;
		}
	}

	// Token: 0x170004D2 RID: 1234
	// (get) Token: 0x06001734 RID: 5940 RVA: 0x000EB8B4 File Offset: 0x000E9AB4
	public static string SettingsHigh
	{
		get
		{
			return Language.sL.SettingsHigh;
		}
	}

	// Token: 0x170004D3 RID: 1235
	// (get) Token: 0x06001735 RID: 5941 RVA: 0x000EB8C0 File Offset: 0x000E9AC0
	public static string SettingsMiddle
	{
		get
		{
			return Language.sL.SettingsMiddle;
		}
	}

	// Token: 0x170004D4 RID: 1236
	// (get) Token: 0x06001736 RID: 5942 RVA: 0x000EB8CC File Offset: 0x000E9ACC
	public static string SettingsLow
	{
		get
		{
			return Language.sL.SettingsLow;
		}
	}

	// Token: 0x170004D5 RID: 1237
	// (get) Token: 0x06001737 RID: 5943 RVA: 0x000EB8D8 File Offset: 0x000E9AD8
	public static string SettingsMax
	{
		get
		{
			return Language.sL.SettingsMax;
		}
	}

	// Token: 0x170004D6 RID: 1238
	// (get) Token: 0x06001738 RID: 5944 RVA: 0x000EB8E4 File Offset: 0x000E9AE4
	public static string SettingsNickAllow
	{
		get
		{
			return Language.sL.SettingsNickAllow;
		}
	}

	// Token: 0x170004D7 RID: 1239
	// (get) Token: 0x06001739 RID: 5945 RVA: 0x000EB8F0 File Offset: 0x000E9AF0
	public static string SettingsNickNotAllow
	{
		get
		{
			return Language.sL.SettingsNickNotAllow;
		}
	}

	// Token: 0x170004D8 RID: 1240
	// (get) Token: 0x0600173A RID: 5946 RVA: 0x000EB8FC File Offset: 0x000E9AFC
	public static string SettingsGame
	{
		get
		{
			return Language.sL.SettingsGame;
		}
	}

	// Token: 0x170004D9 RID: 1241
	// (get) Token: 0x0600173B RID: 5947 RVA: 0x000EB908 File Offset: 0x000E9B08
	public static string SettingsVideoAudio
	{
		get
		{
			return Language.sL.SettingsVideoAudio;
		}
	}

	// Token: 0x170004DA RID: 1242
	// (get) Token: 0x0600173C RID: 5948 RVA: 0x000EB914 File Offset: 0x000E9B14
	public static string SettingsControl
	{
		get
		{
			return Language.sL.SettingsControl;
		}
	}

	// Token: 0x170004DB RID: 1243
	// (get) Token: 0x0600173D RID: 5949 RVA: 0x000EB920 File Offset: 0x000E9B20
	public static string SettingsNetwork
	{
		get
		{
			return Language.sL.SettingsNetwork;
		}
	}

	// Token: 0x170004DC RID: 1244
	// (get) Token: 0x0600173E RID: 5950 RVA: 0x000EB92C File Offset: 0x000E9B2C
	public static string SettingsBonuses
	{
		get
		{
			return Language.sL.SettingsBonuses;
		}
	}

	// Token: 0x170004DD RID: 1245
	// (get) Token: 0x0600173F RID: 5951 RVA: 0x000EB938 File Offset: 0x000E9B38
	public static string SettingsHopsBonus
	{
		get
		{
			return Language.sL.SettingsHopsBonus;
		}
	}

	// Token: 0x170004DE RID: 1246
	// (get) Token: 0x06001740 RID: 5952 RVA: 0x000EB944 File Offset: 0x000E9B44
	public static string SettingsHopsBonusHint
	{
		get
		{
			return Language.sL.SettingsHopsBonusHint;
		}
	}

	// Token: 0x170004DF RID: 1247
	// (get) Token: 0x06001741 RID: 5953 RVA: 0x000EB950 File Offset: 0x000E9B50
	public static string SettingsHopsKey
	{
		get
		{
			return Language.sL.SettingsHopsKey;
		}
	}

	// Token: 0x170004E0 RID: 1248
	// (get) Token: 0x06001742 RID: 5954 RVA: 0x000EB95C File Offset: 0x000E9B5C
	public static string SettingsApply
	{
		get
		{
			return Language.sL.SettingsApply;
		}
	}

	// Token: 0x170004E1 RID: 1249
	// (get) Token: 0x06001743 RID: 5955 RVA: 0x000EB968 File Offset: 0x000E9B68
	public static string SettingsClan
	{
		get
		{
			return Language.sL.SettingsClan;
		}
	}

	// Token: 0x170004E2 RID: 1250
	// (get) Token: 0x06001744 RID: 5956 RVA: 0x000EB974 File Offset: 0x000E9B74
	public static string SettingsNickCheck
	{
		get
		{
			return Language.sL.SettingsNickCheck;
		}
	}

	// Token: 0x170004E3 RID: 1251
	// (get) Token: 0x06001745 RID: 5957 RVA: 0x000EB980 File Offset: 0x000E9B80
	public static string SettingsNickMaxLenght
	{
		get
		{
			return Language.sL.SettingsNickMaxLenght;
		}
	}

	// Token: 0x170004E4 RID: 1252
	// (get) Token: 0x06001746 RID: 5958 RVA: 0x000EB98C File Offset: 0x000E9B8C
	public static string SettingsYourNickUsedInGame
	{
		get
		{
			return Language.sL.SettingsYourNickUsedInGame;
		}
	}

	// Token: 0x170004E5 RID: 1253
	// (get) Token: 0x06001747 RID: 5959 RVA: 0x000EB998 File Offset: 0x000E9B98
	public static string SettingsYouAllowToChangeNick
	{
		get
		{
			return Language.sL.SettingsYouAllowToChangeNick;
		}
	}

	// Token: 0x170004E6 RID: 1254
	// (get) Token: 0x06001748 RID: 5960 RVA: 0x000EB9A4 File Offset: 0x000E9BA4
	public static string SettingsTimes
	{
		get
		{
			return Language.sL.SettingsTimes;
		}
	}

	// Token: 0x170004E7 RID: 1255
	// (get) Token: 0x06001749 RID: 5961 RVA: 0x000EB9B0 File Offset: 0x000E9BB0
	public static string SettingsBuyNickChange
	{
		get
		{
			return Language.sL.SettingsBuyNickChange;
		}
	}

	// Token: 0x170004E8 RID: 1256
	// (get) Token: 0x0600174A RID: 5962 RVA: 0x000EB9BC File Offset: 0x000E9BBC
	public static string SettingsBuyChange
	{
		get
		{
			return Language.sL.SettingsBuyChange;
		}
	}

	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x0600174B RID: 5963 RVA: 0x000EB9C8 File Offset: 0x000E9BC8
	public static string SettingsBuyColorChange
	{
		get
		{
			return Language.sL.SettingsBuyColorChange;
		}
	}

	// Token: 0x170004EA RID: 1258
	// (get) Token: 0x0600174C RID: 5964 RVA: 0x000EB9D4 File Offset: 0x000E9BD4
	public static string SettingsBuyChangePopUp
	{
		get
		{
			return Language.sL.SettingsBuyChangePopUp;
		}
	}

	// Token: 0x170004EB RID: 1259
	// (get) Token: 0x0600174D RID: 5965 RVA: 0x000EB9E0 File Offset: 0x000E9BE0
	public static string SettingsBuyChangeColorPopUp
	{
		get
		{
			return Language.sL.SettingsBuyChangeColorPopUp;
		}
	}

	// Token: 0x170004EC RID: 1260
	// (get) Token: 0x0600174E RID: 5966 RVA: 0x000EB9EC File Offset: 0x000E9BEC
	public static string SettingsTransoprentyRadar
	{
		get
		{
			return Language.sL.SettingsTransoprentyRadar;
		}
	}

	// Token: 0x170004ED RID: 1261
	// (get) Token: 0x0600174F RID: 5967 RVA: 0x000EB9F8 File Offset: 0x000E9BF8
	public static string SettingsShowProgressContract
	{
		get
		{
			return Language.sL.SettingsShowProgressContract;
		}
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x06001750 RID: 5968 RVA: 0x000EBA04 File Offset: 0x000E9C04
	public static string SettingsSimpleShowContract
	{
		get
		{
			return Language.sL.SettingsSimpleShowContract;
		}
	}

	// Token: 0x170004EF RID: 1263
	// (get) Token: 0x06001751 RID: 5969 RVA: 0x000EBA10 File Offset: 0x000E9C10
	public static string SettingsAlwaysShowHpDef
	{
		get
		{
			return Language.sL.SettingsAlwaysShowHpDef;
		}
	}

	// Token: 0x170004F0 RID: 1264
	// (get) Token: 0x06001752 RID: 5970 RVA: 0x000EBA1C File Offset: 0x000E9C1C
	public static string SettingsAutorespawn
	{
		get
		{
			return Language.sL.SettingsAutorespawn;
		}
	}

	// Token: 0x170004F1 RID: 1265
	// (get) Token: 0x06001753 RID: 5971 RVA: 0x000EBA28 File Offset: 0x000E9C28
	public static string SettingsSwitchOffChat
	{
		get
		{
			return Language.sL.SettingsSwitchOffChat;
		}
	}

	// Token: 0x170004F2 RID: 1266
	// (get) Token: 0x06001754 RID: 5972 RVA: 0x000EBA34 File Offset: 0x000E9C34
	public static string SettingsSecondaryEquiped
	{
		get
		{
			return Language.sL.SettingsSecondaryEquiped;
		}
	}

	// Token: 0x170004F3 RID: 1267
	// (get) Token: 0x06001755 RID: 5973 RVA: 0x000EBA40 File Offset: 0x000E9C40
	public static string SettingsHideInterface
	{
		get
		{
			return Language.sL.SettingsHideInterface;
		}
	}

	// Token: 0x170004F4 RID: 1268
	// (get) Token: 0x06001756 RID: 5974 RVA: 0x000EBA4C File Offset: 0x000E9C4C
	public static string SettingsEnableFullScreenInBattle
	{
		get
		{
			return Language.sL.SettingsEnableFullScreenInBattle;
		}
	}

	// Token: 0x170004F5 RID: 1269
	// (get) Token: 0x06001757 RID: 5975 RVA: 0x000EBA58 File Offset: 0x000E9C58
	public static string SettingsScreenRez
	{
		get
		{
			return Language.sL.SettingsScreenRez;
		}
	}

	// Token: 0x170004F6 RID: 1270
	// (get) Token: 0x06001758 RID: 5976 RVA: 0x000EBA64 File Offset: 0x000E9C64
	public static string SettingsModelQuality
	{
		get
		{
			return Language.sL.SettingsModelQuality;
		}
	}

	// Token: 0x170004F7 RID: 1271
	// (get) Token: 0x06001759 RID: 5977 RVA: 0x000EBA70 File Offset: 0x000E9C70
	public static string SettingsPostEffect
	{
		get
		{
			return Language.sL.SettingsPostEffect;
		}
	}

	// Token: 0x170004F8 RID: 1272
	// (get) Token: 0x0600175A RID: 5978 RVA: 0x000EBA7C File Offset: 0x000E9C7C
	public static string LimitFrameRate
	{
		get
		{
			return Language.sL.LimitFrameRate;
		}
	}

	// Token: 0x170004F9 RID: 1273
	// (get) Token: 0x0600175B RID: 5979 RVA: 0x000EBA88 File Offset: 0x000E9C88
	public static string SettingsGraphicQuality
	{
		get
		{
			return Language.sL.SettingsGraphicQuality;
		}
	}

	// Token: 0x170004FA RID: 1274
	// (get) Token: 0x0600175C RID: 5980 RVA: 0x000EBA94 File Offset: 0x000E9C94
	public static string SettingsVeryLow
	{
		get
		{
			return Language.sL.SettingsVeryLow;
		}
	}

	// Token: 0x170004FB RID: 1275
	// (get) Token: 0x0600175D RID: 5981 RVA: 0x000EBAA0 File Offset: 0x000E9CA0
	public static string SettingsLowMiddle
	{
		get
		{
			return Language.sL.SettingsLowMiddle;
		}
	}

	// Token: 0x170004FC RID: 1276
	// (get) Token: 0x0600175E RID: 5982 RVA: 0x000EBAAC File Offset: 0x000E9CAC
	public static string SettingsCustom
	{
		get
		{
			return Language.sL.SettingsCustom;
		}
	}

	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x0600175F RID: 5983 RVA: 0x000EBAB8 File Offset: 0x000E9CB8
	public static string SettingsAdvancedSettingsGr
	{
		get
		{
			return Language.sL.SettingsAdvancedSettingsGr;
		}
	}

	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06001760 RID: 5984 RVA: 0x000EBAC4 File Offset: 0x000E9CC4
	public static string SettingsShadowRadius
	{
		get
		{
			return Language.sL.SettingsShadowRadius;
		}
	}

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x06001761 RID: 5985 RVA: 0x000EBAD0 File Offset: 0x000E9CD0
	public static string SettingsPaltryObjects
	{
		get
		{
			return Language.sL.SettingsPaltryObjects;
		}
	}

	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x06001762 RID: 5986 RVA: 0x000EBADC File Offset: 0x000E9CDC
	public static string SettingsAudioMusic
	{
		get
		{
			return Language.sL.SettingsAudioMusic;
		}
	}

	// Token: 0x17000501 RID: 1281
	// (get) Token: 0x06001763 RID: 5987 RVA: 0x000EBAE8 File Offset: 0x000E9CE8
	public static string SettingsOverallVolume
	{
		get
		{
			return Language.sL.SettingsOverallVolume;
		}
	}

	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x06001764 RID: 5988 RVA: 0x000EBAF4 File Offset: 0x000E9CF4
	public static string SettingsSoundVolume
	{
		get
		{
			return Language.sL.SettingsSoundVolume;
		}
	}

	// Token: 0x17000503 RID: 1283
	// (get) Token: 0x06001765 RID: 5989 RVA: 0x000EBB00 File Offset: 0x000E9D00
	public static string SettingsRadioVolume
	{
		get
		{
			return Language.sL.SettingsRadioVolume;
		}
	}

	// Token: 0x17000504 RID: 1284
	// (get) Token: 0x06001766 RID: 5990 RVA: 0x000EBB0C File Offset: 0x000E9D0C
	public static string SettingsTextureQuality
	{
		get
		{
			return Language.sL.SettingsTextureQuality;
		}
	}

	// Token: 0x17000505 RID: 1285
	// (get) Token: 0x06001767 RID: 5991 RVA: 0x000EBB18 File Offset: 0x000E9D18
	public static string SettingsShadowQuality
	{
		get
		{
			return Language.sL.SettingsShadowQuality;
		}
	}

	// Token: 0x17000506 RID: 1286
	// (get) Token: 0x06001768 RID: 5992 RVA: 0x000EBB24 File Offset: 0x000E9D24
	public static string SettingsLightningQuality
	{
		get
		{
			return Language.sL.SettingsLightningQuality;
		}
	}

	// Token: 0x17000507 RID: 1287
	// (get) Token: 0x06001769 RID: 5993 RVA: 0x000EBB30 File Offset: 0x000E9D30
	public static string SettingsPhysicsQuality
	{
		get
		{
			return Language.sL.SettingsPhysicsQuality;
		}
	}

	// Token: 0x17000508 RID: 1288
	// (get) Token: 0x0600176A RID: 5994 RVA: 0x000EBB3C File Offset: 0x000E9D3C
	public static string SettingsDefaultValue
	{
		get
		{
			return Language.sL.SettingsDefaultValue;
		}
	}

	// Token: 0x17000509 RID: 1289
	// (get) Token: 0x0600176B RID: 5995 RVA: 0x000EBB48 File Offset: 0x000E9D48
	public static string SettingsAction
	{
		get
		{
			return Language.sL.SettingsAction;
		}
	}

	// Token: 0x1700050A RID: 1290
	// (get) Token: 0x0600176C RID: 5996 RVA: 0x000EBB54 File Offset: 0x000E9D54
	public static string SettingsContolButton
	{
		get
		{
			return Language.sL.SettingsContolButton;
		}
	}

	// Token: 0x1700050B RID: 1291
	// (get) Token: 0x0600176D RID: 5997 RVA: 0x000EBB60 File Offset: 0x000E9D60
	public static string SettingsMouseSensitivity
	{
		get
		{
			return Language.sL.SettingsMouseSensitivity;
		}
	}

	// Token: 0x1700050C RID: 1292
	// (get) Token: 0x0600176E RID: 5998 RVA: 0x000EBB6C File Offset: 0x000E9D6C
	public static string SettingsInvertMouse
	{
		get
		{
			return Language.sL.SettingsInvertMouse;
		}
	}

	// Token: 0x1700050D RID: 1293
	// (get) Token: 0x0600176F RID: 5999 RVA: 0x000EBB78 File Offset: 0x000E9D78
	public static string SettingsHold
	{
		get
		{
			return Language.sL.SettingsHold;
		}
	}

	// Token: 0x1700050E RID: 1294
	// (get) Token: 0x06001770 RID: 6000 RVA: 0x000EBB84 File Offset: 0x000E9D84
	public static string SettingsMoveForward
	{
		get
		{
			return Language.sL.SettingsMoveForward;
		}
	}

	// Token: 0x1700050F RID: 1295
	// (get) Token: 0x06001771 RID: 6001 RVA: 0x000EBB90 File Offset: 0x000E9D90
	public static string SettingsMoveBack
	{
		get
		{
			return Language.sL.SettingsMoveBack;
		}
	}

	// Token: 0x17000510 RID: 1296
	// (get) Token: 0x06001772 RID: 6002 RVA: 0x000EBB9C File Offset: 0x000E9D9C
	public static string SettingsMoveLeft
	{
		get
		{
			return Language.sL.SettingsMoveLeft;
		}
	}

	// Token: 0x17000511 RID: 1297
	// (get) Token: 0x06001773 RID: 6003 RVA: 0x000EBBA8 File Offset: 0x000E9DA8
	public static string SettingsMoveRight
	{
		get
		{
			return Language.sL.SettingsMoveRight;
		}
	}

	// Token: 0x17000512 RID: 1298
	// (get) Token: 0x06001774 RID: 6004 RVA: 0x000EBBB4 File Offset: 0x000E9DB4
	public static string SettingsJump
	{
		get
		{
			return Language.sL.SettingsJump;
		}
	}

	// Token: 0x17000513 RID: 1299
	// (get) Token: 0x06001775 RID: 6005 RVA: 0x000EBBC0 File Offset: 0x000E9DC0
	public static string SettingsWalk
	{
		get
		{
			return Language.sL.SettingsWalk;
		}
	}

	// Token: 0x17000514 RID: 1300
	// (get) Token: 0x06001776 RID: 6006 RVA: 0x000EBBCC File Offset: 0x000E9DCC
	public static string SettingsSit
	{
		get
		{
			return Language.sL.SettingsSit;
		}
	}

	// Token: 0x17000515 RID: 1301
	// (get) Token: 0x06001777 RID: 6007 RVA: 0x000EBBD8 File Offset: 0x000E9DD8
	public static string SettingsFire
	{
		get
		{
			return Language.sL.SettingsFire;
		}
	}

	// Token: 0x17000516 RID: 1302
	// (get) Token: 0x06001778 RID: 6008 RVA: 0x000EBBE4 File Offset: 0x000E9DE4
	public static string SettingsAim
	{
		get
		{
			return Language.sL.SettingsAim;
		}
	}

	// Token: 0x17000517 RID: 1303
	// (get) Token: 0x06001779 RID: 6009 RVA: 0x000EBBF0 File Offset: 0x000E9DF0
	public static string SettingsRecharge
	{
		get
		{
			return Language.sL.SettingsRecharge;
		}
	}

	// Token: 0x17000518 RID: 1304
	// (get) Token: 0x0600177A RID: 6010 RVA: 0x000EBBFC File Offset: 0x000E9DFC
	public static string SettingsGrenade
	{
		get
		{
			return Language.sL.SettingsGrenade;
		}
	}

	// Token: 0x17000519 RID: 1305
	// (get) Token: 0x0600177B RID: 6011 RVA: 0x000EBC08 File Offset: 0x000E9E08
	public static string SettingsKnife
	{
		get
		{
			return Language.sL.SettingsKnife;
		}
	}

	// Token: 0x1700051A RID: 1306
	// (get) Token: 0x0600177C RID: 6012 RVA: 0x000EBC14 File Offset: 0x000E9E14
	public static string SettingsFireMode
	{
		get
		{
			return Language.sL.SettingsFireMode;
		}
	}

	// Token: 0x1700051B RID: 1307
	// (get) Token: 0x0600177D RID: 6013 RVA: 0x000EBC20 File Offset: 0x000E9E20
	public static string SettingsSwitchWeapon
	{
		get
		{
			return Language.sL.SettingsSwitchWeapon;
		}
	}

	// Token: 0x1700051C RID: 1308
	// (get) Token: 0x0600177E RID: 6014 RVA: 0x000EBC2C File Offset: 0x000E9E2C
	public static string SettingsSelectPistol
	{
		get
		{
			return Language.sL.SettingsSelectPistol;
		}
	}

	// Token: 0x1700051D RID: 1309
	// (get) Token: 0x0600177F RID: 6015 RVA: 0x000EBC38 File Offset: 0x000E9E38
	public static string SettingsSelectMainWeapon
	{
		get
		{
			return Language.sL.SettingsSelectMainWeapon;
		}
	}

	// Token: 0x1700051E RID: 1310
	// (get) Token: 0x06001780 RID: 6016 RVA: 0x000EBC44 File Offset: 0x000E9E44
	public static string SettingsUse
	{
		get
		{
			return Language.sL.SettingsUse;
		}
	}

	// Token: 0x1700051F RID: 1311
	// (get) Token: 0x06001781 RID: 6017 RVA: 0x000EBC50 File Offset: 0x000E9E50
	public static string SettingsCallSupport
	{
		get
		{
			return Language.sL.SettingsCallSupport;
		}
	}

	// Token: 0x17000520 RID: 1312
	// (get) Token: 0x06001782 RID: 6018 RVA: 0x000EBC5C File Offset: 0x000E9E5C
	public static string SettingsCallMortarStrike
	{
		get
		{
			return Language.sL.SettingsCallMortarStrike;
		}
	}

	// Token: 0x17000521 RID: 1313
	// (get) Token: 0x06001783 RID: 6019 RVA: 0x000EBC68 File Offset: 0x000E9E68
	public static string SettingsCallSonar
	{
		get
		{
			return Language.sL.SettingsCallSonar;
		}
	}

	// Token: 0x17000522 RID: 1314
	// (get) Token: 0x06001784 RID: 6020 RVA: 0x000EBC74 File Offset: 0x000E9E74
	public static string SettingsUseSpecEquipment
	{
		get
		{
			return Language.sL.SettingsUseSpecEquipment;
		}
	}

	// Token: 0x17000523 RID: 1315
	// (get) Token: 0x06001785 RID: 6021 RVA: 0x000EBC80 File Offset: 0x000E9E80
	public static string SettingsHideShowInterface
	{
		get
		{
			return Language.sL.SettingsHideShowInterface;
		}
	}

	// Token: 0x17000524 RID: 1316
	// (get) Token: 0x06001786 RID: 6022 RVA: 0x000EBC8C File Offset: 0x000E9E8C
	public static string SettingsMatchStatistics
	{
		get
		{
			return Language.sL.SettingsMatchStatistics;
		}
	}

	// Token: 0x17000525 RID: 1317
	// (get) Token: 0x06001787 RID: 6023 RVA: 0x000EBC98 File Offset: 0x000E9E98
	public static string SettingsExitToMainMenu
	{
		get
		{
			return Language.sL.SettingsExitToMainMenu;
		}
	}

	// Token: 0x17000526 RID: 1318
	// (get) Token: 0x06001788 RID: 6024 RVA: 0x000EBCA4 File Offset: 0x000E9EA4
	public static string SettingsFullScreen
	{
		get
		{
			return Language.sL.SettingsFullScreen;
		}
	}

	// Token: 0x17000527 RID: 1319
	// (get) Token: 0x06001789 RID: 6025 RVA: 0x000EBCB0 File Offset: 0x000E9EB0
	public static string SettingsTeamChange
	{
		get
		{
			return Language.sL.SettingsTeamChange;
		}
	}

	// Token: 0x17000528 RID: 1320
	// (get) Token: 0x0600178A RID: 6026 RVA: 0x000EBCBC File Offset: 0x000E9EBC
	public static string SettingsTeamMessage
	{
		get
		{
			return Language.sL.SettingsTeamMessage;
		}
	}

	// Token: 0x17000529 RID: 1321
	// (get) Token: 0x0600178B RID: 6027 RVA: 0x000EBCC8 File Offset: 0x000E9EC8
	public static string SettingsMessageToAll
	{
		get
		{
			return Language.sL.SettingsMessageToAll;
		}
	}

	// Token: 0x1700052A RID: 1322
	// (get) Token: 0x0600178C RID: 6028 RVA: 0x000EBCD4 File Offset: 0x000E9ED4
	public static string SettingsRadioCommand
	{
		get
		{
			return Language.sL.SettingsRadioCommand;
		}
	}

	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x0600178D RID: 6029 RVA: 0x000EBCE0 File Offset: 0x000E9EE0
	public static string SettingsScreenshot
	{
		get
		{
			return Language.sL.SettingsScreenshot;
		}
	}

	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x0600178E RID: 6030 RVA: 0x000EBCEC File Offset: 0x000E9EEC
	public static string SettingsNetworkDescr0
	{
		get
		{
			return Language.sL.SettingsNetworkDescr0;
		}
	}

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x0600178F RID: 6031 RVA: 0x000EBCF8 File Offset: 0x000E9EF8
	public static string SettingsNetworkDescr1
	{
		get
		{
			return Language.sL.SettingsNetworkDescr1;
		}
	}

	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06001790 RID: 6032 RVA: 0x000EBD04 File Offset: 0x000E9F04
	public static string SettingsNetworkDescr2
	{
		get
		{
			return Language.sL.SettingsNetworkDescr2;
		}
	}

	// Token: 0x1700052F RID: 1327
	// (get) Token: 0x06001791 RID: 6033 RVA: 0x000EBD10 File Offset: 0x000E9F10
	public static string SettingsNetworkDescr3
	{
		get
		{
			return Language.sL.SettingsNetworkDescr3;
		}
	}

	// Token: 0x17000530 RID: 1328
	// (get) Token: 0x06001792 RID: 6034 RVA: 0x000EBD1C File Offset: 0x000E9F1C
	public static string SettingsNetworkDescr4
	{
		get
		{
			return Language.sL.SettingsNetworkDescr4;
		}
	}

	// Token: 0x17000531 RID: 1329
	// (get) Token: 0x06001793 RID: 6035 RVA: 0x000EBD28 File Offset: 0x000E9F28
	public static string SettingsNetworkDescr5
	{
		get
		{
			return Language.sL.SettingsNetworkDescr5;
		}
	}

	// Token: 0x17000532 RID: 1330
	// (get) Token: 0x06001794 RID: 6036 RVA: 0x000EBD34 File Offset: 0x000E9F34
	public static string SettingsNetworkDescr6
	{
		get
		{
			return Language.sL.SettingsNetworkDescr6;
		}
	}

	// Token: 0x17000533 RID: 1331
	// (get) Token: 0x06001795 RID: 6037 RVA: 0x000EBD40 File Offset: 0x000E9F40
	public static string GrenadeThrowMessage1
	{
		get
		{
			return Language.sL.GrenadeThrowMessage1;
		}
	}

	// Token: 0x17000534 RID: 1332
	// (get) Token: 0x06001796 RID: 6038 RVA: 0x000EBD4C File Offset: 0x000E9F4C
	public static string GrenadeThrowMessage2
	{
		get
		{
			return Language.sL.GrenadeThrowMessage2;
		}
	}

	// Token: 0x17000535 RID: 1333
	// (get) Token: 0x06001797 RID: 6039 RVA: 0x000EBD58 File Offset: 0x000E9F58
	public static string SGGlobal
	{
		get
		{
			return Language.sL.SGGlobal;
		}
	}

	// Token: 0x17000536 RID: 1334
	// (get) Token: 0x06001798 RID: 6040 RVA: 0x000EBD64 File Offset: 0x000E9F64
	public static string SGWhithoutRating
	{
		get
		{
			return Language.sL.SGWhithoutRating;
		}
	}

	// Token: 0x17000537 RID: 1335
	// (get) Token: 0x06001799 RID: 6041 RVA: 0x000EBD70 File Offset: 0x000E9F70
	public static string SGFritends
	{
		get
		{
			return Language.sL.SGFritends;
		}
	}

	// Token: 0x17000538 RID: 1336
	// (get) Token: 0x0600179A RID: 6042 RVA: 0x000EBD7C File Offset: 0x000E9F7C
	public static string SGFavorites
	{
		get
		{
			return Language.sL.SGFavorites;
		}
	}

	// Token: 0x17000539 RID: 1337
	// (get) Token: 0x0600179B RID: 6043 RVA: 0x000EBD88 File Offset: 0x000E9F88
	public static string SGLatests
	{
		get
		{
			return Language.sL.SGLatests;
		}
	}

	// Token: 0x1700053A RID: 1338
	// (get) Token: 0x0600179C RID: 6044 RVA: 0x000EBD94 File Offset: 0x000E9F94
	public static string SGServer
	{
		get
		{
			return Language.sL.SGServer;
		}
	}

	// Token: 0x1700053B RID: 1339
	// (get) Token: 0x0600179D RID: 6045 RVA: 0x000EBDA0 File Offset: 0x000E9FA0
	public static string SGMap
	{
		get
		{
			return Language.sL.SGMap;
		}
	}

	// Token: 0x1700053C RID: 1340
	// (get) Token: 0x0600179E RID: 6046 RVA: 0x000EBDAC File Offset: 0x000E9FAC
	public static string SGRate
	{
		get
		{
			return Language.sL.SGRate;
		}
	}

	// Token: 0x1700053D RID: 1341
	// (get) Token: 0x0600179F RID: 6047 RVA: 0x000EBDB8 File Offset: 0x000E9FB8
	public static string SGPlayers
	{
		get
		{
			return Language.sL.SGPlayers;
		}
	}

	// Token: 0x1700053E RID: 1342
	// (get) Token: 0x060017A0 RID: 6048 RVA: 0x000EBDC4 File Offset: 0x000E9FC4
	public static string SGMode
	{
		get
		{
			return Language.sL.SGMode;
		}
	}

	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x060017A1 RID: 6049 RVA: 0x000EBDD0 File Offset: 0x000E9FD0
	public static string SGEmpty
	{
		get
		{
			return Language.sL.SGEmpty;
		}
	}

	// Token: 0x17000540 RID: 1344
	// (get) Token: 0x060017A2 RID: 6050 RVA: 0x000EBDDC File Offset: 0x000E9FDC
	public static string SGFull
	{
		get
		{
			return Language.sL.SGFull;
		}
	}

	// Token: 0x17000541 RID: 1345
	// (get) Token: 0x060017A3 RID: 6051 RVA: 0x000EBDE8 File Offset: 0x000E9FE8
	public static string SGLevel
	{
		get
		{
			return Language.sL.SGLevel;
		}
	}

	// Token: 0x17000542 RID: 1346
	// (get) Token: 0x060017A4 RID: 6052 RVA: 0x000EBDF4 File Offset: 0x000E9FF4
	public static string SGCreateServer
	{
		get
		{
			return Language.sL.SGCreateServer;
		}
	}

	// Token: 0x17000543 RID: 1347
	// (get) Token: 0x060017A5 RID: 6053 RVA: 0x000EBE00 File Offset: 0x000EA000
	public static string SGConnect
	{
		get
		{
			return Language.sL.SGConnect;
		}
	}

	// Token: 0x17000544 RID: 1348
	// (get) Token: 0x060017A6 RID: 6054 RVA: 0x000EBE0C File Offset: 0x000EA00C
	public static string ChToAll
	{
		get
		{
			return Language.sL.ChToAll;
		}
	}

	// Token: 0x17000545 RID: 1349
	// (get) Token: 0x060017A7 RID: 6055 RVA: 0x000EBE18 File Offset: 0x000EA018
	public static string ChToTeam
	{
		get
		{
			return Language.sL.ChToTeam;
		}
	}

	// Token: 0x17000546 RID: 1350
	// (get) Token: 0x060017A8 RID: 6056 RVA: 0x000EBE24 File Offset: 0x000EA024
	public static string Task
	{
		get
		{
			return Language.sL.Task;
		}
	}

	// Token: 0x17000547 RID: 1351
	// (get) Token: 0x060017A9 RID: 6057 RVA: 0x000EBE30 File Offset: 0x000EA030
	public static string Packages
	{
		get
		{
			return Language.sL.Packages;
		}
	}

	// Token: 0x17000548 RID: 1352
	// (get) Token: 0x060017AA RID: 6058 RVA: 0x000EBE3C File Offset: 0x000EA03C
	public static string IdleKick
	{
		get
		{
			return Language.sL.IdleKick;
		}
	}

	// Token: 0x17000549 RID: 1353
	// (get) Token: 0x060017AB RID: 6059 RVA: 0x000EBE48 File Offset: 0x000EA048
	public static string PingKickHeader
	{
		get
		{
			return Language.sL.PingKickHeader;
		}
	}

	// Token: 0x1700054A RID: 1354
	// (get) Token: 0x060017AC RID: 6060 RVA: 0x000EBE54 File Offset: 0x000EA054
	public static string PingKickBody1
	{
		get
		{
			return Language.sL.PingKickBody1;
		}
	}

	// Token: 0x1700054B RID: 1355
	// (get) Token: 0x060017AD RID: 6061 RVA: 0x000EBE60 File Offset: 0x000EA060
	public static string PingKickBody2
	{
		get
		{
			return Language.sL.PingKickBody2;
		}
	}

	// Token: 0x1700054C RID: 1356
	// (get) Token: 0x060017AE RID: 6062 RVA: 0x000EBE6C File Offset: 0x000EA06C
	public static string TeamKillKick
	{
		get
		{
			return Language.sL.TeamKillKick;
		}
	}

	// Token: 0x1700054D RID: 1357
	// (get) Token: 0x060017AF RID: 6063 RVA: 0x000EBE78 File Offset: 0x000EA078
	public static string TeamKillBan
	{
		get
		{
			return Language.sL.TeamKillBan;
		}
	}

	// Token: 0x1700054E RID: 1358
	// (get) Token: 0x060017B0 RID: 6064 RVA: 0x000EBE84 File Offset: 0x000EA084
	public static string ErrorNetworkProtocol
	{
		get
		{
			return Language.sL.ErrorNetworkProtocol;
		}
	}

	// Token: 0x1700054F RID: 1359
	// (get) Token: 0x060017B1 RID: 6065 RVA: 0x000EBE90 File Offset: 0x000EA090
	public static string FloodKick
	{
		get
		{
			return Language.sL.FloodKick;
		}
	}

	// Token: 0x17000550 RID: 1360
	// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000EBE9C File Offset: 0x000EA09C
	public static string ServerFullPlayers
	{
		get
		{
			return Language.sL.ServerFullPlayers;
		}
	}

	// Token: 0x17000551 RID: 1361
	// (get) Token: 0x060017B3 RID: 6067 RVA: 0x000EBEA8 File Offset: 0x000EA0A8
	public static string ServerFullSpec
	{
		get
		{
			return Language.sL.ServerFullSpec;
		}
	}

	// Token: 0x17000552 RID: 1362
	// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000EBEB4 File Offset: 0x000EA0B4
	public static string ServerFullSlot
	{
		get
		{
			return Language.sL.ServerFullSlot;
		}
	}

	// Token: 0x17000553 RID: 1363
	// (get) Token: 0x060017B5 RID: 6069 RVA: 0x000EBEC0 File Offset: 0x000EA0C0
	public static string ServerSlotAvaliable
	{
		get
		{
			return Language.sL.ServerSlotAvaliable;
		}
	}

	// Token: 0x17000554 RID: 1364
	// (get) Token: 0x060017B6 RID: 6070 RVA: 0x000EBECC File Offset: 0x000EA0CC
	public static string ServerRestarted
	{
		get
		{
			return Language.sL.ServerRestarted;
		}
	}

	// Token: 0x17000555 RID: 1365
	// (get) Token: 0x060017B7 RID: 6071 RVA: 0x000EBED8 File Offset: 0x000EA0D8
	public static string UserQuited
	{
		get
		{
			return Language.sL.UserQuited;
		}
	}

	// Token: 0x17000556 RID: 1366
	// (get) Token: 0x060017B8 RID: 6072 RVA: 0x000EBEE4 File Offset: 0x000EA0E4
	public static string ServerDisconnect
	{
		get
		{
			return Language.sL.ServerDisconnect;
		}
	}

	// Token: 0x17000557 RID: 1367
	// (get) Token: 0x060017B9 RID: 6073 RVA: 0x000EBEF0 File Offset: 0x000EA0F0
	public static string Dead
	{
		get
		{
			return Language.sL.Dead;
		}
	}

	// Token: 0x17000558 RID: 1368
	// (get) Token: 0x060017BA RID: 6074 RVA: 0x000EBEFC File Offset: 0x000EA0FC
	public static string ServerDisconnetProfileLoadError
	{
		get
		{
			return Language.sL.ServerDisconnetProfileLoadError;
		}
	}

	// Token: 0x17000559 RID: 1369
	// (get) Token: 0x060017BB RID: 6075 RVA: 0x000EBF08 File Offset: 0x000EA108
	public static string ServerDisconnetKeepAliveError
	{
		get
		{
			return Language.sL.ServerDisconnetKeepaliveError;
		}
	}

	// Token: 0x1700055A RID: 1370
	// (get) Token: 0x060017BC RID: 6076 RVA: 0x000EBF14 File Offset: 0x000EA114
	public static string ProjectNews
	{
		get
		{
			return Language.sL.ProjectNews;
		}
	}

	// Token: 0x1700055B RID: 1371
	// (get) Token: 0x060017BD RID: 6077 RVA: 0x000EBF20 File Offset: 0x000EA120
	public static string BadlyFinishedBoy
	{
		get
		{
			return Language.sL.BadlyFinishedBoy;
		}
	}

	// Token: 0x1700055C RID: 1372
	// (get) Token: 0x060017BE RID: 6078 RVA: 0x000EBF2C File Offset: 0x000EA12C
	public static string ExittingFromServer
	{
		get
		{
			return Language.sL.ExittingFromServer;
		}
	}

	// Token: 0x1700055D RID: 1373
	// (get) Token: 0x060017BF RID: 6079 RVA: 0x000EBF38 File Offset: 0x000EA138
	public static string WrongPassword
	{
		get
		{
			return Language.sL.WrongPassword;
		}
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x060017C0 RID: 6080 RVA: 0x000EBF44 File Offset: 0x000EA144
	public static string YouAreAlreadyAtServer
	{
		get
		{
			return Language.sL.YouAreAlreadyAtServer;
		}
	}

	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x060017C1 RID: 6081 RVA: 0x000EBF50 File Offset: 0x000EA150
	public static string ClientNotMatchTheServerVersion
	{
		get
		{
			return Language.sL.ClientNotMatchTheServerVersion;
		}
	}

	// Token: 0x17000560 RID: 1376
	// (get) Token: 0x060017C2 RID: 6082 RVA: 0x000EBF5C File Offset: 0x000EA15C
	public static string ConnetionDropped
	{
		get
		{
			return Language.sL.ConnetionDropped;
		}
	}

	// Token: 0x17000561 RID: 1377
	// (get) Token: 0x060017C3 RID: 6083 RVA: 0x000EBF68 File Offset: 0x000EA168
	public static string ServerForciblyClosed
	{
		get
		{
			return Language.sL.ServerForciblyClosed;
		}
	}

	// Token: 0x17000562 RID: 1378
	// (get) Token: 0x060017C4 RID: 6084 RVA: 0x000EBF74 File Offset: 0x000EA174
	public static string VotedFor
	{
		get
		{
			return Language.sL.VotedFor;
		}
	}

	// Token: 0x17000563 RID: 1379
	// (get) Token: 0x060017C5 RID: 6085 RVA: 0x000EBF80 File Offset: 0x000EA180
	public static string SuspectedUser
	{
		get
		{
			return Language.sL.SuspectedUser;
		}
	}

	// Token: 0x17000564 RID: 1380
	// (get) Token: 0x060017C6 RID: 6086 RVA: 0x000EBF8C File Offset: 0x000EA18C
	public static string PromoCodeAlreadyActivated
	{
		get
		{
			return Language.sL.PromoCodeAlreadyActivated;
		}
	}

	// Token: 0x17000565 RID: 1381
	// (get) Token: 0x060017C7 RID: 6087 RVA: 0x000EBF98 File Offset: 0x000EA198
	public static string PromoObsolete
	{
		get
		{
			return Language.sL.PromoObsolete;
		}
	}

	// Token: 0x17000566 RID: 1382
	// (get) Token: 0x060017C8 RID: 6088 RVA: 0x000EBFA4 File Offset: 0x000EA1A4
	public static string PromoCodeAlreadyActivatedThisMember
	{
		get
		{
			return Language.sL.PromoCodeAlreadyActivatedThisMember;
		}
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x060017C9 RID: 6089 RVA: 0x000EBFB0 File Offset: 0x000EA1B0
	public static string PromoUnknownCode
	{
		get
		{
			return Language.sL.PromoUnknownCode;
		}
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x060017CA RID: 6090 RVA: 0x000EBFBC File Offset: 0x000EA1BC
	public static string PromoUnknownError
	{
		get
		{
			return Language.sL.PromoUnknownError;
		}
	}

	// Token: 0x17000569 RID: 1385
	// (get) Token: 0x060017CB RID: 6091 RVA: 0x000EBFC8 File Offset: 0x000EA1C8
	public static string PromoErrorActivation
	{
		get
		{
			return Language.sL.PromoErrorActivation;
		}
	}

	// Token: 0x1700056A RID: 1386
	// (get) Token: 0x060017CC RID: 6092 RVA: 0x000EBFD4 File Offset: 0x000EA1D4
	public static string PromoActivated
	{
		get
		{
			return Language.sL.PromoActivated;
		}
	}

	// Token: 0x1700056B RID: 1387
	// (get) Token: 0x060017CD RID: 6093 RVA: 0x000EBFE0 File Offset: 0x000EA1E0
	public static string ProcessingRequest
	{
		get
		{
			return Language.sL.ProcessingRequest;
		}
	}

	// Token: 0x1700056C RID: 1388
	// (get) Token: 0x060017CE RID: 6094 RVA: 0x000EBFEC File Offset: 0x000EA1EC
	public static string FundsDelivery
	{
		get
		{
			return Language.sL.FundsDelivery;
		}
	}

	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x060017CF RID: 6095 RVA: 0x000EBFF8 File Offset: 0x000EA1F8
	public static string FundsDeliveryFailed
	{
		get
		{
			return Language.sL.FundsDeliveryFailed;
		}
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x060017D0 RID: 6096 RVA: 0x000EC004 File Offset: 0x000EA204
	public static string ClanTransactionLoading
	{
		get
		{
			return Language.sL.ClanTransactionLoading;
		}
	}

	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x060017D1 RID: 6097 RVA: 0x000EC010 File Offset: 0x000EA210
	public static string ClanTransactionLoadingFailed
	{
		get
		{
			return Language.sL.ClanTransactionLoadingFailed;
		}
	}

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x060017D2 RID: 6098 RVA: 0x000EC01C File Offset: 0x000EA21C
	public static string BuyKit
	{
		get
		{
			return Language.sL.BuyKit;
		}
	}

	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x060017D3 RID: 6099 RVA: 0x000EC028 File Offset: 0x000EA228
	public static string BuyKitProcessing
	{
		get
		{
			return Language.sL.BuyKitProcessing;
		}
	}

	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x060017D4 RID: 6100 RVA: 0x000EC034 File Offset: 0x000EA234
	public static string KitDelivered
	{
		get
		{
			return Language.sL.KitDelivered;
		}
	}

	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x060017D5 RID: 6101 RVA: 0x000EC040 File Offset: 0x000EA240
	public static string BuySet
	{
		get
		{
			return Language.sL.BuySet;
		}
	}

	// Token: 0x17000574 RID: 1396
	// (get) Token: 0x060017D6 RID: 6102 RVA: 0x000EC04C File Offset: 0x000EA24C
	public static string BuyBox
	{
		get
		{
			return Language.sL.BuyBox;
		}
	}

	// Token: 0x17000575 RID: 1397
	// (get) Token: 0x060017D7 RID: 6103 RVA: 0x000EC058 File Offset: 0x000EA258
	public static string BuyBoxRequest
	{
		get
		{
			return Language.sL.BuyBoxRequest;
		}
	}

	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x060017D8 RID: 6104 RVA: 0x000EC064 File Offset: 0x000EA264
	public static string BuyBoxProcessing
	{
		get
		{
			return Language.sL.BuyBoxProcessing;
		}
	}

	// Token: 0x17000577 RID: 1399
	// (get) Token: 0x060017D9 RID: 6105 RVA: 0x000EC070 File Offset: 0x000EA270
	public static string BoxDelivered
	{
		get
		{
			return Language.sL.BoxDelivered;
		}
	}

	// Token: 0x17000578 RID: 1400
	// (get) Token: 0x060017DA RID: 6106 RVA: 0x000EC07C File Offset: 0x000EA27C
	public static string BuyBoxAttention
	{
		get
		{
			return Language.sL.BuyBoxAttention;
		}
	}

	// Token: 0x17000579 RID: 1401
	// (get) Token: 0x060017DB RID: 6107 RVA: 0x000EC088 File Offset: 0x000EA288
	public static string BuyNick
	{
		get
		{
			return Language.sL.BuyNick;
		}
	}

	// Token: 0x1700057A RID: 1402
	// (get) Token: 0x060017DC RID: 6108 RVA: 0x000EC094 File Offset: 0x000EA294
	public static string BuyNickColor
	{
		get
		{
			return Language.sL.BuyNickColor;
		}
	}

	// Token: 0x1700057B RID: 1403
	// (get) Token: 0x060017DD RID: 6109 RVA: 0x000EC0A0 File Offset: 0x000EA2A0
	public static string BuyNickProcessing
	{
		get
		{
			return Language.sL.BuyNickProcessing;
		}
	}

	// Token: 0x1700057C RID: 1404
	// (get) Token: 0x060017DE RID: 6110 RVA: 0x000EC0AC File Offset: 0x000EA2AC
	public static string BuyNickColorProcessing
	{
		get
		{
			return Language.sL.BuyNickColorProcessing;
		}
	}

	// Token: 0x1700057D RID: 1405
	// (get) Token: 0x060017DF RID: 6111 RVA: 0x000EC0B8 File Offset: 0x000EA2B8
	public static string NickDelivered
	{
		get
		{
			return Language.sL.NickDelivered;
		}
	}

	// Token: 0x1700057E RID: 1406
	// (get) Token: 0x060017E0 RID: 6112 RVA: 0x000EC0C4 File Offset: 0x000EA2C4
	public static string CrrierBonus
	{
		get
		{
			return Language.sL.CrrierBonus;
		}
	}

	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x060017E1 RID: 6113 RVA: 0x000EC0D0 File Offset: 0x000EA2D0
	public static string GetLevel0
	{
		get
		{
			return Language.sL.GetLevel0;
		}
	}

	// Token: 0x17000580 RID: 1408
	// (get) Token: 0x060017E2 RID: 6114 RVA: 0x000EC0DC File Offset: 0x000EA2DC
	public static string GetLevel1
	{
		get
		{
			return Language.sL.GetLevel1;
		}
	}

	// Token: 0x17000581 RID: 1409
	// (get) Token: 0x060017E3 RID: 6115 RVA: 0x000EC0E8 File Offset: 0x000EA2E8
	public static string Modification
	{
		get
		{
			return Language.sL.Modification;
		}
	}

	// Token: 0x17000582 RID: 1410
	// (get) Token: 0x060017E4 RID: 6116 RVA: 0x000EC0F4 File Offset: 0x000EA2F4
	public static string ModificationProcessing
	{
		get
		{
			return Language.sL.ModificationProcessing;
		}
	}

	// Token: 0x17000583 RID: 1411
	// (get) Token: 0x060017E5 RID: 6117 RVA: 0x000EC100 File Offset: 0x000EA300
	public static string Repair
	{
		get
		{
			return Language.sL.Repair;
		}
	}

	// Token: 0x17000584 RID: 1412
	// (get) Token: 0x060017E6 RID: 6118 RVA: 0x000EC10C File Offset: 0x000EA30C
	public static string EquipmentRepaired
	{
		get
		{
			return Language.sL.EquipmentRepaired;
		}
	}

	// Token: 0x17000585 RID: 1413
	// (get) Token: 0x060017E7 RID: 6119 RVA: 0x000EC118 File Offset: 0x000EA318
	public static string RepairFailure
	{
		get
		{
			return Language.sL.RepairFailure;
		}
	}

	// Token: 0x17000586 RID: 1414
	// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000EC124 File Offset: 0x000EA324
	public static string RepairProcessing
	{
		get
		{
			return Language.sL.RepairProcessing;
		}
	}

	// Token: 0x17000587 RID: 1415
	// (get) Token: 0x060017E9 RID: 6121 RVA: 0x000EC130 File Offset: 0x000EA330
	public static string ServerLoadOnFail0
	{
		get
		{
			return Language.sL.ServerLoadOnFail0;
		}
	}

	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x060017EA RID: 6122 RVA: 0x000EC13C File Offset: 0x000EA33C
	public static string ServerLoadOnFail1
	{
		get
		{
			return Language.sL.ServerLoadOnFail1;
		}
	}

	// Token: 0x17000589 RID: 1417
	// (get) Token: 0x060017EB RID: 6123 RVA: 0x000EC148 File Offset: 0x000EA348
	public static string ServerLoadOnFail2
	{
		get
		{
			return Language.sL.ServerLoadOnFail2;
		}
	}

	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x060017EC RID: 6124 RVA: 0x000EC154 File Offset: 0x000EA354
	public static string ServerLoadOnFail3
	{
		get
		{
			return Language.sL.ServerLoadOnFail3;
		}
	}

	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x060017ED RID: 6125 RVA: 0x000EC160 File Offset: 0x000EA360
	public static string ServerLoadOnFail4
	{
		get
		{
			return Language.sL.ServerLoadOnFail4;
		}
	}

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x060017EE RID: 6126 RVA: 0x000EC16C File Offset: 0x000EA36C
	public static string DailyBonus
	{
		get
		{
			return Language.sL.DailyBonus;
		}
	}

	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x060017EF RID: 6127 RVA: 0x000EC178 File Offset: 0x000EA378
	public static string StatsOneDone
	{
		get
		{
			return Language.sL.StatsOneDone;
		}
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x060017F0 RID: 6128 RVA: 0x000EC184 File Offset: 0x000EA384
	public static string StatsMinusOne
	{
		get
		{
			return Language.sL.StatsMinusOne;
		}
	}

	// Token: 0x1700058F RID: 1423
	// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000EC190 File Offset: 0x000EA390
	public static string StatsThisOneDone
	{
		get
		{
			return Language.sL.StatsThisOneDone;
		}
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x060017F2 RID: 6130 RVA: 0x000EC19C File Offset: 0x000EA39C
	public static string SUITE
	{
		get
		{
			return Language.sL.SUITE;
		}
	}

	// Token: 0x17000591 RID: 1425
	// (get) Token: 0x060017F3 RID: 6131 RVA: 0x000EC1A8 File Offset: 0x000EA3A8
	public static string WTaskNotCount
	{
		get
		{
			return Language.sL.WTaskNotCount;
		}
	}

	// Token: 0x17000592 RID: 1426
	// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000EC1B4 File Offset: 0x000EA3B4
	public static string[] ClassName
	{
		get
		{
			return Language.sL.ClassName;
		}
	}

	// Token: 0x17000593 RID: 1427
	// (get) Token: 0x060017F5 RID: 6133 RVA: 0x000EC1C0 File Offset: 0x000EA3C0
	public static string HGUnlock
	{
		get
		{
			return Language.sL.HGUnlock;
		}
	}

	// Token: 0x17000594 RID: 1428
	// (get) Token: 0x060017F6 RID: 6134 RVA: 0x000EC1CC File Offset: 0x000EA3CC
	public static string HGUnlockQuestion
	{
		get
		{
			return Language.sL.HGUnlockQuestion;
		}
	}

	// Token: 0x17000595 RID: 1429
	// (get) Token: 0x060017F7 RID: 6135 RVA: 0x000EC1D8 File Offset: 0x000EA3D8
	public static string RepairQuestion
	{
		get
		{
			return Language.sL.RepairQuestion;
		}
	}

	// Token: 0x17000596 RID: 1430
	// (get) Token: 0x060017F8 RID: 6136 RVA: 0x000EC1E4 File Offset: 0x000EA3E4
	public static string HGState
	{
		get
		{
			return Language.sL.HGState;
		}
	}

	// Token: 0x17000597 RID: 1431
	// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000EC1F0 File Offset: 0x000EA3F0
	public static string HGWeaponBrokenNeedRepair
	{
		get
		{
			return Language.sL.HGWeaponBrokenNeedRepair;
		}
	}

	// Token: 0x17000598 RID: 1432
	// (get) Token: 0x060017FA RID: 6138 RVA: 0x000EC1FC File Offset: 0x000EA3FC
	public static string HGIndestructible
	{
		get
		{
			return Language.sL.HGIndestructible;
		}
	}

	// Token: 0x17000599 RID: 1433
	// (get) Token: 0x060017FB RID: 6139 RVA: 0x000EC208 File Offset: 0x000EA408
	public static string HGPayQuestion
	{
		get
		{
			return Language.sL.HGPayQuestion;
		}
	}

	// Token: 0x1700059A RID: 1434
	// (get) Token: 0x060017FC RID: 6140 RVA: 0x000EC214 File Offset: 0x000EA414
	public static string HGRent
	{
		get
		{
			return Language.sL.HGRent;
		}
	}

	// Token: 0x1700059B RID: 1435
	// (get) Token: 0x060017FD RID: 6141 RVA: 0x000EC220 File Offset: 0x000EA420
	public static string HGRentQuestion
	{
		get
		{
			return Language.sL.HGRentQuestion;
		}
	}

	// Token: 0x1700059C RID: 1436
	// (get) Token: 0x060017FE RID: 6142 RVA: 0x000EC22C File Offset: 0x000EA42C
	public static string HGWeapon
	{
		get
		{
			return Language.sL.HGWeapon;
		}
	}

	// Token: 0x1700059D RID: 1437
	// (get) Token: 0x060017FF RID: 6143 RVA: 0x000EC238 File Offset: 0x000EA438
	public static string HGPremiumBuy
	{
		get
		{
			return Language.sL.HGPremiumBuy;
		}
	}

	// Token: 0x1700059E RID: 1438
	// (get) Token: 0x06001800 RID: 6144 RVA: 0x000EC244 File Offset: 0x000EA444
	public static string HGBuyQuestion
	{
		get
		{
			return Language.sL.HGBuyQuestion;
		}
	}

	// Token: 0x1700059F RID: 1439
	// (get) Token: 0x06001801 RID: 6145 RVA: 0x000EC250 File Offset: 0x000EA450
	public static string ForeverNormal
	{
		get
		{
			return Language.sL.ForeverNormal;
		}
	}

	// Token: 0x170005A0 RID: 1440
	// (get) Token: 0x06001802 RID: 6146 RVA: 0x000EC25C File Offset: 0x000EA45C
	public static string HGAvaliable
	{
		get
		{
			return Language.sL.HGAvaliable;
		}
	}

	// Token: 0x170005A1 RID: 1441
	// (get) Token: 0x06001803 RID: 6147 RVA: 0x000EC268 File Offset: 0x000EA468
	public static string HGNotAvaliable
	{
		get
		{
			return Language.sL.HGNotAvaliable;
		}
	}

	// Token: 0x170005A2 RID: 1442
	// (get) Token: 0x06001804 RID: 6148 RVA: 0x000EC274 File Offset: 0x000EA474
	public static string[] MHGDescr
	{
		get
		{
			return Language.sL.MHGDescr;
		}
	}

	// Token: 0x170005A3 RID: 1443
	// (get) Token: 0x06001805 RID: 6149 RVA: 0x000EC280 File Offset: 0x000EA480
	public static string MHGHelp
	{
		get
		{
			return Language.sL.MHGHelp;
		}
	}

	// Token: 0x170005A4 RID: 1444
	// (get) Token: 0x06001806 RID: 6150 RVA: 0x000EC28C File Offset: 0x000EA48C
	public static string MRYourResult
	{
		get
		{
			return Language.sL.MRYourResult;
		}
	}

	// Token: 0x170005A5 RID: 1445
	// (get) Token: 0x06001807 RID: 6151 RVA: 0x000EC298 File Offset: 0x000EA498
	public static string MRBestResult
	{
		get
		{
			return Language.sL.MRBestResult;
		}
	}

	// Token: 0x170005A6 RID: 1446
	// (get) Token: 0x06001808 RID: 6152 RVA: 0x000EC2A4 File Offset: 0x000EA4A4
	public static string MRCreditsForProgress
	{
		get
		{
			return Language.sL.MRCreditsForProgress;
		}
	}

	// Token: 0x170005A7 RID: 1447
	// (get) Token: 0x06001809 RID: 6153 RVA: 0x000EC2B0 File Offset: 0x000EA4B0
	public static string MRExpRate
	{
		get
		{
			return Language.sL.MRExpRate;
		}
	}

	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x0600180A RID: 6154 RVA: 0x000EC2BC File Offset: 0x000EA4BC
	public static string MRSkill
	{
		get
		{
			return Language.sL.MRSkill;
		}
	}

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x0600180B RID: 6155 RVA: 0x000EC2C8 File Offset: 0x000EA4C8
	public static string MRDoubleExp
	{
		get
		{
			return Language.sL.MRDoubleExp;
		}
	}

	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x0600180C RID: 6156 RVA: 0x000EC2D4 File Offset: 0x000EA4D4
	public static string MRPlayersTax
	{
		get
		{
			return Language.sL.MRPlayersTax;
		}
	}

	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x0600180D RID: 6157 RVA: 0x000EC2E0 File Offset: 0x000EA4E0
	public static string MRNightCredits
	{
		get
		{
			return Language.sL.MRNightCredits;
		}
	}

	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x0600180E RID: 6158 RVA: 0x000EC2EC File Offset: 0x000EA4EC
	public static string MRNightExp
	{
		get
		{
			return Language.sL.MRNightExp;
		}
	}

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x0600180F RID: 6159 RVA: 0x000EC2F8 File Offset: 0x000EA4F8
	public static string MRSPSpend
	{
		get
		{
			return Language.sL.MRSPSpend;
		}
	}

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x06001810 RID: 6160 RVA: 0x000EC304 File Offset: 0x000EA504
	public static string MRMPSpend
	{
		get
		{
			return Language.sL.MRMPSpend;
		}
	}

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x06001811 RID: 6161 RVA: 0x000EC310 File Offset: 0x000EA510
	public static string MRMatchBonus
	{
		get
		{
			return Language.sL.MRMatchBonus;
		}
	}

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x06001812 RID: 6162 RVA: 0x000EC31C File Offset: 0x000EA51C
	public static string MRClanExpDep
	{
		get
		{
			return Language.sL.MRClanExpDep;
		}
	}

	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x06001813 RID: 6163 RVA: 0x000EC328 File Offset: 0x000EA528
	public static string MRClanCrDep
	{
		get
		{
			return Language.sL.MRClanCrDep;
		}
	}

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x06001814 RID: 6164 RVA: 0x000EC334 File Offset: 0x000EA534
	public static string MRTotal
	{
		get
		{
			return Language.sL.MRTotal;
		}
	}

	// Token: 0x170005B3 RID: 1459
	// (get) Token: 0x06001815 RID: 6165 RVA: 0x000EC340 File Offset: 0x000EA540
	public static string MREarnExp
	{
		get
		{
			return Language.sL.MREarnExp;
		}
	}

	// Token: 0x170005B4 RID: 1460
	// (get) Token: 0x06001816 RID: 6166 RVA: 0x000EC34C File Offset: 0x000EA54C
	public static string MRBestResultForMatch
	{
		get
		{
			return Language.sL.MRBestResultForMatch;
		}
	}

	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x06001817 RID: 6167 RVA: 0x000EC358 File Offset: 0x000EA558
	public static string MRNoAchievements
	{
		get
		{
			return Language.sL.MRNoAchievements;
		}
	}

	// Token: 0x170005B6 RID: 1462
	// (get) Token: 0x06001818 RID: 6168 RVA: 0x000EC364 File Offset: 0x000EA564
	public static string MRBestPlayer
	{
		get
		{
			return Language.sL.MRBestPlayer;
		}
	}

	// Token: 0x170005B7 RID: 1463
	// (get) Token: 0x06001819 RID: 6169 RVA: 0x000EC370 File Offset: 0x000EA570
	public static string MRWorthPlayer
	{
		get
		{
			return Language.sL.MRWorthPlayer;
		}
	}

	// Token: 0x170005B8 RID: 1464
	// (get) Token: 0x0600181A RID: 6170 RVA: 0x000EC37C File Offset: 0x000EA57C
	public static string MRWin
	{
		get
		{
			return Language.sL.MRWin;
		}
	}

	// Token: 0x170005B9 RID: 1465
	// (get) Token: 0x0600181B RID: 6171 RVA: 0x000EC388 File Offset: 0x000EA588
	public static string MRDraw
	{
		get
		{
			return Language.sL.MRDraw;
		}
	}

	// Token: 0x170005BA RID: 1466
	// (get) Token: 0x0600181C RID: 6172 RVA: 0x000EC394 File Offset: 0x000EA594
	public static string MRMatchResult
	{
		get
		{
			return Language.sL.MRMatchResult;
		}
	}

	// Token: 0x170005BB RID: 1467
	// (get) Token: 0x0600181D RID: 6173 RVA: 0x000EC3A0 File Offset: 0x000EA5A0
	public static string MRGameTime
	{
		get
		{
			return Language.sL.MRGameTime;
		}
	}

	// Token: 0x170005BC RID: 1468
	// (get) Token: 0x0600181E RID: 6174 RVA: 0x000EC3AC File Offset: 0x000EA5AC
	public static string MRKillsTotal
	{
		get
		{
			return Language.sL.MRKillsTotal;
		}
	}

	// Token: 0x170005BD RID: 1469
	// (get) Token: 0x0600181F RID: 6175 RVA: 0x000EC3B8 File Offset: 0x000EA5B8
	public static string MRDeathTotal
	{
		get
		{
			return Language.sL.MRDeathTotal;
		}
	}

	// Token: 0x170005BE RID: 1470
	// (get) Token: 0x06001820 RID: 6176 RVA: 0x000EC3C4 File Offset: 0x000EA5C4
	public static string MainMaxMatchesDescr
	{
		get
		{
			return Language.sL.MainMaxMatchesDescr;
		}
	}

	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x06001821 RID: 6177 RVA: 0x000EC3D0 File Offset: 0x000EA5D0
	public static string RadarBeaconSet
	{
		get
		{
			return Language.sL.RadarBeaconSet;
		}
	}

	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06001822 RID: 6178 RVA: 0x000EC3DC File Offset: 0x000EA5DC
	public static string ServerGUICreatingServer
	{
		get
		{
			return Language.sL.ServerGUICreatingServer;
		}
	}

	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06001823 RID: 6179 RVA: 0x000EC3E8 File Offset: 0x000EA5E8
	public static string ServerGUICreate
	{
		get
		{
			return Language.sL.ServerGUICreate;
		}
	}

	// Token: 0x170005C2 RID: 1474
	// (get) Token: 0x06001824 RID: 6180 RVA: 0x000EC3F4 File Offset: 0x000EA5F4
	public static string ServerGUIName
	{
		get
		{
			return Language.sL.ServerGUIName;
		}
	}

	// Token: 0x170005C3 RID: 1475
	// (get) Token: 0x06001825 RID: 6181 RVA: 0x000EC400 File Offset: 0x000EA600
	public static string ServerGUIPlayersCount
	{
		get
		{
			return Language.sL.ServerGUIPlayersCount;
		}
	}

	// Token: 0x170005C4 RID: 1476
	// (get) Token: 0x06001826 RID: 6182 RVA: 0x000EC40C File Offset: 0x000EA60C
	public static string ServerGUIGameMode
	{
		get
		{
			return Language.sL.ServerGUIGameMode;
		}
	}

	// Token: 0x170005C5 RID: 1477
	// (get) Token: 0x06001827 RID: 6183 RVA: 0x000EC418 File Offset: 0x000EA618
	public static string RadioEmpty0
	{
		get
		{
			return Language.sL.RadioEmpty0;
		}
	}

	// Token: 0x170005C6 RID: 1478
	// (get) Token: 0x06001828 RID: 6184 RVA: 0x000EC424 File Offset: 0x000EA624
	public static string RadioEmpty1
	{
		get
		{
			return Language.sL.RadioEmpty1;
		}
	}

	// Token: 0x170005C7 RID: 1479
	// (get) Token: 0x06001829 RID: 6185 RVA: 0x000EC430 File Offset: 0x000EA630
	public static string RadioStart
	{
		get
		{
			return Language.sL.RadioStart;
		}
	}

	// Token: 0x170005C8 RID: 1480
	// (get) Token: 0x0600182A RID: 6186 RVA: 0x000EC43C File Offset: 0x000EA63C
	public static string RadioStart0
	{
		get
		{
			return Language.sL.RadioStart0;
		}
	}

	// Token: 0x170005C9 RID: 1481
	// (get) Token: 0x0600182B RID: 6187 RVA: 0x000EC448 File Offset: 0x000EA648
	public static string RadioStart1
	{
		get
		{
			return Language.sL.RadioStart1;
		}
	}

	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x0600182C RID: 6188 RVA: 0x000EC454 File Offset: 0x000EA654
	public static string RadioReceived
	{
		get
		{
			return Language.sL.RadioReceived;
		}
	}

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x0600182D RID: 6189 RVA: 0x000EC460 File Offset: 0x000EA660
	public static string RadioReceived0
	{
		get
		{
			return Language.sL.RadioReceived0;
		}
	}

	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x0600182E RID: 6190 RVA: 0x000EC46C File Offset: 0x000EA66C
	public static string RadioReceived1
	{
		get
		{
			return Language.sL.RadioReceived1;
		}
	}

	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x0600182F RID: 6191 RVA: 0x000EC478 File Offset: 0x000EA678
	public static string RadioCover
	{
		get
		{
			return Language.sL.RadioCover;
		}
	}

	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x06001830 RID: 6192 RVA: 0x000EC484 File Offset: 0x000EA684
	public static string RadioCover0
	{
		get
		{
			return Language.sL.RadioCover0;
		}
	}

	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x06001831 RID: 6193 RVA: 0x000EC490 File Offset: 0x000EA690
	public static string RadioCover1
	{
		get
		{
			return Language.sL.RadioCover1;
		}
	}

	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x06001832 RID: 6194 RVA: 0x000EC49C File Offset: 0x000EA69C
	public static string RadioAttention
	{
		get
		{
			return Language.sL.RadioAttention;
		}
	}

	// Token: 0x170005D1 RID: 1489
	// (get) Token: 0x06001833 RID: 6195 RVA: 0x000EC4A8 File Offset: 0x000EA6A8
	public static string RadioAttention0
	{
		get
		{
			return Language.sL.RadioAttention0;
		}
	}

	// Token: 0x170005D2 RID: 1490
	// (get) Token: 0x06001834 RID: 6196 RVA: 0x000EC4B4 File Offset: 0x000EA6B4
	public static string RadioAttention1
	{
		get
		{
			return Language.sL.RadioAttention1;
		}
	}

	// Token: 0x170005D3 RID: 1491
	// (get) Token: 0x06001835 RID: 6197 RVA: 0x000EC4C0 File Offset: 0x000EA6C0
	public static string RadioClear
	{
		get
		{
			return Language.sL.RadioClear;
		}
	}

	// Token: 0x170005D4 RID: 1492
	// (get) Token: 0x06001836 RID: 6198 RVA: 0x000EC4CC File Offset: 0x000EA6CC
	public static string RadioClear0
	{
		get
		{
			return Language.sL.RadioClear0;
		}
	}

	// Token: 0x170005D5 RID: 1493
	// (get) Token: 0x06001837 RID: 6199 RVA: 0x000EC4D8 File Offset: 0x000EA6D8
	public static string RadioClear1
	{
		get
		{
			return Language.sL.RadioClear1;
		}
	}

	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x06001838 RID: 6200 RVA: 0x000EC4E4 File Offset: 0x000EA6E4
	public static string RadioStop
	{
		get
		{
			return Language.sL.RadioStop;
		}
	}

	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x06001839 RID: 6201 RVA: 0x000EC4F0 File Offset: 0x000EA6F0
	public static string RadioStop0
	{
		get
		{
			return Language.sL.RadioStop0;
		}
	}

	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x0600183A RID: 6202 RVA: 0x000EC4FC File Offset: 0x000EA6FC
	public static string RadioStop1
	{
		get
		{
			return Language.sL.RadioStop1;
		}
	}

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x0600183B RID: 6203 RVA: 0x000EC508 File Offset: 0x000EA708
	public static string RadioGood
	{
		get
		{
			return Language.sL.RadioGood;
		}
	}

	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x0600183C RID: 6204 RVA: 0x000EC514 File Offset: 0x000EA714
	public static string RadioGood0
	{
		get
		{
			return Language.sL.RadioGood0;
		}
	}

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x0600183D RID: 6205 RVA: 0x000EC520 File Offset: 0x000EA720
	public static string RadioGood1
	{
		get
		{
			return Language.sL.RadioGood1;
		}
	}

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x0600183E RID: 6206 RVA: 0x000EC52C File Offset: 0x000EA72C
	public static string RadioFollowMe
	{
		get
		{
			return Language.sL.RadioFollowMe;
		}
	}

	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x0600183F RID: 6207 RVA: 0x000EC538 File Offset: 0x000EA738
	public static string RadioFollowMe0
	{
		get
		{
			return Language.sL.RadioFollowMe0;
		}
	}

	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x06001840 RID: 6208 RVA: 0x000EC544 File Offset: 0x000EA744
	public static string RadioFollowMe1
	{
		get
		{
			return Language.sL.RadioFollowMe1;
		}
	}

	// Token: 0x170005DF RID: 1503
	// (get) Token: 0x06001841 RID: 6209 RVA: 0x000EC550 File Offset: 0x000EA750
	public static string RadioHelp
	{
		get
		{
			return Language.sL.RadioHelp;
		}
	}

	// Token: 0x170005E0 RID: 1504
	// (get) Token: 0x06001842 RID: 6210 RVA: 0x000EC55C File Offset: 0x000EA75C
	public static string RadioHelp0
	{
		get
		{
			return Language.sL.RadioHelp0;
		}
	}

	// Token: 0x170005E1 RID: 1505
	// (get) Token: 0x06001843 RID: 6211 RVA: 0x000EC568 File Offset: 0x000EA768
	public static string RadioHelp1
	{
		get
		{
			return Language.sL.RadioHelp1;
		}
	}

	// Token: 0x170005E2 RID: 1506
	// (get) Token: 0x06001844 RID: 6212 RVA: 0x000EC574 File Offset: 0x000EA774
	public static string RadioCancel
	{
		get
		{
			return Language.sL.RadioCancel;
		}
	}

	// Token: 0x170005E3 RID: 1507
	// (get) Token: 0x06001845 RID: 6213 RVA: 0x000EC580 File Offset: 0x000EA780
	public static string RadioCancel0
	{
		get
		{
			return Language.sL.RadioCancel0;
		}
	}

	// Token: 0x170005E4 RID: 1508
	// (get) Token: 0x06001846 RID: 6214 RVA: 0x000EC58C File Offset: 0x000EA78C
	public static string RadioCancel1
	{
		get
		{
			return Language.sL.RadioCancel1;
		}
	}

	// Token: 0x170005E5 RID: 1509
	// (get) Token: 0x06001847 RID: 6215 RVA: 0x000EC598 File Offset: 0x000EA798
	public static string MatchExp
	{
		get
		{
			return Language.sL.MatchExp;
		}
	}

	// Token: 0x170005E6 RID: 1510
	// (get) Token: 0x06001848 RID: 6216 RVA: 0x000EC5A4 File Offset: 0x000EA7A4
	public static string TeamWin
	{
		get
		{
			return Language.sL.TeamWin;
		}
	}

	// Token: 0x170005E7 RID: 1511
	// (get) Token: 0x06001849 RID: 6217 RVA: 0x000EC5B0 File Offset: 0x000EA7B0
	public static string Accuracy
	{
		get
		{
			return Language.sL.Accuracy;
		}
	}

	// Token: 0x170005E8 RID: 1512
	// (get) Token: 0x0600184A RID: 6218 RVA: 0x000EC5BC File Offset: 0x000EA7BC
	public static string Impact
	{
		get
		{
			return Language.sL.Impact;
		}
	}

	// Token: 0x170005E9 RID: 1513
	// (get) Token: 0x0600184B RID: 6219 RVA: 0x000EC5C8 File Offset: 0x000EA7C8
	public static string Damage
	{
		get
		{
			return Language.sL.Damage;
		}
	}

	// Token: 0x170005EA RID: 1514
	// (get) Token: 0x0600184C RID: 6220 RVA: 0x000EC5D4 File Offset: 0x000EA7D4
	public static string FireRate
	{
		get
		{
			return Language.sL.FireRate;
		}
	}

	// Token: 0x170005EB RID: 1515
	// (get) Token: 0x0600184D RID: 6221 RVA: 0x000EC5E0 File Offset: 0x000EA7E0
	public static string Mobility
	{
		get
		{
			return Language.sL.Mobility;
		}
	}

	// Token: 0x170005EC RID: 1516
	// (get) Token: 0x0600184E RID: 6222 RVA: 0x000EC5EC File Offset: 0x000EA7EC
	public static string ReloadRate
	{
		get
		{
			return Language.sL.ReloadRate;
		}
	}

	// Token: 0x170005ED RID: 1517
	// (get) Token: 0x0600184F RID: 6223 RVA: 0x000EC5F8 File Offset: 0x000EA7F8
	public static string Ammunition
	{
		get
		{
			return Language.sL.Ammunition;
		}
	}

	// Token: 0x170005EE RID: 1518
	// (get) Token: 0x06001850 RID: 6224 RVA: 0x000EC604 File Offset: 0x000EA804
	public static string Cartridge
	{
		get
		{
			return Language.sL.Cartridge;
		}
	}

	// Token: 0x170005EF RID: 1519
	// (get) Token: 0x06001851 RID: 6225 RVA: 0x000EC610 File Offset: 0x000EA810
	public static string Penetration
	{
		get
		{
			return Language.sL.Penetration;
		}
	}

	// Token: 0x170005F0 RID: 1520
	// (get) Token: 0x06001852 RID: 6226 RVA: 0x000EC61C File Offset: 0x000EA81C
	public static string Objective
	{
		get
		{
			return Language.sL.Objective;
		}
	}

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x06001853 RID: 6227 RVA: 0x000EC628 File Offset: 0x000EA828
	public static string EffectiveDistance
	{
		get
		{
			return Language.sL.EffectiveDistance;
		}
	}

	// Token: 0x170005F2 RID: 1522
	// (get) Token: 0x06001854 RID: 6228 RVA: 0x000EC634 File Offset: 0x000EA834
	public static string ShotGrouping
	{
		get
		{
			return Language.sL.ShotGrouping;
		}
	}

	// Token: 0x170005F3 RID: 1523
	// (get) Token: 0x06001855 RID: 6229 RVA: 0x000EC640 File Offset: 0x000EA840
	public static string HearDistance
	{
		get
		{
			return Language.sL.HearDistance;
		}
	}

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x06001856 RID: 6230 RVA: 0x000EC64C File Offset: 0x000EA84C
	public static string MainGUIFreeChooseWeapon
	{
		get
		{
			return Language.sL.MainGUIFreeChooseWeapon;
		}
	}

	// Token: 0x170005F5 RID: 1525
	// (get) Token: 0x06001857 RID: 6231 RVA: 0x000EC658 File Offset: 0x000EA858
	public static string MainGUIBlocked
	{
		get
		{
			return Language.sL.MainGUIBlocked;
		}
	}

	// Token: 0x170005F6 RID: 1526
	// (get) Token: 0x06001858 RID: 6232 RVA: 0x000EC664 File Offset: 0x000EA864
	public static string MainGUIChooseSavedSets
	{
		get
		{
			return Language.sL.MainGUIChooseSavedSets;
		}
	}

	// Token: 0x170005F7 RID: 1527
	// (get) Token: 0x06001859 RID: 6233 RVA: 0x000EC670 File Offset: 0x000EA870
	public static string Unlock
	{
		get
		{
			return Language.sL.Unlock;
		}
	}

	// Token: 0x170005F8 RID: 1528
	// (get) Token: 0x0600185A RID: 6234 RVA: 0x000EC67C File Offset: 0x000EA87C
	public static string WeaponView
	{
		get
		{
			return Language.sL.WeaponView;
		}
	}

	// Token: 0x170005F9 RID: 1529
	// (get) Token: 0x0600185B RID: 6235 RVA: 0x000EC688 File Offset: 0x000EA888
	public static string Spectators
	{
		get
		{
			return Language.sL.Spectators;
		}
	}

	// Token: 0x170005FA RID: 1530
	// (get) Token: 0x0600185C RID: 6236 RVA: 0x000EC694 File Offset: 0x000EA894
	public static string InviteFriends
	{
		get
		{
			return Language.sL.InviteFriends;
		}
	}

	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x0600185D RID: 6237 RVA: 0x000EC6A0 File Offset: 0x000EA8A0
	public static string YouMadeDoubleHeadshot
	{
		get
		{
			return Language.sL.YouMadeDoubleHeadshot;
		}
	}

	// Token: 0x170005FC RID: 1532
	// (get) Token: 0x0600185E RID: 6238 RVA: 0x000EC6AC File Offset: 0x000EA8AC
	public static string YouMadeTripleKill
	{
		get
		{
			return Language.sL.YouMadeTripleKill;
		}
	}

	// Token: 0x170005FD RID: 1533
	// (get) Token: 0x0600185F RID: 6239 RVA: 0x000EC6B8 File Offset: 0x000EA8B8
	public static string YouMadeQuadKill
	{
		get
		{
			return Language.sL.YouMadeQuadKill;
		}
	}

	// Token: 0x170005FE RID: 1534
	// (get) Token: 0x06001860 RID: 6240 RVA: 0x000EC6C4 File Offset: 0x000EA8C4
	public static string YouMadeRageKill
	{
		get
		{
			return Language.sL.YouMadeRageKill;
		}
	}

	// Token: 0x170005FF RID: 1535
	// (get) Token: 0x06001861 RID: 6241 RVA: 0x000EC6D0 File Offset: 0x000EA8D0
	public static string YouMadeStormKill
	{
		get
		{
			return Language.sL.YouMadeStormKill;
		}
	}

	// Token: 0x17000600 RID: 1536
	// (get) Token: 0x06001862 RID: 6242 RVA: 0x000EC6DC File Offset: 0x000EA8DC
	public static string YouMadeProKill
	{
		get
		{
			return Language.sL.YouMadeProKill;
		}
	}

	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x06001863 RID: 6243 RVA: 0x000EC6E8 File Offset: 0x000EA8E8
	public static string YouMadeLegendaryKill
	{
		get
		{
			return Language.sL.YouMadeLegendaryKill;
		}
	}

	// Token: 0x17000602 RID: 1538
	// (get) Token: 0x06001864 RID: 6244 RVA: 0x000EC6F4 File Offset: 0x000EA8F4
	public static string[] MHGtabs
	{
		get
		{
			return Language.sL.MHGtabs;
		}
	}

	// Token: 0x17000603 RID: 1539
	// (get) Token: 0x06001865 RID: 6245 RVA: 0x000EC700 File Offset: 0x000EA900
	public static string ChooseDislocate
	{
		get
		{
			return Language.sL.ChooseDislocate;
		}
	}

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x06001866 RID: 6246 RVA: 0x000EC70C File Offset: 0x000EA90C
	public static string Dislocate
	{
		get
		{
			return Language.sL.Dislocate;
		}
	}

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x06001867 RID: 6247 RVA: 0x000EC718 File Offset: 0x000EA918
	public static string SettingsCharacterQuality
	{
		get
		{
			return Language.sL.SettingsCharacterQuality;
		}
	}

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x06001868 RID: 6248 RVA: 0x000EC724 File Offset: 0x000EA924
	public static string Ready
	{
		get
		{
			return Language.sL.Ready;
		}
	}

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x06001869 RID: 6249 RVA: 0x000EC730 File Offset: 0x000EA930
	public static string TutorHintFitstTime
	{
		get
		{
			return Language.sL.TutorHintFitstTime;
		}
	}

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x0600186A RID: 6250 RVA: 0x000EC73C File Offset: 0x000EA93C
	public static string TutorNickname
	{
		get
		{
			return Language.sL.TutorNickname;
		}
	}

	// Token: 0x17000609 RID: 1545
	// (get) Token: 0x0600186B RID: 6251 RVA: 0x000EC748 File Offset: 0x000EA948
	public static string TutorExpBar
	{
		get
		{
			return Language.sL.TutorExpBar;
		}
	}

	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x0600186C RID: 6252 RVA: 0x000EC754 File Offset: 0x000EA954
	public static string TutorContracts
	{
		get
		{
			return Language.sL.TutorContracts;
		}
	}

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x0600186D RID: 6253 RVA: 0x000EC760 File Offset: 0x000EA960
	public static string TutorBalance
	{
		get
		{
			return Language.sL.TutorBalance;
		}
	}

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x0600186E RID: 6254 RVA: 0x000EC76C File Offset: 0x000EA96C
	public static string TutorBuyWeapon
	{
		get
		{
			return Language.sL.TutorBuyWeapon;
		}
	}

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x0600186F RID: 6255 RVA: 0x000EC778 File Offset: 0x000EA978
	public static string TutorConfirmPayment
	{
		get
		{
			return Language.sL.TutorConfirmPayment;
		}
	}

	// Token: 0x1700060E RID: 1550
	// (get) Token: 0x06001870 RID: 6256 RVA: 0x000EC784 File Offset: 0x000EA984
	public static string TutorEquipPrimary
	{
		get
		{
			return Language.sL.TutorEquipPrimary;
		}
	}

	// Token: 0x1700060F RID: 1551
	// (get) Token: 0x06001871 RID: 6257 RVA: 0x000EC790 File Offset: 0x000EA990
	public static string TutorInstallWtask
	{
		get
		{
			return Language.sL.TutorInstallWtask;
		}
	}

	// Token: 0x17000610 RID: 1552
	// (get) Token: 0x06001872 RID: 6258 RVA: 0x000EC79C File Offset: 0x000EA99C
	public static string TutorEquipSecondary
	{
		get
		{
			return Language.sL.TutorEquipSecondary;
		}
	}

	// Token: 0x17000611 RID: 1553
	// (get) Token: 0x06001873 RID: 6259 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
	public static string TutorSaveWeaponKit
	{
		get
		{
			return Language.sL.TutorSaveWeaponKit;
		}
	}

	// Token: 0x17000612 RID: 1554
	// (get) Token: 0x06001874 RID: 6260 RVA: 0x000EC7B4 File Offset: 0x000EA9B4
	public static string TutorSelectedKit
	{
		get
		{
			return Language.sL.TutorSelectedKit;
		}
	}

	// Token: 0x17000613 RID: 1555
	// (get) Token: 0x06001875 RID: 6261 RVA: 0x000EC7C0 File Offset: 0x000EA9C0
	public static string TutorQuickMatchOpen
	{
		get
		{
			return Language.sL.TutorQuickMatchOpen;
		}
	}

	// Token: 0x17000614 RID: 1556
	// (get) Token: 0x06001876 RID: 6262 RVA: 0x000EC7CC File Offset: 0x000EA9CC
	public static string TutorQuickMatchGo
	{
		get
		{
			return Language.sL.TutorQuickMatchGo;
		}
	}

	// Token: 0x17000615 RID: 1557
	// (get) Token: 0x06001877 RID: 6263 RVA: 0x000EC7D8 File Offset: 0x000EA9D8
	public static string TutorFullScreen
	{
		get
		{
			return Language.sL.TutorFullScreen;
		}
	}

	// Token: 0x17000616 RID: 1558
	// (get) Token: 0x06001878 RID: 6264 RVA: 0x000EC7E4 File Offset: 0x000EA9E4
	public static string TutorHeader1
	{
		get
		{
			return Language.sL.TutorHeader1;
		}
	}

	// Token: 0x17000617 RID: 1559
	// (get) Token: 0x06001879 RID: 6265 RVA: 0x000EC7F0 File Offset: 0x000EA9F0
	public static string TutorHeader2
	{
		get
		{
			return Language.sL.TutorHeader2;
		}
	}

	// Token: 0x17000618 RID: 1560
	// (get) Token: 0x0600187A RID: 6266 RVA: 0x000EC7FC File Offset: 0x000EA9FC
	public static string TutorHeader3
	{
		get
		{
			return Language.sL.TutorHeader3;
		}
	}

	// Token: 0x17000619 RID: 1561
	// (get) Token: 0x0600187B RID: 6267 RVA: 0x000EC808 File Offset: 0x000EAA08
	public static string TutorInGameControlHeader
	{
		get
		{
			return Language.sL.TutorInGameControlHeader;
		}
	}

	// Token: 0x1700061A RID: 1562
	// (get) Token: 0x0600187C RID: 6268 RVA: 0x000EC814 File Offset: 0x000EAA14
	public static string TutorInGameWeaponChange
	{
		get
		{
			return Language.sL.TutorInGameWeaponChange;
		}
	}

	// Token: 0x1700061B RID: 1563
	// (get) Token: 0x0600187D RID: 6269 RVA: 0x000EC820 File Offset: 0x000EAA20
	public static string TutorInGameMenu
	{
		get
		{
			return Language.sL.TutorInGameMenu;
		}
	}

	// Token: 0x1700061C RID: 1564
	// (get) Token: 0x0600187E RID: 6270 RVA: 0x000EC82C File Offset: 0x000EAA2C
	public static string TutorInGameFSmode
	{
		get
		{
			return Language.sL.TutorInGameFSmode;
		}
	}

	// Token: 0x1700061D RID: 1565
	// (get) Token: 0x0600187F RID: 6271 RVA: 0x000EC838 File Offset: 0x000EAA38
	public static string TutorInGameWalk
	{
		get
		{
			return Language.sL.TutorInGameWalk;
		}
	}

	// Token: 0x1700061E RID: 1566
	// (get) Token: 0x06001880 RID: 6272 RVA: 0x000EC844 File Offset: 0x000EAA44
	public static string TutorInGameMovement
	{
		get
		{
			return Language.sL.TutorInGameMovement;
		}
	}

	// Token: 0x1700061F RID: 1567
	// (get) Token: 0x06001881 RID: 6273 RVA: 0x000EC850 File Offset: 0x000EAA50
	public static string TutorInGameReload
	{
		get
		{
			return Language.sL.TutorInGameReload;
		}
	}

	// Token: 0x17000620 RID: 1568
	// (get) Token: 0x06001882 RID: 6274 RVA: 0x000EC85C File Offset: 0x000EAA5C
	public static string TutorInGameCrouch
	{
		get
		{
			return Language.sL.TutorInGameCrouch;
		}
	}

	// Token: 0x17000621 RID: 1569
	// (get) Token: 0x06001883 RID: 6275 RVA: 0x000EC868 File Offset: 0x000EAA68
	public static string TutorInGameKnife
	{
		get
		{
			return Language.sL.TutorInGameKnife;
		}
	}

	// Token: 0x17000622 RID: 1570
	// (get) Token: 0x06001884 RID: 6276 RVA: 0x000EC874 File Offset: 0x000EAA74
	public static string TutorInGameJump
	{
		get
		{
			return Language.sL.TutorInGameJump;
		}
	}

	// Token: 0x17000623 RID: 1571
	// (get) Token: 0x06001885 RID: 6277 RVA: 0x000EC880 File Offset: 0x000EAA80
	public static string TutorInGameFire
	{
		get
		{
			return Language.sL.TutorInGameFire;
		}
	}

	// Token: 0x17000624 RID: 1572
	// (get) Token: 0x06001886 RID: 6278 RVA: 0x000EC88C File Offset: 0x000EAA8C
	public static string TutorInGameAim
	{
		get
		{
			return Language.sL.TutorInGameAim;
		}
	}

	// Token: 0x17000625 RID: 1573
	// (get) Token: 0x06001887 RID: 6279 RVA: 0x000EC898 File Offset: 0x000EAA98
	public static string TutorInGameContinue
	{
		get
		{
			return Language.sL.TutorInGameContinue;
		}
	}

	// Token: 0x17000626 RID: 1574
	// (get) Token: 0x06001888 RID: 6280 RVA: 0x000EC8A4 File Offset: 0x000EAAA4
	public static string TutorInGameGameplayHeader
	{
		get
		{
			return Language.sL.TutorInGameGameplayHeader;
		}
	}

	// Token: 0x17000627 RID: 1575
	// (get) Token: 0x06001889 RID: 6281 RVA: 0x000EC8B0 File Offset: 0x000EAAB0
	public static string TutorInGameGameplayHint1_1
	{
		get
		{
			return Language.sL.TutorInGameGameplayHint1_1;
		}
	}

	// Token: 0x17000628 RID: 1576
	// (get) Token: 0x0600188A RID: 6282 RVA: 0x000EC8BC File Offset: 0x000EAABC
	public static string TutorInGameGameplayHint1_2
	{
		get
		{
			return Language.sL.TutorInGameGameplayHint1_2;
		}
	}

	// Token: 0x17000629 RID: 1577
	// (get) Token: 0x0600188B RID: 6283 RVA: 0x000EC8C8 File Offset: 0x000EAAC8
	public static string TutorInGameGameplayHint2
	{
		get
		{
			return Language.sL.TutorInGameGameplayHint2;
		}
	}

	// Token: 0x1700062A RID: 1578
	// (get) Token: 0x0600188C RID: 6284 RVA: 0x000EC8D4 File Offset: 0x000EAAD4
	public static string TutorInGameGameplayHint3
	{
		get
		{
			return Language.sL.TutorInGameGameplayHint3;
		}
	}

	// Token: 0x1700062B RID: 1579
	// (get) Token: 0x0600188D RID: 6285 RVA: 0x000EC8E0 File Offset: 0x000EAAE0
	public static string TutorInGameGameplayHint4
	{
		get
		{
			return Language.sL.TutorInGameGameplayHint4;
		}
	}

	// Token: 0x1700062C RID: 1580
	// (get) Token: 0x0600188E RID: 6286 RVA: 0x000EC8EC File Offset: 0x000EAAEC
	public static string DN
	{
		get
		{
			return Language.sL.DN;
		}
	}

	// Token: 0x1700062D RID: 1581
	// (get) Token: 0x0600188F RID: 6287 RVA: 0x000EC8F8 File Offset: 0x000EAAF8
	public static string Buyed
	{
		get
		{
			return Language.sL.Buyed;
		}
	}

	// Token: 0x1700062E RID: 1582
	// (get) Token: 0x06001890 RID: 6288 RVA: 0x000EC904 File Offset: 0x000EAB04
	public static string Cancel
	{
		get
		{
			return Language.sL.Cancel;
		}
	}

	// Token: 0x1700062F RID: 1583
	// (get) Token: 0x06001891 RID: 6289 RVA: 0x000EC910 File Offset: 0x000EAB10
	public static string FindInFull
	{
		get
		{
			return Language.sL.FindInFull;
		}
	}

	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x06001892 RID: 6290 RVA: 0x000EC91C File Offset: 0x000EAB1C
	public static string ClansInfo
	{
		get
		{
			return Language.sL.ClansInfo;
		}
	}

	// Token: 0x17000631 RID: 1585
	// (get) Token: 0x06001893 RID: 6291 RVA: 0x000EC928 File Offset: 0x000EAB28
	public static string ClansCreate
	{
		get
		{
			return Language.sL.ClansCreate;
		}
	}

	// Token: 0x17000632 RID: 1586
	// (get) Token: 0x06001894 RID: 6292 RVA: 0x000EC934 File Offset: 0x000EAB34
	public static string ClansJoin
	{
		get
		{
			return Language.sL.ClansJoin;
		}
	}

	// Token: 0x17000633 RID: 1587
	// (get) Token: 0x06001895 RID: 6293 RVA: 0x000EC940 File Offset: 0x000EAB40
	public static string ClansWars
	{
		get
		{
			return Language.sL.ClansWars;
		}
	}

	// Token: 0x17000634 RID: 1588
	// (get) Token: 0x06001896 RID: 6294 RVA: 0x000EC94C File Offset: 0x000EAB4C
	public static string ClansManagment
	{
		get
		{
			return Language.sL.ClansManagment;
		}
	}

	// Token: 0x17000635 RID: 1589
	// (get) Token: 0x06001897 RID: 6295 RVA: 0x000EC958 File Offset: 0x000EAB58
	public static string ClansLeveling
	{
		get
		{
			return Language.sL.ClansLeveling;
		}
	}

	// Token: 0x17000636 RID: 1590
	// (get) Token: 0x06001898 RID: 6296 RVA: 0x000EC964 File Offset: 0x000EAB64
	public static string ClansName
	{
		get
		{
			return Language.sL.ClansName;
		}
	}

	// Token: 0x17000637 RID: 1591
	// (get) Token: 0x06001899 RID: 6297 RVA: 0x000EC970 File Offset: 0x000EAB70
	public static string ClansLead
	{
		get
		{
			return Language.sL.ClansLead;
		}
	}

	// Token: 0x17000638 RID: 1592
	// (get) Token: 0x0600189A RID: 6298 RVA: 0x000EC97C File Offset: 0x000EAB7C
	public static string ClansLeadYou
	{
		get
		{
			return Language.sL.ClansLeadYou;
		}
	}

	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x0600189B RID: 6299 RVA: 0x000EC988 File Offset: 0x000EAB88
	public static string ClansStats
	{
		get
		{
			return Language.sL.ClansStats;
		}
	}

	// Token: 0x1700063A RID: 1594
	// (get) Token: 0x0600189C RID: 6300 RVA: 0x000EC994 File Offset: 0x000EAB94
	public static string ClansYourStats
	{
		get
		{
			return Language.sL.ClansYourStats;
		}
	}

	// Token: 0x1700063B RID: 1595
	// (get) Token: 0x0600189D RID: 6301 RVA: 0x000EC9A0 File Offset: 0x000EABA0
	public static string ClansYourContribution
	{
		get
		{
			return Language.sL.ClansYourContribution;
		}
	}

	// Token: 0x1700063C RID: 1596
	// (get) Token: 0x0600189E RID: 6302 RVA: 0x000EC9AC File Offset: 0x000EABAC
	public static string ClansYourContributionHint
	{
		get
		{
			return Language.sL.ClansYourContributionHint;
		}
	}

	// Token: 0x1700063D RID: 1597
	// (get) Token: 0x0600189F RID: 6303 RVA: 0x000EC9B8 File Offset: 0x000EABB8
	public static string ClansExpSliderHint
	{
		get
		{
			return Language.sL.ClansExpSliderHint;
		}
	}

	// Token: 0x1700063E RID: 1598
	// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000EC9C4 File Offset: 0x000EABC4
	public static string ClansBalanceHint
	{
		get
		{
			return Language.sL.ClansBalanceHint;
		}
	}

	// Token: 0x1700063F RID: 1599
	// (get) Token: 0x060018A1 RID: 6305 RVA: 0x000EC9D0 File Offset: 0x000EABD0
	public static string ClansExpSliderInGameHint
	{
		get
		{
			return Language.sL.ClansExpSliderInGameHint;
		}
	}

	// Token: 0x17000640 RID: 1600
	// (get) Token: 0x060018A2 RID: 6306 RVA: 0x000EC9DC File Offset: 0x000EABDC
	public static string ClansSize
	{
		get
		{
			return Language.sL.ClansSize;
		}
	}

	// Token: 0x17000641 RID: 1601
	// (get) Token: 0x060018A3 RID: 6307 RVA: 0x000EC9E8 File Offset: 0x000EABE8
	public static string ClansVictory
	{
		get
		{
			return Language.sL.ClansVictory;
		}
	}

	// Token: 0x17000642 RID: 1602
	// (get) Token: 0x060018A4 RID: 6308 RVA: 0x000EC9F4 File Offset: 0x000EABF4
	public static string Officers
	{
		get
		{
			return Language.sL.Officers;
		}
	}

	// Token: 0x17000643 RID: 1603
	// (get) Token: 0x060018A5 RID: 6309 RVA: 0x000ECA00 File Offset: 0x000EAC00
	public static string ClansYourWarrior
	{
		get
		{
			return Language.sL.ClansYourWarrior;
		}
	}

	// Token: 0x17000644 RID: 1604
	// (get) Token: 0x060018A6 RID: 6310 RVA: 0x000ECA0C File Offset: 0x000EAC0C
	public static string ClansRequest1
	{
		get
		{
			return Language.sL.ClansRequest1;
		}
	}

	// Token: 0x17000645 RID: 1605
	// (get) Token: 0x060018A7 RID: 6311 RVA: 0x000ECA18 File Offset: 0x000EAC18
	public static string ClansRequest2
	{
		get
		{
			return Language.sL.ClansRequest2;
		}
	}

	// Token: 0x17000646 RID: 1606
	// (get) Token: 0x060018A8 RID: 6312 RVA: 0x000ECA24 File Offset: 0x000EAC24
	public static string ClansWithdraw
	{
		get
		{
			return Language.sL.ClansWithdraw;
		}
	}

	// Token: 0x17000647 RID: 1607
	// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000ECA30 File Offset: 0x000EAC30
	public static string ClansCreateLabel
	{
		get
		{
			return Language.sL.ClansCreateLabel;
		}
	}

	// Token: 0x17000648 RID: 1608
	// (get) Token: 0x060018AA RID: 6314 RVA: 0x000ECA3C File Offset: 0x000EAC3C
	public static string ClansRequestLeft
	{
		get
		{
			return Language.sL.ClansRequestLeft;
		}
	}

	// Token: 0x17000649 RID: 1609
	// (get) Token: 0x060018AB RID: 6315 RVA: 0x000ECA48 File Offset: 0x000EAC48
	public static string ClansRaceBtn
	{
		get
		{
			return Language.sL.ClansRaceBtn;
		}
	}

	// Token: 0x1700064A RID: 1610
	// (get) Token: 0x060018AC RID: 6316 RVA: 0x000ECA54 File Offset: 0x000EAC54
	public static string ClansWarsBtn
	{
		get
		{
			return Language.sL.ClansWarsBtn;
		}
	}

	// Token: 0x1700064B RID: 1611
	// (get) Token: 0x060018AD RID: 6317 RVA: 0x000ECA60 File Offset: 0x000EAC60
	public static string ClansArmoryBtn
	{
		get
		{
			return Language.sL.ClansArmoryBtn;
		}
	}

	// Token: 0x1700064C RID: 1612
	// (get) Token: 0x060018AE RID: 6318 RVA: 0x000ECA6C File Offset: 0x000EAC6C
	public static string ClansCamouflageBtn
	{
		get
		{
			return Language.sL.ClansCamouflageBtn;
		}
	}

	// Token: 0x1700064D RID: 1613
	// (get) Token: 0x060018AF RID: 6319 RVA: 0x000ECA78 File Offset: 0x000EAC78
	public static string ClansHistory
	{
		get
		{
			return Language.sL.ClansHistory;
		}
	}

	// Token: 0x1700064E RID: 1614
	// (get) Token: 0x060018B0 RID: 6320 RVA: 0x000ECA84 File Offset: 0x000EAC84
	public static string ClansClantag
	{
		get
		{
			return Language.sL.ClansClantag;
		}
	}

	// Token: 0x1700064F RID: 1615
	// (get) Token: 0x060018B1 RID: 6321 RVA: 0x000ECA90 File Offset: 0x000EAC90
	public static string ClansClantagColor
	{
		get
		{
			return Language.sL.ClansClantagColor;
		}
	}

	// Token: 0x17000650 RID: 1616
	// (get) Token: 0x060018B2 RID: 6322 RVA: 0x000ECA9C File Offset: 0x000EAC9C
	public static string ChangeNickColor
	{
		get
		{
			return Language.sL.ChangeNickColor;
		}
	}

	// Token: 0x17000651 RID: 1617
	// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000ECAA8 File Offset: 0x000EACA8
	public static string ClansClanName
	{
		get
		{
			return Language.sL.ClansClanName;
		}
	}

	// Token: 0x17000652 RID: 1618
	// (get) Token: 0x060018B4 RID: 6324 RVA: 0x000ECAB4 File Offset: 0x000EACB4
	public static string ClansBase
	{
		get
		{
			return Language.sL.ClansBase;
		}
	}

	// Token: 0x17000653 RID: 1619
	// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000ECAC0 File Offset: 0x000EACC0
	public static string ClansExtended
	{
		get
		{
			return Language.sL.ClansExtended;
		}
	}

	// Token: 0x17000654 RID: 1620
	// (get) Token: 0x060018B6 RID: 6326 RVA: 0x000ECACC File Offset: 0x000EACCC
	public static string ClansPremium
	{
		get
		{
			return Language.sL.ClansPremium;
		}
	}

	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000ECAD8 File Offset: 0x000EACD8
	public static string ClansCreateHint1
	{
		get
		{
			return Language.sL.ClansCreateHint1;
		}
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x060018B8 RID: 6328 RVA: 0x000ECAE4 File Offset: 0x000EACE4
	public static string ClansCreateHint2
	{
		get
		{
			return Language.sL.ClansCreateHint2;
		}
	}

	// Token: 0x17000657 RID: 1623
	// (get) Token: 0x060018B9 RID: 6329 RVA: 0x000ECAF0 File Offset: 0x000EACF0
	public static string ClansCreateHint3
	{
		get
		{
			return Language.sL.ClansCreateHint3;
		}
	}

	// Token: 0x17000658 RID: 1624
	// (get) Token: 0x060018BA RID: 6330 RVA: 0x000ECAFC File Offset: 0x000EACFC
	public static string ClansCreateHint4
	{
		get
		{
			return Language.sL.ClansCreateHint4;
		}
	}

	// Token: 0x17000659 RID: 1625
	// (get) Token: 0x060018BB RID: 6331 RVA: 0x000ECB08 File Offset: 0x000EAD08
	public static string ClansCreateHint5
	{
		get
		{
			return Language.sL.ClansCreateHint5;
		}
	}

	// Token: 0x1700065A RID: 1626
	// (get) Token: 0x060018BC RID: 6332 RVA: 0x000ECB14 File Offset: 0x000EAD14
	public static string ClansCreateHint6
	{
		get
		{
			return Language.sL.ClansCreateHint6;
		}
	}

	// Token: 0x1700065B RID: 1627
	// (get) Token: 0x060018BD RID: 6333 RVA: 0x000ECB20 File Offset: 0x000EAD20
	public static string ClansManagmentDiscard1
	{
		get
		{
			return Language.sL.ClansManagmentDiscard1;
		}
	}

	// Token: 0x1700065C RID: 1628
	// (get) Token: 0x060018BE RID: 6334 RVA: 0x000ECB2C File Offset: 0x000EAD2C
	public static string ClansManagmentDiscard2
	{
		get
		{
			return Language.sL.ClansManagmentDiscard2;
		}
	}

	// Token: 0x1700065D RID: 1629
	// (get) Token: 0x060018BF RID: 6335 RVA: 0x000ECB38 File Offset: 0x000EAD38
	public static string ClansManagmentExtend
	{
		get
		{
			return Language.sL.ClansManagmentExtend;
		}
	}

	// Token: 0x1700065E RID: 1630
	// (get) Token: 0x060018C0 RID: 6336 RVA: 0x000ECB44 File Offset: 0x000EAD44
	public static string ClansManagmentLeave
	{
		get
		{
			return Language.sL.ClansManagmentLeave;
		}
	}

	// Token: 0x1700065F RID: 1631
	// (get) Token: 0x060018C1 RID: 6337 RVA: 0x000ECB50 File Offset: 0x000EAD50
	public static string ClansManagmentRequest
	{
		get
		{
			return Language.sL.ClansManagmentRequest;
		}
	}

	// Token: 0x17000660 RID: 1632
	// (get) Token: 0x060018C2 RID: 6338 RVA: 0x000ECB5C File Offset: 0x000EAD5C
	public static string ClansManagmentCurrent
	{
		get
		{
			return Language.sL.ClansManagmentCurrent;
		}
	}

	// Token: 0x17000661 RID: 1633
	// (get) Token: 0x060018C3 RID: 6339 RVA: 0x000ECB68 File Offset: 0x000EAD68
	public static string ClansBalance
	{
		get
		{
			return Language.sL.ClansBalance;
		}
	}

	// Token: 0x17000662 RID: 1634
	// (get) Token: 0x060018C4 RID: 6340 RVA: 0x000ECB74 File Offset: 0x000EAD74
	public static string ClansSkillAccess
	{
		get
		{
			return Language.sL.ClansSkillAccess;
		}
	}

	// Token: 0x17000663 RID: 1635
	// (get) Token: 0x060018C5 RID: 6341 RVA: 0x000ECB80 File Offset: 0x000EAD80
	public static string ClansMinimalTransaction
	{
		get
		{
			return Language.sL.ClansMinimalTransaction;
		}
	}

	// Token: 0x17000664 RID: 1636
	// (get) Token: 0x060018C6 RID: 6342 RVA: 0x000ECB8C File Offset: 0x000EAD8C
	public static string ClansTableContribution
	{
		get
		{
			return Language.sL.ClansTableContribution;
		}
	}

	// Token: 0x17000665 RID: 1637
	// (get) Token: 0x060018C7 RID: 6343 RVA: 0x000ECB98 File Offset: 0x000EAD98
	public static string ClansTableDiff
	{
		get
		{
			return Language.sL.ClansTableDiff;
		}
	}

	// Token: 0x17000666 RID: 1638
	// (get) Token: 0x060018C8 RID: 6344 RVA: 0x000ECBA4 File Offset: 0x000EADA4
	public static string ClansRaceAttention
	{
		get
		{
			return Language.sL.ClansRaceAttention;
		}
	}

	// Token: 0x17000667 RID: 1639
	// (get) Token: 0x060018C9 RID: 6345 RVA: 0x000ECBB0 File Offset: 0x000EADB0
	public static string ClansRaceHint
	{
		get
		{
			return Language.sL.ClansRaceHint;
		}
	}

	// Token: 0x17000668 RID: 1640
	// (get) Token: 0x060018CA RID: 6346 RVA: 0x000ECBBC File Offset: 0x000EADBC
	public static string ClansRaceHint1
	{
		get
		{
			return Language.sL.ClansRaceHint1;
		}
	}

	// Token: 0x17000669 RID: 1641
	// (get) Token: 0x060018CB RID: 6347 RVA: 0x000ECBC8 File Offset: 0x000EADC8
	public static string ClansRaceEnding
	{
		get
		{
			return Language.sL.ClansRaceEnding;
		}
	}

	// Token: 0x1700066A RID: 1642
	// (get) Token: 0x060018CC RID: 6348 RVA: 0x000ECBD4 File Offset: 0x000EADD4
	public static string ClansRaceExp
	{
		get
		{
			return Language.sL.ClansRaceExp;
		}
	}

	// Token: 0x1700066B RID: 1643
	// (get) Token: 0x060018CD RID: 6349 RVA: 0x000ECBE0 File Offset: 0x000EADE0
	public static string ClansRaceKills
	{
		get
		{
			return Language.sL.ClansRaceKills;
		}
	}

	// Token: 0x1700066C RID: 1644
	// (get) Token: 0x060018CE RID: 6350 RVA: 0x000ECBEC File Offset: 0x000EADEC
	public static string ClansDisbanded
	{
		get
		{
			return Language.sL.ClansDisbanded;
		}
	}

	// Token: 0x1700066D RID: 1645
	// (get) Token: 0x060018CF RID: 6351 RVA: 0x000ECBF8 File Offset: 0x000EADF8
	public static string ClansPopupError
	{
		get
		{
			return Language.sL.ClansPopupError;
		}
	}

	// Token: 0x1700066E RID: 1646
	// (get) Token: 0x060018D0 RID: 6352 RVA: 0x000ECC04 File Offset: 0x000EAE04
	public static string ClansPopupCreate
	{
		get
		{
			return Language.sL.ClansPopupCreate;
		}
	}

	// Token: 0x1700066F RID: 1647
	// (get) Token: 0x060018D1 RID: 6353 RVA: 0x000ECC10 File Offset: 0x000EAE10
	public static string ClansPopupCreateHint1
	{
		get
		{
			return Language.sL.ClansPopupCreateHint1;
		}
	}

	// Token: 0x17000670 RID: 1648
	// (get) Token: 0x060018D2 RID: 6354 RVA: 0x000ECC1C File Offset: 0x000EAE1C
	public static string ClansPopupCreateHint2
	{
		get
		{
			return Language.sL.ClansPopupCreateHint2;
		}
	}

	// Token: 0x17000671 RID: 1649
	// (get) Token: 0x060018D3 RID: 6355 RVA: 0x000ECC28 File Offset: 0x000EAE28
	public static string ClansPopupCreateHint3
	{
		get
		{
			return Language.sL.ClansPopupCreateHint3;
		}
	}

	// Token: 0x17000672 RID: 1650
	// (get) Token: 0x060018D4 RID: 6356 RVA: 0x000ECC34 File Offset: 0x000EAE34
	public static string ClansPopupCreateHint4
	{
		get
		{
			return Language.sL.ClansPopupCreateHint4;
		}
	}

	// Token: 0x17000673 RID: 1651
	// (get) Token: 0x060018D5 RID: 6357 RVA: 0x000ECC40 File Offset: 0x000EAE40
	public static string ClansPopupCreateHint5
	{
		get
		{
			return Language.sL.ClansPopupCreateHint5;
		}
	}

	// Token: 0x17000674 RID: 1652
	// (get) Token: 0x060018D6 RID: 6358 RVA: 0x000ECC4C File Offset: 0x000EAE4C
	public static string ClansPopupCreateError1
	{
		get
		{
			return Language.sL.ClansPopupCreateError1;
		}
	}

	// Token: 0x17000675 RID: 1653
	// (get) Token: 0x060018D7 RID: 6359 RVA: 0x000ECC58 File Offset: 0x000EAE58
	public static string ClansPopupCreateError2
	{
		get
		{
			return Language.sL.ClansPopupCreateError1;
		}
	}

	// Token: 0x17000676 RID: 1654
	// (get) Token: 0x060018D8 RID: 6360 RVA: 0x000ECC64 File Offset: 0x000EAE64
	public static string ClansPopupExtend
	{
		get
		{
			return Language.sL.ClansPopupExtend;
		}
	}

	// Token: 0x17000677 RID: 1655
	// (get) Token: 0x060018D9 RID: 6361 RVA: 0x000ECC70 File Offset: 0x000EAE70
	public static string ClansPopupExtendHint
	{
		get
		{
			return Language.sL.ClansPopupExtendHint;
		}
	}

	// Token: 0x17000678 RID: 1656
	// (get) Token: 0x060018DA RID: 6362 RVA: 0x000ECC7C File Offset: 0x000EAE7C
	public static string ClansPopupRequest
	{
		get
		{
			return Language.sL.ClansPopupRequest;
		}
	}

	// Token: 0x17000679 RID: 1657
	// (get) Token: 0x060018DB RID: 6363 RVA: 0x000ECC88 File Offset: 0x000EAE88
	public static string ClansPopupRequestFailedByOrder1
	{
		get
		{
			return Language.sL.ClansPopupRequestFailedByOrder1;
		}
	}

	// Token: 0x1700067A RID: 1658
	// (get) Token: 0x060018DC RID: 6364 RVA: 0x000ECC94 File Offset: 0x000EAE94
	public static string ClansPopupRequestFailedByOrder2
	{
		get
		{
			return Language.sL.ClansPopupRequestFailedByOrder2;
		}
	}

	// Token: 0x1700067B RID: 1659
	// (get) Token: 0x060018DD RID: 6365 RVA: 0x000ECCA0 File Offset: 0x000EAEA0
	public static string ClansPopupRequestFailedByVacancy1
	{
		get
		{
			return Language.sL.ClansPopupRequestFailedByVacancy1;
		}
	}

	// Token: 0x1700067C RID: 1660
	// (get) Token: 0x060018DE RID: 6366 RVA: 0x000ECCAC File Offset: 0x000EAEAC
	public static string ClansPopupRequestFailedByVacancy2
	{
		get
		{
			return Language.sL.ClansPopupRequestFailedByVacancy2;
		}
	}

	// Token: 0x1700067D RID: 1661
	// (get) Token: 0x060018DF RID: 6367 RVA: 0x000ECCB8 File Offset: 0x000EAEB8
	public static string ClansPopupRequestFailedByVacancy3
	{
		get
		{
			return Language.sL.ClansPopupRequestFailedByVacancy3;
		}
	}

	// Token: 0x1700067E RID: 1662
	// (get) Token: 0x060018E0 RID: 6368 RVA: 0x000ECCC4 File Offset: 0x000EAEC4
	public static string ClansPopupDiscard
	{
		get
		{
			return Language.sL.ClansPopupDiscard;
		}
	}

	// Token: 0x1700067F RID: 1663
	// (get) Token: 0x060018E1 RID: 6369 RVA: 0x000ECCD0 File Offset: 0x000EAED0
	public static string ClansPopupDiscardHint
	{
		get
		{
			return Language.sL.ClansPopupDiscardHint;
		}
	}

	// Token: 0x17000680 RID: 1664
	// (get) Token: 0x060018E2 RID: 6370 RVA: 0x000ECCDC File Offset: 0x000EAEDC
	public static string ClansPopupDismiss
	{
		get
		{
			return Language.sL.ClansPopupDismiss;
		}
	}

	// Token: 0x17000681 RID: 1665
	// (get) Token: 0x060018E3 RID: 6371 RVA: 0x000ECCE8 File Offset: 0x000EAEE8
	public static string ClansPopupDismissHint1
	{
		get
		{
			return Language.sL.ClansPopupDismissHint1;
		}
	}

	// Token: 0x17000682 RID: 1666
	// (get) Token: 0x060018E4 RID: 6372 RVA: 0x000ECCF4 File Offset: 0x000EAEF4
	public static string ClansPopupDismissHint2
	{
		get
		{
			return Language.sL.ClansPopupDismissHint2;
		}
	}

	// Token: 0x17000683 RID: 1667
	// (get) Token: 0x060018E5 RID: 6373 RVA: 0x000ECD00 File Offset: 0x000EAF00
	public static string ClansPopupLeave
	{
		get
		{
			return Language.sL.ClansPopupLeave;
		}
	}

	// Token: 0x17000684 RID: 1668
	// (get) Token: 0x060018E6 RID: 6374 RVA: 0x000ECD0C File Offset: 0x000EAF0C
	public static string ClansPopupLeaveHint
	{
		get
		{
			return Language.sL.ClansPopupLeaveHint;
		}
	}

	// Token: 0x17000685 RID: 1669
	// (get) Token: 0x060018E7 RID: 6375 RVA: 0x000ECD18 File Offset: 0x000EAF18
	public static string ClansPopupBalance
	{
		get
		{
			return Language.sL.ClansPopupBalance;
		}
	}

	// Token: 0x17000686 RID: 1670
	// (get) Token: 0x060018E8 RID: 6376 RVA: 0x000ECD24 File Offset: 0x000EAF24
	public static string ClansPopupBalanceHint
	{
		get
		{
			return Language.sL.ClansPopupBalanceHint;
		}
	}

	// Token: 0x17000687 RID: 1671
	// (get) Token: 0x060018E9 RID: 6377 RVA: 0x000ECD30 File Offset: 0x000EAF30
	public static string ClansHistoryWho
	{
		get
		{
			return Language.sL.ClansHistoryWho;
		}
	}

	// Token: 0x17000688 RID: 1672
	// (get) Token: 0x060018EA RID: 6378 RVA: 0x000ECD3C File Offset: 0x000EAF3C
	public static string CreateClan
	{
		get
		{
			return Language.sL.CreateClan;
		}
	}

	// Token: 0x17000689 RID: 1673
	// (get) Token: 0x060018EB RID: 6379 RVA: 0x000ECD48 File Offset: 0x000EAF48
	public static string CreateClanProcessing
	{
		get
		{
			return Language.sL.CreateClanProcessing;
		}
	}

	// Token: 0x1700068A RID: 1674
	// (get) Token: 0x060018EC RID: 6380 RVA: 0x000ECD54 File Offset: 0x000EAF54
	public static string CreateClanComplete
	{
		get
		{
			return Language.sL.CreateClanComplete;
		}
	}

	// Token: 0x1700068B RID: 1675
	// (get) Token: 0x060018ED RID: 6381 RVA: 0x000ECD60 File Offset: 0x000EAF60
	public static string ExtendClan
	{
		get
		{
			return Language.sL.ExtendClan;
		}
	}

	// Token: 0x1700068C RID: 1676
	// (get) Token: 0x060018EE RID: 6382 RVA: 0x000ECD6C File Offset: 0x000EAF6C
	public static string ExtendClanProcessing
	{
		get
		{
			return Language.sL.ExtendClanProcessing;
		}
	}

	// Token: 0x1700068D RID: 1677
	// (get) Token: 0x060018EF RID: 6383 RVA: 0x000ECD78 File Offset: 0x000EAF78
	public static string ExtendClanComplete
	{
		get
		{
			return Language.sL.ExtendClanComplete;
		}
	}

	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x060018F0 RID: 6384 RVA: 0x000ECD84 File Offset: 0x000EAF84
	public static string DeleteAllRequest
	{
		get
		{
			return Language.sL.DeleteAllRequest;
		}
	}

	// Token: 0x1700068F RID: 1679
	// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000ECD90 File Offset: 0x000EAF90
	public static string DeleteAllRequestProcessing
	{
		get
		{
			return Language.sL.DeleteAllRequestProcessing;
		}
	}

	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x060018F2 RID: 6386 RVA: 0x000ECD9C File Offset: 0x000EAF9C
	public static string DeleteAllRequestComplete
	{
		get
		{
			return Language.sL.DeleteAllRequestComplete;
		}
	}

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x060018F3 RID: 6387 RVA: 0x000ECDA8 File Offset: 0x000EAFA8
	public static string KickFromClan
	{
		get
		{
			return Language.sL.KickFromClan;
		}
	}

	// Token: 0x17000692 RID: 1682
	// (get) Token: 0x060018F4 RID: 6388 RVA: 0x000ECDB4 File Offset: 0x000EAFB4
	public static string KickFromClanProcessing
	{
		get
		{
			return Language.sL.KickFromClanProcessing;
		}
	}

	// Token: 0x17000693 RID: 1683
	// (get) Token: 0x060018F5 RID: 6389 RVA: 0x000ECDC0 File Offset: 0x000EAFC0
	public static string KickFromClanComplete
	{
		get
		{
			return Language.sL.KickFromClanComplete;
		}
	}

	// Token: 0x17000694 RID: 1684
	// (get) Token: 0x060018F6 RID: 6390 RVA: 0x000ECDCC File Offset: 0x000EAFCC
	public static string SendRequest
	{
		get
		{
			return Language.sL.SendRequest;
		}
	}

	// Token: 0x17000695 RID: 1685
	// (get) Token: 0x060018F7 RID: 6391 RVA: 0x000ECDD8 File Offset: 0x000EAFD8
	public static string SendRequestProcessing
	{
		get
		{
			return Language.sL.SendRequestProcessing;
		}
	}

	// Token: 0x17000696 RID: 1686
	// (get) Token: 0x060018F8 RID: 6392 RVA: 0x000ECDE4 File Offset: 0x000EAFE4
	public static string SendRequestComplete
	{
		get
		{
			return Language.sL.SendRequestComplete;
		}
	}

	// Token: 0x17000697 RID: 1687
	// (get) Token: 0x060018F9 RID: 6393 RVA: 0x000ECDF0 File Offset: 0x000EAFF0
	public static string RevokeRequest
	{
		get
		{
			return Language.sL.RevokeRequest;
		}
	}

	// Token: 0x17000698 RID: 1688
	// (get) Token: 0x060018FA RID: 6394 RVA: 0x000ECDFC File Offset: 0x000EAFFC
	public static string RevokeRequestProcessing
	{
		get
		{
			return Language.sL.RevokeRequestProcessing;
		}
	}

	// Token: 0x17000699 RID: 1689
	// (get) Token: 0x060018FB RID: 6395 RVA: 0x000ECE08 File Offset: 0x000EB008
	public static string RevokeRequestComplete
	{
		get
		{
			return Language.sL.RevokeRequestComplete;
		}
	}

	// Token: 0x1700069A RID: 1690
	// (get) Token: 0x060018FC RID: 6396 RVA: 0x000ECE14 File Offset: 0x000EB014
	public static string AcceptRequest
	{
		get
		{
			return Language.sL.AcceptRequest;
		}
	}

	// Token: 0x1700069B RID: 1691
	// (get) Token: 0x060018FD RID: 6397 RVA: 0x000ECE20 File Offset: 0x000EB020
	public static string AcceptRequestProcessing
	{
		get
		{
			return Language.sL.AcceptRequestProcessing;
		}
	}

	// Token: 0x1700069C RID: 1692
	// (get) Token: 0x060018FE RID: 6398 RVA: 0x000ECE2C File Offset: 0x000EB02C
	public static string AcceptRequestComplete
	{
		get
		{
			return Language.sL.AcceptRequestComplete;
		}
	}

	// Token: 0x1700069D RID: 1693
	// (get) Token: 0x060018FF RID: 6399 RVA: 0x000ECE38 File Offset: 0x000EB038
	public static string DeleteRequest
	{
		get
		{
			return Language.sL.DeleteRequest;
		}
	}

	// Token: 0x1700069E RID: 1694
	// (get) Token: 0x06001900 RID: 6400 RVA: 0x000ECE44 File Offset: 0x000EB044
	public static string DeleteRequestProcessing
	{
		get
		{
			return Language.sL.DeleteRequestProcessing;
		}
	}

	// Token: 0x1700069F RID: 1695
	// (get) Token: 0x06001901 RID: 6401 RVA: 0x000ECE50 File Offset: 0x000EB050
	public static string DeleteRequestComplete
	{
		get
		{
			return Language.sL.DeleteRequestComplete;
		}
	}

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x06001902 RID: 6402 RVA: 0x000ECE5C File Offset: 0x000EB05C
	public static string ExitClan
	{
		get
		{
			return Language.sL.ExitClan;
		}
	}

	// Token: 0x170006A1 RID: 1697
	// (get) Token: 0x06001903 RID: 6403 RVA: 0x000ECE68 File Offset: 0x000EB068
	public static string ExitClanProcessing
	{
		get
		{
			return Language.sL.ExitClanProcessing;
		}
	}

	// Token: 0x170006A2 RID: 1698
	// (get) Token: 0x06001904 RID: 6404 RVA: 0x000ECE74 File Offset: 0x000EB074
	public static string ExitClanComplete
	{
		get
		{
			return Language.sL.ExitClanComplete;
		}
	}

	// Token: 0x170006A3 RID: 1699
	// (get) Token: 0x06001905 RID: 6405 RVA: 0x000ECE80 File Offset: 0x000EB080
	public static string ClanListLoading
	{
		get
		{
			return Language.sL.ClanListLoading;
		}
	}

	// Token: 0x170006A4 RID: 1700
	// (get) Token: 0x06001906 RID: 6406 RVA: 0x000ECE8C File Offset: 0x000EB08C
	public static string ClanListLoadingDesc
	{
		get
		{
			return Language.sL.ClanListLoadingDesc;
		}
	}

	// Token: 0x170006A5 RID: 1701
	// (get) Token: 0x06001907 RID: 6407 RVA: 0x000ECE98 File Offset: 0x000EB098
	public static string ClanListLoadingFin
	{
		get
		{
			return Language.sL.ClanListLoadingFin;
		}
	}

	// Token: 0x170006A6 RID: 1702
	// (get) Token: 0x06001908 RID: 6408 RVA: 0x000ECEA4 File Offset: 0x000EB0A4
	public static string ClanListLoadingErrDesc
	{
		get
		{
			return Language.sL.ClanListLoadingErrDesc;
		}
	}

	// Token: 0x170006A7 RID: 1703
	// (get) Token: 0x06001909 RID: 6409 RVA: 0x000ECEB0 File Offset: 0x000EB0B0
	public static string ClanDetailLoading
	{
		get
		{
			return Language.sL.ClanDetailLoading;
		}
	}

	// Token: 0x170006A8 RID: 1704
	// (get) Token: 0x0600190A RID: 6410 RVA: 0x000ECEBC File Offset: 0x000EB0BC
	public static string ClanDetailLoadingDesc
	{
		get
		{
			return Language.sL.ClanDetailLoadingDesc;
		}
	}

	// Token: 0x170006A9 RID: 1705
	// (get) Token: 0x0600190B RID: 6411 RVA: 0x000ECEC8 File Offset: 0x000EB0C8
	public static string ClanDetailLoadingFin
	{
		get
		{
			return Language.sL.ClanDetailLoadingFin;
		}
	}

	// Token: 0x170006AA RID: 1706
	// (get) Token: 0x0600190C RID: 6412 RVA: 0x000ECED4 File Offset: 0x000EB0D4
	public static string ClanDetailLoadingErrDesc
	{
		get
		{
			return Language.sL.ClanDetailLoadingErrDesc;
		}
	}

	// Token: 0x170006AB RID: 1707
	// (get) Token: 0x0600190D RID: 6413 RVA: 0x000ECEE0 File Offset: 0x000EB0E0
	public static string ClansCheck
	{
		get
		{
			return Language.sL.ClansCheck;
		}
	}

	// Token: 0x170006AC RID: 1708
	// (get) Token: 0x0600190E RID: 6414 RVA: 0x000ECEEC File Offset: 0x000EB0EC
	public static string ClansAvailable
	{
		get
		{
			return Language.sL.ClansAvailable;
		}
	}

	// Token: 0x170006AD RID: 1709
	// (get) Token: 0x0600190F RID: 6415 RVA: 0x000ECEF8 File Offset: 0x000EB0F8
	public static string ClansUnavailable
	{
		get
		{
			return Language.sL.ClansUnavailable;
		}
	}

	// Token: 0x170006AE RID: 1710
	// (get) Token: 0x06001910 RID: 6416 RVA: 0x000ECF04 File Offset: 0x000EB104
	public static string CurrentColor
	{
		get
		{
			return Language.sL.CurrentColor;
		}
	}

	// Token: 0x170006AF RID: 1711
	// (get) Token: 0x06001911 RID: 6417 RVA: 0x000ECF10 File Offset: 0x000EB110
	public static string ClansHomePage
	{
		get
		{
			return Language.sL.ClansHomePage;
		}
	}

	// Token: 0x170006B0 RID: 1712
	// (get) Token: 0x06001912 RID: 6418 RVA: 0x000ECF1C File Offset: 0x000EB11C
	public static string ClansHomePageHint
	{
		get
		{
			return Language.sL.ClansHomePageHint;
		}
	}

	// Token: 0x170006B1 RID: 1713
	// (get) Token: 0x06001913 RID: 6419 RVA: 0x000ECF28 File Offset: 0x000EB128
	public static string ClansHeadquarters1
	{
		get
		{
			return Language.sL.ClansHeadquarters1;
		}
	}

	// Token: 0x170006B2 RID: 1714
	// (get) Token: 0x06001914 RID: 6420 RVA: 0x000ECF34 File Offset: 0x000EB134
	public static string ClansHeadquarters2
	{
		get
		{
			return Language.sL.ClansHeadquarters2;
		}
	}

	// Token: 0x170006B3 RID: 1715
	// (get) Token: 0x06001915 RID: 6421 RVA: 0x000ECF40 File Offset: 0x000EB140
	public static string ClansLeader
	{
		get
		{
			return Language.sL.ClansLeader;
		}
	}

	// Token: 0x170006B4 RID: 1716
	// (get) Token: 0x06001916 RID: 6422 RVA: 0x000ECF4C File Offset: 0x000EB14C
	public static string ClansLieutenant
	{
		get
		{
			return Language.sL.ClansLieutenant;
		}
	}

	// Token: 0x170006B5 RID: 1717
	// (get) Token: 0x06001917 RID: 6423 RVA: 0x000ECF58 File Offset: 0x000EB158
	public static string ClansOfficer
	{
		get
		{
			return Language.sL.ClansOfficer;
		}
	}

	// Token: 0x170006B6 RID: 1718
	// (get) Token: 0x06001918 RID: 6424 RVA: 0x000ECF64 File Offset: 0x000EB164
	public static string ClansContractor
	{
		get
		{
			return Language.sL.ClansContractor;
		}
	}

	// Token: 0x170006B7 RID: 1719
	// (get) Token: 0x06001919 RID: 6425 RVA: 0x000ECF70 File Offset: 0x000EB170
	public static string ClansLeaderShort
	{
		get
		{
			return Language.sL.ClansLeaderShort;
		}
	}

	// Token: 0x170006B8 RID: 1720
	// (get) Token: 0x0600191A RID: 6426 RVA: 0x000ECF7C File Offset: 0x000EB17C
	public static string ClansLieutenantShort
	{
		get
		{
			return Language.sL.ClansLieutenantShort;
		}
	}

	// Token: 0x170006B9 RID: 1721
	// (get) Token: 0x0600191B RID: 6427 RVA: 0x000ECF88 File Offset: 0x000EB188
	public static string ClansOfficerShort
	{
		get
		{
			return Language.sL.ClansOfficerShort;
		}
	}

	// Token: 0x170006BA RID: 1722
	// (get) Token: 0x0600191C RID: 6428 RVA: 0x000ECF94 File Offset: 0x000EB194
	public static string ClansNotInClan
	{
		get
		{
			return Language.sL.ClansNotInClan;
		}
	}

	// Token: 0x170006BB RID: 1723
	// (get) Token: 0x0600191D RID: 6429 RVA: 0x000ECFA0 File Offset: 0x000EB1A0
	public static string Role
	{
		get
		{
			return Language.sL.Role;
		}
	}

	// Token: 0x170006BC RID: 1724
	// (get) Token: 0x0600191E RID: 6430 RVA: 0x000ECFAC File Offset: 0x000EB1AC
	public static string Earn
	{
		get
		{
			return Language.sL.Earn;
		}
	}

	// Token: 0x170006BD RID: 1725
	// (get) Token: 0x0600191F RID: 6431 RVA: 0x000ECFB8 File Offset: 0x000EB1B8
	public static string ClansEditPopupHeader
	{
		get
		{
			return Language.sL.ClansEditPopupHeader;
		}
	}

	// Token: 0x170006BE RID: 1726
	// (get) Token: 0x06001920 RID: 6432 RVA: 0x000ECFC4 File Offset: 0x000EB1C4
	public static string ClansSetLeaderPopupHeader
	{
		get
		{
			return Language.sL.ClansSetLeaderPopupHeader;
		}
	}

	// Token: 0x170006BF RID: 1727
	// (get) Token: 0x06001921 RID: 6433 RVA: 0x000ECFD0 File Offset: 0x000EB1D0
	public static string ClansSetLtPopupHeader
	{
		get
		{
			return Language.sL.ClansSetLtPopupHeader;
		}
	}

	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06001922 RID: 6434 RVA: 0x000ECFDC File Offset: 0x000EB1DC
	public static string ClansSetOfficerPopupHeader
	{
		get
		{
			return Language.sL.ClansSetOfficerPopupHeader;
		}
	}

	// Token: 0x170006C1 RID: 1729
	// (get) Token: 0x06001923 RID: 6435 RVA: 0x000ECFE8 File Offset: 0x000EB1E8
	public static string ClansDismissLtPopupHeader
	{
		get
		{
			return Language.sL.ClansDismissLtPopupHeader;
		}
	}

	// Token: 0x170006C2 RID: 1730
	// (get) Token: 0x06001924 RID: 6436 RVA: 0x000ECFF4 File Offset: 0x000EB1F4
	public static string ClansDismissOfficerPopupHeader
	{
		get
		{
			return Language.sL.ClansDismissOfficerPopupHeader;
		}
	}

	// Token: 0x170006C3 RID: 1731
	// (get) Token: 0x06001925 RID: 6437 RVA: 0x000ED000 File Offset: 0x000EB200
	public static string ClansSetLeaderPopupBody
	{
		get
		{
			return Language.sL.ClansSetLeaderPopupBody;
		}
	}

	// Token: 0x170006C4 RID: 1732
	// (get) Token: 0x06001926 RID: 6438 RVA: 0x000ED00C File Offset: 0x000EB20C
	public static string ClansSetLtPopupBody
	{
		get
		{
			return Language.sL.ClansSetLtPopupBody;
		}
	}

	// Token: 0x170006C5 RID: 1733
	// (get) Token: 0x06001927 RID: 6439 RVA: 0x000ED018 File Offset: 0x000EB218
	public static string ClansDismissLtPopupBody
	{
		get
		{
			return Language.sL.ClansDismissLtPopupBody;
		}
	}

	// Token: 0x170006C6 RID: 1734
	// (get) Token: 0x06001928 RID: 6440 RVA: 0x000ED024 File Offset: 0x000EB224
	public static string ClansSetOfficerPopupBody
	{
		get
		{
			return Language.sL.ClansSetOfficerPopupBody;
		}
	}

	// Token: 0x170006C7 RID: 1735
	// (get) Token: 0x06001929 RID: 6441 RVA: 0x000ED030 File Offset: 0x000EB230
	public static string ClansDismissOfficerPopupBody
	{
		get
		{
			return Language.sL.ClansDismissOfficerPopupBody;
		}
	}

	// Token: 0x170006C8 RID: 1736
	// (get) Token: 0x0600192A RID: 6442 RVA: 0x000ED03C File Offset: 0x000EB23C
	public static string ClansSetLeaderPopupHint
	{
		get
		{
			return Language.sL.ClansSetLeaderPopupHint;
		}
	}

	// Token: 0x170006C9 RID: 1737
	// (get) Token: 0x0600192B RID: 6443 RVA: 0x000ED048 File Offset: 0x000EB248
	public static string ClansSetLtPopupHint
	{
		get
		{
			return Language.sL.ClansSetLtPopupHint;
		}
	}

	// Token: 0x170006CA RID: 1738
	// (get) Token: 0x0600192C RID: 6444 RVA: 0x000ED054 File Offset: 0x000EB254
	public static string ClansSetOfficerPopupHint
	{
		get
		{
			return Language.sL.ClansSetOfficerPopupHint;
		}
	}

	// Token: 0x170006CB RID: 1739
	// (get) Token: 0x0600192D RID: 6445 RVA: 0x000ED060 File Offset: 0x000EB260
	public static string ClansEditMessagePopup
	{
		get
		{
			return Language.sL.ClansEditMessagePopup;
		}
	}

	// Token: 0x170006CC RID: 1740
	// (get) Token: 0x0600192E RID: 6446 RVA: 0x000ED06C File Offset: 0x000EB26C
	public static string ClansEditMessageCharactersleft
	{
		get
		{
			return Language.sL.ClansEditMessageCharactersleft;
		}
	}

	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x0600192F RID: 6447 RVA: 0x000ED078 File Offset: 0x000EB278
	public static string ClansDefaultMessage
	{
		get
		{
			return Language.sL.ClansDefaultMessage;
		}
	}

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x06001930 RID: 6448 RVA: 0x000ED084 File Offset: 0x000EB284
	public static string ClansError1001
	{
		get
		{
			return Language.sL.ClansError1001;
		}
	}

	// Token: 0x170006CF RID: 1743
	// (get) Token: 0x06001931 RID: 6449 RVA: 0x000ED090 File Offset: 0x000EB290
	public static string ClansError1002
	{
		get
		{
			return Language.sL.ClansError1002;
		}
	}

	// Token: 0x170006D0 RID: 1744
	// (get) Token: 0x06001932 RID: 6450 RVA: 0x000ED09C File Offset: 0x000EB29C
	public static string ClansError1003
	{
		get
		{
			return Language.sL.ClansError1003;
		}
	}

	// Token: 0x170006D1 RID: 1745
	// (get) Token: 0x06001933 RID: 6451 RVA: 0x000ED0A8 File Offset: 0x000EB2A8
	public static string ClansError1004
	{
		get
		{
			return Language.sL.ClansError1004;
		}
	}

	// Token: 0x170006D2 RID: 1746
	// (get) Token: 0x06001934 RID: 6452 RVA: 0x000ED0B4 File Offset: 0x000EB2B4
	public static string ClansError1005
	{
		get
		{
			return Language.sL.ClansError1005;
		}
	}

	// Token: 0x170006D3 RID: 1747
	// (get) Token: 0x06001935 RID: 6453 RVA: 0x000ED0C0 File Offset: 0x000EB2C0
	public static string ClansError1006
	{
		get
		{
			return Language.sL.ClansError1006;
		}
	}

	// Token: 0x170006D4 RID: 1748
	// (get) Token: 0x06001936 RID: 6454 RVA: 0x000ED0CC File Offset: 0x000EB2CC
	public static string ClansError1007
	{
		get
		{
			return Language.sL.ClansError1007;
		}
	}

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x06001937 RID: 6455 RVA: 0x000ED0D8 File Offset: 0x000EB2D8
	public static string ClansError1008
	{
		get
		{
			return Language.sL.ClansError1008;
		}
	}

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x06001938 RID: 6456 RVA: 0x000ED0E4 File Offset: 0x000EB2E4
	public static string ClansError1009
	{
		get
		{
			return Language.sL.ClansError1009;
		}
	}

	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x06001939 RID: 6457 RVA: 0x000ED0F0 File Offset: 0x000EB2F0
	public static string ClansError1010
	{
		get
		{
			return Language.sL.ClansError1010;
		}
	}

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x0600193A RID: 6458 RVA: 0x000ED0FC File Offset: 0x000EB2FC
	public static string ClansError1011
	{
		get
		{
			return Language.sL.ClansError1011;
		}
	}

	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x0600193B RID: 6459 RVA: 0x000ED108 File Offset: 0x000EB308
	public static string ClansError1012
	{
		get
		{
			return Language.sL.ClansError1012;
		}
	}

	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x0600193C RID: 6460 RVA: 0x000ED114 File Offset: 0x000EB314
	public static string ClansError1013
	{
		get
		{
			return Language.sL.ClansError1013;
		}
	}

	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x0600193D RID: 6461 RVA: 0x000ED120 File Offset: 0x000EB320
	public static string ClansError1014
	{
		get
		{
			return Language.sL.ClansError1014;
		}
	}

	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x0600193E RID: 6462 RVA: 0x000ED12C File Offset: 0x000EB32C
	public static string ClansError1015
	{
		get
		{
			return Language.sL.ClansError1015;
		}
	}

	// Token: 0x170006DD RID: 1757
	// (get) Token: 0x0600193F RID: 6463 RVA: 0x000ED138 File Offset: 0x000EB338
	public static string ClansError1016
	{
		get
		{
			return Language.sL.ClansError1016;
		}
	}

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x06001940 RID: 6464 RVA: 0x000ED144 File Offset: 0x000EB344
	public static string ClansError1017
	{
		get
		{
			return Language.sL.ClansError1017;
		}
	}

	// Token: 0x170006DF RID: 1759
	// (get) Token: 0x06001941 RID: 6465 RVA: 0x000ED150 File Offset: 0x000EB350
	public static string ClansError1018
	{
		get
		{
			return Language.sL.ClansError1018;
		}
	}

	// Token: 0x170006E0 RID: 1760
	// (get) Token: 0x06001942 RID: 6466 RVA: 0x000ED15C File Offset: 0x000EB35C
	public static string ClansError1019
	{
		get
		{
			return Language.sL.ClansError1019;
		}
	}

	// Token: 0x170006E1 RID: 1761
	// (get) Token: 0x06001943 RID: 6467 RVA: 0x000ED168 File Offset: 0x000EB368
	public static string ClansError1020
	{
		get
		{
			return Language.sL.ClansError1020;
		}
	}

	// Token: 0x170006E2 RID: 1762
	// (get) Token: 0x06001944 RID: 6468 RVA: 0x000ED174 File Offset: 0x000EB374
	public static string ClansError1100
	{
		get
		{
			return Language.sL.ClansError1100;
		}
	}

	// Token: 0x170006E3 RID: 1763
	// (get) Token: 0x06001945 RID: 6469 RVA: 0x000ED180 File Offset: 0x000EB380
	public static string NotEnoughWarriorTasks
	{
		get
		{
			return Language.sL.NotEnoughWarriorTasks;
		}
	}

	// Token: 0x170006E4 RID: 1764
	// (get) Token: 0x06001946 RID: 6470 RVA: 0x000ED18C File Offset: 0x000EB38C
	public static string NotEnoughWarriorExp
	{
		get
		{
			return Language.sL.NotEnoughWarriorExp;
		}
	}

	// Token: 0x170006E5 RID: 1765
	// (get) Token: 0x06001947 RID: 6471 RVA: 0x000ED198 File Offset: 0x000EB398
	public static string Reward
	{
		get
		{
			return Language.sL.Reward;
		}
	}

	// Token: 0x170006E6 RID: 1766
	// (get) Token: 0x06001948 RID: 6472 RVA: 0x000ED1A4 File Offset: 0x000EB3A4
	public static string And
	{
		get
		{
			return Language.sL.And;
		}
	}

	// Token: 0x170006E7 RID: 1767
	// (get) Token: 0x06001949 RID: 6473 RVA: 0x000ED1B0 File Offset: 0x000EB3B0
	public static string For
	{
		get
		{
			return Language.sL.For;
		}
	}

	// Token: 0x170006E8 RID: 1768
	// (get) Token: 0x0600194A RID: 6474 RVA: 0x000ED1BC File Offset: 0x000EB3BC
	public static string GotAchievement0
	{
		get
		{
			return Language.sL.GotAchievement0;
		}
	}

	// Token: 0x170006E9 RID: 1769
	// (get) Token: 0x0600194B RID: 6475 RVA: 0x000ED1C8 File Offset: 0x000EB3C8
	public static string GotAchievement1
	{
		get
		{
			return Language.sL.GotAchievement1;
		}
	}

	// Token: 0x170006EA RID: 1770
	// (get) Token: 0x0600194C RID: 6476 RVA: 0x000ED1D4 File Offset: 0x000EB3D4
	public static string Roulette
	{
		get
		{
			return Language.sL.Roulette;
		}
	}

	// Token: 0x170006EB RID: 1771
	// (get) Token: 0x0600194D RID: 6477 RVA: 0x000ED1E0 File Offset: 0x000EB3E0
	public static string RouletteTries
	{
		get
		{
			return Language.sL.RouletteTries;
		}
	}

	// Token: 0x170006EC RID: 1772
	// (get) Token: 0x0600194E RID: 6478 RVA: 0x000ED1EC File Offset: 0x000EB3EC
	public static string RouletteDescription
	{
		get
		{
			return Language.sL.RouletteDescription;
		}
	}

	// Token: 0x170006ED RID: 1773
	// (get) Token: 0x0600194F RID: 6479 RVA: 0x000ED1F8 File Offset: 0x000EB3F8
	public static string RouleteTriesLeft
	{
		get
		{
			return Language.sL.RouleteTriesLeft;
		}
	}

	// Token: 0x170006EE RID: 1774
	// (get) Token: 0x06001950 RID: 6480 RVA: 0x000ED204 File Offset: 0x000EB404
	public static string RouletteTriesAdd
	{
		get
		{
			return Language.sL.RouletteTriesAdd;
		}
	}

	// Token: 0x170006EF RID: 1775
	// (get) Token: 0x06001951 RID: 6481 RVA: 0x000ED210 File Offset: 0x000EB410
	public static string RouletteRoll
	{
		get
		{
			return Language.sL.RouletteRoll;
		}
	}

	// Token: 0x170006F0 RID: 1776
	// (get) Token: 0x06001952 RID: 6482 RVA: 0x000ED21C File Offset: 0x000EB41C
	public static string RouletteWin
	{
		get
		{
			return Language.sL.RouletteWin;
		}
	}

	// Token: 0x170006F1 RID: 1777
	// (get) Token: 0x06001953 RID: 6483 RVA: 0x000ED228 File Offset: 0x000EB428
	public static string RouletteWinSpecial
	{
		get
		{
			return Language.sL.RouletteWinSpecial;
		}
	}

	// Token: 0x170006F2 RID: 1778
	// (get) Token: 0x06001954 RID: 6484 RVA: 0x000ED234 File Offset: 0x000EB434
	public static string RouletteWinSkill
	{
		get
		{
			return Language.sL.RouletteWinSkill;
		}
	}

	// Token: 0x170006F3 RID: 1779
	// (get) Token: 0x06001955 RID: 6485 RVA: 0x000ED240 File Offset: 0x000EB440
	public static string RouletteWinWeapon
	{
		get
		{
			return Language.sL.RouletteWinWeapon;
		}
	}

	// Token: 0x170006F4 RID: 1780
	// (get) Token: 0x06001956 RID: 6486 RVA: 0x000ED24C File Offset: 0x000EB44C
	public static string RouletteLose
	{
		get
		{
			return Language.sL.RouletteLose;
		}
	}

	// Token: 0x170006F5 RID: 1781
	// (get) Token: 0x06001957 RID: 6487 RVA: 0x000ED258 File Offset: 0x000EB458
	public static string RouletteTryAgain
	{
		get
		{
			return Language.sL.RouletteTryAgain;
		}
	}

	// Token: 0x170006F6 RID: 1782
	// (get) Token: 0x06001958 RID: 6488 RVA: 0x000ED264 File Offset: 0x000EB464
	public static string RouletteTriesEnded
	{
		get
		{
			return Language.sL.RouletteTriesEnded;
		}
	}

	// Token: 0x170006F7 RID: 1783
	// (get) Token: 0x06001959 RID: 6489 RVA: 0x000ED270 File Offset: 0x000EB470
	public static string RouletteWaitOrBuy
	{
		get
		{
			return Language.sL.RouletteWaitOrBuy;
		}
	}

	// Token: 0x170006F8 RID: 1784
	// (get) Token: 0x0600195A RID: 6490 RVA: 0x000ED27C File Offset: 0x000EB47C
	public static string RouletteSkill
	{
		get
		{
			return Language.sL.RouletteSkill;
		}
	}

	// Token: 0x170006F9 RID: 1785
	// (get) Token: 0x0600195B RID: 6491 RVA: 0x000ED288 File Offset: 0x000EB488
	public static string RouletteWeapon
	{
		get
		{
			return Language.sL.RouletteWeapon;
		}
	}

	// Token: 0x170006FA RID: 1786
	// (get) Token: 0x0600195C RID: 6492 RVA: 0x000ED294 File Offset: 0x000EB494
	public static string RoulettePopupHeader
	{
		get
		{
			return Language.sL.RoulettePopupHeader;
		}
	}

	// Token: 0x170006FB RID: 1787
	// (get) Token: 0x0600195D RID: 6493 RVA: 0x000ED2A0 File Offset: 0x000EB4A0
	public static string RoulettePopupBody
	{
		get
		{
			return Language.sL.RoulettePopupBody;
		}
	}

	// Token: 0x170006FC RID: 1788
	// (get) Token: 0x0600195E RID: 6494 RVA: 0x000ED2AC File Offset: 0x000EB4AC
	public static string RouletteAttempt
	{
		get
		{
			return Language.sL.RouletteAttempt;
		}
	}

	// Token: 0x170006FD RID: 1789
	// (get) Token: 0x0600195F RID: 6495 RVA: 0x000ED2B8 File Offset: 0x000EB4B8
	public static string RouletteCamo
	{
		get
		{
			return Language.sL.RouletteCamo;
		}
	}

	// Token: 0x170006FE RID: 1790
	// (get) Token: 0x06001960 RID: 6496 RVA: 0x000ED2C4 File Offset: 0x000EB4C4
	public static string RouletteOneAttempt
	{
		get
		{
			return Language.sL.RouletteOneAttempt;
		}
	}

	// Token: 0x170006FF RID: 1791
	// (get) Token: 0x06001961 RID: 6497 RVA: 0x000ED2D0 File Offset: 0x000EB4D0
	public static string RouletteCamouflage
	{
		get
		{
			return Language.sL.RouletteCamouflage;
		}
	}

	// Token: 0x17000700 RID: 1792
	// (get) Token: 0x06001962 RID: 6498 RVA: 0x000ED2DC File Offset: 0x000EB4DC
	public static string DataBaseFailure
	{
		get
		{
			return Language.sL.DataBaseFailure;
		}
	}

	// Token: 0x17000701 RID: 1793
	// (get) Token: 0x06001963 RID: 6499 RVA: 0x000ED2E8 File Offset: 0x000EB4E8
	public static string LevelFailure
	{
		get
		{
			return Language.sL.LevelFailure;
		}
	}

	// Token: 0x17000702 RID: 1794
	// (get) Token: 0x06001964 RID: 6500 RVA: 0x000ED2F4 File Offset: 0x000EB4F4
	public static string LeagueRank
	{
		get
		{
			return Language.sL.LeagueRank;
		}
	}

	// Token: 0x17000703 RID: 1795
	// (get) Token: 0x06001965 RID: 6501 RVA: 0x000ED300 File Offset: 0x000EB500
	public static string LeaguePlace
	{
		get
		{
			return Language.sL.LeaguePlace;
		}
	}

	// Token: 0x17000704 RID: 1796
	// (get) Token: 0x06001966 RID: 6502 RVA: 0x000ED30C File Offset: 0x000EB50C
	public static string LeagueLP
	{
		get
		{
			return Language.sL.LeagueLP;
		}
	}

	// Token: 0x17000705 RID: 1797
	// (get) Token: 0x06001967 RID: 6503 RVA: 0x000ED318 File Offset: 0x000EB518
	public static string LeagueWins
	{
		get
		{
			return Language.sL.LeagueWins;
		}
	}

	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x06001968 RID: 6504 RVA: 0x000ED324 File Offset: 0x000EB524
	public static string LeagueDefeats
	{
		get
		{
			return Language.sL.LeagueDefeats;
		}
	}

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x06001969 RID: 6505 RVA: 0x000ED330 File Offset: 0x000EB530
	public static string LeagueLeaves
	{
		get
		{
			return Language.sL.LeagueLeaves;
		}
	}

	// Token: 0x17000708 RID: 1800
	// (get) Token: 0x0600196A RID: 6506 RVA: 0x000ED33C File Offset: 0x000EB53C
	public static string LeagueRatio
	{
		get
		{
			return Language.sL.LeagueRatio;
		}
	}

	// Token: 0x17000709 RID: 1801
	// (get) Token: 0x0600196B RID: 6507 RVA: 0x000ED348 File Offset: 0x000EB548
	public static string LeagueRules
	{
		get
		{
			return Language.sL.LeagueRules;
		}
	}

	// Token: 0x1700070A RID: 1802
	// (get) Token: 0x0600196C RID: 6508 RVA: 0x000ED354 File Offset: 0x000EB554
	public static string LeagueCurrentPrizes
	{
		get
		{
			return Language.sL.LeagueCurrentPrizes;
		}
	}

	// Token: 0x1700070B RID: 1803
	// (get) Token: 0x0600196D RID: 6509 RVA: 0x000ED360 File Offset: 0x000EB560
	public static string LeaguePastPrizes
	{
		get
		{
			return Language.sL.LeaguePastPrizes;
		}
	}

	// Token: 0x1700070C RID: 1804
	// (get) Token: 0x0600196E RID: 6510 RVA: 0x000ED36C File Offset: 0x000EB56C
	public static string LeagueFuturePrizes
	{
		get
		{
			return Language.sL.LeagueFuturePrizes;
		}
	}

	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x0600196F RID: 6511 RVA: 0x000ED378 File Offset: 0x000EB578
	public static string LeagueRating
	{
		get
		{
			return Language.sL.LeagueRating;
		}
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x06001970 RID: 6512 RVA: 0x000ED384 File Offset: 0x000EB584
	public static string LeagueSearchGame
	{
		get
		{
			return Language.sL.LeagueSearchGame;
		}
	}

	// Token: 0x1700070F RID: 1807
	// (get) Token: 0x06001971 RID: 6513 RVA: 0x000ED390 File Offset: 0x000EB590
	public static string LeagueCancel
	{
		get
		{
			return Language.sL.LeagueCancel;
		}
	}

	// Token: 0x17000710 RID: 1808
	// (get) Token: 0x06001972 RID: 6514 RVA: 0x000ED39C File Offset: 0x000EB59C
	public static string LeagueBoosters
	{
		get
		{
			return Language.sL.LeagueBoosters;
		}
	}

	// Token: 0x17000711 RID: 1809
	// (get) Token: 0x06001973 RID: 6515 RVA: 0x000ED3A8 File Offset: 0x000EB5A8
	public static string LeagueSeasonEnd
	{
		get
		{
			return Language.sL.LeagueSeasonEnd;
		}
	}

	// Token: 0x17000712 RID: 1810
	// (get) Token: 0x06001974 RID: 6516 RVA: 0x000ED3B4 File Offset: 0x000EB5B4
	public static string LeagueSeasonBreak
	{
		get
		{
			return Language.sL.LeagueSeasonBreak;
		}
	}

	// Token: 0x17000713 RID: 1811
	// (get) Token: 0x06001975 RID: 6517 RVA: 0x000ED3C0 File Offset: 0x000EB5C0
	public static string LeagueRatingHeaderLvl
	{
		get
		{
			return Language.sL.LeagueRatingHeaderLvl;
		}
	}

	// Token: 0x17000714 RID: 1812
	// (get) Token: 0x06001976 RID: 6518 RVA: 0x000ED3CC File Offset: 0x000EB5CC
	public static string LeagueRatingHeaderNameNick
	{
		get
		{
			return Language.sL.LeagueRatingHeaderNameNick;
		}
	}

	// Token: 0x17000715 RID: 1813
	// (get) Token: 0x06001977 RID: 6519 RVA: 0x000ED3D8 File Offset: 0x000EB5D8
	public static string LeagueRatingHeaderLP
	{
		get
		{
			return Language.sL.LeagueRatingHeaderLP;
		}
	}

	// Token: 0x17000716 RID: 1814
	// (get) Token: 0x06001978 RID: 6520 RVA: 0x000ED3E4 File Offset: 0x000EB5E4
	public static string LeagueRatingHeaderWins
	{
		get
		{
			return Language.sL.LeagueRatingHeaderWins;
		}
	}

	// Token: 0x17000717 RID: 1815
	// (get) Token: 0x06001979 RID: 6521 RVA: 0x000ED3F0 File Offset: 0x000EB5F0
	public static string LeagueRatingHeaderDefeats
	{
		get
		{
			return Language.sL.LeagueRatingHeaderDefeats;
		}
	}

	// Token: 0x17000718 RID: 1816
	// (get) Token: 0x0600197A RID: 6522 RVA: 0x000ED3FC File Offset: 0x000EB5FC
	public static string LeagueRatingHeaderLeaves
	{
		get
		{
			return Language.sL.LeagueRatingHeaderLeaves;
		}
	}

	// Token: 0x17000719 RID: 1817
	// (get) Token: 0x0600197B RID: 6523 RVA: 0x000ED408 File Offset: 0x000EB608
	public static string LeagueSearching
	{
		get
		{
			return Language.sL.LeagueSearching;
		}
	}

	// Token: 0x1700071A RID: 1818
	// (get) Token: 0x0600197C RID: 6524 RVA: 0x000ED414 File Offset: 0x000EB614
	public static string LeagueInQueue
	{
		get
		{
			return Language.sL.LeagueInQueue;
		}
	}

	// Token: 0x1700071B RID: 1819
	// (get) Token: 0x0600197D RID: 6525 RVA: 0x000ED420 File Offset: 0x000EB620
	public static string LeaguePlaying
	{
		get
		{
			return Language.sL.LeaguePlaying;
		}
	}

	// Token: 0x1700071C RID: 1820
	// (get) Token: 0x0600197E RID: 6526 RVA: 0x000ED42C File Offset: 0x000EB62C
	public static string LeagueAdBoosters
	{
		get
		{
			return Language.sL.LeagueAdBoosters;
		}
	}

	// Token: 0x1700071D RID: 1821
	// (get) Token: 0x0600197F RID: 6527 RVA: 0x000ED438 File Offset: 0x000EB638
	public static string LeagueGameReady
	{
		get
		{
			return Language.sL.LeagueGameReady;
		}
	}

	// Token: 0x1700071E RID: 1822
	// (get) Token: 0x06001980 RID: 6528 RVA: 0x000ED444 File Offset: 0x000EB644
	public static string LeagueAccept
	{
		get
		{
			return Language.sL.LeagueAccept;
		}
	}

	// Token: 0x1700071F RID: 1823
	// (get) Token: 0x06001981 RID: 6529 RVA: 0x000ED450 File Offset: 0x000EB650
	public static string LeagueMap
	{
		get
		{
			return Language.sL.LeagueMap;
		}
	}

	// Token: 0x17000720 RID: 1824
	// (get) Token: 0x06001982 RID: 6530 RVA: 0x000ED45C File Offset: 0x000EB65C
	public static string LeagueMode
	{
		get
		{
			return Language.sL.LeagueMode;
		}
	}

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x06001983 RID: 6531 RVA: 0x000ED468 File Offset: 0x000EB668
	public static string LeagueMapLoading
	{
		get
		{
			return Language.sL.LeagueMapLoading;
		}
	}

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x06001984 RID: 6532 RVA: 0x000ED474 File Offset: 0x000EB674
	public static string LeagueGameStarted
	{
		get
		{
			return Language.sL.LeagueGameStarted;
		}
	}

	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x06001985 RID: 6533 RVA: 0x000ED480 File Offset: 0x000EB680
	public static string LeagueWaitingPlayers
	{
		get
		{
			return Language.sL.LeagueWaitingPlayers;
		}
	}

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x06001986 RID: 6534 RVA: 0x000ED48C File Offset: 0x000EB68C
	public static string LeagueMatchStart
	{
		get
		{
			return Language.sL.LeagueMatchStart;
		}
	}

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x06001987 RID: 6535 RVA: 0x000ED498 File Offset: 0x000EB698
	public static string LeagueGoneGameResult
	{
		get
		{
			return Language.sL.LeagueGoneGameResult;
		}
	}

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x06001988 RID: 6536 RVA: 0x000ED4A4 File Offset: 0x000EB6A4
	public static string LeagueYourTeamWon
	{
		get
		{
			return Language.sL.LeagueYourTeamWon;
		}
	}

	// Token: 0x17000727 RID: 1831
	// (get) Token: 0x06001989 RID: 6537 RVA: 0x000ED4B0 File Offset: 0x000EB6B0
	public static string LeagueYourTeamLose
	{
		get
		{
			return Language.sL.LeagueYourTeamLose;
		}
	}

	// Token: 0x17000728 RID: 1832
	// (get) Token: 0x0600198A RID: 6538 RVA: 0x000ED4BC File Offset: 0x000EB6BC
	public static string LeagueTie
	{
		get
		{
			return Language.sL.LeagueTie;
		}
	}

	// Token: 0x17000729 RID: 1833
	// (get) Token: 0x0600198B RID: 6539 RVA: 0x000ED4C8 File Offset: 0x000EB6C8
	public static string LeagueShare
	{
		get
		{
			return Language.sL.LeagueShare;
		}
	}

	// Token: 0x1700072A RID: 1834
	// (get) Token: 0x0600198C RID: 6540 RVA: 0x000ED4D4 File Offset: 0x000EB6D4
	public static string LeagueNext
	{
		get
		{
			return Language.sL.LeagueNext;
		}
	}

	// Token: 0x1700072B RID: 1835
	// (get) Token: 0x0600198D RID: 6541 RVA: 0x000ED4E0 File Offset: 0x000EB6E0
	public static string LeaguePointLeft
	{
		get
		{
			return Language.sL.LeaguePointLeft;
		}
	}

	// Token: 0x1700072C RID: 1836
	// (get) Token: 0x0600198E RID: 6542 RVA: 0x000ED4EC File Offset: 0x000EB6EC
	public static string LeagueYouLeave
	{
		get
		{
			return Language.sL.LeagueYouLeave;
		}
	}

	// Token: 0x1700072D RID: 1837
	// (get) Token: 0x0600198F RID: 6543 RVA: 0x000ED4F8 File Offset: 0x000EB6F8
	public static string LeagueNotAvailable
	{
		get
		{
			return Language.sL.LeagueNotAvailable;
		}
	}

	// Token: 0x1700072E RID: 1838
	// (get) Token: 0x06001990 RID: 6544 RVA: 0x000ED504 File Offset: 0x000EB704
	public static string LeaguePrizes
	{
		get
		{
			return Language.sL.LeaguePrizes;
		}
	}

	// Token: 0x1700072F RID: 1839
	// (get) Token: 0x06001991 RID: 6545 RVA: 0x000ED510 File Offset: 0x000EB710
	public static string LeagueNickname
	{
		get
		{
			return Language.sL.LeagueNickname;
		}
	}

	// Token: 0x17000730 RID: 1840
	// (get) Token: 0x06001992 RID: 6546 RVA: 0x000ED51C File Offset: 0x000EB71C
	public static string LeagueKills
	{
		get
		{
			return Language.sL.LeagueKills;
		}
	}

	// Token: 0x17000731 RID: 1841
	// (get) Token: 0x06001993 RID: 6547 RVA: 0x000ED528 File Offset: 0x000EB728
	public static string LeagueDeaths
	{
		get
		{
			return Language.sL.LeagueDeaths;
		}
	}

	// Token: 0x17000732 RID: 1842
	// (get) Token: 0x06001994 RID: 6548 RVA: 0x000ED534 File Offset: 0x000EB734
	public static string LeagueAssists
	{
		get
		{
			return Language.sL.LeagueAssists;
		}
	}

	// Token: 0x17000733 RID: 1843
	// (get) Token: 0x06001995 RID: 6549 RVA: 0x000ED540 File Offset: 0x000EB740
	public static string LeagueUnknown
	{
		get
		{
			return Language.sL.LeagueUnknown;
		}
	}

	// Token: 0x17000734 RID: 1844
	// (get) Token: 0x06001996 RID: 6550 RVA: 0x000ED54C File Offset: 0x000EB74C
	public static string LeagueOffSeason
	{
		get
		{
			return Language.sL.LeagueOffSeason;
		}
	}

	// Token: 0x17000735 RID: 1845
	// (get) Token: 0x06001997 RID: 6551 RVA: 0x000ED558 File Offset: 0x000EB758
	public static string LeagueWinners
	{
		get
		{
			return Language.sL.LeagueWinners;
		}
	}

	// Token: 0x17000736 RID: 1846
	// (get) Token: 0x06001998 RID: 6552 RVA: 0x000ED564 File Offset: 0x000EB764
	public static string LeaguePopupFirst
	{
		get
		{
			return Language.sL.LeaguePopupFirst;
		}
	}

	// Token: 0x17000737 RID: 1847
	// (get) Token: 0x06001999 RID: 6553 RVA: 0x000ED570 File Offset: 0x000EB770
	public static string LeaguePopupSecond
	{
		get
		{
			return Language.sL.LeaguePopupSecond;
		}
	}

	// Token: 0x17000738 RID: 1848
	// (get) Token: 0x0600199A RID: 6554 RVA: 0x000ED57C File Offset: 0x000EB77C
	public static string LeaguePopupThird
	{
		get
		{
			return Language.sL.LeaguePopupThird;
		}
	}

	// Token: 0x17000739 RID: 1849
	// (get) Token: 0x0600199B RID: 6555 RVA: 0x000ED588 File Offset: 0x000EB788
	public static string LeaguePopupYourResults
	{
		get
		{
			return Language.sL.LeaguePopupYourResults;
		}
	}

	// Token: 0x1700073A RID: 1850
	// (get) Token: 0x0600199C RID: 6556 RVA: 0x000ED594 File Offset: 0x000EB794
	public static string LeaguePopupYourRewards
	{
		get
		{
			return Language.sL.LeaguePopupYourRewards;
		}
	}

	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x0600199D RID: 6557 RVA: 0x000ED5A0 File Offset: 0x000EB7A0
	public static string LeaguePopupCongrats
	{
		get
		{
			return Language.sL.LeaguePopupCongrats;
		}
	}

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x0600199E RID: 6558 RVA: 0x000ED5AC File Offset: 0x000EB7AC
	public static string LeagueCurrent
	{
		get
		{
			return Language.sL.LeagueCurrent;
		}
	}

	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x0600199F RID: 6559 RVA: 0x000ED5B8 File Offset: 0x000EB7B8
	public static string LeaguePast
	{
		get
		{
			return Language.sL.LeaguePast;
		}
	}

	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x060019A0 RID: 6560 RVA: 0x000ED5C4 File Offset: 0x000EB7C4
	public static string LeagueFuture
	{
		get
		{
			return Language.sL.LeagueFuture;
		}
	}

	// Token: 0x1700073F RID: 1855
	// (get) Token: 0x060019A1 RID: 6561 RVA: 0x000ED5D0 File Offset: 0x000EB7D0
	public static string LeagueNotification1
	{
		get
		{
			return Language.sL.LeagueNotification1;
		}
	}

	// Token: 0x17000740 RID: 1856
	// (get) Token: 0x060019A2 RID: 6562 RVA: 0x000ED5DC File Offset: 0x000EB7DC
	public static string LeagueNotification2
	{
		get
		{
			return Language.sL.LeagueNotification2;
		}
	}

	// Token: 0x17000741 RID: 1857
	// (get) Token: 0x060019A3 RID: 6563 RVA: 0x000ED5E8 File Offset: 0x000EB7E8
	public static string League
	{
		get
		{
			return Language.sL.League;
		}
	}

	// Token: 0x17000742 RID: 1858
	// (get) Token: 0x060019A4 RID: 6564 RVA: 0x000ED5F4 File Offset: 0x000EB7F4
	public static string LeagueUpper
	{
		get
		{
			return Language.sL.LeagueUpper;
		}
	}

	// Token: 0x17000743 RID: 1859
	// (get) Token: 0x060019A5 RID: 6565 RVA: 0x000ED600 File Offset: 0x000EB800
	public static string LeagueLoading
	{
		get
		{
			return Language.sL.LeagueLoading;
		}
	}

	// Token: 0x17000744 RID: 1860
	// (get) Token: 0x060019A6 RID: 6566 RVA: 0x000ED60C File Offset: 0x000EB80C
	public static string LeagueAvailableHintStart
	{
		get
		{
			return Language.sL.LeagueAvailableHintStart;
		}
	}

	// Token: 0x17000745 RID: 1861
	// (get) Token: 0x060019A7 RID: 6567 RVA: 0x000ED618 File Offset: 0x000EB818
	public static string LeagueAvailableHintEnd
	{
		get
		{
			return Language.sL.LeagueAvailableHintEnd;
		}
	}

	// Token: 0x17000746 RID: 1862
	// (get) Token: 0x060019A8 RID: 6568 RVA: 0x000ED624 File Offset: 0x000EB824
	public static string Hours
	{
		get
		{
			return Language.sL.Hours;
		}
	}

	// Token: 0x17000747 RID: 1863
	// (get) Token: 0x060019A9 RID: 6569 RVA: 0x000ED630 File Offset: 0x000EB830
	public static string HightPacketLoss
	{
		get
		{
			return Language.sL.HightPacketLoss;
		}
	}

	// Token: 0x17000748 RID: 1864
	// (get) Token: 0x060019AA RID: 6570 RVA: 0x000ED63C File Offset: 0x000EB83C
	public static string HighPing
	{
		get
		{
			return Language.sL.HighPing;
		}
	}

	// Token: 0x17000749 RID: 1865
	// (get) Token: 0x060019AB RID: 6571 RVA: 0x000ED648 File Offset: 0x000EB848
	public static string GettingServerList
	{
		get
		{
			return Language.sL.GettingServerList;
		}
	}

	// Token: 0x1700074A RID: 1866
	// (get) Token: 0x060019AC RID: 6572 RVA: 0x000ED654 File Offset: 0x000EB854
	public static string Sorting
	{
		get
		{
			return Language.sL.Sorting;
		}
	}

	// Token: 0x1700074B RID: 1867
	// (get) Token: 0x060019AD RID: 6573 RVA: 0x000ED660 File Offset: 0x000EB860
	public static string GamelistIsNotAvailable
	{
		get
		{
			return Language.sL.GamelistIsNotAvailable;
		}
	}

	// Token: 0x1700074C RID: 1868
	// (get) Token: 0x060019AE RID: 6574 RVA: 0x000ED66C File Offset: 0x000EB86C
	public static string KillStreak
	{
		get
		{
			return Language.sL.KillStreak;
		}
	}

	// Token: 0x1700074D RID: 1869
	// (get) Token: 0x060019AF RID: 6575 RVA: 0x000ED678 File Offset: 0x000EB878
	public static string WeatherEffects
	{
		get
		{
			return Language.sL.WeatherEffects;
		}
	}

	// Token: 0x1700074E RID: 1870
	// (get) Token: 0x060019B0 RID: 6576 RVA: 0x000ED684 File Offset: 0x000EB884
	public static string UnityCaching
	{
		get
		{
			return Language.sL.UnityCaching;
		}
	}

	// Token: 0x1700074F RID: 1871
	// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000ED690 File Offset: 0x000EB890
	public static string ProKillScreenShotSetting
	{
		get
		{
			return Language.sL.ProKillScreenShotSetting;
		}
	}

	// Token: 0x17000750 RID: 1872
	// (get) Token: 0x060019B2 RID: 6578 RVA: 0x000ED69C File Offset: 0x000EB89C
	public static string QuadKillScreenShotSetting
	{
		get
		{
			return Language.sL.QuadKillScreenShotSetting;
		}
	}

	// Token: 0x17000751 RID: 1873
	// (get) Token: 0x060019B3 RID: 6579 RVA: 0x000ED6A8 File Offset: 0x000EB8A8
	public static string LevelUpScreenShotSetting
	{
		get
		{
			return Language.sL.LevelUpScreenShotSetting;
		}
	}

	// Token: 0x17000752 RID: 1874
	// (get) Token: 0x060019B4 RID: 6580 RVA: 0x000ED6B4 File Offset: 0x000EB8B4
	public static string AchievementScreenShotSetting
	{
		get
		{
			return Language.sL.AchievementScreenShotSetting;
		}
	}

	// Token: 0x17000753 RID: 1875
	// (get) Token: 0x060019B5 RID: 6581 RVA: 0x000ED6C0 File Offset: 0x000EB8C0
	public static string AutoScreenshotAt
	{
		get
		{
			return Language.sL.AutoScreenshotAt;
		}
	}

	// Token: 0x17000754 RID: 1876
	// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000ED6CC File Offset: 0x000EB8CC
	public static string GoFullscreen
	{
		get
		{
			return Language.sL.GoFullscreen;
		}
	}

	// Token: 0x17000755 RID: 1877
	// (get) Token: 0x060019B7 RID: 6583 RVA: 0x000ED6D8 File Offset: 0x000EB8D8
	public static string FriendsShort
	{
		get
		{
			return Language.sL.FriendsShort;
		}
	}

	// Token: 0x17000756 RID: 1878
	// (get) Token: 0x060019B8 RID: 6584 RVA: 0x000ED6E4 File Offset: 0x000EB8E4
	public static string FriendsOne
	{
		get
		{
			return Language.sL.FriendsOne;
		}
	}

	// Token: 0x17000757 RID: 1879
	// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000ED6F0 File Offset: 0x000EB8F0
	public static string FriendsSeveral
	{
		get
		{
			return Language.sL.FriendsSeveral;
		}
	}

	// Token: 0x17000758 RID: 1880
	// (get) Token: 0x060019BA RID: 6586 RVA: 0x000ED6FC File Offset: 0x000EB8FC
	public static string AddToFavorites
	{
		get
		{
			return Language.sL.AddToFavorites;
		}
	}

	// Token: 0x17000759 RID: 1881
	// (get) Token: 0x060019BB RID: 6587 RVA: 0x000ED708 File Offset: 0x000EB908
	public static string RemoveFromFavorites
	{
		get
		{
			return Language.sL.RemoveFromFavorites;
		}
	}

	// Token: 0x1700075A RID: 1882
	// (get) Token: 0x060019BC RID: 6588 RVA: 0x000ED714 File Offset: 0x000EB914
	public static string AddToFavoritesQuestion
	{
		get
		{
			return Language.sL.AddToFavoritesQuestion;
		}
	}

	// Token: 0x1700075B RID: 1883
	// (get) Token: 0x060019BD RID: 6589 RVA: 0x000ED720 File Offset: 0x000EB920
	public static string AddToFavoritesHint
	{
		get
		{
			return Language.sL.AddToFavoritesHint;
		}
	}

	// Token: 0x1700075C RID: 1884
	// (get) Token: 0x060019BE RID: 6590 RVA: 0x000ED72C File Offset: 0x000EB92C
	public static string RemoveFromFavoritesQuestion
	{
		get
		{
			return Language.sL.RemoveFromFavoritesQuestion;
		}
	}

	// Token: 0x1700075D RID: 1885
	// (get) Token: 0x060019BF RID: 6591 RVA: 0x000ED738 File Offset: 0x000EB938
	public static string AllowToAddMe
	{
		get
		{
			return Language.sL.AllowToAddMe;
		}
	}

	// Token: 0x1700075E RID: 1886
	// (get) Token: 0x060019C0 RID: 6592 RVA: 0x000ED744 File Offset: 0x000EB944
	public static string AddToFavoritesLimitReached
	{
		get
		{
			return Language.sL.AddToFavoritesLimitReached;
		}
	}

	// Token: 0x1700075F RID: 1887
	// (get) Token: 0x060019C1 RID: 6593 RVA: 0x000ED750 File Offset: 0x000EB950
	public static string AddToFavoritesDeniedByUser
	{
		get
		{
			return Language.sL.AddToFavoritesDeniedByUser;
		}
	}

	// Token: 0x17000760 RID: 1888
	// (get) Token: 0x060019C2 RID: 6594 RVA: 0x000ED75C File Offset: 0x000EB95C
	public static string HintRatingBtnTop
	{
		get
		{
			return Language.sL.HintRatingBtnTop;
		}
	}

	// Token: 0x17000761 RID: 1889
	// (get) Token: 0x060019C3 RID: 6595 RVA: 0x000ED768 File Offset: 0x000EB968
	public static string SeasonTop
	{
		get
		{
			return Language.sL.SeasonTop;
		}
	}

	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x060019C4 RID: 6596 RVA: 0x000ED774 File Offset: 0x000EB974
	public static string Season
	{
		get
		{
			return Language.sL.Season;
		}
	}

	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x060019C5 RID: 6597 RVA: 0x000ED780 File Offset: 0x000EB980
	public static string HintRatingBtnHardcore
	{
		get
		{
			return Language.sL.HintRatingBtnHardcore;
		}
	}

	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x060019C6 RID: 6598 RVA: 0x000ED78C File Offset: 0x000EB98C
	public static string HintRatingBtnTopOnline
	{
		get
		{
			return Language.sL.HintRatingBtnTopOnline;
		}
	}

	// Token: 0x17000765 RID: 1893
	// (get) Token: 0x060019C7 RID: 6599 RVA: 0x000ED798 File Offset: 0x000EB998
	public static string HintRatingBtnTopFriends
	{
		get
		{
			return Language.sL.HintRatingBtnTopFriends;
		}
	}

	// Token: 0x17000766 RID: 1894
	// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000ED7A4 File Offset: 0x000EB9A4
	public static string HintRatingBtnTopYourPosition
	{
		get
		{
			return Language.sL.HintRatingBtnTopYourPosition;
		}
	}

	// Token: 0x17000767 RID: 1895
	// (get) Token: 0x060019C9 RID: 6601 RVA: 0x000ED7B0 File Offset: 0x000EB9B0
	public static string HintRatingBtnFavorites
	{
		get
		{
			return Language.sL.HintRatingBtnFavorites;
		}
	}

	// Token: 0x17000768 RID: 1896
	// (get) Token: 0x060019CA RID: 6602 RVA: 0x000ED7BC File Offset: 0x000EB9BC
	public static string HintRatingBtnRefresh
	{
		get
		{
			return Language.sL.HintRatingBtnRefresh;
		}
	}

	// Token: 0x17000769 RID: 1897
	// (get) Token: 0x060019CB RID: 6603 RVA: 0x000ED7C8 File Offset: 0x000EB9C8
	public static string HintRatingBtnInfo
	{
		get
		{
			return Language.sL.HintRatingBtnInfo;
		}
	}

	// Token: 0x1700076A RID: 1898
	// (get) Token: 0x060019CC RID: 6604 RVA: 0x000ED7D4 File Offset: 0x000EB9D4
	public static string HintRatingBtnAddToFavorites
	{
		get
		{
			return Language.sL.HintRatingBtnAddToFavorites;
		}
	}

	// Token: 0x1700076B RID: 1899
	// (get) Token: 0x060019CD RID: 6605 RVA: 0x000ED7E0 File Offset: 0x000EB9E0
	public static string WatchlistLoadingTitle
	{
		get
		{
			return Language.sL.WatchlistLoadingTitle;
		}
	}

	// Token: 0x1700076C RID: 1900
	// (get) Token: 0x060019CE RID: 6606 RVA: 0x000ED7EC File Offset: 0x000EB9EC
	public static string WatchlistLoadingBody
	{
		get
		{
			return Language.sL.WatchlistLoadingTitle;
		}
	}

	// Token: 0x1700076D RID: 1901
	// (get) Token: 0x060019CF RID: 6607 RVA: 0x000ED7F8 File Offset: 0x000EB9F8
	public static string WatchlistLoadedBody
	{
		get
		{
			return Language.sL.WatchlistLoadedBody;
		}
	}

	// Token: 0x1700076E RID: 1902
	// (get) Token: 0x060019D0 RID: 6608 RVA: 0x000ED804 File Offset: 0x000EBA04
	public static string BankTransactions
	{
		get
		{
			return Language.sL.BankTransactions;
		}
	}

	// Token: 0x1700076F RID: 1903
	// (get) Token: 0x060019D1 RID: 6609 RVA: 0x000ED810 File Offset: 0x000EBA10
	public static string BankTransactionsLoading
	{
		get
		{
			return Language.sL.BankTransactionsLoading;
		}
	}

	// Token: 0x17000770 RID: 1904
	// (get) Token: 0x060019D2 RID: 6610 RVA: 0x000ED81C File Offset: 0x000EBA1C
	public static string BankTransactionsLoaded
	{
		get
		{
			return Language.sL.BankTransactionsLoaded;
		}
	}

	// Token: 0x17000771 RID: 1905
	// (get) Token: 0x060019D3 RID: 6611 RVA: 0x000ED828 File Offset: 0x000EBA28
	public static string RepairAllWeaponPopupHeader
	{
		get
		{
			return Language.sL.RepairAllWeaponPopupHeader;
		}
	}

	// Token: 0x17000772 RID: 1906
	// (get) Token: 0x060019D4 RID: 6612 RVA: 0x000ED834 File Offset: 0x000EBA34
	public static string RepairAllWeaponPopupBody1
	{
		get
		{
			return Language.sL.RepairAllWeaponPopupBody1;
		}
	}

	// Token: 0x17000773 RID: 1907
	// (get) Token: 0x060019D5 RID: 6613 RVA: 0x000ED840 File Offset: 0x000EBA40
	public static string RepairAllWeaponPopupBody2
	{
		get
		{
			return Language.sL.RepairAllWeaponPopupBody2;
		}
	}

	// Token: 0x17000774 RID: 1908
	// (get) Token: 0x060019D6 RID: 6614 RVA: 0x000ED84C File Offset: 0x000EBA4C
	public static string ProfileReset
	{
		get
		{
			return Language.sL.ProfileReset;
		}
	}

	// Token: 0x17000775 RID: 1909
	// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000ED858 File Offset: 0x000EBA58
	public static string ProfileResetNotification
	{
		get
		{
			return Language.sL.ProfileResetNotification;
		}
	}

	// Token: 0x17000776 RID: 1910
	// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000ED864 File Offset: 0x000EBA64
	public static string ProfileResetConfirmation0
	{
		get
		{
			return Language.sL.ProfileResetConfirmation0;
		}
	}

	// Token: 0x17000777 RID: 1911
	// (get) Token: 0x060019D9 RID: 6617 RVA: 0x000ED870 File Offset: 0x000EBA70
	public static string ProfileResetConfirmation1
	{
		get
		{
			return Language.sL.ProfileResetConfirmation1;
		}
	}

	// Token: 0x17000778 RID: 1912
	// (get) Token: 0x060019DA RID: 6618 RVA: 0x000ED87C File Offset: 0x000EBA7C
	public static string ProfileResetConfirmation2
	{
		get
		{
			return Language.sL.ProfileResetConfirmation2;
		}
	}

	// Token: 0x17000779 RID: 1913
	// (get) Token: 0x060019DB RID: 6619 RVA: 0x000ED888 File Offset: 0x000EBA88
	public static string MasteringPopupMetaBuyHeader
	{
		get
		{
			return Language.sL.MasteringPopupMetaBuyHeader;
		}
	}

	// Token: 0x1700077A RID: 1914
	// (get) Token: 0x060019DC RID: 6620 RVA: 0x000ED894 File Offset: 0x000EBA94
	public static string MasteringPopupMetaBuyBody
	{
		get
		{
			return Language.sL.MasteringPopupMetaBuyBody;
		}
	}

	// Token: 0x1700077B RID: 1915
	// (get) Token: 0x060019DD RID: 6621 RVA: 0x000ED8A0 File Offset: 0x000EBAA0
	public static string MasteringPopupModBuyHeader
	{
		get
		{
			return Language.sL.MasteringPopupModBuyHeader;
		}
	}

	// Token: 0x1700077C RID: 1916
	// (get) Token: 0x060019DE RID: 6622 RVA: 0x000ED8AC File Offset: 0x000EBAAC
	public static string MasteringPopupModBuyBody
	{
		get
		{
			return Language.sL.MasteringPopupModBuyBody;
		}
	}

	// Token: 0x1700077D RID: 1917
	// (get) Token: 0x060019DF RID: 6623 RVA: 0x000ED8B8 File Offset: 0x000EBAB8
	public static string Save
	{
		get
		{
			return Language.sL.Save;
		}
	}

	// Token: 0x1700077E RID: 1918
	// (get) Token: 0x060019E0 RID: 6624 RVA: 0x000ED8C4 File Offset: 0x000EBAC4
	public static string MasteringPopupSaveModHeader
	{
		get
		{
			return Language.sL.MasteringPopupSaveModHeader;
		}
	}

	// Token: 0x1700077F RID: 1919
	// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000ED8D0 File Offset: 0x000EBAD0
	public static string MasteringPopupSaveModProcess
	{
		get
		{
			return Language.sL.MasteringPopupSaveModProcess;
		}
	}

	// Token: 0x17000780 RID: 1920
	// (get) Token: 0x060019E2 RID: 6626 RVA: 0x000ED8DC File Offset: 0x000EBADC
	public static string MasteringPopupSaveModComplete
	{
		get
		{
			return Language.sL.MasteringPopupSaveModComplete;
		}
	}

	// Token: 0x17000781 RID: 1921
	// (get) Token: 0x060019E3 RID: 6627 RVA: 0x000ED8E8 File Offset: 0x000EBAE8
	public static string MasteringPopupSaveModError
	{
		get
		{
			return Language.sL.MasteringPopupSaveModError;
		}
	}

	// Token: 0x17000782 RID: 1922
	// (get) Token: 0x060019E4 RID: 6628 RVA: 0x000ED8F4 File Offset: 0x000EBAF4
	public static string MasteringPopupModBuyProcess
	{
		get
		{
			return Language.sL.MasteringPopupModBuyProcess;
		}
	}

	// Token: 0x17000783 RID: 1923
	// (get) Token: 0x060019E5 RID: 6629 RVA: 0x000ED900 File Offset: 0x000EBB00
	public static string MasteringPopupModBuyComplete
	{
		get
		{
			return Language.sL.MasteringPopupModBuyComplete;
		}
	}

	// Token: 0x17000784 RID: 1924
	// (get) Token: 0x060019E6 RID: 6630 RVA: 0x000ED90C File Offset: 0x000EBB0C
	public static string MasteringPopupMetaBuyProcess
	{
		get
		{
			return Language.sL.MasteringPopupMetaBuyProcess;
		}
	}

	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000ED918 File Offset: 0x000EBB18
	public static string MasteringPopupMetaBuyComplete
	{
		get
		{
			return Language.sL.MasteringPopupMetaBuyComplete;
		}
	}

	// Token: 0x17000786 RID: 1926
	// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000ED924 File Offset: 0x000EBB24
	public static string MasteringNotEnoughMp
	{
		get
		{
			return Language.sL.MasteringNotEnoughMp;
		}
	}

	// Token: 0x17000787 RID: 1927
	// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000ED930 File Offset: 0x000EBB30
	public static string NotEnoughGp
	{
		get
		{
			return Language.sL.NotEnoughGp;
		}
	}

	// Token: 0x17000788 RID: 1928
	// (get) Token: 0x060019EA RID: 6634 RVA: 0x000ED93C File Offset: 0x000EBB3C
	public static string NotEnoughCr
	{
		get
		{
			return Language.sL.NotEnoughCr;
		}
	}

	// Token: 0x17000789 RID: 1929
	// (get) Token: 0x060019EB RID: 6635 RVA: 0x000ED948 File Offset: 0x000EBB48
	public static string Sight
	{
		get
		{
			return Language.sL.Sight;
		}
	}

	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x060019EC RID: 6636 RVA: 0x000ED954 File Offset: 0x000EBB54
	public static string MuzzleDevice
	{
		get
		{
			return Language.sL.MuzzleDevice;
		}
	}

	// Token: 0x1700078B RID: 1931
	// (get) Token: 0x060019ED RID: 6637 RVA: 0x000ED960 File Offset: 0x000EBB60
	public static string TacticalDevice
	{
		get
		{
			return Language.sL.TacticalDevice;
		}
	}

	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x060019EE RID: 6638 RVA: 0x000ED96C File Offset: 0x000EBB6C
	public static string AmmoType
	{
		get
		{
			return Language.sL.AmmoType;
		}
	}

	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x060019EF RID: 6639 RVA: 0x000ED978 File Offset: 0x000EBB78
	public static string ModSlotUnavailable
	{
		get
		{
			return Language.sL.ModSlotUnavailable;
		}
	}

	// Token: 0x1700078E RID: 1934
	// (get) Token: 0x060019F0 RID: 6640 RVA: 0x000ED984 File Offset: 0x000EBB84
	public static string MasteringWtaskHint
	{
		get
		{
			return Language.sL.MasteringWtaskHint;
		}
	}

	// Token: 0x1700078F RID: 1935
	// (get) Token: 0x060019F1 RID: 6641 RVA: 0x000ED990 File Offset: 0x000EBB90
	public static string MasteringWeaponExp
	{
		get
		{
			return Language.sL.MasteringWeaponExp;
		}
	}

	// Token: 0x17000790 RID: 1936
	// (get) Token: 0x060019F2 RID: 6642 RVA: 0x000ED99C File Offset: 0x000EBB9C
	public static string WeaponCustomization
	{
		get
		{
			return Language.sL.WeaponCustomization;
		}
	}

	// Token: 0x17000791 RID: 1937
	// (get) Token: 0x060019F3 RID: 6643 RVA: 0x000ED9A8 File Offset: 0x000EBBA8
	public static string MpBuyError
	{
		get
		{
			return Language.sL.MpBuyError;
		}
	}

	// Token: 0x17000792 RID: 1938
	// (get) Token: 0x060019F4 RID: 6644 RVA: 0x000ED9B4 File Offset: 0x000EBBB4
	public static string Purchase
	{
		get
		{
			return Language.sL.Purchase;
		}
	}

	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x060019F5 RID: 6645 RVA: 0x000ED9C0 File Offset: 0x000EBBC0
	public static string GetFree
	{
		get
		{
			return Language.sL.GetFree;
		}
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x060019F6 RID: 6646 RVA: 0x000ED9CC File Offset: 0x000EBBCC
	public static string FreeCamouflage
	{
		get
		{
			return Language.sL.FreeCamouflage;
		}
	}

	// Token: 0x17000795 RID: 1941
	// (get) Token: 0x060019F7 RID: 6647 RVA: 0x000ED9D8 File Offset: 0x000EBBD8
	public static string PopupCamouflageBuyHeader
	{
		get
		{
			return Language.sL.PopupCamouflageBuyHeader;
		}
	}

	// Token: 0x17000796 RID: 1942
	// (get) Token: 0x060019F8 RID: 6648 RVA: 0x000ED9E4 File Offset: 0x000EBBE4
	public static string PopupCamouflageBuyBody
	{
		get
		{
			return Language.sL.PopupCamouflageBuyBody;
		}
	}

	// Token: 0x17000797 RID: 1943
	// (get) Token: 0x060019F9 RID: 6649 RVA: 0x000ED9F0 File Offset: 0x000EBBF0
	public static string PopupCamouflageBuyComplete
	{
		get
		{
			return Language.sL.PopupCamouflageBuyComplete;
		}
	}

	// Token: 0x17000798 RID: 1944
	// (get) Token: 0x060019FA RID: 6650 RVA: 0x000ED9FC File Offset: 0x000EBBFC
	public static string PopupCamouflageBuyProcess
	{
		get
		{
			return Language.sL.PopupCamouflageBuyProcess;
		}
	}

	// Token: 0x17000799 RID: 1945
	// (get) Token: 0x060019FB RID: 6651 RVA: 0x000EDA08 File Offset: 0x000EBC08
	public static string Equipped
	{
		get
		{
			return Language.sL.Equipped;
		}
	}

	// Token: 0x1700079A RID: 1946
	// (get) Token: 0x060019FC RID: 6652 RVA: 0x000EDA14 File Offset: 0x000EBC14
	public static string SortByName
	{
		get
		{
			return Language.sL.SortByName;
		}
	}

	// Token: 0x1700079B RID: 1947
	// (get) Token: 0x060019FD RID: 6653 RVA: 0x000EDA20 File Offset: 0x000EBC20
	public static string SortByPrice
	{
		get
		{
			return Language.sL.SortByPrice;
		}
	}

	// Token: 0x1700079C RID: 1948
	// (get) Token: 0x060019FE RID: 6654 RVA: 0x000EDA2C File Offset: 0x000EBC2C
	public static string RouletteDiscount1
	{
		get
		{
			return Language.sL.RouletteDiscount1;
		}
	}

	// Token: 0x1700079D RID: 1949
	// (get) Token: 0x060019FF RID: 6655 RVA: 0x000EDA38 File Offset: 0x000EBC38
	public static string RouletteDiscount2Part1
	{
		get
		{
			return Language.sL.RouletteDiscount2Part1;
		}
	}

	// Token: 0x1700079E RID: 1950
	// (get) Token: 0x06001A00 RID: 6656 RVA: 0x000EDA44 File Offset: 0x000EBC44
	public static string RouletteDiscount2Part2
	{
		get
		{
			return Language.sL.RouletteDiscount2Part2;
		}
	}

	// Token: 0x1700079F RID: 1951
	// (get) Token: 0x06001A01 RID: 6657 RVA: 0x000EDA50 File Offset: 0x000EBC50
	public static string RouletteDiscount3Part1
	{
		get
		{
			return Language.sL.RouletteDiscount3Part1;
		}
	}

	// Token: 0x170007A0 RID: 1952
	// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000EDA5C File Offset: 0x000EBC5C
	public static string RouletteDiscount3Part2
	{
		get
		{
			return Language.sL.RouletteDiscount3Part2;
		}
	}

	// Token: 0x170007A1 RID: 1953
	// (get) Token: 0x06001A03 RID: 6659 RVA: 0x000EDA68 File Offset: 0x000EBC68
	public static string Bonus
	{
		get
		{
			return Language.sL.Bonus;
		}
	}

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000EDA74 File Offset: 0x000EBC74
	public static string Achievement
	{
		get
		{
			return Language.sL.Achievement;
		}
	}

	// Token: 0x170007A3 RID: 1955
	// (get) Token: 0x06001A05 RID: 6661 RVA: 0x000EDA80 File Offset: 0x000EBC80
	public static string StandaloneLoginCaption
	{
		get
		{
			return Language.sL.StandaloneLoginCaption;
		}
	}

	// Token: 0x170007A4 RID: 1956
	// (get) Token: 0x06001A06 RID: 6662 RVA: 0x000EDA8C File Offset: 0x000EBC8C
	public static string StandaloneMailTextFieldCaption
	{
		get
		{
			return Language.sL.StandaloneMailTextFieldCaption;
		}
	}

	// Token: 0x170007A5 RID: 1957
	// (get) Token: 0x06001A07 RID: 6663 RVA: 0x000EDA98 File Offset: 0x000EBC98
	public static string StandalonePassTextFieldCaption
	{
		get
		{
			return Language.sL.StandalonePassTextFieldCaption;
		}
	}

	// Token: 0x170007A6 RID: 1958
	// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000EDAA4 File Offset: 0x000EBCA4
	public static string StandaloneSignUp
	{
		get
		{
			return Language.sL.StandaloneSignUp;
		}
	}

	// Token: 0x170007A7 RID: 1959
	// (get) Token: 0x06001A09 RID: 6665 RVA: 0x000EDAB0 File Offset: 0x000EBCB0
	public static string StandaloneSignIn
	{
		get
		{
			return Language.sL.StandaloneSignIn;
		}
	}

	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x06001A0A RID: 6666 RVA: 0x000EDABC File Offset: 0x000EBCBC
	public static string WrongLoginData
	{
		get
		{
			return Language.sL.WrongLoginData;
		}
	}

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x06001A0B RID: 6667 RVA: 0x000EDAC8 File Offset: 0x000EBCC8
	public static string ShowPassword
	{
		get
		{
			return Language.sL.ShowPassword;
		}
	}

	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x06001A0C RID: 6668 RVA: 0x000EDAD4 File Offset: 0x000EBCD4
	public static string RetypePassword
	{
		get
		{
			return Language.sL.RetypePassword;
		}
	}

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06001A0D RID: 6669 RVA: 0x000EDAE0 File Offset: 0x000EBCE0
	public static string LoginAttemptsExceeded
	{
		get
		{
			return Language.sL.LoginAttemptsExceeded;
		}
	}

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x06001A0E RID: 6670 RVA: 0x000EDAEC File Offset: 0x000EBCEC
	public static string Seconds
	{
		get
		{
			return Language.sL.Seconds;
		}
	}

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x06001A0F RID: 6671 RVA: 0x000EDAF8 File Offset: 0x000EBCF8
	public static string UnknownReasonLoginFail
	{
		get
		{
			return Language.sL.UnknownReasonLoginFail;
		}
	}

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x06001A10 RID: 6672 RVA: 0x000EDB04 File Offset: 0x000EBD04
	public static string TransferProfile
	{
		get
		{
			return Language.sL.TransferProfile;
		}
	}

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x06001A11 RID: 6673 RVA: 0x000EDB10 File Offset: 0x000EBD10
	public static string ProfileTransferedSuccessfully
	{
		get
		{
			return Language.sL.ProfileTransferedSuccessfully;
		}
	}

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x06001A12 RID: 6674 RVA: 0x000EDB1C File Offset: 0x000EBD1C
	public static string ProfileAlreadyTransfered
	{
		get
		{
			return Language.sL.ProfileAlreadyTransfered;
		}
	}

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x06001A13 RID: 6675 RVA: 0x000EDB28 File Offset: 0x000EBD28
	public static string ProfileTransferError
	{
		get
		{
			return Language.sL.ProfileTransferError;
		}
	}

	// Token: 0x170007B2 RID: 1970
	// (get) Token: 0x06001A14 RID: 6676 RVA: 0x000EDB34 File Offset: 0x000EBD34
	public static string NewLevel
	{
		get
		{
			return Language.sL.NewLevel;
		}
	}

	// Token: 0x170007B3 RID: 1971
	// (get) Token: 0x06001A15 RID: 6677 RVA: 0x000EDB40 File Offset: 0x000EBD40
	public static string PressToPost
	{
		get
		{
			return Language.sL.PressToPost;
		}
	}

	// Token: 0x170007B4 RID: 1972
	// (get) Token: 0x06001A16 RID: 6678 RVA: 0x000EDB4C File Offset: 0x000EBD4C
	public static string Copy
	{
		get
		{
			return Language.sL.Copy;
		}
	}

	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06001A17 RID: 6679 RVA: 0x000EDB58 File Offset: 0x000EBD58
	public static string Exit
	{
		get
		{
			return Language.sL.Exit;
		}
	}

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06001A18 RID: 6680 RVA: 0x000EDB64 File Offset: 0x000EBD64
	public static string VersionCheckCaption
	{
		get
		{
			return Language.sL.VersionCheckCaption;
		}
	}

	// Token: 0x170007B7 RID: 1975
	// (get) Token: 0x06001A19 RID: 6681 RVA: 0x000EDB70 File Offset: 0x000EBD70
	public static string VersionCheckDescription
	{
		get
		{
			return Language.sL.VersionCheckDescription;
		}
	}

	// Token: 0x170007B8 RID: 1976
	// (get) Token: 0x06001A1A RID: 6682 RVA: 0x000EDB7C File Offset: 0x000EBD7C
	public static string Lang
	{
		get
		{
			return Language.sL.Language;
		}
	}

	// Token: 0x170007B9 RID: 1977
	// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000EDB88 File Offset: 0x000EBD88
	public static string RusLanguage
	{
		get
		{
			return Language.sL.RusLanguage;
		}
	}

	// Token: 0x170007BA RID: 1978
	// (get) Token: 0x06001A1C RID: 6684 RVA: 0x000EDB94 File Offset: 0x000EBD94
	public static string EngLanguage
	{
		get
		{
			return Language.sL.EngLanguage;
		}
	}

	// Token: 0x170007BB RID: 1979
	// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000EDBA0 File Offset: 0x000EBDA0
	public static string SavePassword
	{
		get
		{
			return Language.sL.SavePassword;
		}
	}

	// Token: 0x170007BC RID: 1980
	// (get) Token: 0x06001A1E RID: 6686 RVA: 0x000EDBAC File Offset: 0x000EBDAC
	public static string AreYouSure
	{
		get
		{
			return Language.sL.AreYouSure;
		}
	}

	// Token: 0x170007BD RID: 1981
	// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000EDBB8 File Offset: 0x000EBDB8
	public static string[] AwardsHints
	{
		get
		{
			return Language.sL.AwardsHints;
		}
	}

	// Token: 0x170007BE RID: 1982
	// (get) Token: 0x06001A20 RID: 6688 RVA: 0x000EDBC4 File Offset: 0x000EBDC4
	public static string SeasonAward
	{
		get
		{
			return Language.sL.SeasonAward;
		}
	}

	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x06001A21 RID: 6689 RVA: 0x000EDBD0 File Offset: 0x000EBDD0
	public static string SeasonAwardDescription
	{
		get
		{
			return Language.sL.SeasonAwardDescription;
		}
	}

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x06001A22 RID: 6690 RVA: 0x000EDBDC File Offset: 0x000EBDDC
	public static string MipmapCheckFailCaption
	{
		get
		{
			return Language.sL.MipmapCheckFailCaption;
		}
	}

	// Token: 0x170007C1 RID: 1985
	// (get) Token: 0x06001A23 RID: 6691 RVA: 0x000EDBE8 File Offset: 0x000EBDE8
	public static string MipmapCheckFailDescription
	{
		get
		{
			return Language.sL.MipmapCheckFailDescription;
		}
	}

	// Token: 0x170007C2 RID: 1986
	// (get) Token: 0x06001A24 RID: 6692 RVA: 0x000EDBF4 File Offset: 0x000EBDF4
	public static string HopsKeyWonCaption
	{
		get
		{
			return Language.sL.HopsKeyWonCaption;
		}
	}

	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x06001A25 RID: 6693 RVA: 0x000EDC00 File Offset: 0x000EBE00
	public static string HopsKeyWonDescription
	{
		get
		{
			return Language.sL.HopsKeyWonDescription;
		}
	}

	// Token: 0x170007C4 RID: 1988
	// (get) Token: 0x06001A26 RID: 6694 RVA: 0x000EDC0C File Offset: 0x000EBE0C
	public static string HopsActivationInstruction
	{
		get
		{
			return Language.sL.HopsActivationInstruction;
		}
	}

	// Token: 0x04001EC9 RID: 7881
	public static string jSon = string.Empty;

	// Token: 0x04001ECA RID: 7882
	private static BaseLanguage eng = new ENGLang();

	// Token: 0x04001ECB RID: 7883
	private static BaseLanguage rus = new BaseLanguage();

	// Token: 0x04001ECC RID: 7884
	private static BaseLanguage sL = new BaseLanguage();

	// Token: 0x04001ECD RID: 7885
	private static string file = "ENGJson.txt";

	// Token: 0x04001ECE RID: 7886
	private static Dictionary<string, object> dict = new Dictionary<string, object>();

	// Token: 0x04001ECF RID: 7887
	private static string[] _languages;
}
