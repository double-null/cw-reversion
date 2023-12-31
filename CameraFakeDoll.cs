using System;
using UnityEngine;

// Token: 0x0200004C RID: 76
internal class CameraFakeDoll : MonoBehaviour
{
	// Token: 0x06000108 RID: 264 RVA: 0x0000BB78 File Offset: 0x00009D78
	private void Start()
	{
		this._lerpTime = 2f;
		this._initPosition = base.transform.localPosition;
		this._startPosition = base.transform.localPosition;
		this._endPosition = this._initPosition + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
		this._speed = UnityEngine.Random.Range(1f, 3f);
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000BC0C File Offset: 0x00009E0C
	private void Update()
	{
		this._step += this._speed * Time.deltaTime;
		if (this._step < this._lerpTime)
		{
			base.transform.localPosition = Vector3.Lerp(this._startPosition, this._endPosition, this._step);
		}
		else
		{
			this._lerpTime = 2f;
			this._step = 0f;
			this._startPosition = base.transform.localPosition;
			this._endPosition = this._initPosition + new Vector3(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(-2f, 2f));
			this._speed = UnityEngine.Random.Range(1f, 3f);
		}
	}

	// Token: 0x040001C4 RID: 452
	private const float MoveDelta = 2f;

	// Token: 0x040001C5 RID: 453
	private float _step;

	// Token: 0x040001C6 RID: 454
	private float _lerpTime;

	// Token: 0x040001C7 RID: 455
	private Vector3 _initPosition;

	// Token: 0x040001C8 RID: 456
	private Vector3 _startPosition;

	// Token: 0x040001C9 RID: 457
	private Vector3 _endPosition;

	// Token: 0x040001CA RID: 458
	private float _speed;
}
