using System;

namespace ClanSystemGUI
{
	// Token: 0x0200011C RID: 284
	public interface IScrollListItem : IComparable
	{
		// Token: 0x060007B8 RID: 1976
		void OnGUI(float x, float y, int index);

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060007B9 RID: 1977
		float Width { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060007BA RID: 1978
		float Height { get; }
	}
}
