using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000179 RID: 377
[AddComponentMenu("Scripts/GUI/SearchGamesGUI")]
internal class SearchGamesGUI : Form
{
	// Token: 0x06000AAD RID: 2733 RVA: 0x00081110 File Offset: 0x0007F310
	[Obfuscation(Exclude = true)]
	private void ShowSearchGames(object obj)
	{
		this.gameModesNames[0] = Language.anyMale;
		this.Show(0.5f, 0f);
		this.BeginAssemble();
		this.canShowSortingCaution = true;
		this.sortingCautionHided = false;
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x00081144 File Offset: 0x0007F344
	[Obfuscation(Exclude = true)]
	private void HideSearchGames(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000AAF RID: 2735 RVA: 0x00081154 File Offset: 0x0007F354
	private void SetSort(GameSortOrder order)
	{
		if (this.currentSortOrder.order != order)
		{
			this.currentSortOrder.complete = false;
			this.currentSortOrder.inverse = false;
			this.currentSortOrder.order = order;
		}
		else
		{
			this.currentSortOrder.inverse = !this.currentSortOrder.inverse;
			this.currentSortOrder.complete = false;
		}
	}

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x000811C0 File Offset: 0x0007F3C0
	public override int Width
	{
		get
		{
			return this.gui.Width;
		}
	}

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x000811D0 File Offset: 0x0007F3D0
	public override int Height
	{
		get
		{
			return this.gui.Height;
		}
	}

	// Token: 0x06000AB2 RID: 2738 RVA: 0x000811E0 File Offset: 0x0007F3E0
	public override void MainInitialize()
	{
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--kg"))
		{
			this.lastParams.showFull = true;
			this.currentParams.showFull = true;
		}
		this.FillDensity = 30;
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x06000AB3 RID: 2739 RVA: 0x0008123C File Offset: 0x0007F43C
	public override void Show(float time = 0.5f, float delay = 0f)
	{
		Peer.ForceUpdateServers();
		this.reloadCounter = 0;
		this.reloadTimer = 0f;
		this.serverGUI = this.gui.GetComponent<ServerGUI>();
		this.mapNames = new string[Globals.I.maps.Length + 1];
		this.mapNames[0] = Language.anyFemale;
		for (int i = 0; i < Globals.I.maps.Length; i++)
		{
			this.mapNames[i + 1] = Globals.I.maps[i].Name;
		}
		base.Show(time, delay);
		this.gameListAssembled = false;
		SmartPingManager.Refresh();
		this.isSortedByPingAndPlayers = false;
	}

	// Token: 0x06000AB4 RID: 2740 RVA: 0x000812EC File Offset: 0x0007F4EC
	public override void Clear()
	{
	}

	// Token: 0x06000AB5 RID: 2741 RVA: 0x000812F0 File Offset: 0x0007F4F0
	public override void Register()
	{
		EventFactory.Register("ShowSearchGames", this);
		EventFactory.Register("HideSearchGames", this);
	}

	// Token: 0x06000AB6 RID: 2742 RVA: 0x00081308 File Offset: 0x0007F508
	public override void OnUpdate()
	{
		if (this.assembleOrderComplete && !this.sortingCautionHided && !this.sorting && this.ctime <= 0f)
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.SearchGamesGUI, string.Empty, Language.Sorting, PopupState.progress, false, false, "StopConnection", string.Empty));
			this.sortingCautionHided = true;
			this.canShowSortingCaution = true;
		}
		if (this.windowID != this.gui.FocusedWindow && this.sortingCautionHided)
		{
			return;
		}
		if (!this.currentSortOrder.complete && this.assembleOrderComplete)
		{
			this.SortServers();
		}
		if (!this.assembleOrderComplete && !this.gameNotFound)
		{
			this.sorting = true;
		}
		else
		{
			this.sorting = false;
		}
		if (!this.isSortedByPingAndPlayers && this.assembleOrderComplete)
		{
			this.currentSortOrder.order = GameSortOrder.ping;
			this.SortServers();
			this.currentSortOrder.order = GameSortOrder.pingPlayers;
			this.SortServers();
			this.SortByFriends(this.array);
			this.isSortedByPingAndPlayers = true;
		}
		if (this.array.Count == 0 && this.startGameSearchingTime == 0f)
		{
			this.startGameSearchingTime = Time.realtimeSinceStartup;
		}
		if (this.startGameSearchingTime != -1f && this.startGameSearchingTime + 3f < Time.realtimeSinceStartup)
		{
			if (this.array.Count != 0)
			{
				this.gameNotFound = false;
			}
			else
			{
				this.gameNotFound = true;
			}
			this.startGameSearchingTime = -1f;
		}
		if (this.startGameSearchingTime == -1f && this.array.Count == 0)
		{
			this.gameNotFound = true;
		}
		else
		{
			this.gameNotFound = false;
		}
		if (this.ctime == 0f && this.gameListAssembled)
		{
			this.ctime = Time.realtimeSinceStartup;
		}
		if (this.gameListAssembled && this.ctime != -1f && this.ctime + 2f < Time.realtimeSinceStartup)
		{
			this.isSortedByPingAndPlayers = !this.isSortedByPingAndPlayers;
			this.ctime = -1f;
		}
		if (this.lastParams.ParamsChanged(this.currentParams))
		{
			this.BeginAssemble();
			this.isSortedByPingAndPlayers = false;
		}
		if (Peer.games.Count > 0 && !this.assembleOrderComplete)
		{
			for (int i = 0; i < 14; i++)
			{
				if (!this.assembleOrderComplete)
				{
					if (this.assembleIndex >= Peer.games.Count)
					{
						this.assembleOrderComplete = true;
						this.assembleIndex = 0;
						this.gameListAssembled = true;
						break;
					}
					HostInfo hostInfo = Peer.games[this.assembleIndex];
					bool flag = true;
					if (!hostInfo.IsProtected && this.currentParams.showProtected)
					{
						flag = false;
					}
					if (!hostInfo.Hardcore && this.currentParams.showHardcore)
					{
						flag = false;
					}
					if (!hostInfo.Friends && this.currentParams.showFriends)
					{
						flag = false;
					}
					if ((!hostInfo.PasswordProtected && this.currentParams.showClan) || (hostInfo.PasswordProtected && !this.currentParams.showClan))
					{
						flag = false;
					}
					if ((!this.currentParams.showFull && hostInfo.ConnectionsCoint >= hostInfo.MaxPlayers) || (!this.currentParams.showEmpty && hostInfo.PlayerCount == 0))
					{
						flag = false;
					}
					if (this.currentParams.bshowOnlyMyLevel && (hostInfo.MinLevel > Main.UserInfo.currentLevel || hostInfo.MaxLevel < Main.UserInfo.currentLevel))
					{
						flag = false;
					}
					if ((this.currentParams.MapIndex != 0 && hostInfo.MapIndex + 1 != this.currentParams.MapIndex) || (this.currentParams.gameTypeIndex != 0 && hostInfo.GameMode + 1 != (GameMode)this.currentParams.gameTypeIndex))
					{
						flag = false;
					}
					if (flag)
					{
						hostInfo.ping = SmartPingManager.GetPing1(hostInfo.ip);
						this.array.Add(hostInfo);
					}
					this.assembleIndex++;
				}
			}
		}
	}

