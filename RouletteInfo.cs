using System;
using System.Collections.Generic;

// Token: 0x020002AE RID: 686
internal class RouletteInfo : Convertible
{
	// Token: 0x06001352 RID: 4946 RVA: 0x000CFFC4 File Offset: 0x000CE1C4
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "enabled", ref this.Enabled, isWrite);
		JSON.ReadWrite(dict, "attemptCost", ref this.AttemptCost, isWrite);
		Dictionary<string, object> dictionary = (Dictionary<string, object>)dict["rent"];
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dict["bonus"];
		this.SkillRentTerm = (int)dictionary["skill"];
		this.WeaponRentTerm = (int)dictionary["weapon"];
		this.BDCurrency = (int)dict["blackDivisionCurrency"];
		object obj;
		if (dictionary2.TryGetValue("cr", out obj))
		{
			this.InitBonusArray(ref this.amountCR, obj);
		}
		if (dictionary2.TryGetValue("gp", out obj))
		{
			this.InitBonusArray(ref this.amountGP, obj);
		}
		if (dictionary2.TryGetValue("mp", out obj))
		{
			this.InitBonusArray(ref this.amountMP, obj);
		}
		else
		{
			this.amountMP = new int[2];
		}
		if (dictionary2.TryGetValue("sp", out obj))
		{
			this.InitBonus(ref this.amountSP, obj);
		}
		if (dictionary2.TryGetValue("blackDivision", out obj))
		{
			this.InitBonus(ref this.amountBD, obj);
		}
	}

	// Token: 0x06001353 RID: 4947 RVA: 0x000D0108 File Offset: 0x000CE308
	private void InitBonus(ref int bonus, object obj)
	{
		object[] array = obj as object[];
		if (array != null)
		{
			for (int i = 0; i < array.Length; i++)
			{
				bonus = (int)(array[i] as Dictionary<string, object>)["amount"];
			}
		}
	}

	// Token: 0x06001354 RID: 4948 RVA: 0x000D0150 File Offset: 0x000CE350
	private void InitBonusArray(ref int[] arr, object obj)
	{
		object[] array = obj as object[];
		if (array != null)
		{
			arr = new int[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				arr[i] = (int)(array[i] as Dictionary<string, object>)["amount"];
			}
		}
	}

	// Token: 0x04001654 RID: 5716
	public bool Enabled;

	// Token: 0x04001655 RID: 5717
	public int AttemptCost;

	// Token: 0x04001656 RID: 5718
	public int amountSP;

	// Token: 0x04001657 RID: 5719
	public int[] amountGP;

	// Token: 0x04001658 RID: 5720
	public int[] amountCR;

	// Token: 0x04001659 RID: 5721
	public int[] amountMP;

	// Token: 0x0400165A RID: 5722
	public int BDCurrency;

	// Token: 0x0400165B RID: 5723
	public int amountBD;

	// Token: 0x0400165C RID: 5724
	public int WeaponRentTerm;

	// Token: 0x0400165D RID: 5725
	public int SkillRentTerm;

	// Token: 0x0400165E RID: 5726
	public Dictionary<string, object> d;
}
