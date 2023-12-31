using System;
using cygwin_x32.ObscuredTypes;
using UnityEngine;

// Token: 0x020001A0 RID: 416
[AddComponentMenu("Scripts/Game/BaseEntity")]
internal class BaseEntity : PoolableBehaviour, cwID, cwEntityType, cwNetworkSerializable
{
	// Token: 0x06000C29 RID: 3113 RVA: 0x00095654 File Offset: 0x00093854
	public override void OnPoolDespawn()
	{
		this.state = new EntityState();
		base.OnPoolDespawn();
	}

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06000C2A RID: 3114 RVA: 0x00095668 File Offset: 0x00093868
	public ObscuredInt ID
	{
		get
		{
			return this.state.ID;
		}
	}

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0009567C File Offset: 0x0009387C
	public EntityType EntityType
	{
		get
		{
			return this.state.type;
		}
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x0009568C File Offset: 0x0009388C
	public static void DummyDeserialize(eNetworkStream stream)
	{
		BaseEntity.TempState.Deserialize(stream);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x0009569C File Offset: 0x0009389C
	public virtual void Serialize(eNetworkStream stream)
	{
		this.state.Serialize(stream);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x000956AC File Offset: 0x000938AC
	public virtual void Deserialize(eNetworkStream stream)
	{
		this.state.Deserialize(stream);
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x000956BC File Offset: 0x000938BC
	public virtual void CallLateUpdate()
	{
	}

	// Token: 0x04000DC8 RID: 3528
	public EntityState state = new EntityState();

	// Token: 0x04000DC9 RID: 3529
	public static EntityState TempState = new EntityState();
}
