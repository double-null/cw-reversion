using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200022C RID: 556
[AddComponentMenu("Scripts/Game/Events/RepairWeapon")]
internal class RepairWeapon : DatabaseEvent
{
	// Token: 0x0600115A RID: 4442 RVA: 0x000C1830 File Offset: 0x000BFA30
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		int num2 = (int)Crypt.ResolveVariable(args, 0, 1);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.Repair, Language.RepairProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("RepairWeapon", Color.grey);
		this.repairCached = num;
		HtmlLayer.Request("?action=repair&weapon_index=" + num.ToString() + "&amount=" + num2.ToString(), new RequestFinished(this.OnResponse), null, string.Empty, string.Empty);
	}

	// Token: 0x0600115B RID: 4443 RVA: 0x000C18DC File Offset: 0x000BFADC
	protected override void OnResponse(string text)
	{
		Debug.Log(text);
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			this.OnFail(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] != 0)
		{
			this.OnFail(new Exception("Data Server Error"));
		}
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.Repair, Language.EquipmentRepaired, PopupState.information, false, false, string.Empty, string.Empty));
		global::Console.print("Repair Finished", Color.green);
		Main.UserInfo.weaponsStates[this.repairCached].repair_info = Convert.ToSingle(dictionary["new_weapon_info"]);
		Main.UserInfo.CR = (int)dictionary["new_cr"];
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x000C1A00 File Offset: 0x000BFC00
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.Repair, Language.RepairFailure, PopupState.information, true, false, string.Empty, string.Empty));
		global::Console.print("Repair failed", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x04001119 RID: 4377
	protected int repairCached;
}
