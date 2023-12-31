using System;
using UnityEngine;

// Token: 0x020001BB RID: 443
[Serializable]
public class CameraAnimation
{
	// Token: 0x17000249 RID: 585
	// (get) Token: 0x06000F4A RID: 3914 RVA: 0x000B0514 File Offset: 0x000AE714
	public bool Initialized
	{
		get
		{
			return this.initialized;
		}
	}

	// Token: 0x06000F4B RID: 3915 RVA: 0x000B051C File Offset: 0x000AE71C
	public void Initialize(Transform root)
	{
		this.root = root;
		this.initialized = true;
		for (int i = 0; i < this.animations.Length; i++)
		{
			this.animations[i].Initialize(root);
		}
	}

	// Token: 0x06000F4C RID: 3916 RVA: 0x000B0560 File Offset: 0x000AE760
	public void ResetTransform()
	{
		for (int i = 0; i < this.animations.Length; i++)
		{
			this.animations[i].ResetTransform();
		}
	}

	// Token: 0x06000F4D RID: 3917 RVA: 0x000B0594 File Offset: 0x000AE794
	public void Evalute(float lastTime, float currentTime)
	{
		for (int i = 0; i < this.animations.Length; i++)
		{
			this.animations[i].Evalute(currentTime);
		}
		for (int j = 0; j < this.events.Length; j++)
		{
			if (this.events[j].time >= lastTime && this.events[j].time <= currentTime && this.root)
			{
				this.root.SendMessage(this.events[j].method, this.events[j].parameters, SendMessageOptions.RequireReceiver);
			}
		}
	}

	// Token: 0x04000F9B RID: 3995
	public AnimatedTransform[] animations;

	// Token: 0x04000F9C RID: 3996
	public AnimatedEvent[] events;

	// Token: 0x04000F9D RID: 3997
	private bool initialized;

	// Token: 0x04000F9E RID: 3998
	private Transform root;
}
