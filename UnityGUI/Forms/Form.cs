using System;
using System.Collections.Generic;

namespace UnityGUI.Forms
{
	// Token: 0x020000C6 RID: 198
	internal class Form : Control
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00020FC4 File Offset: 0x0001F1C4
		public void Add(Control c)
		{
			this.Controls.Add(c);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00020FD4 File Offset: 0x0001F1D4
		public void Remove(Control c)
		{
			this.Controls.Remove(c);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00020FE4 File Offset: 0x0001F1E4
		public override void OnGUI()
		{
			foreach (Control control in this.Controls)
			{
				control.OnGUI();
			}
		}

		// Token: 0x04000498 RID: 1176
		protected List<Control> Controls = new List<Control>();
	}
}
