using System;
using UnityEngine;

// Token: 0x0200012B RID: 299
[Serializable]
internal class Alpha
{
	// Token: 0x060007E7 RID: 2023 RVA: 0x00048708 File Offset: 0x00046908
	public void Show(float time = 0.5f, float delay = 0f)
	{
		if (this.showing || this.MaxVisible)
		{
			return;
		}
		this.showing = true;
		this.hiding = false;
		this.showList[1].x = delay;
		this.showList[2].x = time + delay;
		this.GNAME.Init(this.showList);
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00048774 File Offset: 0x00046974
	public void Hide(float time = 0.5f, float delay = 0f)
	{
		if (this.hiding || !this.Visible)
		{
			return;
		}
		this.showing = false;
		this.hiding = true;
		this.hideList[0].y = this.visibility;
		this.hideList[1].y = this.visibility;
		this.hideList[1].x = delay;
		this.hideList[2].x = time + delay;
		this.GNAME.Init(this.hideList);
		this.hideList[0].y = 1f;
		this.hideList[1].y = 1f;
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x00048838 File Offset: 0x00046A38
	public void Once(float time = 0f)
	{
		this.onceList2[0].y = ((!this.GNAME.Exist()) ? 0f : this.GNAME.Get());
		this.onceList2[1].x = 0.5f;
		this.onceList2[2].x = 1f + time;
		this.onceList2[3].x = 1.5f + time;
		this.GNAME.Init(this.onceList2);
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x000488D4 File Offset: 0x00046AD4
	public void Once(float start, float stay, float fade)
	{
		this.onceList[1].x = start;
		this.onceList[2].x = start + stay;
		this.onceList[3].x = start + stay + fade;
		this.GNAME.Init(this.onceList);
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00048930 File Offset: 0x00046B30
	public void OnceLong(float fade, float stay)
	{
		this.onceList[1].x = fade;
		this.onceList[2].x = fade + stay;
		this.onceList[3].x = fade + stay + fade;
		this.GNAME.Init(this.onceList);
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0004898C File Offset: 0x00046B8C
	public void Pulse(float time = 0f)
	{
		if (this.GNAME.ExistTime())
		{
			return;
		}
		this.onceList[1].x = 0.5f;
		this.onceList[2].x = 1f + time;
		this.onceList[3].x = 1.5f + time;
		this.GNAME.Init(this.onceList);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x00048A04 File Offset: 0x00046C04
	public void FastOnce()
	{
		this.onceList2[0].y = ((!this.GNAME.Exist()) ? 0f : this.GNAME.Get());
		this.onceList2[1].x = 0.05f;
		this.onceList2[2].x = 0.08f;
		this.onceList2[3].x = 1.12f;
		this.GNAME.Init(this.onceList2);
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x060007EE RID: 2030 RVA: 0x00048A9C File Offset: 0x00046C9C
	public bool Showing
	{
		get
		{
			if (this.GNAME.Get() == 1f || this.GNAME.NotExist())
			{
				this.showing = false;
			}
			return this.showing;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x060007EF RID: 2031 RVA: 0x00048ADC File Offset: 0x00046CDC
	public bool Hiding
	{
		get
		{
			if (this.GNAME.Get() == 1f || this.GNAME.NotExist())
			{
				this.hiding = false;
			}
			return this.hiding;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00048B1C File Offset: 0x00046D1C
	public float visibility_clean
	{
		get
		{
			return Mathf.Max(this.GNAME.Get(), 0f);
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00048B34 File Offset: 0x00046D34
	public float visibility
	{
		get
		{
			return this.GNAME.Get();
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00048B44 File Offset: 0x00046D44
	public bool Visible
	{
		get
		{
			return this.GNAME.Get() >= 0f;
		}
	}

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00048B5C File Offset: 0x00046D5C
	public bool MaxVisible
	{
		get
		{
			return this.GNAME.Get() == 1f;
		}
	}

	// Token: 0x0400085D RID: 2141
	private GraphicValue GNAME = new GraphicValue();

	// Token: 0x0400085E RID: 2142
	private bool showing;

	// Token: 0x0400085F RID: 2143
	private bool hiding;

	// Token: 0x04000860 RID: 2144
	private Vector2[] showList = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(float.MaxValue, 1f)
	};

	// Token: 0x04000861 RID: 2145
	private Vector2[] hideList = new Vector2[]
	{
		new Vector2(0f, 1f),
		new Vector2(0f, 1f),
		new Vector2(0f, 0f)
	};

	// Token: 0x04000862 RID: 2146
	private Vector2[] onceList = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(0f, 1f),
		new Vector2(0f, 0f)
	};

	// Token: 0x04000863 RID: 2147
	private Vector2[] onceList2 = new Vector2[]
	{
		new Vector2(0f, 0f),
		new Vector2(0f, 1f),
		new Vector2(0f, 1f),
		new Vector2(0f, 0f)
	};
}
