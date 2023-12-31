using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000088 RID: 136
[Serializable]
internal class InvokerState : cwNetworkSerializable, Convertible, ReusableClass<InvokerState>
{
	// Token: 0x060002F1 RID: 753 RVA: 0x000156D8 File Offset: 0x000138D8
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		Debug.LogWarning("oops!");
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x000156E4 File Offset: 0x000138E4
	public void Serialize(eNetworkStream stream)
	{
		short num = (short)this.keys.Length;
		stream.Serialize(ref num);
		stream.Serialize<InvokerStateKey>(ref this.keys);
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00015714 File Offset: 0x00013914
	public void Deserialize(eNetworkStream stream)
	{
		short num = (short)this.keys.Length;
		stream.Serialize(ref num);
		this.keys.Clear();
		eCache.InvokerStateKey.Clear();
		for (int i = 0; i < (int)num; i++)
		{
			this.keys.Add(eCache.InvokerStateKey);
		}
		stream.Serialize<InvokerStateKey>(ref this.keys);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0001577C File Offset: 0x0001397C
	public void Clone(InvokerState i)
	{
		this.keys.Clone(i.keys);
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00015790 File Offset: 0x00013990
	public void Clear()
	{
		this.keys.Clear();
	}

	// Token: 0x04000355 RID: 853
	internal ClassArray<InvokerStateKey> keys = new ClassArray<InvokerStateKey>(16);
}