	// Token: 0x06000AB7 RID: 2743 RVA: 0x00081788 File Offset: 0x0007F988
	private void BeginAssemble()
	{
		this.array.Clear();
		this.assembleOrderComplete = false;
		this.currentSortOrder.complete = false;
		this.assembleIndex = 0;
	}

	// Token: 0x06000AB8 RID: 2744 RVA: 0x000817B0 File Offset: 0x0007F9B0
	private void ReassembleList()
	{
		this.array.Clear();
		this.assembleOrderComplete = false;
		this.assembleIndex = 0;
	}

	// Token: 0x06000AB9 RID: 2745 RVA: 0x000817CC File Offset: 0x0007F9CC
	private void Krutilka(Vector2 center)
	{
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		this.gui.RotateGUI(angle, new Vector2(center.x + (float)(this.krutilka.width / 2), center.y + (float)(this.krutilka.height / 2)));
		this.gui.Picture(new Vector2(center.x, center.y), this.krutilka);
		this.gui.RotateGUI(0f, Vector2.zero);
	}

	// Token: 0x06000ABA RID: 2746 RVA: 0x00081864 File Offset: 0x0007FA64
	public override void InterfaceGUI()
	{
		this.gui.BeginGroup(this.Rect, this.windowID != this.gui.FocusedWindow);
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		this.gui.Picture(new Vector2(50f, 40f), this.gui.games_bg);
		this.gui.BeginGroup(new Rect(50f, 40f, (float)this.gui.games_bg.width, (float)this.gui.games_bg.height));
		if (this.typeDropDown || this.mapDropDown || this.windowID != this.gui.FocusedWindow)
		{
			GUI.enabled = false;
		}
		if (GUI.Button(this.icolHC, "HC", this.headersStyle))
		{
			this.SetSort(GameSortOrder.hardcore);
		}
		if (GUI.Button(this.icolServerName, Language.SGServer, this.headerGameNameStyle))
		{
			this.SetSort(GameSortOrder.gameName);
		}
		if (GUI.Button(this.icolMap, Language.SGMap, this.headersStyle))
		{
			this.SetSort(GameSortOrder.map);
		}
		if (GUI.Button(this.icolPlayers, Language.SGPlayers, this.headersStyle))
		{
			this.SetSort(GameSortOrder.players);
		}
		if (GUI.Button(this.icolMode, Language.SGMode, this.headersStyle))
		{
			this.SetSort(GameSortOrder.mode);
		}
		if (GUI.Button(this.icolPing, Language.TabPing, this.headersStyle))
		{
			this.SetSort(GameSortOrder.ping);
		}
		if (GUI.Button(this.icolLevel, Language.CarrLVL, this.headersStyle))
		{
			this.SetSort(GameSortOrder.level);
		}
		if (GUI.Button(this.icolExpCoef, Language.SGRate, this.headersStyle))
		{
			this.SetSort(GameSortOrder.expCoef);
		}
		this.ScrollRect2.Set(this.ScrollRect2.x, this.ScrollRect2.y, this.ScrollRect2.width, (float)(this.FillDensity * this.array.Count));
		if (this.ScrollRect2.height < this.ScrollRect1.height)
		{
			this.ScrollRect2.Set(this.ScrollRect2.x, this.ScrollRect2.y, this.ScrollRect2.width, this.ScrollRect1.height);
		}
		this.scrollPosition = GUI.BeginScrollView(this.ScrollRect1, this.scrollPosition, this.ScrollRect2, false, false);
		if (this.scrollPosition.y < 0f)
		{
			this.scrollPosition.y = 0f;
		}
		if (this.gameListAssembled && !this.gameNotFound)
		{
			int num = (int)(this.scrollPosition.y / (float)this.FillDensity);
			while ((float)num < (this.scrollPosition.y + this.ScrollRect1.height) / (float)this.FillDensity)
			{
				if (num < this.array.Count)
				{
					this.GameListItem(num, this.array[num]);
				}
				num++;
			}
			if (this._showFriend)
			{
				this.friendNicknameStyle.alignment = TextAnchor.UpperLeft;
				if (this._index >= 0 && this._index < this.array.Count)
				{
					Helpers.HintList(this.friendsRect, this.array[this._index].FriendsList, this.friendNicknameStyle, false, -150f, 0f);
				}
				this.friendNicknameStyle.alignment = TextAnchor.MiddleLeft;
			}
		}
		else if (this.gameNotFound)
		{
			this.gui.TextLabel(new Rect(0f, 150f, (float)this.gui.games_bg.width, 22f), Language.GamelistIsNotAvailable, 18, "#FFFFFF_D", TextAnchor.MiddleCenter, true);
		}
		else
		{
			this.gui.TextLabel(new Rect(0f, 150f, (float)this.gui.games_bg.width, 22f), Language.GettingServerList, 18, "#FFFFFF_D", TextAnchor.MiddleCenter, true);
			this.Krutilka(new Vector2((float)((this.gui.games_bg.width - this.krutilka.width) / 2), 180f));
		}
		GUI.EndScrollView();
		GUI.enabled = true;
		if (CVars.IsStandaloneRealm)
		{
			this.currentParams.showProtected = this.gui.ProtectedToggle(new Vector2(30f, 25f), this.currentParams.showProtected, ref this.currentParams.bshowOnlyMyLevel, ref this.currentParams.showFull);
			this.currentParams.showFriends = this.gui.FriendsToggle(new Vector2(65f, 25f), this.currentParams.showFriends, ref this.currentParams.bshowOnlyMyLevel, ref this.currentParams.showFull);
			this.currentParams.showHardcore = this.gui.HardcoreToggle(new Vector2(100f, 25f), this.currentParams.showHardcore, ref this.currentParams.bshowOnlyMyLevel, ref this.currentParams.showFull);
			this.currentParams.showClan = this.gui.ClanToggle(new Vector2(135f, 25f), this.currentParams.showClan);
			int num2 = 175;
			this.gui.TextField(new Rect((float)num2, 25f, 200f, 50f), Language.Map.ToUpper(), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect((float)((Language.CurrentLanguage != ELanguage.RU) ? 392 : 412), 25f, 200f, 50f), Language.Mode.ToUpper(), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
			this.gui.BeginGroup(new Rect((float)((Language.CurrentLanguage != ELanguage.RU) ? (num2 + 30) : (num2 + 40)), 25f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.typeDropDown);
			this.serverGUI.DropDown<string>(new Vector2(0f, 0f), ref this.mapScrollPos, ref this.mapDropDown, this.mapNames, ref this.currentParams.MapIndex);
			this.gui.EndGroup();
			this.gui.BeginGroup(new Rect(458f, 25f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.mapDropDown);
		}
		else
		{
			this.currentParams.showFriends = this.gui.FriendsToggle(new Vector2(30f, 25f), this.currentParams.showFriends, ref this.currentParams.bshowOnlyMyLevel, ref this.currentParams.showFull);
			this.currentParams.showHardcore = this.gui.HardcoreToggle(new Vector2(65f, 25f), this.currentParams.showHardcore, ref this.currentParams.bshowOnlyMyLevel, ref this.currentParams.showFull);
			this.currentParams.showClan = this.gui.ClanToggle(new Vector2(100f, 25f), this.currentParams.showClan);
			this.gui.TextField(new Rect(145f, 25f, 200f, 50f), Language.Map.ToUpper(), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect((float)((Language.CurrentLanguage != ELanguage.RU) ? 380 : 400), 25f, 200f, 50f), Language.Mode.ToUpper(), 16, "#FFFFFF", TextAnchor.UpperLeft, false, false);
			this.gui.BeginGroup(new Rect((float)((Language.CurrentLanguage != ELanguage.RU) ? 180 : 190), 25f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.typeDropDown);
			this.serverGUI.DropDown<string>(new Vector2(0f, 0f), ref this.mapScrollPos, ref this.mapDropDown, this.mapNames, ref this.currentParams.MapIndex);
			this.gui.EndGroup();
			this.gui.BeginGroup(new Rect(450f, 25f, 220f, 300f), this.windowID != this.gui.FocusedWindow || this.mapDropDown);
		}
		this.serverGUI.DropDown<string>(new Vector2(0f, 0f), ref this.typeScrollPos, ref this.typeDropDown, this.gameModesNames, ref this.currentParams.gameTypeIndex);
		this.gui.EndGroup();
		if (Peer.PeerType == NetworkPeerType.Disconnected)
		{
			if ((this.sorting || this.ctime > 0f) && !this.gameNotFound && this.startGameSearchingTime <= 0f)
			{
				if (this.blinkUp)
				{
					this.blink += 0.05f;
					if (this.blink > 1f)
					{
						this.blinkUp = false;
					}
				}
				else
				{
					this.blink -= 0.05f;
					if (this.blink < 0f)
					{
						this.blinkUp = true;
					}
				}
				Color color = this.gui.color;
				this.gui.color = new Color(1f, 1f, 1f, this.blink);
				this.gui.TextLabel(new Rect(255f, 501f, 150f, 22f), Language.Sorting, 16, "#9d9d9d", TextAnchor.UpperRight, true);
				this.Krutilka(new Vector2(410f, 500f));
				this.gui.color = color;
				if (this.canShowSortingCaution)
				{
					EventFactory.Call("ShowPopup", new Popup(WindowsID.SearchGamesGUI, string.Empty, Language.Sorting, PopupState.progress, false, false, "StopConnection", string.Empty));
					this.canShowSortingCaution = false;
					this.sortingCautionHided = false;
				}
			}
			if (this.gui.Button(new Vector2(492f, 500f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SGConnect, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.reconnectTimer < Time.realtimeSinceStartup)
			{
				this.reconnectTimer = Time.realtimeSinceStartup + 3f;
				Peer.JoinGame(this.selectedHostInfo, false);
			}
			ButtonState buttonState;
			buttonState.Clicked = false;
			if (this.refreshTimer.Elapsed == 0f)
			{
				buttonState = this.gui.Button(new Vector2(458f, 500f), this.gui.server_window[1], this.gui.server_window[2], this.gui.server_window[17], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				this.gui.Picture(new Vector2(464f, 504f), this.gui.server_window[9]);
			}
			else
			{
				this.gui.Picture(new Vector2(458f, 500f), this.gui.server_window[1]);
				this.gui.BeginGroup(new Rect(464f, 504f, 20f, 20f));
				this.gui.color = new Color(1f, this.refreshTimer.Elapsed / 5f, this.refreshTimer.Elapsed / 5f);
				this.gui.Picture(new Vector2(0f, 0f), this.gui.server_window[9]);
				this.gui.EndGroup();
				if (this.refreshTimer.Elapsed >= 5f)
				{
					this.refreshTimer.Stop();
				}
			}
			if (buttonState.Clicked)
			{
				SmartPingManager.Refresh();
				this.isSortedByPingAndPlayers = false;
				this.refreshTimer.Start();
				Peer.ForceUpdateServers();
				this.ReassembleList();
				this.currentSortOrder.complete = false;
				this.gameListAssembled = false;
				this.ctime = 0f;
				this.startGameSearchingTime = 0f;
			}
		}
		this.currentParams.showEmpty = this.gui.CheckBox(new Vector2(23f, 500f), this.currentParams.showEmpty, null, string.Empty, 16, "#FFFFFF", TextAnchor.UpperLeft);
		this.gui.TextField(new Rect(58f, 498f, 270f, 30f), Language.SGEmpty, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.currentParams.showFull = this.gui.CheckBox(new Vector2(107f, 500f), this.currentParams.showFull, null, string.Empty, 16, "#FFFFFF", TextAnchor.UpperLeft);
		this.gui.TextField(new Rect(139f, 498f, 270f, 30f), Language.SGFull, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.currentParams.bshowOnlyMyLevel = this.gui.CheckBox(new Vector2(190f, 500f), this.currentParams.bshowOnlyMyLevel, null, string.Empty, 16, "#FFFFFF", TextAnchor.UpperLeft);
		this.gui.TextField(new Rect(224f, 498f, 270f, 30f), Language.SGLevel, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		float width = (float)CWGUI.p.closeButtonSmall.normal.background.width;
		float height = (float)CWGUI.p.closeButtonSmall.normal.background.height;
		if (GUI.Button(new Rect(656f, 25f, width, height), string.Empty, CWGUI.p.closeButtonSmall) || Input.GetKeyUp(KeyCode.Escape))
		{
			EventFactory.Call("HidePopup", new Popup(WindowsID.SearchGamesGUI, string.Empty, Language.Sorting, PopupState.progress, false, false, "StopConnection", string.Empty));
			Peer.Info = null;
			this.Hide(0.35f);
			Peer.Disconnect(true);
			EventFactory.Call("ShowInterface", null);
		}
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x06000ABB RID: 2747 RVA: 0x000827F4 File Offset: 0x000809F4
	private void GameListItem(int i, HostInfo info)
	{
		int num = this.FillDensity * i;
		Color textColor = this.gameItemStyle.normal.textColor;
		if (info == this.selectedHostInfo)
		{
			if (GUI.Button(new Rect(this.icolR.xMin, (float)num, (float)this.gui.server_window[11].width, (float)this.gui.server_window[11].height), string.Empty, (!info.Friends) ? this.gameItemJoinButton : this.gameItemJoinButtonFr))
			{
				if (Time.realtimeSinceStartup - this.gameDoubleClick < 1f && this.reconnectTimer < Time.realtimeSinceStartup)
				{
					this.reconnectTimer = Time.realtimeSinceStartup + 3f;
					Peer.JoinGame(info, false);
				}
				this.gameDoubleClick = Time.realtimeSinceStartup;
			}
		}
		else if (GUI.Toggle(new Rect(this.icolR.xMin, (float)num, (float)this.gui.server_window[11].width, (float)this.gui.server_window[11].height), this.selectedGame == i, string.Empty, (!info.Friends) ? this.gameItemButtonStyle : this.gameItemButtonStyleFr))
		{
			if (this.selectedHostInfo != info)
			{
				this.gameDoubleClick = Time.realtimeSinceStartup;
			}
			this.selectedHostInfo = info;
		}
		if (info.Friends)
		{
			GUI.DrawTexture(new Rect(this.icolF.xMin + 7f, (float)(num + (this.FillDensity - CWGUI.p.FavoriteIcon.height) / 2), (float)CWGUI.p.FavoriteIcon.width, (float)CWGUI.p.FavoriteIcon.height), CWGUI.p.FavoriteIcon);
		}
		if (CVars.IsStandaloneRealm && info.IsProtected)
		{
			GUI.DrawTexture(new Rect(this.icolHC.xMin - (float)this.gui.server_window[38].width * 1.5f, (float)(num + (this.FillDensity - this.gui.server_window[38].height) / 2), (float)this.gui.server_window[38].width, (float)this.gui.server_window[38].height), this.gui.server_window[38]);
		}
		if (info.Hardcore)
		{
			GUI.DrawTexture(new Rect(this.icolHC.xMin, (float)(num + (this.FillDensity - this.gui.server_window[6].height) / 2), (float)this.gui.server_window[6].width, (float)this.gui.server_window[6].height), this.gui.server_window[6]);
		}
		else
		{
			GUI.DrawTexture(new Rect(this.icolHC.xMin, (float)(num + (this.FillDensity - this.gui.server_window[5].height) / 2), (float)this.gui.server_window[5].width, (float)this.gui.server_window[5].height), this.gui.server_window[5]);
		}
		GUI.Label(new Rect(this.icolServerName.x, (float)((!info.Friends) ? num : (num - 7)), this.icolServerName.width, (float)this.FillDensity), info.Name, this.gameServerNameStyle);
		if (info.Friends)
		{
			string text = info.FriendNick;
			Rect rect = new Rect(this.icolServerName.x, (float)(num + 16), this.icolServerName.width, 10f);
			if (info.FriendsCount > 1)
			{
				text = string.Concat(new object[]
				{
					info.FriendNick,
					" + ",
					info.FriendsCount - 1,
					" ",
					(info.FriendsCount <= 2) ? Language.FriendsOne : Language.FriendsSeveral
				});
			}
			GUI.Label(new Rect(this.icolServerName.x, (float)(num + 6), this.icolServerName.width, (float)this.FillDensity), text, this.friendNicknameStyle);
			if (rect.Contains(Event.current.mousePosition) && info.FriendsList.Count > 1)
			{
				this._showFriend = true;
				this._index = i;
				this.friendsRect = new Rect(this.icolServerName.x, (float)(num + 16), this.icolServerName.width, 10f);
			}
		}
		if (info.MinLevel > Main.UserInfo.currentLevel || info.MaxLevel < Main.UserInfo.currentLevel)
		{
			this.gameItemStyle.normal.textColor = this.red;
		}
		GUI.Label(new Rect(this.icolLevel.x, (float)num, this.icolLevel.width, (float)this.FillDensity), info.MinLevel + "-" + info.MaxLevel, this.gameItemStyle);
		this.gameItemStyle.normal.textColor = textColor;
		GUI.Label(new Rect(this.icolMap.x, (float)num, this.icolMap.width, (float)this.FillDensity), info.MapName, this.gameItemStyle);
		float num2 = (!info.Hardcore) ? info.ExpCoef : (info.ExpCoef * CVars.g_hardcorexpCoef);
		GUI.Label(new Rect(this.icolExpCoef.x, (float)num, this.icolExpCoef.width, (float)this.FillDensity), num2.ToString(), this.gameItemStyle);
		GUI.Label(new Rect(this.icolMode.x, (float)num, this.icolMode.width, (float)this.FillDensity), info.GameMode.ToString(), this.gameItemStyle);
		this.gameItemStyle.normal.textColor = this.green;
		if (info.PlayerCount >= info.MaxPlayers)
		{
			this.gameItemStyle.normal.textColor = this.red;
		}
		GUI.Label(new Rect(this.icolPlayers.x, (float)num, this.icolPlayers.width, (float)this.FillDensity), info.PlayerCount + "/" + info.MaxPlayers, this.gameItemStyle);
		this.gameItemStyle.normal.textColor = textColor;
		if (info.Ping > 100)
		{
			this.gameItemStyle.normal.textColor = this.red;
		}
		else
		{
			this.gameItemStyle.normal.textColor = this.yellow;
		}
		if (info.Ping < 20)
		{
			this.gameItemStyle.normal.textColor = this.green;
		}
		if (info.Ping >= 999)
		{
			this.gameItemStyle.normal.textColor = this.orange;
			GUI.Label(new Rect(this.icolPing.x, (float)num, this.icolPing.width, (float)this.FillDensity), "---", this.gameItemStyle);
		}
		else
		{
			GUI.Label(new Rect(this.icolPing.x, (float)num, this.icolPing.width, (float)this.FillDensity), info.Ping.ToString(), this.gameItemStyle);
		}
		this.gameItemStyle.normal.textColor = textColor;
	}

	// Token: 0x06000ABC RID: 2748 RVA: 0x00082FD4 File Offset: 0x000811D4
	private void SortServers()
	{
		if (this.currentSortOrder.order == GameSortOrder.ranked)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user1.Ranked.CompareTo(user2.Ranked);
				}
				return user2.Ranked.CompareTo(user1.Ranked);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.friends)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user2.Friends.CompareTo(user1.Friends);
				}
				return user1.Friends.CompareTo(user2.Friends);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.hardcore)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user2.Hardcore.CompareTo(user1.Hardcore);
				}
				return user1.Hardcore.CompareTo(user2.Hardcore);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.gameName)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user2.Name.CompareTo(user1.Name);
				}
				return user1.Name.CompareTo(user2.Name);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.map)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user2.MapName.CompareTo(user1.MapName);
				}
				return user1.MapName.CompareTo(user2.MapName);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.players)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user2.PlayerCount.CompareTo(user1.PlayerCount);
				}
				return user1.PlayerCount.CompareTo(user2.PlayerCount);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.mode)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user1.GameMode.CompareTo(user2.GameMode);
				}
				return user2.GameMode.CompareTo(user1.GameMode);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.ping)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user1.Ping.CompareTo(user2.Ping);
				}
				return user2.Ping.CompareTo(user1.Ping);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.level)
		{
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				if (this.currentSortOrder.inverse)
				{
					return user1.MaxLevel.CompareTo(user2.MaxLevel);
				}
				return user2.MaxLevel.CompareTo(user1.MaxLevel);
			});
		}
		else if (this.currentSortOrder.order == GameSortOrder.expCoef)
		{
			this.array.Sort((HostInfo user1, HostInfo user2) => user2.ExpCoef.CompareTo(user1.ExpCoef));
		}
		else if (this.currentSortOrder.order == GameSortOrder.pingPlayers)
		{
			int ping;
			this.array.Sort(delegate(HostInfo user1, HostInfo user2)
			{
				ping = user1.Ping;
				if (user1.Ping == ping && user2.Ping == ping)
				{
					return user2.PlayerCount.CompareTo(user1.PlayerCount);
				}
				return -1;
			});
		}
		this.currentSortOrder.complete = true;
	}

	// Token: 0x06000ABD RID: 2749 RVA: 0x000831F0 File Offset: 0x000813F0
	private void SortByFriends(List<HostInfo> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			if (list[i].Friends)
			{
				HostInfo item = list[i];
				list.Remove(list[i]);
				list.Insert(0, item);
			}
		}
	}

	// Token: 0x04000C84 RID: 3204
	private bool canShowSortingCaution = true;

	// Token: 0x04000C85 RID: 3205
	private bool sortingCautionHided;

	// Token: 0x04000C86 RID: 3206
	public Texture2D krutilka;

	// Token: 0x04000C87 RID: 3207
	private bool gameListAssembled;

	// Token: 0x04000C88 RID: 3208
	private float blink;

	// Token: 0x04000C89 RID: 3209
	private bool blinkUp = true;

	// Token: 0x04000C8A RID: 3210
	private bool isSortedByPingAndPlayers;

	// Token: 0x04000C8B RID: 3211
	private bool sorting;

	// Token: 0x04000C8C RID: 3212
	private bool gameNotFound;

	// Token: 0x04000C8D RID: 3213
	private float ctime;

	// Token: 0x04000C8E RID: 3214
	private float startGameSearchingTime;

	// Token: 0x04000C8F RID: 3215
	private bool _showFriend;

	// Token: 0x04000C90 RID: 3216
	private int _index;

	// Token: 0x04000C91 RID: 3217
	private Rect friendsRect = default(Rect);

	// Token: 0x04000C92 RID: 3218
	private SearchGamesGUI.CurrentSortOrder currentSortOrder = new SearchGamesGUI.CurrentSortOrder();

	// Token: 0x04000C93 RID: 3219
	private bool assembleOrderComplete;

	// Token: 0x04000C94 RID: 3220
	private int assembleIndex;

	// Token: 0x04000C95 RID: 3221
	private SearchGamesGUI.AssembleParams lastParams = new SearchGamesGUI.AssembleParams();

	// Token: 0x04000C96 RID: 3222
	private SearchGamesGUI.AssembleParams currentParams = new SearchGamesGUI.AssembleParams();

	// Token: 0x04000C97 RID: 3223
	private eTimer refreshTimer = new eTimer();

	// Token: 0x04000C98 RID: 3224
	private float reconnectTimer;

	// Token: 0x04000C99 RID: 3225
	private ServerGUI serverGUI;

	// Token: 0x04000C9A RID: 3226
	private Vector2 mapScrollPos = Vector2.zero;

	// Token: 0x04000C9B RID: 3227
	private bool mapDropDown;

	// Token: 0x04000C9C RID: 3228
	private bool typeDropDown;

	// Token: 0x04000C9D RID: 3229
	private Vector2 typeScrollPos = Vector2.zero;

	// Token: 0x04000C9E RID: 3230
	private string[] gameModesNames = new string[]
	{
		Language.anyMale,
		"Deathmatch",
		"TeamElimination",
		"TargetDesignation",
		"TacticalConquest"
	};

	// Token: 0x04000C9F RID: 3231
	private string[] mapNames;

	// Token: 0x04000CA0 RID: 3232
	public GUIStyle headersStyle = new GUIStyle();

	// Token: 0x04000CA1 RID: 3233
	public GUIStyle headerGameNameStyle = new GUIStyle();

	// Token: 0x04000CA2 RID: 3234
	public GUIStyle gameItemStyle = new GUIStyle();

	// Token: 0x04000CA3 RID: 3235
	public GUIStyle gameServerNameStyle = new GUIStyle();

	// Token: 0x04000CA4 RID: 3236
	public GUIStyle gameItemButtonStyle = new GUIStyle();

	// Token: 0x04000CA5 RID: 3237
	public GUIStyle gameItemButtonStyleFr = new GUIStyle();

	// Token: 0x04000CA6 RID: 3238
	public GUIStyle gameItemJoinButton = new GUIStyle();

	// Token: 0x04000CA7 RID: 3239
	public GUIStyle gameItemJoinButtonFr = new GUIStyle();

	// Token: 0x04000CA8 RID: 3240
	public GUIStyle friendNicknameStyle = new GUIStyle();

	// Token: 0x04000CA9 RID: 3241
	public int FillDensity = 30;

	// Token: 0x04000CAA RID: 3242
	private int selectedGame = -1;

	// Token: 0x04000CAB RID: 3243
	private HostInfo selectedHostInfo;

	// Token: 0x04000CAC RID: 3244
	private float gameDoubleClick;

	// Token: 0x04000CAD RID: 3245
	public Color green = default(Color);

	// Token: 0x04000CAE RID: 3246
	public Color red = default(Color);

	// Token: 0x04000CAF RID: 3247
	public Color orange = default(Color);

	// Token: 0x04000CB0 RID: 3248
	private Color yellow = Color.yellow;

	// Token: 0x04000CB1 RID: 3249
	public Rect icolF = default(Rect);

	// Token: 0x04000CB2 RID: 3250
	public Rect icolR = default(Rect);

	// Token: 0x04000CB3 RID: 3251
	public Rect icolHC = default(Rect);

	// Token: 0x04000CB4 RID: 3252
	public Rect icolServerName = default(Rect);

	// Token: 0x04000CB5 RID: 3253
	public Rect icolLevel = default(Rect);

	// Token: 0x04000CB6 RID: 3254
	public Rect icolMap = default(Rect);

	// Token: 0x04000CB7 RID: 3255
	public Rect icolExpCoef = default(Rect);

	// Token: 0x04000CB8 RID: 3256
	public Rect icolPlayers = default(Rect);

	// Token: 0x04000CB9 RID: 3257
	public Rect icolMode = default(Rect);

	// Token: 0x04000CBA RID: 3258
	public Rect icolPing = default(Rect);

	// Token: 0x04000CBB RID: 3259
	public Rect ScrollRect1 = default(Rect);

	// Token: 0x04000CBC RID: 3260
	public Rect ScrollRect2 = default(Rect);

	// Token: 0x04000CBD RID: 3261
	private Vector2 scrollPosition = Vector2.zero;

	// Token: 0x04000CBE RID: 3262
	private List<HostInfo> array = new List<HostInfo>();

	// Token: 0x04000CBF RID: 3263
	private byte reloadCounter;

	// Token: 0x04000CC0 RID: 3264
	private float reloadTimer;

	// Token: 0x0200017A RID: 378
	private class CurrentSortOrder
	{
		// Token: 0x04000CC2 RID: 3266
		public bool complete;

		// Token: 0x04000CC3 RID: 3267
		public GameSortOrder order = GameSortOrder.players;

		// Token: 0x04000CC4 RID: 3268
		public bool inverse = true;
	}

	// Token: 0x0200017B RID: 379
	private class AssembleParams
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0008351C File Offset: 0x0008171C
		public bool ParamsChanged(SearchGamesGUI.AssembleParams currntParams)
		{
			this.changed = false;
			if (this.showProtected != currntParams.showProtected || this.showFriends != currntParams.showFriends || this.showHardcore != currntParams.showHardcore || this.showClan != currntParams.showClan || this.showFull != currntParams.showFull || this.showEmpty != currntParams.showEmpty || this.bshowOnlyMyLevel != currntParams.bshowOnlyMyLevel || this.MapIndex != currntParams.MapIndex || this.gameTypeIndex != currntParams.gameTypeIndex)
			{
				this.changed = true;
				this.showProtected = currntParams.showProtected;
				this.showFriends = currntParams.showFriends;
				this.showHardcore = currntParams.showHardcore;
				this.showClan = currntParams.showClan;
				this.showFull = currntParams.showFull;
				this.showEmpty = currntParams.showEmpty;
				this.bshowOnlyMyLevel = currntParams.bshowOnlyMyLevel;
				this.MapIndex = currntParams.MapIndex;
				this.gameTypeIndex = currntParams.gameTypeIndex;
			}
			return this.changed;
		}

		// Token: 0x04000CC5 RID: 3269
		public bool showProtected;

		// Token: 0x04000CC6 RID: 3270
		public bool showFriends;

		// Token: 0x04000CC7 RID: 3271
		public bool showHardcore;

		// Token: 0x04000CC8 RID: 3272
		public bool showClan;

		// Token: 0x04000CC9 RID: 3273
		public bool showFull;

		// Token: 0x04000CCA RID: 3274
		public bool showEmpty = true;

		// Token: 0x04000CCB RID: 3275
		public bool bshowOnlyMyLevel = true;

		// Token: 0x04000CCC RID: 3276
		public int MapIndex;

		// Token: 0x04000CCD RID: 3277
		public int gameTypeIndex;

		// Token: 0x04000CCE RID: 3278
		private bool changed;
	}
}
