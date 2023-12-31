using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Assets.Scripts.Camouflage;
using ClanSystemGUI;
using CWSARequests;
using LeagueGUI;
using PeerNamespace;
using UnityEngine;

// Token: 0x0200016C RID: 364
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/GUI/PopupGUI")]
internal class PopupGUI : Form, OnPeerEvent
{
	// Token: 0x060009E3 RID: 2531 RVA: 0x00069E74 File Offset: 0x00068074
	public PopupGUI()
	{
		bool[] array = new bool[3];
		array[0] = true;
		this.rentToggle = array;
		this.tmpStyle = new GUIStyle();
		this.tgl_1_Style = new GUIStyle();
		this.tgl_2_Style = new GUIStyle();
		this.tgl_3_Style = new GUIStyle();
		this.connectionProblemStyle = new GUIStyle();
		this.listTryedToConnectGames = new List<HostInfo>();
		this.hofPositions = new PopupGUI.HOFPositions();
		this.LWPPos = new PopupGUI.LeagueWinnerPopupPositions();
		this._canSendLogin = true;
		this._pass = string.Empty;
		this._useHashedPass = true;
		this._sendLoginCoolDownTimer = new Timer();
		this._attemptCooldown = new Timer();
		this._nickColor = string.Empty;
		this._previousValidNickColor = string.Empty;
		this._url = string.Empty;
		this._color = string.Empty;
		this._mpToBuy = 1;
		this.gameList = new List<HostInfo>();
		this.tmpMode = GameMode.any;
		this.tmpMap = Language.anyFemale;
		base..ctor();
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0006A05C File Offset: 0x0006825C
	public static bool IsAnyPopupShow
	{
		get
		{
			return PopupGUI.thisObject != null && PopupGUI.thisObject.popups.Count > 0;
		}
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0006A090 File Offset: 0x00068290
	private Popup getByUniquie(WindowsID ID)
	{
		for (int i = 0; i < this.popups.Count; i++)
		{
			if (this.popups[i].windowID == (int)ID)
			{
				return this.popups[i];
			}
		}
		return null;
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0006A0E0 File Offset: 0x000682E0
	private Popup getByUniquieToShow(WindowsID ID)
	{
		for (int i = 0; i < this.popups.Count; i++)
		{
			if (this.popups[i].windowID == (int)ID && i == 0)
			{
				return this.popups[i];
			}
		}
		return null;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0006A134 File Offset: 0x00068334
	[Obfuscation(Exclude = true)]
	private void ShowConnectionProblem(object obj)
	{
		this.error.Pulse(1f);
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0006A148 File Offset: 0x00068348
	[Obfuscation(Exclude = true)]
	private void ShowPingProblem(object obj)
	{
		this._highPingAlpha.Pulse(1f);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0006A15C File Offset: 0x0006835C
	[Obfuscation(Exclude = true)]
	public void StopConnection()
	{
		Peer.Disconnect(false);
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0006A164 File Offset: 0x00068364
	private void CleanUp()
	{
		for (int i = 0; i < this.popups.Count; i++)
		{
			if (!this.popups[i].popupA.Visible && !this.popups[i].popupA.Showing)
			{
				this.popups.RemoveAt(i);
				i = 0;
			}
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0006A1D8 File Offset: 0x000683D8
	[Obfuscation(Exclude = true)]
	private void MarkNewsRead()
	{
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0006A1DC File Offset: 0x000683DC
	[Obfuscation(Exclude = true)]
	private void MarkNewsReadFinished(string text, string url)
	{
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0006A1E0 File Offset: 0x000683E0
	[Obfuscation(Exclude = true)]
	private void ShowPopup(object obj)
	{
		Popup popup = (Popup)obj;
		if (popup.popupState == PopupState.SeasonAward && this.carrierGUI.AwardsManager.SeasonAwards == null)
		{
			base.StartCoroutine(this.ShowSeasonAwardPopupAtReady(obj));
			return;
		}
		Popup popup2 = this.getByUniquie((WindowsID)popup.windowID);
		if (popup2 == null)
		{
			if (popup.popupState == PopupState.quickGame)
			{
				this.StartSearchGames = false;
				this.SearchInFullGames = false;
				this.gameListAssembled = false;
				this.sortingComplete = false;
			}
			this.popups.Add(popup);
			popup2 = popup;
		}
		else
		{
			this.popups[this.popups.IndexOf(popup2)] = popup;
			popup2 = popup;
		}
		if (this.mapsDeathmatch == null && this.mapsTargetDesignation == null && this.mapsTeamElimination == null && this.mapsTacticalConquest == null)
		{
			this.gameModesNames[0] = Language.anyMale;
			this.mapsAllList.Add(Language.anyFemale);
			this.mapsDeathmatchList.Add(Language.anyFemale);
			this.mapsTargetDesignationList.Add(Language.anyFemale);
			this.mapsTeamEliminationList.Add(Language.anyFemale);
			this.mapsTacticalConquestList.Add(Language.anyFemale);
			for (int i = 0; i < Globals.I.maps.Length; i++)
			{
				if (Globals.I.maps[i].Name != Maps.training.ToString() && Globals.I.maps[i].Name != Maps.develop.ToString())
				{
					this.mapsAllList.Add(Globals.I.maps[i].Name);
					for (int j = 0; j < Globals.I.maps[i].Modes.Count; j++)
					{
						if (Globals.I.maps[i].Modes[j] == GameMode.Deathmatch)
						{
							this.mapsDeathmatchList.Add(Globals.I.maps[i].Name);
						}
						if (Globals.I.maps[i].Modes[j] == GameMode.TargetDesignation)
						{
							this.mapsTargetDesignationList.Add(Globals.I.maps[i].Name);
						}
						if (Globals.I.maps[i].Modes[j] == GameMode.TeamElimination)
						{
							this.mapsTeamEliminationList.Add(Globals.I.maps[i].Name);
						}
						if (Globals.I.maps[i].Modes[j] == GameMode.TacticalConquest)
						{
							this.mapsTacticalConquestList.Add(Globals.I.maps[i].Name);
						}
					}
				}
			}
			this.mapsDeathmatch = this.mapsDeathmatchList.ToArray();
			this.mapsTargetDesignation = this.mapsTargetDesignationList.ToArray();
			this.mapsTeamElimination = this.mapsTeamEliminationList.ToArray();
			this.mapsTacticalConquest = this.mapsTacticalConquestList.ToArray();
			this.mapsALL = this.mapsAllList.ToArray();
			this.mapsDeathmatchList.Clear();
			this.mapsTargetDesignationList.Clear();
			this.mapsTeamEliminationList.Clear();
			this.mapsTacticalConquestList.Clear();
		}
		popup2.Show(0.25f);
		this.Show(0.5f, 0f);
		this.gui.FocusWindow(popup.windowID);
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0006A554 File Offset: 0x00068754
	private IEnumerator ShowSeasonAwardPopupAtReady(object obj)
	{
		while (this.carrierGUI.AwardsManager.SeasonAwards == null)
		{
			yield return new WaitForEndOfFrame();
		}
		this.ShowPopup(obj);
		yield break;
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0006A580 File Offset: 0x00068780
	public override void Show(float time = 0.3f, float delay = 0f)
	{
		base.Show(time, delay);
		this.promoAlpha.Once(1.3f);
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0006A59C File Offset: 0x0006879C
	[Obfuscation(Exclude = true)]
	private void HidePopup(object obj)
	{
		Popup popup = (Popup)obj;
		Popup byUniquie = this.getByUniquie((WindowsID)popup.windowID);
		if (byUniquie == null)
		{
			return;
		}
		byUniquie.Hide(popup, 0.3f, 0f);
		this.CleanUp();
		this.gui.UnFocusWindow(popup.windowID);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0006A5EC File Offset: 0x000687EC
	private void SimplePopup(Popup popup)
	{
		this.PopupWindowFunc(popup.windowID);
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0006A5FC File Offset: 0x000687FC
	private void PopupWindowFunc(int id)
	{
		Popup byUniquieToShow = this.getByUniquieToShow((WindowsID)id);
		if (byUniquieToShow == null)
		{
			return;
		}
		if (byUniquieToShow.popupState == PopupState.banned)
		{
			GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), MainGUI.Instance.black);
		}
		this.gui.color = new Color(1f, 1f, 1f, byUniquieToShow.popupA.visibility);
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		this.gui.Picture(new Vector2(0f, 0f), this.gui.settings_window[8]);
		if (byUniquieToShow.popupState == PopupState.ActivatePromo)
		{
			this.ActivatePromoTab(byUniquieToShow);
		}
		if (byUniquieToShow.popupState == PopupState.progressBonus)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 1;
			string text = string.Empty;
			if (LoadProfile.newLevel == 0)
			{
				text = Language.BonusString0;
				num3 = 50;
				num4 = 5;
				num = 15;
				num5 = 6;
				num2 = 12000;
			}
			else if (LoadProfile.newLevel >= 1 && LoadProfile.newLevel <= 9)
			{
				text = Language.BonusString3;
				num2 = 1000;
				num3 = 10;
				num4 = 1;
			}
			if (LoadProfile.newLevel == 10)
			{
				text = Language.BonusString10;
				num2 = 1000;
				num3 = 10;
				num4 = 1;
				num = 9;
				num5 = 3;
			}
			else if (LoadProfile.newLevel == 20)
			{
				text = Language.BonusString20;
				num = 24;
				num5 = 3;
				num2 = 4000;
			}
			else if (LoadProfile.newLevel == 60)
			{
				text = Language.BonusString60;
				num3 = 100;
			}
			else if (LoadProfile.newLevel == 61)
			{
				text = Language.BonusString61;
				num3 = 100;
			}
			else if (LoadProfile.newLevel == 62)
			{
				text = Language.BonusString62;
				num3 = 100;
			}
			else if (LoadProfile.newLevel == 63)
			{
				text = Language.BonusString63;
				num3 = 100;
			}
			else if (LoadProfile.newLevel == 64)
			{
				text = Language.BonusString64;
				num3 = 200;
			}
			else if (LoadProfile.newLevel == 65)
			{
				text = Language.BonusString65;
				num3 = 200;
			}
			else if (LoadProfile.newLevel == 66)
			{
				text = Language.BonusString66;
				num3 = 500;
			}
			else if (LoadProfile.newLevel == 67)
			{
				text = Language.BonusString67;
				num3 = 500;
			}
			else if (LoadProfile.newLevel == 68)
			{
				text = Language.BonusString68;
				num3 = 750;
			}
			else if (LoadProfile.newLevel == 69)
			{
				text = Language.BonusString69;
				num3 = 1000;
			}
			else if (LoadProfile.newLevel == 70)
			{
				text = Language.BonusString70;
				num3 = 2000;
			}
			if (text.Length < 1 && num == 0 && num2 == 0 && num3 == 0 && num4 == 0)
			{
				this.gui.EndGroup();
				byUniquieToShow.Hide(null, 0.5f, 0.5f);
				return;
			}
			this.gui.Picture(new Vector2(20f, 95f), this.progressBonus);
			if (LoadProfile.newLevel == 0)
			{
				HostInfo hostInfo = this.SmartFindGame(this.gameModes[this.DropDowntempIndex], this.GetMapsByMode(this.gameModes[this.DropDowntempIndex])[this.MapIndex]);
				if (hostInfo == null && this.reloadTimer < Time.realtimeSinceStartup && this.reloadCounter < 5)
				{
					this.reloadTimer = Time.realtimeSinceStartup + 5f;
					this.reloadCounter += 1;
					Peer.ForceUpdateServers();
				}
				if (this.gui.Button(new Vector2(220f, 160f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.Later, 15, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					byUniquieToShow.Hide(null, 0.5f, 0.5f);
					Main.AddDatabaseRequest<ClearBonusInfo>(new object[0]);
				}
				Color color = this.gui.color;
				GUI.enabled = (hostInfo != null);
				MainGUI gui = this.gui;
				string text2 = Language.JoinFight;
				this.unlock = gui.Button(new Vector2(25f, 160f), this.gui.mainMenuButtons[11], this.gui.mainMenuButtons[12], null, text2, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				GUI.enabled = true;
				if (hostInfo != null)
				{
					this.gui.color = Colors.alpha(this.gui.color, byUniquieToShow.popupA.visibility * Mathf.Abs(Mathf.Cos(Time.realtimeSinceStartup * 4f)));
					GUI.DrawTexture(new Rect(25f, 160f, (float)this.gui.YellowGlow.width, (float)this.gui.YellowGlow.height), this.gui.YellowGlow);
				}
				this.gui.color = color;
				if (this.unlock.Clicked && hostInfo != null && !byUniquieToShow.alreadyClicked)
				{
					byUniquieToShow.alreadyClicked = true;
					this.StartSearchGames = true;
					this.reloadCounter = 0;
					this.reloadTimer = 0f;
					this.listTryedToConnectGames.Clear();
					if (!this.JoinGame(hostInfo))
					{
						Debug.Log(Helpers.ColoredLog(hostInfo.Name, "#00FF00"));
						this.OnConnectionFailed();
					}
					byUniquieToShow.Hide(null, 0.5f, 0.5f);
					Main.AddDatabaseRequest<ClearBonusInfo>(new object[0]);
				}
			}
			else if (this.gui.Button(new Vector2(325f, 155f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], "OK", 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, null).Clicked)
			{
				byUniquieToShow.Hide(null, 0.5f, 0.5f);
				Main.AddDatabaseRequest<ClearBonusInfo>(new object[0]);
			}
			this.gui.TextField(new Rect(28f, 44f, 390f, 50f), text, 12, "#ffffff_Tahoma", TextAnchor.MiddleLeft, false, false);
			if (num > 0)
			{
				this.gui.TextField(new Rect(340f, 98f, 50f, 19f), "PREMIUM", 14, "#ffa200", TextAnchor.MiddleLeft, false, false);
				this.gui.TextField(new Rect(340f, 115f, 70f, 19f), Main.UserInfo.weaponsStates[num].CurrentWeapon.InterfaceName, 14, "#ffffff", TextAnchor.MiddleLeft, false, false);
				this.gui.TextField(new Rect(340f, 130f, 50f, 19f), Language.TheTerm, 14, "#999999", TextAnchor.MiddleLeft, false, false);
				Rect rect = new Rect(369f, 130f, 50f, 19f);
				this.gui.CompositeTextClean(ref rect, num5.ToString(), 14, "#ffffff", TextAnchor.MiddleLeft);
				if (num5 == 1)
				{
					this.gui.CompositeTextClean(ref rect, Language.Day, 14, "#ffffff", TextAnchor.MiddleLeft);
				}
				else if (num5 < 5 && num5 > 1)
				{
					this.gui.CompositeTextClean(ref rect, Language.Days_dnya, 14, "#ffffff", TextAnchor.MiddleLeft);
				}
				else if (num5 > 4)
				{
					this.gui.CompositeTextClean(ref rect, Language.Days_dney, 14, "#ffffff", TextAnchor.MiddleLeft);
				}
				this.gui.Picture(new Vector2((float)(225 - this.gui.weapon_unlocked[num].width / 2), 93f), this.gui.weapon_unlocked[num]);
				if (num2 > 0)
				{
					this.gui.Picture(new Vector2(71f, 99f), this.gui.crIcon);
					this.gui.TextField(new Rect(16f, 86f, 50f, 50f), num2.ToString(), 22, "#ffffff", TextAnchor.MiddleRight, false, false);
				}
				if (num3 > 0)
				{
					this.gui.Picture(new Vector2(72f, 127f), this.gui.gldIcon);
					this.gui.TextField(new Rect(16f, 113f, 50f, 50f), num3.ToString(), 22, "#ffffff", TextAnchor.MiddleRight, false, false);
				}
				if ((double)num4 > 0.9)
				{
					this.gui.Picture(new Vector2(132f, 112f), this.gui.spIcon_med);
					this.gui.TextField(new Rect(76f, 101f, 50f, 50f), num4, 22, "#ffffff", TextAnchor.MiddleRight, false, false);
				}
				if ((num2 > 0 && num3 > 0) || (num2 > 0 && num4 > 0) || (num4 > 0 && num3 > 0))
				{
					this.gui.TextField(new Rect(98f, 98f, 50f, 50f), "+", 24, "#ffffff", TextAnchor.MiddleLeft, false, false);
				}
			}
			else
			{
				if (num3 > 0)
				{
					this.gui.TextField(new Rect(50f, 101f, 50f, 50f), num3.ToString(), 22, "#ffffff", TextAnchor.MiddleRight, false, false);
					this.gui.Picture(new Vector2(110f, 112f), this.gui.gldIcon);
				}
				if (num2 > 0 && num3 > 0)
				{
					this.gui.TextField(new Rect(150f, 98f, 50f, 50f), "+", 24, "#ffffff", TextAnchor.MiddleLeft, false, false);
				}
				if (num2 > 0)
				{
					this.gui.TextField(new Rect(170f, 101f, 50f, 50f), num2.ToString(), 22, "#ffffff", TextAnchor.MiddleRight, false, false);
					this.gui.Picture(new Vector2(230f, 112f), this.gui.crIcon);
				}
				if (num2 > 0 && num4 > 0)
				{
					this.gui.TextField(new Rect(280f, 98f, 50f, 50f), "+", 24, "#ffffff", TextAnchor.MiddleLeft, false, false);
				}
				if (num4 > 0)
				{
					this.gui.TextField(new Rect(290f, 101f, 50f, 50f), num4, 22, "#ffffff", TextAnchor.MiddleRight, false, false);
					this.gui.Picture(new Vector2(350f, 112f), this.gui.spIcon_med);
				}
			}
		}
		if (byUniquieToShow.popupState == PopupState.banned)
		{
			this.gui.Picture(new Vector2(35f, 12f), this.banned);
			if (Main.UserInfo.bannedUntil > 0)
			{
				this.gui.Picture(new Vector2(10f, 165f), this.carrierGUI.stats[1]);
				int seconds = Main.UserInfo.bannedUntil - HtmlLayer.serverUtc;
				this.gui.TextLabel(new Rect(40f, 168f, 300f, 50f), MainGUI.Instance.SecondsTostringDDHHMMSS(seconds), 16, "#FFFFFF", TextAnchor.UpperLeft, true);
			}
			if (!string.IsNullOrEmpty(Main.UserInfo.bannedReason))
			{
				this.gui.TextLabel(new Rect(30f, 22f, (float)this.banned.width, 50f), Language.Reason + " " + Main.UserInfo.bannedReason, 18, "#FFFFFF", TextAnchor.UpperLeft, true);
			}
			this.gui.TextLabel(new Rect(30f, 168f, (float)this.banned.width, 50f), "game user id: " + Main.UserInfo.userID, 16, "#FFFFFF", TextAnchor.UpperRight, true);
		}
		if (byUniquieToShow.error)
		{
			this.gui.Picture(new Vector2(375f, 147f), this.warning);
		}
		if (byUniquieToShow.closable && this.gui.Button(new Vector2(336f, 20f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			PopupGUI.showingNews = false;
			PopupGUI.showingDaliyBonus = false;
			byUniquieToShow.Hide(null, 0.15f, 0f);
			if (byUniquieToShow.closeMethod != string.Empty)
			{
				base.Invoke(byUniquieToShow.closeMethod, 0f);
			}
		}
		this.gui.TextField(new Rect(35f, 11f, (float)this.gui.weapon_info.width, 50f), byUniquieToShow.title, 19, "#000000", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(35f, 10f, (float)this.gui.weapon_info.width, 50f), byUniquieToShow.title, 19, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
		if (byUniquieToShow.popupState == PopupState.information)
		{
			this.gui.TextField(new Rect(35f, 60f, 380f, (float)this.gui.weapon_info.height), byUniquieToShow.desc, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
		}
		else if (byUniquieToShow.popupState == PopupState.progress)
		{
			this.gui.TextField(new Rect(12f, 60f, 395f, (float)this.gui.weapon_info.height), byUniquieToShow.desc, 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
			this.gui.EndGroup();
			float angle = 180f * Time.realtimeSinceStartup * 1.5f;
			Vector2 vector = new Vector2((float)(Screen.width / 2 - this.gui.settings_window[9].width / 2), (float)(Screen.height / 2 - this.gui.settings_window[9].height / 2 + 15));
			this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.gui.settings_window[9].width / 2), vector.y + (float)(this.gui.settings_window[9].height / 2)));
			this.gui.Picture(new Vector2(vector.x, vector.y), this.gui.settings_window[9]);
			this.gui.RotateGUI(0f, Vector2.zero);
			if (byUniquieToShow.progressName != string.Empty)
			{
				this.gui.TextField(new Rect((float)((Screen.width - this.gui.settings_window[9].width) / 2 - 3), (float)((Screen.height - this.gui.settings_window[9].height) / 2 + 15), (float)this.gui.settings_window[9].width, (float)this.gui.settings_window[9].height), (int)((double)Loader.Progress(byUniquieToShow.progressName) * 100.0), 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			}
			this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		}
		else if (byUniquieToShow.popupState == PopupState.invite)
		{
			this.gui.Picture(new Vector2(20f, 50f), this.invite[0]);
			if (CVars.realm != "mc" && this.gui.Button(new Vector2(193f, 136f), this.invite[1], this.invite[2], this.invite[2], Language.GatherTheTeam, 20, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				if (CVars.realm != "standalone")
				{
					Application.ExternalCall("Invite", new object[0]);
				}
				byUniquieToShow.Hide(null, 0.5f, 0.5f);
				if (byUniquieToShow.closeMethod != string.Empty)
				{
					base.Invoke(byUniquieToShow.closeMethod, 0f);
				}
			}
			this.gui.TextField(new Rect(193f, 135f, (float)this.invite[1].width, (float)this.invite[1].height), Language.GatherTheTeam, 20, "#000000", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(193f, 65f, (float)this.invite[1].width, (float)(this.invite[1].height + 10)), Language.FriendText, 14, "#ffffff", TextAnchor.UpperLeft, false, false);
		}
		else if (byUniquieToShow.popupState == PopupState.buyGp)
		{
			int num6 = 0;
			int num7 = 0;
			string arg = string.Empty;
			if (CVars.realm == "fb")
			{
				num6 = (int)(Main.UserInfo.CurrencyInfo.AmounGp[0] * (float)Main.UserInfo.CurrencyInfo.MultiplierGp);
				num7 = (int)Main.UserInfo.CurrencyInfo.PricesGp[0];
				string text3 = Main.UserInfo.CurrencyInfo.CurrencySymbol;
				if (text3 == string.Empty)
				{
					text3 = Main.UserInfo.CurrencyInfo.CurrencyName;
				}
				arg = text3;
			}
			else
			{
				object obj;
				if (Globals.I.CurrentBank.TryGetValue("2", out obj))
				{
					using (Dictionary<string, object>.Enumerator enumerator = ((Dictionary<string, object>)obj).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							KeyValuePair<string, object> keyValuePair = enumerator.Current;
							num6 = int.Parse(keyValuePair.Key);
							num7 = int.Parse(keyValuePair.Value.ToString());
						}
					}
				}
				arg = ((!CVars.realm.Equals("kg")) ? "$" : "KR");
			}
			this.gui.Picture(new Vector2(20f, 50f), this.buyGp);
			if (this.gui.Button(new Vector2(193f, 136f), this.invite[1], this.invite[2], this.invite[2], Language.GetGpNow, 20, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				if (CVars.IsStandaloneRealm)
				{
					Main.AddDatabaseRequest<CWSABuyRequest>(new object[]
					{
						true,
						1
					});
				}
				else if (CVars.realm == "vk" || CVars.realm == "omega" || CVars.realm == "local" || CVars.realm == "release" || CVars.realm == "kg" || CVars.realm == "fr")
				{
					Application.ExternalCall("Payment", new object[]
					{
						"gp-" + num6
					});
				}
				else if (CVars.realm == "fb")
				{
					Application.ExternalCall("Payment", new object[]
					{
						"gp",
						num6 / Main.UserInfo.CurrencyInfo.MultiplierGp
					});
				}
				else if (CVars.realm == "ag" || CVars.realm == "mc")
				{
					Application.ExternalCall("Payment", new object[]
					{
						num7,
						LoadProfile.XsollaToken
					});
				}
				byUniquieToShow.Hide(null, 0.5f, 0.5f);
				if (byUniquieToShow.closeMethod != string.Empty)
				{
					base.Invoke(byUniquieToShow.closeMethod, 0f);
				}
			}
			this.gui.TextField(new Rect(193f, 135f, (float)this.invite[1].width, (float)this.invite[1].height), Language.GetGpNow, 20, "#000000", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(48f, 70f, (float)this.invite[1].width, (float)this.invite[1].height), num6 + " GP", 40, "#f49b01", TextAnchor.MiddleRight, false, false);
			this.gui.TextField(new Rect(190f, 52f, (float)this.invite[1].width, (float)this.invite[1].height), "only", 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(190f, 75f, (float)this.invite[1].width, (float)this.invite[1].height), "FOR", 30, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.TextField(new Rect(333f, 70f, (float)this.invite[1].width, (float)this.invite[1].height), num7 + " " + arg, 40, "#f49b01", TextAnchor.MiddleLeft, false, false);
		}
		else if (byUniquieToShow.popupState == PopupState.news)
		{
			PopupGUI.showingNews = true;
			string desc = byUniquieToShow.desc;
			string text4 = string.Empty;
			Regex regex = new Regex("http://([\\w+?\\.\\w+])+([ky-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
			Match match = regex.Match(byUniquieToShow.desc);
			if (match.Success)
			{
				text4 = match.Groups[0].Value;
				if (this.gui.Button(new Vector2(25f, 162f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FollowByLink, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					if (CVars.IsStandaloneRealm)
					{
						Application.OpenURL(text4);
					}
					else
					{
						Application.ExternalEval("window.open('" + text4 + "')");
					}
				}
			}
			this.gui.TextField(new Rect(35f, 60f, 380f, (float)this.gui.weapon_info.height), desc, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2(23f, 160f), this.news_stripe);
			if (CVars.realm != "kg" && this.gui.Button(new Vector2(220f, 162f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.InOtherNews, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				string text2 = CVars.realm;
				switch (text2)
				{
				case "standalone":
				{
					string url = (Language.CurrentLanguage != ELanguage.EN) ? "'http://vkontakte.ru/cwclub'" : "'https://www.facebook.com/ContractWars'";
					Application.OpenURL(url);
					break;
				}
				case "ok":
					Application.ExternalEval("window.open('http://www.odnoklassniki.ru/group/51290377093266')");
					break;
				case "vk":
					Application.ExternalEval("window.open('http://vkontakte.ru/cwclub')");
					break;
				case "fb":
					Application.ExternalEval("window.open('https://www.facebook.com/ContractWars')");
					break;
				case "mailru":
					Application.ExternalEval("window.open('http://my.mail.ru/community/cwarsclub/')");
					break;
				}
			}
		}
		else if (byUniquieToShow.popupState == PopupState.kitUnlock)
		{
			string textUnlockKit = Language.TextUnlockKit;
			this.gui.TextField(new Rect(35f, 60f, 380f, (float)this.gui.weapon_info.height), textUnlockKit, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
			if (Main.UserInfo.GP >= Globals.I.weaponSetPrices[BuyKit.KitIndexCached])
			{
				this.gui.TextField(new Rect(122f, 119f, 200f, 20f), Language.UnlockFor, 15, "#666666", TextAnchor.MiddleCenter, false, false);
				ButtonState buttonState = this.gui.Button(new Vector2(179f, 144f), this.gui.server_window[14], this.gui.server_window[15], null, Globals.I.weaponSetPrices[BuyKit.KitIndexCached].ToString() + "   -", 18, "#fbc421", TextAnchor.MiddleCenter, null, null, null, null);
				this.gui.Picture(new Vector2(228f, 147f), this.gui.gldIcon);
				if (buttonState.Clicked && !byUniquieToShow.alreadyClicked)
				{
					byUniquieToShow.alreadyClicked = true;
					byUniquieToShow.Hide(null, 0f, 0f);
					Main.AddDatabaseRequest<BuyKit>(new object[]
					{
						BuyKit.KitIndexCached
					});
				}
			}
			else
			{
				this.gui.TextField(new Rect(122f, 119f, 200f, 20f), Language.InsufficientFunds + ":", 15, "#cccccc", TextAnchor.MiddleCenter, false, false);
				if (this.gui.Button(new Vector2(123f, 145f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					byUniquieToShow.Hide(null, 0f, 0f);
					EventFactory.Call("ShowBankGUI", null);
				}
			}
		}
		else if (byUniquieToShow.popupState == PopupState.setUnlock)
		{
			string textUnlockSet = Language.TextUnlockSet;
			this.gui.TextField(new Rect(35f, 60f, 380f, (float)this.gui.weapon_info.height), textUnlockSet, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
			if (Main.UserInfo.GP >= Globals.I.unlockSetPrices[BuySet.setIndexCached])
			{
				this.gui.TextField(new Rect(122f, 119f, 200f, 20f), Language.UnlockFor, 15, "#666666", TextAnchor.MiddleCenter, false, false);
				ButtonState buttonState2 = this.gui.Button(new Vector2(179f, 144f), this.gui.server_window[14], this.gui.server_window[15], null, Globals.I.unlockSetPrices[BuySet.setIndexCached].ToString() + "   -", 18, "#fbc421", TextAnchor.MiddleCenter, null, null, null, null);
				this.gui.Picture(new Vector2(230f, 147f), this.gui.gldIcon);
				if (buttonState2.Clicked && !byUniquieToShow.alreadyClicked)
				{
					byUniquieToShow.alreadyClicked = true;
					byUniquieToShow.Hide(null, 0f, 0f);
					Main.AddDatabaseRequest<BuySet>(new object[]
					{
						BuySet.setIndexCached
					});
				}
			}
			else
			{
				this.gui.TextField(new Rect(90f, 131f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleCenter, false, false);
				this.gui.TextField(new Rect(207f, 131f, 200f, 20f), Globals.I.unlockSetPrices[BuySet.setIndexCached].ToString(), 15, "#ffffff", TextAnchor.MiddleCenter, false, false);
				this.gui.Picture(new Vector2(322f, 130f), this.gui.gldIcon);
				if (this.gui.Button(new Vector2(123f, 160f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					byUniquieToShow.Hide(null, 0f, 0f);
					EventFactory.Call("ShowBankGUI", null);
				}
			}
		}
		else if (byUniquieToShow.popupState == PopupState.wtaskUnlock)
		{
			string textUnlockWTask = Language.TextUnlockWTask;
			this.gui.TextField(new Rect(35f, 60f, 380f, (float)this.gui.weapon_info.height), textUnlockWTask, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
			if (Main.UserInfo.GP >= Main.UserInfo.weaponsStates[BuyWtask.unlockWtaskCached].CurrentWeapon.wtaskPrice)
			{
				this.gui.TextField(new Rect(122f, 119f, 200f, 20f), Language.BuyModFor, 15, "#666666", TextAnchor.MiddleCenter, false, false);
				ButtonState buttonState3 = this.gui.Button(new Vector2(179f, 144f), this.gui.server_window[14], this.gui.server_window[15], null, Main.UserInfo.weaponsStates[BuyWtask.unlockWtaskCached].CurrentWeapon.wtaskPrice.ToString() + "   -", 18, "#fbc421", TextAnchor.MiddleCenter, null, null, null, null);
				this.gui.Picture(new Vector2(230f, 147f), this.gui.gldIcon);
				if (buttonState3.Clicked && !byUniquieToShow.alreadyClicked)
				{
					byUniquieToShow.alreadyClicked = false;
					byUniquieToShow.Hide(null, 0f, 0f);
					Main.AddDatabaseRequest<BuyWtask>(new object[]
					{
						BuyWtask.unlockWtaskCached
					});
				}
			}
			else
			{
				this.gui.TextField(new Rect(90f, 131f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleCenter, false, false);
				this.gui.TextField(new Rect(207f, 131f, 200f, 20f), Main.UserInfo.weaponsStates[BuyWtask.unlockWtaskCached].CurrentWeapon.wtaskPrice.ToString(), 15, "#ffffff", TextAnchor.MiddleCenter, false, false);
				this.gui.Picture(new Vector2(322f, 130f), this.gui.gldIcon);
				if (this.gui.Button(new Vector2(123f, 160f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					byUniquieToShow.Hide(null, 0f, 0f);
					EventFactory.Call("ShowBankGUI", null);
				}
			}
		}
		else if (byUniquieToShow.popupState == PopupState.unlockSkill)
		{
			int num9 = 22;
			int num10 = 47;
			int num11 = 0;
			bool flag = false;
			bool flag2 = false;
			if (this.selectIndex < 0)
			{
				this.selectIndex = 0;
			}
			this.gui.Picture(new Vector2(24f, 130f), this.gui.GetComponent<HelpersGUI>().lowBar);
			if (Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentTime != null)
			{
				this.gui.TextLabel(new Rect(270f, 112f, 100f, 40f), Language.RentSkill, 15, "#999999", TextAnchor.LowerRight, true);
				for (int i = 0; i < Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice.Length; i++)
				{
					Texture2D texture2D = (i != this.selectIndex) ? this.gui.GetComponent<HelpersGUI>().checkbox[0] : this.gui.GetComponent<HelpersGUI>().checkbox[1];
					if (this.gui.Button(new Vector2(263f, (float)(57 + i * 21)), texture2D, texture2D, texture2D, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						this.selectIndex = i;
					}
					this.gui.TextLabel(new Rect(293f, (float)(57 + i * 21), 100f, 22f), Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentTime[i] + " " + Language.Days_dney, 16, "#FFFFFF", TextAnchor.UpperLeft, true);
					this.gui.TextLabel(new Rect(288f, (float)(57 + i * 21), 100f, 22f), Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice[i], 16, "#fbc421", TextAnchor.UpperRight, true);
					this.gui.Picture(new Vector2(389f, (float)(57 + i * 21)), this.gui.gldIcon);
					if (Main.UserInfo.skillsInfos.Length > this.carrierGUI.GetSelectedSkill() && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice != null && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice.Length > this.selectIndex)
					{
						num11 = Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice[this.selectIndex];
					}
				}
				if (Main.UserInfo.skillsInfos.Length > this.carrierGUI.GetSelectedSkill() && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice != null && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice.Length > this.selectIndex)
				{
					if (Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].isPremium)
					{
						flag2 = true;
						if (Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice[this.selectIndex] <= Main.UserInfo.GP)
						{
							flag = true;
						}
					}
					else
					{
						flag2 = false;
						if (Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].rentPrice[this.selectIndex] <= Main.UserInfo.CR)
						{
							flag = true;
						}
					}
				}
				this.gui.TextLabel(new Rect(-25f, 145f, 100f, 40f), Language.Price, 25, (!flag) ? "#FF0000" : "#FFFFFF", TextAnchor.LowerRight, true);
				this.gui.TextLabel(new Rect(30f, 145f, 100f, 40f), num11, 25, (!flag) ? "#FF0000" : ((!flag2) ? "#FFFFFF" : "#fbc421"), TextAnchor.LowerRight, true);
				this.gui.Picture(new Vector2(133f, 155f), (!Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].isPremium) ? this.gui.crIcon : this.gui.gldIcon);
			}
			else
			{
				string str = Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].CR.ToString();
				string str2 = Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].GP.ToString();
				this.selectIndex = -1;
				this.gui.TextLabel(new Rect((float)(num9 + 10), (float)(num10 + 82), 100f, 50f), Language.Cost, 14, "#92C5FF", TextAnchor.UpperLeft, true);
				this.gui.TextLabel(new Rect((float)(num9 - 59), (float)(num10 + 97), 100f, 50f), Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].SP, 30, "#FFFFFF", TextAnchor.MiddleRight, true);
				this.gui.TextLabel(new Rect((float)(num9 + 29), (float)(num10 + 100), 100f, 20f), Helpers.SeparateNumericString(str), 16, "#FFFFFF", TextAnchor.MiddleRight, true);
				this.gui.TextLabel(new Rect((float)(num9 + 29), (float)(num10 + 120), 100f, 20f), Helpers.SeparateNumericString(str2), 16, "#FFFFFF", TextAnchor.MiddleRight, true);
				flag = (Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].SP <= Main.UserInfo.SP && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].GP <= Main.UserInfo.GP && Main.UserInfo.skillsInfos[this.carrierGUI.GetSelectedSkill()].CR <= Main.UserInfo.CR);
				this.gui.Picture(new Vector2((float)(num9 + 50), (float)(num10 + 108)), this.carrierGUI.Class_backgr_description[1]);
				this.gui.Picture(new Vector2((float)(num9 + 133), (float)(num10 + 98)), this.carrierGUI.Class_backgr_description[2]);
				this.gui.Picture(new Vector2((float)(num9 + 134), (float)(num10 + 119)), this.carrierGUI.Class_backgr_description[3]);
				this.gui.TextLabel(new Rect(270f, 112f, 100f, 40f), Language.UnlockQuestion, 15, "#999999", TextAnchor.LowerRight, true);
			}
			this.gui.Picture(new Vector2(38f, 74f), this.carrierGUI.Class_ICON[this.carrierGUI.GetSelectedProf()]);
			this.gui.Picture(new Vector2(178f, 64f), this.carrierGUI.Class_skills[this.carrierGUI.GetSelectedSkill()]);
			this.gui.Picture(new Vector2(144f, 28f), this.carrierGUI.Class_skill_button[3]);
			if (flag && this.lockButton != this.carrierGUI.GetSelectedSkill() && this.gui.Button(new Vector2(227f, 151f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.Yes, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound).Clicked)
			{
				this.lockButton = this.carrierGUI.GetSelectedSkill();
				Main.AddDatabaseRequest<UnlockSkill>(new object[]
				{
					this.carrierGUI.GetSelectedSkill(),
					this.selectIndex
				});
				byUniquieToShow.Hide(null, 0.15f, 0f);
				if (byUniquieToShow.closeMethod != string.Empty)
				{
					base.Invoke(byUniquieToShow.closeMethod, 0f);
				}
			}
			if (this.gui.Button(new Vector2(321f, 151f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.No, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				byUniquieToShow.Hide(null, 0.15f, 0f);
				if (byUniquieToShow.closeMethod != string.Empty)
				{
					base.Invoke(byUniquieToShow.closeMethod, 0f);
				}
			}
		}
		else if (byUniquieToShow.popupState == PopupState.resetSkills)
		{
			MainGUI gui2 = this.gui;
			int num8 = Main.UserInfo.totalSPspent * Globals.I.skillResetPrice;
			this.tmpwidth = gui2.CalcWidth(num8.ToString(), this.gui.fontDNC57, 16);
			bool flag3 = Main.UserInfo.totalSPspent * Globals.I.skillResetPrice <= Main.UserInfo.GP;
			bool flag4 = Main.UserInfo.totalSPspent * Globals.I.skillResetPriceCR <= Main.UserInfo.CR;
			this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
			this.gui.TextField(new Rect(0f, 0f, 370f, 100f), Language.TextResetSkills, 16, "#c0c0c0", TextAnchor.UpperCenter, false, false);
			Rect rect2 = new Rect(110f, 40f, 80f, 30f);
			this.gui.CompositeTextClean(ref rect2, Language.ResetSkillYouGet, 16, "#c0c0c0", TextAnchor.UpperCenter);
			this.gui.CompositeTextClean(ref rect2, Main.UserInfo.totalSPspent + " SP", 16, "#00aeff", TextAnchor.UpperCenter);
			this.gui.CompositeTextClean(ref rect2, Main.UserInfo.skillResetGPRefund + " GP", 16, "#ffa200", TextAnchor.UpperCenter);
			this.gui.TextField(new Rect(125f, 60f, 120f, 30f), Language.ResetSkillsFor, 16, "#717171", TextAnchor.UpperLeft, false, false);
			this.unlock = this.gui.Button(new Vector2(80f, 80f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], string.Empty, 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
			this.gui.TextField(new Rect(63f, 80f, 120f, 30f), Main.UserInfo.totalSPspent * Globals.I.skillResetPrice, 16, (!flag3) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			this.gui.Picture(new Vector2(80f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.gldIcon.height) / 2 + 1)), this.gui.gldIcon);
			this.gui.TextField(new Rect(155f, 80f, 50f, 30f), Language.Or, 16, "#717171", TextAnchor.MiddleCenter, false, false);
			this.unlock2 = this.gui.Button(new Vector2(200f, 80f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], string.Empty, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
			this.gui.TextField(new Rect(175f, 80f, 120f, 30f), Main.UserInfo.totalSPspent * Globals.I.skillResetPriceCR, 16, (!flag4) ? "#ff0000" : "#ffffff", TextAnchor.MiddleCenter, false, false);
			this.gui.Picture(new Vector2(210f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.crIcon.height) / 2)), this.gui.crIcon);
			if (this.unlock2.Clicked && !byUniquieToShow.alreadyClicked)
			{
				if (flag4)
				{
					byUniquieToShow.alreadyClicked = true;
					Main.AddDatabaseRequest<ResetSkills>(new object[]
					{
						2
					});
					this.carrierGUI.ResetAllSkills();
					byUniquieToShow.Hide(null, 0.15f, 0f);
					if (byUniquieToShow.closeMethod != string.Empty)
					{
						base.Invoke(byUniquieToShow.closeMethod, 0f);
					}
					this.lockButton = -1;
				}
				else
				{
					byUniquieToShow.Hide(null, 0.15f, 0f);
					EventFactory.Call("ShowBankGUI", null);
				}
			}
			if (this.unlock.Clicked && !byUniquieToShow.alreadyClicked)
			{
				if (flag3)
				{
					byUniquieToShow.alreadyClicked = true;
					Main.AddDatabaseRequest<ResetSkills>(new object[0]);
					this.carrierGUI.ResetAllSkills();
					byUniquieToShow.Hide(null, 0.15f, 0f);
					if (byUniquieToShow.closeMethod != string.Empty)
					{
						base.Invoke(byUniquieToShow.closeMethod, 0f);
					}
					this.lockButton = -1;
				}
				else
				{
					byUniquieToShow.Hide(null, 0.15f, 0f);
					EventFactory.Call("ShowBankGUI", null);
				}
			}
			this.gui.TextField(new Rect(0f, 115f, 370f, 30f), Language.ResetSkillAttention, 16, "#717171", TextAnchor.MiddleCenter, false, false);
			this.gui.EndGroup();
		}
		else if (byUniquieToShow.popupState == PopupState.buySP)
		{
			this.BuySP(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.dailyBonus)
		{
			this.DailyBonus(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.XPBonus)
		{
			this.XPBonus(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.PersonalDiscount)
		{
			this.PersonalDiscount(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyNickCnage)
		{
			this.BuyNicknameChange(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyNickColorCnage)
		{
			this.BuyNickColorChange(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyBox)
		{
			this.BuyBoxfunc(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.createClan)
		{
			this.CreateClan(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.extendClan)
		{
			this.ExtendClan(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.discardAllRequest)
		{
			this.DiscardAllRequest(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.leaveClan)
		{
			this.LeaveClan(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.dismissWarrior)
		{
			this.DismissWarrior(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.fillUpClanBalance)
		{
			this.FillUpClanBalance(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.clanError)
		{
			this.ClanError(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.unlockClanSkill)
		{
			this.UnlockClanSkill(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.quickGame)
		{
			this.QuickGame(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.StandaloneLogin)
		{
			this.StandaloneLogin(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.StandaloneQuit)
		{
			this.StandaloneQuit(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.auth)
		{
			this.Auth(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.halloffamepopup)
		{
			this.HOFPopup(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.leagueWinner)
		{
			this.LeagueWinnerPopup(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyAttempt)
		{
			this.BuyAttempt(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.updateContracts)
		{
			this.PerformContracts(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.skipContract)
		{
			this.SkipContract(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.editClanInfo)
		{
			this.EditClanInfo(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.setClanRole)
		{
			this.SetClanRole(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.editClanMessage)
		{
			this.EditClanMessage(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.favorite)
		{
			this.Favorite(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.leagueLoading)
		{
			this.LeagueLoading(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.leagueNotification)
		{
			this.LeagueNotification(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.repairAllWeapon)
		{
			this.RepairAllWeapon(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyMasteringMetaLevel)
		{
			this.BuyMasteringMetaLevel(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyMasteringMod)
		{
			this.BuyMasteringMod(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.purchaseCamouflage)
		{
			this.PurchaseCamouflage(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.precentageWeaponsProgress || byUniquieToShow.popupState == PopupState.precentageMapsProgress)
		{
			this.DownloadedPrecentage(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.buyMp)
		{
			this.BuyMp(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.SeasonAward)
		{
			this.SeasonAwardPopup(byUniquieToShow);
		}
		else if (byUniquieToShow.popupState == PopupState.WonHopsKey)
		{
			this.WonHopsKeyPopup(byUniquieToShow);
		}
		this.gui.EndGroup();
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x0006DB9C File Offset: 0x0006BD9C
	private void WonHopsKeyPopup(Popup popup)
	{
		this.gui.TextField(new Rect(35f, 48f, 380f, (float)this.gui.weapon_info.height), popup.desc, 16, "#c1c1c1", TextAnchor.UpperLeft, false, false);
		Texture2D texture2D = this.gui.settings_window[0];
		Texture2D over = this.gui.settings_window[1];
		Rect rect = new Rect(68f, 93f, (float)texture2D.width, (float)texture2D.height);
		this.gui.TextField(rect, Main.UserInfo.HopsRoulettePrizeKey, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		if (this.gui.Button(new Vector2(rect.x, rect.y), texture2D, over, null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			TextEditor textEditor = new TextEditor
			{
				content = new GUIContent(Main.UserInfo.HopsRoulettePrizeKey)
			};
			textEditor.SelectAll();
			textEditor.Copy();
		}
		this.gui.TextField(new Rect(217f, 128f, this.rect.width, 20f), Language.SettingsHopsBonusHint, 12, "#00FF00", TextAnchor.MiddleCenter, false, false);
		if (this.gui.Button(new Vector2(120f, 148f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.HopsActivationInstruction, 14, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			if (Application.isWebPlayer)
			{
				Application.ExternalCall("window.open", new object[]
				{
					CVars.HopsActivationInstructionUrl
				});
			}
			else
			{
				Application.OpenURL(CVars.HopsActivationInstructionUrl);
			}
		}
	}

	// Token: 0x060009F5 RID: 2549 RVA: 0x0006DDA4 File Offset: 0x0006BFA4
	private void ExitGame()
	{
		Main.ExitGame();
	}

	// Token: 0x060009F6 RID: 2550 RVA: 0x0006DDAC File Offset: 0x0006BFAC
	private IEnumerator DownloadAward(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		this._award = www.texture;
		this._awardLoaded = true;
		yield break;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0006DDD8 File Offset: 0x0006BFD8
	private void LeagueWinnerPopup(Popup popup)
	{
		if (this._lwpInfo == null)
		{
			this._lwpInfo = new PopupGUI.LWPInfo(popup.args[0]);
		}
		if (!this._awardLoaded && !string.IsNullOrEmpty((string)this._lwpInfo.url))
		{
			base.StartCoroutine(this.DownloadAward((string)this._lwpInfo.url));
		}
		if (this._award != null)
		{
			this.LWPPos.medal = this._award;
		}
		GUI.BeginGroup(new Rect(0f, 47f, 421f, 145f));
		this.LWPPos.DrawTexture(ref this.LWPPos.BackgroundRect, this.LWPPos.background);
		this.LWPPos.DrawTexture(ref this.LWPPos.MedalRect, this.LWPPos.medal);
		LeagueHelpers.DrawRank(this.LWPPos.RankRect.x, this.LWPPos.RankRect.y, (int)this._lwpInfo.lp, 1f, true);
		GUI.Label(this.LWPPos.LabelRects[0], this._lwpInfo.season.ToString().ToUpper(), this.LWPPos.StyleKorataki);
		GUI.Label(this.LWPPos.LabelRects[1], this._lwpInfo.place + " " + Language.LeaguePlace.ToUpper(), this.LWPPos.StyleKorataki);
		GUI.Label(this.LWPPos.LabelRects[2], Language.LeaguePopupYourResults, this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[3], Language.LeaguePopupYourRewards, this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[4], this._lwpInfo.place.ToString(), this.LWPPos.StyleDYNCWhite);
		GUI.Label(this.LWPPos.LabelRects[5], this._lwpInfo.lp.ToString(), this.LWPPos.StyleDYNCWhite);
		GUI.Label(this.LWPPos.LabelRects[6], this._lwpInfo.wins.ToString(), this.LWPPos.StyleDYNCWhite);
		GUI.Label(this.LWPPos.LabelRects[7], this._lwpInfo.loss.ToString(), this.LWPPos.StyleDYNCWhite);
		GUI.Label(this.LWPPos.LabelRects[8], this._lwpInfo.leav.ToString(), this.LWPPos.StyleDYNCWhite);
		this.LWPPos.StyleDYNCBrown.alignment = TextAnchor.MiddleCenter;
		GUI.Label(this.LWPPos.LabelRects[9], Language.LeagueRank.ToLower(), this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[10], Language.LeaguePlace, this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[11], "LP", this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[12], Language.LeagueRatingHeaderWins.ToLower(), this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[13], Language.LeagueRatingHeaderDefeats.ToLower(), this.LWPPos.StyleDYNCBrown);
		GUI.Label(this.LWPPos.LabelRects[14], Language.LeagueRatingHeaderLeaves.ToLower(), this.LWPPos.StyleDYNCBrown);
		if ((int)this._lwpInfo.place < 4)
		{
			GUI.Label(this.LWPPos.LabelRects[15], "+", this.LWPPos.StyleDYNCWhite);
		}
		GUI.Label(((int)this._lwpInfo.place <= 3) ? this.LWPPos.LabelRects[16] : this.LWPPos.LabelRects[18], this._lwpInfo.sum.ToString(), this.LWPPos.StyleDYNCGold);
		GUI.DrawTexture(((int)this._lwpInfo.place <= 3) ? this.LWPPos.LabelRects[17] : this.LWPPos.LabelRects[19], MainGUI.Instance.gldIcon);
		this.LWPPos.StyleDYNCBrown.alignment = TextAnchor.MiddleLeft;
		CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn.fontSize = 16;
		this.btnClick = GUI.Button(new Rect(327f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		GUI.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0006E42C File Offset: 0x0006C62C
	private void HOFPopup(Popup popup)
	{
		this.hofPositions.DrawTexture(ref this.hofPositions.Background, this.hofPositions.TextureBackground);
		this.hofPositions.DrawTexture(ref this.hofPositions.BackgroundIcon, this.hofPositions.TextureBackIcon);
		this.hofPositions.DrawTexture(ref this.hofPositions.HOFGroup, this.hofPositions.TextureHOFGroup);
		this.hofPositions.DrawTexture(ref this.hofPositions.NominationIcon, this.hofPositions.TextureNominationIcon);
		this.hofPositions.DrawTexture(ref this.hofPositions.Level, this.hofPositions.TextureLVL);
		this.hofPositions.DrawTexture(ref this.hofPositions.Class, this.hofPositions.TextureClass);
		this.hofPositions.DrawTexture(ref this.hofPositions.Avatar, this.hofPositions.TextureAvatar);
		this.gui.TextField(this.hofPositions.Month, "Декабрь 2012".ToUpper(), 14, "#FFFFFF_Micra", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(this.hofPositions.ClanTag, "WWS", 16, "#FFFFFF", TextAnchor.MiddleRight, false, false);
		this.gui.TextField(this.hofPositions.Nick, "Zlojopa2", 16, "#FFFFFF", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(this.hofPositions.Name, "Леонид Петренко", 14, "#808080", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(this.hofPositions.Nomination, "Лучший по K/D".ToUpper(), 16, "#3eb4ff", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(this.hofPositions.Value, "5.14", 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		if (this.gui.Button(new Vector2(this.hofPositions.HOFButton.x, this.hofPositions.HOFButton.y), this.hofPositions.TextureButtonIdle, this.hofPositions.TextureButtonOver, this.hofPositions.TextureButtonOver, "ЗАЛ СЛАВЫ", 16, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			popup.Hide(null, 0.15f, 0f);
			EventFactory.Call("ShowHallOfFameGUI", null);
		}
		if (this.gui.Button(new Vector2(this.hofPositions.OKButton.x, this.hofPositions.OKButton.y), this.hofPositions.TextureButtonIdle, this.hofPositions.TextureButtonOver, this.hofPositions.TextureButtonOver, "OK", 16, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			popup.Hide(null, 0.15f, 0f);
		}
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0006E73C File Offset: 0x0006C93C
	private void StandaloneLogin(Popup popup)
	{
		MainGUI.Instance.textMaxSize = 100;
		Rect dialogRect = new Rect(50f, 40f, 355f, 155f);
		this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 42f, 200f, 25f), Language.StandaloneMailTextFieldCaption.ToLower(), 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.gui.Picture(new Vector2(dialogRect.xMin + dialogRect.width / 2f - 100f, 62f), this.serverGUI.servername_field);
		Main.StandaloneMail = this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 62f, 200f, 25f), Main.StandaloneMail, 16, "#FFFFFF", TextAnchor.UpperCenter, true, true);
		this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 92f, 200f, 25f), Language.StandalonePassTextFieldCaption, 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.gui.Picture(new Vector2(dialogRect.xMin + dialogRect.width / 2f - 100f, 112f), this.serverGUI.servername_field);
		this.ShowHidePassButton();
		Main.SavePass = GUI.Toggle(new Rect(335f, 113f, 26f, 22f), Main.SavePass, string.Empty, CWGUI.p.smallCheckBox);
		this.gui.TextField(new Rect(355f, 109f, 50f, 22f), Language.SavePassword, 10, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.PassField(dialogRect);
		this.LanguageSwitcher(dialogRect);
		this.StandaloneLoginState = this.gui.Button(new Vector2(dialogRect.width / 2f + 48f, dialogRect.height - 10f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.StandaloneSignIn, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		if (this.StandaloneLoginState.Clicked && this._canSendLogin && (!this._sendLoginCoolDownTimer.IsStarted || this._sendLoginCoolDownTimer.Time > 2f))
		{
			this._sendLoginCoolDownTimer.Start();
			this._canSendLogin = false;
			Login login = SingletoneComponent<Main>.Instance.GetComponentInChildren<Login>();
			if (!this._useHashedPass || !Main.PassHashed)
			{
				Main.StandalonePass = this._pass;
				Main.PassHashed = false;
			}
			Main.AddDatabaseRequestCallBack<StandaloneLoginRequest>(delegate
			{
				Main.StandalonePass = string.Empty;
				login.SuccessAction();
				Main.StandaloneLogined = true;
				this._loginFailed = false;
				EventFactory.Call("HidePopup", new Popup(WindowsID.Invite, Language.StandaloneLoginCaption, string.Empty, PopupState.StandaloneLogin, false, true, "ExitGame", string.Empty));
				EventFactory.Call("StartSplash", null);
			}, delegate
			{
				login.FailedAction();
				this._canSendLogin = true;
				this._loginFailed = true;
				this._attemptCooldown.Start();
			}, new object[]
			{
				Main.StandaloneMail,
				Main.StandalonePass
			});
		}
		this.StandaloneLoginState = this.gui.Button(new Vector2(dialogRect.width / 2f - 48f, dialogRect.height - 10f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.StandaloneSignUp, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		if (this.StandaloneLoginState.Clicked)
		{
			Application.OpenURL(CVars.StandaloneRegistartionUrl);
		}
		if (this._loginFailed)
		{
			this.OnLoginFailed(dialogRect);
		}
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0006EB44 File Offset: 0x0006CD44
	private void OnLoginFailed(Rect dialogRect)
	{
		StandaloneLoginRequest componentInChildren = SingletoneComponent<Main>.Instance.GetComponentInChildren<StandaloneLoginRequest>();
		int failresult = componentInChildren.Failresult;
		int timeLeft = componentInChildren.TimeLeft;
		int num = failresult;
		string text;
		if (num != 1006)
		{
			if (num != 1007)
			{
				if (num != 1001)
				{
					text = Language.UnknownReasonLoginFail;
				}
				else
				{
					text = Language.WrongLoginData;
				}
			}
			else
			{
				text = Language.RetypePassword;
			}
		}
		else
		{
			int num2 = (int)Mathf.Clamp((float)timeLeft - this._attemptCooldown.Time, 0f, (float)timeLeft);
			this._canSendLogin = (num2 == 0);
			if (this._canSendLogin)
			{
				text = string.Empty;
			}
			else
			{
				text = string.Concat(new object[]
				{
					Language.LoginAttemptsExceeded,
					num2,
					" ",
					Language.Seconds
				});
			}
		}
		this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 103f, dialogRect.height + 40f, 200f, 25f), text, 12, "#FF0000", TextAnchor.UpperCenter, false, false);
	}

	// Token: 0x060009FB RID: 2555 RVA: 0x0006EC78 File Offset: 0x0006CE78
	private void LanguageSwitcher(Rect dialogRect)
	{
		float num = dialogRect.width / 2f - 144f;
		float num2 = dialogRect.height - 9f;
		this.gui.Picture(new Vector2(num, num2), this.gui.SwitchLanguageSub);
		ELanguage currentLanguage = Language.CurrentLanguage;
		ButtonState buttonState;
		if (currentLanguage != ELanguage.EN)
		{
			if (currentLanguage == ELanguage.RU)
			{
				buttonState = this.gui.Button(new Vector2(num - 4f, num2 - 3f), this.gui.SwitchLanguageIdle, this.gui.SwitchLanguageOver, this.gui.SwitchLanguagePressed, "RUS", 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				goto IL_127;
			}
		}
		buttonState = this.gui.Button(new Vector2(num + 26f, num2 - 3f), this.gui.SwitchLanguageIdle, this.gui.SwitchLanguageOver, this.gui.SwitchLanguagePressed, "ENG", 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		IL_127:
		if (buttonState.Clicked)
		{
			currentLanguage = Language.CurrentLanguage;
			if (currentLanguage != ELanguage.EN)
			{
				if (currentLanguage == ELanguage.RU)
				{
					Language.CurrentLanguage = ELanguage.EN;
					return;
				}
			}
			Language.CurrentLanguage = ELanguage.RU;
		}
	}

	// Token: 0x060009FC RID: 2556 RVA: 0x0006EDE8 File Offset: 0x0006CFE8
	private void RealmSwitcher(Rect dialogRect)
	{
		float num = dialogRect.width / 2f + 148f;
		float num2 = dialogRect.height - 9f;
		this.gui.Picture(new Vector2(num, num2), this.gui.SwitchLanguageSub);
		ButtonState buttonState;
		if (CVars.IsVanilla)
		{
			buttonState = this.gui.Button(new Vector2(num - 4f, num2 - 3f), this.gui.SwitchLanguageIdle, this.gui.SwitchLanguageOver, this.gui.SwitchLanguagePressed, "Vanilla", 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		}
		else
		{
			buttonState = this.gui.Button(new Vector2(num + 26f, num2 - 3f), this.gui.SwitchLanguageIdle, this.gui.SwitchLanguageOver, this.gui.SwitchLanguagePressed, "Actual", 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		}
		if (buttonState.Clicked)
		{
			CVars.IsVanilla = !CVars.IsVanilla;
			if (CVars.IsVanilla)
			{
				Globals.I.SetVanillaBackEnd();
			}
			else
			{
				Globals.I.SetActualBackEnd();
			}
		}
	}

	// Token: 0x060009FD RID: 2557 RVA: 0x0006EF44 File Offset: 0x0006D144
	private void ShowHidePassButton()
	{
		Texture2D texture2D = (!this._showPass) ? this.gui.PassViewOff : this.gui.PassViewOn;
		if (this.gui.Button(new Vector2(300f, 118f), texture2D, texture2D, texture2D, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this._showPass = !this._showPass;
		}
	}

	// Token: 0x060009FE RID: 2558 RVA: 0x0006EFD4 File Offset: 0x0006D1D4
	private void PassField(Rect dialogRect)
	{
		if (Main.PassHashed && this._useHashedPass)
		{
			this._pass = this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 112f, 200f, 25f), "******", 16, "#FFFFFF", TextAnchor.UpperCenter, true, true);
			if (this._pass != "******")
			{
				this._useHashedPass = false;
				for (int i = 0; i < this._pass.Length; i++)
				{
					if (i >= "******".Length || this._pass[i] != "******"[i])
					{
						this._pass = this._pass[i].ToString();
						break;
					}
					if (i == this._pass.Length - 1)
					{
						this._pass = string.Empty;
						break;
					}
				}
			}
		}
		else if (this._showPass)
		{
			this._pass = this.gui.TextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 112f, 200f, 25f), this._pass, 16, "#FFFFFF", TextAnchor.UpperCenter, true, true);
		}
		else
		{
			this._pass = this.gui.PasswordTextField(new Rect(dialogRect.xMin + dialogRect.width / 2f - 100f, 112f, 200f, 25f), this._pass, 16, TextAnchor.UpperCenter);
		}
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x0006F19C File Offset: 0x0006D39C
	private void StandaloneQuit(Popup popup)
	{
		Rect rect = new Rect(50f, 40f, 355f, 155f);
		this.gui.TextField(new Rect(rect.xMin + rect.width / 2f - 100f, 90f, 200f, 25f), Language.AreYouSure, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		if (this.gui.Button(new Vector2(rect.xMin + rect.width / 2f - (float)(this.gui.server_window[14].width / 2) - 55f, rect.height - 10f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.Yes, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			Main.ExitGame();
		}
		if (this.gui.Button(new Vector2(rect.xMin + rect.width / 2f - (float)(this.gui.server_window[14].width / 2) + 48f, rect.height - 10f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.No, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.id0, Language.Exit, string.Empty, PopupState.StandaloneQuit, false, true, string.Empty, string.Empty));
		}
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0006F38C File Offset: 0x0006D58C
	private void SeasonAwardPopup(Popup popup)
	{
		if (this._awardsDict == null)
		{
			this._awardsDict = (popup.args[0] as Dictionary<string, object>[]);
		}
		if (this._awardsDict == null)
		{
			Debug.LogError("can't get awards dict");
			popup.Hide(null, 0.5f, 0.5f);
			return;
		}
		int num = (int)popup.args[1];
		int num2 = (!this._awardsDict[num].ContainsKey("gp")) ? -1 : ((int)this._awardsDict[num]["gp"]);
		int num3 = (!this._awardsDict[num].ContainsKey("award")) ? -1 : ((int)this._awardsDict[num]["award"]);
		if (num2 < 0 || num3 < 0)
		{
			Debug.LogError("wrong award data");
			popup.Hide(null, 0.5f, 0.5f);
			return;
		}
		this.gui.Picture(new Vector2(20f, 95f), this.progressBonus);
		if (this.gui.Button(new Vector2(325f, 155f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], "OK", 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, null).Clicked)
		{
			if (this._awardsDict.Length > 1 && num < 1)
			{
				popup.args[1] = num + 1;
				EventFactory.Call("ShowPopup", popup);
			}
			else
			{
				popup.Hide(null, 0.5f, 0.5f);
			}
		}
		this.gui.TextField(new Rect(28f, 44f, 390f, 50f), Language.SeasonAwardDescription, 12, "#ffffff_Tahoma", TextAnchor.MiddleLeft, false, false);
		float num4 = 100f;
		this.gui.TextField(new Rect(num4, 101f, 50f, 50f), num2.ToString(), 22, "#ffffff", TextAnchor.MiddleRight, false, false);
		this.gui.Picture(new Vector2(num4 + 60f, 112f), this.gui.gldIcon);
		num4 += 110f;
		this.gui.TextField(new Rect(num4 + -5f, 98f, 50f, 50f), "+", 24, "#ffffff", TextAnchor.MiddleLeft, false, false);
		Texture2D texture2D = this.carrierGUI.AwardsManager.SeasonAwards[num3];
		if (texture2D != null)
		{
			this.gui.Picture(new Vector2(num4 + 25f, 97f), texture2D);
		}
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0006F678 File Offset: 0x0006D878
	private void Auth(Popup popup)
	{
		Rect rect = new Rect(50f, 40f, 355f, 155f);
		this.gui.TextField(new Rect(rect.xMin + rect.width / 2f - 100f, 60f, 200f, 25f), Language.EnterPassword, 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.gui.Picture(new Vector2(rect.xMin + rect.width / 2f - 100f, 85f), this.serverGUI.servername_field);
		eNetwork.password = this.gui.TextField(new Rect(rect.xMin + rect.width / 2f - 100f, 85f, 200f, 25f), eNetwork.password, 16, "#FFFFFF", TextAnchor.UpperCenter, true, true);
		this.unlock = this.gui.Button(new Vector2(rect.xMin + rect.width / 2f - (float)(this.gui.server_window[14].width / 2), rect.height - 40f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.OK, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
		if (this.unlock.Clicked && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
			Peer.JoinGame(Peer.Info, false);
		}
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0006F874 File Offset: 0x0006DA74
	private void ActivatePromoTab(Popup popup)
	{
		if (this.gui.Button(new Vector2(325f, 155f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.OK, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, null).Clicked)
		{
			popup.Hide(null, 0.5f, 0.5f);
		}
		if (!this.promoAlpha.Visible)
		{
			this.promoPage++;
			if (this.promoPage == Globals.I.bonuses.Count)
			{
				this.promoPage = 0;
			}
			this.promoAlpha.Once(1.3f);
		}
		this.gui.Picture(new Vector2(23f, 100f), this.progressBonus);
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		Rect rect = new Rect(31f, 40f, 385f, 155f);
		this.gui.BeginGroup(rect);
		Rect rect2 = new Rect(0f, 12f, 370f, 100f);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHello, 15, "#ffffff", TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 4f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Main.UserInfo.nick, 15, Colors.RadarBlueWeb, TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 3f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, "!", 15, "#ffffff", TextAnchor.UpperLeft);
		rect2.Set(rect2.xMin - 6f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHelloForResourse0, 15, "#c0c0c0", TextAnchor.UpperCenter);
		this.gui.TextField(new Rect(3f, 27f, 370f, 100f), Language.PromoHelloForResourse1, 15, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		Rect rect3 = new Rect(80f, 75f, 120f, 50f);
		this.gui.color = Colors.alpha(this.gui.color, this.promoAlpha.visibility * base.visibility);
		Vector2 pos = new Vector2(205f, 75f);
		int num = 0;
		if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.cr)
		{
			this.gui.TextField(rect3, Globals.I.bonuses[this.promoPage].amount, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
			this.gui.Picture(pos, this.gui.crIcon);
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.gp)
		{
			this.gui.TextField(rect3, Globals.I.bonuses[this.promoPage].amount, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
			this.gui.Picture(pos, this.gui.gldIcon);
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.sp)
		{
			this.gui.TextField(rect3, Globals.I.bonuses[this.promoPage].amount, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
			this.gui.Picture(pos, this.gui.spIcon_med);
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.bg)
		{
			this.gui.TextField(rect3, Language.HundredsOfOil, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.weapon_buy)
		{
			if (this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width > 190)
			{
				num = this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width - 190;
			}
			this.gui.TextField(new Rect((float)(200 + num), 70f, 120f, 30f), "PREMIUM", 13, "#fac321", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2((float)(190 + num - this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width), 65f), this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex]);
			this.gui.TextField(new Rect((float)(200 + num), 83f, 150f, 30f), Main.UserInfo.weaponsStates[Globals.I.bonuses[this.promoPage].weapindex].CurrentWeapon.Name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Rect rect4 = new Rect((float)(200 + num), 95f, 100f, 50f);
			this.gui.CompositeTextClean(ref rect4, Language.TheTerm + Language.Forever, 13, "#727272", TextAnchor.UpperLeft);
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.weapon_rent)
		{
			if (this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width > 190)
			{
				num = this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width - 190;
			}
			this.gui.TextField(new Rect((float)(200 + num), 70f, 120f, 30f), "PREMIUM", 13, "#fac321", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2((float)(190 + num - this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex].width), 65f), this.gui.weapon_unlocked[Globals.I.bonuses[this.promoPage].weapindex]);
			this.gui.TextField(new Rect((float)(200 + num), 83f, 150f, 30f), Main.UserInfo.weaponsStates[Globals.I.bonuses[this.promoPage].weapindex].CurrentWeapon.Name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Rect rect5 = new Rect((float)(200 + num), 95f, 100f, 50f);
			this.gui.CompositeTextClean(ref rect5, Language.TheTerm, 13, "#727272", TextAnchor.UpperLeft);
			this.gui.CompositeTextClean(ref rect5, Globals.I.bonuses[this.promoPage].rent_days.ToString(), 13, "#dfdfdf", TextAnchor.UpperLeft);
			if (Globals.I.bonuses[this.promoPage].rent_days == 1)
			{
				this.gui.CompositeTextClean(ref rect5, Language.Day, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
			else if (Globals.I.bonuses[this.promoPage].rent_days == 2)
			{
				this.gui.CompositeTextClean(ref rect5, Language.Days_dnya, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
			else
			{
				this.gui.CompositeTextClean(ref rect5, Language.Days_dney, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
		}
		else if (Globals.I.bonuses[this.promoPage].type == PromoBonusType.skill_rent)
		{
			this.gui.TextField(new Rect(170f, 67f, 120f, 30f), "PREMIUM", 13, "#fac321", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2(110f, 65f), CarrierGUI.I.Class_skills[Globals.I.bonuses[this.promoPage].SkillIndex]);
			this.gui.TextField(new Rect(170f, 80f, 150f, 30f), Main.UserInfo.skillsInfos[Globals.I.bonuses[this.promoPage].SkillIndex].name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Rect rect6 = new Rect(170f, 92f, 100f, 50f);
			this.gui.CompositeTextClean(ref rect6, Language.TheTerm, 13, "#727272", TextAnchor.UpperLeft);
			this.gui.CompositeTextClean(ref rect6, Globals.I.bonuses[this.promoPage].rent_days.ToString(), 13, "#dfdfdf", TextAnchor.UpperLeft);
			if (Globals.I.bonuses[this.promoPage].rent_days == 1)
			{
				this.gui.CompositeTextClean(ref rect6, Language.Day, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
			else if (Globals.I.bonuses[this.promoPage].rent_days == 2)
			{
				this.gui.CompositeTextClean(ref rect6, Language.Days_dnya, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
			else
			{
				this.gui.CompositeTextClean(ref rect6, Language.Days_dney, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00070364 File Offset: 0x0006E564
	private void DailyBonus(Popup popup)
	{
		if (!Main.UserInfo.dailyBonus.IsWin() || this.gui.weapon_unlocked.Length <= Main.UserInfo.dailyBonus.WeaponID)
		{
			popup.Hide(null, 0f, 0.5f);
			return;
		}
		PopupGUI.showingDaliyBonus = true;
		int num = 0;
		this.gui.Picture(new Vector2(23f, 100f), this.progressBonus);
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		Rect rect = new Rect(31f, 40f, 385f, 155f);
		this.gui.BeginGroup(rect);
		Rect rect2 = new Rect(0f, 12f, 370f, 100f);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHello, 15, "#ffffff", TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 4f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Main.UserInfo.nick, 15, Colors.RadarBlueWeb, TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 3f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, "!", 15, "#ffffff", TextAnchor.UpperLeft);
		rect2.Set(rect2.xMin - 6f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHelloForResourse0, 15, "#c0c0c0", TextAnchor.UpperCenter);
		this.gui.TextField(new Rect(3f, 27f, 370f, 100f), Language.PromoHelloForResourse1, 15, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		if (Main.UserInfo.dailyBonus.BonusIsWeapon())
		{
			if (this.gui.weapon_unlocked[Main.UserInfo.dailyBonus.WeaponID].width > 190)
			{
				num = this.gui.weapon_unlocked[Main.UserInfo.dailyBonus.WeaponID].width - 190;
			}
			this.gui.TextField(new Rect((float)(200 + num), 60f, 120f, 30f), "PREMIUM", 13, "#fac321", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2((float)(190 + num - this.gui.weapon_unlocked[Main.UserInfo.dailyBonus.WeaponID].width), 55f), this.gui.weapon_unlocked[Main.UserInfo.dailyBonus.WeaponID]);
			this.gui.TextField(new Rect((float)(200 + num), 73f, 150f, 30f), Main.UserInfo.weaponsStates[Main.UserInfo.dailyBonus.WeaponID].CurrentWeapon.Name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Rect rect3 = new Rect((float)(200 + num), 85f, 100f, 50f);
			this.gui.CompositeTextClean(ref rect3, Language.TheTerm, 13, "#727272", TextAnchor.UpperLeft);
			if (Main.UserInfo.dailyBonus.IsPermanentWeapon)
			{
				this.gui.CompositeTextClean(ref rect3, Language.Forever, 13, "#dfdfdf", TextAnchor.UpperLeft);
			}
			else
			{
				this.gui.CompositeTextClean(ref rect3, Main.UserInfo.dailyBonus.iRentDays.ToString(), 13, "#dfdfdf", TextAnchor.UpperLeft);
				if (Main.UserInfo.dailyBonus.iRentDays == 1)
				{
					this.gui.CompositeTextClean(ref rect3, Language.Day, 13, "#dfdfdf", TextAnchor.UpperLeft);
				}
				else if (Main.UserInfo.dailyBonus.iRentDays == 2)
				{
					this.gui.CompositeTextClean(ref rect3, Language.Days_dnya, 13, "#dfdfdf", TextAnchor.UpperLeft);
				}
				else
				{
					this.gui.CompositeTextClean(ref rect3, Language.Days_dney, 13, "#dfdfdf", TextAnchor.UpperLeft);
				}
			}
		}
		else
		{
			Rect rect4 = new Rect(80f, 75f, 120f, 50f);
			Vector2 pos = new Vector2(205f, 75f);
			if (Main.UserInfo.dailyBonus.CR > 0)
			{
				this.gui.TextField(rect4, Main.UserInfo.dailyBonus.CR, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
				this.gui.Picture(pos, this.gui.crIcon);
			}
			else if (Main.UserInfo.dailyBonus.GP > 0)
			{
				this.gui.TextField(rect4, Main.UserInfo.dailyBonus.GP, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
				this.gui.Picture(pos, this.gui.gldIcon);
			}
			else if (Main.UserInfo.dailyBonus.SP > 0)
			{
				this.gui.TextField(rect4, Main.UserInfo.dailyBonus.SP, 16, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
				this.gui.Picture(pos, this.gui.spIcon_med);
			}
		}
		this.gui.TextField(new Rect(3f, rect.height - 41f, rect.width - 90f, 50f), Language.WellcomeToCWandGetBonus, 13, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(3f, rect.height - 41f + 13f, rect.width - 90f, 50f), Language.CRGPSPEveryDay, 13, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		this.unlock = this.gui.Button(new Vector2(rect.width - 90f, rect.height - 40f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.OK, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
		if (this.unlock.Clicked)
		{
			PopupGUI.showingDaliyBonus = false;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
			Main.UserInfo.dailyBonus.Clear();
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x00070A90 File Offset: 0x0006EC90
	private void XPBonus(Popup popup)
	{
		this.gui.Picture(new Vector2(23f, 100f), this.progressBonus);
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		Rect rect = new Rect(31f, 40f, 385f, 155f);
		this.gui.BeginGroup(rect);
		Rect rect2 = new Rect(0f, 12f, 370f, 100f);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHello, 15, "#ffffff", TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 4f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Main.UserInfo.nick, 15, Colors.RadarBlueWeb, TextAnchor.UpperCenter);
		rect2.Set(rect2.xMin - 3f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, "!", 15, "#ffffff", TextAnchor.UpperLeft);
		rect2.Set(rect2.xMin - 6f, rect2.yMin, rect2.width, rect2.height);
		this.gui.CompositeTextClean(ref rect2, Language.PromoHelloForResourse0, 15, "#c0c0c0", TextAnchor.UpperCenter);
		this.gui.TextField(new Rect(3f, 27f, 370f, 100f), Language.PromoHelloForResourse1, 15, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		this.gui.TextField(new Rect(200f, 65f, 120f, 30f), "PREMIUM", 13, "#fac321", TextAnchor.UpperLeft, false, false);
		this.gui.Picture(new Vector2((float)(200 - this.carrierGUI.Class_skills[140].width), 63f), this.carrierGUI.Class_skills[140]);
		this.gui.TextField(new Rect(200f, 78f, 150f, 30f), Main.UserInfo.skillsInfos[140].name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
		Rect rect3 = new Rect(200f, 90f, 100f, 50f);
		this.gui.CompositeTextClean(ref rect3, Language.TheTerm, 13, "#727272", TextAnchor.UpperLeft);
		this.gui.CompositeTextClean(ref rect3, Language.tventyMinutes, 13, "#dfdfdf", TextAnchor.UpperLeft);
		this.unlock = this.gui.Button(new Vector2(rect.width - 90f, rect.height - 40f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.OK, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
		if (this.unlock.Clicked)
		{
			PopupGUI.showingDaliyBonus = false;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x00070E30 File Offset: 0x0006F030
	private void PersonalDiscount(Popup popup)
	{
		this.gui.Picture(new Vector2(23f, 95f), this.discount_stripe);
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		Rect rect = new Rect(31f, 40f, 385f, 155f);
		this.gui.BeginGroup(rect);
		this.gui.TextField(new Rect(3f, 20f, 370f, 100f), Language.PromoHelloForResourse2, 15, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		int num = (Main.UserInfo.discount_id >= Main.UserInfo.weaponsStates.Length || Main.UserInfo.discount_id < 0) ? 0 : Main.UserInfo.discount_id;
		this.gui.TextField(new Rect(4f, 67f, 150f, 60f), Language.discount, 26, "#fda100", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(95f, 66f, 150f, 50f), Main.UserInfo.discount.ToString() + "%", 45, "#fda100", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(4f, 53f, 220f, 30f), Main.UserInfo.weaponsStates[num].CurrentWeapon.Name, 13, "#dfdfdf", TextAnchor.UpperLeft, false, false);
		this.gui.Picture(new Vector2((float)(150 + (115 - this.gui.weapon_unlocked[num].width / 2)), 55f), this.gui.weapon_unlocked[num]);
		this.gui.TextField(new Rect(3f, 120f, 370f, 100f), Language.discountGreeting2, 15, "#c0c0c0", TextAnchor.UpperLeft, false, false);
		this.unlock = this.gui.Button(new Vector2(rect.width - 90f, rect.height - 40f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.OK, 16, "#ffffff", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
		if (this.unlock.Clicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x00071130 File Offset: 0x0006F330
	private void BuyNicknameChange(Popup popup)
	{
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextField(new Rect(0f, 15f, 370f, 100f), Language.SettingsBuyChangePopUp, 16, "#c0c0c0", TextAnchor.UpperCenter, false, false);
		if (Main.UserInfo.GP >= Globals.I.buyNicknameChangePrice)
		{
			this.unlock = this.gui.Button(new Vector2(140f, 80f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Globals.I.buyNicknameChangePrice + "  .", 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
			this.gui.Picture(new Vector2(140f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth - 8f, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.gldIcon.height) / 2)), this.gui.gldIcon);
		}
		else
		{
			this.gui.TextField(new Rect(80f, 50f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(260f, 50f, 100f, 20f), Globals.I.buyNicknameChangePrice.ToString(), 15, "#fac321", TextAnchor.MiddleLeft, false, false);
			this.gui.Picture(new Vector2(252f + this.tmpwidth, 49f), this.gui.gldIcon);
			if (this.gui.Button(new Vector2(90f, 90f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				popup.Hide(null, 0f, 0f);
				EventFactory.Call("ShowBankGUI", null);
			}
		}
		if (this.unlock.Clicked && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<BuyNick>(new object[0]);
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0007147C File Offset: 0x0006F67C
	private void BuyNickColorChange(Popup popup)
	{
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextField(new Rect(0f, 15f, 370f, 100f), Language.SettingsBuyChangeColorPopUp, 16, "#c0c0c0", TextAnchor.UpperCenter, false, false);
		if (Main.UserInfo.GP >= Globals.I.buyNickColorChangePrice)
		{
			GUI.DrawTexture(new Rect(145f, 50f, 80f, 28f), ClanSystemWindow.I.Textures.clanTagBack);
			string text = MainGUI.Instance.TextField(new Rect(145f, 50f, 80f, 28f), this.FixNickColorString(this._nickColor), 20, "#" + this._previousValidNickColor, TextAnchor.MiddleCenter, true, true);
			if (text.Length <= 6)
			{
				this._nickColor = text;
			}
			bool flag = this._nickColor.Equals(Main.UserInfo.nickColor.Substring(1), StringComparison.OrdinalIgnoreCase);
			bool flag2 = this._nickColor != string.Empty && !flag && Utility.ValidColor(this._nickColor);
			if (flag2)
			{
				this._previousValidNickColor = this._nickColor;
			}
			if (flag2)
			{
				this.unlock = this.gui.Button(new Vector2(140f, 90f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Globals.I.buyNickColorChangePrice + "    .", 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
				this.gui.Picture(new Vector2(140f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth - 8f, (float)(90 + Mathf.Abs(this.gui.server_window[14].height - this.gui.gldIcon.height) / 2)), this.gui.gldIcon);
			}
			else
			{
				string text2 = (!flag) ? Language.ClansUnavailable : Language.CurrentColor;
				int num = (!flag) ? 152 : 142;
				GUI.Label(new Rect((float)num, 90f, 100f, 20f), text2, ClanSystemWindow.I.Styles.styleGrayLabel);
			}
		}
		else
		{
			this.gui.TextField(new Rect(80f, 50f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(260f, 50f, 100f, 20f), Globals.I.buyNickColorChangePrice.ToString(), 15, "#fac321", TextAnchor.MiddleLeft, false, false);
			this.gui.Picture(new Vector2(260f + this.tmpwidth, 49f), this.gui.gldIcon);
			if (this.gui.Button(new Vector2(90f, 90f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				popup.Hide(null, 0f, 0f);
				EventFactory.Call("ShowBankGUI", null);
			}
		}
		if (this.unlock.Clicked && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<BuyNickColor>(new object[]
			{
				this._nickColor
			});
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x00071914 File Offset: 0x0006FB14
	private string FixNickColorString(string str)
	{
		Regex regex = new Regex("[^a-fA-F0-9]");
		return regex.Replace(str, string.Empty);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0007193C File Offset: 0x0006FB3C
	private void BuyBoxfunc(Popup popup)
	{
		if (BuyBox.isGP)
		{
			this.tmpwidth = this.gui.CalcWidth("0", this.gui.fontDNC57, 16);
		}
		else
		{
			this.tmpwidth = this.gui.CalcWidth("00", this.gui.fontDNC57, 16);
		}
		this.twidth = this.gui.CalcWidth(BuyBox.BoxName, this.gui.fontDNC57, 14);
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextField(new Rect(0f, 15f, 370f, 100f), Language.BuyBoxRequest, 16, "#c0c0c0", TextAnchor.UpperCenter, false, false);
		if ((BuyBox.isGP && Main.UserInfo.GP > BuyBox.price) || (!BuyBox.isGP && Main.UserInfo.CR > BuyBox.price))
		{
			this.unlock = this.gui.Button(new Vector2(140f, 80f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], BuyBox.price.ToString() + "  .", 16, (!BuyBox.isGP) ? "#ffffff" : "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
			if (BuyBox.isGP)
			{
				this.gui.Picture(new Vector2(140f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.gldIcon.height) / 2)), this.gui.gldIcon);
			}
			else
			{
				this.gui.Picture(new Vector2(143f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.crIcon.height) / 2)), this.gui.crIcon);
			}
		}
		else
		{
			this.gui.TextField(new Rect(80f, 65f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(260f, 65f, 100f, 20f), BuyBox.price.ToString(), 15, (!BuyBox.isGP) ? "#ffffff" : "#fac321", TextAnchor.MiddleLeft, false, false);
			this.gui.Picture(new Vector2(270f + this.tmpwidth, 64f), (!BuyBox.isGP) ? this.gui.crIcon : this.gui.gldIcon);
			if (this.gui.Button(new Vector2(90f, 90f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				popup.Hide(null, 0f, 0f);
				EventFactory.Call("ShowBankGUI", null);
			}
		}
		this.gui.Picture(new Vector2(169f - this.twidth / 2f, 49f), this.gui.packages_icon[0]);
		this.gui.TextField(new Rect(0f, 45f, 370f, 100f), BuyBox.BoxName, 14, "#ffffff", TextAnchor.UpperCenter, false, false);
		this.gui.TextField(new Rect(6f, 115f, 370f, 30f), Language.BuyBoxAttention, 16, "#717171", TextAnchor.UpperLeft, false, false);
		if (this.unlock.Clicked && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<BuyBox>(new object[0]);
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x00071E58 File Offset: 0x00070058
	private void CreateClan(Popup popup)
	{
		bool flag = false;
		int num = 0;
		int num2 = 0;
		string content = string.Empty;
		string[] array = new string[5];
		if (popup.desc == "base")
		{
			if (Globals.I.clanCRPrice > 0)
			{
				flag = true;
				num = Globals.I.clanCRPrice;
			}
			else
			{
				num2 = 1;
				num = Globals.I.clanBasePrice;
			}
			content = Language.ClansPopupCreateHint2;
		}
		if (popup.desc == "extended")
		{
			num2 = 2;
			num = Globals.I.clanExtendedPrice;
			content = Language.ClansPopupCreateHint3;
		}
		if (popup.desc == "premium")
		{
			num2 = 3;
			num = Globals.I.clanPremiumPrice;
			content = Language.ClansPopupCreateHint4;
		}
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextLabel(new Rect(0f, 10f, 370f, 25f), Language.ClansPopupCreateHint1, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 115f, 370f, 25f), Language.ClansPopupCreateHint5, 16, "#717171", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 35f, 370f, 25f), content, 16, "#717171", TextAnchor.MiddleCenter, true);
		if (flag && Main.UserInfo.CR >= num)
		{
			this.btnClick = GUI.Button(new Rect(140f, 60f, 87f, 29f), Helpers.SeparateNumericString(num.ToString()), CarrierGUI.I.clanSystemWindow.Styles.styleCreateBtnWhite);
			GUI.DrawTexture(new Rect(200f, 64f, 21f, 22f), this.gui.crIcon);
		}
		else if (Main.UserInfo.GP >= num)
		{
			this.btnClick = GUI.Button(new Rect(140f, 60f, 87f, 29f), num.ToString(), CarrierGUI.I.clanSystemWindow.Styles.styleCreateBtn);
			GUI.DrawTexture(new Rect(190f, 64f, 21f, 22f), this.gui.gldIcon);
		}
		else
		{
			this.NeedMoreGP(popup, num, flag);
		}
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			array[0] = (string)popup.args[0];
			array[1] = (string)popup.args[1];
			array[2] = (string)popup.args[2];
			array[3] = (string)popup.args[3];
			array[4] = num2.ToString();
			popup.alreadyClicked = true;
			Main.AddDatabaseRequestCallBack<CreateClan>(delegate
			{
				Main.AddDatabaseRequest<LoadProfile>(new object[0]);
				CarrierGUI.I.Hide(0.35f);
				CarrierGUI.I.Clear();
			}, delegate
			{
			}, array);
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A0B RID: 2571 RVA: 0x000721D0 File Offset: 0x000703D0
	private void ExtendClan(Popup popup)
	{
		int num = (int)popup.args[0];
		string desc = popup.desc;
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextLabel(new Rect(10f, 15f, 350f, 50f), Language.ClansPopupExtendHint, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		if (num >= Globals.I.clanExtendPrice)
		{
			this.btnClick = GUI.Button(new Rect(140f, 80f, 87f, 29f), Globals.I.clanExtendPrice.ToString(), CarrierGUI.I.clanSystemWindow.Styles.styleCreateBtn);
			GUI.DrawTexture(new Rect(190f, 84f, 21f, 22f), this.gui.gldIcon);
		}
		else
		{
			this.NeedMoreGP(popup, Globals.I.clanExtendPrice, false);
		}
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<UpgradeClan>(new object[]
			{
				desc
			});
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
			popup.action();
		}
	}

	// Token: 0x06000A0C RID: 2572 RVA: 0x00072358 File Offset: 0x00070558
	private void ClanError(Popup popup)
	{
		string str = string.Empty;
		string content = string.Empty;
		string content2 = string.Empty;
		if (popup.desc == "createFailed")
		{
			str = Language.ClansPopupCreateError1;
			content = Language.ClansPopupCreateError2;
		}
		else if (popup.desc == "byOrderFailed")
		{
			str = Language.ClansPopupRequestFailedByOrder1;
			content = Language.ClansPopupRequestFailedByOrder2;
		}
		else if (popup.desc == "byVacancyFailed")
		{
			str = Language.ClansPopupRequestFailedByVacancy1;
			content = Language.ClansPopupRequestFailedByVacancy2;
			content2 = Language.ClansPopupRequestFailedByVacancy3;
		}
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 20f, 370f, 20f), Language.ClansPopupError + " " + str, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 40f, 370f, 20f), content, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 60f, 370f, 20f), content2, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.btnClick = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0007253C File Offset: 0x0007073C
	private void DiscardAllRequest(Popup popup)
	{
		string desc = popup.desc;
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 30f, 370f, 50f), Language.ClansPopupDiscardHint, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<DeleteAllRequest>(new object[]
			{
				desc
			});
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x000726EC File Offset: 0x000708EC
	private void DismissWarrior(Popup popup)
	{
		int num = (int)popup.args[0];
		int num2 = (int)popup.args[1];
		string text = (string)popup.args[2];
		string str = (string)popup.args[3];
		string str2 = (string)popup.args[4];
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 10f, 370f, 20f), Language.ClansPopupDismissHint1, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 30f, 370f, 20f), Language.ClansPopupDismissHint2, 16, "#c00000", TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect(85f, 60f, (float)CarrierGUI.I.avatar.width, (float)CarrierGUI.I.avatar.height), CarrierGUI.I.avatar);
		GUI.DrawTexture(new Rect(140f, 65f, (float)MainGUI.Instance.rank_icon[num].width, (float)MainGUI.Instance.rank_icon[num].height), MainGUI.Instance.rank_icon[num]);
		if (num2 != 0)
		{
			GUI.DrawTexture(new Rect(165f, 60f, (float)ClanSystemWindow.I.Textures.statsClass[num2 - 1].width, (float)ClanSystemWindow.I.Textures.statsClass[num2 - 1].height), ClanSystemWindow.I.Textures.statsClass[num2 - 1]);
			GUI.Label(new Rect(190f, 65f, 100f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		}
		else
		{
			GUI.Label(new Rect(175f, 65f, 100f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		}
		GUI.Label(new Rect(175f, 83f, 100f, 20f), str + " " + str2, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A0F RID: 2575 RVA: 0x00072A7C File Offset: 0x00070C7C
	private void LeaveClan(Popup popup)
	{
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 30f, 370f, 50f), Language.ClansPopupLeaveHint, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequestCallBack<ExitFromClan>(delegate
			{
				popup.action();
			}, delegate
			{
			}, new object[]
			{
				popup.desc
			});
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A10 RID: 2576 RVA: 0x00072C88 File Offset: 0x00070E88
	private void UnlockClanSkill(Popup popup)
	{
		int num = (int)popup.args[0];
		DetailClanInfo clanInfo = (DetailClanInfo)popup.args[1];
		bool flag = Main.UserInfo.ClanSkillsInfos[num].RentPrice != null;
		int num2 = -1;
		float num3 = 15f;
		int rent_price;
		if (!flag)
		{
			if (Main.UserInfo.ClanSkillsInfos[num].IsPremium)
			{
				rent_price = Main.UserInfo.ClanSkillsInfos[num].PriceGP.Value;
			}
			else
			{
				rent_price = Main.UserInfo.ClanSkillsInfos[num].PriceCR.Value;
			}
		}
		else
		{
			rent_price = Main.UserInfo.ClanSkillsInfos[num].RentPrice[0];
		}
		bool flag2 = this.CanBuy(flag, num, clanInfo);
		this.gui.Picture(new Vector2(24f, 130f), this.gui.GetComponent<HelpersGUI>().lowBar);
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		GUI.DrawTexture(new Rect((float)((!flag) ? 160 : 130), 20f, (float)ClanSystemWindow.I.Textures.clanSkills[0].width, (float)ClanSystemWindow.I.Textures.clanSkills[0].height), ClanSystemWindow.I.Textures.clanSkills[num]);
		GUI.DrawTexture(new Rect((float)((!flag) ? 126 : 96), -14f, (float)CarrierGUI.I.Class_skill_button[3].width, (float)CarrierGUI.I.Class_skill_button[3].height), CarrierGUI.I.Class_skill_button[3]);
		if (flag)
		{
			if (this.rentToggle[0])
			{
				this.tgl_1_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[1];
			}
			else
			{
				this.tgl_1_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[0];
			}
			if (this.rentToggle[1])
			{
				this.tgl_2_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[1];
			}
			else
			{
				this.tgl_2_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[0];
			}
			if (this.rentToggle[2])
			{
				this.tgl_3_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[1];
			}
			else
			{
				this.tgl_3_Style.normal.background = this.gui.GetComponent<HelpersGUI>().checkbox[0];
			}
			if (GUI.Toggle(new Rect(210f, 15f, 22f, 21f), this.rentToggle[0], string.Empty, this.tgl_1_Style))
			{
				this.rentToggle[0] = true;
				this.rentToggle[1] = false;
				this.rentToggle[2] = false;
				rent_price = Main.UserInfo.ClanSkillsInfos[num].RentPrice[0];
				num2 = 0;
			}
			if (GUI.Toggle(new Rect(210f, 35f, 22f, 21f), this.rentToggle[1], string.Empty, this.tgl_2_Style))
			{
				this.rentToggle[0] = false;
				this.rentToggle[1] = true;
				this.rentToggle[2] = false;
				rent_price = Main.UserInfo.ClanSkillsInfos[num].RentPrice[1];
				num2 = 1;
			}
			if (GUI.Toggle(new Rect(210f, 55f, 22f, 21f), this.rentToggle[2], string.Empty, this.tgl_3_Style))
			{
				this.rentToggle[0] = false;
				this.rentToggle[1] = false;
				this.rentToggle[2] = true;
				rent_price = Main.UserInfo.ClanSkillsInfos[num].RentPrice[2];
				num2 = 2;
			}
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
			ClanSystemWindow.I.Styles.styleGoldLabel.fontSize = 16;
			for (int i = 0; i < 3; i++)
			{
				GUI.Label(new Rect(228f, num3, 50f, 20f), Main.UserInfo.ClanSkillsInfos[num].RentTime[i].ToString("F0") + " " + Language.Days_dney, ClanSystemWindow.I.Styles.styleWhiteLabel16);
				if (Main.UserInfo.ClanSkillsInfos[num].IsPremium)
				{
					GUI.Label(new Rect(280f, num3, 80f, 20f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[num].RentPrice[i].ToString("F0")), ClanSystemWindow.I.Styles.styleGoldLabel);
					GUI.DrawTexture(new Rect(360f, num3 - 2f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
				}
				else
				{
					GUI.Label(new Rect(275f, num3, 80f, 20f), Helpers.SeparateNumericString(Main.UserInfo.ClanSkillsInfos[num].RentPrice[i].ToString("F0")), ClanSystemWindow.I.Styles.styleWhiteLabel16);
					GUI.DrawTexture(new Rect(360f, num3 - 2f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
				}
				num3 += 22f;
			}
			ClanSystemWindow.I.Styles.styleGoldLabel.fontSize = 20;
			ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
		}
		ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 20;
		GUI.Label(new Rect(0f, 110f, 50f, 20f), Language.Price, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 16;
		if (Main.UserInfo.ClanSkillsInfos[num].IsPremium)
		{
			Color textColor = ClanSystemWindow.I.Styles.styleGoldLabel.normal.textColor;
			ClanSystemWindow.I.Styles.styleGoldLabel.alignment = TextAnchor.MiddleLeft;
			if (!flag2)
			{
				ClanSystemWindow.I.Styles.styleGoldLabel.normal.textColor = Colors.RadarRed;
			}
			this.tmpStyle = ClanSystemWindow.I.Styles.styleGoldLabel;
			GUI.Label(new Rect(45f, 110f, 200f, 20f), Helpers.SeparateNumericString(rent_price.ToString("F0")), this.tmpStyle);
			ClanSystemWindow.I.Styles.styleGoldLabel.alignment = TextAnchor.MiddleRight;
			if (!flag2)
			{
				ClanSystemWindow.I.Styles.styleGoldLabel.normal.textColor = textColor;
			}
			GUI.DrawTexture(new Rect(45f + MainGUI.Instance.CalcWidth(rent_price.ToString("F0"), MainGUI.Instance.fontDNC57, 20), 108f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		}
		else
		{
			ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 20;
			if (!flag2)
			{
				ClanSystemWindow.I.Styles.styleWhiteLabel16.normal.textColor = Colors.RadarRed;
			}
			this.tmpStyle = ClanSystemWindow.I.Styles.styleWhiteLabel16;
			GUI.Label(new Rect(45f, 110f, 200f, 20f), Helpers.SeparateNumericString(rent_price.ToString("F0")), this.tmpStyle);
			ClanSystemWindow.I.Styles.styleWhiteLabel16.fontSize = 16;
			if (!flag2)
			{
				ClanSystemWindow.I.Styles.styleWhiteLabel16.normal.textColor = Colors.RadarWhite;
			}
			GUI.DrawTexture(new Rect(55f + MainGUI.Instance.CalcWidth(rent_price.ToString("F0"), MainGUI.Instance.fontDNC57, 20), 108f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
		}
		ClanSystemWindow.I.Styles.styleGrayLabel14Left.alignment = TextAnchor.MiddleRight;
		GUI.Label(new Rect(240f, 85f, 100f, 20f), (!flag) ? Language.UnlockQuestion : Language.RentSkill, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
		ClanSystemWindow.I.Styles.styleGrayLabel14Left.alignment = TextAnchor.MiddleLeft;
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			object[] args = new object[]
			{
				num,
				num2
			};
			ClanSkills.Rent_price = rent_price;
			if (flag2)
			{
				Main.AddDatabaseRequestCallBack<UnlockClanSkill>(delegate
				{
					popup.action();
				}, delegate
				{
				}, args);
			}
			else
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
			this.rentToggle[0] = true;
			this.rentToggle[1] = false;
			this.rentToggle[2] = false;
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
			this.rentToggle[0] = true;
			this.rentToggle[1] = false;
			this.rentToggle[2] = false;
		}
	}

	// Token: 0x06000A11 RID: 2577 RVA: 0x000737F4 File Offset: 0x000719F4
	private bool CanBuy(bool isRentable, int skillIndex, DetailClanInfo clanInfo)
	{
		bool result = false;
		if (!isRentable)
		{
			if (!Main.UserInfo.ClanSkillsInfos[skillIndex].IsPremium && (long)Main.UserInfo.ClanSkillsInfos[skillIndex].PriceCR.Value <= clanInfo.clanCR)
			{
				result = true;
			}
			else if (Main.UserInfo.ClanSkillsInfos[skillIndex].IsPremium && Main.UserInfo.ClanSkillsInfos[skillIndex].PriceGP.Value <= clanInfo.clanGP)
			{
				result = true;
			}
		}
		else
		{
			for (int i = 0; i < 3; i++)
			{
				if (Main.UserInfo.ClanSkillsInfos[skillIndex].IsPremium && Main.UserInfo.ClanSkillsInfos[skillIndex].RentPrice[i] <= clanInfo.clanGP && this.rentToggle[i])
				{
					result = true;
				}
				else if (!Main.UserInfo.ClanSkillsInfos[skillIndex].IsPremium && (long)Main.UserInfo.ClanSkillsInfos[skillIndex].RentPrice[i] <= clanInfo.clanCR && this.rentToggle[i])
				{
					result = true;
				}
			}
		}
		return result;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x00073928 File Offset: 0x00071B28
	private void FillUpClanBalance(Popup popup)
	{
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 0f, 370f, 50f), Language.ClansPopupBalanceHint, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 117f, 300f, 20f), Language.ClansMinimalTransaction + " 100 000 CR, 200 GP", 13, "#c0c0c0", TextAnchor.MiddleLeft, true);
		ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleRight;
		GUI.Label(new Rect(0f, 60f, 70f, 20f), Helpers.SeparateNumericString((Main.UserInfo.CR.Value - ClanSystemWindow.I.crToClan).ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
		GUI.Label(new Rect(0f, 86f, 70f, 20f), Helpers.SeparateNumericString((Main.UserInfo.GP.Value - ClanSystemWindow.I.gpToClan).ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
		ClanSystemWindow.I.Styles.styleWhiteLabel16.alignment = TextAnchor.MiddleLeft;
		GUI.DrawTexture(new Rect(70f, 59f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
		GUI.Label(new Rect(292f, 60f, 70f, 20f), Helpers.SeparateNumericString(ClanSystemWindow.I.crToClan.ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
		GUI.DrawTexture(new Rect(291f + MainGUI.Instance.CalcWidth(ClanSystemWindow.I.crToClan.ToString(), MainGUI.Instance.fontDNC57, 16), 59f, (float)MainGUI.Instance.crIcon.width, (float)MainGUI.Instance.crIcon.height), MainGUI.Instance.crIcon);
		ClanSystemWindow.I.crToClan = (int)MainGUI.Instance.FloatSlider(new Vector2(100f, 65f), (float)ClanSystemWindow.I.crToClan, 0f, (float)Main.UserInfo.CR.Value, false, false, false);
		if (ClanSystemWindow.I.crToClan > Main.UserInfo.CR.Value)
		{
			ClanSystemWindow.I.crToClan = Main.UserInfo.CR.Value;
		}
		GUI.DrawTexture(new Rect(71f, 85f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		GUI.Label(new Rect(292f, 86f, 70f, 20f), Helpers.SeparateNumericString(ClanSystemWindow.I.gpToClan.ToString()), ClanSystemWindow.I.Styles.styleWhiteLabel16);
		GUI.DrawTexture(new Rect(292f + MainGUI.Instance.CalcWidth(ClanSystemWindow.I.gpToClan.ToString(), MainGUI.Instance.fontDNC57, 16), 85f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		ClanSystemWindow.I.gpToClan = (int)MainGUI.Instance.FloatSlider(new Vector2(100f, 90f), (float)ClanSystemWindow.I.gpToClan, 0f, (float)Main.UserInfo.GP.Value, false, false, false);
		if (ClanSystemWindow.I.gpToClan > Main.UserInfo.GP.Value)
		{
			ClanSystemWindow.I.gpToClan = Main.UserInfo.GP.Value;
		}
		if ((ClanSystemWindow.I.gpToClan < 200 || (ClanSystemWindow.I.crToClan != 0 && ClanSystemWindow.I.crToClan < 100000)) && (ClanSystemWindow.I.crToClan < 100000 || (ClanSystemWindow.I.gpToClan != 0 && ClanSystemWindow.I.gpToClan < 200)))
		{
			GUI.enabled = false;
		}
		this.btnClick = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		GUI.enabled = true;
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			string[] args = new string[]
			{
				ClanSystemWindow.I.crToClan.ToString(),
				ClanSystemWindow.I.gpToClan.ToString()
			};
			Main.AddDatabaseRequestCallBack<FillUpClanBalance>(delegate
			{
				popup.action();
				if (popup.closeMethod != string.Empty)
				{
					this.Invoke(popup.closeMethod, 0f);
				}
			}, delegate
			{
			}, args);
			popup.Hide(null, 0.15f, 0f);
		}
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x00073EC8 File Offset: 0x000720C8
	private void NeedMoreGP(Popup popup, int price, bool isCR = false)
	{
		this.tmpwidth = this.gui.CalcWidth(price.ToString(), this.gui.fontDNC57, 18);
		this.gui.TextLabel(new Rect(80f, 65f, 200f, 20f), Language.InsufficientFundsNeed, 15, "#cccccc", TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(260f, 63f, 100f, 25f), Helpers.SeparateNumericString(price.ToString()), 18, (!isCR) ? "#fac321" : "#ffffff", TextAnchor.MiddleLeft, true);
		this.gui.Picture(new Vector2(260f + this.tmpwidth, 64f), (!isCR) ? this.gui.gldIcon : this.gui.crIcon);
		if (this.gui.Button(new Vector2(90f, 90f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], null, Language.FillUpBalance, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			popup.Hide(null, 0f, 0f);
			EventFactory.Call("ShowBankGUI", null);
		}
	}

	// Token: 0x06000A14 RID: 2580 RVA: 0x00074038 File Offset: 0x00072238
	private void BuyAttempt(Popup popup)
	{
		string text = "<color=#fac321>" + (Globals.I.RouletteInfo.AttemptCost * this.attemptsToBuy).ToString() + "</color>";
		GUI.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		GUI.Label(new Rect(0f, 30f, 370f, 20f), Language.RoulettePopupBody, this.RoulettePopupStyle);
		this.attemptsToBuy = (int)MainGUI.Instance.FloatSlider(new Vector2(100f, 60f), (float)this.attemptsToBuy, 1f, 20f, true, false, false);
		if (this.attemptsToBuy < 1)
		{
			this.attemptsToBuy = 1;
		}
		if (GUI.Button(new Rect(140f, 80f, 87f, 29f), text, this.RoulettePopupBtnStyle) && !popup.alreadyClicked)
		{
			if (Main.UserInfo.GP >= Globals.I.RouletteInfo.AttemptCost * this.attemptsToBuy)
			{
				Audio.Play(MainGUI.Instance.buy_sound);
				Main.AddDatabaseRequest<BuyAttempt>(new object[]
				{
					this.attemptsToBuy.ToString()
				});
			}
			else
			{
				RouletteGUI.I.Hide(0.35f);
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		GUI.DrawTexture(new Rect(188f, 84f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		GUI.EndGroup();
	}

	// Token: 0x06000A15 RID: 2581 RVA: 0x0007422C File Offset: 0x0007242C
	private void PerformContracts(Popup popup)
	{
		string text = "<color=#fac321>" + Globals.I.ContractsUpdateCost + "</color>";
		string text2 = "<color=#ff0000>" + Language.CarrUpdateContractsPopupBody1 + "</color>";
		GUI.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		GUI.Label(new Rect(0f, 30f, 370f, 20f), Language.CarrUpdateContractsPopupBody0, this.RoulettePopupStyle);
		GUI.Label(new Rect(0f, 115f, 370f, 20f), text2, this.RoulettePopupStyle);
		if (GUI.Button(new Rect(140f, 70f, 87f, 29f), text, this.RoulettePopupBtnStyle) && !popup.alreadyClicked)
		{
			if (Main.UserInfo.GP >= Globals.I.ContractsUpdateCost)
			{
				Audio.Play(MainGUI.Instance.buy_sound);
				Main.AddDatabaseRequestCallBack<PerformContracts>(delegate
				{
					Main.UserInfo.GP -= Globals.I.ContractsUpdateCost;
					this.ContractsRoutine(Main.UserInfo.contractsInfo);
				}, delegate
				{
				}, new object[0]);
			}
			else
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		GUI.DrawTexture(new Rect(188f, 74f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		GUI.EndGroup();
	}

	// Token: 0x06000A16 RID: 2582 RVA: 0x00074404 File Offset: 0x00072604
	private void ContractsRoutine(ContractsState state)
	{
		if (state.CurrentEasyCount >= state.CurrentEasy.task_counter)
		{
			Main.UserInfo.contractsInfo.CurrentEasyIndex++;
			Main.UserInfo.contractsInfo.CurrentEasyCount = 0;
		}
		else
		{
			state.CurrentEasyCount = 0;
		}
		if (state.CurrentNormalCount >= state.CurrentNormal.task_counter)
		{
			Main.UserInfo.contractsInfo.CurrentNormalIndex++;
			Main.UserInfo.contractsInfo.CurrentNormalCount = 0;
		}
		else
		{
			state.CurrentNormalCount = 0;
		}
		if (state.CurrentHardCount >= state.CurrentHard.task_counter)
		{
			Main.UserInfo.contractsInfo.CurrentHardIndex++;
			Main.UserInfo.contractsInfo.CurrentHardCount = 0;
		}
		else
		{
			state.CurrentHardCount = 0;
		}
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x000744EC File Offset: 0x000726EC
	private void SkipContract(Popup popup)
	{
		int contractType = Convert.ToInt32(popup.args[0]);
		int num = Convert.ToInt32(popup.args[1]);
		int num2;
		if (contractType == 0)
		{
			num2 = 2;
		}
		else if (contractType == 1)
		{
			num2 = 5;
		}
		else
		{
			num2 = 25;
		}
		int cost = num2 * ((num <= 0) ? 1 : num);
		string text = "<color=#fac321>" + cost + "</color>";
		string text2 = "<color=#ff0000>" + Language.CarrSkipContractPopupBody1 + "</color>";
		string text3 = "<color=#ff0000>" + Language.CarrSkipContractPopupBody2 + "</color>";
		GUI.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		GUI.Label(new Rect(0f, 20f, 370f, 20f), Language.CarrSkipContractPopupBody0, this.RoulettePopupStyle);
		GUI.Label(new Rect(0f, 95f, 370f, 20f), text2, this.RoulettePopupStyle);
		GUI.Label(new Rect(0f, 115f, 370f, 20f), text3, this.RoulettePopupStyle);
		if (GUI.Button(new Rect(140f, 50f, 87f, 29f), text, this.RoulettePopupBtnStyle) && !popup.alreadyClicked)
		{
			if (Main.UserInfo.GP >= cost)
			{
				object[] args = new object[]
				{
					contractType,
					num
				};
				Audio.Play(MainGUI.Instance.buy_sound);
				Main.AddDatabaseRequestCallBack<SkipContract>(delegate
				{
					Main.UserInfo.GP -= cost;
					int contractType = contractType;
					if (contractType != 0)
					{
						if (contractType != 1)
						{
							Main.UserInfo.contractsInfo.CurrentHardIndex++;
							Main.UserInfo.contractsInfo.CurrentHardCount = 0;
						}
						else
						{
							Main.UserInfo.contractsInfo.CurrentNormalIndex++;
							Main.UserInfo.contractsInfo.CurrentNormalCount = 0;
						}
					}
					else
					{
						Main.UserInfo.contractsInfo.CurrentEasyIndex++;
						Main.UserInfo.contractsInfo.CurrentEasyCount = 0;
					}
				}, delegate
				{
				}, args);
			}
			else
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		GUI.DrawTexture(new Rect(188f, 54f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		GUI.EndGroup();
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x00074780 File Offset: 0x00072980
	private void EditClanInfo(Popup popup)
	{
		int textMaxSize = MainGUI.Instance.textMaxSize;
		if (popup.args[0].ToString() != string.Empty && this._url == string.Empty)
		{
			this._url = popup.args[0].ToString();
		}
		if (popup.args[1].ToString() != string.Empty && this._color == string.Empty)
		{
			this._color = popup.args[1].ToString();
		}
		int num = Convert.ToInt32(popup.args[2]);
		this.gui.BeginGroup(new Rect(22f, 47f, 390f, 145f));
		MainGUI.Instance.TextLabel(new Rect(0f, 2f, 303f, 20f), Language.ClansHomePage, 18, "#9d9e9f", TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect(0f, 25f, 303f, 28f), MainGUI.Instance.settings_window[0]);
		MainGUI.Instance.textMaxSize = 100;
		this._url = MainGUI.Instance.TextField(new Rect(0f, 25f, 303f, 28f), this._url, 18, "#FFFFFF_D", TextAnchor.MiddleCenter, true, true);
		MainGUI.Instance.TextLabel(new Rect(215f, 80f, 90f, 20f), Language.ClansClantagColor, 18, "#9d9e9f", TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect(220f, 102f, 80f, 28f), ClanSystemWindow.I.Textures.clanTagBack);
		MainGUI.Instance.textMaxSize = 6;
		this._color = MainGUI.Instance.TextField(new Rect(220f, 102f, 80f, 28f), Helpers.HexOnly(this._color), 18, "#" + this._color, TextAnchor.MiddleCenter, true, true);
		this.btnClick = GUI.Button(new Rect(306f, 24f, 87f, 29f), Helpers.ColoredText(Globals.I.ClanChangeUrlCost, "#ffc929"), CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(306f, 100f, 87f, 29f), Helpers.ColoredText(Globals.I.ClanChangeTagColorCost, "#ffc929"), CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		GUI.DrawTexture(new Rect(361f, 28f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		GUI.DrawTexture(new Rect(361f, 104f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		this.gui.EndGroup();
		MainGUI.Instance.textMaxSize = textMaxSize;
		if (this.btnClick && !popup.alreadyClicked)
		{
			if (Globals.I.ClanChangeUrlCost <= num)
			{
				Main.AddDatabaseRequestCallBack<EditClanLink>(delegate
				{
				}, delegate
				{
				}, new object[]
				{
					Main.UserInfo.userID,
					Main.UserInfo.clanID,
					this._url
				});
			}
			else
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		if (this.btnClickNo && !popup.alreadyClicked)
		{
			if (Globals.I.ClanChangeTagColorCost <= num)
			{
				Main.AddDatabaseRequestCallBack<EditClanTagColor>(delegate
				{
				}, delegate
				{
				}, new object[]
				{
					Main.UserInfo.userID,
					Main.UserInfo.clanID,
					this._color
				});
			}
			else
			{
				EventFactory.Call("ShowBankGUI", null);
			}
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A19 RID: 2585 RVA: 0x00074CB0 File Offset: 0x00072EB0
	private void SetClanRole(Popup popup)
	{
		bool flag = false;
		int clanAssignmentCost = Globals.I.ClanAssignmentCost;
		int num = (int)popup.args[0];
		int num2 = (int)popup.args[1];
		string text = (string)popup.args[2];
		string str = (string)popup.args[3];
		string str2 = (string)popup.args[4];
		int num3 = (int)popup.args[5];
		string content = string.Empty;
		string content2 = string.Empty;
		if (popup.desc == "leader")
		{
			content = Language.ClansSetLeaderPopupBody;
			content2 = Language.ClansSetLeaderPopupHint;
		}
		else if (popup.desc == "lt")
		{
			if (num3 == 4)
			{
				content = Language.ClansDismissLtPopupBody;
				flag = true;
			}
			else
			{
				content = Language.ClansSetLtPopupBody;
			}
			content2 = Language.ClansSetLtPopupHint;
		}
		else
		{
			if (num3 == 2)
			{
				content = Language.ClansDismissOfficerPopupBody;
				flag = true;
			}
			else
			{
				content = Language.ClansSetOfficerPopupBody;
			}
			content2 = Language.ClansSetOfficerPopupHint;
		}
		this.gui.BeginGroup(new Rect(0f, 47f, 410f, 145f));
		MainGUI.Instance.TextLabel(new Rect(10f, 5f, 410f, 20f), content, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		MainGUI.Instance.TextLabel(new Rect(35f, 95f, 380f, 40f), content2, 16, "#717171", TextAnchor.MiddleLeft, true);
		GUI.DrawTexture(new Rect(65f, 30f, (float)CarrierGUI.I.avatar.width, (float)CarrierGUI.I.avatar.height), CarrierGUI.I.avatar);
		GUI.DrawTexture(new Rect(120f, 35f, (float)MainGUI.Instance.rank_icon[num].width, (float)MainGUI.Instance.rank_icon[num].height), MainGUI.Instance.rank_icon[num]);
		if (num2 != 0)
		{
			GUI.DrawTexture(new Rect(145f, 30f, (float)ClanSystemWindow.I.Textures.statsClass[num2 - 1].width, (float)ClanSystemWindow.I.Textures.statsClass[num2 - 1].height), ClanSystemWindow.I.Textures.statsClass[num2 - 1]);
			GUI.Label(new Rect(170f, 35f, 100f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		}
		else
		{
			GUI.Label(new Rect(155f, 35f, 100f, 20f), text, ClanSystemWindow.I.Styles.styleWhiteLabel16);
		}
		GUI.Label(new Rect(155f, 53f, 100f, 20f), str + " " + str2, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
		this.btnClick = GUI.Button(new Rect(280f, 35f, 87f, 29f), (!flag && !(popup.desc == "officer")) ? Helpers.ColoredText(clanAssignmentCost, "#ffc929") : Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		if (!flag && popup.desc != "officer")
		{
			GUI.DrawTexture(new Rect(335f, 39f, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height), MainGUI.Instance.gldIcon);
		}
		this.gui.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A1A RID: 2586 RVA: 0x000750EC File Offset: 0x000732EC
	private void EditClanMessage(Popup popup)
	{
		int textMaxSize = MainGUI.Instance.textMaxSize;
		MainGUI.Instance.textMaxSize = CVars.ClanMessageLength;
		Color color = this.gui.color;
		string text = Convert.ToString(Crypt.ResolveVariable(popup.args, string.Empty, 0));
		if (this._clanMessage == null)
		{
			this._clanMessage = text;
		}
		GUI.BeginGroup(new Rect(25f, 45f, 390f, 145f));
		this.gui.color = new Color(1f, 1f, 1f, 0.3f);
		GUI.DrawTexture(new Rect(0f, 7f, 385f, 100f), MainGUI.Instance.black);
		this.gui.color = color;
		this._clanMessage = MainGUI.Instance.TextField(new Rect(3f, 7f, 385f, 100f), this._clanMessage, 18, "#FFFFFF", TextAnchor.UpperLeft, true, true);
		this.btnClick = GUI.Button(new Rect(303f, 112f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		string content = Language.ClansEditMessageCharactersleft + ": " + (CVars.ClanMessageLength - this._clanMessage.Length);
		this.gui.TextLabel(new Rect(3f, 115f, 200f, 20f), content, 16, "#FFFFFF", TextAnchor.MiddleLeft, true);
		MainGUI.Instance.textMaxSize = textMaxSize;
		if (this.btnClick && !popup.alreadyClicked)
		{
			CreepingLine.TempMessage = this._clanMessage;
			if (text != this._clanMessage)
			{
				popup.action();
			}
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		GUI.EndGroup();
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x00075314 File Offset: 0x00073514
	private void Favorite(Popup popup)
	{
		bool remove = popup.desc == "remove";
		bool flag = popup.desc == "1001";
		bool flag2 = popup.desc == "1002";
		if (flag || flag2)
		{
			GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
			this.gui.TextLabel(new Rect(0f, 5f, 370f, 100f), (!flag) ? Language.AddToFavoritesDeniedByUser : Language.AddToFavoritesLimitReached, 16, "#c0c0c0", TextAnchor.UpperLeft, true);
			this.btnClick = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			if (this.btnClick && !popup.alreadyClicked)
			{
				popup.alreadyClicked = true;
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
			GUI.EndGroup();
		}
		else
		{
			int userId = (int)popup.args[0];
			int num = (int)popup.args[1];
			int num2 = (int)popup.args[2];
			string text = (string)popup.args[3];
			string str = (string)popup.args[4];
			string str2 = (string)popup.args[5];
			string str3 = (string)popup.args[6];
			if (!string.IsNullOrEmpty(text))
			{
				text = Helpers.ColoredTag(text);
			}
			GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
			this.gui.TextLabel(new Rect(0f, 0f, 370f, 20f), (!remove) ? Language.AddToFavoritesQuestion : Language.RemoveFromFavoritesQuestion, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
			if (!remove)
			{
				this.gui.TextLabel(new Rect(0f, 80f, 370f, 20f), Language.AddToFavoritesHint, 16, "#717171", TextAnchor.MiddleCenter, true);
			}
			GUI.DrawTexture(new Rect(85f, 25f, (float)CarrierGUI.I.avatar.width, (float)CarrierGUI.I.avatar.height), CarrierGUI.I.avatar);
			GUI.DrawTexture(new Rect(140f, 30f, (float)MainGUI.Instance.rank_icon[num].width, (float)MainGUI.Instance.rank_icon[num].height), MainGUI.Instance.rank_icon[num]);
			if (num2 != 0)
			{
				Texture2D texture2D = ClanSystemWindow.I.Textures.statsClass[num2 - 1];
				GUI.DrawTexture(new Rect(165f, 25f, (float)texture2D.width, (float)texture2D.height), texture2D);
				GUI.Label(new Rect(190f, 30f, 200f, 20f), text + " " + str, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			}
			else
			{
				GUI.Label(new Rect(175f, 30f, 200f, 20f), text + " " + str, ClanSystemWindow.I.Styles.styleWhiteLabel16);
			}
			GUI.Label(new Rect(175f, 48f, 100f, 20f), str2 + " " + str3, ClanSystemWindow.I.Styles.styleGrayLabel14Left);
			this.btnClick = GUI.Button(new Rect(100f, 105f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			this.btnClickNo = GUI.Button(new Rect(187f, 105f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			if (this.btnClick && !popup.alreadyClicked)
			{
				Main.AddDatabaseRequestCallBack<EditWatchlist>(delegate
				{
					if (remove)
					{
						Main.UserInfo.WatchlistUsersId.Remove(userId);
					}
					else
					{
						Main.UserInfo.WatchlistUsersId.Add(userId);
					}
				}, delegate
				{
				}, new object[]
				{
					userId,
					remove
				});
				popup.alreadyClicked = true;
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
			if (this.btnClickNo && !popup.alreadyClicked)
			{
				popup.alreadyClicked = true;
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
			GUI.EndGroup();
		}
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x00075890 File Offset: 0x00073A90
	private void LeagueLoading(Popup popup)
	{
		this.gui.TextField(new Rect(12f, 60f, 395f, (float)this.gui.weapon_info.height), popup.desc, 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.gui.EndGroup();
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		Vector2 vector = new Vector2((float)(Screen.width / 2 - this.gui.settings_window[9].width / 2), (float)(Screen.height / 2 - this.gui.settings_window[9].height / 2 + 15));
		this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.gui.settings_window[9].width / 2), vector.y + (float)(this.gui.settings_window[9].height / 2)));
		this.gui.Picture(new Vector2(vector.x, vector.y), this.gui.settings_window[9]);
		this.gui.RotateGUI(0f, Vector2.zero);
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		if (LeagueContentDownloader.I.LeagueContentLoaded && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A1D RID: 2589 RVA: 0x00075A5C File Offset: 0x00073C5C
	private void LeagueNotification(Popup popup)
	{
		GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 10f, 370f, 20f), Language.ClansRaceAttention, 16, "#ff0000", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 40f, 370f, 20f), Language.LeagueNotification1, 16, "#ffffff", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 60f, 370f, 40f), Language.LeagueNotification2, 16, "#ffffff", TextAnchor.MiddleCenter, true);
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		GUI.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A1E RID: 2590 RVA: 0x00075C50 File Offset: 0x00073E50
	private void DownloadedPrecentage(Popup popup)
	{
		this.gui.TextField(new Rect(12f, 60f, 395f, (float)this.gui.weapon_info.height), popup.desc, 16, "#FFFFFF", TextAnchor.UpperCenter, false, false);
		this.gui.EndGroup();
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		int num = 0;
		if (popup.popupState == PopupState.precentageWeaponsProgress)
		{
			num = ((NetworkSettingsController.Instance.WeaponsSizeDownloaded != 0) ? (100 * NetworkSettingsController.Instance.WeaponsSizeDownloaded / NetworkSettingsController.Instance.TotalWeaponsSize) : 0);
		}
		else if (popup.popupState == PopupState.precentageMapsProgress)
		{
			num = ((NetworkSettingsController.Instance.MapsSizeDownloaded != 0) ? (100 * NetworkSettingsController.Instance.MapsSizeDownloaded / NetworkSettingsController.Instance.TotalMapsSize) : 0);
		}
		this.gui.TextLabel(new Rect(200f, 100f, 50f, 50f), num.ToString("D") + "%", 16, "#FFFFFF", TextAnchor.UpperLeft, true);
		this.gui.EndGroup();
		if (popup.progressName != string.Empty)
		{
			this.gui.TextField(new Rect((float)((Screen.width - this.gui.settings_window[9].width) / 2 - 3), (float)((Screen.height - this.gui.settings_window[9].height) / 2 + 15), (float)this.gui.settings_window[9].width, (float)this.gui.settings_window[9].height), (int)((double)Loader.Progress(popup.progressName) * 100.0), 20, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		}
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
	}

	// Token: 0x06000A1F RID: 2591 RVA: 0x00075E74 File Offset: 0x00074074
	private void RepairAllWeapon(Popup popup)
	{
		string text = popup.args[0].ToString();
		string repairAllWeaponPopupBody = Language.RepairAllWeaponPopupBody1;
		string str = Language.RepairAllWeaponPopupBody2 + ": ";
		float left = this.gui.CalcWidth(repairAllWeaponPopupBody + text, this.gui.fontDNC57, 14);
		GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 10f, 370f, 20f), repairAllWeaponPopupBody, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 40f, 370f, 20f), str + Helpers.SeparateNumericString(text), 16, "#ffffff", TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect(left, 38f, (float)this.gui.crIcon.width, (float)this.gui.crIcon.height), this.gui.crIcon);
		this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		GUI.EndGroup();
		if (this.btnClick && !popup.alreadyClicked)
		{
			popup.action();
			popup.alreadyClicked = true;
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x000760B8 File Offset: 0x000742B8
	private void BuyMasteringMetaLevel(Popup popup)
	{
		int num = (int)popup.args[0];
		MasteringMetaLevel masteringMetaLevel = (MasteringMetaLevel)popup.args[1];
		int num2 = (masteringMetaLevel.Id <= 0) ? Main.UserInfo.weaponsStates[num].CurrentWeapon.wtaskPrice : masteringMetaLevel.GpToUnlock;
		bool flag = Main.UserInfo.GP >= num2;
		GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 40f, 370f, 20f), popup.desc, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		string text = string.Concat(new object[]
		{
			"<color=#c0c0c0>",
			Language.BankFor.ToLower(),
			"</color> <color=#FF9314>",
			num2,
			"</color>"
		});
		Texture2D gldIcon = MainGUI.Instance.gldIcon;
		float num3 = (float)gldIcon.width + MainGUI.Instance.CalcWidth(text, MainGUI.Instance.fontDNC57, 16);
		this.gui.TextLabel(new Rect(15f - num3 * 0.5f, 60f, 370f, 20f), text, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect((370f + num3) * 0.5f - (float)gldIcon.width, 58f, (float)gldIcon.width, (float)gldIcon.height), gldIcon);
		if (flag)
		{
			this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		}
		else
		{
			this.gui.TextLabel(new Rect(0f, 85f, 370f, 22f), "<color=#c0c0c0>" + Language.NotEnoughGp + "</color>", 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			int width = CWGUI.p.MastFillUpBalanceBtnStyle.normal.background.width;
			int height = CWGUI.p.MastFillUpBalanceBtnStyle.normal.background.height;
			if (GUI.Button(new Rect((float)(370 - width) * 0.5f, 110f, (float)width, (float)height), Language.FillUpBalance, CWGUI.p.MastFillUpBalanceBtnStyle))
			{
				popup.Hide(null, 0f, 0f);
				EventFactory.Call("ShowBankGUI", null);
			}
		}
		GUI.EndGroup();
		if (flag)
		{
			if (this.btnClick && !popup.alreadyClicked)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.MasteringPopupMetaBuyHeader, Language.MasteringPopupMetaBuyProcess, PopupState.progress, false, false, string.Empty, string.Empty));
				popup.alreadyClicked = true;
				Main.AddDatabaseRequestCallBack<BuyMasteringMetaLevel>(delegate
				{
					popup.action();
					popup.Hide(null, 0.15f, 0f);
					if (popup.closeMethod != string.Empty)
					{
						this.Invoke(popup.closeMethod, 0f);
					}
				}, delegate
				{
				}, popup.args);
			}
			else if (this.btnClickNo && !popup.alreadyClicked)
			{
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
		}
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x000764CC File Offset: 0x000746CC
	private void BuyMasteringMod(Popup popup)
	{
		int key = (int)popup.args[0];
		MasteringMod masteringMod = (MasteringMod)popup.args[1];
		bool flag = Main.UserInfo.Mastering.MasteringPoints >= masteringMod.WeaponSpecificInfo[key].Mp;
		string textColor = "#FFD800";
		if (masteringMod.Type == ModType.optic)
		{
			textColor = "#00BAFF";
		}
		if (masteringMod.Type == ModType.tactical)
		{
			textColor = "#90FF00";
		}
		GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 5f, 370f, 20f), popup.desc, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 25f, 370f, 20f), masteringMod.FullName, 16, textColor, TextAnchor.MiddleCenter, true);
		GUI.DrawTexture(new Rect((float)(370 - masteringMod.BigIcon.width) * 0.5f, 45f, (float)masteringMod.BigIcon.width, (float)masteringMod.BigIcon.height), masteringMod.BigIcon);
		string text = string.Concat(new object[]
		{
			"<color=#c0c0c0>",
			Language.BankFor.ToLower(),
			"</color> <color=#FF9314>",
			masteringMod.WeaponSpecificInfo[key].Mp,
			"</color>"
		});
		Texture2D texture2D = MainGUI.Instance.masteringTextures[6];
		float num = (float)texture2D.width + MainGUI.Instance.CalcWidth(text, MainGUI.Instance.fontDNC57, 16);
		if (flag)
		{
			this.gui.TextLabel(new Rect(15f - num * 0.5f, 87f, 370f, 20f), text, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			GUI.DrawTexture(new Rect((370f + num) * 0.5f - (float)texture2D.width, 89f, (float)texture2D.width, (float)texture2D.height), texture2D);
			this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		}
		else
		{
			this.gui.TextLabel(new Rect(0f, 87f, 370f, 20f), "<color=#c0c0c0>" + Language.MasteringNotEnoughMp + "</color> ", 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			this.btnClick = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		}
		GUI.EndGroup();
		if (flag)
		{
			if (this.btnClick && !popup.alreadyClicked)
			{
				popup.alreadyClicked = true;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.MasteringPopupModBuyHeader, Language.MasteringPopupModBuyProcess, PopupState.progress, false, false, string.Empty, string.Empty));
				Main.AddDatabaseRequestCallBack<BuyMasteringMod>(delegate
				{
					popup.action();
					popup.Hide(null, 0.15f, 0f);
					if (popup.closeMethod != string.Empty)
					{
						this.Invoke(popup.closeMethod, 0f);
					}
				}, delegate
				{
				}, popup.args);
			}
			else if (this.btnClickNo && !popup.alreadyClicked)
			{
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
		}
		else if (this.btnClick && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A22 RID: 2594 RVA: 0x0007698C File Offset: 0x00074B8C
	private void PurchaseCamouflage(Popup popup)
	{
		CamouflageInfo cInfo = (CamouflageInfo)popup.args[0];
		Texture2D[] array = new Texture2D[]
		{
			this.gui.crIcon,
			this.gui.gldIcon,
			this.gui.masteringTextures[6]
		};
		Currency currencyType = cInfo.CurrencyType;
		bool flag;
		string str;
		if (currencyType != Currency.Cr)
		{
			if (currencyType != Currency.Gp)
			{
				flag = (Main.UserInfo.Mastering.MasteringPoints >= cInfo.Price);
				str = Language.MasteringNotEnoughMp;
			}
			else
			{
				flag = (Main.UserInfo.GP >= cInfo.Price);
				str = Language.NotEnoughGp;
			}
		}
		else
		{
			flag = (Main.UserInfo.CR >= cInfo.Price);
			str = Language.NotEnoughCr;
		}
		string textColor = RarityColors.Colors[cInfo.Rarity.ToString().ToLower()].ToString();
		GUI.BeginGroup(new Rect(31f, 47f, 390f, 145f));
		this.gui.TextLabel(new Rect(0f, 5f, 370f, 20f), popup.desc, 16, "#c0c0c0", TextAnchor.MiddleCenter, true);
		this.gui.TextLabel(new Rect(0f, 25f, 370f, 20f), cInfo.FullName, 16, textColor, TextAnchor.MiddleCenter, true);
		MasteringMod modById = ModsStorage.Instance().GetModById(cInfo.Id);
		Texture2D texture2D = (modById == null) ? null : modById.BigIcon;
		if (texture2D)
		{
			GUI.DrawTexture(new Rect((float)(370 - texture2D.width) * 0.5f, 48f, (float)texture2D.width, (float)texture2D.height), texture2D);
		}
		string text = (cInfo.CurrencyType != Currency.Gp) ? "<color=#FFFFFF>" : "<color=#FF9314>";
		string text2 = string.Concat(new string[]
		{
			"<color=#c0c0c0>",
			Language.BankFor.ToLower(),
			"</color> ",
			text,
			Helpers.SeparateNumericString(cInfo.Price.ToString()),
			"</color>"
		});
		if (cInfo.Available)
		{
			text2 = Language.FreeCamouflage;
		}
		Texture2D texture2D2 = array[(int)cInfo.CurrencyType];
		float num = MainGUI.Instance.CalcWidth(text2, MainGUI.Instance.fontDNC57, 16);
		if (flag || cInfo.Available)
		{
			this.gui.TextLabel(new Rect((float)((!cInfo.Available) ? (-(float)texture2D2.width / 2) : 0), 87f, 370f, 20f), text2, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			if (!cInfo.Available)
			{
				GUI.DrawTexture(new Rect((370f + num - (float)texture2D2.width) * 0.5f, (float)((cInfo.CurrencyType != Currency.Mp) ? 85 : 89), (float)texture2D2.width, (float)texture2D2.height), texture2D2);
			}
			this.btnClick = GUI.Button(new Rect(210f, 110f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
			this.btnClickNo = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		}
		else
		{
			this.gui.TextLabel(new Rect(0f, 87f, 370f, 20f), "<color=#c0c0c0>" + str + "</color> ", 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			this.btnClick = GUI.Button(new Rect(297f, 110f, 87f, 29f), Language.OK, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		}
		GUI.EndGroup();
		if (flag || cInfo.Available)
		{
			if (this.btnClick && !popup.alreadyClicked)
			{
				popup.alreadyClicked = true;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.PopupCamouflageBuyHeader, Language.PopupCamouflageBuyProcess, PopupState.progress, false, false, string.Empty, string.Empty));
				Main.AddDatabaseRequestCallBack<PurchaseCamouflage>(delegate
				{
					popup.action();
					popup.Hide(null, 0.15f, 0f);
					if (popup.closeMethod != string.Empty)
					{
						this.Invoke(popup.closeMethod, 0f);
					}
					Main.UserInfo.Mastering.WearStates.Add(cInfo.Id, null);
					if (!cInfo.Available)
					{
						Currency currencyType2 = cInfo.CurrencyType;
						if (currencyType2 != Currency.Cr)
						{
							if (currencyType2 != Currency.Gp)
							{
								Main.UserInfo.Mastering.MasteringPoints -= cInfo.Price;
							}
							else
							{
								Main.UserInfo.GP -= cInfo.Price;
							}
						}
						else
						{
							Main.UserInfo.CR -= cInfo.Price;
						}
					}
					Audio.Play(this.gui.buy_sound);
				}, delegate
				{
				}, new object[]
				{
					cInfo.Id
				});
			}
			else if (this.btnClickNo && !popup.alreadyClicked)
			{
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					base.Invoke(popup.closeMethod, 0f);
				}
			}
		}
		else if (this.btnClick && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00076FC0 File Offset: 0x000751C0
	private void BuySP(Popup popup)
	{
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		this.gui.BeginGroup(new Rect(31f, 47f, 370f, 135f));
		this.gui.TextField(new Rect(0f, 15f, 370f, 100f), Language.BuySPAttention, 16, "#c0c0c0", TextAnchor.UpperCenter, false, false);
		this.gui.TextField(new Rect(145f, 60f, 120f, 30f), Language.BuySPFor, 16, "#717171", TextAnchor.UpperLeft, false, false);
		this.unlock = this.gui.Button(new Vector2(140f, 80f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Globals.I.buySPPrice.ToString(), 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
		this.gui.Picture(new Vector2(140f + ((float)this.gui.server_window[14].width - this.tmpwidth) / 2f + this.tmpwidth, (float)(80 + Mathf.Abs(this.gui.server_window[14].height - this.gui.gldIcon.height) / 2)), this.gui.gldIcon);
		if (this.unlock.Clicked && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequest<BuySP>(new object[0]);
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x000771EC File Offset: 0x000753EC
	private void BuyMp(Popup popup)
	{
		GUI.enabled = !popup.alreadyClicked;
		this.gui.Picture(new Vector2(24f, 130f), this.gui.GetComponent<HelpersGUI>().lowBar);
		this.gui.BeginGroup(new Rect(31f, 47f, 390f, 135f));
		this._mpToBuy = (int)MainGUI.Instance.FloatSlider(new Vector2(100f, 40f), (float)this._mpToBuy, 1f, 100f, true, false, true);
		int totalCost = this._mpToBuy * Globals.I.MpCost;
		bool flag = Main.UserInfo.GP >= totalCost;
		string text = Language.Price + " " + totalCost;
		this.gui.TextLabel(new Rect(0f, 103f, 200f, 40f), text, 25, (!flag) ? "#FF0000" : "#FFFFFF", TextAnchor.MiddleLeft, true);
		float num = MainGUI.Instance.CalcWidth(text, MainGUI.Instance.fontDNC57, 25);
		this.gui.Picture(new Vector2(num - 4f, 111f), MainGUI.Instance.gldIcon);
		this.gui.TextLabel(new Rect(245f, 80f, 100f, 30f), Language.HGBuyQuestion, 15, "#999999", TextAnchor.MiddleCenter, true);
		this.btnClick = GUI.Button(new Rect(200f, 103f, 87f, 29f), Language.Yes, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		this.btnClickNo = GUI.Button(new Rect(290f, 103f, 87f, 29f), Language.No, CarrierGUI.I.clanSystemWindow.Styles.stylePopupYesNoBtn);
		if (popup.alreadyClicked)
		{
			Texture2D krutilkaSmall = MainGUI.Instance.KrutilkaSmall;
			Rect position = new Rect(179f, 50f, (float)krutilkaSmall.width, (float)krutilkaSmall.height);
			GUIUtility.RotateAroundPivot(180f * Time.time, position.center);
			GUI.DrawTexture(position, krutilkaSmall);
			GUIUtility.RotateAroundPivot(-180f * Time.time, position.center);
		}
		if (this.btnClick && flag && !popup.alreadyClicked)
		{
			popup.alreadyClicked = true;
			Main.AddDatabaseRequestCallBack<BuyMp>(delegate
			{
				Main.UserInfo.GP -= totalCost;
				Main.UserInfo.Mastering.MasteringPoints += this._mpToBuy;
				Audio.Play(MainGUI.Instance.buy_sound);
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					this.Invoke(popup.closeMethod, 0f);
				}
			}, delegate
			{
				popup.Hide(null, 0.15f, 0f);
				if (popup.closeMethod != string.Empty)
				{
					this.Invoke(popup.closeMethod, 0f);
				}
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.MpBuyError, PopupState.information, true, true, string.Empty, string.Empty));
			}, new object[]
			{
				this._mpToBuy
			});
		}
		else if (this.btnClickNo && !popup.alreadyClicked)
		{
			popup.Hide(null, 0.15f, 0f);
			if (popup.closeMethod != string.Empty)
			{
				base.Invoke(popup.closeMethod, 0f);
			}
		}
		this.gui.EndGroup();
		GUI.enabled = true;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x00077568 File Offset: 0x00075768
	private void QuickGame(Popup popup)
	{
		this.gui.Picture(new Vector2(20f, 90f), this.progressBonus);
		this.QuickGameFindInHardcore = GUI.Toggle(new Rect(130f, 24f, 26f, 22f), this.QuickGameFindInHardcore, string.Empty, CWGUI.p.hardcoreCheckBox);
		this.QuickGameFindInFull = GUI.Toggle(new Rect(160f, 24f, (float)CWGUI.p.smallCheckBox.normal.background.width, (float)CWGUI.p.smallCheckBox.normal.background.height), this.QuickGameFindInFull, string.Empty, CWGUI.p.smallCheckBox);
		MainGUI.Instance.TextLabel(new Rect(185f, 24f, 170f, (float)CWGUI.p.smallCheckBox.normal.background.height), Language.FindInFull, 14, "#FFFFFF", TextAnchor.MiddleLeft, true);
		this.tmpwidth = this.gui.CalcWidth("100", this.gui.fontDNC57, 16);
		Rect rect = new Rect(25f, 40f, 385f, 355f);
		this.gui.BeginGroup(rect);
		this.gui.TextLabel(new Rect(0f, 8f, 380f, 25f), Language.QuickGameDescrCutted[0], 15, "#ffffff", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(0f, 23f, 370f, 25f), Language.QuickGameDescrCutted[1], 15, "#ffffff", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(0f, 108f, 400f, 50f), Language.GameModeDescrCutted[(this.DropDowntempIndex >= 1) ? (this.DropDowntempIndex - 1) : this.DropDowntempIndex, 0], 14, "#c0c0c0", TextAnchor.UpperLeft, true);
		this.gui.TextLabel(new Rect(0f, 123f, 400f, 50f), Language.GameModeDescrCutted[(this.DropDowntempIndex >= 1) ? (this.DropDowntempIndex - 1) : this.DropDowntempIndex, 1], 14, "#c0c0c0", TextAnchor.UpperLeft, true);
		if (!this.StartSearchGames)
		{
			this.gui.TextLabel(new Rect(0f, 53f, 60f, 25f), Language.Mode, 15, "#c0c0c0", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect(0f, 80f, 50f, 25f), Language.Map, 15, "#c0c0c0", TextAnchor.UpperLeft, true);
			this.gui.BeginGroup(new Rect(60f, 80f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.typeDropDown);
			this.serverGUI.DropDown<string>(new Vector2(0f, 0f), ref this.mapScrollPos, ref this.mapDropDown, this.GetMapsByMode(this.gameModes[this.DropDowntempIndex]), ref this.MapIndex);
			this.gui.EndGroup();
			this.gui.BeginGroup(new Rect(60f, 53f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.mapDropDown);
			this.serverGUI.DropDown<string>(new Vector2(0f, 0f), ref this.typeScrollPos, ref this.typeDropDown, this.gameModesNames, ref this.DropDowntempIndex);
			if (this.MapIndex >= this.GetMapsByMode(this.gameModes[this.DropDowntempIndex]).Length)
			{
				this.MapIndex = 0;
			}
			this.gui.EndGroup();
			HostInfo hostInfo = this.SmartFindGame(this.gameModes[this.DropDowntempIndex], this.GetMapsByMode(this.gameModes[this.DropDowntempIndex])[this.MapIndex]);
			bool flag = hostInfo == null;
			if (flag && this.reloadTimer < Time.realtimeSinceStartup && this.reloadCounter < 5)
			{
				this.reloadTimer = Time.realtimeSinceStartup + 5f;
				this.reloadCounter += 1;
				Peer.ForceUpdateServers();
			}
			if (flag)
			{
				this.gui.color = Colors.alpha(this.gui.color, base.visibility * 0.2f);
			}
			if (flag)
			{
				this.unlock = this.gui.Button(new Vector2(rect.width - 130f, 60f), this.bankGui.Bank[6], this.bankGui.Bank[6], this.bankGui.Bank[6], string.Empty, 22, "#000000", TextAnchor.MiddleCenter, null, null, null, null);
			}
			else
			{
				this.unlock = this.gui.Button(new Vector2(rect.width - 130f, 60f), this.bankGui.Bank[6], this.bankGui.Bank[7], this.bankGui.Bank[7], Language.GoToTheBattle, 22, "#000000", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.buy_sound);
			}
			this.gui.color = Colors.alpha(this.gui.color, base.visibility);
			if (flag)
			{
				this.gui.color = Colors.alpha(this.gui.color, base.visibility * this.gui.color.a * Mathf.Abs(Mathf.Cos(Time.realtimeSinceStartup * 4f)));
				this.gui.TextLabel(new Rect(rect.width - 127f, 55f, 100f, 50f), (this.reloadTimer >= Time.realtimeSinceStartup) ? Language.GettingServerList : Language.GamesNotFound, 14, "#ffffff", TextAnchor.MiddleCenter, true);
				this.gui.color = Colors.alpha(this.gui.color, base.visibility);
			}
			int num = 10;
			if (this.unlock.Clicked && !flag && !popup.alreadyClicked)
			{
				if (Main.IsNewVersionAvailablePopupShowing)
				{
					EventFactory.Call("HidePopup", new Popup(WindowsID.QuickGameGUI, Language.QuickPlay, string.Empty, PopupState.quickGame, false, true, string.Empty, string.Empty));
				}
				else
				{
					for (;;)
					{
						popup.alreadyClicked = true;
						this.StartSearchGames = true;
						this.reloadCounter = 0;
						this.reloadTimer = 0f;
						this.listTryedToConnectGames.Clear();
						if (this.JoinGame(hostInfo))
						{
							break;
						}
						num--;
						Debug.Log("attempt" + num + " failed!");
						Debug.Log(Helpers.ColoredLog(hostInfo.Name, "#00FF00"));
						if (num <= 0)
						{
							this.OnConnectionFailed();
						}
					}
				}
			}
		}
		else
		{
			float angle = 180f * Time.realtimeSinceStartup * 1.5f;
			Vector2 vector = new Vector2(rect.width - 230f - (float)(this.gui.settings_window[9].width / 2), (float)(80 - this.gui.settings_window[9].height / 2));
			this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.gui.settings_window[9].width / 2), vector.y + (float)(this.gui.settings_window[9].height / 2)));
			this.gui.Picture(new Vector2(vector.x, vector.y), this.gui.settings_window[9]);
			this.gui.RotateGUI(0f, Vector2.zero);
			this.unlock = this.gui.Button(new Vector2(rect.width - 130f, 60f), this.bankGui.Bank[6], this.bankGui.Bank[7], this.bankGui.Bank[7], Language.Cancel, 22, "#000000", TextAnchor.MiddleCenter, null, null, null, null);
			if (this.unlock.Clicked)
			{
				this.StartSearchGames = false;
				this.gameListAssembled = false;
				popup.alreadyClicked = false;
				Peer.Disconnect(true);
				EventFactory.Call("HidePopup", new Popup(WindowsID.Connection, Language.Connection, Language.ConnectionCompleted, PopupState.information, false, true, string.Empty, string.Empty));
			}
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00077EAC File Offset: 0x000760AC
	private HostInfo FindGame(GameMode gameMode, string MapName)
	{
		HostInfo hostInfo = null;
		if (Peer.games == null)
		{
			return null;
		}
		for (int i = 0; i < Peer.games.Count; i++)
		{
			HostInfo hostInfo2 = Peer.games[i];
			if (!hostInfo2.PasswordProtected)
			{
				if (hostInfo2.Hardcore || !this.QuickGameFindInHardcore)
				{
					if (!hostInfo2.Hardcore || this.QuickGameFindInHardcore)
					{
						if ((hostInfo2.GameMode == gameMode || gameMode == GameMode.any) && (MapName == Language.anyFemale || hostInfo2.MapName == MapName) && hostInfo2.MinLevel <= Main.UserInfo.currentLevel && hostInfo2.MaxLevel >= Main.UserInfo.currentLevel && !this.listTryedToConnectGames.Contains(hostInfo2))
						{
							if (hostInfo == null)
							{
								hostInfo = hostInfo2;
							}
							if ((!hostInfo2.IsFull && !hostInfo2.IsFullByConnections) || this.SearchInFullGames)
							{
								if (hostInfo.ConnectionsCoint < hostInfo2.ConnectionsCoint)
								{
									hostInfo = hostInfo2;
								}
								if (PingManager.Ping(hostInfo2) <= PingManager.Ping(hostInfo) && hostInfo2.PlayerCount == hostInfo.PlayerCount)
								{
									hostInfo = hostInfo2;
								}
							}
						}
					}
				}
			}
		}
		return hostInfo;
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x00078004 File Offset: 0x00076204
	private void SortGameList()
	{
		this.gameList.Sort((HostInfo info1, HostInfo info2) => info1.Ping.CompareTo(info2.Ping));
		int ping = 0;
		if (this.gameList.Count > 0)
		{
			ping = this.gameList[0].Ping;
		}
		this.gameList.Sort(delegate(HostInfo info1, HostInfo info2)
		{
			if (info1.Ping != ping)
			{
				ping = info1.Ping;
			}
			if (info1.Ping == ping && info2.Ping == ping)
			{
				return info2.PlayerCount.CompareTo(info1.PlayerCount);
			}
			return -1;
		});
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x00078088 File Offset: 0x00076288
	private HostInfo SmartFindGame(GameMode mode, string map)
	{
		if (Peer.games.Count == 0)
		{
			return null;
		}
		if (this.tmpMode != mode)
		{
			this.tmpMode = mode;
			this.gameListAssembled = false;
		}
		if (this.tmpMap != map)
		{
			this.tmpMap = map;
			this.gameListAssembled = false;
		}
		if (this.tmpHC != this.QuickGameFindInHardcore)
		{
			this.tmpHC = this.QuickGameFindInHardcore;
			this.gameListAssembled = false;
		}
		if (this.tmpFull != this.SearchInFullGames)
		{
			this.tmpFull = this.SearchInFullGames;
			this.gameListAssembled = false;
		}
		if (!this.gameListAssembled)
		{
			this.gameList.Clear();
			this.showOnce = false;
			this.ctime = 0f;
			this.sortingComplete = false;
			this.gameFound = false;
			this.game = null;
			for (int i = 0; i < Peer.games.Count; i++)
			{
				if (!Peer.games[i].IsHidden && !Peer.games[i].PasswordProtected)
				{
					if (Peer.games[i].GameMode == mode || mode == GameMode.any)
					{
						if (!(Peer.games[i].MapName != map) || !(map != Language.anyFemale))
						{
							if (!Peer.games[i].IsFull)
							{
								if (!Peer.games[i].SkipInQuickGame)
								{
									if (this.QuickGameFindInHardcore)
									{
										if (!Peer.games[i].Hardcore)
										{
											goto IL_2E7;
										}
										if (!Main.UserInfo.skillUnlocked(Skills.car_hcunlock) && (Main.UserInfo.currentLevel < Peer.games[i].MinLevel || Main.UserInfo.currentLevel > Peer.games[i].MaxLevel))
										{
											goto IL_2E7;
										}
										if (Main.UserInfo.currentLevel > Peer.games[i].MaxLevel)
										{
											goto IL_2E7;
										}
									}
									else
									{
										if (Peer.games[i].Hardcore)
										{
											goto IL_2E7;
										}
										if (Main.UserInfo.currentLevel < Peer.games[i].MinLevel || Main.UserInfo.currentLevel > Peer.games[i].MaxLevel)
										{
											goto IL_2E7;
										}
									}
									if (!this.gameList.Contains(Peer.games[i]))
									{
										this.info = Peer.games[i];
										this.info.ping = SmartPingManager.GetPing1(this.info.ip);
										this.gameList.Add(Peer.games[i]);
									}
								}
							}
						}
					}
				}
				IL_2E7:;
			}
			this.gameListAssembled = true;
		}
		if (this.gameListAssembled && !this.sortingComplete && this.ctime == 0f)
		{
			if (this.ctime == 0f)
			{
				this.ctime = Time.realtimeSinceStartup;
			}
			this.SortGameList();
		}
		if (this.gameListAssembled && this.ctime != 0f && this.ctime + 2f < Time.realtimeSinceStartup)
		{
			this.ctime = 0f;
			this.SortGameList();
			this.sortingComplete = true;
		}
		if (this.gameList.Count > 0 && this.sortingComplete)
		{
			for (int j = 0; j < this.gameList.Count; j++)
			{
				if (this.gameList[j].ConnectionsCoint <= this.gameList[j].MaxPlayers)
				{
					if (this.gameList[j].PlayerCount != 0 || this.gameList[j].ConnectionsCoint != 0)
					{
						if (this.gameList[j].Ping < Globals.I.BadPing)
						{
							if (this.game == null)
							{
								this.game = this.gameList[j];
							}
							if (this.game.ConnectionsCoint < this.gameList[j].ConnectionsCoint && this.game.Ping > this.gameList[j].Ping && !this.listTryedToConnectGames.Contains(this.gameList[j]))
							{
								this.game = this.gameList[j];
							}
						}
					}
				}
			}
			if (this.game != null)
			{
				this.gameFound = true;
			}
		}
		return (!this.gameFound) ? null : this.game;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x00078598 File Offset: 0x00076798
	private string[] GetMapsByMode(GameMode mode)
	{
		if (mode == GameMode.Deathmatch)
		{
			return this.mapsDeathmatch;
		}
		if (mode == GameMode.TargetDesignation)
		{
			return this.mapsTargetDesignation;
		}
		if (mode == GameMode.TeamElimination)
		{
			return this.mapsTeamElimination;
		}
		if (mode == GameMode.TacticalConquest)
		{
			return this.mapsTacticalConquest;
		}
		if (mode == GameMode.any)
		{
			return this.mapsALL;
		}
		return this.mapsDeathmatch;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x000785F0 File Offset: 0x000767F0
	private bool JoinGame(HostInfo game)
	{
		if (game == null)
		{
			return false;
		}
		this.listTryedToConnectGames.Add(game);
		return Peer.JoinGame(game, false);
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00078610 File Offset: 0x00076810
	public bool IsShowingQuickGame
	{
		get
		{
			for (int i = 0; i < this.popups.Count; i++)
			{
				if (this.popups[i].windowID == 5)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00078654 File Offset: 0x00076854
	public bool IsFirstTimeInGame
	{
		get
		{
			for (int i = 0; i < this.popups.Count; i++)
			{
				if (this.popups[i].windowID == 28 && LoadProfile.newLevel == 0)
				{
					return true;
				}
			}
			return false;
		}
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x000786A4 File Offset: 0x000768A4
	private void CancelDownload()
	{
		Loader.Unload(false);
		EventFactory.Call("ShowPopup", new Popup(WindowsID.SettingsGUI, string.Empty, Language.ErrorDownloadingContent, PopupState.information, true, true, string.Empty, string.Empty));
	}

	// Token: 0x06000A2E RID: 2606 RVA: 0x000786E0 File Offset: 0x000768E0
	private void cancelReceive()
	{
		NetworkSettingsController.Instance.cancelReceive();
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000786EC File Offset: 0x000768EC
	public override Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width / 2 - this.Width / 2) + this.rect.x, (float)(Screen.height / 2 - this.Height / 2) + this.rect.y, (float)this.Width, (float)(this.Height * 2));
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00078748 File Offset: 0x00076948
	public override int Width
	{
		get
		{
			return this.gui.settings_window[8].width;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0007875C File Offset: 0x0007695C
	public override int Height
	{
		get
		{
			return this.gui.settings_window[8].height;
		}
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x00078770 File Offset: 0x00076970
	public override void MainInitialize()
	{
		PopupGUI.thisObject = this;
		this.isUpdating = true;
		this.isRendering = true;
		base.MainInitialize();
		this.carrierGUI = this.gui.GetComponent<CarrierGUI>();
		this.bankGui = this.gui.GetComponent<BankGUI>();
		this.serverGUI = this.gui.GetComponent<ServerGUI>();
		SingletoneForm<Peer>.Instance.events.Add(this);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x000787DC File Offset: 0x000769DC
	public override void OnInitialized()
	{
		base.OnInitialized();
		this._nickColor = (this._previousValidNickColor = Main.UserInfo.nickColor.Substring(1));
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x00078810 File Offset: 0x00076A10
	public override void Clear()
	{
		this.popups = new List<Popup>();
		this.error = new Alpha();
		this._highPingAlpha = new Alpha();
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x00078834 File Offset: 0x00076A34
	public override void Register()
	{
		EventFactory.Register("ShowPopup", this);
		EventFactory.Register("HidePopup", this);
		EventFactory.Register("ShowConnectionProblem", this);
		EventFactory.Register("ShowPingProblem", this);
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x00078870 File Offset: 0x00076A70
	public override void GameGUI()
	{
		if (this.error.Visible)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.error.visibility);
			this.gui.Picture(new Vector2(242f, 0f), this.connectionError);
			GUI.Label(new Rect(217f, (float)this.connectionError.height, (float)(this.connectionError.width + 40), 20f), Language.HightPacketLoss, this.connectionProblemStyle);
		}
		else if (this._highPingAlpha.Visible)
		{
			this.gui.color = new Color(1f, 1f, 1f, this._highPingAlpha.visibility);
			this.gui.Picture(new Vector2(242f, 0f), this.connectionError);
			GUI.Label(new Rect(217f, (float)this.connectionError.height, (float)(this.connectionError.width + 40), 20f), Language.HighPing, this.connectionProblemStyle);
		}
	}

	// Token: 0x06000A37 RID: 2615 RVA: 0x000789AC File Offset: 0x00076BAC
	public override void InterfaceGUI()
	{
		for (int i = 0; i < this.popups.Count; i++)
		{
			if (this.popups[i].popupA.Visible)
			{
				this.SimplePopup(this.popups[i]);
			}
		}
	}

	// Token: 0x06000A38 RID: 2616 RVA: 0x00078A04 File Offset: 0x00076C04
	public override void OnUpdate()
	{
		this.CleanUp();
		if (this.popups.Count == 0)
		{
			this.Hide(0.35f);
		}
	}

	// Token: 0x06000A39 RID: 2617 RVA: 0x00078A28 File Offset: 0x00076C28
	public void OnConnectionFailed()
	{
		while (this.StartSearchGames)
		{
			HostInfo hostInfo = this.SmartFindGame(this.gameModes[this.DropDowntempIndex], this.GetMapsByMode(this.gameModes[this.DropDowntempIndex])[this.MapIndex]);
			if (hostInfo != null)
			{
				this.JoinGame(hostInfo);
			}
			else
			{
				if (!this.SearchInFullGames && this.QuickGameFindInFull)
				{
					this.SearchInFullGames = true;
					continue;
				}
				EventFactory.Call("HidePopup", new Popup(WindowsID.Connection, Language.Connection, Language.ConnectionCompleted, PopupState.information, false, true, string.Empty, string.Empty));
				this.StartSearchGames = false;
			}
			return;
		}
	}

	// Token: 0x06000A3A RID: 2618 RVA: 0x00078AE0 File Offset: 0x00076CE0
	public void OnConnectionSuccessful()
	{
		this.listTryedToConnectGames.Clear();
		for (int i = 0; i < this.popups.Count; i++)
		{
			if (this.popups[i].popupState == PopupState.quickGame)
			{
				this.popups[i].Hide(null, 0.15f, 0f);
				if (this.popups[i].closeMethod != string.Empty)
				{
					base.Invoke(this.popups[i].closeMethod, 0f);
				}
				break;
			}
		}
		this.StartSearchGames = false;
		this.SearchInFullGames = false;
	}

	// Token: 0x06000A3B RID: 2619 RVA: 0x00078B98 File Offset: 0x00076D98
	public void OnUpdateServersList()
	{
		this.listTryedToConnectGames.Clear();
	}

	// Token: 0x04000B42 RID: 2882
	private const int MailFieldY = 42;

	// Token: 0x04000B43 RID: 2883
	private const int PassFieldY = 92;

	// Token: 0x04000B44 RID: 2884
	private const int CaptionAndTextFieldHeightDelta = 20;

	// Token: 0x04000B45 RID: 2885
	private const float SEND_LOGIN_COOLDOWN = 2f;

	// Token: 0x04000B46 RID: 2886
	private const string SAVED_PASSWORD = "******";

	// Token: 0x04000B47 RID: 2887
	public Texture2D connectionError;

	// Token: 0x04000B48 RID: 2888
	public Texture2D warning;

	// Token: 0x04000B49 RID: 2889
	public Texture2D banned;

	// Token: 0x04000B4A RID: 2890
	public Texture2D buyGp;

	// Token: 0x04000B4B RID: 2891
	public Texture2D[] invite;

	// Token: 0x04000B4C RID: 2892
	public Texture2D progressBonus;

	// Token: 0x04000B4D RID: 2893
	public Texture2D news_stripe;

	// Token: 0x04000B4E RID: 2894
	public Texture2D discount_stripe;

	// Token: 0x04000B4F RID: 2895
	public GUIStyle RoulettePopupStyle;

	// Token: 0x04000B50 RID: 2896
	public GUIStyle RoulettePopupBtnStyle;

	// Token: 0x04000B51 RID: 2897
	private bool QuickGameFindInFull = true;

	// Token: 0x04000B52 RID: 2898
	private bool QuickGameFindInHardcore;

	// Token: 0x04000B53 RID: 2899
	private bool QuickGamePlayWithFriends;

	// Token: 0x04000B54 RID: 2900
	private float reloadTimer;

	// Token: 0x04000B55 RID: 2901
	private byte reloadCounter;

	// Token: 0x04000B56 RID: 2902
	private int attemptsToBuy = 1;

	// Token: 0x04000B57 RID: 2903
	private string _clanMessage;

	// Token: 0x04000B58 RID: 2904
	public static PopupGUI thisObject;

	// Token: 0x04000B59 RID: 2905
	[HideInInspector]
	public List<Popup> popups = new List<Popup>();

	// Token: 0x04000B5A RID: 2906
	private BankGUI bankGui;

	// Token: 0x04000B5B RID: 2907
	private ServerGUI serverGUI;

	// Token: 0x04000B5C RID: 2908
	private Alpha error = new Alpha();

	// Token: 0x04000B5D RID: 2909
	private Alpha _highPingAlpha = new Alpha();

	// Token: 0x04000B5E RID: 2910
	private CarrierGUI carrierGUI;

	// Token: 0x04000B5F RID: 2911
	private int selectIndex;

	// Token: 0x04000B60 RID: 2912
	private float tmpwidth;

	// Token: 0x04000B61 RID: 2913
	private float twidth;

	// Token: 0x04000B62 RID: 2914
	private ButtonState unlock;

	// Token: 0x04000B63 RID: 2915
	private ButtonState unlock2;

	// Token: 0x04000B64 RID: 2916
	private bool btnClick;

	// Token: 0x04000B65 RID: 2917
	private bool btnClickNo;

	// Token: 0x04000B66 RID: 2918
	private int lockButton = -1;

	// Token: 0x04000B67 RID: 2919
	private Vector2 mapScrollPos = Vector2.zero;

	// Token: 0x04000B68 RID: 2920
	private bool mapDropDown;

	// Token: 0x04000B69 RID: 2921
	private int MapIndex;

	// Token: 0x04000B6A RID: 2922
	private bool typeDropDown;

	// Token: 0x04000B6B RID: 2923
	private int DropDowntempIndex;

	// Token: 0x04000B6C RID: 2924
	private Vector2 typeScrollPos = Vector2.zero;

	// Token: 0x04000B6D RID: 2925
	private string mapAnyMapName = "ANY";

	// Token: 0x04000B6E RID: 2926
	private GameMode[] gameModes = new GameMode[]
	{
		GameMode.any,
		GameMode.Deathmatch,
		GameMode.TargetDesignation,
		GameMode.TeamElimination,
		GameMode.TacticalConquest
	};

	// Token: 0x04000B6F RID: 2927
	private string[] gameModesNames = new string[]
	{
		"ANY1",
		"Deathmatch",
		"TargetDesignation",
		"TeamElimination",
		"TacticalConquest"
	};

	// Token: 0x04000B70 RID: 2928
	private List<string> mapsDeathmatchList = new List<string>();

	// Token: 0x04000B71 RID: 2929
	private List<string> mapsTargetDesignationList = new List<string>();

	// Token: 0x04000B72 RID: 2930
	private List<string> mapsTeamEliminationList = new List<string>();

	// Token: 0x04000B73 RID: 2931
	private List<string> mapsTacticalConquestList = new List<string>();

	// Token: 0x04000B74 RID: 2932
	private List<string> mapsAllList = new List<string>();

	// Token: 0x04000B75 RID: 2933
	private string[] mapsDeathmatch;

	// Token: 0x04000B76 RID: 2934
	private string[] mapsTargetDesignation;

	// Token: 0x04000B77 RID: 2935
	private string[] mapsTeamElimination;

	// Token: 0x04000B78 RID: 2936
	private string[] mapsTacticalConquest;

	// Token: 0x04000B79 RID: 2937
	private string[] mapsALL;

	// Token: 0x04000B7A RID: 2938
	public static bool showingDaliyBonus;

	// Token: 0x04000B7B RID: 2939
	public static bool showingNews;

	// Token: 0x04000B7C RID: 2940
	private int promoPage;

	// Token: 0x04000B7D RID: 2941
	private Alpha promoAlpha = new Alpha();

	// Token: 0x04000B7E RID: 2942
	private bool[] rentToggle;

	// Token: 0x04000B7F RID: 2943
	private GUIStyle tmpStyle;

	// Token: 0x04000B80 RID: 2944
	private GUIStyle tgl_1_Style;

	// Token: 0x04000B81 RID: 2945
	private GUIStyle tgl_2_Style;

	// Token: 0x04000B82 RID: 2946
	private GUIStyle tgl_3_Style;

	// Token: 0x04000B83 RID: 2947
	[SerializeField]
	public GUIStyle connectionProblemStyle;

	// Token: 0x04000B84 RID: 2948
	private List<HostInfo> listTryedToConnectGames;

	// Token: 0x04000B85 RID: 2949
	private bool StartSearchGames;

	// Token: 0x04000B86 RID: 2950
	private bool SearchInFullGames;

	// Token: 0x04000B87 RID: 2951
	public PopupGUI.HOFPositions hofPositions;

	// Token: 0x04000B88 RID: 2952
	public PopupGUI.LeagueWinnerPopupPositions LWPPos;

	// Token: 0x04000B89 RID: 2953
	private Texture2D _award;

	// Token: 0x04000B8A RID: 2954
	private bool _awardLoaded;

	// Token: 0x04000B8B RID: 2955
	private PopupGUI.LWPInfo _lwpInfo;

	// Token: 0x04000B8C RID: 2956
	private ButtonState StandaloneLoginState;

	// Token: 0x04000B8D RID: 2957
	private bool _canSendLogin;

	// Token: 0x04000B8E RID: 2958
	private bool _loginFailed;

	// Token: 0x04000B8F RID: 2959
	private string _pass;

	// Token: 0x04000B90 RID: 2960
	private bool _useHashedPass;

	// Token: 0x04000B91 RID: 2961
	private Timer _sendLoginCoolDownTimer;

	// Token: 0x04000B92 RID: 2962
	private Timer _attemptCooldown;

	// Token: 0x04000B93 RID: 2963
	private bool _showPass;

	// Token: 0x04000B94 RID: 2964
	private Dictionary<string, object>[] _awardsDict;

	// Token: 0x04000B95 RID: 2965
	private string _nickColor;

	// Token: 0x04000B96 RID: 2966
	private string _previousValidNickColor;

	// Token: 0x04000B97 RID: 2967
	private string _url;

	// Token: 0x04000B98 RID: 2968
	private string _color;

	// Token: 0x04000B99 RID: 2969
	private int _mpToBuy;

	// Token: 0x04000B9A RID: 2970
	private List<HostInfo> gameList;

	// Token: 0x04000B9B RID: 2971
	private bool gameListAssembled;

	// Token: 0x04000B9C RID: 2972
	private bool sortingComplete;

	// Token: 0x04000B9D RID: 2973
	private float ctime;

	// Token: 0x04000B9E RID: 2974
	private bool showOnce;

	// Token: 0x04000B9F RID: 2975
	private bool tmpHC;

	// Token: 0x04000BA0 RID: 2976
	private bool tmpFull;

	// Token: 0x04000BA1 RID: 2977
	private GameMode tmpMode;

	// Token: 0x04000BA2 RID: 2978
	private string tmpMap;

	// Token: 0x04000BA3 RID: 2979
	private bool gameFound;

	// Token: 0x04000BA4 RID: 2980
	private HostInfo info;

	// Token: 0x04000BA5 RID: 2981
	private HostInfo game;

	// Token: 0x0200016D RID: 365
	[Serializable]
	internal class HOFPositions
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00078D6C File Offset: 0x00076F6C
		public void DrawTexture(ref Rect rect, Texture2D texture)
		{
			if (texture == null)
			{
				return;
			}
			rect.Set(rect.x, rect.y, (float)texture.width, (float)texture.height);
			GUI.DrawTexture(rect, texture);
		}

		// Token: 0x04000BB7 RID: 2999
		public Rect Month = default(Rect);

		// Token: 0x04000BB8 RID: 3000
		public Rect Name = default(Rect);

		// Token: 0x04000BB9 RID: 3001
		public Rect ClanTag = default(Rect);

		// Token: 0x04000BBA RID: 3002
		public Rect Nick = default(Rect);

		// Token: 0x04000BBB RID: 3003
		public Rect Level = default(Rect);

		// Token: 0x04000BBC RID: 3004
		public Rect Avatar = default(Rect);

		// Token: 0x04000BBD RID: 3005
		public Rect Class = default(Rect);

		// Token: 0x04000BBE RID: 3006
		public Rect Background = default(Rect);

		// Token: 0x04000BBF RID: 3007
		public Rect BackgroundIcon = default(Rect);

		// Token: 0x04000BC0 RID: 3008
		public Rect HOFButton = default(Rect);

		// Token: 0x04000BC1 RID: 3009
		public Rect OKButton = default(Rect);

		// Token: 0x04000BC2 RID: 3010
		public Rect HOFGroup = default(Rect);

		// Token: 0x04000BC3 RID: 3011
		public Rect Nomination = default(Rect);

		// Token: 0x04000BC4 RID: 3012
		public Rect NominationIcon = default(Rect);

		// Token: 0x04000BC5 RID: 3013
		public Rect Value = default(Rect);

		// Token: 0x04000BC6 RID: 3014
		public Texture2D TextureBackground;

		// Token: 0x04000BC7 RID: 3015
		public Texture2D TextureBackIcon;

		// Token: 0x04000BC8 RID: 3016
		public Texture2D TextureHOFGroup;

		// Token: 0x04000BC9 RID: 3017
		public Texture2D TextureAvatar;

		// Token: 0x04000BCA RID: 3018
		public Texture2D TextureLVL;

		// Token: 0x04000BCB RID: 3019
		public Texture2D TextureClass;

		// Token: 0x04000BCC RID: 3020
		public Texture2D TextureButtonIdle;

		// Token: 0x04000BCD RID: 3021
		public Texture2D TextureButtonOver;

		// Token: 0x04000BCE RID: 3022
		public Texture2D TextureNominationIcon;
	}

	// Token: 0x0200016E RID: 366
	[Serializable]
	internal class LeagueWinnerPopupPositions
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x00078E2C File Offset: 0x0007702C
		public void DrawTexture(ref Rect r, Texture2D t)
		{
			if (!t)
			{
				return;
			}
			r.Set(r.x, r.y, (float)t.width, (float)t.height);
			GUI.DrawTexture(r, t);
		}

		// Token: 0x04000BCF RID: 3023
		public GUIStyle StyleKorataki = new GUIStyle();

		// Token: 0x04000BD0 RID: 3024
		public GUIStyle StyleDYNCWhite = new GUIStyle();

		// Token: 0x04000BD1 RID: 3025
		public GUIStyle StyleDYNCBrown = new GUIStyle();

		// Token: 0x04000BD2 RID: 3026
		public GUIStyle StyleDYNCGold = new GUIStyle();

		// Token: 0x04000BD3 RID: 3027
		public Rect BackgroundRect = default(Rect);

		// Token: 0x04000BD4 RID: 3028
		public Rect MedalRect = default(Rect);

		// Token: 0x04000BD5 RID: 3029
		public Rect RankRect = default(Rect);

		// Token: 0x04000BD6 RID: 3030
		public Rect[] LabelRects = new Rect[0];

		// Token: 0x04000BD7 RID: 3031
		public Texture2D background;

		// Token: 0x04000BD8 RID: 3032
		public Texture2D medal;

		// Token: 0x04000BD9 RID: 3033
		public Texture2D rank;
	}

	// Token: 0x0200016F RID: 367
	private class LWPInfo
	{
		// Token: 0x06000A51 RID: 2641 RVA: 0x00078E74 File Offset: 0x00077074
		public LWPInfo(object args)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)args;
			dictionary.TryGetValue("season", out this.season);
			dictionary.TryGetValue("place", out this.place);
			dictionary.TryGetValue("lp", out this.lp);
			dictionary.TryGetValue("wins", out this.wins);
			dictionary.TryGetValue("loss", out this.loss);
			dictionary.TryGetValue("leav", out this.leav);
			dictionary.TryGetValue("sum", out this.sum);
			dictionary.TryGetValue("award", out this.url);
		}

		// Token: 0x04000BDA RID: 3034
		public object season;

		// Token: 0x04000BDB RID: 3035
		public object place;

		// Token: 0x04000BDC RID: 3036
		public object lp;

		// Token: 0x04000BDD RID: 3037
		public object wins;

		// Token: 0x04000BDE RID: 3038
		public object loss;

		// Token: 0x04000BDF RID: 3039
		public object leav;

		// Token: 0x04000BE0 RID: 3040
		public object sum;

		// Token: 0x04000BE1 RID: 3041
		public object url;
	}
}
