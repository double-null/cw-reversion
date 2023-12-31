using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200019E RID: 414
[AddComponentMenu("Scripts/Game/BaseClientGame")]
internal class BaseClientGame : BaseGame
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00094940 File Offset: 0x00092B40
	// (set) Token: 0x06000C0C RID: 3084 RVA: 0x00094948 File Offset: 0x00092B48
	public ClientNetPlayer LocalPlayer { get; private set; }

	// Token: 0x06000C0D RID: 3085 RVA: 0x00094954 File Offset: 0x00092B54
	public override void OnPoolDespawn()
	{
		if (this.LocalPlayer)
		{
			SingletoneForm<PoolManager>.Instance[this.LocalPlayer.name].Despawn(this.LocalPlayer.GetComponent<PoolItem>());
			this.LocalPlayer = null;
		}
		foreach (EntityNetPlayer entityNetPlayer in this._clientEntityNetPlayers)
		{
			SingletoneForm<PoolManager>.Instance[entityNetPlayer.name].Despawn(entityNetPlayer.GetComponent<PoolItem>());
		}
		this._clientEntityNetPlayers.Clear();
		foreach (ClientEntity clientEntity in this.clientEntities)
		{
			SingletoneForm<PoolManager>.Instance[clientEntity.name].Despawn(clientEntity.GetComponent<PoolItem>());
		}
		this.clientEntities.Clear();
		foreach (BotNetPlayer botNetPlayer in this.bots)
		{
			SingletoneForm<PoolManager>.Instance[botNetPlayer.name].Despawn(botNetPlayer.GetComponent<PoolItem>());
		}
		this.bots.Clear();
		this._isFakeAdded = false;
		base.OnPoolDespawn();
	}

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00094B10 File Offset: 0x00092D10
	public bool IsFull
	{
		get
		{
			return this.PlayerCount >= Main.HostInfo.MaxPlayers;
		}
	}

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00094B28 File Offset: 0x00092D28
	protected int PlayerCount
	{
		get
		{
			int num = 0;
			foreach (EntityNetPlayer entityNetPlayer in this._clientEntityNetPlayers)
			{
				if (!entityNetPlayer.IsSpectactor)
				{
					if (!EntityNetPlayer.IsClientPlayer(entityNetPlayer.ID))
					{
						num++;
					}
				}
			}
			return num;
		}
	}

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00094BB8 File Offset: 0x00092DB8
	public List<EntityNetPlayer> AlivePlayers
	{
		get
		{
			List<EntityNetPlayer> list = new List<EntityNetPlayer>();
			foreach (EntityNetPlayer entityNetPlayer in this._clientEntityNetPlayers)
			{
				if (!entityNetPlayer.IsDeadOrSpectactor)
				{
					if (!(entityNetPlayer.Position == CVars.h_v3infinity))
					{
						if (!(entityNetPlayer == this.LocalPlayer.Entity))
						{
							if (this.IsTeamGame)
							{
								if (this.LocalPlayer.IsAlive || this.LocalPlayer.IsTeam(entityNetPlayer) || this.LocalPlayer.IsSpectactor)
								{
									list.Add(entityNetPlayer);
								}
							}
							else
							{
								list.Add(entityNetPlayer);
							}
						}
					}
				}
			}
			return list;
		}
	}

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00094CB4 File Offset: 0x00092EB4
	public List<EntityNetPlayer> AllPlayers
	{
		get
		{
			return this._clientEntityNetPlayers;
		}
	}

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00094CBC File Offset: 0x00092EBC
	public List<ClientEntity> AllEntities
	{
		get
		{
			return this.clientEntities;
		}
	}

	// Token: 0x06000C13 RID: 3091 RVA: 0x00094CC4 File Offset: 0x00092EC4
	public ClientEntity CreateClientEntity(int id, EntityType type)
	{
		this.LocalPlayer.FullUpdateRequest();
		ClientEntity clientEntity = null;
		if (type == EntityType.grenade)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_grenade"].Spawn().GetComponent<ClientEntity>();
		}
		if (type == EntityType.mortar)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_marker"].Spawn().GetComponent<ClientEntity>();
			StartData.weaponShaders.ReplaceEntityInvert(clientEntity.gameObject);
		}
		if (type == EntityType.sonar)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_marker"].Spawn().GetComponent<ClientEntity>();
			StartData.weaponShaders.ReplaceEntityInvert(clientEntity.gameObject);
		}
		if (type == EntityType.beacon)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_beacon"].Spawn().GetComponent<ClientEntity>();
		}
		if (type != EntityType.placement)
		{
			if (type == EntityType.mortar)
			{
				clientEntity.transform.FindChild("marker_red").renderer.enabled = true;
			}
			if (type == EntityType.sonar)
			{
				clientEntity.transform.FindChild("marker_green").renderer.enabled = true;
			}
		}
		else if (type == EntityType.placement)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_placement"].Spawn().GetComponent<ClientEntity>();
		}
		if (type == EntityType.tactical_point)
		{
			clientEntity = SingletoneForm<PoolManager>.Instance["client_tactical_point"].Spawn().GetComponent<ClientEntity>();
		}
		if (clientEntity == null)
		{
			return null;
		}
		Utility.SetLayerRecursively(clientEntity.gameObject, LayerMask.NameToLayer("Default"));
		clientEntity.transform.position = CVars.h_v3infinity;
		clientEntity.transform.parent = Peer.ClientGame.transform;
		return clientEntity;
	}

	// Token: 0x06000C14 RID: 3092 RVA: 0x00094E60 File Offset: 0x00093060
	public EntityNetPlayer CreateClientPlayerEntity(int id, EntityType type)
	{
		this.LocalPlayer.FullUpdateRequest();
		GameObject gameObject = SingletoneForm<PoolManager>.Instance["entity_user"].Spawn();
		gameObject.transform.parent = Main.Trash;
		EntityNetPlayer component = gameObject.GetComponent<EntityNetPlayer>();
		component.ID = id;
		component.UC = new ClientCmdCollector();
		return component;
	}

	// Token: 0x06000C15 RID: 3093 RVA: 0x00094EBC File Offset: 0x000930BC
	public override void Deserialize(eNetworkStream stream)
	{
		ArrayUtility.DeserializeList<EntityNetPlayer>(stream, this._clientEntityNetPlayers, new CreateForSyncList<EntityNetPlayer>(this.CreateClientPlayerEntity));
		ArrayUtility.DeserializeList<ClientEntity>(stream, this.clientEntities, new CreateForSyncList<ClientEntity>(this.CreateClientEntity));
		if (!this._isFakeAdded && Peer.ClientGame.LocalPlayer.Entity != null)
		{
			this._isFakeAdded = true;
			Peer.ClientGame.AddPlayers();
		}
	}

	// Token: 0x06000C16 RID: 3094 RVA: 0x00094F30 File Offset: 0x00093130
	public void AddPlayers()
	{
		CVars.ClientPlayerId = Peer.ClientGame.AllPlayers[0].ID - 3;
		for (int i = 0; i < CVars.ClientPlayersCount; i++)
		{
			EntityNetPlayer entityNetPlayer = this.CreateClientPlayerEntity(i, EntityType.player);
			entityNetPlayer.Init(Peer.ClientGame.LocalPlayer.Entity, i);
			this._clientEntityNetPlayers.Add(entityNetPlayer);
		}
	}

	// Token: 0x06000C17 RID: 3095 RVA: 0x00094FAC File Offset: 0x000931AC
	public void ClearEntities()
	{
		foreach (ClientEntity clientEntity in this.clientEntities)
		{
			SingletoneForm<PoolManager>.Instance[clientEntity.name].Despawn(clientEntity.GetComponent<PoolItem>());
		}
		this.clientEntities.Clear();
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x00095034 File Offset: 0x00093234
	public void BotMainInitialize(NetworkViewID myId, NetworkViewID targetId, int playerId, int group, float serverTime)
	{
		BotNetPlayer component = SingletoneForm<PoolManager>.Instance["client_botuser"].Spawn().GetComponent<BotNetPlayer>();
		component.transform.parent = base.transform;
		component.InitializeNetwork(new ClientCmdCollector(), myId, targetId, group);
		if (myId.owner != default(NetworkPlayer))
		{
			component.NetworkPlayer.owner = myId.owner;
		}
		component.CreateViews();
		component.EnableViews();
		component.playerInfo.playerID = playerId;
		component.ChooseAmmunition();
		component.update = UpdateType.FullUpdate;
		component.StartLoading();
		this.bots.Add(component);
	}

	// Token: 0x06000C19 RID: 3097 RVA: 0x000950E4 File Offset: 0x000932E4
	public void MainInitialize(NetworkViewID myId, NetworkViewID targetId, int playerId, int group, float serverTime)
	{
		base.MainInitialize();
		this.serverStartTime = serverTime;
		ClientNetPlayer component = SingletoneForm<PoolManager>.Instance["client_user"].Spawn().GetComponent<ClientNetPlayer>();
		component.transform.parent = base.transform;
		component.InitializeNetwork(new ClientCmdCollector(), myId, targetId, group);
		if (myId.owner != default(NetworkPlayer))
		{
			component.NetworkPlayer.owner = myId.owner;
		}
		component.CreateViews();
		component.EnableViews();
		component.playerInfo.playerID = playerId;
		component.playerInfo.Nick = Main.UserInfo.nick;
		component.playerInfo.NickColor = Main.UserInfo.nickColor;
		component.playerInfo.clanTag = Main.UserInfo.clanTag;
		component.playerInfo.skillsInfos = Main.UserInfo.skillArray;
		component.playerInfo.clanSkillsInfos = ((!ClientLeagueSystem.IsLeagueGame) ? Main.UserInfo.clanSkillArray : new bool[0]);
		component.ChooseAmmunition();
		component.update = UpdateType.FullUpdate;
		component.StartLoading();
		this.LocalPlayer = component;
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x0009521C File Offset: 0x0009341C
	public override void OnFixedUpdate()
	{
		this.LocalPlayer.CallFixedUpdate();
		BaseClientGame.UsecCount = 0;
		BaseClientGame.BearCount = 0;
		foreach (EntityNetPlayer entityNetPlayer in this._clientEntityNetPlayers)
		{
			entityNetPlayer.CallFixedUpdate();
			if (!entityNetPlayer.IsSpectactor)
			{
				if (entityNetPlayer.IsBear)
				{
					BaseClientGame.BearCount++;
				}
				else
				{
					BaseClientGame.UsecCount++;
				}
			}
		}
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x000952D0 File Offset: 0x000934D0
	public override void OnUpdate()
	{
		if (this.LocalPlayer != null)
		{
			this.LocalPlayer.CallUpdate();
		}
	}

	// Token: 0x06000C1C RID: 3100 RVA: 0x000952F0 File Offset: 0x000934F0
	public override void OnLateUpdate()
	{
		if (!Main.IsGameLoaded)
		{
			return;
		}
		if (this.LocalPlayer)
		{
			this.LocalPlayer.CallLateUpdate();
		}
		foreach (EntityNetPlayer entityNetPlayer in this._clientEntityNetPlayers)
		{
			entityNetPlayer.CallLateUpdate();
		}
		foreach (ClientEntity clientEntity in this.clientEntities)
		{
			clientEntity.CallLateUpdate();
		}
	}

	// Token: 0x04000DBE RID: 3518
	private List<EntityNetPlayer> _clientEntityNetPlayers = new List<EntityNetPlayer>();

	// Token: 0x04000DBF RID: 3519
	private List<ClientEntity> clientEntities = new List<ClientEntity>();

	// Token: 0x04000DC0 RID: 3520
	private List<BotNetPlayer> bots = new List<BotNetPlayer>();

	// Token: 0x04000DC1 RID: 3521
	public static int UsecCount;

	// Token: 0x04000DC2 RID: 3522
	public static int BearCount;

	// Token: 0x04000DC3 RID: 3523
	private bool _isFakeAdded;
}
