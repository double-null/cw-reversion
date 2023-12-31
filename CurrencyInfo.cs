using System;
using System.Collections.Generic;

// Token: 0x02000256 RID: 598
internal class CurrencyInfo : Convertible
{
	// Token: 0x0600125A RID: 4698 RVA: 0x000C9244 File Offset: 0x000C7444
	public CurrencyInfo()
	{
		this.PricesGp[0] = 1f;
		this.PricesGp[1] = 2f;
		this.PricesGp[2] = 5f;
		this.PricesGp[3] = 10f;
		this.PricesGp[4] = 20f;
		this.PricesCr[0] = 1f;
		this.PricesCr[1] = 2f;
		this.PricesCr[2] = 5f;
		this.PricesCr[3] = 10f;
		this.AmounGp[0] = 3f;
		this.AmounGp[1] = 8f;
		this.AmounGp[2] = 20f;
		this.AmounGp[3] = 55f;
		this.AmounGp[4] = 135f;
		this.AmounCr[0] = 12f;
		this.AmounCr[1] = 30f;
		this.AmounCr[2] = 90f;
		this.AmounCr[3] = 240f;
		this.CurrencyName = "USD";
		this.CurrencySymbol = "$";
		this.PrefixSymbol = true;
		this.MultiplierGp = 10;
		this.MultiplierCr = 1000;
	}

	// Token: 0x0600125B RID: 4699 RVA: 0x000C93B8 File Offset: 0x000C75B8
	public void ParsePrices(Dictionary<string, object> dict)
	{
		int num = 0;
		this.PricesGp = new float[((Dictionary<string, object>)dict["gp"]).Count];
		foreach (KeyValuePair<string, object> keyValuePair in ((Dictionary<string, object>)dict["gp"]))
		{
			float.TryParse(keyValuePair.Value.ToString(), out this.PricesGp[num]);
			float.TryParse(keyValuePair.Key, out this.AmounGp[num]);
			num++;
		}
		num = 0;
		this.PricesCr = new float[((Dictionary<string, object>)dict["cr"]).Count];
		foreach (KeyValuePair<string, object> keyValuePair2 in ((Dictionary<string, object>)dict["cr"]))
		{
			float.TryParse(keyValuePair2.Value.ToString(), out this.PricesCr[num]);
			float.TryParse(keyValuePair2.Key, out this.AmounCr[num]);
			num++;
		}
	}

	// Token: 0x0600125C RID: 4700 RVA: 0x000C9538 File Offset: 0x000C7738
	public void ParseMultipliers(Dictionary<string, object> dict)
	{
		this.MultiplierGp = (int)dict["gp"];
		this.MultiplierCr = (int)dict["cr"];
	}

	// Token: 0x0600125D RID: 4701 RVA: 0x000C9574 File Offset: 0x000C7774
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "name", ref this.CurrencyName, isWrite);
		JSON.ReadWrite(dict, "symbol", ref this.CurrencySymbol, isWrite);
		JSON.ReadWrite(dict, "pre", ref this.PrefixSymbol, isWrite);
	}

	// Token: 0x040011F9 RID: 4601
	public float[] PricesGp = new float[5];

	// Token: 0x040011FA RID: 4602
	public float[] PricesCr = new float[4];

	// Token: 0x040011FB RID: 4603
	public float[] AmounGp = new float[5];

	// Token: 0x040011FC RID: 4604
	public float[] AmounCr = new float[5];

	// Token: 0x040011FD RID: 4605
	public string CurrencyName = string.Empty;

	// Token: 0x040011FE RID: 4606
	public string CurrencySymbol = string.Empty;

	// Token: 0x040011FF RID: 4607
	public bool PrefixSymbol;

	// Token: 0x04001200 RID: 4608
	public int MultiplierGp;

	// Token: 0x04001201 RID: 4609
	public int MultiplierCr;
}
