using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000183 RID: 387
	internal class SpectatePlayer : SpectateState
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x0008B398 File Offset: 0x00089598
		public SpectatePlayer(CamSpectator controller)
		{
			this.controller = controller;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0008B3B4 File Offset: 0x000895B4
		public override void next()
		{
			base.next();
			this.watchedAlpha.Hide(0f, 0f);
			if (this.startIndex >= Peer.ClientGame.AlivePlayers.Count - 1)
			{
				this.startIndex = 0;
			}
			for (int i = this.startIndex; i < Peer.ClientGame.AlivePlayers.Count; i++)
			{
				if (this.nextPlayer == null)
				{
					this.nextPlayer = Peer.ClientGame.AlivePlayers[i];
				}
				else if (this.nextPlayer != Peer.ClientGame.AlivePlayers[i])
				{
					this.nextPlayer = Peer.ClientGame.AlivePlayers[i];
					break;
				}
			}
			if (Main.IsDeathMatch && !CVars.g_allowDMSpectate && Main.UserInfo.Permission < EPermission.Admin)
			{
				this.nextPlayer = null;
			}
			if (this.nextPlayer == null || Peer.ClientGame.AlivePlayers.Count == 0)
			{
				this.controller.SetState(this.controller.spectatePlans);
			}
			else if (this.spec != null)
			{
				this.spec.LookAt(this.nextPlayer);
			}
			this.startIndex++;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0008B528 File Offset: 0x00089728
		public override void Enable()
		{
			base.Enable();
			this.watchedAlpha.Hide(0f, 0f);
			if (this.spec == null)
			{
				this.camera = PlayerMainCameraManager.GetCamera();
				this.spec = this.camera.GetComponent<Spectactor3D>();
				this.spec.transform.parent = Main.Trash;
				this.spec.enabled = true;
			}
			CameraListener.ChangeTo(this.camera);
			this.next();
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0008B5B0 File Offset: 0x000897B0
		public override void Disable()
		{
			base.Disable();
			if (this.spec != null)
			{
				this.spec.enabled = false;
				if (this.spec.transform.parent == Main.Trash)
				{
					PlayerMainCameraManager.OffCamera();
				}
				this.spec = null;
				this.nextPlayer = null;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0008B614 File Offset: 0x00089814
		public override void OnGUI()
		{
			if (this.watchedAlpha.visibility < 0.1f && !this.watchedAlpha.Showing)
			{
				this.watchedAlpha.Show(0.5f, 0f);
			}
			if (this.nextPlayer != null)
			{
				BannerGUI.I.SpectateForBanner(this.nextPlayer, this.watchedAlpha);
			}
		}

		// Token: 0x04000D30 RID: 3376
		private Alpha watchedAlpha = new Alpha();

		// Token: 0x04000D31 RID: 3377
		private CamSpectator controller;

		// Token: 0x04000D32 RID: 3378
		private Spectactor3D spec;

		// Token: 0x04000D33 RID: 3379
		private GameObject camera;

		// Token: 0x04000D34 RID: 3380
		private EntityNetPlayer nextPlayer;

		// Token: 0x04000D35 RID: 3381
		private int startIndex;
	}
}
