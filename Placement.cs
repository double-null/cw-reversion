using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002A6 RID: 678
[AddComponentMenu("Scripts/Game/Foundation/Placement")]
public class Placement : PoolableBehaviour
{
	// Token: 0x0600130A RID: 4874 RVA: 0x000CDF50 File Offset: 0x000CC150
	public override void OnPoolDespawn()
	{
		this._player = null;
		this._endTime = -1f;
		this.plaecementProgress = 0f;
		this.state = PlacementState.preparing;
		if (this._beacon)
		{
			SingletoneForm<PoolManager>.Instance[this._beacon.name].Despawn(this._beacon.GetComponent<PoolItem>());
			this._beacon = null;
		}
		this._placmentSpamTimer = new eTimer();
		base.collider.enabled = false;
		base.OnPoolDespawn();
	}

	// Token: 0x170002B3 RID: 691
	// (get) Token: 0x0600130B RID: 4875 RVA: 0x000CDFDC File Offset: 0x000CC1DC
	// (set) Token: 0x0600130C RID: 4876 RVA: 0x000CDFF8 File Offset: 0x000CC1F8
	public int ElapsedTime
	{
		get
		{
			return (int)Mathf.Max(this._endTime - Time.realtimeSinceStartup, 0f);
		}
		set
		{
			this._endTime = Time.realtimeSinceStartup + (float)value;
		}
	}

	// Token: 0x170002B4 RID: 692
	// (get) Token: 0x0600130D RID: 4877 RVA: 0x000CE008 File Offset: 0x000CC208
	// (set) Token: 0x0600130E RID: 4878 RVA: 0x000CE044 File Offset: 0x000CC244
	public int PlayerID
	{
		get
		{
			if (this._player == null)
			{
				return IDUtil.NoID;
			}
			if (this.state == PlacementState.bombed)
			{
				return IDUtil.NoID2;
			}
			return this._player.ID;
		}
		set
		{
			if (value == Peer.ClientGame.LocalPlayer.ID)
			{
				this._player = Peer.ClientGame.LocalPlayer;
			}
			else
			{
				this._player = null;
			}
		}
	}

	// Token: 0x170002B5 RID: 693
	// (get) Token: 0x0600130F RID: 4879 RVA: 0x000CE088 File Offset: 0x000CC288
	public bool IsPlacing
	{
		get
		{
			return this.state == PlacementState.placing;
		}
	}

	// Token: 0x170002B6 RID: 694
	// (get) Token: 0x06001310 RID: 4880 RVA: 0x000CE094 File Offset: 0x000CC294
	public bool IsDeplacing
	{
		get
		{
			return this.state == PlacementState.deplacing;
		}
	}

	// Token: 0x170002B7 RID: 695
	// (get) Token: 0x06001311 RID: 4881 RVA: 0x000CE0A0 File Offset: 0x000CC2A0
	public bool IsReadyOrPlacing
	{
		get
		{
			return this.state == PlacementState.ready || this.state == PlacementState.placing;
		}
	}

	// Token: 0x170002B8 RID: 696
	// (get) Token: 0x06001312 RID: 4882 RVA: 0x000CE0BC File Offset: 0x000CC2BC
	public bool IsWaitingOrDeplacing
	{
		get
		{
			return this.state == PlacementState.waiting_bomber || this.state == PlacementState.deplacing;
		}
	}

	// Token: 0x170002B9 RID: 697
	// (get) Token: 0x06001313 RID: 4883 RVA: 0x000CE0D8 File Offset: 0x000CC2D8
	internal ServerEntity Beacon
	{
		get
		{
			return this._beacon;
		}
	}

	// Token: 0x06001314 RID: 4884 RVA: 0x000CE0E0 File Offset: 0x000CC2E0
	internal void StartBeacon(ServerNetPlayer user, bool difuse)
	{
		if (difuse)
		{
			if (this.state != PlacementState.waiting_bomber)
			{
				return;
			}
			this.state = PlacementState.deplacing;
		}
		else
		{
			if (this.state != PlacementState.ready)
			{
				return;
			}
			this.state = PlacementState.placing;
			this.plaecementProgress = 0f;
		}
		this._player = user;
	}

