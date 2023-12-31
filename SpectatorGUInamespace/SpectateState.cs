using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000182 RID: 386
	public abstract class SpectateState : Spectate
	{
		// Token: 0x06000B2B RID: 2859 RVA: 0x0008B384 File Offset: 0x00089584
		public virtual void next()
		{
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0008B388 File Offset: 0x00089588
		public virtual void OnConnected()
		{
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0008B38C File Offset: 0x0008958C
		public virtual void Enable()
		{
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0008B390 File Offset: 0x00089590
		public virtual void Disable()
		{
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0008B394 File Offset: 0x00089594
		public virtual void OnGUI()
		{
		}
	}
}
