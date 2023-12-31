using System;

// Token: 0x0200012D RID: 301
public struct ButtonState
{
	// Token: 0x060007F5 RID: 2037 RVA: 0x00048B78 File Offset: 0x00046D78
	public ButtonState(bool h, bool c, bool s)
	{
		this.Hover = h;
		this.Clicked = c;
		this.Selected = s;
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00048B90 File Offset: 0x00046D90
	public ButtonState(ButtonState s)
	{
		this.Hover = s.Hover;
		this.Clicked = s.Clicked;
		this.Selected = s.Selected;
	}

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00048BBC File Offset: 0x00046DBC
	public bool normal
	{
		get
		{
			return !this.Hover || !this.Selected;
		}
	}

	// Token: 0x0400086A RID: 2154
	public bool Hover;

	// Token: 0x0400086B RID: 2155
	public bool Clicked;

	// Token: 0x0400086C RID: 2156
	public bool Selected;
}
