using System;
using System.Collections.Generic;

// Token: 0x0200033C RID: 828
internal class MasteringMainLoad : DatabaseEvent
{
	// Token: 0x06001BEB RID: 7147 RVA: 0x000F9DFC File Offset: 0x000F7FFC
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=customization/player/main_load", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BEC RID: 7148 RVA: 0x000F9E38 File Offset: 0x000F8038
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("MasteringMainLoad. Responsce is null or empty");
		}
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		object obj;
		if (dictionary.TryGetValue("mod_info", out obj))
		{
			Dictionary<string, object> dictionary2 = (Dictionary<string, object>)obj;
			object obj2;
			if (dictionary2.TryGetValue("result", out obj2))
			{
				if ((int)obj2 != 0)
				{
					throw new Exception("Mod info. Bad result: " + obj2);
				}
				ModsStorage.Instance().FillStorage(ArrayUtility.ToJSON<string, object>(dictionary2));
			}
			object obj3;
			if (dictionary.TryGetValue("weapon_info", out obj3))
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)obj3;
				object obj4;
				if (dictionary3.TryGetValue("result", out obj4))
				{
					if ((int)obj2 != 0)
					{
						throw new Exception("Weapon info. Bad result: " + obj2);
					}
					WeaponModsStorage.Instance().FillStorage(ArrayUtility.ToJSON<string, object>(dictionary3));
				}
			}
		}
		object obj5;
		if (dictionary.TryGetValue("load", out obj5))
		{
			Dictionary<string, object> dict = (Dictionary<string, object>)obj5;
			Main.UserInfo.Mastering.Initialize(ArrayUtility.ToJSON<string, object>(dict));
		}
		object obj6;
		if (dictionary.TryGetValue("load_suits", out obj6))
		{
			Dictionary<string, object> dict2 = (Dictionary<string, object>)obj6;
			MasteringSuitsInfo.Instance.Initialize(ArrayUtility.ToJSON<string, object>(dict2));
		}
		object obj7;
		if (dictionary.TryGetValue("rarity_colors", out obj7))
		{
			Dictionary<string, object> dict3 = (Dictionary<string, object>)obj7;
			RarityColors.Initialize(ArrayUtility.ToJSON<string, object>(dict3));
		}
		this.SuccessAction();
	}
}
