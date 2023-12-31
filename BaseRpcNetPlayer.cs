using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Assets.Scripts.Game;
using LeagueSystem;
using UnityEngine;

// Token: 0x020001A4 RID: 420
[AddComponentMenu("Scripts/Game/BaseRpcNetPlayer")]
internal class BaseRpcNetPlayer : BaseNetPlayer
{
	// Token: 0x06000CC3 RID: 3267 RVA: 0x00098C78 File Offset: 0x00096E78
	public override void OnPoolDespawn()
	{
		this.DestroyViews();
		this.remoteClientLoaded = false;
		if (this.networkPlayer != null)
		{
			if (this.networkPlayer.IsVirtual)
			{
				this.myID = default(NetworkViewID);
				this.targetID = default(NetworkViewID);
			}
			this.networkPlayer = null;
		}
		this.myView = null;
		this.targetView = null;
		this.group = IDUtil.NoID;
		base.OnPoolDespawn();
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x00098CF4 File Offset: 0x00096EF4
	protected override void OnEnable()
	{
		if (this.myView)
		{
			this.myID = this.myView.viewID;
		}
		if (this.targetView)
		{
			this.targetID = this.targetView.viewID;
		}
		if (this.myView && this.targetView)
		{
			this.DisableViews();
			this.EnableViews();
		}
		base.OnEnable();
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00098D78 File Offset: 0x00096F78
	public override void CallLateUpdate()
	{
		base.CallLateUpdate();
	}

	// Token: 0x170001B7 RID: 439
	// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00098D80 File Offset: 0x00096F80
	private ServerNetPlayer serverPlayer
	{
		get
		{
			return this as ServerNetPlayer;
		}
	}

	// Token: 0x170001B8 RID: 440
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00098D88 File Offset: 0x00096F88
	private ClientNetPlayer clientPlayer
	{
		get
		{
			return this as ClientNetPlayer;
		}
	}

	// Token: 0x170001B9 RID: 441
	// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00098D90 File Offset: 0x00096F90
	public eNetworkPlayer NetworkPlayer
	{
		get
		{
			return this.networkPlayer;
		}
	}

	// Token: 0x170001BA RID: 442
	// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00098D98 File Offset: 0x00096F98
	public NetworkViewID TargetID
	{
		get
		{
			return this.targetID;
		}
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00098DA0 File Offset: 0x00096FA0
	public NetworkViewID MyID
	{
		get
		{
			return this.myID;
		}
	}

	// Token: 0x170001BC RID: 444
	// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00098DA8 File Offset: 0x00096FA8
	public int Group
	{
		get
		{
			return this.group;
		}
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00098DB0 File Offset: 0x00096FB0
	public void ToClient(string name, params object[] args)
	{
		if (this.remoteClientLoaded)
		{
			this.targetView.RPC(name, this.targetID, args);
		}
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00098DD0 File Offset: 0x00096FD0
	public void ToClientPreload(string name, params object[] args)
	{
		this.targetView.RPC(name, this.targetID, args);
	}

	// Token: 0x06000CCE RID: 3278 RVA: 0x00098DE8 File Offset: 0x00096FE8
	public void ToServer(string name, params object[] args)
	{
		this.targetView.RPC(name, this.targetID, args);
	}

	// Token: 0x06000CCF RID: 3279 RVA: 0x00098E00 File Offset: 0x00097000
	public void InitializeNetwork(BaseCmdCollector UC, NetworkViewID myID, NetworkViewID targetID, int group)
	{
		this.networkPlayer = new eNetworkPlayer();
		this.UC = UC;
		this.myID = myID;
		this.targetID = targetID;
		this.group = group;
	}

	// Token: 0x06000CD0 RID: 3280 RVA: 0x00098E38 File Offset: 0x00097038
	public void CreateViews()
	{
		this.myView = base.gameObject.AddComponent<eNetworkView>();
		this.myView.owner = this.networkPlayer;
		if (this is ServerNetPlayer)
		{
			this.myView.sendRate = (float)CVars.n_updaterate;
		}
		else
		{
			this.myView.sendRate = (float)CVars.n_cmdrate;
		}
		this.myView.viewID = this.myID;
		this.myView.group = this.group;
		this.myView.stateSynchronization = NetworkStateSynchronization.Unreliable;
		this.myView.observed = this;
		this.myView.isMine = true;
		this.targetView = base.gameObject.AddComponent<eNetworkView>();
		this.targetView.owner = this.networkPlayer;
		this.targetView.viewID = this.targetID;
		this.targetView.group = this.group;
		this.targetView.stateSynchronization = NetworkStateSynchronization.Unreliable;
		this.targetView.observed = this;
		this.targetView.isMine = false;
	}

	// Token: 0x06000CD1 RID: 3281 RVA: 0x00098F48 File Offset: 0x00097148
	public void DestroyViews()
	{
		if (this.myView)
		{
			if (this.myID != default(NetworkViewID))
			{
				this.myID = default(NetworkViewID);
			}
			UnityEngine.Object.DestroyObject(this.myView.view);
			UnityEngine.Object.DestroyObject(this.myView);
			this.myView = null;
		}
		if (this.targetView)
		{
			if (this.targetID != default(NetworkViewID))
			{
				this.targetID = default(NetworkViewID);
			}
			UnityEngine.Object.DestroyObject(this.targetView.view);
			UnityEngine.Object.DestroyObject(this.targetView);
			this.targetView = null;
		}
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0009900C File Offset: 0x0009720C
	public void DisableViews()
	{
		if (this.myView)
		{
			this.myView.Disable();
		}
		if (this.targetView)
		{
			this.targetView.Disable();
		}
		if (this.networkPlayer != null)
		{
			if (!this.networkPlayer.IsVirtual)
			{
				if (this.targetView)
				{
					this.targetView.Enable(this.targetID.owner);
				}
				eNetwork.DisableAllGroups(this.targetID.owner);
				eNetwork.SetReceivingEnabled(this.targetID.owner, this.group, true);
			}
		}
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000990BC File Offset: 0x000972BC
	public void EnableViews()
	{
		this.myView.Disable();
		this.targetView.Disable();
		if (!this.networkPlayer.IsVirtual)
		{
			this.myView.Enable(this.targetID.owner);
			this.targetView.Enable(this.targetID.owner);
			eNetwork.DisableAllGroups(this.targetID.owner);
			eNetwork.SetReceivingEnabled(this.targetID.owner, this.group, true);
			eNetwork.SetSendingEnabled(this.targetID.owner, this.group, true);
		}
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x00099160 File Offset: 0x00097360
	public void AddClanBuffs(ServerNetPlayer player)
	{
		if (player.UserInfo.clanID != 0 && player.UserInfo.playerClass == PlayerClass.destroyer && player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_dest3))
		{
			this.ClanBuffDistance |= 134217728;
		}
		if (player.UserInfo.clanID != 0 && player.UserInfo.playerClass == PlayerClass.sniper && player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_sni2))
		{
			this.ClanBuffTeammate |= 1024;
		}
		if (player.UserInfo.clanID != 0 && player.UserInfo.playerClass == PlayerClass.gunsmith && player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_repair))
		{
			this.ClanBuffTeammate |= 268435456;
		}
		if (player.UserInfo.clanID != 0 && player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_squad))
		{
			this.ClanCompositeBuffItem |= 1 << (int)player.UserInfo.playerClass;
		}
		if (player.UserInfo.ClanRole >= Role.lt)
		{
			if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_lead1))
			{
				this.ClanBuffTeammate |= 16777216;
				this.ClanBuffSelf |= 16777216;
			}
			if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_lead2))
			{
				this.ClanBuffTeammate |= 33554432;
				this.ClanBuffSelf |= 33554432;
			}
			if (player.UserInfo.clanSkillUnlocked(Cl_Skills.cl_lead3))
			{
				this.ClanBuffDistance |= 67108864;
				this.ClanBuffSelf |= 67108864;
			}
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x00099330 File Offset: 0x00097530
	public void ClearClanBuffs()
	{
		this.ClanBuffDistance = 0;
		this.ClanBuffSelf = 0;
		this.ClanBuffTeammate = 0;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x00099348 File Offset: 0x00097548
	public virtual void ChooseTeamFromClientBase(int type, bool penalty = false)
	{
		Debug.Log("ChooseTeamFromClientBase");
		if (Peer.ServerGame.IsFull && base.IsSpectactor)
		{
			return;
		}
		if (base.IsDeadOrSpectactor)
		{
			this.playerInfo.playerType = (PlayerType)type;
			this.ToClient("ChooseTeamFromServer", new object[]
			{
				type
			});
			if (Main.IsRoundedGame && Peer.ServerGame.isInFirstTenSeconds && Peer.ServerGame.MatchState == MatchState.round_going && !base.IsSpectactor)
			{
				this.serverPlayer.EnablePlacement(Peer.ServerGame.Placement.PointType);
				this.serverPlayer.BaseServerSpawn();
			}
		}
		else if ((Main.IsRoundedGame || Main.IsTeamElimination || Main.IsTacticalConquest) && this.playerInfo.playerType != (PlayerType)type)
		{
			this.playerInfo.playerType = (PlayerType)type;
			this.ToClient("ChooseTeamFromServer", new object[]
			{
				type
			});
			if (penalty)
			{
				this.serverPlayer.Stats.Suicide(this.serverPlayer);
			}
			this.serverPlayer.Kill(this.serverPlayer, 123, false, this.serverPlayer.ID, "legs", Vector3.zero, 0);
			this.serverPlayer.Stats.Kill(this.serverPlayer);
		}
		else
		{
			this.ToClient("ChooseTeamFromServer", new object[]
			{
				(int)this.playerInfo.playerType
			});
		}
		if (base.IsSpectactor)
		{
			this.serverPlayer.spectator_timer.Start();
		}
		if (!Peer.Info.Hardcore && !ServerLeagueSystem.Enabled)
		{
			if (type != 2)
			{
				this.AddClanBuffs(this.serverPlayer);
			}
			else
			{
				this.ClearClanBuffs();
			}
		}
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0009953C File Offset: 0x0009773C
	[RPC]
	[Obfuscation(Exclude = true)]
	public void Aim()
	{
		Debug.Log("Aim");
		this.ammo.ChangeAimMode(false);
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00099554 File Offset: 0x00097754
	[Obfuscation(Exclude = true)]
	[RPC]
	public void Sit()
	{
		Debug.Log("Sit");
		this.UInput.SetKey(UKeyCode.sit);
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00099570 File Offset: 0x00097770
	[RPC]
	[Obfuscation(Exclude = true)]
	public void ChangePlayerWeapon(int weapon)
	{
		Debug.Log("BaseRpcNetPlayer.ChangePlayerWeapon");
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x0009957C File Offset: 0x0009777C
	[RPC]
	[Obfuscation(Exclude = true)]
	public void SendAdditionalData(int[] data)
	{
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x00099580 File Offset: 0x00097780
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void ChooseTeamFromClient(int type)
	{
		Debug.Log("ChooseTeamFromClient");
		if (Peer.ServerGame.IsFull && base.IsSpectactor)
		{
			return;
		}
		if (ServerLeagueSystem.Enabled)
		{
			return;
		}
		this.ChooseTeamFromClientBase(type, false);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x000995C8 File Offset: 0x000977C8
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void ChooseTeamFromServer(int type)
	{
		Debug.Log("ChooseTeamFromServer");
		this.playerInfo.playerType = (PlayerType)type;
		EventFactory.Call("HideTeamChoose", 0.5f);
	}

	// Token: 0x06000CDD RID: 3293 RVA: 0x00099600 File Offset: 0x00097800
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void ChooseAmmunitionFromClient(int suitIndex, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, string secondaryMods, string primaryMods, int mpExp, int weaponKit)
	{
		WeaponInfo[] weaponsStates = this.serverPlayer.UserInfo.weaponsStates;
		int num = (primaryIndex >= weaponsStates.Length) ? -1 : weaponsStates[primaryIndex].CurrentWeapon.skillReq;
		bool flag = num != -1;
		bool flag2 = primaryIndex != 127 && flag && this.serverPlayer.UserInfo.skillsInfos[num].Unlocked;
		int skillReq = weaponsStates[secondaryIndex].CurrentWeapon.skillReq;
		bool flag3 = secondaryIndex != 127 && skillReq != -1 && this.serverPlayer.UserInfo.skillsInfos[skillReq].Unlocked;
		if (secondaryIndex != 127 && weaponsStates[secondaryIndex].CurrentWeapon.IsPrimary)
		{
			secondaryIndex = 0;
		}
		if (primaryIndex != 127 && weaponsStates[primaryIndex].CurrentWeapon.IsSecondary)
		{
			primaryIndex = 127;
		}
		if (secondaryIndex != 127 && !weaponsStates[secondaryIndex].Unlocked && !flag3)
		{
			secondaryIndex = 0;
		}
		if (secondaryIndex != 127 && weaponsStates[secondaryIndex].repair_info >= (float)weaponsStates[secondaryIndex].CurrentWeapon.durability && !weaponsStates[secondaryIndex].CurrentWeapon.isPremium)
		{
			secondaryIndex = 0;
		}
		if (secondaryIndex != 127 && !weaponsStates[secondaryIndex].wtaskUnlocked && secondaryMod)
		{
			secondaryMod = false;
		}
		if (primaryIndex != 127 && (!weaponsStates[primaryIndex].Unlocked || flag) && !flag2)
		{
			primaryIndex = 127;
		}
		if (primaryIndex != 127 && weaponsStates[primaryIndex].repair_info >= (float)weaponsStates[primaryIndex].CurrentWeapon.durability && !weaponsStates[primaryIndex].CurrentWeapon.isPremium)
		{
			primaryIndex = 127;
		}
		if (primaryIndex != 127 && !weaponsStates[primaryIndex].wtaskUnlocked && primaryMod)
		{
			primaryMod = false;
		}
		this.SecondaryIndex = secondaryIndex;
		this.PrimaryIndex = primaryIndex;
		this.SecondaryMod = secondaryMod;
		this.PrimaryMod = primaryMod;
		this.WeaponKit = weaponKit;
		this.SecondaryMods = secondaryMods;
		this.PrimaryMods = primaryMods;
		this.serverPlayer.UserInfo.CurrentMpExp = mpExp;
	}

	// Token: 0x06000CDE RID: 3294 RVA: 0x00099820 File Offset: 0x00097A20
	public void BaseServerSpawn(int ID)
	{
		Debug.Log(this + " BaseRpcNetPlayer.BaseServerSpawn " + this.ID);
		if (Peer.ServerGame.MatchState != MatchState.round_going && Peer.ServerGame.MatchState != MatchState.alone)
		{
			return;
		}
		if (base.IsAlive || base.IsSpectactor)
		{
			return;
		}
		Spawn spawnPoint = Peer.ServerGame.getSpawnPoint(base.IsBear, ID);
		if (spawnPoint == null)
		{
			return;
		}
		float num = 0f;
		float num2 = 0f;
		if (this.SecondaryIndex != 127)
		{
			num = this.serverPlayer.UserInfo.weaponsStates[this.SecondaryIndex].repair_info;
		}
		if (this.PrimaryIndex != 127)
		{
			num2 = this.serverPlayer.UserInfo.weaponsStates[this.PrimaryIndex].repair_info;
		}
		this.SpawnPlayerObject(spawnPoint.pos, spawnPoint.euler, this.SecondaryIndex, this.PrimaryIndex, this.SecondaryMod, this.PrimaryMod, num, num2, this.SecondaryMods, this.PrimaryMods, this.WeaponKit);
		this.ToClient("SpawnFromServer", new object[]
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

	// Token: 0x06000CDF RID: 3295 RVA: 0x000999E8 File Offset: 0x00097BE8
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void SpawnFromClient()
	{
		if (Main.IsRoundedGame)
		{
			return;
		}
		if (this.serverPlayer.CanSpawn)
		{
			this.serverPlayer.BaseServerSpawn();
		}
	}

	// Token: 0x06000CE0 RID: 3296 RVA: 0x00099A1C File Offset: 0x00097C1C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void SpawnFromClientAt(int ID)
	{
		if (Main.IsRoundedGame)
		{
			return;
		}
		if (this.serverPlayer.CanSpawn)
		{
			this.BaseServerSpawn(ID);
		}
	}

	// Token: 0x06000CE1 RID: 3297 RVA: 0x00099A4C File Offset: 0x00097C4C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void SpawnFromServer(int playerType, Vector3 pos, Vector3 euler, int secondaryIndex, int primaryIndex, bool secondaryMod, bool primaryMod, float secondary_repair_info, float primary_repair_info, string secondaryMods, string primaryMods, int weaponKit)
	{
		Debug.Log("BaseRpcNetPlayer.SpawnFromServer");
		if (!Main.IsGameLoaded)
		{
			return;
		}
		this.playerInfo.playerType = (PlayerType)playerType;
		this.SpawnPlayerObject(pos, euler, secondaryIndex, primaryIndex, secondaryMod, primaryMod, secondary_repair_info, primary_repair_info, secondaryMods, primaryMods, weaponKit);
	}

	// Token: 0x06000CE2 RID: 3298 RVA: 0x00099A94 File Offset: 0x00097C94
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void ChatFromClient(string msg, int infoInt)
	{
		Peer.ServerGame.Chat(this, msg, (ChatInfo)infoInt);
	}

	// Token: 0x06000CE3 RID: 3299 RVA: 0x00099AA4 File Offset: 0x00097CA4
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void ChatFromServer(int playerID, string nick, int infoInt, string msg)
	{
		EventFactory.Call("ChatMessage", new object[]
		{
			(ChatInfo)infoInt,
			nick,
			msg
		});
		global::Console.print(nick + ":" + msg, Colors.Chat);
		if (infoInt == 1 || infoInt == 7)
		{
			for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
			{
				if (Peer.ClientGame.AllPlayers[i].ID == playerID)
				{
					Peer.ClientGame.AllPlayers[i].Talk();
				}
			}
		}
	}

	// Token: 0x06000CE4 RID: 3300 RVA: 0x00099B50 File Offset: 0x00097D50
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void InfoFromServer(byte[] bytes)
	{
		global::Console.print("server: " + Encoding.UTF8.GetString(bytes), Colors.Chat);
	}

	// Token: 0x06000CE5 RID: 3301 RVA: 0x00099B74 File Offset: 0x00097D74
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void AdminCommandFromClient(string cmd, int userId = -1)
	{
		string[] array = cmd.Split(new char[]
		{
			' '
		});
		if (this.serverPlayer.UserInfo.Permission < EPermission.Moder && cmd.Contains("kick") && this.serverPlayer.UserInfo.repa >= 500f)
		{
			Peer.ServerGame.Kick(Convert.ToInt32(array[1]), string.Empty);
		}
		if (this.serverPlayer.UserInfo.Permission >= EPermission.Moder)
		{
			if (cmd.Contains("ss"))
			{
				int num = Convert.ToInt32(array[1]);
				string reporterNick = string.Empty;
				ServerNetPlayer serverNetPlayer = null;
				foreach (ServerNetPlayer serverNetPlayer2 in Peer.ServerGame.ServerNetPlayers)
				{
					if (serverNetPlayer2.ID == num)
					{
						serverNetPlayer = serverNetPlayer2;
					}
					if (serverNetPlayer2.UserID == userId)
					{
						reporterNick = serverNetPlayer2.Nick;
					}
				}
				if (serverNetPlayer)
				{
					serverNetPlayer.CaptureScreen(reporterNick, userId);
				}
			}
			else if (cmd.Contains("ban"))
			{
				Peer.ServerGame.KickByUid(Convert.ToInt32(array[1]), string.Empty);
			}
		}
		if (this.serverPlayer.UserInfo.Permission >= EPermission.Admin)
		{
			try
			{
				if (cmd.ToLower().Contains("kick"))
				{
					Peer.ServerGame.Kick(Convert.ToInt32(array[1]), string.Empty);
				}
				else if (cmd.ToLower() == "nw_h")
				{
					this.ToClient("InfoFromServer", new object[]
					{
						Encoding.UTF8.GetBytes(Utility.PrintHierarchy(SingletoneComponent<Main>.Instance.transform, 1))
					});
				}
				else if (cmd.ToLower() == "admin_pain")
				{
					CVars.a_grenade = true;
				}
				else if (cmd.ToLower() == "nw_info")
				{
					string text = "server network view info:";
					text += "\n\nNetwork.connections:";
					for (int i = 0; i < Network.connections.Length; i++)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							"\n",
							Network.connections[i],
							" ",
							Network.connections[i].ipAddress
						});
					}
					text += "\n\nServerNetPlayers:";
					for (int j = 0; j < Peer.ServerGame.ServerNetPlayers.Count; j++)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							"\nUID ",
							Peer.ServerGame.ServerNetPlayers[j].UserID,
							" ID ",
							Peer.ServerGame.ServerNetPlayers[j].ID,
							" myID ",
							Peer.ServerGame.ServerNetPlayers[j].myID,
							" targetID ",
							Peer.ServerGame.ServerNetPlayers[j].targetID
						});
						if (Peer.ServerGame.ServerNetPlayers[j].networkPlayer.IsVirtual)
						{
							text += "   Virtual";
						}
						else
						{
							text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								"   Dedicated ",
								Peer.ServerGame.ServerNetPlayers[j].networkPlayer.owner,
								" ",
								Peer.ServerGame.ServerNetPlayers[j].networkPlayer.owner.ipAddress
							});
						}
					}
					text += "\n\nloadingNetPlayer:";
					for (int k = 0; k < Peer.ServerGame.LoadingNetPlayers.Count; k++)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							"\nUID ",
							Peer.ServerGame.LoadingNetPlayers[k].UserID,
							" ID ",
							Peer.ServerGame.LoadingNetPlayers[k].ID,
							" myID ",
							Peer.ServerGame.LoadingNetPlayers[k].myID,
							" targetID ",
							Peer.ServerGame.LoadingNetPlayers[k].targetID
						});
						if (Peer.ServerGame.LoadingNetPlayers[k].networkPlayer.IsVirtual)
						{
							text += "   Virtual";
						}
						else
						{
							text2 = text;
							text = string.Concat(new object[]
							{
								text2,
								"   Dedicated ",
								Peer.ServerGame.LoadingNetPlayers[k].networkPlayer.owner,
								" ",
								Peer.ServerGame.LoadingNetPlayers[k].networkPlayer.owner.ipAddress
							});
						}
					}
					text += "\n\nNetworkViews:";
					NetworkView[] componentsInChildren = SingletoneComponent<Main>.Instance.GetComponentsInChildren<NetworkView>(true);
					for (int l = 0; l < componentsInChildren.Length; l++)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							"\n",
							componentsInChildren[l],
							" ",
							componentsInChildren[l].viewID
						});
					}
					this.ToClient("InfoFromServer", new object[]
					{
						Encoding.UTF8.GetBytes(text)
					});
				}
				else if (cmd.ToLower() == "start")
				{
					Peer.ServerGame.MatchStart();
					Peer.ServerGame.RoundStart();
				}
				else if (cmd.ToLower() == "stop")
				{
					Peer.ServerGame.RoundPreEnd();
					Peer.ServerGame.RoundEnd();
					Peer.ServerGame.MatchEnd();
				}
				else if (cmd.ToLower() == "restart")
				{
					Peer.ServerGame.RoundPreEnd();
					Peer.ServerGame.RoundEnd();
					Peer.ServerGame.MatchEnd();
				}
				else if (cmd.ToLower() == "bearwin")
				{
					Peer.ServerGame.RoundPreEnd();
					if (Main.IsTargetDesignation)
					{
						(Peer.ServerGame as ServerTargetDesignationGame).BearWins(true);
					}
				}
				else if (cmd.ToLower() == "usecwin")
				{
					Peer.ServerGame.RoundPreEnd();
					if (Main.IsTargetDesignation)
					{
						(Peer.ServerGame as ServerTargetDesignationGame).BearWins(false);
					}
				}
				else if (cmd.ToLower().Contains("match start"))
				{
					Peer.ServerGame.MatchStart();
				}
				else if (cmd.ToLower().Contains("round start"))
				{
					Peer.ServerGame.RoundStart();
				}
				else if (cmd.ToLower().Contains("round end"))
				{
					Peer.ServerGame.RoundEnd();
				}
				else if (cmd.ToLower().Contains("kill"))
				{
					Vector3 position = Peer.ServerGame.GetPlayer(Convert.ToInt32(array[1])).Position;
					Peer.ServerGame.Explosion(position);
					Peer.ServerGame.GrenadeExplosion(this as ServerNetPlayer, position);
				}
				else if (cmd.ToLower().Contains("server_targetframerate"))
				{
					Application.targetFrameRate = Convert.ToInt32(array[1]);
				}
				else if (cmd.ToLower().Contains("round pre end"))
				{
					(Peer.ServerGame as ServerTargetDesignationGame).BearWins(true);
					(Peer.ServerGame as ServerTargetDesignationGame).RoundPreEnd();
				}
				else if (cmd.ToLower().Contains("match end"))
				{
					Peer.ServerGame.RoundEnd();
					Peer.ServerGame.MatchEnd();
				}
				else if (cmd.ToLower().Contains("match time"))
				{
					float matchRoundTime = Main.GameModeInfo.matchRoundTime;
					string[] array2 = cmd.Split(new char[]
					{
						' '
					});
					try
					{
						matchRoundTime = (float)Convert.ToInt32(array2[2]);
					}
					catch (Exception e)
					{
						global::Console.exception(e);
					}
					Main.GameModeInfo.matchRoundTime = matchRoundTime;
				}
				else if (cmd.Contains("mortar"))
				{
					this.serverPlayer.ArmStreak(ArmstreakEnum.mortar);
				}
				else if (cmd.Contains("sonar"))
				{
					this.serverPlayer.ArmStreak(ArmstreakEnum.sonar);
				}
				else if (cmd.Contains("health"))
				{
					this.serverPlayer.Health = (float)Convert.ToInt32(array[1]);
				}
				else if (cmd.Contains("armor"))
				{
					this.serverPlayer.Armor = (float)Convert.ToInt32(array[1]);
				}
				else if (cmd.Contains("grenade"))
				{
					if (array.Length == 2)
					{
						this.serverPlayer.ammo.state.grenadeCount += (int)(Convert.ToInt16(array[1]) - 1);
						this.serverPlayer.ArmStreak(ArmstreakEnum.grenade);
					}
					if (array.Length == 3)
					{
						int num2 = Convert.ToInt32(array[2]);
						Debug.Log("PLayerID " + num2);
						foreach (ServerNetPlayer serverNetPlayer3 in Peer.ServerGame.ServerNetPlayers)
						{
							if (serverNetPlayer3.ID == num2)
							{
								serverNetPlayer3.ammo.state.grenadeCount += (int)(Convert.ToInt16(array[1]) - 1);
								serverNetPlayer3.ArmStreak(ArmstreakEnum.grenade);
							}
						}
					}
				}
				else if (cmd.Contains("boots"))
				{
					this.boots = Convert.ToSingle(array[1]);
				}
				else if (cmd.Contains("frog"))
				{
					this.frog = Convert.ToSingle(array[1]);
				}
				else if (cmd.Contains("close") && this.serverPlayer.UserInfo.Permission >= EPermission.Admin)
				{
					Peer.Disconnect(true);
					EventFactory.Call("ShowInterface", null);
					Network.Disconnect();
					Application.Quit();
				}
			}
			catch (Exception message)
			{
				Debug.Log(message);
			}
		}
	}

	// Token: 0x06000CE6 RID: 3302 RVA: 0x0009A70C File Offset: 0x0009890C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void VoteFromClient(string votefor)
	{
		Peer.ServerGame.EventMessage(this.serverPlayer.Nick, ChatInfo.network_message, Language.VotedFor + " " + votefor);
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0009A740 File Offset: 0x00098940
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void SuspectFromClient(int uid, int type)
	{
		if (this.StartSuspectCooldown < 0f && this.SuspectCooldown < 0f && ReportSystem.Instance.ServerAddSuspect(uid, base.UserID, (ReportType)type))
		{
			this.SuspectCooldown = (float)CVars.SuspectCooldownTime;
		}
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x0009A798 File Offset: 0x00098998
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void QuitFromClient(string reason)
	{
		Peer.ServerGame.PlayerDisconnected(this.serverPlayer, reason);
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x0009A7AC File Offset: 0x000989AC
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void QuitFromServer()
	{
		EventFactory.Call("HidePopup", new Popup(WindowsID.Quit, Language.Connection, Language.ExittingFromServer, PopupState.information, false, true, string.Empty, string.Empty));
		Peer.Disconnect(true);
		EventFactory.Call("ShowInterface", null);
		this.UpdateInfo();
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x0009A7F8 File Offset: 0x000989F8
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void SyncFromClient()
	{
		this.UC.Clear();
		this.update = UpdateType.FullUpdate;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x0009A80C File Offset: 0x00098A0C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void SyncFinishedFromClient()
	{
		this.UC.Clear();
		this.update = UpdateType.Update;
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x0009A820 File Offset: 0x00098A20
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void FullUpdateRequestFromClient()
	{
		this.update = UpdateType.FullUpdate;
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x0009A82C File Offset: 0x00098A2C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void FullUpdateRequestFinishedFromClient()
	{
		this.update = UpdateType.Update;
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x0009A838 File Offset: 0x00098A38
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void StartLoadingFromClient()
	{
		this.loadingState = LoadingState.clientLoading;
	}

	// Token: 0x170001BD RID: 445
	// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0009A844 File Offset: 0x00098A44
	// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x0009A84C File Offset: 0x00098A4C
	public bool IsLoadingFinished { get; private set; }

	// Token: 0x06000CF1 RID: 3313 RVA: 0x0009A858 File Offset: 0x00098A58
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void FinishedLoadingFromClient()
	{
		this.EnableViews();
		this.loadingState = LoadingState.clientFinishedLoading;
		this.update = UpdateType.FullUpdate;
		this.remoteClientLoaded = true;
		if (ServerLeagueSystem.Enabled)
		{
			this.IsLoadingFinished = true;
			if (ServerLeagueSystem.Team(base.UserID) == 1)
			{
				this.playerInfo.playerType = PlayerType.bear;
			}
			if (ServerLeagueSystem.Team(base.UserID) == 2)
			{
				this.playerInfo.playerType = PlayerType.usec;
			}
			if (ServerLeagueSystem.Team(base.UserID) <= 0)
			{
				this.playerInfo.playerType = PlayerType.spectactor;
			}
			this.ChooseTeamFromClientBase((int)this.playerInfo.playerType, false);
			Debug.Log("MatchStart");
		}
		else
		{
			this.serverPlayer.spectator_timer.Start();
		}
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0009A928 File Offset: 0x00098B28
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void PlayHit(int playerID, int targetID, float health, float armor, float power)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (base.Health == health && Main.IsTeamGame && !Peer.HardcoreMode)
		{
			return;
		}
		if (this.ID == targetID)
		{
			this.Hit(playerID, targetID, health, armor);
			if (base.UserInfo.skillUnlocked(Skills.att1))
			{
				EventFactory.Call("AddHit", playerID);
			}
		}
		else
		{
			for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
			{
				EntityNetPlayer entityNetPlayer = Peer.ClientGame.AllPlayers[i];
				if (!entityNetPlayer.IsDeadOrSpectactor)
				{
					if (!(entityNetPlayer.Position == CVars.h_v3infinity))
					{
						if (!(entityNetPlayer == Peer.ClientGame.LocalPlayer.Entity))
						{
							if (entityNetPlayer.ID == targetID)
							{
								if (Peer.ClientGame.LocalPlayer.IsAlive)
								{
									entityNetPlayer.Hit(playerID, targetID, health, armor);
								}
								return;
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x0009AA54 File Offset: 0x00098C54
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void KillFromServer(string boneToBeWild, Vector3 bonePower)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (base.IsPlayerObject)
		{
			Forms.OnDie();
			EventFactory.Call("Die", null);
			EntityNetPlayer entity = (this as ClientNetPlayer).Entity;
			if (entity != null)
			{
				if (entity.PlayerObject != null)
				{
					this.mainCamera.transform.parent = Utility.FindHierarchy(entity.PlayerTransform, "EYE_OF_GOD");
					this.mainCamera.transform.localPosition = Vector3.zero;
					this.mainCamera.transform.localEulerAngles = Vector3.zero;
					this.mainCamera = null;
					entity.Kill(boneToBeWild, bonePower);
				}
				else
				{
					entity.Disappear();
				}
			}
		}
		this.DespawnPlayerObject();
		this.KillPlayer();
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x0009AB20 File Offset: 0x00098D20
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void RaiseWtaskFromServer(int index, int count, bool farmDetected)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Main.UserInfo.weaponsStates[index].wtaskCurrent = (float)count;
		if (farmDetected)
		{
			EventFactory.Call("WtaskFailed", new object[]
			{
				Main.UserInfo.weaponsStates[index].CurrentWeapon.WtaskName,
				count,
				Main.UserInfo.weaponsStates[index].CurrentWeapon.wtask.count
			});
			return;
		}
		if (count == Main.UserInfo.weaponsStates[index].CurrentWeapon.wtask.count)
		{
			EventFactory.Call("WtaskGained", Main.UserInfo.weaponsStates[index].CurrentWeapon.WtaskName);
			Main.AddDatabaseRequest<UnlockMasteringWtask>(new object[]
			{
				Main.UserInfo.userID,
				Main.UserInfo.weaponsStates[index].CurrentWeapon.type
			});
		}
		else
		{
			EventFactory.Call("Wtask", new object[]
			{
				Main.UserInfo.weaponsStates[index].CurrentWeapon.WtaskName,
				count,
				Main.UserInfo.weaponsStates[index].CurrentWeapon.wtask.count
			});
		}
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x0009AC78 File Offset: 0x00098E78
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void RaiseAchievementFromServer(int index, int count, bool farmDetected)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (farmDetected)
		{
			EventFactory.Call("AchievementFailed", index);
			return;
		}
		Main.UserInfo.achievementsInfos[index].current = count;
		string name = (count != Main.UserInfo.achievementsInfos[index].count) ? "Achievement" : "AchievementGained";
		EventFactory.Call(name, index);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x0009ACEC File Offset: 0x00098EEC
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void RaiseContractFromServer(int difficulty, int count, bool farmDetected)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (farmDetected)
		{
			EventFactory.Call("ContractFailed", new object[]
			{
				(ContractTaskType)difficulty,
				count
			});
		}
		else
		{
			switch (difficulty)
			{
			case 0:
				Main.UserInfo.contractsInfo.easyCounter = count;
				break;
			case 1:
				Main.UserInfo.contractsInfo.normalCounter = count;
				break;
			case 2:
				Main.UserInfo.contractsInfo.hardCounter = count;
				break;
			}
			EventFactory.Call("ContractGained", new object[]
			{
				(ContractTaskType)difficulty,
				count
			});
		}
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0009ADAC File Offset: 0x00098FAC
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void ArmStreakFromServer(int armStreakFrom)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (armStreakFrom == 0)
		{
			this.playerInfo.hasMortar = true;
			EventFactory.Call("Armstreak", (ArmstreakEnum)armStreakFrom);
		}
		if (armStreakFrom == 1)
		{
			this.playerInfo.hasSonar = true;
			EventFactory.Call("Armstreak", (ArmstreakEnum)armStreakFrom);
		}
		if (armStreakFrom == 2)
		{
			this.ammo.state.grenadeCount++;
		}
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x0009AE2C File Offset: 0x0009902C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void CaptureScreenFromServer(string reporterNick, int reporterID)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x0009AE3C File Offset: 0x0009903C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void KillInfoFromServer(int killerId, int method, bool headShot, int killedId, string boneToBeWild, Vector3 boneForce, int weaponKills = 0)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (killedId == this.playerInfo.playerID)
		{
			BannerGUI.KillerWeaponKillsCount = weaponKills;
		}
		string text = string.Empty;
		string text2 = "#ffffff";
		string text3 = string.Empty;
		string text4 = "#ffffff";
		EntityNetPlayer entityNetPlayer = null;
		EntityNetPlayer entityNetPlayer2 = null;
		for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
		{
			EntityNetPlayer entityNetPlayer3 = Peer.ClientGame.AllPlayers[i];
			if (entityNetPlayer3.ID == killerId)
			{
				text = entityNetPlayer3.Nick;
				text2 = entityNetPlayer3.NickColor;
				entityNetPlayer = entityNetPlayer3;
				if (killedId == this.playerInfo.playerID)
				{
					BannerGUI.KillerEntity = entityNetPlayer;
				}
			}
			if (entityNetPlayer3.ID == killedId)
			{
				text3 = entityNetPlayer3.Nick;
				text4 = entityNetPlayer3.NickColor;
				entityNetPlayer2 = entityNetPlayer3;
				if (!entityNetPlayer3.isPlayer)
				{
					entityNetPlayer3.Kill(boneToBeWild, boneForce);
				}
			}
		}
		if (!entityNetPlayer || !entityNetPlayer2)
		{
			return;
		}
		global::Console.print(string.Concat(new object[]
		{
			text,
			" killed ",
			text3,
			" with ",
			(Weapons)method
		}), Colors.Chat);
		if (method == 123)
		{
			killerId = -1;
			text = string.Empty;
		}
		bool isBear = entityNetPlayer.IsBear;
		bool isBear2 = entityNetPlayer2.IsBear;
		EventFactory.Call("AddBeef", new object[]
		{
			text,
			text2,
			method,
			headShot,
			text3,
			text4,
			isBear,
			isBear2
		});
		if (this.ID == killerId)
		{
			EventFactory.Call("KillBanner", new object[]
			{
				entityNetPlayer2.Nick,
				entityNetPlayer2.NickColor,
				entityNetPlayer2.Level,
				entityNetPlayer2.ClanTag
			});
			if (method != 122 && method != 126 && method != 124)
			{
				Main.UserInfo.userStats.weaponKills[method]++;
			}
		}
		else if (this.ID == killedId && killerId != -1)
		{
			KillData killData = new KillData();
			killData.headShot = headShot;
			if (method == 123)
			{
				killData.self = true;
			}
			else
			{
				killData.method = (Weapons)method;
			}
			try
			{
				bool flag = false;
				if (entityNetPlayer.Ammo.state.equiped == WeaponEquipedState.Primary)
				{
					flag = entityNetPlayer.Ammo.state.primaryMod;
				}
				if (entityNetPlayer.Ammo.state.equiped == WeaponEquipedState.Secondary)
				{
					flag = entityNetPlayer.Ammo.state.secondaryMod;
				}
				EventFactory.Call("DeadBanner", new object[]
				{
					killData.method,
					entityNetPlayer.Nick,
					entityNetPlayer.NickColor,
					entityNetPlayer.Level,
					entityNetPlayer.ClanTag,
					flag
				});
			}
			catch
			{
			}
		}
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x0009B1A4 File Offset: 0x000993A4
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void KillStreakFromServer(int killStreakFrom)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		EventFactory.Call("KillStreak", (KillStreakEnum)killStreakFrom);
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x0009B1D0 File Offset: 0x000993D0
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void ExpFromServer(float currentXP, float exp, float bonusExp)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Main.UserInfo.currentXP = currentXP;
		EventFactory.Call("Exp", new object[]
		{
			(int)exp,
			(int)bonusExp
		});
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x0009B21C File Offset: 0x0009941C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void LevelUpFromServer(float currentXP, int SP, int deltaSP)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Main.UserInfo.currentXP = currentXP;
		EventFactory.Call("LevelUp", new object[]
		{
			Main.UserInfo.getLevel(currentXP),
			deltaSP
		});
		Main.UserInfo.SP = SP;
		Main.UserInfo.RefreshPlayerLevel();
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x0009B28C File Offset: 0x0009948C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void MasteringExpFromServer(int exp)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Main.UserInfo.CurrentMpExp += exp;
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x0009B2AC File Offset: 0x000994AC
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void ServerReload()
	{
		this.ammo.ServerReload();
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x0009B2BC File Offset: 0x000994BC
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void MpFromServer(int currentMpExp)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Main.UserInfo.CurrentMpExp = currentMpExp;
		EventFactory.Call("GainMp", null);
		Main.UserInfo.Mastering.MasteringPoints++;
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x0009B304 File Offset: 0x00099504
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void AddAttackerFromServer(int id)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		EventFactory.Call("AddEnemy", id);
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x0009B324 File Offset: 0x00099524
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void MarkPlayerFromServer(int id)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		EventFactory.Call("AddEnemy", id);
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x0009B344 File Offset: 0x00099544
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void MarkPlayer(int id)
	{
		if (!Main.IsGameLoaded || !Peer.ServerGame.IsTeamGame)
		{
			return;
		}
		List<ServerNetPlayer> serverNetPlayers = Peer.ServerGame.ServerNetPlayers;
		ServerNetPlayer serverNetPlayer = null;
		foreach (ServerNetPlayer serverNetPlayer2 in serverNetPlayers)
		{
			if (serverNetPlayer2.ID == id)
			{
				serverNetPlayer = serverNetPlayer2;
			}
		}
		if (serverNetPlayer == null || serverNetPlayer.IsDeadOrSpectactor)
		{
			return;
		}
		bool isBear = serverNetPlayer.IsBear;
		foreach (ServerNetPlayer serverNetPlayer3 in serverNetPlayers)
		{
			if (serverNetPlayer3.IsBear != isBear && !(serverNetPlayer3 == serverNetPlayer) && !serverNetPlayer3.IsDeadOrSpectactor)
			{
				serverNetPlayer3.AddAttacker(id);
			}
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x0009B478 File Offset: 0x00099678
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void PlayerHilight(int id)
	{
		if (!Main.IsGameLoaded || !Peer.ServerGame.IsTeamGame)
		{
			return;
		}
		List<ServerNetPlayer> serverNetPlayers = Peer.ServerGame.ServerNetPlayers;
		ServerNetPlayer serverNetPlayer = this as ServerNetPlayer;
		if (serverNetPlayer != null)
		{
			foreach (ServerNetPlayer serverNetPlayer2 in serverNetPlayers)
			{
				if (serverNetPlayer2.ID == id)
				{
					serverNetPlayer2.Hightlight();
					if (!serverNetPlayer.hightlitedPlayers.Contains(serverNetPlayer2))
					{
						serverNetPlayer.Exp(5f, 0f, true);
						serverNetPlayer.hightlitedPlayers.Add(serverNetPlayer2);
					}
				}
			}
		}
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x0009B550 File Offset: 0x00099750
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void GameEventMessageFromServer(string nick, int infoInt, string msg)
	{
		EventFactory.Call("ChatMessage", new object[]
		{
			(ChatInfo)infoInt,
			nick,
			msg
		});
		global::Console.print(nick + ":" + msg, Colors.Chat);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x0009B598 File Offset: 0x00099798
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void UpdateInfo()
	{
		Main.AddDatabaseRequest<LoadProfile>(new object[0]);
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x0009B5A8 File Offset: 0x000997A8
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void MatchStartFromServer()
	{
		this.playerInfo.hasMortar = false;
		this.playerInfo.hasSonar = false;
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Forms.OnMatchStart();
		if (ClientLeagueSystem.IsLeagueGame)
		{
			(this as ClientNetPlayer).IsTeamChoosed = true;
			ClientLeagueSystem.MatchStarting();
		}
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x0009B5F8 File Offset: 0x000997F8
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void SendDataOnConnect(byte[] bytes)
	{
		if (ClientLeagueSystem.IsLeagueGame)
		{
			ClientLeagueSystem.SetMatchData(Encoding.UTF8.GetString(bytes));
		}
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x0009B614 File Offset: 0x00099814
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void MatchEndFromServer(byte[] bytes)
	{
		Peer.ClientGame.ClearEntities();
		this.DespawnPlayerObject();
		this.KillPlayer();
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (ClientLeagueSystem.IsLeagueGame)
		{
			ClientLeagueSystem.MatchEnd(Encoding.UTF8.GetString(bytes));
		}
		else
		{
			EventFactory.Call("ShowMatchEndResult", new object[]
			{
				Encoding.UTF8.GetString(bytes)
			});
		}
		Forms.OnMatchEnd();
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x0009B684 File Offset: 0x00099884
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void RoundStartFromServer()
	{
		Peer.ClientGame.ClearEntities();
		this.DespawnPlayerObject();
		this.KillPlayer();
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Forms.OnRoundStart();
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x0009B6B8 File Offset: 0x000998B8
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void RoundEndFromServer()
	{
		for (int i = 0; i < Peer.ClientGame.AllPlayers.Count; i++)
		{
			Peer.ClientGame.AllPlayers[i].Disappear();
		}
		Peer.ClientGame.ClearEntities();
		this.DespawnPlayerObject();
		this.KillPlayer();
		base.StopCoroutine("MortarExplosionEnum");
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Forms.OnRoundEnd();
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x0009B72C File Offset: 0x0009992C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void BearWinsFromServer(bool bearWins)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		EventFactory.Call("ShowRoundEnd", bearWins);
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x0009B74C File Offset: 0x0009994C
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void EnablePlacementFromServer(int placementIndex)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		this.DisablePlacementFromServer();
		Peer.ClientGame.SpawnPlacement(placementIndex);
	}

	// Token: 0x06000D0D RID: 3341 RVA: 0x0009B76C File Offset: 0x0009996C
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void DisablePlacementFromServer()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Peer.ClientGame.DespawnPlacement();
	}

	// Token: 0x06000D0E RID: 3342 RVA: 0x0009B784 File Offset: 0x00099984
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void RadioFromClient(int radio)
	{
		Peer.ServerGame.Radio((RadioEnum)radio, base.IsBear);
	}

	// Token: 0x06000D0F RID: 3343 RVA: 0x0009B7A4 File Offset: 0x000999A4
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void RadioFromServer(int radio)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		PoolItem component = base.GetComponent<PoolItem>();
		if (base.IsAlive)
		{
			component = this.mainCamera.GetComponent<PoolItem>();
		}
		if (this.clientPlayer != null)
		{
			if (radio == 14)
			{
				this.clientPlayer.PlayTDRadio("round_start", component);
			}
			if (radio == 13)
			{
				EventFactory.Call("Beep", null);
			}
			if (radio == 18)
			{
				this.clientPlayer.PlayChatRadio("bear_hurt1", component);
			}
			if (radio == 19)
			{
				this.clientPlayer.PlayChatRadio("bear_hurt2", component);
			}
			if (radio == 20)
			{
				this.clientPlayer.PlayChatRadio("bear_needhelp", component);
			}
			if (radio == 21)
			{
				this.clientPlayer.PlayChatRadio("bear_needhelp2", component);
			}
			if (radio == 22)
			{
				this.clientPlayer.PlayChatRadio("bear_empty1", component);
			}
			if (radio == 23)
			{
				this.clientPlayer.PlayChatRadio("bear_empty2", component);
			}
			if (radio == 24)
			{
				this.clientPlayer.PlayChatRadio("bear_grenade", component);
			}
			if (radio == 25)
			{
				this.clientPlayer.PlayChatRadio("bear_grenade2", component);
			}
			if (radio == 26)
			{
				this.clientPlayer.PlayChatRadio("bear_killconfirmed1", component);
			}
			if (radio == 27)
			{
				this.clientPlayer.PlayChatRadio("bear_killconfirmed2", component);
			}
			if (radio == 28)
			{
				this.clientPlayer.PlayChatRadio("bear_killconfirmed3", component);
			}
			if (radio == 29)
			{
				this.clientPlayer.PlayChatRadio("bear_teammatedown", component);
			}
			if (radio == 30)
			{
				this.clientPlayer.PlayChatRadio("bear_clear1", component);
			}
			if (radio == 31)
			{
				this.clientPlayer.PlayChatRadio("bear_clear2", component);
			}
			if (radio == 32)
			{
				this.clientPlayer.PlayChatRadio("bear_coverme1", component);
			}
			if (radio == 33)
			{
				this.clientPlayer.PlayChatRadio("bear_coverme2", component);
			}
			if (radio == 34)
			{
				this.clientPlayer.PlayChatRadio("bear_eyesopen1", component);
			}
			if (radio == 35)
			{
				this.clientPlayer.PlayChatRadio("bear_eyesopen2", component);
			}
			if (radio == 36)
			{
				this.clientPlayer.PlayChatRadio("bear_followme1", component);
			}
			if (radio == 37)
			{
				this.clientPlayer.PlayChatRadio("bear_followme2", component);
			}
			if (radio == 38)
			{
				this.clientPlayer.PlayChatRadio("bear_gogogo1", component);
			}
			if (radio == 39)
			{
				this.clientPlayer.PlayChatRadio("bear_gogogo2", component);
			}
			if (radio == 40)
			{
				this.clientPlayer.PlayChatRadio("bear_goodwork1", component);
			}
			if (radio == 41)
			{
				this.clientPlayer.PlayChatRadio("bear_goodwork2", component);
			}
			if (radio == 42)
			{
				this.clientPlayer.PlayChatRadio("bear_negative1", component);
			}
			if (radio == 43)
			{
				this.clientPlayer.PlayChatRadio("bear_negative2", component);
			}
			if (radio == 44)
			{
				this.clientPlayer.PlayChatRadio("bear_rogerthat1", component);
			}
			if (radio == 45)
			{
				this.clientPlayer.PlayChatRadio("bear_rogerthat2", component);
			}
			if (radio == 46)
			{
				this.clientPlayer.PlayChatRadio("bear_stop1", component);
			}
			if (radio == 47)
			{
				this.clientPlayer.PlayChatRadio("bear_stop2", component);
			}
		}
		if (Main.IsTargetDesignation)
		{
			if (radio == 0)
			{
				this.clientPlayer.PlayTDRadio("01_bear_beacon_request", component);
			}
			if (radio == 1 && Peer.ClientGame.Placement)
			{
				this.clientPlayer.PlayRadio(SingletoneForm<SoundFactory>.Instance.placementSounds[(int)Peer.ClientGame.Placement.PointType].request, component);
			}
			if (radio == 2 && Peer.ClientGame.Placement)
			{
				this.clientPlayer.PlayRadio(SingletoneForm<SoundFactory>.Instance.placementSounds[(int)Peer.ClientGame.Placement.PointType].recieved, component);
			}
			if (radio == 3)
			{
				this.clientPlayer.PlayTDRadio("04_bear_beacon_placing", component);
			}
			if (radio == 4)
			{
				this.clientPlayer.PlayTDRadio("05_bear_beacon_placing2_bomber", component);
			}
			if (radio == 5)
			{
				this.clientPlayer.PlayTDRadio("06_bear_beacon_placing3", component);
			}
			if (radio == 7)
			{
				this.clientPlayer.PlayTDRadio("07_bear_beacon_success_bomber", component);
			}
			if (radio == 8)
			{
				this.clientPlayer.PlayTDRadio("08_bear_beacon_success2", component);
			}
			if (radio == 6)
			{
				this.clientPlayer.PlayTDRadio("07_bear_beacon_failed_bomber", component);
			}
			if (radio == 10)
			{
				this.clientPlayer.PlayTDRadio("beacon_danger", component);
			}
			if (radio == 11)
			{
				this.clientPlayer.PlayTDRadio("beacon_deployed_3rd", component);
			}
			if (radio == 12)
			{
				this.clientPlayer.PlayTDRadio("beacon_difused_3rd", component);
			}
		}
		else if (Main.IsTeamElimination)
		{
			if (radio == 16)
			{
				this.clientPlayer.PlayTERadio(((RadioEnum)radio).ToString(), component);
			}
			if (radio == 17)
			{
				this.clientPlayer.PlayTERadio(((RadioEnum)radio).ToString(), component);
			}
		}
		else if (Main.IsTacticalConquest)
		{
			if (radio == 48)
			{
				this.clientPlayer.PlayTCRadio("point_start2_capture", component);
			}
			if (radio == 49)
			{
				this.clientPlayer.PlayTCRadio("point_friendly_recapturing_loop", component);
			}
			if (radio == 50)
			{
				this.clientPlayer.PlayTCRadio("point_enemy_recapturing", component);
			}
			if (radio == 51)
			{
				this.clientPlayer.PlayTCRadio("point_finish_capture", component);
			}
			if (radio == 52)
			{
				this.clientPlayer.PlayTCRadio("point_captured", component);
			}
		}
	}

	// Token: 0x06000D10 RID: 3344 RVA: 0x0009BD68 File Offset: 0x00099F68
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void TacticalExplosionFromServer(Vector3 pos, int placementIndex)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		this.DespawnPlayerObject();
		this.KillPlayer();
		base.StartCoroutine(this.TacticalExplosionCamera(pos, placementIndex));
	}

	// Token: 0x06000D11 RID: 3345 RVA: 0x0009BD9C File Offset: 0x00099F9C
	[Obfuscation(Exclude = true)]
	protected virtual IEnumerator TacticalExplosionCamera(Vector3 pos, int placementIndex)
	{
		this.clientPlayer.ExplosionCamera = SingletoneForm<PoolManager>.Instance["placement_camera_" + placementIndex].Spawn();
		CameraListener.ChangeTo(this.clientPlayer.ExplosionCamera);
		Utility.ChangeParent(this.clientPlayer.ExplosionCamera.transform, Main.Trash);
		this.clientPlayer.ExplosionCamera.rigidbody.Sleep();
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.4f));
		Audio.Play(Peer.ClientGame.Placement.transform.position, SingletoneForm<SoundFactory>.Instance.placementSounds[(int)Peer.ClientGame.Placement.PointType].explosion, -1f, -1f);
		EventFactory.Call("BannerShowed", null);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(1.7f));
		EventFactory.Call("Blink", pos);
		this.clientPlayer.ExplosionCamera.rigidbody.WakeUp();
		this.clientPlayer.ExplosionCamera.rigidbody.AddExplosionForce(400f, pos, 200f, 1.3f, ForceMode.Impulse);
		this.clientPlayer.ExplosionCamera.GetComponent<PoolItem>().AutoDespawn(21f);
		EventFactory.Call("ShowSpectactorCamera", 20f);
		yield break;
	}

	// Token: 0x06000D12 RID: 3346 RVA: 0x0009BDD4 File Offset: 0x00099FD4
	[RPC]
	[Obfuscation(Exclude = true)]
	public virtual void ExplosionFromServer(Vector3 pos)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (this.controller)
		{
			(this.controller as ClientMoveController).shaker.InitShake(pos);
		}
		Collider[] array = Physics.OverlapSphere(pos, 20f, 1 << LayerMask.NameToLayer("client_ragdoll"));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].rigidbody != null)
			{
				array[i].rigidbody.AddExplosionForce(50f, pos, 20f, 1.3f, ForceMode.Impulse);
			}
		}
		DecalsAndTracersData.CreateGrenade(pos);
	}

	// Token: 0x06000D13 RID: 3347 RVA: 0x0009BE78 File Offset: 0x0009A078
	[Obfuscation(Exclude = true)]
	[RPC]
	public virtual void MortarExplosionFromServer(string json)
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		Dictionary<string, object>[] array = ArrayUtility.ArrayFromJSON(json, "data");
		eVector3[] array2 = new eVector3[array.Length];
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i] = new eVector3();
			array2[i].Convert(array[i], false);
		}
		base.StartCoroutine("MortarExplosionEnum", array2);
	}

	// Token: 0x06000D14 RID: 3348 RVA: 0x0009BEDC File Offset: 0x0009A0DC
	protected virtual IEnumerator MortarExplosionEnum(object obj)
	{
		eVector3[] poses = (eVector3[])obj;
		yield return new WaitForSeconds(2f);
		Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.mortarClips[0], -1f, -1f);
		yield return new WaitForSeconds(3f);
		Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.mortarClips[1], -1f, -1f);
		yield return new WaitForSeconds(1f);
		Audio.Play(base.transform.position, SingletoneForm<SoundFactory>.Instance.mortarClips[2], -1f, -1f);
		yield return new WaitForSeconds(1f);
		for (int i = 0; i < poses.Length; i++)
		{
			DecalsAndTracersData.CreateTracerFromTheSky(poses[i].Value);
			yield return new WaitForSeconds(0.1f);
			yield return new WaitForFixedUpdate();
			DecalsAndTracersData.CreateGrenade(poses[i].Value);
			if (this.controller)
			{
				(this.controller as ClientMoveController).shaker.InitShake(poses[i].Value);
			}
			Collider[] colliders = Physics.OverlapSphere(this.Position, 10f, 1 << LayerMask.NameToLayer("client_ragdoll"));
			for (int x = 0; x < colliders.Length; x++)
			{
				if (colliders[x].rigidbody != null)
				{
					colliders[x].rigidbody.AddExplosionForce(50f, this.Position, 10f, 1.3f, ForceMode.Impulse);
				}
			}
			yield return new WaitForSeconds(0.1f);
		}
		yield break;
	}

	// Token: 0x04000E21 RID: 3617
	protected eNetworkPlayer networkPlayer;

	// Token: 0x04000E22 RID: 3618
	protected NetworkViewID myID;

	// Token: 0x04000E23 RID: 3619
	protected NetworkViewID targetID;

	// Token: 0x04000E24 RID: 3620
	protected eNetworkView myView;

	// Token: 0x04000E25 RID: 3621
	protected eNetworkView targetView;

	// Token: 0x04000E26 RID: 3622
	protected int group = IDUtil.NoID;

	// Token: 0x04000E27 RID: 3623
	[HideInInspector]
	public bool remoteClientLoaded;
}