	// Token: 0x06001315 RID: 4885 RVA: 0x000CE134 File Offset: 0x000CC334
	internal void StopBeacon(ServerNetPlayer user, bool difuse)
	{
		if (this._player != user)
		{
			return;
		}
		if (difuse)
		{
			if (this.state != PlacementState.deplacing)
			{
				return;
			}
			this.state = PlacementState.waiting_bomber;
			this.plaecementProgress = 1f;
		}
		else
		{
			if (this.state != PlacementState.placing)
			{
				return;
			}
			this.state = PlacementState.ready;
			this.plaecementProgress = 0f;
		}
	}

	// Token: 0x06001316 RID: 4886 RVA: 0x000CE19C File Offset: 0x000CC39C
	internal void Advance(ServerNetPlayer user, bool difuse)
	{
		if (difuse)
		{
			if (this._player != user)
			{
				return;
			}
			if (this.state != PlacementState.deplacing)
			{
				return;
			}
			this.plaecementProgress -= CVars.g_one_div_tickrate / Main.GameModeInfo.beaconDePlacing * this._player.difuseMult;
			this.plaecementProgress = Mathf.Clamp01(this.plaecementProgress);
			if (this.plaecementProgress == 0f && this._beacon)
			{
				this.DiffuseBeacon();
			}
		}
		else
		{
			if (this.state != PlacementState.placing)
			{
				return;
			}
			if (this._player != user)
			{
				return;
			}
			if (this._placmentSpamTimer.Elapsed > 5f)
			{
				this._placmentSpamTimer.Stop();
			}
			if (this.plaecementProgress == 0f && this._placmentSpamTimer.Elapsed == 0f)
			{
				Peer.ServerGame.Radio(RadioEnum.beacon_placing, Main.GameModeInfo.isBearPlacement);
				this._placmentSpamTimer.Start();
			}
			this.plaecementProgress += CVars.g_one_div_tickrate / Main.GameModeInfo.beaconPlacing * this._player.plantMult;
			this.plaecementProgress = Mathf.Clamp01(this.plaecementProgress);
			if (this.plaecementProgress == 1f && !this._beacon)
			{
				this.PlaceBeacon();
			}
		}
	}

	// Token: 0x06001317 RID: 4887 RVA: 0x000CE318 File Offset: 0x000CC518
	private void PlaceBeacon()
	{
		Peer.ServerGame.EventMessage(this._player.Nick, ChatInfo.gameflow_message, Language.PlayerPlacedMarker);
		Peer.ServerGame.Radio(RadioEnum.beacon_deployed);
		this.state = PlacementState.waiting_bomber;
		this._endTime = Time.realtimeSinceStartup;
		base.collider.enabled = false;
		this._beacon = SingletoneForm<PoolManager>.Instance["server_beacon"].Spawn().GetComponent<ServerEntity>();
		this._beacon.transform.parent = Peer.ServerGame.transform;
		this._beacon.state.type = EntityType.beacon;
		this._beacon.transform.FindChild("diffuse").GetComponent<Collider>().isTrigger = true;
		this._beacon.transform.position = this._player.Position + Vector3.up * 0.3f + this._player.Controller.rootCamera.forward * 0.5f;
		this._beacon.rigidbody.velocity = Vector3.zero;
		this._beacon.rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		this._beacon.state.ID = Peer.ServerGame.NextEntityID;
		this._beacon.Init();
		Peer.ServerGame.AddEntity(this._beacon);
		ServerNetPlayer serverNetPlayer = this._player as ServerNetPlayer;
		if (serverNetPlayer != null)
		{
			serverNetPlayer.Exp(Globals.I.BeaconExp, 0f, true);
		}
		base.StartCoroutine(this.Launch());
	}

