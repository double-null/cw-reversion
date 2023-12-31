using System;
using System.Collections.Generic;

// Token: 0x0200024F RID: 591
internal class ClanSkillInfo : cwNetworkSerializable, Convertible
{
	// Token: 0x0600120E RID: 4622 RVA: 0x000C7684 File Offset: 0x000C5884
	public ClanSkillInfo()
	{
	}

	// Token: 0x0600120F RID: 4623 RVA: 0x000C76A4 File Offset: 0x000C58A4
	public ClanSkillInfo(Dictionary<string, object> dict, int index)
	{
		this._index = index;
		JSON.ReadWriteEnum<Cl_Skills>(dict, "type", ref this.Type, false);
		JSON.ReadWrite(dict, "CR", ref this.PriceCR, false);
		JSON.ReadWrite(dict, "GP", ref this.PriceGP, false);
		JSON.ReadWrite(dict, "class", ref this.Class, false);
		JSON.ReadWriteEnum<Cl_Skills>(dict, "requirements", ref this.Requirements, false);
		JSON.ReadWrite(dict, "rentPrice", ref this.RentPrice, false);
		JSON.ReadWrite(dict, "rentTime", ref this.RentTime, false);
		JSON.ReadWrite(dict, "isPremium", ref this.IsPremium, false);
	}

	// Token: 0x17000268 RID: 616
	// (get) Token: 0x06001210 RID: 4624 RVA: 0x000C7768 File Offset: 0x000C5968
	public string Name
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedName;
		}
	}

	// Token: 0x17000269 RID: 617
	// (get) Token: 0x06001211 RID: 4625 RVA: 0x000C7778 File Offset: 0x000C5978
	public string Function
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedFunction;
		}
	}

	// Token: 0x1700026A RID: 618
	// (get) Token: 0x06001212 RID: 4626 RVA: 0x000C7788 File Offset: 0x000C5988
	public string Bonus
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedbonus;
		}
	}

	// Token: 0x06001213 RID: 4627 RVA: 0x000C7798 File Offset: 0x000C5998
	private void RecacheStringsIfNeeded()
	{
		if (!string.IsNullOrEmpty(this._cachedName) && !string.IsNullOrEmpty(this._cachedFunction) && !string.IsNullOrEmpty(this._cachedbonus) && this._cachedLanguage == Language.CurrentLanguage)
		{
			return;
		}
		this._cachedName = (Globals.I.ClanSkills[this._index]["name"] as string);
		this._cachedFunction = (Globals.I.ClanSkills[this._index]["function"] as string);
		this._cachedbonus = (Globals.I.ClanSkills[this._index]["bonus"] as string);
		this._cachedLanguage = Language.CurrentLanguage;
	}

	// Token: 0x06001214 RID: 4628 RVA: 0x000C7864 File Offset: 0x000C5A64
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "unlocked", ref this.isUnlocked, isWrite);
		JSON.ReadWrite(dict, "rentEnd", ref this.RentEnd, isWrite);
	}

	// Token: 0x06001215 RID: 4629 RVA: 0x000C7898 File Offset: 0x000C5A98
	public void Serialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.isUnlocked);
	}

	// Token: 0x06001216 RID: 4630 RVA: 0x000C78A8 File Offset: 0x000C5AA8
	public void Deserialize(eNetworkStream stream)
	{
		stream.Serialize(ref this.isUnlocked);
	}

	// Token: 0x1700026B RID: 619
	// (get) Token: 0x06001217 RID: 4631 RVA: 0x000C78B8 File Offset: 0x000C5AB8
	// (set) Token: 0x06001218 RID: 4632 RVA: 0x000C78F4 File Offset: 0x000C5AF4
	public bool IsUnlocked
	{
		get
		{
			int num = this.RentEnd - HtmlLayer.serverUtc;
			if (this.RentEnd != 0 && num < 0)
			{
				this.isUnlocked = false;
			}
			return this.isUnlocked;
		}
		set
		{
			this.isUnlocked = value;
		}
	}

	// Token: 0x040011B0 RID: 4528
	public Cl_Skills Type;

	// Token: 0x040011B1 RID: 4529
	private int _index;

	// Token: 0x040011B2 RID: 4530
	private ELanguage _cachedLanguage;

	// Token: 0x040011B3 RID: 4531
	private string _cachedName;

	// Token: 0x040011B4 RID: 4532
	private string _cachedFunction;

	// Token: 0x040011B5 RID: 4533
	private string _cachedbonus;

	// Token: 0x040011B6 RID: 4534
	public int Class;

	// Token: 0x040011B7 RID: 4535
	public Int PriceCR = new Int(0);

	// Token: 0x040011B8 RID: 4536
	public Int PriceGP = new Int(0);

	// Token: 0x040011B9 RID: 4537
	public Cl_Skills[] Requirements;

	// Token: 0x040011BA RID: 4538
	public int[] RentPrice;

	// Token: 0x040011BB RID: 4539
	public int[] RentTime;

	// Token: 0x040011BC RID: 4540
	public bool IsPremium;

	// Token: 0x040011BD RID: 4541
	private bool isUnlocked;

	// Token: 0x040011BE RID: 4542
	public int RentEnd;
}
