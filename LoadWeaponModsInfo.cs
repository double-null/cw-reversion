using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033D RID: 829
internal class LoadWeaponModsInfo : DatabaseEvent
{
	// Token: 0x06001BEE RID: 7150 RVA: 0x000F9FC0 File Offset: 0x000F81C0
	public override void Initialize(params object[] args)
	{
		string actions = "adm.php?q=customization/client/weapon/";
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001BEF RID: 7151 RVA: 0x000FA000 File Offset: 0x000F8200
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception innerException)
		{
			Debug.LogError(new Exception("JSON:" + text, innerException));
			return;
		}
		object obj;
		if (dictionary.TryGetValue("result", out obj))
		{
			if ((int)obj != 0)
			{
				throw new Exception("Bad result: " + obj);
			}
			WeaponModsStorage.Instance().FillStorage(text);
		}
		this.SuccessAction();
	}
}
