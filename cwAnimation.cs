using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002E4 RID: 740
[AddComponentMenu("Scripts/Game/Foundation/cwAnimation")]
internal class cwAnimation : PoolableBehaviour
{
	// Token: 0x06001481 RID: 5249 RVA: 0x000D8DD4 File Offset: 0x000D6FD4
	public void Play(string name, float speed = 1f)
	{
	}

	// Token: 0x04001942 RID: 6466
	public List<cwAnimationGroup> groups = new List<cwAnimationGroup>();

	// Token: 0x04001943 RID: 6467
	public List<cwAnimationEvent> events = new List<cwAnimationEvent>();
}
