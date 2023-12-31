using System;
using UnityEngine;

// Token: 0x02000386 RID: 902
internal class GUIProgressBar
{
	// Token: 0x06001D16 RID: 7446 RVA: 0x00100314 File Offset: 0x000FE514
	public GUIProgressBar(Texture backGround, Texture foreGround, Vector2 scale)
	{
		this._backGround = backGround;
		this._foreGround = foreGround;
		this._screenPos = UtilsScreen.Rect;
		UtilsScreen.OnScreenChange += this.UpdateSize;
		this._scale = scale;
		this.Culc();
		this._foreGroundUv = new Rect(0f, 0f, 1f, 1f);
	}

	// Token: 0x1700083D RID: 2109
	// (get) Token: 0x06001D17 RID: 7447 RVA: 0x00100380 File Offset: 0x000FE580
	// (set) Token: 0x06001D18 RID: 7448 RVA: 0x00100388 File Offset: 0x000FE588
	public Texture BackGround
	{
		get
		{
			return this._backGround;
		}
		set
		{
			if (value == this._backGround)
			{
				return;
			}
			this._backGround = value;
			this.Culc();
		}
	}

	// Token: 0x1700083E RID: 2110
	// (get) Token: 0x06001D19 RID: 7449 RVA: 0x001003AC File Offset: 0x000FE5AC
	// (set) Token: 0x06001D1A RID: 7450 RVA: 0x001003B4 File Offset: 0x000FE5B4
	public Texture ForeGround
	{
		get
		{
			return this._foreGround;
		}
		set
		{
			if (value == this._foreGround)
			{
				return;
			}
			this._foreGround = value;
			this.Culc();
		}
	}

	// Token: 0x1700083F RID: 2111
	// (get) Token: 0x06001D1B RID: 7451 RVA: 0x001003D8 File Offset: 0x000FE5D8
	// (set) Token: 0x06001D1C RID: 7452 RVA: 0x001003E0 File Offset: 0x000FE5E0
	public float Value
	{
		get
		{
			return this._value;
		}
		set
		{
			if (value == this._value)
			{
				return;
			}
			this._value = value;
			this._foreGroundUv.width = this._value;
			this._foreGroundRect.width = this._backGroundRect.width * this._value;
		}
	}

	// Token: 0x06001D1D RID: 7453 RVA: 0x00100430 File Offset: 0x000FE630
	private void UpdateSize()
	{
		this._screenPos = UtilsScreen.Rect;
		this.Culc();
	}

	// Token: 0x06001D1E RID: 7454 RVA: 0x00100444 File Offset: 0x000FE644
	private void Culc()
	{
		this._backGroundRect = new Rect((this._screenPos.width - (float)this._backGround.width * this._scale.x) * 0.5f, this._screenPos.height - 200f, (float)this._backGround.width * this._scale.x, (float)this._backGround.height * this._scale.y);
		this._foreGroundRect = this._backGroundRect;
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x001004D4 File Offset: 0x000FE6D4
	public void Draw()
	{
		GUI.DrawTexture(this._backGroundRect, this._backGround);
		GUI.DrawTextureWithTexCoords(this._foreGroundRect, this._foreGround, this._foreGroundUv);
	}

	// Token: 0x040021C4 RID: 8644
	private Texture _backGround;

	// Token: 0x040021C5 RID: 8645
	private Texture _foreGround;

	// Token: 0x040021C6 RID: 8646
	private float _value;

	// Token: 0x040021C7 RID: 8647
	private Rect _screenPos;

	// Token: 0x040021C8 RID: 8648
	private Rect _backGroundRect;

	// Token: 0x040021C9 RID: 8649
	private Rect _foreGroundRect;

	// Token: 0x040021CA RID: 8650
	private Rect _foreGroundUv;

	// Token: 0x040021CB RID: 8651
	private Vector2 _scale;
}
