using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200029C RID: 668
[Serializable]
internal class Map : Convertible
{
	// Token: 0x170002A6 RID: 678
	// (get) Token: 0x060012CF RID: 4815 RVA: 0x000CBAFC File Offset: 0x000C9CFC
	public string Name
	{
		get
		{
			return this.name;
		}
	}

	// Token: 0x170002A7 RID: 679
	// (get) Token: 0x060012D0 RID: 4816 RVA: 0x000CBB04 File Offset: 0x000C9D04
	public List<GameMode> Modes
	{
		get
		{
			if (this._availableModes != null)
			{
				return this._availableModes;
			}
			this._availableModes = this.GameModes.Keys.ToList<GameMode>();
			return this._availableModes;
		}
	}

	// Token: 0x060012D1 RID: 4817 RVA: 0x000CBB40 File Offset: 0x000C9D40
	public void Convert(Dictionary<string, object> dict, bool isWrite)
	{
		if (isWrite)
		{
			return;
		}
		JSON.ReadWrite(dict, "name", ref this.name, isWrite);
		if (!dict.ContainsKey("Modes"))
		{
			Debug.LogError("Can't find modes for map " + this.name);
			return;
		}
		this.GameModes = new Dictionary<GameMode, GameModeInfo>();
		Dictionary<string, object> dict2 = (Dictionary<string, object>)dict["Modes"];
		this.ParseMode(dict2, GameMode.Deathmatch);
		this.ParseMode(dict2, GameMode.TargetDesignation);
		this.ParseMode(dict2, GameMode.TeamElimination);
		this.ParseMode(dict2, GameMode.TacticalConquest);
	}

	// Token: 0x060012D2 RID: 4818 RVA: 0x000CBBC8 File Offset: 0x000C9DC8
	private void ParseMode(Dictionary<string, object> dict, GameMode mode)
	{
		GameModeInfo gameModeInfo = null;
		JSON.ReadWrite<GameModeInfo>(dict, mode.ToString(), ref gameModeInfo, false);
		if (gameModeInfo != null)
		{
			this.GameModes.Add(mode, gameModeInfo);
		}
	}

	// Token: 0x04001584 RID: 5508
	private string name;

	// Token: 0x04001585 RID: 5509
	public Dictionary<GameMode, GameModeInfo> GameModes;

	// Token: 0x04001586 RID: 5510
	private List<GameMode> _availableModes;
}
