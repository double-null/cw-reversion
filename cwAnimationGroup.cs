using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E2 RID: 738
[Serializable]
internal class cwAnimationGroup
{
	// Token: 0x04001939 RID: 6457
	public string name = string.Empty;

	// Token: 0x0400193A RID: 6458
	public int layer;

	// Token: 0x0400193B RID: 6459
	public WrapMode wrapMode = WrapMode.Once;

	// Token: 0x0400193C RID: 6460
	public AnimationBlendMode blendMode;

	// Token: 0x0400193D RID: 6461
	public Transform mixingTransform;

	// Token: 0x0400193E RID: 6462
	public List<string> animations = new List<string>();
}
