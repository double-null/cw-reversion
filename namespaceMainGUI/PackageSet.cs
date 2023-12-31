using System;
using System.Collections.Generic;
using UnityEngine;

namespace namespaceMainGUI
{
	// Token: 0x0200015B RID: 347
	[Serializable]
	internal class PackageSet
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x0005AE54 File Offset: 0x00059054
		public PackageSet(Packages p)
		{
			this.p = p;
			this.items.Clear();
			this.maxWidth = 0f;
			for (int i = 0; i < p.items.Length; i++)
			{
				PackageElement packageElement = new PackageElement(p.items[i]);
				this.maxWidth += packageElement.Width;
				this.items.Add(packageElement);
			}
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0005AF08 File Offset: 0x00059108
		public void Refresh()
		{
			for (int i = 0; i < this.items.Count; i++)
			{
				this.items[i].RecalcSize();
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0005AF44 File Offset: 0x00059144
		public Packages Pack
		{
			get
			{
				return this.p;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x0005AF4C File Offset: 0x0005914C
		public int ID
		{
			get
			{
				return this.p.ID;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0005AF5C File Offset: 0x0005915C
		public bool IsGP
		{
			get
			{
				return this.p.gp_price > 0;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0005AF74 File Offset: 0x00059174
		public string setname
		{
			get
			{
				return this.p.name;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x0005AF84 File Offset: 0x00059184
		public string setdescription
		{
			get
			{
				return this.p.description;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0005AF94 File Offset: 0x00059194
		public int price
		{
			get
			{
				if (this.IsGP)
				{
					return this.p.gp_price;
				}
				return this.p.cr_price;
			}
		}

		// Token: 0x04000A45 RID: 2629
		private Packages p;

		// Token: 0x04000A46 RID: 2630
		public Rect rectPlus = default(Rect);

		// Token: 0x04000A47 RID: 2631
		public Rect rectSetName = default(Rect);

		// Token: 0x04000A48 RID: 2632
		public Rect rectSetDescription = default(Rect);

		// Token: 0x04000A49 RID: 2633
		public float maxWidth;

		// Token: 0x04000A4A RID: 2634
		public List<PackageElement> items = new List<PackageElement>();
	}
}
