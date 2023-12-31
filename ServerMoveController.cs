using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
[AddComponentMenu("Scripts/Game/ServerMoveController")]
internal class ServerMoveController : BaseMoveController
{
	// Token: 0x060014D5 RID: 5333 RVA: 0x000DBB50 File Offset: 0x000D9D50
	public override void OnPoolDespawn()
	{
		this.sit = false;
		this.model = null;
		this.lastGroundedPos = Vector3.zero;
		this.fireDamage = new eTimer();
		base.OnPoolDespawn();
	}

	// Token: 0x17000325 RID: 805
	// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000DBB88 File Offset: 0x000D9D88
	private ServerNetPlayer serverPlayer
	{
		get
		{
			return this.player as ServerNetPlayer;
		}
	}

	// Token: 0x060014D7 RID: 5335 RVA: 0x000DBB98 File Offset: 0x000D9D98
	public override void Init(BaseNetPlayer serverPlayer, Vector3 position)
	{
		base.Init(serverPlayer, position);
		this._detector = (base.GetComponent<CollisionDetector>() ?? base.gameObject.AddComponent<CollisionDetector>());
		this.root = base.transform.FindChild("proxy/head");
		this.root.localRotation = Quaternion.Euler(this.state.euler);
		this.root.localPosition = new Vector3(0f, CVars.pl_stand_height, 0f);
		this.rootCamera = this.root.FindChild("camera");
		this.model = base.transform.FindChild("proxy/model");
		this.spine = Utility.FindHierarchy(this.model, "NPC_Spine1");
	}

	// Token: 0x060014D8 RID: 5336 RVA: 0x000DBC60 File Offset: 0x000D9E60
	public override void Tick(float dt)
	{
		this.root.rotation = Quaternion.Euler(270f, this.state.euler.y, 0f);
		this.rootCamera.rotation = Quaternion.Euler(this.state.euler.x + 90f, this.state.euler.y, this.state.euler.z);
		this.model.rotation = Quaternion.Euler(0f, this.state.euler.y, 0f);
		base.Tick(dt);
	}

	// Token: 0x060014D9 RID: 5337 RVA: 0x000DBD10 File Offset: 0x000D9F10
	protected override void SitCheck(CWInput UInput, float dt)
	{
		if (this.state.isSeat)
		{
			if (!this.sit)
			{
				this.sit = true;
				this.OnSeat();
			}
		}
		else if (this.sit)
		{
			this.sit = false;
			this.OnStand();
		}
	}

