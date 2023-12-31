using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D4 RID: 212
	internal class BannerQueueItem : BannerQueueAbstract
	{
		// Token: 0x060005AD RID: 1453 RVA: 0x0002A664 File Offset: 0x00028864
		public override void OnGUI()
		{
			if (this.screenableBanner && this.makeScreenshotOnce.Do())
			{
				Main.MakeScreenShotWithDelay(0.5f / this.speed, this.screenShotEvent);
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002A6A4 File Offset: 0x000288A4
		public void InitFastTimer(float realTime, float requiredTime)
		{
			if (this.clip != null)
			{
				Audio.Play(this.clip);
			}
			this.GNAME.InitFastTimer(realTime, requiredTime);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002A6DC File Offset: 0x000288DC
		private void InitTimer(float maxTime)
		{
			this.GNAME.InitTimer(maxTime);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002A6EC File Offset: 0x000288EC
		public override void Init(float speed)
		{
			this.inited = true;
			if (this.clip != null)
			{
				Audio.Play(this.clip);
			}
			base.Init(0f);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002A720 File Offset: 0x00028920
		public override void Clear()
		{
			this.GNAME = new GraphicValue();
			this.inited = false;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002A734 File Offset: 0x00028934
		public override bool Complete()
		{
			return this.GNAME.Get() < -1f;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002A748 File Offset: 0x00028948
		public override bool Inited()
		{
			return this.inited;
		}

		// Token: 0x0400057A RID: 1402
		protected float speed = 1f;

		// Token: 0x0400057B RID: 1403
		protected float initTimer;

		// Token: 0x0400057C RID: 1404
		protected bool inited;

		// Token: 0x0400057D RID: 1405
		protected AudioClip clip;

		// Token: 0x0400057E RID: 1406
		protected GraphicValue GNAME = new GraphicValue();

		// Token: 0x0400057F RID: 1407
		protected DoOnce makeScreenshotOnce = new DoOnce();

		// Token: 0x04000580 RID: 1408
		protected bool screenableBanner;

		// Token: 0x04000581 RID: 1409
		protected ScreenShotEvent screenShotEvent;
	}
}
