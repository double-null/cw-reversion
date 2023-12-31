using System;
using System.Reflection;
using Assets.Scripts.Game;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020001A3 RID: 419
[AddComponentMenu("Scripts/Game/BaseNetPlayer")]
internal class BaseNetPlayer : PoolableBehaviour, cwID, cwEntityType, cwNetworkSerializable, eObserved
{
	// Token: 0x06000C6A RID: 3178 RVA: 0x0009698C File Offset: 0x00094B8C
	public override void OnPoolDespawn()
	{
		ReportSystem.Instance.ClearReportList(this.UserID);
		this.PacketNum = 0;
		this.DespawnPlayerObject();
		this.ClanCompositeBuff = 0;
		this.ClanCompositeBuffItem = 0;
		this._hightlitedTime = 0f;
		this.ClanBuffSelf = 0;
		this.ClanBuffDistance = 0;
		this.ClanBuffTeammate = 0;
		this.playerObject = null;
		this.ammo = null;
		this.controller = null;
		this.animations = null;
		this.animationsThird = null;
		this.mainCamera = null;
		this.playerInfo.Clear();
		this.UInput.Clear();
		this.UC = null;
		this.update = UpdateType.NoUpdate;
		this.loadingState = LoadingState.none;
		this.equiped = WeaponEquipedState.Secondary;
		this.SecondaryIndex = 127;
		this.PrimaryIndex = 127;
		this.SecondaryMod = false;
		this.PrimaryMod = false;
		this.expMult = 1;
		this.weaponExpMult = 1;
		this.boots = 1f;
		this.frog = 1f;
		this.explosiveDamageMult = 1f;
		this.grenadeDamageMult = 1f;
		this.grenadeExplosionRadiusMult = 1f;
		this.rightClick = new Alpha();
		this.fTalkTime = 0f;
		this.iChatMessageCounter = 0;
		this.plantMult = 1f;
		this.difuseMult = 1f;
		this.wtaskMult = 1f;
		this.achievementsMult = 1f;
		this.ImmortalityTime = 2f;
		this.ImmortalTimer = new eTimer();
		this.regen = new eTimer();
		this._isFirstSpawn = true;
		base.OnPoolDespawn();
	}

	// Token: 0x1700018C RID: 396
	// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00096B20 File Offset: 0x00094D20
	// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00096B28 File Offset: 0x00094D28
	public virtual bool AimSynchFromServer { get; protected set; }

	// Token: 0x1700018D RID: 397
	// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00096B34 File Offset: 0x00094D34
	// (set) Token: 0x06000C6E RID: 3182 RVA: 0x00096B3C File Offset: 0x00094D3C
	public UserInfo UserInfo
	{
		get
		{
			return this.userInfo;
		}
		set
		{
			this.userInfo = value;
		}
	}

	// Token: 0x1700018E RID: 398
	// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00096B48 File Offset: 0x00094D48
	public PlayerClass PlayerClass
	{
		get
		{
			return this.playerInfo.playerClass;
		}
	}

	// Token: 0x1700018F RID: 399
	// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00096B58 File Offset: 0x00094D58
	// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00096B60 File Offset: 0x00094D60
	public GameObject PlayerObject
	{
		get
		{
			return this.playerObject;
		}
		set
		{
			this.playerObject = value;
			if (this.playerObject != null)
			{
				this.PlayerTransform = this.playerObject.transform;
			}
		}
	}

	// Token: 0x17000190 RID: 400
	// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00096B8C File Offset: 0x00094D8C
	public float LifeTime
	{
		get
		{
			return Time.time - this._reviveTime;
		}
	}

	// Token: 0x17000191 RID: 401
	// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00096B9C File Offset: 0x00094D9C
	public float LifeTimeExpCoef
	{
		get
		{
			return (!this.IsVip) ? 1f : Math.Min(2.5f, 1f + (float)((int)(this.LifeTime / 20f)) / 10f);
		}
	}

	// Token: 0x17000192 RID: 402
	// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00096BE4 File Offset: 0x00094DE4
	public bool AimWithOptic
	{
		get
		{
			return this.ammo && this.ammo.weaponEquiped && this.ammo.state.isAim && this.ammo.CurrentWeapon.Optic;
		}
	}

	// Token: 0x17000193 RID: 403
	// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00096C3C File Offset: 0x00094E3C
	public BaseAmmunitions Ammo
	{
		get
		{
			return this.ammo;
		}
	}

	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00096C44 File Offset: 0x00094E44
	public BaseMoveController Controller
	{
		get
		{
			return this.controller;
		}
	}

	// Token: 0x17000195 RID: 405
	// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00096C4C File Offset: 0x00094E4C
	public Animations Animations
	{
		get
		{
			return this.animations;
		}
	}

	// Token: 0x17000196 RID: 406
	// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00096C54 File Offset: 0x00094E54
	public AnimationsThird AnimationsThird
	{
		get
		{
			return this.animationsThird;
		}
	}

	// Token: 0x17000197 RID: 407
	// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00096C5C File Offset: 0x00094E5C
	public Camera MainCamera
	{
		get
		{
			return this.mainCamera;
		}
	}

	// Token: 0x17000198 RID: 408
	// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00096C64 File Offset: 0x00094E64
	public int ExpMult
	{
		get
		{
			return this.expMult;
		}
	}

	// Token: 0x17000199 RID: 409
	// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00096C6C File Offset: 0x00094E6C
	public int WeaponExpMult
	{
		get
		{
			return this.weaponExpMult;
		}
	}

	// Token: 0x1700019A RID: 410
	// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00096C74 File Offset: 0x00094E74
	public virtual Vector3 Forward
	{
		get
		{
			return (!this.controller) ? Vector3.forward : this.controller.root.forward;
		}
	}

