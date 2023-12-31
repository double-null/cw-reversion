using System;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D3 RID: 211
	public abstract class BannerQueueAbstract : BannerQueue
	{
		// Token: 0x060005A7 RID: 1447 RVA: 0x0002A624 File Offset: 0x00028824
		public virtual void OnGUI()
		{
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002A628 File Offset: 0x00028828
		public virtual void Clear()
		{
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002A62C File Offset: 0x0002882C
		public virtual void Init(float speed)
		{
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002A630 File Offset: 0x00028830
		public virtual bool Complete()
		{
			return true;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002A634 File Offset: 0x00028834
		public virtual bool Inited()
		{
			return true;
		}
	}
}
