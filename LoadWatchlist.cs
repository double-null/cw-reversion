using System;
using System.Collections.Generic;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

// Token: 0x0200023E RID: 574
internal class LoadWatchlist : DatabaseEvent
{
	// Token: 0x0600119F RID: 4511 RVA: 0x000C3DE0 File Offset: 0x000C1FE0
	public override void Initialize(params object[] args)
	{
		HtmlLayer.Request("?action=wl_list", new RequestFinished(this.OnResponse), new RequestFailed(this.OnFail), string.Empty, string.Empty);
	}

	// Token: 0x060011A0 RID: 4512 RVA: 0x000C3E1C File Offset: 0x000C201C
	protected override void OnResponse(string text)
	{
		if (string.IsNullOrEmpty(text) || text == "NULL")
		{
			throw new Exception("String is null or empty");
		}
		Dictionary<string, object> dictionary = null;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			this.SuccessAction();
			return;
		}
		Main.Watchlist.Clear();
		if (((object[])dictionary["list"]).Length == 0)
		{
			this.SuccessAction();
		}
		else
		{
			foreach (Dictionary<string, object> dict in (Dictionary<string, object>[])dictionary["list"])
			{
				WatchlistItem watchlistItem = new WatchlistItem();
				watchlistItem.Read(dict);
				Main.Watchlist.Add(watchlistItem);
			}
			this.SuccessAction();
		}
	}

	// Token: 0x060011A1 RID: 4513 RVA: 0x000C3F20 File Offset: 0x000C2120
	protected override void OnFail(Exception e)
	{
		this.FailedAction();
	}
}
