using System;
using UnityEngine;

namespace BannerGUINamespace.BannerQueueNamespace
{
	// Token: 0x020000D9 RID: 217
	internal class BannerAchievement : BannerQueueItem
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0002B478 File Offset: 0x00029678
		public BannerAchievement()
		{
			this.clip = BannerGUI.I.achiv_clip;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x0002B4B4 File Offset: 0x000296B4
		public override void Init(float speed)
		{
			this.screenableBanner = Main.UserInfo.settings.graphics.AchievementTakeScreen;
			base.Init(speed);
			this.speed = speed;
			this.initTimer = BannerGUI.I.red_banner_POSYKeys[BannerGUI.I.red_banner_POSYKeys.Length - 1].time;
			this.GNAME.InitTimer(this.initTimer / this.speed);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0002B52C File Offset: 0x0002972C
		public override void OnGUI()
		{
			base.OnGUI();
			MainGUI.Instance.BeginGroup(new Rect(0f, -15f, (float)Screen.width, (float)Screen.height));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.red_banner_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.redPos = new Vector2((float)(Screen.width / 2 - BannerGUI.I.red_banner.width / 2), BannerGUI.I.red_banner_POSY.Evaluate(this.GNAME.Get() * this.speed) * (float)BannerGUI.I.red_banner.height - (float)BannerGUI.I.red_banner.height);
			MainGUI.Instance.Picture(this.redPos, BannerGUI.I.yellow_banner);
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.killstreaks_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.killstreaks_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2)), CarrierGUI.I.achievementsUnocked[this.current.achievement.index], new Vector2(this.scale, this.scale));
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 - 85)), BannerGUI.I.achiv_complited, new Vector2(1f, 1f));
			MainGUI.Instance.color = new Color(1f, 1f, 1f, BannerGUI.I.achiv_glow_Alpha.Evaluate(this.GNAME.Get() * this.speed));
			this.scale = BannerGUI.I.yellow_glow_Scale.Evaluate(this.GNAME.Get() * this.speed);
			MainGUI.Instance.PictureCentered(new Vector2((float)(Screen.width / 2), (float)(BannerGUI.I.red_banner.height / 2 + 15)), BannerGUI.I.yellow_glow, new Vector2(this.scale, this.scale));
			MainGUI.Instance.BeginGroup(new Rect((float)(Screen.width / 2 - BannerGUI.I.yellow_glow.width / 2), 0f, (float)BannerGUI.I.yellow_glow.width, (float)(BannerGUI.I.yellow_glow.height * 2)));
			MainGUI.Instance.TextLabel(new Rect(-128f, 78f, (float)BannerGUI.I.yellow_glow.width, (float)BannerGUI.I.yellow_glow.height), "+" + Main.UserInfo.achievementsInfos[this.current.achievement.index].prize, 19, "#FFFFFF_Micra", TextAnchor.MiddleRight, true);
			MainGUI.Instance.PictureCentered(new Vector2(190f, 125f), BannerGUI.I.achiv_whitecr, Vector2.one);
			MainGUI.Instance.EndGroup();
			MainGUI.Instance.EndGroup();
		}

		// Token: 0x0400058C RID: 1420
		private float scale = 1f;

		// Token: 0x0400058D RID: 1421
		public Vector2 redPos = Vector2.zero;

		// Token: 0x0400058E RID: 1422
		public BannerInfo current = new BannerInfo();
	}
}
