using System;
using System.Collections;
using System.Collections.Generic;
using CarrierGUINamespace;
using JsonFx.Json;
using UnityEngine;

// Token: 0x020000CF RID: 207
internal class AwardsManager
{
	// Token: 0x06000547 RID: 1351 RVA: 0x00021A5C File Offset: 0x0001FC5C
	internal AwardsManager()
	{
		HtmlLayer.Request("adm.php?q=setting/getAwards", delegate(string text, string url)
		{
			Dictionary<string, object>[] array = JsonReader.Deserialize(text) as Dictionary<string, object>[];
			if (array == null)
			{
				Debug.LogError("cant parse awards data");
				return;
			}
			this._awards = new AwardInfo[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this._awards[i] = new AwardInfo(array[i]);
			}
			this.SetSeasonAwards();
		}, delegate(Exception exception, string url)
		{
			Debug.LogError("Error downloading award icons");
		}, string.Empty, string.Empty);
	}

	// Token: 0x06000548 RID: 1352 RVA: 0x00021AAC File Offset: 0x0001FCAC
	internal Texture2D GetAwardIconByPlace(int place, bool hc)
	{
		int awardIndex = this.GetAwardIndex(place, hc);
		if (this.SeasonAwards == null || awardIndex < 0)
		{
			return null;
		}
		return this.SeasonAwards[awardIndex];
	}

	// Token: 0x06000549 RID: 1353 RVA: 0x00021AE0 File Offset: 0x0001FCE0
	private int GetAwardIndex(int place, bool hc)
	{
		if (this.AwardPlaces == null)
		{
			return -1;
		}
		int num = (!hc) ? 0 : this.AwardPlaces.Length;
		if (place == 1)
		{
			return num;
		}
		num++;
		int i = 0;
		while (i < this.AwardPlaces.Length - 1)
		{
			if (place > this.AwardPlaces[i] && place <= this.AwardPlaces[i + 1])
			{
				return num;
			}
			i++;
			num++;
		}
		return -1;
	}

	// Token: 0x0600054A RID: 1354 RVA: 0x00021B5C File Offset: 0x0001FD5C
	private void SetSeasonAwards()
	{
		HtmlLayer.Request("adm.php?q=setting/getSeasonAwardsPath", new RequestFinished(this.OnSeasonAwardsDataLoaded), delegate(Exception exception, string url)
		{
			Debug.LogError("Error downloading season award urls");
		}, string.Empty, string.Empty);
	}

	// Token: 0x0600054B RID: 1355 RVA: 0x00021B9C File Offset: 0x0001FD9C
	private void OnSeasonAwardsDataLoaded(string text, string url)
	{
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		string[] array = dictionary["urls"] as string[];
		this.AwardPlaces = (dictionary["places"] as int[]);
		if (array == null || this.AwardPlaces == null || 2 * this.AwardPlaces.Length != array.Length)
		{
			Debug.LogError("Wrong awards data format");
			return;
		}
		this.SeasonAwards = new Texture2D[array.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string url2 = array[i];
			CoroutineRunner.RunCoroutine(this.DownloadSeasonAwardIcon(url2, i));
		}
	}

	// Token: 0x0600054C RID: 1356 RVA: 0x00021C40 File Offset: 0x0001FE40
	private IEnumerator DownloadSeasonAwardIcon(string url, int index)
	{
		WWW www = new WWW(url);
		yield return www;
		this.SeasonAwards[index] = www.texture;
		yield break;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00021C78 File Offset: 0x0001FE78
	internal void SetEarnedAwards(ScrollableLine awardScroll, OverviewInfo info)
	{
		awardScroll.ClearContainer();
		foreach (KeyValuePair<string, int[]> awardData in info.Awards)
		{
			CoroutineRunner.RunCoroutine(this.SetEarnedAwardCoroutine(awardData, awardScroll));
		}
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x00021CEC File Offset: 0x0001FEEC
	private IEnumerator SetEarnedAwardCoroutine(KeyValuePair<string, int[]> awardData, ScrollableLine awardScroll)
	{
		while (this._awards == null)
		{
			yield return new WaitForEndOfFrame();
		}
		int awardIndex = int.Parse(awardData.Key);
		if (awardIndex >= this._awards.Length)
		{
			Debug.LogError("award index is out of range");
			yield break;
		}
		AwardInfo award = this._awards[awardIndex];
		if (award.Icon == null)
		{
			WWW www = new WWW(award.Url);
			yield return www;
			award.Icon = www.texture;
		}
		for (int i = 0; i < awardData.Value.Length; i++)
		{
			int hintIndex = awardData.Value[i];
			string hint = award.AwardsByLanguage[hintIndex];
			awardScroll.SetImage(new ButtonContainer(award.Icon, hint, false));
		}
		yield break;
	}

	// Token: 0x040004BC RID: 1212
	public Texture2D[] SeasonAwards;

	// Token: 0x040004BD RID: 1213
	public int[] AwardPlaces;

	// Token: 0x040004BE RID: 1214
	private AwardInfo[] _awards;
}
