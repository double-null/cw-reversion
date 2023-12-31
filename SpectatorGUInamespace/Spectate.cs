using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000181 RID: 385
	internal interface Spectate
	{
		// Token: 0x06000B25 RID: 2853
		void next();

		// Token: 0x06000B26 RID: 2854
		void OnConnected();

		// Token: 0x06000B27 RID: 2855
		void Enable();

		// Token: 0x06000B28 RID: 2856
		void Disable();

		// Token: 0x06000B29 RID: 2857
		void OnGUI();
	}
}
