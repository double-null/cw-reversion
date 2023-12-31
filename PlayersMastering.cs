using System;
using System.Collections.Generic;

// Token: 0x02000354 RID: 852
internal class PlayersMastering
{
	// Token: 0x17000825 RID: 2085
	// (get) Token: 0x06001C46 RID: 7238 RVA: 0x000FBFE8 File Offset: 0x000FA1E8
	public static PlayersMastering Instance
	{
		get
		{
			return PlayersMastering._instance;
		}
	}

	// Token: 0x0400210B RID: 8459
	private static readonly PlayersMastering _instance = new PlayersMastering();

	// Token: 0x0400210C RID: 8460
	public Dictionary<int, PlayerSuits> Players = new Dictionary<int, PlayerSuits>();
}
