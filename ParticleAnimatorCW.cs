using System;
using UnityEngine;

// Token: 0x020001CD RID: 461
[AddComponentMenu("Scripts/Game/Components/ParticleAnimatorCW")]
internal class ParticleAnimatorCW : MonoBehaviour
{
	// Token: 0x06000F89 RID: 3977 RVA: 0x000B1E34 File Offset: 0x000B0034
	public void Awake()
	{
		if (base.renderer.material.name != "Default-Diffuse (Instance)")
		{
			this.startColor = base.renderer.material.GetColor("_TintColor");
		}
		base.enabled = false;
		this.intensivityKeys = this.intensivity.keys;
	}

	// Token: 0x06000F8A RID: 3978 RVA: 0x000B1E94 File Offset: 0x000B0094
	private void Enable()
	{
		if (this.intensivityKeys == null)
		{
			this.intensivityKeys = this.intensivity.keys;
		}
	}

	// Token: 0x06000F8B RID: 3979 RVA: 0x000B1EB4 File Offset: 0x000B00B4
	public void Update()
	{
		if (!this.time.ExistTime())
		{
			this.time.InitTimer(this.intensivityKeys[this.intensivityKeys.Length - 1].time);
		}
		base.renderer.material.SetColor("_TintColor", this.startColor * this.intensivity.Evaluate(this.time.Get()));
	}

	// Token: 0x04001007 RID: 4103
	public AnimationCurve intensivity;

	// Token: 0x04001008 RID: 4104
	private Color startColor;

	// Token: 0x04001009 RID: 4105
	private GraphicValue time = new GraphicValue();

	// Token: 0x0400100A RID: 4106
	private Keyframe[] intensivityKeys;
}
