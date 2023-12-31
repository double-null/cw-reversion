using System;
using System.Collections.Generic;

namespace LeagueSystem
{
	// Token: 0x020002FE RID: 766
	internal class PlayerData
	{
		// Token: 0x06001A51 RID: 6737 RVA: 0x000EF774 File Offset: 0x000ED974
		public void Read(Dictionary<string, object> data)
		{
			JSON.ReadWrite(data, "team", ref this.Team, false);
			JSON.ReadWrite(data, "uid", ref this.UserID, false);
			int status = (int)this.Status;
			JSON.ReadWrite(data, "status", ref status, true);
			this.Status = (PlayerStatus)status;
			JSON.ReadWrite(data, "disconnectTime", ref this.DisconnectTime, false);
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000EF7D4 File Offset: 0x000ED9D4
		public bool Leaver
		{
			get
			{
				return this.Status == PlayerStatus.leaver;
			}
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000EF7E0 File Offset: 0x000ED9E0
		public void Write(Dictionary<string, object> data)
		{
			JSON.ReadWrite(data, "team", ref this.Team, true);
			JSON.ReadWrite(data, "uid", ref this.UserID, true);
			int status = (int)this.Status;
			JSON.ReadWrite(data, "status", ref status, true);
			JSON.ReadWrite(data, "disconnectTime", ref this.DisconnectTime, true);
		}

		// Token: 0x04001EF9 RID: 7929
		public PlayerStatus Status;

		// Token: 0x04001EFA RID: 7930
		public int Team;

		// Token: 0x04001EFB RID: 7931
		public int UserID;

		// Token: 0x04001EFC RID: 7932
		public float DisconnectTime;
	}
}
