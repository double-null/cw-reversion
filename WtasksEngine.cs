using System;

// Token: 0x020002E1 RID: 737
[Serializable]
internal class WtasksEngine
{
	// Token: 0x0600147C RID: 5244 RVA: 0x000D8C40 File Offset: 0x000D6E40
	public static void Kill(ServerNetPlayer iKiller, AchievementTarget beef, AchievementKillType killType, KillStreakEnum difficult, bool atDeath, bool lastClip, WeaponSpecific weaponType, bool farmDetected = false)
	{
		BaseWeapon currentWeapon = iKiller.Ammo.CurrentWeapon;
		if (currentWeapon.isPremium && !currentWeapon.forceEnableWtask)
		{
			return;
		}
		if (iKiller.UserInfo.weaponsStates[(int)currentWeapon.type].wtaskUnlocked)
		{
			return;
		}
		if (currentWeapon.wtask.Kill(beef, killType, difficult, atDeath, lastClip, weaponType))
		{
			if (!farmDetected)
			{
				int num = 0;
				while ((float)num < iKiller.wtaskMult)
				{
					iKiller.UserInfo.weaponsStates[(int)currentWeapon.type].wtaskCurrent += 1f;
					if (iKiller.UserInfo.weaponsStates[(int)currentWeapon.type].wtaskCurrent == (float)iKiller.UserInfo.weaponsStates[(int)currentWeapon.type].GetWeapon.wtask.count)
					{
						break;
					}
					num++;
				}
			}
			iKiller.RaiseWtask((int)currentWeapon.type, (int)iKiller.UserInfo.weaponsStates[(int)currentWeapon.type].wtaskCurrent, farmDetected);
		}
	}
}
