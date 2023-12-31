using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D6 RID: 214
	internal class BannerKillStreak : BannerQueueItem
	{
		// Token: 0x060005B8 RID: 1464 RVA: 0x0002AB90 File Offset: 0x00028D90
		public override void Init(float speed)
		{
			string tempName = this.current.streak.tempName;
			switch (tempName)
			{
			case "quadkill":
				this.screenShotEvent = ScreenShotEvent.quadkill;
				this.screenableBanner = Main.UserInfo.settings.graphics.QuadKillTakeScreen;
				break;
			case "prokill":
				this.screenShotEvent = ScreenShotEvent.prokill;
				this.screenableBanner = Main.UserInfo.settings.graphics.ProKillTakeScreen;
				break;
			case "legendarykill":
				this.screenShotEvent = ScreenShotEvent.legendarykill;
				this.screenableBanner = Main.UserInfo.settings.graphics.ProKillTakeScreen;
				break;
			}
			this.inited = true;
			this.speed = speed;
			if (this.current.streak.clip != null)
			{
				Audio.Play(this.current.streak.clip);
			}
			this.initTimer = BannerGUI.I.red_banner_POSYKeys[BannerGUI.I.red_banner_POSYKeys.Length - 1].time;
			this.GNAME.InitTimer(this.initTimer / this.speed);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x0002AD0C File Offset: 0x00028F0C
		public override void OnGUI()
		{
			base.OnGUI();
			MainGUI.Instance.BeginGroup(new Rect(0f, -15f, (float)Screen.width, (float)Screen.height));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.red_banner_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.redPos = new Vector2((float)(Screen.width / 2 - BannerGUI.I.red_banner.width / 2), BannerGUI.I.red_banner_POSY.Evaluate(this.GNAME.Get() * this.speed) * (float)BannerGUI.I.red_banner.height - (float)BannerGUI.I.red_banner.height);
			if (this.current.streak.type == KillStreakEnum.suicide)
			{
				MainGUI.Instance.Picture(this.redPos, BannerGUI.I.gray_banner);
			}
			else
			{
				MainGUI.Instance.Picture(this.redPos, BannerGUI.I.red_banner);
			}
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.red_glow_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.red_glow_Scale.Evaluate(this.GNAME.Get());
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 15)), BannerGUI.I.red_glow, new Vector2(this.scale, this.scale));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.killstreaks_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.killstreaks_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 15)), this.current.streak.texture, new Vector2(this.scale, this.scale));
			MainGUI.Instance.EndGroup();
		}

		// Token: 0x04000585 RID: 1413
		private float scale = 1f;

		// Token: 0x04000586 RID: 1414
		public Vector2 redPos = Vector2.zero;

		// Token: 0x04000587 RID: 1415
		public BannerInfo current = new BannerInfo();
	}
}
