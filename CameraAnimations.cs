using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001BD RID: 445
[AddComponentMenu("Scripts/Game/Components/CameraAnimations")]
public class CameraAnimations : SingletoneComponent<CameraAnimations>
{
	// Token: 0x06000F54 RID: 3924 RVA: 0x000B0758 File Offset: 0x000AE958
	public void Initialize(Transform root)
	{
		for (int i = 0; i < this.animations.Length; i++)
		{
			this.animations[i].Initialize(root);
		}
		this.ResetTransform();
	}

	// Token: 0x06000F55 RID: 3925 RVA: 0x000B0794 File Offset: 0x000AE994
	public void ResetTransform()
	{
		for (int i = 0; i < this.playing.Count; i++)
		{
			this.playing[i].ResetTransform();
		}
	}

	// Token: 0x06000F56 RID: 3926 RVA: 0x000B07D0 File Offset: 0x000AE9D0
	public void Play(AnimationType type)
	{
		PlayingCameraAnimation playingCameraAnimation = new PlayingCameraAnimation(this.animations[(int)type]);
		playingCameraAnimation.Start();
		this.playing.Add(playingCameraAnimation);
	}

	// Token: 0x06000F57 RID: 3927 RVA: 0x000B0800 File Offset: 0x000AEA00
	public void CallLateUpdate()
	{
		for (int i = 0; i < this.playing.Count; i++)
		{
			this.playing[i].Evalute();
		}
		for (int j = 0; j < this.playing.Count; j++)
		{
			if (this.playing[j].Ended)
			{
				this.playing.RemoveAt(j);
				j = -1;
			}
		}
	}

	// Token: 0x04000FA3 RID: 4003
	public CameraAnimation[] animations;

	// Token: 0x04000FA4 RID: 4004
	private List<PlayingCameraAnimation> playing = new List<PlayingCameraAnimation>();
}
