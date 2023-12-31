using System;
using UnityEngine;

// Token: 0x020001AB RID: 427
[AddComponentMenu("Scripts/Game/ClientEntity")]
internal class ClientEntity : BaseEntity
{
	// Token: 0x06000E60 RID: 3680 RVA: 0x000A7738 File Offset: 0x000A5938
	public override void OnPoolDespawn()
	{
		base.gameObject.layer = LayerMask.NameToLayer("Default");
		this.packets.Clear();
		if (base.name == "client_grenade" && Main.IsGameLoaded && Peer.ClientGame != null && Peer.ClientGame.LocalPlayer != null)
		{
			float num = Vector3.Distance(this.state.pos, Peer.ClientGame.LocalPlayer.Position);
			if (num < 10f)
			{
				Peer.ClientGame.LocalPlayer.OnGrenadeExplodedNear((num <= 2f) ? 1f : num);
				BattleScreenGUI.DirtToFace();
				BattleScreenGUI.DirtToFace();
				BattleScreenGUI.DirtToFace();
				BattleScreenGUI.DirtToFace();
			}
		}
		if (base.name == "client_marker")
		{
			base.transform.FindChild("marker_red").renderer.enabled = false;
			base.transform.FindChild("marker_green").renderer.enabled = false;
			base.transform.FindChild("marker_red").gameObject.layer = LayerMask.NameToLayer("Default");
			base.transform.FindChild("marker_green").gameObject.layer = LayerMask.NameToLayer("Default");
			if (this.sr)
			{
				this.sr.enabled = true;
			}
			base.transform.position = new Vector3(0f, 0f, 0f);
			base.transform.eulerAngles = new Vector3(90f, 0f, 0f);
			base.animation.Stop();
		}
		base.OnPoolDespawn();
	}

	// Token: 0x06000E61 RID: 3681 RVA: 0x000A790C File Offset: 0x000A5B0C
	public override void Deserialize(eNetworkStream stream)
	{
		base.Deserialize(stream);
		this.state.Time = Peer.ClientGame.ServerTime;
		this.packets.Add(this.state);
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x000A7948 File Offset: 0x000A5B48
	protected override void Awake()
	{
		this.sr = base.GetComponentInChildren<SkinnedMeshRenderer>();
		base.Awake();
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x000A795C File Offset: 0x000A5B5C
	public override void CallLateUpdate()
	{
		EntityState entityState = null;
		EntityState entityState2 = null;
		float t = 0f;
		ArrayUtility.Interpolate<EntityState>(this.packets, out entityState2, out entityState, Peer.ClientGame.ServerTime - CVars.p_interpolateTime, out t);
		if (base.renderer != null)
		{
			base.renderer.enabled = (this.packets.Length >= 3);
		}
		base.transform.position = Vector3.Lerp(entityState2.pos, entityState.pos, t);
		base.transform.eulerAngles = Vector3.Lerp(entityState2.euler, entityState.euler, t);
		if (this.state.type == EntityType.placement && Peer.ClientGame.Placement)
		{
			Peer.ClientGame.Placement.PlayerID = this.state.playerID;
			Peer.ClientGame.Placement.state = this.state.placementState;
			Peer.ClientGame.Placement.ElapsedTime = this.state.sonarTime;
			Peer.ClientGame.Placement.PointType = this.state.PointType;
			Peer.ClientGame.Placement.plaecementProgress = Mathf.Lerp(entityState2.plaecementProgress, entityState.plaecementProgress, t);
			if (Peer.ClientGame.LocalPlayer.IsAlive && BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 16))
			{
				EventFactory.Call("ShowPlacementProgress", null);
			}
			else
			{
				EventFactory.Call("HidePlacementProgress", null);
			}
			if (Peer.ClientGame.LocalPlayer.IsAlive && BIT.AND((int)Peer.ClientGame.LocalPlayer.playerInfo.buffs, 32))
			{
				EventFactory.Call("ShowBeaconUser", null);
			}
			else
			{
				EventFactory.Call("HideBeaconUser", null);
			}
		}
	}

	// Token: 0x04000F0B RID: 3851
	private ClassArray<EntityState> packets = new ClassArray<EntityState>(CVars.g_tickrate);

	// Token: 0x04000F0C RID: 3852
	private SkinnedMeshRenderer sr;
}
