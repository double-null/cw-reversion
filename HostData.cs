using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000081 RID: 129
[Serializable]
internal class HostData : Convertible
{
	// Token: 0x060002DC RID: 732 RVA: 0x000154A4 File Offset: 0x000136A4
	public HostData()
	{
	}

	// Token: 0x060002DD RID: 733 RVA: 0x000154C4 File Offset: 0x000136C4
	public HostData(UnityEngine.HostData i)
	{
		this.comment = i.comment;
		this.connectedPlayers = i.connectedPlayers;
		this.gameName = i.gameName;
		this.gameType = i.gameType;
		this.guid = i.guid;
		this.ip = i.ip[0];
		this.playerLimit = i.playerLimit;
		this.port = i.port;
		this.useNat = i.useNat;
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060002DE RID: 734 RVA: 0x0001555C File Offset: 0x0001375C
	// (set) Token: 0x060002DF RID: 735 RVA: 0x00015564 File Offset: 0x00013764
	public int Port
	{
		get
		{
			return this.port;
		}
		set
		{
			this.port = value;
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060002E0 RID: 736 RVA: 0x00015570 File Offset: 0x00013770
	public string Ip
	{
		get
		{
			return this.ip;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060002E1 RID: 737 RVA: 0x00015578 File Offset: 0x00013778
	public string GUID
	{
		get
		{
			return this.guid;
		}
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00015580 File Offset: 0x00013780
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "comment", ref this.comment, isWrite);
		JSON.ReadWrite(dict, "connectedPlayers", ref this.connectedPlayers, isWrite);
		JSON.ReadWrite(dict, "gameName", ref this.gameName, isWrite);
		JSON.ReadWrite(dict, "gameType", ref this.gameType, isWrite);
		JSON.ReadWrite(dict, "guid", ref this.guid, isWrite);
		JSON.ReadWrite(dict, "playerLimit", ref this.playerLimit, isWrite);
		JSON.ReadWrite(dict, "useNat", ref this.useNat, isWrite);
	}

	// Token: 0x0400034A RID: 842
	private string comment;

	// Token: 0x0400034B RID: 843
	private int connectedPlayers;

	// Token: 0x0400034C RID: 844
	private string gameName;

	// Token: 0x0400034D RID: 845
	private string gameType;

	// Token: 0x0400034E RID: 846
	private string guid;

	// Token: 0x0400034F RID: 847
	public string ip = string.Empty;

	// Token: 0x04000350 RID: 848
	private int playerLimit;

	// Token: 0x04000351 RID: 849
	public int port = 27015;

	// Token: 0x04000352 RID: 850
	private bool useNat;
}
