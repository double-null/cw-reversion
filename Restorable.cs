using System;

// Token: 0x02000053 RID: 83
public struct Restorable<T>
{
	// Token: 0x0600013E RID: 318 RVA: 0x0000C478 File Offset: 0x0000A678
	public Restorable(T val)
	{
		this.value = val;
		this.restoredValue = val;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0000C488 File Offset: 0x0000A688
	public T GetChecked(Restorable<T>.Check ch)
	{
		bool flag = ch();
		if (flag)
		{
			return this.value;
		}
		return this.restoredValue;
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000140 RID: 320 RVA: 0x0000C4B0 File Offset: 0x0000A6B0
	public T Original
	{
		get
		{
			return this.restoredValue;
		}
	}

	// Token: 0x06000141 RID: 321 RVA: 0x0000C4B8 File Offset: 0x0000A6B8
	public void Restore()
	{
		this.value = this.restoredValue;
	}

	// Token: 0x040001D9 RID: 473
	public T value;

	// Token: 0x040001DA RID: 474
	private T restoredValue;

	// Token: 0x020003A9 RID: 937
	// (Invoke) Token: 0x06001E0C RID: 7692
	public delegate bool Check();
}
