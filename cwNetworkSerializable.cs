using System;

// Token: 0x02000085 RID: 133
internal interface cwNetworkSerializable
{
	// Token: 0x060002E6 RID: 742
	void Serialize(eNetworkStream stream);

	// Token: 0x060002E7 RID: 743
	void Deserialize(eNetworkStream stream);
}
