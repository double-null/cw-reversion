using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CE RID: 718
[Serializable]
public class SurfaceDescription
{
	// Token: 0x04001843 RID: 6211
	public SurfaceType type;

	// Token: 0x04001844 RID: 6212
	public string prefabName = string.Empty;

	// Token: 0x04001845 RID: 6213
	public List<AudioClip> sounds = new List<AudioClip>();

	// Token: 0x04001846 RID: 6214
	public List<AudioClip> walkSounds = new List<AudioClip>();

	// Token: 0x04001847 RID: 6215
	public List<ScrapDescription> scraps = new List<ScrapDescription>();

	// Token: 0x04001848 RID: 6216
	public float minCount;

	// Token: 0x04001849 RID: 6217
	public float maxCount = 1f;

	// Token: 0x0400184A RID: 6218
	public string[] particlesNames = new string[0];

	// Token: 0x0400184B RID: 6219
	public string effectReplacerName = string.Empty;
}
