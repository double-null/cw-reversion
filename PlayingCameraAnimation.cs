using System;

// Token: 0x020001BC RID: 444
[Serializable]
internal class PlayingCameraAnimation
{
	// Token: 0x06000F4E RID: 3918 RVA: 0x000B0640 File Offset: 0x000AE840
	public PlayingCameraAnimation(CameraAnimation track)
	{
		this.track = track;
		for (int i = 0; i < track.animations.Length; i++)
		{
			if (this.maxTime < track.animations[i].MaxTime)
			{
				this.maxTime = track.animations[i].MaxTime;
			}
		}
		this.maxTime *= 1.5f;
		this.duration = new eTimer();
		this.duration.Start();
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x06000F4F RID: 3919 RVA: 0x000B06DC File Offset: 0x000AE8DC
	public bool Ended
	{
		get
		{
			return this.duration.Elapsed > this.maxTime;
		}
	}

	// Token: 0x06000F50 RID: 3920 RVA: 0x000B06F4 File Offset: 0x000AE8F4
	public void Start()
	{
		this.duration.Start();
	}

	// Token: 0x06000F51 RID: 3921 RVA: 0x000B0704 File Offset: 0x000AE904
	public void ResetTransform()
	{
		this.track.ResetTransform();
	}

	// Token: 0x06000F52 RID: 3922 RVA: 0x000B0714 File Offset: 0x000AE914
	public void Evalute()
	{
		this.track.Evalute(this.lastTime, this.duration.Elapsed);
		this.lastTime = this.duration.Elapsed;
	}

	// Token: 0x04000F9F RID: 3999
	private CameraAnimation track;

	// Token: 0x04000FA0 RID: 4000
	private eTimer duration;

	// Token: 0x04000FA1 RID: 4001
	private float maxTime = -1f;

	// Token: 0x04000FA2 RID: 4002
	private float lastTime = -1f;
}
