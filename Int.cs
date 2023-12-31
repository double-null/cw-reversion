using System;
using UnityEngine;

// Token: 0x02000051 RID: 81
[Serializable]
internal class Int
{
	// Token: 0x06000112 RID: 274 RVA: 0x0000BE64 File Offset: 0x0000A064
	public Int()
	{
		this.param = 0;
		this.coded = (this.param * 4 + 50 & this.param >> 3 & (this.param << 11) - this.param * this.param);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
	public Int(int i)
	{
		this.Value = i;
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000114 RID: 276 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
	// (set) Token: 0x06000115 RID: 277 RVA: 0x0000BF88 File Offset: 0x0000A188
	public int Value
	{
		get
		{
			int num = this.param * 4 + 50 & this.param >> 3 & (this.param << 11) - this.param * this.param;
			if (this.coded == num)
			{
				return this.param;
			}
			if (this.logOnce)
			{
				return this.param;
			}
			this.logOnce = true;
			EventFactory.Call("ShowPopup", new Popup(WindowsID.BANNED, Language.BadlyFinishedBoy, string.Empty, PopupState.banned, false, false, string.Empty, string.Empty));
			Main.AddDatabaseRequest<HoneyPot>(new object[]
			{
				"Cheats use attempt",
				ViolationType.CeArtmoney
			});
			Application.ExternalCall("window.open", new object[]
			{
				"http://www.youtube.com/watch?v=oHg5SJYRHA0"
			});
			return this.param;
		}
		set
		{
			this.param = value;
			this.coded = (this.param * 4 + 50 & this.param >> 3 & (this.param << 11) - this.param * this.param);
		}
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000BFC4 File Offset: 0x0000A1C4
	public override bool Equals(object i)
	{
		return this.Value == ((Int)i).Value;
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000BFDC File Offset: 0x0000A1DC
	public override int GetHashCode()
	{
		return this.Value.GetHashCode();
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000BFF8 File Offset: 0x0000A1F8
	public override string ToString()
	{
		return this.Value.ToString();
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000C014 File Offset: 0x0000A214
	public static Int operator +(Int i1, int i2)
	{
		Int @int = new Int();
		return i1.Value + i2;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000C038 File Offset: 0x0000A238
	public static Int operator -(Int i1, int i2)
	{
		Int @int = new Int();
		return i1.Value - i2;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000C05C File Offset: 0x0000A25C
	public static Int operator *(Int i1, int i2)
	{
		Int @int = new Int();
		return i1.Value * i2;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000C080 File Offset: 0x0000A280
	public static Int operator /(Int i1, int i2)
	{
		Int @int = new Int();
		return i1.Value / i2;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000C0A4 File Offset: 0x0000A2A4
	public static Int operator %(Int i1, int i2)
	{
		Int @int = new Int();
		return i1.Value % i2;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
	public static Int operator ++(Int i)
	{
		i.Value++;
		return i;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000C0DC File Offset: 0x0000A2DC
	public static Int operator --(Int i)
	{
		i.Value--;
		return i;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
	public static bool operator >(Int i1, int i2)
	{
		return i1.Value > i2;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000C0FC File Offset: 0x0000A2FC
	public static bool operator <(Int i1, int i2)
	{
		return i1.Value < i2;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000C108 File Offset: 0x0000A308
	public static bool operator >=(Int i1, int i2)
	{
		return i1.Value >= i2;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000C118 File Offset: 0x0000A318
	public static bool operator <=(Int i1, int i2)
	{
		return i1.Value <= i2;
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000C128 File Offset: 0x0000A328
	public static bool operator ==(Int i1, int i2)
	{
		return i1.Value == i2;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000C134 File Offset: 0x0000A334
	public static bool operator !=(Int i1, int i2)
	{
		return i1.Value != i2;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000C144 File Offset: 0x0000A344
	public static implicit operator int(Int x)
	{
		return x.Value;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000C14C File Offset: 0x0000A34C
	public static implicit operator Int(int x)
	{
		return new Int(x);
	}

	// Token: 0x040001D3 RID: 467
	private int param;

	// Token: 0x040001D4 RID: 468
	private int coded;

	// Token: 0x040001D5 RID: 469
	private bool logOnce;
}
