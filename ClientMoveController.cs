using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001AC RID: 428
[AddComponentMenu("Scripts/Game/ClientMoveController")]
internal class ClientMoveController : BaseMoveController
{
	// Token: 0x06000E66 RID: 3686 RVA: 0x000A7BA4 File Offset: 0x000A5DA4
	public override void OnPoolDespawn()
	{
		this.hands.localPosition = new Vector3(0f, CVars.pl_stand_height, 0f);
		this.shaker = null;
		this.armsRoot = null;
		this.camRoot = null;
		this.rotator = null;
		this.hands = null;
		this.bober = new Bober(1.6f);
		this.currentMouseSensitivity = 1f;
		this.prevDelta = Vector2.zero;
		this.state.Clear();
		this.nowState.Clear();
		this.prevLateUpdateTime = -1f;
		base.OnPoolDespawn();
	}

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000A7C40 File Offset: 0x000A5E40
	// (set) Token: 0x06000E68 RID: 3688 RVA: 0x000A7C50 File Offset: 0x000A5E50
	public float MouseSensitivity
	{
		get
		{
			return this.currentMouseSensitivity * ClientMoveController.MouseSensitivityMult;
		}
		set
		{
			this.currentMouseSensitivity = Main.Binds.sens * value;
			if (this.player.Ammo.weaponEquiped && this.player.Ammo.IsAim && this.player.Ammo.CurrentWeapon.Optic)
			{
				this.currentMouseSensitivity *= this.player.Ammo.CurrentWeapon.SensitivityMult;
			}
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000A7CD8 File Offset: 0x000A5ED8
	// (set) Token: 0x06000E6A RID: 3690 RVA: 0x000A7CE8 File Offset: 0x000A5EE8
	public override Vector3 Position
	{
		get
		{
			return this.state.pos;
		}
		set
		{
			this.controllerTransform.position = value;
			this.state.pos = value;
		}
	}

	// Token: 0x06000E6B RID: 3691 RVA: 0x000A7D04 File Offset: 0x000A5F04
	private void OnDrawGizmos()
	{
		if (this.player != null)
		{
			(this.player as ClientNetPlayer).Prediction.Draw();
		}
	}

	// Token: 0x06000E6C RID: 3692 RVA: 0x000A7D38 File Offset: 0x000A5F38
	public void TestJump(string parameters)
	{
		Audio.Play(this.Position, SingletoneForm<SoundFactory>.Instance.dieSounds[0], -1f, -1f);
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x000A7D68 File Offset: 0x000A5F68
	public override void Init(BaseNetPlayer player, Vector3 position)
	{
		base.Init(player, position);
		this.hands = base.transform.FindChild("proxy/hands");
		this.armsRoot = base.transform.FindChild("proxy/hands/root/Arms_root");
		this.camRoot = base.transform.FindChild("proxy/hands/root/Camera_root");
		this.rotator = base.transform.FindChild("proxy/hands/root/Camera_root/Camera_root_Animated/rotator");
		this.root = base.transform.FindChild("proxy/hands/root").transform;
		this.root.localRotation = Quaternion.Euler(this.state.euler);
		this.shaker = base.GetComponent<CameraShaker>();
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x000A7E18 File Offset: 0x000A6018
	public override void Tick(float dt)
	{
		Vector3 pos = this.state.pos;
		base.Tick(dt);
		Vector3 v = (pos - this.state.pos) / dt;
		if (v.normalized != -Vector3.up)
		{
			this.bober.AdvanceStep(v, dt, this.state.isGrounded, this.player.Ammo.AimSpeed);
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x000A7E94 File Offset: 0x000A6094
	public override void Recover(MoveState state)
	{
		base.Recover(state);
	}

	// Token: 0x06000E70 RID: 3696 RVA: 0x000A7EA0 File Offset: 0x000A60A0
	protected override void OnGrounded(Vector3 speed, float dt)
	{
		if (this.state.isGrounded)
		{
			if (this.state.hiPos - this.state.pos.y > 0.5f)
			{
				base.PlayStep();
				base.Invoke("PlayStep", 0.1f);
			}
			if (this.state.hiPos - this.state.pos.y > 3f)
			{
				Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.fallPain[0], false, 10f, 150f);
				this.shaker.InitShake(this.state.pos + new Vector3(0f, 1f - this.state.hiPos / 8f, 0f) * 8f);
				if (this.player.Ammo.IsAim)
				{
					(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.fall_isAim);
				}
				else
				{
					(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.fall);
				}
			}
		}
		base.OnGrounded(speed, dt);
	}

	// Token: 0x06000E71 RID: 3697 RVA: 0x000A7FEC File Offset: 0x000A61EC
	protected override void OnStand()
	{
		base.OnStand();
		if (this.player.Ammo.IsAim)
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.stand_isAim);
		}
		else
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.stand);
		}
		this.hands.localPosition = new Vector3(0f, CVars.pl_stand_height, 0f);
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.CrouchSound[1], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
	}

	// Token: 0x06000E72 RID: 3698 RVA: 0x000A8098 File Offset: 0x000A6298
	protected override void OnJumpUp()
	{
		if (this.player.Ammo.IsAim)
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.jump_isAim);
		}
		else
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.jump);
		}
		base.OnJumpUp();
	}

	// Token: 0x06000E73 RID: 3699 RVA: 0x000A80F4 File Offset: 0x000A62F4
	protected override void OnSeat()
	{
		base.OnSeat();
		if (this.player.Ammo.IsAim)
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.sit_isAim);
		}
		else
		{
			(this.player as ClientNetPlayer).CameraAnimations.Play(AnimationType.sit);
		}
		this.hands.localPosition = new Vector3(0f, CVars.pl_seat_height, 0f);
		Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), SingletoneForm<SoundFactory>.Instance.CrouchSound[0], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
	}

	// Token: 0x06000E74 RID: 3700 RVA: 0x000A81A0 File Offset: 0x000A63A0
	protected override void PostVelocity(ref Vector3 speed)
	{
		base.PostVelocity(ref speed);
		ClientNetPlayer clientNetPlayer = this.player as ClientNetPlayer;
		if (clientNetPlayer.Prediction.Instantly)
		{
			this.controllerTransform.position = clientNetPlayer.Prediction.Target;
		}
		speed += clientNetPlayer.Prediction.SmoothDir(this.state);
	}

	// Token: 0x06000E75 RID: 3701 RVA: 0x000A8208 File Offset: 0x000A6408
	protected override void StepSound(RaycastHit hit)
	{
		string name = hit.collider.material.name;
		List<SurfaceDescription> surfaces = SingletoneForm<DecalFactory>.Instance.surfaces;
		int count = surfaces.Count;
		for (int i = 0; i < count; i++)
		{
			if (name.Contains(surfaces[i].type.ToString()))
			{
				List<AudioClip> walkSounds = surfaces[i].walkSounds;
				Audio.Play((this.player as ClientNetPlayer).MainCamera.GetComponent<PoolItem>(), walkSounds[FastRndom.Int(walkSounds.Count - 1)], true, CVars.pl_minStepDistance, CVars.pl_maxStepDistance);
			}
		}
	}

	// Token: 0x06000E76 RID: 3702 RVA: 0x000A82B4 File Offset: 0x000A64B4
	private void Update()
	{
		this.mx += Input.GetAxis("Mouse X");
		this.my += Input.GetAxis("Mouse Y");
	}

	// Token: 0x06000E77 RID: 3703 RVA: 0x000A82F0 File Offset: 0x000A64F0
	public override void CallLateUpdate()
	{
		if (Forms.keyboardLock || !Forms.mouseLock)
		{
			this.mx = 0f;
			this.my = 0f;
			this.prevDelta = Vector2.zero;
		}
		if (Main.Binds.invertMouse)
		{
			this.my = -this.my;
		}
		Vector2 a = new Vector2(this.mx, this.my);
		this.mx = 0f;
		this.my = 0f;
		this.proxy.position = this.state.pos;
		MoveState state = this.state;
		state.euler.y = state.euler.y + this.MouseSensitivity * a.x * 270f * 0.02f;
		MoveState state2 = this.state;
		state2.euler.x = state2.euler.x - this.MouseSensitivity * a.y * 270f * 0.02f;
		if (this.state.euler.x < 180f)
		{
			this.state.euler.x = 180f;
		}
		if (this.state.euler.x > 360f)
		{
			this.state.euler.x = 360f;
		}
		if (this.root.eulerAngles.y != this.state.euler.y)
		{
			this.root.rotation = Quaternion.Euler(270f, this.state.euler.y, 0f);
		}
		if (this.camRoot.eulerAngles != this.state.euler)
		{
			this.camRoot.rotation = Quaternion.Euler(this.state.euler);
		}
		this.prevDelta += (a - this.prevDelta) * 0.5f * Time.deltaTime * 10f;
		this.prevDelta.x = Mathf.Clamp(this.prevDelta.x, -1f, 1f);
		this.prevDelta.y = Mathf.Clamp(this.prevDelta.y, -1f, 1f);
		float num;
		if (this.MouseSensitivity >= 0.4f)
		{
			num = 2f;
		}
		else
		{
			num = this.MouseSensitivity / 0.2f;
		}
		this.bober.Update(this.player.Ammo.AimSpeed * num);
		this.camRoot.localPosition = this.bober.Position1(this.player.Ammo.AimSpeed);
		this.armsRoot.localPosition = this.bober.Position2(this.player.Ammo.AimSpeed);
		this.armsRoot.RotateAround(this.rotator.position, this.rotator.up, this.prevDelta.x * 2.5f * this.player.Ammo.AimSpeed * num);
		this.armsRoot.RotateAround(this.rotator.position, this.rotator.right, -this.prevDelta.y * 1.8f * this.player.Ammo.AimSpeed * num);
		if (this.player.Ammo.IsAim)
		{
			if (BIT.AND((int)this.player.playerInfo.buffs, 1048576) && Peer.ClientGame.LocalPlayer.Ammo.cPrimary)
			{
				this.armsRoot.localRotation = Quaternion.Euler(this.state.euler.x + this.shaker.JumpEuler + Mathf.Sin(Time.realtimeSinceStartup) / 3f, Mathf.Cos(Time.realtimeSinceStartup), 0f) * this.bober.rotation;
			}
			else
			{
				this.armsRoot.localRotation = Quaternion.Euler(this.state.euler.x + this.shaker.JumpEuler, 0f, 0f) * this.bober.rotation;
			}
			this.armsRoot.RotateAround(this.rotator.position, this.rotator.up, this.prevDelta.x * 3.5f * this.player.Ammo.AimSpeed * num);
			this.armsRoot.RotateAround(this.rotator.position, this.rotator.right, -this.prevDelta.y * 2.2f * this.player.Ammo.AimSpeed * num);
		}
		else if (!this.player.Ammo.IsAim)
		{
			if (BIT.AND((int)this.player.playerInfo.buffs, 1048576) && Peer.ClientGame.LocalPlayer.Ammo.cPrimary)
			{
				this.armsRoot.localRotation = Quaternion.Euler(this.state.euler.x + this.shaker.JumpEuler + Mathf.Sin(Time.realtimeSinceStartup * 3f), Mathf.Cos(Time.realtimeSinceStartup * 2f), 0f) * this.bober.rotation;
			}
			else
			{
				this.armsRoot.localRotation = Quaternion.Euler(this.state.euler.x + this.shaker.JumpEuler + Mathf.Sin(Time.realtimeSinceStartup * 3f) / 4f, Mathf.Sin(Time.realtimeSinceStartup * 2f) / 2f, 0f) * this.bober.rotation;
			}
		}
		this.shaker.CustomUpdate();
		this.camRoot.localPosition += this.shaker.pos;
		this.armsRoot.localPosition += this.shaker.pos * 0.9f;
		if (this.bober.NextStep())
		{
			base.PlayStep();
		}
	}

	// Token: 0x04000F0D RID: 3853
	private const float horizontalAimingSpeed = 270f;

	// Token: 0x04000F0E RID: 3854
	private const float verticalAimingSpeed = 270f;

	// Token: 0x04000F0F RID: 3855
	private const float speedDelta = 0.5f;

	// Token: 0x04000F10 RID: 3856
	public CameraShaker shaker;

	// Token: 0x04000F11 RID: 3857
	public static float MouseSensitivityMult = 1f;

	// Token: 0x04000F12 RID: 3858
	private Transform armsRoot;

	// Token: 0x04000F13 RID: 3859
	private Transform camRoot;

	// Token: 0x04000F14 RID: 3860
	private Transform rotator;

	// Token: 0x04000F15 RID: 3861
	private Transform hands;

	// Token: 0x04000F16 RID: 3862
	private Bober bober = new Bober(1.6f);

	// Token: 0x04000F17 RID: 3863
	private float currentMouseSensitivity = 1f;

	// Token: 0x04000F18 RID: 3864
	private Vector2 prevDelta = Vector2.zero;

	// Token: 0x04000F19 RID: 3865
	private MoveState nowState = new MoveState();

	// Token: 0x04000F1A RID: 3866
	private float prevLateUpdateTime = -1f;

	// Token: 0x04000F1B RID: 3867
	private float mx;

	// Token: 0x04000F1C RID: 3868
	private float my;
}
