using System;
using UnityEngine;

// Token: 0x0200003A RID: 58
[ExecuteInEditMode]
public class RainFollow : MonoBehaviour
{
	// Token: 0x060000E6 RID: 230 RVA: 0x0000B520 File Offset: 0x00009720
	public void Init(Transform target)
	{
		this._transform = base.GetComponent<Transform>();
		this._target = target;
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x0000B538 File Offset: 0x00009738
	private void Update()
	{
		if (this._target == null)
		{
			return;
		}
		this._transform.position = this._target.position + new Vector3(0f, this._offset, 0f);
	}

	// Token: 0x04000195 RID: 405
	[SerializeField]
	private float _offset = 10f;

	// Token: 0x04000196 RID: 406
	private Transform _target;

	// Token: 0x04000197 RID: 407
	private Transform _transform;
}
