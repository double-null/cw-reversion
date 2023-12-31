using System;
using System.Collections.Generic;
using System.Reflection;
using BannerGUINamespace.BannerQueueNamespace;
using UnityEngine;

// Token: 0x020000D1 RID: 209
[AddComponentMenu("Scripts/GUI/BannerGUI")]
internal class BannerGUI : Form
{
	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06000566 RID: 1382 RVA: 0x000246DC File Offset: 0x000228DC
	private UserInfo Info
	{
		get
		{
			return Main.UserInfo;
		}
	}

	// Token: 0x06000567 RID: 1383 RVA: 0x000246E4 File Offset: 0x000228E4
	[Obfuscation(Exclude = true)]
	private void Armstreak(object obj)
	{
		ArmstreakEnum armstreakEnum = (ArmstreakEnum)((int)obj);
		ArmstrData armstrData = this.armstreaks[(int)armstreakEnum];
		if (Peer.HardcoreMode)
		{
			if (armstreakEnum == ArmstreakEnum.mortar || armstreakEnum == ArmstreakEnum.sonar)
			{
				if ((this.bMortar || armstreakEnum != ArmstreakEnum.mortar) && (this.bSonar || armstreakEnum != ArmstreakEnum.sonar))
				{
					return;
				}
				armstrData.window.Show(0.5f, 0f);
				if (armstreakEnum == ArmstreakEnum.mortar)
				{
					this.bMortar = true;
				}
				if (armstreakEnum == ArmstreakEnum.sonar)
				{
					this.bSonar = true;
				}
			}
		}
		else if (armstreakEnum == ArmstreakEnum.mortar || armstreakEnum == ArmstreakEnum.sonar)
		{
			if ((this.bMortar || armstreakEnum != ArmstreakEnum.mortar) && (this.bSonar || armstreakEnum != ArmstreakEnum.sonar))
			{
				return;
			}
			armstrData.window.Show(0.5f, 0f);
			BannerMortar bannerMortar = new BannerMortar();
			if (armstreakEnum == ArmstreakEnum.mortar)
			{
				bannerMortar.button = Main.UserInfo.settings.binds.mortar.ToString().Replace("Alpha", string.Empty);
			}
			if (armstreakEnum == ArmstreakEnum.sonar)
			{
				bannerMortar.button = Main.UserInfo.settings.binds.support.ToString().Replace("Alpha", string.Empty);
			}
			bannerMortar.buttonWidth = 0f;
			bannerMortar.currentArmstr = armstrData;
			this.bannerQueue.Add(bannerMortar);
			if (armstreakEnum == ArmstreakEnum.mortar)
			{
				this.bMortar = true;
			}
			if (armstreakEnum == ArmstreakEnum.sonar)
			{
				this.bSonar = true;
			}
		}
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x00024874 File Offset: 0x00022A74
	[Obfuscation(Exclude = true)]
	private void ArmstreakUsed(object obj)
	{
		ArmstreakEnum armstreakEnum = (ArmstreakEnum)((int)obj);
		ArmstrData armstrData = this.armstreaks[(int)armstreakEnum];
		armstrData.window.Hide(0.5f, 0f);
		if (armstreakEnum == ArmstreakEnum.mortar)
		{
			this.bMortar = false;
		}
		if (armstreakEnum == ArmstreakEnum.sonar)
		{
			this.bSonar = false;
		}
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x000248C4 File Offset: 0x00022AC4
	[Obfuscation(Exclude = true)]
	private void Wtask(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		object[] array = (object[])obj;
		string text = (string)array[0];
		int num = (int)array[1];
		int num2 = (int)array[2];
		AchivData achivData = new AchivData();
		achivData.type = AchivDataType.wtask;
		achivData.text = text;
		achivData.left = num.ToString();
		achivData.right = "/" + num2.ToString();
		this.ongoingAchiv.Add(achivData);
	}

	// Token: 0x0600056A RID: 1386 RVA: 0x00024948 File Offset: 0x00022B48
	[Obfuscation(Exclude = true)]
	private void WtaskGained(object obj)
	{
		if (!Peer.HardcoreMode)
		{
			string tempName = (string)obj;
			BannerWtask bannerWtask = new BannerWtask();
			bannerWtask.current.type = BannerInfoType.wtask;
			bannerWtask.current.wtask = new WtaskStreak();
			bannerWtask.current.wtask.tempName = tempName;
			this.bannerQueue.Add(bannerWtask);
		}
	}

	// Token: 0x0600056B RID: 1387 RVA: 0x000249A8 File Offset: 0x00022BA8
	[Obfuscation(Exclude = true)]
	private void WtaskFailed(object obj)
	{
		if (!Peer.HardcoreMode)
		{
			object[] array = (object[])obj;
			int num = (int)array[1];
			int num2 = (int)array[2];
			AchivData achivData = new AchivData();
			achivData.type = AchivDataType.wtask;
			achivData.text = Language.WTaskNotCount;
			achivData.left = num.ToString();
			achivData.right = "/" + num2.ToString();
			this.ongoingAchiv.Add(achivData);
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00024A20 File Offset: 0x00022C20
	[Obfuscation(Exclude = true)]
	private void Achievement(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		int num = (int)obj;
		bool flag = true;
		if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
		{
			for (int i = 0; i < this.ongoingAchiv.Count; i++)
			{
				if (this.ongoingAchiv[i].difficult == Main.UserInfo.achievementsInfos[num].difficult && this.ongoingAchiv[i].weapNature == Main.UserInfo.achievementsInfos[num].weaponNature)
				{
					flag = false;
				}
			}
		}
		if (!flag)
		{
			return;
		}
		AchivData achivData = new AchivData();
		achivData.type = AchivDataType.achievement;
		achivData.text = Main.UserInfo.achievementsInfos[num].name;
		achivData.left = Main.UserInfo.achievementsInfos[num].current.ToString();
		achivData.right = "/" + Main.UserInfo.achievementsInfos[num].count.ToString();
		achivData.difficult = Main.UserInfo.achievementsInfos[num].difficult;
		achivData.weapNature = Main.UserInfo.achievementsInfos[num].weaponNature;
		this.ongoingAchiv.Add(achivData);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x00024B70 File Offset: 0x00022D70
	[Obfuscation(Exclude = true)]
	private void AchievementFailed(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		AchivData achivData = new AchivData();
		achivData.type = AchivDataType.achievement;
		achivData.text = Language.AchievementWillNotCount;
		achivData.left = string.Empty;
		achivData.right = "0";
		this.ongoingAchiv.Add(achivData);
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x00024BC4 File Offset: 0x00022DC4
	[Obfuscation(Exclude = true)]
	private void AchievementGained(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		int index = (int)obj;
		BannerAchievement item = new BannerAchievement
		{
			current = 
			{
				type = BannerInfoType.achievement,
				achievement = new AchievementBannerInfo
				{
					index = index
				}
			}
		};
		this.bannerQueue.Add(item);
	}

	// Token: 0x0600056F RID: 1391 RVA: 0x00024C1C File Offset: 0x00022E1C
	[Obfuscation(Exclude = true)]
	private void Card(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		int num = (int)obj;
		int count = Main.UserInfo.achievementsInfos[num].count;
		if (count > 100)
		{
			if (count % 20 != 0)
			{
				return;
			}
		}
		else if (count > 15 && count % 20 != 0)
		{
			return;
		}
		AchivData achivData = new AchivData();
		achivData.type = AchivDataType.cards;
		this.ongoingAchiv.Add(achivData);
	}

	// Token: 0x06000570 RID: 1392 RVA: 0x00024C90 File Offset: 0x00022E90
	[Obfuscation(Exclude = true)]
	private void CardGained(object obj)
	{
		BannerInfo bannerInfo = new BannerInfo();
		bannerInfo.type = BannerInfoType.cards;
	}

	// Token: 0x06000571 RID: 1393 RVA: 0x00024CAC File Offset: 0x00022EAC
	[Obfuscation(Exclude = true)]
	private void LevelUp(object[] args)
	{
		if (!Peer.HardcoreMode)
		{
			int level = (int)Crypt.ResolveVariable(args, 0, 0);
			int sp = (int)Crypt.ResolveVariable(args, 0, 1);
			BannerLevelUP bannerLevelUP = new BannerLevelUP();
			bannerLevelUP.current.level = level;
			bannerLevelUP.current.sp = sp;
			bannerLevelUP.current.type = BannerInfoType.levelup;
			this.bannerQueue.Add(bannerLevelUP);
		}
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x00024D20 File Offset: 0x00022F20
	[Obfuscation(Exclude = true)]
	private void GainMp(object[] args)
	{
		BannerGUI.drawWeaponLevelUp = true;
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x00024D28 File Offset: 0x00022F28
	[Obfuscation(Exclude = true)]
	private void KillStreak(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		KillStreakEnum killStreakEnum = (KillStreakEnum)((int)obj);
		for (int i = 0; i < this.streaks.Length; i++)
		{
			if ((this.streaks[i].type & killStreakEnum) != KillStreakEnum.none)
			{
				BannerKillStreak bannerKillStreak = new BannerKillStreak();
				bannerKillStreak.current.streak = this.streaks[i];
				bannerKillStreak.current.type = BannerInfoType.killstreak;
				this.bannerQueue.Add(bannerKillStreak);
			}
		}
	}

	// Token: 0x06000574 RID: 1396 RVA: 0x00024DA8 File Offset: 0x00022FA8
	[Obfuscation(Exclude = true)]
	private void Exp(object obj)
	{
		object[] array = (object[])obj;
		int num = (int)array[0];
		int num2 = (int)array[1];
		this.ongoingExp.Add(new int[]
		{
			num,
			num2
		});
	}

	// Token: 0x06000575 RID: 1397 RVA: 0x00024DE8 File Offset: 0x00022FE8
	[Obfuscation(Exclude = true)]
	private void DeadBanner(object obj)
	{
		if (!Peer.HardcoreMode)
		{
			object[] array = (object[])obj;
			Weapons killMethod = (Weapons)((int)array[0]);
			string playerName = (string)array[1];
			string playerNameColor = (string)array[2];
			int playerRank = (int)array[3];
			bool flag = (bool)array[5];
			KillGUIState killGUIState = new KillGUIState();
			killGUIState.playerName = playerName;
			killGUIState.playerNameColor = playerNameColor;
			killGUIState.playerRank = playerRank;
			killGUIState.killMethod = killMethod;
			killGUIState.wtask = flag;
			killGUIState.clanTag = (string)array[4];
			if (killGUIState.killMethod == Weapons.knife)
			{
				killGUIState.killMethodName = Language.BannerGUIKnife;
			}
			else if (killGUIState.killMethod == Weapons.grenade)
			{
				killGUIState.killMethodName = Language.BannerGUIGrenade;
			}
			else if (killGUIState.killMethod == Weapons.mortar)
			{
				killGUIState.killMethodName = Language.BannerGUIMortarStrike;
			}
			else if (flag)
			{
				killGUIState.killMethodName = Main.UserInfo.weaponsStates[(int)killGUIState.killMethod].CurrentWeapon.ModInterfaceName;
			}
			else
			{
				killGUIState.killMethodName = Main.UserInfo.weaponsStates[(int)killGUIState.killMethod].CurrentWeapon.InterfaceName;
			}
			killGUIState.kill = true;
			this.ongoingKill.Add(killGUIState);
		}
		BannerGUI.killCounter = 0;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x00024F40 File Offset: 0x00023140
	[Obfuscation(Exclude = true)]
	private void KillBanner(object obj)
	{
		this.notEnoughWarrior = false;
		if (Peer.HardcoreMode)
		{
			return;
		}
		object[] array = (object[])obj;
		string playerName = (string)array[0];
		string playerNameColor = (string)array[1];
		int playerRank = (int)array[2];
		KillGUIState killGUIState = new KillGUIState();
		BannerGUI.killCounter += 1;
		killGUIState.playerName = playerName;
		killGUIState.playerNameColor = playerNameColor;
		killGUIState.playerRank = playerRank;
		if (array.Length > 3)
		{
			killGUIState.clanTag = (string)array[3];
		}
		killGUIState.kill = false;
		this.ongoingKill.Add(killGUIState);
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x00024FDC File Offset: 0x000231DC
	[Obfuscation(Exclude = true)]
	private void ClearKillBanner(object obj)
	{
		this.ongoingKill.Clear();
		this.killGNAME = new GraphicValue();
		this.currentKill = null;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x00024FFC File Offset: 0x000231FC
	[Obfuscation(Exclude = true)]
	private void ContractGained(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		if (obj == null)
		{
			return;
		}
		object[] array = (object[])obj;
		ContractTaskType contractTaskType = (ContractTaskType)((int)array[0]);
		if (contractTaskType == ContractTaskType.EasyTask)
		{
			int id = Main.UserInfo.contractsInfo.CurrentEasyIndex;
			int current = (int)array[1];
			int task_counter = Main.UserInfo.contractsInfo.CurrentEasy.task_counter;
			ContractGUIState contractGUIState = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentEasy);
			this.ContractSoundAndAlpha(task_counter, current);
			if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
			{
				if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress && this.IsSimpleShowContract(contractGUIState, current, ref this.easyContractProgress))
				{
					this.ongoingContract_easy.Add(contractGUIState);
				}
				else
				{
					this.currentContract_easy = contractGUIState;
				}
			}
			else
			{
				this.ongoingContract_easy.Add(contractGUIState);
			}
		}
		else if (contractTaskType == ContractTaskType.NormalTask)
		{
			int id = Main.UserInfo.contractsInfo.CurrentNormalIndex;
			int current = (int)array[1];
			int task_counter = Main.UserInfo.contractsInfo.CurrentNormal.task_counter;
			ContractGUIState contractGUIState2 = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentNormal);
			this.ContractSoundAndAlpha(task_counter, current);
			if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
			{
				if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress && this.IsSimpleShowContract(contractGUIState2, current, ref this.normalContractProgress))
				{
					this.ongoingContract_normal.Add(contractGUIState2);
				}
				else
				{
					this.currentContract_normal = contractGUIState2;
				}
			}
			else
			{
				this.ongoingContract_normal.Add(contractGUIState2);
			}
		}
		else
		{
			int id = Main.UserInfo.contractsInfo.CurrentHardIndex;
			int current = (int)array[1];
			int task_counter = Main.UserInfo.contractsInfo.CurrentHard.task_counter;
			ContractGUIState contractGUIState3 = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentHard);
			this.ContractSoundAndAlpha(task_counter, current);
			if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
			{
				if (Main.UserInfo.settings.graphics.ShowingSimpleContractProgress && this.IsSimpleShowContract(contractGUIState3, current, ref this.hardContractProgress))
				{
					this.ongoingContract_hard.Add(contractGUIState3);
				}
				else
				{
					this.currentContract_hard = contractGUIState3;
				}
			}
			else
			{
				this.ongoingContract_hard.Add(contractGUIState3);
			}
		}
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x000252A0 File Offset: 0x000234A0
	private void ContractSoundAndAlpha(int maxCount, int current)
	{
		if (maxCount > 0)
		{
			if (current >= maxCount)
			{
				Audio.Play(this.contract_complete);
			}
			else
			{
				Audio.Play(this.contract_progress);
			}
		}
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x000252D0 File Offset: 0x000234D0
	private bool IsSimpleShowContract(ContractGUIState contract, int current, ref int progress)
	{
		bool result = false;
		if (progress < 1)
		{
			if (contract.MaxProgress > 10)
			{
				progress = current / (contract.MaxProgress / 10) * contract.MaxProgress / 10;
			}
			else
			{
				progress = 1;
			}
		}
		if (progress <= contract.currentProgress && Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
		{
			if (contract.MaxProgress > 10)
			{
				progress += contract.MaxProgress / 10;
			}
			else
			{
				progress++;
			}
			if (progress > contract.MaxProgress)
			{
				progress = contract.MaxProgress;
			}
			result = true;
		}
		else if (!Main.UserInfo.settings.graphics.ShowingSimpleContractProgress)
		{
			result = true;
		}
		return result;
	}

	// Token: 0x0600057B RID: 1403 RVA: 0x00025398 File Offset: 0x00023598
	[Obfuscation(Exclude = true)]
	private void ContractShowAtStart(object obj)
	{
		if (Peer.HardcoreMode)
		{
			return;
		}
		if (obj == null)
		{
			return;
		}
		object[] array = (object[])obj;
		int current = 0;
		ContractTaskType contractTaskType = (ContractTaskType)((int)array[0]);
		if (contractTaskType == ContractTaskType.EasyTask)
		{
			int id = Main.UserInfo.contractsInfo.CurrentEasyIndex;
			if (array.Length > 1)
			{
				if ((int)array[1] == 0)
				{
					current = Main.UserInfo.contractsInfo.CurrentEasyCount;
				}
				else
				{
					current = (int)array[1];
				}
			}
			int task_counter = Main.UserInfo.contractsInfo.CurrentEasy.task_counter;
			this.currentContract_easy = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentEasy);
			this.ongoingContract_easy.Add(this.currentContract_easy);
		}
		else if (contractTaskType == ContractTaskType.NormalTask)
		{
			int id = Main.UserInfo.contractsInfo.CurrentNormalIndex;
			current = Main.UserInfo.contractsInfo.CurrentNormalCount;
			int task_counter = Main.UserInfo.contractsInfo.CurrentNormal.task_counter;
			this.currentContract_normal = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentNormal);
			this.ongoingContract_normal.Add(this.currentContract_normal);
		}
		else
		{
			int id = Main.UserInfo.contractsInfo.CurrentHardIndex;
			current = Main.UserInfo.contractsInfo.CurrentHardCount;
			int task_counter = Main.UserInfo.contractsInfo.CurrentHard.task_counter;
			this.currentContract_hard = new ContractGUIState(id, current, task_counter, contractTaskType, Main.UserInfo.contractsInfo.CurrentHard);
			this.ongoingContract_hard.Add(this.currentContract_hard);
		}
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0002553C File Offset: 0x0002373C
	[Obfuscation(Exclude = true)]
	private void ShowPlacementProgress(object obj)
	{
		this.placementProgress.Show(0.5f, 0f);
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x00025554 File Offset: 0x00023754
	[Obfuscation(Exclude = true)]
	private void HidePlacementProgress(object obj)
	{
		this.placementProgress.Hide(0.5f, 0f);
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0002556C File Offset: 0x0002376C
	[Obfuscation(Exclude = true)]
	private void ShowBeaconUser(object obj)
	{
		this.beaconUserAlpha.Show(0.5f, 0f);
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x00025584 File Offset: 0x00023784
	[Obfuscation(Exclude = true)]
	private void HideBeaconUser(object obj)
	{
		this.beaconUserAlpha.Hide(0.5f, 0f);
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0002559C File Offset: 0x0002379C
	[Obfuscation(Exclude = true)]
	private void SprintShow(object obj)
	{
		this.sprint.Show(0.5f, 0f);
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x000255B4 File Offset: 0x000237B4
	[Obfuscation(Exclude = true)]
	private void SprintHide(object obj)
	{
		this.sprint.Hide(0.5f, 0f);
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x000255CC File Offset: 0x000237CC
	private void TacticalConquestGUI(TacticalPointState state, float progress, int pointIndex, int AllyInPoint, int enemyInPoint, string name)
	{
		if (pointIndex < 0 || Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		TacticalPoint tacticalPoint = this.tacticalPoints[pointIndex];
		if (Peer.ClientGame.LocalPlayer.IsBear && state == TacticalPointState.usec_captured && tacticalPoint.InAction)
		{
			this.gui.ProgressDualTexturedTCMode(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 50)), 200f, progress, this.placement_progressbar[0], this.capturebar_red[1], false);
		}
		else if (Peer.ClientGame.LocalPlayer.IsBear && tacticalPoint.InAction)
		{
			this.gui.ProgressDualTexturedTCMode(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 50)), 200f, progress, this.placement_progressbar[0], this.capturebar_blue[1], true);
		}
		else if (!Peer.ClientGame.LocalPlayer.IsBear && state == TacticalPointState.bear_captured && tacticalPoint.InAction)
		{
			this.gui.ProgressDualTexturedTCMode(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 50)), 200f, progress, this.placement_progressbar[0], this.capturebar_red[1], false);
		}
		else if (!Peer.ClientGame.LocalPlayer.IsBear && tacticalPoint.InAction)
		{
			this.gui.ProgressDualTexturedTCMode(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 50)), 200f, progress, this.placement_progressbar[0], this.capturebar_blue[1], true);
		}
		if (tacticalPoint.InAction)
		{
			AllyInPoint = tacticalPoint.playersNeeded;
		}
		this.DrawSlot(tacticalPoint, AllyInPoint, state);
		if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == -1 && progress == 1f)
		{
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 30)), this.enemycaptured, Vector2.one);
		}
		if (state == TacticalPointState.neutral)
		{
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 30)), this.neutralpoint, Vector2.one);
		}
		if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == 1)
		{
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 30)), this.youcaptured, Vector2.one);
		}
		if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == -1 && progress < 1f)
		{
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 30)), this.enemycaptured, Vector2.one);
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 30)), this.neutralpoint, Vector2.one);
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2 + 100), (float)(Screen.height - 30)), this.youcaptured, Vector2.one);
		}
		this.TextRect.Set((float)(Screen.width / 2 - 180), (float)(Screen.height - 80), 360f, 50f);
		if (!tacticalPoint.InAction && (BaseClientGame.BearCount < 3 || BaseClientGame.UsecCount < 3))
		{
			if (BaseClientGame.BearCount == BaseClientGame.UsecCount)
			{
				this.gui.TextLabel(this.TextRect, (!Peer.ClientGame.LocalPlayer.IsBear) ? Language.NeedMoreUsec : Language.NeedMoreBear, 16, "#FF0000", TextAnchor.UpperCenter, true);
			}
			else if (BaseClientGame.BearCount < BaseClientGame.UsecCount)
			{
				this.gui.TextLabel(this.TextRect, Language.NeedMoreBear, 16, "#FF0000", TextAnchor.UpperCenter, true);
			}
			else
			{
				this.gui.TextLabel(this.TextRect, Language.NeedMoreUsec, 16, "#FF0000", TextAnchor.UpperCenter, true);
			}
		}
		else if (AllyInPoint != 0 && enemyInPoint != 0)
		{
			this.gui.TextLabel(this.TextRect, Language.PointPurification, 16, "#ffffff", TextAnchor.UpperCenter, true);
		}
		else
		{
			if (tacticalPoint.InAction && !tacticalPoint.InEnemyPoint(Peer.ClientGame.LocalPlayer))
			{
				this.gui.TextLabel(this.TextRect, Language.CapturingPoint + " " + name, 16, "#ffffff", TextAnchor.UpperCenter, true);
			}
			if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == -1 && progress != 1f)
			{
				this.gui.TextLabel(this.TextRect, Language.NeutralizePoint + " " + name, 16, "#ffffff", TextAnchor.UpperCenter, true);
			}
			if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == 1 && progress == 1f)
			{
				this.gui.TextLabel(this.TextRect, string.Concat(new string[]
				{
					Language.Point,
					" ",
					name,
					" ",
					Language.FriendCaptured
				}), 16, "#ffffff", TextAnchor.UpperCenter, true);
			}
			if (tacticalPoint.isEnemy(Peer.ClientGame.LocalPlayer) == -1 && progress == 1f)
			{
				this.gui.TextLabel(this.TextRect, string.Concat(new string[]
				{
					Language.Point,
					" ",
					name,
					" ",
					Language.EnemyCaptured
				}), 16, "#ffffff", TextAnchor.UpperCenter, true);
			}
			if (state == TacticalPointState.neutral && !tacticalPoint.InAction)
			{
				this.gui.TextLabel(this.TextRect, Language.NeedMoreWarrior, 16, "#ffffff", TextAnchor.UpperCenter, true);
			}
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00025BD8 File Offset: 0x00023DD8
	private void DrawSlot(TacticalPoint point, int slotUsed, TacticalPointState state)
	{
		if (point.isEnemy(Peer.ClientGame.LocalPlayer) == 1)
		{
			return;
		}
		for (int i = 0; i < point.playersNeeded; i++)
		{
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2 + (this.usedslot.width - 16) / 2) - (float)((this.usedslot.width - 16) * point.playersNeeded) / 2f + (float)((this.usedslot.width - 16) * i), (float)(Screen.height - 110)), (i >= slotUsed) ? this.freeslot : this.usedslot, Vector2.one);
		}
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00025C94 File Offset: 0x00023E94
	private void Splash(Vector2 pos, Texture2D tex)
	{
		this.gui.color = Colors.alpha(this.gui.color, this.gui.color.a);
		this.gui.PictureCentered(pos, tex, Vector2.one * Time.fixedTime * 2.5f);
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00025CF8 File Offset: 0x00023EF8
	private void DrawBuff(Buffs buffs, Buffs buff, Alpha alpha, Texture2D texture, bool blink = true)
	{
		if (Main.UserInfo.settings.graphics.HideInterface)
		{
			return;
		}
		if (alpha.Visible)
		{
			if (blink)
			{
				this.gui.color = new Color(1f, 1f, 1f, alpha.visibility * this.buff_Alpha.Evaluate(Time.realtimeSinceStartup));
			}
			else
			{
				this.gui.color = new Color(1f, 1f, 1f, 1f);
			}
			this.gui.Picture(new Vector2(this.delta, (float)(Screen.height - 160)), texture, Vector2.one);
			if (Peer.Info.TestVip && (buff == Buffs.defender || buff == Buffs.VIP))
			{
				foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
				{
					if (entityNetPlayer.IsVip)
					{
						if (entityNetPlayer.IsTeam(Peer.ClientGame.LocalPlayer))
						{
							this.gui.TextLabel(new Rect(this.delta, (float)(Screen.height - 170), (float)texture.width, 20f), "x" + entityNetPlayer.LifeTimeExpCoef.ToString("F1"), 16, "#FFFFFF", TextAnchor.MiddleCenter, true);
						}
					}
				}
			}
		}
		if (BIT.AND((int)buffs, (int)buff))
		{
			alpha.Show(0.5f, 0f);
			this.delta += (float)(texture.width - 20);
		}
		else
		{
			alpha.Hide(0.5f, 0f);
		}
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00025EF8 File Offset: 0x000240F8
	private void HintGameMode()
	{
		int num = Screen.height / 4;
		if (this.bVipHintWhow && Peer.ClientGame.LocalPlayer.IsVip)
		{
			this.bHintShow = true;
			this.bVipHintWhow = false;
		}
		else if (!this.bHintShow && !Peer.ClientGame.LocalPlayer.IsVip)
		{
			this.bVipHintWhow = true;
		}
		if (this.bHintShow)
		{
			this.alphaHintShow.Show(7f, 0f);
			this.bHintShow = false;
		}
		if (this.alphaHintShow.visibility > 0f || this.alphaHintShow.MaxVisible)
		{
			this.gui.color = Colors.alpha(this.gui.color, this.alphaHintShow.visibility);
			if (Peer.HardcoreMode)
			{
				this.gui.PictureCentered(new Vector2((float)(Screen.width / 2 - 108), (float)(Screen.height / 2 + (num - 10))), this.hardcoreMark, Vector2.one);
				this.CenterdTextPosBacgr((float)Screen.width, (float)(num - 23), "HARDCORE", 18, "#ffffff_Micra");
			}
			if (Main.IsDeathMatch)
			{
				this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.DMHeader, 22, "#ffffff_Micra");
				this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.DMDescription, 16, "#62aeea");
			}
			else if (Peer.ClientGame.LocalPlayer.IsVip)
			{
				this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.VIPHeader, 22, "#ffffff_Micra");
				this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.VIPDescription, 16, "#62aeea");
			}
			else if (Main.IsTargetDesignation)
			{
				if (Peer.ClientGame.LocalPlayer.IsBear)
				{
					this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.TDSHeader, 22, "#ffffff_Micra");
					this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.TDSDescriptionB, 16, "#62aeea");
				}
				else
				{
					this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.TDSHeader, 22, "#ffffff_Micra");
					this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.TDSDescriptionU, 16, "#62aeea");
				}
			}
			else if (Main.IsTeamElimination)
			{
				this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.TEHeader, 22, "#ffffff_Micra");
				this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.TEDescription, 16, "#62aeea");
			}
			else if (Main.IsTacticalConquest)
			{
				this.CenterdTextPosBacgr((float)Screen.width, (float)num, Language.TCHeader, 22, "#ffffff_Micra");
				this.CenterdTextPosBacgr((float)Screen.width, (float)(num + 28), Language.TCDescription, 16, "#62aeea");
			}
			this.gui.color = Colors.alpha(Color.white, base.visibility);
			if (this.alphaHintShow.MaxVisible)
			{
				this.alphaHintShow.Hide(1f, 0f);
			}
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00026224 File Offset: 0x00024424
	private void CenterdTextPosBacgr(float width, float height, string str, int fontsize, string colortext)
	{
		Font font = this.gui.fontDNC57;
		if (colortext.Length >= 8)
		{
			if (colortext[8] == 'T')
			{
				font = this.gui.fontTahoma;
			}
			if (colortext[8] == 'M')
			{
				font = this.gui.fontMicra;
			}
		}
		float num = this.gui.CalcWidth(str, font, fontsize);
		float height2 = this.gui.CalcHeight(str, num, font, fontsize);
		this.gui.TextFieldBacgr(new Rect(Mathf.Abs((width - num) / 2f), (float)(Screen.height / 2) + height, num, height2), str, fontsize, colortext, TextAnchor.UpperCenter, (!this.alphaHintShow.Showing) ? this.alphaHintShow.visibility : (this.alphaHintShow.visibility * 4f));
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00026310 File Offset: 0x00024510
	private void CenterdTextPosBacgrMicra(float width, float height, string str, int fontsize, string colortext)
	{
		Font font = this.gui.fontMicra;
		if (colortext.Length >= 8)
		{
			if (colortext[8] == 'T')
			{
				font = this.gui.fontTahoma;
			}
			if (colortext[8] == 'M')
			{
				font = this.gui.fontMicra;
			}
		}
		float num = this.gui.CalcWidth(str, font, fontsize);
		float height2 = this.gui.CalcHeight(str, num, font, fontsize);
		this.gui.TextFieldBacgr(new Rect(Mathf.Abs((width - num) / 2f), height, num, height2), str, fontsize, colortext, TextAnchor.UpperCenter, 1f);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x000263C8 File Offset: 0x000245C8
	private void WeaponLevelUpBanner()
	{
		this.drawMastering = true;
		this.masteringAlpha.Show(0.25f, 0f);
		Audio.Play(this.weaponLevelUp);
		this.yPos = (float)(Screen.height - 125);
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00026404 File Offset: 0x00024604
	public static void DrawWeaponLevelUpBanner()
	{
		if (BannerGUI.I != null)
		{
			BannerGUI.I.WeaponLevelUpBanner();
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00026420 File Offset: 0x00024620
	public void SpectateForBanner(EntityNetPlayer watched, Alpha watchedAlpha)
	{
		this.gui.color = new Color(1f, 1f, 1f, this.achiv_Alpha.Evaluate(watchedAlpha.visibility));
		this.redPos = new Vector2(0f, (float)(this.killer_bar.height - 10) - this.red_banner_POSY.Evaluate(watchedAlpha.visibility) * (float)this.killer_bar.height);
		this.gui.BeginGroup(new Rect(0f, this.redPos.y, (float)Screen.width, (float)Screen.height));
		this.gui.TextLabel(new Rect(295f + (float)(Screen.width - this.killed_bar.width) * 0.5f, (float)(-5 + Screen.height - this.killed_bar.height), 200f, 25f), Language.SpecViewaAt, 20, "#ffffff", TextAnchor.MiddleLeft, true);
		Rect rect = new Rect((float)(Screen.width - this.killed_bar.width) * 0.5f, (float)(Screen.height - this.killed_bar.height), (float)this.killed_bar.width, (float)this.killed_bar.height);
		this.gui.BeginGroup(rect);
		this.gui.Picture(new Vector2(0f, 0f), this.killed_bar);
		this.gui.Picture(new Vector2(23f, 22f), this.helpersGUI.weaponFrame);
		if (watched != null && watched.Ammo != null)
		{
			if (watched.IsAlive || watched.Ammo.weaponEquiped)
			{
				if (watched.Ammo.CurrentWeapon != null)
				{
					if (watched.Ammo.CurrentWeapon.type < Weapons.lastWeapon)
					{
						if (watched.Ammo.CurrentWeapon.state.isMod)
						{
							this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.gui.weapon_wtask[(int)watched.Ammo.CurrentWeapon.type].width) / 2), 22f), this.gui.weapon_wtask[(int)watched.Ammo.CurrentWeapon.type]);
						}
						else
						{
							this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.gui.weapon_unlocked[(int)watched.Ammo.CurrentWeapon.type].width) / 2), 22f), this.gui.weapon_unlocked[(int)watched.Ammo.CurrentWeapon.type]);
						}
					}
					else
					{
						this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.killMethods[(int)watched.Ammo.CurrentWeapon.type].width) / 2), 22f), this.killMethods[(int)watched.Ammo.CurrentWeapon.type]);
					}
					this.gui.TextLabel(new Rect(29f, 64f, 200f, 19f), watched.Ammo.CurrentWeapon.guiInterfaceName, 14, "#ffffff", TextAnchor.MiddleLeft, true);
				}
			}
			else
			{
				this.gui.TextLabel(new Rect(29f, 64f, 200f, 19f), Language.No, 14, "#ffffff", TextAnchor.MiddleLeft, true);
			}
			float num = this.gui.CalcWidth(watched.playerInfo.clanTag, this.gui.fontDNC57, 22);
			this.gui.Picture(new Vector2(270f, 34f), this.gui.rank_icon[watched.playerInfo.level]);
			this.gui.TextField(new Rect(300f, 42f, 200f, 25f), watched.playerInfo.clanTag, 22, "#d40000", TextAnchor.MiddleLeft, false, false);
			this.gui.TextField(new Rect((!(watched.playerInfo.clanTag == string.Empty)) ? (300f + num) : 300f, 42f, 200f, 25f), watched.playerInfo.Nick, 22, watched.playerInfo.NickColor, TextAnchor.MiddleLeft, false, false);
			if (watched.Ammo.CurrentWeapon != null && watched.Ammo.CurrentWeapon.IsModable)
			{
				string[] array = (!watched.Ammo.CurrentWeapon.IsPrimary) ? watched.Ammo.state.secondaryState.Mods.Split(new char[]
				{
					' '
				}) : watched.Ammo.state.primaryState.Mods.Split(new char[]
				{
					' '
				});
				Dictionary<ModType, int> dictionary = new Dictionary<ModType, int>();
				foreach (string text in array)
				{
					if (!string.IsNullOrEmpty(text))
					{
						MasteringMod modById = ModsStorage.Instance().GetModById(int.Parse(text));
						dictionary.Add(modById.Type, modById.Id);
					}
				}
				int num2 = 0;
				foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
				{
					MasteringMod modById2 = ModsStorage.Instance().GetModById(keyValuePair.Value);
					Texture2D texture2D = this._icons[modById2.Type];
					int num3 = 490 + num2 * this.ModBorder.width;
					int num4 = 24;
					if (modById2.BigIcon != null)
					{
						GUI.DrawTexture(new Rect((float)num3, (float)num4, (float)this.ModBorder.width, (float)this.ModBorder.height), this.ModBorder);
						GUI.DrawTexture(new Rect((float)num3 + (float)(this.ModBorder.width - modById2.BigIcon.width) * 0.5f, (float)num4 + (float)(this.ModBorder.height - modById2.BigIcon.height) * 0.5f - 2f, (float)modById2.BigIcon.width, (float)modById2.BigIcon.height), modById2.BigIcon);
						if (texture2D != null)
						{
							GUI.DrawTexture(new Rect((float)(num3 + 3), (float)(num4 + 3), (float)texture2D.width, (float)texture2D.height), texture2D);
						}
						string text2 = (string)RarityColors.Colors[modById2.Rarity.ToString().ToLower()];
						Rect rect2 = new Rect((float)(num3 - 1), (float)(num4 + this.ModBorder.height - 4), (float)this.ModBorder.width, 18f);
						this.gui.TextLabel(rect2, modById2.ShortName, 12, (!modById2.IsCamo) ? "#FFFFFF" : text2, TextAnchor.UpperCenter, false);
					}
					num2++;
				}
			}
		}
		this.gui.EndGroup();
		this.gui.EndGroup();
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00026BF0 File Offset: 0x00024DF0
	public void TextFieldBacgr(Rect textRect, string str, int fontsize, string colortext, TextAnchor alignment = TextAnchor.UpperCenter, float BackgrAlpha = 1f)
	{
		this.gui.color = Colors.alpha(this.gui.color, BackgrAlpha * 0.4f);
		this.gui.PictureSized(new Vector2((textRect.xMax + textRect.xMin - this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize)) / 2f, textRect.yMin + 2f), this.gui.black, new Vector2(this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize) + 2f, this.gui.CalcHeight(str, textRect.width, this.gui.fontDNC57, fontsize) - 5f));
		this.gui.color = Colors.alpha(this.gui.color, BackgrAlpha);
		this.gui.TextLabel(textRect, str, fontsize, colortext, alignment, true);
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00026CF0 File Offset: 0x00024EF0
	private void CompositeText(ref Rect rect, string str, int fontsize, string colr, TextAnchor alignment = TextAnchor.UpperCenter, float BackgrAlpha = 2f)
	{
		if (BackgrAlpha > 1f)
		{
			BackgrAlpha = base.visibility;
		}
		float width = this.gui.CalcWidth(str, this.gui.fontDNC57, fontsize);
		rect.Set(rect.xMin, rect.yMin, width, rect.height);
		this.TextFieldBacgr(rect, str, fontsize, colr, alignment, BackgrAlpha);
		rect.Set(rect.xMax + 2f, rect.y, rect.width, rect.height);
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x0600058E RID: 1422 RVA: 0x00026D7C File Offset: 0x00024F7C
	public override int Width
	{
		get
		{
			return Screen.width;
		}
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x0600058F RID: 1423 RVA: 0x00026D84 File Offset: 0x00024F84
	public override int Height
	{
		get
		{
			return Screen.height;
		}
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00026D8C File Offset: 0x00024F8C
	public override void MainInitialize()
	{
		BannerGUI.I = this;
		this.isRendering = true;
		base.MainInitialize();
		this.helpersGUI = this.gui.GetComponent<HelpersGUI>();
		this.carrierGUI = this.gui.GetComponent<CarrierGUI>();
		this._icons = new Dictionary<ModType, Texture2D>
		{
			{
				ModType.silencer,
				this.IconMuzzle
			},
			{
				ModType.optic,
				this.IconOptic
			},
			{
				ModType.tactical,
				this.IconTactical
			},
			{
				ModType.camo,
				null
			},
			{
				ModType.ammo,
				this.IconAmmo
			}
		};
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00026E18 File Offset: 0x00025018
	public override void OnConnected()
	{
		this.NotEnoughWarriorExp.text = Language.NotEnoughWarriorExp.ToUpper();
		this.NotEnoughWarriorTasks.text = Language.NotEnoughWarriorTasks.ToUpper();
		this.TC_indexShowPoint = -1;
		this.ClearOnExpMatch();
		base.OnConnected();
		this.Clear();
		if (Main.IsTacticalConquest)
		{
			this.tacticalPoints = (Peer.ClientGame as ClientTacticalConquestGame).TacticalPoints;
		}
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x00026E88 File Offset: 0x00025088
	public override void OnDisconnect()
	{
		this.ClearOnExpMatch();
		base.OnDisconnect();
		this.Clear();
		this.onceShowContract = true;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00026EA4 File Offset: 0x000250A4
	private void ClearOnExpMatch()
	{
		if (Main.UserInfo.contractsInfo.CurrentEasy.task_type == ContractType.ExpMatch && Main.UserInfo.contractsInfo.CurrentEasy.task_counter > Main.UserInfo.contractsInfo.CurrentEasyCount)
		{
			Main.UserInfo.contractsInfo.CurrentEasyCount = 0;
		}
		if (Main.UserInfo.contractsInfo.CurrentNormal.task_type == ContractType.ExpMatch && Main.UserInfo.contractsInfo.CurrentNormal.task_counter > Main.UserInfo.contractsInfo.CurrentNormalCount)
		{
			Main.UserInfo.contractsInfo.CurrentNormalCount = 0;
		}
		if (Main.UserInfo.contractsInfo.CurrentHard.task_type == ContractType.ExpMatch && Main.UserInfo.contractsInfo.CurrentHard.task_counter > Main.UserInfo.contractsInfo.CurrentHardCount)
		{
			Main.UserInfo.contractsInfo.CurrentHardCount = 0;
		}
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00026FA8 File Offset: 0x000251A8
	public override void OnEnable()
	{
		this.red_banner_POSYKeys = this.red_banner_POSY.keys;
		this.red_banner_AlphaKeys = this.red_banner_Alpha.keys;
		this.red_glow_AlphaKeys = this.red_glow_Alpha.keys;
		this.red_glow_ScaleKeys = this.red_glow_Scale.keys;
		this.killstreaks_AlphaKeys = this.killstreaks_Alpha.keys;
		this.killstreaks_ScaleKeys = this.killstreaks_Scale.keys;
		this.yellow_glow_AlphaKeys = this.yellow_glow_Alpha.keys;
		this.yellow_glow_ScaleKeys = this.yellow_glow_Scale.keys;
		this.star_rotationKeys = this.star_rotation.keys;
		this.armstrBig_AlphaKeys = this.armstrBig_Alpha.keys;
		this.armstrSmall_AlphaKeys = this.armstrSmall_Alpha.keys;
		this.expText_AlphaKeys = this.expText_Alpha.keys;
		this.exp_AlphaKeys = this.exp_Alpha.keys;
		this.exp_ScaleKeys = this.exp_Scale.keys;
		this.achiv_AlphaKeys = this.achiv_Alpha.keys;
		this.achiv_glow_AlphaKeys = this.achiv_glow_Alpha.keys;
		this.buff_AlphaKeys = this.buff_Alpha.keys;
		this.contractsAlphaKeys = this.red_banner_POSYKeys;
		this.contractsWinAlphsKeys = this.contractWin_Alpha.keys;
		base.OnEnable();
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x000270FC File Offset: 0x000252FC
	public override void Clear()
	{
		this.bHintShow = true;
		this.bVipHintWhow = true;
		this.scale = 0f;
		this.redPos = Vector2.zero;
		this.easyContractProgress = 0;
		this.normalContractProgress = 0;
		this.hardContractProgress = 0;
		this.expGNAME = new GraphicValue();
		this.currentExp = new int[2];
		this.ongoingExp = new List<int[]>();
		this.bannerQueue.Clear();
		this.achivGNAME = new GraphicValue();
		this.currentAchiv = null;
		this.ongoingAchiv = new List<AchivData>();
		this.contractGNAME_easy = new GraphicValue();
		this.currentContract_easy = new ContractGUIState(Main.UserInfo.contractsInfo.CurrentEasyIndex, Main.UserInfo.contractsInfo.CurrentEasyCount, Main.UserInfo.contractsInfo.CurrentEasy.task_counter, ContractTaskType.EasyTask, Main.UserInfo.contractsInfo.CurrentEasy);
		this.ongoingContract_easy = new List<ContractGUIState>();
		this.contractGNAME_normal = new GraphicValue();
		this.currentContract_normal = new ContractGUIState(Main.UserInfo.contractsInfo.CurrentNormalIndex, Main.UserInfo.contractsInfo.CurrentNormalCount, Main.UserInfo.contractsInfo.CurrentNormal.task_counter, ContractTaskType.NormalTask, Main.UserInfo.contractsInfo.CurrentNormal);
		this.ongoingContract_normal = new List<ContractGUIState>();
		this.contractGNAME_hard = new GraphicValue();
		this.currentContract_hard = new ContractGUIState(Main.UserInfo.contractsInfo.CurrentHardIndex, Main.UserInfo.contractsInfo.CurrentHardCount, Main.UserInfo.contractsInfo.CurrentHard.task_counter, ContractTaskType.HardTask, Main.UserInfo.contractsInfo.CurrentHard);
		this.ongoingContract_hard = new List<ContractGUIState>();
		this.killGNAME = new GraphicValue();
		this.currentKill = null;
		this.ongoingKill = new List<KillGUIState>();
		this.armstreaks[0].window = new Alpha();
		this.armstreaks[1].window = new Alpha();
		this.delta = 0f;
		this.fireBuff = new Alpha();
		this.immortalBuff = new Alpha();
		this.health_boostBuff = new Alpha();
		this.is_nightBuff = new Alpha();
		this.VIPBuff = new Alpha();
		this.defenderBuff = new Alpha();
		this.brokenLegBuff = new Alpha();
		this.brokenHandBuff = new Alpha();
		this.bleedBuff = new Alpha();
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.notEnoughWarrior = false;
		this.notEnoughWarriorAlpha = new Alpha();
		this.notEnoughWarriorShowTime = 0f;
		BannerGUI.killCounter = 0;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000273A0 File Offset: 0x000255A0
	public override void Register()
	{
		EventFactory.Register("Armstreak", this);
		EventFactory.Register("ArmstreakUsed", this);
		EventFactory.Register("Wtask", this);
		EventFactory.Register("WtaskGained", this);
		EventFactory.Register("WtaskFailed", this);
		EventFactory.Register("Achievement", this);
		EventFactory.Register("AchievementGained", this);
		EventFactory.Register("AchievementFailed", this);
		EventFactory.Register("Card", this);
		EventFactory.Register("CardGained", this);
		EventFactory.Register("LevelUp", this);
		EventFactory.Register("GainMp", this);
		EventFactory.Register("KillStreak", this);
		EventFactory.Register("Exp", this);
		EventFactory.Register("DeadBanner", this);
		EventFactory.Register("KillBanner", this);
		EventFactory.Register("SprintShow", this);
		EventFactory.Register("SprintHide", this);
		EventFactory.Register("ClearKillBanner", this);
		EventFactory.Register("ContractGained", this);
		EventFactory.Register("ShowPlacementProgress", this);
		EventFactory.Register("HidePlacementProgress", this);
		EventFactory.Register("ShowBeaconUser", this);
		EventFactory.Register("HideBeaconUser", this);
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x000274B8 File Offset: 0x000256B8
	public override void GameGUI()
	{
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		if (Peer.ClientGame.LocalPlayer.DBRunning || this.KrutilkaAlpha.visibility > 0.05f)
		{
			if (Peer.ClientGame.LocalPlayer.DBRunning && !this.KrutilkaAlpha.Showing)
			{
				this.KrutilkaAlpha.Show(0.5f, 0f);
			}
			if (!Peer.ClientGame.LocalPlayer.DBRunning && !this.KrutilkaAlpha.Hiding)
			{
				this.KrutilkaAlpha.Hide(0.5f, 0f);
			}
			this.gui.color = Colors.alpha(this.gui.color, this.KrutilkaAlpha.visibility);
			float angle = 180f * Time.realtimeSinceStartup * 1.5f;
			this.gui.RotateGUI(angle, new Vector2((float)Screen.width - (float)this.gui.settings_window[9].width * 0.5f - 15f, (float)Screen.height - (float)this.gui.settings_window[9].height * 0.5f - 15f));
			this.gui.Picture(new Vector2((float)(Screen.width - this.gui.settings_window[9].width + 1 - 15), (float)(Screen.height - this.gui.settings_window[9].height + 1 - 15)), this.gui.settings_window[9]);
			this.gui.RotateGUI(0f, Vector2.zero);
			this.gui.color = Colors.alpha(this.gui.color, base.visibility);
		}
		if (this.onceShowContract && SingletoneForm<Loader>.Instance.IsGameLoadedAndClicked && !Main.UserInfo.settings.graphics.HideInterface)
		{
			this.ContractShowAtStart(new object[]
			{
				ContractTaskType.EasyTask,
				Main.UserInfo.contractsInfo.easyCounter
			});
			this.ContractShowAtStart(new object[]
			{
				ContractTaskType.NormalTask,
				Main.UserInfo.contractsInfo.normalCounter
			});
			this.ContractShowAtStart(new object[]
			{
				ContractTaskType.HardTask,
				Main.UserInfo.contractsInfo.hardCounter
			});
			Audio.Play(this.contract_progress);
			this.onceShowContract = false;
		}
		if (!Peer.HardcoreMode && !Main.UserInfo.settings.graphics.HideInterface)
		{
			if (!this.killGNAME.ExistTime())
			{
				if (this.ongoingKill.Count != 0)
				{
					this.currentKill = this.ongoingKill[0];
					this.ongoingKill.RemoveAt(0);
					if (this.currentKill.kill)
					{
						Vector2[] v = new Vector2[]
						{
							new Vector2(0f, 0f),
							new Vector2(0.5f, 0.5f),
							new Vector2(float.MaxValue, 0.5f)
						};
						this.killGNAME.Init(v);
					}
					else if (this.ongoingKill.Count != 0)
					{
						this.killGNAME.InitFastTimer(this.red_banner_POSYKeys[this.red_banner_POSYKeys.Length - 1].time * 0.625f, this.red_banner_POSYKeys[this.red_banner_POSYKeys.Length - 1].time);
					}
					else
					{
						this.killGNAME.InitTimer(this.red_banner_POSYKeys[this.red_banner_POSYKeys.Length - 1].time);
					}
				}
			}
			else
			{
				this.redPos = new Vector2(0f, (float)(this.killer_bar.height - 10) - this.red_banner_POSY.Evaluate(this.killGNAME.Get()) * (float)this.killer_bar.height);
				this.gui.color = new Color(1f, 1f, 1f, this.achiv_Alpha.Evaluate(this.killGNAME.Get()));
				this.gui.BeginGroup(new Rect(0f, this.redPos.y, (float)Screen.width, (float)Screen.height));
				if (!this.currentKill.kill)
				{
					float num = this.gui.CalcWidth(this.currentKill.clanTag, this.gui.fontDNC57, 22);
					float num2 = ((float)this.killer_bar.width - this.gui.CalcWidth(this.currentKill.clanTag, this.gui.fontDNC57, 22) - this.gui.CalcWidth(this.currentKill.playerName, this.gui.fontDNC57, 22) - (float)this.gui.rank_icon[this.currentKill.playerRank].width) / 2f;
					this.gui.TextLabel(new Rect((float)(96 + (Screen.width - this.killer_bar.width) / 2), (float)(-16 + Screen.height - this.killer_bar.height), 200f, 25f), Language.YouKill, 20, "#ffffff", TextAnchor.MiddleLeft, true);
					this.gui.Picture(new Vector2((float)((Screen.width - this.killer_bar.width) / 2), (float)(Screen.height - this.killer_bar.height)), this.killer_bar);
					this.gui.BeginGroup(new Rect((float)((Screen.width - this.killer_bar.width) / 2), (float)(Screen.height - this.killer_bar.height), (float)this.killer_bar.width, (float)this.killer_bar.height));
					this.gui.Picture(new Vector2(num2, 27f), this.gui.rank_icon[this.currentKill.playerRank]);
					this.gui.TextField(new Rect(num2 + 32f, 36f, 100f, 25f), this.currentKill.clanTag, 22, "#d40000", TextAnchor.MiddleLeft, false, false);
					this.gui.TextField(new Rect((!(this.currentKill.clanTag == string.Empty)) ? (num2 + 32f + num) : (num2 + 32f), 36f, 200f, 25f), this.currentKill.playerName, 22, this.currentKill.playerNameColor, TextAnchor.MiddleLeft, false, false);
					this.gui.EndGroup();
				}
				else if (SpectactorGUI.I.fSpawnTimer - HtmlLayer.serverUtc > 0)
				{
					this.gui.TextLabel(new Rect((float)(316 + (Screen.width - this.killed_bar.width) / 2), (float)(-16 + Screen.height - this.killed_bar.height), 200f, 25f), Language.YouKilled, 20, "#ffffff", TextAnchor.MiddleLeft, true);
					Rect rect = new Rect((float)((Screen.width - this.killed_bar.width) / 2), (float)(Screen.height - this.killed_bar.height), (float)this.killed_bar.width, (float)this.killed_bar.height);
					this.gui.BeginGroup(rect);
					this.gui.Picture(new Vector2(0f, 0f), this.killed_bar);
					this.gui.Picture(new Vector2(23f, 22f), this.helpersGUI.weaponFrame);
					if (this.currentKill.killMethod < Weapons.lastWeapon)
					{
						if (this.currentKill.wtask)
						{
							this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.gui.weapon_wtask[(int)this.currentKill.killMethod].width) / 2), 22f), this.gui.weapon_wtask[(int)this.currentKill.killMethod]);
						}
						else
						{
							this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.gui.weapon_unlocked[(int)this.currentKill.killMethod].width) / 2), 22f), this.gui.weapon_unlocked[(int)this.currentKill.killMethod]);
						}
						if (BannerGUI.KillerEntity != null && BannerGUI.KillerEntity.Ammo.CurrentWeapon != null && BannerGUI.KillerEntity.Ammo.CurrentWeapon.IsModable)
						{
							string[] array = (!BannerGUI.KillerEntity.Ammo.CurrentWeapon.IsPrimary) ? BannerGUI.KillerEntity.Ammo.state.secondaryState.Mods.Split(new char[]
							{
								' '
							}) : BannerGUI.KillerEntity.Ammo.state.primaryState.Mods.Split(new char[]
							{
								' '
							});
							Dictionary<ModType, int> dictionary = new Dictionary<ModType, int>();
							foreach (string text in array)
							{
								if (!string.IsNullOrEmpty(text))
								{
									MasteringMod modById = ModsStorage.Instance().GetModById(int.Parse(text));
									dictionary.Add(modById.Type, modById.Id);
								}
							}
							int num3 = 0;
							foreach (KeyValuePair<ModType, int> keyValuePair in dictionary)
							{
								MasteringMod modById2 = ModsStorage.Instance().GetModById(keyValuePair.Value);
								Texture2D texture2D = this._icons[modById2.Type];
								int num4 = 490 + num3 * this.ModBorder.width;
								int num5 = 24;
								if (modById2.BigIcon != null)
								{
									GUI.DrawTexture(new Rect((float)num4, (float)num5, (float)this.ModBorder.width, (float)this.ModBorder.height), this.ModBorder);
									GUI.DrawTexture(new Rect((float)num4 + (float)(this.ModBorder.width - modById2.BigIcon.width) * 0.5f, (float)num5 + (float)(this.ModBorder.height - modById2.BigIcon.height) * 0.5f - 2f, (float)modById2.BigIcon.width, (float)modById2.BigIcon.height), modById2.BigIcon);
									if (texture2D != null)
									{
										GUI.DrawTexture(new Rect((float)(num4 + 3), (float)(num5 + 3), (float)texture2D.width, (float)texture2D.height), texture2D);
									}
									string text2 = (string)RarityColors.Colors[modById2.Rarity.ToString().ToLower()];
									Rect rect2 = new Rect((float)(num4 - 1), (float)(num5 + this.ModBorder.height - 4), (float)this.ModBorder.width, 18f);
									this.gui.TextLabel(rect2, modById2.ShortName, 12, (!modById2.IsCamo) ? "#FFFFFF" : text2, TextAnchor.UpperCenter, false);
								}
								num3++;
							}
						}
						else
						{
							this.gui.Picture(new Vector2((float)(this.helpersGUI.weaponFrame.width - this.weaponLevel.width / 2 - 4), (float)(this.helpersGUI.weaponFrame.height + 3)), this.weaponLevel);
							this.gui.TextLabel(new Rect(0f, 0f, (float)(this.helpersGUI.weaponFrame.width + 15), (float)(this.helpersGUI.weaponFrame.height + 20)), this.Info.getWeaponLevel(BannerGUI.KillerWeaponKillsCount).ToString(), 10, "#ff9314_Micra", TextAnchor.LowerRight, true);
						}
					}
					else
					{
						this.gui.Picture(new Vector2((float)(23 + (this.helpersGUI.weaponFrame.width - this.killMethods[(int)this.currentKill.killMethod].width) / 2), 22f), this.killMethods[(int)this.currentKill.killMethod]);
					}
					this.gui.TextLabel(new Rect(29f, 64f, 200f, 19f), this.currentKill.killMethodName, 14, "#ffffff", TextAnchor.MiddleLeft, true);
					float num6 = this.gui.CalcWidth(this.currentKill.clanTag, this.gui.fontDNC57, 22);
					this.gui.Picture(new Vector2(270f, 34f), this.gui.rank_icon[this.currentKill.playerRank]);
					this.gui.TextField(new Rect(300f, 42f, 200f, 25f), this.currentKill.clanTag, 22, "#d40000", TextAnchor.MiddleLeft, false, false);
					this.gui.TextField(new Rect((!(this.currentKill.clanTag == string.Empty)) ? (300f + num6) : 300f, 42f, 200f, 25f), this.currentKill.playerName, 22, this.currentKill.playerNameColor, TextAnchor.MiddleLeft, false, false);
					this.gui.EndGroup();
				}
				this.gui.EndGroup();
			}
			if (!this.achivGNAME.ExistTime())
			{
				if (this.ongoingAchiv.Count != 0)
				{
					this.currentAchiv = this.ongoingAchiv[0];
					this.ongoingAchiv.RemoveAt(0);
					Audio.Play(this.wtask_clip);
					this.achivGNAME.InitTimer(this.achiv_AlphaKeys[this.achiv_AlphaKeys.Length - 1].time);
				}
			}
			else if (this.currentAchiv.type == AchivDataType.wtask)
			{
				this.gui.color = new Color(1f, 1f, 1f, this.achiv_Alpha.Evaluate(this.achivGNAME.Get()));
				this.gui.BeginGroup(new Rect((float)(Screen.width - this.task_progress.width), (float)((Screen.height - this.gui.Height) / 2 + 230), (float)this.task_progress.width, (float)this.task_progress.height));
				this.gui.Picture(new Vector2(0f, 0f), this.task_progress);
				this.gui.TextLabel(new Rect(0f, 0f, (float)this.task_progress.width, 18f), Language.WTaskProgress, 10, "#fe7800_Micra", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(-6f, 14f, 200f, 40f), this.currentAchiv.text, 16, "#ffffff", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(25f, 16f, 200f, 40f), this.currentAchiv.left, 22, "#ffffff", TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(223f, 26f, 200f, 40f), this.currentAchiv.right, 17, "#ffffff", TextAnchor.UpperLeft, true);
				this.gui.EndGroup();
			}
			else if (this.currentAchiv.type == AchivDataType.achievement)
			{
				this.gui.color = new Color(1f, 1f, 1f, this.achiv_Alpha.Evaluate(this.achivGNAME.Get()));
				this.gui.BeginGroup(new Rect((float)(Screen.width - this.task_progress.width), (float)((Screen.height - this.gui.Height) / 2 + 230), (float)this.task_progress.width, (float)this.task_progress.height));
				this.gui.Picture(new Vector2(0f, 0f), this.task_progress);
				this.gui.TextLabel(new Rect(0f, 0f, (float)this.task_progress.width, 18f), Language.ProgressTowards, 10, "#62aeea_Micra", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(-12f, 14f, 200f, 40f), this.currentAchiv.text, 16, "#ffffff", TextAnchor.MiddleCenter, true);
				this.gui.TextLabel(new Rect(25f, 16f, 200f, 40f), this.currentAchiv.left, 22, "#ffffff", TextAnchor.UpperRight, true);
				this.gui.TextLabel(new Rect(223f, 26f, 200f, 40f), this.currentAchiv.right, 17, "#ffffff", TextAnchor.UpperLeft, true);
				this.gui.EndGroup();
			}
		}
		if (Main.IsAlive)
		{
			float percentForNextLevel = Main.UserInfo.PercentForNextLevel;
			float percentForNextMpLevel = Main.UserInfo.PercentForNextMpLevel;
			float num7 = this.expGNAME.Get();
			if (num7 < 0f)
			{
				num7 = 0f;
			}
			this.gui.color = new Color(1f, 1f, 1f, 0.5f + num7);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - this.expBack.height * 2), (float)Screen.width, (float)this.expBack.height), this.expBack);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - this.expFore.height * 2), (float)Screen.width * percentForNextMpLevel, (float)this.expBack.height), MainGUI.Instance.orange);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - this.expBack.height), (float)Screen.width, (float)this.expBack.height), this.expBack);
			GUI.DrawTexture(new Rect(0f, (float)(Screen.height - this.expFore.height), (float)Screen.width * percentForNextLevel, (float)this.expBack.height), this.expFore);
			this.gui.color = new Color(1f, 1f, 1f, 0.5f + num7);
		}
		if (!this.expGNAME.ExistTime())
		{
			if (this.ongoingExp.Count != 0)
			{
				this.currentExp = this.ongoingExp[0];
				this.ongoingExp.RemoveAt(0);
				if (!Peer.HardcoreMode)
				{
					Audio.Play(this.exp_clip);
				}
				if (this.ongoingExp.Count != 0)
				{
					this.expGNAME.InitFastTimer(this.exp_AlphaKeys[this.exp_AlphaKeys.Length - 1].time * 0.5f, this.exp_AlphaKeys[this.exp_AlphaKeys.Length - 1].time);
				}
				else
				{
					this.expGNAME.InitTimer(this.exp_AlphaKeys[this.exp_AlphaKeys.Length - 1].time);
				}
			}
		}
		else if (!Main.UserInfo.settings.graphics.HideInterface)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.exp_Alpha.Evaluate(this.expGNAME.Get()));
			this.gui.BeginGroup(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height));
			this.scale = this.exp_Scale.Evaluate(this.expGNAME.Get());
			if (this.currentExp[0] >= 0)
			{
				if (Peer.HardcoreMode)
				{
					this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), 30f), this.blue_exp, new Vector2(this.scale / 1.5f, this.scale / 1.5f));
				}
				else
				{
					this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2 - 85)), this.blue_exp, new Vector2(this.scale, this.scale));
				}
			}
			else if (Peer.HardcoreMode)
			{
				this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), 30f), this.red_exp, new Vector2(this.scale / 1.5f, this.scale / 1.5f));
			}
			else
			{
				this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height / 2 - 85)), this.red_exp, new Vector2(this.scale, this.scale));
			}
			if (Peer.HardcoreMode)
			{
				string content = (int)((float)this.currentExp[0] * this.expText_Alpha.Evaluate(this.expGNAME.Get())) + ((this.currentExp[1] != 0) ? ("+" + (int)((float)this.currentExp[1] * this.expText_Alpha.Evaluate(this.expGNAME.Get()))) : string.Empty);
				this.gui.TextLabel(new Rect((float)(Screen.width / 2 - 90), 0f, 175f, 50f), content, 10, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			else
			{
				string content2 = (int)((float)this.currentExp[0] * this.expText_Alpha.Evaluate(this.expGNAME.Get())) + ((this.currentExp[1] != 0) ? ("+" + (int)((float)this.currentExp[1] * this.expText_Alpha.Evaluate(this.expGNAME.Get()))) : string.Empty);
				this.gui.TextLabel(new Rect((float)(Screen.width / 2 - 90), (float)(Screen.height / 2 - 85 - 25), 175f, 50f), content2, 20, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			}
			this.gui.EndGroup();
		}
		if (!Peer.HardcoreMode && !Main.UserInfo.settings.graphics.HideInterface && this.bannerQueue.Count > 0)
		{
			if (this.bannerQueue[0].Inited())
			{
				this.bannerQueue[0].OnGUI();
			}
			else
			{
				this.bannerQueue[0].Init((float)this.bannerQueue.Count);
			}
			if (this.bannerQueue[0].Complete())
			{
				this.bannerQueue.RemoveAt(0);
			}
		}
		if ((this.bMortar || this.bSonar) && Peer.ClientGame.LocalPlayer.IsAlive && !Main.UserInfo.settings.graphics.HideInterface)
		{
			Vector2 zero = Vector2.zero;
			if (Peer.HardcoreMode)
			{
				for (int j = 0; j < this.armstreaks.Length; j++)
				{
					if ((j == 0 && this.bMortar) || (j == 1 && this.bSonar))
					{
						zero.x += (float)this.armstreaks[j].tex_verysmall.width;
						if (Peer.ClientGame.LocalPlayer.Ammo.lastSupport == (ArmstreakEnum)j)
						{
							this.gui.color = new Color(1f, 1f, 1f, this.armstreaks[j].window.visibility * 0.5f);
						}
						else
						{
							this.gui.color = new Color(1f, 1f, 1f, this.armstreaks[j].window.visibility);
						}
						this.gui.Picture(new Vector2((float)(Screen.width / 2 - 150) - zero.x, 0f), this.armstreaks[j].tex_verysmall);
					}
				}
			}
			else
			{
				for (int k = 0; k < this.armstreaks.Length; k++)
				{
					if ((k == 0 && this.bMortar) || (k == 1 && this.bSonar))
					{
						zero.x += (float)this.armstreaks[k].tex_small.width;
						if (Peer.ClientGame.LocalPlayer.Ammo.lastSupport == ArmstreakEnum.none)
						{
							this.gui.color = new Color(1f, 1f, 1f, this.armstreaks[k].window.visibility);
						}
						else if (Peer.ClientGame.LocalPlayer.Ammo.lastSupport == (ArmstreakEnum)k)
						{
							this.gui.color = new Color(1f, 1f, 1f, this.armstreaks[k].window.visibility);
						}
						else
						{
							this.gui.color = new Color(1f, 1f, 1f, this.armstreaks[k].window.visibility * 0.5f);
						}
						this.gui.Picture(new Vector2((float)Screen.width - zero.x, (float)(Screen.height * 2 / 3)), this.armstreaks[k].tex_small);
						if (k == 0)
						{
							this.gui.TextLabel(new Rect((float)Screen.width - zero.x, (float)(Screen.height * 2 / 3 + this.armstreaks[k].tex_small.height - 20), (float)this.armstreaks[k].tex_small.width, 20f), '"' + Main.UserInfo.settings.binds.mortar.ToString().Replace("Alpha", string.Empty) + '"', 16, "#ffffff", TextAnchor.MiddleCenter, true);
						}
						if (k == 1)
						{
							this.gui.TextLabel(new Rect((float)Screen.width - zero.x, (float)(Screen.height * 2 / 3 + this.armstreaks[k].tex_small.height - 20), (float)this.armstreaks[k].tex_small.width, 20f), '"' + Main.UserInfo.settings.binds.support.ToString().Replace("Alpha", string.Empty) + '"', 16, "#ffffff", TextAnchor.MiddleCenter, true);
						}
					}
				}
			}
		}
		if (Main.IsAlive)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.sprint.visibility);
			this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 70)), this.gui.sprint, Vector2.one);
			float sprintDelay = Peer.ClientGame.LocalPlayer.Controller.state.sprintDelay;
			if (sprintDelay != 1f)
			{
				this.gui.ProgressDualTextured(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 30)), 200f, sprintDelay, this.placement_progressbar[0], this.placement_progressbar[1]);
			}
		}
		if (Peer.ClientGame.Placement)
		{
			float plaecementProgress = Peer.ClientGame.Placement.plaecementProgress;
			if (this.placementProgress.Visible)
			{
				this.gui.color = new Color(1f, 1f, 1f, this.placementProgress.visibility);
				if (Peer.ClientGame.LocalPlayer.IsTeam(Main.GameModeInfo.isBearPlacement))
				{
					this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 70)), this.placement_bigicon, Vector2.one);
					if (plaecementProgress != 1f)
					{
						this.gui.ProgressDualTextured(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 30)), 200f, plaecementProgress, this.placement_progressbar[0], this.placement_progressbar[1]);
					}
					if (plaecementProgress == 0f)
					{
						this.gui.color = new Color(1f, 1f, 1f, this.placementProgress.visibility * 0.4f);
						this.TextRect.Set((float)(Screen.width / 2 - 130), (float)(Screen.height - 120), 300f, 50f);
						this.CompositeText(ref this.TextRect, Language.Push, 16, "#ffffff", TextAnchor.UpperCenter, this.placementProgress.visibility);
						this.CompositeText(ref this.TextRect, Main.UserInfo.settings.binds.interaction.ToString().Replace("Alpha", string.Empty), 16, "#62aeea", TextAnchor.UpperCenter, this.placementProgress.visibility);
						this.CompositeText(ref this.TextRect, Language.BeginInstallingTheBeacon, 16, "#ffffff", TextAnchor.UpperCenter, this.placementProgress.visibility);
					}
				}
				else
				{
					this.gui.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(Screen.height - 70)), this.difuse_bigicon, Vector2.one);
					if (plaecementProgress != 0f)
					{
						this.gui.ProgressDualTextured(new Vector2((float)(Screen.width / 2 - 100), (float)(Screen.height - 30)), 200f, plaecementProgress, this.placement_progressbar[0], this.placement_progressbar[1]);
					}
					if (plaecementProgress == 1f)
					{
						this.gui.color = new Color(1f, 1f, 1f, this.placementProgress.visibility * 0.4f);
						this.TextRect.Set((float)(Screen.width / 2 - 140), (float)(Screen.height - 120), 300f, 50f);
						this.CompositeText(ref this.TextRect, Language.Push, 16, "#ffffff", TextAnchor.UpperCenter, 2f);
						this.CompositeText(ref this.TextRect, Main.UserInfo.settings.binds.interaction.ToString().Replace("Alpha", string.Empty), 16, "#62aeea", TextAnchor.UpperCenter, 2f);
						this.CompositeText(ref this.TextRect, Language.ClearanceToStartBeacon, 16, "#ffffff", TextAnchor.UpperCenter, 2f);
					}
				}
			}
		}
		if (Main.IsTacticalConquest && !Main.IsShowingMatchResult)
		{
			int inPoint = Peer.ClientGame.LocalPlayer.InPoint;
			if (this.tacticalPoints != null && this.tacticalPoints.Length > 0)
			{
				this.TC_indexShowPoint = inPoint;
				if (this.TC_indexShowPoint != -1)
				{
					this.TC_indexShowPoint = inPoint;
					this.TC_showingState = this.tacticalPoints[this.TC_indexShowPoint].pointState;
					this.TC_showingProgress = this.tacticalPoints[this.TC_indexShowPoint].captureProgress;
					if (Peer.ClientGame.LocalPlayer.IsBear)
					{
						this.TC_AllyInPoint = this.tacticalPoints[this.TC_indexShowPoint].bearIn;
						this.TC_EnemyInPoint = this.tacticalPoints[this.TC_indexShowPoint].usecIn;
					}
					else
					{
						this.TC_AllyInPoint = this.tacticalPoints[this.TC_indexShowPoint].usecIn;
						this.TC_EnemyInPoint = this.tacticalPoints[this.TC_indexShowPoint].bearIn;
					}
					if (!this.captureProgress.Showing)
					{
						this.captureProgress.Show(0.5f, 0f);
					}
					if (this.captureProgress.visibility > 0f || !this.tacticalPoints[this.TC_indexShowPoint].IsHomebase(Peer.ClientGame.LocalPlayer))
					{
						this.gui.color = Colors.alpha(this.gui.color, this.captureProgress.visibility);
						this.TacticalConquestGUI(this.TC_showingState, this.TC_showingProgress, this.TC_indexShowPoint, this.TC_AllyInPoint, this.TC_EnemyInPoint, this.tacticalPoints[this.TC_indexShowPoint].Name);
						this.gui.color = Colors.alpha(this.gui.color, base.visibility);
					}
					if (inPoint == -1 && !this.captureProgress.Hiding)
					{
						this.captureProgress.Hide(0.5f, 0f);
					}
				}
			}
		}
		this.delta = 0f;
		if (Peer.ClientGame.LocalPlayer.playerInfo.buffs != (Buffs)0)
		{
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.fire, this.fireBuff, this.fireBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.bruno_helix, this.brunoBuff, this.brunoBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.immortal, this.immortalBuff, this.immortalBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.health_boost, this.health_boostBuff, this.health_boostBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.is_night, this.is_nightBuff, this.is_nightBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.VIP, this.VIPBuff, this.VIPBuffTexture, true);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.defender, this.defenderBuff, this.defenderBuffTexture, !Peer.Info.TestVip);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.brokenLeg, this.brokenLegBuff, this.brokenLegTexture, false);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.brokenHand, this.brokenHandBuff, this.brokenHandTexture, false);
			this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.bleed, this.bleedBuff, this.bleedTexture, true);
			if (!Peer.ClientGame.LocalPlayer.IsBear)
			{
				this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.bearBaseHurt, this.bearBaseBuff, this.bearBaseTexture, true);
			}
			if (Peer.ClientGame.LocalPlayer.IsBear)
			{
				this.DrawBuff(Peer.ClientGame.LocalPlayer.playerInfo.buffs, Buffs.usecBaseHurt, this.usecBaseBuff, this.usecBaseTexture, true);
			}
		}
		if (!Peer.HardcoreMode && !Main.UserInfo.settings.graphics.HideInterface)
		{
			this.ShowContractProgressBanner(ref this.contractGNAME_easy, ref this.ongoingContract_easy, ref this.currentContract_easy, 80f);
			this.ShowContractProgressBanner(ref this.contractGNAME_normal, ref this.ongoingContract_normal, ref this.currentContract_normal, 130f);
			this.ShowContractProgressBanner(ref this.contractGNAME_hard, ref this.ongoingContract_hard, ref this.currentContract_hard, 180f);
		}
		if (!Main.IsDeadOrSpectactor && Main.IsRoundGoing && !Main.UserInfo.settings.graphics.HideInterface)
		{
			this.HintGameMode();
		}
		if (!Main.UserInfo.settings.graphics.HideInterface)
		{
			if (!this.notEnoughWarrior && Peer.ClientGame.LocalPlayer.IsAlive && (BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 4194304) || BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 8388608)))
			{
				this.notEnoughWarriorAlpha.Show(0.5f, 0f);
				this.notEnoughWarrior = true;
				this.notEnoughWarriorShowTime = Time.realtimeSinceStartup;
			}
			if (this.notEnoughWarriorAlpha.visibility > 0f)
			{
				float num8 = (float)Screen.height * 0.2f;
				this.gui.color = Colors.alpha(this.gui.color, this.notEnoughWarriorAlpha.visibility * 0.75f);
				if (BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 4194304) && Main.UserInfo.PlayerLevel > 9)
				{
					GUI.Label(new Rect((float)(Screen.width / 2 - 150), num8, 300f, 20f), this.NotEnoughWarriorExp, this.RestictedTextStyle);
				}
				if (BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 8388608) && Main.UserInfo.PlayerLevel > 9)
				{
					GUI.Label(new Rect((float)(Screen.width / 2 - 150), num8 + 20f, 300f, 20f), this.NotEnoughWarriorTasks, this.RestictedTextStyle);
				}
				this.gui.color = Colors.alpha(Color.white, base.visibility);
				if (this.notEnoughWarriorShowTime + 4f < Time.realtimeSinceStartup)
				{
					this.notEnoughWarriorAlpha.Hide(0.5f, 0f);
				}
			}
		}
		if (Peer.ClientGame.LocalPlayer.IsAlive && BannerGUI.drawWeaponLevelUp && !Main.UserInfo.settings.graphics.HideInterface)
		{
			BannerGUI.drawWeaponLevelUp = false;
			BannerGUI.DrawWeaponLevelUpBanner();
		}
		if ((this.drawMastering || this.masteringAlpha.visibility > 0f) && !Main.UserInfo.settings.graphics.HideInterface)
		{
			this.gui.color = new Color(1f, 1f, 1f, this.masteringAlpha.visibility);
			this.gui.Picture(new Vector2((float)(Screen.width - 165), this.yPos), this.mastering);
			this.yPos -= 2.5f;
			if (!this.masteringAlpha.Hiding && this.masteringAlpha.MaxVisible)
			{
				this.masteringAlpha.Hide(1f, 0f);
				this.drawMastering = false;
			}
		}
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00029DA8 File Offset: 0x00027FA8
	private void ContractsTimer()
	{
		if (!Main.UserInfo.settings.graphics.ShowingContractProgress)
		{
			return;
		}
		int seconds;
		if ((float)Main.UserInfo.contractsInfo.DeltaTime > Peer.ClientGame.ElapsedNextEventTime)
		{
			seconds = Main.UserInfo.contractsInfo.DeltaTime;
		}
		else
		{
			seconds = (int)Peer.ClientGame.ElapsedNextEventTime;
		}
		this.gui.color = Colors.alpha(this.gui.color, 1f);
		this.TextFieldBacgr(new Rect((float)(Screen.width - 50), (float)((Screen.height - this.gui.Height) / 2 + 60), 50f, 20f), this.gui.SecondsToStringHHHMMSS(seconds), 12, "#ffffff", TextAnchor.LowerCenter, 0.5f);
		this.gui.color = Colors.alpha(this.gui.color, base.visibility);
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x00029EA0 File Offset: 0x000280A0
	private void ShowContractProgressBanner(ref GraphicValue graphicVal, ref List<ContractGUIState> list, ref ContractGUIState tmpSample, float y = 100f)
	{
		if (tmpSample == null)
		{
			if (list.Count <= 0)
			{
				return;
			}
			tmpSample = list[0];
		}
		this.gui.color = new Color(1f, 1f, 1f, 0.6f);
		if (!graphicVal.ExistTime())
		{
			this.ContractPlashka(0f, y, tmpSample);
			if (list.Count != 0)
			{
				tmpSample = list[0];
				list.RemoveAt(0);
				if (tmpSample.completed)
				{
					graphicVal.InitTimer(this.contractsWinAlphsKeys[this.contractsWinAlphsKeys.Length - 1].time);
				}
				else
				{
					graphicVal.InitTimer(this.contractsAlphaKeys[this.contractsAlphaKeys.Length - 1].time);
				}
			}
		}
		else
		{
			this.gui.color = new Color(1f, 1f, 1f, 1f);
			float x;
			if (tmpSample.completed)
			{
				x = this.contractWin_Alpha.Evaluate(graphicVal.Get());
			}
			else
			{
				x = this.red_banner_POSY.Evaluate(graphicVal.Get());
			}
			this.ContractPlashka(x, y, tmpSample);
		}
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00029FF4 File Offset: 0x000281F4
	private void ContractPlashka(float x, float y, ContractGUIState tmpSample)
	{
		if (!Main.UserInfo.settings.graphics.ShowingContractProgress || tmpSample == null)
		{
			return;
		}
		y += (float)((Screen.height - this.gui.Height) / 2);
		x += (float)Screen.width - (float)(this.battleScreenContract.width - 39) * x - 65f;
		GUI.DrawTexture(new Rect(x, y, (float)this.battleScreenContract.width, (float)this.battleScreenContract.height), this.battleScreenContract, ScaleMode.ScaleToFit, true);
		if (tmpSample.type == ContractTaskType.EasyTask)
		{
			GUI.DrawTexture(new Rect(x + 25f, y + 3f, (float)this.carrierGUI.ContractStarsImgs[0].width, (float)this.carrierGUI.ContractStarsImgs[0].height), this.carrierGUI.ContractStarsImgs[0], ScaleMode.ScaleToFit, true);
		}
		else if (tmpSample.type == ContractTaskType.NormalTask)
		{
			GUI.DrawTexture(new Rect(x + 25f, y + 3f, (float)this.carrierGUI.ContractStarsImgs[1].width, (float)this.carrierGUI.ContractStarsImgs[1].height), this.carrierGUI.ContractStarsImgs[1], ScaleMode.ScaleToFit, true);
		}
		else if (tmpSample.type == ContractTaskType.HardTask)
		{
			GUI.DrawTexture(new Rect(x + 25f, y + 3f, (float)this.carrierGUI.ContractStarsImgs[2].width, (float)this.carrierGUI.ContractStarsImgs[2].height), this.carrierGUI.ContractStarsImgs[2], ScaleMode.ScaleToFit, true);
		}
		this.gui.TextLabel(new Rect(x + 10f, y + 3f, 20f, 20f), tmpSample.iContractNum.ToString(), 12, "#ffffff", TextAnchor.UpperCenter, true);
		if (tmpSample.currentProgress < tmpSample.MaxProgress)
		{
			this.gui.TextLabel(new Rect(x + 15f, y + 25f, 50f, 20f), tmpSample.currentProgress + "/" + tmpSample.MaxProgress, 12, "#ffffff", TextAnchor.UpperCenter, true);
			this.gui.TextLabel(new Rect(x + 63f, y + 5f, (float)(this.battleScreenContract.width - 73), 40f), tmpSample.description, 14, "#ffffff", TextAnchor.UpperCenter, true);
		}
		else
		{
			GUI.DrawTexture(new Rect(x + 38f, y + 23f, (float)this.carrierGUI.ContractImgs[4].width, (float)this.carrierGUI.ContractImgs[4].height), this.carrierGUI.ContractImgs[4], ScaleMode.ScaleToFit, true);
			Rect rect = new Rect(x + 70f, y + 13f, (float)(this.battleScreenContract.width - 73), 40f);
			this.gui.CompositeTextClean(ref rect, Language.CompletedSmall, 14, "#ffffff", TextAnchor.UpperCenter);
			rect.Set(x + rect.xMin + 75f, y + rect.yMin, rect.width, rect.height);
			this.gui.CompositeTextClean(ref rect, " + ", 14, "#ffffff", TextAnchor.UpperCenter);
			if (tmpSample.iRewardCR > 0)
			{
				this.gui.CompositeTextClean(ref rect, tmpSample.iRewardCR.ToString(), 14, "#ffffff", TextAnchor.UpperCenter);
				this.gui.Picture(new Vector2(rect.xMin, rect.yMin), this.gui.crIcon);
			}
			else if (tmpSample.iRewardGP > 0)
			{
				this.gui.CompositeTextClean(ref rect, tmpSample.iRewardGP.ToString(), 14, "#ffffff", TextAnchor.UpperCenter);
				this.gui.Picture(new Vector2(rect.xMin, rect.yMin), this.gui.gldIcon);
			}
			else if (tmpSample.iRewardSP > 0)
			{
				this.gui.CompositeTextClean(ref rect, tmpSample.iRewardSP.ToString(), 14, "#ffffff", TextAnchor.UpperCenter);
				this.gui.Picture(new Vector2(rect.xMin, rect.yMin), this.gui.spIcon_med);
			}
		}
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x0002A460 File Offset: 0x00028660
	public override void OnSpawn()
	{
		this.TC_indexShowPoint = -1;
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
		this.killGNAME = new GraphicValue();
		this.currentKill = null;
		this.ongoingKill.Clear();
		if (!Main.IsTargetDesignation)
		{
			BannerGUI.killCounter = 0;
		}
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0002A4C4 File Offset: 0x000286C4
	public override void OnDie()
	{
		Peer.ClientGame.LocalPlayer.InPoint = -1;
		this.TC_indexShowPoint = -1;
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0002A50C File Offset: 0x0002870C
	public override void OnRoundStart()
	{
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
		this.killGNAME = new GraphicValue();
		this.currentKill = null;
		this.ongoingKill.Clear();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0002A558 File Offset: 0x00028758
	public override void OnRoundEnd()
	{
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0002A57C File Offset: 0x0002877C
	public override void OnMatchStart()
	{
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
		this.ArmstreakUsed(ArmstreakEnum.mortar);
		this.ArmstreakUsed(ArmstreakEnum.sonar);
		this.killGNAME = new GraphicValue();
		this.currentKill = null;
		this.ongoingKill.Clear();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0002A5E0 File Offset: 0x000287E0
	public override void OnMatchEnd()
	{
		this.beaconUserAlpha = new Alpha();
		this.placementProgress = new Alpha();
		this.captureProgress = new Alpha();
		this.ArmstreakUsed(ArmstreakEnum.mortar);
		this.ArmstreakUsed(ArmstreakEnum.sonar);
	}

	// Token: 0x040004D0 RID: 1232
	private TacticalPoint[] tacticalPoints;

	// Token: 0x040004D1 RID: 1233
	public Texture2D red_banner;

	// Token: 0x040004D2 RID: 1234
	public Texture2D gray_banner;

	// Token: 0x040004D3 RID: 1235
	public Texture2D yellow_banner;

	// Token: 0x040004D4 RID: 1236
	public Texture2D red_glow;

	// Token: 0x040004D5 RID: 1237
	public Texture2D yellow_glow;

	// Token: 0x040004D6 RID: 1238
	public Texture2D red_exp;

	// Token: 0x040004D7 RID: 1239
	public Texture2D blue_exp;

	// Token: 0x040004D8 RID: 1240
	public Texture2D task_progress;

	// Token: 0x040004D9 RID: 1241
	public Texture2D wtask_complited;

	// Token: 0x040004DA RID: 1242
	public Texture2D wtask_star;

	// Token: 0x040004DB RID: 1243
	public Texture2D achiv_complited;

	// Token: 0x040004DC RID: 1244
	public Texture2D achiv_whitecr;

	// Token: 0x040004DD RID: 1245
	public Texture2D level_complited;

	// Token: 0x040004DE RID: 1246
	public Texture2D killed_bar;

	// Token: 0x040004DF RID: 1247
	public Texture2D killer_bar;

	// Token: 0x040004E0 RID: 1248
	public Texture2D[] killMethods;

	// Token: 0x040004E1 RID: 1249
	public Texture2D battleScreenContract;

	// Token: 0x040004E2 RID: 1250
	public AnimationCurve red_banner_POSY;

	// Token: 0x040004E3 RID: 1251
	public AnimationCurve red_banner_Alpha;

	// Token: 0x040004E4 RID: 1252
	public AnimationCurve red_glow_Alpha;

	// Token: 0x040004E5 RID: 1253
	public AnimationCurve red_glow_Scale;

	// Token: 0x040004E6 RID: 1254
	public AnimationCurve killstreaks_Alpha;

	// Token: 0x040004E7 RID: 1255
	public AnimationCurve killstreaks_Scale;

	// Token: 0x040004E8 RID: 1256
	public AnimationCurve yellow_glow_Alpha;

	// Token: 0x040004E9 RID: 1257
	public AnimationCurve yellow_glow_Scale;

	// Token: 0x040004EA RID: 1258
	public AnimationCurve star_rotation;

	// Token: 0x040004EB RID: 1259
	public AnimationCurve armstrBig_Alpha;

	// Token: 0x040004EC RID: 1260
	public AnimationCurve armstrSmall_Alpha;

	// Token: 0x040004ED RID: 1261
	public AnimationCurve expText_Alpha;

	// Token: 0x040004EE RID: 1262
	public AnimationCurve exp_Alpha;

	// Token: 0x040004EF RID: 1263
	public AnimationCurve exp_Scale;

	// Token: 0x040004F0 RID: 1264
	public AnimationCurve achiv_Alpha;

	// Token: 0x040004F1 RID: 1265
	public AnimationCurve achiv_glow_Alpha;

	// Token: 0x040004F2 RID: 1266
	public AnimationCurve buff_Alpha;

	// Token: 0x040004F3 RID: 1267
	public AnimationCurve contractWin_Alpha;

	// Token: 0x040004F4 RID: 1268
	public AudioClip exp_clip;

	// Token: 0x040004F5 RID: 1269
	public AudioClip achiv_clip;

	// Token: 0x040004F6 RID: 1270
	public AudioClip wtask_clip;

	// Token: 0x040004F7 RID: 1271
	public AudioClip levelup_clip;

	// Token: 0x040004F8 RID: 1272
	public AudioClip contract_progress;

	// Token: 0x040004F9 RID: 1273
	public AudioClip contract_complete;

	// Token: 0x040004FA RID: 1274
	public AudioClip weaponLevelUp;

	// Token: 0x040004FB RID: 1275
	public KillStreak[] streaks;

	// Token: 0x040004FC RID: 1276
	public ArmstrData[] armstreaks;

	// Token: 0x040004FD RID: 1277
	public Texture2D placement_bigicon;

	// Token: 0x040004FE RID: 1278
	public Texture2D difuse_bigicon;

	// Token: 0x040004FF RID: 1279
	public Texture2D[] placement_progressbar;

	// Token: 0x04000500 RID: 1280
	public Texture2D expBack;

	// Token: 0x04000501 RID: 1281
	public Texture2D expFore;

	// Token: 0x04000502 RID: 1282
	public Texture2D fireBuffTexture;

	// Token: 0x04000503 RID: 1283
	public Texture2D immortalBuffTexture;

	// Token: 0x04000504 RID: 1284
	public Texture2D health_boostBuffTexture;

	// Token: 0x04000505 RID: 1285
	public Texture2D placementBuffTexture;

	// Token: 0x04000506 RID: 1286
	public Texture2D is_nightBuffTexture;

	// Token: 0x04000507 RID: 1287
	public Texture2D VIPBuffTexture;

	// Token: 0x04000508 RID: 1288
	public Texture2D defenderBuffTexture;

	// Token: 0x04000509 RID: 1289
	public Texture2D bearBaseTexture;

	// Token: 0x0400050A RID: 1290
	public Texture2D usecBaseTexture;

	// Token: 0x0400050B RID: 1291
	public Texture2D brokenLegTexture;

	// Token: 0x0400050C RID: 1292
	public Texture2D brokenHandTexture;

	// Token: 0x0400050D RID: 1293
	public Texture2D bleedTexture;

	// Token: 0x0400050E RID: 1294
	public Texture2D brunoBuffTexture;

	// Token: 0x0400050F RID: 1295
	public Texture2D[] capturebar_blue;

	// Token: 0x04000510 RID: 1296
	public Texture2D[] capturebar_red;

	// Token: 0x04000511 RID: 1297
	public Texture2D usedslot;

	// Token: 0x04000512 RID: 1298
	public Texture2D freeslot;

	// Token: 0x04000513 RID: 1299
	public Texture2D neutralpoint;

	// Token: 0x04000514 RID: 1300
	public Texture2D enemycaptured;

	// Token: 0x04000515 RID: 1301
	public Texture2D youcaptured;

	// Token: 0x04000516 RID: 1302
	public Texture2D hardcoreMark;

	// Token: 0x04000517 RID: 1303
	public Texture2D mastering;

	// Token: 0x04000518 RID: 1304
	public Texture2D weaponLevel;

	// Token: 0x04000519 RID: 1305
	public Texture2D ModBorder;

	// Token: 0x0400051A RID: 1306
	public Texture2D IconMuzzle;

	// Token: 0x0400051B RID: 1307
	public Texture2D IconOptic;

	// Token: 0x0400051C RID: 1308
	public Texture2D IconTactical;

	// Token: 0x0400051D RID: 1309
	public Texture2D IconAmmo;

	// Token: 0x0400051E RID: 1310
	private Dictionary<ModType, Texture2D> _icons;

	// Token: 0x0400051F RID: 1311
	private HelpersGUI helpersGUI;

	// Token: 0x04000520 RID: 1312
	private CarrierGUI carrierGUI;

	// Token: 0x04000521 RID: 1313
	public bool bMortar;

	// Token: 0x04000522 RID: 1314
	public bool bSonar;

	// Token: 0x04000523 RID: 1315
	private int TC_indexShowPoint = -1;

	// Token: 0x04000524 RID: 1316
	private int TC_AllyInPoint;

	// Token: 0x04000525 RID: 1317
	private int TC_EnemyInPoint;

	// Token: 0x04000526 RID: 1318
	private float TC_showingProgress;

	// Token: 0x04000527 RID: 1319
	private TacticalPointState TC_showingState = TacticalPointState.neutral;

	// Token: 0x04000528 RID: 1320
	private ClientTacticalConquestGame TCgame;

	// Token: 0x04000529 RID: 1321
	public Keyframe[] red_banner_POSYKeys;

	// Token: 0x0400052A RID: 1322
	public Keyframe[] red_banner_AlphaKeys;

	// Token: 0x0400052B RID: 1323
	public Keyframe[] red_glow_AlphaKeys;

	// Token: 0x0400052C RID: 1324
	public Keyframe[] red_glow_ScaleKeys;

	// Token: 0x0400052D RID: 1325
	public Keyframe[] killstreaks_AlphaKeys;

	// Token: 0x0400052E RID: 1326
	public Keyframe[] killstreaks_ScaleKeys;

	// Token: 0x0400052F RID: 1327
	public Keyframe[] yellow_glow_AlphaKeys;

	// Token: 0x04000530 RID: 1328
	public Keyframe[] yellow_glow_ScaleKeys;

	// Token: 0x04000531 RID: 1329
	public Keyframe[] star_rotationKeys;

	// Token: 0x04000532 RID: 1330
	public Keyframe[] armstrBig_AlphaKeys;

	// Token: 0x04000533 RID: 1331
	public Keyframe[] armstrSmall_AlphaKeys;

	// Token: 0x04000534 RID: 1332
	public Keyframe[] expText_AlphaKeys;

	// Token: 0x04000535 RID: 1333
	public Keyframe[] exp_AlphaKeys;

	// Token: 0x04000536 RID: 1334
	public Keyframe[] exp_ScaleKeys;

	// Token: 0x04000537 RID: 1335
	public Keyframe[] achiv_AlphaKeys;

	// Token: 0x04000538 RID: 1336
	public Keyframe[] achiv_glow_AlphaKeys;

	// Token: 0x04000539 RID: 1337
	public Keyframe[] buff_AlphaKeys;

	// Token: 0x0400053A RID: 1338
	public Keyframe[] contractsAlphaKeys;

	// Token: 0x0400053B RID: 1339
	public Keyframe[] contractsWinAlphsKeys;

	// Token: 0x0400053C RID: 1340
	private float scale;

	// Token: 0x0400053D RID: 1341
	private bool bHintShow = true;

	// Token: 0x0400053E RID: 1342
	private bool bVipHintWhow = true;

	// Token: 0x0400053F RID: 1343
	private Alpha alphaHintShow = new Alpha();

	// Token: 0x04000540 RID: 1344
	private Vector2 redPos = Vector2.zero;

	// Token: 0x04000541 RID: 1345
	private Alpha sprint = new Alpha();

	// Token: 0x04000542 RID: 1346
	private GraphicValue expGNAME = new GraphicValue();

	// Token: 0x04000543 RID: 1347
	private int[] currentExp = new int[2];

	// Token: 0x04000544 RID: 1348
	private List<int[]> ongoingExp = new List<int[]>();

	// Token: 0x04000545 RID: 1349
	private GraphicValue achivGNAME = new GraphicValue();

	// Token: 0x04000546 RID: 1350
	private AchivData currentAchiv;

	// Token: 0x04000547 RID: 1351
	private List<AchivData> ongoingAchiv = new List<AchivData>();

	// Token: 0x04000548 RID: 1352
	private GraphicValue killGNAME = new GraphicValue();

	// Token: 0x04000549 RID: 1353
	private KillGUIState currentKill;

	// Token: 0x0400054A RID: 1354
	private List<KillGUIState> ongoingKill = new List<KillGUIState>();

	// Token: 0x0400054B RID: 1355
	private bool onceShowContract = true;

	// Token: 0x0400054C RID: 1356
	private GraphicValue contractGNAME_easy = new GraphicValue();

	// Token: 0x0400054D RID: 1357
	private ContractGUIState currentContract_easy;

	// Token: 0x0400054E RID: 1358
	private List<ContractGUIState> ongoingContract_easy = new List<ContractGUIState>();

	// Token: 0x0400054F RID: 1359
	private GraphicValue contractGNAME_normal = new GraphicValue();

	// Token: 0x04000550 RID: 1360
	private ContractGUIState currentContract_normal;

	// Token: 0x04000551 RID: 1361
	private List<ContractGUIState> ongoingContract_normal = new List<ContractGUIState>();

	// Token: 0x04000552 RID: 1362
	private GraphicValue contractGNAME_hard = new GraphicValue();

	// Token: 0x04000553 RID: 1363
	private ContractGUIState currentContract_hard;

	// Token: 0x04000554 RID: 1364
	private List<ContractGUIState> ongoingContract_hard = new List<ContractGUIState>();

	// Token: 0x04000555 RID: 1365
	private float delta;

	// Token: 0x04000556 RID: 1366
	private Alpha fireBuff = new Alpha();

	// Token: 0x04000557 RID: 1367
	private Alpha immortalBuff = new Alpha();

	// Token: 0x04000558 RID: 1368
	private Alpha health_boostBuff = new Alpha();

	// Token: 0x04000559 RID: 1369
	private Alpha is_nightBuff = new Alpha();

	// Token: 0x0400055A RID: 1370
	private Alpha VIPBuff = new Alpha();

	// Token: 0x0400055B RID: 1371
	private Alpha brunoBuff = new Alpha();

	// Token: 0x0400055C RID: 1372
	private Alpha defenderBuff = new Alpha();

	// Token: 0x0400055D RID: 1373
	private Alpha bearBaseBuff = new Alpha();

	// Token: 0x0400055E RID: 1374
	private Alpha usecBaseBuff = new Alpha();

	// Token: 0x0400055F RID: 1375
	private Alpha brokenLegBuff = new Alpha();

	// Token: 0x04000560 RID: 1376
	private Alpha brokenHandBuff = new Alpha();

	// Token: 0x04000561 RID: 1377
	private Alpha bleedBuff = new Alpha();

	// Token: 0x04000562 RID: 1378
	private Alpha beaconUserAlpha = new Alpha();

	// Token: 0x04000563 RID: 1379
	private Alpha placementProgress = new Alpha();

	// Token: 0x04000564 RID: 1380
	private Alpha captureProgress = new Alpha();

	// Token: 0x04000565 RID: 1381
	private Rect TextRect = new Rect(0f, 0f, 0f, 0f);

	// Token: 0x04000566 RID: 1382
	private Color tmpColor;

	// Token: 0x04000567 RID: 1383
	private Alpha KrutilkaAlpha = new Alpha();

	// Token: 0x04000568 RID: 1384
	private Alpha masteringAlpha = new Alpha();

	// Token: 0x04000569 RID: 1385
	private int easyContractProgress;

	// Token: 0x0400056A RID: 1386
	private int normalContractProgress;

	// Token: 0x0400056B RID: 1387
	private int hardContractProgress;

	// Token: 0x0400056C RID: 1388
	private float yPos = (float)(Screen.height - 125);

	// Token: 0x0400056D RID: 1389
	private bool drawMastering;

	// Token: 0x0400056E RID: 1390
	public static bool drawWeaponLevelUp;

	// Token: 0x0400056F RID: 1391
	public static BannerGUI I;

	// Token: 0x04000570 RID: 1392
	private List<BannerQueue> bannerQueue = new List<BannerQueue>();

	// Token: 0x04000571 RID: 1393
	public static int KillerWeaponKillsCount;

	// Token: 0x04000572 RID: 1394
	public static EntityNetPlayer KillerEntity;

	// Token: 0x04000573 RID: 1395
	public GUIStyle RestictedTextStyle = new GUIStyle();

	// Token: 0x04000574 RID: 1396
	private GUIContent NotEnoughWarriorExp = new GUIContent();

	// Token: 0x04000575 RID: 1397
	private GUIContent NotEnoughWarriorTasks = new GUIContent();

	// Token: 0x04000576 RID: 1398
	private Alpha notEnoughWarriorAlpha = new Alpha();

	// Token: 0x04000577 RID: 1399
	private float notEnoughWarriorShowTime;

	// Token: 0x04000578 RID: 1400
	private bool notEnoughWarrior;

	// Token: 0x04000579 RID: 1401
	public static byte killCounter;
}
