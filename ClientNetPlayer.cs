using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Game;
using cygwin_x32;
using UnityEngine;

// Token: 0x020001AD RID: 429
[AddComponentMenu("Scripts/Game/ClientNetPlayer")]
internal class ClientNetPlayer : BaseRpcNetPlayer
{
	// Token: 0x06000E78 RID: 3704 RVA: 0x000A89A0 File Offset: 0x000A6BA0
	public ClientNetPlayer()
	{
		this.CameraAnimations = null;
		this.IsTeamChoosed = false;
	}

	// Token: 0x06000E79 RID: 3705 RVA: 0x000A89FC File Offset: 0x000A6BFC
	public override void OnPoolDespawn()
	{
		if (this.ExplosionCamera)
		{
			SingletoneForm<PoolManager>.Instance[this.ExplosionCamera.name].Despawn(this.ExplosionCamera.GetComponent<PoolItem>());
			this.ExplosionCamera = null;
		}
		this._packetLoss = 0;
		this.InPoint = -1;
		this.PlayerInput.Clear();
		this.Prediction.Clear();
		this._fullUpdateRequest = false;
		this._hitSoundsRandom = null;
		this._armorHitSoundsRandom = null;
		this._entity = null;
		this._needSync = true;
		this._systemHit = false;
		this.CmdNumber = 0;
		this.CameraAnimations = null;
		this.IsTeamChoosed = false;
		base.OnPoolDespawn();
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000E7A RID: 3706 RVA: 0x000A8AB0 File Offset: 0x000A6CB0
	// (set) Token: 0x06000E7B RID: 3707 RVA: 0x000A8AB8 File Offset: 0x000A6CB8
	public bool IsTeamChoosed { get; set; }

	// Token: 0x1700022C RID: 556
	// (get) Token: 0x06000E7C RID: 3708 RVA: 0x000A8AC4 File Offset: 0x000A6CC4
	// (set) Token: 0x06000E7D RID: 3709 RVA: 0x000A8ACC File Offset: 0x000A6CCC
	public CameraAnimations CameraAnimations { get; private set; }

	// Token: 0x1700022D RID: 557
	// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000A8AD8 File Offset: 0x000A6CD8
	public bool IsContused
	{
		get
		{
			return this._lowpassFilter != null && this._lowpassFilter.enabled;
		}
	}

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000A8AFC File Offset: 0x000A6CFC
	// (set) Token: 0x06000E80 RID: 3712 RVA: 0x000A8B04 File Offset: 0x000A6D04
	public EntityNetPlayer Entity
	{
		get
		{
			return this._entity;
		}
		set
		{
			this._entity = value;
		}
	}

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06000E81 RID: 3713 RVA: 0x000A8B10 File Offset: 0x000A6D10
	public new Vector3 Position
	{
		get
		{
			return (!this.PlayerTransform) ? Vector3.zero : this.PlayerTransform.position;
		}
	}

	// Token: 0x06000E82 RID: 3714 RVA: 0x000A8B38 File Offset: 0x000A6D38
	protected override void Send(eNetworkStream stream)
	{
		short num = 1;
		if (this.loadingState == LoadingState.clientLoading)
		{
			num |= 4;
		}
		if (this.UC.CmdsCount > 0)
		{
			num |= 2;
		}
		stream.Serialize(ref num);
		if (this.loadingState == LoadingState.clientLoading)
		{
			int num2 = (int)(Loader.Progress("GameData") * 100f);
			stream.Serialize(ref num2);
		}
		if (this.UC.CmdsCount > 0)
		{
			this.UC.Serialize(stream);
		}
		this.PlayerState = 0;
		if (base.Ammo != null)
		{
			this.PlayerState = (int)base.Ammo.state.equiped;
			this.PlayerState <<= 5;
			if (this.controller)
			{
				this.PlayerState |= ((!this.controller.state.isSeat) ? 0 : 1) << 2;
			}
			this.PlayerState |= ((!base.Ammo.IsAim) ? 0 : 1) << 1;
		}
		stream.Serialize(ref this.PlayerState);
		int num3 = 0;
		if (base.UserInfo != null)
		{
			num3 = base.UserInfo.Violation.Value;
		}
		stream.Serialize(ref num3);
		if (this._violTimer.Time > 30f || !this._violTimer.IsStarted)
		{
			this._violTimer.Start();
			base.ToServer("SendAdditionalData", new object[]
			{
				new int[]
				{
					num3
				}
			});
		}
		int emuState = CygWin32L.Instance.GetEmuState();
		stream.Serialize(ref emuState);
	}

	// Token: 0x06000E83 RID: 3715 RVA: 0x000A8CEC File Offset: 0x000A6EEC
	protected override void Recieve(eNetworkStream stream)
	{
		bool isGameLoading = Main.IsGameLoading;
		bool flag = !PrefabFactory.AddLoaded;
		bool flag2 = !SingletoneForm<Loader>.Instance.IsGameLoaded;
		if (isGameLoading || flag || flag2)
		{
			return;
		}
		Peer.ClientGame.Deserialize(stream);
		this.playerInfo.clientHalfPing = stream.halfPing;
		if (stream.isFullUpdate)
		{
			if (this._needSync)
			{
				this.Sync();
			}
			this.FullUpdateRequestFinished();
		}
		if (base.IsAlive && this._entity && !this._entity.lastPacket.playerInfo.dead)
		{
			this.playerInfo.Clone(this._entity.lastPacket.playerInfo);
			this.playerInfo.clientHalfPing = stream.halfPing;
			this.Prediction.AddServer(this._entity.lastPacket);
			if (this.Prediction.NeedMoveDrop)
			{
				EventFactory.Call("ShowConnectionProblem", null);
			}
			if (this.Prediction.NeedFullUpdate)
			{
				this.SyncRequest();
			}
		}
		this.PacketNum++;
		int num = 0;
		stream.Serialize(ref num);
		if (this.PacketNum != num && this._packetLossTimer < Time.time)
		{
			this._packetLossTimer = Time.time + 1f;
			this._packetLoss = num - this.PacketNum;
			this.PacketNum = num;
			if (this._packetLoss >= 2)
			{
				EventFactory.Call("ShowConnectionProblem", null);
			}
		}
		if (this.controller)
		{
			this.controller.state.isWalk = this._entity.lastPacket.moveState.isWalk;
		}
		if (this.ammo)
		{
			this.AimSynchFromServer = this._entity.lastPacket.ammoState.isAim;
		}
	}

	// Token: 0x06000E84 RID: 3716 RVA: 0x000A8EE0 File Offset: 0x000A70E0
	public void SetCmdNumber(int cmdnumber)
	{
		this.CmdNumber = cmdnumber;
	}

	// Token: 0x06000E85 RID: 3717 RVA: 0x000A8EEC File Offset: 0x000A70EC
	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.UC != null)
		{
			this.UC = new ClientCmdCollector();
		}
	}

	// Token: 0x06000E86 RID: 3718 RVA: 0x000A8F0C File Offset: 0x000A710C
	public override void Initialize(int userID, int playerID)
	{
		Debug.Log("ClientNetPlayer.Initialize");
		this.playerInfo.Nick = Main.UserInfo.nick;
		this.playerInfo.NickColor = Main.UserInfo.nickColor;
		this.playerInfo.clanTag = Main.UserInfo.clanTag;
		this.playerInfo.skillsInfos = Main.UserInfo.skillArray;
		if (!ClientLeagueSystem.IsLeagueGame)
		{
			this.playerInfo.clanSkillsInfos = Main.UserInfo.clanSkillArray;
		}
		base.Initialize(userID, playerID);
	}

	// Token: 0x06000E87 RID: 3719 RVA: 0x000A8FA0 File Offset: 0x000A71A0
	public override void Hit(int playerID, int targetID, float health, float armor)
	{
		base.Hit(playerID, targetID, health, armor);
		if (base.Health == health && Main.IsTeamGame && !Peer.HardcoreMode)
		{
			return;
		}
		if (base.IsAlive)
		{
			this._systemHit = true;
			if (base.Armor != armor)
			{
				if (this._armorHitSoundsRandom == null)
				{
					this._armorHitSoundsRandom = new CircleRandom();
					this._armorHitSoundsRandom.InitNew(SingletoneForm<SoundFactory>.Instance.armorHitSounds.Length);
				}
				Audio.Play(this.mainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.armorHitSounds[this._armorHitSoundsRandom.Get()], false, 10f, 150f);
			}
			else
			{
				if (this._hitSoundsRandom == null)
				{
					this._hitSoundsRandom = new CircleRandom();
					this._hitSoundsRandom.InitNew(SingletoneForm<SoundFactory>.Instance.hitSounds.Length);
				}
				Audio.Play(this.mainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.hitSounds[this._hitSoundsRandom.Get()], false, 10f, 150f);
			}
			EventFactory.Call("Hit", null);
			(this.controller as ClientMoveController).shaker.InitShake((base.Health - health) / 100f);
		}
	}

	// Token: 0x06000E88 RID: 3720 RVA: 0x000A90E8 File Offset: 0x000A72E8
	public void OnGrenadeExplodedNear(float distance)
	{
		this.Contusion(distance);
	}

	// Token: 0x06000E89 RID: 3721 RVA: 0x000A90F4 File Offset: 0x000A72F4
	public void Contusion(float time)
	{
		if (this._lowpassFilter != null)
		{
			this._contusionFadeTime = 100f / Mathf.Max(Mathf.Pow(time, 2f), 25f);
			this._contusionTimer = Time.realtimeSinceStartup + this._contusionFadeTime;
			this._lowpassFilter.enabled = true;
			this._lowpassFilter.cutoffFrequency = 1000f;
			if (Peer.HardcoreMode)
			{
				ClientMoveController.MouseSensitivityMult = 1f / this._contusionFadeTime;
			}
		}
	}

	// Token: 0x06000E8A RID: 3722 RVA: 0x000A917C File Offset: 0x000A737C
	[Obfuscation(Exclude = true)]
	protected override void SpawnPlayerObject(Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
		this.DespawnPlayerObject();
		if (this._entity)
		{
			this._entity.CancelInvoke();
		}
		if (this._playerMainCamera != null)
		{
			PlayerMainCameraManager.OffCamera();
			this._playerMainCamera = null;
		}
		base.StopCoroutine("TacticalExplosionCamera");
		if (this.ExplosionCamera)
		{
			SingletoneForm<PoolManager>.Instance[this.ExplosionCamera.name].Despawn(this.ExplosionCamera.GetComponent<PoolItem>());
			this.ExplosionCamera = null;
		}
		if (base.IsBear)
		{
			base.PlayerObject = SingletoneForm<PoolManager>.Instance["client_bearHands"].Spawn();
		}
		else
		{
			base.PlayerObject = SingletoneForm<PoolManager>.Instance["client_usecHands"].Spawn();
		}
		this.PlayerTransform.parent = base.transform;
		Transform transform = base.PlayerObject.transform.FindChild("proxy/hands/Arms");
		if (transform)
		{
			foreach (Material material in transform.GetComponent<SkinnedMeshRenderer>().materials)
			{
				if (material.shader.name.ToLower().Contains("bumped specular mask"))
				{
					MasteringMod modById = ModsStorage.Instance().GetModById(Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex]);
					material.SetTexture("_DetailTex", (modById == null) ? null : modById.Texture);
				}
			}
		}
		this._playerMainCamera = PlayerMainCameraManager.GetCamera();
		Utility.ChangeParent(this._playerMainCamera.transform, this.PlayerTransform.FindChild("proxy/hands/root/Camera_root/Camera_root_Animated/Head_Camera"));
		this._playerMainCamera.transform.localPosition = Vector3.zero;
		this._playerMainCamera.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
		if (base.GetType() == typeof(ClientNetPlayer))
		{
			PlayerClass playerClass = Main.UserInfo.playerClass;
			if (playerClass == PlayerClass.storm_trooper && this.playerInfo.clanSkillUnlocked(Cl_Skills.cl_storm2))
			{
				Thermal.AddTo(this._playerMainCamera);
				Thermal.ForceSwitchOff();
			}
			if (playerClass == PlayerClass.scout && this.playerInfo.clanSkillUnlocked(Cl_Skills.cl_scout2))
			{
				Binocular.AddTo(this._playerMainCamera);
				Binocular.HighLight = this.playerInfo.clanSkillUnlocked(Cl_Skills.cl_scout3);
				if (base.Ammo != null)
				{
					base.Ammo.CanUseBinocular = true;
				}
			}
			RainController rainController = UnityEngine.Object.FindObjectOfType(typeof(RainController)) as RainController;
			if (rainController != null)
			{
				rainController.Init(this._playerMainCamera.transform);
			}
		}
		this.mainCamera = this._playerMainCamera.camera;
		if (BIT.AND(this.mainCamera.cullingMask, 1 << LayerMask.NameToLayer("AWH")))
		{
			this.mainCamera.cullingMask ^= 1 << LayerMask.NameToLayer("AWH");
		}
		if (this._lowpassFilter == null)
		{
			this._lowpassFilter = this._playerMainCamera.gameObject.AddComponent<AudioLowPassFilter>();
			this._lowpassFilter.enabled = false;
		}
		else
		{
			this._lowpassFilter.enabled = false;
		}
		CameraListener.ChangeTo(this.mainCamera.gameObject);
		this.userInfo = Main.UserInfo;
		this.playerInfo.playerClass = this.userInfo.playerClass;
		this.animations = base.PlayerObject.GetComponent<Animations>();
		this.animations.Init();
		if (secondaryIndex != 127)
		{
			PrefabFactory.GenerateHandsAnimationWithoutCreating(secondaryIndex, this.animations.handsAnimation);
		}
		if (primaryIndex != 127)
		{
			PrefabFactory.GenerateHandsAnimationWithoutCreating(primaryIndex, this.animations.handsAnimation);
		}
		this.CameraAnimations = SingletoneComponent<CameraAnimations>.Instance;
		this.CameraAnimations.Initialize(this.PlayerTransform);
		this.Prediction.Clear();
		base.SpawnPlayerObject(pos, euler, secondaryIndex, primaryIndex, secondaryMod, primaryMod, secondary_repair_info, primary_repair_info, secondaryMods, primaryMods, weaponKit);
		Forms.OnSpawn();
		this.SyncRequest();
		if (base.GetType() == typeof(ClientNetPlayer))
		{
			if (SingletoneForm<LevelSettings>.Instance.PlayerCameraFarClipPlane != 0f)
			{
				this.mainCamera.farClipPlane = SingletoneForm<LevelSettings>.Instance.PlayerCameraFarClipPlane;
			}
			if (SingletoneForm<LevelSettings>.Instance.PlayerCameraNearClipPlane != 0f)
			{
				this.mainCamera.nearClipPlane = SingletoneForm<LevelSettings>.Instance.PlayerCameraNearClipPlane;
			}
			this.ReplaceWeaponShaders();
			base.Invoke("ReplaceWeaponShaders", 1f);
			global::Console.AddCommand("fawhdebug", delegate
			{
				this._playerMainCamera.SetActive(!this._playerMainCamera.activeSelf);
			});
		}
	}

	// Token: 0x06000E8B RID: 3723 RVA: 0x000A964C File Offset: 0x000A784C
	[Obfuscation(Exclude = true)]
	private void ReplaceWeaponShaders()
	{
		StartData.WeaponShaders.Replace replace = new StartData.WeaponShaders.Replace();
		replace.Init();
	}

	// Token: 0x06000E8C RID: 3724 RVA: 0x000A9668 File Offset: 0x000A7868
	public void ChooseTeam(PlayerType type)
	{
		if (ClientLeagueSystem.IsLeagueGame)
		{
			return;
		}
		this.IsTeamChoosed = true;
		base.ToServer("ChooseTeamFromClient", new object[]
		{
			(int)type
		});
	}

	// Token: 0x06000E8D RID: 3725 RVA: 0x000A96A4 File Offset: 0x000A78A4
	public void PlayTDRadio(string name, PoolItem poolItem)
	{
		if (poolItem != null)
		{
			for (int i = 0; i < SingletoneForm<SoundFactory>.Instance.targetDestignation.Length; i++)
			{
				if (SingletoneForm<SoundFactory>.Instance.targetDestignation[i].name.Contains(name))
				{
					Audio.PlayTyped(poolItem, SoundType.radio, SingletoneForm<SoundFactory>.Instance.targetDestignation[i], true, 0f, 1000000f);
					return;
				}
			}
		}
		Debug.LogWarning("Missing sound: " + name);
	}

	// Token: 0x06000E8E RID: 3726 RVA: 0x000A9728 File Offset: 0x000A7928
	public void PlayChatRadio(string name, PoolItem poolItem)
	{
		if (poolItem != null)
		{
			for (int i = 0; i < SingletoneForm<SoundFactory>.Instance.chatRadio.Length; i++)
			{
				if (SingletoneForm<SoundFactory>.Instance.chatRadio[i].name.Contains(name))
				{
					Audio.PlayTyped(poolItem, SoundType.radio, SingletoneForm<SoundFactory>.Instance.chatRadio[i], true, 0f, 1000000f);
					return;
				}
			}
		}
		Debug.LogWarning("Missing sound: " + name);
	}

	// Token: 0x06000E8F RID: 3727 RVA: 0x000A97AC File Offset: 0x000A79AC
	public void PlayTERadio(string name, PoolItem poolItem)
	{
		if (poolItem != null)
		{
			for (int i = 0; i < SingletoneForm<SoundFactory>.Instance.TeamElliminationSounds.Length; i++)
			{
				if (SingletoneForm<SoundFactory>.Instance.TeamElliminationSounds[i].name.Contains(name))
				{
					Audio.PlayTyped(poolItem, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TeamElliminationSounds[i], true, 0f, 1000000f);
					return;
				}
			}
		}
		Debug.LogWarning("Missing sound: " + name);
	}

	// Token: 0x06000E90 RID: 3728 RVA: 0x000A9830 File Offset: 0x000A7A30
	public void PlayTCRadio(string name, PoolItem poolItem)
	{
		if (poolItem != null)
		{
			for (int i = 0; i < SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds.Length; i++)
			{
				if (SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[i].name.Contains(name))
				{
					Audio.PlayTyped(poolItem, SoundType.radio, SingletoneForm<SoundFactory>.Instance.TacticalConquestSounds[i], true, 0f, 1000000f);
					return;
				}
			}
		}
		Debug.LogWarning("Missing sound: " + name);
	}

	// Token: 0x06000E91 RID: 3729 RVA: 0x000A98B4 File Offset: 0x000A7AB4
	public void PlayRadio(AudioClip clip, PoolItem poolItem)
	{
		Audio.PlayTyped(poolItem, SoundType.radio, clip, true, 0f, 10000f);
	}

	// Token: 0x06000E92 RID: 3730 RVA: 0x000A98CC File Offset: 0x000A7ACC
	public virtual void ChooseAmmunition()
	{
		int currentSuitIndex = MainGUI.Instance.CurrentSuitIndex;
		int primaryIndex = Main.UserInfo.suits[currentSuitIndex].primaryIndex;
		int secondaryIndex = Main.UserInfo.suits[currentSuitIndex].secondaryIndex;
		Suit suit = MasteringSuitsInfo.Instance.Suits[currentSuitIndex];
		string text = string.Empty;
		if (suit.CurrentWeaponsMods.ContainsKey(primaryIndex))
		{
			Dictionary<ModType, int> mods = suit.CurrentWeaponsMods[primaryIndex].Mods;
			text = Utility.ModsToString(mods);
		}
		string text2 = string.Empty;
		if (suit.CurrentWeaponsMods.ContainsKey(secondaryIndex))
		{
			Dictionary<ModType, int> mods2 = suit.CurrentWeaponsMods[secondaryIndex].Mods;
			text2 = Utility.ModsToString(mods2);
		}
		base.ToServer("ChooseAmmunitionFromClient", new object[]
		{
			currentSuitIndex,
			secondaryIndex,
			primaryIndex,
			Main.UserInfo.suits[currentSuitIndex].secondaryMod,
			Main.UserInfo.suits[currentSuitIndex].primaryMod,
			text2,
			text,
			Main.UserInfo.CurrentMpExp,
			currentSuitIndex
		});
	}

	// Token: 0x06000E93 RID: 3731 RVA: 0x000A9A08 File Offset: 0x000A7C08
	public void AdminCommand(string cmd)
	{
		string[] array = cmd.Split(new char[]
		{
			' '
		});
		try
		{
			if (cmd.Contains("boots"))
			{
				this.boots = Convert.ToSingle(array[1]);
			}
			else if (cmd.Contains("frog"))
			{
				this.frog = Convert.ToSingle(array[1]);
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
		base.ToServer("AdminCommandFromClient", new object[]
		{
			cmd,
			base.UserID
		});
	}

	// Token: 0x06000E94 RID: 3732 RVA: 0x000A9AC0 File Offset: 0x000A7CC0
	public void Quit()
	{
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Quit, Language.Connection, Language.ExittingFromServer, PopupState.information, false, true, string.Empty, string.Empty));
		base.ToServer("QuitFromClient", new object[]
		{
			Language.UserQuited
		});
	}

	// Token: 0x06000E95 RID: 3733 RVA: 0x000A9B10 File Offset: 0x000A7D10
	public void Spawn()
	{
		base.ToServer("SpawnFromClient", new object[0]);
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x000A9B24 File Offset: 0x000A7D24
	public void Spawn(int ID)
	{
		Debug.Log("ClientNetPlayer.Spawn");
		base.ToServer("SpawnFromClientAt", new object[]
		{
			ID
		});
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x000A9B58 File Offset: 0x000A7D58
	public void Chat(string msg, ChatInfo info)
	{
		base.ToServer("ChatFromClient", new object[]
		{
			msg,
			(int)info
		});
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x000A9B78 File Offset: 0x000A7D78
	public void Radio(RadioEnum radio)
	{
		base.ToServer("RadioFromClient", new object[]
		{
			(int)radio
		});
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x000A9B94 File Offset: 0x000A7D94
	protected override void CheckBinoculars()
	{
		if (Main.UserInfo.playerClass == PlayerClass.scout && Main.UserInfo.clanSkillUnlocked(Cl_Skills.cl_scout2) && base.Ammo != null)
		{
			base.Ammo.CanUseBinocular = true;
		}
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x000A9BE0 File Offset: 0x000A7DE0
	public void Sync()
	{
		this._needSync = false;
		if (base.IsAlive && this._entity && this.Prediction.NeedFullUpdate)
		{
			this.ammo.Recover(this.Entity.lastPacket.ammoState);
			if (base.Ammo.cPrimary)
			{
				Main.UserInfo.weaponsStates[base.Ammo.state.primaryIndex].repair_info = base.Ammo.state.primaryState.repair_info;
			}
			if (base.Ammo.cSecondary)
			{
				Main.UserInfo.weaponsStates[base.Ammo.state.secondaryIndex].repair_info = base.Ammo.state.secondaryState.repair_info;
			}
			if (this.playerInfo.hasMortar)
			{
				EventFactory.Call("Armstreak", ArmstreakEnum.mortar);
			}
			else
			{
				EventFactory.Call("ArmstreakUsed", ArmstreakEnum.mortar);
			}
			if (this.playerInfo.hasSonar)
			{
				EventFactory.Call("Armstreak", ArmstreakEnum.sonar);
			}
			else
			{
				EventFactory.Call("ArmstreakUsed", ArmstreakEnum.sonar);
			}
			this.controller.Recover(this._entity.lastPacket.moveState);
			this.UC.Clear();
			this.Prediction.Clear();
		}
		this.SyncFinished();
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000A9D70 File Offset: 0x000A7F70
	public void SyncRequest()
	{
		this._needSync = true;
		base.ToServer("SyncFromClient", new object[0]);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x000A9D8C File Offset: 0x000A7F8C
	public void FullUpdateRequest()
	{
		if (this._fullUpdateRequest)
		{
			return;
		}
		this._fullUpdateRequest = true;
		base.ToServer("FullUpdateRequestFromClient", new object[0]);
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x000A9DC0 File Offset: 0x000A7FC0
	public void MessageVoitingFor(string votefor)
	{
		base.ToServer("VoteFromClient", new object[]
		{
			votefor
		});
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000A9DD8 File Offset: 0x000A7FD8
	public void Suspect(int uid, ReportType type)
	{
		if (ReportSystem.Instance.ClientAddSuspect(uid, base.UserID, type))
		{
			this.SuspectCooldown = (float)CVars.SuspectCooldownTime;
			base.ToServer("SuspectFromClient", new object[]
			{
				uid,
				(int)type
			});
		}
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x000A9E30 File Offset: 0x000A8030
	protected void FullUpdateRequestFinished()
	{
		this._fullUpdateRequest = false;
		base.ToServer("FullUpdateRequestFinishedFromClient", new object[0]);
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x000A9E4C File Offset: 0x000A804C
	protected void SyncFinished()
	{
		base.ToServer("SyncFinishedFromClient", new object[0]);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x000A9E60 File Offset: 0x000A8060
	public void StartLoading()
	{
		this.loadingState = LoadingState.clientLoading;
		base.ToServer("StartLoadingFromClient", new object[0]);
	}

	// Token: 0x06000EA2 RID: 3746 RVA: 0x000A9E7C File Offset: 0x000A807C
	public void FinishedLoading()
	{
		this.loadingState = LoadingState.clientFinishedLoading;
		base.ToServer("FinishedLoadingFromClient", new object[0]);
	}

	// Token: 0x06000EA3 RID: 3747 RVA: 0x000A9E98 File Offset: 0x000A8098
	public bool SkillUnlocked(Skills skill)
	{
		return this.playerInfo.skillUnlocked(skill);
	}

	// Token: 0x06000EA4 RID: 3748 RVA: 0x000A9EA8 File Offset: 0x000A80A8
	public override void CallFixedUpdate()
	{
		if (base.IsAlive)
		{
			if (this.playerInfo.ping >= Globals.I.BadPing)
			{
				EventFactory.Call("ShowPingProblem", null);
			}
			int count = this.UC.Count;
			for (int i = 0; i < count; i++)
			{
				this.UC.AdvanceTick();
				if (Forms.keyboardLock)
				{
					this.PlayerInput.Current.Clear();
				}
				if (!Forms.mouseLock)
				{
					this.PlayerInput.Current.SetKey(UKeyCode.fire, KeyState.released);
				}
				if (this._systemHit)
				{
					this.PlayerInput.Current.SetKey(UKeyCode.system_hit, KeyState.just_down);
					this._systemHit = false;
				}
				eCache.PlayerCmd.Clear();
				eCache.PlayerCmd.halfPing = this.playerInfo.clientHalfPing;
				eCache.PlayerCmd.euler = this.Euler;
				eCache.PlayerCmd.number = ++this.CmdNumber;
				eCache.PlayerCmd.buttons = this.PlayerInput.Current.Save();
				int num = 0;
				foreach (object obj in Enum.GetValues(typeof(AimbotButtons)))
				{
					AimbotButtons key = (AimbotButtons)((int)obj);
					if (Input.GetKey((KeyCode)key))
					{
						eCache.PlayerCmd.CheatButtons |= 1 << num;
					}
					num++;
				}
				this.UC.Push(eCache.PlayerCmd);
				this.playerInfo.number = eCache.PlayerCmd.number;
				this.UInput.Load(eCache.PlayerCmd.buttons);
				base.CallFixedUpdate();
				if (this.ammo.IsAim)
				{
					(this.controller as ClientMoveController).MouseSensitivity = ((!this.controller.state.isWalk) ? 0.6f : 0.3f);
				}
				else
				{
					(this.controller as ClientMoveController).MouseSensitivity = 1f;
				}
				if (this.ammo.state.equiped == WeaponEquipedState.Visor && Binocular.Exist)
				{
					(this.controller as ClientMoveController).MouseSensitivity = Binocular.GetSensitivity();
				}
				if (this.ammo.weaponEquiped)
				{
					if (this.UInput.GetKeyDown(UKeyCode.fire, true) && (this.ammo.CurrentWeapon.Empty || this.ammo.CurrentWeapon.Damaged))
					{
						Audio.Play(this.mainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.noAmmo, false, 10f, 150f);
					}
					if (this.UInput.GetKeyDown(UKeyCode.auto, true))
					{
						Audio.Play(this.mainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.noAmmo, false, 10f, 150f);
						EventFactory.Call("Fire", null);
					}
				}
				eCache.PredictionState.Clear();
				eCache.PredictionState.SetUInput(this.UInput);
				eCache.PredictionState.SetClient(this.playerInfo, base.Ammo.state, base.Controller.state);
				this.Prediction.AddClient(eCache.PredictionState);
				this.PlayerInput.Tick();
			}
		}
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x000AA258 File Offset: 0x000A8458
	public override void CallUpdate()
	{
		if (this._contusionTimer < Time.realtimeSinceStartup && this.IsContused)
		{
			if (this._lowpassFilter.cutoffFrequency > 22000f)
			{
				this._lowpassFilter.enabled = false;
			}
			else
			{
				this._lowpassFilter.cutoffFrequency = 1000f + 2200f * this._contusionFadeTime * (Time.realtimeSinceStartup - this._contusionTimer);
			}
			if (Peer.HardcoreMode && ClientMoveController.MouseSensitivityMult < 1f)
			{
				ClientMoveController.MouseSensitivityMult = 1f / this._contusionFadeTime + 1f / this._contusionFadeTime * (1f + Time.realtimeSinceStartup - this._contusionTimer);
			}
			if (ClientMoveController.MouseSensitivityMult > 1f)
			{
				ClientMoveController.MouseSensitivityMult = 1f;
			}
		}
		if (!this.IsContused && ClientMoveController.MouseSensitivityMult != 1f)
		{
			ClientMoveController.MouseSensitivityMult = 1f;
		}
		if (this.UInput.GetKeyUp(UKeyCode.hideInterface, true) && !this._hideInterface)
		{
			this._hideInterface = true;
			Main.UserInfo.settings.graphics.HideInterface = true;
		}
		else if (this.UInput.GetKeyUp(UKeyCode.hideInterface, true) && this._hideInterface)
		{
			this._hideInterface = false;
			Main.UserInfo.settings.graphics.HideInterface = false;
		}
		this.PlayerInput.Update();
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x000AA3E4 File Offset: 0x000A85E4
	public override void CallLateUpdate()
	{
		if (this.CameraAnimations)
		{
			this.CameraAnimations.ResetTransform();
		}
		base.CallLateUpdate();
		if (this.CameraAnimations)
		{
			this.CameraAnimations.CallLateUpdate();
		}
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x000AA430 File Offset: 0x000A8630
	public override void OnSingleAttack(WeaponNature nature)
	{
		if (CVars.g_hitsShowTime != 0f)
		{
			if (this._capsules == null)
			{
				this._capsules = (CapsuleCollider[])UnityEngine.Object.FindObjectsOfType(typeof(CapsuleCollider));
			}
			for (int i = 0; i < this._capsules.Length; i++)
			{
				if (!(this._capsules[i].GetComponent<DamageX>() == null))
				{
					if (!(this._capsules[i].GetComponent<DamageX>().player == null))
					{
						if (!(this._capsules[i].GetComponent<DamageX>().player is EntityNetPlayer))
						{
							Utility.DrawCapsule(this._capsules[i], this._capsules[i].transform.position, this._capsules[i].transform.rotation, Color.green, CVars.g_hitsShowTime);
						}
					}
				}
			}
		}
	}

	// Token: 0x04000F1D RID: 3869
	private Thermal _thermalVision;

	// Token: 0x04000F1E RID: 3870
	private AudioLowPassFilter _lowpassFilter;

	// Token: 0x04000F1F RID: 3871
	private GameObject _playerMainCamera;

	// Token: 0x04000F20 RID: 3872
	private CircleRandom _hitSoundsRandom;

	// Token: 0x04000F21 RID: 3873
	private CircleRandom _armorHitSoundsRandom;

	// Token: 0x04000F22 RID: 3874
	private EntityNetPlayer _entity;

	// Token: 0x04000F23 RID: 3875
	private int _packetLoss;

	// Token: 0x04000F24 RID: 3876
	private bool _needSync = true;

	// Token: 0x04000F25 RID: 3877
	private bool _hideInterface;

	// Token: 0x04000F26 RID: 3878
	private bool _fullUpdateRequest;

	// Token: 0x04000F27 RID: 3879
	private bool _systemHit;

	// Token: 0x04000F28 RID: 3880
	private float _packetLossTimer;

	// Token: 0x04000F29 RID: 3881
	private float _contusionTimer;

	// Token: 0x04000F2A RID: 3882
	private float _contusionFadeTime = 7f;

	// Token: 0x04000F2B RID: 3883
	private readonly Timer _violTimer = new Timer();

	// Token: 0x04000F2C RID: 3884
	protected int CmdNumber;

	// Token: 0x04000F2D RID: 3885
	public GameObject ExplosionCamera;

	// Token: 0x04000F2E RID: 3886
	public PlayerInput PlayerInput = new PlayerInput();

	// Token: 0x04000F2F RID: 3887
	public Prediction Prediction = new Prediction();

	// Token: 0x04000F30 RID: 3888
	public int PlayerState;

	// Token: 0x04000F31 RID: 3889
	[HideInInspector]
	public int InPoint = -1;

	// Token: 0x04000F32 RID: 3890
	private CapsuleCollider[] _capsules;
}
