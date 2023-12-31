using System;

// Token: 0x02000086 RID: 134
internal interface eObserved
{
	// Token: 0x060002E8 RID: 744
	void OnNetworkEvent(eNetworkStream stream);

	// Token: 0x060002E9 RID: 745
	UpdateType GetUpdateType();
}
