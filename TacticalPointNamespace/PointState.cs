using System;

namespace TacticalPointNamespace
{
	// Token: 0x020002D1 RID: 721
	internal abstract class PointState : ITacticalPointState
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x000D48A0 File Offset: 0x000D2AA0
		public TacticalPointState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x000D48A8 File Offset: 0x000D2AA8
		public virtual void Update()
		{
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x000D48AC File Offset: 0x000D2AAC
		public virtual void OnSet()
		{
		}

		// Token: 0x04001883 RID: 6275
		public TacticalPointState state;

		// Token: 0x04001884 RID: 6276
		protected TacticalPoint point;
	}
}
