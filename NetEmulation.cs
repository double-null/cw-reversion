using System;
using cygwin_x32;
using UnityEngine;

// Token: 0x02000226 RID: 550
internal class NetEmulation : MonoBehaviour
{
	// Token: 0x06001145 RID: 4421 RVA: 0x000C121C File Offset: 0x000BF41C
	private void Awake()
	{
		CygWin32L.Instance.OnJsonDataFalse += delegate()
		{
			HtmlLayer.Cookies.Clear();
			Main.AddDatabaseRequest<InitUser>(new object[]
			{
				true
			});
		};
	}

	// Token: 0x06001146 RID: 4422 RVA: 0x000C1248 File Offset: 0x000BF448
	private void Update()
	{
		if (!NetEmulation.Enabled)
		{
			return;
		}
		CygWin32L.Instance.EmuHost = CVars.n_protocol + WWWUtil.databaseWWW;
		CygWin32L.Instance.SystemInfo = HtmlLayer.Cookies;
		CygWin32L.Instance.UserId = Main.UserInfo.userID;
		if (Main.IsGameLoaded && !this._isJoinedGame)
		{
			this._isJoinedGame = !this._isJoinedGame;
			CygWin32L.Instance.EmulateUniCon(Main.HostInfo.OnlineInfo());
		}
		if (!Main.IsGameLoaded && this._isJoinedGame)
		{
			this._isJoinedGame = !this._isJoinedGame;
			CygWin32L.Instance.EmulateUniCon(null);
		}
	}

	// Token: 0x040010F9 RID: 4345
	public static bool Enabled;

	// Token: 0x040010FA RID: 4346
	private bool _isJoinedGame;
}
