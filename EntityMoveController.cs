using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001E5 RID: 485
[AddComponentMenu("Scripts/Game/EntityMoveController")]
internal class EntityMoveController : BaseMoveController
{
	// Token: 0x06001010 RID: 4112 RVA: 0x000B5334 File Offset: 0x000B3534
	public override void OnPoolDespawn()
	{
		this.Neck = null;
		this.bober = new Bober(1.2f);
		base.OnPoolDespawn();
	}

	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06001011 RID: 4113 RVA: 0x000B5354 File Offset: 0x000B3554
	private EntityNetPlayer playerEntity
	{
		get
		{
			return this.player as EntityNetPlayer;
		}
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06001012 RID: 4114 RVA: 0x000B5364 File Offset: 0x000B3564
	// (set) Token: 0x06001013 RID: 4115 RVA: 0x000B536C File Offset: 0x000B356C
	public override Vector3 Position
	{
		get
		{
			return base.Position;
		}
		set
		{
			this.playerEntity.PlayerTransform.position = value;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06001014 RID: 4116 RVA: 0x000B5380 File Offset: 0x000B3580
	public override bool isMoving
	{
		get
		{
			return (this.prevPosition - this.Position).magnitude > 0.2f;
		}
	}

	// Token: 0x17000257 RID: 599
	// (get) Token: 0x06001015 RID: 4117 RVA: 0x000B53B0 File Offset: 0x000B35B0
	public override bool IsGrounded
	{
		get
		{
			return this.state.isGrounded;
		}
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x000B53C0 File Offset: 0x000B35C0
	public override void Init(BaseNetPlayer player, Vector3 position)
	{
		this._lastTime = 0f;
		this._timeBetweenPackets = 0.2f;
		this.player = player;
		this.Position = position;
		this._lastPosition = position;
		this.spine = Utility.FindHierarchy(base.transform, "NPC_Spine1");
		this.spine1 = this.spine.transform;
		this.root = base.transform.FindChild("proxy/head");
		this.root.localRotation = Quaternion.Euler(this.state.euler);
		this.rootCamera = this.root.FindChild("camera");
		this.Neck = Utility.FindHierarchy(this.spine, "NPC_Neck").transform;
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x000B5484 File Offset: 0x000B3684
	public override void Tick(float dt)
	{
		this.root.rotation = Quaternion.Euler(270f, this.state.euler.y, 0f);
		this.rootCamera.rotation = Quaternion.Euler(this.state.euler.x + 90f, this.state.euler.y, this.state.euler.z);
	}

	// Token: 0x06001018 RID: 4120 RVA: 0x000B5504 File Offset: 0x000B3704
	protected override void OnStand()
	{
		this.root.localPosition = new Vector3(0f, CVars.pl_stand_height, 0f);
	}

	// Token: 0x06001019 RID: 4121 RVA: 0x000B5528 File Offset: 0x000B3728
	protected override void OnSeat()
	{
		Debug.Log("and now?");
		this.root.localPosition = new Vector3(0f, CVars.pl_seat_height, 0f);
	}

	// Token: 0x0600101A RID: 4122 RVA: 0x000B5554 File Offset: 0x000B3754
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

	// Token: 0x0600101B RID: 4123 RVA: 0x000B55A8 File Offset: 0x000B37A8
	protected override void StepSound(RaycastHit hit)
	{
		if (EntityNetPlayer.IsClientPlayer(this.playerEntity.ID))
		{
			return;
		}
		string name = hit.collider.material.name;
		List<SurfaceDescription> surfaces = SingletoneForm<DecalFactory>.Instance.surfaces;
		int count = surfaces.Count;
		for (int i = 0; i < count; i++)
		{
			if (name.Contains(surfaces[i].type.ToString()))
			{
				List<AudioClip> walkSounds = surfaces[i].walkSounds;
				Audio.Play(base.GetComponent<PoolItem>(), walkSounds[FastRndom.Int(walkSounds.Count - 1)], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
			}
		}
	}

	// Token: 0x0600101C RID: 4124 RVA: 0x000B5660 File Offset: 0x000B3860
	public override void CallLateUpdate()
	{
		EntityNetPlayer entityNetPlayer = this.player as EntityNetPlayer;
		if (!entityNetPlayer)
		{
			return;
		}
		if (entityNetPlayer.lastPacket.moveState.pos == Vector3.zero)
		{
			return;
		}
		float num = Time.time - this._timeBetweenPackets;
		float num2 = 1f - (entityNetPlayer.lastPacket.Time - num) / this._timeBetweenPackets + 0.1f;
		this.prevPosition = entityNetPlayer.PlayerTransform.position;
		if (Math.Abs(this._lastTime - entityNetPlayer.lastPacket.Time) > 1E-45f)
		{
			float num3 = entityNetPlayer.lastPacket.Time - this._lastTime;
			if (EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID) || (num3 > 0.01f && num3 < 1f))
			{
				this._timeBetweenPackets = Mathf.Lerp(this._timeBetweenPackets, num3, 0.1f);
			}
			this._lastTime = entityNetPlayer.lastPacket.Time;
			this._lastPosition = entityNetPlayer.PlayerTransform.position;
			Vector3 euler = entityNetPlayer.lastPacket.moveState.euler;
			euler.x = 0f;
			this._lastRotation = entityNetPlayer.PlayerTransform.rotation;
			this._newRotation = Quaternion.Euler(euler);
		}
		if (float.IsNaN(num2))
		{
			num2 = 1f;
		}
		if (num2 > 1f)
		{
			num2 = 1f;
		}
		if (num2 < 0f)
		{
			num2 = 0f;
		}
		Vector3 position = this._lastPosition + (entityNetPlayer.lastPacket.moveState.pos - this._lastPosition) * num2;
		entityNetPlayer.PlayerTransform.position = position;
		entityNetPlayer.PlayerTransform.rotation = Quaternion.Lerp(this._lastRotation, this._newRotation, num2);
		this.state = entityNetPlayer.lastPacket.moveState;
		if (this.player != null && !BIT.AND((int)this.player.playerInfo.buffs, 1024) && this._lastBlinkTime < Time.time)
		{
			this._lastBlinkTime = Time.time + 0.5f;
			BaseAmmunitions ammo = this.player.Ammo;
			if (!entityNetPlayer.isPlayer && !this.player.hasAntiFlareLens && ammo != null && ammo.CurrentWeapon != null && this.player.AimWithOptic && Main.IsAlive && Main.UserInfo.skillUnlocked(Skills.att3))
			{
				ClientNetPlayer localPlayer = Peer.ClientGame.LocalPlayer;
				Transform transform = this.player.MainCamera.transform;
				Vector3 position2 = transform.position;
				float num4 = Utility.DistancePointLine(localPlayer.Position, position2, position2 + transform.forward * 10000f);
				float magnitude = (localPlayer.Position - position2).magnitude;
				float num5 = num4 / magnitude;
				if (num5 < 0.175f && !Physics.Linecast(position2, localPlayer.Position, PhysicsUtility.level_layers) && !localPlayer.AimWithOptic)
				{
					EventFactory.Call("AddHotspot", new object[]
					{
						this.player.ID,
						num4
					});
				}
			}
		}
		if (this.playerEntity.isPlayer)
		{
			return;
		}
		if (!this.playerEntity.IsAlive)
		{
			return;
		}
		if (EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID))
		{
			return;
		}
		if (entityNetPlayer.Controller.state.isGrounded)
		{
			this._stepCounter += (this.Position - this.prevPosition).sqrMagnitude;
			if (this._stepCounter > this._stepDistance * 2f)
			{
				base.PlayStep();
				this._stepCounter = 0f;
			}
		}
		if (entityNetPlayer.Controller.state.isSeat && !this.playOnce)
		{
			this.playOnce = true;
			Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.CrouchSound[0], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
		}
		if (!entityNetPlayer.Controller.state.isSeat && this.playOnce)
		{
			this.playOnce = false;
			Audio.Play(base.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.CrouchSound[1], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
		}
	}

	// Token: 0x04001079 RID: 4217
	public Transform Neck;

	// Token: 0x0400107A RID: 4218
	private Bober bober = new Bober(1.2f);

	// Token: 0x0400107B RID: 4219
	private float _stepCounter;

	// Token: 0x0400107C RID: 4220
	private float _stepDistance = 1.2f;

	// Token: 0x0400107D RID: 4221
	private bool playOnce;

	// Token: 0x0400107E RID: 4222
	private Transform spine1;

	// Token: 0x0400107F RID: 4223
	private float _lastBlinkTime = -5f;

	// Token: 0x04001080 RID: 4224
	[SerializeField]
	public float forTime = 0.1f;

	// Token: 0x04001081 RID: 4225
	private float _lastTime;

	// Token: 0x04001082 RID: 4226
	private float _timeBetweenPackets = 0.2f;

	// Token: 0x04001083 RID: 4227
	private Vector3 _lastPosition = default(Vector3);

	// Token: 0x04001084 RID: 4228
	private Quaternion _lastRotation = default(Quaternion);

	// Token: 0x04001085 RID: 4229
	private Quaternion _newRotation = default(Quaternion);
}
