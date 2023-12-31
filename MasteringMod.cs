using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200034D RID: 845
internal class MasteringMod
{
	// Token: 0x06001C1A RID: 7194 RVA: 0x000FB21C File Offset: 0x000F941C
	public MasteringMod(Dictionary<string, object> dict)
	{
		this.Convert(dict, false);
	}

	// Token: 0x1700080E RID: 2062
	// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000FB36C File Offset: 0x000F956C
	public bool IsCamo
	{
		get
		{
			return this.Type == ModType.camo;
		}
	}

	// Token: 0x1700080F RID: 2063
	// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000FB378 File Offset: 0x000F9578
	public bool IsAmmo
	{
		get
		{
			return this.Type == ModType.ammo;
		}
	}

	// Token: 0x17000810 RID: 2064
	// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000FB384 File Offset: 0x000F9584
	public bool IsOpticSight
	{
		get
		{
			return this.SightType == SightType.Optic;
		}
	}

	// Token: 0x17000811 RID: 2065
	// (get) Token: 0x06001C1E RID: 7198 RVA: 0x000FB390 File Offset: 0x000F9590
	public bool IsCollimatorSight
	{
		get
		{
			return this.SightType == SightType.Collimator;
		}
	}

	// Token: 0x17000812 RID: 2066
	// (get) Token: 0x06001C1F RID: 7199 RVA: 0x000FB39C File Offset: 0x000F959C
	public bool IsHybridSight
	{
		get
		{
			return this.SightType == SightType.Hybrid;
		}
	}

	// Token: 0x17000813 RID: 2067
	// (get) Token: 0x06001C20 RID: 7200 RVA: 0x000FB3A8 File Offset: 0x000F95A8
	public bool IsSilencer
	{
		get
		{
			return this.BarrelDeviceType == BarrelDeviceType.Silencer;
		}
	}

	// Token: 0x17000814 RID: 2068
	// (get) Token: 0x06001C21 RID: 7201 RVA: 0x000FB3B4 File Offset: 0x000F95B4
	public bool IsFlashHider
	{
		get
		{
			return this.BarrelDeviceType == BarrelDeviceType.Flashhider;
		}
	}

	// Token: 0x17000815 RID: 2069
	// (get) Token: 0x06001C22 RID: 7202 RVA: 0x000FB3C0 File Offset: 0x000F95C0
	public bool IsLtp
	{
		get
		{
			return this.TacticalDeviceType == TacticalDeviceType.Ltp;
		}
	}

	// Token: 0x17000816 RID: 2070
	// (get) Token: 0x06001C23 RID: 7203 RVA: 0x000FB3CC File Offset: 0x000F95CC
	public bool IsBullet
	{
		get
		{
			return this.AmmoType == AmmoType.Bullet;
		}
	}

	// Token: 0x17000817 RID: 2071
	// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000FB3D8 File Offset: 0x000F95D8
	public bool IsFlechette
	{
		get
		{
			return this.AmmoType == AmmoType.Flechette;
		}
	}

	// Token: 0x17000818 RID: 2072
	// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000FB3E4 File Offset: 0x000F95E4
	public bool IsBuckshot
	{
		get
		{
			return this.AmmoType == AmmoType.Buckshot;
		}
	}

	// Token: 0x17000819 RID: 2073
	// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000FB3F0 File Offset: 0x000F95F0
	public float Sensitivity
	{
		get
		{
			return this._sensitivity;
		}
	}

	// Token: 0x1700081A RID: 2074
	// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000FB3F8 File Offset: 0x000F95F8
	public ModType Type
	{
		get
		{
			return (ModType)this._typeAsInt;
		}
	}

	// Token: 0x1700081B RID: 2075
	// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000FB400 File Offset: 0x000F9600
	public SightType SightType
	{
		get
		{
			return (SightType)this._sightTypeAsInt;
		}
	}

