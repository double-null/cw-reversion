using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008F RID: 143
[Serializable]
public class Memory
{
	// Token: 0x06000334 RID: 820 RVA: 0x000171C0 File Offset: 0x000153C0
	public static void Snapshot()
	{
		Memory.snapshot = new MemorySnapshot();
		Memory.snapshot.objects = new List<UnityEngine.Object>(Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)));
		Memory.snapshot.textures = new List<Texture>((Texture[])Resources.FindObjectsOfTypeAll(typeof(Texture)));
		Memory.snapshot.meshes = new List<Mesh>((Mesh[])Resources.FindObjectsOfTypeAll(typeof(Mesh)));
		Memory.snapshot.materials = new List<Material>((Material[])Resources.FindObjectsOfTypeAll(typeof(Material)));
		Memory.snapshot.animationClips = new List<AnimationClip>((AnimationClip[])Resources.FindObjectsOfTypeAll(typeof(AnimationClip)));
		Memory.snapshot.audioClips = new List<AudioClip>((AudioClip[])Resources.FindObjectsOfTypeAll(typeof(AudioClip)));
		Memory.snapshot.gameObjects = new List<GameObject>((GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)));
		Memory.snapshot.monoBehaviours = new List<MonoBehaviour>((MonoBehaviour[])Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)));
		Memory.snapshot.shaders = new List<Shader>((Shader[])Resources.FindObjectsOfTypeAll(typeof(Shader)));
		Memory.snapshot.transforms = new List<Transform>((Transform[])Resources.FindObjectsOfTypeAll(typeof(Transform)));
	}

	// Token: 0x06000335 RID: 821 RVA: 0x00017330 File Offset: 0x00015530
	public static void Delta()
	{
		Memory.delta = new MemorySnapshot();
		Memory.delta.objects = new List<UnityEngine.Object>(Resources.FindObjectsOfTypeAll(typeof(UnityEngine.Object)));
		Memory.delta.textures = new List<Texture>((Texture[])Resources.FindObjectsOfTypeAll(typeof(Texture)));
		Memory.delta.meshes = new List<Mesh>((Mesh[])Resources.FindObjectsOfTypeAll(typeof(Mesh)));
		Memory.delta.materials = new List<Material>((Material[])Resources.FindObjectsOfTypeAll(typeof(Material)));
		Memory.delta.animationClips = new List<AnimationClip>((AnimationClip[])Resources.FindObjectsOfTypeAll(typeof(AnimationClip)));
		Memory.delta.audioClips = new List<AudioClip>((AudioClip[])Resources.FindObjectsOfTypeAll(typeof(AudioClip)));
		Memory.delta.gameObjects = new List<GameObject>((GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)));
		Memory.delta.monoBehaviours = new List<MonoBehaviour>((MonoBehaviour[])Resources.FindObjectsOfTypeAll(typeof(MonoBehaviour)));
		Memory.delta.shaders = new List<Shader>((Shader[])Resources.FindObjectsOfTypeAll(typeof(Shader)));
		Memory.delta.transforms = new List<Transform>((Transform[])Resources.FindObjectsOfTypeAll(typeof(Transform)));
		for (int i = 0; i < Memory.snapshot.objects.Count; i++)
		{
			if (Memory.delta.objects.Contains(Memory.snapshot.objects[i]))
			{
				Memory.delta.objects.Remove(Memory.snapshot.objects[i]);
			}
		}
		for (int j = 0; j < Memory.snapshot.textures.Count; j++)
		{
			if (Memory.delta.textures.Contains(Memory.snapshot.textures[j]))
			{
				Memory.delta.textures.Remove(Memory.snapshot.textures[j]);
			}
		}
		for (int k = 0; k < Memory.snapshot.meshes.Count; k++)
		{
			if (Memory.delta.meshes.Contains(Memory.snapshot.meshes[k]))
			{
				Memory.delta.meshes.Remove(Memory.snapshot.meshes[k]);
			}
		}
		for (int l = 0; l < Memory.snapshot.materials.Count; l++)
		{
			if (Memory.delta.materials.Contains(Memory.snapshot.materials[l]))
			{
				Memory.delta.materials.Remove(Memory.snapshot.materials[l]);
			}
		}
		for (int m = 0; m < Memory.snapshot.animationClips.Count; m++)
		{
			if (Memory.delta.animationClips.Contains(Memory.snapshot.animationClips[m]))
			{
				Memory.delta.animationClips.Remove(Memory.snapshot.animationClips[m]);
			}
		}
		for (int n = 0; n < Memory.snapshot.audioClips.Count; n++)
		{
			if (Memory.delta.audioClips.Contains(Memory.snapshot.audioClips[n]))
			{
				Memory.delta.audioClips.Remove(Memory.snapshot.audioClips[n]);
			}
		}
		for (int num = 0; num < Memory.snapshot.gameObjects.Count; num++)
		{
			if (Memory.delta.gameObjects.Contains(Memory.snapshot.gameObjects[num]))
			{
				Memory.delta.gameObjects.Remove(Memory.snapshot.gameObjects[num]);
			}
		}
		for (int num2 = 0; num2 < Memory.snapshot.monoBehaviours.Count; num2++)
		{
			if (Memory.delta.monoBehaviours.Contains(Memory.snapshot.monoBehaviours[num2]))
			{
				Memory.delta.monoBehaviours.Remove(Memory.snapshot.monoBehaviours[num2]);
			}
		}
		for (int num3 = 0; num3 < Memory.snapshot.shaders.Count; num3++)
		{
			if (Memory.delta.shaders.Contains(Memory.snapshot.shaders[num3]))
			{
				Memory.delta.shaders.Remove(Memory.snapshot.shaders[num3]);
			}
		}
		for (int num4 = 0; num4 < Memory.snapshot.transforms.Count; num4++)
		{
			if (Memory.delta.transforms.Contains(Memory.snapshot.transforms[num4]))
			{
				Memory.delta.transforms.Remove(Memory.snapshot.transforms[num4]);
			}
		}
	}

	// Token: 0x06000336 RID: 822 RVA: 0x000178AC File Offset: 0x00015AAC
	public static void MemoryStart()
	{
		Memory.memory = GC.GetTotalMemory(false);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x000178BC File Offset: 0x00015ABC
	public static long MemoryEnd()
	{
		long totalMemory = GC.GetTotalMemory(false);
		long num = (totalMemory - Memory.memory) / 1024L / 1024L;
		Debug.LogWarning(num);
		return num;
	}

	// Token: 0x0400036C RID: 876
	public static MemorySnapshot snapshot = new MemorySnapshot();

	// Token: 0x0400036D RID: 877
	public static MemorySnapshot delta = new MemorySnapshot();

	// Token: 0x0400036E RID: 878
	private static long memory = 0L;
}
