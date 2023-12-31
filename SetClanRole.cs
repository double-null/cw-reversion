using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200020D RID: 525
internal class SetClanRole : DatabaseEvent
{
	// Token: 0x060010C4 RID: 4292 RVA: 0x000BC07C File Offset: 0x000BA27C
	public override void Initialize(params object[] args)
	{
		string str = Convert.ToString(Crypt.ResolveVariable(args, string.Empty, 0));
		string str2 = Convert.ToString(Crypt.ResolveVariable(args, string.Empty, 1));
		int num = Convert.ToInt32(Crypt.ResolveVariable(args, string.Empty, 2));
		bool flag = Convert.ToBoolean(Crypt.ResolveVariable(args, string.Empty, 3));
		string actions = string.Empty;
		if (flag)
		{
			if (num == 4)
			{
				actions = "clans.php?action=dismiss_alt_leader&user_id=" + str2 + "&clan_id=" + str;
			}
			if (num == 2)
			{
				actions = "clans.php?action=dismiss_officer&user_id=" + str2 + "&clan_id=" + str;
			}
		}
		else
		{
			if (num == 8)
			{
				actions = "clans.php?action=change_leader&user_id=" + str2 + "&clan_id=" + str;
			}
			if (num == 4)
			{
				actions = "clans.php?action=assign_alt_leader&user_id=" + str2 + "&clan_id=" + str;
			}
			if (num == 2)
			{
				actions = "clans.php?action=assign_officer&user_id=" + str2 + "&clan_id=" + str;
			}
		}
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060010C5 RID: 4293 RVA: 0x000BC190 File Offset: 0x000BA390
	protected override void OnResponse(string text)
	{
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			this.OnFail(ex);
			return;
		}
		object value;
		dictionary.TryGetValue("result", out value);
		int num = Convert.ToInt32(value);
		switch (num)
		{
		case 1006:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1006, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1007:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1007, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1008:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1008, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1009:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1009, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1010:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1010, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1011:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1011, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1012:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1012, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1013:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1013, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1014:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1014, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1015:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1015, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1016:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1016, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		case 1017:
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1017, PopupState.information, false, true, string.Empty, string.Empty));
			this.FailedAction();
			break;
		default:
			if (num != 0)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.ClansError1100, PopupState.information, false, true, string.Empty, string.Empty));
				this.FailedAction();
			}
			else
			{
				this.SuccessAction();
			}
			break;
		}
	}

	// Token: 0x060010C6 RID: 4294 RVA: 0x000BC52C File Offset: 0x000BA72C
	protected override void OnFail(Exception e)
	{
		base.OnFail(e);
	}
}
