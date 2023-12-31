using System;
using UnityEngine;

// Token: 0x02000381 RID: 897
public static class Audio2DPlayer
{
	// Token: 0x06001CF6 RID: 7414 RVA: 0x000FFD0C File Offset: 0x000FDF0C
	public static void Play(AudioClip clip)
	{
		Audio2DPlayer._source.PlayOneShot(clip);
	}

	// Token: 0x040021AF RID: 8623
	private static AudioSource _source = new GameObject("Audio2DPlayer").AddComponent<AudioSource>();
}
