using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Foundation
{
	// Token: 0x020002DC RID: 732
	internal class SimpleWatchlistElement : Convertible
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x000D8180 File Offset: 0x000D6380
		public void Convert(Dictionary<string, object> dict, bool isWrite)
		{
			JSON.ReadWrite(dict, string.Empty, ref this.UserId, isWrite);
		}

		// Token: 0x0400190D RID: 6413
		public int UserId;
	}
}
