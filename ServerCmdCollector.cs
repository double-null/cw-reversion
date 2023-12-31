using System;
using System.Collections.Generic;

// Token: 0x020002EA RID: 746
[Serializable]
internal class ServerCmdCollector : BaseCmdCollector
{
	// Token: 0x060014B8 RID: 5304 RVA: 0x000DB290 File Offset: 0x000D9490
	public override void Serialize(eNetworkStream stream)
	{
		this.buffer.Clear();
		int num = 0;
		if (stream.ID != IDUtil.NoID)
		{
			if (this.numberID.IndexOfKey(stream.ID) == -1)
			{
				this.numberID.Add(stream.ID, -1);
			}
			int num2 = this.numberID[stream.ID];
			int length = this.allcmds.Length;
			for (int i = 0; i < length; i++)
			{
				PlayerCmd playerCmd = this.allcmds[i];
				if (playerCmd.number > num2)
				{
					this.buffer.Add(playerCmd);
				}
			}
			num = this.buffer.Length;
			if (num != 0)
			{
				this.numberID[stream.ID] = this.buffer[this.buffer.Length - 1].number;
			}
		}
		this.debugCount = num;
		stream.Serialize(ref num);
		for (int j = 0; j < num; j++)
		{
			this.buffer[j].Serialize(stream);
		}
	}

	// Token: 0x060014B9 RID: 5305 RVA: 0x000DB3B4 File Offset: 0x000D95B4
	public override void Push(PlayerCmd cmd)
	{
		base.Push(cmd);
		this.allcmds.Add(cmd);
		if (this.cmds.Length == 0)
		{
			this.allcmds.Clear();
		}
	}

	// Token: 0x060014BA RID: 5306 RVA: 0x000DB3F0 File Offset: 0x000D95F0
	public override void Clear()
	{
		base.Clear();
		this.allcmds.Clear();
	}

	// Token: 0x060014BB RID: 5307 RVA: 0x000DB404 File Offset: 0x000D9604
	public override void Clone(BaseCmdCollector i)
	{
		base.Clone(i);
		this.allcmds.Clone((i as ServerCmdCollector).allcmds);
	}

	// Token: 0x0400195B RID: 6491
	protected ClassArray<PlayerCmd> allcmds = new ClassArray<PlayerCmd>(CVars.g_tickrate);

	// Token: 0x0400195C RID: 6492
	private SortedList<int, int> numberID = new SortedList<int, int>();

	// Token: 0x0400195D RID: 6493
	public int debugCount;
}
