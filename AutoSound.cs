using System;
using UnityEngine;

// Token: 0x02000069 RID: 105
[AddComponentMenu("Scripts/Engine/Components/AutoSound")]
internal class AutoSound : PoolableBehaviour
{
	// Token: 0x17000027 RID: 39
	// (set) Token: 0x060001AB RID: 427 RVA: 0x0000E574 File Offset: 0x0000C774
	public float Volume
	{
		set
		{
			if (base.audio)
			{
				base.audio.volume = value;
			}
		}
	}

	// Token: 0x060001AC RID: 428 RVA: 0x0000E594 File Offset: 0x0000C794
	public override void OnPoolDespawn()
	{
		this.type = SoundType.none;
		base.OnPoolDespawn();
	}

	// Token: 0x060001AD RID: 429 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
	public void Follow(Transform follow)
	{
		this.follow = follow;
	}

	// Token: 0x060001AE RID: 430 RVA: 0x0000E5B0 File Offset: 0x0000C7B0
	public void LateUpdate()
	{
		if (this.follow != null)
		{
			base.transform.position = this.follow.position;
		}
	}

	// Token: 0x04000213 RID: 531
	public SoundType type;

	// Token: 0x04000214 RID: 532
	private Transform follow;
}
