using System;
using System.Collections.Generic;

// Token: 0x02000229 RID: 553
[Serializable]
internal class PromoBonus : Convertible
{
	// Token: 0x06001151 RID: 4433 RVA: 0x000C1530 File Offset: 0x000BF730
	public PromoBonus(PromoBonusType type, int amount = 0, int weapindex = -1, int skillIndex = -1, int rent_days = 0)
	{
		this.type = type;
		this.amount = amount;
		this.weapindex = weapindex;
		this.SkillIndex = skillIndex;
		this.rent_days = rent_days;
	}

	// Token: 0x06001152 RID: 4434 RVA: 0x000C156C File Offset: 0x000BF76C
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
	}

	// Token: 0x040010FD RID: 4349
	public PromoBonusType type;

	// Token: 0x040010FE RID: 4350
	public int amount;

	// Token: 0x040010FF RID: 4351
	public int weapindex = -1;

	// Token: 0x04001100 RID: 4352
	public int SkillIndex = -1;

	// Token: 0x04001101 RID: 4353
	public int rent_days;
}
