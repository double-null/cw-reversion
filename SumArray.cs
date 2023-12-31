using System;

// Token: 0x0200039E RID: 926
internal class SumArray
{
	// Token: 0x06001D86 RID: 7558 RVA: 0x001031E0 File Offset: 0x001013E0
	public SumArray(int length)
	{
		this._vals = new float[length];
		this.Length = length;
	}

	// Token: 0x17000850 RID: 2128
	// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00103210 File Offset: 0x00101410
	// (set) Token: 0x06001D88 RID: 7560 RVA: 0x00103218 File Offset: 0x00101418
	public int Length { get; private set; }

	// Token: 0x17000851 RID: 2129
	// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00103224 File Offset: 0x00101424
	// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0010322C File Offset: 0x0010142C
	public int CurrentLength { get; private set; }

	// Token: 0x17000852 RID: 2130
	// (get) Token: 0x06001D8B RID: 7563 RVA: 0x00103238 File Offset: 0x00101438
	// (set) Token: 0x06001D8C RID: 7564 RVA: 0x00103240 File Offset: 0x00101440
	public float Sum { get; private set; }

	// Token: 0x06001D8D RID: 7565 RVA: 0x0010324C File Offset: 0x0010144C
	public void AddValue(float val)
	{
		if (!this._full)
		{
			this._lastIndex++;
			this.CurrentLength = this._lastIndex;
			if (this._lastIndex == this.Length)
			{
				this._lastIndex = 0;
				this._full = true;
			}
		}
		else
		{
			this._lastIndex++;
			if (this._lastIndex == this.Length)
			{
				this._lastIndex = 0;
			}
		}
		this.Sum -= this._vals[this._lastIndex];
		this._vals[this._lastIndex] = val;
		this.Sum += val;
	}

	// Token: 0x04002242 RID: 8770
	private float[] _vals;

	// Token: 0x04002243 RID: 8771
	private int _lastIndex = -1;

	// Token: 0x04002244 RID: 8772
	private bool _full;
}
