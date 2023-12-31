using System;
using System.Collections.Generic;
using System.Reflection;
using JsonFx.Json;
using LeagueGUI;
using UnityEngine;

// Token: 0x02000335 RID: 821
internal class LoadLeagueRating : DatabaseEvent
{
	// Token: 0x06001B8B RID: 7051 RVA: 0x000F8118 File Offset: 0x000F6318
	public override void Initialize(params object[] args)
	{
		if (LoadLeagueRating._lockTimeout > Time.time)
		{
			this.SuccessAction();
			return;
		}
		if (LeagueWindow.I.LeagueRatingInfo == null || LeagueWindow.I.LeagueRatingInfo.Length < 1)
		{
			LoadLeagueRating._lockTimeout = Time.time + 10f;
		}
		else
		{
			LoadLeagueRating._lockTimeout = Time.time + 30f;
		}
		bool flag = (bool)Crypt.ResolveVariable(args, false, 0);
		string actions = "adm.php?q=ladder/rating";
		if (flag)
		{
			actions = "adm.php?q=ladder/selfrating";
		}
		if (LeagueWindow.I.LeagueInfo.Offseason)
		{
			actions = "adm.php?q=ladder/rating/prev";
		}
		HtmlLayer.Request(actions, new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x06001B8C RID: 7052 RVA: 0x000F81F0 File Offset: 0x000F63F0
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			throw new Exception("String is null or empty");
		}
		try
		{
			LoadLeagueRating.JsonLeagueRatingData jsonLeagueRatingData = JsonReader.Deserialize<LoadLeagueRating.JsonLeagueRatingData>(text);
			LeagueWindow.I.LeagueRatingInfo = jsonLeagueRatingData.data.ToArray();
			this.SuccessAction();
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	// Token: 0x04002081 RID: 8321
	private static float _lockTimeout;

	// Token: 0x02000336 RID: 822
	[Obfuscation(Exclude = true)]
	private class JsonLeagueRatingData
	{
		// Token: 0x04002082 RID: 8322
		public int result;

		// Token: 0x04002083 RID: 8323
		public List<LeagueRatingInfo> data = new List<LeagueRatingInfo>();
	}
}
