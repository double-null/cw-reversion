using System;
using UnityEngine;

namespace UnityGUI.Forms
{
	// Token: 0x020000C5 RID: 197
	public abstract class Control
	{
		// Token: 0x06000528 RID: 1320 RVA: 0x00020FAC File Offset: 0x0001F1AC
		public virtual void OnGUI()
		{
		}

		// Token: 0x04000497 RID: 1175
		public Rect Rect = default(Rect);
	}
}
