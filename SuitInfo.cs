using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002CB RID: 715
[Serializable]
internal class SuitInfo : Convertible
{
	// Token: 0x0600138A RID: 5002 RVA: 0x000D31A8 File Offset: 0x000D13A8
	public SuitInfo()
	{
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000D31F8 File Offset: 0x000D13F8
	public SuitInfo(bool forceInit)
	{
		if (!forceInit)
		{
			return;
		}
		this._wtaskStates = new bool[Globals.I.weapons.Length];
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x000D3268 File Offset: 0x000D1468
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "unlocked", ref this.Unlocked, isWrite);
		JSON.ReadWrite(dict, "suitName", ref this.SuitName, isWrite);
		JSON.ReadWrite(dict, "chosenPistol", ref this._chosenSecondary, isWrite);
		JSON.ReadWrite(dict, "chosenGun", ref this._chosenPrimary, isWrite);
		if (this._chosenSecondary == -1)
		{
			this._chosenSecondary = 127;
		}
		if (this._chosenPrimary == -1)
		{
			this._chosenPrimary = 127;
		}
		ArrayUtility.AdjustArraySize<bool>(ref this._wtaskStates, Globals.I.weapons.Length);
		JSON.ReadWrite(dict, "WtaskStates", ref this._wtaskStates, isWrite);
	}

	// Token: 0x170002C8 RID: 712
	// (get) Token: 0x0600138D RID: 5005 RVA: 0x000D3324 File Offset: 0x000D1524
	// (set) Token: 0x0600138E RID: 5006 RVA: 0x000D3364 File Offset: 0x000D1564
	public int secondaryIndex
	{
		get
		{
			if (this._chosenSecondary == -1)
			{
				this._chosenSecondary = 127;
			}
			if (this._chosenSecondary != 127)
			{
				return this._chosenSecondary;
			}
			return 0;
		}
		set
		{
			if (value == -1)
			{
				value = 127;
			}
			this._chosenSecondary = value;
		}
	}

	// Token: 0x170002C9 RID: 713
	// (get) Token: 0x0600138F RID: 5007 RVA: 0x000D3380 File Offset: 0x000D1580
	// (set) Token: 0x06001390 RID: 5008 RVA: 0x000D33CC File Offset: 0x000D15CC
	public int primaryIndex
	{
		get
		{
			if (this._chosenPrimary == -1)
			{
				this._chosenPrimary = 127;
			}
			if (this._chosenPrimary != 127)
			{
				return this._chosenPrimary;
			}
			return 127;
		}
		set
		{
			if (value == -1)
			{
				value = 127;
			}
			this._chosenPrimary = value;
		}
	}

	// Token: 0x170002CA RID: 714
	// (get) Token: 0x06001391 RID: 5009 RVA: 0x000D33E8 File Offset: 0x000D15E8
	// (set) Token: 0x06001392 RID: 5010 RVA: 0x000D343C File Offset: 0x000D163C
	public bool secondaryMod
	{
		get
		{
			if (this._chosenSecondary == -1)
			{
				this._chosenSecondary = 127;
			}
			return this._chosenSecondary != 127 && this._wtaskStates[this._chosenSecondary];
		}
		set
		{
			if (this._chosenSecondary == -1)
			{
				return;
			}
			if (this._chosenSecondary != 127)
			{
				this._wtaskStates[this._chosenSecondary] = value;
			}
		}
	}

	// Token: 0x170002CB RID: 715
	// (get) Token: 0x06001393 RID: 5011 RVA: 0x000D3478 File Offset: 0x000D1678
	// (set) Token: 0x06001394 RID: 5012 RVA: 0x000D34CC File Offset: 0x000D16CC
	public bool primaryMod
	{
		get
		{
			if (this._chosenPrimary == -1)
			{
				this._chosenPrimary = 127;
			}
			return this._chosenPrimary != 127 && this._wtaskStates[this._chosenPrimary];
		}
		set
		{
			if (this._chosenPrimary == -1)
			{
				return;
			}
			if (this._chosenPrimary != 127)
			{
				this._wtaskStates[this._chosenPrimary] = value;
			}
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000D3508 File Offset: 0x000D1708
	public void SetWtask(int index, bool state)
	{
		if (this._wtaskStates.Length < index)
		{
			return;
		}
		this._wtaskStates[index] = state;
		if (Main.UserInfo.weaponsStates[index].CurrentWeapon.IsMod != state)
		{
			Main.UserInfo.weaponsStates[index].UpdateWeapon();
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x000D355C File Offset: 0x000D175C
	public bool GetWtask(int index)
	{
		return this._wtaskStates.Length > index && this._wtaskStates[index];
	}

	// Token: 0x04001837 RID: 6199
	private Int _chosenSecondary = 127;

	// Token: 0x04001838 RID: 6200
	private Int _chosenPrimary = 127;

	// Token: 0x04001839 RID: 6201
	private bool[] _wtaskStates;

	// Token: 0x0400183A RID: 6202
	public bool Unlocked;

	// Token: 0x0400183B RID: 6203
	public string SuitName = Language.SUITE;

	// Token: 0x0400183C RID: 6204
	public bool[] WtaskModeStates = new bool[6];

	// Token: 0x0400183D RID: 6205
	public Vector2 ScrollPos = Vector2.zero;
}
