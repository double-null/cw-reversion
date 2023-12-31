using System;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D2 RID: 210
	internal interface BannerQueue
	{
		// Token: 0x060005A1 RID: 1441
		void OnGUI();

		// Token: 0x060005A2 RID: 1442
		void Clear();

		// Token: 0x060005A3 RID: 1443
		void Init(float speed);

		// Token: 0x060005A4 RID: 1444
		bool Inited();

		// Token: 0x060005A5 RID: 1445
		bool Complete();
	}
}
