using System;
using System.Reflection;
using UnityEngine;

// Token: 0x020001A2 RID: 418
[AddComponentMenu("Scripts/Game/BaseMoveController")]
internal class BaseMoveController : PoolableBehaviour, cwNetworkSerializable
{
	// Token: 0x06000C4B RID: 3147 RVA: 0x00095A1C File Offset: 0x00093C1C
	public override void OnPoolDespawn()
	{
		this.state.Clear();
		this.seatBonusMult = 1f;
		this.player = null;
		this.spine = null;
		this.proxy = null;
		this.root = null;
		this.rootCamera = null;
		if (this.controller)
		{
			this.controller.enabled = false;
		}
		this.prevPosition = CVars.h_v3infinity;
		base.OnPoolDespawn();
	}

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00095A90 File Offset: 0x00093C90
	// (set) Token: 0x06000C4D RID: 3149 RVA: 0x00095AA0 File Offset: 0x00093CA0
	public virtual Vector3 Position
	{
		get
		{
			return this.state.pos;
		}
		set
		{
			this.controllerTransform.position = value;
			this.state.pos = value;
			this.proxy.position = this.state.pos;
		}
	}

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06000C4E RID: 3150 RVA: 0x00095ADC File Offset: 0x00093CDC
	public bool isSeat
	{
		get
		{
			return this.state.isSeat;
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06000C4F RID: 3151 RVA: 0x00095AEC File Offset: 0x00093CEC
	public bool isWalk
	{
		get
		{
			return this.state.isWalk;
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06000C50 RID: 3152 RVA: 0x00095AFC File Offset: 0x00093CFC
	public virtual bool isMoving
	{
		get
		{
			Vector3 vector = new Vector3(this.controller.velocity.x, 0f, this.controller.velocity.z);
			return vector.magnitude > 0.2f;
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00095B4C File Offset: 0x00093D4C
	public virtual bool IsGrounded
	{
		get
		{
			return this.controller.isGrounded || (this.controller.collisionFlags & CollisionFlags.Below) != CollisionFlags.None;
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06000C52 RID: 3154 RVA: 0x00095B80 File Offset: 0x00093D80
	public virtual float Velocity
	{
		get
		{
			return (this.prevPosition - this.Position).magnitude / Time.deltaTime;
		}
	}

	// Token: 0x17000189 RID: 393
	// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00095BAC File Offset: 0x00093DAC
	public virtual float SpeedMult
	{
		get
		{
			Vector3 a = this.state.moveDir * CVars.g_runSpeed * this.state.speedReducer * this.player.boots * this.state.jumpReducer;
			if (this.state.isSeat)
			{
				a *= CVars.g_seatMult * 1.9f * this.seatBonusMult;
			}
			else if (this.state.isWalk)
			{
				a *= CVars.g_walkMult * 2.5f;
			}
			else
			{
				a *= 1.4f;
			}
			return a.magnitude / CVars.g_runSpeed;
		}
	}

	// Token: 0x1700018A RID: 394
	// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00095C70 File Offset: 0x00093E70
	public Vector3 DownCap
	{
		get
		{
			return this.controllerTransform.position - Vector3.up * (CVars.px_capsuleheight / 2f - CVars.px_capsuleradius);
		}
	}

	// Token: 0x1700018B RID: 395
	// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00095CA0 File Offset: 0x00093EA0
	public Vector3 UpCap
	{
		get
		{
			return this.controllerTransform.position + Vector3.up * (CVars.px_capsuleheight / 2f - CVars.px_capsuleradius);
		}
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00095CD0 File Offset: 0x00093ED0
	public void Serialize(eNetworkStream stream)
	{
		this.state.Serialize(stream);
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x00095CE0 File Offset: 0x00093EE0
	public void Deserialize(eNetworkStream stream)
	{
		this.state.Deserialize(stream);
	}

	// Token: 0x06000C58 RID: 3160 RVA: 0x00095CF0 File Offset: 0x00093EF0
	protected virtual void SitCheck(CWInput UInput, float dt)
	{
		if (Main.UserInfo.settings.binds.holdSit)
		{
			this.HoldSitCHeck(UInput, dt);
		}
		else
		{
			this.PressSitCheck(UInput, dt);
		}
	}

	// Token: 0x06000C59 RID: 3161 RVA: 0x00095D2C File Offset: 0x00093F2C
	private void PressSitCheck(CWInput UInput, float dt)
	{
		if (UInput.GetKeyDown(UKeyCode.sit, true) && this.hitInfo.distance < 1.2f && this.state.sitTime <= 0f)
		{
			if (!this.state.isSeat)
			{
				this.state.isSeat = true;
				this.OnSeat();
			}
			else
			{
				this.state.isSeat = false;
				this.OnStand();
			}
			this.state.sitTime = 0.2f;
			return;
		}
		this.state.sitTime -= dt;
	}

	// Token: 0x06000C5A RID: 3162 RVA: 0x00095DD0 File Offset: 0x00093FD0
	private void HoldSitCHeck(CWInput UInput, float dt)
	{
		if (this.hitInfo.distance < 1.2f && this.state.sitTime <= 0f)
		{
			if (!this.state.isSeat && UInput.GetKey(UKeyCode.sit, true))
			{
				this.state.isSeat = true;
				this.OnSeat();
			}
			else if (this.state.isSeat && !UInput.GetKey(UKeyCode.sit, true))
			{
				this.state.isSeat = false;
				this.OnStand();
			}
			this.state.sitTime = 0.2f;
			return;
		}
		this.state.sitTime -= dt;
	}

	// Token: 0x06000C5B RID: 3163 RVA: 0x00095E90 File Offset: 0x00094090
	protected void Move(CWInput UInput, float dt)
	{
		float num = 0f;
		float num2 = 0f;
		bool flag = this.RayCastGroundCheck();
		if (this.IsGrounded || CVars.FlyControl || flag)
		{
			if (UInput.GetKey(UKeyCode.up, true))
			{
				num += 1f;
			}
			if (UInput.GetKey(UKeyCode.down, true))
			{
				num -= 1f;
			}
			if (UInput.GetKey(UKeyCode.right, true))
			{
				num2 += 1f;
			}
			if (UInput.GetKey(UKeyCode.left, true))
			{
				num2 -= 1f;
			}
		}
		Vector2 vector = new Vector2(num2, num);
		vector.Normalize();
		Quaternion rotation = Quaternion.Euler(270f, this.state.euler.y, 0f);
		Vector3 vector2 = rotation * new Vector3(vector.x, -vector.y, 0f);
		this.SitCheck(UInput, dt);
		if (Main.UserInfo.settings.binds.holdWalk)
		{
			this.state.isWalk = UInput.GetKey(UKeyCode.walk, true);
		}
		else if (UInput.GetKeyDown(UKeyCode.walk, true))
		{
			this.state.isWalk = !this.state.isWalk;
		}
		if (this.IsGrounded)
		{
			this.state.velocity.y = 0f;
		}
		bool flag2 = (this.state.isGrounded || this.IsGrounded || flag) && !this.state.isSeat;
		if (UInput.GetKeyDown(UKeyCode.jump, true) && flag2)
		{
			this.state.moveDir = vector2;
			if (this.IsGrounded)
			{
				this.OnJumpUp();
				this.state.jumpState = JumpState._Jump_Up;
			}
			this.state.velocity.y = Utility.CalculateJumpVerticalSpeed();
			MoveState moveState = this.state;
			moveState.velocity.y = moveState.velocity.y * this.player.frog;
			this._timeSinceLastJump = Time.fixedTime;
		}
		if (this.state.isGrounded)
		{
			this.state.moveDir = vector2;
		}
		else
		{
			this.state.moveDir += vector2 * 0.01f;
		}
		this.PrepareVelocity();
		Vector3 vector3 = this.state.moveDir * CVars.g_runSpeed * this.state.speedReducer * this.player.boots * this.state.jumpReducer;
		if (this.state.isSeat)
		{
			vector3 *= CVars.g_seatMult * this.seatBonusMult;
		}
		else if (this.state.isWalk)
		{
			vector3 *= CVars.g_walkMult;
		}
		else if (num < 0f || (num == 0f && (num2 > 0f || num2 < 0f)))
		{
			vector3 *= 0.85f;
		}
		if ((BIT.AND((int)this.player.playerInfo.buffs, 524288) || BIT.AND((int)this.player.playerInfo.buffs, 2097152)) && Peer.Info.Hardcore)
		{
			vector3 *= CVars.g_walkMult / 2f;
		}
		if (BIT.AND((int)this.player.playerInfo.buffs, 32768))
		{
			vector3 *= CVars.g_walkMult * 0.7f;
		}
		this.PostVelocity(ref vector3);
		if (this.state.velocity.y < -10f)
		{
			this.state.velocity.y = -10f;
		}
		MoveState moveState2 = this.state;
		moveState2.velocity.y = moveState2.velocity.y - 9.81f * dt;
		vector3.y = this.state.velocity.y;
		bool flag3 = false;
		Collider[] array = Physics.OverlapSphere(this.DownCap, CVars.px_capsuleradius, 1 << LayerMask.NameToLayer("triggers"));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].transform.name == "wind")
			{
				flag3 = true;
				this.state.wind += array[i].transform.forward * dt * CVars.px_windpower;
			}
		}
		this.state.wind *= 0.95f;
		if (flag3)
		{
			this.state.wind.y = 0f;
			vector3 += this.state.wind;
		}
		else if (this.state.wind.magnitude < dt)
		{
			this.state.wind = Vector3.zero;
		}
		this.controller.Move(vector3 * dt);
		this.state.pos = this.controllerTransform.position;
		this.state.isGrounded = this.IsGrounded;
		this.state.isFly = !this.IsGrounded;
		this.prevPosition = this.state.pos;
		this.proxy.position = this.state.pos;
		this.OnGrounded(vector3, dt);
	}

	// Token: 0x06000C5C RID: 3164 RVA: 0x00096448 File Offset: 0x00094648
	public virtual void Init(BaseNetPlayer player, Vector3 position)
	{
		this.player = player;
		this.proxy = base.transform.FindChild("proxy");
		this.controller = base.transform.GetComponent<CharacterController>();
		if (this.controller == null)
		{
			this.controller = base.transform.gameObject.AddComponent<CharacterController>();
			this.controller.slopeLimit = 50f;
			this.controller.stepOffset = 0.35f;
			this.controller.radius = 0.4f;
			this.controller.height = 1.74f;
		}
		this.controller.gameObject.layer = base.gameObject.layer;
		this.controller.enabled = true;
		this.controllerTransform = this.controller.transform;
		this.Position = position;
	}

	// Token: 0x06000C5D RID: 3165 RVA: 0x0009652C File Offset: 0x0009472C
	private bool RayCastGroundCheck()
	{
		Bounds bounds = base.collider.bounds;
		Vector3 origin = bounds.center - new Vector3(0f, bounds.extents.y, 0f);
		RaycastHit raycastHit;
		if (Physics.Raycast(origin, new Vector3(0f, -1f, 0f), out raycastHit, float.PositiveInfinity))
		{
			bool flag = Time.fixedTime - this._timeSinceLastJump >= 1f;
			return (double)raycastHit.distance <= 0.5 && flag;
		}
		return false;
	}

	// Token: 0x06000C5E RID: 3166 RVA: 0x000965CC File Offset: 0x000947CC
	public virtual void Tick(float dt)
	{
		this.Move(this.player.UInput, dt);
	}

	// Token: 0x06000C5F RID: 3167 RVA: 0x000965E0 File Offset: 0x000947E0
	public virtual void Recover(MoveState state)
	{
		state.Clone(state);
	}

	// Token: 0x06000C60 RID: 3168 RVA: 0x000965EC File Offset: 0x000947EC
	protected virtual void OnGrounded(Vector3 speed, float dt)
	{
		if (this.state.isGrounded)
		{
			this.state.velocity.y = 0f;
			if (this.state.jumpState != JumpState.None)
			{
				this.state.jumpReducer *= 0.25f;
			}
			this.state.jumpState = JumpState.None;
		}
		if (this.state.jumpState == JumpState.None)
		{
			this.state.jumpReducer = Mathf.Min(this.state.jumpReducer + dt / 1f, 1f);
		}
		if (this.state.hiPos == float.NaN)
		{
			this.state.hiPos = this.state.pos.y;
		}
		if (!this.state.isGrounded)
		{
			if (this.state.hiPos < this.state.pos.y)
			{
				this.state.hiPos = this.state.pos.y;
			}
		}
		else
		{
			this.state.hiPos = this.state.pos.y;
		}
	}

	// Token: 0x06000C61 RID: 3169 RVA: 0x00096728 File Offset: 0x00094928
	protected virtual void OnStand()
	{
	}

	// Token: 0x06000C62 RID: 3170 RVA: 0x0009672C File Offset: 0x0009492C
	protected virtual void OnSeat()
	{
	}

	// Token: 0x06000C63 RID: 3171 RVA: 0x00096730 File Offset: 0x00094930
	protected virtual void OnJumpUp()
	{
	}

	// Token: 0x06000C64 RID: 3172 RVA: 0x00096734 File Offset: 0x00094934
	protected virtual void PrepareVelocity()
	{
		this.state.speedReducer = this.player.Ammo.SpeedReducer;
	}

	// Token: 0x06000C65 RID: 3173 RVA: 0x00096754 File Offset: 0x00094954
	protected virtual void PostVelocity(ref Vector3 speed)
	{
	}

	// Token: 0x06000C66 RID: 3174 RVA: 0x00096758 File Offset: 0x00094958
	protected virtual void StepSound(RaycastHit hit)
	{
	}

	// Token: 0x06000C67 RID: 3175 RVA: 0x0009675C File Offset: 0x0009495C
	[Obfuscation(Exclude = true)]
	protected void PlayStep()
	{
		if (this.player.playerInfo.skillUnlocked(Skills.silwalk) || BIT.AND((int)this.player.playerInfo.buffs, 536870912))
		{
			return;
		}
		if (this.state.jumpState == JumpState._Jump_Up)
		{
			return;
		}
		if (this.state.jumpState == JumpState._Jump_Fly)
		{
			return;
		}
		if (base.IsInvoking("PlayStep"))
		{
			return;
		}
		base.CancelInvoke();
		if (this.state.isSeat || this.state.isWalk)
		{
			return;
		}
		Ray ray = new Ray(this.state.pos, -Vector3.up);
		RaycastHit[] array = Physics.RaycastAll(ray, 10f, PhysicsUtility.level_layers);
		if (array.Length == 0)
		{
			return;
		}
		Array.Sort(array, new RaycastSorter());
		this.StepSound(array[0]);
	}

	// Token: 0x06000C68 RID: 3176 RVA: 0x00096850 File Offset: 0x00094A50
	public virtual void CallLateUpdate()
	{
	}

	// Token: 0x04000DD5 RID: 3541
	private const float JumpCoolDown = 1f;

	// Token: 0x04000DD6 RID: 3542
	public MoveState state = new MoveState();

	// Token: 0x04000DD7 RID: 3543
	public float seatBonusMult = 1f;

	// Token: 0x04000DD8 RID: 3544
	protected bool sit;

	// Token: 0x04000DD9 RID: 3545
	protected BaseNetPlayer player;

	// Token: 0x04000DDA RID: 3546
	public Transform spine;

	// Token: 0x04000DDB RID: 3547
	public Transform proxy;

	// Token: 0x04000DDC RID: 3548
	public Transform root;

	// Token: 0x04000DDD RID: 3549
	public Transform rootCamera;

	// Token: 0x04000DDE RID: 3550
	protected CharacterController controller;

	// Token: 0x04000DDF RID: 3551
	protected Transform controllerTransform;

	// Token: 0x04000DE0 RID: 3552
	private RaycastHit hitInfo = default(RaycastHit);

	// Token: 0x04000DE1 RID: 3553
	protected Vector3 prevPosition = CVars.h_v3infinity;

	// Token: 0x04000DE2 RID: 3554
	private float _timeSinceLastJump;
}
