using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Assets.Scripts.Game.Foundation;
using CarrierGUINamespace;
using ClanSystemGUI;
using UnityEngine;

// Token: 0x020000ED RID: 237
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/GUI/CarrierGUI")]
internal class CarrierGUI : Form
{
	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x0600063C RID: 1596 RVA: 0x00031558 File Offset: 0x0002F758
	public AwardsManager AwardsManager
	{
		get
		{
			if (this._awardsManager == null)
			{
				this._awardsManager = new AwardsManager();
			}
			return this._awardsManager;
		}
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x00031578 File Offset: 0x0002F778
	[Obfuscation(Exclude = true)]
	private void ShowCarrier(object obj)
	{
		this.ClassName = Language.CarrClassName;
		this.RecalcProgress();
		this.Show(0.5f, 0f);
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0003159C File Offset: 0x0002F79C
	[Obfuscation(Exclude = true)]
	private void HideCarrier(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x000315AC File Offset: 0x0002F7AC
	[Obfuscation(Exclude = true)]
	private void InitCarrier(object obj)
	{
		this._info = (OverviewInfo)obj;
		this.avatar = this.avatar_back;
		base.StartCoroutine(this.DownloadAvatar());
		this.idCached = this._info.userID.Value;
		this.statistics = new CarrierStatistics(this._info);
		this.AwardsManager.SetEarnedAwards(this.awardScroll, this._info);
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0003161C File Offset: 0x0002F81C
	[Obfuscation(Exclude = true)]
	private void StartVoting(object obj)
	{
		this.voteProcessing = true;
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00031628 File Offset: 0x0002F828
	[Obfuscation(Exclude = true)]
	private void StopVoting(object obj)
	{
		this.voteProcessing = false;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00031634 File Offset: 0x0002F834
	public void LoadInfo(int userID)
	{
		this.idCached = userID;
		HtmlLayer.Request("?action=overview&user_id=" + userID + "&notask=1", new RequestFinished(this.LoadInfoFinished), new RequestFailed(this.LoadInfoFailed), string.Empty, string.Empty);
		global::Console.print("IDLoad", Color.magenta);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00031694 File Offset: 0x0002F894
	private void LoadInfoFinished(string text, string url)
	{
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(text, string.Empty);
		}
		catch (Exception ex)
		{
			if (Application.isEditor || Peer.Dedicated)
			{
				global::Console.print(ex.ToString());
				global::Console.print(text);
			}
			global::Console.exception(new Exception("Data Server Error"));
			return;
		}
		if ((int)dictionary["result"] == 1)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.PopupGUI, Language.Error, Language.CarrUserForbidYourself, PopupState.information, true, true, string.Empty, string.Empty));
		}
		if ((int)dictionary["result"] != 0)
		{
			return;
		}
		OverviewInfo overviewInfo = new OverviewInfo(true);
		overviewInfo.Read(dictionary, true);
		if (overviewInfo.skillUnlocked(Skills.car_block) && Main.UserInfo.Permission < EPermission.Admin && Main.UserInfo.Permission != EPermission.Moder)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.PopupGUI, Language.CarrClassified, Language.CarrUserForbidYourself, PopupState.information, true, true, string.Empty, string.Empty));
			global::Console.print("IDLoad denied", Color.magenta);
			return;
		}
		overviewInfo.userID = this.idCached;
		for (int i = 0; i < Globals.I.weapons.Length; i++)
		{
			overviewInfo.weaponsStates[i].Init(true, i);
		}
		this.InitCarrier(overviewInfo);
		this.RecalcProgress();
		this._carrierState = CarrierState.OVERVIEW;
		global::Console.print("IDLoad Finished", Color.magenta);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00031838 File Offset: 0x0002FA38
	private void LoadInfoFailed(Exception e, string url)
	{
		global::Console.print("IDLoad error", Color.red);
		global::Console.exception(e);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00031850 File Offset: 0x0002FA50
	private IEnumerator DownloadAvatar()
	{
		if (string.IsNullOrEmpty(this._info.socialInfo.photo))
		{
			yield break;
		}
		WWW www = new WWW(this._info.socialInfo.photo);
		yield return www;
		if (www.error != null)
		{
			yield break;
		}
		if (this.wwwTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.wwwTexture);
			this.wwwTexture = null;
		}
		if (www.texture)
		{
			this.wwwTexture = www.texture;
			this.avatar = this.wwwTexture;
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0003186C File Offset: 0x0002FA6C
	public void AchievementDraw(Vector2 pos, int index)
	{
		bool flag = this._info.achievementsInfos[index].current == this._info.achievementsInfos[index].count;
		if (this.gui.Button(pos, (!flag) ? this.achievementsLocked[index] : this.achievementsUnocked[index], (!flag) ? this.achievementsLocked[index] : this.achievementsUnocked[index], (!flag) ? this.achievementsLocked[index] : this.achievementsUnocked[index], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Hover)
		{
			this.lastIndex = index;
			this.popup.Show(0.5f, 0f);
			this.mousePos = Input.mousePosition;
		}
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00031958 File Offset: 0x0002FB58
	private ContractInfo GetContract(OverviewInfo userInfo, int i = 0)
	{
		if (i == 0)
		{
			return userInfo.contractsInfo.CurrentEasy;
		}
		if (i == 1)
		{
			return userInfo.contractsInfo.CurrentNormal;
		}
		return userInfo.contractsInfo.CurrentHard;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x00031998 File Offset: 0x0002FB98
	private int GetContractCount(OverviewInfo userInfo, int i = 0)
	{
		if (i == 0)
		{
			return userInfo.contractsInfo.CurrentEasyCount;
		}
		if (i == 1)
		{
			return userInfo.contractsInfo.CurrentNormalCount;
		}
		return userInfo.contractsInfo.CurrentHardCount;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x000319D8 File Offset: 0x0002FBD8
	private ContractInfo[] GetArrContracts(OverviewInfo userInfo, int i = 0)
	{
		if (i == 0)
		{
			return userInfo.contractsInfo.Easy;
		}
		if (i == 1)
		{
			return userInfo.contractsInfo.Normal;
		}
		return userInfo.contractsInfo.Hard;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x00031A18 File Offset: 0x0002FC18
	private int GetContractID(OverviewInfo userInfo, int i = 0)
	{
		if (i == 0)
		{
			return userInfo.contractsInfo.CurrentEasyIndex;
		}
		if (i == 1)
		{
			return userInfo.contractsInfo.CurrentNormalIndex;
		}
		if (i == 2)
		{
			return userInfo.contractsInfo.CurrentHardIndex;
		}
		return 0;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x00031A60 File Offset: 0x0002FC60
	private void CarrierOneContractGUI(OverviewInfo userInfo, int i, ContractInfo contract, int ID)
	{
		this.nextContract = this.GetArrContracts(userInfo, i);
		int contractCount = this.GetContractCount(userInfo, i);
		if (contractCount >= contract.task_counter)
		{
			this.bcontractComplete = true;
		}
		else
		{
			this.bcontractComplete = false;
		}
		if (this.bcontractComplete)
		{
			this.gui.Picture(new Vector2(0f, 0f), this.ContractImgs[6]);
		}
		else
		{
			this.gui.Picture(new Vector2(0f, 0f), this.ContractImgs[5]);
		}
		this.gui.Picture(new Vector2(16f, 15f), this.ContractStarsImgs[i]);
		if (this.ContractTypeImgs.Length > contract.imgidx)
		{
			this.gui.Picture(new Vector2((float)((this.ContractImgs[6].height - this.ContractTypeImgs[0].height) / 2 - 2), (float)((this.ContractImgs[6].width - this.ContractTypeImgs[0].width) / 2)), this.ContractTypeImgs[contract.imgidx]);
		}
		this.gui.TextLabel(new Rect(160f, 158f, 50f, 50f), ID.ToString(), 12, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(24f, 175f, 149f, 51f), contract.description, 9, "#FFFFFF_Tahoma", TextAnchor.UpperCenter, true);
		if (this.bcontractComplete)
		{
			this.gui.Picture(new Vector2(139f, 30f), this.ContractImgs[4]);
		}
		this.gui.Picture(new Vector2(20f, 202f), this.ContractImgs[7]);
		if (contract.prizeCR > 0)
		{
			this.gui.Picture(new Vector2((float)(20 + this.ContractImgs[7].width - this.gui.crIcon.width - 5), 205f), this.gui.crIcon);
			this.gui.TextLabel(new Rect(67f, 205f, 80f, 50f), "+" + Helpers.SeparateNumericString(contract.prizeCR.ToString()), 16, "#FFFFFF", TextAnchor.UpperRight, true);
		}
		if (contract.prizeSP > 0)
		{
			this.gui.Picture(new Vector2((float)(20 + this.ContractImgs[7].width - this.gui.crIcon.width - 6), 204f), this.gui.spIcon_med);
			this.gui.TextLabel(new Rect(67f, 205f, 80f, 50f), "+" + contract.prizeSP, 16, "#FFFFFF", TextAnchor.UpperRight, true);
		}
		if (contract.prizeGP > 0)
		{
			this.gui.Picture(new Vector2((float)(20 + this.ContractImgs[7].width - this.gui.crIcon.width - 3), 205f), this.gui.gldIcon);
			this.gui.TextLabel(new Rect(67f, 205f, 80f, 50f), "+" + Helpers.SeparateNumericString(contract.prizeGP.ToString()), 16, "#FFFFFF", TextAnchor.UpperRight, true);
		}
		this.gui.TextLabel(new Rect(10f, 200f, 80f, 50f), contractCount + "/" + contract.task_counter, 16, "#FFFFFF", TextAnchor.UpperCenter, true);
		this.gui.Picture(new Vector2(28f, 220f), this.gui.repair[2]);
		this.gui.PictureSized(new Vector2(30f, 222f), this.gui.repair[4], new Vector2((float)(this.gui.repair[2].width - 4) * ((float)contractCount / (float)contract.task_counter), (float)this.gui.repair[4].height));
		if (this.nextContract.Length > ID + 1)
		{
			this.NextContracts(ID + 1, 0f);
		}
		if (this.nextContract.Length > ID + 2)
		{
			this.NextContracts(ID + 2, 33f);
		}
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		if (this.bcontractComplete)
		{
			this.gui.Picture(new Vector2(80f, 220f), this.ContractImgs[0]);
		}
		else if (userInfo.userID == Main.UserInfo.userID)
		{
			if (PopupGUI.IsAnyPopupShow || this.nextContract.Length == ID + 1 || Main.IsGameLoaded)
			{
				GUI.enabled = false;
			}
			if (GUI.Button(new Rect(33f, 233f, (float)this.SkipContractBtn.normal.background.width, (float)this.SkipContractBtn.normal.background.height), Language.CarrSkipContract, this.SkipContractBtn))
			{
				object[] args = new object[]
				{
					i,
					ID
				};
				EventFactory.Call("ShowPopup", new Popup(WindowsID.SkipContract, Language.CarrSkipContractPopupHeader, string.Empty, delegate()
				{
				}, PopupState.skipContract, false, true, args));
			}
			float num = MainGUI.Instance.CalcWidth(Language.CarrSkipContract, MainGUI.Instance.fontDNC57, 16);
			float left = 33f + ((float)this.SkipContractBtn.normal.background.width - num) / 2f + num - this.SkipContractBtn.contentOffset.x;
			GUI.DrawTexture(new Rect(left, 234f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
			GUI.enabled = true;
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x000320EC File Offset: 0x000302EC
	private void NextContracts(int id, float yPos = 0f)
	{
		string content = string.Empty;
		if (!this.bcontractComplete)
		{
			this.gui.color = Colors.alpha(this.gui.color, base.visibility * 0.5f);
		}
		this.gui.Picture(new Vector2(20f, (float)(255 + this.ContractImgs[8].height) + yPos), this.ContractImgs[8]);
		if (this.nextContract[id].prizeCR > 0)
		{
			this.gui.Picture(new Vector2((float)(15 + this.ContractImgs[8].width - this.gui.crIcon.width), (float)(258 + this.ContractImgs[8].height) + yPos), this.gui.crIcon);
			content = Helpers.SeparateNumericString(this.nextContract[id].prizeCR.ToString());
		}
		else if (this.nextContract[id].prizeGP > 0)
		{
			this.gui.Picture(new Vector2((float)(17 + this.ContractImgs[8].width - this.gui.crIcon.width), (float)(258 + this.ContractImgs[8].height) + yPos), this.gui.gldIcon);
			content = Helpers.SeparateNumericString(this.nextContract[id].prizeGP.ToString());
		}
		else
		{
			this.gui.Picture(new Vector2((float)(14 + this.ContractImgs[8].width - this.gui.crIcon.width), (float)(257 + this.ContractImgs[8].height) + yPos), this.gui.spIcon_med);
			content = this.nextContract[id].prizeSP.ToString();
		}
		this.gui.TextLabel(new Rect(67f, (float)(258 + this.ContractImgs[8].height) + yPos, 80f, 50f), content, 16, "#FFFFFF", TextAnchor.UpperRight, true);
		this.gui.TextLabel(new Rect(27f, (float)(258 + this.ContractImgs[8].height) + yPos, 80f, 50f), id.ToString(), 12, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00032354 File Offset: 0x00030554
	private void GenerateList()
	{
		this.TopRatingList.Clear();
		for (int i = 0; i < Main.RatingTable.Length; i++)
		{
			this.TopRatingList.Add(new TopRatingBar(Main.RatingTable[i], this._isSeasonRating, !string.IsNullOrEmpty(this.HCRatingSuffix)));
		}
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x000323B0 File Offset: 0x000305B0
	private void SetSort(CarrierGUI.WatchlistSortingBy sortingBy)
	{
		if (this._sortOrder.sortingBy != sortingBy)
		{
			this._sortOrder.sortingBy = sortingBy;
			this._sortOrder.invers = false;
			this._sortOrder.complete = false;
		}
		else
		{
			this._sortOrder.invers = !this._sortOrder.invers;
			this._sortOrder.complete = false;
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0003241C File Offset: 0x0003061C
	private void WatchlistSorting()
	{
		switch (this._sortOrder.sortingBy)
		{
		case CarrierGUI.WatchlistSortingBy.Exp:
			Main.Watchlist.Sort((WatchlistItem item1, WatchlistItem item2) => (!this._sortOrder.invers) ? item1.PlayerExp.CompareTo(item2.PlayerExp) : item2.PlayerExp.CompareTo(item1.PlayerExp));
			break;
		case CarrierGUI.WatchlistSortingBy.Kill:
			Main.Watchlist.Sort((WatchlistItem item1, WatchlistItem item2) => (!this._sortOrder.invers) ? item2.PlayerKills.CompareTo(item1.PlayerKills) : item1.PlayerKills.CompareTo(item2.PlayerKills));
			break;
		case CarrierGUI.WatchlistSortingBy.Death:
			Main.Watchlist.Sort((WatchlistItem item1, WatchlistItem item2) => (!this._sortOrder.invers) ? item2.PlayerDeaths.CompareTo(item1.PlayerDeaths) : item1.PlayerDeaths.CompareTo(item2.PlayerDeaths));
			break;
		case CarrierGUI.WatchlistSortingBy.Kd:
			Main.Watchlist.Sort((WatchlistItem item1, WatchlistItem item2) => (!this._sortOrder.invers) ? item2.Kd.CompareTo(item1.Kd) : item1.Kd.CompareTo(item2.Kd));
			break;
		case CarrierGUI.WatchlistSortingBy.Reputation:
			Main.Watchlist.Sort((WatchlistItem item1, WatchlistItem item2) => (!this._sortOrder.invers) ? item2.PlayerReputation.CompareTo(item1.PlayerReputation) : item1.PlayerReputation.CompareTo(item2.PlayerReputation));
			break;
		}
		this.AssembleWatchlist();
		this._sortOrder.complete = true;
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x000324F0 File Offset: 0x000306F0
	private void GenerateWatchlist()
	{
		this.WatchList.Clear();
		this.SetSort(CarrierGUI.WatchlistSortingBy.Exp);
		foreach (WatchlistItem item in Main.Watchlist)
		{
			this.WatchList.Add(new WatchlistBar(item));
		}
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00032574 File Offset: 0x00030774
	private void AssembleWatchlist()
	{
		this.WatchList.Clear();
		foreach (WatchlistItem item in Main.Watchlist)
		{
			this.WatchList.Add(new WatchlistBar(item));
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x000325F0 File Offset: 0x000307F0
	private void CarrierOverall()
	{
		this.gui.Picture(new Vector2(26f, 76f), this.matchResultGUI.result_window[5]);
		this.gui.Picture(new Vector2(78f, 76f), this.progressBar);
		this.gui.ProgressBar(new Vector2(82f, 79f), (float)(this.progressBar.width - 8), (this._info.currentXP.Value - OverviewInfo.MinXp(this._info.currentXP.Value)) / (OverviewInfo.MaxXp(this._info.currentXP.Value) - OverviewInfo.MinXp(this._info.currentXP.Value)), this.matchResultGUI.result_window[1], 0f, false, true);
		float num = (float)Globals.I.expTable[OverviewInfo.GetLevel(this._info.currentXP.Value)];
		float num2 = (float)Globals.I.expTable[OverviewInfo.GetLevel(this._info.currentXP.Value) + 1];
		this.gui.TextLabel(new Rect(84f, 75f, 100f, 30f), Helpers.SeparateNumericString(num.ToString("F0")), 17, "#000000", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(83f, 75f, 100f, 30f), Helpers.SeparateNumericString(num.ToString("F0")), 17, "#ffffff", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(241f, 75f, 100f, 30f), Helpers.SeparateNumericString(num2.ToString("F0")), 17, "#000000", TextAnchor.MiddleRight, true);
		this.gui.TextLabel(new Rect(240f, 75f, 100f, 30f), Helpers.SeparateNumericString(num2.ToString("F0")), 17, "#ffffff", TextAnchor.MiddleRight, true);
		float num3 = (this._info.currentXP.Value - OverviewInfo.MinXp(this._info.currentXP.Value)) / (OverviewInfo.MaxXp(this._info.currentXP.Value) - OverviewInfo.MinXp(this._info.currentXP.Value));
		float num4 = (float)(this.progressBar.width - 8) * num3;
		if (num4 > (float)(this.progressBar.width - 30))
		{
			num4 = (float)(this.progressBar.width - 30);
		}
		this.gui.TextLabel(new Rect(num4 + 30f, 97f, 100f, 30f), Helpers.SeparateNumericString(this._info.currentXP.Value.ToString("F0")), 17, "#62aeea", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(6f, 57f, (float)this.progressBar.width, 20f), Language.CarrToTheNextRank, 15, "#999999", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(70f, 57f, (float)this.progressBar.width, 20f), Helpers.SeparateNumericString((num2 - this._info.currentXP.Value).ToString("F0")), 15, "#ffffff", TextAnchor.MiddleRight, true);
		this.gui.Picture(new Vector2(26f, 126f), this.carrerOverall[0]);
		this.gui.Picture(new Vector2(43f, 140f), this.bigRanks[this._info.CurrentLevel]);
		this.gui.TextLabel(new Rect(22f, 120f, 160f, 30f), Language.CarrRank, 15, "#cccccc", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(22f, 290f, 160f, 30f), this.gui.rank_text[OverviewInfo.GetLevel(this._info.currentXP.Value)], 15, "#cccccc", TextAnchor.MiddleCenter, true);
		this.gui.Picture(new Vector2(212f, 140f), this.bigRanks[this._info.CurrentLevel + 1]);
		this.gui.TextLabel(new Rect(193f, 120f, 160f, 30f), Language.CarrNextRank, 15, "#999999", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(193f, 290f, 160f, 30f), this.gui.rank_text[OverviewInfo.GetLevel(this._info.currentXP.Value) + 1], 15, "#999999", TextAnchor.MiddleCenter, true);
		this.gui.Picture(new Vector2(193f, 126f), this.carrerOverall[1]);
		if (this.gui.Button(new Vector2(26f, 326f), this.carrerOverall[2], this.carrerOverall[3], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.scrollPos.y = (float)((this._info.ratingPlace - 1) * 40);
			this._carrierState = CarrierState.RATING;
		}
		this.gui.Picture(new Vector2(330f, 343f), this.carrerOverall[9]);
		this.gui.Picture(new Vector2(25f, 325f), this.carrerOverall[4]);
		this.gui.TextLabel(new Rect(25f, 325f, 326f, 22f), Language.CarrPlaceRanking, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(25f, 329f, 326f, 64f), '#' + ((this.ratingPlaceCached == -1) ? this._info.ratingPlace : this.ratingPlaceCached).ToString(), 17, "#d6d6d6_Micra", TextAnchor.MiddleCenter, true);
		if (this.gui.Button(new Vector2(26f, 392f), this.carrerOverall[2], this.carrerOverall[3], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.ACHIEVEMENTS;
		}
		this.gui.Picture(new Vector2(330f, 410f), this.carrerOverall[9]);
		this.gui.Picture(new Vector2(104f, 392f), this.carrerOverall[5]);
		this.gui.TextLabel(new Rect(25f, 392f, 326f, 22f), Language.CarrAchievementComplete, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(25f, 396f, 326f, 64f), AchievementsEngine.OpenedCount(this._info), 17, "#d6d6d6_Micra", TextAnchor.MiddleCenter, true);
		this.gui.Button(new Vector2(26f, 458f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.TextLabel(new Rect(25f, 458f, 326f, 22f), Language.CarrAchievementNext, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		int num5 = AchievementsEngine.Closest(this._info);
		string content = string.Empty;
		string content2 = string.Empty;
		if (num5 != -1)
		{
			content = Main.UserInfo.achievementsInfos[num5].name;
			content2 = "(" + Math.Round((double)(AchievementsEngine.Proc(this._info, num5) * 100f)) + "%)";
		}
		else
		{
			content = Language.NoSmall;
		}
		this.gui.TextLabel(new Rect(25f, 455f, 326f, 64f), content, 15, "#d6d6d6_Micra", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(25f, 475f, 326f, 64f), content2, 15, "#d6d6d6_Micra", TextAnchor.MiddleCenter, true);
		this.gui.Button(new Vector2(357f, 125f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.Picture(new Vector2(357f, 125f), this.carrerOverall[7]);
		this.gui.TextLabel(new Rect(357f, 125f, 326f, 22f), Language.CarrWTaskComplete, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(357f, 129f, 326f, 64f), this._info.WtaskOpenedCount, 17, "#d6d6d6_Micra", TextAnchor.MiddleCenter, true);
		this.gui.Button(new Vector2(357f, 191f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.Picture(new Vector2(412f, 210f), this.carrerOverall[8]);
		this.gui.TextLabel(new Rect(357f, 191f, 326f, 22f), Language.CarrContractsComplete, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		for (int i = 0; i < this.ContractStarsImgs.Length; i++)
		{
			int num6;
			switch (i)
			{
			case 0:
				if (this._info.contractsInfo.CurrentEasyCount >= this._info.contractsInfo.CurrentEasy.task_counter)
				{
					num6 = this._info.contractsInfo.CurrentEasyIndex + 1;
				}
				else
				{
					num6 = this._info.contractsInfo.CurrentEasyIndex;
				}
				break;
			case 1:
				if (this._info.contractsInfo.CurrentNormalCount >= this._info.contractsInfo.CurrentNormal.task_counter)
				{
					num6 = this._info.contractsInfo.CurrentNormalIndex + 1;
				}
				else
				{
					num6 = this._info.contractsInfo.CurrentNormalIndex;
				}
				break;
			case 2:
				if (this._info.contractsInfo.CurrentHardCount >= this._info.contractsInfo.CurrentHard.task_counter)
				{
					num6 = this._info.contractsInfo.CurrentHardIndex + 1;
				}
				else
				{
					num6 = this._info.contractsInfo.CurrentHardIndex;
				}
				break;
			default:
				num6 = 0;
				break;
			}
			this.gui.Picture(new Vector2((float)(450 + (this.ContractStarsImgs[0].width + 25) * i), 210f), this.ContractStarsImgs[i]);
			this.gui.TextLabel(new Rect((float)(453 + (this.ContractStarsImgs[0].width + 25) * i), 230f, 20f, 20f), num6.ToString(), 9, "#d1d8e4_Tahoma", TextAnchor.MiddleCenter, true);
		}
		this.gui.Button(new Vector2(357f, 258f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.TextLabel(new Rect(357f, 258f, 326f, 22f), Language.ClansName.ToLower(), 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		if (this._info.clanID != 0)
		{
			float num7 = MainGUI.Instance.CalcWidth(Helpers.GetTag(this._info.clanTag), MainGUI.Instance.fontMicra, 12);
			float num8 = num7 + MainGUI.Instance.CalcWidth(this._info.ClanName, MainGUI.Instance.fontDNC57, 16);
			float num9 = ((float)this.carrerOverall[2].width - num8) / 2f;
			this.gui.TextLabel(new Rect(357f + num9, 277f, 326f, 22f), Helpers.GetTag(this._info.clanTag), 12, Helpers.GetTagHexColor(this._info.clanTag) + "_M", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(357f + num7 + num9, 277f, 326f, 22f), this._info.ClanName, 16, "#FFFFFF_D", TextAnchor.MiddleLeft, true);
			string text = string.Concat(new object[]
			{
				"<color=#979797>",
				Language.Role,
				": </color>",
				this._info.sClanRole,
				"  <color=#979797>",
				Language.CarrPlace.ToLower(),
				": </color>",
				this._info.ClanPlace,
				" <color=#979797>",
				Language.Earn,
				": </color>",
				this._info.ClanEarnProc * 100f,
				"%"
			});
			float num10 = MainGUI.Instance.CalcWidth(text, MainGUI.Instance.fontDNC57, 14);
			float num11 = num10 + (float)this.expSmall.width + MainGUI.Instance.CalcWidth(Helpers.SeparateNumericString(this._info.ClanEarn.ToString()), MainGUI.Instance.fontDNC57, 14);
			float num12 = ((float)this.carrerOverall[2].width - num11) / 2f - 10f;
			this.gui.TextLabel(new Rect(367f + num12, 300f, 326f, 20f), text, 14, "#ffffff_D", TextAnchor.MiddleLeft, true);
			GUI.DrawTexture(new Rect(367f + num10 + num12, 304f, (float)this.expSmall.width, (float)this.expSmall.height), this.expSmall);
			this.gui.TextLabel(new Rect(370f + num10 + (float)this.expSmall.width + num12, 300f, 100f, 20f), Helpers.SeparateNumericString(this._info.ClanEarn.ToString("F0")), 14, "#ffffff_D", TextAnchor.MiddleLeft, true);
		}
		else
		{
			this.gui.TextLabel(new Rect(357f, 259f, 326f, 64f), Language.ClansNotInClan, 17, "#979797_D", TextAnchor.MiddleCenter, true);
		}
		this.gui.Button(new Vector2(357f, 326f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.TextLabel(new Rect(357f, 326f, 326f, 22f), Language.CarrSpecialBadges, 18, "#d6d6d6", TextAnchor.MiddleCenter, true);
		this.awardScroll.OnGUI();
		if (this.gui.TextButton(new Rect(355f, 340f, 40f, 40f), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Hover)
		{
			this.awardScroll.Left();
		}
		if (this.gui.TextButton(new Rect(650f, 340f, 40f, 40f), string.Empty, 24, "#ffffff", "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Hover)
		{
			this.awardScroll.Right();
		}
		GUI.DrawTexture(new Rect(357f, 57f, (float)this.infoBG.width, (float)this.infoBG.height), this.infoBG);
		GUI.DrawTexture(new Rect(364f, 64f, 50f, 50f), this.avatar);
		if (this.gui.Button(new Vector2(591f, 63f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrProfile, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			if (CVars.IsStandaloneRealm)
			{
				HtmlLayer.Request("?action=profileLink&user_id=" + this._info.userID, new RequestFinished(this.OpenProfileLink), new RequestFailed(this.OnGetProfileLinkFailed), string.Empty, string.Empty);
			}
			else if (!SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--mailru"))
			{
				if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--ok"))
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://www.odnoklassniki.ru/profile/" + this._info.socialID + "/"
					});
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--kg"))
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://www.kongregate.com/accounts/" + this._info.socialInfo.firstName + "/"
					});
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fb"))
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://facebook.com/" + this._info.socialID.Substring(2)
					});
				}
				else if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--fr"))
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://www.friendster.com/profiles/" + this._info.socialID.Substring(2)
					});
				}
				else
				{
					Application.ExternalCall("window.open", new object[]
					{
						"http://vkontakte.ru/id" + this._info.socialID
					});
				}
			}
		}
		float num13 = 0f;
		if (this._info.clanTag != string.Empty)
		{
			num13 = this.gui.CalcWidth(this._info.clanTag, this.gui.fontDNC57, 21);
			this.gui.TextField(new Rect(418f, 62f, 326f, 23f), this._info.clanTag, 21, "#d40000", TextAnchor.MiddleLeft, false, false);
		}
		this.gui.TextField(new Rect(418f + num13, 62f, 326f, 23f), this._info.nick, 21, this._info.nickColor, TextAnchor.MiddleLeft, false, false);
		this.showHCOverall = this.gui.HardcoreToggle(new Vector2(559f, 66f), this.showHCOverall);
		this.gui.TextLabel(new Rect(418f, 79f, 326f, 22f), this._info.socialInfo.firstName + " " + this._info.socialInfo.lastName, 15, "#999999", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(418f, 96f, 326f, 23f), "K:", 15, "#999999", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(430f, 96f, 326f, 23f), Helpers.SeparateNumericString(((!this.showHCOverall) ? this._info.killCount : this._info.hcKillCount).ToString()), 15, "#ffffff", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(482f, 96f, 326f, 23f), "D:", 15, "#999999", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(494f, 96f, 326f, 23f), Helpers.SeparateNumericString(((!this.showHCOverall) ? this._info.deathCount : this._info.hcDeathCount).ToString()), 15, "#ffffff", TextAnchor.MiddleLeft, true);
		float num14;
		if (!this.showHCOverall)
		{
			num14 = ((!(this._info.deathCount > 0)) ? ((float)this._info.killCount) : ((float)this._info.killCount / (float)this._info.deathCount));
		}
		else
		{
			num14 = ((!(this._info.hcDeathCount > 0)) ? ((float)this._info.hcKillCount) : ((float)this._info.hcKillCount / (float)this._info.hcDeathCount));
		}
		this.gui.TextLabel(new Rect(546f, 96f, 326f, 23f), "K/D:", 15, "#999999", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(570f, 96f, 326f, 23f), num14.ToString("F2"), 15, "#ffffff", TextAnchor.MiddleLeft, true);
		if (this._info.userID != Main.UserInfo.userID)
		{
			this.gui.VoteWidget(new Vector2(575f, 93f), this._info.userID, this._info.repa, -1);
			float width = (float)CWGUI.p.FavoriteButton.normal.background.width;
			float height = (float)CWGUI.p.FavoriteButton.normal.background.height;
			if (GUI.Button(new Rect(656f, 95f, width, height), string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(this._info.userID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
			{
				object[] args = new object[]
				{
					this._info.userID,
					this._info.PlayerLevel,
					(int)this._info.playerClass,
					this._info.clanTag,
					this._info.nick,
					this._info.socialInfo.firstName,
					this._info.socialInfo.lastName
				};
				WatchlistManager.AddRemovePlayer(args);
			}
		}
		else
		{
			this.gui.VoteWidget(new Vector2(575f, 93f), Main.UserInfo.userID.Value, this._info.repa.Value, -1);
		}
		if (this._info.banned != 0)
		{
			Int @int = this._info.bannedUntil - HtmlLayer.serverUtc;
			if (@int > 0 || this._info.bannedUntil < 0)
			{
				this.gui.Button(new Vector2(357f, 393f), this.carrerOverall[2], this.carrerOverall[2], null, string.Empty, 13, "#990000", TextAnchor.MiddleCenter, null, null, null, null);
				Rect position = new Rect(376f, 401f, (float)this.carrerOverall[10].width, (float)this.carrerOverall[10].height);
				GUI.DrawTexture(position, this.carrerOverall[10]);
				string content3 = (!(this._info.bannedUntil < 0)) ? MainGUI.Instance.SecondsTostringDDHHMMSS(@int) : "PERMANENT";
				this.gui.TextLabel(new Rect(447f, 435f, 150f, 23f), content3, 15, "#bb0000", TextAnchor.LowerCenter, true);
				if (!string.IsNullOrEmpty(this._info.bannedReason) && position.Contains(Event.current.mousePosition))
				{
					GUI.Label(new Rect((float)(326 + this.carrerOverall[10].width / 2), 385f, 100f, 10f), this._info.bannedReason, CWGUI.p.awardsStyle);
				}
			}
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x000340B8 File Offset: 0x000322B8
	private void OpenProfileLink(string text, string url)
	{
		Dictionary<string, object> dictionary = ArrayUtility.FromJSON(text, string.Empty);
		Application.OpenURL(dictionary["url"] as string);
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x000340E8 File Offset: 0x000322E8
	private void OnGetProfileLinkFailed(Exception e, string url = "")
	{
		Debug.LogError("cant get profile link");
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x000340F4 File Offset: 0x000322F4
	private void CarrierRating()
	{
		if (this._showWatchlist && !this._sortOrder.complete)
		{
			this.WatchlistSorting();
		}
		GUI.DrawTexture(new Rect(25f, 56f, (float)this.top_bg.width, (float)this.top_bg.height), this.top_bg);
		GUI.DrawTexture(new Rect(30f, 57f, (float)this.ratingRecord[15].width, (float)this.ratingRecord[15].height), this.ratingRecord[15]);
		this.searchTerm = this.gui.TextField(new Rect(38f, 56f, 180f, 25f), this.searchTerm, 15, "#FFFFFF", TextAnchor.MiddleCenter, true, true);
		if (this.searchTerm == string.Empty)
		{
			GUI.enabled = false;
		}
		if (GUI.Button(new Rect(221f, 56f, 32f, 24f), string.Empty, CarrierGUI.I.RatingStyles.RatingOnlineBtn))
		{
			this.top100type = 0;
			Main.AddDatabaseRequest<LoadRating>(new object[]
			{
				this.top100type,
				this.searchTerm,
				false,
				false,
				this.showHCOverall,
				this._isSeasonRating
			});
		}
		GUI.enabled = true;
		GUI.DrawTexture(new Rect(228f, 60f, (float)this.ratingRecord[17].width, (float)this.ratingRecord[17].height), this.ratingRecord[17]);
		CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 16;
		this._top100labelBuilder.Remove(0, this._top100labelBuilder.Length);
		this._top100labelBuilder.AppendLine((!this._isSeasonRating) ? this.top100 : (Language.Season + " " + this.top100));
		GUI.Label(new Rect(233f, 65f, 200f, 28f), this._top100labelBuilder.ToString(), CarrierGUI.I.RatingStyles.RatingLabel);
		CarrierGUI.I.RatingStyles.RatingLabel.fontSize = 20;
		float width = (float)this.gui.server_window[1].width;
		float height = (float)this.gui.server_window[1].height;
		int num = (!SingletoneComponent<Globals>.Instance.IsSeasonGoing) ? 438 : 403;
		Rect onHoverRect = new Rect((float)num, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnTop, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		if (this.gui.Button(new Vector2((float)num, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.top100type = 0;
			this.top100 = "ТОР 300";
			this._showWatchlist = false;
		}
		this.gui.TextLabel(new Rect((float)(num - 2), 58f, (float)this.gui.server_window[1].width, (float)this.gui.server_window[1].height), Language.CarrTOP, 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
		if (SingletoneComponent<Globals>.Instance.IsSeasonGoing)
		{
			onHoverRect.Set(438f, 57f, width, height);
			if (onHoverRect.Contains(Event.current.mousePosition))
			{
				Helpers.Hint(onHoverRect, Language.SeasonTop, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
			}
			if (this.gui.Button(new Vector2(438f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this._isSeasonRating = !this._isSeasonRating;
				Main.AddDatabaseRequest<LoadRating>(new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					false,
					this._isSeasonRating
				});
				this._showWatchlist = false;
			}
			int num2 = (!this._isSeasonRating) ? 19 : 20;
			this.gui.Picture(new Vector2(445f, 60f), this.ratingRecord[num2]);
		}
		onHoverRect.Set(473f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnHardcore, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		this.showHCOverall = this.gui.HardcoreToggle(new Vector2(473f, 57f), this.showHCOverall, ref this.forceReloadrating, ref this.forceReloadHCRating);
		if (this.forceReloadHCRating)
		{
			this.HCRatingSuffix = " HC";
			this.top100type = 0;
			Main.AddDatabaseRequest<LoadRating>(new object[]
			{
				this.top100type,
				string.Empty,
				false,
				false,
				true,
				this._isSeasonRating
			});
			this.forceReloadHCRating = false;
		}
		if (this.forceReloadrating)
		{
			this.HCRatingSuffix = string.Empty;
			this.top100type = 0;
			Main.AddDatabaseRequest<LoadRating>(new object[]
			{
				this.top100type,
				string.Empty,
				false,
				false,
				false,
				this._isSeasonRating
			});
			this.top100 = "ТОР 300";
			this.ratingLoaded = true;
			this.forceReloadrating = false;
			this.HCRatingSuffix = string.Empty;
		}
		onHoverRect.Set(508f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnTopOnline, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		if (this.gui.Button(new Vector2(508f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.top100 = "ТОР ONLINE";
			this.top100type = 0;
			Main.AddDatabaseRequest<LoadRating>(new object[]
			{
				this.top100type,
				string.Empty,
				false,
				true,
				false,
				this._isSeasonRating
			});
			this.showHCOverall = false;
			this._showWatchlist = false;
		}
		this.gui.Picture(new Vector2(513f, 59f), this.ratingRecord[14]);
		onHoverRect.Set(542f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnTopFriends, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		if (this.gui.Button(new Vector2(542f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.top100type = 0;
			Main.AddDatabaseRequest<LoadRating>(new object[]
			{
				this.top100type,
				string.Empty,
				true,
				false,
				false,
				this._isSeasonRating
			});
			this.top100 = Language.CarrTopFriends;
			this.showHCOverall = false;
			this._showWatchlist = false;
		}
		this.gui.Picture(new Vector2(548f, 62f), this.ratingRecord[16]);
		onHoverRect.Set(576f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnTopYourPosition, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		if (this.gui.Button(new Vector2(576f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.scrollPos.y = (float)((this._info.ratingPlace - 1) * 40);
			this._showWatchlist = false;
		}
		this.gui.Picture(new Vector2(588f, 61f), this.ratingRecord[18]);
		onHoverRect.Set(611f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnFavorites, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
		}
		if (this.gui.Button(new Vector2(611f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				return;
			}
			if (this._watchlistLoaded)
			{
				this._showWatchlist = true;
				this.top100 = Language.HintRatingBtnFavorites.ToUpper();
				return;
			}
			EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.WatchlistLoadingTitle, Language.WatchlistLoadingBody, PopupState.progress, false, false, string.Empty, string.Empty));
			Main.AddDatabaseRequestCallBack<LoadWatchlist>(delegate
			{
				EventFactory.Call("HidePopup", new Popup(WindowsID.LoadRating, Language.WatchlistLoadingTitle, Language.WatchlistLoadedBody, PopupState.progress, false, false, string.Empty, string.Empty));
				this.GenerateWatchlist();
				this._showWatchlist = true;
				this._watchlistLoaded = true;
				this.top100 = Language.HintRatingBtnFavorites.ToUpper();
			}, delegate
			{
			}, new object[0]);
		}
		GUI.DrawTexture(new Rect(615f, 58f, (float)CWGUI.p.FavoriteIcon.width, (float)CWGUI.p.FavoriteIcon.height), CWGUI.p.FavoriteIcon);
		onHoverRect.Set(652f, 57f, width, height);
		if (onHoverRect.Contains(Event.current.mousePosition))
		{
			Helpers.Hint(onHoverRect, Language.HintRatingBtnRefresh, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Rigth, 0f, 0f);
		}
		GUI.enabled = (this._ratingRefreshCooldown + 5f < Time.realtimeSinceStartup);
		if (this.gui.Button(new Vector2(652f, 57f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._ratingRefreshCooldown = Time.realtimeSinceStartup;
			if (this._showWatchlist)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.LoadRating, Language.WatchlistLoadingTitle, Language.WatchlistLoadingBody, PopupState.progress, false, false, string.Empty, string.Empty));
				Main.AddDatabaseRequestCallBack<LoadWatchlist>(delegate
				{
					EventFactory.Call("HidePopup", new Popup(WindowsID.LoadRating, Language.WatchlistLoadingTitle, Language.WatchlistLoadedBody, PopupState.progress, false, false, string.Empty, string.Empty));
					this.GenerateWatchlist();
				}, delegate
				{
				}, new object[0]);
			}
			else
			{
				this.top100type = 0;
				Main.AddDatabaseRequest<LoadRating>(new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					this.showHCOverall,
					this._isSeasonRating
				});
				this.top100 = "ТОР 300";
			}
		}
		GUI.enabled = true;
		Color color = this.gui.color;
		if (this._ratingRefreshCooldown > 0f)
		{
			this.gui.color = new Color(1f, (Time.realtimeSinceStartup - this._ratingRefreshCooldown) / 5f, (Time.realtimeSinceStartup - this._ratingRefreshCooldown) / 5f);
		}
		this.gui.Picture(new Vector2(659f, 61f), this.gui.server_window[9]);
		this.gui.color = color;
		this.gui.TextLabel(new Rect(65f, 80f, 70f, 25f), Language.CarrPlace, 18, "#9d9d9d", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(120f, 80f, 35f, 25f), Language.CarrLVL, 18, "#9d9d9d", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(148f, 80f, 70f, 25f), Language.CarrItemName, 18, "#9d9d9d", TextAnchor.UpperLeft, true);
		if (this.gui.TextButton(new Rect(306f, 80f, 70f, 25f), Language.CarrPoints, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleLeft, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				this.SetSort(CarrierGUI.WatchlistSortingBy.Exp);
			}
			else
			{
				this.top100type = 1;
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					this.showHCOverall,
					this._isSeasonRating
				});
				this.top100 = Language.CarrTop100EXP + this.HCRatingSuffix;
			}
		}
		if (this.gui.TextButton(new Rect(377f, 80f, 70f, 25f), Language.CarrKills, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleLeft, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				this.SetSort(CarrierGUI.WatchlistSortingBy.Kill);
			}
			else
			{
				this.top100type = 2;
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					this.showHCOverall,
					this._isSeasonRating
				});
				this.top100 = Language.CarrTop100Kills + this.HCRatingSuffix;
			}
		}
		if (!this._isSeasonRating && this.gui.TextButton(new Rect(447f, 80f, 70f, 25f), Language.CarrDeath, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleLeft, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				this.SetSort(CarrierGUI.WatchlistSortingBy.Death);
			}
			else
			{
				this.top100type = 3;
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					this.showHCOverall,
					this._isSeasonRating
				});
				this.top100 = Language.CarrTop100Death + this.HCRatingSuffix;
			}
		}
		int num3 = (!this._isSeasonRating) ? 517 : 457;
		if (this.gui.TextButton(new Rect((float)num3, 80f, 70f, 25f), "K/D", 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleLeft, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				this.SetSort(CarrierGUI.WatchlistSortingBy.Kd);
			}
			else
			{
				this.top100type = 4;
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					this.showHCOverall,
					this._isSeasonRating
				});
				this.top100 = Language.CarrTop100KD + this.HCRatingSuffix;
			}
		}
		if (this._isSeasonRating)
		{
			Rect rect = new Rect(560f, 80f, 90f, 25f);
			string text = Language.SeasonReward.Substring(0, Language.SeasonReward.IndexOf(' '));
			this.gui.TextField(rect, text, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
			rect.y = 93f;
			string text2 = Language.SeasonReward.Substring(Language.SeasonReward.IndexOf(' ') + 1);
			this.gui.TextField(rect, text2, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		}
		else if (this.gui.TextButton(new Rect(565f, 80f, 90f, 25f), Language.CarrReputation, 18, "#9d9d9d", "#FFFFFF", TextAnchor.MiddleLeft, null, null, null, null).Clicked)
		{
			if (this._showWatchlist)
			{
				this.SetSort(CarrierGUI.WatchlistSortingBy.Reputation);
			}
			else
			{
				this.top100type = 5;
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					false,
					this._isSeasonRating
				});
				this.top100 = Language.CarrTop100Rep;
				this.showHCOverall = false;
			}
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if (this._showWatchlist)
		{
			this.WatchList.OnGUI();
		}
		else
		{
			this.TopRatingList.OnGUI();
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x000355B8 File Offset: 0x000337B8
	private void CarrierAchievementsHint()
	{
		if (this.lastIndex != -1)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.popup.visibility);
			if (this.mousePos.x >= (float)(Screen.width / 2) && this.mousePos.y >= (float)(Screen.height / 2))
			{
				this.gui.BeginGroup(new Rect(this.mousePos.x - (float)this.popup_background.width, (float)Screen.height - this.mousePos.y, (float)this.popup_background.width, (float)this.popup_background.height));
			}
			else if (this.mousePos.x <= (float)(Screen.width / 2) && this.mousePos.y >= (float)(Screen.height / 2))
			{
				this.gui.BeginGroup(new Rect(this.mousePos.x, (float)Screen.height - this.mousePos.y, (float)this.popup_background.width, (float)this.popup_background.height));
			}
			else if (this.mousePos.x >= (float)(Screen.width / 2) && this.mousePos.y <= (float)(Screen.height / 2))
			{
				this.gui.BeginGroup(new Rect(this.mousePos.x - (float)this.popup_background.width, (float)Screen.height - this.mousePos.y - (float)this.popup_background.height, (float)this.popup_background.width, (float)this.popup_background.height));
			}
			else
			{
				this.gui.BeginGroup(new Rect(this.mousePos.x, (float)Screen.height - this.mousePos.y - (float)this.popup_background.height, (float)this.popup_background.width, (float)this.popup_background.height));
			}
			this.gui.Picture(new Vector2(0f, 0f), this.popup_background);
			this.gui.Picture(new Vector2(11f, 11f), this.popup_title);
			this.gui.PictureCentered(new Vector2(170f, 163f), this.achievementsBig[this.lastIndex], Vector2.one);
			this.gui.TextLabel(new Rect(25f, 12f, 300f, 50f), this._info.achievementsInfos[this.lastIndex].name, 19, "#c9c9c9", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect(-105f, 12f, 400f, 50f), Helpers.SeparateNumericString(this._info.achievementsInfos[this.lastIndex].prize.ToString()), 19, "#ffa200", TextAnchor.UpperRight, true);
			this.gui.TextLabel(new Rect(-34f, 300f, 400f, 50f), this._info.achievementsInfos[this.lastIndex].description, 16, "#c9c9c9", TextAnchor.UpperCenter, true);
			float num = (float)this._info.achievementsInfos[this.lastIndex].current;
			float num2 = (float)this._info.achievementsInfos[this.lastIndex].count;
			if (this._info.achievementsInfos[this.lastIndex].type == AchievementType.onlineTime)
			{
				num = (float)(this._info.userStats.timeOnline / 3600);
				num2 = (float)(this._info.achievementsInfos[this.lastIndex].count / 3600);
			}
			if (this._info.achievementsInfos[this.lastIndex].type == AchievementType.wtaskComplete)
			{
				num = Mathf.Min((float)this._info.WtaskOpenedCount, num2);
			}
			if (num > num2)
			{
				num = num2;
			}
			string content = num.ToString("F0") + "/" + num2.ToString("F0");
			float proc = num / num2;
			if (this._info.achievementsInfos[this.lastIndex].current != this._info.achievementsInfos[this.lastIndex].count)
			{
				this.gui.ProgressDoubleTextured(new Vector2(20f, 336f), 300f, proc, this.gui.wtask_icon[1], this.gui.wtask_icon[2], this.gui.wtask_icon[4], this.gui.wtask_icon[5]);
				this.gui.TextLabel(new Rect(-4f, 331f, (float)this.gui.wtask_popup.width, 20f), content, 14, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(-5f, 330f, (float)this.gui.wtask_popup.width, 20f), content, 14, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(-5f, 331f, (float)this.gui.wtask_popup.width, 20f), content, 14, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			else
			{
				this.gui.ProgressDoubleTextured(new Vector2(20f, 336f), 300f, 1f, this.gui.wtask_icon[1], this.gui.wtask_icon[2], this.gui.wtask_icon[4], this.gui.wtask_icon[5]);
				this.gui.TextLabel(new Rect(-4f, 331f, (float)this.gui.wtask_popup.width, 20f), Language.Completed + "!", 14, "#000000_Micra", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(-5f, 331f, (float)this.gui.wtask_popup.width, 20f), Language.Completed + "!", 14, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			this.gui.Picture(new Vector2(297f, 13f), this.gui.crIcon);
			this.gui.EndGroup();
			this.lastIndex = -1;
		}
		else
		{
			this.popup.Hide(0.5f, 0f);
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00035C80 File Offset: 0x00033E80
	private void CarrierContracts(OverviewInfo userInfo)
	{
		Rect rect = new Rect(21f, 52f, 665f, 470f);
		Rect rect2 = new Rect(0f, 0f, rect.width, rect.height);
		this.gui.BeginGroup(rect);
		this.gui.Picture(new Vector2((float)((this.Class_Button[0].width - this.Class_Button[3].width) / 2), (float)((this.Class_Button[3].height - this.Class_Button[0].height) / 2 - 2)), this.Class_Button[3]);
		this.gui.Button(new Vector2(0f, -2f), this.Class_Button[0], this.Class_Button[1], this.Class_Button[2], Language.CarrDaily, 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null);
		this.gui.Picture(new Vector2(rect2.center.x - (float)(this.stats[1].width / 2), 0f), this.stats[1]);
		this.gui.Picture(new Vector2(2f, 35f), this.ContractImgs[2]);
		this.gui.Picture(new Vector2(rect2.center.x - (float)(this.ContractImgs[3].width / 2), 35f), this.ContractImgs[3]);
		if (Main.UserInfo.socialID == userInfo.socialID)
		{
			this.gui.TextLabel(new Rect(rect2.center.x - 80f, 31f, 190f, 100f), this.gui.SecondsToStringHHHMMSS((Main.UserInfo.contractsInfo.DeltaTime < 0) ? 0 : Main.UserInfo.contractsInfo.DeltaTime), 22, "#FFFFFF_Micra", TextAnchor.UpperLeft, true);
		}
		if (PopupGUI.IsAnyPopupShow || Main.IsGameLoaded)
		{
			GUI.enabled = false;
		}
		if (userInfo.userID == Main.UserInfo.userID && GUI.Button(new Rect(458f, 36f, (float)this.UpdateContractBtn.normal.background.width, (float)this.UpdateContractBtn.normal.background.height), string.Empty, this.UpdateContractBtn))
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.UpdateContracts, Language.CarrUpdateContractsPopupHeader, string.Empty, PopupState.updateContracts, false, true, string.Empty, string.Empty));
		}
		GUI.enabled = true;
		this.gui.TextLabel(new Rect(4f, 35f, 200f, 50f), Language.CarrCurrentContractsCAPS, 16, "#92c4fe", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(rect2.xMax - 205f, 35f, 200f, 50f), Language.CarrContractRefreshDescr0, 16, "#dddddd", TextAnchor.UpperRight, true);
		this.gui.Picture(new Vector2(2f, 315f), this.ContractImgs[2]);
		this.gui.TextLabel(new Rect(4f, 315f, 200f, 100f), Language.CarrNextContractsCAPS, 16, "#92c4fe", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(65f, 410f, 530f, 25f), Language.CarrContractRefreshDescr1, 15, "#FFFFFF", TextAnchor.UpperCenter, true);
		this.gui.TextLabel(new Rect(65f, 430f, 530f, 25f), Language.CarrContractRefreshDescr2, 15, "#FFFFFF", TextAnchor.UpperCenter, true);
		this.gui.TextLabel(new Rect(65f, 450f, 530f, 25f), Language.CarrContractRefreshDescr3, 15, "#FFFFFF", TextAnchor.UpperCenter, true);
		for (int i = 0; i < 3; i++)
		{
			this.gui.BeginGroup(new Rect((float)(50 + i * 195), 55f, 195f, 355f));
			this.CarrierOneContractGUI(userInfo, i, this.GetContract(userInfo, i), this.GetContractID(userInfo, i));
			this.gui.EndGroup();
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x00036124 File Offset: 0x00034324
	private void CarrierAchivements()
	{
		this.gui.Picture(new Vector2(663f, 55f), this.statsScrollbarBg);
		int num = 1676;
		if (this.ach_scrollPos.y > (float)num)
		{
			this.scrollPos.y = (float)num;
		}
		this.ach_scrollPos = this.gui.BeginScrollView(new Rect(23f, 58f, 657f, 458f), this.ach_scrollPos, new Rect(0f, 0f, 200f, (float)num), float.MaxValue);
		this.AchievementDraw(new Vector2(2f, 0f), 28);
		this.AchievementDraw(new Vector2(128f, 0f), 29);
		this.AchievementDraw(new Vector2(254f, 0f), 30);
		this.AchievementDraw(new Vector2(380f, 0f), 31);
		this.AchievementDraw(new Vector2(506f, 0f), 32);
		this.AchievementDraw(new Vector2(2f, 61f), 33);
		this.AchievementDraw(new Vector2(128f, 61f), 34);
		this.AchievementDraw(new Vector2(254f, 61f), 35);
		this.AchievementDraw(new Vector2(380f, 61f), 36);
		this.AchievementDraw(new Vector2(506f, 61f), 37);
		this.AchievementDraw(new Vector2(2f, 123f), 38);
		this.AchievementDraw(new Vector2(128f, 123f), 39);
		this.AchievementDraw(new Vector2(254f, 123f), 40);
		this.AchievementDraw(new Vector2(380f, 123f), 41);
		this.AchievementDraw(new Vector2(506f, 123f), 42);
		this.AchievementDraw(new Vector2(2f, 185f), 43);
		this.AchievementDraw(new Vector2(128f, 185f), 44);
		this.AchievementDraw(new Vector2(254f, 185f), 45);
		this.AchievementDraw(new Vector2(380f, 185f), 46);
		this.AchievementDraw(new Vector2(506f, 185f), 47);
		this.AchievementDraw(new Vector2(193f, 247f), 48);
		this.AchievementDraw(new Vector2(321f, 247f), 49);
		this.gui.BeginGroup(new Rect(0f, 302f, 657f, 1361f));
		this.AchievementDraw(new Vector2(14f, 0f), 0);
		this.AchievementDraw(new Vector2(259f, 12f), 1);
		this.AchievementDraw(new Vector2(482f, 32f), 2);
		this.AchievementDraw(new Vector2(243f, 857f), 3);
		this.AchievementDraw(new Vector2(110f, 144f), 4);
		this.AchievementDraw(new Vector2(377f, 23f), 5);
		this.AchievementDraw(new Vector2(327f, 130f), 6);
		this.AchievementDraw(new Vector2(-3f, 282f), 7);
		this.AchievementDraw(new Vector2(410f, 352f), 8);
		this.AchievementDraw(new Vector2(513f, 244f), 9);
		this.AchievementDraw(new Vector2(116f, 362f), 10);
		this.AchievementDraw(new Vector2(277f, 753f), 11);
		this.AchievementDraw(new Vector2(154f, 753f), 12);
		this.AchievementDraw(new Vector2(398f, 753f), 13);
		this.AchievementDraw(new Vector2(231f, 664f), 14);
		this.AchievementDraw(new Vector2(9f, 505f), 15);
		this.AchievementDraw(new Vector2(524f, 503f), 16);
		this.AchievementDraw(new Vector2(270f, 422f), 17);
		this.AchievementDraw(new Vector2(247f, 227f), 18);
		this.AchievementDraw(new Vector2(110f, 10f), 19);
		this.AchievementDraw(new Vector2(281f, 980f), 20);
		this.AchievementDraw(new Vector2(546f, 156f), 21);
		this.AchievementDraw(new Vector2(19f, 162f), 22);
		this.AchievementDraw(new Vector2(128f, 886f), 23);
		this.AchievementDraw(new Vector2(447f, 891f), 24);
		this.AchievementDraw(new Vector2(459f, 638f), 25);
		this.AchievementDraw(new Vector2(37f, 638f), 26);
		this.AchievementDraw(new Vector2(247f, 559f), 27);
		this.AchievementDraw(new Vector2(136f, 254f), 50);
		this.AchievementDraw(new Vector2(404f, 252f), 51);
		this.AchievementDraw(new Vector2(393f, 488f), 52);
		this.AchievementDraw(new Vector2(125f, 488f), 53);
		this.AchievementDraw(new Vector2(10f, 732f), 54);
		this.AchievementDraw(new Vector2(501f, 753f), 55);
		this.AchievementDraw(new Vector2(2f, 982f), 56);
		this.AchievementDraw(new Vector2(379f, 986f), 57);
		this.AchievementDraw(new Vector2(182f, 1111f), 58);
		this.AchievementDraw(new Vector2(332f, 1124f), 59);
		this.AchievementDraw(new Vector2(17f, 1237f), 60);
		this.AchievementDraw(new Vector2(142f, 1242f), 61);
		this.AchievementDraw(new Vector2(261f, 1242f), 62);
		this.AchievementDraw(new Vector2(381f, 1242f), 63);
		this.AchievementDraw(new Vector2(513f, 1235f), 64);
		this.gui.EndGroup();
		this.gui.EndScrollView();
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x000367CC File Offset: 0x000349CC
	private void CarrierSkills()
	{
		if (this.Selected_prof == 5)
		{
			this.gui.Picture(new Vector2((float)(this.Sk_x + 3 + this.SKB_width * 0), (float)(this.Sk_y + this.Class_Button[0].height / 2)), this.Class_Button[3]);
		}
		else
		{
			this.gui.Picture(new Vector2((float)(this.Sk_x + 3 + this.SKB_width * (this.Selected_prof + 1)), (float)(this.Sk_y + this.Class_Button[0].height / 2)), this.Class_Button[3]);
		}
		this.gui.skin.button.active.textColor = Color.black;
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 1), (float)this.Sk_y), (this.Selected_prof != 0) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], this.ClassName[0], 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 0)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 0;
		}
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 2), (float)this.Sk_y), (this.Selected_prof != 1) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], Language.CarrStormtrooper + " ", 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 1)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 1;
		}
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 3), (float)this.Sk_y), (this.Selected_prof != 2) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], Language.CarrDestroyer, 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 2)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 2;
		}
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 4), (float)this.Sk_y), (this.Selected_prof != 3) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], Language.CarrSniper + " ", 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 3)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 3;
		}
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 5), (float)this.Sk_y), (this.Selected_prof != 4) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], Language.CarrArmorer + " ", 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 4)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 4;
		}
		if (this.gui.Button(new Vector2((float)(this.Sk_x + this.SKB_width * 0), (float)this.Sk_y), (this.Selected_prof != 5) ? this.Class_Button[0] : this.Class_Button[2], this.Class_Button[1], this.Class_Button[2], Language.CarrCareerist + " ", 12, "#000000", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_prof != 5)
		{
			this.Selected_skill[0] = -1;
			this.Selected_skill[1] = -1;
			this.Selected_prof = 5;
		}
		this.gui.skin.button.active.textColor = Color.white;
		if (this.Selected_prof < this.Class_ICON.Length)
		{
			this.gui.Picture(new Vector2(30f, 80f), this.Class_ICON[this.Selected_prof]);
		}
		this.gui.Picture(new Vector2((float)this.SKW_x, (float)this.SKW_y), this.Class_backgr_balance[0]);
		this.gui.TextLabel(new Rect((float)(this.SKW_x + this.Class_backgr_balance[0].width / 2 - 40), (float)(this.SKW_y + 3), 100f, 50f), Language.CarrCurrentBalance, 14, "#FFFFFF", TextAnchor.UpperLeft, true);
		if (this.gui.Button(new Vector2((float)(this.SKW_x + 202), (float)(this.SKW_y + 4)), this.Class_backgr_balance[1], this.Class_backgr_balance[2], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && Main.UserInfo.userID == this._info.userID)
		{
			EventFactory.Call("ShowBankGUI", null);
		}
		this.gui.Picture(new Vector2((float)(this.SKW_x + 10), (float)(this.SKW_y + 31)), this.Class_backgr_balance[6]);
		this.gui.TextLabel(new Rect((float)(this.SKW_x - 25), (float)(this.SKW_y + 28), 100f, 50f), (this._info.SP == null) ? "-" : this._info.SP.ToString(), 37, "#FFFFFF", TextAnchor.MiddleRight, true);
		this.gui.Picture(new Vector2((float)(this.SKW_x + 80), (float)(this.SKW_y + 33)), this.Class_backgr_balance[3]);
		this.gui.TextLabel(new Rect((float)(this.SKW_x + 107), (float)(this.SKW_y + 18), 90f, 50f), Helpers.SeparateNumericString(this._info.CR.ToString()), 19, "#FFFFFF", TextAnchor.MiddleRight, true);
		this.gui.TextLabel(new Rect((float)(this.SKW_x + 107), (float)(this.SKW_y + 38), 90f, 50f), Helpers.SeparateNumericString(this._info.GP.ToString()), 19, "#FFFFFF", TextAnchor.MiddleRight, true);
		this.gui.Picture(new Vector2((float)(this.SKW_x + 200), (float)(this.SKW_y + 31)), this.Class_backgr_balance[4]);
		this.gui.Picture(new Vector2((float)(this.SKW_x + 201), (float)(this.SKW_y + 52)), this.Class_backgr_balance[5]);
		int num = 10 + this.Class_backgr_balance[0].height;
		this.gui.Picture(new Vector2((float)this.SKW_x, (float)(this.SKW_y + num)), this.Class_backgr_progress[0]);
		if (!Main.IsGameLoaded && Main.UserInfo.userID == this._info.userID && this.gui.Button(new Vector2((float)(this.SKW_x + 202), (float)(this.SKW_y + 103)), this.Skills_reset_button[0], this.Skills_reset_button[1], this.Skills_reset_button[1], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.CarrResetSkills, string.Empty, PopupState.resetSkills, false, true, string.Empty, string.Empty));
			this.RecalcProgress();
		}
		this.gui.TextLabel(new Rect((float)(this.SKW_x + 90), (float)(this.SKW_y + 3 + num), 75f, 50f), Language.CarrBootSkills, 14, "#FFFFFF", TextAnchor.UpperLeft, true);
		for (int i = 0; i < this.ClassName.Length - 1; i++)
		{
			bool flag = this.currentPlayerClass == this.TranslatePlayerClass(i);
			if (!flag)
			{
				this.gui.TextLabel(new Rect((float)(this.SKW_x + 10), (float)(this.SKW_y + 23 + 21 * i + num), 100f, 50f), this.ClassName[i], 12, "#999999", TextAnchor.UpperLeft, true);
			}
			else
			{
				this.gui.TextLabel(new Rect((float)(this.SKW_x + 10), (float)(this.SKW_y + 23 + 21 * i + num), 100f, 50f), this.ClassName[i], 12, "#FFFFFF", TextAnchor.UpperLeft, true);
			}
			if (flag)
			{
				this.gui.Picture(new Vector2((float)(this.SKW_x + 4), (float)(this.SKW_y + 25 + 21 * i + num)), this.Class_backgr_progress[3]);
			}
			this.gui.Picture(new Vector2((float)(this.SKW_x + 75), (float)(this.SKW_y + 29 + 21 * i + num)), this.Class_backgr_progress[1]);
			this.gui.BeginGroup(new Rect((float)(this.SKW_x + 75), (float)(this.SKW_y + 29 + 21 * i + num), (float)(3 * this.GetProgress(this.TranslatePlayerClass(i))), (float)this.Class_backgr_progress[2].height));
			this.gui.Picture(new Vector2(0f, 0f), this.Class_backgr_progress[2]);
			this.gui.EndGroup();
		}
		int num2 = this.Class_backgr_balance[0].height + this.Class_backgr_progress[0].height;
		this.gui.Picture(new Vector2((float)(this.SKW_x - 59), (float)(this.SKW_y + 20 + num2)), this.Class_backgr_description[0]);
		string text = (!Application.isEditor) ? string.Empty : string.Concat(new object[]
		{
			" ",
			(Skills)this.GetSelectedSkill(),
			" №:",
			this.GetSelectedSkill()
		});
		if (Application.isEditor)
		{
			this.gui.TextLabel(new Rect((float)(this.SKW_x + 10 + 3), (float)(this.SKW_y + 115 + num2), 200f, 20f), Language.CarrBonus + text, 14, "#7EFF00", TextAnchor.UpperLeft, true);
		}
		else
		{
			this.gui.TextLabel(new Rect((float)(this.SKW_x + 10 + 3), (float)(this.SKW_y + 115 + num2), 200f, 20f), Language.CarrBonus, 14, "#7EFF00", TextAnchor.UpperLeft, true);
		}
		if (this.Selected_skill[0] != -1 && this.Selected_skill[1] != -1)
		{
			this.gui.Picture(new Vector2((float)(this.SKW_x + 186), (float)(this.SKW_y + 124 + num2)), this.GetSelectedIcon());
		}
		if (this.GetSelectedSkill() != -1)
		{
			if (this._info.skillsInfos[this.GetSelectedSkill()].name != null)
			{
				if (this._info.skillsInfos[this.GetSelectedSkill()].name[0] == '!')
				{
					text = this._info.skillsInfos[this.GetSelectedSkill()].name;
					this.gui.TextLabel(new Rect((float)(this.SKW_x + 10), (float)(this.SKW_y + 8 + num2), 220f, 50f), text.Substring(1, text.Length - 1).ToUpper(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
				}
				else
				{
					this.gui.TextLabel(new Rect((float)(this.SKW_x + 10), (float)(this.SKW_y + 8 + num2), 220f, 50f), this._info.skillsInfos[this.GetSelectedSkill()].name.ToUpper(), 14, "#FFFFFF", TextAnchor.MiddleCenter, true);
				}
			}
			if (this._info.skillsInfos[this.GetSelectedSkill()].function != null)
			{
				this.gui.TextLabel(new Rect((float)(this.SKW_x + 10 + 3), (float)(this.SKW_y + 44 + num2), 218f, 80f), this._info.skillsInfos[this.GetSelectedSkill()].function, 16, "#999999", TextAnchor.UpperLeft, true);
			}
			if (this._info.skillsInfos[this.GetSelectedSkill()].bonus != null)
			{
				this.gui.TextLabel(new Rect((float)(this.SKW_x + 10 + 3), (float)(this.SKW_y + 132 + num2), 175f, 60f), this._info.skillsInfos[this.GetSelectedSkill()].bonus, 14, "#FFFFFF", TextAnchor.UpperLeft, true);
			}
			if (this._info.skillsInfos[this.GetSelectedSkill()].Unlocked)
			{
				if (this._info.skillsInfos[this.GetSelectedSkill()].isPremium)
				{
					if (this._info.skillsInfos[this.GetSelectedSkill()].rentEnd > 0)
					{
						int seconds = this._info.skillsInfos[this.GetSelectedSkill()].rentEnd - HtmlLayer.serverUtc;
						this.gui.TextLabel(new Rect((float)(this.SKW_x + 135), (float)(this.SKW_x + 40), 100f, 20f), this.gui.SecondsToStringHHHMMSS(seconds), 13, "#ffffff", TextAnchor.MiddleCenter, true);
						this.gui.Picture(new Vector2((float)(this.SKW_x + 135), (float)(this.SKW_x + 35)), this.stats[1]);
						this.gui.TextLabel(new Rect((float)this.SKW_x, (float)(this.SKW_x + 40), 100f, 20f), Language.CarrRentTime, 13, "#ffffff", TextAnchor.MiddleCenter, true);
					}
					else
					{
						this.gui.TextLabel(new Rect((float)this.SKW_x, (float)(this.SKW_x + 40), 100f, 20f), Language.CarrUnlocked, 13, "#ffffff", TextAnchor.MiddleCenter, true);
					}
				}
				else
				{
					this.gui.TextLabel(new Rect((float)this.SKW_x, (float)(this.SKW_x + 40), 100f, 20f), Language.CarrUnlocked, 13, "#ffffff", TextAnchor.MiddleCenter, true);
				}
			}
			else
			{
				this.gui.TextLabel(new Rect((float)(this.SKW_x + 10), (float)(this.SKW_y + 176 + num2), 100f, 50f), Language.Cost, 14, "#92C5FF", TextAnchor.UpperLeft, true);
				if (this._info.skillsInfos[this.GetSelectedSkill()].rentTime == null)
				{
					this.gui.Picture(new Vector2((float)(this.SKW_x + 35), (float)(this.SKW_y + 208 + num2)), this.Class_backgr_description[1]);
					this.gui.Picture(new Vector2((float)(this.SKW_x + 113), (float)(this.SKW_y + 198 + num2)), this.Class_backgr_description[2]);
					this.gui.Picture(new Vector2((float)(this.SKW_x + 114), (float)(this.SKW_y + 219 + num2)), this.Class_backgr_description[3]);
					if (this._info.SP != null)
					{
						this.gui.TextLabel(new Rect((float)(this.SKW_x - 65), (float)(this.SKW_y + 197 + num2), 100f, 50f), this._info.skillsInfos[this.GetSelectedSkill()].SP.ToString(), 30, (!(this._info.SP < this._info.skillsInfos[this.GetSelectedSkill()].SP)) ? "#FFFFFF" : "#FF0000", TextAnchor.MiddleRight, true);
					}
					if (this._info.CR < this._info.skillsInfos[this.GetSelectedSkill()].CR)
					{
						this.gui.TextLabel(new Rect((float)(this.SKW_x + 61), (float)(this.SKW_y + 200 + num2), 50f, 20f), Helpers.SeparateNumericString(this._info.skillsInfos[this.GetSelectedSkill()].CR.ToString()), 16, "#FF0000", TextAnchor.MiddleRight, true);
					}
					else
					{
						this.gui.TextLabel(new Rect((float)(this.SKW_x + 61), (float)(this.SKW_y + 200 + num2), 50f, 20f), Helpers.SeparateNumericString(this._info.skillsInfos[this.GetSelectedSkill()].CR.ToString()), 16, "#FFFFFF", TextAnchor.MiddleRight, true);
					}
					if (this._info.GP < this._info.skillsInfos[this.GetSelectedSkill()].GP)
					{
						this.gui.TextLabel(new Rect((float)(this.SKW_x + 61), (float)(this.SKW_y + 220 + num2), 50f, 20f), Helpers.SeparateNumericString(this._info.skillsInfos[this.GetSelectedSkill()].GP.ToString()), 16, "#FF0000", TextAnchor.MiddleRight, true);
					}
					else
					{
						this.gui.TextLabel(new Rect((float)(this.SKW_x + 61), (float)(this.SKW_y + 220 + num2), 50f, 20f), Helpers.SeparateNumericString(this._info.skillsInfos[this.GetSelectedSkill()].GP.ToString()), 16, "#FFFFFF", TextAnchor.MiddleRight, true);
					}
				}
				else if (this._info.skillsInfos[this.GetSelectedSkill()].rentPrice.Length > 0)
				{
					bool isPremium = this._info.skillsInfos[this.GetSelectedSkill()].isPremium;
					int num3 = this._info.skillsInfos[this.GetSelectedSkill()].rentPrice.Length - 1;
					this.gui.Picture(new Vector2((float)(this.SKW_x + 113), (float)(this.SKW_y + 210 + num2)), (!isPremium) ? this.gui.crIcon : this.gui.gldIcon);
					string content = this._info.skillsInfos[this.GetSelectedSkill()].rentPrice[0] + " - " + this._info.skillsInfos[this.GetSelectedSkill()].rentPrice[num3];
					if (this._info.skillsInfos[this.GetSelectedSkill()].rentPrice[0] == this._info.skillsInfos[this.GetSelectedSkill()].rentPrice[num3])
					{
						content = this._info.skillsInfos[this.GetSelectedSkill()].rentPrice[0].ToString();
					}
					this.gui.TextLabel(new Rect((float)(this.SKW_x - 10), (float)(this.SKW_y + 197 + num2), 120f, 50f), content, 30, (!isPremium) ? "#FFFFFF" : "#fbc421", TextAnchor.MiddleRight, true);
				}
				if (this._info.skillsInfos[this.GetSelectedSkill()].name[0] != '!')
				{
					if (this._info.nick == Main.UserInfo.nick)
					{
						if (!Main.IsGameLoaded)
						{
							if (this.isMayUnlock(this.GetSelectedSkill()) && this.isMayBuy(this.GetSelectedSkill()) && Main.UserInfo.userID == this._info.userID)
							{
								if ((this.idCached == -1 || this.idCached == Main.UserInfo.userID) && this.gui.Button(new Vector2((float)(this.SKW_x + 134), (float)(this.SKW_y + 197 + num2)), this.class_unlockbutton[0], this.class_unlockbutton[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.Selected_skill[0] != -1 && this.Selected_skill[1] != -1 && this._info.skillsInfos.Length > this.GetSelectedSkill() && this._info.skillsInfos[this.GetSelectedSkill()].name != null)
								{
									EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, this._info.skillsInfos[this.GetSelectedSkill()].name, string.Empty, PopupState.unlockSkill, false, true, string.Empty, string.Empty));
									this.RecalcProgress();
								}
								GUI.Label(new Rect((float)(this.SKW_x + 134), (float)(this.SKW_y + 197 + num2 + this.class_unlockbutton[0].height - 22), (float)this.class_unlockbutton[0].width, 22f), Language.Unlock, this.UnlockSkillButtonStyle);
							}
						}
						else if (this.isMayUnlock(this.GetSelectedSkill()))
						{
							this.gui.TextLabel(new Rect((float)(this.SKW_x + 134), (float)(this.SKW_y + 200 + num2), 100f, 80f), Language.CarrYouInTheBattle, 13, "#FF0000", TextAnchor.UpperCenter, true);
						}
					}
				}
				else
				{
					this.gui.TextLabel(new Rect((float)(this.SKW_x + 134), (float)(this.SKW_y + 200 + num2), 100f, 80f), Language.CarrTemporarilyUnavailable, 13, "#FF0000", TextAnchor.UpperCenter, true);
				}
			}
		}
		this.DrawSkills(this.Selected_prof);
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00037FAC File Offset: 0x000361AC
	public override void MainInitialize()
	{
		CarrierGUI.I = this;
		this.SKB_width = this.Class_Button[0].width - 2;
		this.isRendering = true;
		base.MainInitialize();
		this.matchResultGUI = this.gui.GetComponent<MatchResultGUI>();
		this.avatar_back = this.avatar;
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00038000 File Offset: 0x00036200
	public override void OnDestroy()
	{
		this.avatar = null;
		if (this.wwwTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.wwwTexture);
			this.wwwTexture = null;
		}
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x0003802C File Offset: 0x0003622C
	public override void Clear()
	{
		base.Clear();
		this._info = Main.UserInfo;
		this.idCached = -1;
		this.ratingPlaceCached = -1;
		this.top100 = "TOP 300";
		this.top100type = 1;
		this.searchTerm = string.Empty;
		this.myPos = 0;
		this.voteProcessing = false;
		this.ratingLoaded = false;
		this.ratingLoading = false;
		this.visible = true;
		this.scrollPos = Vector2.zero;
		this.ach_scrollPos = Vector2.zero;
		this._carrierState = CarrierState.OVERVIEW;
		this._info = null;
		this.lastIndex = -1;
		this.popup = new Alpha();
		this.mousePos = Vector2.zero;
		this.voteIndex = 0;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000380E0 File Offset: 0x000362E0
	public override void Register()
	{
		EventFactory.Register("ShowCarrier", this);
		EventFactory.Register("HideCarrier", this);
		EventFactory.Register("InitCarrier", this);
		EventFactory.Register("StartVoting", this);
		EventFactory.Register("StopVoting", this);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0003811C File Offset: 0x0003631C
	public override void InterfaceGUI()
	{
		if (!this.skillTest)
		{
			this.SkillTest();
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow);
		this.gui.Picture(new Vector2(50f, 40f), this.gui.games_bg);
		this.gui.BeginGroup(new Rect(50f, 40f, (float)this.gui.games_bg.width, (float)this.gui.games_bg.height));
		int num = 89;
		if (this._carrierState == CarrierState.SKILLS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(23f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrSkills, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(23f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrSkills, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.SKILLS;
		}
		if (this._carrierState == CarrierState.RATING)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 112), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrRating, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 112), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrRating, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.RATING;
			if (!this.ratingLoading && !this.ratingLoaded)
			{
				Main.AddDatabaseRequestCallBack<LoadRating>(new DatabaseEvent.SomeAction(this.GenerateList), delegate
				{
				}, new object[]
				{
					this.top100type,
					string.Empty,
					false,
					false,
					false,
					this._isSeasonRating
				});
			}
		}
		if (this._carrierState == CarrierState.OVERVIEW)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 23), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrSummary, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 23), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrSummary, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.OVERVIEW;
		}
		if (this._carrierState == CarrierState.ACHIEVEMENTS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 201), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrAchievements, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 201), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrAchievements, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.ACHIEVEMENTS;
		}
		if (this._carrierState == CarrierState.CONTRACTS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 290), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrContracts, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 290), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrContracts, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.CONTRACTS;
		}
		if (this._carrierState == CarrierState.STATISTICS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 379), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrStatistics, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 379), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrStatistics, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.STATISTICS;
		}
		if (this._carrierState == CarrierState.CLANS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2((float)(num + 468), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrClans, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2((float)(num + 468), 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.CarrClans, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._carrierState = CarrierState.CLANS;
			if (Main.UserInfo.clanID == 0)
			{
				this.clanSystemWindow.controller.SetState(this.clanSystemWindow.controller.TabJoin);
			}
			else
			{
				this.clanSystemWindow.controller.SetState(this.clanSystemWindow.controller.TabManagment);
			}
		}
		if (this.gui.Button(new Vector2(655f, 27f), this.gui.closeSmallButton[0], this.gui.closeSmallButton[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.Hide(0.35f);
		}
		if (this._carrierState == CarrierState.OVERVIEW)
		{
			this.CarrierOverall();
		}
		if (this._carrierState == CarrierState.RATING)
		{
			this.CarrierRating();
		}
		if (this._carrierState == CarrierState.STATISTICS)
		{
			this.statistics.OnGUI();
		}
		if (this._carrierState == CarrierState.ACHIEVEMENTS)
		{
			this.CarrierAchivements();
		}
		if (this._carrierState == CarrierState.SKILLS)
		{
			this.CarrierSkills();
		}
		if (this._carrierState == CarrierState.CONTRACTS)
		{
			this.CarrierContracts(this._info);
		}
		if (this._carrierState == CarrierState.CLANS)
		{
			this.clanSystemWindow.OnGUI();
		}
		this.gui.EndGroup();
		this.gui.EndGroup();
		if (this._carrierState == CarrierState.ACHIEVEMENTS)
		{
			this.CarrierAchievementsHint();
		}
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00038A9C File Offset: 0x00036C9C
	public void ClassDrawGrid(ref int[,] grid, ref Texture2D TextureGrid)
	{
		int num = 50;
		int num2 = 100;
		this.gui.Picture(new Vector2((float)num, (float)num2), TextureGrid);
		this.ClassDrawGrid(ref grid, ref this.Selected_skill);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00038AD4 File Offset: 0x00036CD4
	private void RecalcProgress()
	{
		this.currentPlayerClass = PlayerClass.careerist;
		float num = 0f;
		for (int i = 0; i < 7; i++)
		{
			float num2 = (float)this.GetProgress(this.TranslatePlayerClass(i));
			if (num2 > num)
			{
				num = num2;
				this.currentPlayerClass = this.TranslatePlayerClass(i);
			}
		}
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x00038B2C File Offset: 0x00036D2C
	public void ClassDrawGrid(ref int[,] grid, ref int[] Selected_skill)
	{
		int num = 50;
		int num2 = 100;
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				if (grid[i, j] != -1 && grid[i, j] < this.Class_skills.Length)
				{
					if (CVars.LeagueLevel < 0 && Main.UserInfo.Permission < EPermission.Admin)
					{
						if (this._info.skillsInfos[grid[i, j]].type == Skills.lp_gain)
						{
							goto IL_379;
						}
						if (this._info.skillsInfos[grid[i, j]].type == Skills.lp_protect)
						{
							goto IL_379;
						}
					}
					int num3 = num - 24 + (this.Class_skills[0].height + 10) * j;
					int num4 = num2 - 24 + this.Class_skills[0].height * i;
					this.gui.Picture(new Vector2((float)num3, (float)num4), this.Class_skills[grid[i, j]]);
					if ((Selected_skill[0] != i || Selected_skill[1] != j) && !this.isMayUnlock(grid[i, j]) && !this.isSkillUnlock(grid[i, j]))
					{
						this.gui.Picture(new Vector2((float)num3, (float)(num4 + 1)), this.Class_skill_button[4]);
					}
					else if (!this._info.skillsInfos[grid[i, j]].Unlocked && this.isMayUnlock(grid[i, j]) && this.isMayBuy(grid[i, j]))
					{
						this.gui.color = Colors.alpha(Color.white, base.visibility * Mathf.Abs(Mathf.Sin(Time.realtimeSinceStartup)));
						this.gui.Picture(new Vector2((float)num3, (float)(num4 + 1)), this.Class_skill_button[7]);
						this.gui.color = Colors.alpha(Color.white, base.visibility);
					}
					if (this._info.skillsInfos[grid[i, j]].isPremium)
					{
						this.gui.Picture(new Vector2((float)num3, (float)(num4 + 1)), this.Class_skill_button[2]);
					}
					if (this._info.skillsInfos[grid[i, j]].Unlocked)
					{
						this.gui.Picture(new Vector2((float)num3, (float)(num4 + 1)), this.Class_skill_button[5]);
					}
					this.ev = this.gui.Button(new Vector2((float)num3, (float)num4), null, this.Class_skill_button[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
					if (this.ev.Clicked)
					{
						Selected_skill[0] = i;
						Selected_skill[1] = j;
					}
					if (this.ev.Hover)
					{
						if (!this._info.skillsInfos[grid[i, j]].Unlocked)
						{
							this.gui.Picture(new Vector2((float)(num3 + 28), (float)(num4 + 24)), this.Class_skill_button[6]);
						}
						this.showHint = true;
						this.hbx = num3;
						this.hby = num4;
						this.OnHoverSkill = grid[i, j];
					}
				}
				IL_379:;
			}
		}
		if (Selected_skill[0] != -1 && Selected_skill[1] != -1)
		{
			this.gui.Picture(new Vector2((float)(num - 58 + (this.Class_skills[0].height + 10) * Selected_skill[1]), (float)(num2 - 58 + this.Class_skills[0].height * Selected_skill[0])), this.Class_skill_button[3]);
		}
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x00038F30 File Offset: 0x00037130
	private void DrawSkills(int i)
	{
		this.showHint = false;
		switch (i)
		{
		case 0:
			this.ClassDrawGrid(ref this.scout, ref this.Class_Grid[i]);
			break;
		case 1:
			if (CVars.IsVanilla)
			{
				this.ClassDrawGrid(ref this.storm_trooper_vanilla, ref this.Class_Grid_Vanilla[i]);
			}
			else
			{
				this.ClassDrawGrid(ref this.storm_trooper, ref this.Class_Grid[i]);
			}
			break;
		case 2:
			this.ClassDrawGrid(ref this.destroyer, ref this.Class_Grid[i]);
			break;
		case 3:
			this.ClassDrawGrid(ref this.sniper, ref this.Class_Grid[i]);
			break;
		case 4:
			if (CVars.IsVanilla)
			{
				this.ClassDrawGrid(ref this.gunsmith_vanilla, ref this.Class_Grid_Vanilla[i]);
			}
			else
			{
				this.ClassDrawGrid(ref this.gunsmith, ref this.Class_Grid[i]);
			}
			break;
		case 5:
			if (CVars.IsVanilla)
			{
				this.ClassDrawGrid(ref this.careerist_vanilla, ref this.Selected_skill);
			}
			else
			{
				this.ClassDrawGrid(ref this.careerist, ref this.Selected_skill);
			}
			break;
		}
		if (this.showHint)
		{
			if (this._info.skillsInfos[this.OnHoverSkill].rentPrice != null && this._info.skillsInfos[this.OnHoverSkill].rentTime != null && this._info.skillsInfos[this.OnHoverSkill].Unlocked)
			{
				int seconds = this._info.skillsInfos[this.OnHoverSkill].rentEnd - HtmlLayer.serverUtc;
				this.ShowHint(this.gui.SecondsToStringHHHMMSS(seconds));
			}
			else if (!this._info.skillsInfos[this.OnHoverSkill].Unlocked && this._info.skillsInfos != null)
			{
				this.ShowHint(this._info.skillsInfos[this.OnHoverSkill].SP, this._info.skillsInfos[this.OnHoverSkill].CR, this._info.skillsInfos[this.OnHoverSkill].GP, this.OnHoverSkill);
			}
		}
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x000391A8 File Offset: 0x000373A8
	private void ShowHint(int SP, int CR, int GP, int SkillIndex)
	{
		int num = 0;
		float num2 = this.gui.CalcWidth("SP", this.gui.fontTahoma, 9);
		if (SP > 0)
		{
			float num3 = this.gui.CalcWidth(SP.ToString(), this.gui.fontTahoma, 9);
			num2 = this.gui.CalcWidth("SP", this.gui.fontTahoma, 9);
			this.gui.PictureSized(new Vector2((float)(this.hbx + 47), (float)(this.hby + 11 * num + 5)), this.gui.black, new Vector2(num3 + num2 - 7f, 11f));
			if (this._info.SP != null)
			{
				this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), SP.ToString(), 9, (!(this._info.SP < this._info.skillsInfos[this.OnHoverSkill].SP)) ? "#FFFFFF_Tahoma" : "#FF0000_Tahoma", TextAnchor.MiddleLeft, true);
			}
			this.gui.TextLabel(new Rect((float)(this.hbx + 47) + num3 - 4f, (float)(this.hby + 11 * num), 50f, 20f), "SP", 9, "#82afe2_Tahoma", TextAnchor.MiddleLeft, true);
			num++;
		}
		if (CR > 0)
		{
			float num3 = this.gui.CalcWidth(CR.ToString(), this.gui.fontTahoma, 9);
			num2 = this.gui.CalcWidth("CR", this.gui.fontTahoma, 9);
			this.gui.PictureSized(new Vector2((float)(this.hbx + 47), (float)(this.hby + 11 * num + 5)), this.gui.black, new Vector2(num3 + num2 - 7f, 11f));
			if (this._info.CR < this._info.skillsInfos[this.OnHoverSkill].CR)
			{
				this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), CR.ToString(), 9, "#FF0000_Tahoma", TextAnchor.MiddleLeft, true);
			}
			else
			{
				this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), CR.ToString(), 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
			}
			this.gui.TextLabel(new Rect((float)(this.hbx + 47) + num3 - 4f, (float)(this.hby + 11 * num), 50f, 20f), "CR", 9, "#bfbfbf_Tahoma", TextAnchor.MiddleLeft, true);
			num++;
		}
		if (GP > 0)
		{
			float num3 = this.gui.CalcWidth(GP.ToString(), this.gui.fontTahoma, 9);
			num2 = this.gui.CalcWidth("GP", this.gui.fontTahoma, 9);
			this.gui.PictureSized(new Vector2((float)(this.hbx + 47), (float)(this.hby + 11 * num + 5)), this.gui.black, new Vector2(num3 + num2 - 7f, 11f));
			if (this._info.GP < this._info.skillsInfos[this.OnHoverSkill].GP)
			{
				this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), GP.ToString(), 9, "#FF0000_Tahoma", TextAnchor.MiddleLeft, true);
			}
			else
			{
				this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), GP.ToString(), 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
			}
			this.gui.TextLabel(new Rect((float)(this.hbx + 47) + num3 - 4f, (float)(this.hby + 11 * num), 50f, 20f), "GP", 9, "#ff8400_Tahoma", TextAnchor.MiddleLeft, true);
			num++;
		}
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00039654 File Offset: 0x00037854
	private void ShowHint(string str)
	{
		int num = 0;
		if (str.Length > 0)
		{
			float num2 = this.gui.CalcWidth(str, this.gui.fontTahoma, 9);
			this.gui.PictureSized(new Vector2((float)(this.hbx + 47), (float)(this.hby + 11 * num + 5)), this.gui.black, new Vector2(num2 - 7f, 11f));
			this.gui.TextLabel(new Rect((float)(this.hbx + 47), (float)(this.hby + 11 * num), 50f, 20f), str, 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
		}
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00039710 File Offset: 0x00037910
	private Texture2D GetSelectedIcon()
	{
		if (this.Selected_skill[0] == -1 || this.Selected_skill[1] == -1)
		{
			return null;
		}
		switch (this.Selected_prof)
		{
		case 0:
			return this.Class_skills[this.scout[this.Selected_skill[0], this.Selected_skill[1]]];
		case 1:
			return (!CVars.IsVanilla) ? this.Class_skills[this.storm_trooper[this.Selected_skill[0], this.Selected_skill[1]]] : this.Class_skills[this.storm_trooper_vanilla[this.Selected_skill[0], this.Selected_skill[1]]];
		case 2:
			return this.Class_skills[this.destroyer[this.Selected_skill[0], this.Selected_skill[1]]];
		case 3:
			return this.Class_skills[this.sniper[this.Selected_skill[0], this.Selected_skill[1]]];
		case 4:
			return (!CVars.IsVanilla) ? this.Class_skills[this.gunsmith[this.Selected_skill[0], this.Selected_skill[1]]] : this.Class_skills[this.gunsmith_vanilla[this.Selected_skill[0], this.Selected_skill[1]]];
		case 5:
			return (!CVars.IsVanilla) ? this.Class_skills[this.careerist[this.Selected_skill[0], this.Selected_skill[1]]] : this.Class_skills[this.careerist_vanilla[this.Selected_skill[0], this.Selected_skill[1]]];
		default:
			return null;
		}
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x000398D0 File Offset: 0x00037AD0
	public int GetSelectedSkill()
	{
		if (this.Selected_skill[0] == -1 || this.Selected_skill[1] == -1)
		{
			return -1;
		}
		switch (this.Selected_prof)
		{
		case 0:
			return this.scout[this.Selected_skill[0], this.Selected_skill[1]];
		case 1:
			return (!CVars.IsVanilla) ? this.storm_trooper[this.Selected_skill[0], this.Selected_skill[1]] : this.storm_trooper_vanilla[this.Selected_skill[0], this.Selected_skill[1]];
		case 2:
			return this.destroyer[this.Selected_skill[0], this.Selected_skill[1]];
		case 3:
			return this.sniper[this.Selected_skill[0], this.Selected_skill[1]];
		case 4:
			return (!CVars.IsVanilla) ? this.gunsmith[this.Selected_skill[0], this.Selected_skill[1]] : this.gunsmith_vanilla[this.Selected_skill[0], this.Selected_skill[1]];
		case 5:
			return (!CVars.IsVanilla) ? this.careerist[this.Selected_skill[0], this.Selected_skill[1]] : this.careerist_vanilla[this.Selected_skill[0], this.Selected_skill[1]];
		default:
			return -1;
		}
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x00039A54 File Offset: 0x00037C54
	private bool isMayUnlock(int Index)
	{
		if (this._info.skillsInfos[Index].requirements == null)
		{
			return true;
		}
		if (this._info.skillsInfos[Index].requirements.Length > 0)
		{
			for (int i = 0; i < this._info.skillsInfos[Index].requirements.Length; i++)
			{
				if (this._info.skillsInfos[(int)this._info.skillsInfos[Index].requirements[i]].Unlocked)
				{
					return true;
				}
			}
			return false;
		}
		return true;
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00039AF0 File Offset: 0x00037CF0
	private bool isMayBuy(int Index)
	{
		return this._info.SP != null && (this._info.skillsInfos[Index].SP <= this._info.SP && this._info.skillsInfos[Index].CR <= this._info.CR && this._info.skillsInfos[Index].GP <= this._info.GP);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00039B98 File Offset: 0x00037D98
	public int GetSelectedProf()
	{
		return this.Selected_prof;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x00039BA0 File Offset: 0x00037DA0
	private bool isSkillUnlock(int Index)
	{
		return Index < this._info.skillsInfos.Length && this._info.skillsInfos[Index].Unlocked;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x00039BCC File Offset: 0x00037DCC
	public int GetProgress(PlayerClass plClass)
	{
		int num = 0;
		int num2 = 0;
		int result = 0;
		int num3 = 0;
		switch (plClass)
		{
		case PlayerClass.storm_trooper:
			if (CVars.IsVanilla)
			{
				this.GetCountSkills(ref this.storm_trooper_vanilla, out num, out num2, out result, out num3);
			}
			else
			{
				this.GetCountSkills(ref this.storm_trooper, out num, out num2, out result, out num3);
			}
			break;
		case PlayerClass.scout:
			this.GetCountSkills(ref this.scout, out num, out num2, out result, out num3);
			break;
		case PlayerClass.sniper:
			this.GetCountSkills(ref this.sniper, out num, out num2, out result, out num3);
			break;
		case PlayerClass.destroyer:
			this.GetCountSkills(ref this.destroyer, out num, out num2, out result, out num3);
			break;
		case PlayerClass.gunsmith:
			if (CVars.IsVanilla)
			{
				this.GetCountSkills(ref this.gunsmith_vanilla, out num, out num2, out result, out num3);
			}
			else
			{
				this.GetCountSkills(ref this.gunsmith, out num, out num2, out result, out num3);
			}
			break;
		case PlayerClass.careerist:
			if (CVars.IsVanilla)
			{
				this.GetCountSkills(ref this.careerist_vanilla, out num, out num2, out result, out num3);
			}
			else
			{
				this.GetCountSkills(ref this.careerist, out num, out num2, out result, out num3);
			}
			break;
		}
		return result;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00039D10 File Offset: 0x00037F10
	public void GetCountSkills(ref int[,] grid, out int SkillCount, out int countUnlock, out int countSpendSP, out int countMaxSP)
	{
		countMaxSP = 0;
		SkillCount = 0;
		countUnlock = 0;
		countSpendSP = 0;
		if (this._info.skillsInfos != null || this._info.skillsInfos.Length > 1)
		{
			for (int i = 0; i < 9; i++)
			{
				for (int j = 0; j < 7; j++)
				{
					int num = grid[i, j];
					if (num != -1)
					{
						if (this._info.skillsInfos[num].Unlocked)
						{
							countSpendSP += this._info.skillsInfos[num].SP;
							countUnlock++;
						}
						countMaxSP += this._info.skillsInfos[num].SP;
						SkillCount++;
					}
				}
			}
		}
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00039DE8 File Offset: 0x00037FE8
	public void SetCarrerState(CarrierState i)
	{
		this._carrierState = i;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00039DF4 File Offset: 0x00037FF4
	public void ResetAllSkills()
	{
		for (int i = 0; i < Main.UserInfo.skillsInfos.Length; i++)
		{
			if (!this.isCarrierisSkill(i))
			{
				if (i != 124)
				{
					if (i != 72)
					{
						if (i != 12)
						{
							if (i != 26)
							{
								Main.UserInfo.skillsInfos[i].Unlocked = false;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00039E78 File Offset: 0x00038078
	private bool isCarrierisSkill(int index)
	{
		for (int i = 0; i < 9; i++)
		{
			for (int j = 0; j < 7; j++)
			{
				if (this.careerist[i, j] == index)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00039EC0 File Offset: 0x000380C0
	private int TranslatePlayerClass(PlayerClass plClass)
	{
		switch (plClass)
		{
		case PlayerClass.storm_trooper:
			return 1;
		case PlayerClass.scout:
			return 0;
		case PlayerClass.sniper:
			return 3;
		case PlayerClass.destroyer:
			return 2;
		case PlayerClass.gunsmith:
			return 4;
		case PlayerClass.careerist:
			return 5;
		default:
			return 0;
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00039F04 File Offset: 0x00038104
	private PlayerClass TranslatePlayerClass(int plClass)
	{
		switch (plClass)
		{
		case 0:
			return PlayerClass.scout;
		case 1:
			return PlayerClass.storm_trooper;
		case 2:
			return PlayerClass.destroyer;
		case 3:
			return PlayerClass.sniper;
		case 4:
			return PlayerClass.gunsmith;
		case 5:
			return PlayerClass.careerist;
		default:
			return PlayerClass.none;
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x00039F44 File Offset: 0x00038144
	private void SkillTest()
	{
		this.skillTest = true;
		List<int[,]> list = new List<int[,]>();
		list.Add(this.scout);
		list.Add(this.sniper);
		list.Add((!CVars.IsVanilla) ? this.storm_trooper : this.storm_trooper_vanilla);
		list.Add(this.destroyer);
		list.Add((!CVars.IsVanilla) ? this.gunsmith : this.gunsmith_vanilla);
		list.Add((!CVars.IsVanilla) ? this.careerist : this.careerist_vanilla);
		Skills[] array = (Skills[])Enum.GetValues(typeof(Skills));
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < list.Count; j++)
			{
				for (int k = 0; k < 9; k++)
				{
					for (int l = 0; l < 7; l++)
					{
						if (array[i] == (Skills)list[j][k, l])
						{
							array[i] = Skills.none;
						}
					}
				}
			}
		}
		string text = string.Empty;
		for (int m = 0; m < array.Length; m++)
		{
			if (array[m] != Skills.none)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"UnusedSkill: ",
					array[m],
					"\n"
				});
			}
		}
		Debug.Log(text);
	}

	// Token: 0x040006A0 RID: 1696
	public static CarrierGUI I;

	// Token: 0x040006A1 RID: 1697
	private ScrollableLine awardScroll = new ScrollableLine(new Rect(395f, 340f, 255f, 40f));

	// Token: 0x040006A2 RID: 1698
	public ClanSystemWindow clanSystemWindow = new ClanSystemWindow();

	// Token: 0x040006A3 RID: 1699
	public ScrollList TopRatingList = new ScrollList();

	// Token: 0x040006A4 RID: 1700
	public ScrollList WatchList = new WatchlistList();

	// Token: 0x040006A5 RID: 1701
	public CarrierGUI.GroupRatingStyles RatingStyles = new CarrierGUI.GroupRatingStyles();

	// Token: 0x040006A6 RID: 1702
	private bool bcontractComplete;

	// Token: 0x040006A7 RID: 1703
	private float _ratingRefreshCooldown;

	// Token: 0x040006A8 RID: 1704
	private bool _showWatchlist;

	// Token: 0x040006A9 RID: 1705
	private bool _watchlistLoaded;

	// Token: 0x040006AA RID: 1706
	private ContractInfo[] nextContract;

	// Token: 0x040006AB RID: 1707
	private CarrierStatistics statistics;

	// Token: 0x040006AC RID: 1708
	private AwardsManager _awardsManager;

	// Token: 0x040006AD RID: 1709
	public Texture2D top_bg;

	// Token: 0x040006AE RID: 1710
	public Texture2D progressBar;

	// Token: 0x040006AF RID: 1711
	public Texture2D infoBG;

	// Token: 0x040006B0 RID: 1712
	public Texture2D avatar;

	// Token: 0x040006B1 RID: 1713
	public Texture2D statsScrollbarBg;

	// Token: 0x040006B2 RID: 1714
	public Texture2D popup_background;

	// Token: 0x040006B3 RID: 1715
	public Texture2D popup_title;

	// Token: 0x040006B4 RID: 1716
	public Texture2D[] ratingRecord;

	// Token: 0x040006B5 RID: 1717
	public Texture2D[] carrerOverall;

	// Token: 0x040006B6 RID: 1718
	public Texture2D[] bigRanks;

	// Token: 0x040006B7 RID: 1719
	public Texture2D[] stats;

	// Token: 0x040006B8 RID: 1720
	public Texture2D[] achievementsLocked;

	// Token: 0x040006B9 RID: 1721
	public Texture2D[] achievementsUnocked;

	// Token: 0x040006BA RID: 1722
	public Texture2D[] achievementsBig;

	// Token: 0x040006BB RID: 1723
	public Texture2D awardPopup;

	// Token: 0x040006BC RID: 1724
	public Texture2D expSmall;

	// Token: 0x040006BD RID: 1725
	public Texture2D[] ContractTypeImgs;

	// Token: 0x040006BE RID: 1726
	public Texture2D[] ContractStarsImgs;

	// Token: 0x040006BF RID: 1727
	public Texture2D[] ContractImgs;

	// Token: 0x040006C0 RID: 1728
	public GUIStyle SkipContractBtn;

	// Token: 0x040006C1 RID: 1729
	public GUIStyle UpdateContractBtn;

	// Token: 0x040006C2 RID: 1730
	public Texture2D[] Class_Grid;

	// Token: 0x040006C3 RID: 1731
	public Texture2D[] Class_Grid_Vanilla;

	// Token: 0x040006C4 RID: 1732
	public Texture2D[] Class_Button;

	// Token: 0x040006C5 RID: 1733
	public Texture2D[] Class_backgr_balance;

	// Token: 0x040006C6 RID: 1734
	public Texture2D[] Class_backgr_description;

	// Token: 0x040006C7 RID: 1735
	public Texture2D[] Class_backgr_progress;

	// Token: 0x040006C8 RID: 1736
	public Texture2D[] class_unlockbutton;

	// Token: 0x040006C9 RID: 1737
	public Texture2D[] Class_skills;

	// Token: 0x040006CA RID: 1738
	public Texture2D[] Class_skill_button;

	// Token: 0x040006CB RID: 1739
	public Texture2D[] Class_ICON;

	// Token: 0x040006CC RID: 1740
	public Texture2D[] Class_small_ICON;

	// Token: 0x040006CD RID: 1741
	public Texture2D[] Skills_reset_button;

	// Token: 0x040006CE RID: 1742
	private Color tempColor;

	// Token: 0x040006CF RID: 1743
	private string endMinutesStr;

	// Token: 0x040006D0 RID: 1744
	private string endSecondsStr;

	// Token: 0x040006D1 RID: 1745
	private int endHours;

	// Token: 0x040006D2 RID: 1746
	private int endMinutes;

	// Token: 0x040006D3 RID: 1747
	private int endSeconds;

	// Token: 0x040006D4 RID: 1748
	private int[] Selected_skill = new int[]
	{
		-1,
		-1
	};

	// Token: 0x040006D5 RID: 1749
	private int Selected_prof = 5;

	// Token: 0x040006D6 RID: 1750
	private int OnHoverSkill = -1;

	// Token: 0x040006D7 RID: 1751
	private bool skillTest = true;

	// Token: 0x040006D8 RID: 1752
	public int[,] scout = new int[,]
	{
		{
			-1,
			-1,
			4,
			-1,
			5,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			6,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			7,
			-1,
			8,
			-1,
			-1
		},
		{
			-1,
			12,
			10,
			9,
			11,
			152,
			-1
		},
		{
			-1,
			-1,
			17,
			2,
			18,
			20,
			-1
		},
		{
			26,
			25,
			14,
			0,
			15,
			16,
			-1
		},
		{
			-1,
			-1,
			19,
			1,
			21,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			3,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			22,
			23,
			24,
			-1,
			-1
		}
	};

	// Token: 0x040006D9 RID: 1753
	public int[,] sniper = new int[,]
	{
		{
			-1,
			-1,
			-1,
			64,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			65,
			75,
			66,
			-1,
			-1
		},
		{
			-1,
			-1,
			70,
			78,
			69,
			62,
			-1
		},
		{
			-1,
			-1,
			-1,
			68,
			-1,
			76,
			-1
		},
		{
			-1,
			-1,
			54,
			71,
			57,
			74,
			-1
		},
		{
			-1,
			-1,
			55,
			-1,
			59,
			77,
			-1
		},
		{
			-1,
			-1,
			56,
			-1,
			60,
			-1,
			-1
		},
		{
			-1,
			-1,
			58,
			-1,
			61,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006DA RID: 1754
	private int[,] storm_trooper = new int[,]
	{
		{
			-1,
			-1,
			-1,
			35,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			36,
			38,
			37,
			72,
			-1
		},
		{
			-1,
			-1,
			41,
			27,
			42,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			43,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			28,
			44,
			30,
			-1,
			-1
		},
		{
			-1,
			-1,
			29,
			45,
			31,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			49,
			32,
			-1,
			-1
		},
		{
			-1,
			-1,
			50,
			51,
			33,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			34,
			-1,
			-1
		}
	};

	// Token: 0x040006DB RID: 1755
	private int[,] gunsmith = new int[,]
	{
		{
			-1,
			-1,
			-1,
			88,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			89,
			94,
			96,
			-1
		},
		{
			85,
			82,
			79,
			90,
			95,
			97,
			-1
		},
		{
			86,
			83,
			80,
			91,
			98,
			100,
			-1
		},
		{
			87,
			84,
			81,
			92,
			99,
			101,
			-1
		},
		{
			-1,
			-1,
			-1,
			93,
			138,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006DC RID: 1756
	private int[,] destroyer = new int[,]
	{
		{
			-1,
			-1,
			-1,
			106,
			-1,
			-1,
			-1
		},
		{
			124,
			123,
			103,
			109,
			107,
			111,
			-1
		},
		{
			-1,
			126,
			115,
			108,
			116,
			112,
			-1
		},
		{
			-1,
			125,
			-1,
			119,
			-1,
			120,
			-1
		},
		{
			-1,
			127,
			113,
			104,
			114,
			121,
			-1
		},
		{
			-1,
			-1,
			128,
			105,
			129,
			122,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006DD RID: 1757
	private int[,] careerist = new int[,]
	{
		{
			-1,
			-1,
			136,
			140,
			141,
			-1,
			153
		},
		{
			-1,
			-1,
			-1,
			142,
			139,
			-1,
			154
		},
		{
			-1,
			137,
			-1,
			143,
			-1,
			130,
			-1
		},
		{
			-1,
			-1,
			150,
			-1,
			151,
			-1,
			-1
		},
		{
			-1,
			147,
			-1,
			133,
			-1,
			131,
			-1
		},
		{
			-1,
			-1,
			-1,
			145,
			-1,
			52,
			-1
		},
		{
			-1,
			148,
			-1,
			144,
			-1,
			149,
			-1
		},
		{
			-1,
			135,
			-1,
			132,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006DE RID: 1758
	private int[,] storm_trooper_vanilla = new int[,]
	{
		{
			-1,
			-1,
			-1,
			35,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			36,
			38,
			37,
			-1,
			-1
		},
		{
			-1,
			-1,
			41,
			27,
			42,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			43,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			28,
			30,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			29,
			31,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			49,
			32,
			-1,
			-1
		},
		{
			-1,
			-1,
			50,
			51,
			33,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006DF RID: 1759
	private int[,] gunsmith_vanilla = new int[,]
	{
		{
			-1,
			-1,
			-1,
			88,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			89,
			94,
			96,
			-1
		},
		{
			85,
			82,
			79,
			90,
			95,
			97,
			-1
		},
		{
			86,
			83,
			80,
			91,
			98,
			100,
			-1
		},
		{
			87,
			84,
			81,
			92,
			99,
			44,
			-1
		},
		{
			-1,
			-1,
			-1,
			93,
			138,
			45,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006E0 RID: 1760
	private int[,] careerist_vanilla = new int[,]
	{
		{
			-1,
			-1,
			136,
			140,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			142,
			139,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			143,
			-1,
			130,
			-1
		},
		{
			-1,
			-1,
			150,
			-1,
			151,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			131,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			52,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		},
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		}
	};

	// Token: 0x040006E1 RID: 1761
	public GUIStyle UnlockSkillButtonStyle = new GUIStyle();

	// Token: 0x040006E2 RID: 1762
	[HideInInspector]
	public int idCached = -1;

	// Token: 0x040006E3 RID: 1763
	[HideInInspector]
	public int ratingPlaceCached = -1;

	// Token: 0x040006E4 RID: 1764
	[HideInInspector]
	public int cardIndex = -1;

	// Token: 0x040006E5 RID: 1765
	[HideInInspector]
	public int voteIndex;

	// Token: 0x040006E6 RID: 1766
	[HideInInspector]
	public string top100 = "TOP 300";

	// Token: 0x040006E7 RID: 1767
	[HideInInspector]
	public int top100type = 1;

	// Token: 0x040006E8 RID: 1768
	[HideInInspector]
	public string searchTerm = string.Empty;

	// Token: 0x040006E9 RID: 1769
	[HideInInspector]
	public int myPos;

	// Token: 0x040006EA RID: 1770
	[HideInInspector]
	public bool voteProcessing;

	// Token: 0x040006EB RID: 1771
	[HideInInspector]
	public bool ratingLoaded;

	// Token: 0x040006EC RID: 1772
	[HideInInspector]
	public bool ratingLoading;

	// Token: 0x040006ED RID: 1773
	[HideInInspector]
	public bool visible = true;

	// Token: 0x040006EE RID: 1774
	[HideInInspector]
	public Vector2 scrollPos = Vector2.zero;

	// Token: 0x040006EF RID: 1775
	[HideInInspector]
	public Vector2 ach_scrollPos = Vector2.zero;

	// Token: 0x040006F0 RID: 1776
	[HideInInspector]
	private OverviewInfo _info;

	// Token: 0x040006F1 RID: 1777
	private CarrierState _carrierState;

	// Token: 0x040006F2 RID: 1778
	private bool _isSeasonRating;

	// Token: 0x040006F3 RID: 1779
	private int lastIndex = -1;

	// Token: 0x040006F4 RID: 1780
	private Alpha popup = new Alpha();

	// Token: 0x040006F5 RID: 1781
	private Vector2 mousePos = Vector2.zero;

	// Token: 0x040006F6 RID: 1782
	private Texture2D avatar_back;

	// Token: 0x040006F7 RID: 1783
	private Texture2D wwwTexture;

	// Token: 0x040006F8 RID: 1784
	private PlayerClass currentPlayerClass = PlayerClass.careerist;

	// Token: 0x040006F9 RID: 1785
	private MatchResultGUI matchResultGUI;

	// Token: 0x040006FA RID: 1786
	private string[] ClassName = Language.ClassName;

	// Token: 0x040006FB RID: 1787
	private ButtonState ev;

	// Token: 0x040006FC RID: 1788
	private int hbx = 47;

	// Token: 0x040006FD RID: 1789
	private int hby;

	// Token: 0x040006FE RID: 1790
	private bool showHint;

	// Token: 0x040006FF RID: 1791
	private int Sk_x = 23;

	// Token: 0x04000700 RID: 1792
	private int Sk_y = 50;

	// Token: 0x04000701 RID: 1793
	private int SKB_width;

	// Token: 0x04000702 RID: 1794
	private int SKW_x = 445;

	// Token: 0x04000703 RID: 1795
	private int SKW_y = 50;

	// Token: 0x04000704 RID: 1796
	private bool showHCOverall;

	// Token: 0x04000705 RID: 1797
	private string HCRatingSuffix = string.Empty;

	// Token: 0x04000706 RID: 1798
	private bool forceReloadrating;

	// Token: 0x04000707 RID: 1799
	private bool forceReloadHCRating;

	// Token: 0x04000708 RID: 1800
	private Texture2D tempTexture2D;

	// Token: 0x04000709 RID: 1801
	private CarrierGUI.SortOrder _sortOrder = new CarrierGUI.SortOrder();

	// Token: 0x0400070A RID: 1802
	private StringBuilder _top100labelBuilder = new StringBuilder();

	// Token: 0x020000EE RID: 238
	[Serializable]
	internal class GroupRatingStyles
	{
		// Token: 0x04000714 RID: 1812
		public GUIStyle RatingLabel = new GUIStyle();

		// Token: 0x04000715 RID: 1813
		public GUIStyle RatingOnlineBtn = new GUIStyle();

		// Token: 0x04000716 RID: 1814
		public GUIStyle RatingSearchBtn = new GUIStyle();

		// Token: 0x04000717 RID: 1815
		public GUIStyle RatingHardcoreToggle = new GUIStyle();
	}

	// Token: 0x020000EF RID: 239
	private enum WatchlistSortingBy
	{
		// Token: 0x04000719 RID: 1817
		Exp,
		// Token: 0x0400071A RID: 1818
		Kill,
		// Token: 0x0400071B RID: 1819
		Death,
		// Token: 0x0400071C RID: 1820
		Kd,
		// Token: 0x0400071D RID: 1821
		Reputation
	}

	// Token: 0x020000F0 RID: 240
	private class SortOrder
	{
		// Token: 0x0400071E RID: 1822
		public CarrierGUI.WatchlistSortingBy sortingBy;

		// Token: 0x0400071F RID: 1823
		public bool invers;

		// Token: 0x04000720 RID: 1824
		public bool complete;
	}
}
