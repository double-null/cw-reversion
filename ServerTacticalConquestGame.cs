using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002F0 RID: 752
[AddComponentMenu("Scripts/Game/ServerTacticalConquestGame")]
internal class ServerTacticalConquestGame : BaseServerGame
{
	// Token: 0x06001531 RID: 5425 RVA: 0x000DFEEC File Offset: 0x000DE0EC
	public override void OnPoolDespawn()
	{
		this.needAutoTeamBalance = false;
		this.winCountFix = false;
		base.OnPoolDespawn();
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x000DFF04 File Offset: 0x000DE104
	protected bool AllBearDead()
	{
		bool result = true;
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			if (!serverNetPlayer.IsSpectactor)
			{
				if (serverNetPlayer.IsBear && serverNetPlayer.IsAlive)
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x000DFF90 File Offset: 0x000DE190
	protected bool AllUsecDead()
	{
		bool result = true;
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			if (!serverNetPlayer.IsSpectactor)
			{
				if (!serverNetPlayer.IsBear && serverNetPlayer.IsAlive)
				{
					result = false;
				}
			}
		}
		return result;
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x000E001C File Offset: 0x000DE21C
	private int BearCount()
	{
		int num = 0;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (!serverNetPlayer.IsSpectactor)
			{
				if (serverNetPlayer.IsBear)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x06001535 RID: 5429 RVA: 0x000E0074 File Offset: 0x000DE274
	private int UsecCount()
	{
		int num = 0;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (!serverNetPlayer.IsSpectactor)
			{
				if (!serverNetPlayer.IsBear)
				{
					num++;
				}
			}
		}
		return num;
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x06001536 RID: 5430 RVA: 0x000E00CC File Offset: 0x000DE2CC
	protected bool hasBears
	{
		get
		{
			return this.BearCount() != 0;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x06001537 RID: 5431 RVA: 0x000E00DC File Offset: 0x000DE2DC
	protected bool hasUsecs
	{
		get
		{
			return this.UsecCount() != 0;
		}
	}

	// Token: 0x17000330 RID: 816
	// (get) Token: 0x06001538 RID: 5432 RVA: 0x000E00EC File Offset: 0x000DE2EC
	public override bool BearWin
	{
		get
		{
			return this.bearWinCount > this.usecWinCount;
		}
	}

	// Token: 0x17000331 RID: 817
	// (get) Token: 0x06001539 RID: 5433 RVA: 0x000E00FC File Offset: 0x000DE2FC
	public override bool DeadHeat
	{
		get
		{
			return this.bearWinCount == this.usecWinCount;
		}
	}

	// Token: 0x17000332 RID: 818
	// (get) Token: 0x0600153A RID: 5434 RVA: 0x000E010C File Offset: 0x000DE30C
	public override bool IsTeamGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600153B RID: 5435 RVA: 0x000E0110 File Offset: 0x000DE310
	public override void Serialize(eNetworkStream stream)
	{
		base.Serialize(stream);
		int state = (int)this.state;
		stream.Serialize(ref state);
		float num = Mathf.Max(this.nextEventTime - Time.realtimeSinceStartup, 0f);
		stream.Serialize(ref num);
		stream.Serialize(ref this.bearWinCount);
		stream.Serialize(ref this.usecWinCount);
		short num2 = (short)this.tacticalPoints.Length;
		stream.Serialize(ref num2);
		try
		{
			foreach (TacticalPoint tacticalPoint in this.tacticalPoints)
			{
				tacticalPoint.Serialize(stream);
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x0600153C RID: 5436 RVA: 0x000E01DC File Offset: 0x000DE3DC
	protected override void UpdateMatch()
	{
		base.UpdateMatch();
		switch (this.state)
		{
		case MatchState.stoped:
			if (base.StopOnQuit())
			{
				return;
			}
			if (base.PlayerCount > 1)
			{
				this.MatchStart();
				this.RoundStart();
			}
			else if (base.PlayerCount == 1)
			{
				this.MatchStart();
				this.AloneStart();
			}
			break;
		case MatchState.alone:
			if (base.StopOnQuit())
			{
				return;
			}
			if (base.PlayerCount > 1)
			{
				this.MatchStart();
				this.RoundStart();
			}
			break;
		case MatchState.round_going:
			if (base.StopOnAllExit)
			{
				return;
			}
			if (base.OnNextTimeEvent)
			{
				this.RoundEnd();
				this.MatchEnd();
			}
			if (ServerLeagueSystem.Enabled)
			{
				if (this.usecWinCount >= Main.GameModeInfo.LeagueMatchNeededPoints || this.bearWinCount >= Main.GameModeInfo.LeagueMatchNeededPoints)
				{
					this.RoundEnd();
					this.MatchEnd();
				}
			}
			else if (this.usecWinCount >= Main.GameModeInfo.matchNeededPoints || this.bearWinCount >= Main.GameModeInfo.matchNeededPoints)
			{
				this.RoundEnd();
				this.MatchEnd();
			}
			break;
		case MatchState.match_result:
			if (base.OnNextTimeEvent)
			{
				this.state = MatchState.stoped;
			}
			break;
		}
	}

	// Token: 0x0600153D RID: 5437 RVA: 0x000E0348 File Offset: 0x000DE548
	public override void AloneStart()
	{
		base.AloneStart();
	}

	// Token: 0x0600153E RID: 5438 RVA: 0x000E0350 File Offset: 0x000DE550
	public override void MatchStart()
	{
		base.MatchStart();
		this.bearWinCount = 0;
		this.usecWinCount = 0;
		this.nextEventTime = Time.realtimeSinceStartup;
		this.ClearData();
	}

	// Token: 0x0600153F RID: 5439 RVA: 0x000E0378 File Offset: 0x000DE578
	public void ClearData()
	{
		foreach (TacticalPoint tacticalPoint in this.tacticalPoints)
		{
			tacticalPoint.Clear();
		}
		this.tempbearWincount = 0f;
		this.tempusecWincount = 0f;
	}

	// Token: 0x06001540 RID: 5440 RVA: 0x000E03C0 File Offset: 0x000DE5C0
	public override void RoundStart()
	{
		base.RoundStart();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchRoundTime;
	}

	// Token: 0x06001541 RID: 5441 RVA: 0x000E03E0 File Offset: 0x000DE5E0
	private void InitTacticalPoint()
	{
		this.tacticalPoints = (TacticalPoint[])UnityEngine.Object.FindSceneObjectsOfType(typeof(TacticalPoint));
		this.SortTP(this.tacticalPoints);
		foreach (TacticalPoint tacticalPoint in this.tacticalPoints)
		{
			tacticalPoint.SetAsServerEntity();
		}
	}

	// Token: 0x06001542 RID: 5442 RVA: 0x000E0438 File Offset: 0x000DE638
	private void SortTP(TacticalPoint[] ob)
	{
		for (int i = 0; i < ob.Length; i++)
		{
			for (int j = 0; j < ob.Length; j++)
			{
				if (ob[i].NumberOfPoint < ob[j].NumberOfPoint)
				{
					TacticalPoint tacticalPoint = ob[i];
					ob[i] = ob[j];
					ob[j] = tacticalPoint;
				}
			}
		}
	}

	// Token: 0x06001543 RID: 5443 RVA: 0x000E0498 File Offset: 0x000DE698
	public override void RoundEnd()
	{
		base.RoundEnd();
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001544 RID: 5444 RVA: 0x000E04AC File Offset: 0x000DE6AC
	public override Spawn getSpawnPoint(bool isBear, int ID)
	{
		Spawn result = null;
		foreach (TacticalPoint tacticalPoint4 in from tacticalPoint in this.tacticalPoints
		where tacticalPoint.NumberOfPoint == ID
		select tacticalPoint)
		{
			TacticalPoint tacticalPoint2 = tacticalPoint4;
			if (tacticalPoint4.SpawnAtBase)
			{
				using (IEnumerator<TacticalPoint> enumerator2 = this.tacticalPoints.Where((TacticalPoint basePoint) => basePoint.IsBasePoint).GetEnumerator())
				{
					if (enumerator2.MoveNext())
					{
						TacticalPoint tacticalPoint3 = enumerator2.Current;
						Debug.Log("SpawnAtBase!!!");
						tacticalPoint2 = tacticalPoint3;
					}
				}
			}
			bool flag = (tacticalPoint2.pointState == TacticalPointState.bear_captured || tacticalPoint2.pointState == TacticalPointState.bear_homeBase) && isBear;
			bool flag2 = (tacticalPoint2.pointState == TacticalPointState.usec_captured || tacticalPoint2.pointState == TacticalPointState.usec_homeBase) && !isBear;
			if ((flag || flag2) && tacticalPoint2.spawnPoints.Count > 0)
			{
				result = new Spawn(tacticalPoint2.spawnPoints[UnityEngine.Random.Range(0, tacticalPoint2.spawnPoints.Count - 1)]);
				break;
			}
		}
		return result;
	}

	// Token: 0x06001545 RID: 5445 RVA: 0x000E064C File Offset: 0x000DE84C
	public override void MatchEnd()
	{
		this.AddExpForMatchEnd();
		base.MatchEnd();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchResultTime;
		foreach (TacticalPoint tacticalPoint in this.tacticalPoints)
		{
			tacticalPoint.Clear();
		}
	}

	// Token: 0x06001546 RID: 5446 RVA: 0x000E06A0 File Offset: 0x000DE8A0
	public override void BearWins(bool bearWins)
	{
		if (bearWins)
		{
			this.bearWinCount++;
		}
		else
		{
			this.usecWinCount++;
		}
	}

	// Token: 0x06001547 RID: 5447 RVA: 0x000E06CC File Offset: 0x000DE8CC
	public override void OnLevelLoaded()
	{
		base.OnLevelLoaded();
		this.InitTacticalPoint();
	}

	// Token: 0x06001548 RID: 5448 RVA: 0x000E06DC File Offset: 0x000DE8DC
	public override Spawn getSpawnPoint(bool isBear)
	{
		return null;
	}

	// Token: 0x06001549 RID: 5449 RVA: 0x000E06E0 File Offset: 0x000DE8E0
	public override void MainInitialize()
	{
		base.MainInitialize();
		this.InitTacticalPoint();
	}

	// Token: 0x0600154A RID: 5450 RVA: 0x000E06F0 File Offset: 0x000DE8F0
	public override void OnLateUpdate()
	{
		float time = Time.time;
		base.OnLateUpdate();
		if (base.MatchState == MatchState.alone || base.MatchState == MatchState.match_result || this.iBearCount < 3 || this.iUsecCount < 3)
		{
			TacticalPoint.canUpdate = false;
		}
		else
		{
			TacticalPoint.canUpdate = true;
		}
		if (this._timeCapturePoints < time)
		{
			this._timeCapturePoints = time + this._deltaTimeCapturePoints;
			foreach (TacticalPoint tacticalPoint in this.tacticalPoints)
			{
				if (!tacticalPoint.IsBasePoint)
				{
					if (tacticalPoint.pointState == TacticalPointState.usec_captured)
					{
						this.usecWinCount++;
					}
					if (tacticalPoint.pointState == TacticalPointState.bear_captured)
					{
						this.bearWinCount++;
					}
				}
			}
		}
		if (!CVars.TCAutoBalance)
		{
			return;
		}
		int num = this.BearCount();
		int num2 = this.UsecCount();
		int value = num - num2;
		if (Mathf.Abs(value) <= 1)
		{
			return;
		}
		PlayerType fromTeam = (num <= num2) ? PlayerType.usec : PlayerType.bear;
		PlayerType type = (num >= num2) ? PlayerType.usec : PlayerType.bear;
		ServerNetPlayer nextPlayer = this.GetNextPlayer(fromTeam);
		if (nextPlayer == null)
		{
			return;
		}
		nextPlayer.ChooseTeamFromClient((int)type);
		nextPlayer.ChatFromClient(Language.PlayerAutoBallanced, 6);
		nextPlayer.isAutoBallanced = true;
	}

	// Token: 0x0600154B RID: 5451 RVA: 0x000E085C File Offset: 0x000DEA5C
	private ServerNetPlayer GetNextPlayer(PlayerType fromTeam)
	{
		ServerNetPlayer serverNetPlayer = null;
		int num = int.MaxValue;
		bool flag = fromTeam == PlayerType.bear;
		foreach (ServerNetPlayer serverNetPlayer2 in this.serverNetPlayers)
		{
			if (!serverNetPlayer2.isAutoBallanced && serverNetPlayer2.IsBear == flag)
			{
				int num2 = serverNetPlayer2.UserInfo.killCount + serverNetPlayer2.UserInfo.deathCount;
				if (num2 <= num)
				{
					serverNetPlayer = serverNetPlayer2;
					num = num2;
				}
			}
		}
		if (serverNetPlayer != null && !serverNetPlayer.IsAlive && !serverNetPlayer.IsSpectactor)
		{
			return serverNetPlayer;
		}
		return null;
	}

	// Token: 0x0600154C RID: 5452 RVA: 0x000E0944 File Offset: 0x000DEB44
	private void AddExpForMatchEnd()
	{
		int num;
		int num2;
		if (base.UsecWinCount < base.BearWinCount)
		{
			num = base.UsecWinCount / 6;
			num2 = base.BearWinCount / 2;
		}
		else if (base.UsecWinCount > base.BearWinCount)
		{
			num = base.UsecWinCount / 2;
			num2 = base.BearWinCount / 6;
		}
		else
		{
			num = base.UsecWinCount / 4;
			num2 = base.BearWinCount / 4;
		}
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			if (!serverNetPlayer.IsSpectactor)
			{
				if (serverNetPlayer.IsBear)
				{
					if (this.bearWinCount != 0 || this.usecWinCount != 0)
					{
						serverNetPlayer.Stats.IsWinner = ((this.bearWinCount < this.usecWinCount) ? -1 : 1);
					}
					serverNetPlayer.Exp((float)num2, 0f, true);
					serverNetPlayer.matchExpBonus = num2;
				}
				else
				{
					if (this.bearWinCount != 0 || this.usecWinCount != 0)
					{
						serverNetPlayer.Stats.IsWinner = ((this.bearWinCount > this.usecWinCount) ? -1 : 1);
					}
					serverNetPlayer.matchExpBonus = num;
					serverNetPlayer.Exp((float)num, 0f, true);
				}
			}
		}
	}

	// Token: 0x04001993 RID: 6547
	protected bool needAutoTeamBalance;

	// Token: 0x04001994 RID: 6548
	protected bool winCountFix;

	// Token: 0x04001995 RID: 6549
	private float _deltaTimeCapturePoints = 3f;

	// Token: 0x04001996 RID: 6550
	private float _timeCapturePoints;

	// Token: 0x04001997 RID: 6551
	protected float deltaCapture = 0.1f;

	// Token: 0x04001998 RID: 6552
	protected eTimer bearTimer = new eTimer();

	// Token: 0x04001999 RID: 6553
	protected eTimer usecTimer = new eTimer();

	// Token: 0x0400199A RID: 6554
	protected eTimer soundTimer = new eTimer();

	// Token: 0x0400199B RID: 6555
	protected bool bearMatchEndExp;

	// Token: 0x0400199C RID: 6556
	protected bool usecMatchEndExp;

	// Token: 0x0400199D RID: 6557
	private float tempbearWincount;

	// Token: 0x0400199E RID: 6558
	private float tempusecWincount;

	// Token: 0x0400199F RID: 6559
	private TacticalPoint[] tacticalPoints;

	// Token: 0x040019A0 RID: 6560
	private System.Random randomSpawn = new System.Random();
}
