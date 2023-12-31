using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000210 RID: 528
internal class EditClanMessage : DatabaseEvent
{
	// Token: 0x060010D0 RID: 4304 RVA: 0x000BC9A0 File Offset: 0x000BABA0
	public override void Initialize(params object[] args)
	{
		string text = Convert.ToString(Crypt.ResolveVariable(args, string.Empty, 0));
		string actions = string.Concat(new object[]
		{
			"clans.php?action=post_message&clan_id=",
			Main.UserInfo.clanID,
			"&text=",
			text
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010D1 RID: 4305 RVA: 0x000BCA1C File Offset: 0x000BAC1C
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			Debug.Log("Bad response. Null or empty");
			return;
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		object value;
		if (!dictionary.TryGetValue("result", out value))
		{
			this.FailedAction();
			Debug.Log("TryGetValue fault");
			return;
		}
		int num = Convert.ToInt32(value);
		if (num != 0)
		{
			if (num != 1001)
			{
				if (num != 1009)
				{
					if (num != 1018)
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1100, PopupState.information, false, true, string.Empty, string.Empty));
						this.FailedAction();
					}
					else
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1018, PopupState.information, false, true, string.Empty, string.Empty));
						this.FailedAction();
					}
				}
				else
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1009, PopupState.information, false, true, string.Empty, string.Empty));
					this.FailedAction();
				}
			}
			else
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1001, PopupState.information, false, true, string.Empty, string.Empty));
				this.FailedAction();
			}
		}
		else
		{
			this.SuccessAction();
		}
	}

	// Token: 0x060010D2 RID: 4306 RVA: 0x000BCB98 File Offset: 0x000BAD98
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
