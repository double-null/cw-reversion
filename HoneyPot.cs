using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200021E RID: 542
[AddComponentMenu("Scripts/Game/Events/HoneyPot")]
internal class HoneyPot : DatabaseEvent
{
	// Token: 0x06001109 RID: 4361 RVA: 0x000BDBF8 File Offset: 0x000BBDF8
	public override void Initialize(params object[] args)
	{
		if (!this.honeyPotRecorded)
		{
			string text = (string)args[0];
			if (args.Length > 1)
			{
				this.type = (ViolationType)((int)args[1]);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			string text2 = text.Trim();
			Main.UserInfo.Violation.Value |= (int)this.type;
			string data = Main.UserInfo.Violation.Value.ToString();
			HtmlLayer.SendCompressed("?action=honeypot&user_id=" + Main.UserInfo.userID, data, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail));
		}
	}

	// Token: 0x0600110A RID: 4362 RVA: 0x000BDCA8 File Offset: 0x000BBEA8
	protected override void OnResponse(string text)
	{
		Main.UserInfo.Violation.Value |= (int)this.type;
		MonoBehaviour.print(text);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.BANNED, string.Empty, string.Empty, PopupState.banned, false, false, string.Empty, string.Empty));
		this.honeyPotRecorded = true;
		Main.UserInfo.banned = 1;
	}

	// Token: 0x0600110B RID: 4363 RVA: 0x000BDD18 File Offset: 0x000BBF18
	protected override void OnFail(Exception e, string url)
	{
		Main.UserInfo.Violation.Value |= (int)this.type;
		Main.UserInfo.Violation.Value |= 4;
	}

	// Token: 0x040010E5 RID: 4325
	private bool honeyPotRecorded;

	// Token: 0x040010E6 RID: 4326
	private ViolationType type = ViolationType.None;
}
