using System;
using UnityEngine;

// Token: 0x0200036E RID: 878
[AddComponentMenu("Scripts/Engine/Components/CustomLight")]
internal class CustomLight : MonoBehaviour
{
	// Token: 0x06001C99 RID: 7321 RVA: 0x000FD740 File Offset: 0x000FB940
	private void Awake()
	{
		this.rate = this.Rate;
		this.list = new Vector2[]
		{
			new Vector2(0f, 1f),
			new Vector2(this.rate, 1f)
		};
		this.lightGNAME.Init(this.list);
		this.maxIntensivity = base.light.intensity;
	}

	// Token: 0x06001C9A RID: 7322 RVA: 0x000FD7C0 File Offset: 0x000FB9C0
	private void FixedUpdate()
	{
		if (this.lightGNAME.Get() == -2f || this.rate != this.Rate)
		{
			this.rate = this.Rate;
			this.list = new Vector2[]
			{
				new Vector2(0f, (float)((!this.show) ? 0 : 1)),
				new Vector2(this.rate, (float)((!this.show) ? 0 : 1))
			};
			this.lightGNAME.Init(this.list);
			this.show = !this.show;
		}
		base.light.intensity = this.lightGNAME.Get() * this.maxIntensivity;
	}

	// Token: 0x0400216F RID: 8559
	private float rate;

	// Token: 0x04002170 RID: 8560
	public float Rate = 0.5f;

	// Token: 0x04002171 RID: 8561
	public float maxIntensivity;

	// Token: 0x04002172 RID: 8562
	private GraphicValue lightGNAME = new GraphicValue();

	// Token: 0x04002173 RID: 8563
	private Vector2[] list;

	// Token: 0x04002174 RID: 8564
	private bool show;
}
