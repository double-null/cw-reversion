using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

// Token: 0x020001A7 RID: 423
[AddComponentMenu("Scripts/Game/BotNetPlayer")]
internal class BotNetPlayer : ClientNetPlayer
{
	// Token: 0x06000E03 RID: 3587 RVA: 0x000A38B8 File Offset: 0x000A1AB8
	protected override void Recieve(eNetworkStream stream)
	{
		if (stream.isFullUpdate)
		{
			base.FullUpdateRequestFinished();
		}
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x000A38CC File Offset: 0x000A1ACC
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void ChooseTeamFromServer(int type)
	{
		this.playerInfo.playerType = (PlayerType)type;
	}

	// Token: 0x06000E05 RID: 3589 RVA: 0x000A38DC File Offset: 0x000A1ADC
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void SpawnFromServer(int playerType, Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
	}

	// Token: 0x06000E06 RID: 3590 RVA: 0x000A38E0 File Offset: 0x000A1AE0
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void ChatFromServer(int playerID, string nick, int infoInt, string msg)
	{
	}

	// Token: 0x06000E07 RID: 3591 RVA: 0x000A38E4 File Offset: 0x000A1AE4
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void QuitFromServer()
	{
		base.GetComponent<PoolItem>().AutoDespawn(0.1f);
	}

	// Token: 0x06000E08 RID: 3592 RVA: 0x000A38F8 File Offset: 0x000A1AF8
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void PlayHit(int playerID, int targetID, float health, float armor, float power)
	{
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x000A38FC File Offset: 0x000A1AFC
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void KillFromServer(string boneToBeWild, Vector3 bonePower)
	{
		this.KillPlayer();
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x000A3904 File Offset: 0x000A1B04
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void RaiseWtaskFromServer(int index, int count, bool farmDetected)
	{
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x000A3908 File Offset: 0x000A1B08
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void RaiseAchievementFromServer(int index, int count, bool farmDetected)
	{
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x000A390C File Offset: 0x000A1B0C
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void ArmStreakFromServer(int armStreakFrom)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (armStreakFrom == 0)
		{
			this.playerInfo.hasMortar = true;
		}
		if (armStreakFrom == 1)
		{
			this.playerInfo.hasSonar = true;
		}
		if (armStreakFrom == 2)
		{
			this.ammo.state.grenadeCount++;
		}
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x000A396C File Offset: 0x000A1B6C
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void KillInfoFromServer(int killerId, int method, bool headShot, int killedId, string boneToBeWild, Vector3 boneForce, int weaponKills = 0)
	{
	}

	// Token: 0x06000E0E RID: 3598 RVA: 0x000A3970 File Offset: 0x000A1B70
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void KillStreakFromServer(int killStreakFrom)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E0F RID: 3599 RVA: 0x000A3980 File Offset: 0x000A1B80
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void ExpFromServer(float currentXP, float exp, float bonusExp)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E10 RID: 3600 RVA: 0x000A3990 File Offset: 0x000A1B90
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void LevelUpFromServer(float currentXP, int SP, int deltaSP)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E11 RID: 3601 RVA: 0x000A39A0 File Offset: 0x000A1BA0
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void AddAttackerFromServer(int id)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E12 RID: 3602 RVA: 0x000A39B0 File Offset: 0x000A1BB0
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void GameEventMessageFromServer(string nick, int infoInt, string msg)
	{
	}

	// Token: 0x06000E13 RID: 3603 RVA: 0x000A39B4 File Offset: 0x000A1BB4
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void UpdateInfo()
	{
	}

	// Token: 0x06000E14 RID: 3604 RVA: 0x000A39B8 File Offset: 0x000A1BB8
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void MatchStartFromServer()
	{
		this.playerInfo.hasMortar = false;
		this.playerInfo.hasSonar = false;
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x000A39E0 File Offset: 0x000A1BE0
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void MatchEndFromServer(byte[] bytes)
	{
		this.KillPlayer();
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x000A39F4 File Offset: 0x000A1BF4
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void RoundStartFromServer()
	{
		this.KillPlayer();
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E17 RID: 3607 RVA: 0x000A3A08 File Offset: 0x000A1C08
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void RoundEndFromServer()
	{
		this.KillPlayer();
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E18 RID: 3608 RVA: 0x000A3A1C File Offset: 0x000A1C1C
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void BearWinsFromServer(bool bearWins)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x000A3A2C File Offset: 0x000A1C2C
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void EnablePlacementFromServer(int placementIndex)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x000A3A3C File Offset: 0x000A1C3C
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void DisablePlacementFromServer()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x000A3A4C File Offset: 0x000A1C4C
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void RadioFromServer(int radio)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x000A3A5C File Offset: 0x000A1C5C
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void TacticalExplosionFromServer(Vector3 pos, int placementIndex)
	{
		if (!Main.IsGameLoaded && !Peer.ClientGame.Placement)
		{
			return;
		}
		this.KillPlayer();
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x000A3A84 File Offset: 0x000A1C84
	[Obfuscation(Exclude = true)]
	[RPC]
	public override void ExplosionFromServer(Vector3 pos)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x000A3A94 File Offset: 0x000A1C94
	[RPC]
	[Obfuscation(Exclude = true)]
	public override void MortarExplosionFromServer(string json)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x000A3AA4 File Offset: 0x000A1CA4
	public void Update()
	{
		this.PlayerInput.Update();
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x000A3AB4 File Offset: 0x000A1CB4
	public void LateUpdate()
	{
		foreach (EntityNetPlayer entityNetPlayer in Peer.ClientGame.AllPlayers)
		{
			if (entityNetPlayer.ID == this.ID)
			{
				this.playerInfo = entityNetPlayer.playerInfo;
				break;
			}
		}
		if (base.IsDeadOrSpectactor)
		{
			base.FinishedLoading();
			if (!this.autoBallanceTest && !this._isTeamChoosen)
			{
				base.StartCoroutine(this.ChooseTeam());
			}
			this.ChooseAmmunition();
			if (!Main.IsTacticalConquest)
			{
				base.Spawn();
			}
			else
			{
				TacticalPoint[] tacticalPoints = ((ClientTacticalConquestGame)Peer.ClientGame).TacticalPoints;
				if (tacticalPoints != null)
				{
					foreach (TacticalPoint tacticalPoint in from point in tacticalPoints
					where point.isEnemy(this) == 1
					select point)
					{
						base.Spawn(tacticalPoint.NumberOfPoint);
					}
				}
				else
				{
					base.Spawn(0);
				}
			}
			this._changeWayTimer = 0f;
		}
		if (CVars.g_botmove && Peer.ClientGame)
		{
			this.cmd.buttons = 0;
			this.cmd.euler.x = 270f;
		}
		if (Peer.ClientGame)
		{
			if (this.UsePlayerCmd)
			{
				this.cmd.buttons = this.PlayerInput.Current.Save();
			}
			this.cmd.buttons = this.PlayerInput.Current.Save();
		}
		int count = this.UC.Count;
		for (int i = 0; i < count; i++)
		{
			this.cmd.number = ++this.CmdNumber;
			this.UC.AdvanceTick();
			this.UC.Push(this.cmd);
		}
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x000A3D14 File Offset: 0x000A1F14
	private IEnumerator ChooseTeam()
	{
		base.ChooseTeam((PlayerType)CVars.g_botType);
		this._isTeamChoosen = true;
		yield return null;
		yield break;
	}

	// Token: 0x04000EEE RID: 3822
	private PlayerCmd cmd = new PlayerCmd();

	// Token: 0x04000EEF RID: 3823
	public bool Walking;

	// Token: 0x04000EF0 RID: 3824
	public bool Fireing;

	// Token: 0x04000EF1 RID: 3825
	public bool UsePlayerCmd = true;

	// Token: 0x04000EF2 RID: 3826
	private int _layerMask;

	// Token: 0x04000EF3 RID: 3827
	private float _changeWayTimer;

	// Token: 0x04000EF4 RID: 3828
	private float _changeWayDelay = 2.5f;

	// Token: 0x04000EF5 RID: 3829
	private int _wayButton;

	// Token: 0x04000EF6 RID: 3830
	[SerializeField]
	public bool autoBallanceTest;

	// Token: 0x04000EF7 RID: 3831
	private bool _isTeamChoosen;
}
