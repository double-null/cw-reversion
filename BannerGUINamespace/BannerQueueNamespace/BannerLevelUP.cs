using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D5 RID: 213
	internal class BannerLevelUP : BannerQueueItem
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0002A750 File Offset: 0x00028950
		public BannerLevelUP()
		{
			this.clip = BannerGUI.I.levelup_clip;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002A78C File Offset: 0x0002898C
		public override void Init(float speed)
		{
			this.screenableBanner = Main.UserInfo.settings.graphics.LevelUpTakeScreen;
			this.screenShotEvent = ScreenShotEvent.levelUp;
			this.inited = true;
			this.speed = speed;
			base.Init(speed);
			this.initTimer = BannerGUI.I.red_banner_POSYKeys[BannerGUI.I.red_banner_POSYKeys.Length - 1].time;
			this.GNAME.InitTimer(this.initTimer / this.speed);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002A810 File Offset: 0x00028A10
		public override void OnGUI()
		{
			base.OnGUI();
			MainGUI.Instance.BeginGroup(new Rect(0f, -15f, (float)Screen.width, (float)Screen.height));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.red_banner_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.redPos = new Vector2((float)(Screen.width / 2 - BannerGUI.I.red_banner.width / 2), BannerGUI.I.red_banner_POSY.Evaluate(this.GNAME.Get() * this.speed) * (float)BannerGUI.I.red_banner.height - (float)BannerGUI.I.red_banner.height);
			MainGUI.Instance.Picture(this.redPos, BannerGUI.I.yellow_banner);
			if (this.current.sp != 0)
			{
				MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.achiv_glow_Alpha.Evaluate(this.GNAME.Get() * this.speed));
				this.scale = BannerGUI.I.yellow_glow_Scale.Evaluate(this.GNAME.Get() * this.speed);
				MainGUI.Instance.BeginGroup(new Rect((float)(Screen.width / 2 - BannerGUI.I.yellow_glow.width / 2), 80f, (float)BannerGUI.I.yellow_glow.width, (float)(BannerGUI.I.yellow_glow.height * 2)));
				MainGUI.Instance.TextLabel(new Rect(-158f, 78f, (float)BannerGUI.I.yellow_glow.width, (float)BannerGUI.I.yellow_glow.height), "+" + this.current.sp, 25, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
				MainGUI.Instance.PictureCentered(new Vector2(160f, 125f), MainGUI.Instance.spIcon_med, Vector2.one);
				MainGUI.Instance.EndGroup();
			}
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.killstreaks_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.killstreaks_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2)), CarrierGUI.I.bigRanks[this.current.level], new Vector2(this.scale, this.scale));
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 85)), BannerGUI.I.level_complited, new Vector2(1f, 1f));
			MainGUI.Instance.EndGroup();
		}

		// Token: 0x04000582 RID: 1410
		private float scale = 1f;

		// Token: 0x04000583 RID: 1411
		public BannerInfo current = new BannerInfo();

		// Token: 0x04000584 RID: 1412
		public Vector2 redPos = Vector2.zero;
	}
}
