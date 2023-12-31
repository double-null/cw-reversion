using System;
using UnityEngine;

// Token: 0x020000A8 RID: 168
[Serializable]
internal class eNetworkPlayer
{
	// Token: 0x060003D4 RID: 980 RVA: 0x0001AE28 File Offset: 0x00019028
	public eNetworkPlayer()
	{
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0001AE30 File Offset: 0x00019030
	public eNetworkPlayer(NetworkMessageInfo msg)
	{
		if (msg.sender != default(NetworkPlayer))
		{
			this.owner = msg.sender;
		}
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0001AE6C File Offset: 0x0001906C
	public eNetworkPlayer(NetworkPlayer player)
	{
		this.owner = player;
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x060003D7 RID: 983 RVA: 0x0001AE7C File Offset: 0x0001907C
	public bool IsVirtual
	{
		get
		{
			NetworkPlayer? networkPlayer = this.unity;
			return networkPlayer == null;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x060003D8 RID: 984 RVA: 0x0001AE9C File Offset: 0x0001909C
	// (set) Token: 0x060003D9 RID: 985 RVA: 0x0001AEAC File Offset: 0x000190AC
	public NetworkPlayer owner
	{
		get
		{
			return this.unity.Value;
		}
		set
		{
			this.unity = new NetworkPlayer?(value);
		}
	}

	// Token: 0x040003B0 RID: 944
	private NetworkPlayer? unity;
}
