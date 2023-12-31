using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002B1 RID: 689
[Serializable]
internal class Social : Convertible
{
	// Token: 0x06001369 RID: 4969 RVA: 0x000D0900 File Offset: 0x000CEB00
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		try
		{
			int i = 0;
			if (this.firstName.Length > 1)
			{
				try
				{
					for (i = 0; i < this.firstName.Length; i++)
					{
						char.ConvertToUtf32(this.firstName, i);
					}
				}
				catch (Exception ex)
				{
					if (i > 0)
					{
						this.firstName = this.firstName.Substring(0, i - 1);
					}
					else
					{
						this.firstName = string.Empty;
					}
				}
			}
			if (this.lastName.Length > 1)
			{
				try
				{
					for (i = 0; i < this.lastName.Length; i++)
					{
						char.ConvertToUtf32(this.lastName, i);
					}
				}
				catch (Exception ex2)
				{
					if (i > 0)
					{
						this.lastName = this.lastName.Substring(0, i - 1);
					}
					else
					{
						this.lastName = string.Empty;
					}
				}
			}
			JSON.ReadWrite(dict, "first_name", ref this.firstName, isWrite);
			JSON.ReadWrite(dict, "last_name", ref this.lastName, isWrite);
			JSON.ReadWrite(dict, "photo", ref this.photo, isWrite);
			JSON.ReadWrite(dict, "photo_medium", ref this.photo_med, isWrite);
			JSON.ReadWrite(dict, "photo_big", ref this.photo_big, isWrite);
			if (this.photo == "https://vk.com/images/deactivated_cn.png")
			{
				this.photo = string.Empty;
			}
		}
		catch (Exception ex3)
		{
			Debug.Log("Error loading Social Info");
			this.firstName = string.Empty;
			this.lastName = string.Empty;
			this.photo = string.Empty;
			this.photo_med = string.Empty;
			this.photo_big = string.Empty;
		}
	}

	// Token: 0x04001684 RID: 5764
	public string firstName = string.Empty;

	// Token: 0x04001685 RID: 5765
	public string lastName = string.Empty;

	// Token: 0x04001686 RID: 5766
	public string photo;

	// Token: 0x04001687 RID: 5767
	public string photo_med;

	// Token: 0x04001688 RID: 5768
	public string photo_big;
}
