using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x020000FF RID: 255
	internal interface IClanTab
	{
		// Token: 0x060006C8 RID: 1736
		void SetRect(Rect rect);

		// Token: 0x060006C9 RID: 1737
		void OnStart();

		// Token: 0x060006CA RID: 1738
		void OnGUI();

		// Token: 0x060006CB RID: 1739
		void OnDrawButton();

		// Token: 0x060006CC RID: 1740
		void OnUpdate();
	}
}
