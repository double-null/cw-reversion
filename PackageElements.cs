using System;
using System.Collections.Generic;

// Token: 0x020002A1 RID: 673
[Serializable]
internal class PackageElements : Convertible
{
	// Token: 0x060012F0 RID: 4848 RVA: 0x000CD52C File Offset: 0x000CB72C
	public PackageElements()
	{
	}

	// Token: 0x060012F1 RID: 4849 RVA: 0x000CD56C File Offset: 0x000CB76C
	public PackageElements(Dictionary<string, object> dict)
	{
		JSON.ReadWriteEnum<PackagesType>(dict, "type", ref this.type, false);
		if (this.type == PackagesType.twoSkills)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
		}
		else if (this.type == PackagesType.threeSkills)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
		}
		else if (this.type == PackagesType.fourSkills)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
		}
		else if (this.type == PackagesType.primaryOneSkill)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
		}
		else if (this.type == PackagesType.primaryTwoSkills)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Skills>(dict, "skills", ref this.skill, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
		}
		else if (this.type == PackagesType.twoPrimary)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
		}
		else if (this.type == PackagesType.threeSecondary)
		{
			JSON.ReadWrite(dict, "name", ref this.name, false);
			JSON.ReadWrite(dict, "description", ref this.description, false);
			JSON.ReadWrite(dict, "price", ref this.price, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
			JSON.ReadWriteEnum<Weapons>(dict, "weapon", ref this.weapon, false);
		}
	}

	// Token: 0x060012F2 RID: 4850 RVA: 0x000CD900 File Offset: 0x000CBB00
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "current", ref this.current, isWrite);
	}

	// Token: 0x060012F3 RID: 4851 RVA: 0x000CD914 File Offset: 0x000CBB14
	private void F()
	{
	}

	// Token: 0x040015E9 RID: 5609
	public WeaponBlockUnlocked set = WeaponBlockUnlocked.none;

	// Token: 0x040015EA RID: 5610
	public Weapons weapon = Weapons.none;

	// Token: 0x040015EB RID: 5611
	public Skills skill = Skills.none;

	// Token: 0x040015EC RID: 5612
	public PackagesType type;

	// Token: 0x040015ED RID: 5613
	public string name = string.Empty;

	// Token: 0x040015EE RID: 5614
	public string description = string.Empty;

	// Token: 0x040015EF RID: 5615
	public float price;

	// Token: 0x040015F0 RID: 5616
	public int current;
}