	// Token: 0x1700019B RID: 411
	// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00096CAC File Offset: 0x00094EAC
	public virtual Vector3 Position
	{
		get
		{
			return (!this.controller) ? CVars.h_v3infinity : this.controller.Position;
		}
	}

	// Token: 0x1700019C RID: 412
	// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00096CD4 File Offset: 0x00094ED4
	public virtual Vector3 Euler
	{
		get
		{
			return (!this.controller) ? Vector3.zero : this.controller.state.euler;
		}
	}

	// Token: 0x1700019D RID: 413
	// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00096D0C File Offset: 0x00094F0C
	public int WeaponEquipedIndex
	{
		get
		{
			if (this.IsAlive)
			{
				this.equiped = this.Ammo.state.equiped;
			}
			if (this.equiped == WeaponEquipedState.Secondary)
			{
				return this.SecondaryIndex;
			}
			return this.PrimaryIndex;
		}
	}

	// Token: 0x1700019E RID: 414
	// (get) Token: 0x06000C80 RID: 3200 RVA: 0x00096D54 File Offset: 0x00094F54
	public bool IsSpectactor
	{
		get
		{
			return this.playerInfo.playerType == PlayerType.spectactor;
		}
	}

	// Token: 0x1700019F RID: 415
	// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00096D64 File Offset: 0x00094F64
	// (set) Token: 0x06000C82 RID: 3202 RVA: 0x00096D74 File Offset: 0x00094F74
	public PlayerType PlayerType
	{
		get
		{
			return this.playerInfo.playerType;
		}
		set
		{
			this.playerInfo.playerType = value;
		}
	}

