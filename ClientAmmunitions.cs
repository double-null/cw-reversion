using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x020001A8 RID: 424
[AddComponentMenu("Scripts/Game/ClientAmmunitions")]
internal class ClientAmmunitions : BaseAmmunitions
{
	// Token: 0x06000E24 RID: 3620 RVA: 0x000A3DC8 File Offset: 0x000A1FC8
	private void DestroyMarker()
	{
		if (this.marker)
		{
			SingletoneForm<PoolManager>.Instance[this.marker.name].Despawn(this.marker.GetComponent<PoolItem>());
			this.marker = null;
		}
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x000A3E14 File Offset: 0x000A2014
	public override void OnPoolDespawn()
	{
		this.DestroyMarker();
		this.Arms_root_Animated = null;
		this.Arms_root_Animated_StartPos = Vector3.zero;
		this.Arms_root_Animated_StartRot = Vector3.zero;
		this.cockshot = Vector3.zero;
		this.cockShotDist = 0f;
		this.prevDelta = 0f;
		this.nowDelta = 0f;
		this.speedDelta = 2f;
		this.prevCamFov = 60f;
		this.nowCamFov = 40f;
		this.speedCamFov = 1f;
		this.prevWeapFov = 40f;
		this.nowWeapFov = 40f;
		this.speedWeapFov = 1f;
		this.layerMask = 0;
		if (this.knife)
		{
			this.knife.renderer.enabled = false;
		}
		this.knife = null;
		this.battleScreen = null;
		if (this.player && this.player.MainCamera)
		{
			this.player.MainCamera.fov = 60f;
		}
		if (this.player && this.player.GetType() == typeof(ClientNetPlayer))
		{
			StartData.WeaponShaders.Replace.SetWeaponAspect(0.66f);
		}
		base.OnPoolDespawn();
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x000A3F68 File Offset: 0x000A2168
	public override void OnPoolSpawn()
	{
		base.OnPoolSpawn();
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x000A3F70 File Offset: 0x000A2170
	protected override void TakeWeapon()
	{
		base.TakeWeapon();
		if (this.cSecondary)
		{
			Utility.ChangeParent(this.cSecondary.transform, this.gunHolderBone.transform);
			this.cSecondary.transform.localPosition = Vector3.zero;
			this.cSecondary.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		}
		if (this.cPrimary)
		{
			Utility.ChangeParent(this.cPrimary.transform, this.gunHolderBone.transform);
			this.cPrimary.transform.localPosition = Vector3.zero;
			this.cPrimary.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
		}
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x000A404C File Offset: 0x000A224C
	[Obfuscation(Exclude = true)]
	public override void Binoculars()
	{
		if (Thermal.Exist)
		{
			Thermal.StartSwitch(!Thermal.On);
		}
		if (base.BreakableGroup || !this.CanUseBinocular)
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
		if (this.useBinocular)
		{
			this.state.equiped = this.state.preSupportEquiped;
			if (this.state.equiped == WeaponEquipedState.none)
			{
				this.state.equiped = WeaponEquipedState.Secondary;
			}
			this.useBinocular = false;
			Binocular.StartSwitch(false);
			this.OutIdle();
		}
		else
		{
			if (this.state.isAim)
			{
				this.player.rightClick.Hide(0.5f, 0f);
				base.ChangeAimMode(false);
				this.G.Invoke("Binoculars", base.CurrentWeapon.AimIdleTime * 1.05f, false);
				return;
			}
			this.IdleOut();
			this.G.Invoke("AfterBinocularsIdleOut", base.CurrentWeapon.IdleOut, false);
		}
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x000A4178 File Offset: 0x000A2378
	[Obfuscation(Exclude = true)]
	protected override void AfterBinocularsIdleOut()
	{
		this.state.preSupportEquiped = this.state.equiped;
		this.state.equiped = WeaponEquipedState.Visor;
		this.useBinocular = true;
		Binocular.StartSwitch(true);
		this.OutIdle();
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x000A41B0 File Offset: 0x000A23B0
	public override void Cancel()
	{
		Binocular.StartSwitch(false);
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

	// Token: 0x06000E2B RID: 3627 RVA: 0x000A420C File Offset: 0x000A240C
	public override void Init(BaseNetPlayer player)
	{
		base.Init(player);
		this.knife = Utility.FindHierarchy(base.transform, "knife");
		if (this.knife != null)
		{
			this.knife.renderer.enabled = false;
		}
		if (!this.LOD)
		{
			Utility.ChangeParent(this.knife, this.gunHolderBone);
			base.GetComponent<Animations>().Init();
			this.battleScreen = SingletoneComponent<Main>.Instance.GetComponentInChildren<BattleScreenGUI>();
		}
		if (player.Ammo != null && player.Ammo.CurrentWeapon != null && player.Ammo.CurrentWeapon.Optic && player.Ammo.CurrentWeapon.OpticZoomed)
		{
			player.Ammo.CurrentWeapon.OpticZoomed = false;
			ClientWeapon clientWeapon = player.Ammo.CurrentWeapon as ClientWeapon;
			if (clientWeapon)
			{
				clientWeapon.OpticCamera.fieldOfView *= 2f;
			}
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x000A4324 File Offset: 0x000A2524
	public override void Recover(AmmoState i)
	{
		this.Hide();
		base.Recover(i);
		this.Show();
		if (base.NotLOD && base.weaponEquiped)
		{
			this.player.Animations.Arms.renderer.enabled = true;
			EventFactory.Call("Fire", null);
			if (base.CurrentWeapon.IsModable)
			{
				this.player.Animations.SetPreName(base.CurrentWeapon.type.ToString() + "_cs");
			}
			else
			{
				this.player.Animations.SetPreName(base.CurrentWeapon.type.ToString());
			}
			this.player.Animations.SetWeapon(base.CurrentWeapon.FireAnimations, base.CurrentWeapon.FireAimAnimations);
			base.CurrentWeapon.transform.parent = this.gunHolderBone.transform;
			base.CurrentWeapon.transform.localPosition = Vector3.zero;
			base.CurrentWeapon.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
			this.player.Animations.Init();
			if (base.weaponEquiped)
			{
				if (this.state.isAim && (base.CurrentWeapon.Optic || base.CurrentWeapon.Kolimator))
				{
					this.player.Animations.PlayAimOpticFrame();
				}
				else if (this.state.isAim)
				{
					this.player.Animations.PlayAimFrame();
				}
				else
				{
					this.player.Animations.PlayIdleFrame();
				}
			}
		}
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x000A44F8 File Offset: 0x000A26F8
	public override void SwitchOptic()
	{
		if (this.player.UserID != Main.UserInfo.userID)
		{
			return;
		}
		if (this.player.playerInfo.playerClass != PlayerClass.sniper || !this.player.playerInfo.clanSkillUnlocked(Cl_Skills.cl_sni3))
		{
			return;
		}
		if (!this.player.Ammo.CurrentWeapon.Optic)
		{
			return;
		}
		if (!base.IsAim)
		{
			return;
		}
		ClientWeapon clientWeapon = this.player.Ammo.CurrentWeapon as ClientWeapon;
		if (clientWeapon == null)
		{
			return;
		}
		if (!clientWeapon.OpticZoomed)
		{
			clientWeapon.OpticCamera.fieldOfView = clientWeapon.OpticCamera.fieldOfView / 2f;
			clientWeapon.OpticZoomed = true;
		}
		else
		{
			clientWeapon.OpticCamera.fieldOfView = clientWeapon.OpticCamera.fieldOfView * 2f;
			clientWeapon.OpticZoomed = false;
		}
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.SwitchOpticSound, false, 10f, 150f);
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x000A4628 File Offset: 0x000A2828
	public override void AdditionalMagazineSound()
	{
		if (!Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.IsPrimary)
		{
			return;
		}
		if (this.player.Ammo.CurrentWeapon.state.bagSize <= this.player.Ammo.CurrentWeapon.bagSize - this.player.Ammo.CurrentWeapon.magSize)
		{
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.AdditionalAmmunitionSound, false, 10f, 150f);
		}
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x000A46D0 File Offset: 0x000A28D0
	public override void CallLateUpdate()
	{
		base.CallLateUpdate();
		if (base.IsInvoking("PatronsRendererToggle") && !base.CurrentWeapon.isBelt)
		{
			base.CancelInvoke("PatronsRendererToggle");
		}
		if (this.LOD)
		{
			return;
		}
		if (base.weaponEquiped && this.player.MainCamera)
		{
			if (this.cPrimary && this.cPrimary.correct && base.CurrentWeapon == this.cPrimary)
			{
				if (this.cPrimary.correctAlways)
				{
					this.Arms_root_Animated.localPosition = this.cPrimary.rootPositionCorrect;
					this.Arms_root_Animated.localEulerAngles = this.cPrimary.rootRotationCorrect;
				}
				else
				{
					this.Arms_root_Animated.localPosition = this.cPrimary.rootPositionCorrect * base.AimPos + (1f - base.AimPos) * this.Arms_root_Animated_StartPos;
					this.Arms_root_Animated.localEulerAngles = this.cPrimary.rootRotationCorrect * base.AimPos + (1f - base.AimPos) * this.Arms_root_Animated_StartRot;
				}
			}
			if (this.cSecondary && this.cSecondary.correct && base.CurrentWeapon == this.cSecondary)
			{
				if (this.cSecondary.correctAlways)
				{
					this.Arms_root_Animated.localPosition = this.cSecondary.rootPositionCorrect;
					this.Arms_root_Animated.localEulerAngles = this.cSecondary.rootRotationCorrect;
				}
				else
				{
					this.Arms_root_Animated.localPosition = this.cSecondary.rootPositionCorrect * base.AimPos + (1f - base.AimPos) * this.Arms_root_Animated_StartPos;
					this.Arms_root_Animated.localEulerAngles = this.cSecondary.rootRotationCorrect * base.AimPos + (1f - base.AimPos) * this.Arms_root_Animated_StartRot;
				}
			}
			this.cockShotDist = 10f * Mathf.Tan(base.CurrentWeapon.CurrentAccuracy * 0.017453292f);
			this.cockshot = this.player.MainCamera.transform.position + this.player.MainCamera.transform.forward * 10f + this.player.MainCamera.transform.right * this.cockShotDist;
			this.cockshot = this.player.MainCamera.WorldToScreenPoint(this.cockshot);
			this.cockshot.x = this.cockshot.x / (float)Screen.width - 0.5f;
			this.cockshot.y = this.cockshot.y / (float)Screen.height - 0.5f;
			this.cockshot.z = 0f;
			this.nowDelta = this.cockshot.magnitude;
			if (this.nowDelta < 0.01f)
			{
				this.nowDelta = 0f;
			}
			if (this.prevDelta == -1f)
			{
				this.prevDelta = this.nowDelta;
			}
			this.prevDelta += this.speedDelta * (this.nowDelta - this.prevDelta) * Time.deltaTime * 4f;
			this.battleScreen.delta.x = this.prevDelta;
			this.battleScreen.delta.y = this.prevDelta;
			this.battleScreen.accuracy = Mathf.Max(1f - this.prevDelta * 10f, 0f);
			if (this.state.isAim || base.CurrentWeapon.weaponSpecific == WeaponSpecific.sniper)
			{
				this.battleScreen.accuracy = 0f;
			}
			if (base.CurrentWeapon.stab_now.Enabled)
			{
				this.battleScreen.stab_alpha.Show(0.5f, 0f);
			}
			else
			{
				this.battleScreen.stab_alpha.Hide(0.5f, 0f);
			}
			if (this.G.IsInvoking("AfterAimIdle"))
			{
				this.nowCamFov = 60f - base.CurrentWeapon.worldAimFov + base.CurrentWeapon.worldAimFov * this.aimGNAME.Get();
				this.nowWeapFov = base.CurrentWeapon.AimFov + (40f - base.CurrentWeapon.AimFov) * this.aimWeapGNAME.Get();
			}
			else if (this.G.IsInvoking("AfterIdleAim"))
			{
				this.nowCamFov = 60f - base.CurrentWeapon.worldAimFov * this.aimGNAME.Get();
				this.nowWeapFov = 40f - (40f - base.CurrentWeapon.AimFov) * this.aimWeapGNAME.Get();
			}
			else if (this.state.isAim)
			{
				this.nowCamFov = 60f - base.CurrentWeapon.worldAimFov;
				this.nowWeapFov = base.CurrentWeapon.AimFov;
			}
			else
			{
				this.nowCamFov = 60f;
				this.nowWeapFov = 40f;
			}
			this.prevCamFov += this.speedCamFov * (this.nowCamFov - this.prevCamFov) * Time.deltaTime * 10f;
			this.prevWeapFov += this.speedWeapFov * (this.nowWeapFov - this.prevWeapFov) * Time.deltaTime * 40f;
			float num = Mathf.Clamp(this.player.rightClick.visibility_clean - 0.5f, 0f, 0.5f) * 10f;
			this.player.MainCamera.fov = this.prevCamFov - num;
			StartData.WeaponShaders.Replace.SetWeaponAspect((this.nowWeapFov - num * 0.2f) * 0.0166f);
		}
		else if (this.player.MainCamera)
		{
			if (this.state.equiped == WeaponEquipedState.Visor && Binocular.Exist)
			{
				this.nowCamFov = Binocular.Instance.FOV;
				this.nowWeapFov = this.nowCamFov - 20f;
			}
			this.prevCamFov += this.speedCamFov * (this.nowCamFov - this.prevCamFov) * Time.deltaTime * 10f;
			this.prevWeapFov += this.speedWeapFov * (this.nowWeapFov - this.prevWeapFov) * Time.deltaTime * 10f;
			this.player.MainCamera.fov = this.prevCamFov - 5f * Mathf.Min(Mathf.Max(this.player.rightClick.visibility_clean - 0.5f, 0f), 0.5f) / 0.5f;
			StartData.WeaponShaders.Replace.SetWeaponAspect(this.nowWeapFov * 0.0166f - 5f * Mathf.Min(Mathf.Max(this.player.rightClick.visibility_clean - 0.5f, 0f), 0.5f) / 0.5f);
		}
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x000A4E8C File Offset: 0x000A308C
	protected override void SingleAttack(WeaponNature nature, BaseWeapon weapon = null)
	{
		if (this.useBinocular)
		{
			return;
		}
		base.SingleAttack(nature, weapon);
		bool flag = nature == WeaponNature.knife;
		Vector3 position = Peer.ClientGame.LocalPlayer.Position;
		this.player.OnSingleAttack(nature);
		UnityEngine.Random.seed = this.player.Ammo.state.randomSeed;
		float f = 0f;
		if (base.weaponEquiped)
		{
			f = base.CurrentWeapon.CurrentAccuracy * 0.017453292f;
		}
		float num = 1f;
		if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun && Utility.IsModableWeapon((int)base.CurrentWeapon.type))
		{
			num = ((!base.CurrentWeapon.Slug) ? base.CurrentWeapon.ShotGroupingProc : 1f);
		}
		Vector3 vector = new Vector3(UnityEngine.Random.insideUnitCircle.x * Mathf.Sin(f) * num, UnityEngine.Random.insideUnitCircle.y * Mathf.Sin(f) * num, Mathf.Cos(f));
		Ray ray = new Ray(this.player.MainCamera.transform.position, Quaternion.Euler(this.player.Euler.x + 90f, this.player.Euler.y, this.player.Euler.z) * vector.normalized);
		if (this.LOD)
		{
			if (!flag)
			{
				Vector3 lineEnd = ray.origin + ray.direction * 100f;
				Vector3 vector2 = Utility.ProjectPointLine(position, ray.origin, lineEnd);
				float magnitude = (vector2 - position).magnitude;
				if (magnitude < 1.5f)
				{
					AudioSource audioSource = Audio.Play(vector2, SingletoneForm<SoundFactory>.Instance.whizClips[(int)((float)SingletoneForm<SoundFactory>.Instance.whizClips.Length * UnityEngine.Random.value)], -1f, -1f);
					audioSource.minDistance = 0.3f;
					audioSource.maxDistance = 2f;
				}
			}
		}
		else
		{
			BattleScreenGUI.I.Fire(null);
		}
		float distance = 1000f;
		if (flag)
		{
			distance = 1.5f;
		}
		if (this.layerMask == 0)
		{
			this.layerMask = (1 << LayerMask.NameToLayer("client_ragdoll") | 1 << LayerMask.NameToLayer("Water") | PhysicsUtility.level_layers);
		}
		RaycastHit[] array = Physics.RaycastAll(ray, distance, this.layerMask);
		if (array.Length == 0)
		{
			return;
		}
		StaticUtils.RaycastHitSort(array);
		if (flag && base.NotLOD)
		{
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.knifeSounds[1], false, 10f, 150f);
		}
		float num2;
		if (flag)
		{
			num2 = 1000f;
		}
		else
		{
			num2 = base.CurrentWeapon.damage + this.ApAmmo;
			if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun && !base.CurrentWeapon.Slug)
			{
				num2 = base.CurrentWeapon.damage / (float)base.CurrentWeapon.caseshot + this.ApAmmo;
			}
			else if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun && base.CurrentWeapon.Slug)
			{
				num2 = base.CurrentWeapon.damage + this.ApAmmo;
			}
		}
		RaycastHit raycastHit = default(RaycastHit);
		for (int i = 0; i < array.Length; i++)
		{
			RaycastHit raycastHit2 = array[i];
			Transform transform = raycastHit2.transform;
			string name = raycastHit2.collider.material.name;
			DamageX component = transform.GetComponent<DamageX>();
			bool flag2 = name.Contains("blood");
			if (!component || !(component.player == this.player))
			{
				if (Vector3.Distance(position, raycastHit2.point) < 2f && (name.Contains("concrete") || name.Contains("soil") || name.Contains("wood")))
				{
					Vector3 vector3 = CameraListener.Camera.WorldToScreenPoint(raycastHit2.point);
					if (vector3.z >= 0f && vector3.y < (float)Screen.height && vector3.y > 0f && vector3.x < (float)Screen.width && vector3.x > 0f)
					{
						BattleScreenGUI.DirtToFace();
					}
				}
				if (flag2)
				{
					if (i < array.Length - 1 && !flag)
					{
						bool flag3 = false;
						for (int j = i + 1; j < array.Length; j++)
						{
							if (!array[j].collider.material.name.Contains("blood"))
							{
								DecalsAndTracersData.CreateBody(raycastHit2, array[j]);
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							DecalsAndTracersData.CreateBody(raycastHit2);
						}
					}
					else
					{
						DecalsAndTracersData.CreateBody(raycastHit2);
					}
				}
				else
				{
					DecalsAndTracersData.Create(raycastHit2, flag);
				}
				if (flag2)
				{
					raycastHit2.rigidbody.AddForce(-raycastHit2.normal * 50f, ForceMode.Impulse);
					raycastHit = raycastHit2;
					break;
				}
				float num3 = (!flag) ? base.CurrentWeapon.PierceReducer : 1f;
				string tag = transform.tag;
				if (tag.Contains("piercesAll"))
				{
					num2 -= 0f * num3;
				}
				if (tag.Contains("piercesLight"))
				{
					num2 -= 20f * num3;
				}
				if (tag.Contains("piercesMedium"))
				{
					num2 -= 30f * num3;
				}
				if (tag.Contains("piercesHeavy"))
				{
					num2 -= 40f * num3;
				}
				if (tag.Contains("Untagged"))
				{
					num2 -= 10000f;
				}
				if (num2 <= 0f)
				{
					raycastHit = raycastHit2;
					break;
				}
				raycastHit = raycastHit2;
			}
		}
		if (!flag)
		{
			byte alpha = byte.MaxValue;
			if (Peer.Info.Hardcore)
			{
				alpha = 153;
			}
			if (this.player.playerInfo.skillUnlocked(Skills.longb_mg))
			{
				alpha = 178;
			}
			bool doubleSpeed = this.player.ID == Peer.ClientGame.LocalPlayer.ID && Peer.ClientGame.LocalPlayer.Ammo.CurrentWeapon.Optic && Peer.ClientGame.LocalPlayer.Ammo.IsAim;
			Tracer.Create((base.CurrentWeapon as ClientWeapon).BulletHolder.position, raycastHit.point, alpha, doubleSpeed);
		}
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x000A55D8 File Offset: 0x000A37D8
	[Obfuscation(Exclude = true)]
	protected override void IdleOut()
	{
		base.IdleOut();
		if (base.NotLOD)
		{
			if (base.weaponEquiped)
			{
				this.player.Animations.PlayIdleOut(base.CurrentWeapon.IdleOut);
				base.CurrentWeapon.PlayIdleOut();
			}
			else
			{
				this.player.Animations.SupportIdleOut(CVars.pl_marker_idle_to_out);
			}
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x000A5644 File Offset: 0x000A3844
	[Obfuscation(Exclude = true)]
	protected override void OutIdle()
	{
		this.knife.renderer.enabled = false;
		if (base.weaponEquiped)
		{
			this.Hide();
			this.Show();
			base.CurrentWeapon.PlayOutIdle();
			this.G.Invoke("PlayEquipSound", base.CurrentWeapon.equipTime, false);
		}
		if (base.NotLOD)
		{
			this.player.Animations.Arms.renderer.enabled = true;
			if (base.weaponEquiped)
			{
				this.DestroyMarker();
				if (base.CurrentWeapon.IsModable)
				{
					this.player.Animations.SetPreName(base.CurrentWeapon.type.ToString() + "_cs");
				}
				else
				{
					this.player.Animations.SetPreName(base.CurrentWeapon.type.ToString());
				}
				this.player.Animations.SetWeapon(base.CurrentWeapon.FireAnimations, base.CurrentWeapon.FireAimAnimations);
				this.player.Animations.PlayOutIdle(base.CurrentWeapon.OutIdle);
			}
			else if (this.state.equiped == WeaponEquipedState.Marker)
			{
				this.player.Animations.SupportOutIdle(CVars.pl_marker_out_to_idle);
			}
			if (base.CurrentWeapon.isBelt && base.CurrentWeapon.state.clips <= base.CurrentWeapon.hideAmmoCount)
			{
				this.PatronsRendererToggle(false, base.CurrentWeapon.state.clips);
			}
		}
		base.OutIdle();
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x000A57FC File Offset: 0x000A39FC
	[Obfuscation(Exclude = true)]
	protected override void AfterOutIdle()
	{
		base.AfterOutIdle();
		if (base.NotLOD && base.weaponEquiped)
		{
			this.player.Animations.PlayIdleFrame();
		}
	}

	// Token: 0x06000E34 RID: 3636 RVA: 0x000A5838 File Offset: 0x000A3A38
	[Obfuscation(Exclude = true)]
	protected override void AfterGrenade()
	{
		if (base.NotLOD)
		{
			this.player.Animations.Arms.renderer.enabled = true;
			if (this.player.playerInfo.hasSonar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			}
			else if (this.player.playerInfo.hasMortar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			}
		}
		base.AfterGrenade();
	}

	// Token: 0x06000E35 RID: 3637 RVA: 0x000A58EC File Offset: 0x000A3AEC
	[Obfuscation(Exclude = true)]
	protected override void GrenadeLaunch()
	{
		if (this.LOD)
		{
			return;
		}
		Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.changeClips[4], -1f, -1f);
		EventFactory.Call("Grenade", null);
	}

	// Token: 0x06000E36 RID: 3638 RVA: 0x000A5938 File Offset: 0x000A3B38
	public override void Knife()
	{
		if (base.BreakableGroup || this.state.supportReady || this.useBinocular)
		{
			return;
		}
		base.Knife();
		this.knife.renderer.enabled = true;
		if (base.NotLOD)
		{
			if (base.weaponEquiped)
			{
				base.CurrentWeapon.PlayIdle();
			}
			Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.knifeSounds[0], -1f, -1f);
			this.Hide();
			this.player.Animations.Knife(0.9f);
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.knife_hit);
			EntityNetPlayer entityNetPlayer = this.player as EntityNetPlayer;
			if (entityNetPlayer != null && entityNetPlayer.isPlayer)
			{
				return;
			}
			if (this.player.playerInfo.hasSonar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			}
			else if (this.player.playerInfo.hasMortar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
			}
		}
	}

	// Token: 0x06000E37 RID: 3639 RVA: 0x000A5A9C File Offset: 0x000A3C9C
	[Obfuscation(Exclude = true)]
	protected override void AfterKnife()
	{
		this.OutIdle();
		this.knife.renderer.enabled = false;
		if (base.NotLOD)
		{
			if (this.player.playerInfo.hasSonar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			}
			else if (this.player.playerInfo.hasMortar && this.marker)
			{
				this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
			}
		}
	}

	// Token: 0x06000E38 RID: 3640 RVA: 0x000A5B48 File Offset: 0x000A3D48
	[Obfuscation(Exclude = true)]
	protected override void SupportOutIdle()
	{
		base.SupportOutIdle();
		if (base.NotLOD)
		{
			this.DestroyMarker();
			this.marker = SingletoneForm<PoolManager>.Instance["client_marker"].Spawn();
			StartData.weaponShaders.ReplaceEntity(this.marker);
			this.marker.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.layer = LayerMask.NameToLayer("hands_render");
			Utility.ChangeParent(this.marker.transform, this.gunHolderBone);
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[5], false, 10f, 150f);
		}
		this.Hide();
	}

	// Token: 0x06000E39 RID: 3641 RVA: 0x000A5C04 File Offset: 0x000A3E04
	[Obfuscation(Exclude = true)]
	protected override void SupportLight()
	{
		if (base.NotLOD)
		{
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[6], false, 10f, 150f);
			if (this.player.playerInfo.hasSonar && this.marker && this.lastSupport == ArmstreakEnum.sonar)
			{
				this.marker.transform.FindChild("marker_green").renderer.enabled = true;
				this.marker.transform.FindChild("marker_green").gameObject.layer = LayerMask.NameToLayer("hands_render");
			}
			else if (this.player.playerInfo.hasMortar && this.marker)
			{
				this.marker.transform.FindChild("marker_red").renderer.enabled = true;
				this.marker.transform.FindChild("marker_red").gameObject.layer = LayerMask.NameToLayer("hands_render");
			}
		}
	}

	// Token: 0x06000E3A RID: 3642 RVA: 0x000A5D3C File Offset: 0x000A3F3C
	public override void SupportAim()
	{
		if (this.state.supportReady || this.marker == null)
		{
			return;
		}
		base.SupportAim();
		if (!this.LOD)
		{
			this.marker.animation.Play();
			this.player.Animations.ChargeSupport();
		}
	}

	// Token: 0x06000E3B RID: 3643 RVA: 0x000A5DA0 File Offset: 0x000A3FA0
	[Obfuscation(Exclude = true)]
	protected override void AfterSupport()
	{
		if (this.state.equiped != WeaponEquipedState.Primary || this.state.equiped != WeaponEquipedState.Secondary)
		{
			this.state.equiped = this.state.preSupportEquiped;
		}
		if (this.state.equiped == WeaponEquipedState.none)
		{
			this.state.equiped = WeaponEquipedState.Secondary;
		}
		this.Show();
		if (base.CurrentWeapon.isBelt && base.CurrentWeapon.state.clips <= base.CurrentWeapon.hideAmmoCount && base.NotLOD)
		{
			this.PatronsRendererToggle(false, base.CurrentWeapon.state.clips);
		}
	}

	// Token: 0x06000E3C RID: 3644 RVA: 0x000A5E5C File Offset: 0x000A405C
	public virtual void Hide()
	{
		if (this.cPrimary)
		{
			(this.cPrimary as ClientWeapon).Hide();
		}
		if (this.cSecondary)
		{
			(this.cSecondary as ClientWeapon).Hide();
		}
	}

	// Token: 0x06000E3D RID: 3645 RVA: 0x000A5EAC File Offset: 0x000A40AC
	public virtual void Show()
	{
		if (this.state.equiped == WeaponEquipedState.Secondary && this.cSecondary != null)
		{
			(this.cSecondary as ClientWeapon).Show();
		}
		if (this.state.equiped == WeaponEquipedState.Primary && this.cPrimary != null)
		{
			(this.cPrimary as ClientWeapon).Show();
		}
	}

	// Token: 0x06000E3E RID: 3646 RVA: 0x000A5F20 File Offset: 0x000A4120
	[Obfuscation(Exclude = true)]
	protected void PlayEquipSound()
	{
		if (base.NotLOD)
		{
			if (base.CurrentWeapon != null && base.CurrentWeapon.equip != null)
			{
				Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), base.CurrentWeapon.equip, false, 10f, 150f);
			}
			else
			{
				if (this.state.equiped == WeaponEquipedState.Secondary)
				{
					Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[1], false, 10f, 150f);
				}
				if (this.state.equiped == WeaponEquipedState.Primary)
				{
					Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[2], false, 10f, 150f);
				}
			}
		}
	}

	// Token: 0x06000E3F RID: 3647 RVA: 0x000A6018 File Offset: 0x000A4218
	protected override void PrepareWeapon()
	{
		if (!this.LOD)
		{
			this.gunHolderBone = Utility.FindHierarchy(base.transform, "Arms_Weapons_Bone").transform;
			this.Arms_root_Animated = Utility.FindHierarchy(base.transform, "Arms_root_Animated").transform;
			this.Arms_root_Animated_StartPos = this.Arms_root_Animated.transform.localPosition;
			this.Arms_root_Animated_StartRot = this.Arms_root_Animated.transform.localEulerAngles;
		}
		else
		{
			this.gunHolderBone = base.transform.parent.GetComponentInChildren<WeaponHolder>().transform;
		}
		this.PrepareSecondary();
		this.PreparePrimary();
	}

	// Token: 0x06000E40 RID: 3648 RVA: 0x000A60C0 File Offset: 0x000A42C0
	private void PreparePrimary()
	{
		if (this.state.primaryIndex == 127)
		{
			return;
		}
		GameObject gameObject = PoolManager.Spawn("client_weapon");
		this.cPrimary = gameObject.GetComponent<ClientWeapon>();
		this.cPrimary.state.isMod = this.state.primaryMod;
		this.cPrimary.state.Mods = this.state.primaryState.Mods;
		if (base.NotLOD)
		{
			this.cPrimary.Copy(PrefabFactory.GenerateWeaponWithoutCreating(this.state.primaryIndex).GetComponent<BaseWeapon>());
		}
		else
		{
			this.cPrimary.Copy(PrefabFactory.GenerateLodPrimaryWeaponWithoutCreating(this.state.primaryIndex).GetComponent<BaseWeapon>());
		}
		if (Utility.IsModableWeapon(this.state.primaryIndex))
		{
			Dictionary<ModType, int> dictionary;
			if (Main.IsGameLoaded)
			{
				dictionary = Utility.StringToMods(this.state.primaryState.Mods);
			}
			else
			{
				dictionary = MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex.Value].CurrentWeaponsMods[this.state.primaryIndex].Mods;
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
				{
					if (keyValuePair.Value != 0)
					{
						MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
						if (modById != null && !modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
						{
							string modPrefabName = Utility.GetModPrefabName(modById, WeaponPrefabType.Weapon);
							string modPrefabName2 = Utility.GetModPrefabName(modById, WeaponPrefabType.Lod);
							if (base.NotLOD)
							{
								if (PrefabFactory.IsHolderExist(modPrefabName))
								{
									modById.Prefab = PrefabFactory.GenerateModWithoutCreating(modById);
								}
								else
								{
									global::Console.WriteLine("Primary -- Mod Holder not exist: " + modPrefabName, new Color?(Color.red));
								}
							}
							else if (PrefabFactory.IsHolderExist(modPrefabName2))
							{
								modById.Prefab = PrefabFactory.GenerateLodModWithoutCreating(modById);
							}
						}
					}
				}
			}
		}
		this.cPrimary.Init(this.player, this.state.primaryIndex);
		this.cPrimary.AfterInit(this.state.primaryState.repair_info);
	}

	// Token: 0x06000E41 RID: 3649 RVA: 0x000A634C File Offset: 0x000A454C
	private void PrepareSecondary()
	{
		GameObject gameObject = PoolManager.Spawn("client_weapon");
		Utility.ChangeParent(gameObject.transform, base.transform);
		this.cSecondary = gameObject.GetComponent<ClientWeapon>();
		this.cSecondary.state.isMod = this.state.secondaryMod;
		this.cSecondary.state.Mods = this.state.secondaryState.Mods;
		this.cSecondary.Copy(this.LOD ? PrefabFactory.GenerateLodSecondaryWeaponWithoutCreating(this.state.secondaryIndex).GetComponent<BaseWeapon>() : PrefabFactory.GenerateWeaponWithoutCreating(this.state.secondaryIndex).GetComponent<BaseWeapon>());
		if (Utility.IsModableWeapon(this.state.secondaryIndex))
		{
			Dictionary<ModType, int> dictionary = (!Main.IsGameLoaded) ? MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex].CurrentWeaponsMods[this.state.secondaryIndex].Mods : Utility.StringToMods(this.state.secondaryState.Mods);
			if (dictionary != null)
			{
				foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
				{
					if (keyValuePair.Value != 0)
					{
						MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
						if (modById != null && !modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
						{
							string modPrefabName = Utility.GetModPrefabName(modById, WeaponPrefabType.Weapon);
							string modPrefabName2 = Utility.GetModPrefabName(modById, WeaponPrefabType.Lod);
							if (!this.LOD)
							{
								if (PrefabFactory.IsHolderExist(modPrefabName))
								{
									modById.Prefab = PrefabFactory.GenerateModWithoutCreating(modById);
								}
								else
								{
									global::Console.WriteLine("Secondary -- Mod Holder not exist: " + modPrefabName, new Color?(Color.red));
								}
							}
							else if (PrefabFactory.IsHolderExist(modPrefabName2))
							{
								modById.Prefab = PrefabFactory.GenerateLodModWithoutCreating(modById);
							}
						}
					}
				}
			}
		}
		this.cSecondary.Init(this.player, this.state.secondaryIndex);
		this.cSecondary.AfterInit(this.state.secondaryState.repair_info);
	}

	// Token: 0x06000E42 RID: 3650 RVA: 0x000A65CC File Offset: 0x000A47CC
	protected override void OnFire()
	{
		if (this.useBinocular)
		{
			return;
		}
		if (base.CurrentWeapon != null && !(base.CurrentWeapon as ClientWeapon).Visible)
		{
			return;
		}
		this.FireAnimationHands();
		if (base.CurrentWeapon.pompovik)
		{
			base.Invoke("ShellAnimation", 0.5f);
			return;
		}
		if (!base.CurrentWeapon.isBolt)
		{
			this.ShellAnimation();
		}
		else if (!this.player.Ammo.IsAim)
		{
			base.Invoke("ShellAnimation", 1f);
		}
		if (base.CurrentWeapon.isBelt && base.CurrentWeapon.state.clips <= base.CurrentWeapon.hideAmmoCount && !this.LOD)
		{
			this.HidePatron(base.CurrentWeapon.state.clips);
		}
		if (base.CurrentWeapon.IsAbakan && base.CurrentWeapon.state.clips > 1 && (base.CurrentWeapon.AbakanFirstShot || base.CurrentWeapon.state.singleShot))
		{
			base.Invoke("ShellAnimation", 0.05f);
		}
	}

	// Token: 0x06000E43 RID: 3651 RVA: 0x000A6720 File Offset: 0x000A4920
	private void HidePatron(int index)
	{
		Transform transform = Utility.FindHierarchy(base.CurrentWeapon.transform, "patron" + index).transform;
		if (!transform)
		{
			return;
		}
		transform.renderer.enabled = false;
	}

	// Token: 0x06000E44 RID: 3652 RVA: 0x000A676C File Offset: 0x000A496C
	[Obfuscation(Exclude = true)]
	private void PatronsRendererToggle(bool enable = true, int startIndex = 0)
	{
		for (int i = startIndex; i <= base.CurrentWeapon.hideAmmoCount; i++)
		{
			Transform transform = Utility.FindHierarchy(base.CurrentWeapon.transform, "patron" + i.ToString()).transform;
			if (transform)
			{
				transform.renderer.enabled = enable;
			}
		}
	}

	// Token: 0x06000E45 RID: 3653 RVA: 0x000A67D8 File Offset: 0x000A49D8
	[Obfuscation(Exclude = true)]
	private void PatronsRendererToggle()
	{
		for (int i = 0; i <= base.CurrentWeapon.hideAmmoCount; i++)
		{
			Transform transform = Utility.FindHierarchy(base.CurrentWeapon.transform, "patron" + i.ToString()).transform;
			if (transform)
			{
				transform.renderer.enabled = true;
			}
		}
	}

	// Token: 0x06000E46 RID: 3654 RVA: 0x000A6844 File Offset: 0x000A4A44
	[Obfuscation(Exclude = true)]
	private void ShellAnimation()
	{
		bool flag = CameraListener.Camera && (CameraListener.Camera.transform.position - this.player.Position).magnitude < 10f;
		if (this.LOD)
		{
			flag = (CameraListener.Camera && (CameraListener.Camera.transform.position - this.player.transform.position).magnitude < 10f);
		}
		if (base.CurrentWeapon.shellName != string.Empty && flag)
		{
			GameObject gameObject = SingletoneForm<PoolManager>.Instance[base.CurrentWeapon.shellName].Spawn();
			if (this.player is ClientNetPlayer)
			{
				gameObject.transform.position = StartData.WeaponShaders.Replace.ModifyVec((base.CurrentWeapon as ClientWeapon).ShellHolder.position);
			}
			else
			{
				gameObject.transform.position = (base.CurrentWeapon as ClientWeapon).ShellHolder.position;
			}
			gameObject.rigidbody.velocity = (base.CurrentWeapon.transform.up * 0.5f + base.CurrentWeapon.transform.right * 0.4f) * (1.5f + UnityEngine.Random.value * 1f);
			gameObject.transform.localRotation = UnityEngine.Random.rotation;
			gameObject.transform.parent = Main.Trash;
			if (this.LOD)
			{
				ShellManager component = gameObject.GetComponent<ShellManager>();
				component.Change();
				component.CancelInvoke("Change");
			}
		}
	}

	// Token: 0x06000E47 RID: 3655 RVA: 0x000A6A18 File Offset: 0x000A4C18
	private void FireAnimationHands()
	{
		if (this.LOD)
		{
			return;
		}
		if (this.state.isAim)
		{
			if (base.CurrentWeapon.Optic || base.CurrentWeapon.Kolimator)
			{
				if (base.CurrentWeapon.duplet)
				{
					this.player.Animations.PlayAimOpticExtraFire();
				}
				else if (!Utility.IsModableWeapon((int)base.CurrentWeapon.type))
				{
					this.player.Animations.PlayAimOpticFire();
				}
				else
				{
					this.player.Animations.PlayFire(null);
				}
			}
			else if (base.CurrentWeapon.duplet)
			{
				this.player.Animations.PlayAimExtraFire();
			}
			else if (Utility.IsModableWeapon((int)base.CurrentWeapon.type))
			{
				this.player.Animations.PlayFire(null);
			}
			else
			{
				this.player.Animations.PlayAimFire();
			}
		}
		else if (base.CurrentWeapon.duplet)
		{
			this.player.Animations.PlayExtraFire();
		}
		else if (base.CurrentWeapon.isBolt && base.CurrentWeapon.IsModable)
		{
			this.player.Animations.PlayFire(new Action<float>((base.CurrentWeapon as ClientWeapon).AnimationPlayBolt));
		}
		else
		{
			this.player.Animations.PlayFire(null);
		}
	}

	// Token: 0x06000E48 RID: 3656 RVA: 0x000A6BAC File Offset: 0x000A4DAC
	protected override void OnFireReload()
	{
		if (this.LOD)
		{
			return;
		}
		if (this.state.isAim)
		{
			if (base.CurrentWeapon.Optic || base.CurrentWeapon.Kolimator || base.CurrentWeapon.isBolt)
			{
				if (base.CurrentWeapon.isBolt)
				{
					if (Utility.IsModableWeapon((int)base.CurrentWeapon.type))
					{
						this.player.Animations.PlayBoltDistort();
					}
					else
					{
						this.player.Animations.PlayAimOpticBolt();
					}
					base.Invoke("ShellAnimation", 0.7f);
				}
				else if (base.CurrentWeapon.duplet)
				{
					this.player.Animations.PlayAimOpticExtraFire();
				}
				else if (Utility.IsModableWeapon((int)base.CurrentWeapon.type))
				{
					this.player.Animations.PlayFire(null);
				}
				else
				{
					this.player.Animations.PlayAimOpticFire();
				}
			}
			else if (base.CurrentWeapon.duplet)
			{
				this.player.Animations.PlayAimExtraFire();
			}
			else if (Utility.IsModableWeapon((int)base.CurrentWeapon.type))
			{
				this.player.Animations.PlayFire(null);
			}
			else
			{
				this.player.Animations.PlayAimFire();
			}
		}
		else if (base.CurrentWeapon.isBolt)
		{
			if (base.CurrentWeapon.IsModable)
			{
				this.player.Animations.PlayBoltDistort();
			}
			else
			{
				this.player.Animations.PlayFireSince(this.BoltDistortStartTime);
			}
		}
		else if (base.CurrentWeapon.duplet)
		{
			this.player.Animations.PlayExtraFire();
		}
		else
		{
			this.player.Animations.PlayFire(null);
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000E49 RID: 3657 RVA: 0x000A6DB4 File Offset: 0x000A4FB4
	private float BoltDistortStartTime
	{
		get
		{
			Weapons type = base.CurrentWeapon.type;
			if (type == Weapons.mosina)
			{
				return 0.126f;
			}
			if (type == Weapons.vihlop)
			{
				return 0.255f;
			}
			if (type != Weapons.awm)
			{
				return 0.124f;
			}
			return 0.3f;
		}
	}

	// Token: 0x06000E4A RID: 3658 RVA: 0x000A6E04 File Offset: 0x000A5004
	protected override void OnFirePlayReloadEnd()
	{
		if (this.LOD)
		{
			return;
		}
		float time = (!base.CurrentWeapon.UseShotGunReload) ? 1f : 2.1f;
		this.player.Animations.PlayReloadEnd(time);
	}

	// Token: 0x06000E4B RID: 3659 RVA: 0x000A6E50 File Offset: 0x000A5050
	public override void UpdateFireInterface()
	{
		if (base.NotLOD)
		{
			EventFactory.Call("Fire", null);
		}
	}

	// Token: 0x06000E4C RID: 3660 RVA: 0x000A6E68 File Offset: 0x000A5068
	protected override void OnPlayReload()
	{
		if (base.CurrentWeapon.isBelt && base.NotLOD)
		{
			base.Invoke("PatronsRendererToggle", base.CurrentWeapon.enableBeltTime - 10f / base.CurrentWeapon.reloadSpeed);
		}
		if (base.NotLOD)
		{
			this.player.Animations.PlayReload(base.CurrentWeapon.ReloadTime);
		}
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x000A6EE0 File Offset: 0x000A50E0
	protected override void OnPlayPompovikReload()
	{
		if (this.LOD)
		{
			return;
		}
		float time = (!base.CurrentWeapon.UseShotGunReload) ? 1f : (base.CurrentWeapon as ClientWeapon).AniReloadStartTime;
		this.player.Animations.PlayReloadStart(time);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x000A6F38 File Offset: 0x000A5138
	protected override void UpdateAllInterface()
	{
		if (this.LOD)
		{
			return;
		}
		EventFactory.Call("Fire", null);
		EventFactory.Call("Hp", null);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x000A6F68 File Offset: 0x000A5168
	protected override void OnPlayReloadEnd()
	{
		if (this.LOD)
		{
			return;
		}
		float time = (!base.CurrentWeapon.UseShotGunReload) ? 1f : 2f;
		this.player.Animations.PlayReloadEnd(time);
	}

	// Token: 0x06000E50 RID: 3664 RVA: 0x000A6FB4 File Offset: 0x000A51B4
	protected override void OnPlayInsertClip()
	{
		if (this.LOD)
		{
			return;
		}
		this.player.Animations.PlayReloadLoop(base.CurrentWeapon.ReloadTime / 4f);
	}

	// Token: 0x06000E51 RID: 3665 RVA: 0x000A6FF0 File Offset: 0x000A51F0
	protected override void OnPlayAfterReload()
	{
		if (this.LOD)
		{
			return;
		}
		EventFactory.Call("Fire", null);
		this.player.Animations.PlayIdleFrame();
	}

	// Token: 0x06000E52 RID: 3666 RVA: 0x000A701C File Offset: 0x000A521C
	protected override void OnPlayPompovikReloadEnd()
	{
		if (this.LOD)
		{
			return;
		}
		float time = (!base.CurrentWeapon.UseShotGunReload) ? 1f : 2.1f;
		this.player.Animations.PlayReloadEnd(time);
	}

	// Token: 0x06000E53 RID: 3667 RVA: 0x000A7068 File Offset: 0x000A5268
	protected override void OnPlayPompovikInsertClip()
	{
		if (base.NotLOD)
		{
			this.player.Animations.PlayReloadLoop(base.CurrentWeapon.ReloadTime / 4f);
		}
	}

	// Token: 0x06000E54 RID: 3668 RVA: 0x000A70A4 File Offset: 0x000A52A4
	[Obfuscation(Exclude = true)]
	protected void DelayedAfterGranadeActions()
	{
		this.state.isAim = false;
		this._isStartAim = false;
	}

	// Token: 0x06000E55 RID: 3669 RVA: 0x000A70BC File Offset: 0x000A52BC
	[Obfuscation(Exclude = true)]
	public override void Grenade()
	{
		if (base.BreakableGroup || this.state.supportReady || this.useBinocular || this.G.IsInvoking("AfterAimIdle") || this.state.grenadeCount == 0)
		{
			return;
		}
		this.state.grenadeCount--;
		this.Cancel();
		this.G.Invoke("AfterGrenade", 0.85f, false);
		this.OnGrenade();
		this.grenadeTime = Time.realtimeSinceStartup;
		this.G.Invoke("DelayedAfterGranadeActions", 0.1f, false);
	}

	// Token: 0x06000E56 RID: 3670 RVA: 0x000A716C File Offset: 0x000A536C
	protected override void OnGrenade()
	{
		base.OnGrenade();
		if (this.LOD)
		{
			return;
		}
		if (base.weaponEquiped)
		{
			base.CurrentWeapon.PlayIdle();
		}
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[8], false, 10f, 150f);
		this.Hide();
		this.G.Invoke("GrenadeLaunch", 0f, false);
		this.player.Animations.Grenade();
		(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.grenade_throw);
		if (this.player.playerInfo.hasSonar && this.marker)
		{
			this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		}
		else if (this.player.playerInfo.hasMortar && this.marker)
		{
			this.marker.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
		}
		if (!Main.IsTeamGame || !CVars.g_radio)
		{
			return;
		}
		System.Random random = new System.Random();
		if (random.Next(2) == 0)
		{
			Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_grenade);
			Peer.ClientGame.LocalPlayer.Chat(Language.GrenadeThrowMessage1, ChatInfo.radio_message_at_hit);
		}
		else
		{
			Peer.ClientGame.LocalPlayer.Radio(RadioEnum.bear_grenade2);
			Peer.ClientGame.LocalPlayer.Chat(Language.GrenadeThrowMessage1, ChatInfo.radio_message_at_hit);
		}
	}

	// Token: 0x06000E57 RID: 3671 RVA: 0x000A730C File Offset: 0x000A550C
	protected override void OnMortarStrike()
	{
		if (base.NotLOD)
		{
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[7], false, 10f, 150f);
			if (this.player.playerInfo.hasMortar)
			{
				EventFactory.Call("ArmstreakUsed", ArmstreakEnum.mortar);
			}
			this.player.Animations.ThrowSupport();
			this.DestroyMarker();
		}
		base.OnMortarStrike();
	}

	// Token: 0x06000E58 RID: 3672 RVA: 0x000A7398 File Offset: 0x000A5598
	protected override void OnSupportFire()
	{
		if (base.NotLOD)
		{
			Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.changeClips[7], false, 10f, 150f);
			if (this.player.playerInfo.hasSonar)
			{
				EventFactory.Call("ArmstreakUsed", ArmstreakEnum.sonar);
			}
			this.player.Animations.ThrowSupport();
			this.DestroyMarker();
		}
		base.OnSupportFire();
	}

	// Token: 0x06000E59 RID: 3673 RVA: 0x000A7424 File Offset: 0x000A5624
	protected override void OnPlayIdleAim()
	{
		if (this.LOD)
		{
			return;
		}
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.AimSound[0], false, 10f, 150f);
		if (base.CurrentWeapon.IsModable)
		{
			return;
		}
		if (base.CurrentWeapon.Kolimator || base.CurrentWeapon.Optic)
		{
			this.player.Animations.PlayIdleAimOptic(base.CurrentWeapon.IdleAimTime);
		}
		else
		{
			this.player.Animations.PlayIdleAim(base.CurrentWeapon.IdleAimTime);
		}
		if (!base.CurrentWeapon.isBelt)
		{
			return;
		}
		foreach (object obj in base.CurrentWeapon.GetComponentInChildren<Animation>())
		{
			AnimationState animationState = (AnimationState)obj;
			if (animationState.name == "idle_to_aim")
			{
				base.CurrentWeapon.GetComponentInChildren<Animation>().Play("idle_to_aim");
			}
		}
	}

	// Token: 0x06000E5A RID: 3674 RVA: 0x000A7578 File Offset: 0x000A5778
	protected override void OnPlayAimIdle()
	{
		if (this.LOD)
		{
			return;
		}
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.AimSound[1], false, 10f, 150f);
		if (base.CurrentWeapon.IsModable)
		{
			return;
		}
		if (base.CurrentWeapon.Kolimator || base.CurrentWeapon.Optic)
		{
			this.player.Animations.PlayAimIdleOptic(base.CurrentWeapon.AimIdleTime);
		}
		else
		{
			this.player.Animations.PlayAimIdle(base.CurrentWeapon.AimIdleTime);
		}
		if (!base.CurrentWeapon.isBelt)
		{
			return;
		}
		foreach (object obj in base.CurrentWeapon.GetComponentInChildren<Animation>())
		{
			AnimationState animationState = (AnimationState)obj;
			if (animationState.name == "aim_to_idle")
			{
				base.CurrentWeapon.GetComponentInChildren<Animation>().Play("aim_to_idle");
			}
		}
	}

	// Token: 0x04000EF8 RID: 3832
	private GameObject marker;

	// Token: 0x04000EF9 RID: 3833
	private Transform Arms_root_Animated;

	// Token: 0x04000EFA RID: 3834
	private Vector3 Arms_root_Animated_StartPos = Vector3.zero;

	// Token: 0x04000EFB RID: 3835
	private Vector3 Arms_root_Animated_StartRot = Vector3.zero;

	// Token: 0x04000EFC RID: 3836
	private Vector3 cockshot = Vector3.zero;

	// Token: 0x04000EFD RID: 3837
	private float cockShotDist;

	// Token: 0x04000EFE RID: 3838
	private float prevDelta = -1f;

	// Token: 0x04000EFF RID: 3839
	private float nowDelta;

	// Token: 0x04000F00 RID: 3840
	private float speedDelta = 2f;

	// Token: 0x04000F01 RID: 3841
	private float prevCamFov = 60f;

	// Token: 0x04000F02 RID: 3842
	private float nowCamFov = 40f;

	// Token: 0x04000F03 RID: 3843
	private float speedCamFov = 1f;

	// Token: 0x04000F04 RID: 3844
	private float prevWeapFov = 40f;

	// Token: 0x04000F05 RID: 3845
	private float nowWeapFov = 40f;

	// Token: 0x04000F06 RID: 3846
	private float speedWeapFov = 1f;

	// Token: 0x04000F07 RID: 3847
	private int layerMask;

	// Token: 0x04000F08 RID: 3848
	private int dirtMask;

	// Token: 0x04000F09 RID: 3849
	protected Transform knife;

	// Token: 0x04000F0A RID: 3850
	private BattleScreenGUI battleScreen;
}
