using System;

namespace Samples
{
	// Token: 0x02000174 RID: 372
	internal interface item
	{
		// Token: 0x06000A92 RID: 2706
		void Set(SpawnEngine e);

		// Token: 0x06000A93 RID: 2707
		int GetID();

		// Token: 0x06000A94 RID: 2708
		void OnClick();

		// Token: 0x06000A95 RID: 2709
		void OnGUI();

		// Token: 0x06000A96 RID: 2710
		void Reset();
	}
}
