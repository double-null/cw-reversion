using System;

// Token: 0x02000079 RID: 121
[Serializable]
internal class ClassArray<T> where T : class, ReusableClass<T>, new()
{
	// Token: 0x0600026D RID: 621 RVA: 0x00013FA8 File Offset: 0x000121A8
	internal ClassArray(int size)
	{
		this.array = new T[size];
		this.indexes = new int[size];
		for (int i = 0; i < this.array.Length; i++)
		{
			this.array[i] = Activator.CreateInstance<T>();
			this.array[i].Clear();
			this.indexes[i] = IDUtil.NoID;
		}
	}

	// Token: 0x17000033 RID: 51
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

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x06000270 RID: 624 RVA: 0x000140C0 File Offset: 0x000122C0
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

	// Token: 0x06000271 RID: 625 RVA: 0x00014100 File Offset: 0x00012300
	public void RemoveAt(int index)
	{
		for (int i = 0; i < this.indexes.Length; i++)
		{
			if (this.indexes[i] == index)
			{
				this.array[i].Clear();
				this.indexes[i] = IDUtil.NoID;
			}
			else if (this.indexes[i] > index)
			{
				this.indexes[i]--;
			}
		}
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0001417C File Offset: 0x0001237C
	public void RemoveRange(int startIndex, int count)
	{
		for (int i = 0; i < count; i++)
		{
			this.RemoveAt(startIndex);
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x000141A4 File Offset: 0x000123A4
	public void Clear()
	{
		for (int i = 0; i < this.array.Length; i++)
		{
			this.array[i].Clear();
			this.indexes[i] = IDUtil.NoID;
		}
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000141F0 File Offset: 0x000123F0
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
				this.array[i].Clone(t);
				this.indexes[i] = this.Length;
				return;
			}
		}
		throw new IndexOutOfRangeException();
	}

	// Token: 0x06000275 RID: 629 RVA: 0x00014270 File Offset: 0x00012470
	public int IndexOf(T t)
	{
		for (int i = 0; i < this.indexes.Length; i++)
		{
			if (this.array[i] == t)
			{
				return this.indexes[i];
			}
		}
		return -1;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000142BC File Offset: 0x000124BC
	public void Clone(ClassArray<T> i)
	{
		if (this.array.Length != i.array.Length)
		{
			throw new ArrayTypeMismatchException();
		}
		for (int j = 0; j < this.array.Length; j++)
		{
			this.array[j].Clone(i.array[j]);
			this.indexes[j] = i.indexes[j];
		}
	}

	// Token: 0x04000321 RID: 801
	private T[] array;

	// Token: 0x04000322 RID: 802
	private int[] indexes;
}