	// Token: 0x06001318 RID: 4888 RVA: 0x000CE4C0 File Offset: 0x000CC6C0
	private void DiffuseBeacon()
	{
		Peer.ServerGame.EventMessage(this._player.Nick, ChatInfo.gameflow_message, Language.PlayerDifuseMarker);
		ServerNetPlayer serverNetPlayer = this._player as ServerNetPlayer;
		if (serverNetPlayer != null)
		{
			if (this.ElapsedTime <= 5)
			{
				serverNetPlayer.Exp(Globals.I.BeaconExp * 1.5f * 2f, 0f, true);
			}
			else
			{
				serverNetPlayer.Exp(Globals.I.BeaconExp * 1.5f, 0f, true);
			}
			ContractsEngine.DisarmExplode(serverNetPlayer, (Maps)Main.HostInfo.MapIndex, BeaconAction.disarm);
		}
		base.StopAllCoroutines();
		this.Disable();
		Peer.ServerGame.Radio(RadioEnum.beacon_difuse);
		Peer.ServerGame.Radio(RadioEnum.beacon_failed_bomber, Main.GameModeInfo.isBearPlacement);
		Peer.ServerGame.RoundPreEnd();
		Peer.ServerGame.BearWins(!Main.GameModeInfo.isBearPlacement);
		this._player = null;
	}

	// Token: 0x06001319 RID: 4889 RVA: 0x000CE5B8 File Offset: 0x000CC7B8
	private IEnumerator Launch()
	{
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(30f));
		this._endTime = Time.realtimeSinceStartup + Main.GameModeInfo.beaconTimer;
		Peer.ServerGame.Radio(RadioEnum.beacon_placing2_bomber, Main.GameModeInfo.isBearPlacement);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(7f));
		Peer.ServerGame.Radio(RadioEnum.beacon_placing3, Main.GameModeInfo.isBearPlacement);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(5f));
		Peer.ServerGame.Radio(RadioEnum.beacon_succeed_bomber, Main.GameModeInfo.isBearPlacement);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(6f));
		Peer.ServerGame.Radio(RadioEnum.beacon_danger, Main.GameModeInfo.isBearPlacement);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(12f));
		ServerNetPlayer p = this._player as ServerNetPlayer;
		if (p != null)
		{
			p.Exp(20f, 0f, true);
			ContractsEngine.DisarmExplode(p, (Maps)Main.HostInfo.MapIndex, BeaconAction.explode);
		}
		this.Disable();
		Peer.ServerGame.TacticalExplosion(base.transform.position);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(3f));
		Peer.ServerGame.Radio(RadioEnum.beacon_succeed2, Main.GameModeInfo.isBearPlacement);
		Peer.ServerGame.RoundPreEnd();
		Peer.ServerGame.BearWins(Main.GameModeInfo.isBearPlacement);
		this.Clear();
		yield break;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x000CE5D4 File Offset: 0x000CC7D4
	public void Disable()
	{
		this.plaecementProgress = 0f;
		if (this._beacon)
		{
			Peer.ServerGame.RemoveEntity(this._beacon.GetComponent<ServerEntity>());
			this._beacon = null;
		}
		base.collider.enabled = false;
		this._player = null;
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x000CE62C File Offset: 0x000CC82C
	public void Clear()
	{
		this.state = PlacementState.none;
		base.StopAllCoroutines();
		this.Disable();
	}

	// Token: 0x0600131C RID: 4892 RVA: 0x000CE644 File Offset: 0x000CC844
	public void Enable()
	{
		this.Clear();
		base.collider.enabled = true;
		this.state = PlacementState.preparing;
	}

	// Token: 0x0600131D RID: 4893 RVA: 0x000CE660 File Offset: 0x000CC860
	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(base.transform.position, "placement.png");
	}

	// Token: 0x040015FF RID: 5631
	public PlacementType PointType;

	// Token: 0x04001600 RID: 5632
	[HideInInspector]
	public float plaecementProgress;

	// Token: 0x04001601 RID: 5633
	[HideInInspector]
	public PlacementState state = PlacementState.preparing;

	// Token: 0x04001602 RID: 5634
	private BaseRpcNetPlayer _player;

	// Token: 0x04001603 RID: 5635
	private ServerEntity _beacon;

	// Token: 0x04001604 RID: 5636
	private eTimer _placmentSpamTimer = new eTimer();

	// Token: 0x04001605 RID: 5637
	private float _endTime = -1f;
}
