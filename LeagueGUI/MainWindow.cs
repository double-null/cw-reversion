using System;

namespace LeagueGUI
{
	// Token: 0x0200030F RID: 783
	internal class MainWindow : AbstractWindow
	{
		// Token: 0x06001AB7 RID: 6839 RVA: 0x000F1C48 File Offset: 0x000EFE48
		public override void OnStart()
		{
			this.rulesFrame.OnStart();
			this.ratingFrame.OnStart();
			this.prizeFrame.OnStart();
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000F1C6C File Offset: 0x000EFE6C
		public override void OnDrawWindow()
		{
			if (LeagueWindow.I.controller.CurrentWindow == this)
			{
				LeagueWindow.I.controller.SetState(this);
			}
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000F1C94 File Offset: 0x000EFE94
		public override void OnGUI()
		{
			this.rulesFrame.OnGUI();
			this.ratingFrame.OnGUI();
			this.prizeFrame.OnGUI();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x000F1CB8 File Offset: 0x000EFEB8
		public override void OnUpdate()
		{
			this.prizeFrame.OnUpdate();
		}

		// Token: 0x04001FCD RID: 8141
		private IFrame rulesFrame = new RulesFrame();

		// Token: 0x04001FCE RID: 8142
		private IFrame ratingFrame = new RatingFrame();

		// Token: 0x04001FCF RID: 8143
		private IFrame prizeFrame = new PrizesFrame();
	}
}
