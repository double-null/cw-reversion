using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000197 RID: 407
	internal class SpawnContoller
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0008E348 File Offset: 0x0008C548
		public void OnGUI()
		{
			if (this.currentState != null && !MainGUI.Instance.Visible)
			{
				this.currentState.OnGUI();
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0008E370 File Offset: 0x0008C570
		public void OnUpdate()
		{
			if (this.currentState != null && !MainGUI.Instance.Visible)
			{
				this.currentState.OnUpdate();
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0008E398 File Offset: 0x0008C598
		public void OnConnected()
		{
			if (Main.IsDeathMatch)
			{
				this.SetState(this.dmSpawn);
			}
			if (Main.IsTeamElimination)
			{
				this.SetState(this.teSpawn);
			}
			if (Main.IsTacticalConquest)
			{
				this.SetState(this.tcSpawn);
			}
			if (Main.IsTargetDesignation)
			{
				this.SetState(this.tdsSpawn);
			}
			if (this.currentState != null)
			{
				this.currentState.OnConnected();
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0008E414 File Offset: 0x0008C614
		public void Spawn()
		{
			if (this.currentState != null)
			{
				this.currentState.Spawn();
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0008E42C File Offset: 0x0008C62C
		public void SetState(SpawnState state)
		{
			this.currentState = state;
		}

		// Token: 0x04000D4F RID: 3407
		private SpawnState currentState;

		// Token: 0x04000D50 RID: 3408
		public SpawnState dmSpawn = new DMSpawn();

		// Token: 0x04000D51 RID: 3409
		public SpawnState teSpawn = new TESpawn();

		// Token: 0x04000D52 RID: 3410
		public SpawnState tcSpawn = new TCSpawn();

		// Token: 0x04000D53 RID: 3411
		public SpawnState tdsSpawn = new TDSSpawn();

		// Token: 0x04000D54 RID: 3412
		public SpawnState noSpawn = new NoSpawn();
	}
}
