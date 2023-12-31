using System;
using System.Collections.Generic;
using LeagueGUI;
using UnityEngine;

// Token: 0x02000334 RID: 820
internal class LoadLeagueInfo : DatabaseEvent
{
	// Token: 0x06001B87 RID: 7047 RVA: 0x000F7ED0 File Offset: 0x000F60D0
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("adm.php?q=ladder/info", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001B88 RID: 7048 RVA: 0x000F7F0C File Offset: 0x000F610C
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
		object obj;
		dictionary.TryGetValue("result", out obj);
		if (obj != null && (int)obj != 0)
		{
			throw new Exception("Bad responce, result is: " + obj);
		}
		Dictionary<string, object> dictionary2 = (Dictionary<string, object>)dictionary["data"];
		LeagueWindow.I.LeagueInfo = new LeagueInfo();
		LeagueWindow.I.LeagueInfo.ParseSeason((Dictionary<string, object>)dictionary2["season"]);
		LeagueWindow.I.LeagueInfo.ParseUser((Dictionary<string, object>)dictionary2["user"]);
		LeagueWindow.I.LeagueInfo.ParseRules(dictionary2);
		if (!LeagueWindow.I.LeagueInfo.Offseason)
		{
			LeagueWindow.I.LeagueInfo.ParseAds((Dictionary<string, object>[])dictionary2["ads"]);
			object obj2;
			if (dictionary2.TryGetValue("ads_settings", out obj2))
			{
				Dictionary<string, object> dictionary3 = (Dictionary<string, object>)obj2;
				object value;
				if (dictionary3.TryGetValue("show_interval", out value))
				{
					LeagueWindow.I.LeagueInfo.AdShowTime = Convert.ToSingle(value);
				}
			}
			object obj3;
			if (dictionary2.TryGetValue("lp_delta", out obj3))
			{
				Dictionary<string, object> dictionary4 = (Dictionary<string, object>)obj3;
				object value2;
				if (dictionary4.TryGetValue("win", out value2))
				{
					LeagueWindow.I.LeagueInfo.WinPoint = Convert.ToInt32(value2);
				}
				object value3;
				if (dictionary4.TryGetValue("los", out value3))
				{
					LeagueWindow.I.LeagueInfo.LosePoint = Convert.ToInt32(value3);
				}
			}
		}
		this.SuccessAction();
	}
}
