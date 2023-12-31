using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000186 RID: 390
	internal interface SpawnState
	{
		// Token: 0x06000B45 RID: 2885
		void OnGUI();

		// Token: 0x06000B46 RID: 2886
		void OnUpdate();

		// Token: 0x06000B47 RID: 2887
		void OnConnected();

		// Token: 0x06000B48 RID: 2888
		void Spawn();
	}
}
