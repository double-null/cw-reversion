using System;

namespace LeagueGUI
{
	// Token: 0x02000312 RID: 786
	internal interface IFrame
	{
		// Token: 0x06001ACA RID: 6858
		void OnStart();

		// Token: 0x06001ACB RID: 6859
		void OnGUI();

		// Token: 0x06001ACC RID: 6860
		void OnUpdate();

		// Token: 0x06001ACD RID: 6861
		void Clear();
	}
}
