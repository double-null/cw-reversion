using System;
using System.Collections.Generic;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002EB RID: 747
[AddComponentMenu("Scripts/Game/ServerDeathMatchGame")]
internal class ServerDeathMatchGame : BaseServerGame
{
	// Token: 0x060014BD RID: 5309 RVA: 0x000DB444 File Offset: 0x000D9644
	public override void OnPoolDespawn()
	{
		this.spawnRandom = new CircleRandom();
		this.spawns = new List<Spawn>();
		base.OnPoolDespawn();
	}

	// Token: 0x17000324 RID: 804
	// (get) Token: 0x060014BE RID: 5310 RVA: 0x000DB464 File Offset: 0x000D9664
	public override bool IsTeamGame
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060014BF RID: 5311 RVA: 0x000DB468 File Offset: 0x000D9668
	public override void Serialize(eNetworkStream stream)
	{
		base.Serialize(stream);
		int state = (int)this.state;
		stream.Serialize(ref state);
		float num = Mathf.Max(this.nextEventTime - Time.realtimeSinceStartup, 0f);
		stream.Serialize(ref num);
	}

	// Token: 0x060014C0 RID: 5312 RVA: 0x000DB4AC File Offset: 0x000D96AC
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
			else
			{
				for (int i = 0; i < this.serverNetPlayers.Count; i++)
				{
					if (ServerLeagueSystem.Enabled)
					{
						if (this.serverNetPlayers[i].Points >= Main.GameModeInfo.LeagueMatchNeededPoints)
						{
							this.RoundEnd();
							this.MatchEnd();
						}
					}
					else if (this.serverNetPlayers[i].Points >= Main.GameModeInfo.matchNeededPoints)
					{
						this.RoundEnd();
						this.MatchEnd();
					}
				}
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

	// Token: 0x060014C1 RID: 5313 RVA: 0x000DB628 File Offset: 0x000D9828
	public override void ClanBuffLogic(ServerNetPlayer reciever, ServerNetPlayer giver)
	{
		if (BIT.AND(reciever.ClanBuffTeammate, 1024))
		{
			reciever.playerInfo.buffs |= Buffs.unblicable;
		}
	}

	// Token: 0x060014C2 RID: 5314 RVA: 0x000DB664 File Offset: 0x000D9864
	public override void MatchStart()
	{
		base.MatchStart();
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060014C3 RID: 5315 RVA: 0x000DB678 File Offset: 0x000D9878
	public override void RoundStart()
	{
		base.RoundStart();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchRoundTime;
	}

	// Token: 0x060014C4 RID: 5316 RVA: 0x000DB698 File Offset: 0x000D9898
	public override void RoundEnd()
	{
		base.RoundEnd();
		this.nextEventTime = Time.realtimeSinceStartup;
	}

	// Token: 0x060014C5 RID: 5317 RVA: 0x000DB6AC File Offset: 0x000D98AC
	public override void MatchEnd()
	{
		base.MatchEnd();
		this.nextEventTime = Time.realtimeSinceStartup + Main.GameModeInfo.matchResultTime;
	}

	// Token: 0x060014C6 RID: 5318 RVA: 0x000DB6CC File Offset: 0x000D98CC
	public override Spawn getSpawnPoint(bool IsBear = false)
	{
		List<Spawn> list = new List<Spawn>();
		for (int i = 0; i < this.spawns.Count; i++)
		{
			list.Add(this.spawns[i]);
		}
		list.Sort((Spawn spawn1, Spawn spawn2) => this.GetClosestSpawn(spawn1) - this.GetClosestSpawn(spawn2));
		if (!CVars.RandomSpawn)
		{
			return list[list.Count - 1];
		}
		int num = list.Count - 5;
		int max = list.Count - 1;
		if (num < 0)
		{
			num = 0;
		}
		return list[UnityEngine.Random.Range(num, max)];
	}

	// Token: 0x060014C7 RID: 5319 RVA: 0x000DB760 File Offset: 0x000D9960
	private int GetClosestSpawn(Spawn spawn)
	{
		float num = float.MaxValue;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!this.serverNetPlayers[i].IsDeadOrSpectactor)
			{
				float magnitude = (this.serverNetPlayers[i].Position - spawn.pos).magnitude;
				if (magnitude <= num)
				{
					num = magnitude;
				}
			}
		}
		return (int)num;
	}

	// Token: 0x060014C8 RID: 5320 RVA: 0x000DB7DC File Offset: 0x000D99DC
	public override void MainInitialize()
	{
		base.MainInitialize();
		SpawnPoint[] array = (SpawnPoint[])UnityEngine.Object.FindObjectsOfType(typeof(SpawnPoint));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].isDeathMatch)
			{
				this.spawns.Add(new Spawn(array[i]));
			}
		}
		this.spawnRandom.InitNew(this.spawns.Count);
	}

	// Token: 0x0400195E RID: 6494
	private CircleRandom spawnRandom = new CircleRandom();

	// Token: 0x0400195F RID: 6495
	private List<Spawn> spawns = new List<Spawn>();
}
