using System;
using System.Collections.Generic;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020000A2 RID: 162
internal class ListStream
{
	// Token: 0x060003B0 RID: 944 RVA: 0x0001A5A4 File Offset: 0x000187A4
	public ListStream(bool isReading)
	{
		this.timestamp = Network.time;
		this.isReading = isReading;
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x060003B1 RID: 945 RVA: 0x0001A5CC File Offset: 0x000187CC
	public bool isWriting
	{
		get
		{
			return !this.isReading;
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0001A5D8 File Offset: 0x000187D8
	public void Serialize<T>(ref T v) where T : struct, IConvertible
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (T)((object)this.values[this.streamIndex++]);
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0001A634 File Offset: 0x00018834
	public void Serialize(ref ObscuredInt v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (ObscuredInt)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0001A690 File Offset: 0x00018890
	public void Serialize(ref bool v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (bool)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x0001A6E4 File Offset: 0x000188E4
	public void Serialize(ref char v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (char)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x0001A738 File Offset: 0x00018938
	public void Serialize(ref float v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (float)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x0001A78C File Offset: 0x0001898C
	public void Serialize(ref int v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			if (this.streamIndex >= this.values.Count)
			{
				v = 0;
			}
			else
			{
				v = (int)this.values[this.streamIndex];
			}
			this.streamIndex++;
		}
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0001A800 File Offset: 0x00018A00
	public void Serialize(ref Quaternion v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (Quaternion)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x0001A85C File Offset: 0x00018A5C
	public void Serialize(ref short v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (short)this.values[this.streamIndex++];
		}
	}

	// Token: 0x060003BA RID: 954 RVA: 0x0001A8B0 File Offset: 0x00018AB0
	public void Serialize(ref Vector3 v)
	{
		if (this.isWriting)
		{
			this.values.Add(v);
		}
		else
		{
			v = (Vector3)this.values[this.streamIndex++];
		}
	}

	// Token: 0x040003A5 RID: 933
	private List<object> values = new List<object>();

	// Token: 0x040003A6 RID: 934
	private int streamIndex;

	// Token: 0x040003A7 RID: 935
	public double timestamp;

	// Token: 0x040003A8 RID: 936
	public bool isReading;
}
