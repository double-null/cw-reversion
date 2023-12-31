using System;
using UnityEngine;

// Token: 0x02000065 RID: 101
[AddComponentMenu("Scripts/Engine/Audio")]
internal class Audio : SingletoneComponent<Audio>
{
	// Token: 0x17000020 RID: 32
	// (get) Token: 0x0600018C RID: 396 RVA: 0x0000D51C File Offset: 0x0000B71C
	public static float soundVolume
	{
		get
		{
			return Main.UserInfo.settings.soundLoudness;
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x0600018D RID: 397 RVA: 0x0000D530 File Offset: 0x0000B730
	public static float musicVolume
	{
		get
		{
			return Main.UserInfo.settings.musicLoudness;
		}
	}

	// Token: 0x17000022 RID: 34
	// (get) Token: 0x0600018E RID: 398 RVA: 0x0000D544 File Offset: 0x0000B744
	public static float radioVolume
	{
		get
		{
			return Main.UserInfo.settings.graphics.radioLoudness;
		}
	}

	// Token: 0x17000023 RID: 35
	// (get) Token: 0x0600018F RID: 399 RVA: 0x0000D55C File Offset: 0x0000B75C
	public static AudioListener Listener
	{
		get
		{
			return SingletoneComponent<Audio>.Instance.other;
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000D568 File Offset: 0x0000B768
	public static void Enable()
	{
		SingletoneComponent<Audio>.Instance.paused = false;
		AudioListener.pause = false;
		AudioListener.volume = Main.UserInfo.settings.globalLoudness;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000D590 File Offset: 0x0000B790
	public static void Disable()
	{
		SingletoneComponent<Audio>.Instance.paused = true;
		AudioListener.pause = true;
		AudioListener.volume = 0f;
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000D5B0 File Offset: 0x0000B7B0
	public static void EnableMusic()
	{
		AutoSound[] array = (AutoSound[])UnityEngine.Object.FindObjectsOfType(typeof(AutoSound));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].type == SoundType.music)
			{
				array[i].audio.mute = false;
				array[i].audio.Play();
			}
		}
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000D610 File Offset: 0x0000B810
	public static void DisableMusic()
	{
		AutoSound[] array = (AutoSound[])UnityEngine.Object.FindObjectsOfType(typeof(AutoSound));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].type == SoundType.music)
			{
				array[i].audio.mute = true;
			}
		}
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000D664 File Offset: 0x0000B864
	public static void Init()
	{
		Audio.RefreshLoudness();
	}

	// Token: 0x06000195 RID: 405 RVA: 0x0000D66C File Offset: 0x0000B86C
	public static void RefreshLoudness()
	{
		AudioListener.volume = Main.UserInfo.settings.globalLoudness;
		foreach (AutoSound autoSound in (AutoSound[])UnityEngine.Object.FindObjectsOfType(typeof(AutoSound)))
		{
			if (autoSound.type == SoundType.music)
			{
				autoSound.Volume = Main.UserInfo.settings.musicLoudness;
			}
			else if (autoSound.type == SoundType.radio)
			{
				autoSound.Volume = Main.UserInfo.settings.graphics.radioLoudness;
			}
			else
			{
				autoSound.Volume = Main.UserInfo.settings.soundLoudness;
			}
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x0000D720 File Offset: 0x0000B920
	public static void ChangeListener(AudioListener l)
	{
		if (SingletoneComponent<Audio>.Instance.other != null)
		{
			SingletoneComponent<Audio>.Instance.other.enabled = false;
		}
		SingletoneComponent<Audio>.Instance.other = l;
		SingletoneComponent<Audio>.Instance.other.enabled = true;
		if (SingletoneComponent<Audio>.Instance.paused)
		{
			AudioListener.pause = true;
			AudioListener.volume = 0f;
		}
		else
		{
			AudioListener.pause = false;
			AudioListener.volume = Main.UserInfo.settings.globalLoudness;
		}
	}

	// Token: 0x06000197 RID: 407 RVA: 0x0000D7AC File Offset: 0x0000B9AC
	public static AudioSource Play(AudioClip clip)
	{
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["sound"].Spawn();
		gameObject.transform.position = Vector3.zero;
		gameObject.transform.parent = Main.Trash;
		if (Peer.Dedicated)
		{
			return gameObject.audio;
		}
		gameObject.audio.clip = clip;
		gameObject.audio.volume = Audio.soundVolume;
		gameObject.audio.Play();
		gameObject.GetComponent<PoolItem>().AutoDespawn(clip.length * 1.5f);
		return gameObject.audio;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0000D844 File Offset: 0x0000BA44
	public static AudioSource Play(Vector3 pos, AudioClip clip, float minDistance = -1f, float maxDistance = -1f)
	{
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["sound"].Spawn();
		gameObject.transform.position = pos;
		gameObject.transform.parent = Main.Trash;
		if (Peer.Dedicated)
		{
			return gameObject.audio;
		}
		gameObject.audio.clip = clip;
		gameObject.audio.volume = Audio.soundVolume;
		gameObject.audio.Play();
		if (minDistance != -1f && maxDistance != -1f)
		{
			gameObject.audio.minDistance = minDistance;
			gameObject.audio.maxDistance = maxDistance;
		}
		gameObject.GetComponent<PoolItem>().AutoDespawn(clip.length * 1.5f);
		return gameObject.audio;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x0000D908 File Offset: 0x0000BB08
	public static void Play(PoolItem poolItem, AudioClip clip, bool forcePlay = false, float minDistance = 10f, float maxDistance = 150f)
	{
		if (Peer.Dedicated || poolItem == null)
		{
			return;
		}
		foreach (AutoSound autoSound in poolItem.gameObject.GetComponentsInChildren<AutoSound>())
		{
			if (autoSound.audio.clip == clip && !forcePlay)
			{
				return;
			}
		}
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["sound"].Spawn();
		gameObject.transform.parent = poolItem.transform;
		gameObject.transform.localPosition = Vector3.zero;
		gameObject.GetComponent<PoolItem>().autoDespawn = true;
		gameObject.audio.clip = clip;
		gameObject.audio.minDistance = minDistance;
		gameObject.audio.maxDistance = maxDistance;
		gameObject.audio.volume = Audio.soundVolume;
		if (gameObject.activeInHierarchy && gameObject.audio.enabled)
		{
			gameObject.audio.Play();
		}
		gameObject.GetComponent<PoolItem>().AutoDespawn(clip.length);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x0000DA1C File Offset: 0x0000BC1C
	public static void PlayTyped(PoolItem poolItem, SoundType type, AudioClip clip, bool forcePlay = false, float minDistance = 10f, float maxDistance = 150f)
	{
		if (Peer.Dedicated)
		{
			return;
		}
		foreach (AutoSound autoSound in poolItem.gameObject.GetComponentsInChildren<AutoSound>())
		{
			if (autoSound.audio.clip == clip && !forcePlay)
			{
				return;
			}
		}
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["sound"].Spawn();
		gameObject.transform.parent = poolItem.transform;
		gameObject.transform.localPosition = Vector3.zero;
		poolItem.Childs.Add(gameObject.GetComponent<PoolItem>());
		gameObject.audio.clip = clip;
		gameObject.audio.minDistance = minDistance;
		gameObject.audio.maxDistance = maxDistance;
		gameObject.audio.priority = 0;
		gameObject.audio.volume = Audio.soundVolume;
		if (type == SoundType.music)
		{
			gameObject.audio.volume = Main.UserInfo.settings.musicLoudness;
		}
		if (type == SoundType.radio)
		{
			gameObject.audio.volume = Main.UserInfo.settings.graphics.radioLoudness;
		}
		gameObject.audio.Play();
		gameObject.GetComponent<AutoSound>().type = type;
		gameObject.GetComponent<PoolItem>().AutoDespawn(clip.length * 1.5f);
	}

	// Token: 0x04000208 RID: 520
	private AudioListener other;

	// Token: 0x04000209 RID: 521
	private bool paused;
}
