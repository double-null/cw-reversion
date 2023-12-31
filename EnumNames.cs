using System;

// Token: 0x0200025B RID: 603
internal class EnumNames
{
	// Token: 0x0600127F RID: 4735 RVA: 0x000CA9F4 File Offset: 0x000C8BF4
	public static string GameModeName(GameMode type)
	{
		if (type == GameMode.dontKnow || type == GameMode.none)
		{
			return "none";
		}
		return EnumNames.GameModeNames[(int)type];
	}

	// Token: 0x06001280 RID: 4736 RVA: 0x000CAA14 File Offset: 0x000C8C14
	public static string PlacementTypeName(PlacementType type)
	{
		return EnumNames.PlacementTypeNames[(int)type];
	}

	// Token: 0x06001281 RID: 4737 RVA: 0x000CAA20 File Offset: 0x000C8C20
	public static string Weapons(Weapons b)
	{
		return EnumNames.WeaponsNames[(int)b];
	}

	// Token: 0x06001282 RID: 4738 RVA: 0x000CAA2C File Offset: 0x000C8C2C
	public static string Weapons(byte b)
	{
		if (!isDefined.isWeapons((int)b))
		{
			return "none";
		}
		return EnumNames.WeaponsNames[(int)b];
	}

	// Token: 0x06001283 RID: 4739 RVA: 0x000CAA48 File Offset: 0x000C8C48
	public static string Weapons(int b)
	{
		if (!isDefined.isWeapons(b))
		{
			return "none";
		}
		return EnumNames.WeaponsNames[b];
	}

	// Token: 0x06001284 RID: 4740 RVA: 0x000CAA64 File Offset: 0x000C8C64
	public static int WeaponsID(string name)
	{
		Weapons weapons = (Weapons)((int)Enum.Parse(typeof(Weapons), name));
		if (!isDefined.isWeapons((int)((byte)weapons)))
		{
			return -1;
		}
		return (int)weapons;
	}

	// Token: 0x06001285 RID: 4741 RVA: 0x000CAA98 File Offset: 0x000C8C98
	public static int KillMethodID(string name)
	{
		for (int i = 0; i < EnumNames.WeaponsNames.Length; i++)
		{
			if (EnumNames.WeaponsNames[i] == name)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x04001240 RID: 4672
	public static string[] GameModeNames = new string[]
	{
		"Deathmatch",
		"TeamElimination",
		"TargetDesignation",
		"TacticalConquest"
	};

	// Token: 0x04001241 RID: 4673
	public static string[] PlacementTypeNames = new string[]
	{
		"3-2",
		"4-7",
		"5-8"
	};

	// Token: 0x04001242 RID: 4674
	private static string[] WeaponsNames = new string[]
	{
		"pm",
		"aps",
		"glock",
		"kedr",
		"mp5k",
		"kac_pdw",
		"IJ52",
		"usp_match",
		"p226",
		"SR3M",
		"Bizon2",
		"SCARL",
		"rpk74",
		"tkpd1",
		"RJ",
		"mp5sd6",
		"wilson",
		"pernach",
		"veresk",
		"hk416c",
		"tkpds",
		"sv98",
		"m14cqb",
		"m14s",
		"val",
		"benelli",
		"mr133",
		"akms",
		"beretta",
		"python",
		"scorpion",
		"saiga",
		"gsh18",
		"rfb",
		"vltor",
		"ksg",
		"pdr",
		"dtsrs",
		"akms_gold",
		"ak105",
		"dtsrs2",
		"vss",
		"usp",
		"pmm",
		"PKP",
		"aek",
		"berettaPre",
		"M40A6",
		"mp5spec",
		"p90",
		"DE",
		"sphinx",
		"p90_prem",
		"g36c",
		"_9a91",
		"ak12",
		"iar",
		"mp7",
		"sks",
		"kriss",
		"mosina",
		"pph",
		"tt",
		"svu",
		"t5000",
		"spas",
		"noveske",
		"glock17",
		"val_mod3",
		"sw",
		"scarh",
		"auga3",
		"rpd",
		"vihlop",
		"saigaFull",
		"ak74",
		"acr",
		"hk417",
		"fnp45t",
		"x95",
		"pecheneg",
		"vityaz",
		"grach",
		"svd",
		"ump",
		"mdr",
		"an94",
		"awm",
		"c1911",
		"uzi",
		"sa58",
		"ij43c",
		"thor",
		"mpx",
		"fseven",
		"hk243",
		"qbu",
		"mdrc",
		"aksu",
		"isr",
		"fabarm",
		"101",
		"102",
		"103",
		"104",
		"105",
		"106",
		"107",
		"108",
		"109",
		"110",
		"111",
		"112",
		"113",
		"114",
		"115",
		"116",
		"117",
		"118",
		"119",
		"120",
		"121",
		"mortar",
		"self",
		"knife",
		"headshot",
		"grenade",
		"none"
	};
}
