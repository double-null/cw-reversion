using System;
using UnityEngine;

// Token: 0x020001BA RID: 442
[Serializable]
public class AnimatedTransform
{
	// Token: 0x17000248 RID: 584
	// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000B015C File Offset: 0x000AE35C
	public float MaxTime
	{
		get
		{
			float num = 0f;
			if (this.x.length != 0 && this.x[this.x.length - 1].time > num)
			{
				num = this.x[this.x.length - 1].time;
			}
			if (this.y.length != 0 && this.y[this.y.length - 1].time > num)
			{
				num = this.y[this.y.length - 1].time;
			}
			if (this.z.length != 0 && this.z[this.z.length - 1].time > num)
			{
				num = this.z[this.z.length - 1].time;
			}
			if (this.ex.length != 0 && this.ex[this.ex.length - 1].time > num)
			{
				num = this.ex[this.ex.length - 1].time;
			}
			if (this.ey.length != 0 && this.ey[this.ey.length - 1].time > num)
			{
				num = this.ey[this.ey.length - 1].time;
			}
			if (this.ez.length != 0 && this.ez[this.ez.length - 1].time > num)
			{
				num = this.ez[this.ez.length - 1].time;
			}
			return num;
		}
	}

	// Token: 0x06000F46 RID: 3910 RVA: 0x000B0384 File Offset: 0x000AE584
	public void Initialize(Transform root)
	{
		this.transform = Utility.FindHierarchy(root, this.transformName);
	}

	// Token: 0x06000F47 RID: 3911 RVA: 0x000B0398 File Offset: 0x000AE598
	public void ResetTransform()
	{
		if (this.transform)
		{
			this.transform.localPosition = Vector3.zero;
			this.transform.localEulerAngles = Vector3.zero;
		}
	}

	// Token: 0x06000F48 RID: 3912 RVA: 0x000B03D8 File Offset: 0x000AE5D8
	public void Evalute(float time)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		if (this.x.length != 0)
		{
			num = this.x.Evaluate(time);
		}
		if (this.y.length != 0)
		{
			num2 = this.y.Evaluate(time);
		}
		if (this.z.length != 0)
		{
			num3 = this.z.Evaluate(time);
		}
		float num4 = 0f;
		float num5 = 0f;
		float num6 = 0f;
		if (this.ex.length != 0)
		{
			num4 = this.ex.Evaluate(time);
		}
		if (this.ey.length != 0)
		{
			num5 = this.ey.Evaluate(time);
		}
		if (this.ez.length != 0)
		{
			num6 = this.ez.Evaluate(time);
		}
		if (this.transform)
		{
			this.transform.localPosition += new Vector3(num, num2, num3);
			this.transform.localEulerAngles += new Vector3(num4, num5, num6);
		}
	}

	// Token: 0x04000F93 RID: 3987
	[HideInInspector]
	[NonSerialized]
	private Transform transform;

	// Token: 0x04000F94 RID: 3988
	public string transformName = string.Empty;

	// Token: 0x04000F95 RID: 3989
	public AnimationCurve x = new AnimationCurve();

	// Token: 0x04000F96 RID: 3990
	public AnimationCurve y = new AnimationCurve();

	// Token: 0x04000F97 RID: 3991
	public AnimationCurve z = new AnimationCurve();

	// Token: 0x04000F98 RID: 3992
	public AnimationCurve ex = new AnimationCurve();

	// Token: 0x04000F99 RID: 3993
	public AnimationCurve ey = new AnimationCurve();

	// Token: 0x04000F9A RID: 3994
	public AnimationCurve ez = new AnimationCurve();
}
