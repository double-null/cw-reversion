using System;
using UnityEngine;

namespace LeagueGUI
{
	// Token: 0x0200030D RID: 781
	internal interface IWindow
	{
		// Token: 0x06001A9F RID: 6815
		void SetRect(Rect rect);

		// Token: 0x06001AA0 RID: 6816
		void OnDrawWindow();

		// Token: 0x06001AA1 RID: 6817
		void OnStart();

		// Token: 0x06001AA2 RID: 6818
		void OnGUI();

		// Token: 0x06001AA3 RID: 6819
		void OnUpdate();

		// Token: 0x06001AA4 RID: 6820
		void OnReadyGame();

		// Token: 0x06001AA5 RID: 6821
		void OnJoinGame();

		// Token: 0x06001AA6 RID: 6822
		void OnWaitingPlayers();

		// Token: 0x06001AA7 RID: 6823
		void OnCancelGame();

		// Token: 0x06001AA8 RID: 6824
		void OnMapLoading();

		// Token: 0x06001AA9 RID: 6825
		void OnMatchStarting();
	}
}
