using System;
using UnityEngine;

// Token: 0x020002AC RID: 684
[Serializable]
internal class PredictionState : ReusableClass<PredictionState>
{
	// Token: 0x06001349 RID: 4937 RVA: 0x000CFBA8 File Offset: 0x000CDDA8
	public void Clear()
	{
		this.UInput.Clear();
		this.client = false;
		this.predicted = false;
		this.server = false;
		this.inMinThreshold = false;
		this.clientState.Clear();
		this.predictedState.Clear();
		this.serverState.Clear();
	}

	// Token: 0x0600134A RID: 4938 RVA: 0x000CFC00 File Offset: 0x000CDE00
	public void Clone(PredictionState i)
	{
		this.UInput.Clone(i.UInput);
		this.client = i.client;
		this.predicted = i.predicted;
		this.server = i.server;
		this.inMinThreshold = i.inMinThreshold;
		this.clientState.Clone(i.clientState);
		this.predictedState.Clone(i.predictedState);
		this.serverState.Clone(i.serverState);
	}

	// Token: 0x0600134B RID: 4939 RVA: 0x000CFC84 File Offset: 0x000CDE84
	public void SetUInput(CWInput UInput)
	{
		this.UInput.Clone(UInput);
	}

	// Token: 0x0600134C RID: 4940 RVA: 0x000CFC94 File Offset: 0x000CDE94
	public void SetClient(PlayerInfo playerInfo, AmmoState ammoState, MoveState moveState)
	{
		this.client = true;
		this.clientState.playerInfo.Clone(playerInfo);
		this.clientState.ammoState.Clone(ammoState);
		this.clientState.moveState.Clone(moveState);
	}

	// Token: 0x0600134D RID: 4941 RVA: 0x000CFCDC File Offset: 0x000CDEDC
	public void SetPredicted(PlayerInfo playerInfo, AmmoState ammoState, MoveState moveState)
	{
		this.predicted = true;
		this.predictedState.playerInfo.Clone(playerInfo);
		this.predictedState.ammoState.Clone(ammoState);
		this.predictedState.moveState.Clone(moveState);
	}

	// Token: 0x0600134E RID: 4942 RVA: 0x000CFD24 File Offset: 0x000CDF24
	public void SetServer(PlayerInfo playerInfo, AmmoState ammoState, MoveState moveState)
	{
		this.server = true;
		this.serverState.playerInfo.Clone(playerInfo);
		this.serverState.ammoState.Clone(ammoState);
		this.serverState.moveState.Clone(moveState);
	}

	// Token: 0x0600134F RID: 4943 RVA: 0x000CFD6C File Offset: 0x000CDF6C
	public void ClearPredicted()
	{
		this.predicted = false;
		this.predictedState.Clear();
	}

	// Token: 0x06001350 RID: 4944 RVA: 0x000CFD80 File Offset: 0x000CDF80
	public void Draw()
	{
		if (this.client)
		{
			Gizmos.color = new Color(0f, 0f, 1f, 0.3f);
			Gizmos.DrawLine(this.clientState.moveState.pos - Vector3.up, this.clientState.moveState.pos);
			Gizmos.DrawSphere(this.clientState.moveState.pos - Vector3.up, 0.05f);
			Gizmos.DrawSphere(this.clientState.moveState.pos, 0.05f);
		}
		if (this.predicted)
		{
			if (this.inMinThreshold)
			{
				Gizmos.color = new Color(0.5f, 1f, 0.5f, 0.4f);
			}
			else
			{
				Gizmos.color = new Color(0f, 1f, 0f, 0.4f);
			}
			Gizmos.DrawLine(this.predictedState.moveState.pos - Vector3.up, this.predictedState.moveState.pos);
			Gizmos.DrawSphere(this.predictedState.moveState.pos - Vector3.up, 0.05f);
			Gizmos.DrawSphere(this.predictedState.moveState.pos, 0.05f);
		}
		if (this.server)
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.4f);
			Gizmos.DrawLine(this.serverState.moveState.pos - Vector3.up, this.serverState.moveState.pos + Vector3.up * 0.8f - Vector3.up);
			Gizmos.DrawSphere(this.serverState.moveState.pos - Vector3.up, 0.05f);
			Gizmos.DrawSphere(this.serverState.moveState.pos + Vector3.up * 0.8f - Vector3.up, 0.05f);
		}
	}

	// Token: 0x04001644 RID: 5700
	public CWInput UInput = new CWInput();

	// Token: 0x04001645 RID: 5701
	public bool client;

	// Token: 0x04001646 RID: 5702
	public bool predicted;

	// Token: 0x04001647 RID: 5703
	public bool server;

	// Token: 0x04001648 RID: 5704
	public bool inMinThreshold;

	// Token: 0x04001649 RID: 5705
	public EntityPacket clientState = new EntityPacket();

	// Token: 0x0400164A RID: 5706
	public EntityPacket predictedState = new EntityPacket();

	// Token: 0x0400164B RID: 5707
	public EntityPacket serverState = new EntityPacket();
}
