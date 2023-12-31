using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game.Foundation
{
	// Token: 0x020002A5 RID: 677
	internal class PersonalBankDiscount : Convertible
	{
		// Token: 0x06001303 RID: 4867 RVA: 0x000CDE84 File Offset: 0x000CC084
		public PersonalBankDiscount()
		{
			this._discountEnds = HtmlLayer.serverUtc + SingletoneComponent<Globals>.Instance.GpDiscountTime * 3600;
			this._discountValue = SingletoneComponent<Globals>.Instance.GpDiscount;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x000CDEC4 File Offset: 0x000CC0C4
		public bool DiscountExists
		{
			get
			{
				return this._discountEnds > HtmlLayer.serverUtc;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x000CDED4 File Offset: 0x000CC0D4
		public int DiscountEnds
		{
			get
			{
				return this._discountEnds;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x000CDEDC File Offset: 0x000CC0DC
		public int DiscountValue
		{
			get
			{
				return this._discountValue;
			}
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000CDEE4 File Offset: 0x000CC0E4
		public void Convert(Dictionary<string, object> dict, bool isWrite)
		{
			JSON.ReadWrite(dict, "discount_ends", ref this._discountEnds, isWrite);
			JSON.ReadWrite(dict, "discount_value", ref this._discountValue, isWrite);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000CDF18 File Offset: 0x000CC118
		public void CancelDiscount()
		{
			this._discountEnds = 0;
			this._discountValue = 0;
		}

		// Token: 0x040015FD RID: 5629
		private int _discountEnds;

		// Token: 0x040015FE RID: 5630
		private int _discountValue;
	}
}
