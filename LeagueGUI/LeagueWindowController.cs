using System;
using System.Collections.Generic;

namespace LeagueGUI
{
	// Token: 0x0200030B RID: 779
	internal class LeagueWindowController
	{
		// Token: 0x06001A8C RID: 6796 RVA: 0x000F0A98 File Offset: 0x000EEC98
		public LeagueWindowController()
		{
			this.MainWindow = new MainWindow();
			this.ResultWindow = new ResultWindow();
			this.MatchmakeWindow = new MatchmakeWindow();
			this.currentWindow = this.MainWindow;
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001A8D RID: 6797 RVA: 0x000F0AD0 File Offset: 0x000EECD0
		public IWindow CurrentWindow
		{
			get
			{
				return this.currentWindow;
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000F0AD8 File Offset: 0x000EECD8
		public void DownloadRankPoints()
		{
			HtmlLayer.Request("adm.php?q=setting/getLeague", delegate(string text, string url)
			{
				Dictionary<string, object> dict = ArrayUtility.FromJSON(text, string.Empty);
				int[] ranks = null;
				JSON.ReadWrite(dict, "ranks", ref ranks, false);
				LeagueHelpers.Ranks = ranks;
			}, null, string.Empty, string.Empty);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000F0B18 File Offset: 0x000EED18
		public void OnStart()
		{
			this.MainWindow.OnStart();
			this.MatchmakeWindow.OnStart();
			this.ResultWindow.OnStart();
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000F0B3C File Offset: 0x000EED3C
		public void OnGUI()
		{
			if (this.currentWindow != null)
			{
				this.currentWindow.OnGUI();
			}
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000F0B54 File Offset: 0x000EED54
		public void OnUpdate()
		{
			if (this.currentWindow != null)
			{
				this.currentWindow.OnUpdate();
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000F0B6C File Offset: 0x000EED6C
		public void SetState(IWindow state)
		{
			this.currentWindow = state;
			state.OnStart();
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000F0B7C File Offset: 0x000EED7C
		public IWindow GetState()
		{
			return this.currentWindow;
		}

		// Token: 0x04001FAA RID: 8106
		private IWindow currentWindow;

		// Token: 0x04001FAB RID: 8107
		public IWindow MainWindow;

		// Token: 0x04001FAC RID: 8108
		public IWindow ResultWindow;

		// Token: 0x04001FAD RID: 8109
		public IWindow MatchmakeWindow;
	}
}
