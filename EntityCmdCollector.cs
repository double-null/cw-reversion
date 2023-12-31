using System;

// Token: 0x020001E6 RID: 486
internal class EntityCmdCollector : BaseCmdCollector
{
	// Token: 0x0600101E RID: 4126 RVA: 0x000B5B28 File Offset: 0x000B3D28
	public override void Deserialize(eNetworkStream stream)
	{
		this.compressButtons = 0;
		int num = 0;
		stream.Serialize(ref num);
		for (int i = 0; i < num; i++)
		{
			eCache.PlayerCmd.Clear();
			eCache.PlayerCmd.Deserialize(stream);
			eCache.PlayerCmd.packetLatency = eCache.PlayerCmd.packetLatency + (float)(num + CVars.s_cmdTune) / (float)CVars.g_tickrate;
			this.compressButtons |= eCache.PlayerCmd.buttons;
			this.lastCmd.Clone(eCache.PlayerCmd);
		}
		this.lastCmd.buttons = this.compressButtons;
	}

	// Token: 0x04001086 RID: 4230
	public PlayerCmd lastCmd = new PlayerCmd();

	// Token: 0x04001087 RID: 4231
	private int compressButtons;
}
