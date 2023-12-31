using System;
using UnityEngine;

// Token: 0x020001BE RID: 446
[AddComponentMenu("Scripts/Game/Components/CameraShaker")]
internal class CameraShaker : PoolableBehaviour
{
	// Token: 0x06000F59 RID: 3929 RVA: 0x000B08AC File Offset: 0x000AEAAC
	public override void OnPoolDespawn()
	{
		this.pos = Vector3.zero;
		this.timeGNAME = new GraphicValue();
		this.realTime = 1f;
		this.power = 0f;
		this.controller = null;
		base.OnPoolDespawn();
	}

	// Token: 0x06000F5A RID: 3930 RVA: 0x000B08E8 File Offset: 0x000AEAE8
	protected override void Awake()
	{
		this.shakeXKeys = this.shakeX.keys;
		this.shakeYKeys = this.shakeY.keys;
		this.shakePowerKeys = this.shakePower.keys;
	}

	// Token: 0x06000F5B RID: 3931 RVA: 0x000B0920 File Offset: 0x000AEB20
	private void OnEnable()
	{
		if (this.shakeXKeys == null)
		{
			this.shakeXKeys = this.shakeX.keys;
		}
		if (this.shakeYKeys == null)
		{
			this.shakeYKeys = this.shakeY.keys;
		}
		if (this.shakePowerKeys == null)
		{
			this.shakePowerKeys = this.shakePower.keys;
		}
	}

	// Token: 0x06000F5C RID: 3932 RVA: 0x000B0984 File Offset: 0x000AEB84
	public void InitShake(Vector3 pos)
	{
		if (this.controller == null)
		{
			this.controller = base.GetComponent<BaseMoveController>();
		}
		this.power = this.shakePower.Evaluate((this.controller.Position - pos).magnitude);
		this.timeGNAME.InitFastTimer(this.realTime, this.shakeXKeys[this.shakeXKeys.Length - 1].time);
	}

	// Token: 0x06000F5D RID: 3933 RVA: 0x000B0A04 File Offset: 0x000AEC04
	public void InitShake(float Power)
	{
		this.power = this.shakePower.Evaluate(40f - 40f * Power * 0.8f);
		if (this.power > 1.7f)
		{
			this.power = 1.7f;
		}
		if (this.power < 0.2f)
		{
			this.power = 0.2f;
		}
		this.timeGNAME.InitFastTimer(this.realTime, this.shakeXKeys[this.shakeXKeys.Length - 1].time);
	}

	// Token: 0x06000F5E RID: 3934 RVA: 0x000B0A98 File Offset: 0x000AEC98
	public void CustomUpdate()
	{
		if (this.timeGNAME.ExistTime())
		{
			this.pos.x = this.shakeX.Evaluate(this.timeGNAME.Get()) * this.power;
			this.pos.z = this.shakeY.Evaluate(this.timeGNAME.Get()) * this.power;
		}
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x06000F5F RID: 3935 RVA: 0x000B0B08 File Offset: 0x000AED08
	public float JumpEuler
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x04000FA5 RID: 4005
	public AnimationCurve shakeX;

	// Token: 0x04000FA6 RID: 4006
	public AnimationCurve shakeY;

	// Token: 0x04000FA7 RID: 4007
	public AnimationCurve shakePower;

	// Token: 0x04000FA8 RID: 4008
	private BaseMoveController controller;

	// Token: 0x04000FA9 RID: 4009
	[HideInInspector]
	public Vector3 pos = Vector3.zero;

	// Token: 0x04000FAA RID: 4010
	private GraphicValue timeGNAME = new GraphicValue();

	// Token: 0x04000FAB RID: 4011
	public float realTime = 1f;

	// Token: 0x04000FAC RID: 4012
	private float power;

	// Token: 0x04000FAD RID: 4013
	private Keyframe[] shakeXKeys;

	// Token: 0x04000FAE RID: 4014
	private Keyframe[] shakeYKeys;

	// Token: 0x04000FAF RID: 4015
	private Keyframe[] shakePowerKeys;
}
