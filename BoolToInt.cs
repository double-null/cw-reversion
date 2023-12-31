using System;

// Token: 0x020000A3 RID: 163
public struct BoolToInt
{
	// Token: 0x060003BB RID: 955 RVA: 0x0001A90C File Offset: 0x00018B0C
	public BoolToInt(byte count)
	{
		this.index = 0;
		this.Compressed = 0;
		this.index = count;
		this.size = 32;
	}

	// Token: 0x060003BC RID: 956 RVA: 0x0001A92C File Offset: 0x00018B2C
	public bool Push(bool b)
	{
		if (this.index < this.size && this.index >= 0)
		{
			this.Compressed |= ((!b) ? 0 : 1) << (int)this.index;
			this.index += 1;
			return true;
		}
		return false;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x0001A990 File Offset: 0x00018B90
	public bool Pop()
	{
		if (this.index <= this.size && this.index > 0)
		{
			bool result = (this.Compressed & 1 << (int)(this.index - 1)) != 0;
			this.Compressed ^= 1 << (int)(this.index - 1);
			this.index -= 1;
			return result;
		}
		return false;
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x060003BE RID: 958 RVA: 0x0001AA04 File Offset: 0x00018C04
	public int Count
	{
		get
		{
			return (int)this.index;
		}
	}

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x060003BF RID: 959 RVA: 0x0001AA0C File Offset: 0x00018C0C
	public bool IsFull
	{
		get
		{
			return this.index >= this.size;
		}
	}

	// Token: 0x040003A9 RID: 937
	public int Compressed;

	// Token: 0x040003AA RID: 938
	private byte size;

	// Token: 0x040003AB RID: 939
	private byte index;
}
