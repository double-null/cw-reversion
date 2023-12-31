using System;
using System.Collections.Generic;
using UnityEngine;

namespace namespaceMainGUI
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	internal class Package
	{
		// Token: 0x06000933 RID: 2355 RVA: 0x0005B8BC File Offset: 0x00059ABC
		private void RefreshPosition(PackageElement element, float x, float y)
		{
			element.rectTexture.Set(x, y + (float)((this.boxback.height - element.texture.height) / 5) + (float)((element.Type != PackageItemType.credits) ? 0 : 10), (float)element.texture.width, (float)element.texture.height);
			element.rectDescr.Set(element.rectTexture.xMin + (element.rectTexture.width - this.descrWidth) / 2f, y + 48f, this.descrWidth, this.decrHeight);
			element.rectRentTime.Set(element.rectTexture.xMin + element.rectTexture.width / 2f - (float)(this.styleRentTime.normal.background.width / 2), element.rectDescr.yMax - 2f, (float)this.styleRentTime.normal.background.width, (float)this.styleRentTime.normal.background.height);
			element.rectRamka.Set(element.rectTexture.x, element.rectTexture.y, (float)this.SkillRamka.width, (float)this.SkillRamka.height);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0005BA18 File Offset: 0x00059C18
		private void DrawElement(PackageElement element)
		{
			GUI.DrawTexture(element.rectTexture, element.texture);
			GUI.Label(element.rectRentTime, element.rentTime, this.styleRentTime);
			GUI.Label(element.rectDescr, element.description, this.styleDescription);
			if (element.Type == PackageItemType.skill && element.Unlocked)
			{
				GUI.DrawTexture(element.rectRamka, this.SkillRamka);
			}
			if (element.Unlocked)
			{
				if (!element.RecalcWidthOnce)
				{
					element.hint.ReCalcWidth();
					element.RecalcWidthOnce = true;
				}
				element.hint.OnGUI(element.rectTexture.x + (float)((element.texture.width - element.hint.width) / 2), element.rectTexture.y);
			}
			else
			{
				element.skillhint.OnGUI(element.rectTexture.x + (float)((element.texture.width - element.hint.width) / 2), element.rectTexture.y);
			}
			element.onHoveHint.OnGUI(element.rectTexture.x + (float)((element.texture.width - element.hint.width) / 2), element.rectTexture.y);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0005BB70 File Offset: 0x00059D70
		private void DrawSet(Rect rect, PackageSet set)
		{
			this.gui = MainGUI.Instance;
			if (set.IsGP)
			{
				this.tmpwidth = this.gui.CalcWidth("0", this.gui.fontDNC57, 18);
			}
			else
			{
				this.tmpwidth = this.gui.CalcWidth("00", this.gui.fontDNC57, 16);
			}
			this.rectBox.Set(rect.x, rect.y, (float)this.box.width, (float)this.box.height);
			this.rectBoxBack.Set(rect.x, rect.y + (float)this.box.height + 2f, (float)this.boxback.width, (float)this.boxback.height);
			set.rectSetName.Set(rect.x + this.rectBox.width + 5f, rect.y, 300f, (float)this.box.height);
			GUI.DrawTexture(this.rectBox, this.box);
			GUI.DrawTexture(this.rectBoxBack, this.boxback);
			GUI.Label(set.rectSetName, set.setname.ToUpper(), this.boxNameStyle);
			GUI.enabled = !Main.IsGameLoaded;
			string textColor = "#ffffff";
			float num = 1.5f;
			if (set.IsGP)
			{
				textColor = "#f5c026";
				num = 2.5f;
			}
			if (MainGUI.Instance.Button(new Vector2(rect.xMax - (float)this.bTxrs.idle.width, rect.yMax - (float)this.bTxrs.idle.height), this.bTxrs.idle, this.bTxrs.over, this.bTxrs.clicked, set.price + "   .", 16, textColor, TextAnchor.MiddleCenter, null, null, null, null).Clicked)
			{
				BuyBox.boxIndexCached = set.ID;
				BuyBox.isGP = set.IsGP;
				BuyBox.price = set.price;
				BuyBox.BoxName = set.setname.ToUpper();
				BuyBox.pack = set.Pack;
				BuyBox.toRefresh = set;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BuyBox, string.Empty, PopupState.buyBox, false, true, string.Empty, string.Empty));
			}
			this.gui.Picture(new Vector2(rect.xMax - this.tmpwidth * num, rect.yMax - (float)this.bTxrs.idle.height / 1.1f), (!set.IsGP) ? this.gui.crIcon : this.gui.gldIcon);
			GUI.enabled = true;
			this.lastWidth = 0f;
			for (int i = 0; i < set.items.Count; i++)
			{
				this.leftOffset = ((float)this.boxback.width - set.maxWidth) / (float)(set.items.Count * 2) + 5f;
				if (this.lastWidth < rect.width)
				{
					if (i != 0)
					{
						set.rectPlus.Set(rect.xMin + this.lastWidth, rect.y + (float)this.box.height, this.leftOffset, (float)this.boxback.height);
						GUI.Label(set.rectPlus, "+", this.stylePlus);
					}
					else if (set.items[i].texture.width < 100)
					{
						this.lastWidth += 50f;
					}
					this.RefreshPosition(set.items[i], rect.xMin + this.lastWidth + this.leftOffset, rect.y + (float)this.box.height);
					this.DrawElement(set.items[i]);
				}
				this.lastWidth += set.items[i].Width + this.leftOffset;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0005BFDC File Offset: 0x0005A1DC
		public void OnStart()
		{
			this.SetRectSize(ref this.rectSet1);
			this.SetRectSize(ref this.rectSet2);
			this.SetRectSize(ref this.rectSet3);
			this.SetRectSize(ref this.rectSet4);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0005C01C File Offset: 0x0005A21C
		private void SetRectSize(ref Rect onerect)
		{
			onerect.Set(this.rect.x, onerect.y, this.rect.width, onerect.height);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0005C054 File Offset: 0x0005A254
		public void OnGUI()
		{
			if (this.black != null)
			{
				GUI.DrawTexture(this.rect, this.black, ScaleMode.StretchToFill);
			}
			if (this.packageSets.Count > 0)
			{
				this.DrawSet(this.rectSet1, this.packageSets[0]);
			}
			if (this.packageSets.Count > 1)
			{
				this.DrawSet(this.rectSet2, this.packageSets[1]);
			}
			if (this.packageSets.Count > 2)
			{
				this.DrawSet(this.rectSet3, this.packageSets[2]);
			}
			if (this.packageSets.Count > 3)
			{
				this.DrawSet(this.rectSet4, this.packageSets[3]);
			}
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0005C128 File Offset: 0x0005A328
		public void OnUpdate()
		{
			this.RecalcSets();
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0005C130 File Offset: 0x0005A330
		private void RecalcSets()
		{
			if (this.lasSelectedSet != MainGUI.Instance.SelectedSet && MainGUI.Instance.SelectedSet < Main.UserInfo.packageInfo.Length)
			{
				this.lasSelectedSet = MainGUI.Instance.SelectedSet;
				this.ParsePackageInfo(Main.UserInfo.packageInfo[MainGUI.Instance.SelectedSet].package);
			}
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0005C1A0 File Offset: 0x0005A3A0
		private void ParsePackageInfo(List<Packages> p)
		{
			this.packageSets.Clear();
			int num = 0;
			while (num < p.Count && num < 4)
			{
				this.packageSets.Add(new PackageSet(p[num]));
				num++;
			}
		}

		// Token: 0x04000A63 RID: 2659
		private float tmpwidth;

		// Token: 0x04000A64 RID: 2660
		public Rect rectSet1 = default(Rect);

		// Token: 0x04000A65 RID: 2661
		public Rect rectSet2 = default(Rect);

		// Token: 0x04000A66 RID: 2662
		public Rect rectSet3 = default(Rect);

		// Token: 0x04000A67 RID: 2663
		public Rect rectSet4 = default(Rect);

		// Token: 0x04000A68 RID: 2664
		public MainGUI gui;

		// Token: 0x04000A69 RID: 2665
		public Texture2D box;

		// Token: 0x04000A6A RID: 2666
		public Texture2D boxback;

		// Token: 0x04000A6B RID: 2667
		public Texture2D SkillRamka;

		// Token: 0x04000A6C RID: 2668
		private Rect rectBox = default(Rect);

		// Token: 0x04000A6D RID: 2669
		private Rect rectBoxBack = default(Rect);

		// Token: 0x04000A6E RID: 2670
		public ButtonTextures bTxrs = new ButtonTextures();

		// Token: 0x04000A6F RID: 2671
		public Rect rect = default(Rect);

		// Token: 0x04000A70 RID: 2672
		public Texture2D black;

		// Token: 0x04000A71 RID: 2673
		public GUIStyle styleRentTime = new GUIStyle();

		// Token: 0x04000A72 RID: 2674
		public GUIStyle styleDescription = new GUIStyle();

		// Token: 0x04000A73 RID: 2675
		public GUIStyle boxNameStyle = new GUIStyle();

		// Token: 0x04000A74 RID: 2676
		public GUIStyle stylePlus = new GUIStyle();

		// Token: 0x04000A75 RID: 2677
		public List<PackageSet> packageSets = new List<PackageSet>();

		// Token: 0x04000A76 RID: 2678
		private int lasSelectedSet = -1;

		// Token: 0x04000A77 RID: 2679
		private float descrWidth = 140f;

		// Token: 0x04000A78 RID: 2680
		private float decrHeight = 20f;

		// Token: 0x04000A79 RID: 2681
		private float lastWidth;

		// Token: 0x04000A7A RID: 2682
		private float leftOffset;
	}
}
