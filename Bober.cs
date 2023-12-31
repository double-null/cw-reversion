using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
[Serializable]
internal class Bober
{
	// Token: 0x060011FC RID: 4604 RVA: 0x000C6D5C File Offset: 0x000C4F5C
	public Bober(float step)
	{
		this.step = step;
	}

	// Token: 0x17000266 RID: 614
	// (get) Token: 0x060011FD RID: 4605 RVA: 0x000C6D98 File Offset: 0x000C4F98
	private bool GroundedDelayed
	{
		get
		{
			return this.delay - Time.realtimeSinceStartup > 0f;
		}
	}

	// Token: 0x060011FE RID: 4606 RVA: 0x000C6DB8 File Offset: 0x000C4FB8
	public bool NextStep()
	{
		if (this.sum > this.nextStep)
		{
			this.nextStep = this.sum + this.step;
			return true;
		}
		return false;
	}

	// Token: 0x060011FF RID: 4607 RVA: 0x000C6DE4 File Offset: 0x000C4FE4
	public void AdvanceStep(Vector3 v, float dt, bool isGrounded, float aimSpeed)
	{
		this.k -= dt * 3f;
		this.k = Mathf.Clamp01(this.k);
		if (isGrounded)
		{
			this.delay = Time.realtimeSinceStartup + 0.3f;
		}
		if (this.GroundedDelayed)
		{
			if (v.magnitude != 0f)
			{
				this.k += dt * 5f;
			}
			this.sum += v.magnitude * dt;
			if (this.sum > this.step * 2f)
			{
				this.sum -= this.step * 2f;
				this.nextStep -= this.step * 2f;
			}
			this.boberSum += v.magnitude * dt * 0.7f;
			if (this.boberSum > 6.2831855f)
			{
				this.boberSum -= 6.2831855f;
			}
		}
	}

	// Token: 0x06001200 RID: 4608 RVA: 0x000C6EFC File Offset: 0x000C50FC
	public void Update(float aimSpeed)
	{
		this.waveslice = Mathf.Sin(this.sum / this.step * 3.1415927f);
		this.rotation = Quaternion.Euler(0f, -Mathf.Sin(this.boberSum) * 1.5f * aimSpeed * this.k, 0f);
	}

	// Token: 0x06001201 RID: 4609 RVA: 0x000C6F58 File Offset: 0x000C5158
	public Vector3 Position1(float aimSpeed)
	{
		return new Vector3(0f, 0f, -Mathf.Abs(this.waveslice) / 20f * aimSpeed) * this.k;
	}

	// Token: 0x06001202 RID: 4610 RVA: 0x000C6F94 File Offset: 0x000C5194
	public Vector3 Position2(float aimSpeed)
	{
		return new Vector3(this.waveslice * 0.01f * aimSpeed, 0f, -this.waveslice * 0.01f * 0.3f * aimSpeed - Mathf.Abs(this.waveslice) / 20f * aimSpeed) * this.k;
	}

	// Token: 0x04001183 RID: 4483
	private const float bobbingAmount = 0.01f;

	// Token: 0x04001184 RID: 4484
	public float step = 1.6f;

	// Token: 0x04001185 RID: 4485
	private float sum;

	// Token: 0x04001186 RID: 4486
	private float boberSum;

	// Token: 0x04001187 RID: 4487
	private float nextStep = 1.6f;

	// Token: 0x04001188 RID: 4488
	private float waveslice;

	// Token: 0x04001189 RID: 4489
	public Quaternion rotation = Quaternion.identity;

	// Token: 0x0400118A RID: 4490
	private float k = 1f;

	// Token: 0x0400118B RID: 4491
	private float delay;
}
