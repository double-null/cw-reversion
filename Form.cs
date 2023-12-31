using System;
using UnityEngine;

// Token: 0x0200007D RID: 125
[AddComponentMenu("Scripts/Engine/Foundation/Form")]
public class Form : MonoEvented
{
	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000291 RID: 657 RVA: 0x00014A6C File Offset: 0x00012C6C
	public bool IsFocused
	{
		get
		{
			return this.WindowID == this.gui.FocusedWindow;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x06000292 RID: 658 RVA: 0x00014A84 File Offset: 0x00012C84
	public virtual int Height
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x06000293 RID: 659 RVA: 0x00014A88 File Offset: 0x00012C88
	public virtual int Width
	{
		get
		{
			return 0;
		}
	}

	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000294 RID: 660 RVA: 0x00014A8C File Offset: 0x00012C8C
	public virtual Rect Rect
	{
		get
		{
			if (Globals.I.AdEnabled)
			{
				return new Rect((float)(Screen.width / 2 - this.Width / 2) + this.rect.x, 10f, (float)this.Width, (float)this.Height);
			}
			return new Rect((float)(Screen.width / 2 - this.Width / 2) + this.rect.x, (float)(Screen.height / 2 - this.Height / 2) + this.rect.y, (float)this.Width, (float)this.Height);
		}
	}

	// Token: 0x06000295 RID: 661 RVA: 0x00014B2C File Offset: 0x00012D2C
	public void Move(int dx, int dy)
	{
		this.rect.x = this.rect.x + (float)dx;
		this.rect.y = this.rect.y + (float)dy;
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00014B64 File Offset: 0x00012D64
	public void MoveTo(int x, int y)
	{
		this.rect.x = (float)x;
		this.rect.y = (float)y;
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000297 RID: 663 RVA: 0x00014B80 File Offset: 0x00012D80
	public bool Showing
	{
		get
		{
			if (this.alpha.Get() == 1f || this.alpha.NotExist())
			{
				this.showing = false;
			}
			return this.showing;
		}
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000298 RID: 664 RVA: 0x00014BC0 File Offset: 0x00012DC0
	public bool Hiding
	{
		get
		{
			if (this.alpha.Get() == 1f || this.alpha.NotExist())
			{
				this.hiding = false;
			}
			return this.hiding;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000299 RID: 665 RVA: 0x00014C00 File Offset: 0x00012E00
	public float visibility
	{
		get
		{
			return this.alpha.Get();
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600029A RID: 666 RVA: 0x00014C10 File Offset: 0x00012E10
	public bool Visible
	{
		get
		{
			return this.alpha.Get() >= 0f;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600029B RID: 667 RVA: 0x00014C28 File Offset: 0x00012E28
	public bool FocusVisible
	{
		get
		{
			return this.alpha.Get() >= 0.5f;
		}
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x0600029C RID: 668 RVA: 0x00014C40 File Offset: 0x00012E40
	public bool MaxVisible
	{
		get
		{
			return this.alpha.Get() == 1f;
		}
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00014C54 File Offset: 0x00012E54
	public virtual void Show(float time = 0.5f, float delay = 0f)
	{
		if (this.Showing || this.MaxVisible)
		{
			return;
		}
		this.showing = true;
		this.hiding = false;
		Vector2[] v = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(delay, 0f),
			new Vector2(time + delay, 1f),
			new Vector2(float.MaxValue, 1f)
		};
		this.alpha.Init(v);
		if (this.gui)
		{
			this.gui.FocusWindow(this.windowID);
		}
	}

	// Token: 0x0600029E RID: 670 RVA: 0x00014D20 File Offset: 0x00012F20
	public virtual void Hide(float time = 0.35f)
	{
		if (this.Hiding || !this.Visible)
		{
			return;
		}
		this.showing = false;
		this.hiding = true;
		Vector2[] v = new Vector2[]
		{
			new Vector2(0f, this.alpha.Get()),
			new Vector2(time, 0f)
		};
		this.alpha.Init(v);
		this.gui.UnFocusWindow(this.windowID);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00014DB0 File Offset: 0x00012FB0
	public static int Compare(object a, object b)
	{
		MonoEvented monoEvented = (MonoEvented)a;
		MonoEvented monoEvented2 = (MonoEvented)b;
		return monoEvented.WindowID.CompareTo(monoEvented2.WindowID);
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x060002A0 RID: 672 RVA: 0x00014DE0 File Offset: 0x00012FE0
	// (set) Token: 0x060002A1 RID: 673 RVA: 0x00014DE8 File Offset: 0x00012FE8
	public override bool isRendering
	{
		get
		{
			return this.rendering;
		}
		set
		{
			this.rendering = value;
		}
	}

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x060002A2 RID: 674 RVA: 0x00014DF4 File Offset: 0x00012FF4
	// (set) Token: 0x060002A3 RID: 675 RVA: 0x00014DFC File Offset: 0x00012FFC
	public override bool isUpdating
	{
		get
		{
			return this.updating;
		}
		set
		{
			this.updating = value;
		}
	}

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x060002A4 RID: 676 RVA: 0x00014E08 File Offset: 0x00013008
	// (set) Token: 0x060002A5 RID: 677 RVA: 0x00014E10 File Offset: 0x00013010
	public override bool isGameHandler
	{
		get
		{
			return this.gameHandler;
		}
		set
		{
			this.gameHandler = value;
		}
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060002A6 RID: 678 RVA: 0x00014E1C File Offset: 0x0001301C
	// (set) Token: 0x060002A7 RID: 679 RVA: 0x00014E24 File Offset: 0x00013024
	public override int WindowID
	{
		get
		{
			return this.windowID;
		}
		set
		{
			this.windowID = value;
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00014E30 File Offset: 0x00013030
	public override void MainInitialize()
	{
		this.gui = GameObject.Find("main").GetComponent<Main>().GetComponentInChildren<MainGUI>();
		if (this.isRendering)
		{
			this.windowID = (int)Enum.Parse(typeof(WindowsID), base.GetType().ToString());
		}
		base.MainInitialize();
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x00014E90 File Offset: 0x00013090
	public override void Clear()
	{
		this.alpha = new GraphicValue();
		base.StopAllCoroutines();
		base.CancelInvoke();
	}

	// Token: 0x04000335 RID: 821
	internal MainGUI gui;

	// Token: 0x04000336 RID: 822
	private bool rendering;

	// Token: 0x04000337 RID: 823
	private bool updating;

	// Token: 0x04000338 RID: 824
	private bool gameHandler;

	// Token: 0x04000339 RID: 825
	protected int windowID = -1;

	// Token: 0x0400033A RID: 826
	internal GraphicValue alpha = new GraphicValue();

	// Token: 0x0400033B RID: 827
	protected Rect rect = new Rect(0f, 0f, 0f, 0f);

	// Token: 0x0400033C RID: 828
	private bool showing;

	// Token: 0x0400033D RID: 829
	private bool hiding;
}
