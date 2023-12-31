using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020F RID: 527
internal class EditClanTagColor : DatabaseEvent
{
	// Token: 0x060010CC RID: 4300 RVA: 0x000BC770 File Offset: 0x000BA970
	public override void Initialize(params object[] args)
	{
		string text = Convert.ToString(Crypt.ResolveVariable(args, "0", 0));
		string text2 = Convert.ToString(Crypt.ResolveVariable(args, "0", 1));
		string text3 = Convert.ToString(Crypt.ResolveVariable(args, string.Empty, 2));
		string actions = string.Concat(new string[]
		{
			"clans.php?action=set_color&user_id=",
			text,
			"&clan_id=",
			text2,
			"&color=",
			text3
		});
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010CD RID: 4301 RVA: 0x000BC810 File Offset: 0x000BAA10
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
				if (num != 1010)
				{
					if (num != 1019)
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1100, PopupState.information, false, true, string.Empty, string.Empty));
						this.FailedAction();
					}
					else
					{
						EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1019, PopupState.information, false, true, string.Empty, string.Empty));
						this.FailedAction();
					}
				}
				else
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1010, PopupState.information, false, true, string.Empty, string.Empty));
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

	// Token: 0x060010CE RID: 4302 RVA: 0x000BC98C File Offset: 0x000BAB8C
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
