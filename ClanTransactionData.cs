using System;
using System.Collections.Generic;

// Token: 0x02000251 RID: 593
internal class ClanTransactionData : Convertible
{
	// Token: 0x0600121F RID: 4639 RVA: 0x000C7A7C File Offset: 0x000C5C7C
	public ClanTransactionData()
	{
	}

	// Token: 0x06001220 RID: 4640 RVA: 0x000C7ABC File Offset: 0x000C5CBC
	public ClanTransactionData(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x06001221 RID: 4641 RVA: 0x000C7B04 File Offset: 0x000C5D04
	private int GetPlayerLevel(float exp)
	{
		for (int i = 0; i < Globals.I.expTable.Length; i++)
		{
			if (exp < (float)Globals.I.expTable[i])
			{
				return i - 1;
			}
		}
		return Globals.I.expTable.Length - 1;
	}

	// Token: 0x06001222 RID: 4642 RVA: 0x000C7B54 File Offset: 0x000C5D54
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "amount", ref this.Amount, isWrite);
		JSON.ReadWrite(dict, "currency", ref this.Currency, isWrite);
		JSON.ReadWrite(dict, "date", ref this.Date, isWrite);
		JSON.ReadWrite(dict, "user_name", ref this.Nickname, isWrite);
		JSON.ReadWrite(dict, "first_name", ref this.Firstname, isWrite);
		JSON.ReadWrite(dict, "last_name", ref this.Lastname, isWrite);
		JSON.ReadWrite(dict, "curr_xp", ref this.exp, isWrite);
		if (!isWrite)
		{
			this.Level = this.GetPlayerLevel((float)this.exp);
		}
	}

	// Token: 0x040011C6 RID: 4550
	public int Amount;

	// Token: 0x040011C7 RID: 4551
	public int Currency;

	// Token: 0x040011C8 RID: 4552
	public string Date = string.Empty;

	// Token: 0x040011C9 RID: 4553
	public string Nickname = string.Empty;

	// Token: 0x040011CA RID: 4554
	public string Firstname = string.Empty;

	// Token: 0x040011CB RID: 4555
	public string Lastname = string.Empty;

	// Token: 0x040011CC RID: 4556
	public int Level;

	// Token: 0x040011CD RID: 4557
	private int exp;
}
