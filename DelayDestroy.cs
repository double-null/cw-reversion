using System;
using UnityEngine;

// Token: 0x0200037A RID: 890
internal class DelayDestroy : MonoBehaviour
{
	// Token: 0x06001CDC RID: 7388 RVA: 0x000FF360 File Offset: 0x000FD560
	private void Start()
	{
		UnityEngine.Object.Destroy(base.gameObject, this.Time);
	}

	// Token: 0x04002197 RID: 8599
	[SerializeField]
	public float Time = 1f;
}
