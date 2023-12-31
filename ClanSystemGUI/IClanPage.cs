using System;

namespace ClanSystemGUI
{
	// Token: 0x02000107 RID: 263
	internal interface IClanPage
	{
		// Token: 0x060006E9 RID: 1769
		void OnStart();

		// Token: 0x060006EA RID: 1770
		void OnGUI();

		// Token: 0x060006EB RID: 1771
		void OnUpdate();

		// Token: 0x060006EC RID: 1772
		void Clear();
	}
}
