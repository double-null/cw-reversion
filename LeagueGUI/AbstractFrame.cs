using System;

namespace LeagueGUI
{
	// Token: 0x02000313 RID: 787
	public abstract class AbstractFrame : IFrame
	{
		// Token: 0x06001ACF RID: 6863 RVA: 0x000F1F18 File Offset: 0x000F0118
		public virtual void OnStart()
		{
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000F1F1C File Offset: 0x000F011C
		public virtual void OnGUI()
		{
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000F1F20 File Offset: 0x000F0120
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000F1F24 File Offset: 0x000F0124
		public virtual void Clear()
		{
		}
	}
}
