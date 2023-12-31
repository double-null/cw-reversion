using System;
using System.Collections.Generic;

// Token: 0x02000239 RID: 569
[Serializable]
internal class Transaction : Convertible
{
	// Token: 0x06001190 RID: 4496 RVA: 0x000C3398 File Offset: 0x000C1598
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "currency", ref this.currency, isWrite);
		JSON.ReadWrite(dict, "amount", ref this.amount, isWrite);
		JSON.ReadWrite(dict, "comment", ref this.comment, isWrite);
		JSON.ReadWrite(dict, "date", ref this.date, isWrite);
	}

	// Token: 0x04001125 RID: 4389
	public int currency;

	// Token: 0x04001126 RID: 4390
	public int amount;

	// Token: 0x04001127 RID: 4391
	public string date;

	// Token: 0x04001128 RID: 4392
	public string comment;
}
