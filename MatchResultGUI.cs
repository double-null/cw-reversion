using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Game.Foundation;
using UnityEngine;

// Token: 0x02000166 RID: 358
[AddComponentMenu("Scripts/GUI/MatchResultGUI")]
internal class MatchResultGUI : Form
{
	// Token: 0x0600099E RID: 2462 RVA: 0x0006587C File Offset: 0x00063A7C
	private IEnumerator DownloadAvatar()
	{
		string bestPlayerPhoto = this.results.bestPlayer.playerInfo.socialInfo.photo;
		if (string.IsNullOrEmpty(bestPlayerPhoto))
		{
			yield break;
		}
		WWW www = new WWW(bestPlayerPhoto);
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
			if (!this.originAvatar)
			{
				this.originAvatar = this.avatar;
			}
			this.wwwTexture = www.texture;
			this.avatar = this.wwwTexture;
		}
		www.Dispose();
		yield break;
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x00065898 File Offset: 0x00063A98
	[Obfuscation(Exclude = true)]
	private void ShowMatchEndResult(object obj)
	{
		this.Clear();
		this.autoChangeTab = 1;
		this.results = new MatchResultData();
		this.results.Read(ArrayUtility.FromJSON((obj as object[])[0] as string, string.Empty));
		List<MatchResultPlayerData> list = new List<MatchResultPlayerData>(this.results.players);
		list.Sort(new Comparison<MatchResultPlayerData>(this.SortPlayers));
		this.results.players = list.ToArray();
		this.showXP1 = 0f;
		this.showXP2 = 0f;
		if (base.audio == null)
		{
			base.gameObject.AddComponent<AudioSource>();
		}
		this.Show(0.5f, 0f);
		this.TabSelected = MatchResultTab.tab_overall;
		this.overallA.Show(0.5f, 0f);
		base.StartCoroutine(this.DownloadAvatar());
		Audio.Play(this.matchEndSound);
		WatchlistManager.AddOnce = false;
		WatchlistManager.FirstAddOnce = false;
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x00065998 File Offset: 0x00063B98
	private int GetContractProgres(int i)
	{
		int result = 0;
		switch (i)
		{
		case 0:
			result = Main.UserInfo.contractsInfo.CurrentEasyCount * 100 / Main.UserInfo.contractsInfo.CurrentEasy.task_counter;
			break;
		case 1:
			result = Main.UserInfo.contractsInfo.CurrentNormalCount * 100 / Main.UserInfo.contractsInfo.CurrentNormal.task_counter;
			break;
		case 2:
			result = Main.UserInfo.contractsInfo.CurrentHardCount * 100 / Main.UserInfo.contractsInfo.CurrentHard.task_counter;
			break;
		}
		return result;
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x00065A50 File Offset: 0x00063C50
	private void DrawPlayer(Vector2 pos, MatchResultPlayerData player, int i = 0)
	{
		this.gui.Picture(new Vector2(pos.x, pos.y), this.result_window[9]);
		if (player.clanTag.Length > 0)
		{
			this.gui.TextField(new Rect(pos.x + 90f, pos.y - 2f, 200f, 28f), player.clanTag, 20, "#d40000", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(pos.x + 90f + this.gui.CalcWidth(player.clanTag, this.gui.fontDNC57, 20), pos.y - 2f, 200f, 28f), player.nick, 20, player.nickColor, TextAnchor.MiddleLeft, false, false);
		}
		else
		{
			this.gui.TextField(new Rect(pos.x + 90f, pos.y - 2f, 200f, 28f), player.nick, 20, player.nickColor, TextAnchor.MiddleLeft, false, false);
		}
		this.gui.TextFieldfloat(new Rect(pos.x + -13f, pos.y + 6f, 80f, 28f), (float)(i + 1), 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(new Rect(pos.x + 100f, pos.y + 15f, 200f, 28f), player.socialInfo.firstName + " " + player.socialInfo.lastName, 15, "#999999", TextAnchor.UpperLeft, false, false);
		this.gui.Picture(new Vector2(pos.x + 58f, pos.y), this.gui.rank_icon[player.level]);
		this.gui.TextFieldfloat(new Rect(pos.x + 254f, pos.y + 6f, 80f, 28f), (float)player.expa, 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.TextFieldfloat(new Rect(pos.x + 335f, pos.y + 6f, 80f, 28f), (float)player.killCount, 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.TextFieldfloat(new Rect(pos.x + 400f, pos.y + 6f, 80f, 28f), (float)player.deathCount, 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(new Rect(pos.x + 452f, pos.y + 6f, 80f, 28f), player.kd_string, 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		if (this.gui.VoteWidget(new Vector2(pos.x + 530f, pos.y + 6f), player.userID, player.reputation, i))
		{
			Peer.ClientGame.LocalPlayer.MessageVoitingFor(player.nick);
			this.autoChangeTab = -1;
		}
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x00065DD4 File Offset: 0x00063FD4
	private void ShowPlayerInfo(Alpha alpha, MatchResultData data, ref float showXP)
	{
		UnityDictionarySI unityDictionarySI = new UnityDictionarySI();
		this.gui.color = new Color(1f, 1f, 1f, alpha.visibility);
		this.gui.BeginGroup(new Rect(10f, 15f, (float)this.result_window[0].width, (float)this.result_window[0].height));
		float num = 50f;
		float num2 = 0f;
		float num3 = data.newXP - ((!data.teamWin) ? 0f : CVars.g_teamWinBonus) - data.oldXP;
		unityDictionarySI[Language.MatchExp] = (int)num3;
		if (data.teamWin)
		{
			unityDictionarySI[Language.TeamWin] = (int)CVars.g_teamWinBonus;
		}
		if (this.TabSelected == MatchResultTab.tab_yours)
		{
			this.gui.TextField(new Rect(60f, 22f, 670f, 30f), Language.MRYourResult, 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
		}
		if (this.TabSelected == MatchResultTab.tab_firstPlayer)
		{
			this.gui.TextField(new Rect(60f, 22f, 670f, 30f), Language.MRBestResult, 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
		}
		for (int i = 0; i < unityDictionarySI.Count; i++)
		{
			this.gui.TextField(new Rect(530f, num + (float)(14 * i), 200f, 130f), unityDictionarySI[i], 14, "#ffa800", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(605f, num + (float)(14 * i), 130f, 130f), ((unityDictionarySI[unityDictionarySI[i]] <= 0) ? string.Empty : "+") + unityDictionarySI[unityDictionarySI[i]], 14, (unityDictionarySI[unityDictionarySI[i]] <= 0) ? "#cc0000" : "#ffa800", TextAnchor.UpperRight, false, false);
			num2 += (float)unityDictionarySI[unityDictionarySI[i]];
			this.gui.Picture(new Vector2(740f, num + (float)(14 * i) + 4f), this.result_window[16]);
		}
		num2 = data.newXP - data.oldXP;
		int num4 = unityDictionarySI.Count;
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRMatchBonus, 14, "#ffa800", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), data.matchEndBonus.ToString(), 14, "#ffa800", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[16]);
		num4++;
		if (this.TabSelected == MatchResultTab.tab_yours)
		{
			this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRSkill + " \"" + Language.MRDoubleExp + "\"", 14, "#ffa800", TextAnchor.UpperLeft, false, false);
			if (Main.UserInfo.skillUnlocked(Skills.car_exp2) || Main.UserInfo.skillUnlocked(Skills.car_exp3))
			{
				this.gui.TextField(new Rect(729f, num + (float)(14 * num4), 130f, 130f), (!Main.UserInfo.skillUnlocked(Skills.car_exp3)) ? "x2" : "x3", 14, "#ffa800", TextAnchor.UpperLeft, false, false);
			}
			else
			{
				this.gui.TextField(new Rect(729f, num + (float)(14 * num4), 130f, 130f), Language.No, 14, "#ffa800", TextAnchor.UpperLeft, false, false);
			}
			num4++;
		}
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRSkill + " \"" + Language.MRPlayersTax + "\"", 14, "#ffa800", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), data.tax, 14, "#ffa800", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[16]);
		num4++;
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRSkill + " \"" + Language.MRNightExp + "\"", 14, "#ffa800", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), (!data.IsNight) ? 0 : ((int)(num2 * 0.3f)), 14, "#ffa800", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[16]);
		num4++;
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRSkill + " \"" + Language.MRNightCredits + "\"", 14, "#ffffff", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), (!data.IsNight) ? 0 : ((int)((float)data.gainedCR * 0.3f)), 14, "#ffffff", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[14]);
		num4++;
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRCreditsForProgress, 14, "#ffffff", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), data.AchievementCR, 14, "#ffffff", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[14]);
		num4++;
		this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRSPSpend, 14, "#019fde", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), data.sp_gained, 14, "#019fde", TextAnchor.UpperRight, false, false);
		this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[15]);
		num4++;
		if (this.TabSelected == MatchResultTab.tab_yours)
		{
			this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRMPSpend, 14, "#019fde", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), data.MpGained, 14, "#019fde", TextAnchor.UpperRight, false, false);
			this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[17]);
			num4++;
		}
		if ((Main.UserInfo.clanID != 0 && this.TabSelected == MatchResultTab.tab_yours) || (data.playerInfo.clanTag != string.Empty && this.TabSelected == MatchResultTab.tab_firstPlayer))
		{
			this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRClanExpDep, 14, "#ffa800", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), (num3 * data.ClanEarnProc).ToString("F0"), 14, "#ffa800", TextAnchor.UpperRight, false, false);
			this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[16]);
			num4++;
			this.gui.TextField(new Rect(530f, num + (float)(14 * num4), 200f, 130f), Language.MRClanCrDep, 14, "#ffffff", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(605f, num + (float)(14 * num4), 130f, 130f), (num3 * data.ClanEarnProc).ToString("F0"), 14, "#ffffff", TextAnchor.UpperRight, false, false);
			this.gui.Picture(new Vector2(740f, num + (float)(14 * num4) + 4f), this.result_window[14]);
			num4++;
		}
		if (showXP < num2)
		{
			if (Event.current.type == EventType.Repaint)
			{
				showXP += Time.deltaTime * num2 / 2f;
			}
			if (showXP > num2)
			{
				showXP = num2;
			}
			if (base.audio.clip != this.expTickSound || !base.audio.isPlaying)
			{
				base.audio.clip = this.expTickSound;
				base.audio.volume = Audio.soundVolume;
				base.audio.loop = true;
				base.audio.Play();
			}
		}
		else if (showXP == num2 && base.audio.isPlaying && base.audio.clip == this.expTickSound)
		{
			base.audio.Stop();
			base.audio.loop = false;
		}
		this.gui.Picture(new Vector2(33f, 143f), this.result_window[8]);
		this.gui.TextField(new Rect(40f, 120f, 670f, 30f), Language.MREarnExp, 18, "#ffffff", TextAnchor.MiddleLeft, false, false);
		this.gui.BeginGroup(new Rect(38f, 149f, 457f, 30f));
		float num5 = (float)Globals.I.expTable[Main.UserInfo.getLevel(data.oldXP + showXP)];
		float num6 = (float)Globals.I.expTable[Main.UserInfo.getLevel(data.oldXP + showXP) + 1];
		float num7;
		if (Main.UserInfo.getLevel(data.oldXP) == Main.UserInfo.getLevel(data.oldXP + showXP))
		{
			this.gui.ProgressBar(new Vector2(0f, 0f), 457f, (data.oldXP + showXP - Main.UserInfo.minXP(data.oldXP)) / (Main.UserInfo.maxXP(data.oldXP) - Main.UserInfo.minXP(data.oldXP)), this.result_window[3], 0f, false, true);
			this.gui.ProgressBar(new Vector2(0f, 0f), 457f, (data.oldXP - Main.UserInfo.minXP(data.oldXP)) / (Main.UserInfo.maxXP(data.oldXP) - Main.UserInfo.minXP(data.oldXP)), this.result_window[1], 0f, false, true);
			num7 = (data.oldXP + showXP - Main.UserInfo.minXP(data.oldXP)) / (Main.UserInfo.maxXP(data.oldXP) - Main.UserInfo.minXP(data.oldXP));
			this.gui.Picture(new Vector2(457f * num7 - (float)this.result_window[2].width, 2f), this.result_window[2]);
		}
		else
		{
			this.gui.ProgressBar(new Vector2(0f, 0f), 457f, (data.oldXP + showXP - num5) / (num6 - num5), this.result_window[3], 0f, false, true);
			num7 = (data.oldXP + showXP - num5) / (num6 - num5);
			this.gui.Picture(new Vector2(457f * num7 - (float)this.result_window[2].width, 2f), this.result_window[2]);
		}
		this.gui.EndGroup();
		this.gui.TextLabel(new Rect(39f, 145f, 670f, 30f), Helpers.SeparateNumericString(num5.ToString("F0")), 18, "#000000", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(38f, 145f, 670f, 30f), Helpers.SeparateNumericString(num5.ToString("F0")), 18, "#ffffff", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(391f, 145f, 100f, 30f), Helpers.SeparateNumericString(num6.ToString("F0")), 18, "#000000", TextAnchor.MiddleRight, true);
		this.gui.TextLabel(new Rect(390f, 145f, 100f, 30f), Helpers.SeparateNumericString(num6.ToString("F0")), 18, "#ffffff", TextAnchor.MiddleRight, true);
		float num8 = num7;
		if (showXP == num2)
		{
			num8 = Math.Min(0.845f, num7);
		}
		this.gui.TextLabel(new Rect(457f * num8 - 10f, 170f, 100f, 30f), Helpers.SeparateNumericString(showXP.ToString("F0")), 18, "#ffa800", TextAnchor.MiddleCenter, true);
		if (showXP == num2)
		{
			int num9 = (int)((float)data.gainedCR * data.CR_map_mult + 0.5f) + data.AchievementCR;
			float num10 = MainGUI.Instance.CalcWidth(Helpers.SeparateNumericString(showXP.ToString("F0")), MainGUI.Instance.fontDNC57, 14);
			string text = showXP.ToString("F0") + " + " + num9.ToString();
			float num11 = MainGUI.Instance.CalcWidth(text, MainGUI.Instance.fontDNC57, 18) + 20f;
			if (data.IsNight)
			{
				num9 = (int)((float)num9 * 1.3f);
			}
			this.gui.TextLabel(new Rect(457f * num8 + num10, 170f, 100f, 30f), " + " + Helpers.SeparateNumericString(num9.ToString()), 18, "#ffffff", TextAnchor.MiddleCenter, true);
			this.gui.Picture(new Vector2(457f * num8 + num11, 173f), this.result_window[4]);
		}
		if (this.TabSelected == MatchResultTab.tab_yours)
		{
			this.gui.Picture(new Vector2(30f, 62f), this.gui.rank_icon[Main.UserInfo.getLevel(data.oldXP + showXP)]);
			this.gui.TextField(new Rect(75f, 59f, 130f, 130f), Language.CarrRank, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(75f, 77f, 170f, 130f), this.gui.rank_text[Main.UserInfo.getLevel(data.oldXP + showXP)], 17, "#cccccc", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2(280f, 62f), this.gui.rank_icon[Main.UserInfo.getLevel(data.oldXP + showXP) + 1]);
			this.gui.TextField(new Rect(325f, 59f, 130f, 130f), Language.CarrNextRank, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(325f, 77f, 170f, 130f), this.gui.rank_text[Main.UserInfo.getLevel(data.oldXP + showXP) + 1], 17, "#cccccc", TextAnchor.UpperLeft, false, false);
		}
		else if (this.TabSelected == MatchResultTab.tab_firstPlayer)
		{
			this.gui.Picture(new Vector2(280f, 62f), this.gui.rank_icon[Main.UserInfo.getLevel(data.oldXP + showXP)]);
			this.gui.TextField(new Rect(325f, 59f, 130f, 130f), Language.CarrRank, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(325f, 77f, 170f, 130f), this.gui.rank_text[Main.UserInfo.getLevel(data.oldXP + showXP)], 17, "#cccccc", TextAnchor.UpperLeft, false, false);
			GUI.DrawTexture(new Rect(27f, 58f, 50f, 50f), this.avatar, ScaleMode.StretchToFill);
			this.gui.TextField(new Rect(81f, 55f, 326f, 23f), data.playerInfo.nick, 21, data.playerInfo.nickColor, TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(81f, 73f, 326f, 22f), data.playerInfo.socialInfo.firstName + " " + data.playerInfo.socialInfo.lastName, 15, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(81f, 90f, 326f, 23f), "K:", 15, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextFieldInt(new Rect(93f, 90f, 326f, 23f), data.playerInfo.killCount, 15, "#ffffff", TextAnchor.MiddleLeft, false, false);
			float num12 = (data.playerInfo.deathCount <= 0) ? ((float)data.playerInfo.killCount) : ((float)data.playerInfo.killCount / (float)data.playerInfo.deathCount);
			this.gui.TextField(new Rect(124f, 90f, 326f, 23f), "K/D:", 15, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(149f, 90f, 326f, 23f), string.Format("{0:0.00}", num12), 15, "#ffffff", TextAnchor.MiddleLeft, false, false);
			if (data.playerInfo.userID != Main.UserInfo.userID)
			{
				this.gui.VoteWidget(new Vector2(156f, 88f), data.playerInfo.userID, data.playerInfo.reputation, -1);
				GUI.enabled = !WatchlistManager.FirstAddOnce;
				Rect rect = new Rect(238f, 90f, 23f, 23f);
				if (GUI.Button(rect, string.Empty, (!Main.UserInfo.WatchlistUsersId.Contains(data.playerInfo.userID)) ? CWGUI.p.NotFavoriteButton : CWGUI.p.FavoriteButton))
				{
					WatchlistManager.FirstAddOnce = true;
					bool remove = Main.UserInfo.WatchlistUsersId.Contains(data.playerInfo.userID);
					Main.AddDatabaseRequestCallBack<EditWatchlist>(delegate
					{
						if (remove)
						{
							Main.UserInfo.WatchlistUsersId.Remove(data.playerInfo.userID);
						}
						else
						{
							Main.UserInfo.WatchlistUsersId.Add(data.playerInfo.userID);
						}
					}, delegate
					{
					}, new object[]
					{
						data.playerInfo.userID,
						remove
					});
				}
				GUI.enabled = true;
				if (rect.Contains(Event.current.mousePosition))
				{
					Helpers.Hint(rect, Language.HintRatingBtnAddToFavorites, CWGUI.p.standartDNC5714, Helpers.HintAlignment.Left, 0f, 0f);
				}
			}
			else
			{
				this.gui.VoteWidget(new Vector2(180f, 88f), Main.UserInfo.userID, (float)data.playerInfo.reputation, -1);
			}
		}
		this.gui.TextLabel(new Rect(145f, 107f, 300f, 50f), ((showXP <= 0f) ? string.Empty : "+") + Helpers.SeparateNumericString(showXP.ToString("F0")), 24, (num2 <= 0f) ? "#cc0000_Micra" : "#ffa800_Micra", TextAnchor.MiddleRight, true);
		this.gui.Picture(new Vector2(448f, 119f), this.result_window[5]);
		int[] array = new int[]
		{
			-1,
			-1,
			-1
		};
		int[] array2 = new int[3];
		if (data.recordValues != null)
		{
			int record = data.record;
			int[] recordValues = data.recordValues;
			List<int> list = BIT.INDEXES(record);
			SortedList<int, int> sortedList = new SortedList<int, int>();
			for (int j = 0; j < list.Count; j++)
			{
				sortedList.Add(list[j], Globals.I.streakExp[list[j]]);
			}
			if (list.Count >= 3)
			{
				array[2] = sortedList.Keys[sortedList.Count - 3];
				array2[2] = recordValues[sortedList.Keys[sortedList.Count - 3]];
			}
			if (list.Count >= 2)
			{
				array[1] = sortedList.Keys[sortedList.Count - 2];
				array2[1] = recordValues[sortedList.Keys[sortedList.Count - 2]];
			}
			if (list.Count >= 1)
			{
				array[0] = sortedList.Keys[sortedList.Count - 1];
				array2[0] = recordValues[sortedList.Keys[sortedList.Count - 1]];
			}
		}
		this.gui.TextField(new Rect(60f, 211f, 670f, 30f), Language.MRBestResultForMatch, 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
		bool flag = false;
		for (int k = 0; k < 3; k++)
		{
			if (array[k] == -1)
			{
				break;
			}
			this.gui.Picture(new Vector2((float)(15 + 250 * k), 240f), this.resultKillStreaks[array[k]]);
			this.gui.BeginGroup(new Rect((float)(15 + 250 * k), 240f, (float)this.resultKillStreaks[array[k]].width, (float)(this.resultKillStreaks[array[k]].height + 30)));
			this.gui.TextField(new Rect(33f, 300f, 130f, 30f), (array[k] + KillStreakEnumIndexes.kill).ToString(), 18, "#cccccc", TextAnchor.LowerLeft, false, false);
			this.gui.TextFieldint(new Rect(155f, 293f, 50f, 40f), array2[k], 30, "#999999", TextAnchor.LowerRight, false, false);
			this.gui.EndGroup();
			flag = true;
		}
		if (!flag)
		{
			this.gui.TextField(new Rect(60f, 383f, 670f, 30f), Language.MRNoAchievements, 16, "#666666_Micra", TextAnchor.MiddleCenter, false, false);
		}
		this.gui.EndGroup();
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0006793C File Offset: 0x00065B3C
	private int SortPlayers(MatchResultPlayerData p1, MatchResultPlayerData p2)
	{
		if (p1.expa > p2.expa)
		{
			return -1;
		}
		if (p1.expa < p2.expa)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x00067968 File Offset: 0x00065B68
	public override void MainInitialize()
	{
		this.isGameHandler = true;
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x00067980 File Offset: 0x00065B80
	public override void OnConnected()
	{
		base.OnConnected();
		this.Clear();
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x00067990 File Offset: 0x00065B90
	public override void Clear()
	{
		base.Clear();
		this.showXP1 = 0f;
		this.showXP2 = 0f;
		this.results = null;
		this.TabSelected = MatchResultTab.tab_overall;
		this.overallA = new Alpha();
		this.yoursA = new Alpha();
		this.firstPlayerA = new Alpha();
		if (this.originAvatar)
		{
			this.avatar = this.originAvatar;
		}
		this.scrollPos = Vector2.zero;
		if (base.audio)
		{
			base.audio.Stop();
		}
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x00067A2C File Offset: 0x00065C2C
	public override void Register()
	{
		EventFactory.Register("ShowMatchEndResult", this);
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x00067A3C File Offset: 0x00065C3C
	public override void GameGUI()
	{
		if (!base.Visible)
		{
			return;
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.BeginGroup(rect, false);
		this.gui.Picture(new Vector2(10f, 15f), this.result_window[0]);
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if (this.gui.Button(new Vector2(29f, 33f), this.result_window[6], this.result_window[6], this.result_window[6], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			if (this.TabSelected == MatchResultTab.tab_firstPlayer)
			{
				this.firstPlayerA.Hide(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_overall)
			{
				this.overallA.Hide(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_yours)
			{
				this.yoursA.Hide(0.5f, 0f);
			}
			this.TabSelected--;
			if (this.TabSelected < MatchResultTab.tab_overall)
			{
				this.TabSelected = MatchResultTab.tab_firstPlayer;
			}
			if (this.TabSelected == MatchResultTab.tab_firstPlayer)
			{
				this.firstPlayerA.Show(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_yours)
			{
				this.yoursA.Show(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_overall)
			{
				this.overallA.Show(0.5f, 0f);
			}
			this.autoChangeTab = -1;
		}
		if (this.gui.Button(new Vector2(696f, 33f), this.result_window[7], this.result_window[7], this.result_window[7], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			if (this.TabSelected == MatchResultTab.tab_firstPlayer)
			{
				this.firstPlayerA.Hide(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_overall)
			{
				this.overallA.Hide(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_yours)
			{
				this.yoursA.Hide(0.5f, 0f);
			}
			this.TabSelected++;
			if (this.TabSelected > MatchResultTab.tab_firstPlayer)
			{
				this.TabSelected = MatchResultTab.tab_overall;
			}
			if (this.TabSelected == MatchResultTab.tab_firstPlayer)
			{
				this.firstPlayerA.Show(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_yours)
			{
				this.yoursA.Show(0.5f, 0f);
			}
			if (this.TabSelected == MatchResultTab.tab_overall)
			{
				this.overallA.Show(0.5f, 0f);
			}
			this.autoChangeTab = -1;
		}
		if (Main.IsGameLoaded)
		{
			this.gui.TextField(new Rect(299f, 23f, 200f, 30f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 11, "#000000_Micra", TextAnchor.UpperCenter, false, false);
			this.gui.TextField(new Rect(301f, 23f, 200f, 30f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 11, "#000000_Micra", TextAnchor.UpperCenter, false, false);
			this.gui.TextField(new Rect(300f, 23f, 200f, 30f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 11, Colors.RadarWhiteWeb + "_Micra", TextAnchor.UpperCenter, false, false);
			if (this.autoChangeTab >= 0 && (int)Peer.ClientGame.ElapsedNextEventTime > 5)
			{
				if (this.autoChangeTab < (int)Peer.ClientGame.ElapsedNextEventTime)
				{
					this.autoChangeTab = (int)Peer.ClientGame.ElapsedNextEventTime;
				}
				if (this.autoChangeTab >= (int)Peer.ClientGame.ElapsedNextEventTime + 7)
				{
					if (this.TabSelected == MatchResultTab.tab_firstPlayer)
					{
						this.firstPlayerA.Hide(0.5f, 0f);
					}
					if (this.TabSelected == MatchResultTab.tab_overall)
					{
						this.overallA.Hide(0.5f, 0f);
					}
					if (this.TabSelected == MatchResultTab.tab_yours)
					{
						this.yoursA.Hide(0.5f, 0f);
					}
					this.TabSelected++;
					if (this.TabSelected > MatchResultTab.tab_firstPlayer)
					{
						this.TabSelected = MatchResultTab.tab_overall;
					}
					if (this.TabSelected == MatchResultTab.tab_firstPlayer)
					{
						this.firstPlayerA.Show(0.5f, 0f);
					}
					if (this.TabSelected == MatchResultTab.tab_yours)
					{
						this.yoursA.Show(0.5f, 0f);
					}
					if (this.TabSelected == MatchResultTab.tab_overall)
					{
						this.overallA.Show(0.5f, 0f);
					}
					this.autoChangeTab = (int)Peer.ClientGame.ElapsedNextEventTime;
				}
			}
		}
		if (this.yoursA.Visible)
		{
			this.ShowPlayerInfo(this.yoursA, this.results, ref this.showXP1);
		}
		if (this.firstPlayerA.Visible)
		{
			this.ShowPlayerInfo(this.firstPlayerA, this.results.bestPlayer, ref this.showXP2);
		}
		if (this.TabSelected == MatchResultTab.tab_overall)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.overallA.visibility);
			this.gui.BeginGroup(new Rect(10f, 15f, (float)this.result_window[0].width, (float)this.result_window[0].height));
			MatchResultPlayerData playerInfo = this.results.bestPlayer.playerInfo;
			if (!Main.IsTeamGame)
			{
				this.gui.Picture(new Vector2(30f, 62f), this.gui.rank_icon[this.results.bestPlayer.playerInfo.level]);
				this.gui.TextField(new Rect(75f, 59f, 130f, 130f), Language.MRBestPlayer, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(75f, 77f, 170f, 130f), playerInfo.nick, 17, playerInfo.nickColor, TextAnchor.UpperLeft, false, false);
				MatchResultPlayerData playerInfo2 = this.results.worstPlayer.playerInfo;
				this.gui.Picture(new Vector2(280f, 62f), this.gui.rank_icon[this.results.worstPlayer.playerInfo.level]);
				this.gui.TextField(new Rect(325f, 59f, 130f, 130f), Language.MRWorthPlayer, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(325f, 77f, 170f, 130f), playerInfo2.nick, 17, playerInfo2.nickColor, TextAnchor.UpperLeft, false, false);
			}
			else
			{
				this.gui.Picture(new Vector2(280f, 62f), this.gui.rank_icon[this.results.bestPlayer.playerInfo.level]);
				this.gui.TextField(new Rect(325f, 59f, 130f, 130f), Language.MRBestPlayer, 15, "#6f6f6f", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(325f, 77f, 170f, 130f), playerInfo.nick, 17, playerInfo.nickColor, TextAnchor.UpperLeft, false, false);
				bool flag = false;
				if (Main.IsTeamElimination || Main.IsTargetDesignation || Main.IsTacticalConquest)
				{
					flag = (Peer.ClientGame.UsecWinCount < Peer.ClientGame.BearWinCount);
				}
				if (Peer.ClientGame.UsecWinCount != Peer.ClientGame.BearWinCount || Main.IsTargetDesignation)
				{
					this.gui.Picture(new Vector2(25f, 52f), (!flag) ? this.result_window[13] : this.result_window[12]);
					this.gui.TextField(new Rect(101f, 64f, 170f, 130f), Language.MRWin, 25, "#999999", TextAnchor.UpperLeft, false, false);
					this.gui.TextField(new Rect(171f, 64f, 170f, 130f), (!flag) ? "USEC" : "BEAR", 25, "#ffffff", TextAnchor.UpperLeft, false, false);
				}
				else if (Peer.ClientGame.UsecWinCount == Peer.ClientGame.BearWinCount)
				{
					this.gui.TextField(new Rect(101f, 64f, 170f, 130f), Language.MRDraw, 25, "#999999", TextAnchor.UpperLeft, false, false);
				}
			}
			this.gui.EndGroup();
			this.gui.TextField(new Rect(60f, 37f, 670f, 30f), Language.MRMatchResult, 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(60f, 226f, 670f, 30f), Language.CarrPlayers, 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(540f, 70f, 200f, 130f), Language.MRGameTime, 15, "#cccccc", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(640f, 70f, 130f, 130f), this.gui.SecondsToStringMS(this.results.gameTotalTime), 15, "#ffa800", TextAnchor.UpperRight, false, false);
			this.gui.TextField(new Rect(540f, 87f, 200f, 130f), Language.MRKillsTotal, 15, "#cccccc", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(640f, 87f, 130f, 130f), this.results.gameTotalKills.ToString(), 15, "#ffa800", TextAnchor.UpperRight, false, false);
			this.gui.TextField(new Rect(540f, 104f, 200f, 130f), Language.MRDeathTotal, 15, "#cccccc", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(640f, 104f, 130f, 130f), this.results.gameTotalDeaths.ToString(), 15, "#ffa800", TextAnchor.UpperRight, false, false);
			this.gui.TextField(new Rect(540f, 121f, 200f, 130f), Language.MRExpRate, 15, "#cccccc", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(640f, 121f, 130f, 130f), (!Peer.HardcoreMode) ? ("x" + this.results.XP_map_mult.ToString("F1")) : ("x" + (this.results.XP_map_mult * CVars.g_hardcorexpCoef).ToString("F1")), 15, "#ffa800", TextAnchor.UpperRight, false, false);
			this.gui.TextField(new Rect(126f, 255f, 100f, 20f), Language.CarrPlace, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(180f, 255f, 100f, 20f), Language.CarrLVL, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(204f, 255f, 100f, 20f), Language.CarrItemName, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(396f, 255f, 100f, 20f), Language.CarrPoints, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(465f, 255f, 100f, 20f), Language.CarrKills, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(534f, 255f, 100f, 20f), Language.CarrDeath, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(597f, 255f, 100f, 20f), "K/D", 16, "#999999", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(659f, 255f, 100f, 20f), Language.CarrReputation, 16, "#999999", TextAnchor.MiddleLeft, false, false);
			int num = this.results.players.Length * 40;
			if (Main.IsTeamGame)
			{
				num += 20;
			}
			if (this.scrollPos.y > (float)num)
			{
				this.scrollPos.y = (float)num;
			}
			this.scrollPos = this.gui.BeginScrollView(new Rect(35f, 276f, 742f, 308f), this.scrollPos, new Rect(0f, 0f, 645f, (float)num), float.MaxValue);
			if (!Main.IsTeamGame)
			{
				int num2 = 0;
				for (int i = 0; i < this.results.players.Length; i++)
				{
					if (!this.results.players[i].spectactor)
					{
						this.DrawPlayer(new Vector2(80f, (float)(40 * num2)), this.results.players[i], num2);
						num2++;
					}
				}
			}
			else
			{
				List<MatchResultPlayerData> list = new List<MatchResultPlayerData>();
				List<MatchResultPlayerData> list2 = new List<MatchResultPlayerData>();
				for (int j = 0; j < this.results.players.Length; j++)
				{
					if (!this.results.players[j].spectactor)
					{
						if (this.results.players[j].isBear)
						{
							list.Add(this.results.players[j]);
						}
						else
						{
							list2.Add(this.results.players[j]);
						}
					}
				}
				int num3 = 0;
				this.gui.Picture(new Vector2(-7f, (float)(40 * num3 + 5)), this.result_window[10]);
				this.gui.TextField(new Rect(26f, 3f, 50f, 30f), "BEAR", 25, "#ffffffff", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(24f, 33f, 50f, 30f), Peer.ClientGame.BearWinCount, 11, "#ffffff_Micra", TextAnchor.UpperCenter, false, false);
				list.Sort(new Comparison<MatchResultPlayerData>(this.SortPlayers));
				list2.Sort(new Comparison<MatchResultPlayerData>(this.SortPlayers));
				foreach (MatchResultPlayerData matchResultPlayerData in list)
				{
					this.DrawPlayer(new Vector2(80f, (float)(40 * num3)), (!(matchResultPlayerData.socialID == this.results.bestPlayer.playerInfo.socialID)) ? matchResultPlayerData : this.results.bestPlayer.playerInfo, num3);
					num3++;
				}
				int num4 = 0;
				this.gui.Picture(new Vector2(-4f, (float)(20 + 40 * list.Count + 40 * num4 + 4)), this.result_window[11]);
				this.gui.TextField(new Rect(28f, (float)(20 + 40 * list.Count), 50f, 30f), "USEC", 25, "#ffffffff", TextAnchor.UpperLeft, false, false);
				this.gui.TextField(new Rect(24f, (float)(20 + 40 * list.Count + 30), 50f, 30f), Peer.ClientGame.UsecWinCount, 11, "#ffffff_Micra", TextAnchor.UpperCenter, false, false);
				foreach (MatchResultPlayerData matchResultPlayerData2 in list2)
				{
					this.DrawPlayer(new Vector2(80f, (float)(20 + 40 * list.Count + 40 * num4)), (!(matchResultPlayerData2.socialID == this.results.bestPlayer.playerInfo.socialID)) ? matchResultPlayerData2 : this.results.bestPlayer.playerInfo, num4);
					num4++;
				}
			}
			this.gui.EndScrollView();
		}
		this.gui.EndGroup();
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x00068CEC File Offset: 0x00066EEC
	public override void OnSpawn()
	{
		this.Clear();
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x00068CF4 File Offset: 0x00066EF4
	public override void OnDie()
	{
		this.Clear();
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x00068CFC File Offset: 0x00066EFC
	public override void OnRoundStart()
	{
		this.Clear();
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x00068D04 File Offset: 0x00066F04
	public override void OnRoundEnd()
	{
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x00068D08 File Offset: 0x00066F08
	public override void OnMatchStart()
	{
		this.Clear();
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x00068D10 File Offset: 0x00066F10
	public override void OnDestroy()
	{
		this.avatar = this.originAvatar;
		if (this.wwwTexture)
		{
			UnityEngine.Object.DestroyImmediate(this.wwwTexture);
			this.wwwTexture = null;
		}
	}

	// Token: 0x04000B19 RID: 2841
	public Texture2D avatar;

	// Token: 0x04000B1A RID: 2842
	public Texture2D[] result_window;

	// Token: 0x04000B1B RID: 2843
	public Texture2D[] resultKillStreaks;

	// Token: 0x04000B1C RID: 2844
	public AudioClip expTickSound;

	// Token: 0x04000B1D RID: 2845
	public AudioClip matchEndSound;

	// Token: 0x04000B1E RID: 2846
	[HideInInspector]
	public Vector2 scrollPos = Vector2.zero;

	// Token: 0x04000B1F RID: 2847
	private float showXP1;

	// Token: 0x04000B20 RID: 2848
	private float showXP2;

	// Token: 0x04000B21 RID: 2849
	private MatchResultData results;

	// Token: 0x04000B22 RID: 2850
	private int autoChangeTab = 1;

	// Token: 0x04000B23 RID: 2851
	private MatchResultTab TabSelected;

	// Token: 0x04000B24 RID: 2852
	private Alpha overallA = new Alpha();

	// Token: 0x04000B25 RID: 2853
	private Alpha yoursA = new Alpha();

	// Token: 0x04000B26 RID: 2854
	private Alpha firstPlayerA = new Alpha();

	// Token: 0x04000B27 RID: 2855
	private Texture2D originAvatar;

	// Token: 0x04000B28 RID: 2856
	private Texture2D wwwTexture;
}
