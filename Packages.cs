using System;
using System.Collections.Generic;

// Token: 0x020002A3 RID: 675
[Serializable]
internal class Packages : Convertible
{
	// Token: 0x060012F8 RID: 4856 RVA: 0x000CDBD0 File Offset: 0x000CBDD0
	public Packages()
	{
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x000CDBE0 File Offset: 0x000CBDE0
	public Packages(Dictionary<string, object> dict, int ID)
	{
		this.ID = ID;
		this.gp_price = 0;
		this.cr_price = 0;
		JSON.ReadWrite(dict, "price_gp", ref this.gp_price, false);
		JSON.ReadWrite(dict, "price_cr", ref this.cr_price, false);
		JSON.ReadWrite(dict, "set", ref this.set, false);
		this.items = new PackageItem[((Dictionary<string, object>[])dict["items"]).Length];
		for (int i = 0; i < this.items.Length; i++)
		{
			this.items[i] = new PackageItem(((Dictionary<string, object>[])dict["items"])[i]);
		}
	}

	// Token: 0x170002AE RID: 686
	// (get) Token: 0x060012FA RID: 4858 RVA: 0x000CDC9C File Offset: 0x000CBE9C
	public string name
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedName;
		}
	}

	// Token: 0x170002AF RID: 687
	// (get) Token: 0x060012FB RID: 4859 RVA: 0x000CDCAC File Offset: 0x000CBEAC
	public string description
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedDescription;
		}
	}

	// Token: 0x060012FC RID: 4860 RVA: 0x000CDCBC File Offset: 0x000CBEBC
	private void RecacheStringsIfNeeded()
	{
		if (!string.IsNullOrEmpty(this._cachedName) && this._cachedDescription != null && this._cachedlanguage == Language.CurrentLanguage)
		{
			return;
		}
		this._cachedName = (Globals.I.packages[this.ID]["name"] as string);
		object obj;
		if (Globals.I.packages[this.ID].TryGetValue("description", out obj))
		{
			this._cachedDescription = (obj as string);
		}
		else
		{
			this._cachedDescription = string.Empty;
		}
		this._cachedlanguage = Language.CurrentLanguage;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x000CDD64 File Offset: 0x000CBF64
	public void Unlock()
	{
		for (int i = 0; i < this.items.Length; i++)
		{
			this.items[i].Unlock();
		}
	}

	// Token: 0x060012FE RID: 4862 RVA: 0x000CDD98 File Offset: 0x000CBF98
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
	}

	// Token: 0x040015F4 RID: 5620
	private string _cachedName;

	// Token: 0x040015F5 RID: 5621
	private string _cachedDescription;

	// Token: 0x040015F6 RID: 5622
	public int gp_price;

	// Token: 0x040015F7 RID: 5623
	public int cr_price;

	// Token: 0x040015F8 RID: 5624
	public int set;

	// Token: 0x040015F9 RID: 5625
	public int ID = -1;

	// Token: 0x040015FA RID: 5626
	public PackageItem[] items;

	// Token: 0x040015FB RID: 5627
	private ELanguage _cachedlanguage;
}
