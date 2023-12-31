using System;

namespace SpectatorGUInamespace
{
	// Token: 0x02000187 RID: 391
	public abstract class AbstractSpawnState : SpawnState
	{
		// Token: 0x06000B4A RID: 2890 RVA: 0x0008B9B0 File Offset: 0x00089BB0
		public virtual void OnGUI()
		{
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x0008B9B4 File Offset: 0x00089BB4
		public virtual void OnUpdate()
		{
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0008B9B8 File Offset: 0x00089BB8
		public virtual void OnConnected()
		{
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0008B9BC File Offset: 0x00089BBC
		public virtual void Spawn()
		{
			if (((TabGUI)Forms.Get(typeof(TabGUI))).Visible)
			{
				return;
			}
			Peer.ClientGame.LocalPlayer.ChooseAmmunition();
			Peer.ClientGame.LocalPlayer.Spawn();
		}
	}
}
