using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

// Token: 0x02000164 RID: 356
[AddComponentMenu("Scripts/GUI/MasteringGUI")]
internal class MasteringGUI : Form
{
	// Token: 0x1700012C RID: 300
	// (get) Token: 0x0600095F RID: 2399 RVA: 0x0005F460 File Offset: 0x0005D660
	public new Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width - this.gui.Width) * 0.5f, (float)(Screen.height - this.gui.Height) * 0.5f, (float)this.gui.Width, (float)this.gui.Height);
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x06000960 RID: 2400 RVA: 0x0005F4BC File Offset: 0x0005D6BC
	public Suit CurrentSuit
	{
		get
		{
			return MasteringSuitsInfo.Instance.Suits[Main.UserInfo.suitNameIndex];
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0005F4DC File Offset: 0x0005D6DC
	private void Start()
	{
		this._charactBarBack = this.masteringTextures[0];
		this._charactBarBlue = this.masteringTextures[1];
		this._charactBarFilled = this.masteringTextures[2];
		this._charactBarRed = this.masteringTextures[3];
		this._charactBarGreen = this.masteringTextures[4];
		this._expBar = this.masteringTextures[12];
		this._expBarBack = this.masteringTextures[13];
		this._modEmpty = this.masteringTextures[15];
		this._modUnavailable = this.masteringTextures[19];
		this._modBackGray = this.masteringTextures[20];
		this._modBackRed = this.masteringTextures[21];
		this._opticBack = this.masteringTextures[23];
		this._wTask = this.masteringTextures[25];
		this._expIcon = this.masteringTextures[26];
		this._mpPriceBack = this.masteringTextures[27];
		this._grayBack = this.masteringTextures[28];
		this._mastBigIcon = this.masteringTextures[29];
		this._mpBirdSmall = this.masteringTextures[30];
		this._mpBackGlow = this.masteringTextures[31];
		this._modInstalledBorder = this.masteringTextures[32];
		this._modSelectedBorder = this.masteringTextures[33];
		this._modLockedIcon = this.masteringTextures[34];
		this._grayScrollFill = this.masteringTextures[36];
		this._smallProgressBarBorder = this.masteringTextures[40];
		this._smallProgressBarFiller = this.masteringTextures[41];
		this._fade = this.masteringTextures[42];
		this._krutilka = this.weaponViewerTextures[0];
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0005F678 File Offset: 0x0005D878
	public override void MainInitialize()
	{
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0005F690 File Offset: 0x0005D890
	public void ShowMasteringGUI(WeaponInfo weapon)
	{
		this._scrollPosition = Vector2.zero;
		this._selectedMod = null;
		this._currentWeapon = null;
		this._weaponInfo = null;
		this._weaponInfo = weapon;
		this._currentWeapon = weapon.CurrentWeapon;
		this._wid = (int)weapon.CurrentWeapon.type;
		this._suitWeaponMods = this.CurrentSuit.CurrentWeaponsMods[this._wid].Mods;
		this._tempWeaponMods = new Dictionary<ModType, int>(this._suitWeaponMods);
		this.CurrentSuit.CurrentWeaponsMods[this._wid].TemporaryMods = this._tempWeaponMods;
		this._tmpWeaponExp = Main.UserInfo.Mastering.WeaponsStats[this._wid].WeaponExp;
		float num = ((float)this._currentWeapon.durability - weapon.repair_info) / (float)this._currentWeapon.durability;
		if (weapon.isUndestructable)
		{
			num = 1f;
		}
		this._baseCharacteristics = new float[]
		{
			this._currentWeapon.BaseAccuracyProc * num,
			this._currentWeapon.BaseRecoilProc,
			this._currentWeapon.BaseDamageProc,
			this._currentWeapon.BasePierceProc,
			this._currentWeapon.BaseMobilityProc
		};
		this._skillCharacteristics = new float[]
		{
			this._currentWeapon.SkillAccuracyProcBonus,
			this._currentWeapon.SkillRecoilProcBonus,
			this._currentWeapon.SkillDamageProcBonus,
			this._currentWeapon.SkillPierceProcBonus,
			this._currentWeapon.SkillMobilityProcBonus
		};
		this._additionModCharacteristics = new float[7];
		this._multiplicationModCharacteristics = new float[7];
		this._weaponStats = Main.UserInfo.Mastering.WeaponsStats[this._wid];
		this._currentWeapon.LoadTable(Globals.I.weapons[this._wid]);
		this.UpdateCharacteristics();
		this.ShowWeaponViewer(weapon.CurrentWeapon.type);
		Main.UserInfo.weaponsStates[this._wid].CurrentWeapon.ApplyModsEffect(this._suitWeaponMods);
		this.Show(0.5f, 0f);
		MainGUI.Instance.MasteringBtnClicked = false;
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0005F8D8 File Offset: 0x0005DAD8
	public override void InterfaceGUI()
	{
		this._tempSkin = GUI.skin;
		GUI.skin = this.MasteringSkin;
		GUI.enabled = (!PopupGUI.IsAnyPopupShow && this._weaponObject != null);
		this.DrawBackground();
		this.DrawWeaponViewer();
		GUI.BeginGroup(this.Rect);
		this.DrawCloseButton();
		this.DrawWindowHeader();
		this._masteringVisibility = ((!this._zoomed) ? (1f - this.ZoomTimer.Time * 4f) : (4f * this.ZoomTimer.Time)) * base.visibility;
		this.gui.color = Colors.alpha(this.gui.color, this._masteringVisibility);
		GUI.enabled = (!this._zoomed && !PopupGUI.IsAnyPopupShow && this._weaponObject != null);
		this.DrawScrollList();
		this.DrawCurrentWeaponMods();
		this.DrawModDescription(this._selectedMod);
		this.DrawCurrentWeaponInfo();
		this._effectsLabels = new string[]
		{
			Language.Accuracy,
			Language.Impact,
			Language.Damage,
			Language.Penetration,
			Language.Mobility,
			Language.EffectiveDistance,
			Language.HearDistance,
			Language.ShotGrouping
		};
		this.DrawCharacteristics(new Vector2(300f, 485f), this.ModEffectIcons, this._effectsLabels, this._baseCharacteristics, this._skillCharacteristics, this._additionModCharacteristics, this._multiplicationModCharacteristics);
		GUI.enabled = true;
		GUI.EndGroup();
		GUI.skin = this._tempSkin;
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0005FA84 File Offset: 0x0005DC84
	private void DrawWindowHeader()
	{
		GUI.Label(new Rect(10f, 10f, 100f, 10f), Language.WeaponCustomization.ToUpper() + " " + this._currentWeapon.ShortName, CWGUI.p.MastWindowHeaderStyle);
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x0005FAD8 File Offset: 0x0005DCD8
	private void DrawBackground()
	{
		this.gui.color = Colors.alpha(Color.white, base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2((float)Screen.width, (float)Screen.height));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x0005FB58 File Offset: 0x0005DD58
	private void DrawCharacteristics(Vector2 upperLeft, Texture2D[] icons, string[] labels, float[] weaponCharacteristics, float[] skillCharacteristic, float[] addModCharacteristics, float[] multModCharacteristics)
	{
		GUI.DrawTexture(new Rect(upperLeft.x + 3f, upperLeft.y, 493f, 106f), this._grayBack);
		int num = addModCharacteristics.Length;
		for (int i = 0; i < num - 3; i++)
		{
			GUI.DrawTexture(new Rect(upperLeft.x + 30f, upperLeft.y + 11f + (float)(i * 16), (float)icons[i].width, (float)icons[i].height), icons[i]);
			GUI.Label(new Rect(upperLeft.x + 44f, upperLeft.y + 11f + (float)(i * 16), 50f, 9f), labels[i], CWGUI.p.MastCharacteristicStyle);
			float num2 = weaponCharacteristics[i];
			float num3 = skillCharacteristic[i];
			float num4 = addModCharacteristics[i];
			float num5 = multModCharacteristics[i];
			this.DrawProgressBar(new Vector2(upperLeft.x + 117f, upperLeft.y + 11f + (float)(i * 16)), num2, num3, num4, num5, labels[i] == Language.Impact);
			string text = "<color=grey>" + weaponCharacteristics[i].ToString("F0") + "</color>";
			string text2 = num3.ToString("F0");
			string text3 = num4.ToString("F0");
			if (num5 != 0f)
			{
				num4 = Mathf.Round(num2 / 100f * num5);
			}
			string text4 = (num2 + num4 + num3).ToString("F0");
			if (num2 + num4 + num3 <= 0f)
			{
				text4 = "1";
			}
			if (num3 != 0f)
			{
				text2 = ((num2 + num3 <= num2 && !(labels[i] == Language.Impact)) ? string.Concat(new string[]
				{
					"<color=",
					Colors.RadarRedWeb,
					">",
					num3.ToString("F0"),
					"</color>"
				}) : string.Concat(new string[]
				{
					"<color=",
					Colors.RadarBlueWeb,
					">",
					(num3 <= 0f) ? string.Empty : "+",
					num3.ToString("F0"),
					"</color>"
				}));
			}
			if (num4 != 0f)
			{
				text3 = ((num2 + num4 <= num2 && !(labels[i] == Language.Impact)) ? string.Concat(new string[]
				{
					"<color=",
					Colors.RadarRedWeb,
					">",
					num4.ToString("F0"),
					"</color>"
				}) : string.Concat(new string[]
				{
					"<color=",
					Colors.RadarGreenWeb,
					">",
					(num4 <= 0f) ? string.Empty : "+",
					num4.ToString("F0"),
					"</color>"
				}));
			}
			TextAnchor alignment = CWGUI.p.MastCharacteristicStyle.alignment;
			CWGUI.p.MastCharacteristicStyle.alignment = TextAnchor.MiddleCenter;
			GUI.Label(new Rect(upperLeft.x + 415f, upperLeft.y + 11f + (float)(i * 16), 20f, 9f), text, CWGUI.p.MastCharacteristicStyle);
			GUI.Label(new Rect(upperLeft.x + 435f, upperLeft.y + 11f + (float)(i * 16), 20f, 9f), text2, CWGUI.p.MastCharacteristicStyle);
			GUI.Label(new Rect(upperLeft.x + 455f, upperLeft.y + 11f + (float)(i * 16), 20f, 9f), text3, CWGUI.p.MastCharacteristicStyle);
			GUI.Label(new Rect(upperLeft.x + 475f, upperLeft.y + 11f + (float)(i * 16), 20f, 9f), text4, CWGUI.p.MastCharacteristicStyle);
			CWGUI.p.MastCharacteristicStyle.alignment = alignment;
		}
		GUI.DrawTexture(new Rect(upperLeft.x + 30f, upperLeft.y + 11f + 80f, (float)icons[5].width, (float)icons[5].height), icons[5]);
		GUI.Label(new Rect(upperLeft.x + 44f, upperLeft.y + 11f + 80f, 50f, 9f), labels[5], CWGUI.p.MastCharacteristicStyle);
		float num6 = multModCharacteristics[5];
		string text5 = "+" + num6 + "%";
		if (num6 != 0f)
		{
			text5 = ((num6 <= 0f) ? string.Concat(new object[]
			{
				"<color=",
				Colors.RadarRedWeb,
				">",
				num6,
				"%</color>"
			}) : string.Concat(new object[]
			{
				"<color=",
				Colors.RadarGreenWeb,
				">+",
				num6,
				"%</color>"
			}));
		}
		GUI.Label(new Rect(upperLeft.x + 130f, upperLeft.y + 11f + 80f, 20f, 9f), text5, CWGUI.p.MastCharacteristicStyle);
		GUI.DrawTexture(new Rect(upperLeft.x + 185f, upperLeft.y + 11f + 80f, (float)icons[6].width, (float)icons[6].height), icons[6]);
		GUI.Label(new Rect(upperLeft.x + 199f, upperLeft.y + 11f + 80f, 50f, 9f), labels[6], CWGUI.p.MastCharacteristicStyle);
		float num7 = (addModCharacteristics[6] <= 0f) ? 0f : addModCharacteristics[6];
		string text6 = "+" + num7 + "%";
		if (num7 > 0f)
		{
			text6 = ((num7 <= 1f) ? string.Concat(new string[]
			{
				"<color=",
				Colors.RadarGreenWeb,
				">-",
				(100f - num7 * 100f).ToString("F0"),
				"%</color>"
			}) : string.Concat(new string[]
			{
				"<color=",
				Colors.RadarRedWeb,
				">+",
				(num7 * 100f - 100f).ToString("F0"),
				"%</color>"
			}));
		}
		GUI.Label(new Rect(upperLeft.x + 290f, upperLeft.y + 11f + 80f, 20f, 9f), text6, CWGUI.p.MastCharacteristicStyle);
		if (this._weaponInfo.CurrentWeapon.weaponNature == WeaponNature.shotgun)
		{
			GUI.DrawTexture(new Rect(upperLeft.x + 345f, upperLeft.y + 11f + 80f, (float)icons[7].width, (float)icons[7].height), icons[7]);
			GUI.Label(new Rect(upperLeft.x + 359f, upperLeft.y + 11f + 80f, 50f, 9f), labels[7], CWGUI.p.MastCharacteristicStyle);
			float num8 = multModCharacteristics[7];
			string text7 = (num8 <= 1f) ? string.Concat(new string[]
			{
				"<color=",
				Colors.RadarGreenWeb,
				">-",
				(100f - num8 * 100f).ToString("F0"),
				"%</color>"
			}) : string.Concat(new string[]
			{
				"<color=",
				Colors.RadarRedWeb,
				">+",
				(num8 * 100f - 100f).ToString("F0"),
				"%</color>"
			});
			GUI.Label(new Rect(upperLeft.x + 453f, upperLeft.y + 11f + 80f, 20f, 9f), text7, CWGUI.p.MastCharacteristicStyle);
		}
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00060454 File Offset: 0x0005E654
	private void DrawProgressBar(Vector2 upperLeft, float baseValue, float skillVal, float addModVal, float multModVal, bool inverse = false)
	{
		baseValue = Mathf.Min(Mathf.Round(baseValue), 100f);
		this._charactBarBack.wrapMode = TextureWrapMode.Repeat;
		Rect position = new Rect(upperLeft.x, upperLeft.y, 300f, (float)this._charactBarBack.height);
		GUI.DrawTextureWithTexCoords(position, this._charactBarBack, new Rect(0f, 0f, position.width / (float)this._charactBarBack.width, position.height / (float)this._charactBarBack.height));
		this._charactBarFilled.wrapMode = TextureWrapMode.Repeat;
		Rect position2 = new Rect(upperLeft.x, upperLeft.y, baseValue * 3f, (float)this._charactBarFilled.height);
		GUI.DrawTextureWithTexCoords(position2, this._charactBarFilled, new Rect(0f, 0f, position2.width / (float)this._charactBarFilled.width, position2.height / (float)this._charactBarFilled.height));
		GUI.BeginGroup(new Rect(upperLeft.x, upperLeft.y, 300f, (float)this._charactBarBack.height));
		if (multModVal != 0f)
		{
			Texture2D texture2D = this._charactBarBlue;
			texture2D.wrapMode = TextureWrapMode.Repeat;
			float num = Mathf.Round(skillVal);
			Rect position3 = new Rect(baseValue * 3f, 0f, num * 3f, (float)texture2D.height);
			GUI.DrawTextureWithTexCoords(position3, texture2D, new Rect(0f, 0f, position3.width / (float)texture2D.width, position3.height / (float)texture2D.height));
			texture2D = ((multModVal >= 0f || inverse) ? this._charactBarGreen : this._charactBarRed);
			texture2D.wrapMode = TextureWrapMode.Repeat;
			num = Mathf.Round(baseValue / 100f * multModVal);
			position3 = new Rect((skillVal + baseValue) * 3f, 0f, num * 3f, (float)texture2D.height);
			GUI.DrawTextureWithTexCoords(position3, texture2D, new Rect(0f, 0f, position3.width / (float)texture2D.width, position3.height / (float)texture2D.height));
		}
		else
		{
			Texture2D texture2D2 = this._charactBarBlue;
			texture2D2.wrapMode = TextureWrapMode.Repeat;
			Rect position4 = new Rect(baseValue * 3f, 0f, Mathf.Round(skillVal) * 3f, (float)this._charactBarBlue.height);
			GUI.DrawTextureWithTexCoords(position4, texture2D2, new Rect(0f, 0f, position4.width / (float)texture2D2.width, position4.height / (float)texture2D2.height));
			texture2D2 = ((addModVal >= 0f || inverse) ? this._charactBarGreen : this._charactBarRed);
			texture2D2.wrapMode = TextureWrapMode.Repeat;
			position4 = new Rect((skillVal + baseValue) * 3f, 0f, Mathf.Round(addModVal) * 3f, (float)this._charactBarBlue.height);
			GUI.DrawTextureWithTexCoords(position4, texture2D2, new Rect(0f, 0f, position4.width / (float)texture2D2.width, position4.height / (float)texture2D2.height));
		}
		GUI.EndGroup();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x000607A4 File Offset: 0x0005E9A4
	private void DrawCloseButton()
	{
		int width = CWGUI.p.closeButton.normal.background.width;
		int height = CWGUI.p.closeButton.normal.background.height;
		if (GUI.Button(new Rect(this.Rect.width - (float)width - 10f, 5f, (float)width, (float)height), string.Empty, CWGUI.p.closeButton) && !PopupGUI.IsAnyPopupShow)
		{
			this._currentWeapon.LoadTable(Globals.I.weapons[this._wid]);
			this.UpdateCharacteristics();
			Main.UserInfo.weaponsStates[this._wid].CurrentWeapon.ApplyModsEffect(this._suitWeaponMods);
			this.CloseMastering();
		}
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00060878 File Offset: 0x0005EA78
	private void CloseMastering()
	{
		this.HideWeaponViewer(0.35f);
		this.Hide(0.35f);
		this.CurrentSuit.CurrentWeaponsMods[this._wid].TemporaryMods = null;
		EventFactory.Call("ShowInterface", null);
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x000608C4 File Offset: 0x0005EAC4
	private void DrawScrollList()
	{
		int count = WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).MetaLevels.Count;
		bool wtaskUnlocked = this._weaponStats.MetaUnlocked(0);
		this._scrollRect2.Set(this._scrollRect2.x, this._scrollRect2.y, this._scrollRect2.width, (float)(72 * count));
		GUI.DrawTexture(new Rect(this._scrollRect1.xMin, this._scrollRect1.yMin - 3f, this._scrollRect1.width + 3f, this._scrollRect1.height + 6f), this._grayBack);
		this._scrollPosition = GUI.BeginScrollView(this._scrollRect1, this._scrollPosition, this._scrollRect2);
		this._tmpWeaponExp = Main.UserInfo.Mastering.WeaponsStats[this._wid].WeaponExp;
		float num = 0f;
		for (int i = 0; i < count; i++)
		{
			bool hidden = WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).MetaLevels[i].Hidden;
			if (hidden)
			{
				num = 72f;
			}
			bool metaLocked = !this._weaponStats.MetaUnlocked(i - 1);
			bool metaInProgress = this._weaponStats.MetaInProgress(i - 1, this._currentWeapon.isPremium);
			Vector2 pos = new Vector2(this._scrollRect1.xMin + 5f, this._scrollRect1.yMin + (float)(i * 72) - num);
			this.DrawMetaLevelBar(i, pos, this._weaponStats.MetaExp, metaLocked, metaInProgress, wtaskUnlocked, hidden);
		}
		if (Input.GetMouseButtonDown(1))
		{
			this._selectedMod = null;
		}
		GUI.EndScrollView();
		if (this._showWtaskHint && !this.ZoomTimer.IsStarted && !this._zoomed)
		{
			float a = MainGUI.Instance.CalcWidth(this._currentWeapon.WtaskDescription, MainGUI.Instance.fontTahoma, 9);
			float b = MainGUI.Instance.CalcWidth(Language.MasteringWtaskHint + ":", MainGUI.Instance.fontTahoma, 9);
			float width = Mathf.Max(a, b) + 5f;
			Rect position = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 32f, width, 32f);
			this.gui.color = new Color(1f, 1f, 1f, base.visibility * 0.9f);
			GUI.DrawTexture(position, MainGUI.Instance.black);
			this.gui.color = new Color(1f, 1f, 1f, base.visibility);
			CWGUI.p.standartTahoma9.normal.textColor = Helpers.HexToColor("4AA5FF");
			GUI.Label(new Rect(position.x + 5f, position.y - 9f, position.width, position.height), Language.MasteringWtaskHint + ":", CWGUI.p.standartTahoma9);
			CWGUI.p.standartTahoma9.normal.textColor = Colors.RadarWhite;
			GUI.Label(new Rect(position.x + 5f, position.y + 5f, position.width, position.height), this._currentWeapon.WtaskDescription, CWGUI.p.standartTahoma9);
		}
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x00060C7C File Offset: 0x0005EE7C
	private void DrawCurrentWeaponInfo()
	{
		GUI.DrawTexture(new Rect(0f, 535f, (float)this._mpBackGlow.width, (float)this._mpBackGlow.height), this._mpBackGlow);
		GUIStyle style = new GUIStyle
		{
			normal = 
			{
				background = this._mastBigIcon
			}
		};
		if (GUI.Button(new Rect(65f, 523f, (float)this._mastBigIcon.width, (float)this._mastBigIcon.height), string.Empty, style))
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BankMPTitle, string.Empty, PopupState.buyMp, false, true, string.Empty, string.Empty));
		}
		GUI.Label(new Rect(30f, 537f, 40f, 40f), Main.UserInfo.Mastering.MasteringPoints.ToString(), CWGUI.p.MastMPCountStyle);
		GUI.DrawTexture(new Rect(85f, 580f, (float)this._smallProgressBarBorder.width, (float)this._smallProgressBarBorder.height), this._smallProgressBarBorder);
		float width = (float)(this._smallProgressBarBorder.width - 2) * ((float)Main.UserInfo.Mastering.CurrentExp / (float)Main.UserInfo.Mastering.ExpToNextPoint) - 2f;
		GUI.DrawTexture(new Rect(87f, 582f, width, 2f), this._smallProgressBarFiller);
		string text = Helpers.SeparateNumericString(Main.UserInfo.Mastering.CurrentExp.ToString());
		TextAnchor alignment = CWGUI.p.MastModDescContentStyle.alignment;
		CWGUI.p.MastModDescContentStyle.alignment = TextAnchor.MiddleRight;
		GUI.Label(new Rect(52f, 577f, 30f, 10f), text, CWGUI.p.MastModDescContentStyle);
		text = Helpers.SeparateNumericString(Main.UserInfo.Mastering.ExpToNextPoint.ToString());
		CWGUI.p.MastModDescContentStyle.alignment = alignment;
		GUI.Label(new Rect((float)(85 + this._smallProgressBarBorder.width + 4), 577f, 30f, 10f), text, CWGUI.p.MastModDescContentStyle);
		string text2 = (this._weaponStats.WeaponExp >= 100000) ? Helpers.KFormat(this._weaponStats.WeaponExp) : Helpers.SeparateNumericString(this._weaponStats.WeaponExp.ToString("F0"));
		GUI.DrawTexture(new Rect(165f, 553f, (float)this._expIcon.width, (float)this._expIcon.height), this._expIcon);
		GUI.Label(new Rect(195f, 551f, 70f, 15f), text2, CWGUI.p.MastExpCountStyle);
		alignment = CWGUI.p.MastModDescContentStyle.alignment;
		CWGUI.p.MastModDescContentStyle.alignment = TextAnchor.MiddleCenter;
		GUI.Label(new Rect(145f, 568f, 150f, 9f), Language.MasteringWeaponExp, CWGUI.p.MastModDescContentStyle);
		CWGUI.p.MastModDescContentStyle.alignment = alignment;
		float num = MainGUI.Instance.CalcWidth(text2, CWGUI.p.MastExpCountStyle.font, CWGUI.p.MastExpCountStyle.fontSize);
		int value = Main.UserInfo.userStats.weaponKills[(int)this._currentWeapon.type];
		GUI.DrawTexture(new Rect(200f + num, 550f, (float)this.KillsIcon.width, (float)this.KillsIcon.height), this.KillsIcon);
		GUI.Label(new Rect(203f + num + (float)this.KillsIcon.width, 551f, 70f, 15f), Helpers.KFormat(value), CWGUI.p.MastExpCountStyle);
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0006107C File Offset: 0x0005F27C
	private void DrawModDescription(MasteringMod mod)
	{
		GUI.DrawTexture(new Rect(5f, 465f, 295f, 60f), this._opticBack);
		GUI.DrawTexture(new Rect(300f, 465f, 497f, 17f), this._opticBack);
		if (mod == null)
		{
			return;
		}
		WeaponSpecificModInfo weaponSpecificModInfo = mod.WeaponSpecificInfo[this._wid];
		bool flag = Main.UserInfo.Mastering.WeaponsStats[this._wid].ModUnlocked(weaponSpecificModInfo.Level, weaponSpecificModInfo.Index);
		bool flag2 = Main.UserInfo.Mastering.WeaponsStats[this._wid].ModUnlocked(weaponSpecificModInfo.ParentLevel, weaponSpecificModInfo.ParentIndex);
		bool flag3 = Main.UserInfo.Mastering.WeaponsStats[this._wid].MetaUnlocked(weaponSpecificModInfo.Level);
		GUI.Label(new Rect(10f, 465f, 250f, 20f), (!mod.IsCamo) ? mod.FullName : mod.ShortName, CWGUI.p.MastModDescHeaderStyle);
		GUI.Label(new Rect(10f, 475f, 245f, 45f), (!mod.IsCamo) ? mod.Description : mod.FullName, CWGUI.p.MastModDescContentStyle);
		if (!flag)
		{
			this.DrawModPrice(new Vector2(252f, 468f), weaponSpecificModInfo.Mp);
			int width = CWGUI.p.MastUnlockBtnStyle.normal.background.width;
			int height = CWGUI.p.MastUnlockBtnStyle.normal.background.height;
			if (flag3 && ((weaponSpecificModInfo.HasParent && flag2) || !weaponSpecificModInfo.HasParent) && GUI.Button(new Rect(264f, 490f, (float)width, (float)height), string.Empty, CWGUI.p.MastUnlockBtnStyle))
			{
				object[] args = new object[]
				{
					this._currentWeapon.type,
					mod,
					weaponSpecificModInfo.Level,
					weaponSpecificModInfo.Index
				};
				EventFactory.Call("ShowPopup", new Popup(WindowsID.BuyMod, Language.MasteringPopupModBuyHeader, Language.MasteringPopupModBuyBody, delegate()
				{
					EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.MasteringPopupModBuyHeader, Language.MasteringPopupModBuyComplete, PopupState.information, false, false, string.Empty, string.Empty));
					Audio.Play(this.ModBuyAudioClip);
				}, PopupState.buyMasteringMod, false, true, args));
			}
		}
		if (mod.ShotGrouping.Val > 0f)
		{
			string text = (mod.ShotGrouping.Val <= 1f) ? string.Concat(new string[]
			{
				"<color=",
				Colors.RadarGreenWeb,
				">-",
				(100f - mod.ShotGrouping.Val * 100f).ToString("F0"),
				"%</color>"
			}) : string.Concat(new string[]
			{
				"<color=",
				Colors.RadarRedWeb,
				">+",
				(mod.ShotGrouping.Val * 100f - 100f).ToString("F0"),
				"%</color>"
			});
			this._modEffects = new string[]
			{
				mod.Accuracy.StrVal,
				mod.Recoil.StrVal,
				mod.Damage.StrVal,
				mod.Penetration.StrVal,
				mod.Mobility.StrVal,
				mod.EffectiveDistance.StrVal,
				mod.HearDistance.StrVal,
				text
			};
		}
		else
		{
			this._modEffects = new string[]
			{
				mod.Accuracy.StrVal,
				mod.Recoil.StrVal,
				mod.Damage.StrVal,
				mod.Penetration.StrVal,
				mod.Mobility.StrVal,
				mod.EffectiveDistance.StrVal,
				mod.HearDistance.StrVal
			};
		}
		for (int i = 0; i < this._modEffects.Length; i++)
		{
			float top = 465f + (float)(17 - this.ModEffectIcons[i].height) * 0.5f;
			GUI.DrawTexture(new Rect((float)(350 + 55 * i), top, (float)this.ModEffectIcons[i].width, (float)this.ModEffectIcons[i].height), this.ModEffectIcons[i]);
			bool flag4 = this._effectsLabels[i] == Language.Impact || this._effectsLabels[i] == Language.HearDistance;
			string text2 = (!string.IsNullOrEmpty(this._modEffects[i])) ? this._modEffects[i] : "<color=grey>---</color>";
			if (!string.IsNullOrEmpty(this._modEffects[i]))
			{
				text2 = ((!this._modEffects[i].Contains("-")) ? ("<color=" + ((!flag4) ? Colors.RadarGreenWeb : Colors.RadarRedWeb) + ">+" + this._modEffects[i]) : ("<color=" + ((!flag4) ? Colors.RadarRedWeb : Colors.RadarGreenWeb) + ">" + this._modEffects[i])) + "</color>";
			}
			GUI.Label(new Rect((float)(355 + this.ModEffectIcons[i].width + 55 * i), 469f, 50f, 9f), text2, CWGUI.p.MastCharacteristicStyle);
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x00061668 File Offset: 0x0005F868
	private void DrawModPrice(Vector2 leftTop, int price)
	{
		GUI.DrawTexture(new Rect(leftTop.x, leftTop.y, (float)this._mpPriceBack.width, (float)this._mpPriceBack.height), this._mpPriceBack);
		GUI.DrawTexture(new Rect(leftTop.x + 25f, leftTop.y + 1f, (float)this._mpBirdSmall.width, (float)this._mpBirdSmall.height), this._mpBirdSmall);
		GUI.Label(new Rect(leftTop.x, leftTop.y + 3f, 25f, 10f), price.ToString(), CWGUI.p.MastMPPriceStyle);
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x00061728 File Offset: 0x0005F928
	private void DrawCurrentWeaponMods()
	{
		this.ModBar(new Vector2(305f, 332f), ModType.silencer, WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).SilencerAvailable);
		this.ModBar(new Vector2(472f, 332f), ModType.optic, WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).OpticAvailable);
		this.ModBar(new Vector2(305f, 406f), ModType.tactical, WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).TacticalAvailable);
		this.ModBar(new Vector2(472f, 406f), ModType.ammo, WeaponModsStorage.Instance().GetModsByWeaponId((int)this._currentWeapon.type).AmmoTypeAvailable);
		int width = CWGUI.p.MastSaveBtnStyle.normal.background.width;
		int height = CWGUI.p.MastSaveBtnStyle.normal.background.height;
		if (GUI.Button(new Rect(710f, 430f, (float)width, (float)height), Language.Save.ToUpper(), CWGUI.p.MastSaveBtnStyle))
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			int weaponId = (int)this._currentWeapon.type;
			foreach (KeyValuePair<ModType, int> keyValuePair in this._tempWeaponMods)
			{
				MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
				if (!modById.IsCamo)
				{
					string key = modById.WeaponSpecificInfo[this._wid].Level + "_" + modById.WeaponSpecificInfo[this._wid].Index;
					dictionary.Add(key, modById.Id);
				}
			}
			dictionary.Add("camo", this._tempWeaponMods[ModType.camo]);
			string text = ArrayUtility.ToJSON<string, object>(dictionary);
			EventFactory.Call("ShowPopup", new Popup(WindowsID.Load, Language.MasteringPopupSaveModHeader, Language.MasteringPopupSaveModProcess, PopupState.progress, false, false, string.Empty, string.Empty));
			Main.AddDatabaseRequestCallBack<MasteringSaveSuits>(delegate
			{
				Audio.Play(this.ModSaveAudioClip);
				EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.MasteringPopupSaveModHeader, Language.MasteringPopupSaveModComplete, PopupState.information, false, false, string.Empty, string.Empty));
				Main.UserInfo.weaponsStates[weaponId].CurrentWeapon.ApplyModsEffect(this._tempWeaponMods);
				this.CurrentSuit.CurrentWeaponsMods[this._wid].Mods = new Dictionary<ModType, int>(this._tempWeaponMods);
				if (this._weaponInfo.repair_info != (float)this._currentWeapon.durability)
				{
					if (this._currentWeapon.weaponUseType == WeaponUseType.Primary)
					{
						Main.UserInfo.suits[Main.UserInfo.suitNameIndex].primaryIndex = (int)this._currentWeapon.type;
					}
					else
					{
						Main.UserInfo.suits[Main.UserInfo.suitNameIndex].secondaryIndex = (int)this._currentWeapon.type;
					}
					Main.AddDatabaseRequest<SaveProfile>(new object[0]);
				}
				this.CloseMastering();
			}, delegate
			{
				EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.MasteringPopupSaveModHeader, Language.MasteringPopupSaveModComplete, PopupState.information, false, false, string.Empty, string.Empty));
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.Error, Language.MasteringPopupSaveModError, PopupState.information, true, true, string.Empty, string.Empty));
			}, new object[]
			{
				Main.UserInfo.suitNameIndex,
				text,
				weaponId
			});
		}
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x000619F4 File Offset: 0x0005FBF4
	private void DrawMetaLevelBar(int metaLevelIndex, Vector2 pos, int metaExp, bool metaLocked, bool metaInProgress, bool wtaskUnlocked = false, bool hiddenMetaLevel = false)
	{
		if (hiddenMetaLevel)
		{
			return;
		}
		int type = (int)this._currentWeapon.type;
		MasteringMetaLevel masteringMetaLevel = WeaponModsStorage.Instance().GetModsByWeaponId(type).MetaLevels[metaLevelIndex];
		List<MasteringMod> mods = masteringMetaLevel.Mods;
		bool flag = metaLevelIndex == 1;
		if (metaLocked)
		{
			GUI.DrawTexture(new Rect(pos.x, pos.y, (float)this._modBackRed.width, (float)this._modBackRed.height), this._modBackRed);
			string text = Helpers.SeparateNumericString(masteringMetaLevel.ExpToUnlock.ToString("F0"));
			if (metaInProgress || (this._currentWeapon.isPremium && !wtaskUnlocked && flag))
			{
				string text2 = Helpers.SeparateNumericString(metaExp.ToString("F0")) + " / " + text;
				if (flag)
				{
					text = Helpers.SeparateNumericString(Main.UserInfo.weaponsStates[type].CurrentWeapon.wtask.count.ToString("F0"));
					float num = Main.UserInfo.weaponsStates[type].wtaskProgress;
					text2 = Helpers.SeparateNumericString(Main.UserInfo.weaponsStates[type].wtaskCurrent.ToString("F0")) + " / " + text;
					if (this._currentWeapon.isPremium)
					{
						text2 = string.Empty;
					}
					GUI.DrawTexture(new Rect(pos.x + 2f, pos.y + 54f, (float)(this._expBarBack.width + 26), (float)this._expBarBack.height), this._expBarBack);
					GUI.DrawTexture(new Rect(pos.x + 2f, pos.y + 54f, (float)(this._expBarBack.width + 26) * num, (float)this._expBarBack.height), this._expBar);
					if (!this._currentWeapon.isPremium)
					{
						Rect rect = new Rect(pos.x, pos.y, (float)this._modBackRed.width, (float)this._modBackRed.height);
						this._showWtaskHint = rect.Contains(Event.current.mousePosition);
					}
				}
				else
				{
					float num = (float)metaExp / (float)masteringMetaLevel.ExpToUnlock;
					GUI.DrawTexture(new Rect(pos.x + 28f, pos.y + 54f, (float)this._expBarBack.width, (float)this._expBarBack.height), this._expBarBack);
					GUI.DrawTexture(new Rect(pos.x + 28f, pos.y + 54f, (float)this._expBarBack.width * num, (float)this._expBarBack.height), this._expBar);
					int width = CWGUI.p.MastUnlockBtnStyle.normal.background.width;
					int height = CWGUI.p.MastUnlockBtnStyle.normal.background.height;
					if (GUI.Button(new Rect(pos.x + 15f, pos.y + 30f, (float)width, (float)height), string.Empty, CWGUI.p.MastUnlockBtnStyle))
					{
						object[] args = new object[]
						{
							(int)this._currentWeapon.type,
							masteringMetaLevel
						};
						EventFactory.Call("ShowPopup", new Popup(WindowsID.BuyMod, Language.MasteringPopupMetaBuyHeader, Language.MasteringPopupMetaBuyBody, delegate()
						{
							EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.MasteringPopupMetaBuyHeader, Language.MasteringPopupMetaBuyComplete, PopupState.information, false, false, string.Empty, string.Empty));
							Audio.Play(MainGUI.Instance.buy_sound);
						}, PopupState.buyMasteringMetaLevel, false, true, args));
					}
				}
				GUI.Label(new Rect(pos.x + (float)this._expIcon.width, pos.y + 54f, (float)this._expBarBack.width, 9f), text2, CWGUI.p.MastProgressBarLabelStyle);
			}
			else
			{
				if (!flag)
				{
					GUI.DrawTexture(new Rect(pos.x + 15f, pos.y + 24f, (float)this._modLockedIcon.width, (float)this._modLockedIcon.height), this._modLockedIcon);
					this._tmpWeaponExp -= masteringMetaLevel.ExpToUnlock;
					int num2 = masteringMetaLevel.ExpToUnlock;
					if (this._tmpWeaponExp < 0)
					{
						num2 = masteringMetaLevel.ExpToUnlock + this._tmpWeaponExp;
					}
					if (!this._currentWeapon.isPremium && !wtaskUnlocked && num2 > 0)
					{
						text = Helpers.SeparateNumericString(num2.ToString()) + " / " + Helpers.SeparateNumericString(masteringMetaLevel.ExpToUnlock.ToString());
					}
				}
				GUI.Label(new Rect(pos.x + (float)this._expIcon.width, pos.y + 54f, (float)this._expBarBack.width, 9f), text, CWGUI.p.MastProgressBarLabelStyle);
			}
			if (!flag)
			{
				GUI.DrawTexture(new Rect(pos.x + 2f, pos.y + 54f, (float)this._expIcon.width, (float)this._expIcon.height), this._expIcon);
			}
		}
		else
		{
			GUI.DrawTexture(new Rect(pos.x, pos.y, (float)this._modBackGray.width, (float)this._modBackGray.height), this._modBackGray);
		}
		if (mods != null)
		{
			for (int i = 0; i < mods.Count; i++)
			{
				if (mods[i] != null)
				{
					Texture2D texture2D = null;
					Texture2D texture2D2 = null;
					WeaponSpecificModInfo weaponSpecificModInfo = mods[i].WeaponSpecificInfo[type];
					bool flag2 = !Main.UserInfo.Mastering.WeaponsStats[type].ModUnlocked(metaLevelIndex - 1, i + 1);
					bool flag3 = !Main.UserInfo.Mastering.WeaponsStats[type].MetaUnlocked(weaponSpecificModInfo.ParentLevel);
					if (!mods[i].IsCamo)
					{
						int indexFromType = this.GetIndexFromType(mods[i].Type);
						texture2D = this.ModTypeBorders[indexFromType];
						texture2D2 = this.ModTypeIcons[indexFromType];
					}
					if (weaponSpecificModInfo.ParentLevel > -1 && weaponSpecificModInfo.ParentIndex > 0)
					{
						int num3 = i;
						int num4 = mods[i].WeaponSpecificInfo[type].ParentIndex - 1;
						int parentLevel = mods[i].WeaponSpecificInfo[type].ParentLevel;
						int level = mods[i].WeaponSpecificInfo[type].Level;
						float num5 = pos.x + 50f + (float)this._modEmpty.width * 0.5f + (float)(i * (this._modEmpty.width + 7));
						if (level != parentLevel)
						{
							float num6 = 28f;
							float num7 = pos.y + 4f - num6;
							if (metaLocked)
							{
								if (Mathf.Abs(level - parentLevel) > 1)
								{
									num6 = (float)(28 + 72 * (Mathf.Abs(level - parentLevel) - 1));
									num7 = pos.y + 4f - num6;
									if (!flag3)
									{
										GUI.DrawTexture(new Rect(num5, num7, 2f, 76f), this._grayScrollFill);
									}
									else
									{
										GUI.DrawTexture(new Rect(num5, num7, 2f, 4f), this._grayScrollFill);
									}
									for (int j = 0; j < Mathf.Abs(level - parentLevel) - 1; j++)
									{
										bool flag4 = Main.UserInfo.Mastering.WeaponsStats[(int)this._currentWeapon.type].MetaUnlocked(parentLevel + j + 1);
										GUI.DrawTexture(new Rect(num5, num7 + 18f + (float)(72 * j), 2f, (float)((!flag4) ? 58 : 72)), this._grayScrollFill);
									}
									GUI.DrawTexture(new Rect(num5, pos.y + 4f - 10f, 2f, 10f), this._grayScrollFill);
								}
								else
								{
									GUI.DrawTexture(new Rect(num5, num7, 2f, (float)((!flag3) ? 28 : 4)), this._grayScrollFill);
									if (flag3)
									{
										GUI.DrawTexture(new Rect(num5, num7 + 18f, 2f, 10f), this._grayScrollFill);
									}
								}
							}
							else
							{
								if (Mathf.Abs(level - parentLevel) > 1)
								{
									num6 = (float)(28 + 72 * (Mathf.Abs(level - parentLevel) - 1));
									num7 = pos.y + 4f - num6;
								}
								GUI.DrawTexture(new Rect(num5, num7, 2f, num6), this._grayScrollFill);
							}
							if (num3 != num4)
							{
								float top = num7 - 22f;
								num6 = (float)(33 * (num4 - num3));
								bool flag5 = num3 < num4;
								GUI.DrawTexture(new Rect(num5, top, 2f, 22f), this._grayScrollFill);
								if (flag5)
								{
									GUI.DrawTexture(new Rect(num5, top, num6 * (float)(num4 - num3), 2f), this._grayScrollFill);
								}
								else
								{
									num6 = (float)(33 * (num3 - num4)) + 22f;
									GUI.DrawTexture(new Rect(num5 - num6, top, num6, 2f), this._grayScrollFill);
								}
							}
						}
						else
						{
							float num8 = num5 + 22f;
							int num9 = 11;
							if (num3 < num4)
							{
								if (Mathf.Abs(num3 - num4) > 1)
								{
									num9 = 44 * (Mathf.Abs(num3 - num4) - 1) + 11 * Mathf.Abs(num3 - num4);
								}
								GUI.DrawTexture(new Rect(num8, pos.y + 4f + 22f, (float)num9, 2f), this._grayScrollFill);
							}
							else
							{
								num8 = num5 - 22f;
								if (Mathf.Abs(num3 - num4) > 1)
								{
									num9 = 44 * (Mathf.Abs(num3 - num4) - 1) + 11 * Mathf.Abs(num3 - num4);
								}
								num8 -= (float)num9;
								GUI.DrawTexture(new Rect(num8, pos.y + 4f + 22f, (float)num9, 2f), this._grayScrollFill);
							}
						}
					}
					float num10 = pos.x + 50f + (float)(i * (this._modEmpty.width + 7));
					GUI.DrawTexture(new Rect(num10, pos.y + 3f, (float)this._modEmpty.width, (float)this._modEmpty.height), this._modEmpty);
					if (mods[i].BigIcon != null)
					{
						Texture2D bigIcon = mods[i].BigIcon;
						float left = num10 + (float)(this._modEmpty.width - bigIcon.width) * 0.5f;
						float top2 = pos.y + (float)(this._modEmpty.height - bigIcon.height) * 0.5f + 1f;
						GUI.DrawTexture(new Rect(left, top2, (float)bigIcon.width, (float)bigIcon.height), bigIcon);
					}
					if (!mods[i].IsCamo)
					{
						GUI.DrawTexture(new Rect(num10 + 3f, pos.y + 5f, (float)texture2D2.width, (float)texture2D2.height), texture2D2);
					}
					if (flag2)
					{
						GUI.DrawTexture(new Rect(num10 - 1f, pos.y + 2f, (float)this._fade.width, (float)this._fade.height), this._fade);
						GUI.DrawTexture(new Rect(num10 + 28f, pos.y + 26f, (float)this._modLockedIcon.width, (float)this._modLockedIcon.height), this._modLockedIcon);
						Rect rect2 = new Rect(num10, pos.y, (float)this._modEmpty.width, (float)this._modEmpty.height);
						if (rect2.Contains(Event.current.mousePosition) && mods[i].WeaponSpecificInfo[type].Mp > 0)
						{
							this.DrawModPrice(new Vector2(num10 + 2f, pos.y - 11f), mods[i].WeaponSpecificInfo[type].Mp);
						}
					}
					if (this._selectedMod == mods[i])
					{
						GUI.DrawTexture(new Rect(num10 - 35f, pos.y - 34f, (float)this._modSelectedBorder.width, (float)this._modSelectedBorder.height), this._modSelectedBorder);
					}
					else if (!mods[i].IsCamo)
					{
						GUI.DrawTexture(new Rect(num10 + 1f, pos.y + 3f, (float)texture2D.width, (float)texture2D.height), texture2D);
					}
					if (this._tempWeaponMods.ContainsKey(mods[i].Type) && this._tempWeaponMods[mods[i].Type] == mods[i].Id)
					{
						GUI.DrawTexture(new Rect(num10 - 1f, pos.y + 2f, (float)this._modInstalledBorder.width, (float)this._modInstalledBorder.height), this._modInstalledBorder);
					}
					if (GUI.Button(new Rect(num10, pos.y + 3f, (float)this._modEmpty.width, (float)this._modEmpty.height), string.Empty, CWGUI.p.MastModBtnStyle) && this._loadingModId == -1 && Event.current.button == 0)
					{
						Audio.Play(this.ModSelectAudioClip);
						this._selectedMod = mods[i];
						if (Main.UserInfo.Mastering.WeaponsStats[(int)this._currentWeapon.type].ModUnlocked(this._selectedMod.WeaponSpecificInfo[type].Level, this._selectedMod.WeaponSpecificInfo[type].Index))
						{
							if (this._selectedMod.Prefab == null && !this._selectedMod.IsBasic && !this._selectedMod.IsCamo && !this._selectedMod.IsAmmo)
							{
								this._loadingModId = mods[i].Id;
								Loader.DownloadModLod(mods[i], delegate
								{
									if (!this.OnModSelect(this._selectedMod) || this._selectedMod == null)
									{
										return;
									}
									Audio.Play(this.ModInstallAudioClip);
									this._currentWeapon.LoadTable(Globals.I.weapons[this._wid]);
									this.UpdateCharacteristics();
								});
							}
							else
							{
								this.SetMod(mods[i]);
								Audio.Play(this.ModInstallAudioClip);
								this._currentWeapon.LoadTable(Globals.I.weapons[this._wid]);
								this.UpdateCharacteristics();
							}
						}
					}
					if (mods[i].Id == this._loadingModId || mods[i].BigIcon == null)
					{
						Rect position = new Rect(num10 + 12f, pos.y + 14f, (float)this._krutilka.width, (float)this._krutilka.height);
						GUIUtility.RotateAroundPivot(180f * Time.time, position.center);
						GUI.DrawTexture(position, this._krutilka);
						GUIUtility.RotateAroundPivot(-180f * Time.time, position.center);
					}
				}
			}
		}
		if (!flag)
		{
			if (metaLevelIndex > 1)
			{
				metaLevelIndex--;
			}
			string text3 = (metaLevelIndex >= 10) ? metaLevelIndex.ToString() : ("0" + metaLevelIndex);
			GUI.Label(new Rect(pos.x + 10f, pos.y + 15f, 30f, 14f), text3, (!metaLocked || metaInProgress) ? CWGUI.p.MastUnlockedMetaLevelIndexStyle : CWGUI.p.MastLockedMetaLevelIndexStyle);
		}
		else
		{
			if (Main.UserInfo.Mastering.WeaponsStats[type].WtaskMetaUnlocked || this._currentWeapon.isPremium)
			{
				GUI.DrawTexture(new Rect(pos.x + 6f, pos.y + 4f, (float)this._wTask.width, (float)this._wTask.height), this._wTask);
			}
			else if (GUI.Button(new Rect(pos.x + 6f, pos.y + 4f, (float)this._wTask.width, (float)this._wTask.height), string.Empty, CWGUI.p.MastWtaskBtnStyle))
			{
				BuyWtask.unlockWtaskCached = type;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.KitUnlock, Language.ThePurchaseOfModification + Main.UserInfo.weaponsStates[type].CurrentWeapon.WtaskName, "qq", PopupState.wtaskUnlock, false, true, string.Empty, string.Empty));
			}
			int wtaskProgress = Main.UserInfo.weaponsStates[(int)this._currentWeapon.type].wtaskProgress100;
			GUI.Label(new Rect(pos.x + 6f, pos.y + 37f, (float)this._wTask.width, 10f), wtaskProgress + "%", CWGUI.p.MastWTaskProgressStyle);
		}
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x00062C28 File Offset: 0x00060E28
	private void ModBar(Vector2 pos, ModType type, bool availableToInstall)
	{
		this._modTypeNames = new string[]
		{
			Language.Sight,
			Language.MuzzleDevice,
			Language.TacticalDevice,
			Language.AmmoType
		};
		int indexFromType = this.GetIndexFromType(type);
		Texture2D image = this.ModTypeSubstrates[indexFromType];
		Texture2D texture2D = this.ModTypeIcons[indexFromType];
		string text = this._modTypeNames[indexFromType];
		GUIStyle mastCurrentModsHeaderStyle = CWGUI.p.MastCurrentModsHeaderStyle;
		GUIStyle mastCurrentModsNameStyle = CWGUI.p.MastCurrentModsNameStyle;
		GUI.DrawTexture(new Rect(pos.x, pos.y - 15f, (float)texture2D.width, (float)texture2D.height), texture2D);
		GUI.Label(new Rect(pos.x + 15f, pos.y - 17f, 128f, (float)mastCurrentModsHeaderStyle.fontSize), text, mastCurrentModsHeaderStyle);
		GUI.DrawTexture(new Rect(pos.x, pos.y, 162f, 56f), image);
		GUI.DrawTexture(new Rect(pos.x + 4f, pos.y + 4f, (float)this._modEmpty.width, (float)this._modEmpty.height), this._modEmpty);
		if (!availableToInstall)
		{
			GUI.DrawTexture(new Rect(pos.x + 6f, pos.y + 5f, (float)this._modUnavailable.width, (float)this._modUnavailable.height), this._modUnavailable);
			GUI.Label(new Rect(pos.x + (float)this._modEmpty.width + 6f, pos.y + 6f, 128f, (float)mastCurrentModsNameStyle.fontSize), "<color=red>" + Language.ModSlotUnavailable + "</color>", mastCurrentModsNameStyle);
		}
		else
		{
			int id = (!this._tempWeaponMods.ContainsKey(type)) ? 0 : this._tempWeaponMods[type];
			MasteringMod modById = ModsStorage.Instance().GetModById(id);
			if (modById == null)
			{
				return;
			}
			if (modById.BigIcon != null)
			{
				float left = pos.x + 4f + (float)(this._modEmpty.width - modById.BigIcon.width) * 0.5f;
				float top = pos.y + 4f + (float)(this._modEmpty.height - modById.BigIcon.height) * 0.5f;
				GUI.DrawTexture(new Rect(left, top, (float)modById.BigIcon.width, (float)modById.BigIcon.height), modById.BigIcon);
			}
			else
			{
				Rect position = new Rect(pos.x + 16f, pos.y + 15f, (float)this._krutilka.width, (float)this._krutilka.height);
				GUIUtility.RotateAroundPivot(180f * Time.time, position.center);
				GUI.DrawTexture(position, this._krutilka);
				GUIUtility.RotateAroundPivot(-180f * Time.time, position.center);
			}
			GUI.Label(new Rect(pos.x + (float)this._modEmpty.width + 6f, pos.y + 6f, 128f, (float)mastCurrentModsNameStyle.fontSize), modById.ShortName, mastCurrentModsNameStyle);
			if (modById.ShotGrouping.Val > 0f)
			{
				string text2 = (modById.ShotGrouping.Val <= 1f) ? string.Concat(new string[]
				{
					"<color=",
					Colors.RadarGreenWeb,
					">-",
					(100f - modById.ShotGrouping.Val * 100f).ToString("F0"),
					"%</color>"
				}) : string.Concat(new string[]
				{
					"<color=",
					Colors.RadarRedWeb,
					">+",
					(modById.ShotGrouping.Val * 100f - 100f).ToString("F0"),
					"%</color>"
				});
				this._TmodEffects = new string[]
				{
					modById.Accuracy.StrVal,
					modById.Recoil.StrVal,
					modById.Damage.StrVal,
					modById.Penetration.StrVal,
					modById.Mobility.StrVal,
					modById.EffectiveDistance.StrVal,
					modById.HearDistance.StrVal,
					text2
				};
			}
			else
			{
				this._TmodEffects = new string[]
				{
					modById.Accuracy.StrVal,
					modById.Recoil.StrVal,
					modById.Damage.StrVal,
					modById.Penetration.StrVal,
					modById.Mobility.StrVal,
					modById.EffectiveDistance.StrVal,
					modById.HearDistance.StrVal
				};
			}
			this._TmodEffectsToDraw = new string[this._TmodEffects.Length];
			this._TmodEffectsNameToDraw = new string[this._TmodEffects.Length];
			this._TeffectIcon = new Texture2D[this._TmodEffects.Length];
			int num = 0;
			for (int i = 0; i < this._TmodEffects.Length; i++)
			{
				if (!string.IsNullOrEmpty(this._TmodEffects[i]))
				{
					if (num > 4)
					{
						break;
					}
					this._TmodEffectsToDraw[num] = this._TmodEffects[i];
					this._TmodEffectsNameToDraw[num] = this._effectsLabels[i];
					this._TeffectIcon[num] = this.ModEffectIcons[i];
					num++;
				}
			}
			float num2 = 5f;
			for (int j = 0; j < this._TmodEffectsToDraw.Length; j++)
			{
				if (!string.IsNullOrEmpty(this._TmodEffectsToDraw[j]))
				{
					if (j == 2)
					{
						num2 = 5f;
					}
					Texture2D texture2D2 = this._TeffectIcon[j];
					string text3 = this._TmodEffectsToDraw[j];
					float num3 = pos.y + (float)((j <= 1) ? 25 : 40);
					GUI.DrawTexture(new Rect(pos.x + (float)this._modEmpty.width + num2, num3 + (float)(9 - texture2D2.height) * 0.5f, (float)texture2D2.width, (float)texture2D2.height), texture2D2);
					num2 += (float)(texture2D2.width + 3);
					bool flag = texture2D2 == this.ModEffectIcons[1] || texture2D2 == this.ModEffectIcons[6];
					string text4 = ((!text3.Contains("-")) ? ("<color=" + ((!flag) ? Colors.RadarGreenWeb : Colors.RadarRedWeb) + ">+" + text3) : ("<color=" + ((!flag) ? Colors.RadarRedWeb : Colors.RadarGreenWeb) + ">" + text3)) + "</color>";
					GUI.Label(new Rect(pos.x + (float)this._modEmpty.width + num2, num3, 50f, 9f), text4, CWGUI.p.MastCharacteristicStyle);
					num2 += MainGUI.Instance.CalcWidth((!text3.Contains("-")) ? ("+" + text3) : text3, CWGUI.p.MastCharacteristicStyle.font, CWGUI.p.MastCharacteristicStyle.fontSize);
				}
			}
		}
	}

	// Token: 0x06000972 RID: 2418 RVA: 0x0006341C File Offset: 0x0006161C
	private int GetIndexFromType(ModType type)
	{
		return (int)((type <= ModType.camo) ? type : (type - 1));
	}

	// Token: 0x06000973 RID: 2419 RVA: 0x00063440 File Offset: 0x00061640
	private void CamouflageBar(Vector2 pos, float zoomWidth)
	{
		int count = Main.UserInfo.Mastering.WeaponsStats[this._wid].Camouflages.Count;
		if (count < 2)
		{
			return;
		}
		int num = 40;
		bool flag = false;
		this._camoScrollOuterRect.Set(pos.x, pos.y - (float)num, zoomWidth, this._camoScrollOuterRect.height);
		this._camoScrollInnerRect.Set(pos.x, pos.y, (float)count * 30.1f, this._camoScrollInnerRect.height);
		Rect position = new Rect(this._camoScrollOuterRect.x, this._camoScrollOuterRect.y + (float)num, this._camoScrollOuterRect.width, this._camoScrollOuterRect.height - (float)num);
		GUI.DrawTexture(position, this._grayBack);
		this._mouseInCamoRect = position.Contains(Event.current.mousePosition);
		if (this._mouseInCamoRect)
		{
			Vector2 mousePosition = Event.current.mousePosition;
			float num2 = Mathf.Abs(pos.x + this._camoScrollOuterRect.width * 0.5f - mousePosition.x);
			if (mousePosition.x < pos.x + this._camoScrollOuterRect.width * 0.5f)
			{
				this._camoScrollPosition.x = this._camoScrollPosition.x - Time.deltaTime * num2;
			}
			else
			{
				this._camoScrollPosition.x = this._camoScrollPosition.x + Time.deltaTime * num2;
				flag = true;
			}
		}
		this._camoScrollPosition = GUI.BeginScrollView(this._camoScrollOuterRect, this._camoScrollPosition, this._camoScrollInnerRect, GUIStyle.none, GUIStyle.none);
		for (int i = 0; i < Main.UserInfo.Mastering.WeaponsStats[this._wid].Camouflages.Count; i++)
		{
			int num3 = Main.UserInfo.Mastering.WeaponsStats[this._wid].Camouflages[i];
			MasteringMod modById = ModsStorage.Instance().GetModById(num3);
			Texture2D texture2D = (!modById.IsBasic) ? modById.SmallIcon : this.BaseCamouflageIcon;
			Texture2D camouflageSelected = this.CamouflageSelected;
			Texture2D rarityBorder = this.RarityBorder;
			this._camoBtnStyle.normal.background = texture2D;
			if (texture2D != null)
			{
				Rect position2 = new Rect(pos.x + 5f + (float)(i * (texture2D.width + 5)), pos.y + (float)num + 4f, (float)texture2D.width, (float)texture2D.height);
				if (GUI.Button(position2, string.Empty, this._camoBtnStyle))
				{
					this.SetMod(modById);
					this._selectedMod = null;
					Audio.Play(this.Paint);
				}
				if (num3 == this._tempWeaponMods[ModType.camo])
				{
					GUI.DrawTexture(new Rect(pos.x + 3f + (float)(i * (texture2D.width + 5)), pos.y + (float)num + 2f, (float)camouflageSelected.width, (float)camouflageSelected.height), camouflageSelected);
				}
				else
				{
					string key = modById.Rarity.ToString().ToLower();
					Color color = this.gui.color;
					this.gui.color = Helpers.HexToColor(RarityColors.Colors[key].ToString().Substring(1));
					GUI.DrawTexture(new Rect(pos.x + 2f + (float)(i * (texture2D.width + 5)), pos.y + (float)num + 1f, (float)rarityBorder.width, (float)rarityBorder.height), rarityBorder);
					this.gui.color = color;
				}
				if (position2.Contains(Event.current.mousePosition))
				{
					float num4 = (!flag) ? position2.x : (position2.x - 144f + (float)texture2D.width);
					Rect position3 = new Rect(num4, position2.y - 36f, 144f, 32f);
					GUIStyle guistyle = new GUIStyle(CWGUI.p.standartTahoma9);
					string text = modById.Rarity.ToString().ToLower();
					GUI.DrawTexture(position3, MainGUI.Instance.black);
					position3.Set(num4 + 4f, position2.y - 34f, 136f, 28f);
					guistyle.alignment = TextAnchor.UpperLeft;
					guistyle.normal.textColor = Color.white;
					GUI.Label(position3, modById.ShortName, guistyle);
					guistyle.alignment = TextAnchor.UpperRight;
					guistyle.normal.textColor = Helpers.HexToColor(RarityColors.Colors[text].ToString().Substring(1));
					GUI.Label(position3, text, guistyle);
					guistyle.alignment = TextAnchor.LowerLeft;
					guistyle.normal.textColor = Color.gray;
					GUI.Label(position3, modById.FullName, guistyle);
				}
			}
		}
		GUI.EndScrollView();
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x00063978 File Offset: 0x00061B78
	private void SetMod(MasteringMod mod)
	{
		if (mod == null)
		{
			return;
		}
		if (mod.IsCamo)
		{
			this.SetCamo(mod);
			return;
		}
		if (mod.IsAmmo)
		{
			this._tempWeaponMods[ModType.ammo] = mod.Id;
			return;
		}
		Transform child = GameObject.Find("GUI/wv_weapon").transform.GetChild(0);
		Transform weaponRoot = GameObject.Find("GUI/wv_weapon").transform.GetChild(0).Find("Weapons_Root");
		this.RemoveOldMod(mod, child);
		this.InstallNewMod(mod, weaponRoot);
		MasteringGUI.DisableEmptyMounts(weaponRoot);
		this.DisableDisabledObjects(child);
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x00063A10 File Offset: 0x00061C10
	private void DisableDisabledObjects(Transform root)
	{
		foreach (KeyValuePair<ModType, int> keyValuePair in this._tempWeaponMods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (!modById.IsCamo)
			{
				foreach (string name in modById.WeaponSpecificInfo[this._wid].DisabledGameObjects)
				{
					Transform transform = Utility.FindHierarchy(root, name);
					transform.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x06000976 RID: 2422 RVA: 0x00063AE0 File Offset: 0x00061CE0
	private static void DisableEmptyMounts(Transform weaponRoot)
	{
		foreach (object obj in weaponRoot)
		{
			Transform transform = (Transform)obj;
			if (transform.name.Contains("mount"))
			{
				bool active = false;
				foreach (object obj2 in transform)
				{
					Transform transform2 = (Transform)obj2;
					if (transform2.childCount != 0)
					{
						active = true;
					}
				}
				transform.gameObject.SetActive(active);
			}
		}
	}

	// Token: 0x06000977 RID: 2423 RVA: 0x00063BD4 File Offset: 0x00061DD4
	private void InstallNewMod(MasteringMod mod, Transform weaponRoot)
	{
		Transform parent;
		if (!string.IsNullOrEmpty(mod.WeaponSpecificInfo[this._wid].Device) && !mod.IsBasic)
		{
			Transform transform = weaponRoot.Find(mod.WeaponSpecificInfo[this._wid].Device);
			transform.gameObject.SetActive(true);
			parent = transform.Find(mod.Type.ToString()).transform;
		}
		else
		{
			parent = weaponRoot.Find(mod.Type.ToString());
		}
		GameObject gameObject = null;
		if (!mod.IsBasic)
		{
			string name = mod.EngShortName + "_prefab_lod";
			gameObject = PoolManager.Spawn(name, mod.Prefab, 4);
			gameObject.transform.parent = parent;
			gameObject.transform.localPosition = mod.WeaponSpecificInfo[this._wid].Shift;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = ((!(mod.WeaponSpecificInfo[this._wid].Scale != Vector3.zero)) ? Vector3.one : mod.WeaponSpecificInfo[this._wid].Scale);
		}
		this._tempWeaponMods[mod.Type] = mod.Id;
		switch (mod.Type)
		{
		case ModType.optic:
			this.cWeapon.OpticMod = gameObject;
			break;
		case ModType.silencer:
			this.cWeapon.SilencerMod = gameObject;
			break;
		case ModType.tactical:
			this.cWeapon.TacticalMod = gameObject;
			break;
		}
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x00063D90 File Offset: 0x00061F90
	private void RemoveOldMod(MasteringMod mod, Transform root)
	{
		MasteringMod masteringMod = null;
		if (this._tempWeaponMods.ContainsKey(mod.Type))
		{
			masteringMod = ModsStorage.Instance().GetModById(this._tempWeaponMods[mod.Type]);
		}
		if (masteringMod == null || masteringMod.IsBasic)
		{
			return;
		}
		string oldModHolder = masteringMod.EngShortName + "_prefab_lod";
		this.DespawnOldMod(mod, oldModHolder);
		this.EnableDisabledObjects(root, masteringMod);
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x00063E04 File Offset: 0x00062004
	private void EnableDisabledObjects(Transform root, MasteringMod oldMod)
	{
		foreach (string name in oldMod.WeaponSpecificInfo[this._wid].DisabledGameObjects)
		{
			Transform transform = Utility.FindHierarchy(root, name);
			transform.gameObject.SetActive(true);
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x00063E54 File Offset: 0x00062054
	private void DespawnOldMod(MasteringMod mod, string oldModHolder)
	{
		switch (mod.Type)
		{
		case ModType.optic:
			PoolManager.Despawn(oldModHolder, this.cWeapon.OpticMod);
			break;
		case ModType.silencer:
			PoolManager.Despawn(oldModHolder, this.cWeapon.SilencerMod);
			break;
		case ModType.tactical:
			PoolManager.Despawn(oldModHolder, this.cWeapon.TacticalMod);
			break;
		}
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00063EC4 File Offset: 0x000620C4
	private void SetCamo(MasteringMod mod)
	{
		this._tempWeaponMods[ModType.camo] = mod.Id;
		Texture texture = mod.Texture;
		GameObject.Find("GUI/wv_weapon").transform.GetChild(0).Find("weapon").GetComponent<MeshRenderer>().materials[0].SetTexture("_DetailTex", texture);
		if (!Main.UserInfo.Mastering.WeaponsStats[this._wid].Camouflages.Contains(mod.Id))
		{
			Main.UserInfo.Mastering.WeaponsStats[this._wid].Camouflages.Add(mod.Id);
			Main.AddDatabaseRequest<MasteringSetCamouflageInfo>(new object[]
			{
				this._wid,
				mod.WeaponSpecificInfo[this._wid].Level,
				mod.WeaponSpecificInfo[this._wid].Index
			});
		}
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x00063FD0 File Offset: 0x000621D0
	private void UpdateCharacteristics()
	{
		this._additionModCharacteristics = new float[8];
		this._multiplicationModCharacteristics = new float[8];
		this._multiplicationModCharacteristics[7] = 1f;
		foreach (KeyValuePair<ModType, int> keyValuePair in this._tempWeaponMods)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			if (modById != null)
			{
				if (modById.Accuracy.Multiplication)
				{
					this._multiplicationModCharacteristics[0] += modById.Accuracy.PercentVal;
				}
				else
				{
					this._additionModCharacteristics[0] += modById.Accuracy.Val;
				}
				if (modById.Recoil.Multiplication)
				{
					this._multiplicationModCharacteristics[1] += modById.Recoil.PercentVal;
				}
				else
				{
					this._additionModCharacteristics[1] += modById.Recoil.Val;
				}
				if (modById.Damage.Multiplication)
				{
					this._multiplicationModCharacteristics[2] += modById.Damage.PercentVal;
				}
				else
				{
					this._additionModCharacteristics[2] += modById.Damage.Val;
				}
				if (modById.Penetration.Multiplication)
				{
					this._multiplicationModCharacteristics[3] += modById.Penetration.PercentVal;
				}
				else
				{
					this._additionModCharacteristics[3] += modById.Penetration.Val;
				}
				if (modById.Mobility.Multiplication)
				{
					this._multiplicationModCharacteristics[4] += modById.Mobility.PercentVal;
				}
				this._additionModCharacteristics[4] += modById.Mobility.Val;
				if (modById.EffectiveDistance.Multiplication)
				{
					this._multiplicationModCharacteristics[5] += modById.EffectiveDistance.PercentVal;
				}
				this._additionModCharacteristics[5] += modById.EffectiveDistance.Val;
				if (modById.HearDistance.Multiplication)
				{
					this._multiplicationModCharacteristics[6] += modById.HearDistance.PercentVal;
				}
				this._additionModCharacteristics[6] += modById.HearDistance.Val;
				if (modById.ShotGrouping.Val > 0f)
				{
					this._multiplicationModCharacteristics[7] += modById.ShotGrouping.Val - 1f;
				}
			}
		}
		this._skillCharacteristics = new float[]
		{
			this._currentWeapon.SkillAccuracyProcBonus,
			this._currentWeapon.SkillRecoilProcBonus,
			this._currentWeapon.SkillDamageProcBonus,
			this._currentWeapon.SkillPierceProcBonus,
			this._currentWeapon.SkillMobilityProcBonus
		};
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x000642F8 File Offset: 0x000624F8
	private void ShowWeaponViewer(Weapons type)
	{
		this._weaponType = type;
		this._weaponMod = false;
		this._weaponName = this._weaponType.ToString();
		base.CancelInvoke();
		this.Clear();
		if (this._cameraObject == null)
		{
			this._cameraObject = SingletoneForm<PoolManager>.Instance["weapon_wv"].Spawn();
			this._cameraObject.transform.parent = base.transform;
			this._cameraObject.GetComponent<Camera>().targetTexture = MainGUI.Instance.masteringWeaponViewer;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		foreach (Light light in componentsInChildren)
		{
			light.enabled = true;
		}
		CameraListener.Enable(this._cameraObject);
		this.isUpdating = true;
		this._stateName = Loader.DownloadLodWV((int)this._weaponType, new StateDownloaderFinishedCallback(this.PrefabDownloaded));
		this._zoomed = false;
		this._weaponOffset = 256f;
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x00064404 File Offset: 0x00062604
	private void HideWeaponViewer(float obj)
	{
		base.Invoke("DelayedHide", obj);
		this.isUpdating = false;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0006441C File Offset: 0x0006261C
	[Obfuscation(Exclude = true)]
	private void DelayedHide()
	{
		this.Clear();
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x00064424 File Offset: 0x00062624
	private void PrintMods(Dictionary<ModType, int> dict)
	{
		foreach (KeyValuePair<ModType, int> keyValuePair in dict)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
			Debug.Log(keyValuePair.Key + " " + modById.EngShortName);
		}
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x000644B4 File Offset: 0x000626B4
	[Obfuscation(Exclude = true)]
	private void PrefabDownloaded()
	{
		Debug.Log("MGUI.PrefabDownloaded");
		PrefabFactory.GetHolderByName("preweapon").LoadAllAsync();
		this._inactivity.Start();
		if (this._weaponObject)
		{
			Utility.SetLayerRecursively(this._weaponObject, 0);
			PoolManager.Despawn(this._weaponObject);
			this._weaponObject = null;
		}
		this._weaponObject = PoolManager.Spawn("wv_weapon");
		this.cWeapon = this._weaponObject.GetComponent<ClientWeapon>();
		this.cWeapon.GetComponent<BaseWeapon>().LOD = true;
		this.cWeapon.state.isMod = this._weaponMod;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(WWWUtil.lodsUrl(this._weaponName));
		GameObject gameObject = PrefabFactory.GetHolderByName(fileNameWithoutExtension).Generate();
		this.cWeapon.Copy(gameObject.GetComponent<BaseWeapon>());
		if (this._tempWeaponMods != null)
		{
			foreach (KeyValuePair<ModType, int> keyValuePair in this._tempWeaponMods)
			{
				MasteringMod modById = ModsStorage.Instance().GetModById(keyValuePair.Value);
				if (modById != null && !modById.IsBasic && !modById.IsCamo && !modById.IsAmmo)
				{
					modById.Prefab = PrefabFactory.GenerateLodModWithoutCreating(modById);
				}
			}
		}
		this.cWeapon.Init(null, (int)this._weaponType);
		this.cWeapon.AfterInit(Main.UserInfo.weaponsStates[(int)this.cWeapon.type].repair_info);
		this.cWeapon.transform.eulerAngles = new Vector3(0f, 0f, 0f);
		Utility.SetLayerRecursively(this._weaponObject, LayerMask.NameToLayer("weaponViewer"));
		this._weaponObject.transform.parent = base.transform;
		Bounds bounds = default(Bounds);
		foreach (Renderer renderer in this.cWeapon.RenderersCurrent)
		{
			bounds.Encapsulate(renderer.bounds);
		}
		this._center = bounds.center;
		if (Main.UserInfo.weaponsStates[(int)this._weaponType].CurrentWeapon.IsPrimary)
		{
			this._min = bounds.size.magnitude * 1.25f;
			this._max = bounds.size.magnitude * 1.75f;
			this._camCurrentRad = bounds.size.magnitude * 1.5f;
			this._camTargetRad = bounds.size.magnitude * 1.5f;
		}
		else
		{
			this._camCurrentRad = bounds.size.magnitude * 1.5f;
			this._camTargetRad = bounds.size.magnitude * 1.5f;
			this._min = bounds.size.magnitude * 1.5f;
			this._max = bounds.size.magnitude * 3f;
		}
		this._camEuler.x = 280f;
		this._camEuler.y = 150f;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x00064860 File Offset: 0x00062A60
	private bool OnModSelect(MasteringMod mod)
	{
		if (mod != null && !mod.IsBasic)
		{
			mod.Prefab = PrefabFactory.GenerateLodModWithoutCreating(mod);
		}
		if (mod != null && mod.Prefab == null)
		{
			this._loadingModId = -1;
			return false;
		}
		this.SetMod(mod);
		this._loadingModId = -1;
		return true;
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x000648BC File Offset: 0x00062ABC
	public override void Clear()
	{
		base.Clear();
		if (this._cameraObject)
		{
			CameraListener.Disable(this._cameraObject);
			PoolManager.Despawn(this._cameraObject);
			this._cameraObject = null;
		}
		if (this._weaponObject)
		{
			Utility.SetLayerRecursively(this._weaponObject, 0);
			PoolManager.Despawn(this._weaponObject);
			this._weaponObject = null;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00064958 File Offset: 0x00062B58
	public override void OnUpdate()
	{
		if (!base.Visible)
		{
			return;
		}
		if (PopupGUI.IsAnyPopupShow)
		{
			return;
		}
		if (this._mouseInRect && Input.GetMouseButtonDown(0))
		{
			this._clickedInRect = true;
		}
		if (Input.GetMouseButtonUp(0))
		{
			this._clickedInRect = false;
		}
		if (this._mouseInRect)
		{
			if (this._camTargetRad < this._min)
			{
				this._camTargetRad = this._min;
			}
			if (this._camTargetRad > this._max)
			{
				this._camTargetRad = this._max;
			}
			if (this._camCurrentRad > this._camTargetRad)
			{
				this._camCurrentRad = this._camTargetRad;
			}
			if (this._camCurrentRad < this._camTargetRad)
			{
				this._camCurrentRad += Time.deltaTime * 10f;
			}
			this._camTargetRad -= Input.GetAxis("Mouse ScrollWheel") * 100f * Time.deltaTime;
		}
		if (this._clickedInRect && Input.GetMouseButton(0) && !this._mouseInCamoRect)
		{
			if (!this._permanentInactivity)
			{
				this._inactivity.Start();
			}
			float axis = Input.GetAxis("Mouse X");
			float num = Input.GetAxis("Mouse Y");
			if (Main.UserInfo.settings.binds.invertMouse)
			{
				num *= -1f;
			}
			this._camEuler.y = this._camEuler.y + ((!Input.GetKey(KeyCode.LeftShift)) ? 4f : 0.5f) * axis * 270f * Time.deltaTime;
			this._camEuler.x = this._camEuler.x - ((!Input.GetKey(KeyCode.LeftShift)) ? 4f : 0.5f) * num * 270f * Time.deltaTime;
			if (this._camEuler.x > 359.5f)
			{
				this._camEuler.x = 359.5f;
			}
			if (this._camEuler.x < 180.5f)
			{
				this._camEuler.x = 180.5f;
			}
		}
		if (this._inactivity.Elapsed > 5f && !this._permanentInactivity)
		{
			this._camEuler.y = this._camEuler.y + 30f * Time.deltaTime;
		}
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00064BCC File Offset: 0x00062DCC
	public override void OnLateUpdate()
	{
		if (!base.Visible)
		{
			return;
		}
		if (this._cameraObject)
		{
			this._cameraObject.transform.position = this._center + Quaternion.Euler(this._camEuler) * Vector3.up * this._camCurrentRad;
			this._cameraObject.transform.LookAt(this._center);
		}
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x00064C48 File Offset: 0x00062E48
	public override void OnConnected()
	{
		this.Clear();
		base.OnConnected();
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00064C58 File Offset: 0x00062E58
	public void DrawWeaponViewer()
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if ((double)base.visibility > 0.75)
		{
			this._wvRect.Set(0f, 0f, (float)Screen.width, (float)Screen.height);
			this.gui.BeginGroup(this._wvRect, this.windowID != this.gui.FocusedWindow);
			float num = (!this._zoomed) ? this.ZoomInCurve.Evaluate(this.ZoomTimer.Time) : this.ZoomOutCurve.Evaluate(this.ZoomTimer.Time);
			this._weaponOffset = ((!this._zoomed) ? (256f - 1024f * this.ZoomTimer.Time) : (1024f * this.ZoomTimer.Time));
			Vector2 pos = new Vector2((this._weaponOffset + this._wvRect.width - (float)this.gui.masteringWeaponViewer.width * num) * 0.5f, (this._wvRect.height - (float)this.gui.masteringWeaponViewer.height) * 0.5f);
			Vector2 size = new Vector2((float)this.gui.masteringWeaponViewer.width * num, (float)this.gui.masteringWeaponViewer.height * num);
			this.gui.PictureSizedNoBlend(pos, this.gui.masteringWeaponViewer, size, false);
			Rect rect = new Rect(pos.x + 30f, pos.y, size.x, size.y);
			this._mouseInRect = rect.Contains(Event.current.mousePosition);
			if (Loader.Progress(this._stateName) < 1f)
			{
				int num2 = (int)Loader.Progress(this._stateName) * 100;
				Vector2 vector = new Vector2((this._weaponOffset + this._wvRect.width) * 0.5f, (this._wvRect.height - (float)this.gui.masteringWeaponViewer.height * 0.5f) * 0.5f);
				float angle = 180f * Time.realtimeSinceStartup * 1.5f;
				this.gui.TextField(new Rect(this._weaponOffset * 0.5f, -130f, this._wvRect.width, this._wvRect.height), num2 + "%", 11, "#b1b0b1_Micra", TextAnchor.MiddleCenter, false, false);
				this.gui.RotateGUI(angle, new Vector2(vector.x + (float)this._krutilka.width * 0.5f - 10f, vector.y + (float)this._krutilka.height * 0.5f + 10f));
				this.gui.Picture(new Vector2(vector.x - 10f, vector.y + 10f), this._krutilka);
				this.gui.RotateGUI(0f, Vector2.zero);
			}
			float num3 = pos.x + size.x / 2f + 352f * num;
			float num4 = pos.y + size.y - 64f * num;
			float width = (float)CWGUI.p.MastViewerBtnStyle.normal.background.width * 0.8f;
			float height = (float)CWGUI.p.MastViewerBtnStyle.normal.background.height * 0.8f;
			Color color = this.gui.color;
			if (this._permanentInactivity)
			{
				this.gui.color = Color.red;
			}
			if (GUI.Button(new Rect(num3, num4 - 20f, width, height), string.Empty, CWGUI.p.MastViewerBtnStyle))
			{
				this._permanentInactivity = !this._permanentInactivity;
			}
			this.gui.color = color;
			width = (float)CWGUI.p.MastViewerZoomInBtnStyle.normal.background.width;
			height = (float)CWGUI.p.MastViewerZoomInBtnStyle.normal.background.height;
			if (GUI.Button(new Rect(num3 - 45f, num4 - 30f, width, height), string.Empty, (!this._zoomed) ? CWGUI.p.MastViewerZoomInBtnStyle : CWGUI.p.MastViewerZoomOutBtnStyle) || (Input.GetKeyDown(KeyCode.Space) && !PopupGUI.IsAnyPopupShow && this._weaponObject != null && !this.ZoomTimer.IsStarted))
			{
				this.ZoomTimer.Start();
			}
			if (this.ZoomTimer.Time > 0.25f)
			{
				this._zoomed = !this._zoomed;
				this.ZoomTimer.Stop();
			}
			float num5 = 610f - 610f * num;
			float zoomWidth = this.Rect.width * num + (186f - 186f * num);
			this.CamouflageBar(new Vector2(this.Rect.xMin + num5, num4 + 12f), zoomWidth);
			this.gui.EndGroup();
		}
	}

	// Token: 0x04000AA3 RID: 2723
	public GUISkin MasteringSkin;

	// Token: 0x04000AA4 RID: 2724
	public AnimationCurve ZoomInCurve;

	// Token: 0x04000AA5 RID: 2725
	public AnimationCurve ZoomOutCurve;

	// Token: 0x04000AA6 RID: 2726
	public Timer ZoomTimer = new Timer();

	// Token: 0x04000AA7 RID: 2727
	public Texture2D[] masteringTextures;

	// Token: 0x04000AA8 RID: 2728
	public Texture2D[] weaponViewerTextures;

	// Token: 0x04000AA9 RID: 2729
	public Texture2D[] ModTypeIcons;

	// Token: 0x04000AAA RID: 2730
	public Texture2D[] ModTypeSubstrates;

	// Token: 0x04000AAB RID: 2731
	public Texture2D[] ModTypeBorders;

	// Token: 0x04000AAC RID: 2732
	public Texture2D[] ModEffectIcons;

	// Token: 0x04000AAD RID: 2733
	public Texture2D BaseCamouflageIcon;

	// Token: 0x04000AAE RID: 2734
	public Texture2D CamouflageSelected;

	// Token: 0x04000AAF RID: 2735
	public Texture2D RarityBorder;

	// Token: 0x04000AB0 RID: 2736
	public Texture2D Unknown;

	// Token: 0x04000AB1 RID: 2737
	public Texture2D KillsIcon;

	// Token: 0x04000AB2 RID: 2738
	public AudioClip ModBuyAudioClip;

	// Token: 0x04000AB3 RID: 2739
	public AudioClip ModInstallAudioClip;

	// Token: 0x04000AB4 RID: 2740
	public AudioClip ModSaveAudioClip;

	// Token: 0x04000AB5 RID: 2741
	public AudioClip ModSelectAudioClip;

	// Token: 0x04000AB6 RID: 2742
	public AudioClip Paint;

	// Token: 0x04000AB7 RID: 2743
	private int _wid;

	// Token: 0x04000AB8 RID: 2744
	private WeaponInfo _weaponInfo;

	// Token: 0x04000AB9 RID: 2745
	private BaseWeapon _currentWeapon;

	// Token: 0x04000ABA RID: 2746
	private GUISkin _tempSkin;

	// Token: 0x04000ABB RID: 2747
	private MasteringInfo.MasteringWeaponStats _weaponStats;

	// Token: 0x04000ABC RID: 2748
	private Dictionary<ModType, int> _suitWeaponMods;

	// Token: 0x04000ABD RID: 2749
	private Dictionary<ModType, int> _tempWeaponMods;

	// Token: 0x04000ABE RID: 2750
	private bool _zoomed;

	// Token: 0x04000ABF RID: 2751
	private float _masteringVisibility;

	// Token: 0x04000AC0 RID: 2752
	private float _weaponOffset;

	// Token: 0x04000AC1 RID: 2753
	private bool _showWtaskHint;

	// Token: 0x04000AC2 RID: 2754
	private int _tmpWeaponExp;

	// Token: 0x04000AC3 RID: 2755
	private string[] _effectsLabels = new string[]
	{
		Language.Accuracy,
		Language.Impact,
		Language.Damage,
		Language.Penetration,
		Language.Mobility,
		Language.EffectiveDistance,
		Language.HearDistance,
		Language.ShotGrouping
	};

	// Token: 0x04000AC4 RID: 2756
	private float[] _baseCharacteristics;

	// Token: 0x04000AC5 RID: 2757
	private float[] _skillCharacteristics;

	// Token: 0x04000AC6 RID: 2758
	private float[] _additionModCharacteristics;

	// Token: 0x04000AC7 RID: 2759
	private float[] _multiplicationModCharacteristics;

	// Token: 0x04000AC8 RID: 2760
	private Texture2D _charactBarBack;

	// Token: 0x04000AC9 RID: 2761
	private Texture2D _charactBarBlue;

	// Token: 0x04000ACA RID: 2762
	private Texture2D _charactBarFilled;

	// Token: 0x04000ACB RID: 2763
	private Texture2D _charactBarRed;

	// Token: 0x04000ACC RID: 2764
	private Texture2D _charactBarGreen;

	// Token: 0x04000ACD RID: 2765
	private Texture2D _expBar;

	// Token: 0x04000ACE RID: 2766
	private Texture2D _expBarBack;

	// Token: 0x04000ACF RID: 2767
	private Texture2D _modEmpty;

	// Token: 0x04000AD0 RID: 2768
	private Texture2D _modUnavailable;

	// Token: 0x04000AD1 RID: 2769
	private Texture2D _modBackGray;

	// Token: 0x04000AD2 RID: 2770
	private Texture2D _modBackRed;

	// Token: 0x04000AD3 RID: 2771
	private Texture2D _opticBack;

	// Token: 0x04000AD4 RID: 2772
	private Texture2D _wTask;

	// Token: 0x04000AD5 RID: 2773
	private Texture2D _expIcon;

	// Token: 0x04000AD6 RID: 2774
	private Texture2D _mpPriceBack;

	// Token: 0x04000AD7 RID: 2775
	private Texture2D _grayBack;

	// Token: 0x04000AD8 RID: 2776
	private Texture2D _grayScrollFill;

	// Token: 0x04000AD9 RID: 2777
	private Texture2D _mastBigIcon;

	// Token: 0x04000ADA RID: 2778
	private Texture2D _mpBirdSmall;

	// Token: 0x04000ADB RID: 2779
	private Texture2D _mpBackGlow;

	// Token: 0x04000ADC RID: 2780
	private Texture2D _modInstalledBorder;

	// Token: 0x04000ADD RID: 2781
	private Texture2D _modSelectedBorder;

	// Token: 0x04000ADE RID: 2782
	private Texture2D _modLockedIcon;

	// Token: 0x04000ADF RID: 2783
	private Texture2D _smallProgressBarBorder;

	// Token: 0x04000AE0 RID: 2784
	private Texture2D _smallProgressBarFiller;

	// Token: 0x04000AE1 RID: 2785
	private Texture2D _fade;

	// Token: 0x04000AE2 RID: 2786
	private Texture2D _krutilka;

	// Token: 0x04000AE3 RID: 2787
	private Texture2D _wvIcon;

	// Token: 0x04000AE4 RID: 2788
	private Rect _scrollRect1 = new Rect(5f, 29f, 292f, 430f);

	// Token: 0x04000AE5 RID: 2789
	private Rect _scrollRect2 = new Rect(5f, 26f, 270f, 500f);

	// Token: 0x04000AE6 RID: 2790
	private Vector2 _scrollPosition = Vector2.zero;

	// Token: 0x04000AE7 RID: 2791
	private MasteringMod _selectedMod;

	// Token: 0x04000AE8 RID: 2792
	private bool _updateCharacteristics;

	// Token: 0x04000AE9 RID: 2793
	private int _loadingModId = -1;

	// Token: 0x04000AEA RID: 2794
	private string[] _modEffects = new string[7];

	// Token: 0x04000AEB RID: 2795
	private string[] _modTypeNames = new string[]
	{
		Language.Sight,
		Language.MuzzleDevice,
		Language.TacticalDevice
	};

	// Token: 0x04000AEC RID: 2796
	private string[] _TmodEffects = new string[6];

	// Token: 0x04000AED RID: 2797
	private string[] _TmodEffectsToDraw = new string[3];

	// Token: 0x04000AEE RID: 2798
	private string[] _TmodEffectsNameToDraw = new string[3];

	// Token: 0x04000AEF RID: 2799
	private Texture2D[] _TeffectIcon = new Texture2D[3];

	// Token: 0x04000AF0 RID: 2800
	private GUIStyle _camoBtnStyle = new GUIStyle();

	// Token: 0x04000AF1 RID: 2801
	private Rect _camoScrollOuterRect = new Rect(0f, 0f, 493f, 73f);

	// Token: 0x04000AF2 RID: 2802
	private Rect _camoScrollInnerRect = new Rect(0f, 0f, 0f, 33f);

	// Token: 0x04000AF3 RID: 2803
	private Vector2 _camoScrollPosition = Vector2.zero;

	// Token: 0x04000AF4 RID: 2804
	private string _modDeviceName;

	// Token: 0x04000AF5 RID: 2805
	private float _min;

	// Token: 0x04000AF6 RID: 2806
	private float _max;

	// Token: 0x04000AF7 RID: 2807
	private float _camCurrentRad;

	// Token: 0x04000AF8 RID: 2808
	private float _camTargetRad;

	// Token: 0x04000AF9 RID: 2809
	private bool _mouseInRect;

	// Token: 0x04000AFA RID: 2810
	private bool _clickedInRect;

	// Token: 0x04000AFB RID: 2811
	private bool _permanentInactivity = true;

	// Token: 0x04000AFC RID: 2812
	private GameObject _weaponObject;

	// Token: 0x04000AFD RID: 2813
	private GameObject _cameraObject;

	// Token: 0x04000AFE RID: 2814
	private string _weaponName = string.Empty;

	// Token: 0x04000AFF RID: 2815
	private bool _weaponMod;

	// Token: 0x04000B00 RID: 2816
	private Weapons _weaponType = Weapons.none;

	// Token: 0x04000B01 RID: 2817
	private Vector3 _center = Vector3.zero;

	// Token: 0x04000B02 RID: 2818
	private Vector3 _camEuler = new Vector3(280f, -280f, 0f);

	// Token: 0x04000B03 RID: 2819
	private Rect _wvRect = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);

	// Token: 0x04000B04 RID: 2820
	private readonly eTimer _inactivity = new eTimer();

	// Token: 0x04000B05 RID: 2821
	private ClientWeapon cWeapon;

	// Token: 0x04000B06 RID: 2822
	private string _stateName;

	// Token: 0x04000B07 RID: 2823
	private bool _mouseInCamoRect;
}
