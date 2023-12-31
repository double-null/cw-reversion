using System;
using UnityEngine;

// Token: 0x02000145 RID: 325
[Serializable]
public class PageElement
{
	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000817 RID: 2071 RVA: 0x000493E4 File Offset: 0x000475E4
	// (set) Token: 0x06000818 RID: 2072 RVA: 0x000493EC File Offset: 0x000475EC
	public virtual Rect Rect
	{
		get
		{
			return this.rect;
		}
		set
		{
			this.rect = value;
		}
	}

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06000819 RID: 2073 RVA: 0x000493F8 File Offset: 0x000475F8
	// (set) Token: 0x0600081A RID: 2074 RVA: 0x00049408 File Offset: 0x00047608
	public virtual float Width
	{
		get
		{
			return this.rect.width;
		}
		set
		{
			this.rect.width = value;
		}
	}

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x0600081B RID: 2075 RVA: 0x00049418 File Offset: 0x00047618
	// (set) Token: 0x0600081C RID: 2076 RVA: 0x00049428 File Offset: 0x00047628
	public virtual float Height
	{
		get
		{
			return this.rect.height;
		}
		set
		{
			this.rect.height = value;
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x00049438 File Offset: 0x00047638
	public virtual void PreDraw()
	{
		this.cacheColor = GUI.color;
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x00049448 File Offset: 0x00047648
	public virtual void Draw(Vector2 mousePosition, bool enabled)
	{
		GUI.color *= this.color;
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x00049460 File Offset: 0x00047660
	public virtual void AfterDraw()
	{
		GUI.color = this.cacheColor;
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x00049470 File Offset: 0x00047670
	public virtual void Paint(Vector2 mousePosition, bool enabled)
	{
		this.PreDraw();
		this.Draw(mousePosition, enabled);
		this.AfterDraw();
	}

	// Token: 0x04000922 RID: 2338
	public Rect rect = new Rect(0f, 0f, 64f, 64f);

	// Token: 0x04000923 RID: 2339
	public bool whByTexture = true;

	// Token: 0x04000924 RID: 2340
	public Color color = Color.white;

	// Token: 0x04000925 RID: 2341
	private Color cacheColor = Color.white;
}
