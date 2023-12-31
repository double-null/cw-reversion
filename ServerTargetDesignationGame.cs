using System;
using System.Collections.Generic;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002F1 RID: 753
[AddComponentMenu("Scripts/Game/ServerTargetDesignationGame")]
internal class ServerTargetDesignationGame : BaseServerGame
{
	// Token: 0x0600154F RID: 5455 RVA: 0x000E0B10 File Offset: 0x000DED10
	public override void OnPoolDespawn()
	{
		this.needAutoTeamBalance = false;
		this.winCountFix = false;
		this.spawnRandomBear = new CircleRandom();
		this.spawnRanomdUsec = new CircleRandom();
		this.spawnsUsec.Clear();
		this.spawnsBear.Clear();
		base.OnPoolDespawn();
	}

	// Token: 0x06001550 RID: 5456 RVA: 0x000E0B60 File Offset: 0x000DED60
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

	// Token: 0x06001551 RID: 5457 RVA: 0x000E0BC4 File Offset: 0x000DEDC4
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

	// Token: 0x06001552 RID: 5458 RVA: 0x000E0C28 File Offset: 0x000DEE28
	private bool AwaitingBomb()
	{
		return this.placement && (this.placement.state == PlacementState.waiting_bomber || this.placement.state == PlacementState.deplacing);
	}

	// Token: 0x06001553 RID: 5459 RVA: 0x000E0C60 File Offset: 0x000DEE60
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

	// Token: 0x06001554 RID: 5460 RVA: 0x000E0CB8 File Offset: 0x000DEEB8
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

	// Token: 0x17000333 RID: 819
	// (get) Token: 0x06001555 RID: 5461 RVA: 0x000E0D10 File Offset: 0x000DEF10
	protected bool hasUsecs
	{
		get
		{
			return this.UsecCount() != 0;
		}
	}

	// Token: 0x17000334 RID: 820
	// (get) Token: 0x06001556 RID: 5462 RVA: 0x000E0D20 File Offset: 0x000DEF20
	protected bool hasBears
	{
		get
		{
			return this.BearCount() != 0;
		}
	}

	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06001557 RID: 5463 RVA: 0x000E0D30 File Offset: 0x000DEF30
	private bool noBombPlaced
	{
		get
		{
			return !this.AwaitingBomb();
		}
	}

	// Token: 0x17000336 RID: 822
	// (get) Token: 0x06001558 RID: 5464 RVA: 0x000E0D3C File Offset: 0x000DEF3C
	public override bool BearWin
	{
		get
		{
			return this.bearWinCount > this.usecWinCount;
		}
	}

	// Token: 0x17000337 RID: 823
	// (get) Token: 0x06001559 RID: 5465 RVA: 0x000E0D54 File Offset: 0x000DEF54
	public override bool NeedAdditionalRound
	{
		get
		{
			return this.bearWinCount == this.usecWinCount;
		}
	}

	// Token: 0x17000338 RID: 824
	// (get) Token: 0x0600155A RID: 5466 RVA: 0x000E0D6C File Offset: 0x000DEF6C
	public override bool IsTeamGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x17000339 RID: 825
	// (get) Token: 0x0600155B RID: 5467 RVA: 0x000E0D70 File Offset: 0x000DEF70
	public override bool IsRounedGame
	{
		get
		{
			return true;
		}
	}

	// Token: 0x0600155C RID: 5468 RVA: 0x000E0D74 File Offset: 0x000DEF74
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

	// Token: 0x0600155D RID: 5469 RVA: 0x000E0DD0 File Offset: 0x000DEFD0
	private float LevelX10PlusPoints(List<ServerNetPlayer> list)
	{
		float num = 0f;
		for (int i = 0; i < list.Count; i++)
		{
			num += (float)list[i].LevelX10PlusPoints;
		}
		return num;
	}

