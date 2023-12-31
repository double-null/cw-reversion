using System;
using System.Collections.Generic;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002B6 RID: 694
[Serializable]
internal class Stats : Convertible
{
	// Token: 0x06001377 RID: 4983 RVA: 0x000D1228 File Offset: 0x000CF428
	public Stats()
	{
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x000D12A0 File Offset: 0x000CF4A0
	public Stats(bool forceInit)
	{
		if (!forceInit)
		{
			return;
		}
		this.weaponKills = new int[Globals.I.weapons.Length];
		if (Peer.ServerGame != null)
		{
			this.timeStart = Peer.ServerGame.ServerTime;
		}
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x000D1358 File Offset: 0x000CF558
	public void AddDelta(Stats i)
	{
		this.timeOnline += i.timeOnline;
		this.timeHardcore += i.timeHardcore;
		this.totalDamage += i.totalDamage;
		this.totalAmmo += i.totalAmmo;
		this.headShots += i.headShots;
		this.doubleKills += i.doubleKills;
		this.doubleHeadShots += i.doubleHeadShots;
		this.tripleKills += i.tripleKills;
		this.longShots += i.longShots;
		this.grenadeKills += i.grenadeKills;
		this.suicides += i.suicides;
		this.rageKills += i.rageKills;
		this.stormKills += i.stormKills;
		this.proKills += i.proKills;
		if (Globals.I.LegendaryKill)
		{
			this.legendaryKills += i.legendaryKills;
		}
		this.assists += i.assists;
		this.teamKills += i.teamKills;
		this.usecKills += i.usecKills;
		this.bearKills += i.bearKills;
		this.knifeKills += i.knifeKills;
		this.favGun += i.favGun;
		ArrayUtility.AdjustArraySize<int>(ref this.weaponKills, Globals.I.weapons.Length);
		for (int j = 0; j < Math.Min(this.weaponKills.Length, i.weaponKills.Length); j++)
		{
			this.weaponKills[j] += i.weaponKills[j];
		}
		this.creditsSpent += i.creditsSpent;
		this.armstreaksEarned += i.armstreaksEarned;
		this.armstreaksUsed += i.armstreaksUsed;
		this.totalHits += i.totalHits;
		this.matchesStarted += i.matchesStarted;
		this.matchesEnded += i.matchesEnded;
		this.KillCheatButtons.AddRange(i.KillCheatButtons);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000D15DC File Offset: 0x000CF7DC
	public void Convert(Dictionary<string, object> dict, bool set)
	{
		JSON.ReadWrite(dict, "killDelta", ref this.killDelta, set);
		JSON.ReadWrite(dict, "delta_exp", ref this.obtainedXP, set);
		JSON.ReadWrite(dict, "timeOnline", ref this.timeOnline, set);
		JSON.ReadWrite(dict, "timeHardcore", ref this.timeHardcore, set);
		JSON.ReadWrite(dict, "totalAmmo", ref this.totalAmmo, set);
		JSON.ReadWrite(dict, "totalDamage", ref this.totalDamage, set);
		JSON.ReadWrite(dict, "headShots", ref this.headShots, set);
		JSON.ReadWrite(dict, "doubleKills", ref this.doubleKills, set);
		JSON.ReadWrite(dict, "doubleHeadShots", ref this.doubleHeadShots, set);
		JSON.ReadWrite(dict, "tripleKills", ref this.tripleKills, set);
		JSON.ReadWrite(dict, "longShots", ref this.longShots, set);
		JSON.ReadWrite(dict, "grenadeKills", ref this.grenadeKills, set);
		JSON.ReadWrite(dict, "suicides", ref this.suicides, set);
		JSON.ReadWrite(dict, "rageKills", ref this.rageKills, set);
		JSON.ReadWrite(dict, "stormKills", ref this.stormKills, set);
		JSON.ReadWrite(dict, "proKills", ref this.proKills, set);
		if (Globals.I.LegendaryKill)
		{
			JSON.ReadWrite(dict, "legendaryKills", ref this.legendaryKills, set);
		}
		JSON.ReadWrite(dict, "assists", ref this.assists, set);
		JSON.ReadWrite(dict, "teamKills", ref this.teamKills, set);
		JSON.ReadWrite(dict, "bearKills", ref this.bearKills, set);
		JSON.ReadWrite(dict, "usecKills", ref this.usecKills, set);
		JSON.ReadWrite(dict, "knifeKills", ref this.knifeKills, set);
		JSON.ReadWrite(dict, "favGun", ref this.favGun, set);
		ArrayUtility.AdjustArraySize<int>(ref this.weaponKills, Globals.I.weapons.Length);
		JSON.ReadWrite(dict, "weaponKills", ref this.weaponKills, set);
		JSON.ReadWrite(dict, "creditsSpent", ref this.creditsSpent, set);
		JSON.ReadWrite(dict, "armstreaksEarned", ref this.armstreaksEarned, set);
		JSON.ReadWrite(dict, "armstreaksUsed", ref this.armstreaksUsed, set);
		JSON.ReadWrite(dict, "totalHits", ref this.totalHits, set);
		JSON.ReadWrite(dict, "matches_started", ref this.matchesStarted, set);
		JSON.ReadWrite(dict, "matches_ended", ref this.matchesEnded, set);
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x000D182C File Offset: 0x000CFA2C
	public Dictionary<string, object> LegueSystemConvert()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		bool isWrite = true;
		JSON.ReadWrite(dictionary, "isWinner", ref this.IsWinner, isWrite);
		JSON.ReadWrite(dictionary, "kills", ref this.kills, isWrite);
		JSON.ReadWrite(dictionary, "deaths", ref this.deaths, isWrite);
		JSON.ReadWrite(dictionary, "timeOnline", ref this.timeOnline, isWrite);
		JSON.ReadWrite(dictionary, "timeHardcore", ref this.timeHardcore, isWrite);
		JSON.ReadWrite(dictionary, "totalAmmo", ref this.totalAmmo, isWrite);
		JSON.ReadWrite(dictionary, "totalDamage", ref this.totalDamage, isWrite);
		JSON.ReadWrite(dictionary, "headShots", ref this.headShots, isWrite);
		JSON.ReadWrite(dictionary, "doubleKills", ref this.doubleKills, isWrite);
		JSON.ReadWrite(dictionary, "doubleHeadShots", ref this.doubleHeadShots, isWrite);
		JSON.ReadWrite(dictionary, "tripleKills", ref this.tripleKills, isWrite);
		JSON.ReadWrite(dictionary, "longShots", ref this.longShots, isWrite);
		JSON.ReadWrite(dictionary, "grenadeKills", ref this.grenadeKills, isWrite);
		JSON.ReadWrite(dictionary, "suicides", ref this.suicides, isWrite);
		JSON.ReadWrite(dictionary, "rageKills", ref this.rageKills, isWrite);
		JSON.ReadWrite(dictionary, "stormKills", ref this.stormKills, isWrite);
		JSON.ReadWrite(dictionary, "proKills", ref this.proKills, isWrite);
		if (Globals.I.LegendaryKill)
		{
			JSON.ReadWrite(dictionary, "legendaryKills", ref this.legendaryKills, isWrite);
		}
		JSON.ReadWrite(dictionary, "assists", ref this.assists, isWrite);
		JSON.ReadWrite(dictionary, "teamKills", ref this.teamKills, isWrite);
		JSON.ReadWrite(dictionary, "bearKills", ref this.bearKills, isWrite);
		JSON.ReadWrite(dictionary, "usecKills", ref this.usecKills, isWrite);
		JSON.ReadWrite(dictionary, "knifeKills", ref this.knifeKills, isWrite);
		JSON.ReadWrite(dictionary, "favGun", ref this.favGun, isWrite);
		JSON.ReadWrite(dictionary, "armstreaksEarned", ref this.armstreaksEarned, isWrite);
		JSON.ReadWrite(dictionary, "armstreaksUsed", ref this.armstreaksUsed, isWrite);
		JSON.ReadWrite(dictionary, "totalHits", ref this.totalHits, isWrite);
		return dictionary;
	}

	// Token: 0x170002C4 RID: 708
	// (get) Token: 0x0600137C RID: 4988 RVA: 0x000D1A38 File Offset: 0x000CFC38
	public int[] StreaksArray
	{
		get
		{
			return new int[]
			{
				this.kills,
				this.headShots,
				this.doubleKills,
				this.tripleKills,
				this.longShots,
				this.grenadeKills,
				this.rageKills,
				this.stormKills,
				this.suicides,
				this.assists,
				this.proKills,
				this.sitkills,
				this.peepkills,
				this.doubleHeadShots,
				this.vipkills,
				this.quadKills
			};
		}
	}

	// Token: 0x170002C5 RID: 709
	// (get) Token: 0x0600137D RID: 4989 RVA: 0x000D1AE4 File Offset: 0x000CFCE4
	public int StreaksIndexes
	{
		get
		{
			KillStreakEnum killStreakEnum = KillStreakEnum.none;
			if (this.kills > 0)
			{
				killStreakEnum |= KillStreakEnum.kill;
			}
			if (this.headShots > 0)
			{
				killStreakEnum |= KillStreakEnum.headshot;
			}
			if (this.doubleKills > 0)
			{
				killStreakEnum |= KillStreakEnum.doubleKill;
			}
			if (this.tripleKills > 0)
			{
				killStreakEnum |= KillStreakEnum.trippleKill;
			}
			if (this.quadKills > 0)
			{
				killStreakEnum |= KillStreakEnum.quadkill;
			}
			if (this.longShots > 0)
			{
				killStreakEnum |= KillStreakEnum.longShot;
			}
			if (this.grenadeKills > 0)
			{
				killStreakEnum |= KillStreakEnum.grenade;
			}
			if (this.rageKills > 0)
			{
				killStreakEnum |= KillStreakEnum.rage;
			}
			if (this.stormKills > 0)
			{
				killStreakEnum |= KillStreakEnum.storm;
			}
			if (this.suicides > 0)
			{
				killStreakEnum |= KillStreakEnum.suicide;
			}
			if (this.proKills > 0)
			{
				killStreakEnum |= KillStreakEnum.prokill;
			}
			if (this.assists > 0)
			{
				killStreakEnum |= KillStreakEnum.assist;
			}
			if (this.vipkills > 0)
			{
				killStreakEnum |= KillStreakEnum.vipkill;
			}
			if (this.doubleHeadShots > 0)
			{
				killStreakEnum |= KillStreakEnum.doubleHeadshot;
			}
			for (int i = 0; i < Peer.ServerGame.ServerNetPlayers.Count; i++)
			{
				ServerNetPlayer serverNetPlayer = Peer.ServerGame.ServerNetPlayers[i];
				if (serverNetPlayer.Stats.kills > this.kills && this.kills != 0)
				{
					killStreakEnum--;
					break;
				}
				if (serverNetPlayer.Stats.headShots > this.headShots && this.headShots != 0)
				{
					killStreakEnum -= 2;
					break;
				}
				if (serverNetPlayer.Stats.doubleKills > this.doubleKills && this.doubleKills != 0)
				{
					killStreakEnum -= 4;
					break;
				}
				if (serverNetPlayer.Stats.tripleKills > this.tripleKills && this.tripleKills != 0)
				{
					killStreakEnum -= 8;
					break;
				}
				if (serverNetPlayer.Stats.quadKills > this.quadKills && this.quadKills != 0)
				{
					killStreakEnum -= 32768;
					break;
				}
				if (serverNetPlayer.Stats.longShots > this.longShots && this.longShots != 0)
				{
					killStreakEnum -= 16;
					break;
				}
				if (serverNetPlayer.Stats.grenadeKills > this.grenadeKills && this.grenadeKills != 0)
				{
					killStreakEnum -= 32;
					break;
				}
				if (serverNetPlayer.Stats.rageKills > this.doubleKills && this.doubleKills != 0)
				{
					killStreakEnum -= 64;
					break;
				}
				if (serverNetPlayer.Stats.stormKills > this.stormKills && this.stormKills != 0)
				{
					killStreakEnum -= 128;
					break;
				}
				if (serverNetPlayer.Stats.suicides > this.suicides && this.suicides != 0)
				{
					killStreakEnum -= 256;
					break;
				}
				if (serverNetPlayer.Stats.proKills > this.proKills && this.proKills != 0)
				{
					killStreakEnum -= 1024;
					break;
				}
				if (serverNetPlayer.Stats.assists > this.assists && this.assists != 0)
				{
					killStreakEnum -= 512;
					break;
				}
				if (serverNetPlayer.Stats.vipkills > this.vipkills && this.vipkills != 0)
				{
					killStreakEnum -= 16384;
					break;
				}
				if (serverNetPlayer.Stats.doubleHeadShots > this.doubleHeadShots && this.doubleHeadShots != 0)
				{
					killStreakEnum -= 8192;
					break;
				}
			}
			return (int)killStreakEnum;
		}
	}

	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x0600137E RID: 4990 RVA: 0x000D1E90 File Offset: 0x000D0090
	public string timeOnlineStr
	{
		get
		{
			int num = this.timeOnline / 3600;
			int num2 = (this.timeOnline - num * 3600) / 60;
			return string.Concat(new object[]
			{
				num,
				" ",
				Language.H,
				" ",
				num2,
				" ",
				Language.M
			});
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x0600137F RID: 4991 RVA: 0x000D1F04 File Offset: 0x000D0104
	public string timeHardcoreStr
	{
		get
		{
			return string.Concat(new object[]
			{
				this.timeHardcore / 3600,
				" ",
				Language.H,
				" ",
				(this.timeHardcore - this.timeHardcore / 3600 * 3600) / 60,
				" ",
				Language.M
			});
		}
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x000D1F7C File Offset: 0x000D017C
	public void StartStreak()
	{
		this.killStreak = KillStreakEnum.none;
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x000D1F88 File Offset: 0x000D0188
	public void EndStreak(ServerNetPlayer player)
	{
		if (this.NowEndStreak() == KillStreakEnum.none)
		{
			return;
		}
		player.KillStreak(this.NowEndStreak());
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000D1FA4 File Offset: 0x000D01A4
	public KillStreakEnum NowEndStreak()
	{
		KillStreakEnum killStreakEnum = this.killStreak;
		if (BIT.AND((int)killStreakEnum, 1) && killStreakEnum != KillStreakEnum.kill)
		{
			killStreakEnum--;
		}
		if (BIT.AND((int)killStreakEnum, 2) && killStreakEnum != KillStreakEnum.headshot)
		{
			killStreakEnum -= 2;
		}
		if (BIT.AND((int)killStreakEnum, 32) && killStreakEnum != KillStreakEnum.grenade)
		{
			killStreakEnum -= 32;
		}
		return killStreakEnum;
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000D2004 File Offset: 0x000D0204
	public void Suicide(ServerNetPlayer target)
	{
		this.Kill(target);
		if (!target.playerInfo.skillUnlocked(Skills.car_suicide) && !Peer.HardcoreMode)
		{
			target.Exp(-10f, 0f, true);
		}
		this.suicides++;
		this.killStreak |= KillStreakEnum.suicide;
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000D206C File Offset: 0x000D026C
	public void GrenadeKill(ServerNetPlayer player, ServerNetPlayer target)
	{
		if (!player.IsTeam(target))
		{
			this.grenadeKills++;
			this.killStreak |= KillStreakEnum.grenade;
		}
		foreach (ServerNetPlayer serverNetPlayer in Peer.ServerGame.ServerNetPlayers)
		{
			if (serverNetPlayer != player && serverNetPlayer.IsAlive && serverNetPlayer.playerInfo.skillUnlocked(Skills.mast_dest))
			{
				serverNetPlayer.Exp(5f, 0f, true);
			}
		}
		this.SafeKill(player, false, false, target, WeaponNature.grenade, null);
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000D2140 File Offset: 0x000D0340
	public void ExternKill(ServerNetPlayer player, bool HeadShot, bool LongShot, ServerNetPlayer target)
	{
		this.tripleKillTimer.Stop();
		this.doubleKillTimer.Stop();
		this.doubleHeadshotsTimer.Stop();
		this.quadKillTimer.Stop();
		if (player == null)
		{
			return;
		}
		this.SafeKill(player, false, false, target, WeaponNature.mortar, null);
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000D2198 File Offset: 0x000D0398
	public void SafeKill(ServerNetPlayer killer, bool HeadShot, bool LongShot, ServerNetPlayer target, WeaponNature nature, BaseWeapon weapon = null)
	{
		try
		{
			this.Kill(killer, HeadShot, LongShot, target, nature, weapon);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
		}
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000D21E4 File Offset: 0x000D03E4
	public void Kill(ServerNetPlayer killer, bool HeadShot, bool LongShot, ServerNetPlayer target, WeaponNature nature, BaseWeapon weapon = null)
	{
		bool flag = (killer.UserInfo.clanID != 0 || target.UserInfo.clanID != 0) && killer.UserInfo.clanID == target.UserInfo.clanID;
		if (flag && !ServerLeagueSystem.Enabled)
		{
			if (killer.IsTeam(target))
			{
				this.teamKillBuffer++;
				killer.Exp((!Peer.HardcoreMode) ? -10f : ((!target.IsVip) ? -50f : (-CVars.g_vipassistexp * 2f)), 0f, true);
			}
			if (this.teamKillBuffer >= 5)
			{
				this.teamKills += this.teamKillBuffer;
			}
			this.Kill(target);
			return;
		}
		int num = 0;
		foreach (object obj in Enum.GetValues(typeof(AimbotButtons)))
		{
			AimbotButtons aimbotButtons = (AimbotButtons)((int)obj);
			if ((killer.CheatButtons & 1 << num) != 0)
			{
				this.KillCheatButtons.Add(killer.CheatButtons);
			}
			num++;
		}
		float num2 = 1f;
		this.Kill(target);
		bool flag2 = false;
		bool flag3 = false;
		if (Main.IsTeamGame)
		{
			flag2 = target.IsAssist(killer);
			if (killer.IsTeam(target))
			{
				flag3 = true;
			}
			if (flag3)
			{
				flag2 = false;
			}
			if (flag3)
			{
				this.teamKillBuffer++;
			}
			if (this.teamKillBuffer >= 5)
			{
				this.teamKills += this.teamKillBuffer;
			}
			if (target.IsBear && !flag3)
			{
				this.bearKills++;
				if (Main.IsTeamElimination)
				{
					Peer.ServerGame.BearWins(false);
				}
			}
			if (!target.IsBear && !flag3)
			{
				this.usecKills++;
				if (Main.IsTeamElimination)
				{
					Peer.ServerGame.BearWins(true);
				}
			}
		}
		if (flag3)
		{
			killer.Exp((float)((!Peer.HardcoreMode) ? -10 : -50), 0f, true);
			return;
		}
		float num3 = 1f;
		if (killer.playerInfo.skillUnlocked(Skills.car_streak12))
		{
			num3 = 1.5f;
		}
		if (killer.playerInfo.skillUnlocked(Skills.mast_p) && nature == WeaponNature.pistol)
		{
			num2 *= 2f;
		}
		if (killer.playerInfo.skillUnlocked(Skills.mast_scout) && nature == WeaponNature.knife)
		{
			num2 *= 3f;
		}
		if (killer.playerInfo.skillUnlocked(Skills.mast_storm) && target.playerInfo.playerClass == PlayerClass.storm_trooper)
		{
			num2 *= 1.5f;
		}
		if (killer.Ammo != null && killer.playerInfo.clanSkillUnlocked(Cl_Skills.cl_sni1) && killer.playerInfo.playerClass == PlayerClass.sniper && killer.Ammo.CurrentWeapon.weaponNature == WeaponNature.sniper_rifle)
		{
			num2 *= 1.3f;
		}
		if (killer.playerInfo.clanSkillUnlocked(Cl_Skills.cl_train2) && target.UserInfo.clanID != 0 && target.UserInfo.clanID != killer.UserInfo.clanID)
		{
			num2 *= 1.1f;
		}
		this.ongoingKills++;
		this.kills++;
		if (nature == WeaponNature.knife)
		{
			this.knifeKills++;
		}
		if (CVars.g_radio)
		{
			if (killer.IsBear && Main.IsTargetDesignation)
			{
				System.Random random = new System.Random();
				int num4 = random.Next(3);
				if (num4 == 0)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed1, true);
					Peer.ServerGame.Chat(killer, Language.StatsOneDone, ChatInfo.radio_message_at_hit);
				}
				if (num4 == 1)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed2, true);
					Peer.ServerGame.Chat(killer, Language.StatsMinusOne, ChatInfo.radio_message_at_hit);
				}
				if (num4 == 2)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed3, true);
					Peer.ServerGame.Chat(killer, Language.StatsThisOneDone, ChatInfo.radio_message_at_hit);
				}
			}
			else if (!killer.IsBear && Main.IsTargetDesignation)
			{
				System.Random random2 = new System.Random();
				int num5 = random2.Next(3);
				if (num5 == 0)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed1, false);
					Peer.ServerGame.Chat(killer, Language.StatsOneDone, ChatInfo.radio_message_at_hit);
				}
				if (num5 == 1)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed2, false);
					Peer.ServerGame.Chat(killer, Language.StatsMinusOne, ChatInfo.radio_message_at_hit);
				}
				if (num5 == 2)
				{
					Peer.ServerGame.Radio(RadioEnum.bear_killconfirmed3, false);
					Peer.ServerGame.Chat(killer, Language.StatsThisOneDone, ChatInfo.radio_message_at_hit);
				}
			}
		}
		if (target.IsVip)
		{
			this.killStreak |= KillStreakEnum.vipkill;
		}
		bool flag4 = weapon == null;
		Weapons type = (!flag4) ? weapon.type : Weapons.none;
		bool isModable = !flag4 && weapon.IsModable;
		if (flag2)
		{
			float num6 = (!target.IsVip) ? CVars.g_assistexp : CVars.g_vipassistexp;
			num6 = (float)Mathf.CeilToInt(num6 * num2);
			float exp = killer.Exp(num6, (float)killer.GetKillBonus(target), true);
			target.TransferAssistKill(killer);
			this.AddExpToWeapon(killer, type, isModable, exp);
		}
		else if (target.IsVip)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.notify_message, Language.killedVIP);
			float num6 = CVars.g_vipassistexp;
			if (killer.Ammo != null && killer.playerInfo.clanSkillUnlocked(Cl_Skills.cl_sni1) && killer.playerInfo.playerClass == PlayerClass.sniper && killer.Ammo.CurrentWeapon.weaponNature == WeaponNature.sniper_rifle)
			{
				num6 *= 1.3f;
			}
			float exp2 = killer.Exp(num6, (float)killer.GetKillBonus(target), true);
			this.AddExpToWeapon(killer, type, isModable, exp2);
		}
		else
		{
			float num6;
			if (LongShot)
			{
				this.longShots++;
				this.killStreak |= KillStreakEnum.longShot;
				float num7 = 1f;
				if (killer.playerInfo.skillUnlocked(Skills.mast_sni))
				{
					num7 = 2f;
				}
				num6 = (float)Mathf.CeilToInt(15f * num2 * num7);
				float exp3 = killer.Exp(num6, (float)killer.GetKillBonus(target), true);
				this.AddExpToWeapon(killer, type, isModable, exp3);
			}
			else if (HeadShot)
			{
				this.headShots++;
				this.killStreak |= KillStreakEnum.headshot;
				num6 = (float)Mathf.CeilToInt(10f * num2);
				float exp4 = killer.Exp(num6, (float)killer.GetKillBonus(target), true);
				this.AddExpToWeapon(killer, type, isModable, exp4);
			}
			else
			{
				this.killStreak |= KillStreakEnum.kill;
				num6 = (float)Mathf.CeilToInt(5f * num2);
				float exp5 = killer.Exp(num6, (float)killer.GetKillBonus(target), true);
				this.AddExpToWeapon(killer, type, isModable, exp5);
			}
			if (killer.IsVip)
			{
				Peer.ServerGame.VIPMakeKill(num6);
			}
		}
		if (this.tripleKillTimer.Elapsed > CVars.g_killDelay)
		{
			this.tripleKillTimer.Stop();
		}
		if (this.quadKillTimer.Elapsed > CVars.g_killDelay)
		{
			this.quadKillTimer.Stop();
		}
		if (this.doubleKillTimer.Elapsed > CVars.g_killDelay)
		{
			this.doubleKillTimer.Stop();
		}
		if (this.doubleHeadshotsTimer.Elapsed > CVars.g_killDelay)
		{
			this.doubleHeadshotsTimer.Stop();
		}
		if (this.tripleKillTimer.Enabled)
		{
			this.doubleKillTimer.Stop();
			this.doubleHeadshotsTimer.Stop();
			this.tripleKillTimer.Stop();
			this.quadKillTimer.Start();
			this.tripleKills++;
			this.killStreak |= KillStreakEnum.trippleKill;
			if (BIT.AND((int)this.killStreak, 4))
			{
				this.killStreak -= 4;
			}
			float num6 = 10f * num3;
			float exp6 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeTripleKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp6);
		}
		else if (this.quadKillTimer.Enabled)
		{
			this.tripleKillTimer.Stop();
			this.quadKillTimer.Stop();
			this.quadKills++;
			this.killStreak |= KillStreakEnum.quadkill;
			float num6 = 20f * num3;
			float exp7 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeQuadKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp7);
		}
		else if (this.doubleHeadshotsTimer.Enabled && HeadShot)
		{
			this.doubleKillTimer.Stop();
			this.doubleHeadshotsTimer.Stop();
			this.tripleKillTimer.Start();
			this.doubleHeadShots++;
			this.killStreak |= KillStreakEnum.doubleHeadshot;
			if (!BIT.AND((int)this.killStreak, 8))
			{
				this.killStreak |= KillStreakEnum.doubleKill;
			}
			float num6 = 15f * num3;
			float exp8 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeDoubleHeadshot);
			}
			this.AddExpToWeapon(killer, type, isModable, exp8);
		}
		else if (this.doubleKillTimer.Enabled)
		{
			this.doubleKillTimer.Stop();
			this.doubleHeadshotsTimer.Stop();
			this.tripleKillTimer.Start();
			this.doubleKills++;
			this.killStreak |= KillStreakEnum.doubleKill;
			float num6 = 5f * num3;
			float exp9 = killer.Exp(num6, 0f, true);
			this.AddExpToWeapon(killer, type, isModable, exp9);
		}
		else
		{
			this.doubleKillTimer.Start();
			if (HeadShot)
			{
				this.doubleHeadshotsTimer.Start();
			}
		}
		if (this.ongoingKills == 5)
		{
			this.rageKills++;
			this.killStreak |= KillStreakEnum.rage;
			float num6 = 15f * num3;
			float exp10 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeRageKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp10);
		}
		if (killer.UnlockClanSkill(Cl_Skills.cl_weap1) && killer.PlayerClass == PlayerClass.gunsmith && this.ongoingKills == 3)
		{
			this._receivedSonarEarlier = true;
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerRecieveSonar);
			killer.ArmStreak(ArmstreakEnum.sonar);
		}
		else if (!this._receivedSonarEarlier && this.ongoingKills == 4)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerRecieveSonar);
			killer.ArmStreak(ArmstreakEnum.sonar);
		}
		if (killer.UnlockClanSkill(Cl_Skills.cl_weap1) && killer.PlayerClass == PlayerClass.gunsmith && this.ongoingKills == 5)
		{
			this._receivedMortarEarlier = true;
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerRecieveMortar);
			killer.ArmStreak(ArmstreakEnum.mortar);
		}
		else if (!this._receivedMortarEarlier && this.ongoingKills == 6)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerRecieveMortar);
			killer.ArmStreak(ArmstreakEnum.mortar);
		}
		if (this.ongoingKills == 10)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerMakeStormKill);
			this.stormKills++;
			this.killStreak |= KillStreakEnum.storm;
			float num6 = 40f * num3;
			float exp11 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeStormKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp11);
		}
		if (this.ongoingKills == 20)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerMakeProKill);
			this.proKills++;
			this.killStreak |= KillStreakEnum.prokill;
			float num6 = 100f * num3;
			float exp12 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeProKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp12);
		}
		if (this.ongoingKills == 40 && Globals.I.LegendaryKill)
		{
			Peer.ServerGame.EventMessage(killer.Nick, ChatInfo.gameflow_message, Language.PlayerMakeLegendaryKill);
			this.legendaryKills++;
			this.killStreak |= KillStreakEnum.legendarykill;
			float num6 = 500f * num3;
			float exp13 = killer.Exp(num6, 0f, true);
			if (Peer.HardcoreMode)
			{
				killer.EventMessage(string.Empty, ChatInfo.gameflow_message, Language.YouMadeProKill);
			}
			this.AddExpToWeapon(killer, type, isModable, exp13);
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x000D2FCC File Offset: 0x000D11CC
	public void Kill(ServerNetPlayer target)
	{
		target.Stats.doubleKillTimer.Stop();
		target.Stats.doubleHeadshotsTimer.Stop();
		target.Stats.tripleKillTimer.Stop();
		target.Stats.quadKillTimer.Stop();
		target.Stats.ongoingKills = 0;
		target.Stats.deaths++;
		if (Peer.HardcoreMode)
		{
			target.Exp(-2.5f, 0f, true);
		}
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000D3054 File Offset: 0x000D1254
	public void AddExpToWeapon(ServerNetPlayer player, Weapons type, bool isModable, float exp)
	{
		int num = Mathf.CeilToInt(exp);
		num *= player.WeaponExpMult;
		player.UserInfo.CurrentMpExp += num;
		player.ToClient("MasteringExpFromServer", new object[]
		{
			num
		});
		if (player.UserInfo.CurrentMpExp >= Globals.I.MasteringExpToNextPoint)
		{
			player.Stats.obtainedMP++;
			player.UserInfo.CurrentMpExp -= Globals.I.MasteringExpToNextPoint;
			player.ToClient("MpFromServer", new object[]
			{
				player.UserInfo.CurrentMpExp
			});
		}
		Dictionary<string, float> masteringWeaponStats = player.Stats.MasteringWeaponStats;
		if (!masteringWeaponStats.ContainsKey("general_exp"))
		{
			masteringWeaponStats.Add("general_exp", 0f);
		}
		Dictionary<string, float> dictionary2;
		Dictionary<string, float> dictionary = dictionary2 = masteringWeaponStats;
		string key2;
		string key = key2 = "general_exp";
		float num2 = dictionary2[key2];
		dictionary[key] = num2 + (float)num;
		if (type == Weapons.none || !isModable)
		{
			return;
		}
		int num3 = (int)type;
		string text = num3.ToString();
		if (!masteringWeaponStats.ContainsKey(text))
		{
			masteringWeaponStats.Add(text, 0f);
		}
		Dictionary<string, float> dictionary4;
		Dictionary<string, float> dictionary3 = dictionary4 = masteringWeaponStats;
		string key3 = key2 = text;
		num2 = dictionary4[key2];
		dictionary3[key3] = num2 + (float)num;
	}

	// Token: 0x0400169B RID: 5787
	public Int killDelta = new Int();

	// Token: 0x0400169C RID: 5788
	public int timeOnline;

	// Token: 0x0400169D RID: 5789
	public int timeHardcore;

	// Token: 0x0400169E RID: 5790
	public int totalDamage;

	// Token: 0x0400169F RID: 5791
	public int totalAmmo;

	// Token: 0x040016A0 RID: 5792
	public int headShots;

	// Token: 0x040016A1 RID: 5793
	public int doubleKills;

	// Token: 0x040016A2 RID: 5794
	public int doubleHeadShots;

	// Token: 0x040016A3 RID: 5795
	public int tripleKills;

	// Token: 0x040016A4 RID: 5796
	public int quadKills;

	// Token: 0x040016A5 RID: 5797
	public int longShots;

	// Token: 0x040016A6 RID: 5798
	public int grenadeKills;

	// Token: 0x040016A7 RID: 5799
	public int suicides;

	// Token: 0x040016A8 RID: 5800
	public int rageKills;

	// Token: 0x040016A9 RID: 5801
	public int stormKills;

	// Token: 0x040016AA RID: 5802
	public int proKills;

	// Token: 0x040016AB RID: 5803
	public int legendaryKills;

	// Token: 0x040016AC RID: 5804
	public int assists;

	// Token: 0x040016AD RID: 5805
	public int teamKills;

	// Token: 0x040016AE RID: 5806
	public int usecKills;

	// Token: 0x040016AF RID: 5807
	public int bearKills;

	// Token: 0x040016B0 RID: 5808
	public int knifeKills;

	// Token: 0x040016B1 RID: 5809
	public int sitkills;

	// Token: 0x040016B2 RID: 5810
	public int peepkills;

	// Token: 0x040016B3 RID: 5811
	public int vipkills;

	// Token: 0x040016B4 RID: 5812
	public int favGun;

	// Token: 0x040016B5 RID: 5813
	public int[] weaponKills = new int[0];

	// Token: 0x040016B6 RID: 5814
	public int creditsSpent;

	// Token: 0x040016B7 RID: 5815
	public int armstreaksEarned;

	// Token: 0x040016B8 RID: 5816
	public int armstreaksUsed;

	// Token: 0x040016B9 RID: 5817
	public int totalHits;

	// Token: 0x040016BA RID: 5818
	public int matchesStarted;

	// Token: 0x040016BB RID: 5819
	public int matchesEnded;

	// Token: 0x040016BC RID: 5820
	public int wins;

	// Token: 0x040016BD RID: 5821
	public int loses;

	// Token: 0x040016BE RID: 5822
	public int kills;

	// Token: 0x040016BF RID: 5823
	public int deaths;

	// Token: 0x040016C0 RID: 5824
	public int IsWinner;

	// Token: 0x040016C1 RID: 5825
	public float timeStart = -1f;

	// Token: 0x040016C2 RID: 5826
	public int timerOnline;

	// Token: 0x040016C3 RID: 5827
	public int achivCR;

	// Token: 0x040016C4 RID: 5828
	public float obtainedXP;

	// Token: 0x040016C5 RID: 5829
	public int obtainedSP;

	// Token: 0x040016C6 RID: 5830
	public int obtainedCR;

	// Token: 0x040016C7 RID: 5831
	public int obtainedMP;

	// Token: 0x040016C8 RID: 5832
	public int teamKillBuffer;

	// Token: 0x040016C9 RID: 5833
	public eTimer doubleKillTimer = new eTimer();

	// Token: 0x040016CA RID: 5834
	public eTimer doubleHeadshotsTimer = new eTimer();

	// Token: 0x040016CB RID: 5835
	public eTimer tripleKillTimer = new eTimer();

	// Token: 0x040016CC RID: 5836
	public eTimer quadKillTimer = new eTimer();

	// Token: 0x040016CD RID: 5837
	public int ongoingKills;

	// Token: 0x040016CE RID: 5838
	private KillStreakEnum killStreak;

	// Token: 0x040016CF RID: 5839
	private bool _receivedMortarEarlier;

	// Token: 0x040016D0 RID: 5840
	private bool _receivedSonarEarlier;

	// Token: 0x040016D1 RID: 5841
	public Dictionary<string, float> MasteringWeaponStats = new Dictionary<string, float>();

	// Token: 0x040016D2 RID: 5842
	public List<int> KillCheatButtons = new List<int>();
}
