using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
[Serializable]
internal class GraphicValue
{
	// Token: 0x060002CD RID: 717 RVA: 0x00014FBC File Offset: 0x000131BC
	public void Clear()
	{
		this.xy = new Vector2[1];
		this.xy[0].x = -1f;
		this.xy[0].y = -1f;
		this.startTime = -1f;
		this.prevValue = -1f;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00015018 File Offset: 0x00013218
	public void InitTimer(float maxTime)
	{
		this.startTime = Time.realtimeSinceStartup;
		this.prevValue = -1f;
		this.xy = new Vector2[2];
		this.xy[0] = Vector2.zero;
		this.xy[1] = new Vector2(maxTime, maxTime);
	}

	// Token: 0x060002CF RID: 719 RVA: 0x00015078 File Offset: 0x00013278
	public void InitFastTimer(float realTime, float requiredTime)
	{
		this.startTime = Time.realtimeSinceStartup;
		this.prevValue = -1f;
		this.xy = new Vector2[2];
		this.xy[0] = Vector2.zero;
		this.xy[1] = new Vector2(realTime, requiredTime);
	}

	// Token: 0x060002D0 RID: 720 RVA: 0x000150D8 File Offset: 0x000132D8
	public void ReInitTimer(float maxTime)
	{
		this.startTime = Time.realtimeSinceStartup;
		this.prevValue = -1f;
		this.xy = new Vector2[2];
		this.xy[0] = Vector2.zero;
		this.xy[1] = new Vector2(maxTime, maxTime);
	}

	// Token: 0x060002D1 RID: 721 RVA: 0x00015138 File Offset: 0x00013338
	public void Init(Vector2[] v)
	{
		this.startTime = Time.realtimeSinceStartup;
		this.prevValue = -1f;
		this.xy = new Vector2[v.Length];
		v.CopyTo(this.xy, 0);
	}

	// Token: 0x060002D2 RID: 722 RVA: 0x0001516C File Offset: 0x0001336C
	public float Lenght()
	{
		if (this.xy.Length == 0)
		{
			return -1f;
		}
		return this.xy[this.xy.Length - 1].x;
	}

	// Token: 0x060002D3 RID: 723 RVA: 0x0001519C File Offset: 0x0001339C
	public bool ExistTime()
	{
		return this.xy.Length != 0 && this.GetTime() != -2f;
	}

	// Token: 0x060002D4 RID: 724 RVA: 0x000151CC File Offset: 0x000133CC
	public bool Exist()
	{
		return this.Get() > 0f;
	}

	// Token: 0x060002D5 RID: 725 RVA: 0x000151E4 File Offset: 0x000133E4
	public bool NotExist()
	{
		return !this.Exist();
	}

	// Token: 0x060002D6 RID: 726 RVA: 0x000151F0 File Offset: 0x000133F0
	public bool OnChange()
	{
		float num = this.Get();
		if (num == -2f && !this.fuck)
		{
			this.fuck = true;
			return true;
		}
		if (num < 0f)
		{
			return false;
		}
		if (this.prevValue != num)
		{
			this.prevValue = num;
			return true;
		}
		this.prevValue = num;
		return false;
	}

	// Token: 0x060002D7 RID: 727 RVA: 0x00015250 File Offset: 0x00013450
	public float Get()
	{
		float num = Time.realtimeSinceStartup - this.startTime;
		if (this.xy.Length == 0)
		{
			return -3f;
		}
		if (this.xy[this.xy.Length - 1].x < num)
		{
			return -2f;
		}
		for (int i = 1; i < this.xy.Length; i++)
		{
			if (this.xy[i].x > num)
			{
				float num2 = (num - this.xy[i - 1].x) / (this.xy[i].x - this.xy[i - 1].x);
				return this.xy[i - 1].y + num2 * (this.xy[i].y - this.xy[i - 1].y);
			}
		}
		return -1f;
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00015354 File Offset: 0x00013554
	public float GetTime()
	{
		float num = Time.realtimeSinceStartup - this.startTime;
		if (this.xy[this.xy.Length - 1].x < num)
		{
			return -2f;
		}
		for (int i = 1; i < this.xy.Length; i++)
		{
			if (this.xy[i].x > num)
			{
				return num;
			}
		}
		return -1f;
	}

	// Token: 0x04000342 RID: 834
	private Vector2[] xy = new Vector2[]
	{
		new Vector2(-1f, -1f)
	};

	// Token: 0x04000343 RID: 835
	private float startTime = -1f;

	// Token: 0x04000344 RID: 836
	private float prevValue = -1f;

	// Token: 0x04000345 RID: 837
	private bool fuck;
}
