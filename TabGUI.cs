using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000199 RID: 409
[AddComponentMenu("Scripts/GUI/TabGUI")]
internal class TabGUI : Form
{
	// Token: 0x06000B86 RID: 2950 RVA: 0x0008EC90 File Offset: 0x0008CE90
	[Obfuscation(Exclude = true)]
	private void ShowTab(object obj)
	{
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000B87 RID: 2951 RVA: 0x0008ECA4 File Offset: 0x0008CEA4
	[Obfuscation(Exclude = true)]
	private void HideTab(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B88 RID: 2952 RVA: 0x0008ECB4 File Offset: 0x0008CEB4
	private int SortPlayers(PlayerInfo p1, PlayerInfo p2)
	{
		if (p1.points > p2.points)
		{
			return -1;
		}
		if (p1.points < p2.points)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x06000B89 RID: 2953 RVA: 0x0008ECE0 File Offset: 0x0008CEE0
	public void GeneratePlayerList(ref List<PlayerInfo> list, PlayerListType listType)
	{
		list.Clear();
		for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
		{
			if (!EntityNetPlayer.IsClientPlayer(Peer.ClientGame.AllPlayers[i].ID))
			{
				if (listType != PlayerListType.All)
				{
					if (listType != PlayerListType.Spectators)
					{
						if (Peer.ClientGame.AllPlayers[i].IsSpectactor)
						{
							goto IL_F6;
						}
						if (listType != PlayerListType.AllPlayers)
						{
							if (!Peer.ClientGame.AllPlayers[i].IsBear && listType == PlayerListType.BearPlayers)
							{
								goto IL_F6;
							}
							if (Peer.ClientGame.AllPlayers[i].IsBear && listType == PlayerListType.UsecPlayers)
							{
								goto IL_F6;
							}
						}
					}
					else if (!Peer.ClientGame.AllPlayers[i].IsSpectactor)
					{
						goto IL_F6;
					}
				}
				list.Add(Peer.ClientGame.AllPlayers[i].playerInfo);
			}
			IL_F6:;
		}
		list.Sort(new Comparison<PlayerInfo>(this.SortPlayers));
	}

	// Token: 0x06000B8A RID: 2954 RVA: 0x0008EE10 File Offset: 0x0008D010
	private void DrawPlayer(int delta, PlayerInfo info, bool isBear = false)
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		this.gui.BeginGroup(new Rect(18f, (float)delta, (float)this.tab_window[2].width, (float)this.tab_window[2].height));
		if (info.level >= 0 && info.level < this.gui.rank_icon.Length)
		{
			this.gui.Picture(new Vector2(7f, 2f), this.gui.rank_icon[info.level]);
		}
		else if (this.wrongPlayerTimer < Time.time)
		{
			this.wrongPlayerTimer = Time.time + 20f;
			global::Console.WriteLine(info.ToString(), new Color?(Color.red));
		}
		if (info.playerType != PlayerType.spectactor)
		{
			if (info.dead)
			{
				GUI.DrawTexture(new Rect(35f, 10f, (float)this.Dead.width, (float)this.Dead.height), this.Dead);
				this.gui.color = new Color(Colors.RadarRed.r, Colors.RadarRed.g, Colors.RadarRed.b, base.visibility);
			}
			else if (info.playerClass > PlayerClass.none)
			{
				if (Main.IsTeamGame && Peer.ClientGame.LocalPlayer.IsBear != isBear)
				{
					this.gui.Picture(new Vector2(30f, 4f), this.enemyClassIcons[info.playerClass - PlayerClass.storm_trooper]);
				}
				else
				{
					this.gui.Picture(new Vector2(30f, 4f), this.classIcons[info.playerClass - PlayerClass.storm_trooper]);
				}
			}
			int num = 0;
			if (BIT.AND((int)info.buffs, 128))
			{
				this.gui.Picture(new Vector2(52f, 2f), this.vipIcons[(isBear != Peer.ClientGame.LocalPlayer.IsBear) ? 1 : 0]);
				num = this.vipIcons[0].width - 4;
			}
			float num2 = this.gui.CalcWidth(info.clanTag, this.gui.fontDNC57, 19);
			this.gui.TextField(new Rect((float)(56 + num), 0f, 300f, (float)this.tab_window[2].height), info.clanTag, 19, "#d40000", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect((float)(56 + num) + num2, 0f, 150f, (float)this.tab_window[2].height), info.Nick, 19, info.NickColor, TextAnchor.MiddleLeft, false, false);
			this.gui.color = new Color(1f, 1f, 1f, base.visibility);
			this.gui.TextLabel(new Rect(381f, 0f, 300f, (float)this.tab_window[2].height), info.points.ToString(), 19, "#ffffff", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(465f, 0f, 300f, (float)this.tab_window[2].height), info.killCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
			this.gui.TextLabel(new Rect(552f, 0f, 300f, (float)this.tab_window[2].height), info.deathCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
		}
		else
		{
			if (info.loading < 100)
			{
				this.gui.TextField(new Rect(0f, 0f, (float)this.tab_window[2].width, (float)this.tab_window[2].height), string.Concat(new object[]
				{
					Language.TabLoading,
					" (",
					info.loading,
					")"
				}), 19, "#ffffff", TextAnchor.MiddleCenter, false, false);
			}
			float num3 = this.gui.CalcWidth(info.clanTag, this.gui.fontDNC57, 19);
			this.gui.TextField(new Rect(56f, 0f, 300f, (float)this.tab_window[2].height), info.clanTag, 19, "#d40000", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(56f + num3, 0f, 300f, (float)this.tab_window[2].height), info.Nick, 19, info.NickColor, TextAnchor.MiddleLeft, false, false);
		}
		if (info.ping == -1)
		{
			info.ping = 0;
		}
		this.gui.TextField(new Rect(612f, 0f, 300f, (float)this.tab_window[2].height), info.ping, 19, "#ffffff", TextAnchor.MiddleLeft, false, false);
		if (info.IsSuspected && info.playerID != Peer.ClientGame.LocalPlayer.ID && Peer.ClientGame.LocalPlayer.UserInfo.Permission >= EPermission.Moder)
		{
			this.gui.Picture(new Vector2(625f, 2f), this.suspectIcon);
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x0008F3E8 File Offset: 0x0008D5E8
	private void DrawHoverPlayer(int delta, PlayerInfo info, bool isBear = false)
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		this.gui.BeginGroup(new Rect(18f, (float)delta, (float)this.tab_window[2].width, (float)this.tab_window[2].height));
		if (info.level >= 0 && info.level < this.gui.rank_icon.Length)
		{
			this.gui.Picture(new Vector2(7f, 2f), this.gui.rank_icon[info.level]);
		}
		else if (this.wrongPlayerTimer < Time.time)
		{
			this.wrongPlayerTimer = Time.time + 20f;
			global::Console.WriteLine(info.ToString(), new Color?(Color.red));
		}
		if (info.playerType != PlayerType.spectactor)
		{
			if (info.dead)
			{
				GUI.DrawTexture(new Rect(35f, 10f, (float)this.Dead.width, (float)this.Dead.height), this.Dead);
				this.gui.color = new Color(Colors.RadarRed.r, Colors.RadarRed.g, Colors.RadarRed.b, base.visibility);
			}
			else if (info.playerClass > PlayerClass.none)
			{
				if (Main.IsTeamGame && Peer.ClientGame.LocalPlayer.IsBear != isBear)
				{
					this.gui.Picture(new Vector2(30f, 4f), this.enemyClassIcons[info.playerClass - PlayerClass.storm_trooper]);
				}
				else
				{
					this.gui.Picture(new Vector2(30f, 4f), this.classIcons[info.playerClass - PlayerClass.storm_trooper]);
				}
			}
			int num = 0;
			if (BIT.AND((int)info.buffs, 128))
			{
				this.gui.Picture(new Vector2(52f, 2f), this.vipIcons[(isBear != Peer.ClientGame.LocalPlayer.IsBear) ? 1 : 0]);
				num = this.vipIcons[0].width - 4;
			}
			float num2 = this.gui.CalcWidth(info.clanTag, this.gui.fontDNC57, 19);
			this.gui.TextField(new Rect((float)(56 + num), 0f, 300f, (float)this.tab_window[2].height), info.clanTag, 19, "#d40000", TextAnchor.MiddleLeft, false, false);
			if (info.IsSuspected && Peer.ClientGame.LocalPlayer.UserInfo.Permission >= EPermission.Moder)
			{
				this.gui.TextField(new Rect((float)(56 + num) + num2, 0f, 300f, (float)this.tab_window[2].height), info.Nick + " " + Language.TabSuspected, 19, info.NickColor, TextAnchor.MiddleLeft, false, false);
			}
			else
			{
				this.gui.TextField(new Rect((float)(56 + num) + num2, 0f, 300f, (float)this.tab_window[2].height), info.Nick, 19, info.NickColor, TextAnchor.MiddleLeft, false, false);
			}
			this.gui.color = new Color(1f, 1f, 1f, base.visibility);
			int num3 = (int)Math.Max(Peer.ClientGame.LocalPlayer.SuspectCooldown, Peer.ClientGame.LocalPlayer.StartSuspectCooldown);
			if (num3 <= 0)
			{
				ButtonState buttonState = default(ButtonState);
				buttonState = this.gui.Button(new Vector2(630f, 5f), this.reportIcon, this.reportIconHover, null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
				if (buttonState.Clicked)
				{
					this._reportPopupData.IsShowPopup = true;
					this._reportPopupData.PopupCoords = new Vector2(690f, (float)(delta + 30));
					this._reportPopupData.Suspect = info;
				}
				else if (buttonState.Hover)
				{
					this.gui.TextLabel(new Rect(510f, 0f, 100f, (float)this.tab_window[2].height), Language.TabSuspectPlayer, 19, "#ffffff", TextAnchor.MiddleLeft, true);
				}
				else
				{
					this.gui.TextLabel(new Rect(381f, 0f, 300f, (float)this.tab_window[2].height), info.points.ToString(), 19, "#ffffff", TextAnchor.MiddleLeft, true);
					this.gui.TextLabel(new Rect(465f, 0f, 300f, (float)this.tab_window[2].height), info.killCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
					this.gui.TextLabel(new Rect(552f, 0f, 300f, (float)this.tab_window[2].height), info.deathCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
					if (info.ping == -1)
					{
						info.ping = 0;
					}
					this.gui.TextField(new Rect(612f, 0f, 300f, (float)this.tab_window[2].height), info.ping, 19, "#ffffff", TextAnchor.MiddleLeft, false, false);
				}
			}
			else if (this.gui.HoverPicture(new Vector2(630f, 5f), this.reportIcon, this.reportIcon))
			{
				this.gui.TextLabel(new Rect(510f, 0f, 285f, (float)this.tab_window[2].height), string.Concat(new object[]
				{
					Language.TabSuspectPlayer,
					" (",
					num3,
					")"
				}), 19, "#777777", TextAnchor.MiddleLeft, true);
			}
			else
			{
				this.gui.TextLabel(new Rect(381f, 0f, 300f, (float)this.tab_window[2].height), info.points.ToString(), 19, "#ffffff", TextAnchor.MiddleLeft, true);
				this.gui.TextLabel(new Rect(465f, 0f, 300f, (float)this.tab_window[2].height), info.killCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
				this.gui.TextLabel(new Rect(552f, 0f, 300f, (float)this.tab_window[2].height), info.deathCount, 19, "#ffffff", TextAnchor.MiddleLeft, true);
				if (info.ping == -1)
				{
					info.ping = 0;
				}
				this.gui.TextField(new Rect(612f, 0f, 100f, (float)this.tab_window[2].height), info.ping, 19, "#ffffff", TextAnchor.MiddleLeft, false, false);
			}
		}
		else
		{
			if (info.loading < 100)
			{
				this.gui.TextField(new Rect(0f, 0f, (float)this.tab_window[2].width, (float)this.tab_window[2].height), string.Concat(new object[]
				{
					Language.TabLoading,
					" (",
					info.loading,
					")"
				}), 19, "#ffffff", TextAnchor.MiddleCenter, false, false);
			}
			float num4 = this.gui.CalcWidth(info.clanTag, this.gui.fontDNC57, 19);
			this.gui.TextField(new Rect(56f, 0f, 300f, (float)this.tab_window[2].height), info.clanTag, 19, "#d40000", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(56f + num4, 0f, 300f, (float)this.tab_window[2].height), info.Nick, 19, info.NickColor, TextAnchor.MiddleLeft, false, false);
			if (info.ping == -1)
			{
				info.ping = 0;
			}
			this.gui.TextField(new Rect(612f, 0f, 100f, (float)this.tab_window[2].height), info.ping, 19, "#ffffff", TextAnchor.MiddleLeft, false, false);
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x0008FCE8 File Offset: 0x0008DEE8
	public override void MainInitialize()
	{
		this.list = new List<PlayerInfo>();
		this.isGameHandler = true;
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000B8D RID: 2957 RVA: 0x0008FD0C File Offset: 0x0008DF0C
	public override void Register()
	{
		EventFactory.Register("ShowTab", this);
		EventFactory.Register("HideTab", this);
	}

	// Token: 0x06000B8E RID: 2958 RVA: 0x0008FD24 File Offset: 0x0008DF24
	public override void Clear()
	{
		this.scrollPos = Vector2.zero;
		this.delta = 0;
	}

	// Token: 0x06000B8F RID: 2959 RVA: 0x0008FD38 File Offset: 0x0008DF38
	public override void GameGUI()
	{
		if (this.iterations > 30)
		{
			this.iterations = 0;
			this.GeneratePlayerList(ref this.list, PlayerListType.All);
			for (int i = 0; i < this.list.Count; i++)
			{
				if (this.list[i].playerID == Peer.ClientGame.LocalPlayer.playerInfo.playerID)
				{
					this.cachedMatchPlace = i + 1;
					this.myExp = this.list[i].points;
				}
			}
			if (this.cachedMatchPlace == 0)
			{
				this.cachedMatchPlace = this.list.Count;
			}
			if (this.list.Count == 0)
			{
				return;
			}
			this.currentLeader = this.list[0];
			if (this.lastLeader.Nick == "unknown")
			{
				this.lastLeader = this.list[0];
			}
			if (this.lastLeader.Nick != "unknown" && this.lastLeader.playerID != this.currentLeader.playerID)
			{
				this.lastLeader = this.currentLeader;
				if (this.currentLeader.points > 0 && !Main.IsShowingMatchResult)
				{
					EventFactory.Call("ChatMessage", new object[]
					{
						ChatInfo.gameflow_message,
						this.currentLeader.Nick,
						Language.TabLeadingOnPoints
					});
				}
			}
		}
		else
		{
			this.iterations++;
		}
		if (SingletoneForm<global::Console>.Instance.Visible || LoadingGUI.I.Visible)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			this.Show(0.1f, 0f);
		}
		if (Input.GetKeyUp(KeyCode.Tab))
		{
			this.Hide(0.1f);
		}
		if (!base.Visible)
		{
			this.IsMouseClicked = false;
			this._reportPopupData.IsShowPopup = false;
			return;
		}
		if (Input.GetMouseButton(0))
		{
			this.IsMouseClicked = true;
		}
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.BeginGroup(rect, false);
		this.gui.Picture(new Vector2(50f, 15f), this.tab_window[0]);
		this.gui.BeginGroup(new Rect(50f, 15f, (float)this.tab_window[0].width, (float)this.tab_window[0].height));
		this.gui.Picture(new Vector2(19f, 20f), this.tab_window[1]);
		this.gui.TextField(new Rect(30f, 20f, 270f, 30f), Main.HostInfo.Name, 11, "#c9c9c9_Micra", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(30f, 20f, 670f, 30f), EnumNames.GameModeName(Peer.Info.GameMode), 16, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(new Rect(10f, 20f, 670f, 30f), this.gui.SecondsToStringMS((int)Peer.ClientGame.ElapsedNextEventTime), 11, "#c9c9c9_Micra", TextAnchor.MiddleRight, false, false);
		if (this.scrollPos.y < (float)(this.delta - this.gui.Height) && this.delta > this.gui.Height)
		{
			this.gui.Picture(new Vector2(342f, 523f), this.tab_window[4]);
		}
		if (Event.current.type == EventType.ScrollWheel)
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				this.scrollPos.y = this.scrollPos.y + Time.deltaTime * 3000f;
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				this.scrollPos.y = this.scrollPos.y - Time.deltaTime * 3000f;
			}
		}
		Texture2D background = GUI.skin.verticalScrollbarThumb.normal.background;
		GUI.skin.verticalScrollbarThumb.normal.background = null;
		this.scrollPos = this.gui.BeginScrollView(new Rect(0f, 50f, (float)this.tab_window[0].width, (float)this.tab_window[0].height * 0.8f), this.scrollPos, new Rect(0f, 0f, (float)this.tab_window[0].width * 0.9f, (float)this.delta), float.MaxValue);
		GUI.skin.verticalScrollbarThumb.normal.background = background;
		this.delta = 0;
		if (Main.IsTeamGame)
		{
			this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 600f));
			this.gui.Picture(new Vector2(18f, 18f), this.bearBar);
			this.gui.Picture(new Vector2(22f, 3f), this.bearLogo);
			this.gui.TextField(new Rect(568f, 18f, 100f, 50f), Peer.ClientGame.BearWinCount, 20, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
			this.gui.EndGroup();
			this.delta += 48;
			this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 100f));
			this.Headers();
			this.gui.EndGroup();
			this.delta += 27;
			this.DrawPlayersList(PlayerListType.BearPlayers);
			this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 600f));
			this.gui.Picture(new Vector2(18f, 18f), this.usecBar);
			this.gui.Picture(new Vector2(18f, 3f), this.usecLogo);
			this.gui.TextField(new Rect(568f, 18f, 100f, 50f), Peer.ClientGame.UsecWinCount, 20, "#ffffff_Micra", TextAnchor.UpperRight, false, false);
			this.gui.EndGroup();
			this.delta += 48;
			this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 100f));
			this.Headers();
			this.gui.EndGroup();
			this.delta += 27;
			this.DrawPlayersList(PlayerListType.UsecPlayers);
		}
		else
		{
			this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 100f));
			this.Headers();
			this.gui.EndGroup();
			this.delta += 27;
			this.DrawPlayersList(PlayerListType.AllPlayers);
		}
		this.gui.BeginGroup(new Rect(0f, (float)this.delta, 800f, 600f));
		this.gui.Picture(new Vector2(18f, 18f), this.spectactorBar);
		this.gui.TextField(new Rect(6f, 18f, (float)(this.spectactorBar.width / 2), (float)this.spectactorBar.height), Language.Spectators, 14, "#ffffff_Micra", TextAnchor.MiddleCenter, false, false);
		this.gui.EndGroup();
		this.delta += 48;
		this.DrawPlayersList(PlayerListType.Spectators);
		if (this._reportPopupData.IsShowPopup)
		{
			this.DrawReportPopup();
		}
		this.gui.EndScrollView();
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x06000B90 RID: 2960 RVA: 0x00090600 File Offset: 0x0008E800
	private void DrawReportPopup()
	{
		int num = 130;
		int num2 = 30;
		if (Main.UserInfo.repa >= (float)CVars.MinReportReputation)
		{
			num = 197;
			num2 = 77;
		}
		this.gui.BeginGroup(new Rect(this._reportPopupData.PopupCoords.x - (float)num, this._reportPopupData.PopupCoords.y, (float)num, (float)num2));
		this.gui.color = new Color(1f, 1f, 1f, 0.8f);
		this.gui.PictureSized(Vector2.zero, this.gui.black, new Vector2((float)num, (float)num2));
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if (Main.UserInfo.repa >= (float)CVars.MinReportReputation)
		{
			if (this.gui.Button(new Vector2(2f, 2f), this.reportButton, this.gui.mainMenuButtons[1], null, Language.TabSuspectCheat, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Peer.ClientGame.LocalPlayer.Suspect(this._reportPopupData.Suspect.userID, ReportType.Cheating);
				this._reportPopupData.IsShowPopup = false;
			}
			if (this.gui.Button(new Vector2(2f, 28f), this.reportButton, this.gui.mainMenuButtons[1], null, Language.TabSuspectBugUse, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Peer.ClientGame.LocalPlayer.Suspect(this._reportPopupData.Suspect.userID, ReportType.BugUsage);
				this._reportPopupData.IsShowPopup = false;
			}
			if (this.gui.Button(new Vector2(2f, 53f), this.reportButton, this.gui.mainMenuButtons[1], null, Language.TabSuspectAbuse, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Peer.ClientGame.LocalPlayer.Suspect(this._reportPopupData.Suspect.userID, ReportType.Abuse);
				this._reportPopupData.IsShowPopup = false;
			}
		}
		else
		{
			this.gui.TextLabel(new Rect(5f, 0f, (float)num, (float)num2), Language.TabNeedReputation, 9, "#FFFFFF_Tahoma", TextAnchor.MiddleLeft, true);
		}
		if (Input.GetMouseButton(0) && !this.gui.inRect(new Rect(0f, 0f, (float)num, (float)num2), this.gui.upper, this.gui.cursorPosition))
		{
			this._reportPopupData.IsShowPopup = false;
		}
		this.gui.EndGroup();
	}

	// Token: 0x06000B91 RID: 2961 RVA: 0x00090934 File Offset: 0x0008EB34
	private void DrawPlayersList(PlayerListType listType)
	{
		this.GeneratePlayerList(ref this.list, listType);
		foreach (PlayerInfo playerInfo in this.list)
		{
			bool flag = false;
			if (listType == PlayerListType.Spectators)
			{
				if (playerInfo.isModerator && playerInfo.playerType == PlayerType.spectactor)
				{
					continue;
				}
				if (playerInfo.isModerator && Main.UserInfo.Permission < EPermission.Admin && playerInfo.userID != Main.UserInfo.userID && playerInfo.playerType == PlayerType.spectactor)
				{
					continue;
				}
			}
			if (!EntityNetPlayer.IsClientPlayer(playerInfo.playerID))
			{
				if (playerInfo.isHost)
				{
					if (!this._reportPopupData.IsShowPopup)
					{
						flag = this.gui.HoverPicture(new Vector2(18f, (float)this.delta), this.tab_window[5], this.tab_window[6]);
					}
					else if (this._reportPopupData.Suspect != null && this._reportPopupData.Suspect.playerID == playerInfo.playerID)
					{
						this.gui.Picture(new Vector2(18f, (float)this.delta), this.tab_window[6]);
					}
				}
				if (playerInfo.playerID == Peer.ClientGame.LocalPlayer.ID)
				{
					this.gui.Picture(new Vector2(18f, (float)this.delta), this.tab_window[3]);
				}
				else if (!this._reportPopupData.IsShowPopup)
				{
					flag = this.gui.HoverPicture(new Vector2(18f, (float)this.delta), this.tab_window[2], this.tab_window[6]);
				}
				else if (this._reportPopupData.Suspect != null && this._reportPopupData.Suspect.playerID == playerInfo.playerID)
				{
					this.gui.Picture(new Vector2(18f, (float)this.delta), this.tab_window[6]);
				}
				if ((flag && !this._reportPopupData.IsShowPopup) || (this._reportPopupData.Suspect != null && this._reportPopupData.IsShowPopup && this._reportPopupData.Suspect.playerID == playerInfo.playerID))
				{
					this.DrawHoverPlayer(this.delta, playerInfo, true);
				}
				else if (listType == PlayerListType.BearPlayers)
				{
					this.DrawPlayer(this.delta, playerInfo, true);
				}
				else
				{
					this.DrawPlayer(this.delta, playerInfo, false);
				}
				this.delta += 42;
			}
		}
	}

	// Token: 0x06000B92 RID: 2962 RVA: 0x00090C60 File Offset: 0x0008EE60
	private void Headers()
	{
		this.gui.TextField(new Rect(75f, 2f, 70f, 25f), Language.CarrItemName, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(400f, 2f, 70f, 25f), Language.CarrPoints, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(470f, 2f, 70f, 25f), Language.CarrKills, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(560f, 2f, 70f, 25f), Language.CarrDeath, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
		this.gui.TextField(new Rect(630f, 2f, 70f, 25f), Language.TabPing, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
	}

	// Token: 0x06000B93 RID: 2963 RVA: 0x00090D74 File Offset: 0x0008EF74
	public override void OnSpawn()
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B94 RID: 2964 RVA: 0x00090D84 File Offset: 0x0008EF84
	public override void OnDie()
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B95 RID: 2965 RVA: 0x00090D94 File Offset: 0x0008EF94
	public override void OnRoundEnd()
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B96 RID: 2966 RVA: 0x00090DA4 File Offset: 0x0008EFA4
	public override void OnRoundStart()
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B97 RID: 2967 RVA: 0x00090DB4 File Offset: 0x0008EFB4
	public override void OnMatchStart()
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000B98 RID: 2968 RVA: 0x00090DC4 File Offset: 0x0008EFC4
	public override void OnMatchEnd()
	{
		this.Hide(0.35f);
	}

	// Token: 0x04000D62 RID: 3426
	public Texture2D bearLogo;

	// Token: 0x04000D63 RID: 3427
	public Texture2D bearBar;

	// Token: 0x04000D64 RID: 3428
	public Texture2D usecLogo;

	// Token: 0x04000D65 RID: 3429
	public Texture2D usecBar;

	// Token: 0x04000D66 RID: 3430
	public Texture2D spectactorBar;

	// Token: 0x04000D67 RID: 3431
	public Texture2D Dead;

	// Token: 0x04000D68 RID: 3432
	public Texture2D[] tab_window;

	// Token: 0x04000D69 RID: 3433
	public Texture2D[] vipIcons;

	// Token: 0x04000D6A RID: 3434
	public Texture2D[] classIcons;

	// Token: 0x04000D6B RID: 3435
	public Texture2D[] enemyClassIcons;

	// Token: 0x04000D6C RID: 3436
	public Texture2D reportIcon;

	// Token: 0x04000D6D RID: 3437
	public Texture2D reportIconHover;

	// Token: 0x04000D6E RID: 3438
	public Texture2D suspectIcon;

	// Token: 0x04000D6F RID: 3439
	public Texture2D reportButton;

	// Token: 0x04000D70 RID: 3440
	public int cachedMatchPlace;

	// Token: 0x04000D71 RID: 3441
	public bool IsMouseClicked;

	// Token: 0x04000D72 RID: 3442
	private TabGUI.ReportPopupData _reportPopupData = new TabGUI.ReportPopupData();

	// Token: 0x04000D73 RID: 3443
	public List<PlayerInfo> list = new List<PlayerInfo>();

	// Token: 0x04000D74 RID: 3444
	private int iterations;

	// Token: 0x04000D75 RID: 3445
	public PlayerInfo currentLeader;

	// Token: 0x04000D76 RID: 3446
	public PlayerInfo lastLeader;

	// Token: 0x04000D77 RID: 3447
	public int myExp;

	// Token: 0x04000D78 RID: 3448
	private Vector2 scrollPos = Vector2.zero;

	// Token: 0x04000D79 RID: 3449
	private int delta;

	// Token: 0x04000D7A RID: 3450
	private float wrongPlayerTimer;

	// Token: 0x0200019A RID: 410
	private class ReportPopupData
	{
		// Token: 0x04000D7B RID: 3451
		public bool IsShowPopup;

		// Token: 0x04000D7C RID: 3452
		public Vector2 PopupCoords = Vector2.zero;

		// Token: 0x04000D7D RID: 3453
		public PlayerInfo Suspect;
	}
}
