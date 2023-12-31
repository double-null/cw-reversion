using System;
using UnityEngine;

// Token: 0x02000149 RID: 329
[Serializable]
public class PageTexture : PageElement
{
	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000828 RID: 2088 RVA: 0x000496E4 File Offset: 0x000478E4
	// (set) Token: 0x06000829 RID: 2089 RVA: 0x00049718 File Offset: 0x00047918
	public override Rect Rect
	{
		get
		{
			return new Rect(this.rect.x, this.rect.y, this.Width, this.Height);
		}
		set
		{
			this.rect = value;
		}
	}

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x0600082A RID: 2090 RVA: 0x00049724 File Offset: 0x00047924
	// (set) Token: 0x0600082B RID: 2091 RVA: 0x0004975C File Offset: 0x0004795C
	public override float Width
	{
		get
		{
			if (this.whByTexture && this.texture)
			{
				return (float)this.texture.width;
			}
			return this.rect.width;
		}
		set
		{
			this.rect.width = value;
		}
	}

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x0600082C RID: 2092 RVA: 0x0004976C File Offset: 0x0004796C
	// (set) Token: 0x0600082D RID: 2093 RVA: 0x000497A4 File Offset: 0x000479A4
	public override float Height
	{
		get
		{
			if (this.whByTexture && this.texture)
			{
				return (float)this.texture.height;
			}
			return this.rect.height;
		}
		set
		{
			this.rect.height = value;
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x000497B4 File Offset: 0x000479B4
	public override void Draw(Vector2 mousePosition, bool enabled)
	{
		base.Draw(mousePosition, enabled);
		if (this.texture)
		{
			GUI.DrawTexture(this.Rect, this.texture, this.scaleMode, this.alphaBlend, this.imageAspect);
		}
	}

	// Token: 0x04000934 RID: 2356
	public Texture texture;

	// Token: 0x04000935 RID: 2357
	public ScaleMode scaleMode;

	// Token: 0x04000936 RID: 2358
	public bool alphaBlend = true;

	// Token: 0x04000937 RID: 2359
	public float imageAspect;
}
