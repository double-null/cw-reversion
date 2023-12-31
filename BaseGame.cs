using System;
using UnityEngine;

// Token: 0x020001A1 RID: 417
[AddComponentMenu("Scripts/Game/BaseGame")]
internal class BaseGame : Form
{
	// Token: 0x06000C31 RID: 3121 RVA: 0x000956F8 File Offset: 0x000938F8
	public void SpawnPlacement(int placementIndex)
	{
		this.placementIndex = placementIndex;
		this.placement = SingletoneForm<PoolManager>.Instance["placement_" + placementIndex].Spawn().GetComponent<Placement>();
		this.placement.transform.parent = base.transform;
		this.placement.Enable();
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00095758 File Offset: 0x00093958
	public void DespawnPlacement()
	{
		if (this.placement)
		{
			SingletoneForm<PoolManager>.Instance[this.placement.name].Despawn(this.placement.GetComponent<PoolItem>());
			this.placement = null;
		}
		this.placementIndex = -1;
	}

	// Token: 0x06000C33 RID: 3123 RVA: 0x000957A8 File Offset: 0x000939A8
	public override void OnPoolDespawn()
	{
		this.DespawnPlacement();
		this.state = MatchState.stoped;
		this.nextEventTime = -1f;
		this.serverStartTime = -1f;
		this.usecWinCount = 0;
		this.bearWinCount = 0;
		this.usecClanBuff = 0;
		this.bearClanBuff = 0;
		this.clanBuff = 0;
		base.OnPoolDespawn();
	}

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00095804 File Offset: 0x00093A04
	public int UsecWinCount
	{
		get
		{
			return this.usecWinCount;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0009580C File Offset: 0x00093A0C
	public int BearWinCount
	{
		get
		{
			return this.bearWinCount;
		}
	}

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00095814 File Offset: 0x00093A14
	public Placement Placement
	{
		get
		{
			return this.placement;
		}
	}

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0009581C File Offset: 0x00093A1C
	public bool PlacementPreparing
	{
		get
		{
			return this.placement.state == PlacementState.preparing;
		}
	}

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0009582C File Offset: 0x00093A2C
	public bool PlacementReady
	{
		get
		{
			return this.placement.state == PlacementState.ready;
		}
	}

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0009583C File Offset: 0x00093A3C
	public bool PlacementPlacing
	{
		get
		{
			return this.placement.state == PlacementState.placing;
		}
	}

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06000C3A RID: 3130 RVA: 0x0009584C File Offset: 0x00093A4C
	public bool PlacementWaitingBomber
	{
		get
		{
			return this.placement.state == PlacementState.waiting_bomber;
		}
	}

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0009585C File Offset: 0x00093A5C
	public bool PlacementDeplacing
	{
		get
		{
			return this.placement.state == PlacementState.deplacing;
		}
	}

	// Token: 0x1700017A RID: 378
	// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0009586C File Offset: 0x00093A6C
	public bool PlacementBombed
	{
		get
		{
			return this.placement.state == PlacementState.bombed;
		}
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0009587C File Offset: 0x00093A7C
	public MatchState MatchState
	{
		get
		{
			return this.state;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000C3E RID: 3134 RVA: 0x00095884 File Offset: 0x00093A84
	public float ElapsedNextEventTime
	{
		get
		{
			return Mathf.Max(this.nextEventTime - Time.realtimeSinceStartup, 0f);
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0009589C File Offset: 0x00093A9C
	protected bool OnNextTimeEvent
	{
		get
		{
			return Time.realtimeSinceStartup > this.nextEventTime;
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06000C40 RID: 3136 RVA: 0x000958AC File Offset: 0x00093AAC
	public float ServerTime
	{
		get
		{
			return Time.realtimeSinceStartup - this.serverStartTime;
		}
	}

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06000C41 RID: 3137 RVA: 0x000958BC File Offset: 0x00093ABC
	public bool isInFirstTenSeconds
	{
		get
		{
			return this.nextEventTime - Time.realtimeSinceStartup > Main.GameModeInfo.matchRoundTime - 10f;
		}
	}

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06000C42 RID: 3138 RVA: 0x000958F0 File Offset: 0x00093AF0
	public float MatchPassedTime
	{
		get
		{
			return Main.GameModeInfo.matchRoundTime - this.ElapsedNextEventTime;
		}
	}

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06000C43 RID: 3139 RVA: 0x00095904 File Offset: 0x00093B04
	public virtual bool IsTeamGame
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x06000C44 RID: 3140 RVA: 0x00095908 File Offset: 0x00093B08
	public virtual bool IsRounedGame
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x0009590C File Offset: 0x00093B0C
	public virtual void Serialize(eNetworkStream stream)
	{
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00095910 File Offset: 0x00093B10
	public virtual void Deserialize(eNetworkStream stream)
	{
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00095914 File Offset: 0x00093B14
	public void AddClanBuff(Buffs buff, int team = 0)
	{
		if (team == 1)
		{
			this.bearClanBuff |= (int)buff;
		}
		else if (team == 2)
		{
			this.usecClanBuff |= (int)buff;
		}
		else
		{
			this.clanBuff |= (int)buff;
		}
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00095964 File Offset: 0x00093B64
	public override void OnConnected()
	{
		int num = LayerMask.NameToLayer("triggers");
		Collider[] array = (Collider[])UnityEngine.Object.FindSceneObjectsOfType(typeof(Collider));
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i].gameObject.layer == num)
			{
				array[i].isTrigger = true;
			}
		}
		base.OnConnected();
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x000959C8 File Offset: 0x00093BC8
	public override void MainInitialize()
	{
		this.isUpdating = true;
		base.MainInitialize();
	}

	// Token: 0x04000DCA RID: 3530
	protected Placement placement;

	// Token: 0x04000DCB RID: 3531
	protected int placementIndex = -1;

	// Token: 0x04000DCC RID: 3532
	protected int tacticalPointIndex = -1;

	// Token: 0x04000DCD RID: 3533
	protected MatchState state;

	// Token: 0x04000DCE RID: 3534
	protected float nextEventTime = -1f;

	// Token: 0x04000DCF RID: 3535
	protected float serverStartTime = -1f;

	// Token: 0x04000DD0 RID: 3536
	protected int usecWinCount;

	// Token: 0x04000DD1 RID: 3537
	protected int bearWinCount;

	// Token: 0x04000DD2 RID: 3538
	protected int usecClanBuff;

	// Token: 0x04000DD3 RID: 3539
	protected int bearClanBuff;

	// Token: 0x04000DD4 RID: 3540
	protected int clanBuff;
}