	// Token: 0x170001A0 RID: 416
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x00096D84 File Offset: 0x00094F84
	public bool IsAlive
	{
		get
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (!callingAssembly.Equals(Assembly.GetExecutingAssembly()))
			{
				base.StartCoroutine(SingletoneComponent<Main>.Instance.TakeAndSendScreenshot("gayCheck1", Main.UserInfo.userID));
			}
			return !this.playerInfo.dead;
		}
	}

	// Token: 0x170001A1 RID: 417
	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00096DDC File Offset: 0x00094FDC
	public bool IsPlayerObject
	{
		get
		{
			return this.playerObject != null;
		}
	}

	// Token: 0x170001A2 RID: 418
	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00096DEC File Offset: 0x00094FEC
	public bool IsDeadOrSpectactor
	{
		get
		{
			return !this.IsAlive || this.IsSpectactor;
		}
	}

	// Token: 0x170001A3 RID: 419
	// (get) Token: 0x06000C86 RID: 3206 RVA: 0x00096E04 File Offset: 0x00095004
	public ObscuredInt UserID
	{
		get
		{
			if (this.playerInfo.Nick == "unknown")
			{
				return -1;
			}
			return this.playerInfo.userID;
		}
	}

	// Token: 0x170001A4 RID: 420
	// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00096E40 File Offset: 0x00095040
	// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00096E50 File Offset: 0x00095050
	public virtual ObscuredInt ID
	{
		get
		{
			return this.playerInfo.playerID;
		}
		set
		{
		}
	}

	// Token: 0x170001A5 RID: 421
	// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00096E54 File Offset: 0x00095054
	public virtual EntityType EntityType
	{
		get
		{
			return EntityType.player;
		}
	}

	// Token: 0x170001A6 RID: 422
	// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00096E58 File Offset: 0x00095058
	public string Nick
	{
		get
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (!callingAssembly.Equals(Assembly.GetExecutingAssembly()))
			{
				base.StartCoroutine(SingletoneComponent<Main>.Instance.TakeAndSendScreenshot("gayCheck2", Main.UserInfo.userID));
			}
			return (!(this.playerInfo.Nick == "unknown")) ? this.playerInfo.Nick.ToString() : string.Empty;
		}
	}

	// Token: 0x170001A7 RID: 423
	// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00096ED4 File Offset: 0x000950D4
	public string NickColor
	{
		get
		{
			return this.playerInfo.NickColor;
		}
	}

	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00096EE4 File Offset: 0x000950E4
	public string ClanTag
	{
		get
		{
			return this.playerInfo.clanTag;
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000C8D RID: 3213 RVA: 0x00096EF4 File Offset: 0x000950F4
	public int Level
	{
		get
		{
			return this.playerInfo.level;
		}
	}

	// Token: 0x170001AA RID: 426
	// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00096F04 File Offset: 0x00095104
	public int Weight
	{
		get
		{
			return (this.Level >= 61) ? (this.Level + 2 * ((this.Level % 10 == 0) ? 10 : (this.Level % 10))) : this.Level;
		}
	}

	// Token: 0x170001AB RID: 427
	// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00096F50 File Offset: 0x00095150
	public int Points
	{
		get
		{
			return (!(this.playerInfo.Nick == "unknown")) ? this.playerInfo.points : 0;
		}
	}

	// Token: 0x170001AC RID: 428
	// (get) Token: 0x06000C90 RID: 3216 RVA: 0x00096F80 File Offset: 0x00095180
	public int LevelX10PlusPoints
	{
		get
		{
			return (!(this.playerInfo.Nick == "unknown")) ? (this.Level * 10 + this.Points) : 0;
		}
	}

	// Token: 0x170001AD RID: 429
	// (get) Token: 0x06000C91 RID: 3217 RVA: 0x00096FC0 File Offset: 0x000951C0
	// (set) Token: 0x06000C92 RID: 3218 RVA: 0x00097014 File Offset: 0x00095214
	public float Health
	{
		get
		{
			Assembly callingAssembly = Assembly.GetCallingAssembly();
			if (!callingAssembly.Equals(Assembly.GetExecutingAssembly()))
			{
				base.StartCoroutine(SingletoneComponent<Main>.Instance.TakeAndSendScreenshot("gayCheck3", Main.UserInfo.userID));
			}
			return this.playerInfo.Health;
		}
		set
		{
			this.playerInfo.Health = value;
		}
	}

	// Token: 0x170001AE RID: 430
	// (get) Token: 0x06000C93 RID: 3219 RVA: 0x00097024 File Offset: 0x00095224
	// (set) Token: 0x06000C94 RID: 3220 RVA: 0x00097034 File Offset: 0x00095234
	public float Armor
	{
		get
		{
			return this.playerInfo.armor;
		}
		set
		{
			this.playerInfo.armor = value;
		}
	}

	// Token: 0x170001AF RID: 431
	// (get) Token: 0x06000C95 RID: 3221 RVA: 0x00097044 File Offset: 0x00095244
	public bool IsBear
	{
		get
		{
			return this.playerInfo.playerType == PlayerType.bear;
		}
	}

	// Token: 0x170001B0 RID: 432
	// (get) Token: 0x06000C96 RID: 3222 RVA: 0x00097054 File Offset: 0x00095254
	public bool BeaconUser
	{
		get
		{
			return BIT.AND((int)this.playerInfo.buffs, 32);
		}
	}

	// Token: 0x170001B1 RID: 433
	// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00097068 File Offset: 0x00095268
	public bool InPlacementZone
	{
		get
		{
			return BIT.AND((int)this.playerInfo.buffs, 16);
		}
	}

	// Token: 0x170001B2 RID: 434
	// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0009707C File Offset: 0x0009527C
	public bool InHomeBase
	{
		get
		{
			return (!this.IsBear && BIT.AND((int)this.playerInfo.buffs, 262144)) || (this.IsBear && BIT.AND((int)this.playerInfo.buffs, 131072));
		}
	}

	// Token: 0x170001B3 RID: 435
	// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000970D4 File Offset: 0x000952D4
	public bool IsVip
	{
		get
		{
			return BIT.AND((int)this.playerInfo.buffs, 128);
		}
	}

	// Token: 0x170001B4 RID: 436
	// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000970EC File Offset: 0x000952EC
	public bool IsRegen
	{
		get
		{
			return BIT.AND((int)this.playerInfo.buffs, 8);
		}
	}

	// Token: 0x170001B5 RID: 437
	// (get) Token: 0x06000C9B RID: 3227 RVA: 0x00097100 File Offset: 0x00095300
	public bool DBRunning
	{
		get
		{
			return BIT.AND((int)this.playerInfo.buffs, 256) || BIT.AND((int)this.playerInfo.buffs, 512);
		}
	}

	// Token: 0x170001B6 RID: 438
	// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00097140 File Offset: 0x00095340
	public bool Immortal
	{
		get
		{
			return this.ImmortalTimer.Enabled && this.ImmortalTimer.Elapsed < this.ImmortalityTime;
		}
	}

	// Token: 0x06000C9D RID: 3229 RVA: 0x00097174 File Offset: 0x00095374
	private void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
	{
		double num = info.timestamp;
		if (num == -1.0)
		{
			num = Network.time;
		}
		if (stream.isReading)
		{
			this.OnNetworkEvent(new eNetworkStream(stream, num));
		}
		else
		{
			this.OnNetworkEvent(new eNetworkStream(this.update, stream, num));
		}
	}

	// Token: 0x06000C9E RID: 3230 RVA: 0x000971D0 File Offset: 0x000953D0
	public void OnNetworkEvent(eNetworkStream stream)
	{
		if (stream.isNoUpdate)
		{
			return;
		}
		if (stream.isWriting)
		{
			try
			{
				this.Send(stream);
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
		}
		else
		{
			try
			{
				this.Recieve(stream);
			}
			catch (Exception e2)
			{
				global::Console.exception(e2);
			}
		}
	}

	// Token: 0x06000C9F RID: 3231 RVA: 0x00097260 File Offset: 0x00095460
	public UpdateType GetUpdateType()
	{
		return this.update;
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x00097268 File Offset: 0x00095468
	protected virtual void Send(eNetworkStream stream)
	{
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0009726C File Offset: 0x0009546C
	protected virtual void Recieve(eNetworkStream stream)
	{
	}

	// Token: 0x06000CA2 RID: 3234 RVA: 0x00097270 File Offset: 0x00095470
	public virtual void Serialize(eNetworkStream stream)
	{
		this.playerInfo.Serialize(stream);
		if (!this.IsAlive)
		{
			return;
		}
		this.controller.Serialize(stream);
		this.ammo.Serialize(stream);
		this.UC.Serialize(stream);
	}

	// Token: 0x06000CA3 RID: 3235 RVA: 0x000972BC File Offset: 0x000954BC
	public virtual void Deserialize(eNetworkStream stream)
	{
		this.playerInfo.Deserialize(stream);
		if (!this.IsAlive)
		{
			return;
		}
		this.controller.Deserialize(stream);
		this.ammo.Deserialize(stream);
		this.UC.Deserialize(stream);
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x00097308 File Offset: 0x00095508
	protected void ParseSkill(Skills skill)
	{
		if (skill == Skills.efd)
		{
			this.Ammo.state.grenadeCount++;
		}
		if (skill == Skills.efd2)
		{
			this.Ammo.state.grenadeCount++;
		}
		if (skill == Skills.fragarmor)
		{
			this.explosiveDamageMult *= 0.55f;
		}
		if (skill == Skills.efd_dam)
		{
			this.grenadeDamageMult *= 1.5f;
		}
		if (skill == Skills.efd_throw)
		{
			this.Ammo.throwPowerMult *= 1.5f;
		}
		if (skill == Skills.efd_radius)
		{
			this.grenadeExplosionRadiusMult *= 1.5f;
		}
		if (skill == Skills.hp10)
		{
			this.Health += 10f;
		}
		if (skill == Skills.aimspeed1)
		{
			this.Ammo.aimBonusMult *= 1.2f;
		}
		if (skill == Skills.aimspeed2)
		{
			this.Ammo.aimBonusMult *= 1.45f;
		}
		if (skill == Skills.armor10)
		{
			this.Armor += 20f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
			}
		}
		if (skill == Skills.hp5)
		{
			this.Health += 5f;
		}
		if (skill == Skills.des1)
		{
			this.plantMult *= 1.5f;
		}
		if (skill == Skills.des2)
		{
			this.difuseMult *= 1.5f;
		}
		if (skill == Skills.car_exp2 && this.LastExpMult != Skills.car_exp3)
		{
			this.LastExpMult = Skills.car_exp2;
			this.expMult *= 2;
		}
		if (skill == Skills.car_exp3)
		{
			if (this.LastExpMult == Skills.car_exp2)
			{
				this.expMult /= 2;
			}
			this.expMult *= 3;
			this.LastExpMult = Skills.car_exp3;
		}
		if (skill == Skills.car_wtask)
		{
			this.wtaskMult *= 2f;
		}
		if (skill == Skills.car_ach)
		{
			this.achievementsMult *= 2f;
		}
		if (skill == Skills.car_immortal)
		{
			this.ImmortalityTime = 3.5f;
		}
		if (skill == Skills.car_expbonus1)
		{
			this.weaponExpMult *= 2;
		}
		if (skill == Skills.sitspeed)
		{
			this.Controller.seatBonusMult += 0.25f;
		}
		if (skill == Skills.arm_limbs && this is ServerNetPlayer)
		{
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Calf ").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Thigh").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Calf ").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Thigh").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L UpperArm").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Forearm").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R UpperArm").GetComponent<DamageX>().X *= 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Forearm").GetComponent<DamageX>().X *= 0.8f;
		}
		if (skill == Skills.hp5_scout)
		{
			this.Health += 10f;
		}
		if (skill == Skills.knife)
		{
			this.Ammo.KnifeDelay *= 0.75f;
		}
		if (skill == Skills.hp5_assault)
		{
			this.Health += 5f;
		}
		if (skill == Skills.arm1)
		{
			this.Armor = 20f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
			}
		}
		if (skill == Skills.arm2)
		{
			this.Armor = 40f;
		}
		if (skill == Skills.arm3)
		{
			this.Armor = 60f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Pelvis").GetComponent<DamageX>().Armor = true;
			}
		}
		if (skill == Skills.arm4)
		{
			this.Armor = 80f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_R UpperArm").GetComponent<DamageX>().Armor = true;
				Utility.FindHierarchy(this.PlayerTransform, "NPC_L UpperArm").GetComponent<DamageX>().Armor = true;
			}
		}
		if (skill == Skills.arm5)
		{
			this.Armor = 130f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Head").GetComponent<DamageX>().Armor = true;
			}
		}
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00097850 File Offset: 0x00095A50
	protected void ParseClanSkill(Cl_Skills clanSkill)
	{
		if (this.playerInfo.clanTag.Length < 1)
		{
			return;
		}
		if (clanSkill == Cl_Skills.cl_efd)
		{
			this.Ammo.state.grenadeCount++;
		}
		if (clanSkill == Cl_Skills.cl_armor)
		{
			this.Armor += 20f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
			}
		}
		if (clanSkill == Cl_Skills.cl_dest1 && this.playerInfo.playerClass == PlayerClass.destroyer)
		{
			this.Armor += 20f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
			}
			this.Health += 15f;
		}
		if (clanSkill == Cl_Skills.cl_dest2 && this.playerInfo.playerClass == PlayerClass.destroyer)
		{
			this.Armor += 20f;
			if (this is ServerNetPlayer)
			{
				Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
			}
			this.Health += 15f;
		}
		if (clanSkill == Cl_Skills.cl_sni2 && this.playerInfo.playerClass == PlayerClass.sniper)
		{
			this.hasAntiFlareLens = true;
		}
		if (clanSkill == Cl_Skills.cl_weap2 && this.playerInfo.playerClass == PlayerClass.gunsmith)
		{
			this.plantMult *= 2f;
			this.difuseMult *= 2f;
		}
		if (clanSkill != Cl_Skills.cl_weap3 || this.playerInfo.playerClass == PlayerClass.gunsmith)
		{
		}
		if (clanSkill == Cl_Skills.cl_train1)
		{
			this.Health += 5f;
		}
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00097A28 File Offset: 0x00095C28
	protected void EnableHardCoreSkills()
	{
		this.Ammo.state.grenadeCount++;
		this.Armor = 50f;
		if (this is ServerNetPlayer)
		{
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = true;
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00097A80 File Offset: 0x00095C80
	protected void EnableTestVipParams()
	{
		this.Armor += 200f;
		if (this is ServerNetPlayer)
		{
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Head").GetComponent<DamageX>().Armor = true;
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00097AC8 File Offset: 0x00095CC8
	public int GetKillBonus(ServerNetPlayer enemy)
	{
		int result;
		try
		{
			if ((float)this.playerInfo.level >= (float)enemy.playerInfo.level)
			{
				result = 0;
			}
			else
			{
				result = Mathf.CeilToInt(((float)enemy.playerInfo.level - (float)this.playerInfo.level) / 10f) * 2;
			}
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			result = 0;
		}
		return result;
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00097B5C File Offset: 0x00095D5C
	public virtual void Hit(int playerID, int targetID, float health, float armor)
	{
		if (this.animationsThird)
		{
			this.animationsThird.PlayHit();
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x00097B7C File Offset: 0x00095D7C
	public bool IsTeam(bool IsBear)
	{
		return this.IsBear == IsBear;
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00097B88 File Offset: 0x00095D88
	public bool IsTeam(BaseNetPlayer player)
	{
		return Main.IsTeamGame && this.IsBear == player.IsBear;
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00097BA8 File Offset: 0x00095DA8
	public bool IsClanmate(BaseNetPlayer player)
	{
		return this.ClanTag == player.ClanTag;
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x00097BBC File Offset: 0x00095DBC
	public void Hightlight()
	{
		this._hightlitedTime = Time.time + 4f;
	}

	// Token: 0x06000CAE RID: 3246 RVA: 0x00097BD0 File Offset: 0x00095DD0
	public void ChangeAim()
	{
		bool holdAim = Main.UserInfo.settings.binds.holdAim;
		if (this.ammo.IsAim)
		{
			if (!this.getTime)
			{
				this.cTime = Time.realtimeSinceStartup;
				this.getTime = true;
			}
			if (this.playerInfo.skillUnlocked(Skills.conc) && this.Ammo.state.equiped == WeaponEquipedState.Primary && this.cTime + 0.2f < Time.realtimeSinceStartup)
			{
				if (this.UInput.GetKey(UKeyCode.aim, true))
				{
					if (!this.rightClick.Visible)
					{
						this.rightClick.Show(0.5f, 0f);
					}
				}
				else
				{
					this.rightClick.Hide(0.5f, 0f);
				}
				if ((this.UInput.GetKeyUp(UKeyCode.aim, true) && this.rightClick.visibility_clean < 0.5f) || (holdAim && !this.UInput.GetKey(UKeyCode.aim, true)))
				{
					this.ammo.ChangeAimMode(true);
				}
			}
			else if (this.UInput.GetKeyDown(UKeyCode.aim, true) || (holdAim && !this.UInput.GetKey(UKeyCode.aim, true)))
			{
				this.ammo.ChangeAimMode(true);
			}
		}
		else
		{
			if (this.UInput.GetKeyDown(UKeyCode.aim, true) && holdAim)
			{
				this.ammo.ForcedChangeAimMode = false;
			}
			if (this.UInput.GetKeyDown(UKeyCode.aim, true) || (holdAim && this.UInput.GetKey(UKeyCode.aim, true)))
			{
				this.ammo.ChangeAimMode(true);
				this.getTime = false;
			}
		}
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00097DBC File Offset: 0x00095FBC
	protected virtual void KillPlayer()
	{
		if (this.IsAlive && this.IsPlayerObject)
		{
			this.equiped = this.Ammo.state.equiped;
		}
		this.playerInfo.dead = true;
		this.playerInfo.Health = 0f;
		this.playerInfo.buffs = Buffs.none;
		this.playerInfo.armor = 0f;
	}

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00097E30 File Offset: 0x00096030
	protected virtual void RevivePlayer()
	{
		this.playerInfo.dead = false;
		this.playerInfo.Health = CVars.g_baseHealthAmount;
		this.playerInfo.buffs = Buffs.none;
		this.playerInfo.armor = 0f;
		this._reviveTime = Time.time;
	}

	// Token: 0x06000CB1 RID: 3249 RVA: 0x00097E80 File Offset: 0x00096080
	protected virtual void DespawnPlayerObject()
	{
		this.LastExpMult = Skills.none;
		this.expMult = 1;
		this.weaponExpMult = 1;
		this.boots = 1f;
		this.frog = 1f;
		this.explosiveDamageMult = 1f;
		this.grenadeDamageMult = 1f;
		this.grenadeExplosionRadiusMult = 1f;
		this.plantMult = 1f;
		this.difuseMult = 1f;
		this.wtaskMult = 1f;
		this.achievementsMult = 1f;
		if (this.playerObject)
		{
			if (this.animationsThird)
			{
				this.animationsThird = null;
			}
			SingletoneForm<PoolManager>.Instance[this.playerObject.name].Despawn(this.playerObject.GetComponent<PoolItem>());
			this.playerObject = null;
			this.ammo = null;
			this.animations = null;
			this.controller = null;
		}
	}

	// Token: 0x06000CB2 RID: 3250 RVA: 0x00097F70 File Offset: 0x00096170
	protected virtual void CheckBinoculars()
	{
		if (this.playerInfo.playerClass == PlayerClass.scout && this.playerInfo.clanSkillUnlocked(Cl_Skills.cl_scout2) && this.Ammo != null)
		{
			this.Ammo.CanUseBinocular = true;
		}
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x00097FC0 File Offset: 0x000961C0
	public virtual void Initialize(int userID, int playerID)
	{
		Debug.Log("BaseNetPlayer.Initialize");
		this.playerInfo.userID = userID;
		this.playerInfo.playerID = playerID;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x00097FFC File Offset: 0x000961FC
	protected virtual void SpawnPlayerObject(Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
		this.RevivePlayer();
		this.UInput.Clear();
		this.UC.Clear();
		if (secondaryIndex == -1)
		{
			secondaryIndex = 0;
		}
		if (primaryIndex == -1)
		{
			primaryIndex = 127;
		}
		this.ammo = this.playerObject.GetComponent<BaseAmmunitions>();
		this.ammo.state.primaryIndex = primaryIndex;
		this.ammo.state.secondaryIndex = secondaryIndex;
		this.ammo.state.primaryMod = primaryMod;
		this.ammo.state.secondaryMod = secondaryMod;
		this.ammo.state.primaryState.repair_info = primary_repair_info;
		this.ammo.state.secondaryState.repair_info = secondary_repair_info;
		this.ammo.state.primaryState.Mods = primaryMods;
		this.ammo.state.secondaryState.Mods = secondaryMods;
		this.ammo.state.WeaponKit = weaponKit;
		this.ammo.Init(this);
		this.ammo.AfterInit();
		this.CheckBinoculars();
		Binocular.UnLocked = true;
		this.controller = this.playerObject.GetComponent<BaseMoveController>();
		if (this.controller == null)
		{
			Debug.LogWarning(string.Concat(new object[]
			{
				"BaseMoveContraller = ",
				this.controller,
				" playerObject = ",
				this.playerObject
			}));
			global::Console.print(string.Concat(new object[]
			{
				"BaseMoveContraller = ",
				this.controller,
				" playerObject = ",
				this.playerObject
			}), Color.magenta);
			if (!(this.playerObject != null))
			{
				throw new Exception("Ой беда, беда, огорчение!");
			}
			this.playerObject.AddComponent<BaseMoveController>();
			this.controller = this.playerObject.GetComponent<BaseMoveController>();
		}
		this.controller.Init(this, pos);
		this.controller.state.euler = euler;
		this.controller.Position = pos;
		this.controller.CallLateUpdate();
		this.LastExpMult = Skills.none;
		if (this.animationsThird)
		{
			this.animationsThird.CallLateUpdate();
		}
		if (!Peer.HardcoreMode)
		{
			for (int i = 0; i < this.playerInfo.skillsInfos.Length; i++)
			{
				if (this.playerInfo.skillsInfos[i])
				{
					this.ParseSkill((Skills)i);
				}
			}
			for (int j = 0; j < this.playerInfo.clanSkillsInfos.Length; j++)
			{
				if (this.playerInfo.clanSkillsInfos[j])
				{
					this.ParseClanSkill((Cl_Skills)j);
				}
			}
		}
		else
		{
			this.EnableHardCoreSkills();
		}
		this.ImmortalTimer.Start();
		if (this.Ammo.cPrimary != null && this.Ammo.cPrimary.IsAbakan)
		{
			this.Ammo.cPrimary.AbakanFirstShot = true;
		}
		if (this._isFirstSpawn)
		{
			this.StartSuspectCooldown = (float)CVars.StartSuspectCooldownTime;
			this._isFirstSpawn = false;
		}
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x0009832C File Offset: 0x0009652C
	public virtual void CallFixedUpdate()
	{
		if (this.IsDeadOrSpectactor)
		{
			this._shotTimer.Stop();
		}
		if (!this.IsAlive)
		{
			return;
		}
		if (this._hightlitedTime > Time.time)
		{
			this.playerInfo.buffs |= Buffs.hilighted;
		}
		else if (BIT.AND((int)this.playerInfo.buffs, 4096))
		{
			this.playerInfo.buffs ^= Buffs.hilighted;
		}
		if (!this.IsPlayerObject)
		{
			return;
		}
		this.UpdatePlacementInput();
		if (this.ammo)
		{
			this.UpdateInput();
			this.ammo.Tick(CVars.g_one_div_tickrate);
		}
		if (this.controller != null)
		{
			this.controller.Tick(CVars.g_one_div_tickrate);
		}
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00098414 File Offset: 0x00096614
	private void UpdateInput()
	{
		this.ammo.GrenadeTimer -= Time.deltaTime;
		if (this.ammo.state.equiped != WeaponEquipedState.Visor)
		{
			this.ItemInHandsToggle();
		}
		if (this.UInput.GetKeyUp(UKeyCode.clanSpecialAbility, true))
		{
			this.ammo.Binoculars();
			this.ammo.SwitchOptic();
		}
		if (this.UInput.GetKeyDown(UKeyCode.PrimaryWeapon, true))
		{
			this.ammo.ToggleTo(WeaponEquipedState.Primary);
		}
		if (this.UInput.GetKeyDown(UKeyCode.SecondaryWeapon, true))
		{
			this.ammo.ToggleTo(WeaponEquipedState.Secondary);
		}
		if (this.ammo.weaponEquiped)
		{
			this.UpdateEquipedWeapon();
		}
		else if (this.ammo.state.equiped == WeaponEquipedState.Marker)
		{
			if (this.UInput.GetKeyDown(UKeyCode.fire, true))
			{
				this.ammo.SupportAim();
			}
			if (this.UInput.GetKeyUp(UKeyCode.fire, true) && this.ammo.HoldTime > 0.25f)
			{
				this.ammo.SupportFire();
			}
		}
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x0009854C File Offset: 0x0009674C
	private void UpdateEquipedWeapon()
	{
		this.ChangeAim();
		if (this.UInput.GetKeyDown(UKeyCode.auto, true))
		{
			this.SwitchFireMode();
		}
		if (!this.UInput.GetKey(UKeyCode.radio, true))
		{
			this.UpdateFireInput();
		}
		this.UpdateReload();
	}

	// Token: 0x06000CB8 RID: 3256 RVA: 0x000985A0 File Offset: 0x000967A0
	private void UpdateFireInput()
	{
		if (this.ammo.CurrentWeapon.IsAbakan)
		{
			if (this.ammo.CurrentWeapon.state.singleShot || (this.ammo.CurrentWeapon.AbakanFirstShot && !this._shotTimer.IsStarted))
			{
				if (!this.UInput.GetKeyDown(UKeyCode.fire, true))
				{
					return;
				}
				this.ammo.Fire();
				if (this.ammo.CurrentWeapon.state.singleShot)
				{
					return;
				}
				this._shotTimer.Start();
				this.ammo.CurrentWeapon.AbakanFirstShot = false;
			}
			else if (this._shotTimer.IsStarted)
			{
				if (this.UInput.GetKey(UKeyCode.fire, true) && this._shotTimer.Time > 0.15f)
				{
					this.ammo.Fire();
				}
				else if (this.UInput.GetKeyUp(UKeyCode.fire, true))
				{
					this._shotTimer.Stop();
					this.ammo.CurrentWeapon.AbakanFirstShot = true;
				}
			}
		}
		else if (this.ammo.CurrentWeapon.state.singleShot)
		{
			if (this.UInput.GetKeyDown(UKeyCode.fire, true))
			{
				this.ammo.Fire();
			}
		}
		else if (this.UInput.GetKey(UKeyCode.fire, true))
		{
			this.ammo.Fire();
		}
	}

	// Token: 0x06000CB9 RID: 3257 RVA: 0x00098744 File Offset: 0x00096944
	private void UpdateReload()
	{
		if (this.UInput.GetKeyDown(UKeyCode.reload, true) && this.ammo.GrenadeTime > 1.3f)
		{
			if (Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
			{
				if (this is ClientNetPlayer)
				{
					ClientNetPlayer clientNetPlayer = this as ClientNetPlayer;
					if (clientNetPlayer)
					{
						clientNetPlayer.ToServer("ServerReload", new object[0]);
					}
					this.ammo.Reload();
				}
			}
			else
			{
				this._reloadStartTime = Time.time;
				this.ammo.Reload();
			}
		}
		this.SendAdditionalReload();
	}

	// Token: 0x06000CBA RID: 3258 RVA: 0x000987E8 File Offset: 0x000969E8
	private void SwitchFireMode()
	{
		if (this.ammo.CurrentWeapon.weaponNature == WeaponNature.shotgun && this.ammo.CurrentWeapon.canDuplet)
		{
			this.ammo.CurrentWeapon.duplet = !this.ammo.CurrentWeapon.duplet;
		}
		else if (this.ammo.CurrentWeapon.auto)
		{
			this.ammo.CurrentWeapon.state.singleShot = !this.ammo.CurrentWeapon.state.singleShot;
		}
	}

	// Token: 0x06000CBB RID: 3259 RVA: 0x00098890 File Offset: 0x00096A90
	private void ItemInHandsToggle()
	{
		if (this.UInput.GetKeyDown(UKeyCode.sonar, true))
		{
			this.ammo.Support();
		}
		if (this.UInput.GetKeyDown(UKeyCode.mortar, true))
		{
			this.ammo.MortarStrike();
		}
		if (this.UInput.GetKeyDown(UKeyCode.knife, true))
		{
			this.ammo.Knife();
		}
		if (this.UInput.GetKeyUp(UKeyCode.grenade, true) && this.ammo.GrenadeTimer <= 0f)
		{
			this.ammo.Grenade();
		}
		if (this.UInput.GetKey(UKeyCode.toggle, true))
		{
			this.ammo.ToggleTo((this.ammo.state.equiped != WeaponEquipedState.Secondary) ? WeaponEquipedState.Secondary : WeaponEquipedState.Primary);
		}
	}

	// Token: 0x06000CBC RID: 3260 RVA: 0x00098974 File Offset: 0x00096B74
	private void UpdatePlacementInput()
	{
		if (!this.UInput.GetKey(UKeyCode.interaction, true) || !this.BeaconUser || !this.InPlacementZone)
		{
			return;
		}
		this.UInput.SetKey(UKeyCode.up, KeyState.released);
		this.UInput.SetKey(UKeyCode.down, KeyState.released);
		this.UInput.SetKey(UKeyCode.left, KeyState.released);
		this.UInput.SetKey(UKeyCode.right, KeyState.released);
		this.UInput.SetKey(UKeyCode.sit, KeyState.released);
		this.UInput.SetKey(UKeyCode.walk, KeyState.released);
		this.UInput.SetKey(UKeyCode.knife, KeyState.released);
		this.UInput.SetKey(UKeyCode.PrimaryWeapon, KeyState.released);
		this.UInput.SetKey(UKeyCode.SecondaryWeapon, KeyState.released);
		this.UInput.SetKey(UKeyCode.sonar, KeyState.released);
		this.UInput.SetKey(UKeyCode.mortar, KeyState.released);
		this.UInput.SetKey(UKeyCode.clanSpecialAbility, KeyState.released);
		this.UInput.SetKey(UKeyCode.flashlight, KeyState.released);
		this.UInput.SetKey(UKeyCode.hideInterface, KeyState.released);
		this.UInput.SetKey(UKeyCode.teamChoose, KeyState.released);
		this.UInput.SetKey(UKeyCode.toggle, KeyState.released);
		this.UInput.SetKey(UKeyCode.reload, KeyState.released);
		this.UInput.SetKey(UKeyCode.auto, KeyState.released);
		this.UInput.SetKey(UKeyCode.fire, KeyState.released);
		this.UInput.SetKey(UKeyCode.aim, KeyState.released);
		this.UInput.SetKey(UKeyCode.jump, KeyState.released);
		this.UInput.SetKey(UKeyCode.grenade, KeyState.released);
	}

	// Token: 0x06000CBD RID: 3261 RVA: 0x00098B0C File Offset: 0x00096D0C
	public virtual void CallUpdate()
	{
	}

	// Token: 0x06000CBE RID: 3262 RVA: 0x00098B10 File Offset: 0x00096D10
	public virtual void CallLateUpdate()
	{
		this.StartSuspectCooldown -= Time.deltaTime;
		this.SuspectCooldown -= Time.deltaTime;
		if (!this.IsAlive)
		{
			return;
		}
		if (!this.IsPlayerObject)
		{
			return;
		}
		if (this.controller != null)
		{
			this.controller.CallLateUpdate();
		}
		if (this.animationsThird)
		{
			this.animationsThird.CallLateUpdate();
		}
		this.ammo.CallLateUpdate();
	}

	// Token: 0x06000CBF RID: 3263 RVA: 0x00098B9C File Offset: 0x00096D9C
	public virtual void OnSingleAttack(WeaponNature nature)
	{
	}

	// Token: 0x06000CC0 RID: 3264 RVA: 0x00098BA0 File Offset: 0x00096DA0
	protected virtual void OnEnable()
	{
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x00098BA4 File Offset: 0x00096DA4
	private void SendAdditionalReload()
	{
		if (this._reloadStartTime < 0f)
		{
			return;
		}
		if (this._reloadStartTime + 0.75f < Time.time)
		{
			this.ammo.Reload();
			this._reloadStartTime = -1f;
			this._sendOne = false;
			this._sendTwo = false;
			return;
		}
		if (this._reloadStartTime + 0.5f < Time.time && !this._sendTwo)
		{
			this.ammo.Reload();
			this._sendTwo = true;
			return;
		}
		if (this._reloadStartTime + 0.25f < Time.time && !this._sendOne)
		{
			this.ammo.Reload();
			this._sendOne = true;
		}
	}

	// Token: 0x04000DE3 RID: 3555
	private const int HightliteDeltaTime = 4;

	// Token: 0x04000DE4 RID: 3556
	protected Skills LastExpMult = Skills.none;

	// Token: 0x04000DE5 RID: 3557
	public Transform PlayerTransform;

	// Token: 0x04000DE6 RID: 3558
	private float _hightlitedTime;

	// Token: 0x04000DE7 RID: 3559
	protected GameObject playerObject;

	// Token: 0x04000DE8 RID: 3560
	protected BaseAmmunitions ammo;

	// Token: 0x04000DE9 RID: 3561
	protected BaseMoveController controller;

	// Token: 0x04000DEA RID: 3562
	protected Animations animations;

	// Token: 0x04000DEB RID: 3563
	protected AnimationsThird animationsThird;

	// Token: 0x04000DEC RID: 3564
	protected Camera mainCamera;

	// Token: 0x04000DED RID: 3565
	public PlayerInfo playerInfo = new PlayerInfo();

	// Token: 0x04000DEE RID: 3566
	public CWInput UInput = new CWInput();

	// Token: 0x04000DEF RID: 3567
	public BaseCmdCollector UC;

	// Token: 0x04000DF0 RID: 3568
	public bool isPlayer;

	// Token: 0x04000DF1 RID: 3569
	public UpdateType update = UpdateType.NoUpdate;

	// Token: 0x04000DF2 RID: 3570
	protected LoadingState loadingState;

	// Token: 0x04000DF3 RID: 3571
	protected WeaponEquipedState equiped = WeaponEquipedState.Secondary;

	// Token: 0x04000DF4 RID: 3572
	protected string PrimaryMods;

	// Token: 0x04000DF5 RID: 3573
	protected string SecondaryMods;

	// Token: 0x04000DF6 RID: 3574
	protected int WeaponKit;

	// Token: 0x04000DF7 RID: 3575
	protected int SecondaryIndex = 127;

	// Token: 0x04000DF8 RID: 3576
	protected int PrimaryIndex = 127;

	// Token: 0x04000DF9 RID: 3577
	protected bool SecondaryMod;

	// Token: 0x04000DFA RID: 3578
	protected bool PrimaryMod;

	// Token: 0x04000DFB RID: 3579
	protected int expMult = 1;

	// Token: 0x04000DFC RID: 3580
	protected int weaponExpMult = 1;

	// Token: 0x04000DFD RID: 3581
	public float boots = 1f;

	// Token: 0x04000DFE RID: 3582
	public float frog = 1f;

	// Token: 0x04000DFF RID: 3583
	public float explosiveDamageMult = 1f;

	// Token: 0x04000E00 RID: 3584
	public float grenadeDamageMult = 1f;

	// Token: 0x04000E01 RID: 3585
	public float grenadeExplosionRadiusMult = 1f;

	// Token: 0x04000E02 RID: 3586
	public Alpha rightClick = new Alpha();

	// Token: 0x04000E03 RID: 3587
	public float fTalkTime;

	// Token: 0x04000E04 RID: 3588
	public int iChatMessageCounter;

	// Token: 0x04000E05 RID: 3589
	public float plantMult = 1f;

	// Token: 0x04000E06 RID: 3590
	public float difuseMult = 1f;

	// Token: 0x04000E07 RID: 3591
	public float wtaskMult = 1f;

	// Token: 0x04000E08 RID: 3592
	public float achievementsMult = 1f;

	// Token: 0x04000E09 RID: 3593
	public float ImmortalityTime = 2f;

	// Token: 0x04000E0A RID: 3594
	public eTimer ImmortalTimer = new eTimer();

	// Token: 0x04000E0B RID: 3595
	public eTimer regen = new eTimer();

	// Token: 0x04000E0C RID: 3596
	public eTimer BleedingTimer = new eTimer();

	// Token: 0x04000E0D RID: 3597
	public bool hasAntiFlareLens;

	// Token: 0x04000E0E RID: 3598
	public int ClanBuffDistance;

	// Token: 0x04000E0F RID: 3599
	public int ClanBuffSelf;

	// Token: 0x04000E10 RID: 3600
	public int ClanBuffTeammate;

	// Token: 0x04000E11 RID: 3601
	public int ClanCompositeBuff;

	// Token: 0x04000E12 RID: 3602
	public int ClanCompositeBuffItem;

	// Token: 0x04000E13 RID: 3603
	private float cTime;

	// Token: 0x04000E14 RID: 3604
	private bool getTime;

	// Token: 0x04000E15 RID: 3605
	protected UserInfo userInfo;

	// Token: 0x04000E16 RID: 3606
	protected int PacketNum;

	// Token: 0x04000E17 RID: 3607
	private float _reviveTime;

	// Token: 0x04000E18 RID: 3608
	private readonly Timer _shotTimer = new Timer();

	// Token: 0x04000E19 RID: 3609
	public float StartSuspectCooldown = (float)CVars.StartSuspectCooldownTime;

	// Token: 0x04000E1A RID: 3610
	public float SuspectCooldown = (float)CVars.SuspectCooldownTime;

	// Token: 0x04000E1B RID: 3611
	private bool _isFirstSpawn = true;

	// Token: 0x04000E1C RID: 3612
	private Timer timer = new Timer();

	// Token: 0x04000E1D RID: 3613
	private float _reloadStartTime = -1f;

	// Token: 0x04000E1E RID: 3614
	private bool _sendOne;

	// Token: 0x04000E1F RID: 3615
	private bool _sendTwo;
}
