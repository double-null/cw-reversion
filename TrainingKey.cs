using System;

// Token: 0x020001DF RID: 479
[Serializable]
internal class TrainingKey
{
	// Token: 0x06000FF1 RID: 4081 RVA: 0x000B4D10 File Offset: 0x000B2F10
	public void TimerOn()
	{
		if (this.isTimered)
		{
			this.Timer.Start();
		}
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x000B4D28 File Offset: 0x000B2F28
	public void TimerOff()
	{
		if (this.isTimered)
		{
			this.Timer.Stop();
		}
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x000B4D40 File Offset: 0x000B2F40
	public float TimerElapsed()
	{
		if (this.isTimered)
		{
			return this.Timer.Elapsed;
		}
		return 0f;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x000B4D60 File Offset: 0x000B2F60
	public bool isTimerEnabled()
	{
		return this.Timer.Enabled;
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x000B4D70 File Offset: 0x000B2F70
	public void SetTaskEnd()
	{
		this.bTaskEnd = true;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x000B4D7C File Offset: 0x000B2F7C
	public bool TaskEnd()
	{
		return this.bTaskEnd;
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x000B4D84 File Offset: 0x000B2F84
	public void Click(int Index)
	{
		if (Index < this.values.Length && !this.values[Index])
		{
			this.values[Index] = true;
			this.bTaskEnd = true;
			for (int i = 0; i < this.values.Length; i++)
			{
				if (!this.values[i])
				{
					this.bTaskEnd = false;
				}
			}
		}
	}

	// Token: 0x04001070 RID: 4208
	public UKeyCode[] keys;

	// Token: 0x04001071 RID: 4209
	public bool[] values;

	// Token: 0x04001072 RID: 4210
	public string description;

	// Token: 0x04001073 RID: 4211
	private bool bTaskEnd;

	// Token: 0x04001074 RID: 4212
	public bool isTimered;

	// Token: 0x04001075 RID: 4213
	private eTimer Timer = new eTimer();
}
