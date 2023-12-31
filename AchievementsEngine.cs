using System;
using UnityEngine;

// Token: 0x02000241 RID: 577
internal class AchievementsEngine
{
	// Token: 0x060011B5 RID: 4533 RVA: 0x000C4AC0 File Offset: 0x000C2CC0
	public static int OpenedCount(OverviewInfo info)
	{
		int num = 0;
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			if (info.achievementsInfos[i].count == info.achievementsInfos[i].current)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x060011B6 RID: 4534 RVA: 0x000C4B10 File Offset: 0x000C2D10
	public static int Closest(OverviewInfo info)
	{
		float num = 0f;
		int result = -1;
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			float num2 = (float)info.achievementsInfos[i].current / (float)info.achievementsInfos[i].count;
			if (num2 > num && num2 != 1f)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	// Token: 0x060011B7 RID: 4535 RVA: 0x000C4B80 File Offset: 0x000C2D80
	public static float Proc(OverviewInfo info, int achIdx)
	{
		if (Globals.I.achievements.Length > achIdx)
		{
			return (float)info.achievementsInfos[achIdx].current / (float)info.achievementsInfos[achIdx].count;
		}
		return 0f;
	}

	// Token: 0x060011B8 RID: 4536 RVA: 0x000C4BB8 File Offset: 0x000C2DB8
	public static void Kill(ServerNetPlayer player, ServerNetPlayer target, AchievementTarget achivTarget, AchievementKillType killType, KillStreakEnum difficult, WeaponNature weaponNature, bool atDeath, bool lastClip, bool farmDetected = false)
	{
		if (player.IsDeadOrSpectactor || target.IsDeadOrSpectactor)
		{
			return;
		}
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.kill)
			{
				try
				{
					if (achievementInfo.current < achievementInfo.count)
					{
						if (achievementInfo.Kill(weaponNature, player.Ammo.CurrentWeapon, target.Ammo.CurrentWeapon, achivTarget, killType, difficult, atDeath, lastClip))
						{
							if (!farmDetected)
							{
								achievementInfo.current = Mathf.Min((int)((float)achievementInfo.current + player.achievementsMult), achievementInfo.count);
								int money = 0;
								if (achievementInfo.current == achievementInfo.count)
								{
									money = achievementInfo.prize;
								}
								player.RaiseAchievement(i, achievementInfo.current, money, false);
							}
							else
							{
								int money2 = 0;
								player.RaiseAchievement(i, achievementInfo.current, money2, farmDetected);
							}
						}
					}
				}
				catch (Exception e)
				{
					global::Console.exception(e);
					break;
				}
			}
		}
	}

	// Token: 0x060011B9 RID: 4537 RVA: 0x000C4CFC File Offset: 0x000C2EFC
	public static void MatchResult(ServerNetPlayer player, GameMode mode, MatchResultInfo matchResult, bool noDeaths)
	{
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.matchResult)
			{
				if (player.UserInfo.achievementsInfos[i].current < achievementInfo.count)
				{
					bool flag = achievementInfo.MatchResult(mode, matchResult, noDeaths);
					if (flag)
					{
						int money = 0;
						player.UserInfo.achievementsInfos[i].current = Mathf.Min((int)((float)player.UserInfo.achievementsInfos[i].current + player.achievementsMult), achievementInfo.count);
						if (player.UserInfo.achievementsInfos[i].current > achievementInfo.count)
						{
							player.UserInfo.achievementsInfos[i].current = achievementInfo.count;
						}
						if (player.UserInfo.achievementsInfos[i].current == achievementInfo.count)
						{
							break;
						}
						if (player.UserInfo.achievementsInfos[i].current == achievementInfo.count)
						{
							money = achievementInfo.prize;
						}
						player.RaiseAchievement(i, player.UserInfo.achievementsInfos[i].current, money, false);
					}
				}
			}
		}
	}

	// Token: 0x060011BA RID: 4538 RVA: 0x000C4E44 File Offset: 0x000C3044
	public static void WtaskComplete(ServerNetPlayer player)
	{
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.wtaskComplete)
			{
				if (player.UserInfo.achievementsInfos[i].current < achievementInfo.count)
				{
					bool flag = achievementInfo.WtaskComplete(player.UserInfo.wtaskOpenedCount);
					if (flag)
					{
						player.UserInfo.achievementsInfos[i].current = achievementInfo.count;
						int prize = achievementInfo.prize;
						player.RaiseAchievement(i, achievementInfo.count, prize, false);
					}
				}
			}
		}
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x000C4EF0 File Offset: 0x000C30F0
	public static void ArmoryUnlock(ServerNetPlayer player)
	{
		for (int i = 0; i < Globals.I.achievements.Length; i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.armoryUnlock)
			{
				if (player.UserInfo.achievementsInfos[i].current < achievementInfo.count)
				{
					bool flag = achievementInfo.ArmoryUnlock((WeaponBlockUnlocked)(player.UserInfo.currentLevel / 10), player.UserInfo);
					if (flag)
					{
						int money = 0;
						player.UserInfo.achievementsInfos[i].current++;
						if (player.UserInfo.achievementsInfos[i].current == achievementInfo.count)
						{
							break;
						}
						if (player.UserInfo.achievementsInfos[i].current == achievementInfo.count)
						{
							money = achievementInfo.prize;
						}
						player.RaiseAchievement(i, achievementInfo.count, money, false);
					}
				}
			}
		}
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x000C4FE8 File Offset: 0x000C31E8
	public static void OnlineTime(ServerNetPlayer player, int totalSeconds)
	{
		for (int i = 0; i < Mathf.Min(Globals.I.achievements.Length, Globals.I.achievements.Length); i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.onlineTime)
			{
				if (player.UserInfo.achievementsInfos[i].current < achievementInfo.count)
				{
					bool flag = achievementInfo.OnlineTime(totalSeconds);
					if (flag)
					{
						int prize = achievementInfo.prize;
						player.UserInfo.achievementsInfos[i].current = achievementInfo.count;
						player.RaiseAchievement(i, achievementInfo.count, prize, false);
					}
				}
			}
		}
	}

	// Token: 0x060011BD RID: 4541 RVA: 0x000C509C File Offset: 0x000C329C
	public static void SonarDetect(ServerNetPlayer player)
	{
		for (int i = 0; i < Mathf.Min(Globals.I.achievements.Length, Globals.I.achievements.Length); i++)
		{
			AchievementInfo achievementInfo = player.UserInfo.achievementsInfos[i];
			if (achievementInfo.type == AchievementType.sonarDetection)
			{
				int money = 0;
				if (player.UserInfo.achievementsInfos[i].current < achievementInfo.count)
				{
					player.UserInfo.achievementsInfos[i].current = Mathf.Min((int)((float)player.UserInfo.achievementsInfos[i].current + player.achievementsMult), achievementInfo.count);
					if (player.UserInfo.achievementsInfos[i].current > achievementInfo.count)
					{
						player.UserInfo.achievementsInfos[i].current = achievementInfo.count;
					}
					if (player.UserInfo.achievementsInfos[i].current == achievementInfo.count)
					{
						money = achievementInfo.prize;
					}
					player.RaiseAchievement(i, player.UserInfo.achievementsInfos[i].current, money, false);
				}
			}
		}
	}
}
