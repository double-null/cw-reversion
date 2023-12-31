using System;
using UnityEngine;

// Token: 0x020001B3 RID: 435
internal class CollisionDetector : MonoBehaviour
{
	// Token: 0x06000EEF RID: 3823 RVA: 0x000ADA04 File Offset: 0x000ABC04
	private void Start()
	{
		this._triggersLayer = LayerMask.NameToLayer("triggers");
	}

	// Token: 0x06000EF0 RID: 3824 RVA: 0x000ADA18 File Offset: 0x000ABC18
	private void OnTriggerEnter(Collider collider)
	{
		GameObject gameObject = collider.gameObject;
		if (base.gameObject == gameObject || gameObject.layer == this._triggersLayer)
		{
			return;
		}
		this.IsCollides = true;
	}

	// Token: 0x06000EF1 RID: 3825 RVA: 0x000ADA58 File Offset: 0x000ABC58
	private void OnTriggerExit(Collider collider)
	{
		GameObject gameObject = collider.gameObject;
		if (base.gameObject == gameObject || gameObject.layer == this._triggersLayer)
		{
			return;
		}
		this.IsCollides = false;
	}

	// Token: 0x04000F4E RID: 3918
	private const string TriggersLayerName = "triggers";

	// Token: 0x04000F4F RID: 3919
	public bool IsCollides;

	// Token: 0x04000F50 RID: 3920
	private int _triggersLayer;
}
