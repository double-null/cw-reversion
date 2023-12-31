using System;
using System.Collections.Generic;

// Token: 0x020002A2 RID: 674
[Serializable]
internal class PackageItem : Convertible
{
	// Token: 0x060012F4 RID: 4852 RVA: 0x000CD918 File Offset: 0x000CBB18
	public PackageItem()
	{
	}

	// Token: 0x060012F5 RID: 4853 RVA: 0x000CD928 File Offset: 0x000CBB28
	public PackageItem(Dictionary<string, object> dict)
	{
		JSON.ReadWriteEnum<PackageItemType>(dict, "type", ref this.type, false);
		JSON.ReadWrite(dict, "id", ref this.ID, false);
		JSON.ReadWrite(dict, "rentTime", ref this.rentTime, false);
	}

	// Token: 0x060012F6 RID: 4854 RVA: 0x000CD978 File Offset: 0x000CBB78
	public void Unlock()
	{
		if (this.type == PackageItemType.skill)
		{
			if (this.rentTime > 0)
			{
				if (Main.UserInfo.skillsInfos[this.ID].rentEnd - HtmlLayer.serverUtc < 0 && Main.UserInfo.skillsInfos[this.ID].Unlocked)
				{
					return;
				}
				if (Main.UserInfo.skillsInfos[this.ID].rentEnd - HtmlLayer.serverUtc > 0)
				{
					Main.UserInfo.skillsInfos[this.ID].rentEnd = Main.UserInfo.skillsInfos[this.ID].rentEnd - HtmlLayer.serverUtc + this.rentTime * 24 * 60 * 60 + HtmlLayer.serverUtc;
				}
				else
				{
					Main.UserInfo.skillsInfos[this.ID].rentEnd = this.rentTime * 24 * 60 * 60 + HtmlLayer.serverUtc;
				}
				Main.UserInfo.skillsInfos[this.ID].Unlocked = true;
			}
			else
			{
				Main.UserInfo.skillsInfos[this.ID].Unlocked = true;
			}
		}
		if (this.type == PackageItemType.weapon)
		{
			if (Main.UserInfo.weaponsStates[this.ID].rentEnd - HtmlLayer.serverUtc < 0 && Main.UserInfo.weaponsStates[this.ID].Unlocked)
			{
				return;
			}
			if (this.rentTime > 0)
			{
				if (Main.UserInfo.weaponsStates[this.ID].rentEnd - HtmlLayer.serverUtc > 0)
				{
					Main.UserInfo.weaponsStates[this.ID].rentEnd = Main.UserInfo.weaponsStates[this.ID].rentEnd - HtmlLayer.serverUtc + this.rentTime * 24 * 60 * 60 + HtmlLayer.serverUtc;
				}
				else
				{
					Main.UserInfo.weaponsStates[this.ID].rentEnd = this.rentTime * 24 * 60 * 60 + HtmlLayer.serverUtc;
				}
				Main.UserInfo.weaponsStates[this.ID].Unlocked = true;
			}
			else
			{
				Main.UserInfo.weaponsStates[this.ID].Unlocked = true;
			}
		}
	}

	// Token: 0x060012F7 RID: 4855 RVA: 0x000CDBCC File Offset: 0x000CBDCC
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
	}

	// Token: 0x040015F1 RID: 5617
	public PackageItemType type;

	// Token: 0x040015F2 RID: 5618
	public int ID;

	// Token: 0x040015F3 RID: 5619
	public int rentTime = -1;
}