	// Token: 0x060014DA RID: 5338 RVA: 0x000DBD64 File Offset: 0x000D9F64
	protected override void OnGrounded(Vector3 speed, float dt)
	{
		if (this.state.pos.y < -100f)
		{
			this.Position = this.lastGroundedPos + new Vector3(0f, 0.1f, 0f);
			this.state.velocity.y = 0f;
		}
		else if (this.state.isGrounded)
		{
			this.lastGroundedPos = this.state.pos;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 2) && this.fireDamage.Elapsed > 0.5f)
		{
			this.InZoneDamage(10f, 0f, this.serverPlayer, Buffs.fire);
		}
		if ((BIT.AND((int)this.player.playerInfo.buffs, 131072) && this.fireDamage.Elapsed > 0.5f) || (BIT.AND((int)this.player.playerInfo.buffs, 262144) && this.fireDamage.Elapsed > 0.5f))
		{
			this.InZoneDamage(10f, 0f, this.serverPlayer, (!this.serverPlayer.IsBear) ? Buffs.bearBaseHurt : Buffs.usecBaseHurt);
		}
		if ((BIT.AND((int)this.player.playerInfo.buffs, 32768) && this.fireDamage.Elapsed > 0.5f) || this._detector.IsCollides)
		{
			this.InZoneDamage(10f, 0f, this.serverPlayer, Buffs.bruno_helix);
		}
		if (this.state.hiPos - this.state.pos.y > 3.5f && this.state.isGrounded && this.serverPlayer.frog == 1f && this.serverPlayer.boots == 1f)
		{
			float num = (this.state.hiPos - this.state.pos.y - 3f) / 5.4f * 100f;
			if (this.serverPlayer.playerInfo.skillUnlocked(Skills.fall))
			{
				num /= 2f;
			}
			if (this.serverPlayer.PlayerHit(num, 0f, this.serverPlayer, null))
			{
				this.serverPlayer.Stats.Suicide(this.serverPlayer);
				this.serverPlayer.Kill(this.serverPlayer, 123, false, this.serverPlayer.ID, "legs", Vector3.zero, 0);
				return;
			}
			if (Peer.HardcoreMode && num > 60f && !this.player.Immortal)
			{
				this.player.playerInfo.buffs |= Buffs.brokenLeg;
			}
		}
		base.OnGrounded(speed, dt);
	}

	// Token: 0x060014DB RID: 5339 RVA: 0x000DC088 File Offset: 0x000DA288
	protected override void OnJumpUp()
	{
	}

	// Token: 0x060014DC RID: 5340 RVA: 0x000DC08C File Offset: 0x000DA28C
	protected override void OnStand()
	{
		this.root.localPosition = new Vector3(0f, CVars.pl_stand_height, 0f);
		this.serverPlayer.AnimationsThird.transform.localPosition = new Vector3(0f, -0.9f, 0f);
	}

	// Token: 0x060014DD RID: 5341 RVA: 0x000DC0E4 File Offset: 0x000DA2E4
	protected override void OnSeat()
	{
		this.root.localPosition = new Vector3(0f, CVars.pl_seat_height, 0f);
		this.serverPlayer.AnimationsThird.transform.localPosition = new Vector3(0f, -0.9f, 0f);
	}

	// Token: 0x060014DE RID: 5342 RVA: 0x000DC13C File Offset: 0x000DA33C
	private void InZoneDamage(float damage, float armorDamage, ServerNetPlayer player, Buffs buff)
	{
		if (!BIT.AND((int)player.playerInfo.buffs, (int)buff) || this.fireDamage.Elapsed <= 0.5f)
		{
			return;
		}
		this.fireDamage.Stop();
		if (!this.serverPlayer.PlayerHit(damage, armorDamage, player, null))
		{
			return;
		}
		this.serverPlayer.Stats.Suicide(this.serverPlayer);
		this.serverPlayer.Kill(this.serverPlayer, 123, false, this.serverPlayer.ID, "legs", Vector3.zero, 0);
	}

	// Token: 0x060014DF RID: 5343 RVA: 0x000DC1DC File Offset: 0x000DA3DC
	public override void CallLateUpdate()
	{
		if (BIT.AND((int)this.player.playerInfo.buffs, 2))
		{
			this.player.playerInfo.buffs -= 2;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 16))
		{
			this.player.playerInfo.buffs -= 16;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 32))
		{
			this.player.playerInfo.buffs -= 32;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 32768))
		{
			this.player.playerInfo.buffs -= 32768;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 131072))
		{
			this.player.playerInfo.buffs -= 131072;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 262144))
		{
			this.player.playerInfo.buffs -= 262144;
		}
		if (this._detector.IsCollides)
		{
			this.player.playerInfo.buffs |= Buffs.bruno_helix;
			if (!this.fireDamage.Enabled)
			{
				this.fireDamage.Start();
			}
		}
		Collider[] array = Physics.OverlapSphere(this.state.pos, 1f, 1 << LayerMask.NameToLayer("triggers"));
		Collider[] array2 = array;
		int i = 0;
		while (i < array2.Length)
		{
			Collider collider = array2[i];
			string name = collider.transform.name;
			switch (name)
			{
			case "fire":
				this.player.playerInfo.buffs |= Buffs.fire;
				if (!this.fireDamage.Enabled)
				{
					this.fireDamage.Start();
				}
				break;
			case "bearBaseHurt":
				if (Main.IsTacticalConquest)
				{
					this.player.playerInfo.buffs |= Buffs.bearBaseHurt;
					if (!this.fireDamage.Enabled)
					{
						this.fireDamage.Start();
					}
				}
				break;
			case "usecBaseHurt":
				if (Main.IsTacticalConquest)
				{
					this.player.playerInfo.buffs |= Buffs.usecBaseHurt;
					if (!this.fireDamage.Enabled)
					{
						this.fireDamage.Start();
					}
				}
				break;
			case "bruno_helix":
				this.player.playerInfo.buffs |= Buffs.bruno_helix;
				if (!this.fireDamage.Enabled)
				{
					this.fireDamage.Start();
				}
				break;
			case "diffuse":
				if (Main.IsTargetDesignation && !this.player.IsTeam(Main.GameModeInfo.isBearPlacement) && Peer.ServerGame.Placement.IsWaitingOrDeplacing)
				{
					this.player.playerInfo.buffs |= Buffs.placement;
					if (Peer.ServerGame.Placement.PlayerID == this.player.ID)
					{
						this.player.playerInfo.buffs |= Buffs.beacon_user;
					}
				}
				break;
			case "placement_0":
			case "placement_1":
			case "placement_2":
				if (Main.IsTargetDesignation && this.player.IsTeam(Main.GameModeInfo.isBearPlacement) && Peer.ServerGame.Placement.IsReadyOrPlacing)
				{
					this.player.playerInfo.buffs |= Buffs.placement;
					if (Peer.ServerGame.Placement.PlayerID == this.player.ID)
					{
						this.player.playerInfo.buffs |= Buffs.beacon_user;
					}
				}
				break;
			}
			IL_4BE:
			i++;
			continue;
			goto IL_4BE;
		}
		base.CallLateUpdate();
	}

	// Token: 0x04001962 RID: 6498
	public Transform model;

	// Token: 0x04001963 RID: 6499
	private Vector3 lastGroundedPos = Vector3.zero;

	// Token: 0x04001964 RID: 6500
	private eTimer fireDamage = new eTimer();

	// Token: 0x04001965 RID: 6501
	private CollisionDetector _detector;
}
