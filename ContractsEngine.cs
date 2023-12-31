using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000254 RID: 596
internal class ContractsEngine
{
	// Token: 0x06001239 RID: 4665 RVA: 0x000C8230 File Offset: 0x000C6430
	public static void DisarmExplode(ServerNetPlayer player, Maps map, BeaconAction action)
	{
		List<ContractInfo> list = new List<ContractInfo>();
		list.Add(player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex]);
		list.Add(player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex]);
		list.Add(player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex]);
		int i = 0;
		while (i < 3)
		{
			ContractInfo contractInfo = list[i];
			if (contractInfo.task_type == ContractType.Disarm && action == BeaconAction.disarm)
			{
				goto IL_B0;
			}
			if (contractInfo.task_type == ContractType.Explode && action == BeaconAction.explode)
			{
				goto Block_3;
			}
			IL_100:
			i++;
			continue;
			Block_3:
			try
			{
				IL_B0:
				if (player.UserInfo.contractsInfo.getCurrentCount(i) < contractInfo.task_counter)
				{
					bool flag = contractInfo.DisarmExplode(map);
					if (flag)
					{
						ContractsEngine.IncreaseContractCounter(contractInfo, player, false, false);
					}
				}
			}
			catch (Exception e)
			{
				global::Console.exception(e);
				break;
			}
			goto IL_100;
		}
	}

	// Token: 0x0600123A RID: 4666 RVA: 0x000C8364 File Offset: 0x000C6564
	public static void Kill(ServerNetPlayer player, ServerNetPlayer target, AchievementTarget achivTarget, AchievementKillType killType, KillStreakEnum difficult, WeaponNature weaponNature, Maps map, GameMode mode, bool farmDetected = false)
	{
		if (farmDetected)
		{
			return;
		}
		if (Peer.HardcoreMode && player.IsTeam(target))
		{
			return;
		}
		List<ContractInfo> list = new List<ContractInfo>();
		list.Add(player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex]);
		list.Add(player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex]);
		list.Add(player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex]);
		for (int i = 0; i < 3; i++)
		{
			ContractInfo contractInfo = list[i];
			if (contractInfo.task_type == ContractType.Kill)
			{
				try
				{
					if (player.UserInfo.contractsInfo.getCurrentCount(i) < contractInfo.task_counter)
					{
						bool flag = contractInfo.Kill(weaponNature, player, target, achivTarget, killType, difficult, map, mode, target.Level, target.UserInfo.playerClass);
						if (flag)
						{
							ContractsEngine.IncreaseContractCounter(contractInfo, player, false, false);
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

	// Token: 0x0600123B RID: 4667 RVA: 0x000C84B8 File Offset: 0x000C66B8
	public static void MatchResult(ServerNetPlayer player, GameMode mode, MatchResultInfo matchResult, Maps map)
	{
		List<ContractInfo> list = new List<ContractInfo>();
		list.Add(player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex]);
		list.Add(player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex]);
		list.Add(player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex]);
		for (int i = 0; i < 3; i++)
		{
			ContractInfo contractInfo = list[i];
			if (contractInfo.task_type == ContractType.WinMatch)
			{
				if (player.UserInfo.contractsInfo.getCurrentCount(i) < contractInfo.task_counter)
				{
					bool flag = contractInfo.MatchResult(player, mode, matchResult, map);
					if (flag)
					{
						ContractsEngine.IncreaseContractCounter(contractInfo, player, contractInfo.task_type == ContractType.ExpMatch || contractInfo.task_type == ContractType.EarnExp, contractInfo.task_type == ContractType.ExpMatch);
					}
				}
			}
		}
	}

	// Token: 0x0600123C RID: 4668 RVA: 0x000C85C0 File Offset: 0x000C67C0
	public static void ClearPointsAtEarnExpMatch(ServerNetPlayer player)
	{
		try
		{
			if (!(player == null))
			{
				if (player.UserInfo.contractsInfo.easy.Length > player.UserInfo.contractsInfo.CurrentEasyIndex && player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_counter > player.UserInfo.contractsInfo.CurrentEasyCount)
				{
					player.UserInfo.contractsInfo.CurrentEasyCount = 0;
				}
				if (player.UserInfo.contractsInfo.normal.Length > player.UserInfo.contractsInfo.CurrentNormalIndex && player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex].task_counter > player.UserInfo.contractsInfo.CurrentNormalCount)
				{
					player.UserInfo.contractsInfo.CurrentNormalCount = 0;
				}
				if (player.UserInfo.contractsInfo.hard.Length > player.UserInfo.contractsInfo.CurrentHardIndex && player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_counter > player.UserInfo.contractsInfo.CurrentHardCount)
				{
					player.UserInfo.contractsInfo.CurrentHardCount = 0;
				}
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x0600123D RID: 4669 RVA: 0x000C87E8 File Offset: 0x000C69E8
	public static void OnEarnExpShowAtMatchEnd(ServerNetPlayer player)
	{
		if (player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_counter > player.UserInfo.contractsInfo.CurrentEasyCount)
		{
			player.RaiseContract(ContractTaskType.EasyTask, player.UserInfo.contractsInfo.CurrentEasyCount, false);
		}
		if (player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex].task_counter > player.UserInfo.contractsInfo.CurrentNormalCount)
		{
			player.RaiseContract(ContractTaskType.NormalTask, player.UserInfo.contractsInfo.CurrentNormalCount, false);
		}
		if (player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_type == ContractType.ExpMatch && player.UserInfo.contractsInfo.Hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_counter > player.UserInfo.contractsInfo.CurrentHardCount)
		{
			player.RaiseContract(ContractTaskType.HardTask, player.UserInfo.contractsInfo.CurrentHardCount, false);
		}
	}

	// Token: 0x0600123E RID: 4670 RVA: 0x000C8974 File Offset: 0x000C6B74
	public static void OnEarnExp(ServerNetPlayer player, GameMode mode, int perKillExp, int obtainedExp, Maps map)
	{
		if (perKillExp <= 0)
		{
			return;
		}
		if (!Peer.HardcoreMode && player.UserInfo.skillsInfos[139].Unlocked)
		{
			perKillExp *= 2;
			obtainedExp *= 2;
		}
		ContractsEngine.RiseOnEarnExp(player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex], player, mode, perKillExp, obtainedExp, map);
		ContractsEngine.RiseOnEarnExp(player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex], player, mode, perKillExp, obtainedExp, map);
		ContractsEngine.RiseOnEarnExp(player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex], player, mode, perKillExp, obtainedExp, map);
	}

	// Token: 0x0600123F RID: 4671 RVA: 0x000C8A3C File Offset: 0x000C6C3C
	private static void RiseOnEarnExp(ContractInfo contractInfo, ServerNetPlayer player, GameMode mode, int perKillExp, int obtainedExp, Maps map)
	{
		if (contractInfo.game_mode != mode && contractInfo.game_mode != GameMode.any)
		{
			return;
		}
		if ((contractInfo.task_type == ContractType.EarnExp || contractInfo.task_type == ContractType.ExpMatch) && (map == contractInfo.MAP || contractInfo.MAP == Maps.none))
		{
			if (contractInfo.MAP != Maps.none && contractInfo.MAP != Maps.all && map != contractInfo.MAP)
			{
				return;
			}
			int num = 0;
			if (contractInfo.DIFFCLTY == ContractTaskType.EasyTask)
			{
				if (contractInfo.task_counter == player.UserInfo.contractsInfo.CurrentEasyCount)
				{
					return;
				}
				if (contractInfo.task_type != ContractType.ExpMatch)
				{
					num = player.UserInfo.contractsInfo.CurrentEasyCount;
					player.UserInfo.contractsInfo.CurrentEasyCount += perKillExp;
				}
				else
				{
					player.UserInfo.contractsInfo.CurrentEasyCount = 0;
				}
			}
			if (contractInfo.DIFFCLTY == ContractTaskType.NormalTask)
			{
				if (contractInfo.task_counter == player.UserInfo.contractsInfo.CurrentNormalCount)
				{
					return;
				}
				if (contractInfo.task_type != ContractType.ExpMatch)
				{
					num = player.UserInfo.contractsInfo.CurrentNormalCount;
					player.UserInfo.contractsInfo.CurrentNormalCount += perKillExp;
				}
				else
				{
					player.UserInfo.contractsInfo.CurrentNormalCount = 0;
				}
			}
			if (contractInfo.DIFFCLTY == ContractTaskType.HardTask)
			{
				if (contractInfo.task_counter == player.UserInfo.contractsInfo.CurrentHardCount)
				{
					return;
				}
				if (contractInfo.task_type != ContractType.ExpMatch)
				{
					num = player.UserInfo.contractsInfo.CurrentHardCount;
					player.UserInfo.contractsInfo.CurrentHardCount += perKillExp;
				}
				else
				{
					player.UserInfo.contractsInfo.CurrentHardCount = 0;
				}
			}
			num += ((contractInfo.task_type == ContractType.ExpMatch) ? obtainedExp : perKillExp);
			if (num >= contractInfo.task_counter)
			{
				num = contractInfo.task_counter;
				if (contractInfo.DIFFCLTY == ContractTaskType.EasyTask)
				{
					player.UserInfo.contractsInfo.CurrentEasyCount = contractInfo.task_counter;
				}
				if (contractInfo.DIFFCLTY == ContractTaskType.NormalTask)
				{
					player.UserInfo.contractsInfo.CurrentNormalCount = contractInfo.task_counter;
				}
				if (contractInfo.DIFFCLTY == ContractTaskType.HardTask)
				{
					player.UserInfo.contractsInfo.CurrentHardCount = contractInfo.task_counter;
				}
			}
			if ((perKillExp > 0 || obtainedExp > 0) && contractInfo.task_counter >= num)
			{
				player.RaiseContract(contractInfo.DIFFCLTY, num, false);
			}
		}
	}

	// Token: 0x06001240 RID: 4672 RVA: 0x000C8CD0 File Offset: 0x000C6ED0
	private static void IncreaseContractCounter(ContractInfo contract, ServerNetPlayer player, bool useXP = false, bool matchOnly = false)
	{
		int num = 1;
		if (useXP)
		{
			num = player.Points;
		}
		if (!Peer.HardcoreMode && player.UserInfo.skillsInfos[139].Unlocked)
		{
			num *= 2;
		}
		if (contract.DIFFCLTY == ContractTaskType.EasyTask)
		{
			int num2 = Mathf.Min(player.UserInfo.contractsInfo.easyCounter + num, player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_counter);
			player.UserInfo.contractsInfo.easyCounter = num2;
			if (!matchOnly)
			{
				player.RaiseContract(ContractTaskType.EasyTask, num2, false);
			}
			else if (num2 == player.UserInfo.contractsInfo.easy[player.UserInfo.contractsInfo.CurrentEasyIndex].task_counter)
			{
				player.RaiseContract(ContractTaskType.EasyTask, num2, false);
			}
		}
		else if (contract.DIFFCLTY == ContractTaskType.NormalTask)
		{
			int num3 = Mathf.Min(player.UserInfo.contractsInfo.normalCounter + num, player.UserInfo.contractsInfo.CurrentNormal.task_counter);
			player.UserInfo.contractsInfo.normalCounter = num3;
			if (!matchOnly)
			{
				player.RaiseContract(ContractTaskType.NormalTask, num3, false);
			}
			else if (num3 == player.UserInfo.contractsInfo.normal[player.UserInfo.contractsInfo.CurrentNormalIndex].task_counter)
			{
				player.RaiseContract(ContractTaskType.NormalTask, num3, false);
			}
		}
		else if (contract.DIFFCLTY == ContractTaskType.HardTask)
		{
			int num4 = Mathf.Min(player.UserInfo.contractsInfo.hardCounter + num, player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_counter);
			player.UserInfo.contractsInfo.hardCounter = num4;
			if (!matchOnly)
			{
				player.RaiseContract(ContractTaskType.HardTask, num4, false);
			}
			else if (num4 == player.UserInfo.contractsInfo.hard[player.UserInfo.contractsInfo.CurrentHardIndex].task_counter)
			{
				player.RaiseContract(ContractTaskType.HardTask, num4, false);
			}
		}
	}
}
