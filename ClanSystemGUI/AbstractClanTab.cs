using System;
using UnityEngine;

namespace ClanSystemGUI
{
	// Token: 0x02000100 RID: 256
	public abstract class AbstractClanTab : IClanTab
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
		public virtual void SetRect(Rect rect)
		{
			this.rect = rect;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0003CFE4 File Offset: 0x0003B1E4
		public virtual void OnDrawButton()
		{
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
		public virtual void OnStart()
		{
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0003CFEC File Offset: 0x0003B1EC
		public virtual void OnGUI()
		{
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003CFF0 File Offset: 0x0003B1F0
		public virtual void OnUpdate()
		{
		}

		// Token: 0x040007A7 RID: 1959
		protected Rect rect;
	}
}
