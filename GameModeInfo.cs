using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000297 RID: 663
[Serializable]
internal class GameModeInfo : Convertible
{
	// Token: 0x06001289 RID: 4745 RVA: 0x000CAB10 File Offset: 0x000C8D10
	public float GetExpCoefByRealm(Realms realm)
	{
		return (this._expCoefs != null) ? this._expCoefs[realm] : this.expCoef;
	}

	// Token: 0x0600128A RID: 4746 RVA: 0x000CAB40 File Offset: 0x000C8D40
	public void Convert(Dictionary<string, object> dict, bool set)
	{
		this.ConvertCoefsDict(dict, ref this._expCoefs);
		JSON.ReadWrite(dict, "expCoef", ref this.expCoef, set);
		JSON.ReadWrite(dict, "incomeCoef", ref this.incomeCoef, set);
		JSON.ReadWrite(dict, "matchRoundTime", ref this.matchRoundTime, set);
		JSON.ReadWrite(dict, "roundPreparingTime", ref this.roundPreparingTime, set);
		JSON.ReadWrite(dict, "matchResultTime", ref this.matchResultTime, set);
		JSON.ReadWrite(dict, "matchNeededPoints", ref this.matchNeededPoints, set);
		JSON.ReadWrite(dict, "matchNeededKills", ref this.matchNeededKills, set);
		JSON.ReadWrite(dict, "matchNeededTeamKills", ref this.matchNeededTeamKills, set);
		JSON.ReadWrite(dict, "isBearPlacement", ref this.isBearPlacement, set);
		JSON.ReadWrite(dict, "beaconPlacing", ref this.beaconPlacing, set);
		JSON.ReadWrite(dict, "beaconDePlacing", ref this.beaconDePlacing, set);
		JSON.ReadWrite(dict, "beaconTimer", ref this.beaconTimer, set);
		JSON.ReadWrite(dict, "farmCountermeasuresDepth", ref this.farmCountermeasuresDepth, set);
		JSON.ReadWrite(dict, "playersRequiredForExp", ref this.playersRequiredForExp, set);
		JSON.ReadWrite(dict, "playersRequiredForTasks", ref this.playersRequiredForTasks, set);
		JSON.ReadWrite(dict, "LeagueMatchNeededTeamKills", ref this.LeagueMatchNeededTeamKills, set);
		JSON.ReadWrite(dict, "LeagueMatchNeededPoints", ref this.LeagueMatchNeededPoints, set);
	}

	// Token: 0x0600128B RID: 4747 RVA: 0x000CAC8C File Offset: 0x000C8E8C
	private void ConvertCoefsDict(Dictionary<string, object> dict, ref Dictionary<Realms, float> dictToConvert)
	{
		try
		{
			object obj;
			Dictionary<string, object> dictionary;
			if (dict.TryGetValue("expCoefs", out obj) && (dictionary = (obj as Dictionary<string, object>)) != null)
			{
				dictToConvert = new Dictionary<Realms, float>();
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					Realms key = (Realms)((int)Enum.Parse(typeof(Realms), keyValuePair.Key));
					float value = (!(keyValuePair.Value is int)) ? ((float)keyValuePair.Value) : ((float)((int)keyValuePair.Value));
					dictToConvert.Add(key, value);
				}
			}
		}
		catch (Exception)
		{
			dictToConvert = null;
			Debug.LogError("Can't convert dict");
		}
	}

	// Token: 0x0400153C RID: 5436
	public float expCoef;

	// Token: 0x0400153D RID: 5437
	public float incomeCoef;

	// Token: 0x0400153E RID: 5438
	private Dictionary<Realms, float> _expCoefs;

	// Token: 0x0400153F RID: 5439
	public float matchRoundTime;

	// Token: 0x04001540 RID: 5440
	public float roundPreparingTime = 6f;

	// Token: 0x04001541 RID: 5441
	public float matchResultTime = 30f;

	// Token: 0x04001542 RID: 5442
	public int matchNeededPoints;

	// Token: 0x04001543 RID: 5443
	public int matchNeededKills;

	// Token: 0x04001544 RID: 5444
	public int matchNeededTeamKills;

	// Token: 0x04001545 RID: 5445
	public bool isBearPlacement;

	// Token: 0x04001546 RID: 5446
	public float beaconPlacing;

	// Token: 0x04001547 RID: 5447
	public float beaconDePlacing;

	// Token: 0x04001548 RID: 5448
	public float beaconTimer;

	// Token: 0x04001549 RID: 5449
	public int farmCountermeasuresDepth;

	// Token: 0x0400154A RID: 5450
	public int playersRequiredForExp;

	// Token: 0x0400154B RID: 5451
	public int playersRequiredForTasks;

	// Token: 0x0400154C RID: 5452
	public int LeagueMatchNeededTeamKills;

	// Token: 0x0400154D RID: 5453
	public int LeagueMatchNeededPoints;
}
