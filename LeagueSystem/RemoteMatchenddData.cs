using System;
using System.Collections.Generic;

namespace LeagueSystem
{
	// Token: 0x02000303 RID: 771
	internal class RemoteMatchenddData
	{
		// Token: 0x04001F3C RID: 7996
		public int result;

		// Token: 0x04001F3D RID: 7997
		public Dictionary<string, RemoteMatchendPlayerData> data = new Dictionary<string, RemoteMatchendPlayerData>();
	}
}
