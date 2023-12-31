using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Assets.Scripts.Game;
using LeagueSystem;
using UnityEngine;

// Token: 0x020001A5 RID: 421
[AddComponentMenu("Scripts/Game/BaseServerGame")]
internal class BaseServerGame : BaseGame
{
	// Token: 0x06000D16 RID: 3350 RVA: 0x0009BFB0 File Offset: 0x0009A1B0
	private void Start()
	{
		ServerLeagueSystem.PlayerListChangedCallback = (Action<int>)Delegate.Combine(ServerLeagueSystem.PlayerListChangedCallback, new Action<int>(this.ResendDataToClients));
	}

	// Token: 0x06000D17 RID: 3351 RVA: 0x0009BFE0 File Offset: 0x0009A1E0
	public override void OnPoolDespawn()
	{
		this.indexBuffGiver = -1;
		for (int i = 0; i < this.loadNetPlayers.Count; i++)
		{
			PoolManager.Despawn(this.loadNetPlayers[i].gameObject);
		}
		this.loadNetPlayers.Clear();
		for (int j = 0; j < this.serverNetPlayers.Count; j++)
		{
			PoolManager.Despawn(this.serverNetPlayers[j].gameObject);
		}
		this.serverNetPlayers.Clear();
		for (int k = 0; k < this.serverEntities.Count; k++)
		{
			PoolItem component = this.serverEntities[k].GetComponent<PoolItem>();
			base.GetComponent<PoolItem>().Childs.Remove(component);
			SingletoneForm<PoolManager>.Instance[component.name].Despawn(component);
		}
		this.serverEntities.Clear();
		for (int l = 0; l < base.transform.GetChildCount(); l++)
		{
			PoolManager.Despawn(base.transform.GetChild(l).gameObject);
		}
		this.newEntityID = 0;
		this.matchStartTime = 0f;
		this.matches = CVars.n_maxMatches;
		this.matchesTimer = new eTimer();
		this.is_night = false;
		this.VIP = null;
		this.bearVIP = PlayerType.spectactor;
		this.slot = new Slot();
		base.OnPoolDespawn();
	}

	// Token: 0x170001BE RID: 446
	// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0009C154 File Offset: 0x0009A354
	public bool isRated
	{
		get
		{
			return this.PlayerCount >= Main.GameModeInfo.playersRequiredForExp;
		}
	}

	// Token: 0x170001BF RID: 447
	// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0009C170 File Offset: 0x0009A370
	public bool tasksEnabled
	{
		get
		{
			return this.PlayerCount >= Main.GameModeInfo.playersRequiredForTasks;
		}
	}

	// Token: 0x170001C0 RID: 448
	// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0009C188 File Offset: 0x0009A388
	public bool IsFull
	{
		get
		{
			return this.PlayerCount >= Main.HostInfo.MaxPlayers;
		}
	}

