using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x0200019D RID: 413
[AddComponentMenu("Scripts/Game/BaseAmmunitions")]
internal class BaseAmmunitions : PoolableBehaviour, cwNetworkSerializable
{
	// Token: 0x06000BB7 RID: 2999 RVA: 0x000921C4 File Offset: 0x000903C4
	public override void OnPoolDespawn()
	{
		if (this.cPrimary)
		{
			PoolManager.Despawn(this.cPrimary.gameObject);
			this.cPrimary = null;
		}
		if (this.cSecondary)
		{
			PoolManager.Despawn(this.cSecondary.gameObject);
			this.cSecondary = null;
		}
		this.CanUseBinocular = false;
		this.useBinocular = false;
		this.state.Clear();
		this.G.Clear();
		this.player = null;
		this.aimGNAME = new GraphicValue();
		this.aimWeapGNAME = new GraphicValue();
		this.gunHolderBone = null;
		this.supportTime = -1f;
		this.attackerDelay = 0f;
		this.throwPowerMult = 1f;
		this.ReloadSpeedMult = 1f;
		this.AccuracyMult = 1f;
		this.ApAmmo = 0f;
		this.aimBonusMult = 1f;
		this.KnifeDelay = 0.9f;
		this.LOD = false;
		base.OnPoolDespawn();
		if (base.GetType() == typeof(ClientAmmunitions))
		{
			try
			{
				Binocular.ForceSwitchOff();
				Thermal.ForceSwitchOff();
			}
			catch (Exception arg)
			{
				if (CVars.n_httpDebug)
				{
					Debug.Log("Oleg ATATA!" + arg);
				}
			}
		}
	}

	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0009232C File Offset: 0x0009052C
	public float ThrowPower
	{
		get
		{
			return Mathf.Min(Time.realtimeSinceStartup - this.supportTime, 2f) / 2f + 0.3f;
		}
	}

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0009235C File Offset: 0x0009055C
	public float HoldTime
	{
		get
		{
			return Time.realtimeSinceStartup - this.supportTime;
		}
	}

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0009236C File Offset: 0x0009056C
	public float GrenadeTime
	{
		get
		{
			return Time.realtimeSinceStartup - this.grenadeTime;
		}
	}

	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0009237C File Offset: 0x0009057C
	public bool NotLOD
	{
		get
		{
			return !this.LOD;
		}
	}

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06000BBC RID: 3004 RVA: 0x00092388 File Offset: 0x00090588
	public BaseWeapon CurrentWeapon
	{
		get
		{
			if (this.state.equiped == WeaponEquipedState.Primary)
			{
				return this.cPrimary;
			}
			if (this.state.equiped == WeaponEquipedState.Secondary)
			{
				return this.cSecondary;
			}
			return this.cSecondary;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06000BBD RID: 3005 RVA: 0x000923CC File Offset: 0x000905CC
	public float SpeedReducer
	{
		get
		{
			float num = 1f;
			if (this.state.isAim || (this.player is ClientNetPlayer && this.player.AimSynchFromServer))
			{
				num = 0.5f;
			}
			if (this.state.isAim && this.state.equiped == WeaponEquipedState.Primary)
			{
				num *= this.aimBonusMult;
			}
			if (this.CurrentWeapon == null || this.CurrentWeapon.weaponNature == WeaponNature.shotgun)
			{
				return 1f;
			}
			return this.CurrentWeapon.SpeedReducer * num;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000BBE RID: 3006 RVA: 0x00092478 File Offset: 0x00090678
	public bool weaponEquiped
	{
		get
		{
			return this.state.equiped == WeaponEquipedState.Primary || this.state.equiped == WeaponEquipedState.Secondary;
		}
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x000924A8 File Offset: 0x000906A8
	protected virtual void TakeWeapon()
	{
		if (this.cSecondary)
		{
			this.state.equiped = WeaponEquipedState.Secondary;
			this.cSecondary.transform.localPosition = Vector3.zero;
			this.cSecondary.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		}
		if (this.cPrimary)
		{
			this.state.equiped = WeaponEquipedState.Primary;
			this.cPrimary.transform.localPosition = Vector3.zero;
			this.cPrimary.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		}
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x00092560 File Offset: 0x00090760
	public virtual void Init(BaseNetPlayer player)
	{
		this.player = player;
		this.G.Init(this);
		this.PrepareWeapon();
		this.TakeWeapon();
		this._areTransformsSet = false;
		this._isMoveDeltaSet = false;
		this._isStartAim = false;
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x000925A4 File Offset: 0x000907A4
	public void AfterInit()
	{
		this.OutIdle();
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x000925AC File Offset: 0x000907AC
	public void HideWeapon()
	{
		this.IdleOut();
	}

	// Token: 0x06000BC3 RID: 3011 RVA: 0x000925B4 File Offset: 0x000907B4
	public void BreakReload()
	{
		this.G.CancelInvoke("AfterReload");
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x000925C8 File Offset: 0x000907C8
	public void Tick(float dt)
	{
		if (this.cPrimary)
		{
			this.cPrimary.Tick(dt);
			this.state.primaryState.Clone(this.cPrimary.state);
		}
		if (this.cSecondary)
		{
			this.cSecondary.Tick(dt);
			this.state.secondaryState.Clone(this.cSecondary.state);
		}
		this.G.Tick(dt);
		this.state.G.Clone(this.G.state);
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x0009266C File Offset: 0x0009086C
	public virtual void CallLateUpdate()
	{
		if (this.cPrimary != null)
		{
			this.cPrimary.CallLateUpdate();
		}
		if (this.cSecondary != null)
		{
			this.cSecondary.CallLateUpdate();
		}
		if (!this.tmpWeapon || this.tmpWeapon == this.CurrentWeapon)
		{
			return;
		}
		this.tmpWeapon.CancelPostReload();
		this.tmpWeapon = null;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x000926EC File Offset: 0x000908EC
	public virtual void Recover(AmmoState i)
	{
		this.state.Clone(i);
		this.G.state.Clone(i.G);
		if (this.cPrimary)
		{
			this.cPrimary.state.Clone(i.primaryState);
			this.cPrimary.Recover();
		}
		if (this.cSecondary)
		{
			this.cSecondary.state.Clone(i.secondaryState);
			this.cSecondary.Recover();
		}
		Debug.Log("Recover");
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00092788 File Offset: 0x00090988
	[Obfuscation(Exclude = true)]
	public void Fire()
	{
		if (this.useBinocular)
		{
			return;
		}
		if (!this.weaponEquiped)
		{
			return;
		}
		if (this.player != null && !this.player.IsAlive)
		{
			return;
		}
		this.CurrentWeapon.AdvanceSound();
		if (this.AlwaysGroup)
		{
			return;
		}
		if (Globals.I.ReloadScheme != ReloadScheme.OldSynchronous && this.state.BlockFireByServer)
		{
			return;
		}
		if (this.player is ClientNetPlayer && (this.player.AimSynchFromServer ^ this.IsAim) && Globals.I.ReloadScheme == ReloadScheme.Synchronous)
		{
			return;
		}
		if (this.player is EntityNetPlayer)
		{
			ClientWeapon clientWeapon = (ClientWeapon)this.CurrentWeapon;
			if (!clientWeapon.IsWeaponInited)
			{
				return;
			}
		}
		if (this.G.IsInvoking("InsertClip"))
		{
			this.G.CancelInvoke("InsertClip");
			this.OnFirePlayReloadEnd();
			this.CurrentWeapon.ReloadEnd();
			this.G.Invoke("AfterReloadEnd", (!this.CurrentWeapon.UseShotGunReload) ? 1f : 2f, false);
			return;
		}
		if (this.CurrentWeapon.state.clips <= 0)
		{
			if (Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
			{
				if (this is ClientAmmunitions && !this._reloadStarted)
				{
					ClientNetPlayer clientNetPlayer = this.player as ClientNetPlayer;
					if (clientNetPlayer)
					{
						clientNetPlayer.ToServer("ServerReload", new object[0]);
						this.Reload();
					}
				}
			}
			else
			{
				this.Reload();
			}
			if (this.CurrentWeapon.UseShotGunReload)
			{
				this.CurrentWeapon.state.needReload = false;
			}
			return;
		}
		this._reloadStarted = false;
		bool flag = this.CurrentWeapon.state.clips >= 2;
		FireType fireType = this.CurrentWeapon.Fire();
		if (fireType == FireType.nofire)
		{
			return;
		}
		if (fireType == FireType.reload)
		{
			this.OnFireReload();
			if (this.CurrentWeapon.UseShotGunReload && this.CurrentWeapon.state.clips <= 0)
			{
				this.CurrentWeapon.state.needReload = false;
			}
			return;
		}
		this.OnFire();
		if (this.CurrentWeapon.IsAbakan && (this.CurrentWeapon.AbakanFirstShot || (this.CurrentWeapon.state.singleShot && this.CurrentWeapon.state.clips > 1)))
		{
			for (int i = 0; i < 2; i++)
			{
				this.SingleAttack(this.CurrentWeapon.weaponNature, this.CurrentWeapon);
			}
		}
		else if (!this.CurrentWeapon.Slug)
		{
			for (int j = 0; j < this.CurrentWeapon.caseshot; j++)
			{
				this.SingleAttack(this.CurrentWeapon.weaponNature, this.CurrentWeapon);
			}
		}
		if (this.CurrentWeapon.weaponNature == WeaponNature.shotgun && this.CurrentWeapon.duplet && flag)
		{
			for (int k = 0; k < this.CurrentWeapon.caseshot; k++)
			{
				this.SingleAttack(this.CurrentWeapon.weaponNature, this.CurrentWeapon);
			}
		}
		if (this.CurrentWeapon.weaponNature == WeaponNature.shotgun && this.CurrentWeapon.Slug)
		{
			this.SingleAttack(this.CurrentWeapon.weaponNature, this.CurrentWeapon);
		}
		this.CurrentWeapon.AddAccuracy();
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00092B50 File Offset: 0x00090D50
	protected virtual void SingleAttack(WeaponNature nature, BaseWeapon weapon = null)
	{
		this.player.Ammo.state.randomSeed += 100;
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x00092B70 File Offset: 0x00090D70
	public bool IsAim
	{
		get
		{
			return this.state.isAim || this._isStartAim;
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00092B8C File Offset: 0x00090D8C
	public float AimSpeed
	{
		get
		{
			float aimPos = this.AimPos;
			return aimPos * 0.1f + (1f - aimPos) * 1f;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x06000BCB RID: 3019 RVA: 0x00092BB8 File Offset: 0x00090DB8
	public float AimPos
	{
		get
		{
			if (this.G.IsInvoking("AfterAimIdle"))
			{
				return 1f - this.aimGNAME.Get();
			}
			if (this.G.IsInvoking("AfterIdleAim"))
			{
				return this.aimGNAME.Get();
			}
			return (float)((!this.state.isAim) ? 0 : 1);
		}
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00092C28 File Offset: 0x00090E28
	[Obfuscation(Exclude = true)]
	protected void AfterAimIdle()
	{
		if (this.cPrimary)
		{
			this.cPrimary.PlayAfterAimIdle();
		}
		if (this.cSecondary)
		{
			this.cSecondary.PlayAfterAimIdle();
		}
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00092C6C File Offset: 0x00090E6C
	[Obfuscation(Exclude = true)]
	protected void AfterIdleAim()
	{
		this.state.isAim = true;
		if (this.cPrimary)
		{
			this.cPrimary.PlayAfterIdleAim();
		}
		if (this.cSecondary)
		{
			this.cSecondary.PlayAfterIdleAim();
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00092CBC File Offset: 0x00090EBC
	public void AutoChangeAimMode(bool isAim)
	{
		if (this.AlwaysGroup || this.G.IsInvoking("InsertClip"))
		{
			return;
		}
		Vector2[] v = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(this.CurrentWeapon.IdleAimTime * 0.5f, 1f),
			new Vector2(this.CurrentWeapon.IdleAimTime * 0.9f, 1f),
			new Vector2(this.CurrentWeapon.IdleAimTime, 1f),
			new Vector2(this.CurrentWeapon.IdleAimTime * 100f, 1f)
		};
		this.aimGNAME.Init(v);
		Vector2[] v2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(this.CurrentWeapon.IdleAimTime * 0.3f, 0f),
			new Vector2(this.CurrentWeapon.IdleAimTime, 1f),
			new Vector2(this.CurrentWeapon.IdleAimTime * 100f, 1f)
		};
		this.aimWeapGNAME.Init(v2);
		if (this.CurrentWeapon.IsModable)
		{
			if (isAim)
			{
				if (this.state.isAim)
				{
					return;
				}
				this.step = 0f;
				if (!this._areTransformsSet && this.player is ClientNetPlayer)
				{
					this.SetTransforms();
				}
				if (!CVars.IsAimTest && !this._isMoveDeltaSet)
				{
					this.SetMoveDelta();
				}
				if (this.opticCamera != null)
				{
					this.opticCamera.enabled = true;
				}
				this.G.Invoke("AfterIdleAim", this.CurrentWeapon.IdleAimTime, false);
			}
			else
			{
				if (!this.state.isAim)
				{
					return;
				}
				this.step = 0f;
				if (this.opticCamera != null)
				{
					this.opticCamera.enabled = false;
				}
				this.G.Invoke("AfterAimIdle", this.CurrentWeapon.AimIdleTime, false);
				this.state.isAim = false;
			}
		}
		else if (isAim)
		{
			if (this.state.isAim)
			{
				return;
			}
			this.CurrentWeapon.PlayIdleAim();
			this.G.Invoke("AfterIdleAim", this.CurrentWeapon.IdleAimTime, false);
			this.OnPlayIdleAim();
		}
		else
		{
			if (!this.state.isAim)
			{
				return;
			}
			this.CurrentWeapon.PlayAimIdle();
			this.G.Invoke("AfterAimIdle", this.CurrentWeapon.AimIdleTime, false);
			this.OnPlayAimIdle();
			this.state.isAim = false;
		}
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00092FF0 File Offset: 0x000911F0
	public void SetTransforms()
	{
		this.modAimPoint = null;
		this.weaponAimPoint = null;
		this.playerCamera = this.player.MainCamera.transform;
		this.cameraRotation = this.playerCamera.localRotation;
		this.weapon = Utility.FindHierarchy(base.transform, this.CurrentWeapon.weaponPrefab.name + "(Clone)");
		Transform transform = Utility.FindHierarchy(this.weapon, "Weapons_Root");
		int type = (int)this.CurrentWeapon.type;
		Dictionary<ModType, int> dictionary = Utility.StringToMods((!this.CurrentWeapon.IsPrimary) ? this.state.secondaryState.Mods : this.state.primaryState.Mods);
		this.weaponAimPoint = Utility.FindHierarchy(this.weapon, "aim_camera");
		int id;
		if (dictionary != null && dictionary.TryGetValue(ModType.optic, out id))
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(id);
			if (!modById.IsBasic)
			{
				Transform current = transform.FindChild("optic");
				if (!string.IsNullOrEmpty(modById.WeaponSpecificInfo[type].Device))
				{
					current = Utility.FindHierarchy(transform, modById.WeaponSpecificInfo[type].Device);
				}
				this.modAimPoint = Utility.FindHierarchy(current, "optic_camera");
				if (this.modAimPoint == null)
				{
					this.modAimPoint = Utility.FindHierarchy(current, "aim_camera");
				}
				else
				{
					this.opticCamera = this.modAimPoint.gameObject.GetComponent<Camera>();
					this.opticCamera.fieldOfView = modById.OpticFov.Val;
				}
			}
		}
		this._areTransformsSet = true;
	}

	// Token: 0x06000BD0 RID: 3024 RVA: 0x000931AC File Offset: 0x000913AC
	private void Update()
	{
		if (!this._areTransformsSet)
		{
			return;
		}
		if (!this.CurrentWeapon.IsModable)
		{
			return;
		}
		this.step += this.speed * Time.deltaTime;
		this.playerCamera.localPosition = ((!this._isStartAim) ? Vector3.Lerp(this.MoveDelta, Vector3.zero, this.step) : Vector3.Lerp(Vector3.zero, this.MoveDelta, this.step));
	}

	// Token: 0x06000BD1 RID: 3025 RVA: 0x00093238 File Offset: 0x00091438
	private void SetMoveDelta()
	{
		int type = (int)this.CurrentWeapon.type;
		Dictionary<ModType, int> dictionary = Utility.StringToMods((!this.CurrentWeapon.IsPrimary) ? this.state.secondaryState.Mods : this.state.primaryState.Mods);
		int id;
		if (dictionary != null && dictionary.TryGetValue(ModType.optic, out id))
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(id);
			this.MoveDelta = modById.WeaponSpecificInfo[type].AimDelta;
			Debug.Log(this.FormatMoveDelta("moveDelta set to"));
		}
		this._isMoveDeltaSet = true;
	}

	// Token: 0x06000BD2 RID: 3026 RVA: 0x000932DC File Offset: 0x000914DC
	private string FormatMoveDelta(string prefix)
	{
		return string.Concat(new string[]
		{
			prefix,
			"\t\tx: ",
			this.MoveDelta.x.ToString("n4"),
			"\t\ty: ",
			this.MoveDelta.y.ToString("n4"),
			"\t\tz: ",
			this.MoveDelta.z.ToString("n4")
		});
	}

	// Token: 0x06000BD3 RID: 3027 RVA: 0x00093358 File Offset: 0x00091558
	public void ChangeAimMode(bool changeByInput = false)
	{
		if (this.ForcedChangeAimMode && changeByInput && Main.UserInfo.settings.binds.holdAim)
		{
			return;
		}
		if (!changeByInput && Main.UserInfo.settings.binds.holdAim)
		{
			this.ForcedChangeAimMode = true;
		}
		if (this.AlwaysGroup || this.G.IsInvoking("InsertClip"))
		{
			return;
		}
		Vector2[] v = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(this.CurrentWeapon.AimIdleTime * 0.5f, 1f),
			new Vector2(this.CurrentWeapon.AimIdleTime * 0.9f, 1f),
			new Vector2(this.CurrentWeapon.AimIdleTime, 1f),
			new Vector2(this.CurrentWeapon.AimIdleTime * 100f, 1f)
		};
		this.aimGNAME.Init(v);
		Vector2[] v2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(this.CurrentWeapon.AimIdleTime * 0.3f, 0f),
			new Vector2(this.CurrentWeapon.AimIdleTime, 1f),
			new Vector2(this.CurrentWeapon.AimIdleTime * 100f, 1f)
		};
		this.aimWeapGNAME.Init(v2);
		if (this.CurrentWeapon.IsModable)
		{
			this.step = 0f;
			if (this.state.isAim)
			{
				if (this.opticCamera != null)
				{
					this.opticCamera.enabled = false;
				}
				this.G.Invoke("AfterAimIdle", this.CurrentWeapon.AimIdleTime, false);
				this.deltaCounter = 0;
				this.state.isAim = false;
				this._isStartAim = false;
				this.OnPlayAimIdle();
			}
			else
			{
				if (!this._areTransformsSet && this.player is ClientNetPlayer)
				{
					this.SetTransforms();
				}
				if (!CVars.IsAimTest && !this._isMoveDeltaSet)
				{
					this.SetMoveDelta();
				}
				if (this.opticCamera != null)
				{
					this.opticCamera.enabled = true;
				}
				this.G.Invoke("AfterIdleAim", this.CurrentWeapon.IdleAimTime, false);
				this.OnPlayIdleAim();
				this._isStartAim = true;
			}
		}
		else if (this.state.isAim)
		{
			this.CurrentWeapon.PlayAimIdle();
			this.G.Invoke("AfterAimIdle", this.CurrentWeapon.AimIdleTime, false);
			this.OnPlayAimIdle();
			this.state.isAim = false;
		}
		else
		{
			this.aimWeapGNAME.Init(v2);
			this.CurrentWeapon.PlayIdleAim();
			this.G.Invoke("AfterIdleAim", this.CurrentWeapon.IdleAimTime, false);
			this.OnPlayIdleAim();
		}
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x000936D0 File Offset: 0x000918D0
	protected bool AlwaysGroup
	{
		get
		{
			return this.G.IsInvoking("AfterAimIdle") || this.G.IsInvoking("AfterIdleAim") || this.G.IsInvoking("OutIdle") || this.G.IsInvoking("IdleOut") || this.G.IsInvoking("AfterIdleOut") || this.G.IsInvoking("AfterOutIdle") || this.G.IsInvoking("Toggle") || this.G.IsInvoking("AfterToggleIdleOut") || this.G.IsInvoking("Reload") || this.G.IsInvoking("AfterReload") || this.G.IsInvoking("AfterReloadStart") || this.G.IsInvoking("AfterReloadEnd") || this.G.IsInvoking("AfterGrenade") || this.G.IsInvoking("AfterKnife") || this.G.IsInvoking("Support") || this.G.IsInvoking("MortarStrike") || this.G.IsInvoking("SupportOutIdle") || this.G.IsInvoking("AfterSupport") || (this.weaponEquiped && this.CurrentWeapon != null && this.CurrentWeapon.AlwaysGroup);
		}
	}

	// Token: 0x17000167 RID: 359
	// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00093884 File Offset: 0x00091A84
	protected bool BreakableGroup
	{
		get
		{
			return this.G.IsInvoking("AfterAimIdle") || this.G.IsInvoking("AfterIdleAim") || this.G.IsInvoking("OutIdle") || this.G.IsInvoking("IdleOut") || this.G.IsInvoking("AfterIdleOut") || this.G.IsInvoking("AfterOutIdle") || this.G.IsInvoking("Toggle") || this.G.IsInvoking("AfterToggleIdleOut") || this.G.IsInvoking("AfterGrenade") || this.G.IsInvoking("AfterKnife") || this.G.IsInvoking("Support") || this.G.IsInvoking("MortarStrike") || this.G.IsInvoking("SupportOutIdle") || this.G.IsInvoking("AfterSupport");
		}
	}

	// Token: 0x06000BD6 RID: 3030 RVA: 0x000939B8 File Offset: 0x00091BB8
	[Obfuscation(Exclude = true)]
	protected virtual void IdleOut()
	{
		if (this.weaponEquiped)
		{
			this.G.Invoke("AfterIdleOut", (!(this.CurrentWeapon != null)) ? 1f : this.CurrentWeapon.IdleOut, false);
		}
		else
		{
			this.G.Invoke("AfterIdleOut", CVars.pl_marker_idle_to_out, false);
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00093A24 File Offset: 0x00091C24
	[Obfuscation(Exclude = true)]
	protected virtual void AfterIdleOut()
	{
		this.UpdateFireInterface();
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00093A2C File Offset: 0x00091C2C
	[Obfuscation(Exclude = true)]
	protected virtual void OutIdle()
	{
		this.G.Invoke("AfterOutIdle", (!this.weaponEquiped) ? CVars.pl_marker_out_to_idle : this.CurrentWeapon.OutIdle, false);
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00093A6C File Offset: 0x00091C6C
	[Obfuscation(Exclude = true)]
	protected virtual void AfterOutIdle()
	{
		this.UpdateFireInterface();
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00093A74 File Offset: 0x00091C74
	[Obfuscation(Exclude = true)]
	public virtual void Toggle()
	{
		if (this.BreakableGroup)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.useBinocular)
		{
			return;
		}
		if (this.G.IsInvoking("Toggle"))
		{
			return;
		}
		Debug.Log(string.Concat(new object[]
		{
			"BaseAmmo.Toggle on ",
			this.player,
			" ",
			this.player.ID
		}));
		if (!this.weaponEquiped)
		{
			this.state.equiped = this.state.preSupportEquiped;
			if (this.state.equiped == WeaponEquipedState.none)
			{
				this.state.equiped = WeaponEquipedState.Secondary;
			}
			this.lastSupport = ArmstreakEnum.none;
			this.Cancel();
			this.OutIdle();
			return;
		}
		if (this.state.equiped == WeaponEquipedState.Secondary && !this.cPrimary)
		{
			return;
		}
		if (this.state.equiped == WeaponEquipedState.Primary && !this.cSecondary)
		{
			return;
		}
		this.Cancel();
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			this.G.Invoke("Toggle", this.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		this.IdleOut();
		this.G.Invoke("AfterToggleIdleOut", this.CurrentWeapon.IdleOut, false);
		this._isMoveDeltaSet = false;
		this._areTransformsSet = false;
	}

	// Token: 0x06000BDB RID: 3035 RVA: 0x00093C1C File Offset: 0x00091E1C
	public void ToggleTo(WeaponEquipedState newState)
	{
		if (this.CurrentWeapon.isBolt && this.IsAim && this.CurrentWeapon.state.needReload)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			return;
		}
		if (this.BreakableGroup)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.useBinocular)
		{
			return;
		}
		if (this.G.IsInvoking("Toggle"))
		{
			return;
		}
		if (this.state.equiped == newState)
		{
			return;
		}
		if (newState == WeaponEquipedState.Secondary && !this.cSecondary)
		{
			return;
		}
		if (newState == WeaponEquipedState.Primary && !this.cPrimary)
		{
			return;
		}
		this.Cancel();
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			this.G.Invoke("Toggle", this.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		this.IdleOut();
		this.G.Invoke("AfterToggleIdleOut", this.CurrentWeapon.IdleOut, false);
		this._cachedEquipedState = newState;
		this.state.equiped = WeaponEquipedState.none;
		this._isMoveDeltaSet = false;
		this._areTransformsSet = false;
	}

	// Token: 0x06000BDC RID: 3036 RVA: 0x00093D9C File Offset: 0x00091F9C
	[Obfuscation(Exclude = true)]
	protected virtual void AfterToggleIdleOut()
	{
		this.state.equiped = this._cachedEquipedState;
		this.OutIdle();
	}

	// Token: 0x06000BDD RID: 3037 RVA: 0x00093DB8 File Offset: 0x00091FB8
	[Obfuscation(Exclude = true)]
	public virtual void Binoculars()
	{
	}

	// Token: 0x06000BDE RID: 3038 RVA: 0x00093DBC File Offset: 0x00091FBC
	public virtual void SwitchOptic()
	{
	}

	// Token: 0x06000BDF RID: 3039 RVA: 0x00093DC0 File Offset: 0x00091FC0
	[Obfuscation(Exclude = true)]
	public virtual void AdditionalMagazineSound()
	{
	}

	// Token: 0x06000BE0 RID: 3040 RVA: 0x00093DC4 File Offset: 0x00091FC4
	[Obfuscation(Exclude = true)]
	protected virtual void AfterBinocularsIdleOut()
	{
		this.state.preSupportEquiped = this.state.equiped;
		this.state.equiped = WeaponEquipedState.Visor;
		this.useBinocular = true;
		this.OutIdle();
	}

	// Token: 0x06000BE1 RID: 3041 RVA: 0x00093DF8 File Offset: 0x00091FF8
	[Obfuscation(Exclude = true)]
	public void ServerReload()
	{
		if (!this.weaponEquiped)
		{
			return;
		}
		if (this.CurrentWeapon.Empty || this.CurrentWeapon.state.clips == this.CurrentWeapon.magSize)
		{
			return;
		}
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			return;
		}
		if (this.CurrentWeapon.pompovik)
		{
			if (this.CurrentWeapon.PompovikReload())
			{
				this.OnPlayPompovikReload();
				this.G.Invoke("AfterReloadStart", 1f, false);
			}
		}
		else if (this.CurrentWeapon.UseShotGunReload)
		{
			if (this.CurrentWeapon.PompovikReload())
			{
				this.OnPlayPompovikReload();
				this.G.Invoke("AfterReloadStart", 2.3f, false);
			}
		}
		else if (this.CurrentWeapon.ServerReload())
		{
			this.OnPlayReload();
			this.G.Invoke("AfterReload", this.CurrentWeapon.ReloadTime, false);
			this.tmpWeapon = this.CurrentWeapon;
		}
		if (Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
		{
			this.state.ServerReloadStart = true;
		}
	}

	// Token: 0x06000BE2 RID: 3042 RVA: 0x00093F50 File Offset: 0x00092150
	[Obfuscation(Exclude = true)]
	public void Reload()
	{
		if (this is ServerAmmunitions && Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
		{
			this.UpdateAllInterface();
		}
		if (this is EntityAmmunitions)
		{
			if (this.CurrentWeapon.G.IsInvoking("PostReload"))
			{
				return;
			}
		}
		else if (this.AlwaysGroup)
		{
			return;
		}
		if (!this.weaponEquiped)
		{
			return;
		}
		if (this.CurrentWeapon.Empty || this.CurrentWeapon.state.clips == this.CurrentWeapon.magSize)
		{
			return;
		}
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			this.G.Invoke("Reload", this.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		if (this is ClientAmmunitions && Globals.I.ReloadScheme == ReloadScheme.Asynchronous && !this.state.ServerReloadStart)
		{
			return;
		}
		this.Cancel();
		if (this.CurrentWeapon.pompovik)
		{
			if (this.CurrentWeapon.PompovikReload())
			{
				this.OnPlayPompovikReload();
				this.G.Invoke("AfterReloadStart", 1f, false);
			}
		}
		else if (this.CurrentWeapon.UseShotGunReload)
		{
			if (this.CurrentWeapon.PompovikReload())
			{
				this.OnPlayPompovikReload();
				this.G.Invoke("AfterReloadStart", 2.3f, false);
			}
		}
		else if (this.CurrentWeapon.Reload())
		{
			this.OnPlayReload();
			this.G.Invoke("AfterReload", this.CurrentWeapon.ReloadTime, false);
			this.tmpWeapon = this.CurrentWeapon;
		}
	}

	// Token: 0x06000BE3 RID: 3043 RVA: 0x00094138 File Offset: 0x00092338
	[Obfuscation(Exclude = true)]
	protected void AfterReload()
	{
		if (this is ServerAmmunitions && Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
		{
			this.state.ServerReloadStart = false;
		}
		this.OnPlayAfterReload();
	}

	// Token: 0x06000BE4 RID: 3044 RVA: 0x00094174 File Offset: 0x00092374
	[Obfuscation(Exclude = true)]
	protected void AfterReloadStart()
	{
		if (this is ServerAmmunitions && Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
		{
			this.state.ServerReloadStart = false;
		}
		this.CurrentWeapon.ReloadStart();
		if (this.CurrentWeapon.CanInsertClip(this.CurrentWeapon.ReloadTime / 4f))
		{
			this.OnPlayInsertClip();
			this.G.Invoke("InsertClip", this.CurrentWeapon.ReloadTime / 4f, false);
		}
		else
		{
			this.OnPlayReloadEnd();
			this.CurrentWeapon.ReloadEnd();
			this.G.Invoke("AfterReloadEnd", (!this.CurrentWeapon.UseShotGunReload) ? 1f : 2f, false);
		}
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00094244 File Offset: 0x00092444
	[Obfuscation(Exclude = true)]
	protected void InsertClip()
	{
		this.CurrentWeapon.InsertClip();
		if (this.CurrentWeapon.CanInsertClip(this.CurrentWeapon.ReloadTime / 4f))
		{
			this.OnPlayPompovikInsertClip();
			this.G.Invoke("InsertClip", this.CurrentWeapon.ReloadTime / 4f, false);
		}
		else
		{
			Debug.Log(this + " ReloadEnd");
			this.OnPlayPompovikReloadEnd();
			this.CurrentWeapon.ReloadEnd();
			this.G.Invoke("AfterReloadEnd", 1f, false);
		}
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x000942E4 File Offset: 0x000924E4
	[Obfuscation(Exclude = true)]
	protected void AfterReloadEnd()
	{
		if (this is ServerAmmunitions && Globals.I.ReloadScheme != ReloadScheme.OldSynchronous)
		{
			this.state.ServerReloadStart = false;
		}
		this.OnPlayAfterReload();
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00094320 File Offset: 0x00092520
	[Obfuscation(Exclude = true)]
	public virtual void Grenade()
	{
		if (this.BreakableGroup || this.state.supportReady || this.useBinocular || this.G.IsInvoking("AfterAimIdle") || this.state.grenadeCount == 0)
		{
			return;
		}
		this.GrenadeTimer = CVars.g_grenadeTimeout;
		this.state.grenadeCount--;
		this.state.isAim = false;
		this._isStartAim = false;
		this.Cancel();
		this.G.Invoke("AfterGrenade", 0.6f, false);
		this.OnGrenade();
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x000943CC File Offset: 0x000925CC
	[Obfuscation(Exclude = true)]
	protected virtual void AfterGrenade()
	{
		this.OutIdle();
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x000943D4 File Offset: 0x000925D4
	[Obfuscation(Exclude = true)]
	protected virtual void GrenadeLaunch()
	{
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000943D8 File Offset: 0x000925D8
	[Obfuscation(Exclude = true)]
	public void KnifeAttack()
	{
		this.SingleAttack(WeaponNature.knife, null);
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x000943E4 File Offset: 0x000925E4
	public virtual void Knife()
	{
		if (this.BreakableGroup || this.state.supportReady || this.useBinocular)
		{
			return;
		}
		this.Cancel();
		this.state.isAim = false;
		this._isStartAim = false;
		this.G.Invoke("KnifeAttack", this.KnifeDelay / 3f, false);
		this.G.Invoke("AfterKnife", this.KnifeDelay, false);
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x00094468 File Offset: 0x00092668
	[Obfuscation(Exclude = true)]
	protected virtual void AfterKnife()
	{
		this.OutIdle();
	}

	// Token: 0x06000BED RID: 3053 RVA: 0x00094470 File Offset: 0x00092670
	[Obfuscation(Exclude = true)]
	public void MortarStrike()
	{
		if (this.BreakableGroup)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.state.equiped == WeaponEquipedState.Marker)
		{
			return;
		}
		if (!this.player.playerInfo.hasMortar)
		{
			return;
		}
		this.state.supportReady = false;
		this.Cancel();
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			this.G.Invoke("MortarStrike", this.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		this.lastSupport = ArmstreakEnum.mortar;
		this.IdleOut();
		this.G.Invoke("SupportOutIdle", CVars.pl_marker_out_to_idle, false);
	}

	// Token: 0x06000BEE RID: 3054 RVA: 0x0009454C File Offset: 0x0009274C
	[Obfuscation(Exclude = true)]
	public void Support()
	{
		if (this.BreakableGroup)
		{
			return;
		}
		if (this.state.supportReady)
		{
			return;
		}
		if (this.state.equiped == WeaponEquipedState.Marker)
		{
			return;
		}
		if (!this.player.playerInfo.hasSonar)
		{
			return;
		}
		this.state.supportReady = false;
		this.Cancel();
		if (this.state.isAim)
		{
			this.player.rightClick.Hide(0.5f, 0f);
			this.ChangeAimMode(false);
			this.G.Invoke("Support", this.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		this.lastSupport = ArmstreakEnum.sonar;
		this.IdleOut();
		this.G.Invoke("SupportOutIdle", CVars.pl_marker_out_to_idle, false);
	}

	// Token: 0x06000BEF RID: 3055 RVA: 0x00094628 File Offset: 0x00092828
	[Obfuscation(Exclude = true)]
	protected virtual void SupportOutIdle()
	{
		this.state.preSupportEquiped = this.state.equiped;
		if (this.state.preSupportEquiped == WeaponEquipedState.Marker || this.state.preSupportEquiped == WeaponEquipedState.none)
		{
			this.state.preSupportEquiped = WeaponEquipedState.Secondary;
		}
		this.state.equiped = WeaponEquipedState.Marker;
		this.OutIdle();
	}

	// Token: 0x06000BF0 RID: 3056 RVA: 0x0009468C File Offset: 0x0009288C
	[Obfuscation(Exclude = true)]
	protected virtual void SupportLight()
	{
	}

	// Token: 0x06000BF1 RID: 3057 RVA: 0x00094690 File Offset: 0x00092890
	public virtual void SupportAim()
	{
		if (this.state.supportReady)
		{
			return;
		}
		this.state.supportReady = true;
		this.G.Invoke("SupportLight", CVars.pl_marker_idle_to_charge * 0.4f, false);
		this.supportTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06000BF2 RID: 3058 RVA: 0x000946E4 File Offset: 0x000928E4
	public void SupportFire()
	{
		if (this.state.supportReady)
		{
			this.state.supportReady = false;
			this.state.equiped = this.state.preSupportEquiped;
			if (this.state.equiped == WeaponEquipedState.none || this.state.equiped == WeaponEquipedState.Marker)
			{
				this.state.equiped = WeaponEquipedState.Secondary;
			}
			this.Cancel();
			this.G.Invoke("OutIdle", CVars.pl_marker_charge_to_out, false);
			this.G.Invoke("AfterSupport", CVars.pl_marker_charge_to_out, false);
			if (this.player.playerInfo.hasMortar && this.lastSupport == ArmstreakEnum.mortar)
			{
				this.OnMortarStrike();
			}
			if (this.player.playerInfo.hasSonar && this.lastSupport == ArmstreakEnum.sonar)
			{
				this.OnSupportFire();
			}
		}
	}

	// Token: 0x06000BF3 RID: 3059 RVA: 0x000947D0 File Offset: 0x000929D0
	[Obfuscation(Exclude = true)]
	protected virtual void AfterSupport()
	{
	}

	// Token: 0x06000BF4 RID: 3060 RVA: 0x000947D4 File Offset: 0x000929D4
	public virtual void Cancel()
	{
		this.useBinocular = false;
		this.G.CancelInvoke();
		if (this.cSecondary)
		{
			this.cSecondary.Cancel();
		}
		if (this.cPrimary)
		{
			this.cPrimary.Cancel();
		}
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0009482C File Offset: 0x00092A2C
	public void Serialize(eNetworkStream stream)
	{
		this.state.Serialize(stream);
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0009483C File Offset: 0x00092A3C
	public void Deserialize(eNetworkStream stream)
	{
		this.state.Deserialize(stream);
	}

	// Token: 0x06000BF7 RID: 3063 RVA: 0x0009484C File Offset: 0x00092A4C
	protected virtual void PrepareWeapon()
	{
	}

	// Token: 0x06000BF8 RID: 3064 RVA: 0x00094850 File Offset: 0x00092A50
	protected virtual void OnFire()
	{
	}

	// Token: 0x06000BF9 RID: 3065 RVA: 0x00094854 File Offset: 0x00092A54
	protected virtual void OnFireReload()
	{
	}

	// Token: 0x06000BFA RID: 3066 RVA: 0x00094858 File Offset: 0x00092A58
	protected virtual void OnFirePlayReloadEnd()
	{
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x0009485C File Offset: 0x00092A5C
	protected virtual void OnPlayReload()
	{
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00094860 File Offset: 0x00092A60
	protected virtual void OnPlayPompovikReload()
	{
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00094864 File Offset: 0x00092A64
	protected virtual void UpdateAllInterface()
	{
	}

	// Token: 0x06000BFE RID: 3070 RVA: 0x00094868 File Offset: 0x00092A68
	protected virtual void OnPlayReloadEnd()
	{
	}

	// Token: 0x06000BFF RID: 3071 RVA: 0x0009486C File Offset: 0x00092A6C
	protected virtual void OnPlayInsertClip()
	{
	}

	// Token: 0x06000C00 RID: 3072 RVA: 0x00094870 File Offset: 0x00092A70
	protected virtual void OnPlayAfterReload()
	{
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00094874 File Offset: 0x00092A74
	protected virtual void OnPlayPompovikReloadEnd()
	{
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x00094878 File Offset: 0x00092A78
	protected virtual void OnPlayPompovikInsertClip()
	{
	}

	// Token: 0x06000C03 RID: 3075 RVA: 0x0009487C File Offset: 0x00092A7C
	protected virtual void OnGrenade()
	{
		this.Cancel();
		this.G.Invoke("AfterGrenade", 0.7f, false);
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x0009489C File Offset: 0x00092A9C
	protected virtual void OnSupportFire()
	{
		if (this.player.playerInfo.hasSonar)
		{
			this.player.playerInfo.hasSonar = false;
		}
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000948D0 File Offset: 0x00092AD0
	protected virtual void OnMortarStrike()
	{
		if (this.player.playerInfo.hasMortar)
		{
			this.player.playerInfo.hasMortar = false;
		}
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x00094904 File Offset: 0x00092B04
	protected virtual void OnPlayIdleAim()
	{
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00094908 File Offset: 0x00092B08
	protected virtual void OnPlayAimIdle()
	{
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x0009490C File Offset: 0x00092B0C
	public virtual void UpdateFireInterface()
	{
	}

	// Token: 0x04000D96 RID: 3478
	public BaseWeapon cPrimary;

	// Token: 0x04000D97 RID: 3479
	public BaseWeapon cSecondary;

	// Token: 0x04000D98 RID: 3480
	public BaseWeapon tmpWeapon;

	// Token: 0x04000D99 RID: 3481
	public AmmoState state = new AmmoState();

	// Token: 0x04000D9A RID: 3482
	public Invoker G = new Invoker();

	// Token: 0x04000D9B RID: 3483
	protected BaseNetPlayer player;

	// Token: 0x04000D9C RID: 3484
	protected GraphicValue aimGNAME = new GraphicValue();

	// Token: 0x04000D9D RID: 3485
	protected GraphicValue aimWeapGNAME = new GraphicValue();

	// Token: 0x04000D9E RID: 3486
	protected Transform gunHolderBone;

	// Token: 0x04000D9F RID: 3487
	protected float supportTime = -1f;

	// Token: 0x04000DA0 RID: 3488
	protected float grenadeTime;

	// Token: 0x04000DA1 RID: 3489
	protected float attackerDelay;

	// Token: 0x04000DA2 RID: 3490
	public float throwPowerMult = 1f;

	// Token: 0x04000DA3 RID: 3491
	public float ReloadSpeedMult = 1f;

	// Token: 0x04000DA4 RID: 3492
	public float AccuracyMult = 1f;

	// Token: 0x04000DA5 RID: 3493
	public float ApAmmo;

	// Token: 0x04000DA6 RID: 3494
	public float aimBonusMult = 1f;

	// Token: 0x04000DA7 RID: 3495
	public float KnifeDelay = 0.9f;

	// Token: 0x04000DA8 RID: 3496
	public bool LOD;

	// Token: 0x04000DA9 RID: 3497
	protected bool useBinocular;

	// Token: 0x04000DAA RID: 3498
	public bool CanUseBinocular;

	// Token: 0x04000DAB RID: 3499
	public float GrenadeTimer = CVars.g_grenadeTimeout;

	// Token: 0x04000DAC RID: 3500
	public ArmstreakEnum lastSupport = ArmstreakEnum.none;

	// Token: 0x04000DAD RID: 3501
	private bool _reloadStarted;

	// Token: 0x04000DAE RID: 3502
	private Transform playerCamera;

	// Token: 0x04000DAF RID: 3503
	private Quaternion cameraRotation;

	// Token: 0x04000DB0 RID: 3504
	protected Vector3 MoveDelta;

	// Token: 0x04000DB1 RID: 3505
	private Transform weapon;

	// Token: 0x04000DB2 RID: 3506
	private Camera opticCamera;

	// Token: 0x04000DB3 RID: 3507
	private Transform weaponAimPoint;

	// Token: 0x04000DB4 RID: 3508
	private Transform modAimPoint;

	// Token: 0x04000DB5 RID: 3509
	private float step;

	// Token: 0x04000DB6 RID: 3510
	private float speed = 5f;

	// Token: 0x04000DB7 RID: 3511
	private int deltaCounter;

	// Token: 0x04000DB8 RID: 3512
	private Vector3 prevDelta;

	// Token: 0x04000DB9 RID: 3513
	private bool _areTransformsSet;

	// Token: 0x04000DBA RID: 3514
	private bool _isMoveDeltaSet;

	// Token: 0x04000DBB RID: 3515
	protected bool _isStartAim;

	// Token: 0x04000DBC RID: 3516
	public bool ForcedChangeAimMode;

	// Token: 0x04000DBD RID: 3517
	private WeaponEquipedState _cachedEquipedState;
}
