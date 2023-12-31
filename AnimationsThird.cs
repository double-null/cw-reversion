using System;
using UnityEngine;

// Token: 0x020001B5 RID: 437
[AddComponentMenu("Scripts/Game/Components/AnimationsThird")]
public class AnimationsThird : PoolableBehaviour
{
	// Token: 0x06000EF6 RID: 3830 RVA: 0x000ADC08 File Offset: 0x000ABE08
	public override void OnPoolSpawn()
	{
		Animation component = base.gameObject.GetComponent<Animation>();
		if (component && !Peer.Dedicated)
		{
			component.enabled = true;
		}
		base.OnPoolSpawn();
	}

	// Token: 0x06000EF7 RID: 3831 RVA: 0x000ADC44 File Offset: 0x000ABE44
	public override void OnPoolDespawn()
	{
		if (!Peer.Dedicated)
		{
			base.gameObject.GetComponent<Animation>().enabled = false;
		}
		this._player = null;
		this._ytemp = float.NaN;
		this._angle = "_45";
		this._direction = "_Wait";
		Transform transform = Utility.FindHierarchy(base.transform, "Player Main Camera");
		if (transform)
		{
			transform.transform.parent = null;
			transform.transform.position = Vector3.zero;
			transform.transform.eulerAngles = new Vector3(0f, -180f, 0f);
			SingletoneForm<PoolManager>.Instance[transform.name].Despawn(transform.GetComponent<PoolItem>());
		}
		this._hierarchy.Reset();
		if (this._lod)
		{
			this._lod.locked = true;
			this._lod.Hide();
		}
		if (this._rigid != null)
		{
			foreach (Rigidbody rigidbody in this._rigid)
			{
				rigidbody.isKinematic = true;
				rigidbody.Sleep();
			}
		}
		base.OnPoolDespawn();
		this._lodGroup = base.gameObject.GetComponent<LODGroup>();
		if (this._lodGroup != null)
		{
			this._lodGroup.enabled = false;
		}
	}

	// Token: 0x17000237 RID: 567
	// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000ADDA4 File Offset: 0x000ABFA4
	private bool IsWait
	{
		get
		{
			return !this._player.UInput.GetKey(UKeyCode.up, true) && !this._player.UInput.GetKey(UKeyCode.down, true) && !this._player.UInput.GetKey(UKeyCode.left, true) && !this._player.UInput.GetKey(UKeyCode.right, true);
		}
	}

