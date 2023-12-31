using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Camouflage;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020001E7 RID: 487
[AddComponentMenu("Scripts/Game/EntityNetPlayer")]
internal class EntityNetPlayer : BaseNetPlayer
{
	// Token: 0x06001021 RID: 4129 RVA: 0x000B5CDC File Offset: 0x000B3EDC
	public override void OnPoolDespawn()
	{
		this.WidthTag = 0f;
		this.WidthNick = 0f;
		this.SpaceInfo = string.Empty;
		this.lastFireTime = new Float(EntityNetPlayer.ZeroInit.Value);
		this.lastSonarTime = new Float(EntityNetPlayer.ZeroInit.Value);
		base.OnPoolDespawn();
		this.MaySpawn = false;
		this.FirstUpdate = false;
		this.Locked = false;
		this.isPlayer = false;
		this.revive = new eTimer();
		this.hitSoundsRandom = null;
		this.armorHitSoundsRandom = null;
		this.showHealth = new Alpha();
		this.showArmor = new Alpha();
		this.controller = null;
		this.lod = null;
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06001022 RID: 4130 RVA: 0x000B5D94 File Offset: 0x000B3F94
	// (set) Token: 0x06001023 RID: 4131 RVA: 0x000B5D9C File Offset: 0x000B3F9C
	public override ObscuredInt ID { get; set; }

	// Token: 0x17000259 RID: 601
	// (get) Token: 0x06001024 RID: 4132 RVA: 0x000B5DA8 File Offset: 0x000B3FA8
	public Vector3 NeckPosition
	{
		get
		{
			if (this.controller == null)
			{
				return Vector3.zero;
			}
			if (this.controller.Neck == null)
			{
				return Vector3.zero;
			}
			return this.controller.Neck.position;
		}
	}

	// Token: 0x1700025A RID: 602
	// (get) Token: 0x06001025 RID: 4133 RVA: 0x000B5DF8 File Offset: 0x000B3FF8
	public bool IsTalking
	{
		get
		{
			return this.talk.Visible;
		}
	}

	// Token: 0x1700025B RID: 603
	// (get) Token: 0x06001026 RID: 4134 RVA: 0x000B5E08 File Offset: 0x000B4008
	public float TalkingValue
	{
		get
		{
			return this.talk.visibility_clean;
		}
	}

	// Token: 0x06001027 RID: 4135 RVA: 0x000B5E18 File Offset: 0x000B4018
	public void Init(EntityNetPlayer prototype, int index)
	{
		base.UserInfo = new UserInfo(false);
		this._fakePlayerIndex = index;
		this._prototype = prototype;
		this._fakeUserId = UnityEngine.Random.Range(100000000, 500000000);
		this._changePosInterval = UnityEngine.Random.Range(0.8f, 1.2f);
		EntityNetPlayer prototype2 = this._prototype;
		prototype2.OnDeserialize = (Action)Delegate.Combine(prototype2.OnDeserialize, new Action(this.CopyPrototype));
	}

	// Token: 0x06001028 RID: 4136 RVA: 0x000B5EA0 File Offset: 0x000B40A0
	public static bool IsClientPlayer(int playerId)
	{
		Assembly callingAssembly = Assembly.GetCallingAssembly();
		if (!callingAssembly.Equals(Assembly.GetExecutingAssembly()))
		{
			Peer.ClientGame.StartCoroutine(SingletoneComponent<Main>.Instance.TakeAndSendScreenshot("rcl", Main.UserInfo.userID));
		}
		return playerId <= CVars.ClientPlayerId;
	}

	// Token: 0x06001029 RID: 4137 RVA: 0x000B5EFC File Offset: 0x000B40FC
	public override void Deserialize(eNetworkStream stream)
	{
		this.lastPacket.playerInfo.Deserialize(stream);
		this.playerInfo = this.lastPacket.playerInfo;
		if (!this.lastPacket.playerInfo.dead && this.lastPacket.playerInfo.playerType != PlayerType.spectactor)
		{
			this.lastPacket.moveState.Deserialize(stream);
			this.lastPacket.ammoState.Deserialize(stream);
			if (base.Ammo != null)
			{
				base.Ammo.state.grenadeCount = this.lastPacket.ammoState.grenadeCount;
				if (base.Ammo.CurrentWeapon != null)
				{
					if (base.Ammo.CurrentWeapon.IsPrimary)
					{
						base.Ammo.CurrentWeapon.state.clips = this.lastPacket.ammoState.primaryState.clips;
						base.Ammo.CurrentWeapon.state.bagSize = this.lastPacket.ammoState.primaryState.bagSize;
					}
					if (base.Ammo.CurrentWeapon.IsSecondary)
					{
						base.Ammo.CurrentWeapon.state.clips = this.lastPacket.ammoState.secondaryState.clips;
						base.Ammo.CurrentWeapon.state.bagSize = this.lastPacket.ammoState.secondaryState.bagSize;
					}
				}
			}
			this.enCollector.Deserialize(stream);
		}
		if (this.lastPacket.playerInfo.skillsInfos == null)
		{
			Peer.ClientGame.LocalPlayer.FullUpdateRequest();
			return;
		}
		if (Peer.ClientGame.LocalPlayer.ID == this.lastPacket.playerInfo.playerID)
		{
			this.isPlayer = true;
			Peer.ClientGame.LocalPlayer.Entity = this;
		}
		this.lastPacket.Time = Time.time;
		this.tempPacket.Clone(this.lastPacket);
		if (!this.MaySpawn)
		{
			this.packets.Add(this.tempPacket);
			if (this.packets.Length > 2)
			{
				this.MaySpawn = true;
			}
		}
		this.FirstUpdate = true;
		this.OnDeserialize();
	}

	// Token: 0x0600102A RID: 4138 RVA: 0x000B6170 File Offset: 0x000B4370
	private void CopyPrototype()
	{
		if (this._changePosTimer.IsStarted && this._changePosTimer.Time < this._changePosInterval)
		{
			return;
		}
		this._changePosTimer.Start();
		this.playerInfo.Clone(this._prototype.playerInfo);
		base.UserInfo = (UserInfo)base.UserInfo.Clone();
		this.lastPacket.Clone(this._prototype.lastPacket);
		this.FirstUpdate = this._prototype.FirstUpdate;
		this.MaySpawn = this._prototype.MaySpawn;
		this.loadingState = LoadingState.clientFinishedLoading;
		if (this._fakePlayerIndex >= CVars.ClientPlayersCount - 2)
		{
			this._offsetFromPrototype = new Vector3(UnityEngine.Random.Range(-5f, 5f), UnityEngine.Random.Range(-0.5f, 1f), UnityEngine.Random.Range(3f, 10f));
		}
		if (this._fakePlayerIndex == CVars.ClientPlayersCount - 3)
		{
			this._offsetFromPrototype = new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(-0.5f, 0.5f), 30f + UnityEngine.Random.Range(-0.5f, 0.5f));
		}
		this._offsetFromPrototype = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-0.5f, 1f), UnityEngine.Random.Range(3f, 10f));
		this.lastPacket.moveState.euler += UnityEngine.Random.Range(-60f, 60f) * Vector3.up;
		if (base.UserInfo != null)
		{
			base.UserInfo.userID = null;
		}
		this.playerInfo.userID = this._fakeUserId;
		this.SpaceInfo = string.Concat(new object[]
		{
			"Player_",
			this.playerInfo.userID,
			"[",
			this.playerInfo.level,
			"]"
		});
		this.playerInfo.Nick = this.SpaceInfo;
		base.UserInfo.nick = null;
		base.UserInfo.nickColor = null;
		this.loadingState = LoadingState.none;
		this.playerInfo.loading = 0;
		this.lastPacket.playerInfo.loading = 0;
		this.playerInfo.clanTag = string.Empty;
		this.lastPacket.playerInfo.clanTag = string.Empty;
		this.playerInfo.buffs = Buffs.none;
		this.lastPacket.playerInfo.buffs = Buffs.none;
		this.ID = CVars.ClientPlayerId - this._fakePlayerIndex * this._fakePlayerIndex;
		this.playerInfo.playerID = this.ID;
		this.lastPacket.playerInfo.playerID = this.ID;
		this.lastPacket.ammoState.grenadeCount = 0;
		this.lastPacket.ammoState.primaryIndex = 127;
		this.lastPacket.ammoState.secondaryIndex = 0;
		this.lastPacket.ammoState.primaryMod = false;
		this.lastPacket.ammoState.secondaryMod = false;
		this.lastPacket.ammoState.primaryState.Mods = string.Empty;
		this.lastPacket.ammoState.secondaryState.Mods = string.Empty;
		this.lastPacket.ammoState.secondaryState.Mods = string.Empty;
		this.lastPacket.ammoState.equiped = WeaponEquipedState.Secondary;
		if (this._prototype.PlayerType == PlayerType.bear)
		{
			base.PlayerType = PlayerType.usec;
			this.playerInfo.playerType = PlayerType.usec;
			this.lastPacket.playerInfo.playerType = PlayerType.usec;
		}
		else if (this._prototype.PlayerType == PlayerType.usec)
		{
			base.PlayerType = PlayerType.bear;
			this.playerInfo.playerType = PlayerType.bear;
			this.lastPacket.playerInfo.playerType = PlayerType.bear;
		}
		this.tempPacket.Clone(this.lastPacket);
	}

	// Token: 0x0600102B RID: 4139 RVA: 0x000B65D8 File Offset: 0x000B47D8
	public override void CallFixedUpdate()
	{
		if (!this.FirstUpdate)
		{
			return;
		}
		if (!this.MaySpawn)
		{
			return;
		}
		if (this.revive.Elapsed < 0.1f)
		{
			return;
		}
		if (base.IsAlive)
		{
			this.UInput.Load(this.enCollector.lastCmd.buttons);
			if (!this.isPlayer)
			{
				base.CallFixedUpdate();
			}
		}
	}

	// Token: 0x0600102C RID: 4140 RVA: 0x000B664C File Offset: 0x000B484C
	public override void CallLateUpdate()
	{
		if (!this.FirstUpdate)
		{
			return;
		}
		if (!this.MaySpawn)
		{
			return;
		}
		if (EntityNetPlayer.IsClientPlayer(this.ID))
		{
			Quaternion rotation = Quaternion.Euler(Peer.ClientGame.LocalPlayer.Euler.x + 90f, Peer.ClientGame.LocalPlayer.Euler.y, Peer.ClientGame.LocalPlayer.Euler.z);
			Vector3 v = rotation * Vector3.right;
			Vector3 v2 = rotation * Vector3.up;
			Vector3 v3 = rotation * Vector3.forward;
			Vector4 v4 = Peer.ClientGame.LocalPlayer.Position;
			v4.w = 1f;
			Matrix4x4 matrix4x = default(Matrix4x4);
			matrix4x.SetColumn(0, v);
			matrix4x.SetColumn(1, v2);
			matrix4x.SetColumn(2, v3);
			matrix4x.SetColumn(3, v4);
			Vector3 pos = matrix4x.MultiplyPoint(this._offsetFromPrototype);
			this.lastPacket.moveState.pos = pos;
			this.tempPacket.moveState.pos = pos;
		}
		if ((this.lastPacket.playerInfo.dead || this.lastPacket.playerInfo.playerType == PlayerType.spectactor) && base.IsPlayerObject)
		{
			if (this.playerInfo.playerID == Peer.ClientGame.LocalPlayer.ID)
			{
				Forms.OnDie();
				EventFactory.Call("Die", null);
				GameObject camera = PlayerMainCameraManager.GetCamera();
				this.mainCamera = camera.camera;
				this.mainCamera.transform.parent = Utility.FindHierarchy(this.PlayerTransform, "EYE_OF_GOD");
				this.mainCamera.transform.localPosition = Vector3.zero;
				this.mainCamera.transform.localEulerAngles = Vector3.zero;
				this.mainCamera = null;
			}
			this.Kill("legs", Vector3.zero);
			this.DespawnPlayerObject();
			this.KillPlayer();
		}
		if (base.IsAlive && !base.IsPlayerObject)
		{
			this.SpawnPlayerObject(this.lastPacket.moveState.pos, this.lastPacket.moveState.euler, this.lastPacket.ammoState.secondaryIndex, this.lastPacket.ammoState.primaryIndex, this.lastPacket.ammoState.secondaryMod, this.lastPacket.ammoState.primaryMod, this.lastPacket.ammoState.secondaryState.repair_info, this.lastPacket.ammoState.primaryState.repair_info, this.lastPacket.ammoState.secondaryState.Mods, this.lastPacket.ammoState.primaryState.Mods, this.lastPacket.ammoState.WeaponKit);
			this._bugAnalFuckYouIvanCounter = 0;
		}
		if (this.revive.Elapsed < 0.1f)
		{
			return;
		}
		if (base.IsAlive)
		{
			if (!this.isPlayer && !EntityNetPlayer.IsClientPlayer(this.ID))
			{
				if (base.Ammo != null && this.Locked && base.Ammo.state.equiped != this.lastPacket.ammoState.equiped)
				{
					base.Ammo.state.equiped = this.lastPacket.ammoState.equiped;
					if (base.Ammo.CurrentWeapon != null && base.Ammo.state.equiped != WeaponEquipedState.none)
					{
						((ClientWeapon)base.Ammo.CurrentWeapon).Hide();
						if (base.Ammo.state.equiped == WeaponEquipedState.Primary)
						{
							base.AnimationsThird.PlayIdleOut(AnimWeapon.Pistol, base.Ammo.CurrentWeapon.OutIdle);
							((ClientWeapon)base.Ammo.cPrimary).Show();
							((ClientWeapon)base.Ammo.cSecondary).Hide();
						}
						if (base.Ammo.state.equiped == WeaponEquipedState.Secondary)
						{
							base.AnimationsThird.PlayIdleOut(AnimWeapon.AsRifle, base.Ammo.CurrentWeapon.OutIdle);
							((ClientWeapon)base.Ammo.cSecondary).Show();
							((ClientWeapon)base.Ammo.cPrimary).Hide();
						}
					}
				}
				if (base.IsPlayerObject)
				{
					if (this.animationsThird)
					{
						this.animationsThird.CallLateUpdate();
					}
					if (this.controller != null)
					{
						this.controller.CallLateUpdate();
					}
				}
			}
			else if (base.IsPlayerObject)
			{
				if (this.animationsThird)
				{
					this.animationsThird.CallLateUpdate();
				}
				if (this.controller != null)
				{
					this.controller.CallLateUpdate();
				}
			}
			if (!this.isPlayer && this._bugAnalFuckYouIvanCounter < 20 && base.Ammo != null && base.Ammo.CurrentWeapon != null)
			{
				if (this._bugAnalFuckYouIvanCounter == 19)
				{
					List<Renderer> renderersCurrent = ((ClientWeapon)base.Ammo.CurrentWeapon).RenderersCurrent;
					foreach (Renderer renderer in renderersCurrent)
					{
						renderer.enabled = true;
					}
					this._bugAnalFuckYouIvanCounter = 0;
				}
				this._bugAnalFuckYouIvanCounter++;
			}
		}
	}

	// Token: 0x0600102D RID: 4141 RVA: 0x000B6C68 File Offset: 0x000B4E68
	public override void OnSingleAttack(WeaponNature nature)
	{
		if (nature != WeaponNature.mortar && nature != WeaponNature.knife && nature != WeaponNature.grenade)
		{
			this.lastFireTime = Time.time + 1f;
		}
	}

	// Token: 0x0600102E RID: 4142 RVA: 0x000B6C9C File Offset: 0x000B4E9C
	protected override void DespawnPlayerObject()
	{
		if (base.Ammo != null && base.Ammo.CurrentWeapon != null)
		{
			((ClientWeapon)base.Ammo.CurrentWeapon).Hide();
		}
		base.CancelInvoke("DelayedKill");
		this.packets.Clear();
		this.k = 0f;
		this.revive.Start();
		this.lod = null;
		this.FirstUpdate = false;
		base.DespawnPlayerObject();
		this.controller = null;
	}

	// Token: 0x0600102F RID: 4143 RVA: 0x000B6D2C File Offset: 0x000B4F2C
	protected override void SpawnPlayerObject(Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
		this.DespawnPlayerObject();
		this.SpaceInfo = string.Concat(new object[]
		{
			base.Nick,
			"[",
			base.Level,
			"]"
		});
		base.PlayerObject = SingletoneForm<PoolManager>.Instance["client_player"].Spawn();
		PoolItem component = base.PlayerObject.GetComponent<PoolItem>();
		if (component.Childs.Count > 0)
		{
			component.OnPoolDespawn();
		}
		this.PlayerTransform.parent = base.transform;
		base.PlayerObject.GetComponent<EntityAmmunitions>().LOD = true;
		string text = string.Empty;
		if (this.lastPacket.playerInfo.playerType == PlayerType.bear)
		{
			text = "client_bear";
		}
		else if (this.lastPacket.playerInfo.playerType == PlayerType.usec)
		{
			text = "client_usec";
		}
		if (EntityNetPlayer.IsClientPlayer(this.ID))
		{
			text += "_fake";
		}
		GameObject gameObject = SingletoneForm<PoolManager>.Instance[text].Spawn();
		gameObject.name = gameObject.name.Replace("_fake", string.Empty);
		gameObject.GetComponent<Animation>().enabled = true;
		base.PlayerObject.GetComponent<PoolItem>().Childs.Add(gameObject.GetComponent<PoolItem>());
		Utility.ChangeParent(gameObject.transform, this.PlayerTransform);
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(this.playerInfo.Camouflages, string.Empty);
		int num = 0;
		if (dictionary.ContainsKey(weaponKit.ToString()))
		{
			num = (int)dictionary[weaponKit.ToString()];
		}
		Transform transform = gameObject.transform.FindChild("LOD0");
		foreach (Material material in transform.GetComponent<SkinnedMeshRenderer>().materials)
		{
			if (material.shader.name.ToLower().Contains("bumped specular mask"))
			{
				CamouflageInfo camouflageInfo = ModsStorage.Instance().CharacterCamouflages[num];
				MasteringMod modById = ModsStorage.Instance().GetModById(num);
				material.SetTexture("_DetailTex", (modById == null) ? null : modById.Texture);
				material.SetTextureScale("_DetailTex", new Vector2(camouflageInfo.Scale, camouflageInfo.Scale));
			}
		}
		Transform transform2 = gameObject.transform.FindChild("LOD1");
		transform2.GetComponent<SkinnedMeshRenderer>().castShadows = !EntityNetPlayer.IsClientPlayer(this.ID);
		foreach (Material material2 in transform2.GetComponent<SkinnedMeshRenderer>().materials)
		{
			if (material2.shader.name.ToLower().Contains("bumped specular mask"))
			{
				CamouflageInfo camouflageInfo2 = ModsStorage.Instance().CharacterCamouflages[num];
				MasteringMod modById2 = ModsStorage.Instance().GetModById(num);
				material2.SetTexture("_DetailTex", (modById2 == null) ? null : modById2.Texture);
				material2.SetTextureScale("_DetailTex", new Vector2(camouflageInfo2.Scale, camouflageInfo2.Scale));
			}
		}
		this.animationsThird = base.PlayerObject.GetComponentInChildren<AnimationsThird>();
		this.animationsThird.Init(this);
		this.animationsThird.animation.enabled = true;
		DamageX[] componentsInChildren = this.animationsThird.GetComponentsInChildren<DamageX>();
		for (int k = 0; k < componentsInChildren.Length; k++)
		{
			componentsInChildren[k].player = this;
		}
		this.mainCamera = Utility.FindHierarchy(this.PlayerTransform, "camera").camera;
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
		this.ammo = base.PlayerObject.GetComponent<BaseAmmunitions>();
		this.ammo.state.primaryIndex = primaryIndex;
		this.ammo.state.secondaryIndex = secondaryIndex;
		this.ammo.state.primaryMod = primaryMod;
		this.ammo.state.secondaryMod = secondaryMod;
		this.ammo.state.primaryState.repair_info = primary_repair_info;
		this.ammo.state.secondaryState.repair_info = secondary_repair_info;
		this.ammo.state.primaryState.Mods = primaryMods;
		this.ammo.state.secondaryState.Mods = secondaryMods;
		this.ammo.state.WeaponKit = weaponKit;
		if (!this.isPlayer && !EntityNetPlayer.IsClientPlayer(this.ID))
		{
			this.ammo.Init(this);
			this.ammo.AfterInit();
		}
		this.controller = base.PlayerObject.GetComponent<EntityMoveController>();
		this.controller = this.controller;
		this.controller.Init(this, pos);
		this.controller.state.euler = euler;
		this.controller.CallLateUpdate();
		this.RevivePlayer();
		this.LastExpMult = Skills.none;
		if (this.animationsThird)
		{
			this.animationsThird.CallLateUpdate();
		}
		if (!Peer.HardcoreMode)
		{
			for (int l = 0; l < this.playerInfo.skillsInfos.Length; l++)
			{
				if (this.playerInfo.skillsInfos[l])
				{
					base.ParseSkill((Skills)l);
				}
			}
			for (int m = 0; m < this.playerInfo.clanSkillsInfos.Length; m++)
			{
				if (this.playerInfo.clanSkillsInfos[m])
				{
					base.ParseClanSkill((Cl_Skills)m);
				}
			}
		}
		else
		{
			base.EnableHardCoreSkills();
		}
		this.CheckBinoculars();
		if (!EntityNetPlayer.IsClientPlayer(this.ID))
		{
			Thermal.HighLightEntity(this);
		}
		if (this.isPlayer || EntityNetPlayer.IsClientPlayer(this.ID))
		{
			this.Locked = EntityNetPlayer.IsClientPlayer(this.ID);
			gameObject.GetComponent<LODGroup>().enabled = EntityNetPlayer.IsClientPlayer(this.ID);
			(base.Ammo as EntityAmmunitions).locked = false;
			(base.Ammo as EntityAmmunitions).Hide();
			if (base.Ammo.CurrentWeapon != null)
			{
				((ClientWeapon)base.Ammo.CurrentWeapon).Hide();
			}
		}
		else
		{
			LODGroup component2 = gameObject.GetComponent<LODGroup>();
			component2.enabled = true;
			if (Main.UserInfo.settings.graphics.CharacterLQ)
			{
				component2.ForceLOD(1);
			}
			else
			{
				component2.ForceLOD(-1);
			}
			this.Locked = true;
			if (base.Ammo.CurrentWeapon != null)
			{
				(base.Ammo as EntityAmmunitions).Show();
			}
			Thermal.RefrashMaterials(gameObject);
			this._prototype = Peer.ClientGame.LocalPlayer.Entity;
		}
		if (EntityNetPlayer.IsClientPlayer(this.ID))
		{
			foreach (Collider collider in base.GetComponentsInChildren<Collider>())
			{
				collider.enabled = false;
			}
			foreach (DamageX damageX in base.GetComponentsInChildren<DamageX>())
			{
				damageX.enabled = false;
			}
			this._changePosTimer.Start();
		}
	}

	// Token: 0x06001030 RID: 4144 RVA: 0x000B74E4 File Offset: 0x000B56E4
	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.UC != null)
		{
			this.UC = new EntityCmdCollector();
		}
	}

	// Token: 0x06001031 RID: 4145 RVA: 0x000B7504 File Offset: 0x000B5704
	public override void Hit(int playerID, int targetID, float health, float armor)
	{
		if (this.isPlayer || EntityNetPlayer.IsClientPlayer(this.ID))
		{
			return;
		}
		if (playerID == Peer.ClientGame.LocalPlayer.ID && Peer.ClientGame.LocalPlayer.IsAlive && Main.UserInfo.skillUnlocked(Skills.analyze2))
		{
			if (Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon != null && Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.Optic && Peer.ClientGame.LocalPlayer.Ammo.IsAim)
			{
				return;
			}
			this.showHealth.OnceLong(0.3f, 1.5f);
		}
		if (playerID == Peer.ClientGame.LocalPlayer.ID && Peer.ClientGame.LocalPlayer.IsAlive && Main.UserInfo.skillUnlocked(Skills.att4))
		{
			if (Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon != null && Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.Optic && Peer.ClientGame.LocalPlayer.Ammo.IsAim)
			{
				return;
			}
			if (base.Armor != armor)
			{
				this.showArmor.OnceLong(0.5f, 1f);
			}
		}
		if (playerID == Peer.ClientGame.LocalPlayer.ID && Peer.ClientGame.LocalPlayer.IsAlive && Peer.ClientGame.LocalPlayer.SkillUnlocked(Skills.hear2))
		{
			if (base.Armor != armor)
			{
				if (this.armorHitSoundsRandom == null)
				{
					this.showArmor.OnceLong(0.3f, 1.5f);
					this.armorHitSoundsRandom = new CircleRandom();
					this.armorHitSoundsRandom.InitNew(SingletoneForm<SoundFactory>.Instance.armorHitSounds.Length);
				}
				if (BIT.AND((int)this.playerInfo.buffs, 1048576) || BIT.AND((int)this.playerInfo.buffs, 524288))
				{
					Audio.Play(SingletoneForm<SoundFactory>.Instance.brokenLimb);
				}
				else
				{
					Audio.Play(Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.armorHitSounds[this.armorHitSoundsRandom.Get()], false, 10f, 150f);
				}
			}
			else
			{
				if (this.hitSoundsRandom == null)
				{
					this.hitSoundsRandom = new CircleRandom();
					this.hitSoundsRandom.InitNew(SingletoneForm<SoundFactory>.Instance.hitSounds.Length);
				}
				if (BIT.AND((int)this.playerInfo.buffs, 1048576) || BIT.AND((int)this.playerInfo.buffs, 524288))
				{
					Audio.Play(SingletoneForm<SoundFactory>.Instance.brokenLimb);
				}
				else
				{
					Audio.Play(Peer.ClientGame.LocalPlayer.MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.hitSounds[this.hitSoundsRandom.Get()], false, 10f, 150f);
				}
			}
		}
		base.Hit(playerID, targetID, health, armor);
	}

	// Token: 0x06001032 RID: 4146 RVA: 0x000B786C File Offset: 0x000B5A6C
	[Obfuscation(Exclude = true)]
	private void DelayedKill()
	{
		this.Kill("legs", Vector3.zero);
	}

	// Token: 0x06001033 RID: 4147 RVA: 0x000B7880 File Offset: 0x000B5A80
	public virtual void Kill(string name, Vector3 power)
	{
		if (EntityNetPlayer.IsClientPlayer(this.ID))
		{
			this._changePosTimer.Stop();
		}
		if (!base.IsPlayerObject)
		{
			this.Disappear();
			return;
		}
		this.Locked = false;
		this.animationsThird.animation.Stop();
		this.animationsThird.animation.enabled = false;
		if (EntityNetPlayer.IsClientPlayer(this.ID))
		{
			GameObject gameObject = this.animationsThird.gameObject;
			gameObject.name += "_fake";
		}
		switch (name)
		{
		case "tactical":
			this.animationsThird.RagDoll(power * 30f);
			goto IL_179;
		case "mortar":
		case "grenade":
			this.animationsThird.RagDoll(power * 15f);
			goto IL_179;
		case "legs":
		case "chooseTeam":
			this.animationsThird.RagDoll(Vector3.zero);
			goto IL_179;
		}
		this.animationsThird.RagDoll(power * 10f);
		IL_179:
		this.DieSound();
		if (this.playerInfo.playerID == Peer.ClientGame.LocalPlayer.ID || EntityNetPlayer.IsClientPlayer(this.ID))
		{
			base.PlayerObject.GetComponent<PoolItem>().AutoDespawn(Main.UserInfo.settings.graphics.SelfRagDollTime);
		}
		else
		{
			base.PlayerObject.GetComponent<PoolItem>().AutoDespawn(Main.UserInfo.settings.graphics.RagDollTime);
		}
		this.PlayerTransform.parent = Main.Trash;
		this.playerObject = null;
		this.DespawnPlayerObject();
	}

	// Token: 0x06001034 RID: 4148 RVA: 0x000B7AB4 File Offset: 0x000B5CB4
	private void DieSound()
	{
		int num = SingletoneForm<SoundFactory>.Instance.dieSounds.Length;
		if (num < 0)
		{
			num = 0;
		}
		Audio.Play(this.PlayerTransform.position, SingletoneForm<SoundFactory>.Instance.dieSounds[UnityEngine.Random.Range(0, num - 1)], -1f, -1f);
	}

	// Token: 0x06001035 RID: 4149 RVA: 0x000B7B08 File Offset: 0x000B5D08
	public void Disappear()
	{
		this.DespawnPlayerObject();
	}

	// Token: 0x06001036 RID: 4150 RVA: 0x000B7B10 File Offset: 0x000B5D10
	public void Talk()
	{
		this.talk.Once(0.5f, 2f, 0.5f);
	}

	// Token: 0x04001088 RID: 4232
	private static readonly Float ZeroInit = new Float(0f);

	// Token: 0x04001089 RID: 4233
	private EntityCmdCollector enCollector = new EntityCmdCollector();

	// Token: 0x0400108A RID: 4234
	internal ClassArray<EntityPacket> packets = new ClassArray<EntityPacket>(CVars.g_tickrate);

	// Token: 0x0400108B RID: 4235
	public EntityPacket lastPacket = new EntityPacket();

	// Token: 0x0400108C RID: 4236
	public EntityPacket tempPacket = new EntityPacket();

	// Token: 0x0400108D RID: 4237
	public float k;

	// Token: 0x0400108E RID: 4238
	private eTimer revive = new eTimer();

	// Token: 0x0400108F RID: 4239
	private CircleRandom hitSoundsRandom;

	// Token: 0x04001090 RID: 4240
	private CircleRandom armorHitSoundsRandom;

	// Token: 0x04001091 RID: 4241
	public Alpha showHealth = new Alpha();

	// Token: 0x04001092 RID: 4242
	public Alpha showArmor = new Alpha();

	// Token: 0x04001093 RID: 4243
	private Alpha talk = new Alpha();

	// Token: 0x04001094 RID: 4244
	private AutoCustomLod lod;

	// Token: 0x04001095 RID: 4245
	private new EntityMoveController controller;

	// Token: 0x04001096 RID: 4246
	private bool Locked;

	// Token: 0x04001097 RID: 4247
	public bool FirstUpdate;

	// Token: 0x04001098 RID: 4248
	public bool MaySpawn;

	// Token: 0x04001099 RID: 4249
	public Float lastFireTime = new Float(EntityNetPlayer.ZeroInit.Value);

	// Token: 0x0400109A RID: 4250
	public Float lastSonarTime = new Float(EntityNetPlayer.ZeroInit.Value);

	// Token: 0x0400109B RID: 4251
	public float lastHotSpotTime;

	// Token: 0x0400109C RID: 4252
	public string SpaceInfo = string.Empty;

	// Token: 0x0400109D RID: 4253
	public float WidthTag;

	// Token: 0x0400109E RID: 4254
	public float WidthNick;

	// Token: 0x0400109F RID: 4255
	public Action OnDeserialize = delegate()
	{
	};

	// Token: 0x040010A0 RID: 4256
	private ObscuredInt _fakePlayerIndex = 0;

	// Token: 0x040010A1 RID: 4257
	private ObscuredVector3 _offsetFromPrototype = Vector3.zero;

	// Token: 0x040010A2 RID: 4258
	private ObscuredFloat _changePosInterval;

	// Token: 0x040010A3 RID: 4259
	private Timer _changePosTimer = new Timer();

	// Token: 0x040010A4 RID: 4260
	private EntityNetPlayer _prototype;

	// Token: 0x040010A5 RID: 4261
	private ObscuredInt _fakeUserId = 0;

	// Token: 0x040010A6 RID: 4262
	private int _bugAnalFuckYouIvanCounter;
}
