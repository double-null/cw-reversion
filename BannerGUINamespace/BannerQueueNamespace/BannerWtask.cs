using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D7 RID: 215
	internal class BannerWtask : BannerQueueItem
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x0002AFA4 File Offset: 0x000291A4
		public BannerWtask()
		{
			this.clip = BannerGUI.I.wtask_clip;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002AFE0 File Offset: 0x000291E0
		public override void Init(float speed)
		{
			this.screenableBanner = Main.UserInfo.settings.graphics.AchievementTakeScreen;
			this.inited = true;
			base.Init(speed);
			this.speed = speed;
			this.initTimer = BannerGUI.I.red_banner_POSYKeys[BannerGUI.I.red_banner_POSYKeys.Length - 1].time;
			this.GNAME.InitTimer(this.initTimer / this.speed);
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002B05C File Offset: 0x0002925C
		public override void OnGUI()
		{
			base.OnGUI();
			MainGUI.Instance.BeginGroup(new Rect(0f, -15f, (float)Screen.width, (float)Screen.height));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.red_banner_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.redPos = new Vector2((float)(Screen.width / 2 - BannerGUI.I.red_banner.width / 2), BannerGUI.I.red_banner_POSY.Evaluate(this.GNAME.Get() * this.speed) * (float)BannerGUI.I.red_banner.height - (float)BannerGUI.I.red_banner.height);
			MainGUI.Instance.Picture(this.redPos, BannerGUI.I.yellow_banner);
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.yellow_glow_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.yellow_glow_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 65)), BannerGUI.I.yellow_glow, new Vector2(this.scale, this.scale));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.killstreaks_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.killstreaks_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.RotateGUI(BannerGUI.I.star_rotation.Evaluate(this.GNAME.Get() * this.speed) * 90f, new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 5)));
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 5)), BannerGUI.I.wtask_star, new Vector2(this.scale, this.scale));
			MainGUI.Instance.RotateGUI(0f, Vector2.zero);
			MainGUI.Instance.BeginGroup(new Rect((float)(Screen.width / 2 - BannerGUI.I.yellow_glow.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 70 - BannerGUI.I.yellow_glow.height / 2), (float)BannerGUI.I.yellow_glow.width, (float)BannerGUI.I.yellow_glow.height));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.killstreaks_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			MainGUI.Instance.TextLabel(new Rect(0f, 0f, (float)BannerGUI.I.yellow_glow.width, (float)BannerGUI.I.yellow_glow.height), this.current.wtask.tempName, 12, "#FFFFFF_Micra", TextAnchor.MiddleCenter, true);
			MainGUI.Instance.EndGroup();
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 87)), BannerGUI.I.wtask_complited, new Vector2(1f, 1f));
			MainGUI.Instance.EndGroup();
		}

		// Token: 0x04000589 RID: 1417
		private float scale = 1f;

		// Token: 0x0400058A RID: 1418
		public Vector2 redPos = Vector2.zero;

		// Token: 0x0400058B RID: 1419
		public BannerInfo current = new BannerInfo();
	}
}
