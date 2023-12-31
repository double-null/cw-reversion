using System;

// Token: 0x0200019F RID: 415
[Serializable]
internal class BaseCmdCollector : cwNetworkSerializable, ReusableClass<BaseCmdCollector>
{
	// Token: 0x06000C1E RID: 3102 RVA: 0x00095404 File Offset: 0x00093604
	public virtual void Serialize(eNetworkStream stream)
	{
		int length = this.cmds.Length;
		stream.Serialize(ref length);
		for (int i = 0; i < this.cmds.Length; i++)
		{
			this.cmds[i].Serialize(stream);
		}
		this.cmds.Clear();
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x00095460 File Offset: 0x00093660
	public virtual void Deserialize(eNetworkStream stream)
	{
		int num = 0;
		stream.Serialize(ref num);
		for (int i = 0; i < num; i++)
		{
			eCache.PlayerCmd.Clear();
			eCache.PlayerCmd.Deserialize(stream);
			eCache.PlayerCmd.packetLatency = eCache.PlayerCmd.packetLatency + (float)(num + CVars.s_cmdTune) / (float)CVars.g_tickrate;
			this.Push(eCache.PlayerCmd);
		}
	}

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000954D0 File Offset: 0x000936D0
	public int Count
	{
		get
		{
			return this.balancer.Count;
		}
	}

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06000C21 RID: 3105 RVA: 0x000954E0 File Offset: 0x000936E0
	public int CmdsCount
	{
		get
		{
			return this.cmds.Length;
		}
	}

	// Token: 0x06000C22 RID: 3106 RVA: 0x000954F0 File Offset: 0x000936F0
	public virtual void Push(PlayerCmd cmd)
	{
		this.cmds.Add(cmd);
		if (this.cmds.Length > CVars.g_tickrate)
		{
			this.cmds.Clear();
		}
	}

	// Token: 0x06000C23 RID: 3107 RVA: 0x0009552C File Offset: 0x0009372C
	internal ClassArray<PlayerCmd> Pop()
	{
		this.buffer.Clear();
		int count = this.balancer.Count;
		if (count == 0)
		{
			return this.buffer;
		}
		for (int i = 0; i < this.cmds.Length; i++)
		{
			this.buffer.Add(this.cmds[i]);
			this.cmds.RemoveAt(i);
			this.AdvanceTick();
			if (this.buffer.Length == count)
			{
				break;
			}
			i = -1;
		}
		return this.buffer;
	}

	// Token: 0x06000C24 RID: 3108 RVA: 0x000955C8 File Offset: 0x000937C8
	public void AdvanceTick()
	{
		this.balancer.AdvanceTick();
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x000955D8 File Offset: 0x000937D8
	public virtual void Clear()
	{
		this.balancer.Clear();
		this.buffer.Clear();
		this.cmds.Clear();
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000955FC File Offset: 0x000937FC
	public virtual void Clone(BaseCmdCollector i)
	{
		this.balancer.Clone(i.balancer);
		this.buffer.Clone(i.buffer);
		this.cmds.Clone(i.cmds);
	}

	// Token: 0x04000DC5 RID: 3525
	protected Balancer balancer = new Balancer();

	// Token: 0x04000DC6 RID: 3526
	protected ClassArray<PlayerCmd> buffer = new ClassArray<PlayerCmd>(CVars.g_tickrate);

	// Token: 0x04000DC7 RID: 3527
	protected ClassArray<PlayerCmd> cmds = new ClassArray<PlayerCmd>(CVars.g_tickrate);
}
