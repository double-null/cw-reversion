using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000AD RID: 173
[Serializable]
internal class eVector3 : Convertible
{
	// Token: 0x060003FF RID: 1023 RVA: 0x0001B504 File Offset: 0x00019704
	public eVector3()
	{
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x0001B50C File Offset: 0x0001970C
	public eVector3(Vector3 vector)
	{
		this.vector = vector;
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001B51C File Offset: 0x0001971C
	// (set) Token: 0x06000402 RID: 1026 RVA: 0x0001B524 File Offset: 0x00019724
	public Vector3 Value
	{
		get
		{
			return this.vector;
		}
		set
		{
			this.vector = value;
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0001B530 File Offset: 0x00019730
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "x", ref this.vector.x, isWrite);
		JSON.ReadWrite(dict, "y", ref this.vector.y, isWrite);
		JSON.ReadWrite(dict, "z", ref this.vector.z, isWrite);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001B584 File Offset: 0x00019784
	public static explicit operator Vector3(eVector3 x)
	{
		return x.Value;
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001B58C File Offset: 0x0001978C
	public static implicit operator eVector3(Vector3 x)
	{
		return new eVector3(x);
	}

	// Token: 0x040003BD RID: 957
	private Vector3 vector;
}
