using System;
using UnityEngine;

// Token: 0x02000077 RID: 119
[Serializable]
internal class CircleRandom
{
	// Token: 0x06000266 RID: 614 RVA: 0x00013EDC File Offset: 0x000120DC
	public void InitNew(int count)
	{
		this.counts = count;
		this.prevI = 0;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00013EEC File Offset: 0x000120EC
	public void InitStatic(int count)
	{
		if (this.counts != -1)
		{
			return;
		}
		this.counts = count;
		this.prevI = 0;
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00013F0C File Offset: 0x0001210C
	public int Get()
	{
		int num = (int)(UnityEngine.Random.value * (float)this.counts);
		if (num == this.prevI)
		{
			num++;
		}
		if (num == this.counts)
		{
			num = 0;
		}
		this.prevI = num;
		return num;
	}

	// Token: 0x06000269 RID: 617 RVA: 0x00013F50 File Offset: 0x00012150
	public int GetI()
	{
		int num = (int)(UnityEngine.Random.value * (float)this.counts);
		if (num == this.prevI)
		{
			num++;
		}
		if (num > this.counts)
		{
			num = 0;
		}
		this.prevI = num;
		return num + 1;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x00013F94 File Offset: 0x00012194
	public int GetNonStatic()
	{
		return (int)(UnityEngine.Random.value * (float)(this.counts + 1)) + 1;
	}

	// Token: 0x0400031F RID: 799
	private int counts = -1;

	// Token: 0x04000320 RID: 800
	private int prevI;
}