	// Token: 0x1700081C RID: 2076
	// (get) Token: 0x06001C29 RID: 7209 RVA: 0x000FB408 File Offset: 0x000F9608
	public BarrelDeviceType BarrelDeviceType
	{
		get
		{
			return (BarrelDeviceType)this._barrelDeviceTypeAsInt;
		}
	}

	// Token: 0x1700081D RID: 2077
	// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000FB410 File Offset: 0x000F9610
	public TacticalDeviceType TacticalDeviceType
	{
		get
		{
			return (TacticalDeviceType)this._tacticalDeviceTypeAsInt;
		}
	}

	// Token: 0x1700081E RID: 2078
	// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000FB418 File Offset: 0x000F9618
	public AmmoType AmmoType
	{
		get
		{
			return (AmmoType)this._ammoTypeAsInt;
		}
	}

	// Token: 0x1700081F RID: 2079
	// (get) Token: 0x06001C2C RID: 7212 RVA: 0x000FB420 File Offset: 0x000F9620
	public CamouflageRarity Rarity
	{
		get
		{
			return (!string.IsNullOrEmpty(this._rarity)) ? this._rarityConverter[this._rarity.ToLower()] : CamouflageRarity.Common;
		}
	}

	// Token: 0x06001C2D RID: 7213 RVA: 0x000FB45C File Offset: 0x000F965C
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		JSON.ReadWrite(dict, "id", ref this.Id, isWrite);
		JSON.ReadWrite(dict, "mod_short_name", ref this.ShortName, isWrite);
		JSON.ReadWrite(dict, "naming", ref this.EngShortName, isWrite);
		JSON.ReadWrite(dict, "mod_name", ref this.FullName, isWrite);
		JSON.ReadWrite(dict, "description", ref this.Description, isWrite);
		JSON.ReadWrite(dict, "type", ref this._typeAsInt, isWrite);
		JSON.ReadWrite(dict, "accuracy", ref this.Accuracy.StrVal, isWrite);
		JSON.ReadWrite(dict, "kickback", ref this.Recoil.StrVal, isWrite);
		JSON.ReadWrite(dict, "killpower", ref this.Damage.StrVal, isWrite);
		JSON.ReadWrite(dict, "efficiency", ref this.Penetration.StrVal, isWrite);
		JSON.ReadWrite(dict, "mobility", ref this.Mobility.StrVal, isWrite);
		JSON.ReadWrite(dict, "effective_distance", ref this.EffectiveDistance.StrVal, isWrite);
		JSON.ReadWrite(dict, "hear_distance", ref this.HearDistance.StrVal, isWrite);
		JSON.ReadWrite(dict, "aim_fov", ref this.AimFov.StrVal, isWrite);
		JSON.ReadWrite(dict, "optic_fov", ref this.OpticFov.StrVal, isWrite);
		JSON.ReadWrite(dict, "shot_grouping", ref this.ShotGrouping.StrVal, isWrite);
		JSON.ReadWrite(dict, "is_basic", ref this.IsBasic, isWrite);
		JSON.ReadWrite(dict, "rarity", ref this._rarity, isWrite);
		JSON.ReadWrite(dict, "sense", ref this._sensitivity, isWrite);
		JSON.ReadWrite(dict, "sight_type", ref this._sightTypeAsInt, isWrite);
		JSON.ReadWrite(dict, "muzzle_type", ref this._barrelDeviceTypeAsInt, isWrite);
		JSON.ReadWrite(dict, "tactical_type", ref this._tacticalDeviceTypeAsInt, isWrite);
		JSON.ReadWrite(dict, "ammo_type", ref this._ammoTypeAsInt, isWrite);
	}

	// Token: 0x06001C2E RID: 7214 RVA: 0x000FB63C File Offset: 0x000F983C
	public override string ToString()
	{
		return string.Concat(new object[]
		{
			this.EngShortName,
			"- type: ",
			this.Type,
			" id: ",
			this.Id
		});
	}

	// Token: 0x040020DB RID: 8411
	private int _typeAsInt;

	// Token: 0x040020DC RID: 8412
	private int _sightTypeAsInt;

	// Token: 0x040020DD RID: 8413
	private int _barrelDeviceTypeAsInt;

	// Token: 0x040020DE RID: 8414
	private int _tacticalDeviceTypeAsInt;

	// Token: 0x040020DF RID: 8415
	private int _ammoTypeAsInt;

	// Token: 0x040020E0 RID: 8416
	private string _rarity;

	// Token: 0x040020E1 RID: 8417
	private float _sensitivity = 1f;

	// Token: 0x040020E2 RID: 8418
	public string ShortName;

	// Token: 0x040020E3 RID: 8419
	public string EngShortName;

	// Token: 0x040020E4 RID: 8420
	public string FullName;

	// Token: 0x040020E5 RID: 8421
	public string Description;

	// Token: 0x040020E6 RID: 8422
	public int Id;

	// Token: 0x040020E7 RID: 8423
	public MasteringMod.ModVal Accuracy = new MasteringMod.ModVal();

	// Token: 0x040020E8 RID: 8424
	public MasteringMod.ModVal Recoil = new MasteringMod.ModVal();

	// Token: 0x040020E9 RID: 8425
	public MasteringMod.ModVal Damage = new MasteringMod.ModVal();

	// Token: 0x040020EA RID: 8426
	public MasteringMod.ModVal Penetration = new MasteringMod.ModVal();

	// Token: 0x040020EB RID: 8427
	public MasteringMod.ModVal Mobility = new MasteringMod.ModVal();

	// Token: 0x040020EC RID: 8428
	public MasteringMod.ModVal EffectiveDistance = new MasteringMod.ModVal();

	// Token: 0x040020ED RID: 8429
	public MasteringMod.ModVal HearDistance = new MasteringMod.ModVal();

	// Token: 0x040020EE RID: 8430
	public MasteringMod.ModVal AimFov = new MasteringMod.ModVal();

	// Token: 0x040020EF RID: 8431
	public MasteringMod.ModVal OpticFov = new MasteringMod.ModVal();

	// Token: 0x040020F0 RID: 8432
	public MasteringMod.ModVal ShotGrouping = new MasteringMod.ModVal();

	// Token: 0x040020F1 RID: 8433
	public Dictionary<int, WeaponSpecificModInfo> WeaponSpecificInfo = new Dictionary<int, WeaponSpecificModInfo>();

	// Token: 0x040020F2 RID: 8434
	public Texture2D SmallIcon;

	// Token: 0x040020F3 RID: 8435
	public Texture2D BigIcon;

	// Token: 0x040020F4 RID: 8436
	public Texture Texture;

	// Token: 0x040020F5 RID: 8437
	public GameObject Prefab;

	// Token: 0x040020F6 RID: 8438
	public bool IsBasic;

	// Token: 0x040020F7 RID: 8439
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

	// Token: 0x0200034E RID: 846
	internal class ModVal
	{
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x000FB6A0 File Offset: 0x000F98A0
		public bool Multiplication
		{
			get
			{
				return this.StrVal.Contains("%");
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x000FB6B4 File Offset: 0x000F98B4
		public float Val
		{
			get
			{
				if (string.IsNullOrEmpty(this.StrVal))
				{
					return 0f;
				}
				if (!this.Multiplication)
				{
					return float.Parse(this.StrVal);
				}
				float num;
				if (!float.TryParse(this.StrVal.Substring(0, this.StrVal.Length - 1), out num))
				{
					return 0f;
				}
				if (num > 0f)
				{
					return 1f + num / 100f;
				}
				return 1f - Mathf.Abs(num) / 100f;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x000FB744 File Offset: 0x000F9944
		public float PercentVal
		{
			get
			{
				if (!this.Multiplication)
				{
					return 0f;
				}
				float result;
				if (float.TryParse(this.StrVal.Substring(0, this.StrVal.Length - 1), out result))
				{
					return result;
				}
				return 0f;
			}
		}

		// Token: 0x040020F8 RID: 8440
		public string StrVal = string.Empty;
	}
}
