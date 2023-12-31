using System;
using System.Collections.Generic;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002F2 RID: 754
[AddComponentMenu("Scripts/Game/ServerTeamEliminationGame")]
internal class ServerTeamEliminationGame : BaseServerGame
{
	// Token: 0x0600156D RID: 5485 RVA: 0x000E1948 File Offset: 0x000DFB48
	public override void OnPoolDespawn()
	{
		this.spawnRandom = new CircleRandom();
		this.spawns = new List<Spawn>();
		base.OnPoolDespawn();
	}

	// Token: 0x0600156E RID: 5486 RVA: 0x000E1968 File Offset: 0x000DFB68
	protected bool AllBearDead()
	{
		bool result = true;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
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

	// Token: 0x0600156F RID: 5487 RVA: 0x000E19CC File Offset: 0x000DFBCC
	protected bool AllUsecDead()
	{
		bool result = true;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
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

	// Token: 0x06001570 RID: 5488 RVA: 0x000E1A30 File Offset: 0x000DFC30
	private bool AwaitingBomb()
	{
		return this.placement && (this.placement.state == PlacementState.waiting_bomber || this.placement.state == PlacementState.deplacing);
	}

	// Token: 0x06001571 RID: 5489 RVA: 0x000E1A68 File Offset: 0x000DFC68
	public int BearCount()
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

	// Token: 0x06001572 RID: 5490 RVA: 0x000E1AC0 File Offset: 0x000DFCC0
	public int UsecCount()
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

	// Token: 0x1700033A RID: 826
	// (get) Token: 0x06001573 RID: 5491 RVA: 0x000E1B18 File Offset: 0x000DFD18
	protected bool hasUsecs
	{
		get
		{
			return this.UsecCount() != 0;
		}
	}

	// Token: 0x1700033B RID: 827
	// (get) Token: 0x06001574 RID: 5492 RVA: 0x000E1B28 File Offset: 0x000DFD28
	protected bool hasBears
	{
		get
		{
			return this.BearCount() != 0;
		}
	}

	// Token: 0x1700033C RID: 828
	// (get) Token: 0x06001575 RID: 5493 RVA: 0x000E1B38 File Offset: 0x000DFD38
	private bool noBombPlaced
	{
		get
		{
			return !this.AwaitingBomb();
		}
	}

	// Token: 0x1700033D RID: 829
	// (get) Token: 0x06001576 RID: 5494 RVA: 0x000E1B44 File Offset: 0x000DFD44
	public override bool BearWin
	{
		get
		{
			return this.bearWinCount > this.usecWinCount;
		}
	}

	// Token: 0x1700033E RID: 830
	// (get) Token: 0x06001577 RID: 5495 RVA: 0x000E1B5C File Offset: 0x000DFD5C
	public override bool NeedAdditionalRound
	{
		get
		{
			return this.bearWinCount == this.usecWinCount;
		}
	}

	// Token: 0x1700033F RID: 831
	// (get) Token: 0x06001578 RID: 5496 RVA: 0x000E1B74 File Offset: 0x000DFD74
	public override bool IsTeamGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06001579 RID: 5497 RVA: 0x000E1B78 File Offset: 0x000DFD78
	public override void Serialize(eNetworkStream stream)
	{
		base.Serialize(stream);
		int state = (int)this.state;
		stream.Serialize(ref state);
		float num = Mathf.Max(this.nextEventTime - Time.realtimeSinceStartup, 0f);
		stream.Serialize(ref num);
		stream.Serialize(ref this.usecWinCount);
		stream.Serialize(ref this.bearWinCount);
	}

	// Token: 0x0600157A RID: 5498 RVA: 0x000E1BD4 File Offset: 0x000DFDD4
	protected override void UpdateMatch()
	{
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
				if (this.usecWinCount >= Main.GameModeInfo.LeagueMatchNeededTeamKills || this.bearWinCount >= Main.GameModeInfo.LeagueMatchNeededTeamKills)
				{
					this.RoundEnd();
					this.MatchEnd();
				}
			}
			else if (this.usecWinCount >= Main.GameModeInfo.matchNeededTeamKills || this.bearWinCount >= Main.GameModeInfo.matchNeededTeamKills)
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

	// Token: 0x0600157B RID: 5499 RVA: 0x000E1D38 File Offset: 0x000DFF38
	public override void MatchStart()
	{
		base.MatchStart();
		this.usecWinCount = 0;
		this.bearWinCount = 0;
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x0600157C RID: 5500 RVA: 0x000E1D5C File Offset: 0x000DFF5C
	public override void RoundStart()
	{
		base.RoundStart();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchRoundTime;
	}

	// Token: 0x0600157D RID: 5501 RVA: 0x000E1D7C File Offset: 0x000DFF7C
	public override void RoundEnd()
	{
		base.RoundEnd();
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x0600157E RID: 5502 RVA: 0x000E1D90 File Offset: 0x000DFF90
	public override void MatchEnd()
	{
		if (this.bearWinCount != 0 || this.usecWinCount != 0)
		{
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				if (this.serverNetPlayers[i].IsBear)
				{
					this.serverNetPlayers[i].Stats.IsWinner = ((this.bearWinCount < this.usecWinCount) ? -1 : 1);
				}
				else
				{
					this.serverNetPlayers[i].Stats.IsWinner = ((this.bearWinCount > this.usecWinCount) ? -1 : 1);
				}
			}
		}
		base.MatchEnd();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchResultTime;
	}

	// Token: 0x0600157F RID: 5503 RVA: 0x000E1E64 File Offset: 0x000E0064
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

	// Token: 0x06001580 RID: 5504 RVA: 0x000E1E90 File Offset: 0x000E0090
	private float getClosestPlayerDistance(Spawn S, bool hostile, bool isBear)
	{
		float num = float.MaxValue;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!this.serverNetPlayers[i].IsDeadOrSpectactor)
			{
				if (!hostile || this.serverNetPlayers[i].IsBear != isBear)
				{
					if (hostile || this.serverNetPlayers[i].IsBear == isBear)
					{
						float magnitude = (this.serverNetPlayers[i].Position - S.pos).magnitude;
						if (magnitude <= num)
						{
							num = magnitude;
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06001581 RID: 5505 RVA: 0x000E1F50 File Offset: 0x000E0150
	public override Spawn getSpawnPoint(bool IsBear = false)
	{
		List<Spawn> list = new List<Spawn>();
		for (int i = 0; i < this.spawns.Count; i++)
		{
			list.Add(this.spawns[i]);
		}
		list.Sort((Spawn S1, Spawn S2) => (int)this.getClosestPlayerDistance(S1, true, IsBear) - (int)this.getClosestPlayerDistance(S2, true, IsBear));
		for (int j = 0; j < list.Count / 2; j++)
		{
			list.RemoveAt(0);
		}
		list.Sort((Spawn S1, Spawn S2) => (int)this.getClosestPlayerDistance(S1, false, IsBear) - (int)this.getClosestPlayerDistance(S2, false, IsBear));
		if (!CVars.RandomSpawn)
		{
			return list[0];
		}
		int max = (list.Count <= 3) ? list.Count : 3;
		return list[UnityEngine.Random.Range(0, max)];
	}

	// Token: 0x06001582 RID: 5506 RVA: 0x000E2028 File Offset: 0x000E0228
	public override void OnLateUpdate()
	{
		base.OnLateUpdate();
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (base.Vip == null)
			{
				if (BIT.AND((int)this.serverNetPlayers[i].playerInfo.buffs, 65536))
				{
					this.serverNetPlayers[i].playerInfo.buffs -= 65536;
				}
			}
			else if (!this.serverNetPlayers[i].IsVip)
			{
				if (Peer.Info.TestVip)
				{
					if (BIT.AND((int)this.serverNetPlayers[i].playerInfo.buffs, 65536))
					{
						this.serverNetPlayers[i].playerInfo.buffs -= 65536;
					}
					if (base.Vip.IsTeam(this.serverNetPlayers[i].IsBear))
					{
						this.serverNetPlayers[i].playerInfo.buffs |= Buffs.defender;
					}
				}
				else
				{
					Vector3 vector = base.Vip.Position - this.serverNetPlayers[i].Position;
					if (BIT.AND((int)this.serverNetPlayers[i].playerInfo.buffs, 65536))
					{
						this.serverNetPlayers[i].playerInfo.buffs -= 65536;
					}
					if (vector.magnitude < 15f && base.Vip.IsTeam(this.serverNetPlayers[i].IsBear))
					{
						this.serverNetPlayers[i].playerInfo.buffs |= Buffs.defender;
					}
				}
			}
		}
		if (CVars.TEAutoBalance)
		{
			int num = this.BearCount();
			int num2 = this.UsecCount();
			int value = num - num2;
			if (Mathf.Abs(value) > 1)
			{
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
		}
	}

	// Token: 0x06001583 RID: 5507 RVA: 0x000E22B4 File Offset: 0x000E04B4
	private ServerNetPlayer GetNextPlayer(PlayerType fromTeam)
	{
		ServerNetPlayer serverNetPlayer = null;
		int num = int.MaxValue;
		bool flag = fromTeam == PlayerType.bear;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer2 = this.serverNetPlayers[i];
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

	// Token: 0x06001584 RID: 5508 RVA: 0x000E2378 File Offset: 0x000E0578
	public override void MainInitialize()
	{
		base.MainInitialize();
		SpawnPoint[] array = (SpawnPoint[])UnityEngine.Object.FindObjectsOfType(typeof(SpawnPoint));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isTeamEllimination)
			{
				this.spawns.Add(new Spawn(array[i]));
			}
		}
		this.spawnRandom.InitNew(this.spawns.Count);
	}

	// Token: 0x040019AA RID: 6570
	private CircleRandom spawnRandom = new CircleRandom();

	// Token: 0x040019AB RID: 6571
	private List<Spawn> spawns = new List<Spawn>();
}
