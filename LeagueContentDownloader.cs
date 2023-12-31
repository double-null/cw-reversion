using System;
using System.Collections;
using LeagueGUI;
using UnityEngine;

// Token: 0x02000304 RID: 772
internal class LeagueContentDownloader : MonoBehaviour
{
	// Token: 0x06001A70 RID: 6768 RVA: 0x000F0274 File Offset: 0x000EE474
	private IEnumerator DownloadAd()
	{
		for (int i = 0; i < LeagueWindow.I.LeagueInfo.Ads.Length; i++)
		{
			WWW www = new WWW(LeagueWindow.I.LeagueInfo.Ads[i].AdLink);
			yield return www;
			LeagueWindow.I.LeagueInfo.Ads[i].AdBanner = www.texture;
		}
		this._adLoaded = true;
		yield break;
	}

	// Token: 0x06001A71 RID: 6769 RVA: 0x000F0290 File Offset: 0x000EE490
	private IEnumerator DownloadPrize()
	{
		for (int i = 0; i < 3; i++)
		{
			WWW www = new WWW(LeagueWindow.I.LeagueInfo.SeasonPrizes[i].AwardLink);
			yield return www;
			LeagueWindow.I.LeagueInfo.SeasonPrizes[i].AwardIcon = www.texture;
		}
		this._prizeLoaded = true;
		yield break;
	}

	// Token: 0x06001A72 RID: 6770 RVA: 0x000F02AC File Offset: 0x000EE4AC
	public void DownloadContent()
	{
		if (LeagueWindow.I.LeagueInfo.Offseason)
		{
			this._adLoaded = true;
			base.StartCoroutine(this.DownloadPrize());
			return;
		}
		base.StartCoroutine(this.DownloadAd());
		base.StartCoroutine(this.DownloadPrize());
	}

	// Token: 0x170007CA RID: 1994
	// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000F02FC File Offset: 0x000EE4FC
	public bool LeagueContentLoaded
	{
		get
		{
			return this._adLoaded && this._prizeLoaded;
		}
	}

	// Token: 0x06001A74 RID: 6772 RVA: 0x000F0314 File Offset: 0x000EE514
	private void Start()
	{
		LeagueContentDownloader.I = this;
	}

	// Token: 0x04001F3E RID: 7998
	public static LeagueContentDownloader I;

	// Token: 0x04001F3F RID: 7999
	private bool _adLoaded;

	// Token: 0x04001F40 RID: 8000
	private bool _prizeLoaded;
}