	// Token: 0x17000238 RID: 568
	// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000ADE10 File Offset: 0x000AC010
	private AnimWeapon Weapon
	{
		get
		{
			if (this._player.Ammo.CurrentWeapon == null)
			{
				return AnimWeapon.AsRifle;
			}
			return (this._player.Ammo.CurrentWeapon.weaponUseType != WeaponUseType.Secondary) ? AnimWeapon.AsRifle : AnimWeapon.Pistol;
		}
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000ADE68 File Offset: 0x000AC068
	private string WeaponString
	{
		get
		{
			if (this._player == null || this._player.Ammo == null || this._player.Ammo.CurrentWeapon == null)
			{
				return "AsRifle";
			}
			return (this._player.Ammo.CurrentWeapon.weaponUseType != WeaponUseType.Secondary) ? "AsRifle" : "Pistol";
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000ADEE8 File Offset: 0x000AC0E8
	private string PoseString
	{
		get
		{
			return (!this._player.Controller.state.isSeat) ? "_Stand" : "_Seat";
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000ADF14 File Offset: 0x000AC114
	private string WalkString
	{
		get
		{
			return (!this._player.Controller.state.isWalk && !this._player.Controller.state.isSeat) ? "_Run" : "_Walk";
		}
	}

	// Token: 0x06000EFD RID: 3837 RVA: 0x000ADF64 File Offset: 0x000AC164
	private string DirectionString(Vector2 dir)
	{
		dir.y *= 0.98f;
		if (dir.y > dir.x)
		{
			return (dir.y <= -dir.x) ? "_Left" : "_Front";
		}
		return (dir.y <= -dir.x) ? "_Back" : "_Right";
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000ADFE0 File Offset: 0x000AC1E0
	private bool FrontRight
	{
		get
		{
			return this._player.UInput.GetKey(UKeyCode.up, true) && this._player.UInput.GetKey(UKeyCode.right, true);
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000EFF RID: 3839 RVA: 0x000AE01C File Offset: 0x000AC21C
	private bool FrontLeft
	{
		get
		{
			return this._player.UInput.GetKey(UKeyCode.up, true) && this._player.UInput.GetKey(UKeyCode.left, true);
		}
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000AE058 File Offset: 0x000AC258
	private bool BackRight
	{
		get
		{
			return this._player.UInput.GetKey(UKeyCode.down, true) && this._player.UInput.GetKey(UKeyCode.right, true);
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000F01 RID: 3841 RVA: 0x000AE094 File Offset: 0x000AC294
	private bool BacktLeft
	{
		get
		{
			return this._player.UInput.GetKey(UKeyCode.down, true) && this._player.UInput.GetKey(UKeyCode.left, true);
		}
	}

	// Token: 0x06000F02 RID: 3842 RVA: 0x000AE0D0 File Offset: 0x000AC2D0
	private void SetLoop(AnimationState s, int layer = 0)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Loop;
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x000AE0E0 File Offset: 0x000AC2E0
	private void SetOnce(Animation animation, AnimationState s, int layer = 1)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Once;
		Transform mix = animation.transform.FindChild("NPC_ROOT/NPC_Orientation/NPC_Pelvis/NPC_Spine");
		s.AddMixingTransform(mix);
		s.blendMode = AnimationBlendMode.Blend;
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x000AE11C File Offset: 0x000AC31C
	private void SetOnceHigh(Animation animation, AnimationState s, int layer = 2)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Once;
		Transform mix = animation.transform.FindChild("NPC_ROOT/NPC_Orientation/NPC_Pelvis/NPC_Spine");
		s.AddMixingTransform(mix);
		s.blendMode = AnimationBlendMode.Blend;
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x000AE158 File Offset: 0x000AC358
	private void SetOnceAll(AnimationState s, int layer = 1)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Once;
		s.blendMode = AnimationBlendMode.Blend;
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x000AE170 File Offset: 0x000AC370
	private void SetLoopAll(AnimationState s, int layer = 1)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Loop;
		s.blendMode = AnimationBlendMode.Blend;
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x000AE188 File Offset: 0x000AC388
	private void SetOnceAdditive(AnimationState s, int layer = 1)
	{
		s.layer = layer;
		s.wrapMode = WrapMode.Once;
		s.blendMode = AnimationBlendMode.Additive;
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x000AE1A0 File Offset: 0x000AC3A0
	private void OnBecameVisible()
	{
		if (!Peer.Dedicated && this._anim != null && !this._anim.enabled)
		{
			this._anim.enabled = true;
		}
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x000AE1DC File Offset: 0x000AC3DC
	private void OnBecameInvisible()
	{
		if (!Peer.Dedicated && this._anim != null && this._anim.enabled)
		{
			this._anim.enabled = false;
		}
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x000AE218 File Offset: 0x000AC418
	protected override void Awake()
	{
		base.Awake();
		this._anim = base.gameObject.GetComponent<Animation>();
		this._hierarchy.Init(base.transform);
		this._lod = base.GetComponent<AutoCustomLod>();
		this._rigid = base.GetComponentsInChildren<Rigidbody>(true);
		this.SetLoop(base.animation["Pistol_Stand_Run_Front"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Run_Back"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Run_Left"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Run_Right"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Walk_Front"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Walk_Back"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Walk_Left"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Walk_Right"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Wait"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Walk_Front"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Walk_Back"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Walk_Left"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Walk_Right"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Wait"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Rotate_45Right"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Rotate_45Left"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Rotate_90Right"], 0);
		this.SetLoop(base.animation["Pistol_Stand_Rotate_90Left"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Rotate_45Right"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Rotate_45Left"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Rotate_90Right"], 0);
		this.SetLoop(base.animation["Pistol_Seat_Rotate_90Left"], 0);
		this.SetOnceAdditive(base.animation["Pistol_Stand_Attack_Shot"], 1);
		this.SetOnce(base.animation, base.animation["Pistol_Stand_Grenade"], 1);
		this.SetOnce(base.animation, base.animation["Pistol_Stand_Reload"], 1);
		this.SetOnceAdditive(base.animation["Pistol_Stand_Hit_Body"], 1);
		this.SetOnceAdditive(base.animation["Pistol_Seat_Attack_Shot"], 1);
		this.SetOnce(base.animation, base.animation["Pistol_Seat_Grenade"], 1);
		this.SetOnce(base.animation, base.animation["Pistol_Seat_Reload"], 1);
		this.SetOnceAdditive(base.animation["Pistol_Seat_Hit_Body"], 1);
		this.SetOnceHigh(base.animation, base.animation["Pistol_Stand_Knife"], 2);
		this.SetOnceHigh(base.animation, base.animation["Pistol_Stand_Smena"], 2);
		this.SetOnceAll(base.animation["Pistol_Stand_Jump_Up"], 0);
		this.SetLoopAll(base.animation["Pistol_Stand_Jump_Fly"], 0);
		this.SetOnceAll(base.animation["Pistol_Stand_Jump_Land"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Run_Front"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Run_Back"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Run_Left"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Run_Right"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Walk_Front"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Walk_Back"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Walk_Left"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Walk_Right"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Wait"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Walk_Front"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Walk_Back"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Walk_Left"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Walk_Right"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Wait"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Rotate_45Right"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Rotate_45Left"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Rotate_90Right"], 0);
		this.SetLoop(base.animation["AsRifle_Stand_Rotate_90Left"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Rotate_45Right"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Rotate_45Left"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Rotate_90Right"], 0);
		this.SetLoop(base.animation["AsRifle_Seat_Rotate_90Left"], 0);
		this.SetOnceAdditive(base.animation["AsRifle_Stand_Attack_Shot"], 1);
		this.SetOnce(base.animation, base.animation["AsRifle_Stand_Grenade"], 1);
		this.SetOnce(base.animation, base.animation["AsRifle_Stand_Reload"], 1);
		this.SetOnceAdditive(base.animation["AsRifle_Stand_Hit_Body"], 1);
		this.SetOnceAdditive(base.animation["AsRifle_Seat_Attack_Shot"], 1);
		this.SetOnce(base.animation, base.animation["AsRifle_Seat_Grenade"], 1);
		this.SetOnce(base.animation, base.animation["AsRifle_Seat_Reload"], 1);
		this.SetOnceAdditive(base.animation["AsRifle_Seat_Hit_Body"], 1);
		this.SetOnceHigh(base.animation, base.animation["AsRifle_Stand_Knife"], 2);
		this.SetOnceHigh(base.animation, base.animation["AsRifle_Stand_Smena"], 2);
		this.SetOnceAll(base.animation["AsRifle_Stand_Jump_Up"], 0);
		this.SetLoopAll(base.animation["AsRifle_Stand_Jump_Fly"], 0);
		this.SetOnceAll(base.animation["AsRifle_Stand_Jump_Land"], 0);
	}

	// Token: 0x06000F0B RID: 3851 RVA: 0x000AE8F8 File Offset: 0x000ACAF8
	internal void Init(BaseNetPlayer player)
	{
		this._player = player;
		int layer = (!player.isPlayer) ? LayerMask.NameToLayer("client_ragdoll") : 0;
		foreach (Rigidbody rigidbody in this._rigid)
		{
			rigidbody.gameObject.layer = layer;
		}
	}

	// Token: 0x06000F0C RID: 3852 RVA: 0x000AE954 File Offset: 0x000ACB54
	public void RagDoll(Vector3 power)
	{
		int layer = LayerMask.NameToLayer("client_ragdoll");
		bool flag = power.x != 0f || power.y != 0f || power.x != 0f;
		for (int i = 0; i < this._rigid.Length; i++)
		{
			Rigidbody rigidbody = this._rigid[i];
			rigidbody.gameObject.layer = layer;
			rigidbody.isKinematic = false;
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			if (flag)
			{
				rigidbody.AddForce(power, ForceMode.Impulse);
			}
		}
	}

	// Token: 0x06000F0D RID: 3853 RVA: 0x000AE9FC File Offset: 0x000ACBFC
	public void PlayJump()
	{
		base.animation.CrossFade(this.WeaponString + this.PoseString + "_Jump_Fly", 0.25f);
	}

	// Token: 0x06000F0E RID: 3854 RVA: 0x000AEA30 File Offset: 0x000ACC30
	public void PlayIdleOut(float time)
	{
		string text = this.WeaponString + this.PoseString + "_Smena";
		if (base.animation[text] == null)
		{
			return;
		}
		if (this.Weapon == AnimWeapon.Pistol)
		{
			base.animation[text].speed = base.animation[text].length / time;
		}
		else if (this.Weapon == AnimWeapon.AsRifle)
		{
			base.animation[text].speed = base.animation[text].length / time;
		}
		base.animation.CrossFade(text, 0.05f);
	}

	// Token: 0x06000F0F RID: 3855 RVA: 0x000AEAEC File Offset: 0x000ACCEC
	public void PlayIdleOut(AnimWeapon animtype, float time)
	{
		string text = animtype.ToString();
		if (text == string.Empty)
		{
			return;
		}
		string text2 = text + this.PoseString + "_Smena";
		if (base.animation[text2] == null)
		{
			return;
		}
		base.animation[text2].speed = base.animation[text2].length / time;
		base.animation.CrossFade(text2, 0.05f);
	}

	// Token: 0x06000F10 RID: 3856 RVA: 0x000AEB78 File Offset: 0x000ACD78
	public void PlayGrenade(float time)
	{
		string text = this.WeaponString + this.PoseString + "_Grenade";
		base.animation[text].speed = base.animation[text].length / time * 0.45f;
		base.animation.CrossFade(text, 0.05f);
	}

	// Token: 0x06000F11 RID: 3857 RVA: 0x000AEBD8 File Offset: 0x000ACDD8
	public void PlayKnife(float time)
	{
		string text = this.WeaponString + this.PoseString + "_Knife";
		base.animation[text].speed = base.animation[text].length / time;
		base.animation.CrossFade(text, 0.05f, PlayMode.StopAll);
	}

	// Token: 0x06000F12 RID: 3858 RVA: 0x000AEC34 File Offset: 0x000ACE34
	public void PlayReload(float time)
	{
		string text = this.WeaponString + this.PoseString + "_Reload";
		base.animation[text].speed = base.animation[text].length / time;
		base.animation.CrossFade(text, 0.05f);
	}

	// Token: 0x06000F13 RID: 3859 RVA: 0x000AEC90 File Offset: 0x000ACE90
	public void PlayFire()
	{
		string text = this.WeaponString + this.PoseString + "_Attack_Shot";
		base.animation.Stop(text);
		base.animation.Play(text);
	}

	// Token: 0x06000F14 RID: 3860 RVA: 0x000AECD0 File Offset: 0x000ACED0
	public void PlayHit()
	{
		string text = this.WeaponString + this.PoseString + "_Hit_Body";
		base.animation[text].speed = 0.8f;
		base.animation.Stop(text);
		base.animation.CrossFade(text, 0.1f);
	}

	// Token: 0x06000F15 RID: 3861 RVA: 0x000AED28 File Offset: 0x000ACF28
	public void PlayRotate(string angle, string direction)
	{
		string animation = string.Concat(new string[]
		{
			this.WeaponString,
			this.PoseString,
			"_Rotate",
			angle,
			direction
		});
		base.animation.CrossFade(animation, CVars.e_movelerk, PlayMode.StopSameLayer);
	}

	// Token: 0x06000F16 RID: 3862 RVA: 0x000AED78 File Offset: 0x000ACF78
	public void PlayMoving()
	{
		Vector3 v = this._player.PlayerTransform.position - this._lastPos;
		this._lastPos = this._player.PlayerTransform.position;
		v = this._player.PlayerTransform.worldToLocalMatrix * v;
		this._sumX.AddValue(v.x);
		this._sumY.AddValue(v.z);
		Vector2 dir = new Vector2(this._sumX.Sum / 8f, this._sumY.Sum / 8f);
		float num = dir.magnitude / Time.deltaTime;
		bool flag = num < 0.01f;
		bool flag2 = !this._player.Controller.IsGrounded;
		RaycastHit raycastHit = default(RaycastHit);
		if (flag2 && this._player.GetType() == typeof(EntityNetPlayer))
		{
			if (this._renderer == null)
			{
				this._renderer = this._player.gameObject.GetComponentInChildren<Renderer>();
			}
			Vector3 lastPos = this._lastPos;
			lastPos.y -= this._renderer.bounds.extents.y - 0.05f;
			if (Physics.Raycast(lastPos, -Vector3.up, out raycastHit, 10.1f, PhysicsUtility.level_layers))
			{
				flag2 = (raycastHit.distance > 0.2f);
			}
		}
		string str = (!flag) ? (this.WalkString + this.DirectionString(dir)) : "_Wait";
		string text = this.WeaponString + this.PoseString + str;
		if (flag2)
		{
			base.animation[text].speed = Mathf.Max(1f - raycastHit.distance * 0.1f, 0f) * CVars.e_move * this._player.Controller.SpeedMult / 2.7f;
		}
		else if (flag)
		{
			base.animation[text].speed = CVars.e_move * 0.4f;
		}
		else if (BIT.AND((int)this._player.playerInfo.buffs, 524288))
		{
			base.animation[text].speed = CVars.e_move * this._player.Controller.SpeedMult / 2.7f;
		}
		else
		{
			base.animation[text].speed = CVars.e_move * this._player.Controller.SpeedMult;
		}
		base.animation.CrossFade(text, 0.3f, PlayMode.StopSameLayer);
	}

	// Token: 0x06000F17 RID: 3863 RVA: 0x000AF054 File Offset: 0x000AD254
	public void CallLateUpdate()
	{
		if (this._player == null)
		{
			return;
		}
		this.PlayMoving();
		Vector3 euler = this._player.Euler;
		bool isWait = this.IsWait;
		if (this._player.isPlayer)
		{
			return;
		}
		Transform spine = this._player.Controller.spine;
		if (this._currentAngle != euler.x)
		{
			this._lastAngle = this._currentAngle;
			this._currentAngle = euler.x;
			this._lastTime = this._currentTime;
			this._currentTime = Time.time;
			this._timeDelta = this._currentTime - this._lastTime;
			this._timeDelta = Mathf.Min(this._timeDelta, 0.5f);
		}
		float t = (Time.time - this._currentTime) / this._timeDelta;
		float value = Mathf.Lerp(this._lastAngle, this._currentAngle, t);
		Vector3 eulerAngles = spine.eulerAngles;
		eulerAngles.z -= 90f + Mathf.Clamp(value, 210f, 330f);
		spine.eulerAngles = eulerAngles;
	}

	// Token: 0x04000F56 RID: 3926
	private const int SmoothLength = 8;

	// Token: 0x04000F57 RID: 3927
	private readonly Hierarchy _hierarchy = new Hierarchy();

	// Token: 0x04000F58 RID: 3928
	private Rigidbody[] _rigid;

	// Token: 0x04000F59 RID: 3929
	private AutoCustomLod _lod;

	// Token: 0x04000F5A RID: 3930
	private BaseNetPlayer _player;

	// Token: 0x04000F5B RID: 3931
	private float _ytemp = float.NaN;

	// Token: 0x04000F5C RID: 3932
	private string _angle = "_45";

	// Token: 0x04000F5D RID: 3933
	private string _direction = "_Wait";

	// Token: 0x04000F5E RID: 3934
	private Vector3 _lastPos;

	// Token: 0x04000F5F RID: 3935
	private LODGroup _lodGroup;

	// Token: 0x04000F60 RID: 3936
	private Animation _anim;

	// Token: 0x04000F61 RID: 3937
	private Renderer _renderer;

	// Token: 0x04000F62 RID: 3938
	private readonly SumArray _sumX = new SumArray(8);

	// Token: 0x04000F63 RID: 3939
	private readonly SumArray _sumY = new SumArray(8);

	// Token: 0x04000F64 RID: 3940
	private float _currentAngle;

	// Token: 0x04000F65 RID: 3941
	private float _lastAngle;

	// Token: 0x04000F66 RID: 3942
	private float _currentTime;

	// Token: 0x04000F67 RID: 3943
	private float _lastTime;

	// Token: 0x04000F68 RID: 3944
	private float _timeDelta;
}
