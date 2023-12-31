using System;

namespace TacticalPointNamespace
{
	// Token: 0x020002D0 RID: 720
	internal interface ITacticalPointState
	{
		// Token: 0x060013BD RID: 5053
		void Update();

		// Token: 0x060013BE RID: 5054
		void OnSet();

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060013BF RID: 5055
		TacticalPointState State { get; }
	}
}
