using System;

namespace LeagueGUI
{
	// Token: 0x02000310 RID: 784
	internal class ResultWindow : AbstractWindow
	{
		// Token: 0x06001ABC RID: 6844 RVA: 0x000F1CE8 File Offset: 0x000EFEE8
		public override void OnStart()
		{
			this.header.OnStart();
			this.body.OnStart();
			SearchFrame.Accepted = false;
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x000F1D08 File Offset: 0x000EFF08
		public override void OnDrawWindow()
		{
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000F1D0C File Offset: 0x000EFF0C
		public override void OnGUI()
		{
			this.header.OnGUI();
			this.body.OnGUI();
		}

		// Token: 0x04001FD0 RID: 8144
		private IFrame header = new ResultHeaderFrame();

		// Token: 0x04001FD1 RID: 8145
		private IFrame body = new ResultBodyFrame();
	}
}
