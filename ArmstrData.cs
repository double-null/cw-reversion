using System;
using UnityEngine;

// Token: 0x0200012A RID: 298
[Serializable]
internal class ArmstrData
{
	// Token: 0x04000858 RID: 2136
	public AudioClip ready;

	// Token: 0x04000859 RID: 2137
	public Texture2D tex_big;

	// Token: 0x0400085A RID: 2138
	public Texture2D tex_small;

	// Token: 0x0400085B RID: 2139
	public Texture2D tex_verysmall;

	// Token: 0x0400085C RID: 2140
	[HideInInspector]
	public Alpha window = new Alpha();
}
