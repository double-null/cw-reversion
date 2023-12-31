using System;
using UnityEngine;

// Token: 0x02000052 RID: 82
[Serializable]
internal class Float
{
	// Token: 0x06000128 RID: 296 RVA: 0x0000C154 File Offset: 0x0000A354
	public Float()
	{
		this.param = 0f;
		this.coded = this.param * 3f + 3f * this.param - this.param * this.param * (1f + this.param);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000C1AC File Offset: 0x0000A3AC
	public Float(float i)
	{
		this.Value = i;
	}

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x0600012A RID: 298 RVA: 0x0000C1BC File Offset: 0x0000A3BC
	// (set) Token: 0x0600012B RID: 299 RVA: 0x0000C28C File Offset: 0x0000A48C
	public float Value
	{
		get
		{
			float num = this.param * 3f + 3f * this.param - this.param * this.param * (1f + this.param);
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
			this.coded = this.param * 3f + 3f * this.param - this.param * this.param * (1f + this.param);
		}
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000C2DC File Offset: 0x0000A4DC
	public override bool Equals(object i)
	{
		return this.Value == ((Float)i).Value;
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000C2F4 File Offset: 0x0000A4F4
	public override int GetHashCode()
	{
		return this.Value.GetHashCode();
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000C310 File Offset: 0x0000A510
	public override string ToString()
	{
		return this.Value.ToString();
	}

	// Token: 0x0600012F RID: 303 RVA: 0x0000C32C File Offset: 0x0000A52C
	public static Float operator +(Float i1, float i2)
	{
		Float @float = new Float();
		return i1.Value + i2;
	}

	// Token: 0x06000130 RID: 304 RVA: 0x0000C350 File Offset: 0x0000A550
	public static Float operator -(Float i1, float i2)
	{
		Float @float = new Float();
		return i1.Value - i2;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x0000C374 File Offset: 0x0000A574
	public static Float operator *(Float i1, float i2)
	{
		Float @float = new Float();
		return i1.Value * i2;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x0000C398 File Offset: 0x0000A598
	public static Float operator /(Float i1, float i2)
	{
		Float @float = new Float();
		return i1.Value / i2;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000C3BC File Offset: 0x0000A5BC
	public static Float operator %(Float i1, float i2)
	{
		Float @float = new Float();
		return i1.Value % 2f;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x0000C3E4 File Offset: 0x0000A5E4
	public static Float operator ++(Float i)
	{
		i.Value += 1f;
		return i;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x0000C3FC File Offset: 0x0000A5FC
	public static Float operator --(Float i)
	{
		i.Value -= 1f;
		return i;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x0000C414 File Offset: 0x0000A614
	public static bool operator >(Float i1, float i2)
	{
		return i1.Value > i2;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000C420 File Offset: 0x0000A620
	public static bool operator <(Float i1, float i2)
	{
		return i1.Value < i2;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000C42C File Offset: 0x0000A62C
	public static bool operator >=(Float i1, float i2)
	{
		return i1.Value >= i2;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000C43C File Offset: 0x0000A63C
	public static bool operator <=(Float i1, float i2)
	{
		return i1.Value <= i2;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x0000C44C File Offset: 0x0000A64C
	public static bool operator ==(Float i1, float i2)
	{
		return i1.Value == i2;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x0000C458 File Offset: 0x0000A658
	public static bool operator !=(Float i1, float i2)
	{
		return i1.Value != i2;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000C468 File Offset: 0x0000A668
	public static explicit operator float(Float x)
	{
		return x.Value;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000C470 File Offset: 0x0000A670
	public static implicit operator Float(float x)
	{
		return new Float(x);
	}

	// Token: 0x040001D6 RID: 470
	private float param;

	// Token: 0x040001D7 RID: 471
	private float coded;

	// Token: 0x040001D8 RID: 472
	private bool logOnce;
}
