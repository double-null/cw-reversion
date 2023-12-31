using System;
using System.Collections.Generic;

// Token: 0x020000A6 RID: 166
[Serializable]
internal class UnityDictionarySI
{
	// Token: 0x060003CC RID: 972 RVA: 0x0001ACF4 File Offset: 0x00018EF4
	public bool ContainsKey(string key)
	{
		return this.keys.IndexOf(key) != -1;
	}

	// Token: 0x17000074 RID: 116
	public string this[int i]
	{
		get
		{
			if (i >= this.keys.Count)
			{
				return string.Empty;
			}
			return this.keys[i];
		}
	}

	// Token: 0x17000075 RID: 117
	public int this[string key]
	{
		get
		{
			if (this.keys.IndexOf(key) != -1)
			{
				return this.values[this.keys.IndexOf(key)];
			}
			return -13;
		}
		set
		{
			if (this.keys.IndexOf(key) == -1)
			{
				this.keys.Add(key);
				this.values.Add(value);
			}
			else
			{
				this.values[this.keys.IndexOf(key)] = value;
			}
		}
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x0001ADC0 File Offset: 0x00018FC0
	public void Clear()
	{
		this.keys.Clear();
		this.values.Clear();
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001ADD8 File Offset: 0x00018FD8
	public int Count
	{
		get
		{
			return this.keys.Count;
		}
	}

	// Token: 0x040003AE RID: 942
	private List<string> keys = new List<string>();

	// Token: 0x040003AF RID: 943
	private List<int> values = new List<int>();
}
