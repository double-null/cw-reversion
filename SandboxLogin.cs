using System;
using UnityEngine;

// Token: 0x02000231 RID: 561
[AddComponentMenu("Scripts/Game/Events/SandboxLogin")]
internal class SandboxLogin : Login
{
	// Token: 0x0600116E RID: 4462 RVA: 0x000C24B4 File Offset: 0x000C06B4
	public override void Initialize(params object[] args)
	{
		string str = (string)Crypt.ResolveVariable(args, string.Empty, 0);
		if (!LoadProfile.splashScreenOn)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Login, Language.CWMainLoading, Language.CWMainLoginDesc, PopupState.progress, false, false, string.Empty, string.Empty));
		}
		global::Console.print("Login", Color.grey);
		this.OnResponse(str);
	}
}
