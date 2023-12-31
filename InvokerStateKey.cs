using System;
using System.Collections.Generic;

// Token: 0x02000087 RID: 135
[Serializable]
internal class InvokerStateKey : cwNetworkSerializable, Convertible, ReusableClass<InvokerStateKey>
{
	// Token: 0x060002EB RID: 747 RVA: 0x00015620 File Offset: 0x00013820
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "key", ref this.name, isWrite);
		JSON.ReadWrite(dict, "elapsed", ref this.elapsed, isWrite);
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00015654 File Offset: 0x00013854
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.name);
		stream.Serialize(ref this.elapsed);
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00015670 File Offset: 0x00013870
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.name);
		stream.Serialize(ref this.elapsed);
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0001568C File Offset: 0x0001388C
	public void Clone(InvokerStateKey i)
	{
		this.name = i.name;
		this.elapsed = i.elapsed;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x000156A8 File Offset: 0x000138A8
	public void Clear()
	{
		this.name = string.Empty;
		this.elapsed = 0f;
	}

	// Token: 0x04000353 RID: 851
	public string name = string.Empty;

	// Token: 0x04000354 RID: 852
	public float elapsed;
}
