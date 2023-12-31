using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200006A RID: 106
[AddComponentMenu("Scripts/Engine/Components/NewBillboard")]
public class NewBillboard : PoolableBehaviour
{
	// Token: 0x060001B0 RID: 432 RVA: 0x0000E684 File Offset: 0x0000C884
	public override void OnPoolDespawn()
	{
		this.time = 0f;
		this.localScale = Vector3.one;
		this.localRotation = Quaternion.identity;
		this.x = 0;
		this.y = 0;
		this.startTime = 0f;
		this.endTime = 0f;
		this.cframe = 0;
		this.lightRange = 1f;
		this.lightIntensity = 1f;
		if (this.localTransform == null)
		{
			this.localTransform = base.transform;
		}
		this.localTransform.localScale = this.localScale;
		if (this.selfEnable)
		{
			base.renderer.enabled = false;
			if (this.hasLight)
			{
				base.light.enabled = false;
			}
		}
		base.OnPoolDespawn();
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x0000E758 File Offset: 0x0000C958
	public override void OnPoolSpawn()
	{
		this.time = Time.realtimeSinceStartup;
		this.startTime = Time.realtimeSinceStartup;
		this.endTime = Time.realtimeSinceStartup + this.alphaKeys[this.alphaKeys.Length - 1].time;
		this.localScale = this.localScaleCached * (this.minScale + UnityEngine.Random.value * (this.maxScale - this.minScale));
		if (this.RandomTextureOffset)
		{
			this.cframe = (int)(UnityEngine.Random.value * (float)this.nframes);
			this.x = this.cframe % this.xframes;
			this.y = this.yframes - 1 - this.cframe / this.yframes;
			this.mat.mainTextureOffset = new Vector2((float)this.x / (float)this.xframes, (float)this.y / (float)this.yframes);
		}
		if (this.randomRotation)
		{
			this.localRotation = Quaternion.Euler(0f, 0f, UnityEngine.Random.value * 360f);
		}
		if (this.selfDestroy && base.GetComponent<PoolItem>())
		{
			base.GetComponent<PoolItem>().AutoDespawn(this.alphaKeys[this.alphaKeys.Length - 1].time);
		}
		if (this.selfDestroy && base.GetComponent<PoolItem>() == null)
		{
			base.Invoke("Hide", this.alphaKeys[this.alphaKeys.Length - 1].time);
		}
		if (this.hasLight)
		{
			this.lightRange = this.minLightRange + UnityEngine.Random.value * (this.maxLightRange - this.minLightRange);
			this.lightIntensity = this.minLightIntensity + UnityEngine.Random.value * (this.maxLightIntensity - this.minLightIntensity);
		}
		if (this.selfEnable)
		{
			base.renderer.enabled = true;
			if (this.hasLight)
			{
				base.light.enabled = true;
			}
		}
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x0000E970 File Offset: 0x0000CB70
	private void Start()
	{
		this.OnPoolSpawn();
	}

	// Token: 0x060001B3 RID: 435 RVA: 0x0000E978 File Offset: 0x0000CB78
	protected override void Awake()
	{
		if (this.localTransform == null)
		{
			this.localTransform = base.transform;
		}
		this.scaleKeys = this.scale.keys;
		this.rotationKeys = this.rotation.keys;
		this.alphaKeys = this.alpha.keys;
		if (!this.mat)
		{
			this.mat = new Material(base.renderer.material);
			base.renderer.material = this.mat;
		}
		this.localScaleCached = this.localTransform.localScale;
		if (this.RandomTextureOffset)
		{
			this.mat.mainTextureScale = new Vector2((float)this.xframes / (float)this.nframes, (float)this.yframes / (float)this.nframes);
		}
		else if (this.nframes != -1)
		{
			this.mat.mainTextureScale = new Vector2((float)this.xframes / (float)this.nframes, (float)this.yframes / (float)this.nframes);
		}
	}

	// Token: 0x060001B4 RID: 436 RVA: 0x0000EA98 File Offset: 0x0000CC98
	private void OnEnable()
	{
		if (this.scaleKeys == null)
		{
			this.scaleKeys = this.scale.keys;
		}
		if (this.rotationKeys == null)
		{
			this.rotationKeys = this.rotation.keys;
		}
		if (this.alphaKeys == null)
		{
			this.alphaKeys = this.alpha.keys;
		}
	}

	// Token: 0x060001B5 RID: 437 RVA: 0x0000EAFC File Offset: 0x0000CCFC
	private void OnDestroy()
	{
		if (this.mat)
		{
			base.renderer.material = base.renderer.sharedMaterial;
			UnityEngine.Object.DestroyImmediate(this.mat);
		}
	}

	// Token: 0x060001B6 RID: 438 RVA: 0x0000EB3C File Offset: 0x0000CD3C
	[Obfuscation(Exclude = true)]
	private void Hide()
	{
		this.OnPoolDespawn();
	}

	// Token: 0x060001B7 RID: 439 RVA: 0x0000EB44 File Offset: 0x0000CD44
	private void LateUpdate()
	{
		if (this.rotateTowardsCamera && Camera.mainCamera != null)
		{
			this.localTransform.LookAt(Camera.mainCamera.transform.position);
			if (this.randomRotation)
			{
				if (this.rotationKeys.Length != 0)
				{
					this.localTransform.localRotation *= this.localRotation * Quaternion.Euler(0f, 0f, this.rotation.Evaluate(Time.realtimeSinceStartup - this.time));
				}
				else
				{
					this.localTransform.localRotation *= this.localRotation;
				}
			}
		}
		if (!this.RandomTextureOffset)
		{
			if (this.nframes != -1)
			{
				this.cframe = (int)((Time.realtimeSinceStartup - this.startTime) / (this.endTime - this.startTime) * (float)this.nframes);
				this.x = this.cframe % this.xframes;
				this.y = this.yframes - 1 - this.cframe / this.yframes;
				this.mat.mainTextureOffset = new Vector2((float)this.x / (float)this.xframes, (float)this.y / (float)this.yframes);
			}
		}
		if (this.scaleKeys.Length != 0)
		{
			this.localTransform.localScale = this.localScale * this.scale.Evaluate(Time.realtimeSinceStartup - this.time);
		}
		else
		{
			this.localTransform.localScale = this.localScale;
		}
		if (this.TintShader)
		{
			this.mat.SetColor("_TintColor", new Color(1f, 1f, 1f, this.alpha.Evaluate(Time.realtimeSinceStartup - this.time)));
		}
		else
		{
			this.mat.color = new Color(1f, 1f, 1f, this.alpha.Evaluate(Time.realtimeSinceStartup - this.time));
		}
		if (this.hasLight)
		{
			base.light.range = this.scale.Evaluate(Time.realtimeSinceStartup - this.time) * this.lightRange;
			base.light.intensity = this.alpha.Evaluate(Time.realtimeSinceStartup - this.time) * this.lightIntensity;
		}
	}

	// Token: 0x04000215 RID: 533
	public bool selfEnable = true;

	// Token: 0x04000216 RID: 534
	public bool selfDestroy;

	// Token: 0x04000217 RID: 535
	public bool rotateTowardsCamera;

	// Token: 0x04000218 RID: 536
	public bool randomRotation;

	// Token: 0x04000219 RID: 537
	public bool TintShader;

	// Token: 0x0400021A RID: 538
	public bool RandomTextureOffset;

	// Token: 0x0400021B RID: 539
	public float minScale = 1f;

	// Token: 0x0400021C RID: 540
	public float maxScale = 1f;

	// Token: 0x0400021D RID: 541
	public int xframes = -1;

	// Token: 0x0400021E RID: 542
	public int yframes = -1;

	// Token: 0x0400021F RID: 543
	public int nframes = -1;

	// Token: 0x04000220 RID: 544
	public AnimationCurve scale;

	// Token: 0x04000221 RID: 545
	public AnimationCurve rotation;

	// Token: 0x04000222 RID: 546
	public AnimationCurve alpha;

	// Token: 0x04000223 RID: 547
	public bool hasLight;

	// Token: 0x04000224 RID: 548
	public float minLightRange = 1f;

	// Token: 0x04000225 RID: 549
	public float maxLightRange = 1f;

	// Token: 0x04000226 RID: 550
	public float minLightIntensity = 1f;

	// Token: 0x04000227 RID: 551
	public float maxLightIntensity = 1f;

	// Token: 0x04000228 RID: 552
	private float time;

	// Token: 0x04000229 RID: 553
	private Material mat;

	// Token: 0x0400022A RID: 554
	private Vector3 localScale = Vector3.one;

	// Token: 0x0400022B RID: 555
	private Vector3 localScaleCached = Vector3.one;

	// Token: 0x0400022C RID: 556
	private Quaternion localRotation = Quaternion.identity;

	// Token: 0x0400022D RID: 557
	private int x;

	// Token: 0x0400022E RID: 558
	private int y;

	// Token: 0x0400022F RID: 559
	private float startTime;

	// Token: 0x04000230 RID: 560
	private float endTime;

	// Token: 0x04000231 RID: 561
	private int cframe;

	// Token: 0x04000232 RID: 562
	private float lightRange = 1f;

	// Token: 0x04000233 RID: 563
	private float lightIntensity = 1f;

	// Token: 0x04000234 RID: 564
	private Keyframe[] scaleKeys;

	// Token: 0x04000235 RID: 565
	private Keyframe[] rotationKeys;

	// Token: 0x04000236 RID: 566
	private Keyframe[] alphaKeys;

	// Token: 0x04000237 RID: 567
	private Transform localTransform;
}
