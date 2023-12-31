using System;

// Token: 0x0200029A RID: 666
[Serializable]
internal class HitInfo : cwTime, ReusableClass<HitInfo>
{
	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06001296 RID: 4758 RVA: 0x000CB378 File Offset: 0x000C9578
	// (set) Token: 0x06001297 RID: 4759 RVA: 0x000CB380 File Offset: 0x000C9580
	public float Time
	{
		get
		{
			return this.time;
		}
		set
		{
			this.time = value;
		}
	}

	// Token: 0x06001298 RID: 4760 RVA: 0x000CB38C File Offset: 0x000C958C
	public void Clear()
	{
		this.proxy.Clear();
		this.NPC_Pelvis.Clear();
		this.NPC_L_Thigh.Clear();
		this.NPC_L_Calf.Clear();
		this.NPC_R_Thigh.Clear();
		this.NPC_R_Calf.Clear();
		this.NPC_Spine1.Clear();
		this.NPC_Head.Clear();
		this.NPC_L_UpperArm.Clear();
		this.NPC_L_Forearm.Clear();
		this.NPC_R_UpperArm.Clear();
		this.NPC_R_Forearm.Clear();
		this.time = 0f;
	}

	// Token: 0x06001299 RID: 4761 RVA: 0x000CB428 File Offset: 0x000C9628
	public void Clone(HitInfo i)
	{
		this.proxy.Clone(i.proxy);
		this.NPC_Pelvis.Clone(i.NPC_Pelvis);
		this.NPC_L_Thigh.Clone(i.NPC_L_Thigh);
		this.NPC_L_Calf.Clone(i.NPC_L_Calf);
		this.NPC_R_Thigh.Clone(i.NPC_R_Thigh);
		this.NPC_R_Calf.Clone(i.NPC_R_Calf);
		this.NPC_Spine1.Clone(i.NPC_Spine1);
		this.NPC_Head.Clone(i.NPC_Head);
		this.NPC_L_UpperArm.Clone(i.NPC_L_UpperArm);
		this.NPC_L_Forearm.Clone(i.NPC_L_Forearm);
		this.NPC_R_UpperArm.Clone(i.NPC_R_UpperArm);
		this.NPC_R_Forearm.Clone(i.NPC_R_Forearm);
		this.time = i.time;
	}

	// Token: 0x0400155D RID: 5469
	public HitInfoUnit proxy = new HitInfoUnit();

	// Token: 0x0400155E RID: 5470
	public HitInfoUnit NPC_Pelvis = new HitInfoUnit();

	// Token: 0x0400155F RID: 5471
	public HitInfoUnit NPC_L_Thigh = new HitInfoUnit();

	// Token: 0x04001560 RID: 5472
	public HitInfoUnit NPC_L_Calf = new HitInfoUnit();

	// Token: 0x04001561 RID: 5473
	public HitInfoUnit NPC_R_Thigh = new HitInfoUnit();

	// Token: 0x04001562 RID: 5474
	public HitInfoUnit NPC_R_Calf = new HitInfoUnit();

	// Token: 0x04001563 RID: 5475
	public HitInfoUnit NPC_Spine1 = new HitInfoUnit();

	// Token: 0x04001564 RID: 5476
	public HitInfoUnit NPC_Head = new HitInfoUnit();

	// Token: 0x04001565 RID: 5477
	public HitInfoUnit NPC_L_UpperArm = new HitInfoUnit();

	// Token: 0x04001566 RID: 5478
	public HitInfoUnit NPC_L_Forearm = new HitInfoUnit();

	// Token: 0x04001567 RID: 5479
	public HitInfoUnit NPC_R_UpperArm = new HitInfoUnit();

	// Token: 0x04001568 RID: 5480
	public HitInfoUnit NPC_R_Forearm = new HitInfoUnit();

	// Token: 0x04001569 RID: 5481
	private float time;
}
