using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000238 RID: 568
[AddComponentMenu("Scripts/Game/Events/TakeOwnership")]
internal class TakeOwnership : LoadProfile
{
	// Token: 0x0600118D RID: 4493 RVA: 0x000C3204 File Offset: 0x000C1404
	public override void Initialize(params object[] args)
	{
		int num = (int)args[0];
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.CWMainLoad, Language.CWMainLoadDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		global::Console.print("Load", Color.grey);
		foreach (WeaponInfo weaponInfo in Main.UserInfo.weaponsStates)
		{
			PoolManager.Despawn(weaponInfo.CurrentWeapon.gameObject);
		}
		Main.UserInfo = new UserInfo(true)
		{
			userID = num
		};
		HtmlLayer.Request("?action=own&user_id=" + num, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x000C32DC File Offset: 0x000C14DC
	protected override void OnResponse(string text, string url)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.MainBlocker, Language.CWMainLoading, Language.CWMainGlobalInfoLoadingFinished, PopupState.progress, false, false, string.Empty, string.Empty));
		Dictionary<string, object> dict;
		try
		{
			dict = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error\n" + text));
			return;
		}
		Main.UserInfo.Read(dict, true);
	}
}