	// Token: 0x0600155E RID: 5470 RVA: 0x000E0E0C File Offset: 0x000DF00C
	private void AutoTeamBalance()
	{
		if (ServerLeagueSystem.Enabled)
		{
			return;
		}
		if (!this.needAutoTeamBalance)
		{
			return;
		}
		this.needAutoTeamBalance = false;
		List<List<ServerNetPlayer>> list = new List<List<ServerNetPlayer>>();
		List<ServerNetPlayer> list2 = new List<ServerNetPlayer>();
		List<ServerNetPlayer> list3 = new List<ServerNetPlayer>();
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (!serverNetPlayer.IsSpectactor)
			{
				if (serverNetPlayer.IsBear)
				{
					list2.Add(serverNetPlayer);
				}
				else
				{
					list3.Add(serverNetPlayer);
				}
			}
		}
		list2.Sort((ServerNetPlayer P1, ServerNetPlayer P2) => P1.LevelX10PlusPoints - P2.LevelX10PlusPoints);
		list3.Sort((ServerNetPlayer P1, ServerNetPlayer P2) => P1.LevelX10PlusPoints - P2.LevelX10PlusPoints);
		list.Add(list3);
		list.Add(list2);
		int num = Mathf.Abs(list2.Count - list3.Count);
		for (int j = 0; j < num; j++)
		{
			int count = list[0].Count;
			int count2 = list[1].Count;
			if (count - count2 >= 1)
			{
				list[1].Add(list[0][0]);
				list[0].RemoveAt(0);
			}
			else if (count2 - count >= 1)
			{
				list[0].Add(list[1][0]);
				list[1].RemoveAt(0);
			}
		}
		for (int k = 0; k < this.serverNetPlayers.Count; k++)
		{
			ServerNetPlayer serverNetPlayer2 = this.serverNetPlayers[k];
			if (!serverNetPlayer2.IsSpectactor)
			{
				for (int l = 0; l < list[0].Count; l++)
				{
					if (list[0][l].ID == serverNetPlayer2.ID && serverNetPlayer2.IsBear)
					{
						serverNetPlayer2.ChooseTeamFromClientBase(1, false);
					}
				}
				for (int m = 0; m < list[1].Count; m++)
				{
					if (list[1][m].ID == serverNetPlayer2.ID && !serverNetPlayer2.IsBear)
					{
						serverNetPlayer2.ChooseTeamFromClientBase(0, false);
					}
				}
			}
		}
	}

	// Token: 0x0600155F RID: 5471 RVA: 0x000E10A8 File Offset: 0x000DF2A8
	protected override void UpdateMatch()
	{
		base.UpdateMatch();
		switch (this.state)
		{
		case MatchState.stoped:
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
			else if (this.AllBearDead() && this.AllUsecDead() && base.PlayerCount == 1)
			{
				this.MatchStart();
				this.AloneStart();
			}
			break;
		case MatchState.round_pre_ended:
			if (Time.realtimeSinceStartup + 4f > this.nextEventTime)
			{
				this.RoundEnd();
			}
			break;
		case MatchState.round_ended:
			if (base.OnNextTimeEvent)
			{
				if (base.StopOnAllExit)
				{
					return;
				}
				if (this.usecWinCount >= Main.GameModeInfo.matchNeededKills || this.bearWinCount >= Main.GameModeInfo.matchNeededKills)
				{
					this.MatchEnd();
					return;
				}
				this.RoundStart();
			}
			break;
		case MatchState.round_going:
		{
			if (base.StopOnAllExit)
			{
				return;
			}
			bool flag = this.AllUsecDead() && this.hasUsecs;
			bool flag2 = this.AllBearDead() && this.hasBears && this.noBombPlaced;
			bool flag3 = this.AllUsecDead() && flag2;
			if (flag2 || flag3)
			{
				this.RoundPreEnd();
				this.BearWins(false);
				return;
			}
			if (flag)
			{
				this.RoundPreEnd();
				this.BearWins(true);
				return;
			}
			if (base.OnNextTimeEvent && this.noBombPlaced)
			{
				if (this.placement.IsWaitingOrDeplacing)
				{
					base.Radio(RadioEnum.beacon_failed_bomber, Main.GameModeInfo.isBearPlacement);
				}
				this.RoundPreEnd();
				this.BearWins(false);
			}
			break;
		}
		case MatchState.match_result:
			if (base.OnNextTimeEvent)
			{
				this.state = MatchState.stoped;
			}
			break;
		}
	}

	// Token: 0x06001560 RID: 5472 RVA: 0x000E1298 File Offset: 0x000DF498
	public override void AloneStart()
	{
		base.AloneStart();
		base.DespawnPlacement();
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].DisablePlacement();
		}
		for (int j = 0; j < this.serverNetPlayers.Count; j++)
		{
			this.serverNetPlayers[j].BaseServerSpawn();
		}
	}

	// Token: 0x06001561 RID: 5473 RVA: 0x000E130C File Offset: 0x000DF50C
	public override void MatchStart()
	{
		base.MatchStart();
		this.usecWinCount = 0;
		this.bearWinCount = 0;
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001562 RID: 5474 RVA: 0x000E1330 File Offset: 0x000DF530
	public override void RoundStart()
	{
		this.winCountFix = true;
		this.AutoTeamBalance();
		base.RoundStart();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchRoundTime;
		base.DespawnPlacement();
		this.FindPlacement();
		base.SpawnPlacement(this.placementIndex);
		ServerEntity component = SingletoneForm<PoolManager>.Instance["server_placement"].Spawn().GetComponent<ServerEntity>();
		component.transform.parent = base.transform;
		base.GetComponent<PoolItem>().Childs.Add(component.GetComponent<PoolItem>());
		component.state.type = EntityType.placement;
		component.state.PointType = this.placement.PointType;
		component.state.isBear = Main.GameModeInfo.isBearPlacement;
		component.transform.position = this.placement.transform.position;
		component.StartAsPlacement();
		base.AddEntity(component);
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].EnablePlacement(this.placement.PointType);
		}
		for (int j = 0; j < this.serverNetPlayers.Count; j++)
		{
			this.serverNetPlayers[j].BaseServerSpawn();
		}
	}

	// Token: 0x06001563 RID: 5475 RVA: 0x000E1484 File Offset: 0x000DF684
	public override void RoundPreEnd()
	{
		if (Mathf.Abs(this.UsecCount() - this.BearCount()) >= 2 && !this.needAutoTeamBalance && !ClientLeagueSystem.IsLeagueGame)
		{
			this.needAutoTeamBalance = true;
			Peer.ServerGame.EventMessage(string.Empty, ChatInfo.network_message, Language.AutoTeamBalance);
		}
		if (this.placement)
		{
			this.placement.Clear();
		}
		base.RoundPreEnd();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.roundPreparingTime + 4f;
	}

	// Token: 0x06001564 RID: 5476 RVA: 0x000E1518 File Offset: 0x000DF718
	public override void RoundEnd()
	{
		base.DespawnPlacement();
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].DisablePlacement();
		}
		base.RoundEnd();
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06001565 RID: 5477 RVA: 0x000E156C File Offset: 0x000DF76C
	public override void MatchEnd()
	{
		base.MatchEnd();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchResultTime;
	}

	// Token: 0x06001566 RID: 5478 RVA: 0x000E158C File Offset: 0x000DF78C
	public override void BearWins(bool bearWins)
	{
		if (this.winCountFix)
		{
			if (bearWins)
			{
				this.bearWinCount++;
			}
			else
			{
				this.usecWinCount++;
			}
			this.winCountFix = false;
		}
		base.BearWins(bearWins);
	}

	// Token: 0x06001567 RID: 5479 RVA: 0x000E15DC File Offset: 0x000DF7DC
	protected void FindPlacement()
	{
		List<int> list = new List<int>();
		List<float> list2 = new List<float>();
		for (int i = 0; i < 3; i++)
		{
			if (GameObject.Find("placement_" + i))
			{
				list.Add(i);
				list2.Add(UnityEngine.Random.value);
			}
		}
		this.placementIndex = list[0];
		float num = list2[0];
		for (int j = 1; j < list2.Count; j++)
		{
			if (list2[j] > num)
			{
				this.placementIndex = list[j];
				num = list2[j];
			}
		}
	}

	// Token: 0x06001568 RID: 5480 RVA: 0x000E1690 File Offset: 0x000DF890
	public override Spawn getSpawnPoint(bool isBear)
	{
		if (isBear)
		{
			Spawn result = this.spawnsBear[0];
			float num = 0f;
			for (int i = 0; i < this.spawnsBear.Count; i++)
			{
				float num2 = float.MaxValue;
				for (int j = 0; j < this.serverNetPlayers.Count; j++)
				{
					if (!this.serverNetPlayers[j].IsDeadOrSpectactor)
					{
						float magnitude = (this.serverNetPlayers[j].Position - this.spawnsBear[i].pos).magnitude;
						if (magnitude <= num2)
						{
							num2 = magnitude;
						}
					}
				}
				if (num2 > num)
				{
					result = this.spawnsBear[i];
					num = num2;
				}
			}
			return result;
		}
		Spawn result2 = this.spawnsUsec[0];
		float num3 = 0f;
		for (int k = 0; k < this.spawnsUsec.Count; k++)
		{
			float num4 = float.MaxValue;
			for (int l = 0; l < this.serverNetPlayers.Count; l++)
			{
				if (!this.serverNetPlayers[l].IsDeadOrSpectactor)
				{
					float magnitude2 = (this.serverNetPlayers[l].Position - this.spawnsUsec[k].pos).magnitude;
					if (magnitude2 <= num4)
					{
						num4 = magnitude2;
					}
				}
			}
			if (num4 > num3)
			{
				result2 = this.spawnsUsec[k];
				num3 = num4;
			}
		}
		return result2;
	}

	// Token: 0x06001569 RID: 5481 RVA: 0x000E1844 File Offset: 0x000DFA44
	public override void MainInitialize()
	{
		base.MainInitialize();
		SpawnPoint[] array = (SpawnPoint[])UnityEngine.Object.FindObjectsOfType(typeof(SpawnPoint));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isTeam && array[i].isBear)
			{
				this.spawnsBear.Add(new Spawn(array[i]));
			}
			if (array[i].isTeam && !array[i].isBear)
			{
				this.spawnsUsec.Add(new Spawn(array[i]));
			}
		}
		this.spawnRandomBear.InitNew(this.spawnsBear.Count);
		this.spawnRanomdUsec.InitNew(this.spawnsUsec.Count);
	}

	// Token: 0x040019A2 RID: 6562
	protected bool needAutoTeamBalance;

	// Token: 0x040019A3 RID: 6563
	protected bool winCountFix;

	// Token: 0x040019A4 RID: 6564
	protected CircleRandom spawnRandomBear = new CircleRandom();

	// Token: 0x040019A5 RID: 6565
	protected CircleRandom spawnRanomdUsec = new CircleRandom();

	// Token: 0x040019A6 RID: 6566
	protected List<Spawn> spawnsUsec = new List<Spawn>();

	// Token: 0x040019A7 RID: 6567
	protected List<Spawn> spawnsBear = new List<Spawn>();
}
