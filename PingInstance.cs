using System;
using UnityEngine;

// Token: 0x020000B6 RID: 182
[Serializable]
internal class PingInstance
{
	// Token: 0x060004B6 RID: 1206 RVA: 0x0001F78C File Offset: 0x0001D98C
	public PingInstance(HostInfo hostInfo)
	{
		this.hostInfo = hostInfo;
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
	public int Ping
	{
		get
		{
			if (Network.peerType == NetworkPeerType.Server)
			{
				if (this.hostInfo.ForceNAT)
				{
					if (this.ping == null)
					{
						this.ping = new Ping(Globals.I.databaseIP);
					}
					if (this.ping.isDone)
					{
						this.tempPing = this.ping.time;
					}
				}
				else
				{
					this.tempPing = 0;
				}
			}
			else if (Network.peerType == NetworkPeerType.Client || Network.peerType == NetworkPeerType.Disconnected)
			{
				if (this.hostInfo.ForceNAT)
				{
					if (this.ping == null)
					{
						this.ping = new Ping(Globals.I.databaseIP);
					}
					if (this.ping.isDone)
					{
						this.tempPing = Math.Min(99999, this.ping.time + this.serverPing);
					}
				}
				else
				{
					if (this.ping == null)
					{
						this.ping = new Ping(this.hostInfo.Ip);
					}
					if (this.ping.isDone)
					{
						this.tempPing = this.ping.time;
					}
				}
			}
			return this.tempPing;
		}
	}

	// Token: 0x04000451 RID: 1105
	public HostInfo hostInfo;

	// Token: 0x04000452 RID: 1106
	private Ping ping;

	// Token: 0x04000453 RID: 1107
	private int tempPing = 99999;

	// Token: 0x04000454 RID: 1108
	private int serverPing = 99999;
}
