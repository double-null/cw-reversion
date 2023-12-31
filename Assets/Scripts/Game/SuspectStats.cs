using System;
using System.Collections.Generic;

namespace Assets.Scripts.Game
{
	// Token: 0x020002F4 RID: 756
	internal class SuspectStats : Convertible
	{
		// Token: 0x06001587 RID: 5511 RVA: 0x000E2434 File Offset: 0x000E0634
		public SuspectStats()
		{
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x000E243C File Offset: 0x000E063C
		public SuspectStats(Stats stats)
		{
			this._kills = stats.kills;
			this._deaths = stats.deaths;
			this._time = (int)stats.timeStart;
			this._totalHits = stats.totalHits;
			this._totalAmmo = stats.totalAmmo;
			this._headShots = stats.headShots;
			this._doubleKills = stats.doubleKills;
			this._doubleHeadShots = stats.doubleHeadShots;
			this._tripleKills = stats.tripleKills;
			this._quadKills = stats.quadKills;
			this._longShots = stats.longShots;
			this._rageKills = stats.rageKills;
			this._stormKills = stats.stormKills;
			this._proKills = stats.proKills;
			this._legendaryKills = stats.legendaryKills;
			this._cheatButtons = 0;
			for (int i = 0; i < Enum.GetValues(typeof(AimbotButtons)).Length; i++)
			{
				int num = 0;
				foreach (int num2 in stats.KillCheatButtons)
				{
					if ((num2 & 1 << i) != 0)
					{
						num++;
					}
				}
				if ((float)num * 1f / (float)stats.KillCheatButtons.Count > CVars.AimbotButtonRatio)
				{
					this._cheatButtons |= 1 << i;
				}
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000E25CC File Offset: 0x000E07CC
		public void Convert(Dictionary<string, object> dict, bool set)
		{
			if (this._kills > 0)
			{
				JSON.ReadWrite(dict, "kills", ref this._kills, set);
			}
			if (this._deaths > 0)
			{
				JSON.ReadWrite(dict, "deaths", ref this._deaths, set);
			}
			if (this._time > 0)
			{
				JSON.ReadWrite(dict, "time", ref this._time, set);
			}
			if (this._totalHits > 0)
			{
				JSON.ReadWrite(dict, "total_hits", ref this._totalHits, set);
			}
			if (this._totalAmmo > 0)
			{
				JSON.ReadWrite(dict, "total_ammo", ref this._totalAmmo, set);
			}
			if (this._headShots > 0)
			{
				JSON.ReadWrite(dict, "head_shots", ref this._headShots, set);
			}
			if (this._doubleKills > 0)
			{
				JSON.ReadWrite(dict, "double_kills", ref this._doubleKills, set);
			}
			if (this._doubleHeadShots > 0)
			{
				JSON.ReadWrite(dict, "double_head_shots", ref this._doubleHeadShots, set);
			}
			if (this._tripleKills > 0)
			{
				JSON.ReadWrite(dict, "triple_kills", ref this._tripleKills, set);
			}
			if (this._quadKills > 0)
			{
				JSON.ReadWrite(dict, "quad_kills", ref this._quadKills, set);
			}
			if (this._longShots > 0)
			{
				JSON.ReadWrite(dict, "long_shots", ref this._longShots, set);
			}
			if (this._rageKills > 0)
			{
				JSON.ReadWrite(dict, "rage_kills", ref this._rageKills, set);
			}
			if (this._stormKills > 0)
			{
				JSON.ReadWrite(dict, "storm_kills", ref this._stormKills, set);
			}
			if (this._proKills > 0)
			{
				JSON.ReadWrite(dict, "pro_kills", ref this._proKills, set);
			}
			if (Globals.I.LegendaryKill && this._legendaryKills > 0)
			{
				JSON.ReadWrite(dict, "legendary_kills", ref this._legendaryKills, set);
			}
			if (this._cheatButtons > 0)
			{
				JSON.ReadWrite(dict, "cheat_buttons", ref this._cheatButtons, set);
			}
		}

		// Token: 0x040019AC RID: 6572
		private int _kills;

		// Token: 0x040019AD RID: 6573
		private int _deaths;

		// Token: 0x040019AE RID: 6574
		private int _time;

		// Token: 0x040019AF RID: 6575
		private int _totalHits;

		// Token: 0x040019B0 RID: 6576
		private int _totalAmmo;

		// Token: 0x040019B1 RID: 6577
		private int _headShots;

		// Token: 0x040019B2 RID: 6578
		private int _doubleKills;

		// Token: 0x040019B3 RID: 6579
		private int _doubleHeadShots;

		// Token: 0x040019B4 RID: 6580
		private int _tripleKills;

		// Token: 0x040019B5 RID: 6581
		private int _quadKills;

		// Token: 0x040019B6 RID: 6582
		private int _longShots;

		// Token: 0x040019B7 RID: 6583
		private int _rageKills;

		// Token: 0x040019B8 RID: 6584
		private int _stormKills;

		// Token: 0x040019B9 RID: 6585
		private int _proKills;

		// Token: 0x040019BA RID: 6586
		private int _legendaryKills;

		// Token: 0x040019BB RID: 6587
		private int _cheatButtons;
	}
}
