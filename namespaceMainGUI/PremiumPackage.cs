using System;
using System.Collections.Generic;
using UnityEngine;

namespace namespaceMainGUI
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	internal class PremiumPackage
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x0005C2F4 File Offset: 0x0005A4F4
		private Rect groupRect
		{
			get
			{
				if (Globals.I.AdEnabled)
				{
					return new Rect((float)((Screen.width - 800) / 2), this.AdvPos.y, 800f, this.AdvPos.y + this.AdvPos.height);
				}
				if (CVars.realm == "fr")
				{
					this.AdvPos = this.FrAdvPos;
					return new Rect((float)((Screen.width - 800) / 2), (float)((Screen.height - 800) / 2 - 14), 800f, this.AdvPos.y + this.AdvPos.height);
				}
				return new Rect((float)((Screen.width - 800) / 2), (float)((Screen.height - 800) / 2), 800f, 1000f);
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0005C3D8 File Offset: 0x0005A5D8
		public void OnStart()
		{
			this.InitRect();
			this.ParseSet(Main.UserInfo.packageInfo[0].package[0]);
			for (int i = 0; i < Main.UserInfo.packageInfo[0].package.Count; i++)
			{
				this.pe = new PackageElement(Main.UserInfo.packageInfo[0].package[0].items[i]);
				this.peList.Add(this.pe);
			}
			this.init = true;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0005C470 File Offset: 0x0005A670
		public void OnGUI()
		{
			if (!this.init)
			{
				this.OnStart();
			}
			if (PopupGUI.IsAnyPopupShow)
			{
				GUI.enabled = false;
			}
			else
			{
				GUI.enabled = true;
			}
			GUI.BeginGroup(this.groupRect);
			GUI.DrawTexture(this.AdvPos, this.Adv_fill);
			this.DrawSet(this.SetRect, this.set);
			GUI.EndGroup();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0005C4DC File Offset: 0x0005A6DC
		public void OnUpdate()
		{
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0005C4E0 File Offset: 0x0005A6E0
		private void ParseSet(Packages p)
		{
			this.set = new PackageSet(p);
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x0005C4F0 File Offset: 0x0005A6F0
		private void DrawSet(Rect r, PackageSet set)
		{
			if (!this.once)
			{
				this.CalculateOffset();
			}
			this.once = true;
			GUI.BeginGroup(this.AdvPos);
			GUI.DrawTexture(this.BoxRect, this.box);
			GUI.Label(this.SetNameRect, set.setname.ToUpper(), this.WhiteLabel);
			GUI.Label(this.SetDescriptionRect, set.setdescription, this.GrayLabel);
			GUI.DrawTexture(this.BackRect, this.back);
			for (int i = 0; i < this.peList.Count; i++)
			{
				this.DrawElement(this.peList[i], i);
			}
			this.DrawButton();
			GUI.EndGroup();
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0005C5B0 File Offset: 0x0005A7B0
		private void DrawButton()
		{
			GUI.DrawTexture(this.GlowRect, this.btnglow);
			if (Main.IsGameLoaded)
			{
				GUI.enabled = false;
			}
			else
			{
				GUI.enabled = true;
			}
			if (GUI.Button(this.BtnRect, this.set.price.ToString(), this.BuyBtnStyle))
			{
				BuyBox.boxIndexCached = this.set.ID;
				BuyBox.isGP = true;
				BuyBox.price = this.set.price;
				BuyBox.BoxName = this.set.setname.ToUpper();
				BuyBox.pack = this.set.Pack;
				BuyBox.toRefresh = this.set;
				EventFactory.Call("ShowPopup", new Popup(WindowsID.Invite, Language.BuyBox, string.Empty, PopupState.buyBox, false, true, string.Empty, string.Empty));
			}
			GUI.enabled = true;
			GUI.DrawTexture(this.GPIconRect, MainGUI.Instance.gldIcon);
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x0005C6AC File Offset: 0x0005A8AC
		private void CalculateOffset()
		{
			for (int i = 0; i < this.peList.Count; i++)
			{
				this.offset += (float)this.peList[i].texture.width;
			}
			this.offset = (this.BackRect.width - this.offset) * 0.5f - 30f;
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0005C720 File Offset: 0x0005A920
		private void DrawElement(PackageElement pe, int index)
		{
			GUI.DrawTexture(new Rect(this.offset + (float)(90 * index), (this.AdvPos.height - (float)pe.texture.height) * 0.5f + 10f, (float)pe.texture.width, (float)pe.texture.height), pe.texture);
			if (pe.Unlocked)
			{
				if (!pe.RecalcWidthOnce)
				{
					pe.hint.ReCalcWidth();
					pe.RecalcWidthOnce = true;
				}
				pe.hint.OnGUI(this.offset + (float)(90 * index) + (float)((pe.texture.width - pe.hint.width) / 2), (this.AdvPos.height - (float)pe.texture.height) * 0.5f + 10f);
			}
			else
			{
				pe.skillhint.OnGUI(this.offset + (float)(90 * index), (this.AdvPos.height - (float)pe.texture.height) * 0.5f + 10f);
			}
			pe.onHoveHint.OnGUI(this.offset + (float)(90 * index), (this.AdvPos.height - (float)pe.texture.height) * 0.5f + 10f);
			if (pe.Unlocked && pe.Type == PackageItemType.skill)
			{
				GUI.DrawTexture(new Rect(this.offset + (float)(90 * index), (this.AdvPos.height - (float)this.frame.height) * 0.5f + 10f, (float)this.frame.width, (float)this.frame.height), this.frame);
			}
			if (pe.Type == PackageItemType.weapon)
			{
				GUI.DrawTexture(new Rect(this.offset + (float)(90 * index) + (float)(pe.texture.width - this.rentBack.width) * 0.5f, 102f, (float)this.rentBack.width, (float)this.rentBack.height), this.rentBack);
				GUI.Label(new Rect(this.offset + (float)(90 * index), 85f, (float)pe.texture.width, 20f), pe.description, this.TahomaLabel);
				GUI.Label(new Rect(this.offset + (float)(90 * index), 96f, (float)pe.texture.width, 20f), pe.rentTime, this.TahomaGrayLabel);
			}
			else
			{
				GUI.DrawTexture(new Rect(this.offset + (float)(90 * index) - (float)(this.rentBack.width - pe.texture.width) * 0.5f, 102f, (float)this.rentBack.width, (float)this.rentBack.height), this.rentBack);
				GUI.Label(new Rect(this.offset + (float)(90 * index) - (float)(100 - pe.texture.width) * 0.5f, 85f, 100f, 20f), pe.description, this.TahomaLabel);
				GUI.Label(new Rect(this.offset + (float)(90 * index) - (float)(100 - pe.texture.width) * 0.5f, 96f, 100f, 20f), pe.rentTime, this.TahomaGrayLabel);
			}
			if (index < this.peList.Count - 1)
			{
				this.WhiteLabel.fontSize = 20;
				GUI.Label(new Rect(this.offset + (float)(90 * index) + (float)pe.texture.width + 15f, 60f, 20f, 20f), "+", this.WhiteLabel);
				this.WhiteLabel.fontSize = 14;
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0005CB1C File Offset: 0x0005AD1C
		private void InitRect()
		{
			this.BoxRect.Set(this.BoxRect.x, this.BoxRect.y, (float)this.box.width, (float)this.box.height);
			this.BackRect.Set(this.BackRect.x, this.BackRect.y, (float)this.back.width, (float)this.back.height);
			this.FrameRect.Set(this.FrameRect.x, this.FrameRect.y, (float)this.frame.width, (float)this.frame.height);
			this.GlowRect.Set(this.GlowRect.x, this.GlowRect.y, (float)this.btnglow.width, (float)this.btnglow.height);
			this.BtnRect.Set(this.BtnRect.x, this.BtnRect.y, (float)this.BuyBtnStyle.normal.background.width, (float)this.BuyBtnStyle.normal.background.height);
			this.GPIconRect.Set(this.GPIconRect.x, this.GPIconRect.y, (float)MainGUI.Instance.gldIcon.width, (float)MainGUI.Instance.gldIcon.height);
		}

		// Token: 0x04000A7B RID: 2683
		private bool once;

		// Token: 0x04000A7C RID: 2684
		private bool init;

		// Token: 0x04000A7D RID: 2685
		private PackageSet set;

		// Token: 0x04000A7E RID: 2686
		private PackageElement pe;

		// Token: 0x04000A7F RID: 2687
		private List<PackageElement> peList = new List<PackageElement>();

		// Token: 0x04000A80 RID: 2688
		public Rect AdvPos = default(Rect);

		// Token: 0x04000A81 RID: 2689
		public Rect FrAdvPos = default(Rect);

		// Token: 0x04000A82 RID: 2690
		public Rect SetRect = default(Rect);

		// Token: 0x04000A83 RID: 2691
		public Rect BoxRect = default(Rect);

		// Token: 0x04000A84 RID: 2692
		public Rect BackRect = default(Rect);

		// Token: 0x04000A85 RID: 2693
		public Rect FrameRect = default(Rect);

		// Token: 0x04000A86 RID: 2694
		public Rect GlowRect = default(Rect);

		// Token: 0x04000A87 RID: 2695
		public Rect GPIconRect = default(Rect);

		// Token: 0x04000A88 RID: 2696
		public Rect SetNameRect = default(Rect);

		// Token: 0x04000A89 RID: 2697
		public Rect SetDescriptionRect = default(Rect);

		// Token: 0x04000A8A RID: 2698
		public Rect BtnRect = default(Rect);

		// Token: 0x04000A8B RID: 2699
		public GUIStyle WhiteLabel = new GUIStyle();

		// Token: 0x04000A8C RID: 2700
		public GUIStyle GrayLabel = new GUIStyle();

		// Token: 0x04000A8D RID: 2701
		public GUIStyle TahomaLabel = new GUIStyle();

		// Token: 0x04000A8E RID: 2702
		public GUIStyle TahomaGrayLabel = new GUIStyle();

		// Token: 0x04000A8F RID: 2703
		public GUIStyle BuyBtnStyle = new GUIStyle();

		// Token: 0x04000A90 RID: 2704
		public Texture2D Adv_fill;

		// Token: 0x04000A91 RID: 2705
		public Texture2D box;

		// Token: 0x04000A92 RID: 2706
		public Texture2D back;

		// Token: 0x04000A93 RID: 2707
		public Texture2D frame;

		// Token: 0x04000A94 RID: 2708
		public Texture2D btnglow;

		// Token: 0x04000A95 RID: 2709
		public Texture2D rentBack;

		// Token: 0x04000A96 RID: 2710
		private float offset;
	}
}
