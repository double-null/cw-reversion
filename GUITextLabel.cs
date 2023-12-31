using System;
using UnityEngine;

// Token: 0x02000387 RID: 903
internal class GUITextLabel
{
	// Token: 0x06001D20 RID: 7456 RVA: 0x0010050C File Offset: 0x000FE70C
	public GUITextLabel(GUITextLabel.XAlignment xAlig, GUITextLabel.YAlignment yAlig, Vector2 pos, Vector2 size, bool relatively = false)
	{
		this._xAlig = xAlig;
		this._yAlig = yAlig;
		this._pos = pos;
		this._size = size;
		this._relatively = relatively;
		UtilsScreen.OnScreenChange += this.UpdateSize;
		this.UpdateSize();
		if (this._style == null)
		{
			this._style = new GUIStyle();
			this._style.alignment = TextAnchor.MiddleCenter;
			this._style.normal.textColor = Color.white;
		}
	}

	// Token: 0x17000840 RID: 2112
	// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00100594 File Offset: 0x000FE794
	// (set) Token: 0x06001D22 RID: 7458 RVA: 0x001005A4 File Offset: 0x000FE7A4
	public TextAnchor TextAnchor
	{
		get
		{
			return this._style.alignment;
		}
		set
		{
			this._style.alignment = value;
		}
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x001005B4 File Offset: 0x000FE7B4
	private void UpdateSize()
	{
		Vector2 size = UtilsScreen.Size;
		float num = this._pos.x;
		float num2 = this._pos.y;
		if (this._relatively)
		{
			num *= size.x;
			num2 *= size.y;
		}
		GUITextLabel.XAlignment xAlig = this._xAlig;
		if (xAlig != GUITextLabel.XAlignment.Center)
		{
			if (xAlig == GUITextLabel.XAlignment.Right)
			{
				num = size.x - num;
			}
		}
		else
		{
			num = (size.x - this._size.x) * 0.5f + num;
		}
		GUITextLabel.YAlignment yAlig = this._yAlig;
		if (yAlig != GUITextLabel.YAlignment.Center)
		{
			if (yAlig == GUITextLabel.YAlignment.Bottom)
			{
				num2 = size.y - num2;
			}
		}
		else
		{
			num2 = (size.y - this._size.y) * 0.5f + num2;
		}
		this._rect = new Rect(num, num2, this._size.x, this._size.y);
	}

	// Token: 0x06001D24 RID: 7460 RVA: 0x001006B8 File Offset: 0x000FE8B8
	public void Draw()
	{
		GUI.Label(this._rect, this.Text, this._style);
	}

	// Token: 0x040021CC RID: 8652
	private GUIStyle _style;

	// Token: 0x040021CD RID: 8653
	private GUITextLabel.XAlignment _xAlig;

	// Token: 0x040021CE RID: 8654
	private GUITextLabel.YAlignment _yAlig;

	// Token: 0x040021CF RID: 8655
	private Vector2 _pos;

	// Token: 0x040021D0 RID: 8656
	private Vector2 _size;

	// Token: 0x040021D1 RID: 8657
	private Rect _rect;

	// Token: 0x040021D2 RID: 8658
	private bool _relatively;

	// Token: 0x040021D3 RID: 8659
	public string Text;

	// Token: 0x02000388 RID: 904
	public enum XAlignment
	{
		// Token: 0x040021D5 RID: 8661
		Left,
		// Token: 0x040021D6 RID: 8662
		Center,
		// Token: 0x040021D7 RID: 8663
		Right
	}

	// Token: 0x02000389 RID: 905
	public enum YAlignment
	{
		// Token: 0x040021D9 RID: 8665
		Top,
		// Token: 0x040021DA RID: 8666
		Center,
		// Token: 0x040021DB RID: 8667
		Bottom
	}
}
