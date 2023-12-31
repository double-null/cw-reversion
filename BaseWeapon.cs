using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001A6 RID: 422
[AddComponentMenu("Scripts/Game/BaseWeapon")]
public class BaseWeapon : PoolableBehaviour
{
	// Token: 0x170001D0 RID: 464
	// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0009FA44 File Offset: 0x0009DC44
	internal bool IsAbakan
	{
		get
		{
			return this.type == Weapons.an94;
		}
	}

	// Token: 0x170001D1 RID: 465
	// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0009FA50 File Offset: 0x0009DC50
	private float maxAccuracy
	{
		get
		{
			if (this.weaponNature == WeaponNature.shotgun && !this.Slug)
			{
				return 2f;
			}
			return 3.5f;
		}
	}

	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0009FA84 File Offset: 0x0009DC84
	private float minAccuracy
	{
		get
		{
			return 0.04f;
		}
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x0009FA8C File Offset: 0x0009DC8C
	public override void OnPoolDespawn()
	{
		this.FirstReload = true;
		this.LOD = false;
		this.weaponPrefab = null;
		this.markTexture = null;
		this.shellName = string.Empty;
		this.FireAnimations = 0;
		this.FireAimAnimations = 0;
		this.muzzleFlashName = string.Empty;
		this.silencedMuzzleFlashName = string.Empty;
		this.boltReloadSounds = null;
		this.reloadSounds = new AudioClip[0];
		this.reloadTimes = null;
		this.reloadBools = null;
		this.fireSounds = new AudioClip[0];
		this.secondSoundScheme = false;
		this.singleTime = 0.088f;
		this.loopFire = null;
		this.tailFire = null;
		this.modFireSounds = new AudioClip[0];
		this.modSecondSoundScheme = false;
		this.modSingleTime = 0.088f;
		this.modLoopFire = null;
		this.modTailFire = null;
		this.equipTime = 0f;
		this.equip = null;
		this.correct = false;
		this.rootPositionCorrect = Vector3.zero;
		this.rootRotationCorrect = new Vector3(90f, 0f, 0f);
		this.correctAlways = false;
		this.type = Weapons.none;
		this.InterfaceName = string.Empty;
		this.ModInterfaceName = string.Empty;
		this.weaponNature = WeaponNature.none;
		this.auto = false;
		this.armoryBlock = 0;
		this.weaponUseType = WeaponUseType.none;
		this.pompovik = false;
		this.Name = null;
		this.ShortName = null;
		this.Info = null;
		this.price = 0;
		this.IndestructiblePrice = 0;
		this.durability = 1;
		this.wtaskPrice = 1000;
		this.distanceReducerE1 = 1f;
		this.distanceReducerE2 = 1f;
		this.isPremium = false;
		this.isDonate = false;
		this.rentPrice = null;
		this.rentTime = null;
		this.isSocial = false;
		this.friendsRequired = -1;
		this.permanentPrice = 0;
		this.WtaskGearID = 0;
		this.wtask = null;
		this.WtaskName = string.Empty;
		this.WtaskDescription = string.Empty;
		this.WtaskGearName = string.Empty;
		this.WtaskGearDesc = string.Empty;
		this.WtaskInfo = string.Empty;
		this.WtaskAmmo = string.Empty;
		this.weaponSpecific = WeaponSpecific.none;
		this.focalDistance = 0.29f;
		this.ammoString = string.Empty;
		this.GridPosition = new int[2];
		this.state = new WeaponState();
		this.G = new Invoker();
		this.player = null;
		this.burst = false;
		this.duplet = false;
		this.canDuplet = false;
		this.isBolt = false;
		this.stab = false;
		this.stab_now = new eTimer();
		this.caseshot = 1;
		this._sensMult = 1f;
		this.hearRadius = -1f;
		this.skillReq = -1;
		this.unlockBySkill = false;
		this.magSize = 0;
		this.bagSize = 0;
		this.repair_coef = 1f;
		this.accuracyHitFade = 1f;
		this.accuracyFade = new eTimer();
		this.accuracyFader = 1f;
		this.first100Accuracy = false;
		this.first100Accuracy_now = First100Accuracy.none;
		this.recoilSeatMult = 1f;
		this.aimRecoilMult = 1f;
		this.seatAccuracyMult = 1f;
		this.walkAccuracyMult = 1f;
		this.aimFov = 30f;
		this.worldAimFov = 5f;
		this.TDP = 0.3f;
		this.TYP = 0.4f;
		this.TDA = 0.4f;
		this.TYA = 0.4f;
		this.OutIdleMult = 1f;
		this.IdleOutMult = 1f;
		this.IdleAimMult = 1f;
		this.AimIdleMult = 1f;
		this.AIMP = 0.2f;
		this.AIMA = 0.2f;
		this.hasOptic = false;
		this.hasLTP = false;
		this.hasKolimator = false;
		this.hasGrip = false;
		this.hasSS = false;
		this.hasCompensator = false;
		this.hasSlug = false;
		this.hasFlashlight = false;
		this.modHasOptic = false;
		this.modHasLTP = false;
		this.modHasKolimator = false;
		this.modHasGrip = false;
		this.modHasSS = false;
		this.modHasCompensator = false;
		this.modHasSlug = false;
		this.modHasFlashlight = false;
		this.minRecoil = 0.2f;
		this.maxRecoil = 1.3f;
		this.baseAccuracyProc = 0f;
		this.baseRecoilProc = 0f;
		this.baseDamageProc = 0f;
		this.baseFirerateProc = 0f;
		this.baseMobilityProc = 0f;
		this.baseReloadSpeedProc = 0f;
		this.basePierceProc = 75f;
		this.damageReduceDistanceMin = 1000f;
		this.damageReduceDistanceMax = 10000f;
		this.modAccuracyProcBonus = 0f;
		this.modRecoilProcBonus = 0f;
		this.modDamageProcBonus = 0f;
		this.modFirerateProcBonus = 0f;
		this.modMobilityProcBonus = 0f;
		this.modReloadSpeedProcBonus = 0f;
		this.modPierceProcBonus = 0f;
		this.skillAccuracyProcBonus = 0f;
		this.skillRecoilProcBonus = 0f;
		this.skillDamageProcBonus = 0f;
		this.skillFirerateProcBonus = 0f;
		this.skillMobilityProcBonus = 0f;
		this.skillReloadSpeedProcBonus = 0f;
		this.skillPierceProcBonus = 0f;
		this.damageReduceDistanceMinSkillBonus = 0f;
		this.damageReduceDistanceMaxSkillBonus = 0f;
		this.UseShotGunReload = false;
		this.scatter = 50f;
		base.OnPoolDespawn();
	}

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0009FFFC File Offset: 0x0009E1FC
	internal float HearRadius
	{
		get
		{
			return (!this.IsMod || !this.modHasSS) ? this.hearRadius : (this.hearRadius / 3f);
		}
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x000A002C File Offset: 0x0009E22C
	internal int GetRentPrice(int option)
	{
		if (this.type == (Weapons)Main.UserInfo.discount_id)
		{
			return Mathf.FloorToInt((float)this.rentPrice[option] - (float)this.rentPrice[option] * (float)Main.UserInfo.discount / 100f);
		}
		return this.rentPrice[option];
	}

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06000D76 RID: 3446 RVA: 0x000A0084 File Offset: 0x0009E284
	internal int getPermanentPrice
	{
		get
		{
			if (this.type == (Weapons)Main.UserInfo.discount_id)
			{
				return Mathf.FloorToInt((float)this.permanentPrice - (float)this.permanentPrice * (float)Main.UserInfo.discount / 100f);
			}
			return this.permanentPrice;
		}
	}

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06000D77 RID: 3447 RVA: 0x000A00D4 File Offset: 0x0009E2D4
	internal bool Empty
	{
		get
		{
			return this.state.clips == 0 && this.state.bagSize == 0;
		}
	}

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06000D78 RID: 3448 RVA: 0x000A00F8 File Offset: 0x0009E2F8
	internal int BagSize
	{
		get
		{
			return this.state.bagSize;
		}
	}

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06000D79 RID: 3449 RVA: 0x000A0108 File Offset: 0x0009E308
	internal float OutIdle
	{
		get
		{
			return this.TDS * this.OutIdleMult;
		}
	}

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06000D7A RID: 3450 RVA: 0x000A0118 File Offset: 0x0009E318
	internal float IdleOut
	{
		get
		{
			return this.TYS * this.IdleOutMult;
		}
	}

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06000D7B RID: 3451 RVA: 0x000A0128 File Offset: 0x0009E328
	internal float IdleAimTime
	{
		get
		{
			return this.AIMS * this.IdleAimMult;
		}
	}

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000A0138 File Offset: 0x0009E338
	internal float AimIdleTime
	{
		get
		{
			return this.AIMS * this.AimIdleMult;
		}
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000A0148 File Offset: 0x0009E348
	internal bool isUndestructable
	{
		get
		{
			return this.isPremium || this.isSocial || this.isDonate || this.type == Weapons.pm || this.state.repair_info == -77f;
		}
	}

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000A0198 File Offset: 0x0009E398
	internal bool Damaged
	{
		get
		{
			return !this.isUndestructable && this.state.repair_info >= (float)this.durability;
		}
	}

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06000D7F RID: 3455 RVA: 0x000A01CC File Offset: 0x0009E3CC
	internal bool AlwaysGroup
	{
		get
		{
			return this.G.IsInvoking("PostFire") || this.G.IsInvoking("PostReload");
		}
	}

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06000D80 RID: 3456 RVA: 0x000A0204 File Offset: 0x0009E404
	private bool BreakableGroup
	{
		get
		{
			return this.G.IsInvoking("PostFire") || this.G.IsInvoking("PostReload");
		}
	}

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06000D81 RID: 3457 RVA: 0x000A023C File Offset: 0x0009E43C
	private float TDS
	{
		get
		{
			if (this.weaponUseType == WeaponUseType.Secondary)
			{
				return this.TDP + this.TDP * (1f - this.baseMobilityProc / 100f);
			}
			return this.TDA + this.TDA * (1f - this.baseMobilityProc / 100f);
		}
	}

	// Token: 0x170001E0 RID: 480
	// (get) Token: 0x06000D82 RID: 3458 RVA: 0x000A0298 File Offset: 0x0009E498
	private float TYS
	{
		get
		{
			if (this.weaponUseType == WeaponUseType.Secondary)
			{
				return this.TYP + this.TYP * (1f - this.baseMobilityProc / 100f);
			}
			return this.TYA + this.TYA * (1f - this.baseMobilityProc / 100f);
		}
	}

	// Token: 0x170001E1 RID: 481
	// (get) Token: 0x06000D83 RID: 3459 RVA: 0x000A02F4 File Offset: 0x0009E4F4
	private float AIMS
	{
		get
		{
			if (this.weaponUseType == WeaponUseType.Secondary)
			{
				return this.AIMP + this.AIMP * (1f - this.baseMobilityProc / 100f);
			}
			return this.AIMA + this.AIMA * (1f - this.baseMobilityProc / 100f);
		}
	}

	// Token: 0x170001E2 RID: 482
	// (get) Token: 0x06000D84 RID: 3460 RVA: 0x000A0350 File Offset: 0x0009E550
	// (set) Token: 0x06000D85 RID: 3461 RVA: 0x000A0360 File Offset: 0x0009E560
	internal bool IsMod
	{
		get
		{
			return this.state.isMod;
		}
		set
		{
			this.state.isMod = value;
		}
	}

	// Token: 0x170001E3 RID: 483
	// (get) Token: 0x06000D86 RID: 3462 RVA: 0x000A0370 File Offset: 0x0009E570
	internal string guiInterfaceName
	{
		get
		{
			if (this.IsMod && !Utility.IsModableWeapon((int)this.type))
			{
				return this.ModInterfaceName;
			}
			return this.InterfaceName;
		}
	}

	// Token: 0x170001E4 RID: 484
	// (get) Token: 0x06000D87 RID: 3463 RVA: 0x000A03A8 File Offset: 0x0009E5A8
	internal bool showMarks
	{
		get
		{
			return this.IsMod || !this.modHasKolimator || this.hasKolimator;
		}
	}

	// Token: 0x170001E5 RID: 485
	// (get) Token: 0x06000D88 RID: 3464 RVA: 0x000A03DC File Offset: 0x0009E5DC
	internal float SensitivityMult
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.optic);
				if (mod != null)
				{
					return mod.Sensitivity;
				}
			}
			return this._sensMult;
		}
	}

	// Token: 0x170001E6 RID: 486
	// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000A0414 File Offset: 0x0009E614
	internal bool Optic
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.optic);
				return mod != null && mod.IsOpticSight;
			}
			return (!this.IsMod) ? this.hasOptic : this.modHasOptic;
		}
	}

	// Token: 0x170001E7 RID: 487
	// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000A0468 File Offset: 0x0009E668
	internal bool Hybrid
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.optic);
				return mod != null && mod.IsHybridSight;
			}
			return (!this.IsMod) ? this.hasOptic : this.modHasOptic;
		}
	}

	// Token: 0x170001E8 RID: 488
	// (get) Token: 0x06000D8B RID: 3467 RVA: 0x000A04BC File Offset: 0x0009E6BC
	// (set) Token: 0x06000D8C RID: 3468 RVA: 0x000A04C4 File Offset: 0x0009E6C4
	internal bool OpticZoomed { get; set; }

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x06000D8D RID: 3469 RVA: 0x000A04D0 File Offset: 0x0009E6D0
	internal bool LTP
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.tactical);
				return mod != null && mod.IsLtp;
			}
			return (!this.IsMod) ? this.hasLTP : this.modHasLTP;
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06000D8E RID: 3470 RVA: 0x000A0524 File Offset: 0x0009E724
	internal bool Kolimator
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.optic);
				return mod != null && mod.IsCollimatorSight;
			}
			return (!this.IsMod) ? this.hasKolimator : this.modHasKolimator;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06000D8F RID: 3471 RVA: 0x000A0578 File Offset: 0x0009E778
	internal bool Grip
	{
		get
		{
			return (!this.IsMod) ? this.hasGrip : this.modHasGrip;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06000D90 RID: 3472 RVA: 0x000A0598 File Offset: 0x0009E798
	internal bool SS
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.silencer);
				return mod != null && mod.IsSilencer;
			}
			return (!this.IsMod) ? this.hasSS : this.modHasSS;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06000D91 RID: 3473 RVA: 0x000A05EC File Offset: 0x0009E7EC
	internal bool FlashHider
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.silencer);
				return mod != null && mod.IsFlashHider;
			}
			return (!this.IsMod) ? this.hasSS : this.modHasSS;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06000D92 RID: 3474 RVA: 0x000A0640 File Offset: 0x0009E840
	internal bool Compensator
	{
		get
		{
			return (!this.IsMod) ? this.hasCompensator : this.modHasCompensator;
		}
	}

	// Token: 0x170001EF RID: 495
	// (get) Token: 0x06000D93 RID: 3475 RVA: 0x000A0660 File Offset: 0x0009E860
	internal bool Slug
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type))
			{
				MasteringMod mod = this.GetMod(ModType.ammo);
				return mod != null && mod.IsBullet;
			}
			return (!this.IsMod) ? this.hasSlug : this.modHasSlug;
		}
	}

	// Token: 0x170001F0 RID: 496
	// (get) Token: 0x06000D94 RID: 3476 RVA: 0x000A06B4 File Offset: 0x0009E8B4
	internal bool Flashlight
	{
		get
		{
			return (!this.IsMod) ? this.hasFlashlight : this.modHasFlashlight;
		}
	}

	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x06000D95 RID: 3477 RVA: 0x000A06D4 File Offset: 0x0009E8D4
	internal bool IsPrimary
	{
		get
		{
			return this.weaponUseType == WeaponUseType.Primary;
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x06000D96 RID: 3478 RVA: 0x000A06E0 File Offset: 0x0009E8E0
	internal bool IsSecondary
	{
		get
		{
			return this.weaponUseType == WeaponUseType.Secondary;
		}
	}

	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x06000D97 RID: 3479 RVA: 0x000A06EC File Offset: 0x0009E8EC
	internal float SpeedReducer
	{
		get
		{
			return this.speedReducer;
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000A06F4 File Offset: 0x0009E8F4
	internal float ReloadTime
	{
		get
		{
			return this.reloadSpeed;
		}
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06000D99 RID: 3481 RVA: 0x000A06FC File Offset: 0x0009E8FC
	internal float CurrentAccuracy
	{
		get
		{
			float num = 1f;
			try
			{
				if (this.player.Controller.isMoving)
				{
					num *= this.maxAccuracy;
				}
				if (this.player.Ammo.IsAim)
				{
					num *= 0.5f;
				}
				if (Peer.HardcoreMode && !this.player.Ammo.IsAim)
				{
					num += 2f;
				}
				if (!Peer.HardcoreMode && !this.player.Ammo.IsAim && this.player.Ammo.CurrentWeapon.weaponNature == WeaponNature.sniper_rifle)
				{
					num += 3f;
				}
				if (this.player.Controller.isSeat)
				{
					num *= Mathf.Min(0.65f, this.seatAccuracyMult);
				}
				else if (this.player.Controller.isWalk)
				{
					num *= Mathf.Min(0.75f, this.walkAccuracyMult);
				}
				if (!this.player.Controller.IsGrounded && !this.player.Ammo.CurrentWeapon.isBolt)
				{
					num *= 5f;
				}
				if (this.weaponNature == WeaponNature.shotgun && !this.Slug)
				{
					num *= this.scatter;
				}
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
			if (this.first100Accuracy && this.first100Accuracy_now != First100Accuracy.need_reload && this.weaponNature != WeaponNature.shotgun)
			{
				return 0f;
			}
			if (this.player.Ammo.CurrentWeapon.isBolt && this.player.Ammo.IsAim && this.player.Controller.isMoving)
			{
				return Mathf.Min((this.accuracy + this.currentAccuracy) * num, this.maxAccuracy) * this.accuracyHitFade;
			}
			if (this.player.Controller.isMoving)
			{
				return Mathf.Min(num, this.maxAccuracy) * this.accuracyHitFade;
			}
			return Mathf.Min((this.accuracy + this.currentAccuracy) * num, this.maxAccuracy) * this.accuracyHitFade;
		}
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x000A0974 File Offset: 0x0009EB74
	private float recoilAdder(bool IsAim, bool IsSeat, bool IsWalk)
	{
		float num = 1f;
		if (IsAim)
		{
			num = num / 2f * this.aimRecoilMult;
		}
		if (IsSeat)
		{
			num = num / 1.5f * this.recoilSeatMult;
		}
		else if (IsWalk)
		{
			num /= 1.5f;
		}
		return this.minRecoil + this.recoil * (this.maxRecoil - this.minRecoil) * num;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x000A09E4 File Offset: 0x0009EBE4
	internal float getDistanceReducer(float distance)
	{
		if (distance <= this.DamageReduceDistanceMin)
		{
			return this.distanceReducerE1;
		}
		if (distance >= this.DamageReduceDistanceMax)
		{
			return this.distanceReducerE2;
		}
		if (this.DamageReduceDistanceMin != this.DamageReduceDistanceMax)
		{
			return this.distanceReducerE1 + (distance - this.DamageReduceDistanceMin) * (this.distanceReducerE2 - this.distanceReducerE1) / (this.DamageReduceDistanceMax - this.DamageReduceDistanceMin);
		}
		return 1f;
	}

	// Token: 0x170001F6 RID: 502
	// (get) Token: 0x06000D9C RID: 3484 RVA: 0x000A0A5C File Offset: 0x0009EC5C
	internal float fireDelay
	{
		get
		{
			return (float)((int)(60f / this.firerate * 1000f)) / 1000f;
		}
	}

	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000A0A78 File Offset: 0x0009EC78
	private float speedReducer
	{
		get
		{
			return (100f - this.mobility) * 1.2f / 100f;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06000D9E RID: 3486 RVA: 0x000A0A94 File Offset: 0x0009EC94
	internal float DamageReduceDistanceMin
	{
		get
		{
			return this.damageReduceDistanceMin + this.damageReduceDistanceMinSkillBonus + this.damageReduceDistanceMinModBonus;
		}
	}

	// Token: 0x170001F9 RID: 505
	// (get) Token: 0x06000D9F RID: 3487 RVA: 0x000A0AAC File Offset: 0x0009ECAC
	internal float DamageReduceDistanceMax
	{
		get
		{
			return this.damageReduceDistanceMax + this.damageReduceDistanceMaxSkillBonus + this.damageReduceDistanceMaxModBonus;
		}
	}

	// Token: 0x170001FA RID: 506
	// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x000A0AC4 File Offset: 0x0009ECC4
	internal float AccuracyProcWithoutDurab
	{
		get
		{
			if (this.baseAccuracyProc + ((!this.IsMod) ? 0f : this.modAccuracyProcBonus) + this.skillAccuracyProcBonus > 100f)
			{
				return 100f;
			}
			return this.baseAccuracyProc + ((!this.IsMod) ? 0f : this.modAccuracyProcBonus) + this.skillAccuracyProcBonus;
		}
	}

	// Token: 0x170001FB RID: 507
	// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x000A0B34 File Offset: 0x0009ED34
	internal float AccuracyProc
	{
		get
		{
			float num = 1f - this.state.repair_info / (float)this.durability;
			if (this.isUndestructable)
			{
				num = 1f;
			}
			return Mathf.Max(20f, Mathf.Min(this.baseAccuracyProc + this.modAccuracyProcBonus + this.skillAccuracyProcBonus + ((!Peer.HardcoreMode) ? 0f : CVars.g_hardcoreAccuracyIncrease), 100f) * num);
		}
	}

	// Token: 0x170001FC RID: 508
	// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x000A0BB0 File Offset: 0x0009EDB0
	internal float RecoilProc
	{
		get
		{
			return Mathf.Max(1f, this.baseRecoilProc + this.modRecoilProcBonus + this.skillRecoilProcBonus + ((!Peer.HardcoreMode) ? 0f : CVars.g_hardcoreRecoilIncrease));
		}
	}

	// Token: 0x170001FD RID: 509
	// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x000A0BF8 File Offset: 0x0009EDF8
	internal float DamageProc
	{
		get
		{
			return Mathf.Max(1f, this.baseDamageProc + this.modDamageProcBonus + this.skillDamageProcBonus);
		}
	}

	// Token: 0x170001FE RID: 510
	// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x000A0C18 File Offset: 0x0009EE18
	// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x000A0C20 File Offset: 0x0009EE20
	internal float ShotGroupingProc { get; private set; }

	// Token: 0x170001FF RID: 511
	// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x000A0C2C File Offset: 0x0009EE2C
	internal float FirerateProc
	{
		get
		{
			return Mathf.Max(1f, this.baseFirerateProc + this.modFirerateProcBonus + this.skillFirerateProcBonus);
		}
	}

	// Token: 0x17000200 RID: 512
	// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x000A0C4C File Offset: 0x0009EE4C
	internal float MobilityProc
	{
		get
		{
			return Mathf.Max(1f, this.baseMobilityProc + this.modMobilityProcBonus + this.skillMobilityProcBonus + ((!Peer.HardcoreMode) ? 0f : CVars.g_hardcoreMobilityIncrease));
		}
	}

	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x000A0C94 File Offset: 0x0009EE94
	internal float ReloadSpeedProc
	{
		get
		{
			return Mathf.Max(1f, this.baseReloadSpeedProc + this.modReloadSpeedProcBonus + this.skillReloadSpeedProcBonus);
		}
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x000A0CB4 File Offset: 0x0009EEB4
	internal float PierceProc
	{
		get
		{
			return Mathf.Min(Mathf.Max(1f, this.basePierceProc + this.modPierceProcBonus + this.skillPierceProcBonus), 100f);
		}
	}

	// Token: 0x17000203 RID: 515
	// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000A0CEC File Offset: 0x0009EEEC
	internal float accuracy
	{
		get
		{
			return this.maxAccuracy + (this.minAccuracy - this.maxAccuracy) * this.AccuracyProc / 100f;
		}
	}

	// Token: 0x17000204 RID: 516
	// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000A0D1C File Offset: 0x0009EF1C
	internal float recoil
	{
		get
		{
			if (BIT.AND((int)this.player.playerInfo.buffs, 1048576) && this.IsPrimary)
			{
				return (this.RecoilProc + 50f) / 100f;
			}
			return this.RecoilProc / 100f;
		}
	}

	// Token: 0x17000205 RID: 517
	// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000A0D74 File Offset: 0x0009EF74
	internal float damage
	{
		get
		{
			if (Peer.HardcoreMode)
			{
				return this.DamageProc * CVars.g_hardcoreDamageCoef;
			}
			return this.DamageProc;
		}
	}

	// Token: 0x17000206 RID: 518
	// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000A0D94 File Offset: 0x0009EF94
	internal float firerate
	{
		get
		{
			return 40f + 1160f * this.FirerateProc / 100f;
		}
	}

	// Token: 0x17000207 RID: 519
	// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000A0DB0 File Offset: 0x0009EFB0
	internal float mobility
	{
		get
		{
			return 60f + -60f * this.MobilityProc / 100f;
		}
	}

	// Token: 0x17000208 RID: 520
	// (get) Token: 0x06000DAF RID: 3503 RVA: 0x000A0DCC File Offset: 0x0009EFCC
	internal float reloadSpeed
	{
		get
		{
			if (this.reloadTime <= 0f)
			{
				return (8f + -7f * this.ReloadSpeedProc / 100f) * this.player.Ammo.ReloadSpeedMult;
			}
			if (this.DoubleMagazine)
			{
				return (!this.FirstReload) ? ((8f + -7f * this.ReloadSpeedProc / 100f) * this.player.Ammo.ReloadSpeedMult) : this.reloadTime;
			}
			return (this.reloadTime + (1f - this.reloadTime) * this.ReloadSpeedProc / 100f) * this.player.Ammo.ReloadSpeedMult;
		}
	}

	// Token: 0x17000209 RID: 521
	// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000A0E90 File Offset: 0x0009F090
	internal float PierceReducer
	{
		get
		{
			return Mathf.Max(1f - this.PierceProc / 100f, 0.01f);
		}
	}

	// Token: 0x1700020A RID: 522
	// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000A0EB0 File Offset: 0x0009F0B0
	internal float BaseAccuracyProc
	{
		get
		{
			return this.baseAccuracyProc;
		}
	}

	// Token: 0x1700020B RID: 523
	// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000A0EB8 File Offset: 0x0009F0B8
	internal float BaseRecoilProc
	{
		get
		{
			return this.baseRecoilProc;
		}
	}

	// Token: 0x1700020C RID: 524
	// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000A0EC0 File Offset: 0x0009F0C0
	internal float BaseDamageProc
	{
		get
		{
			return this.baseDamageProc;
		}
	}

	// Token: 0x1700020D RID: 525
	// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x000A0EC8 File Offset: 0x0009F0C8
	internal float BaseFirerateProc
	{
		get
		{
			return this.baseFirerateProc;
		}
	}

	// Token: 0x1700020E RID: 526
	// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000A0ED0 File Offset: 0x0009F0D0
	internal float BaseMobilityProc
	{
		get
		{
			return this.baseMobilityProc;
		}
	}

	// Token: 0x1700020F RID: 527
	// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x000A0ED8 File Offset: 0x0009F0D8
	internal float BaseReloadSpeedProc
	{
		get
		{
			return this.baseReloadSpeedProc;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x000A0EE0 File Offset: 0x0009F0E0
	internal float BasePierceProc
	{
		get
		{
			return this.basePierceProc;
		}
	}

	// Token: 0x17000211 RID: 529
	// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x000A0EE8 File Offset: 0x0009F0E8
	internal float ModAccuracyProcBonus
	{
		get
		{
			return this.modAccuracyProcBonus;
		}
	}

	// Token: 0x17000212 RID: 530
	// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x000A0EF0 File Offset: 0x0009F0F0
	internal float ModRecoilProcBonus
	{
		get
		{
			return this.modRecoilProcBonus;
		}
	}

	// Token: 0x17000213 RID: 531
	// (get) Token: 0x06000DBA RID: 3514 RVA: 0x000A0EF8 File Offset: 0x0009F0F8
	internal float ModDamageProcBonus
	{
		get
		{
			return this.modDamageProcBonus;
		}
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x06000DBB RID: 3515 RVA: 0x000A0F00 File Offset: 0x0009F100
	internal float ModFirerateProcBonus
	{
		get
		{
			return this.modFirerateProcBonus;
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x06000DBC RID: 3516 RVA: 0x000A0F08 File Offset: 0x0009F108
	internal float ModMobilityProcBonus
	{
		get
		{
			return this.modMobilityProcBonus;
		}
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000A0F10 File Offset: 0x0009F110
	internal float ModReloadSpeedProcBonus
	{
		get
		{
			return this.modReloadSpeedProcBonus;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x06000DBE RID: 3518 RVA: 0x000A0F18 File Offset: 0x0009F118
	internal float ModPierceProcBonus
	{
		get
		{
			return this.modPierceProcBonus;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000A0F20 File Offset: 0x0009F120
	internal float SkillAccuracyProcBonus
	{
		get
		{
			return this.skillAccuracyProcBonus;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x000A0F28 File Offset: 0x0009F128
	internal float SkillRecoilProcBonus
	{
		get
		{
			return this.skillRecoilProcBonus;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000A0F30 File Offset: 0x0009F130
	internal float SkillDamageProcBonus
	{
		get
		{
			return this.skillDamageProcBonus;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000A0F38 File Offset: 0x0009F138
	internal float SkillFirerateProcBonus
	{
		get
		{
			return this.skillFirerateProcBonus;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x000A0F40 File Offset: 0x0009F140
	internal float SkillMobilityProcBonus
	{
		get
		{
			return this.skillMobilityProcBonus;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000A0F48 File Offset: 0x0009F148
	internal float SkillReloadSpeedProcBonus
	{
		get
		{
			return this.skillReloadSpeedProcBonus;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x000A0F50 File Offset: 0x0009F150
	internal float SkillPierceProcBonus
	{
		get
		{
			return this.skillPierceProcBonus;
		}
	}

	// Token: 0x1700021F RID: 543
	// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000A0F58 File Offset: 0x0009F158
	internal string StringAmmo
	{
		get
		{
			if (Utility.IsModableWeapon((int)this.type) && WeaponModsStorage.Instance().GetModsByWeaponId((int)this.type).AmmoTypeAvailable)
			{
				MasteringMod mod = this.GetMod(ModType.ammo);
				if (mod != null)
				{
					return mod.ShortName;
				}
			}
			if (this.IsMod && (this.hasSlug || this.modHasSlug))
			{
				return this.WtaskAmmo;
			}
			return this.ammoString;
		}
	}

	// Token: 0x17000220 RID: 544
	// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x000A0FD4 File Offset: 0x0009F1D4
	internal bool IsModable
	{
		get
		{
			return this.isModable;
		}
	}

	// Token: 0x17000221 RID: 545
	// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x000A0FDC File Offset: 0x0009F1DC
	internal float AimFov
	{
		get
		{
			if (!this.isModable)
			{
				return this.aimFov;
			}
			Dictionary<ModType, int> dictionary = Utility.StringToMods(this.state.Mods);
			int id;
			dictionary.TryGetValue(ModType.optic, out id);
			MasteringMod modById = ModsStorage.Instance().GetModById(id);
			int num = modById.WeaponSpecificInfo[(int)this.type].AimFov;
			return (float)((num == 0) ? 30 : num);
		}
	}

	// Token: 0x17000222 RID: 546
	// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x000A1048 File Offset: 0x0009F248
	// (set) Token: 0x06000DCA RID: 3530 RVA: 0x000A1050 File Offset: 0x0009F250
	internal float UiModAccuracy { get; set; }

	// Token: 0x17000223 RID: 547
	// (get) Token: 0x06000DCB RID: 3531 RVA: 0x000A105C File Offset: 0x0009F25C
	// (set) Token: 0x06000DCC RID: 3532 RVA: 0x000A1064 File Offset: 0x0009F264
	internal float UiModDamage { get; set; }

	// Token: 0x17000224 RID: 548
	// (get) Token: 0x06000DCD RID: 3533 RVA: 0x000A1070 File Offset: 0x0009F270
	// (set) Token: 0x06000DCE RID: 3534 RVA: 0x000A1078 File Offset: 0x0009F278
	internal float UiModMobility { get; set; }

	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000DCF RID: 3535 RVA: 0x000A1084 File Offset: 0x0009F284
	// (set) Token: 0x06000DD0 RID: 3536 RVA: 0x000A108C File Offset: 0x0009F28C
	internal float UiModPenetration { get; set; }

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x000A1098 File Offset: 0x0009F298
	// (set) Token: 0x06000DD2 RID: 3538 RVA: 0x000A10A0 File Offset: 0x0009F2A0
	internal float UiModRecoil { get; set; }

	// Token: 0x06000DD3 RID: 3539 RVA: 0x000A10AC File Offset: 0x0009F2AC
	internal void LoadTable(Dictionary<string, object> dict)
	{
		JSON.ReadWriteEnum<Weapons>(dict, "type", ref this.type, false);
		JSON.ReadWrite(dict, "InterfaceName", ref this.InterfaceName, false);
		JSON.ReadWrite(dict, "ModInterfaceName", ref this.ModInterfaceName, false);
		JSON.ReadWriteEnum<WeaponUseType>(dict, "weaponUseType", ref this.weaponUseType, false);
		JSON.ReadWriteEnum<WeaponNature>(dict, "weaponNature", ref this.weaponNature, false);
		JSON.ReadWrite(dict, "Name", ref this.Name, false);
		JSON.ReadWrite(dict, "ShortName", ref this.ShortName, false);
		JSON.ReadWrite(dict, "Info", ref this.Info, false);
		JSON.ReadWrite(dict, "price", ref this.price, false);
		JSON.ReadWrite(dict, "IndestructiblePrice", ref this.IndestructiblePrice, false);
		JSON.ReadWrite(dict, "UnitRepairCost", ref this.UnitRepairCost, false);
		JSON.ReadWrite(dict, "GridPosition", ref this.GridPosition, false);
		JSON.ReadWrite(dict, "isVisible", ref this.isVisible, false);
		JSON.ReadWrite(dict, "isBolt", ref this.isBolt, false);
		JSON.ReadWrite(dict, "isBelt", ref this.isBelt, false);
		JSON.ReadWrite(dict, "DoubleMagazine", ref this.DoubleMagazine, false);
		JSON.ReadWrite(dict, "UseShotGunReload", ref this.UseShotGunReload, false);
		JSON.ReadWrite(dict, "canDuplet", ref this.canDuplet, false);
		JSON.ReadWrite(dict, "ammoString", ref this.ammoString, false);
		JSON.ReadWrite(dict, "durability", ref this.durability, false);
		JSON.ReadWrite(dict, "wtaskPrice", ref this.wtaskPrice, false);
		JSON.ReadWrite(dict, "forceEnableWtask", ref this.forceEnableWtask, false);
		JSON.ReadWrite(dict, "isPremium", ref this.isPremium, false);
		JSON.ReadWrite(dict, "isDonate", ref this.isDonate, false);
		JSON.ReadWrite(dict, "rentPrice", ref this.rentPrice, false);
		JSON.ReadWrite(dict, "permanentPrice", ref this.permanentPrice, false);
		JSON.ReadWrite(dict, "rentTime", ref this.rentTime, false);
		if (CVars.realm != "kg")
		{
			JSON.ReadWrite(dict, "isSocial", ref this.isSocial, false);
		}
		JSON.ReadWrite(dict, "friendsRequired", ref this.friendsRequired, false);
		JSON.ReadWrite(dict, "WtaskGearID", ref this.WtaskGearID, false);
		this.wtask = new Wtask();
		this.wtask.Read((Dictionary<string, object>)dict["Wtask"]);
		JSON.ReadWrite(dict, "WtaskName", ref this.WtaskName, false);
		JSON.ReadWrite(dict, "WtaskDescription", ref this.WtaskDescription, false);
		JSON.ReadWrite(dict, "WtaskGearName", ref this.WtaskGearName, false);
		JSON.ReadWrite(dict, "WtaskGearDesc", ref this.WtaskGearDesc, false);
		JSON.ReadWrite(dict, "WtaskInfo", ref this.WtaskInfo, false);
		JSON.ReadWrite(dict, "WtaskAmmo", ref this.WtaskAmmo, false);
		JSON.ReadWriteEnum<WeaponSpecific>(dict, "weaponType", ref this.weaponSpecific, false);
		JSON.ReadWrite(dict, "shotgunBullets", ref this.caseshot, false);
		JSON.ReadWrite(dict, "pompovik", ref this.pompovik, false);
		JSON.ReadWrite(dict, "autoMode", ref this.auto, false);
		JSON.ReadWrite(dict, "block", ref this.armoryBlock, false);
		JSON.ReadWrite(dict, "hasOptic", ref this.hasOptic, false);
		JSON.ReadWrite(dict, "hasLTP", ref this.hasLTP, false);
		JSON.ReadWrite(dict, "hasKolimator", ref this.hasKolimator, false);
		JSON.ReadWrite(dict, "hasGrip", ref this.hasGrip, false);
		JSON.ReadWrite(dict, "hasSS", ref this.hasSS, false);
		JSON.ReadWrite(dict, "hasCompensator", ref this.hasCompensator, false);
		JSON.ReadWrite(dict, "hasSlug", ref this.hasSlug, false);
		JSON.ReadWrite(dict, "hasFlashlight", ref this.hasFlashlight, false);
		JSON.ReadWrite(dict, "modHasOptic", ref this.modHasOptic, false);
		JSON.ReadWrite(dict, "modHasLTP", ref this.modHasLTP, false);
		JSON.ReadWrite(dict, "modHasKolimator", ref this.modHasKolimator, false);
		JSON.ReadWrite(dict, "modHasGrip", ref this.modHasGrip, false);
		JSON.ReadWrite(dict, "modHasSS", ref this.modHasSS, false);
		JSON.ReadWrite(dict, "modHasCompensator", ref this.modHasCompensator, false);
		JSON.ReadWrite(dict, "modHasSlug", ref this.modHasSlug, false);
		JSON.ReadWrite(dict, "modHasFlashlight", ref this.modHasFlashlight, false);
		JSON.ReadWrite(dict, "scatter", ref this.scatter, false);
		JSON.ReadWrite(dict, "accuracyProc", ref this.baseAccuracyProc, false);
		JSON.ReadWrite(dict, "recoilProc", ref this.baseRecoilProc, false);
		JSON.ReadWrite(dict, "damageProc", ref this.baseDamageProc, false);
		JSON.ReadWrite(dict, "firerateProc", ref this.baseFirerateProc, false);
		JSON.ReadWrite(dict, "magSize", ref this.magSize, false);
		JSON.ReadWrite(dict, "mobilityProc", ref this.baseMobilityProc, false);
		JSON.ReadWrite(dict, "reloadSpeedProc", ref this.baseReloadSpeedProc, false);
		JSON.ReadWrite(dict, "bagSize", ref this.bagSize, false);
		JSON.ReadWrite(dict, "accuracyFader", ref this.accuracyFader, false);
		JSON.ReadWrite(dict, "pierceProc", ref this.basePierceProc, false);
		JSON.ReadWrite(dict, "IdleAimMult", ref this.IdleAimMult, false);
		JSON.ReadWrite(dict, "AimIdleMult", ref this.AimIdleMult, false);
		JSON.ReadWrite(dict, "OutIdleMult", ref this.OutIdleMult, false);
		JSON.ReadWrite(dict, "IdleOutMult", ref this.IdleOutMult, false);
		JSON.ReadWrite(dict, "hearRadius", ref this.hearRadius, false);
		JSON.ReadWrite(dict, "focalDistance", ref this.focalDistance, false);
		JSON.ReadWrite(dict, "aimFov", ref this.aimFov, false);
		JSON.ReadWrite(dict, "worldAimFov", ref this.worldAimFov, false);
		JSON.ReadWrite(dict, "sensMult", ref this._sensMult, false);
		JSON.ReadWrite(dict, "damageReduceDistanceMin", ref this.damageReduceDistanceMin, false);
		JSON.ReadWrite(dict, "damageReduceDistanceMax", ref this.damageReduceDistanceMax, false);
		JSON.ReadWrite(dict, "distanceReducerE2", ref this.distanceReducerE2, false);
		JSON.ReadWrite(dict, "hideAmmoCount", ref this.hideAmmoCount, false);
		JSON.ReadWrite(dict, "enableBeltTime", ref this.enableBeltTime, false);
		JSON.ReadWrite(dict, "reloadTime", ref this.reloadTime, false);
		JSON.ReadWrite(dict, "isModdable", ref this.isModable, false);
		if (dict.ContainsKey("skillReq"))
		{
			JSON.ReadWrite(dict, "skillReq", ref this.skillReq, false);
			this.skillReq--;
		}
		this._tempDamageReduceDistanceMin = this.damageReduceDistanceMin;
		this._tempDamageReduceDistanceMax = this.damageReduceDistanceMax;
		this.skillAccuracyProcBonus = 0f;
		this.skillRecoilProcBonus = 0f;
		this.skillDamageProcBonus = 0f;
		this.skillFirerateProcBonus = 0f;
		this.skillMobilityProcBonus = 0f;
		this.skillReloadSpeedProcBonus = 0f;
		this.skillPierceProcBonus = 0f;
		this.damageReduceDistanceMinSkillBonus = 0f;
		this.damageReduceDistanceMaxSkillBonus = 0f;
		this.repair_coef = 1f;
		this.recoilSeatMult = 1f;
		this.aimRecoilMult = 1f;
		this.seatAccuracyMult = 1f;
		this.walkAccuracyMult = 1f;
		this.AIMA = 0.2f;
		this.OutIdleMult = 1f;
		this.ApplayWtaskModsEffect();
		if (this.player == null)
		{
			if (MasteringSuitsInfo.Instance.Suits != null && MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods.ContainsKey((int)this.type))
			{
				Dictionary<ModType, int> mods = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods[(int)this.type].Mods;
				this.ApplyModsEffect(mods);
			}
			for (int i = 0; i < Main.UserInfo.skillsInfos.Length; i++)
			{
				if (Main.UserInfo.skillUnlocked((Skills)i))
				{
					this.ParseSkill((Skills)i);
				}
			}
			if (Main.UserInfo.clanID != 0)
			{
				for (int j = 0; j < Main.UserInfo.ClanSkillsInfos.Length; j++)
				{
					if (Main.UserInfo.clanSkillUnlocked((Cl_Skills)j))
					{
						this.ParseClanSkill((Cl_Skills)j, Main.UserInfo.playerClass);
					}
				}
			}
		}
		else
		{
			if (Utility.IsModableWeapon(this.type))
			{
				Dictionary<ModType, int> dictionary = new Dictionary<ModType, int>();
				string[] array = (!this.IsPrimary) ? this.player.Ammo.state.secondaryState.Mods.Split(new char[]
				{
					' '
				}) : this.player.Ammo.state.primaryState.Mods.Split(new char[]
				{
					' '
				});
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(text))
					{
						MasteringMod modById = ModsStorage.Instance().GetModById(int.Parse(text));
						dictionary.Add(modById.Type, modById.Id);
					}
				}
				this.ApplyModsEffect(dictionary);
			}
			for (int l = 0; l < this.player.playerInfo.skillsInfos.Length; l++)
			{
				if (this.player.playerInfo.skillUnlocked((Skills)l))
				{
					this.ParseSkill((Skills)l);
				}
			}
			if (this.player.playerInfo.clanTag != string.Empty)
			{
				for (int m = 0; m < this.player.playerInfo.clanSkillsInfos.Length; m++)
				{
					if (this.player.playerInfo.clanSkillUnlocked((Cl_Skills)m))
					{
						this.ParseClanSkill((Cl_Skills)m, this.player.playerInfo.playerClass);
					}
				}
			}
		}
		this.SetUiModsEffects();
	}

	// Token: 0x06000DD4 RID: 3540 RVA: 0x000A1A90 File Offset: 0x0009FC90
	private void ParseSkill(Skills skill)
	{
		if (this.skillReq != -1 && this.skillReq == (int)skill)
		{
			this.unlockBySkill = true;
		}
		if (skill == Skills.rec2 && this.IsPrimary)
		{
			this.skillRecoilProcBonus -= 3f;
		}
		if (skill == Skills.rel_mg1)
		{
			if (this.IsPrimary)
			{
				this.repair_coef *= 0.7f;
			}
			if (this.IsSecondary)
			{
				this.repair_coef *= 0.7f;
			}
		}
		if (skill == Skills.rel_heavy && this.IsPrimary && this.baseMobilityProc + this.modMobilityProcBonus < 46f)
		{
			this.skillReloadSpeedProcBonus += 8f;
		}
		if (skill == Skills.ammo && this.IsPrimary)
		{
			if (this.weaponNature == WeaponNature.shotgun)
			{
				this.bagSize += 30;
			}
			else
			{
				this.bagSize += this.magSize * 2;
			}
			this.state.bagSize = this.bagSize;
		}
		if (skill == Skills.sit_rec && this.IsPrimary)
		{
			this.recoilSeatMult *= 0.6f;
		}
		if (skill == Skills.rec3 && this.IsPrimary)
		{
			this.skillRecoilProcBonus -= 8f;
		}
		if (skill == Skills.operator_mg && this.IsPrimary && this.MobilityProc < 46f)
		{
			this.skillMobilityProcBonus += 8f;
		}
		if (skill == Skills.stab && this.IsPrimary)
		{
			this.stab = true;
		}
		if (skill == Skills.accr1 && this.IsPrimary)
		{
			this.skillAccuracyProcBonus += 3f;
		}
		if (skill == Skills.aim_rec && this.IsPrimary)
		{
			this.aimRecoilMult = 0.8f;
		}
		if (skill == Skills.sit_accr && this.IsPrimary)
		{
			this.seatAccuracyMult = 0.6666667f;
		}
		if (skill == Skills.walk_accr && this.IsPrimary)
		{
			this.walkAccuracyMult = 0.6666667f;
		}
		if (skill == Skills.marks)
		{
		}
		if (skill == Skills.accr2 && this.IsPrimary)
		{
			this.skillAccuracyProcBonus += 5f;
		}
		if (skill == Skills.conc && this.IsPrimary)
		{
			this.skillAccuracyProcBonus += 3f;
		}
		if (skill == Skills.operator_sni && this.IsPrimary)
		{
			this.AIMA *= 0.6f;
		}
		if (skill == Skills.pro_sn && this.IsPrimary)
		{
			this.first100Accuracy = true;
		}
		if (skill == Skills.accr3 && this.IsPrimary)
		{
			this.skillAccuracyProcBonus += 9f;
		}
		if (skill == Skills.colim_zoom && this.Kolimator)
		{
			this.worldAimFov += 5f;
		}
		if (skill == Skills.mob_mods && (this.Optic || this.LTP || this.Kolimator || this.SS || this.Hybrid || this.FlashHider))
		{
			this.skillMobilityProcBonus += 1f;
		}
		if (skill == Skills.colim_accr && this.Kolimator)
		{
			this.skillAccuracyProcBonus += 3f;
		}
		if (skill == Skills.rec_mods && (this.Optic || this.LTP || this.Kolimator || this.SS || this.Hybrid || this.FlashHider))
		{
			this.skillRecoilProcBonus -= 2f;
		}
		if (skill == Skills.optics_accr && this.Optic)
		{
			this.skillAccuracyProcBonus += 3f;
		}
		if (skill == Skills.mob_sil && this.SS)
		{
			this.skillMobilityProcBonus += 2f;
		}
		if (skill == Skills.accr_sil && this.SS)
		{
			this.skillAccuracyProcBonus += 3f;
		}
		if (skill == Skills.nodam_sil)
		{
			if (Utility.IsModableWeapon((int)this.type) && this.SS)
			{
				MasteringMod mod = this.GetMod(ModType.silencer);
				this.skillDamageProcBonus += (this.baseDamageProc - this.baseDamageProc * mod.Damage.Val) * 0.75f;
			}
			else if (this.modHasSS && this.IsMod && !this.hasSS)
			{
				this.skillDamageProcBonus += this.baseDamageProc * 0.15f;
			}
		}
		if (skill == Skills.apammo && this.IsPrimary)
		{
			this.skillPierceProcBonus += 20f;
		}
		if (skill == Skills.accr_p && this.IsSecondary)
		{
			this.skillAccuracyProcBonus += 5f;
		}
		if (skill == Skills.mob1 && this.IsPrimary)
		{
			this.skillMobilityProcBonus += 3f;
		}
		if (skill == Skills.rec_p && this.IsSecondary)
		{
			this.skillRecoilProcBonus -= 10f;
		}
		if (skill == Skills.rel1 && this.IsPrimary)
		{
			this.skillReloadSpeedProcBonus += 5f;
		}
		if (skill == Skills.ammo_p && this.IsSecondary)
		{
			this.bagSize += this.magSize * 2;
			this.state.bagSize = this.bagSize;
		}
		if (skill == Skills.rel_p && this.IsSecondary)
		{
			this.skillReloadSpeedProcBonus += 10f;
		}
		if (skill == Skills.operator_pp && (this.IsPrimary || this.IsSecondary))
		{
			this.OutIdleMult *= 0.6f;
			this.IdleOutMult *= 0.6f;
		}
		if (skill == Skills.mob2 && this.IsPrimary)
		{
			this.skillMobilityProcBonus += 6f;
		}
		if (skill == Skills.rec2_p && this.IsSecondary)
		{
			this.skillRecoilProcBonus -= 15f;
		}
		if (skill == Skills.scan)
		{
		}
		if (skill == Skills.longb1 && this.IsPrimary)
		{
			this.damageReduceDistanceMinSkillBonus += 10f;
			this.damageReduceDistanceMaxSkillBonus += 10f;
		}
		if (skill == Skills.firerate && this.IsPrimary && !this.pompovik && !this.isBolt)
		{
			this.skillFirerateProcBonus += 5f;
		}
		if (skill == Skills.dam_p && this.IsSecondary)
		{
			this.skillDamageProcBonus += 10f;
		}
		if (skill == Skills.mob3 && this.IsPrimary)
		{
			this.skillMobilityProcBonus += 8f;
		}
		if (skill == Skills.rel2 && this.IsPrimary)
		{
			this.skillReloadSpeedProcBonus += 5f;
		}
		if (skill == Skills.operator_storm && !this.IsMod)
		{
			this.worldAimFov += 5f;
		}
		if (skill == Skills.longb2 && this.IsPrimary)
		{
			this.damageReduceDistanceMaxSkillBonus += 15f;
			this.damageReduceDistanceMinSkillBonus += 15f;
		}
		if (skill == Skills.rec1 && this.IsPrimary)
		{
			this.skillRecoilProcBonus -= 3f;
		}
		if (skill == Skills.penetr)
		{
			if (this.IsPrimary)
			{
				this.skillPierceProcBonus += 8f;
			}
			if (this.IsSecondary)
			{
				this.skillPierceProcBonus += 5f;
			}
		}
		if (skill == Skills.rel3 && this.IsPrimary)
		{
			this.skillReloadSpeedProcBonus += 7f;
		}
		if (skill == Skills.dam1 && this.IsPrimary)
		{
			this.skillDamageProcBonus += 4f;
		}
		if (skill == Skills.dam2 && this.IsPrimary)
		{
			this.skillDamageProcBonus += 6f;
		}
		if (skill == Skills.dam3 && this.IsPrimary)
		{
			this.skillDamageProcBonus += 10f;
		}
	}

	// Token: 0x06000DD5 RID: 3541 RVA: 0x000A2370 File Offset: 0x000A0570
	private void ParseClanSkill(Cl_Skills clanSkill, PlayerClass playerClass)
	{
		if (clanSkill == Cl_Skills.cl_dam)
		{
			this.skillDamageProcBonus += 4f;
		}
		if (clanSkill == Cl_Skills.cl_train3)
		{
			this.skillMobilityProcBonus += 4f;
		}
		if (clanSkill == Cl_Skills.cl_sni1 && playerClass == PlayerClass.sniper)
		{
			this.skillMobilityProcBonus += 7f;
		}
		if (clanSkill == Cl_Skills.cl_storm1 && playerClass == PlayerClass.storm_trooper)
		{
			this.skillPierceProcBonus += 7f;
			if (this.IsPrimary)
			{
				if (this.weaponNature == WeaponNature.shotgun)
				{
					this.bagSize += 16;
				}
				else
				{
					this.bagSize += this.magSize;
				}
				this.state.bagSize = this.bagSize;
			}
		}
		if (clanSkill == Cl_Skills.cl_weap1 && playerClass == PlayerClass.gunsmith)
		{
			this.skillDamageProcBonus += 10f;
		}
		if (clanSkill == Cl_Skills.cl_scout1 && playerClass == PlayerClass.scout)
		{
			this.skillAccuracyProcBonus += 10f;
		}
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x000A2488 File Offset: 0x000A0688
	internal void ApplyModsEffect(Dictionary<ModType, int> mods)
	{
		this.modAccuracyProcBonus = 0f;
		this.modRecoilProcBonus = 0f;
		this.modDamageProcBonus = 0f;
		this.modPierceProcBonus = 0f;
		this.modMobilityProcBonus = 0f;
		this.ShotGroupingProc = 1f;
		float num = 0f;
		float num2 = 0f;
		this.damageReduceDistanceMin = this._tempDamageReduceDistanceMin;
		this.damageReduceDistanceMax = this._tempDamageReduceDistanceMax;
		foreach (KeyValuePair<ModType, int> keyValuePair in mods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (modById.Accuracy.Multiplication)
			{
				this.modAccuracyProcBonus += this.baseAccuracyProc * modById.Accuracy.Val - this.baseAccuracyProc;
			}
			else
			{
				this.modAccuracyProcBonus += modById.Accuracy.Val;
			}
			if (modById.Recoil.Multiplication)
			{
				this.modRecoilProcBonus += this.baseRecoilProc * modById.Recoil.Val - this.baseRecoilProc;
			}
			else
			{
				this.modRecoilProcBonus += modById.Recoil.Val;
			}
			if (modById.Damage.Multiplication)
			{
				this.modDamageProcBonus += this.baseDamageProc * modById.Damage.Val - this.baseDamageProc;
			}
			else
			{
				this.modDamageProcBonus += modById.Damage.Val;
			}
			if (modById.Penetration.Multiplication)
			{
				this.modPierceProcBonus += this.basePierceProc * modById.Penetration.Val - this.basePierceProc;
			}
			else
			{
				this.modPierceProcBonus += modById.Penetration.Val;
			}
			if (modById.Mobility.Multiplication)
			{
				this.modMobilityProcBonus += this.baseMobilityProc * modById.Mobility.Val - this.baseMobilityProc;
			}
			else
			{
				this.modMobilityProcBonus += modById.Mobility.Val;
			}
			if (modById.EffectiveDistance.Multiplication)
			{
				num += modById.EffectiveDistance.PercentVal;
			}
			if (modById.HearDistance.Multiplication)
			{
				num2 -= modById.HearDistance.PercentVal;
			}
			if (modById.ShotGrouping.Val > 0f)
			{
				this.ShotGroupingProc += modById.ShotGrouping.Val - 1f;
			}
		}
		this.damageReduceDistanceMin *= 1f + num / 100f;
		this.damageReduceDistanceMax *= 1f + num / 100f;
		this.hearRadius *= 1f + num2 / 100f;
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x000A27CC File Offset: 0x000A09CC
	internal void ApplayWtaskModsEffect()
	{
		if (Utility.IsModableWeapon(this.type))
		{
			return;
		}
		this.modAccuracyProcBonus = 0f;
		this.modRecoilProcBonus = 0f;
		this.modDamageProcBonus = 0f;
		this.modPierceProcBonus = 0f;
		this.modMobilityProcBonus = 0f;
		if (!this.IsMod)
		{
			return;
		}
		if (this.modHasOptic)
		{
			this.modAccuracyProcBonus += 7f;
			this.modMobilityProcBonus += -6f;
			this.modRecoilProcBonus += -4f;
		}
		if (this.modHasLTP)
		{
			this.modAccuracyProcBonus += 4f;
			this.modMobilityProcBonus += -2f;
			this.modRecoilProcBonus += -3f;
		}
		if (this.modHasKolimator)
		{
			this.modAccuracyProcBonus += 5f;
			this.modMobilityProcBonus += -4f;
			this.modRecoilProcBonus += -3f;
		}
		if (this.modHasSS && !this.hasSS)
		{
			this.modDamageProcBonus += -(this.baseDamageProc * 0.15f);
			this.modMobilityProcBonus += -3f;
			this.modRecoilProcBonus += -2f;
		}
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x000A2944 File Offset: 0x000A0B44
	internal void SetUiModsEffects()
	{
		if (Utility.IsModableWeapon(this.type))
		{
			return;
		}
		this.UiModAccuracy = 0f;
		this.UiModDamage = 0f;
		this.UiModMobility = 0f;
		this.UiModPenetration = 0f;
		this.UiModRecoil = 0f;
		if (this.modHasOptic)
		{
			this.UiModAccuracy += 7f;
			this.UiModMobility += -6f;
			this.UiModRecoil += -4f;
		}
		if (this.modHasLTP)
		{
			this.UiModAccuracy += 4f;
			this.UiModMobility += -2f;
			this.UiModRecoil += -3f;
		}
		if (this.modHasKolimator)
		{
			this.UiModAccuracy += 5f;
			this.UiModMobility += -4f;
			this.UiModRecoil += -3f;
		}
		if (this.modHasSS && !this.hasSS)
		{
			this.UiModDamage += -(this.baseDamageProc * 0.15f);
			this.UiModMobility += -3f;
			this.UiModRecoil += -2f;
		}
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x000A2AB0 File Offset: 0x000A0CB0
	internal void Copy(BaseWeapon copy)
	{
		this.LOD = copy.LOD;
		this.weaponPrefab = copy.weaponPrefab;
		this.markTexture = copy.markTexture;
		this.FireAnimations = copy.FireAnimations;
		this.FireAimAnimations = copy.FireAimAnimations;
		this.shellName = copy.shellName;
		this.muzzleFlashName = copy.muzzleFlashName;
		this.silencedMuzzleFlashName = copy.silencedMuzzleFlashName;
		this.boltReloadSounds = copy.boltReloadSounds;
		this.reloadSounds = copy.reloadSounds;
		this.reloadTimes = copy.reloadTimes;
		this.reloadBools = copy.reloadBools;
		this.fireSounds = copy.fireSounds;
		this.secondSoundScheme = copy.secondSoundScheme;
		this.singleTime = copy.singleTime;
		this.loopFire = copy.loopFire;
		this.tailFire = copy.tailFire;
		this.modFireSounds = copy.modFireSounds;
		this.modSecondSoundScheme = copy.modSecondSoundScheme;
		this.modSingleTime = copy.modSingleTime;
		this.modLoopFire = copy.modLoopFire;
		this.modTailFire = copy.modTailFire;
		this.equipTime = copy.equipTime;
		this.equip = copy.equip;
		this.correct = copy.correct;
		this.rootPositionCorrect = copy.rootPositionCorrect;
		this.rootRotationCorrect = copy.rootRotationCorrect;
		this.correctAlways = copy.correctAlways;
		this.isModable = copy.isModable;
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x000A2C1C File Offset: 0x000A0E1C
	internal virtual void Init(BaseNetPlayer player, int index)
	{
		this.player = player;
		this.G.Init(this);
		try
		{
			this.LoadTable(Globals.I.weapons[index]);
		}
		catch (Exception e)
		{
			global::Console.print("BaseWeapon error, index=" + index, Color.red);
			global::Console.exception(e);
			if (this is ClientWeapon)
			{
				this.LoadTable(Globals.I.weapons[0]);
			}
		}
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x000A2CB4 File Offset: 0x000A0EB4
	internal virtual void AfterInit(float repair_info)
	{
		this.state.clips = this.magSize;
		this.state.bagSize = this.bagSize;
		this.state.singleShot = !this.auto;
		this.accuracyFade.Start();
		this.state.repair_info = repair_info;
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x000A2D10 File Offset: 0x000A0F10
	internal virtual void Tick(float dt)
	{
		if (this.accuracyHitFade > 1f)
		{
			this.accuracyHitFade -= this.accuracyFader * dt / 2f;
		}
		else
		{
			this.accuracyHitFade = 1f;
		}
		if (this.accuracyFade.Elapsed > 0.15f)
		{
			this.currentAccuracy -= this.accuracyFader * dt;
			if (this.currentAccuracy < this.minAccuracy)
			{
				this.currentAccuracy = this.minAccuracy;
			}
			this.stab_now.Stop();
		}
		this.G.Tick(dt);
		this.state.G.Clone(this.G.state);
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x000A2DD4 File Offset: 0x000A0FD4
	internal virtual void CallLateUpdate()
	{
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x000A2DD8 File Offset: 0x000A0FD8
	internal virtual void Recover()
	{
		this.G.state.Clone(this.state.G);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x000A2DF8 File Offset: 0x000A0FF8
	internal FireType Fire()
	{
		if (this.AlwaysGroup)
		{
			return FireType.nofire;
		}
		if (this.Damaged)
		{
			return FireType.nofire;
		}
		this.G.CancelInvoke("PostFire");
		float num;
		if (this.isBolt && !this.state.needReload && this.player.Ammo.IsAim)
		{
			num = 0.1f;
		}
		else if (this.UseShotGunReload)
		{
			num = 2f;
		}
		else if (this.isBolt && this.isModable && !this.player.Ammo.IsAim)
		{
			num = 1.6f;
		}
		else
		{
			num = 1f;
		}
		this.G.Invoke("PostFire", this.fireDelay * num, false);
		this.BrokenGunFire();
		this.OnFire();
		if (this.isBolt && this.state.needReload)
		{
			return this.BoltReload();
		}
		this.state.clips--;
		if (this.duplet && this.state.clips > 0)
		{
			this.state.clips--;
		}
		if (this.IsAbakan && this.state.clips > 0 && (this.AbakanFirstShot || this.state.singleShot))
		{
			this.state.clips--;
		}
		if (this.first100Accuracy && this.first100Accuracy_now == First100Accuracy.none)
		{
			this.first100Accuracy_now = First100Accuracy.ready;
		}
		return FireType.fire;
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x000A2FB0 File Offset: 0x000A11B0
	private FireType BoltReload()
	{
		if (this.player.Ammo.IsAim)
		{
			return FireType.reload;
		}
		this.state.needReload = !this.state.needReload;
		return FireType.reload;
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x000A2FF0 File Offset: 0x000A11F0
	private void BrokenGunFire()
	{
		if (!this.isUndestructable)
		{
			this.state.repair_info += this.repair_coef / (float)CVars.g_shotsToDamageWeapon * CVars.g_DamageWeaponReducer;
			if (this.state.repair_info >= (float)this.durability)
			{
				this.state.repair_info = (float)this.durability;
				this.OnBrakeFire();
			}
		}
		if (this.weaponNature != WeaponNature.shotgun)
		{
			return;
		}
		if (this.state.clips < 2 || !this.duplet || this.isUndestructable)
		{
			return;
		}
		this.state.repair_info += 1f / (float)CVars.g_shotsToDamageWeapon;
		if (this.state.repair_info <= (float)this.durability)
		{
			return;
		}
		this.state.repair_info = (float)this.durability;
		this.OnBrakeFire();
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x000A30E4 File Offset: 0x000A12E4
	[Obfuscation(Exclude = true)]
	protected void PostFire()
	{
		if (this.IsAbakan && (this.AbakanFirstShot || this.state.singleShot) && this.state.clips > 1)
		{
			this.AdditionalMuzzleFlash();
		}
		if (this.isBolt && this.player.Ammo.IsAim)
		{
			this.state.needReload = !this.state.needReload;
		}
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x000A3168 File Offset: 0x000A1368
	internal bool Reload()
	{
		if (this.AlwaysGroup)
		{
			return false;
		}
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		if (this.Damaged)
		{
			return false;
		}
		this.OnReload();
		this.G.Invoke("PostReload", this.ReloadTime, false);
		this.state.needReload = false;
		return true;
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x000A31E4 File Offset: 0x000A13E4
	internal bool ServerReload()
	{
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		if (this.Damaged)
		{
			return false;
		}
		this.OnReload();
		this.G.Invoke("PostReload", this.ReloadTime, false);
		this.state.needReload = false;
		return true;
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x000A3254 File Offset: 0x000A1454
	[Obfuscation(Exclude = true)]
	protected void PostReload()
	{
		if (this.DoubleMagazine)
		{
			this.FirstReload = !this.FirstReload;
		}
		if (this.magSize - this.state.clips >= this.state.bagSize)
		{
			this.state.clips += this.state.bagSize;
			this.state.bagSize = 0;
		}
		else
		{
			if (Peer.HardcoreMode)
			{
				this.state.bagSize = this.state.bagSize - this.magSize;
			}
			else
			{
				this.state.bagSize = this.state.bagSize - (this.magSize - this.state.clips);
			}
			if (this.state.bagSize < 0)
			{
				this.state.bagSize = 0;
			}
			if (this.state.bagSize < this.magSize)
			{
				this.state.clips = this.state.bagSize;
			}
			this.state.clips = this.magSize;
		}
		if (this.first100Accuracy && this.first100Accuracy_now == First100Accuracy.need_reload)
		{
			this.first100Accuracy_now = First100Accuracy.none;
		}
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x000A3398 File Offset: 0x000A1598
	internal void CancelPostReload()
	{
		this.G.CancelInvoke("PostReload");
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x000A33AC File Offset: 0x000A15AC
	internal bool PompovikReload()
	{
		if (this.AlwaysGroup)
		{
			return false;
		}
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		if (this.Damaged)
		{
			return false;
		}
		this.OnPompovikReloadStart();
		return true;
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x000A3404 File Offset: 0x000A1604
	internal bool ReloadStart()
	{
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		this.state.bagSize--;
		this.state.clips++;
		this.OnReloadStart();
		return true;
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x000A3468 File Offset: 0x000A1668
	internal bool CanInsertClip(float time)
	{
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		this.OnCanInsertClip(time);
		this.state.needReload = false;
		return true;
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x000A34B4 File Offset: 0x000A16B4
	internal bool InsertClip()
	{
		if (this.state.clips == this.magSize)
		{
			return false;
		}
		if (this.state.bagSize == 0)
		{
			return false;
		}
		this.state.bagSize--;
		this.state.clips++;
		this.OnInsertClipSound();
		return true;
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x000A3518 File Offset: 0x000A1718
	internal bool ReloadEnd()
	{
		this.OnReloadEnd();
		return true;
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x000A3524 File Offset: 0x000A1724
	internal void AddAccuracy()
	{
		if (this.first100Accuracy && this.first100Accuracy_now == First100Accuracy.ready)
		{
			this.first100Accuracy_now = First100Accuracy.need_reload;
		}
		if (this.stab_now.Elapsed > 0f)
		{
			this.currentAccuracy -= this.recoilAdder(this.player.Ammo.IsAim, this.player.Controller.isSeat, this.player.Controller.isWalk) * this.player.Ammo.AccuracyMult / 6f;
			float num = -this.minAccuracy * (float)((!this.player.Ammo.IsAim) ? 7 : 2);
			if (this.currentAccuracy < num)
			{
				this.currentAccuracy = num;
			}
		}
		else
		{
			if (!this.IsAbakan || !this.AbakanFirstShot)
			{
				this.currentAccuracy += this.recoilAdder(this.player.Ammo.IsAim, this.player.Controller.isSeat, this.player.Controller.isWalk) * this.player.Ammo.AccuracyMult;
			}
			if (this.stab && this.currentAccuracy > this.maxAccuracy / 1.5f)
			{
				this.stab_now.Start();
			}
			if (this.currentAccuracy > this.maxAccuracy)
			{
				this.currentAccuracy = this.maxAccuracy;
			}
		}
		this.accuracyFade.Start();
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x000A36BC File Offset: 0x000A18BC
	internal virtual void AdvanceSound()
	{
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x000A36C0 File Offset: 0x000A18C0
	[Obfuscation(Exclude = true)]
	internal virtual void StopFireLoopSound()
	{
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x000A36C4 File Offset: 0x000A18C4
	[Obfuscation(Exclude = true)]
	internal virtual void ReloadSound()
	{
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x000A36C8 File Offset: 0x000A18C8
	internal virtual void PlayAfterIdleAim()
	{
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000A36CC File Offset: 0x000A18CC
	internal virtual void PlayAfterAimIdle()
	{
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000A36D0 File Offset: 0x000A18D0
	internal virtual void PlayIdleAim()
	{
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x000A36D4 File Offset: 0x000A18D4
	internal virtual void PlayAimIdle()
	{
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000A36D8 File Offset: 0x000A18D8
	internal virtual void PlayIdleOut()
	{
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x000A36DC File Offset: 0x000A18DC
	internal virtual void PlayOutIdle()
	{
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x000A36E0 File Offset: 0x000A18E0
	internal virtual void PlayIdle()
	{
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x000A36E4 File Offset: 0x000A18E4
	internal virtual void Cancel()
	{
		this.G.CancelInvoke();
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000A36F4 File Offset: 0x000A18F4
	protected virtual void OnBrakeFire()
	{
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x000A36F8 File Offset: 0x000A18F8
	protected virtual void OnFire()
	{
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x000A36FC File Offset: 0x000A18FC
	protected virtual void OnReload()
	{
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000A3700 File Offset: 0x000A1900
	protected virtual void OnPompovikReloadStart()
	{
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x000A3704 File Offset: 0x000A1904
	protected virtual void OnReloadStart()
	{
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x000A3708 File Offset: 0x000A1908
	protected virtual void OnCanInsertClip(float time)
	{
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x000A370C File Offset: 0x000A190C
	protected virtual void OnInsertClipSound()
	{
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x000A3710 File Offset: 0x000A1910
	protected virtual void OnReloadEnd()
	{
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x000A3714 File Offset: 0x000A1914
	protected virtual void AdditionalMuzzleFlash()
	{
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x000A3718 File Offset: 0x000A1918
	private MasteringMod GetMod(ModType modType)
	{
		if (modType == ModType.camo)
		{
			return null;
		}
		MasteringMod result = null;
		Dictionary<ModType, int> dictionary;
		if (!this.LOD)
		{
			if (MasteringSuitsInfo.Instance.Suits == null)
			{
				return null;
			}
			Suit suit = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex];
			dictionary = suit.CurrentWeaponsMods[(int)this.type].Mods;
			if (suit.CurrentWeaponsMods[(int)this.type].TemporaryMods != null)
			{
				dictionary = suit.CurrentWeaponsMods[(int)this.type].TemporaryMods;
			}
		}
		else
		{
			dictionary = Utility.StringToMods(this.state.Mods);
		}
		switch (modType)
		{
		case ModType.optic:
			result = ((!dictionary.ContainsKey(ModType.optic)) ? null : ModsStorage.Instance().GetModById(dictionary[ModType.optic]));
			break;
		case ModType.silencer:
			result = ((!dictionary.ContainsKey(ModType.silencer)) ? null : ModsStorage.Instance().GetModById(dictionary[ModType.silencer]));
			break;
		case ModType.tactical:
			result = ((!dictionary.ContainsKey(ModType.tactical)) ? null : ModsStorage.Instance().GetModById(dictionary[ModType.tactical]));
			break;
		case ModType.ammo:
			result = ((!dictionary.ContainsKey(ModType.ammo)) ? null : ModsStorage.Instance().GetModById(dictionary[ModType.ammo]));
			break;
		}
		return result;
	}

	// Token: 0x04000E44 RID: 3652
	public bool LOD;

	// Token: 0x04000E45 RID: 3653
	public GameObject weaponPrefab;

	// Token: 0x04000E46 RID: 3654
	public Texture markTexture;

	// Token: 0x04000E47 RID: 3655
	public string shellName = string.Empty;

	// Token: 0x04000E48 RID: 3656
	public int FireAnimations;

	// Token: 0x04000E49 RID: 3657
	public int FireAimAnimations;

	// Token: 0x04000E4A RID: 3658
	public string muzzleFlashName = string.Empty;

	// Token: 0x04000E4B RID: 3659
	public string silencedMuzzleFlashName = string.Empty;

	// Token: 0x04000E4C RID: 3660
	public AudioClip boltReloadSounds;

	// Token: 0x04000E4D RID: 3661
	public AudioClip[] reloadSounds = new AudioClip[0];

	// Token: 0x04000E4E RID: 3662
	public float[] reloadTimes;

	// Token: 0x04000E4F RID: 3663
	public bool[] reloadBools;

	// Token: 0x04000E50 RID: 3664
	public AudioClip[] fireSounds = new AudioClip[0];

	// Token: 0x04000E51 RID: 3665
	public bool secondSoundScheme;

	// Token: 0x04000E52 RID: 3666
	public float singleTime = 0.088f;

	// Token: 0x04000E53 RID: 3667
	public AudioClip loopFire;

	// Token: 0x04000E54 RID: 3668
	public AudioClip tailFire;

	// Token: 0x04000E55 RID: 3669
	public AudioClip[] modFireSounds = new AudioClip[0];

	// Token: 0x04000E56 RID: 3670
	public bool modSecondSoundScheme;

	// Token: 0x04000E57 RID: 3671
	public float modSingleTime = 0.088f;

	// Token: 0x04000E58 RID: 3672
	public AudioClip modLoopFire;

	// Token: 0x04000E59 RID: 3673
	public AudioClip modTailFire;

	// Token: 0x04000E5A RID: 3674
	public float equipTime;

	// Token: 0x04000E5B RID: 3675
	public AudioClip equip;

	// Token: 0x04000E5C RID: 3676
	public bool correct;

	// Token: 0x04000E5D RID: 3677
	public Vector3 rootPositionCorrect = Vector3.zero;

	// Token: 0x04000E5E RID: 3678
	public Vector3 rootRotationCorrect = new Vector3(90f, 0f, 0f);

	// Token: 0x04000E5F RID: 3679
	public bool correctAlways;

	// Token: 0x04000E60 RID: 3680
	internal Weapons type = Weapons.none;

	// Token: 0x04000E61 RID: 3681
	internal string InterfaceName = string.Empty;

	// Token: 0x04000E62 RID: 3682
	internal string ModInterfaceName = string.Empty;

	// Token: 0x04000E63 RID: 3683
	internal WeaponNature weaponNature = WeaponNature.none;

	// Token: 0x04000E64 RID: 3684
	internal bool auto;

	// Token: 0x04000E65 RID: 3685
	internal int armoryBlock;

	// Token: 0x04000E66 RID: 3686
	internal WeaponUseType weaponUseType = WeaponUseType.none;

	// Token: 0x04000E67 RID: 3687
	internal bool pompovik;

	// Token: 0x04000E68 RID: 3688
	internal string Name;

	// Token: 0x04000E69 RID: 3689
	internal string ShortName;

	// Token: 0x04000E6A RID: 3690
	internal string Info;

	// Token: 0x04000E6B RID: 3691
	internal int price;

	// Token: 0x04000E6C RID: 3692
	internal int IndestructiblePrice;

	// Token: 0x04000E6D RID: 3693
	internal float UnitRepairCost;

	// Token: 0x04000E6E RID: 3694
	internal int durability = 100;

	// Token: 0x04000E6F RID: 3695
	internal int wtaskPrice = 1000;

	// Token: 0x04000E70 RID: 3696
	internal float distanceReducerE1 = 1f;

	// Token: 0x04000E71 RID: 3697
	internal float distanceReducerE2 = 1f;

	// Token: 0x04000E72 RID: 3698
	internal bool isVisible = true;

	// Token: 0x04000E73 RID: 3699
	internal bool isPremium;

	// Token: 0x04000E74 RID: 3700
	internal bool isDonate;

	// Token: 0x04000E75 RID: 3701
	internal int[] rentPrice;

	// Token: 0x04000E76 RID: 3702
	internal int[] rentTime;

	// Token: 0x04000E77 RID: 3703
	internal bool isSocial;

	// Token: 0x04000E78 RID: 3704
	internal int friendsRequired = -1;

	// Token: 0x04000E79 RID: 3705
	internal int permanentPrice;

	// Token: 0x04000E7A RID: 3706
	internal int WtaskGearID;

	// Token: 0x04000E7B RID: 3707
	internal Wtask wtask;

	// Token: 0x04000E7C RID: 3708
	internal string WtaskName = string.Empty;

	// Token: 0x04000E7D RID: 3709
	internal string WtaskDescription = string.Empty;

	// Token: 0x04000E7E RID: 3710
	internal string WtaskGearName = string.Empty;

	// Token: 0x04000E7F RID: 3711
	internal string WtaskGearDesc = string.Empty;

	// Token: 0x04000E80 RID: 3712
	internal string WtaskInfo = string.Empty;

	// Token: 0x04000E81 RID: 3713
	internal string WtaskAmmo = string.Empty;

	// Token: 0x04000E82 RID: 3714
	internal WeaponSpecific weaponSpecific = WeaponSpecific.none;

	// Token: 0x04000E83 RID: 3715
	internal float focalDistance = 0.29f;

	// Token: 0x04000E84 RID: 3716
	internal string ammoString = string.Empty;

	// Token: 0x04000E85 RID: 3717
	internal int[] GridPosition = new int[2];

	// Token: 0x04000E86 RID: 3718
	internal WeaponState state = new WeaponState();

	// Token: 0x04000E87 RID: 3719
	internal Invoker G = new Invoker();

	// Token: 0x04000E88 RID: 3720
	internal BaseNetPlayer player;

	// Token: 0x04000E89 RID: 3721
	internal bool burst;

	// Token: 0x04000E8A RID: 3722
	internal bool duplet;

	// Token: 0x04000E8B RID: 3723
	internal bool canDuplet;

	// Token: 0x04000E8C RID: 3724
	internal bool isBolt;

	// Token: 0x04000E8D RID: 3725
	internal bool isBelt;

	// Token: 0x04000E8E RID: 3726
	internal bool DoubleMagazine;

	// Token: 0x04000E8F RID: 3727
	internal bool FirstReload = true;

	// Token: 0x04000E90 RID: 3728
	internal bool UseShotGunReload;

	// Token: 0x04000E91 RID: 3729
	private bool stab;

	// Token: 0x04000E92 RID: 3730
	internal eTimer stab_now = new eTimer();

	// Token: 0x04000E93 RID: 3731
	internal int caseshot = 1;

	// Token: 0x04000E94 RID: 3732
	private float _sensMult = 1f;

	// Token: 0x04000E95 RID: 3733
	internal float hearRadius = -1f;

	// Token: 0x04000E96 RID: 3734
	internal int skillReq = -1;

	// Token: 0x04000E97 RID: 3735
	internal bool unlockBySkill;

	// Token: 0x04000E98 RID: 3736
	internal int magSize;

	// Token: 0x04000E99 RID: 3737
	internal int bagSize;

	// Token: 0x04000E9A RID: 3738
	internal int hideAmmoCount;

	// Token: 0x04000E9B RID: 3739
	private float repair_coef = 1f;

	// Token: 0x04000E9C RID: 3740
	internal bool forceEnableWtask;

	// Token: 0x04000E9D RID: 3741
	internal float accuracyHitFade = 1f;

	// Token: 0x04000E9E RID: 3742
	private eTimer accuracyFade = new eTimer();

	// Token: 0x04000E9F RID: 3743
	private float accuracyFader = 1f;

	// Token: 0x04000EA0 RID: 3744
	private bool first100Accuracy;

	// Token: 0x04000EA1 RID: 3745
	private First100Accuracy first100Accuracy_now;

	// Token: 0x04000EA2 RID: 3746
	private float recoilSeatMult = 1f;

	// Token: 0x04000EA3 RID: 3747
	private float aimRecoilMult = 1f;

	// Token: 0x04000EA4 RID: 3748
	private float seatAccuracyMult = 1f;

	// Token: 0x04000EA5 RID: 3749
	private float walkAccuracyMult = 1f;

	// Token: 0x04000EA6 RID: 3750
	private float aimFov = 30f;

	// Token: 0x04000EA7 RID: 3751
	internal float worldAimFov = 5f;

	// Token: 0x04000EA8 RID: 3752
	private float TDP = 0.3f;

	// Token: 0x04000EA9 RID: 3753
	private float TYP = 0.4f;

	// Token: 0x04000EAA RID: 3754
	private float TDA = 0.4f;

	// Token: 0x04000EAB RID: 3755
	private float TYA = 0.4f;

	// Token: 0x04000EAC RID: 3756
	private float OutIdleMult = 1f;

	// Token: 0x04000EAD RID: 3757
	private float IdleOutMult = 1f;

	// Token: 0x04000EAE RID: 3758
	private float IdleAimMult = 1f;

	// Token: 0x04000EAF RID: 3759
	private float AimIdleMult = 1f;

	// Token: 0x04000EB0 RID: 3760
	private float AIMP = 0.2f;

	// Token: 0x04000EB1 RID: 3761
	private float AIMA = 0.2f;

	// Token: 0x04000EB2 RID: 3762
	private bool hasOptic;

	// Token: 0x04000EB3 RID: 3763
	private bool hasLTP;

	// Token: 0x04000EB4 RID: 3764
	private bool hasKolimator;

	// Token: 0x04000EB5 RID: 3765
	private bool hasGrip;

	// Token: 0x04000EB6 RID: 3766
	private bool hasSS;

	// Token: 0x04000EB7 RID: 3767
	private bool hasCompensator;

	// Token: 0x04000EB8 RID: 3768
	private bool hasSlug;

	// Token: 0x04000EB9 RID: 3769
	private bool hasFlashlight;

	// Token: 0x04000EBA RID: 3770
	private bool modHasOptic;

	// Token: 0x04000EBB RID: 3771
	private bool modHasLTP;

	// Token: 0x04000EBC RID: 3772
	private bool modHasKolimator;

	// Token: 0x04000EBD RID: 3773
	private bool modHasGrip;

	// Token: 0x04000EBE RID: 3774
	private bool modHasSS;

	// Token: 0x04000EBF RID: 3775
	private bool modHasCompensator;

	// Token: 0x04000EC0 RID: 3776
	private bool modHasSlug;

	// Token: 0x04000EC1 RID: 3777
	private bool modHasFlashlight;

	// Token: 0x04000EC2 RID: 3778
	private bool isModable;

	// Token: 0x04000EC3 RID: 3779
	internal bool AbakanFirstShot = true;

	// Token: 0x04000EC4 RID: 3780
	private float minRecoil = 0.13f;

	// Token: 0x04000EC5 RID: 3781
	private float maxRecoil = 1f;

	// Token: 0x04000EC6 RID: 3782
	private float currentAccuracy;

	// Token: 0x04000EC7 RID: 3783
	private float _tempDamageReduceDistanceMin;

	// Token: 0x04000EC8 RID: 3784
	private float _tempDamageReduceDistanceMax;

	// Token: 0x04000EC9 RID: 3785
	internal float damageReduceDistanceMin = 1000f;

	// Token: 0x04000ECA RID: 3786
	internal float damageReduceDistanceMax = 10000f;

	// Token: 0x04000ECB RID: 3787
	internal float damageReduceDistanceMinSkillBonus;

	// Token: 0x04000ECC RID: 3788
	internal float damageReduceDistanceMaxSkillBonus;

	// Token: 0x04000ECD RID: 3789
	internal float damageReduceDistanceMinModBonus;

	// Token: 0x04000ECE RID: 3790
	internal float damageReduceDistanceMaxModBonus;

	// Token: 0x04000ECF RID: 3791
	private float baseAccuracyProc;

	// Token: 0x04000ED0 RID: 3792
	private float baseRecoilProc;

	// Token: 0x04000ED1 RID: 3793
	private float baseDamageProc;

	// Token: 0x04000ED2 RID: 3794
	private float baseFirerateProc;

	// Token: 0x04000ED3 RID: 3795
	private float baseMobilityProc;

	// Token: 0x04000ED4 RID: 3796
	private float baseReloadSpeedProc;

	// Token: 0x04000ED5 RID: 3797
	private float basePierceProc = 75f;

	// Token: 0x04000ED6 RID: 3798
	private float modAccuracyProcBonus;

	// Token: 0x04000ED7 RID: 3799
	private float modRecoilProcBonus;

	// Token: 0x04000ED8 RID: 3800
	private float modDamageProcBonus;

	// Token: 0x04000ED9 RID: 3801
	private float modFirerateProcBonus;

	// Token: 0x04000EDA RID: 3802
	private float modMobilityProcBonus;

	// Token: 0x04000EDB RID: 3803
	private float modReloadSpeedProcBonus;

	// Token: 0x04000EDC RID: 3804
	private float modPierceProcBonus;

	// Token: 0x04000EDD RID: 3805
	private float skillAccuracyProcBonus;

	// Token: 0x04000EDE RID: 3806
	private float skillRecoilProcBonus;

	// Token: 0x04000EDF RID: 3807
	private float skillDamageProcBonus;

	// Token: 0x04000EE0 RID: 3808
	private float skillFirerateProcBonus;

	// Token: 0x04000EE1 RID: 3809
	private float skillMobilityProcBonus;

	// Token: 0x04000EE2 RID: 3810
	private float skillReloadSpeedProcBonus;

	// Token: 0x04000EE3 RID: 3811
	private float skillPierceProcBonus;

	// Token: 0x04000EE4 RID: 3812
	private float scatter = 50f;

	// Token: 0x04000EE5 RID: 3813
	internal float enableBeltTime;

	// Token: 0x04000EE6 RID: 3814
	private float reloadTime;
}
