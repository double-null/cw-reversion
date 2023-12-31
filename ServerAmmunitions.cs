using System;
using System.Collections.Generic;
using System.Reflection;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002E9 RID: 745
[AddComponentMenu("Scripts/Game/ServerAmmunitions")]
internal class ServerAmmunitions : BaseAmmunitions
{
	// Token: 0x060014A6 RID: 5286 RVA: 0x000D9A8C File Offset: 0x000D7C8C
	public override void OnPoolDespawn()
	{
		this.attackAngles = new float[]
		{
			0f,
			-25f,
			25f
		};
		base.OnPoolDespawn();
	}

	// Token: 0x17000323 RID: 803
	// (get) Token: 0x060014A7 RID: 5287 RVA: 0x000D9ABC File Offset: 0x000D7CBC
	private ServerNetPlayer serverPlayer
	{
		get
		{
			return this.player as ServerNetPlayer;
		}
	}

	// Token: 0x060014A8 RID: 5288 RVA: 0x000D9ACC File Offset: 0x000D7CCC
	protected override void TakeWeapon()
	{
		base.TakeWeapon();
		if (this.cSecondary)
		{
			this.cSecondary.transform.parent = base.transform;
		}
		if (this.cPrimary)
		{
			this.cPrimary.transform.parent = base.transform;
		}
	}

