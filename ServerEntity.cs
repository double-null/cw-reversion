using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020002EC RID: 748
[AddComponentMenu("Scripts/Game/ServerEntity")]
internal class ServerEntity : BaseEntity
{
	// Token: 0x060014CB RID: 5323 RVA: 0x000DB878 File Offset: 0x000D9A78
	public override void OnPoolDespawn()
	{
		this.player = null;
		this.IDs.Clear();
		base.transform.position = Vector3.zero;
		base.transform.eulerAngles = Vector3.zero;
		if (base.rigidbody)
		{
			base.rigidbody.velocity = Vector3.zero;
			base.rigidbody.constraints = RigidbodyConstraints.None;
		}
		base.OnPoolDespawn();
	}

	// Token: 0x060014CC RID: 5324 RVA: 0x000DB8EC File Offset: 0x000D9AEC
	public void Init()
	{
		this.state.pos = base.transform.position;
		this.state.euler = base.transform.eulerAngles;
		this.state.ID = Peer.ServerGame.NextEntityID;
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x000DB93C File Offset: 0x000D9B3C
	public void StartAsPlacement()
	{
		this.Init();
		base.StartCoroutine(this.PlacementStartEnum());
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x000DB954 File Offset: 0x000D9B54
	public void Init(ServerNetPlayer player, Vector3 pos, Vector3 dir, float power = 1f)
	{
		this.player = player;
		this.state.playerID = player.ID;
		base.transform.position = pos;
		base.rigidbody.velocity = dir * 20f * power;
		this.Init();
		if (this.state.type == EntityType.grenade)
		{
			base.StartCoroutine(this.GrenadeExplosion());
		}
		if (this.state.type == EntityType.mortar)
		{
			base.StartCoroutine(this.MortarStart());
		}
		if (this.state.type == EntityType.sonar)
		{
			base.StartCoroutine(this.SonarStart());
		}
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x000DBA08 File Offset: 0x000D9C08
	private IEnumerator PlacementStartEnum()
	{
		this.state.placementState = PlacementState.preparing;
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(2f));
		Peer.ServerGame.Radio(RadioEnum.beacon_request_start, Main.GameModeInfo.isBearPlacement);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(18f));
		Peer.ServerGame.Radio(RadioEnum.beacon_request_bomber, Main.GameModeInfo.isBearPlacement);
		this.state.marker = true;
		Peer.ServerGame.Placement.state = PlacementState.ready;
		this.state.placementState = PlacementState.ready;
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(7f));
		Peer.ServerGame.Radio(RadioEnum.beacon_recieved, Main.GameModeInfo.isBearPlacement);
		yield break;
	}

	// Token: 0x060014D0 RID: 5328 RVA: 0x000DBA24 File Offset: 0x000D9C24
	private IEnumerator GrenadeExplosion()
	{
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(3f));
		Peer.ServerGame.Explosion(base.transform.position);
		Peer.ServerGame.GrenadeExplosion(this.player, base.transform.position + Vector3.up * 0.1f);
		Peer.ServerGame.RemoveEntity(this);
		yield break;
	}

	// Token: 0x060014D1 RID: 5329 RVA: 0x000DBA40 File Offset: 0x000D9C40
	private IEnumerator MortarStart()
	{
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(3f));
		this.state.marker = true;
		this.player.Stats.armstreaksUsed++;
		Peer.ServerGame.MortarExplosion(this.player, base.transform.position);
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(2f));
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(3f));
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(1f));
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.1f));
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.1f));
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.1f));
		Peer.ServerGame.RemoveEntity(this);
		yield break;
	}

	// Token: 0x060014D2 RID: 5330 RVA: 0x000DBA5C File Offset: 0x000D9C5C
	private IEnumerator SonarStart()
	{
		this.player.Stats.armstreaksUsed++;
		yield return base.StartCoroutine(this.WaitForRealTimeSeconds(3f));
		float endTime = Time.realtimeSinceStartup + 20f;
		if (this.player.playerInfo.skillUnlocked(Skills.sonar2))
		{
			endTime += 20f;
		}
		if (this.player.playerInfo.clanSkillUnlocked(Cl_Skills.cl_scout1) && this.player.PlayerClass == PlayerClass.scout)
		{
			endTime += 10f;
		}
		this.state.marker = true;
		this.state.sonarTime = (int)(endTime - Time.realtimeSinceStartup);
		while (endTime > Time.realtimeSinceStartup)
		{
			yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.5f));
			this.state.sonarTime = (int)(endTime - Time.realtimeSinceStartup);
			yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.5f));
			this.state.sonarTime = (int)(endTime - Time.realtimeSinceStartup);
			yield return base.StartCoroutine(this.WaitForRealTimeSeconds(0.5f));
			this.state.sonarTime = (int)(endTime - Time.realtimeSinceStartup);
			Peer.ServerGame.SonarTick(this.player, this, this.IDs);
		}
		Peer.ServerGame.RemoveEntity(this);
		yield break;
	}

	// Token: 0x060014D3 RID: 5331 RVA: 0x000DBA78 File Offset: 0x000D9C78
	public override void CallLateUpdate()
	{
		base.CallLateUpdate();
		this.state.pos = base.transform.position;
		this.state.euler = base.transform.eulerAngles;
		if (this.state.type == EntityType.placement)
		{
			this.state.placementState = Peer.ServerGame.Placement.state;
			this.state.playerID = Peer.ServerGame.Placement.PlayerID;
			this.state.sonarTime = Peer.ServerGame.Placement.ElapsedTime;
			this.state.plaecementProgress = Peer.ServerGame.Placement.plaecementProgress;
		}
	}

	// Token: 0x04001960 RID: 6496
	private ServerNetPlayer player;

	// Token: 0x04001961 RID: 6497
	private List<int> IDs = new List<int>();
}
