using System;
using System.Reflection;
using UnityEngine;

// Token: 0x0200006B RID: 107
[AddComponentMenu("Scripts/Engine/Components/ParticleLife")]
internal class ParticleLife : PoolableBehaviour
{
	// Token: 0x060001B9 RID: 441 RVA: 0x0000EE18 File Offset: 0x0000D018
	public override void OnPoolDespawn()
	{
		if (base.renderer)
		{
			base.renderer.material.color = Colors.alpha(base.renderer.material.color, 1f);
		}
		this.gr = new GraphicValue();
		base.enabled = false;
		base.OnPoolDespawn();
	}

	// Token: 0x060001BA RID: 442 RVA: 0x0000EE78 File Offset: 0x0000D078
	public override void OnPoolSpawn()
	{
		base.CancelInvoke();
		base.Invoke("StartFade", (float)this.lifeTime);
		base.GetComponent<PoolItem>().AutoDespawn((float)this.lifeTime + this.alphaKeys[this.alphaKeys.Length - 1].time);
		if (this.fourTextures)
		{
			base.renderer.material.mainTextureOffset = new Vector2((float)((int)(UnityEngine.Random.value * 2f)) * 0.5f, (float)((int)(UnityEngine.Random.value * 2f)) * 0.5f);
			base.renderer.material.mainTextureScale = new Vector2(0.5f, 0.5f);
		}
		if (this.randomRotation)
		{
			base.transform.localRotation = this.cachedLocalrotation * Quaternion.AngleAxis(UnityEngine.Random.value * 360f, new Vector3(0f, 0f, 1f));
		}
		base.transform.localScale = this.cachedLocalScale * (this.minScale + (this.maxScale - this.minScale) * UnityEngine.Random.value);
		base.OnPoolSpawn();
	}

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x060001BB RID: 443 RVA: 0x0000EFAC File Offset: 0x0000D1AC
	public float LifeTime
	{
		get
		{
			return (float)this.lifeTime + this.alphaKeys[this.alphaKeys.Length - 1].time;
		}
	}

	// Token: 0x060001BC RID: 444 RVA: 0x0000EFDC File Offset: 0x0000D1DC
	protected override void Awake()
	{
		this.cachedLocalrotation = base.transform.localRotation;
		this.cachedLocalScale = base.transform.localScale;
		this.alphaKeys = this.alpha.keys;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000F01C File Offset: 0x0000D21C
	private void OnEnable()
	{
		if (this.alphaKeys == null)
		{
			this.alphaKeys = this.alpha.keys;
		}
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000F03C File Offset: 0x0000D23C
	[Obfuscation(Exclude = true)]
	private void StartFade()
	{
		this.gr.InitTimer(this.alphaKeys[this.alphaKeys.Length - 1].time);
		base.enabled = true;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x0000F078 File Offset: 0x0000D278
	private void Update()
	{
		if (base.renderer)
		{
			base.renderer.material.color = Colors.alpha(base.renderer.material.color, this.alpha.Evaluate(this.gr.Get()));
		}
	}

	// Token: 0x04000238 RID: 568
	public bool fourTextures;

	// Token: 0x04000239 RID: 569
	public bool randomRotation;

	// Token: 0x0400023A RID: 570
	public float minScale = 1f;

	// Token: 0x0400023B RID: 571
	public float maxScale = 1f;

	// Token: 0x0400023C RID: 572
	public AnimationCurve alpha = new AnimationCurve();

	// Token: 0x0400023D RID: 573
	public int lifeTime;

	// Token: 0x0400023E RID: 574
	private Quaternion cachedLocalrotation;

	// Token: 0x0400023F RID: 575
	private Vector3 cachedLocalScale;

	// Token: 0x04000240 RID: 576
	private GraphicValue gr = new GraphicValue();

	// Token: 0x04000241 RID: 577
	private Keyframe[] alphaKeys;
}