	// Token: 0x170001C1 RID: 449
	// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0009C1A0 File Offset: 0x0009A3A0
	protected int PlayerCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				if (!(this.serverNetPlayers[i] == null) && !this.serverNetPlayers[i].IsSpectactor)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0009C204 File Offset: 0x0009A404
	protected int SpectactorCount
	{
		get
		{
			int num = 0;
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				if (this.serverNetPlayers[i] != null && this.serverNetPlayers[i].IsSpectactor)
				{
					num++;
				}
			}
			return num;
		}
	}

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0009C264 File Offset: 0x0009A464
	public int AllPlayersCount
	{
		get
		{
			return this.serverNetPlayers.Count + this.loadNetPlayers.Count;
		}
	}

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0009C280 File Offset: 0x0009A480
	public bool HaveSlot
	{
		get
		{
			return this.PlayerCount < Main.HostInfo.MaxPlayers && this.SpectactorCount < Mathf.Max(Main.HostInfo.MaxPlayers - this.PlayerCount, 0) + Main.HostInfo.MaxPlayers / 3 && this.AllPlayersCount < 30 && this.slot.HaveSlot;
		}
	}

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0009C2F0 File Offset: 0x0009A4F0
	public string HaveSlotText
	{
		get
		{
			if (this.PlayerCount >= Main.HostInfo.MaxPlayers)
			{
				return Language.ServerFullPlayers;
			}
			if (this.SpectactorCount >= Mathf.Max(Main.HostInfo.MaxPlayers - this.PlayerCount, 0) + Main.HostInfo.MaxPlayers / 3)
			{
				return Language.ServerFullSpec;
			}
			if (this.AllPlayersCount >= 30)
			{
				return Language.ServerFullSlot;
			}
			return (!this.slot.HaveSlot) ? Language.ServerFullSlot : Language.ServerSlotAvaliable;
		}
	}

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0009C380 File Offset: 0x0009A580
	public int NextEntityID
	{
		get
		{
			return this.newEntityID++;
		}
	}

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0009C3A0 File Offset: 0x0009A5A0
	public List<ServerNetPlayer> ServerNetPlayers
	{
		get
		{
			return this.serverNetPlayers;
		}
	}

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0009C3A8 File Offset: 0x0009A5A8
	public List<ServerNetPlayer> LoadingNetPlayers
	{
		get
		{
			return this.loadNetPlayers;
		}
	}

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0009C3B0 File Offset: 0x0009A5B0
	protected bool OnMatchesEnded
	{
		get
		{
			if (this.matchesTimer.Elapsed > CVars.g_matchMaxTime)
			{
				this.matches = 0;
			}
			return this.matches <= 0;
		}
	}

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0009C3E0 File Offset: 0x0009A5E0
	public virtual bool BearWin
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170001CB RID: 459
	// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0009C3E4 File Offset: 0x0009A5E4
	public virtual bool DeadHeat
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170001CC RID: 460
	// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0009C3E8 File Offset: 0x0009A5E8
	public virtual bool NeedAdditionalRound
	{
		get
		{
			return false;
		}
	}

	// Token: 0x170001CD RID: 461
	// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0009C3EC File Offset: 0x0009A5EC
	public bool StopOnAllExit
	{
		get
		{
			bool flag = true;
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
				if (!serverNetPlayer.IsSpectactor || !serverNetPlayer.IsLoadingFinished)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.RoundPreEnd();
				this.RoundEnd();
				this.MatchEnd();
			}
			return flag;
		}
	}

	// Token: 0x06000D28 RID: 3368 RVA: 0x0009C460 File Offset: 0x0009A660
	[Obfuscation(Exclude = true)]
	private void ClearState()
	{
		if (ServerLeagueSystem.Enabled)
		{
			ServerLeagueSystem.Clear();
		}
		this.state = MatchState.stoped;
		base.StopAllCoroutines();
	}

	// Token: 0x06000D29 RID: 3369 RVA: 0x0009C480 File Offset: 0x0009A680
	private IEnumerator DisconnectAll(float time)
	{
		yield return new WaitForSeconds(time);
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].DelayDisconnect(5f);
		}
		for (int j = 0; j < this.LoadingNetPlayers.Count; j++)
		{
			this.LoadingNetPlayers[j].DelayDisconnect(5f);
		}
		base.Invoke("ClearState", 20f);
		yield return 0;
		yield break;
	}

	// Token: 0x06000D2A RID: 3370 RVA: 0x0009C4AC File Offset: 0x0009A6AC
	public bool StopOnQuit()
	{
		if (this.matches == IDUtil.NoID2)
		{
			return true;
		}
		if (this.OnMatchesEnded)
		{
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				this.serverNetPlayers[i].QuitFromClient(Language.ServerRestarted);
			}
			this.matches = IDUtil.NoID2;
			Main.Quit();
			return true;
		}
		return false;
	}

	// Token: 0x06000D2B RID: 3371 RVA: 0x0009C51C File Offset: 0x0009A71C
	public void PlayerConnected()
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].EnableViews();
		}
		for (int j = 0; j < this.loadNetPlayers.Count; j++)
		{
			this.loadNetPlayers[j].DisableViews();
		}
	}

	// Token: 0x06000D2C RID: 3372 RVA: 0x0009C584 File Offset: 0x0009A784
	public void PlayerConnectedOverheaded()
	{
		NetworkView[] array = (NetworkView[])UnityEngine.Object.FindObjectsOfType(typeof(NetworkView));
		for (int i = 0; i < Network.connections.Length; i++)
		{
			for (int j = 0; j < array.Length; j++)
			{
				array[j].SetScope(Network.connections[i], false);
			}
		}
		this.PlayerConnected();
	}

	// Token: 0x06000D2D RID: 3373 RVA: 0x0009C5F4 File Offset: 0x0009A7F4
	public override void Serialize(eNetworkStream stream)
	{
		try
		{
			List<ServerNetPlayer> list = new List<ServerNetPlayer>(this.serverNetPlayers);
			ArrayUtility.SerializeList<ServerNetPlayer>(stream, list);
			ArrayUtility.SerializeList<ServerEntity>(stream, this.serverEntities);
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x170001CE RID: 462
	// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0009C650 File Offset: 0x0009A850
	public BaseNetPlayer Vip
	{
		get
		{
			return this.VIP;
		}
	}

	// Token: 0x06000D2F RID: 3375 RVA: 0x0009C658 File Offset: 0x0009A858
	public virtual Spawn getSpawnPoint(bool isBear)
	{
		return new Spawn();
	}

	// Token: 0x06000D30 RID: 3376 RVA: 0x0009C660 File Offset: 0x0009A860
	public virtual Spawn getSpawnPoint(bool isBear, int ID)
	{
		return new Spawn();
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0009C668 File Offset: 0x0009A868
	protected virtual void UpdateMatch()
	{
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0009C66C File Offset: 0x0009A86C
	public virtual void AloneStart()
	{
		this.state = MatchState.alone;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].RoundStart();
		}
	}

	// Token: 0x06000D33 RID: 3379 RVA: 0x0009C6B0 File Offset: 0x0009A8B0
	public virtual void MatchStart()
	{
		this.iBearCount = 0;
		this.iUsecCount = 0;
		this.bearList.Clear();
		this.usecList.Clear();
		this.bearWeight = 0;
		this.usecWeight = 0;
		this.matchStartTime = Time.realtimeSinceStartup;
		this.state = MatchState.match_started;
		if (!ServerLeagueSystem.Enabled && Main.IsTeamGame && Globals.I.BalanceTeamAtStart && this.usecList.Count + this.bearList.Count >= 5)
		{
			foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
			{
				if (!serverNetPlayer.IsSpectactor)
				{
					if (serverNetPlayer.PlayerType == PlayerType.bear)
					{
						this.bearList.Add(serverNetPlayer);
						this.bearWeight += serverNetPlayer.Weight;
					}
					else
					{
						this.usecList.Add(serverNetPlayer);
						this.usecWeight += serverNetPlayer.Weight;
					}
				}
			}
			if (Math.Abs(this.bearWeight - this.usecWeight) > 2 * (this.bearWeight + this.usecWeight) / (this.bearList.Count + this.usecList.Count))
			{
				this.BalanceTeam();
			}
		}
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].MatchStart();
		}
		global::Console.print("MATCH START");
	}

	// Token: 0x06000D34 RID: 3380 RVA: 0x0009C86C File Offset: 0x0009AA6C
	public virtual void OnMatchEndAppyToPlayer(ServerNetPlayer player)
	{
		if (this.is_night && player.UnlockSkill(Skills.car_night2))
		{
			player.AddExpClean(player.ObtainedExp * 0.3f);
		}
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0009C89C File Offset: 0x0009AA9C
	public virtual void MatchEnd()
	{
		if (this.iBearCount == 0)
		{
			this.bearWinCount = 0;
			this.usecWinCount++;
		}
		if (this.iUsecCount == 0)
		{
			this.bearWinCount++;
			this.usecWinCount = 0;
		}
		this.matches--;
		if (SingletoneForm<ServerManagerCommunicator>.Instance != null)
		{
			SingletoneForm<ServerManagerCommunicator>.Instance.SendMatchesLeftCount(this.matches);
		}
		this.bearWeight = 0;
		this.usecWeight = 0;
		if (this.bearList != null)
		{
			this.bearList.Clear();
		}
		if (this.usecList != null)
		{
			this.usecList.Clear();
		}
		if (this.bearClanIdList != null)
		{
			this.bearClanIdList.Clear();
		}
		if (this.usecClanIdList != null)
		{
			this.usecClanIdList.Clear();
		}
		if (this.OnMatchesEnded)
		{
			if (!ServerLeagueSystem.Enabled)
			{
				Peer.ServerGame.EventMessage(string.Empty, ChatInfo.notify_message, Language.ServerRestarting);
			}
			Peer.IsHidden = true;
			Peer.IsClosing = true;
		}
		this._delayedMatchStart.Clear();
		this._leagueMatchStartTimer.Stop();
		this.state = MatchState.match_result;
		this.ClearEntities();
		if (this.serverNetPlayers.Count == 0 && !ServerLeagueSystem.Enabled)
		{
			return;
		}
		MatchResultData matchResultData = new MatchResultData();
		MatchResultPlayerData[] array = new MatchResultPlayerData[this.serverNetPlayers.Count];
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.OnMatchEndAppyToPlayer(this.serverNetPlayers[i]);
			array[i] = new MatchResultPlayerData(this.serverNetPlayers[i]);
			matchResultData.gameTotalKills += array[i].killCount;
			matchResultData.gameTotalDeaths += array[i].deathCount;
			if (ServerLeagueSystem.Enabled)
			{
				ServerLeagueSystem.AddPlayerStats(this.serverNetPlayers[i].UserID.ToString(), this.serverNetPlayers[i].Stats);
				this.serverNetPlayers[i].UserInfo.userStats.AddDelta(this.serverNetPlayers[i].Stats);
			}
		}
		matchResultData.gameTotalTime = (int)(Time.realtimeSinceStartup - this.matchStartTime);
		if (this.serverNetPlayers.Count != 0)
		{
			matchResultData.bestPlayer = this.Top1InMatch().matchResultData;
			matchResultData.worstPlayer = this.TopNMinus1InMatch().matchResultData;
			matchResultData.players = array;
		}
		this.SaveMasteringStats(this.serverNetPlayers);
		ReportSystem.Instance.SaveSuspects();
		if (ServerLeagueSystem.Enabled)
		{
			ServerLeagueSystem.AddWinPoints(base.UsecWinCount, base.BearWinCount);
			ServerLeagueSystem.SaveAtMatchEnd();
			base.StartCoroutine(this.DisconnectAll(10f));
		}
		else
		{
			for (int j = 0; j < this.serverNetPlayers.Count; j++)
			{
				ServerNetPlayer serverNetPlayer = this.serverNetPlayers[j];
				serverNetPlayer.MatchEnd(matchResultData);
				if (this.IsTeamGame && !serverNetPlayer.IsSpectactor)
				{
					if (serverNetPlayer.PlayerType == PlayerType.bear)
					{
						serverNetPlayer.ToClient("ChooseTeamFromServer", new object[]
						{
							1
						});
						serverNetPlayer.PlayerType = PlayerType.usec;
						this.usecWeight += serverNetPlayer.Weight;
						if (this.usecList != null)
						{
							this.usecList.Add(serverNetPlayer);
						}
						if (this.usecClanIdList != null && serverNetPlayer.UserInfo.clanID != 0 && !this.usecClanIdList.Contains(serverNetPlayer.UserInfo.clanID))
						{
							this.usecClanIdList.Add(serverNetPlayer.UserInfo.clanID);
						}
					}
					else if (serverNetPlayer.PlayerType == PlayerType.usec)
					{
						serverNetPlayer.ToClient("ChooseTeamFromServer", new object[]
						{
							0
						});
						serverNetPlayer.PlayerType = PlayerType.bear;
						this.bearWeight += serverNetPlayer.Weight;
						if (this.bearList != null)
						{
							this.bearList.Add(serverNetPlayer);
						}
						if (this.bearClanIdList != null && serverNetPlayer.UserInfo.clanID != 0 && !this.bearClanIdList.Contains(serverNetPlayer.UserInfo.clanID))
						{
							this.bearClanIdList.Add(serverNetPlayer.UserInfo.clanID);
						}
					}
				}
			}
		}
		global::Console.print("MATCH END (" + this.matches + ")");
	}

	// Token: 0x06000D36 RID: 3382 RVA: 0x0009CD40 File Offset: 0x0009AF40
	private void OnApplicationQuit()
	{
		ServerLeagueSystem.SaveAtMatchEnd();
	}

	// Token: 0x06000D37 RID: 3383 RVA: 0x0009CD48 File Offset: 0x0009AF48
	public void MatchEndLeague(string json)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].MatchEndLeague(json);
		}
	}

	// Token: 0x06000D38 RID: 3384 RVA: 0x0009CD84 File Offset: 0x0009AF84
	private void BalanceTeam()
	{
		this.usecClanIdList.Clear();
		this.bearClanIdList.Clear();
		this.bearList.Clear();
		this.usecList.Clear();
		this.bearWeight = 0;
		this.usecWeight = 0;
		this.serverNetPlayers.Sort((ServerNetPlayer p1, ServerNetPlayer p2) => p2.Weight.CompareTo(p1.Weight));
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			bool flag = this.JoinToBear(serverNetPlayer);
			serverNetPlayer.ToClient("ChooseTeamFromServer", new object[]
			{
				(!flag) ? 1 : 0
			});
			serverNetPlayer.playerInfo.playerType = ((!flag) ? PlayerType.usec : PlayerType.bear);
			if (flag)
			{
				this.bearWeight += serverNetPlayer.Weight;
				this.bearList.Add(serverNetPlayer);
				if (serverNetPlayer.UserInfo.clanID != 0 && !this.bearClanIdList.Contains(serverNetPlayer.UserInfo.clanID))
				{
					this.bearClanIdList.Add(serverNetPlayer.UserInfo.clanID);
				}
			}
			else
			{
				this.usecWeight += serverNetPlayer.Weight;
				this.usecList.Add(serverNetPlayer);
				if (serverNetPlayer.UserInfo.clanID != 0 && !this.usecClanIdList.Contains(serverNetPlayer.UserInfo.clanID))
				{
					this.usecClanIdList.Add(serverNetPlayer.UserInfo.clanID);
				}
			}
		}
	}

	// Token: 0x06000D39 RID: 3385 RVA: 0x0009CF58 File Offset: 0x0009B158
	private bool JoinToBear(ServerNetPlayer p)
	{
		bool flag = false;
		bool flag2 = false;
		if (p.UserInfo.clanID != 0 && (this.bearList.Count > 0 || this.usecList.Count > 0))
		{
			if (this.bearClanIdList.Contains(p.UserInfo.clanID) && this.bearList.Count - this.usecList.Count <= Globals.I.TeamPlayersOdds)
			{
				flag = true;
			}
			else if (this.usecClanIdList.Contains(p.UserInfo.clanID) && this.usecList.Count - this.bearList.Count <= Globals.I.TeamPlayersOdds)
			{
				flag2 = true;
			}
		}
		return flag || (!flag2 && ((this.bearWeight < this.usecWeight && this.usecWeight - this.bearWeight > Globals.I.TeamWeightOdds) || (this.usecWeight < this.bearWeight && this.bearWeight - this.usecWeight > Globals.I.TeamWeightOdds && false)));
	}

	// Token: 0x06000D3A RID: 3386 RVA: 0x0009D098 File Offset: 0x0009B298
	public virtual void RoundStart()
	{
		this.state = MatchState.round_going;
		this.ClearEntities();
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			serverNetPlayer.RoundStart();
		}
		this.Radio(RadioEnum.round_start);
	}

	// Token: 0x06000D3B RID: 3387 RVA: 0x0009D114 File Offset: 0x0009B314
	public virtual void RoundPreEnd()
	{
		this.state = MatchState.round_pre_ended;
	}

	// Token: 0x06000D3C RID: 3388 RVA: 0x0009D120 File Offset: 0x0009B320
	public virtual void RoundEnd()
	{
		this.state = MatchState.round_ended;
		this.ClearEntities();
		base.StopCoroutine("MortarExplosionEnum");
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			serverNetPlayer.RoundEnd();
		}
	}

	// Token: 0x06000D3D RID: 3389 RVA: 0x0009D1A0 File Offset: 0x0009B3A0
	public virtual void BearWins(bool bearWins)
	{
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			serverNetPlayer.BearWins(bearWins);
		}
	}

	// Token: 0x06000D3E RID: 3390 RVA: 0x0009D208 File Offset: 0x0009B408
	public void AddPlayer(int userID, NetworkViewID targetID, string guid = "", string hash = "")
	{
		ServerNetPlayer component = SingletoneForm<PoolManager>.Instance["server_user"].Spawn().GetComponent<ServerNetPlayer>();
		component.transform.parent = base.transform;
		component.GetComponent<PoolItem>().AutoDespawn(60f);
		component.InitializeNetwork(new ServerCmdCollector(), Network.AllocateViewID(), targetID, this.slot.Spawn());
		if (targetID.owner != default(NetworkPlayer))
		{
			component.NetworkPlayer.owner = targetID.owner;
		}
		component.guid = guid;
		component.CreateViews();
		component.DisableViews();
		component.UserInfo.userID = userID;
		component.playerInfo.playerID = Peer.ServerGame.NextEntityID;
		component.playerInfo.userID = userID;
		this.loadNetPlayers.Add(component);
		component.Hash = hash;
		component.LoadProfile(true);
	}

	// Token: 0x06000D3F RID: 3391 RVA: 0x0009D304 File Offset: 0x0009B504
	public void AddPlayer(ServerNetPlayer player)
	{
		this.loadNetPlayers.Remove(player);
		this.serverNetPlayers.Add(player);
		if (SingletoneForm<ServerManagerCommunicator>.Instance != null)
		{
			SingletoneForm<ServerManagerCommunicator>.Instance.SendCurrentPlayersCount(this.AllPlayersCount);
		}
	}

	// Token: 0x06000D40 RID: 3392 RVA: 0x0009D34C File Offset: 0x0009B54C
	public void RemovePlayer(ServerNetPlayer player)
	{
		this.slot.Despawn(player.Group);
		if (this.loadNetPlayers.Contains(player))
		{
			this.loadNetPlayers.Remove(player);
		}
		if (this.serverNetPlayers.Contains(player))
		{
			this.serverNetPlayers.Remove(player);
		}
		if (SingletoneForm<ServerManagerCommunicator>.Instance != null)
		{
			SingletoneForm<ServerManagerCommunicator>.Instance.SendCurrentPlayersCount(this.AllPlayersCount);
		}
	}

	// Token: 0x06000D41 RID: 3393 RVA: 0x0009D3C8 File Offset: 0x0009B5C8
	public void PlayerDisconnected(NetworkPlayer player)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!this.serverNetPlayers[i].NetworkPlayer.IsVirtual && this.serverNetPlayers[i].NetworkPlayer.owner == player)
			{
				if (ServerLeagueSystem.Enabled)
				{
					ServerLeagueSystem.OnLeave(this.serverNetPlayers[i].UserID);
				}
				this.serverNetPlayers[i].DisconnectPlayer();
			}
		}
		for (int j = 0; j < this.loadNetPlayers.Count; j++)
		{
			if (j >= this.serverNetPlayers.Count)
			{
				break;
			}
			if (!this.loadNetPlayers[j].NetworkPlayer.IsVirtual && this.loadNetPlayers[j].NetworkPlayer.owner == player)
			{
				if (ServerLeagueSystem.Enabled)
				{
					ServerLeagueSystem.OnLeave(this.serverNetPlayers[j].UserID);
				}
				this.loadNetPlayers[j].Disconnect();
			}
		}
	}

	// Token: 0x06000D42 RID: 3394 RVA: 0x0009D508 File Offset: 0x0009B708
	public void PlayerDisconnected(ServerNetPlayer player, string reason = "")
	{
		if (reason == string.Empty)
		{
			reason = Language.UserQuited;
		}
		if (CVars.earlyExitSave)
		{
			this.SaveMasteringStats(new List<ServerNetPlayer>
			{
				player
			});
			player.SaveAndDisconnect(reason);
		}
		else
		{
			player.PlayerDisconnected(reason);
		}
	}

	// Token: 0x06000D43 RID: 3395 RVA: 0x0009D560 File Offset: 0x0009B760
	public void AddEntity(ServerEntity entity)
	{
		this.serverEntities.Add(entity);
	}

	// Token: 0x06000D44 RID: 3396 RVA: 0x0009D570 File Offset: 0x0009B770
	public void RemoveEntity(ServerEntity entity)
	{
		this.serverEntities.Remove(entity);
		SingletoneForm<PoolManager>.Instance[entity.name].Despawn(entity.GetComponent<PoolItem>());
	}

	// Token: 0x06000D45 RID: 3397 RVA: 0x0009D5A8 File Offset: 0x0009B7A8
	public void ClearEntities()
	{
		for (int i = 0; i < this.serverEntities.Count; i++)
		{
			SingletoneForm<PoolManager>.Instance[this.serverEntities[i].name].Despawn(this.serverEntities[i].GetComponent<PoolItem>());
		}
		this.serverEntities.Clear();
	}

	// Token: 0x06000D46 RID: 3398 RVA: 0x0009D610 File Offset: 0x0009B810
	public bool CanSpawnAsVIP(BaseNetPlayer player)
	{
		if (this.VIP == null && this is ServerTeamEliminationGame && (CVars.VIP_test || ((this.bearVIP == PlayerType.spectactor || player.playerInfo.playerType != this.bearVIP) && (this as ServerTeamEliminationGame).BearCount() >= CVars.g_VIPRequiredPlayers && (this as ServerTeamEliminationGame).UsecCount() >= CVars.g_VIPRequiredPlayers)))
		{
			this.VIP = player;
			this.bearVIP = player.playerInfo.playerType;
			return true;
		}
		return false;
	}

	// Token: 0x06000D47 RID: 3399 RVA: 0x0009D6AC File Offset: 0x0009B8AC
	public void DespawnVIP(BaseNetPlayer player)
	{
		if (this.VIP == player)
		{
			for (int i = 0; i < this.serverNetPlayers.Count; i++)
			{
				if (!(this.serverNetPlayers[i] == this.VIP))
				{
					if (!this.serverNetPlayers[i].IsTeam(this.VIP) && !this.serverNetPlayers[i].IsSpectactor)
					{
						this.serverNetPlayers[i].Exp(20f, 0f, true);
					}
				}
			}
			this.VIP = null;
		}
	}

	// Token: 0x06000D48 RID: 3400 RVA: 0x0009D760 File Offset: 0x0009B960
	public void VIPSpawned(BaseNetPlayer player)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!(this.serverNetPlayers[i] == player))
			{
				if (this.serverNetPlayers[i].IsTeam(player))
				{
					this.serverNetPlayers[i].EventMessage(string.Empty, ChatInfo.notify_message, Language.VIPInYourTeam);
				}
				else
				{
					this.serverNetPlayers[i].EventMessage(string.Empty, ChatInfo.notify_message, Language.VIPInEnemyTeam);
				}
			}
		}
	}

	// Token: 0x06000D49 RID: 3401 RVA: 0x0009D7FC File Offset: 0x0009B9FC
	public void VIPMakeKill(float exp)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!(this.serverNetPlayers[i] == this.VIP))
			{
				if (this.serverNetPlayers[i].IsTeam(this.VIP) && !this.serverNetPlayers[i].IsSpectactor)
				{
					this.serverNetPlayers[i].Exp(exp / 5f, 0f, true);
				}
			}
		}
	}

	// Token: 0x06000D4A RID: 3402 RVA: 0x0009D898 File Offset: 0x0009BA98
	public bool IsPlayerInGame(int playerID)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].ID == playerID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D4B RID: 3403 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
	public bool IsUserInGame(int userID)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].UserID == userID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000D4C RID: 3404 RVA: 0x0009D928 File Offset: 0x0009BB28
	public void EventMessage(string nick, ChatInfo info, string msg)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].EventMessage(nick, info, msg);
		}
	}

	// Token: 0x06000D4D RID: 3405 RVA: 0x0009D968 File Offset: 0x0009BB68
	public void KickByUid(int uid, string reason = "")
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].UserID == uid)
			{
				this.serverNetPlayers[i].PlayerDisconnected(Language.ServerDisconnect + " " + ((!(reason == string.Empty)) ? reason : "(kick)"));
				return;
			}
		}
	}

	// Token: 0x06000D4E RID: 3406 RVA: 0x0009D9F0 File Offset: 0x0009BBF0
	public void Kick(int playerID, string reason = "")
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].ID == playerID)
			{
				this.serverNetPlayers[i].PlayerDisconnected(Language.ServerDisconnect + " " + ((!(reason == string.Empty)) ? reason : "(kick)"));
				return;
			}
		}
	}

	// Token: 0x06000D4F RID: 3407 RVA: 0x0009DA78 File Offset: 0x0009BC78
	public void RaycastAll(Ray ray, float packetLatency, List<eRaycastHit> hits)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (serverNetPlayer.IsAlive)
			{
				serverNetPlayer.RaycastAll(ray, Peer.ServerGame.ServerTime - packetLatency - CVars.p_interpolateTime, hits);
			}
		}
	}

	// Token: 0x06000D50 RID: 3408 RVA: 0x0009DAD4 File Offset: 0x0009BCD4
	public ServerNetPlayer GetPlayer(int playerID)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].ID == playerID)
			{
				return this.serverNetPlayers[i];
			}
		}
		return null;
	}

	// Token: 0x06000D51 RID: 3409 RVA: 0x0009DB28 File Offset: 0x0009BD28
	public void AddAttacker(ServerNetPlayer attacker, bool hasSS = false)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (!(serverNetPlayer == attacker))
			{
				if (!serverNetPlayer.IsDeadOrSpectactor)
				{
					if (!attacker.IsDeadOrSpectactor)
					{
						bool flag = (attacker.Position - serverNetPlayer.Position).magnitude < ((!attacker.Ammo.weaponEquiped) ? 30f : attacker.Ammo.CurrentWeapon.hearRadius);
						if (flag)
						{
							if (this.IsTeamGame)
							{
								if (!(attacker.IsBear ^ !serverNetPlayer.IsBear))
								{
									if (hasSS)
									{
										if (serverNetPlayer.UserInfo.skillUnlocked(Skills.hear3))
										{
											serverNetPlayer.AddAttacker(attacker.ID);
										}
									}
									else
									{
										serverNetPlayer.AddAttacker(attacker.ID);
									}
									for (int j = 0; j < this.serverNetPlayers.Count; j++)
									{
										ServerNetPlayer serverNetPlayer2 = this.serverNetPlayers[j];
										if (!(serverNetPlayer == serverNetPlayer2))
										{
											if (!(attacker.IsBear ^ serverNetPlayer.IsBear))
											{
												if (hasSS)
												{
													if (serverNetPlayer2.UserInfo.skillUnlocked(Skills.hear3))
													{
														serverNetPlayer2.AddAttacker(attacker.ID);
													}
												}
												else
												{
													serverNetPlayer2.AddAttacker(attacker.ID);
												}
											}
										}
									}
								}
							}
							else if (hasSS)
							{
								if (serverNetPlayer.UserInfo.skillUnlocked(Skills.hear3))
								{
									serverNetPlayer.AddAttacker(attacker.ID);
								}
							}
							else
							{
								serverNetPlayer.AddAttacker(attacker.ID);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000D52 RID: 3410 RVA: 0x0009DD24 File Offset: 0x0009BF24
	public void KillInfo(int killerId, int method, bool headShot, int killedId, string boneToBeWild, Vector3 boneForce, int weaponKills)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].KillInfo(killerId, method, headShot, killedId, boneToBeWild, boneForce, weaponKills);
		}
	}

	// Token: 0x06000D53 RID: 3411 RVA: 0x0009DD6C File Offset: 0x0009BF6C
	public void Radio(RadioEnum radio)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].Radio(radio);
		}
	}

	// Token: 0x06000D54 RID: 3412 RVA: 0x0009DDA8 File Offset: 0x0009BFA8
	public void Radio(RadioEnum radio, bool IsBear)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.IsTeamGame)
			{
				if (this.serverNetPlayers[i].IsTeam(IsBear))
				{
					this.serverNetPlayers[i].Radio(radio);
				}
			}
			else
			{
				this.serverNetPlayers[i].Radio(radio);
			}
		}
	}

	// Token: 0x06000D55 RID: 3413 RVA: 0x0009DE1C File Offset: 0x0009C01C
	public void Explosion(Vector3 pos)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			this.serverNetPlayers[i].Explosion(pos);
		}
	}

	// Token: 0x06000D56 RID: 3414 RVA: 0x0009DE58 File Offset: 0x0009C058
	public void MortarExplosion(ServerNetPlayer player, Vector3 pos)
	{
		if (player == null)
		{
			return;
		}
		int num = 4;
		int num2 = 10;
		if (player.playerInfo.skillUnlocked(Skills.mortar1))
		{
			num = 8;
			if (player.playerInfo.skillUnlocked(Skills.mortar2))
			{
				num = 14;
				if (player.playerInfo.skillUnlocked(Skills.mortar3))
				{
					num = 25;
					num2 = 25;
				}
			}
		}
		Vector3[] array = new Vector3[num];
		for (int i = 0; i < num; i++)
		{
			Ray ray = new Ray(pos + new Vector3(UnityEngine.Random.insideUnitCircle.x * (float)num2, 30f, UnityEngine.Random.insideUnitCircle.x * 10f), -Vector3.up);
			RaycastHit raycastHit;
			Physics.Raycast(ray, out raycastHit, 200f, PhysicsUtility.level_layers);
			array[i] = raycastHit.point;
		}
		base.StartCoroutine("MortarExplosionEnum", new object[]
		{
			player,
			array
		});
		for (int j = 0; j < this.serverNetPlayers.Count; j++)
		{
			this.serverNetPlayers[j].MortarExplosion(array);
		}
	}

	// Token: 0x06000D57 RID: 3415 RVA: 0x0009DF94 File Offset: 0x0009C194
	[Obfuscation(Exclude = true)]
	protected IEnumerator MortarExplosionEnum(object obj)
	{
		ServerNetPlayer player = (ServerNetPlayer)((object[])obj)[0];
		Vector3[] poses = (Vector3[])((object[])obj)[1];
		yield return new WaitForSeconds(2f);
		yield return new WaitForSeconds(3f);
		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(0.1f);
		for (int i = 0; i < poses.Length; i++)
		{
			yield return new WaitForSeconds(0.1f);
			yield return new WaitForFixedUpdate();
			if (player == null || player.UserInfo == null)
			{
				break;
			}
			for (int p = 0; p < this.serverNetPlayers.Count; p++)
			{
				if (!this.serverNetPlayers[p].IsDeadOrSpectactor)
				{
					float damage = (1f - (Mathf.Min(Mathf.Max((poses[i] - this.serverNetPlayers[p].Position).magnitude, 3f), 10f) - 3f) / 7f) * 150f * this.serverNetPlayers[p].explosiveDamageMult;
					if (damage != 0f)
					{
						if (!this.serverNetPlayers[p].InHomeBase)
						{
							if (!ServerLeagueSystem.Enabled || !Peer.HardcoreMode || !this.serverNetPlayers[p].IsTeam(player.IsBear))
							{
								if (!this.serverNetPlayers[p].IsTeam(player.IsBear) || !this.IsTeamGame || Peer.HardcoreMode)
								{
									ServerNetPlayer target = this.serverNetPlayers[p];
									if (target.PlayerHit(damage, 0f, player, null))
									{
										if (player != target)
										{
											if (this.IsTeamGame)
											{
												if (target.IsTeam(player.IsBear))
												{
													player.Exp((float)((!Peer.HardcoreMode) ? -10 : -50), 0f, true);
												}
												else
												{
													player.Stats.ExternKill(player, false, false, target);
													if (ServerVars.canFarmTasksCached || player.UserInfo.currentLevel < 10)
													{
														AchievementsEngine.Kill(player, target, AchievementTarget.any, AchievementKillType.kill, KillStreakEnum.none, WeaponNature.mortar, player.Health < 10f, false, false);
													}
												}
											}
											else
											{
												player.Stats.ExternKill(player, false, false, target);
												if (ServerVars.canFarmTasksCached || player.UserInfo.currentLevel < 10)
												{
													AchievementsEngine.Kill(player, target, AchievementTarget.any, AchievementKillType.kill, KillStreakEnum.none, WeaponNature.mortar, player.Health < 10f, false, false);
												}
											}
											target.Kill(player, 122, false, target.ID, "mortar", (target.Position - poses[i]).normalized, 0);
										}
										else
										{
											target.Stats.Suicide(target);
											target.Kill(target, 123, false, target.ID, "mortar", (target.Position - poses[i]).normalized, 0);
										}
									}
								}
							}
						}
					}
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	// Token: 0x06000D58 RID: 3416 RVA: 0x0009DFC0 File Offset: 0x0009C1C0
	public void TacticalExplosion(Vector3 pos)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (serverNetPlayer.IsAlive && (serverNetPlayer.Position - pos).magnitude < 20f)
			{
				serverNetPlayer.Stats.Suicide(serverNetPlayer);
				serverNetPlayer.Kill(serverNetPlayer, 123, false, serverNetPlayer.ID, "tactical", (serverNetPlayer.Position - pos).normalized, 0);
			}
			serverNetPlayer.TacticalExplosion(pos, this.placementIndex);
		}
	}

	// Token: 0x06000D59 RID: 3417 RVA: 0x0009E068 File Offset: 0x0009C268
	public ServerNetPlayer Top1InMatch()
	{
		ServerNetPlayer serverNetPlayer = this.serverNetPlayers[0];
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].Points > serverNetPlayer.Points)
			{
				serverNetPlayer = this.serverNetPlayers[i];
			}
		}
		return serverNetPlayer;
	}

	// Token: 0x06000D5A RID: 3418 RVA: 0x0009E0C8 File Offset: 0x0009C2C8
	public ServerNetPlayer TopNMinus1InMatch()
	{
		ServerNetPlayer serverNetPlayer = this.serverNetPlayers[0];
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (!this.serverNetPlayers[i].IsSpectactor)
			{
				if (this.serverNetPlayers[i].Points < serverNetPlayer.Points)
				{
					serverNetPlayer = this.serverNetPlayers[i];
				}
			}
		}
		return serverNetPlayer;
	}

	// Token: 0x06000D5B RID: 3419 RVA: 0x0009E144 File Offset: 0x0009C344
	public void Chat(BaseNetPlayer player, string msg, ChatInfo info)
	{
		if (info != ChatInfo.radio_message_at_hit)
		{
			if (player.fTalkTime > Time.realtimeSinceStartup)
			{
				player.iChatMessageCounter++;
			}
			else
			{
				player.fTalkTime = Time.realtimeSinceStartup + CVars.g_floodTimer;
				player.iChatMessageCounter = 0;
			}
		}
		string text = player.Nick;
		if (!player.IsSpectactor)
		{
			if (info == ChatInfo.enemy_message)
			{
				text = text + "(" + Language.ChToAll + ((!player.IsAlive) ? ("," + Language.Dead + ")") : ")");
			}
			else
			{
				text += ((!player.IsAlive) ? ("(" + Language.Dead + ")") : string.Empty);
			}
		}
		if (player.IsSpectactor && info != ChatInfo.radio_message)
		{
			info = ChatInfo.spectactor_message;
		}
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			bool flag = false;
			if (this.IsTeamGame)
			{
				if (player.IsDeadOrSpectactor && serverNetPlayer.IsAlive)
				{
					continue;
				}
				if (!player.IsTeam(serverNetPlayer) && (info == ChatInfo.teammate_message || info == ChatInfo.radio_message_at_hit))
				{
					continue;
				}
				if (!player.IsTeam(serverNetPlayer) && info == ChatInfo.radio_message)
				{
					continue;
				}
				if (serverNetPlayer.IsSpectactor && info == ChatInfo.teammate_message)
				{
					continue;
				}
				if (player.IsTeam(serverNetPlayer) && info == ChatInfo.enemy_message)
				{
					flag = true;
				}
			}
			serverNetPlayer.ToClient("ChatFromServer", new object[]
			{
				player.ID,
				text,
				(int)((!flag) ? info : ChatInfo.teammate_message),
				msg
			});
		}
	}

	// Token: 0x06000D5C RID: 3420 RVA: 0x0009E34C File Offset: 0x0009C54C
	public void GrenadeExplosion(ServerNetPlayer player, Vector3 pos)
	{
		if (player == null)
		{
			return;
		}
		float num = 3f;
		float num2 = 7f * player.grenadeExplosionRadiusMult;
		pos.y += 0.05f;
		player.Stats.StartStreak();
		bool flag = false;
		List<int> list = BIT.INDEXES(32768);
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			if (!serverNetPlayer.IsDeadOrSpectactor)
			{
				if (!ServerLeagueSystem.Enabled || !Peer.HardcoreMode || !player.IsTeam(serverNetPlayer) || !(serverNetPlayer != player))
				{
					if (Peer.HardcoreMode || !player.IsTeam(serverNetPlayer) || !(serverNetPlayer != player))
					{
						if (!serverNetPlayer.InHomeBase)
						{
							float magnitude = (serverNetPlayer.Position - pos).magnitude;
							if (magnitude < num2)
							{
								if (magnitude <= num || !Physics.Linecast(pos, serverNetPlayer.Position, PhysicsUtility.level_layers))
								{
									float num3 = 1f - Mathf.Max(magnitude - num, 0f) / (num2 - num);
									float num4 = num3 * 105f * player.grenadeDamageMult * serverNetPlayer.explosiveDamageMult;
									if (num4 != 0f)
									{
										if (serverNetPlayer.PlayerHit(num4, 0f, player, null))
										{
											if (player != serverNetPlayer)
											{
												player.Stats.GrenadeKill(player, serverNetPlayer);
												if (this.tasksEnabled && !player.IsTeam(serverNetPlayer))
												{
													bool flag2 = true;
													List<int> list2 = BIT.INDEXES((int)player.Stats.NowEndStreak());
													foreach (int item in list)
													{
														flag2 &= list2.Contains(item);
													}
													if (flag2 && flag)
													{
														continue;
													}
													this.CalcGranadeAchievements(player, serverNetPlayer);
													if (flag2)
													{
														flag = true;
													}
													ContractsEngine.Kill(player, serverNetPlayer, AchievementTarget.any, AchievementKillType.kill, player.Stats.NowEndStreak(), WeaponNature.grenade, (Maps)Main.HostInfo.MapIndex, Main.GameMode, false);
												}
												serverNetPlayer.Kill(player, 126, false, serverNetPlayer.ID, "grenade", (serverNetPlayer.Position - pos).normalized * num4 / 100f, 0);
											}
											else
											{
												player.Stats.Suicide(player);
												serverNetPlayer.Kill(serverNetPlayer, 123, false, serverNetPlayer.ID, "grenade", (serverNetPlayer.Position - pos).normalized * num4 / 100f, 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		player.Stats.EndStreak(player);
	}

	// Token: 0x06000D5D RID: 3421 RVA: 0x0009E6B0 File Offset: 0x0009C8B0
	private void CalcGranadeAchievements(ServerNetPlayer player, ServerNetPlayer target)
	{
		AchievementTarget achievementTarget = AchievementTarget.any;
		if (target.playerInfo.Health < 10f)
		{
			achievementTarget |= AchievementTarget.almostDead;
		}
		if (target == Peer.ServerGame.Top1InMatch())
		{
			achievementTarget |= AchievementTarget.firstInMatch;
		}
		AchievementKillType killType = AchievementKillType.kill;
		AchievementsEngine.Kill(player, target, achievementTarget, killType, player.Stats.NowEndStreak(), WeaponNature.grenade, player.playerInfo.Health < 10f, false, false);
	}

	// Token: 0x06000D5E RID: 3422 RVA: 0x0009E71C File Offset: 0x0009C91C
	public void SonarTick(ServerNetPlayer player, ServerEntity entity, List<int> IDs)
	{
		if (player == null || entity == null || IDs == null)
		{
			return;
		}
		float num = 25f;
		try
		{
			if (player.playerInfo.skillUnlocked(Skills.sonar1))
			{
				num *= 5f;
			}
			if (player.playerInfo.skillUnlocked(Skills.sonar3))
			{
				num += 30f;
			}
		}
		catch
		{
			return;
		}
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			ServerNetPlayer serverNetPlayer = this.serverNetPlayers[i];
			if (serverNetPlayer.ID != player.ID && serverNetPlayer.ID != -1 && !serverNetPlayer.IsDeadOrSpectactor)
			{
				if (!this.IsTeamGame || !(serverNetPlayer.IsBear ^ !player.IsBear))
				{
					if ((entity.transform.position - serverNetPlayer.Position).magnitude < num)
					{
						if (IDs.IndexOf(serverNetPlayer.ID) == -1 && player.UserInfo != null)
						{
							IDs.Add(serverNetPlayer.ID);
							player.Exp(5f, 0f, true);
							player.Radio(RadioEnum.beep);
							if (ServerVars.canFarmTasksCached || player.UserInfo.currentLevel < 10)
							{
								AchievementsEngine.SonarDetect(player);
							}
						}
						this.AddAttacker(serverNetPlayer, false);
					}
				}
			}
		}
	}

	// Token: 0x06000D5F RID: 3423 RVA: 0x0009E8D4 File Offset: 0x0009CAD4
	[Obfuscation(Exclude = true)]
	protected void UpdateNight()
	{
		int num = Convert.ToInt32(DateTime.Now.ToString("%H")) + 2;
		if (num >= 2 && num <= 8)
		{
			this.is_night = true;
		}
		else
		{
			this.is_night = false;
		}
	}

	// Token: 0x06000D60 RID: 3424 RVA: 0x0009E91C File Offset: 0x0009CB1C
	public void SpawnAll()
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].PlayerType != PlayerType.spectactor)
			{
				this.serverNetPlayers[i].BaseServerSpawn();
			}
		}
	}

	// Token: 0x06000D61 RID: 3425 RVA: 0x0009E970 File Offset: 0x0009CB70
	public override void MainInitialize()
	{
		base.MainInitialize();
		this.ClanBuffComposite |= 16;
		this.ClanBuffComposite |= 32;
		this.ClanBuffComposite |= 4;
		this.ClanBuffComposite |= 8;
		this.ClanBuffComposite |= 2;
		if (SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--matches"))
		{
			this.matches = Convert.ToInt32(SingletoneComponent<Main>.Instance.commandLineArgs.TryGetValue("--matches"));
		}
		else
		{
			this.matches = CVars.n_maxMatches;
		}
		this.matchesTimer.Start();
		base.InvokeRepeating("UpdateNight", 1f, 60f);
	}

	// Token: 0x06000D62 RID: 3426 RVA: 0x0009EA34 File Offset: 0x0009CC34
	private IEnumerator MatchStart(float time)
	{
		yield return new WaitForSeconds(time);
		if (this.state != MatchState.stoped)
		{
			yield break;
		}
		if (ServerLeagueSystem.Enabled)
		{
			if (string.IsNullOrEmpty(ServerLeagueSystem.MatchID))
			{
				yield break;
			}
			if (!this.CanStartLeagueMatch)
			{
				this._delayedMatchStart.Clear();
				yield break;
			}
			this.MatchStart();
			this.RoundStart();
		}
		else
		{
			this.MatchStart();
			this.RoundStart();
		}
		yield break;
	}

	// Token: 0x06000D63 RID: 3427 RVA: 0x0009EA60 File Offset: 0x0009CC60
	private bool IsAllPlayersLoaded()
	{
		bool result = this.serverNetPlayers.Count > 0;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (this.serverNetPlayers[i].Loading != 100)
			{
				result = false;
			}
		}
		return result;
	}

	// Token: 0x06000D64 RID: 3428 RVA: 0x0009EAB4 File Offset: 0x0009CCB4
	public void DelayedMatchStart(float time)
	{
		Debug.Log("Match Started at " + time + " seconds");
		base.StartCoroutine(this.MatchStart(time));
	}

	// Token: 0x170001CF RID: 463
	// (get) Token: 0x06000D65 RID: 3429 RVA: 0x0009EAEC File Offset: 0x0009CCEC
	private bool CanStartLeagueMatch
	{
		get
		{
			return ServerLeagueSystem.AllPlayersReady() && this.IsAllPlayersLoaded() && ServerLeagueSystem.PlayersCount <= this.serverNetPlayers.Count && Main.HostInfo.MaxPlayers == this.serverNetPlayers.Count;
		}
	}

	// Token: 0x06000D66 RID: 3430 RVA: 0x0009EB40 File Offset: 0x0009CD40
	public override void OnFixedUpdate()
	{
		if (ServerLeagueSystem.Enabled)
		{
			if (this.state == MatchState.stoped)
			{
				if (this.StopOnQuit())
				{
					return;
				}
				if (this._refreshAllLoadedTimer < Time.time)
				{
					this._refreshAllLoadedTimer = Time.time + 2f;
					if (this.CanStartLeagueMatch && this._delayedMatchStart.Do())
					{
						this.DelayedMatchStart((float)CVars.LeagueAllReadyStartTimeout);
					}
				}
			}
			else if (this.state != MatchState.stoped)
			{
				this.UpdateMatch();
			}
		}
		else
		{
			this.UpdateMatch();
		}
		foreach (ServerNetPlayer serverNetPlayer in this.serverNetPlayers)
		{
			serverNetPlayer.CallFixedUpdate();
		}
	}

	// Token: 0x06000D67 RID: 3431 RVA: 0x0009EC30 File Offset: 0x0009CE30
	public virtual void ClanBuffLogic(ServerNetPlayer reciever, ServerNetPlayer giver)
	{
		if (reciever.UserInfo.clanID == giver.UserInfo.clanID && reciever.playerInfo.playerType == giver.playerInfo.playerType)
		{
			if (BIT.AND((int)reciever.playerInfo.buffs, 67108864))
			{
				reciever.playerInfo.buffs ^= Buffs.leader_regen;
			}
			reciever.playerInfo.buffs |= (Buffs)giver.ClanBuffTeammate;
			giver.playerInfo.buffs |= (Buffs)giver.ClanBuffSelf;
			if (giver == reciever && BIT.AND(giver.ClanBuffDistance, 134217728) && !reciever.AddMagOnce && reciever.Ammo.state.equiped == WeaponEquipedState.Primary && reciever.Ammo.CurrentWeapon.state.bagSize <= reciever.Ammo.CurrentWeapon.magSize)
			{
				reciever.AddMagOnce = true;
				reciever.Ammo.CurrentWeapon.state.bagSize += reciever.Ammo.CurrentWeapon.magSize;
			}
			if (giver != reciever && giver.playerInfo.playerType == reciever.playerInfo.playerType)
			{
				float num = Vector3.Distance(giver.Position, reciever.Position);
				if (num < 1f && BIT.AND(giver.ClanBuffDistance, 134217728) && !reciever.AddMagOnce && reciever.Ammo.state.equiped == WeaponEquipedState.Primary && reciever.Ammo.CurrentWeapon.state.bagSize <= reciever.Ammo.CurrentWeapon.bagSize - reciever.Ammo.CurrentWeapon.magSize)
				{
					reciever.AddMagOnce = true;
					reciever.Ammo.CurrentWeapon.state.bagSize += reciever.Ammo.CurrentWeapon.magSize;
				}
				reciever.ClanCompositeBuff |= giver.ClanCompositeBuffItem;
				reciever.ClanCompositeBuff |= reciever.ClanCompositeBuffItem;
				if (reciever.ClanCompositeBuff == this.ClanBuffComposite)
				{
					reciever.playerInfo.buffs |= Buffs.clanAllProfs;
				}
				if (num < 2f && BIT.AND(giver.ClanBuffDistance, 67108864))
				{
					reciever.playerInfo.buffs |= Buffs.leader_regen;
				}
			}
		}
	}

	// Token: 0x06000D68 RID: 3432 RVA: 0x0009EEE0 File Offset: 0x0009D0E0
	public override void OnLateUpdate()
	{
		for (int i = 0; i < this.serverEntities.Count; i++)
		{
			if (this.serverEntities[i])
			{
				this.serverEntities[i].CallLateUpdate();
			}
		}
		this.iBearCount = 0;
		this.iUsecCount = 0;
		this.bearClanBuff = 0;
		this.usecClanBuff = 0;
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < this.serverNetPlayers.Count; j++)
		{
			if (!(this.serverNetPlayers[j] == null))
			{
				this.serverNetPlayers[j].CallLateUpdate();
				if (j >= this.serverNetPlayers.Count)
				{
					break;
				}
				if (!this.serverNetPlayers[j].IsSpectactor)
				{
					if (this.serverNetPlayers[j].IsBear)
					{
						this.iBearCount++;
					}
					if (!this.serverNetPlayers[j].IsBear)
					{
						this.iUsecCount++;
					}
				}
				if (this.tasksEnabled)
				{
					Buffs buffs = this.serverNetPlayers[j].playerInfo.buffs;
					bool flag = BIT.AND((int)buffs, 8388608);
					if (flag)
					{
						this.serverNetPlayers[j].playerInfo.buffs -= 8388608;
					}
					ServerVars.canFarmTasksCached = true;
				}
				else
				{
					this.serverNetPlayers[j].playerInfo.buffs |= Buffs.tasks_disabled;
					ServerVars.canFarmTasksCached = false;
				}
				if (this.isRated)
				{
					if (BIT.AND((int)this.serverNetPlayers[j].playerInfo.buffs, 4194304))
					{
						this.serverNetPlayers[j].playerInfo.buffs -= 4194304;
					}
					ServerVars.canFarmExpCached = true;
				}
				else
				{
					this.serverNetPlayers[j].playerInfo.buffs |= Buffs.exp_disabled;
					ServerVars.canFarmExpCached = false;
				}
				if (!this.serverNetPlayers[j].IsSpectactor && this.serverNetPlayers[j].IsAlive)
				{
					if (this.indexBuffGiver >= this.serverNetPlayers.Count)
					{
						this.indexBuffGiver = -1;
					}
					if (this.indexBuffGiver == -1)
					{
						this.indexBuffGiver = j;
					}
					if (this.serverNetPlayers[j] != null && this.serverNetPlayers[this.indexBuffGiver] != null && !Peer.Info.Hardcore && !ServerLeagueSystem.Enabled)
					{
						this.ClanBuffLogic(this.serverNetPlayers[j], this.serverNetPlayers[this.indexBuffGiver]);
					}
				}
				if (this.serverNetPlayers[j].IsSpectactor)
				{
					num2++;
				}
				else
				{
					num++;
				}
			}
		}
		this.indexBuffGiver++;
		if (this.indexBuffGiver > this.serverNetPlayers.Count - 1)
		{
			this.indexBuffGiver = 0;
		}
		Main.HostInfo.PlayerCount = num;
		Main.HostInfo.SpectactorCount = num2;
		Main.HostInfo.LoadPlayerCount = this.loadNetPlayers.Count;
		for (int k = 0; k < this.serverNetPlayers.Count; k++)
		{
			if (!(this.serverNetPlayers[k] == null))
			{
				if (!this.serverNetPlayers[k].ShouldDisconnect)
				{
					if (this.serverNetPlayers[k].IsBear)
					{
						this.serverNetPlayers[k].playerInfo.buffs |= (Buffs)this.bearClanBuff;
					}
					else
					{
						this.serverNetPlayers[k].playerInfo.buffs |= (Buffs)this.usecClanBuff;
					}
					if (this.serverNetPlayers[k].KickByIdle && this.serverNetPlayers[k].UserInfo.Permission < EPermission.Moder)
					{
						if (ServerLeagueSystem.Enabled)
						{
							ServerLeagueSystem.OnLeave(this.serverNetPlayers[k].UserID);
						}
						this.serverNetPlayers[k].DisconnectPlayer(Language.IdleKick, string.Empty);
						break;
					}
					if (this.serverNetPlayers[k].KickByPing && this.serverNetPlayers[k].UserInfo.Permission < EPermission.Moder)
					{
						string reason = Language.PingKickBody1 + Globals.I.BadPing + Language.PingKickBody2;
						this.serverNetPlayers[k].DisconnectPlayer(reason, string.Empty);
						break;
					}
					if (this.serverNetPlayers[k].Stats.teamKills >= 5 && Peer.HardcoreMode)
					{
						Peer.TemporaryBan(this.serverNetPlayers[k].UserID, 7200f);
						this.serverNetPlayers[k].DisconnectPlayer(Language.TeamKillKick, string.Empty);
						break;
					}
					if (!eNetwork.PlayerConnected(this.serverNetPlayers[k].NetworkPlayer))
					{
						Peer.ServerGame.PlayerDisconnected(this.serverNetPlayers[k], Language.ErrorNetworkProtocol);
						break;
					}
					if (this.serverNetPlayers[k].iChatMessageCounter > CVars.g_floodCounter)
					{
						Peer.ServerGame.PlayerDisconnected(this.serverNetPlayers[k], Language.FloodKick);
						break;
					}
				}
			}
		}
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x0009F4F4 File Offset: 0x0009D6F4
	private void ResendDataToClients(int disconnectedPlayerIndex)
	{
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (disconnectedPlayerIndex != i)
			{
				this.serverNetPlayers[i].ToClientPreload("SendDataOnConnect", new object[]
				{
					Encoding.UTF8.GetBytes(ServerLeagueSystem.GetMatchData())
				});
			}
		}
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x0009F558 File Offset: 0x0009D758
	public void BroadcastRPC(string name, BroadCastTo to, params object[] args)
	{
		bool flag = true;
		for (int i = 0; i < this.serverNetPlayers.Count; i++)
		{
			if (BIT.AND((short)to, 0) && !this.serverNetPlayers[i].IsAlive)
			{
				flag = false;
			}
			if (BIT.AND((short)to, 0) && this.serverNetPlayers[i].IsAlive)
			{
				flag = false;
			}
			if (BIT.AND((short)to, 0) && !this.serverNetPlayers[i].IsSpectactor)
			{
				flag = false;
			}
			if (BIT.AND((short)to, 0) && !this.serverNetPlayers[i].IsBear)
			{
				flag = false;
			}
			if (BIT.AND((short)to, 0) && this.serverNetPlayers[i].IsBear)
			{
				flag = false;
			}
			if (BIT.AND((short)to, 1))
			{
				flag = true;
			}
			if (flag)
			{
				this.serverNetPlayers[i].ToClient(name, args);
			}
		}
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x0009F660 File Offset: 0x0009D860
	private void SaveMasteringStats(List<ServerNetPlayer> players)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (ServerNetPlayer serverNetPlayer in players)
		{
			string key = serverNetPlayer.UserInfo.userID.ToString();
			if (!dictionary.ContainsKey(key))
			{
				dictionary.Add(key, serverNetPlayer.Stats.MasteringWeaponStats);
			}
		}
		string data = ArrayUtility.ToJSON<string, object>(dictionary);
		HtmlLayer.SendCompressed("adm.php?q=customization/player/save", data, delegate
		{
		}, delegate
		{
		});
	}

	// Token: 0x04000E29 RID: 3625
	protected List<ServerNetPlayer> serverNetPlayers = new List<ServerNetPlayer>();

	// Token: 0x04000E2A RID: 3626
	private List<ServerNetPlayer> loadNetPlayers = new List<ServerNetPlayer>();

	// Token: 0x04000E2B RID: 3627
	protected List<ServerEntity> serverEntities = new List<ServerEntity>();

	// Token: 0x04000E2C RID: 3628
	public int iBearCount;

	// Token: 0x04000E2D RID: 3629
	public int iUsecCount;

	// Token: 0x04000E2E RID: 3630
	private int bearWeight;

	// Token: 0x04000E2F RID: 3631
	private int usecWeight;

	// Token: 0x04000E30 RID: 3632
	private List<ServerNetPlayer> bearList = new List<ServerNetPlayer>();

	// Token: 0x04000E31 RID: 3633
	private List<ServerNetPlayer> usecList = new List<ServerNetPlayer>();

	// Token: 0x04000E32 RID: 3634
	private List<int> bearClanIdList = new List<int>();

	// Token: 0x04000E33 RID: 3635
	private List<int> usecClanIdList = new List<int>();

	// Token: 0x04000E34 RID: 3636
	private int newEntityID;

	// Token: 0x04000E35 RID: 3637
	private float matchStartTime;

	// Token: 0x04000E36 RID: 3638
	private int matches = CVars.n_maxMatches;

	// Token: 0x04000E37 RID: 3639
	private eTimer matchesTimer = new eTimer();

	// Token: 0x04000E38 RID: 3640
	public bool is_night;

	// Token: 0x04000E39 RID: 3641
	private BaseNetPlayer VIP;

	// Token: 0x04000E3A RID: 3642
	private PlayerType bearVIP = PlayerType.spectactor;

	// Token: 0x04000E3B RID: 3643
	private int indexBuffGiver = -1;

	// Token: 0x04000E3C RID: 3644
	protected int ClanBuffComposite;

	// Token: 0x04000E3D RID: 3645
	private Slot slot = new Slot();

	// Token: 0x04000E3E RID: 3646
	private DoOnce _delayedMatchStart = new DoOnce();

	// Token: 0x04000E3F RID: 3647
	private float _refreshAllLoadedTimer;

	// Token: 0x04000E40 RID: 3648
	private Timer _leagueMatchStartTimer = new Timer();
}