	// Token: 0x060014A9 RID: 5289 RVA: 0x000D9B2C File Offset: 0x000D7D2C
	protected override void SingleAttack(WeaponNature nature, BaseWeapon weapon = null)
	{
		base.SingleAttack(nature, weapon);
		List<eRaycastHit> list = new List<eRaycastHit>();
		Ray ray = default(Ray);
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
		ray.origin = this.serverPlayer.Controller.rootCamera.position;
		if (this.state.isAim && base.CurrentWeapon.IsModable)
		{
			Vector3 position = new Vector3(this.serverPlayer.Controller.rootCamera.localPosition.x - this.MoveDelta.x + this.offset.x, this.serverPlayer.Controller.rootCamera.localPosition.y - this.MoveDelta.y + this.offset.y, this.serverPlayer.Controller.rootCamera.localPosition.z - this.MoveDelta.z + this.offset.z);
			ray.origin = this.serverPlayer.Controller.rootCamera.TransformPoint(position);
		}
		ray.direction = Quaternion.Euler(this.serverPlayer.Euler.x + 90f, this.serverPlayer.Euler.y, this.serverPlayer.Euler.z) * vector.normalized;
		this.serverPlayer.Stats.totalAmmo++;
		float num2 = 200f;
		int num3 = 1;
		if (nature == WeaponNature.knife)
		{
			num2 = 1.5f;
			num3 = 3;
		}
		for (int i = 0; i < num3; i++)
		{
			if (i != 0)
			{
				ray.direction = Quaternion.Euler(this.serverPlayer.Euler.x + 90f, this.serverPlayer.Euler.y + this.attackAngles[i], this.serverPlayer.Euler.z) * vector.normalized;
			}
			Peer.ServerGame.RaycastAll(ray, this.serverPlayer.playerInfo.packetLatency, list);
		}
		RaycastHit[] array = Physics.RaycastAll(ray, num2, PhysicsUtility.level_layers);
		for (int j = 0; j < array.Length; j++)
		{
			list.Add(new eRaycastHit(array[j]));
		}
		if (nature == WeaponNature.knife)
		{
			for (int k = 0; k < list.Count; k++)
			{
				if (list[k].distance >= num2)
				{
					list.RemoveAt(k);
					k = -1;
				}
			}
		}
		if (list.Count == 0)
		{
			return;
		}
		list.Sort(new eRaycastSorter());
		float num4;
		if (nature == WeaponNature.knife)
		{
			num4 = 1000f;
		}
		else
		{
			num4 = base.CurrentWeapon.damage + this.ApAmmo;
			if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun && !base.CurrentWeapon.Slug)
			{
				num4 = base.CurrentWeapon.damage / (float)base.CurrentWeapon.caseshot + this.ApAmmo;
			}
			else if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun && base.CurrentWeapon.Slug)
			{
				num4 = base.CurrentWeapon.damage + this.ApAmmo;
			}
		}
		float num5 = num4;
		this.serverPlayer.Stats.StartStreak();
		for (int l = 0; l < list.Count; l++)
		{
			eRaycastHit eRaycastHit = list[l];
			if (l == 0 && CVars.a_grenade && this.serverPlayer.UserInfo.Permission >= EPermission.Admin)
			{
				Peer.ServerGame.Explosion(list[0].point);
				Peer.ServerGame.GrenadeExplosion(this.serverPlayer, list[0].point);
			}
			float num6 = num4;
			float distanceReducer = base.CurrentWeapon.getDistanceReducer(eRaycastHit.distance);
			num4 *= distanceReducer;
			if (CVars.g_damageDebug)
			{
				MonoBehaviour.print(string.Concat(new object[]
				{
					"Damage before distance check: ",
					num6,
					" Damage after: ",
					num4,
					" Distance: ",
					eRaycastHit.distance,
					" Reducer: ",
					distanceReducer
				}));
			}
			bool flag = eRaycastHit.MaterialName.Contains("blood");
			if (flag)
			{
				DamageX component = eRaycastHit.transform.GetComponent<DamageX>();
				if (component)
				{
					ServerNetPlayer serverNetPlayer = (ServerNetPlayer)component.player;
					if (serverNetPlayer)
					{
						if (!(this.serverPlayer == serverNetPlayer))
						{
							bool flag2 = false;
							if (serverNetPlayer.IsTeam(this.player.IsBear) && !CVars.g_friendlyfire && Main.IsTeamGame && !Peer.HardcoreMode)
							{
								flag2 = true;
							}
							if (serverNetPlayer.IsTeam(this.player.IsBear) && Peer.HardcoreMode && (ServerLeagueSystem.Enabled || Globals.I.DisableHardcoreFriendlyFire))
							{
								flag2 = true;
							}
							float num7 = 0f;
							if (!flag2)
							{
								float num8 = 0f;
								float num9 = num4;
								if (component.Armor && serverNetPlayer.Armor > 0f)
								{
									float num10 = num4 * base.CurrentWeapon.PierceReducer;
									num7 = num10 / 2f;
									if (num7 > serverNetPlayer.Armor)
									{
										num8 = num7 - serverNetPlayer.Armor;
										num7 = serverNetPlayer.Armor;
									}
									if (component.X < 10f)
									{
										num4 = num4 - num10 + num8;
									}
									if (CVars.g_damageDebug)
									{
										MonoBehaviour.print(string.Concat(new string[]
										{
											"Base damage: ",
											num9.ToString(),
											" Body damage: ",
											num4.ToString(),
											" Armor damage: ",
											num7.ToString(),
											" Absorbed damage: ",
											num10.ToString(),
											" extra damage: ",
											num8.ToString(),
											" ARMOR:",
											(serverNetPlayer.Armor - num7).ToString(),
											" HEALTH:",
											(serverNetPlayer.Health - num4).ToString()
										}));
									}
								}
							}
							if (component.X != 10f)
							{
								num4 *= component.X;
							}
							if (!flag2 && component.X >= 10f && component.Armor)
							{
								component.Armor = false;
								if (num4 < 60f)
								{
									num4 = 0f;
								}
							}
							float num11 = num4;
							if (serverNetPlayer.IsBear && this.serverPlayer.UserInfo.skillUnlocked(Skills.car_bear))
							{
								if (CVars.g_damageDebug)
								{
									MonoBehaviour.print("Bear bonus- damage:" + num4.ToString() + " damage after: " + (num4 * 1.1f).ToString());
								}
								num4 *= 1.1f;
							}
							if (!serverNetPlayer.IsBear && this.serverPlayer.UserInfo.skillUnlocked(Skills.car_usec))
							{
								if (CVars.g_damageDebug)
								{
									MonoBehaviour.print("Bear bonus- damage:" + num4.ToString() + " damage after: " + (num4 * 1.1f).ToString());
								}
								num4 *= 1.1f;
							}
							if (flag2)
							{
								num4 = 0f;
							}
							if (component.X == 10f)
							{
								num4 *= component.X;
							}
							if (Peer.HardcoreMode && !serverNetPlayer.Immortal)
							{
								if ((component.X == 0.6f || component.X == 0.8f) && num4 > 60f)
								{
									serverNetPlayer.playerInfo.buffs |= Buffs.brokenLeg;
								}
								if ((component.X == 0.5f || component.X == 0.7f) && num4 > 50f)
								{
									serverNetPlayer.playerInfo.buffs |= Buffs.brokenHand;
								}
							}
							this.serverPlayer.Stats.totalDamage += (int)Mathf.Min(num4, serverNetPlayer.playerInfo.Health);
							this.serverPlayer.Stats.totalHits++;
							if (serverNetPlayer.PlayerHit(num4, num7, this.serverPlayer, base.CurrentWeapon))
							{
								bool flag3 = false;
								bool flag4 = false;
								int num12 = (int)base.CurrentWeapon.type;
								if (nature == WeaponNature.knife)
								{
									num12 = 124;
								}
								if (component.X == 10f)
								{
									flag3 = true;
									num4 *= component.X;
								}
								float magnitude = (ray.origin - eRaycastHit.transform.position).magnitude;
								if (magnitude > 15f && flag3)
								{
									flag4 = true;
								}
								float num13 = num4;
								if (!CVars.f_bigrigs && flag3)
								{
									num13 /= component.X;
								}
								if (base.CurrentWeapon.weaponNature == WeaponNature.shotgun)
								{
									num13 = base.CurrentWeapon.damage;
								}
								AchievementTarget achievementTarget = AchievementTarget.any;
								if (serverNetPlayer.playerInfo.Health < 10f)
								{
									achievementTarget |= AchievementTarget.almostDead;
								}
								if (serverNetPlayer == Peer.ServerGame.Top1InMatch())
								{
									achievementTarget |= AchievementTarget.firstInMatch;
								}
								AchievementKillType killType = AchievementKillType.kill;
								if (flag3)
								{
									killType = AchievementKillType.headshot;
								}
								if (flag4)
								{
									killType = AchievementKillType.longshot;
								}
								bool atDeath = this.serverPlayer.playerInfo.Health < 10f;
								bool lastClip = base.CurrentWeapon.state.clips == 0;
								bool farmDetected = false;
								if (Main.GameModeInfo.farmCountermeasuresDepth > 0)
								{
									if (this.serverPlayer.victimList.Count > 5)
									{
										this.serverPlayer.victimList.Clear();
									}
									if (this.serverPlayer.victimList.Contains(serverNetPlayer.playerInfo.playerID))
									{
										farmDetected = true;
									}
									if (!this.serverPlayer.victimList.Contains(serverNetPlayer.playerInfo.playerID))
									{
										this.serverPlayer.victimList.Enqueue(serverNetPlayer.playerInfo.playerID);
									}
									if (this.serverPlayer.victimList.Count > Main.GameModeInfo.farmCountermeasuresDepth)
									{
										this.serverPlayer.victimList.Dequeue();
									}
								}
								this.serverPlayer.Stats.SafeKill(this.serverPlayer, flag3, flag4, serverNetPlayer, nature, weapon);
								if (!serverNetPlayer.IsTeam(this.serverPlayer) && (ServerVars.canFarmTasksCached || this.serverPlayer.UserInfo.currentLevel < 10))
								{
									if (nature != WeaponNature.knife)
									{
										WtasksEngine.Kill(this.serverPlayer, achievementTarget, killType, this.serverPlayer.Stats.NowEndStreak(), atDeath, lastClip, base.CurrentWeapon.weaponSpecific, farmDetected);
									}
									AchievementsEngine.Kill(this.serverPlayer, serverNetPlayer, achievementTarget, killType, this.serverPlayer.Stats.NowEndStreak(), nature, atDeath, lastClip, farmDetected);
									ContractsEngine.Kill(this.serverPlayer, serverNetPlayer, achievementTarget, killType, this.serverPlayer.Stats.NowEndStreak(), nature, (Maps)Main.HostInfo.MapIndex, Main.GameMode, farmDetected);
								}
								if (nature == WeaponNature.knife)
								{
									serverNetPlayer.Kill(this.serverPlayer, num12, flag3, serverNetPlayer.ID, eRaycastHit.transform.name, ray.direction, 0);
								}
								else
								{
									serverNetPlayer.Kill(this.serverPlayer, num12, flag3, serverNetPlayer.ID, eRaycastHit.transform.name, ray.direction * num13 / 100f, this.serverPlayer.UserInfo.userStats.weaponKills[num12]);
								}
								if (nature != WeaponNature.knife)
								{
									this.serverPlayer.Stats.weaponKills[(int)base.CurrentWeapon.type]++;
								}
							}
							if (nature != WeaponNature.knife)
							{
								num4 = num11 * 0.5f;
								if (num4 <= 0f)
								{
									break;
								}
							}
							else
							{
								num4 = 0f;
							}
						}
					}
				}
			}
			else
			{
				float num14 = 0f;
				if (nature != WeaponNature.knife)
				{
					num14 = base.CurrentWeapon.PierceProc;
				}
				else
				{
					num4 = 0f;
				}
				string tag = eRaycastHit.transform.tag;
				if (tag.Contains("piercesLight"))
				{
					if (num14 >= 15f)
					{
						num4 -= 0.2f * num5;
					}
					else
					{
						num4 = 0f;
					}
				}
				if (tag.Contains("piercesMedium"))
				{
					if (num14 >= 30f)
					{
						num4 -= 0.5f * num5;
					}
					else
					{
						num4 = 0f;
					}
				}
				if (tag.Contains("piercesHeavy"))
				{
					if (num14 >= 60f)
					{
						num4 -= 0.7f * num5;
					}
					else
					{
						num4 = 0f;
					}
				}
				if (tag.Contains("Untagged"))
				{
					num4 = 0f;
				}
				if (num4 <= 0f)
				{
					break;
				}
			}
		}
		this.serverPlayer.Stats.EndStreak(this.serverPlayer);
	}

	// Token: 0x060014AA RID: 5290 RVA: 0x000DAA54 File Offset: 0x000D8C54
	[Obfuscation(Exclude = true)]
	public override void Toggle()
	{
		if (base.BreakableGroup)
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
		if (!base.weaponEquiped)
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
			base.ChangeAimMode(false);
			this.G.Invoke("Toggle", base.CurrentWeapon.AimIdleTime * 1.05f, false);
			return;
		}
		this.IdleOut();
		this.G.Invoke("AfterToggleIdleOut", base.CurrentWeapon.IdleOut, false);
		if (base.weaponEquiped)
		{
			this.serverPlayer.AnimationsThird.PlayIdleOut(base.CurrentWeapon.OutIdle);
		}
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000DABC4 File Offset: 0x000D8DC4
	[Obfuscation(Exclude = true)]
	protected override void AfterToggleIdleOut()
	{
		this.OutIdle();
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000DABCC File Offset: 0x000D8DCC
	[Obfuscation(Exclude = true)]
	protected override void GrenadeLaunch()
	{
		Transform transform = base.transform.Find("proxy/head/camera/GrenadeHolder");
		ServerEntity component = SingletoneForm<PoolManager>.Instance["server_grenade"].Spawn().GetComponent<ServerEntity>();
		component.transform.parent = Peer.ServerGame.transform;
		Peer.ServerGame.GetComponent<PoolItem>().Childs.Add(component.GetComponent<PoolItem>());
		component.state.type = EntityType.grenade;
		component.Init(this.serverPlayer, transform.position, transform.forward, this.throwPowerMult);
		Peer.ServerGame.AddEntity(component);
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000DAC68 File Offset: 0x000D8E68
	public override void Knife()
	{
		if (base.BreakableGroup || this.state.supportReady || this.useBinocular)
		{
			return;
		}
		base.Knife();
		this.serverPlayer.AnimationsThird.PlayKnife(this.KnifeDelay);
	}

	// Token: 0x060014AE RID: 5294 RVA: 0x000DACB8 File Offset: 0x000D8EB8
	[Obfuscation(Exclude = true)]
	public override void Binoculars()
	{
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

	// Token: 0x060014AF RID: 5295 RVA: 0x000DADC4 File Offset: 0x000D8FC4
	protected override void PrepareWeapon()
	{
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["server_weapon"].Spawn();
		gameObject.transform.parent = base.transform;
		this.cSecondary = gameObject.GetComponent<ServerWeapon>();
		this.cSecondary.LOD = true;
		this.cSecondary.state.isMod = this.state.secondaryMod;
		this.cSecondary.state.Mods = this.state.secondaryState.Mods;
		this.cSecondary.Init(this.player, this.state.secondaryIndex);
		this.cSecondary.AfterInit(this.state.secondaryState.repair_info);
		if (this.state.primaryIndex != 127)
		{
			gameObject = SingletoneForm<PoolManager>.Instance["server_weapon"].Spawn();
			gameObject.transform.parent = base.transform;
			this.cPrimary = gameObject.GetComponent<ServerWeapon>();
			this.cPrimary.LOD = true;
			this.cPrimary.state.isMod = this.state.primaryMod;
			this.cPrimary.state.Mods = this.state.primaryState.Mods;
			this.cPrimary.Init(this.player, this.state.primaryIndex);
			this.cPrimary.AfterInit(this.state.primaryState.repair_info);
		}
	}

	// Token: 0x060014B0 RID: 5296 RVA: 0x000DAF44 File Offset: 0x000D9144
	protected override void OnFire()
	{
		if (Time.realtimeSinceStartup - this.attackerDelay > 0.3f)
		{
			this.attackerDelay = Time.realtimeSinceStartup;
			Peer.ServerGame.AddAttacker(this.serverPlayer, base.CurrentWeapon.SS);
		}
	}

	// Token: 0x060014B1 RID: 5297 RVA: 0x000DAF90 File Offset: 0x000D9190
	protected override void OnPlayReload()
	{
		this.serverPlayer.AnimationsThird.PlayReload(base.CurrentWeapon.ReloadTime);
	}

	// Token: 0x060014B2 RID: 5298 RVA: 0x000DAFB8 File Offset: 0x000D91B8
	protected override void OnPlayPompovikReload()
	{
		this.serverPlayer.AnimationsThird.PlayReload(base.CurrentWeapon.ReloadTime);
	}

	// Token: 0x060014B3 RID: 5299 RVA: 0x000DAFE0 File Offset: 0x000D91E0
	[Obfuscation(Exclude = true)]
	public override void Grenade()
	{
		if (base.BreakableGroup || this.state.supportReady || this.useBinocular || this.G.IsInvoking("AfterAimIdle") || this.state.grenadeCount == 0)
		{
			return;
		}
		this.state.grenadeCount--;
		this.state.isAim = false;
		this._isStartAim = false;
		this.Cancel();
		this.G.Invoke("AfterGrenade", 0.85f, false);
		this.OnGrenade();
		this.grenadeTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060014B4 RID: 5300 RVA: 0x000DB08C File Offset: 0x000D928C
	protected override void OnGrenade()
	{
		this.serverPlayer.AnimationsThird.PlayGrenade(0.4f);
		this.G.Invoke("GrenadeLaunch", 0f, false);
	}

	// Token: 0x060014B5 RID: 5301 RVA: 0x000DB0C4 File Offset: 0x000D92C4
	protected override void OnMortarStrike()
	{
		ServerEntity component = SingletoneForm<PoolManager>.Instance["server_marker"].Spawn().GetComponent<ServerEntity>();
		component.transform.parent = Peer.ServerGame.transform;
		Peer.ServerGame.GetComponent<PoolItem>().Childs.Add(component.GetComponent<PoolItem>());
		component.state.playerID = this.player.ID;
		component.state.isBear = this.player.IsBear;
		component.state.type = EntityType.mortar;
		Transform transform = base.transform.Find("proxy/head/camera/GrenadeHolder");
		component.Init(this.serverPlayer, transform.position, transform.forward, base.ThrowPower);
		Peer.ServerGame.AddEntity(component);
		base.OnMortarStrike();
	}

	// Token: 0x060014B6 RID: 5302 RVA: 0x000DB198 File Offset: 0x000D9398
	protected override void OnSupportFire()
	{
		ServerEntity component = SingletoneForm<PoolManager>.Instance["server_marker"].Spawn().GetComponent<ServerEntity>();
		component.transform.parent = Peer.ServerGame.transform;
		Peer.ServerGame.GetComponent<PoolItem>().Childs.Add(component.GetComponent<PoolItem>());
		component.state.playerID = this.player.ID;
		component.state.isBear = this.player.IsBear;
		component.state.type = EntityType.sonar;
		Transform transform = base.transform.Find("proxy/head/camera/GrenadeHolder");
		component.Init(this.serverPlayer, transform.position, transform.forward, base.ThrowPower);
		Peer.ServerGame.AddEntity(component);
		base.OnSupportFire();
	}

	// Token: 0x04001958 RID: 6488
	private float[] attackAngles = new float[]
	{
		0f,
		-25f,
		25f
	};

	// Token: 0x04001959 RID: 6489
	private float _fireLock;

	// Token: 0x0400195A RID: 6490
	public Vector3 offset = new Vector3(-0.04f, 0f, 0f);
}
