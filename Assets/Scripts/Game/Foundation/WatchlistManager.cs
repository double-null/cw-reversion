using System;

namespace Assets.Scripts.Game.Foundation
{
	// Token: 0x020002DB RID: 731
	public static class WatchlistManager
	{
		// Token: 0x0600145D RID: 5213 RVA: 0x000D80F0 File Offset: 0x000D62F0
		public static void AddRemovePlayer(object[] args)
		{
			bool flag = Main.UserInfo.WatchlistUsersId.Contains((int)args[0]);
			string title = (!flag) ? Language.AddToFavorites : Language.RemoveFromFavorites;
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Favorite, title, (!flag) ? string.Empty : "remove", delegate()
			{
			}, PopupState.favorite, false, true, args));
		}

		// Token: 0x0400190A RID: 6410
		public static bool AddOnce;

		// Token: 0x0400190B RID: 6411
		public static bool FirstAddOnce;
	}
}
