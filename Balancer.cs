using System;
using UnityEngine;

// Token: 0x0200024A RID: 586
[Serializable]
internal class Balancer : ReusableClass<Balancer>
{
	// Token: 0x17000264 RID: 612
	// (get) Token: 0x060011F7 RID: 4599 RVA: 0x000C6B14 File Offset: 0x000C4D14
	private int Rate
	{
		get
		{
			return CVars.g_tickrate;
		}
	}

	// Token: 0x17000265 RID: 613
	// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000C6B1C File Offset: 0x000C4D1C
	public int Count
	{
		get
		{
			int result = 0;
			if (this.GameStartTimeSinceStartup == -1f)
			{
				this.GameStartTimeSinceStartup = Time.realtimeSinceStartup;
			}
			if (Time.realtimeSinceStartup > this.GameStartTimeSinceStartup + 1f)
			{
				this.GameStartTimeSinceStartup += 1f;
				this.ticked -= this.Rate;
			}
			int num = (int)((Time.realtimeSinceStartup - this.GameStartTimeSinceStartup) / (1f / (float)this.Rate));
			int num2 = num - this.ticked;
			if (num2 > 0)
			{
				if (this.stepDelay == 0)
				{
					if (num2 > this.Rate / 2)
					{
						this.maxHeight = 3;
					}
					else if (num2 > this.Rate / 4)
					{
						this.maxHeight = 2;
					}
					else if (num2 > 2)
					{
						this.maxHeight = 1;
					}
					else
					{
						this.maxHeight = 0;
					}
					this.stepDelay = this.maxHeight;
					if (this.stepDelay != 0)
					{
						this.grow = true;
					}
				}
				if (this.grow)
				{
					this.currentHeight++;
				}
				else if (this.currentHeight != 0)
				{
					this.currentHeight--;
				}
				if (this.currentHeight == this.maxHeight && this.grow)
				{
					this.grow = false;
				}
				if (this.currentHeight == 0 && this.stepDelay != 0)
				{
					this.stepDelay--;
				}
				result = this.currentHeight + 1;
			}
			else if (num2 <= 0)
			{
			}
			return result;
		}
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x000C6CB8 File Offset: 0x000C4EB8
	public virtual void AdvanceTick()
	{
		this.ticked++;
	}

	// Token: 0x060011FA RID: 4602 RVA: 0x000C6CC8 File Offset: 0x000C4EC8
	public void Clear()
	{
		this.grow = false;
		this.stepDelay = 0;
		this.currentHeight = 0;
		this.maxHeight = 0;
		this.ticked = 0;
		this.GameStartTimeSinceStartup = -1f;
	}

	// Token: 0x060011FB RID: 4603 RVA: 0x000C6D04 File Offset: 0x000C4F04
	public void Clone(Balancer i)
	{
		this.grow = i.grow;
		this.stepDelay = i.stepDelay;
		this.currentHeight = i.currentHeight;
		this.maxHeight = i.maxHeight;
		this.ticked = i.ticked;
		this.GameStartTimeSinceStartup = i.GameStartTimeSinceStartup;
	}

	// Token: 0x0400117D RID: 4477
	private bool grow;

	// Token: 0x0400117E RID: 4478
	private int stepDelay;

	// Token: 0x0400117F RID: 4479
	private int currentHeight;

	// Token: 0x04001180 RID: 4480
	private int maxHeight;

	// Token: 0x04001181 RID: 4481
	private int ticked;

	// Token: 0x04001182 RID: 4482
	private float GameStartTimeSinceStartup = -1f;
}
