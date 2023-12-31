using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using CW.Server.Statistic;
using LeagueSystem;
using UnityEngine;

// Token: 0x020002EE RID: 750
[AddComponentMenu("Scripts/Game/ServerNetPlayer")]
internal class ServerNetPlayer : BaseRpcNetPlayer
{
	// Token: 0x060014E1 RID: 5345 RVA: 0x000DC780 File Offset: 0x000DA980
	public override void OnPoolDespawn()
	{
		this.Hash = string.Empty;
		this.BestWeaponStatistic.Clear();
		this.spectator_timer = new eTimer();
		this.victimList = new Queue<int>();
		this.KickByIdle = false;
		this.KickByPing = false;
		this._pingTime = -1f;
		this.stats = null;
		this.userInfo = null;
		this.idle_timer = new eTimer();
		this.hitBody = null;
		this.cached_cmd.Clear();
		this.vip = false;
		this._totalTakenDamage = 0f;
		this.assistData.Clear();
		this._keepaliveDisconnected = false;
		this._keepaliveTimer = 5f;
		this.disconnectAfterSave = false;
		this.canSpawn = false;
		this.AddMagOnce = false;
		base.OnPoolDespawn();
		base.UserInfo = new UserInfo(false);
		this.Stats = new Stats();
		this.CheatButtons = 0;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x000DC868 File Offset: 0x000DAA68
	public override void OnPoolSpawn()
	{
		this._saveProfileTimer = Time.time + this._saveProfileTimout;
		base.UserInfo = new UserInfo(true);
		this.Stats = new Stats(true);
		this.hightlitedPlayers.Clear();
		base.OnPoolSpawn();
	}

	// Token: 0x17000326 RID: 806
	// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000DC8B0 File Offset: 0x000DAAB0
	public override bool AimSynchFromServer
	{
		get
		{
			return this.ammo.state.isAim;
		}
	}

	// Token: 0x17000327 RID: 807
	// (get) Token: 0x060014E4 RID: 5348 RVA: 0x000DC8C4 File Offset: 0x000DAAC4
	public float ObtainedExp
	{
		get
		{
			return this.Stats.obtainedXP;
		}
	}

	// Token: 0x17000328 RID: 808
	// (get) Token: 0x060014E5 RID: 5349 RVA: 0x000DC8D4 File Offset: 0x000DAAD4
	// (set) Token: 0x060014E6 RID: 5350 RVA: 0x000DC8DC File Offset: 0x000DAADC
	public Stats Stats
	{
		get
		{
			return this.stats;
		}
		set
		{
			this.stats = value;
		}
	}

	// Token: 0x17000329 RID: 809
	// (get) Token: 0x060014E7 RID: 5351 RVA: 0x000DC8E8 File Offset: 0x000DAAE8
	public MatchResultInfo matchInfo
	{
		get
		{
			MatchResultInfo matchResultInfo = MatchResultInfo.none;
			if (Peer.ServerGame.Top1InMatch() == this)
			{
				matchResultInfo |= MatchResultInfo.FirstPlace;
			}
			if (Peer.ServerGame.TopNMinus1InMatch() == this)
			{
				matchResultInfo |= MatchResultInfo.LastPlace;
			}
			if (Main.IsTeamGame)
			{
				if (base.IsBear ^ !Peer.ServerGame.BearWin)
				{
					this.stats.wins++;
					matchResultInfo |= MatchResultInfo.TeamWin;
					base.UserInfo.IsWinner = true;
				}
				if (base.IsBear ^ Peer.ServerGame.BearWin)
				{
					this.stats.loses++;
					matchResultInfo |= MatchResultInfo.TeamLose;
				}
			}
			return matchResultInfo;
		}
	}

	// Token: 0x1700032A RID: 810
	// (get) Token: 0x060014E8 RID: 5352 RVA: 0x000DC9A0 File Offset: 0x000DABA0
	public MatchResultData matchResultData
	{
		get
		{
			MatchResultData matchResultData = new MatchResultData(this);
			if (!base.IsSpectactor)
			{
				matchResultData.record = this.stats.StreaksIndexes;
				matchResultData.recordValues = this.stats.StreaksArray;
			}
			return matchResultData;
		}
	}

	// Token: 0x1700032B RID: 811
	// (get) Token: 0x060014E9 RID: 5353 RVA: 0x000DC9E4 File Offset: 0x000DABE4
	public bool ShouldDisconnect
	{
		get
		{
			return this.disconnectAfterSave;
		}
	}

	// Token: 0x1700032C RID: 812
	// (get) Token: 0x060014EA RID: 5354 RVA: 0x000DC9EC File Offset: 0x000DABEC
	public int Loading
	{
		get
		{
			return this.playerInfo.loading;
		}
	}

	// Token: 0x1700032D RID: 813
	// (get) Token: 0x060014EB RID: 5355 RVA: 0x000DC9FC File Offset: 0x000DABFC
	// (set) Token: 0x060014EC RID: 5356 RVA: 0x000DCA18 File Offset: 0x000DAC18
	public bool CanSpawn
	{
		get
		{
			return this.canSpawn && !this.ShouldDisconnect;
		}
		set
		{
			this.canSpawn = value;
		}
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x000DCA24 File Offset: 0x000DAC24
	public override void Serialize(eNetworkStream stream)
	{
		base.Serialize(stream);
		this.update = this.update << 28 >> 28;
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x000DCA40 File Offset: 0x000DAC40
	public void PlayerDisconnected(string reason)
	{
		try
		{
			if (reason != Language.UserQuited)
			{
				eNetwork.RPC("OpenConnectionFailed", this.networkPlayer, new object[]
				{
					reason
				});
			}
			else
			{
				base.ToClient("QuitFromServer", new object[0]);
			}
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
		base.StartCoroutine(this.Disconnect(reason));
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x000DCAC8 File Offset: 0x000DACC8
	[Obfuscation(Exclude = true)]
	private IEnumerator Disconnect(string reason)
	{
		yield return new WaitForSeconds(2f);
		this.DespawnPlayerObject();
		this.KillPlayer();
		Peer.ServerGame.RemovePlayer(this);
		base.DisableViews();
		base.DestroyViews();
		eNetwork.CloseConnection(this.networkPlayer, string.Empty, reason);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.1f));
		PoolManager.Despawn(base.gameObject);
		yield break;
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x000DCAF4 File Offset: 0x000DACF4
	protected override void Send(eNetworkStream stream)
	{
		this.playerInfo.points = (int)this.stats.obtainedXP;
		this.playerInfo.killCount = this.stats.kills;
		this.playerInfo.deathCount = this.stats.deaths;
		stream.ID = this.ID;
		Peer.ServerGame.Serialize(stream);
		this.PacketNum++;
		stream.Serialize(ref this.PacketNum);
	}

	// Token: 0x060014F1 RID: 5361 RVA: 0x000DCB7C File Offset: 0x000DAD7C
	protected override void Recieve(eNetworkStream stream)
	{
		short f = 0;
		stream.Serialize(ref f);
		if (BIT.AND((int)f, 4))
		{
			stream.Serialize(ref this.playerInfo.loading);
		}
		if (BIT.AND((int)f, 2))
		{
			this.UC.Deserialize(stream);
		}
		stream.Serialize(ref this.playerState);
		if (base.Ammo != null)
		{
			int num = this.playerState >> 5;
			if (!base.Ammo.state.supportReady && num != 0 && num != (int)base.Ammo.state.equiped)
			{
				base.Ammo.state.equiped = (WeaponEquipedState)num;
				base.Ammo.BreakReload();
			}
			if (this.controller)
			{
				this.controller.state.isSeat = ((this.playerState & 4) != 0);
			}
			base.Ammo.AutoChangeAimMode((this.playerState & 2) != 0);
		}
	}

	// Token: 0x060014F2 RID: 5362 RVA: 0x000DCC84 File Offset: 0x000DAE84
	private void ActionDisconnect()
	{
		this.FreeToActions = false;
		this.DisconnectPlayer(Language.UserQuited, string.Empty);
	}

	// Token: 0x060014F3 RID: 5363 RVA: 0x000DCCA0 File Offset: 0x000DAEA0
	private void ActionSave()
	{
		this.FreeToActions = false;
		if (this.ProfileLoaded && this._saveProfileTimer < Time.time)
		{
			this._saveProfileTimer = Time.time + this._saveProfileTimout;
			string text = this.ProfileToJson();
			if (CVars.debugMode)
			{
				global::Console.print(text);
			}
			string str = "?action=idsave&user_id=" + base.UserInfo.userID;
			if (CVars.IsStandaloneRealm)
			{
				str += "&platform=standalone";
			}
			HtmlLayer.SendCompressed(str, text, new RequestFinished(this.OnProfileSaved), new RequestFailed(this.OnFailedRequest));
		}
		else
		{
			this.FreeToActions = true;
			base.ToClient("UpdateInfo", new object[0]);
			if (this.ShouldDisconnect || this.disconnectAfterSave)
			{
				this.PlayerDisconnected(Language.UserQuited);
			}
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000DCD84 File Offset: 0x000DAF84
	private void ActionLoad()
	{
		this.FreeToActions = false;
		this.canSpawn = false;
		this.playerInfo.buffs |= Buffs.db_load;
		Int loadSentId = base.UserInfo.userID;
		HtmlLayer.RequestCompressed("?action=idload&user_id=" + base.UserInfo.userID + ((!string.IsNullOrEmpty(this.Hash)) ? ("&hash=" + this.Hash) : string.Empty), delegate(string json, string url)
		{
			if (this.UserInfo.userID == loadSentId)
			{
				this.OnJsonLoaded(json, url);
			}
		}, delegate(Exception exception, string url)
		{
			if (this.UserInfo.userID == loadSentId)
			{
				this.OnFailedRequest(exception, url);
			}
		}, string.Empty, string.Empty);
	}

	// Token: 0x060014F5 RID: 5365 RVA: 0x000DCE3C File Offset: 0x000DB03C
	protected override void OnEnable()
	{
		base.OnEnable();
		if (this.UC != null)
		{
			this.UC = new ServerCmdCollector();
		}
	}

	// Token: 0x060014F6 RID: 5366 RVA: 0x000DCE5C File Offset: 0x000DB05C
	public void Disconnect()
	{
		Debug.Log("ServerNetPlayer.Disconnect");
		this.DespawnPlayerObject();
		try
		{
			this.KillPlayer();
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
		Peer.ServerGame.RemovePlayer(this);
		base.DisableViews();
		base.DestroyViews();
		if (this._keepaliveDisconnected)
		{
			string serverDisconnetKeepAliveError = Language.ServerDisconnetKeepAliveError;
			eNetwork.CloseConnection(this.networkPlayer, string.Empty, serverDisconnetKeepAliveError);
		}
		else
		{
			eNetwork.CloseConnection(this.networkPlayer, string.Empty, string.Empty);
		}
		PoolManager.Despawn(base.gameObject);
		this.ProfileLoaded = false;
		this.FreeToActions = true;
		this.actions.Clear();
	}

	// Token: 0x060014F7 RID: 5367 RVA: 0x000DCF24 File Offset: 0x000DB124
	public void DelayDisconnect(float time)
	{
		base.Invoke("Disconnect", time);
		base.ToClient("QuitFromServer", new object[0]);
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x000DCF44 File Offset: 0x000DB144
	public void InformPlayer(string reason, string title = "")
	{
		try
		{
			eNetwork.CloseConnection(this.networkPlayer, title, reason);
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x000DCF8C File Offset: 0x000DB18C
	public void DisconnectPlayer(string reason, string title = "")
	{
		try
		{
			eNetwork.RPC("OpenConnectionFailed", this.networkPlayer, new object[]
			{
				reason
			});
		}
		catch (Exception e)
		{
			global::Console.exception(e);
		}
		this.DespawnPlayerObject();
		this.KillPlayer();
		Peer.ServerGame.RemovePlayer(this);
		base.DisableViews();
		base.DestroyViews();
		eNetwork.CloseConnection(this.networkPlayer, title, reason);
		PoolManager.Despawn(base.gameObject);
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000DD01C File Offset: 0x000DB21C
	public bool UnlockSkill(Skills skill)
	{
		return base.UserInfo != null && base.UserInfo.skillsInfos[(int)skill].Unlocked;
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x000DD040 File Offset: 0x000DB240
	public bool UnlockClanSkill(Cl_Skills clanSkill)
	{
		return !Peer.HardcoreMode && (base.UserInfo != null && (Cl_Skills)base.UserInfo.ClanSkillsInfos.Length > clanSkill) && base.UserInfo.ClanSkillsInfos[(int)clanSkill].IsUnlocked;
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x000DD08C File Offset: 0x000DB28C
	public void SaveAndDisconnect(string reason)
	{
		if (this.ShouldDisconnect)
		{
			return;
		}
		if (base.IsSpectactor)
		{
			if (Peer.HardcoreMode)
			{
				this.stats.timeHardcore += (int)(Peer.ServerGame.ServerTime - this.stats.timeStart);
			}
			this.stats.timeOnline += (int)(Peer.ServerGame.ServerTime - this.stats.timeStart);
			this.stats.timeStart = Peer.ServerGame.ServerTime;
		}
		ContractsEngine.ClearPointsAtEarnExpMatch(this);
		this.DespawnPlayerObject();
		this.KillPlayer();
		this.disconnectAfterSave = true;
		this.SaveProfile();
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000DD140 File Offset: 0x000DB340
	public void DBFail(Exception e, string url)
	{
		this.PlayerDisconnected(Language.LevelFailure);
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000DD150 File Offset: 0x000DB350
	public void SaveProfile()
	{
		if (ServerLeagueSystem.Enabled)
		{
			base.ToClient("UpdateInfo", new object[0]);
			if (this.ShouldDisconnect || this.disconnectAfterSave)
			{
				this.PlayerDisconnected(Language.UserQuited);
			}
		}
		else if (base.UserInfo.ProfileLoaded)
		{
			this.actions.Enqueue(new Action(this.ActionSave));
		}
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000DD1C8 File Offset: 0x000DB3C8
	public void LoadProfile(bool FirstTime = false)
	{
		if (FirstTime)
		{
			this.actions.Clear();
			this.ActionLoad();
		}
		else
		{
			this.actions.Enqueue(new Action(this.ActionLoad));
		}
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x000DD200 File Offset: 0x000DB400
	public void DisconnectPlayer()
	{
		if (base.UserInfo.ProfileLoaded && !ServerLeagueSystem.Enabled)
		{
			this.actions.Enqueue(new Action(this.ActionSave));
		}
		this.actions.Enqueue(new Action(this.ActionDisconnect));
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000DD258 File Offset: 0x000DB458
	private string ProfileToJson()
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		base.UserInfo.userStats.AddDelta(this.Stats);
		int currentLevel = base.UserInfo.currentLevel;
		this.Stats.obtainedXP = ((this.Stats.obtainedXP >= 0f) ? this.Stats.obtainedXP : 0f);
		if (Peer.HardcoreMode)
		{
			base.UserInfo.hcCurrentXP += this.Stats.obtainedXP;
			base.UserInfo.currentXP += this.Stats.obtainedXP;
			base.UserInfo.hcKillCount += this.Stats.kills;
			base.UserInfo.hcDeathCount += this.Stats.deaths;
			base.UserInfo.hcWinCount += this.Stats.wins;
			base.UserInfo.hcLossCount += this.Stats.loses;
		}
		else
		{
			base.UserInfo.currentXP += this.Stats.obtainedXP;
			base.UserInfo.killCount += this.Stats.kills;
			base.UserInfo.deathCount += this.Stats.deaths;
			base.UserInfo.winCount += this.Stats.wins;
			base.UserInfo.lossCount += this.Stats.loses;
		}
		base.UserInfo.userStats.killDelta = this.Stats.kills;
		if (base.UserInfo.currentLevel > currentLevel)
		{
			base.UserInfo.SP += this.Stats.obtainedSP;
		}
		base.UserInfo.IsWinner = (this.Stats.IsWinner == 1);
		float obtainedXP = this.Stats.obtainedXP;
		int num = Math.Max(Mathf.CeilToInt(this.Stats.obtainedXP * Main.GameModeInfo.incomeCoef) + this.Stats.achivCR, 0);
		if (Peer.ServerGame.is_night && base.UserInfo.skillUnlocked(Skills.car_night))
		{
			num += Math.Max(Mathf.CeilToInt(this.Stats.obtainedXP * Main.GameModeInfo.incomeCoef * 0.3f), 0);
		}
		base.UserInfo.DeltaCr = (float)num;
		base.UserInfo.DeltaExp = obtainedXP;
		base.UserInfo.CR += num;
		string result = base.UserInfo.ToJSON(true);
		base.UserInfo.userStats.killDelta = 0;
		return result;
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000DD590 File Offset: 0x000DB790
	private void OnProfileSaved(string result, string url)
	{
		if (CVars.n_httpDebug && base.UserInfo != null)
		{
			global::Console.print("NetPlayer " + base.UserInfo.userID + " Save Finished");
		}
		this.playerInfo.buffs -= 512;
		base.ToClient("UpdateInfo", new object[0]);
		if (this.ShouldDisconnect)
		{
			this.PlayerDisconnected(Language.UserQuited);
		}
		else
		{
			this.CanSpawn = true;
		}
		if (this.disconnectAfterSave)
		{
			this.PlayerDisconnected(Language.UserQuited);
			return;
		}
		this.Stats = new Stats(true);
		this.FreeToActions = true;
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000DD648 File Offset: 0x000DB848
	private void OnJsonLoaded(string json, string url)
	{
		if (ServerLoad.ErrorTest)
		{
			json = null;
		}
		Dictionary<string, object> dictionary;
		try
		{
			dictionary = ArrayUtility.FromJSON(json, string.Empty);
		}
		catch (Exception innerException)
		{
			global::Console.exception(new Exception("Parse JSON Failed \n url = " + url + "\n", innerException));
			this.PlayerDisconnected(Language.ServerDisconnect);
			return;
		}
		if (dictionary.ContainsKey("msg"))
		{
			Debug.Log("ServerNetPlayer.OnJsonLoaded: msg = " + dictionary["msg"]);
		}
		if (dictionary.ContainsKey("result"))
		{
			if ((int)dictionary["result"] != 0)
			{
				Debug.Log("ServerNetPlayer.OnJsonLoaded: result = " + dictionary["result"]);
				global::Console.exception(new Exception("DataServer Error Bad Result"));
				this.PlayerDisconnected(Language.ServerDisconnect);
			}
		}
		else
		{
			global::Console.exception(new Exception("DataServer Error Bad Result"));
			this.PlayerDisconnected(Language.ServerDisconnect);
		}
		base.UserInfo.weaponsStates = new WeaponInfo[Globals.I.weapons.Length];
		for (int i = 0; i < base.UserInfo.weaponsStates.Length; i++)
		{
			base.UserInfo.weaponsStates[i] = new WeaponInfo();
			base.UserInfo.weaponsStates[i].Init(true, i);
		}
		base.UserInfo.Read(dictionary, true);
		HtmlLayer.Request("adm.php?q=customization/server/load/" + base.UserInfo.userID, new RequestFinished(this.OnCustomizationLoaded), delegate(Exception exception, string s)
		{
			Debug.LogException(exception);
		}, string.Empty, string.Empty);
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x000DD81C File Offset: 0x000DBA1C
	private void OnCustomizationLoaded(string text, string s)
	{
		base.UserInfo.Mastering.Initialize(text);
		this.playerInfo.Camouflages = ArrayUtility.ToJSON<int, int>(base.UserInfo.Mastering.Camouflages);
		this.playerInfo.level = base.UserInfo.currentLevel;
		this.playerInfo.Nick = base.UserInfo.nick;
		this.playerInfo.NickColor = base.UserInfo.nickColor;
		this.playerInfo.IsSuspected = base.UserInfo.isSuspected;
		this.playerInfo.clanTag = base.UserInfo.clanTag;
		this.playerInfo.skillsInfos = base.UserInfo.skillArray;
		this.playerInfo.playerClass = base.UserInfo.playerClass;
		if (!ServerLeagueSystem.Enabled)
		{
			this.playerInfo.clanSkillsInfos = base.UserInfo.clanSkillArray;
		}
		else
		{
			this.playerInfo.clanSkillsInfos = new bool[0];
		}
		this.playerInfo.isModerator = (base.UserInfo.Permission == EPermission.Moder);
		if (this.playerInfo.Nick == "Player")
		{
			PlayerInfo playerInfo = this.playerInfo;
			playerInfo.Nick += base.UserInfo.userID.ToString();
		}
		if (this.playerInfo.Nick == "Guest")
		{
			PlayerInfo playerInfo2 = this.playerInfo;
			playerInfo2.Nick += this.playerInfo.playerID.ToString();
		}
		this.Stats = new Stats(true);
		if (base.UserInfo.banned != 0)
		{
			this.PlayerDisconnected(Language.ServerLoadOnFail0);
			return;
		}
		if (base.UserInfo.userID != IDUtil.BotID && base.UserInfo.Permission <= EPermission.Tester && (Main.HostInfo.Name == "UnityTestServer" || Main.HostInfo.Name.Contains("Developer")))
		{
			return;
		}
		if (base.UserInfo.Permission != EPermission.Moder)
		{
			if (base.UserInfo.userID == IDUtil.GuestID && Main.IsTargetDesignation)
			{
				this.PlayerDisconnected(Language.ServerLoadOnFail2);
				return;
			}
			if (!Peer.Info.MyLevelRange(this.playerInfo.level, false) && base.UserInfo.Permission <= EPermission.Tester)
			{
				if (!Peer.HardcoreMode)
				{
					this.PlayerDisconnected(Language.ServerLoadOnFail4);
					return;
				}
				if (!base.UserInfo.skillsInfos[52].Unlocked)
				{
					this.PlayerDisconnected(Language.ServerLoadOnFail4);
					return;
				}
			}
		}
		this.playerInfo.buffs -= 256;
		if (!this.playerInfo.isModerator)
		{
			Peer.ServerGame.EventMessage(this.playerInfo.Nick, ChatInfo.network_message, Language.PlayerConnected);
		}
		this.ProfileLoaded = true;
		Peer.ServerGame.AddPlayer(this);
		base.GetComponent<PoolItem>().CancelInvoke("Despawn");
		eNetwork.RPC("OpenConnectionFinished", base.NetworkPlayer, new object[]
		{
			base.UserID,
			base.TargetID,
			base.MyID,
			this.ID,
			base.Group,
			Peer.ServerGame.ServerTime,
			SingletoneComponent<Main>.Instance.commandLineArgs.HasValue("--snow")
		});
		this.CanSpawn = true;
		this.FreeToActions = true;
		if (ServerLeagueSystem.Enabled)
		{
			base.ToClientPreload("SendDataOnConnect", new object[]
			{
				Encoding.UTF8.GetBytes(ServerLeagueSystem.GetMatchData())
			});
		}
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x000DDC2C File Offset: 0x000DBE2C
	private void OnFailedRequest(Exception e, string url)
	{
		this.FreeToActions = true;
		Debug.Log(url + "\n" + e);
		if (!this.ProfileLoaded || CVars.KickAtDBFail)
		{
			this.DisconnectPlayer();
		}
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x000DDC64 File Offset: 0x000DBE64
	protected override void KillPlayer()
	{
		base.KillPlayer();
		this.SetRespawnTime();
		Peer.ServerGame.DespawnVIP(this);
		this.vip = false;
		this.addHealthOnceByLife = false;
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000DDC98 File Offset: 0x000DBE98
	protected void SetRespawnTime()
	{
		if (this._setRespawnTimeFirstTime)
		{
			this._setRespawnTimeFirstTime = false;
			return;
		}
		bool flag = base.UserInfo.skillUnlocked(Skills.car_respawn);
		float num;
		if (Main.IsTeamElimination)
		{
			num = 5f;
			if (flag)
			{
				num /= 2f;
			}
		}
		else
		{
			num = 3f;
			if (flag)
			{
				num /= 2f;
			}
		}
		this._respawnTime = HtmlLayer.serverUtc + (int)num;
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000DDD10 File Offset: 0x000DBF10
	public void BaseServerSpawn()
	{
		Debug.Log(this + " BaseRpcNetPlayer.BaseServerSpawn " + this.ID);
		if (Peer.ServerGame.MatchState != MatchState.round_going && Peer.ServerGame.MatchState != MatchState.alone)
		{
			return;
		}
		bool flag = Peer.ServerGame.IsRounedGame || this._respawnTime - HtmlLayer.serverUtc < 0;
		if (base.IsAlive || base.IsSpectactor || !flag)
		{
			return;
		}
		Spawn spawnPoint = Peer.ServerGame.getSpawnPoint(base.IsBear);
		float num = 0f;
		float num2 = 0f;
		if (this.SecondaryIndex != 127)
		{
			num = base.UserInfo.weaponsStates[this.SecondaryIndex].repair_info;
		}
		if (this.PrimaryIndex != 127)
		{
			num2 = base.UserInfo.weaponsStates[this.PrimaryIndex].repair_info;
		}
		this.SpawnPlayerObject(spawnPoint.pos, spawnPoint.euler, this.SecondaryIndex, this.PrimaryIndex, this.SecondaryMod, this.PrimaryMod, num, num2, this.SecondaryMods, this.PrimaryMods, this.WeaponKit);
		base.ToClient("SpawnFromServer", new object[]
		{
			(int)this.playerInfo.playerType,
			spawnPoint.pos,
			spawnPoint.euler,
			this.SecondaryIndex,
			this.PrimaryIndex,
			this.SecondaryMod,
			this.PrimaryMod,
			num,
			num2,
			this.SecondaryMods,
			this.PrimaryMods,
			this.WeaponKit
		});
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x000DDEEC File Offset: 0x000DC0EC
	protected override void RevivePlayer()
	{
		base.RevivePlayer();
		if (Peer.ServerGame.CanSpawnAsVIP(this))
		{
			base.Health = (float)CVars.VIP_health;
			this.vip = true;
			this.Radio(RadioEnum.youreVIP);
			Peer.ServerGame.Radio(RadioEnum.viponsite);
			Peer.ServerGame.VIPSpawned(this);
			if (Peer.Info.TestVip)
			{
				base.EnableTestVipParams();
			}
		}
		this.spectator_timer.Stop();
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x000DDF64 File Offset: 0x000DC164
	protected override void DespawnPlayerObject()
	{
		this.FreeToActions = true;
		this.idle_timer.Stop();
		this.spectator_timer.Stop();
		this.assistData.Clear();
		this._totalTakenDamage = 0f;
		if (this.hitBody)
		{
			this.hitBody = null;
		}
		if (this.animationsThird)
		{
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Spine1").GetComponent<DamageX>().Armor = false;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Pelvis").GetComponent<DamageX>().Armor = false;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Head").GetComponent<DamageX>().Armor = false;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R UpperArm").GetComponent<DamageX>().Armor = false;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L UpperArm").GetComponent<DamageX>().Armor = false;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_Head").GetComponent<DamageX>().X = 10f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Calf ").GetComponent<DamageX>().X = 0.6f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Thigh").GetComponent<DamageX>().X = 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Calf ").GetComponent<DamageX>().X = 0.6f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Thigh").GetComponent<DamageX>().X = 0.8f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L UpperArm").GetComponent<DamageX>().X = 0.7f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_L Forearm").GetComponent<DamageX>().X = 0.5f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R UpperArm").GetComponent<DamageX>().X = 0.7f;
			Utility.FindHierarchy(this.PlayerTransform, "NPC_R Forearm").GetComponent<DamageX>().X = 0.5f;
		}
		base.DespawnPlayerObject();
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000DE170 File Offset: 0x000DC370
	protected override void SpawnPlayerObject(Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
		this.DespawnPlayerObject();
		base.PlayerObject = SingletoneForm<PoolManager>.Instance["server_player"].Spawn();
		this.PlayerTransform.parent = base.transform;
		this.animationsThird = base.PlayerObject.GetComponentInChildren<AnimationsThird>();
		this.animationsThird.Init(this);
		this.mainCamera = this.animationsThird.GetComponentInChildren<Camera>();
		DamageX[] componentsInChildren = this.animationsThird.GetComponentsInChildren<DamageX>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].player = this;
		}
		base.SpawnPlayerObject(pos, euler, secondaryIndex, primaryIndex, secondaryMod, primaryMod, secondary_repair_info, primary_repair_info, secondaryMods, primaryMods, weaponKit);
		this.hitBody = base.PlayerObject.GetComponent<HitBody>();
		if (base.Ammo.cPrimary != null)
		{
			this.fullBag = base.Ammo.cPrimary.bagSize;
		}
		this.AddMagOnce = false;
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000DE264 File Offset: 0x000DC464
	public void RaycastAll(Ray ray, float time, List<eRaycastHit> hits)
	{
		if (this.hitBody != null)
		{
			this.hitBody.RaycastAll(ray, time, hits);
		}
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000DE288 File Offset: 0x000DC488
	public void Placement(bool difuse)
	{
		if (this.UInput.GetKey(UKeyCode.interaction, true))
		{
			Peer.ServerGame.Placement.StartBeacon(this, difuse);
			Peer.ServerGame.Placement.Advance(this, difuse);
		}
		else
		{
			Peer.ServerGame.Placement.StopBeacon(this, difuse);
		}
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x000DE2E4 File Offset: 0x000DC4E4
	public bool PlayerHit(float damage, float armor_damage, ServerNetPlayer player, BaseWeapon weapon = null)
	{
		if (player == null)
		{
			return false;
		}
		if (base.IsDeadOrSpectactor)
		{
			return false;
		}
		if (Main.IsTeamGame && !base.IsTeam(player))
		{
			bool flag = weapon != null;
			if (this != player)
			{
				this.lastHitted = player;
			}
			if (this.assistData.Count > 10)
			{
				this.assistData.Clear();
			}
			if (!this.assistData.ContainsKey(player))
			{
				this.assistData.Add(player, new DamageByWeapon
				{
					Damage = damage,
					Type = ((!flag) ? Weapons.none : weapon.type),
					IsModable = (flag && weapon.IsModable)
				});
			}
			else
			{
				this.assistData[player].Damage += damage;
			}
			this._totalTakenDamage += damage;
		}
		else if (this != player)
		{
			this.lastHitted = player;
		}
		if (!base.Immortal)
		{
			base.Armor -= armor_damage;
			base.Health -= damage;
		}
		float num = 1.3f + Mathf.Min(damage, 200f) * 4f / 100f;
		if (player.playerInfo.skillUnlocked(Skills.stop_ammo))
		{
			num *= 2f;
		}
		if (this.playerInfo.skillUnlocked(Skills.stopper))
		{
			num /= 2f;
		}
		num = Mathf.Min(num, 4.5f);
		this.animationsThird.PlayHit();
		foreach (ServerNetPlayer serverNetPlayer in Peer.ServerGame.ServerNetPlayers)
		{
			serverNetPlayer.ToClient("PlayHit", new object[]
			{
				player.ID,
				this.ID,
				base.Health,
				base.Armor,
				num
			});
		}
		return base.Health < 1f;
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000DE554 File Offset: 0x000DC754
	public void Kill(ServerNetPlayer killer, int method, bool headShot, int killedId, string boneToBeWild, Vector3 boneForce, int weaponKills = 0)
	{
		int killerId = killer.ID;
		killer.BestWeaponStatistic.GatherStatistic(method);
		if (Main.IsTargetDesignation && Peer.ServerGame.MatchState == MatchState.round_going)
		{
			Peer.ServerGame.Placement.StopBeacon(this, !base.IsTeam(Main.GameModeInfo.isBearPlacement));
		}
		this.DespawnPlayerObject();
		this.KillPlayer();
		base.ToClient("KillFromServer", new object[]
		{
			boneToBeWild,
			boneForce
		});
		Peer.ServerGame.KillInfo(killerId, method, headShot, killedId, boneToBeWild, boneForce, weaponKills);
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000DE5FC File Offset: 0x000DC7FC
	public void RaiseWtask(int index, int count, bool farmDetected = false)
	{
		base.ToClient("RaiseWtaskFromServer", new object[]
		{
			index,
			count,
			farmDetected
		});
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000DE638 File Offset: 0x000DC838
	public void RaiseAchievement(int index, int count, int money, bool farmDetected = false)
	{
		if (money >= 0)
		{
			this.stats.achivCR += money;
		}
		base.ToClient("RaiseAchievementFromServer", new object[]
		{
			index,
			count,
			farmDetected
		});
		if (count == this.userInfo.achievementsInfos[index].count)
		{
			Peer.ServerGame.EventMessage(base.Nick, ChatInfo.gameflow_message, Language.PlayerGainedAchivment + " " + this.userInfo.achievementsInfos[index].name.ToLower());
		}
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000DE6DC File Offset: 0x000DC8DC
	public void RaiseContract(ContractTaskType difficulty, int taskCount, bool farmDetected = false)
	{
		base.ToClient("RaiseContractFromServer", new object[]
		{
			(int)difficulty,
			taskCount,
			farmDetected
		});
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000DE718 File Offset: 0x000DC918
	public void ArmStreak(ArmstreakEnum armStreak)
	{
		this.stats.armstreaksEarned++;
		if (armStreak == ArmstreakEnum.mortar)
		{
			this.playerInfo.hasMortar = true;
		}
		if (armStreak == ArmstreakEnum.sonar)
		{
			this.playerInfo.hasSonar = true;
		}
		if (armStreak == ArmstreakEnum.grenade)
		{
			this.ammo.state.grenadeCount++;
		}
		base.ToClient("ArmStreakFromServer", new object[]
		{
			(int)armStreak
		});
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000DE798 File Offset: 0x000DC998
	public void KillInfo(int killerId, int method, bool headShot, int killedId, string boneToBeWild, Vector3 boneForce, int weaponKills)
	{
		base.ToClient("KillInfoFromServer", new object[]
		{
			killerId,
			method,
			headShot,
			killedId,
			boneToBeWild,
			boneForce,
			weaponKills
		});
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000DE7F4 File Offset: 0x000DC9F4
	public void KillStreak(KillStreakEnum killStreak)
	{
		base.ToClient("KillStreakFromServer", new object[]
		{
			(int)killStreak
		});
	}

	// Token: 0x06001516 RID: 5398 RVA: 0x000DE810 File Offset: 0x000DCA10
	public void CaptureScreen(string reporterNick, int reporterID)
	{
		base.ToClient("CaptureScreenFromServer", new object[]
		{
			reporterNick,
			reporterID
		});
	}

	// Token: 0x06001517 RID: 5399 RVA: 0x000DE830 File Offset: 0x000DCA30
	public void AddExpClean(float exp)
	{
		float exp2 = base.UserInfo.currentXP.Value + this.stats.obtainedXP;
		float num = base.UserInfo.currentXP.Value + this.stats.obtainedXP + exp;
		this.stats.obtainedXP += exp;
		if (this.stats.obtainedXP < 0f)
		{
			this.stats.obtainedXP = 0f;
		}
		if (ServerVars.canFarmTasksCached || base.UserInfo.currentLevel < 10)
		{
			ContractsEngine.OnEarnExp(this, Peer.Info.GameMode, (int)exp, (int)this.stats.obtainedXP, (Maps)Main.HostInfo.MapIndex);
		}
		base.ToClient("ExpFromServer", new object[]
		{
			num,
			exp,
			0
		});
		if (base.UserInfo.getLevel(num) != base.UserInfo.getLevel(exp2))
		{
			int level = this.playerInfo.level;
			this.playerInfo.level = base.UserInfo.getLevel(num);
			int num2 = this.playerInfo.level - level;
			for (int i = level + 1; i < this.playerInfo.level + 1; i++)
			{
				if (base.UserInfo.skillUnlocked(Skills.car_sp) && i % 5 == 0)
				{
					num2++;
				}
			}
			this.stats.obtainedSP += num2;
			base.ToClient("LevelUpFromServer", new object[]
			{
				num,
				base.UserInfo.SP.Value + this.stats.obtainedSP,
				num2
			});
		}
	}

	// Token: 0x06001518 RID: 5400 RVA: 0x000DEA1C File Offset: 0x000DCC1C
	public float Exp(float exp, float bonusExp = 0f, bool useExpMult = true)
	{
		if ((!ServerVars.canFarmExpCached && this.userInfo.currentLevel > 9) || base.UserInfo == null)
		{
			return 0f;
		}
		if (Peer.HardcoreMode)
		{
			exp *= CVars.g_hardcorexpCoef;
		}
		float num = this.stats.obtainedXP + base.UserInfo.currentXP.Value;
		if (Main.IsTeamElimination)
		{
			if (Peer.Info.TestVip)
			{
				if (Peer.ServerGame.Vip != null && Peer.ServerGame.Vip.IsTeam(this))
				{
					exp *= Mathf.Max(1f, Peer.ServerGame.Vip.LifeTimeExpCoef);
				}
			}
			else if (BIT.AND((int)this.playerInfo.buffs, 65536) && exp > 0f)
			{
				exp *= CVars.g_isDefenderExpMult;
			}
		}
		if (BIT.AND((int)this.playerInfo.buffs, 16777216) && exp > 0f)
		{
			exp *= CVars.g_ClanLeaderExpBuffMult;
		}
		if (exp > 0f)
		{
			float num2 = 1f;
			bool flag = Environment.StackTrace.IndexOf("kill", StringComparison.OrdinalIgnoreCase) >= 0;
			if (base.UserInfo.skillUnlocked(Skills.car_health) && base.Health > 0f && base.Health < 20f && flag)
			{
				num2 = (1f - base.Health / 20f) * 1.1f + 1.1f;
			}
			exp = num2 * exp * CVars.g_globalExpMult * Main.GameModeInfo.expCoef * (float)this.expMult;
			bonusExp = num2 * bonusExp * CVars.g_globalExpMult * Main.GameModeInfo.expCoef * (float)this.expMult;
		}
		else if (num + exp + bonusExp < base.UserInfo.minXP(num))
		{
			exp = base.UserInfo.minXP(num) - num;
			bonusExp = 0f;
		}
		this.stats.obtainedXP = this.stats.obtainedXP + exp + bonusExp;
		if (this.stats.obtainedXP < 0f)
		{
			this.stats.obtainedXP = 0f;
		}
		float num3 = num + exp + bonusExp;
		if (ServerVars.canFarmTasksCached || base.UserInfo.currentLevel < 10)
		{
			ContractsEngine.OnEarnExp(this, Peer.Info.GameMode, (int)(exp + bonusExp), (int)this.stats.obtainedXP, (Maps)Main.HostInfo.MapIndex);
		}
		base.ToClient("ExpFromServer", new object[]
		{
			num3,
			exp,
			bonusExp
		});
		if (base.UserInfo.getLevel(num3) != base.UserInfo.getLevel(num))
		{
			int level = this.playerInfo.level;
			this.playerInfo.level = base.UserInfo.getLevel(num3);
			int num4 = this.playerInfo.level - level;
			for (int i = level + 1; i < this.playerInfo.level + 1; i++)
			{
				if (base.UserInfo.skillUnlocked(Skills.car_sp) && i % 5 == 0)
				{
					num4++;
				}
			}
			this.stats.obtainedSP += num4;
			base.ToClient("LevelUpFromServer", new object[]
			{
				num3,
				base.UserInfo.SP.Value + this.stats.obtainedSP,
				num4
			});
		}
		return exp + bonusExp;
	}

	// Token: 0x06001519 RID: 5401 RVA: 0x000DEDE8 File Offset: 0x000DCFE8
	public void AddAttacker(int id)
	{
		base.ToClient("AddAttackerFromServer", new object[]
		{
			id
		});
	}

	// Token: 0x0600151A RID: 5402 RVA: 0x000DEE04 File Offset: 0x000DD004
	public void MatchStart()
	{
		this.hightlitedPlayers.Clear();
		this.playerInfo.hasSonar = false;
		this.playerInfo.hasMortar = false;
		this.stats = new Stats(true);
		base.ToClient("MatchStartFromServer", new object[0]);
	}

	// Token: 0x0600151B RID: 5403 RVA: 0x000DEE54 File Offset: 0x000DD054
	public void MatchEndLeague(string json)
	{
		base.ToClient("MatchEndFromServer", new object[]
		{
			Encoding.UTF8.GetBytes(json)
		});
	}

	// Token: 0x0600151C RID: 5404 RVA: 0x000DEE80 File Offset: 0x000DD080
	public void MatchEnd(MatchResultData data)
	{
		this.BestWeaponStatistic.Clear();
		this.DespawnPlayerObject();
		this.KillPlayer();
		this.hightlitedPlayers.Clear();
		if (this.playerInfo.skillUnlocked(Skills.car_expbonus2) && this.playerInfo.killCount > 4)
		{
			float num = 0f;
			foreach (MatchResultPlayerData matchResultPlayerData in data.players)
			{
				if (this.playerInfo.userID != matchResultPlayerData.userID)
				{
					num += (float)matchResultPlayerData.expa;
				}
			}
			num *= 0.015f;
			if (num >= 1f)
			{
				this.Exp((float)Mathf.RoundToInt(num), 0f, true);
				data.tax = Mathf.RoundToInt(num);
			}
		}
		if (!base.IsSpectactor)
		{
			if (Peer.HardcoreMode)
			{
				this.stats.timeHardcore += (int)(Peer.ServerGame.ServerTime - this.stats.timeStart);
			}
			this.stats.timeOnline += (int)(Peer.ServerGame.ServerTime - this.stats.timeStart);
			this.stats.timeStart = Peer.ServerGame.ServerTime;
			try
			{
				ContractsEngine.ClearPointsAtEarnExpMatch(this);
				ContractsEngine.OnEarnExpShowAtMatchEnd(this);
				ContractsEngine.MatchResult(this, Peer.Info.GameMode, this.matchInfo, (Maps)Main.HostInfo.MapIndex);
				AchievementsEngine.OnlineTime(this, this.userInfo.userStats.timeOnline + this.stats.timeOnline);
				AchievementsEngine.MatchResult(this, Peer.Info.GameMode, this.matchInfo, this.playerInfo.deathCount == 0);
				AchievementsEngine.ArmoryUnlock(this);
				AchievementsEngine.WtaskComplete(this);
			}
			catch (Exception e)
			{
				global::Console.exception(e);
			}
			if (Main.UserInfo.Permission >= EPermission.Admin)
			{
				this.SaveProfile();
			}
		}
		MatchResultData obj = new MatchResultData(data, this);
		base.ToClient("MatchEndFromServer", new object[]
		{
			Encoding.UTF8.GetBytes(obj.ToJSON())
		});
	}

	// Token: 0x0600151D RID: 5405 RVA: 0x000DF0C0 File Offset: 0x000DD2C0
	public void RoundStart()
	{
		this.victimList.Clear();
		this.DespawnPlayerObject();
		this.KillPlayer();
		base.ToClient("RoundStartFromServer", new object[0]);
	}

	// Token: 0x0600151E RID: 5406 RVA: 0x000DF0F8 File Offset: 0x000DD2F8
	public void RoundEnd()
	{
		this.DespawnPlayerObject();
		this.KillPlayer();
		base.ToClient("RoundEndFromServer", new object[0]);
	}

	// Token: 0x0600151F RID: 5407 RVA: 0x000DF118 File Offset: 0x000DD318
	public void BearWins(bool bearWins)
	{
		if (base.IsBear)
		{
			this.stats.IsWinner = ((!bearWins) ? -1 : 1);
		}
		else
		{
			this.stats.IsWinner = (bearWins ? -1 : 1);
		}
		base.ToClient("BearWinsFromServer", new object[]
		{
			bearWins
		});
	}

	// Token: 0x06001520 RID: 5408 RVA: 0x000DF180 File Offset: 0x000DD380
	public void EnablePlacement(PlacementType pointType)
	{
		base.ToClient("EnablePlacementFromServer", new object[]
		{
			(int)pointType
		});
	}

	// Token: 0x06001521 RID: 5409 RVA: 0x000DF19C File Offset: 0x000DD39C
	public void DisablePlacement()
	{
		base.ToClient("DisablePlacementFromServer", new object[0]);
	}

	// Token: 0x06001522 RID: 5410 RVA: 0x000DF1B0 File Offset: 0x000DD3B0
	public void Radio(RadioEnum radio)
	{
		base.ToClient("RadioFromServer", new object[]
		{
			(int)radio
		});
	}

	// Token: 0x06001523 RID: 5411 RVA: 0x000DF1CC File Offset: 0x000DD3CC
	public void EventMessage(string nick, ChatInfo info, string msg)
	{
		base.ToClient("GameEventMessageFromServer", new object[]
		{
			nick,
			(int)info,
			msg
		});
	}

	// Token: 0x06001524 RID: 5412 RVA: 0x000DF1FC File Offset: 0x000DD3FC
	public void TacticalExplosion(Vector3 pos, int placementType)
	{
		base.ToClient("TacticalExplosionFromServer", new object[]
		{
			pos,
			placementType
		});
	}

	// Token: 0x06001525 RID: 5413 RVA: 0x000DF224 File Offset: 0x000DD424
	public void Explosion(Vector3 pos)
	{
		try
		{
			base.ToClient("ExplosionFromServer", new object[]
			{
				pos
			});
		}
		catch (Exception ex)
		{
			Debug.LogWarning("PlayerExited while he killing by mortar");
		}
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000DF27C File Offset: 0x000DD47C
	public void MortarExplosion(Vector3[] poses)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		Dictionary<string, object>[] array = new Dictionary<string, object>[poses.Length];
		for (int i = 0; i < poses.Length; i++)
		{
			array[i] = new Dictionary<string, object>();
			eVector3 eVector = new eVector3(poses[i]);
			eVector.Convert(array[i], true);
		}
		dictionary.Add("data", array);
		base.ToClient("MortarExplosionFromServer", new object[]
		{
			ArrayUtility.ToJSON<string, object>(dictionary)
		});
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x000DF2F8 File Offset: 0x000DD4F8
	public void RadioChat()
	{
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x000DF2FC File Offset: 0x000DD4FC
	public bool IsAssist(ServerNetPlayer player)
	{
		return this.assistData.Count > 1 && player && this.assistData.ContainsKey(player);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000DF32C File Offset: 0x000DD52C
	public float AssistInvestment(ServerNetPlayer player, float damage)
	{
		if (player.playerInfo.clanSkillUnlocked(Cl_Skills.cl_train4))
		{
			return 1f;
		}
		return damage / ((this._totalTakenDamage <= 0f) ? damage : this._totalTakenDamage);
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000DF370 File Offset: 0x000DD570
	public void TransferAssistKill(ServerNetPlayer player)
	{
		bool flag = false;
		try
		{
			foreach (KeyValuePair<ServerNetPlayer, DamageByWeapon> keyValuePair in this.assistData)
			{
				if (player == null || keyValuePair.Key == null || keyValuePair.Key.Stats == null)
				{
					flag = true;
					break;
				}
				if (!(player == keyValuePair.Key) && keyValuePair.Value.Type != Weapons.none)
				{
					if (keyValuePair.Key.ID != -1)
					{
						keyValuePair.Key.Stats.assists++;
						float exp = ((!base.IsVip) ? 5f : CVars.g_vipassistexp) * this.AssistInvestment(keyValuePair.Key, keyValuePair.Value.Damage);
						exp = keyValuePair.Key.Exp(exp, (float)keyValuePair.Key.GetKillBonus(this), true);
						this.AddExpToWeaponForAssist(keyValuePair.Key, exp, keyValuePair.Value);
						keyValuePair.Key.KillStreak(KillStreakEnum.assist);
					}
					else
					{
						flag = true;
					}
				}
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
			this.assistData.Clear();
		}
		if (flag)
		{
			this.assistData.Clear();
		}
	}

	// Token: 0x0600152B RID: 5419 RVA: 0x000DF528 File Offset: 0x000DD728
	public override void CallFixedUpdate()
	{
		if (!Main.IsRoundedGame && Peer.ServerGame.MatchState == MatchState.round_going && this.spectator_timer.Elapsed > 300f)
		{
			this.KickByIdle = true;
		}
		this.playerInfo.ping = eNetwork.Ping(this.networkPlayer);
		if (base.IsSpectactor)
		{
			return;
		}
		ClassArray<PlayerCmd> classArray = this.UC.Pop();
		int length = classArray.Length;
		if (length > 0)
		{
			for (int i = 0; i < length; i++)
			{
				PlayerCmd playerCmd = classArray[i];
				if (this.controller != null)
				{
					this.controller.state.euler = playerCmd.euler;
				}
				this.playerInfo.packetLatency = playerCmd.packetLatency;
				this.playerInfo.number = playerCmd.number;
				if (Peer.ServerGame.MatchState == MatchState.round_going)
				{
					if (!this.idle_timer.Enabled && Main.IsGameLoaded)
					{
						this.idle_timer.Start();
						this.cached_cmd.Clone(playerCmd);
					}
					else if (this.idle_timer.Elapsed > 10f && (this.cached_cmd.buttons != playerCmd.buttons || this.cached_cmd.euler != playerCmd.euler))
					{
						this.idle_timer.Start();
						this.cached_cmd.Clone(playerCmd);
					}
					if (this.idle_timer.Elapsed > 90f)
					{
						this.KickByIdle = true;
					}
				}
				this.UInput.Load(playerCmd.buttons);
				this.CheatButtons = 0;
				this.CheatButtons |= playerCmd.CheatButtons;
				base.CallFixedUpdate();
				if (base.IsDeadOrSpectactor)
				{
					break;
				}
				if (Main.IsTargetDesignation && Peer.ServerGame.Placement && base.InPlacementZone)
				{
					this.Placement(base.IsBear ^ Main.GameModeInfo.isBearPlacement);
				}
			}
		}
		if (Peer.ServerGame.MatchState == MatchState.round_going)
		{
			if (length == 0 && !this.idle_timer.Enabled)
			{
				this.idle_timer.Start();
			}
			if (!Main.IsTargetDesignation && !base.IsAlive && this.idle_timer.Elapsed > 90f)
			{
				this.KickByIdle = true;
			}
		}
		if (this.playerInfo.ping >= Globals.I.BadPing && this._pingTime < 0f)
		{
			this._pingTime = Time.realtimeSinceStartup;
		}
		else if (this.playerInfo.ping < Globals.I.BadPing)
		{
			this._pingTime = -1f;
		}
		else if (this._pingTime + 5f < Time.realtimeSinceStartup)
		{
			this.KickByPing = true;
		}
	}

	// Token: 0x0600152C RID: 5420 RVA: 0x000DF830 File Offset: 0x000DDA30
	public override void CallLateUpdate()
	{
		if (this.FreeToActions && this.actions.Count > 0)
		{
			this.FreeToActions = false;
			this.actions.Dequeue()();
		}
		base.CallLateUpdate();
		if (base.IsAlive && base.IsPlayerObject)
		{
			this.hitBody.Add(Peer.ServerGame.ServerTime);
			if (BIT.AND((int)this.playerInfo.buffs, 4))
			{
				this.playerInfo.buffs -= 4;
			}
			if (base.Immortal)
			{
				this.playerInfo.buffs |= Buffs.immortal;
			}
			if (BIT.AND((int)this.playerInfo.buffs, 64))
			{
				this.playerInfo.buffs -= 64;
			}
			if ((base.UserInfo.skillUnlocked(Skills.car_night) || base.UserInfo.skillUnlocked(Skills.car_night2)) && Peer.ServerGame.is_night)
			{
				this.playerInfo.buffs |= Buffs.is_night;
			}
			if (BIT.AND((int)this.playerInfo.buffs, 8))
			{
				this.playerInfo.buffs -= 8;
			}
			float num = 0f;
			if (base.UserInfo.skillUnlocked(Skills.regen1))
			{
				num = 40f;
			}
			if (base.UserInfo.skillUnlocked(Skills.regen2))
			{
				num = 60f;
			}
			if (num > 0f && base.Health < num)
			{
				if (this.regen.Enabled)
				{
					if (this.regen.Elapsed > 0.33f)
					{
						base.Health += 1f;
						if (base.Health > num)
						{
							base.Health = num;
						}
						this.regen.Stop();
					}
				}
				else
				{
					this.regen.Start();
				}
				this.playerInfo.buffs |= Buffs.health_boost;
			}
			if (!this.addHealthOnceByLife && BIT.AND((int)this.playerInfo.buffs, 33554432))
			{
				this.addHealthOnceByLife = true;
				base.Health += 20f;
			}
			if (BIT.AND((int)this.playerInfo.buffs, 67108864) && base.Health < 75f)
			{
				if (this.regen.Enabled)
				{
					if (this.regen.Elapsed > ((base.UserInfo.ClanRole <= Role.officer) ? 1f : 2f))
					{
						base.Health += 5f;
						if (base.Health >= 75f)
						{
							base.Health = 75f;
						}
						this.regen.Stop();
					}
				}
				else
				{
					this.regen.Start();
				}
			}
			if (!BIT.AND((int)this.playerInfo.buffs, 134217728) || this.AddMagOnce || !base.Ammo.CurrentWeapon.IsPrimary || base.Ammo.CurrentWeapon.state.bagSize < this.fullBag)
			{
			}
			if (!this.setDamageWeaponReducer)
			{
				if (BIT.AND((int)this.playerInfo.buffs, 268435456))
				{
					this.setDamageWeaponReducer = true;
					CVars.g_DamageWeaponReducer *= 0.5f;
				}
				else
				{
					CVars.g_DamageWeaponReducer = 1f;
				}
			}
			if (Peer.HardcoreMode && base.Health < 10f && base.IsAlive)
			{
				this.playerInfo.buffs |= Buffs.bleed;
				if (base.Health < 1f)
				{
					if (BIT.AND((int)this.playerInfo.buffs, 2097152))
					{
						this.playerInfo.buffs -= 2097152;
					}
					if (this.lastHitted != null)
					{
						this.lastHitted.Stats.SafeKill(this.lastHitted, false, false, this, WeaponNature.knife, null);
						this.Kill(this.lastHitted, this.lastHitted.WeaponEquipedIndex, false, this.ID, "legs", Vector3.zero, (this.lastHitted.WeaponEquipedIndex >= this.lastHitted.Stats.weaponKills.Length) ? 0 : this.lastHitted.Stats.weaponKills[this.lastHitted.WeaponEquipedIndex]);
					}
					else
					{
						this.Stats.Suicide(this);
						this.Kill(this, 123, false, this.ID, "legs", Vector3.zero, 0);
					}
				}
				else if (this.BleedingTimer.Enabled && base.IsAlive)
				{
					if (this.BleedingTimer.Elapsed > 2f)
					{
						base.Health -= 1f;
						this.BleedingTimer.Stop();
					}
				}
				else
				{
					this.BleedingTimer.Start();
				}
			}
			if (BIT.AND((int)this.playerInfo.buffs, 128))
			{
				this.playerInfo.buffs -= 128;
			}
			if (this.vip)
			{
				this.playerInfo.buffs |= Buffs.VIP;
			}
		}
		if (this.playerInfo != null)
		{
			this.playerInfo.temporaryBuff = (Buffs)0;
		}
		if (this.isAutoBallanced)
		{
			this.autoBallanceTimer -= Time.deltaTime;
			if (this.autoBallanceTimer < 0f)
			{
				this.isAutoBallanced = false;
				this.autoBallanceTimer = 60f;
			}
		}
		this._keepaliveTimer -= Time.deltaTime;
		if (this._keepaliveTimer < 0f)
		{
			this.autoBallanceTimer = 60f;
		}
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x000DFE68 File Offset: 0x000DE068
	private void AddExpToWeaponForAssist(ServerNetPlayer player, float exp, DamageByWeapon weapon)
	{
		this.stats.AddExpToWeapon(player, weapon.Type, weapon.IsModable, exp);
	}

	// Token: 0x04001967 RID: 6503
	private const float TIMER_VALUE = 60f;

	// Token: 0x04001968 RID: 6504
	private const float KeepaliveTimerValue = 5f;

	// Token: 0x04001969 RID: 6505
	public bool KickByIdle;

	// Token: 0x0400196A RID: 6506
	public bool KickByPing;

	// Token: 0x0400196B RID: 6507
	public bool AddMagOnce;

	// Token: 0x0400196C RID: 6508
	public int matchExpBonus;

	// Token: 0x0400196D RID: 6509
	public string guid = string.Empty;

	// Token: 0x0400196E RID: 6510
	public string Hash = string.Empty;

	// Token: 0x0400196F RID: 6511
	public List<ServerNetPlayer> hightlitedPlayers = new List<ServerNetPlayer>();

	// Token: 0x04001970 RID: 6512
	public eTimer spectator_timer = new eTimer();

	// Token: 0x04001971 RID: 6513
	public Queue<int> victimList = new Queue<int>();

	// Token: 0x04001972 RID: 6514
	public bool isAutoBallanced;

	// Token: 0x04001973 RID: 6515
	[SerializeField]
	public float autoBallanceTimer = 60f;

	// Token: 0x04001974 RID: 6516
	private bool vip;

	// Token: 0x04001975 RID: 6517
	private bool disconnectAfterSave;

	// Token: 0x04001976 RID: 6518
	private bool canSpawn;

	// Token: 0x04001977 RID: 6519
	private bool addHealthOnceByLife;

	// Token: 0x04001978 RID: 6520
	private bool setDamageWeaponReducer;

	// Token: 0x04001979 RID: 6521
	private int fullBag;

	// Token: 0x0400197A RID: 6522
	private float _totalTakenDamage;

	// Token: 0x0400197B RID: 6523
	private eTimer idle_timer = new eTimer();

	// Token: 0x0400197C RID: 6524
	private HitBody hitBody;

	// Token: 0x0400197D RID: 6525
	private PlayerCmd cached_cmd = new PlayerCmd();

	// Token: 0x0400197E RID: 6526
	private ServerNetPlayer lastHitted;

	// Token: 0x0400197F RID: 6527
	private Dictionary<ServerNetPlayer, DamageByWeapon> assistData = new Dictionary<ServerNetPlayer, DamageByWeapon>();

	// Token: 0x04001980 RID: 6528
	private float _pingTime = -1f;

	// Token: 0x04001981 RID: 6529
	protected Stats stats;

	// Token: 0x04001982 RID: 6530
	private float _saveProfileTimer;

	// Token: 0x04001983 RID: 6531
	private float _saveProfileTimout = 10f;

	// Token: 0x04001984 RID: 6532
	public int playerState;

	// Token: 0x04001985 RID: 6533
	private Queue<Action> actions = new Queue<Action>();

	// Token: 0x04001986 RID: 6534
	private bool FreeToActions = true;

	// Token: 0x04001987 RID: 6535
	private bool ProfileLoaded;

	// Token: 0x04001988 RID: 6536
	private WeaponStatistic BestWeaponStatistic = new WeaponStatistic();

	// Token: 0x04001989 RID: 6537
	private float _lastPacketTime;

	// Token: 0x0400198A RID: 6538
	private bool _keepaliveDisconnected;

	// Token: 0x0400198B RID: 6539
	private float _keepaliveTimer = 5f;

	// Token: 0x0400198C RID: 6540
	private int _respawnTime = -1;

	// Token: 0x0400198D RID: 6541
	private bool _setRespawnTimeFirstTime = true;

	// Token: 0x0400198E RID: 6542
	public int CheatButtons;
}
