using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
internal class FakeDoll : MonoBehaviour
{
	// Token: 0x0600010B RID: 267 RVA: 0x0000BCF4 File Offset: 0x00009EF4
	private void Start()
	{
		this._lerpTime = UnityEngine.Random.Range(0f, 4f);
		this._initPosition = base.transform.position;
		this._startPosition = base.transform.position;
		this._endPosition = this._initPosition + new Vector3(UnityEngine.Random.Range(-4f, 4f), 0f, UnityEngine.Random.Range(-4f, 4f));
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000BD74 File Offset: 0x00009F74
	private void Update()
	{
		this._step += Time.deltaTime;
		if (this._step < this._lerpTime)
		{
			base.transform.position = Vector3.Lerp(this._startPosition, this._endPosition, this._step);
		}
		else
		{
			this._lerpTime = UnityEngine.Random.Range(0f, 4f);
			this._step = 0f;
			this._startPosition = base.transform.position;
			this._endPosition = this._initPosition + new Vector3(UnityEngine.Random.Range(-4f, 4f), 0f, UnityEngine.Random.Range(-4f, 4f));
		}
	}

	// Token: 0x040001CB RID: 459
	private const float MoveDelta = 4f;

	// Token: 0x040001CC RID: 460
	private const float TimeDelta = 4f;

	// Token: 0x040001CD RID: 461
	private float _step;

	// Token: 0x040001CE RID: 462
	private float _lerpTime;

	// Token: 0x040001CF RID: 463
	private Vector3 _initPosition;

	// Token: 0x040001D0 RID: 464
	private Vector3 _startPosition;

	// Token: 0x040001D1 RID: 465
	private Vector3 _endPosition;
}
