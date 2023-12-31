using System;

namespace ClanSystemGUI
{
	// Token: 0x02000123 RID: 291
	internal class MemberList : ScrollList
	{
		// Token: 0x060007CE RID: 1998 RVA: 0x00047E40 File Offset: 0x00046040
		public MemberList()
		{
			for (int i = 0; i < 30; i++)
			{
				this.ItemList.Add(new PlayerInfoBar());
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00047E78 File Offset: 0x00046078
		public override void OnGUI()
		{
			base.OnGUI();
		}
	}
}
