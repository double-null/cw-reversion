using System;
using UnityEngine;

// Token: 0x02000037 RID: 55
[RequireComponent(typeof(Collider))]
[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class RainCollider : MonoBehaviour
{
	// Token: 0x14000001 RID: 1
	// (add) Token: 0x060000D6 RID: 214 RVA: 0x0000B10C File Offset: 0x0000930C
	// (remove) Token: 0x060000D7 RID: 215 RVA: 0x0000B128 File Offset: 0x00009328
	public event Action OnEnter;

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x060000D8 RID: 216 RVA: 0x0000B144 File Offset: 0x00009344
	// (remove) Token: 0x060000D9 RID: 217 RVA: 0x0000B160 File Offset: 0x00009360
	public event Action OnExit;

	// Token: 0x060000DA RID: 218 RVA: 0x0000B17C File Offset: 0x0000937C
	private void OnTriggerEnter(Collider other)
	{
		if (this.OnEnter != null)
		{
			this.OnEnter();
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x0000B194 File Offset: 0x00009394
	private void OnTriggerExit(Collider other)
	{
		if (this.OnExit != null)
		{
			this.OnExit();
		}
	}
}
