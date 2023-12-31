using System;
using UnityEngine;

// Token: 0x020002A7 RID: 679
[Serializable]
internal class PlayerCmd : cwNetworkSerializable, ReusableClass<PlayerCmd>
{
	// Token: 0x0600131F RID: 4895 RVA: 0x000CE698 File Offset: 0x000CC898
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.number);
		stream.Serialize(ref this.euler);
		stream.Serialize(ref this.buttons);
		stream.Serialize(ref this.CheatButtons);
		stream.Serialize(ref this.halfPing);
		this.packetLatency = this.halfPing + stream.halfPing;
	}

	// Token: 0x06001320 RID: 4896 RVA: 0x000CE6F4 File Offset: 0x000CC8F4
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.number);
		stream.Serialize(ref this.euler);
		stream.Serialize(ref this.buttons);
		stream.Serialize(ref this.CheatButtons);
		stream.Serialize(ref this.halfPing);
	}

	// Token: 0x06001321 RID: 4897 RVA: 0x000CE740 File Offset: 0x000CC940
	public void Clear()
	{
		this.number = 0;
		this.buttons = 0;
		this.CheatButtons = 0;
		this.halfPing = 0f;
		this.packetLatency = 0f;
		this.euler = Vector3.zero;
	}

	// Token: 0x06001322 RID: 4898 RVA: 0x000CE784 File Offset: 0x000CC984
	public void Clone(PlayerCmd i)
	{
		this.number = i.number;
		this.buttons = i.buttons;
		this.CheatButtons = i.CheatButtons;
		this.halfPing = i.halfPing;
		this.packetLatency = i.packetLatency;
		this.euler = i.euler;
	}

	// Token: 0x04001606 RID: 5638
	public int number = IDUtil.NoID;

	// Token: 0x04001607 RID: 5639
	public int buttons;

	// Token: 0x04001608 RID: 5640
	public int CheatButtons;

	// Token: 0x04001609 RID: 5641
	public float halfPing;

	// Token: 0x0400160A RID: 5642
	public float packetLatency;

	// Token: 0x0400160B RID: 5643
	public Vector3 euler = Vector3.zero;
}
