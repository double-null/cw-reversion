using System;
using System.Collections.Generic;

// Token: 0x020002DF RID: 735
[Serializable]
internal class WeaponState : cwNetworkSerializable, Convertible, ReusableClass<WeaponState>
{
	// Token: 0x06001471 RID: 5233 RVA: 0x000D86C4 File Offset: 0x000D68C4
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "isMod", ref this.isMod, isWrite);
		JSON.ReadWrite(dict, "singleShot", ref this.singleShot, isWrite);
		JSON.ReadWrite(dict, "bagSize", ref this.bagSize, isWrite);
		JSON.ReadWrite(dict, "clips", ref this.clips, isWrite);
		JSON.ReadWrite(dict, "repair_info", ref this.repair_info, isWrite);
		JSON.ReadWrite(dict, "needReload", ref this.needReload, isWrite);
		JSON.ReadWrite(dict, "OpticModId", ref this.Mods, isWrite);
		JSON.ReadWrite<InvokerState>(dict, "G", ref this.G, isWrite);
	}

	// Token: 0x06001472 RID: 5234 RVA: 0x000D8764 File Offset: 0x000D6964
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.isMod);
		stream.Serialize(ref this.singleShot);
		this.sBagSize = (short)this.bagSize;
		stream.Serialize(ref this.sBagSize);
		this.sClips = (short)this.clips;
		stream.Serialize(ref this.sClips);
		stream.Serialize(ref this.repair_info);
		stream.Serialize(ref this.needReload);
		stream.Serialize(ref this.Mods);
		if (stream.isFullUpdate)
		{
			this.G.Serialize(stream);
		}
	}

	// Token: 0x06001473 RID: 5235 RVA: 0x000D87F8 File Offset: 0x000D69F8
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.isMod);
		stream.Serialize(ref this.singleShot);
		stream.Serialize(ref this.sBagSize);
		this.bagSize = (int)this.sBagSize;
		stream.Serialize(ref this.sClips);
		this.clips = (int)this.sClips;
		stream.Serialize(ref this.repair_info);
		stream.Serialize(ref this.needReload);
		stream.Serialize(ref this.Mods);
		if (stream.isFullUpdate)
		{
			this.G.Deserialize(stream);
		}
	}

	// Token: 0x06001474 RID: 5236 RVA: 0x000D8888 File Offset: 0x000D6A88
	public void Clear()
	{
		this.isMod = false;
		this.singleShot = false;
		this.bagSize = 0;
		this.clips = 0;
		this.repair_info = 0f;
		this.needReload = false;
		this.Mods = string.Empty;
		this.G.Clear();
	}

	// Token: 0x06001475 RID: 5237 RVA: 0x000D88DC File Offset: 0x000D6ADC
	public void Clone(WeaponState i)
	{
		this.isMod = i.isMod;
		this.singleShot = i.singleShot;
		this.bagSize = i.bagSize;
		this.clips = i.clips;
		this.repair_info = i.repair_info;
		this.needReload = i.needReload;
		this.Mods = i.Mods;
		this.G.Clone(i.G);
	}

	// Token: 0x06001476 RID: 5238 RVA: 0x000D8950 File Offset: 0x000D6B50
	public string OnChange(WeaponState i)
	{
		if (this.isMod != i.isMod)
		{
			return "mod fail";
		}
		if (this.singleShot != i.singleShot)
		{
			return "singleShot fail";
		}
		if (this.bagSize != i.bagSize)
		{
			return "bagSize fail";
		}
		if (this.clips != i.clips)
		{
			return "clips fail";
		}
		if (this.repair_info != i.repair_info)
		{
			return "repair_info fail";
		}
		if (this.needReload != i.needReload)
		{
			return "needReload fail";
		}
		if (this.Mods != i.Mods)
		{
			return "Mods fail";
		}
		return string.Empty;
	}

	// Token: 0x06001477 RID: 5239 RVA: 0x000D8A08 File Offset: 0x000D6C08
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			"{ ",
			this.isMod,
			" | ",
			this.singleShot,
			" | ",
			this.bagSize,
			" | ",
			this.clips,
			" | ",
			this.repair_info,
			" | ",
			this.needReload,
			" }"
		});
	}

	// Token: 0x04001927 RID: 6439
	public bool isMod;

	// Token: 0x04001928 RID: 6440
	public bool singleShot;

	// Token: 0x04001929 RID: 6441
	public int bagSize;

	// Token: 0x0400192A RID: 6442
	public int clips;

	// Token: 0x0400192B RID: 6443
	public float repair_info;

	// Token: 0x0400192C RID: 6444
	public bool needReload;

	// Token: 0x0400192D RID: 6445
	public string Mods = string.Empty;

	// Token: 0x0400192E RID: 6446
	public InvokerState G = new InvokerState();

	// Token: 0x0400192F RID: 6447
	private short sBagSize;

	// Token: 0x04001930 RID: 6448
	private short sClips;
}
