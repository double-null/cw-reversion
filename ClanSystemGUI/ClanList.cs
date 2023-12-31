using System;

namespace ClanSystemGUI
{
	// Token: 0x02000122 RID: 290
	internal class ClanList : ScrollList
	{
		// Token: 0x060007CC RID: 1996 RVA: 0x00047DC0 File Offset: 0x00045FC0
		public void WithdrawAllRequests()
		{
			foreach (IScrollListItem scrollListItem in this.ItemList)
			{
				ClanInfoBar clanInfoBar = scrollListItem as ClanInfoBar;
				if (clanInfoBar == null)
				{
					break;
				}
				clanInfoBar.WithdrawRequest();
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00047E38 File Offset: 0x00046038
		public override void OnGUI()
		{
			base.OnGUI();
		}
	}
}
