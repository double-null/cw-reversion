using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x02000039 RID: 57
[Obfuscation(Exclude = true)]
public class RainController : Form
{
	// Token: 0x060000DF RID: 223 RVA: 0x0000B268 File Offset: 0x00009468
	public void Init(Transform camTransform)
	{
		if (this._rainCollider != null || this._rainFollow != null)
		{
			return;
		}
		this._rainCollider = (UnityEngine.Object.Instantiate(this._rainColliderPrefab) as GameObject);
		if (this._rainCollider == null)
		{
			base.enabled = false;
			return;
		}
		this._rainFollow = (UnityEngine.Object.Instantiate(this._rainEffects) as GameObject);
		if (this._rainFollow == null)
		{
			base.enabled = false;
			return;
		}
		this._rainFollow.GetComponent<RainFollow>().Init(camTransform);
		this._rainCollider.transform.parent = camTransform;
		this._rainCollider.transform.localPosition = Vector3.zero;
		this._rainCollider.transform.localEulerAngles = Vector3.zero;
		RainCollider component = this._rainCollider.GetComponent<RainCollider>();
		component.OnEnter += delegate()
		{
			this._indoor = 1;
		};
		component.OnExit += delegate()
		{
			this._indoor = -1;
		};
		this._screenWater = camTransform.GetComponent<ScreenWater>();
		this._indorAudio.Play();
		this._outdorAudio.Play();
		if (this._screenWater != null)
		{
			base.StartCoroutine(this.EnableScreenWater());
		}
	}

	// Token: 0x060000E0 RID: 224 RVA: 0x0000B3B4 File Offset: 0x000095B4
	private IEnumerator EnableScreenWater()
	{
		yield return new WaitForSeconds(0.99f);
		if (this._screenWater != null)
		{
			this._screenWater.enabled = true;
		}
		yield break;
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x0000B3D0 File Offset: 0x000095D0
	private void Update()
	{
		if (this._screenWater == null)
		{
			return;
		}
		if (this._rainCollider != null)
		{
			this._rainCollider.SetActive(true);
		}
		float num = Time.deltaTime / this._transitionTime;
		this._timer = Mathf.Clamp01(this._timer + num * (float)this._indoor);
		float num2 = this._fadeCurve.Evaluate(this._timer);
		this._screenWater.Intens = Mathf.Lerp(this._maxDistortion, this._minDistortion, num2);
		this._indorAudio.volume = Mathf.Lerp(0f, this._maxAudioVolume * Main.UserInfo.settings.soundLoudness, num2);
		this._outdorAudio.volume = Mathf.Lerp(0f, this._maxAudioVolume * Main.UserInfo.settings.soundLoudness, 1f - num2);
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x0000B4C4 File Offset: 0x000096C4
	public override void OnLevelUnloaded()
	{
		if (this._rainFollow != null)
		{
			UnityEngine.Object.Destroy(this._rainFollow);
		}
		base.OnLevelUnloaded();
	}

	// Token: 0x04000187 RID: 391
	[SerializeField]
	private GameObject _rainColliderPrefab;

	// Token: 0x04000188 RID: 392
	[SerializeField]
	private float _minDistortion;

	// Token: 0x04000189 RID: 393
	[SerializeField]
	private float _maxDistortion = 0.05f;

	// Token: 0x0400018A RID: 394
	[SerializeField]
	private GameObject _rainEffects;

	// Token: 0x0400018B RID: 395
	[SerializeField]
	public AudioSource _indorAudio;

	// Token: 0x0400018C RID: 396
	[SerializeField]
	public AudioSource _outdorAudio;

	// Token: 0x0400018D RID: 397
	[SerializeField]
	public float _maxAudioVolume = 0.5f;

	// Token: 0x0400018E RID: 398
	[SerializeField]
	public AnimationCurve _fadeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400018F RID: 399
	[SerializeField]
	public float _transitionTime = 2f;

	// Token: 0x04000190 RID: 400
	private ScreenWater _screenWater;

	// Token: 0x04000191 RID: 401
	private float _timer;

	// Token: 0x04000192 RID: 402
	private int _indoor = -1;

	// Token: 0x04000193 RID: 403
	private GameObject _rainCollider;

	// Token: 0x04000194 RID: 404
	private GameObject _rainFollow;
}
