using System;
using UnityEngine;

namespace SpectatorGUInamespace
{
	// Token: 0x02000184 RID: 388
	internal class SpectatePlans : SpectateState
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x0008B684 File Offset: 0x00089884
		public SpectatePlans(CamSpectator controller)
		{
			this.controller = controller;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0008B694 File Offset: 0x00089894
		private void EnableCamera(SpecCamera camera)
		{
			if (camera != null)
			{
				camera.enabled = true;
				camera.camera.enabled = true;
			}
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0008B6B8 File Offset: 0x000898B8
		private void DisableCamera(SpecCamera camera)
		{
			camera.enabled = false;
			camera.camera.enabled = false;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0008B6D0 File Offset: 0x000898D0
		public override void next()
		{
			base.next();
			this.DisableCamera(this.cameras[this.cameraIndex]);
			this.cameraIndex++;
			if (this.cameraIndex == this.cameras.Length)
			{
				this.cameraIndex = 0;
			}
			this.EnableCamera(this.cameras[this.cameraIndex]);
			CameraListener.ChangeTo(this.cameras[this.cameraIndex].gameObject);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0008B748 File Offset: 0x00089948
		public override void OnConnected()
		{
			base.OnConnected();
			this.cameras = (SpecCamera[])UnityEngine.Object.FindObjectsOfType(typeof(SpecCamera));
			this.cameraIndex = (int)(UnityEngine.Random.value * (float)this.cameras.Length);
			for (int i = 0; i < this.cameras.Length; i++)
			{
				this.DisableCamera(this.cameras[i]);
				this.cameras[i].camera.depth = 0f;
			}
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x0008B7CC File Offset: 0x000899CC
		public override void Enable()
		{
			base.Enable();
			if (this.cameras == null)
			{
				return;
			}
			this.EnableCamera(this.cameras[this.cameraIndex]);
			CameraListener.ChangeTo(this.cameras[this.cameraIndex].gameObject);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0008B818 File Offset: 0x00089A18
		public override void Disable()
		{
			base.Disable();
			if (this.cameras != null && this.cameras[this.cameraIndex] != null)
			{
				this.DisableCamera(this.cameras[this.cameraIndex]);
			}
		}

		// Token: 0x04000D36 RID: 3382
		private CamSpectator controller;

		// Token: 0x04000D37 RID: 3383
		private SpecCamera[] cameras;

		// Token: 0x04000D38 RID: 3384
		private int cameraIndex;
	}
}
