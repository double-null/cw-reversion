using System;
using UnityEngine;

namespace namespaceMainGUI
{
	// Token: 0x0200015F RID: 351
	[Serializable]
	internal class PackageElement : GetText, HoverEvent
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0005B23C File Offset: 0x0005943C
		public PackageElement(PackageItem p)
		{
			this.p = p;
			this.hint.obj = this;
			if (p.type == PackageItemType.skill)
			{
				this.texture = CarrierGUI.I.Class_skills[p.ID];
				this.rentTime = ((p.rentTime <= 0) ? Language.Forever.ToLower() : (p.rentTime.ToString() + " " + Language.DN + "."));
				if (Main.UserInfo.skillsInfos[p.ID].bonus.Length > 1 && !Main.UserInfo.skillsInfos[p.ID].Unlocked)
				{
					this.skillhint.Hint = Main.UserInfo.skillsInfos[p.ID].bonus;
					this.skillhint.rect.Set(0f, 0f, (float)this.texture.width, (float)this.texture.height);
					this.skillhint.xoffset = (float)(-(float)this.texture.width / 2);
					this.skillhint.width = this.texture.width * 2;
					this.skillhint.OnChangedRect();
				}
			}
			if (p.type == PackageItemType.weapon)
			{
				this.onHoveHint.hoverEvent = this;
				this.texture = MainGUI.Instance.weapon_unlocked[p.ID];
				this.rentTime = ((p.rentTime <= 0) ? Language.Forever.ToLower() : (p.rentTime.ToString() + " " + Language.DN + "."));
			}
			if (p.type == PackageItemType.credits)
			{
				this.texture = MainGUI.Instance.cr_BIG;
				this.rentTime = p.ID.ToString();
			}
			this.onHoveHint.rect.Set(0f, 0f, (float)this.texture.width, (float)this.texture.height);
			this.onHoveHint.ReCalcWidth();
			this.hint.rect.Set(0f, 0f, (float)this.texture.width, (float)this.texture.height);
			this.hint.ReCalcWidth();
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0005B510 File Offset: 0x00059710
		public void RecalcSize()
		{
			this.hint.ReCalcWidth();
			if (this.p.type == PackageItemType.skill)
			{
				this.hint.ReCalcWidth();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0005B53C File Offset: 0x0005973C
		public string description
		{
			get
			{
				switch (this.p.type)
				{
				case PackageItemType.skill:
					return Main.UserInfo.skillsInfos[this.p.ID].name;
				case PackageItemType.weapon:
					return Main.UserInfo.weaponsStates[this.p.ID].CurrentWeapon.ShortName;
				case PackageItemType.credits:
					return "CREDITS";
				default:
					return string.Empty;
				}
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0005B5B8 File Offset: 0x000597B8
		public bool Unlocked
		{
			get
			{
				if (this.p.type == PackageItemType.skill)
				{
					return Main.UserInfo.skillsInfos[this.p.ID].Unlocked;
				}
				return this.p.type == PackageItemType.weapon && Main.UserInfo.weaponsStates[this.p.ID].Unlocked;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0005B620 File Offset: 0x00059820
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0005B628 File Offset: 0x00059828
		public bool RecalcWidthOnce { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0005B634 File Offset: 0x00059834
		public PackageItemType Type
		{
			get
			{
				return this.p.type;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0005B644 File Offset: 0x00059844
		public float Width
		{
			get
			{
				return (float)this.texture.width;
			}
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0005B654 File Offset: 0x00059854
		public string GetText()
		{
			if (this.p.rentTime > 0)
			{
				if (this.p.type == PackageItemType.weapon && Main.UserInfo.weaponsStates[this.p.ID].rentEnd - HtmlLayer.serverUtc > 0)
				{
					return MainGUI.Instance.SecondsTostringDDHHMMSS(Main.UserInfo.weaponsStates[this.p.ID].rentEnd - HtmlLayer.serverUtc);
				}
				if (this.p.type == PackageItemType.skill && Main.UserInfo.skillsInfos[this.p.ID].rentEnd - HtmlLayer.serverUtc > 0)
				{
					return MainGUI.Instance.SecondsTostringDDHHMMSS(Main.UserInfo.skillsInfos[this.p.ID].rentEnd - HtmlLayer.serverUtc);
				}
			}
			return Language.Buyed;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0005B740 File Offset: 0x00059940
		public void OnHover()
		{
			if (this.p.type == PackageItemType.weapon)
			{
				MainGUI.Instance.weapHelpAlpha.Show(0.3f, 0f);
				MainGUI.Instance.weapHelpMousePos = Input.mousePosition;
				MainGUI.Instance.wtask_mode_weapstate = false;
				MainGUI.Instance.newWeapState = Main.UserInfo.weaponsStates[this.p.ID];
				MainGUI.Instance.onHoverWeaponInSet = true;
			}
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0005B7C4 File Offset: 0x000599C4
		public void OnNormal()
		{
			MainGUI.Instance.onHoverWeaponInSet = true;
		}

		// Token: 0x04000A55 RID: 2645
		public TextHint hint = new TextHint();

		// Token: 0x04000A56 RID: 2646
		public TextHint skillhint = new TextHint();

		// Token: 0x04000A57 RID: 2647
		public TextHint onHoveHint = new TextHint();

		// Token: 0x04000A58 RID: 2648
		private PackageItem p;

		// Token: 0x04000A59 RID: 2649
		public Texture2D texture;

		// Token: 0x04000A5A RID: 2650
		public string rentTime = string.Empty;

		// Token: 0x04000A5B RID: 2651
		public Rect rectTexture = default(Rect);

		// Token: 0x04000A5C RID: 2652
		public Rect rectRentTime = default(Rect);

		// Token: 0x04000A5D RID: 2653
		public Rect rectDescr = default(Rect);

		// Token: 0x04000A5E RID: 2654
		public Rect rectRamka = default(Rect);
	}
}
