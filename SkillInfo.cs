using System;
using System.Collections.Generic;
using cygwin_x32.ObscuredTypes;

// Token: 0x020002B0 RID: 688
[Serializable]
internal class SkillInfo : cwNetworkSerializable, Convertible
{
	// Token: 0x0600135C RID: 4956 RVA: 0x000D0570 File Offset: 0x000CE770
	public SkillInfo()
	{
	}

	// Token: 0x0600135D RID: 4957 RVA: 0x000D05CC File Offset: 0x000CE7CC
	public SkillInfo(Dictionary<string, object> dict, int index)
	{
		this._index = index;
		JSON.ReadWriteEnum<Skills>(dict, "type", ref this.type, false);
		JSON.ReadWriteEnum<WeaponSpecific>(dict, "class", ref this.classType, false);
		JSON.ReadWrite(dict, "CR", ref this.CR, false);
		JSON.ReadWrite(dict, "GP", ref this.GP, false);
		JSON.ReadWrite(dict, "SP", ref this.SP, false);
		JSON.ReadWrite(dict, "BG", ref this.BG, false);
		JSON.ReadWriteEnum<Skills>(dict, "requirements", ref this.requirements, false);
		JSON.ReadWrite(dict, "isPremium", ref this.isPremium, false);
		JSON.ReadWrite(dict, "rentPrice", ref this.rentPrice, false);
		JSON.ReadWrite(dict, "rentTime", ref this.rentTime, false);
	}

	// Token: 0x170002BF RID: 703
	// (get) Token: 0x0600135E RID: 4958 RVA: 0x000D06E4 File Offset: 0x000CE8E4
	public string name
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedName;
		}
	}

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x0600135F RID: 4959 RVA: 0x000D06F4 File Offset: 0x000CE8F4
	public string function
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedFunction;
		}
	}

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06001360 RID: 4960 RVA: 0x000D0704 File Offset: 0x000CE904
	public string bonus
	{
		get
		{
			this.RecacheStringsIfNeeded();
			return this._cachedbonus;
		}
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x000D0714 File Offset: 0x000CE914
	private void RecacheStringsIfNeeded()
	{
		if (!string.IsNullOrEmpty(this._cachedName) && !string.IsNullOrEmpty(this._cachedFunction) && !string.IsNullOrEmpty(this._cachedbonus) && this._cachedLanguage == Language.CurrentLanguage)
		{
			return;
		}
		this._cachedName = (Globals.I.skills[this._index]["name"] as string);
		this._cachedFunction = (Globals.I.skills[this._index]["function"] as string);
		this._cachedbonus = (Globals.I.skills[this._index]["bonus"] as string);
		this._cachedLanguage = Language.CurrentLanguage;
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000D07E0 File Offset: 0x000CE9E0
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		bool value = this._encryptedUnlocked;
		JSON.ReadWrite(dict, "unlocked", ref value, isWrite);
		JSON.ReadWrite(dict, "rentEnd", ref this.rentEnd, isWrite);
		this._encryptedUnlocked = value;
	}

	// Token: 0x170002C2 RID: 706
	// (get) Token: 0x06001363 RID: 4963 RVA: 0x000D0828 File Offset: 0x000CEA28
	public bool rentable
	{
		get
		{
			return this.rentEnd != 0;
		}
	}

	// Token: 0x170002C3 RID: 707
	// (get) Token: 0x06001364 RID: 4964 RVA: 0x000D0838 File Offset: 0x000CEA38
	// (set) Token: 0x06001365 RID: 4965 RVA: 0x000D087C File Offset: 0x000CEA7C
	public bool Unlocked
	{
		get
		{
			int num = this.rentEnd - HtmlLayer.serverUtc;
			if (this.rentEnd != 0 && num < 0)
			{
				this._encryptedUnlocked = false;
			}
			return this._encryptedUnlocked;
		}
		set
		{
			this._encryptedUnlocked = value;
		}
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000D088C File Offset: 0x000CEA8C
	public void Serialize(eNetworkStream stream)
	{
		bool flag = this._encryptedUnlocked;
		stream.Serialize(ref flag);
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x000D08B0 File Offset: 0x000CEAB0
	public void Deserialize(eNetworkStream stream)
	{
		bool value = this._encryptedUnlocked;
		stream.Serialize(ref value);
		this._encryptedUnlocked = value;
	}

	// Token: 0x04001673 RID: 5747
	private int _index;

	// Token: 0x04001674 RID: 5748
	private ELanguage _cachedLanguage;

	// Token: 0x04001675 RID: 5749
	private string _cachedName;

	// Token: 0x04001676 RID: 5750
	private string _cachedFunction;

	// Token: 0x04001677 RID: 5751
	private string _cachedbonus;

	// Token: 0x04001678 RID: 5752
	public Int CR = new Int(0);

	// Token: 0x04001679 RID: 5753
	public Int GP = new Int(0);

	// Token: 0x0400167A RID: 5754
	public Int SP = new Int(0);

	// Token: 0x0400167B RID: 5755
	public Int BG = new Int(0);

	// Token: 0x0400167C RID: 5756
	public WeaponSpecific classType = WeaponSpecific.none;

	// Token: 0x0400167D RID: 5757
	public Skills type;

	// Token: 0x0400167E RID: 5758
	public Skills[] requirements;

	// Token: 0x0400167F RID: 5759
	public bool isPremium;

	// Token: 0x04001680 RID: 5760
	public int[] rentPrice;

	// Token: 0x04001681 RID: 5761
	public int[] rentTime;

	// Token: 0x04001682 RID: 5762
	private ObscuredBool _encryptedUnlocked = false;

	// Token: 0x04001683 RID: 5763
	public int rentEnd;
}
