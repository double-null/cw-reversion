using System;
using System.Collections.Generic;

namespace LeagueSystem
{
	// Token: 0x02000301 RID: 769
	[Serializable]
	internal class MatchJsonData
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x000F0164 File Offset: 0x000EE364
		public void SetRemoteData(RemoteMatchenddData data)
		{
			if (data.data == null)
			{
				return;
			}
			foreach (PlayerJsonData playerJsonData in this.players)
			{
				RemoteMatchendPlayerData remoteData;
				if (data.data.TryGetValue(playerJsonData.uid, out remoteData))
				{
					playerJsonData.SetRemoteData(remoteData);
				}
			}
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000F01F0 File Offset: 0x000EE3F0
		public void AddUserStats(string uid, Stats stats)
		{
			for (int i = 0; i < this.players.Count; i++)
			{
				if (this.players[i].uid == uid)
				{
					this.players[i].SetPlayerStats(stats);
					break;
				}
			}
		}

		// Token: 0x04001F2F RID: 7983
		public int result;

		// Token: 0x04001F30 RID: 7984
		public string match_id = string.Empty;

		// Token: 0x04001F31 RID: 7985
		public string ip = string.Empty;

		// Token: 0x04001F32 RID: 7986
		public int port;

		// Token: 0x04001F33 RID: 7987
		public int bear_points;

		// Token: 0x04001F34 RID: 7988
		public int usec_points;

		// Token: 0x04001F35 RID: 7989
		public List<PlayerJsonData> players = new List<PlayerJsonData>();

		// Token: 0x04001F36 RID: 7990
		public string[] CancelledPlayers;
	}
}
