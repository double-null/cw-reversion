using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200008E RID: 142
[Serializable]
public class MemorySnapshot
{
	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000327 RID: 807 RVA: 0x00016CC0 File Offset: 0x00014EC0
	public string Object
	{
		get
		{
			string text = "objects(" + this.objects.Count + "): ";
			for (int i = 0; i < this.objects.Count; i++)
			{
				text = text + "\n" + this.objects[i].name;
			}
			return text;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000328 RID: 808 RVA: 0x00016D28 File Offset: 0x00014F28
	public string Texture
	{
		get
		{
			string text = "textures(" + this.textures.Count + "): ";
			for (int i = 0; i < this.textures.Count; i++)
			{
				text = text + "\n" + this.textures[i].name;
			}
			return text;
		}
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000329 RID: 809 RVA: 0x00016D90 File Offset: 0x00014F90
	public string Mesh
	{
		get
		{
			string text = "meshes(" + this.meshes.Count + "): ";
			for (int i = 0; i < this.meshes.Count; i++)
			{
				text = text + "\n" + this.meshes[i].name;
			}
			return text;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x0600032A RID: 810 RVA: 0x00016DF8 File Offset: 0x00014FF8
	public string Material
	{
		get
		{
			string text = "materials(" + this.materials.Count + "): ";
			for (int i = 0; i < this.materials.Count; i++)
			{
				text = text + "\n" + this.materials[i].name;
			}
			return text;
		}
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x0600032B RID: 811 RVA: 0x00016E60 File Offset: 0x00015060
	public string AnimationClip
	{
		get
		{
			string text = "animationClips(" + this.animationClips.Count + "): ";
			for (int i = 0; i < this.animationClips.Count; i++)
			{
				text = text + "\n" + this.animationClips[i].name;
			}
			return text;
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x0600032C RID: 812 RVA: 0x00016EC8 File Offset: 0x000150C8
	public string AudioClip
	{
		get
		{
			string text = "audioClips(" + this.audioClips.Count + "): ";
			for (int i = 0; i < this.audioClips.Count; i++)
			{
				text = text + "\n" + this.audioClips[i].name;
			}
			return text;
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600032D RID: 813 RVA: 0x00016F30 File Offset: 0x00015130
	public string GameObject
	{
		get
		{
			string text = "gameObjects(" + this.gameObjects.Count + "): ";
			for (int i = 0; i < this.gameObjects.Count; i++)
			{
				text = text + "\n" + this.gameObjects[i].name;
			}
			return text;
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600032E RID: 814 RVA: 0x00016F98 File Offset: 0x00015198
	public string MonoBehaviour
	{
		get
		{
			string text = "monoBehaviours(" + this.monoBehaviours.Count + "): ";
			for (int i = 0; i < this.monoBehaviours.Count; i++)
			{
				text = text + "\n" + this.monoBehaviours[i].name;
			}
			return text;
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600032F RID: 815 RVA: 0x00017000 File Offset: 0x00015200
	public string Shader
	{
		get
		{
			string text = "shaders(" + this.shaders.Count + "): ";
			for (int i = 0; i < this.shaders.Count; i++)
			{
				text = text + "\n" + this.shaders[i].name;
			}
			return text;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000330 RID: 816 RVA: 0x00017068 File Offset: 0x00015268
	public string Transform
	{
		get
		{
			string text = "transforms(" + this.transforms.Count + "): ";
			for (int i = 0; i < this.transforms.Count; i++)
			{
				text = text + "\n" + this.transforms[i].name;
			}
			return text;
		}
	}

	// Token: 0x06000331 RID: 817 RVA: 0x000170D0 File Offset: 0x000152D0
	public void ClearLeak()
	{
		for (int i = 0; i < this.materials.Count; i++)
		{
			UnityEngine.Object.DestroyImmediate(this.materials[i], true);
		}
		for (int j = 0; j < this.textures.Count; j++)
		{
			UnityEngine.Object.DestroyImmediate(this.textures[j], true);
		}
		for (int k = 0; k < this.meshes.Count; k++)
		{
			UnityEngine.Object.DestroyImmediate(this.meshes[k], true);
		}
		for (int l = 0; l < this.shaders.Count; l++)
		{
			UnityEngine.Object.DestroyImmediate(this.shaders[l], true);
		}
	}

	// Token: 0x04000362 RID: 866
	public List<UnityEngine.Object> objects = new List<UnityEngine.Object>();

	// Token: 0x04000363 RID: 867
	public List<Texture> textures = new List<Texture>();

	// Token: 0x04000364 RID: 868
	public List<Mesh> meshes = new List<Mesh>();

	// Token: 0x04000365 RID: 869
	public List<Material> materials = new List<Material>();

	// Token: 0x04000366 RID: 870
	public List<AnimationClip> animationClips = new List<AnimationClip>();

	// Token: 0x04000367 RID: 871
	public List<AudioClip> audioClips = new List<AudioClip>();

	// Token: 0x04000368 RID: 872
	public List<GameObject> gameObjects = new List<GameObject>();

	// Token: 0x04000369 RID: 873
	public List<MonoBehaviour> monoBehaviours = new List<MonoBehaviour>();

	// Token: 0x0400036A RID: 874
	public List<Shader> shaders = new List<Shader>();

	// Token: 0x0400036B RID: 875
	public List<Transform> transforms = new List<Transform>();
}
