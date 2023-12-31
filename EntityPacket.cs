using System;

// Token: 0x02000259 RID: 601
[Serializable]
internal class EntityPacket : cwTime, ReusableClass<EntityPacket>
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x06001272 RID: 4722 RVA: 0x000C9FDC File Offset: 0x000C81DC
	// (set) Token: 0x06001273 RID: 4723 RVA: 0x000C9FE4 File Offset: 0x000C81E4
	public float Time { get; set; }

	// Token: 0x06001274 RID: 4724 RVA: 0x000C9FF0 File Offset: 0x000C81F0
	public void Clear()
	{
		this.playerInfo.Clear();
		this.moveState.Clear();
		this.ammoState.Clear();
		this.Time = 0f;
	}

	// Token: 0x06001275 RID: 4725 RVA: 0x000CA02C File Offset: 0x000C822C
	public void Clone(EntityPacket i)
	{
		this.playerInfo.Clone(i.playerInfo);
		this.moveState.Clone(i.moveState);
		this.ammoState.Clone(i.ammoState);
		this.Time = i.Time;
	}

	// Token: 0x04001229 RID: 4649
	public PlayerInfo playerInfo = new PlayerInfo();

	// Token: 0x0400122A RID: 4650
	public MoveState moveState = new MoveState();

	// Token: 0x0400122B RID: 4651
	public AmmoState ammoState = new AmmoState();
}
