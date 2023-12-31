using System;
using System.Collections.Generic;
using System.Reflection;
using Assets.Scripts.Camouflage;
using UnityEngine;

// Token: 0x020000EC RID: 236
[AddComponentMenu("Scripts/GUI/CamouflageGUI")]
[Obfuscation(Exclude = true)]
internal class CamouflageGUI : Form
{
	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06000612 RID: 1554 RVA: 0x0002EFEC File Offset: 0x0002D1EC
	public new Rect Rect
	{
		get
		{
			return new Rect((float)(Screen.width - this.gui.Width) * 0.5f, (float)(Screen.height - this.gui.Height) * 0.5f, (float)this.gui.Width, (float)this.gui.Height);
		}
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002F048 File Offset: 0x0002D248
	private void Start()
	{
		this._currencyIcons = new Texture2D[]
		{
			this._cr,
			this._gp,
			this._mp
		};
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0002F074 File Offset: 0x0002D274
	private void Update()
	{
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0002F078 File Offset: 0x0002D278
	public override void MainInitialize()
	{
		this.isRendering = true;
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0002F090 File Offset: 0x0002D290
	public void Open()
	{
		this.ShowCharacter();
		this.Show(0.5f, 0f);
		this.FillList();
		this.SortByPrice();
		this._equippedId = Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex];
		this._fittingId = this._equippedId;
		this._camouflageInfo = ModsStorage.Instance().CharacterCamouflages[this._equippedId];
		this._masteringMod = ModsStorage.Instance().GetModById(this._equippedId);
		this._scrollPosition = Vector2.zero;
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0002F130 File Offset: 0x0002D330
	private void Close()
	{
		this.HideCharacter(0.35f);
		this.Hide(0.35f);
		this._characterInitialized = false;
		CamouflageGUI.AddDownloaded = false;
		UnityEngine.Object.Destroy(GameObject.Find("add"));
		EventFactory.Call("ShowInterface", null);
		this._masteringMod = null;
		this._camouflageInfo = null;
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002F188 File Offset: 0x0002D388
	public override void InterfaceGUI()
	{
		this._tempSkin = GUI.skin;
		GUI.skin = this._customSkin;
		this.DrawBackground();
		GUI.BeginGroup(this.Rect);
		this.DrawCloseButton();
		GUI.enabled = (!PopupGUI.IsAnyPopupShow && ModIconsDownloader.Instance.DataLoadingCompleted && this._characterInitialized);
		if (!ModIconsDownloader.Instance.DataLoadingCompleted || !this._characterInitialized)
		{
			this.gui.ProcessingIndicator(new Vector2(this.Rect.width / 2f + 140f, 16f));
		}
		this.DrawCharacter();
		this.DrawHeader();
		this.DrawSwapButtons();
		this.DrawBalance(new Vector2(2f, this.Rect.height - 36f));
		this.DrawList();
		this.DrawSortingButtons();
		this.DrawSaveButton();
		GUI.EndGroup();
		GUI.skin = this._tempSkin;
		GUI.enabled = true;
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0002F290 File Offset: 0x0002D490
	private void DrawBackground()
	{
		this.gui.color = Colors.alpha(Color.white, base.visibility);
		this.gui.PictureSized(new Vector2(0f, 0f), this.gui.black, new Vector2((float)Screen.width, (float)Screen.height));
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0002F310 File Offset: 0x0002D510
	private void DrawCloseButton()
	{
		Texture2D background = CWGUI.p.closeButton.normal.background;
		Rect position = new Rect(this.Rect.width - (float)background.width - 10f, 5f, (float)background.width, (float)background.height);
		if (GUI.Button(position, string.Empty, CWGUI.p.closeButton) && !PopupGUI.IsAnyPopupShow)
		{
			this.Close();
		}
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0002F394 File Offset: 0x0002D594
	private void DrawSaveButton()
	{
		Texture2D background = this._saveBtnStyle.normal.background;
		Rect position = new Rect(this.Rect.width - (float)background.width - 10f, this.Rect.height - (float)background.height - 6f, (float)background.width, (float)background.height);
		GUI.enabled = (this._equippedId != Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex]);
		if (GUI.Button(position, Language.Save.ToUpper(), this._saveBtnStyle))
		{
			if (this._equippedId != 0)
			{
				Main.AddDatabaseRequestCallBack<SetCamouflage>(delegate
				{
					Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex] = this._equippedId;
					this.Close();
				}, delegate
				{
					throw new Exception("Camouflage not saved");
				}, new object[]
				{
					this._equippedId,
					Main.UserInfo.suitNameIndex
				});
			}
			else
			{
				Main.AddDatabaseRequestCallBack<UnsetCamouflage>(delegate
				{
					Main.UserInfo.Mastering.Camouflages[Main.UserInfo.suitNameIndex] = this._equippedId;
					this.Close();
				}, delegate
				{
					throw new Exception("Camouflage not saved");
				}, new object[]
				{
					Main.UserInfo.suitNameIndex
				});
			}
		}
		GUI.enabled = true;
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0002F4F4 File Offset: 0x0002D6F4
	private void DrawSortingButtons()
	{
		Rect position = new Rect(this.Rect.width - 404f, this.Rect.height - 28f, 96f, 16f);
		Color color = this.gui.color;
		if (position.Contains(Event.current.mousePosition) && GUI.enabled)
		{
			this.gui.color = Colors.alpha(color, 0.75f);
		}
		if (GUI.Button(position, Language.SortByName, this._emptyBtn))
		{
			this.SortByName();
		}
		this.gui.color = color;
		position.Set(this.Rect.width - 220f, this.Rect.height - 28f, 96f, 16f);
		if (position.Contains(Event.current.mousePosition) && GUI.enabled)
		{
			this.gui.color = Colors.alpha(color, 0.75f);
		}
		if (GUI.Button(position, Language.SortByPrice, this._emptyBtn))
		{
			this.SortByPrice();
		}
		this.gui.color = color;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0002F640 File Offset: 0x0002D840
	private void SortByName()
	{
		List<CamouflageInfo> list = new List<CamouflageInfo>();
		List<CamouflageInfo> list2 = new List<CamouflageInfo>();
		foreach (CamouflageInfo camouflageInfo in this._camouflages)
		{
			if (camouflageInfo.Locked)
			{
				list2.Add(camouflageInfo);
			}
			else
			{
				list.Add(camouflageInfo);
			}
		}
		list.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => string.Compare(ci1.ShortName, ci2.ShortName, StringComparison.Ordinal));
		list2.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => string.Compare(ci1.ShortName, ci2.ShortName, StringComparison.Ordinal));
		this._camouflages.Clear();
		foreach (CamouflageInfo item in list)
		{
			this._camouflages.Add(item);
		}
		foreach (CamouflageInfo item2 in list2)
		{
			this._camouflages.Add(item2);
		}
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0002F7CC File Offset: 0x0002D9CC
	private void SortByPrice()
	{
		List<CamouflageInfo> list = new List<CamouflageInfo>();
		List<CamouflageInfo> list2 = new List<CamouflageInfo>();
		List<CamouflageInfo> list3 = new List<CamouflageInfo>();
		List<CamouflageInfo> list4 = new List<CamouflageInfo>();
		foreach (CamouflageInfo camouflageInfo in this._camouflages)
		{
			if (!camouflageInfo.Locked)
			{
				list.Add(camouflageInfo);
			}
			else
			{
				Currency currencyType = camouflageInfo.CurrencyType;
				if (currencyType != Currency.Cr)
				{
					if (currencyType != Currency.Gp)
					{
						list3.Add(camouflageInfo);
					}
					else
					{
						list4.Add(camouflageInfo);
					}
				}
				else
				{
					list2.Add(camouflageInfo);
				}
			}
		}
		list.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => ci1.Price.CompareTo(ci2.Price));
		list2.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => ci1.Price.CompareTo(ci2.Price));
		list4.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => ci1.Price.CompareTo(ci2.Price));
		list3.Sort((CamouflageInfo ci1, CamouflageInfo ci2) => ci1.Price.CompareTo(ci2.Price));
		this._camouflages.Clear();
		foreach (CamouflageInfo item in list)
		{
			this._camouflages.Add(item);
		}
		foreach (CamouflageInfo item2 in list2)
		{
			this._camouflages.Add(item2);
		}
		foreach (CamouflageInfo item3 in list3)
		{
			this._camouflages.Add(item3);
		}
		foreach (CamouflageInfo item4 in list4)
		{
			this._camouflages.Add(item4);
		}
	}

	// Token: 0x0600061F RID: 1567 RVA: 0x0002FAA0 File Offset: 0x0002DCA0
	private void DrawHeader()
	{
		GUI.Label(new Rect(10f, 10f, 100f, 10f), Language.CamouflageSelection, CWGUI.p.MastWindowHeaderStyle);
	}

	// Token: 0x06000620 RID: 1568 RVA: 0x0002FAD0 File Offset: 0x0002DCD0
	private void DrawBalance(Vector2 pos)
	{
		Rect position = new Rect(pos.x, pos.y, 310f, 34f);
		GUI.DrawTexture(position, this._fill);
		GUI.BeginGroup(position);
		string text = Helpers.SeparateNumericString(Main.UserInfo.GP.ToString());
		Rect rect = new Rect(40f, 0f, 128f, position.height);
		this.gui.TextLabel(rect, text, 16, "#ff9e00", TextAnchor.MiddleLeft, true);
		GUI.DrawTexture(new Rect(rect.x - (float)this._gp.width, (position.height - (float)this._gp.height) / 2f, (float)this._gp.width, (float)this._gp.height), this._gp);
		text = Helpers.SeparateNumericString(Main.UserInfo.CR.ToString());
		this.gui.TextLabel(new Rect(0f, 0f, position.width, position.height), text, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
		float num = this.gui.CalcWidth(text, this.gui.fontDNC57, 16);
		GUI.DrawTexture(new Rect((position.width - num) / 2f - (float)this._cr.width, (position.height - (float)this._cr.height) / 2f, (float)this._cr.width, (float)this._cr.height), this._cr);
		text = Helpers.SeparateNumericString(Main.UserInfo.Mastering.MasteringPoints.ToString());
		rect = new Rect(position.width - 168f, 0f, 128f, position.height);
		this.gui.TextLabel(rect, text, 16, "#FFFFFF", TextAnchor.MiddleRight, true);
		num = this.gui.CalcWidth(text, this.gui.fontDNC57, 16);
		GUI.DrawTexture(new Rect(position.width - 40f - num - (float)this._mp.width, (position.height - (float)this._mp.height) / 2f, (float)this._mp.width, (float)this._mp.height), this._mp);
		GUI.EndGroup();
	}

	// Token: 0x06000621 RID: 1569 RVA: 0x0002FD40 File Offset: 0x0002DF40
	private void DrawList()
	{
		Rect position = new Rect(this.Rect.width - 485f, 36f, 478f, 525f);
		this._mouseInList = position.Contains(Event.current.mousePosition);
		GUI.BeginGroup(position);
		this._viewRect.Set(this._viewRect.x, this._viewRect.y, this._viewRect.width, (float)(60 * this._camouflages.Count + 3));
		GUI.DrawTexture(this._scrollRect, this._fill);
		this._scrollPosition = GUI.BeginScrollView(this._scrollRect, this._scrollPosition, this._viewRect);
		if (this._camouflages != null)
		{
			for (int i = 0; i < this._camouflages.Count; i++)
			{
				Vector2 pos = new Vector2(this._scrollRect.xMin + 3f, this._scrollRect.yMin + 3f + (float)(i * 60));
				this.DrawCamouflageBar(pos, this._camouflages[i]);
			}
		}
		GUI.EndScrollView();
		GUI.EndGroup();
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x0002FE74 File Offset: 0x0002E074
	private void FillList()
	{
		if (this._camouflages == null)
		{
			this._camouflages = new List<CamouflageInfo>();
		}
		this._camouflages.Clear();
		foreach (KeyValuePair<int, CamouflageInfo> keyValuePair in ModsStorage.Instance().CharacterCamouflages)
		{
			if (Main.UserInfo.Mastering.WearStates.ContainsKey(keyValuePair.Key))
			{
				this._camouflages.Add(keyValuePair.Value);
			}
		}
		foreach (KeyValuePair<int, CamouflageInfo> keyValuePair2 in ModsStorage.Instance().CharacterCamouflages)
		{
			if (!Main.UserInfo.Mastering.WearStates.ContainsKey(keyValuePair2.Key))
			{
				this._camouflages.Add(keyValuePair2.Value);
			}
		}
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002FFB8 File Offset: 0x0002E1B8
	private void EquipCamouflage(CamouflageInfo camouflageInfo, MasteringMod m, bool fitting = false)
	{
		string name = (!this._isBear) ? "USEC_LOD0" : "Bear_LOD0";
		foreach (Material material in this._charactersTransform.FindChild(name).GetComponent<MeshRenderer>().materials)
		{
			if (material.shader.name.ToLower().Contains("bumped specular mask"))
			{
				material.SetTexture("_DetailTex", (m == null) ? null : m.Texture);
				material.SetTextureScale("_DetailTex", new Vector2(camouflageInfo.Scale, camouflageInfo.Scale));
			}
		}
		if (!fitting)
		{
			this._equippedId = camouflageInfo.Id;
			this._fittingId = camouflageInfo.Id;
		}
		else
		{
			this._fittingId = camouflageInfo.Id;
		}
		this._modelSwaped = false;
		Audio.Play(this._equipedAudioClip);
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x000300AC File Offset: 0x0002E2AC
	private void DrawCamouflageBar(Vector2 pos, CamouflageInfo cInfo)
	{
		Texture2D image = (!cInfo.Locked) ? this._unlocked : this._locked;
		if (this._equippedId == cInfo.Id)
		{
			image = this._equipped;
		}
		Texture2D texture2D = (!cInfo.Locked) ? this._grayBack : this._redBack;
		Rect position = new Rect(pos.x + 2f, pos.y + 2f, (float)texture2D.width, (float)texture2D.height);
		GUI.DrawTexture(new Rect(pos.x, pos.y, 456f, 56f), image);
		GUI.DrawTexture(position, texture2D);
		GUI.DrawTexture(new Rect(pos.x + 4f, pos.y + 5f, (float)this._cell.width, (float)this._cell.height), this._cell);
		if (cInfo.Locked)
		{
			Rect position2 = new Rect(pos.x + (float)texture2D.width - (float)this.gui.locked.width, pos.y + (float)texture2D.height - (float)this.gui.locked.height, (float)this.gui.locked.width, (float)this.gui.locked.height);
			GUI.DrawTexture(position2, this.gui.locked);
		}
		string textColor = "#FFFFFF";
		if (RarityColors.Colors != null)
		{
			textColor = RarityColors.Colors[cInfo.Rarity.ToString().ToLower()].ToString();
		}
		this.gui.TextLabel(new Rect(pos.x + 54f, pos.y + 14f, 256f, 14f), cInfo.ShortName, 14, textColor, TextAnchor.MiddleLeft, true);
		this.gui.TextLabel(new Rect(pos.x + 54f, pos.y + 32f, 256f, 14f), cInfo.FullName, 14, "#797979_D", TextAnchor.MiddleLeft, true);
		MasteringMod modById = ModsStorage.Instance().GetModById(cInfo.Id);
		if (modById != null && modById.BigIcon)
		{
			GUI.DrawTexture(new Rect(pos.x + 10f, pos.y + 10f, (float)modById.BigIcon.width, (float)modById.BigIcon.height), modById.BigIcon);
		}
		if (this._equippedId == cInfo.Id)
		{
			GUI.DrawTexture(new Rect(pos.x + 3f, pos.y + 4f, (float)this._equippedFrame.width, (float)this._equippedFrame.height), this._equippedFrame);
		}
		if (this._fittingId == cInfo.Id && this._fittingId != this._equippedId)
		{
			GUI.DrawTexture(new Rect(pos.x - 31f, pos.y - 32f, (float)this._selectedFrame.width, (float)this._selectedFrame.height), this._selectedFrame);
		}
		Texture2D background = this._saveBtnStyle.normal.background;
		if (cInfo.Locked)
		{
			string text = Helpers.SeparateNumericString(cInfo.Price.ToString());
			if (cInfo.Free)
			{
				if (cInfo.FreeType == FreeType.Level)
				{
					text = string.Concat(new object[]
					{
						cInfo.FreeCount,
						" LVL ",
						Language.Or.ToLower(),
						" ",
						(cInfo.CurrencyType != Currency.Gp) ? Helpers.KFormat(cInfo.Price) : Helpers.SeparateNumericString(cInfo.Price.ToString())
					});
				}
				if (cInfo.FreeType == FreeType.Achievement)
				{
					text = string.Concat(new string[]
					{
						Language.Achievement,
						" ",
						Language.Or.ToLower(),
						" ",
						(cInfo.CurrencyType != Currency.Gp) ? Helpers.KFormat(cInfo.Price) : Helpers.SeparateNumericString(cInfo.Price.ToString())
					});
				}
			}
			float num = this.gui.CalcWidth(text, this.gui.fontDNC57, 16);
			Texture2D texture2D2 = this._currencyIcons[(int)cInfo.CurrencyType];
			Rect rect = new Rect(pos.x + 320f - (196f - (float)background.width + (float)texture2D2.width) / 2f, pos.y + 4f, 196f, 16f);
			Rect position3 = new Rect(new Rect(rect.xMax - (rect.width - num) / 2f, pos.y + (float)((cInfo.CurrencyType != Currency.Mp) ? 1 : 5), (float)texture2D2.width, (float)texture2D2.height));
			GUI.DrawTexture(position3, texture2D2);
			this.gui.TextLabel(rect, text, 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
			if (GUI.Button(new Rect(pos.x + 320f, pos.y + 22f, (float)background.width, (float)background.height), ((!cInfo.Available) ? Language.Purchase : Language.GetFree).ToUpper(), this._saveBtnStyle))
			{
				EventFactory.Call("ShowPopup", new Popup(WindowsID.PurchaseCamouflage, Language.PopupCamouflageBuyHeader, Language.PopupCamouflageBuyBody, delegate()
				{
					EventFactory.Call("HidePopup", new Popup(WindowsID.Load, Language.PopupCamouflageBuyHeader, Language.PopupCamouflageBuyComplete, PopupState.information, false, false, string.Empty, string.Empty));
				}, PopupState.purchaseCamouflage, false, true, new object[]
				{
					cInfo
				}));
			}
			if (GUI.enabled && cInfo.Free && cInfo.FreeType == FreeType.Achievement && rect.Contains(Event.current.mousePosition))
			{
				AchievementInfo achievementInfo = Main.UserInfo.achievementsInfos[cInfo.FreeCount];
				float a = MainGUI.Instance.CalcWidth(achievementInfo.description, MainGUI.Instance.fontTahoma, 9);
				float b = MainGUI.Instance.CalcWidth(achievementInfo.name + ":", MainGUI.Instance.fontTahoma, 9);
				float num2 = Mathf.Max(a, b) + 5f;
				Rect position4 = new Rect(Event.current.mousePosition.x - num2, Event.current.mousePosition.y - 32f, num2, 32f);
				this.gui.color = new Color(1f, 1f, 1f, base.visibility * 0.9f);
				GUI.DrawTexture(position4, MainGUI.Instance.black);
				this.gui.color = new Color(1f, 1f, 1f, base.visibility);
				CWGUI.p.standartTahoma9.normal.textColor = Helpers.HexToColor("4AA5FF");
				GUI.Label(new Rect(position4.x + 5f, position4.y - 9f, position4.width, position4.height), achievementInfo.name + ":", CWGUI.p.standartTahoma9);
				CWGUI.p.standartTahoma9.normal.textColor = Colors.RadarWhite;
				GUI.Label(new Rect(position4.x + 5f, position4.y + 5f, position4.width, position4.height), achievementInfo.description, CWGUI.p.standartTahoma9);
			}
		}
		else if (this._equippedId != cInfo.Id)
		{
			if (GUI.Button(new Rect(pos.x + 320f, pos.y + 12f, (float)background.width, (float)background.height), Language.Select.ToUpper(), this._saveBtnStyle))
			{
				this._camouflageInfo = cInfo;
				this._masteringMod = modById;
				this.EquipCamouflage(cInfo, modById, false);
			}
		}
		else
		{
			this.gui.TextLabel(new Rect(pos.x + 320f, pos.y + 12f, (float)background.width, (float)background.height), Language.Equipped, 16, "#797979", TextAnchor.MiddleCenter, true);
		}
		if (this._mouseInList && position.Contains(Event.current.mousePosition) && Input.GetMouseButtonDown(0) && !PopupGUI.IsAnyPopupShow && GUI.enabled)
		{
			this._camouflageInfo = cInfo;
			this._masteringMod = modById;
			this.EquipCamouflage(cInfo, modById, true);
		}
		if (this._modelSwaped)
		{
			this.EquipCamouflage(this._camouflageInfo, this._masteringMod, true);
		}
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x00030A00 File Offset: 0x0002EC00
	private void ShowCharacter()
	{
		base.CancelInvoke();
		this.Clear();
		if (this._cameraObject == null)
		{
			this._cameraObject = SingletoneForm<PoolManager>.Instance["characterViewer"].Spawn();
			this._cameraObject.transform.parent = base.transform;
			this._cameraObject.GetComponent<Camera>().targetTexture = MainGUI.Instance.CharacterViewer;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		foreach (Light light in componentsInChildren)
		{
			light.enabled = true;
		}
		CameraListener.Enable(this._cameraObject);
		this.isUpdating = true;
		this._cameraObject.transform.FindChild("WV_glow").gameObject.SetActive(false);
		this._stateName = Loader.DownloadCharacterView();
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00030AE4 File Offset: 0x0002ECE4
	private void InitializeCharacter()
	{
		this._characterInitialized = true;
		Debug.Log(GameObject.Find("add"));
		GameObject gameObject = GameObject.Find("add");
		if (gameObject)
		{
			this._charactersTransform = gameObject.transform.FindChild("characters/Characters_still");
			if (this._charactersTransform)
			{
				this._charactersTransform.gameObject.SetActive(true);
				this.SwapCharacter(true);
			}
		}
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00030B5C File Offset: 0x0002ED5C
	private void SwapCharacter(bool isBear = true)
	{
		if (!this._charactersTransform)
		{
			throw new Exception("Characters transform is null");
		}
		this._charactersTransform.FindChild("Bear_LOD0").gameObject.SetActive(isBear);
		this._charactersTransform.FindChild("USEC_LOD0").gameObject.SetActive(!isBear);
		this._isBear = isBear;
		this._modelSwaped = true;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x00030BCC File Offset: 0x0002EDCC
	private void HideCharacter(float obj)
	{
		base.Invoke("DelayedHide", obj);
		this.isUpdating = false;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x00030BE4 File Offset: 0x0002EDE4
	public override void Clear()
	{
		base.Clear();
		if (this._cameraObject)
		{
			CameraListener.Disable(this._cameraObject);
			PoolManager.Despawn(this._cameraObject);
			this._cameraObject = null;
		}
		if (this._characterObject)
		{
			Utility.SetLayerRecursively(this._characterObject, 0);
			PoolManager.Despawn(this._characterObject);
			this._characterObject = null;
		}
		Light[] componentsInChildren = this.gui.gameObject.GetComponentsInChildren<Light>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].enabled = false;
		}
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00030C80 File Offset: 0x0002EE80
	public override void OnConnected()
	{
		this.Clear();
		base.OnConnected();
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00030C90 File Offset: 0x0002EE90
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
		if (!this._charactersTransform)
		{
			return;
		}
		if (this._mouseInRect && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
		{
			this._clickedInRect = true;
		}
		if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
		{
			this._clickedInRect = false;
		}
		if (this._clickedInRect && Input.GetMouseButton(0))
		{
			this._charactersTransform.RotateAroundLocal(Vector3.up, -Time.deltaTime * Input.GetAxis("Mouse X") * 10f);
		}
		else if (this._clickedInRect && Input.GetMouseButton(1))
		{
			if (((double)this._charactersTransform.position.y < -1.5 && Input.GetAxis("Mouse Y") < 0f) || ((double)this._charactersTransform.position.y > -0.6 && Input.GetAxis("Mouse Y") > 0f))
			{
				return;
			}
			this._charactersTransform.Translate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse Y") * 2f);
		}
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00030E04 File Offset: 0x0002F004
	[Obfuscation(Exclude = true)]
	private void DelayedHide()
	{
		this.Clear();
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x00030E0C File Offset: 0x0002F00C
	public void DrawCharacter()
	{
		this.gui.color = new Color(1f, 1f, 1f, base.visibility);
		if ((double)base.visibility > 0.75)
		{
			this._charRect.Set(0f, 0f, (float)Screen.width, (float)Screen.height);
			this.gui.BeginGroup(this._charRect, this.windowID != this.gui.FocusedWindow);
			Vector2 zero = Vector2.zero;
			Vector2 size = new Vector2((float)this.gui.CharacterViewer.width, (float)this.gui.CharacterViewer.height);
			this.gui.PictureSizedNoBlend(zero, this.gui.CharacterViewer, size, false);
			Rect rect = new Rect(zero.x, zero.y, size.x, size.y);
			this._mouseInRect = rect.Contains(Event.current.mousePosition);
			if (Loader.Progress(this._stateName) < 1f)
			{
				Debug.Log("load in progress");
			}
			if (!this._characterInitialized && CamouflageGUI.AddDownloaded)
			{
				this.InitializeCharacter();
			}
			this.gui.EndGroup();
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x00030F64 File Offset: 0x0002F164
	public void DrawSwapButtons()
	{
		if (!this._characterInitialized)
		{
			return;
		}
		Texture2D background = this._usecBtn.normal.background;
		Rect position = new Rect(20f, 40f, (float)background.width, (float)background.height);
		background = this._bearBtn.normal.background;
		Rect position2 = new Rect(240f, 35f, (float)background.width, (float)background.height);
		Rect rect = new Rect(20f + (position.width - (float)this._glow.width) / 2f, 35f + (position.height - (float)this._glow.height) / 2f, (float)this._glow.width, (float)this._glow.height);
		Rect rect2 = new Rect(240f + (position2.width - (float)this._glow.width) / 2f, 35f + (position2.height - (float)this._glow.height) / 2f, (float)this._glow.width, (float)this._glow.height);
		GUI.DrawTexture((!this._isBear) ? rect : rect2, this._glow);
		Color color = this.gui.color;
		if (this._isBear)
		{
			this.gui.color = Colors.alpha(color, (!position.Contains(Event.current.mousePosition)) ? 0.5f : 0.75f);
		}
		if (GUI.Button(position, string.Empty, this._usecBtn))
		{
			this.SwapCharacter(false);
		}
		this.gui.color = color;
		if (!this._isBear)
		{
			this.gui.color = Colors.alpha(color, (!position2.Contains(Event.current.mousePosition)) ? 0.5f : 0.75f);
		}
		if (GUI.Button(position2, string.Empty, this._bearBtn))
		{
			this.SwapCharacter(true);
		}
		this.gui.color = color;
	}

	// Token: 0x04000668 RID: 1640
	private GUISkin _tempSkin;

	// Token: 0x04000669 RID: 1641
	[SerializeField]
	private GUISkin _customSkin;

	// Token: 0x0400066A RID: 1642
	[SerializeField]
	private Texture2D _cell;

	// Token: 0x0400066B RID: 1643
	[SerializeField]
	private Texture2D _equippedFrame;

	// Token: 0x0400066C RID: 1644
	[SerializeField]
	private Texture2D _selectedFrame;

	// Token: 0x0400066D RID: 1645
	[SerializeField]
	private Texture2D _grayBack;

	// Token: 0x0400066E RID: 1646
	[SerializeField]
	private Texture2D _redBack;

	// Token: 0x0400066F RID: 1647
	[SerializeField]
	private Texture2D _fill;

	// Token: 0x04000670 RID: 1648
	[SerializeField]
	private Texture2D _locked;

	// Token: 0x04000671 RID: 1649
	[SerializeField]
	private Texture2D _unlocked;

	// Token: 0x04000672 RID: 1650
	[SerializeField]
	private Texture2D _equipped;

	// Token: 0x04000673 RID: 1651
	[SerializeField]
	private Texture2D _gp;

	// Token: 0x04000674 RID: 1652
	[SerializeField]
	private Texture2D _cr;

	// Token: 0x04000675 RID: 1653
	[SerializeField]
	private Texture2D _mp;

	// Token: 0x04000676 RID: 1654
	[SerializeField]
	private GUIStyle _saveBtnStyle;

	// Token: 0x04000677 RID: 1655
	[SerializeField]
	private GUIStyle _bearBtn;

	// Token: 0x04000678 RID: 1656
	[SerializeField]
	private GUIStyle _usecBtn;

	// Token: 0x04000679 RID: 1657
	[SerializeField]
	private GUIStyle _emptyBtn;

	// Token: 0x0400067A RID: 1658
	[SerializeField]
	private Texture2D _glow;

	// Token: 0x0400067B RID: 1659
	[SerializeField]
	private AudioClip _equipedAudioClip;

	// Token: 0x0400067C RID: 1660
	private Texture2D[] _currencyIcons;

	// Token: 0x0400067D RID: 1661
	private List<CamouflageInfo> _camouflages;

	// Token: 0x0400067E RID: 1662
	private int _equippedId;

	// Token: 0x0400067F RID: 1663
	private int _fittingId;

	// Token: 0x04000680 RID: 1664
	private bool _modelSwaped;

	// Token: 0x04000681 RID: 1665
	private CamouflageInfo _camouflageInfo;

	// Token: 0x04000682 RID: 1666
	private MasteringMod _masteringMod;

	// Token: 0x04000683 RID: 1667
	private Rect _scrollRect = new Rect(0f, 0f, 478f, 525f);

	// Token: 0x04000684 RID: 1668
	private Rect _viewRect = new Rect(0f, 0f, 456f, 525f);

	// Token: 0x04000685 RID: 1669
	private Vector2 _scrollPosition = Vector2.zero;

	// Token: 0x04000686 RID: 1670
	private bool _mouseInList;

	// Token: 0x04000687 RID: 1671
	private Rect _charRect;

	// Token: 0x04000688 RID: 1672
	private bool _mouseInRect;

	// Token: 0x04000689 RID: 1673
	private bool _clickedInRect;

	// Token: 0x0400068A RID: 1674
	private float _min;

	// Token: 0x0400068B RID: 1675
	private float _max;

	// Token: 0x0400068C RID: 1676
	private float _camCurrentRad;

	// Token: 0x0400068D RID: 1677
	private float _camTargetRad;

	// Token: 0x0400068E RID: 1678
	private Vector3 _camEuler;

	// Token: 0x0400068F RID: 1679
	private Vector3 _center;

	// Token: 0x04000690 RID: 1680
	private string _stateName;

	// Token: 0x04000691 RID: 1681
	private GameObject _cameraObject;

	// Token: 0x04000692 RID: 1682
	private GameObject _characterObject;

	// Token: 0x04000693 RID: 1683
	private bool _characterInitialized;

	// Token: 0x04000694 RID: 1684
	private Transform _charactersTransform;

	// Token: 0x04000695 RID: 1685
	public static bool AddDownloaded;

	// Token: 0x04000696 RID: 1686
	private bool _isBear;
}
