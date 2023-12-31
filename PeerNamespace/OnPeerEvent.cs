using System;

namespace PeerNamespace
{
	// Token: 0x020000B5 RID: 181
	internal interface OnPeerEvent
	{
		// Token: 0x060004B3 RID: 1203
		void OnConnectionFailed();

		// Token: 0x060004B4 RID: 1204
		void OnConnectionSuccessful();

		// Token: 0x060004B5 RID: 1205
		void OnUpdateServersList();
	}
}
