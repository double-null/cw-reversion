using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

// Token: 0x02000163 RID: 355
[Obfuscation(Exclude = true, ApplyToMembers = true)]
[AddComponentMenu("Scripts/GUI/MainHelpGUI")]
internal class MainHelpGUI : Form
{
	// Token: 0x1700012A RID: 298
	// (get) Token: 0x06000948 RID: 2376 RVA: 0x0005CD34 File Offset: 0x0005AF34
	public override int Width
	{
		get
		{
			return this.gui.Width;
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000949 RID: 2377 RVA: 0x0005CD44 File Offset: 0x0005AF44
	public override int Height
	{
		get
		{
			return this.gui.Height;
		}
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0005CD54 File Offset: 0x0005AF54
	[Obfuscation(Exclude = true)]
	public void ShowMainHelpGUI(object obj)
	{
		for (int i = 0; i < this.tHelpImages.Length; i++)
		{
			if (this.tHelpImages[i] == null)
			{
				base.StartCoroutine(this.downloadHelpImage(i, this.sImageName[i]));
			}
		}
		this.Show(0.5f, 0f);
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0005CDB4 File Offset: 0x0005AFB4
	[Obfuscation(Exclude = true)]
	public void HideMainHelpGUI(object obj)
	{
		for (int i = 0; i < this.HintsAlpha.Length; i++)
		{
			this.HintsAlpha[i].Hide(0.5f, 0f);
		}
		this.Hide(0.35f);
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x0005CDFC File Offset: 0x0005AFFC
	private void HideTab()
	{
		for (int i = 0; i < this.HintsAlpha.Length; i++)
		{
			this.HintsAlpha[i].Hide(0.5f, 0f);
		}
		this.mainPictureAlpha.Hide(0.5f, 0f);
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x0005CE50 File Offset: 0x0005B050
	private void ExitWindowButton()
	{
		float num = (float)((!(CVars.realm == "mailru") && !(CVars.realm == "ok") && !(CVars.realm == "fb")) ? 0 : 40);
		if (this.gui.Button(new Vector2((float)(this.Width - this.gui.server_window[3].width) - num, 0f), this.gui.server_window[3], this.gui.server_window[4], null, string.Empty, 24, "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
		{
			this.HideMainHelpGUI(null);
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x0005CF2C File Offset: 0x0005B12C
	private void HelpPage0()
	{
		if (this.tHelpImages[0] == null)
		{
			this.KrutilkaOnLoading();
			return;
		}
		this.DrawMainPicture(this.tHelpImages[0], this.mainPictureAlpha.visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 159f, 120f, 30f), Language.MHGDescr[0], string.Empty, new Vector2(180f, 169f), this.HintsAlpha[0].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 206f, 132f, 50f), Language.MHGDescr[1], string.Empty, new Vector2(180f, 214f), this.HintsAlpha[1].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 247f, 117f, 30f), Language.MHGDescr[2], string.Empty, new Vector2(196f, 256f), this.HintsAlpha[2].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 267f, 134f, 30f), Language.MHGDescr[3], Language.MHGDescr[4], new Vector2(223f, 284f), this.HintsAlpha[3].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 304f, 160f, 30f), Language.MHGDescr[5], Language.MHGDescr[6], new Vector2(188f, 311f), this.HintsAlpha[4].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 342f, 110f, 30f), Language.MHGDescr[7], Language.MHGDescr[8], new Vector2(180f, 351f), this.HintsAlpha[5].visibility * base.visibility);
		this.DrawingHint(new Rect(9f, 377f, 136f, 30f), Language.MHGDescr[9], Language.MHGDescr[10], new Vector2(174f, 385f), this.HintsAlpha[6].visibility * base.visibility);
		this.DrawingHint(new Rect(125f, 507f, 166f, 30f), Language.MHGDescr[11], Language.MHGDescr[12], new Vector2(268f, 268f), this.HintsAlpha[7].visibility * base.visibility);
		this.DrawingHint(new Rect(295f, 507f, 178f, 30f), Language.MHGDescr[13], string.Empty, new Vector2(300f, 412f), this.HintsAlpha[8].visibility * base.visibility);
		this.DrawingHint(new Rect(479f, 507f, 204f, 30f), Language.MHGDescr[14], Language.MHGDescr[15], new Vector2(560f, 449f), this.HintsAlpha[9].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 401f, 136f, 30f), Language.MHGDescr[16], Language.MHGDescr[17], new Vector2(609f, 411f), this.HintsAlpha[10].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 292f, 130f, 30f), Language.MHGDescr[18], Language.MHGDescr[19], new Vector2(456f, 305f), this.HintsAlpha[11].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 223f, 130f, 30f), Language.MHGDescr[20], string.Empty, new Vector2(613f, 229f), this.HintsAlpha[12].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 203f, 135f, 30f), Language.MHGDescr[21], string.Empty, new Vector2(585f, 213f), this.HintsAlpha[13].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 162f, 143f, 30f), Language.MHGDescr[22], string.Empty, new Vector2(585f, 175f), this.HintsAlpha[14].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 127f, 143f, 30f), Language.MHGDescr[23], string.Empty, new Vector2(615f, 141f), this.HintsAlpha[15].visibility * base.visibility);
		this.DrawingHint(new Rect(407f, 108f, 269f, 30f), Language.MHGDescr[24], string.Empty, new Vector2(415f, 168f), this.HintsAlpha[16].visibility * base.visibility);
		this.DrawingHint(new Rect(317f, 89f, 192f, 30f), Language.MHGDescr[25], string.Empty, new Vector2(336f, 168f), this.HintsAlpha[17].visibility * base.visibility);
		this.DrawingHint(new Rect(180f, 44f, 188f, 30f), Language.MHGDescr[26], Language.MHGDescr[27], new Vector2(186f, 146f), this.HintsAlpha[18].visibility * base.visibility);
		this.SetShowAlpha(19, true);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0005D578 File Offset: 0x0005B778
	private void HelpPage1()
	{
		if (this.tHelpImages[1] == null)
		{
			this.KrutilkaOnLoading();
			return;
		}
		this.DrawMainPicture(this.tHelpImages[1], this.mainPictureAlpha.visibility * base.visibility);
		this.DrawingHint(new Rect(7f, 186f, 140f, 30f), Language.MHGDescr[28], Language.MHGDescr[29], new Vector2(171f, 197f), this.HintsAlpha[0].visibility * base.visibility);
		this.DrawingHint(new Rect(7f, 263f, 150f, 30f), Language.MHGDescr[30], Language.MHGDescr[31], new Vector2(184f, 266f), this.HintsAlpha[1].visibility * base.visibility);
		this.DrawingHint(new Rect(162f, 499f, 193f, 30f), Language.MHGDescr[32], Language.MHGDescr[33], new Vector2(346f, 349f), this.HintsAlpha[2].visibility * base.visibility);
		this.DrawingHint(new Rect(366f, 499f, 139f, 30f), Language.MHGDescr[34], string.Empty, new Vector2(366f, 395f), this.HintsAlpha[3].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 280f, 110f, 30f), Language.MHGDescr[35], string.Empty, new Vector2(595f, 288f), this.HintsAlpha[4].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 227f, 134f, 30f), Language.MHGDescr[36], string.Empty, new Vector2(624f, 237f), this.HintsAlpha[5].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 205f, 139f, 30f), Language.MHGDescr[37], string.Empty, new Vector2(607f, 217f), this.HintsAlpha[6].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 159f, 103f, 30f), Language.MHGDescr[38], string.Empty, new Vector2(618f, 172f), this.HintsAlpha[7].visibility * base.visibility);
		this.DrawingHint(new Rect(411f, 131f, 236f, 30f), Language.MHGDescr[39], string.Empty, new Vector2(495f, 175f), this.HintsAlpha[8].visibility * base.visibility);
		this.DrawingHint(new Rect(327f, 67f, 79f, 30f), Language.MHGDescr[40], string.Empty, new Vector2(395f, 173f), this.HintsAlpha[9].visibility * base.visibility);
		this.DrawingHint(new Rect(215f, 98f, 181f, 30f), Language.MHGDescr[41], string.Empty, new Vector2(386f, 190f), this.HintsAlpha[10].visibility * base.visibility);
		this.DrawingHint(new Rect(135f, 120f, 242f, 30f), Language.MHGDescr[42], string.Empty, new Vector2(368f, 243f), this.HintsAlpha[11].visibility * base.visibility);
		this.DrawingHint(new Rect(169f, 143f, 185f, 30f), Language.MHGDescr[43], string.Empty, new Vector2(341f, 167f), this.HintsAlpha[12].visibility * base.visibility);
		this.SetShowAlpha(13, true);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0005D9DC File Offset: 0x0005BBDC
	private void HelpPage3()
	{
		if (this.tHelpImages[3] == null)
		{
			this.KrutilkaOnLoading();
			return;
		}
		this.DrawMainPicture(this.tHelpImages[3], this.mainPictureAlpha.visibility * base.visibility);
		this.DrawingHint(new Rect(8f, 184f, 123f, 30f), Language.MHGDescr[44], string.Empty, new Vector2(175f, 190f), this.HintsAlpha[0].visibility * base.visibility);
		this.DrawingHint(new Rect(8f, 209f, 129f, 30f), Language.MHGDescr[45], Language.MHGDescr[46], new Vector2(184f, 208f), this.HintsAlpha[1].visibility * base.visibility);
		this.DrawingHint(new Rect(8f, 292f, 142f, 30f), Language.MHGDescr[47], Language.MHGDescr[48], new Vector2(221f, 298f), this.HintsAlpha[2].visibility * base.visibility);
		this.DrawingHint(new Rect(8f, 442f, 155f, 30f), Language.MHGDescr[49], Language.MHGDescr[50], new Vector2(182f, 450f), this.HintsAlpha[3].visibility * base.visibility);
		this.DrawingHint(new Rect(156f, 526f, 49f, 30f), Language.MHGDescr[51], string.Empty, new Vector2(195f, 478f), this.HintsAlpha[4].visibility * base.visibility);
		this.DrawingHint(new Rect(226f, 526f, 35f, 30f), Language.MHGDescr[52], string.Empty, new Vector2(249f, 478f), this.HintsAlpha[5].visibility * base.visibility);
		this.DrawingHint(new Rect(219f, 548f, 123f, 30f), Language.MHGDescr[53], Language.MHGDescr[54], new Vector2(320f, 482f), this.HintsAlpha[6].visibility * base.visibility);
		this.DrawingHint(new Rect(364f, 548f, 146f, 30f), Language.MHGDescr[55], string.Empty, new Vector2(364f, 482f), this.HintsAlpha[7].visibility * base.visibility);
		this.DrawingHint(new Rect(435f, 503f, 120f, 30f), Language.MHGDescr[56], string.Empty, new Vector2(547f, 482f), this.HintsAlpha[8].visibility * base.visibility);
		this.DrawingHint(new Rect(441f, 524f, 155f, 30f), Language.MHGDescr[57], string.Empty, new Vector2(585f, 482f), this.HintsAlpha[9].visibility * base.visibility);
		this.DrawingHint(new Rect(592f, 569f, 155f, 30f), Language.MHGDescr[58], string.Empty, new Vector2(593f, 464f), this.HintsAlpha[10].visibility * base.visibility);
		this.DrawingHint(new Rect(611f, 525f, 142f, 30f), Language.MHGDescr[59], string.Empty, new Vector2(616f, 478f), this.HintsAlpha[11].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 433f, 142f, 30f), Language.MHGDescr[60], Language.MHGDescr[61], new Vector2(623f, 440f), this.HintsAlpha[12].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 372f, 142f, 30f), Language.MHGDescr[62], Language.MHGDescr[63], new Vector2(621f, 380f), this.HintsAlpha[13].visibility * base.visibility);
		this.DrawingHint(new Rect(637f, 238f, 151f, 30f), Language.MHGDescr[64], string.Empty, new Vector2(621f, 246f), this.HintsAlpha[14].visibility * base.visibility);
		this.DrawingHint(new Rect(551f, 48f, 147f, 30f), Language.MHGDescr[65], Language.MHGDescr[66], new Vector2(551f, 118f), this.HintsAlpha[15].visibility * base.visibility);
		this.DrawingHint(new Rect(394f, 48f, 147f, 30f), Language.MHGDescr[67], Language.MHGDescr[68], new Vector2(403f, 118f), this.HintsAlpha[16].visibility * base.visibility);
		this.DrawingHint(new Rect(383f, 27f, 125f, 30f), Language.MHGDescr[69], string.Empty, new Vector2(386f, 233f), this.HintsAlpha[17].visibility * base.visibility);
		this.DrawingHint(new Rect(219f, 69f, 165f, 30f), Language.MHGDescr[70], Language.MHGDescr[71], new Vector2(361f, 286f), this.HintsAlpha[18].visibility * base.visibility);
		this.DrawingHint(new Rect(164f, 46f, 118f, 30f), Language.MHGDescr[72], string.Empty, new Vector2(187f, 125f), this.HintsAlpha[19].visibility * base.visibility);
		this.SetShowAlpha(20, true);
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0005E080 File Offset: 0x0005C280
	private void HelpPage2()
	{
		if (this.tHelpImages[2] == null)
		{
			this.KrutilkaOnLoading();
			return;
		}
		this.DrawMainPicture(this.tHelpImages[2], this.mainPictureAlpha.visibility * base.visibility);
		this.DrawingHint(new Rect(10f, 151f, 134f, 30f), Language.MHGDescr[73], string.Empty, new Vector2(181f, 154f), this.HintsAlpha[0].visibility * base.visibility);
		this.DrawingHint(new Rect(10f, 183f, 104f, 30f), Language.MHGDescr[74], string.Empty, new Vector2(264f, 189f), this.HintsAlpha[1].visibility * base.visibility);
		this.DrawingHint(new Rect(10f, 216f, 121f, 30f), Language.MHGDescr[75], string.Empty, new Vector2(307f, 224f), this.HintsAlpha[2].visibility * base.visibility);
		this.DrawingHint(new Rect(10f, 242f, 114f, 30f), Language.MHGDescr[76], string.Empty, new Vector2(271f, 252f), this.HintsAlpha[3].visibility * base.visibility);
		this.DrawingHint(new Rect(10f, 284f, 96f, 30f), Language.MHGDescr[77], string.Empty, new Vector2(234f, 283f), this.HintsAlpha[4].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 446f, 121f, 30f), Language.MHGDescr[78], string.Empty, new Vector2(596f, 454f), this.HintsAlpha[5].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 279f, 152f, 30f), Language.MHGDescr[79], Language.MHGDescr[80], new Vector2(614f, 287f), this.HintsAlpha[6].visibility * base.visibility);
		this.DrawingHint(new Rect(635f, 217f, 152f, 30f), Language.MHGDescr[81], Language.MHGDescr[82], new Vector2(614f, 225f), this.HintsAlpha[7].visibility * base.visibility);
		this.DrawingHint(new Rect(604f, 51f, 128f, 30f), Language.MHGDescr[83], string.Empty, new Vector2(613f, 154f), this.HintsAlpha[8].visibility * base.visibility);
		this.DrawingHint(new Rect(435f, 51f, 146f, 30f), Language.MHGDescr[84], Language.MHGDescr[85], new Vector2(508f, 168f), this.HintsAlpha[9].visibility * base.visibility);
		this.SetShowAlpha(10, true);
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0005E3F4 File Offset: 0x0005C5F4
	private IEnumerator downloadHelpImage(int iImageIndex, string sImageName)
	{
		WWW www = new WWW(WWWUtil.rootWWWNoExtension("img/help/" + sImageName));
		yield return www;
		if (www != null && www.error == null)
		{
			this.tHelpImages[iImageIndex] = www.texture;
			yield return new WaitForSeconds(1f);
			www.Dispose();
			www = null;
		}
		yield break;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0005E42C File Offset: 0x0005C62C
	private void SetShowAlpha(int count = 0, bool invert = false)
	{
		if (!this.mainPictureAlpha.Showing && this.lastIndex == this.iTabIndex)
		{
			this.mainPictureAlpha.Show(0.2f, 0f);
		}
		if (this.mainPictureAlpha.MaxVisible)
		{
			if (!invert)
			{
				if (base.MaxVisible)
				{
					for (int i = 0; i < this.HintsAlpha.Length; i++)
					{
						if (i == 0)
						{
							if (!this.HintsAlpha[i].Showing && !this.HintsAlpha[i].MaxVisible)
							{
								this.HintsAlpha[i].Show(0.1f, 0f);
							}
						}
						else if (this.HintsAlpha[i - 1].visibility > 0.7f && !this.HintsAlpha[i].Showing && !this.HintsAlpha[i].MaxVisible)
						{
							this.HintsAlpha[i].Show(0.1f, 0f);
						}
					}
				}
			}
			else if (base.MaxVisible)
			{
				if (count >= this.HintsAlpha.Length || count == 0)
				{
					count = this.HintsAlpha.Length - 1;
				}
				for (int j = count; j > 0; j--)
				{
					if (j == count)
					{
						if (!this.HintsAlpha[j].Showing && !this.HintsAlpha[j].MaxVisible)
						{
							this.HintsAlpha[j].Show(0.1f, 0f);
						}
					}
					else if (this.HintsAlpha[j + 1].visibility > 0.7f && !this.HintsAlpha[j].Showing && !this.HintsAlpha[j].MaxVisible)
					{
						this.HintsAlpha[j].Show(0.1f, 0f);
					}
				}
			}
		}
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0005E620 File Offset: 0x0005C820
	private void DrawingTab()
	{
		Vector2 pos = this.vTabVector;
		for (int i = 0; i < Language.MHGtabs.Length; i++)
		{
			if (i == this.iTabIndex)
			{
				this.gui.Picture(new Vector2(pos.x + 3f, pos.y + (float)(this.tab_buttons[0].height / 2)), this.tab_buttons[3]);
			}
			if (this.gui.Button(pos, (i != this.iTabIndex) ? this.tab_buttons[0] : this.tab_buttons[2], (i != this.iTabIndex) ? this.tab_buttons[1] : this.tab_buttons[2], this.tab_buttons[2], Language.MHGtabs[i], 12, (i != this.iTabIndex) ? "#000000" : "#ffffff", TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				this.iTabIndex = i;
				if (this.lastIndex != this.iTabIndex)
				{
					this.HideTab();
				}
			}
			pos.Set(pos.x + (float)this.tab_buttons[0].width, pos.y);
		}
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0005E774 File Offset: 0x0005C974
	private void DrawingHint(Rect rHintRect, string summary, string description, Vector2 vLinetoPoint, float alpha = 1f)
	{
		this.gui.color = Colors.alpha(this.gui.color, alpha * base.visibility);
		Rect rect = new Rect(rHintRect.xMin, rHintRect.yMin, rHintRect.width, this.gui.CalcHeight(summary, rHintRect.width, this.gui.fontTahoma, this.iHintFontSize));
		Rect rect2;
		if (description.Length < 1)
		{
			rect2 = new Rect(rHintRect.xMin, rHintRect.yMin, 0f, 0f);
		}
		else
		{
			rect2 = new Rect(rHintRect.xMin, rHintRect.yMin + rect.height - 5f, rHintRect.width, this.gui.CalcHeight(description, rHintRect.width, this.gui.fontTahoma, this.iHintFontSize));
		}
		Vector2 vector = default(Vector2);
		Vector2 pos = default(Vector2);
		this.gui.TextLabel(rect, summary, this.iHintFontSize, this.sHintSummaryColor, TextAnchor.UpperLeft, true);
		this.gui.TextLabel(rect2, description, this.iHintFontSize, this.sHintDescriptionColor, TextAnchor.UpperLeft, true);
		rHintRect.Set(rHintRect.xMin, rHintRect.yMin, rHintRect.width, rect.height + rect2.height);
		if (rHintRect.xMin <= vLinetoPoint.x && rHintRect.xMax <= vLinetoPoint.x && rHintRect.yMax <= vLinetoPoint.x && rHintRect.yMin <= vLinetoPoint.x)
		{
			if (vLinetoPoint.y - rHintRect.yMax > vLinetoPoint.x - rHintRect.xMax)
			{
				pos.Set(rHintRect.xMax, rHintRect.yMax);
				vector.Set(rHintRect.xMax, vLinetoPoint.y);
			}
			else
			{
				pos.Set(rHintRect.xMax, rHintRect.yMax);
				vector.Set(vLinetoPoint.x, rHintRect.yMax);
			}
		}
		if (rHintRect.xMin <= vLinetoPoint.x && rHintRect.xMax >= vLinetoPoint.x && rHintRect.yMax <= vLinetoPoint.y && rHintRect.yMin <= vLinetoPoint.y)
		{
			pos.Set(vLinetoPoint.x, rHintRect.yMax);
			vector.Set(vLinetoPoint.x, vLinetoPoint.y);
		}
		if (rHintRect.xMin >= vLinetoPoint.x && rHintRect.xMax >= vLinetoPoint.x && rHintRect.yMax <= vLinetoPoint.y && rHintRect.yMin <= vLinetoPoint.y)
		{
			if (vLinetoPoint.y - rHintRect.yMax > rHintRect.xMin - vLinetoPoint.x)
			{
				pos.Set(rHintRect.xMin, rHintRect.yMax);
				vector.Set(rHintRect.xMin, vLinetoPoint.y);
			}
			else
			{
				pos.Set(rHintRect.xMin, rHintRect.yMax);
				vector.Set(vLinetoPoint.x, rHintRect.yMax);
			}
		}
		if (rHintRect.xMin >= vLinetoPoint.x && rHintRect.xMax >= vLinetoPoint.x && rHintRect.yMax >= vLinetoPoint.y && rHintRect.yMin <= vLinetoPoint.y)
		{
			pos.Set(rHintRect.xMin, vLinetoPoint.y);
			vector.Set(vLinetoPoint.x, vLinetoPoint.y);
		}
		if (rHintRect.xMin >= vLinetoPoint.x && rHintRect.xMax >= vLinetoPoint.x && rHintRect.yMax >= vLinetoPoint.y && rHintRect.yMin >= vLinetoPoint.y)
		{
			if (rHintRect.yMin - vLinetoPoint.y > rHintRect.xMin - vLinetoPoint.x)
			{
				pos.Set(rHintRect.xMin, rHintRect.yMin);
				vector.Set(rHintRect.xMin, vLinetoPoint.y);
			}
			else
			{
				pos.Set(rHintRect.xMin, rHintRect.yMin);
				vector.Set(vLinetoPoint.x, rHintRect.yMin);
			}
		}
		if (rHintRect.xMin <= vLinetoPoint.x && rHintRect.xMax >= vLinetoPoint.x && rHintRect.yMax >= vLinetoPoint.y && rHintRect.yMin >= vLinetoPoint.y)
		{
			pos.Set(vLinetoPoint.x, rHintRect.yMin);
			vector.Set(vLinetoPoint.x, vLinetoPoint.y);
		}
		if (rHintRect.xMin <= vLinetoPoint.x && rHintRect.xMax <= vLinetoPoint.x && rHintRect.yMax >= vLinetoPoint.y && rHintRect.yMin >= vLinetoPoint.y)
		{
			if (rHintRect.yMin - vLinetoPoint.y > vLinetoPoint.x - rHintRect.xMax)
			{
				pos.Set(rHintRect.xMax, rHintRect.yMin);
				vector.Set(rHintRect.xMax, vLinetoPoint.y);
			}
			else
			{
				pos.Set(rHintRect.xMax, rHintRect.yMin);
				vector.Set(vLinetoPoint.x, rHintRect.yMin);
			}
		}
		if (rHintRect.xMin <= vLinetoPoint.x && rHintRect.xMax <= vLinetoPoint.x && rHintRect.yMax >= vLinetoPoint.y && rHintRect.yMin <= vLinetoPoint.y)
		{
			pos.Set(rHintRect.xMax, vLinetoPoint.y);
			vector.Set(vLinetoPoint.x, vLinetoPoint.y);
		}
		this.gui.PictureSized(pos, this.line, new Vector2(vector.x - pos.x + 1f, vector.y - pos.y + 1f));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0005EE30 File Offset: 0x0005D030
	private void DrawMainPicture(Texture2D picture, float pictureAlpha)
	{
		this.gui.color = Colors.alpha(this.gui.color, pictureAlpha * base.visibility);
		this.gui.PictureCentered(new Vector2((float)(this.Width / 2), (float)(this.Height / 2)), picture, new Vector2(1f, 1f));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0005EEB4 File Offset: 0x0005D0B4
	private bool KrutilkaOnLoading()
	{
		float angle = 180f * Time.realtimeSinceStartup * 1.5f;
		Rect rect = new Rect((float)(Screen.width / 2 - this.gui.Width / 2), (float)(Screen.height / 2 - this.gui.Height / 2), (float)this.gui.Width, (float)this.gui.Height);
		this.gui.RotateGUI(angle, new Vector2(rect.center.x, rect.center.y));
		this.gui.Picture(new Vector2(rect.center.x - (float)(this.gui.settings_window[9].width / 2), rect.center.y - (float)(this.gui.settings_window[9].height / 2)), this.gui.settings_window[9]);
		this.gui.RotateGUI(0f, Vector2.zero);
		return false;
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0005EFD0 File Offset: 0x0005D1D0
	public override void MainInitialize()
	{
		for (int i = 0; i < this.HintsAlpha.Length; i++)
		{
			this.HintsAlpha[i] = new Alpha();
		}
		for (int j = 0; j < this.tHelpImages.Length; j++)
		{
			this.tHelpImages[j] = null;
		}
		this.isRendering = true;
		base.MainInitialize();
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0005F034 File Offset: 0x0005D234
	public override void OnInitialized()
	{
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0005F038 File Offset: 0x0005D238
	public override void OnDestroy()
	{
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0005F03C File Offset: 0x0005D23C
	public override void Clear()
	{
		base.Clear();
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0005F044 File Offset: 0x0005D244
	public override void Register()
	{
		EventFactory.Register("ShowMainHelpGUI", this);
		EventFactory.Register("HideMainHelpGUI", this);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0005F05C File Offset: 0x0005D25C
	public override void InterfaceGUI()
	{
		this.gui.color = Colors.alpha(Color.white, 0.9f * base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2((float)Screen.width, (float)Screen.height));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		Rect rect;
		if (CVars.realm == "mailru" || CVars.realm == "ok" || CVars.realm == "fb")
		{
			rect = new Rect((float)((Screen.width + 30 - this.gui.Width) / 2), (float)((Screen.height - this.gui.Height) / 2), (float)this.Width, (float)this.Height);
		}
		else
		{
			rect = new Rect((float)((Screen.width - this.gui.Width) / 2), (float)((Screen.height - this.gui.Height) / 2), (float)this.Width, (float)this.Height);
		}
		this.gui.BeginGroup(rect, this.windowID != this.gui.FocusedWindow);
		this.ExitWindowButton();
		this.gui.TextField(new Rect(0f, 0f, 100f, 20f), Language.MHGHelp, 16, "#ffffff", TextAnchor.UpperCenter, false, false);
		this.DrawingTab();
		if (!this.mainPictureAlpha.Visible)
		{
			this.lastIndex = this.iTabIndex;
		}
		switch (this.lastIndex)
		{
		case 0:
			this.HelpPage0();
			break;
		case 1:
			this.HelpPage1();
			break;
		case 2:
			this.HelpPage2();
			break;
		case 3:
			this.HelpPage3();
			break;
		default:
			this.HelpPage0();
			break;
		}
		this.gui.EndGroup();
	}

	// Token: 0x04000A97 RID: 2711
	private Texture2D[] tHelpImages = new Texture2D[4];

	// Token: 0x04000A98 RID: 2712
	private string[] sImageName = new string[]
	{
		"help_mainmenu.png",
		"help_weapons.png",
		"help_skills.png",
		"help_battle.png"
	};

	// Token: 0x04000A99 RID: 2713
	public Texture2D[] tab_buttons;

	// Token: 0x04000A9A RID: 2714
	public Texture2D line;

	// Token: 0x04000A9B RID: 2715
	private int iTabIndex;

	// Token: 0x04000A9C RID: 2716
	private Vector2 vTabVector = new Vector2(100f, 0f);

	// Token: 0x04000A9D RID: 2717
	private int iHintFontSize = 9;

	// Token: 0x04000A9E RID: 2718
	private string sHintSummaryColor = "#4aa8ff_Tahoma";

	// Token: 0x04000A9F RID: 2719
	private string sHintDescriptionColor = "#a1a1a1_Tahoma";

	// Token: 0x04000AA0 RID: 2720
	private Alpha[] HintsAlpha = new Alpha[20];

	// Token: 0x04000AA1 RID: 2721
	private Alpha mainPictureAlpha = new Alpha();

	// Token: 0x04000AA2 RID: 2722
	private int lastIndex;
}
