using System;
using UnityEngine;

// Token: 0x02000385 RID: 901
internal class GUITiledTex
{
	// Token: 0x06001D10 RID: 7440 RVA: 0x001001D4 File Offset: 0x000FE3D4
	public GUITiledTex(Texture texture, float scale, bool horTile = false, bool verTile = false)
	{
		this.HorTile = horTile;
		this.VerTile = verTile;
		this._scale = scale;
		this._texture = texture;
		this._screenPos = UtilsScreen.Rect;
		UtilsScreen.OnScreenChange += this.UpdateSize;
		this.Culc();
	}

	// Token: 0x1700083C RID: 2108
	// (get) Token: 0x06001D11 RID: 7441 RVA: 0x00100228 File Offset: 0x000FE428
	// (set) Token: 0x06001D12 RID: 7442 RVA: 0x00100230 File Offset: 0x000FE430
	public Texture Texture
	{
		get
		{
			return this._texture;
		}
		set
		{
			if (value == this._texture)
			{
				return;
			}
			this._texture = value;
			this.Culc();
		}
	}

	// Token: 0x06001D13 RID: 7443 RVA: 0x00100254 File Offset: 0x000FE454
	private void UpdateSize()
	{
		this._screenPos = UtilsScreen.Rect;
		this.Culc();
	}

	// Token: 0x06001D14 RID: 7444 RVA: 0x00100268 File Offset: 0x000FE468
	private void Culc()
	{
		float height = (!this.VerTile) ? 1f : (this._screenPos.height / ((float)this._texture.height * this._scale));
		float width = (!this.HorTile) ? 1f : (this._screenPos.width / ((float)this._texture.width * this._scale));
		this.UvPos = new Rect(0f, 0f, width, height);
	}

	// Token: 0x06001D15 RID: 7445 RVA: 0x001002F8 File Offset: 0x000FE4F8
	public void Draw()
	{
		GUI.DrawTextureWithTexCoords(this._screenPos, this._texture, this.UvPos);
	}

	// Token: 0x040021BE RID: 8638
	private Texture _texture;

	// Token: 0x040021BF RID: 8639
	private Rect _screenPos;

	// Token: 0x040021C0 RID: 8640
	private Rect UvPos;

	// Token: 0x040021C1 RID: 8641
	private float _scale;

	// Token: 0x040021C2 RID: 8642
	public bool HorTile;

	// Token: 0x040021C3 RID: 8643
	public bool VerTile;
}
