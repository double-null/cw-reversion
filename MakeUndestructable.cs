using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000225 RID: 549
[AddComponentMenu("Scripts/Game/Events/MakeUndestructable")]
internal class MakeUndestructable : DatabaseEvent
{
	// Token: 0x06001140 RID: 4416 RVA: 0x000C1018 File Offset: 0x000BF218
	public override void Initialize(params object[] args)
	{
		int num = (int)Crypt.ResolveVariable(args, 0, 0);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.Repair, Language.Modification, Language.ModificationProcessing, PopupState.progress, false, true, string.Empty, string.Empty));
		global::Console.print("RepairWeapon", Color.grey);
		this.repairCached = num;
		HtmlLayer.Request("?action=make_undestructible&weapon_index=" + num.ToString(), new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001141 RID: 4417 RVA: 0x000C10B0 File Offset: 0x000BF2B0
	protected override void OnResponse(string text)
	{
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
		Main.UserInfo.weaponsStates[this.repairCached].repair_info = -77f;
		Main.UserInfo.GP = (int)dictionary["new_gp"];
	}

	// Token: 0x06001142 RID: 4418 RVA: 0x000C11C4 File Offset: 0x000BF3C4
	protected override void OnFail(Exception e)
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Repair, Language.Repair, Language.RepairFailure, PopupState.information, true, false, string.Empty, string.Empty));
		global::Console.print("Repair failed", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x040010F8 RID: 4344
	protected int repairCached;
}
