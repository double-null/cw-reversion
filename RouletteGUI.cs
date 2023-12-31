using System;
using System.Reflection;
using UnityEngine;

// Token: 0x02000172 RID: 370
[AddComponentMenu("Scripts/GUI/RouletteGUI")]
[Obfuscation(Exclude = true, ApplyToMembers = true)]
internal class RouletteGUI : Form
{
	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0007E734 File Offset: 0x0007C934
	public override Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width - 800) * 0.5f, (float)(Screen.height - 600) * 0.5f, 800f, 600f);
		}
	}

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0007E76C File Offset: 0x0007C96C
	private Vector2 Pivot
	{
		get
		{
			return new Vector2(this.Rect.width * 0.5f, this.Rect.height * 0.5f);
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0007E7A8 File Offset: 0x0007C9A8
	private void RndStartPosition()
	{
		int[] array = new int[12];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i * 30;
		}
		this.StartPosition = (float)array[UnityEngine.Random.Range(0, array.Length)];
		this.angle = this.StartPosition;
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0007E7F8 File Offset: 0x0007C9F8
	private void RndStopPosition(string s)
	{
		int[] array = new int[12];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i * 30;
		}
		if (s == "cr-3")
		{
			this.StopPosition = (float)array[0];
		}
		else if (s == "sp-0")
		{
			this.StopPosition = (float)array[1];
		}
		else if (s == "gp-1")
		{
			this.StopPosition = (float)array[2];
		}
		else if (s == "cr-0")
		{
			this.StopPosition = (float)array[3];
		}
		else if (s == "plusone-0")
		{
			this.StopPosition = (float)array[4];
		}
		else if (s.Contains("skill"))
		{
			this.StopPosition = (float)array[5];
			this.skillID = Convert.ToInt32(s.Substring(6));
			this.skillTexture = CarrierGUI.I.Class_skills[Convert.ToInt32(s.Substring(6))];
		}
		else if (s == "cr-1")
		{
			this.StopPosition = (float)array[6];
		}
		else if (s == "blackDivision-0")
		{
			this.StopPosition = (float)array[7];
			this.bdStr = Globals.I.RouletteInfo.amountBD.ToString();
		}
		else if (s == "gp-0")
		{
			this.StopPosition = (float)array[8];
		}
		else if (s.Contains("weapon"))
		{
			this.StopPosition = (float)array[9];
			this.weaponID = Convert.ToInt32(s.Substring(7));
			this._weaponTexture = MainGUI.Instance.weapon_unlocked[Convert.ToInt32(s.Substring(7))];
		}
		else if (s == "cr-2")
		{
			this.StopPosition = (float)array[10];
		}
		else if (s.Contains("camo"))
		{
			this.StopPosition = (float)array[11];
			this.weaponID = Convert.ToInt32(s.Split(new char[]
			{
				'-'
			})[2]);
			this.camoID = Convert.ToInt32(s.Split(new char[]
			{
				'-'
			})[1]);
			this._weaponTexture = MainGUI.Instance.weapon_unlocked[Convert.ToInt32(s.Split(new char[]
			{
				'-'
			})[2])];
		}
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0007EA78 File Offset: 0x0007CC78
	private void RotatePointer()
	{
		GUIUtility.RotateAroundPivot(this.angle, new Vector2(this.Rect.width * 0.5f, this.Rect.height * 0.5f));
		GUI.BeginGroup(new Rect(0f, 0f, this.Rect.width, this.Rect.height));
		GUI.DrawTexture(new Rect(114f, 106f, (float)this.Roulette_Pointer.width, (float)this.Roulette_Pointer.height), this.Roulette_Pointer);
		GUI.EndGroup();
		GUIUtility.RotateAroundPivot(-this.angle, new Vector2(this.Rect.width * 0.5f, this.Rect.height * 0.5f));
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x0007EB60 File Offset: 0x0007CD60
	private void SetPointer()
	{
		this.distance = 360f * this.rotate;
		if (this.angle < 360f)
		{
			return;
		}
		float startPosition = this.angle - (float)(360 * ((int)this.angle / 360));
		this.angle = startPosition;
		this.StartPosition = startPosition;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x0007EBBC File Offset: 0x0007CDBC
	private void Roll(float _start, float _stop, float _distance, float _velocity)
	{
		if (this.angle >= _distance + _stop - 0.2f)
		{
			this.angle = Mathf.Ceil(_distance + _stop);
			this.isStarted = false;
			this.isResult = true;
			Audio.Play((this.ChangeType() != PrizeType.special) ? this.OnWinSound : this.OnBDSound);
		}
		else
		{
			this.angle += _velocity * Time.deltaTime;
		}
	}

	// Token: 0x06000A86 RID: 2694 RVA: 0x0007EC38 File Offset: 0x0007CE38
	private float Velocity()
	{
		return Mathf.Max(this.distance + this.StopPosition - this.angle, 20f);
	}

	// Token: 0x06000A87 RID: 2695 RVA: 0x0007EC58 File Offset: 0x0007CE58
	private PrizeType ChangeType()
	{
		PrizeType[] array = (!CVars.IsVanilla) ? this.aTypes : this.aTypesVanilla;
		if (this.isResult)
		{
			this.index = (int)this.StopPosition / 30;
		}
		else if (!this.isStarted)
		{
			if (this.angle < 360f)
			{
				this.index = (int)this.StartPosition / 30;
			}
		}
		else
		{
			this.index = ((this.angle >= 360f) ? ((int)(this.angle - (float)(360 * ((int)this.angle / 360))) / 30) : ((int)this.angle / 30)) + 1;
			if (this.index >= array.Length)
			{
				this.index = 0;
			}
			if (this.prevIndex != this.index)
			{
				this.prevIndex = this.index;
				Audio.Play(this.OnRollSound);
				if (this.Velocity() > 180f)
				{
					this._weaponTexture = MainGUI.Instance.weapon_unlocked[UnityEngine.Random.Range(1, MainGUI.Instance.weapon_unlocked.Length - 1)];
					this.skillTexture = CarrierGUI.I.Class_skills[this.skillPrize[UnityEngine.Random.Range(0, this.skillPrize.Length - 1)]];
				}
				else
				{
					if (this.weaponID > 0)
					{
						this._weaponTexture = MainGUI.Instance.weapon_unlocked[this.weaponID];
					}
					if (this.skillID > 0)
					{
						this.skillTexture = CarrierGUI.I.Class_skills[this.skillID];
					}
				}
			}
		}
		this.prizeIndex = this.index;
		return array[this.index];
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0007EE14 File Offset: 0x0007D014
	private void DrawPrize(PrizeType type)
	{
		if (this.noTries)
		{
			GUI.Label(new Rect(0f, 260f, this.Rect.width, 20f), Language.RouletteTriesEnded.ToUpper(), this.LabelRed);
			GUI.Label(new Rect((this.Rect.width - 170f) * 0.5f, 290f, 170f, 20f), Language.RouletteWaitOrBuy, this.LabelGray);
			return;
		}
		if (type == PrizeType.camo)
		{
			MasteringMod modById = ModsStorage.Instance().GetModById(this.camoID);
			Texture2D texture2D = (modById != null) ? modById.BigIcon : this.Roulette_Camo;
			if (this.isResult)
			{
				string str = Main.UserInfo.weaponsStates[this.weaponID].CurrentWeapon.ShortName.ToUpper();
				GUI.Label(new Rect(0f, 210f, this.Rect.width, 20f), Language.RouletteCamo, this.LabelGray);
				GUI.Label(new Rect(0f, 230f, this.Rect.width, 20f), Language.For + " " + str, this.LableWhite);
				if (modById != null)
				{
					GUIStyle guistyle = new GUIStyle(this.LableWhite)
					{
						alignment = TextAnchor.MiddleLeft
					};
					GUI.Label(new Rect(this.Rect.width * 0.5f + 10f, 340f, this.Rect.width, 20f), modById.ShortName, guistyle);
					guistyle.fontSize = 14;
					string text = modById.Rarity.ToString();
					guistyle.normal.textColor = Helpers.HexToColor(RarityColors.Colors[text.ToLower()].ToString().Substring(1));
					GUI.Label(new Rect(this.Rect.width * 0.5f + 10f, 360f, this.Rect.width, 20f), text, guistyle);
				}
				if (texture2D)
				{
					Rect position = new Rect(this.Rect.width * 0.5f - (float)texture2D.width, (this.Rect.height - (float)texture2D.height) * 0.5f + 60f, (float)texture2D.width, (float)texture2D.height);
					GUI.DrawTexture(position, texture2D);
					GUI.DrawTexture(new Rect(position.x - 41f, position.y - 42f, (float)this.Roulette_Camo_Border.width, (float)this.Roulette_Camo_Border.height), this.Roulette_Camo_Border);
				}
				if (this._weaponTexture)
				{
					Rect position2 = new Rect((this.Rect.width - (float)this._weaponTexture.width) * 0.5f, (this.Rect.height - (float)this._weaponTexture.height) * 0.5f - 10f, (float)this._weaponTexture.width, (float)this._weaponTexture.height);
					GUI.DrawTexture(position2, this._weaponTexture);
				}
			}
			else
			{
				Rect position3 = new Rect((this.Rect.width - (float)this.Roulette_Camo.width) * 0.5f, (this.Rect.height - (float)this.Roulette_Camo.height) * 0.5f, (float)this.Roulette_Camo.width, (float)this.Roulette_Camo.height);
				GUI.DrawTexture(position3, this.Roulette_Camo);
			}
		}
		else if (type == PrizeType.skill)
		{
			if (this.isResult)
			{
				string text2 = Main.UserInfo.skillsInfos[this.skillID].name.ToUpper();
				GUI.Label(new Rect(0f, 240f, this.Rect.width, 20f), Language.RouletteWinSkill, this.LabelGray);
				GUI.Label(new Rect(0f, 340f, this.Rect.width, 20f), string.Concat(new object[]
				{
					text2,
					" <color=#727272>",
					Language.For.ToUpper(),
					" ",
					Globals.I.RouletteInfo.SkillRentTerm,
					Language.Hours,
					"</color>"
				}), this.LableWhite);
			}
			GUI.DrawTexture(new Rect((this.Rect.width - (float)this.skillTexture.width) * 0.5f, (this.Rect.height - (float)this.skillTexture.height) * 0.5f, (float)this.skillTexture.width, (float)this.skillTexture.height), this.skillTexture);
		}
		else if (type == PrizeType.weapon)
		{
			if (this.isResult)
			{
				string text3 = Main.UserInfo.weaponsStates[this.weaponID].CurrentWeapon.ShortName.ToUpper();
				GUI.Label(new Rect(0f, 240f, this.Rect.width, 20f), Language.RouletteWinWeapon, this.LabelGray);
				GUI.Label(new Rect(0f, 340f, this.Rect.width, 20f), string.Concat(new object[]
				{
					text3,
					" <color=#727272>",
					Language.For.ToUpper(),
					" ",
					Globals.I.RouletteInfo.WeaponRentTerm,
					Language.Hours,
					"</color>"
				}), this.LableWhite);
			}
			if (this._weaponTexture)
			{
				GUI.DrawTexture(new Rect((this.Rect.width - (float)this._weaponTexture.width) * 0.5f, (this.Rect.height - (float)this._weaponTexture.height) * 0.5f, (float)this._weaponTexture.width, (float)this._weaponTexture.height), this._weaponTexture);
			}
		}
		else if (type == PrizeType.special)
		{
			GUI.DrawTexture(new Rect((this.Rect.width - (float)this.Roulette_SpecBD.width) * 0.5f, (!this.isResult) ? ((this.Rect.height - (float)this.Roulette_SpecBD.height) * 0.5f) : 200f, (float)this.Roulette_SpecBD.width, (float)this.Roulette_SpecBD.height), this.Roulette_SpecBD);
			if (this.isResult)
			{
				if (Globals.I.RouletteInfo.BDCurrency == 2)
				{
					this.prizeTexture = this.Roulette_GP;
				}
				else
				{
					this.prizeTexture = this.Roulette_CR;
				}
				GUI.Label(new Rect(0f, 260f, this.Rect.width, 20f), Language.RouletteWinSpecial, this.LabelGray);
				GUI.Label(new Rect(0f, 280f, this.Rect.width, 40f), Helpers.SeparateNumericString(this.bdStr), this.LabelWhiteKorataki20);
				GUI.DrawTexture(new Rect((this.Rect.width - (float)this.prizeTexture.width) * 0.5f, 320f, (float)this.prizeTexture.width, (float)this.prizeTexture.height), this.prizeTexture);
			}
		}
		else
		{
			if (type == PrizeType.cr)
			{
				int num = (Main.UserInfo.currentLevel <= 0) ? 1 : Main.UserInfo.currentLevel;
				this.prizeTexture = this.Roulette_CR;
				this.prizeText = (this.iArr[this.prizeIndex] * num).ToString();
			}
			else if (type == PrizeType.gp)
			{
				this.prizeTexture = this.Roulette_GP;
				this.prizeText = this.iArr[this.prizeIndex].ToString();
			}
			else if (type == PrizeType.sp)
			{
				this.prizeTexture = this.Roulette_SP;
				this.prizeText = Globals.I.RouletteInfo.amountSP.ToString();
			}
			else if (type == PrizeType.plusone)
			{
				this.prizeTexture = MainGUI.Instance.RouletteIcon;
				this.prizeText = "1";
			}
			else if (type == PrizeType.mp)
			{
				this.prizeTexture = this.MpIconBig;
				this.prizeText = "1";
			}
			else if (type == PrizeType.gpDiscount)
			{
				this.prizeTexture = this.GpDiscountIconBig;
			}
			if (this.isResult && type != PrizeType.gpDiscount)
			{
				GUI.Label(new Rect(0f, 260f, this.Rect.width, 20f), Language.RouletteWin, this.LabelGray);
				if (Main.UserInfo.WonAttempt)
				{
					Main.UserInfo.WonAttempt = false;
					Main.UserInfo.Attempts++;
				}
			}
			if (type == PrizeType.gpDiscount)
			{
				if (this.isResult)
				{
					bool wordWrap = this.LableWhite.wordWrap;
					this.LableWhite.wordWrap = true;
					string text4 = string.Concat(new object[]
					{
						Language.RouletteDiscount1,
						" ",
						SingletoneComponent<Globals>.Instance.GpDiscountTime,
						" ",
						Language.Hours
					});
					GUI.Label(new Rect(0f, 200f, this.Rect.width, 18f), Helpers.ColoredText(text4, "#757575"), this.LableWhite);
					text4 = string.Concat(new object[]
					{
						Language.RouletteDiscount2Part1,
						"<color=#dbb411> +",
						SingletoneComponent<Globals>.Instance.GpDiscount,
						"%</color> ",
						Language.RouletteDiscount2Part2
					});
					GUI.Label(new Rect((this.Rect.width - 220f) / 2f, 230f, 220f, 36f), text4, this.LableWhite);
					int seconds = Main.UserInfo.PersonaBankDiscount.DiscountEnds - HtmlLayer.serverUtc;
					text4 = string.Concat(new string[]
					{
						Language.RouletteDiscount3Part1.ToUpper(),
						" <color=#757575>",
						MainGUI.Instance.SecondsToStringHHHMMSS(seconds),
						"</color> ",
						Language.RouletteDiscount3Part2.ToUpper()
					});
					int fontSize = this.LableWhite.fontSize;
					this.LableWhite.fontSize = 14;
					GUI.Label(new Rect((this.Rect.width - 200f) / 2f, 378f, 200f, 36f), text4, this.LableWhite);
					this.LableWhite.fontSize = fontSize;
					this.LableWhite.wordWrap = wordWrap;
				}
				if (this.prizeTexture)
				{
					GUI.DrawTexture(new Rect((this.Rect.width - (float)this.prizeTexture.width) * 0.5f, (this.Rect.height - (float)this.prizeTexture.height) * 0.5f + 20f, (float)this.prizeTexture.width, (float)this.prizeTexture.height), this.prizeTexture);
				}
			}
			else
			{
				GUI.Label(new Rect(-3f, 280f, this.Rect.width, 40f), Helpers.SeparateNumericString(this.prizeText), this.LabelWhiteKorataki20);
				if (this.prizeTexture)
				{
					GUI.DrawTexture(new Rect((this.Rect.width - (float)this.prizeTexture.width) * 0.5f, 320f, (float)this.prizeTexture.width, (float)this.prizeTexture.height), this.prizeTexture);
				}
			}
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0007FAE8 File Offset: 0x0007DCE8
	private void DrawRoulette()
	{
		if (Main.UserInfo.Attempts > 0)
		{
			this.noTries = false;
		}
		string text = "3eb4ff";
		if (Main.UserInfo.Attempts == 0)
		{
			text = "d10000";
		}
		string str = string.Concat(new string[]
		{
			"<color=#",
			text,
			"><size=20>",
			Main.UserInfo.Attempts.ToString(),
			"</size></color>"
		});
		GUI.DrawTexture(new Rect(0f, 0f, this.Rect.xMax, this.Rect.yMax), MainGUI.Instance.black);
		GUI.Label(new Rect(10f, 5f, 100f, 20f), Language.Roulette.ToUpper(), this.LabelWhiteDINC);
		GUI.Label(new Rect(0f, 5f, this.Rect.width, 20f), Language.RouletteDescription, this.LabelGray);
		GUI.Label(new Rect(0f, 35f, 800f, 20f), Language.RouleteTriesLeft.ToUpper() + ": " + str, this.LabelWhiteKorataki);
		GUI.Label(new Rect(610f, 35f, 300f, 20f), "<size=14>" + Language.RouletteTriesAdd.ToUpper() + "</size>", this.LabelWhiteDINC);
		this.RotatePointer();
		if (this.isStarted)
		{
			this.Roll(this.StartPosition, this.StopPosition, this.distance, this.Velocity());
		}
		GUI.DrawTexture(new Rect((this.Rect.width - (float)this.BackGround.width) * 0.5f, (this.Rect.height - (float)this.BackGround.height) * 0.5f, (float)this.BackGround.width, (float)this.BackGround.height), this.BackGround);
		if (CVars.IsVanilla)
		{
			GUI.DrawTexture(new Rect(230f, 135f, (float)this.MpIconBig.width * 0.75f, (float)this.MpIconBig.height * 0.75f), this.MpIconBig);
		}
		else
		{
			GUI.DrawTexture(new Rect(240f, 140f, (float)this.Roulette_SP.width, (float)this.Roulette_SP.height), this.Roulette_SP);
		}
		GUI.DrawTexture(new Rect(332f, 90f, (float)this.Roulette_GP.width, (float)this.Roulette_GP.height), this.Roulette_GP);
		GUI.DrawTexture(new Rect(435f, 475f, (float)this.Roulette_GP.width, (float)this.Roulette_GP.height), this.Roulette_GP);
		GUI.DrawTexture(new Rect(430f, 90f, (float)this.Roulette_CR.width, (float)this.Roulette_CR.height), this.Roulette_CR);
		GUI.DrawTexture(new Rect(560f, 330f, (float)this.MpIconBig.width * 0.75f, (float)this.MpIconBig.height * 0.75f), this.MpIconBig);
		GUI.DrawTexture(new Rect(227f, 412f, (float)this.GpDiscountIcon.width, (float)this.GpDiscountIcon.height), this.GpDiscountIcon);
		GUI.DrawTexture(new Rect(188f, 228f, (float)this.Roulette_CR.width, (float)this.Roulette_CR.height), this.Roulette_CR);
		GUI.DrawTexture(new Rect(532f, 150f, (float)MainGUI.Instance.RouletteIcon.width, (float)MainGUI.Instance.RouletteIcon.height), MainGUI.Instance.RouletteIcon);
		GUI.DrawTexture(new Rect(568f, 232f, (float)this.Roulette_Skills.width, (float)this.Roulette_Skills.height), this.Roulette_Skills);
		GUI.DrawTexture(new Rect(307f, 468f, (float)this.Roulette_Weapons.width, (float)this.Roulette_Weapons.height), this.Roulette_Weapons);
		GUI.enabled = (!this.isStarted && !this.requestSended && !PopupGUI.IsAnyPopupShow);
		if (GUI.Button(new Rect((this.Rect.width - (float)this.RollButtonStyle.normal.background.width) * 0.5f, 545f, (float)this.RollButtonStyle.normal.background.width, (float)this.RollButtonStyle.normal.background.height), Language.RouletteRoll.ToUpper(), this.RollButtonStyle))
		{
			if (Main.UserInfo.Attempts > 0)
			{
				Main.AddDatabaseRequestCallBack<RouletteGetPrize>(delegate
				{
					this.RndStopPosition(RouletteGUI.Result);
					this.isStarted = true;
					this.requestSended = false;
					Main.UserInfo.Attempts--;
				}, delegate
				{
					this.isStarted = false;
					this.requestSended = false;
				}, new object[0]);
				this.SetPointer();
				this.isResult = false;
				this.requestSended = true;
			}
			else
			{
				this.noTries = true;
			}
		}
		if (GUI.Button(new Rect(570f, 34f, (float)this.AddTryButtonStyle.normal.background.width, (float)this.AddTryButtonStyle.normal.background.height), string.Empty, this.AddTryButtonStyle))
		{
			EventFactory.Call("ShowPopup", new Popup(WindowsID.BuyAttempt, Language.RoulettePopupHeader, string.Empty, PopupState.buyAttempt, false, true, string.Empty, string.Empty));
		}
		if (GUI.Button(new Rect(this.Rect.width - (float)CWGUI.p.closeButton.normal.background.width, 0f, (float)CWGUI.p.closeButton.normal.background.width, (float)CWGUI.p.closeButton.normal.background.height), string.Empty, CWGUI.p.closeButton))
		{
			this.Hide(0.35f);
		}
		GUI.enabled = true;
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x00080168 File Offset: 0x0007E368
	private void DrawInnerLabel(float[] angle, Rect[] rect)
	{
		int fontSize = this.LabelGray.fontSize;
		this.LabelGray.fontSize = 15;
		int num = (Main.UserInfo.currentLevel <= 0) ? 1 : Main.UserInfo.currentLevel;
		GUIUtility.RotateAroundPivot(angle[0], this.Pivot);
		GUI.Label(rect[0], Helpers.SeparateNumericString((Globals.I.RouletteInfo.amountCR[0] * num).ToString()) + " CR", this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[0], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[1], this.Pivot);
		GUI.Label(rect[1], Language.RouletteSkill, this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[1], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[2], this.Pivot);
		GUI.Label(rect[2], Helpers.SeparateNumericString(Globals.I.RouletteInfo.amountMP[0].ToString()) + " MP", this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[2], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[3], this.Pivot);
		GUI.Label(rect[3], Helpers.SeparateNumericString(Globals.I.RouletteInfo.amountGP[0].ToString()) + " GP", this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[3], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[4], this.Pivot);
		GUI.Label(rect[4], Language.RouletteWeapon, this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[4], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[5], this.Pivot);
		GUI.Label(rect[5], Helpers.ColoredText("GP BONUS", "#dbb411"), this.LableWhite);
		GUIUtility.RotateAroundPivot(-angle[5], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[6], this.Pivot);
		GUI.Label(rect[6], Helpers.SeparateNumericString((Globals.I.RouletteInfo.amountCR[1] * num).ToString()) + " CR", this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[6], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[7], this.Pivot);
		if (CVars.IsVanilla)
		{
			GUI.Label(rect[7], Helpers.SeparateNumericString(Globals.I.RouletteInfo.amountMP[1].ToString()) + " MP", this.LabelGray);
		}
		else
		{
			GUI.Label(rect[7], Globals.I.RouletteInfo.amountSP + " SP", this.LabelGray);
		}
		GUIUtility.RotateAroundPivot(-angle[7], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[8], this.Pivot);
		GUI.Label(rect[8], Helpers.SeparateNumericString(Globals.I.RouletteInfo.amountGP[1].ToString()) + " GP", this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[8], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[9], this.Pivot);
		GUI.Label(rect[9], Language.RouletteOneAttempt.ToUpper(), this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[9], this.Pivot);
		GUIUtility.RotateAroundPivot(angle[10], this.Pivot);
		GUI.Label(rect[10], Language.RouletteCamouflage.ToUpper(), this.LabelGray);
		GUIUtility.RotateAroundPivot(-angle[10], this.Pivot);
		this.LabelGray.fontSize = fontSize;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x00080568 File Offset: 0x0007E768
	[Obfuscation(Exclude = true)]
	private void ShowRoulette(object obj)
	{
		this.Show(0.5f, 0f);
		this.isResult = false;
		this._weaponTexture = MainGUI.Instance.weapon_unlocked[UnityEngine.Random.Range(1, MainGUI.Instance.weapon_unlocked.Length - 1)];
		this.skillTexture = CarrierGUI.I.Class_skills[this.skillPrize[UnityEngine.Random.Range(0, this.skillPrize.Length - 1)]];
		int[] array = new int[12];
		array[0] = Globals.I.RouletteInfo.amountCR[1];
		array[1] = Globals.I.RouletteInfo.amountMP[1];
		array[2] = Globals.I.RouletteInfo.amountGP[1];
		array[3] = Globals.I.RouletteInfo.amountCR[0];
		array[6] = Globals.I.RouletteInfo.amountMP[0];
		array[8] = Globals.I.RouletteInfo.amountGP[0];
		this.iArr = array;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x00080660 File Offset: 0x0007E860
	[Obfuscation(Exclude = true)]
	private void HideRoulette(object obj)
	{
		this.Hide(0.35f);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x00080670 File Offset: 0x0007E870
	public override void Register()
	{
		EventFactory.Register("ShowRoulette", this);
		EventFactory.Register("HideRoulette", this);
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x00080688 File Offset: 0x0007E888
	public override void MainInitialize()
	{
		this.isRendering = true;
		RouletteGUI.I = this;
		base.MainInitialize();
		this.RndStartPosition();
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x000806A4 File Offset: 0x0007E8A4
	public override void InterfaceGUI()
	{
		this.gui.color = Colors.alpha(Color.white, base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2((float)Screen.width, (float)Screen.height));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		GUI.BeginGroup(this.Rect);
		this.DrawRoulette();
		this.DrawInnerLabel(this.TiltAngle, this.Position);
		this.DrawPrize(this.ChangeType());
		GUI.EndGroup();
	}

	// Token: 0x04000C2A RID: 3114
	public static RouletteGUI I;

	// Token: 0x04000C2B RID: 3115
	private int prizeIndex;

	// Token: 0x04000C2C RID: 3116
	public float angle;

	// Token: 0x04000C2D RID: 3117
	public float StartPosition;

	// Token: 0x04000C2E RID: 3118
	public float StopPosition;

	// Token: 0x04000C2F RID: 3119
	private float distance;

	// Token: 0x04000C30 RID: 3120
	public float rotate;

	// Token: 0x04000C31 RID: 3121
	private bool isStarted;

	// Token: 0x04000C32 RID: 3122
	private bool isResult;

	// Token: 0x04000C33 RID: 3123
	private bool noTries;

	// Token: 0x04000C34 RID: 3124
	private bool requestSended;

	// Token: 0x04000C35 RID: 3125
	public AudioClip OnRollSound;

	// Token: 0x04000C36 RID: 3126
	public AudioClip OnWinSound;

	// Token: 0x04000C37 RID: 3127
	public AudioClip OnFailSound;

	// Token: 0x04000C38 RID: 3128
	public AudioClip OnBDSound;

	// Token: 0x04000C39 RID: 3129
	public PrizeType Type;

	// Token: 0x04000C3A RID: 3130
	public Texture2D BackGround;

	// Token: 0x04000C3B RID: 3131
	public Texture2D Roulette_Weapons;

	// Token: 0x04000C3C RID: 3132
	public Texture2D Roulette_Skills;

	// Token: 0x04000C3D RID: 3133
	public Texture2D Roulette_SP;

	// Token: 0x04000C3E RID: 3134
	public Texture2D Roulette_GP;

	// Token: 0x04000C3F RID: 3135
	public Texture2D Roulette_CR;

	// Token: 0x04000C40 RID: 3136
	public Texture2D Roulette_Pointer;

	// Token: 0x04000C41 RID: 3137
	public Texture2D Roulette_SpecBD;

	// Token: 0x04000C42 RID: 3138
	public Texture2D Roulette_Camo;

	// Token: 0x04000C43 RID: 3139
	public Texture2D Roulette_Camo_Border;

	// Token: 0x04000C44 RID: 3140
	public Texture2D MpIcon;

	// Token: 0x04000C45 RID: 3141
	public Texture2D MpIconBig;

	// Token: 0x04000C46 RID: 3142
	public Texture2D GpDiscountIcon;

	// Token: 0x04000C47 RID: 3143
	public Texture2D GpDiscountIconBig;

	// Token: 0x04000C48 RID: 3144
	public GUIStyle LabelWhiteDINC;

	// Token: 0x04000C49 RID: 3145
	public GUIStyle LabelWhiteKorataki;

	// Token: 0x04000C4A RID: 3146
	public GUIStyle LabelWhiteKorataki20;

	// Token: 0x04000C4B RID: 3147
	public GUIStyle LabelGray;

	// Token: 0x04000C4C RID: 3148
	public GUIStyle LableWhite;

	// Token: 0x04000C4D RID: 3149
	public GUIStyle LabelRed;

	// Token: 0x04000C4E RID: 3150
	public GUIStyle RollButtonStyle;

	// Token: 0x04000C4F RID: 3151
	public GUIStyle AddTryButtonStyle;

	// Token: 0x04000C50 RID: 3152
	public float[] TiltAngle;

	// Token: 0x04000C51 RID: 3153
	public Rect[] Position;

	// Token: 0x04000C52 RID: 3154
	private string prizeText = string.Empty;

	// Token: 0x04000C53 RID: 3155
	private Texture2D prizeTexture;

	// Token: 0x04000C54 RID: 3156
	private Texture2D skillTexture;

	// Token: 0x04000C55 RID: 3157
	private Texture2D _weaponTexture;

	// Token: 0x04000C56 RID: 3158
	private PrizeType[] aTypes = new PrizeType[]
	{
		PrizeType.cr,
		PrizeType.sp,
		PrizeType.gp,
		PrizeType.cr,
		PrizeType.plusone,
		PrizeType.skill,
		PrizeType.mp,
		PrizeType.special,
		PrizeType.gp,
		PrizeType.weapon,
		PrizeType.gpDiscount,
		PrizeType.camo
	};

	// Token: 0x04000C57 RID: 3159
	private PrizeType[] aTypesVanilla = new PrizeType[]
	{
		PrizeType.cr,
		PrizeType.mp,
		PrizeType.gp,
		PrizeType.cr,
		PrizeType.plusone,
		PrizeType.skill,
		PrizeType.mp,
		PrizeType.special,
		PrizeType.gp,
		PrizeType.weapon,
		PrizeType.gpDiscount,
		PrizeType.camo
	};

	// Token: 0x04000C58 RID: 3160
	private readonly int[] skillPrize = new int[]
	{
		12,
		52,
		72,
		124,
		130,
		132,
		133,
		137,
		139,
		140,
		141,
		142,
		143,
		144,
		145,
		147,
		150,
		151
	};

	// Token: 0x04000C59 RID: 3161
	private int[] iArr;

	// Token: 0x04000C5A RID: 3162
	public static string Result = string.Empty;

	// Token: 0x04000C5B RID: 3163
	private int index;

	// Token: 0x04000C5C RID: 3164
	private int prevIndex = -1;

	// Token: 0x04000C5D RID: 3165
	private int weaponID;

	// Token: 0x04000C5E RID: 3166
	private int camoID;

	// Token: 0x04000C5F RID: 3167
	private int skillID;

	// Token: 0x04000C60 RID: 3168
	private string bdStr = string.Empty;
}
