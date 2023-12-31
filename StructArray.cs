using System;

// Token: 0x020000A5 RID: 165
[Serializable]
internal class StructArray<T> where T : struct, eStruct<T>
{
	// Token: 0x060003C2 RID: 962 RVA: 0x0001AA20 File Offset: 0x00018C20
	public StructArray(int size)
	{
		this.array = new T[size];
		this.indexes = new int[size];
		for (int i = 0; i < this.array.Length; i++)
		{
			this.array[i] = default(T);
			this.array[i].Clear();
			this.indexes[i] = IDUtil.NoID;
		}
	}

	// Token: 0x17000071 RID: 113
	public T this[int index]
	{
		get
		{
			for (int i = 0; i < this.indexes.Length; i++)
			{
				if (this.indexes[i] == index)
				{
					return this.array[i];
				}
			}
			throw new IndexOutOfRangeException();
		}
		set
		{
			for (int i = 0; i < this.indexes.Length; i++)
			{
				if (this.indexes[i] == index)
				{
					this.array[i].Clone(value);
					return;
				}
			}
			throw new IndexOutOfRangeException();
		}
	}

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001AB3C File Offset: 0x00018D3C
	public int Length
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.indexes.Length; i++)
			{
				if (this.indexes[i] != IDUtil.NoID)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x060003C6 RID: 966 RVA: 0x0001AB7C File Offset: 0x00018D7C
	public int Count
	{
		get
		{
			return this.Length;
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0001AB84 File Offset: 0x00018D84
	public void RemoveAt(int index)
	{
		for (int i = 0; i < this.indexes.Length; i++)
		{
			if (this.indexes[i] >= index)
			{
				this.array[i].Clear();
				this.indexes[i]--;
			}
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x0001ABE0 File Offset: 0x00018DE0
	public void RemoveRange(int startIndex, int count)
	{
		for (int i = 0; i < count; i++)
		{
			this.RemoveAt(startIndex);
		}
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0001AC08 File Offset: 0x00018E08
	public void Clear()
	{
		for (int i = 0; i < this.array.Length; i++)
		{
			this.array[i].Clear();
			this.indexes[i] = IDUtil.NoID;
		}
	}

	// Token: 0x060003CA RID: 970 RVA: 0x0001AC54 File Offset: 0x00018E54
	public void Add(T t)
	{
		if (this.Length == this.indexes.Length)
		{
			this.RemoveAt(0);
		}
		for (int i = 0; i < this.indexes.Length; i++)
		{
			if (this.indexes[i] == IDUtil.NoID)
			{
				this.indexes[i] = this.Length;
				this.array[i].Clone(t);
				return;
			}
		}
		throw new IndexOutOfRangeException();
	}

	// Token: 0x040003AC RID: 940
	private T[] array;

	// Token: 0x040003AD RID: 941
	private int[] indexes;
}
