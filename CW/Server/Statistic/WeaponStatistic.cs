using System;
using System.Collections.Generic;

namespace CW.Server.Statistic
{
	// Token: 0x020002F6 RID: 758
	internal class WeaponStatistic
	{
		// Token: 0x0600158B RID: 5515 RVA: 0x000E27D0 File Offset: 0x000E09D0
		public WeaponStatistic()
		{
			this._bestWeapon = new Weapon();
			this._bestWeapon.WeaponID = -1;
			this._bestWeapon.WeaponKills = 0;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x000E2814 File Offset: 0x000E0A14
		public Weapon BestWeapon
		{
			get
			{
				return this._bestWeapon;
			}
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000E281C File Offset: 0x000E0A1C
		public void GatherStatistic(int weaponID)
		{
			Weapon weapon;
			if (this._data.TryGetValue(weaponID, out weapon))
			{
				weapon.WeaponKills++;
				if (this._bestWeapon.WeaponKills < weapon.WeaponKills)
				{
					this._bestWeapon = weapon;
				}
			}
			else
			{
				this._data.Add(weaponID, this.CreateWeapon(weaponID));
			}
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000E2880 File Offset: 0x000E0A80
		private Weapon CreateWeapon(int weaponID)
		{
			return new Weapon
			{
				WeaponID = weaponID,
				WeaponKills = 1
			};
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x000E28A4 File Offset: 0x000E0AA4
		public void Clear()
		{
			this._data.Clear();
			this._bestWeapon = new Weapon();
			this._bestWeapon.WeaponID = -1;
			this._bestWeapon.WeaponKills = 0;
		}

		// Token: 0x040019BE RID: 6590
		private Weapon _bestWeapon;

		// Token: 0x040019BF RID: 6591
		private Dictionary<int, Weapon> _data = new Dictionary<int, Weapon>();
	}
}
