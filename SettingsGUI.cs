using System;
using System.Collections.Generic;
using System.Reflection;
using ClanSystemGUI;
using CWSARequests;
using UnityEngine;

// Token: 0x0200017F RID: 383
[AddComponentMenu("Scripts/GUI/SettingsGUI")]
internal class SettingsGUI : Form
{
	// Token: 0x06000AED RID: 2797 RVA: 0x000850D4 File Offset: 0x000832D4
	[Obfuscation(Exclude = true)]
	private void ShowSettings(object obj)
	{
		this.nick = Main.UserInfo.nick;
		this.prevNick = this.nick;
		this.nickTimer.Start();
		this.controller.reset();
		this.Show(0.5f, 0f);
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x00085124 File Offset: 0x00083324
	[Obfuscation(Exclude = true)]
	private void HideSettings(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x00085134 File Offset: 0x00083334
	[Obfuscation(Exclude = true)]
	private void NickAvailable(object obj)
	{
		this.nickAvailable = (bool)obj;
		if (this.nickAvailable)
		{
			this.nickError = Language.SettingsNickAllow;
		}
		else
		{
			this.nickError = Language.SettingsNickNotAllow;
		}
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00085174 File Offset: 0x00083374
	private void SimpleKey(Vector3 pos, string text, ref KeyCode key)
	{
		if (this.whereKey == this.indexKey)
		{
			this.gui.Button(pos, this.gui.server_window[13], this.gui.server_window[13], this.gui.server_window[13], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			KeyCode? key2 = UserBinds.getKey();
			if (key2 != null)
			{
				if (key2.Value == KeyCode.Escape)
				{
					this.whereKey = -1f;
					return;
				}
				key = key2.Value;
				this.whereKey = -1f;
				return;
			}
		}
		else if (this.gui.Button(pos, this.gui.server_window[11], this.gui.server_window[12], this.gui.server_window[13], string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked && this.gui.isDoubleClick)
		{
			this.whereKey = this.indexKey;
		}
		this.gui.BeginGroup(new Rect(pos.x, pos.y, (float)this.gui.server_window[11].width, (float)this.gui.server_window[11].height));
		this.gui.TextField(new Rect(25f, 0f, 300f, (float)this.gui.server_window[11].height), text, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.TextField(new Rect(285f, 0f, 300f, (float)this.gui.server_window[11].height), key.ToString().Replace("Alpha", string.Empty), 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
		this.gui.EndGroup();
		this.indexKey += 1f;
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x000853B8 File Offset: 0x000835B8
	public void ResolutionDropDown(Vector2 pos)
	{
		if (this.gui.Button(pos, this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Main.UserInfo.settings.resolution.width + "x" + Main.UserInfo.settings.resolution.height, 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.resDropDown = !this.resDropDown;
		}
		if (!this.resDropDown)
		{
			this.gui.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.gui.settings_window[2]);
		}
		if (this.resDropDown)
		{
			this.gui.Picture(new Vector2(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height), this.gui.settings_window[4]);
			List<Resolution> list = new List<Resolution>();
			for (int i = 0; i < Screen.resolutions.Length; i++)
			{
				if (Screen.resolutions[i].width >= 800 && Screen.resolutions[i].width <= 1920)
				{
					list.Add(Screen.resolutions[i]);
				}
			}
			if (this.resScrollPos.y > (float)(list.Count * (this.gui.mainMenuButtons[0].height + 5) - 112))
			{
				this.resScrollPos.y = (float)(list.Count * (this.gui.mainMenuButtons[0].height + 5) - 112);
			}
			this.gui.BeginGroup(new Rect(0f, 0f, 4096f, 4096f), !this.resDropDown);
			if (list.Count > 4)
			{
				this.gui.Picture(new Vector2(pos.x + 200f, pos.y + (float)this.gui.mainMenuButtons[0].height + 3f), this.gui.settings_window[7]);
				this.resScrollPos = this.gui.BeginScrollView(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.resScrollPos, new Rect(0f, 0f, (float)(this.gui.settings_window[4].width - 25), (float)(list.Count * (this.gui.mainMenuButtons[0].height + 5) + 40)), float.MaxValue);
			}
			else
			{
				this.gui.BeginGroup(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)));
			}
			for (int j = 0; j < list.Count; j++)
			{
				if (this.gui.Button(new Vector2(4f, (float)(0 + (this.gui.mainMenuButtons[0].height + 5) * j + 3)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], list[j].width + "x" + list[j].height, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					this.resDropDown = false;
					Main.UserInfo.settings.resolution = list[j];
				}
			}
			if (list.Count > 4)
			{
				this.gui.EndScrollView();
			}
			else
			{
				this.gui.EndGroup();
			}
			this.gui.EndGroup();
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.gui.upper, this.gui.cursorPosition) && this.resDropDown && Input.GetMouseButtonDown(0))
			{
				this.resDropDown = false;
			}
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x00085920 File Offset: 0x00083B20
	public ELanguage LanguageDropDown(Vector2 pos, ELanguage language, ref bool dropDown)
	{
		if (this.gui.Button(pos, this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.CurrentLanguageStr, 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			dropDown = !dropDown;
		}
		if (!dropDown)
		{
			this.gui.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.gui.settings_window[2]);
		}
		if (dropDown)
		{
			this.gui.Picture(new Vector2(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height), this.gui.settings_window[10]);
			this.gui.BeginGroup(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)));
			if (this.gui.Button(new Vector2(4f, 3f), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.RusLanguage, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				language = ELanguage.RU;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + this.gui.mainMenuButtons[1].height + 5)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.EngLanguage, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				language = ELanguage.EN;
			}
			this.gui.EndGroup();
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.gui.upper, this.gui.cursorPosition))
			{
				if (dropDown && Input.GetMouseButtonDown(0))
				{
					dropDown = false;
				}
			}
		}
		return language;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00085C1C File Offset: 0x00083E1C
	public SimpleQuality SimpleQualityDropDown(Vector2 pos, SimpleQuality quality, ref bool dropDown)
	{
		if (this.gui.Button(pos, this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], UserGraphics.SimpleQualityString(quality), 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			dropDown = !dropDown;
		}
		if (!dropDown)
		{
			this.gui.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.gui.settings_window[2]);
		}
		if (dropDown)
		{
			this.gui.Picture(new Vector2(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height), this.gui.settings_window[4]);
			this.gui.BeginGroup(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)));
			if (this.gui.Button(new Vector2(4f, 3f), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsHigh, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = SimpleQuality.high;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + this.gui.mainMenuButtons[1].height + 5)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsMiddle, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = SimpleQuality.average;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + (this.gui.mainMenuButtons[1].height + 5) * 2)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsLow, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = SimpleQuality.low;
			}
			this.gui.EndGroup();
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.gui.upper, this.gui.cursorPosition))
			{
				if (dropDown && Input.GetMouseButtonDown(0))
				{
					dropDown = false;
				}
			}
		}
		return quality;
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00085F98 File Offset: 0x00084198
	public Quality QualityDropDown(Vector2 pos, Quality quality, ref bool dropDown)
	{
		if (this.gui.Button(pos, this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], UserGraphics.QualityString(quality), 17, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			dropDown = !dropDown;
		}
		if (!dropDown)
		{
			this.gui.Picture(new Vector2(pos.x + 165f, pos.y + 8f), this.gui.settings_window[2]);
		}
		if (dropDown)
		{
			this.gui.Picture(new Vector2(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height), this.gui.settings_window[4]);
			this.gui.BeginGroup(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)));
			if (this.gui.Button(new Vector2(4f, 3f), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsMax, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = Quality.max;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + this.gui.mainMenuButtons[1].height + 5)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsHigh, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = Quality.high;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + (this.gui.mainMenuButtons[1].height + 5) * 2)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsMiddle, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = Quality.average;
			}
			if (this.gui.Button(new Vector2(4f, (float)(3 + (this.gui.mainMenuButtons[1].height + 5) * 3)), null, this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsLow, 17, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				dropDown = false;
				quality = Quality.low;
			}
			this.gui.EndGroup();
			if (!this.gui.inRect(new Rect(pos.x - 5f, pos.y + (float)this.gui.mainMenuButtons[0].height + 4f, (float)(this.gui.settings_window[4].width - 5), (float)(this.gui.settings_window[4].height - 8)), this.gui.upper, this.gui.cursorPosition))
			{
				if (dropDown && Input.GetMouseButtonDown(0))
				{
					dropDown = false;
				}
			}
		}
		return quality;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00086394 File Offset: 0x00084594
	protected override void Awake()
	{
		base.Awake();
		this.controller = NetworkSettingsController.Instance;
	}

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x000863A8 File Offset: 0x000845A8
	public override Rect Rect
	{
		get
		{
			return new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		}
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x000863C8 File Offset: 0x000845C8
	public override void MainInitialize()
	{
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000AF8 RID: 2808 RVA: 0x000863D8 File Offset: 0x000845D8
	public override void Clear()
	{
		this.textureDropDown = false;
		this.shadowDropDown = false;
		this.resScrollPos = Vector2.zero;
		this.resDropDown = false;
		this.indexKey = 0f;
		this.whereKey = -1f;
		this.levelSlider = -1f;
		this.controlsScroll = Vector2.zero;
		this.settingsState = SettingsState.GAME;
	}

	// Token: 0x06000AF9 RID: 2809 RVA: 0x00086438 File Offset: 0x00084638
	public override void Register()
	{
		EventFactory.Register("ShowSettings", this);
		EventFactory.Register("HideSettings", this);
		EventFactory.Register("NickAvailable", this);
	}

	// Token: 0x06000AFA RID: 2810 RVA: 0x0008645C File Offset: 0x0008465C
	public override void InterfaceGUI()
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow);
		this.gui.Picture(new Vector2(50f, 40f), this.gui.games_bg);
		this.gui.BeginGroup(new Rect(50f, 40f, (float)this.gui.games_bg.width, (float)this.gui.games_bg.height), this.resDropDown);
		if (this.controller.ReceivingCanceled)
		{
			base.StopAllCoroutines();
			this.controller.ReceivingCanceled = false;
			this.settingsState = ((this.settingsState != SettingsState.NETWORK) ? this.settingsState : SettingsState.GAME);
		}
		if (this.settingsState == SettingsState.GAME)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(23f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsGame, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(23f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsGame, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.cancelReceive();
			this.settingsState = SettingsState.GAME;
		}
		if (this.settingsState == SettingsState.VIDEO_AUDIO)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(112f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsVideoAudio, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(112f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsVideoAudio, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.cancelReceive();
			this.settingsState = SettingsState.VIDEO_AUDIO;
		}
		if (this.settingsState == SettingsState.CONTROLS)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(201f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsControl, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		else if (this.gui.Button(new Vector2(201f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsControl, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.cancelReceive();
			this.settingsState = SettingsState.CONTROLS;
		}
		if (this.settingsState == SettingsState.NETWORK)
		{
			this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(290f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsNetwork, 15, "#FFFFFF", TextAnchor.MiddleCenter);
		}
		if (!CVars.IsVanilla && (!string.IsNullOrEmpty(Main.UserInfo.HopsSecretKey) || !string.IsNullOrEmpty(Main.UserInfo.HopsRoulettePrizeKey)))
		{
			if (this.settingsState == SettingsState.BONUSES)
			{
				this.gui.ButtonSelected(new ButtonState(false, false, true), new Vector2(379f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsBonuses, 15, "#FFFFFF", TextAnchor.MiddleCenter);
			}
			else if (this.gui.Button(new Vector2(379f, 23f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Language.SettingsBonuses, 15, "#FFFFFF", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.cancelReceive();
				this.settingsState = SettingsState.BONUSES;
			}
		}
		if (this.gui.Button(new Vector2(295f, 500f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.OK, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.cancelReceive();
			this.settingsState = SettingsState.GAME;
			this.SaveSettings();
			this.Hide(0.35f);
			if (Screen.fullScreen)
			{
				Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, Screen.fullScreen);
			}
			else
			{
				Utility.SetResolution(800, 600, Screen.fullScreen);
			}
		}
		if (this.gui.Button(new Vector2(492f, 500f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsApply, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.cancelReceive();
			this.SaveSettings();
			if (Screen.fullScreen)
			{
				Utility.SetResolution(Main.UserInfo.settings.resolution.width, Main.UserInfo.settings.resolution.height, Screen.fullScreen);
			}
			else
			{
				Utility.SetResolution(800, 600, Screen.fullScreen);
			}
		}
		if (this.settingsState == SettingsState.GAME)
		{
			if (Main.UserInfo.clanTag != string.Empty)
			{
				this.gui.Picture(new Vector2(70f, 96f), this.clanTag_back);
				this.gui.TextField(new Rect(-42f, 65f, 300f, 50f), Language.SettingsClan, 18, "#9d9d9d", TextAnchor.UpperCenter, false, false);
				this.gui.TextField(new Rect(-42f, 95f, (float)this.gui.settings_window[0].width, (float)this.gui.settings_window[0].height), Main.UserInfo.clanTag, 24, "#d40000", TextAnchor.MiddleCenter, false, false);
			}
			if (Main.UserInfo.nickChange == 0)
			{
				this.gui.TextField(new Rect(160f, 95f, (float)this.gui.settings_window[0].width, (float)this.gui.settings_window[0].height), this.nick, 24, Main.UserInfo.nickColor, TextAnchor.MiddleCenter, false, false);
				this.gui.Button(new Vector2(160f, 95f), this.gui.settings_window[0], this.gui.settings_window[0], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			}
			else
			{
				this.nick = this.gui.TextField(new Rect(160f, 95f, (float)this.gui.settings_window[0].width, (float)this.gui.settings_window[0].height), Helpers.FixedNickname(this.nick, Main.UserInfo.nick), 24, Main.UserInfo.nickColor, TextAnchor.MiddleCenter, true, true);
				this.gui.Button(new Vector2(160f, 95f), this.gui.settings_window[0], this.gui.settings_window[1], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null);
			}
			this.SettingNickColorZone();
			if (this.gui.Button(new Vector2(480f, 94f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Globals.I.buyNicknameChangePrice + "  .", 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.clickSoundPrefab).Clicked && !Main.IsGameLoaded)
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.SettingsBuyChange, string.Empty, PopupState.buyNickCnage, false, true, string.Empty, string.Empty));
			}
			this.gui.Picture(new Vector2((float)(480 + this.gui.server_window[14].width / 2 + 6), 97f), this.gui.gldIcon);
			if (this.nick == Main.UserInfo.nick)
			{
				this.nickTimer.Start();
				this.nickError = string.Empty;
			}
			else if (this.prevNick != this.nick)
			{
				this.nickTimer.Start();
				this.nickError = Language.SettingsNickCheck;
			}
			else if (this.nick.Length < 4)
			{
				this.nickError = Language.SettingsNickMaxLenght;
			}
			else if (this.nickTimer.Elapsed > 1f)
			{
				this.nickTimer.Stop();
				Main.AddDatabaseRequest<CheckNick>(new object[]
				{
					this.nick
				});
			}
			this.prevNick = this.nick;
			this.gui.TextLabel(new Rect(210f, 65f, 300f, 50f), Language.SettingsYourNickUsedInGame, 18, "#9d9d9d", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect(480f, 65f, 300f, 50f), Language.SettingsBuyNickChange, 18, "#9d9d9d", TextAnchor.UpperLeft, true);
			this.gui.TextLabel(new Rect(160f, 125f, 200f, 50f), Language.SettingsYouAllowToChangeNick, 15, "#9d9d9d", TextAnchor.UpperRight, true);
			this.gui.TextLabel(new Rect(370f, 125f, 100f, 50f), Main.UserInfo.nickChange + Language.SettingsTimes, 15, Colors.RadarRedWeb, TextAnchor.UpperLeft, true);
			string textColor = Colors.RadarRedWeb;
			if (this.nickError == Language.SettingsNickAllow)
			{
				textColor = Colors.RadarGreenWeb;
			}
			if (this.nickError == Language.SettingsNickCheck)
			{
				textColor = "#FFFFFF";
				float angle = 180f * Time.realtimeSinceStartup * 1.5f;
				Vector2 vector = new Vector2(581f, 96f);
				this.gui.RotateGUI(angle, new Vector2(vector.x + (float)(this.progress_small.width / 2), vector.y + (float)(this.progress_small.height / 2) + 30f));
				this.gui.Picture(new Vector2(vector.x, vector.y + 30f), this.progress_small);
				this.gui.RotateGUI(0f, Vector2.zero);
			}
			this.gui.TextField(new Rect(474f, 165f, 300f, 50f), this.nickError, 15, textColor, TextAnchor.UpperLeft, false, false);
			Vector2 pos = new Vector2(110f, 175f);
			UserGraphics graphics = Main.UserInfo.settings.graphics;
			Rect value = new Rect(40f, 0f, 600f, 50f);
			graphics.ShowingContractProgress = this.gui.CheckBox(pos, graphics.ShowingContractProgress, new Rect?(value), Language.SettingsShowProgressContract, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.ShowingSimpleContractProgress = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), graphics.ShowingSimpleContractProgress, new Rect?(value), Language.SettingsSimpleShowContract, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.AllwaysShowHPDef = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), graphics.AllwaysShowHPDef, new Rect?(value), Language.SettingsAlwaysShowHpDef, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.HideInterface = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), graphics.HideInterface, new Rect?(value), Language.SettingsHideInterface, 16, "#dfdfdf", TextAnchor.UpperLeft);
			this.makeNextCheckBoxCoordinates(ref pos, 30);
			Main.UserInfo.wl_perm = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), Main.UserInfo.wl_perm, new Rect?(value), Language.AllowToAddMe, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.EnableFullScreenInBattle = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), graphics.EnableFullScreenInBattle, new Rect?(value), Language.SettingsEnableFullScreenInBattle, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.ShouldSwitchOffChat = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos, 30), graphics.ShouldSwitchOffChat, new Rect?(value), Language.SettingsSwitchOffChat, 16, "#dfdfdf", TextAnchor.UpperLeft);
			this.gui.TextField(new Rect(405f, 150f, 200f, 30f), Language.AutoScreenshotAt, 15, "#9d9d9d", TextAnchor.UpperLeft, false, false);
			Vector2 pos2 = new Vector2(400f, 175f);
			graphics.ProKillTakeScreen = this.gui.CheckBox(pos2, graphics.ProKillTakeScreen, new Rect?(value), Language.ProKillScreenShotSetting, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.QuadKillTakeScreen = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos2, 30), graphics.QuadKillTakeScreen, new Rect?(value), Language.QuadKillScreenShotSetting, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.LevelUpTakeScreen = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos2, 30), graphics.LevelUpTakeScreen, new Rect?(value), Language.LevelUpScreenShotSetting, 16, "#dfdfdf", TextAnchor.UpperLeft);
			graphics.AchievementTakeScreen = this.gui.CheckBox(this.makeNextCheckBoxCoordinates(ref pos2, 30), graphics.AchievementTakeScreen, new Rect?(value), Language.AchievementScreenShotSetting, 16, "#dfdfdf", TextAnchor.UpperLeft);
			Main.UserInfo.settings.radarAlpha = this.gui.FloatSlider0dot00(new Vector2(420f, 332f), Main.UserInfo.settings.radarAlpha, 0f, 1f, true);
			this.gui.TextField(new Rect(405f, 302f, 200f, 30f), Language.SettingsTransoprentyRadar, 15, "#9d9d9d", TextAnchor.UpperLeft, false, false);
			if (CVars.realm != "standalone" && this.gui.Button(new Vector2(405f, 370f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.TransferProfile, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Main.AddDatabaseRequest<TransferHashGenRequest>(new object[0]);
			}
			if (CVars.IsStandaloneRealm)
			{
				this.gui.TextField(new Rect(405f, 360f, 200f, 30f), Language.Lang, 15, "#9d9d9d", TextAnchor.UpperLeft, false, false);
				Language.CurrentLanguage = this.LanguageDropDown(new Vector2(405f, 380f), Language.CurrentLanguage, ref this.languageDropDown);
			}
		}
		else if (this.settingsState == SettingsState.VIDEO_AUDIO)
		{
			this.gui.Picture(new Vector2(22f, 60f), this.gui.server_window[11]);
			this.gui.TextField(new Rect(30f, 60f, 600f, 50f), Language.SettingsScreenRez, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
			Main.UserInfo.settings.graphics.IsTurnOnMaxQueuedFrames = this.gui.CheckBox(new Vector2(420f, 340f), Main.UserInfo.settings.graphics.IsTurnOnMaxQueuedFrames, new Rect?(new Rect(40f, 0f, 600f, 50f)), "maxQueuedFrames", 16, "#dfdfdf", TextAnchor.UpperLeft);
			Main.UserInfo.settings.graphics.CharacterLQ = this.gui.CheckBox(new Vector2(420f, 305f), Main.UserInfo.settings.graphics.CharacterLQ, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsCharacterQuality, 16, "#dfdfdf", TextAnchor.UpperLeft);
			Main.UserInfo.settings.graphics.PostEffects = this.gui.CheckBox(new Vector2(30f, 305f), Main.UserInfo.settings.graphics.PostEffects, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsPostEffect, 16, "#dfdfdf", TextAnchor.UpperLeft);
			Main.UserInfo.settings.graphics.ShouldLimitFrameRate = this.gui.CheckBox(new Vector2(30f, 340f), Main.UserInfo.settings.graphics.ShouldLimitFrameRate, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.LimitFrameRate, 16, "#dfdfdf", TextAnchor.UpperLeft);
			this.gui.Picture(new Vector2(22f, 115f), this.gui.server_window[11]);
			this.gui.TextField(new Rect(30f, 117f, 600f, 50f), Language.SettingsGraphicQuality, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
			switch (Main.UserInfo.settings.graphics.Level)
			{
			case QualityLevelUser.VeryLow:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsVeryLow, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.Low:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsLow, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.LowMiddle:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsLowMiddle, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.Middle:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsMiddle, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.High:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsHigh, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.Max:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsMax, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			case QualityLevelUser.Custom:
				this.gui.TextField(new Rect(150f, 104f, 300f, 50f), Language.SettingsCustom, 16, "#dfdfdf", TextAnchor.MiddleRight, false, false);
				break;
			}
			if (this.levelSlider == -1f)
			{
				this.levelSlider = (float)Main.UserInfo.settings.graphics.Level;
			}
			if (Main.UserInfo.settings.graphics.Level == QualityLevelUser.Custom)
			{
				this.levelSlider = this.gui.FloatSlider(new Vector2(465f, 124f), 0f, 0f, 5f, false, false, false);
				if ((int)this.levelSlider != 0)
				{
					Main.UserInfo.settings.graphics.Level = (QualityLevelUser)this.levelSlider;
				}
			}
			else
			{
				this.levelSlider = this.gui.FloatSlider(new Vector2(465f, 124f), this.levelSlider, 0f, 5f, false, false, false);
				Main.UserInfo.settings.graphics.Level = (QualityLevelUser)this.levelSlider;
			}
			this.gui.Picture(new Vector2(22f, 152f), this.gui.server_window[11]);
			this.gui.TextField(new Rect(30f, 152f, 600f, 50f), Language.SettingsAdvancedSettingsGr, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 242f, 600f, 50f), Language.SettingsShadowRadius, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 272f, 600f, 50f), Language.SettingsPaltryObjects, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.Picture(new Vector2(22f, 375f), this.gui.server_window[11]);
			this.gui.TextField(new Rect(30f, 375f, 600f, 50f), Language.SettingsAudioMusic, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 410f, 600f, 50f), Language.SettingsOverallVolume, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 440f, 600f, 50f), Language.SettingsSoundVolume, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 470f, 600f, 50f), Language.SettingsRadioVolume, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Main.UserInfo.settings.globalLoudness = this.gui.FloatSlider(new Vector2(265f, 418f), Main.UserInfo.settings.globalLoudness * 10f, 0f, 10f, true, false, false) / 10f;
			Main.UserInfo.settings.soundLoudness = this.gui.FloatSlider(new Vector2(265f, 448f), Main.UserInfo.settings.soundLoudness * 10f, 0f, 10f, true, false, false) / 10f;
			Main.UserInfo.settings.graphics.radioLoudness = this.gui.FloatSlider(new Vector2(265f, 478f), Main.UserInfo.settings.graphics.radioLoudness * 10f, 0f, 10f, true, false, false) / 10f;
			if (this.textureDropDown || this.shadowDropDown)
			{
				Main.UserInfo.settings.graphics.ShadowDistance = this.gui.FloatSlider(new Vector2(265f, 248f), Main.UserInfo.settings.graphics.ShadowDistance, 10f, 80f, false, true, false);
				Main.UserInfo.settings.graphics.SmallDistance = this.gui.FloatSlider(new Vector2(265f, 278f), Main.UserInfo.settings.graphics.SmallDistance, 35f, 100f, false, true, false);
			}
			else
			{
				Main.UserInfo.settings.graphics.ShadowDistance = this.gui.FloatSlider(new Vector2(265f, 248f), Main.UserInfo.settings.graphics.ShadowDistance, 10f, 80f, true, false, false);
				Main.UserInfo.settings.graphics.SmallDistance = this.gui.FloatSlider(new Vector2(265f, 278f), Main.UserInfo.settings.graphics.SmallDistance, 35f, 100f, true, false, false);
			}
			this.gui.TextField(new Rect(30f, 182f, 600f, 50f), Language.SettingsTextureQuality, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(30f, 212f, 600f, 50f), Language.SettingsShadowQuality, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Main.UserInfo.settings.graphics.TextureQ = this.QualityDropDown(new Vector2(150f, 184f), Main.UserInfo.settings.graphics.TextureQ, ref this.textureDropDown);
			if (!this.textureDropDown)
			{
				Main.UserInfo.settings.graphics.ShadowQ = this.SimpleQualityDropDown(new Vector2(150f, 212f), Main.UserInfo.settings.graphics.ShadowQ, ref this.shadowDropDown);
			}
			this.gui.TextField(new Rect(352f, 182f, 600f, 50f), Language.SettingsLightningQuality, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			this.gui.TextField(new Rect(352f, 212f, 600f, 50f), Language.SettingsPhysicsQuality, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Main.UserInfo.settings.graphics.LightingQ = this.QualityDropDown(new Vector2(476f, 184f), Main.UserInfo.settings.graphics.LightingQ, ref this.lightingDropDown);
			if (!this.lightingDropDown)
			{
				Main.UserInfo.settings.graphics.PhysicsQ = this.SimpleQualityDropDown(new Vector2(476f, 212f), Main.UserInfo.settings.graphics.PhysicsQ, ref this.physicsDropDown);
			}
			this.ResolutionDropDown(new Vector2(475f, 64f));
		}
		else if (this.settingsState == SettingsState.CONTROLS)
		{
			if (this.gui.Button(new Vector2(98f, 500f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.SettingsDefaultValue, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				Main.UserInfo.settings.binds = new UserBinds();
			}
			this.gui.TextField(new Rect(170f, 55f, 70f, 25f), Language.SettingsAction, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect(400f, 55f, 200f, 25f), Language.SettingsContolButton, 18, "#9d9d9d", TextAnchor.MiddleLeft, false, false);
			this.gui.Picture(new Vector2(667f, 80f), this.gui.settings_window[5]);
			this.gui.TextField(new Rect(50f, 420f, 600f, 50f), Language.SettingsMouseSensitivity, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			Main.UserInfo.settings.binds.sens = this.gui.FloatSlider0dot00(new Vector2(225f, 428f), Main.UserInfo.settings.binds.sens, 0.01f, 1f, true);
			Main.UserInfo.settings.binds.invertMouse = this.gui.CheckBox(new Vector2(50f, 455f), Main.UserInfo.settings.binds.invertMouse, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsInvertMouse, 16, "#dfdfdf", TextAnchor.UpperLeft);
			this.gui.TextField(new Rect(230f, 455f, 600f, 50f), Language.SettingsHold, 16, "#dfdfdf", TextAnchor.UpperLeft, false, false);
			if (CVars.HoldAim)
			{
				Main.UserInfo.settings.binds.holdAim = this.gui.CheckBox(new Vector2(565f, 455f), Main.UserInfo.settings.binds.holdAim, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsAim, 16, "#dfdfdf", TextAnchor.UpperLeft);
			}
			Main.UserInfo.settings.binds.holdSit = this.gui.CheckBox(new Vector2(435f, 455f), Main.UserInfo.settings.binds.holdSit, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsSit, 16, "#dfdfdf", TextAnchor.UpperLeft);
			Main.UserInfo.settings.binds.holdWalk = this.gui.CheckBox(new Vector2(305f, 455f), Main.UserInfo.settings.binds.holdWalk, new Rect?(new Rect(40f, 0f, 600f, 50f)), Language.SettingsWalk, 16, "#dfdfdf", TextAnchor.UpperLeft);
			this.controlsScroll = this.gui.BeginScrollView(new Rect(22f, 80f, (float)(this.gui.server_window[11].width + 19), 330f), this.controlsScroll, new Rect(0f, 0f, (float)(this.gui.server_window[11].width - 20), (this.indexKey + 2f) * (float)(this.gui.server_window[11].height + 1)), float.MaxValue);
			this.indexKey = 0f;
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMoveForward, ref Main.UserInfo.settings.binds.up);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMoveBack, ref Main.UserInfo.settings.binds.down);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMoveLeft, ref Main.UserInfo.settings.binds.left);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMoveRight, ref Main.UserInfo.settings.binds.right);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsJump, ref Main.UserInfo.settings.binds.jump);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsWalk, ref Main.UserInfo.settings.binds.walk);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsSit, ref Main.UserInfo.settings.binds.sit);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsFire, ref Main.UserInfo.settings.binds.fire);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsAim, ref Main.UserInfo.settings.binds.aim);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsRecharge, ref Main.UserInfo.settings.binds.reload);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsGrenade, ref Main.UserInfo.settings.binds.grenade);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsKnife, ref Main.UserInfo.settings.binds.knife);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsFireMode, ref Main.UserInfo.settings.binds.burst);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsSwitchWeapon, ref Main.UserInfo.settings.binds.toggle);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsSelectPistol, ref Main.UserInfo.settings.binds.pistol);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsSelectMainWeapon, ref Main.UserInfo.settings.binds.weapon);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsUse, ref Main.UserInfo.settings.binds.interaction);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsCallSonar, ref Main.UserInfo.settings.binds.support);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsCallMortarStrike, ref Main.UserInfo.settings.binds.mortar);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsUseSpecEquipment, ref Main.UserInfo.settings.binds.clanSpecialAbility);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsHideShowInterface, ref Main.UserInfo.settings.binds.hideInterface);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMatchStatistics, ref Main.UserInfo.settings.binds.statistics);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsExitToMainMenu, ref Main.UserInfo.settings.binds.menu);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsFullScreen, ref Main.UserInfo.settings.binds.fullscreen);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsTeamChange, ref Main.UserInfo.settings.binds.teamChoose);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsTeamMessage, ref Main.UserInfo.settings.binds.talkTeam);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsMessageToAll, ref Main.UserInfo.settings.binds.talkAll);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsRadioCommand, ref Main.UserInfo.settings.binds.radio);
			this.SimpleKey(new Vector2(0f, 0f + (float)(this.gui.server_window[11].height + 3) * this.indexKey), Language.SettingsScreenshot, ref Main.UserInfo.settings.binds.screenshot);
			this.gui.EndScrollView();
		}
		else if (this.settingsState == SettingsState.NETWORK)
		{
			if (this.controller.Prepared)
			{
				int num = 200;
				int num2 = 22;
				int num3 = 30;
				this.gui.Picture(new Vector2((float)num2, (float)num), this.gui.server_window[11]);
				this.gui.TextField(new Rect((float)num3, (float)num, 600f, 50f), Language.UnityCaching, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
				CVars.UseUnityCache = this.gui.CheckBox(new Vector2(630f, (float)(num + 2)), CVars.UseUnityCache, new Rect?(new Rect(40f, 0f, 600f, 50f)), string.Empty, 16, "#dfdfdf", TextAnchor.UpperLeft);
				if (CVars.UseUnityCache)
				{
					int num4 = num + 50;
					int num5 = num4 + 50;
					int num6 = 250;
					int num7 = 510;
					this.gui.Picture(new Vector2((float)num2, (float)num4), this.gui.server_window[11]);
					this.gui.Picture(new Vector2((float)num2, (float)num5), this.gui.server_window[11]);
					this.gui.TextField(new Rect((float)num3, (float)num4, 600f, 50f), Language.WeaponSizeLoaded, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
					this.gui.TextField(new Rect((float)num3, (float)num5, 600f, 50f), Language.MapsSizeLoaded, 16, "#62aeea", TextAnchor.UpperLeft, false, false);
					string totalWeaponsSizeStr = this.controller.TotalWeaponsSizeStr;
					string weaponsSizeDownloadedStr = this.controller.WeaponsSizeDownloadedStr;
					string totalMapsSizeStr = this.controller.TotalMapsSizeStr;
					string mapsSizeDownloadedStr = this.controller.MapsSizeDownloadedStr;
					this.gui.TextField(new Rect((float)num7, (float)(num4 + 4), 400f, 200f), string.Concat(new string[]
					{
						weaponsSizeDownloadedStr,
						"kb / ",
						Language.SizeTotal,
						totalWeaponsSizeStr,
						"kb"
					}), 15, "#9d9d9d", TextAnchor.UpperLeft, false, false);
					this.gui.TextField(new Rect((float)num7, (float)(num5 + 4), 400f, 200f), string.Concat(new string[]
					{
						mapsSizeDownloadedStr,
						"kb / ",
						Language.SizeTotal,
						totalMapsSizeStr,
						"kb"
					}), 15, "#9d9d9d", TextAnchor.UpperLeft, false, false);
					if (this.gui.Button(new Vector2((float)num6, (float)(num4 + 2)), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.DownloadAllWeapons, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						if (this.controller.TotalWeaponsSize > this.controller.WeaponsSizeDownloaded)
						{
							SingletoneForm<Loader>.Instance.DownloadAllWeapons();
							EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadWeapons, PopupState.precentageWeaponsProgress, false, true, "CancelDownload", string.Empty));
						}
						else
						{
							EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, string.Empty, Language.WeaponsLoaded, PopupState.information, false, true, string.Empty, string.Empty));
						}
					}
					if (this.gui.Button(new Vector2((float)num6, (float)(num5 + 2)), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.DownloadAllMaps, 15, "#adb0af", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
					{
						if (this.controller.TotalMapsSize > this.controller.MapsSizeDownloaded)
						{
							SingletoneForm<Loader>.Instance.DownloadAllMaps();
							EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, string.Empty, Language.DownloadMaps, PopupState.precentageMapsProgress, false, true, "CancelDownload", string.Empty));
						}
						else
						{
							EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, string.Empty, Language.MapsLoaded, PopupState.information, false, true, string.Empty, string.Empty));
						}
					}
				}
			}
		}
		else if (this.settingsState == SettingsState.BONUSES)
		{
			GUI.DrawTexture(new Rect((rect.width - (float)this.HopsLogo.width) / 2f - 50f, 80f, (float)this.HopsLogo.width, (float)this.HopsLogo.height), this.HopsLogo);
			this.gui.TextField(new Rect(-50f, 200f, rect.width, 20f), Language.SettingsHopsBonus, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			Texture2D texture2D = this.gui.settings_window[0];
			Texture2D over = this.gui.settings_window[1];
			Rect rect2 = new Rect((rect.width - (float)texture2D.width) / 2f - 50f, 225f, (float)texture2D.width, (float)texture2D.height);
			this.gui.TextField(rect2, Main.UserInfo.HopsSecretKey, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
			if (this.gui.Button(new Vector2(rect2.x, rect2.y), texture2D, over, null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				TextEditor textEditor = new TextEditor();
				textEditor.content = new GUIContent(Main.UserInfo.HopsSecretKey);
				textEditor.SelectAll();
				textEditor.Copy();
			}
			this.gui.TextField(new Rect(-50f, 255f, rect.width, 20f), Language.SettingsHopsBonusHint, 12, "#00FF00", TextAnchor.MiddleCenter, false, false);
			if (!string.IsNullOrEmpty(Main.UserInfo.HopsRoulettePrizeKey))
			{
				rect2.y += 85f;
				this.gui.TextField(new Rect(-50f, 285f, rect.width, 20f), Language.SettingsHopsKey, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
				this.gui.TextField(rect2, Main.UserInfo.HopsRoulettePrizeKey, 16, "#FFFFFF", TextAnchor.MiddleCenter, false, false);
				if (this.gui.Button(new Vector2(rect2.x, rect2.y), texture2D, over, null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
				{
					TextEditor textEditor2 = new TextEditor();
					textEditor2.content = new GUIContent(Main.UserInfo.HopsRoulettePrizeKey);
					textEditor2.SelectAll();
					textEditor2.Copy();
				}
				this.gui.TextField(new Rect(-50f, 340f, rect.width, 20f), Language.SettingsHopsBonusHint, 12, "#00FF00", TextAnchor.MiddleCenter, false, false);
				if (this.gui.Button(new Vector2(252f, 360f), this.gui.mainMenuButtons[0], this.gui.mainMenuButtons[1], this.gui.mainMenuButtons[1], Language.HopsActivationInstruction, 14, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
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
		}
		if (this.gui.Button(new Vector2(608f, 23f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.settingsState = SettingsState.GAME;
			this.Hide(0.35f);
			if (!this.nickAvailable)
			{
				this.nick = Main.UserInfo.nick;
			}
		}
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x06000AFB RID: 2811 RVA: 0x00089770 File Offset: 0x00087970
	private void SettingNickColorZone()
	{
		GUI.Label(new Rect(590f, 54f, 100f, 50f), Language.ChangeNickColor, ClanSystemWindow.I.Styles.styleGrayLabel);
		this._tmpStyle.textColor = ClanSystemWindow.I.Styles.styleGrayLabel.normal.textColor;
		ClanSystemWindow.I.Styles.styleGrayLabel.normal.textColor = this._tmpStyle.textColor;
		if (this.gui.Button(new Vector2(577f, 95f), this.gui.server_window[14], this.gui.server_window[15], this.gui.server_window[16], Globals.I.buyNickColorChangePrice + "    .", 16, "#fac321", TextAnchor.MiddleCenter, null, null, this.gui.hoverSoundPrefab, this.gui.clickSoundPrefab).Clicked && !Main.IsGameLoaded)
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.SettingsBuyColorChange, string.Empty, delegate()
			{
			}, PopupState.buyNickColorCnage, false, true, new object[0]));
		}
		this.gui.Picture(new Vector2((float)(577 + this.gui.server_window[14].width / 2 + 6), 98f), this.gui.gldIcon);
	}

	// Token: 0x06000AFC RID: 2812 RVA: 0x0008991C File Offset: 0x00087B1C
	private bool MakeCheckBox(string description, Vector2 firstColumnInitialPosition, UserGraphics settingsGraphics, Rect groupRect, int textSize = 16, string textColor = "#dfdfdf")
	{
		return this.gui.CheckBox(firstColumnInitialPosition, settingsGraphics.ShowingContractProgress, new Rect?(groupRect), description, textSize, textColor, TextAnchor.UpperLeft);
	}

	// Token: 0x06000AFD RID: 2813 RVA: 0x00089948 File Offset: 0x00087B48
	private Vector2 makeNextCheckBoxCoordinates(ref Vector2 vector, int yStep = 30)
	{
		vector.y += (float)yStep;
		return vector;
	}

	// Token: 0x06000AFE RID: 2814 RVA: 0x00089960 File Offset: 0x00087B60
	private void allocateCheckBoxes(ref bool[] boxValue, string[] boxName, int yPosition = 185, int yStep = 30, int numberOfBoxesInColumn = 6)
	{
		if (boxValue.Length == boxName.Length)
		{
			int num = 110;
			for (int i = 0; i < boxValue.Length; i++)
			{
				boxValue[i] = this.gui.CheckBox(new Vector2((float)num, (float)yPosition), boxValue[i], new Rect?(new Rect(40f, 0f, 600f, 50f)), boxName[i], 16, "#dfdfdf", TextAnchor.UpperLeft);
				if (i == numberOfBoxesInColumn - 1)
				{
					yPosition = 185;
					num = 450;
				}
				else
				{
					yPosition += yStep;
				}
			}
		}
		else
		{
			Debug.Log("not equal number of checkboxes and their names");
		}
	}

	// Token: 0x06000AFF RID: 2815 RVA: 0x00089A08 File Offset: 0x00087C08
	private void SaveSettings()
	{
		if (this.nickAvailable && this.nickError == Language.SettingsNickAllow && Main.UserInfo.nickChange > 0)
		{
			this.nick = this.nick.Replace("\n", string.Empty);
			Main.UserInfo.nick = this.nick;
			UserInfo userInfo = Main.UserInfo;
			userInfo.nickChange = --userInfo.nickChange;
		}
		Main.AddDatabaseRequest<SaveProfile>(new object[0]);
	}

	// Token: 0x06000B00 RID: 2816 RVA: 0x00089A98 File Offset: 0x00087C98
	private void cancelReceive()
	{
		if (!this.controller.Prepared)
		{
			this.controller.cancelReceive();
			EventFactory.Call("HidePopup", new Popup(WindowsID.Load, string.Empty, Language.ReceivingInformation, PopupState.progress, false, true, string.Empty, string.Empty));
		}
	}

	// Token: 0x04000CF2 RID: 3314
	private NetworkSettingsController controller;

	// Token: 0x04000CF3 RID: 3315
	public Texture2D progress_small;

	// Token: 0x04000CF4 RID: 3316
	public Texture2D clanTag_back;

	// Token: 0x04000CF5 RID: 3317
	public Texture2D HopsLogo;

	// Token: 0x04000CF6 RID: 3318
	private bool textureDropDown;

	// Token: 0x04000CF7 RID: 3319
	private bool shadowDropDown;

	// Token: 0x04000CF8 RID: 3320
	private bool languageDropDown;

	// Token: 0x04000CF9 RID: 3321
	private bool physicsDropDown;

	// Token: 0x04000CFA RID: 3322
	private bool lightingDropDown;

	// Token: 0x04000CFB RID: 3323
	private Vector2 resScrollPos = Vector2.zero;

	// Token: 0x04000CFC RID: 3324
	private bool resDropDown;

	// Token: 0x04000CFD RID: 3325
	private float indexKey;

	// Token: 0x04000CFE RID: 3326
	private float whereKey = -1f;

	// Token: 0x04000CFF RID: 3327
	private float levelSlider = -1f;

	// Token: 0x04000D00 RID: 3328
	private Vector2 controlsScroll = Vector2.zero;

	// Token: 0x04000D01 RID: 3329
	private SettingsState settingsState;

	// Token: 0x04000D02 RID: 3330
	private string nickError = string.Empty;

	// Token: 0x04000D03 RID: 3331
	private string nick = string.Empty;

	// Token: 0x04000D04 RID: 3332
	private string prevNick = string.Empty;

	// Token: 0x04000D05 RID: 3333
	private bool nickAvailable = true;

	// Token: 0x04000D06 RID: 3334
	private eTimer nickTimer = new eTimer();

	// Token: 0x04000D07 RID: 3335
	private GUIStyleState _tmpStyle = new GUIStyleState();
}
