using System;
using System.Collections.Generic;

namespace Assets.Scripts.Camouflage
{
	// Token: 0x02000058 RID: 88
	internal class CamouflageInfo
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000C8B8 File Offset: 0x0000AAB8
		public CamouflageInfo()
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C99C File Offset: 0x0000AB9C
		public CamouflageInfo(Dictionary<string, object> dict)
		{
			this.Convert(dict, false);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000CA88 File Offset: 0x0000AC88
		public Currency CurrencyType
		{
			get
			{
				return (Currency)this._currency;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000CA90 File Offset: 0x0000AC90
		public CamouflageRarity Rarity
		{
			get
			{
				return this._rarityConverter[this._rarity];
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000CAA4 File Offset: 0x0000ACA4
		public int Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000CAAC File Offset: 0x0000ACAC
		public int Price
		{
			get
			{
				return this._price;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public bool Free
		{
			get
			{
				return this._free;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public int FreeCount
		{
			get
			{
				return this._freeCount;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public FreeType FreeType
		{
			get
			{
				return (FreeType)this._freeType;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000CACC File Offset: 0x0000ACCC
		public float Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000CAD4 File Offset: 0x0000ACD4
		public bool Locked
		{
			get
			{
				return !Main.UserInfo.Mastering.WearStates.ContainsKey(this._id);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000CAF4 File Offset: 0x0000ACF4
		public bool Available
		{
			get
			{
				if (this._free)
				{
					if (this.FreeType == FreeType.Level)
					{
						return Main.UserInfo.PlayerLevel >= this._freeCount;
					}
					if (this.FreeType == FreeType.Achievement)
					{
						return Main.UserInfo.achievementsInfos[this._freeCount].Complete;
					}
				}
				return false;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000CB54 File Offset: 0x0000AD54
		public void Convert(Dictionary<string, object> dict, bool isWrite)
		{
			JSON.ReadWrite(dict, "id", ref this._id, isWrite);
			JSON.ReadWrite(dict, "mod_short_name", ref this.ShortName, isWrite);
			JSON.ReadWrite(dict, "mod_name", ref this.FullName, isWrite);
			JSON.ReadWrite(dict, "currency_for_character", ref this._currency, isWrite);
			JSON.ReadWrite(dict, "price_for_character", ref this._price, isWrite);
			JSON.ReadWrite(dict, "rarity", ref this._rarity, isWrite);
			JSON.ReadWrite(dict, "free", ref this._free, isWrite);
			JSON.ReadWrite(dict, "free_for", ref this._freeType, isWrite);
			JSON.ReadWrite(dict, "free_counter", ref this._freeCount, isWrite);
			JSON.ReadWrite(dict, "scale_for_character", ref this._scale, isWrite);
			if (this._scale <= 0f)
			{
				this._scale = 8f;
			}
		}

		// Token: 0x040001DC RID: 476
		private int _id;

		// Token: 0x040001DD RID: 477
		private int _currency;

		// Token: 0x040001DE RID: 478
		private int _price;

		// Token: 0x040001DF RID: 479
		private bool _free;

		// Token: 0x040001E0 RID: 480
		private int _freeType;

		// Token: 0x040001E1 RID: 481
		private int _freeCount;

		// Token: 0x040001E2 RID: 482
		private float _scale;

		// Token: 0x040001E3 RID: 483
		private string _rarity = "common";

		// Token: 0x040001E4 RID: 484
		public string ShortName = "STANDART";

		// Token: 0x040001E5 RID: 485
		public string FullName = "Default PMC color (no camo)";

		// Token: 0x040001E6 RID: 486
		private Dictionary<string, CamouflageRarity> _rarityConverter = new Dictionary<string, CamouflageRarity>
		{
			{
				CamouflageRarity.Common.ToString().ToLower(),
				CamouflageRarity.Common
			},
			{
				CamouflageRarity.Rare.ToString().ToLower(),
				CamouflageRarity.Rare
			},
			{
				CamouflageRarity.Infrequent.ToString().ToLower(),
				CamouflageRarity.Infrequent
			},
			{
				CamouflageRarity.Pro.ToString().ToLower(),
				CamouflageRarity.Pro
			},
			{
				CamouflageRarity.Legendary.ToString().ToLower(),
				CamouflageRarity.Legendary
			},
			{
				CamouflageRarity.Secret.ToString().ToLower(),
				CamouflageRarity.Secret
			},
			{
				CamouflageRarity.Unique.ToString().ToLower(),
				CamouflageRarity.Unique
			}
		};
	}
}
