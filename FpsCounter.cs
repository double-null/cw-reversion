using System;
using UnityEngine;

// Token: 0x0200037C RID: 892
[Serializable]
public class FpsCounter
{
	// Token: 0x17000836 RID: 2102
	// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x000FF3BC File Offset: 0x000FD5BC
	public int Count
	{
		get
		{
			return this.counter;
		}
	}

	// Token: 0x06001CE2 RID: 7394 RVA: 0x000FF3C4 File Offset: 0x000FD5C4
	public void Advance()
	{
		if (this.startTime == 0f)
		{
			this.startTime = Time.realtimeSinceStartup;
			this.count = 0;
		}
		if (Time.realtimeSinceStartup - this.startTime > 1f)
		{
			this.counter = this.count;
			this.startTime = Time.realtimeSinceStartup;
			this.count = 0;
		}
		this.count++;
	}

	// Token: 0x04002199 RID: 8601
	private int count;

	// Token: 0x0400219A RID: 8602
	private float startTime;

	// Token: 0x0400219B RID: 8603
	private int counter;

	// Token: 0x0400219C RID: 8604
	private float deltaTime;

	// Token: 0x0400219D RID: 8605
	private float fps;

	// Token: 0x0400219E RID: 8606
	private float frameCount;

	// Token: 0x0400219F RID: 8607
	private float nextUpdate;

	// Token: 0x040021A0 RID: 8608
	private float updateRate = 4f;
}
