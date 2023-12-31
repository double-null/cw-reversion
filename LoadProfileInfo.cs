using System;
using System.Collections.Generic;
using LeagueGUI;
using UnityEngine;

// Token: 0x02000337 RID: 823
internal class LoadProfileInfo : DatabaseEvent
{
	// Token: 0x06001B8F RID: 7055 RVA: 0x000F8280 File Offset: 0x000F6480
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=ladder/profileinfo", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001B90 RID: 7056 RVA: 0x000F82BC File Offset: 0x000F64BC
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception innerException)
		{
			Debug.LogError(new Exception("JSON:" + text, innerException));
			return;
		}
		LeagueWindow.I.LeagueInfo.ParseUser(dictionary);
		this.SuccessAction();
	}
}
