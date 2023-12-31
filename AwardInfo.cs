using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class AwardInfo
{
	// Token: 0x06000544 RID: 1348 RVA: 0x00021910 File Offset: 0x0001FB10
	public AwardInfo(Dictionary<string, object> dict)
	{
		JSON.ReadWrite(dict, "url", ref this.Url, false);
		this.ConvertAwards(dict);
	}

	// Token: 0x170000BC RID: 188
	// (get) Token: 0x06000545 RID: 1349 RVA: 0x00021934 File Offset: 0x0001FB34
	public string[] AwardsByLanguage
	{
		get
		{
			return this._awardDescriptions[(int)Language.CurrentLanguage];
		}
	}

	// Token: 0x06000546 RID: 1350 RVA: 0x00021944 File Offset: 0x0001FB44
	private void ConvertAwards(Dictionary<string, object> dict)
	{
		object[] array = dict["awards"] as object[];
		if (array == null)
		{
			Debug.LogError("error parsing award data");
			return;
		}
		this._awardDescriptions = new string[Language.Languages.Length][];
		for (int i = 0; i < this._awardDescriptions.Length; i++)
		{
			this._awardDescriptions[i] = new string[array.Length];
		}
		for (int j = 0; j < array.Length; j++)
		{
			object obj = array[j];
			for (int k = 0; k < Language.Languages.Length; k++)
			{
				string key = "description_" + Language.Languages[k].ToLower();
				Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
				if (dictionary == null)
				{
					this._awardDescriptions[k][j] = string.Empty;
					Debug.LogError("error parsing award description");
				}
				else
				{
					string text = dictionary[key] as string;
					this._awardDescriptions[k][j] = ((!string.IsNullOrEmpty(text)) ? text : string.Empty);
				}
			}
		}
	}

	// Token: 0x040004B9 RID: 1209
	public Texture2D Icon;

	// Token: 0x040004BA RID: 1210
	public string Url;

	// Token: 0x040004BB RID: 1211
	private string[][] _awardDescriptions;
}
